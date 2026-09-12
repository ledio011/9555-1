import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 15678))
CHAR_DB = "characters_final.json"
server_session_counter = 8000
GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {} # inst_id -> nid (to resolve rewards)
NPC_HP_MAP = {}   # inst_id -> current hp

# Load Mission Data
missions_data = {}
rewards_data = {}
LEVEL_DATA = {}
MONSTER_DATA = {} # mapId -> list of monster spawns
STATIC_NPC_DATA = {} # mapId -> list of static NPC spawns
NPC_CONFIG = {}   # npcId -> npc info template
MAP_CONFIG = {}   # mapId -> map info
MAP_CONNECT_DATA = {} # (src_id, target_id) -> PosX, PosY, PosZ
GUILD_CAPTURE_DATA = {} # id -> mapId
KILL_TARGET_SPAWNS = {} # missionId -> list of spawns
TARGET_CAR_SPAWNS = {}  # missionId -> list of car spawns

try:
    script_dir = os.path.dirname(__file__)
    md_path = os.path.join(script_dir, "missions.json")
    rd_path = os.path.join(script_dir, "mission_rewards.json")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f: missions_data = json.load(f)
    if os.path.exists(rd_path):
        with open(rd_path, "r", encoding='utf-8') as f: rewards_data = json.load(f)

    def is_data(line): return line.startswith("*,") or ("," in line and line.split(",")[1].isdigit())

    # Load BaseLvData for EXP requirements and stats
    lv_path = os.path.join(script_dir, "assets/Bundle/TextAsset/BaseLvData")
    if os.path.exists(lv_path):
        with open(lv_path, "r", encoding='utf-8') as f:
            for line in f:
                if is_data(line):
                    parts = line.strip().split(",")
                    if len(parts) > 20 and parts[1].isdigit():
                        lv = int(parts[1])
                        LEVEL_DATA[lv] = {
                            'exp': int(parts[3]),
                            'power': int(parts[2]),
                            'atk': [int(parts[4]), int(parts[11]), int(parts[18])],
                            'hp': [int(parts[5]), int(parts[12]), int(parts[19])],
                            'def': [int(parts[6]), int(parts[13]), int(parts[20])],
                            'hit': [int(parts[7]), int(parts[14]), int(parts[21])],
                            'eva': [int(parts[8]), int(parts[15]), int(parts[22])],
                            'cri': [int(parts[9]), int(parts[16]), int(parts[23])],
                            'res': [int(parts[10]), int(parts[17]), int(parts[24])]
                        }
        print(f"[LEVEL TABLE LOADED] levels={len(LEVEL_DATA)}")

    # Load MapInfoData
    map_info_path = os.path.join(script_dir, "assets/Bundle/TextAsset/MapInfoData")
    if os.path.exists(map_info_path):
        with open(map_info_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 8:
                        mid = parts[1]
                        MAP_CONFIG[mid] = {
                            'name': parts[2],
                            'scene': parts[3],
                            'type': int(parts[4]) if parts[4].isdigit() else 0,
                            'width': int(parts[6]) if parts[6].isdigit() else 0,
                            'height': int(parts[7]) if parts[7].isdigit() else 0,
                            'birth': parts[8],
                            'teleport_pos': parts[10] if len(parts) > 10 else "",
                            'open_lv': int(parts[26]) if len(parts) > 26 and parts[26].isdigit() else 0
                        }
        print(f"[MAP CONFIG LOADED] count={len(MAP_CONFIG)}")

    # Load MapConnectInfoData
    conn_path = os.path.join(script_dir, "assets/Bundle/TextAsset/MapConnectInfoData")
    if os.path.exists(conn_path):
        with open(conn_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 6:
                        src = parts[2]
                        dst = parts[3]
                        try:
                            px = float(parts[4])
                            py = float(parts[5]) if parts[5] else 0.0
                            pz = float(parts[6])
                            MAP_CONNECT_DATA[(src, dst)] = (px, py, pz)
                        except: pass
        print(f"[MAP CONNECT DATA LOADED] count={len(MAP_CONNECT_DATA)}")

    # Load GuildCaptureData
    gc_path = os.path.join(script_dir, "assets/Bundle/TextAsset/GuildCaptureData")
    if os.path.exists(gc_path):
        with open(gc_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,"):
                    parts = line.strip().split(",")
                    if len(parts) > 2:
                        GUILD_CAPTURE_DATA[parts[1]] = parts[2]
        print(f"[GUILD CAPTURE DATA LOADED] count={len(GUILD_CAPTURE_DATA)}")

    # Load NpcData
    npc_path = os.path.join(script_dir, "assets/Bundle/TextAsset/NpcData")
    if os.path.exists(npc_path):
        with open(npc_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 60:
                        nid = parts[1]
                        lvl = int(parts[9]) if parts[9].isdigit() else 1
                        is_abs = "绝对值" in parts[12]
                        NPC_CONFIG[nid] = {
                            'name': parts[2],
                            'model': parts[4],
                            'level': lvl,
                            'is_abs': is_abs,
                            'atk_coe': int(parts[26]) if len(parts) > 26 and parts[26].isdigit() else 10000,
                            'hp_coe': int(parts[27]) if len(parts) > 27 and parts[27].isdigit() else 10000,
                            'def_coe': int(parts[28]) if len(parts) > 28 and parts[28].isdigit() else 10000,
                            'atk_abs': int(parts[44]) if is_abs and len(parts) > 44 and parts[44].isdigit() else 0,
                            'hp_abs': int(parts[45]) if is_abs and len(parts) > 45 and parts[45].isdigit() else 0,
                            'def_abs': int(parts[46]) if is_abs and len(parts) > 46 and parts[46].isdigit() else 0
                        }
        print(f"[NPC CONFIG LOADED] count={len(NPC_CONFIG)}")

    # Load MonsterData (and split into Monster vs Static NPC)
    mon_path = os.path.join(script_dir, "assets/Bundle/TextAsset/MonsterData")
    if os.path.exists(mon_path):
        with open(mon_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 6 and parts[1].isdigit():
                        mid = parts[1]
                        group = int(parts[2]) if parts[2].isdigit() else 0
                        nid = parts[3]
                        entry = {
                            'nid': nid,
                            'x': int(parts[4]),
                            'z': int(parts[5]),
                            'o': int(parts[6])
                        }
                        if group == 9999:
                            if mid not in STATIC_NPC_DATA: STATIC_NPC_DATA[mid] = []
                            STATIC_NPC_DATA[mid].append(entry)
                        else:
                            if mid not in MONSTER_DATA: MONSTER_DATA[mid] = []
                            MONSTER_DATA[mid].append(entry)
        print(f"[MONSTER DATA LOADED] monsters_map={len(MONSTER_DATA)} static_npcs_map={len(STATIC_NPC_DATA)}")

    # Load KillTargetMissionData (Mission Spawns)
    kt_path = os.path.join(script_dir, "assets/Bundle/TextAsset/KillTargetMissionData")
    if os.path.exists(kt_path):
        with open(kt_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 7 and parts[1].isdigit():
                        mid = parts[1]
                        if mid not in KILL_TARGET_SPAWNS: KILL_TARGET_SPAWNS[mid] = []
                        KILL_TARGET_SPAWNS[mid].append({
                            'map': parts[2],
                            'x': int(parts[3]),
                            'z': int(parts[4]),
                            'o': int(parts[5]) if parts[5] else 0,
                            'nid': parts[6],
                            'num': int(parts[7]) if parts[7] else 1
                        })
        print(f"[KILL TARGET DATA LOADED] count={len(KILL_TARGET_SPAWNS)}")

    # Load TargetCarMissionData
    tc_path = os.path.join(script_dir, "assets/Bundle/TextAsset/TargetCarMissionData")
    if os.path.exists(tc_path):
        with open(tc_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or ("," in line and line.split(",")[1].isdigit()):
                    parts = line.strip().split(",")
                    if len(parts) > 6:
                        mid = parts[1]
                        if mid not in TARGET_CAR_SPAWNS: TARGET_CAR_SPAWNS[mid] = []
                        TARGET_CAR_SPAWNS[mid].append({
                            'map': parts[2],
                            'x': int(float(parts[3])),
                            'z': int(float(parts[4])),
                            'car_id': parts[6] # e.g. "Chevrolet"
                        })
        print(f"[TARGET CAR DATA LOADED] count={len(TARGET_CAR_SPAWNS)}")
except: traceback.print_exc()

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

all_accounts_chars = load_chars()

def generate_unique_char_id():
    return int(time.time() * 1000) % 1000000000

def get_area_id(serverId):
    try:
        sid = int(serverId)
        if sid == 1 or (300 <= sid < 400): return 1 # Europe
        if sid == 2 or (600 <= sid < 700): return 2 # Asia
        if sid == 3 or (10 <= sid < 100): return 0 # America
    except: pass
    return 0

def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, int): return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4: return struct.unpack("<i", val)[0]
        if len(val) == 8: return struct.unpack("<q", val)[0]
        if len(val) == 1: return val[0]
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
                    # Use 8-byte integers (long) for compatibility with List<long>
                    v = b"\x08" + b"".join([struct.pack("<q", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)): pass
                        else: item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                # A Sproto map is encoded as an array of its elements
                items = []
                for item in val.values():
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

# Skill System Constants
PROF_SKILLS = {
    0: {"atk": "101", "dodge": "104", "actives": ["105", "106", "107", "108", "109", "110"]},
    1: {"atk": "201", "dodge": "204", "actives": ["205", "206", "207", "208", "209", "210"]},
    2: {"atk": "301", "dodge": "304", "actives": ["305", "306", "307", "308", "309", "310"]}
}
SKILL_UNLOCK_LVS = [1, 5, 10, 15, 20, 25]

def get_skill_upgrade_cost(lv):
    if lv < 0: return 0
    if lv < 15: return (lv + 1) * 10000
    if lv < 24: return (lv - 13) * 100000 + 100000
    if lv == 24: return 3000000
    if lv == 25: return 7000000
    if lv == 26: return 18000000
    return 20000000

def build_skills_map(prof, char_level, skill_levels=None):
    prof = int(prof)
    if skill_levels is None: skill_levels = {}
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    smap = {}
    smap[p["atk"]] = encode_sproto([(0, p["atk"]), (1, skill_levels.get(p["atk"], 0)), (2, 0), (3, 1), (4, 0), (5, False)])
    smap[p["dodge"]] = encode_sproto([(0, p["dodge"]), (1, skill_levels.get(p["dodge"], 0)), (2, 3), (3, 1), (4, 1), (5, False)])
    for i in range(len(p["actives"])):
        sid = p["actives"][i]
        unlock_lv = SKILL_UNLOCK_LVS[i]
        if char_level >= unlock_lv:
            smap[sid] = encode_sproto([
                (0, sid),
                (1, skill_levels.get(sid, 0)),
                (2, 4 + i),
                (3, unlock_lv),
                (4, 2 + i),
                (5, False)
            ])
    return smap

def get_general(c):
    return encode_sproto([
        (0, c.get('name', 'Hero')),
        (1, c.get('prof', 0)),
        (2, 1),
        (3, str(c.get('map_id', '11'))),
        (4, 1)
    ])

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_character_stats(c):
    """Calculates all character attributes and Power based on profession and level."""
    lv = c.get('level', 1)
    prof = c.get('prof', 0)
    ld = LEVEL_DATA.get(lv, LEVEL_DATA.get(1))

    # Base attributes from BaseLvData
    atk = ld['atk'][prof]
    hp_max = ld['hp'][prof]
    df = ld['def'][prof]
    hit = ld['hit'][prof]
    eva = ld['eva'][prof]
    cri = ld['cri'][prof]
    res = ld['res'][prof]

    # Profession-specific coefficients from GameDefine.cs
    # XD (0), QJ (1), NQS (2)
    coeffs = [
        {"atk":16, "hp":1, "def":11, "hit":2, "eva":5.5, "cri":10, "res":10},
        {"atk":20, "hp":1, "def":12, "hit":1, "eva":6, "cri":5, "res":10},
        {"atk":7, "hp":1, "def":7.4, "hit":3, "eva":3.7, "cri":15, "res":10}
    ][prof]

    # Calculate Power (ComboValue) using the real weighting system found in client coefficients
    # Multiplied by 3.0 to match original gameplay scaling (approx 60k for starter)
    raw_power = (atk * coeffs['atk'] + hp_max * coeffs['hp'] + df * coeffs['def'] +
                 hit * coeffs['hit'] + eva * coeffs['eva'] + cri * coeffs['cri'] + res * coeffs['res'])
    power = int(raw_power * 3.0)

    return {
        'atk': atk, 'hp_max': hp_max, 'def': df,
        'hit': hit, 'eva': eva, 'cri': cri, 'res': res,
        'power': power, 'lv': lv, 'exp': c.get('exp', 0)
    }

def get_char_ov(c, sort_index=None):
    gen = get_general(c)
    stats = get_character_stats(c)
    attr = encode_sproto([(0, stats['lv']), (1, stats['power'])])
    # The client sorts character_overview.createtime ASCENDING.
    # To make last played show first, we use a virtual index as createtime.
    ctime = sort_index if sort_index is not None else c.get('createtime', int(time.time()))
    return encode_sproto([
        (0, c['id']),
        (1, gen),
        (2, attr),
        (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))),
        (4, ctime),
        (5, 0)
    ])

def get_full_char(c):
    gen = get_general(c)
    stats = get_character_stats(c)

    hp_cur = c.get('hp', stats['hp_max'])

    attr_oth = encode_sproto([
        (0, hp_cur),
        (1, stats['exp']),
        (2, stats['lv']),
        (3, stats['power']),
        (15, 1)
    ])

    prop = encode_sproto([(13, c.get('cash', 1000)), (14, 100), (15, 10), (16, 0), (17, 0), (18, 0)])
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])

    attr_run = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    # attr_all tags (attribute.cs): 0:max_hp, 2:atk, 3:def, 4:hit, 5:eva, 6:cri, 7:res, 13:mov
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (13, 500)
    ]
    attr_all = encode_sproto(attr_all_data)
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    char_level = stats['lv']
    skill_levels = c.get('skill_levels', {})
    skills_map = build_skills_map(c.get('prof', 0), char_level, skill_levels)
    wid = "10001" if c.get('prof', 0) == 0 else "20001" if c.get('prof', 0) == 1 else "30001"
    w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])
    equip_map = {5: w1}

    # download tag(15) set to 2 to make IsFinishDownload = true (prevents mission blocking)
    return encode_sproto([
        (0, c['id']),
        (1, gen),
        (2, attr_oth),
        (5, prop),
        (6, get_visual(c.get('name', 'Hero'), c.get('prof', 0))),
        (7, mv),
        (8, skills_map),
        (9, equip_map),
        (12, 0),
        (13, run),
        (15, 2)
    ])

def sync_char_attrs_rpc(conn, picked_char):
    """Sends TAG 510 (aoi_update_attribute) to sync all stats."""
    stats = get_character_stats(picked_char)
    hp_cur = picked_char.get('hp', stats['hp_max'])

    # attribute_other (Tag 1 in character_aoi_attribute)
    attr_oth = encode_sproto([
        (0, hp_cur), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)
    ])

    # attribute (Tag 2 in character_aoi_attribute)
    attr_base = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])

    # attribute_all (Tag 3 in character_aoi_attribute)
    attr_all = encode_sproto([
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (13, 500)
    ])

    prop = encode_sproto([(13, picked_char.get('cash', 0))])

    aoi_attr = encode_sproto([
        (0, picked_char['id']), (1, attr_oth), (2, attr_base), (3, attr_all), (5, prop)
    ])

    print(f"[PLAYER SYNC] HP={hp_cur}/{stats['hp_max']} POWER={stats['power']} LV={stats['lv']}")
    try:
        ph_p = encode_sproto([(0, 510)])
        pf_p = sproto_pack(ph_p + encode_sproto([(0, aoi_attr)]))
        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
    except: pass

def get_npc_attr(nid):
    cfg = NPC_CONFIG.get(nid)
    if not cfg: return 10000, 10000, 100, 10, 1 # Default fallback

    lvl = cfg.get('level', 1)
    ld = LEVEL_DATA.get(lvl, LEVEL_DATA.get(1))

    if cfg.get('is_abs'):
        hp = cfg.get('hp_abs', 10000)
        atk = cfg.get('atk_abs', 100)
        df = cfg.get('def_abs', 10)
    else:
        hp = (ld['hp'][0] * cfg.get('hp_coe', 10000)) // 10000
        atk = (ld['atk'][0] * cfg.get('atk_coe', 10000)) // 10000
        df = (ld['def'][0] * cfg.get('def_coe', 10000)) // 10000

    # Original stats check: if Atk/Hp are defined explicitly in NpcData, use them as minimums
    if cfg.get('hp_abs', 0) > hp: hp = cfg['hp_abs']
    if cfg.get('atk_abs', 0) > atk: atk = cfg['atk_abs']

    return hp, hp, atk, df, lvl

def spawn_map_npcs(conn, map_id, picked_char=None):
    """Spawns all NPCs, Monsters, and Traffic defined in data for the map."""
    map_str = str(map_id)

    def send_npc_create(nid, name, x, z, o):
        global GLOBAL_INST_COUNTER
        hp_cur, hp_max, atk, df, lvl = get_npc_attr(nid)
        GLOBAL_INST_COUNTER += 1
        inst_id = GLOBAL_INST_COUNTER
        NPC_HP_MAP[inst_id] = hp_max
        NPC_INST_MAP[inst_id] = str(nid) # Resolver mapping
        
        # Handle composite models like "PartA;PartB;PartC" to prevent client crashes
        final_nid = str(nid)
        if ";" in final_nid:
            if "XD_A" in final_nid: final_nid = "100"
            elif "QJ_A" in final_nid: final_nid = "104"
            elif "NQS_A" in final_nid: final_nid = "105"

        # npc_attribute schema: id(0), npcdataid(1), hp(2), max_hp(3), atk(4), def(5), x(15), z(16), o(17), level(18), player_name(21)
        attr = encode_sproto([
            (0, inst_id), (1, final_nid), (2, hp_cur), (3, hp_max), (4, atk), (5, df),
            (15, x), (16, z), (17, o), (18, lvl), (21, name)
        ])
        ph = encode_sproto([(0, 509)]); pf = sproto_pack(ph + encode_sproto([(0, attr)]))
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass
        return inst_id

    # 1. Spawn Static NPCs & Monsters
    if map_str in STATIC_NPC_DATA:
        for m in STATIC_NPC_DATA[map_str]:
            cfg = NPC_CONFIG.get(m['nid'], {'name': f"NPC_{m['nid']}"})
            send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    if map_str in MONSTER_DATA:
        for i, m in enumerate(MONSTER_DATA[map_str]):
            cfg = NPC_CONFIG.get(m['nid'], {'name': f"Monster_{m['nid']}"})
            send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    # Traffic Simulation (Random Cars) - Increased density
    if map_str == "11" or map_str == "101" or map_str == "105":
        car_models = ["Chevrolet", "SportsCar", "PoliceCar", "daKeChe", "jiaoChe_01", "jiaoChe_02", "chuZuChe", "xiaoKeChe"]
        spawn_count = 35 if map_str == "11" else 20
        for _ in range(spawn_count):
            model = random.choice(car_models)
            rx, rz = random.randint(-45000, 45000), random.randint(-45000, 45000)
            send_npc_create(model, f"CityCar_{model}", rx, rz, random.randint(0, 36000))

    # 3. Spawn Mission targets
    if picked_char:
        for mid, mdata in picked_char.get('active_missions', {}).items():
            if mdata['state'] == 1:
                if mid in KILL_TARGET_SPAWNS:
                    for s in KILL_TARGET_SPAWNS[mid]:
                        if str(s['map']) == map_str:
                            for _ in range(s['num']): send_npc_create(s['nid'], f"Quest_{s['nid']}", s['x'], s['z'], 0)
                if mid in TARGET_CAR_SPAWNS:
                    for s in TARGET_CAR_SPAWNS[mid]:
                        if str(s['map']) == map_str:
                            send_npc_create(s['car_id'], f"QuestCar_{s['car_id']}", s['x'], s['z'], 0)

def sync_mission_data(picked_char):
    own_missions_list = []
    for mid, mdata in picked_char.get('active_missions', {}).items():
        # Ensure parm has 8 elements and is long list
        parm = mdata.get('parm', [0]*8)
        if len(parm) < 8: parm += [0]*(8-len(parm))
        # ownmission schema: missionId(0), missionstate(1), missionquality(2), parm(3)
        # FIX: APK logic for SyncMissionList requires int.Parse(mid) for main missions check
        m_bytes = encode_sproto([
            (0, str(mid)),
            (1, int(mdata['state'])),
            (2, 0), # missionquality
            (3, [int(x) for x in parm])
        ])
        own_missions_list.append(m_bytes)
    
    last_main = picked_char.get('last_main_mission_id', "-1")
    if last_main == "" or last_main == "None": last_main = "-1"

    # sync_mission.request schema: missions(0), last_missionId(1), sidedone_mission(2)
    # FIX: missions must be encoded as a Sproto array of objects (concatenated length-prefixed chunks)
    data_list = [
        (0, own_missions_list),
        (1, str(last_main)),
        (2, [int(x) for x in picked_char.get('completed_side_missions', []) if x])
    ]
    return encode_sproto(data_list)

def sync_inventory_data(picked_char):
    items = {}
    inv = picked_char.get('inventory', [])
    for i in range(len(inv)):
        item = inv[i]
        guid = i + 10000
        items[guid] = encode_sproto([
            (0, guid),       # indexId
            (1, item['id']), # itemId
            (2, True),       # bindflag
            (5, item['amount']) # stack
        ])
    return encode_sproto([(0, items)])

def add_to_inventory(picked_char, item_id, amount):
    if 'inventory' not in picked_char: picked_char['inventory'] = []
    for item in picked_char['inventory']:
        if item['id'] == item_id:
            item['amount'] += amount
            return
    picked_char['inventory'].append({'id': item_id, 'amount': amount})

def give_mission_rewards(picked_char, mid, send_rpc_push):
    """Resolves rewards by profession and calculates level ups using BaseLvData."""
    try:
        m = missions_data.get(mid)
        if not m or not m.get('reward_ids'): return 0, 0, []

        prof = picked_char.get('prof', 0)
        rids = m['reward_ids']
        rid = rids[prof] if prof < len(rids) else rids[0]
        reward = rewards_data.get(rid)
        if not reward: return 0, 0, []

        added_exp = reward.get('exp', 0)
        added_cash = reward.get('cash', 0)

        picked_char['cash'] = picked_char.get('cash', 0) + added_cash
        picked_char['exp'] = picked_char.get('exp', 0) + added_exp

        while True:
            lv = picked_char.get('level', 1)
            req_data = LEVEL_DATA.get(lv)
            if not req_data: break
            if picked_char['exp'] >= req_data['exp']:
                picked_char['exp'] -= req_data['exp']
                picked_char['level'] = lv + 1
            else: break

        # Send original reward popup (Tag 638)
        popup_items = [
            encode_sproto([(0, "2001"), (1, added_exp), (2, 0)]),
            encode_sproto([(0, "1001"), (1, added_cash), (2, 0)])
        ]

        items, amts = reward.get('items', []), reward.get('item_amounts', [])
        granted_items = []
        for i in range(len(items)):
            if items[i]:
                amt = amts[i] if i < len(amts) else 1
                popup_items.append(encode_sproto([(0, items[i]), (1, amt), (2, 0)]))
                add_to_inventory(picked_char, items[i], amt)
                granted_items.append((items[i], amt))

        send_rpc_push(638, encode_sproto([(0, popup_items)]))
        return added_exp, added_cash, granted_items
    except:
        traceback.print_exc()
        return 0, 0, []

def accept_mission_logic(picked_char, mid):
    if mid not in missions_data:
        print(f"[accept_mission_logic] FAILED: {mid} not in missions_data")
        return False
    m = missions_data[mid]
    if picked_char.get('level', 1) < m.get('min_level', 0):
        print(f"[accept_mission_logic] FAILED: level too low {picked_char.get('level')} < {m.get('min_level')}")
        return False

    pre_id = m.get('pre_id', "")
    if pre_id:
        if m.get('class') == 1:
            if str(picked_char.get('last_main_mission_id', "")) != str(pre_id):
                print(f"[accept_mission_logic] FAILED: last_main {picked_char.get('last_main_mission_id')} != pre_id {pre_id}")
                return False
        else:
            try:
                ipre = int(pre_id)
                if ipre not in picked_char.get('completed_side_missions', []):
                    print(f"[accept_mission_logic] FAILED: side pre_id {ipre} not in completed {picked_char.get('completed_side_missions')}")
                    return False
            except: return False

    if 'active_missions' not in picked_char: picked_char['active_missions'] = {}
    if mid in picked_char['active_missions']:
        print(f"[accept_mission_logic] FAILED: {mid} already active")
        return False
    try:
        imid = int(mid)
        if imid in picked_char.get('completed_side_missions', []):
            print(f"[accept_mission_logic] FAILED: {mid} already in completed_side")
            return False
    except: pass
    if str(mid) == str(picked_char.get('last_main_mission_id')):
        print(f"[accept_mission_logic] FAILED: {mid} is last_main_mission_id")
        return False

    parm = [0]*8; parm[7] = int(time.time())
    picked_char['active_missions'][mid] = {'state': 1, 'parm': parm, 'accept_time': int(time.time())}
    return True

def init_character_fields(c):
    fields = {
        'level': 1, 'exp': 0, 'cash': 1000,
        'skill_levels': {},
        'active_missions': {},
        'completed_side_missions': [],
        'last_main_mission_id': "-1",
        'inventory': [],
        'pos': [29860, 100, -17005, 0],
        'map_id': "11"
    }
    for k, v in fields.items():
        if k not in c: c[k] = v

    # Initialize HP if not set
    if 'hp' not in c:
        lv = c.get('level', 1)
        prof = c.get('prof', 0)
        ld = LEVEL_DATA.get(lv, LEVEL_DATA.get(1, {'hp': [3000,3000,3000]}))
        c['hp'] = ld['hp'][prof] if prof < len(ld['hp']) else ld['hp'][0]

def start_map_transition(conn, picked_char, target_map_id, send_rpc_push):
    src_map = picked_char.get('map_id', '11')
    target_map_id = str(target_map_id)
    picked_char['map_id'] = target_map_id
    scene_name = "Unknown"

    # Update position
    landing_pos = None
    # 1. Try teleport portal heuristic
    if (target_map_id, src_map) in MAP_CONNECT_DATA:
        px, py, pz = MAP_CONNECT_DATA[(target_map_id, src_map)]
        # Liberty City height fix: ensure player is above NavMesh
        y_coord = int(py * 100)
        if target_map_id == "101" or target_map_id == "105":
            y_coord = 200 
        landing_pos = [int(px * 100), y_coord, int(pz * 100), 0]
        print(f"[TELEPORT] Transition {src_map} -> {target_map_id} using portal heuristic: {landing_pos}")

    # 2. Fallback to birth pos
    if not landing_pos and target_map_id in MAP_CONFIG:
        birth = MAP_CONFIG[target_map_id]['birth']
        scene_name = MAP_CONFIG[target_map_id]['scene']
        if birth:
            parts = birth.split('#')
            if len(parts) >= 3:
                # Fix: If height is 0, set it to a safe level
                y_coord = int(parts[1])
                if target_map_id == "101" or target_map_id == "105":
                    y_coord = 200 # Safe height above floor
                elif y_coord == 0:
                    y_coord = 100

                landing_pos = [int(parts[0]), y_coord, int(parts[2]), int(parts[3]) if len(parts) > 3 else 0]
                print(f"[TELEPORT] Spawn fix map={target_map_id} pos={landing_pos}")

    if landing_pos:
        picked_char['pos'] = landing_pos
    else:
        print(f"[MAP CONFIG MISSING] map_id={target_map_id}")

    save_chars(all_accounts_chars)
    # TAG 503: enter_map
    print("[DEBUG] BEFORE MAP ENTER")
    try:
        ph_p = encode_sproto([(0, 503)])
        # mapInfoId(0), line_index(1), line_count(2)
        data = encode_sproto([(0, target_map_id), (1, 0), (2, 1)])
        pf_p = sproto_pack(ph_p + data)
        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        print(f"[TX] PUSH TAG=503 SIZE={len(data)}")
        print(f"[MAP ENTER SEND] map_id={target_map_id} scene={scene_name} pos={picked_char['pos']}")

        # TAG 504: main_player_create
        send_rpc_push(504, encode_sproto([
            (0, get_full_char(picked_char)),
            (1, get_movement(picked_char['pos'][0], picked_char['pos'][1], picked_char['pos'][2], picked_char['pos'][3]))
        ]))
        print(f"[MAIN PLAYER CREATE SEND] map_id={target_map_id}")

        # TAG 505: aoi_add (NPCs)
        spawn_map_npcs(conn, target_map_id, picked_char)

    except Exception:
        print("[!] FAILED TO SEND MAP ENTER TRANSITION")
        traceback.print_exc()
    print("[DEBUG] AFTER MAP ENTER")

def is_skill_locked(sid, level, prof):
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    if sid in p["actives"]:
        idx = p["actives"].index(sid)
        if level < SKILL_UNLOCK_LVS[idx]:
            return True, SKILL_UNLOCK_LVS[idx]
    return False, 0

NPC_HP_MAP = {} # server-id -> current_hp

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0
    global server_session_counter

    def send_rpc_push(tag, data):
        try:
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            print(f"[TX] PUSH TAG={tag} SIZE={len(data)}")
        except Exception:
            print(f"[!] FAILED TO SEND PUSH TAG={tag}")
            traceback.print_exc()

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
            print(f"[RX] MSG={msg} SESSION={session}")
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1); cur_areaId = get_area_id(sid)
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (3, 1)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                chars = all_accounts_chars.get(cur_areaId, {}).get(acc_id, [])
                # Sort by last_played descending (internal)
                chars.sort(key=lambda x: x.get('last_played', 0), reverse=True)

                # The client sorts character_overview.createtime ASCENDING.
                # To make the last played (newest) show first, we give it the smallest createtime.
                ov_list = []
                for i, c in enumerate(chars):
                    ov_list.append(get_char_ov(c, i))

                resp = encode_sproto([(0, ov_list)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8') if isinstance(c_data.get(0), bytes) else str(c_data.get(0, "Hero"))
                prof = get_val_int(c_data, 1, 0); cid = generate_unique_char_id()
                if cur_areaId not in all_accounts_chars: all_accounts_chars[cur_areaId] = {}
                if acc_id not in all_accounts_chars[cur_areaId]: all_accounts_chars[cur_areaId][acc_id] = []
                nc = {'id': cid, 'name': name, 'prof': prof}
                init_character_fields(nc)
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
                    picked_char['last_played'] = int(time.time())
                    print(f"[CHARACTER PICK] id={picked_char['id']} level={picked_char.get('level')}")
                    init_character_fields(picked_char)
                    # Initial Mission Assignment for new characters
                    has_active_main = False
                    for active_id in picked_char['active_missions']:
                        if missions_data.get(active_id, {}).get('class') == 1:
                            has_active_main = True
                            break

                    if picked_char.get('last_main_mission_id') in [None, "", "-1", "0"] and not has_active_main:
                        if accept_mission_logic(picked_char, "1001"):
                            print(f"[MISSION ACCEPT] mission_id=1001 (Starting mission)")

                    save_chars(all_accounts_chars)

                    # Correct Sequence: 614 -> 611 -> 540 -> 519 -> 503

                    # 614: function_sync
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))

                    # 611: inventory_sync
                    send_rpc_push(611, sync_inventory_data(picked_char))

                    # 592: backpack_sync
                    send_rpc_push(592, encode_sproto([(0, {})]))

                    # 616: fashion_sync
                    send_rpc_push(616, encode_sproto([(0, {})]))

                    # 510: initial stats sync
                    sync_char_attrs_rpc(conn, picked_char)

                    # 540: skill_sync
                    smap = build_skills_map(picked_char['prof'], picked_char['level'], picked_char.get('skill_levels', {}))
                    send_rpc_push(540, encode_sproto([(0, smap), (1, False)]))

                    # 519: mission_sync
                    send_rpc_push(519, sync_mission_data(picked_char))

                    # TAG 503: enter_map
                    mid = str(picked_char.get('map_id', '11'))
                    scene_name = "Unknown"
                    if mid in MAP_CONFIG:
                        scene_name = MAP_CONFIG[mid]['scene']
                    else:
                        print(f"[MAP CONFIG MISSING] map_id={mid}")

                    print("[DEBUG] BEFORE MAP ENTER")
                    try:
                        ph_p = encode_sproto([(0, 503)])
                        data = encode_sproto([(0, mid), (1, 0), (2, 1)])
                        pf_p = sproto_pack(ph_p + data)
                        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
                        print(f"[TX] PUSH TAG=503 SIZE={len(data)}")
                        print(f"[MAP ENTER SEND] map_id={mid} scene={scene_name} pos={picked_char['pos']}")

                        # TAG 504: main_player_create
                        send_rpc_push(504, encode_sproto([
                            (0, get_full_char(picked_char)),
                            (1, get_movement(picked_char['pos'][0], picked_char['pos'][1], picked_char['pos'][2], picked_char['pos'][3]))
                        ]))
                        print(f"[MAIN PLAYER CREATE SEND] map_id={mid}")

                        # TAG 505: aoi_add (NPCs)
                        spawn_map_npcs(conn, mid, picked_char)

                    except Exception:
                        print("[!] FAILED TO SEND INITIAL MAP ENTER")
                        traceback.print_exc()
                    print("[DEBUG] AFTER MAP ENTER")
                    # Initial main_player_create handled by map_ready (MSG 100)

            elif msg == 100: # map_ready
                if picked_char:
                    mid = picked_char.get('map_id', '11')
                    print(f"[MAP READY RECEIVED] map_id={mid}")
                    send_rpc_push(654, encode_sproto([(0, 1)]))
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 270: # download_finish
                if picked_char:
                    print("[MSG 270] Client finished download. Sending start_enter_game.")
                    send_rpc_push(654, encode_sproto([(0, 1)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 106: # enter_new_map
                mid = body.get(0, b"").decode('utf-8')
                print(f"[RX] enter_new_map: {mid}")
                if picked_char:
                    start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 107: # enter_copy_scene
                mid = body.get(0, b"").decode('utf-8')
                print(f"[RX] enter_copy_scene: {mid}")
                if picked_char:
                    start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 246: # enter_survive_batttle
                mid = body.get(0, b"").decode('utf-8')
                if picked_char: start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 273: # enter_scuffle_batttle
                mid = body.get(0, b"").decode('utf-8')
                if picked_char: start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 207: # enter_bar_fight
                mid = body.get(0, b"").decode('utf-8')
                if picked_char: start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 201: # enter_wild_boss
                mid = body.get(0, b"").decode('utf-8')
                if picked_char: start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 322: # enter_guild_city_scene
                did = body.get(0, b"").decode('utf-8')
                mid = GUILD_CAPTURE_DATA.get(did, did)
                if picked_char: start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 101: # move
                if session is not None:
                    p_raw = body.get(0)
                    if p_raw and picked_char:
                        pd = decode_sproto(p_raw)
                        picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                        save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 112: # accept_mission
                if picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    res = accept_mission_logic(picked_char, mid)
                    print(f"[MISSION ACCEPT REQ] mid={mid} result={res}")
                    if res:
                        save_chars(all_accounts_chars)
                        print(f"[MISSION ACCEPT] mission_id={mid}")
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 113: # complete_mission
                if picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    m_entry = picked_char.get('active_missions', {}).get(mid)
                    print(f"[MISSION COMPLETE REQ] mid={mid} entry_exists={m_entry is not None} state={m_entry['state'] if m_entry else 'N/A'}")
                    if m_entry and m_entry['state'] == 2:
                        m_cfg = missions_data.get(mid)
                        if m_cfg:
                            print(f"[MISSION CHAIN] completed={mid} last_main_before={picked_char.get('last_main_mission_id')}")
                            exp_add, cash_add, items_add = give_mission_rewards(picked_char, mid, send_rpc_push)

                            # Mission Chain and Unlocking logic
                            is_chained = False
                            if m_cfg.get('class') == 1:
                                picked_char['last_main_mission_id'] = mid
                                print(f"[MISSION CHAIN] updated last_main={mid}")
                                # Auto-chain to next Main Mission
                                next_mid = m_cfg.get('next_id')
                                if next_mid and str(next_mid) in missions_data:
                                    res = accept_mission_logic(picked_char, str(next_mid))
                                    print(f"[MISSION NEXT] previous={mid} next={next_mid} accepted={res}")
                                    is_chained = res
                            else:
                                try:
                                    imid = int(mid)
                                    if imid not in picked_char.get('completed_side_missions', []):
                                        picked_char['completed_side_missions'].append(imid)
                                except: pass

                            del picked_char['active_missions'][mid]
                            save_chars(all_accounts_chars)
                            print(f"[MISSION COMPLETE] mission_id={mid} chained={is_chained}")

                            # Standard completion response
                            if session is not None:
                                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                                conn.sendall(struct.pack(">H", len(pf)) + pf)

                            # Push Sync sequence
                            send_rpc_push(521, encode_sproto([(0, mid), (1, 1)])) # Success feedback
                            sync_char_attrs_rpc(conn, picked_char)               # Stats update
                            send_rpc_push(519, sync_mission_data(picked_char))   # Mission UI update
                            if items_add:
                                send_rpc_push(611, sync_inventory_data(picked_char)) # Inventory sync
                        else:
                            if session is not None:
                                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                                conn.sendall(struct.pack(">H", len(pf)) + pf)
                    else:
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 524: # set_mission_param
                mid = body.get(0, b"").decode('utf-8')
                idx = get_val_int(body, 1); val = get_val_int(body, 2)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['parm'][idx-1] = val
                    save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 523: # set_mission_state
                mid = body.get(0, b"").decode('utf-8')
                state = get_val_int(body, 1)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['state'] = state
                    save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 130: # skill_level_up
                sid = body.get(0, b"").decode('utf-8')
                cur_lv = get_val_int(body, 1)
                if picked_char:
                    cost = get_skill_upgrade_cost(cur_lv)
                    if picked_char.get('cash', 0) >= cost and picked_char.get('level', 1) > cur_lv + 1:
                        picked_char['cash'] -= cost
                        picked_char['skill_levels'][sid] = cur_lv + 1
                        save_chars(all_accounts_chars)
                        smap = build_skills_map(picked_char['prof'], picked_char['level'], picked_char['skill_levels'])
                        send_rpc_push(540, encode_sproto([(0, smap), (1, True)]))
                        sync_char_attrs_rpc(conn, picked_char)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 102: # skill_use
                sid = body.get(1, b"").decode('utf-8'); tid = get_val_int(body, 0); alist = body.get(3, [])
                if picked_char:
                    locked, req_lv = is_skill_locked(sid, picked_char.get('level', 1), picked_char.get('prof', 0))
                    if locked:
                        print(f"[SKILL LOCKED] sid={sid} req={req_lv}")
                        send_rpc_push(529, encode_sproto([(0, "#{100681}"), (1, True)]))
                        # Do NOT send 508. Response will be empty.
                    else:
                        send_rpc_push(508, encode_sproto([(0, picked_char['id']), (1, tid), (2, sid), (3, alist)]))
                        
                        # Authoritative Combat: Calculate and Sync Damage
                        target_nid = NPC_INST_MAP.get(tid)
                        if target_nid:
                            hp_t, hp_t_m, atk_t, def_t, lvl_t = get_npc_attr(target_nid)
                            p_stats = get_character_stats(picked_char)
                            
                            # Player -> NPC Damage
                            val_dmg = p_stats['atk'] - def_t
                            dmg = val_dmg if val_dmg > 50 else 50
                            dmg = int(dmg * random.uniform(0.9, 1.1))
                            
                            # Update Server State
                            if tid in NPC_HP_MAP:
                                NPC_HP_MAP[tid] -= dmg
                            
                            # Push damage info to client (Tag 111: accept_damge)
                            dmg_item = encode_sproto([(0, tid), (1, dmg), (2, sid), (4, False)])
                            send_rpc_push(111, encode_sproto([(0, [dmg_item])]))
                            
                            # NPC -> Player Counter-Attack
                            val_ndmg = atk_t - p_stats['def']
                            npc_dmg = val_ndmg if val_ndmg > 10 else 10
                            npc_dmg = int(npc_dmg * random.uniform(0.8, 1.2))
                            
                            new_hp = picked_char.get('hp', p_stats['hp_max']) - npc_dmg
                            picked_char['hp'] = new_hp if new_hp > 0 else 0
                            
                            # Push player damage to client
                            p_dmg_item = encode_sproto([(0, picked_char['id']), (1, npc_dmg), (2, "1"), (4, False)])
                            send_rpc_push(111, encode_sproto([(0, [p_dmg_item])]))
                            
                            # Sync player attributes (HP bar)
                            sync_char_attrs_rpc(conn, picked_char)

                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 111: # accept_damge
                if picked_char and session is not None:
                    dlist = body.get(0, [])
                    for d in dlist:
                        target_id = d.get(0)
                        dmg = d.get(1)
                        if target_id == picked_char['id']:
                            # Ensure NPCs deal enough damage to be a threat
                            final_dmg = dmg if dmg > 10 else 10
                            new_hp = picked_char.get('hp', 0) - final_dmg
                            picked_char['hp'] = new_hp if new_hp > 0 else 0
                            sync_char_attrs_rpc(conn, picked_char)
                        elif target_id in NPC_HP_MAP:
                            NPC_HP_MAP[target_id] -= dmg
                            if NPC_HP_MAP[target_id] <= 0:
                                # Spawn original reward drop (Cash 1001, Exp 2001)
                                global GLOBAL_INST_COUNTER
                                GLOBAL_INST_COUNTER += 1
                                # drop_item_info (527): id(0), itemid(1), count(2), x(3), z(4)
                                pos = picked_char['pos']
                                drop_data = encode_sproto([(0, GLOBAL_INST_COUNTER), (1, "1001"), (2, 50), (3, pos[0]+100), (4, pos[2]+100)])
                                send_rpc_push(527, drop_data)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 307: # local_npc_die
                raw_id = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                die_type = get_val_int(body, 3)
                
                # Resolve Identity: Is it an Instance ID?
                npcid = None
                try:
                    inst_id = int(raw_id)
                    npcid = NPC_INST_MAP.get(inst_id)
                except: pass
                
                if not npcid: npcid = raw_id # Fallback to raw string ID

                # Cleanup HP tracking
                try:
                    to_del = [k for k in NPC_HP_MAP if str(k) == raw_id]
                    for k in to_del: del NPC_HP_MAP[k]
                except: pass

                if picked_char and npcid != "None":
                    # Original Kill Reward Logic
                    h_m, h_m, a_m, d_m, lvl_m = get_npc_attr(npcid)
                    # Increased base reward for generic kills
                    exp_kill = lvl_m * 20
                    cash_kill = lvl_m * 100
                    picked_char['exp'] = picked_char.get('exp', 0) + exp_kill
                    picked_char['cash'] = picked_char.get('cash', 0) + cash_kill

                    # Send reward tip (Tag 638)
                    send_rpc_push(638, encode_sproto([(0, [
                        encode_sproto([(0, "2001"), (1, exp_kill), (2, 0)]),
                        encode_sproto([(0, "1001"), (1, cash_kill), (2, 0)])
                    ])]))

                    # Level up loop
                    while True:
                        lv = picked_char.get('level', 1)
                        rd = LEVEL_DATA.get(lv)
                        if rd and picked_char['exp'] >= rd['exp']:
                            picked_char['exp'] -= rd['exp']
                            picked_char['level'] = lv + 1
                            print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']}")
                        else: break

                    updated = False
                    for mid, mdata in picked_char.get('active_missions', {}).items():
                        m_cfg = missions_data.get(mid)
                        if not m_cfg: continue

                        ltype = m_cfg.get('logic_type')
                        # 1: KILLMONSTER, 4: KILL_DROP, 6: INVESTIGATE, 11: COPY_KILL, 17: MASSACRE_NPC, 23: KILL_TARGET_NPC, 25: CAPTURE
                        if ltype in [1, 4, 6, 11, 17, 23, 25]:
                            if m_cfg.get('target_id') == npcid or ltype == 17:
                                mdata['parm'][0] += 1
                                print(f"[*] Mission {mid} progress: {mdata['parm'][0]}/{m_cfg.get('require_num')}")
                                send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, mdata['parm'][0])]))
                                if mdata['parm'][0] >= m_cfg.get('require_num'):
                                    mdata['state'] = 2 # COMPLETE
                                    send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                updated = True
                        # 19: ROB_CAR, 24: TARGET_ROB_CAR
                        elif ltype in [19, 24]:
                            if die_type in [2, 6]:
                                mdata['parm'][0] += 1
                                send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, mdata['parm'][0])]))
                                if mdata['parm'][0] >= m_cfg.get('require_num'):
                                    mdata['state'] = 2
                                    send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                updated = True
                        # 20: IMPACT_NPC
                        elif ltype == 20:
                            if die_type == 3:
                                mdata['parm'][0] += 1
                                send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, mdata['parm'][0])]))
                                if mdata['parm'][0] >= m_cfg.get('require_num'):
                                    mdata['state'] = 2
                                    send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                updated = True
                        # 0: STORY, 2: SEND_MSG, 21: ARRIVE_TARGET
                        elif ltype in [0, 2, 21]:
                            if die_type == 4 and npcid == mid:
                                mdata['state'] = 2
                                send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                updated = True

                    # CRITICAL: Sync attributes immediately after kill rewards
                    sync_char_attrs_rpc(conn, picked_char)
                    if updated:
                        save_chars(all_accounts_chars)
                        send_rpc_push(519, sync_mission_data(picked_char))

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 311: # enter_domin_pk_scene
                did = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[*] Entering PK scene for Domin ID={did}")
                # copy_scene_result (552): result(0)=1 (Win)
                send_rpc_push(552, encode_sproto([(0, 1)]))
                # Trigger capture_success logic (LogicType 25)
                if picked_char:
                    updated_missions = False
                    for mid_act, mdata in picked_char['active_missions'].items():
                        m_cfg = missions_data.get(mid_act)
                        if m_cfg and m_cfg.get('logic_type') == 25:
                            mdata['state'] = 2
                            send_rpc_push(523, encode_sproto([(0, mid_act), (1, 2)]))
                            updated_missions = True
                    if updated_missions:
                        save_chars(all_accounts_chars)
                        send_rpc_push(519, sync_mission_data(picked_char))
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 298: # impact_npc
                nid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[*] Interaction with NPC ID={nid}")
                if picked_char and nid == "1105":
                    # Special logic for Mission 1003 Challenge Dialogue
                    # This triggers the client-side Yes/No box (Dialog string 102098)
                    send_rpc_push(529, encode_sproto([(0, "102098"), (1, True)]))

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # update_game_server
                servers = [encode_sproto([(0, 302), (1, "EU-001"), (2, "s16.serv00.com"), (3, 15678), (4, 1), (5, 1), (6, 1), (7, 0), (8, 1), (9, 1)])]
                resp = encode_sproto([(0, servers)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 270: # download_finish - Give free car and items
                if picked_char:
                    picked_char['mount_id'] = "1001" # DJ_Car_01
                    add_to_inventory(picked_char, "9011", 10)
                    add_to_inventory(picked_char, "9001", 20)
                    add_to_inventory(picked_char, "5026", 5)
                    save_chars(all_accounts_chars)
                    send_rpc_push(611, sync_inventory_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)
                    # Special logic for Mission 1003 Challenge Dialogue
                    # This triggers the client-side Yes/No box (Dialog string 102098)
                    send_rpc_push(529, encode_sproto([(0, "102098"), (1, True)]))

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 298: # impact_npc
                nid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[*] Interaction with NPC ID={nid}")
                if picked_char and nid == "1105":
                    # Special logic for Mission 1003 Challenge Dialogue
                    # This triggers the client-side Yes/No box (Dialog string 102098)
                    send_rpc_push(529, encode_sproto([(0, "102098"), (1, True)]))

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # update_game_server
                servers = [encode_sproto([(0, 302), (1, "EU-001"), (2, "s16.serv00.com"), (3, 15678), (4, 1), (5, 1), (6, 1), (7, 0), (8, 1), (9, 1)])]
                resp = encode_sproto([(0, servers)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 270: # download_finish - Give free car and items
                if picked_char:
                    picked_char['mount_id'] = "1001" # DJ_Car_01
                    add_to_inventory(picked_char, "9011", 10)
                    add_to_inventory(picked_char, "9001", 20)
                    add_to_inventory(picked_char, "5026", 5)
                    save_chars(all_accounts_chars)
                    send_rpc_push(611, sync_inventory_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 310, 313, 319]:
                resp_data = encode_sproto([])
                if msg == 118: resp_data = encode_sproto([(0, f"User_{random.randint(100,999)}")])
                elif msg == 218: resp_data = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp_data)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if msg == 310: send_rpc_push(684, encode_sproto([]))
                elif msg == 145: send_rpc_push(555, encode_sproto([(0, [])]))

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (ATG MISSION SYSTEM REBUILT)");
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
