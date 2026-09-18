# ==========================================================
# AUTO THEFT GANGSTERS - UNIFIED GAME SERVER 9555
# Protocols: TCP Big-Endian Length + Sproto 0-packing
# Header: Package (Tag 0: type, Tag 1: session)
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
                if val and isinstance(val[0], int): v = b"\x04" + b"".join([struct.pack("<i", x) for x in val])
                else:
                    items = []
                    for it in val:
                        if not isinstance(it, (bytes, bytearray)): it = str(it).encode('utf-8')
                        items.append(struct.pack("<I", len(it)) + it)
                    v = b"".join(items)
            elif isinstance(val, dict):
                # Sproto Maps are encoded as lists of structs
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
    print(f"[DATA] Loaded {len(mission_logic_db)} missions, {len(equip_db)} equipments.")

load_game_data()

def load_db():
    global all_accounts_chars
    with db_lock:
        if os.path.exists(CHAR_DB):
            try:
                with open(CHAR_DB, "r") as f: all_accounts_chars = json.load(f)
            except: all_accounts_chars = {}
        else: all_accounts_chars = {}

def save_chars(data):
    with db_lock:
        try:
            with open(CHAR_DB, "w") as f: json.dump(data, f, indent=4)
        except: pass

load_db()

# ==========================================================
# RECONSTRUCTED SCHEMAS (Verified Tags)
# ==========================================================

def world_to_protocol(val): return int(val * PROTOCOL_COORD_SCALE)
def protocol_to_world(val): return float(val) / PROTOCOL_COORD_SCALE

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"}, 
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"}, 
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    # characterVisual: name(0), ModeId(1), HeadId(2), BodyId(3), LegId(4), WeaponId(5)
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

def get_general(c):
    # general: name(0), profession(1), lineIndex(2), mapInfoId(3), tutorial(4)
    return encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('mapId', "11"))), (4, 1)])

def get_movement(pos):
    # pos = [x, y, z, o] - world units
    p = encode_sproto([(0, world_to_protocol(pos[0])), (1, world_to_protocol(pos[1])), (2, world_to_protocol(pos[2])), (3, world_to_protocol(pos[3]))])
    # movement: pos(0), pos2(1)
    return encode_sproto([(0, p), (1, p)])

def get_property(c):
    # property: verified tags 13-18
    return encode_sproto([
        (13, c.get('cash', 1000)), (14, c.get('gold', 0)), (15, c.get('diamond', 0)),
        (16, c.get('guild_contribute', 0)), (17, c.get('battle_coin', 0)), (18, c.get('activity_coin', 0))
    ])

def get_full_char(c):
    prof, lv = c.get('prof', 0), c.get('level', 1)
    hp, atk, def_val, hit, dge = 3000, 300, 35, 480, 60
    
    # attribute_other: hp(0), exp(1), level(2), combValue(3)
    cv = calculate_power(hp, atk, def_val, hit, dge)
    attr_oth = encode_sproto([(0, hp), (1, c.get('exp', 0)), (2, lv), (3, cv), (15, 1)])
    
    prop = get_property(c)
    mv = get_movement(c.get('pos', [298.60, 1.00, -170.05, 0]))
    
    # runtime_agent: attribute(6), attribute_all(7)
    attr_data = encode_sproto([(0, hp), (2, atk), (3, def_val), (4, hit), (5, dge)])
    run = encode_sproto([(6, attr_data), (7, attr_data)])
    
    # Skills map (implicit keys)
    skills_map = {}
    for sid, sd in c.get('skills', {}).items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd.get('unlock',1)), (4, sd.get('pos2',0)), (5, sd.get('dis',False))])
    
    # Equip map (implicit keys)
    equip_map = {}
    for slot, item in c.get('equip', {}).items():
        equip_map[slot] = encode_sproto([(0, int(item['uid'])), (1, str(item['id'])), (2, True), (3, item.get('lv', 1)), (5, 1), (6, item.get('qual', 1)), (7, [0]*8)])

    # character: Tags verified from character.cs
    return encode_sproto([(0, c['id']), (1, get_general(c)), (2, attr_oth), (5, prop), (6, get_visual(c['name'], prof)), (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)])

def get_char_ov(c):
    # character_overview: id(0), general(1), attribute_overview(2), visual(3), createtime(4), forbidden(5)
    gen = get_general(c)
    attr = encode_sproto([(0, c.get('level', 1)), (1, 5000)]) # attribute_overview: level(0), combValue(1)
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c['name'], c.get('prof', 0))), (4, c.get('createtime', int(time.time()))), (5, 0)])

def calculate_power(hp, atk, def_val, hit, dge):
    return int((hp * 0.1) + (atk * 2.5) + (def_val * 5) + (hit * 1.5) + (dge * 1.5))

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
    print(f"[CONNECT] {addr}"); acc_id, picked_char = None, None
    def send_rpc_push(tag, data):
        ph = encode_sproto([(0, tag)])
        pf = SprotoPacker.pack(ph + data)
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass
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
            body = SprotoDecoder(raw, dec.consumed).decode()

            if msg == 4: # Login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                print(f"[LOGIN] account={acc_id} session={session}")
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (13, 1)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 103: # CharList
                with db_lock: chars = all_accounts_chars.get("0", {}).get(acc_id, [])
                resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 118: # RandomName
                name = f"Hero{random.randint(1000, 9999)}"
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, name)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 104: # CharCreate
                c_data = SprotoDecoder(body.get(0, b"")).decode() # nested general
                name, prof = c_data.get(0, b"").decode('utf-8'), get_val_int(c_data, 1, 0)
                cid = int(time.time() * 1000) % 1000000000
                nc = {'id': cid, 'name': name, 'prof': prof, 'level': 1, 'hp': 3000, 'exp': 0, 'createtime': int(time.time()), 'skills': {}, 'backpack': [], 'equip': {}, 'mapId': "11", 'pos': [298.60, 1.00, -170.05, 0]}
                with db_lock: all_accounts_chars.setdefault("0", {}).setdefault(acc_id, []).append(nc); save_chars(all_accounts_chars)
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, get_char_ov(nc)), (1, 0)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 105: # CharPick
                char_id = get_val_int(body, 0)
                with db_lock: picked_char = next((c for c in all_accounts_chars.get("0", {}).get(acc_id, []) if c['id'] == char_id), None)
                print(f"[PICK] char_id={char_id} found={picked_char is not None}")
                # Success errno = 1 (Source Verified)
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, 1 if picked_char else 0)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if picked_char:
                    online_clients[char_id] = (conn, picked_char['mapId'], 1, picked_char)
                    # Sequence: 614 -> 504 -> 503
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, {}), (13, 1), (14, int(time.time()))]))
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(picked_char['pos']))]))
                    send_rpc_push(503, encode_sproto([(0, picked_char['mapId']), (1, 1), (2, 1)]))
            elif msg == 100: # MapReady
                print(f"[MAP-READY] received from {addr}")
                send_rpc_push(654, encode_sproto([(0, 1)]))
            elif msg == 218: # Heartbeat
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif session is not None:
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
    except Exception: traceback.print_exc()
    finally:
        if picked_char: online_clients.pop(picked_char['id'], None)
        conn.close()

srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM); srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1); srv.bind(("0.0.0.0", PORT)); srv.listen(50)
print(f"Unified Server 9555 Active on {PORT}"); 
while True:
    try: cl, ad = srv.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
    except: pass
