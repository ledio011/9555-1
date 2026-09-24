import socket
import struct
import subprocess
import sys
import os
import threading
import traceback

# ============================================================
# CONFIG
# ============================================================

HOST = "0.0.0.0"
PORT = int(os.environ.get("PORT", "15678"))

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
INDEXER = os.path.join(BASE_DIR, "ask-game-apk_indexer.py")

# ============================================================
# SPROTO PACK / UNPACK
# ============================================================

def sproto_pack(data: bytes) -> bytes:
    out = bytearray()
    pos = 0

    while pos < len(data):
        chunk = data[pos:pos + 8]
        pos += len(chunk)

        n = len(chunk)

        if n == 0:
            break

        # sproto packed header
        out.append(n)

        # 0-byte compression
        bitmap = 0
        payload = bytearray()

        for i in range(8):
            if i < n and chunk[i] != 0:
                bitmap |= (1 << i)
                payload.append(chunk[i])

        out[-1] = bitmap
        out.extend(payload)

    return bytes(out)


def sproto_unpack(data: bytes) -> bytes:
    out = bytearray()
    pos = 0

    while pos < len(data):
        if pos >= len(data):
            break

        bitmap = data[pos]
        pos += 1

        if bitmap == 0:
            out.extend(b"\x00" * 8)
            continue

        for i in range(8):
            if bitmap & (1 << i):
                if pos >= len(data):
                    raise ValueError("Invalid sproto packed data")

                out.append(data[pos])
                pos += 1
            else:
                out.append(0)

    return bytes(out)


# ============================================================
# FRAME
# ============================================================

def recv_exact(sock, size):
    buf = bytearray()

    while len(buf) < size:
        chunk = sock.recv(size - len(buf))

        if not chunk:
            return None

        buf.extend(chunk)

    return bytes(buf)


def recv_frame(sock):
    header = recv_exact(sock, 2)

    if header is None:
        return None

    size = struct.unpack(">H", header)[0]

    if size <= 0:
        return b""

    return recv_exact(sock, size)


def send_frame(sock, payload):
    if payload is None:
        return

    sock.sendall(
        struct.pack(">H", len(payload)) +
        payload
    )


# ============================================================
# SPROTO HEADER
# ============================================================

def read_sproto_header(payload):
    """
    Normal RPC packet:

        [sproto header]
        [body]

    First field is normally:
        0 = type/message
        1 = session
    """

    if len(payload) < 2:
        return None, None, payload

    try:
        header_size = struct.unpack("<H", payload[:2])[0]

        if header_size < 0:
            return None, None, payload

        header_end = 2 + header_size * 2

        if header_end > len(payload):
            return None, None, payload

        fields = {}

        pos = 2
        body_pos = header_end
        last_tag = -1

        for tag in range(header_size):
            if pos + 2 > len(payload):
                break

            h = struct.unpack("<H", payload[pos:pos + 2])[0]
            pos += 2

            if h == 0:
                continue

            # field header encoding
            if h & 1:
                skip = (h - 1) // 2 + 1
                tag = last_tag + skip

            else:
                tag = last_tag + 1

            value = h >> 1

            if value == 0:
                if body_pos + 4 > len(payload):
                    break

                length = struct.unpack(
                    "<I",
                    payload[body_pos:body_pos + 4]
                )[0]

                body_pos += 4

                if body_pos + length > len(payload):
                    break

                raw = payload[body_pos:body_pos + length]
                body_pos += length

                fields[tag] = raw

            else:
                fields[tag] = value - 1

            last_tag = tag

        msg = fields.get(0)
        session = fields.get(1)

        return msg, session, payload[body_pos:]

    except Exception:
        return None, None, payload


# ============================================================
# INDEXER
# ============================================================

def call_indexer(msg, session, body):
    """
    Sends the request to:

        ask-game-apk_indexer.py -response

    stdin format:

        JSON

    Expected indexer output:

        JSON containing response data.

    """

    request = {
        "msg": msg,
        "session": session,
        "body_hex": body.hex()
    }

    try:
        proc = subprocess.run(
            [
                sys.executable,
                INDEXER,
                "-response"
            ],
            input=(json_dump(request) + "\n").encode("utf-8"),
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            cwd=BASE_DIR,
            timeout=30
        )

    except subprocess.TimeoutExpired:
        print(
            f"[INDEXER TIMEOUT] MSG={msg}",
            flush=True
        )
        return None

    except Exception as e:
        print(
            f"[INDEXER ERROR] {e}",
            flush=True
        )
        return None

    stderr = proc.stderr.decode(
        "utf-8",
        errors="replace"
    ).strip()

    if stderr:
        print(
            "[INDEXER STDERR]",
            stderr,
            flush=True
        )

    output = proc.stdout.decode(
        "utf-8",
        errors="replace"
    ).strip()

    if not output:
        print(
            f"[INDEXER EMPTY] MSG={msg}",
            flush=True
        )
        return None

    return parse_indexer_response(output)


# ============================================================
# JSON
# ============================================================

def json_dump(obj):
    import json
    return json.dumps(
        obj,
        separators=(",", ":")
    )


def parse_indexer_response(output):
    """
    Accept several response formats so the indexer can evolve.

    Supported:

        {"response_hex":"...."}

    or:

        {"hex":"...."}

    or:

        {"data":"...."}

    or raw hex:

        00ff....
    """

    import json

    # JSON response
    try:
        obj = json.loads(output)

        if isinstance(obj, dict):

            for key in (
                "response_hex",
                "hex",
                "data",
                "payload"
            ):
                value = obj.get(key)

                if isinstance(value, str):
                    try:
                        return bytes.fromhex(value)
                    except ValueError:
                        pass

            # response already represented as integer list
            value = obj.get("bytes")

            if isinstance(value, list):
                return bytes(value)

    except Exception:
        pass

    # Raw HEX
    try:
        return bytes.fromhex(output)
    except ValueError:
        pass

    return None


# ============================================================
# CLIENT
# ============================================================

def handle_client(conn, addr):

    print(
        f"[CONNECTED] {addr}",
        flush=True
    )

    try:

        while True:

            packet = recv_frame(conn)

            if packet is None:
                break

            if not packet:
                continue

            # ------------------------------------------------
            # Unpack network Sproto
            # ------------------------------------------------

            try:
                raw = sproto_unpack(packet)
            except Exception as e:
                print(
                    f"[SPROTO UNPACK ERROR] {e}",
                    flush=True
                )
                continue

            # ------------------------------------------------
            # Read MSG / SESSION
            # ------------------------------------------------

            msg, session, body = read_sproto_header(raw)

            print(
                f"[RX] MSG={msg} SESSION={session} "
                f"SIZE={len(packet)}",
                flush=True
            )

            # ------------------------------------------------
            # Ask APK indexer
            # ------------------------------------------------

            response = call_indexer(
                msg,
                session,
                body
            )

            if response is None:

                print(
                    f"[NO RESPONSE] MSG={msg}",
                    flush=True
                )

                continue

            # ------------------------------------------------
            # If indexer returned a complete packed frame,
            # send it directly.
            #
            # Otherwise pack the Sproto response.
            # ------------------------------------------------

            try:

                packed = sproto_pack(response)

            except Exception as e:

                print(
                    f"[SPROTO PACK ERROR] "
                    f"MSG={msg}: {e}",
                    flush=True
                )

                continue

            send_frame(
                conn,
                packed
            )

            print(
                f"[TX] RESPONSE MSG={msg} "
                f"SIZE={len(packed)}",
                flush=True
            )

    except ConnectionResetError:
        pass

    except BrokenPipeError:
        pass

    except Exception:
        traceback.print_exc()

    finally:

        try:
            conn.close()
        except Exception:
            pass

        print(
            f"[DISCONNECTED] {addr}",
            flush=True
        )


# ============================================================
# SERVER
# ============================================================

def start_server():

    if not os.path.exists(INDEXER):

        print(
            "[ERROR] ask-game-apk_indexer.py not found:",
            INDEXER,
            flush=True
        )

        sys.exit(1)

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

    server.listen(128)

    print(
        "==========================================",
        flush=True
    )

    print(
        " ATG APK RESPONSE SERVER",
        flush=True
    )

    print(
        "==========================================",
        flush=True
    )

    print(
        f"[LISTENING] {HOST}:{PORT}",
        flush=True
    )

    print(
        f"[INDEXER] {INDEXER}",
        flush=True
    )

    print(
        "==========================================",
        flush=True
    )

    while True:

        conn, addr = server.accept()

        thread = threading.Thread(
            target=handle_client,
            args=(conn, addr),
            daemon=True
        )

        thread.start()


if __name__ == "__main__":
    start_server()
