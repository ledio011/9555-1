import socket
import struct
import traceback
import os
import time

HOST = "0.0.0.0"
PORT = 15678

RAW_DIR = "raw_requests"
os.makedirs(RAW_DIR, exist_ok=True)

# Vetëm emërtim nga kodi real i 9555.py.
# KJO NUK përcakton se çfarë request-i do të bëjë klienti.
REQUEST_NAMES = {
    4: "login",
    7: "update_game_server",
    100: "map_ready",
    101: "move",
    103: "character_list",
    104: "character_create",
    105: "character_pick",
    108: "leave_copy_scene",
    112: "accept_mission",
    113: "complete_mission",
    118: "random_name",
    193: "car_chase_result",
    218: "heartbeat",
    220: "start_battle",
}

request_counter = 0


# ============================================================
# SPROTO PACK / UNPACK
# ============================================================

def sproto_unpack(data):
    out = bytearray()
    i = 0
    n = len(data)

    while i < n:
        mask = data[i]
        i += 1

        if mask == 0xFF:
            if i >= n:
                break

            count = (data[i] + 1) * 8
            i += 1

            out.extend(data[i:i + count])
            i += count

        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < n:
                        out.append(data[i])
                        i += 1
                else:
                    out.append(0)

    return bytes(out)


def sproto_pack(data):
    out = bytearray()
    i = 0
    n = len(data)

    while i < n:
        chunk = data[i:i + 8]
        i += len(chunk)

        mask = 0
        body = bytearray()

        for bit, value in enumerate(chunk):
            if value != 0:
                mask |= (1 << bit)
                body.append(value)

        if len(chunk) == 8 and mask == 0:
            # Literal zero block
            out.append(0)
            continue

        out.append(mask)
        out.extend(body)

    return bytes(out)


# ============================================================
# SPROTO DECODER
# ============================================================

def decode_sproto(data, offset=0):
    fields = {}

    if len(data) < offset + 2:
        return fields

    try:
        fn = struct.unpack_from("<H", data, offset)[0]

        h_ptr = offset + 2
        b_ptr = offset + 2 + fn * 2

        if b_ptr > len(data):
            return fields

        curr_tag = -1

        for i in range(fn):
            pos = h_ptr + i * 2

            if pos + 2 > len(data):
                break

            v = struct.unpack_from("<H", data, pos)[0]

            if v == 0:
                curr_tag += 1

                if b_ptr + 4 <= len(data):
                    length = struct.unpack_from("<I", data, b_ptr)[0]
                    b_ptr += 4

                    if b_ptr + length <= len(data):
                        fields[curr_tag] = data[b_ptr:b_ptr + length]
                        b_ptr += length

            elif v == 1:
                curr_tag += 1

            elif v & 1:
                curr_tag += (v >> 1) + 1

            else:
                curr_tag += 1
                fields[curr_tag] = (v >> 1) - 1

    except Exception:
        pass

    return fields


def get_int(fields, tag, default=None):
    value = fields.get(tag)

    if value is None:
        return default

    if isinstance(value, int):
        return value

    if isinstance(value, bytes):
        if len(value) == 1:
            return value[0]

        if len(value) == 2:
            return int.from_bytes(value, "little", signed=True)

        if len(value) == 4:
            return int.from_bytes(value, "little", signed=True)

        if len(value) == 8:
            return int.from_bytes(value, "little", signed=True)

    return default


def decode_value(value):
    if isinstance(value, int):
        return value

    if not isinstance(value, bytes):
        return value

    if len(value) == 0:
        return ""

    # Integer-like values
    if len(value) in (1, 2, 4, 8):
        try:
            i = int.from_bytes(value, "little", signed=True)

            # Keep printable strings when they clearly are strings.
            try:
                s = value.decode("utf-8")
                if all((c.isprintable() or c in "\r\n\t") for c in s):
                    return repr(s)
            except Exception:
                pass

            return i
        except Exception:
            pass

    # String
    try:
        s = value.decode("utf-8")

        if all((c.isprintable() or c in "\r\n\t") for c in s):
            return repr(s)
    except Exception:
        pass

    # Nested sproto
    try:
        nested = decode_sproto(value)

        if nested:
            return {
                f"tag_{k}": decode_value(v)
                for k, v in nested.items()
            }
    except Exception:
        pass

    return "0x" + value.hex()


# ============================================================
# REQUEST DECODER
# ============================================================

def decode_client_request(raw):
    """
    Actual Sproto RPC package:

        tag 0 = request/message id
        tag 1 = session

        remaining bytes = request body
    """

    package = decode_sproto(raw, 0)

    msg = get_int(package, 0)
    session = get_int(package, 1)

    # Same offset logic used by 9555.py
    if len(raw) >= 2:
        fn = struct.unpack_from("<H", raw, 0)[0]
        body_offset = 2 + fn * 2
    else:
        body_offset = len(raw)

    body = decode_sproto(raw, body_offset)

    return msg, session, body


# ============================================================
# REQUEST REPORT
# ============================================================

def print_request(number, msg, session, body, raw):
    name = REQUEST_NAMES.get(msg)

    print()
    print("=" * 72)
    print(f"REQUEST #{number}")

    if name:
        print(f"TAG      = {msg}")
        print(f"NAME     = {name}")
    else:
        print(f"TAG      = {msg}")
        print("NAME     = NOT_IN_9555_HANDLER")

    print(f"SESSION  = {session}")
    print("DIRECTION = CLIENT -> SERVER")

    if body:
        print("FIELDS:")

        for tag, value in sorted(body.items()):
            print(
                f"    FIELD[{tag}] = "
                f"{decode_value(value)}"
            )
    else:
        print("FIELDS   = {}")

    print(f"RAW_SIZE = {len(raw)} bytes")
    print("=" * 72)

    if not name:
        print()
        print(">>> MISSING REQUEST HANDLER DETECTED <<<")
        print(f">>> CLIENT SENT TAG {msg}")
        print(">>> 9555.py does not have a known handler for this tag")
        print()


# ============================================================
# RAW REQUEST SAVE
# ============================================================

def save_raw(number, raw):
    path = os.path.join(
        RAW_DIR,
        f"request_{number:04d}.bin"
    )

    with open(path, "wb") as f:
        f.write(raw)


# ============================================================
# READ ONE TCP FRAME
# ============================================================

def recv_frame(conn, buffer):
    while True:

        # Need 2-byte big-endian frame size
        if len(buffer) >= 2:
            size = struct.unpack(">H", buffer[:2])[0]

            if len(buffer) >= 2 + size:
                frame = buffer[2:2 + size]
                buffer = buffer[2 + size:]
                return frame, buffer

        data = conn.recv(65536)

        if not data:
            return None, buffer

        buffer += data


# ============================================================
# SERVER
# ============================================================

def handle_client(conn, addr):
    global request_counter

    print()
    print(f"[+] CLIENT CONNECTED: {addr}")

    buffer = b""

    try:
        while True:

            packed, buffer = recv_frame(conn, buffer)

            if packed is None:
                break

            if not packed:
                continue

            try:
                raw = sproto_unpack(packed)

                msg, session, body = decode_client_request(raw)

                request_counter += 1
                number = request_counter

                save_raw(number, raw)

                print_request(
                    number,
                    msg,
                    session,
                    body,
                    raw
                )

                # ==================================================
                # IMPORTANT:
                #
                # KETU futet serveri yt real.
                #
                # Mos vendosim:
                #
                #   if request == 1: login
                #   if request == 2: character_list
                #
                # sepse klienti duhet të vendosë vetë çfarë kërkon.
                #
                # ==================================================

                response = handle_real_request(
                    conn,
                    msg,
                    session,
                    body
                )

                if response:
                    send_frame(conn, response)

            except Exception as e:
                print()
                print("[!] REQUEST DECODE ERROR")
                print(f"    {e}")
                traceback.print_exc()

    except Exception as e:
        print()
        print(f"[!] CLIENT ERROR {addr}: {e}")

    finally:
        try:
            conn.close()
        except Exception:
            pass

        print(f"[-] CLIENT DISCONNECTED: {addr}")


# ============================================================
# REAL SERVER HANDLER
# ============================================================

def handle_real_request(conn, msg, session, body):

    """
    KJO është pika ku futet kodi ekzistues i 9555.py.

    Request-i vjen nga CLIENT.
    Nuk krijojmë request artificial.

    msg = tag real që dërgoi APK-ja.
    session = session real.
    body = payload real.

    Handler-i ekzistues duhet të prodhojë response-in real.
    """

    # ----------------------------------------------------------
    # Këtu duhet të transferohen handler-at REALË nga 9555.py.
    # ----------------------------------------------------------

    if msg == 4:
        return handle_login(session, body)

    elif msg == 7:
        return handle_update_game_server(session, body)

    elif msg == 100:
        return handle_map_ready(session, body)

    elif msg == 101:
        return handle_move(session, body)

    elif msg == 103:
        return handle_character_list(session, body)

    elif msg == 104:
        return handle_character_create(session, body)

    elif msg == 105:
        return handle_character_pick(session, body)

    elif msg == 108:
        return handle_leave_copy_scene(session, body)

    elif msg == 112:
        return handle_accept_mission(session, body)

    elif msg == 113:
        return handle_complete_mission(session, body)

    elif msg == 118:
        return handle_random_name(session, body)

    elif msg == 193:
        return handle_car_chase_result(session, body)

    elif msg == 218:
        return handle_heartbeat(session, body)

    elif msg == 220:
        return handle_start_battle(session, body)

    else:
        # Nuk e shpikim request-in.
        #
        # Ky është request REAL i APK-së që serveri aktual
        # nuk e ka handler-in.
        print()
        print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
        print(f"MISSING SERVER HANDLER: TAG {msg}")
        print("CLIENT SENT THIS REQUEST BUT 9555.py DOES NOT HANDLE IT")
        print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")

        return None


# ============================================================
# FRAME SEND
# ============================================================

def send_frame(conn, raw):
    packed = sproto_pack(raw)

    packet = struct.pack(">H", len(packed)) + packed

    conn.sendall(packet)


# ============================================================
# PLACEHOLDER ONLY FOR EXISTING 9555 HANDLERS
# ============================================================
#
# Këto NUK duhet të jenë përgjigje të shpikura.
# Duhet të zëvendësohen me funksionet ekzistuese të 9555.py.
#

def handle_login(session, body):
    return existing_9555_login(session, body)


def handle_update_game_server(session, body):
    return existing_9555_update_game_server(session, body)


def handle_map_ready(session, body):
    return existing_9555_map_ready(session, body)


def handle_move(session, body):
    return existing_9555_move(session, body)


def handle_character_list(session, body):
    return existing_9555_character_list(session, body)


def handle_character_create(session, body):
    return existing_9555_character_create(session, body)


def handle_character_pick(session, body):
    return existing_9555_character_pick(session, body)


def handle_leave_copy_scene(session, body):
    return existing_9555_leave_copy_scene(session, body)


def handle_accept_mission(session, body):
    return existing_9555_accept_mission(session, body)


def handle_complete_mission(session, body):
    return existing_9555_complete_mission(session, body)


def handle_random_name(session, body):
    return existing_9555_random_name(session, body)


def handle_car_chase_result(session, body):
    return existing_9555_car_chase_result(session, body)


def handle_heartbeat(session, body):
    return existing_9555_heartbeat(session, body)


def handle_start_battle(session, body):
    return existing_9555_start_battle(session, body)


# ============================================================
# START
# ============================================================

def main():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    server.setsockopt(
        socket.SOL_SOCKET,
        socket.SO_REUSEADDR,
        1
    )

    server.bind((HOST, PORT))
    server.listen(20)

    print()
    print("============================================================")
    print(" ATG CLIENT REQUEST ANALYZER / SERVER")
    print("============================================================")
    print(f"LISTENING: {HOST}:{PORT}")
    print("CLIENT REQUESTS ARE DISCOVERED FROM THE TCP STREAM")
    print("NOT FROM A PREDEFINED REQUEST SEQUENCE")
    print("============================================================")
    print()

    while True:
        conn, addr = server.accept()

        handle_client(
            conn,
            addr
        )


if __name__ == "__main__":
    main()
