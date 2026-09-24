import os
import socket
import struct
import threading
import json
import traceback
import re
from pathlib import Path
from collections import defaultdict


# ============================================================
# SERVER CONFIG
# ============================================================

HOST = "0.0.0.0"
PORT = int(os.environ.get("PORT", "15678"))

BASE_DIR = Path(__file__).resolve().parent
INDEX_DIR = BASE_DIR / "apk_index"
INDEX_FILE = INDEX_DIR / "index.json"

APK_INDEX = None

# Everything extracted from dec&normal is loaded here once.
SOURCE_CACHE = []

# Numeric lookup:
# number -> source entries
NUMERIC_CACHE = defaultdict(list)

# Text lookup:
# lowercase token -> source entries
TEXT_CACHE = defaultdict(list)


# ============================================================
# INDEX LOADER
# ============================================================

def load_index():

    global APK_INDEX

    if not INDEX_FILE.exists():

        print(
            f"[ERROR] Missing index: {INDEX_FILE}",
            flush=True
        )

        return False

    try:

        with open(
            INDEX_FILE,
            "r",
            encoding="utf-8"
        ) as f:

            APK_INDEX = json.load(f)

        files = APK_INDEX.get("files", [])

        print(
            f"[+] APK index loaded: {len(files)} files",
            flush=True
        )

        build_source_cache(files)

        return True

    except Exception as e:

        print(
            f"[ERROR] Cannot load APK index: {e}",
            flush=True
        )

        traceback.print_exc()

        return False


# ============================================================
# SOURCE CACHE
# ============================================================

def build_source_cache(files):

    SOURCE_CACHE.clear()
    NUMERIC_CACHE.clear()
    TEXT_CACHE.clear()

    loaded_text = 0
    loaded_strings = 0

    for entry in files:

        relative_path = entry.get(
            "relative_path",
            ""
        )

        source_path = entry.get(
            "path",
            relative_path
        )

        record = {
            "path": source_path,
            "relative_path": relative_path,
            "type": entry.get("type"),
            "name": entry.get("name"),
            "text": "",
            "strings": "",
            "metadata": entry.get(
                "metadata",
                {}
            ),
            "protocol_candidates": entry.get(
                "protocol_candidates",
                []
            )
        }

        # ----------------------------------------------------
        # TEXT EXTRACT
        # ----------------------------------------------------

        text_file = entry.get("text_file")

        if text_file:

            path = Path(text_file)

            if not path.is_absolute():
                path = BASE_DIR / path

            if path.exists():

                try:

                    record["text"] = path.read_text(
                        encoding="utf-8",
                        errors="ignore"
                    )

                    loaded_text += 1

                except Exception:
                    pass

        # ----------------------------------------------------
        # STRING EXTRACT
        # ----------------------------------------------------

        strings_file = entry.get("strings_file")

        if strings_file:

            path = Path(strings_file)

            if not path.is_absolute():
                path = BASE_DIR / path

            if path.exists():

                try:

                    record["strings"] = path.read_text(
                        encoding="utf-8",
                        errors="ignore"
                    )

                    loaded_strings += 1

                except Exception:
                    pass

        SOURCE_CACHE.append(record)

        # ----------------------------------------------------
        # INDEX NUMERIC RELATIONS
        # ----------------------------------------------------

        for relation in record["protocol_candidates"]:

            value = relation.get("value")

            if value is None:
                continue

            NUMERIC_CACHE[str(value)].append({
                "path": relative_path,
                "name": relation.get("name"),
                "context": relation.get(
                    "context",
                    ""
                )
            })

        # ----------------------------------------------------
        # INDEX TEXT
        # ----------------------------------------------------

        combined = (
            record["text"]
            + "\n"
            + record["strings"]
        )

        if combined:

            words = re.findall(
                r"[A-Za-z_][A-Za-z0-9_.]{2,}",
                combined
            )

            for word in set(words):

                TEXT_CACHE[
                    word.lower()
                ].append(relative_path)

    print(
        f"[+] Source cache ready",
        flush=True
    )

    print(
        f"    Text extracts   : {loaded_text}",
        flush=True
    )

    print(
        f"    String extracts : {loaded_strings}",
        flush=True
    )

    print(
        f"    Numeric keys    : {len(NUMERIC_CACHE)}",
        flush=True
    )

    print(
        f"    Text keys       : {len(TEXT_CACHE)}",
        flush=True
    )


# ============================================================
# SOURCE SEARCH
# ============================================================

def search_source_number(number):

    return NUMERIC_CACHE.get(
        str(number),
        []
    )


def search_source_text(value):

    if not value:
        return []

    value = str(value).lower()

    results = []

    # Exact indexed token first.
    for path in TEXT_CACHE.get(value, []):

        results.append({
            "path": path,
            "match": value
        })

    # Then source-content search.
    if not results:

        for entry in SOURCE_CACHE:

            text = (
                entry["text"]
                + "\n"
                + entry["strings"]
            )

            if value in text.lower():

                results.append({
                    "path": entry["relative_path"],
                    "match": value
                })

    return results


# ============================================================
# REQUEST CONTEXT
# ============================================================

def inspect_packet(raw):

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
# AUTOMATIC SOURCE RESOLVER
# ============================================================

def resolve_source(msg):

    """
    Find everything the local dec&normal-derived index
    knows about this numeric value.

    No ATG-specific message list exists here.
    """

    if msg is None:
        return []

    return search_source_number(msg)


def resolve_response(msg, session, raw_request):

    matches = resolve_source(msg)

    print(
        f"[RESOLVE] MSG={msg} "
        f"SESSION={session} "
        f"SOURCE_MATCHES={len(matches)}",
        flush=True
    )

    for match in matches[:10]:

        name = match.get("name")

        print(
            f"    -> {match.get('path')}"
            + (
                f" | name={name}"
                if name
                else ""
            ),
            flush=True
        )

    # --------------------------------------------------------
    # IMPORTANT
    #
    # The source index describes the CLIENT source/data.
    # It does not automatically contain live server state
    # or ready-made response packets.
    #
    # Therefore do not fabricate bytes here.
    # --------------------------------------------------------

    return None


# ============================================================
# TCP FRAME
# ============================================================

def recv_exact(sock, size):

    data = bytearray()

    while len(data) < size:

        chunk = sock.recv(
            size - len(data)
        )

        if not chunk:
            return None

        data.extend(chunk)

    return bytes(data)


def recv_frame(sock):

    header = recv_exact(
        sock,
        2
    )

    if header is None:
        return None

    size = struct.unpack(
        ">H",
        header
    )[0]

    if size == 0:
        return b""

    return recv_exact(
        sock,
        size
    )


def send_frame(sock, payload):

    if payload is None:
        return

    if len(payload) > 65535:

        raise ValueError(
            f"Payload too large: {len(payload)}"
        )

    sock.sendall(
        struct.pack(
            ">H",
            len(payload)
        )
        + payload
    )


# ============================================================
# SPROTO
# ============================================================

def sproto_unpack(data):

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

                out.append(
                    data[pos]
                )

                pos += 1

            else:

                out.append(0)

    return bytes(out)


def sproto_pack(data):

    out = bytearray()
    pos = 0

    while pos < len(data):

        chunk = data[
            pos:pos + 8
        ]

        pos += len(chunk)

        bitmap = 0
        values = bytearray()

        for i, value in enumerate(chunk):

            if value != 0:

                bitmap |= (
                    1 << i
                )

                values.append(value)

        out.append(bitmap)
        out.extend(values)

    return bytes(out)


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

                raw = sproto_unpack(
                    packet
                )

            except Exception as e:

                print(
                    f"[SPROTO ERROR] {e}",
                    flush=True
                )

                continue

            msg, session = inspect_packet(
                raw
            )

            print(
                f"[RX] MSG={msg} "
                f"SESSION={session}",
                flush=True
            )

            response = resolve_response(
                msg,
                session,
                raw
            )

            if response is None:

                print(
                    f"[NO RESPONSE] "
                    f"MSG={msg}",
                    flush=True
                )

                continue

            packed = sproto_pack(
                response
            )

            send_frame(
                conn,
                packed
            )

            print(
                f"[TX] SIZE={len(packed)}",
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
    print("ATG 9555 SOURCE-INDEX SERVER")
    print("=" * 60)

    # --------------------------------------------------------
    # IMPORTANT:
    # This happens BEFORE accept().
    #
    # dec&normal -> apk_index -> RAM
    # --------------------------------------------------------

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
        (
            HOST,
            PORT
        )
    )

    server.listen(128)

    print(
        f"[LISTENING] "
        f"{HOST}:{PORT}",
        flush=True
    )

    print(
        f"[INDEX] "
        f"{INDEX_FILE}",
        flush=True
    )

    print(
        "[READY] Source data is cached in RAM.",
        flush=True
    )

    print("=" * 60)

    while True:

        conn, addr = server.accept()

        thread = threading.Thread(
            target=client_thread,
            args=(conn, addr),
            daemon=True
        )

        thread.start()


if __name__ == "__main__":

    main()
