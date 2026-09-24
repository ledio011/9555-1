import os
import socket
import struct
import threading
import json
import traceback

HOST = "0.0.0.0"
PORT = int(os.environ.get("PORT", "15678"))

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
INDEX_FILE = os.path.join(BASE_DIR, "apk_index", "index.json")


# ============================================================
# LOAD APK INDEX
# ============================================================

APK_INDEX = None


def load_index():
    global APK_INDEX

    if not os.path.exists(INDEX_FILE):
        print(f"[ERROR] Missing index: {INDEX_FILE}", flush=True)
        return False

    try:
        with open(INDEX_FILE, "r", encoding="utf-8") as f:
            APK_INDEX = json.load(f)

        print(
            f"[+] APK index loaded: "
            f"{len(APK_INDEX.get('files', []))} files",
            flush=True
        )

        return True

    except Exception as e:
        print(f"[ERROR] Cannot load APK index: {e}", flush=True)
        return False


# ============================================================
# TCP FRAME
# ============================================================

def recv_exact(sock, size):
    data = bytearray()

    while len(data) < size:
        chunk = sock.recv(size - len(data))

        if not chunk:
            return None

        data.extend(chunk)

    return bytes(data)


def recv_frame(sock):
    header = recv_exact(sock, 2)

    if header is None:
        return None

    size = struct.unpack(">H", header)[0]

    if size == 0:
        return b""

    return recv_exact(sock, size)


def send_frame(sock, payload):
    if payload is None:
        return

    if len(payload) > 65535:
        raise ValueError(
            f"Payload too large: {len(payload)}"
        )

    sock.sendall(
        struct.pack(">H", len(payload)) +
        payload
    )


# ============================================================
# SPROTO PACK
# ============================================================

def sproto_unpack(data):
    """
    Decode sproto's 8-byte zero-compressed blocks.
    """

    out = bytearray()
    pos = 0

    while pos < len(data):

        bitmap = data[pos]
        pos += 1

        for i in range(8):

            if bitmap & (1 << i):

                if pos >= len(data):
                    raise ValueError(
                        "Invalid sproto packed data"
                    )

                out.append(data[pos])
                pos += 1

            else:
                out.append(0)

    return bytes(out)


def sproto_pack(data):
    """
    Encode raw sproto data using zero compression.
    """

    out = bytearray()
    pos = 0

    while pos < len(data):

        chunk = data[pos:pos + 8]
        pos += len(chunk)

        bitmap = 0
        values = bytearray()

        for i, value in enumerate(chunk):

            if value != 0:
                bitmap |= (1 << i)
                values.append(value)

        out.append(bitmap)
        out.extend(values)

    return bytes(out)


# ============================================================
# BASIC SPROTO HEADER INSPECTION
# ============================================================

def inspect_packet(raw):
    """
    This does NOT invent a response.

    It only extracts useful information for logging.
    """

    if len(raw) < 2:
        return None, None

    try:
        header_words = struct.unpack(
            "<H",
            raw[:2]
        )[0]

        header_size = 2 + header_words * 2

        if header_size > len(raw):
            return None, None

        fields = {}

        p = 2

        for tag in range(header_words):

            if p + 2 > len(raw):
                break

            value = struct.unpack(
                "<H",
                raw[p:p + 2]
            )[0]

            p += 2

            fields[tag] = value

        msg = fields.get(0)
        session = fields.get(1)

        return msg, session

    except Exception:
        return None, None


# ============================================================
# APK INDEX SEARCH
# ============================================================

def search_index_for_tag(msg):
    """
    Search textual/string extracts for references to TAG/msg.

    This is intentionally only a lookup.
    It does NOT fabricate an Sproto response.
    """

    if not APK_INDEX:
        return []

    needle1 = f"tag {msg}"
    needle2 = f"tag={msg}"
    needle3 = f"msg={msg}"
    needle4 = f"msg {msg}"

    results = []

    for entry in APK_INDEX.get("files", []):

        path = entry.get("path", "")

        text_path = entry.get("text_extract")
        strings_path = entry.get("strings_extract")

        candidates = []

        if text_path:
            candidates.append(
                os.path.join(BASE_DIR, text_path)
            )

        if strings_path:
            candidates.append(
                os.path.join(BASE_DIR, strings_path)
            )

        for candidate in candidates:

            if not os.path.exists(candidate):
                continue

            try:

                with open(
                    candidate,
                    "r",
                    encoding="utf-8",
                    errors="ignore"
                ) as f:

                    text = f.read()

                low = text.lower()

                if (
                    needle1 in low
                    or needle2 in low
                    or needle3 in low
                    or needle4 in low
                ):

                    results.append({
                        "path": path,
                        "extract": candidate
                    })

                    break

            except Exception:
                pass

    return results


# ============================================================
# RESPONSE RESOLVER
# ============================================================

def resolve_response(msg, session, raw_request):
    """
    IMPORTANT:

    apk_indexer.py currently creates an INDEX.
    It does not contain a database of ready-made server
    responses.

    Therefore this function currently returns None instead
    of inventing fake protocol data.
    """

    matches = search_index_for_tag(msg)

    print(
        f"[INDEX LOOKUP] MSG={msg} "
        f"SESSION={session} "
        f"MATCHES={len(matches)}",
        flush=True
    )

    for match in matches[:10]:
        print(
            f"    -> {match['path']}",
            flush=True
        )

    return None


# ============================================================
# CLIENT
# ============================================================

def client_thread(conn, addr):

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

            print(
                f"[RX] FRAME SIZE={len(packet)}",
                flush=True
            )

            try:
                raw = sproto_unpack(packet)

            except Exception as e:

                print(
                    f"[SPROTO ERROR] {e}",
                    flush=True
                )

                continue

            msg, session = inspect_packet(raw)

            print(
                f"[RX] MSG={msg} SESSION={session}",
                flush=True
            )

            response = resolve_response(
                msg,
                session,
                raw
            )

            if response is None:

                print(
                    f"[NO RESPONSE] MSG={msg}",
                    flush=True
                )

                continue

            packed = sproto_pack(response)

            send_frame(
                conn,
                packed
            )

            print(
                f"[TX] MSG={msg} "
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

def main():

    print("=" * 60)
    print("ATG 9555 APK-INDEX SERVER")
    print("=" * 60)

    if not load_index():
        raise SystemExit(1)

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
        f"[LISTENING] {HOST}:{PORT}",
        flush=True
    )

    print(
        f"[INDEX] {INDEX_FILE}",
        flush=True
    )

    print("=" * 60)

    while True:

        conn, addr = server.accept()

        t = threading.Thread(
            target=client_thread,
            args=(conn, addr),
            daemon=True
        )

        t.start()


if __name__ == "__main__":
    main()
