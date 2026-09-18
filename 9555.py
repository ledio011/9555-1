# ==========================================================
# AUTO THEFT GANGSTERS - UNIFIED GAME SERVER 9555
# Restoration of 71.1 KB Baseline + Verified APK Protocol
# Protocols: TCP Big-Endian Length + Sproto 0-packing
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

# Configuration
PORT = int(os.environ.get("PORT", 15678))
CHAR_DB = os.environ.get("CHARACTER_FILE", "characters_final.json")
ASSET_ROOT = os.environ.get("ASSET_ROOT", "assets")

# Verified Protocol Constants
PROTOCOL_COORD_SCALE = 100 # Source Audit: 1.0m = 100 units

# Global Persistence & World State
db_lock = threading.RLock()
online_clients = {} # {char_id: (conn, map_id, line, char_obj)}
npc_hps = {}
all_accounts_chars = {}

# ==========================================================
# SPROTO CORE (Verified APK Implementation)
# ==========================================================

class SprotoPacker:
    @staticmethod
    def pack(data):
        out = bytearray(); n = len(data); i = 0
        while i < n:
            chunk = data[i:i+8]
            if len(chunk) < 8: chunk += b'\x00' * (8 - len(chunk))
            mask = 0
            for j in range(8):
                if chunk[j] != 0: mask |= (1 << j)
            if mask == 0xFF:
                run = []
                while i < n:
                    c = data[i:i+8]
                    if len(c) < 8: break
                    m = 0
                    for j in range(8):
                        if c[j] != 0: m |= (1 << j)
                    if m == 0xFF:
                        run.append(c); i += 8
                        if len(run) == 256: break
                    else: break
                out.append(0xFF); out.append(len(run) - 1)
                for b in run: out.extend(b)
            else:
                out.append(mask)
                for j in range(8):
                    if mask & (1 << j): out.append(chunk[j])
                i += 8
        return bytes(out)

    @staticmethod
    def unpack(data):
        out = bytearray(); i = 0; n = len(data)
        while i < n:
            mask = data[i]; i += 1
            if mask == 0xFF:
                if i >= n: break
                count = (data[i] + 1) * 8; i += 1
                out.extend(data[i:i+count]); i += count
            else:
                for bit in range(8):
                    if mask & (1 << bit):
                        if i < n: out.append(data[i]); i += 1
                    else: out.append(0)
        return bytes(out)

class SprotoDecoder:
    def __init__(self, data, offset=0):
        self.data = data
        self.offset = offset
        self.consumed = 0

    def decode(self):
        if len(self.data) < self.offset + 2: return {}
        fn = struct.unpack("<H", self.data[self.offset:self.offset+2])[0]
        h_ptr = self.offset + 2
        b_ptr = h_ptr + fn * 2
        fields, curr_tag = {}, -1
        for i in range(fn):
            if h_ptr + i*2 + 2 > len(self.data): break
            v = struct.unpack("<H", self.data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
            if v == 0:
                curr_tag += 1
                if b_ptr + 4 <= len(self.data):
                    l = struct.unpack("<I", self.data[b_ptr:b_ptr+4])[0]
                    fields[curr_tag] = self.data[b_ptr+4:b_ptr+4+l]
                    b_ptr += 4 + l
            elif v == 1: curr_tag += 1
            elif v & 1: curr_tag += (v >> 1) + 1
            else:
                curr_tag += 1
                fields[curr_tag] = (v >> 1) - 1
        self.consumed = b_ptr - self.offset
        return fields

def encode_sproto(fields):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    header = []; body = bytearray(); last_tag = -1
    for tag, val in fields:
        skip = tag - last_tag - 1
        if skip > 0: header.append(2 * (skip - 1) + 1)
        if val is None: header.append(1)
        elif isinstance(val, bool): header.append((1 if val else 0) * 2 + 2)
        elif isinstance(val, int):
            if 0 <= val <= 32766: header.append((val + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= val <= 2147483647: body += struct.pack("<I", 4) + struct.pack("<i", val)
                else: body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray, list, dict)):
            header.append(0)
            if isinstance(val, str): v = val.encode('utf-8')
            elif isinstance(val, list):
                v_body = bytearray()
                for it in val:
                    if isinstance(it, int): v_body.extend(struct.pack("<i", it))
                    else:
                        if not isinstance(it, (bytes, bytearray)): it = str(it).encode('utf-8')
                        v_body.extend(struct.pack("<I", len(it)) + it)
                v = (b"\x04" if val and isinstance(val[0], int) else b"") + v_body
            elif isinstance(val, dict):
                items = [struct.pack("<I", len(it)) + it for it in val.values() if isinstance(it, (bytes, bytearray))]
                v = b"".join(items)
            else: v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag
    res = struct.pack("<H", len(header))
    for h in header: res += struct.pack("<H", h)
    return res + body

def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, int): return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4: return struct.unpack("<i", val)[0]
        if len(val) == 8: return struct.unpack("<q", val)[0]
    return default

# ==========================================================
# DATA LOADING & NORMALIZATION
# ==========================================================

def normalize_character(c):
    c.setdefault('mapId', "11")
    c.setdefault('pos', [298.6, 1.0, -170.05, 0])
    c.setdefault('cash', 1000); c.setdefault('gold', 0); c.setdefault('diamond', 0)
    c.setdefault('exp', 0); c.setdefault('level', 1)
    c.setdefault('skills', {}); c.setdefault('equip', {})
    c.setdefault('createtime', int(time.time()))
    return c

def load_db():
    global all_accounts_chars
    with db_lock:
        if os.path.exists(CHAR_DB):
            try:
                with open(CHAR_DB, "r") as f: 
                    data = json.load(f)
                    for area in data.values():
                        for acc_id in area:
                            area[acc_id] = [normalize_character(c) for c in area[acc_id]]
                    all_accounts_chars = data
            except: all_accounts_chars = {}
        else: all_accounts_chars = {}

def save_chars(data):
    with db_lock:
        try:
            with open(CHAR_DB, "w") as f: json.dump(data, f, indent=4)
        except: 
            print("[ERROR] Database save failed")
            traceback.print_exc()

load_db()

# ==========================================================
# SOURCE-VERIFIED SCHEMAS (character.cs Mapping)
# ==========================================================

def world_to_protocol(val): return int(val * PROTOCOL_COORD_SCALE)
def protocol_to_world(val): return float(val) / PROTOCOL_COORD_SCALE

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"}, 
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"}, 
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"])])

def get_movement_struct(pos):
    p = encode_sproto([(0, world_to_protocol(pos[0])), (1, world_to_protocol(pos[1])), (2, world_to_protocol(pos[2])), (3, world_to_protocol(pos[3]))])
    return encode_sproto([(0, p), (1, p)])

def get_full_char(c):
    prof, lv = c['prof'], c['level']
    hp, atk, def_val, hit, dge = 3000, 300, 35, 480, 60
    
    gen = encode_sproto([(0, c['name']), (1, prof), (2, 1), (3, c['mapId']), (4, 1)])
    attr_oth = encode_sproto([(0, hp), (1, c['exp']), (2, lv), (3, 5000), (14, 0), (15, 1)])
    prop = encode_sproto([(13, c['cash']), (14, c['gold']), (15, c['diamond']), (16, 0), (17, 0), (18, 0)])
    mv = get_movement_struct(c['pos'])
    
    attr_data = encode_sproto([(0, hp), (2, atk), (3, def_val), (4, hit), (5, dge), (13, 500)])
    run = encode_sproto([(6, attr_data), (7, attr_data)])
    
    skills = [encode_sproto([(0, sid), (1, sd['lv']), (2, sd['pos']), (3, 1), (4, 0), (5, False)]) for sid, sd in c['skills'].items()]
    equips = [encode_sproto([(0, int(item['uid'])), (1, str(item['id'])), (2, True), (3, item.get('lv', 1)), (5, 1), (6, item.get('qual', 1))]) for slot, item in c['equip'].items()]

    return encode_sproto([(0, c['id']), (1, gen), (2, attr_oth), (5, prop), (6, get_visual(c['name'], prof)), (7, mv), (8, skills), (9, equips), (13, run), (15, 2)])

def get_char_ov(c):
    gen = encode_sproto([(0, c['name']), (1, c['prof']), (3, c['mapId']), (4, 1)])
    attr = encode_sproto([(0, c['level']), (1, 5000)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c['name'], c['prof'])), (4, c['createtime']), (5, 0)])

# ==========================================================
# SERVER CORE
# ==========================================================

def recv_exact(conn, n):
    data = b""
    while len(data) < n:
        chunk = conn.recv(n - len(data))
        if not chunk: return None
        data += chunk
    return data

def client_handler(conn, addr):
    print(f"[1] TCP CONNECTION: {addr} connected")
    acc_id, picked_char = None, None
    def send_rpc_push(tag, data, name):
        print(f"[{name}] START")
        if tag == 614: print(f"[PUSH-614] encoded_fields: serverTime, func_info, server_level")
        elif tag == 504:
            print(f"[PUSH-504] character_id={picked_char['id']} name={picked_char['name']} mapId={picked_char['mapId']}")
            print(f"[PUSH-504] character_pos={picked_char['pos']}")
        
        ph = encode_sproto([(0, tag)])
        pf = SprotoPacker.pack(ph + data)
        print(f"[{name}] encoded_len={len(data)} packet_len={len(pf)}")
        if tag == 504: print(f"[PUSH-504] packed_HEX={pf.hex().upper()}")
        
        try: 
            conn.sendall(struct.pack(">H", len(pf)) + pf)
            print(f"[{name}] packet sent")
        except: print(f"[ERROR] Failed to send {name}")
        print(f"[{name}] END")

    try:
        while True:
            h = recv_exact(conn, 2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = recv_exact(conn, size)
            if not data: break
            
            raw = SprotoPacker.unpack(data)
            dec = SprotoDecoder(raw); pkg = dec.decode()
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            body_dec = SprotoDecoder(raw, dec.consumed)
            body = body_dec.decode()

            print(f"[RX] len={size} packed={data.hex().upper()} unpacked_len={len(raw)} tag={msg} session={session} body_offset={dec.consumed} body_fields={list(body.keys())}")

            if msg == 4: # Login
                print(f"[LOGIN] LOGIN START")
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                print(f"[LOGIN] account_id={acc_id} version={body.get(3)} platform={body.get(4)}")
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, 2), (1, "1.012.017"), (2, "200")]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                print(f"[LOGIN] LOGIN RESPONSE SENT")
                print(f"[LOGIN] LOGIN END")
            elif msg == 103: # CharList
                print(f"[CHAR-LIST] CHAR LIST START")
                with db_lock: chars = all_accounts_chars.get("0", {}).get(acc_id, [])
                print(f"[CHAR-LIST] account_id={acc_id} num_chars={len(chars)}")
                for c in chars: print(f"[CHAR-LIST] char_id={c['id']} name={c['name']}")
                resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                print(f"[CHAR-LIST] response sent")
                print(f"[CHAR-LIST] CHAR LIST END")
            elif msg == 118: # RandomName
                print(f"[RANDOM-NAME] RANDOM NAME START")
                print(f"[RANDOM-NAME] requested_type={body.get(0)}")
                name = f"Hero{random.randint(1000, 9999)}"
                print(f"[RANDOM-NAME] generated_name={name}")
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, name)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                print(f"[RANDOM-NAME] response sent")
                print(f"[RANDOM-NAME] RANDOM NAME END")
            elif msg == 104: # CharCreate
                print(f"[CREATE] CREATE START")
                raw_nested = body.get(0, b"")
                print(f"[CREATE] raw_nested_bytes={raw_nested.hex().upper()}")
                c_req = SprotoDecoder(raw_nested).decode()
                print(f"[CREATE] decoded_nested_fields={list(c_req.keys())}")
                name, prof = c_req.get(0, b"").decode('utf-8'), get_val_int(c_req, 1, 0)
                print(f"[CREATE] requested_name={name} profession={prof}")
                cid = int(time.time() * 1000) % 1000000000
                print(f"[CREATE] generated_character_id={cid}")
                nc = normalize_character({'id': cid, 'name': name, 'prof': prof})
                print(f"[CREATE] normalized_character_object={nc}")
                print(f"[CREATE] database save START")
                with db_lock: 
                    all_accounts_chars.setdefault("0", {}).setdefault(acc_id, []).append(nc)
                    save_chars(all_accounts_chars)
                    print(f"[CREATE] database save SUCCESS")
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, get_char_ov(nc)), (1, 0)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                print(f"[CREATE] response packet sent")
                print(f"[CREATE] CREATE END")
            elif msg == 105: # CharPick
                char_id = get_val_int(body, 0)
                print(f"[PICK] PICK START")
                print(f"[PICK] requested_character_id={char_id}")
                with db_lock: picked_char = next((c for c in all_accounts_chars.get("0", {}).get(acc_id, []) if c['id'] == char_id), None)
                if picked_char:
                    print(f"[PICK] character found: mapId={picked_char['mapId']} pos={picked_char['pos']}")
                    online_clients[char_id] = (conn, picked_char['mapId'], 1, picked_char)
                else: print(f"[PICK] character NOT found")
                
                errno_val = 1 if picked_char else 0
                print(f"[PICK] exact_errno_being_returned={errno_val}")
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, errno_val)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                print(f"[PICK] response sent")
                
                if picked_char:
                    print(f"[PICK] PICK SUCCESS")
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, []), (13, 1)]), "PUSH-614")
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement_struct(picked_char['pos']))]), "PUSH-504")
                    send_rpc_push(503, encode_sproto([(0, "11"), (1, 1), (2, 1)]), "PUSH-503")
            elif msg == 100: # MapReady
                print(f"[MAP-READY] MAP READY RECEIVED")
                print(f"[MAP-READY] session={session} body_fields={list(body.keys())}")
                print(f"[MAP-READY] current_picked_character={picked_char['name'] if picked_char else 'NONE'}")
                print(f"[MAP-READY] current_map={picked_char['mapId'] if picked_char else 'NONE'}")
                print(f"[MAP-READY] MAP READY RESPONSE/PUSH START")
                send_rpc_push(654, encode_sproto([(0, 1)]), "PUSH-654")
                print(f"[MAP-READY] END")
                print(f"[GAME] waiting for next client packet")
            elif msg == 218: # Heartbeat
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            else:
                print(f"[UNKNOWN] UNKNOWN TAG tag={msg} session={session} body_HEX={raw[dec.consumed:].hex().upper()}")
                print(f"[UNKNOWN] decoded_fields={body}")
                if session is not None:
                    pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)
    except Exception:
        print("[ERROR]")
        traceback.print_exc()
    finally:
        if picked_char: online_clients.pop(picked_char['id'], None)
        print(f"[1] TCP CONNECTION: {addr} disconnected")
        conn.close()

srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM); srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1); srv.bind(("0.0.0.0", PORT)); srv.listen(50)
print(f"Unified Debug Server 9555 Active on {PORT}"); 
while True:
    try: cl, ad = srv.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
    except: pass
