# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - STABLE v17 LOADING FIX
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
    mf = find_data_file("MissionData")
    if mf:
        with open(mf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                parts = line.split(",")
                if len(parts) > 14:
                    mid = parts[1].strip()
                    mclass = int(parts[6]) if parts[6].isdigit() else 0
                    if mclass == 1:
                        mission_logic_db[mid] = {
                            "logicType": int(parts[7]) if parts[7].isdigit() else 0,
                            "logicId": parts[9].strip(),
                            "target": parts[11].strip(),
                            "nextId": parts[14].strip()
                        }
    kf = find_data_file("KillTargetMissionData")
    if kf:
        with open(kf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 8:
                    lid = p[1].strip()
                    kill_target_db[lid] = {
                        "npcId": p[6].strip(), 
                        "x": int(p[3]), 
                        "z": int(p[4]), 
                        "require": int(p[8]) if p[8].isdigit() else 1
                    }
    cf = find_data_file("TargetCarMissionData")
    if cf:
        with open(cf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 6:
                    car_target_db[p[1].strip()] = {
                        "carId": p[6].strip(), 
                        "x": int(p[3]), 
                        "z": int(p[4])
                    }
    rf = find_data_file("MissionRequireData")
    if rf:
        with open(rf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 5:
                    lid = p[1].strip()
                    mission_require_db[lid] = {
                        "npcId": p[4].strip(), 
                        "require": int(p[5]) if p[5].isdigit() else 1
                    }
    sf = find_data_file("SkillData")
    if sf:
        with open(sf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 1:
                    sid = p[1].strip()
                    skill_db[sid] = {"id": sid, "name": p[2].strip()}
    ef = find_data_file("EquipData")
    if ef:
        with open(ef, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 15:
                    eid = p[1].strip()
                    # Positions: 0:Weapon, 1:Head, 2:Body, 3:Leg, 4:Belt, 5:Necklace
                    # Stats: 1001:ATK, 1002:HP, 1003:DEF, 1004:HIT, 1005:DGE
                    try:
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
        print("--- SKILL DATABASE ---")
        for sid in sorted(skill_db.keys(), key=lambda x: int(x) if x.isdigit() else 999999):
            print(f"ID: {sid} Name: {skill_db[sid]['name']}")
        print("----------------------")

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

def send_mail_sync(char, conn):
    mails = char.get('mails', [])
    for m in mails:
        # State: 0:unread, 1:read, 2:uncollected, 3:collected
        m_proto = encode_sproto([
            (0, m['id']), (1, m.get('senderType', 0)), (3, m['title']),
            (4, m['time']), (5, char['id']), (6, m.get('readTime', 0)),
            (7, m['context']), (8, m['state']), (9, m['time']), (11, 30)
        ])
        # ph_p = encode_sproto([(0, 531)]); pf_p = sproto_pack(ph_p + m_proto)
        # conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        # Using send_rpc_push directly is easier if I define it outside or pass it
        pass

def send_friend_sync(char, conn, send_push):
    friends = char.get('friends', [])
    foes = char.get('foes', [])
    res = {}
    for fid in friends + foes:
        c = get_char_by_id(fid)
        if c:
            ftype = 6 if fid in foes else 0 # 6: ENEMY, 0: NORMAL
            res[fid] = encode_sproto([
                (0, char['id']), (1, fid), (2, c['name']), (3, c.get('level', 1)),
                (4, c.get('prof', 0)), (5, 5000), (6, 1 if fid in online_clients else 0),
                (8, ftype)
            ])
    send_push(538, encode_sproto([(0, res)]))

def send_item_sync(char, send_push):
    items = char.get('backpack', [])
    res = {}
    for i, it in enumerate(items):
        # SprotoType.gameitem: indexId(0), itemId(1), bind(2), lv(3), flags(4), stack(5), qual(6), parm(7)
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
            conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        send_item_sync(char, push_wrapper)

all_accounts_chars = load_chars()
online_clients = {} 
npc_hps = {}

VERIFICATION_MODE = True # [TEST ONLY] Set to False to disable starter kits

def calculate_power(hp, atk, def_val, hit, dge):
    # Dynamic Power formula based on weighted attributes
    # Formula: (HP * 0.1) + (ATK * 2.5) + (DEF * 5) + (HIT * 1.5) + (DGE * 1.5)
    power = (hp * 0.1) + (atk * 2.5) + (def_val * 5) + (hit * 1.5) + (dge * 1.5)
    return int(power)

def get_default_skills(prof):
    # Restoring original Level 1 starter skills
    sid = "101" if prof == 0 else "201" if prof == 1 else "301"
    did = "104" if prof == 0 else "204" if prof == 1 else "304"
    return {
        sid: {"id": sid, "lv": 1, "pos": 0, "unlock": 1, "pos2": 0, "dis": False},
        did: {"id": did, "lv": 1, "pos": 3, "unlock": 1, "pos2": 1, "dis": False}
    }

def get_skill_sync(c):
    # Level 1 sync: exactly 2 starter skills
    skills_data = c.get('skills', get_default_skills(c.get('prof', 0)))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])
    return encode_sproto([(0, skills_map), (1, False)])

def init_mission_state(mid):
    logic = mission_logic_db.get(mid)
    if not logic: return {"alive_sids": [], "dead_sids": [], "progress": 0}
    lid = logic['logicId']
    ltype = logic['logicType']
    alive_sids = []
    if ltype in [23, 1]:
        target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
        count = target.get('require', 1) if target else 1
        base_sid = 900000 + int(lid) if ltype == 23 else 700000 + int(lid)
        for i in range(count):
            alive_sids.append(base_sid * 10 + i)
    return {"alive_sids": alive_sids, "dead_sids": [], "progress": 0}

def generate_unique_char_id():
    return int(time.time() * 1000) % 1000000000

def get_area_id(serverId):
    try:
        sid = int(serverId)
        if sid == 1 or (300 <= sid < 400): return 1
        if sid == 2 or (600 <= sid < 700): return 2
        if sid == 3 or (10 <= sid < 100): return 0
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
        if val is None:
            header.append(1)
        elif isinstance(val, bool):
            header.append((1 if val else 0) * 2 + 2)
        elif isinstance(val, int):
            if 0 <= val <= 32766:
                header.append((val + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= val <= 2147483647:
                    body += struct.pack("<I", 4) + struct.pack("<i", val)
                else:
                    body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray, list, dict)):
            header.append(0)
            if isinstance(val, str):
                v = val.encode('utf-8')
            elif isinstance(val, list):
                if val and isinstance(val[0], int):
                    v = b"\x04" + b"".join([struct.pack("<i", item) for item in val])
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
                    if isinstance(item, str): item = item.encode('utf-8')
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else:
                        items.append(struct.pack("<I", 1) + (b'\x01' if item else b'\x00'))
                v = b"".join(items)
            else:
                v = val
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
        if mask == 0xFF:
            out.append(0xFF); out.append(0); out.extend(chunk)
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
        elif v == 1:
            curr_tag += 1
        elif v & 1:
            curr_tag += (v >> 1) + 1
        else:
            curr_tag += 1
            fields[curr_tag] = (v >> 1) - 1
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
    attr = encode_sproto([(0, 1), (1, 5000)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))), (4, int(time.time())), (5, 0)])

VERIFICATION_MODE = True # [TEST ONLY] Set to False to disable starter kits

def calculate_power(hp, atk, def_val, hit, dge):
    # Isolated Power formula based on client Combat Value logic
    # Weights derived from CharacterAttributeData.cs and quality expectations
    power = (hp * 0.1) + (atk * 2.5) + (def_val * 5) + (hit * 1.5) + (dge * 1.5)
    return int(power)

def get_full_char(c):
    gen = get_general(c)
    prof = c.get('prof', 0)
    lv = c.get('level', 1)
    
    # Baseline stats
    hp = 3000; atk = 300; def_val = 35; hit = 480; dge = 60
    
    # Sum all equipment bonuses
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
    attr_all = encode_sproto([(0, hp), (2, atk), (3, def_val), (4, hit), (5, dge), (13, 500)])
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    # Level 1: exactly 2 starter skills
    skills_data = c.get('skills', get_default_skills(prof))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])

    # Synchronize equipment map (Tag 9)
    equip_map = {}
    for slot, item in equips.items():
        equip_map[int(slot)] = encode_sproto([
            (0, int(item['uid'])), (1, str(item['id'])), (2, True),
            (3, item.get('lv', 1)), (5, 1), (6, item.get('qual', 1)), (7, [0]*8)
        ])

    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr_oth), (5, prop), (6, get_visual(c.get('name', 'Hero'), prof)),
        (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)
    ])

def send_attr_sync(char, send_push):
    # Tag 510: aoi_update_attribute
    prof = char.get('prof', 0)
    hp = 3000; atk = 300; def_val = 35; hit = 480; dge = 60
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
    print(f"[EQUIP DEBUG] {char['name']} New Stats Sum: ATK: {atk} | HP: {hp} | DEF: {def_val} | POWER: {cv}")

def get_skill_sync(c):
    # DIAGNOSTIC: Sync all skills
    skills_data = get_default_skills(c.get('prof', 0))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])
    return encode_sproto([(0, skills_map), (1, False)])

def get_mission_sync(c):
    try:
        active = c.get('active_missions', {"1001": [1, 0, [0]*8]})
        missions_map = {}
        for mid, mdata in active.items():
            state, qual, parms = mdata
            missions_map[mid] = encode_sproto([(0, mid), (1, state), (2, qual), (3, parms)])
        last_id = c.get('last_main_mission', "")
        done_side = c.get('completed_side_missions', [])
        return encode_sproto([(0, missions_map), (1, last_id), (2, done_side)])
    except Exception:
        print("[SCENE ERROR] get_mission_sync failed:")
        traceback.print_exc()
        return encode_sproto([(0, {}), (1, "")])

def get_aoi_npc(npc_id, server_id, x, z, name="NPC"):
    vis = encode_sproto([(0, name), (1, "NPC_Nan_013"), (10, 0)])
    gen = encode_sproto([(0, name), (1, 0), (2, 1), (3, "11"), (4, 1)])
    stats = encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])
    pos_data = encode_sproto([(0, x), (1, 0), (2, z), (3, 0)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])
    attr = encode_sproto([(6, stats), (7, stats)])
    return encode_sproto([(0, server_id), (1, vis), (2, gen), (3, stats), (5, mv), (6, attr)])

def get_aoi_car(car_id, server_id, x, z, name="Car"):
    vis = encode_sproto([(0, name), (1, "DJ_Car_01"), (10, 0)])
    gen = encode_sproto([(0, name), (1, 0), (2, 1), (3, "11"), (4, 1)])
    stats = encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])
    pos_data = encode_sproto([(0, x), (1, 0), (2, z), (3, 0)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])
    attr = encode_sproto([(6, stats), (7, stats)])
    return encode_sproto([(0, server_id), (1, vis), (2, gen), (3, stats), (5, mv), (6, attr)])

def sync_mission_world_objects(char, send_push_func):
    active = char.get('active_missions', {})
    mstate = char.get('mission_state', {})
    for mid, mdata in active.items():
        try:
            logic = mission_logic_db.get(mid)
            if not logic: continue
            lid = logic['logicId']
            ltype = logic['logicType']
            state = mstate.get(mid, {})
            alive_sids = state.get('alive_sids', [])
            dead_sids = state.get('dead_sids', [])
            if ltype in [23, 1]:
                target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
                if target:
                    npc_id = target['npcId']
                    for sid in alive_sids:
                        if sid in dead_sids: continue
                        npc_hps[sid] = 1000
                        x = target.get('x', 29860) + random.randint(-100, 100)
                        z = target.get('z', -17005) + random.randint(-100, 100)
                        print(f"[MISSION SPAWN] Mission: {mid} SID: {sid} NPC: {npc_id} Pos: {x}, {z}")
                        send_push_func(505, get_aoi_npc(npc_id, sid, x, z, "Target"))
            elif ltype == 24 and state.get('progress', 0) == 0:
                target = car_target_db.get(lid)
                if target:
                    sid = 800000 + int(lid)
                    print(f"[MISSION SPAWN] Mission: {mid} SID: {sid} Car: {target['carId']}")
                    send_push_func(505, get_aoi_car(target['carId'], sid, target['x'], target['z'], "Car"))
        except Exception as e:
            print(f"[MISSION WARNING] Error syncing mission {mid}: {e}")

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0; cur_map_id = "11"
    global server_session_counter, online_clients, npc_hps

    def send_rpc_push(tag, data, target_conn=None):
        try:
            target = target_conn if target_conn else conn
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            target.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            # print(f"[TX PUSH] Tag: {tag} Size: {len(data)}")
        except Exception: pass

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
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                chars = all_accounts_chars.get(cur_areaId, {}).get(acc_id, [])
                resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8') if isinstance(c_data.get(0), bytes) else str(c_data.get(0, "Hero"))
                prof = get_val_int(c_data, 1, 0); cid = generate_unique_char_id()
                if cur_areaId not in all_accounts_chars: all_accounts_chars[cur_areaId] = {}
                if acc_id not in all_accounts_chars[cur_areaId]: all_accounts_chars[cur_areaId][acc_id] = []
                
                # START AT LEVEL 1 (Clean Baseline)
                nc = {'id': cid, 'name': name, 'prof': prof, 
                      'hp': 3000, 'level': 1, 'exp': 0,
                      'skills': get_default_skills(prof), 
                      'active_missions': {"1001": [1, 0, [0]*8]}, 
                      'mission_state': {"1001": init_mission_state("1001")}, 
                      'last_main_mission': "",
                      'mails': [], 'friends': [], 'foes': [],
                      'backpack': [], 'equip': {}}
                
                if VERIFICATION_MODE:
                    # [TEST ONLY] Grant "Starter Kit" (Weapon, Head, Body)
                    wid = "10001" if prof == 0 else "20001" if prof == 1 else "30001"
                    gain_item(nc, wid)   # Weapon
                    gain_item(nc, "10002") # Head
                    gain_item(nc, "10003") # Body
                    print(f"[VERIFICATION] Granted starter gear to {name}")
                
                all_accounts_chars[cur_areaId][acc_id].append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(cur_areaId, {}).get(acc_id, []) if c['id'] == char_id), None)
                resp = encode_sproto([(0, 1 if picked_char else 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if picked_char:
                    # Restore/Initialize missing fields for starter baseline
                    if 'level' not in picked_char: picked_char['level'] = 1
                    if 'exp' not in picked_char: picked_char['exp'] = 0
                    if 'mails' not in picked_char: picked_char['mails'] = []
                    if 'friends' not in picked_char: picked_char['friends'] = []
                    if 'foes' not in picked_char: picked_char['foes'] = []
                    if 'backpack' not in picked_char: picked_char['backpack'] = []
                    if 'equip' not in picked_char: picked_char['equip'] = {}
                    
                    if 'active_missions' not in picked_char: picked_char['active_missions'] = {"1001": [1, 0, [0]*8]}
                    if 'mission_state' not in picked_char:
                        try:
                            picked_char['mission_state'] = {}
                            for mid in picked_char['active_missions']: picked_char['mission_state'][mid] = init_mission_state(mid)
                        except Exception as e: print(f"[SCENE ERROR] init_mission_state failed: {e}")
                    
                    save_chars(all_accounts_chars)
                    cur_map_id, line_idx = "11", 1; online_clients[char_id] = (conn, cur_map_id, line_idx, picked_char)
                    
                    # STARTER UNLOCK: Only functions with Condition <= level
                    fids = ["4083","3031","4084","100","3016","4063","4061","4062","4064","3001","4081","111","112","110","3024","3025","3026","3027","3028","3029","107","108","109","3030","3014"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    
                    # [MAP FLOW] Correct Order for Map Entry
                    print("[MAP FLOW] Sending 614 sync_common_data")
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))
                    
                    print("[MAP FLOW] Sending 519 sync_mission")
                    send_rpc_push(519, get_mission_sync(picked_char))
                    
                    print("[MAP FLOW] Sending 503 enter_map")
                    send_rpc_push(503, encode_sproto([(0, cur_map_id), (1, line_idx), (2, 1)]))
                    
                    print("[MAP FLOW] Sending 504 main_player_create")
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(29860, 100, -17005))]))
                    
                    # Phase 1: Social & Mail Sync on Pick
                    send_friend_sync(picked_char, conn, send_rpc_push)
                    for m in picked_char['mails']:
                        send_rpc_push(531, encode_sproto([
                            (0, m['id']), (1, m.get('senderType', 0)), (3, m['title']),
                            (4, m['time']), (5, char_id), (6, m.get('readTime', 0)),
                            (7, m['context']), (8, m['state']), (9, m['time']), (11, 30)
                        ]))

            elif msg == 100: # map_ready
                print("[MAP FLOW] Received map_ready")
                if picked_char:
                    print("[MAP FLOW] Sending 611 sync_item_pack (Inventory)")
                    send_item_sync(picked_char, send_rpc_push)
                    
                    print("[MAP FLOW] Sending 540 sync_skill_info")
                    send_rpc_push(540, get_skill_sync(picked_char))
                    
                    print("[MAP FLOW] Sending 505 (World Objects)")
                    try:
                        sync_mission_world_objects(picked_char, send_rpc_push)
                    except Exception as e:
                        print(f"[MISSION WARNING] Missing data for mission object: {e}")
                    
                    # Fix PUSH tags to use correct server-to-client versions
                    print("[MAP FLOW] Sending 684 ret_domin_info")
                    send_rpc_push(684, encode_sproto([]))
                    
                    print("[MAP FLOW] Sending 555 sync_copyscenes_info")
                    send_rpc_push(555, encode_sproto([(0, [])]))
                    
                    print("[MAP FLOW] Sending 619 ret_request_activity_info")
                    send_rpc_push(619, encode_sproto([(0, [])]))
                    
                    print("[MAP FLOW] Sending 654 start_enter_game")
                    send_rpc_push(654, encode_sproto([(0, 1)]))

            elif msg == 120: # chat
                if session is not None and picked_char:
                    c_info = body.get(2, b"").decode('utf-8') if isinstance(body.get(2), bytes) else str(body.get(2))
                    c_type = get_val_int(body, 3) # 0:SYSTEM, 1:NEARBY, 2:WORLD, 3:TEAM, 4:GUILD, 5:PRIVATE
                    tell_id = get_val_int(body, 0)
                    
                    item = encode_sproto([
                        (0, picked_char['id']), (1, picked_char['name']),
                        (2, tell_id), (3, ""), # tellName placeholder
                        (4, c_info), (5, c_type), (9, picked_char.get('prof', 0)),
                        (10, picked_char.get('level', 50)), (11, 5000)
                    ])
                    push_data = encode_sproto([(0, [item])])
                    
                    if c_type == 2: # WORLD
                        for cid, (cl, _, _, _) in online_clients.items():
                            send_rpc_push(528, push_data, target_conn=cl)
                    elif c_type == 1: # NEARBY
                        for cid, (cl, mid, _, _) in online_clients.items():
                            if mid == cur_map_id: send_rpc_push(528, push_data, target_conn=cl)
                    else: # Fallback: return to sender
                        send_rpc_push(528, push_data)
                    
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 123: # mail_operation
                if session is not None and picked_char:
                    mid = get_val_int(body, 0)
                    op = get_val_int(body, 1)
                    if op == 0: # READ
                        for m in picked_char['mails']:
                            if m['id'] == mid: m['state'] = 1; m['readTime'] = int(time.time())
                    elif op == 1: # DELETE SINGLE
                        picked_char['mails'] = [m for m in picked_char['mails'] if m['id'] != mid]
                    elif op == 3: # GET ALL ITEMS
                        for m in picked_char['mails']:
                            if m['state'] == 2: m['state'] = 3
                    elif op == 4: # DELETE ALL
                        picked_char['mails'] = [m for m in picked_char['mails'] if m['state'] == 0 or m['state'] == 2] # Keep unread/uncollected
                    
                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 124: # add_friend
                if session is not None and picked_char:
                    fid = get_val_int(body, 0)
                    ftype = get_val_int(body, 1) # 0: NORMAL, 1: ENEMY
                    if ftype == 0:
                        if fid not in picked_char['friends']: picked_char['friends'].append(fid)
                    else:
                        if fid not in picked_char['foes']: picked_char['foes'].append(fid)
                    save_chars(all_accounts_chars)
                    send_friend_sync(picked_char, conn, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 125: # del_friend
                if session is not None and picked_char:
                    fid = get_val_int(body, 0)
                    ftype = get_val_int(body, 1)
                    if ftype == 0:
                        if fid in picked_char['friends']: picked_char['friends'].remove(fid)
                    else:
                        if fid in picked_char['foes']: picked_char['foes'].remove(fid)
                    save_chars(all_accounts_chars)
                    send_friend_sync(picked_char, conn, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 116: # equip_item
                if session is not None and picked_char:
                    uid = get_val_int(body, 0)
                    item = next((it for it in picked_char['backpack'] if it['uid'] == uid), None)
                    if item:
                        edata = equip_db.get(item['id'])
                        if edata:
                            pos = str(edata['pos'])
                            # SWAP: If slot occupied, move current equip to backpack
                            if pos in picked_char['equip']:
                                old = picked_char['equip'][pos]
                                picked_char['backpack'].append(old)
                            # Move new item to equip slot
                            picked_char['equip'][pos] = item
                            picked_char['backpack'] = [it for it in picked_char['backpack'] if it['uid'] != uid]
                            save_chars(all_accounts_chars)
                            # Sync updated state
                            send_item_sync(picked_char, send_rpc_push)
                            send_attr_sync(picked_char, send_rpc_push) # LIVE STAT UPDATE
                            print(f"[EQUIP DEBUG] {picked_char['name']} equipped ID: {item['id']} (Slot: {pos})")
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 117: # unequip_item
                if session is not None and picked_char:
                    uid = get_val_int(body, 0)
                    # Find slot by uid
                    target_pos = None
                    for pos, it in picked_char['equip'].items():
                        if it['uid'] == uid: target_pos = pos; break
                    if target_pos:
                        item = picked_char['equip'].pop(target_pos)
                        picked_char['backpack'].append(item)
                        save_chars(all_accounts_chars)
                        send_item_sync(picked_char, send_rpc_push)
                        send_attr_sync(picked_char, send_rpc_push) # LIVE STAT UPDATE
                        print(f"[EQUIP DEBUG] {picked_char['name']} unequipped ID: {item['id']} (Slot: {target_pos})")
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 101: # move
                if session is not None and picked_char:
                    p_raw = body.get(0)
                    if p_raw:
                        pd = decode_sproto(p_raw)
                        picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                        save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 102: # skill_use
                if session is not None and picked_char:
                    sk_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                    sk_name = skill_db.get(sk_id, {}).get('name', 'Unknown')
                    # Find slot index from the player's current skill map
                    slot = "Unknown"
                    if sk_id in picked_char.get('skills', {}):
                        slot = picked_char['skills'][sk_id].get('pos', 'Unknown')
                    
                    print(f"[SKILL USED]\nSkill ID: {sk_id}\nSkill Name: {sk_name}\nProfession: {picked_char.get('prof')}\nSlot: {slot}\nPlayer ID: {picked_char.get('id')}")
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 111: # accept_damge
                if session is not None and picked_char:
                    dmgs = body.get(0, [])
                    if isinstance(dmgs, bytes):
                        r, dmgs, p = dmgs, [], 0
                        while p < len(r):
                            l = struct.unpack("<I", r[p:p+4])[0]
                            dmgs.append(decode_sproto(r[p+4:p+4+l])); p += 4 + l
                    for d in dmgs:
                        tid, val = get_val_int(d, 0), get_val_int(d, 1)
                        cur_h = npc_hps.get(tid, 1000)
                        new_hp = cur_h - val
                        if new_hp < 0: new_hp = 0
                        npc_hps[tid] = new_hp
                        send_rpc_push(510, encode_sproto([(0, tid), (1, encode_sproto([(0, new_hp)]))]))
                        if new_hp == 0:
                            send_rpc_push(506, encode_sproto([(0, tid)]))
                            for mid, ms in picked_char.get('mission_state', {}).items():
                                    if tid in ms.get('alive_sids', []):
                                        if 'dead_sids' not in ms: ms['dead_sids'] = []
                                        if tid not in ms['dead_sids']:
                                            ms['dead_sids'].append(tid)
                                            ms['progress'] += 1
                                            if mid in picked_char['active_missions']:
                                                picked_char['active_missions'][mid][2][0] = ms['progress']
                                                logic = mission_logic_db.get(mid, {})
                                                req = kill_target_db.get(logic.get('logicId'), {}).get('require', 1) if logic.get('logicType') == 23 else mission_require_db.get(logic.get('logicId'), {}).get('require', 1)
                                                print(f"[MISSION DEAD] Mission: {mid} SID: {tid} Progress: {ms['progress']}/{req}")
                                                if ms['progress'] >= req:
                                                    picked_char['active_missions'][mid][0] = 2 
                                                    send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                            send_rpc_push(519, get_mission_sync(picked_char))
                                            save_chars(all_accounts_chars)
                                            break
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 113: # complete_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    if mid in picked_char.get('active_missions', {}):
                        del picked_char['active_missions'][mid]
                        if 'mission_state' in picked_char and mid in picked_char['mission_state']: del picked_char['mission_state'][mid]
                        picked_char['last_main_mission'] = mid
                        logic = mission_logic_db.get(mid)
                        if logic and logic['nextId'] and logic['nextId'] != "#N/A":
                            next_mid = logic['nextId']
                            picked_char['active_missions'][next_mid] = [1, 0, [0]*8]
                            picked_char['mission_state'][next_mid] = init_mission_state(next_mid)
                            print(f"[MISSION] {mid} -> {next_mid}")
                        save_chars(all_accounts_chars)
                        send_rpc_push(519, get_mission_sync(picked_char))
                        sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 112: # accept_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    picked_char['active_missions'][mid] = [1, 0, [0]*8]
                    if 'mission_state' not in picked_char: picked_char['mission_state'] = {}
                    picked_char['mission_state'][mid] = init_mission_state(mid)
                    save_chars(all_accounts_chars); send_rpc_push(519, get_mission_sync(picked_char))
                    sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 238: # use_mount (rob car)
                if session is not None and picked_char:
                    for mid, mdata in picked_char.get('active_missions', {}).items():
                        logic = mission_logic_db.get(mid)
                        if logic and logic['logicType'] == 24:
                            mdata[2][0], mdata[0] = 1, 2
                            send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                            send_rpc_push(519, get_mission_sync(picked_char))
                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # game server list
                srv = [encode_sproto([(0, 302), (1, "EU-001"), (2, "tokaido.proxy.rlwy.net"), (3, 48282), (4, 1), (5, 1), (6, 1), (7, 0), (8, 1), (9, 1)])]
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, srv)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 310, 313, 319, 686, 588, 550, 207, 680, 582, 633, 235, 655, 115, 120, 107, 129, 121, 130, 137, 122]:
                resp = encode_sproto([(0, f"U_{random.randint(10,99)}")]) if msg == 118 else encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))]) if msg == 218 else encode_sproto([])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if msg == 310: 
                    print("[MAP FLOW] Sending 684 ret_domin_info (via request)")
                    send_rpc_push(684, encode_sproto([]))
                elif msg == 145: 
                    print("[MAP FLOW] Sending 555 sync_copyscenes_info (via request)")
                    send_rpc_push(555, encode_sproto([(0, [])]))
                elif msg == 225:
                    print("[MAP FLOW] Sending 619 ret_request_activity_info (via request)")
                    send_rpc_push(619, encode_sproto([(0, [])]))

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally:
        if picked_char:
            cid = picked_char['id']
            if cid in online_clients: del online_clients[cid]
        conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (v17 LOADING FIX)")
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
