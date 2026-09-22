import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
# Keep the database beside this server script.  A relative path depends on the
# process working directory and can make the same account appear empty after a
# restart started from a different directory.
CHAR_DB = os.path.join(SCRIPT_DIR, "characters_final.json")
CHAR_DB_LOCK = threading.RLock()
CHAR_DB_LOAD_ERROR = False
RESOURCE_ROOT = os.path.join(os.path.dirname(__file__), "assets")
server_session_counter = 8000
GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {} # inst_id -> nid (to resolve rewards)
NPC_HP_MAP = {}   # inst_id -> current hp
NPC_SPAWNED_MAPS = {}  # connection identity -> maps already sent to that client
DEAD_NPC_SET = set() # duplicate death/reward prevention set

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
DAILY_EXP_CONFIG = {} # Exp Stage ID -> DailyExpData wave definitions

try:
    script_dir = os.path.dirname(__file__)
    md_path = os.path.join(script_dir, "missions.json")
    rd_path = os.path.join(script_dir, "mission_rewards.json")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f: missions_data = json.load(f)
    if os.path.exists(rd_path):
        with open(rd_path, "r", encoding='utf-8') as f: rewards_data = json.load(f)

    def is_data(line): return line.startswith("*,") or ("," in line and line.split(",")[1].isdigit())

    # Accept both the original folder name and the plural name used by the
    # deployed resource tree.
    text_asset_root = os.path.join(script_dir, "assets", "Bundle", "TextAsset")
    if not os.path.isdir(text_asset_root):
        text_asset_root = os.path.join(script_dir, "assets", "Bundle", "TextAssets")
    # The local reverse-engineering workspace keeps the APK tables under
    # Decompiled/.  Deployed servers still use assets/ first; this fallback
    # only makes the identical data available for local verification.
    if not os.path.isdir(text_asset_root):
        text_asset_root = os.path.join(script_dir, "Decompiled", "assets", "Bundle", "TextAsset")

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
                    # Map all 75 columns. Real values (Absolute stats) start at index 44.
                    # NpcData columns (index): Atk(44), Hp(45), Def(46), HIT(47), DGE(48), CRI(49),
                    # RES(50), EXD(51), EXR(52), CRD(53), CRR(54), AntiStun(55), AntiKnockDown(56),
                    # DEFA(57), DGEA(58), RESA(59), HITA(60), CRIA(61).
                    is_abs = "绝对值" in parts[12] or (len(parts) > 45 and parts[45].isdigit() and int(parts[45]) > 100)
                    NPC_CONFIG[nid] = {
                        'name': parts[2],
                        'model': parts[4],
                        'level': lvl,
                        'is_abs': is_abs,
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
                        'defa_abs': int(parts[57]) if len(parts) > 57 and parts[57].isdigit() else 0,
                        'dgea_abs': int(parts[58]) if len(parts) > 58 and parts[58].isdigit() else 0,
                        'resa_abs': int(parts[59]) if len(parts) > 59 and parts[59].isdigit() else 0,
                        'hita_abs': int(parts[60]) if len(parts) > 60 and parts[60].isdigit() else 0,
                        'cria_abs': int(parts[61]) if len(parts) > 61 and parts[61].isdigit() else 0,
                        'drop_id': parts[20] if len(parts) > 20 else ""
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
                        'group': group,
                        'x': int(parts[4]),
                        'z': int(parts[5]),
                        'o': int(parts[6]),
                        'pathid': parts[9] if len(parts) > 9 else ""
                    }
                    if group == 9999:
                        if mid not in STATIC_NPC_DATA: STATIC_NPC_DATA[mid] = []
                        STATIC_NPC_DATA[mid].append(entry)
                    else:
                        if mid not in MONSTER_DATA: MONSTER_DATA[mid] = []
                        MONSTER_DATA[mid].append(entry)
        print(f"[MONSTER DATA LOADED] monsters_map={len(MONSTER_DATA)} static_npcs_map={len(STATIC_NPC_DATA)}")

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

    # Load the actual garage vehicle definitions.  Only rows marked NeedShow
    # are player vehicles; GTA traffic rows deliberately remain server-side
    # scene objects and are not sent to the garage.
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

    # Daily-copy UI receives its state from TAG 555.  Keep the IDs/types in
    # lockstep with CopySceneData so client tutorials can locate their target.
    copy_path = os.path.join(text_asset_root, "CopySceneData")
    if os.path.exists(copy_path):
        with open(copy_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 31 and parts[0] == "*" and parts[1].isdigit() and parts[11] == "1":
                    COPY_SCENE_CONFIG[parts[1]] = {
                        'map_id': parts[5],
                        'subtype': int(parts[12]) if parts[12].isdigit() else 0,
                        'exist_time': int(parts[13]) if parts[13].isdigit() else 0,
                        'end_time': int(parts[10]) if parts[10].isdigit() else 0,
                        'max_plays': int(parts[18]) if parts[18].isdigit() else 0,
                        'min_level': int(parts[19]) if parts[19].isdigit() else 1,
                        'max_level': int(parts[20]) if parts[20].isdigit() else 80,
                        'min_member': int(parts[21]) if parts[21].isdigit() else 1,
                        'max_member': int(parts[22]) if parts[22].isdigit() else 1,
                        'single_map_id': parts[31]
                    }
        print(f"[COPY SCENE CONFIG LOADED] daily_copies={len(COPY_SCENE_CONFIG)}")

    # Experience Stage uses DailyExpData for its seven groups and four waves.
    # The MonsterData map IDs listed here provide the actual NPC placement for
    # every wave; this keeps both the counts and enemy stats data-driven.
    daily_exp_path = os.path.join(text_asset_root, "DailyExpData")
    if os.path.exists(daily_exp_path):
        with open(daily_exp_path, "r", encoding='utf-8') as f:
            for line in f:
                parts = line.strip().split(',')
                if len(parts) >= 7 and parts[0] == "*" and parts[1].isdigit():
                    DAILY_EXP_CONFIG[parts[1]] = {
                        'wave_count': int(parts[2]) if parts[2].isdigit() else 0,
                        'wave_sizes': [int(v) for v in parts[3].split('#') if v.isdigit()],
                        'group_count': int(parts[4]) if parts[4].isdigit() else 0,
                        'group_time': int(parts[5]) if parts[5].isdigit() else 0,
                        'monster_maps': [v for v in parts[6].split('#') if v]
                    }
        print(f"[DAILY EXP CONFIG LOADED] stages={len(DAILY_EXP_CONFIG)}")

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
                        item_id = parts[index]
                        if not item_id:
                            continue
                        quality = int(parts[index + 1]) if parts[index + 1].isdigit() else 0
                        count = int(parts[index + 2]) if parts[index + 2].isdigit() else 1
                        rewards.append((item_id, quality, count))
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
except: traceback.print_exc()

def load_chars():
    """Load the persistent account -> character database without altering it."""
    global CHAR_DB_LOAD_ERROR
    if not os.path.exists(CHAR_DB):
        return {}
    try:
        with open(CHAR_DB, "r", encoding="utf-8") as f:
            data = json.load(f)
        if not isinstance(data, dict):
            raise ValueError("character database root is not an object")
        return data
    except Exception as exc:
        # Do not replace a damaged database with an empty one: that would make
        # existing accounts look new and could cause an accidental re-create.
        CHAR_DB_LOAD_ERROR = True
        print(f"[CHAR DB] load failed; preserving existing file: {exc}")
        return {}

def save_chars(data):
    """Atomically persist all character state after each important mutation."""
    tmp_path = f"{CHAR_DB}.tmp.{os.getpid()}.{threading.get_ident()}"
    try:
        with CHAR_DB_LOCK:
            with open(tmp_path, "w", encoding="utf-8") as f:
                json.dump(data, f, indent=4, ensure_ascii=False)
                f.flush()
                os.fsync(f.fileno())
            # os.replace is atomic on a single filesystem: after kill -9 the
            # database is either the complete old file or the complete new one.
            os.replace(tmp_path, CHAR_DB)
        return True
    except Exception as exc:
        print(f"[CHAR DB] save failed; existing database was not replaced: {exc}")
        try:
            if os.path.exists(tmp_path):
                os.remove(tmp_path)
        except OSError:
            pass
        return False

all_accounts_chars = load_chars()

def area_key(area_id):
    """JSON object keys are strings, including after a server restart."""
    try:
        return str(int(area_id))
    except (TypeError, ValueError):
        return str(area_id)

def account_characters(area_id, account_id, create=False):
    """Return the one account's persisted character list using canonical keys."""
    key = area_key(area_id)
    account_id = str(account_id)
    if create:
        area = all_accounts_chars.setdefault(key, {})
        return area.setdefault(account_id, [])
    area = all_accounts_chars.get(key, {})
    chars = area.get(account_id, []) if isinstance(area, dict) else []
    return chars if isinstance(chars, list) else []

def generate_unique_char_id():
    """Allocate an ID once; never reuse an ID already persisted in the DB."""
    with CHAR_DB_LOCK:
        used_ids = set()
        for area in all_accounts_chars.values():
            if not isinstance(area, dict):
                continue
            for chars in area.values():
                if not isinstance(chars, list):
                    continue
                for character in chars:
                    if isinstance(character, dict) and 'id' in character:
                        try:
                            used_ids.add(int(character['id']))
                        except (TypeError, ValueError):
                            pass
        candidate = int(time.time() * 1000) % 1000000000
        while candidate in used_ids:
            candidate = (candidate + 1) % 1000000000
        return candidate

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

def get_boss_char(inst_id, did):
    # Domin 1 boss stats and visual (XD profession)
    # These names are server placeholders, not names supplied by the APK data.
    name = "XK7NQ2VJ"
    prof = 0
    
    # VERIFIED ORIGINAL BOSS DATA: Level 3
    lv = 3
    hp_max = 9560
    power = 6000
    atk = 660
    df = 60
    
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
    skills_map = build_skills_map(prof, 25, boss_skill_levels)
    
    # Runtime: attribute(6), attribute_all(7)
    attr_run = encode_sproto([(0, hp_max), (2, atk), (3, df)])
    
    # Load level 3 coefficients for Boss
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
        (1, encode_sproto([(0, name), (1, prof), (2, 1), (3, "502"), (4, 1)])), # general
        (2, attr_oth),
        (6, v),
        (7, mv),
        (8, skills_map),
        (13, run)
    ])

# Skill System Constants
PROF_SKILLS = {
    0: {"atk": "101", "dodge": "104", "actives": ["105", "106", "107", "108", "109", "110"]},
    1: {"atk": "201", "dodge": "204", "actives": ["205", "206", "207", "208", "209", "210"]},
    2: {"atk": "301", "dodge": "304", "actives": ["305", "306", "307", "308", "309", "310"]}
}
# The APK receives character skills from sync_skill_info (tag 540).  A new
# character's starter layout contains only its normal attack, dodge, and the
# first class active in the existing SkillData order.  The other actives are
# not granted here.
SKILL_UNLOCK_LVS = [1, 5, 10, 15, 20, 25]

def get_skill_upgrade_cost(lv):
    if lv < 0: return 0
    if lv < 15: return (lv + 1) * 10000
    if lv < 24: return (lv - 13) * 100000 + 100000
    if lv == 24: return 3000000
    if lv == 25: return 7000000
    if lv == 26: return 18000000
    return 20000000

def build_skills_map(prof, char_level, skill_levels=None, skill_layout=None):
    prof = int(prof)
    if skill_levels is None: skill_levels = {}
    if skill_layout is None: skill_layout = {}
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    smap = {}

    # 1. Normal Attack (Group 0, Slot 0)
    atk_sid = p["atk"]
    smap[atk_sid] = encode_sproto([(0, atk_sid), (1, int(skill_levels.get(atk_sid, 1))), (2, 0), (3, 1), (4, 0), (5, False)])

    # Support for Attack Combo stages 2 and 3 (Group 0, Slot 10 - hidden)
    # This prevents the button from showing a cooldown icon when cycling the combo.
    base_atk = int(atk_sid)
    for combo_sid in [str(base_atk + 1), str(base_atk + 2)]:
        smap[combo_sid] = encode_sproto([(0, combo_sid), (1, 1), (2, 10), (3, 1), (4, 0), (5, False)])

    # 2. Dodge (Group 1, Slot 3)
    dodge_sid = p["dodge"]
    smap[dodge_sid] = encode_sproto([(0, dodge_sid), (1, int(skill_levels.get(dodge_sid, 1))), (2, 3), (3, 1), (4, 1), (5, False)])

    # 3. Active Professional Skills (Group 2, 3, 4, 5, 6, 7)
    for i, sid in enumerate(p["actives"]):
        unlock_lv = SKILL_UNLOCK_LVS[i] if i < len(SKILL_UNLOCK_LVS) else 1
        if char_level >= unlock_lv:
            # Map actives to slots 4-9
            smap[sid] = encode_sproto([
                (0, sid), (1, int(skill_levels.get(sid, 1))),
                (2, 4 + i), (3, unlock_lv), (4, 2 + i), (5, False)
            ])

    # 4. Preserve any other saved skills (unassigned)
    for sid, level in skill_levels.items():
        sid = str(sid)
        if sid not in smap:
            saved = skill_layout.get(sid, {})
            slot = int(saved.get('index', 10)) if isinstance(saved, dict) else 10
            slot2 = int(saved.get('index2', slot)) if isinstance(saved, dict) else slot
            smap[sid] = encode_sproto([
                (0, sid), (1, int(level)), (2, slot), (3, slot2),
                (4, 0), (5, False)
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
    
    # Add Weapon ATK (Level 1 weapon 10001/20001/30001 gives 180 ATK)
    atk += 180

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
        'exd': ld['exd'][prof], 'exr': ld['exr'][prof], 'crd': ld['crd'][prof], 'crr': ld['crr'][prof]
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
    skills_map = build_skills_map(c.get('prof', 0), char_level, skill_levels, c.get('skill_layout', {}))
    wid = "10001" if c.get('prof', 0) == 0 else "20001" if c.get('prof', 0) == 1 else "30001"
    w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])
    equip_map = {5: w1}

    # character.download (Tag 15):
    # The APK shows the "Download / With New Car" tip when Tag 15 == 1.
    # It stops showing it when Tag 15 == 0 or 2.
    # New characters start with False, so they see the prompt.
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
        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
    except: pass

def get_npc_attr(nid):
    cfg = NPC_CONFIG.get(nid)
    if not cfg: return {'hp_max': 10000, 'atk': 100, 'def': 10, 'hit': 100, 'eva': 10, 'cri': 10, 'res': 10, 'lv': 1, 'defa': 3000, 'dgea': 6000, 'resa': 3000, 'hita': 300, 'cria': 3000, 'exd': 0, 'exr': 0, 'crd': 5000, 'crr': 0}

    lvl = cfg.get('level', 1)
    max_lv = max(LEVEL_DATA.keys())
    ld = LEVEL_DATA.get(min(lvl, max_lv), LEVEL_DATA[1])

    stats = {}
    stats['hp_max'] = cfg.get('hp_abs') if cfg.get('hp_abs') else (ld['hp'][0] * cfg.get('hp_coe', 10000)) // 10000
    stats['atk'] = cfg.get('atk_abs') if cfg.get('atk_abs') else (ld['atk'][0] * cfg.get('atk_coe', 10000)) // 10000
    stats['def'] = cfg.get('def_abs') if cfg.get('def_abs') else (ld['def'][0] * cfg.get('def_coe', 10000)) // 10000
    
    stats['hit'] = cfg.get('hit_abs') if cfg.get('hit_abs') else (ld['hit'][0] * cfg.get('hit_coe', 10000)) // 10000
    stats['eva'] = cfg.get('eva_abs') if cfg.get('eva_abs') else (ld['eva'][0] * cfg.get('eva_coe', 10000)) // 10000
    stats['cri'] = cfg.get('cri_abs') if cfg.get('cri_abs') else (ld['cri'][0] * cfg.get('cri_coe', 10000)) // 10000
    stats['res'] = cfg.get('res_abs') if cfg.get('res_abs') else (ld['res'][0] * cfg.get('res_coe', 10000)) // 10000

    stats['exd'] = cfg.get('exd_abs') if cfg.get('exd_abs') else (ld['exd'][0] * cfg.get('exd_coe', 10000)) // 10000
    stats['exr'] = cfg.get('exr_abs') if cfg.get('exr_abs') else (ld['exr'][0] * cfg.get('exr_coe', 10000)) // 10000
    stats['crd'] = cfg.get('crd_abs') if cfg.get('crd_abs') else (ld['crd'][0] * cfg.get('crd_coe', 10000)) // 10000
    stats['crr'] = cfg.get('crr_abs') if cfg.get('crr_abs') else (ld['crr'][0] * cfg.get('crr_coe', 10000)) // 10000

    stats['defa'] = cfg.get('defa_abs') if cfg.get('defa_abs') else ld['defa']
    stats['dgea'] = cfg.get('dgea_abs') if cfg.get('dgea_abs') else ld['dgea']
    stats['resa'] = cfg.get('resa_abs') if cfg.get('resa_abs') else ld['resa']
    stats['hita'] = cfg.get('hita_abs') if cfg.get('hita_abs') else ld['hita']
    stats['cria'] = cfg.get('cria_abs') if cfg.get('cria_abs') else ld['cria']

    stats['lv'] = lvl

    # Calculate Power
    prof_coeffs = {"atk":16, "hp":1, "def":11, "hit":2, "eva":5.5, "cri":10, "res":10}
    raw_power = (stats['atk'] * prof_coeffs['atk'] + stats['hp_max'] * prof_coeffs['hp'] + stats['def'] * prof_coeffs['def'])
    stats['power'] = int(raw_power * 3.0)

    return stats

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

def get_combat_damage(attacker_stats, defender_stats, skill_id, skill_lv, is_area=False, pvp_scale=1.0):
    """Original Damage calculation reproduced from CharacterAttributeData.cs"""
    prefix = "[AREA DAMAGE]" if is_area else "[COMBAT]"
    
    # 1. Get Skill Multipliers from EffInfoData
    skill_cfg = SKILL_CONFIG.get(skill_id, {})
    eff_id = skill_cfg.get('eff0', "10000") # Default to NormalAttack if not found
    eff_cfg = EFF_CONFIG.get(eff_id, {
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
    
    hit_prob = 1.0 + hit_p - dge_p + skill_shit
    roll_hit = random.random()
    
    if is_area:
        print(f"{prefix} HIT CHECK: roll={roll_hit:.3f} prob={hit_prob:.3f} (hit_p={hit_p:.3f}, dge_p={dge_p:.3f}, skill={skill_shit:.3f})")
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
    
    base_dmg = attacker_stats['atk'] * scaled_scale + scaled_damage
    def_red = min((defender_stats['def'] + 1.0) / (defender_stats['def'] + attacker_stats['defa']), 0.5)
    
    # 5. Handling Critical Multiplier
    crit_mult = 1.0
    if is_cri:
        crit_mult = max(1.0, min(1.0 + (attacker_stats['crd'] - defender_stats['crr']) / 10000.0, 2.0))
        
    # 6. Final Formula with Random Variance [0.95, 1.05]
    rand_var = random.randint(0, 1000) / 1000.0 + 0.95
    
    skill_sexd = eff_cfg['adds'].get(3003, 0) / 10000.0
    exd_factor = 1.0 + (attacker_stats['exd'] - defender_stats['exr']) / 10000.0 + skill_sexd
    
    final_dmg = crit_mult * base_dmg * rand_var * (1.0 - def_red) * exd_factor
    
    if is_area:
        print(f"{prefix} RESULT: HIT dmg={int(final_dmg)} base={base_dmg:.1f} red={def_red:.3f} crit={crit_mult:.2f} exd={exd_factor:.2f} var={rand_var:.3f}")
        
    return int(max(1, final_dmg)), True, is_cri

def spawn_map_npcs(conn, map_id, picked_char=None):
    """Spawns all NPCs, Monsters, and Traffic defined in data for the map."""
    map_str = str(map_id)
    connection_id = id(conn)
    spawned_maps = NPC_SPAWNED_MAPS.setdefault(connection_id, set())
    if map_str in spawned_maps:
        print(f"[NPC SPAWN] already sent map={map_str} to this connection")
        return
    spawned_maps.add(map_str)

    def send_npc_create(nid, name, x, z, o, pathid=""):
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
        NPC_INST_MAP[inst_id] = str(nid) # Resolver mapping

        # Handle composite models like "PartA;PartB;PartC" to prevent client crashes
        final_nid = str(nid)
        if ";" in final_nid:
            if "XD_A" in final_nid: final_nid = "100"
            elif "QJ_A" in final_nid: final_nid = "104"
            elif "NQS_A" in final_nid: final_nid = "105"

        # ObjInitNpcData reads all combat fields.  An abbreviated attribute
        # creates an NPC with null/zero state and fails inside ObjNpcPoolGroup.
        attr = build_npc_attribute(inst_id, final_nid, npc_stats, x, z, o, pathid)
        ph = encode_sproto([(0, 509)]); pf = sproto_pack(ph + encode_sproto([(0, attr)]))
        try: conn.sendall(struct.pack(">H", len(pf)) + pf)
        except: pass
        return inst_id

    # 1. Spawn Static NPCs & Monsters
    # Map 11 (TUTORIAL_CAR) handles spawning locally on client.
    if map_str != "11":
        if map_str in STATIC_NPC_DATA:
            for m in STATIC_NPC_DATA[map_str]:
                cfg = NPC_CONFIG.get(m['nid'], {'name': f"NPC_{m['nid']}"})
                send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'], m.get('pathid', ''))

        if map_str in MONSTER_DATA:
            for i, m in enumerate(MONSTER_DATA[map_str]):
                cfg = NPC_CONFIG.get(m['nid'], {'name': f"Monster_{m['nid']}"})
                send_npc_create(m['nid'], cfg['name'], m['x'], m['z'], m['o'], m.get('pathid', ''))

    # 2. Spawn Mission targets defined by the APK data.
    # Map 11 (TUTORIAL_CAR) spawns targets LOCALLY. Server spawning causes duplicates/crashes.
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

def spawn_exp_stage_wave(conn, exp_cfg, group_index, wave_index):
    """Spawn exactly one APK-configured Experience Stage wave.

    DailyExpData names four MonsterData maps, one per wave.  Each such map
    contains 28 placement groups (seven stage groups times four waves).
    """
    monster_maps = exp_cfg.get('monster_maps', [])
    if not (0 <= wave_index < len(monster_maps)):
        return set()
    phase = group_index * exp_cfg.get('wave_count', 0) + wave_index + 1
    records = [m for m in MONSTER_DATA.get(str(monster_maps[wave_index]), [])
               if m.get('group') == phase]
    instance_ids = set()
    global GLOBAL_INST_COUNTER
    for monster in records:
        nid = str(monster['nid'])
        stats = get_npc_attr(nid)
        GLOBAL_INST_COUNTER += 1
        inst_id = GLOBAL_INST_COUNTER
        NPC_INST_MAP[inst_id] = nid
        NPC_HP_MAP[inst_id] = stats['hp_max']
        attr = build_npc_attribute(inst_id, nid, stats, monster['x'], monster['z'], monster['o'], monster.get('pathid', ''))
        packet = sproto_pack(encode_sproto([(0, 509)]) + encode_sproto([(0, attr)]))
        try:
            conn.sendall(struct.pack(">H", len(packet)) + packet)
            instance_ids.add(inst_id)
        except Exception:
            break
    return instance_ids

def build_npc_attribute(inst_id, nid, stats, x, z, o, pathid=""):
    """Complete npc_attribute required by ObjInitNpcData.InitData in the APK."""
    # player_name (Tag 21) is hijacked for PathID if it starts with a digit
    # or matches the NPCPathData pattern.
    return encode_sproto([
        (0, inst_id), (1, str(nid)), (2, stats['hp_max']), (3, stats['hp_max']),
        (4, stats['atk']), (5, stats['def']), (6, stats['hit']), (7, stats['eva']),
        (8, stats['cri']), (9, stats['exd']), (10, stats['exr']), (11, stats['res']),
        (12, stats['crd']), (13, stats['crr']), (14, stats['defa']),
        (15, x), (16, z), (17, o), (18, stats['lv']),
        (19, 0), (20, 0), (21, pathid if pathid else NPC_CONFIG.get(str(nid), {}).get('name', '')),
        (24, stats['dgea']), (25, stats['resa']), (26, stats['hita']), (27, stats['cria'])
    ])

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
    """
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
    """The server's daily-copy reset key (UTC calendar day)."""
    return time.strftime('%Y-%m-%d', time.gmtime())

def ensure_daily_copy_state(picked_char):
    """Reset the APK-configured daily attempts once per calendar day."""
    state = picked_char.setdefault('daily_copy_state', {})
    stamp = current_daily_stamp()
    if state.get('day') != stamp:
        state['day'] = stamp
        state['remaining'] = {}
    return state

def copy_attempts_remaining(picked_char, copy_id, cfg):
    state = ensure_daily_copy_state(picked_char)
    remaining = state.setdefault('remaining', {})
    if copy_id not in remaining:
        remaining[copy_id] = int(cfg['max_plays'])
    return max(0, int(remaining[copy_id]))

def street_race_rewards(level):
    """Return level-resolved _drop_bc ShowRewardData items + guaranteed Spare Parts."""
    level = int(level)
    eligible = [lv for lv in STREET_RACE_REWARD_BY_LEVEL if lv <= level]
    reward_id = STREET_RACE_REWARD_BY_LEVEL[max(eligible)] if eligible else ''
    rewards = list(SHOW_REWARD_CONFIG.get(reward_id, []))
    # Guaranteed Spare Parts (Item 3001) for finishing.
    # Base 10 + 5 per 10 levels.
    parts_amt = 10 + (level // 10) * 5
    rewards.append(("3001", 1, parts_amt))
    return rewards

def sync_copy_scenes(picked_char):
    """Build TAG 555 from the APK's CopySceneData definitions."""
    level = int(picked_char.get('level', 1))
    # CopySceneData contains the level variants for each daily activity.  The
    # client expects one current copy per subtype, not every future/parallel
    # Street Race row.  Select the highest unlocked level bracket; ties use
    # the first configured ID (e.g. Street Race 211 at level 4).
    selected = {}
    for copy_id, cfg in COPY_SCENE_CONFIG.items():
        if level < cfg['min_level'] or level > cfg.get('max_level', 80):
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
        best_time = state.get('best_times', {}).get(copy_id, 0)
        copies[copy_id] = encode_sproto([
            (0, copy_id), (1, copy_attempts_remaining(picked_char, copy_id, cfg)),
            (2, int(best_time)), (3, 1), (5, True), (6, 0), (7, cfg['subtype'])
        ])
    return encode_sproto([(0, copies)])

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
            matched = logic_type == 17 or target == target_value
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
        elif logic_type == 25 and event == 'capture':
            # LogicType 25 (Capture) matches activity ID or NPC target
            matched = not target or target == target_value or str(cfg.get('logic_id', '')) == target_value
        elif logic_type in [102, 103, 105, 106, 107, 108, 110, 113, 114, 117, 119, 120, 132] and event == 'interact':
            # Dungeon/Guide entry missions advance on interaction/entry
            matched = str(cfg.get('logic_id')) == str(target_id)
        elif logic_type == 114 and event == 'world_boss':
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
        # Per-skill slots use the existing change_skill_position protocol.
        'skill_layout': {},
        'active_missions': {},
        'completed_side_missions': [],
        'last_main_mission_id': "-1",
        'inventory': [],
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
        'exp_copy_state': None,
        'exp_return_scheduled': False,
        'active_domin_id': None,
        'boss_inst_id': None,
        'pre_arena_pos': None
    }
    for k, v in fields.items():
        if k not in c: c[k] = v

    # Initialize HP if not set
    if 'hp' not in c:
        lv = c.get('level', 1)
        prof = c.get('prof', 0)
        ld = LEVEL_DATA.get(lv, LEVEL_DATA.get(1, {'hp': [3000,3000,3000]}))
        c['hp'] = ld['hp'][prof] if prof < len(ld['hp']) else ld['hp'][0]

def start_map_transition(conn, picked_char, target_map_id, send_rpc_push, override_pos=None):
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

        # BOSS SPAWN for Dominance Map 502
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
            # The APK cannot create the zombie player (or the VS panel) until
            # map_ready.  Remember it here; MSG 100 sends the actual tag 544.
            picked_char['boss_waiting_for_map_ready'] = True
            print(f"[M1003 DEBUG] Prepared Boss did={did} inst={boss_inst_id} max_hp={boss_stats['hp_max']} (awaiting map_ready)")

    except Exception:
        print("[!] FAILED TO SEND MAP ENTER TRANSITION")
        traceback.print_exc()
    print("[DEBUG] AFTER MAP ENTER")

def is_skill_locked(sid, level, prof):
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    base_atk = int(p["atk"])
    # Basic attack combo stages 1, 2, 3 and dodge are NEVER locked
    combo_chain = {str(base_atk), str(base_atk + 1), str(base_atk + 2), str(p["dodge"])}
    sid_str = str(sid)
    if sid_str in combo_chain:
        return False, 0
    # Active professional skills follow the unlock level schedule
    if sid_str in p["actives"]:
        idx = p["actives"].index(sid_str)
        unlock_lv = SKILL_UNLOCK_LVS[idx] if idx < len(SKILL_UNLOCK_LVS) else 1
        return (level < unlock_lv), unlock_lv
    # Other internal/mission skills are unlocked by default
    if sid_str.isdigit() and int(sid_str) >= 1000:
        return False, 0
    return True, 0

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

def client_handler(conn, addr):
    global GLOBAL_INST_COUNTER
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0
    global server_session_counter
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

            return_pos = picked_char.get('pre_arena_pos')
            if picked_char.pop('domin_return_restore_hp', False):
                # A map transition recreates the main player with this value.
                # Restore only after the exit countdown, never in the arena.
                picked_char['hp'] = get_character_stats(picked_char)['hp_max']
            picked_char['boss_inst_id'] = None
            picked_char['active_domin_id'] = None
            picked_char['pre_arena_pos'] = None
            picked_char['domin_return_scheduled'] = False
            # The APK has no dedicated "close arena timer" packet. Its
            # notify_copy_start_info handler closes the timer immediately for
            # an elapsed end_time, before the map transition begins.
            send_rpc_push(629, encode_sproto([(0, int(time.time())), (1, 2)]))
            start_map_transition(conn, picked_char, '11', send_rpc_push, override_pos=return_pos)
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
            picked_char['pre_copy_pos'] = None
            picked_char['active_copy_id'] = None
            picked_char['street_race_return_scheduled'] = False
            save_chars(all_accounts_chars)
            start_map_transition(conn, picked_char, '11', send_rpc_push, override_pos=return_pos)
            print(f"[STREET RACE] exit countdown finished; returned after copy={copy_id}")

        leave_after_notice(delay)

    def schedule_exp_stage_return():
        """Use the Exp Stage CopySceneData EndTime=5 exit countdown."""
        if not picked_char or picked_char.get('exp_return_scheduled'):
            return
        state = picked_char.get('exp_copy_state') or {}
        copy_id = str(state.get('copy_id') or '')
        cfg = COPY_SCENE_CONFIG.get(copy_id, {})
        picked_char['exp_return_scheduled'] = True

        def leave_after_notice(seconds_left):
            if not picked_char or not picked_char.get('exp_return_scheduled'):
                return
            if seconds_left > 0:
                send_rpc_push(529, encode_sproto([
                    (0, f"You will leave the scene after {seconds_left} seconds"), (1, True)
                ]))
                timer = threading.Timer(1.0, leave_after_notice, args=(seconds_left - 1,))
                timer.daemon = True
                timer.start()
                return
            return_pos = picked_char.get('pre_copy_pos')
            picked_char['pre_copy_pos'] = None
            picked_char['active_copy_id'] = None
            picked_char['exp_copy_state'] = None
            picked_char['exp_return_scheduled'] = False
            # Exp Stage returns the player to city healthy; it does not respawn
            # them inside the timed copy.
            picked_char['hp'] = get_character_stats(picked_char)['hp_max']
            save_chars(all_accounts_chars)
            start_map_transition(conn, picked_char, '11', send_rpc_push, override_pos=return_pos)
            print(f"[EXP STAGE] exit countdown finished; returned after copy={copy_id}")

        leave_after_notice(int(cfg.get('end_time') or 5))

    def send_exp_stage_info():
        state = picked_char.get('exp_copy_state') or {}
        copy_id = str(state.get('copy_id') or '')
        exp_cfg = DAILY_EXP_CONFIG.get(copy_id, {})
        wave_index = int(state.get('wave_index', 0))
        # notice_copy_scene_info drives the APK HUD.
        # index(1): current wave index.
        # parm1(3): group index (1-based).
        # parm2(4): current kills in this wave/group.
        # parm3(5): total kills in stage.
        send_rpc_push(683, encode_sproto([
            (0, copy_id), (1, wave_index), (2, int(state.get('end_time', time.time()))),
            (3, int(state.get('group_index', 0)) + 1),
            (4, int(state.get('group_kills', 0))),
            (5, int(state.get('total_kills', 0)))
        ]))

    def offer_exp_stage_respawn():
        """Open the APK's current-map rebirth UI after an Exp Stage death."""
        state = picked_char.get('exp_copy_state') or {}
        if not state or state.get('finished') or state.get('awaiting_respawn'):
            return
        state['awaiting_respawn'] = True
        # REBIRTH_TYPE.CURRENT_MAP_REBIRTH is enum value 1.  The configured
        # download reward supplies item 9011, used here as the one-item cost.
        send_rpc_push(618, encode_sproto([(0, 1), (1, 1), (2, '9011')]))
        print('[EXP STAGE] player died; offered current-map respawn or city return')

    def resolve_exp_stage_respawn(in_place):
        state = picked_char.get('exp_copy_state') or {}
        if not state.get('awaiting_respawn'):
            return
        if not in_place:
            state['awaiting_respawn'] = False
            finish_exp_stage(False)
            return
        inventory = picked_char.get('inventory', [])
        item = next((entry for entry in inventory if str(entry.get('id')) == '9011' and int(entry.get('amount', 0)) > 0), None)
        if not item:
            # Do not permit a free in-place resurrection; the client also
            # disables this button when the required item is unavailable.
            return
        item['amount'] -= 1
        if item['amount'] <= 0:
            inventory.remove(item)
        state['awaiting_respawn'] = False
        picked_char['hp'] = get_character_stats(picked_char)['hp_max']
        # aoi_relife_player updates the local player model and restores HP in
        # the APK without resetting the active timed-copy wave state.
        relife = encode_sproto([
            (0, picked_char['id']), (1, encode_sproto([(0, picked_char['hp'])])),
            (2, get_movement(picked_char['pos'][0], picked_char['pos'][1], picked_char['pos'][2], picked_char['pos'][3]))
        ])
        send_rpc_push(512, encode_sproto([(0, relife)]))
        send_rpc_push(611, sync_inventory_data(picked_char))
        save_chars(all_accounts_chars)
        print('[EXP STAGE] current-map respawn used item=9011')

    def finish_exp_stage(won):
        state = picked_char.get('exp_copy_state') or {}
        if not state or state.get('finished'):
            return
        state['finished'] = True
        copy_id = str(state['copy_id'])
        result_items = []
        if won:
            exp_reward = 50000
            cash_reward = 20000
            picked_char['exp'] = int(picked_char.get('exp', 0)) + exp_reward
            picked_char['cash'] = int(picked_char.get('cash', 0)) + cash_reward
            get_character_stats(picked_char)
            sync_char_attrs_rpc(conn, picked_char)
            send_rpc_push(519, sync_mission_data(picked_char))
            send_rpc_push(611, sync_inventory_data(picked_char))
            advance_missions(picked_char, send_rpc_push, 'interact', target_id=copy_id)
            advance_missions(picked_char, send_rpc_push, 'interact', target_id='102')
            result_items = [
                encode_sproto([(0, "2001"), (1, exp_reward), (3, 2)]),
                encode_sproto([(0, "1001"), (1, cash_reward), (3, 2)])
            ]

        send_rpc_push(552, encode_sproto([
            (0, 12), (1, copy_id), (2, bool(won)), (3, 0), (4, 0), (5, result_items)
        ]))
        send_rpc_push(555, sync_copy_scenes(picked_char))
        save_chars(all_accounts_chars)
        schedule_exp_stage_return()
        print(f"[EXP STAGE] result id={copy_id} win={won} kills={state.get('total_kills', 0)}")

    def start_exp_stage_battle():
        state = picked_char.get('exp_copy_state') or {}
        copy_id = str(state.get('copy_id') or '')
        exp_cfg = DAILY_EXP_CONFIG.get(copy_id)
        copy_cfg = COPY_SCENE_CONFIG.get(copy_id)
        if not exp_cfg or not copy_cfg or state.get('started'):
            return
        state.update({
            'started': True, 'finished': False, 'group_index': 0, 'wave_index': 0,
            'group_kills': 0, 'total_kills': 0,
            'end_time': int(time.time()) + int(copy_cfg['exist_time'])
        })
        state['instance_ids'] = list(spawn_exp_stage_wave(conn, exp_cfg, 0, 0))
        # notify_copy_start_info starts the APK countdown; type 12 is the
        # configured CopySceneData subtype and wave_time is DailyExpData's 60.
        send_rpc_push(629, encode_sproto([
            (0, state['end_time']), (1, 12), (2, exp_cfg['group_time']), (3, 1)
        ]))
        send_exp_stage_info()

        def expire(expected_copy_id=copy_id, expected_end=state['end_time']):
            live = picked_char.get('exp_copy_state') or {}
            if (live.get('copy_id') == expected_copy_id and live.get('end_time') == expected_end
                    and not live.get('finished')):
                finish_exp_stage(False)

        timer = threading.Timer(max(1, int(copy_cfg['exist_time'])), expire)
        timer.daemon = True
        timer.start()
        print(f"[EXP STAGE] started id={copy_id} groups={exp_cfg['group_count']} waves={exp_cfg['wave_count']} end={state['end_time']}")

    def record_exp_stage_kill(inst_id):
        state = picked_char.get('exp_copy_state') or {}
        if not state.get('started') or state.get('finished'):
            return
        instance_ids = set(state.get('instance_ids', []))
        if inst_id not in instance_ids:
            return
        instance_ids.discard(inst_id)
        state['instance_ids'] = list(instance_ids)
        state['group_kills'] = int(state.get('group_kills', 0)) + 1
        state['total_kills'] = int(state.get('total_kills', 0)) + 1
        if instance_ids:
            send_exp_stage_info()
            return
        exp_cfg = DAILY_EXP_CONFIG.get(str(state['copy_id']), {})
        state['wave_index'] = int(state.get('wave_index', 0)) + 1
        if state['wave_index'] >= int(exp_cfg.get('wave_count', 0)):
            state['wave_index'] = 0
            state['group_index'] = int(state.get('group_index', 0)) + 1
            state['group_kills'] = 0
        if state['group_index'] >= int(exp_cfg.get('group_count', 0)):
            send_exp_stage_info()
            finish_exp_stage(True)
            return
        state['instance_ids'] = list(spawn_exp_stage_wave(
            conn, exp_cfg, int(state['group_index']), int(state['wave_index'])))
        # The client has a next_wave handler for this exact transition.
        send_rpc_push(515, encode_sproto([(0, int(state['wave_index']) + 1)]))
        send_exp_stage_info()

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

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1); cur_areaId = get_area_id(sid)
                # sync_common_data: serverTime(0), time_offset(2), func_info(9), pvp_scale(4), seed(12), server_level(13), start_time(14)
                resp = encode_sproto([
                    (0, 2), (1, "1.012.017"), (2, "205"), (3, 1),
                    # pvp_scale = 1.0 (10000), seed = random
                    (4, 10000), (12, random.randint(1, 10000))
                ])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                # JSON restores area IDs as strings.  account_characters()
                # uses the canonical key, so this finds the same record after
                # a restart instead of presenting the account as empty.
                chars = account_characters(cur_areaId, acc_id)
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
                existing_chars = account_characters(cur_areaId, acc_id)
                if len(existing_chars) >= 4:
                    print(f"[CHARACTER CREATE] list full for account={acc_id}")
                    resp = encode_sproto([(1, 2)]) # Error code for full
                elif CHAR_DB_LOAD_ERROR:
                    print("[CHARACTER CREATE] refused because character database did not load")
                    resp = encode_sproto([(1, 1)])
                else:
                    prof = get_val_int(c_data, 1, 0)
                    cid = generate_unique_char_id()
                    nc = {'id': cid, 'name': name, 'prof': prof}
                    init_character_fields(nc)
                    account_characters(cur_areaId, acc_id, create=True).append(nc)
                    if not save_chars(all_accounts_chars):
                        account_characters(cur_areaId, acc_id).remove(nc)
                        resp = encode_sproto([(1, 1)])
                    else:
                        print(f"[CHARACTER CREATE] new character id={cid} name={name}")
                        resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in account_characters(cur_areaId, acc_id) if c.get('id') == char_id), None)
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

                    # 614: sync_common_data
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
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
                    smap = build_skills_map(
                        picked_char['prof'], picked_char['level'],
                        picked_char.get('skill_levels', {}), picked_char.get('skill_layout', {})
                    )
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

                    except Exception:
                        print("[!] FAILED TO SEND INITIAL MAP ENTER")
                        traceback.print_exc()
                    print("[DEBUG] AFTER MAP ENTER")

            elif msg == 100: # map_ready
                if picked_char:
                    mid = picked_char.get('map_id', '11')
                    print(f"[MAP READY RECEIVED] map_id={mid}")
                    if picked_char.get('exp_copy_state') and not picked_char['exp_copy_state'].get('started'):
                        start_exp_stage_battle()

                    # Tag 654 is start_enter_game.  The APK interprets state=1
                    # as completion of the optional resource download. Sending
                    # it prematurely on map_ready would close the download tip
                    # before the player can click it. Only send if already done.
                    if picked_char.get('download_complete'):
                        send_rpc_push(654, encode_sproto([(0, 1)]))

                    if mid == "502":
                        # Map 502 exposes its match timer only through this APK tag.
                        send_rpc_push(629, encode_sproto([(0, int(time.time()) + 60), (1, 0)]))
                        boss_id = picked_char.get('boss_inst_id')
                        if boss_id and picked_char.pop('boss_waiting_for_map_ready', False):
                            did = picked_char.get('active_domin_id', '1')
                            boss_stats = get_npc_attr("1105")
                            # Tag 544 creates ObjZombiePlayer and opens the VS UI.
                            send_rpc_push(544, encode_sproto([(0, get_boss_char(boss_id, did))]))
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

                if session is not None:
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
                            send_rpc_push(521, encode_sproto([(0, mid), (1, 1)])) # Success feedback
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
                    prof_skills = PROF_SKILLS.get(int(picked_char.get('prof', 0)), PROF_SKILLS[0])
                    granted = set(build_skills_map(
                        picked_char['prof'], picked_char['level'],
                        picked_char.get('skill_levels', {}), picked_char.get('skill_layout', {})
                    ))
                    # Dodge has no upgrade data.  Other skills must already
                    # be present in the character's tag-540 skill map.
                    if sid not in granted or sid == prof_skills['dodge']:
                        sid = ''
                    cost = get_skill_upgrade_cost(cur_lv)
                    if sid and picked_char.get('cash', 0) >= cost and picked_char.get('level', 1) > cur_lv + 1:
                        picked_char['cash'] -= cost
                        picked_char['skill_levels'][sid] = cur_lv + 1
                        save_chars(all_accounts_chars)
                        smap = build_skills_map(
                            picked_char['prof'], picked_char['level'],
                            picked_char['skill_levels'], picked_char.get('skill_layout', {})
                        )
                        send_rpc_push(540, encode_sproto([(0, smap), (1, True)]))
                        sync_char_attrs_rpc(conn, picked_char)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 192: # change_skill_position
                # APK request fields: skillId(0), indexPos(1).  Persist the
                # exact existing request so an active skill does not move back
                # to its default slot after a forced server restart.
                sid = field_text(body, 0)
                index_pos = get_val_int(body, 1, -1)
                if picked_char and 0 <= index_pos <= 10:
                    granted = build_skills_map(
                        picked_char['prof'], picked_char['level'],
                        picked_char.get('skill_levels', {}), picked_char.get('skill_layout', {})
                    )
                    if sid in granted:
                        picked_char.setdefault('skill_layout', {})[sid] = {
                            'index': index_pos,
                            'index2': index_pos
                        }
                        save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 316: # change_skill_index
                # The APK switches its current active bar locally.  Accept the
                # existing request; no new protocol field or server-side skill
                # grant is needed for this action.
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
                        # The client applies the attack and then sends MSG 111
                        # (accept_damge).  Re-applying it here and again in
                        # MSG 111 was the source of doubled player damage.

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
                send_rpc_push(526, encode_sproto([(0, success), (1, index_id)]))
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
                dmg = get_val_int(body, 1)
                eff_id = body.get(2, b"").decode('utf-8')
                
                if picked_char and target_id == picked_char['id']:
                    is_area = (picked_char.get('map_id') == "502")
                    if is_area:
                        print(f"[AREA BOSS ATTACK] dmg={dmg} eff={eff_id}")
                    
                    new_hp = picked_char.get('hp', 0) - dmg
                    picked_char['hp'] = max(0, new_hp)
                    # HP is character state too; persist it immediately so a
                    # forced process stop cannot revive or reset the player.
                    save_chars(all_accounts_chars)
                    sync_char_attrs_rpc(conn, picked_char)
                    if picked_char['hp'] == 0 and picked_char.get('map_id') == '502':
                        print('[M1003 DEBUG] Player died in arena; scheduling loss return')
                        schedule_domin_return(restore_hp=True)
                    elif picked_char['hp'] == 0 and picked_char.get('exp_copy_state'):
                        offer_exp_stage_respawn()
                
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 111: # accept_damge
                if picked_char:
                    dlist_raw = body.get(0, b"")
                    dlist = decode_sproto_list(dlist_raw)
                    print(f"[COMBAT] RX 111 count={len(dlist)}")
                    for d_bytes in dlist:
                        d = decode_sproto(d_bytes)
                        target_id = get_val_int(d, 0)
                        dmg = get_val_int(d, 1)
                        # Tag 4 is bool cri. Sproto bool is encoded as int in header.
                        is_cri = get_val_int(d, 4, 0) == 1

                        print(f"[COMBAT] accept_damge target={target_id} dmg={dmg} cri={is_cri}")

                        if target_id == picked_char['id']:
                            # Damage to player
                            new_hp = picked_char.get('hp', 0) - dmg
                            picked_char['hp'] = max(0, new_hp)
                            # Save each accepted player-damage update.  This
                            # server is commonly stopped with kill -9, so
                            # shutdown-time persistence is not sufficient.
                            save_chars(all_accounts_chars)
                            sync_char_attrs_rpc(conn, picked_char)
                            if picked_char['hp'] == 0 and picked_char.get('map_id') == '502':
                                print('[M1003 DEBUG] Player died in arena; scheduling loss return')
                                schedule_domin_return(restore_hp=True)
                            elif picked_char['hp'] == 0 and picked_char.get('exp_copy_state'):
                                offer_exp_stage_respawn()
                        elif target_id in NPC_HP_MAP:
                            # Damage to NPC/Monster/Boss
                            NPC_HP_MAP[target_id] -= dmg
                            
                            # Synchronization of target HP to ensure bar update
                            target_nid = NPC_INST_MAP.get(target_id)
                            defender_stats = None
                            if target_nid:
                                # Resolve stats for syncing (Boss uses "1105", NPCs use nid)
                                nid_str = "1105" if target_nid.startswith("BOSS_") else target_nid
                                defender_stats = get_npc_attr(nid_str)
                                
                                # Send attribute update (Tag 510)
                                a_oth_fields = [(0, max(0, NPC_HP_MAP[target_id])), (2, defender_stats['lv'])]
                                if NPC_INST_MAP.get(target_id, '').startswith('BOSS_'):
                                    a_oth_fields.extend([(4, 1), (15, 2)])
                                a_oth = encode_sproto(a_oth_fields)
                                a_base = encode_sproto([(0, defender_stats['hp_max'])])
                                aoi_attr = encode_sproto([(0, target_id), (1, a_oth), (2, a_base)])
                                send_rpc_push(510, encode_sproto([(0, aoi_attr)]))

                            if NPC_HP_MAP[target_id] <= 0:
                                # Ensure death is processed exactly once
                                if target_id in DEAD_NPC_SET:
                                    continue
                                DEAD_NPC_SET.add(target_id)

                                # Exp Stage combat reaches zero HP through
                                # accept_damge before the APK emits its
                                # follow-up single_copy_scene_npc_die packet.
                                # Count the authoritative instance here so a
                                # duplicate notification cannot stall a wave.
                                if picked_char.get('exp_copy_state'):
                                    record_exp_stage_kill(target_id)

                                # BOSS DEATH HANDLING
                                if target_id == picked_char.get('boss_inst_id'):
                                    # Send final HP=0 sync before ending scene to trigger client animation
                                    if defender_stats:
                                        a_oth_fields = [(0, 0), (2, defender_stats['lv'])]
                                        if NPC_INST_MAP.get(target_id, '').startswith('BOSS_'):
                                            a_oth_fields.extend([(4, 1), (15, 2)])
                                        a_oth = encode_sproto(a_oth_fields)
                                        aoi_attr = encode_sproto([(0, target_id), (1, a_oth)])
                                        send_rpc_push(510, encode_sproto([(0, aoi_attr)]))
                                    
                                    did = picked_char.get('active_domin_id', '1')
                                    print(f"[M1003 DEBUG] Boss {target_id} killed by client dmg. Winning did={did}")
                                    # Do not send generic copy_scene_result (552) for Capture.
                                    advance_missions(picked_char, send_rpc_push, 'capture', target_id=did)
                                    picked_char['boss_inst_id'] = None
                                else:
                                    if not picked_char.get('exp_copy_state'):
                                        # Regular NPC/Monster visual drop (GTA-style)
                                        GLOBAL_INST_COUNTER += 1
                                        pos = picked_char['pos']
                                        # Resolve rewards based on NPC level
                                        reward_lv = defender_stats['lv']
                                        cash_amt = reward_lv * 50 + random.randint(10, 100)
                                        nested_item = encode_sproto([(0, "1001"), (1, cash_amt), (3, 1)])
                                        drop_data = encode_sproto([
                                            (0, GLOBAL_INST_COUNTER), (1, int(pos[0] + 50)), (2, int(pos[2] + 50)),
                                            (3, 1), (4, nested_item), (7, picked_char['id'])
                                        ])
                                        send_rpc_push(527, drop_data)

                                        if target_nid:
                                            advance_missions(picked_char, send_rpc_push, 'kill', target_id=target_nid)

                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 307 or msg == 127: # local_npc_die (307) or single_copy_scene_npc_die (127)
                npcid = None
                inst_id = None
                die_type = 0

                if msg == 307:
                    # local_npc_die: npcid(0), x(1), z(2), type(3)
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
                    # single_copy_scene_npc_die: characterId(0), npcdataid(1), pos_x(2), pos_z(3), type(4)
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

                # CRITICAL DOUBLE-DEATH PROTECTION
                is_duplicate = False
                if inst_id is not None and inst_id > 1000000: # Only deduplicate server-side instances
                    if inst_id in DEAD_NPC_SET: is_duplicate = True
                    else: DEAD_NPC_SET.add(inst_id)

                # Cleanup HP tracking
                if inst_id and inst_id in NPC_HP_MAP: del NPC_HP_MAP[inst_id]

                if picked_char and not is_duplicate:
                    # Experience Stage NPCs are server-instanced.  Count only
                    # those IDs, never a client-supplied NPC name or a local
                    # Street Race obstacle.
                    if msg == 127:
                        record_exp_stage_kill(inst_id)
                    # Car-robbery packets (type 2/6) usually have no NPC ID.
                    if die_type in [2, 6]:
                        advance_missions(picked_char, send_rpc_push, 'car', die_type=die_type)

                    if npcid and npcid != "None" and not picked_char.get('exp_copy_state'):
                        # Open-world NPC rewards.  Exp Stage has a dynamic
                        # server reward table (ShowRewardData 30301 contains
                        # no quantities), so never substitute fabricated
                        # per-kill cash/EXP there.
                        npc_stats = get_npc_attr(npcid)
                        reward_level = npc_stats['lv'] if 1 <= npc_stats['lv'] <= 200 else 1
                        exp_kill = reward_level * 20
                        cash_kill = reward_level * 100
                        picked_char['exp'] += exp_kill
                        picked_char['cash'] += cash_kill

                        # Send reward tip (Tag 638)
                        send_rpc_push(638, encode_sproto([(0, [
                            encode_sproto([(0, "2001"), (1, exp_kill), (3, 0)]),
                            encode_sproto([(0, "1001"), (1, cash_kill), (3, 0)])
                        ])]))

                        # GTA-style visual drop model on the ground
                        GLOBAL_INST_COUNTER += 1
                        pos = picked_char['pos']
                        nested_item = encode_sproto([(0, "1001"), (1, cash_kill), (3, 1)])
                        drop_data = encode_sproto([
                            (0, GLOBAL_INST_COUNTER), (1, int(pos[0] + 50)), (2, int(pos[2] + 50)),
                            (3, 1), (4, nested_item), (7, picked_char['id'])
                        ])
                        send_rpc_push(527, drop_data)

                        # Level up loop
                        while True:
                            lv = picked_char.get('level', 1)
                            rd = LEVEL_DATA.get(lv)
                            if rd and picked_char['exp'] >= rd['exp']:
                                picked_char['exp'] -= rd['exp']
                                picked_char['level'] = lv + 1
                                # Level Up: Fully Restore HP
                                new_stats = get_character_stats(picked_char)
                                picked_char['hp'] = new_stats['hp_max']
                                print(f"[LEVEL UP] CharID={picked_char['id']} NewLevel={picked_char['level']} HP Restored to {picked_char['hp']}")
                            else: break

                        advance_missions(picked_char, send_rpc_push, 'kill', target_id=npcid)

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
                        boss_stats = get_npc_attr('1105')
                        sync_npc_attrs_rpc(conn, boss_id, boss_stats, 0)
                        did = picked_char.get('active_domin_id', '1')
                        print(f"[M1003 DEBUG] RX 137 zombie died; winning did={did}")
                        # Capture wins update mission progress, not copy_scene_result (552).
                        advance_missions(picked_char, send_rpc_push, 'capture', target_id=did)
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
                    # RESET HP TO MAX FOR AREA DUEL
                    picked_char['hp'] = get_character_stats(picked_char)['hp_max']
                    # SAVE LATEST POSITION EXACTLY (Ensure list copy)
                    latest_pos = picked_char.get('pos', [34611, 100, -49480, 8632])
                    picked_char['pre_arena_pos'] = list(latest_pos)
                    print(f"[M1003 DEBUG] Saved pre-arena pos: {picked_char['pre_arena_pos']}")
                    picked_char['active_domin_id'] = did
                    start_map_transition(conn, picked_char, "502", send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 298: # impact_npc (Interaction / Vehicle hit)
                nid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[*] Impact/Interaction with NPC ID={nid}")
                if picked_char:
                    if nid == "1105": # Mission 1003 challenge
                        print("[M1003 DEBUG] RX 298 NPC=1105")
                        print("[M1003 DEBUG] TX 529 dialog=102098 NPC=1105")
                        send_rpc_push(529, encode_sproto([(0, "102098"), (1, True)]))
                    advance_missions(picked_char, send_rpc_push, 'impact', target_id=nid, die_type=3)
                    advance_missions(picked_char, send_rpc_push, 'interact', target_id=nid)

                    # Exact rewards from NPC impact / Street Race NPC hit
                    base_cash = 250 + random.randint(50, 150)
                    base_exp = 50 + random.randint(10, 30)
                    picked_char['cash'] = int(picked_char.get('cash', 0)) + base_cash
                    picked_char['exp'] = int(picked_char.get('exp', 0)) + base_exp

                    # Physical model drop for visual confirmation (GTA style)
                    GLOBAL_INST_COUNTER += 1
                    pos = picked_char['pos']
                    nested_item = encode_sproto([(0, "1001"), (1, base_cash), (3, 1)])
                    drop_data = encode_sproto([
                        (0, GLOBAL_INST_COUNTER), (1, int(pos[0] + 30)), (2, int(pos[2] + 30)),
                        (3, 1), (4, nested_item), (7, picked_char['id'])
                    ])
                    send_rpc_push(527, drop_data)

                    get_character_stats(picked_char)
                    sync_char_attrs_rpc(conn, picked_char)
                    send_rpc_push(519, sync_mission_data(picked_char))
                    save_chars(all_accounts_chars)
                    print(f"[IMPACT NPC REWARD] nid={nid} cash={base_cash} exp={base_exp}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 107: # enter_copy_scene
                copy_id = field_text(body, 0)
                cfg = COPY_SCENE_CONFIG.get(copy_id)
                if picked_char and cfg and cfg['subtype'] == 7:
                    remaining = copy_attempts_remaining(picked_char, copy_id, cfg)
                    if remaining > 0:
                        # CarRewardPageRootLogic's Repeat button sends this
                        # same request.  Keep the original city position so
                        # Continue/Exit always returns the player to city.
                        if picked_char.get('pre_copy_pos') is None:
                            picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                        state = ensure_daily_copy_state(picked_char)
                        state['remaining'][copy_id] = remaining - 1
                        picked_char['active_copy_id'] = copy_id
                        picked_char['street_race_return_scheduled'] = False
                        save_chars(all_accounts_chars)
                        start_map_transition(conn, picked_char, cfg['map_id'], send_rpc_push)
                        # The client decreases its local counter immediately;
                        # TAG 555 makes the authoritative remaining count
                        # survive a reconnect or a Retry.
                        send_rpc_push(555, sync_copy_scenes(picked_char))
                        print(f"[STREET RACE] entered id={copy_id} remaining={remaining - 1}/{cfg['max_plays']}")
                    else:
                        print(f"[STREET RACE] denied id={copy_id}; daily attempts exhausted")
                elif picked_char and cfg and cfg['subtype'] == 12:
                    remaining = copy_attempts_remaining(picked_char, copy_id, cfg)
                    exp_cfg = DAILY_EXP_CONFIG.get(copy_id)
                    if remaining > 0 and exp_cfg:
                        if picked_char.get('pre_copy_pos') is None:
                            picked_char['pre_copy_pos'] = list(picked_char.get('pos', [29860, 100, -17005, 0]))
                        state = ensure_daily_copy_state(picked_char)
                        state['remaining'][copy_id] = remaining - 1
                        picked_char['active_copy_id'] = copy_id
                        picked_char['exp_return_scheduled'] = False
                        picked_char['exp_copy_state'] = {'copy_id': copy_id, 'started': False, 'finished': False}
                        # Use Map 301 (Chinatown) or Map 306 (Rich Dist) to enable AI.
                        # These are SINGLE_KILL_MONSTER_COPY which allow NPCs to move/attack.
                        target_map = '301' if int(copy_id) >= 223 else '306'
                        start_map_transition(conn, picked_char, target_map, send_rpc_push)
                        send_rpc_push(555, sync_copy_scenes(picked_char))
                        save_chars(all_accounts_chars)
                        print(f"[EXP STAGE] entered id={copy_id} remaining={remaining - 1}/{cfg['max_plays']} map={target_map}")
                    else:
                        print(f"[EXP STAGE] denied id={copy_id}; attempts exhausted or configuration unavailable")
                elif picked_char:
                    # Other copy subtypes retain the server's existing map
                    # transition behavior until their individual flows are
                    # implemented and verified.
                    start_map_transition(conn, picked_char, copy_id, send_rpc_push)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 193: # car_chase_result
                active_copy_id = str(picked_char.get('active_copy_id') or '') if picked_char else ''
                cfg = COPY_SCENE_CONFIG.get(active_copy_id)
                won = get_val_int(body, 0, 0) == 1
                elapsed = get_val_int(body, 1, 0)
                if picked_char and cfg and cfg['subtype'] == 7:
                    state = ensure_daily_copy_state(picked_char)
                    best_times = state.setdefault('best_times', {})
                    old_time = best_times.get(active_copy_id)
                    new_record = won and (old_time is None or elapsed < int(old_time))
                    if won:
                        best_times[active_copy_id] = elapsed if new_record else int(old_time)
                    rewards = street_race_rewards(picked_char.get('level', 1)) if won else []
                    for item_id, _, amount in rewards:
                        add_to_inventory(picked_char, item_id, amount)
                    save_chars(all_accounts_chars)
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
                        # Mission 1004 is LogicType 102 / LogicID 102.  The
                        # APK considers a successfully completed Street Race
                        # the qualifying dungeon event; entering or failing
                        # the race must not advance it.
                        advance_missions(picked_char, send_rpc_push, 'interact', target_id='102')
                        send_rpc_push(611, sync_inventory_data(picked_char))
                    send_rpc_push(555, sync_copy_scenes(picked_char))
                    schedule_street_race_return()
                    print(f"[STREET RACE] result id={active_copy_id} win={won} time={elapsed} rewards={rewards}")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 220: # start_battle (sent by EXPSceneManager after arrival)
                if picked_char and picked_char.get('exp_copy_state'):
                    start_exp_stage_battle()
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 132: # relife_player
                if picked_char and picked_char.get('exp_copy_state'):
                    # isInplace is the sole request field in the APK protocol.
                    resolve_exp_stage_respawn(get_val_int(body, 0, 0) == 1)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [106, 246, 273, 207, 201, 322]:
                # Scene/Dungeon Entry
                mid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                print(f"[RX] Scene Entry: {mid} (MSG={msg})")
                if picked_char:
                    start_map_transition(conn, picked_char, mid, send_rpc_push)
                    advance_missions(picked_char, send_rpc_push, 'interact', target_id=mid)
                    if msg == 201: # world_boss
                        advance_missions(picked_char, send_rpc_push, 'world_boss')
                        send_rpc_push(552, encode_sproto([(0, 1), (1, mid), (2, True)]))
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # update_game_server
                # Tag 2 in response is the game_server list.
                # Tag 5 in game_server is serverPlayerState (-4=Normal).
                # Tag 10 in game_server is newServer (0=Old).
                server = encode_sproto([
                    (0, 302), (1, "EU-001"), (2, "s16.serv00.com"), (3, 15678),
                    (4, 1), (5, -4), (6, 1), (7, 1), (8, 1), (9, 1), (10, 0)
                ])
                resp = encode_sproto([(2, [server])])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 270: # download_finish
                if picked_char and not picked_char.get('download_complete'):
                    picked_char['download_complete'] = True
                    # Expansion Rewards: Mount 9301 (Chevrolet voucher), 9011 (10), 9001 (20), 5026 (5)
                    add_to_inventory(picked_char, "9301", 1)
                    add_to_inventory(picked_char, "9011", 10)
                    add_to_inventory(picked_char, "9001", 20)
                    add_to_inventory(picked_char, "5026", 5)
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

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 313, 319]:
                resp_data = encode_sproto([])
                if msg == 118: resp_data = encode_sproto([(0, f"User_{random.randint(100,999)}")])
                elif msg == 218: resp_data = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp_data)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if msg == 145 and picked_char:
                    send_rpc_push(555, sync_copy_scenes(picked_char))
                    print(f"[COPY] sent daily copy state level={picked_char.get('level', 1)}")

            elif msg == 310:  # request_domin_info
                print("[M1003 DEBUG] RX 310 request_domin_info")
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

                v_p = encode_sproto([
                    (0, "Q7MZK4RP"),
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
                mid = body.get(0, b"").decode('utf-8')
                if picked_char:
                    # Survey interaction sends msg 178
                    advance_missions(picked_char, send_rpc_push, 'interact', target_id=mid)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 119: # ask_pickup_item
                iid = body.get(0, b"").decode('utf-8') if isinstance(body.get(0), bytes) else str(body.get(0))
                if picked_char:
                    # Item pickup can advance Logic 3/4 missions
                    advance_missions(picked_char, send_rpc_push, 'pickup', target_id=iid)
                    add_to_inventory(picked_char, iid, 1)
                    send_rpc_push(611, sync_inventory_data(picked_char))
                if session is not None:
                    # Response: ret(0)=0 (Success)
                    resp = encode_sproto([(0, 0)])
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 108: # leave_copy_scene
                if picked_char:
                    # Street Race has its own Continue/Exit button.  Unlike
                    # the Capture arena it does not auto-return after five
                    # seconds, and it must restore the city position saved
                    # when the race was entered.
                    if picked_char.get('exp_copy_state'):
                        saved_pos = picked_char.get('pre_copy_pos')
                        picked_char['pre_copy_pos'] = None
                        picked_char['active_copy_id'] = None
                        picked_char['exp_copy_state'] = None
                        picked_char['exp_return_scheduled'] = False
                        picked_char['hp'] = get_character_stats(picked_char)['hp_max']
                    elif picked_char.get('active_copy_id'):
                        saved_pos = picked_char.get('pre_copy_pos')
                        picked_char['pre_copy_pos'] = None
                        picked_char['active_copy_id'] = None
                        picked_char['street_race_return_scheduled'] = False
                    else:
                        saved_pos = picked_char.get('pre_arena_pos')
                        picked_char['pre_arena_pos'] = None
                    start_map_transition(conn, picked_char, "11", send_rpc_push, override_pos=saved_pos)
                    save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (ATG MISSION SYSTEM REBUILT)");
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
