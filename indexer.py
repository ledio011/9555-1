import os
import re
import json
from pathlib import Path
from collections import defaultdict

OUTPUT = Path("apk_index")
for directory in [OUTPUT, OUTPUT / "protocol"]:
    directory.mkdir(parents=True, exist_ok=True)


def extract_sproto_from_csharp(source_dir):
    protocols = {}
    types = {}

    files = list(Path(source_dir).rglob("*"))
    files = [f for f in files if f.is_file()]
    total_files = len(files)

    print(f"\n[+] Total files found to scan: {total_files}")
    print("=" * 70)

    # 1. Search for Protocol.cs
    protocol_cs_files = [f for f in files if f.name == "Protocol.cs"]
    if protocol_cs_files:
        protocol_cs = protocol_cs_files[0]
        content = protocol_cs.read_text(encoding="utf-8", errors="replace")
        print(f"[+] Found Protocol.cs at: {protocol_cs.relative_to(source_dir)}")

        proto_tag_map = {}

        for m in re.finditer(r"SetProtocol<Protocol\.([A-Za-z0-9_]+)>\((\d+)\)", content):
            pname, tag = m.group(1), m.group(2)
            if tag not in proto_tag_map:
                proto_tag_map[tag] = {"name": pname, "tag": int(tag), "request": None, "response": None}

        for m in re.finditer(r"SetRequest<SprotoType\.([A-Za-z0-9_.]+)\.request>\((\d+)\)", content):
            req_type, tag = m.group(1), m.group(2)
            if tag in proto_tag_map:
                proto_tag_map[tag]["request"] = f"{req_type}.request"

        for m in re.finditer(r"SetResponse<SprotoType\.([A-Za-z0-9_.]+)\.response>\((\d+)\)", content):
            resp_type, tag = m.group(1), m.group(2)
            if tag in proto_tag_map:
                proto_tag_map[tag]["response"] = f"{resp_type}.response"

        protocols = proto_tag_map

    # 2. Progress loop reading X of Y files
    sproto_type_files = [f for f in files if "SprotoType" in str(f) and f.suffix.lower() == ".cs"]
    print(f"[+] Scanning {len(sproto_type_files)} SprotoType files...")

    for index, cs_file in enumerate(sproto_type_files, start=1):
        rel_path = cs_file.relative_to(source_dir)
        print(f"[{index}/{len(sproto_type_files)}] Reading {rel_path}...")

        try:
            text = cs_file.read_text(encoding="utf-8", errors="replace")
            parent_class_match = re.search(r"public class ([A-Za-z0-9_]+)", text)
            if not parent_class_match:
                continue

            parent_name = parent_class_match.group(1)

            def parse_class_fields(class_text):
                fields = []
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
                    else:
                        stype = "*string"

                    fields.append({"name": fname, "tag": ftag, "type": stype})

                return sorted(fields, key=lambda x: x["tag"])

            if "public class request" in text or "public class response" in text:
                req_match = re.search(r"public class request[^{]*\{([\s\S]*?)\n\t\t\}", text)
                if req_match:
                    req_fields = parse_class_fields(req_match.group(1))
                    if req_fields:
                        types[f"{parent_name}.request"] = {
                            "name": f"{parent_name}.request",
                            "fields": req_fields
                        }

                resp_match = re.search(r"public class response[^{]*\{([\s\S]*?)\n\t\t\}", text)
                if resp_match:
                    resp_fields = parse_class_fields(resp_match.group(1))
                    if resp_fields:
                        types[f"{parent_name}.response"] = {
                            "name": f"{parent_name}.response",
                            "fields": resp_fields
                        }
            else:
                standalone_fields = parse_class_fields(text)
                if standalone_fields:
                    types[parent_name] = {
                        "name": parent_name,
                        "fields": standalone_fields
                    }
        except Exception as e:
            print(f"[!] Error reading {cs_file.name}: {e}")

    return protocols, types


def main():
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

    print(f"\n[+] Source Directory: {source_dir}")

    protocols, types = extract_sproto_from_csharp(source_dir)

    # Save JSON index files
    with open(OUTPUT / "protocol" / "sproto_protocols.json", "w", encoding="utf-8") as f:
        json.dump(protocols, f, indent=2, ensure_ascii=False)

    with open(OUTPUT / "protocol" / "sproto_types.json", "w", encoding="utf-8") as f:
        json.dump(types, f, indent=2, ensure_ascii=False)

    print("\n" + "=" * 70)
    print("INDEX COMPLETE")
    print("=" * 70)
    print(f"Sproto Protocols Extracted : {len(protocols)}")
    print(f"Sproto Types Extracted     : {len(types)}")
    print(f"Saved: {OUTPUT / 'protocol' / 'sproto_protocols.json'}")
    print(f"Saved: {OUTPUT / 'protocol' / 'sproto_types.json'}\n")


if __name__ == "__main__":
    main()
