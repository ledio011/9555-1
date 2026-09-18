# ==========================================================
# AUTO THEFT GANGSTERS - UNIFIED GAME SERVER 9555
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
                if val and isinstance(val[0], int): v = b"\x04" + b"".join([struct.pack("<i", x) for x in val])
                else:
                    items = []
                    for it in val:
                        if not isinstance(it, (bytes, bytearray)): it = str(it).encode('utf-8')
                        items.append(struct.pack("<I", len(it)) + it)
                    v = b"".join(items)
            elif isinstance(val, dict):
                # Sproto Dictionary is encoded as a List of objects
                items = []
                for it in val.values():
                    if not isinstance(it, (bytes, bytearray)): it = str(it).encode('utf-8')
                    items.append(struct.pack("<I", len(it)) + it)
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
# DATA LOADING (Restored from Baseline)
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

# ==========================================================
# PERSISTENCE & HELPERS
# ==========================================================

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

def world_to_protocol(val):
    return int(val * PROTOCOL_COORD_SCALE)

def protocol_to_world(val):
    return float(val) / PROTOCOL_COORD_SCALE

def calculate_power(hp, atk, def_val, hit, dge):
    return int((hp * 0.1) + (atk * 2.5) + (def_val * 5) + (hit * 1.5) + (dge * 1.5))

def get_default_skills(prof):
    sid = "101" if prof == 0 else "201" if prof == 1 else "301"
    did = "104" if prof == 0 else "204" if prof == 1 else "304"
    return {sid: {"id": sid, "lv": 1, "pos": 0, "unlock": 1, "pos2": 0, "dis": False}, did: {"id": did, "lv": 1, "pos": 3, "unlock": 1, "pos2": 1, "dis": False}}

def init_mission_state(mid):
    logic = mission_logic_db.get(mid)
    if not logic: return {"alive_sids": [], "dead_sids": [], "progress": 0}
    lid, ltype = logic['logicId'], logic['logicType']
    alive_sids = []
    if ltype in [23, 1]:
        target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
        count = target.get('require', 1) if target else 1
        base_sid = 900000 + int(lid)
        for i in range(count): alive_sids.append(base_sid * 10 + i)
    return {"alive_sids": alive_sids, "dead_sids": [], "progress": 0}

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"}, 
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"}, 
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

def get_general(c):
    return encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('mapId', "11"))), (4, 1)])

def get_movement(pos):
    # pos = [x, y, z, o] - world units
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
    equips = c.get('equip', {})
    for item in equips.values():
        edata = equip_db.get(item['id'])
        if edata:
            for st, sv in edata['stats']:
                if st == 1001: atk += sv
                elif st == 1002: hp += sv
                elif st == 1003: def_val += sv
                elif st == 1004: hit += sv
                elif st == 1005: dge += sv
    cv = calculate_power(hp, atk, def_val, hit, dge)
    attr_oth = encode_sproto([(0, hp), (1, c.get('exp', 0)), (2, lv), (3, cv), (15, 1)])
    prop = get_property(c)
    mv = get_movement(c.get('pos', [298.60, 1.00, -170.05, 0]))
    run = encode_sproto([(6, encode_sproto([(0, hp), (2, atk), (3, def_val), (4, hit), (5, dge)])), (7, encode_sproto([(0, hp), (2, atk), (3, def_val), (4, hit), (5, dge), (13, 500)]))])
    skills_map = {sid: encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])]) for sid, sd in c.get('skills', get_default_skills(prof)).items()}
    equip_map = {int(slot): encode_sproto([(0, int(item['uid'])), (1, str(item['id'])), (2, True), (3, item.get('lv', 1)), (5, 1), (6, item.get('qual', 1)), (7, [0]*8)]) for slot, item in equips.items()}
    return encode_sproto([(0, c['id']), (1, get_general(c)), (2, attr_oth), (5, prop), (6, get_visual(c['name'], prof)), (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)])

def get_char_ov(c):
    gen = get_general(c)
    attr = encode_sproto([(0, c.get('level', 1)), (1, 5000)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c['name'], c.get('prof', 0))), (4, c.get('createtime', int(time.time()))), (5, 0)])

def sync_mission_world_objects(char, send_push):
    active, mstate = char.get('active_missions', {}), char.get('mission_state', {})
    for mid, mdata in active.items():
        logic = mission_logic_db.get(mid)
        if not logic: continue
        lid, ltype, state = logic['logicId'], logic['logicType'], mstate.get(mid, {})
        if ltype in [23, 1]:
            target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
            if target:
                for sid in state.get('alive_sids', []):
                    if sid in state.get('dead_sids', []): continue
                    npc_hps[sid] = 1000
                    send_push(509, encode_sproto([(0, encode_sproto([(0, sid), (1, str(target['npcId'])), (2, 1000), (3, 1000), (4, 100), (15, world_to_protocol(target.get('x', 0))), (16, world_to_protocol(target.get('z', 0))), (17, 0), (18, 1), (21, "Target")]))]))
        elif ltype == 24 and state.get('progress', 0) == 0:
            target = car_target_db.get(lid)
            if target: 
                sid = 800000 + int(lid)
                send_push(505, encode_sproto([(0, sid), (1, encode_sproto([(0, "Car"), (1, "DJ_Car_01"), (10, 0)])), (2, encode_sproto([(0, "Car"), (1, 0), (2, 1), (3, "11"), (4, 1)])), (3, encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])), (5, get_movement([target['x'], 0, target['z'], 0])), (6, encode_sproto([(6, encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])), (7, encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)]))]))]))

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
    print(f"[+] Client: {addr}"); acc_id, picked_char = None, None
    def send_rpc_push(tag, data):
        pf = SprotoPacker.pack(encode_sproto([(0, tag)]) + data)
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass
    try:
        while True:
            h = recv_exact(conn, 2)
            if not h: break
            data = recv_exact(conn, struct.unpack(">H", h)[0])
            if not data: break
            raw = SprotoPacker.unpack(data)
            dec = SprotoDecoder(raw); pkg = dec.decode()
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            body = SprotoDecoder(raw, dec.consumed).decode()

            if msg == 4: # Login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (12, 1)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 103: # CharList
                with db_lock: chars = all_accounts_chars.get("0", {}).get(acc_id, [])
                # character_list.response Tag 0 is map of objects, encoded as list of objects
                ovs = [get_char_ov(c) for c in chars]
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, ovs)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 104: # CharCreate
                c_data = SprotoDecoder(body.get(0, b"")).decode()
                name, prof = c_data.get(0, b"").decode('utf-8'), get_val_int(c_data, 1, 0)
                cid = int(time.time() * 1000) % 1000000000
                nc = {'id': cid, 'name': name, 'prof': prof, 'level': 1, 'hp': 3000, 'exp': 0, 'createtime': int(time.time()), 'skills': get_default_skills(prof), 'active_missions': {"1001": [1, 0, [0]*8]}, 'mission_state': {"1001": init_mission_state("1001")}, 'backpack': [], 'equip': {}, 'mapId': "11", 'pos': [298.60, 1.00, -170.05, 0]}
                with db_lock: all_accounts_chars.setdefault("0", {}).setdefault(acc_id, []).append(nc); save_chars(all_accounts_chars)
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, get_char_ov(nc)), (1, 0)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 105: # CharPick
                char_id = get_val_int(body, 0)
                with db_lock: picked_char = next((c for c in all_accounts_chars.get("0", {}).get(acc_id, []) if c['id'] == char_id), None)
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, 0 if picked_char else 1)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if picked_char:
                    online_clients[char_id] = (conn, picked_char['mapId'], 1, picked_char)
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (9, {}), (13, 1), (14, int(time.time()))]))
                    m_map = {mid: encode_sproto([(0, mid), (1, md[0]), (2, md[1]), (3, md[2])]) for mid, md in picked_char.get('active_missions', {}).items()}
                    send_rpc_push(519, encode_sproto([(0, m_map), (1, picked_char.get('last_main_mission', ""))]))
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char))]))
                    send_rpc_push(503, encode_sproto([(0, picked_char['mapId']), (1, 1), (2, 1)]))
            elif msg == 100: # MapReady
                if picked_char:
                    items = {int(it['uid']): encode_sproto([(0, int(it['uid'])), (1, str(it['id'])), (2, True), (3, it.get('lv', 1)), (5, it.get('count', 1)), (6, it.get('qual', 1))]) for it in picked_char.get('backpack', [])}
                    send_rpc_push(611, encode_sproto([(0, items)]))
                    send_rpc_push(540, encode_sproto([(0, {sid: encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos'])]) for sid, sd in picked_char.get('skills', {}).items()})]))
                    sync_mission_world_objects(picked_char, send_rpc_push)
                    send_rpc_push(654, encode_sproto([(0, 1)]))
            elif msg == 101: # Move
                if picked_char:
                    p_raw = body.get(0); pd = SprotoDecoder(p_raw).decode()
                    picked_char['pos'] = [protocol_to_world(get_val_int(pd, 0)), protocol_to_world(get_val_int(pd, 1)), protocol_to_world(get_val_int(pd, 2)), protocol_to_world(get_val_int(pd, 3))]
                    pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, p_raw)])); conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 111: # Damage
                if picked_char:
                    dmgs = body.get(0, [])
                    if isinstance(dmgs, bytes):
                        r, dmgs, p = dmgs, [], 0
                        while p < len(r): l = struct.unpack("<I", r[p:p+4])[0]; dmgs.append(SprotoDecoder(r[p+4:p+4+l]).decode()); p += 4 + l
                    for d in dmgs:
                        tid, val = get_val_int(d, 0), get_val_int(d, 1)
                        npc_hps[tid] = max(0, npc_hps.get(tid, 1000) - val)
                        send_rpc_push(510, encode_sproto([(0, tid), (1, encode_sproto([(0, npc_hps[tid])]))]))
                        if npc_hps[tid] == 0:
                            send_rpc_push(506, encode_sproto([(0, tid)]))
                            for mid, ms in picked_char.get('mission_state', {}).items():
                                if tid in ms.get('alive_sids', []) and tid not in ms.get('dead_sids', []):
                                    ms.setdefault('dead_sids', []).append(tid); ms['progress'] += 1
                                    if mid in picked_char['active_missions']:
                                        picked_char['active_missions'][mid][2][0] = ms['progress']
                                        send_rpc_push(524, encode_sproto([(0, mid), (1, 0), (2, ms['progress'])]))
                                        logic = mission_logic_db.get(mid, {}); lid = logic.get('logicId'); ltype = logic.get('logicType')
                                        req = (kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid) or {}).get('require', 1)
                                        if ms['progress'] >= req: picked_char['active_missions'][mid][0] = 2; send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                    save_chars(all_accounts_chars); break
                    pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif msg == 218: # Heartbeat
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])); conn.sendall(struct.pack(">H", len(pf)) + pf)
            elif session is not None:
                pf = SprotoPacker.pack(encode_sproto([(1, session)]) + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: traceback.print_exc()
    finally:
        if picked_char: online_clients.pop(picked_char['id'], None)
        conn.close()

srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM); srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1); srv.bind(("0.0.0.0", PORT)); srv.listen(50)
print(f"Unified Server 9555 Active on {PORT}");
while True:
    try: cl, ad = srv.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
    except: pass
