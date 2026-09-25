import copy
import os
import re
import socket
import struct
import threading
import time


PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
DECOMPILED_ROOT = os.path.join(SCRIPT_DIR, "Decompiled")
MAX_FRAME_SIZE = 65535
MAX_COLLECTION_ITEMS = 100000
PACKAGE_SCHEMA = {
    0: {"name": "type", "type": "integer"},
    1: {"name": "session", "type": "integer"},
}

ASSET_CACHE = {}
ASSET_CACHE_LOADED = False
ASSET_CACHE_LOCK = threading.Lock()
RESPONSE_CATALOG = {}
REQUEST_CATALOG = {}
SCHEMA_CATALOG = {}
CLIENT_REQUEST_HANDLERS = {}
RESPONSE_FIELD_USAGE = {}
RESPONSE_PRODUCERS = {}
GAME_STATE = {}
GAME_STATE_LOCK = threading.Lock()
GAME_METADATA = {}


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
            header.append(4 if value else 2)
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


def decode_sproto_object(data, schema=None):
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
            descriptor = (schema or {}).get(tag, {})
            field_type = descriptor.get("type")
            if field_type == "boolean":
                fields[tag] = bool(integer)
            elif field_type == "integer":
                fields[tag] = integer
            else:
                fields[tag] = integer
            continue

        if body_offset + 4 > len(data):
            raise ValueError("Truncated Sproto field length")
        length = struct.unpack_from("<I", data, body_offset)[0]
        body_offset += 4
        if body_offset + length > len(data):
            raise ValueError("Truncated Sproto field data")
        payload = data[body_offset : body_offset + length]
        descriptor = (schema or {}).get(tag, {})
        fields[tag] = decode_sproto_value(payload, descriptor)
        body_offset += length

    return fields


def decode_sproto_value(payload, descriptor):
    field_type = descriptor.get("type")
    if field_type == "integer":
        if len(payload) not in (4, 8):
            raise ValueError(f"Invalid Sproto integer size: {len(payload)}")
        return int.from_bytes(payload, "little", signed=True)
    if field_type == "boolean":
        if len(payload) not in (4, 8):
            raise ValueError(f"Invalid Sproto boolean size: {len(payload)}")
        return bool(int.from_bytes(payload, "little", signed=True))
    if field_type == "string":
        return payload.decode("utf-8")
    if field_type == "object":
        return decode_sproto_object(payload, descriptor.get("object_schema"))
    if field_type in ("integer_list", "boolean_list", "string_list", "object_list", "map"):
        if len(payload) < 4:
            raise ValueError("Truncated Sproto array length")
        array_size = struct.unpack_from("<I", payload)[0]
        array_end = 4 + array_size
        if array_end > len(payload):
            raise ValueError("Sproto array length exceeds field payload")
        return decode_sproto_array(payload[4:array_end], descriptor)
    return payload


def decode_sproto_array(data, descriptor):
    field_type = descriptor["type"]
    if field_type == "integer_list":
        if not data:
            return []
        width = data[0]
        if width not in (4, 8) or (len(data) - 1) % width:
            raise ValueError("Invalid Sproto integer array")
        return [
            int.from_bytes(data[offset : offset + width], "little", signed=True)
            for offset in range(1, len(data), width)
        ]
    if field_type == "boolean_list":
        if len(data) > MAX_COLLECTION_ITEMS:
            raise ValueError("Sproto boolean array exceeds item limit")
        return [bool(value) for value in data]

    values = []
    offset = 0
    while offset < len(data):
        if len(values) >= MAX_COLLECTION_ITEMS or offset + 4 > len(data):
            raise ValueError("Invalid or oversized Sproto array")
        item_size = struct.unpack_from("<I", data, offset)[0]
        offset += 4
        if offset + item_size > len(data):
            raise ValueError("Truncated Sproto array item")
        item = data[offset : offset + item_size]
        offset += item_size
        if field_type == "string_list":
            values.append(item.decode("utf-8"))
        else:
            values.append(decode_sproto_object(item, descriptor.get("object_schema")))

    if field_type == "map":
        key_tag = descriptor.get("key_tag")
        if key_tag is not None:
            return {
                item.get(key_tag): item
                for item in values
                if key_tag in item
            }
    return values


def decode_sproto_integer_fields(data):
    return decode_sproto_object(data)


def encode_sproto_object(fields, schema=None):
    schema = schema or {}
    encoded_fields = []
    for tag, value in fields.items():
        if value is None:
            continue
        descriptor = schema.get(tag, {})
        encoded_fields.append((tag, encode_sproto_value(value, descriptor)))
    return encode_sproto(encoded_fields)


def encode_sproto_value(value, descriptor):
    field_type = descriptor.get("type")
    if field_type == "integer":
        if not isinstance(value, int) or isinstance(value, bool):
            raise TypeError("Sproto integer field requires an integer")
        return value
    if field_type == "boolean":
        if not isinstance(value, bool):
            raise TypeError("Sproto boolean field requires a boolean")
        return value
    if field_type == "string":
        if not isinstance(value, str):
            raise TypeError("Sproto string field requires a string")
        return value
    if field_type == "object":
        return encode_sproto_object(value, descriptor.get("object_schema"))
    if field_type in ("integer_list", "boolean_list"):
        if not isinstance(value, list):
            raise TypeError(f"Sproto {field_type} field requires a list")
        if field_type == "integer_list":
            if value:
                widths = [
                    8 if item < -(1 << 31) or item >= (1 << 31) else 4
                    for item in value
                ]
                width = max(widths)
                raw = bytes([width]) + b"".join(
                    int(item).to_bytes(width, "little", signed=True)
                    for item in value
                )
            else:
                raw = b""
        else:
            raw = bytes(1 if item else 0 for item in value)
        return struct.pack("<I", len(raw)) + raw
    if field_type in ("string_list", "object_list", "map"):
        items = list(value.values()) if isinstance(value, dict) else value
        raw = bytearray()
        for item in items:
            if field_type == "string_list":
                encoded = item.encode("utf-8")
            else:
                encoded = encode_sproto_object(item, descriptor.get("object_schema"))
            raw.extend(struct.pack("<I", len(encoded)))
            raw.extend(encoded)
        return struct.pack("<I", len(raw)) + raw
    if isinstance(value, (bytes, bytearray)):
        return bytes(value)
    return value


def sproto_body_offset(data):
    if len(data) < 2:
        raise ValueError("Truncated Sproto package")
    field_count = struct.unpack_from("<H", data)[0]
    offset = 2 + field_count * 2
    if offset > len(data):
        raise ValueError("Truncated Sproto package header")

    for index in range(field_count):
        header_word = struct.unpack_from("<H", data, 2 + index * 2)[0]
        if not (header_word & 1) and (header_word >> 1) - 1 < 0:
            if offset + 4 > len(data):
                raise ValueError("Truncated Sproto package field length")
            length = struct.unpack_from("<I", data, offset)[0]
            offset += 4 + length
            if offset > len(data):
                raise ValueError("Truncated Sproto package field")
    return offset


def encode_game_frame(package_fields, body_fields, body_schema):
    package = encode_sproto(package_fields)
    body = encode_sproto_object(body_fields, body_schema)
    packed = sproto_pack(package + body)
    if not packed or len(packed) > MAX_FRAME_SIZE:
        raise ValueError(f"Invalid outgoing game frame size: {len(packed)}")
    return struct.pack(">H", len(packed)) + packed


def send_rpc_response(connection, state, tag, session, body_fields):
    spec = RESPONSE_CATALOG.get(tag)
    if spec is None:
        raise KeyError(f"No APK response schema registered for RPC tag {tag}")
    frame = encode_game_frame([(1, session)], body_fields, spec["fields"])
    with state.send_lock:
        with state.lock:
            pending_tag = state.pending_client_sessions.get(session)
            if pending_tag is None:
                raise KeyError(
                    f"Session {session} is not pending on this client connection"
                )
            if pending_tag != tag:
                raise ValueError(
                    f"Session {session} is for RPC tag {pending_tag}, not {tag}"
                )
        connection.sendall(frame)
        with state.lock:
            state.pending_client_sessions.pop(session, None)


def send_server_push(connection, state, tag, body_fields):
    spec = REQUEST_CATALOG.get(tag)
    if spec is None:
        raise KeyError(f"No APK request schema registered for server push tag {tag}")
    frame = encode_game_frame([(0, tag)], body_fields, spec["fields"])
    with state.send_lock:
        connection.sendall(frame)


def send_server_rpc(connection, state, tag, body_fields):
    spec = REQUEST_CATALOG.get(tag)
    if spec is None or tag not in RESPONSE_CATALOG:
        raise KeyError(f"RPC tag {tag} is not registered for request and response")
    with state.lock:
        if len(state.pending_server_rpcs) >= 4096:
            raise RuntimeError("Too many outstanding server-to-client RPC sessions")
        session = state.next_server_session
        state.next_server_session += 1
        state.pending_server_rpcs[session] = {
            "tag": tag,
            "schema": RESPONSE_CATALOG[tag]["fields"],
        }
    frame = encode_game_frame([(0, tag), (1, session)], body_fields, spec["fields"])
    try:
        with state.send_lock:
            connection.sendall(frame)
    except Exception:
        with state.lock:
            state.pending_server_rpcs.pop(session, None)
        raise
    return session


def generate_auto_response(tag, request_fields, game_state=None):
    spec = RESPONSE_CATALOG.get(tag)
    if spec is None:
        return None, "APK has no response schema for this tag"

    if game_state is not None:
        authoritative_responses = game_state.get("authoritative_responses", {})
        if tag in authoritative_responses:
            response_fields = authoritative_responses[tag]
            if not isinstance(response_fields, dict):
                return None, "GAME_STATE authoritative response must be a field map"
            if any(field_tag not in spec["fields"] for field_tag in response_fields):
                return None, "GAME_STATE authoritative response contains unknown fields"
            try:
                encode_sproto_object(response_fields, spec["fields"])
            except (TypeError, ValueError, OverflowError) as exc:
                return None, f"GAME_STATE response does not match APK schema: {exc}"
            return response_fields, "authoritative per-client GAME_STATE"

    if spec["name"] == "login":
        request_spec = REQUEST_CATALOG.get(tag)
        request_logintype_tag = next(
            (
                field_tag
                for field_tag, field in (request_spec or {}).get("fields", {}).items()
                if field["name"] == "logintype" and field["type"] == "integer"
            ),
            None,
        )
        if (
            request_logintype_tag is None
            or request_fields.get(request_logintype_tag) not in (1, 2)
        ):
            return None, "login request has no supported logintype"

        response_tags = {
            field["name"]: field_tag for field_tag, field in spec["fields"].items()
        }
        required_metadata = ("versionCode", "dataVersionCode")
        missing = [key for key in required_metadata if not GAME_METADATA.get(key)]
        if missing:
            return None, f"Decompiled metadata is missing {', '.join(missing)}"

        configured_server_level = os.environ.get("SERVER_LEVEL")
        if configured_server_level is None:
            return None, "SERVER_LEVEL is required; Decompiled has no account server level"
        try:
            server_level = int(configured_server_level)
        except ValueError:
            return None, "SERVER_LEVEL must be an integer"
        if server_level < 0:
            return None, "SERVER_LEVEL must be non-negative"
        if not all(key in response_tags for key in (*required_metadata, "type", "serverLevel")):
            return None, "APK login response schema is missing a required field"

        # NetManager.LoginResponse treats type > 1 as an accepted normal login.
        return {
            response_tags["type"]: 2,
            response_tags["versionCode"]: GAME_METADATA["versionCode"],
            response_tags["dataVersionCode"]: GAME_METADATA["dataVersionCode"],
            response_tags["serverLevel"]: server_level,
        }, (
            "APK login acceptance branch; version metadata from Decompiled; "
            f"SERVER_LEVEL={server_level} configuration"
        )

    if spec["name"] == "heart_beat":
        request_spec = REQUEST_CATALOG.get(tag)
        if request_spec is None:
            return None, "APK has no heartbeat request schema"
        request_time_tag = next(
            (
                field_tag
                for field_tag, field in request_spec["fields"].items()
                if field["name"] == "time" and field["type"] == "integer"
            ),
            None,
        )
        response_time_tag = next(
            (
                field_tag
                for field_tag, field in spec["fields"].items()
                if field["name"] == "time" and field["type"] == "integer"
            ),
            None,
        )
        if request_time_tag is None or request_fields.get(request_time_tag) is None:
            return None, "heartbeat request is missing its client time field"
        if response_time_tag is None:
            return None, "APK heartbeat response schema has no integer time field"

        response_fields = {
            response_time_tag: 621355968000000000 + time.time_ns() // 100
        }
        server_time_tag = next(
            (
                field_tag
                for field_tag, field in spec["fields"].items()
                if field["name"] == "serverTime" and field["type"] == "integer"
            ),
            None,
        )
        if server_time_tag is not None:
            response_fields[server_time_tag] = int(time.time())
        return response_fields, "host UTC clock and Unix-epoch seconds"

    request_spec = REQUEST_CATALOG.get(tag)
    request_schema = request_spec["fields"] if request_spec else {}
    try:
        response_fields, missing_fields = generate_request_derived_response(
            spec["fields"],
            request_schema,
            request_fields,
            path=spec["name"],
            field_budget=[0],
        )
        if missing_fields:
            return None, (
                "Decompiled contains no authoritative producer for response fields: "
                + ", ".join(missing_fields[:12])
            )
        encode_sproto_object(response_fields, spec["fields"])
    except (TypeError, ValueError, OverflowError) as exc:
        return None, f"request-derived response could not be safely encoded: {exc}"
    producer_note = ""
    producers = RESPONSE_PRODUCERS.get(tag, ())
    if producers and not any(
        producer.get("server_reply_usable") for producer in producers
    ):
        producer_note = (
            "; scanned APK producer is a client-side handler for server-originated "
            "RPC, not an authoritative game-server response"
        )
    return response_fields, "values copied from matching request fields" + producer_note


def extract_game_metadata(assets):
    settings = find_asset(
        assets, "Managed/Assembly-CSharp/GameSettingData.cs"
    )
    if settings is None:
        raise RuntimeError("Decompiled GameSettingData.cs was not found")
    settings_source = settings.decode("utf-8-sig")
    version_match = re.search(
        r'\bGameVersion\s*=\s*"([^"]+)"', settings_source
    )
    if version_match is None:
        raise RuntimeError("GameSettingData.GameVersion was not found")

    version_data = find_asset(assets, "assets/UpdateInfo/Version.info")
    if version_data is None:
        raise RuntimeError("Decompiled UpdateInfo/Version.info was not found")
    data_version = version_data.decode("utf-8-sig").strip()
    if not data_version.isdecimal():
        raise RuntimeError("UpdateInfo/Version.info is not a decimal version")
    return {
        "versionCode": version_match.group(1),
        "dataVersionCode": data_version,
    }


def generate_request_derived_response(
    response_schema,
    request_schema=None,
    request_fields=None,
    path="response",
    field_budget=None,
):
    if path.count(".") > 24:
        raise ValueError("nested response schema exceeds depth limit")
    if field_budget is None:
        field_budget = [0]
    request_schema = request_schema or {}
    request_fields = request_fields or {}
    request_fields_by_name = {
        descriptor["name"]: (tag, descriptor)
        for tag, descriptor in request_schema.items()
    }
    generated = {}
    missing_fields = []

    for response_tag, descriptor in response_schema.items():
        field_budget[0] += 1
        if field_budget[0] > 4096:
            raise ValueError("response schema exceeds generated field limit")

        request_match = request_fields_by_name.get(descriptor["name"])
        request_value = None
        request_descriptor = None
        if request_match:
            request_tag, request_descriptor = request_match
            request_value = request_fields.get(request_tag)
            if request_descriptor.get("type") != descriptor.get("type"):
                request_value = None
                request_descriptor = None

        field_type = descriptor.get("type")
        value = None
        field_path = f"{path}.{descriptor.get('name', response_tag)}"
        if field_type == "integer":
            if isinstance(request_value, int) and not isinstance(request_value, bool):
                value = request_value
        elif field_type == "boolean":
            if isinstance(request_value, bool):
                value = request_value
        elif field_type == "string":
            if isinstance(request_value, str):
                value = request_value
        elif field_type == "bytes":
            if isinstance(request_value, (bytes, bytearray)):
                value = bytes(request_value)
        elif field_type == "object":
            nested, missing = generate_request_derived_response(
                descriptor.get("object_schema", {}),
                (request_descriptor or {}).get("object_schema", {}),
                request_value if isinstance(request_value, dict) else {},
                field_path,
                field_budget,
            )
            if not missing:
                value = nested
            else:
                missing_fields.extend(missing)
        elif field_type in ("integer_list", "boolean_list", "string_list"):
            if isinstance(request_value, list):
                value = request_value
        elif field_type == "object_list":
            if isinstance(request_value, list):
                value = []
                request_object_schema = (request_descriptor or {}).get("object_schema", {})
                for index, item in enumerate(request_value):
                    if not isinstance(item, dict):
                        missing_fields.append(f"{field_path}[{index}]")
                        continue
                    generated_item, missing = generate_request_derived_response(
                        descriptor.get("object_schema", {}),
                        request_object_schema,
                        item,
                        f"{field_path}[{index}]",
                        field_budget,
                    )
                    if missing:
                        missing_fields.extend(missing)
                    else:
                        value.append(generated_item)
        elif field_type == "map":
            if isinstance(request_value, dict):
                value = {}
                request_object_schema = (request_descriptor or {}).get(
                    "object_schema", {}
                )
                response_key_tag = descriptor.get("key_tag")
                for map_key, item in request_value.items():
                    if not isinstance(item, dict):
                        missing_fields.append(f"{field_path}[{map_key!r}]")
                        continue
                    generated_item, missing = generate_request_derived_response(
                        descriptor.get("object_schema", {}),
                        request_object_schema,
                        item,
                        f"{field_path}[{map_key!r}]",
                        field_budget,
                    )
                    if missing:
                        missing_fields.extend(missing)
                        continue
                    if response_key_tag is not None:
                        generated_key = generated_item.get(response_key_tag, map_key)
                    else:
                        generated_key = map_key
                    value[generated_key] = generated_item
        else:
            raise TypeError(
                f"unsupported response field type {field_type!r} for {field_path}"
            )

        if value is not None:
            generated[response_tag] = value
        elif not any(
            missing == field_path
            or missing.startswith(field_path + ".")
            or missing.startswith(field_path + "[")
            for missing in missing_fields
        ):
            missing_fields.append(field_path)
    return generated, missing_fields


class ClientConnectionState:
    def __init__(self):
        self.state_id = id(self)
        self.next_server_session = 1
        self.pending_server_rpcs = {}
        self.pending_client_sessions = {}
        self.observed_requests = {}
        self.game_state = {
            "identity": None,
            "observed_requests": {},
            "authoritative_responses": {},
        }
        self.lock = threading.Lock()
        self.send_lock = threading.Lock()

    def record_request(self, tag, request_fields, request_schema):
        named_fields = {
            request_schema.get(field_tag, {}).get("name", str(field_tag)): value
            for field_tag, value in request_fields.items()
        }
        with self.lock:
            self.observed_requests.pop(tag, None)
            self.observed_requests[tag] = named_fields
            self.game_state["observed_requests"].pop(tag, None)
            self.game_state["observed_requests"][tag] = named_fields
            if REQUEST_CATALOG.get(tag, {}).get("name") == "login":
                identity_tag = next(
                    (
                        field_tag
                        for field_tag, field in request_schema.items()
                        if field["name"] == "id" and field["type"] == "string"
                    ),
                    None,
                )
                identity = request_fields.get(identity_tag)
                if isinstance(identity, str):
                    self.game_state["identity"] = identity
            while len(self.observed_requests) > 64:
                oldest_tag = next(iter(self.observed_requests))
                self.observed_requests.pop(oldest_tag)
                self.game_state["observed_requests"].pop(oldest_tag, None)

    def set_authoritative_response(self, tag, response_fields):
        spec = RESPONSE_CATALOG.get(tag)
        if spec is None:
            raise KeyError(f"No APK response schema registered for RPC tag {tag}")
        if not isinstance(response_fields, dict):
            raise TypeError("Authoritative response must be a field map")
        if any(field_tag not in spec["fields"] for field_tag in response_fields):
            raise ValueError("Authoritative response contains unknown schema fields")
        encode_sproto_object(response_fields, spec["fields"])
        with self.lock:
            self.game_state["authoritative_responses"][tag] = copy.deepcopy(
                response_fields
            )

    def clear(self):
        with self.lock:
            self.pending_server_rpcs.clear()
            self.pending_client_sessions.clear()
            self.observed_requests.clear()
            self.game_state["identity"] = None
            self.game_state["observed_requests"].clear()
            self.game_state["authoritative_responses"].clear()


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


def csharp_type_descriptor(type_name, _write_method, decode_body):
    normalized = re.sub(r"\s+", "", type_name)
    descriptor = {"type": "unknown"}

    if normalized in ("long", "int", "uint", "ulong", "short", "ushort", "byte"):
        descriptor["type"] = "integer"
    elif normalized == "bool":
        descriptor["type"] = "boolean"
    elif normalized == "string":
        descriptor["type"] = "string"
    elif normalized in ("byte[]", "System.Byte[]"):
        descriptor["type"] = "bytes"
    elif normalized.startswith("List<") and normalized.endswith(">"):
        element_type = normalized[5:-1]
        if element_type in ("long", "int", "uint", "ulong", "short", "ushort", "byte"):
            descriptor["type"] = "integer_list"
        elif element_type == "bool":
            descriptor["type"] = "boolean_list"
        elif element_type == "string":
            descriptor["type"] = "string_list"
        else:
            descriptor.update(type="object_list", object_name=element_type)
    elif normalized.startswith("Dictionary<") and normalized.endswith(">"):
        key_and_value = normalized[len("Dictionary<"):-1]
        key_type, separator, value_type = key_and_value.partition(",")
        if not separator:
            raise ValueError(f"Malformed Sproto map type: {type_name}")
        descriptor.update(
            type="map",
            key_type=key_type,
            object_name=value_type,
        )
    else:
        descriptor.update(type="object", object_name=normalized)

    if descriptor["type"] in ("object", "object_list", "map"):
        object_name = descriptor["object_name"]
        map_key = re.search(
            rf"read_map\s*<[^,>]+,\s*{re.escape(object_name)}\s*>\s*"
            rf"\(\s*\([^)]*\)\s*=>\s*\w+\.(\w+)\s*\)",
            decode_body,
        )
        if map_key:
            descriptor["key_name"] = map_key.group(1)
    return descriptor


def parse_sproto_schemas(assets):
    schemas = {}
    suffix = "Managed/Assembly-CSharp/SprotoType/"
    for path, contents in assets.items():
        normalized_path = path.replace("\\", "/")
        if suffix not in normalized_path or not normalized_path.endswith(".cs"):
            continue
        name = normalized_path.rsplit("/", 1)[-1][:-3]
        source = contents.decode("utf-8-sig")
        candidates = []
        for match in re.finditer(r"\bpublic\s+class\s+(request|response)\b", source):
            candidates.append((name, match.group(1), match))
        for match in re.finditer(
            r"\bpublic\s+class\s+(\w+)\s*:\s*SprotoTypeBase\b", source
        ):
            object_name = match.group(1)
            if object_name not in ("request", "response"):
                candidates.append((object_name, "object", match))

        for schema_name, kind, match in candidates:
            opening = source.find("{", match.end())
            class_body = extract_braced_block(source, opening)
            encode_match = re.search(
                r"public\s+override\s+int\s+encode\s*\(\s*SprotoStream\s+\w+\s*\)",
                class_body,
            )
            decode_match = re.search(
                r"protected\s+override\s+void\s+decode\s*\(\s*\)",
                class_body,
            )
            if encode_match is None or decode_match is None:
                continue
            encode_body = extract_braced_block(
                class_body, class_body.find("{", encode_match.end())
            )
            decode_body = extract_braced_block(
                class_body, class_body.find("{", decode_match.end())
            )
            fields = {}
            for writer in re.finditer(
                r"\bwrite_(\w+)\s*(?:<([^;()]*?)>)?\s*"
                r"\(\s*this\.(\w+)\s*,\s*(\d+)\s*\)",
                encode_body,
            ):
                write_method, generic_types, field_name, tag_text = writer.groups()
                property_match = re.search(
                    rf"\bpublic\s+([\w<>,\[\]\s]+?)\s+{re.escape(field_name)}\s*(?:\{{|;)",
                    class_body,
                )
                if property_match is None:
                    raise RuntimeError(
                        f"Could not infer type for {schema_name}.{kind}.{field_name}"
                    )
                descriptor = csharp_type_descriptor(
                    property_match.group(1).strip(),
                    write_method,
                    decode_body,
                )
                fields[int(tag_text)] = {
                    "name": field_name,
                    **descriptor,
                }
            schemas[(schema_name, kind)] = fields

    for fields in schemas.values():
        for descriptor in fields.values():
            object_name = descriptor.get("object_name")
            if object_name:
                nested = schemas.get((object_name, "object"))
                if nested is None:
                    nested = schemas.get((object_name, "request"))
                if nested is None:
                    nested = schemas.get((object_name, "response"))
                if nested is not None:
                    descriptor["object_schema"] = nested
                    key_name = descriptor.get("key_name")
                    if key_name:
                        descriptor["key_tag"] = next(
                            (
                                tag
                                for tag, nested_field in nested.items()
                                if nested_field["name"] == key_name
                            ),
                            None,
                        )
    return schemas


def build_rpc_catalogs(assets):
    protocol_bytes = find_asset(
        assets, "Managed/Assembly-CSharp/Protocol.cs"
    )
    if protocol_bytes is None:
        raise RuntimeError("Decompiled Protocol.cs was not found in the asset cache")

    protocol_source = protocol_bytes.decode("utf-8-sig")
    protocols = {
        name: int(tag)
        for name, tag in re.findall(
            r"SetProtocol<Protocol\.(\w+)>\((\d+)\)", protocol_source
        )
    }
    requests = {
        int(tag): {
            "name": name,
            "fields": {},
            "has_fields": False,
        }
        for name, tag in re.findall(
            r"SetRequest<SprotoType\.(\w+)\.request>\((\d+)\)",
            protocol_source,
        )
    }
    sent_protocols = set()
    for path, contents in assets.items():
        if not path.replace("\\", "/").endswith(".cs"):
            continue
        source = contents.decode("utf-8-sig", errors="replace")
        sent_protocols.update(
            re.findall(r"\bSend(?:Internal)?<Protocol\.(\w+)>", source)
        )
    for protocol_name in sent_protocols:
        tag = protocols.get(protocol_name)
        if tag is not None and tag not in requests:
            requests[tag] = {
                "name": protocol_name,
                "fields": {},
                "has_fields": False,
                "empty_request": True,
            }

    responses = {
        int(tag): {
            "name": name,
            "fields": {},
            "has_fields": False,
        }
        for name, tag in re.findall(
            r"SetResponse<SprotoType\.(\w+)\.response>\((\d+)\)",
            protocol_source,
        )
    }
    if not requests and not responses:
        raise RuntimeError("No Sproto RPC registrations found in Protocol.cs")

    schemas = parse_sproto_schemas(assets)
    for catalog, kind in ((requests, "request"), (responses, "response")):
        for tag, entry in catalog.items():
            name = entry["name"]
            fields = schemas.get((name, kind))
            if fields is None and kind == "request" and entry.get("empty_request"):
                entry["protocol_name"] = name
                continue
            if fields is None:
                raise RuntimeError(f"Sproto {kind} schema not found: {name}.{kind}")
            catalog[tag]["fields"] = fields
            catalog[tag]["has_fields"] = bool(fields)
            catalog[tag]["protocol_name"] = next(
                (
                    protocol
                    for protocol, protocol_tag in protocols.items()
                    if protocol_tag == tag
                ),
                name,
            )

    handlers_bytes = find_asset(
        assets, "Managed/Assembly-CSharp/NetReceiver.cs"
    )
    client_handlers = {}
    if handlers_bytes is not None:
        handler_source = handlers_bytes.decode("utf-8-sig")
        for protocol_name, handler in re.findall(
            r"AddHandler<Protocol\.(\w+)>\s*\(\s*new\s+RpcReqHandler\(([\w.]+)\)",
            handler_source,
        ):
            tag = protocols.get(protocol_name)
            if tag is not None:
                client_handlers[tag] = handler

    return requests, responses, schemas, client_handlers


def scan_response_producers(assets, responses, client_handlers):
    producers = {}
    for tag, response_spec in responses.items():
        handler_path = client_handlers.get(tag)
        if not handler_path:
            continue
        handler_class, separator, method_name = handler_path.rpartition(".")
        if not separator:
            continue

        source = find_asset(
            assets,
            f"Managed/Assembly-CSharp/{handler_class}.cs",
        )
        if source is None:
            continue
        source = source.decode("utf-8-sig", errors="replace")
        method_match = re.search(
            rf"\b{re.escape(method_name)}\s*\([^;{{}}]*\)\s*\{{",
            source,
        )
        if method_match is None:
            continue
        opening_brace = source.find("{", method_match.start(), method_match.end())
        method_body = extract_braced_block(source, opening_brace)
        method_source = source[opening_brace + 1 : opening_brace + 1 + len(method_body)]

        response_name = response_spec["name"]
        constructor = re.search(
            rf"\b(?:[\w.]+\.)?{re.escape(response_name)}\.response\s+"
            rf"(?P<variable>[A-Za-z_]\w*)\s*=\s*new\s+"
            rf"(?:[\w.]+\.)?{re.escape(response_name)}\.response\s*\(",
            method_source,
        )
        if constructor is None:
            continue

        variable = constructor.group("variable")
        fields_by_name = {
            field["name"]: field_tag
            for field_tag, field in response_spec["fields"].items()
        }
        field_evidence = {}
        assignment_pattern = re.compile(
            rf"\b{re.escape(variable)}\.(?P<field>[A-Za-z_]\w*)\s*"
            r"(?P<operator>\+=|=)\s*(?P<expression>[^;]+);"
        )
        for assignment in assignment_pattern.finditer(method_source):
            field_name = assignment.group("field")
            field_tag = fields_by_name.get(field_name)
            if field_tag is None:
                continue
            expression = assignment.group("expression").strip()
            request_match = re.fullmatch(
                r"request\.(?P<field>[A-Za-z_]\w*)", expression
            )
            if request_match:
                source_kind = "request_field"
                source_field = request_match.group("field")
            elif re.fullmatch(r"-?\d+[lL]?", expression):
                source_kind = "literal_integer"
                source_field = None
            elif expression in ("true", "false"):
                source_kind = "literal_boolean"
                source_field = None
            elif expression.startswith('"') and expression.endswith('"'):
                source_kind = "literal_string"
                source_field = None
            elif re.search(r"\b(?:PlayerData|GameManager|DataManager|DB|player|mainPlayer)\b", expression):
                source_kind = "client_state_or_computation"
                source_field = None
            else:
                source_kind = "computed_or_unknown"
                source_field = None
            line = source.count(
                "\n", 0, opening_brace + 1 + assignment.start()
            ) + 1
            field_evidence.setdefault(field_tag, []).append(
                {
                    "field": field_name,
                    "operation": assignment.group("operator"),
                    "expression": expression,
                    "source_kind": source_kind,
                    "source_field": source_field,
                    "line": line,
                }
            )

        for mutation in re.finditer(
            rf"\b{re.escape(variable)}\.(?P<field>[A-Za-z_]\w*)\."
            r"(?P<operation>Add|AddRange|Clear)\s*\(",
            method_source,
        ):
            field_name = mutation.group("field")
            field_tag = fields_by_name.get(field_name)
            if field_tag is None:
                continue
            line = source.count(
                "\n", 0, opening_brace + 1 + mutation.start()
            ) + 1
            field_evidence.setdefault(field_tag, []).append(
                {
                    "field": field_name,
                    "operation": mutation.group("operation"),
                    "expression": None,
                    "source_kind": "collection_mutation",
                    "source_field": None,
                    "line": line,
                }
            )

        if field_evidence:
            producers[tag] = [
                {
                    "handler": handler_path,
                    "file": f"Managed/Assembly-CSharp/{handler_class}.cs",
                    "direction": "client_handler_reply_to_server_rpc",
                    "server_reply_usable": False,
                    "fields": field_evidence,
                }
            ]
    return producers


def scan_response_field_usage(assets, responses):
    usages = {tag: {} for tag in responses}
    method_pattern = re.compile(
        r"(?m)^[ \t]*(?:(?:public|private|protected|internal|static|virtual|"
        r"override|sealed|async|new|partial|extern)\s+)+"
        r"[\w.<>\[\],?]+\s+\w+\s*\([^;{}]*\)\s*\{"
    )
    response_types = {
        entry["name"]: (
            tag,
            entry["fields"],
            {
                field["name"]: field_tag
                for field_tag, field in entry["fields"].items()
            },
        )
        for tag, entry in responses.items()
    }

    for path, contents in assets.items():
        normalized_path = path.replace("\\", "/")
        if (
            not normalized_path.endswith(".cs")
            or "/SprotoType/" in normalized_path
            or "Managed/Assembly-CSharp/" not in normalized_path
        ):
            continue
        source = contents.decode("utf-8-sig", errors="replace")
        for method_match in method_pattern.finditer(source):
            opening_brace = source.find("{", method_match.start(), method_match.end())
            method_body = extract_braced_block(source, opening_brace)
            method_end = opening_brace + len(method_body) + 2
            method_source = source[opening_brace + 1 : method_end - 1]
            method_signature = method_match.group(0)[:-1]

            for response_name, (rpc_tag, fields, field_tags_by_name) in response_types.items():
                declaration_pattern = re.compile(
                    rf"\b{re.escape(response_name)}\.response\s+"
                    r"(?P<variable>[A-Za-z_]\w*)\s*="
                )
                declarations = [
                    (declaration.group("variable"), declaration.end())
                    for declaration in declaration_pattern.finditer(method_source)
                ]
                declarations.extend(
                    (parameter.group("variable"), 0)
                    for parameter in re.finditer(
                        rf"\b{re.escape(response_name)}\.response\s+"
                        r"(?P<variable>[A-Za-z_]\w*)\b",
                        method_signature,
                    )
                )
                for variable, start_offset in declarations:
                    accesses = re.finditer(
                        rf"\b{re.escape(variable)}\.(?P<member>[A-Za-z_]\w*)",
                        method_source[start_offset:],
                    )
                    for access in accesses:
                        member = access.group("member")
                        field_name = member[3:] if member.startswith("Has") else member
                        if field_name:
                            field_name = field_name[0].lower() + field_name[1:]
                        field_tag = field_tags_by_name.get(field_name)
                        if field_tag is None:
                            continue
                        absolute_offset = (
                            opening_brace + 1 + start_offset + access.start()
                        )
                        line = source.count("\n", 0, absolute_offset) + 1
                        usages[rpc_tag].setdefault(field_tag, []).append(
                            {
                                "file": normalized_path,
                                "line": line,
                                "member": member,
                            }
                        )
    return usages


def read_game_assets(client_address):
    global ASSET_CACHE, ASSET_CACHE_LOADED, RESPONSE_CATALOG
    global REQUEST_CATALOG, SCHEMA_CATALOG, CLIENT_REQUEST_HANDLERS
    global RESPONSE_FIELD_USAGE, RESPONSE_PRODUCERS, GAME_METADATA

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
        (
            REQUEST_CATALOG,
            RESPONSE_CATALOG,
            SCHEMA_CATALOG,
            CLIENT_REQUEST_HANDLERS,
        ) = build_rpc_catalogs(assets)
        RESPONSE_FIELD_USAGE = scan_response_field_usage(assets, RESPONSE_CATALOG)
        RESPONSE_PRODUCERS = scan_response_producers(
            assets,
            RESPONSE_CATALOG,
            CLIENT_REQUEST_HANDLERS,
        )
        GAME_METADATA = extract_game_metadata(assets)
        ASSET_CACHE_LOADED = True
        print("\rPlease wait reading game assets 100/100%")
        print(
            f"[ASSETS] Cached {len(assets)} files ({bytes_read:,} bytes) in RAM; "
            f"indexed {len(REQUEST_CATALOG)} requests, {len(RESPONSE_CATALOG)} "
            f"responses, {len(SCHEMA_CATALOG)} Sproto structs, and "
            f"{sum(len(fields) for fields in RESPONSE_FIELD_USAGE.values())} "
            "response fields with client-side usage; "
            f"{sum(len(producers) for producers in RESPONSE_PRODUCERS.values())} "
            "client-side response producers indexed; "
            f"game version {GAME_METADATA['versionCode']} / data "
            f"{GAME_METADATA['dataVersionCode']}."
        )


def handle_client(connection, address, state=None):
    if state is None:
        state = ClientConnectionState()
    with GAME_STATE_LOCK:
        GAME_STATE[state.state_id] = state.game_state
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
            package = decode_sproto_object(packet, PACKAGE_SCHEMA)
            rpc_tag = package.get(0)
            session = package.get(1)

            if rpc_tag is None:
                with state.lock:
                    pending = state.pending_server_rpcs.pop(session, None)
                if pending is None:
                    print(
                        f"[RPC] Unmatched client response session={session} "
                        f"from {address}"
                    )
                    continue
                response_body = decode_sproto_object(
                    packet[sproto_body_offset(packet) :],
                    pending["schema"],
                )
                print(
                    f"[RPC] Matched client response for tag={pending['tag']} "
                    f"session={session}: {response_body}"
                )
                continue

            spec = REQUEST_CATALOG.get(rpc_tag)
            if spec is None:
                print(
                    f"[RPC] Unknown client request tag={rpc_tag} session={session}; "
                    "no response schema or safe fallback exists; no packet sent."
                )
                continue
            request_body = decode_sproto_object(
                packet[sproto_body_offset(packet) :],
                spec["fields"],
            )
            state.record_request(rpc_tag, request_body, spec["fields"])
            named_request_body = {
                spec["fields"].get(tag, {}).get("name", str(tag)): value
                for tag, value in request_body.items()
            }
            print(
                f"[RPC] Request {spec['name']} tag={rpc_tag} session={session} "
                f"body={named_request_body}"
            )
            if session is None:
                print(f"[RPC] One-way request tag={rpc_tag}; no reply expected.")
                continue
            with state.lock:
                if session in state.pending_client_sessions:
                    duplicate_session = True
                else:
                    duplicate_session = False
                    state.pending_client_sessions[session] = rpc_tag
                    if len(state.pending_client_sessions) > 4096:
                        state.pending_client_sessions.pop(
                            next(iter(state.pending_client_sessions))
                        )
            if duplicate_session:
                print(
                    f"[RPC] Duplicate outstanding client session={session}; "
                    "request is not answered."
                )
                continue
            response_spec = RESPONSE_CATALOG.get(rpc_tag)
            handler = CLIENT_REQUEST_HANDLERS.get(rpc_tag)
            response_fields = []
            for field_tag, field in sorted(
                (response_spec or {}).get("fields", {}).items()
            ):
                uses = RESPONSE_FIELD_USAGE.get(rpc_tag, {}).get(field_tag, [])
                locations = ", ".join(
                    f"{os.path.basename(use['file'])}:{use['line']}"
                    for use in uses[:3]
                )
                usage_note = f"; {locations}" if locations else ""
                response_fields.append(
                    f"{field['name']}:{field['type']} "
                    f"(client uses: {len(uses)}{usage_note})"
                )
            response_fields = tuple(response_fields)
            if response_spec:
                generated_fields, source = generate_auto_response(
                    rpc_tag, request_body, state.game_state
                )
                if generated_fields is not None:
                    send_rpc_response(
                        connection, state, rpc_tag, session, generated_fields
                    )
                    generated_names = {
                        response_spec["fields"][field_tag]["name"]: value
                        for field_tag, value in generated_fields.items()
                    }
                    print(
                        f"[RPC] Auto-replied to {spec['name']} tag={rpc_tag} "
                        f"session={session} fields={generated_names} source={source}"
                    )
                    continue
                source_note = source
            else:
                source_note = "APK has no response schema for this request"
            print(
                f"[RPC] Session {session} awaits response. "
                f"APK response fields={response_fields or 'none'}; "
                f"APK handler for server-originated RPC={handler or 'not registered'}. "
                f"No reply sent: {source_note}."
            )
    except (ConnectionError, OSError) as exc:
        print(f"[!] Connection ended for {address}: {exc}")
    except Exception as exc:
        print(f"[!] Failed handling game client {address}: {exc}")
    finally:
        with GAME_STATE_LOCK:
            GAME_STATE.pop(state.state_id, None)
        state.clear()
        connection.close()
        print(f"[-] Client disconnected: {address}")


def start_server():
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server:
        server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        server.bind(("0.0.0.0", PORT))
        server.listen()
        print(f"GAME SERVER AUTOMATOR READY ON PORT {PORT}")

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
