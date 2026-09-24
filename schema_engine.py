"""Source-derived schema loading/resolution layer; no hardcoded protocol IDs."""
from pathlib import Path
import json

class SchemaEngine:
    def __init__(self, index_dir):
        self.index_dir=Path(index_dir); self.types={}; self.protocols={}; self.fields={}; self.numeric_relations={}; self.load()
    def _load(self,p,d):
        try: return json.loads(p.read_text(encoding="utf-8"))
        except (OSError,ValueError): return d
    def load(self):
        p=self.index_dir/"protocol"
        self.types=self._load(p/"sproto_types.json",{})
        self.protocols=self._load(p/"sproto_protocols.json",{})
        self.fields=self._load(p/"fields.json",{})
        self.numeric_relations=self._load(p/"numeric_relations.json",{})
    def protocol(self,message_id): return self.protocols.get(str(message_id))
    def type(self,name): return self.types.get(str(name)) if name is not None else None
    def package_fields(self):
        for name,schema in self.types.items():
            if str(name).lower().split(".")[-1]=="package":
                return {str(f["name"]).lower():f.get("tag") for f in schema.get("fields",[]) if f.get("name") is not None}
        return {}
    def resolve_message(self,message_id): return {"message_id":message_id,"protocol":self.protocol(message_id)}
    def resolve_field(self,type_name,field_name):
        for f in (self.type(type_name) or {}).get("fields",[]):
            if f.get("name")==field_name: return f
        return None
