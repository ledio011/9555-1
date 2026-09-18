import os
import json
import time
import random
import socket
import struct
import threading
import traceback
import math

# ============================================================
# ATG GAME SERVER 9555 - AUTHENTIC REVIVAL
# Reconstructed from Assembly-CSharp / Protocol.cs / Sproto
# ============================================================

HOST = "0.0.0.0"
PORT = 15678

DB_FILE = os.environ.get("CHAR_DB", "characters.json")
ACCOUNT_FILE = os.environ.get("ACCOUNT_DB", "accounts.json")

GAME_VERSION = "1.012.017"
DATA_VERSION = "205"

DEFAULT_MAP = "11" # Tutorial map is the true starting point
DEFAULT_LINE = 1

DB_LOCK = threading.RLock()
ONLINE_LOCK = threading.RLock()

ONLINE_PLAYERS = {}
NPC_STATE = {}
NEXT_NPC_INSTANCE = 3000000

# ============================================================
# SPROTO ENCODER (Corrected for Primitive Lists)
# ============================================================

def encode_sproto(fields):
    if not fields:
        return struct.pack("<H", 0)

    fields = sorted(fields, key=lambda x: x[0])

    header = []
    body = bytearray()
    last_tag = -1

    for tag, value in fields:
        skip = tag - last_tag - 1
        if skip > 0:
            header.append(2 * (skip - 1) + 1)

        if value is None:
            header.append(1)
        elif isinstance(value, bool):
            header.append(4 if value else 2)
        elif isinstance(value, int):
            if 0 <= value <= 32766:
                header.append((value + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= value <= 2147483647:
                    body.extend(struct.pack("<I", 4))
                    body.extend(struct.pack("<i", value))
                else:
                    body.extend(struct.pack("<I", 8))
                    body.extend(struct.pack("<q", value))
        elif isinstance(value, str):
            header.append(0)
            payload = value.encode("utf-8")
            body.extend(struct.pack("<I", len(payload)))
            body.extend(payload)
        elif isinstance(value, (bytes, bytearray)):
            header.append(0)
            payload = bytes(value)
            body.extend(struct.pack("<I", len(payload)))
            body.extend(payload)
        elif isinstance(value, list):
            header.append(0)
            payload = bytearray()
            if len(value) > 0 and isinstance(value[0], int):
                # Integer List Format: [Size][ElemSize][Data...]
                body.extend(struct.pack("<I", len(value) * 8 + 1))
                body.extend(struct.pack("<B", 8)) # Use 8-byte for safety
                for v in value:
                    body.extend(struct.pack("<q", v))
            else:
                # Object/String List Format: [Size][Length][Data...]
                for item in value:
                    if isinstance(item, bytes):
                        payload.extend(struct.pack("<I", len(item)))
                        payload.extend(item)
                    elif isinstance(item, str):
                        s_payload = item.encode("utf-8")
                        payload.extend(struct.pack("<I", len(s_payload)))
                        payload.extend(s_payload)
                body.extend(struct.pack("<I", len(payload)))
                body.extend(payload)

        last_tag = tag

    fn = len(header)
    res = bytearray(struct.pack("<H", fn))
    for h in header:
        res.extend(struct.pack("<H", h))
    res.extend(body)
    return res

def decode_sproto(data):
    if len(data) < 2: return {}
    fn = struct.unpack("<H", data[:2])[0]
    offset = 2 + fn * 2
    body = data[offset:]

    fields = {}
    last_tag = -1
    body_offset = 0

    for i in range(fn):
        h = struct.unpack("<H", data[2+i*2 : 4+i*2])[0]
        if h & 1:
            last_tag += (h // 2) + 1
            continue

        last_tag += 1
        tag = last_tag

        if h == 0:
            if body_offset + 4 > len(body): break
            sz = struct.unpack("<I", body[body_offset : body_offset+4])[0]
            body_offset += 4
            val = body[body_offset : body_offset+sz]
            body_offset += sz
            fields[tag] = val
        else:
            fields[tag] = (h // 2) - 1

    return fields

# ============================================================
# CHARACTER STRUCTURES (Authoritative Tag Mappings)
# ============================================================

def build_general(c):
    return encode_sproto([
        (0, c.get("name", "Hero")),
        (1, c.get("prof", 0)),
        (3, str(c.get("map_id", DEFAULT_MAP))),
        (4, 1 if c.get("tutorial_done") else 0)
    ])

def build_attribute_overview(c):
    return encode_sproto([
        (0, c.get("level", 1)),
        (1, power(c))
    ])

def build_visual(c):
    prof = c.get("prof", 0)
    # Correct Bundle IDs from CreateRoleRootLogic.cs
    # 0=XD, 1=QJ, 2=NQS
    models = {
        0: ["10001", "XD_A_T", "XD_A_S", "XD_A_X", "XD_A_WQ"],
        1: ["10002", "QJ_A_T", "QJ_A_S", "QJ_A_X", "QJ_A_WQ"],
        2: ["10003", "NQS_A_T", "NQS_A_S", "NQS_A_X", "NQS_A_WQ"]
    }
    m = models.get(prof, models[0])
    return encode_sproto([
        (0, c.get("name", "Hero")),
        (1, m[0]), # ModeId
        (2, m[1]), # HeadId
        (3, m[2]), # BodyId
        (4, m[3]), # LegId
        (5, m[4])  # WeaponId
    ])

def build_attribute_other(c):
    return encode_sproto([
        (0, c.get("hp", 3000)),
        (1, c.get("exp", 0)),
        (2, c.get("level", 1)),
        (3, power(c)),
        (4, 1), # title_level
        (14, c.get("vip", 0)),
        (16, c.get("pk_mode", 0))
    ])

def build_property(c):
    # Authoritative tags from property.cs: 13-18
    return encode_sproto([
        (13, c.get("cash", 1000)),
        (14, c.get("gold", 0)),
        (15, c.get("diamond", 0)),
        (16, c.get("battle_coin", 0))
    ])

def character_overview(c):
    return encode_sproto([
        (0, c["id"]),
        (1, build_general(c)),
        (2, build_attribute_overview(c)),
        (3, build_visual(c)),
        (4, c.get("createtime", int(time.time()))),
        (5, 0) # forbidden
    ])

def full_character(c):
    # character.cs mappings
    return encode_sproto([
        (0, c["id"]),
        (1, build_general(c)),
        (2, build_attribute_other(c)),
        (5, build_property(c)),
        (6, build_visual(c)),
        (7, movement_data(c.get("pos", [0, 0, 0, 0]))),
        (8, build_skill_map(c)),
        (9, build_equip_map(c)),
        (15, 2) # download finish
    ])

def character_aoi(c):
    # character_aoi.cs mappings
    return encode_sproto([
        (0, c["id"]),
        (1, build_visual(c)),
        (2, build_general(c)),
        (3, build_attribute_other(c)),
        (5, movement_data(c.get("pos", [0, 0, 0, 0])))
    ])

def movement_data(pos):
    # movement.cs -> position.cs
    p = encode_sproto([
        (0, int(pos[0])),
        (1, int(pos[1])),
        (2, int(pos[2])),
        (3, int(pos[3]))
    ])
    return encode_sproto([(0, p)])

# ============================================================
# SKILLS & INVENTORY
# ============================================================

def build_skill_map(c):
    skills = []
    for sid, level in c.get("skill_levels", {}).items():
        skills.append(encode_sproto([
            (0, sid),
            (1, level),
            (2, 0) # indexPos
        ]))
    return skills # Sproto Map is encoded as List of objects

def build_equip_map(c):
    equips = []
    # Simplified: just base weapon if new
    if not c.get("equips"):
        prof = c.get("prof", 0)
        weapon_ids = {0: "1001", 1: "2001", 2: "3001"}
        equips.append(encode_sproto([
            (0, 1), # indexId
            (1, weapon_ids[prof]), # itemId
            (5, 1), # stack
            (6, 1), # quality
            (7, [0]*8) # parm
        ]))
    return equips

def sync_inventory(c):
    items = []
    for item in c.get("inventory", []):
        items.append(encode_sproto([
            (0, int(item.get("index", 1))),
            (1, str(item["id"])),
            (5, int(item["count"]))
        ]))
    return encode_sproto([(0, items)])

# ============================================================
# WORLD & NPC
# ============================================================

def power(c):
    return int(c.get("atk", 100) * 16 + c.get("hp_max", 1000))

def npc_create_packet(npc):
    # npc_attribute.cs flat coordinates (15, 16, 17)
    attr = encode_sproto([
        (0, npc["id"]),
        (1, str(npc["definition"])),
        (2, npc["hp"]),
        (3, npc["hp_max"]),
        (15, int(npc["pos"][0])),
        (16, int(npc["pos"][2])), # Sproto uses Z for Tag 16
        (17, int(npc["pos"][3]))
    ])
    return encode_sproto([(0, attr)])

# ============================================================
# PACKET HELPERS
# ============================================================

def make_packet(msg_type, body, session=None):
    pkg = [(0, msg_type)]
    if session is not None:
        pkg.append((1, session))

    header = encode_sproto(pkg)
    full = header + body
    return struct.pack(">H", len(full)) + full

def send_push(conn, msg_type, body):
    conn.sendall(make_packet(msg_type, body))

def send_reply(conn, session, body):
    # Session reply: package has session but NO type
    pkg = [(1, session)]
    header = encode_sproto(pkg)
    full = header + body
    conn.sendall(struct.pack(">H", len(full)) + full)

# ============================================================
# DATABASE
# ============================================================

CHARACTERS = {}
ACCOUNTS = {}

def load_db():
    global CHARACTERS, ACCOUNTS
    try:
        if os.path.exists(DB_FILE):
            with open(DB_FILE, "r") as f: CHARACTERS = json.load(f)
        if os.path.exists(ACCOUNT_FILE):
            with open(ACCOUNT_FILE, "r") as f: ACCOUNTS = json.load(f)
    except: pass

def save_db():
    with DB_LOCK:
        with open(DB_FILE, "w") as f: json.dump(CHARACTERS, f)
        with open(ACCOUNT_FILE, "w") as f: json.dump(ACCOUNTS, f)

def init_character(c):
    c.update({
        "level": 1,
        "hp": 3000, "hp_max": 3000,
        "atk": 150, "def": 50,
        "cash": 1000,
        "map_id": DEFAULT_MAP,
        "pos": [7007, 100, 5033, 0], # Tutorial Map BirthPos
        "skill_levels": {"1001": 1},
        "inventory": []
    })

# ============================================================
# RPC HANDLERS
# ============================================================

def handle_login(conn, body, session):
    acc_id = decode_sproto(body).get(1)
    if not acc_id: return
    acc_id = acc_id.decode("utf-8")

    # login.response (Tag 4)
    res = encode_sproto([
        (0, 1), # type: 1 = character list
        (5, GAME_VERSION),
        (6, DATA_VERSION)
    ])
    send_reply(conn, session, res)
    return acc_id

def handle_char_list(conn, acc_id, session):
    char_ids = ACCOUNTS.get(acc_id, [])
    char_data = [character_overview(CHARACTERS[str(cid)]) for cid in char_ids]
    send_reply(conn, session, encode_sproto([(0, char_data)]))

def handle_char_create(conn, acc_id, session, body):
    data = decode_sproto(decode_sproto(body).get(0))
    name = data.get(0).decode("utf-8")
    prof = data.get(1, 0)

    cid = int(time.time() * 1000)
    c = {"id": cid, "name": name, "prof": prof}
    init_character(c)

    CHARACTERS[str(cid)] = c
    ACCOUNTS.setdefault(acc_id, []).append(cid)
    save_db()

    send_reply(conn, session, encode_sproto([
        (0, character_overview(c)),
        (1, 0) # errno
    ]))

def handle_char_pick(conn, acc_id, session, body):
    cid = decode_sproto(body).get(0)
    c = CHARACTERS.get(str(cid))
    if not c: return

    send_reply(conn, session, encode_sproto([(0, 0)]))

    # AUTHORITATIVE SEQUENCE:
    # 1. Push Player Data
    send_push(conn, 504, encode_sproto([
        (0, full_character(c)),
        (1, movement_data(c["pos"]))
    ]))
    # 2. Transition to Map
    send_push(conn, 503, encode_sproto([
        (0, str(c["map_id"])),
        (1, DEFAULT_LINE),
        (2, 1) # line count
    ]))

    with ONLINE_LOCK:
        ONLINE_PLAYERS[cid] = {"conn": conn, "char": c}
    return cid

def handle_map_ready(conn, cid):
    c = ONLINE_PLAYERS[cid]["char"]
    # 3. Finalize entry
    send_push(conn, 654, encode_sproto([(0, 1)])) # state=1

    # 4. Sync World Objects
    # (Simplified: spawn some static NPCs)
    for i in range(2):
        npc = {"id": 5000+i, "definition": "1101", "hp": 1000, "hp_max": 1000, "pos": [7100+i*100, 100, 5100, 0]}
        send_push(conn, 509, npc_create_packet(npc))

def handle_move(cid, body):
    data = decode_sproto(body)
    mv_raw = data.get(0)
    if not mv_raw: return

    mv = decode_sproto(mv_raw)
    pos_raw = mv.get(0)
    if not pos_raw: return

    p = decode_sproto(pos_raw)
    new_pos = [p.get(0), p.get(1), p.get(2), p.get(3)]

    c = ONLINE_PLAYERS[cid]["char"]
    c["pos"] = new_pos

    # Correct AOI update move (nested character_aoi_move)
    update = encode_sproto([
        (0, cid),
        (1, movement_data(new_pos)),
        (2, False) # walk
    ])

    pkg = encode_sproto([(0, update)])
    mid = c["map_id"]

    with ONLINE_LOCK:
        for ocid, other in ONLINE_PLAYERS.items():
            if ocid != cid and other["char"]["map_id"] == mid:
                send_push(other["conn"], 507, pkg)

# ============================================================
# SERVER LOOP
# ============================================================

def client_handler(conn):
    acc_id = None
    cid = None
    try:
        while True:
            head = conn.recv(2)
            if not head: break
            size = struct.unpack(">H", head)[0]
            data = conn.recv(size)
            if not data: break

            pkg = decode_sproto(data)
            msg = pkg.get(0)
            session = pkg.get(1)
            body = data[len(encode_sproto([(0, msg or 0), (1, session or 0)])):] # Roughly

            # Sproto decoding is safer with full packet parsing
            full_pkg = decode_sproto(data)
            msg = full_pkg.get(0)

            # Map handlers
            if msg == 4: acc_id = handle_login(conn, data, session)
            elif msg == 103: handle_char_list(conn, acc_id, session)
            elif msg == 104: handle_char_create(conn, acc_id, session, data)
            elif msg == 118:
                send_reply(conn, session, encode_sproto([(0, f"Hero_{random.randint(100,999)}")]))
            elif msg == 105: cid = handle_char_pick(conn, acc_id, session, data)
            elif msg == 100: handle_map_ready(conn, cid)
            elif msg == 101: handle_move(cid, data)
            elif msg == 218: # Heartbeat
                send_reply(conn, session, encode_sproto([(0, int(time.time()*1000))]))

    except:
        traceback.print_exc()
    finally:
        if cid:
            with ONLINE_LOCK: ONLINE_PLAYERS.pop(cid, None)
        conn.close()

def main():
    load_db()
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind((HOST, PORT))
    s.listen(5)
    print(f"ATG Revival Server running on {PORT}...")
    while True:
        conn, addr = s.accept()
        threading.Thread(target=client_handler, args=(conn,), daemon=True).start()

if __name__ == "__main__":
    main()
