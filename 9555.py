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
# CONFIG
# ============================================================

HOST = "0.0.0.0"
PORT = int(
    os.environ.get(
        "PORT",
        "15678"
    )
)

BASE_DIR = Path(
    __file__
).resolve().parent

INDEX_DIR = (
    BASE_DIR
    / "apk_index"
)

INDEX_FILE = (
    INDEX_DIR
    / "index.json"
)


# ============================================================
# RAM CACHE
# ============================================================

APK_INDEX = None

SOURCE_CACHE = []

NUMERIC_CACHE = defaultdict(list)

TEXT_CACHE = defaultdict(list)

SPROTO_TYPES = {}
SPROTO_PROTOCOLS = {}
PACKAGE_FIELDS = {}


# ============================================================
# LOAD INDEX
# ============================================================

def load_index():

    global APK_INDEX

    if not INDEX_FILE.exists():

        print(
            f"[ERROR] Missing index: "
            f"{INDEX_FILE}",
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

        files = APK_INDEX.get(
            "files",
            []
        )

        print(
            f"[+] APK index loaded: "
            f"{len(files)} files",
            flush=True
        )

        build_ram_cache(
            files
        )

        load_source_protocol()

        return True

    except Exception as e:

        print(
            f"[ERROR] Cannot load index: "
            f"{e}",
            flush=True
        )

        traceback.print_exc()

        return False


# ============================================================
# BUILD RAM CACHE
# ============================================================

def build_ram_cache(files):

    SOURCE_CACHE.clear()

    NUMERIC_CACHE.clear()

    TEXT_CACHE.clear()

    text_count = 0

    string_count = 0

    for entry in files:

        record = {

            "path":
                entry.get(
                    "path",
                    ""
                ),

            "relative_path":
                entry.get(
                    "relative_path",
                    ""
                ),

            "name":
                entry.get(
                    "name",
                    ""
                ),

            "type":
                entry.get(
                    "type",
                    ""
                ),

            "text":
                "",

            "strings":
                "",

            "metadata":
                entry.get(
                    "metadata",
                    {}
                ),

            "protocol_candidates":
                entry.get(
                    "protocol_candidates",
                    []
                )
        }

        # ----------------------------------------------------
        # TEXT
        # ----------------------------------------------------

        text_file = entry.get(
            "text_file"
        )

        if text_file:

            path = Path(
                text_file
            )

            if not path.is_absolute():

                path = (
                    BASE_DIR
                    / path
                )

            if path.exists():

                try:

                    record["text"] = (
                        path.read_text(
                            encoding="utf-8",
                            errors="ignore"
                        )
                    )

                    text_count += 1

                except Exception:
                    pass

        # ----------------------------------------------------
        # STRINGS
        # ----------------------------------------------------

        strings_file = entry.get(
            "strings_file"
        )

        if strings_file:

            path = Path(
                strings_file
            )

            if not path.is_absolute():

                path = (
                    BASE_DIR
                    / path
                )

            if path.exists():

                try:

                    record["strings"] = (
                        path.read_text(
                            encoding="utf-8",
                            errors="ignore"
                        )
                    )

                    string_count += 1

                except Exception:
                    pass

        SOURCE_CACHE.append(
            record
        )

        # ----------------------------------------------------
        # NUMERIC INDEX
        # ----------------------------------------------------

        for relation in record[
            "protocol_candidates"
        ]:

            value = relation.get(
                "value"
            )

            if value is None:
                continue

            NUMERIC_CACHE[
                str(value)
            ].append({

                "source":
                    record[
                        "relative_path"
                    ],

                "name":
                    relation.get(
                        "name"
                    ),

                "context":
                    relation.get(
                        "context",
                        ""
                    )
            })

        # ----------------------------------------------------
        # TEXT INDEX
        # ----------------------------------------------------

        combined = (
            record["text"]
            + "\n"
            + record["strings"]
        )

        if not combined:
            continue

        words = re.findall(
            r"[A-Za-z_][A-Za-z0-9_.]{2,}",
            combined
        )

        for word in set(words):

            TEXT_CACHE[
                word.lower()
            ].append(
                record[
                    "relative_path"
                ]
            )

    print(
        "[+] RAM cache ready",
        flush=True
    )

    print(
        f"    Text extracts   : "
        f"{text_count}",
        flush=True
    )

    print(
        f"    String extracts : "
        f"{string_count}",
        flush=True
    )

    print(
        f"    Numeric keys    : "
        f"{len(NUMERIC_CACHE)}",
        flush=True
    )

    print(
        f"    Text keys       : "
        f"{len(TEXT_CACHE)}",
        flush=True
    )


# ============================================================
# SOURCE-DERIVED SPROTO
# ============================================================

def load_source_protocol():

    global SPROTO_TYPES
    global SPROTO_PROTOCOLS
    global PACKAGE_FIELDS

    SPROTO_TYPES = {}
    SPROTO_PROTOCOLS = {}
    PACKAGE_FIELDS = {}

    types_file = (
        INDEX_DIR / "protocol" / "sproto_types.json"
    )

    protocols_file = (
        INDEX_DIR / "protocol" / "sproto_protocols.json"
    )

    try:

        if types_file.exists():

            SPROTO_TYPES = json.loads(
                types_file.read_text(
                    encoding="utf-8",
                    errors="ignore"
                )
            )

        if protocols_file.exists():

            SPROTO_PROTOCOLS = json.loads(
                protocols_file.read_text(
                    encoding="utf-8",
                    errors="ignore"
                )
            )

        for name, schema in SPROTO_TYPES.items():

            if name.lower().split(".")[-1] != "package":
                continue

            for field in schema.get("fields", []):

                field_name = field.get("name")

                if field_name:
                    PACKAGE_FIELDS[
                        field_name.lower()
                    ] = field.get("tag")

            break

    except Exception as e:

        print(
            f"[SPROTO SOURCE ERROR] {e}",
            flush=True
        )

    print(
        f"[SPROTO SOURCE] "
        f"types={len(SPROTO_TYPES)} "
        f"protocols={len(SPROTO_PROTOCOLS)} "
        f"package_fields={PACKAGE_FIELDS}",
        flush=True
    )


def find_protocol(msg):

    if msg is None:
        return None

    return SPROTO_PROTOCOLS.get(str(msg))


def decode_sproto_integer(value):

    if value is None:
        return None

    if value == 0:
        return 0

    if value % 2 == 0:
        return (value // 2) - 1

    return value


def encode_sproto_integer(value):

    try:
        value = int(value)
    except Exception:
        return None

    if value < 0:
        return None

    encoded = (value + 1) * 2

    if encoded > 0xFFFF:
        return None

    return encoded


def build_source_response(msg, session):

    protocol = find_protocol(msg)

    if protocol is None or session is None:
        return None

    session_tag = PACKAGE_FIELDS.get("session")

    if session_tag is None:
        return None

    field_count = int(session_tag) + 1
    fields = [0] * field_count

    encoded_session = encode_sproto_integer(session)

    if encoded_session is None:
        return None

    fields[int(session_tag)] = encoded_session

    raw = (
        struct.pack("<H", field_count)
        + b"".join(
            struct.pack("<H", value)
            for value in fields
        )
    )

    return sproto_pack(raw)


# ============================================================
# SOURCE LOOKUPS
# ============================================================

def lookup_number(value):

    if value is None:
        return []

    return NUMERIC_CACHE.get(
        str(value),
        []
    )


def lookup_text(value):

    if not value:
        return []

    key = str(value).lower()

    results = []

    for path in TEXT_CACHE.get(
        key,
        []
    ):

        results.append({
            "path": path,
            "match": key
        })

    return results


# ============================================================
# TCP
# ============================================================

def recv_exact(
    sock,
    size
):

    data = bytearray()

    while len(data) < size:

        chunk = sock.recv(
            size - len(data)
        )

        if not chunk:
            return None

        data.extend(
            chunk
        )

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


def send_frame(
    sock,
    payload
):

    if payload is None:
        return

    if len(payload) > 65535:

        raise ValueError(
            f"Payload too large: "
            f"{len(payload)}"
        )

    sock.sendall(
        struct.pack(
            ">H",
            len(payload)
        )
        + payload
    )


# ============================================================
# SPROTO PACK
# ============================================================

def sproto_unpack(data):

    out = bytearray()

    pos = 0

    while pos < len(data):

        bitmap = data[
            pos
        ]

        pos += 1

        for i in range(8):

            if bitmap & (
                1 << i
            ):

                if pos >= len(data):

                    raise ValueError(
                        "Invalid sproto data"
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

        for i, value in enumerate(
            chunk
        ):

            if value != 0:

                bitmap |= (
                    1 << i
                )

                values.append(
                    value
                )

        out.append(
            bitmap
        )

        out.extend(
            values
        )

    return bytes(out)


# ============================================================
# PACKET INSPECTION
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
        position = 2

        for tag in range(header_words):

            if position + 2 > len(raw):
                break

            fields[tag] = struct.unpack(
                "<H",
                raw[position:position + 2]
            )[0]

            position += 2

        type_tag = PACKAGE_FIELDS.get("type")
        session_tag = PACKAGE_FIELDS.get("session")

        msg = (
            fields.get(type_tag)
            if type_tag is not None
            else None
        )

        session = (
            fields.get(session_tag)
            if session_tag is not None
            else None
        )

        if msg is not None:
            msg = decode_sproto_integer(msg)

        if session is not None:
            session = decode_sproto_integer(session)

        return msg, session

    except Exception:

        return None, None


# ============================================================
# AUTOMATIC RESOLVER
# ============================================================

def resolve_request(
    msg,
    session,
    raw
):

    if msg is None:

        return {
            "matches": [],
            "known": False
        }

    matches = lookup_number(
        msg
    )

    print(
        f"[RESOLVE] "
        f"MSG={msg} "
        f"SESSION={session} "
        f"MATCHES={len(matches)}",
        flush=True
    )

    for item in matches[:10]:

        print(
            f"    -> "
            f"{item.get('source')}"
            + (
                f" | {item.get('name')}"
                if item.get("name")
                else ""
            ),
            flush=True
        )

    return {
        "matches": matches,
        "known": bool(matches)
    }


# ============================================================
# CLIENT
# ============================================================

def client_thread(
    conn,
    addr
):

    print(
        f"[CONNECTED] {addr}",
        flush=True
    )

    try:

        while True:

            packet = recv_frame(
                conn
            )

            if packet is None:
                break

            if not packet:
                continue

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

            msg, session = (
                inspect_packet(raw)
            )

            print(
                f"[RX] "
                f"MSG={msg} "
                f"SESSION={session}",
                flush=True
            )

            result = resolve_request(
                msg,
                session,
                raw
            )

            if not result["known"]:

                print(
                    f"[SOURCE UNKNOWN] "
                    f"MSG={msg}",
                    flush=True
                )

            else:

                response = build_source_response(
                    msg,
                    session
                )

                if response is None:

                    print(
                        f"[SOURCE FOUND / "
                        f"NO RESPONSE SCHEMA] "
                        f"MSG={msg} "
                        f"SESSION={session}",
                        flush=True
                    )

                    continue

                send_frame(
                    conn,
                    response
                )

                protocol = find_protocol(msg)

                print(
                    f"[TX] "
                    f"MSG={msg} "
                    f"SESSION={session} "
                    f"PROTOCOL="
                    f"{protocol.get('name') if protocol else None} "
                    f"RESPONSE="
                    f"{protocol.get('response') if protocol else None}",
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
    print(
        "ATG 9555 SOURCE RESOLVER"
    )
    print("=" * 60)

    # --------------------------------------------------------
    # IMPORTANT:
    #
    # Read source/index BEFORE accepting clients.
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

    server.listen(
        128
    )

    print(
        f"[LISTENING] "
        f"{HOST}:{PORT}",
        flush=True
    )

    print(
        "[READY] "
        "dec&normal index loaded into RAM.",
        flush=True
    )

    print("=" * 60)

    while True:

        conn, addr = (
            server.accept()
        )

        thread = threading.Thread(
            target=client_thread,
            args=(
                conn,
                addr
            ),
            daemon=True
        )

        thread.start()


if __name__ == "__main__":

    main()
