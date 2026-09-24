import os
import json
import struct
from pathlib import Path


def encode_sproto(fields, fn=None):
    """
    Sproto encoder. Formats a list of (tag, value) tuples into binary Sproto format.
    Handles ints, bools, strings, lists, dicts, and nested Sproto structures.
    """
    if not fields:
        return struct.pack("<H", 0)

    # Sort fields by tag
    fields = sorted(fields, key=lambda x: x[0])
    header = []
    body = bytearray()
    last_tag = -1

    for tag, val in fields:
        skip = tag - last_tag - 1
        if skip > 0:
            header.append(2 * (skip - 1) + 1)

        if val is None:
            header.append(1)
        elif isinstance(val, bool):
            header.append((1 if val else 0) * 2 + 2)
        elif isinstance(val, int):
            if 0 <= val <= 32766:
                header.append((val + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= val <= 2147483647:
                    body += struct.pack("<I", 4) + struct.pack("<i", val)
                else:
                    body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray, list, dict)):
            header.append(0)
            if isinstance(val, str):
                v = val.encode('utf-8')
            elif isinstance(val, list):
                if val and isinstance(val[0], int):
                    v = b"\x08" + b"".join([struct.pack("<q", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str):
                            item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)):
                            pass
                        else:
                            item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for item in val.values():
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else:
                        items.append(struct.pack("<I", 1) + (b'\x01' if item else b'\x00'))
                v = b"".join(items)
            else:
                v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag

    fn_val = fn if fn is not None else len(header)
    res = struct.pack("<H", fn_val)
    for h in header:
        res += struct.pack("<H", h)
    return res + body


def sproto_pack(data):
    """
    Sproto 0-pack compression function.
    """
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i + 8]
        if len(chunk) < 8:
            chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0:
                mask |= (1 << j)
        if mask == 0xFF:
            out.append(0xFF)
            out.append(0)
            out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j):
                    out.append(chunk[j])
    return bytes(out)


def sproto_unpack(data):
    """
    Sproto byte unpacker.
    """
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


class SchemaResponseEngine:
    """
    In-memory RAM Sproto Schema & Response Engine for com.doodlemobile.vicecity.
    """
    def __init__(self, index_dir="apk_index"):
        self.protocols = {}
        self.types = {}
        self.load_schemas(index_dir)

    def load_schemas(self, index_dir):
        base_path = Path(index_dir) / "protocol"
        proto_file = base_path / "sproto_protocols.json"
        types_file = base_path / "sproto_types.json"

        if proto_file.exists():
            try:
                with open(proto_file, "r", encoding="utf-8") as f:
                    self.protocols = json.load(f)
            except Exception as e:
                print(f"[WARN] Error loading sproto_protocols.json: {e}")

        if types_file.exists():
            try:
                with open(types_file, "r", encoding="utf-8") as f:
                    self.types = json.load(f)
            except Exception as e:
                print(f"[WARN] Error loading sproto_types.json: {e}")

        print(f"[SCHEMA LOADED] Protocols: {len(self.protocols)} | Types: {len(self.types)}")

    def auto_build_type(self, type_name, custom_values=None):
        if not type_name or type_name not in self.types:
            return []

        if custom_values is None:
            custom_values = {}

        schema = self.types[type_name]
        fields = []

        for field in schema.get("fields", []):
            name = field["name"]
            tag = field["tag"]
            ftype = field["type"]

            if name in custom_values:
                val = custom_values[name]
            else:
                val = self._generate_default_value(ftype)

            if val is not None:
                fields.append((tag, val))

        return fields

    def _generate_default_value(self, ftype):
        ftype_lower = ftype.lower()
        if ftype_lower.startswith("*"):
            return []
        elif ftype_lower in ("integer", "int", "long"):
            return 0
        elif ftype_lower in ("boolean", "bool"):
            return True
        elif ftype_lower == "string":
            return ""
        elif ftype in self.types:
            sub_fields = self.auto_build_type(ftype)
            return encode_sproto(sub_fields)
        return None

    def create_response_frame(self, msg_tag, session_id, custom_data=None):
        msg_str = str(msg_tag)
        proto_info = self.protocols.get(msg_str)

        if not proto_info:
            if session_id is not None:
                return self._build_ack_frame(session_id, encode_sproto([]))
            return None

        response_type_name = proto_info.get("response")

        if not response_type_name:
            return None

        response_fields = self.auto_build_type(response_type_name, custom_data)
        encoded_body = encode_sproto(response_fields)

        if session_id is not None:
            return self._build_ack_frame(session_id, encoded_body)
        return None

    def _build_ack_frame(self, session_id, body_bytes):
        ph = encode_sproto([(1, session_id)])
        raw_packet = ph + body_bytes
        return sproto_pack(raw_packet)
