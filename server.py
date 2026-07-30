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

def send_mail_sync(char, conn, send_push):
    mails = char.get('mails', [])
    for m in mails:
        # SprotoType.mail_update.request (Tag 531)
        # 0: mailId, 1: sendertype, 3: title, 4: senderTime, 5: receiveId, 6: readTime
        # 7: context, 8: mailState, 9: sortTime, 10: items (map string->item), 11: expireday
        m_proto = encode_sproto([
            (0, m['id']), (1, m.get('senderType', 0)), (3, m['title']),
            (4, m['time']), (5, char['id']), (6, m.get('readTime', 0)),
            (7, m['context']), (8, m['state']), (9, m['time']), (10, {}), (11, 30)
        ])
        send_push(531, m_proto)

def get_friend_info_proto(local_id, target_id, ftype=0):
    c = get_char_by_id(target_id)
    if not c: return None
    # ftype: 0:Normal, 1:Apply, 2:Applied, 6:Enemy
    return encode_sproto([
        (0, local_id), (1, target_id), (2, c['name']), (3, c.get('level', 1)),
        (4, c.get('prof', 0)), (5, calculate_power(3000, 300, 35, 480, 60)),
        (6, 1 if target_id in online_clients else 0),
        (7, 0), (8, ftype), (9, 0), (10, ""), (11, 0)
    ])

def send_friend_sync(char, conn, send_push):
    friends = char.get('friends', [])
    foes = char.get('foes', [])
    res = {}
    for fid in friends:
        p = get_friend_info_proto(char['id'], fid, 0) # Normal
        if p: res[fid] = p
    for fid in foes:
        p = get_friend_info_proto(char['id'], fid, 6) # Enemy
        if p: res[fid] = p
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

def get_mission_npc_proto(npc_id, server_id, x, z, name="Mission Target"):
    # SprotoType.npc_attribute (Tag 509 npc_create expects this)
    # id(0), npcdataid(1), hp(2), max_hp(3), atk(4), def(5), hit(6), eva(7), cri(8)
    # exd(9), exr(10), res(11), crd(12), crr(13), defa(14), x(15), z(16), o(17), level(18)
    # player_name(21)
    return encode_sproto([
        (0, server_id), (1, str(npc_id)), (2, 1000), (3, 1000),
        (4, 100), (5, 50), (6, 100), (15, x), (16, z), (17, 0), (18, 1),
        (21, name)
    ])

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
                        print(f"[MISSION SPAWN] {mid} SID: {sid} NPC: {npc_id}")
                        # Tag 509: npc_create: npc_attribute(0)
                        send_push_func(509, encode_sproto([(0, get_mission_npc_proto(npc_id, sid, x, z))]))
            elif ltype == 24 and state.get('progress', 0) == 0:
                target = car_target_db.get(lid)
                if target:
                    sid = 800000 + int(lid)
                    print(f"[MISSION SPAWN] {mid} SID: {sid} Car: {target['carId']}")
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
                      'backpack': [], 'equip': {},
                      'mapId': "11", 'pos': [29860, 100, -17005, 0]}

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
                    if 'mapId' not in picked_char: picked_char['mapId'] = "11"
                    if 'pos' not in picked_char: picked_char['pos'] = [29860, 100, -17005, 0]

                    if 'active_missions' not in picked_char: picked_char['active_missions'] = {"1001": [1, 0, [0]*8]}
                    if 'mission_state' not in picked_char:
                        try:
                            picked_char['mission_state'] = {}
                            for mid in picked_char['active_missions']: picked_char['mission_state'][mid] = init_mission_state(mid)
                        except Exception as e: print(f"[SCENE ERROR] init_mission_state failed: {e}")

                    # Check for automatic mission completions (Level Up)
                    for mid, mdata in picked_char['active_missions'].items():
                        logic = mission_logic_db.get(mid)
                        if logic and logic['logicType'] == 7: # LEVEL_UP
                            if picked_char.get('level', 1) >= int(logic['logicId']):
                                mdata[0] = 2 # Completable

                    save_chars(all_accounts_chars)
                    cur_map_id = picked_char.get('mapId', "11")
                    line_idx = 1; online_clients[char_id] = (conn, cur_map_id, line_idx, picked_char)

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

                    last_pos = picked_char.get('pos', [29860, 100, -17005, 0])
                    print(f"[MAP FLOW] Sending 504 main_player_create at {last_pos}")
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(last_pos[0], last_pos[1], last_pos[2], last_pos[3]))]))

                    # Phase 1: Social & Mail Sync on Pick
                    send_friend_sync(picked_char, conn, send_rpc_push)
                    send_mail_sync(picked_char, conn, send_rpc_push)

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

            elif msg == 126: # request_update_friend_useinfo
                print(f"[UI REQUEST] tag={msg} (Social)")
                ftype = get_val_int(body, 1)
                res_map = {}
                target_ids = picked_char.get('friends', []) if ftype == 0 else picked_char.get('foes', [])
                for fid in target_ids:
                    c = get_char_by_id(fid)
                    if c:
                        res_map[fid] = encode_sproto([
                            (1, fid), (2, c['name']), (3, c.get('level', 1)),
                            (4, c.get('prof', 0)), (5, 5000), (6, 1 if fid in online_clients else 0),
                            (8, 6 if ftype == 1 else 0)
                        ])
                print(f"[UI RESPONSE] tag=534 (Social Sync)")
                send_rpc_push(534, encode_sproto([(0, res_map), (1, ftype)]))
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 143: # ask_shop_list
                print(f"[UI REQUEST] tag={msg} (Shop)")
                stype = get_val_int(body, 0)
                # SprotoType.ret_ask_shop_list (554): type(0), curPage(1), maxPage(2), shop_list(3), subType(4)
                resp = encode_sproto([(0, stype), (1, 1), (2, 1), (3, []), (4, 0)])
                print(f"[UI RESPONSE] tag=554 (Shop List)")
                send_rpc_push(554, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 242: # request_slot_info
                print(f"[UI REQUEST] tag={msg} (Slot Machine)")
                # SprotoType.ret_slot_info (633): slot_info(0), slot_datas(1), slot_items(2)
                s_info = encode_sproto([(1, 10), (2, 100)]) # Placeholder coins
                resp = encode_sproto([(0, s_info), (1, {}), (2, {})])
                print(f"[UI RESPONSE] tag=633 (Slot Info)")
                send_rpc_push(633, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 145: # ask_copyscenes_info
                print(f"[UI REQUEST] tag={msg} (Side Missions/Dungeons)")
                # SprotoType.sync_copyscenes_info (555): copyscenes(0) map string->copyscene_info
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=555 (Copy Scenes Sync)")
                send_rpc_push(555, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 225: # request_activity_info
                print(f"[UI REQUEST] tag={msg} (Activities)")
                # SprotoType.ret_request_activity_info (619): activity_info(0) map string->activity_info
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=619 (Activity Sync)")
                send_rpc_push(619, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 254: # sign_30_day
                print(f"[UI REQUEST] tag={msg} (Sign 30)")
                # ret_sign_30_day (642): cur_sign(0), replenish(1), sys_sign(2), cur_sign_state(3), replenish_sign_state(4), count(5), str(6)
                resp = encode_sproto([(0, 1), (1, 0), (2, 1), (3, False), (4, False), (5, 0), (6, "")])
                print(f"[UI RESPONSE] tag=642")
                send_rpc_push(642, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 255: # sign_week
                print(f"[UI REQUEST] tag={msg} (Sign Week)")
                # ret_sign_week (643): cur_sign(0), cur_sign_state(1)
                resp = encode_sproto([(0, 1), (1, False)])
                print(f"[UI RESPONSE] tag=643")
                send_rpc_push(643, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 257: # request_invest_pack
                print(f"[UI REQUEST] tag={msg} (Invest)")
                # ret_request_invest_pack (645): invest_pack(0) map string->invest_pack
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=645")
                send_rpc_push(645, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 261: # request_daily_active
                print(f"[UI REQUEST] tag={msg} (Daily Active)")
                # ret_request_daily_active (649): daily_actives(0), daily_rewards(1), score(2)
                resp = encode_sproto([(0, {}), (1, {}), (2, 0)])
                print(f"[UI RESPONSE] tag=649")
                send_rpc_push(649, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

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
                    # Operations: 0:Read, 1:Delete, 2:Get Single, 3:Get All, 4:Delete All
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
                        # Delete all except unread (0) or uncollected (2)
                        picked_char['mails'] = [m for m in picked_char['mails'] if m['state'] == 0 or m['state'] == 2]

                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 124: # add_friend
                if session is not None and picked_char:
                    fid = get_val_int(body, 0)
                    ftype = get_val_int(body, 1) # 0: NORMAL/FRIEND, 1: ENEMY
                    target_char = get_char_by_id(fid)

                    if target_char:
                        if ftype == 0: # Friend Request
                            # Sender: APPLY (1)
                            # (In a real system, we'd wait for accept, but let's implement the handshake)
                            if fid not in picked_char['friends']:
                                picked_char['friends'].append(fid)
                                # Remove from foes if exists
                                if fid in picked_char.get('foes', []): picked_char['foes'].remove(fid)

                            # Notify Sender: Tag 533 (Normal for now to satisfy UI)
                            p_sender = get_friend_info_proto(picked_char['id'], fid, 0)
                            send_rpc_push(533, encode_sproto([(0, p_sender)]))

                            # Notify Receiver (if online): Tag 533 (APPLIED 2)
                            if fid in online_clients:
                                r_conn, _, _, _ = online_clients[fid]
                                p_rec = get_friend_info_proto(fid, picked_char['id'], 2)
                                ph_r = encode_sproto([(0, 533)]); pf_r = sproto_pack(ph_r + p_rec)
                                try: r_conn.sendall(struct.pack(">H", len(pf_r)) + pf_r)
                                except: pass

                        else: # Add Enemy
                            if fid not in picked_char.setdefault('foes', []):
                                picked_char['foes'].append(fid)
                                if fid in picked_char.get('friends', []): picked_char['friends'].remove(fid)

                            p_enemy = get_friend_info_proto(picked_char['id'], fid, 6)
                            send_rpc_push(533, encode_sproto([(0, p_enemy)]))

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

            elif msg == 126: # request_update_friend_useinfo
                print(f"[UI REQUEST] tag=126 (Friends Sync) SESSION={session}")
                ftype = get_val_int(body, 1)
                res = {}
                target_ids = picked_char.get('friends', []) if ftype == 0 else picked_char.get('foes', [])
                for fid in target_ids:
                    p = get_friend_info_proto(picked_char['id'], fid, ftype == 1)
                    if p: res[fid] = p

                # ret_request_update_friend_useinfo (534): friend_list(0), type(1)
                resp = encode_sproto([(0, res), (1, ftype)])
                print(f"[UI RESPONSE] tag=534 (Friends Sync)")
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 143: # ask_shop_list
                print(f"[UI REQUEST] tag=143 (Shop List) SESSION={session}")
                # ret_ask_shop_list (554): type(0), curPage(1), maxPage(2), shop_list(3)
                resp = encode_sproto([(0, body.get(0, 0)), (1, 1), (2, 1), (3, [])])
                print(f"[UI RESPONSE] tag=554 (Shop List)")
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 242: # request_slot_info
                print(f"[UI REQUEST] tag=242 (Slot Info) SESSION={session}")
                # ret_slot_info (633): slot_info(0), slot_datas(1), slot_items(2)
                # slot_info: type(0), free_count(1), total_count(2)...
                si = encode_sproto([(0, 0), (1, 5), (2, 0)])
                resp = encode_sproto([(0, si), (1, {}), (2, {})])
                print(f"[UI RESPONSE] tag=633 (Slot Info)")
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 145: # ask_copyscenes_info
                print(f"[UI REQUEST] tag=145 (Side Missions) SESSION={session}")
                # sync_copyscenes_info (555): copyscenes(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=555 (Side Missions)")
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 225: # request_activity_info
                print(f"[UI REQUEST] tag=225 (Activity Info) SESSION={session}")
                # ret_request_activity_info (619): activity_info(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=619 (Activity Info)")
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 156: # title_req_level_up
                print(f"[UI REQUEST] tag=156 (Title Up) SESSION={session}")
                # ret_title_req_level_up (569): title_level(0), title_exp(1)
                tl = picked_char.get('title_level', 0)
                te = picked_char.get('title_exp', 0)
                resp = encode_sproto([(0, tl), (1, te)])
                print(f"[UI RESPONSE] tag=569 (Title Up)")
                send_rpc_push(569, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 191: # request_top_rank_list
                print(f"[UI REQUEST] tag=191 (Ranking) SESSION={session}")
                # ret_top_rank_list (598): rank_list(0) list
                resp = encode_sproto([(0, [])])
                print(f"[UI RESPONSE] tag=598 (Ranking)")
                send_rpc_push(598, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 263: # require_invest_reward
                print(f"[UI REQUEST] tag=263 (Invest) SESSION={session}")
                # ret_request_invest_pack (645): invest_list(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=645 (Invest)")
                send_rpc_push(645, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 254: # sign_30_day
                print(f"[UI REQUEST] tag=254 (SignMonth) SESSION={session}")
                # ret_sign_30_day (642): sign_list(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=642 (SignMonth)")
                send_rpc_push(642, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 255: # sign_week
                print(f"[UI REQUEST] tag=255 (SignWeek) SESSION={session}")
                # ret_sign_week (643): sign_list(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=643 (SignWeek)")
                send_rpc_push(643, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 299: # require_vip_info
                print(f"[UI REQUEST] tag=299 (VIP Info) SESSION={session}")
                # ret_require_vip_info (678): vip_level(0), recharge_count(1)...
                resp = encode_sproto([(0, 0), (1, 0), (2, 0)])
                print(f"[UI RESPONSE] tag=678 (VIP Info)")
                send_rpc_push(678, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 296: # req_level_reward
                print(f"[UI REQUEST] tag=296 (Level Reward) SESSION={session}")
                # ret_level_reward (674): reward_list(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=674 (Level Reward)")
                send_rpc_push(674, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 258: # request_daily_buy
                print(f"[UI REQUEST] tag=258 (Daily Buy) SESSION={session}")
                # ret_request_daily_buy (646): buy_list(0) map
                resp = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=646 (Daily Buy)")
                send_rpc_push(646, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 261: # request_daily_active
                print(f"[UI REQUEST] tag=261 (Daily Active) SESSION={session}")
                # ret_request_daily_active (649): active_info(0) obj
                ai = encode_sproto([(0, 0), (1, {})]) # score(0), rewards(1)
                resp = encode_sproto([(0, ai)])
                print(f"[UI RESPONSE] tag=649 (Daily Active)")
                send_rpc_push(649, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 260: # request_big_pack
                print(f"[UI REQUEST] tag=260 (Big Pack Info) SESSION={session}")
                # ret_request_big_pack (648): pack_list(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(648, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 259: # request_first_buy
                print(f"[UI REQUEST] tag=259 (First Buy Info) SESSION={session}")
                # ret_request_first_buy (647): first_buy_info(0) obj
                fbi = encode_sproto([(0, 0)]) # state(0)
                resp = encode_sproto([(0, fbi)])
                send_rpc_push(647, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 318: # request_guild_map_domine_top
                print(f"[UI REQUEST] tag=318 (Domin Top) SESSION={session}")
                # ret_guild_map_domine_top (688): top_list(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(688, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 235: # request_mount_info
                print(f"[UI REQUEST] tag=235 (Mount Info) SESSION={session}")
                # ret_mount_info (630): mount_info(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(630, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 253: # request_sign_week_info
                print(f"[UI REQUEST] tag=253 (Sign Week Info) SESSION={session}")
                # ret_request_sign_week_info (641): sign_info(0) obj
                swi = encode_sproto([(0, 0), (1, 0)]) # day(0), state(1)
                resp = encode_sproto([(0, swi)])
                send_rpc_push(641, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 274: # request_special_big_pack
                print(f"[UI REQUEST] tag=274 (Special Big Pack) SESSION={session}")
                # ret_special_big_pack (656): pack_list(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(656, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 252: # request_sign_30_day_info
                print(f"[UI REQUEST] tag=252 (Sign Month Info) SESSION={session}")
                # ret_request_30_day_info (640): sign_info(0) obj
                smi = encode_sproto([(0, 0), (1, 0)]) # day(0), state(1)
                resp = encode_sproto([(0, smi)])
                send_rpc_push(640, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 133: # request_random_rank_pvp_opponent
                print(f"[UI REQUEST] tag=133 (Arena) SESSION={session}")
                # ret_request_random_rank_pvp_opponent (542): opponent_list(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(542, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 202: # request_tower_copy_info
                print(f"[UI REQUEST] tag=202 (Tower) SESSION={session}")
                # ret_request_tower_copy_info (606): tower_info(0) obj
                ti = encode_sproto([(0, 0), (1, 1), (2, 0)]) # max_floor(0), cur_floor(1), today_count(2)
                resp = encode_sproto([(0, ti)])
                send_rpc_push(606, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 313: # request_dance_state_info
                print(f"[UI REQUEST] tag=313 (Dance State) SESSION={session}")
                # ret_request_dance_info (623): dance_info(0) obj
                di = encode_sproto([(0, 0)]) # state(0)
                resp = encode_sproto([(0, di)])
                send_rpc_push(623, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 319: # request_guild_map_info
                print(f"[UI REQUEST] tag=319 (Guild Map) SESSION={session}")
                # ret_request_guild_map_info (689): map_info(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(689, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 310: # request_domin_info
                print(f"[UI REQUEST] tag=310 (Domin Info) SESSION={session}")
                # ret_domin_info (684): domin_info(0) map
                resp = encode_sproto([(0, {})])
                send_rpc_push(684, resp)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 156: # title_req_level_up (Promote)
                print(f"[UI REQUEST] tag={msg} (Promote)")
                # ret_title_req_level_up (569): title_level(0), title_exp(1)
                resp_push = encode_sproto([(0, picked_char.get('title_lv', 0)), (1, picked_char.get('title_exp', 0))])
                print(f"[UI RESPONSE] tag=569")
                send_rpc_push(569, resp_push)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 210: # request_rank_pvp_data
                print(f"[UI REQUEST] tag={msg} (Ranking)")
                # syn_rank_pvp_data (541): combValue(0), times(1), rankPos(2)...
                resp_push = encode_sproto([(0, calculate_power(3000, 300, 35, 480, 60)), (1, 10), (2, 999)])
                print(f"[UI RESPONSE] tag=541")
                send_rpc_push(541, resp_push)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 235: # request_mount_info
                print(f"[UI REQUEST] tag={msg} (Vehicle Info)")
                # ret_mount_info (630): mount_info(0) map string->mount
                resp_push = encode_sproto([(0, {})])
                print(f"[UI RESPONSE] tag=630")
                send_rpc_push(630, resp_push)
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 101: # move
                if session is not None and picked_char:
                    p_raw = body.get(0)
                    if p_raw:
                        pd = decode_sproto(p_raw)
                        # Save [X, Y, Z, O]
                        picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                        # Also keep character's current mapId in sync
                        picked_char['mapId'] = cur_map_id
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
                            print(f"[COMBAT] Target {tid} died.")
                            send_rpc_push(506, encode_sproto([(0, tid)]))
                            for mid, ms in picked_char.get('mission_state', {}).items():
                                if tid in ms.get('alive_sids', []):
                                    if tid not in ms.get('dead_sids', []):
                                        ms.setdefault('dead_sids', []).append(tid)
                                        ms['progress'] += 1
                                        if mid in picked_char['active_missions']:
                                            picked_char['active_missions'][mid][2][0] = ms['progress']
                                            # Tag 524: set_mission_param: missionId(0), paramindex(1), param(2)
                                            # paramindex 0 is standard for kill count / primary objective
                                            send_rpc_push(524, encode_sproto([(0, mid), (1, 0), (2, ms['progress'])]))

                                            logic = mission_logic_db.get(mid, {})
                                            lid = logic.get('logicId')
                                            ltype = logic.get('logicType')
                                            req_data = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
                                            req = req_data.get('require', 1) if req_data else 1

                                            print(f"[MISSION PROGRESS] {mid}: {ms['progress']}/{req}")
                                            if ms['progress'] >= req:
                                                picked_char['active_missions'][mid][0] = 2
                                                # Tag 523: set_mission_state: missionId(0), missionstate(1)
                                                send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
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
                            # Accept Next Mission automatically
                            picked_char['active_missions'][next_mid] = [1, 0, [0]*8]
                            picked_char['mission_state'][next_mid] = init_mission_state(next_mid)

                            # Check for immediate requirements (e.g. Level Up)
                            n_logic = mission_logic_db.get(next_mid)
                            if n_logic and n_logic['logicType'] == 7: # LEVEL_UP
                                req_lv = int(n_logic['logicId'])
                                if picked_char.get('level', 1) >= req_lv:
                                    picked_char['active_missions'][next_mid][0] = 2

                            print(f"[STORY PROGRESS] Completed {mid} -> Started {next_mid}")

                        save_chars(all_accounts_chars)
                        # Push Updated Mission List (Tag 519)
                        send_rpc_push(519, get_mission_sync(picked_char))
                        # Spawn next mission objects
                        sync_mission_world_objects(picked_char, send_rpc_push)

                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 112: # accept_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    picked_char['active_missions'][mid] = [1, 0, [0]*8]
                    if 'mission_state' not in picked_char: picked_char['mission_state'] = {}
                    picked_char['mission_state'][mid] = init_mission_state(mid)

                    # Logic 2: Talk/Delivery - complete immediately upon interaction
                    logic = mission_logic_db.get(mid)
                    if logic and logic['logicType'] == 2:
                        picked_char['active_missions'][mid][0] = 2

                    save_chars(all_accounts_chars); send_rpc_push(519, get_mission_sync(picked_char))
                    sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 238: # use_mount (rob car)
                if session is not None and picked_char:
                    for mid, mdata in picked_char.get('active_missions', {}).items():
                        logic = mission_logic_db.get(mid)
                        # Type 24: TARGET_ROB_CAR
                        if logic and logic['logicType'] == 24:
                            mdata[0] = 2 # Completable
                            # Tag 523: set_mission_state
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
