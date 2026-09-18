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
# ATG GAME SERVER 9555
# Rebuilt from available Assembly-CSharp / Sproto / TextAsset
# Server-side implementation only.
# ============================================================

HOST = "0.0.0.0"
PORT = 15678

DB_FILE = os.environ.get("CHAR_DB", "characters.json")
ACCOUNT_FILE = os.environ.get("ACCOUNT_DB", "accounts.json")

GAME_VERSION = "1.012.017"
DATA_VERSION = "205"

DEFAULT_MAP = "101"
DEFAULT_LINE = 0

DB_LOCK = threading.RLock()
ONLINE_LOCK = threading.RLock()

ONLINE_PLAYERS = {}
NPC_STATE = {}
NEXT_NPC_INSTANCE = 3000000

# ============================================================
# SPROTO
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

        elif isinstance(value, float):
            header.append(0)
            payload = struct.pack("<f", value)
            body.extend(struct.pack("<I", len(payload)))
            body.extend(payload)

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

            for item in value:
                if isinstance(item, bytes):
                    payload.extend(struct.pack("<I", len(item)))
                    payload.extend(item)
                elif isinstance(item, bytearray):
                    item = bytes(item)
                    payload.extend(struct.pack("<I", len(item)))
                    payload.extend(item)
                elif isinstance(item, str):
                    b = item.encode()
                    payload.extend(struct.pack("<I", len(b)))
                    payload.extend(b)

            body.extend(struct.pack("<I", len(payload)))
            body.extend(payload)

        elif isinstance(value, dict):
            header.append(0)
            payload = json.dumps(value, separators=(",", ":")).encode()
            body.extend(struct.pack("<I", len(payload)))
            body.extend(payload)

        else:
            header.append(0)
            payload = str(value).encode()
            body.extend(struct.pack("<I", len(payload)))
            body.extend(payload)

        last_tag = tag

    out = bytearray()
    out.extend(struct.pack("<H", len(header)))

    for h in header:
        out.extend(struct.pack("<H", h))

    out.extend(body)

    return bytes(out)


def decode_sproto(data, offset=0):
    if not data or len(data) < offset + 2:
        return {}

    try:
        fn = struct.unpack_from("<H", data, offset)[0]

        header_start = offset + 2
        body_start = header_start + fn * 2

        if body_start > len(data):
            return {}

        result = {}
        tag = -1
        body_pos = body_start

        for i in range(fn):
            h = struct.unpack_from(
                "<H",
                data,
                header_start + i * 2
            )[0]

            if h == 0:
                tag += 1

                if body_pos + 4 > len(data):
                    result[tag] = b""
                    continue

                length = struct.unpack_from(
                    "<I",
                    data,
                    body_pos
                )[0]

                body_pos += 4

                end = min(body_pos + length, len(data))
                result[tag] = data[body_pos:end]
                body_pos = end

            elif h == 1:
                tag += 1

            elif h & 1:
                tag += (h >> 1) + 1

            else:
                tag += 1
                result[tag] = (h >> 1) - 1

        return result

    except Exception:
        return {}


def sproto_pack(data):
    out = bytearray()

    for i in range(0, len(data), 8):
        chunk = data[i:i + 8]

        if len(chunk) < 8:
            chunk += b"\x00" * (8 - len(chunk))

        mask = 0

        for j in range(8):
            if chunk[j] != 0:
                mask |= 1 << j

        if mask == 0xff:
            out.append(0xff)
            out.append(0)
            out.extend(chunk)
        else:
            out.append(mask)

            for j in range(8):
                if mask & (1 << j):
                    out.append(chunk[j])

    return bytes(out)


def sproto_unpack(data):
    out = bytearray()
    pos = 0

    while pos < len(data):
        mask = data[pos]
        pos += 1

        if mask == 0xff:
            if pos >= len(data):
                break

            count = (data[pos] + 1) * 8
            pos += 1

            out.extend(data[pos:pos + count])
            pos += count
            continue

        for bit in range(8):
            if mask & (1 << bit):
                if pos < len(data):
                    out.append(data[pos])
                    pos += 1
            else:
                out.append(0)

    return bytes(out)


def intval(v, default=0):
    if v is None:
        return default

    if isinstance(v, bool):
        return int(v)

    if isinstance(v, int):
        return v

    if isinstance(v, bytes):
        try:
            return int(v.decode())
        except Exception:
            try:
                return struct.unpack("<i", v[:4])[0]
            except Exception:
                return default

    try:
        return int(v)
    except Exception:
        return default


def textval(v, default=""):
    if v is None:
        return default

    if isinstance(v, bytes):
        try:
            return v.decode("utf-8")
        except Exception:
            return ""

    return str(v)


# ============================================================
# FRAME / RPC
# ============================================================

def make_packet(tag, session=None, body=None):
    if body is None:
        body = encode_sproto([])

    header = encode_sproto([(0, tag)])

    if session is not None:
        header = encode_sproto([
            (0, tag),
            (1, session)
        ])

    packed = sproto_pack(header + body)

    return struct.pack(">H", len(packed)) + packed


def make_response(session, body=None):
    if body is None:
        body = encode_sproto([])

    packet = encode_sproto([(1, session)]) + body
    packed = sproto_pack(packet)

    return struct.pack(">H", len(packed)) + packed


def send_push(conn, tag, body):
    try:
        conn.sendall(make_packet(tag, None, body))
        print(f"[PUSH] Tag {tag}")
    except Exception:
        pass


def send_reply(conn, session, body=None):
    try:
        conn.sendall(make_response(session, body))
    except Exception:
        pass


def broadcast_map(map_id, tag, body, exclude_conn=None):
    map_id = str(map_id)
    with ONLINE_LOCK:
        for pid, data in ONLINE_PLAYERS.items():
            if str(data["map"]) == map_id and data["conn"] != exclude_conn:
                try:
                    data["conn"].sendall(make_packet(tag, None, body))
                except Exception:
                    pass


# ============================================================
# DATABASE
# ============================================================

def load_json(path, default):
    if not os.path.exists(path):
        return default

    try:
        with open(path, "r", encoding="utf-8") as f:
            return json.load(f)
    except Exception:
        return default


def save_json(path, data):
    tmp = path + ".tmp"

    try:
        with open(tmp, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=2, ensure_ascii=False)

        os.replace(tmp, path)

    except Exception as e:
        print("[DB ERROR]", e)


accounts = load_json(ACCOUNT_FILE, {})
characters_db = load_json(DB_FILE, {})


def save_characters():
    with DB_LOCK:
        save_json(DB_FILE, characters_db)


# ============================================================
# CHARACTER DATABASE
# ============================================================

def character_bucket(account_id):
    account_id = str(account_id)

    with DB_LOCK:
        if account_id not in characters_db:
            characters_db[account_id] = []

        return characters_db[account_id]


def generate_character_id():
    with DB_LOCK:
        used = set()

        for chars in characters_db.values():
            for c in chars:
                try:
                    used.add(int(c.get("id")))
                except Exception:
                    pass

        while True:
            # Numeric 13-17 digit character ID.
            cid = random.randint(
                1000000000000,
                99999999999999999
            )

            if cid not in used:
                return cid


def init_character(c):
    now = int(time.time())

    defaults = {
        "id": generate_character_id(),
        "name": "Hero",
        "prof": 0,

        "level": 1,
        "exp": 0,

        "cash": 1000,
        "gold": 0,
        "diamond": 0,
        "battle_coin": 0,

        "hp": 3000,
        "hp_max": 3000,

        "atk": 150,
        "def": 50,
        "hit": 100,
        "eva": 50,
        "cri": 20,
        "res": 10,

        "crd": 15000,
        "crr": 0,
        "exd": 0,
        "exr": 0,

        "map_id": DEFAULT_MAP,
        "line_index": DEFAULT_LINE,

        "pos": [6600, 200, -2371, 0],

        "skill_levels": {},
        "inventory": [],
        "mount_id": "",

        "active_missions": {},
        "completed_side_missions": [],
        "last_main_mission_id": "-1",

        "friends": [],
        "foes": [],
        "mail": [],

        "last_played": now,
        "create_time": now
    }

    for k, v in defaults.items():
        if k not in c:
            c[k] = v

    return c


# ============================================================
# MAP DATA
# ============================================================

MAPS = {
    "11": {
        "scene": "DSJ_GTA",
        "name": "Tutorial",
        "birth": [29860, 100, -17005, 0]
    },

    "101": {
        "scene": "DSJ_zhuCheng",
        "name": "Main City",
        "birth": [6600, 200, -2371, 0]
    },

    "102": {
        "scene": "DSJ_pinMinKu_1",
        "name": "Slum",
        "birth": [2800, 100, 5000, 0]
    },

    "104": {
        "scene": "DSJ_duCheng_1",
        "name": "Casino",
        "birth": [700, 100, -2100, 0]
    },

    "105": {
        "scene": "DSJ_shangYeZhongXin_1",
        "name": "Commercial Center",
        "birth": [1000, 200, -1000, 0]
    },

    "106": {
        "scene": "DSJ_haiBian_1",
        "name": "Seaside",
        "birth": [1000, 100, 1000, 0]
    },

    "107": {
        "scene": "DSJ_zhongGuoCheng_1",
        "name": "Chinatown",
        "birth": [-4200, 100, -3400, 0]
    },

    "108": {
        "scene": "DSJ_fuRenQu_1",
        "name": "Rich Area",
        "birth": [0, 100, 0, 0]
    },

    "109": {
        "scene": "DSJ_pkWanFa_1",
        "name": "PVP",
        "birth": [0, 100, 0, 0]
    }
}


def map_spawn(map_id):
    map_id = str(map_id)

    cfg = MAPS.get(map_id)

    if cfg:
        return list(cfg["birth"])

    return [6600, 200, -2371, 0]


def movement_data(pos):
    x = intval(pos[0])
    y = intval(pos[1])
    z = intval(pos[2])
    r = intval(pos[3])

    return encode_sproto([
        (0, x),
        (1, y),
        (2, z),
        (3, r)
    ])


# ============================================================
# CHARACTER SPROTO
# ============================================================

def build_general(c):
    return encode_sproto([
        (0, c.get("name", "Hero")),
        (1, c.get("prof", 0)),
        (3, c.get("map_id", "101")),
        (4, 1)  # tutorial finish
    ])


def build_attribute_overview(c):
    return encode_sproto([
        (0, c.get("level", 1)),
        (1, power(c))  # combValue
    ])


def build_attribute(c):
    # Logic of attribute.cs: Tags 0-7
    return encode_sproto([
        (0, c.get("hp_max", 3000)),
        (1, c.get("exp", 0)),
        (2, c.get("atk", 150)),
        (3, c.get("def", 50)),
        (4, c.get("hit", 100)),
        (5, c.get("eva", 50)),
        (6, c.get("cri", 20)),
        (7, c.get("res", 10))
    ])


def build_attribute_other(c):
    # Logic of attribute_other.cs: Tags 0-18
    return encode_sproto([
        (0, c.get("hp", 3000)),
        (1, c.get("exp", 0)),
        (2, c.get("level", 1)),
        (3, power(c)),    # combValue
        (4, 1),           # title_level
        (5, 0),           # title_exp
        (6, -1),          # guildId
        (7, 0),           # guildJob
        (8, ""),          # guildName
        (14, 0),          # vip
        (15, 0),          # camp
        (16, 0),          # pkMode
        (17, 0)           # dance_state
    ])


def build_visual(c):
    # Logic of characterVisual.cs: Tags 0-3 for appearance strings
    # IDs must be strings matching Bundle keys
    return encode_sproto([
        (1, "10001"),  # ModeId
        (2, "20001"),  # head
        (3, "30001"),  # body
        (4, "40001")   # leg
    ])


def build_property(c):
    return encode_sproto([
        (0, c.get("cash", 0)),
        (1, c.get("gold", 0)),
        (2, c.get("diamond", 0)),
        (3, c.get("battle_coin", 0)),
        (4, 0),
        (5, 0)
    ])


def character_overview(c):
    # Logic of character_overview.cs: Tags 0-5
    return encode_sproto([
        (0, c.get("id", 0)),
        (1, build_general(c)),
        (2, build_attribute_overview(c)),
        (3, build_visual(c)),
        (4, c.get("create_time", 0)),
        (5, 0)  # forbidden
    ])


def full_character(c):
    # Logic of character.cs authoritative tags: 0, 1, 2, 5, 6, 7, 8, 12, 13, 15, 16
    return encode_sproto([
        (0, c.get("id", 0)),
        (1, build_general(c)),
        (2, build_attribute_other(c)),  # Tag 2
        (5, build_property(c)),         # Tag 5
        (6, build_visual(c)),           # Tag 6
        (7, movement_data(c.get("pos", [0, 0, 0, 0]))),  # Tag 7
        (15, 2)                         # download is Tag 15
    ])


def character_aoi(c):
    return encode_sproto([
        (0, c.get("id", 0)),
        (1, build_visual(c)),
        (2, build_general(c)),
        (3, build_attribute_other(c)),
        (5, movement_data(c.get("pos", [0, 0, 0, 0])))
    ])


# ============================================================
# ATTRIBUTES
# ============================================================

def sync_attributes(c):
    return encode_sproto([
        (0, c.get("hp", 0)),
        (1, c.get("hp_max", 0)),
        (2, c.get("atk", 0)),
        (3, c.get("def", 0)),
        (4, c.get("hit", 0)),
        (5, c.get("eva", 0)),
        (6, c.get("cri", 0)),
        (7, c.get("res", 0)),
        (8, c.get("crd", 15000)),
        (9, c.get("crr", 0)),
        (10, c.get("exd", 0)),
        (11, c.get("exr", 0))
    ])


def power(c):
    return int(
        c.get("atk", 100) * 16 +
        c.get("hp_max", 1000) +
        c.get("def", 50) * 11 +
        c.get("hit", 100) * 2 +
        c.get("eva", 50) * 5.5 +
        c.get("cri", 20) * 10 +
        c.get("res", 10) * 10
    )


# ============================================================
# INVENTORY
# ============================================================

def inventory_add(c, item_id, count):
    item_id = str(item_id)
    count = int(count)

    for item in c.setdefault("inventory", []):
        if str(item.get("id")) == item_id:
            item["count"] = int(item.get("count", 0)) + count
            return

    c["inventory"].append({
        "id": item_id,
        "count": count,
        "state": 1
    })


def inventory_remove(c, item_id, count):
    item_id = str(item_id)
    count = int(count)

    for item in c.get("inventory", []):
        if str(item.get("id")) == item_id:
            current = int(item.get("count", 0))

            if current < count:
                return False

            item["count"] = current - count

            if item["count"] <= 0:
                c["inventory"].remove(item)

            return True

    return False


def sync_inventory(c):
    entries = []

    for item in c.get("inventory", []):
        entries.append(
            encode_sproto([
                (0, str(item.get("id", ""))),
                (1, int(item.get("count", 0))),
                (2, int(item.get("state", 1)))
            ])
        )

    return encode_sproto([
        (0, entries)
    ])


# ============================================================
# SKILLS
# ============================================================

PROF_SKILLS = {
    0: ["1001", "1002", "1003", "1004"],
    1: ["2001", "2002", "2003", "2004"],
    2: ["3001", "3002", "3003", "3004"]
}

SKILL_UNLOCK_LEVELS = [1, 5, 10, 15]


def build_skill_sync(c):
    prof = int(c.get("prof", 0))
    levels = c.get("skill_levels", {})

    skills = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    result = []

    for i, sid in enumerate(skills):
        required = SKILL_UNLOCK_LEVELS[
            min(i, len(SKILL_UNLOCK_LEVELS) - 1)
        ]

        level = int(levels.get(sid, 1))

        result.append(
            encode_sproto([
                (0, sid),
                (1, level),
                (2, 1 if c.get("level", 1) >= required else 0)
            ])
        )

    return encode_sproto([
        (0, result),
        (1, False)
    ])


# ============================================================
# MISSION ENGINE
# ============================================================

MISSIONS = {
    "1001": {
        "class": 1,
        "logic_type": 0,
        "require_num": 1,
        "next_id": "1002"
    },

    "1002": {
        "class": 1,
        "logic_type": 1,
        "target_id": "1101",
        "require_num": 1,
        "next_id": "1003"
    },

    "1003": {
        "class": 1,
        "logic_type": 25,
        "require_num": 1,
        "next_id": ""
    }
}


def accept_mission(c, mission_id):
    mission_id = str(mission_id)

    cfg = MISSIONS.get(mission_id)

    if not cfg:
        return False

    active = c.setdefault("active_missions", {})

    if mission_id in active:
        return False

    active[mission_id] = {
        "state": 1,
        "parm": [0] * 8,
        "accept_time": int(time.time())
    }

    return True


def mission_sync(c):
    entries = []

    for mid, m in c.get("active_missions", {}).items():
        parm = m.get("parm", [0] * 8)

        entries.append(
            encode_sproto([
                (0, str(mid)),
                (1, int(m.get("state", 1))),
                (2, parm[0] if parm else 0),
                (3, int(m.get("accept_time", 0)))
            ])
        )

    return encode_sproto([
        (0, entries)
    ])


def mission_progress(c, npc_id=None, die_type=0):
    changed = False

    for mid, mission in list(
            c.get("active_missions", {}).items()
    ):
        cfg = MISSIONS.get(str(mid))

        if not cfg:
            continue

        logic = cfg.get("logic_type")

        valid = False

        if logic in (1, 4, 11, 17, 23):
            if logic == 17:
                valid = True
            elif str(cfg.get("target_id")) == str(npc_id):
                valid = True

        elif logic == 25:
            valid = True

        elif logic in (19, 24):
            valid = die_type in (2, 6)

        elif logic == 20:
            valid = die_type == 3

        if not valid:
            continue

        parm = mission.setdefault("parm", [0] * 8)

        parm[0] += 1

        required = int(cfg.get("require_num", 1))

        if parm[0] >= required:
            mission["state"] = 2

        changed = True

    return changed


def complete_mission(c, mid):
    mid = str(mid)

    mission = c.get("active_missions", {}).get(mid)

    if not mission:
        return False

    if int(mission.get("state", 0)) != 2:
        return False

    cfg = MISSIONS.get(mid)

    if not cfg:
        return False

    c["exp"] = int(c.get("exp", 0)) + 100
    c["cash"] = int(c.get("cash", 0)) + 500

    if cfg.get("class") == 1:
        c["last_main_mission_id"] = mid

        next_id = cfg.get("next_id")

        if next_id:
            accept_mission(c, next_id)

    else:
        try:
            c.setdefault(
                "completed_side_missions", []
            ).append(int(mid))
        except Exception:
            pass

    del c["active_missions"][mid]

    return True


# ============================================================
# NPC SYSTEM
# ============================================================

NPC_DEFINITIONS = {
    "1101": {
        "name": "Mission NPC",
        "level": 1,
        "hp": 1000,
        "atk": 100,
        "def": 50,
        "pos": [6700, 200, -2400, 0]
    },

    "1105": {
        "name": "Mission Character",
        "level": 1,
        "hp": 1500,
        "atk": 100,
        "def": 50,
        "pos": [6800, 200, -2400, 0]
    },

    "9909": {
        "name": "Police",
        "level": 1,
        "hp": 1200,
        "atk": 120,
        "def": 60,
        "pos": [7000, 200, -2300, 0]
    }
}


def create_npc(map_id, definition_id):
    global NEXT_NPC_INSTANCE

    cfg = NPC_DEFINITIONS.get(
        str(definition_id),
        NPC_DEFINITIONS["1101"]
    )

    with ONLINE_LOCK:
        NEXT_NPC_INSTANCE += 1
        instance_id = NEXT_NPC_INSTANCE

        NPC_STATE[instance_id] = {
            "id": instance_id,
            "definition": str(definition_id),
            "map_id": str(map_id),
            "hp": int(cfg["hp"]),
            "hp_max": int(cfg["hp"]),
            "level": int(cfg["level"]),
            "pos": list(cfg["pos"])
        }

    return NPC_STATE[instance_id]


def npc_create_packet(npc):
    # Logic of npc_attribute.cs
    attr = encode_sproto([
        (0, npc["id"]),
        (1, npc["definition"]),
        (2, npc["hp"]),
        (3, npc["hp_max"]),
        (4, intval(npc.get("atk", 100))),
        (5, intval(npc.get("def", 50))),
        (6, movement_data(npc["pos"]))
    ])
    # Logic of npc_create.cs: Tag 0 is npc_attribute
    return encode_sproto([(0, attr)])


def spawn_map_npcs(conn, map_id):
    # Core mission / city NPC set.
    definitions = [
        "1101",
        "1105"
    ]

    for definition_id in definitions:
        npc = create_npc(map_id, definition_id)

        send_push(
            conn,
            509,
            npc_create_packet(npc)
        )


def initial_sync(conn, c):
    # 614 sync_common_data
    send_push(conn, 614, function_sync())
    # 611 sync_item_pack
    send_push(conn, 611, sync_inventory(c))
    # 592 sync_backpack_item
    send_push(conn, 592, encode_sproto([(0, {})]))
    # 616 sync_fashion_backpack_item
    send_push(conn, 616, encode_sproto([(0, {})]))
    # 540 sync_skill_info
    send_push(conn, 540, build_skill_sync(c))
    # 519 sync_mission
    send_push(conn, 519, mission_sync(c))


def enter_map(conn, c):
    map_id = str(c.get("map_id", DEFAULT_MAP))

    if map_id not in MAPS:
        map_id = DEFAULT_MAP
        c["map_id"] = map_id

    if not c.get("pos"):
        c["pos"] = map_spawn(map_id)

    save_characters()

    # 503 enter_map: Client will start loading scene
    send_push(
        conn,
        503,
        encode_sproto([
            (0, map_id),
            (1, int(c.get("line_index", 0))),
            (2, 1)
        ])
    )


def transition_map(conn, c, map_id):
    map_id = str(map_id)

    if map_id not in MAPS:
        map_id = DEFAULT_MAP

    c["map_id"] = map_id
    c["line_index"] = 0
    c["pos"] = map_spawn(map_id)

    save_characters()

    enter_map(conn, c)


# ============================================================
# WORLD SYNC
# ============================================================

def function_sync():
    ids = [
        "100",
        "107",
        "108",
        "3001",
        "3010",
        "3013",
        "3014",
        "3015",
        "3030",
        "4014",
        "4026",
        "4061",
        "4064",
        "4081",
        "4084"
    ]

    functions = []

    for fid in ids:
        functions.append(
            encode_sproto([
                (0, fid),
                (1, 1)
            ])
        )

    return encode_sproto([
        (0, int(time.time())),
        (2, 0),
        (9, functions),
        (13, 1),
        (14, int(time.time()))
    ])


# ============================================================
# LOGIN / SESSION
# ============================================================

def resolve_account(body):
    candidates = [
        body.get(1),
        body.get(0),
        body.get(2),
        body.get(5)
    ]

    for value in candidates:
        if value is None:
            continue

        value = textval(value)

        if value:
            return value

    return ""


# ============================================================
# CHARACTER HANDLERS
# ============================================================

def handle_character_list(conn, session, account_id):
    chars = character_bucket(account_id)

    chars.sort(
        key=lambda c: int(c.get("last_played", 0)),
        reverse=True
    )

    overview = [
        character_overview(c)
        for c in chars
    ]

    send_reply(
        conn,
        session,
        encode_sproto([
            (0, overview)
        ])
    )


def handle_character_create(
        conn,
        session,
        account_id,
        body
):
    raw = body.get(0)

    if raw:
        data = decode_sproto(raw)

        name = textval(
            data.get(0),
            f"Hero_{random.randint(100,999)}"
        )

        prof = intval(
            data.get(1),
            0
        )
    else:
        name = f"Hero_{random.randint(100,999)}"
        prof = 0

    chars = character_bucket(account_id)

    if len(chars) >= 3:
        send_reply(
            conn,
            session,
            encode_sproto([
                (1, 1)
            ])
        )
        return

    c = {
        "id": generate_character_id(),
        "name": name[:24],
        "prof": prof
    }

    init_character(c)

    chars.append(c)

    save_characters()

    send_reply(
        conn,
        session,
        encode_sproto([
            (0, character_overview(c)),
            (1, 0)
        ])
    )


def find_character(account_id, char_id):
    chars = character_bucket(account_id)

    for c in chars:
        if intval(c.get("id")) == intval(char_id):
            return c

    return None


# ============================================================
# GAME CLIENT
# ============================================================

def client_handler(conn, addr):
    print(f"[+] GAME CONNECTION {addr}")

    account_id = ""
    selected = None
    session_last = None

    try:
        conn.settimeout(120)

        while True:
            header = recv_exact(conn, 2)

            if not header:
                break

            size = struct.unpack(">H", header)[0]

            if size <= 0:
                continue

            packed = recv_exact(conn, size)

            if not packed:
                break

            raw = sproto_unpack(packed)

            packet = decode_sproto(raw)

            msg = intval(packet.get(0), -1)
            session = packet.get(1)

            session_last = session

            offset = 2

            if len(raw) >= 2:
                fn = struct.unpack_from(
                    "<H",
                    raw,
                    0
                )[0]

                offset = 2 + fn * 2

            body = decode_sproto(
                raw,
                offset
            )

            print(
                f"[RX] TAG={msg} SESSION={session}"
            )

            # ------------------------------------------------
            # 4 LOGIN
            # ------------------------------------------------

            if msg == 4:
                account_id = resolve_account(body)

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, 2),
                        (1, GAME_VERSION),
                        (2, DATA_VERSION),
                        (3, 1)
                    ])
                )

                print(
                    f"[LOGIN] account={account_id}"
                )

            # ------------------------------------------------
            # 103 CHARACTER LIST
            # ------------------------------------------------

            elif msg == 103:
                handle_character_list(
                    conn,
                    session,
                    account_id
                )

            # ------------------------------------------------
            # 104 CHARACTER CREATE
            # ------------------------------------------------

            elif msg == 104:
                handle_character_create(
                    conn,
                    session,
                    account_id,
                    body
                )

            # ------------------------------------------------
            # 105 CHARACTER PICK
            # ------------------------------------------------

            elif msg == 105:
                char_id = intval(
                    body.get(0)
                )

                selected = find_character(
                    account_id,
                    char_id
                )

                if selected:
                    init_character(selected)

                    selected["last_played"] = int(
                        time.time()
                    )

                    if not selected.get(
                            "active_missions"
                    ):
                        accept_mission(
                            selected,
                            "1001"
                        )

                    save_characters()

                    with ONLINE_LOCK:
                        ONLINE_PLAYERS[
                            str(selected["id"])
                        ] = {
                            "conn": conn,
                            "char": selected,
                            "map": selected.get(
                                "map_id",
                                DEFAULT_MAP
                            )
                        }

                # Response Tag 0 is errno. 0 = success.
                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, 0 if selected else 1)
                    ])
                )

                if selected:
                    initial_sync(
                        conn,
                        selected
                    )

                    # Initial push to start enter game flow
                    send_push(
                        conn,
                        654,
                        encode_sproto([
                            (0, 1)
                        ])
                    )

                    enter_map(
                        conn,
                        selected
                    )

            # ------------------------------------------------
            # 100 MAP READY
            # ------------------------------------------------

            elif msg == 100:
                if selected:
                    print(f"[MAP READY] {selected.get('map_id')}")

                    # 1. Authoritative player creation for local client (Tag 504 main_player_create)
                    # Logic of main_player_create.cs: Tag 0 is character, Tag 1 is movement
                    send_push(
                        conn,
                        504,
                        encode_sproto([
                            (0, full_character(selected)),
                            (1, movement_data(selected["pos"]))
                        ])
                    )

                    # 2. Tell me who else is here (Other Players)
                    # aoi_add.request: Tag 0 is character_aoi
                    with ONLINE_LOCK:
                        for pid, pdata in ONLINE_PLAYERS.items():
                            if str(pdata["map"]) == str(selected["map_id"]) and str(pid) != str(selected["id"]):
                                send_push(conn, 505, encode_sproto([(0, character_aoi(pdata["char"]))]))

                    # 3. Tell others I arrived (Tag 505 aoi_add)
                    # aoi_add.request: Tag 0 is character_aoi
                    broadcast_map(selected["map_id"], 505, encode_sproto([(0, character_aoi(selected))]), exclude_conn=conn)

                    # 4. Spawn NPCs
                    spawn_map_npcs(conn, selected["map_id"])

                    # Final signal to enter gameplay
                    send_push(
                        conn,
                        654,
                        encode_sproto([(0, 1)])
                    )

            # ------------------------------------------------
            # 270 DOWNLOAD FINISH
            # ------------------------------------------------

            elif msg == 270:
                if selected:
                    inventory_add(
                        selected,
                        "9011",
                        10
                    )

                    inventory_add(
                        selected,
                        "9001",
                        20
                    )

                    inventory_add(
                        selected,
                        "5026",
                        5
                    )

                    selected["mount_id"] = "1001"

                    save_characters()

                    send_push(
                        conn,
                        611,
                        sync_inventory(
                            selected
                        )
                    )

                    send_push(
                        conn,
                        654,
                        encode_sproto([
                            (0, 1)
                        ])
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 101 MOVE
            # ------------------------------------------------

            elif msg == 101:
                movement_raw = body.get(0)

                if selected and movement_raw:
                    movement_obj = decode_sproto(
                        movement_raw
                    )

                    pos = [
                        intval(movement_obj.get(0)),
                        intval(movement_obj.get(1)),
                        intval(movement_obj.get(2)),
                        intval(movement_obj.get(3))
                    ]

                    selected["pos"] = pos
                    selected["last_played"] = int(
                        time.time()
                    )

                    save_characters()

                    # Echo movement.
                    send_reply(
                        conn,
                        session,
                        encode_sproto([
                            (0, movement_raw)
                        ])
                    )

                    # AOI movement (Tag 507 aoi_update_move)
                    # aoi_update_move.request: Tag 0 is character (type character_aoi_move)
                    # character_aoi_move: Tag 0 is id, Tag 1 is movement
                    char_move = encode_sproto([
                        (0, selected["id"]),
                        (1, movement_raw)
                    ])
                    broadcast_map(selected["map_id"], 507, encode_sproto([(0, char_move)]), exclude_conn=conn)

                else:
                    send_reply(
                        conn,
                        session
                    )

            # ------------------------------------------------
            # 218 HEARTBEAT
            # ------------------------------------------------

            elif msg == 218:
                client_time = intval(
                    body.get(0),
                    0
                )

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, client_time),
                        (1, int(time.time()))
                    ])
                )

            # ------------------------------------------------
            # 118 RANDOM NAME
            # ------------------------------------------------

            elif msg == 118:
                names = [
                    "John",
                    "Mary",
                    "William",
                    "Michael",
                    "James",
                    "David",
                    "Chris",
                    "Lisa",
                    "Robert",
                    "Alex"
                ]

                name = (
                        random.choice(names)
                        + "_"
                        + str(random.randint(100, 999))
                )

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, name)
                    ])
                )

            # ------------------------------------------------
            # 112 ACCEPT MISSION
            # ------------------------------------------------

            elif msg == 112:
                mid = textval(
                    body.get(0)
                )

                result = 0

                if selected:
                    result = 1 if accept_mission(
                        selected,
                        mid
                    ) else 0

                    save_characters()

                    send_push(
                        conn,
                        519,
                        mission_sync(
                            selected
                        )
                    )

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, result)
                    ])
                )

            # ------------------------------------------------
            # 113 COMPLETE MISSION
            # ------------------------------------------------

            elif msg == 113:
                mid = textval(
                    body.get(0)
                )

                result = 0

                if selected:
                    result = 1 if complete_mission(
                        selected,
                        mid
                    ) else 0

                    save_characters()

                    send_push(
                        conn,
                        519,
                        mission_sync(
                            selected
                        )
                    )

                    send_push(
                        conn,
                        510,
                        sync_attributes(
                            selected
                        )
                    )

                    send_push(
                        conn,
                        611,
                        sync_inventory(
                            selected
                        )
                    )

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, result)
                    ])
                )

            # ------------------------------------------------
            # 523 SET MISSION STATE
            # ------------------------------------------------

            elif msg == 523:
                mid = textval(
                    body.get(0)
                )

                state = intval(
                    body.get(1),
                    1
                )

                if selected:
                    mission = selected.get(
                        "active_missions",
                        {}
                    ).get(mid)

                    if mission:
                        mission["state"] = state
                        save_characters()

                        send_push(
                            conn,
                            519,
                            mission_sync(
                                selected
                            )
                        )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 524 SET MISSION PARAM
            # ------------------------------------------------

            elif msg == 524:
                mid = textval(
                    body.get(0)
                )

                index = intval(
                    body.get(1),
                    1
                )

                value = intval(
                    body.get(2),
                    0
                )

                if selected:
                    mission = selected.get(
                        "active_missions",
                        {}
                    ).get(mid)

                    if mission:
                        parm = mission.setdefault(
                            "parm",
                            [0] * 8
                        )

                        idx = max(
                            0,
                            min(
                                len(parm) - 1,
                                index - 1
                            )
                        )

                        parm[idx] = value

                        save_characters()

                        send_push(
                            conn,
                            519,
                            mission_sync(
                                selected
                            )
                        )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 130 SKILL LEVEL UP
            # ------------------------------------------------

            elif msg == 130:
                sid = textval(
                    body.get(0)
                )

                requested_level = intval(
                    body.get(1),
                    0
                )

                result = 0

                if selected:
                    cost = (
                                   requested_level + 1
                           ) * 100

                    if (
                            selected.get("cash", 0)
                            >= cost
                    ):
                        selected["cash"] -= cost

                        selected.setdefault(
                            "skill_levels",
                            {}
                        )[sid] = (
                                requested_level + 1
                        )

                        result = 1

                        save_characters()

                        send_push(
                            conn,
                            540,
                            build_skill_sync(
                                selected
                            )
                        )

                        send_push(
                            conn,
                            510,
                            sync_attributes(
                                selected
                            )
                        )

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, result)
                    ])
                )

            # ------------------------------------------------
            # 102 SKILL USE
            # ------------------------------------------------

            elif msg == 102:
                sid = textval(
                    body.get(1)
                )

                target = intval(
                    body.get(0)
                )

                actions = body.get(3)

                if selected:
                    send_push(
                        conn,
                        508,
                        encode_sproto([
                            (0, selected["id"]),
                            (1, target),
                            (2, sid),
                            (3, actions)
                        ])
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 111 ACCEPT DAMAGE
            # ------------------------------------------------

            elif msg == 111:
                damage_list = body.get(0)

                if selected and isinstance(
                        damage_list,
                        list
                ):
                    for raw_damage in damage_list:
                        if not isinstance(
                                raw_damage,
                                bytes
                        ):
                            continue

                        damage = decode_sproto(
                            raw_damage
                        )

                        target_id = intval(
                            damage.get(0)
                        )

                        amount = max(
                            0,
                            intval(
                                damage.get(1)
                            )
                        )

                        # Player damage.
                        if target_id == intval(
                                selected["id"]
                        ):
                            selected["hp"] = max(
                                0,
                                selected.get(
                                    "hp",
                                    selected.get(
                                        "hp_max",
                                        3000
                                    )
                                ) - amount
                            )

                            send_push(
                                conn,
                                510,
                                sync_attributes(
                                    selected
                                )
                            )

                        # NPC damage.
                        elif target_id in NPC_STATE:
                            npc = NPC_STATE[
                                target_id
                            ]

                            npc["hp"] = max(
                                0,
                                npc["hp"] - amount
                            )

                            if npc["hp"] <= 0:
                                send_push(
                                    conn,
                                    507,
                                    encode_sproto([
                                        (
                                            0,
                                            target_id
                                        )
                                    ])
                                )

                save_characters()

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 307 LOCAL NPC DIE
            # ------------------------------------------------

            elif msg == 307:
                npc_id = textval(
                    body.get(0)
                )

                die_type = intval(
                    body.get(3),
                    0
                )

                if selected:
                    level = int(
                        selected.get(
                            "level",
                            1
                        )
                    )

                    exp_reward = level * 20
                    cash_reward = level * 100

                    selected["exp"] = (
                            selected.get("exp", 0)
                            + exp_reward
                    )

                    selected["cash"] = (
                            selected.get("cash", 0)
                            + cash_reward
                    )

                    mission_progress(
                        selected,
                        npc_id,
                        die_type
                    )

                    send_push(
                        conn,
                        638,
                        encode_sproto([
                            (
                                0,
                                [
                                    encode_sproto([
                                        (0, "2001"),
                                        (1, exp_reward),
                                        (2, 0)
                                    ]),
                                    encode_sproto([
                                        (0, "1001"),
                                        (1, cash_reward),
                                        (2, 0)
                                    ])
                                ]
                            )
                        ])
                    )

                    send_push(
                        conn,
                        519,
                        mission_sync(
                            selected
                        )
                    )

                    send_push(
                        conn,
                        510,
                        sync_attributes(
                            selected
                        )
                    )

                    save_characters()

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 298 IMPACT NPC
            # ------------------------------------------------

            elif msg == 298:
                npc_id = textval(
                    body.get(0)
                )

                print(
                    f"[NPC INTERACT] {npc_id}"
                )

                if selected and npc_id == "1105":
                    send_push(
                        conn,
                        529,
                        encode_sproto([
                            (0, "102098"),
                            (1, True)
                        ])
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 106 ENTER NEW MAP
            # ------------------------------------------------

            elif msg == 106:
                map_id = textval(
                    body.get(0)
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 107 ENTER COPY SCENE
            # ------------------------------------------------

            elif msg == 107:
                map_id = textval(
                    body.get(0)
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 246 SURVIVE
            # ------------------------------------------------

            elif msg == 246:
                map_id = textval(
                    body.get(0),
                    DEFAULT_MAP
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 273 SCUFFLE
            # ------------------------------------------------

            elif msg == 273:
                map_id = textval(
                    body.get(0),
                    DEFAULT_MAP
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 207 BAR FIGHT
            # ------------------------------------------------

            elif msg == 207:
                map_id = textval(
                    body.get(0),
                    DEFAULT_MAP
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 201 WILD BOSS
            # ------------------------------------------------

            elif msg == 201:
                map_id = textval(
                    body.get(0),
                    DEFAULT_MAP
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 322 GUILD CITY
            # ------------------------------------------------

            elif msg == 322:
                map_id = textval(
                    body.get(0),
                    DEFAULT_MAP
                )

                if selected:
                    transition_map(
                        conn,
                        selected,
                        map_id
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 311 DOMIN PK
            # ------------------------------------------------

            elif msg == 311:
                send_push(
                    conn,
                    552,
                    encode_sproto([
                        (0, 1)
                    ])
                )

                if selected:
                    for mid, mission in selected.get(
                            "active_missions",
                            {}
                    ).items():
                        cfg = MISSIONS.get(
                            str(mid)
                        )

                        if cfg and cfg.get(
                                "logic_type"
                        ) == 25:
                            mission["state"] = 2

                    save_characters()

                    send_push(
                        conn,
                        519,
                        mission_sync(
                            selected
                        )
                    )

                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # 7 UPDATE GAME SERVER
            # ------------------------------------------------

            elif msg == 7:
                server_entry = encode_sproto([
                    (0, 302),
                    (1, "EU-001"),
                    (2, "s16.serv00.com"),
                    (3, 15678),
                    (4, 1),
                    (5, 1),
                    (6, 1),
                    (7, 0),
                    (8, 1),
                    (9, 1)
                ])

                send_reply(
                    conn,
                    session,
                    encode_sproto([
                        (0, [server_entry])
                    ])
                )

            # ------------------------------------------------
            # COMMON ACKS
            # ------------------------------------------------

            elif msg in (
                    145,
                    225,
                    258,
                    261,
                    278,
                    296,
                    299,
                    310,
                    313,
                    319
            ):
                send_reply(
                    conn,
                    session
                )

            # ------------------------------------------------
            # UNKNOWN REQUEST
            # ------------------------------------------------

            else:
                print(
                    f"[UNHANDLED] TAG={msg}"
                )

                if session is not None:
                    send_reply(
                        conn,
                        session
                    )

    except socket.timeout:
        print(
            f"[-] TIMEOUT {addr}"
        )

    except ConnectionResetError:
        print(
            f"[-] RESET {addr}"
        )

    except Exception:
        traceback.print_exc()

    finally:
        if selected:
            try:
                selected["last_played"] = int(
                    time.time()
                )

                save_characters()

                with ONLINE_LOCK:
                    ONLINE_PLAYERS.pop(
                        str(selected.get("id")),
                        None
                    )
            except Exception:
                pass

        try:
            conn.close()
        except Exception:
            pass

        print(
            f"[-] GAME CONNECTION CLOSED {addr}"
        )


# ============================================================
# SOCKET HELPERS
# ============================================================

def recv_exact(conn, size):
    data = bytearray()

    while len(data) < size:
        chunk = conn.recv(
            size - len(data)
        )

        if not chunk:
            return None

        data.extend(chunk)

    return bytes(data)


# ============================================================
# PERIODIC SAVE
# ============================================================

def save_loop():
    while True:
        time.sleep(15)

        try:
            save_characters()
        except Exception:
            traceback.print_exc()


# ============================================================
# SERVER
# ============================================================

def start_server():
    print("========================================")
    print(" ATG GAME SERVER")
    print(" PORT:", PORT)
    print(" VERSION:", GAME_VERSION)
    print(" DATA:", DATA_VERSION)
    print(" MAP:", DEFAULT_MAP)
    print("========================================")

    threading.Thread(
        target=save_loop,
        daemon=True
    ).start()

    server = socket.socket(
        socket.AF_INET,
        socket.SOCK_STREAM
    )

    server.setsockopt(
        socket.SOL_SOCKET,
        socket.SO_REUSEADDR,
        1
    )

    server.bind(
        (HOST, PORT)
    )

    server.listen(50)

    print(
        f"[READY] GAME SERVER 9555 LISTENING"
    )

    while True:
        conn, addr = server.accept()

        threading.Thread(
            target=client_handler,
            args=(conn, addr),
            daemon=True
        ).start()


if __name__ == "__main__":
    start_server()
