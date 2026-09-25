import copy
import json
import os
import re
import socket
import struct
import threading
import time
from datetime import datetime, timezone


PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
DECOMPILED_ROOT = os.path.join(SCRIPT_DIR, "Decompiled")
MAX_FRAME_SIZE = 65535
MAX_COLLECTION_ITEMS = 100000
UNKNOWN_REQUEST_LOG = os.environ.get(
    "UNKNOWN_REQUEST_LOG",
    os.path.join(SCRIPT_DIR, "unknown_requests.jsonl"),
)
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
RESPONSE_EVIDENCE_GRAPH = {}
REAL_RESPONSE_PRODUCERS = {}
DATABASE_RESPONSE_PROVIDER = None
SAFE_RESPONSE_DEFAULTS = {}
GAME_STATE = {}
GAME_STATE_LOCK = threading.Lock()
AUTO_GAME_STATE = {}
UNKNOWN_REQUEST_LOG_LOCK = threading.Lock()
GAME_METADATA = {}
CHARACTER_STORE = {}
CLIENT_STATE_SOURCES = {}
DATABASE_SOURCES = {}


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


def generate_schema_default(descriptor, depth=0):
    if depth > 24:
        return None

    field_type = descriptor.get("type")
    if field_type == "integer":
        return 0
    if field_type == "boolean":
        return False
    if field_type == "string":
        return ""
    if field_type == "bytes":
        return b""
    if field_type == "object":
        result = {}
        for tag, child in descriptor.get("object_schema", {}).items():
            value = generate_schema_default(child, depth + 1)
            if value is not None:
                result[tag] = value
        return result
    if field_type in (
        "integer_list",
        "boolean_list",
        "string_list",
        "object_list",
    ):
        return []
    if field_type == "map":
        return {}
    return None


def find_game_state_value(
    game_state,
    field_name,
    descriptor,
    source_kinds=None,
):
    if not isinstance(game_state, dict) or not field_name:
        return None
    normalized_name = re.sub(r"[^a-z0-9]", "", field_name.casefold())
    categories = {
        "CLIENT_STATE": (
            "account_player", "character", "map", "inventory", "skills",
            "missions", "position", "stats", "npc_aoi", "server_time",
        ),
        "PLAYER_STATE": (
            "account_player", "character", "position", "stats",
        ),
        "GAME_STATE": (
            "map", "inventory", "skills", "missions", "npc_aoi",
            "server_time",
        ),
        "DATABASE": ("database_responses",),
        "COLLECTION": (
            "character", "inventory", "skills", "missions", "npc_aoi",
        ),
    }
    allowed_categories = {
        category
        for source_kind in source_kinds or categories
        for category in categories.get(source_kind, ())
    }
    candidates = [game_state]
    for category in allowed_categories:
        value = game_state.get(category)
        if isinstance(value, dict):
            candidates.append(value)
    for candidate in candidates:
        for key, value in candidate.items():
            if re.sub(r"[^a-z0-9]", "", str(key).casefold()) == normalized_name:
                if isinstance(value, dict) and descriptor.get("type") == "map":
                    for collection_key in ("catalog", "records", "values", "data"):
                        if collection_key in value:
                            value = value[collection_key]
                            break
                if (
                    descriptor.get("type") == "map"
                    and isinstance(value, dict)
                    and all(isinstance(key, int) for key in value)
                ):
                    try:
                        encode_sproto_object({0: value}, {0: descriptor})
                    except (TypeError, ValueError, OverflowError, KeyError):
                        pass
                    else:
                        return copy.deepcopy(value)
                return convert_named_data_to_schema(value, descriptor)
    return None


def convert_named_data_to_schema(value, descriptor, depth=0):
    if depth > 24:
        raise ValueError("state data exceeds maximum schema depth")
    field_type = descriptor.get("type")
    if field_type in ("integer", "boolean", "string", "bytes"):
        if field_type == "integer" and isinstance(value, int) and not isinstance(value, bool):
            return value
        if field_type == "boolean" and isinstance(value, bool):
            return value
        if field_type == "string" and isinstance(value, str):
            return value
        if field_type == "bytes" and isinstance(value, (bytes, bytearray)):
            return bytes(value)
        return None
    if field_type == "object":
        return convert_named_object_to_schema(
            value, descriptor.get("object_schema", {}), depth + 1
        )
    if field_type == "map":
        object_schema = descriptor.get("object_schema", {})
        if not isinstance(value, (dict, list)):
            return None
        if isinstance(value, dict):
            items = value.items()
        else:
            key_tag = descriptor.get("key_tag")
            items = []
            for item in value:
                if not isinstance(item, dict) or key_tag is None:
                    return None
                key_name = object_schema.get(key_tag, {}).get("name")
                if key_name not in item:
                    return None
                items.append((item[key_name], item))
        result = {}
        for key, item in items:
            try:
                map_key = int(key)
            except (TypeError, ValueError):
                return None
            converted = convert_named_object_to_schema(item, object_schema, depth + 1)
            if not converted:
                return None
            key_tag = descriptor.get("key_tag")
            if key_tag is not None and converted.get(key_tag, map_key) != map_key:
                return None
            result[map_key] = converted
        return result
    if field_type in ("integer_list", "boolean_list", "string_list"):
        if not isinstance(value, list):
            return None
        item_type = {
            "integer_list": int,
            "boolean_list": bool,
            "string_list": str,
        }[field_type]
        if any(
            not isinstance(item, item_type)
            or (field_type == "integer_list" and isinstance(item, bool))
            for item in value
        ):
            return None
        return copy.deepcopy(value)
    if field_type == "object_list":
        if not isinstance(value, list):
            return None
        converted = [
            convert_named_object_to_schema(
                item, descriptor.get("object_schema", {}), depth + 1
            )
            for item in value
        ]
        return None if any(item is None for item in converted) else converted
    return None


def convert_named_object_to_schema(value, schema, depth=0):
    if depth > 24 or not isinstance(value, dict):
        return None
    by_name = {
        re.sub(r"[^a-z0-9]", "", field["name"].casefold()): (tag, field)
        for tag, field in schema.items()
    }
    result = {}
    for name, child_value in value.items():
        entry = by_name.get(re.sub(r"[^a-z0-9]", "", str(name).casefold()))
        if entry is None:
            continue
        field_tag, child_descriptor = entry
        converted = convert_named_data_to_schema(
            child_value,
            child_descriptor,
            depth + 1,
        )
        if converted is not None:
            result[field_tag] = converted
    return result


def resolve_universal_schema_defaults(
    tag,
    response_schema,
    request_schema,
    request_fields,
    game_state,
):
    generated = {}
    unsafe_collection_types = {
        "map",
        "object_list",
        "integer_list",
        "boolean_list",
        "string_list",
    }
    for field_tag, descriptor in response_schema.items():
        if descriptor.get("type") in unsafe_collection_types:
            continue
        value = generate_schema_default(descriptor)
        if value is not None:
            generated[field_tag] = value
    return generated


def generate_auto_response(tag, request_fields, game_state=None):
    spec = RESPONSE_CATALOG.get(tag)
    if spec is None:
        return None, "APK has no response schema for this tag"
    request_spec = REQUEST_CATALOG.get(tag)
    request_schema = request_spec["fields"] if request_spec else {}
    sources = (
        ("REAL_PRODUCER", resolve_real_response_producer),
        ("GAME_STATE", resolve_game_state_response),
        ("DATABASE", resolve_database_response),
        ("REQUEST_DERIVED", resolve_request_derived_response),
        ("SAFE_DEFAULT", resolve_safe_response_defaults),
        ("UNIVERSAL_DEFAULT", resolve_universal_schema_defaults),
    )
    response_fields = {}
    resolved_sources = []
    for source_name, resolver in sources:
        try:
            candidate_fields = resolver(
                tag,
                spec["fields"],
                request_schema,
                request_fields,
                game_state,
            )
            validate_response_candidate(candidate_fields, spec["fields"], source_name)
        except (TypeError, ValueError, OverflowError, KeyError) as exc:
            return None, f"{source_name} source rejected: {exc}"
        for field_tag, value in candidate_fields.items():
            if field_tag not in response_fields:
                response_fields[field_tag] = value
                resolved_sources.append(source_name)

    required_fields = get_required_response_fields(tag)
    missing_required = sorted(required_fields - response_fields.keys())
    if missing_required:
        missing_names = [
            spec["fields"][field_tag]["name"] for field_tag in missing_required
        ]
        return None, (
            "required response fields have no real source: "
            + ", ".join(missing_names)
        )

    try:
        encode_sproto_object(response_fields, spec["fields"])
    except (TypeError, ValueError, OverflowError) as exc:
        return None, f"resolved response does not match APK schema: {exc}"
    if not response_fields and spec["fields"]:
        return None, "all response fields are unresolved"
    return response_fields, (
        "priority resolver: " + " > ".join(dict.fromkeys(resolved_sources))
        if resolved_sources
        else "empty response schema"
    )


def resolve_real_response_producer(tag, response_schema, request_schema, request_fields, game_state):
    producer = REAL_RESPONSE_PRODUCERS.get(tag)
    if producer is None:
        return {}
    result = producer(request_fields, game_state or {})
    return result or {}


def resolve_game_state_response(tag, response_schema, request_schema, request_fields, game_state):
    if game_state is None:
        return {}
    authoritative_responses = game_state.get("authoritative_responses", {})
    result = copy.deepcopy(authoritative_responses.get(tag, {}))
    evidence = RESPONSE_EVIDENCE_GRAPH.get(tag, {}).get("fields", {})
    for field_tag, descriptor in response_schema.items():
        if field_tag in result:
            continue
        field_evidence = evidence.get(field_tag, {})
        source_kinds = {
            source.get("source_kind")
            for source in field_evidence.get("sources", ())
        }
        if not source_kinds.intersection(
            {
                "CLIENT_STATE",
                "PLAYER_STATE",
                "GAME_STATE",
                "DATABASE",
                "COLLECTION",
            }
        ):
            continue
        value = find_game_state_value(
            game_state,
            descriptor.get("name", ""),
            descriptor,
            source_kinds=source_kinds,
        )
        if value is not None:
            result[field_tag] = value

    protocol_name = RESPONSE_CATALOG.get(tag, {}).get("name")
    if protocol_name == "character_list":
        return result
    if protocol_name == "character_pick":
        character_id_tag = next(
            (
                field_tag
                for field_tag, field in request_schema.items()
                if field["name"] == "id" and field["type"] == "integer"
            ),
            None,
        )
        if character_id_tag is None:
            return {}
        character_id = request_fields.get(character_id_tag)
        character_ids = game_state.get("character", {}).get("ids", set())
        if character_id not in character_ids:
            return result
        errno_tag = next(
            (
                field_tag
                for field_tag, field in response_schema.items()
                if field["name"] == "errno" and field["type"] == "integer"
            ),
            None,
        )
        if errno_tag is None:
            return result
        game_state["character"]["selected_id"] = character_id
        game_state["flow_stage"] = "character_selected"
        result[errno_tag] = 0
    return result


def find_protocol_tag(catalog, protocol_name):
    return next(
        (
            tag
            for tag, spec in catalog.items()
            if spec["name"] == protocol_name
        ),
        None,
    )


def resolve_database_response(tag, response_schema, request_schema, request_fields, game_state):
    if DATABASE_RESPONSE_PROVIDER is not None:
        result = DATABASE_RESPONSE_PROVIDER(
            tag, request_fields, game_state or {}
        )
        if result is not None:
            validate_response_candidate(result, response_schema, "DATABASE")
            if game_state is not None:
                remember_character_catalog(tag, result, response_schema, game_state)
            return result
    if game_state is None:
        return {}
    result = game_state.get("database_responses", {}).get(tag, {})
    protocol_name = RESPONSE_CATALOG.get(tag, {}).get("name")
    if not result and protocol_name == "character_list":
        result = load_character_store_response(game_state, response_schema)
        account_id = game_state.get("identity")
        if isinstance(account_id, str):
            account = CHARACTER_STORE.get(account_id)
            if isinstance(account, dict):
                characters = account.get("characters")
                if characters is not None:
                    try:
                        count = len(characters)
                    except TypeError:
                        count = 0
                    print(
                        f"[DB] character_list account={account_id} "
                        f"characters={count}"
                    )
                else:
                    print(
                        f"[DB] character_list account={account_id} "
                        "has no 'characters' field"
                    )
            else:
                print(
                    f"[DB] character_list account={account_id} "
                    "not found in characters.json"
                )
        else:
            print("[DB] character_list requested before login identity")
    remember_character_catalog(tag, result, response_schema, game_state)
    return result


def load_character_store_response(game_state, response_schema):
    account_id = game_state.get("identity")
    if not isinstance(account_id, str):
        return {}
    account = CHARACTER_STORE.get(account_id)
    if not isinstance(account, dict):
        return {}
    characters = account.get("characters")
    if characters is None:
        return {}
    for field_tag, descriptor in response_schema.items():
        if descriptor.get("name") != "character" or descriptor.get("type") != "map":
            continue
        converted = convert_named_data_to_schema(characters, descriptor)
        if converted is not None:
            return {field_tag: converted}
    return {}


def load_character_store():
    path = os.environ.get(
        "CHARACTERS_FILE",
        os.path.join(SCRIPT_DIR, "characters.json"),
    )
    if not os.path.isfile(path):
        return {}
    with open(path, "r", encoding="utf-8") as character_file:
        data = json.load(character_file)
    if not isinstance(data, dict):
        raise ValueError(f"{path} must contain a JSON object keyed by account ID")
    accounts = data.get("accounts", data)
    if not isinstance(accounts, dict):
        raise ValueError(f"{path} 'accounts' must be a JSON object")
    normalized = {}
    for account_id, account in accounts.items():
        if not isinstance(account, dict):
            raise ValueError(f"{path} account {account_id!r} must be a JSON object")
        normalized[str(account_id)] = account
    return normalized


def remember_character_catalog(tag, response_fields, response_schema, game_state):
    if RESPONSE_CATALOG.get(tag, {}).get("name") != "character_list":
        return
    character_tag = next(
        (
            field_tag
            for field_tag, field in response_schema.items()
            if field["name"] == "character" and field["type"] == "map"
        ),
        None,
    )
    if character_tag is None or character_tag not in response_fields:
        return
    catalog = response_fields[character_tag]
    game_state.setdefault("character", {}).update(
        catalog=copy.deepcopy(catalog),
        ids=set(catalog),
    )


def advance_game_flow_on_response(tag, game_state):
    if game_state is None:
        return
    protocol_name = RESPONSE_CATALOG.get(tag, {}).get("name")
    stages = {
        "login": "authenticated",
        "character_list": "character_list_ready",
        "character_create": "character_created",
        "character_pick": "character_selected",
    }
    if protocol_name in stages:
        game_state["flow_stage"] = stages[protocol_name]


def dispatch_configured_game_flow(connection, state, protocol_name):
    flow_groups = {
        "enter_map": ("main_player_create", "aoi_add"),
        "map_ready": (
            "sync_common_data",
            "sync_item_pack",
            "sync_skill_info",
            "sync_mission",
        ),
    }
    message_names = flow_groups.get(protocol_name)
    if message_names is None:
        return None
    game_state = state.game_state
    if game_state.get("character", {}).get("selected_id") is None:
        return f"{protocol_name} flow paused: no selected character in authoritative state"

    messages = game_state.get("server_messages", {})
    resolved = []
    missing = []
    for message_name in message_names:
        tag = find_protocol_tag(REQUEST_CATALOG, message_name)
        if tag is None:
            missing.append(f"{message_name} has no APK request schema")
        elif tag not in messages:
            missing.append(f"{message_name} has no configured state payload")
        else:
            resolved.append((tag, messages[tag]))
    if missing:
        return f"{protocol_name} flow paused: " + "; ".join(missing)

    for tag, message in resolved:
        if tag in RESPONSE_CATALOG:
            session = send_server_rpc(
                connection,
                state,
                tag,
                message["body"],
            )
            sent_as = f"RPC session={session}"
        else:
            send_server_push(connection, state, tag, message["body"])
            sent_as = "push"
        print(
            f"[FLOW] Sent {REQUEST_CATALOG[tag]['name']} tag={tag} "
            f"as {sent_as}; source={message['source']}"
        )
    game_state["flow_stage"] = (
        "map_entities_sent" if protocol_name == "enter_map" else "initial_state_sent"
    )
    return f"{protocol_name} flow sent {len(resolved)} configured messages"


def resolve_request_derived_response(tag, response_schema, request_schema, request_fields, game_state):
    if RESPONSE_CATALOG.get(tag, {}).get("name") in {
        "character_list",
        "character_create",
        "character_pick",
    }:
        return {}
    response, _missing = generate_request_derived_response(
        response_schema,
        request_schema,
        request_fields,
        path=RESPONSE_CATALOG[tag]["name"],
        field_budget=[0],
    )
    return response


def resolve_safe_response_defaults(tag, response_schema, request_schema, request_fields, game_state):
    if RESPONSE_CATALOG.get(tag, {}).get("name") in {
        "character_list",
        "character_create",
        "character_pick",
    }:
        return {}
    required_fields = get_required_response_fields(tag)
    return {
        field_tag: value
        for field_tag, value in SAFE_RESPONSE_DEFAULTS.get(tag, {}).items()
        if field_tag not in required_fields
    }


def validate_response_candidate(candidate, response_schema, source_name):
    if candidate is None:
        return
    if not isinstance(candidate, dict):
        raise TypeError(f"{source_name} must return a field-tag dictionary")
    if any(field_tag not in response_schema for field_tag in candidate):
        raise ValueError(f"{source_name} returned fields outside the response schema")
    encode_sproto_object(candidate, response_schema)


def produce_heartbeat_response(request_fields, game_state):
    tag = next(
        (
            rpc_tag
            for rpc_tag, spec in RESPONSE_CATALOG.items()
            if spec["name"] == "heart_beat"
        ),
        None,
    )
    if tag is None:
        return {}
    request_schema = REQUEST_CATALOG.get(tag, {}).get("fields", {})
    response_schema = RESPONSE_CATALOG[tag]["fields"]
    request_time_tag = next(
        (
            field_tag
            for field_tag, field in request_schema.items()
            if field["name"] == "time" and field["type"] == "integer"
        ),
        None,
    )
    response_time_tag = next(
        (
            field_tag
            for field_tag, field in response_schema.items()
            if field["name"] == "time" and field["type"] == "integer"
        ),
        None,
    )
    if (
        request_time_tag is None
        or request_fields.get(request_time_tag) is None
        or response_time_tag is None
    ):
        return {}
    response = {
        response_time_tag: 621355968000000000 + time.time_ns() // 100
    }
    server_time_tag = next(
        (
            field_tag
            for field_tag, field in response_schema.items()
            if field["name"] == "serverTime" and field["type"] == "integer"
        ),
        None,
    )
    if server_time_tag is not None:
        response[server_time_tag] = int(time.time())
    return response


def produce_login_response(request_fields, game_state):
    tag = next(
        (
            rpc_tag
            for rpc_tag, spec in RESPONSE_CATALOG.items()
            if spec["name"] == "login"
        ),
        None,
    )
    if tag is None:
        return {}

    response = {}
    for field_tag, descriptor in RESPONSE_CATALOG[tag]["fields"].items():
        name = descriptor["name"]
        if name == "type" and descriptor["type"] == "integer":
            response[field_tag] = 2
        elif name == "versionCode" and descriptor["type"] == "string":
            version = GAME_METADATA.get("versionCode")
            if version:
                response[field_tag] = version
        elif name == "dataVersionCode" and descriptor["type"] == "string":
            version = GAME_METADATA.get("dataVersionCode")
            if version:
                response[field_tag] = version
        elif name == "serverLevel" and descriptor["type"] == "integer":
            configured_level = os.environ.get("SERVER_LEVEL", "1")
            try:
                level = int(configured_level)
            except ValueError:
                raise ValueError("SERVER_LEVEL must be an integer")
            if level < 0:
                raise ValueError("SERVER_LEVEL must be non-negative")
            response[field_tag] = level
    return response


def get_required_response_fields(tag):
    required = set()
    for field_tag, usages in RESPONSE_FIELD_USAGE.get(tag, {}).items():
        direct_read = any(
            not use.get("member", "").startswith("Has") for use in usages
        )
        presence_checked = any(
            use.get("member", "").startswith("Has") for use in usages
        )
        if direct_read and not presence_checked:
            required.add(field_tag)
    return required


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
        self.game_state = build_auto_game_state()
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
            protocol_name = REQUEST_CATALOG.get(tag, {}).get("name", "").lower()
            category = classify_observed_request_category(protocol_name)
            if category:
                category_state = self.game_state[category]
                category_state.pop("last_observation", None)
                category_state["last_observation"] = {
                    "protocol": protocol_name,
                    "fields": copy.deepcopy(named_fields),
                    "source": "client_request_observation_not_authoritative",
                }
            if protocol_name == "login":
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
                    self.game_state["account_player"]["account_id"] = identity
                    self.game_state["account_player"]["source"] = (
                        "client_request_observation_not_authoritative"
                    )
                self.game_state["flow_stage"] = "login_requested"
            elif protocol_name == "character_list":
                self.game_state["flow_stage"] = "character_list_requested"
            elif protocol_name == "character_create":
                self.game_state["flow_stage"] = "character_create_requested"
                character_tag = next(
                    (
                        field_tag
                        for field_tag, field in request_schema.items()
                        if field["name"] == "character"
                        and field["type"] == "object"
                    ),
                    None,
                )
                if character_tag in request_fields:
                    self.game_state["character"]["create_request"] = copy.deepcopy(
                        request_fields[character_tag]
                    )
            elif protocol_name == "character_pick":
                self.game_state["flow_stage"] = "character_pick_requested"
                character_id_tag = next(
                    (
                        field_tag
                        for field_tag, field in request_schema.items()
                        if field["name"] == "id" and field["type"] == "integer"
                    ),
                    None,
                )
                if character_id_tag in request_fields:
                    self.game_state["character"]["requested_id"] = request_fields[
                        character_id_tag
                    ]
            elif protocol_name in ("enter_map", "map_ready"):
                self.game_state["flow_stage"] = "map_entry_requested"
            elif protocol_name in ("sync_item_pack", "inventory"):
                self.game_state["flow_stage"] = "inventory_sync_requested"
            elif protocol_name == "sync_skill_info":
                self.game_state["flow_stage"] = "skills_sync_requested"
            elif protocol_name == "sync_mission":
                self.game_state["flow_stage"] = "missions_sync_requested"
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
            if spec["name"] == "character_list":
                character_field = next(
                    (
                        field_tag
                        for field_tag, field in spec["fields"].items()
                        if field["name"] == "character" and field["type"] == "map"
                    ),
                    None,
                )
                if character_field is not None:
                    catalog = response_fields.get(character_field, {})
                    self.game_state["character"]["catalog"] = copy.deepcopy(catalog)
                    self.game_state["character"]["ids"] = set(catalog)
                    self.game_state["flow_stage"] = "character_list_ready"
                else:
                    self.game_state["character"]["catalog"] = {}
                    self.game_state["character"]["ids"] = set()
            elif spec["name"] == "character_create":
                errno_tag = next(
                    (
                        field_tag
                        for field_tag, field in spec["fields"].items()
                        if field["name"] == "errno"
                    ),
                    None,
                )
                if errno_tag is not None and response_fields.get(errno_tag) == 0:
                    self.game_state["flow_stage"] = "character_created"

    def set_character_catalog(self, character_map, source="database"):
        tag = find_protocol_tag(RESPONSE_CATALOG, "character_list")
        if tag is None:
            raise KeyError("APK has no character_list response schema")
        response_schema = RESPONSE_CATALOG[tag]["fields"]
        character_tag = next(
            (
                field_tag
                for field_tag, field in response_schema.items()
                if field["name"] == "character" and field["type"] == "map"
            ),
            None,
        )
        if character_tag is None:
            raise ValueError("character_list response has no character map field")
        character_schema = response_schema[character_tag].get("object_schema", {})
        id_tag = next(
            (
                field_tag
                for field_tag, field in character_schema.items()
                if field["name"] == "id" and field["type"] == "integer"
            ),
            None,
        )
        if id_tag is None:
            raise ValueError("character overview schema has no integer id field")
        for character_id, overview in character_map.items():
            if not isinstance(overview, dict) or overview.get(id_tag) != character_id:
                raise ValueError(
                    "character catalog keys must match each overview's schema id"
                )
        response_fields = {character_tag: copy.deepcopy(character_map)}
        validate_response_candidate(response_fields, response_schema, "character catalog")
        with self.lock:
            self.game_state["authoritative_responses"][tag] = response_fields
            self.game_state["character"]["catalog"] = copy.deepcopy(character_map)
            self.game_state["character"]["ids"] = set(character_map)
            self.game_state["character"]["source"] = source
            self.game_state["flow_stage"] = "character_list_ready"

    def set_server_message(self, tag, body_fields, source="database"):
        spec = REQUEST_CATALOG.get(tag)
        if spec is None:
            raise KeyError(f"No APK request/push schema registered for tag {tag}")
        if not isinstance(body_fields, dict):
            raise TypeError("Server message body must be a field-tag dictionary")
        validate_response_candidate(body_fields, spec["fields"], "server message")
        with self.lock:
            self.game_state.setdefault("server_messages", {})[tag] = {
                "body": copy.deepcopy(body_fields),
                "source": source,
            }

    def set_database_response(self, tag, response_fields):
        spec = RESPONSE_CATALOG.get(tag)
        if spec is None:
            raise KeyError(f"No APK response schema registered for RPC tag {tag}")
        if not isinstance(response_fields, dict):
            raise TypeError("Database response must be a field map")
        validate_response_candidate(response_fields, spec["fields"], "DATABASE")
        with self.lock:
            self.game_state["database_responses"][tag] = copy.deepcopy(
            response_fields
            )
            if spec["name"] == "character_list":
                character_field = next(
                    (
                        field_tag
                        for field_tag, field in spec["fields"].items()
                        if field["name"] == "character" and field["type"] == "map"
                    ),
                    None,
                )
                if character_field is not None:
                    catalog = response_fields.get(character_field, {})
                    self.game_state["character"]["catalog"] = copy.deepcopy(catalog)
                    self.game_state["character"]["ids"] = set(catalog)
                    self.game_state["flow_stage"] = "character_list_ready"

    def clear(self):
        with self.lock:
            self.pending_server_rpcs.clear()
            self.pending_client_sessions.clear()
            self.observed_requests.clear()
            self.game_state["identity"] = None
            self.game_state["observed_requests"].clear()
            for category in (
                "account_player",
                "character",
                "map",
                "inventory",
                "skills",
                "missions",
                "position",
                "stats",
                "npc_aoi",
            ):
                self.game_state[category].clear()
            self.game_state["character"].update(
                catalog={},
                ids=set(),
                selected_id=None,
            )
            self.game_state["server_time"].clear()
            self.game_state["authoritative_responses"].clear()
            self.game_state["database_responses"].clear()
            self.game_state.pop("server_messages", None)


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
    responses_by_name = {}
    for tag, spec in responses.items():
        responses_by_name.setdefault(spec["name"], []).append((tag, spec))

    method_pattern = re.compile(
        r"(?m)^[ \t]*(?:(?:public|private|protected|internal|static|virtual|"
        r"override|sealed|async|new|partial|extern)\s+)+"
        r"[\w.<>\[\],?]+\s+\w+\s*\([^;{}]*\)\s*\{"
    )

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
            signature = method_match.group(0)[:-1]
            opening_brace = source.find("{", method_match.start(), method_match.end())
            method_body = extract_braced_block(source, opening_brace)
            method_source = source[
                opening_brace + 1 : opening_brace + 1 + len(method_body)
            ]
            method_name_match = re.search(r"([A-Za-z_]\w*)\s*\([^()]*\)\s*$", signature)
            method_name = method_name_match.group(1) if method_name_match else "unknown"
            request_variables = {
                match.group(1)
                for match in re.finditer(
                    r"\b[A-Za-z_]\w*\.request\s+([A-Za-z_]\w*)\b",
                    signature + " " + method_source,
                )
            }
            request_variables.update(
                match.group(1)
                for match in re.finditer(
                    r"\b(?P<variable>[A-Za-z_]\w*)\s*=\s*\w+\s+as\s+"
                    r"(?:[\w.]+\.)?[A-Za-z_]\w*\.request\b",
                    method_source,
                )
            )
            request_variables.add("request")

            for response_name, tagged_specs in responses_by_name.items():
                response_pattern = re.compile(
                    rf"\b(?:[\w.]+\.response\s+)?"
                    r"(?P<variable>[A-Za-z_]\w*)\s*=\s*new\s+"
                    rf"(?:[\w.]+\.)?{re.escape(response_name)}\.response\s*\("
                )
                for construction in response_pattern.finditer(method_source):
                    variable = construction.group("variable")
                    for tag, response_spec in tagged_specs:
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
                            source_kind, source_field = classify_response_expression(
                                expression, request_variables
                            )
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

                        mutation_pattern = re.compile(
                            rf"\b{re.escape(variable)}\.(?P<field>[A-Za-z_]\w*)\."
                            r"(?P<operation>Add|AddRange|Clear)\s*"
                            r"\((?P<arguments>[^;]*)\)\s*;"
                        )
                        for mutation in mutation_pattern.finditer(method_source):
                            field_tag = fields_by_name.get(mutation.group("field"))
                            if field_tag is None:
                                continue
                            line = source.count(
                                "\n", 0, opening_brace + 1 + mutation.start()
                            ) + 1
                            field_evidence.setdefault(field_tag, []).append(
                                {
                                    "field": mutation.group("field"),
                                    "operation": mutation.group("operation"),
                                    "expression": mutation.group("arguments").strip(),
                                    "source_kind": "COLLECTION",
                                    "source_field": None,
                                    "line": line,
                                }
                            )

                        if field_evidence:
                            producer = {
                                "handler": f"{method_name}",
                                "file": normalized_path,
                                "direction": "decompiled_client_code",
                                "server_reply_usable": False,
                                "fields": field_evidence,
                            }
                            producers.setdefault(tag, []).append(producer)
    return producers


def classify_response_expression(expression, request_variables):
    request_match = re.fullmatch(
        r"(?P<variable>[A-Za-z_]\w*)\.(?P<field>[A-Za-z_]\w*)",
        expression,
    )
    if request_match and request_match.group("variable") in request_variables:
        return "REQUEST", request_match.group("field")
    if re.fullmatch(r"-?\d+[lL]?", expression) or expression in ("true", "false"):
        return "CONSTANT", None
    if re.fullmatch(r'"(?:[^"\\]|\\.)*"', expression):
        return "CONSTANT", None
    if re.search(r"\b(?:PlayerData|PlayerCommonData|ObjMainPlayer|PlayerData)\b", expression):
        return "PLAYER_STATE", None
    if re.search(r"\b(?:GameManager|SceneManager|ObjManager|NetManager)\b", expression):
        return "GAME_STATE", None
    if re.search(r"\b(?:DB|DataManager|Database|Sqlite)\b", expression, re.IGNORECASE):
        return "DATABASE", None
    if re.search(
        r"\b(?:new\s+(?:List|Dictionary|HashSet)\s*<|Add|AddRange|Clear)\s*\(",
        expression,
    ):
        return "COLLECTION", None
    if any(token in expression for token in ("(", ")", "+", "-", "*", "/", "?")):
        return "COMPUTED", None
    if re.match(r"data\.", expression, re.IGNORECASE):
        return "GAME_STATE", None
    return "UNKNOWN", None


def resolve_scanned_response_producers(tag, response_schema, request_schema, request_fields):
    generated = {}
    evidence = []
    for producer in RESPONSE_PRODUCERS.get(tag, ()):
        if not producer.get("server_reply_usable"):
            continue
        for field_tag, assignments in producer.get("fields", {}).items():
            descriptor = response_schema.get(field_tag)
            if descriptor is None:
                continue
            for assignment in assignments:
                value = resolve_scanned_assignment(
                    assignment, descriptor, request_schema, request_fields
                )
                if value is not None:
                    generated[field_tag] = value
                    evidence.append(
                        f"{producer['file']}:{assignment['line']} "
                        f"{assignment['source_kind']}"
                    )
                    break
    return generated, evidence


def classify_observed_request_category(protocol_name):
    categories = {
        "account_player": ("login", "visitor", "verfiy", "verify", "account"),
        "character": ("character", "role"),
        "inventory": ("inventory", "item", "bag", "equip", "warehouse"),
        "skills": ("skill",),
        "missions": ("mission", "quest"),
        "position": ("move", "position", "coordinate"),
        "stats": ("attribute", "status", "level", "power"),
        "npc_aoi": ("npc", "aoi", "other_player", "player_map"),
        "map": ("map", "scene", "line", "refresh_online"),
        "server_time": ("heart_beat", "heartbeat", "sync_common_data"),
    }
    for category, keywords in categories.items():
        if any(keyword in protocol_name for keyword in keywords):
            return category
    return None


def build_auto_game_state():
    return {
        "identity": None,
        "flow_stage": "connected",
        "observed_requests": {},
        "account_player": {},
        "character": {
            "catalog": {},
            "ids": set(),
            "selected_id": None,
        },
        "map": {},
        "inventory": {},
        "skills": {},
        "missions": {},
        "position": {},
        "stats": {},
        "npc_aoi": {},
        "server_time": {
            "unix_seconds": int(time.time()),
            "source": "host_clock",
        },
        "authoritative_responses": {},
        "database_responses": {},
        "server_messages": {},
        "state_source": "client_observations_and_configured_providers",
    }


def describe_unknown_value(value):
    if isinstance(value, bool):
        return {"type": "boolean", "value": value}
    if isinstance(value, int):
        return {"type": "integer", "value": value}
    if isinstance(value, str):
        return {"type": "string", "length": len(value), "value": "<redacted>"}
    if isinstance(value, (bytes, bytearray)):
        return {"type": "bytes", "length": len(value), "value": "<redacted>"}
    if isinstance(value, dict):
        return {
            "type": "object",
            "fields": {
                str(field_tag): describe_unknown_value(field_value)
                for field_tag, field_value in value.items()
            },
        }
    if isinstance(value, list):
        return {
            "type": "list",
            "length": len(value),
            "items": [describe_unknown_value(item) for item in value[:20]],
        }
    return {"type": type(value).__name__}


def log_unknown_request(address, tag, session, body_fields):
    response = RESPONSE_CATALOG.get(tag)
    request = REQUEST_CATALOG.get(tag)
    fields = []
    for field_tag, value in sorted(body_fields.items()):
        descriptor = (request or {}).get("fields", {}).get(field_tag, {})
        fields.append(
            {
                "tag": field_tag,
                "name": descriptor.get("name"),
                "schema_type": descriptor.get("type"),
                "observed": describe_unknown_value(value),
            }
        )
    record = {
        "timestamp": datetime.now(timezone.utc).isoformat(),
        "client": str(address[0]),
        "tag": tag,
        "session": session,
        "request": {
            "name": request.get("name") if request else None,
            "schema": (
                {
                    str(field_tag): {
                        "name": descriptor["name"],
                        "type": descriptor["type"],
                    }
                    for field_tag, descriptor in request["fields"].items()
                }
                if request
                else None
            ),
        },
        "response_schema": (
            {
                "name": response["name"],
                "fields": {
                    str(field_tag): {
                        "name": descriptor["name"],
                        "type": descriptor["type"],
                    }
                    for field_tag, descriptor in response["fields"].items()
                },
            }
            if response
            else None
        ),
        "fields": fields,
        "client_handler": CLIENT_REQUEST_HANDLERS.get(tag),
        "producer_evidence": copy.deepcopy(RESPONSE_PRODUCERS.get(tag, [])),
        "raw_packet_saved": False,
    }
    encoded = json.dumps(record, ensure_ascii=True, separators=(",", ":"))
    log_path = os.path.abspath(UNKNOWN_REQUEST_LOG)
    max_bytes = int(os.environ.get("UNKNOWN_REQUEST_LOG_MAX_BYTES", 10 * 1024 * 1024))
    if max_bytes < 1024:
        raise ValueError("UNKNOWN_REQUEST_LOG_MAX_BYTES must be at least 1024")
    with UNKNOWN_REQUEST_LOG_LOCK:
        directory = os.path.dirname(log_path)
        if directory:
            os.makedirs(directory, exist_ok=True)
        if os.path.exists(log_path) and os.path.getsize(log_path) + len(encoded) + 1 > max_bytes:
            backup_path = log_path + ".1"
            if os.path.exists(backup_path):
                os.remove(backup_path)
            os.replace(log_path, backup_path)
        with open(log_path, "a", encoding="utf-8") as log_file:
            log_file.write(encoded + "\n")


def resolve_scanned_assignment(assignment, descriptor, request_schema, request_fields):
    source_kind = assignment.get("source_kind")
    field_type = descriptor.get("type")
    if source_kind == "REQUEST":
        source_field = assignment.get("source_field")
        for request_tag, request_descriptor in request_schema.items():
            if (
                request_descriptor["name"] == source_field
                and request_descriptor["type"] == field_type
                and request_tag in request_fields
            ):
                return request_fields[request_tag]
        return None
    if source_kind != "CONSTANT":
        return None

    expression = assignment.get("expression", "").strip()
    if field_type == "integer" and re.fullmatch(r"-?\d+[lL]?", expression):
        return int(expression.rstrip("lL"))
    if field_type == "boolean" and expression in ("true", "false"):
        return expression == "true"
    if field_type == "string" and re.fullmatch(r'"(?:[^"\\]|\\.)*"', expression):
        return bytes(expression[1:-1], "utf-8").decode("unicode_escape")
    return None


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


def scan_client_state_sources(assets):
    return scan_source_evidence(
        assets,
        re.compile(
            r"\b(?:Player|Character|Game|Scene|Map|Inventory|"
            r"Skill|Mission|Role|Actor)[A-Za-z0-9_]*\s*"
            r"(?:\.Instance|\.instance|\.Current|\.current|\.Instance\.)"
            r"|(?:this\.)?_[A-Za-z0-9]*(?:player|character|state|data|manager)",
            re.IGNORECASE,
        ),
        "CLIENT_STATE",
    )


def scan_database_sources(assets):
    return scan_source_evidence(
        assets,
        re.compile(
            r"\b(?:DB|Db|Database|DataBase|DAO|Dao|Repository|"
            r"PlayerPrefs|SQLite|Sqlite|Table|Query|Select|LoadFromDB)"
            r"[A-Za-z0-9_]*\b",
            re.IGNORECASE,
        ),
        "DATABASE",
    )


def scan_source_evidence(assets, pattern, source_kind):
    evidence = []
    for path, contents in assets.items():
        normalized_path = path.replace("\\", "/")
        if not normalized_path.lower().endswith(".cs"):
            continue
        source = contents.decode("utf-8-sig", errors="replace")
        for match in pattern.finditer(source):
            evidence.append(
                {
                    "source_kind": source_kind,
                    "file": normalized_path,
                    "line": source.count("\n", 0, match.start()) + 1,
                    "symbol": match.group(0),
                }
            )
    return evidence


def build_response_evidence_graph(
    responses,
    client_handlers,
    field_usage,
    producers,
    client_state_sources,
    database_sources,
):
    source_evidence = client_state_sources + database_sources
    graph = {}
    for tag, response in responses.items():
        fields = {}
        for field_tag, descriptor in response["fields"].items():
            usages = copy.deepcopy(field_usage.get(tag, {}).get(field_tag, []))
            producer_assignments = []
            for producer in producers.get(tag, ()):
                for assignment in producer.get("fields", {}).get(field_tag, ()):
                    producer_assignments.append(
                        {
                            "handler": producer["handler"],
                            "file": producer["file"],
                            "direction": producer["direction"],
                            **copy.deepcopy(assignment),
                        }
                    )

            anchors = [
                (item["file"], item["line"])
                for item in usages + producer_assignments
            ]
            related_sources = []
            for source in source_evidence:
                if any(
                    source["file"] == path and abs(source["line"] - line) <= 80
                    for path, line in anchors
                ):
                    related_sources.append(copy.deepcopy(source))

            source_kinds = {
                assignment["source_kind"]
                for assignment in producer_assignments
            }
            source_kinds.update(source["source_kind"] for source in related_sources)
            fields[field_tag] = {
                "name": descriptor["name"],
                "type": descriptor["type"],
                "client_handler": client_handlers.get(tag),
                "client_usage": usages,
                "producer_assignments": producer_assignments,
                "sources": related_sources
                + [
                    {
                        "source_kind": kind,
                        "origin": "producer_expression",
                    }
                    for kind in sorted(source_kinds)
                    if kind
                    not in {source["source_kind"] for source in related_sources}
                ],
            }
        graph[tag] = {
            "protocol": response["name"],
            "fields": fields,
        }
    return graph


def read_game_assets(client_address):
    global ASSET_CACHE, ASSET_CACHE_LOADED, RESPONSE_CATALOG
    global REQUEST_CATALOG, SCHEMA_CATALOG, CLIENT_REQUEST_HANDLERS
    global RESPONSE_FIELD_USAGE, RESPONSE_PRODUCERS, GAME_METADATA
    global CHARACTER_STORE, CLIENT_STATE_SOURCES, DATABASE_SOURCES
    global RESPONSE_EVIDENCE_GRAPH

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
        REAL_RESPONSE_PRODUCERS.clear()
        for tag, spec in RESPONSE_CATALOG.items():
            if spec["name"] == "heart_beat":
                REAL_RESPONSE_PRODUCERS[tag] = produce_heartbeat_response
            elif spec["name"] == "login":
                REAL_RESPONSE_PRODUCERS[tag] = produce_login_response
        RESPONSE_FIELD_USAGE = scan_response_field_usage(assets, RESPONSE_CATALOG)
        RESPONSE_PRODUCERS = scan_response_producers(
            assets,
            RESPONSE_CATALOG,
            CLIENT_REQUEST_HANDLERS,
        )
        GAME_METADATA = extract_game_metadata(assets)
        CHARACTER_STORE = load_character_store()
        CLIENT_STATE_SOURCES = scan_client_state_sources(assets)
        DATABASE_SOURCES = scan_database_sources(assets)
        RESPONSE_EVIDENCE_GRAPH = build_response_evidence_graph(
            RESPONSE_CATALOG,
            CLIENT_REQUEST_HANDLERS,
            RESPONSE_FIELD_USAGE,
            RESPONSE_PRODUCERS,
            CLIENT_STATE_SOURCES,
            DATABASE_SOURCES,
        )
        ASSET_CACHE_LOADED = True
        producer_source_counts = {}
        for producers in RESPONSE_PRODUCERS.values():
            for producer in producers:
                for assignments in producer["fields"].values():
                    for assignment in assignments:
                        source_kind = assignment["source_kind"]
                        producer_source_counts[source_kind] = (
                            producer_source_counts.get(source_kind, 0) + 1
                        )
        evidence_field_count = sum(
            len(entry["fields"]) for entry in RESPONSE_EVIDENCE_GRAPH.values()
        )
        print("\rPlease wait reading game assets 100/100%")
        print(
            f"[ASSETS] Cached {len(assets)} files ({bytes_read:,} bytes) in RAM; "
            f"indexed {len(REQUEST_CATALOG)} requests, {len(RESPONSE_CATALOG)} "
            f"responses, {len(SCHEMA_CATALOG)} Sproto structs, and "
            f"{sum(len(fields) for fields in RESPONSE_FIELD_USAGE.values())} "
            "response fields with client-side usage; "
            f"indexed {len(CLIENT_STATE_SOURCES)} client-state and "
            f"{len(DATABASE_SOURCES)} database-source references; "
            f"linked evidence for {evidence_field_count} response fields; "
            f"{sum(len(producers) for producers in RESPONSE_PRODUCERS.values())} "
            "response producer sites indexed "
            f"(sources: {producer_source_counts}); "
            f"game version {GAME_METADATA['versionCode']} / data "
            f"{GAME_METADATA['dataVersionCode']}."
        )


def handle_client(connection, address, state=None):
    if state is None:
        state = ClientConnectionState()
    with GAME_STATE_LOCK:
        GAME_STATE[state.state_id] = state.game_state
        AUTO_GAME_STATE[state.state_id] = state.game_state
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
                unknown_body = decode_sproto_object(
                    packet[sproto_body_offset(packet) :]
                )
                log_unknown_request(
                    address,
                    rpc_tag,
                    session,
                    unknown_body,
                )
                print(
                    f"[RPC] Unknown client request tag={rpc_tag} session={session}; "
                    f"analysis saved to {UNKNOWN_REQUEST_LOG}; "
                    "no response schema or safe fallback exists; no packet sent."
                )
                continue
            request_body = decode_sproto_object(
                packet[sproto_body_offset(packet) :],
                spec["fields"],
            )
            state.record_request(rpc_tag, request_body, spec["fields"])
            protocol_name = spec["name"]
            if protocol_name in ("enter_map", "map_ready"):
                flow_result = dispatch_configured_game_flow(
                    connection,
                    state,
                    protocol_name,
                )
                if flow_result:
                    print(f"[FLOW] {flow_result}")
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
                    advance_game_flow_on_response(rpc_tag, state.game_state)
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
            AUTO_GAME_STATE.pop(state.state_id, None)
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
