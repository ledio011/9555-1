"""Source-derived schema loading and resolution layer.

This module does not hardcode protocol IDs or field tags. It loads generated
schema/index data and provides lookup helpers for the server.
"""
from pathlib import Path
import json


class SchemaEngine:
    def __init__(self, index_dir):
        self.index_dir = Path(index_dir)
        self.types = {}
        self.protocols = {}
        self.fields = {}
        self.numeric_relations = {}
        self.load()

    def _load_json(self, path, default):
        try:
            return json.loads(path.read_text(encoding="utf-8"))
        except (FileNotFoundError, json.JSONDecodeError, OSError):
            return default

    def load(self):
        protocol_dir = self.index_dir / "protocol"
        self.types = self._load_json(protocol_dir / "sproto_types.json", {})
        self.protocols = self._load_json(protocol_dir / "sproto_protocols.json", {})
        self.fields = self._load_json(protocol_dir / "fields.json", {})
        self.numeric_relations = self._load_json(
            protocol_dir / "numeric_relations.json", {}
        )

    def protocol(self, message_id):
        return self.protocols.get(str(message_id))

    def type(self, name):
        if name is None:
            return None
        return self.types.get(str(name))

    def package_fields(self):
        result = {}
        for name, schema in self.types.items():
            if str(name).lower().split(".")[-1] != "package":
                continue
            for field in schema.get("fields", []):
                if field.get("name") is not None:
                    result[str(field["name"]).lower()] = field.get("tag")
            break
        return result

    def resolve_message(self, message_id):
        return {
            "message_id": message_id,
            "protocol": self.protocol(message_id),
        }

    def resolve_field(self, type_name, field_name):
        schema = self.type(type_name) or {}
        for field in schema.get("fields", []):
            if field.get("name") == field_name:
                return field
        return None
