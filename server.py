# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - STABLE v20 CONSOLIDATED
# GAME SERVER 9555
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters_final.json"
server_session_counter = 8000

# Data Source Truth: assets\Bundle\Data\Data.bundle extraction
def find_data_file(name):
    paths = [
        os.path.join("assets", "Bundle", "TextAsset", name),
        os.path.join("assets", "Bundle", "Data", name),
        os.path.join("assets", "bin", "Data", "Resources", "Data", name),
        os.path.join("assets", "bin", "Data", "TextAsset", name)
    ]
    for p in paths:
        if os.path.exists(p): return p
    return None

mission_logic_db = {}
kill_target_db = {}
car_target_db = {}
mission_require_db = {}
skill_db = {}
equip_db = {}

def load_game_data():
    # Robust Data Loader: Loading 100% of MissionData entries
    mf = find_data_file("MissionData")
    if mf:
        print(f"[DATA] Found MissionData at: {mf}")
        with open(mf, "r", encoding="utf-8-sig") as f:
            for line in f:
                try:
                    if not line.startswith("*"): continue
                    parts = [p.strip() for p in line.split(",")]
                    if len(parts) > 14:
                        mid = parts[1]
                        mission_logic_db[mid] = {
                            "logicType": int(parts[7]) if parts[7].isdigit() else 0,
                            "logicId": parts[9],
                            "target": parts[11],
                            "nextId": parts[14]
                        }
                except: pass
        print(f"[DATA] Loaded {len(mission_logic_db)} missions from MissionData")
        if "1001" in mission_logic_db:
            print(f"[DATA] Loaded 1001: {mission_logic_db['1001']}")
        else:
            print("[DATA WARNING] Mission 1001 NOT FOUND in database!")
    else:
        print("[DATA ERROR] MissionData file NOT FOUND!")

    kf = find_data_file("KillTargetMissionData")
    if kf:
        with open(kf, "r", encoding="utf-8-sig") as f:
            for line in f:
                try:
                    if not line.startswith("*"): continue
                    p = [i.strip() for i in line.split(",")]
                    if len(p) > 8:
                        lid = p[1]
                        kill_target_db[lid] = {
                            "npcId": p[6], 
                            "x": int(p[3]), "z": int(p[4]), 
                            "require": int(p[8]) if p[8].isdigit() else 1
                        }
                except: pass
        print(f"[DATA] Loaded {len(kill_target_db)} kill targets from KillTargetMissionData")

    cf = find_data_file("TargetCarMissionData")
    if cf:
        with open(cf, "r", encoding="utf-8-sig") as f:
            for line in f:
                try:
                    if not line.startswith("*"): continue
                    p = [i.strip() for i in line.split(",")]
                    if len(p) > 6:
                        car_target_db[p[1]] = {
                            "carId": p[6], "x": int(p[3]), "z": int(p[4])
                        }
                except: pass

    rf = find_data_file("MissionRequireData")
    if rf:
        with open(rf, "r", encoding="utf-8-sig") as f:
            for line in f:
                try:
                    if not line.startswith("*"): continue
                    p = [i.strip() for i in line.split(",")]
                    if len(p) > 5:
                        lid = p[1]
                        mission_require_db[lid] = {
                            "npcId": p[4], "require": int(p[5]) if p[5].isdigit() else 1
                        }
                except: pass

    sf = find_data_file("SkillData")
    if sf:
        with open(sf, "r", encoding="utf-8-sig") as f:
            for line in f:
                try:
                    if not line.startswith("*"): continue
                    p = [i.strip() for i in line.split(",")]
                    if len(p) > 1:
                        sid = p[1]; skill_db[sid] = {"id": sid, "name": p[2]}
                except: pass

    ef = find_data_file("EquipData")
    if ef:
        with open(ef, "r", encoding="utf-8-sig") as f:
            for line in f:
                try:
                    if not line.startswith("*"): continue
                    p = [i.strip() for i in line.split(",")]
                    if len(p) > 15:
                        eid = p[1]
                        equip_db[eid] = {
                            "id": eid, "pos": int(p[7]),
                            "stats": [
                                (int(p[8]), int(p[9])),
                                (int(p[10]) if p[10].isdigit() else 0, int(p[11]) if p[11].isdigit() else 0),
                                (int(p[12]) if p[12].isdigit() else 0, int(p[13]) if p[13].isdigit() else 0),
                                (int(p[14]) if p[14].isdigit() else 0, int(p[15]) if p[15].isdigit() else 0)
                            ]
                        }
                except: pass

load_game_data()

def load_chars():
    if os.path.exists(CHAR_DB):
        try:
            with open(CHAR_DB, "r") as f: return json.load(f)
        except: return {}
    return {}

def save_chars(data):
    try:
        with open(CHAR_DB, "w") as f: json.dump(data, f, indent=4)
    except: pass

def get_char_by_id(char_id):
    for area in all_accounts_chars.values():
        for acc in area.values():
            for c in acc:
                if c['id'] == char_id: return c
    return None

def send_mail_sync(char, send_push):
    mails = char.get('mails', [])
    for m in mails:
        # SprotoType.mail_update (531)
        m_proto = encode_sproto([
            (0, m['id']), (1, m.get('senderType', 0)), (3, m['title']),
            (4, m['time']), (5, char['id']), (6, m.get('readTime', 0)),
            (7, m['context']), (8, m['state']), (9, m['time']), (10, {}), (11, 30)
        ])
        send_push(531, m_proto)

def get_friend_info_proto(local_id, target_id, ftype=0):
    c = get_char_by_id(target_id)
    if not c: return None
    # 0:characterId, 1:friendId, 2:name, 3:level, 4:profession, 5:combValue, 6:state...
    return encode_sproto([
        (0, local_id), (1, target_id), (2, c['name']), (3, c.get('level', 1)),
        (4, c.get('prof', 0)), (5, calculate_power(3000, 300, 35, 480, 60)), 
        (6, 1 if target_id in online_clients else 0),
        (7, 0), (8, ftype), (9, 0), (10, ""), (11, 0)
    ])

def send_friend_sync(char, send_push):
    friends = char.get('friends', [])
    foes = char.get('foes', [])
    res = {}
    for fid in friends:
        p = get_friend_info_proto(char['id'], fid, 0)
        if p: res[fid] = p
    for fid in foes:
        p = get_friend_info_proto(char['id'], fid, 6)
        if p: res[fid] = p
    send_push(538, encode_sproto([(0, res)]))

def send_item_sync(char, send_push):
    items = char.get('backpack', [])
    res = {}
    for i, it in enumerate(items):
        res[int(it['uid'])] = encode_sproto([
            (0, int(it['uid'])), (1, str(it['id'])), (2, True),
            (3, it.get('lv', 1)), (5, it.get('count', 1)), (6, it.get('qual', 1))
        ])
    send_push(611, encode_sproto([(0, res)]))

def gain_item(char, item_id, count=1):
    uid = int(time.time() * 1000) % 10000000 + random.randint(1, 999)
    char.setdefault('backpack', []).append({'uid': uid, 'id': str(item_id), 'count': count, 'lv': 1, 'qual': 1})
    save_chars(all_accounts_chars)
    if char['id'] in online_clients:
        conn, _, _, _ = online_clients[char['id']]
        def push_wrapper(tag, data):
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            try: conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            except: pass
        send_item_sync(char, push_wrapper)

all_accounts_chars = load_chars()
online_clients = {} 
npc_hps = {}

VERIFICATION_MODE = True # [TEST ONLY]

def calculate_power(hp, atk, def_val, hit, dge):
    power = (hp * 0.1) + (atk * 2.5) + (def_val * 5) + (hit * 1.5) + (dge * 1.5)
    return int(power)

def get_default_skills(prof):
    sid = "101" if prof == 0 else "201" if prof == 1 else "301"
    did = "104" if prof == 0 else "204" if prof == 1 else "304"
    return {
        sid: {"id": sid, "lv": 1, "pos": 0, "unlock": 1, "pos2": 0, "dis": False},
        did: {"id": did, "lv": 1, "pos": 3, "unlock": 1, "pos2": 1, "dis": False}
    }

def get_skill_sync(c):
    skills_data = c.get('skills', get_default_skills(c.get('prof', 0)))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])
    return encode_sproto([(0, skills_map), (1, False)])

def init_mission_state(char_id, mid):
    logic = mission_logic_db.get(mid)
    if not logic: return {"alive_sids": [], "dead_sids": [], "progress": 0}
    lid, ltype = logic['logicId'], logic['logicType']
    alive_sids = []
    if ltype in [23, 1]:
        target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
        count = target.get('require', 1) if target else 1
        # Range: 9,000,000 + (char_id % 10,000) * 100 + i
        base_sid = 9000000 + (int(char_id) % 10000) * 100 + int(lid)
        for i in range(count): alive_sids.append(base_sid + i)
    return {"alive_sids": alive_sids, "dead_sids": [], "progress": 0}

def get_mission_sync(c):
    active = c.get('active_missions', {"1001": [1, 0, [0]*8]})
    missions_map = {}
    for mid, mdata in active.items():
        state, qual, parms = mdata
        missions_map[mid] = encode_sproto([(0, mid), (1, state), (2, qual), (3, parms)])
    return encode_sproto([(0, missions_map), (1, c.get('last_main_mission', "")), (2, [])])

def generate_unique_char_id():
    return int(time.time() * 1000) % 1000000000

def get_area_id(serverId):
    try:
        sid = int(serverId)
        if sid == 1 or (300 <= sid < 400): return 1
        if sid == 2 or (600 <= sid < 700): return 2
    except: pass
    return 0

def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, int): return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4: return struct.unpack("<i", val)[0]
        if len(val) == 8: return struct.unpack("<q", val)[0]
    return default

def encode_sproto(fields, fn=None):
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
                if val and isinstance(val[0], int): v = b"\x04" + b"".join([struct.pack("<i", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)): pass
                        else: item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for item in val.values():
                    if isinstance(item, (bytes, bytearray)): items.append(struct.pack("<I", len(item)) + item)
                    else: items.append(struct.pack("<I", 1) + (b'\x01' if item else b'\x00'))
                v = b"".join(items)
            else: v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag
    fn_val = fn if fn is not None else len(header)
    res = struct.pack("<H", fn_val)
    for h in header: res += struct.pack("<H", h)
    return res + body

def sproto_pack(data):
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i+8]
        if len(chunk) < 8: chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0: mask |= (1 << j)
        if mask == 0xFF: out.append(0xFF); out.append(0); out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j): out.append(chunk[j])
    return bytes(out)

def sproto_unpack(data):
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

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    fn = struct.unpack("<H", data[offset:offset+2])[0]
    h_ptr, b_ptr = offset + 2, offset + 2 + fn*2
    fields, curr_tag = {}, -1
    for i in range(fn):
        v = struct.unpack("<H", data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
        if v == 0:
            curr_tag += 1
            if b_ptr + 4 <= len(data):
                l = struct.unpack("<I", data[b_ptr:b_ptr+4])[0]
                fields[curr_tag] = data[b_ptr+4:b_ptr+4+l]
                b_ptr += 4 + l
        elif v == 1: curr_tag += 1
        elif v & 1: curr_tag += (v >> 1) + 1
        else: curr_tag += 1; fields[curr_tag] = (v >> 1) - 1
    return fields

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

def get_general(c):
    return encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, "11"), (4, 1)])

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c):
    gen = get_general(c)
    attr = encode_sproto([(0, 1), (1, calculate_power(3000, 300, 35, 480, 60))])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))), (4, int(time.time())), (5, 0)])

def get_full_char(c):
    gen = get_general(c); prof = c.get('prof', 0); lv = c.get('level', 1)
    hp, atk, def_val, hit, dge = 3000, 300, 35, 480, 60
    equips = c.get('equip', {})
    for slot, item in equips.items():
        edata = equip_db.get(item['id'])
        if edata:
            for stype, sval in edata['stats']:
                if stype == 1001: atk += sval
                elif stype == 1002: hp += sval
                elif stype == 1003: def_val += sval
                elif stype == 1004: hit += sval
                elif stype == 1005: dge += sval
    cv = calculate_power(hp, atk, def_val, hit, dge)
    attr_oth = encode_sproto([(0, hp), (1, c.get('exp', 0)), (2, lv), (3, cv), (15, 1)])
    prop = encode_sproto([(13, 1000), (14, 100), (15, 10), (16, 0), (17, 0), (18, 0)])
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])
    attr_run = encode_sproto([(0, hp), (2, atk), (3, def_val), (4, hit), (5, dge)])
    run = encode_sproto([(6, attr_run), (7, attr_run)])
    skills_data = c.get('skills', get_default_skills(prof))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])
    equip_map = {}
    for slot, item in equips.items():
        equip_map[int(slot)] = encode_sproto([(0, int(item['uid'])), (1, str(item['id'])), (2, True), (3, item.get('lv', 1)), (5, 1), (6, item.get('qual', 1)), (7, [0]*8)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr_oth), (5, prop), (6, get_visual(c.get('name', 'Hero'), prof)), (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)])

def send_attr_sync(char, send_push):
    hp, atk, def_val, hit, dge = 3000, 300, 35, 480, 60
    for slot, item in char.get('equip', {}).items():
        edata = equip_db.get(item['id'])
        if edata:
            for stype, sval in edata['stats']:
                if stype == 1001: atk += sval
                elif stype == 1002: hp += sval
                elif stype == 1003: def_val += sval
                elif stype == 1004: hit += sval
                elif stype == 1005: dge += sval
    cv = calculate_power(hp, atk, def_val, hit, dge)
    attr_oth = encode_sproto([(0, hp), (1, char.get('exp', 0)), (2, char.get('level', 1)), (3, cv)])
    send_push(510, encode_sproto([(0, char['id']), (1, attr_oth)]))
    print(f"[EQUIP DEBUG] {char['name']} New Stats Sum: ATK: {atk} | POWER: {cv}")

def get_aoi_car(car_id, server_id, x, z, name="Car"):
    vis = encode_sproto([(0, name), (1, "DJ_Car_01"), (10, 0)])
    gen = encode_sproto([(0, name), (1, 0), (2, 1), (3, "11"), (4, 1)])
    stats = encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])
    pos_data = encode_sproto([(0, x), (1, 0), (2, z), (3, 0)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])
    attr = encode_sproto([(6, stats), (7, stats)])
    return encode_sproto([(0, server_id), (1, vis), (2, gen), (3, stats), (5, mv), (6, attr)])

def get_mission_npc_proto(npc_id, server_id, x, z, name="Mission Target"):
    # SprotoType.npc_attribute (Tag 509 npc_create)
    # 0:id, 1:npcdataid, 2:hp, 3:max_hp, 4:atk, 5:def, 6:hit, 7:eva, 8:cri, 9:exd, 10:exr, 11:res
    # 12:crd, 13:crr, 14:defa, 15:x, 16:z, 17:o, 18:level, 19:anti_stun, 20:anti_knock_down, 21:player_name
    return encode_sproto([
        (0, server_id), (1, str(npc_id)), (2, 1000), (3, 1000), (4, 100), (5, 50),
        (6, 100), (7, 50), (8, 50), (9, 0), (10, 0), (11, 0), (12, 0), (13, 0),
        (14, 0), (15, x), (16, z), (17, 0), (18, 1), (19, 0), (20, 0), (21, name)
    ])

def sync_mission_world_objects(char, send_push_func):
    active = char.get('active_missions', {})
    mstate = char.get('mission_state', {})
    
    print(f"[MISSION DEBUG] Syncing world objects for {char['name']}. Active: {list(active.keys())}")
    
    for mid, mdata in active.items():
        # ONLY spawn targets if mission is Accepted (1), not Completable (2) or Finished (deleted)
        if mdata[0] != 1: 
            print(f"[MISSION DEBUG] Skipping spawn for {mid} - State is {mdata[0]}")
            continue
            
        logic = mission_logic_db.get(mid)
        if not logic: 
            print(f"[MISSION DEBUG] No logic found for {mid}")
            continue
            
        lid, ltype = logic['logicId'], logic['logicType']
        state = mstate.get(mid, {})
        alive_sids, dead_sids = state.get('alive_sids', []), state.get('dead_sids', [])
        
        if ltype in [23, 1]: # Kill Missions
            target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
            if target:
                npc_id = target['npcId']
                for sid in alive_sids:
                    if sid in dead_sids:
                        print(f"[MISSION DEBUG] {mid} SID {sid} is already dead. Skipping.")
                        continue
                    # Initialize/Reset HP
                    npc_hps[sid] = 1000
                    x, z = target.get('x', 29860) + random.randint(-100, 100), target.get('z', -17005) + random.randint(-100, 100)
                    print(f"[MISSION SPAWN] {mid} SID: {sid} NPC: {npc_id} AT: {x}, {z}")
                    # Tag 509: npc_create
                    send_push_func(509, encode_sproto([(0, get_mission_npc_proto(npc_id, sid, x, z))]))
            else:
                print(f"[MISSION DEBUG] {mid} Logic {lid} Target data missing.")
        elif ltype == 24 and state.get('progress', 0) == 0: # Rob Car
            target = car_target_db.get(lid)
            if target:
                sid = 8000000 + (int(char['id']) % 10000) * 100 + int(lid)
                print(f"[MISSION SPAWN] {mid} SID: {sid} Car: {target['carId']}")
                send_push_func(505, get_aoi_car(target['carId'], sid, target['x'], target['z'], "Car"))

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0; cur_map_id = "11"
    def send_rpc_push(tag, data, target_conn=None):
        try:
            target = target_conn if target_conn else conn
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            target.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        except: pass
    try:
        while True:
            h_bytes = conn.recv(2)
            if not h_bytes: break
            size = struct.unpack(">H", h_bytes)[0]
            data = b""
            while len(data) < size:
                chunk = conn.recv(size - len(data))
                if not chunk: break
                data += chunk
            if len(data) < size: break
            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1); cur_areaId = get_area_id(sid)
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (3, 1)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                chars = all_accounts_chars.get(cur_areaId, {}).get(acc_id, [])
                resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8') if isinstance(c_data.get(0), bytes) else str(c_data.get(0, "Hero"))
                prof = get_val_int(c_data, 1, 0); cid = generate_unique_char_id()
                if cur_areaId not in all_accounts_chars: all_accounts_chars[cur_areaId] = {}
                if acc_id not in all_accounts_chars[cur_areaId]: all_accounts_chars[cur_areaId][acc_id] = []
                nc = {'id': cid, 'name': name, 'prof': prof, 'hp': 3000, 'level': 1, 'exp': 0, 'skills': get_default_skills(prof), 'active_missions': {"1001": [1, 0, [0]*8]}, 'mission_state': {"1001": init_mission_state(cid, "1001")}, 'last_main_mission': "", 'mails': [], 'friends': [], 'foes': [], 'backpack': [], 'equip': {}, 'mapId': "11", 'pos': [29860, 100, -17005, 0]}
                if VERIFICATION_MODE:
                    wid = "10001" if prof == 0 else "20001" if prof == 1 else "30001"
                    gain_item(nc, wid); gain_item(nc, "10002"); gain_item(nc, "10003")
                all_accounts_chars[cur_areaId][acc_id].append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(cur_areaId, {}).get(acc_id, []) if c['id'] == char_id), None)
                resp = encode_sproto([(0, 1 if picked_char else 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)
                if picked_char:
                    for f in ['level', 'exp', 'mails', 'friends', 'foes', 'backpack', 'equip', 'mapId', 'pos', 'active_missions']:
                        if f not in picked_char: picked_char[f] = 1 if f=='level' else 0 if f=='exp' else "11" if f=='mapId' else [29860,100,-17005,0] if f=='pos' else {"1001":[1,0,[0]*8]} if f=='active_missions' else [] if f in ['mails','friends','foes','backpack'] else {}
                    if 'mission_state' not in picked_char:
                        picked_char['mission_state'] = {mid: init_mission_state(char_id, mid) for mid in picked_char['active_missions']}
                    else:
                        # MIGRATION: If SIDs are in old 900,000 range, reset them to the new 9,000,000 unique range
                        for mid, ms in picked_char['mission_state'].items():
                            if ms.get('alive_sids') and ms['alive_sids'][0] < 9000000:
                                print(f"[MIGRATION] Resetting mission state for {mid} to unique SIDs.")
                                fresh = init_mission_state(char_id, mid)
                                ms['alive_sids'] = fresh['alive_sids']
                                ms['dead_sids'] = []
                                ms['progress'] = 0
                                if mid in picked_char['active_missions']:
                                    picked_char['active_missions'][mid][0] = 1 # Back to Accepted
                                    picked_char['active_missions'][mid][2][0] = 0
                    for mid, mdata in picked_char['active_missions'].items():
                        logic = mission_logic_db.get(mid)
                        if logic and logic['logicType'] == 7 and picked_char.get('level', 1) >= int(logic['logicId']): mdata[0] = 2
                    save_chars(all_accounts_chars); cur_map_id = picked_char.get('mapId', "11"); line_idx = 1; online_clients[char_id] = (conn, cur_map_id, line_idx, picked_char)
                    fids = ["4083","3031","4084","100","3016","4063","4061","4062","4064","3001","4081","111","112","110","3024","3025","3026","3027","3028","3029","107","108","109","3030","3014"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    print("[MAP FLOW] Sending 614 sequence"); send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))
                    send_rpc_push(519, get_mission_sync(picked_char))
                    send_rpc_push(503, encode_sproto([(0, cur_map_id), (1, line_idx), (2, 1)]))
                    lp = picked_char.get('pos', [29860, 100, -17005, 0]); send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(lp[0], lp[1], lp[2], lp[3]))]))
                    send_friend_sync(picked_char, send_rpc_push); send_mail_sync(picked_char, send_rpc_push)

            elif msg == 100: # map_ready
                if picked_char:
                    send_item_sync(picked_char, send_rpc_push); send_rpc_push(540, get_skill_sync(picked_char))
                    sync_mission_world_objects(picked_char, send_rpc_push)
                    for t in [684, 555, 619]: send_rpc_push(t, encode_sproto([] if t==684 else [(0, {})]))
                    send_rpc_push(654, encode_sproto([(0, 1)]))

            elif msg == 101: # move
                if session is not None and picked_char:
                    p_raw = body.get(0)
                    if p_raw:
                        pd = decode_sproto(p_raw)
                        x, y, z, o = get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)
                        picked_char['pos'] = [x, y, z, o]
                        picked_char['mapId'] = cur_map_id
                        # Optimization: Removed save_chars() to fix joystick lag
                        print(f"[MOVE RECEIVED] {picked_char['name']} x={x} y={y} z={z} o={o}")
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 123: # mail_operation
                if session is not None and picked_char:
                    mid, op = get_val_int(body, 0), get_val_int(body, 1)
                    if op == 0: # READ
                        for m in picked_char['mails']:
                            if m['id'] == mid: m['state'] = 1; m['readTime'] = int(time.time())
                    elif op == 1: # DELETE SINGLE
                        picked_char['mails'] = [m for m in picked_char['mails'] if m['id'] != mid]
                    elif op == 2: # GET SINGLE ITEM
                        for m in picked_char['mails']:
                            if m['id'] == mid and m['state'] == 2: m['state'] = 3
                    elif op == 3: # GET ALL ITEMS
                        for m in picked_char['mails']:
                            if m['state'] == 2: m['state'] = 3
                    elif op == 4: # DELETE ALL
                        picked_char['mails'] = [m for m in picked_char['mails'] if m['state'] == 0 or m['state'] == 2]
                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 111: # accept_damge
                if session is not None and picked_char:
                    dmgs = body.get(0, [])
                    if isinstance(dmgs, bytes):
                        r, dmgs, p = dmgs, [], 0
                        while p < len(r): l = struct.unpack("<I", r[p:p+4])[0]; dmgs.append(decode_sproto(r[p+4:p+4+l])); p += 4 + l
                    
                    for d in dmgs:
                        tid, val = get_val_int(d, 0), get_val_int(d, 1)
                        cur_h = npc_hps.get(tid, 1000)
                        new_hp = max(0, cur_h - val)
                        npc_hps[tid] = new_hp
                        
                        # print(f"[COMBAT] Target {tid} hit for {val}. HP: {cur_h} -> {new_hp}")
                        send_rpc_push(510, encode_sproto([(0, tid), (1, encode_sproto([(0, new_hp)]))]))
                        
                        if new_hp == 0:
                            print(f"[COMBAT] Target {tid} died.")
                            send_rpc_push(506, encode_sproto([(0, tid)])) # Remove from AOI
                            
                            # Check all active missions for this character
                            found_mission = False
                            for mid, ms in picked_char.get('mission_state', {}).items():
                                if tid in ms.get('alive_sids', []):
                                    found_mission = True
                                    if tid not in ms.get('dead_sids', []):
                                        ms.setdefault('dead_sids', []).append(tid)
                                        ms['progress'] += 1
                                        print(f"[MISSION PROGRESS] {picked_char['name']} {mid}: {ms['progress']} kills")
                                        
                                        if mid in picked_char['active_missions']:
                                            picked_char['active_missions'][mid][2][0] = ms['progress']
                                            # Push update to HUD (Tag 524) - using 1-based index
                                            send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, ms['progress'])]))
                                            
                                            logic = mission_logic_db.get(mid, {})
                                            lid = logic.get('logicId')
                                            req_d = kill_target_db.get(lid) if logic.get('logicType') == 23 else mission_require_db.get(lid)
                                            req = req_d.get('require', 1) if req_d else 1
                                            
                                            if ms['progress'] >= req:
                                                print(f"[MISSION COMPLETE] {mid} is now ready to submit.")
                                                picked_char['active_missions'][mid][0] = 2
                                                send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                        break
                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 113: # complete_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    if mid in picked_char.get('active_missions', {}):
                        del picked_char['active_missions'][mid]
                        if 'mission_state' in picked_char: picked_char['mission_state'].pop(mid, None)
                        picked_char['last_main_mission'] = mid; logic = mission_logic_db.get(mid)
                        if logic and logic['nextId'] and logic['nextId'] != "#N/A":
                            nm = logic['nextId']; picked_char['active_missions'][nm] = [1, 0, [0]*8]
                            picked_char['mission_state'][nm] = init_mission_state(picked_char['id'], nm)
                            nl = mission_logic_db.get(nm)
                            if nl:
                                if nl['logicType'] == 7 and picked_char.get('level', 1) >= int(nl['logicId']): picked_char['active_missions'][nm][0] = 2
                                elif nl['logicType'] == 2: picked_char['active_missions'][nm][0] = 2
                            print(f"[STORY PROGRESS] {mid} -> {nm}")
                        save_chars(all_accounts_chars); send_rpc_push(519, get_mission_sync(picked_char)); sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 112: # accept_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8'); picked_char['active_missions'][mid] = [1, 0, [0]*8]
                    picked_char['mission_state'][mid] = init_mission_state(picked_char['id'], mid); logic = mission_logic_db.get(mid)
                    print(f"[MISSION START] {picked_char['name']} accepted {mid}. Targets: {picked_char['mission_state'][mid].get('alive_sids')}")
                    if logic:
                        if logic['logicType'] == 2: 
                            print(f"[MISSION] Auto-completing Talk mission {mid}")
                            picked_char['active_missions'][mid][0] = 2
                        elif logic['logicType'] == 7 and picked_char.get('level', 1) >= int(logic['logicId']): 
                            print(f"[MISSION] Auto-completing Level mission {mid}")
                            picked_char['active_missions'][mid][0] = 2
                    save_chars(all_accounts_chars); send_rpc_push(519, get_mission_sync(picked_char)); sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 120: # chat
                if session is not None and picked_char:
                    c_info = body.get(2, b"").decode('utf-8') if isinstance(body.get(2), bytes) else str(body.get(2))
                    c_type = get_val_int(body, 3); tid = get_val_int(body, 0)
                    item = encode_sproto([(0, picked_char['id']), (1, picked_char['name']), (2, tid), (3, ""), (4, c_info), (5, c_type), (6, 0), (7, []), (8, []), (9, picked_char.get('prof', 0)), (10, picked_char.get('level', 1)), (11, 5000), (12, 0), (13, ""), (14, "")])
                    pd = encode_sproto([(0, [item])])
                    if c_type == 2:
                        for cid, (cl, _, _, _) in online_clients.items(): send_rpc_push(528, pd, cl)
                    elif c_type == 1:
                        for cid, (cl, mid, _, _) in online_clients.items():
                            if mid == cur_map_id: send_rpc_push(528, pd, cl)
                    else: send_rpc_push(528, pd)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 124: # add_friend
                if session is not None and picked_char:
                    fid = get_val_int(body, 0); ftype = get_val_int(body, 1)
                    if get_char_by_id(fid):
                        if ftype == 0:
                            if fid not in picked_char['friends']:
                                picked_char['friends'].append(fid)
                                if fid in picked_char.get('foes', []): picked_char['foes'].remove(fid)
                            send_rpc_push(533, encode_sproto([(0, get_friend_info_proto(picked_char['id'], fid, 0))]))
                            if fid in online_clients:
                                r_c = online_clients[fid][0]; p_r = get_friend_info_proto(fid, picked_char['id'], 2)
                                try:
                                    ph_r = encode_sproto([(0, 533)]); pf_r = sproto_pack(ph_r + p_r)
                                    r_c.sendall(struct.pack(">H", len(pf_r)) + pf_r)
                                except: pass
                        else:
                            if fid not in picked_char.setdefault('foes', []):
                                picked_char['foes'].append(fid)
                                if fid in picked_char.get('friends', []): picked_char['friends'].remove(fid)
                            send_rpc_push(533, encode_sproto([(0, get_friend_info_proto(picked_char['id'], fid, 6))]))
                        save_chars(all_accounts_chars); send_friend_sync(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 125: # del_friend
                if session is not None and picked_char:
                    fid, ftype = get_val_int(body, 0), get_val_int(body, 1)
                    target = picked_char['friends'] if ftype == 0 else picked_char.get('foes', [])
                    if fid in target: target.remove(fid)
                    save_chars(all_accounts_chars); send_friend_sync(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 116: # equip_item
                if session is not None and picked_char:
                    uid = get_val_int(body, 0); item = next((it for it in picked_char['backpack'] if it['uid'] == uid), None)
                    if item:
                        ed = equip_db.get(item['id'])
                        if ed:
                            pos = str(ed['pos'])
                            if pos in picked_char['equip']: picked_char['backpack'].append(picked_char['equip'][pos])
                            picked_char['equip'][pos] = item; picked_char['backpack'] = [it for it in picked_char['backpack'] if it['uid'] != uid]
                            save_chars(all_accounts_chars); send_item_sync(picked_char, send_rpc_push); send_attr_sync(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 117: # unequip_item
                if session is not None and picked_char:
                    uid = get_val_int(body, 0); target_pos = next((p for p, it in picked_char['equip'].items() if it['uid'] == uid), None)
                    if target_pos:
                        item = picked_char['equip'].pop(target_pos); picked_char['backpack'].append(item); save_chars(all_accounts_chars)
                        send_item_sync(picked_char, send_rpc_push); send_attr_sync(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 126: # request_update_friend_useinfo
                ftype, res = get_val_int(body, 1), {}
                for fid in (picked_char.get('friends', []) if ftype == 0 else picked_char.get('foes', [])):
                    p = get_friend_info_proto(picked_char['id'], fid, 6 if ftype == 1 else 0)
                    if p: res[fid] = p
                resp = encode_sproto([(0, res), (1, ftype)]); ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 143: # ask_shop_list
                resp = encode_sproto([(0, body.get(0, 0)), (1, 1), (2, 1), (3, [])]); ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 242: # request_slot_info
                si = encode_sproto([(0, 0), (1, 5), (2, 0)]); resp = encode_sproto([(0, si), (1, {}), (2, {})]); ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 145: # ask_copyscenes_info
                resp = encode_sproto([(0, {})]); ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 225: # request_activity_info
                resp = encode_sproto([(0, {})]); ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 156: # title_req_level_up
                tl, te = picked_char.get('title_level', 0), picked_char.get('title_exp', 0); resp = encode_sproto([(0, tl), (1, te)])
                send_rpc_push(569, resp); ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 118: # request_random_name
                resp = encode_sproto([(0, f"U_{random.randint(10,99)}")])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 218: # heart_beat
                resp = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 129: # sell_item
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [191, 263, 254, 255, 299, 296, 258, 261, 260, 259, 318, 235, 253, 274, 252, 133, 202, 313, 319, 310]:
                tags = {191:598, 263:645, 254:642, 255:643, 299:678, 296:674, 258:646, 261:649, 260:648, 259:647, 318:688, 235:630, 253:641, 274:656, 252:640, 133:542, 202:606, 313:623, 319:689, 310:684}
                rt = tags.get(msg)
                # Map payload to valid (tag, value) pairs
                payloads = {
                    598: [(0, [])],
                    645: [(0, {})], 674: [(0, {})], 646: [(0, {})], 648: [(0, {})], 688: [(0, {})],
                    630: [(0, {})], 656: [(0, {})], 542: [(0, {})], 689: [(0, {})], 684: [(0, {})],
                    642: [(0, 1), (2, 1)], 643: [(0, 1)], 678: [(0, 0), (1, 0)],
                    649: [(0, {}), (1, {}), (2, 0)],
                    647: [(0, encode_sproto([(0, 0)]))],
                    641: [(0, encode_sproto([(0, 1), (1, 0)]))],
                    640: [(0, encode_sproto([(0, 1), (1, 0)]))],
                    606: [(0, encode_sproto([(0, 1), (1, 1), (2, 0)]))],
                    623: [(0, encode_sproto([(0, 0)]))]
                }
                res = encode_sproto(payloads.get(rt, []))
                if rt: send_rpc_push(rt, res)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([])); conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: traceback.print_exc()
    finally:
        if picked_char: cid = picked_char['id']; online_clients.pop(cid, None)
        conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20); print(f"GAME SERVER 9555 CONSOLIDATED")
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
