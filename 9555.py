import os
import re
import socket
import struct
import threading


PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
DECOMPILED_ROOT = os.path.join(SCRIPT_DIR, "Decompiled")
MAX_FRAME_SIZE = 65535

ASSET_CACHE = {}
ASSET_CACHE_LOADED = False
ASSET_CACHE_LOCK = threading.Lock()
RESPONSE_CATALOG = {}
LOGIN_RESPONSE_FIELDS = None


def sproto_pack(data):
    packed = bytearray()
    for offset in range(0, len(data), 8):
        block = data[offset : offset + 8]
        mask = sum(1 << index for index, value in enumerate(block) if value)
        packed.append(mask)
        if mask == 0xFF:
            packed.append(0)
            packed.extend(block)
        else:
            packed.extend(value for value in block if value)
    return bytes(packed)


def sproto_unpack(data):
    unpacked = bytearray()
    offset = 0
    while offset < len(data):
        mask = data[offset]
        offset += 1
        if mask == 0xFF:
            if offset >= len(data):
                raise ValueError("Truncated Sproto packed block")
            count = (data[offset] + 1) * 8
            offset += 1
            if offset + count > len(data):
                raise ValueError("Truncated Sproto full block")
            unpacked.extend(data[offset : offset + count])
            offset += count
            continue

        for bit in range(8):
            if mask & (1 << bit):
                if offset >= len(data):
                    raise ValueError("Truncated Sproto sparse block")
                unpacked.append(data[offset])
                offset += 1
            else:
                unpacked.append(0)
    return bytes(unpacked)


def recv_exact(connection, size):
    received = bytearray()
    while len(received) < size:
        chunk = connection.recv(size - len(received))
        if not chunk:
            if not received:
                return None
            raise ConnectionError("Client disconnected during a game packet")
        received.extend(chunk)
    return bytes(received)


def encode_sproto(fields):
    header = []
    body = bytearray()
    last_tag = -1

    for tag, value in sorted(fields):
        if tag < 0 or tag <= last_tag:
            raise ValueError(f"Invalid or duplicate Sproto tag: {tag}")
        skip = tag - last_tag - 1
        if skip:
            header.append(2 * (skip - 1) + 1)

        if value is None:
            header.append(1)
        elif isinstance(value, bool):
            header.append(2 if value else 0)
        elif isinstance(value, int) and 0 <= value <= 32766:
            header.append((value + 1) * 2)
        elif isinstance(value, int):
            header.append(0)
            if -(1 << 31) <= value < (1 << 31):
                body.extend(struct.pack("<I", 4))
                body.extend(struct.pack("<i", value))
            elif -(1 << 63) <= value < (1 << 63):
                body.extend(struct.pack("<I", 8))
                body.extend(struct.pack("<q", value))
            else:
                raise ValueError(f"Integer outside Sproto range: {value}")
        elif isinstance(value, str):
            encoded = value.encode("utf-8")
            header.append(0)
            body.extend(struct.pack("<I", len(encoded)))
            body.extend(encoded)
        elif isinstance(value, (bytes, bytearray)):
            encoded = bytes(value)
            header.append(0)
            body.extend(struct.pack("<I", len(encoded)))
            body.extend(encoded)
        else:
            raise TypeError(f"Unsupported Sproto value type: {type(value).__name__}")
        last_tag = tag

    return struct.pack("<H", len(header)) + b"".join(
        struct.pack("<H", value) for value in header
    ) + body


def decode_sproto_integer_fields(data):
    if len(data) < 2:
        raise ValueError("Truncated Sproto object")

    field_count = struct.unpack_from("<H", data)[0]
    header_end = 2 + field_count * 2
    if header_end > len(data):
        raise ValueError("Truncated Sproto field header")

    fields = {}
    body_offset = header_end
    tag = -1

    for index in range(field_count):
        value = struct.unpack_from("<H", data, 2 + index * 2)[0]
        if value & 1:
            tag += (value >> 1) + 1
            continue

        tag += 1
        integer = (value >> 1) - 1
        if integer >= 0:
            fields[tag] = integer
            continue

        if body_offset + 4 > len(data):
            raise ValueError("Truncated Sproto field length")
        length = struct.unpack_from("<I", data, body_offset)[0]
        body_offset += 4
        if body_offset + length > len(data):
            raise ValueError("Truncated Sproto field data")
        if length in (4, 8):
            fields[tag] = int.from_bytes(
                data[body_offset : body_offset + length], "little", signed=True
            )
        body_offset += length

    return fields


def extract_braced_block(source, opening_brace):
    depth = 0
    for index in range(opening_brace, len(source)):
        if source[index] == "{":
            depth += 1
        elif source[index] == "}":
            depth -= 1
            if depth == 0:
                return source[opening_brace + 1 : index]
    raise ValueError("Unclosed C# block")


def find_asset(assets, suffix):
    normalized_suffix = suffix.replace("\\", "/").casefold()
    return next(
        (
            contents
            for path, contents in assets.items()
            if path.replace("\\", "/").casefold().endswith(normalized_suffix)
        ),
        None,
    )


def build_response_catalog(assets):
    protocol_bytes = find_asset(
        assets, "Managed/Assembly-CSharp/Protocol.cs"
    )
    if protocol_bytes is None:
        raise RuntimeError("Decompiled Protocol.cs was not found in the asset cache")

    protocol_source = protocol_bytes.decode("utf-8-sig")
    registrations = re.findall(
        r"SetResponse<SprotoType\.(\w+)\.response>\((\d+)\)",
        protocol_source,
    )
    if not registrations:
        raise RuntimeError("No Sproto response registrations found in Protocol.cs")

    catalog = {}
    for name, tag_text in registrations:
        schema_key = next(
            (
                key
                for key in assets
                if key.replace("\\", "/").endswith(
                    f"Managed/Assembly-CSharp/SprotoType/{name}.cs"
                )
            ),
            None,
        )
        if schema_key is None:
            raise RuntimeError(f"Sproto response schema not found: {name}.response")

        source = assets[schema_key].decode("utf-8-sig")
        class_match = re.search(r"\bpublic\s+class\s+response\b", source)
        if class_match is None:
            raise RuntimeError(f"Response class missing from schema: {name}")
        class_open = source.find("{", class_match.end())
        class_body = extract_braced_block(source, class_open)

        encode_match = re.search(
            r"public\s+override\s+int\s+encode\s*\(\s*SprotoStream\s+\w+\s*\)",
            class_body,
        )
        if encode_match is None:
            raise RuntimeError(f"Response encoder missing from schema: {name}")
        encode_open = class_body.find("{", encode_match.end())
        encode_body = extract_braced_block(class_body, encode_open)
        decode_match = re.search(r"protected\s+override\s+void\s+decode\s*\(\s*\)", class_body)
        if decode_match is None:
            raise RuntimeError(f"Response decoder missing from schema: {name}")
        decode_open = class_body.find("{", decode_match.end())
        decode_body = extract_braced_block(class_body, decode_open)
        fields = {}
        for field_match in re.finditer(
            r"case\s+(\d+)\s*:\s*this\.(\w+)\s*=\s*this\.deserialize\.read_(\w+)\s*\(",
            decode_body,
        ):
            fields[int(field_match.group(1))] = {
                "name": field_match.group(2),
                "type": field_match.group(3),
            }

        catalog[int(tag_text)] = {
            "name": name,
            "fields": fields,
            "has_fields": bool(fields) or bool(re.search(r"\bthis\.serialize\.write_", encode_body)),
        }

    return catalog


def infer_login_response(assets, catalog):
    spec = catalog.get(4)
    if spec is None or spec["name"] != "login":
        raise RuntimeError("Login response schema (RPC tag 4) was not found")

    fields_by_name = {
        details["name"]: (tag, details)
        for tag, details in spec["fields"].items()
    }
    required_names = {"type", "versionCode", "dataVersionCode"}
    missing_schema_fields = required_names - fields_by_name.keys()
    if missing_schema_fields:
        raise RuntimeError(
            "Login response schema is missing fields: "
            + ", ".join(sorted(missing_schema_fields))
        )

    settings_bytes = find_asset(
        assets, "Managed/Assembly-CSharp/GameSettingData.cs"
    )
    net_manager_bytes = find_asset(
        assets, "Managed/Assembly-CSharp/NetManager.cs"
    )
    version_bytes = find_asset(assets, "assets/UpdateInfo/Version.info")
    if settings_bytes is None or net_manager_bytes is None or version_bytes is None:
        raise RuntimeError(
            "Could not locate APK login/version source files needed to infer login response"
        )

    settings_source = settings_bytes.decode("utf-8-sig")
    net_manager_source = net_manager_bytes.decode("utf-8-sig")
    version_code = re.search(
        r'public\s+static\s+string\s+GameVersion\s*=\s*"([^"]+)"',
        settings_source,
    )
    if version_code is None:
        raise RuntimeError("Could not infer GameVersion from GameSettingData.cs")

    data_version = version_bytes.decode("ascii", errors="strict").strip()
    if not data_version.isdigit():
        raise RuntimeError("APK UpdateInfo/Version.info is not a numeric version")

    success_threshold = re.search(
        r"response\.type\s*>\s*(\d+)\s*L?",
        net_manager_source,
    )
    if success_threshold is None:
        raise RuntimeError("Could not infer login success condition from NetManager.cs")

    values = {
        "type": int(success_threshold.group(1)) + 1,
        "versionCode": version_code.group(1),
        "dataVersionCode": data_version,
    }

    value_sources = {
        "type": "minimum value accepted by NetManager.LoginResponse",
        "versionCode": "GameSettingData.GameVersion",
        "dataVersionCode": "UpdateInfo/Version.info",
    }
    for name, (tag, schema) in fields_by_name.items():
        if name in values:
            continue
        if schema["type"] == "integer":
            values[name] = 0
            value_sources[name] = "C# default for an unassigned integer response field"
        elif schema["type"] == "boolean":
            values[name] = False
            value_sources[name] = "C# default for an unassigned boolean response field"

    encoded_fields = []
    for name, value in values.items():
        if name not in fields_by_name:
            continue
        tag, schema = fields_by_name[name]
        if schema["type"] == "integer" and isinstance(value, int):
            encoded_fields.append((tag, value))
        elif schema["type"] == "boolean" and isinstance(value, bool):
            encoded_fields.append((tag, value))
        elif schema["type"] == "string" and isinstance(value, str):
            encoded_fields.append((tag, value))
        else:
            raise RuntimeError(
                f"Inferred login field {name} does not match its Sproto schema type"
            )

    return encode_sproto(encoded_fields), values, value_sources


def read_game_assets(client_address):
    global ASSET_CACHE, ASSET_CACHE_LOADED, RESPONSE_CATALOG, LOGIN_RESPONSE_FIELDS

    if ASSET_CACHE_LOADED:
        return

    with ASSET_CACHE_LOCK:
        if ASSET_CACHE_LOADED:
            return

        if not os.path.isdir(DECOMPILED_ROOT):
            raise FileNotFoundError(f"Decompiled directory not found: {DECOMPILED_ROOT}")

        asset_paths = []
        for root, _, filenames in os.walk(DECOMPILED_ROOT):
            asset_paths.extend(os.path.join(root, filename) for filename in filenames)
        asset_paths.sort()

        if not asset_paths:
            raise RuntimeError(f"No game assets found in {DECOMPILED_ROOT}")

        total_bytes = sum(os.path.getsize(path) for path in asset_paths)
        print(f"[+] Client connected: {client_address}")
        print("Please wait reading game assets 0/100%", end="", flush=True)

        assets = {}
        bytes_read = 0
        last_percent = 0

        try:
            for path in asset_paths:
                relative_path = os.path.relpath(path, DECOMPILED_ROOT)
                contents = bytearray()

                with open(path, "rb") as asset_file:
                    while True:
                        chunk = asset_file.read(1024 * 1024)
                        if not chunk:
                            break

                        contents.extend(chunk)
                        bytes_read += len(chunk)

                        if total_bytes:
                            percent = min(100, bytes_read * 100 // total_bytes)
                            if percent > last_percent:
                                print(
                                    f"\rPlease wait reading game assets {percent}/100%",
                                    end="",
                                    flush=True,
                                )
                                last_percent = percent

                assets[relative_path] = bytes(contents)

            if bytes_read != total_bytes:
                raise RuntimeError(
                    f"Asset files changed while reading: expected {total_bytes} bytes, "
                    f"read {bytes_read}"
                )
        except Exception:
            print()
            raise

        ASSET_CACHE = assets
        RESPONSE_CATALOG = build_response_catalog(assets)
        LOGIN_RESPONSE_FIELDS = infer_login_response(assets, RESPONSE_CATALOG)
        ASSET_CACHE_LOADED = True
        print("\rPlease wait reading game assets 100/100%")
        print(
            f"[ASSETS] Cached {len(assets)} files ({bytes_read:,} bytes) in RAM; "
            f"indexed {len(RESPONSE_CATALOG)} RPC response schemas."
        )
        print(
            "[APK] Inferred login.response from Decompiled: "
            + ", ".join(
                f"{key}={value} [{LOGIN_RESPONSE_FIELDS[2][key]}]"
                for key, value in LOGIN_RESPONSE_FIELDS[1].items()
            )
        )


def handle_client(connection, address):
    try:
        read_game_assets(address)
        print(f"[RPC] Listening for game requests from {address}")
        while True:
            frame_header = recv_exact(connection, 2)
            if not frame_header:
                break

            frame_size = struct.unpack(">H", frame_header)[0]
            if frame_size == 0 or frame_size > MAX_FRAME_SIZE:
                raise ValueError(f"Invalid game packet size: {frame_size}")

            packed_frame = recv_exact(connection, frame_size)
            if packed_frame is None:
                raise ConnectionError("Client disconnected before sending the game packet")

            packet = sproto_unpack(packed_frame)
            package = decode_sproto_integer_fields(packet)
            rpc_tag = package.get(0)
            session = package.get(1)

            if rpc_tag is None:
                print(f"[RPC] Received response packet session={session} from {address}")
                continue

            spec = RESPONSE_CATALOG.get(rpc_tag)
            if session is None:
                print(f"[RPC] Received one-way request tag={rpc_tag} from {address}")
                continue
            if spec is None:
                print(
                    f"[RPC] APK has no response schema for request tag={rpc_tag}, "
                    f"session={session}; no reply sent."
                )
                continue
            if rpc_tag == 4:
                response_data = LOGIN_RESPONSE_FIELDS[0]
                response_package = encode_sproto([(1, session)])
                response_frame = sproto_pack(response_package + response_data)
                if len(response_frame) > MAX_FRAME_SIZE:
                    raise ValueError("Generated login response exceeds the game frame limit")
                connection.sendall(struct.pack(">H", len(response_frame)) + response_frame)
                print(f"[RPC] Replied to {spec['name']} tag={rpc_tag} session={session}")
                continue
            if spec["has_fields"]:
                print(
                    f"[RPC] APK response schema for {spec['name']} tag={rpc_tag} "
                    "was found, but no response values could be inferred from static "
                    "APK data. No reply sent."
                )
                continue

            response_package = encode_sproto([(1, session)])
            response_frame = sproto_pack(response_package + encode_sproto([]))
            if len(response_frame) > MAX_FRAME_SIZE:
                raise ValueError("Generated response exceeds the game frame limit")
            connection.sendall(struct.pack(">H", len(response_frame)) + response_frame)
            print(f"[RPC] Replied to {spec['name']} tag={rpc_tag} session={session}")
    except (ConnectionError, OSError) as exc:
        print(f"[!] Connection ended for {address}: {exc}")
    except Exception as exc:
        print(f"[!] Failed handling game client {address}: {exc}")
    finally:
        connection.close()
        print(f"[-] Client disconnected: {address}")


def start_server():
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server:
        server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        server.bind(("0.0.0.0", PORT))
        server.listen()
        print(f"ASSET READER READY ON PORT {PORT}")

        while True:
            try:
                connection, address = server.accept()
                threading.Thread(
                    target=handle_client,
                    args=(connection, address),
                    daemon=True,
                ).start()
            except OSError as exc:
                print(f"[!] Accept failed: {exc}")


if __name__ == "__main__":
    start_server()
