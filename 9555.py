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
ONLINE_CHAR_MAP = {} # char_id -> send_rpc_push

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
EFF_CONFIG = {}   # effId -> effect info template
SKILL_CONFIG = {} # skillId -> skill info template
MOUNT_CONFIG = {} # garage vehicle id -> client MountData definition
COPY_SCENE_CONFIG = {} # daily-copy id -> CopySceneData fields used by the APK
SHOW_REWARD_CONFIG = {} # ShowRewardData id -> exact visible item list
STREET_RACE_REWARD_BY_LEVEL = {} # level -> AdaptData _drop_bc ShowRewardData id
ITEM_CONFIG = {} # itemId -> {type, subtype, function}

try:
    script_dir = os.path.dirname(__file__)
    md_path = os.path.join(script_dir, "missions.json")
    rd_path = os.path.join(script_dir, "mission_rewards.json")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f: missions_data = json.load(f)
    if os.path.exists(rd_path):
        with open(rd_path, "r", encoding='utf-8') as f: rewards_data = json.load(f)

    def is_data(line): return line.startswith("*,") or ("," in line and line.split(",")[1].isdigit())

    text_asset_root = os.path.join(script_dir, "assets", "Bundle", "TextAsset")
    if not os.path.isdir(text_asset_root):
        text_asset_root = os.path.join(script_dir, "assets", "Bundle", "TextAssets")

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
    kt_path = os.path.join(text_asset_root, "KillTargetMissionData")
    if os.path.exists(kt_path):
        with open(kt_path, "r", encoding='utf-8') as f:
            for line in f:
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
    tc_path = os.path.join(text_asset_root, "TargetCarMissionData")
    if os.path.exists(tc_path):
        with open(tc_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 6 and parts[1].isdigit():
                    mid = parts[1]
                    if mid not in TARGET_CAR_SPAWNS: TARGET_CAR_SPAWNS[mid] = []
                    TARGET_CAR_SPAWNS[mid].append({
                        'map': parts[2],
                        'x': int(float(parts[3])),
                        'z': int(float(parts[4])),
                        'car_id': parts[6] # e.g. "Chevrolet"
                    })
        print(f"[TARGET CAR DATA LOADED] count={len(TARGET_CAR_SPAWNS)}")

    mount_path = os.path.join(text_asset_root, "MountData")
    if os.path.exists(mount_path):
        with open(mount_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 36 and parts[0] == "*" and parts[1]:
                    if parts[36] == "1":
                        colors = [x for x in parts[28].split("#") if x]
                        MOUNT_CONFIG[parts[1]] = {
                            'colors': colors,
                            'default_color': parts[29] if parts[29] else (colors[0] if colors else "1"),
                            'item_id': parts[4]
                        }
        print(f"[MOUNT CONFIG LOADED] garage_vehicles={len(MOUNT_CONFIG)}")

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
        if street_reward_index >= 0:
            for parts in rows:
                if len(parts) > street_reward_index and parts[0] == '*' and parts[1].isdigit():
                    STREET_RACE_REWARD_BY_LEVEL[int(parts[1])] = parts[street_reward_index]

    item_path = os.path.join(text_asset_root, "ItemData")
    if os.path.exists(item_path):
        with open(item_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(',')
                if len(parts) > 11 and parts[0] == '*' and parts[1]:
                    ITEM_CONFIG[parts[1]] = {
                        'type': int(parts[7]) if parts[7].isdigit() else 0,
                        'subtype': int(parts[9]) if len(parts) > 9 and parts[9].isdigit() else 0,
                        'function': int(parts[13]) if len(parts) > 13 and parts[13].isdigit() else 0
                    }
        print(f"[ITEM CONFIG LOADED] items={len(ITEM_CONFIG)}")
except: traceback.print_exc()

BAK_DB = CHAR_DB + ".bak"
TMP_DB = CHAR_DB + ".tmp"

def load_chars():
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

    normalized = {}
    for area_k, acc_dict in data.items():
        if isinstance(acc_dict, dict):
            normalized[str(area_k)] = acc_dict
    return normalized

def get_account_chars(all_chars, area_id, acc_id):
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
    if not isinstance(data, dict):
        return
    if not data:
        if os.path.exists(CHAR_DB) and os.path.getsize(CHAR_DB) > 10:
            print("[SAVE GUARD] Refusing to overwrite non-empty CHAR_DB with empty dictionary!")
            return
    try:
        with open(TMP_DB, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=4)
            f.flush()
            os.fsync(f.fileno())

        if os.path.exists(CHAR_DB) and os.path.getsize(CHAR_DB) > 10:
            try:
                with open(CHAR_DB, "r", encoding="utf-8") as src, open(BAK_DB, "w", encoding="utf-8") as dst:
                    dst.write(src.read())
                    dst.flush()
                    os.fsync(dst.fileno())
            except: pass

        os.replace(TMP_DB, CHAR_DB)
    except Exception as e:
        print(f"[ERROR] Failed atomic save_chars: {e}")

all_accounts_chars = load_chars()

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

def decode_sproto_list(data):
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
    if mount_id:
        fields.extend([(12, str(mount_id)), (13, int(mount_state)),
                       (14, str(mount_color or ''))])
    return encode_sproto(fields)

def get_equipped_mount(c):
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
    character = encode_sproto([
        (0, int(picked_char['id'])),
        (4, build_main_player_visual(picked_char))
    ])
    send_rpc_push(510, encode_sproto([(0, character)]))

def get_boss_char(inst_id, did):
    name = "XK7NQ2VJ"
    prof = 0
    lv = 3
    hp_max = 9560
    power = 6000
    atk = 660
    df = 60

    v = get_visual(name, prof)
    attr_oth = encode_sproto([
        (0, hp_max), (1, 0), (2, lv), (3, power), (4, 1), (15, 2)
    ])
    pos_data = encode_sproto([(0, 400), (1, 120), (2, 0), (3, -9000)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])

    boss_skill_levels = {
        "101": 1,
        "105": 1, "106": 1, "107": 1,
        "108": 1, "109": 1, "110": 1,
    }
    skills_map = build_skills_map(prof, 25, boss_skill_levels)
    attr_run = encode_sproto([(0, hp_max), (2, atk), (3, df)])

    ld = LEVEL_DATA.get(3, LEVEL_DATA.get(1))
    attr_all_data = [
        (0, hp_max), (2, atk), (3, df),
        (4, ld['hit'][0]), (5, ld['eva'][0]), (6, ld['cri'][0]), (7, ld['res'][0]),
        (8, ld['exd'][0]), (9, ld['exr'][0]), (10, ld['crd'][0]), (11, ld['crr'][0]),
        (12, ld['defa']), (13, 700), (14, 100),
        (17, ld['dgea']), (18, ld['resa']), (19, ld['hita']), (20, ld['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    return encode_sproto([
        (0, inst_id),
        (1, encode_sproto([(0, name), (1, prof), (2, 1), (3, "502"), (4, 1)])),
        (2, attr_oth),
        (6, v),
        (7, mv),
        (8, skills_map),
        (13, run)
    ])

PROF_SKILLS = {
    0: {"atk": ["101", "102", "103"], "dodge": "104", "actives": ["105", "106", "107", "108", "109", "110"]},
    1: {"atk": ["201", "202", "203"], "dodge": "204", "actives": ["205", "206", "207", "208", "209", "210"]},
    2: {"atk": ["301", "302", "303"], "dodge": "304", "actives": ["305", "306", "307", "308", "309", "310"]}
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

    atk_skills = p["atk"] if isinstance(p["atk"], list) else [p["atk"]]
    for sid in atk_skills:
        smap[sid] = encode_sproto([(0, sid), (1, skill_levels.get(sid, 0)), (2, 0), (3, 1), (4, 0), (5, False)])

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
        (4, c.get('tutorial', 0))
    ])

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_level_data(level):
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

def get_character_stats(c):
    lv = c.get('level', 1)
    prof = c.get('prof', 0)
    ld = get_level_data(lv)

    atk = ld['atk'][prof]
    hp_max = ld['hp'][prof]
    df = ld['def'][prof]
    hit = ld['hit'][prof]
    eva = ld['eva'][prof]
    cri = ld['cri'][prof]
    res = ld['res'][prof]

    atk += 180

    coeffs = [
        {"atk":16, "hp":1, "def":11, "hit":2, "eva":5.5, "cri":10, "res":10},
        {"atk":20, "hp":1, "def":12, "hit":1, "eva":6, "cri":5, "res":10},
        {"atk":7, "hp":1, "def":7.4, "hit":3, "eva":3.7, "cri":15, "res":10}
    ][prof]

    raw_power = (atk * coeffs['atk'] + hp_max * coeffs['hp'] + df * coeffs['def'] +
                 hit * coeffs['hit'] + eva * coeffs['eva'] + cri * coeffs['cri'] + res * coeffs['res'])
    power = int(raw_power * 3.0)

    return {
        'atk': atk, 'hp_max': hp_max, 'def': df,
        'hit': hit, 'eva': eva, 'cri': cri, 'res': res,
        'power': power, 'lv': lv, 'exp': c.get('exp', 0),
        'defa': ld['defa'], 'dgea': ld['dgea'], 'resa': ld['resa'],
        'hita': ld['hita'], 'cria': ld['cria'],
        'exd': ld['exd'][prof], 'exr': ld['exr'][prof], 'crd': ld['crd'][prof], 'crr': ld['crr'][prof]
    }

def get_char_ov(c, sort_index=None):
    gen = get_general(c)
    stats = get_character_stats(c)
    attr = encode_sproto([(0, stats['lv']), (1, stats['power'])])
    ctime = sort_index if sort_index is not None else c.get('createtime', int(time.time()))
    return encode_sproto([
        (0, c['id']),
        (1, gen),
        (2, attr),
        (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))),
        (4, ctime),
        (5, 0)
    ])

def get_equip_slot_index(subtype):
    """Map ItemData.SubType (0=WEAPON, 1=HEAD, 2=BODY, 3=LEG, 4=BELT, 5=NECKLACE) to container slot index."""
    m = {0: 5, 1: 0, 2: 1, 3: 3, 4: 2, 5: 4}
    return m.get(int(subtype), 5)

def encode_gameitem_sproto(item):
    """Encode gameitem object for Sproto serialization."""
    if not item or not isinstance(item, dict): return None
    fields = []
    if 'indexId' in item: fields.append((0, int(item['indexId'])))
    if 'itemId' in item: fields.append((1, str(item['itemId'])))
    if 'bindflag' in item: fields.append((2, bool(item['bindflag'])))
    if 'quality' in item: fields.append((3, int(item['quality'])))
    if 'level' in item: fields.append((4, int(item['level'])))
    if 'stack' in item: fields.append((5, int(item['stack'])))
    if 'parm' in item: fields.append((6, [int(x) for x in item['parm']]))
    if 'appraise' in item: fields.append((7, int(item['appraise'])))
    return encode_sproto(fields)

def sync_backpack_item_rpc(picked_char):
    """Build Sproto Tag 592 (sync_backpack_item) for Equipment Backpack."""
    items = {}
    bp = picked_char.get('equip_backpack', {})
    for index_id, item in bp.items():
        encoded = encode_gameitem_sproto(item)
        if encoded:
            items[int(index_id)] = encoded
    return encode_sproto([(0, items)])

def sync_badgepack_item_rpc(picked_char):
    """Build Sproto Tag 604 (sync_badgepack_item) for Badge Backpack."""
    items = {}
    bp = picked_char.get('badge_backpack', {})
    for index_id, item in bp.items():
        encoded = encode_gameitem_sproto(item)
        if encoded:
            items[int(index_id)] = encoded
    return encode_sproto([(0, items)])

def sync_fashion_backpack_item_rpc(picked_char):
    """Build Sproto Tag 616 (sync_fashion_backpack_item) for Fashion Backpack."""
    items = {}
    bp = picked_char.get('fashion_backpack', {})
    for index_id, item in bp.items():
        encoded = encode_gameitem_sproto(item)
        if encoded:
            items[int(index_id)] = encoded
    return encode_sproto([(0, items)])

def sync_item_pack_rpc(picked_char):
    """Build Sproto Tag 611 (sync_item_pack) for Item Backpack."""
    items = {}
    ibp = picked_char.get('item_backpack', {})
    if ibp:
        for index_id, item in ibp.items():
            encoded = encode_gameitem_sproto(item)
            if encoded:
                items[int(index_id)] = encoded
    else:
        inv = picked_char.get('inventory', [])
        for i in range(len(inv)):
            item = inv[i]
            guid = i + 10000
            items[guid] = encode_sproto([
                (0, guid),
                (1, str(item['id'])),
                (2, True),
                (5, int(item['amount']))
            ])
    return encode_sproto([(0, items)])

def send_update_item_push(send_rpc_push, container_type, index_id, item_dict=None):
    """Send Sproto Tag 525 (update_item) to client."""
    fields = [
        (0, int(container_type)),
        (1, int(index_id))
    ]
    if item_dict:
        fields.append((2, encode_gameitem_sproto(item_dict)))
    send_rpc_push(525, encode_sproto(fields))

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
    
    # Tag 9: equip (Dictionary<long, gameitem>)
    equip_map = {}
    epack = c.get('equip_pack', {})
    if epack:
        for slot, item in epack.items():
            if item and isinstance(item, dict):
                encoded = encode_gameitem_sproto(item)
                if encoded:
                    equip_map[int(item.get('indexId', slot))] = encoded
    if not equip_map:
        wid = "10001" if c.get('prof', 0) == 0 else "20001" if c.get('prof', 0) == 1 else "30001"
        w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, 1), (4, 1), (5, 1), (6, [0]*8), (7, 1)])
        equip_map = {5: w1}

    # Tag 10: badge_equip (Dictionary<long, gameitem>)
    badge_equip_map = {}
    bpack = c.get('badge_equip_pack', {})
    if bpack:
        for pos, item in bpack.items():
            if item and isinstance(item, dict):
                encoded = encode_gameitem_sproto(item)
                if encoded:
                    badge_equip_map[int(item.get('indexId', pos))] = encoded

    # Tag 11: fashion_equip (Dictionary<long, gameitem>)
    fashion_equip_map = {}
    fpack = c.get('fashion_equip_pack', {})
    if fpack:
        for slot, item in fpack.items():
            if item and isinstance(item, dict):
                encoded = encode_gameitem_sproto(item)
                if encoded:
                    fashion_equip_map[int(item.get('indexId', slot))] = encoded

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
        (10, badge_equip_map),
        (11, fashion_equip_map),
        (12, 0),
        (13, run),
        (15, download_state)
    ])

def sync_common_data_rpc(picked_char):
    """Build Sproto Tag 592 (sync_common_data) with tutorial function state dic."""
    func_info_map = {}
    if picked_char and picked_char.get('tutorial', 0) == 1:
        for fid in range(1, 100):
            func_info_map[str(fid)] = encode_sproto([(0, str(fid)), (1, 1)])
    elif picked_char:
        saved_func = picked_char.get('func_info', {})
        for fid, fstate in saved_func.items():
            func_info_map[str(fid)] = encode_sproto([(0, str(fid)), (1, int(fstate))])

    sync_fields = [
        (0, int(time.time())),
        (1, 0),
        (2, int(time.time()) + 86400),
        (3, 10000),
        (4, 0),
        (5, 0),
        (6, 0),
        (7, 0),
        (8, func_info_map),
        (9, 0)
    ]
    return encode_sproto(sync_fields)

def sync_dance_state_rpc(picked_char=None):
    now = int(time.time())
    dance_info_obj = encode_sproto([
        (0, 101),
        (1, "101"),
        (2, now),
        (3, now + 1800),
        (4, 1),
        (5, 0),
        (6, 1800),
        (7, now + 86400)
    ])
    dance_map = {101: dance_info_obj}
    return encode_sproto([
        (0, 1),
        (1, 1),
        (2, dance_map)
    ])

def sync_char_attrs_rpc(conn, picked_char):
    stats = get_character_stats(picked_char)
    hp_cur = picked_char.get('hp', stats['hp_max'])

    attr_oth = encode_sproto([
        (0, hp_cur), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)
    ])
    attr_base = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
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
    cfg = NPC_CONFIG.get(str(nid))
    if not cfg:
        return {
            'hp_max': 10000, 'atk': 100, 'def': 10, 'hit': 100, 'eva': 10, 'cri': 10, 'res': 10,
            'lv': 1, 'defa': 3000, 'dgea': 6000, 'resa': 3000, 'hita': 300, 'cria': 3000,
            'exd': 0, 'exr': 0, 'crd': 5000, 'crr': 0, 'power': 3000
        }

    lvl = cfg.get('level', 1)
    max_lv = max(LEVEL_DATA.keys()) if LEVEL_DATA else 1
    effective_lv = min(lvl, max_lv)
    ld = LEVEL_DATA.get(effective_lv, LEVEL_DATA.get(1, {}))

    if cfg.get('is_abs'):
        hp = cfg.get('hp_abs', 10000)
        atk = cfg.get('atk_abs', 100)
        df = cfg.get('def_abs', 10)
        hit = cfg.get('hit_abs', 100)
        eva = cfg.get('eva_abs', 10)
        cri = cfg.get('cri_abs', 10)
        res = cfg.get('res_abs', 10)
        exd = cfg.get('exd_abs', 0)
        exr = cfg.get('exr_abs', 0)
        crd = cfg.get('crd_abs', 5000)
        crr = cfg.get('crr_abs', 0)
        defa = cfg.get('defa_abs', 3000)
        dgea = cfg.get('dgea_abs', 6000)
        resa = cfg.get('resa_abs', 3000)
        hita = cfg.get('hita_abs', 300)
        cria = cfg.get('cria_abs', 3000)
    else:
        hp = (ld['hp'][0] * cfg.get('hp_coe', 10000)) // 10000
        atk = (ld['atk'][0] * cfg.get('atk_coe', 10000)) // 10000
        df = (ld['def'][0] * cfg.get('def_coe', 10000)) // 10000
        hit = (ld['hit'][0] * cfg.get('hit_coe', 10000)) // 10000
        eva = (ld['eva'][0] * cfg.get('eva_coe', 10000)) // 10000
        cri = (ld['cri'][0] * cfg.get('cri_coe', 10000)) // 10000
        res = (ld['res'][0] * cfg.get('res_coe', 10000)) // 10000
        exd = (ld['exd'][0] * cfg.get('exd_coe', 10000)) // 10000
        exr = (ld['exr'][0] * cfg.get('exr_coe', 10000)) // 10000
        crd = (ld['crd'][0] * cfg.get('crd_coe', 10000)) // 10000
        crr = (ld['crr'][0] * cfg.get('crr_coe', 10000)) // 10000
        defa = (ld.get('defa', 3000) * cfg.get('defa_coe', 10000)) // 10000
        dgea = (ld.get('dgea', 6000) * cfg.get('dgea_coe', 10000)) // 10000
        resa = (ld.get('resa', 3000) * cfg.get('resa_coe', 10000)) // 10000
        hita = (ld.get('hita', 300) * cfg.get('hita_coe', 10000)) // 10000
        cria = (ld.get('cria', 3000) * cfg.get('cria_coe', 10000)) // 10000

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
        'power': power
    }

def sync_npc_attrs_rpc(conn, inst_id, stats, hp_cur):
    attr_other_fields = [(0, hp_cur), (2, stats['lv'])]
    if NPC_INST_MAP.get(inst_id, '').startswith('BOSS_'):
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

def get_combat_damage(attacker_stats, defender_stats, skill_id, skill_lv, is_area=False, pvp_scale=1.0):
    prefix = "[AREA DAMAGE]" if is_area else "[COMBAT]"

    skill_cfg = SKILL_CONFIG.get(skill_id, {})
    eff_id = skill_cfg.get('eff0', "10000")
    eff_cfg = EFF_CONFIG.get(eff_id, {
        'dmg_fixed': 0, 'dmg_fixed_add': 0, 'dmg_multi': 10000, 'dmg_multi_add': 0, 'adds': {}
    })

    skill_damage = eff_cfg['dmg_fixed'] + eff_cfg['dmg_fixed_add'] * skill_lv
    skill_scale = (eff_cfg['dmg_multi'] + (eff_cfg['dmg_multi_add'] or 0) * skill_lv) / 10000.0

    pvp_mult = pvp_scale
    if attacker_stats.get('power', 0) > defender_stats.get('power', 0):
        pvp_mult += 0.05

    skill_shit = eff_cfg['adds'].get(3001, 0) / 10000.0
    hit_p = min((attacker_stats['hit'] + 1.0) / (attacker_stats['hita'] + attacker_stats['hit'] + 1.0), 1.0)
    dge_p = min((defender_stats['eva'] + 1.0) / (defender_stats['dgea'] + defender_stats['eva'] + 1.0), 0.5)

    hit_prob = 1.0
    roll_hit = random.random()

    if is_area:
        print(f"{prefix} HIT CHECK: roll={roll_hit:.3f} prob={hit_prob:.3f} (hit_p={hit_p:.3f}, dge_p={dge_p:.3f}, skill={skill_shit:.3f})")
        print(f"{prefix} STATS: AtkHIT={attacker_stats['hit']} AtkHITA={attacker_stats['hita']} DefEVA={defender_stats['eva']} DefDGEA={defender_stats['dgea']}")

    if roll_hit > hit_prob:
        if is_area: print(f"{prefix} RESULT: MISS")
        return 0, False, False

    skill_scri = eff_cfg['adds'].get(3002, 0) / 10000.0
    cri_p = min((attacker_stats['cri'] + 1.0) / (attacker_stats['cri'] + attacker_stats['cria'] + 1.0), 0.9)
    res_p = min((defender_stats['res'] + 1.0) / (defender_stats['res'] + defender_stats['resa'] + 1.0), 0.8)

    cri_prob = cri_p - res_p + skill_scri
    is_cri = random.random() < cri_prob

    scaled_damage = skill_damage * pvp_mult
    scaled_scale = skill_scale * pvp_mult

    base_dmg = attacker_stats['atk'] * scaled_scale + scaled_damage
    def_red = min((defender_stats['def'] + 1.0) / (defender_stats['def'] + attacker_stats['defa']), 0.5)

    crit_mult = 1.0
    if is_cri:
        crit_mult = max(1.0, min(1.0 + (attacker_stats['crd'] - defender_stats['crr']) / 10000.0, 2.0))

    rand_var = random.randint(0, 1000) / 1000.0 + 0.95

    skill_sexd = eff_cfg['adds'].get(3003, 0) / 10000.0
    exd_factor = 1.0 + (attacker_stats['exd'] - defender_stats['exr']) / 10000.0 + skill_sexd

    final_dmg = crit_mult * base_dmg * rand_var * (1.0 - def_red) * exd_factor

    if is_area:
        print(f"{prefix} RESULT: HIT dmg={int(final_dmg)} base={base_dmg:.1f} red={def_red:.3f} crit={crit_mult:.2f} exd={exd_factor:.2f} var={rand_var:.3f}")

    return int(max(1, final_dmg)), True, is_cri

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

    send_rpc_push(552, encode_sproto([
        (0, 12), (1, copy_id), (2, win), (3, 1), (4, 3 if win else 1), (5, res_items)
    ]))

    if picked_char:
        if win:
            advance_missions(picked_char, send_rpc_push, 'exp_copy')
            advance_missions(picked_char, send_rpc_push, 'interact', target_id=copy_id)
            advance_missions(picked_char, send_rpc_push, 'level')
            saved_pos = picked_char.get('pre_copy_pos')
            picked_char['pre_copy_pos'] = None

            def leave_exp_copy():
                start_map_transition(conn, picked_char, "11", send_rpc_push, override_pos=saved_pos)

            timer = threading.Timer(5.0, leave_exp_copy)
            timer.daemon = True
            timer.start()
        else:
            relife_req = encode_sproto([
                (0, 1),
                (1, 0),
                (2, ""),
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
                    spawn_exp_stage_subwave_internal(conn, send_rpc_push, picked_char, exp_state, exp_cfg, send_npc_wrapper)
                else:
                    if exp_state['cur_group'] < exp_cfg.get('group_count', 7):
                        exp_state['cur_group'] += 1
                        exp_state['cur_wave'] = 1
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

def spawn_map_npcs(conn, map_id, picked_char=None):
    map_str = str(map_id)
    connection_id = id(conn)
    spawned_maps = NPC_SPAWNED_MAPS.setdefault(connection_id, set())
    if map_str in spawned_maps:
        print(f"[NPC SPAWN] already sent map={map_str} to this connection")
        return
    spawned_maps.add(map_str)

    def send_npc_create(nid, name, x, z, o):
        global GLOBAL_INST_COUNTER
        npc_stats = get_npc_attr(nid)
        hp_cur = npc_stats['hp_max']
        hp_max = npc_stats['hp_max']
        atk = npc_stats['atk']
        df = npc_stats['def']
        lvl = npc_stats['lv']

        GLOBAL_INST_COUNTER += 1
        inst_id = GLOBAL_INST_COUNTER
        NPC_HP_MAP[inst_id] = hp_max
        NPC_INST_MAP[inst_id] = str(nid)

        final_nid = str(nid)
        if ";" in final_nid:
            if "XD_A" in final_nid: final_nid = "100"
            elif "QJ_A" in final_nid: final_nid = "104"
            elif "NQS_A" in final_nid: final_nid = "105"

        attr = encode_sproto([
            (0, inst_id), (1, final_nid), (2, hp_cur), (3, hp_max), (4, atk), (5, df),
            (15, x), (16, z), (17, o), (18, lvl), (21, name)
        ])
        ph = encode_sproto([(0, 509)]); pf = sproto_pack(ph + encode_sproto([(0, attr)]))
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass
        return inst_id

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
            advance_missions(picked_char, push_wrapper, 'enter_copy', target_id='105')
            advance_missions(picked_char, push_wrapper, 'exp_copy')
            advance_missions(picked_char, push_wrapper, 'interact', target_id='105')
            advance_missions(picked_char, push_wrapper, 'interact', target_id='1019')
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

    if map_str != "11":
        if map_str in STATIC_NPC_DATA:
            for m in STATIC_NPC_DATA[map_str]:
                cfg = NPC_CONFIG.get(m['nid'], {'name': f"NPC_{m['nid']}"})
                send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

        if map_str in MONSTER_DATA:
            for i, m in enumerate(MONSTER_DATA[map_str]):
                cfg = NPC_CONFIG.get(m['nid'], {'name': f"Monster_{m['nid']}"})
                send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'])

    if picked_char and map_str != "11":
        for mid, mdata in picked_char.get('active_missions', {}).items():
            if mdata['state'] == 1:
                cfg = missions_data.get(mid)
                logic_id = cfg.get('logic_id') if cfg else mid
                if logic_id in KILL_TARGET_SPAWNS:
                    for s in KILL_TARGET_SPAWNS[logic_id]:
                        if str(s['map']) == map_str:
                            for _ in range(s['num']): send_npc_create(s['nid'], f"Quest_{s['nid']}", s['x'], s['z'], 0)
                if logic_id in TARGET_CAR_SPAWNS:
                    for s in TARGET_CAR_SPAWNS[logic_id]:
                        if str(s['map']) == map_str:
                            send_npc_create(s['car_id'], f"QuestCar_{s['car_id']}", s['x'], s['z'], 0)

def sync_mission_data(picked_char):
    own_missions_list = []
    for mid, mdata in picked_char.get('active_missions', {}).items():
        parm = mdata.get('parm', [0]*8)
        if len(parm) < 8: parm += [0]*(8-len(parm))
        m_bytes = encode_sproto([
            (0, str(mid)),
            (1, int(mdata['state'])),
            (2, 0),
            (3, [int(x) for x in parm])
        ])
        own_missions_list.append(m_bytes)

    last_main = picked_char.get('last_main_mission_id', "-1")
    if last_main == "" or last_main == "None": last_main = "-1"

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
            (0, guid),
            (1, item['id']),
            (2, True),
            (5, item['amount'])
        ])
    return encode_sproto([(0, items)])

def add_to_inventory(picked_char, item_id, amount):
    item_id = str(item_id)
    if 'inventory' not in picked_char: picked_char['inventory'] = []
    found = False
    for item in picked_char['inventory']:
        if str(item['id']) == item_id:
            item['amount'] += amount
            found = True
            break
    if not found:
        picked_char['inventory'].append({'id': item_id, 'amount': amount})

    cfg = ITEM_CONFIG.get(item_id, {})
    itype = cfg.get('type', 0)
    
    if itype in (1, 2):
        ckey = 'fashion_backpack' if itype == 2 else 'equip_backpack'
        container = picked_char.setdefault(ckey, {})
        idx = int(time.time() * 1000) % 10000000 + len(container)
        container[idx] = {
            'indexId': idx, 'itemId': item_id, 'bindflag': True,
            'quality': 1, 'level': 1, 'stack': amount, 'parm': [0]*8, 'appraise': 1
        }
    elif itype == 22:
        container = picked_char.setdefault('badge_backpack', {})
        idx = int(time.time() * 1000) % 10000000 + len(container)
        container[idx] = {
            'indexId': idx, 'itemId': item_id, 'bindflag': True,
            'quality': 1, 'level': 1, 'stack': amount, 'parm': [0]*8, 'appraise': 1
        }
    else:
        container = picked_char.setdefault('item_backpack', {})
        existing = False
        for idx, item in container.items():
            if str(item.get('itemId')) == item_id:
                item['stack'] = item.get('stack', 1) + amount
                existing = True
                break
        if not existing:
            idx = int(time.time() * 1000) % 10000000 + len(container)
            container[idx] = {
                'indexId': idx, 'itemId': item_id, 'bindflag': True,
                'quality': 1, 'level': 1, 'stack': amount, 'parm': [0]*8, 'appraise': 1
            }

def build_mount_info(picked_char):
    mount_state = picked_char.setdefault('mounts', {})
    result = {}
    for mount_id, cfg in MOUNT_CONFIG.items():
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
    adjusted_time = time.time() - (3 * 3600)
    return time.strftime('%Y-%m-%d', time.gmtime(adjusted_time))

def ensure_daily_copy_state(picked_char):
    state = picked_char.setdefault('daily_copy_state', {})
    stamp = current_daily_stamp()
    if state.get('day') != stamp:
        state['day'] = stamp
        state['remaining'] = {}
        state['best_times'] = {}
    return state

def calculate_npc_kill_rewards(player_level, npc_level=1):
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

    effective_lv = min(player_level, npc_level + 2)
    req_data = LEVEL_DATA.get(effective_lv, LEVEL_DATA.get(1, {'exp': 400}))
    req_exp = req_data.get('exp', 400)

    exp_reward = max(3, int(req_exp * 0.008))
    cash_reward = max(10, effective_lv * 15 + 10)

    return exp_reward, cash_reward

def copy_attempts_remaining(picked_char, copy_id, cfg):
    state = ensure_daily_copy_state(picked_char)
    remaining = state.setdefault('remaining', {})
    if copy_id not in remaining:
        remaining[copy_id] = int(cfg.get('max_plays', 3))
    return max(0, int(remaining[copy_id]))

def street_race_rewards(level):
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
    level = int(picked_char.get('level', 1))
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
    exp_gained = 0
    cash_gained = 0
    inv_changed = False

    for entry in rewards_list:
        item_id = str(entry[0])
        amount = int(entry[2]) if len(entry) > 2 else int(entry[1])
        if item_id == "2001":
            exp_gained += amount
        elif item_id == "1001":
            cash_gained += amount
        else:
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
        send_rpc_push(611, sync_item_pack_rpc(picked_char))
        send_rpc_push(592, sync_backpack_item_rpc(picked_char))
        send_rpc_push(604, sync_badgepack_item_rpc(picked_char))
        send_rpc_push(616, sync_fashion_backpack_item_rpc(picked_char))

def give_mission_rewards(picked_char, mid):
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
                new_stats = get_character_stats(picked_char)
                picked_char['hp'] = new_stats['hp_max']
                print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']} HP Restored to {picked_char['hp']}")
            else: break

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
            matched = logic_type == 17 or target == target_value
        elif logic_type == 19 and event == 'car':
            matched = die_type == 2
        elif logic_type == 24 and event == 'car':
            matched = die_type == 6
        elif logic_type == 20 and event == 'impact':
            matched = die_type == 3 and (not target or target == target_value)
        elif logic_type in [0, 2, 6, 21] and event == 'interact':
            if logic_type == 21 and die_type == 4:
                matched = (target_value == mid)
            else:
                matched = not target or target == target_value or str(cfg.get('logic_id', '')) == target_value
        elif logic_type in [3, 4] and event == 'pickup':
            matched = str(cfg.get('logic_id')) == target_value
        elif logic_type == 25 and event == 'capture':
            matched = not target or target == target_value or str(cfg.get('logic_id', '')) == target_value
        elif logic_type in [102, 103, 105, 106, 107, 108, 110, 113, 114, 117, 119, 120, 132] and event in ['interact', 'exp_copy', 'enter_copy', 'dungeon']:
            matched = not target_id or str(cfg.get('logic_id')) == str(target_id) or event in ['exp_copy', 'enter_copy', 'dungeon']
        elif logic_type == 114 and event == 'world_boss':
            matched = True
        elif logic_type == 132 and event == 'dance':
            matched = True
        elif logic_type == 7 and event == 'map':
            matched = target == str(map_id)

        if not matched:
            continue

        required = int(cfg.get('require_num') or 1)
        if logic_type in [2, 6, 102, 103, 105, 106, 107, 108, 110, 113, 114, 117, 119, 120, 132]:
            required = 1
        if logic_type == 7:
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
        'equip_backpack': {},
        'equip_pack': {},
        'item_backpack': {},
        'badge_backpack': {},
        'badge_equip_pack': {},
        'fashion_backpack': {},
        'fashion_equip_pack': {},
        'is_show_fashion': False,
        'friends': {},
        'friend_applys': {},
        'enemies': {},
        'mails': {},
        'pos': [29860, 100, -17005, 0],
        'map_id': "11",
        'tutorial': 0,
        'download_complete': False,
        'mounts': {},
        'equipped_mount_id': '',
        'mount_riding': False,
        'daily_copy_state': {},
        'active_copy_id': None,
        'pre_copy_pos': None,
        'active_domin_id': None,
        'boss_inst_id': None,
        'pre_arena_pos': None
    }
    for k, v in fields.items():
        if k not in c: c[k] = v

    epack = c.setdefault('equip_pack', {})
    prof_str = str(c.get('prof', 0))
    starter_set = {
        0: {"0": "10002", "1": "20002", "2": "30002"}.get(prof_str, "10002"),
        1: {"0": "10003", "1": "20003", "2": "30003"}.get(prof_str, "10003"),
        2: {"0": "10005", "1": "20005", "2": "30005"}.get(prof_str, "10005"),
        3: {"0": "10004", "1": "20004", "2": "30004"}.get(prof_str, "10004"),
        4: {"0": "10006", "1": "20006", "2": "30006"}.get(prof_str, "10006"),
        5: {"0": "10001", "1": "20001", "2": "30001"}.get(prof_str, "10001"),
    }
    for s_idx, s_item_id in starter_set.items():
        if s_idx not in epack or not epack[s_idx]:
            epack[s_idx] = {
                'indexId': s_idx,
                'itemId': s_item_id,
                'bindflag': True,
                'quality': 1,
                'level': 1,
                'stack': 1,
                'parm': [0]*8,
                'appraise': 1
            }

    if not c.get('mails'):
        now = int(time.time())
        c['mails']["1001"] = {
            'id': 1001,
            'sendertype': 0,
            'title': "Welcome to Liberty City!",
            'senderTime': now,
            'receiveId': c.get('id', 0),
            'readTime': 0,
            'context': "Welcome to Vice City! Enjoy your adventure in Liberty City.#rClaim your starter rewards below!",
            'mailState': 0,
            'sortTime': now,
            'items': {'1001': {'id': "1001", 'count': 50000}, '2001': {'id': "2001", 'count': 10000}},
            'expireday': 30
        }

    stats = get_character_stats(c)
    if 'hp' not in c or c.get('hp', 0) <= 0:
        c['hp'] = stats['hp_max']

def start_map_transition(conn, picked_char, target_map_id, send_rpc_push, override_pos=None):
    if picked_char and picked_char.get('hp', 0) <= 0:
        stats = get_character_stats(picked_char)
        picked_char['hp'] = stats['hp_max']

    src_map = picked_char.get('map_id', '11')
    target_map_id = str(target_map_id)
    picked_char['map_id'] = target_map_id
    scene_name = "Unknown"

    landing_pos = override_pos
    if not landing_pos and target_map_id == "502":
        landing_pos = [-400, 120, 0, 9000]
        print(f"[TELEPORT] Lord Battle Map 502 start pos={landing_pos}")

    if not landing_pos and (target_map_id, src_map) in MAP_CONNECT_DATA:
        px, py, pz = MAP_CONNECT_DATA[(target_map_id, src_map)]
        y_coord = int(py * 100)
        if target_map_id == "101" or target_map_id == "105":
            y_coord = 200
        landing_pos = [int(px * 100), y_coord, int(pz * 100), 0]
        print(f"[TELEPORT] Transition {src_map} -> {target_map_id} using portal heuristic: {landing_pos}")

    if not landing_pos and target_map_id in MAP_CONFIG:
        birth = MAP_CONFIG[target_map_id]['birth']
        scene_name = MAP_CONFIG[target_map_id]['scene']
        if birth:
            parts = birth.split('#')
            if len(parts) >= 3:
                y_coord = int(parts[1])
                if target_map_id == "101" or target_map_id == "105":
                    y_coord = 200
                elif y_coord == 0:
                    y_coord = 100

                landing_pos = [int(parts[0]), y_coord, int(parts[2]), int(parts[3]) if len(parts) > 3 else 0]
                print(f"[TELEPORT] Spawn fix map={target_map_id} pos={landing_pos}")

    if landing_pos:
        if landing_pos[1] <= 0:
            landing_pos[1] = 100
        picked_char['pos'] = landing_pos
    else:
        print(f"[MAP CONFIG MISSING] map_id={target_map_id}")

    save_chars(all_accounts_chars)
    print("[DEBUG] BEFORE MAP ENTER")
    try:
        ph_p = encode_sproto([(0, 503)])
        data = encode_sproto([(0, target_map_id), (1, 0), (2, 1)])
        pf_p = sproto_pack(ph_p + data)
        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
        print(f"[M1003 DEBUG] TX 503 map_id={target_map_id}")
        print(f"[TX] PUSH TAG=503 SIZE={len(data)}")
        print(f"[MAP ENTER SEND] map_id={target_map_id} scene={scene_name} pos={picked_char['pos']}")

        send_rpc_push(504, encode_sproto([
            (0, get_full_char(picked_char)),
            (1, get_movement(picked_char['pos'][0], picked_char['pos'][1], picked_char['pos'][2], picked_char['pos'][3]))
        ]))
        print(f"[MAIN PLAYER CREATE SEND] map_id={target_map_id}")

        spawn_map_npcs(conn, target_map_id, picked_char)

        if target_map_id == "502":
            did = picked_char.get('active_domin_id', '1')
            global GLOBAL_INST_COUNTER
            GLOBAL_INST_COUNTER += 1
            boss_inst_id = GLOBAL_INST_COUNTER
            picked_char['boss_inst_id'] = boss_inst_id
            picked_char['boss_pos'] = [400, 120, 0, -9000]

            boss_stats = get_npc_attr("1105")
            NPC_HP_MAP[boss_inst_id] = boss_stats['hp_max']
            NPC_INST_MAP[boss_inst_id] = "BOSS_" + did
            picked_char['boss_waiting_for_map_ready'] = True
            print(f"[M1003 DEBUG] Prepared Boss did={did} inst={boss_inst_id} max_hp={boss_stats['hp_max']} (awaiting map_ready)")

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

def serve_resource_http(conn, initial_data):
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
                    block = resource
