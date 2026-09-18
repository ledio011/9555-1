import os
import json
import time
import random
import socket
import struct
import threading
import traceback

PORT = int(os.environ.get("PORT", 15678))
DB_PATH = "db"
DB_FILE = os.path.join(DB_PATH, "characters.json")

GAME_VERSION = "1.012.017"
DATA_VERSION = "205"

DEFAULT_MAP = "11"
DEFAULT_POS = [29860, 100, -17005, 0]

os.makedirs(DB_PATH, exist_ok=True)

PLAYER_DB = {}
ONLINE_PLAYERS = {}

MAP_CONFIG = {
    "11": {
        "scene": "DSJ_GTA",
        "map_id": "11",
        "line": 0,
        "line_count": 1,
    },
    "101": {
        "scene": "DSJ_zhuCheng",
        "map_id": "101",
        "line": 0,
        "line_count": 1,
    },
    "109": {
        "scene": "PVP_GothamCity",
        "map_id": "109",
        "line": 0,
        "line_count": 1,
    },
}


# ============================================================
# DATABASE
# ============================================================

def load_db():
    global PLAYER_DB

    if not os.path.exists(DB_FILE):
        PLAYER_DB = {}
        return

    try:
        with open(DB_FILE, "r", encoding="utf-8") as f:
            PLAYER_DB = json.load(f)
    except Exception:
        PLAYER_DB = {}


def save_db():
    try:
        tmp = DB_FILE + ".tmp"
        with open(tmp, "w", encoding="utf-8") as f:
            json.dump(PLAYER_DB, f, indent=2, ensure_ascii=False)

        os.replace(tmp, DB_FILE)
    except Exception:
        traceback.print_exc()


load_db()


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
                    body += struct.pack("<I", 4)
                    body += struct.pack("<i", value)
                else:
                    body += struct.pack("<I", 8)
                    body += struct.pack("<q", value)

        elif isinstance(value, str):
            payload = value.encode("utf-8")
            header.append(0)
            body += struct.pack("<I", len(payload))
            body += payload

        elif isinstance(value, (bytes, bytearray)):
            payload = bytes(value)
            header.append(0)
            body += struct.pack("<I", len(payload))
            body += payload

        elif isinstance(value, list):
            payload = bytearray()

            for item in value:
                if isinstance(item, (bytes, bytearray)):
                    payload += struct.pack("<I", len(item))
                    payload += item
                elif isinstance(item, str):
                    x = item.encode("utf-8")
                    payload += struct.pack("<I", len(x))
                    payload += x
                else:
                    x = encode_sproto(item) if isinstance(item, list) else str(item).encode()
                    payload += struct.pack("<I", len(x))
                    payload += x

            header.append(0)
            body += struct.pack("<I", len(payload))
            body += payload

        elif isinstance(value, dict):
            payload = bytearray()

            for k, v in value.items():
                entry = encode_sproto([
                    (0, str(k)),
                    (1, v if isinstance(v, int) else 1)
                ])

                payload += struct.pack("<I", len(entry))
                payload += entry

            header.append(0)
            body += struct.pack("<I", len(payload))
            body += payload

        else:
            payload = str(value).encode("utf-8")
            header.append(0)
            body += struct.pack("<I", len(payload))
            body += payload

        last_tag = tag

    result = bytearray()
    result += struct.pack("<H", len(header))

    for h in header:
        result += struct.pack("<H", h)

    result += body

    return bytes(result)


def decode_sproto(data, offset=0):
    if offset + 2 > len(data):
        return {}

    fn = struct.unpack_from("<H", data, offset)[0]

    header_start = offset + 2
    body_start = header_start + fn * 2

    if body_start > len(data):
        return {}

    fields = {}
    tag = -1
    body_pos = body_start

    for i in range(fn):

        p = header_start + i * 2

        if p + 2 > len(data):
            break

        value = struct.unpack_from("<H", data, p)[0]

        if value == 0:

            tag += 1

            if body_pos + 4 > len(data):
                fields[tag] = b""
                continue

            length = struct.unpack_from("<I", data, body_pos)[0]
            body_pos += 4

            if body_pos + length > len(data):
                length = max(0, len(data) - body_pos)

            fields[tag] = data[body_pos:body_pos + length]
            body_pos += length

        elif value == 1:
            tag += 1

        elif value & 1:
            tag += (value >> 1) + 1

        else:
            tag += 1
            fields[tag] = (value >> 1) - 1

    return fields


def get_int(fields, tag, default=0):
    value = fields.get(tag)

    if value is None:
        return default

    if isinstance(value, int):
        return value

    if isinstance(value, bytes):

        if len(value) == 1:
            return value[0]

        if len(value) == 4:
            try:
                return struct.unpack("<i", value)[0]
            except Exception:
                pass

        if len(value) == 8:
            try:
                return struct.unpack("<q", value)[0]
            except Exception:
                pass

    return default


def get_string(fields, tag, default=""):
    value = fields.get(tag)

    if value is None:
        return default

    if isinstance(value, str):
        return value

    if isinstance(value, bytes):
        try:
            return value.decode("utf-8")
        except Exception:
            return default

    return str(value)


def sproto_pack(data):
    result = bytearray()

    for pos in range(0, len(data), 8):

        chunk = data[pos:pos + 8]

        if len(chunk) < 8:
            chunk += b"\x00" * (8 - len(chunk))

        mask = 0

        for i in range(8):
            if chunk[i] != 0:
                mask |= 1 << i

        if mask == 0xFF:
            result.append(0xFF)
            result.append(0)
            result += chunk
        else:
            result.append(mask)

            for i in range(8):
                if mask & (1 << i):
                    result.append(chunk[i])

    return bytes(result)


def sproto_unpack(data):
    result = bytearray()
    pos = 0

    while pos < len(data):

        mask = data[pos]
        pos += 1

        if mask == 0xFF:

            if pos >= len(data):
                break

            count = (data[pos] + 1) * 8
            pos += 1

            result += data[pos:pos + count]
            pos += count

        else:

            for bit in range(8):

                if mask & (1 << bit):

                    if pos < len(data):
                        result.append(data[pos])
                        pos += 1

                else:
                    result.append(0)

    return bytes(result)


# ============================================================
# NETWORK
# ============================================================

def send_frame(conn, payload):
    packed = sproto_pack(payload)

    if len(packed) > 65535:
        return

    conn.sendall(struct.pack(">H", len(packed)) + packed)


def send_response(conn, session, body):
    packet = encode_sproto([
        (1, session)
    ]) + body

    send_frame(conn, packet)


def send_push(conn, tag, body):
    packet = encode_sproto([
        (0, tag)
    ]) + body

    send_frame(conn, packet)

    print(
        "[TX] PUSH TAG=%s SIZE=%s"
        % (tag, len(body))
    )


# ============================================================
# PLAYER
# ============================================================

def new_character(char_id, name, profession):
    return {
        "id": int(char_id),
        "name": name,
        "prof": int(profession),

        "level": 1,
        "exp": 0,

        "cash": 1000,
        "gold": 0,
        "diamond": 0,

        "hp": 3000,
        "hp_max": 3000,

        "atk": 150,
        "def": 50,
        "hit": 100,
        "eva": 50,
        "cri": 20,
        "res": 10,

        "map_id": DEFAULT_MAP,
        "pos": DEFAULT_POS[:],

        "active_missions": {},
        "completed_missions": [],

        "last_main_mission_id": "-1",

        "is_finish_download": 2,
        "tutorial_finished": 0,

        "inventory": {},
        "skills": {},
        "fashion": {},

        "cars": [],
        "friends": [],
        "mail": [],
        "team_id": 0,
        "guild_id": 0,

        "pk_mode": 0,
    }


def get_player(account_id, char_id=None):

    candidates = []

    for key, char in PLAYER_DB.items():

        if char.get("account_id") == str(account_id):
            candidates.append(char)

        elif char_id is not None and str(char.get("id")) == str(char_id):
            candidates.append(char)

    if not candidates:
        return None

    return candidates[0]


def character_list(account_id):

    result = []

    for char in PLAYER_DB.values():

        if char.get("account_id") != str(account_id):
            continue

        result.append(
            encode_sproto([
                (0, char["id"]),
                (1, char["name"]),
                (2, char.get("prof", 0)),
                (3, str(char.get("map_id", DEFAULT_MAP))),
                (4, char.get("level", 1)),
            ])
        )

    return result


def full_character(char):

    return encode_sproto([
        (0, char["id"]),
        (1, char["name"]),
        (2, char.get("prof", 0)),
        (3, char.get("level", 1)),
        (4, char.get("exp", 0)),
        (5, char.get("cash", 0)),
        (6, char.get("gold", 0)),
        (7, char.get("diamond", 0)),
        (8, char.get("hp", 3000)),
        (9, char.get("hp_max", 3000)),
        (10, char.get("atk", 150)),
        (11, char.get("def", 50)),
        (12, char.get("hit", 100)),
        (13, char.get("eva", 50)),
        (14, char.get("cri", 20)),
        (15, char.get("res", 10)),
        (16, str(char.get("map_id", DEFAULT_MAP))),
        (17, 2),
    ])


def movement_data(char):

    pos = char.get("pos", DEFAULT_POS)

    while len(pos) < 4:
        pos.append(0)

    return encode_sproto([
        (0, int(pos[0])),
        (1, int(pos[1])),
        (2, int(pos[2])),
        (3, int(pos[3])),
    ])


# ============================================================
# MISSIONS
# ============================================================

def mission_packet(mid, mission):

    return encode_sproto([
        (0, str(mid)),
        (1, mission.get("state", 1)),
        (2, mission.get("quality", 0)),
        (3, mission.get("parm", [0] * 8)),
    ])


def mission_sync(char):

    missions = []

    for mid, mission in char.get(
        "active_missions", {}
    ).items():

        missions.append(
            mission_packet(mid, mission)
        )

    return encode_sproto([
        (0, missions),
        (1, str(
            char.get(
                "last_main_mission_id",
                "-1"
            )
        )),
    ])


def ensure_first_mission(char):

    if char.get("active_missions"):
        return

    char["active_missions"]["1001"] = {
        "state": 1,
        "quality": 0,
        "parm": [0] * 8,
    }


# ============================================================
# MAP
# ============================================================

def map_info(map_id):

    map_id = str(map_id)

    return MAP_CONFIG.get(
        map_id,
        {
            "scene": "DSJ_GTA",
            "map_id": map_id,
            "line": 0,
            "line_count": 1,
        }
    )


def send_enter_map(conn, char):

    mid = str(char.get("map_id", DEFAULT_MAP))
    cfg = map_info(mid)

    print(
        "[MAP] ENTER map=%s scene=%s"
        % (mid, cfg["scene"])
    )

    send_push(
        conn,
        503,
        encode_sproto([
            (0, mid),
            (1, cfg["line"]),
            (2, cfg["line_count"]),
        ])
    )


def send_main_player(conn, char):

    send_push(
        conn,
        504,
        encode_sproto([
            (0, full_character(char)),
            (1, movement_data(char)),
        ])
    )


def send_aoi_player(conn, char):

    send_push(
        conn,
        505,
        encode_sproto([
            (0, full_character(char)),
            (1, movement_data(char)),
        ])
    )


def send_time_sync(conn):

    now = int(time.time())

    functions = {}

    for fid in [
        "4026",
        "4061",
        "4064",
        "4081",
        "4084",
    ]:
        functions[fid] = encode_sproto([
            (0, int(fid)),
            (1, 1),
        ])

    send_push(
        conn,
        614,
        encode_sproto([
            (0, now),
            (2, 0),
            (9, functions),
            (13, 1),
            (14, now),
        ])
    )


def send_inventory(conn, char):

    inventory = char.get("inventory", {})

    send_push(
        conn,
        611,
        encode_sproto([
            (0, inventory),
        ])
    )

    send_push(
        conn,
        592,
        encode_sproto([
            (0, {}),
        ])
    )

    send_push(
        conn,
        616,
        encode_sproto([
            (0, char.get("fashion", {})),
        ])
    )


def send_skills(conn, char):

    skills = char.get("skills", {})

    send_push(
        conn,
        540,
        encode_sproto([
            (0, skills),
            (1, False),
        ])
    )


def send_attributes(conn, char):

    send_push(
        conn,
        510,
        encode_sproto([
            (0, char.get("hp_max", 3000)),
            (1, char.get("atk", 150)),
            (2, char.get("def", 50)),
            (3, char.get("hit", 100)),
            (4, char.get("eva", 50)),
            (5, char.get("cri", 20)),
            (6, char.get("res", 10)),
        ])
    )


def send_initial_world(conn, char):

    # common data
    send_time_sync(conn)

    # inventory
    send_inventory(conn, char)

    # skills
    send_skills(conn, char)

    # attributes
    send_attributes(conn, char)

    # map FIRST
    send_enter_map(conn, char)

    # player AFTER map
    send_main_player(conn, char)

    # AOI representation
    send_aoi_player(conn, char)


def finish_map_loading(conn, char):

    print(
        "[MAP READY] map=%s"
        % char.get("map_id", DEFAULT_MAP)
    )

    # Client has now initialized RunningMapIdStr.
    # Mission synchronization belongs after map initialization.
    send_push(
        conn,
        519,
        mission_sync(char)
    )

    # Start enter-game only after player/map are ready.
    send_push(
        conn,
        654,
        encode_sproto([
            (0, 1),
        ])
    )

    print("[WORLD] ENTER GAME COMPLETE")


# ============================================================
# NPC / AOI
# ============================================================

def send_npc(conn, npc_id, x, y, z):

    npc_char = encode_sproto([
        (0, npc_id),
        (1, 0),
        (2, 1),
        (3, 1000),
    ])

    movement = encode_sproto([
        (0, x),
        (1, y),
        (2, z),
        (3, 0),
    ])

    send_push(
        conn,
        505,
        encode_sproto([
            (0, npc_char),
            (1, movement),
        ])
    )


def spawn_initial_npcs(conn, char):

    mid = str(char.get("map_id", DEFAULT_MAP))

    if mid != "11":
        return

    # Initial safe world NPC set.
    npc_positions = [
        (9901, 29800, 100, -17000),
        (9902, 30000, 100, -17100),
        (9903, 30200, 100, -17200),
    ]

    for npc_id, x, y, z in npc_positions:
        send_npc(
            conn,
            npc_id,
            x,
            y,
            z
        )


# ============================================================
# GAME ACTIONS
# ============================================================

def handle_move(char, body):

    raw = body.get(0)

    if not raw:
        return

    movement = decode_sproto(raw)

    x = get_int(movement, 0, char["pos"][0])
    y = get_int(movement, 1, char["pos"][1])
    z = get_int(movement, 2, char["pos"][2])
    r = get_int(movement, 3, char["pos"][3])

    char["pos"] = [
        x,
        y,
        z,
        r,
    ]


def handle_skill(char, body):

    if char is None:
        return

    skill_id = get_int(body, 0, 0)

    if skill_id:
        char["last_skill"] = skill_id


def handle_attack(char, body):

    if char is None:
        return

    target = get_int(body, 0, 0)

    if target:
        char["last_target"] = target


def handle_change_map(conn, char, body):

    if char is None:
        return

    target = get_string(
        body,
        0,
        DEFAULT_MAP
    )

    if target not in MAP_CONFIG:
        target = DEFAULT_MAP

    char["map_id"] = target
    char["pos"] = DEFAULT_POS[:]

    save_db()

    send_enter_map(
        conn,
        char
    )

    send_main_player(
        conn,
        char
    )


# ============================================================
# CLIENT HANDLER
# ============================================================

def client_handler(conn, addr):

    print(
        "[+] GAME CONNECTION %s"
        % (addr,)
    )

    account_id = "0"
    picked_char = None

    try:

        while True:

            header = conn.recv(2)

            if not header:
                break

            while len(header) < 2:
                part = conn.recv(2 - len(header))

                if not part:
                    return

                header += part

            size = struct.unpack(
                ">H",
                header
            )[0]

            packet = bytearray()

            while len(packet) < size:

                chunk = conn.recv(
                    size - len(packet)
                )

                if not chunk:
                    return

                packet += chunk

            raw = sproto_unpack(
                bytes(packet)
            )

            if len(raw) < 2:
                continue

            package = decode_sproto(
                raw,
                0
            )

            msg = get_int(
                package,
                0,
                -1
            )

            session = package.get(1)

            print(
                "[RX] TAG=%s SESSION=%s"
                % (msg, session)
            )

            header_len = struct.unpack(
                "<H",
                raw[:2]
            )[0]

            body_offset = 2 + header_len * 2

            body = decode_sproto(
                raw,
                body_offset
            )

            response = encode_sproto([])

            # ------------------------------------------------
            # LOGIN
            # ------------------------------------------------

            if msg == 4:

                account_id = get_string(
                    body,
                    1,
                    "0"
                )

                print(
                    "[LOGIN] account=%s"
                    % account_id
                )

                response = encode_sproto([
                    (0, 2),
                    (1, GAME_VERSION),
                    (2, DATA_VERSION),
                    (3, 1),
                ])

            # ------------------------------------------------
            # CHARACTER LIST
            # ------------------------------------------------

            elif msg == 103:

                chars = character_list(
                    account_id
                )

                response = encode_sproto([
                    (0, chars),
                ])

            # ------------------------------------------------
            # CHARACTER CREATE
            # ------------------------------------------------

            elif msg == 104:

                char_data = body.get(0)

                if char_data:
                    create_data = decode_sproto(
                        char_data
                    )
                else:
                    create_data = {}

                name = get_string(
                    create_data,
                    0,
                    "Player"
                )

                profession = get_int(
                    create_data,
                    1,
                    0
                )

                char_id = int(
                    time.time() * 1000
                )

                while str(char_id) in PLAYER_DB:
                    char_id += 1

                char = new_character(
                    char_id,
                    name,
                    profession
                )

                char["account_id"] = str(
                    account_id
                )

                ensure_first_mission(
                    char
                )

                PLAYER_DB[str(char_id)] = char

                save_db()

                print(
                    "[CREATE] char=%s name=%s"
                    % (
                        char_id,
                        name
                    )
                )

                response = encode_sproto([
                    (0, None),
                    (1, 0),
                ])

            # ------------------------------------------------
            # CHARACTER PICK
            # ------------------------------------------------

            elif msg == 105:

                char_id = get_int(
                    body,
                    0,
                    0
                )

                picked_char = PLAYER_DB.get(
                    str(char_id)
                )

                if picked_char is None:

                    response = encode_sproto([
                        (0, 0),
                    ])

                else:

                    picked_char["account_id"] = str(
                        picked_char.get(
                            "account_id",
                            account_id
                        )
                    )

                    ensure_first_mission(
                        picked_char
                    )

                    ONLINE_PLAYERS[
                        str(char_id)
                    ] = conn

                    print(
                        "[PICK] char=%s map=%s"
                        % (
                            char_id,
                            picked_char.get(
                                "map_id",
                                DEFAULT_MAP
                            )
                        )
                    )

                    response = encode_sproto([
                        (0, 1),
                    ])

                    # Do NOT send mission sync before map.
                    send_initial_world(
                        conn,
                        picked_char
                    )

                    spawn_initial_npcs(
                        conn,
                        picked_char
                    )

            # ------------------------------------------------
            # MAP READY
            # ------------------------------------------------

            elif msg == 100:

                if picked_char:

                    finish_map_loading(
                        conn,
                        picked_char
                    )

                    save_db()

            # ------------------------------------------------
            # MOVE
            # ------------------------------------------------

            elif msg == 101:

                handle_move(
                    picked_char,
                    body
                )

                save_db()

                response = encode_sproto([
                    (0, body.get(0, b"")),
                ])

            # ------------------------------------------------
            # CHARACTER CREATE / RELATED
            # ------------------------------------------------

            elif msg == 106:

                if picked_char:

                    handle_change_map(
                        conn,
                        picked_char,
                        body
                    )

            # ------------------------------------------------
            # ACCEPT MISSION
            # ------------------------------------------------

            elif msg == 112:

                if picked_char:

                    mid = get_string(
                        body,
                        0,
                        ""
                    )

                    if mid:

                        picked_char.setdefault(
                            "active_missions",
                            {}
                        )

                        if mid not in picked_char[
                            "active_missions"
                        ]:

                            picked_char[
                                "active_missions"
                            ][mid] = {
                                "state": 1,
                                "quality": 0,
                                "parm": [0] * 8,
                            }

                            send_push(
                                conn,
                                519,
                                mission_sync(
                                    picked_char
                                )
                            )

                            save_db()

            # ------------------------------------------------
            # COMPLETE MISSION
            # ------------------------------------------------

            elif msg == 113:

                if picked_char:

                    mid = get_string(
                        body,
                        0,
                        ""
                    )

                    mission = picked_char.get(
                        "active_missions",
                        {}
                    ).get(mid)

                    if mission:

                        mission["state"] = 3

                        picked_char.setdefault(
                            "completed_missions",
                            []
                        ).append(
                            int(mid)
                            if mid.isdigit()
                            else mid
                        )

                        del picked_char[
                            "active_missions"
                        ][mid]

                        send_push(
                            conn,
                            519,
                            mission_sync(
                                picked_char
                            )
                        )

                        save_db()

            # ------------------------------------------------
            # SKILL USE
            # ------------------------------------------------

            elif msg == 102:

                handle_skill(
                    picked_char,
                    body
                )

            # ------------------------------------------------
            # HEARTBEAT
            # ------------------------------------------------

            elif msg == 218:

                t1 = get_int(
                    body,
                    0,
                    0
                )

                response = encode_sproto([
                    (0, t1),
                    (1, int(time.time())),
                ])

            # ------------------------------------------------
            # ENTER MAP
            # ------------------------------------------------

            elif msg == 503:

                if picked_char:

                    send_enter_map(
                        conn,
                        picked_char
                    )

            # ------------------------------------------------
            # START ENTER GAME
            # ------------------------------------------------

            elif msg == 654:

                if picked_char:

                    send_push(
                        conn,
                        654,
                        encode_sproto([
                            (0, 1),
                        ])
                    )

            # ------------------------------------------------
            # DOWNLOAD FINISH
            # ------------------------------------------------

            elif msg == 270:

                if picked_char:

                    picked_char[
                        "is_finish_download"
                    ] = 2

                    save_db()

                    send_push(
                        conn,
                        654,
                        encode_sproto([
                            (0, 1),
                        ])
                    )

            # ------------------------------------------------
            # NPC / COMBAT
            # ------------------------------------------------

            elif msg in (
                200,
                201,
                202,
                203,
                204,
                205,
                210,
                225,
                235,
                242,
                252,
                257,
                258,
                261,
                278,
                296,
                299,
                310,
                313,
                319,
            ):

                handle_attack(
                    picked_char,
                    body
                )

            # ------------------------------------------------
            # GENERIC RESPONSE
            # ------------------------------------------------

            if session is not None:

                send_response(
                    conn,
                    get_int(
                        {1: session},
                        1,
                        0
                    ),
                    response
                )

    except Exception:

        traceback.print_exc()

    finally:

        if picked_char:

            save_db()

            cid = str(
                picked_char.get(
                    "id",
                    ""
                )
            )

            ONLINE_PLAYERS.pop(
                cid,
                None
            )

        try:
            conn.close()
        except Exception:
            pass

        print(
            "[-] GAME CONNECTION CLOSED %s"
            % (addr,)
        )


# ============================================================
# SERVER
# ============================================================

def start_server():

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
        ("0.0.0.0", PORT)
    )

    server.listen(50)

    print("========================================")
    print(" ATG GAME SERVER")
    print(" PORT:", PORT)
    print(" VERSION:", GAME_VERSION)
    print(" DATA:", DATA_VERSION)
    print(" MAP:", DEFAULT_MAP)
    print("========================================")
    print("[READY] GAME SERVER 9555 LISTENING")

    while True:

        conn, addr = server.accept()

        threading.Thread(
            target=client_handler,
            args=(conn, addr),
            daemon=True
        ).start()


if __name__ == "__main__":
    start_server()
