import socket
import struct
import threading
import random
import json
import os
import time
import traceback
import math

from schema_engine import (
    encode_sproto,
    sproto_pack,
    sproto_unpack,
    SchemaResponseEngine
)

# ============================================================
# SERVER CONFIG & RAM PRELOAD
# ============================================================

PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
CHAR_DB = os.path.join(SCRIPT_DIR, "characters_final.json")
BAK_DB = CHAR_DB + ".bak"
TMP_DB = CHAR_DB + ".tmp"
RESOURCE_ROOT = os.path.join(SCRIPT_DIR, "assets")

GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {}
NPC_HP_MAP = {}
NPC_SPAWNED_MAPS = {}
DEAD_NPC_SET = set()
ONLINE_CHAR_MAP = {}

# Initialize Schema Engine for Auto-Responses
schema_engine = SchemaResponseEngine(index_dir="apk_index")

# Memory Cache for Data Tables
missions_data = {}
rewards_data = {}
LEVEL_DATA = {}
MONSTER_DATA = {}
STATIC_NPC_DATA = {}
NPC_CONFIG = {}
MAP_CONFIG = {}
MAP_CONNECT_DATA = {}
GUILD_CAPTURE_DATA = {}
KILL_TARGET_SPAWNS = {}
TARGET_CAR_SPAWNS = {}
EFF_CONFIG = {}
SKILL_CONFIG = {}
MOUNT_CONFIG = {}
COPY_SCENE_CONFIG = {}
SHOW_REWARD_CONFIG = {}
STREET_RACE_REWARD_BY_LEVEL = {}
ITEM_CONFIG = {}
EQUIP_MODEL_CONFIG = {}
EQUIP_STATS_CONFIG = {}
BADGE_STATS_CONFIG = {}
DAILY_EXP_CONFIG = {}


def load_game_assets():
    global missions_data, rewards_data, LEVEL_DATA, MONSTER_DATA, STATIC_NPC_DATA
    global NPC_CONFIG, MAP_CONFIG, MAP_CONNECT_DATA, GUILD_CAPTURE_DATA
    global KILL_TARGET_SPAWNS, TARGET_CAR_SPAWNS, EFF_CONFIG, SKILL_CONFIG
    global MOUNT_CONFIG, COPY_SCENE_CONFIG, SHOW_REWARD_CONFIG
    global STREET_RACE_REWARD_BY_LEVEL, ITEM_CONFIG, EQUIP_MODEL_CONFIG
    global EQUIP_STATS_CONFIG, BADGE_STATS_CONFIG, DAILY_EXP_CONFIG

    md_path = os.path.join(SCRIPT_DIR, "missions.json")
    rd_path = os.path.join(SCRIPT_DIR, "mission_rewards.json")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f:
            missions_data = json.load(f)
    if os.path.exists(rd_path):
        with open(rd_path, "r", encoding='utf-8') as f:
            rewards_data = json.load(f)

    def is_data(line):
        return line.startswith("*,") or ("," in line and line.split(",")[1].isdigit())

    text_asset_root = os.path.join(SCRIPT_DIR, "assets", "Bundle", "TextAsset")
    if not os.path.isdir(text_asset_root):
        text_asset_root = os.path.join(SCRIPT_DIR, "assets", "Bundle", "TextAssets")

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
                        if i + 1 < len(parts) and parts[i].isdigit():
                            adds[int(parts[i])] = int(parts[i + 1])
                    EFF_CONFIG[eid] = {
                        'dmg_fixed': int(parts[3]) if parts[3].isdigit() else 0,
                        'dmg_fixed_add': int(parts[4]) if parts[4].isdigit() else 0,
                        'dmg_multi': int(parts[5]) if parts[5].isdigit() else 0,
                        'dmg_multi_add': int(parts[6]) if parts[6].isdigit() else 0,
                        'adds': adds
                    }

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

    # Load BaseLvData
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
                        'hp_abs': int(parts[45]) if len(parts) > 45 and parts[45].isdigit() else 0,
                        'atk_abs': int(parts[44]) if len(parts) > 44 and parts[44].isdigit() else 0,
                        'def_abs': int(parts[46]) if len(parts) > 46 and parts[46].isdigit() else 0
                    }

    # Load MonsterData
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
                        if mid not in STATIC_NPC_DATA:
                            STATIC_NPC_DATA[mid] = []
                        STATIC_NPC_DATA[mid].append(entry)
                    else:
                        if mid not in MONSTER_DATA:
                            MONSTER_DATA[mid] = []
                        MONSTER_DATA[mid].append(entry)

    # Load MountData
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

    # Load CopySceneData
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

    # Load ItemData
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

    print(f"[DATA LOADED] Levels={len(LEVEL_DATA)} Items={len(ITEM_CONFIG)} Maps={len(MAP_CONFIG)} Mounts={len(MOUNT_CONFIG)}")


# Load game assets into RAM on server startup
load_game_assets()


# ============================================================
# DATABASE LOAD & SAVE
# ============================================================

def load_chars():
    if os.path.exists(CHAR_DB):
        try:
            with open(CHAR_DB, "r", encoding="utf-8") as f:
                return json.load(f)
        except Exception:
            pass
    if os.path.exists(BAK_DB):
        try:
            with open(BAK_DB, "r", encoding="utf-8") as f:
                return json.load(f)
        except Exception:
            pass
    return {}


def save_chars(data):
    if not data:
        return
    try:
        with open(TMP_DB, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=4)
            f.flush()
            os.fsync(f.fileno())
        if os.path.exists(CHAR_DB):
            try:
                with open(CHAR_DB, "r", encoding="utf-8") as src, open(BAK_DB, "w", encoding="utf-8") as dst:
                    dst.write(src.read())
            except Exception:
                pass
        os.replace(TMP_DB, CHAR_DB)
    except Exception as e:
        print(f"[ERROR] save_chars failed: {e}")


all_accounts_chars = load_chars()


# ============================================================
# HELPER FUNCTIONS
# ============================================================

def decode_sproto(data, offset=0):
    if len(data) < offset + 2:
        return {}
    fn = struct.unpack("<H", data[offset:offset + 2])[0]
    h_ptr, b_ptr = offset + 2, offset + 2 + fn * 2
    fields, curr_tag = {}, -1
    for i in range(fn):
        v = struct.unpack("<H", data[h_ptr + i * 2: h_ptr + i * 2 + 2])[0]
        if v == 0:
            curr_tag += 1
            if b_ptr + 4 <= len(data):
                l = struct.unpack("<I", data[b_ptr:b_ptr + 4])[0]
                fields[curr_tag] = data[b_ptr + 4:b_ptr + 4 + l]
                b_ptr += 4 + l
        elif v == 1:
            curr_tag += 1
        elif v & 1:
            curr_tag += (v >> 1) + 1
        else:
            curr_tag += 1
            fields[curr_tag] = (v >> 1) - 1
    return fields


def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None:
        return default
    if isinstance(val, int):
        return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4:
            return struct.unpack("<i", val)[0]
        if len(val) == 8:
            return struct.unpack("<q", val)[0]
        if len(val) == 1:
            return val[0]
    return default


def get_area_id(serverId):
    try:
        sid = int(serverId)
        if sid == 1 or (300 <= sid < 400): return 1
        if sid == 2 or (600 <= sid < 700): return 2
        if sid == 3 or (10 <= sid < 100): return 0
    except Exception:
        pass
    return 0


def get_account_chars(all_chars, area_id, acc_id):
    area_key = str(area_id)
    acc_dict = all_chars.get(area_key) or all_chars.get(area_id)
    if isinstance(acc_dict, dict):
        return acc_dict.get(acc_id, [])
    return []


# ============================================================
# CLIENT HANDLER THREAD
# ============================================================

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}")
    acc_id = "0"
    picked_char = None
    cur_areaId = 0

    def send_rpc_push(tag, data):
        try:
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            print(f"[TX PUSH] Tag={tag} Size={len(data)}")
        except Exception as e:
            print(f"[!] Push error tag={tag}: {e}")

    try:
        # Check HTTP vs Binary Sproto
        initial = conn.recv(4, socket.MSG_PEEK)
        if initial.startswith(b"GET ") or initial.startswith(b"HEAD"):
            conn.sendall(b"HTTP/1.1 200 OK\r\nConnection: close\r\n\r\n")
            conn.close()
            return

        while True:
            h_bytes = conn.recv(2)
            if not h_bytes:
                break
            size = struct.unpack(">H", h_bytes)[0]

            data = b""
            while len(data) < size:
                chunk = conn.recv(size - len(data))
                if not chunk:
                    break
                data += chunk
            if len(data) < size:
                break

            raw = sproto_unpack(data)
            pkg = decode_sproto(raw, 0)
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)

            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2)
            body = decode_sproto(raw, off)

            print(f"[RX] MSG={msg} SESSION={session}")

            # --------------------------------------------------------
            # STATEFUL GAME HANDLERS
            # --------------------------------------------------------
            if msg == 4:  # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1)
                cur_areaId = str(get_area_id(sid))
                resp = encode_sproto([
                    (0, 2), (1, "1.012.017"), (2, "205"), (3, 1),
                    (4, 10000), (12, random.randint(1, 10000))
                ])
                if session is not None:
                    ph = encode_sproto([(1, session)])
                    pf = sproto_pack(ph + resp)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103:  # character_list
                chars = get_account_chars(all_accounts_chars, cur_areaId, acc_id)
                ov_list = []
                for i, c in enumerate(chars):
                    gen = encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('map_id', '11')))])
                    attr = encode_sproto([(0, c.get('level', 1)), (1, 3000)])
                    v = encode_sproto([(0, c.get('name', 'Hero')), (1, "104"), (2, "QJ_A_T"), (3, "QJ_A_S"), (4, "QJ_A_X"), (5, "QJ_A_WQ")])
                    ov = encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, v), (4, i), (5, 0)])
                    ov_list.append(ov)
                resp = encode_sproto([(0, ov_list)])
                if session is not None:
                    ph = encode_sproto([(1, session)])
                    pf = sproto_pack(ph + resp)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105:  # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in get_account_chars(all_accounts_chars, cur_areaId, acc_id) if c['id'] == char_id), None)
                resp = encode_sproto([]) if picked_char else encode_sproto([(0, 1)])
                if session is not None:
                    ph = encode_sproto([(1, session)])
                    pf = sproto_pack(ph + resp)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

                if picked_char:
                    # Sync initial state
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (4, 10000)]))
                    send_rpc_push(654, encode_sproto([(0, 1)]))

            elif msg == 101:  # move
                p_raw = body.get(0)
                if p_raw and picked_char:
                    pd = decode_sproto(p_raw)
                    picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                    save_chars(all_accounts_chars)
                if session is not None:
                    ph = encode_sproto([(1, session)])
                    pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif session is not None:
                # ----------------------------------------------------
                # AUTOMATIC SPROTO SCHEMA FALLBACK FOR ALL OTHER MESSAGES
                # ----------------------------------------------------
                response_frame = schema_engine.create_response_frame(msg, session)
                if response_frame:
                    conn.sendall(response_frame)
                    print(f"[AUTO-RESPONSE] Fulfilled MSG={msg} SESSION={session} via Schema Engine.")

    except Exception as e:
        print(f"[-] Client exception {addr}: {e}")
        traceback.print_exc()
    finally:
        conn.close()


def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind(("0.0.0.0", PORT))
    server.listen(128)
    print("=" * 60)
    print(f"GAME SERVER 9555 READY ON PORT {PORT}")
    print("Zero-lag In-Memory Sproto Engine active!")
    print("=" * 60)

    while True:
        try:
            cl, ad = server.accept()
            threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
        except Exception as e:
            print(f"[!] Accept error: {e}")


if __name__ == "__main__":
    start_server()
