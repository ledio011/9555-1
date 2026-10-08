import socket, struct, threading, random, json, os, time, traceback, math

PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
CHAR_DB = os.path.join(SCRIPT_DIR, "characters_final.json")
BAK_DB = CHAR_DB + ".bak"
TMP_DB = CHAR_DB + ".tmp"
RESOURCE_ROOT = os.path.join(SCRIPT_DIR, "assets")
server_session_counter = 8000
GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {} # inst_id -> nid (to resolve rewards)
NPC_HP_MAP = {}   # inst_id -> current hp
NPC_SPAWNED_MAPS = {}  # connection identity -> maps already sent to that client
DEAD_NPC_SET = set() # duplicate death/reward prevention set
RANDOM_NAME_CALLED = set()  # connection identities that have had their first random name request
ALL_CONNECTIONS = {}  # char_id -> (conn, picked_char) for tracking online players
CONNECTION_LOCKS = {}  # char_id -> threading.Lock() for per-connection send protection
MAIL_ID_COUNTER = 1000000  # Will be initialized from max existing mailId on startup

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
EFF_CONFIG = {}   # effId -> effect info template
SKILL_CONFIG = {} # skillId -> skill info template
EFF_TO_SKILL = {} # effId -> skillId (reverse lookup for Tag 128)
MOUNT_CONFIG = {} # garage vehicle id -> client MountData definition
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
                        'eff2': parts[28] if len(parts) > 28 else ""
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
                        'group': group
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
                        'need_num': int(parts[3]) if parts[3].isdigit() else 1
                    }
        print(f"[SURVEY DATA LOADED] count={len(SURVEY_DATA)}")

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
                        'gta_link_car_id': parts[38] if len(parts) > 38 and parts[38] else parts[1]
                    }
        garage_count = sum(1 for v in MOUNT_CONFIG.values() if v.get('need_show') == 1)
        motor_count = sum(1 for v in MOUNT_CONFIG.values() if v.get('is_motor') == 1)
        print(f"[MOUNT CONFIG LOADED] total={len(MOUNT_CONFIG)} garage={garage_count} motorcycles={motor_count}")

    # Daily-copy UI receives its state from TAG 555.  Keep the IDs/types in
    # lockstep with CopySceneData so client tutorials can locate their target.
    copy_path = os.path.join(text_asset_root, "CopySceneData")
    if os.path.exists(copy_path):
        with open(copy_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 19 and parts[0] == "*" and parts[1].isdigit() and parts[11] == "1":
                    COPY_SCENE_CONFIG[parts[1]] = {
                        'map_id': parts[5],
                        'subtype': int(parts[12]) if parts[12].isdigit() else 0,
                        'exist_time': int(parts[13]) if parts[13].isdigit() else 0,
                        'end_time': int(parts[10]) if parts[10].isdigit() else 0,
                        'max_plays': int(parts[18]) if parts[18].isdigit() else 0,
                        'min_level': int(parts[19]) if parts[19].isdigit() else 1
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

def save_chars(data):
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
            json.dump(data, f, indent=4)
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
    # These names are server placeholders, not names supplied by the APK data.
    name = "Ash Viper"
    prof = 0

    # Use get_npc_attr("1105") with player level for consistent stats
    # This ensures boss HP bar, stats, and actual server HP all agree
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

    # ObjZombiePlayer removes a skill from its automatic list after using it.
    # Supplying the complete XD combat set gives it valid fallbacks to chase
    # and attack instead of becoming idle when the first skill is unavailable.
    boss_skill_levels = {
        "101": 1,
        "105": 1, "106": 1, "107": 1,
        "108": 1, "109": 1, "110": 1,
    }
    skills_map = build_skills_map(prof, player_level, boss_skill_levels)

    # Runtime: attribute(6), attribute_all(7)
    attr_run = encode_sproto([(0, hp_max), (2, atk), (3, df)])

    # Use boss_stats for all attributes to ensure consistency
    attr_all_data = [
        (0, hp_max), (2, atk), (3, df),
        (4, boss_stats['hit']), (5, boss_stats['eva']), (6, boss_stats['cri']), (7, boss_stats['res']),
        (8, boss_stats['exd']), (9, boss_stats['exr']), (10, boss_stats['crd']), (11, boss_stats['crr']),
        (12, boss_stats['defa']), (13, 700), (14, 100),
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
    # Assign sequential indexPos starting from 4 for the first 3 extra skills.
    extra_skill_idx = 0
    for sid, slv in skill_levels.items():
        if sid not in smap:
            # Only assign indexPos 4-6 for the first 3 extra skills (zombie AI limit)
            # Skills beyond index 6 will be added but won't be in mEnableSkillIDList
            if extra_skill_idx < 3:
                skill_idx = 4 + extra_skill_idx  # 4, 5, 6
            else:
                skill_idx = 7 + extra_skill_idx  # 7, 8, ... (won't be used by zombie AI)
            smap[sid] = encode_sproto([
                (0, sid),
                (1, slv),
                (2, skill_idx),   # indexPos
                (3, 1),           # unlockLevel
                (4, skill_idx),   # indexPos2
                (5, False)        # disable
            ])
            extra_skill_idx += 1

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
    """Return True if the map should not have player-to-player AOI (e.g., Map 11)."""
    return str(map_id) == "11"

def broadcast_aoi_move(char):
    """Broadcast TAG 507 (aoi_update_move) to all other players on the same map.
    Client expects: request field 0 -> character_aoi_move -> id(0), movement(1), walk(2)"""
    if not char:
        return
    char_id = char.get('id', 0)
    map_id = str(char.get('map_id', '11'))
    if is_single_player_map(map_id):
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
            if ch and ch.get('id', 0) != char_id and str(ch.get('map_id', '11')) == map_id:
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
    if is_single_player_map(map_id):
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
            if ch and ch.get('id', 0) != char_id and str(ch.get('map_id', '11')) == map_id:
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
    if is_single_player_map(map_id):
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
        prop = encode_sproto([(13, char.get('cash', 0))])
        aoi_attr = encode_sproto([
            (0, char_id), (1, attr_oth), (2, attr_base), (3, attr_all), (5, prop)
        ])
        ph_p = encode_sproto([(0, 510)])
        pf_p = sproto_pack(ph_p + encode_sproto([(0, aoi_attr)]))
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
            if ch and ch.get('id', 0) != char_id and str(ch.get('map_id', '11')) == map_id:
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

    prop = encode_sproto([(13, picked_char.get('cash', 0))])

    aoi_attr = encode_sproto([
        (0, picked_char['id']), (1, attr_oth), (2, attr_base), (3, attr_all), (5, prop)
    ])

    print(f"[PLAYER SYNC] HP={hp_cur}/{stats['hp_max']} POWER={stats['power']} LV={stats['lv']}")
    try:
        ph_p = encode_sproto([(0, 510)])
        pf_p = sproto_pack(ph_p + encode_sproto([(0, aoi_attr)]))
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        # Use connection lock to prevent packet framing issues
        if conn_id is not None:
            c_lock = CONNECTION_LOCKS.get(conn_id)
            if c_lock:
                with c_lock:
                    conn.sendall(pkt)
            else:
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
        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
    except: pass

def get_combat_damage(attacker_stats, defender_stats, skill_id, skill_lv, is_area=False, pvp_scale=1.0, effinfo_id=None):
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
    pvp_mult = pvp_scale
    if attacker_stats.get('power', 0) > defender_stats.get('power', 0):
        pvp_mult += 0.05

    # 2. Check Hit/Dodge
    skill_shit = eff_cfg['adds'].get(3001, 0) / 10000.0
    hit_p = min((attacker_stats['hit'] + 1.0) / (attacker_stats['hita'] + attacker_stats['hit'] + 1.0), 1.0)
    dge_p = min((defender_stats['eva'] + 1.0) / (defender_stats['dgea'] + defender_stats['eva'] + 1.0), 0.5)

    # Client formula: 1 + hit_p - dge_p + skill_shit >= random/100
    # Where random is 0-99, so random/100 is 0.00 to 0.99
    hit_prob = 1.0 + hit_p - dge_p + skill_shit
    roll_hit = random.random()  # 0.0 to 1.0

    if is_area:
        print(f"{prefix} HIT CHECK: hit_prob={hit_prob:.3f} (hit_p={hit_p:.3f}, dge_p={dge_p:.3f}, skill={skill_shit:.3f}) roll={roll_hit:.3f}")
        print(f"{prefix} STATS: AtkHIT={attacker_stats['hit']} AtkHITA={attacker_stats['hita']} DefEVA={defender_stats['eva']} DefDGEA={defender_stats['dgea']}")

    if roll_hit > hit_prob:
        if is_area: print(f"{prefix} RESULT: MISS")
        return 0, False, False # MISS

    # 3. Check Crit
    skill_scri = eff_cfg['adds'].get(3002, 0) / 10000.0
    cri_p = min((attacker_stats['cri'] + 1.0) / (attacker_stats['cri'] + attacker_stats['cria'] + 1.0), 0.9)
    res_p = min((defender_stats['res'] + 1.0) / (defender_stats['res'] + defender_stats['resa'] + 1.0), 0.8)

    cri_prob = cri_p - res_p + skill_scri
    is_cri = random.random() < cri_prob

    # 4. Calculate Damage
    scaled_damage = skill_damage * pvp_mult
    scaled_scale = skill_scale * pvp_mult

    # Add SATP/SATM/SATC contribution based on defender type (CharacterAttributeData.cs lines 983-1008)
    # SATC for cars, SATM for NPCs (FunctionType != 10), SATP for other targets (players, zombies)
    # Server doesn't track defender obj_type precisely, so use SATM for NPC targets, SATP for PvP
    sat_type = attacker_stats.get('sat_type', 'satm')  # 'satm' for NPC attacks, 'satp' for player/zombie
    sat_value = attacker_stats.get(sat_type, 0)
    scaled_scale += sat_value / 10000.0 if sat_value else 0
    scaled_damage += sat_value

    base_dmg = attacker_stats['atk'] * scaled_scale + scaled_damage
    def_red = min((defender_stats['def'] + 1.0) / (defender_stats['def'] + attacker_stats['defa']), 0.5)

    # 5. Handling Critical Multiplier
    crit_mult = 1.0
    if is_cri:
        crit_mult = max(1.0, min(1.0 + (attacker_stats['crd'] - defender_stats['crr']) / 10000.0, 2.0))

    # 6. Final Formula with Random Variance [0.95, 1.049]
    # Client: GetRandom() returns 0-99, then /1000 + 0.95 = 0.95 to 1.049
    rand_var = random.randint(0, 99) / 1000.0 + 0.95

    skill_sexd = eff_cfg['adds'].get(3003, 0) / 10000.0
    exd_factor = 1.0 + (attacker_stats['exd'] - defender_stats['exr']) / 10000.0 + skill_sexd

    final_dmg = crit_mult * base_dmg * rand_var * (1.0 - def_red) * exd_factor

    if is_area:
        print(f"{prefix} RESULT: HIT dmg={int(final_dmg)} base={base_dmg:.1f} red={def_red:.3f} crit={crit_mult:.2f} exd={exd_factor:.2f} var={rand_var:.3f}")

    # Client uses Mathf.CeilToInt(num5) - use ceiling, not truncation
    return int(math.ceil(max(1, final_dmg))), True, is_cri

def spawn_exp_stage_subwave_internal(conn, send_rpc_push, picked_char, exp_state, exp_cfg, send_npc_func):
    subwave = (exp_state['cur_group'] - 1) * 4 + exp_state['cur_wave']
    exp_state['subwave'] = subwave
    exp_state['wave_kills'] = 0
    exp_state['active_monsters'] = []

    mon_group_id = str(exp_state['copy_id']) + "1"
    mon_entries = MONSTER_DATA.get(mon_group_id, [])

    subwave_spawns = [m for m in mon_entries if m.get('group') == subwave]
    if not subwave_spawns:
        subwave_spawns = [m for m in mon_entries if m.get('group') == ((subwave - 1) % 28) + 1]
    if not subwave_spawns:
        subwave_spawns = mon_entries[:5]

    for m in subwave_spawns:
        cfg = NPC_CONFIG.get(m['nid'], {'name': f"ExpMonster_{m['nid']}"})
        inst_id = send_npc_func(m['nid'], cfg['name'], m['x'], m['z'], m['o'])
        if inst_id and inst_id not in exp_state['active_monsters']:
            exp_state['active_monsters'].append(inst_id)

    print(f"[EXP STAGE] Spawned copy={exp_state['copy_id']} group={exp_state['cur_group']} wave={exp_state['cur_wave']} subwave={subwave} monsters={len(exp_state['active_monsters'])}")

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

def finish_exp_stage(conn, send_rpc_push, picked_char, exp_state, win=True):
    exp_state['ai_active'] = False
    copy_id = exp_state['copy_id']
    char_lv = picked_char.get('level', 1) if picked_char else 1

    full_exp = get_exp_stage_full_reward(char_lv)
    total_max_kills = 350
    ratio = min(1.0, exp_state['total_kills'] / total_max_kills) if total_max_kills > 0 else 1.0

    exp_reward = int(full_exp * ratio) if win else int(full_exp * ratio * 0.5)
    cash_reward = int(exp_reward * 0.1)

    if picked_char:
        rewards = [("2001", 0, exp_reward), ("1001", 0, cash_reward)]
        grant_item_rewards(picked_char, rewards, conn, send_rpc_push)

    res_items = [
        encode_sproto([(0, "2001"), (1, exp_reward), (3, 0)]),
        encode_sproto([(0, "1001"), (1, cash_reward), (3, 0)])
    ]

    # Tag 552: copy_scene_result (subType=12, id=copy_id, win=win, gradeFlag=1, grade=3 if win else 1, items=res_items)
    send_rpc_push(552, encode_sproto([
        (0, 12), (1, copy_id), (2, win), (3, 1), (4, 3 if win else 1), (5, res_items)
    ]))

    if picked_char:
        if win:
            advance_missions(picked_char, send_rpc_push, 'dungeon', target_id='105')
            advance_missions(picked_char, send_rpc_push, 'level')
            saved_pos = picked_char.get('pre_copy_pos')
            saved_map = picked_char.get('pre_copy_map', '11')
            picked_char['pre_copy_pos'] = None
            picked_char['pre_copy_map'] = None

            def leave_exp_copy():
                start_map_transition(conn, picked_char, saved_map, send_rpc_push, override_pos=saved_pos)

            timer = threading.Timer(5.0, leave_exp_copy)
            timer.daemon = True
            timer.start()
        else:
            # Tag 618: notice_relife_player triggers RebirthUIRoot Respawn UI
            death_count = picked_char.get('death_count', 0) + 1
            picked_char['death_count'] = death_count
            relife_cfg = get_relife_config(death_count)
            # Tag 618: notice_relife_player schema: type(0), cost(1), itemId(2), characterid(3), name(4)
            relife_req = encode_sproto([
                (0, relife_cfg.get('id', 1)),
                (1, relife_cfg.get('use_count', 1)),  # cost field - item cost
                (2, "9202"),
                (3, picked_char['id']),
                (4, picked_char['name'])
            ])
            send_rpc_push(618, relife_req)

        picked_char.pop('exp_stage_state', None)

    print(f"[EXP STAGE] Finished copy={copy_id} win={win} total_kills={exp_state['total_kills']} exp={exp_reward} cash={cash_reward}")

def on_npc_killed(conn, send_rpc_push, picked_char, inst_id, npcid):
    if not picked_char: return

    exp_state = picked_char.get('exp_stage_state')
    if exp_state and inst_id:
        active_monsters = exp_state.get('active_monsters', [])
        if inst_id in active_monsters:
            active_monsters.remove(inst_id)
            exp_state['wave_kills'] += 1
            exp_state['total_kills'] += 1

            exp_cfg = DAILY_EXP_CONFIG.get(exp_state['copy_id'], {})
            wave_reqs = exp_cfg.get('group_npc_count', [5, 10, 15, 20])
            req_kills = wave_reqs[min(exp_state['cur_wave'] - 1, len(wave_reqs) - 1)]

            send_rpc_push(683, encode_sproto([
                (0, exp_state['copy_id']),
                (1, exp_state['cur_wave'] - 1),
                (2, exp_state['end_time']),
                (3, exp_state['cur_group']),
                (4, exp_state['wave_kills']),
                (5, exp_state['total_kills'])
            ]))

            def send_npc_wrapper(nid, name, x, z, o):
                global GLOBAL_INST_COUNTER
                npc_stats = get_npc_attr(nid)
                hp_cur = npc_stats['hp_max']
                hp_max = npc_stats['hp_max']
                atk = npc_stats['atk']
                df = npc_stats['def']
                lvl = npc_stats['lv']

                GLOBAL_INST_COUNTER += 1
                i_id = GLOBAL_INST_COUNTER
                NPC_HP_MAP[i_id] = hp_max
                NPC_INST_MAP[i_id] = str(nid)

                final_nid = str(nid)
                if ";" in final_nid:
                    if "XD_A" in final_nid: final_nid = "100"
                    elif "QJ_A" in final_nid: final_nid = "104"
                    elif "NQS_A" in final_nid: final_nid = "105"

                attr = encode_sproto([
                    (0, i_id), (1, final_nid), (2, hp_cur), (3, hp_max), (4, atk), (5, df),
                    (15, x), (16, z), (17, o), (18, lvl), (21, name)
                ])
                ph = encode_sproto([(0, 509)])
                pf = sproto_pack(ph + encode_sproto([(0, attr)]))
                try: conn.sendall(struct.pack(">H", len(pf)) + pf)
                except: pass
                return i_id

            if len(active_monsters) == 0 or exp_state['wave_kills'] >= req_kills:
                if exp_state['cur_wave'] < exp_cfg.get('wave_count', 4):
                    exp_state['cur_wave'] += 1
                    # TAG 515: next_wave - notify client about new wave
                    send_rpc_push(515, encode_sproto([(0, exp_state['cur_wave'])]))
                    spawn_exp_stage_subwave_internal(conn, send_rpc_push, picked_char, exp_state, exp_cfg, send_npc_wrapper)
                else:
                    if exp_state['cur_group'] < exp_cfg.get('group_count', 7):
                        exp_state['cur_group'] += 1
                        exp_state['cur_wave'] = 1
                        # TAG 515: next_wave - notify client about new wave
                        send_rpc_push(515, encode_sproto([(0, exp_state['cur_wave'])]))
                        spawn_exp_stage_subwave_internal(conn, send_rpc_push, picked_char, exp_state, exp_cfg, send_npc_wrapper)
                    else:
                        finish_exp_stage(conn, send_rpc_push, picked_char, exp_state, win=True)

    if npcid and npcid != "None":
        npc_stats = get_npc_attr(npcid)
        npc_lv = npc_stats.get('lv', 1)
        char_lv = picked_char.get('level', 1)

        exp_kill, cash_kill = calculate_npc_kill_rewards(char_lv, npc_lv)
        kill_rewards = [("2001", 0, exp_kill), ("1001", 0, cash_kill)]
        grant_item_rewards(picked_char, kill_rewards, conn, send_rpc_push)

        send_rpc_push(638, encode_sproto([(0, [
            encode_sproto([(0, "2001"), (1, exp_kill), (3, 0)]),
            encode_sproto([(0, "1001"), (1, cash_kill), (3, 0)])
        ])]))

        advance_missions(picked_char, send_rpc_push, 'kill', target_id=npcid)
        advance_missions(picked_char, send_rpc_push, 'level')

        # TAG 527: drop_item_info - spawn a dropped item on the ground from the killed monster
        # drop_item_info schema: serverId(0), pos_x(1), pos_z(2), type(3), item(4), ownServerId(5/tag 7)
        if inst_id:
            player_pos = picked_char.get('pos', [0, 100, 0, 0])
            drop_x = player_pos[0] + random.randint(-500, 500)
            drop_z = player_pos[2] + random.randint(-500, 500)
            global GLOBAL_INST_COUNTER
            GLOBAL_INST_COUNTER += 1
            drop_inst_id = GLOBAL_INST_COUNTER
            # item schema: itemId(0), itemCount(1), quality(2/tag 3), id(3/tag 4), count2(4/tag 5)
            drop_item = encode_sproto([
                (0, "1001"),   # itemId
                (1, cash_kill), # itemCount (stack count)
                (3, 0)          # quality
            ])
            send_rpc_push(527, encode_sproto([
                (0, drop_inst_id),  # serverId
                (1, drop_x),        # pos_x
                (2, drop_z),        # pos_z
                (3, 0),             # type (0 = monster drop)
                (4, drop_item),     # item object
                (7, picked_char['id'])  # ownServerId (encoded as tag 7)
            ]))

def spawn_map_npcs(conn, map_id, picked_char=None):
    """Spawns all NPCs, Monsters, and Traffic defined in data for the map."""
    map_str = str(map_id)
    connection_id = id(conn)
    spawned_maps = NPC_SPAWNED_MAPS.setdefault(connection_id, set())
    if map_str in spawned_maps:
        print(f"[NPC SPAWN] already sent map={map_str} to this connection")
        return
    spawned_maps.add(map_str)

    def send_npc_create(nid, name, x, z, o):
        global GLOBAL_INST_COUNTER
        player_lvl = picked_char.get('level', 1) if picked_char else 1
        npc_stats = get_npc_attr(nid, player_lvl)
        hp_cur = npc_stats['hp_max']
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

        # npc_attribute schema:
        # id(0), npcdataid(1), hp(2), max_hp(3), atk(4), def(5), hit(6), eva(7), cri(8), exd(9), exr(10), res(11), crd(12), crr(13), defa(14),
        # x(15), z(16), o(17), level(18), player_name(21), dgea(24), resa(25), hita(26), cria(27)
        attr = encode_sproto([
            (0, inst_id), (1, final_nid), (2, hp_cur), (3, hp_max), (4, atk), (5, df),
            (6, hit), (7, eva), (8, cri), (9, exd), (10, exr), (11, res), (12, crd), (13, crr), (14, defa),
            (15, x), (16, z), (17, o), (18, lvl), (21, name),
            (24, dgea), (25, resa), (26, hita), (27, cria)
        ])
        ph = encode_sproto([(0, 509)]); pf = sproto_pack(ph + encode_sproto([(0, attr)]))
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass
        return inst_id

    # Check for EXP Stage maps (223..229)
    if map_str in ["223", "224", "225", "226", "227", "228", "229"]:
        exp_cfg = DAILY_EXP_CONFIG.get(map_str, DAILY_EXP_CONFIG.get("223", {
            'wave_count': 4, 'group_npc_count': [5, 10, 15, 20], 'group_count': 7, 'group_time': 60,
            'monsters': [f"{map_str}1", f"{map_str}2", f"{map_str}3", f"{map_str}4"]
        }))
        end_time = int(time.time()) + 600
        exp_state = {
            'copy_id': map_str,
            'cur_group': 1,
            'cur_wave': 1,
            'subwave': 1,
            'wave_kills': 0,
            'total_kills': 0,
            'start_time': int(time.time()),
            'end_time': end_time,
            'active_monsters': [],
            'monster_pos': {},
            'ai_active': True
        }
        if picked_char:
            picked_char['exp_stage_state'] = exp_state

        def push_wrapper(tag, data):
            try:
                ph_p = encode_sproto([(0, tag)])
                pf_p = sproto_pack(ph_p + data)
                conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            except: pass

        push_wrapper(629, encode_sproto([(0, end_time), (1, 0)]))
        push_wrapper(683, encode_sproto([
            (0, map_str), (1, 0), (2, end_time), (3, 1), (4, 0), (5, 0)
        ]))

        if picked_char:
            advance_missions(picked_char, push_wrapper, 'dungeon', target_id='105')
            push_wrapper(519, sync_mission_data(picked_char))

        def local_send_npc(nid, name, x, z, o):
            inst_id = send_npc_create(nid, name, x, z, o)
            if inst_id:
                exp_state['monster_pos'][inst_id] = [x / 100.0, z / 100.0]
            return inst_id

        spawn_exp_stage_subwave_internal(conn, push_wrapper, picked_char, exp_state, exp_cfg, local_send_npc)

        def run_exp_monster_ai():
            if not picked_char or not exp_state.get('ai_active') or picked_char.get('map_id') != map_str:
                return
            if picked_char.get('hp', 0) <= 0:
                return

            player_pos = picked_char.get('pos', [0, 100, 0, 0])
            px = player_pos[0] / 100.0 if abs(player_pos[0]) > 200 else player_pos[0]
            pz = player_pos[2] / 100.0 if abs(player_pos[2]) > 200 else player_pos[2]

            player_stats = get_character_stats(picked_char)

            for inst_id in list(exp_state.get('active_monsters', [])):
                if NPC_HP_MAP.get(inst_id, 0) <= 0:
                    continue
                target_nid = NPC_INST_MAP.get(inst_id)
                if not target_nid:
                    continue

                m_pos = exp_state['monster_pos'].setdefault(inst_id, [6.94, -11.37])
                mx, mz = m_pos[0], m_pos[1]

                dx = px - mx
                dz = pz - mz
                dist = math.sqrt(dx * dx + dz * dz)

                if dist > 2.2:
                    step = min(4.5, dist - 1.5)
                    new_mx = mx + (dx / dist) * step
                    new_mz = mz + (dz / dist) * step
                    exp_state['monster_pos'][inst_id] = [new_mx, new_mz]

                    pos_obj = encode_sproto([
                        (0, int(new_mx * 100)),
                        (1, 0),
                        (2, int(new_mz * 100)),
                        (3, 0)
                    ])
                    m_move = encode_sproto([(0, pos_obj)])
                    char_move = encode_sproto([
                        (0, inst_id),
                        (1, m_move),
                        (2, False)
                    ])
                    push_wrapper(507, encode_sproto([(0, char_move)]))
                else:
                    # TAG 513: aoi_stop_move - NPC stops moving when it reaches player
                    pos_obj_stop = encode_sproto([
                        (0, int(mx * 100)),
                        (1, 0),
                        (2, int(mz * 100)),
                        (3, 0)
                    ])
                    m_move_stop = encode_sproto([(0, pos_obj_stop)])
                    char_move_stop = encode_sproto([
                        (0, inst_id),
                        (1, m_move_stop),
                        (2, False)
                    ])
                    push_wrapper(513, encode_sproto([(0, char_move_stop)]))

                    monster_cfg = NPC_CONFIG.get(target_nid, {})
                    monster_stats = get_npc_attr(target_nid)
                    monster_skill = monster_cfg.get('skill_group', '50001') or '50001'

                    dmg, is_hit, is_cri = get_combat_damage(monster_stats, player_stats, skill_id=monster_skill, skill_lv=1)

                    push_wrapper(508, encode_sproto([(0, inst_id), (1, picked_char['id']), (2, monster_skill)]))

                    if is_hit and dmg > 0:
                        picked_char['hp'] = max(0, picked_char['hp'] - dmg)
                        push_wrapper(128, encode_sproto([(0, picked_char['id']), (1, dmg), (2, monster_skill)]))
                        sync_char_attrs_rpc(conn, picked_char)
                        print(f"[EXP STAGE AI] Monster {inst_id} attacked player for {dmg} damage! Player HP={picked_char['hp']}")

                        if picked_char['hp'] <= 0:
                            print(f"[EXP STAGE AI] Player {picked_char['id']} died in EXP Stage!")
                            finish_exp_stage(conn, push_wrapper, picked_char, exp_state, win=False)
                            return

            if exp_state.get('ai_active') and picked_char.get('map_id') == map_str:
                timer = threading.Timer(0.8, run_exp_monster_ai)
                timer.daemon = True
                timer.start()

        ai_timer = threading.Timer(0.8, run_exp_monster_ai)
        ai_timer.daemon = True
        ai_timer.start()
        return

    # 1. Spawn Static NPCs & Monsters
    # Map 11 (TUTORIAL_CAR) is fully client-side: CitySimController spawns all ambient NPCs,
    # CheckKillTargetMission spawns mission NPCs, and cars spawn at their map-defined positions.
    # Server must NOT spawn anything on map 11 to avoid duplicates and broken positions.
    if map_str != "11" and map_str in STATIC_NPC_DATA:
        for m in STATIC_NPC_DATA[map_str]:
            cfg = NPC_CONFIG.get(m['nid'], {'name': f"NPC_{m['nid']}"})
            send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    if map_str != "11" and map_str in MONSTER_DATA:
        for i, m in enumerate(MONSTER_DATA[map_str]):
            cfg = NPC_CONFIG.get(m['nid'], {'name': f"Monster_{m['nid']}"})
            send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    # 2. Spawn Mission targets defined by the APK data.
    # Map 11 mission targets are spawned by the client. Skip server-side spawning.
    if map_str == "11":
        return
    if picked_char:
        for mid, mdata in picked_char.get('active_missions', {}).items():
            if mdata['state'] == 1:
                cfg = missions_data.get(mid)
                if cfg:
                    logic_id = str(cfg.get('logic_id', ''))
                    # The client looks up KillTargetMissionData by LogicID (matching DataManager.GetKillTargetMissionDataById).
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

def ensure_daily_copy_state(picked_char):
    """Reset the APK-configured daily attempts once per calendar day at 03:00 AM."""
    state = picked_char.setdefault('daily_copy_state', {})
    stamp = current_daily_stamp()
    if state.get('day') != stamp:
        state['day'] = stamp
        state['remaining'] = {}
        state['best_times'] = {}
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
    if copy_id not in remaining or copy_id in ["223", "224", "225", "226", "227", "228", "229"]:
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
            (0, copy_id),
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

        copies[copy_id] = encode_sproto(info_fields)
    return encode_sproto([(0, copies)])

def grant_item_rewards(picked_char, rewards_list, conn=None, send_rpc_push=None):
    """Grants EXP, Cash, and Inventory items, handles level-ups, and syncs attributes."""
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
        while True:
            lv = picked_char.get('level', 1)
            rd = LEVEL_DATA.get(lv)
            if rd and picked_char['exp'] >= rd['exp']:
                picked_char['exp'] -= rd['exp']
                picked_char['level'] = lv + 1
                new_stats = get_character_stats(picked_char)
                picked_char['hp'] = new_stats['hp_max']
                print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']} HP Restored to {picked_char['hp']}")
            else:
                break

    if cash_gained > 0:
        picked_char['cash'] = picked_char.get('cash', 0) + cash_gained

    save_chars(all_accounts_chars)

    if (exp_gained > 0 or cash_gained > 0) and conn:
        sync_char_attrs_rpc(conn, picked_char)

    if inv_changed and send_rpc_push:
        send_rpc_push(611, sync_inventory_data(picked_char))

def give_mission_rewards(picked_char, mid):
    """Resolves rewards by profession and calculates level ups using BaseLvData."""
    try:
        m = missions_data.get(mid)
        if not m or not m.get('reward_ids'): return 0, 0, [], []

        prof = picked_char.get('prof', 0)
        rids = m['reward_ids']
        rid = rids[prof] if prof < len(rids) else rids[0]
        reward = rewards_data.get(rid)
        if not reward: return 0, 0, [], []

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
                # Level Up: Fully Restore HP
                new_stats = get_character_stats(picked_char)
                picked_char['hp'] = new_stats['hp_max']
                print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']} HP Restored to {picked_char['hp']}")
            else: break

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
        return added_exp, added_cash, granted_items, popup_items
    except:
        traceback.print_exc()
        return 0, 0, [], []

def accept_mission_logic(picked_char, mid):
    if mid not in missions_data:
        print(f"[accept_mission_logic] FAILED: {mid} not in missions_data")
        return False
    m = missions_data[mid]
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
        elif logic_type in [102, 103, 105, 106, 107, 108, 110, 113, 114, 117, 119, 120, 132] and event == 'dungeon':
            # Dungeon/Guide entry missions advance on specific 'dungeon' events with matching logic_id
            matched = str(cfg.get('logic_id')) == str(target_id)
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

        send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, progress)]))
        if progress >= required:
            mdata['state'] = 2
            send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
        updated = True

    if updated:
        save_chars(all_accounts_chars)
        send_rpc_push(519, sync_mission_data(picked_char))
    return updated

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
    if is_single_player_map(target_map):
        return
    # Send to all other connections that have a character on the same map
    for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
        if ch and ch.get('id', 0) != target_id and str(ch.get('map_id', '11')) == target_map:
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
    # Skip player-to-player AOI on single-player maps (e.g., Map 11)
    if is_single_player_map(map_id):
        return
    try:
        aoi_data = encode_sproto([(0, char_id)])
        ph_p = encode_sproto([(0, 506)])
        pf_p = sproto_pack(ph_p + aoi_data)
        pkt = struct.pack(">H", len(pf_p)) + pf_p
        for cid, (c, ch) in list(ALL_CONNECTIONS.items()):
            if ch and str(ch.get('map_id', '11')) == map_id:
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

    src_map = picked_char.get('map_id', '11')
    target_map_id = str(target_map_id)
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

        # Broadcast this new player to all other players on the same map
        # Skip player-to-player AOI on single-player maps (e.g., Map 11)
        if not is_single_player_map(target_map_id):
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
            global GLOBAL_INST_COUNTER
            GLOBAL_INST_COUNTER += 1
            boss_inst_id = GLOBAL_INST_COUNTER
            picked_char['boss_inst_id'] = boss_inst_id
            picked_char['boss_pos'] = [400, 120, 0, -9000]

            # Use player level for consistent boss stats
            player_level = picked_char.get('level', 1)
            boss_stats = get_npc_attr("1105", player_level)
            NPC_HP_MAP[boss_inst_id] = boss_stats['hp_max']
            NPC_INST_MAP[boss_inst_id] = "BOSS_" + did
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
    global server_session_counter, ALL_CONNECTIONS, MAIL_ID_COUNTER
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
                CONNECTION_LOCKS[char_id] = threading.Lock()  # Create per-connection lock
                # Update online state for all friends/enemies who have this player in their list
                _sync_friend_online_state(char_id, 1)
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

                        # MULTIPLAYER AOI SYNC - Broadcast this new player to existing players
                        # Skip player-to-player AOI on single-player maps (e.g., Map 11)
                        if not is_single_player_map(mid):
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
                    if mid in ["223", "224", "225", "226", "227", "228", "229"]:
                        exp_state = picked_char.get('exp_stage_state')
                        if exp_state:
                            send_rpc_push(629, encode_sproto([(0, exp_state['end_time']), (1, 0)]))
                            send_rpc_push(683, encode_sproto([
                                (0, exp_state['copy_id']),
                                (1, exp_state['cur_wave'] - 1),
                                (2, exp_state['end_time']),
                                (3, exp_state['cur_group']),
                                (4, exp_state['wave_kills']),
                                (5, exp_state['total_kills'])
                            ]))
                            print(f"[EXP STAGE] map_ready sent 629 & 683 updates for copy={mid}")
                    if mid == "502":
                        # Map 502 exposes its match timer only through this APK tag.
                        send_rpc_push(629, encode_sproto([(0, int(time.time()) + 60), (1, 0)]))

                        def arena_timeout():
                            if picked_char and picked_char.get('map_id') == '502':
                                print('[M1003 DEBUG] Arena 60s timeout reached; returning')
                                schedule_domin_return(restore_hp=True)

                        t = threading.Timer(60.0, arena_timeout)
                        t.daemon = True
                        t.start()

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
                if p_raw and picked_char:
                    pd = decode_sproto(p_raw)
                    picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                    # Persistent save for safety
                    save_chars(all_accounts_chars)
                    # Broadcast movement to other players on the same map (TAG 507)
                    broadcast_aoi_move(picked_char)

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
                    m_entry = picked_char.get('active_missions', {}).get(mid)
                    print(f"[MISSION COMPLETE REQ] mid={mid} entry_exists={m_entry is not None} state={m_entry['state'] if m_entry else 'N/A'}")
                    if m_entry and m_entry['state'] == 2:
                        m_cfg = missions_data.get(mid)
                        if m_cfg:
                            print(f"[MISSION CHAIN] completed={mid} last_main_before={picked_char.get('last_main_mission_id')}")
                            exp_add, cash_add, items_add, popup_items = give_mission_rewards(picked_char, mid)
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
                idx = get_val_int(body, 1); val = get_val_int(body, 2)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    if 0 < idx <= 8:
                        picked_char['active_missions'][mid]['parm'][idx-1] = val
                        save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 523: # set_mission_state
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                state = get_val_int(body, 1)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['state'] = state
                    save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission_data(picked_char))

            elif msg == 130: # skill_level_up
                sid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
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
                sid = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1, '')); tid = get_val_int(body, 0); alist = body.get(3, [])
                if picked_char:
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
                                if ch and ch.get('id', 0) != attacker_id and str(ch.get('map_id', '11')) == attacker_map:
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
                if picked_char and mount_id in MOUNT_CONFIG and color_id in MOUNT_CONFIG[mount_id]['colors']:
                    mounts = picked_char.setdefault('mounts', {})
                    mount = mounts.setdefault(mount_id, {})
                    if int(mount.get('state', 0)) == 2:
                        unlocked = set(str(x) for x in mount.get('unlocked_colors', []))
                        unlocked.add(MOUNT_CONFIG[mount_id]['default_color'])
                        unlocked.add(color_id)
                        mount['unlocked_colors'] = sorted(unlocked, key=lambda x: (len(x), x))
                        mount['select'] = color_id
                        save_chars(all_accounts_chars)
                        # ret_mount_use_color needs the full map as well as the
                        # selected vehicle/colour; omitting the map is the cause
                        # of PlayerCarRootLogic's null-reference crash.
                        send_rpc_push(632, encode_sproto([
                            (0, build_mount_info(picked_char)), (1, mount_id), (2, color_id)
                        ]))
                        sync_main_player_visual(picked_char, send_rpc_push)
                        print(f"[MOUNT] colour selected id={mount_id} color={color_id}")
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
                    mount = picked_char.setdefault('mounts', {}).setdefault(mount_id, {})
                    if int(mount.get('state', 0)) == 0:
                        mount['state'] = 1
                        mount.setdefault('select', MOUNT_CONFIG[mount_id]['default_color'])
                        mount.setdefault('unlocked_colors', [MOUNT_CONFIG[mount_id]['default_color']])
                        save_chars(all_accounts_chars)
                    # ret_buy_car_shop.mountId(0), state(1)
                    send_rpc_push(691, encode_sproto([(0, mount_id), (1, int(mount.get('state', 1)))]))
                    print(f"[MOUNT] garage purchase id={mount_id}")
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

                if picked_char and target_id == picked_char['id']:
                    is_area = (picked_char.get('map_id') == "502")
                    if is_area:
                        # Server-authoritative boss->player damage calculation
                        boss_id = picked_char.get('boss_inst_id')
                        if boss_id:
                            player_level = picked_char.get('level', 1)
                            boss_stats = get_npc_attr("1105", player_level)
                            player_stats = get_character_stats(picked_char)

                            # Use client-sent effinfo_id to determine which boss skill was used
                            # The client sends effinfoId from the skill's effect data (SkillLogic.cs line 781)
                            # Reverse-lookup the skill_id from the effinfo_id, but ONLY allow
                            # effects that belong to the boss's actual skill set (105-110).
                            # This prevents the client from mapping an arbitrary effect to an unrelated skill.
                            boss_allowed_effects = set()
                            for boss_sid in ('101', '105', '106', '107', '108', '109', '110'):
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
                    # For non-arena Tag 128, still calculate server-authoritative damage
                    # The client sends Tag 128 for any single-copy scene, not just Domin map 502
                    if not is_area and picked_char:
                        # Generic NPC attack on player - use basic NPC stats
                        attacker_npc_id = str(target_id) if target_id != picked_char['id'] else "100"
                        npc_stats = get_npc_attr(attacker_npc_id, picked_char.get('level', 1))
                        player_stats = get_character_stats(picked_char)
                        server_dmg, hit, calc_cri = get_combat_damage(
                            npc_stats, player_stats, "101", 1, effinfo_id=effinfo_id
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
                    for d_bytes in dlist:
                        d = decode_sproto(d_bytes)
                        target_id = get_val_int(d, 0)
                        client_dmg = get_val_int(d, 1)
                        # acceptdamge schema: id(0), damage(1), skillId(2), effinfoId(3), cri(4), parm(5-8)
                        skill_id = str(d.get(2, "50001"))
                        # effinfo_id is NOT trusted from client - will be derived server-side from skill_id
                        effinfo_id = None
                        is_cri = get_val_int(d, 4, 0) == 1

                        print(f"[COMBAT] accept_damge target={target_id} client_dmg={client_dmg} skill={skill_id} eff={effinfo_id} cri={is_cri}")

                        # --- Check if target is an online player (PvP) ---
                        target_cid, target_conn, target_char = find_player_connection(target_id)

                        if target_char is not None:
                            # === PvP: target is an online player ===
                            # Server-authoritative damage calculation
                            target_stats = get_character_stats(target_char)

                            # Verify the attacker actually has this skill learned
                            char_skill_levels = picked_char.get('skill_levels', {})
                            if skill_id not in char_skill_levels:
                                # Skill not learned by player, reject and use default basic attack
                                skill_id = "50001"
                                effinfo_id = None
                            skill_lv = char_skill_levels.get(skill_id, 1)
                            if skill_lv < 1:
                                # Skill level invalid, use default
                                skill_id = "50001"
                                skill_lv = 1
                                effinfo_id = None

                            # Server-authoritative effinfo_id: derive from skill_id, not from client
                            if effinfo_id is None:
                                skill_cfg = SKILL_CONFIG.get(skill_id, {})
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

                            # Send TAG 511 (show_damage_board) to attacker
                            send_rpc_push(511, dmg_board)
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
                                # Tag 618: notice_relife_player schema: type(0), cost(1), itemId(2), characterid(3), name(4)
                                relife_req = encode_sproto([
                                    (0, relife_cfg.get('id', 1)),
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
                                    # Tag 618: notice_relife_player schema: type(0), cost(1), itemId(2), characterid(3), name(4)
                                    relife_req = encode_sproto([
                                        (0, relife_cfg.get('id', 1)),
                                        (1, relife_cfg.get('use_count', 1)),  # cost field - item cost
                                        (2, "9202"),
                                        (3, picked_char['id']),
                                        (4, picked_char['name'])
                                    ])
                                    send_rpc_push(618, relife_req)

                            # TAG 511: show_damage_board
                            dmg_item = encode_sproto([
                                (0, target_id),
                                (1, client_dmg),
                                (2, skill_id),
                                (3, effinfo_id),
                                (4, is_cri)
                            ])
                            send_rpc_push(511, encode_sproto([(0, [dmg_item])]))
                            # TAG 514: hit_action
                            send_rpc_push(514, encode_sproto([
                                (0, target_id),
                                (1, picked_char['id']),
                                (2, effinfo_id)
                            ]))

                        else:
                            # === NPC/Monster/Boss damage ===
                            # Map 11 NPCs are fully client-side - skip all server-side HP/death handling
                            # Do NOT advance missions on damage — only on actual death (tag 307)
                            if picked_char and str(picked_char.get('map_id')) == '11':
                                # Still send damage feedback to attacker
                                dmg_item = encode_sproto([
                                    (0, target_id),
                                    (1, client_dmg),
                                    (2, skill_id),
                                    (3, effinfo_id),
                                    (4, is_cri)
                                ])
                                send_rpc_push(511, encode_sproto([(0, [dmg_item])]))
                                send_rpc_push(514, encode_sproto([
                                    (0, target_id),
                                    (1, picked_char['id']),
                                    (2, effinfo_id)
                                ]))
                                continue

                            # Only process server-spawned NPCs (in NPC_HP_MAP or NPC_INST_MAP)
                            if target_id not in NPC_HP_MAP and target_id not in NPC_INST_MAP:
                                # Unknown target, skip but still send feedback
                                dmg_item = encode_sproto([
                                    (0, target_id),
                                    (1, client_dmg),
                                    (2, skill_id),
                                    (3, effinfo_id),
                                    (4, is_cri)
                                ])
                                send_rpc_push(511, encode_sproto([(0, [dmg_item])]))
                                send_rpc_push(514, encode_sproto([
                                    (0, target_id),
                                    (1, picked_char['id']),
                                    (2, effinfo_id)
                                ]))
                                continue

                            # Check if NPC is already dead (Tag 137 may have killed it first)
                            # This prevents processing damage after the boss is already dead
                            if target_id in DEAD_NPC_SET:
                                print(f"[COMBAT] Tag 111 rejected: NPC {target_id} already dead")
                                continue

                            target_nid = NPC_INST_MAP.get(target_id, str(target_id))
                            NPC_INST_MAP[target_id] = target_nid

                            nid_str = "1105" if target_nid.startswith("BOSS_") else target_nid
                            defender_stats = get_npc_attr(nid_str, picked_char.get('level', 1))

                            if target_id not in NPC_HP_MAP:
                                NPC_HP_MAP[target_id] = defender_stats['hp_max']

                            # Server-authoritative NPC damage calculation
                            # Verify the attacker actually has this skill learned
                            char_skill_levels = picked_char.get('skill_levels', {})
                            if skill_id not in char_skill_levels:
                                # Skill not learned by player, reject and use default basic attack
                                skill_id = "50001"
                                effinfo_id = None
                            skill_lv = char_skill_levels.get(skill_id, 1)
                            if skill_lv < 1:
                                # Skill level invalid, use default
                                skill_id = "50001"
                                skill_lv = 1
                                effinfo_id = None

                            # Server-authoritative effinfo_id: derive from skill_id, not from client
                            # This prevents the client from influencing which damage configuration is used
                            if effinfo_id is None:
                                skill_cfg = SKILL_CONFIG.get(skill_id, {})
                                effinfo_id = skill_cfg.get('eff0', "10000")

                            server_dmg, hit, calc_cri = get_combat_damage(
                                attacker_stats, defender_stats, skill_id, skill_lv, effinfo_id=effinfo_id
                            )
                            if not hit:
                                server_dmg = 0
                                calc_cri = False

                            NPC_HP_MAP[target_id] -= server_dmg

                            # Synchronization of target HP to ensure bar update (Tag 510)
                            sync_npc_attrs_rpc(conn, target_id, defender_stats, max(0, NPC_HP_MAP[target_id]))

                            # TAG 511: show_damage_board with server-calculated damage
                            dmg_item = encode_sproto([
                                (0, target_id),
                                (1, server_dmg),
                                (2, skill_id),
                                (3, effinfo_id),
                                (4, calc_cri)
                            ])
                            send_rpc_push(511, encode_sproto([(0, [dmg_item])]))
                            # TAG 514: hit_action with actual effinfoId
                            send_rpc_push(514, encode_sproto([
                                (0, target_id),
                                (1, picked_char['id']),
                                (2, effinfo_id)
                            ]))

                            if NPC_HP_MAP[target_id] <= 0:
                                DEAD_NPC_SET.add(target_id)

                                send_rpc_push(506, encode_sproto([(0, target_id)]))

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

                is_duplicate = False
                if inst_id is not None and inst_id > 1000000:
                    if inst_id in DEAD_NPC_SET: is_duplicate = True
                    else: DEAD_NPC_SET.add(inst_id)

                if inst_id and inst_id in NPC_HP_MAP: del NPC_HP_MAP[inst_id]

                if picked_char and not is_duplicate:
                    if die_type in [2, 6]:
                        advance_missions(picked_char, send_rpc_push, 'car', die_type=die_type)

                    # Map 11 NPCs are fully client-side — do not run server NPC kill rewards
                    # Instead, resolve the actual NPC ID from KillTargetMissionData and advance missions only
                    cur_map = str(picked_char.get('map_id', '11'))
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

                        # Advance kill missions for map 11 without running on_npc_killed rewards
                        advance_missions(picked_char, send_rpc_push, 'kill', target_id=actual_npcid)
                    else:
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
                # RankPVPLocalSceneManager sends this after the APK's zombie
                # opponent has died locally. Treat it as an idempotent arena
                # win fallback; normal damage processing may already have
                # completed the same battle.
                if picked_char and picked_char.get('map_id') == '502':
                    boss_id = picked_char.get('boss_inst_id')
                    if boss_id and boss_id in NPC_HP_MAP and NPC_HP_MAP[boss_id] > 0:
                        NPC_HP_MAP[boss_id] = 0
                        player_level = picked_char.get('level', 1)
                        boss_stats = get_npc_attr('1105', player_level)
                        sync_npc_attrs_rpc(conn, boss_id, boss_stats, 0)
                        did = picked_char.get('active_domin_id', '1')
                        print(f"[M1003 DEBUG] RX 137 zombie died; winning did={did}")
                        # Capture wins update mission progress, not copy_scene_result (552).
                        cap_target = did
                        for act_m, act_mdata in list(picked_char.get('active_missions', {}).items()):
                            cap_cfg = missions_data.get(act_m, {})
                            if cap_cfg.get('logic_type') == 25:
                                cap_target = str(cap_cfg.get('target_id', did))
                                break
                        advance_missions(picked_char, send_rpc_push, 'capture', target_id=cap_target)
                        picked_char['boss_inst_id'] = None
                        DEAD_NPC_SET.add(boss_id)
                    else:
                        print('[M1003 DEBUG] RX 137 ignored; arena win was already processed')
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
                impact_type = get_val_int(body, 0)
                print(f"[*] Vehicle impact with NPC type={impact_type}")
                if picked_char:
                    char_lv = picked_char.get('level', 1)
                    exp_impact, cash_impact = calculate_npc_kill_rewards(char_lv, npc_level=1)
                    impact_rewards = [("2001", 0, exp_impact), ("1001", 0, cash_impact)]
                    grant_item_rewards(picked_char, impact_rewards, conn, send_rpc_push)

                    # Send floating reward popup tip (Tag 638)
                    send_rpc_push(638, encode_sproto([(0, [
                        encode_sproto([(0, "2001"), (1, exp_impact), (3, 0)]),
                        encode_sproto([(0, "1001"), (1, cash_impact), (3, 0)])
                    ])]))

                    advance_missions(picked_char, send_rpc_push, 'impact')
                    advance_missions(picked_char, send_rpc_push, 'level')
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 107: # enter_copy_scene
                copy_id = field_text(body, 0)
                cfg = COPY_SCENE_CONFIG.get(copy_id)
                if picked_char and copy_id in ["223", "224", "225", "226", "227", "228", "229"]:
                    remaining = copy_attempts_remaining(picked_char, copy_id, cfg or {'max_plays': 3})
                    if remaining > 0:
                        if picked_char.get('pre_copy_pos') is None:
                            picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                            picked_char['pre_copy_map'] = str(picked_char.get('map_id', '11'))
                        state = ensure_daily_copy_state(picked_char)
                        state['remaining'][copy_id] = remaining - 1
                        picked_char['active_copy_id'] = copy_id
                        save_chars(all_accounts_chars)
                        send_rpc_push(555, sync_copy_scenes(picked_char))
                        advance_missions(picked_char, send_rpc_push, 'dungeon', target_id=copy_id)
                        start_map_transition(conn, picked_char, copy_id, send_rpc_push)
                        print(f"[EXP STAGE] entered id={copy_id} remaining={remaining - 1}")
                    else:
                        print(f"[EXP STAGE] denied id={copy_id}; daily attempts exhausted")
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
                        save_chars(all_accounts_chars)
                        start_map_transition(conn, picked_char, cfg['map_id'], send_rpc_push)
                        send_rpc_push(555, sync_copy_scenes(picked_char))
                        advance_missions(picked_char, send_rpc_push, 'dungeon', target_id=copy_id)
                        print(f"[STREET RACE] entered id={copy_id} remaining={remaining - 1}/{cfg['max_plays']}")
                    else:
                        print(f"[STREET RACE] denied id={copy_id}; daily attempts exhausted")
                elif picked_char:
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
                if picked_char and cfg and cfg['subtype'] == 7:
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
                        advance_missions(picked_char, send_rpc_push, 'level')
                    send_rpc_push(555, sync_copy_scenes(picked_char))
                    schedule_street_race_return()
                    print(f"[STREET RACE] result id={active_copy_id} win={won} time={elapsed} rewards={rewards}")
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
                    # Read isInplace from client request: field 0 = bool
                    try:
                        body_raw = body.get(0, b"")
                        relife_req = decode_sproto(body_raw) if body_raw else {}
                        is_inplace = bool(relife_req.get(0, False))
                    except Exception:
                        is_inplace = False

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

                    # Consume revive item from inventory
                    # The revive item ID is "9202" (from Tag 618)
                    revive_item_id = "9202"
                    inventory = picked_char.get('inventory', {})
                    if revive_item_id in inventory:
                        item_count = inventory[revive_item_id]
                        if isinstance(item_count, dict):
                            item_count = item_count.get('count', 1)
                        inventory[revive_item_id] = max(0, item_count - 1)
                        if inventory[revive_item_id] <= 0:
                            del inventory[revive_item_id]
                        picked_char['inventory'] = inventory
                        print(f"[REVIVE] Consumed revive item {revive_item_id}")

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
                    if not is_single_player_map(map_id):
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
                if picked_char and picked_char.get('exp_stage_state'):
                    exp_state = picked_char['exp_stage_state']
                    send_rpc_push(629, encode_sproto([(0, exp_state['end_time']), (1, 0)]))
                    send_rpc_push(683, encode_sproto([
                        (0, exp_state['copy_id']),
                        (1, exp_state['cur_wave'] - 1),
                        (2, exp_state['end_time']),
                        (3, exp_state['cur_group']),
                        (4, exp_state['wave_kills']),
                        (5, exp_state['total_kills'])
                    ]))
                    print(f"[EXP STAGE] start_battle sent 629 & 683 updates for copy={exp_state['copy_id']}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [106, 246, 273, 207, 201, 322]:
                # Scene/Dungeon Entry
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[RX] Scene Entry: {mid} (MSG={msg})")
                if picked_char:
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
                if picked_char and not picked_char.get('download_complete'):
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

                    send_rpc_push(654, encode_sproto([(0, 1)])) # start_enter_game state=1
                    print(f"[REWARD] Expansion finalized and rewards granted for player {picked_char['id']}")
                elif picked_char:
                    print(f"[REWARD] Player {picked_char['id']} already claimed expansion rewards.")
                    # Keep the current client session consistent with the
                    # persisted completion state if it repeats MSG 270.
                    send_rpc_push(654, encode_sproto([(0, 1)]))

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

            elif msg == 110: # chat
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

            elif msg in (109, 161, 163, 166): # team requests: req_invite_team, req_join_team, update_team_setting, leave_team
                # TAG 516: invite_join_team - sent when someone invites you to team
                # TAG 517: req_invite_team_result - result of invite acceptance
                # TAG 518: update_team - team state update
                # update_team.request schema: team(0) - where team has id(0), teamleader(1), count(2),
                #   isVerfiy(3), teammembers(4), goalId(5), minLevel(6), maxLevel(7), recruit(8)
                if picked_char and msg == 109:
                    # Invite sent - send invite to target and result back
                    target_id = get_val_int(body, 0)
                    # TAG 516: invite_join_team - notify the invited player
                    # invite_join_team.request: teamid(0), member(1), goalId(2)
                    # teammember schema: id(0), teamid(1), name(2), level(3)
                    member_info = encode_sproto([
                        (0, picked_char['id']),
                        (2, picked_char['name']),
                        (3, picked_char.get('level', 1))
                    ])
                    send_rpc_push(516, encode_sproto([
                        (0, 0),           # teamid
                        (1, member_info),  # member
                        (2, "")            # goalId
                    ]))
                    # TAG 517: req_invite_team_result - ok(0), id(1)
                    send_rpc_push(517, encode_sproto([(0, 0), (1, target_id)]))
                    # TAG 518: update_team - send proper team object
                    team_obj = encode_sproto([
                        (0, 0),   # id
                        (2, 0),   # count
                        (4, {})   # teammembers (empty dict)
                    ])
                    send_rpc_push(518, encode_sproto([(0, team_obj)]))
                elif picked_char and msg == 161:
                    # Join team request - send result
                    teamid = get_val_int(body, 0)
                    # TAG 517: req_invite_team_result - ok(0), id(1)
                    send_rpc_push(517, encode_sproto([(0, 0), (1, picked_char['id'])]))
                    team_obj = encode_sproto([
                        (0, teamid),
                        (2, 0),   # count
                        (4, {})   # teammembers
                    ])
                    send_rpc_push(518, encode_sproto([(0, team_obj)]))
                elif picked_char and msg == 166:
                    # Leave team - send empty team update
                    team_obj = encode_sproto([
                        (0, 0),
                        (2, 0),
                        (4, {})
                    ])
                    send_rpc_push(518, encode_sproto([(0, team_obj)]))
                elif picked_char and msg == 163:
                    # Update team setting
                    team_obj = encode_sproto([
                        (0, 0),
                        (2, 0),
                        (4, {})
                    ])
                    send_rpc_push(518, encode_sproto([(0, team_obj)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

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
                    day = int(picked_char.get('sign_week_day', 1))
                    claimed = bool(picked_char.get('sign_week_claimed', False))
                    send_rpc_push(641, encode_sproto([(0, day), (1, not claimed), (2, False)]))

                elif msg == 255 and picked_char: # sign_week
                    day = int(picked_char.get('sign_week_day', 1))
                    picked_char['sign_week_claimed'] = True
                    picked_char['cash'] = picked_char.get('cash', 0) + 10000
                    save_chars(all_accounts_chars)
                    send_rpc_push(643, encode_sproto([(0, day), (1, False)]))
                    send_rpc_push(611, sync_inventory_data(picked_char))
                    sync_char_attrs_rpc(conn, picked_char)

                elif msg == 252 and picked_char: # request_sign_30_day_info
                    cur_day = int(picked_char.get('sign_30_day', 0))
                    claimed = bool(picked_char.get('sign_30_claimed', False))
                    send_rpc_push(640, encode_sproto([(0, cur_day), (1, max(1, cur_day)), (2, 0), (3, not claimed), (4, False), (5, 30), (6, "")]))

                elif msg == 254 and picked_char: # sign_30_day
                    cur_day = int(picked_char.get('sign_30_day', 0)) + 1
                    picked_char['sign_30_day'] = cur_day
                    picked_char['sign_30_claimed'] = True
                    picked_char['cash'] = picked_char.get('cash', 0) + 20000
                    save_chars(all_accounts_chars)
                    send_rpc_push(642, encode_sproto([(0, cur_day), (1, cur_day), (2, 0), (3, False), (4, False), (5, 30), (6, "")]))
                    send_rpc_push(611, sync_inventory_data(picked_char))
                    sync_char_attrs_rpc(conn, picked_char)

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
                print("[M1003 DEBUG] RX 310 request_domin_info")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

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
                    # CitySimController creates the Dominance zombie from
                    # ret_domin_info.character_look and reads title_level.
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

                print("[M1003 DEBUG] TX 684 ret_domin_info domin_id=1 state=0")
                send_rpc_push(684, resp_p)

            elif msg == 178: # update_misison_parm
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                if picked_char:
                    # Survey interaction sends msg 178
                    advance_missions(picked_char, send_rpc_push, 'interact', target_id=mid)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 179: # update_misison_complete
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0, ''))
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['state'] = 2
                    save_chars(all_accounts_chars)
                    send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                    send_rpc_push(519, sync_mission_data(picked_char))
                    print(f"[MISSION DIALOG COMPLETE] mission_id={mid}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 119: # ask_pickup_item
                iid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                if picked_char:
                    advance_missions(picked_char, send_rpc_push, 'pickup', target_id=iid)
                    advance_missions(picked_char, send_rpc_push, 'interact', target_id=iid)
                    add_to_inventory(picked_char, iid, 1)
                    send_rpc_push(611, sync_inventory_data(picked_char))
                if session is not None:
                    resp = encode_sproto([(0, 0)])
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 108: # leave_copy_scene
                if picked_char:
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
                    start_map_transition(conn, picked_char, saved_map, send_rpc_push, override_pos=saved_pos)
                    save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            # === SOCIAL UI HANDLERS ===

            elif msg == 122: # send_mail
                if picked_char:
                    receive_id = get_val_int(body, 0)
                    context_raw = body.get(1, b"")
                    if isinstance(context_raw, bytes):
                        context = context_raw.decode('utf-8', errors='ignore')
                    else:
                        context = str(context_raw)
                    mail_id = MAIL_ID_COUNTER
                    MAIL_ID_COUNTER += 1
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
                        for mi in picked_char.get('mails', []):
                            if mi.get('mailId') == mail_id:
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
                            import traceback
                            traceback.print_exc()

                    async_thread = threading.Thread(target=_add_friend_async, daemon=True)
                    async_thread.start()
                    print(f"[FRIEND] add_friend dispatched to async thread for target={target_id}")
                except Exception as e:
                    print(f"[FRIEND] ERROR in add_friend handler: {e}")
                    import traceback
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
                    mail_id = MAIL_ID_COUNTER
                    MAIL_ID_COUNTER += 1
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
            # Broadcast aoi_remove before removing from tracking
            if picked_char:
                broadcast_aoi_remove(picked_char.get('id', 0), picked_char.get('map_id', '11'))
            RANDOM_NAME_CALLED.discard(conn_id)
            if picked_char:
                char_id = picked_char.get('id', 0)
                ALL_CONNECTIONS.pop(char_id, None)  # Remove from online tracking
                CONNECTION_LOCKS.pop(char_id, None)  # Remove connection lock
                # Update offline state for all friends/enemies who have this player
                _sync_friend_online_state(char_id, 0)
            conn.close()
        except Exception:
            pass
        print(f"[-] Client disconnected: {addr}")

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
