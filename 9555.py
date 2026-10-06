import os
import re
import socket
import struct
import traceback
from datetime import datetime

HOST = "0.0.0.0"
PORT = 15678

SERVER_SOURCE = "9555.py"
RAW_DIR = "raw_requests"

os.makedirs(RAW_DIR, exist_ok=True)

request_counter = 0


# ============================================================
# LOAD ACTUAL REQUEST HANDLERS FROM 9555.py
# ============================================================

def load_server_handlers(path):
    handlers = {}

    if not os.path.isfile(path):
        print(f"[!] SERVER SOURCE NOT FOUND: {path}")
        return handlers

    try:
        with open(path, "r", encoding="utf-8", errors="ignore") as f:
            source = f.read()
    except Exception as e:
        print(f"[!] Cannot read {path}: {e}")
        return handlers

    # Examples detected:
    # if msg == 4:       # login
    # elif msg == 103:   # character_list
    # if msg == 108:
    #
    # Also accepts:
    # if msg==4:
    # elif msg==103:

    pattern = re.compile(
        r'^\s*(?:if|elif)\s+msg\s*==\s*(\d+)\s*:\s*(?:#\s*(.*))?$',
        re.MULTILINE
    )

    for match in pattern.finditer(source):
        tag = int(match.group(1))
        comment = (match.group(2) or "").strip()

        # Search forward a little for a useful comment if this line
        # does not contain one.
        if not comment:
            line_end = match.end()
            next_text = source[line_end:line_end + 300]

            cm = re.search(
                r'\n\s*#\s*([^\n]+)',
                next_text
            )

            if cm:
                comment = cm.group(1).strip()

        handlers[tag] = {
            "name": comment if comment else f"MSG_{tag}",
            "source_line": source[:match.start()].count("\n") + 1,
        }

    return handlers


SERVER_HANDLERS = load_server_handlers(SERVER_SOURCE)


# ============================================================
# SPROTO UNPACK
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


# ============================================================
# SPROTO DECODE
# ============================================================

def decode_sproto(data, offset=0):
    fields = {}

    if len(data) < offset + 2:
        return fields

    try:
        fn = struct.unpack_from("<H", data, offset)[0]

        header_start = offset + 2
        body_start = offset + 2 + fn * 2

        if body_start > len(data):
            return fields

        current_tag = -1
        body_ptr = body_start

        for i in range(fn):
            header_ptr = header_start + i * 2

            if header_ptr + 2 > len(data):
                break

            value = struct.unpack_from(
                "<H",
                data,
                header_ptr
            )[0]

            if value == 0:
                current_tag += 1

                if body_ptr + 4 <= len(data):
                    length = struct.unpack_from(
                        "<I",
                        data,
                        body_ptr
                    )[0]

                    body_ptr += 4

                    if body_ptr + length <= len(data):
                        fields[current_tag] = (
                            data[body_ptr:
                                 body_ptr + length]
                        )

                        body_ptr += length

            elif value == 1:
                current_tag += 1

            elif value & 1:
                current_tag += (value >> 1) + 1

            else:
                current_tag += 1
                fields[current_tag] = (value >> 1) - 1

    except Exception:
        return fields

    return fields


# ============================================================
# INTEGER FROM SPROTO FIELD
# ============================================================

def get_int(fields, tag, default=None):
    value = fields.get(tag)

    if value is None:
        return default

    if isinstance(value, int):
        return value

    if not isinstance(value, (bytes, bytearray)):
        return default

    size = len(value)

    try:
        if size == 1:
            return int.from_bytes(
                value,
                "little",
                signed=True
            )

        if size == 2:
            return int.from_bytes(
                value,
                "little",
                signed=True
            )

        if size == 4:
            return int.from_bytes(
                value,
                "little",
                signed=True
            )

        if size == 8:
            return int.from_bytes(
                value,
                "little",
                signed=True
            )
    except Exception:
        pass

    return default


# ============================================================
# SAFE FIELD DISPLAY
# ============================================================

def display_field(value):
    if isinstance(value, int):
        return str(value)

    if not isinstance(value, (bytes, bytearray)):
        return repr(value)

    if len(value) == 0:
        return "EMPTY"

    # Try UTF-8 first.
    try:
        text_value = value.decode("utf-8")

        if all(
            ch.isprintable() or ch in "\r\n\t"
            for ch in text_value
        ):
            return repr(text_value)
    except Exception:
        pass

    # Integer-looking binary field.
    if len(value) in (1, 2, 4, 8):
        try:
            number = int.from_bytes(
                value,
                "little",
                signed=True
            )
            return f"{number} [0x{value.hex()}]"
        except Exception:
            pass

    return f"BYTES[{len(value)}] 0x{value.hex()}"


# ============================================================
# REQUEST BODY DECODE
# ============================================================

def decode_request(raw):
    package = decode_sproto(raw, 0)

    message_id = get_int(
        package,
        0,
        None
    )

    session = get_int(
        package,
        1,
        None
    )

    if len(raw) >= 2:
        function_count = struct.unpack_from(
            "<H",
            raw,
            0
        )[0]

        body_offset = 2 + function_count * 2
    else:
        body_offset = len(raw)

    body = decode_sproto(
        raw,
        body_offset
    )

    return message_id, session, body


# ============================================================
# SAVE RAW REQUEST
# ============================================================

def save_request(number, raw):
    filename = os.path.join(
        RAW_DIR,
        f"request_{number:05d}.bin"
    )

    try:
        with open(filename, "wb") as f:
            f.write(raw)
    except Exception as e:
        print(f"[!] RAW SAVE ERROR: {e}")


# ============================================================
# PRINT SERVER HANDLER TABLE
# ============================================================

def print_loaded_handlers():
    print()
    print("============================================================")
    print(" REQUEST HANDLERS FOUND IN 9555.py")
    print("============================================================")

    if not SERVER_HANDLERS:
        print("NONE FOUND")
    else:
        for tag in sorted(SERVER_HANDLERS):
            info = SERVER_HANDLERS[tag]

            print(
                f"TAG {tag:<4} "
                f"NAME={info['name']:<35} "
                f"LINE={info['source_line']}"
            )

    print("============================================================")
    print()


# ============================================================
# PRINT REQUEST
# ============================================================

def print_request(number, tag, session, body, raw):
    info = SERVER_HANDLERS.get(tag)

    print()
    print("================================================================")
    print(f"REQUEST #{number}")
    print("================================================================")

    print(f"TAG       = {tag}")

    if info:
        print(f"SERVER    = HANDLER EXISTS")
        print(f"NAME      = {info['name']}")
        print(f"CODE LINE = {info['source_line']}")
    else:
        print("SERVER    = MISSING HANDLER")
        print("NAME      = CLIENT REQUEST NOT IMPLEMENTED IN 9555.py")

    print(f"SESSION   = {session}")
    print("DIRECTION = CLIENT -> SERVER")
    print(f"RAW_SIZE  = {len(raw)} bytes")

    if body:
        print("FIELDS:")

        for field_tag in sorted(body):
            print(
                f"    FIELD[{field_tag}] = "
                f"{display_field(body[field_tag])}"
            )
    else:
        print("FIELDS    = {}")

    print("================================================================")

    if info is None:
        print()
        print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
        print("MISSING REQUEST DETECTED")
        print(f"CLIENT SENT TAG {tag}")
        print("9555.py HAS NO msg == TAG HANDLER")
        print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
        print()


# ============================================================
# TCP FRAME READER
# ============================================================

def read_frame(conn, buffer):
    while True:

        # 2-byte BIG-ENDIAN packet size
        if len(buffer) >= 2:
            frame_size = struct.unpack(
                ">H",
                buffer[:2]
            )[0]

            complete_size = 2 + frame_size

            if len(buffer) >= complete_size:
                frame = buffer[2:complete_size]
                buffer = buffer[complete_size:]

                return frame, buffer

        chunk = conn.recv(65536)

        if not chunk:
            return None, buffer

        buffer += chunk


# ============================================================
# CLIENT CONNECTION
# ============================================================

def handle_client(conn, addr):
    global request_counter

    print()
    print(f"[+] CLIENT CONNECTED: {addr}")

    buffer = b""

    try:
        while True:

            packed, buffer = read_frame(
                conn,
                buffer
            )

            if packed is None:
                break

            if len(packed) == 0:
                continue

            # Sproto compression/literal unpack
            raw = sproto_unpack(packed)

            tag, session, body = decode_request(raw)

            request_counter += 1

            number = request_counter

            save_request(
                number,
                raw
            )

            print_request(
                number,
                tag,
                session,
                body,
                raw
            )

            # IMPORTANT:
            #
            # NO RESPONSE IS GENERATED HERE.
            # NO REQUEST IS INVENTED HERE.
            #
            # We only inspect what the REAL CLIENT sent.
            #

    except ConnectionResetError:
        pass

    except Exception as e:
        print()
        print("[!] CONNECTION ERROR")
        print(f"    {e}")
        traceback.print_exc()

    finally:
        try:
            conn.close()
        except Exception:
            pass

        print()
        print(f"[-] CLIENT DISCONNECTED: {addr}")


# ============================================================
# SERVER
# ============================================================

def main():
    global SERVER_HANDLERS

    # Reload source every startup.
    SERVER_HANDLERS = load_server_handlers(
        SERVER_SOURCE
    )

    print()
    print("============================================================")
    print(" ATG CLIENT REQUEST DISCOVERY SERVER")
    print("============================================================")
    print(f"LISTENING       = {HOST}:{PORT}")
    print(f"SOURCE          = {SERVER_SOURCE}")
    print(
        f"HANDLERS FOUND  = "
        f"{len(SERVER_HANDLERS)}"
    )
    print(
        "MODE            = CLIENT REQUEST DISCOVERY"
    )
    print(
        "RESPONSES       = DISABLED"
    )
    print(
        "REQUESTS        = DISCOVERED FROM REAL CLIENT"
    )
    print("============================================================")

    print_loaded_handlers()

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

    server.listen(20)

    print(
        f"[*] Listening on "
        f"{HOST}:{PORT}"
    )

    while True:
        conn, addr = server.accept()

        handle_client(
            conn,
            addr
        )


if __name__ == "__main__":
    main()
