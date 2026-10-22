import socket, struct, threading, random, json, os, time, traceback, math

PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
CHAR_DB = os.path.join(SCRIPT_DIR, "characters_final.json")
BAK_DB = CHAR_DB + ".bak"
TMP_DB = CHAR_DB + ".tmp"
RESOURCE_ROOT = os.path.join(SCRIPT_DIR, "assets")
server_session_counter = 8000
GLOBAL_INST_COUNTER = 3000000
GLOBAL_INST_COUNTER_LOCK = threading.Lock()
NPC_INST_MAP = {} # inst_id -> nid (to resolve rewards)
NPC_INSTANCE_MAP = {} # inst_id -> map ID, to reject cross-map forged death reports
NPC_HP_MAP = {}   # inst_id -> current hp
NPC_ATTR_MAP = {} # runtime NPC payloads for reconnect replay
TEAM_STATES = {}
CHAR_TEAM = {}
TEAM_INVITES = {}
TEAM_MATCH_QUEUE = {}
TEAM_ID_COUNTER = 500000
EXP_RUN_COUNTER = 900000
NPC_SPAWNED_MAPS = {}  # connection identity -> maps already sent to that client
DEAD_NPC_SET = set() # duplicate death/reward prevention set
LOCAL_NPC_DEATHS = {} # (character, map, npc, spawn) -> last accepted local death time
LOCAL_NPC_DEATH_LOCK = threading.Lock()
SURVEY_PROGRESS_TIMES = {} # (character, mission) -> last accepted survey event (monotonic)
CAR_MISSION_EVENT_TIMES = {} # character -> last accepted generic car robbery event (monotonic)
RANDOM_NAME_CALLED = set()  # connection identities that have had their first random name request
ALL_CONNECTIONS = {}  # char_id -> (conn, picked_char) for tracking online players
CONNECTION_LOCKS = {}  # char_id -> threading.Lock() for per-connection send protection
MAIL_ID_COUNTER = 1000000  # Will be initialized from max existing mailId on startup
CHAR_DB_SAVE_LOCK = threading.Lock() # serialize atomic snapshots to the shared temp/backup files
TEAM_ID_COUNTER_LOCK = threading.Lock()
EXP_RUN_COUNTER_LOCK = threading.Lock()
MAIL_ID_COUNTER_LOCK = threading.Lock()

def next_global_inst_id():
    global GLOBAL_INST_COUNTER
    with GLOBAL_INST_COUNTER_LOCK:
        GLOBAL_INST_COUNTER += 1
        return GLOBAL_INST_COUNTER

def next_team_id():
    global TEAM_ID_COUNTER
    with TEAM_ID_COUNTER_LOCK:
        TEAM_ID_COUNTER += 1
        return TEAM_ID_COUNTER

def next_exp_run_id():
    global EXP_RUN_COUNTER
    with EXP_RUN_COUNTER_LOCK:
        EXP_RUN_COUNTER += 1
        return EXP_RUN_COUNTER

def next_mail_id():
    global MAIL_ID_COUNTER
    with MAIL_ID_COUNTER_LOCK:
        mail_id = MAIL_ID_COUNTER
        MAIL_ID_COUNTER += 1
        return mail_id

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
KILL_TARGET_SPAWNS = {} # logicId -> list of spawns
TARGET_CAR_SPAWNS = {}  # logicId -> list of car spawns
MISSION_REQUIRE_DATA = {} # logicId -> dict
MOVE_TARGET_DATA = {}   # logicId -> dict
SURVEY_DATA = {}        # logicId -> dict
MULTI_DELIVERY_DATA = {} # logicId -> ordered delivery stops
EFF_CONFIG = {}   # effId -> effect info template
SKILL_CONFIG = {} # skillId -> skill info template
EFF_TO_SKILL = {} # effId -> skillId (reverse lookup for Tag 128)
MOUNT_CONFIG = {} # garage vehicle id -> client MountData definition
COLOR_CONFIG = {} # color id -> authoritative PriceType/PriceCost from ColorData
WILD_BOSS_CONFIG = {} # ID -> configured PVE world boss and required level band
COPY_SCENE_CONFIG = {} # daily-copy id -> CopySceneData fields used by the APK
SHOW_REWARD_CONFIG = {} # ShowRewardData id -> exact visible item list
STREET_RACE_REWARD_BY_LEVEL = {} # level -> AdaptData _drop_bc ShowRewardData id
ADAPT_DATA = {} # level -> dict of std values from AdaptData
ITEM_CONFIG = {} # itemId -> {type, function}
FUNCTION_DATA = {} # funcId -> {class, condition, is_download, first_open, unlock_type, side_mission}
EQUIP_CONFIG = {} # equipId -> {name, lv, class, job, position, base_stat, base_val, model, ...}
DOWNLOAD_REWARD_DATA = [] # list of (itemId, count, quality) tuples
SKILL_UPGRADE_DATA = {} # level -> {price_type, price_value}
RELIFE_DATA = [] # list of {min_count, max_count, use_count}
SERVER_DATA_LIST = [] # list of server dicts from ServerData
GAME_CONFIG = {} # key -> value (from ConfigData)
DAILY_EXP_CONFIG = {} # copy_id -> DailyExpData fields (wave_count, group_npc_count, etc.)

try:
    script_dir = os.path.dirname(__file__)

    # Accept both the original folder name and the plural name used by the
    # deployed resource tree.
    text_asset_root = os.path.join(script_dir, "assets", "Bundle", "TextAsset")
    if not os.path.isdir(text_asset_root):
        text_asset_root = os.path.join(script_dir, "assets", "Bundle", "TextAssets")
    if not os.path.isdir(text_asset_root):
        text_asset_root = os.path.join(script_dir, "Decompiled", "assets", "Bundle", "TextAsset")

    # Load authoritative MissionData TextAsset first
    md_path = os.path.join(text_asset_root, "MissionData")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 23 and parts[0] == "*" and parts[1].isdigit():
                    mid = parts[1]
                    missions_data[mid] = {
                        'id': mid,
                        'name': parts[2] if len(parts) > 2 else '',
                        'class': int(parts[6]) if parts[6].isdigit() else 0,
                        'logic_type': int(parts[7]) if parts[7].isdigit() else 0,
                        'logic_id': parts[9],
                        'target_id': parts[11],
                        'pre_id': parts[12],
                        'next_id': parts[14],
                        'min_level': int(parts[23]) if parts[23].isdigit() else 1,
                        'reward_ids': [parts[25] if len(parts)>25 else "", parts[27] if len(parts)>27 else "", parts[29] if len(parts)>29 else ""]
                    }
        print(f"[MISSION DATA LOADED] count={len(missions_data)}")

    # Merge decompiled missions.json if present (supplementing missing missions/fields)
    json_md_paths = [
        os.path.join(script_dir, "dec&normal", "Decompiled", "missions.json"),
        os.path.join(script_dir, "decompiled_src", "missions.json"),
        os.path.join(script_dir, "missions.json"),
    ]
    for p in json_md_paths:
        if os.path.exists(p):
            with open(p, "r", encoding='utf-8') as f:
                raw_m = json.load(f)
                for mid_k, mv in raw_m.items():
                    smid = str(mid_k)
                    if smid not in missions_data:
                        missions_data[smid] = {
                            'id': str(mv.get('id', smid)),
                            'name': mv.get('name', ''),
                            'class': int(mv.get('class', 0)),
                            'logic_type': int(mv.get('logic_type', 0)),
                            'logic_id': str(mv.get('logic_id', '')),
                            'target_id': str(mv.get('target_id', '')),
                            'pre_id': str(mv.get('pre_id', '')),
                            'next_id': str(mv.get('next_id', '')),
                            'min_level': int(mv.get('min_level', 1)),
                            'require_num': int(mv.get('require_num', 1)),
                            'target_type': mv.get('target_type', ''),
                            'map_id': str(mv.get('map_id', '')),
                            'story_id': str(mv.get('story_id', '')),
                            'reward_ids': [str(x) for x in mv.get('reward_ids', [])]
                        }
                    else:
                        m_entry = missions_data[smid]
                        if 'reward_ids' not in m_entry or not any(m_entry['reward_ids']):
                            m_entry['reward_ids'] = [str(x) for x in mv.get('reward_ids', [])]
                        if 'require_num' in mv and 'require_num' not in m_entry:
                            m_entry['require_num'] = int(mv.get('require_num', 1))
                        if 'map_id' in mv and not m_entry.get('map_id'):
                            m_entry['map_id'] = str(mv.get('map_id', ''))
                        if 'story_id' in mv and not m_entry.get('story_id'):
                            m_entry['story_id'] = str(mv.get('story_id', ''))
            # Ensure require_num is valid (at least 1) for all missions
            for _mk, _mv in missions_data.items():
                if int(_mv.get('require_num') or 0) <= 0:
                    _mv['require_num'] = 1
            print(f"[MISSION JSON MERGED] count={len(missions_data)}")
            break

    # Load decompiled mission_rewards.json if present
    json_rd_paths = [
        os.path.join(script_dir, "dec&normal", "Decompiled", "mission_rewards.json"),
        os.path.join(script_dir, "decompiled_src", "mission_rewards.json"),
        os.path.join(script_dir, "mission_rewards.json"),
    ]
    for p in json_rd_paths:
        if os.path.exists(p):
            with open(p, "r", encoding='utf-8') as f:
                raw_r = json.load(f)
                for rid_k, rv in raw_r.items():
                    item_amts = rv.get('item_amounts', [])
                    rewards_data[str(rid_k)] = {
                        'exp': int(rv.get('exp', 0)),
                        'cash': int(rv.get('cash', 0)),
                        'items': [str(x) for x in rv.get('items', [])],
                        'item_amounts': item_amts,
                        'amounts': item_amts
                    }
            print(f"[MISSION REWARDS JSON LOADED] count={len(rewards_data)}")
            break

    rd_path = os.path.join(text_asset_root, "ShowRewardData")
    if not rewards_data and os.path.exists(rd_path):
        with open(rd_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 5 and parts[0] == "*" and parts[1].isdigit():
                    rid = parts[1]
                    exp, cash = 0, 0
                    items, amounts = [], []
                    for i in range(8):
                        idx_item = 3 + i*3
                        idx_count = 5 + i*3
                        if idx_count < len(parts) and parts[idx_item].isdigit():
                            iid = parts[idx_item]
                            icount = int(parts[idx_count]) if parts[idx_count].isdigit() else 1
                            if iid == "2001": exp += icount
                            elif iid == "1001": cash += icount
                            else:
                                items.append(iid)
                                amounts.append(icount)
                    rewards_data[rid] = {'exp': exp, 'cash': cash, 'items': items, 'amounts': amounts, 'item_amounts': amounts}
        print(f"[REWARDS DATA LOADED] count={len(rewards_data)}")

    def is_data(line): return line.startswith("*,") or ("," in line and line.split(",")[1].isdigit())

    # Load EffInfoData
    eff_path = os.path.join(text_asset_root, "EffInfoData")
    if os.path.exists(eff_path):
        with open(eff_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) >= 30 and parts[1].isdigit():
                    eid = parts[1]
                    adds = {}
                    for i in [22, 24, 26, 28]:
                        if i+1 < len(parts) and parts[i].isdigit():
                            adds[int(parts[i])] = int(parts[i+1])
                    EFF_CONFIG[eid] = {
                        'dmg_fixed': int(parts[3]) if parts[3].isdigit() else 0,
                        'dmg_fixed_add': int(parts[4]) if parts[4].isdigit() else 0,
                        'dmg_multi': int(parts[5]) if parts[5].isdigit() else 0,
                        'dmg_multi_add': int(parts[6]) if parts[6].isdigit() else 0,
                        'area_type': int(parts[16]) if len(parts) > 16 and parts[16].isdigit() else 0,
                        'params': [int(parts[i]) if len(parts) > i and parts[i].isdigit() else 0 for i in range(17, 22)],
                        'adds': adds
                    }
        print(f"[EFF CONFIG LOADED] count={len(EFF_CONFIG)}")

    # Load SkillData
    skill_path = os.path.join(text_asset_root, "SkillData")
    if os.path.exists(skill_path):
        with open(skill_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) >= 30 and parts[1].isdigit():
                    sid = parts[1]
                    SKILL_CONFIG[sid] = {
                        'eff0': parts[24],
                        'eff1': parts[26] if len(parts) > 26 else "",
                        'eff2': parts[28] if len(parts) > 28 else "",
                        'cooldown_ms': int(parts[10]) if len(parts) > 10 and parts[10].isdigit() else 0,
                        'trace_distance_cm': int(parts[12]) if len(parts) > 12 and parts[12].isdigit() else 0,
                        'priority': int(parts[29]) if len(parts) > 29 and parts[29].isdigit() else 0
                    }
                    # Build reverse lookup: effId -> skillId
                    for eff_key in ('eff0', 'eff1', 'eff2'):
                        eff_id = SKILL_CONFIG[sid][eff_key]
                        if eff_id and eff_id not in EFF_TO_SKILL:
                            EFF_TO_SKILL[eff_id] = sid
        print(f"[SKILL CONFIG LOADED] count={len(SKILL_CONFIG)}")

    # Load BaseLvData for EXP requirements and stats
    lv_path = os.path.join(text_asset_root, "BaseLvData")
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
                            'res': [int(parts[10]), int(parts[17]), int(parts[24])],
                            'exd': [int(parts[25]), int(parts[25]), int(parts[25])],
                            'exr': [int(parts[26]), int(parts[26]), int(parts[26])],
                            'crd': [int(parts[27]), int(parts[27]), int(parts[27])],
                            'crr': [int(parts[28]), int(parts[28]), int(parts[28])],
                            'defa': int(parts[31]), 'dgea': int(parts[32]), 'resa': int(parts[33]),
                            'hita': int(parts[34]), 'cria': int(parts[35])
                        }
        print(f"[LEVEL TABLE LOADED] levels={len(LEVEL_DATA)}")

    # Load MapInfoData
    map_info_path = os.path.join(text_asset_root, "MapInfoData")
    if os.path.exists(map_info_path):
        with open(map_info_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 8 and parts[1].isdigit():
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
    conn_path = os.path.join(text_asset_root, "MapConnectInfoData")
    if os.path.exists(conn_path):
        with open(conn_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 6 and parts[1].isdigit():
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
    gc_path = os.path.join(text_asset_root, "GuildCaptureData")
    if os.path.exists(gc_path):
        with open(gc_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 2 and parts[1].isdigit():
                    GUILD_CAPTURE_DATA[parts[1]] = parts[2]
        print(f"[GUILD CAPTURE DATA LOADED] count={len(GUILD_CAPTURE_DATA)}")

    # Load NpcData
    npc_path = os.path.join(text_asset_root, "NpcData")
    if os.path.exists(npc_path):
        with open(npc_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 60 and parts[1].isdigit():
                    nid = parts[1]
                    lvl = int(parts[9]) if parts[9].isdigit() else 1
                    is_abs = "绝对值" in parts[12]
                    NPC_CONFIG[nid] = {
                        'name': parts[2],
                        'model': parts[4],
                        'level': lvl,
                        'type': int(parts[13]) if len(parts) > 13 and parts[13].isdigit() else 0,
                        'is_abs': is_abs,
                        'skill_group': parts[14] if len(parts) > 14 and parts[14] else '50001',
                        'buff': parts[15] if len(parts) > 15 and parts[15] else '',
                        'patrol_radius': int(parts[16]) if len(parts) > 16 and parts[16].isdigit() else 0,
                        'search_radius': int(parts[17]) if len(parts) > 17 and parts[17].isdigit() else 700,
                        'ai_type': parts[18] if len(parts) > 18 and parts[18] else '',
                        'ai_data_id': parts[19] if len(parts) > 19 and parts[19] else '',
                        'drop_id': parts[20] if len(parts) > 20 and parts[20] else '',
                        'walk_speed': int(parts[21]) if len(parts) > 21 and parts[21].isdigit() else 5,
                        # NpcData.MoveSpeed defaults to 50 in the APK when the table cell is blank.
                        'move_speed': int(parts[22]) if len(parts) > 22 and parts[22].isdigit() else 50,
                        'atk_coe': int(parts[26]) if len(parts) > 26 and parts[26].isdigit() else 10000,
                        'hp_coe': int(parts[27]) if len(parts) > 27 and parts[27].isdigit() else 10000,
                        'def_coe': int(parts[28]) if len(parts) > 28 and parts[28].isdigit() else 10000,
                        'hit_coe': int(parts[29]) if len(parts) > 29 and parts[29].isdigit() else 10000,
                        'eva_coe': int(parts[30]) if len(parts) > 30 and parts[30].isdigit() else 10000,
                        'cri_coe': int(parts[31]) if len(parts) > 31 and parts[31].isdigit() else 10000,
                        'res_coe': int(parts[32]) if len(parts) > 32 and parts[32].isdigit() else 10000,
                        'exd_coe': int(parts[33]) if len(parts) > 33 and parts[33].isdigit() else 10000,
                        'exr_coe': int(parts[34]) if len(parts) > 34 and parts[34].isdigit() else 10000,
                        'crd_coe': int(parts[35]) if len(parts) > 35 and parts[35].isdigit() else 10000,
                        'crr_coe': int(parts[36]) if len(parts) > 36 and parts[36].isdigit() else 10000,
                        'anti_stun_coe': int(parts[37]) if len(parts) > 37 and parts[37].isdigit() else 10000,
                        'anti_knock_down_coe': int(parts[38]) if len(parts) > 38 and parts[38].isdigit() else 10000,
                        'defa_coe': int(parts[39]) if len(parts) > 39 and parts[39].isdigit() else 10000,
                        'dgea_coe': int(parts[40]) if len(parts) > 40 and parts[40].isdigit() else 10000,
                        'resa_coe': int(parts[41]) if len(parts) > 41 and parts[41].isdigit() else 10000,
                        'hita_coe': int(parts[42]) if len(parts) > 42 and parts[42].isdigit() else 10000,
                        'cria_coe': int(parts[43]) if len(parts) > 43 and parts[43].isdigit() else 10000,
                        'atk_abs': int(parts[44]) if len(parts) > 44 and parts[44].isdigit() else 0,
                        'hp_abs': int(parts[45]) if len(parts) > 45 and parts[45].isdigit() else 0,
                        'def_abs': int(parts[46]) if len(parts) > 46 and parts[46].isdigit() else 0,
                        'hit_abs': int(parts[47]) if len(parts) > 47 and parts[47].isdigit() else 0,
                        'eva_abs': int(parts[48]) if len(parts) > 48 and parts[48].isdigit() else 0,
                        'cri_abs': int(parts[49]) if len(parts) > 49 and parts[49].isdigit() else 0,
                        'res_abs': int(parts[50]) if len(parts) > 50 and parts[50].isdigit() else 0,
                        'exd_abs': int(parts[51]) if len(parts) > 51 and parts[51].isdigit() else 0,
                        'exr_abs': int(parts[52]) if len(parts) > 52 and parts[52].isdigit() else 0,
                        'crd_abs': int(parts[53]) if len(parts) > 53 and parts[53].isdigit() else 0,
                        'crr_abs': int(parts[54]) if len(parts) > 54 and parts[54].isdigit() else 0,
                        'anti_stun_abs': int(parts[55]) if len(parts) > 55 and parts[55].isdigit() else 0,
                        'anti_knock_down_abs': int(parts[56]) if len(parts) > 56 and parts[56].isdigit() else 0,
                        'defa_abs': int(parts[57]) if len(parts) > 57 and parts[57].isdigit() else 0,
                        'dgea_abs': int(parts[58]) if len(parts) > 58 and parts[58].isdigit() else 0,
                        'resa_abs': int(parts[59]) if len(parts) > 59 and parts[59].isdigit() else 0,
                        'hita_abs': int(parts[60]) if len(parts) > 60 and parts[60].isdigit() else 0,
                        'cria_abs': int(parts[61]) if len(parts) > 61 and parts[61].isdigit() else 0,
                    }
        print(f"[NPC CONFIG LOADED] count={len(NPC_CONFIG)}")

    # Load AIData (AI behavior parameters for NPCs)
    ai_data_path = os.path.join(text_asset_root, "AIData")
    AI_DATA_CONFIG = {}
    if os.path.exists(ai_data_path):
        with open(ai_data_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 18 and parts[0] == "*" and parts[1].isdigit():
                    ai_id = parts[1]
                    AI_DATA_CONFIG[ai_id] = {
                        'lock_type': int(parts[2]) if parts[2].isdigit() else 0,
                        'lock_distance': int(parts[3]) if parts[3].isdigit() else 500,
                        'around_prob': int(parts[4]) if parts[4].isdigit() else 20,
                        'around_time_min': int(parts[5]) if parts[5].isdigit() else 1500,
                        'around_time_max': int(parts[6]) if parts[6].isdigit() else 3000,
                        'around_min_angle': int(parts[7]) if parts[7].isdigit() else -60,
                        'around_max_angle': int(parts[8]) if parts[8].isdigit() else 60,
                        'around_min_distance': int(parts[9]) if parts[9].isdigit() else 100,
                        'around_max_distance': int(parts[10]) if parts[10].isdigit() else 200,
                        'action_min_time': int(parts[11]) if parts[11].isdigit() else 5000,
                        'action_max_time': int(parts[12]) if parts[12].isdigit() else 6000,
                        'skill_cd_min': int(parts[13]) if parts[13].isdigit() else 1000,
                        'skill_cd_max': int(parts[14]) if parts[14].isdigit() else 3000,
                        'return_distance': int(parts[15]) if parts[15].isdigit() else 500,
                        'change_target_cd': int(parts[16]) if parts[16].isdigit() else 10000,
                        'patrol_type': int(parts[17]) if parts[17].isdigit() else 1,
                        'patrol_distance': int(parts[18]) if parts[18].isdigit() else 400,
                        'wait_time': int(parts[19]) if len(parts) > 19 and parts[19].isdigit() else 300,
                    }
        print(f"[AI DATA CONFIG LOADED] count={len(AI_DATA_CONFIG)}")

    # Override ALL map 11 mission monsters with proper absolute stats
    # These are percentage-based in NpcData with level 9999, which causes incorrect stats
    # Force absolute values matching level 1 single-player mission monsters
    # Covers 9901-9905 (mission targets), 9501-9505 (side missions), 9910-9916 (later missions)
    _map11_monsters = {
        '9901': {'name': 'Hulk', 'model': 'NPC_Nan_013', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9902': {'name': 'Hip Guy', 'model': 'NPC_Nan_022', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9903': {'name': 'PoliceMan', 'model': 'NPC_Nan_023', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9904': {'name': 'Beat Striker', 'model': 'NPC_Nan_036', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9905': {'name': 'Body Guard', 'model': 'NPC_Nan_017', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9501': {'name': 'Hulk', 'model': 'NPC_Nan_013', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9502': {'name': 'Hip Guy', 'model': 'NPC_Nan_022', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9503': {'name': 'PoliceMan', 'model': 'NPC_Nan_023', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9504': {'name': 'Beat Striker', 'model': 'NPC_Nan_036', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9505': {'name': 'The Pain', 'model': 'BOSS_Nan_006', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9910': {'name': 'Hulk', 'model': 'NPC_Nan_013', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9911': {'name': 'Hip Guy', 'model': 'NPC_Nan_022', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9912': {'name': 'PoliceMan', 'model': 'NPC_Nan_023', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9913': {'name': 'Beat Striker', 'model': 'NPC_Nan_036', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9914': {'name': 'Body Guard', 'model': 'NPC_Nan_017', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9915': {'name': 'Gentle Fighter', 'model': 'NPC_Nan_018', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9916': {'name': 'Gang Member', 'model': 'NPC_Nan_022', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        # Map 11 ambient civilians and auto-generated NPCs
        '2001': {'name': 'Civilian', 'model': 'NPC_Nan_002_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2003': {'name': 'Civilian', 'model': 'NPC_Nan_005_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2004': {'name': 'Civilian', 'model': 'NPC_Nan_017_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2005': {'name': 'Civilian', 'model': 'NPC_Nan_022_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2006': {'name': 'Police', 'model': 'NPC_Nan_023_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2007': {'name': 'Civilian', 'model': 'NPC_Nan_027_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2008': {'name': 'Civilian', 'model': 'NPC_Nan_029_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2009': {'name': 'Civilian', 'model': 'NPC_Nan_036_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2010': {'name': 'Civilian', 'model': 'NPC_Nv_002_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2011': {'name': 'Civilian', 'model': 'NPC_Nv_003_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2012': {'name': 'Civilian', 'model': 'NPC_Nv_004_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '2013': {'name': 'Civilian', 'model': 'NPC_Nv_005_Talk', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9906': {'name': 'Civilian', 'model': 'NPC_Nan_017', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9907': {'name': 'Civilian', 'model': 'NPC_Nan_005', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9908': {'name': 'Civilian', 'model': 'NPC_Nv_002_Mission', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9909': {'name': 'Police', 'model': 'NPC_Nan_047', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9917': {'name': 'Civilian', 'model': 'NPC_Nan_017', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9918': {'name': 'Civilian', 'model': 'NPC_Nan_005', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9919': {'name': 'Civilian', 'model': 'NPC_Nv_002_Mission', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9920': {'name': 'Fight Dog', 'model': 'NPC_Dog', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9921': {'name': 'Civilian', 'model': 'NPC_Nv_004', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '9922': {'name': 'Civilian', 'model': 'NPC_Nv_005', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        # Map 11 gang territory NPCs — percentage-based, scaled by player level (original NpcData)
        # Model: XD_A_WQ;XD_A_T;XD_A_S;XD_A_X, Size: 100, Group: 5
        # Level 9999 signals get_npc_attr() to use the player's level for stat scaling
        # All coefficients are 10000 (100%), stats scale via BaseLvData/AdaptData
        # Arena boss (mission 1003) - percentage-based, scales to player level
        # Original NpcData: 百分比 (percentage-based), level empty (defaults to 1), all coefficients 10000 (100%)
        # Level 9999 signals get_npc_attr() to use the player's level for stat scaling
        # This ensures the arena boss is always challenging regardless of player level
        '1105': {'name': '街区占领NPC', 'model': 'XD_A_WQ;XD_A_T;XD_A_S;XD_A_X', 'level': 9999, 'type': 0, 'is_abs': False, 'skill_group': '50001', 'atk_coe': 10000, 'hp_coe': 10000, 'def_coe': 10000, 'hit_coe': 10000, 'eva_coe': 10000, 'cri_coe': 10000, 'res_coe': 10000, 'exd_coe': 0, 'exr_coe': 0, 'crd_coe': 10000, 'crr_coe': 0, 'anti_stun_coe': 0, 'anti_knock_down_coe': 0, 'defa_coe': 10000, 'dgea_coe': 10000, 'resa_coe': 10000, 'hita_coe': 10000, 'cria_coe': 10000},
        '1106': {'name': '街区占领NPC', 'model': 'XD_A_WQ;XD_A_T;XD_A_S;XD_A_X', 'level': 9999, 'type': 0, 'is_abs': False, 'skill_group': '50001', 'atk_coe': 10000, 'hp_coe': 10000, 'def_coe': 10000, 'hit_coe': 10000, 'eva_coe': 10000, 'cri_coe': 10000, 'res_coe': 10000, 'exd_coe': 0, 'exr_coe': 0, 'crd_coe': 10000, 'crr_coe': 0, 'anti_stun_coe': 0, 'anti_knock_down_coe': 0, 'defa_coe': 10000, 'dgea_coe': 10000, 'resa_coe': 10000, 'hita_coe': 10000, 'cria_coe': 10000},
        '1107': {'name': '街区占领NPC', 'model': 'XD_A_WQ;XD_A_T;XD_A_S;XD_A_X', 'level': 9999, 'type': 0, 'is_abs': False, 'skill_group': '50001', 'atk_coe': 10000, 'hp_coe': 10000, 'def_coe': 10000, 'hit_coe': 10000, 'eva_coe': 10000, 'cri_coe': 10000, 'res_coe': 10000, 'exd_coe': 0, 'exr_coe': 0, 'crd_coe': 10000, 'crr_coe': 0, 'anti_stun_coe': 0, 'anti_knock_down_coe': 0, 'defa_coe': 10000, 'dgea_coe': 10000, 'resa_coe': 10000, 'hita_coe': 10000, 'cria_coe': 10000},
        '1108': {'name': '街区占领NPC', 'model': 'XD_A_WQ;XD_A_T;XD_A_S;XD_A_X', 'level': 9999, 'type': 0, 'is_abs': False, 'skill_group': '50001', 'atk_coe': 10000, 'hp_coe': 10000, 'def_coe': 10000, 'hit_coe': 10000, 'eva_coe': 10000, 'cri_coe': 10000, 'res_coe': 10000, 'exd_coe': 0, 'exr_coe': 0, 'crd_coe': 10000, 'crr_coe': 0, 'anti_stun_coe': 0, 'anti_knock_down_coe': 0, 'defa_coe': 10000, 'dgea_coe': 10000, 'resa_coe': 10000, 'hita_coe': 10000, 'cria_coe': 10000},
        # Map 11 additional civilians (1501-1512)
        '1501': {'name': 'William', 'model': 'NPC_Nan_008', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1502': {'name': 'White', 'model': 'NPC_Nan_012', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1503': {'name': 'Tomas.A', 'model': 'NPC_Nan_003', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1504': {'name': 'Mr.casino', 'model': 'NPC_Nan_012', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1505': {'name': 'Clain', 'model': 'NPC_Nan_012', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1506': {'name': 'Business man', 'model': 'NPC_Nan_012', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1507': {'name': 'Philip', 'model': 'NPC_Nan_012', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1508': {'name': 'Grant', 'model': 'NPC_Nan_012', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1509': {'name': 'Waiter', 'model': 'NPC_Nan_017', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1510': {'name': 'Jofors', 'model': 'NPC_Nan_019', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1511': {'name': 'Secret Owner', 'model': 'NPC_Nv_010', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
        '1512': {'name': 'Administrator', 'model': 'NPC_Nan_005', 'level': 1, 'atk_abs': 40, 'hp_abs': 1000, 'def_abs': 100, 'hit_abs': 2844, 'eva_abs': 129, 'cri_abs': 351, 'res_abs': 0, 'exd_abs': 0, 'exr_abs': 0, 'crd_abs': 15000, 'crr_abs': 0, 'anti_stun_abs': 0, 'anti_knock_down_abs': 0, 'defa_abs': 3158, 'dgea_abs': 6317, 'resa_abs': 3158, 'hita_abs': 316, 'cria_abs': 3158},
    }
    for _nid, _override in _map11_monsters.items():
        if _nid in NPC_CONFIG:
            # Always apply the override to ensure correct configuration
            # For percentage-based NPCs (is_abs=False), override the coefficients and level
            # For absolute NPCs (is_abs=True), override the absolute values
            if _override.get('is_abs', True):
                NPC_CONFIG[_nid].update(_override)
                NPC_CONFIG[_nid]['is_abs'] = True
            else:
                # Percentage-based: apply coefficient overrides and level
                for key in ['name', 'model', 'level', 'type', 'is_abs', 'skill_group']:
                    if key in _override:
                        NPC_CONFIG[_nid][key] = _override[key]
                for key in ['atk_coe', 'hp_coe', 'def_coe', 'hit_coe', 'eva_coe', 'cri_coe',
                            'res_coe', 'exd_coe', 'exr_coe', 'crd_coe', 'crr_coe',
                            'anti_stun_coe', 'anti_knock_down_coe', 'defa_coe', 'dgea_coe',
                            'resa_coe', 'hita_coe', 'cria_coe']:
                    if key in _override:
                        NPC_CONFIG[_nid][key] = _override[key]
        else:
            # Fallback: NpcData didn't load this NPC, create config from override
            NPC_CONFIG[_nid] = {
                'name': _override.get('name', f'NPC_{_nid}'),
                'model': _override.get('model', 'NPC_Default'),
                'level': _override.get('level', 1),
                'type': 0,
                'is_abs': _override.get('is_abs', True),
                'skill_group': _override.get('skill_group', '50001'),
            }
            if NPC_CONFIG[_nid]['is_abs']:
                # Absolute stats fallback
                NPC_CONFIG[_nid].update({
                    'atk_coe': 10000, 'hp_coe': 10000, 'def_coe': 10000, 'hit_coe': 10000,
                    'eva_coe': 10000, 'cri_coe': 10000, 'res_coe': 10000,
                    'exd_coe': 10000, 'exr_coe': 10000, 'crd_coe': 10000, 'crr_coe': 10000,
                    'anti_stun_coe': 10000, 'anti_knock_down_coe': 10000,
                    'defa_coe': 10000, 'dgea_coe': 10000, 'resa_coe': 10000, 'hita_coe': 10000, 'cria_coe': 10000,
                    'atk_abs': _override.get('atk_abs', 40), 'hp_abs': _override.get('hp_abs', 1000),
                    'def_abs': _override.get('def_abs', 100), 'hit_abs': _override.get('hit_abs', 2844),
                    'eva_abs': _override.get('eva_abs', 129), 'cri_abs': _override.get('cri_abs', 351),
                    'res_abs': _override.get('res_abs', 0), 'exd_abs': _override.get('exd_abs', 0),
                    'exr_abs': _override.get('exr_abs', 0), 'crd_abs': _override.get('crd_abs', 15000),
                    'crr_abs': _override.get('crr_abs', 0),
                    'anti_stun_abs': _override.get('anti_stun_abs', 0),
                    'anti_knock_down_abs': _override.get('anti_knock_down_abs', 0),
                    'defa_abs': _override.get('defa_abs', 3158), 'dgea_abs': _override.get('dgea_abs', 6317),
                    'resa_abs': _override.get('resa_abs', 3158), 'hita_abs': _override.get('hita_abs', 316),
                    'cria_abs': _override.get('cria_abs', 3158),
                })
            else:
                # Percentage-based fallback: use coefficients from override
                for key in ['atk_coe', 'hp_coe', 'def_coe', 'hit_coe', 'eva_coe', 'cri_coe',
                            'res_coe', 'exd_coe', 'exr_coe', 'crd_coe', 'crr_coe',
                            'anti_stun_coe', 'anti_knock_down_coe', 'defa_coe', 'dgea_coe',
                            'resa_coe', 'hita_coe', 'cria_coe']:
                    NPC_CONFIG[_nid][key] = _override.get(key, 10000)
    print(f"[MAP11 MONSTER OVERRIDES APPLIED] NPCs={list(_map11_monsters.keys())}")

    # Load MonsterData (and split into Monster vs Static NPC)
    mon_path = os.path.join(text_asset_root, "MonsterData")
    if os.path.exists(mon_path):
        with open(mon_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 6 and parts[1].isdigit():
                    mid = parts[1]
                    group = int(parts[2]) if parts[2].isdigit() else 0
                    nid = parts[3]
                    entry = {
                        'nid': nid,
                        'x': int(parts[4]),
                        'z': int(parts[5]),
                        'o': int(parts[6]),
                        'group': group,
                        'rebirth_type': int(parts[7]) if len(parts) > 7 and parts[7].lstrip('-').isdigit() else 0,
                        'rebirth_time': int(parts[8]) if len(parts) > 8 and parts[8].lstrip('-').isdigit() else -1,
                    }
                    if group == 9999:
                        if mid not in STATIC_NPC_DATA: STATIC_NPC_DATA[mid] = []
                        STATIC_NPC_DATA[mid].append(entry)
                    else:
                        if mid not in MONSTER_DATA: MONSTER_DATA[mid] = []
                        MONSTER_DATA[mid].append(entry)
        print(f"[MONSTER DATA LOADED] monsters_map={len(MONSTER_DATA)} static_npcs_map={len(STATIC_NPC_DATA)}")

    # Load DailyExpData
    DAILY_EXP_CONFIG = {}
    exp_path = os.path.join(text_asset_root, "DailyExpData")
    if os.path.exists(exp_path):
        with open(exp_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 6 and parts[1].isdigit():
                    cid = parts[1]
                    DAILY_EXP_CONFIG[cid] = {
                        'wave_count': int(parts[2]) if parts[2].isdigit() else 4,
                        'group_npc_count': [int(x) for x in parts[3].split("#") if x.isdigit()] or [5, 10, 15, 20],
                        'group_count': int(parts[4]) if parts[4].isdigit() else 7,
                        'group_time': int(parts[5]) if parts[5].isdigit() else 60,
                        'monsters': [x for x in parts[6].split("#") if x]
                    }
        print(f"[DAILY EXP CONFIG LOADED] count={len(DAILY_EXP_CONFIG)}")

    # Load KillTargetMissionData (Mission Spawns)
    # KillTargetMissionData rows are keyed by their own ID column.
    # Missions reference them via logic_id (MissionData column 9).
    # Header: *,ID,SceneID,PosX,PosZ,Range,NpcID,FlashNum,RequireNum
    kt_path = os.path.join(text_asset_root, "KillTargetMissionData")
    if os.path.exists(kt_path):
        with open(kt_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 7 and parts[1].isdigit():
                    row_id = parts[1]
                    # Skip duplicate 4-digit MissionID override rows (e.g., 1001 with FlashNum=4) in favor of 1-digit LogicIDs (e.g., 1 with FlashNum=2)
                    if int(row_id) >= 1000 and str(int(row_id) - 1000) in KILL_TARGET_SPAWNS:
                        continue
                    if row_id not in KILL_TARGET_SPAWNS: KILL_TARGET_SPAWNS[row_id] = []
                    flash_num = int(parts[7]) if parts[7].isdigit() else 1
                    require_num = int(parts[8]) if len(parts) > 8 and parts[8].isdigit() else flash_num
                    nid = parts[6]
                    KILL_TARGET_SPAWNS[row_id].append({
                        'map': parts[2],
                        'x': int(parts[3]),
                        'z': int(parts[4]),
                        'range': int(parts[5]) if len(parts) > 5 and parts[5].lstrip('-').isdigit() else 500,
                        'o': 0,
                        'nid': nid,
                        'num': flash_num,
                        'require': require_num
                    })
        print(f"[KILL TARGET DATA LOADED] count={len(KILL_TARGET_SPAWNS)}")

    # Load TargetCarMissionData
    tc_path = os.path.join(text_asset_root, "TargetCarMissionData")
    if os.path.exists(tc_path):
        with open(tc_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 6 and parts[1].isdigit():
                    row_id = parts[1]
                    if row_id not in TARGET_CAR_SPAWNS: TARGET_CAR_SPAWNS[row_id] = []
                    require_num = int(parts[7]) if len(parts) > 7 and parts[7].isdigit() else 1
                    TARGET_CAR_SPAWNS[row_id].append({
                        'map': parts[2],
                        'x': int(float(parts[3])),
                        'z': int(float(parts[4])),
                        'car_id': parts[6], # e.g. "Chevrolet"
                        'num': 1,
                        'require': require_num
                    })
        print(f"[TARGET CAR DATA LOADED] count={len(TARGET_CAR_SPAWNS)}")

    # Load MissionRequireData (Counts and NPC targets for generic kill/collect missions)
    mr_path = os.path.join(text_asset_root, "MissionRequireData")
    if os.path.exists(mr_path):
        with open(mr_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 5 and parts[0] == "*" and parts[1].isdigit():
                    req_id = parts[1]
                    MISSION_REQUIRE_DATA[req_id] = {
                        'name': parts[2],
                        'npc_name': parts[3],
                        'npc_id': parts[4],
                        'require_num': int(parts[5]) if parts[5].isdigit() else 1,
                        'item_id': parts[6] if len(parts) > 6 else ''
                    }
        print(f"[MISSION REQUIRE DATA LOADED] count={len(MISSION_REQUIRE_DATA)}")

    # Load MoveTargetMissionData (Arrive Target missions)
    mt_path = os.path.join(text_asset_root, "MoveTargetMissionData")
    if os.path.exists(mt_path):
        with open(mt_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 3 and parts[0] == "*" and parts[1].isdigit():
                    MOVE_TARGET_DATA[parts[1]] = {
                        'map_id': parts[2],
                        'target_num': int(parts[3]) if parts[3].isdigit() else 1,
                        'target_point': parts[4] if len(parts) > 4 else ''
                    }
        print(f"[MOVE TARGET DATA LOADED] count={len(MOVE_TARGET_DATA)}")

    # Load SurveyMissionData (Survey missions)
    sv_path = os.path.join(text_asset_root, "SurveyMissionData")
    if os.path.exists(sv_path):
        with open(sv_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 3 and parts[0] == "*" and parts[1].isdigit():
                    SURVEY_DATA[parts[1]] = {
                        'scene_id': parts[2],
                        'x': float(parts[4]) if len(parts) > 4 and parts[4] else 0.0,
                        'z': float(parts[6]) if len(parts) > 6 and parts[6] else 0.0,
                        'count': int(parts[8]) if len(parts) > 8 and parts[8].isdigit() else 1,
                        'survey_time': int(parts[9]) if len(parts) > 9 and parts[9].isdigit() else 1000,
                        'auto_refresh_time': int(parts[11]) if len(parts) > 11 and parts[11].isdigit() else 0,
                        'need_num': int(parts[12]) if len(parts) > 12 and parts[12].isdigit() else 1
                    }
        print(f"[SURVEY DATA LOADED] count={len(SURVEY_DATA)}")

    # Load ordered stops for MULTI_DELIVERY missions. Message 178 increments
    # one stop at a time, and message 182 may finish only after the final stop.
    md_path = os.path.join(text_asset_root, "MultiDeliveryMissionData")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 4 and parts[0] == "*" and parts[1].isdigit() and parts[2].isdigit():
                    MULTI_DELIVERY_DATA.setdefault(parts[1], []).append({
                        'index': int(parts[2]),
                        'npc_id': parts[3],
                        'map_id': parts[4],
                    })
        for stops in MULTI_DELIVERY_DATA.values():
            stops.sort(key=lambda stop: stop['index'])
        print(f"[MULTI DELIVERY DATA LOADED] count={len(MULTI_DELIVERY_DATA)}")

    # Universal Mission Requirements & Placements Enrichment
    for mid, m in missions_data.items():
        lt = m.get('logic_type', -1)
        lid = str(m.get('logic_id', ''))
        req = m.get('require_num', 1) or 1
        target = str(m.get('target_id', ''))
        placement = None

        if lt in (1, 14): # KILLMONSTER, LOCAL_KILL_MONSTER
            if lid in MISSION_REQUIRE_DATA:
                req = MISSION_REQUIRE_DATA[lid].get('require_num', req)
                if not target:
                    target = MISSION_REQUIRE_DATA[lid].get('npc_id', target)
        elif lt == 23: # KILL_TARGET_NPC
            if lid in KILL_TARGET_SPAWNS and KILL_TARGET_SPAWNS[lid]:
                kt = KILL_TARGET_SPAWNS[lid][0]
                target = kt.get('nid', target)
                req = kt.get('require', req)
                placement = kt
        elif lt == 24: # TARGET_ROB_CAR
            if lid in TARGET_CAR_SPAWNS and TARGET_CAR_SPAWNS[lid]:
                tc = TARGET_CAR_SPAWNS[lid][0]
                target = tc.get('car_id', target)
                req = tc.get('require', req)
                placement = tc
        elif lt in (3, 4, 15): # COLLECT / MONSTER DROP
            if lid in MISSION_REQUIRE_DATA:
                req = MISSION_REQUIRE_DATA[lid].get('require_num', req)
                if not target:
                    target = MISSION_REQUIRE_DATA[lid].get('npc_id', target)
        elif lt in (17, 18, 19, 20): # MASSACRE, DESTROY_CAR, ROB_CAR, IMPACT_NPC
            if lid in MISSION_REQUIRE_DATA:
                req = MISSION_REQUIRE_DATA[lid].get('require_num', req)
        elif lt == 21: # ARRIVE_TARGET
            if lid in MOVE_TARGET_DATA:
                req = MOVE_TARGET_DATA[lid].get('target_num', req)
        elif lt == 6: # SURVEY
            if lid in SURVEY_DATA:
                req = SURVEY_DATA[lid].get('need_num', req)
        elif lt == 7: # LEVEL_UP
            if lid.isdigit():
                req = int(lid)
        elif lt in (25, 131): # CAPTURE
            req = 1
            if not target:
                target = '1105'
        elif lt in (0, 2, 10, 11, 12, 13, 16) or lt >= 100:
            req = 1

        m['require_num'] = req
        m['target_id'] = target
        if placement:
            m['placement'] = placement
    print(f"[MISSION REQUIREMENTS ENRICHED] count={len(missions_data)}")

    # Load ALL vehicle definitions from MountData.
    # Verified against Decompiled/assets/Bundle/TextAsset/MountData header row:
    # ID(1), CarName(2), Desc(3), ItemID(4), Quality(5), Lv(6), Status1(7), Value1(8),
    # Status2(9), Value2(10), Status3(11), Value3(12), Status4(13), Value4(14),
    # ModelId(15), ShadowHeight(16), GetDesc(17), CarIcon(18), ModelPosX(19),
    # ModelPosY(20), ModelPosZ(21), MaxHP(22), ATK(23), MaxSpeed(24), MaxSteerAngle(25),
    # MaxAcceleration(26), BrakeAcceleration(27), ColorStr(28), DefaultColorId(29),
    # LightPosX(30), LightPosY(31), LightPosZ(32), NameHeight(33), StartTime(34),
    # EndTime(35), NeedShow(36), IsShowPlayer(37), GTALinkCarId(38), CamDis(39),
    # CamHeight(40), IsMotor(41), GetPath(42), PriceType(43), Price(44)
    #
    # NeedShow=1 vehicles go to the garage UI.  NeedShow=0 vehicles (motorcycles,
    # GTA traffic) are still loaded so the server can resolve their data when the
    # client spawns them.  IsMotor=1 marks motorcycles for correct animation handling.
    mount_path = os.path.join(text_asset_root, "MountData")
    if os.path.exists(mount_path):
        with open(mount_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 36 and parts[0] == "*" and parts[1]:
                    colors = [x for x in parts[28].split("#") if x]
                    is_motor = int(parts[41]) if len(parts) > 41 and parts[41].isdigit() else 0
                    MOUNT_CONFIG[parts[1]] = {
                        'colors': colors,
                        'default_color': parts[29] if parts[29] else (colors[0] if colors else "1"),
                        'item_id': parts[4],
                        'need_show': int(parts[36]) if parts[36].isdigit() else 0,
                        'is_motor': is_motor,
                        'model_id': parts[15] if len(parts) > 15 else '',
                        'gta_link_car_id': parts[38] if len(parts) > 38 and parts[38] else parts[1],
                        'price_type': int(parts[43]) if len(parts) > 43 and parts[43].lstrip('-').isdigit() else -1,
                        'price': int(parts[44]) if len(parts) > 44 and parts[44].isdigit() else 0
                    }
        garage_count = sum(1 for v in MOUNT_CONFIG.values() if v.get('need_show') == 1)
        motor_count = sum(1 for v in MOUNT_CONFIG.values() if v.get('is_motor') == 1)
        print(f"[MOUNT CONFIG LOADED] total={len(MOUNT_CONFIG)} garage={garage_count} motorcycles={motor_count}")

    # ColorData is the authority for garage paint prices. The client sends
    # only mountId/colorId in mount_use_color, so never trust a client price.
    color_path = os.path.join(text_asset_root, "ColorData")
    if os.path.exists(color_path):
        with open(color_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 4 and parts[0] == "*" and parts[1]:
                    if parts[3].lstrip('-').isdigit() and parts[4].lstrip('-').isdigit():
                        COLOR_CONFIG[parts[1]] = {
                            'price_type': int(parts[3]),
                            'price': int(parts[4])
                        }
        print(f"[COLOR CONFIG LOADED] colors={len(COLOR_CONFIG)}")

    # Only configured PVE world-boss IDs may enter the corresponding scene.
    wild_boss_path = os.path.join(text_asset_root, "WildBossData")
    if os.path.exists(wild_boss_path):
        with open(wild_boss_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 14 and parts[0] == "*" and parts[1]:
                    WILD_BOSS_CONFIG[parts[1]] = {
                        'map_id': parts[2],
                        'type': int(parts[5]) if parts[5].isdigit() else 0,
                        'min_level': int(parts[13]) if parts[13].isdigit() else 1,
                        'max_level': int(parts[14]) if parts[14].isdigit() else 80,
                    }
        print(f"[WILD BOSS CONFIG LOADED] count={len(WILD_BOSS_CONFIG)}")

    # Daily-copy UI receives its state from TAG 555.  Keep the IDs/types in
    # lockstep with CopySceneData so client tutorials can locate their target.
    # parts[11] is the state field: "1"=daily copies (subtype 1), "2"=exp stage copies (subtype 2).
    # Both must be loaded so the client can display all available copy scenes.
    copy_path = os.path.join(text_asset_root, "CopySceneData")
    if os.path.exists(copy_path):
        with open(copy_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 20 and parts[0] == "*" and parts[1].isdigit() and parts[11] in ("1", "2"):
                    COPY_SCENE_CONFIG[parts[1]] = {
                        'map_id': parts[5],
                        'subtype': int(parts[12]) if parts[12].isdigit() else 0,
                        'exist_time': int(parts[13]) if parts[13].isdigit() else 0,
                        'time_inc': parts[8] if len(parts) > 8 else '',
                        'end_time': int(parts[10]) if parts[10].isdigit() else 0,
                        'max_plays': int(parts[18]) if parts[18].isdigit() else 0,
                        'min_level': int(parts[19]) if parts[19].isdigit() else 1,
                        'max_level': int(parts[20]) if len(parts) > 20 and parts[20].isdigit() else 80,
                        'min_member': int(parts[21]) if len(parts) > 21 and parts[21].isdigit() else 1,
                        'max_member': int(parts[22]) if len(parts) > 22 and parts[22].isdigit() else 4,
                        # EXP single-player entry uses CopySceneData.SingleMapID
                        # as the physical scene while retaining the selected
                        # copy ID for DailyExpData, rewards, and progress.
                        'single_map_id': parts[31] if len(parts) > 31 and parts[31].isdigit() else ''
                    }
        print(f"[COPY SCENE CONFIG LOADED] daily_copies={len(COPY_SCENE_CONFIG)}")

    # Street Race's actual reward preview is resolved by AdaptData's
    # _drop_bc key, then ShowRewardData.  Read those client tables rather
    # than inventing rewards in the server.
    show_reward_path = os.path.join(text_asset_root, "ShowRewardData")
    if os.path.exists(show_reward_path):
        with open(show_reward_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(',')
                if len(parts) >= 5 and parts[0] == '*' and parts[1].isdigit():
                    rewards = []
                    for index in range(3, len(parts) - 2, 3):
                        item_id = parts[index].strip()
                        quality = int(parts[index + 1]) if parts[index + 1].isdigit() else 0
                        count_str = parts[index + 2].strip()
                        count = int(count_str) if count_str.isdigit() else 1

                        if item_id:
                            rewards.append((item_id, quality, count))
                        elif count_str and count_str in ITEM_CONFIG:
                            rewards.append((count_str, quality, 1))
                    SHOW_REWARD_CONFIG[parts[1]] = rewards
        print(f"[SHOW REWARD CONFIG LOADED] count={len(SHOW_REWARD_CONFIG)}")

    adapt_path = os.path.join(text_asset_root, "AdaptData")
    if os.path.exists(adapt_path):
        with open(adapt_path, "r", encoding='utf-8') as f:
            rows = [line.strip().split(',') for line in f if line.strip()]
        header = next((row for row in rows if len(row) > 1 and row[0] == '*' and row[1] == 'ID'), [])
        try:
            street_reward_index = header.index('_drop_bc')
        except ValueError:
            street_reward_index = -1
        for parts in rows:
            if len(parts) > 12 and parts[0] == '*' and parts[1].isdigit():
                lv = int(parts[1])
                if street_reward_index >= 0 and len(parts) > street_reward_index:
                    STREET_RACE_REWARD_BY_LEVEL[lv] = parts[street_reward_index]
                ADAPT_DATA[lv] = {
                    'atk': int(parts[2]) if parts[2].isdigit() else 40,
                    'hp': int(parts[3]) if parts[3].isdigit() else 1000,
                    'def': int(parts[4]) if parts[4].isdigit() else 100,
                    'hit': int(parts[5]) if parts[5].isdigit() else 2844,
                    'dge': int(parts[6]) if parts[6].isdigit() else 129,
                    'cri': int(parts[7]) if parts[7].isdigit() else 351,
                    'res': int(parts[8]) if parts[8].isdigit() else 0,
                    'exd': int(parts[9]) if parts[9].isdigit() else 0,
                    'exr': int(parts[10]) if parts[10].isdigit() else 0,
                    'crd': int(parts[11]) if parts[11].isdigit() else 15000,
                    'crr': int(parts[12]) if parts[12].isdigit() else 0,
                    'defa': int(parts[15]) if len(parts) > 15 and parts[15].isdigit() else 3158,
                    'dgea': int(parts[16]) if len(parts) > 16 and parts[16].isdigit() else 6317,
                    'resa': int(parts[17]) if len(parts) > 17 and parts[17].isdigit() else 3158,
                    'hita': int(parts[18]) if len(parts) > 18 and parts[18].isdigit() else 316,
                    'cria': int(parts[19]) if len(parts) > 19 and parts[19].isdigit() else 3158,
                }
        print(f"[ADAPT DATA LOADED] count={len(ADAPT_DATA)}")
        # Log street race rewards for verification
        if STREET_RACE_REWARD_BY_LEVEL:
            sample_levels = sorted(STREET_RACE_REWARD_BY_LEVEL.keys())[:5]
            for lv in sample_levels:
                reward_id = STREET_RACE_REWARD_BY_LEVEL[lv]
                rewards = SHOW_REWARD_CONFIG.get(str(reward_id), [])
                print(f"[STREET RACE] Level {lv} -> Reward ID {reward_id}: {rewards}")

    item_path = os.path.join(text_asset_root, "ItemData")
    if os.path.exists(item_path):
        with open(item_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(',')
                if len(parts) > 11 and parts[0] == '*' and parts[1]:
                    ITEM_CONFIG[parts[1]] = {
                        'type': int(parts[7]) if parts[7].isdigit() else 0,
                        'function': int(parts[11]) if parts[11].isdigit() else 0
                    }
        print(f"[ITEM CONFIG LOADED] items={len(ITEM_CONFIG)}")

    # Load FunctionData (feature unlock gates by level)
    func_path = os.path.join(text_asset_root, "FunctionData")
    if os.path.exists(func_path):
        with open(func_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 8 and parts[0] == "*" and parts[1] and parts[1] not in ("ID",):
                    fid = parts[1]
                    FUNCTION_DATA[fid] = {
                        'class': int(parts[4]) if len(parts) > 4 and parts[4].isdigit() else 0,
                        'condition': int(parts[6]) if len(parts) > 6 and parts[6].isdigit() else 0,
                        'is_download': int(parts[7]) if len(parts) > 7 and parts[7].isdigit() else 0,
                        'first_open': int(parts[8]) if len(parts) > 8 and parts[8].isdigit() else 0,
                        'unlock_type': int(parts[10]) if len(parts) > 10 and parts[10].isdigit() else 0,
                        'side_mission': parts[13] if len(parts) > 13 else ''
                    }
        print(f"[FUNCTION DATA LOADED] count={len(FUNCTION_DATA)}")

    # Load EquipData (equipment definitions)
    equip_path = os.path.join(text_asset_root, "EquipData")
    if os.path.exists(equip_path):
        with open(equip_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 16 and parts[0] == "*" and parts[1] and parts[1] not in ("ID",):
                    eid = parts[1]
                    EQUIP_CONFIG[eid] = {
                        'name': parts[2],
                        'lv': int(parts[3]) if parts[3].isdigit() else 0,
                        'class': int(parts[4]) if parts[4].isdigit() else 1,
                        'job': int(parts[6]) if parts[6].lstrip('-').isdigit() else -1,
                        'position': int(parts[7]) if parts[7].isdigit() else 0,
                        'base_stat': int(parts[8]) if parts[8].isdigit() else 0,
                        'base_val': int(parts[9]) if parts[9].isdigit() else 0,
                        'stat1': int(parts[10]) if len(parts) > 10 and parts[10].isdigit() else 0,
                        'stat1_val': int(parts[11]) if len(parts) > 11 and parts[11].isdigit() else 0,
                        'stat2': int(parts[12]) if len(parts) > 12 and parts[12].isdigit() else 0,
                        'stat2_val': int(parts[13]) if len(parts) > 13 and parts[13].isdigit() else 0,
                        'model': parts[16] if len(parts) > 16 else '',
                        'base_skills': parts[23].split('#') if len(parts) > 23 and parts[23] else [],
                        'weapon_type': int(parts[24]) if len(parts) > 24 and parts[24].isdigit() else 0
                    }
        print(f"[EQUIP CONFIG LOADED] count={len(EQUIP_CONFIG)}")

    # Load DownloadRewardData (expansion download rewards)
    dl_path = os.path.join(text_asset_root, "DownloadRewardData")
    if os.path.exists(dl_path):
        with open(dl_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 4 and parts[0] == "*" and parts[1] and parts[1] not in ("ID",):
                    # Parse triplets: ItemID, ItemCount, Quality
                    for i in range(2, len(parts) - 2, 3):
                        item_id = parts[i].strip()
                        count = int(parts[i+1]) if i+1 < len(parts) and parts[i+1].strip().isdigit() else 0
                        quality = int(parts[i+2]) if i+2 < len(parts) and parts[i+2].strip().isdigit() else 0
                        if item_id and count > 0 and item_id in ITEM_CONFIG:
                            DOWNLOAD_REWARD_DATA.append((item_id, count, quality))
                        elif item_id and count > 0 and item_id not in ITEM_CONFIG:
                            print(f"[WARN] Download reward item {item_id} not found in ItemData, skipping")
        print(f"[DOWNLOAD REWARD LOADED] items={len(DOWNLOAD_REWARD_DATA)}")

    # Load SkillupgradeData (skill upgrade costs by level)
    sku_path = os.path.join(text_asset_root, "SkillupgradeData")
    if os.path.exists(sku_path):
        with open(sku_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 3 and parts[0] == "*" and parts[1].isdigit():
                    lv = int(parts[1])
                    SKILL_UPGRADE_DATA[lv] = {
                        'price_type': int(parts[2]) if parts[2].isdigit() else 0,
                        'price_value': int(parts[3]) if parts[3].isdigit() else 0
                    }
        print(f"[SKILL UPGRADE DATA LOADED] levels={len(SKILL_UPGRADE_DATA)}")

    # Load RelifeData (respawn tier config)
    relife_path = os.path.join(text_asset_root, "RelifeData")
    if os.path.exists(relife_path):
        with open(relife_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 4 and parts[0] == "*" and parts[1].isdigit():
                    RELIFE_DATA.append({
                        'id': int(parts[1]),
                        'min_count': int(parts[2]) if parts[2].isdigit() else 0,
                        'max_count': int(parts[3]) if parts[3].isdigit() else 999999,
                        'use_count': int(parts[4]) if parts[4].isdigit() else 1
                    })
        RELIFE_DATA.sort(key=lambda x: x['min_count'])
        print(f"[RELIFE DATA LOADED] tiers={len(RELIFE_DATA)}")

    # Load ServerData (server list definitions)
    srv_path = os.path.join(text_asset_root, "ServerData")
    if os.path.exists(srv_path):
        with open(srv_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 15 and parts[0] == "*" and parts[1].isdigit():
                    SERVER_DATA_LIST.append({
                        'id': int(parts[1]),
                        'name': parts[2],
                        'state': int(parts[3]) if parts[3].isdigit() else 0,
                        'area': int(parts[4]) if parts[4].isdigit() else 0,
                        'timezone': int(parts[5]) if parts[5].lstrip('-').isdigit() else 0,
                        'db_local': int(parts[6]) if parts[6].isdigit() else 0,
                        'new_char': int(parts[7]) if parts[7].isdigit() else 0,
                        'server_list': parts[8],
                        'display_name': parts[9],
                        'ip': parts[10],
                        'port': int(parts[11]) if parts[11].isdigit() else 9555,
                        'rank': int(parts[12]) if parts[12].isdigit() else 0,
                        'weight': int(parts[13]) if parts[13].isdigit() else 1,
                        'new_server': int(parts[14]) if parts[14].isdigit() else 0,
                        'player_state': int(parts[15]) if parts[15].lstrip('-').isdigit() else -4,
                    })
        print(f"[SERVER DATA LOADED] servers={len(SERVER_DATA_LIST)}")

    # Load ConfigData (global game configuration)
    cfg_path = os.path.join(text_asset_root, "ConfigData")
    if os.path.exists(cfg_path):
        with open(cfg_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 4 and parts[0] == "*" and parts[1] and parts[1] not in ("Key",):
                    key = parts[1]
                    val_type = int(parts[3]) if parts[3].isdigit() else 0
                    raw_val = parts[4]
                    if val_type == 0:  # float
                        try: GAME_CONFIG[key] = float(raw_val)
                        except: GAME_CONFIG[key] = raw_val
                    else:  # int
                        try: GAME_CONFIG[key] = int(raw_val)
                        except: GAME_CONFIG[key] = raw_val
        print(f"[GAME CONFIG LOADED] keys={len(GAME_CONFIG)}")

except: traceback.print_exc()

BAK_DB = CHAR_DB + ".bak"
TMP_DB = CHAR_DB + ".tmp"

def load_chars():
    """Crash-safe character loader with automatic backup recovery and key normalization."""
    data = None
    if os.path.exists(CHAR_DB):
        try:
            with open(CHAR_DB, "r", encoding="utf-8") as f:
                raw_data = json.load(f)
                if isinstance(raw_data, dict):
                    data = raw_data
        except Exception as e:
            print(f"[WARN] Failed to load {CHAR_DB}: {e}")

    if data is None and os.path.exists(BAK_DB):
        try:
            with open(BAK_DB, "r", encoding="utf-8") as f:
                raw_data = json.load(f)
                if isinstance(raw_data, dict):
                    print(f"[RECOVERY] Restored character database from {BAK_DB}!")
                    data = raw_data
                    try:
                        with open(CHAR_DB, "w", encoding="utf-8") as out:
                            json.dump(data, out, indent=4)
                    except: pass
        except Exception as e:
            print(f"[WARN] Failed to load backup {BAK_DB}: {e}")

    if not isinstance(data, dict):
        return {}

    # Normalize area keys to string so int vs str JSON key lookups always match
    normalized = {}
    for area_k, acc_dict in data.items():
        if isinstance(acc_dict, dict):
            normalized[str(area_k)] = acc_dict
    return normalized

def get_account_chars(all_chars, area_id, acc_id):
    """Safely retrieves character list for account across int/str area_id keys."""
    if not isinstance(all_chars, dict):
        return []
    area_key = str(area_id)
    acc_dict = all_chars.get(area_key)
    if acc_dict is None and isinstance(area_id, int):
        acc_dict = all_chars.get(area_id)
    if isinstance(acc_dict, dict):
        return acc_dict.get(acc_id, [])
    return []

def _json_default_handler(obj):
    """Custom JSON default handler to skip non-serializable objects like Timer, set, lock."""
    if isinstance(obj, threading.Timer):
        return None  # Skip Timer objects — they are runtime-only
    if isinstance(obj, set):
        return list(obj)  # Convert sets to lists
    if isinstance(obj, (threading.Lock, type(threading.Lock()))):
        return None  # Skip lock objects
    if hasattr(obj, '__dict__'):
        return obj.__dict__  # Fallback: serialize object attributes
    return str(obj)  # Last resort: convert to string

def _serializable_character_data(value, seen=None):
    """Copy save data while omitting live EXP state and breaking object cycles."""
    if seen is None:
        seen = set()
    if isinstance(value, dict):
        marker = id(value)
        if marker in seen:
            return None
        seen.add(marker)
        result = {
            key: _serializable_character_data(item, seen)
            for key, item in value.items()
            if key != 'exp_stage_state' and not isinstance(item, threading.Timer)
        }
        seen.remove(marker)
        return result
    if isinstance(value, (list, tuple, set)):
        marker = id(value)
        if marker in seen:
            return None
        seen.add(marker)
        result = [_serializable_character_data(item, seen) for item in value if not isinstance(item, threading.Timer)]
        seen.remove(marker)
        return result
    return value

def save_chars(data):
    """Serialize concurrent writers before using the shared atomic-save files."""
    with CHAR_DB_SAVE_LOCK:
        _save_chars_atomic(data)

def _save_chars_atomic(data):
    """Crash-safe atomic writer to prevent character loss during kill -9."""
    if not isinstance(data, dict):
        return
    # Guard against accidental wipe: never overwrite a non-empty database on disk with an empty dict
    if not data:
        if os.path.exists(CHAR_DB) and os.path.getsize(CHAR_DB) > 10:
            print("[SAVE GUARD] Refusing to overwrite non-empty CHAR_DB with empty dictionary!")
            return
    try:
        # 1. Write new state to temporary file
        with open(TMP_DB, "w", encoding="utf-8") as f:
            json.dump(_serializable_character_data(data), f, indent=4, default=_json_default_handler)
            f.flush()
            os.fsync(f.fileno())

        # 2. Backup current good database file if it exists
        if os.path.exists(CHAR_DB) and os.path.getsize(CHAR_DB) > 10:
            try:
                with open(CHAR_DB, "r", encoding="utf-8") as src, open(BAK_DB, "w", encoding="utf-8") as dst:
                    dst.write(src.read())
                    dst.flush()
                    os.fsync(dst.fileno())
            except: pass

        # 3. Atomic rename guarantees either old file or new file exists intact
        os.replace(TMP_DB, CHAR_DB)
    except Exception as e:
        print(f"[ERROR] Failed atomic save_chars: {e}")

all_accounts_chars = load_chars()

# Initialize MAIL_ID_COUNTER from max existing mailId to avoid collisions after restart
for area_key, area_chars in all_accounts_chars.items():
    for acc_key, char_list in area_chars.items():
        for ch in char_list:
            for mail in ch.get('mails', []):
                if isinstance(mail, dict):
                    mid = mail.get('mailId', 0)
                    if mid >= MAIL_ID_COUNTER:
                        MAIL_ID_COUNTER = mid + 1
print(f"[MAIL] MAIL_ID_COUNTER initialized to {MAIL_ID_COUNTER}")

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

def get_val_str(fields, tag, default=''):
    """Extract a string value from sproto fields."""
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, str): return val
    if isinstance(val, (bytes, bytearray)): return val.decode('utf-8', errors='ignore')
    return str(val) if val is not None else default

def get_online_characters():
    """Return a list of all currently online character dicts."""
    return [ch for conn, ch in ALL_CONNECTIONS.values() if ch is not None]

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
                elif val and isinstance(val[0], bytes):
                    # List of sproto objects (e.g., list of encoded friend_info)
                    items = []
                    for item in val:
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)): pass
                        else: item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                # Sproto map: client reads as array of length-prefixed values only.
                # Keys are extracted from each value using a callback, not encoded on wire.
                # Format: len(val1)+val1+len(val2)+val2+...
                items = []
                for k, item in val.items():
                    if isinstance(item, (bytes, bytearray)):
                        val_data = item
                    elif isinstance(item, dict):
                        val_data = encode_sproto(list(item.items()))
                    elif isinstance(item, bool):
                        val_data = b'\x01' if item else b'\x00'
                    elif isinstance(item, int):
                        val_data = struct.pack("<q", item)
                    elif isinstance(item, str):
                        val_data = item.encode('utf-8')
                    else:
                        val_data = b''
                    items.append(struct.pack("<I", len(val_data)) + val_data)
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

def decode_sproto_list(data):
    """Decodes a Sproto array of objects from raw bytes."""
    if not data: return []
    res = []
    ptr = 0
    while ptr < len(data):
        if ptr + 4 > len(data): break
        l = struct.unpack("<I", data[ptr:ptr+4])[0]
        res.append(data[ptr+4 : ptr+4+l])
        ptr += 4 + l
    return res

def get_visual(name, prof, mount_id='', mount_color='', mount_state=0):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    fields = [(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]),
              (4, v["l"]), (5, v["w"]), (10, 0)]
    # characterVisual uses protocol tags 12-14 for the selected vehicle.
    # MountId is required by the Street Race UI even while the player is not
    # currently driving in the city (mount_state == 0).
    if mount_id:
        fields.extend([(12, str(mount_id)), (13, int(mount_state)),
                       (14, str(mount_color or ''))])
    return encode_sproto(fields)

def get_equipped_mount(c):
    """Return the APK garage vehicle currently equipped by this character."""
    mounts = c.get('mounts', {})
    preferred = str(c.get('equipped_mount_id', ''))
    if preferred and int(mounts.get(preferred, {}).get('state', 0)) == 2:
        mount_id = preferred
    else:
        mount_id = next((str(mid) for mid, state in mounts.items()
                         if int(state.get('state', 0)) == 2), '')
    if not mount_id:
        return '', '', 0
    cfg = MOUNT_CONFIG.get(mount_id, {})
    saved = mounts.get(mount_id, {})
    color = str(saved.get('select') or cfg.get('default_color', ''))
    return mount_id, color, 1 if c.get('mount_riding', False) else 0

def build_main_player_visual(c):
    mount_id, mount_color, mount_state = get_equipped_mount(c)
    return get_visual(c.get('name', 'Hero'), c.get('prof', 0),
                      mount_id, mount_color, mount_state)

def sync_main_player_visual(picked_char, send_rpc_push):
    """Push the normal APK AOI visual update after a garage change."""
    character = encode_sproto([
        (0, int(picked_char['id'])),
        (4, build_main_player_visual(picked_char))
    ])
    send_rpc_push(510, encode_sproto([(0, character)]))

def get_boss_char(inst_id, did, player_level=1):
    # Domin 1 boss stats and visual (XD profession)
    # Boss scales to player level — the arena challenge adapts to the player's strength.
    # NpcData 1105: percentage-based (百分比), all coefficients 100%, skill group defaults to 50001.
    # Model: XD_A_WQ;XD_A_T;XD_A_S;XD_A_X (XD profession actions).
    # The boss uses XD profession skills: 3 basic attacks, dodge, and 3 active skills.
    name = "Ash Viper"
    prof = 0

    # Boss scales to player level for consistent arena challenge.
    # NPC 1105 is percentage-based with level 9999, so get_npc_attr uses the player's level.
    # At player level 21: atk=467, hp=7082, def=541 → power = 61,515
    boss_stats = get_npc_attr("1105", player_level)
    lv = boss_stats['lv']
    hp_max = boss_stats['hp_max']
    power = boss_stats['power']
    atk = boss_stats['atk']
    df = boss_stats['def']

    # Visual
    v = get_visual(name, prof)

    # attribute_other fields are decoded by ObjZombiePlayer. A non-zero
    # title_level (4) makes PlayerHeadInfoLogic render the overhead title.
    # TitleData defines level 1 as a valid title.
    attr_oth = encode_sproto([
        (0, hp_max), (1, 0), (2, lv), (3, power), (4, 1), (15, 2)
    ])

    # Movement: Restore Y=120. Client docking raycasts from Y=150 down.
    pos_data = encode_sproto([(0, 400), (1, 120), (2, 0), (3, -9000)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])

    # Boss uses XD profession skills for authentic arena enemy behavior.
    # ObjZombiePlayer AI uses skills with indexPos 4-6 for auto-combat,
    # sorted by PriorityAutoCombat descending:
    #   110 (斗志, priority 5) -> used first
    #   105 (旋风斩, priority 4) -> used second
    #   106 (棒球, priority 3) -> used third
    # Dodge (104) at indexPos 3 is available for evasion.
    skills_map = {}
    # Basic attack combo chain (required for combo attacks)
    atk_skills = ["101", "102", "103"]
    for sid in atk_skills:
        skills_map[sid] = encode_sproto([(0, sid), (1, 0), (2, 0), (3, 1), (4, 0), (5, False)])
    # Dodge skill at indexPos 3
    skills_map["104"] = encode_sproto([(0, "104"), (1, 0), (2, 3), (3, 1), (4, 1), (5, False)])
    # Active skill 1: 110 (斗志) at indexPos 4 - highest priority auto-fight
    skills_map["110"] = encode_sproto([
        (0, "110"), (1, 1), (2, 4), (3, 1), (4, 2), (5, False)
    ])
    # Active skill 2: 105 (旋风斩) at indexPos 5
    skills_map["105"] = encode_sproto([
        (0, "105"), (1, 1), (2, 5), (3, 1), (4, 3), (5, False)
    ])
    # Active skill 3: 106 (棒球) at indexPos 6
    skills_map["106"] = encode_sproto([
        (0, "106"), (1, 1), (2, 6), (3, 1), (4, 4), (5, False)
    ])

    # Runtime: attribute(6), attribute_all(7)
    attr_run = encode_sproto([(0, hp_max), (2, atk), (3, df)])

    # Use boss_stats for all attributes to ensure consistency
    attr_all_data = [
        (0, hp_max), (2, atk), (3, df),
        (4, boss_stats['hit']), (5, boss_stats['eva']), (6, boss_stats['cri']), (7, boss_stats['res']),
        (8, boss_stats['exd']), (9, boss_stats['exr']), (10, boss_stats['crd']), (11, boss_stats['crr']),
        (12, boss_stats['defa']), (13, 500), (14, 100),
        (17, boss_stats['dgea']), (18, boss_stats['resa']), (19, boss_stats['hita']), (20, boss_stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    return encode_sproto([
        (0, inst_id),
        (1, encode_sproto([(0, name), (1, prof), (2, 1), (3, "502"), (4, 1)])), # general
        (2, attr_oth),
        (6, v),
        (7, mv),
        (8, skills_map),
        (13, run)
    ])

# Skill System Constants
# Starter skills per profession: 3 basic attacks + 1 dodge + 1 active skill
# Additional active skills are granted by weapon skins (LabelID system), not by level
PROF_SKILLS = {
    0: {"atk": ["101", "102", "103"], "dodge": "104", "starter_active": "105"},
    1: {"atk": ["201", "202", "203"], "dodge": "204", "starter_active": "205"},
    2: {"atk": ["301", "302", "303"], "dodge": "304", "starter_active": "305"}
}

def get_skill_upgrade_cost(lv):
    if lv < 0: return 0
    # Data-driven: read from SkillupgradeData TextAsset
    if SKILL_UPGRADE_DATA:
        entry = SKILL_UPGRADE_DATA.get(lv)
        if entry:
            return entry['price_value']
        # Level beyond data table
        max_lv = max(SKILL_UPGRADE_DATA.keys())
        if lv > max_lv:
            return SKILL_UPGRADE_DATA[max_lv]['price_value']
        return 0
    # Fallback: original hardcoded formula
    if lv < 15: return (lv + 1) * 10000
    if lv < 24: return (lv - 13) * 100000 + 100000
    if lv == 24: return 3000000
    if lv == 25: return 7000000
    if lv == 26: return 18000000
    return 20000000

def build_skills_map(prof, char_level, skill_levels=None):
    """Build the skill dictionary sent to the client via sync_skill_info (tag 540).
    Sends starter skills: 3 basic attacks + 1 dodge + 1 active skill.
    If skill_levels contains additional skills (e.g., boss skills), include them too.
    Additional active skills are granted by weapon skins (LabelID system), not by level."""
    prof = int(prof)
    if skill_levels is None: skill_levels = {}
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    smap = {}

    # Basic attack combo chain (101, 102, 103 / 201, 202, 203 / 301, 302, 303)
    atk_skills = p["atk"] if isinstance(p["atk"], list) else [p["atk"]]
    for sid in atk_skills:
        smap[sid] = encode_sproto([(0, sid), (1, skill_levels.get(sid, 0)), (2, 0), (3, 1), (4, 0), (5, False)])

    # Dodge / Roll (104 / 204 / 304) - Index 3
    smap[p["dodge"]] = encode_sproto([(0, p["dodge"]), (1, skill_levels.get(p["dodge"], 0)), (2, 3), (3, 1), (4, 1), (5, False)])

    # Starter active skill (105 / 205 / 305) - Index 4 (first active skill slot)
    starter_active = p["starter_active"]
    smap[starter_active] = encode_sproto([
        (0, starter_active),
        (1, skill_levels.get(starter_active, 0)),
        (2, 4),       # indexPos - first active skill slot
        (3, 1),       # unlockLevel - available from level 1
        (4, 2),       # indexPos2 - skill bar position
        (5, False)    # disable - not disabled
    ])

    # Include any additional skills specified in skill_levels (e.g., boss skills 106-110)
    # The client's ObjZombiePlayer.UpdateSkillList() only accepts indexPos 4, 5, 6
    # (line 114: index > 3 && index < 7), so only 3 active skills can be used by zombie AI.
    # The client then sorts mEnableSkillIDList by PriorityAutoCombat (descending):
    #   110(priority=5) -> used first
    #   105(priority=4) -> used second
    #   106(priority=3) -> used third
    #   107/108/109(priority=2) -> not in mEnableSkillIDList if >3 skills
    # We need to assign indexPos 4-6 to the skills with the highest PriorityAutoCombat.
    # Collect all extra skills and sort by PriorityAutoCombat descending.
    extra_skills = []
    for sid, slv in skill_levels.items():
        if sid not in smap:
            skill_cfg = SKILL_CONFIG.get(sid, {})
            priority = int(skill_cfg.get('priority', 0))
            extra_skills.append((sid, slv, priority))
    # Sort by priority descending so highest priority gets indexPos 4
    extra_skills.sort(key=lambda x: x[2], reverse=True)
    for idx, (sid, slv, priority) in enumerate(extra_skills):
        if idx < 3:
            skill_idx = 4 + idx  # 4, 5, 6 (zombie AI usable)
        else:
            skill_idx = 7 + idx  # 7, 8, ... (won't be used by zombie AI)
        smap[sid] = encode_sproto([
            (0, sid),
            (1, slv),
            (2, skill_idx),   # indexPos
            (3, 1),           # unlockLevel
            (4, skill_idx),   # indexPos2
            (5, False)        # disable
        ])

    return smap

def get_general(c):
    return encode_sproto([
        (0, c.get('name', 'Hero')),
        (1, c.get('prof', 0)),
        (2, 1),
        (3, str(c.get('map_id', '11'))),
        # general.tutorial: 0 runs the APK's first tutorial; 1 means finished.
        (4, c.get('tutorial', 0))
    ])

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_runtime_aoi(c):
    """Build the runtime field (character_aoi field 6) for aoi_add packets.
    Matches the client's ObjInitPlayerData.InitData expectation:
    runtime.attribute (tag 6) and runtime.attribute_all (tag 7)."""
    stats = get_character_stats(c)
    attr_run = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    return encode_sproto([(6, attr_run), (7, attr_all)])

def build_aoi_add_packet(char):
    """Build a complete TAG 505 aoi_add packet for a player character.
    character_aoi: id(0), visual(1), general(2), attribute_other(3), movement(5), runtime(6)
    Returns the full framed packet ready to send."""
    if not char:
        return None
    char_id = char.get('id', 0)
    pos = char.get('pos', [0, 0, 0, 0])
    try:
        visual_bytes = get_visual(char.get('name', 'Hero'), char.get('prof', 0))
        gen_bytes = get_general(char)
        stats = get_character_stats(char)
        attr_oth = encode_sproto([
            (0, char.get('hp', stats['hp_max'])),
            (1, stats['exp']),
            (2, stats['lv']),
            (3, stats['power']),
            (15, 1)
        ])
        mv_bytes = get_movement(pos[0], pos[1], pos[2], pos[3])
        runtime_bytes = get_runtime_aoi(char)
        char_aoi = encode_sproto([
            (0, char_id),
            (1, visual_bytes),
            (2, gen_bytes),
            (3, attr_oth),
            (5, mv_bytes),
            (6, runtime_bytes)
        ])
        aoi_data = encode_sproto([(0, char_aoi)])
        ph_p = encode_sproto([(0, 505)])
        pf_p = sproto_pack(ph_p + aoi_data)
        return struct.pack(">H", len(pf_p)) + pf_p
    except Exception as e:
        print(f"[AOI] Failed to build aoi_add packet for id={char_id}: {e}")
        return None

def is_single_player_map(map_id):
    """Return True if the map should not have player-to-player AOI.
    Map 11 = city (client-side NPCs only).
    Map 502 = Domin arena (instanced, one player per fight).
    Maps 220 and 223-229 = EXP stage scenes. Map 220 is the solo scene with
    player AOI isolated per run; 223-229 are team-copy instances."""
    mid = str(map_id)
    return mid in ("11", "502") or is_exp_map(mid)

def broadcast_aoi_move(char):
    """Broadcast TAG 507 (aoi_update_move) to all other players on the same map.
    Client expects: request field 0 -> character_aoi_move -> id(0), movement(1), walk(2)"""
    if not char:
        return
    char_id = char.get('id', 0)
    map_id = str(char.get('map_id', '11'))
    if is_single_player_map(map_id) and not ((char.get('exp_stage_state') or {}).get('team_id')):
        return
    pos = char.get('pos', [0, 0, 0, 0])
    try:
        mv_bytes = get_movement(pos[0], pos[1], pos[2], pos[3])
        # Build character_aoi_move object: id(0), movement(1), walk(2)
        char_aoi_move = encode_sproto([
            (0, char_id),
            (1, mv_bytes),
            (2, False)  # walk = false (running)
        ])
        # Wrap in request field 0
        aoi_move = encode_sproto([
            (0, char_aoi_move)
        ])
        ph_p = encode_sproto([(0, 507)])
        pf_p = sproto_pack(ph_p + aoi_move)
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
            if ch and can_aoi_exchange(char, ch):
                try:
                    c_lock = CONNECTION_LOCKS.get(cid)
                    if c_lock:
                        with c_lock:
                            c.sendall(pkt)
                    else:
                        c.sendall(pkt)
                except Exception:
                    pass
    except Exception as e:
        print(f"[AOI] Failed to broadcast aoi_move for id={char_id}: {e}")

def broadcast_aoi_stop_move(char):
    """Broadcast TAG 513 (aoi_stop_move) to all other players on the same map.
    Client expects: request field 0 -> character_aoi_move -> id(0), movement(1), walk(2)"""
    if not char:
        return
    char_id = char.get('id', 0)
    map_id = str(char.get('map_id', '11'))
    if is_single_player_map(map_id) and not ((char.get('exp_stage_state') or {}).get('team_id')):
        return
    pos = char.get('pos', [0, 0, 0, 0])
    try:
        mv_bytes = get_movement(pos[0], pos[1], pos[2], pos[3])
        # Build character_aoi_move object: id(0), movement(1), walk(2)
        char_aoi_move = encode_sproto([
            (0, char_id),
            (1, mv_bytes),
            (2, False)
        ])
        # Wrap in request field 0
        aoi_stop = encode_sproto([
            (0, char_aoi_move)
        ])
        ph_p = encode_sproto([(0, 513)])
        pf_p = sproto_pack(ph_p + aoi_stop)
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
            if ch and can_aoi_exchange(char, ch):
                try:
                    c_lock = CONNECTION_LOCKS.get(cid)
                    if c_lock:
                        with c_lock:
                            c.sendall(pkt)
                    else:
                        c.sendall(pkt)
                except Exception:
                    pass
    except Exception as e:
        print(f"[AOI] Failed to broadcast aoi_stop_move for id={char_id}: {e}")

def broadcast_aoi_attribute(char):
    """Broadcast TAG 510 (aoi_update_attribute) to all other players on the same map.
    Used to sync a player's HP/stats after PvP damage."""
    if not char:
        return
    char_id = char.get('id', 0)
    map_id = str(char.get('map_id', '11'))
    if is_single_player_map(map_id) and not ((char.get('exp_stage_state') or {}).get('team_id')):
        return
    stats = get_character_stats(char)
    hp_cur = char.get('hp', stats['hp_max'])
    try:
        attr_oth = encode_sproto([
            (0, hp_cur), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)
        ])
        attr_base = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
        attr_all = encode_sproto([
            (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
            (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
            (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
            (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
        ])
        prop = encode_sproto([(13, get_currency_balance(char, 0)),
                              (14, get_currency_balance(char, 1)),
                              (15, get_currency_balance(char, 2)),
                              (16, get_currency_balance(char, 3)),
                              (17, get_currency_balance(char, 4)),
                              (18, get_currency_balance(char, 5))])
        aoi_attr = encode_sproto([
            (0, char_id), (1, attr_oth), (2, attr_base), (3, attr_all), (5, prop)
        ])
        ph_p = encode_sproto([(0, 510)])
        pf_p = sproto_pack(ph_p + encode_sproto([(0, aoi_attr)]))
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
            if ch and can_aoi_exchange(char, ch):
                try:
                    c_lock = CONNECTION_LOCKS.get(cid)
                    if c_lock:
                        with c_lock:
                            c.sendall(pkt)
                    else:
                        c.sendall(pkt)
                except Exception:
                    pass
    except Exception as e:
        print(f"[AOI] Failed to broadcast aoi_attribute for id={char_id}: {e}")

def find_player_connection(target_id):
    """Find the connection and character dict for an online player by ID."""
    for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
        if ch and ch.get('id', 0) == target_id:
            return cid, c, ch
    return None, None, None

def get_level_data(level):
    """Return a valid BaseLvData row, even if a saved character has a bad level."""
    try:
        level = int(level)
    except (TypeError, ValueError):
        level = 1

    row = LEVEL_DATA.get(level)
    if isinstance(row, dict):
        return row

    valid_levels = [key for key, value in LEVEL_DATA.items() if isinstance(value, dict)]
    if not valid_levels:
        raise RuntimeError("BaseLvData has no valid level rows")
    return LEVEL_DATA[min(valid_levels, key=lambda key: abs(key - level))]

def get_connection_send_lock(c):
    """Return the one writer lock registered for a live client socket."""
    for cid, (active_conn, _ch) in list(ALL_CONNECTIONS.items()):
        if active_conn is c:
            return CONNECTION_LOCKS.get(cid)
    return None


def send_rpc_push_safe(c, tag, data):
    """Safe wrapper for sending TAG push — used by relife AI threads to avoid stale closures."""
    try:
        ph_p = encode_sproto([(0, tag)])
        pf_p = sproto_pack(ph_p + data)
        lock = get_connection_send_lock(c)
        if lock:
            with lock: c.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        else: c.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        return True
    except:
        return False


def send_push_to_char(char_id, tag, data):
    try:
        conn, ch = ALL_CONNECTIONS.get(int(char_id), (None, None))
        if conn is None or ch is None:
            return False
        return bool(send_rpc_push_safe(conn, tag, data))
    except Exception:
        return False

def send_exp_result_to_char(char, result):
    """Deliver an EXP result or keep it for the character's next login."""
    if not char or not result:
        return False
    char['pending_exp_result'] = result.hex()
    delivered = send_push_to_char(char.get('id', 0), 552, result)
    if delivered:
        char.pop('pending_exp_result', None)
    return delivered

def exp_member_ids(state):
    return list(dict.fromkeys(int(cid) for cid in (state.get('members') or [state.get('owner_id')]) if cid is not None))

def is_current_character_connection(conn, char):
    """Reject state-changing EXP requests from a superseded character socket."""
    if not conn or not char:
        return False
    try:
        active_conn, active_char = ALL_CONNECTIONS.get(int(char.get('id', 0)), (None, None))
    except (TypeError, ValueError):
        return False
    return active_conn is conn and active_char is char

def exp_run_map_id(state):
    """Return the physical map hosting a run (logical copy IDs can differ)."""
    if not state:
        return ''
    return str(state.get('map_id') or state.get('copy_id') or '')

def update_exp_authoritative_position(char, state, requested_pos):
    """Track EXP combat position with a server-side movement-speed bound.

    Client movement still drives its normal visual replication, but EXP attack
    range and monster targeting use this bounded position instead.
    """
    char_id = int(char.get('id', 0))
    positions = state.setdefault('player_auth_pos', {})
    timestamps = state.setdefault('player_auth_move_at', {})
    current = positions.get(char_id)
    if not isinstance(current, (list, tuple)) or len(current) < 4:
        current = list(char.get('pos', requested_pos))
        if len(current) < 4:
            current = list(current) + [0] * (4 - len(current))
        positions[char_id] = current
        timestamps[char_id] = time.monotonic()

    now = time.monotonic()
    elapsed = max(0.02, min(now - float(timestamps.get(char_id, now)), 1.5))
    # The APK receives movement speed 500 (5 m/s). Allow 3x for dashes,
    # uneven packet timing, and motion interpolation while still limiting jumps.
    max_step_cm = max(75.0, 500.0 * elapsed * 3.0)
    dx = float(requested_pos[0]) - float(current[0])
    dz = float(requested_pos[2]) - float(current[2])
    distance = math.hypot(dx, dz)
    if distance > max_step_cm and distance > 0:
        scale = max_step_cm / distance
        x = int(round(float(current[0]) + dx * scale))
        z = int(round(float(current[2]) + dz * scale))
    else:
        x = int(requested_pos[0])
        z = int(requested_pos[2])
    tracked = [x, int(requested_pos[1]), z, int(requested_pos[3])]
    positions[char_id] = tracked
    timestamps[char_id] = now
    return tracked

def validate_exp_skill_hit(char, state, target_id, skill_id, requested_effinfo_id,
                           effects_in_batch, targets_in_batch):
    """Validate an EXP skill/effect, range, and replay window from server state."""
    skill_id = str(skill_id)
    skill_cfg = SKILL_CONFIG.get(skill_id)
    if not skill_cfg:
        print(f"[EXP HIT] rejected unknown skill={skill_id}")
        return None

    skill_levels = {str(key): value for key, value in (char.get('skill_levels') or {}).items()}
    prof = char.get('prof', 0)
    try:
        prof = int(prof)
    except (TypeError, ValueError):
        prof = 0
    starter_skills = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    always_available = {str(value) for value in starter_skills.get('atk', [])}
    always_available.update(str(starter_skills.get(key, '')) for key in ('dodge', 'starter_active'))
    always_available.discard('')
    if skill_id not in skill_levels and skill_id not in always_available:
        print(f"[EXP HIT] rejected unlearned skill={skill_id} attacker={char.get('id')}")
        return None
    try:
        skill_lv = int(skill_levels.get(skill_id, 0))
    except (TypeError, ValueError):
        return None
    if skill_lv < 0:
        print(f"[EXP HIT] rejected invalid skill level={skill_lv} skill={skill_id}")
        return None

    allowed_effects = {str(skill_cfg.get(key) or '') for key in ('eff0', 'eff1', 'eff2')}
    allowed_effects.discard('')
    effinfo_id = str(requested_effinfo_id or '')
    if effinfo_id not in allowed_effects:
        print(f"[EXP HIT] rejected effect={effinfo_id!r} for skill={skill_id}")
        return None

    player_pos = state.get('player_auth_pos', {}).get(int(char.get('id', 0)), char.get('pos', [0, 100, 0, 0]))
    monster_pos = state.get('monster_pos', {}).get(int(target_id))
    if (not isinstance(player_pos, (list, tuple)) or len(player_pos) < 3
            or not isinstance(monster_pos, (list, tuple)) or len(monster_pos) < 2):
        print(f"[EXP HIT] rejected target without authoritative position target={target_id}")
        return None

    effect_cfg = EFF_CONFIG.get(effinfo_id, {})
    params = effect_cfg.get('params', [0, 0, 0, 0, 0])
    param1 = abs(int(params[0] if len(params) > 0 else 0)) / 100.0
    param2 = abs(int(params[1] if len(params) > 1 else 0)) / 100.0
    area_type = int(effect_cfg.get('area_type', 0))
    trace_distance = max(0, int(skill_cfg.get('trace_distance_cm', 0))) / 100.0
    if area_type == 1:
        effect_reach = trace_distance + param1
    elif area_type == 2:
        effect_reach = math.hypot(param1, param2 / 2.0)
    elif area_type in (3, 4, 5):
        effect_reach = param1
    else:
        effect_reach = trace_distance
    max_reach = max(trace_distance, effect_reach) + 3.0
    player_x = float(player_pos[0]) / 100.0
    player_z = float(player_pos[2]) / 100.0
    if math.hypot(float(monster_pos[0]) - player_x, float(monster_pos[1]) - player_z) > max_reach:
        print(f"[EXP HIT] rejected out-of-range target={target_id} skill={skill_id}")
        return None

    cooldown_s = max(0.05, int(skill_cfg.get('cooldown_ms', 0)) / 1000.0)
    hit_lock = state.setdefault('_hit_lock', threading.RLock())
    with hit_lock:
        hit_cooldowns = state.setdefault('_hit_cooldowns', {})
        cooldown_key = (int(char.get('id', 0)), skill_id, effinfo_id)
        batch_target_key = (cooldown_key, int(target_id))
        if batch_target_key in targets_in_batch:
            print(f"[EXP HIT] rejected duplicate batch target={target_id} skill={skill_id} effect={effinfo_id}")
            return None
        targets_in_batch.add(batch_target_key)
        if cooldown_key not in effects_in_batch:
            now_mono = time.monotonic()
            last_hit = hit_cooldowns.get(cooldown_key)
            if last_hit is not None and now_mono - last_hit < cooldown_s:
                print(f"[EXP HIT] rejected cooldown replay skill={skill_id} effect={effinfo_id}")
                return None
            hit_cooldowns[cooldown_key] = now_mono
            effects_in_batch.add(cooldown_key)
        if len(hit_cooldowns) > 2048:
            now_mono = time.monotonic()
            state['_hit_cooldowns'] = {
                key: stamp for key, stamp in hit_cooldowns.items()
                if now_mono - stamp < 30.0
            }
    return skill_id, skill_lv, effinfo_id

def exp_send_push(state, tag, data, exclude=None):
    excluded = set(int(cid) for cid in (exclude or []))
    run_map_id = exp_run_map_id(state)
    for cid in exp_member_ids(state):
        conn, ch = ALL_CONNECTIONS.get(cid, (None, None))
        if cid not in excluded and conn is not None and ch is not None and str(ch.get('map_id')) == run_map_id:
            send_rpc_push_safe(conn, tag, data)

def exp_participant_chars(state):
    out = []
    for cid in exp_member_ids(state):
        conn, ch = ALL_CONNECTIONS.get(cid, (None, None))
        if ch is None:
            ch = state.get('char_refs', {}).get(cid)
        if ch is not None:
            out.append((cid, conn, ch))
    return out

def exp_online_members(state, alive=False):
    out = []
    run_map_id = exp_run_map_id(state)
    for cid in exp_member_ids(state):
        conn, ch = ALL_CONNECTIONS.get(cid, (None, None))
        if conn is not None and ch is not None and str(ch.get('map_id')) == run_map_id and (not alive or ch.get('hp', 0) > 0):
            out.append((cid, conn, ch))
    return out

def exp_sync_npc_attrs(state, inst_id, stats, hp):
    oth=encode_sproto([(0,hp),(2,stats['lv'])]); base=encode_sproto([(0,stats['hp_max'])])
    allv=encode_sproto([(0,stats['hp_max']),(2,stats['atk']),(3,stats['def']),(4,stats['hit']),(5,stats['eva']),(6,stats['cri']),(7,stats['res']),(8,stats['exd']),(9,stats['exr']),(10,stats['crd']),(11,stats['crr']),(12,stats['defa']),(13,500),(17,stats['dgea']),(18,stats['resa']),(19,stats['hita']),(20,stats['cria'])])
    obj=encode_sproto([(0,inst_id),(1,oth),(2,base),(3,allv)])
    exp_send_push(state,510,encode_sproto([(0,obj)]))

def is_exp_map(map_id):
    mid = str(map_id)
    if mid in ('223', '224', '225', '226', '227', '228', '229'):
        return True
    # The APK's single-player EXP copies share the physical map configured by
    # CopySceneData.SingleMapID (220 in this client build).
    return any(
        str(cfg.get('single_map_id') or '') == mid
        for copy_id, cfg in COPY_SCENE_CONFIG.items()
        if copy_id in DAILY_EXP_CONFIG
    )

def is_active_copy_current(char):
    """Check whether a saved copy marker still matches the character's map."""
    if not char:
        return False
    active_copy_id = str(char.get('active_copy_id') or '')
    current_map = str(char.get('map_id') or '11')
    # Map 11 is the city/default map, never a copy instance. Older saves can
    # contain active_copy_id=11 from code that confused the current map with
    # the active copy marker.
    if not active_copy_id or active_copy_id == '11' or current_map == '11':
        return False
    allowed_maps = {active_copy_id}
    active_cfg = COPY_SCENE_CONFIG.get(active_copy_id, {})
    if active_cfg.get('map_id'):
        allowed_maps.add(str(active_cfg['map_id']))
    if active_copy_id in DAILY_EXP_CONFIG and active_cfg.get('single_map_id'):
        allowed_maps.add(str(active_cfg['single_map_id']))
    return current_map in allowed_maps

def is_active_daily_copy_current(char):
    """Return True only for a configured daily copy the character entered."""
    if not char:
        return False
    active_copy_id = str(char.get('active_copy_id') or '')
    if active_copy_id not in COPY_SCENE_CONFIG:
        return False
    current_map = str(char.get('map_id') or '11')
    if not active_copy_id or active_copy_id == '11' or current_map == '11':
        return False
    cfg = COPY_SCENE_CONFIG[active_copy_id]
    # Copy entry uses the copy ID as its map, except Street Race, whose
    # configured map ID is the actual scene. Do not infer an active daily
    # run merely because a shared world map appears as another copy's MapId.
    expected_map = str(cfg.get('map_id') or active_copy_id) if cfg.get('subtype') == 7 else active_copy_id
    if active_copy_id in DAILY_EXP_CONFIG and cfg.get('single_map_id'):
        expected_map = str(cfg['single_map_id'])
    return current_map == expected_map

def reconcile_exp_stage_login(char):
    """Restore live EXP runs and discard copy markers that do not match the loaded map."""
    if not char:
        return False
    leave_timer = char.get('_exp_leave_timer')
    if leave_timer is not None:
        try:
            if leave_timer.is_alive():
                return False
        except Exception:
            return False
        char.pop('_exp_leave_timer', None)

    char_id = int(char.get('id', 0))
    current_map = str(char.get('map_id') or '11')
    state = char.get('exp_stage_state')
    if state is None:
        try:
            team_id = int(char.get('exp_team_id') or CHAR_TEAM.get(char_id, 0) or 0)
        except (TypeError, ValueError):
            team_id = 0
        team = TEAM_STATES.get(team_id)
        team_run = team.get('active_run') if team else None
        if team_run and char_id in exp_member_ids(team_run):
            state = team_run
            char['exp_stage_state'] = state

    if state is not None:
        copy_id = str(state.get('copy_id') or '')
        run_map_id = exp_run_map_id(state)
        is_member = char_id in exp_member_ids(state)
        run_live = not state.get('_finish_handled') and not state.get('team_finish_handled') and state.get('ai_active', True)
        if is_exp_map(run_map_id) and copy_id in DAILY_EXP_CONFIG and is_member and run_live:
            char_refs = state.get('char_refs')
            if not isinstance(char_refs, dict):
                char_refs = {}
                state['char_refs'] = char_refs
            char_refs[char_id] = char
            if current_map != run_map_id:
                # The in-memory run survives a socket reconnect, while the
                # character location can lag behind it. Resume at the stage's
                # known safe spawn point so monsters and player share a map.
                char['map_id'] = run_map_id
                char['pos'] = [0, 100, 0, 0]
                char['active_copy_id'] = copy_id
                print(f"[EXP RECOVERY] resumed live run copy={copy_id} player={char_id}")
                return True
            char['active_copy_id'] = copy_id
            return False

        if not is_exp_map(current_map):
            char_refs = state.get('char_refs')
            if isinstance(char_refs, dict) and char_refs.get(char_id) is char:
                char_refs.pop(char_id, None)
            char.pop('exp_stage_state', None)

    active_copy_id = str(char.get('active_copy_id') or '')
    if active_copy_id and not is_active_copy_current(char):
        # Copy state is runtime-only. A saved marker that does not match the
        # loaded map is stale; clear it so city players can create/join teams
        # and enter another copy. This also repairs legacy active_copy_id=11.
        char['active_copy_id'] = None
        char['exp_team_id'] = None
        char.pop('exp_members', None)
        char['exp_defer_start'] = False
        char.pop('pre_copy_pos', None)
        char.pop('pre_copy_map', None)
        print(f"[COPY RECOVERY] cleared stale marker copy={active_copy_id} map={current_map} player={char_id}")
        return True
    return False

def is_character_available_for_team(char):
    if not char:
        return False
    reconcile_exp_stage_login(char)
    return (not char.get('exp_stage_state') and not char.get('_exp_leave_timer')
        and not is_exp_map(char.get('map_id')) and not is_active_daily_copy_current(char))

def can_aoi_exchange(source, target):
    if not source or not target or source.get('id')==target.get('id') or str(source.get('map_id'))!=str(target.get('map_id')): return False
    if is_exp_map(source.get('map_id')):
        state=source.get('exp_stage_state')
        return bool(state and state.get('team_id') and state is target.get('exp_stage_state'))
    return not is_single_player_map(source.get('map_id'))

def send_packet_to_char(char_id, packet):
    conn=ALL_CONNECTIONS.get(int(char_id),(None,None))[0]
    if conn is None: return False
    try:
        lock=CONNECTION_LOCKS.get(int(char_id))
        if lock:
            with lock: conn.sendall(packet)
        else: conn.sendall(packet)
        return True
    except Exception: return False

def sync_exp_team_aoi(char):
    state=char.get('exp_stage_state') if char else None
    if not state or not state.get('team_id'): return
    own=build_aoi_add_packet(char)
    if not own: return
    for cid,conn,peer in exp_online_members(state):
        if cid==int(char.get('id',0)): continue
        other=build_aoi_add_packet(peer)
        if other:
            send_packet_to_char(char['id'],other);send_packet_to_char(cid,own)

def team_member_blob(char, team_id):
    stats = get_character_stats(char)
    hpmax = int(stats.get('hp_max', 1))
    goal = str(char.get('team_goal_copy', ''))
    goal_cfg = COPY_SCENE_CONFIG.get(goal, {})
    attempts = copy_attempts_remaining(char, goal, goal_cfg) if goal in DAILY_EXP_CONFIG else -1
    return encode_sproto([(0, int(char.get('id', 0))), (1, int(team_id)), (2, str(char.get('name', 'Hero'))),
        (3, int(char.get('level', 1))), (4, int(char.get('prof', 0))), (5, int(stats.get('power', 0))),
        (6, int(char.get('map_id', 11))), (7, 0), (8, 0), (9, max(0, int(char.get('hp', hpmax)))),
        (10, hpmax), (14, build_main_player_visual(char)), (15, attempts),
        (16, int(char.get('guild_id', 0))), (17, str(char.get('guild_name', '')))])

def team_payload(team):
    if not team or not team.get('members'):
        return None
    leader_id = int(team['leader_id'])
    leader = ALL_CONNECTIONS.get(leader_id, (None, team.get('char_refs', {}).get(leader_id)))[1]
    if leader is None:
        return None
    others = {}
    for cid in team['members']:
        if int(cid) == leader_id: continue
        char = ALL_CONNECTIONS.get(int(cid), (None, team.get('char_refs', {}).get(int(cid))))[1]
        if char is not None: others[int(cid)] = team_member_blob(char, team['id'])
    return encode_sproto([(0, int(team['id'])), (1, team_member_blob(leader, team['id'])), (2, len(team['members'])),
        (3, int(team.get('verify', 0))), (4, others), (5, str(team.get('goal_id', ''))),
        (6, int(team.get('min_level', 1))), (7, int(team.get('max_level', 80))), (8, int(team.get('recruit', 0)))])

def send_team_update(team_id, only_ids=None):
    team = TEAM_STATES.get(int(team_id))
    payload = team_payload(team)
    if payload is None: return
    data = encode_sproto([(0, payload)])
    for cid in list(team.get('members', []) if only_ids is None else only_ids):
        send_push_to_char(cid, 518, data)

def send_team_member_update(team_id, char, force=False):
    team = TEAM_STATES.get(int(team_id))
    if not team or not char: return
    run = team.get('active_run')
    member_id = int(char.get('id', 0))
    updates = run.setdefault('member_update_at', {}) if run else {}
    now = time.monotonic()
    if not force and now - updates.get(member_id, 0) < 0.5: return
    if run: updates[member_id] = now
    data = encode_sproto([(0, int(team_id)), (1, team_member_blob(char, team_id))])
    for cid in list(team.get('members', [])):
        send_push_to_char(cid, 575, data)

def get_team_for_char(char_id):
    return TEAM_STATES.get(CHAR_TEAM.get(int(char_id)))

def get_relife_config(death_count):
    if RELIFE_DATA:
        for cfg in RELIFE_DATA:
            if cfg['min_count'] <= death_count <= cfg['max_count']:
                return cfg
        return RELIFE_DATA[-1]
    return {'id': 1, 'min_count': 1, 'max_count': 999999, 'use_count': 1}

def get_character_stats(c):
    """Calculates all character attributes and Power based on profession and level."""
    lv = c.get('level', 1)
    prof = c.get('prof', 0)
    ld = get_level_data(lv)

    # Base attributes from BaseLvData
    atk = ld['atk'][prof]
    hp_max = ld['hp'][prof]
    df = ld['def'][prof]
    hit = ld['hit'][prof]
    eva = ld['eva'][prof]
    cri = ld['cri'][prof]
    res = ld['res'][prof]

    # Add Weapon ATK from EquipData (starter weapon per profession)
    starter_wids = {0: "10001", 1: "20001", 2: "30001"}
    wid = starter_wids.get(prof, "10001")
    equip = EQUIP_CONFIG.get(wid)
    weapon_atk = equip['base_val'] if equip and equip.get('base_stat') == 1001 else 180
    atk += weapon_atk

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
        'power': power, 'lv': lv, 'exp': c.get('exp', 0),
        'defa': ld['defa'], 'dgea': ld['dgea'], 'resa': ld['resa'],
        'hita': ld['hita'], 'cria': ld['cria'],
        'exd': ld['exd'][prof], 'exr': ld['exr'][prof], 'crd': ld['crd'][prof], 'crr': ld['crr'][prof],
        'satp': 0, 'satm': 0, 'satc': 0
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

MONEY_FIELDS = ('cash', 'gold', 'diamond', 'guild_contribute', 'battle_coin', 'activity_coin')
MONEY_DEFAULTS = (1000, 100, 10, 0, 0, 0)
CHARACTER_TRANSACTION_LOCKS = {}
CHARACTER_TRANSACTION_LOCKS_GUARD = threading.Lock()

def get_character_transaction_lock(char):
    """Serialize reward and purchase state changes for duplicate character sessions."""
    char_id = str(char.get('id', ''))
    with CHARACTER_TRANSACTION_LOCKS_GUARD:
        return CHARACTER_TRANSACTION_LOCKS.setdefault(char_id, threading.RLock())

def get_currency_balance(char, money_type):
    """Return one persisted APK currency balance (MONEY_TYPE values are 0..5)."""
    try:
        money_type = int(money_type)
        if not 0 <= money_type < len(MONEY_FIELDS):
            return 0
        return max(0, int(char.get(MONEY_FIELDS[money_type], MONEY_DEFAULTS[money_type])))
    except (AttributeError, TypeError, ValueError):
        return 0

def set_currency_balance(char, money_type, amount):
    """Persist one nonnegative APK currency balance on a character."""
    money_type = int(money_type)
    if not 0 <= money_type < len(MONEY_FIELDS):
        return False
    char[MONEY_FIELDS[money_type]] = max(0, int(amount))
    return True

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

    prop = encode_sproto([(13, get_currency_balance(c, 0)), (14, get_currency_balance(c, 1)),
                          (15, get_currency_balance(c, 2)), (16, get_currency_balance(c, 3)),
                          (17, get_currency_balance(c, 4)), (18, get_currency_balance(c, 5))])
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])

    attr_run = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    # attr_all tags (attribute.cs): 0:max_hp, 2:atk, 3:def, 4:hit, 5:eva, 6:cri, 7:res, 8:exd, 9:exr, 10:crd, 11:crr, 12:defa, 13:mov, 17:dgea, 18:resa, 19:hita, 20:cria
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    char_level = stats['lv']
    skill_levels = c.get('skill_levels', {})
    skills_map = build_skills_map(c.get('prof', 0), char_level, skill_levels)
    starter_wids = {0: "10001", 1: "20001", 2: "30001"}
    wid = starter_wids.get(c.get('prof', 0), "10001")
    equip = EQUIP_CONFIG.get(wid)
    equip_class = equip['class'] if equip else 1
    w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, equip_class), (5, 1), (6, 1), (7, [0]*8)])
    equip_map = {5: w1}

    # character.download: the APK treats 2 as completed.  Sending 1 again on
    # reconnect would reopen the optional download/reward UI forever, even
    # though MSG 270 was already persisted for this character.
    download_state = 2 if c.get('download_complete') else 1
    return encode_sproto([
        (0, c['id']),
        (1, gen),
        (2, attr_oth),
        (5, prop),
        (6, build_main_player_visual(c)),
        (7, mv),
        (8, skills_map),
        (9, equip_map),
        (12, 0),
        (13, run),
        (15, download_state)
    ])

def sync_char_attrs_rpc(conn, picked_char, conn_id=None):
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
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ])

    prop = encode_sproto([(13, get_currency_balance(picked_char, 0)),
                          (14, get_currency_balance(picked_char, 1)),
                          (15, get_currency_balance(picked_char, 2)),
                          (16, get_currency_balance(picked_char, 3)),
                          (17, get_currency_balance(picked_char, 4)),
                          (18, get_currency_balance(picked_char, 5))])

    aoi_attr = encode_sproto([
        (0, picked_char['id']), (1, attr_oth), (2, attr_base), (3, attr_all), (5, prop)
    ])

    print(f"[PLAYER SYNC] HP={hp_cur}/{stats['hp_max']} POWER={stats['power']} LV={stats['lv']}")
    try:
        ph_p = encode_sproto([(0, 510)])
        pf_p = sproto_pack(ph_p + encode_sproto([(0, aoi_attr)]))
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        # Use the socket's common writer lock, including callers that only
        # have the socket and not its character ID (for example EXP AI ticks).
        c_lock = CONNECTION_LOCKS.get(conn_id) if conn_id is not None else None
        if c_lock is None:
            c_lock = get_connection_send_lock(conn)
        if c_lock:
            with c_lock:
                conn.sendall(pkt)
        else:
            conn.sendall(pkt)
    except: pass

def get_npc_attr(nid, player_level=1):
    cfg = NPC_CONFIG.get(str(nid))
    if not cfg:
        return {
            'hp_max': 1000, 'atk': 40, 'def': 100, 'hit': 2844, 'eva': 100, 'cri': 351, 'res': 0,
            'lv': player_level, 'defa': 3158, 'dgea': 6317, 'resa': 3158, 'hita': 316, 'cria': 3158,
            'exd': 0, 'exr': 0, 'crd': 15000, 'crr': 0, 'power': 1000
        }

    lvl = cfg.get('level', 1)
    if lvl == 9999:
        lvl = player_level

    max_lv = max(ADAPT_DATA.keys()) if ADAPT_DATA else (max(LEVEL_DATA.keys()) if LEVEL_DATA else 1)
    effective_lv = min(lvl, max_lv)
    adapt = ADAPT_DATA.get(effective_lv, ADAPT_DATA.get(1, {}))
    ld = LEVEL_DATA.get(effective_lv, LEVEL_DATA.get(1, {}))

    if cfg.get('is_abs'):
        hp = cfg.get('hp_abs', 1000)
        atk = cfg.get('atk_abs', 40)
        df = cfg.get('def_abs', 100)
        hit = cfg.get('hit_abs', 2844)
        eva = cfg.get('eva_abs', 100)
        cri = cfg.get('cri_abs', 351)
        res = cfg.get('res_abs', 0)
        exd = cfg.get('exd_abs', 0)
        exr = cfg.get('exr_abs', 0)
        crd = cfg.get('crd_abs', 15000)
        crr = cfg.get('crr_abs', 0)
        defa = cfg.get('defa_abs', 3158)
        dgea = cfg.get('dgea_abs', 6317)
        resa = cfg.get('resa_abs', 3158)
        hita = cfg.get('hita_abs', 316)
        cria = cfg.get('cria_abs', 3158)
    else:
        hp_std = adapt.get('hp', ld.get('hp', [1000])[0] if isinstance(ld.get('hp'), list) else 1000)
        atk_std = adapt.get('atk', ld.get('atk', [40])[0] if isinstance(ld.get('atk'), list) else 40)
        def_std = adapt.get('def', ld.get('def', [100])[0] if isinstance(ld.get('def'), list) else 100)
        hit_std = adapt.get('hit', ld.get('hit', [2844])[0] if isinstance(ld.get('hit'), list) else 2844)
        eva_std = adapt.get('eva', 100)
        cri_std = adapt.get('cri', 351)
        res_std = adapt.get('res', 0)
        exd_std = adapt.get('exd', 0)
        exr_std = adapt.get('exr', 0)
        crd_std = adapt.get('crd', 15000)
        crr_std = adapt.get('crr', 0)
        defa_std = adapt.get('defa', 3158)
        dgea_std = adapt.get('dgea', 6317)
        resa_std = adapt.get('resa', 3158)
        hita_std = adapt.get('hita', 316)
        cria_std = adapt.get('cria', 3158)

        hp = (cfg.get('hp_coe', 10000) * hp_std) // 10000
        atk = (cfg.get('atk_coe', 10000) * atk_std) // 10000
        df = (cfg.get('def_coe', 10000) * def_std) // 10000
        hit = (cfg.get('hit_coe', 10000) * hit_std) // 10000
        eva = (cfg.get('eva_coe', 10000) * eva_std) // 10000
        cri = (cfg.get('cri_coe', 10000) * cri_std) // 10000
        res = (cfg.get('res_coe', 10000) * res_std) // 10000
        exd = (cfg.get('exd_coe', 10000) * exd_std) // 10000
        exr = (cfg.get('exr_coe', 10000) * exr_std) // 10000
        crd = (cfg.get('crd_coe', 10000) * crd_std) // 10000
        crr = (cfg.get('crr_coe', 10000) * crr_std) // 10000
        defa = (cfg.get('defa_coe', 10000) * defa_std) // 10000
        dgea = (cfg.get('dgea_coe', 10000) * dgea_std) // 10000
        resa = (cfg.get('resa_coe', 10000) * resa_std) // 10000
        hita = (cfg.get('hita_coe', 10000) * hita_std) // 10000
        cria = (cfg.get('cria_coe', 10000) * cria_std) // 10000

    if cfg.get('hp_abs', 0) > hp: hp = cfg['hp_abs']
    if cfg.get('atk_abs', 0) > atk: atk = cfg['atk_abs']
    if cfg.get('def_abs', 0) > df: df = cfg['def_abs']

    prof_coeffs = {"atk": 16, "hp": 1, "def": 11}
    raw_power = (atk * prof_coeffs['atk'] + hp * prof_coeffs['hp'] + df * prof_coeffs['def'])
    power = int(raw_power * 3.0)

    return {
        'hp_max': hp, 'atk': atk, 'def': df,
        'hit': hit, 'eva': eva, 'cri': cri, 'res': res,
        'lv': lvl, 'defa': defa, 'dgea': dgea, 'resa': resa, 'hita': hita, 'cria': cria,
        'exd': exd, 'exr': exr, 'crd': crd, 'crr': crr,
        'power': power,
        'satp': 0, 'satm': 0, 'satc': 0
    }

def sync_npc_attrs_rpc(conn, inst_id, stats, hp_cur):
    """Sends TAG 510 to sync NPC stats."""
    # The dominance zombie is initially added to camp 2 by tag 544.  Omitting
    # camp here makes the APK decode it as camp 0 and move it out of
    # DominSceneManager's CampList[2], so rank_pvp_start never enables its AI.
    attr_other_fields = [(0, hp_cur), (2, stats['lv'])]
    if NPC_INST_MAP.get(inst_id, '').startswith('BOSS_'):
        # TAG 510 arrives immediately after TAG 544.  Preserve the title
        # assigned in get_boss_char or the APK resets it to level 0.
        attr_other_fields.extend([(4, 1), (15, 2)])
    attr_oth = encode_sproto(attr_other_fields)
    attr_base = encode_sproto([(0, stats['hp_max'])])
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    aoi_attr = encode_sproto([(0, inst_id), (1, attr_oth), (2, attr_base), (3, attr_all)])
    try:
        ph_p = encode_sproto([(0, 510)])
        pf_p = sproto_pack(ph_p + encode_sproto([(0, aoi_attr)]))
        packet = struct.pack(">H", len(pf_p)) + pf_p
        lock = get_connection_send_lock(conn)
        if lock:
            with lock:
                conn.sendall(packet)
        else:
            conn.sendall(packet)
    except: pass

def get_combat_damage(attacker_stats, defender_stats, skill_id, skill_lv, is_area=False, pvp_scale=1.0, effinfo_id=None, sat_type=None, forced_hit=None, forced_crit=None, max_variance=False):
    """Original Damage calculation reproduced from CharacterAttributeData.cs"""
    prefix = "[AREA DAMAGE]" if is_area else "[COMBAT]"

    # 1. Get Skill Multipliers from EffInfoData
    # Use the actual effinfo_id from TAG 111 if provided, otherwise fall back to eff0
    if effinfo_id is None:
        skill_cfg = SKILL_CONFIG.get(skill_id, {})
        effinfo_id = skill_cfg.get('eff0', "10000")
    eff_cfg = EFF_CONFIG.get(effinfo_id, {
        'dmg_fixed': 0, 'dmg_fixed_add': 0, 'dmg_multi': 10000, 'dmg_multi_add': 0, 'adds': {}
    })

    skill_damage = eff_cfg['dmg_fixed'] + eff_cfg['dmg_fixed_add'] * skill_lv
    skill_scale = (eff_cfg['dmg_multi'] + (eff_cfg['dmg_multi_add'] or 0) * skill_lv) / 10000.0

    # PvP Scale handling (num3 in CharacterAttributeData.cs)
    # Only applies when BOTH attacker and defender are player/zombie/zombie_ragdoll types.
    # For NPC targets, pvp_scale stays at 1.0 (no PvP multiplier).
    pvp_mult = pvp_scale
    if pvp_scale > 1.0:
        # PvP context — check combo value condition from client
        if attacker_stats.get('power', 0) > defender_stats.get('power', 0):
            pvp_mult += 0.05

    # 2. Check Hit/Dodge
    skill_shit = eff_cfg['adds'].get(3001, 0) / 10000.0
    hit_p = min((attacker_stats['hit'] + 1.0) / (attacker_stats['hita'] + attacker_stats['hit'] + 1.0), 1.0)
    dge_p = min((defender_stats['eva'] + 1.0) / (defender_stats['dgea'] + defender_stats['eva'] + 1.0), 0.5)

    # Client formula: 1 + hit_p - dge_p + skill_shit >= random/100
    # Where random is 0-99, so random/100 is 0.00 to 0.99
    # Client uses >=, so server uses >= with same range
    hit_prob = 1.0 + hit_p - dge_p + skill_shit
    roll_hit = random.randint(0, 99) / 100.0 if forced_hit is None else None  # 0.00 to 0.99, matches client

    if is_area:
        print(f"{prefix} HIT CHECK: hit_prob={hit_prob:.3f} (hit_p={hit_p:.3f}, dge_p={dge_p:.3f}, skill={skill_shit:.3f}) roll={roll_hit:.3f}")
        print(f"{prefix} STATS: AtkHIT={attacker_stats['hit']} AtkHITA={attacker_stats['hita']} DefEVA={defender_stats['eva']} DefDGEA={defender_stats['dgea']}")

    if (forced_hit is False) or (forced_hit is None and roll_hit > hit_prob) or (forced_hit is True and hit_prob < 0):
        if is_area: print(f"{prefix} RESULT: MISS")
        return 0, False, False # MISS

    # 3. Check Crit
    skill_scri = eff_cfg['adds'].get(3002, 0) / 10000.0
    cri_p = min((attacker_stats['cri'] + 1.0) / (attacker_stats['cri'] + attacker_stats['cria'] + 1.0), 0.9)
    res_p = min((defender_stats['res'] + 1.0) / (defender_stats['res'] + defender_stats['resa'] + 1.0), 0.8)

    cri_prob = cri_p - res_p + skill_scri
    # Client: cri_prob >= random(0-99)/100  (CharacterAttributeData.cs line 1076)
    is_cri = (cri_prob >= random.randint(0, 99) / 100.0) if forced_crit is None else (bool(forced_crit) and cri_prob >= 0)

    # 4. Calculate Damage
    scaled_damage = skill_damage * pvp_mult
    scaled_scale = skill_scale * pvp_mult

    # Add SATP/SATM/SATC contribution based on defender type (CharacterAttributeData.cs lines 983-1008)
    # The client checks attacker ObjType first: only player/zombie/zombie_ragdoll add SAT.
    # Then it checks defender ObjType:
    #   - OBJ_NPC_CAR / OBJ_PLAYER_CAR  → SATC
    #   - OBJ_NPC with FunctionType==10 → SATC
    #   - OBJ_NPC (other)               → SATM
    #   - else (player, etc.)           → SATP
    # sat_type parameter overrides: 'satp' (default for PvP), 'satm' (NPC), 'satc' (car)
    if sat_type is None:
        sat_type = attacker_stats.get('sat_type', 'satp')
    sat_value = attacker_stats.get(sat_type, 0)
    # Client adds CurSATM/SATP/SATC to skillDamge (fixed damage).
    # The scale add comes from effinfo's AddType fields via GetSkillTypeValue.
    # GetSkillTypeValue returns 0 for OBJ_NPC attackers, but for player attackers
    # it sums effinfo AddType1-4 matching the SKILL_ADD_TYPE and divides by 10000.
    scaled_damage += sat_value
    # Add effect-based SAT scale bonus (client: skillScale += GetSkillTypeValue(..., SATM))
    # This applies to player attackers; for NPC attackers GetSkillTypeValue returns 0.
    if sat_type == 'satm':
        satm_eff = eff_cfg['adds'].get(3005, 0)
        if satm_eff:
            scaled_scale += satm_eff / 10000.0
    elif sat_type == 'satp':
        satp_eff = eff_cfg['adds'].get(3007, 0)
        if satp_eff:
            scaled_scale += satp_eff / 10000.0
    elif sat_type == 'satc':
        satc_eff = eff_cfg['adds'].get(3006, 0)
        if satc_eff:
            scaled_scale += satc_eff / 10000.0

    base_dmg = attacker_stats['atk'] * scaled_scale + scaled_damage
    def_red = min((defender_stats['def'] + 1.0) / (defender_stats['def'] + attacker_stats['defa']), 0.5)

    # 5. Handling Critical Multiplier
    crit_mult = 1.0
    if is_cri:
        crit_mult = max(1.0, min(1.0 + (attacker_stats['crd'] - defender_stats['crr']) / 10000.0, 2.0))

    # 6. Final Formula with Random Variance [0.95, 1.049]
    # Client: GetRandom() returns 0-99, then /1000 + 0.95 = 0.95 to 1.049
    rand_var = 1.049 if max_variance else random.randint(0, 99) / 1000.0 + 0.95

    skill_sexd = eff_cfg['adds'].get(3003, 0) / 10000.0
    exd_factor = 1.0 + (attacker_stats['exd'] - defender_stats['exr']) / 10000.0 + skill_sexd

    final_dmg = crit_mult * base_dmg * rand_var * (1.0 - def_red) * exd_factor

    if is_area:
        print(f"{prefix} RESULT: HIT dmg={int(final_dmg)} base={base_dmg:.1f} red={def_red:.3f} crit={crit_mult:.2f} exd={exd_factor:.2f} var={rand_var:.3f}")

    # Client uses Mathf.CeilToInt(num5) - use ceiling, not truncation
    return int(math.ceil(max(1, final_dmg))), True, is_cri

def build_npc_attr_sproto(nid, name, x, z, o, existing_inst_id=None):
    """Build NPC attribute sproto for TAG 509 (npc_create).
    Bug 1/6: Shared function to eliminate code duplication between send_npc_create and send_npc_wrapper."""
    npc_stats = get_npc_attr(nid)
    hp_max = npc_stats['hp_max']
    atk = npc_stats['atk']
    df = npc_stats['def']
    hit = npc_stats.get('hit', 2844)
    eva = npc_stats.get('eva', 100)
    cri = npc_stats.get('cri', 351)
    exd = npc_stats.get('exd', 0)
    exr = npc_stats.get('exr', 0)
    res = npc_stats.get('res', 0)
    crd = npc_stats.get('crd', 15000)
    crr = npc_stats.get('crr', 0)
    defa = npc_stats.get('defa', 3158)
    dgea = npc_stats.get('dgea', 6317)
    resa = npc_stats.get('resa', 3158)
    hita = npc_stats.get('hita', 316)
    cria = npc_stats.get('cria', 3158)
    lvl = npc_stats['lv']

    if existing_inst_id is None:
        inst_id = next_global_inst_id()
        hp_cur = hp_max
        NPC_HP_MAP[inst_id] = hp_max
        NPC_INST_MAP[inst_id] = str(nid)
        print(f"[NPC SPAWN] inst_id={inst_id} npcid={nid} hp_max={hp_max} atk={atk} def={df}")
    else:
        # Reconnects must recreate the current NPC at its live position and
        # health without allocating a second server instance.
        inst_id = int(existing_inst_id)
        hp_cur = max(0, int(NPC_HP_MAP.get(inst_id, hp_max)))
        NPC_INST_MAP[inst_id] = str(nid)

    final_nid = str(nid)
    if ";" in final_nid:
        if "XD_A" in final_nid: final_nid = "100"
        elif "QJ_A" in final_nid: final_nid = "104"
        elif "NQS_A" in final_nid: final_nid = "105"

    attr = encode_sproto([
        (0, inst_id), (1, final_nid), (2, hp_cur), (3, hp_max), (4, atk), (5, df),
        (6, hit), (7, eva), (8, cri), (9, exd), (10, exr), (11, res), (12, crd), (13, crr), (14, defa),
        (15, x), (16, z), (17, o), (18, lvl), (21, name),
        (24, dgea), (25, resa), (26, hita), (27, cria)
    ])
    NPC_ATTR_MAP[inst_id] = attr
    return inst_id, attr


def _build_exp_npc_wrapper(send_rpc_push, exp_state):
    """Factory function that creates send_npc_wrapper for EXP dungeon wave advancement.
    Bug 1: Moved outside on_npc_killed to avoid redefining 140× per dungeon.
    Bug 6: Uses shared build_npc_attr_sproto to eliminate code duplication.
    Fix: Also tracks monster_pos in exp_state so AI loop uses correct spawn positions."""
    def send_npc_wrapper(nid, name, x, z, o):
        inst_id, attr = build_npc_attr_sproto(nid, name, x, z, o)
        NPC_INSTANCE_MAP[inst_id] = exp_run_map_id(exp_state)
        send_rpc_push(509, encode_sproto([(0, attr)]))
        # Fix: Track monster position in exp_state so AI loop uses correct spawn position
        if exp_state and 'monster_pos' in exp_state:
            exp_state['monster_pos'][inst_id] = [x / 100.0, z / 100.0]
            exp_state.setdefault('monster_heading', {})[inst_id] = int(o)
            exp_state.setdefault('spawned_npcs', set()).add(inst_id)
        return inst_id
    return send_npc_wrapper


def exp_npc_hp_lock(exp_state, inst_id):
    """Serialize HP/death commits for one EXP monster across squad threads."""
    locks = exp_state.setdefault('_npc_hp_locks', {})
    return locks.setdefault(int(inst_id), threading.Lock())


def flush_exp_kill_tracker(exp_state, conn, picked_char, send_rpc_push):
    """Credit each valid NPC death once, including partial waves at timeout."""
    if not exp_state:
        return
    kill_tracker = list(exp_state.get('kill_tracker', []))
    mission_chars = exp_participant_chars(exp_state) if exp_state.get('team_id') else []
    if not exp_state.get('team_id') and picked_char:
        mission_chars = [(int(picked_char.get('id', 0)), conn, picked_char)]
    for member_id, member_conn, member_char in mission_chars:
        member_push = (lambda tag, data, cid=member_id: send_push_to_char(cid, tag, data)) if exp_state.get('team_id') else send_rpc_push
        member_char['_skip_mission_save'] = True
        for unique_nid in kill_tracker:
            advance_missions(member_char, member_push, 'kill', target_id=unique_nid)
        advance_missions(member_char, member_push, 'level')
        member_char['_skip_mission_save'] = False
    exp_state['kill_tracker'] = []


def run_exp_monster_ai_shared(exp_state, map_str, picked_char, send_func, send_rpc_push_func, conn_for_sync, on_player_die):
    """Shared EXP dungeon monster AI loop.
    Bug 2: Extracted from duplicated main AI and relife AI loops to eliminate ~80 lines of duplicate code.
    
    Parameters:
        exp_state: EXP dungeon state dict
        map_str: Current map ID string
        picked_char: Player character dict
        send_func: Function(tag, data) for sending TAG 507/513/511 (e.g., push_wrapper or relife lambda)
        send_rpc_push_func: Function(tag, data) for sending TAG 506 (e.g., send_rpc_push or relife lambda)
        conn_for_sync: Connection object for sync_char_attrs_rpc
        on_player_die: Callback(conn, send_func, picked_char, exp_state) called when player dies
    """
    if not picked_char or not exp_state.get('ai_active') or str(picked_char.get('map_id')) != str(map_str):
        return False
    if exp_state.get('team_id'):
        live = exp_online_members(exp_state, alive=True)
        if not live: return bool(exp_state.get('ai_active'))
        _target_id, conn_for_sync, picked_char = live[0]
    elif picked_char.get('hp', 0) <= 0:
        return False

    # DailyExpData.GroupTime is a run/group setting, not permission to despawn
    # living NPCs or advance the wave. The client expects each configured
    # subwave to remain active until every spawned instance dies; the separate
    # CopySceneData exist_time timer ends the full run.

    player_pos = exp_state.get('player_auth_pos', {}).get(
        int(picked_char.get('id', 0)), picked_char.get('pos', [0, 100, 0, 0]))
    px = player_pos[0] / 100.0
    pz = player_pos[2] / 100.0
    player_stats = get_character_stats(picked_char)

    # EXP monsters carry their own AIDataID in NpcData.  Most currently use
    # 1007, but resolving it per monster keeps every tier's configured AI
    # (lock range, orbit range, and attack cooldown) authoritative.
    ai_cfg_default = {
        'lock_distance': 500, 'skill_cd_min': 1000, 'skill_cd_max': 3000,
        'around_min_distance': 100, 'around_max_distance': 200,
    }

    if 'attack_cd' not in exp_state:
        exp_state['attack_cd'] = {}

    current_time = time.time()

    for inst_id in list(exp_state.get('active_monsters', [])):
        auto_death_nid = None
        auto_death_hp_zero = False
        with exp_npc_hp_lock(exp_state, inst_id):
            already_dead = inst_id in DEAD_NPC_SET
            if not already_dead and NPC_HP_MAP.get(inst_id, 0) <= 0:
                auto_death_hp_zero = True
                auto_death_nid = NPC_INST_MAP.get(inst_id)
                if auto_death_nid:
                    # Mark under the same per-monster lock used by combat so
                    # another hit cannot land between zero HP and death.
                    DEAD_NPC_SET.add(inst_id)

        if already_dead:
            # Combat marks HP zero while holding the same per-NPC lock, then
            # sends the final HP update and calls on_npc_killed after
            # releasing it. Do not tear down its records here: an AI tick in
            # that small window would otherwise make the combat path lose the
            # HP/instance data needed to finish the client death notification.
            continue
        if auto_death_hp_zero:
            print(f"[EXP AI] Auto-death detected inst_id={inst_id} npcid={auto_death_nid} hp={NPC_HP_MAP.get(inst_id, 0)}")
            if auto_death_nid:
                death_stats = get_npc_attr(auto_death_nid)
                if exp_state.get('team_id'):
                    exp_sync_npc_attrs(exp_state, inst_id, death_stats, 0)
                else:
                    sync_npc_attrs_rpc(conn_for_sync, inst_id, death_stats, 0)
                on_npc_killed(conn_for_sync, send_rpc_push_func, picked_char, inst_id, auto_death_nid)
            else:
                if inst_id in exp_state.get('active_monsters', []):
                    exp_state['active_monsters'].remove(inst_id)
            continue
        target_nid = NPC_INST_MAP.get(inst_id)
        if not target_nid:
            continue

        monster_cfg = NPC_CONFIG.get(str(target_nid), {})
        ai_data_id = str(monster_cfg.get('ai_data_id') or '1007')
        ai_cfg = AI_DATA_CONFIG.get(ai_data_id, ai_cfg_default)
        configured_lock = int(ai_cfg.get('lock_distance', 500))
        search_radius = int(monster_cfg.get('search_radius', 0) or 0)
        # AIData LockDistance and NpcData Search are in centimeters.  A
        # negative lock distance means use the NPC's configured search range.
        lock_distance = (search_radius if configured_lock < 0 else configured_lock) / 100.0
        if search_radius > 0:
            lock_distance = min(lock_distance, search_radius / 100.0)
        around_min = int(ai_cfg.get('around_min_distance', 100)) / 100.0
        around_max = int(ai_cfg.get('around_max_distance', 200)) / 100.0
        attack_distance = max(0.5, (around_min + around_max) / 2.0)
        # NpcData.MoveSpeedMeter in the APK is MoveSpeed / 10. Keep the
        # authoritative chase step in the same world-units-per-second scale.
        move_speed = float(monster_cfg.get('move_speed', 50) or 50) / 10.0
        if move_speed <= 0:
            move_speed = 5.0
        move_speed_per_tick = move_speed * 0.2

        m_pos = exp_state['monster_pos'].setdefault(inst_id, [6.94, -11.37])
        if exp_state.get('team_id'):
            live_targets = exp_online_members(exp_state, alive=True)
            if not live_targets: continue
            _tid, target_conn, target_char = min(live_targets, key=lambda item: (
                exp_state.get('player_auth_pos', {}).get(int(item[0]), item[2].get('pos', [0,100,0,0]))[0] / 100.0 - m_pos[0]) ** 2 + (
                exp_state.get('player_auth_pos', {}).get(int(item[0]), item[2].get('pos', [0,100,0,0]))[2] / 100.0 - m_pos[1]) ** 2)
            conn_for_sync = target_conn; picked_char = target_char
            target_pos = exp_state.get('player_auth_pos', {}).get(
                int(picked_char.get('id', 0)), picked_char.get('pos', [0, 100, 0, 0]))
            px = target_pos[0] / 100.0; pz = target_pos[2] / 100.0
            player_stats = get_character_stats(picked_char)
        mx, mz = m_pos[0], m_pos[1]
        dx = px - mx
        dz = pz - mz
        dist = math.sqrt(dx * dx + dz * dz)

        if dist > lock_distance:
            with exp_npc_hp_lock(exp_state, inst_id):
                if (inst_id in DEAD_NPC_SET or NPC_HP_MAP.get(inst_id, 0) <= 0
                        or inst_id not in exp_state.get('active_monsters', [])):
                    continue
                stopped_monsters = exp_state.setdefault('movement_stopped', set())
                if inst_id not in stopped_monsters:
                    stopped_monsters.add(inst_id)
                    heading = int(math.degrees(math.atan2(dx, dz)) * 100)
                    exp_state.setdefault('monster_heading', {})[inst_id] = heading
                    pos_obj_stop = encode_sproto([(0, int(mx * 100)), (1, 0), (2, int(mz * 100)), (3, heading)])
                    m_move_stop = encode_sproto([(0, pos_obj_stop), (1, pos_obj_stop)])
                    char_move_stop = encode_sproto([(0, inst_id), (1, m_move_stop), (2, True)])
                    send_func(513, encode_sproto([(0, char_move_stop)]))
            continue

        if dist > attack_distance:
            with exp_npc_hp_lock(exp_state, inst_id):
                if (inst_id in DEAD_NPC_SET or NPC_HP_MAP.get(inst_id, 0) <= 0
                        or inst_id not in exp_state.get('active_monsters', [])):
                    continue
                exp_state.setdefault('movement_stopped', set()).discard(inst_id)
                step = min(move_speed_per_tick, dist - attack_distance)
                new_mx = mx + (dx / dist) * step
                new_mz = mz + (dz / dist) * step
                exp_state['monster_pos'][inst_id] = [new_mx, new_mz]
                heading = int(math.degrees(math.atan2(dx, dz)) * 100)
                exp_state.setdefault('monster_heading', {})[inst_id] = heading
                pos_obj = encode_sproto([(0, int(new_mx * 100)), (1, 0), (2, int(new_mz * 100)), (3, heading)])
                m_move = encode_sproto([(0, pos_obj), (1, pos_obj)])
                char_move = encode_sproto([(0, inst_id), (1, m_move), (2, True)])
                send_func(507, encode_sproto([(0, char_move)]))
        else:
            with exp_npc_hp_lock(exp_state, inst_id):
                if (inst_id in DEAD_NPC_SET or NPC_HP_MAP.get(inst_id, 0) <= 0
                        or inst_id not in exp_state.get('active_monsters', [])):
                    continue
                stopped_monsters = exp_state.setdefault('movement_stopped', set())
                if inst_id not in stopped_monsters:
                    stopped_monsters.add(inst_id)
                    heading = int(math.degrees(math.atan2(dx, dz)) * 100)
                    exp_state.setdefault('monster_heading', {})[inst_id] = heading
                    pos_obj_stop = encode_sproto([(0, int(mx * 100)), (1, 0), (2, int(mz * 100)), (3, heading)])
                    m_move_stop = encode_sproto([(0, pos_obj_stop), (1, pos_obj_stop)])
                    char_move_stop = encode_sproto([(0, inst_id), (1, m_move_stop), (2, True)])
                    send_func(513, encode_sproto([(0, char_move_stop)]))

                if inst_id not in exp_state['attack_cd']:
                    exp_state['attack_cd'][inst_id] = {
                        'last_attack': 0,
                        'skill_cd': random.uniform(ai_cfg['skill_cd_min'], ai_cfg['skill_cd_max']) / 1000.0
                    }
                cd_info = exp_state['attack_cd'][inst_id]
                if current_time - cd_info['last_attack'] < cd_info['skill_cd']:
                    continue  # Cooldown not ready, skip attack this tick
                cd_info['last_attack'] = current_time

                monster_stats = get_npc_attr(target_nid)
                monster_skill = monster_cfg.get('skill_group', '50001') or '50001'
                dmg, is_hit, is_cri = get_combat_damage(monster_stats, player_stats, skill_id=monster_skill, skill_lv=1)

                if is_hit and dmg > 0:
                    picked_char['hp'] = max(0, picked_char['hp'] - dmg)

                    attack_list_entry = encode_sproto([(0, picked_char['id']), (1, dmg)])
                    ret_skill = encode_sproto([
                        (0, inst_id),
                        (1, picked_char['id']),
                        (2, monster_skill),
                        (3, [attack_list_entry])
                    ])
                    send_rpc_push_func(508, ret_skill)

                    dmg_entry = encode_sproto([(0, picked_char['id']), (1, dmg), (2, monster_skill), (3, "10000"), (4, is_cri)])
                    dmg_board = encode_sproto([(0, [dmg_entry])])
                    send_rpc_push_func(511, dmg_board)
                    sync_char_attrs_rpc(conn_for_sync, picked_char)
                    if exp_state.get('team_id'):
                        broadcast_aoi_attribute(picked_char)
                        send_team_member_update(exp_state['team_id'], picked_char, force=picked_char.get('hp', 0) <= 0)
                    if picked_char['hp'] <= 0:
                        on_player_die(conn_for_sync, send_func, picked_char, exp_state)
                        if not exp_state.get('team_id'):
                            return False

    return exp_state.get('ai_active') and picked_char.get('map_id') == map_str


def exp_spawn_profile(exp_state, exp_cfg):
    """Select the APK's enemy-count/spawn-set variant from squad size.

    DailyExpData has parallel GroupNpcCount and Monsters arrays: one complete
    set for each supported party size (solo through four players). Keep the
    choice on the run state so reconnects or a member leaving cannot change
    the active run's spawn profile midway through.
    """
    counts = exp_cfg.get('group_npc_count', [5, 10, 15, 20])
    monsters = exp_cfg.get('monsters', [])
    if not isinstance(counts, list) or not counts:
        counts = [5, 10, 15, 20]
    if not isinstance(monsters, list) or not monsters:
        raise ValueError(f"EXP config has no spawn profiles copy={exp_state.get('copy_id')}")
    profile_count = min(len(counts), len(monsters))
    if profile_count < 1:
        raise ValueError(f"EXP config has no usable spawn profiles copy={exp_state.get('copy_id')}")

    if exp_state.get('spawn_profile_index') is None:
        member_count = max(1, len(exp_member_ids(exp_state)))
        profile_index = min(member_count - 1, profile_count - 1)
        exp_state['spawn_profile_index'] = profile_index
        exp_state['spawn_count_per_wave'] = max(1, int(counts[profile_index]))
    else:
        profile_index = max(0, min(profile_count - 1, int(exp_state['spawn_profile_index'])))
        exp_state['spawn_profile_index'] = profile_index
        exp_state['spawn_count_per_wave'] = max(
            1, int(exp_state.get('spawn_count_per_wave', counts[profile_index]))
        )
    return profile_index, exp_state['spawn_count_per_wave']


def exp_stage_kill_limits(exp_state, exp_cfg):
    """Return the exact group and run kill caps represented by client index."""
    _profile_index, per_wave = exp_spawn_profile(exp_state, exp_cfg)
    wave_count = max(1, int(exp_cfg.get('wave_count', 4)))
    group_count = max(1, int(exp_cfg.get('group_count', 7)))
    group_target = per_wave * wave_count
    return group_target, group_target * group_count


def _send_exp_tag683(send_rpc_push, exp_state, exp_cfg):
    group_target, total_target = exp_stage_kill_limits(exp_state, exp_cfg)
    profile_index = int(exp_state.get('spawn_profile_index', 0))
    group_kills = min(group_target, max(0, int(exp_state.get('wave_kills', 0))))
    total_kills = min(total_target, max(0, int(exp_state.get('total_kills', 0))))
    send_rpc_push(683, encode_sproto([
        # The APK uses index to select the party-size count in GroupNpcCount.
        # Its displayed caps are per-wave count * WaveCount * GroupCount.
        (0, str(exp_state['copy_id'])), (1, profile_index), (2, exp_state['end_time']),
        (3, exp_state['cur_group']),
        (4, group_kills), (5, total_kills)
    ]))

def spawn_exp_stage_subwave_internal(conn, send_rpc_push, picked_char, exp_state, exp_cfg, send_npc_func):
    wave_count = max(1, int(exp_cfg.get('wave_count', 4)))
    group_count = max(1, int(exp_cfg.get('group_count', 7)))
    cur_group = int(exp_state.get('cur_group', 1))
    cur_wave = int(exp_state.get('cur_wave', 1))
    if not 1 <= cur_group <= group_count or not 1 <= cur_wave <= wave_count:
        raise ValueError(f"invalid EXP stage position copy={exp_state.get('copy_id')} group={cur_group}/{group_count} wave={cur_wave}/{wave_count}")

    subwave = (cur_group - 1) * wave_count + cur_wave
    exp_state['subwave'] = subwave
    exp_state['active_monsters'] = []

    monster_groups = exp_cfg.get('monsters', [])
    if len(monster_groups) != wave_count:
        raise ValueError(f"EXP config wave/map mismatch copy={exp_state.get('copy_id')} waves={wave_count} maps={len(monster_groups)}")
    profile_index, expected_count = exp_spawn_profile(exp_state, exp_cfg)
    mon_group_id = monster_groups[profile_index]
    mon_entries = MONSTER_DATA.get(mon_group_id, [])

    # Each Monsters entry is a complete party-size variant containing every
    # one-based subwave group. GroupNpcCount[index] selects the row count.
    group_num = subwave
    subwave_spawns = [m for m in mon_entries if m.get('group') == group_num]
    group_npc_count = exp_cfg.get('group_npc_count', [5, 10, 15, 20])
    if not isinstance(group_npc_count, list) or len(group_npc_count) < wave_count:
        raise ValueError(f"EXP config has incomplete wave counts copy={exp_state.get('copy_id')} counts={group_npc_count}")
    if len(subwave_spawns) != expected_count:
        raise ValueError(
            f"EXP spawn table mismatch copy={exp_state.get('copy_id')} group={cur_group} wave={cur_wave} "
            f"profile={profile_index} map={mon_group_id} MonsterData.Group={group_num} "
            f"target={expected_count} actual={len(subwave_spawns)}"
        )

    for m in subwave_spawns:
        cfg = NPC_CONFIG.get(str(m['nid']))
        if cfg is None:
            raise ValueError(f"EXP monster has no NpcData config copy={exp_state.get('copy_id')} npc={m['nid']}")
        inst_id = send_npc_func(m['nid'], cfg['name'], m['x'], m['z'], m['o'])
        if inst_id and inst_id not in exp_state['active_monsters']:
            exp_state['active_monsters'].append(inst_id)
            exp_state.setdefault('spawned_npcs', set()).add(inst_id)
    if len(exp_state['active_monsters']) != expected_count:
        raise RuntimeError(
            f"EXP spawn did not create full wave copy={exp_state.get('copy_id')} group={cur_group} wave={cur_wave} "
            f"expected={expected_count} created={len(exp_state['active_monsters'])}"
        )

    print(
        f"[EXP STAGE] Spawned copy={exp_state['copy_id']} group={exp_state['cur_group']} "
        f"wave={exp_state['cur_wave']} subwave={subwave} profile={profile_index} "
        f"map={mon_group_id} target={expected_count} new={len(exp_state['active_monsters'])} monsters"
    )

def get_exp_stage_full_reward(char_lv):
    """Resolve exact EXP Stage reward from ShowRewardData (21000 + char_lv)."""
    try:
        lv = max(1, min(int(char_lv), 80))
    except (TypeError, ValueError):
        lv = 1

    reward_id = str(21000 + lv)
    rewards = SHOW_REWARD_CONFIG.get(reward_id, [])
    if rewards:
        for item_id, qual, count in rewards:
            if item_id == "2001":
                return count
    return 195000


def clear_exp_active_monsters(exp_state, send_rpc_push):
    """Despawn the current wave and retire its instance IDs exactly once."""
    if not exp_state:
        return
    spawned = exp_state.setdefault('spawned_npcs', set())
    for inst_id in list(exp_state.get('active_monsters', [])):
        if inst_id not in DEAD_NPC_SET:
            DEAD_NPC_SET.add(inst_id)
            send_rpc_push(506, encode_sproto([(0, inst_id)]))
        spawned.discard(inst_id)
        NPC_HP_MAP.pop(inst_id, None)
        NPC_INST_MAP.pop(inst_id, None)
        NPC_INSTANCE_MAP.pop(inst_id, None)
        NPC_ATTR_MAP.pop(inst_id, None)
        exp_state.get('monster_pos', {}).pop(inst_id, None)
        exp_state.get('monster_heading', {}).pop(inst_id, None)
        exp_state.get('attack_cd', {}).pop(inst_id, None)
    exp_state['active_monsters'] = []

def finish_exp_team_stage(conn, push, owner, state, win):
    if state.get('team_finish_handled'): return
    state['team_finish_handled']=True; state['ai_active']=False
    flush_exp_kill_tracker(state, conn, owner, push)
    clear_exp_active_monsters(state, push)
    for key in ('timeout_timer','ai_timer'):
        timer=state.get(key)
        if timer: timer.cancel()
    copy_id=str(state['copy_id']); cfg=DAILY_EXP_CONFIG.get(copy_id,{})
    _group_target, expected = exp_stage_kill_limits(state, cfg)
    expected=max(1,expected)
    ratio=min(1.0,max(0.0,int(state.get('total_kills',0))/expected))
    flag=0; grade=0
    if win:
        flag|=1;grade+=1
        if ratio>=0.8:flag|=2;grade+=1
        if int(state.get('deaths',0))==0:flag|=4;grade+=1
    subtype=(COPY_SCENE_CONFIG.get(copy_id) or {}).get('subtype',12)
    members=exp_participant_chars(state)
    if state.get('team_id'):
        for cid,_member_conn,_char in members: broadcast_aoi_remove(cid, copy_id)
    for cid,member_conn,char in members:
        amount=int(get_exp_stage_full_reward(char.get('level',1))*ratio*(1 if win else 0.5)); cash=int(amount*0.1)
        send=lambda tag,data,who=cid:send_push_to_char(who,tag,data)
        if amount or cash:grant_item_rewards(char,[("2001",0,amount),("1001",0,cash)],member_conn or conn,send,save=False)
        items=[encode_sproto([(0,'2001'),(1,amount),(3,0)]),encode_sproto([(0,'1001'),(1,cash),(3,0)])]
        result=encode_sproto([(0,subtype),(1,copy_id),(2,bool(win)),(3,flag),(4,grade),(5,items)])
        if member_conn and str(char.get('map_id')) == exp_run_map_id(state):
            send_exp_result_to_char(char,result)
        else:
            char['pending_exp_result']=result.hex()
        if win:
            advance_missions(char,send,'dungeon',target_id='105');advance_missions(char,send,'level')
        char.pop('_skip_mission_save',None)
    save_chars(all_accounts_chars)
    delay=5 if win else (3 if int(time.time())>=int(state.get('end_time',0)) else 0)
    for cid,_member_conn,char in members:
        pos=char.get('pre_copy_pos'); mid=char.get('pre_copy_map','11')
        char['_exp_return_pos']=pos;char['_exp_return_map']=mid
        char.pop('exp_stage_state',None);char['active_copy_id']=None;char['exp_team_id']=None
        char.pop('pre_copy_pos',None);char.pop('pre_copy_map',None)
        def leave(who=cid,ch=char,retpos=pos,retmap=mid):
            ch.pop('_exp_leave_timer',None);active,online=ALL_CONNECTIONS.get(who,(None,None))
            if active and online is ch:
                start_map_transition(active,ch,retmap,lambda tag,data,target=who:send_push_to_char(target,tag,data),override_pos=retpos)
            else:
                if retpos:ch['pos']=retpos
                ch['map_id']=retmap;save_chars(all_accounts_chars)
        if delay:
            timer=threading.Timer(delay,leave);timer.daemon=True;char['_exp_leave_timer']=timer;timer.start()
        else: leave()
    team=TEAM_STATES.get(int(state.get('team_id') or 0))
    if team:team['active_run']=None;team['pending_run']=None;send_team_update(team['id'])
    save_chars(all_accounts_chars)

def finish_exp_stage(conn, send_rpc_push, picked_char, exp_state, win=True, force_terminal=False):
    if not exp_state:
        return
    run_lock = exp_state.setdefault('_kill_lock', threading.RLock())
    with run_lock:
        # Finish, timeout, and last-kill callbacks can race on separate
        # client/timer threads. Keep the result and reward path single-shot.
        if exp_state.get('_finish_handled'):
            return
        if exp_state.get('team_id'):
            exp_state['_finish_handled'] = True
            finish_exp_team_stage(conn, send_rpc_push, picked_char, exp_state, win)
            return
        if win or force_terminal or int(time.time()) >= int(exp_state.get('end_time', 0)):
            exp_state['_finish_handled'] = True
        return _finish_exp_stage_impl(conn, send_rpc_push, picked_char, exp_state, win, force_terminal)


def _finish_exp_stage_impl(conn, send_rpc_push, picked_char, exp_state, win=True, force_terminal=False):
    if exp_state.get('team_id'):
        finish_exp_team_stage(conn,send_rpc_push,picked_char,exp_state,win)
        return
    flush_exp_kill_tracker(exp_state, conn, picked_char, send_rpc_push)
    is_timeout = int(time.time()) >= int(exp_state.get('end_time', 0))
    terminal = bool(win or is_timeout or force_terminal)
    exp_state['ai_active'] = False
    timeout_timer = exp_state.get('timeout_timer')
    if terminal and timeout_timer is not None:
        timeout_timer.cancel()
    if terminal:
        clear_exp_active_monsters(exp_state, send_rpc_push)
    elif picked_char:
        # A solo death pauses the run and opens the client's revive/exit UI.
        # Do not award partial run rewards until the player actually exits or
        # the timer ends; otherwise every death can pay out the same kills.
        char_id = int(picked_char.get('id', 0))
        dead_members = exp_state.get('dead_members') or []
        exp_state['dead_members'] = dead_members
        if char_id in [int(cid) for cid in dead_members]:
            return
        dead_members.append(char_id)
        exp_state['deaths'] = int(exp_state.get('deaths', 0)) + 1
        relife_cfg = get_relife_config(exp_state['deaths'])
        send_rpc_push(618, encode_sproto([
            (0, 1), (1, relife_cfg.get('use_count', 1)), (2, '9202')
        ]))
        return
    copy_id = exp_state['copy_id']
    char_lv = picked_char.get('level', 1) if picked_char else 1

    full_exp = get_exp_stage_full_reward(char_lv)
    exp_cfg = DAILY_EXP_CONFIG.get(copy_id, {})
    _group_target, total_max_kills = exp_stage_kill_limits(exp_state, exp_cfg)
    total_max_kills = max(1, total_max_kills)
    ratio = min(1.0, exp_state['total_kills'] / total_max_kills) if total_max_kills > 0 else 1.0

    exp_reward = int(full_exp * ratio) if win else int(full_exp * ratio * 0.5)
    cash_reward = int(exp_reward * 0.1)

    if picked_char:
        rewards = [("2001", 0, exp_reward), ("1001", 0, cash_reward)]
        grant_item_rewards(picked_char, rewards, conn, send_rpc_push, save=True)

    res_items = [
        encode_sproto([(0, "2001"), (1, exp_reward), (3, 0)]),
        encode_sproto([(0, "1001"), (1, cash_reward), (3, 0)])
    ]

    gradeFlag = 0
    grade = 0
    if win:
        gradeFlag |= 1
        grade += 1
        kill_ratio = exp_state.get('total_kills', 0) / total_max_kills if total_max_kills > 0 else 0
        if kill_ratio >= 0.8:
            gradeFlag |= 2
            grade += 1
        if exp_state.get('deaths', 0) == 0:
            gradeFlag |= 4
            grade += 1

    if picked_char:
        if win:
            copy_cfg = COPY_SCENE_CONFIG.get(copy_id, {})
            subtype = copy_cfg.get('subtype', 12)
            result = encode_sproto([
                (0, subtype), (1, str(copy_id)), (2, win), (3, gradeFlag), (4, grade), (5, res_items)
            ])
            send_exp_result_to_char(picked_char, result)
            advance_missions(picked_char, send_rpc_push, 'dungeon', target_id='105')
            advance_missions(picked_char, send_rpc_push, 'level')
            picked_char.pop('_skip_mission_save', None)
            save_chars(all_accounts_chars)
            send_rpc_push(519, sync_mission_data(picked_char))
            saved_pos = picked_char.get('pre_copy_pos')
            saved_map = picked_char.get('pre_copy_map', '11')
            picked_char['_exp_return_pos'] = saved_pos
            picked_char['_exp_return_map'] = saved_map
            picked_char['pre_copy_pos'] = None
            picked_char['pre_copy_map'] = None
            picked_char['active_copy_id'] = None
            picked_char['exp_team_id'] = None
            picked_char.pop('exp_members', None)
            picked_char['exp_defer_start'] = False
            def leave_exp_copy():
                try:
                    active_conn = ALL_CONNECTIONS.get(int(picked_char.get('id', 0)), (None, None))[0]
                    if active_conn:
                        start_map_transition(active_conn, picked_char, saved_map, lambda tag, data: send_push_to_char(picked_char['id'], tag, data), override_pos=saved_pos)
                    else:
                        if saved_pos: picked_char['pos'] = saved_pos
                        picked_char['map_id'] = saved_map
                        save_chars(all_accounts_chars)
                except Exception:
                    if saved_pos: picked_char['pos'] = saved_pos
                    picked_char['map_id'] = saved_map
                    save_chars(all_accounts_chars)
                finally:
                    picked_char.pop('_exp_leave_timer', None)

            timer = threading.Timer(5.0, leave_exp_copy)
            timer.daemon = True
            picked_char['_exp_leave_timer'] = timer
            picked_char.pop('exp_stage_state', None)
            timer.start()
        else:
            is_timeout = int(time.time()) >= exp_state.get('end_time', 0)

            if terminal:
                if is_timeout:
                    exp_state['deaths'] = exp_state.get('deaths', 0) + 1
                copy_cfg = COPY_SCENE_CONFIG.get(copy_id, {})
                subtype = copy_cfg.get('subtype', 12)
                result = encode_sproto([
                    (0, subtype), (1, str(copy_id)), (2, False), (3, gradeFlag), (4, grade), (5, res_items)
                ])
                send_exp_result_to_char(picked_char, result)
                picked_char.pop('exp_stage_state', None)
                picked_char['active_copy_id'] = None
                picked_char['exp_team_id'] = None
                picked_char.pop('exp_members', None)
                picked_char['exp_defer_start'] = False
                saved_pos = picked_char.get('pre_copy_pos')
                saved_map = picked_char.get('pre_copy_map', '11')
                picked_char['_exp_return_pos'] = saved_pos; picked_char['_exp_return_map'] = saved_map
                picked_char['pre_copy_pos'] = None
                picked_char['pre_copy_map'] = None
                def leave_after_timeout():
                    try:
                        active_conn = ALL_CONNECTIONS.get(int(picked_char.get('id', 0)), (None, None))[0]
                        if active_conn:
                            start_map_transition(active_conn, picked_char, saved_map, lambda tag, data: send_push_to_char(picked_char['id'], tag, data), override_pos=saved_pos)
                        else:
                            if saved_pos: picked_char['pos'] = saved_pos
                            picked_char['map_id'] = saved_map
                            save_chars(all_accounts_chars)
                    except Exception:
                        if saved_pos: picked_char['pos'] = saved_pos
                        picked_char['map_id'] = saved_map
                        save_chars(all_accounts_chars)
                    finally: picked_char.pop('_exp_leave_timer', None)
                timer = threading.Timer(3.0, leave_after_timeout)
                timer.daemon = True; picked_char['_exp_leave_timer'] = timer
                timer.start()
            else:
                exp_state['deaths'] = exp_state.get('deaths', 0) + 1
                death_count = exp_state['deaths']
                relife_cfg = get_relife_config(death_count)
                relife_req = encode_sproto([
                    (0, 1),
                    (1, relife_cfg.get('use_count', 1)),  # cost field - item cost
                    (2, "9202")
                ])
                send_rpc_push(618, relife_req)

    print(f"[EXP STAGE] Finished copy={copy_id} win={win} total_kills={exp_state['total_kills']} exp={exp_reward} cash={cash_reward}")

def on_npc_killed(conn, send_rpc_push, picked_char, inst_id, npcid):
    state = picked_char.get('exp_stage_state') if picked_char else None
    if state and inst_id:
        run_lock = state.setdefault('_kill_lock', threading.RLock())
        with run_lock:
            return _on_npc_killed_locked(conn, send_rpc_push, picked_char, inst_id, npcid)
    return _on_npc_killed_locked(conn, send_rpc_push, picked_char, inst_id, npcid)


def _on_npc_killed_locked(conn, send_rpc_push, picked_char, inst_id, npcid):
    if not picked_char: return

    exp_state = picked_char.get('exp_stage_state')
    exp_push = (lambda tag, data: exp_send_push(exp_state, tag, data)) if exp_state and exp_state.get('team_id') else send_rpc_push
    exp_kill_counted = False
    exp_wave_completed = False
    if exp_state and inst_id:
        kill_lock = exp_state.setdefault('_kill_lock', threading.RLock())
        with kill_lock:
            if exp_state.get('_finish_handled'):
                print(f"[EXP KILL] IGNORE completed run inst_id={inst_id} copy={exp_state.get('copy_id')}")
                return
            killed_list = exp_state.get('killed_npcs', [])
            if inst_id in killed_list:
                print(f"[EXP KILL] DUPLICATE skip inst_id={inst_id} already processed")
                return
            spawned_npcs = exp_state.setdefault('spawned_npcs', set(exp_state.get('active_monsters', [])))
            if inst_id not in spawned_npcs:
                # Only an instance spawned for this run can count. This rejects
                # late packets for timed-out/despawned enemies and other NPCs.
                print(f"[EXP KILL] IGNORE untracked inst_id={inst_id} copy={exp_state.get('copy_id')}")
                return
            npcid = NPC_INST_MAP.get(inst_id) or npcid
            killed_list.append(inst_id)
            exp_state['killed_npcs'] = killed_list
            spawned_npcs.discard(inst_id)
            active_monsters = exp_state.get('active_monsters', [])
            if inst_id in active_monsters:
                active_monsters.remove(inst_id)
            exp_state['wave_kills'] = int(exp_state.get('wave_kills', 0)) + 1
            exp_state['total_kills'] = int(exp_state.get('total_kills', 0)) + 1
            exp_kill_counted = True
            exp_wave_completed = not active_monsters

    valid_kill = npcid and npcid != "None"

    print(f"[EXP KILL] on_npc_killed inst_id={inst_id} npcid={npcid} valid={valid_kill} exp_state={exp_state is not None}")
    if exp_state and inst_id:
        active_monsters = exp_state.get('active_monsters', [])
        print(f"[EXP KILL] active_monsters_remaining={len(active_monsters)}")

        # Count every spawned instance exactly once, even if its NpcData ID
        # cannot be resolved for mission credit.
        if exp_kill_counted:
            print(f"[EXP KILL] COUNTED wave_kills={exp_state['wave_kills']} total_kills={exp_state['total_kills']} wave={exp_state['cur_wave']} group={exp_state['cur_group']}")
        if valid_kill:
            kt = exp_state.get('kill_tracker', [])
            # Mission progress is per defeated instance.  A wave can contain
            # many copies of the same NPC ID, so keep duplicates here and
            # credit each death when the wave closes.
            kt.append(npcid)

        exp_cfg = DAILY_EXP_CONFIG.get(exp_state['copy_id'], {})
        _send_exp_tag683(exp_push, exp_state, exp_cfg)

        if exp_wave_completed:
            flush_exp_kill_tracker(exp_state, conn, picked_char, exp_push)
            if exp_state['cur_wave'] < exp_cfg.get('wave_count', 4):
                exp_state['cur_wave'] += 1
                spawn_exp_stage_subwave_internal(conn, exp_push, picked_char, exp_state, exp_cfg, _build_exp_npc_wrapper(exp_push, exp_state))
                _send_exp_tag683(exp_push, exp_state, exp_cfg)
            else:
                if exp_state['cur_group'] < exp_cfg.get('group_count', 7):
                    exp_state['cur_group'] += 1
                    exp_state['cur_wave'] = 1
                    exp_state['wave_kills'] = 0
                    exp_state['group_start_time'] = int(time.time())
                    spawn_exp_stage_subwave_internal(conn, exp_push, picked_char, exp_state, exp_cfg, _build_exp_npc_wrapper(exp_push, exp_state))
                    _send_exp_tag683(exp_push, exp_state, exp_cfg)
                else:
                    finish_exp_stage(conn, exp_push, picked_char, exp_state, win=True)

    if inst_id:
        NPC_HP_MAP.pop(inst_id, None)
        NPC_INST_MAP.pop(inst_id, None)
        NPC_INSTANCE_MAP.pop(inst_id, None)
        NPC_ATTR_MAP.pop(inst_id, None)
        exp_state.get('monster_pos', {}).pop(inst_id, None) if exp_state else None
        exp_state.get('monster_heading', {}).pop(inst_id, None) if exp_state else None
        exp_state.get('attack_cd', {}).pop(inst_id, None) if exp_state else None

    if valid_kill:
        npc_stats = get_npc_attr(npcid)
        npc_lv = npc_stats.get('lv', 1)
        char_lv = picked_char.get('level', 1)

        exp_kill, cash_kill = calculate_npc_kill_rewards(char_lv, npc_lv)
        kill_rewards = [("2001", 0, exp_kill), ("1001", 0, cash_kill)]
        if exp_state and exp_state.get('team_id'):
            popup = encode_sproto([(0, [encode_sproto([(0, '2001'), (1, exp_kill), (3, 0)]), encode_sproto([(0, '1001'), (1, cash_kill), (3, 0)])])])
            for member_id, member_conn, member_char in exp_participant_chars(exp_state):
                member_push = lambda tag, data, cid=member_id: send_push_to_char(cid, tag, data)
                grant_item_rewards(member_char, kill_rewards, member_conn, member_push, save=False)
                member_push(638, popup)
        else:
            grant_item_rewards(picked_char, kill_rewards, conn, send_rpc_push, save=False)




def start_exp_stage_runtime(conn, owner, state):
    if not state or state.get('runtime_started') or state.get('defer_start'): return
    state['runtime_started'] = True
    def current_owner():
        owner_id = int(state.get('owner_id', owner.get('id', 0)))
        return ALL_CONNECTIONS.get(owner_id, (None, state.get('char_refs', {}).get(owner_id)))[1] or owner
    def push(tag, data): exp_send_push(state, tag, data)
    def on_player_die(dead_conn, _send, dead_char, run):
        if run.get('team_id'):
            dead_ids = run.setdefault('dead_members', [])
            if int(dead_char['id']) not in dead_ids:
                dead_ids.append(int(dead_char['id'])); run['deaths'] = int(run.get('deaths', 0)) + 1
            cost = get_relife_config(run['deaths']).get('use_count', 1)
            send_push_to_char(dead_char['id'], 618, encode_sproto([(0, 1), (1, cost), (2, '9202')]))
        else:
            finish_exp_stage(dead_conn or conn, push, dead_char, run, win=False)
    def tick():
        if not state.get('ai_active'): return
        char = current_owner()
        if not char: return
        active_conn = ALL_CONNECTIONS.get(int(char.get('id', 0)), (conn, char))[0] or conn
        try: more = run_exp_monster_ai_shared(state, exp_run_map_id(state), char, push, push, active_conn, on_player_die)
        except Exception as exc:
            print(f'[EXP AI] {exc}'); more = bool(state.get('ai_active'))
        if more and state.get('ai_active'):
            t = threading.Timer(0.2, tick); t.daemon = True; state['ai_timer'] = t; t.start()
    def timeout():
        run_lock = state.setdefault('_kill_lock', threading.RLock())
        with run_lock:
            if not state.get('_finish_handled') and int(time.time()) >= int(state.get('end_time', 0)):
                state['ai_active'] = False
                char = current_owner()
                active_conn = ALL_CONNECTIONS.get(int(char.get('id', 0)), (conn, char))[0] or conn if char else conn
                finish_exp_stage(active_conn, push, char, state, win=False)
    t = threading.Timer(max(1, int(state.get('end_time', time.time()+1))-int(time.time())), timeout)
    t.daemon = True; state['timeout_timer'] = t; t.start()
    t = threading.Timer(0.2, tick); t.daemon = True; state['ai_timer'] = t; t.start()


def spawn_map_npcs(conn, map_id, picked_char=None):
    map_str = str(map_id)
    spawned_maps = NPC_SPAWNED_MAPS.setdefault(id(conn), set())
    if map_str in spawned_maps: return
    spawned_maps.add(map_str)
    def send_npc_create(nid, name, x, z, o):
        inst_id, attr = build_npc_attr_sproto(nid, name, x, z, o)
        NPC_INSTANCE_MAP[inst_id] = map_str
        send_rpc_push_safe(conn, 509, encode_sproto([(0, attr)]))
        return inst_id
    if is_exp_map(map_str) and picked_char:
        state=picked_char.get('exp_stage_state')
        copy_id=str(picked_char.get('active_copy_id') or '')
        if copy_id not in DAILY_EXP_CONFIG and isinstance(state, dict):
            copy_id=str(state.get('copy_id') or '')
        if copy_id not in DAILY_EXP_CONFIG and map_str in DAILY_EXP_CONFIG:
            copy_id=map_str
        cfg=DAILY_EXP_CONFIG.get(copy_id)
        if not cfg:
            # EXP scenes use ordered DailyExpData waves. Static map rows must
            # not be mixed into the run or they create uncounted extra enemies.
            print(f"[EXP STAGE] no active copy config for physical map={map_str}; suppress static spawn")
            return
        if state is None and picked_char.get('_exp_leave_timer'):
            # A finished run can reconnect during its short results/exit delay.
            # Do not mistake that terminal map for a fresh solo run.
            print(f"[EXP STAGE] suppress respawn during pending exit id={picked_char.get('id')} copy={copy_id}")
            return
        resuming=bool(state and state.get('run_initialized')
                      and str(state.get('copy_id'))==copy_id
                      and exp_run_map_id(state)==map_str)
        if not resuming:
            now=int(time.time()); members=picked_char.get('exp_members') or [int(picked_char.get('id',0))]
            duration=int((COPY_SCENE_CONFIG.get(copy_id) or {}).get('exist_time') or 600)
            state={'copy_id':copy_id,'map_id':map_str,'members':list(dict.fromkeys(int(x) for x in members)),
                'owner_id':int(picked_char.get('id',0)),'team_id':picked_char.get('exp_team_id'),'char_refs':{},
                'cur_group':1,'cur_wave':1,'subwave':1,'wave_kills':0,'total_kills':0,'deaths':0,
                'start_time':now,'end_time':now+duration,'group_start_time':now,'active_monsters':[],'monster_pos':{},'monster_heading':{},
                'ai_active':True,'run_initialized':True,'runtime_started':False,
                'defer_start':bool(picked_char.get('exp_defer_start')),'kill_tracker':[],'killed_npcs':[],'spawned_npcs':set()}
            for cid in state['members']:
                member=ALL_CONNECTIONS.get(cid,(None,None))[1]
                if member is not None: state['char_refs'][cid]=member; member['exp_stage_state']=state
            picked_char['exp_stage_state']=state
        state.setdefault('char_refs',{})[int(picked_char.get('id',0))]=picked_char
        state.setdefault('player_auth_pos', {}).setdefault(
            int(picked_char.get('id', 0)), list(picked_char.get('pos', [0, 100, 0, 0])))
        state.setdefault('player_auth_move_at', {}).setdefault(int(picked_char.get('id', 0)), time.monotonic())
        def push(tag,data): exp_send_push(state,tag,data)
        push(629,encode_sproto([(0,state['end_time']),(1,2)])); _send_exp_tag683(push,state,cfg)
        if resuming:
            # TAG 618 is a one-shot push.  A player who reconnects while down
            # has missed it, so replay the revive prompt after the stage UI
            # state is restored.
            if (int(picked_char.get('hp', 0)) <= 0
                    and not state.get('_finish_handled')
                    and int(time.time()) < int(state.get('end_time', 0))):
                relife_cost = max(1, int(get_relife_config(max(1, int(state.get('deaths', 0)))).get('use_count', 1)))
                send_rpc_push_safe(conn, 618, encode_sproto([(0, 1), (1, relife_cost), (2, '9202')]))
            for inst in state.get('active_monsters',[]):
                attr=NPC_ATTR_MAP.get(inst)
                if attr is not None:
                    nid=NPC_INST_MAP.get(inst)
                    pos=state.get('monster_pos',{}).get(inst)
                    if nid and pos is not None:
                        npc_cfg=NPC_CONFIG.get(str(nid),{})
                        _inst,attr=build_npc_attr_sproto(
                            nid,npc_cfg.get('name',f"Monster_{nid}"),
                            int(round(pos[0]*100)),int(round(pos[1]*100)),
                            int(state.get('monster_heading',{}).get(inst,0)),
                            existing_inst_id=inst)
                        NPC_INSTANCE_MAP[inst] = map_str
                    send_rpc_push_safe(conn,509,encode_sproto([(0,attr)]))
                    if nid:
                        stats=get_npc_attr(nid)
                        sync_npc_attrs_rpc(conn,inst,stats,NPC_HP_MAP.get(inst,stats['hp_max']))
            start_exp_stage_runtime(conn,picked_char,state)
            return
        def create_exp_npc(nid,name,x,z,o):
            inst,attr=build_npc_attr_sproto(nid,name,x,z,o)
            NPC_INSTANCE_MAP[inst] = map_str
            if inst:
                state['monster_pos'][inst]=[x/100.0,z/100.0]
                state.setdefault('monster_heading',{})[inst]=int(o)
                push(509,encode_sproto([(0,attr)]))
                state.setdefault('spawned_npcs',set()).add(inst)
            return inst
        spawn_exp_stage_subwave_internal(conn,push,picked_char,state,cfg,create_exp_npc)
        start_exp_stage_runtime(conn,picked_char,state)
        return
    if map_str != "11" and map_str in STATIC_NPC_DATA:
        for m in STATIC_NPC_DATA[map_str]:
            cfg = NPC_CONFIG.get(m['nid'], {'name': f"NPC_{m['nid']}"})
            send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    if map_str != "11" and map_str in MONSTER_DATA:
        for i, m in enumerate(MONSTER_DATA[map_str]):
            cfg = NPC_CONFIG.get(m['nid'], {'name': f"Monster_{m['nid']}"})
            send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    if map_str == "11":
        return
    if picked_char:
        for mid, mdata in picked_char.get('active_missions', {}).items():
            if mdata['state'] == 1:
                cfg = missions_data.get(mid)
                if cfg:
                    logic_id = str(cfg.get('logic_id', ''))
                    spawn_key = logic_id if logic_id in KILL_TARGET_SPAWNS else str(mid)
                    if spawn_key in KILL_TARGET_SPAWNS:
                        for s in KILL_TARGET_SPAWNS[spawn_key]:
                            if str(s['map']) == map_str:
                                for _ in range(s['num']): send_npc_create(s['nid'], f"Quest_{s['nid']}", s['x'], s['z'], 0)

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

def build_mount_info(picked_char):
    """Build the exact mount map consumed by the APK garage handlers.

    Vehicle meshes, icons and colour shaders remain APK/resource-bundle data;
    this state only records whether a garage vehicle is owned/equipped and
    which of the MountData colours have been unlocked/selected.

    Only vehicles with NeedShow=1 are sent to the garage UI.  Vehicles with
    NeedShow=0 (motorcycles, GTA traffic) remain in MOUNT_CONFIG for server-side
    resolution but are not displayed in the garage.
    """
    mount_state = picked_char.setdefault('mounts', {})
    result = {}
    for mount_id, cfg in MOUNT_CONFIG.items():
        # Filter: only send NeedShow=1 vehicles to the garage UI
        if cfg.get('need_show', 0) != 1:
            continue
        saved = mount_state.setdefault(mount_id, {})
        state = int(saved.get('state', 0))
        selected = str(saved.get('select') or cfg['default_color'])
        if selected not in cfg['colors']:
            selected = cfg['default_color']
        unlocked = set(str(x) for x in saved.get('unlocked_colors', [cfg['default_color']]))
        unlocked.add(cfg['default_color'])
        colors = {}
        for color_id in cfg['colors']:
            colors[color_id] = encode_sproto([(0, color_id), (1, 1 if color_id in unlocked else 0)])
        result[mount_id] = encode_sproto([(0, mount_id), (1, state), (2, colors), (3, selected)])
    return result

def mount_id_from_voucher(item_id):
    for mount_id, cfg in MOUNT_CONFIG.items():
        if cfg.get('item_id') == str(item_id):
            return mount_id
    return None

def field_text(fields, tag, default=''):
    value = fields.get(tag, default)
    if isinstance(value, (bytes, bytearray)):
        return value.decode('utf-8', errors='replace')
    return str(value) if value is not None else default

def current_daily_stamp():
    """The server's daily-copy reset key (UTC calendar day adjusted for 03:00 AM reset)."""
    adjusted_time = time.time() - (3 * 3600)
    return time.strftime('%Y-%m-%d', time.gmtime(adjusted_time))

def current_sign_calendar():
    """Use the same 03:00 server reset for daily/weekly/monthly sign state."""
    adjusted = time.gmtime(time.time() - (3 * 3600))
    return {
        'date': time.strftime('%Y-%m-%d', adjusted),
        'week_day': adjusted.tm_wday + 1,
        'month_day': adjusted.tm_mday,
    }

def ensure_daily_copy_state(picked_char):
    """Reset the APK-configured daily attempts once per calendar day at 03:00 AM."""
    state = picked_char.setdefault('daily_copy_state', {})
    stamp = current_daily_stamp()
    if state.get('day') != stamp:
        state['day'] = stamp
        state['remaining'] = {}
        # Do NOT reset best_times on daily reset - best times are persistent records
    return state

def calculate_npc_kill_rewards(player_level, npc_level=1):
    """Calculates balanced EXP and Cash rewards for killing/hitting an NPC based on BaseLvData."""
    try:
        player_level = max(1, min(int(player_level), 80))
    except (TypeError, ValueError):
        player_level = 1

    try:
        npc_level = int(npc_level)
        if npc_level > 200 or npc_level <= 0:
            npc_level = player_level
    except (TypeError, ValueError):
        npc_level = player_level

    # Effective level capped to prevent low-level power leveling
    effective_lv = min(player_level, npc_level + 2)

    # Get required EXP for effective level from BaseLvData
    req_data = LEVEL_DATA.get(effective_lv, LEVEL_DATA.get(1, {'exp': 400}))
    req_exp = req_data.get('exp', 400)

    # Grant ~0.8% of level required EXP per NPC kill
    exp_reward = max(3, int(req_exp * 0.008))

    # Cash reward scales smoothly with effective level
    cash_reward = max(10, effective_lv * 15 + 10)

    return exp_reward, cash_reward

def copy_attempts_remaining(picked_char, copy_id, cfg):
    state = ensure_daily_copy_state(picked_char)
    remaining = state.setdefault('remaining', {})
    # Issue 63: Removed "or copy_id in [...]" — it reset the counter on every call, bypassing the daily limit
    if copy_id not in remaining:
        remaining[copy_id] = int(cfg.get('max_plays', 3))
    return max(0, int(remaining[copy_id]))

def street_race_rewards(level):
    """Return exactly the level-resolved _drop_bc ShowRewardData items."""
    try:
        level = int(level)
    except (TypeError, ValueError):
        level = 1
    level = max(1, min(level, 80))
    reward_id = STREET_RACE_REWARD_BY_LEVEL.get(level)
    if not reward_id:
        eligible = [lv for lv in STREET_RACE_REWARD_BY_LEVEL if lv <= level]
        reward_id = STREET_RACE_REWARD_BY_LEVEL[max(eligible)] if eligible else ''
    return SHOW_REWARD_CONFIG.get(str(reward_id), [])

def sync_copy_scenes(picked_char):
    """Build TAG 555 from the APK's CopySceneData definitions."""
    level = int(picked_char.get('level', 1))
    # CopySceneData contains the level variants for each daily activity.  The
    # client expects one current copy per subtype, not every future/parallel
    # Street Race row.  Select the highest unlocked level bracket; ties use
    # the first configured ID (e.g. Street Race 211 at level 4).
    selected = {}
    for copy_id, cfg in COPY_SCENE_CONFIG.items():
        if level < cfg['min_level']:
            continue
        # Fix: Also filter by max_level — don't show copies the player is overleveled for
        if level > cfg.get('max_level', 80):
            continue
        try:
            numeric_id = int(copy_id)
        except (TypeError, ValueError):
            numeric_id = 0
        rank = (int(cfg['min_level']), -numeric_id)
        subtype = cfg['subtype']
        if subtype not in selected or rank > selected[subtype][0]:
            selected[subtype] = (rank, copy_id, cfg)
    copies = {}
    for _, copy_id, cfg in selected.values():
        # copyscene_info: ID, CurNum, BestGrade, Type, str, enable, state, Type2.
        # Type=1 marks a daily copy.  The client finds the Street Race entry
        # through CopySceneData.SubType == 7, rather than a server-made ID.
        state = ensure_daily_copy_state(picked_char)
        best_grade = state.get('best_times', {}).get(copy_id)
        best_str = state.get('best_times_str', {}).get(copy_id)

        info_fields = [
            (0, str(copy_id)),
            (1, copy_attempts_remaining(picked_char, copy_id, cfg)),
            (3, 1),
            (5, True),
            (6, 0),
            (7, cfg['subtype'])
        ]
        if best_grade is not None:
            info_fields.append((2, int(best_grade)))
        if best_str:
            info_fields.append((4, str(best_str)))

        copies[str(copy_id)] = encode_sproto(info_fields)
    return encode_sproto([(0, copies)])

def grant_item_rewards(picked_char, rewards_list, conn=None, send_rpc_push=None, save=True):
    """Grants EXP, Cash, and Inventory items, handles level-ups, and syncs attributes.

    Args:
        save: If False, skip save_chars() to avoid excessive disk I/O (e.g., per-kill in EXP dungeon).
              Caller is responsible for saving at appropriate intervals.
    """
    exp_gained = 0
    cash_gained = 0
    inv_changed = False

    for entry in rewards_list:
        item_id = str(entry[0])
        amount = int(entry[2]) if len(entry) > 2 else int(entry[1])
        if item_id == "2001":  # EXP
            exp_gained += amount
        elif item_id == "1001":  # Cash
            cash_gained += amount
        else:  # Inventory items / currency
            add_to_inventory(picked_char, item_id, amount)
            inv_changed = True

    if exp_gained > 0:
        picked_char['exp'] = picked_char.get('exp', 0) + exp_gained
        old_level = picked_char.get('level', 1)
        leveled_up = False
        while True:
            lv = picked_char.get('level', 1)
            rd = LEVEL_DATA.get(lv)
            if rd and picked_char['exp'] >= rd['exp']:
                picked_char['exp'] -= rd['exp']
                picked_char['level'] = lv + 1
                leveled_up = True
                new_stats = get_character_stats(picked_char)
                picked_char['hp'] = new_stats['hp_max']
                print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']} HP Restored to {picked_char['hp']}")
            else:
                break
        # After level-up: advance level missions and sync skills
        if leveled_up and send_rpc_push:
            advance_missions(picked_char, send_rpc_push, 'level')
            # Sync updated skills map after level-up (new skills may unlock)
            new_level = picked_char.get('level', 1)
            smap = build_skills_map(picked_char.get('prof', 0), new_level, picked_char.get('skill_levels', {}))
            send_rpc_push(540, encode_sproto([(0, smap), (1, False)]))

    if cash_gained > 0:
        picked_char['cash'] = picked_char.get('cash', 0) + cash_gained

    if save:
        save_chars(all_accounts_chars)

    if (exp_gained > 0 or cash_gained > 0) and conn:
        sync_char_attrs_rpc(conn, picked_char)

    if inv_changed and send_rpc_push:
        send_rpc_push(611, sync_inventory_data(picked_char))

def give_mission_rewards(picked_char, mid):
    """Resolves rewards by profession and calculates level ups using BaseLvData."""
    try:
        m = missions_data.get(mid)
        if not m or not m.get('reward_ids'): return 0, 0, [], [], False

        prof = picked_char.get('prof', 0)
        rids = m['reward_ids']
        rid = rids[prof] if prof < len(rids) else rids[0]
        reward = rewards_data.get(rid)
        if not reward: return 0, 0, [], [], False

        added_exp = reward.get('exp', 0)
        added_cash = reward.get('cash', 0)

        picked_char['cash'] = picked_char.get('cash', 0) + added_cash
        picked_char['exp'] = picked_char.get('exp', 0) + added_exp

        leveled_up = False
        while True:
            lv = picked_char.get('level', 1)
            req_data = LEVEL_DATA.get(lv)
            if not req_data: break
            if picked_char['exp'] >= req_data['exp']:
                picked_char['exp'] -= req_data['exp']
                picked_char['level'] = lv + 1
                leveled_up = True
                # Level Up: Fully Restore HP
                new_stats = get_character_stats(picked_char)
                picked_char['hp'] = new_stats['hp_max']
                print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']} HP Restored to {picked_char['hp']}")
            else: break

        # After level-up: advance level missions and sync skills
        if leveled_up:
            advance_missions(picked_char, None, 'level')
            # Note: skill sync is handled by sync_char_attrs_rpc in the caller after this function returns

        # Send original reward popup (Tag 638)
        # item schema: itemId(0), itemCount(1), quality(3)
        popup_items = [
            encode_sproto([(0, "2001"), (1, added_exp), (3, 0)]),
            encode_sproto([(0, "1001"), (1, added_cash), (3, 0)])
        ]

        items, amts = reward.get('items', []), reward.get('item_amounts', [])
        granted_items = []
        for i in range(len(items)):
            if items[i]:
                amt = amts[i] if i < len(amts) else 1
                popup_items.append(encode_sproto([(0, items[i]), (1, amt), (3, 0)]))
                add_to_inventory(picked_char, items[i], amt)
                granted_items.append((items[i], amt))

        # The caller sends TAG 638 after ret_complete_mission (521).  The APK
        # opens MissionPassShowRoot from 521; sending the reward first lets
        # that UI cover the SimpleRewardRoot popup.
        return added_exp, added_cash, granted_items, popup_items, leveled_up
    except:
        traceback.print_exc()
        return 0, 0, [], [], False

def accept_mission_logic(picked_char, mid):
    if mid not in missions_data:
        print(f"[accept_mission_logic] FAILED: {mid} not in missions_data")
        return False
    m = missions_data[mid]
    # Main-chain completion may activate the next mission before the client
    # sends its follow-up accept request. Treat that retry as an acknowledgement
    # and preserve the existing state/progress instead of rejecting or resetting it.
    active_missions = picked_char.get('active_missions')
    if not isinstance(active_missions, dict):
        active_missions = {}
        picked_char['active_missions'] = active_missions
    if mid in active_missions:
        print(f"[accept_mission_logic] {mid} already active; acknowledging existing state")
        return True

    pre_id = m.get('pre_id', "")
    is_main_chain = (
            m.get('class') == 1
            and pre_id
            and str(picked_char.get('last_main_mission_id', "")) == str(pre_id)
    )
    if picked_char.get('level', 1) < m.get('min_level', 0) and not is_main_chain:
        print(f"[accept_mission_logic] FAILED: level too low {picked_char.get('level')} < {m.get('min_level')}")
        return False

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

def advance_missions(picked_char, send_rpc_push, event, target_id=None, die_type=0, map_id=None):
    """Apply one authoritative gameplay event to every active mission."""
    updated = False
    for mid, mdata in picked_char.get('active_missions', {}).items():
        if mdata.get('state') != 1:
            continue
        cfg = missions_data.get(mid)
        if not cfg:
            continue

        logic_type = cfg.get('logic_type')
        target = str(cfg.get('target_id', ''))
        target_value = str(target_id) if target_id is not None else ''
        matched = False

        if logic_type == 7 and event == 'level':
            mdata['parm'][0] = max(mdata['parm'][0], int(picked_char.get('level', 1)))
            matched = True
        elif logic_type in [1, 4, 11, 17, 23] and event == 'kill':
            if logic_type == 17:
                matched = True
            else:
                # Match target NPC from MissionData or spawned NPCs from KillTargetMissionData
                logic_id = str(cfg.get('logic_id', ''))
                spawn_key = logic_id if logic_id in KILL_TARGET_SPAWNS else str(mid)
                spawn_nids = set()
                if spawn_key in KILL_TARGET_SPAWNS:
                    for s in KILL_TARGET_SPAWNS[spawn_key]:
                        spawn_nids.add(str(s['nid']))
                matched = target == target_value or target_value in spawn_nids
        elif logic_type == 19 and event == 'car':
            # The client sends type 2 for a normal car robbery without an
            # NPC/car id, so its event type is the authoritative discriminator.
            matched = die_type == 2
        elif logic_type == 24 and event == 'car':
            # Target-car robbery (mission 1002) sends only type 6.  The
            # request intentionally has no npcid, therefore we match on type.
            matched = die_type == 6
        elif logic_type == 20 and event == 'impact':
            matched = die_type == 3 and (not target or target == target_value)
        elif logic_type in [0, 2, 6, 21] and event == 'interact':
            if logic_type == 21 and die_type == 4:
                # Arrive Target sends missionId as target_id
                matched = (target_value == mid)
            else:
                matched = not target or target == target_value or str(cfg.get('logic_id', '')) == target_value
        elif logic_type in [3, 4] and event == 'pickup':
            # Collect Item / Monster Drop matches on logic_id (item ID)
            matched = str(cfg.get('logic_id')) == target_value
        elif logic_type == 25 and event in ['capture', 'kill']:
            # LogicType 25 (Capture/Boss Kill) - match target NPC or KillTargetMissionData spawns
            logic_id = str(cfg.get('logic_id', ''))
            spawn_key = logic_id if logic_id in KILL_TARGET_SPAWNS else str(mid)
            spawn_nids = set()
            if spawn_key in KILL_TARGET_SPAWNS:
                for s in KILL_TARGET_SPAWNS[spawn_key]:
                    spawn_nids.add(str(s['nid']))
            matched = not target or target == target_value or str(cfg.get('logic_id', '')) == target_value or target_value in spawn_nids
        elif logic_type in [102, 103, 105, 106, 107, 108, 110, 113, 114, 117, 119, 120, 132] and event in ['dungeon', 'car_copy']:
            # Dungeon/Guide entry missions advance on specific 'dungeon' or 'car_copy' events
            logic_id_str = str(cfg.get('logic_id'))
            target_id_str = str(target_id) if target_id is not None else ''
            if logic_type == 102:
                # LogicType 102 (CAR_COPY / 飙车副本) matches any car copy scene (211-217), logic_id "102", "飙车副本", or event "car_copy" / "dungeon"
                matched = target_id_str in ['102', '211', '212', '213', '214', '215', '216', '217', '飙车副本'] or event in ['car_copy', 'dungeon']
            elif logic_type == 105:
                # Mission 1019 (EXP copy) stores its discriminator in Target=105;
                # LogicID is #N/A in this client's MissionData row.
                matched = target == target_id_str or logic_id_str == target_id_str
            else:
                matched = logic_id_str == target_id_str
        elif logic_type == 114 and event == 'world_boss':
            matched = True
        elif logic_type == 7 and event == 'map':
            matched = target == str(map_id)

        if not matched:
            continue

        required = int(cfg.get('count') or cfg.get('require_num') or 1)
        logic_id = str(cfg.get('logic_id', ''))

        if logic_type in [1, 4, 11, 17, 23]: # Kill
            spawn_key = logic_id if logic_id in KILL_TARGET_SPAWNS else str(mid)
            if spawn_key in KILL_TARGET_SPAWNS:
                required = KILL_TARGET_SPAWNS[spawn_key][0].get('require', 1)
        elif logic_type == 25: # Capture / Boss
            required = 1
        elif logic_type == 24: # Target Car
            if logic_id in TARGET_CAR_SPAWNS:
                required = TARGET_CAR_SPAWNS[logic_id][0].get('require', 1)

        if logic_type in [2, 6, 102, 103, 105, 106, 107, 108, 110, 113, 114, 117, 119, 120, 132]:
            required = 1
        if logic_type == 7:
            # Level missions: logic_id IS the required level
            required = int(cfg.get('logic_id') or 1)
            progress = int(mdata['parm'][0])
        else:
            mdata['parm'][0] = min(required, int(mdata['parm'][0]) + 1)
            progress = mdata['parm'][0]

        # Bug 3: Send TAG 524 only if progress actually changed
        old_progress = mdata.get('_last_progress', 0)
        if progress != old_progress:
            send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, progress)]))
        mdata['_last_progress'] = progress

        if progress >= required:
            mdata['state'] = 2
            send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
        updated = True

    # Bug 4: Support skip_save/skip_sync for batched mission advancement
    # When called from EXP dungeon wave completion, skip save/sync to batch at dungeon completion
    skip_save = picked_char.get('_skip_mission_save', False)
    if updated and not skip_save:
        save_chars(all_accounts_chars)
        send_rpc_push(519, sync_mission_data(picked_char))
    return updated

def claim_client_local_npc_death(picked_char, npcid, x, z):
    """Validate and de-duplicate map NPC deaths reported without a server instance ID.

    The APK's city NPC death request contains only NPC type and coordinates.
    Match it against this map's MonsterData or the character's active quest
    spawn, require the player to be nearby, and consume one configured spawn
    slot so replayed reports cannot mint repeated kill rewards.
    """
    try:
        map_id = str(picked_char.get('map_id', ''))
        nid = str(npcid)
        x = int(x)
        z = int(z)
        pos = picked_char.get('pos') or []
        px = int(pos[0])
        pz = int(pos[2])
        char_id = int(picked_char.get('id', 0))
    except (TypeError, ValueError, IndexError):
        return False
    if not char_id or nid not in NPC_CONFIG or math.hypot(px - x, pz - z) > 1500:
        return False

    candidates = []
    for category in (MONSTER_DATA, STATIC_NPC_DATA):
        for slot, spawn in enumerate(category.get(map_id, [])):
            if str(spawn.get('nid')) != nid:
                continue
            rebirth_ms = int(spawn.get('rebirth_time', -1))
            cooldown = max(30.0, min(3600.0, rebirth_ms / 1000.0)) if rebirth_ms > 0 else 300.0
            candidates.append((math.hypot(x - int(spawn['x']), z - int(spawn['z'])),
                               int(spawn['x']), int(spawn['z']), f"{category is STATIC_NPC_DATA}:{slot}",
                               max(1500, int(spawn.get('patrol_distance', 0)) + 1000), cooldown))

    # Kill-target NPCs can be mission-spawned from their separate placement
    # table and therefore may not have a MonsterData row for the current map.
    for mid, mdata in picked_char.get('active_missions', {}).items():
        if mdata.get('state') != 1:
            continue
        cfg = missions_data.get(mid, {})
        logic_id = str(cfg.get('logic_id', ''))
        spawn_key = logic_id if logic_id in KILL_TARGET_SPAWNS else str(mid)
        for row_index, spawn in enumerate(KILL_TARGET_SPAWNS.get(spawn_key, [])):
            if str(spawn.get('map')) != map_id or str(spawn.get('nid')) != nid:
                continue
            count = max(1, int(spawn.get('num', 1)))
            radius = max(1000, int(spawn.get('range', 500)) + 1000)
            distance = math.hypot(x - int(spawn['x']), z - int(spawn['z']))
            for slot in range(count):
                candidates.append((distance, int(spawn['x']), int(spawn['z']),
                                   f"quest:{mid}:{row_index}:{slot}", radius, 300.0))

    candidates = [candidate for candidate in candidates if candidate[0] <= candidate[4]]
    if not candidates:
        return False
    candidates.sort(key=lambda candidate: candidate[0])
    now = time.monotonic()
    with LOCAL_NPC_DEATH_LOCK:
        # Bound memory while preserving recent replay protection.
        if len(LOCAL_NPC_DEATHS) > 10000:
            LOCAL_NPC_DEATHS.clear()
        for _distance, sx, sz, slot_key, _radius, cooldown in candidates:
            key = (char_id, map_id, nid, slot_key, sx, sz)
            last = LOCAL_NPC_DEATHS.get(key)
            if last is not None and now - last < cooldown:
                continue
            LOCAL_NPC_DEATHS[key] = now
            return True
    return False

def validate_arrive_target_report(picked_char, mission_id):
    """Validate the APK's MSG 307 type=4 arrival report against its next waypoint."""
    mdata = picked_char.get('active_missions', {}).get(str(mission_id))
    cfg = missions_data.get(str(mission_id), {})
    if not mdata or mdata.get('state') != 1 or cfg.get('logic_type') != 21:
        return False
    move_cfg = MOVE_TARGET_DATA.get(str(cfg.get('logic_id', '')))
    if not move_cfg or str(move_cfg.get('map_id')) != str(picked_char.get('map_id')):
        return False
    points = [part for part in str(move_cfg.get('target_point', '')).split('|') if part]
    progress = int((mdata.get('parm') or [0])[0])
    if not points:
        return False
    point = points[min(progress, len(points) - 1)].split('#')
    if len(point) < 2:
        return False
    try:
        target_x, target_z = int(point[0]), int(point[1])
        pos = picked_char.get('pos') or []
        return len(pos) >= 3 and math.hypot(int(pos[0]) - target_x, int(pos[2]) - target_z) <= 850
    except (TypeError, ValueError):
        return False

def validate_car_mission_report(picked_char, die_type):
    """Check city car mission events against active objectives and locations."""
    if not picked_char or str(picked_char.get('map_id')) != '11':
        return False
    if die_type == 2:
        if not any(missions_data.get(mid, {}).get('logic_type') == 19
                   and entry.get('state') == 1
                   for mid, entry in picked_char.get('active_missions', {}).items()):
            return False
        char_id = int(picked_char.get('id', 0))
        now = time.monotonic()
        last = CAR_MISSION_EVENT_TIMES.get(char_id, 0.0)
        if now - last < 3.0:
            return False
        CAR_MISSION_EVENT_TIMES[char_id] = now
        return True
    if die_type == 6:
        pos = picked_char.get('pos') or []
        try:
            px, pz = int(pos[0]), int(pos[2])
        except (TypeError, ValueError, IndexError):
            return False
        for mid, entry in picked_char.get('active_missions', {}).items():
            cfg = missions_data.get(mid, {})
            if entry.get('state') != 1 or cfg.get('logic_type') != 24:
                continue
            logic_id = str(cfg.get('logic_id', ''))
            for car in TARGET_CAR_SPAWNS.get(logic_id, []):
                if str(car.get('map')) == '11' and math.hypot(px - int(car.get('x', 0)), pz - int(car.get('z', 0))) <= 1000:
                    return True
    return False

def init_character_fields(c):
    fields = {
        'level': 1, 'exp': 0, 'cash': 1000,
        'skill_levels': {},
        'active_missions': {},
        'completed_side_missions': [],
        'last_main_mission_id': "-1",
        'inventory': [],
        'pos': [29860, 100, -17005, 0],
        'map_id': "11",
        'tutorial': 0,
        'death_count': 0,
        'download_complete': False,
        'mounts': {},
        'equipped_mount_id': '',
        'mount_riding': False,
        'daily_copy_state': {},
        'active_copy_id': None,
        'pre_copy_pos': None,
        'pre_copy_map': None,
        'active_domin_id': None,
        'boss_inst_id': None,
        'pre_arena_pos': None,
        'pre_arena_map': None,
        'completed_tutorials': [],
        'createtime': int(time.time())
    }
    for k, v in fields.items():
        if k not in c: c[k] = v

    # Initialize or restore HP if not set or if dead
    stats = get_character_stats(c)
    if 'hp' not in c or c.get('hp', 0) <= 0:
        c['hp'] = stats['hp_max']

    # Ensure map_id is never None or "None" string — default to city map "11"
    if c.get('map_id') is None or str(c.get('map_id', '')) == 'None':
        c['map_id'] = "11"

def init_social_data(c):
    """Initialize social data (friends, enemies, mails) for a character."""
    if 'friends' not in c:
        c['friends'] = []
    if 'enemies' not in c:
        c['enemies'] = []
    if 'mails' not in c:
        c['mails'] = []
    if 'friend_requests_sent' not in c:
        c['friend_requests_sent'] = []
    if 'friend_requests_received' not in c:
        c['friend_requests_received'] = []

def encode_friend_info(fi, is_enemy=False):
    """Encode a friend_info dict as sproto bytes.
    For enemies (friendType=6), the client swaps friendId and timeInfo on receive.
    So we send: friendId=timeInfo, timeInfo=friendId for enemies.
    """
    friend_id = fi.get('friendId', 0)
    time_info = fi.get('timeInfo', int(time.time()))
    # For enemies, swap fields so client swap restores correct values
    if is_enemy:
        friend_id, time_info = time_info, friend_id
    return encode_sproto([
        (0, fi.get('characterId', 0)),
        (1, friend_id),
        (2, fi.get('name', '')),
        (3, fi.get('level', 1)),
        (4, fi.get('profession', 0)),
        (5, fi.get('combValue', 0)),
        (6, fi.get('state', 1)),
        (7, time_info),
        (8, fi.get('friendType', 0)),
        (9, fi.get('guildId', 0)),
        (10, fi.get('guildName', '')),
        (11, fi.get('friendScore', 0))
    ])

def build_mail_update(mi):
    """Build a mail_update message (msg 531) from a mail entry dict.
    Client wire tags: 0=mailId, 1=sendertype, 3=title, 4=senderTime, 5=receiveId,
                      6=readTime, 7=context, 8=mailState, 9=sortTime, 10=items, 11=expireday
    Note: Wire tag 2 is SKIPPED — title uses wire tag 3, not 2.

    Items are encoded as Dictionary<string, item> where the key is item.id (string).
    Client's item SprotoType wire tags: 0=itemId(string), 1=itemCount(long),
    3=quality(long), 4=id(string), 5=count2(long). Note: wire tag 2 is SKIPPED.
    """
    # Build items as Dictionary<string, item> for wire tag 10
    items_dict = {}
    for it in mi.get('items', []):
        item_key = str(it.get('itemId', 0))  # Dictionary key = item.id
        # Client's item.cs: itemId(string, wire 0), itemCount(long, wire 1),
        # quality(long, wire 3), id(string, wire 4)
        item_obj = encode_sproto([
            (0, str(it.get('itemId', 0))),   # itemId as string
            (1, it.get('count', 1)),          # itemCount as long
            (3, it.get('quality', 0)),        # quality as long (wire tag 3, NOT 2!)
            (4, item_key)                     # id as string (dictionary key)
        ])
        items_dict[item_key] = item_obj

    return encode_sproto([
        (0, mi.get('mailId', 0)),
        (1, mi.get('sendertype', 0)),
        (3, mi.get('title', '')),
        (4, mi.get('senderTime', int(time.time()))),
        (5, mi.get('receiveId', 0)),
        (6, mi.get('readTime', 0)),
        (7, mi.get('context', '')),
        (8, mi.get('mailState', 0)),
        (9, mi.get('sortTime', int(time.time()))),
        (10, items_dict if items_dict else None),
        (11, mi.get('expireday', 7))
    ])

def broadcast_aoi_add(target_conn, target_char, sender_conn=None, sender_char=None):
    """Broadcast aoi_add (msg 505) for a player to all other players on the same map.
    target_conn is the socket to send TO (when called from map transition for existing players).
    When called for the new player, target_conn is ignored and we broadcast to all others."""
    if not target_char:
        return
    target_map = str(target_char.get('map_id', '11'))
    target_id = target_char.get('id', 0)
    # Build character_aoi with runtime(6) included
    pkt = build_aoi_add_packet(target_char)
    if pkt is None:
        return
    # Skip player-to-player AOI on single-player maps (e.g., Map 11)
    if is_single_player_map(target_map) and not ((target_char.get('exp_stage_state') or {}).get('team_id')):
        return
    # Send to all other connections that have a character on the same map
    for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
        if ch and can_aoi_exchange(target_char, ch):
            try:
                c_lock = CONNECTION_LOCKS.get(cid)
                if c_lock:
                    with c_lock:
                        c.sendall(pkt)
                else:
                    c.sendall(pkt)
                print(f"[AOI] Broadcast aoi_add id={target_id} to {ch.get('id', 0)} map={target_map}")
            except Exception:
                pass

def broadcast_aoi_remove(char_id, map_id):
    """Broadcast aoi_remove (msg 506) when a player leaves a map."""
    map_id = str(map_id)
    source_char = ALL_CONNECTIONS.get(int(char_id), (None, None))[1]
    if is_single_player_map(map_id) and not (source_char and (source_char.get('exp_stage_state') or {}).get('team_id')):
        return
    try:
        aoi_data = encode_sproto([(0, char_id)])
        ph_p = encode_sproto([(0, 506)])
        pf_p = sproto_pack(ph_p + aoi_data)
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
            if ch and (can_aoi_exchange(source_char, ch) if source_char else str(ch.get('map_id', '11')) == map_id):
                try:
                    c_lock = CONNECTION_LOCKS.get(cid)
                    if c_lock:
                        with c_lock:
                            c.sendall(pkt)
                    else:
                        c.sendall(pkt)
                    print(f"[AOI] Broadcast aoi_remove id={char_id} to {ch.get('id', 0)} map={map_id}")
                except Exception:
                    pass
    except Exception as e:
        print(f"[AOI] Failed to build aoi_remove for id={char_id}: {e}")

def start_map_transition(conn, picked_char, target_map_id, send_rpc_push, override_pos=None):
    if picked_char and picked_char.get('hp', 0) <= 0:
        stats = get_character_stats(picked_char)
        picked_char['hp'] = stats['hp_max']

    src_map = str(picked_char.get('map_id') or '11')
    target_map_id = str(target_map_id) or '11'
    if src_map != target_map_id: broadcast_aoi_remove(picked_char.get('id', 0), src_map)
    NPC_SPAWNED_MAPS.setdefault(id(conn), set()).discard(target_map_id)
    picked_char['map_id'] = target_map_id
    scene_name = "Unknown"

    # Update position
    landing_pos = override_pos
    if not landing_pos and target_map_id == "502":
        # Lord Battle: Restore verified MapInfo coordinates (Y=120)
        landing_pos = [-400, 120, 0, 9000]
        print(f"[TELEPORT] Lord Battle Map 502 start pos={landing_pos}")

    # 1. Try teleport portal heuristic
    if not landing_pos and (target_map_id, src_map) in MAP_CONNECT_DATA:
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

                x_coord = int(parts[0])
                z_coord = int(parts[2])
                # Fix: Exp stage maps have birth=0#0#0#0 in MapInfoData, use a position
                # near the center of all monster spawn areas so the player is in the battle zone
                # MonsterData 2231-2294 positions: X ranges -970 to 1090, Z ranges -1137 to 1088
                # Center ≈ (0, 0) in world units. Spawn slightly offset toward first wave.
                if target_map_id in ["223", "224", "225", "226", "227", "228", "229"]:
                    x_coord = 0     # Center of monster spawn area
                    z_coord = 0     # Center of monster spawn area
                    y_coord = 100

                landing_pos = [x_coord, y_coord, z_coord, int(parts[3]) if len(parts) > 3 else 0]
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
        print(f"[M1003 DEBUG] TX 503 map_id={target_map_id}")
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
        if is_exp_map(target_map_id): sync_exp_team_aoi(picked_char)

        # Broadcast this new player to all other players on the same map
        # Skip player-to-player AOI on single-player maps (e.g., Map 11)
        if not is_single_player_map(target_map_id) and not is_exp_map(target_map_id):
            broadcast_aoi_add(conn, picked_char)
            # Send existing players on this map to the new player
            for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
                if ch and ch.get('id', 0) != picked_char.get('id', 0) and str(ch.get('map_id', '11')) == target_map_id:
                    # Send this existing player's aoi_add to the new player (conn)
                    pkt_ex = build_aoi_add_packet(ch)
                    if pkt_ex:
                        conn.sendall(pkt_ex)
                        print(f"[AOI] Send existing player id={ch.get('id', 0)} to new player {picked_char.get('id', 0)} map={target_map_id}")

        # BOSS SPAWN for Dominance Map 502
        if target_map_id == "502":
            did = picked_char.get('active_domin_id', '1')
            boss_inst_id = next_global_inst_id()
            picked_char['boss_inst_id'] = boss_inst_id
            picked_char['boss_pos'] = [400, 120, 0, -9000]

            # Use player level for consistent boss stats
            player_level = picked_char.get('level', 1)
            boss_stats = get_npc_attr("1105", player_level)
            NPC_HP_MAP[boss_inst_id] = boss_stats['hp_max']
            NPC_INST_MAP[boss_inst_id] = "BOSS_" + did
            NPC_INSTANCE_MAP[boss_inst_id] = "502"
            # The APK cannot create the zombie player (or the VS panel) until
            # map_ready.  Remember it here; MSG 100 sends the actual tag 544.
            picked_char['boss_waiting_for_map_ready'] = True
            print(f"[M1003 DEBUG] Prepared Boss did={did} inst={boss_inst_id} max_hp={boss_stats['hp_max']} (awaiting map_ready)")

    except Exception:
        print("[!] FAILED TO SEND MAP ENTER TRANSITION")
        traceback.print_exc()
    print("[DEBUG] AFTER MAP ENTER")

def is_skill_locked(sid, level, prof):
    """Check if a skill is locked. Since skills are not unlocked by level,
    only the starter active skill is available. Additional skills come from weapon skins."""
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    # Only the starter active skill is available; all others come from weapon skins
    if sid == p["starter_active"]:
        return False, 0
    # Check if it's a basic attack or dodge
    atk_skills = p["atk"] if isinstance(p["atk"], list) else [p["atk"]]
    if sid in atk_skills or sid == p["dodge"]:
        return False, 0
    # Any other skill is not owned yet (comes from weapon skins)
    return True, 999

def serve_resource_http(conn, initial_data):
    """Serve APK updater files on the game-server port.

    The updater requests /RES_205/... over HTTP.  Restrict this handler to
    versioned resource paths below assets so an HTTP request cannot read game
    data or arbitrary server files.
    """
    try:
        request = initial_data
        while b"\r\n\r\n" not in request and len(request) < 16384:
            chunk = conn.recv(4096)
            if not chunk:
                break
            request += chunk
        first_line = request.split(b"\r\n", 1)[0].decode("ascii", "ignore")
        parts = first_line.split()
        if len(parts) < 2 or parts[0] not in ("GET", "HEAD"):
            conn.sendall(b"HTTP/1.1 400 Bad Request\r\nConnection: close\r\n\r\n")
            return
        relative_path = parts[1].split("?", 1)[0].lstrip("/")
        normalized = os.path.normpath(relative_path).replace("\\", "/")
        if not normalized.startswith("RES_") or normalized.startswith("../"):
            conn.sendall(b"HTTP/1.1 404 Not Found\r\nConnection: close\r\n\r\n")
            return
        local_path = os.path.abspath(os.path.join(RESOURCE_ROOT, normalized))
        resource_root = os.path.abspath(RESOURCE_ROOT) + os.sep
        if not local_path.startswith(resource_root) or not os.path.isfile(local_path):
            print(f"[HTTP 9555] 404 {normalized}")
            conn.sendall(b"HTTP/1.1 404 Not Found\r\nConnection: close\r\n\r\n")
            return
        size = os.path.getsize(local_path)
        headers = (
                b"HTTP/1.1 200 OK\r\n"
                + b"Content-Length: " + str(size).encode("ascii") + b"\r\n"
                + b"Content-Type: application/octet-stream\r\n"
                + b"Connection: close\r\n\r\n"
        )
        conn.sendall(headers)
        if parts[0] == "GET":
            with open(local_path, "rb") as resource_file:
                while True:
                    block = resource_file.read(65536)
                    if not block:
                        break
                    conn.sendall(block)
        print(f"[HTTP 9555] 200 {normalized} ({size} bytes)")
    except Exception as exc:
        print(f"[HTTP 9555] failed: {exc}")
    finally:
        try:
            conn.close()
        except Exception:
            pass

def _sync_friend_online_state(char_id, state):
    """Update the online state of a character in all other players' friend/enemy lists.
    state=1 for login, state=0 for logout.
    Pushes syn_friend_info to online friends/enemies who have this player.
    Uses the EXISTING stored friend record, only updating the state field."""
    for area_key, area_chars in all_accounts_chars.items():
        for acc_key, char_list in area_chars.items():
            for ch in char_list:
                if ch.get('id', 0) == char_id:
                    continue
                updated_friends = []
                updated_enemies = []
                # Check friends - update state in place and collect for push
                for fi in ch.get('friends', []):
                    if fi.get('friendId') == char_id:
                        fi['state'] = state
                        updated_friends.append(fi)
                # Check enemies - update state in place and collect for push
                for ei in ch.get('enemies', []):
                    if ei.get('friendId') == char_id:
                        ei['state'] = state
                        updated_enemies.append(ei)
                if updated_friends or updated_enemies:
                    # Push syn_friend_info to this player if they're online
                    for cid, (c, conn_ch) in list(ALL_CONNECTIONS.items()):
                        if conn_ch and conn_ch.get('id', 0) == ch.get('id', 0):
                            try:
                                # Use the existing stored friend record, not a fake one
                                for fi in updated_friends:
                                    fi_bytes = encode_friend_info(dict(fi))
                                    ph_p = encode_sproto([(0, 538)])
                                    pf_p = sproto_pack(ph_p + encode_sproto([(0, fi_bytes)]))
                                    syn_pkt = struct.pack(">H", len(pf_p)) + pf_p
                                    c_lock = CONNECTION_LOCKS.get(cid)
                                    if c_lock:
                                        with c_lock:
                                            c.sendall(syn_pkt)
                                    else:
                                        c.sendall(syn_pkt)
                                    print(f"[FRIEND] Online state sync: {char_id} state={state} to {ch.get('id', 0)} (friend)")
                                for ei in updated_enemies:
                                    ei_bytes = encode_friend_info(dict(ei), is_enemy=True)
                                    ph_p = encode_sproto([(0, 538)])
                                    pf_p = sproto_pack(ph_p + encode_sproto([(0, ei_bytes)]))
                                    syn_pkt = struct.pack(">H", len(pf_p)) + pf_p
                                    c_lock = CONNECTION_LOCKS.get(cid)
                                    if c_lock:
                                        with c_lock:
                                            c.sendall(syn_pkt)
                                    else:
                                        c.sendall(syn_pkt)
                                    print(f"[FRIEND] Online state sync: {char_id} state={state} to {ch.get('id', 0)} (enemy)")
                            except Exception:
                                pass
                    break
    # Save updated friend/enemy state
    save_chars(all_accounts_chars)

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0
    global server_session_counter, ALL_CONNECTIONS, MAIL_ID_COUNTER, TEAM_ID_COUNTER, EXP_RUN_COUNTER
    conn_id = id(conn)
    # ALL_CONNECTIONS will be updated when character is picked
    send_lock = threading.Lock()

    def send_rpc_push(tag, data):
        try:
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            # The arena start is delayed so the client can show its VS panel.
            # Serialise writes because that delay runs in a timer thread.
            with send_lock:
                conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            print(f"[TX] PUSH TAG={tag} SIZE={len(data)}")
        except Exception:
            print(f"[!] FAILED TO SEND PUSH TAG={tag}")
            traceback.print_exc()

    def schedule_domin_return(restore_hp=False):
        """Return after the Capture mission's APK-localized five-second exit notice."""
        if not picked_char or picked_char.get('domin_return_scheduled'):
            return
        # Cancel the arena timeout timer so it doesn't fire after we've already exited.
        if 'arena_timer' in picked_char:
            picked_char['arena_timer'].cancel()
        picked_char['domin_return_scheduled'] = True
        picked_char['domin_return_restore_hp'] = restore_hp

        def leave_after_notice(seconds_left):
            if not picked_char or picked_char.get('map_id') != '502':
                return
            if seconds_left > 0:
                # Localization 200302: "You will leave the scene after {0} seconds".
                send_rpc_push(529, encode_sproto([
                    (0, f"You will leave the scene after {seconds_left} seconds"),
                    (1, True)
                ]))
                timer = threading.Timer(1.0, leave_after_notice, args=(seconds_left - 1,))
                timer.daemon = True
                timer.start()
                return

            return_map = picked_char.get('pre_arena_map', '11')
            return_pos = picked_char.get('pre_arena_pos')
            if picked_char.pop('domin_return_restore_hp', False):
                # A map transition recreates the main player with this value.
                # Restore only after the exit countdown, never in the arena.
                picked_char['hp'] = get_character_stats(picked_char)['hp_max']
            picked_char['boss_inst_id'] = None
            picked_char['active_domin_id'] = None
            picked_char['pre_arena_pos'] = None
            picked_char['pre_arena_map'] = None
            picked_char['domin_return_scheduled'] = False
            # The APK has no dedicated "close arena timer" packet. Its
            # notify_copy_start_info handler closes the timer immediately for
            # an elapsed end_time, before the map transition begins.
            send_rpc_push(629, encode_sproto([(0, int(time.time())), (1, 2)]))
            start_map_transition(conn, picked_char, return_map, send_rpc_push, override_pos=return_pos)
            print('[M1003 DEBUG] Arena exit countdown finished; returned to saved city position')

        leave_after_notice(5)

    def schedule_street_race_return():
        """Apply CopySceneData.EndTime (five seconds) after a race result."""
        if not picked_char or picked_char.get('street_race_return_scheduled'):
            return
        copy_id = str(picked_char.get('active_copy_id') or '')
        cfg = COPY_SCENE_CONFIG.get(copy_id)
        if not cfg or cfg.get('subtype') != 7:
            return
        picked_char['street_race_return_scheduled'] = True
        delay = int(cfg.get('end_time') or 5)

        def leave_after_notice(seconds_left):
            if not picked_char or picked_char.get('active_copy_id') != copy_id:
                return
            if seconds_left > 0:
                # This follows the CopySceneData EndTime=5 for every Street
                # Race route and uses the same APK dialog notification path
                # as the other local copy exits.
                send_rpc_push(529, encode_sproto([
                    (0, f"You will leave the scene after {seconds_left} seconds"),
                    (1, True)
                ]))
                timer = threading.Timer(1.0, leave_after_notice, args=(seconds_left - 1,))
                timer.daemon = True
                timer.start()
                return

            return_pos = picked_char.get('pre_copy_pos')
            return_map = picked_char.get('pre_copy_map', '11')
            picked_char['pre_copy_pos'] = None
            picked_char['pre_copy_map'] = None
            picked_char['active_copy_id'] = None
            picked_char['street_race_return_scheduled'] = False
            save_chars(all_accounts_chars)
            start_map_transition(conn, picked_char, return_map, send_rpc_push, override_pos=return_pos)
            print(f"[STREET RACE] exit countdown finished; returned to map {return_map} after copy={copy_id}")

        leave_after_notice(delay)

    try:
        # HTTP updater traffic begins with GET/HEAD; game packets begin with a
        # two-byte big-endian Sproto frame length.  Peek without consuming it.
        initial = conn.recv(4, socket.MSG_PEEK)
        if initial.startswith(b"GET ") or initial.startswith(b"HEAD"):
            serve_resource_http(conn, b"")
            return
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

            if msg == 2: # visitor
                new_id = acc_id if acc_id and acc_id != "0" else f"100{random.randint(1000, 9999)}"
                acc_id = new_id
                resp = encode_sproto([(0, 0), (1, new_id), (2, "key123")])
                ph = encode_sproto([(1, session)]) if session is not None else encode_sproto([(0, 2)])
                pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 3: # verfiy
                req_id = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, acc_id))
                if req_id: acc_id = req_id
                resp = encode_sproto([(0, 0), (1, int(time.time()) % 100000), (2, ""), (3, ""), (4, "1.012.017"), (5, "205")])
                ph = encode_sproto([(1, session)]) if session is not None else encode_sproto([(0, 3)])
                pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1); cur_areaId = str(get_area_id(sid))
                # sync_common_data: serverTime(0), time_offset(2), func_info(9), pvp_scale(4), seed(12), server_level(13), start_time(14)
                resp = encode_sproto([
                    (0, 2), (1, "1.012.017"), (2, "205"), (3, 1),
                    # pvp_scale = 1.0 (10000), seed = random
                    (4, 10000), (12, random.randint(1, 10000))
                ])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                chars = get_account_chars(all_accounts_chars, cur_areaId, acc_id)
                # Sort by createtime ascending (oldest created first)
                chars.sort(key=lambda x: x.get('createtime', 0))

                # The client sorts character_overview.createtime ASCENDING.
                # Send real createtime so oldest characters appear first.
                ov_list = []
                for c in chars:
                    ov_list.append(get_char_ov(c, c.get('createtime', 0)))

                resp = encode_sproto([(0, ov_list)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8') if isinstance(c_data.get(0), bytes) else str(c_data.get(0, "Hero"))
                prof = get_val_int(c_data, 1, 0); cid = generate_unique_char_id()
                cur_area_key = str(cur_areaId)
                if cur_area_key not in all_accounts_chars: all_accounts_chars[cur_area_key] = {}
                if acc_id not in all_accounts_chars[cur_area_key]: all_accounts_chars[cur_area_key][acc_id] = []
                nc = {'id': cid, 'name': name, 'prof': prof}
                init_character_fields(nc)
                all_accounts_chars[cur_area_key][acc_id].append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in get_account_chars(all_accounts_chars, cur_areaId, acc_id) if c['id'] == char_id), None)
                char_id = picked_char.get('id', 0)
                ALL_CONNECTIONS[char_id] = (conn, picked_char)  # Track online character
                # All push paths, including timer-driven EXP/AOI pushes and
                # this handler's synchronous sends, must serialize complete
                # length-prefixed packets through the same socket lock.
                CONNECTION_LOCKS[char_id] = send_lock
                # Update online state for all friends/enemies who have this player in their list
                _sync_friend_online_state(char_id, 1)
                resp = encode_sproto([(0, 1 if picked_char else 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if picked_char:
                    picked_char['last_played'] = int(time.time())
                    print(f"[CHARACTER PICK] id={picked_char['id']} level={picked_char.get('level')}")
                    init_character_fields(picked_char)
                    reconcile_exp_stage_login(picked_char)
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

                    # 614: sync_common_data (data-driven from FunctionData)
                    char_level = picked_char.get('level', 1)
                    player_tutorial = picked_char.get('tutorial', 0)
                    if FUNCTION_DATA:
                        fids = []
                        for fid, finfo in FUNCTION_DATA.items():
                            fc = finfo['class']
                            # class 0: default open
                            if fc == 0:
                                fids.append(fid)
                            elif fc == 1:
                                # class 1: level-gated -> unlock when level >= condition
                                if char_level >= finfo['condition']:
                                    fids.append(fid)
                            elif fc == 4:
                                # class 4: tutorial record -> always include so client can track state
                                fids.append(fid)
                    else:
                        # Fallback: original hardcoded list
                        fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    # Build func_info: state=1 means tutorial completed, state=0 means tutorial pending
                    # For MAIN_MISSION (100), state depends on whether player has completed the tutorial
                    # For class 4 tutorial records (e.g. 4083 ROB_CAR_TIP), state depends on completed_tutorials
                    completed_tutorials = picked_char.get('completed_tutorials', [])
                    # Build a set of class-4 function IDs for quick lookup
                    class4_fids = set()
                    if FUNCTION_DATA:
                        for fid, finfo in FUNCTION_DATA.items():
                            if finfo['class'] == 4:
                                class4_fids.add(fid)
                    funcs = {}
                    for fid in fids:
                        if fid == "100":
                            # MAIN_MISSION: state=1 only if tutorial is already finished
                            func_state = 1 if player_tutorial == 1 else 0
                        elif fid in class4_fids:
                            # Class 4 tutorial records: state=1 if completed, state=0 if pending
                            func_state = 1 if fid in completed_tutorials else 0
                        else:
                            func_state = 1
                        funcs[fid] = encode_sproto([(0, fid), (1, func_state)])
                    send_rpc_push(614, encode_sproto([
                        (0, int(time.time())), (2, 0), (4, 10000), (9, funcs), (12, random.randint(1, 10000)), (13, 1), (14, int(time.time()))
                    ]))

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

                    # 538: syn_friend_info - sync friend list on login
                    init_social_data(picked_char)
                    for fi in picked_char.get('friends', []):
                        fi_for_sync = dict(fi)
                        fi_for_sync['friendType'] = 0  # UpdateFriendInfo ignores friendType, but keep consistent
                        fi_bytes = encode_friend_info(fi_for_sync)
                        # syn_friend_info: tag 0 = friend_info object
                        send_rpc_push(538, encode_sproto([(0, fi_bytes)]))

                    # Push pending friend requests on login so they appear in Request tab
                    for req_id in picked_char.get('friend_requests_received', []):
                        # Look up requester's real info
                        requester_name = f'Player{req_id}'
                        requester_level = 1
                        requester_prof = 0
                        requester_combat = 0
                        requester_guildId = 0
                        requester_guildName = ''
                        requester_state = 0  # Default offline
                        # Check online players first
                        for r_cid, (r_conn, ch) in list(ALL_CONNECTIONS.items()):
                            if ch and ch.get('id', 0) == req_id:
                                requester_name = ch.get('name', f'Player{req_id}')
                                requester_level = ch.get('level', 1)
                                requester_prof = ch.get('prof', 0)
                                requester_combat = get_character_stats(ch)['power']
                                requester_guildId = ch.get('guildId', 0)
                                requester_guildName = ch.get('guildName', '')
                                requester_state = 1  # Online
                                break
                        else:
                            # Look up from character database
                            for area_key, area_chars in all_accounts_chars.items():
                                for acc_key, char_list in area_chars.items():
                                    for ch in char_list:
                                        if ch.get('id', 0) == req_id:
                                            requester_name = ch.get('name', f'Player{req_id}')
                                            requester_level = ch.get('level', 1)
                                            requester_prof = ch.get('prof', 0)
                                            requester_combat = get_character_stats(ch)['power']
                                            requester_guildId = ch.get('guildId', 0)
                                            requester_guildName = ch.get('guildName', '')
                                            break
                                    else:
                                        continue
                                    break
                        sender_friend_info = {
                            'characterId': req_id,
                            'friendId': req_id,
                            'name': requester_name,
                            'level': requester_level,
                            'profession': requester_prof,
                            'combValue': requester_combat,
                            'state': requester_state,
                            'timeInfo': int(time.time()),
                            'friendType': 2,
                            'guildId': requester_guildId,
                            'guildName': requester_guildName,
                            'friendScore': 0
                        }
                        notice_data = encode_sproto([(0, encode_friend_info(sender_friend_info))])
                        send_rpc_push(536, notice_data)

                    # Enemies are NOT synced via syn_friend_info on login because
                    # the client handler routes ALL syn_friend_info to UpdateFriendInfo
                    # (friends dict), not the enemies dict. Enemies load correctly
                    # via request_update_friend_useinfo (msg 126) when the tab opens.

                    # 531: mail_update - sync mail list on login
                    for mi in picked_char.get('mails', []):
                        mi_bytes = build_mail_update(mi)
                        send_rpc_push(531, mi_bytes)

                    # TAG 503: enter_map
                    mid = str(picked_char.get('map_id') or '11')
                    if is_exp_map(mid) and not picked_char.get('exp_stage_state') and not picked_char.get('_exp_leave_timer'):
                        # EXP run state is intentionally runtime-only. If the
                        # process restarted while a character was saved inside
                        # an EXP map, return them to the saved entrance point
                        # instead of silently creating a fresh solo run.
                        saved_pos = picked_char.pop('_exp_return_pos', None) or picked_char.get('pre_copy_pos')
                        saved_map = str(picked_char.pop('_exp_return_map', None) or picked_char.get('pre_copy_map') or '11')
                        if is_exp_map(saved_map): saved_map = '11'
                        if saved_pos: picked_char['pos'] = saved_pos
                        picked_char['map_id'] = saved_map
                        picked_char['active_copy_id'] = None
                        picked_char['exp_team_id'] = None
                        picked_char.pop('exp_members', None)
                        picked_char.pop('exp_defer_start', None)
                        picked_char.pop('pre_copy_pos', None)
                        picked_char.pop('pre_copy_map', None)
                        mid = saved_map
                        save_chars(all_accounts_chars)
                        print(f"[EXP RECOVERY] returned stale run to map={saved_map} player={char_id}")
                    if mid == 'None':
                        mid = '11'
                        picked_char['map_id'] = '11'
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
                        print(f"[M1003 DEBUG] TX 503 map_id={mid}")
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
                        if is_exp_map(mid): sync_exp_team_aoi(picked_char)
                        restored_team = get_team_for_char(char_id)
                        if restored_team: send_team_update(restored_team['id'])
                        pending_result = picked_char.get('pending_exp_result')
                        if pending_result:
                            try:
                                if send_push_to_char(char_id, 552, bytes.fromhex(pending_result)):
                                    picked_char.pop('pending_exp_result', None)
                                    save_chars(all_accounts_chars)
                            except Exception: pass

                        # MULTIPLAYER AOI SYNC - Broadcast this new player to existing players
                        # Skip player-to-player AOI on single-player maps (e.g., Map 11)
                        if not is_single_player_map(mid) and not is_exp_map(mid):
                            broadcast_aoi_add(conn, picked_char)
                            # Send existing players on this map to the new player
                            for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
                                if ch and ch.get('id', 0) != picked_char.get('id', 0) and str(ch.get('map_id', '11')) == mid:
                                    pkt_ex = build_aoi_add_packet(ch)
                                    if pkt_ex:
                                        conn.sendall(pkt_ex)
                                        print(f"[AOI] Initial login: Send existing player id={ch.get('id', 0)} to new player {picked_char.get('id', 0)} map={mid}")

                    except Exception:
                        print("[!] FAILED TO SEND INITIAL MAP ENTER")
                        traceback.print_exc()
                    print("[DEBUG] AFTER MAP ENTER")

            elif msg == 100: # map_ready
                if picked_char:
                    mid = picked_char.get('map_id', '11')
                    print(f"[MAP READY RECEIVED] map_id={mid}")
                    if is_exp_map(mid):
                        exp_state = picked_char.get('exp_stage_state')
                        if exp_state:
                            # Issue 57: Change type from 0 to 2 so CountDownTimeLogic auto-closes at 00:00:00
                            exp_send_push(exp_state, 629, encode_sproto([(0, exp_state['end_time']), (1, 2)]))
                            # Send TAG 683 with raw kill counts for client UI
                            exp_cfg_for_ready = DAILY_EXP_CONFIG.get(exp_state['copy_id'], {})
                            _send_exp_tag683(lambda tag, data: exp_send_push(exp_state, tag, data), exp_state, exp_cfg_for_ready)
                            print(f"[EXP STAGE] map_ready sent 629 & 683 updates for copy={mid}")
                    if mid == "502":
                        # Map 502 exposes its match timer only through this APK tag.
                        # Cancel any previous arena timer to prevent stale timers from
                        # repeated map_ready events affecting a new arena run.
                        if 'arena_timer' in picked_char:
                            picked_char['arena_timer'].cancel()
                        send_rpc_push(629, encode_sproto([(0, int(time.time()) + 60), (1, 0)]))

                        def arena_timeout():
                            if picked_char and picked_char.get('map_id') == '502':
                                boss_id = picked_char.get('boss_inst_id')
                                # Only timeout if boss is still alive (not already won/lost)
                                if boss_id and NPC_HP_MAP.get(boss_id, 0) > 0:
                                    print('[M1003 DEBUG] Arena 60s timeout reached; returning')
                                    schedule_domin_return(restore_hp=True)

                        t = threading.Timer(60.0, arena_timeout)
                        t.daemon = True
                        t.start()
                        picked_char['arena_timer'] = t

                        boss_id = picked_char.get('boss_inst_id')
                        if boss_id and picked_char.pop('boss_waiting_for_map_ready', False):
                            did = picked_char.get('active_domin_id', '1')
                            player_level = picked_char.get('level', 1)
                            boss_stats = get_npc_attr("1105", player_level)
                            # Tag 544 creates ObjZombiePlayer and opens the VS UI.
                            send_rpc_push(544, encode_sproto([(0, get_boss_char(boss_id, did, player_level))]))
                            sync_npc_attrs_rpc(conn, boss_id, boss_stats, NPC_HP_MAP.get(boss_id, boss_stats['hp_max']))
                            print(f"[M1003 DEBUG] Spawned Boss did={did} inst={boss_id} after map_ready")

                            # Tag 547 closes the VS UI and activates the APK's
                            # built-in zombie auto-fight.  Sending it immediately
                            # makes the VS UI invisible, so keep it on screen first.
                            def start_domin_battle(expected_boss_id=boss_id):
                                if (picked_char.get('map_id') == '502'
                                        and picked_char.get('boss_inst_id') == expected_boss_id
                                        and NPC_HP_MAP.get(expected_boss_id, 0) > 0):
                                    send_rpc_push(547, encode_sproto([]))
                                    print(f"[M1003 DEBUG] Arena started boss={expected_boss_id}; APK zombie AI enabled")
                            arena_timer = threading.Timer(2.5, start_domin_battle)
                            arena_timer.daemon = True
                            arena_timer.start()
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 101: # move
                p_raw = body.get(0)
                if p_raw and picked_char and (
                        not is_exp_map(picked_char.get('map_id', ''))
                        or (is_current_character_connection(conn, picked_char)
                            and picked_char.get('exp_stage_state')
                            and exp_run_map_id(picked_char['exp_stage_state']) == str(picked_char.get('map_id'))
                            and not picked_char['exp_stage_state'].get('_finish_handled')
                            and int(picked_char.get('id', 0)) in exp_member_ids(picked_char['exp_stage_state'])
                            and int(picked_char.get('hp', 0)) > 0)):
                    pd = decode_sproto(p_raw)
                    new_pos = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                    move_map = str(picked_char.get('map_id', ''))
                    move_exp_state = picked_char.get('exp_stage_state') if is_exp_map(move_map) else None
                    if (move_exp_state and exp_run_map_id(move_exp_state) == move_map
                            and not move_exp_state.get('_finish_handled')
                            and int(picked_char.get('id', 0)) in exp_member_ids(move_exp_state)):
                        # Use the same bounded location for combat, AI and
                        # squad visuals; do not overwrite it with raw client
                        # coordinates below.
                        new_pos = update_exp_authoritative_position(picked_char, move_exp_state, new_pos)
                    picked_char['pos'] = new_pos
                    # Issue 224: Throttle position saves to avoid excessive disk I/O.
                    # Save every 30 seconds instead of on every move packet (~10Hz).
                    # The position is still saved on important events: dungeon entry/exit,
                    # map transition, disconnect, mission completion, etc.
                    last_pos_save = picked_char.get('_last_pos_save', 0)
                    if int(time.time()) - last_pos_save >= 30:
                        save_chars(all_accounts_chars)
                        picked_char['_last_pos_save'] = int(time.time())
                    # Broadcast movement to other players on the same map (TAG 507)
                    broadcast_aoi_move(picked_char)
                elif p_raw and picked_char and is_exp_map(picked_char.get('map_id', '')):
                    print(f"[EXP MOVE] rejected downed/nonparticipant/stale player={picked_char.get('id')} map={picked_char.get('map_id')}")

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 112: # accept_mission
                if picked_char:
                    mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                    res = accept_mission_logic(picked_char, mid)
                    print(f"[MISSION ACCEPT REQ] mid={mid} result={res}")
                    if res:
                        save_chars(all_accounts_chars)
                        print(f"[MISSION ACCEPT] mission_id={mid}")
                        m_entry = picked_char.get('active_missions', {}).get(mid, {})
                        parm = m_entry.get('parm', [0]*8)
                        if len(parm) < 8: parm += [0]*(8-len(parm))
                        own_bytes = encode_sproto([
                            (0, str(mid)),
                            (1, int(m_entry.get('state', 1))),
                            (2, 0),
                            (3, [int(x) for x in parm])
                        ])
                        send_rpc_push(520, encode_sproto([
                            (0, str(mid)),
                            (1, 0),
                            (2, 0),
                            (3, own_bytes)
                        ]))
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 113: # complete_mission
                if picked_char:
                    mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                    claim_mission = False
                    with get_character_transaction_lock(picked_char):
                        m_entry = picked_char.get('active_missions', {}).get(mid)
                        if m_entry and m_entry.get('state') == 2:
                            # Mark before granting: duplicate/reconnected
                            # sessions cannot both claim the same objective.
                            m_entry['state'] = 3
                            claim_mission = True
                    print(f"[MISSION COMPLETE REQ] mid={mid} entry_exists={m_entry is not None} state={m_entry['state'] if m_entry else 'N/A'}")
                    if claim_mission:
                        m_cfg = missions_data.get(mid)
                        if m_cfg:
                            print(f"[MISSION CHAIN] completed={mid} last_main_before={picked_char.get('last_main_mission_id')}")
                            exp_add, cash_add, items_add, popup_items, mission_leveled_up = give_mission_rewards(picked_char, mid)
                            print(f"[MISSION REWARD] mission={mid} exp={exp_add} cash={cash_add} items={items_add}")

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
                            # Main-chain level goals may be accepted after the
                            # preceding mission's reward has already raised
                            # the player.  Re-evaluate the newly accepted
                            # level mission immediately, otherwise mission
                            # 1005 can remain at 0/5 until a later NPC event.
                            if is_chained and next_mid and str(next_mid) in missions_data:
                                next_cfg = missions_data[str(next_mid)]
                                if next_cfg.get('logic_type') == 7:
                                    advance_missions(picked_char, send_rpc_push, 'level')
                            save_chars(all_accounts_chars)
                            print(f"[MISSION COMPLETE] mission_id={mid} chained={is_chained}")

                            # Standard completion response
                            if session is not None:
                                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                                conn.sendall(struct.pack(">H", len(pf)) + pf)

                            # Push Sync sequence
                            send_rpc_push(521, encode_sproto([(0, mid), (1, 0)])) # Success feedback
                            if is_chained and next_mid:
                                next_m_entry = picked_char.get('active_missions', {}).get(str(next_mid), {})
                                next_parm = next_m_entry.get('parm', [0]*8)
                                if len(next_parm) < 8: next_parm += [0]*(8-len(next_parm))
                                next_own_bytes = encode_sproto([
                                    (0, str(next_mid)),
                                    (1, int(next_m_entry.get('state', 1))),
                                    (2, 0),
                                    (3, [int(x) for x in next_parm])
                                ])
                                send_rpc_push(520, encode_sproto([
                                    (0, str(next_mid)),
                                    (1, 0),
                                    (2, 0),
                                    (3, next_own_bytes)
                                ]))
                            sync_char_attrs_rpc(conn, picked_char)               # Stats update
                            # Sync skills if leveled up from mission rewards
                            if mission_leveled_up:
                                smap = build_skills_map(picked_char.get('prof', 0), picked_char.get('level', 1), picked_char.get('skill_levels', {}))
                                send_rpc_push(540, encode_sproto([(0, smap), (1, False)]))
                            send_rpc_push(519, sync_mission_data(picked_char))   # Mission UI update
                            if popup_items:
                                send_rpc_push(638, encode_sproto([(0, popup_items)]))
                            if items_add:
                                send_rpc_push(611, sync_inventory_data(picked_char)) # Inventory sync
                            if mid == '1003' and picked_char.get('map_id') == '502':
                                # Tag 521 invokes the APK's MissionPassShowRoot path. Start
                                # the exit sequence only after that response has been pushed.
                                schedule_domin_return()
                        else:
                            m_entry['state'] = 2
                            if session is not None:
                                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                                conn.sendall(struct.pack(">H", len(pf)) + pf)
                    else:
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 114: # abandon_mission
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                if picked_char and mid in picked_char.get('active_missions', {}):
                    del picked_char['active_missions'][mid]
                    save_chars(all_accounts_chars)
                    send_rpc_push(522, encode_sproto([(0, mid), (1, 0)]))
                    send_rpc_push(519, sync_mission_data(picked_char))
                    print(f"[MISSION ABANDON] mission_id={mid}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 121: # request_daily_mission
                if picked_char:
                    for dmid, dmdata in picked_char.get('active_missions', {}).items():
                        mcfg = missions_data.get(dmid, {})
                        if mcfg.get('class') == 4:
                            parm = dmdata.get('parm', [0]*8)
                            if len(parm) < 8: parm += [0]*(8-len(parm))
                            own_bytes = encode_sproto([
                                (0, str(dmid)),
                                (1, int(dmdata.get('state', 1))),
                                (2, 0),
                                (3, [int(x) for x in parm])
                            ])
                            send_rpc_push(530, encode_sproto([(0, own_bytes)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 524: # set_mission_param
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                # TAG 524 is a server-to-client progress update in this APK.
                # No client request path writes mission parameters directly;
                # progress must come from validated gameplay events.
                print(f"[MISSION SECURITY] ignored client MSG 524 mission={mid}")
                if picked_char:
                    send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 523: # set_mission_state
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                state = get_val_int(body, 1)
                # The APK only receives TAG 523 to apply a server decision.
                # Accepting it from the client lets it mark missions complete
                # (and collect rewards through MSG 113) without doing the task.
                print(f"[MISSION SECURITY] ignored client MSG 523 mission={mid} state={state}")
                if picked_char:
                    send_rpc_push(519, sync_mission_data(picked_char))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 130: # skill_level_up
                sid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                cur_lv = get_val_int(body, 1)
                if picked_char:
                    prof_skills = PROF_SKILLS.get(int(picked_char.get('prof', 0)), PROF_SKILLS[0])
                    valid_skill_ids = set(prof_skills['atk'] if isinstance(prof_skills['atk'], list) else [prof_skills['atk']])
                    valid_skill_ids.update((prof_skills['dodge'], prof_skills['starter_active']))
                    skill_levels = picked_char.setdefault('skill_levels', {})
                    skill_key = next((key for key in skill_levels if str(key) == sid), sid)
                    server_skill_lv = int(skill_levels.get(skill_key, 0))
                    skill_is_owned = sid in valid_skill_ids or skill_key in skill_levels
                    skill_matches_server = skill_is_owned and cur_lv == server_skill_lv and cur_lv >= 0
                    cost = get_skill_upgrade_cost(cur_lv) if skill_matches_server else None
                    if (cost is not None and cost > 0
                            and picked_char.get('cash', 0) >= cost
                            and picked_char.get('level', 1) > server_skill_lv + 1):
                        picked_char['cash'] -= cost
                        skill_levels[skill_key] = server_skill_lv + 1
                        save_chars(all_accounts_chars)
                        smap = build_skills_map(picked_char['prof'], picked_char['level'], picked_char['skill_levels'])
                        send_rpc_push(540, encode_sproto([(0, smap), (1, True)]))
                        sync_char_attrs_rpc(conn, picked_char)
                    elif not skill_matches_server:
                        print(f"[SKILL SECURITY] rejected inconsistent/unknown upgrade sid={sid} client_lv={cur_lv} server_lv={server_skill_lv}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 102: # skill_use
                sid = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1, '')); tid = get_val_int(body, 0); alist = body.get(3, [])
                exp_skill_map = str(picked_char.get('map_id', '')) if picked_char else ''
                exp_skill_state = picked_char.get('exp_stage_state') if picked_char and is_exp_map(exp_skill_map) else None
                if picked_char and is_exp_map(exp_skill_map) and (
                        not is_current_character_connection(conn, picked_char)
                        or not exp_skill_state
                        or exp_run_map_id(exp_skill_state) != exp_skill_map
                        or exp_skill_state.get('_finish_handled')
                        or int(picked_char.get('id', 0)) not in exp_member_ids(exp_skill_state)
                        or int(picked_char.get('hp', 0)) <= 0):
                    print(f"[EXP SKILL] rejected downed/nonparticipant/stale player={picked_char.get('id')} map={exp_skill_map}")
                elif picked_char:
                    locked, req_lv = is_skill_locked(sid, picked_char.get('level', 1), picked_char.get('prof', 0))
                    if locked:
                        print(f"[SKILL LOCKED] sid={sid} req={req_lv}")
                        send_rpc_push(529, encode_sproto([(0, "#{100681}"), (1, True)]))
                    else:
                        # Do not echo Tag 508 back to the casting player.
                        # The casting player executes skill effects locally on client.
                        # Broadcast TAG 508 (ret_skill_use) to other players on the same map
                        attacker_id = picked_char.get('id', 0)
                        attacker_map = str(picked_char.get('map_id', '11'))
                        try:
                            # Build attack_list from client-provided alist
                            attack_list_data = []
                            if isinstance(alist, list):
                                for atk in alist:
                                    if isinstance(atk, (int, str)):
                                        attack_list_data.append(atk)
                                    elif isinstance(atk, bytes):
                                        attack_list_data.append(atk)
                            # ret_skill_use: senderId(0), targetId(1), skillId(2), attack_list(3)
                            skill_resp = encode_sproto([
                                (0, attacker_id),
                                (1, tid),
                                (2, sid),
                                (3, attack_list_data)
                            ])
                            ph_p = encode_sproto([(0, 508)])
                            pf_p = sproto_pack(ph_p + skill_resp)
                            skill_pkt = struct.pack(">H", len(pf_p)) + pf_p
                            for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
                                # EXP map IDs are shared by independent solo
                                # runs and squads. Keep visual skill broadcasts
                                # inside the same live run instance.
                                if ch and can_aoi_exchange(picked_char, ch):
                                    try:
                                        c_lock = CONNECTION_LOCKS.get(cid)
                                        if c_lock:
                                            with c_lock:
                                                c.sendall(skill_pkt)
                                        else:
                                            c.sendall(skill_pkt)
                                    except Exception:
                                        pass
                            print(f"[SKILL] Broadcast 508 skill={sid} target={tid} from={attacker_id} map={attacker_map}")
                        except Exception as e:
                            print(f"[SKILL] Failed to broadcast 508: {e}")

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 235: # request_mount_info
                if picked_char:
                    # ret_mount_info.mount_info(0): every garage vehicle with
                    # its configured colour list, ownership and selected colour.
                    send_rpc_push(630, encode_sproto([(0, build_mount_info(picked_char))]))
                    print(f"[MOUNT] sent garage state vehicles={len(MOUNT_CONFIG)}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in (236, 237): # mount_equip / mount_unequip
                mount_id = field_text(body, 0)
                if picked_char and mount_id in MOUNT_CONFIG:
                    mounts = picked_char.setdefault('mounts', {})
                    target = mounts.setdefault(mount_id, {})
                    if int(target.get('state', 0)) > 0:
                        if msg == 236:
                            for state in mounts.values():
                                if int(state.get('state', 0)) == 2:
                                    state['state'] = 1
                            target['state'] = 2
                            picked_char['equipped_mount_id'] = mount_id
                        else:
                            target['state'] = 1
                            if str(picked_char.get('equipped_mount_id', '')) == mount_id:
                                picked_char['equipped_mount_id'] = ''
                        save_chars(all_accounts_chars)
                        send_rpc_push(631, encode_sproto([(0, build_mount_info(picked_char)), (1, mount_id)]))
                        # The normal city garage response updates only the
                        # garage panel.  The APK's Street Race gate reads
                        # MainPlayer.MountId, so mirror the equipped vehicle
                        # into the live player visual immediately.
                        sync_main_player_visual(picked_char, send_rpc_push)
                        print(f"[MOUNT] {'equipped' if msg == 236 else 'unequipped'} id={mount_id}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 241: # mount_use_color / colour purchase or selection
                mount_id = field_text(body, 0)
                color_id = field_text(body, 1)
                if picked_char and mount_id in MOUNT_CONFIG:
                    mount_cfg = MOUNT_CONFIG[mount_id]
                    response_color = None
                    bought_color = False
                    with get_character_transaction_lock(picked_char):
                        mounts = picked_char.setdefault('mounts', {})
                        mount = mounts.setdefault(mount_id, {})
                        if (mount_cfg.get('need_show') == 1 and
                                int(mount.get('state', 0)) == 2):
                            configured_colors = set(mount_cfg.get('colors', []))
                            raw_unlocked = mount.get('unlocked_colors', [])
                            if not isinstance(raw_unlocked, (list, tuple, set)):
                                raw_unlocked = []
                            unlocked = set(str(x) for x in raw_unlocked)
                            default_color = str(mount_cfg.get('default_color', ''))
                            if default_color in configured_colors:
                                unlocked.add(default_color)

                            if color_id in configured_colors and color_id in unlocked:
                                # An owned color can be selected at no cost.
                                mount['select'] = color_id
                                mount['unlocked_colors'] = sorted(unlocked, key=lambda x: (len(x), x))
                                response_color = color_id
                            elif color_id in configured_colors:
                                # A locked color is a purchase. Price and balance
                                # come from server assets/state, never request data.
                                color_price = COLOR_CONFIG.get(color_id)
                                if color_price:
                                    price_type = color_price.get('price_type', -1)
                                    price = color_price.get('price', -1)
                                    balance = get_currency_balance(picked_char, price_type)
                                    if (0 <= price_type < len(MONEY_FIELDS) and
                                            0 <= price and balance >= price):
                                        set_currency_balance(picked_char, price_type, balance - price)
                                        unlocked.add(color_id)
                                        mount['unlocked_colors'] = sorted(unlocked, key=lambda x: (len(x), x))
                                        mount['select'] = color_id
                                        response_color = color_id
                                        bought_color = True
                                    else:
                                        print(f"[MOUNT SECURITY] rejected color purchase mount={mount_id} color={color_id} price={price} currency={price_type} balance={balance}")
                                else:
                                    print(f"[MOUNT SECURITY] rejected color without config mount={mount_id} color={color_id}")

                            if response_color is not None:
                                save_chars(all_accounts_chars)
                                if bought_color:
                                    sync_char_attrs_rpc(conn, picked_char)
                                sync_main_player_visual(picked_char, send_rpc_push)
                                print(f"[MOUNT] {'color purchased' if bought_color else 'color selected'} id={mount_id} color={response_color}")
                            else:
                                # This response closes the client's wait box and
                                # restores the authoritative selection after a
                                # rejected/forged color request.
                                response_color = str(mount.get('select') or default_color)
                                if response_color not in configured_colors:
                                    response_color = default_color

                    if response_color is not None:
                        # ret_mount_use_color includes the full map, as required
                        # by PlayerCarRootLogic, including on a rejected purchase.
                        send_rpc_push(632, encode_sproto([
                            (0, build_mount_info(picked_char)), (1, mount_id), (2, response_color)
                        ]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 267: # change_mount_state (refresh an existing vehicle state)
                mount_id = field_text(body, 0)
                if picked_char and mount_id in MOUNT_CONFIG:
                    mount = picked_char.setdefault('mounts', {}).setdefault(mount_id, {})
                    if int(mount.get('state', 0)) == 3:
                        mount['state'] = 1
                        save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 323: # buy_car_shop
                mount_id = field_text(body, 0)
                if picked_char and mount_id in MOUNT_CONFIG:
                    with get_character_transaction_lock(picked_char):
                        mount_cfg = MOUNT_CONFIG[mount_id]
                        mount = picked_char.setdefault('mounts', {}).setdefault(mount_id, {})
                        if int(mount.get('state', 0)) == 0:
                            price_type = int(mount_cfg.get('price_type', -1))
                            price = int(mount_cfg.get('price', -1))
                            balance = get_currency_balance(picked_char, price_type)
                            if (mount_cfg.get('need_show') == 1 and
                                    0 <= price_type < len(MONEY_FIELDS) and
                                    price >= 0 and balance >= price):
                                set_currency_balance(picked_char, price_type, balance - price)
                                mount['state'] = 1
                                mount.setdefault('select', mount_cfg['default_color'])
                                mount.setdefault('unlocked_colors', [mount_cfg['default_color']])
                                save_chars(all_accounts_chars)
                                sync_char_attrs_rpc(conn, picked_char)
                                print(f"[MOUNT] garage purchase id={mount_id} price={price} currency={price_type}")
                            else:
                                print(f"[MOUNT SECURITY] rejected purchase id={mount_id} price={price} currency={price_type} balance={balance}")
                        # ret_buy_car_shop.mountId(0), state(1)
                        send_rpc_push(691, encode_sproto([(0, mount_id), (1, int(mount.get('state', 1)))]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 115: # use_item, including vehicle exchange vouchers
                index_id = get_val_int(body, 0, -1)
                success = 0
                if picked_char:
                    inventory = picked_char.get('inventory', [])
                    item_index = index_id - 10000
                    if 0 <= item_index < len(inventory):
                        item = inventory[item_index]
                        mount_id = mount_id_from_voucher(item.get('id'))
                        if mount_id:
                            mount = picked_char.setdefault('mounts', {}).setdefault(mount_id, {})
                            mount['state'] = max(1, int(mount.get('state', 0)))
                            mount.setdefault('select', MOUNT_CONFIG[mount_id]['default_color'])
                            mount.setdefault('unlocked_colors', [MOUNT_CONFIG[mount_id]['default_color']])
                            item['amount'] -= 1
                            if item['amount'] < 1:
                                inventory.pop(item_index)
                            success = 1
                            save_chars(all_accounts_chars)
                            send_rpc_push(611, sync_inventory_data(picked_char))
                            send_rpc_push(630, encode_sproto([(0, build_mount_info(picked_char))]))
                            print(f"[MOUNT] voucher redeemed item={item.get('id')} vehicle={mount_id}")
                        elif item.get('id') in ITEM_CONFIG:
                            cfg = ITEM_CONFIG[item['id']]
                            if cfg['type'] == 18: # REMAIN ticket
                                subtype = cfg['function']
                                state = ensure_daily_copy_state(picked_char)
                                # Find all copy IDs for this subtype and reset their remaining counts.
                                # The client uses Item 9205 (SubType 7) for Street Race.
                                for copy_id, sc_cfg in COPY_SCENE_CONFIG.items():
                                    if sc_cfg['subtype'] == subtype:
                                        state['remaining'][copy_id] = sc_cfg['max_plays']
                                item['amount'] -= 1
                                if item['amount'] < 1:
                                    inventory.pop(item_index)
                                success = 1
                                save_chars(all_accounts_chars)
                                send_rpc_push(611, sync_inventory_data(picked_char))
                                send_rpc_push(555, sync_copy_scenes(picked_char))
                                print(f"[TICKET] used item={item['id']} for subtype={subtype}")
                send_rpc_push(526, encode_sproto([(0, success), (1, index_id)]))

                # TAG 525: update_item - notify client about individual item change
                # update_item schema: containertype(0), indexId(1), gameitem(2)
                # gameitem schema: indexId(0), itemId(1), bindflag(2), level(3), flags(4), stack(5), quality(6), parm(7), appraise(8)
                if success and picked_char:
                    inventory = picked_char.get('inventory', [])
                    item_index = index_id - 10000
                    if 0 <= item_index < len(inventory):
                        item = inventory[item_index]
                        gi = encode_sproto([
                            (0, index_id),          # indexId
                            (1, item['id']),        # itemId
                            (2, False),             # bindflag
                            (3, 0),                 # level
                            (5, item.get('amount', 1)),  # stack
                            (6, 0)                  # quality
                        ])
                        send_rpc_push(525, encode_sproto([
                            (0, 1),  # containertype: ITEM_BACKPACK
                            (1, index_id),
                            (2, gi)
                        ]))

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in (238, 239): # use_mount / unuse_mount
                # The APK applies the visual mount/dismount locally.  Persist
                # the packet acknowledgement so it never stalls on a request.
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 128: # local_character_attack (Boss counter-attack)
                target_id = get_val_int(body, 0)
                client_dmg = get_val_int(body, 1)
                effinfo_id = body.get(2, b"").decode('utf-8') if isinstance(body.get(2), bytes) else str(body.get(2, ''))

                # Initialize server_dmg to 0 to handle malformed packets safely
                server_dmg = 0
                calc_cri = False

                if picked_char and is_exp_map(picked_char.get('map_id', '')):
                    # EXP monster damage is calculated by the server AI from
                    # the active wave. This client-local NPC report carries no
                    # monster instance ID, so accepting it would let a client
                    # inject damage and bypass the run's death/revive state.
                    print(f"[EXP HIT] ignored client-local NPC attack player={picked_char.get('id')}")
                elif picked_char and target_id == picked_char['id']:
                    is_area = (picked_char.get('map_id') == "502")
                    if is_area:
                        # Server-authoritative boss->player damage calculation
                        boss_id = picked_char.get('boss_inst_id')
                        if boss_id:
                            player_level = picked_char.get('level', 1)
                            boss_stats = get_npc_attr("1105", player_level)
                            player_stats = get_character_stats(picked_char)

                            # Use client-sent effinfo_id to determine which boss skill was used.
                            # Only allow effects belonging to the boss's actual skill map:
                            # 101-103 (basic attacks), 104 (dodge), 105, 106, 110 (active skills).
                            boss_allowed_effects = set()
                            for boss_sid in ('101', '102', '103', '104', '105', '106', '110'):
                                bcfg = SKILL_CONFIG.get(boss_sid, {})
                                for eff_key in ('eff0', 'eff1', 'eff2'):
                                    eff = bcfg.get(eff_key, '')
                                    if eff:
                                        boss_allowed_effects.add(eff)

                            if effinfo_id and effinfo_id in boss_allowed_effects and effinfo_id in EFF_TO_SKILL:
                                boss_skill_id = EFF_TO_SKILL[effinfo_id]
                            else:
                                # Fallback: if effinfo_id not recognized or not a boss effect, use basic attack
                                boss_skill_id = "101"
                                skill_cfg = SKILL_CONFIG.get(boss_skill_id, {})
                                effinfo_id = skill_cfg.get('eff0', "10000")

                            server_dmg, hit, calc_cri = get_combat_damage(
                                boss_stats, player_stats, boss_skill_id, 1, effinfo_id=effinfo_id
                            )
                            if not hit:
                                server_dmg = 0
                                calc_cri = False

                            print(f"[AREA BOSS ATTACK] server_dmg={server_dmg} cri={calc_cri} eff={effinfo_id}")
                        else:
                            server_dmg = 0
                            calc_cri = False
                            print(f"[AREA BOSS ATTACK] No boss_inst_id found, damage rejected")
                    # For non-arena Tag 128, the attacker is a local NPC/zombie in a single-copy scene.
                    # The client sends Tag 128 with characterId = player's ServerId (the target),
                    # not the attacker's ID. Without knowing which NPC attacked, the server cannot
                    # look up the correct attacker stats for exact server-authoritative calculation.
                    # However, we should NOT trust client_dmg directly as it allows damage manipulation.
                    # Use a reasonable default NPC attacker stats for server-authoritative calculation.
                    if not is_area and picked_char:
                        # Use a generic NPC attacker with basic stats for non-arena single-copy scenes
                        # The original game trusted client-authoritative damage, but server-side validation
                        # prevents extreme manipulation while still allowing the attack to deal damage.
                        generic_npc_stats = get_npc_attr("100", picked_char.get('level', 1))
                        player_stats = get_character_stats(picked_char)
                        server_dmg, hit, calc_cri = get_combat_damage(
                            generic_npc_stats, player_stats, "101", 1, effinfo_id=effinfo_id
                        )
                        if not hit:
                            server_dmg = 0
                            calc_cri = False

                    new_hp = picked_char.get('hp', 0) - server_dmg
                    picked_char['hp'] = max(0, new_hp)
                    sync_char_attrs_rpc(conn, picked_char)
                    if picked_char['hp'] == 0 and picked_char.get('map_id') == '502':
                        print('[M1003 DEBUG] Player died in arena; scheduling loss return')
                        schedule_domin_return(restore_hp=True)

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 111: # accept_damge
                if picked_char:
                    dlist_raw = body.get(0, b"")
                    dlist = decode_sproto_list(dlist_raw)
                    print(f"[COMBAT] RX 111 count={len(dlist)}")
                    attacker_stats = get_character_stats(picked_char)
                    exp_effects_in_batch = set()
                    exp_targets_in_batch = set()
                    for d_bytes in dlist:
                        d = decode_sproto(d_bytes)
                        target_id = get_val_int(d, 0)
                        client_dmg = get_val_int(d, 1)
                        # acceptdamge schema: id(0), damage(1), skillId(2), effinfoId(3), cri(4), parm(5-8)
                        skill_id = get_val_str(d, 2, "50001")
                        # Accept only a skill's configured effects below; never
                        # use an arbitrary effect id supplied by the client.
                        requested_effinfo_id = get_val_str(d, 3, "")
                        effinfo_id = None
                        is_cri = get_val_int(d, 4, 0) == 1
                        is_exp = is_exp_map(picked_char.get('map_id'))
                        exp_state_hit = picked_char.get('exp_stage_state') if is_exp else None
                        current_conn, current_char = ALL_CONNECTIONS.get(int(picked_char.get('id', 0)), (None, None))

                        # EXP damage is only valid for a live participant in
                        # the same run. This also prevents stale/reconnected
                        # characters from reusing a squad's shared run state.
                        if is_exp and (
                                not exp_state_hit
                                or exp_run_map_id(exp_state_hit) != str(picked_char.get('map_id'))
                                or exp_state_hit.get('_finish_handled')
                                or int(picked_char.get('id', 0)) not in exp_member_ids(exp_state_hit)
                                or int(picked_char.get('hp', 0)) <= 0
                                or current_conn is not conn or current_char is not picked_char):
                            print(f"[EXP HIT] rejected downed/nonparticipant/stale attacker={picked_char.get('id')} map={picked_char.get('map_id')}")
                            continue

                        print(f"[COMBAT] accept_damge target={target_id} client_dmg={client_dmg} skill={skill_id} eff={requested_effinfo_id} cri={is_cri}")

                        # --- Check if target is an online player (PvP) ---
                        target_cid, target_conn, target_char = find_player_connection(target_id)

                        if target_char is not None:
                            if is_exp:
                                print(f"[EXP HIT] rejected player target={target_id} attacker={picked_char.get('id')}")
                                continue
                            # === PvP: target is an online player ===
                            # Server-authoritative damage calculation
                            target_stats = get_character_stats(target_char)

                            # Verify the attacker actually has this skill learned.
                            # Starter skills (101-105 for XD, 201-205 for QJ, 301-305 for NQS)
                            # are always available at level 0, even if not in skill_levels.
                            char_skill_levels = picked_char.get('skill_levels', {})
                            prof = picked_char.get('prof', 0)
                            starter_skills = PROF_SKILLS.get(prof, PROF_SKILLS[0])
                            always_available = set(starter_skills.get('atk', []))
                            always_available.add(starter_skills.get('dodge', ''))
                            always_available.add(starter_skills.get('starter_active', ''))
                            if not is_exp and skill_id not in char_skill_levels and skill_id not in always_available:
                                # Skill not learned by player and not a starter skill, reject
                                skill_id = "50001"
                                effinfo_id = None
                            skill_lv = char_skill_levels.get(skill_id, 0)
                            if skill_lv < 0:
                                skill_id = "50001"
                                skill_lv = 0
                                effinfo_id = None

                            skill_cfg = SKILL_CONFIG.get(skill_id, {})
                            if effinfo_id is None:
                                effinfo_id = skill_cfg.get('eff0', "10000")

                            server_dmg, hit, calc_cri = get_combat_damage(attacker_stats, target_stats, skill_id, skill_lv, pvp_scale=1.0, effinfo_id=effinfo_id)
                            if not hit:
                                server_dmg = 0
                                calc_cri = False
                            print(f"[PvP] Server calc dmg={server_dmg} cri={calc_cri} target={target_id}")

                            # Apply damage to victim
                            old_hp = target_char.get('hp', target_stats['hp_max'])
                            new_hp = max(0, old_hp - server_dmg)
                            target_char['hp'] = new_hp

                            # Sync victim's HP to victim's own client (TAG 510)
                            sync_char_attrs_rpc(target_conn, target_char)
                            # Broadcast victim's updated HP to all other players on the map
                            broadcast_aoi_attribute(target_char)

                            # Save HP state so it persists after reconnect
                            save_chars(all_accounts_chars)

                            # Build TAG 511 damage board with actual skill/eff IDs
                            dmg_item = encode_sproto([
                                (0, target_id),
                                (1, server_dmg),
                                (2, skill_id),
                                (3, effinfo_id),
                                (4, calc_cri)
                            ])
                            dmg_board = encode_sproto([(0, [dmg_item])])

                            # Do NOT echo TAG 511 back to the attacker; the client already displays damage locally.
                            # Send TAG 511 to victim so they see damage numbers on themselves
                            try:
                                t_ph = encode_sproto([(0, 511)])
                                t_pf = sproto_pack(t_ph + dmg_board)
                                t_pkt = struct.pack(">H", len(t_pf)) + t_pf
                                t_lock = CONNECTION_LOCKS.get(target_cid)
                                if t_lock:
                                    with t_lock:
                                        target_conn.sendall(t_pkt)
                                else:
                                    target_conn.sendall(t_pkt)
                            except Exception:
                                pass

                            # Build TAG 514 hit_action with actual effinfoId
                            hit_action = encode_sproto([
                                (0, target_id),
                                (1, picked_char['id']),
                                (2, effinfo_id)
                            ])

                            # Send TAG 514 (hit_action) to attacker
                            send_rpc_push(514, hit_action)
                            # Send TAG 514 to victim so they see hit effects on themselves
                            try:
                                t_ph = encode_sproto([(0, 514)])
                                t_pf = sproto_pack(t_ph + hit_action)
                                t_pkt = struct.pack(">H", len(t_pf)) + t_pf
                                t_lock = CONNECTION_LOCKS.get(target_cid)
                                if t_lock:
                                    with t_lock:
                                        target_conn.sendall(t_pkt)
                                else:
                                    target_conn.sendall(t_pkt)
                            except Exception:
                                pass

                            # Check if victim died
                            if new_hp == 0:
                                print(f"[PvP] Player {target_id} killed by {picked_char['id']}")
                                # Send relife request to victim
                                death_count = target_char.get('death_count', 0) + 1
                                target_char['death_count'] = death_count
                                relife_cfg = get_relife_config(death_count)
                                # Issue 64: Use REBIRTH_TYPE enum (0=MAIN_CITY, 1=CURRENT_MAP), not RelifeData tier ID
                                # PvP death: victim returns to main city
                                relife_req = encode_sproto([
                                    (0, 0),  # MAIN_CITY_REBIRTH
                                    (1, relife_cfg.get('use_count', 1)),  # cost field - item cost
                                    (2, "9202"),
                                    (3, target_char['id']),
                                    (4, target_char['name'])
                                ])
                                try:
                                    t_ph = encode_sproto([(0, 618)])
                                    t_pf = sproto_pack(t_ph + relife_req)
                                    t_pkt = struct.pack(">H", len(t_pf)) + t_pf
                                    t_lock = CONNECTION_LOCKS.get(target_cid)
                                    if t_lock:
                                        with t_lock:
                                            target_conn.sendall(t_pkt)
                                    else:
                                        target_conn.sendall(t_pkt)
                                except Exception:
                                    pass

                        elif target_id == picked_char['id']:
                            if is_exp:
                                print(f"[EXP HIT] rejected self target attacker={picked_char.get('id')}")
                                continue
                            # Self-damage (rare edge case)
                            new_hp = picked_char.get('hp', 0) - client_dmg
                            picked_char['hp'] = max(0, new_hp)
                            sync_char_attrs_rpc(conn, picked_char)
                            if picked_char['hp'] == 0:
                                if picked_char.get('map_id') == '502':
                                    print('[M1003 DEBUG] Player died in arena; scheduling loss return')
                                    schedule_domin_return(restore_hp=True)
                                else:
                                    death_count = picked_char.get('death_count', 0) + 1
                                    picked_char['death_count'] = death_count
                                    relife_cfg = get_relife_config(death_count)
                                    # Issue 64: Use REBIRTH_TYPE enum (0=MAIN_CITY, 1=CURRENT_MAP), not RelifeData tier ID
                                    relife_req = encode_sproto([
                                        (0, 1),  # CURRENT_MAP_REBIRTH
                                        (1, relife_cfg.get('use_count', 1)),  # cost field - item cost
                                        (2, "9202"),
                                        (3, picked_char['id']),
                                        (4, picked_char['name'])
                                    ])
                                    send_rpc_push(618, relife_req)

                            # Do NOT echo TAG 511 back to the attacker; the client already displays damage locally.
                            # Issue 213: TAG 514 (hit_action) client handler does nothing — skip sending

                        else:
                            # === NPC/Monster/Boss damage ===
                            # Map 11 NPCs are fully client-side - skip all server-side HP/death handling
                            # Do NOT advance missions on damage — only on actual death (tag 307)
                            if picked_char and str(picked_char.get('map_id')) == '11':
                                # Issue 213: TAG 514 (hit_action) client handler does nothing — skip sending
                                continue

                            cur_map = str(picked_char.get('map_id', ''))
                            is_exp = is_exp_map(cur_map)
                            exp_state_hit = picked_char.get('exp_stage_state') if is_exp else None
                            if not is_exp and (
                                    target_id not in NPC_INST_MAP
                                    or str(NPC_INSTANCE_MAP.get(target_id, '')) != cur_map
                                    or target_id not in NPC_HP_MAP):
                                print(f"[COMBAT SECURITY] rejected unknown/cross-map NPC target={target_id} map={cur_map}")
                                continue
                            if is_exp:
                                active_ids = exp_state_hit.get('active_monsters', []) if exp_state_hit else []
                                spawned_ids = exp_state_hit.get('spawned_npcs', set()) if exp_state_hit else set()
                                if (not exp_state_hit or exp_run_map_id(exp_state_hit) != cur_map
                                        or exp_state_hit.get('_finish_handled')
                                        or int(picked_char.get('id', 0)) not in exp_member_ids(exp_state_hit)
                                        or target_id not in active_ids or target_id not in spawned_ids
                                        or target_id not in NPC_HP_MAP or target_id not in NPC_INST_MAP
                                        or target_id in DEAD_NPC_SET):
                                    print(f"[EXP HIT] rejected unowned/inactive target={target_id} map={cur_map}")
                                    continue

                            # Check if NPC is already dead (Tag 137 may have killed it first)
                            # This prevents processing damage after the boss is already dead
                            if target_id in DEAD_NPC_SET:
                                print(f"[COMBAT] Tag 111 rejected: NPC {target_id} already dead")
                                continue

                            target_nid = NPC_INST_MAP.get(target_id, str(target_id))
                            if not is_exp:
                                NPC_INST_MAP[target_id] = target_nid
                            print(f"[EXP MSG111] target_id={target_id} target_nid={target_nid} map={cur_map} is_exp={is_exp} client_dmg={client_dmg}")

                            nid_str = "1105" if target_nid.startswith("BOSS_") else target_nid
                            defender_stats = get_npc_attr(nid_str, picked_char.get('level', 1))

                            if target_id not in NPC_HP_MAP:
                                NPC_HP_MAP[target_id] = defender_stats['hp_max']

                            # Server-authoritative NPC damage calculation
                            if is_exp:
                                validated_hit = validate_exp_skill_hit(
                                    picked_char, exp_state_hit, target_id, skill_id,
                                    requested_effinfo_id, exp_effects_in_batch,
                                    exp_targets_in_batch)
                                if validated_hit is None:
                                    continue
                                skill_id, skill_lv, effinfo_id = validated_hit
                            else:
                                # Verify the attacker actually has this skill learned.
                                # Starter skills (101-105 for XD, 201-205 for QJ, 301-305 for NQS)
                                # are always available at level 0, even if not in skill_levels.
                                char_skill_levels = picked_char.get('skill_levels', {})
                                prof = picked_char.get('prof', 0)
                                starter_skills = PROF_SKILLS.get(prof, PROF_SKILLS[0])
                                always_available = set(starter_skills.get('atk', []))
                                always_available.add(starter_skills.get('dodge', ''))
                                always_available.add(starter_skills.get('starter_active', ''))
                                if skill_id not in char_skill_levels and skill_id not in always_available:
                                    # Skill not learned by player and not a starter skill, reject
                                    skill_id = "50001"
                                    effinfo_id = None
                                skill_lv = char_skill_levels.get(skill_id, 0)
                                if skill_lv < 0:
                                    skill_id = "50001"
                                    skill_lv = 0
                                    effinfo_id = None

                                # Server-authoritative effinfo_id: derive from skill_id, not from client
                                if effinfo_id is None:
                                    skill_cfg = SKILL_CONFIG.get(skill_id, {})
                                    effinfo_id = skill_cfg.get('eff0', "10000")

                            server_dmg, hit, calc_cri = get_combat_damage(
                                attacker_stats, defender_stats, skill_id, skill_lv, effinfo_id=effinfo_id, sat_type='satm'
                            )
                            if not hit:
                                server_dmg = 0
                                calc_cri = False

                            if is_exp:
                                # Keep the client packet as the hit-event signal,
                                # but use the server's own hit/crit/variance roll
                                # for HP. The previous ceiling calculation let
                                # clients force criticals and max variance.
                                actual_dmg = server_dmg if client_dmg > 0 else 0
                            else:
                                actual_dmg = server_dmg

                            exp_npc_died = False
                            exp_dead_target = False
                            if is_exp:
                                # A squad can submit hits from different client
                                # threads at nearly the same time. Recheck and
                                # commit HP/death under one per-NPC lock so a
                                # second hit cannot damage an HP-zero enemy or
                                # race the kill counter.
                                with exp_npc_hp_lock(exp_state_hit, target_id):
                                    active_now = exp_state_hit.get('active_monsters', [])
                                    spawned_now = exp_state_hit.get('spawned_npcs', set())
                                    hp_now = NPC_HP_MAP.get(target_id)
                                    if (exp_state_hit.get('_finish_handled')
                                            or int(picked_char.get('hp', 0)) <= 0
                                            or target_id not in active_now
                                            or target_id not in spawned_now
                                            or hp_now is None
                                            or target_id not in NPC_INST_MAP
                                            or target_id in DEAD_NPC_SET):
                                        exp_dead_target = True
                                    elif int(hp_now) <= 0:
                                        # Recover a zero-HP instance left by an
                                        # earlier interrupted death path, and
                                        # finalize it without applying another hit.
                                        NPC_HP_MAP[target_id] = 0
                                        DEAD_NPC_SET.add(target_id)
                                        exp_npc_died = True
                                        actual_dmg = 0
                                    else:
                                        hp_after = max(0, int(hp_now) - max(0, int(actual_dmg)))
                                        NPC_HP_MAP[target_id] = hp_after
                                        if hp_after == 0:
                                            DEAD_NPC_SET.add(target_id)
                                            exp_npc_died = True
                            else:
                                NPC_HP_MAP[target_id] -= actual_dmg

                            if exp_dead_target:
                                print(f"[EXP HIT] rejected target already dead/inactive target={target_id}")
                                continue

                            # Sync changed HP and always deliver HP=0 when death is finalized.
                            if actual_dmg > 0 or (is_exp and exp_npc_died):
                                if exp_state_hit and exp_state_hit.get('team_id'):
                                    exp_sync_npc_attrs(exp_state_hit, target_id, defender_stats, max(0, NPC_HP_MAP[target_id]))
                                else:
                                    sync_npc_attrs_rpc(conn, target_id, defender_stats, max(0, NPC_HP_MAP[target_id]))
                            if exp_state_hit and exp_state_hit.get('team_id'):
                                hit_entry = encode_sproto([(0, target_id), (1, actual_dmg), (2, skill_id), (3, effinfo_id or ''), (4, calc_cri)])
                                exp_send_push(exp_state_hit, 511, encode_sproto([(0, [hit_entry])]), exclude=[picked_char.get('id', 0)])
                                skill_hit = encode_sproto([(0, picked_char.get('id', 0)), (1, target_id), (2, skill_id), (3, [encode_sproto([(0, target_id), (1, actual_dmg)])])])
                                exp_send_push(exp_state_hit, 508, skill_hit, exclude=[picked_char.get('id', 0)])
                            # For non-exp maps, send TAG 511 so client displays damage.
                            if not is_exp:
                                dmg_entry = encode_sproto([
                                    (0, target_id),
                                    (1, server_dmg),
                                    (2, skill_id),
                                    (3, effinfo_id),
                                    (4, calc_cri)
                                ])
                                dmg_board = encode_sproto([(0, [dmg_entry])])
                                send_rpc_push(511, dmg_board)

                            # Issue 213: TAG 514 (hit_action) client handler does nothing — skip sending

                            npc_died = exp_npc_died if is_exp else NPC_HP_MAP[target_id] <= 0
                            if npc_died:
                                # Fix: Only process death once - skip if already dead
                                if not is_exp and target_id in DEAD_NPC_SET:
                                    print(f"[EXP MSG111] NPC already dead target_id={target_id}, skipping")
                                    continue
                                if not is_exp:
                                    DEAD_NPC_SET.add(target_id)
                                print(f"[EXP MSG111] NPC DEAD target_id={target_id} target_nid={target_nid} hp={NPC_HP_MAP[target_id]}")

                                if not is_exp:
                                    # Non-EXP despawns keep their existing AOI path.
                                    send_rpc_push(506, encode_sproto([(0, target_id)]))
                                # EXP clients receive HP=0 above. Their ObjNPC
                                # death handler plays the death state and
                                # recycles the body after its native delay; an
                                # immediate AOI remove skips that lifecycle and
                                # can leave inconsistent visible/invisible NPCs.

                                # BOSS DEATH HANDLING
                                if target_id == picked_char.get('boss_inst_id'):
                                    if defender_stats:
                                        a_oth_fields = [(0, 0), (2, defender_stats['lv'])]
                                        if NPC_INST_MAP.get(target_id, '').startswith('BOSS_'):
                                            a_oth_fields.extend([(4, 1), (15, 2)])
                                        a_oth = encode_sproto(a_oth_fields)
                                        aoi_attr = encode_sproto([(0, target_id), (1, a_oth)])
                                        send_rpc_push(510, encode_sproto([(0, aoi_attr)]))

                                    did = picked_char.get('active_domin_id', '1')
                                    print(f"[M1003 DEBUG] Boss {target_id} killed by client dmg. Winning did={did}")
                                    cap_target = did
                                    for act_m, act_mdata in list(picked_char.get('active_missions', {}).items()):
                                        cap_cfg = missions_data.get(act_m, {})
                                        if cap_cfg.get('logic_type') == 25:
                                            cap_target = str(cap_cfg.get('target_id', did))
                                            break
                                    advance_missions(picked_char, send_rpc_push, 'capture', target_id=cap_target)
                                    # Grant NPC kill rewards (EXP/cash) and spawn drop for the boss
                                    player_level = picked_char.get('level', 1)
                                    boss_stats = get_npc_attr('1105', player_level)
                                    boss_lv = boss_stats.get('lv', 1)
                                    exp_kill, cash_kill = calculate_npc_kill_rewards(player_level, boss_lv)
                                    kill_rewards = [("2001", 0, exp_kill), ("1001", 0, cash_kill)]
                                    grant_item_rewards(picked_char, kill_rewards, conn, send_rpc_push)
                                    send_rpc_push(638, encode_sproto([(0, [
                                        encode_sproto([(0, "2001"), (1, exp_kill), (3, 0)]),
                                        encode_sproto([(0, "1001"), (1, cash_kill), (3, 0)])
                                    ])]))
                                    # Spawn drop item on the ground
                                    player_pos = picked_char.get('pos', [0, 100, 0, 0])
                                    drop_x = player_pos[0] + random.randint(-500, 500)
                                    drop_z = player_pos[2] + random.randint(-500, 500)
                                    drop_inst_id = int(time.time() * 1000) % 1000000000
                                    drop_item = encode_sproto([
                                        (0, "1001"),
                                        (1, cash_kill),
                                        (3, 0)
                                    ])
                                    send_rpc_push(527, encode_sproto([
                                        (0, drop_inst_id),
                                        (1, drop_x),
                                        (2, drop_z),
                                        (3, 0),
                                        (4, drop_item),
                                        (7, picked_char['id'])
                                    ]))
                                    picked_char['boss_inst_id'] = None
                                else:
                                    on_npc_killed(conn, send_rpc_push, picked_char, target_id, target_nid)

                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 307 or msg == 127: # local_npc_die (307) or single_copy_scene_npc_die (127)
                npcid = None
                inst_id = None
                die_type = 0

                if msg == 307:
                    val0 = body.get(0)
                    if isinstance(val0, bytes): s_val0 = val0.decode('utf-8')
                    elif val0 is not None: s_val0 = str(val0)
                    else: s_val0 = ""

                    die_type = get_val_int(body, 3)
                    try:
                        inst_id = int(s_val0)
                        npcid = NPC_INST_MAP.get(inst_id)
                    except: pass
                    if not npcid: npcid = s_val0
                else:
                    val0 = body.get(0)
                    if isinstance(val0, int): inst_id = val0
                    elif isinstance(val0, (bytes, bytearray)):
                        if len(val0) == 8: inst_id = struct.unpack("<q", val0)[0]
                        elif len(val0) == 4: inst_id = struct.unpack("<i", val0)[0]

                    val1 = body.get(1)
                    if isinstance(val1, bytes): npcid = val1.decode('utf-8')
                    elif val1 is not None: npcid = str(val1)

                    die_type = get_val_int(body, 4)
                    if not npcid and inst_id:
                        npcid = NPC_INST_MAP.get(inst_id)

                # EXP kills are finalized by the server-side damage handler
                # after authoritative HP reaches zero. Never let a client
                # death report remove HP state or advance a wave by itself.
                if picked_char and is_exp_map(picked_char.get('map_id', '')):
                    print(f"[EXP DEATH] ignored client death report inst_id={inst_id} map={picked_char.get('map_id')}")
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    continue

                cur_map = str(picked_char.get('map_id', '11')) if picked_char else ''
                report_valid = False
                if picked_char and msg == 127:
                    # MSG 127 carries the NPC's serverId. Only accept instances
                    # that this server actually created, with the matching type.
                    report_valid = bool(inst_id and str(NPC_INST_MAP.get(inst_id, '')) == str(npcid)
                                         and str(NPC_INSTANCE_MAP.get(inst_id, '')) == cur_map
                                         and int(NPC_HP_MAP.get(inst_id, 1)) <= 0)
                elif picked_char and die_type in (2, 6):
                    # Car robbery packets carry no vehicle ID. Match the
                    # active objective; targeted-car missions also require
                    # the player to be at the configured vehicle spawn.
                    report_valid = validate_car_mission_report(picked_char, die_type)
                elif picked_char and die_type == 4:
                    report_valid = validate_arrive_target_report(picked_char, npcid)
                elif picked_char and cur_map == '11':
                    report_x = get_val_int(body, 1, None)
                    report_z = get_val_int(body, 2, None)
                    if report_x is not None and report_z is not None:
                        report_valid = claim_client_local_npc_death(picked_char, npcid, report_x, report_z)
                elif picked_char and inst_id:
                    # Outside the city, client death reports must reference a
                    # server-created NPC instance on this map whose server HP
                    # has reached zero; a death report alone cannot mint kills.
                    report_valid = bool(str(NPC_INST_MAP.get(inst_id, '')) == str(npcid)
                                         and str(NPC_INSTANCE_MAP.get(inst_id, '')) == cur_map
                                         and int(NPC_HP_MAP.get(inst_id, 1)) <= 0)

                if not report_valid:
                    print(f"[NPC DEATH SECURITY] rejected client report msg={msg} inst_id={inst_id} npc={npcid} map={cur_map} type={die_type}")
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    continue

                if picked_char and die_type == 4:
                    advance_missions(picked_char, send_rpc_push, 'interact', target_id=npcid, die_type=die_type)
                    advance_missions(picked_char, send_rpc_push, 'level')
                    save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    continue

                is_duplicate = False
                if inst_id is not None and inst_id > 1000000:
                    if inst_id in DEAD_NPC_SET: is_duplicate = True
                    else: DEAD_NPC_SET.add(inst_id)

                if inst_id and inst_id in NPC_HP_MAP: del NPC_HP_MAP[inst_id]

                if picked_char and not is_duplicate:
                    if die_type in [2, 6]:
                        advance_missions(picked_char, send_rpc_push, 'car', die_type=die_type)

                    # Map 11 NPCs are fully client-side — resolve NPC ID and grant kill rewards
                    if cur_map == '11':
                        # For client-local map 11 NPCs, the inst_id is a client-generated ID.
                        # The client sends the npcid directly in field 0 of tag 307.
                        # Try to resolve the actual NPC type from KILL_TARGET_SPAWNS.
                        actual_npcid = npcid
                        if inst_id:
                            # Check if this instance was server-spawned
                            if inst_id in NPC_INST_MAP:
                                actual_npcid = NPC_INST_MAP[inst_id]
                            else:
                                # Client-local NPC — npcid from the packet is the NPC type ID
                                # For mission 1001, the client sends npcid=9901 directly
                                pass

                        # Advance kill missions
                        advance_missions(picked_char, send_rpc_push, 'kill', target_id=actual_npcid)

                        # Grant NPC kill rewards (EXP/cash) for map 11 — same as other maps
                        if actual_npcid and actual_npcid != "None":
                            npc_stats_m11 = get_npc_attr(actual_npcid, picked_char.get('level', 1))
                            npc_lv_m11 = npc_stats_m11.get('lv', 1)
                            char_lv_m11 = picked_char.get('level', 1)

                            exp_kill_m11, cash_kill_m11 = calculate_npc_kill_rewards(char_lv_m11, npc_lv_m11)
                            kill_rewards_m11 = [("2001", 0, exp_kill_m11), ("1001", 0, cash_kill_m11)]
                            grant_item_rewards(picked_char, kill_rewards_m11, conn, send_rpc_push)

                            # TAG 638: floating reward popup
                            send_rpc_push(638, encode_sproto([(0, [
                                encode_sproto([(0, "2001"), (1, exp_kill_m11), (3, 0)]),
                                encode_sproto([(0, "1001"), (1, cash_kill_m11), (3, 0)])
                            ])]))

                            # TAG 527: drop_item_info - spawn a dropped item on the ground
                            player_pos_m11 = picked_char.get('pos', [0, 100, 0, 0])
                            drop_x_m11 = player_pos_m11[0] + random.randint(-500, 500)
                            drop_z_m11 = player_pos_m11[2] + random.randint(-500, 500)
                            drop_inst_id_m11 = next_global_inst_id()
                            drop_item_m11 = encode_sproto([
                                (0, "1001"),
                                (1, cash_kill_m11),
                                (3, 0)
                            ])
                            send_rpc_push(527, encode_sproto([
                                (0, drop_inst_id_m11),
                                (1, drop_x_m11),
                                (2, drop_z_m11),
                                (3, 0),
                                (4, drop_item_m11),
                                (7, picked_char['id'])
                            ]))
                    else:
                        exp_state_death = picked_char.get('exp_stage_state')
                        if exp_state_death and exp_state_death.get('team_id') and inst_id:
                            exp_send_push(exp_state_death, 506, encode_sproto([(0, inst_id)]), exclude=[picked_char.get('id', 0)])
                        on_npc_killed(conn, send_rpc_push, picked_char, inst_id, npcid)

                    if die_type == 3:
                        advance_missions(picked_char, send_rpc_push, 'impact', target_id=npcid)
                    elif die_type == 4:
                        # For ARRIVE_TARGET, npcid field is the mission ID.
                        advance_missions(picked_char, send_rpc_push, 'interact', target_id=npcid, die_type=die_type)

                    advance_missions(picked_char, send_rpc_push, 'level')
                    sync_char_attrs_rpc(conn, picked_char)
                    save_chars(all_accounts_chars)

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 137: # rank_pvp_other_player_die
                # DominSceneManager.SuccessMission() is EMPTY — the client does NOT
                # send MSG 137 for the Domin flow. Only RankPVPLocalSceneManager
                # sends this, which is a different scene type (not map 502).
                # Ignore MSG 137 for map 502 to prevent forged victory claims.
                if picked_char and picked_char.get('map_id') == '502':
                    print('[M1003 SECURITY] MSG 137 ignored for Domin arena — DominSceneManager.SuccessMission() is empty')
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 311: # enter_domin_pk_scene
                did = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[M1003 DEBUG] RX 311 domin_id={did}")
                if picked_char:
                    # Validate Domin ID against active capture missions
                    # Only allow entering the Domin territory that matches the current mission
                    valid_domin = False
                    valid_domin_id = None
                    for act_m, act_mdata in picked_char.get('active_missions', {}).items():
                        cap_cfg = missions_data.get(act_m, {})
                        if cap_cfg.get('logic_type') == 25:
                            # Mission 1003: logic_type 25 (capture), logic_id 1, target 1105
                            # The Domin ID should match the logic_id from the mission
                            mission_logic_id = cap_cfg.get('logic_id', '')
                            if mission_logic_id == did:
                                valid_domin = True
                                valid_domin_id = did
                                break
                    # Reject if no active capture mission matches the requested Domin ID
                    if not valid_domin:
                        print(f"[M1003 DEBUG] RX 311 rejected: no active capture mission for domin_id={did}")
                        # Reject the request - send empty response
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue

                    # RESET HP TO MAX FOR AREA DUEL
                    picked_char['hp'] = get_character_stats(picked_char)['hp_max']
                    # SAVE LATEST POSITION AND MAP EXACTLY (Ensure list copy)
                    latest_pos = picked_char.get('pos', [34611, 100, -49480, 8632])
                    picked_char['pre_arena_pos'] = list(latest_pos)
                    picked_char['pre_arena_map'] = str(picked_char.get('map_id', '11'))
                    print(f"[M1003 DEBUG] Saved pre-arena pos: {picked_char['pre_arena_pos']}, map: {picked_char['pre_arena_map']}")
                    picked_char['active_domin_id'] = valid_domin_id
                    start_map_transition(conn, picked_char, "502", send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 298: # impact_npc (Vehicle collision / running over NPC)
                # The APK sends this request with no payload. It is only a
                # visual collision notification; the authoritative NPC death
                # and IMPACT_NPC mission event arrives separately as MSG 307.
                # Never grant currency or mission progress from this signal.
                print(f"[SECURITY] ignored rewardless MSG 298 player={picked_char.get('id') if picked_char else '-'}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in (107, 276): # enter_copy_scene / enter_single_exp_scene
                copy_id = field_text(body, 0)
                if picked_char and not is_current_character_connection(conn, picked_char):
                    print(f"[EXP STAGE] denied stale entry request player={picked_char.get('id')} msg={msg}")
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    continue
                if msg == 276 and copy_id not in DAILY_EXP_CONFIG and picked_char:
                    # Some APK routes pass the physical SingleMapID rather
                    # than the selected difficulty ID. Resolve it through the
                    # player's configured level band so DailyExpData still
                    # selects the correct spawn/reward profile.
                    player_level = int(picked_char.get('level', 1))
                    matching_copies = [
                        candidate_id for candidate_id, candidate_cfg in COPY_SCENE_CONFIG.items()
                        if candidate_id in DAILY_EXP_CONFIG
                        and str(candidate_cfg.get('single_map_id') or '') == copy_id
                        and int(candidate_cfg.get('min_level', 1)) <= player_level <= int(candidate_cfg.get('max_level', 80))
                    ]
                    if len(matching_copies) == 1:
                        copy_id = matching_copies[0]
                cfg = COPY_SCENE_CONFIG.get(copy_id)
                single_entry_map = str((cfg or {}).get('single_map_id') or '') if msg == 276 else copy_id
                single_entry_valid = msg != 276 or bool(single_entry_map and is_exp_map(single_entry_map))
                entry_team = get_team_for_char(int(picked_char.get('id', 0))) if picked_char else None
                solo_entry_allowed = (entry_team is None or (msg == 276
                    and not entry_team.get('active_run')
                    and not entry_team.get('pending_run')
                    and not entry_team.get('matching')))
                if (picked_char and copy_id in DAILY_EXP_CONFIG and single_entry_valid
                        and solo_entry_allowed):
                    reconcile_exp_stage_login(picked_char)
                    exp_state = picked_char.get('exp_stage_state')
                    active_copy_id = str(picked_char.get('active_copy_id') or '')
                    current_map = str(picked_char.get('map_id') or '11')
                    active_copy_is_current = is_active_daily_copy_current(picked_char)
                    # Map IDs in CopySceneData can identify shared world scenes.
                    # Only the character's explicit, matching daily-copy marker
                    # (or a live EXP run) says they are already inside a copy.
                    if exp_state is not None or is_exp_map(current_map) or active_copy_is_current:
                        state_copy_id = str(exp_state.get('copy_id') or '') if isinstance(exp_state, dict) else ''
                        print(f"[EXP STAGE] denied id={copy_id}; active state copy={state_copy_id or '-'} active_copy={active_copy_id or '-'} map={current_map}")
                    else:
                        if active_copy_id:
                            picked_char['active_copy_id'] = None
                            if (picked_char.get('pre_copy_pos') is not None
                                    and str(picked_char.get('pre_copy_map') or '') != current_map):
                                picked_char['pre_copy_pos'] = None
                                picked_char['pre_copy_map'] = None
                            print(f"[EXP RECOVERY] released stale copy marker copy={active_copy_id} map={current_map} player={picked_char.get('id')}")
                            save_chars(all_accounts_chars)
                        # Fix: Validate player level against exp stage min_level and max_level
                        player_lv = picked_char.get('level', 1)
                        min_lv = (cfg or {}).get('min_level', 1)
                        max_lv = (cfg or {}).get('max_level', 80)
                        if player_lv < min_lv:
                            print(f"[EXP STAGE] denied id={copy_id}; level {player_lv} < min {min_lv}")
                        elif player_lv > max_lv:
                            print(f"[EXP STAGE] denied id={copy_id}; level {player_lv} > max {max_lv}")
                        else:
                            remaining = copy_attempts_remaining(picked_char, copy_id, cfg or {'max_plays': 3})
                            use_time_item = msg == 107 and bool(body.get(1, False))
                            extra_item_used = False
                            if remaining <= 0 and use_time_item:
                                item_id = str((cfg or {}).get('time_inc') or '')
                                inventory = picked_char.get('inventory', [])
                                stack = next((item for item in inventory if str(item.get('id')) == item_id), None) if item_id else None
                                if stack is not None and int(stack.get('amount', 1)) > 0:
                                    stack['amount'] = int(stack.get('amount', 1)) - 1
                                    if stack['amount'] <= 0: inventory.remove(stack)
                                    picked_char['inventory'] = inventory
                                    extra_item_used = True
                            if remaining > 0 or extra_item_used:
                                # Fix: Restore player HP to full before entering exp stage
                                player_stats_entry = get_character_stats(picked_char)
                                picked_char['hp'] = player_stats_entry['hp_max']
                                sync_char_attrs_rpc(conn, picked_char)
                                if picked_char.get('pre_copy_pos') is None:
                                    picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                                    picked_char['pre_copy_map'] = str(picked_char.get('map_id', '11'))
                                state = ensure_daily_copy_state(picked_char)
                                if remaining > 0: state['remaining'][copy_id] = remaining - 1
                                if extra_item_used: send_rpc_push(611, sync_inventory_data(picked_char))
                                picked_char['active_copy_id'] = copy_id
                                picked_char['exp_members'] = [int(picked_char.get('id', 0))]
                                picked_char['exp_team_id'] = None
                                picked_char['exp_defer_start'] = False
                                save_chars(all_accounts_chars)
                                send_rpc_push(555, sync_copy_scenes(picked_char))
                                # Mission 1019 uses Target=105 in MissionData;
                                # report that entry discriminator, not the map ID.
                                advance_missions(picked_char, send_rpc_push, 'dungeon', target_id='105')
                                start_map_transition(conn, picked_char, single_entry_map, send_rpc_push)
                                print(f"[EXP STAGE] entered id={copy_id} map={single_entry_map} remaining={max(0, remaining - 1)} extra_item={extra_item_used}")
                            else:
                                print(f"[EXP STAGE] denied id={copy_id}; daily attempts exhausted")
                elif picked_char and copy_id in DAILY_EXP_CONFIG:
                    if entry_team and (entry_team.get('active_run') or entry_team.get('pending_run') or entry_team.get('matching')):
                        reason = 'team has an active/matching run'
                    elif entry_team:
                        reason = 'player is in a team; use single-entry request for solo play'
                    else:
                        reason = 'missing configured SingleMapID'
                    print(f"[EXP STAGE] denied solo entry id={copy_id}; {reason}")
                elif picked_char and msg == 276:
                    print(f"[EXP STAGE] denied single entry id={copy_id}; no matching DailyExpData copy for level={picked_char.get('level', 1)}")
                elif picked_char and cfg and cfg['subtype'] == 7:
                    remaining = copy_attempts_remaining(picked_char, copy_id, cfg)
                    if remaining > 0:
                        if picked_char.get('pre_copy_pos') is None:
                            picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                            picked_char['pre_copy_map'] = str(picked_char.get('map_id', '11'))
                        state = ensure_daily_copy_state(picked_char)
                        state['remaining'][copy_id] = remaining - 1
                        picked_char['active_copy_id'] = copy_id
                        picked_char['street_race_return_scheduled'] = False
                        picked_char['street_race_result_recorded'] = False
                        picked_char['street_race_started_at'] = time.monotonic()
                        save_chars(all_accounts_chars)
                        start_map_transition(conn, picked_char, cfg['map_id'], send_rpc_push)
                        send_rpc_push(555, sync_copy_scenes(picked_char))
                        advance_missions(picked_char, send_rpc_push, 'dungeon', target_id=copy_id)
                        print(f"[STREET RACE] entered id={copy_id} remaining={remaining - 1}/{cfg['max_plays']}")
                    else:
                        print(f"[STREET RACE] denied id={copy_id}; daily attempts exhausted")
                elif picked_char and msg != 276 and copy_id not in DAILY_EXP_CONFIG:
                    # Save current map and position before entering any copy scene
                    if picked_char.get('pre_copy_pos') is None:
                        picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                        picked_char['pre_copy_map'] = str(picked_char.get('map_id', '11'))
                    picked_char['active_copy_id'] = copy_id
                    advance_missions(picked_char, send_rpc_push, 'dungeon', target_id=copy_id)
                    start_map_transition(conn, picked_char, copy_id, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 193: # car_chase_result
                active_copy_id = str(picked_char.get('active_copy_id') or '') if picked_char else ''
                cfg = COPY_SCENE_CONFIG.get(active_copy_id)
                won = get_val_int(body, 0, 0) == 1
                elapsed = max(0, get_val_int(body, 1, 0))
                path_str = field_text(body, 2)
                race_started_at = float(picked_char.get('street_race_started_at', 0.0)) if picked_char else 0.0
                server_elapsed = max(0.0, time.monotonic() - race_started_at) if race_started_at else 0.0
                race_map_valid = bool(picked_char and cfg and str(picked_char.get('map_id')) == str(cfg.get('map_id')))
                race_time_valid = bool(elapsed > 0 and elapsed <= 3600000
                                       and server_elapsed >= 5.0
                                       and elapsed <= int((server_elapsed + 5.0) * 100))
                if (picked_char and cfg and cfg['subtype'] == 7 and race_map_valid
                        and not picked_char.get('street_race_result_recorded')
                        and race_time_valid):
                    # A race result is a single-use claim for the active run.
                    # The client supplies the finish result, so also require a
                    # real start, the race map, and a plausible elapsed time.
                    picked_char['street_race_result_recorded'] = True
                    state = ensure_daily_copy_state(picked_char)
                    best_times = state.setdefault('best_times', {})
                    best_strs = state.setdefault('best_times_str', {})
                    old_time = best_times.get(active_copy_id)
                    new_record = won and (old_time is None or elapsed < int(old_time))
                    if won:
                        if new_record:
                            best_times[active_copy_id] = elapsed
                            best_strs[active_copy_id] = path_str
                    rewards = street_race_rewards(picked_char.get('level', 1)) if won else []
                    if won and rewards:
                        grant_item_rewards(picked_char, rewards, conn, send_rpc_push)

                    result_items = [encode_sproto([(0, item_id), (1, amount), (3, quality)])
                                    for item_id, quality, amount in rewards]
                    # car_copy_result (TAG 608) is the dedicated APK Street
                    # Race result panel.  It supplies reward icons, time,
                    # rank placeholders, Continue/Exit and Repeat.
                    send_rpc_push(608, encode_sproto([
                        (0, won), (1, result_items), (2, -1), (3, -1),
                        (4, elapsed), (5, 1 if new_record else 0), (6, active_copy_id)
                    ]))
                    if won:
                        # Fix: use 'dungeon' event with target_id='102' to match Mission 1004 (CAR_COPY, logic_type=102)
                        # Previously 'level' only matched logic_type=7 (LEVEL_UP), blocking mission completion
                        advance_missions(picked_char, send_rpc_push, 'dungeon', target_id='102')
                    send_rpc_push(555, sync_copy_scenes(picked_char))
                    schedule_street_race_return()
                    print(f"[STREET RACE] result id={active_copy_id} win={won} time={elapsed} rewards={rewards}")
                elif picked_char and cfg and cfg.get('subtype') == 7:
                    print(f"[STREET RACE SECURITY] rejected result map_ok={race_map_valid} time_ok={race_time_valid} duplicate={bool(picked_char.get('street_race_result_recorded'))}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 191: # request_top_rank_list
                sort_type = get_val_int(body, 0)
                items = []
                if sort_type == 7: # RANK_TYPE.CAR (Street Race)
                    # For street race rank, we can show characters who have records.
                    # We'll collect all characters across all accounts and sort by best time.
                    all_best_times = []
                    for area_dict in all_accounts_chars.values():
                        if isinstance(area_dict, dict):
                            for char_list in area_dict.values():
                                if isinstance(char_list, list):
                                    for c_data in char_list:
                                        if isinstance(c_data, dict):
                                            char_name = c_data.get('name', 'Hero')
                                            c_state = c_data.get('daily_copy_state', {})
                                            b_times = c_state.get('best_times', {})
                                            times = [int(t) for t in b_times.values() if t is not None]
                                            if times:
                                                all_best_times.append((min(times), char_name, c_data))

                    # Sort by time ASCENDING
                    all_best_times.sort(key=lambda x: x[0])
                    for i, (b_time, name, c_data) in enumerate(all_best_times[:50]):
                        items.append(encode_sproto([
                            (0, i + 1),        # id (rank position)
                            (1, b_time),       # score (time)
                            (2, name),         # name
                            (3, c_data.get('prof', 1)), # profession
                            (4, str(sort_type)) # sortType
                        ]))

                send_rpc_push(598, encode_sproto([(0, items), (1, sort_type)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 132: # relife_player (Revive player)
                if picked_char:
                    if not is_current_character_connection(conn, picked_char):
                        print(f"[RELIFE] denied stale character socket player={picked_char.get('id')}")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue
                    # Security: require a live EXP run and a server-recorded death
                    # before accepting the client's revive choice.
                    current_map = str(picked_char.get('map_id', ''))
                    if not is_exp_map(current_map):
                        print(f"[RELIFE] denied for player {picked_char['id']}; not in exp stage map={current_map}")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue
                    exp_state_check = picked_char.get('exp_stage_state')
                    if exp_state_check is None:
                        print(f"[RELIFE] denied for player {picked_char['id']}; no exp_stage_state on map={current_map}")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue
                    player_id = int(picked_char.get('id', 0))
                    if (exp_run_map_id(exp_state_check) != current_map
                            or player_id not in exp_member_ids(exp_state_check)
                            or player_id not in [int(cid) for cid in (exp_state_check.get('dead_members') or [])]
                            or exp_state_check.get('team_finish_handled')):
                        print(f"[RELIFE] denied for player {player_id}; run/member/death state mismatch map={current_map}")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue
                    if (int(picked_char.get('hp', 0)) > 0
                            or exp_state_check.get('_finish_handled')
                            or int(time.time()) >= int(exp_state_check.get('end_time', 0))):
                        print(f"[RELIFE] denied for player {picked_char['id']}; not down or run is terminal")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue
                    # Read isInplace from client request: field 0 = bool
                    try:
                        body_raw = body.get(0, b"")
                        relife_req = decode_sproto(body_raw) if body_raw else {}
                        is_inplace = bool(relife_req.get(0, False))
                    except Exception:
                        is_inplace = False

                    revive_cost = max(1, int(get_relife_config(exp_state_check.get('deaths', 1)).get('use_count', 1)))
                    revive_item_id = '9202'
                    if is_inplace:
                        inventory = picked_char.get('inventory', [])
                        revive_stack = next((item for item in inventory if str(item.get('id')) == revive_item_id), None)
                        if revive_stack is None or int(revive_stack.get('amount', 1)) < revive_cost:
                            if session is not None:
                                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                                conn.sendall(struct.pack('>H', len(pf)) + pf)
                            continue
                        revive_stack['amount'] = int(revive_stack.get('amount', 1)) - revive_cost
                        if revive_stack['amount'] <= 0: inventory.remove(revive_stack)
                        picked_char['inventory'] = inventory

                    p_stats = get_character_stats(picked_char)
                    picked_char['hp'] = p_stats['hp_max']
                    sync_char_attrs_rpc(conn, picked_char)

                    # Determine revive position based on isInplace
                    pos = picked_char.get('pos', [0, 100, 0, 0])
                    if not is_inplace:
                        # Return to city - use birth position of current map or default city
                        mid = str(picked_char.get('map_id', '11'))
                        if mid in MAP_CONFIG and MAP_CONFIG[mid].get('birth'):
                            birth = MAP_CONFIG[mid]['birth'].split('#')
                            if len(birth) >= 3:
                                pos = [int(birth[0]), int(birth[1]) if int(birth[1]) > 0 else 100, int(birth[2]), int(birth[3]) if len(birth) > 3 else 0]

                    picked_char['pos'] = pos

                    print(f'[REVIVE] Consumed {revive_cost} revive item(s) {revive_item_id}')

                    # Build Tag 512 (aoi_relife_player) with correct nested structure
                    # character_relife: id(0), attribute_other(1), movement(2)
                    attr_oth = encode_sproto([
                        (0, p_stats['hp_max']),
                        (1, p_stats['exp']),
                        (2, p_stats['lv']),
                        (3, p_stats['power']),
                        (15, 1)
                    ])
                    mv_bytes = get_movement(pos[0], pos[1], pos[2], pos[3])
                    relife_char = encode_sproto([
                        (0, picked_char['id']),
                        (1, attr_oth),
                        (2, mv_bytes)
                    ])
                    # Build the Tag 512 packet for broadcasting
                    relife_pkt = build_aoi_add_packet(picked_char)  # not used for 512, build manually below
                    relife_frame_data = encode_sproto([(0, relife_char)])

                    # Send Tag 512 to the revived player's own client first
                    send_rpc_push(512, relife_frame_data)

                    # Broadcast Tag 512 to all other players on the same map
                    # so they see the player revive instead of staying dead/ghost
                    map_id = str(picked_char.get('map_id', '11'))
                    revive_state = picked_char.get('exp_stage_state')
                    if revive_state and revive_state.get('team_id'):
                        exp_send_push(revive_state, 512, relife_frame_data, exclude=[picked_char.get('id', 0)])
                        send_team_update(revive_state['team_id'])
                        send_team_member_update(revive_state['team_id'], picked_char, force=True)
                    elif not is_single_player_map(map_id):
                        try:
                            r_ph = encode_sproto([(0, 512)])
                            r_pf = sproto_pack(r_ph + relife_frame_data)
                            r_pkt = struct.pack(">H", len(r_pf)) + r_pf
                            for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
                                if ch and ch.get('id', 0) != picked_char.get('id', 0) and str(ch.get('map_id', '11')) == map_id:
                                    c_lock = CONNECTION_LOCKS.get(cid)
                                    if c_lock:
                                        with c_lock:
                                            c.sendall(r_pkt)
                                    else:
                                        c.sendall(r_pkt)
                                    print(f"[REVIVE] Broadcast Tag 512 to player id={ch.get('id', 0)} map={map_id}")
                        except Exception as e:
                            print(f"[REVIVE] Failed to broadcast Tag 512: {e}")

                    exp_state = picked_char.get('exp_stage_state')
                    if exp_state and is_inplace:
                        dead_members = exp_state.setdefault('dead_members', [])
                        exp_state['dead_members'] = [cid for cid in dead_members if int(cid) != int(picked_char.get('id', 0))]
                        if exp_state.get('team_id'):
                            exp_send_push(exp_state, 629, encode_sproto([(0, exp_state['end_time']), (1, 2)]))
                            _send_exp_tag683(lambda tag, data: exp_send_push(exp_state, tag, data), exp_state, DAILY_EXP_CONFIG.get(exp_state['copy_id'], {}))
                            sync_exp_team_aoi(picked_char)
                        else:
                            for timer_key in ('ai_timer', 'timeout_timer'):
                                run_timer = exp_state.get(timer_key)
                                if run_timer is not None: run_timer.cancel()
                            exp_state['ai_active'] = True
                            exp_state['runtime_started'] = False
                            start_exp_stage_runtime(conn, picked_char, exp_state)
                        print(f"[RELIFE] Player {picked_char['id']} respawned in EXP copy={exp_state['copy_id']}")
                    elif exp_state and not is_inplace:
                        cid = int(picked_char.get('id', 0))
                        if exp_state.get('team_id'):
                            exp_state['members'] = [x for x in exp_state.get('members', []) if int(x) != cid]
                            exp_state.get('char_refs', {}).pop(cid, None)
                            team = TEAM_STATES.get(int(exp_state.get('team_id') or 0))
                            if team:
                                team_remove_member(team, cid)
                                if not exp_state['members']:
                                    exp_state['ai_active'] = False
                                    for timer_key in ('ai_timer', 'timeout_timer'):
                                        run_timer = exp_state.get(timer_key)
                                        if run_timer is not None: run_timer.cancel()
                                    clear_exp_active_monsters(exp_state, lambda tag, data: exp_send_push(exp_state, tag, data))
                                    team['active_run'] = None; team['pending_run'] = None
                                else:
                                    exp_state['owner_id'] = int(team.get('leader_id', exp_state.get('owner_id', 0)))
                                    team['active_run'] = exp_state
                        else:
                            finish_exp_stage(conn, send_rpc_push, picked_char, exp_state, win=False, force_terminal=True)
                            exp_state = None
                    if exp_state and not is_inplace:
                        cid = int(picked_char.get('id', 0))
                        if exp_state.get('team_id'):
                            broadcast_aoi_remove(cid, picked_char.get('map_id', '11'))
                        picked_char.pop('exp_stage_state', None)
                        picked_char['active_copy_id'] = None
                        picked_char['exp_team_id'] = None
                        saved_pos = picked_char.get('pre_copy_pos')
                        saved_map = picked_char.get('pre_copy_map', '11')
                        picked_char['pre_copy_pos'] = None; picked_char['pre_copy_map'] = None
                        if saved_pos: picked_char['pos'] = saved_pos
                        picked_char['map_id'] = saved_map
                        start_map_transition(conn, picked_char, saved_map, send_rpc_push, override_pos=saved_pos)
                        print(f"[RELIFE] Player {picked_char['id']} returned from EXP to map={saved_map}")
                    save_chars(all_accounts_chars)
                    print(f"[REVIVE] Player {picked_char['id']} revived with HP={p_stats['hp_max']} isInplace={is_inplace} pos={pos}")

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 220: # start_battle (Street Race & EXP Stage start)
                if picked_char and picked_char.get('active_copy_id'):
                    copy_id = picked_char['active_copy_id']
                    cfg = COPY_SCENE_CONFIG.get(copy_id)
                    if cfg and cfg['subtype'] == 7:
                        duration = int(cfg.get('exist_time') or 900)
                        send_rpc_push(629, encode_sproto([(0, int(time.time()) + duration), (1, 0)]))
                        print(f"[STREET RACE] started timer for id={copy_id} duration={duration}s")
                # Security: Validate player is actually on the exp stage map before sending battle updates
                if picked_char and picked_char.get('exp_stage_state'):
                    exp_state = picked_char['exp_stage_state']
                    current_map = str(picked_char.get('map_id', ''))
                    expected_map = exp_run_map_id(exp_state)
                    if current_map != expected_map:
                        print(f"[EXP STAGE] start_battle denied; map mismatch map={current_map} expected={expected_map}")
                    else:
                        # Issue 57: Change type from 0 to 2 so CountDownTimeLogic auto-closes at 00:00:00
                        exp_send_push(exp_state, 629, encode_sproto([(0, exp_state['end_time']), (1, 2)]))
                        # Send TAG 683 with raw kill counts for client UI
                        exp_cfg_for_battle = DAILY_EXP_CONFIG.get(exp_state['copy_id'], {})
                        _send_exp_tag683(lambda tag, data: exp_send_push(exp_state, tag, data), exp_state, exp_cfg_for_battle)
                        print(f"[EXP STAGE] start_battle sent 629 & 683 updates for copy={exp_state['copy_id']}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [106, 246, 273, 207, 201, 322]:
                # Scene/Dungeon Entry
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[RX] Scene Entry: {mid} (MSG={msg})")
                if picked_char:
                    if is_current_character_connection(conn, picked_char):
                        reconcile_exp_stage_login(picked_char)
                    current_map = str(picked_char.get('map_id', '11'))
                    boss_cfg = WILD_BOSS_CONFIG.get(mid, {}) if msg == 201 else {}
                    valid_world_boss = (msg != 201 or (
                        boss_cfg.get('type') == 5
                        and str(boss_cfg.get('map_id')) == mid
                        and int(boss_cfg.get('min_level', 1)) <= int(picked_char.get('level', 1)) <= int(boss_cfg.get('max_level', 80))
                    ))
                    if not is_current_character_connection(conn, picked_char):
                        print(f"[SCENE ENTRY] denied stale character socket player={picked_char.get('id')}")
                    elif not valid_world_boss:
                        print(f"[WORLD BOSS SECURITY] rejected unconfigured/ineligible id={mid} player={picked_char.get('id')}")
                    elif (is_exp_map(mid) or is_exp_map(current_map)
                            or picked_char.get('exp_stage_state')):
                        # EXP maps can only be entered through the validated
                        # daily/solo or ready team flows, and an active run can
                        # only be exited through its explicit leave/revive flow.
                        print(f"[EXP STAGE] denied generic scene entry player={picked_char.get('id')} target={mid} current={current_map}")
                    else:
                        # Save current map and position before entering any scene/dungeon
                        if picked_char.get('pre_copy_pos') is None:
                            picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                            picked_char['pre_copy_map'] = str(picked_char.get('map_id', '11'))
                        picked_char['active_copy_id'] = mid
                        start_map_transition(conn, picked_char, mid, send_rpc_push)
                        if msg == 201: # world_boss
                            advance_missions(picked_char, send_rpc_push, 'world_boss')
                            send_rpc_push(552, encode_sproto([(0, 1), (1, mid), (2, True)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # update_game_server
                # Tag 2 in response is the game_server list.
                servers = []
                if SERVER_DATA_LIST:
                    for s in SERVER_DATA_LIST:
                        # Pack each server definition
                        s_data = encode_sproto([
                            (0, s['id']), (1, s['name']), (2, s['ip']), (3, s['port']),
                            (4, s['state']), (5, s['player_state']), (6, s['area']),
                            (7, s['db_local']), (8, s['timezone']), (9, s['new_char']),
                            (10, s['new_server'])
                        ])
                        servers.append(s_data)
                else:
                    # Fallback
                    server = encode_sproto([
                        (0, 302), (1, "EU-001"), (2, "s16.serv00.com"), (3, 15678),
                        (4, 1), (5, -4), (6, 1), (7, 1), (8, 1), (9, 1), (10, 0)
                    ])
                    servers.append(server)
                resp = encode_sproto([(2, servers)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 270: # download_finish
                if picked_char:
                    with get_character_transaction_lock(picked_char):
                        if not picked_char.get('download_complete'):
                            picked_char['download_complete'] = True
                            # Expansion Rewards from DownloadRewardData
                            if DOWNLOAD_REWARD_DATA:
                                for item_id, count, quality in DOWNLOAD_REWARD_DATA:
                                    add_to_inventory(picked_char, item_id, count)
                            else:
                                # Fallback - only items that exist in ItemData
                                add_to_inventory(picked_char, "5011", 1)   # EXP*100000
                                add_to_inventory(picked_char, "5012", 1)   # EXP*1000000
                                add_to_inventory(picked_char, "5026", 5)   # World Channel Speak
                            save_chars(all_accounts_chars)

                            # Sync items and finalize client state
                            send_rpc_push(611, sync_inventory_data(picked_char))
                            print(f"[REWARD] Expansion finalized and rewards granted for player {picked_char['id']}")
                        else:
                            print(f"[REWARD] Player {picked_char['id']} already claimed expansion rewards.")
                        # Keep the current client session consistent with the
                        # persisted completion state if MSG 270 repeats.
                        send_rpc_push(654, encode_sproto([(0, 1)])) # start_enter_game state=1

                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 306: # tutorial_finish
                # The APK sends this before scheduling its optional-download tip.
                # It is an RPC request, so it must receive an empty success reply.
                if picked_char:
                    picked_char['tutorial'] = 1
                    save_chars(all_accounts_chars)
                print("[TUTORIAL] tutorial_finish acknowledged")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 268: # unlock_function_complete - client reports tutorial/function completion
                # Schema: ID(0) string, state(1) long
                func_id = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                func_state = get_val_int(body, 1)
                if picked_char and func_id:
                    if 'completed_tutorials' not in picked_char:
                        picked_char['completed_tutorials'] = []
                    if func_id not in picked_char['completed_tutorials']:
                        picked_char['completed_tutorials'].append(func_id)
                        save_chars(all_accounts_chars)
                    print(f"[TUTORIAL COMPLETE] func_id={func_id} state={func_state}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            # Keep the legacy chat-shaped 110 payload out of the team invite
            # reply path. The APK's real chat RPC is 120; ret_invite_join_team
            # (110) carries integer fields ok(0), id(1) and is handled below.
            elif msg == 110 and isinstance(body.get(1), (bytes, str)): # legacy chat-shaped payload
                if picked_char:
                    chat_type = get_val_int(body, 0)
                    chat_msg = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1, ''))
                    # TAG 528: ret_chat - echo chat message back to client
                    # chat_item schema: senderId(0), senderName(1), tellId(2), tellName(3),
                    #   chatInfo(4), chattype(5), linktype(6), intdata(7), stringdata(8),
                    #   senderProfession(9), level(10), combValue(11), guildId(12), guildName(13), chatInfo2(14)
                    chat_item = encode_sproto([
                        (0, picked_char['id']),     # senderId
                        (1, picked_char['name']),   # senderName
                        (2, 0),                     # tellId (0 = global chat)
                        (3, ""),                    # tellName
                        (4, chat_msg),              # chatInfo (the message text)
                        (5, chat_type),             # chattype
                        (6, 0),                     # linktype
                        (10, picked_char.get('level', 1))  # level
                    ])
                    send_rpc_push(528, encode_sproto([(0, [chat_item])]))
                    print(f"[CHAT] type={chat_type} from={picked_char['name']} msg={chat_msg[:50]}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in (109, 110, 161, 162, 163, 164, 166, 213, 214, 215, 216, 217):
                cid=int(picked_char.get('id',0)) if picked_char else 0
                if picked_char and not is_current_character_connection(conn, picked_char):
                    print(f"[TEAM] denied stale character socket player={cid} msg={msg}")
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    continue
                if picked_char and msg==109:
                    target=get_val_int(body,0,-1)
                    if target==-1:
                        goal=get_val_str(body,1,''); team=get_team_for_char(cid); ok=False
                        if team is None and is_character_available_for_team(picked_char):
                            goal_min,goal_max=team_goal_level_bounds(goal,get_val_int(body,2,1),get_val_int(body,3,80))
                            team_id=next_team_id()
                            team={'id':team_id,'leader_id':cid,'members':[],'goal_id':goal,
                                'min_level':goal_min,'max_level':goal_max,
                                'verify':get_val_int(body,4,0),'recruit':get_val_int(body,5,0),'char_refs':{},'pending_apps':{}}
                            if team_join_member(team,picked_char): TEAM_STATES[team['id']]=team; ok=True
                        elif team and team.get('leader_id')==cid and not team.get('active_run'):
                            team['goal_id']=goal
                            team['min_level'],team['max_level']=team_goal_level_bounds(
                                goal,get_val_int(body,2,team.get('min_level',1)),get_val_int(body,3,team.get('max_level',80)))
                            team['verify']=get_val_int(body,4,team.get('verify',0))
                            team['recruit']=get_val_int(body,5,team.get('recruit',0))
                            ok=True
                        if ok:
                            picked_char['team_goal_copy']=goal
                            send_team_update(team['id'])
                        send_rpc_push(517,encode_sproto([(0,1 if ok else 0),(1,cid)]))
                    else:
                        team=get_team_for_char(cid); target_conn,target_char=ALL_CONNECTIONS.get(target,(None,None))
                        ok=bool(team and team.get('leader_id')==cid and not team.get('active_run') and target_conn and target_char and target not in team['members'] and len(team['members'])<4 and get_team_for_char(target) is None and is_character_available_for_team(target_char))
                        if ok:
                            sent=send_push_to_char(target,516,encode_sproto([(0,int(team['id'])),(1,team_member_blob(picked_char,team['id'])),(2,str(team.get('goal_id','')))]))
                            ok=bool(sent)
                            if ok:
                                TEAM_INVITES[target]={'team_id':team['id'],'inviter_id':cid,'expires_at':int(time.time())+60}
                        send_rpc_push(517,encode_sproto([(0,1 if ok else 0),(1,target)]))
                elif picked_char and msg==110:
                    invite=TEAM_INVITES.get(cid); ok=get_val_int(body,0,0)==1
                    reply_id=get_val_int(body,1,0)
                    valid=bool(invite and invite.get('expires_at',0)>=int(time.time()) and reply_id==cid)
                    inviter=int(invite.get('inviter_id',0)) if invite else 0
                    if inviter:
                        send_push_to_char(inviter,517,encode_sproto([(0,1 if ok and valid else 0),(1,cid)]))
                    if not ok or not valid: TEAM_INVITES.pop(cid,None)
                elif picked_char and msg==161:
                    tid=get_val_int(body,0); team=TEAM_STATES.get(tid); inv=TEAM_INVITES.get(cid,{})
                    is_invite_accept=get_val_int(body,1,0)==1
                    invited=bool(is_invite_accept and inv.get('team_id')==tid
                        and inv.get('expires_at',0)>=int(time.time())
                        and team and inv.get('inviter_id')==int(team.get('leader_id',0)))
                    if team and not team.get('active_run') and is_invite_accept:
                        if invited and team_join_member(team,picked_char):
                            TEAM_INVITES.pop(cid,None)
                            send_team_update(tid)
                            print(f"[TEAM] invite accepted team={tid} member={cid}")
                        else:
                            if inv and inv.get('expires_at',0)<int(time.time()): TEAM_INVITES.pop(cid,None)
                            print(f"[TEAM] invite join rejected team={tid} member={cid} valid={invited}")
                    elif team and not team.get('active_run') and not is_invite_accept:
                        if team.get('verify') and cid not in team['members']:
                            team.setdefault('pending_apps',{})[cid]=picked_char
                            sent=send_push_to_char(team['leader_id'],572,encode_sproto([(0,team_member_blob(picked_char,tid))]))
                            print(f"[TEAM] join application team={tid} member={cid} leader_notified={sent}")
                        elif not team.get('verify') and team_join_member(team,picked_char):
                            send_team_update(tid)
                            print(f"[TEAM] open join accepted team={tid} member={cid}")
                elif picked_char and msg==162:
                    applicant=get_val_int(body,0); agree=get_val_int(body,1,0)==1
                    team=get_team_for_char(cid)
                    if team and team.get('leader_id')==cid:
                        ch=team.get('pending_apps',{}).pop(applicant,None)
                        if ch is not None:
                            joined=bool(agree and team_join_member(team,ch))
                            send_push_to_char(applicant,576,encode_sproto([(0,int(team['id'])),(1,1 if joined else 0)]))
                            if joined: send_team_update(team['id'])
                elif picked_char and msg==163:
                    team=get_team_for_char(cid)
                    if team and team.get('leader_id')==cid and not team.get('active_run') and not team.get('pending_run'):
                        team['verify']=get_val_int(body,0,team.get('verify',0));send_team_update(team['id'])
                elif picked_char and msg==213:
                    team=get_team_for_char(cid)
                    if team and team.get('leader_id')==cid and not team.get('active_run') and not team.get('pending_run'):
                        goal=get_val_str(body,0,team.get('goal_id',''))
                        if team.get('matching'): stop_team_match(team,team.get('goal_id',''),1)
                        team['goal_id']=goal
                        team['min_level'],team['max_level']=team_goal_level_bounds(
                            goal,get_val_int(body,1,team.get('min_level',1)),get_val_int(body,2,team.get('max_level',80)))
                        team['verify']=get_val_int(body,3,team.get('verify',0))
                        team['recruit']=get_val_int(body,4,team.get('recruit',team.get('recruit',0)))
                        for member_id in team.get('members',[]):
                            member=ALL_CONNECTIONS.get(int(member_id),(None,team.get('char_refs',{}).get(int(member_id))))[1]
                            if member: member['team_goal_copy']=goal
                        send_team_update(team['id'])
                elif picked_char and msg==164:
                    team=get_team_for_char(cid); target=get_val_int(body,0)
                    if team and team.get('leader_id')==cid and target!=cid:
                        if target in team.get('members',[]):
                            detach_exp_team_member(team,target)
                            send_push_to_char(target,518,encode_sproto([]))
                elif picked_char and msg==166:
                    team=get_team_for_char(cid)
                    if team:
                        detach_exp_team_member(team,cid)
                        send_rpc_push(518,encode_sproto([]))
                elif picked_char and msg==214:
                    goal=get_val_str(body,0,''); goal_type=get_val_int(body,1,1)
                    if goal in DAILY_EXP_CONFIG:
                        team=get_team_for_char(cid)
                        if team is None and is_character_available_for_team(picked_char):
                            cfg=COPY_SCENE_CONFIG.get(goal,{})
                            team_id=next_team_id()
                            team={'id':team_id,'leader_id':cid,'members':[],'goal_id':goal,
                                'min_level':int(cfg.get('min_level',1)),'max_level':int(cfg.get('max_level',80)),
                                'verify':0,'recruit':0,'char_refs':{},'pending_apps':{}}
                            if team_join_member(team,picked_char): TEAM_STATES[team['id']]=team
                            else: team=None
                        if (team and team.get('leader_id')==cid and not team.get('active_run')
                                and not team.get('pending_run')
                                and all(is_character_available_for_team(ALL_CONNECTIONS.get(int(mid),(None,None))[1]) for mid in team.get('members',[]))):
                            team['goal_id']=goal
                            team['min_level'],team['max_level']=team_goal_level_bounds(
                                goal,team.get('min_level',1),team.get('max_level',80))
                            picked_char['team_goal_copy']=goal
                            for member_id in team.get('members',[]):
                                member=ALL_CONNECTIONS.get(int(member_id),(None,team.get('char_refs',{}).get(int(member_id))))[1]
                                if member: member['team_goal_copy']=goal
                            queue_team_match(team,goal,goal_type); send_team_update(team['id'])
                elif picked_char and msg==216:
                    goal=get_val_str(body,0,''); goal_type=get_val_int(body,1,1)
                    team=get_team_for_char(cid)
                    if team and team.get('leader_id')==cid and str(team.get('goal_id',''))==goal:
                        stop_team_match(team,goal,goal_type)
                elif picked_char and msg==215:
                    team=get_team_for_char(cid); goal=get_val_str(body,0,''); copy_id=goal if goal in DAILY_EXP_CONFIG else ''
                    cfg=COPY_SCENE_CONFIG.get(copy_id)
                    if (team and team.get('leader_id')==cid and str(team.get('goal_id',''))==copy_id
                            and get_val_int(body,1,1)==1 and cfg and not team.get('active_run') and not team.get('pending_run')):
                        ids=list(team['members']); minm=max(2,int(cfg.get('min_member',2)))
                        valid=minm<=len(ids)<=int(cfg.get('max_member',4)) and all(ALL_CONNECTIONS.get(x,(None,None))[0] is not None and ALL_CONNECTIONS.get(x,(None,None))[1] is not None for x in ids)
                        if valid:
                            valid=all(is_character_available_for_team(ALL_CONNECTIONS[x][1]) and cfg['min_level']<=int(ALL_CONNECTIONS[x][1].get('level',1))<=cfg['max_level'] for x in ids)
                        if valid:
                            # Every participant spends their own daily attempt;
                            # do not let a ready squad carry exhausted members
                            # into a run for free.
                            valid=all(copy_attempts_remaining(ALL_CONNECTIONS[x][1],copy_id,cfg)>0 for x in ids)
                        if valid:
                            session_id=next_exp_run_id()
                            team['pending_run']={'session':session_id,'copy_id':copy_id,'ready':{x:False for x in ids}}
                            for x in ids: send_push_to_char(x,602,encode_sproto([(0,copy_id),(1,1),(2,session_id)]))
                            def expire_ready(tid=int(team['id']), sid=session_id):
                                current=TEAM_STATES.get(tid); waiting=current.get('pending_run') if current else None
                                if waiting and waiting.get('session')==sid:
                                    for member_id in current.get('members',[]): send_push_to_char(member_id,603,encode_sproto([(0,0),(1,1)]))
                                    current['pending_run']=None; current.pop('_ready_timer',None)
                            ready_timer=threading.Timer(35.0,expire_ready); ready_timer.daemon=True
                            team['_ready_timer']=ready_timer; ready_timer.start()
                        else:
                            for member_id in ids: send_push_to_char(member_id,603,encode_sproto([(0,int(cid)),(1,1)]))
                elif picked_char and msg==217:
                    team=get_team_for_char(cid); pending=team.get('pending_run') if team else None
                    if (pending and not pending.get('starting')
                            and get_val_int(body,0)==pending['session'] and cid in pending['ready']):
                        state_value=get_val_int(body,1,1)
                        if state_value==0:
                            pending['ready'][cid]=True
                            for x in team['members']: send_push_to_char(x,603,encode_sproto([(0,cid),(1,0)]))
                            if all(pending['ready'].values()):
                                # Duplicate ready replies can arrive through
                                # overlapping reconnect sockets. Claim this
                                # exact pending session only once.
                                run_lock=team.setdefault('_exp_run_lock',threading.Lock())
                                with run_lock:
                                    current_pending=team.get('pending_run')
                                    start_this_run=(current_pending is pending and not pending.get('starting')
                                        and all(pending['ready'].values()))
                                    if start_this_run: pending['starting']=True
                                if start_this_run:
                                    if not start_team_exp_run(team,pending['copy_id']):
                                        for x in list(team.get('members',[])): send_push_to_char(x,603,encode_sproto([(0,int(cid)),(1,1)]))
                                        if team.get('pending_run') is pending: team['pending_run']=None
                                    ready_timer=team.pop('_ready_timer',None)
                                    if ready_timer: ready_timer.cancel()
                        else:
                            for x in team['members']: send_push_to_char(x,603,encode_sproto([(0,cid),(1,1)]))
                            team['pending_run']=None
                            ready_timer=team.pop('_ready_timer',None)
                            if ready_timer: ready_timer.cancel()
                if session is not None:
                    ph=encode_sproto([(1,session)]);pf=sproto_pack(ph+encode_sproto([]));conn.sendall(struct.pack('>H',len(pf))+pf)

            elif msg in [118, 218, 145, 202, 210, 225, 242, 252, 253, 254, 255, 257, 258, 261, 266, 278, 296, 299, 313, 319]:
                resp_data = encode_sproto([])
                if msg == 118:
                    # Check if this is a random name request (body has field 0 = type)
                    # request_random_name.request: field 0 = type (0=male, 1=female)
                    # After character_pick, the client is on the creation screen and sends
                    # msg 118 to get a random name. Before that (in-game), it's activity info.
                    req_type = body.get(0, -1)
                    if isinstance(req_type, int) and req_type in [0, 1]:
                        # Random name request from character creation screen
                        conn_id = id(conn)
                        if conn_id not in RANDOM_NAME_CALLED:
                            # First call is auto-triggered by profession switch on screen load
                            # Return empty name so the name field stays empty
                            RANDOM_NAME_CALLED.add(conn_id)
                            resp_data = encode_sproto([(0, "")])
                        else:
                            # Subsequent calls are from the random name button click
                            names = [
                                "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Riley", "Cameron", "Quinn", "Avery", "Reese",
                                "Blake", "Dakota", "Drew", "Emery", "Finley", "Harper", "Jamie", "Kendall", "Logan", "Peyton",
                                "Sage", "Skyler", "Toby", "Val", "Ari", "Charlie", "Dana", "Ellis", "Frankie", "Gray",
                                "Indigo", "Jules", "Kit", "Lane", "Marlow", "Nico", "Oakley", "Parker", "River", "Shiloh",
                                "Summer", "Topher", "Winter", "Zion", "Adam", "Brett", "Caleb", "Dylan", "Ethan", "Finn",
                                "Gavin", "Hunter", "Isaac", "Jake", "Kyle", "Liam", "Nathan", "Owen", "Patrick", "Quentin",
                                "Robert", "Sean", "Tyler", "Ulrich", "Vincent", "Wyatt", "Xander", "Yusuf", "Zach", "Aaron",
                                "Brandon", "Chase", "Devin", "Evan", "Gabriel", "Harrison", "Ian", "Jeremiah", "Kai", "Leo",
                                "Mason", "Noah", "Oliver", "Peter", "Quincy", "Rowan", "Samuel", "Theodore", "Ulysses", "Vance",
                                "Will", "Xavier", "Yael", "Zane", "Abigail", "Bella", "Clara", "Daisy", "Eliza", "Faith",
                                "Grace", "Hannah", "Isla", "Julia", "Katie", "Lily", "Monica", "Nora", "Opal", "Paige",
                                "Rachel", "Stella", "Tessa", "Uma", "Vera", "Willa", "Xena", "Yara", "Zara", "Aria",
                                "Brooke", "Cindy", "Diana", "Ella", "Fiona", "Gemma", "Holly", "Ivy", "Jade", "Kira",
                                "Luna", "Mia", "Nina", "Olivia", "Penny", "Ruby", "Sophie", "Tara", "Violet", "Wendy",
                                "Xia", "Yuki", "Zoe", "Alice", "Beth", "Cora", "Daphne", "Elena", "Freya", "Gwen",
                                "Hazel", "Iris", "Jasmine", "Karen", "Lena", "Maya", "Nell", "Olive", "Piper", "Rose",
                                "Sara", "Tina", "Vivian", "Wren", "Zelda", "Amy", "Blair", "Cleo", "Eve", "Faye",
                                "Gina", "Hope", "Iona", "Jill", "Kara", "Lola", "Mona", "Nia", "Ora", "Pam", "Rita",
                                "Sue", "Tess", "Willa", "Yvonne",
                            ]
                            name = f"{random.choice(names)}_{random.randint(100, 999)}"
                            resp_data = encode_sproto([(0, name)])
                    else:
                        # Activity info request (in-game)
                        activity_map = {}
                        resp_data = encode_sproto([(0, activity_map)])
                elif msg == 218: resp_data = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp_data)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

                if msg == 145 and picked_char:
                    send_rpc_push(555, sync_copy_scenes(picked_char))
                    print(f"[COPY] sent daily copy state level={picked_char.get('level', 1)}")

                elif msg == 253 and picked_char: # request_sign_week_info
                    sign_calendar = current_sign_calendar()
                    day = int(sign_calendar['week_day'])
                    claimed = picked_char.get('sign_week_claimed_date') == sign_calendar['date']
                    send_rpc_push(641, encode_sproto([(0, day), (1, not claimed), (2, False)]))

                elif msg == 255 and picked_char: # sign_week
                    with get_character_transaction_lock(picked_char):
                        sign_calendar = current_sign_calendar()
                        day = int(sign_calendar['week_day'])
                        if picked_char.get('sign_week_claimed_date') != sign_calendar['date']:
                            picked_char['sign_week_claimed_date'] = sign_calendar['date']
                            picked_char['sign_week_day'] = day
                            picked_char['sign_week_claimed'] = True
                            picked_char['cash'] = picked_char.get('cash', 0) + 10000
                            save_chars(all_accounts_chars)
                            send_rpc_push(643, encode_sproto([(0, day), (1, False)]))
                            send_rpc_push(611, sync_inventory_data(picked_char))
                            sync_char_attrs_rpc(conn, picked_char)
                        else:
                            print(f"[REWARD SECURITY] duplicate weekly sign rejected player={picked_char.get('id')}")

                elif msg == 252 and picked_char: # request_sign_30_day_info
                    sign_calendar = current_sign_calendar()
                    cur_day = min(30, int(sign_calendar['month_day']))
                    claimed = (picked_char.get('sign_30_claimed_date') == sign_calendar['date']
                               or int(sign_calendar['month_day']) > 30)
                    send_rpc_push(640, encode_sproto([(0, cur_day), (1, cur_day), (2, 0), (3, not claimed), (4, False), (5, 30), (6, "")]))

                elif msg == 254 and picked_char: # sign_30_day
                    with get_character_transaction_lock(picked_char):
                        sign_calendar = current_sign_calendar()
                        cur_day = min(30, int(sign_calendar['month_day']))
                        already_claimed = (picked_char.get('sign_30_claimed_date') == sign_calendar['date']
                                           or int(sign_calendar['month_day']) > 30)
                        if not already_claimed:
                            picked_char['sign_30_day'] = cur_day
                            picked_char['sign_30_claimed_date'] = sign_calendar['date']
                            picked_char['sign_30_claimed'] = True
                            picked_char['cash'] = picked_char.get('cash', 0) + 20000
                            save_chars(all_accounts_chars)
                            send_rpc_push(642, encode_sproto([(0, cur_day), (1, cur_day), (2, 0), (3, False), (4, False), (5, 30), (6, "")]))
                            send_rpc_push(611, sync_inventory_data(picked_char))
                            sync_char_attrs_rpc(conn, picked_char)
                        else:
                            print(f"[REWARD SECURITY] duplicate 30-day sign rejected player={picked_char.get('id')}")

                elif msg == 258 and picked_char: # request_daily_buy
                    d_buys = {
                        "1": encode_sproto([(0, "1"), (1, 0)]),
                        "2": encode_sproto([(0, "2"), (1, 0)]),
                        "3": encode_sproto([(0, "3"), (1, 0)]),
                    }
                    send_rpc_push(646, encode_sproto([(0, d_buys)]))

                elif msg == 261 and picked_char: # request_daily_active
                    # ret_request_daily_active expects:
                    # tag 0: Dictionary<string, daily_active> where daily_active has ID(string), count(long), Type(long)
                    # tag 1: Dictionary<string, daily_reward> where daily_reward has ID(string), state(long)
                    # tag 2: score(long)
                    d_acts = {}
                    d_rews = {}
                    send_rpc_push(649, encode_sproto([(0, d_acts), (1, d_rews), (2, 0)]))

                elif msg == 225 and picked_char: # request_activity_info
                    now_ts = int(time.time())
                    acts = {
                        "1": encode_sproto([(0, 1), (1, now_ts - 3600), (2, now_ts + 86400 * 30), (3, 0), (4, 86400), (5, 1)])
                    }
                    send_rpc_push(619, encode_sproto([(0, acts)]))

                elif msg == 202 and picked_char: # request_tower_copy_info
                    t_info = encode_sproto([(0, 0), (1, 100), (2, 1), (3, 1), (4, 0), (5, 0)])
                    send_rpc_push(606, encode_sproto([(0, t_info), (1, [])]))

                elif msg == 242 and picked_char: # request_slot_info
                    # ret_slot_info expects:
                    # tag 0: slot_info object with curNum(long) at tag 1, sumNum(long) at tag 2
                    # tag 1: Dictionary<string, slot_data>
                    # tag 2: Dictionary<string, slot_item>
                    slot_info_obj = encode_sproto([(1, 0), (2, 0)])
                    send_rpc_push(633, encode_sproto([(0, slot_info_obj), (1, {}), (2, {})]))

                elif msg == 257 and picked_char: # request_invest_pack
                    send_rpc_push(645, encode_sproto([(0, {})]))

                elif msg == 296 and picked_char: # req_level_reward
                    send_rpc_push(674, encode_sproto([(0, {})]))

            elif msg == 310:  # request_domin_info
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

                # DominData row 1: matchpower=6000, LevelMin=1, MapID=502
                # The character_look (ao_p) fields are read by CitySimController
                # to display the VS panel before entering the arena.
                # Using DominData values: DominantMin=1200, DominantMax=2600, Lossspeed=5
                v_p = encode_sproto([
                    (0, "Ash Viper"),
                    (1, "100"),
                    (2, "XD_A_T"),
                    (3, "XD_A_S"),
                    (4, "XD_A_X"),
                    (5, "XD_A_WQ"),
                    (10, 0)
                ])

                g_p = encode_sproto([
                    (0, "Ash Viper"),
                    (1, 0)
                ])

                ao_p = encode_sproto([
                    (2, 1),
                    (3, 6000),
                    (4, 1),
                    (15, 5)
                ])

                cl_p = encode_sproto([
                    (0, 0),
                    (1, g_p),
                    (3, ao_p),
                    (4, v_p)
                ])

                di_p = encode_sproto([
                    (0, "1"),
                    (8, 0),
                    (9, 0)
                ])

                resp_p = encode_sproto([
                    (0, [di_p]),
                    (1, [cl_p])
                ])

                send_rpc_push(684, resp_p)

            elif msg == 178: # update_misison_parm
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                param_type = get_val_int(body, 1)
                param_value = get_val_int(body, 2)
                if picked_char:
                    mdata = picked_char.get('active_missions', {}).get(mid)
                    m_cfg = missions_data.get(mid, {})
                    logic_type = m_cfg.get('logic_type')
                    logic_id = str(m_cfg.get('logic_id', ''))
                    accepted = False
                    required = 0
                    if mdata and mdata.get('state') == 1 and param_type == 1 and param_value == 1:
                        if logic_type == 6:
                            # SurveyMissionData is authoritative for both the
                            # target map and world position. The client can only
                            # submit a single unit while standing at that item.
                            survey = SURVEY_DATA.get(logic_id)
                            pos = picked_char.get('pos') or []
                            if survey and str(survey.get('scene_id')) == str(picked_char.get('map_id')) and len(pos) >= 3:
                                sx = int(round(float(survey.get('x', 0.0)) * 100))
                                sz = int(round(float(survey.get('z', 0.0)) * 100))
                                radius = 400 if str(survey.get('scene_id')) == '11' else 250
                                near_item = math.hypot(int(pos[0]) - sx, int(pos[2]) - sz) <= radius
                                now_mono = time.monotonic()
                                cooldown_key = (int(picked_char.get('id', 0)), mid)
                                last_event = SURVEY_PROGRESS_TIMES.get(cooldown_key, 0.0)
                                cooldown = max(0.8, int(survey.get('survey_time', 1000)) / 1000.0)
                                if near_item and now_mono - last_event >= cooldown:
                                    required = int(survey.get('need_num', m_cfg.get('require_num', 1)))
                                    accepted = True
                                    SURVEY_PROGRESS_TIMES[cooldown_key] = now_mono
                        elif logic_type == 13:
                            # Multi-delivery only advances at the configured
                            # stop for the current progress index.
                            stops = MULTI_DELIVERY_DATA.get(logic_id, [])
                            progress = int((mdata.get('parm') or [0])[0])
                            if progress < max(0, len(stops) - 1) and str(stops[progress].get('map_id')) == str(picked_char.get('map_id')):
                                required = len(stops)
                                accepted = required > 0
                                if accepted:
                                    now_mono = time.monotonic()
                                    cooldown_key = (int(picked_char.get('id', 0)), mid)
                                    last_event = SURVEY_PROGRESS_TIMES.get(cooldown_key, 0.0)
                                    accepted = now_mono - last_event >= 1.0
                                    if accepted:
                                        SURVEY_PROGRESS_TIMES[cooldown_key] = now_mono

                    if accepted:
                        parm = mdata.setdefault('parm', [0] * 8)
                        if len(parm) < 8:
                            parm.extend([0] * (8 - len(parm)))
                        parm[0] = min(required, int(parm[0]) + 1)
                        send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, parm[0])]))
                        if parm[0] >= required:
                            mdata['state'] = 2
                            send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                        save_chars(all_accounts_chars)
                        send_rpc_push(519, sync_mission_data(picked_char))
                        print(f"[MISSION PROGRESS] msg=178 mission={mid} progress={parm[0]}/{required}")
                    else:
                        print(f"[MISSION SECURITY] rejected MSG 178 mission={mid} type={logic_type} map={picked_char.get('map_id')}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in (179, 182): # update_misison_complete (APK protocol ID 182)
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                if picked_char:
                    mdata = picked_char.get('active_missions', {}).get(mid)
                    m_cfg = missions_data.get(mid, {})
                    logic_type = m_cfg.get('logic_type')
                    can_finish_dialog = bool(mdata and mdata.get('state') == 1
                                              and logic_type == 0 and m_cfg.get('class') != 6)
                    if mdata and mdata.get('state') == 1 and logic_type == 13:
                        stops = MULTI_DELIVERY_DATA.get(str(m_cfg.get('logic_id', '')), [])
                        can_finish_dialog = bool(stops and int((mdata.get('parm') or [0])[0]) >= len(stops) - 1)
                    # Story dialogues and completed multi-delivery routes are
                    # the only client-confirmed transitions. Kill, EXP 1019,
                    # capture and other objectives are advanced by the server.
                    if can_finish_dialog:
                        mdata['state'] = 2
                        save_chars(all_accounts_chars)
                        send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                        send_rpc_push(519, sync_mission_data(picked_char))
                        print(f"[MISSION DIALOG COMPLETE] mission_id={mid}")
                    elif mdata and mdata.get('state') != 2:
                        print(f"[MISSION SECURITY] rejected dialog completion mission={mid} type={logic_type} state={mdata.get('state')}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 119: # ask_pickup_item
                server_drop_id = get_val_int(body, 0, -1)
                # APK schema confirms field 0 is a server drop-instance ID.
                # This server's drop objects are client-side reward visuals and
                # already have their rewards issued by the validated kill path;
                # treating that instance ID as an item ID allowed inventory minting.
                print(f"[SECURITY] ignored unregistered pickup request player={picked_char.get('id') if picked_char else '-'} drop={server_drop_id}")
                if session is not None:
                    resp = encode_sproto([(0, 0)])
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 108: # leave_copy_scene
                if picked_char:
                    # Security: Validate player is actually in a copy scene before processing leave
                    in_copy = picked_char.get('active_copy_id') is not None
                    in_exp_stage = picked_char.get('exp_stage_state') is not None
                    has_leave_timer = '_exp_leave_timer' in picked_char
                    if (in_exp_stage or has_leave_timer) and not is_current_character_connection(conn, picked_char):
                        print(f"[LEAVE COPY] denied stale character socket player={picked_char.get('id')}")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue
                    # Fix: After exp stage win, state is already cleaned up but client still sends leave_copy_scene
                    # from the StarRewardPageRoot. Accept it gracefully — just send an ACK.
                    if not in_copy and not in_exp_stage and not has_leave_timer:
                        print(f"[LEAVE COPY] denied for player {picked_char['id']}; not in a copy scene")
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        continue

                    if has_leave_timer:
                        leave_timer = picked_char.pop('_exp_leave_timer', None)
                        if leave_timer is not None: leave_timer.cancel()
                        saved_pos = picked_char.pop('_exp_return_pos', None) or picked_char.get('pre_copy_pos')
                        saved_map = picked_char.pop('_exp_return_map', None) or picked_char.get('pre_copy_map', '11')
                        picked_char.pop('pre_copy_pos', None); picked_char.pop('pre_copy_map', None)
                        picked_char.pop('active_copy_id', None)
                        start_map_transition(conn, picked_char, saved_map, send_rpc_push, override_pos=saved_pos)
                        save_chars(all_accounts_chars)
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                            conn.sendall(struct.pack('>H', len(pf)) + pf)
                        continue

                    if picked_char.get('exp_stage_state'):
                        exp_st = picked_char['exp_stage_state']
                        if exp_st.get('team_id'):
                            cid = int(picked_char.get('id', 0))
                            exp_st['members'] = [x for x in exp_st.get('members', []) if int(x) != cid]
                            exp_st.get('char_refs', {}).pop(cid, None)
                            team = TEAM_STATES.get(int(exp_st.get('team_id') or 0))
                            if team:
                                team_remove_member(team, cid)
                                send_team_update(team['id'])
                                if not exp_st['members']:
                                    exp_st['ai_active'] = False
                                    for timer_key in ('ai_timer', 'timeout_timer'):
                                        run_timer = exp_st.get(timer_key)
                                        if run_timer is not None: run_timer.cancel()
                                    clear_exp_active_monsters(exp_st, send_rpc_push)
                                    team['active_run'] = None
                                    team['pending_run'] = None
                                else:
                                    exp_st['owner_id'] = int(team.get('leader_id', exp_st.get('owner_id', 0)))
                                    team['active_run'] = exp_st
                            else:
                                exp_st['ai_active'] = False
                                for timer_key in ('ai_timer', 'timeout_timer'):
                                    run_timer = exp_st.get(timer_key)
                                    if run_timer is not None: run_timer.cancel()
                                clear_exp_active_monsters(exp_st, send_rpc_push)
                        else:
                            exp_st['ai_active'] = False
                            for timer_key in ('ai_timer', 'timeout_timer'):
                                run_timer = exp_st.get(timer_key)
                                if run_timer is not None: run_timer.cancel()
                            clear_exp_active_monsters(exp_st, send_rpc_push)
                        if exp_st.get('team_id'): broadcast_aoi_remove(picked_char.get('id', 0), picked_char.get('map_id', '11'))
                        picked_char.pop('exp_stage_state', None)
                        picked_char['active_copy_id'] = None
                        picked_char['exp_team_id'] = None
                        picked_char.pop('exp_members', None)
                        picked_char['exp_defer_start'] = False
                        print('[LEAVE COPY] Removed player from EXP run')
                    if picked_char.get('active_copy_id'):
                        saved_pos = picked_char.get('pre_copy_pos')
                        saved_map = picked_char.get('pre_copy_map', '11')
                        picked_char['pre_copy_pos'] = None
                        picked_char['pre_copy_map'] = None
                        picked_char['active_copy_id'] = None
                        picked_char['street_race_return_scheduled'] = False
                    else:
                        saved_pos = picked_char.get('pre_arena_pos')
                        saved_map = picked_char.get('pre_arena_map', '11')
                        picked_char['pre_arena_pos'] = None
                        picked_char['pre_arena_map'] = None
                    if in_exp_stage:
                        saved_pos = picked_char.get('pre_copy_pos')
                        saved_map = picked_char.get('pre_copy_map', '11')
                        picked_char['pre_copy_pos'] = None; picked_char['pre_copy_map'] = None
                    start_map_transition(conn, picked_char, saved_map, send_rpc_push, override_pos=saved_pos)
                    save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 165: # get_team_list
                goal=get_val_str(body,0,'')
                teams={}
                for team_id,team in list(TEAM_STATES.items()):
                    if team.get('active_run') or len(team.get('members',[]))>=4: continue
                    if goal and str(team.get('goal_id',''))!=goal: continue
                    payload=team_payload(team)
                    if payload is not None: teams[int(team_id)]=payload
                send_rpc_push(574,encode_sproto([(0,teams)]))
                if session is not None:
                    ph=encode_sproto([(1,session)]);pf=sproto_pack(ph+encode_sproto([]));conn.sendall(struct.pack('>H',len(pf))+pf)

            # === SOCIAL UI HANDLERS ===

            elif msg == 122: # send_mail
                if picked_char:
                    receive_id = get_val_int(body, 0)
                    context_raw = body.get(1, b"")
                    if isinstance(context_raw, bytes):
                        context = context_raw.decode('utf-8', errors='ignore')
                    else:
                        context = str(context_raw)
                    mail_id = next_mail_id()
                    sender_name = picked_char.get('name', 'Player')
                    mail_entry = {
                        'mailId': mail_id,
                        'sendertype': 4,  # USER type (client MailSenderType.USER=4)
                        'title': f'{sender_name}',
                        'senderTime': int(time.time()),
                        'receiveId': receive_id,
                        'readTime': 0,
                        'context': context,
                        'mailState': 0,
                        'sortTime': int(time.time()),
                        'items': [],
                        'expireday': 7
                    }
                    # BUG FIX: Store mail in RECEIVER's mailbox, not sender's
                    # Find and update the target character's data
                    for area_key, area_chars in all_accounts_chars.items():
                        for acc_key, char_list in area_chars.items():
                            for ch in char_list:
                                if ch.get('id', 0) == receive_id:
                                    if 'mails' not in ch:
                                        ch['mails'] = []
                                    ch['mails'].append(mail_entry)
                                    save_chars(all_accounts_chars)
                                    # If target is online, push mail_update to them
                                    for t_cid, (t_conn, t_char) in list(ALL_CONNECTIONS.items()):
                                        if t_char and t_char.get('id', 0) == receive_id:
                                            try:
                                                mi_bytes = build_mail_update(mail_entry)
                                                ph_p = encode_sproto([(0, 531)])
                                                pf_p = sproto_pack(ph_p + mi_bytes)
                                                mail_pkt = struct.pack(">H", len(pf_p)) + pf_p
                                                t_lock = CONNECTION_LOCKS.get(t_cid)
                                                if t_lock:
                                                    with t_lock:
                                                        t_conn.sendall(mail_pkt)
                                                else:
                                                    t_conn.sendall(mail_pkt)
                                                print(f"[MAIL] mail_update pushed to receiver={receive_id}")
                                            except Exception:
                                                print(f"[MAIL] Failed to push mail_update to receiver={receive_id}")
                                            break
                                    break
                            if ch.get('id', 0) == receive_id:
                                break
                    print(f"[MAIL] send_mail from={picked_char.get('id', 0)} to={receive_id} id={mail_id}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 123: # mail_operation
                if picked_char:
                    mail_id = get_val_int(body, 0)
                    operation = get_val_int(body, 1)
                    print(f"[MAIL] operation={operation} mailId={mail_id}")
                    if operation == 0: # read
                        for mi in picked_char.get('mails', []):
                            if mi.get('mailId') == mail_id:
                                mi['mailState'] = 1
                                mi['readTime'] = int(time.time())
                                break
                        save_chars(all_accounts_chars)
                    elif operation == 1: # delete
                        mails = picked_char.get('mails', [])
                        picked_char['mails'] = [m for m in mails if m.get('mailId') != mail_id]
                        save_chars(all_accounts_chars)
                        send_rpc_push(532, encode_sproto([(0, mail_id)]))
                    elif operation == 2: # get items
                        with get_character_transaction_lock(picked_char):
                            for mi in picked_char.get('mails', []):
                                if mi.get('mailId') == mail_id and mi.get('mailState', 0) != 3:
                                    mi['mailState'] = 3  # GETITEM - client IsGetItem() checks mailstate==3
                                    for it in mi.get('items', []):
                                        add_to_inventory(picked_char, str(it.get('itemId', 0)), it.get('count', 1))
                                    send_rpc_push(611, sync_inventory_data(picked_char))
                                    # Send updated mail_update (531) so client knows the mail state changed
                                    mi_bytes = build_mail_update(mi)
                                    send_rpc_push(531, mi_bytes)
                                    break
                            save_chars(all_accounts_chars)
                    elif operation == 3: # get all items
                        with get_character_transaction_lock(picked_char):
                            for mi in picked_char.get('mails', []):
                                if mi.get('mailState', 0) != 3:  # Skip already claimed
                                    mi['mailState'] = 3  # GETITEM - client IsGetItem() checks mailstate==3
                                    for it in mi.get('items', []):
                                        add_to_inventory(picked_char, str(it.get('itemId', 0)), it.get('count', 1))
                                    # Send updated mail_update (531) for each claimed mail
                                    mi_bytes = build_mail_update(mi)
                                    send_rpc_push(531, mi_bytes)
                            send_rpc_push(611, sync_inventory_data(picked_char))
                            save_chars(all_accounts_chars)
                    elif operation == 4: # delete all
                        picked_char['mails'] = []
                        save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 124: # add_friend (type 0=friend, 1=enemy/foe)
                # CRITICAL: Do NOT block this connection thread with database operations.
                # The client sends heartbeat (218) every 15 seconds and times out after 12 seconds.
                # If this thread blocks for save_chars() or database scans, the heartbeat stops
                # being processed, causing the sender to disconnect.
                # All database work is dispatched to a background thread.
                try:
                    if not picked_char:
                        raise ValueError("No picked_char")
                    target_id = get_val_int(body, 0)
                    add_type = get_val_int(body, 1)
                    my_id = picked_char.get('id', 0)
                    print(f"[FRIEND] add_friend target={target_id} type={add_type}")
                    # Reject self-add (fast, in-memory check)
                    if target_id == my_id:
                        print(f"[FRIEND] Rejected self-add: target={target_id} == my_id={my_id}")
                        continue
                    # Quick in-memory check: is target already in friend_requests_sent?
                    already_sent = False
                    for fr in picked_char.get('friend_requests_sent', []):
                        if fr == target_id:
                            already_sent = True
                            break
                    if already_sent:
                        print(f"[FRIEND] add_friend skipped: already sent request to {target_id}")
                        continue
                    # Quick in-memory check: is target already in enemies?
                    if add_type == 1:
                        already_enemy = False
                        for ei in picked_char.get('enemies', []):
                            if ei.get('friendId') == target_id:
                                already_enemy = True
                                break
                        if already_enemy:
                            print(f"[FRIEND] add_friend enemy skipped: already enemy {target_id}")
                            continue
                    # Dispatch all database work to a background thread
                    # Capture all needed data for the background thread
                    sender_name = picked_char.get('name', f'Player{my_id}')
                    sender_level = picked_char.get('level', 1)
                    sender_prof = picked_char.get('prof', 0)
                    sender_stats = get_character_stats(picked_char)
                    sender_combat = sender_stats['power']
                    sender_guildId = picked_char.get('guildId', 0)
                    sender_guildName = picked_char.get('guildName', '')

                    def _add_friend_async():
                        """Handle all database operations for add_friend in a background thread."""
                        try:
                            # Verify target character exists in the database
                            target_exists = False
                            target_char_data = None
                            for area_key, area_chars in all_accounts_chars.items():
                                for acc_key, char_list in area_chars.items():
                                    for ch in char_list:
                                        if ch.get('id', 0) == target_id:
                                            target_exists = True
                                            target_char_data = ch
                                            break
                                    if target_exists:
                                        break
                                if target_exists:
                                    break
                            if not target_exists:
                                print(f"[FRIEND] Rejected add_friend: target={target_id} does not exist")
                                return

                            if add_type == 1: # enemy/foe
                                if 'enemies' not in picked_char:
                                    picked_char['enemies'] = []
                                # Look up actual target player info
                                target_name = f'Player{target_id}'
                                target_level = 1
                                target_prof = 0
                                target_combat = 0
                                target_state = 0
                                target_guildId = 0
                                target_guildName = ''
                                for t_cid, (t_conn, ch) in list(ALL_CONNECTIONS.items()):
                                    if ch and ch.get('id', 0) == target_id:
                                        target_name = ch.get('name', f'Player{target_id}')
                                        target_level = ch.get('level', 1)
                                        target_prof = ch.get('prof', 0)
                                        target_combat = get_character_stats(ch)['power']
                                        target_state = ch.get('state', 1)
                                        target_guildId = ch.get('guildId', 0)
                                        target_guildName = ch.get('guildName', '')
                                        break
                                else:
                                    if target_char_data:
                                        target_name = target_char_data.get('name', f'Player{target_id}')
                                        target_level = target_char_data.get('level', 1)
                                        target_prof = target_char_data.get('prof', 0)
                                        target_combat = get_character_stats(target_char_data)['power']
                                        target_state = 0
                                        target_guildId = target_char_data.get('guildId', 0)
                                        target_guildName = target_char_data.get('guildName', '')
                                enemy_info = {
                                    'characterId': target_id,
                                    'friendId': target_id,
                                    'name': target_name,
                                    'level': target_level,
                                    'profession': target_prof,
                                    'combValue': target_combat,
                                    'state': target_state,
                                    'timeInfo': int(time.time()),
                                    'friendType': 6,
                                    'guildId': target_guildId,
                                    'guildName': target_guildName,
                                    'friendScore': 0
                                }
                                picked_char['enemies'].append(enemy_info)
                                save_chars(all_accounts_chars)
                                print(f"[FRIEND] Enemy added: {target_id} by {my_id}")
                            else: # friend request
                                if 'friend_requests_sent' not in picked_char:
                                    picked_char['friend_requests_sent'] = []
                                picked_char['friend_requests_sent'].append(target_id)
                                save_chars(all_accounts_chars)
                                # Send notice_add_friend (msg 536) to target if online
                                sender_friend_info = {
                                    'characterId': my_id,
                                    'friendId': my_id,
                                    'name': sender_name,
                                    'level': sender_level,
                                    'profession': sender_prof,
                                    'combValue': sender_combat,
                                    'state': 1,
                                    'timeInfo': int(time.time()),
                                    'friendType': 2,
                                    'guildId': sender_guildId,
                                    'guildName': sender_guildName,
                                    'friendScore': 0
                                }
                                notice_data = encode_sproto([(0, encode_friend_info(sender_friend_info))])
                                ph_p = encode_sproto([(0, 536)])
                                pf_p = sproto_pack(ph_p + notice_data)
                                notice_pkt = struct.pack(">H", len(pf_p)) + pf_p
                                target_found = False
                                print(f"[FRIEND] Looking for target={target_id} in ALL_CONNECTIONS (count={len(ALL_CONNECTIONS)})")
                                for t_cid, (t_conn, t_char) in list(ALL_CONNECTIONS.items()):
                                    if t_char:
                                        print(f"[FRIEND] Checking connection: id={t_char.get('id', 0)} name={t_char.get('name', '')}")
                                    if t_char and t_char.get('id', 0) == target_id:
                                        target_found = True
                                        try:
                                            t_lock = CONNECTION_LOCKS.get(t_cid)
                                            if t_lock:
                                                with t_lock:
                                                    t_conn.sendall(notice_pkt)
                                            else:
                                                t_conn.sendall(notice_pkt)
                                            print(f"[FRIEND] notice_add_friend sent to target={target_id} from={my_id} size={len(notice_pkt)}")
                                        except BrokenPipeError:
                                            print(f"[FRIEND] Target {target_id} connection broken, skipping notice")
                                        except Exception as e:
                                            print(f"[FRIEND] Failed to send notice_add_friend to target={target_id}: {e}")
                                        break
                                if not target_found:
                                    print(f"[FRIEND] Target={target_id} NOT FOUND online - request persisted for next login")
                                # Add sender to target's friend_requests_received for persistence
                                for area_key, area_chars in all_accounts_chars.items():
                                    for acc_key, char_list in area_chars.items():
                                        for ch in char_list:
                                            if ch.get('id', 0) == target_id:
                                                if 'friend_requests_received' not in ch:
                                                    ch['friend_requests_received'] = []
                                                if my_id not in ch['friend_requests_received']:
                                                    ch['friend_requests_received'].append(my_id)
                                                save_chars(all_accounts_chars)
                                                print(f"[FRIEND] add_friend persistence completed for target={target_id}")
                                                break
                                    else:
                                        continue
                                    break
                        except Exception as e:
                            print(f"[FRIEND] ERROR in add_friend async: {e}")
                            traceback.print_exc()

                    async_thread = threading.Thread(target=_add_friend_async, daemon=True)
                    async_thread.start()
                    print(f"[FRIEND] add_friend dispatched to async thread for target={target_id}")
                except Exception as e:
                    print(f"[FRIEND] ERROR in add_friend handler: {e}")
                    traceback.print_exc()
                # Do NOT send session acknowledgment for add_friend (msg 124).
                # The client sends it with session=None (fire-and-forget) and does not expect any response.
                # Sending an unexpected packet can cause the client to crash.

            elif msg == 125: # del_friend (type 0=friend, 1=enemy/foe)
                if picked_char:
                    target_id = get_val_int(body, 0)
                    del_type = get_val_int(body, 1)
                    print(f"[FRIEND] del_friend target={target_id} type={del_type}")
                    if del_type == 1: # enemy/foe
                        enemies = picked_char.get('enemies', [])
                        picked_char['enemies'] = [e for e in enemies if e.get('friendId') != target_id]
                        save_chars(all_accounts_chars)
                        # Client already calls RemoveEnemy() locally in EnemyItemLogic.OnClickDeleteBtn()
                        # Do NOT send ret_del_friend (535) - it calls RemoveFriend() on client,
                        # which would delete the FRIEND with the same ID, not the enemy.
                        # Just send session acknowledgment.
                    else: # friend
                        friends = picked_char.get('friends', [])
                        picked_char['friends'] = [f for f in friends if f.get('friendId') != target_id]
                        save_chars(all_accounts_chars)
                        # For friend deletion, client ret_del_friend_handler calls RemoveFriend()
                        # which is correct - the client does NOT remove friend locally before sending
                        send_rpc_push(535, encode_sproto([(0, target_id)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 126: # request_update_friend_useinfo
                if picked_char:
                    target_id = get_val_int(body, 0)
                    req_type = get_val_int(body, 1)
                    my_id = picked_char.get('id', 0)
                    print(f"[FRIEND] request_update_friend_useinfo target={target_id} type={req_type}")
                    # Return friend list as Dictionary<long, friend_info>
                    # BUG FIX: Client reads dict as value-only array, extracts key from value's friendId
                    # The dict key in Python is ignored by encode_sproto (value-only wire format)
                    # The client extracts the key from each decoded friend_info.friendId
                    friend_dict = {}
                    if req_type == 0: # friends
                        # CRITICAL: Client FilterFriend skips friendType=1 entirely!
                        # friendType==2 -> ApplyFriendDic, friendType!=1 -> MainPlayerFriendDic
                        # So friendType=1 is ignored. Use friendType=0 for confirmed friends.
                        for fi in picked_char.get('friends', []):
                            fi_copy = dict(fi)
                            fi_copy['friendType'] = 0  # Client FilterFriend: friendType=0 -> MainPlayerFriendDic
                            fi_bytes = encode_friend_info(fi_copy)
                            friend_dict[fi.get('friendId', 0)] = fi_bytes
                        # Include pending friend requests so FilterFriend re-adds them to ApplyFriendDic
                        for req_id in picked_char.get('friend_requests_received', []):
                            # Look up requester's real info
                            req_name = f'Player{req_id}'
                            req_level = 1
                            req_prof = 0
                            req_combat = 0
                            req_guildId = 0
                            req_guildName = ''
                            req_state = 0  # Default offline
                            for r_cid, (r_conn, ch) in list(ALL_CONNECTIONS.items()):
                                if ch and ch.get('id', 0) == req_id:
                                    req_name = ch.get('name', f'Player{req_id}')
                                    req_level = ch.get('level', 1)
                                    req_prof = ch.get('prof', 0)
                                    req_combat = get_character_stats(ch)['power']
                                    req_guildId = ch.get('guildId', 0)
                                    req_guildName = ch.get('guildName', '')
                                    req_state = 1  # Online
                                    break
                            else:
                                for area_key, area_chars in all_accounts_chars.items():
                                    for acc_key, char_list in area_chars.items():
                                        for ch in char_list:
                                            if ch.get('id', 0) == req_id:
                                                req_name = ch.get('name', f'Player{req_id}')
                                                req_level = ch.get('level', 1)
                                                req_prof = ch.get('prof', 0)
                                                req_combat = get_character_stats(ch)['power']
                                                req_guildId = ch.get('guildId', 0)
                                                req_guildName = ch.get('guildName', '')
                                                break
                                        else:
                                            continue
                                        break
                            pending_req = {
                                'characterId': req_id,
                                'friendId': req_id,
                                'name': req_name,
                                'level': req_level,
                                'profession': req_prof,
                                'combValue': req_combat,
                                'state': req_state,
                                'timeInfo': int(time.time()),
                                'friendType': 2,
                                'guildId': req_guildId,
                                'guildName': req_guildName,
                                'friendScore': 0
                            }
                            fi_bytes = encode_friend_info(pending_req)
                            friend_dict[req_id] = fi_bytes
                    elif req_type == 1: # enemies
                        for ei in picked_char.get('enemies', []):
                            ei_copy = dict(ei)
                            ei_copy['friendType'] = 6  # Required by client FilterEnemy
                            ei_bytes = encode_friend_info(ei_copy, is_enemy=True)
                            friend_dict[ei.get('friendId', 0)] = ei_bytes
                    # ret_request_update_friend_useinfo: tag 0 = Dictionary, tag 1 = type
                    resp_data = encode_sproto([
                        (0, friend_dict),
                        (1, req_type)
                    ])
                    send_rpc_push(534, resp_data)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 158: # approve_resverve_friend
                if picked_char:
                    target_id = get_val_int(body, 0)
                    is_agree = get_val_int(body, 1)
                    my_id = picked_char.get('id', 0)
                    print(f"[FRIEND] approve_resverve_friend target={target_id} agree={is_agree}")
                    if is_agree == 1:
                        # Remove from pending requests
                        if 'friend_requests_received' not in picked_char:
                            picked_char['friend_requests_received'] = []
                        picked_char['friend_requests_received'] = [
                            r for r in picked_char.get('friend_requests_received', [])
                            if r != target_id
                        ]
                        # Add to friends list (acceptor gets requester as friend)
                        if 'friends' not in picked_char:
                            picked_char['friends'] = []
                        # Look up actual requester info for real name/level/profession
                        req_name = f'Player{target_id}'
                        req_level = 1
                        req_prof = 0
                        req_combat = 0
                        req_guildId = 0
                        req_guildName = ''
                        for r_cid, (r_conn, ch) in list(ALL_CONNECTIONS.items()):
                            if ch and ch.get('id', 0) == target_id:
                                req_name = ch.get('name', f'Player{target_id}')
                                req_level = ch.get('level', 1)
                                req_prof = ch.get('prof', 0)
                                req_combat = get_character_stats(ch)['power']
                                req_guildId = ch.get('guildId', 0)
                                req_guildName = ch.get('guildName', '')
                                break
                        else:
                            for area_key, area_chars in all_accounts_chars.items():
                                for acc_key, char_list in area_chars.items():
                                    for ch in char_list:
                                        if ch.get('id', 0) == target_id:
                                            req_name = ch.get('name', f'Player{target_id}')
                                            req_level = ch.get('level', 1)
                                            req_prof = ch.get('prof', 0)
                                            req_combat = get_character_stats(ch)['power']
                                            req_guildId = ch.get('guildId', 0)
                                            req_guildName = ch.get('guildName', '')
                                            break
                                else:
                                    continue
                                break
                        # Check if already friends to avoid duplicates
                        already_friend = False
                        for existing in picked_char.get('friends', []):
                            if existing.get('friendId') == target_id:
                                already_friend = True
                                break
                        if already_friend:
                            print(f"[FRIEND] Accept skipped: {my_id} already has {target_id} as friend")
                            save_chars(all_accounts_chars)
                            if session is not None:
                                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                                conn.sendall(struct.pack(">H", len(pf)) + pf)
                            continue
                        # Use actual online state for the requester
                        req_state = 1 if target_id in ALL_CONNECTIONS else 0
                        new_friend = {
                            'characterId': target_id,
                            'friendId': target_id,
                            'name': req_name,
                            'level': req_level,
                            'profession': req_prof,
                            'combValue': req_combat,
                            'state': req_state,
                            'timeInfo': int(time.time()),
                            'friendType': 0,
                            'guildId': req_guildId,
                            'guildName': req_guildName,
                            'friendScore': 0
                        }
                        picked_char['friends'].append(new_friend)
                        save_chars(all_accounts_chars)
                        # syn_friend_info: tag 0 = friend_info object
                        send_rpc_push(538, encode_sproto([(0, encode_friend_info(new_friend))]))
                        # BUG FIX: Also add acceptor as friend to the requester's list (mutual friendship)
                        # Find and update the requester's character data
                        for area_key, area_chars in all_accounts_chars.items():
                            for acc_key, char_list in area_chars.items():
                                for ch in char_list:
                                    if ch.get('id', 0) == target_id:
                                        if 'friends' not in ch:
                                            ch['friends'] = []
                                        # Check if already friends
                                        already_friend = False
                                        for existing in ch['friends']:
                                            if existing.get('friendId') == my_id:
                                                already_friend = True
                                                break
                                        if not already_friend:
                                            # Use actual online state for the acceptor
                                            acc_state = 1 if my_id in ALL_CONNECTIONS else 0
                                            mutual_friend = {
                                                'characterId': my_id,
                                                'friendId': my_id,
                                                'name': picked_char.get('name', f'Player{my_id}'),
                                                'level': picked_char.get('level', 1),
                                                'profession': picked_char.get('prof', 0),
                                                'combValue': get_character_stats(picked_char)['power'],
                                                'state': acc_state,
                                                'timeInfo': int(time.time()),
                                                'friendType': 0,
                                                'guildId': picked_char.get('guildId', 0),
                                                'guildName': picked_char.get('guildName', ''),
                                                'friendScore': 0
                                            }
                                            ch['friends'].append(mutual_friend)
                                            # Push syn_friend_info to the requester so they see the mutual friend immediately.
                                            # Send in a background daemon thread to avoid blocking the accepter's
                                            # connection thread. If the requester's socket is slow or stale,
                                            # a synchronous sendall() would block the accepter's handler,
                                            # stopping heartbeat (218) processing and causing the accepter to disconnect.
                                            mutual_friend_copy = dict(mutual_friend)
                                            def _send_syn_friend_info_async():
                                                for r_cid, (r_conn, r_char) in list(ALL_CONNECTIONS.items()):
                                                    if r_char and r_char.get('id', 0) == target_id:
                                                        try:
                                                            fi_bytes = encode_friend_info(mutual_friend_copy)
                                                            ph_p = encode_sproto([(0, 538)])
                                                            pf_p = sproto_pack(ph_p + encode_sproto([(0, fi_bytes)]))
                                                            syn_pkt = struct.pack(">H", len(pf_p)) + pf_p
                                                            r_lock = CONNECTION_LOCKS.get(r_cid)
                                                            if r_lock:
                                                                with r_lock:
                                                                    r_conn.sendall(syn_pkt)
                                                            else:
                                                                r_conn.sendall(syn_pkt)
                                                            print(f"[FRIEND] syn_friend_info pushed to requester={target_id}")
                                                        except Exception:
                                                            print(f"[FRIEND] Failed to push syn_friend_info to requester={target_id}")
                                                        break
                                            syn_thread = threading.Thread(target=_send_syn_friend_info_async, daemon=True)
                                            syn_thread.start()
                                            print(f"[FRIEND] Mutual friendship saved for requester={target_id}")
                                        # Remove from friend_requests_sent
                                        if 'friend_requests_sent' in ch:
                                            ch['friend_requests_sent'] = [r for r in ch['friend_requests_sent'] if r != my_id]
                                        save_chars(all_accounts_chars)
                                        break
                                if ch.get('id', 0) == target_id:
                                    break
                    else:
                        # Reject: remove from receiver's friend_requests_received
                        if 'friend_requests_received' not in picked_char:
                            picked_char['friend_requests_received'] = []
                        picked_char['friend_requests_received'] = [
                            r for r in picked_char.get('friend_requests_received', [])
                            if r != target_id
                        ]
                        # Also remove receiver from sender's friend_requests_sent
                        # so the sender can send another request in the future
                        for area_key, area_chars in all_accounts_chars.items():
                            for acc_key, char_list in area_chars.items():
                                for ch in char_list:
                                    if ch.get('id', 0) == target_id:
                                        if 'friend_requests_sent' in ch:
                                            ch['friend_requests_sent'] = [
                                                r for r in ch['friend_requests_sent'] if r != my_id
                                            ]
                                        save_chars(all_accounts_chars)
                                        break
                                else:
                                    continue
                                break
                        print(f"[FRIEND] Friend request rejected by {my_id} to {target_id}, cleaned up sender's friend_requests_sent")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 284: # send_mail_box (system mail)
                if picked_char:
                    subject_raw = body.get(0, b"")
                    context_raw = body.get(1, b"")
                    email_raw = body.get(2, b"")
                    if isinstance(subject_raw, bytes):
                        subject = subject_raw.decode('utf-8', errors='ignore')
                    else:
                        subject = str(subject_raw)
                    if isinstance(context_raw, bytes):
                        context = context_raw.decode('utf-8', errors='ignore')
                    else:
                        context = str(context_raw)
                    mail_id = next_mail_id()
                    mail_entry = {
                        'mailId': mail_id,
                        'sendertype': 0,
                        'title': subject,
                        'senderTime': int(time.time()),
                        'receiveId': picked_char.get('id', 0),
                        'readTime': 0,
                        'context': context,
                        'mailState': 0,
                        'sortTime': int(time.time()),
                        'items': [],
                        'expireday': 7
                    }
                    if 'mails' not in picked_char:
                        picked_char['mails'] = []
                    picked_char['mails'].append(mail_entry)
                    save_chars(all_accounts_chars)
                    # Push mail_update to client
                    mi_bytes = build_mail_update(mail_entry)
                    send_rpc_push(531, mi_bytes)
                    print(f"[MAIL] send_mail_box id={mail_id} subject={subject}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 159: # req_random_online_character_list
                # Client sends no body data; return random online characters
                if picked_char:
                    online_chars = get_online_characters()
                    friend_list = []
                    for oc in online_chars:
                        if oc.get('id', 0) != picked_char.get('id', 0):
                            fi = {
                                'characterId': oc.get('id', 0),
                                'friendId': oc.get('id', 0),
                                'name': oc.get('name', f'Player{oc.get("id", 0)}'),
                                'level': oc.get('level', 1),
                                'profession': oc.get('prof', 0),
                                'combValue': get_character_stats(oc)['power'],
                                'state': 1,
                                'timeInfo': int(time.time()),
                                'friendType': 0,
                                'guildId': oc.get('guildId', 0),
                                'guildName': oc.get('guildName', ''),
                                'friendScore': 0
                            }
                            friend_list.append(encode_friend_info(fi))
                    # ret_random_online_character_list: tag 0 = List<friend_info>
                    resp_data = encode_sproto([(0, friend_list)])
                    send_rpc_push(570, resp_data)
                    print(f"[FRIEND] req_random_online_character_list returned {len(friend_list)} chars")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 160: # search_online_character_by_name
                if picked_char:
                    search_name = get_val_str(body, 0)
                    friend_list = []
                    if search_name:
                        online_chars = get_online_characters()
                        for oc in online_chars:
                            oc_name = oc.get('name', '')
                            if oc_name and search_name.lower() in oc_name.lower():
                                if oc.get('id', 0) != picked_char.get('id', 0):
                                    fi = {
                                        'characterId': oc.get('id', 0),
                                        'friendId': oc.get('id', 0),
                                        'name': oc_name,
                                        'level': oc.get('level', 1),
                                        'profession': oc.get('prof', 0),
                                        'combValue': get_character_stats(oc)['power'],
                                        'state': 1,
                                        'timeInfo': int(time.time()),
                                        'friendType': 0,
                                        'guildId': oc.get('guildId', 0),
                                        'guildName': oc.get('guildName', ''),
                                        'friendScore': 0
                                    }
                                    friend_list.append(encode_friend_info(fi))
                    # ret_search_online_character_by_name: tag 0 = List<friend_info>
                    resp_data = encode_sproto([(0, friend_list)])
                    send_rpc_push(571, resp_data)
                    print(f"[FRIEND] search_online_character_by_name name={search_name} found={len(friend_list)}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 321: # gather_other_player (warp to another player)
                if picked_char:
                    target_id = get_val_int(body, 0)
                    # Find target player and warp to their location
                    target_conn = None
                    target_char = None
                    for t_cid, (t_conn, ch) in list(ALL_CONNECTIONS.items()):
                        if ch and ch.get('id', 0) == target_id:
                            target_conn = t_conn
                            target_char = ch
                            break
                    if target_char and 'pos' in target_char:
                        target_pos = target_char['pos']
                        target_map = target_char.get('map_id', '11')
                        current_map = str(picked_char.get('map_id', '11'))
                        if not is_current_character_connection(conn, picked_char):
                            print(f"[GATHER] denied stale character socket player={picked_char.get('id')}")
                        elif (is_exp_map(target_map) or is_exp_map(current_map)
                                or picked_char.get('exp_stage_state')):
                            # A gather request must not join or abandon an EXP
                            # instance; those transitions require the run's
                            # normal entry, reconnect, and exit checks.
                            print(f"[EXP STAGE] denied gather across run boundary player={picked_char.get('id')} target={target_id} map={target_map}")
                        else:
                            print(f"[GATHER] Warping player {picked_char.get('id', 0)} to player {target_id} at map={target_map} pos={target_pos}")
                            picked_char['pos'] = list(target_pos)
                            start_map_transition(conn, picked_char, target_map, send_rpc_push, override_pos=list(target_pos))
                    else:
                        print(f"[GATHER] Target player {target_id} not found online")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 324: # update_player_map_info (RPC - get enemy location for revenge)
                # Client sends this as RPC with session - must respond WITH session
                if picked_char:
                    target_id = get_val_int(body, 0)
                    # Find target player and return their map info
                    target_char = None
                    for t_cid, (t_conn, ch) in list(ALL_CONNECTIONS.items()):
                        if ch and ch.get('id', 0) == target_id:
                            target_char = ch
                            break
                    if target_char:
                        pos = target_char.get('pos', [0, 0, 0, 0])
                        pos_obj = encode_sproto([
                            (0, pos[0] if len(pos) > 0 else 0),
                            (1, pos[1] if len(pos) > 1 else 0),
                            (2, pos[2] if len(pos) > 2 else 0),
                            (3, pos[3] if len(pos) > 3 else 0)
                        ])
                        # update_player_map_info.response: tag 0=state, 1=mapid, 2=pos
                        resp_data = encode_sproto([
                            (0, 1),
                            (1, target_char.get('map_id', '11')),
                            (2, pos_obj)
                        ])
                        # Send as RPC response with session ID
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp_data)
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        print(f"[RPC] update_player_map_info target={target_id} map={target_char.get('map_id', '11')}")
                    else:
                        # Target not online - still respond with session
                        resp_data = encode_sproto([(0, 0)])
                        if session is not None:
                            ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp_data)
                            conn.sendall(struct.pack(">H", len(pf)) + pf)
                        print(f"[RPC] update_player_map_info target={target_id} not found")
                elif session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except Exception as exc:
        print(f"[!] Client handler exception for {addr}: {exc}")
        traceback.print_exc()
    finally:
        try:
            if conn_id in NPC_SPAWNED_MAPS:
                del NPC_SPAWNED_MAPS[conn_id]
            if picked_char and picked_char.get('exp_stage_state'):
                save_chars(all_accounts_chars)
            # A superseded socket can close after the player already
            # reconnected. Only the connection currently registered for this
            # character may broadcast removal/offline state.
            char_id = int(picked_char.get('id', 0)) if picked_char else 0
            is_current_connection = bool(picked_char and ALL_CONNECTIONS.get(char_id, (None, None))[0] is conn)
            if picked_char:
                if is_current_connection:
                    broadcast_aoi_remove(char_id, picked_char.get('map_id', '11'))
            RANDOM_NAME_CALLED.discard(conn_id)
            if picked_char:
                if is_current_connection:
                    ALL_CONNECTIONS.pop(char_id, None)
                    CONNECTION_LOCKS.pop(char_id, None)
                    # Update offline state only after the active connection
                    # has actually gone away.
                    _sync_friend_online_state(char_id, 0)
            conn.close()
        except Exception:
            pass
        print(f"[-] Client disconnected: {addr}")


def team_join_member(team, char):
    cid=int(char.get('id',0))
    if not team or team.get('active_run') or team.get('pending_run') or len(team.get('members',[]))>=4 or cid in team.get('members',[]): return False
    if not int(team.get('min_level',1))<=int(char.get('level',1))<=int(team.get('max_level',80)): return False
    if not is_character_available_for_team(char) or (CHAR_TEAM.get(cid) not in (None, int(team.get('id', 0)))): return False
    team.setdefault('members',[]).append(cid); team.setdefault('char_refs',{})[cid]=char
    CHAR_TEAM[cid]=int(team['id']); char['team_goal_copy']=str(team.get('goal_id',''))
    return True

def team_goal_level_bounds(copy_id, min_level=1, max_level=80):
    """Use the APK copy's configured level band for EXP squads."""
    copy_id = str(copy_id or '')
    if copy_id in DAILY_EXP_CONFIG:
        cfg = COPY_SCENE_CONFIG.get(copy_id, {})
        minimum = max(1, int(cfg.get('min_level', min_level)))
        maximum = max(minimum, int(cfg.get('max_level', max_level)))
        return minimum, maximum
    minimum = max(1, int(min_level))
    return minimum, max(minimum, int(max_level))

def team_remove_member(team, char_id):
    cid=int(char_id)
    if not team or cid not in team.get('members',[]): return
    team['members'].remove(cid); CHAR_TEAM.pop(cid,None); team.get('char_refs',{}).pop(cid,None)
    ch=ALL_CONNECTIONS.get(cid,(None,None))[1]
    if ch: ch.pop('team_goal_copy',None)
    if team.get('leader_id')==cid and team['members']: team['leader_id']=team['members'][0]
    if not team.get('members'): TEAM_STATES.pop(int(team['id']),None)

def send_team_match_state(member_ids, goal_id, goal_type, state):
    data=encode_sproto([(0,int(state)),(1,str(goal_id)),(2,int(goal_type))])
    for member_id in member_ids: send_push_to_char(member_id,681,data)

def queue_team_match(team, goal_id, goal_type):
    copy_id=str(goal_id); cfg=COPY_SCENE_CONFIG.get(copy_id)
    if not team or not cfg or copy_id not in DAILY_EXP_CONFIG: return False
    max_members=min(4,int(cfg.get('max_member',4))); min_members=max(2,int(cfg.get('min_member',2)))
    if len(team.get('members',[]))>=min_members:
        team['matching']=False
        send_team_match_state(list(team.get('members',[])),copy_id,goal_type,1)
        return True
    queue=TEAM_MATCH_QUEUE.setdefault(copy_id,[])
    if team['id'] not in queue: queue.append(team['id'])
    team['matching']=True
    send_team_match_state(list(team.get('members',[])),copy_id,goal_type,0)
    while len(team.get('members',[])) < min_members:
        other_id=next((tid for tid in queue if tid!=team['id']),None)
        if other_id is None: break
        other=TEAM_STATES.get(other_id)
        if other is None or other.get('active_run') or not other.get('matching'):
            queue.remove(other_id); continue
        combined=list(team.get('members',[]))+list(other.get('members',[]))
        if len(combined)>max_members:
            queue.remove(other_id); continue
        players=[]; valid=True
        for member_id in combined:
            member=ALL_CONNECTIONS.get(int(member_id),(None,None))[1]
            if member is None or not int(cfg.get('min_level',1))<=int(member.get('level',1))<=int(cfg.get('max_level',80)):
                valid=False; break
            players.append((int(member_id),member))
        if not valid:
            queue.remove(other_id); continue
        queue.remove(other_id)
        for member_id,member in players:
            if member_id not in team['members']: team['members'].append(member_id)
            team.setdefault('char_refs',{})[member_id]=member
            CHAR_TEAM[member_id]=int(team['id']); member['team_goal_copy']=copy_id
        other['members']=[]; other['char_refs']={}; TEAM_STATES.pop(int(other_id),None)
        send_team_update(team['id'])
        if len(team['members'])>=min_members:
            team['matching']=False
            if team['id'] in queue: queue.remove(team['id'])
            send_team_match_state(list(team['members']),copy_id,goal_type,1)
            break
    return True

def stop_team_match(team, goal_id, goal_type):
    copy_id=str(goal_id)
    if team:
        team['matching']=False
        queue=TEAM_MATCH_QUEUE.get(copy_id,[])
        if team['id'] in queue: queue.remove(team['id'])
        if not queue: TEAM_MATCH_QUEUE.pop(copy_id,None)
        send_team_match_state(list(team.get('members',[])),copy_id,goal_type,1)
def detach_exp_team_member(team, member_id):
    cid=int(member_id); state=team.get('active_run') if team else None
    if team and team.get('matching'): stop_team_match(team,team.get('goal_id',''),1)
    if team and team.get('pending_run'):
        for other_id in team.get('members',[]):
            if int(other_id)!=cid: send_push_to_char(other_id,603,encode_sproto([(0,cid),(1,1)]))
        team['pending_run']=None
        ready_timer=team.pop('_ready_timer',None)
        if ready_timer: ready_timer.cancel()
    conn,char=ALL_CONNECTIONS.get(cid,(None,None))
    if char is None and team: char=team.get('char_refs',{}).get(cid)
    if state and cid in exp_member_ids(state):
        if char and char.get('exp_stage_state') is state:
            if conn and is_exp_map(char.get('map_id')): broadcast_aoi_remove(cid,char.get('map_id'))
            pos=char.get('pre_copy_pos'); mid=char.get('pre_copy_map','11')
            state['members']=[x for x in state.get('members',[]) if int(x)!=cid]
            state.get('char_refs',{}).pop(cid,None)
            char.pop('exp_stage_state',None); char['active_copy_id']=None; char['exp_team_id']=None
            char.pop('pre_copy_pos',None); char.pop('pre_copy_map',None)
            if conn:
                start_map_transition(conn,char,mid,lambda tag,data,who=cid:send_push_to_char(who,tag,data),override_pos=pos)
            else:
                if pos: char['pos']=pos
                char['map_id']=mid
        else:
            state['members']=[x for x in state.get('members',[]) if int(x)!=cid]
            state.get('char_refs',{}).pop(cid,None)
    team_remove_member(team,cid)
    if state:
        if state.get('members'):
            team['active_run']=state
            state['owner_id']=int(team.get('leader_id',state.get('owner_id',0)))
        else:
            state['ai_active']=False
            for timer_key in ('ai_timer','timeout_timer'):
                run_timer=state.get(timer_key)
                if run_timer is not None: run_timer.cancel()
            clear_exp_active_monsters(state, lambda tag, data: exp_send_push(state, tag, data))
            if team: team['active_run']=None; team['pending_run']=None
    if team and team.get('members'): send_team_update(team['id'])
    save_chars(all_accounts_chars)
def start_team_exp_run(team, copy_id):
    copy_id=str(copy_id); cfg=COPY_SCENE_CONFIG.get(copy_id)
    ids=list(team.get('members',[]))
    pending=team.get('pending_run') if team else None
    if (not pending or str(pending.get('copy_id'))!=copy_id or str(team.get('goal_id'))!=copy_id
            or not pending.get('starting') or set(pending.get('ready',{}))!=set(ids)
            or not all(pending.get('ready',{}).values())): return False
    if not cfg or copy_id not in DAILY_EXP_CONFIG or len(ids)<max(2,int(cfg.get('min_member',2))) or len(ids)>int(cfg.get('max_member',4)): return False
    players=[]
    for cid in ids:
        conn,ch=ALL_CONNECTIONS.get(cid,(None,None))
        if conn is None or ch is None: return False
        if not is_character_available_for_team(ch) or not cfg['min_level']<=int(ch.get('level',1))<=cfg['max_level']: return False
        if copy_attempts_remaining(ch,copy_id,cfg)<=0: return False
        players.append((cid,conn,ch))
    leader=next((ch for cid,_conn,ch in players if int(cid)==int(team.get('leader_id',0))),None)
    if leader is None: return False
    for cid,conn,ch in players:
        if ch.get('pre_copy_pos') is None:
            ch['pre_copy_pos']=list(ch.get('pos',[29860,100,-17005,0])); ch['pre_copy_map']=str(ch.get('map_id','11'))
        daily=ensure_daily_copy_state(ch)
        remaining=copy_attempts_remaining(ch,copy_id,cfg)
        daily['remaining'][copy_id]=remaining-1
        ch['active_copy_id']=copy_id; ch['exp_members']=ids; ch['exp_team_id']=int(team['id']); ch['exp_defer_start']=True
        ch['hp']=get_character_stats(ch)['hp_max']; sync_char_attrs_rpc(conn,ch)
        send_push_to_char(cid,555,sync_copy_scenes(ch))
        # Mission 1019 uses Target=105, not a specific level bracket map ID.
        advance_missions(ch,lambda tag,data,who=cid:send_push_to_char(who,tag,data),'dungeon',target_id='105')
    # Instance IDs are monotonic; retain dead IDs so delayed duplicate damage
    # packets from another active run cannot revive/count an old monster.
    save_chars(all_accounts_chars)
    for cid,conn,ch in players:
        start_map_transition(conn,ch,copy_id,lambda tag,data,who=cid:send_push_to_char(who,tag,data))
    owner=ALL_CONNECTIONS.get(int(team['leader_id']),(None,players[0][2]))[1] or players[0][2]
    state=owner.get('exp_stage_state')
    if not state or str(state.get('copy_id'))!=copy_id: return False
    state['team_id']=int(team['id']); state['members']=ids; state['owner_id']=int(team['leader_id']); state['defer_start']=False
    for cid,conn,ch in players:
        ch['exp_stage_state']=state; ch['exp_team_id']=int(team['id']); ch['exp_defer_start']=False
        state.setdefault('char_refs',{})[cid]=ch
    team['active_run']=state; team['pending_run']=None
    _send_exp_tag683(lambda tag,data:exp_send_push(state,tag,data),state,DAILY_EXP_CONFIG[copy_id])
    start_exp_stage_runtime(players[0][1],owner,state); send_team_update(team['id']); save_chars(all_accounts_chars)
    return True

def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    if hasattr(socket, "SO_REUSEPORT"):
        try:
            server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEPORT, 1)
        except Exception:
            pass
    server.bind(("0.0.0.0", PORT))
    server.listen(20)
    print(f"GAME SERVER 9555 READY ON PORT {PORT}")
    while True:
        try:
            cl, ad = server.accept()
            threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
        except Exception as e:
            print(f"[!] Accept error: {e}")

if __name__ == "__main__":
    start_server()
