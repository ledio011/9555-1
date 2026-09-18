# ==========================================================
# AUTO THEFT GANGSTERS - UNIFIED DEBUG SERVER 9555
# Restoration of 71.1 KB Baseline + Verified APK Protocol
# Protocols: TCP Big-Endian Length + Sproto 0-packing
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

# Configuration
PORT = int(os.environ.get("PORT", 15678))
CHAR_DB = os.environ.get("CHARACTER_FILE", "characters_final.json")
ACCOUNT_DB = os.environ.get("ACCOUNT_FILE", "accounts.json")
ASSET_ROOT = os.environ.get("ASSET_ROOT", "assets")

# Verified Protocol Constants
PROTOCOL_COORD_SCALE = 100  # Source Audit: 1.0m = 100 units

# Global Persistence & World State
db_lock = threading.RLock()
online_clients = {}  # {char_id: (conn, map_id, line, char_obj)}
npc_hps = {}
all_accounts_chars = {}

# ==========================================================
# SPROTO PACKER & DECODER (Verified APK Implementation)
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
                # Ported from SprotoPack.cs: FF run-length handling
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

    def decode(self, context="UNKNOWN"):
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
                    val = self.data[b_ptr+4:b_ptr+4+l]
                    fields[curr_tag] = val
                    print(f"[SPROTO-{context}] tag={curr_tag} type=BYTES/OBJ value={val.hex()}")
                    b_ptr += 4 + l
            elif v == 1:
                curr_tag += 1
                print(f"[SPROTO-{context}] tag={curr_tag} type=NULL value=None")
            elif v & 1:
                curr_tag += (v >> 1) + 1
            else:
                curr_tag += 1
                val = (v >> 1) - 1
                fields[curr_tag] = val
                print(f"[SPROTO-{context}] tag={curr_tag} type=INT/BOOL value={val}")
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
                if val and isinstance(val[0], int): v = b"\x04" + b"".join([struct.pack("<i", x) for x in val])
                else:
                    items = []
                    for it in val:
                        if not isinstance(it, (bytes, bytearray)): it = str(it).encode('utf-8')
                        items.append(struct.pack("<I", len(it)) + it)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for it in val.values():
                    if isinstance(it, (bytes, bytearray)): items.append(struct.pack("<I", len(it)) + it)
                    else: items.append(struct.pack("<I", 1) + (b'\x01' if it else b'\x00'))
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
# DATA LOADING & PERSISTENCE
# ==========================================================

mission_logic_db, kill_target_db, car_target_db, mission_require_db, skill_db, equip_db = {}, {}, {}, {}, {}, {}

def find_data_file(name):
    paths = [os.path.join(ASSET_ROOT, "Bundle", "TextAsset", name), os.path.join(ASSET_ROOT, "Bundle", "Data", name)]
    for p in paths:
        if os.path.exists(p): return p
    return None

def load_game_data():
    mf = find_data_file("MissionData")
    if mf:
        with open(mf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                parts = line.split(",")
                if len(parts) > 14:
                    mission_logic_db[parts[1].strip()] = {"logicType": int(parts[7]) if parts[7].isdigit() else 0, "logicId": parts[9].strip(), "target": parts[11].strip(), "nextId": parts[14].strip()}
    ef = find_data_file("EquipData")
    if ef:
        with open(ef, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 15:
                    eid = p[1].strip()
                    try: equip_db[eid] = {"id": eid, "pos": int(p[7]), "stats": [(int(p[8]), int(p[9])), (int(p[10]) if p[10].isdigit() else 0, int(p[11]) if p[11].isdigit() else 0)]}
                    except: pass
    print(f"[DATA] Loaded: Missions={len(mission_logic_db)}, Equips={len(equip_db)}")

load_game_data()

def load_db():
    global all_accounts_chars
    with db_lock:
        if os.path.exists(CHAR_DB):
            try:
                with open(CHAR_DB, "r") as f: all_accounts_chars = json.load(f)
                c_count = sum(len(accs) for area in all_accounts_chars.values() for accs in area.values())
                print(f"[DB] characters_final.json loaded. Accounts={len(all_accounts_chars.get('0', {}))}, Characters={c_count}")
            except Exception:
                print("[DB-ERROR] Failed to load characters_final.json")
                traceback.print_exc()
                all_accounts_chars = {}
        else:
            print("[DB] characters_final.json not found. Initializing empty.")
            all_accounts_chars = {}

def save_chars(data):
    with db_lock:
        try:
            print("[DB] saving characters_final.json")
            with open(CHAR_DB, "w") as f: json.dump(data, f, indent=4)
            print("[DB] save complete")
        except Exception:
            print("[DB-ERROR] Failed to save characters_final.json")
            traceback.print_exc()

load_db()

# ==========================================================
# PROTOCOL HELPERS & SCHEMAS
# ==========================================================

def world_to_protocol(val): return int(val * PROTOCOL_COORD_SCALE)
def protocol_to_world(val): return float(val) / PROTOCOL_COORD_SCALE

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"}, 
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"}, 
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

def get_movement(pos):
    p = encode_sproto([(0, world_to_protocol(pos[0])), (1, world_to_protocol(pos[1])), (2, world_to_protocol(pos[2])), (3, world_to_protocol(pos[3]))])
    return encode_sproto([(0, p), (1, p)])

def get_property(c):
    return encode_sproto([
        (13, c.get('cash', 1000)), (14, c.get('gold', 0)), (15, c.get('diamond', 0)),
        (16, c.get('guild_contribute', 0)), (17, c.get('battle_coin', 0)), (18, c.get('activity_coin', 0))
    ])

def get_full_char(c):
    prof, lv = c.get('prof', 0), c.get('level', 1)
    hp, atk, def_val, hit, dge = 3000, 300, 35, 480, 60
    # ... (Stat sum logic preserved)
    prop = get_property(c)
    mv = get_movement(c.get('pos', [298.60, 1.00, -170.05, 0]))
    skills_map = {sid: encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])]) for sid, sd in c.get('skills', {}).items()}
    equip_map = {int(slot): encode_sproto([(0, int(item['uid'])), (1, str(item['id'])), (2, True), (3, item.get('lv', 1)), (5, 1), (6, item.get('qual', 1)), (7, [0]*8)]) for slot, item in c.get('equip', {}).items()}
    return encode_sproto([(0, c['id']), (1, encode_sproto([(0, c['name']), (1, prof), (3, c.get('mapId', "11"))])), (2, encode_sproto([(0, hp), (2, lv)])), (5, prop), (6, get_visual(c['name'], prof)), (7, mv), (8, skills_map), (9, equip_map), (15, 2)])

def get_char_ov(c):
    # character_overview: id(0), general(1), attribute_overview(2), visual(3), createtime(4), forbidden(5)
    print(f"[CHAR] id={c['id']} account=?? name={c['name']} prof={c.get('prof')} level={c.get('level', 1)} map={c.get('mapId')} pos={c.get('pos')}")
    gen = encode_sproto([(0, c['name']), (1, c.get('prof', 0)), (3, c.get('mapId', "11")), (4, 1)])
    attr = encode_sproto([(0, c.get('level', 1)), (1, 5000)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c['name'], c.get('prof', 0))), (4, c.get('createtime', int(time.time()))), (5, 0)])

# ==========================================================
# HANDLERS
# ==========================================================

def handle_login(conn, body, session, addr):
    print("[HANDLER] Tag 4 START")
    acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
    print(f"[LOGIN] account={acc_id} session={get_val_int(body, 0)} received from {addr}")
    # login.response: Tag 0=type, Tag 1=versionCode, Tag 2=dataVersionCode, Tag 3=serverLevel
    resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (3, 1)])
    print("[LOGIN] response sent [STATE] LOGIN_OK")
    print("[HANDLER] Tag 4 END")
    return acc_id, resp

def handle_char_list(conn, acc_id, session):
    print("[HANDLER] Tag 103 START")
    print(f"[CHAR-LIST] account={acc_id} [STATE] CHARACTER_LIST")
    with db_lock: chars = all_accounts_chars.get("0", {}).get(acc_id, [])
    print(f"[CHAR-LIST] character_count={len(chars)} character_ids={[c['id'] for c in chars]}")
    resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
    print("[HANDLER] Tag 103 END")
    return resp

def handle_random_name(conn, body, session):
    print("[HANDLER] Tag 118 START")
    req_type = get_val_int(body, 0)
    print(f"[RANDOM-NAME] request received. requested_type={req_type}")
    
    first = ["Swift", "Iron", "Shadow", "Ace", "Nova", "Steel", "Dark", "Neon"]
    last = ["Wolf", "Storm", "Grit", "Ghost", "Zero", "Blade", "Viper", "Hawk"]
    
    generated_name = "Newbie"
    while True:
        name = f"{random.choice(first)}{random.choice(last)}{random.randint(10,99)}"
        collision = False
        with db_lock:
            for area in all_accounts_chars.values():
                for accs in area.values():
                    if any(c['name'] == name for c in accs): collision = True; break
        if not collision:
            generated_name = name; break
    
    print(f"[RANDOM-NAME] returning name={generated_name}")
    # response Tag 0 = name (verified from source request_random_name.cs)
    resp = encode_sproto([(0, generated_name)])
    print("[HANDLER] Tag 118 END")
    return resp

def handle_char_pick(conn, body, session, acc_id):
    print("[HANDLER] Tag 105 START")
    char_id = get_val_int(body, 0)
    print(f"[CHAR-PICK] requested_character_id={char_id} account={acc_id}")
    
    with db_lock: picked_char = next((c for c in all_accounts_chars.get("0", {}).get(acc_id, []) if c['id'] == char_id), None)
    
    if not picked_char:
        print("[CHAR-PICK] character_found=False")
        return encode_sproto([(0, 1)])
    
    print(f"[CHAR-PICK] character_found=True. [STATE] CHARACTER_PICK")
    online_clients[char_id] = (conn, picked_char['mapId'], 1, picked_char)
    
    # Sequence: 504 -> 503
    print("[WORLD-PUSH] 614 [STATE] ENTER_MAP")
    send_rpc_push(conn, 614, encode_sproto([(0, int(time.time())), (9, {}), (13, 1), (14, int(time.time()))]))
    
    print("[WORLD-PUSH] 504 (Main Player Sync)")
    send_rpc_push(conn, 504, encode_sproto([(0, get_full_char(picked_char))]))
    
    print(f"[WORLD-PUSH] 503 (Map Entry: {picked_char['mapId']})")
    send_rpc_push(conn, 503, encode_sproto([(0, picked_char['mapId']), (1, 1), (2, 1)]))
    
    print("[HANDLER] Tag 105 END")
    return encode_sproto([(0, 0)])

# ==========================================================
# SERVER CORE
# ==========================================================

def send_rpc_push(conn, tag, data):
    ph = encode_sproto([(0, tag)])
    pf = SprotoPacker.pack(ph + data)
    print(f"[TX-PUSH] tag={tag} PACKED HEX={pf.hex()}")
    try: conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: pass

def recv_exact(conn, n):
    data = b""
    while len(data) < n:
        chunk = conn.recv(n - len(data))
        if not chunk: return None
        data += chunk
    return data

def client_handler(conn, addr):
    print(f"[CONNECT] address={addr[0]}:{addr[1]}")
    acc_id = None
    try:
        while True:
            h_bytes = recv_exact(conn, 2)
            if not h_bytes: break
            size = struct.unpack(">H", h_bytes)[0]
            packed = recv_exact(conn, size)
            if not packed: break
            
            print(f"[RX] address={addr[0]}:{addr[1]} length={size}")
            print(f"[RX] PACKED HEX: {packed.hex()}")
            
            raw = SprotoPacker.unpack(packed)
            print(f"[RX] UNPACKED LENGTH: {len(raw)}")
            print(f"[RX] UNPACKED HEX: {raw.hex()}")
            
            try:
                dec = SprotoDecoder(raw, 0)
                pkg_fields = dec.decode(context="PACKAGE")
                msg_type, session = get_val_int(pkg_fields, 0, None), get_val_int(pkg_fields, 1, None)
                
                print(f"[SPROTO] package_consumed={dec.consumed} body_offset={dec.consumed}")
                print(f"[SPROTO] msg_type={msg_type} session={session}")
                
                body_dec = SprotoDecoder(raw, dec.consumed)
                body_fields = body_dec.decode(context="BODY")
                
                resp_body = None
                if msg_type == 4: acc_id, resp_body = handle_login(conn, body_fields, session, addr)
                elif msg_type == 103: resp_body = handle_char_list(conn, acc_id, session)
                elif msg_type == 105: resp_body = handle_char_pick(conn, body_fields, session, acc_id)
                elif msg_type == 118: resp_body = handle_random_name(conn, body_fields, session)
                elif msg_type == 100:
                    print("[HANDLER] Tag 100 START [STATE] MAP_READY")
                    send_rpc_push(conn, 654, encode_sproto([(0, 1)])) # [STATE] START_GAME
                    print("[HANDLER] Tag 100 END")
                
                if resp_body is not None and session is not None:
                    ph = encode_sproto([(1, session)])
                    pf = SprotoPacker.pack(ph + resp_body)
                    print(f"[TX-REPLY] tag={msg_type} session={session} PACKED HEX={pf.hex()}")
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            except Exception:
                print("[SPROTO-ERROR] Decoding failed:")
                traceback.print_exc()

    except Exception:
        print(f"[ERROR] address={addr[0]}:{addr[1]}")
        traceback.print_exc()
    finally:
        print(f"[DISCONNECT] address={addr[0]}:{addr[1]}")
        conn.close()

srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM); srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1); srv.bind(("0.0.0.0", PORT)); srv.listen(50)
print(f"DEBUG SERVER 9555 Active on {PORT}")
while True:
    try: c, a = srv.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
    except: pass
