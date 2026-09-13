import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 15678))
CHAR_DB = "characters_final.json"
server_session_counter = 8000
GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {} # inst_id -> nid (to resolve rewards)
NPC_HP_MAP = {}   # inst_id -> current hp

# Load Game Data Tables
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

    # Load BaseLvData
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
                            'name': parts[2], 'scene': parts[3], 'type': int(parts[4]) if parts[4].isdigit() else 0,
                            'width': int(parts[6]) if parts[6].isdigit() else 0, 'height': int(parts[7]) if parts[7].isdigit() else 0,
                            'birth': parts[8], 'teleport_pos': parts[10] if len(parts) > 10 else "",
                            'open_lv': int(parts[26]) if len(parts) > 26 and parts[26].isdigit() else 0
                        }

    # Load MapConnectInfoData
    conn_path = os.path.join(script_dir, "assets/Bundle/TextAsset/MapConnectInfoData")
    if os.path.exists(conn_path):
        with open(conn_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 6:
                        src, dst = parts[2], parts[3]
                        try:
                            px, py, pz = float(parts[4]), float(parts[5]) if parts[5] else 0.0, float(parts[6])
                            MAP_CONNECT_DATA[(src, dst)] = (px, py, pz)
                        except: pass

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
                            'name': parts[2], 'model': parts[4], 'level': lvl, 'is_abs': is_abs,
                            'atk_coe': int(parts[26]) if len(parts) > 26 and parts[26].isdigit() else 10000,
                            'hp_coe': int(parts[27]) if len(parts) > 27 and parts[27].isdigit() else 10000,
                            'def_coe': int(parts[28]) if len(parts) > 28 and parts[28].isdigit() else 10000,
                            'atk_abs': int(parts[44]) if is_abs and len(parts) > 44 and parts[44].isdigit() else 0,
                            'hp_abs': int(parts[45]) if is_abs and len(parts) > 45 and parts[45].isdigit() else 0,
                            'def_abs': int(parts[46]) if is_abs and len(parts) > 46 and parts[46].isdigit() else 0
                        }

    # Load MonsterData
    mon_path = os.path.join(script_dir, "assets/Bundle/TextAsset/MonsterData")
    if os.path.exists(mon_path):
        with open(mon_path, "r", encoding='utf-8') as f:
            for line in f:
                if line.startswith("*,") or line.startswith(","):
                    parts = line.strip().split(",")
                    if len(parts) > 6 and parts[1].isdigit():
                        mid, group, nid = parts[1], int(parts[2]) if parts[2].isdigit() else 0, parts[3]
                        entry = {'nid': nid, 'x': int(parts[4]), 'z': int(parts[5]), 'o': int(parts[6])}
                        if group == 9999:
                            if mid not in STATIC_NPC_DATA: STATIC_NPC_DATA[mid] = []
                            STATIC_NPC_DATA[mid].append(entry)
                        else:
                            if mid not in MONSTER_DATA: MONSTER_DATA[mid] = []
                            MONSTER_DATA[mid].append(entry)
except: traceback.print_exc()

# --- Sproto System ---
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
                if val and isinstance(val[0], int): v = b"\x08" + b"".join([struct.pack("<q", i) for i in val])
                else:
                    items = []
                    for i in val:
                        if isinstance(i, str): i = i.encode('utf-8')
                        elif isinstance(i, (bytes, bytearray)): pass
                        else: i = str(i).encode('utf-8')
                        items.append(struct.pack("<I", len(i)) + i)
                    v = b"".join(items)
            elif isinstance(val, dict):
                # A Sproto map is encoded as an array of its elements
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

# --- Data Utilities ---
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

def get_character_stats(c):
    """Calculates character attributes based on profession coefficients in GameDefine.cs."""
    lv, prof = c.get('level', 1), c.get('prof', 0)
    ld = LEVEL_DATA.get(lv, LEVEL_DATA.get(1))
    atk, hp_max, df = ld['atk'][prof], ld['hp'][prof], ld['def'][prof]
    # Authoritative weights
    coeffs = [{"atk":16, "hp":1, "def":11}, {"atk":20, "hp":1, "def":12}, {"atk":7, "hp":1, "def":7.4}][prof]
    power = int((atk * coeffs['atk'] + hp_max * coeffs['hp'] + df * coeffs['def']) * 3.0)
    return {'atk': atk, 'hp_max': hp_max, 'def': df, 'hit': ld['hit'][prof], 'eva': ld['eva'][prof], 'cri': ld['cri'][prof], 'res': ld['res'][prof], 'power': power, 'lv': lv, 'exp': c.get('exp', 0)}

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"}, 1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"}, 2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

PROF_SKILLS = {0:{"atk":"101","dodge":"104","actives":["105","106","107","108","109","110"]}, 1:{"atk":"201","dodge":"204","actives":["205","206","207","208","209","210"]}, 2:{"atk":"301","dodge":"304","actives":["305","306","307","308","309","310"]}}
SKILL_UNLOCK_LVS = [1, 5, 10, 15, 20, 25]

def build_skills_map(prof, char_level, skill_levels=None):
    p = PROF_SKILLS.get(int(prof), PROF_SKILLS[0]); skill_levels = skill_levels or {}
    smap = {}
    smap[p["atk"]] = encode_sproto([(0, p["atk"]), (1, skill_levels.get(p["atk"], 0)), (2, 0), (3, 1), (4, 0), (5, False)])
    smap[p["dodge"]] = encode_sproto([(0, p["dodge"]), (1, skill_levels.get(p["dodge"], 0)), (2, 3), (3, 1), (4, 1), (5, False)])
    for i, sid in enumerate(p["actives"]):
        ulv = SKILL_UNLOCK_LVS[i]
        if char_level >= ulv:
            smap[sid] = encode_sproto([(0, sid), (1, skill_levels.get(sid, 0)), (2, 4+i), (3, ulv), (4, 2+i), (5, False)])
    return smap

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c, sort_index=None):
    stats = get_character_stats(c)
    gen = encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('map_id', '11')))])
    attr = encode_sproto([(0, stats['lv']), (1, stats['power'])])
    ctime = sort_index if sort_index is not None else c.get('createtime', int(time.time()))
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))), (4, ctime)])

def get_full_char(c):
    stats = get_character_stats(c)
    gen = encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('map_id', '11')))])
    attr_oth = encode_sproto([(0, c.get('hp', stats['hp_max'])), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)])
    prop = encode_sproto([(13, c.get('cash', 1000)), (14, 100), (15, 10)])
    mv = get_movement(*(c.get('pos', [29860, 100, -17005, 0])))
    skills = build_skills_map(c.get('prof', 0), stats['lv'], c.get('skill_levels', {}))
    attr_run = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def']), (4, stats['hit']), (5, stats['eva']), (13, 500)])
    run = encode_sproto([(6, attr_run), (7, attr_all)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr_oth), (5, prop), (6, get_visual(c['name'], c['prof'])), (7, mv), (8, skills), (12, 0), (13, run), (15, 2)])

def sync_char_attrs_rpc(conn, picked_char):
    stats = get_character_stats(picked_char)
    hp_cur = picked_char.get('hp', stats['hp_max'])
    attr_oth = encode_sproto([(0, hp_cur), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)])
    attr_base = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def']), (4, stats['hit']), (5, stats['eva']), (13, 500)])
    aoi_attr = encode_sproto([(0, picked_char['id']), (1, attr_oth), (2, attr_base), (3, attr_all), (5, encode_sproto([(13, picked_char.get('cash', 0))]))])
    try:
        ph = encode_sproto([(0, 510)]); pf = sproto_pack(ph + encode_sproto([(0, aoi_attr)]))
        conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: pass

def get_npc_attr(nid):
    cfg = NPC_CONFIG.get(nid, {'level':1, 'atk_coe':10000, 'hp_coe':10000, 'def_coe':10000})
    lvl = cfg['level']; ld = LEVEL_DATA.get(lvl, LEVEL_DATA.get(1))
    hp = (ld['hp'][0] * cfg['hp_coe']) // 10000
    atk = (ld['atk'][0] * cfg['atk_coe']) // 10000
    df = (ld['def'][0] * cfg['def_coe']) // 10000
    return hp, hp, atk, df, lvl

def spawn_map_npcs(conn, map_id, picked_char=None):
    map_str = str(map_id)
    def send_npc_create(nid, name, x, z, o):
        global GLOBAL_INST_COUNTER
        hp_cur, hp_max, atk, df, lvl = get_npc_attr(nid)
        GLOBAL_INST_COUNTER += 1
        inst_id = GLOBAL_INST_COUNTER
        NPC_HP_MAP[inst_id] = hp_max; NPC_INST_MAP[inst_id] = str(nid)
        attr = encode_sproto([(0, inst_id), (1, str(nid)), (2, hp_cur), (3, hp_max), (4, atk), (5, df), (15, x), (16, z), (17, o), (18, lvl), (21, name)])
        ph = encode_sproto([(0, 509)]); pf = sproto_pack(ph + encode_sproto([(0, attr)]))
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass

    if map_str in STATIC_NPC_DATA:
        for m in STATIC_NPC_DATA[map_str]: send_npc_create(m['nid'], f"NPC_{m['nid']}", m['x'], m['z'], m['o'])
    if map_str in MONSTER_DATA:
        for m in MONSTER_DATA[map_str]: send_npc_create(m['nid'], f"Monster_{m['nid']}", m['x'], m['z'], m['o'])

def sync_mission_data(picked_char):
    own_list = []
    for mid, mdata in picked_char.get('active_missions', {}).items():
        parm = mdata.get('parm', [0]*8)
        if len(parm) < 8: parm += [0]*(8-len(parm))
        own_list.append(encode_sproto([(0, str(mid)), (1, int(mdata['state'])), (2, 0), (3, [int(x) for x in parm])]))
    last_main = str(picked_char.get('last_main_mission_id', "-1"))
    return encode_sproto([(0, own_list), (1, last_main), (2, [int(x) for x in picked_char.get('completed_side_missions', []) if x])])

def sync_inventory_data(picked_char):
    items = {}
    inv = picked_char.get('inventory', [])
    for i, item in enumerate(inv):
        guid = i + 10000
        items[guid] = encode_sproto([(0, guid), (1, item['id']), (2, True), (5, item['amount'])])
    return encode_sproto([(0, items)])

def add_to_inventory(picked_char, item_id, amount):
    if 'inventory' not in picked_char: picked_char['inventory'] = []
    for it in picked_char['inventory']:
        if it['id'] == item_id: it['amount'] += amount; return
    picked_char['inventory'].append({'id': item_id, 'amount': amount})

def give_mission_rewards(picked_char, mid, send_rpc_push):
    m = missions_data.get(mid)
    if not m or not m.get('reward_ids'): return
    rid = m['reward_ids'][picked_char.get('prof', 0)]
    reward = rewards_data.get(rid)
    if not reward: return
    picked_char['cash'] = picked_char.get('cash', 0) + reward.get('cash', 0)
    picked_char['exp'] = picked_char.get('exp', 0) + reward.get('exp', 0)
    while True:
        lv = picked_char.get('level', 1); rd = LEVEL_DATA.get(lv)
        if rd and picked_char['exp'] >= rd['exp']: picked_char['exp'] -= rd['exp']; picked_char['level'] = lv + 1
        else: break
    pop = [encode_sproto([(0, "2001"), (1, reward.get('exp', 0)), (3, 0)]), encode_sproto([(0, "1001"), (1, reward.get('cash', 0)), (3, 0)])]
    send_rpc_push(638, encode_sproto([(0, pop)]))

def accept_mission_logic(picked_char, mid):
    if mid not in missions_data or picked_char.get('level', 1) < missions_data[mid].get('min_level', 0): return False
    if 'active_missions' not in picked_char: picked_char['active_missions'] = {}
    if mid in picked_char['active_missions']: return False
    picked_char['active_missions'][mid] = {'state': 1, 'parm': [0]*8}
    return True

def init_character_fields(c):
    fields = {'level':1, 'exp':0, 'cash':1000, 'skill_levels':{}, 'active_missions':{}, 'completed_side_missions':[], 'last_main_mission_id':"-1", 'inventory':[], 'pos':[29860, 100, -17005, 0], 'map_id':"11"}
    for k, v in fields.items():
        if k not in c: c[k] = v
    if 'hp' not in c: c['hp'] = get_character_stats(c)['hp_max']

def get_skill_upgrade_cost(lv):
    if lv < 15: return (lv + 1) * 10000
    return 3000000

def is_skill_locked(sid, level, prof):
    p = PROF_SKILLS.get(int(prof), PROF_SKILLS[0])
    if sid in p["actives"]:
        idx = p["actives"].index(sid); ulv = SKILL_UNLOCK_LVS[idx]
        if level < ulv: return True, ulv
    return False, 0

def start_map_transition(conn, picked_char, target_map_id, send_rpc_push):
    src_map = picked_char.get('map_id', '11'); target_map_id = str(target_map_id)
    picked_char['map_id'] = target_map_id
    landing_pos = None
    if (target_map_id, src_map) in MAP_CONNECT_DATA:
        px, py, pz = MAP_CONNECT_DATA[(target_map_id, src_map)]
        landing_pos = [int(px * 100), 200 if target_map_id in ["101", "105"] else int(py * 100), int(pz * 100), 0]
    if not landing_pos and target_map_id in MAP_CONFIG:
        birth = MAP_CONFIG[target_map_id]['birth']
        if birth:
            p = birth.split('#')
            landing_pos = [int(p[0]), 200 if target_map_id in ["101", "105"] else int(p[1]), int(p[2]), int(p[3]) if len(p) > 3 else 0]
    if landing_pos: picked_char['pos'] = landing_pos
    save_chars(all_accounts_chars)
    try:
        ph = encode_sproto([(0, 503)]); data = encode_sproto([(0, target_map_id), (1, 0), (2, 1)])
        conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + data))) + p)
        send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(*(picked_char['pos'])))]))
        spawn_map_npcs(conn, target_map_id, picked_char)
    except: pass

def client_handler(conn, addr):
    acc_id = "0"; picked_char = None; cur_areaId = 0
    def send_rpc_push(tag, data):
        try:
            ph = encode_sproto([(0, tag)]); pf = sproto_pack(ph + data)
            conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass

    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]; data = b""
            while len(data) < size:
                chunk = conn.recv(size - len(data))
                if not chunk: break
                data += chunk
            if len(data) < size: break
            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)

            if msg == 4: # login
                acc_id = str(body.get(1, b"")); cur_areaId = get_area_id(get_val_int(body, 5, 1))
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (3, 1)])
                ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(pf := sproto_pack(ph + resp))) + pf)
            elif msg == 103: # list
                chars = all_accounts_chars.get(cur_areaId, {}).get(acc_id, [])
                chars.sort(key=lambda x: x.get('last_played', 0), reverse=True)
                resp = encode_sproto([(0, [get_char_ov(c, i) for i, c in enumerate(chars)])])
                ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(pf := sproto_pack(ph + resp))) + pf)
            elif msg == 104: # create
                c_data = decode_sproto(body.get(0, b""))
                name, prof = str(c_data.get(0, b"")), get_val_int(c_data, 1, 0)
                nc = {'id': generate_unique_char_id(), 'name': name, 'prof': prof}; init_character_fields(nc)
                all_accounts_chars.setdefault(cur_areaId, {}).setdefault(acc_id, []).append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(pf := sproto_pack(ph + resp))) + pf)
            elif msg == 105: # pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(cur_areaId, {}).get(acc_id, []) if c['id'] == char_id), None)
                ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(pf := sproto_pack(ph + encode_sproto([(0, 1 if picked_char else 0)])))) + pf)
                if picked_char:
                    init_character_fields(picked_char); save_chars(all_accounts_chars)
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014"]
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (9, {f: encode_sproto([(0, f), (1, 1)]) for f in fids})]))
                    send_rpc_push(611, sync_inventory_data(picked_char))
                    sync_char_attrs_rpc(conn, picked_char)
                    send_rpc_push(540, encode_sproto([(0, build_skills_map(picked_char['prof'], picked_char['level'], picked_char['skill_levels'])), (1, False)]))
                    send_rpc_push(519, sync_mission_data(picked_char))
                    mid = str(picked_char.get('map_id', '11'))
                    try:
                        ph, d = encode_sproto([(0, 503)]), encode_sproto([(0, mid), (1, 0), (2, 1)])
                        conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + d))) + p)
                        send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(*(picked_char['pos'])))]))
                        spawn_map_npcs(conn, mid, picked_char)
                    except: pass
            elif msg == 100: # map_ready
                if picked_char: send_rpc_push(654, encode_sproto([(0, 1)])); send_rpc_push(519, sync_mission_data(picked_char))
            elif msg == 270: # download_finish
                if picked_char: send_rpc_push(654, encode_sproto([(0, 1)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(pf := sproto_pack(ph + encode_sproto([])))) + pf)
            elif msg in [106, 107, 246, 273, 201]: # transitions
                mid = body.get(0, b"").decode('utf-8'); start_map_transition(conn, picked_char, mid, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(pf := sproto_pack(ph + encode_sproto([])))) + pf)
            elif msg == 101: # move
                if picked_char and body.get(0):
                    pd = decode_sproto(body[0]); picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                    save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([(0, body.get(0))])))) + p)
            elif msg == 112: # accept
                mid = body.get(0, b"").decode('utf-8')
                if accept_mission_logic(picked_char, mid): save_chars(all_accounts_chars)
                send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([])))) + p)
            elif msg == 113: # complete
                mid = body.get(0, b"").decode('utf-8'); entry = picked_char.get('active_missions', {}).get(mid)
                if entry and entry['state'] == 2:
                    give_mission_rewards(picked_char, mid, send_rpc_push); cfg = missions_data.get(mid)
                    if cfg and cfg.get('class') == 1:
                        picked_char['last_main_mission_id'] = mid
                        if cfg.get('next_id'): accept_mission_logic(picked_char, str(cfg['next_id']))
                    del picked_char['active_missions'][mid]; save_chars(all_accounts_chars)
                    send_rpc_push(521, encode_sproto([(0, mid), (1, 1)])); sync_char_attrs_rpc(conn, picked_char); send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([])))) + p)
            elif msg == 524: # mission_param
                mid, idx, val = body.get(0, b"").decode('utf-8'), get_val_int(body, 1), get_val_int(body, 2)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['parm'][idx-1] = val; save_chars(all_accounts_chars); send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([])))) + p)
            elif msg == 523: # mission_state
                mid, state = body.get(0, b"").decode('utf-8'), get_val_int(body, 1)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['state'] = state; save_chars(all_accounts_chars); send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([])))) + p)
            elif msg == 102: # skill
                sid, tid = body.get(1, b"").decode('utf-8'), get_val_int(body, 0)
                if picked_char:
                    locked, req = is_skill_locked(sid, picked_char['level'], picked_char['prof'])
                    if not locked:
                        send_rpc_push(508, encode_sproto([(0, picked_char['id']), (1, tid), (2, sid)]))
                        tnid = NPC_INST_MAP.get(tid)
                        if tnid:
                            h, h, atk, df, lvl = get_npc_attr(tnid); stats = get_character_stats(picked_char)
                            dmg = max(50, stats['atk'] - df); dmg = int(dmg * random.uniform(0.9, 1.1))
                            if tid in NPC_HP_MAP: NPC_HP_MAP[tid] -= dmg
                            send_rpc_push(111, encode_sproto([(0, [encode_sproto([(0, tid), (1, dmg), (2, sid), (4, False)])])]))
                            n_dmg = max(10, atk - stats['def']); picked_char['hp'] = max(0, picked_char.get('hp', stats['hp_max']) - int(n_dmg * 0.5))
                            send_rpc_push(111, encode_sproto([(0, [encode_sproto([(0, picked_char['id']), (1, int(n_dmg*0.5)), (2, "1"), (4, False)])])]))
                            sync_char_attrs_rpc(conn, picked_char)
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([])))) + p)
            elif msg == 307: # die
                raw_id = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                npcid = NPC_INST_MAP.get(int(raw_id)) if raw_id.isdigit() else raw_id
                if picked_char and npcid:
                    h, h, atk, df, lvl = get_npc_attr(npcid); exp_k, cash_k = lvl * 20, lvl * 100
                    picked_char['exp'] += exp_k; picked_char['cash'] += cash_k
                    while True:
                        lv = picked_char.get('level', 1); rd = LEVEL_DATA.get(lv)
                        if rd and picked_char['exp'] >= rd['exp']: picked_char['exp'] -= rd['exp']; picked_char['level'] = lv + 1
                        else: break
                    send_rpc_push(638, encode_sproto([(0, [encode_sproto([(0, "2001"), (1, exp_k), (3, 0)]), encode_sproto([(0, "1001"), (1, cash_k), (3, 0)])])]))
                    for mid, mdata in picked_char.get('active_missions', {}).items():
                        m_cfg = missions_data.get(mid)
                        if m_cfg and (m_cfg.get('target_id') == npcid or m_cfg.get('logic_type') == 17):
                            mdata['parm'][0] += 1
                            if mdata['parm'][0] >= m_cfg.get('require_num', 1): mdata['state'] = 2
                    sync_char_attrs_rpc(conn, picked_char); save_chars(all_accounts_chars); send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([])))) + p)
            elif msg == 218: # heart
                ph = encode_sproto([(1, session)]); conn.sendall(struct.pack(">H", len(p := sproto_pack(ph + encode_sproto([(0, body.get(0)), (1, int(time.time()))])))) + p)
            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: traceback.print_exc()
    finally: conn.close()

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM); s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.bind(("0.0.0.0", PORT)); s.listen(20)
print(f"GAME SERVER 9555 READY (Complete Restoration)");
while True: cl, ad = s.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
