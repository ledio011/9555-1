import json
import os
import socket
import struct
import threading
import time
import traceback

HOST = "0.0.0.0"
PORT = int(os.environ.get("PORT", "15678"))
DB_PATH = os.environ.get("DB_PATH", "db")
DATA_DIR = os.environ.get("DATA_DIR", "assets/Bundle/TextAsset")

os.makedirs(DB_PATH, exist_ok=True)

ONLINE_PLAYERS = {}
GAME_DATA = {}

GAME_VERSION = "1.012.017"
DATA_VERSION = "205"


def log(text):
    line = f"[{time.strftime('%H:%M:%S')}] {text}"
    print(line, flush=True)
    try:
        with open("game.log", "a", encoding="utf-8") as f:
            f.write(line + "\n")
    except Exception:
        pass


def recv_exact(conn, size):
    data = bytearray()
    while len(data) < size:
        chunk = conn.recv(size - len(data))
        if not chunk:
            return None
        data.extend(chunk)
    return bytes(data)


def get_val_int(fields, tag, default=0):
    value = fields.get(tag)
    if value is None:
        return default
    if isinstance(value, int):
        return value
    if isinstance(value, (bytes, bytearray)):
        if len(value) == 1:
            return value[0]
        if len(value) == 4:
            return struct.unpack("<i", value)[0]
        if len(value) == 8:
            return struct.unpack("<q", value)[0]
    return default


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
            header.append(2 if value else 0)
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
            body += struct.pack("<I", len(payload)) + payload
        elif isinstance(value, (bytes, bytearray)):
            payload = bytes(value)
            header.append(0)
            body += struct.pack("<I", len(payload)) + payload
        elif isinstance(value, list):
            payload = b"".join(value)
            header.append(0)
            body += struct.pack("<I", len(payload)) + payload
        else:
            payload = str(value).encode("utf-8")
            header.append(0)
            body += struct.pack("<I", len(payload)) + payload

        last_tag = tag

    out = bytearray(struct.pack("<H", len(header)))
    for h in header:
        out += struct.pack("<H", h)
    out += body
    return bytes(out)


def decode_sproto(data, offset=0):
    if len(data) < offset + 2:
        return {}

    field_count = struct.unpack_from("<H", data, offset)[0]
    header_start = offset + 2
    body_pos = header_start + field_count * 2
    fields = {}
    current_tag = -1

    if body_pos > len(data):
        return {}

    for i in range(field_count):
        value = struct.unpack_from("<H", data, header_start + i * 2)[0]

        if value == 0:
            current_tag += 1
            if body_pos + 4 > len(data):
                break
            length = struct.unpack_from("<I", data, body_pos)[0]
            body_pos += 4
            fields[current_tag] = data[body_pos:body_pos + length]
            body_pos += length
        elif value == 1:
            current_tag += 1
        elif value & 1:
            current_tag += (value >> 1) + 1
        else:
            current_tag += 1
            fields[current_tag] = (value >> 1) - 1

    return fields


def sproto_pack(data):
    out = bytearray()
    for pos in range(0, len(data), 8):
        chunk = data[pos:pos + 8]
        if len(chunk) < 8:
            chunk += b"\x00" * (8 - len(chunk))

        mask = 0
        for bit, value in enumerate(chunk):
            if value != 0:
                mask |= 1 << bit

        if mask == 0xFF:
            out.append(0xFF)
            out.append(0)
            out.extend(chunk)
        else:
            out.append(mask)
            for bit in range(8):
                if mask & (1 << bit):
                    out.append(chunk[bit])

    return bytes(out)


def sproto_unpack(data):
    out = bytearray()
    pos = 0

    while pos < len(data):
        mask = data[pos]
        pos += 1

        if mask == 0xFF:
            if pos >= len(data):
                break
            count = (data[pos] + 1) * 8
            pos += 1
            out.extend(data[pos:pos + count])
            pos += count
            continue

        for bit in range(8):
            if mask & (1 << bit):
                if pos >= len(data):
                    break
                out.append(data[pos])
                pos += 1
            else:
                out.append(0)

    return bytes(out)


def load_textassets():
    if not os.path.isdir(DATA_DIR):
        log(f"[DATA] directory missing: {DATA_DIR}")
        return

    names = [
        "NpcData", "ItemData", "ShopData", "SlotData", "TeamData",
        "BadgeData", "EquipData", "MountData", "SkillData", "StoryData",
        "BaseLvData", "ConfigData", "MapInfoData", "MissionData",
        "MonsterData", "CopySceneData", "MapConnectInfoData",
        "KillTargetMissionData", "TargetCarMissionData", "MoveTargetMissionData",
        "SkillupgradeData"
    ]

    for name in names:
        path = os.path.join(DATA_DIR, name)
        if not os.path.isfile(path):
            continue

        try:
            with open(path, "r", encoding="utf-8") as f:
                lines = [x.strip() for x in f]

            headers = None
            rows = {}

            for line in lines:
                if not line:
                    continue

                if headers is None and line.startswith("*,"):
                    raw_headers = line.split(",")
                    headers = raw_headers[1:]
                    continue

                if headers is None or "," not in line:
                    continue

                parts = line.split(",")
                if len(parts) < 2:
                    continue

                key = parts[1].strip()
                row = {}
                for i, header in enumerate(headers):
                    index = i + 1
                    if index >= len(parts):
                        continue
                    value = parts[index].strip()
                    try:
                        row[header] = float(value) if "." in value else int(value)
                    except ValueError:
                        row[header] = value

                if key:
                    rows[key] = row

            GAME_DATA[name] = rows
            log(f"[DATA] {name}: {len(rows)} rows")
        except Exception:
            log(f"[DATA ERROR] {name}")
            traceback.print_exc()


load_textassets()


def default_character(cid, name="Player", prof=0):
    return {
        "id": cid,
        "name": name,
        "prof": prof,
        "level": 1,
        "exp": 0,
        "cash": 1000,
        "gold": 100,
        "map_id": "11",
        "pos": [29860, 100, -17005, 0],
        "active_missions": {
            "1001": {"state": 1, "parm": [0] * 8}
        },
        "last_main_mission_id": "-1",
        "completed_missions": []
    }


def load_character(cid):
    path = os.path.join(DB_PATH, f"{cid}.json")
    if not os.path.isfile(path):
        return None
    try:
        with open(path, "r", encoding="utf-8") as f:
            return json.load(f)
    except Exception:
        return None


def save_character(char):
    path = os.path.join(DB_PATH, f"{char['id']}.json")
    with open(path, "w", encoding="utf-8") as f:
        json.dump(char, f, indent=2)


def calculate_power(char):
    return int(
        char.get("atk", 100) * 16
        + char.get("hp_max", 1000)
        + char.get("def", 50) * 11
    )


def get_full_char(char):
    pos = char.get("pos", [29860, 100, -17005, 0])
    while len(pos) < 4:
        pos.append(0)

    general = encode_sproto([
        (0, char.get("name", "Player")),
        (1, char.get("prof", 0)),
        (3, str(char.get("map_id", "11")))
    ])

    attributes = encode_sproto([
        (0, char.get("hp", 3000)),
        (1, char.get("exp", 0)),
        (2, char.get("level", 1)),
        (3, calculate_power(char)),
        (4, 0)
    ])

    properties = encode_sproto([
        (13, char.get("cash", 1000)),
        (14, char.get("gold", 100))
    ])

    movement_inner = encode_sproto([
        (0, int(pos[0])),
        (1, int(pos[1])),
        (2, int(pos[2])),
        (3, int(pos[3]))
    ])

    movement = encode_sproto([(0, movement_inner)])

    return encode_sproto([
        (0, char["id"]),
        (1, general),
        (2, attributes),
        (5, properties),
        (7, movement),
        (15, 2)
    ])


def sync_missions(char):
    entries = []
    for mission_id, mission in char.get("active_missions", {}).items():
        entries.append(encode_sproto([
            (0, str(mission_id)),
            (1, int(mission.get("state", 1))),
            (2, 0),
            (3, mission.get("parm", [0] * 8))
        ]))

    return encode_sproto([
        (0, entries),
        (1, str(char.get("last_main_mission_id", "-1")))
    ])


def send_packet(conn, raw):
    packed = sproto_pack(raw)
    conn.sendall(struct.pack(">H", len(packed)) + packed)


def send_response(conn, session, body):
    fields = []
    if session is not None:
        fields.append((1, session))
    fields.append((0, body)) if False else None
    send_packet(conn, encode_sproto(fields) + body)


def send_rpc_push(conn, tag, body):
    send_packet(conn, encode_sproto([(0, tag)]) + body)
    log(f"[TX PUSH] tag={tag}")


def send_rpc_response(conn, session, body):
    if session is None:
        return
    send_packet(conn, encode_sproto([(1, session)]) + body)
    log(f"[TX RESPONSE] session={session}")


def character_list_response():
    result = []
    for filename in sorted(os.listdir(DB_PATH)):
        if not filename.endswith(".json"):
            continue
        char = load_character(filename[:-5])
        if not char:
            continue

        general = encode_sproto([
            (0, char.get("name", "Player")),
            (1, char.get("prof", 0)),
            (3, str(char.get("map_id", "11")))
        ])
        attribute = encode_sproto([
            (0, char.get("level", 1)),
            (1, calculate_power(char))
        ])
        entry = encode_sproto([
            (0, char["id"]),
            (1, general),
            (2, attribute),
            (4, int(time.time()))
        ])
        result.append(struct.pack("<I", len(entry)) + entry)

    return encode_sproto([(0, result)])


def handle_message(conn, msg, session, body):
    log(f"[RX RPC] tag={msg} session={session}")

    if msg == 4:
        return encode_sproto([
            (0, 2),
            (1, GAME_VERSION),
            (2, DATA_VERSION),
            (3, 1)
        ])

    if msg == 103:
        response = character_list_response()
        log(f"[CHARACTER LIST RESPONSE] body_len={len(response)} body={response.hex()}")
        return response

    if msg == 104:
        raw_character = body.get(0, b"")
        character = decode_sproto(raw_character)
        name = character.get(0, b"Player")
        if isinstance(name, bytes):
            name = name.decode("utf-8", "ignore") or "Player"
        prof = get_val_int(character, 1, 0)
        cid = int(time.time() * 1000)
        char = default_character(cid, name, prof)
        save_character(char)
        log(f"[CHARACTER CREATE] id={cid} name={name} prof={prof}")
        return encode_sproto([(0, None), (1, 0)])

    if msg == 105:
        cid = get_val_int(body, 0)
        char = load_character(cid)
        if not char:
            log(f"[CHARACTER PICK] missing id={cid}")
            return encode_sproto([(0, 0)])

        ONLINE_PLAYERS[str(cid)] = {"data": char, "conn": conn}
        log(f"[CHARACTER PICK] id={cid} map={char.get('map_id', '11')}")

        threading.Thread(
            target=send_initial_game_flow,
            args=(conn, char),
            daemon=True
        ).start()

        return encode_sproto([(0, 1)])

    if msg == 100:
        if ONLINE_PLAYERS:
            send_rpc_push(conn, 654, encode_sproto([(0, 1)]))
            for entry in ONLINE_PLAYERS.values():
                if entry["conn"] is conn:
                    send_rpc_push(conn, 519, sync_missions(entry["data"]))
                    break
        return encode_sproto([])

    if msg == 101:
        if ONLINE_PLAYERS:
            for entry in ONLINE_PLAYERS.values():
                if entry["conn"] is conn:
                    raw_pos = body.get(0)
                    if raw_pos:
                        pos = decode_sproto(raw_pos)
                        entry["data"]["pos"] = [
                            get_val_int(pos, 0),
                            get_val_int(pos, 1),
                            get_val_int(pos, 2),
                            get_val_int(pos, 3)
                        ]
                        save_character(entry["data"])
                    return encode_sproto([(0, raw_pos)])
        return encode_sproto([])

    if msg == 112:
        mission_id = body.get(0, b"")
        if isinstance(mission_id, bytes):
            mission_id = mission_id.decode("utf-8", "ignore")
        for entry in ONLINE_PLAYERS.values():
            if entry["conn"] is conn:
                char = entry["data"]
                missions = GAME_DATA.get("MissionData", {})
                if str(mission_id) in missions:
                    char.setdefault("active_missions", {}).setdefault(
                        str(mission_id), {"state": 1, "parm": [0] * 8}
                    )
                save_character(char)
                send_rpc_push(conn, 519, sync_missions(char))
                break
        return encode_sproto([])

    if msg == 218:
        return encode_sproto([
            (0, body.get(0, 0)),
            (1, int(time.time()))
        ])

    return encode_sproto([])


def send_initial_game_flow(conn, char):
    try:
        now = int(time.time())

        send_rpc_push(conn, 614, encode_sproto([
            (0, now),
            (13, 1),
            (14, now)
        ]))

        send_rpc_push(conn, 611, encode_sproto([(0, {})]))
        send_rpc_push(conn, 540, encode_sproto([(0, {}), (1, False)]))

        map_id = str(char.get("map_id", "11"))
        send_rpc_push(conn, 503, encode_sproto([
            (0, map_id),
            (1, 0),
            (2, 1)
        ]))

        send_rpc_push(conn, 504, encode_sproto([
            (0, get_full_char(char))
        ]))

        log(f"[MAP FLOW] 503 -> 504 sent for map={map_id}")

    except Exception:
        log("[MAP FLOW ERROR]")
        traceback.print_exc()


def client_handler(conn, addr):
    log(f"[CONNECT] {addr}")
    conn.settimeout(120)

    try:
        while True:
            header = recv_exact(conn, 2)
            if header is None:
                break

            size = struct.unpack(">H", header)[0]
            if size == 0:
                continue

            packed = recv_exact(conn, size)
            if packed is None:
                break

            raw = sproto_unpack(packed)
            if len(raw) < 2:
                log("[RX] invalid empty sproto")
                continue

            pkg = decode_sproto(raw, 0)
            msg = get_val_int(pkg, 0, -1)
            session = get_val_int(pkg, 1, None)

            field_count = struct.unpack_from("<H", raw, 0)[0]
            body_offset = 2 + field_count * 2
            body = decode_sproto(raw, body_offset) if body_offset <= len(raw) else {}

            log(f"[RX] size={size} tag={msg} session={session}")

            response = handle_message(conn, msg, session, body)
            send_rpc_response(conn, session, response)

    except socket.timeout:
        log(f"[TIMEOUT] {addr}")
    except ConnectionResetError:
        log(f"[RESET] {addr}")
    except Exception:
        log(f"[ERROR] {addr}")
        traceback.print_exc()
    finally:
        for cid, entry in list(ONLINE_PLAYERS.items()):
            if entry["conn"] is conn:
                try:
                    save_character(entry["data"])
                except Exception:
                    pass
                del ONLINE_PLAYERS[cid]
        try:
            conn.close()
        except Exception:
            pass
        log(f"[CLOSED] {addr}")


def main():
    log("========================================")
    log("ATG 9555 GAME SERVER")
    log(f"LISTEN: {HOST}:{PORT}")
    log(f"GAME VERSION: {GAME_VERSION}")
    log(f"DATA VERSION: {DATA_VERSION}")
    log("FLOW: server-select -> character-list -> character-pick -> 614/611/540 -> 503 -> 504 -> map_ready(100)")
    log("========================================")

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind((HOST, PORT))
    server.listen(50)

    log(f"[LISTENING] {HOST}:{PORT}")

    while True:
        conn, addr = server.accept()
        threading.Thread(
            target=client_handler,
            args=(conn, addr),
            daemon=True
        ).start()


if __name__ == "__main__":
    main()



