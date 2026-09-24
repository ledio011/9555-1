import os
import re
import json
from pathlib import Path
from collections import defaultdict

OUTPUT = Path("apk_index")
for directory in [OUTPUT, OUTPUT / "protocol"]:
    directory.mkdir(parents=True, exist_ok=True)


def extract_sproto_from_csharp(source_dir):
    """
    Parses Protocol.cs and all C# files inside SprotoType/ to extract 100% 
    of Sproto protocols and field types directly from decompiled C# code.
    """
    protocols = {}
    types = {}

    # 1. Locate Protocol.cs
    protocol_cs_files = list(Path(source_dir).rglob("Protocol.cs"))
    if not protocol_cs_files:
        print("[!] Protocol.cs not found in source directory.")
        return protocols, types

    protocol_cs = protocol_cs_files[0]
    content = protocol_cs.read_text(encoding="utf-8", errors="replace")

    print(f"[+] Found Protocol.cs at {protocol_cs.relative_to(source_dir)}")

    # Parse SetProtocol, SetRequest, SetResponse from Protocol.cs
    proto_tag_map = {}  # tag_str -> {"name": ..., "request": ..., "response": ...}

    # Match base.Protocol.SetProtocol<Protocol.name>(tag);
    for m in re.finditer(r"SetProtocol<Protocol\.([A-Za-z0-9_]+)>\((\d+)\)", content):
        pname, tag = m.group(1), m.group(2)
        if tag not in proto_tag_map:
            proto_tag_map[tag] = {"name": pname, "tag": int(tag), "request": None, "response": None}

    # Match base.Protocol.SetRequest<SprotoType.type_name>(tag);
    for m in re.finditer(r"SetRequest<SprotoType\.([A-Za-z0-9_.]+)\.request>\((\d+)\)", content):
        req_type, tag = m.group(1), m.group(2)
        if tag in proto_tag_map:
            proto_tag_map[tag]["request"] = f"{req_type}.request"

    # Match base.Protocol.SetResponse<SprotoType.type_name>(tag);
    for m in re.finditer(r"SetResponse<SprotoType\.([A-Za-z0-9_.]+)\.response>\((\d+)\)", content):
        resp_type, tag = m.group(1), m.group(2)
        if tag in proto_tag_map:
            proto_tag_map[tag]["response"] = f"{resp_type}.response"

    protocols = proto_tag_map

    # 2. Parse all C# files in SprotoType/
    sproto_type_files = list(Path(source_dir).rglob("SprotoType/*.cs"))
    print(f"[+] Found {len(sproto_type_files)} C# files in SprotoType/")

    for cs_file in sproto_type_files:
        text = cs_file.read_text(encoding="utf-8", errors="replace")
        
        # Match class declarations (e.g., class character_list or class response : SprotoTypeBase)
        # Or top level class inside SprotoType namespace
        parent_class_match = re.search(r"public class ([A-Za-z0-9_]+)", text)
        if not parent_class_match:
            continue

        parent_name = parent_class_match.group(1)

        # Look for nested classes (e.g., request / response) or single class
        # Parse fields from encode() method in C#
        # Matches: this.serialize.write_integer(this.field_name, tag)
        # Matches: this.serialize.write_string(this.field_name, tag)
        # Matches: this.serialize.write_obj(this.field_name, tag)
        # Matches: this.serialize.write_map(this.field_name, tag)
        # Matches: this.serialize.write_boolean(this.field_name, tag)

        def parse_class_fields(class_text):
            fields = []
            # Extract serialize.write_... calls
            write_pattern = re.compile(
                r"write_([A-Za-z0-9_<>]+)\s*\(\s*this\.([A-Za-z0-9_]+)\s*,\s*(\d+)\s*\)"
            )
            for wm in write_pattern.finditer(class_text):
                wtype = wm.group(1)
                fname = wm.group(2)
                ftag = int(wm.group(3))

                if "integer" in wtype or "int" in wtype:
                    stype = "integer"
                elif "boolean" in wtype or "bool" in wtype:
                    stype = "boolean"
                elif "string" in wtype:
                    stype = "string"
                elif "map" in wtype or "vector" in wtype or "obj" in wtype:
                    stype = "*string"  # List/map/obj
                else:
                    stype = "string"

                fields.append({"name": fname, "tag": ftag, "type": stype})

            return sorted(fields, key=lambda x: x["tag"])

        # Check if file has request/response nested classes
        if "public class request" in text or "public class response" in text:
            # Parse request
            req_match = re.search(r"public class request[^{]*\{([\s\S]*?)\n\t\t\}", text)
            if req_match:
                req_fields = parse_class_fields(req_match.group(1))
                if req_fields:
                    types[f"{parent_name}.request"] = {
                        "name": f"{parent_name}.request",
                        "fields": req_fields
                    }

            # Parse response
            resp_match = re.search(r"public class response[^{]*\{([\s\S]*?)\n\t\t\}", text)
            if resp_match:
                resp_fields = parse_class_fields(resp_match.group(1))
                if resp_fields:
                    types[f"{parent_name}.response"] = {
                        "name": f"{parent_name}.response",
                        "fields": resp_fields
                    }
        else:
            # Standalone type (e.g. character_overview, gameitem, general)
            standalone_fields = parse_class_fields(text)
            if standalone_fields:
                types[parent_name] = {
                    "name": parent_name,
                    "fields": standalone_fields
                }

    return protocols, types


def main():
    # Auto-detect source directory if not provided
    script_dir = Path(__file__).resolve().parent
    potential_sources = [
        script_dir / "Decompiled",
        script_dir / "Atg_Auto" / "Decompiled",
        Path("C:/Users/User/Downloads/dec&normal/Decompiled"),
        Path("C:/Users/User/Downloads/Atg_Auto/Decompiled")
    ]

    source_dir = None
    for p in potential_sources:
        if p.exists() and p.is_dir():
            source_dir = p
            break

    if not source_dir:
        val = input("Source folder (e.g. Decompiled or Atg_Auto/Decompiled): ").strip().strip('"')
        source_dir = Path(val).resolve()

    print(f"[+] Scanning decompiled source: {source_dir}")

    protocols, types = extract_sproto_from_csharp(source_dir)

    # Save output JSON files
    with open(OUTPUT / "protocol" / "sproto_protocols.json", "w", encoding="utf-8") as f:
        json.dump(protocols, f, indent=2, ensure_ascii=False)

    with open(OUTPUT / "protocol" / "sproto_types.json", "w", encoding="utf-8") as f:
        json.dump(types, f, indent=2, ensure_ascii=False)

    print("\n" + "=" * 70)
    print("INDEX COMPLETE")
    print("=" * 70)
    print(f"Sproto Protocols Extracted : {len(protocols)}")
    print(f"Sproto Types Extracted     : {len(types)}")
    print(f"Output: {OUTPUT / 'protocol' / 'sproto_protocols.json'}")
    print(f"Output: {OUTPUT / 'protocol' / 'sproto_types.json'}\n")


if __name__ == "__main__":
    main()
