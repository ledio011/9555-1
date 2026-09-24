import socket
import struct
import threading
import random
import json
import os
import time
import traceback
import math
from pathlib import Path

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


def scan_and_read_decompiled_folder():
    """
    Scans and reads all project data files and decompiled assets,
    logging live progress: Reading X/Y files...
    """
    potential_paths = [
        os.path.join(SCRIPT_DIR, "assets"),
        os.path.join(SCRIPT_DIR, "apk_index"),
        os.path.join(SCRIPT_DIR, "Decompiled"),
        os.path.join(SCRIPT_DIR, "Atg_Auto", "Decompiled"),
        os.path.expanduser("~/Downloads/Atg_Auto/Decompiled"),
        "C:/Users/User/Downloads/Atg_Auto/Decompiled",
        "C:/Users/User/Downloads/dec&normal/Decompiled"
    ]

    valid_dirs = [p for p in potential_paths if os.path.exists(p) and os.path.isdir(p)]

    all_files = []
    for d in valid_dirs:
        for root, dirs, files in os.walk(d):
            for file in files:
                full_p = os.path.join(root, file)
                if full_p not in all_files:
                    all_files.append(full_p)

    total_files = len(all_files)
    print("=" * 60, flush=True)
    print(f"[RAM PRELOAD START] Scanning game assets & code...", flush=True)
    print(f"[RAM PRELOAD] Total files found: {total_files}", flush=True)
    print("=" * 60, flush=True)

    RAM_FILE_CACHE = {}

    for x, file_path in enumerate(all_files, start=1):
        rel_path = os.path.relpath(file_path, SCRIPT_DIR)

        # Print Reading X/Y files progress
        if x % 20 == 0 or x == total_files or x <= 10:
            print(f"Reading {x}/{total_files} files: {rel_path}", flush=True)

        try:
            with open(file_path, "rb") as f:
                content = f.read()
                RAM_FILE_CACHE[rel_path] = content
        except Exception as e:
            pass

    print("=" * 60, flush=True)
    print(f"[DECOMPILED READ COMPLETE] Read {len(RAM_FILE_CACHE)}/{total_files} files into RAM!", flush=True)
    print("=" * 60, flush=True)


# Run full decompiled RAM preload on server startup
scan_and_read_decompiled_folder()

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


def load_game_assets():
    global missions_data, rewards_data, LEVEL_DATA, MONSTER_DATA, STATIC_NPC_DATA
    global NPC_CONFIG, MAP_CONFIG, MAP_CONNECT_DATA, GUILD_CAPTURE_DATA
    global KILL_TARGET_SPAWNS, TARGET_CAR_SPAWNS, EFF_CONFIG, SKILL_CONFIG
    global MOUNT_CONFIG, COPY_SCENE_CONFIG, SHOW_REWARD_CONFIG
    global STREET_RACE_REWARD_BY_LEVEL, ITEM_CONFIG

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
                        'birth': parts[8],
                    }

    print(f"[DATA LOADED] Levels={len(LEVEL_DATA)} Items={len(ITEM_CONFIG)} Maps={len(MAP_CONFIG)}", flush=True)


load_game_assets()


# ============================================================
# DATABASE LOAD & SAVE
# ============================================================

def load_chars():
    data = {}
    db_file = CHAR_DB if os.path.exists(CHAR_DB) else BAK_DB if os.path.exists(BAK_DB) else None
    if db_file:
        try:
            with open(db_file, "r", encoding="utf-8") as f:
                raw = json.load(f)
                if isinstance(raw, dict):
                    data = raw
                elif isinstance(raw, list):
                    data = {"1": {"user_default": raw}}
        except Exception as e:
            print(f"[WARN] Error loading database: {e}", flush=True)
    return data


def save_chars(data):
    if not isinstance(data, dict) or not data:
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
        print(f"[ERROR] save_chars failed: {e}", flush=True)


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
    if not isinstance(all_chars, dict):
        return []
    area_key = str(area_id)
    acc_dict = all_chars.get(area_key)
    if acc_dict is None and isinstance(area_id, int):
        acc_dict = all_chars.get(area_id)

    if isinstance(acc_dict, dict):
        res = acc_dict.get(acc_id, [])
        return res if isinstance(res, list) else []
    elif isinstance(acc_dict, list):
        return acc_dict
    return []


def send_response(conn, msg, session, custom_data=None):
    if session is None:
        return
    frame = schema_engine.create_response_frame(msg, session, custom_data)
    if frame:
        conn.sendall(struct.pack(">H", len(frame)) + frame)


def build_char_overview(c, idx=0):
    c_name = c.get('name', 'Hero')
    c_prof = c.get('prof', 0)
    c_id = c.get('id', idx + 1)
    gen = encode_sproto([(0, c_name), (1, c_prof), (2, 1), (3, str(c.get('map_id', '11')))])
    attr = encode_sproto([(0, c.get('level', 1)), (1, 3000)])
    v = encode_sproto([(0, c_name), (1, "104"), (2, "QJ_A_T"), (3, "QJ_A_S"), (4, "QJ_A_X"), (5, "QJ_A_WQ")])
    return encode_sproto([(0, c_id), (1, gen), (2, attr), (3, v), (4, idx), (5, 0)])


# ============================================================
# CLIENT HANDLER THREAD
# ============================================================

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}", flush=True)
    acc_id = "user_default"
    picked_char = None
    cur_areaId = "1"

    def send_rpc_push(tag, data):
        try:
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            print(f"[TX PUSH] Tag={tag} Size={len(data)}", flush=True)
        except Exception as e:
            print(f"[!] Push error tag={tag}: {e}", flush=True)

    try:
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

            print(f"[RX] MSG={msg} SESSION={session}", flush=True)

            # --------------------------------------------------------
            # STATEFUL GAME HANDLERS
            # --------------------------------------------------------

            if msg == 4:  # Login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1, "user_default"))
                sid = get_val_int(body, 5, 1)
                cur_areaId = str(get_area_id(sid))

                # Exact login.response fields according to Sproto C# Schema:
                # Tag 0: type = 2 (SUCCESS)
                # Tag 1: versionCode = "1.012.017"
                # Tag 2: dataVersionCode = "205"
                # Tag 3: serverLevel = 1
                send_response(conn, msg, session, {
                    "type": 2,
                    "versionCode": "1.012.017",
                    "dataVersionCode": "205",
                    "serverLevel": 1
                })
                print(f"[LOGIN SUCCESS] Account={acc_id} Area={cur_areaId}", flush=True)

            elif msg == 118:  # request_random_name
                random_name = f"Hero_{random.randint(100, 999)}"
                send_response(conn, msg, session, {"name": random_name})
                print(f"[RANDOM NAME] Granted name: {random_name}", flush=True)

            elif msg == 104:  # character_create
                c_data_raw = body.get(0)
                c_name = "Hero"
                c_prof = 0
                if c_data_raw:
                    c_data = decode_sproto(c_data_raw)
                    c_name_val = c_data.get(0)
                    if isinstance(c_name_val, bytes):
                        c_name = c_name_val.decode('utf-8', errors='replace')
                    elif isinstance(c_name_val, str):
                        c_name = c_name_val
                    c_prof = get_val_int(c_data, 1, 0)

                cid = int(time.time() * 1000) % 1000000000
                area_key = str(cur_areaId)
                if area_key not in all_accounts_chars or not isinstance(all_accounts_chars[area_key], dict):
                    all_accounts_chars[area_key] = {}
                if acc_id not in all_accounts_chars[area_key] or not isinstance(all_accounts_chars[area_key][acc_id], list):
                    all_accounts_chars[area_key][acc_id] = []

                nc = {'id': cid, 'name': c_name, 'prof': c_prof, 'level': 1, 'hp': 1000, 'map_id': "11", 'pos': [7007, 100, 5033, 0]}
                all_accounts_chars[area_key][acc_id].append(nc)
                save_chars(all_accounts_chars)

                ov = build_char_overview(nc, 0)
                send_response(conn, msg, session, {"character": ov, "errno": 0})
                print(f"[CHARACTER CREATED] id={cid} name={c_name} prof={c_prof}", flush=True)

            elif msg == 103:  # character_list
                chars = get_account_chars(all_accounts_chars, cur_areaId, acc_id)
                ov_list = [build_char_overview(c, i) for i, c in enumerate(chars) if isinstance(c, dict)]
                send_response(conn, msg, session, {"character": ov_list})
                print(f"[CHARACTER LIST] Returned {len(ov_list)} characters.", flush=True)

            elif msg == 105:  # character_pick
                char_id = get_val_int(body, 0)
                chars = get_account_chars(all_accounts_chars, cur_areaId, acc_id)
                picked_char = next((c for c in chars if isinstance(c, dict) and c.get('id') == char_id), None)
                send_response(conn, msg, session, {"result": 0 if picked_char else 1})

                if picked_char:
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (4, 10000)]))
                    send_rpc_push(654, encode_sproto([(0, 1)]))
                    print(f"[CHARACTER PICK] Picked character id={char_id}", flush=True)

            elif msg == 101:  # move
                p_raw = body.get(0)
                if p_raw and picked_char:
                    pd = decode_sproto(p_raw)
                    picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                    save_chars(all_accounts_chars)
                send_response(conn, msg, session, {"pos": p_raw})

            elif session is not None:
                # Automatic Sproto Schema Fallback
                send_response(conn, msg, session)
                print(f"[AUTO-RESPONSE] Fulfilled MSG={msg} SESSION={session} via Schema Engine.", flush=True)

    except Exception as e:
        print(f"[-] Client exception {addr}: {e}", flush=True)
        traceback.print_exc()
    finally:
        conn.close()


def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind(("0.0.0.0", PORT))
    server.listen(128)
    print("=" * 60, flush=True)
    print(f"GAME SERVER 9555 READY ON PORT {PORT}", flush=True)
    print("Zero-lag In-Memory Sproto Engine active!", flush=True)
    print("=" * 60, flush=True)

    while True:
        try:
            cl, ad = server.accept()
            threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
        except Exception as e:
            print(f"[!] Accept error: {e}", flush=True)


if __name__ == "__main__":
    start_server()
