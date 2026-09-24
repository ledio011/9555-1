import os
import re
import json
import hashlib
import struct
from pathlib import Path
from collections import defaultdict

OUTPUT = Path("apk_index")

MAX_TEXT_SIZE = 50 * 1024 * 1024
MAX_BINARY_SCAN = 25 * 1024 * 1024
MIN_STRING_LENGTH = 4

for directory in [
    OUTPUT,
    OUTPUT / "text",
    OUTPUT / "strings",
    OUTPUT / "metadata",
    OUTPUT / "protocol",
]:
    directory.mkdir(parents=True, exist_ok=True)


def choose_source():
    print("=" * 70)
    print("GENERIC dec&normal INDEXER")
    print("=" * 70)
    print()
    print("Jep folderin dec&normal ose Atg_Auto/Decompiled.")
    print()

    while True:
        value = input("Source folder: ").strip().strip('"')
        if not value:
            print("[!] Jep nje folder.")
            continue
        path = Path(value).expanduser()
        if not path.exists():
            print("[!] Folderi nuk ekziston.")
            continue
        if not path.is_dir():
            print("[!] Nuk eshte folder.")
            continue
        return path.resolve()


def sha256_file(path):
    digest = hashlib.sha256()
    try:
        with open(path, "rb") as f:
            while True:
                chunk = f.read(1024 * 1024)
                if not chunk:
                    break
                digest.update(chunk)
        return digest.hexdigest()
    except Exception:
        return None


def detect_type(path, header):
    suffix = path.suffix.lower()
    if header.startswith(b"\x7fELF"):
        return "ELF"
    if header.startswith(b"dex\n"):
        return "DEX"
    if header.startswith(b"UnityFS"):
        return "UNITYFS"
    if header.startswith(b"UnityWeb"):
        return "UNITYWEB"
    if header.startswith(b"UnityRaw"):
        return "UNITYRAW"
    if header.startswith(b"PK\x03\x04"):
        return "ZIP"
    if header.startswith(b"MZ"):
        return "PE"

    extension_types = {
        ".cs": "C_SHARP",
        ".json": "JSON",
        ".xml": "XML",
        ".txt": "TEXT",
        ".dll": "DLL",
        ".so": "SO",
        ".dex": "DEX",
        ".arsc": "ARSC",
        ".bundle": "UNITY_BUNDLE",
        ".unity3d": "UNITY_BUNDLE",
        ".assets": "UNITY_ASSETS",
        ".shader": "SHADER",
        ".prefab": "PREFAB",
        ".scene": "SCENE",
        ".bytes": "BYTES",
        ".bin": "BINARY",
    }
    return extension_types.get(suffix, "UNKNOWN")


def extract_ascii_strings(data):
    pattern = rb"[\x20-\x7e]{%d,}" % MIN_STRING_LENGTH
    result = []
    try:
        for match in re.finditer(pattern, data):
            result.append(match.group(0).decode("utf-8", errors="replace"))
    except Exception:
        pass
    return result


def extract_utf16_strings(data):
    pattern = rb"(?:[\x20-\x7e]\x00){%d,}" % MIN_STRING_LENGTH
    result = []
    try:
        for match in re.finditer(pattern, data):
            try:
                result.append(match.group(0).decode("utf-8", errors="replace"))
            except Exception:
                pass
    except Exception:
        pass
    return result


def looks_like_text(data):
    if not data:
        return False
    sample = data[:100000]
    nulls = sample.count(b"\x00")
    if nulls > len(sample) * 0.05:
        return False
    printable = sum(1 for byte in sample if byte in (9, 10, 13) or 32 <= byte <= 126)
    ratio = printable / max(1, len(sample))
    return ratio > 0.80


def extract_numeric_relations(text):
    records = []
    patterns = [
        r"\b([A-Za-z_][A-Za-z0-9_]*)\s*=\s*(-?\d{1,9})\b",
        r"\b([A-Za-z_][A-Za-z0-9_]*)\s*:\s*(-?\d{1,9})\b",
        r"\b(?:id|ID|tag|Tag|msg|MSG|message|Message)\s*=\s*(-?\d{1,9})\b"
    ]
    for pattern in patterns:
        for match in re.finditer(pattern, text):
            try:
                if match.lastindex == 2:
                    name = match.group(1)
                    value = int(match.group(2))
                else:
                    name = None
                    value = int(match.group(1))
            except Exception:
                continue
            start = max(0, match.start() - 500)
            end = min(len(text), match.end() + 1000)
            records.append({
                "name": name,
                "value": value,
                "context": text[start:end]
            })
    return records


def extract_sproto_source(text):
    result = {"types": {}, "protocols": {}}
    if not text:
        return result

    clean = re.sub(r"//.*?$|#.*?$", "", text, flags=re.MULTILINE)

    block_pattern = re.compile(
        r"(?:^|\n)\s*\.?([A-Za-z_][A-Za-z0-9_.]*)\s*\{",
        re.MULTILINE
    )
    starts = list(block_pattern.finditer(clean))

    for i, match in enumerate(starts):
        name = match.group(1)
        start = match.end()
        depth = 1
        pos = start

        while pos < len(clean) and depth:
            if clean[pos] == "{":
                depth += 1
            elif clean[pos] == "}":
                depth -= 1
            pos += 1

        if depth:
            continue

        body = clean[start:pos - 1]
        fields = []
        field_pattern = re.compile(
            r"^\s*([A-Za-z_][A-Za-z0-9_.]*)\s+"
            r"(-?\d+)\s*:\s*"
            r"(\*?[A-Za-z_][A-Za-z0-9_.]*(?:\([^)]*\))?)",
            re.MULTILINE
        )

        for fm in field_pattern.finditer(body):
            fields.append({
                "name": fm.group(1),
                "tag": int(fm.group(2)),
                "type": fm.group(3)
            })

        if fields:
            result["types"][name] = {
                "name": name,
                "fields": sorted(fields, key=lambda x: x["tag"])
            }

    protocol_pattern = re.compile(
        r"(?:^|\n)\s*([A-Za-z_][A-Za-z0-9_.]*)\s+(-?\d+)\s*\{",
        re.MULTILINE
    )

    for match in protocol_pattern.finditer(clean):
        name = match.group(1)
        tag = int(match.group(2))
        start = match.end()
        depth = 1
        pos = start

        while pos < len(clean) and depth:
            if clean[pos] == "{":
                depth += 1
            elif clean[pos] == "}":
                depth -= 1
            pos += 1

        if depth:
            continue

        body = clean[start:pos - 1]
        req = re.search(r"\brequest\s+([A-Za-z_][A-Za-z0-9_.]*)", body)
        resp = re.search(r"\bresponse\s+([A-Za-z_][A-Za-z0-9_.]*)", body)

        result["protocols"][str(tag)] = {
            "name": name,
            "tag": tag,
            "request": req.group(1) if req else None,
            "response": resp.group(1) if resp else None
        }

    return result


def analyze_file(path, source):
    result = {
        "path": str(path),
        "relative_path": str(path.relative_to(source)),
        "name": path.name,
        "extension": path.suffix.lower(),
        "size": 0,
        "sha256": None,
        "type": "UNKNOWN",
        "metadata": {},
        "strings_file": None,
        "text_file": None,
        "protocol_candidates": []
    }

    try:
        result["size"] = path.stat().st_size
    except Exception:
        return result

    try:
        result["sha256"] = sha256_file(path)
        with open(path, "rb") as f:
            header = f.read(4096)
        result["type"] = detect_type(path, header)
    except Exception as error:
        result["metadata"]["error"] = str(error)
        return result

    text = None
    data = None

    if result["extension"] in (".cs", ".json", ".xml", ".txt", ".shader", ".prefab", ".scene"):
        try:
            text = path.read_text(encoding="utf-8", errors="replace")
        except Exception:
            text = None

    if text is None and result["size"] <= MAX_BINARY_SCAN:
        try:
            with open(path, "rb") as f:
                data = f.read(MAX_BINARY_SCAN)
            if looks_like_text(data):
                text = data.decode("utf-8", errors="replace")
        except Exception:
            pass

    if text is not None:
        result["protocol_candidates"] = extract_numeric_relations(text)
        result["metadata"]["sproto"] = extract_sproto_source(text)

    return result


def main():
    source = choose_source()
    print(f"\n[+] Source: {source}\n")

    files = [item for item in source.rglob("*") if item.is_file()]
    files.sort(key=lambda item: str(item).lower())
    print(f"[+] Found {len(files)} files.\n")

    inventory = []
    type_counts = defaultdict(int)
    numeric_index = defaultdict(list)
    sproto_types = {}
    sproto_protocols = {}

    for index, path in enumerate(files, start=1):
        print(f"[{index}/{len(files)}] {path.relative_to(source)}")
        try:
            record = analyze_file(path, source)
        except Exception as error:
            record = {
                "path": str(path),
                "relative_path": str(path.relative_to(source)),
                "name": path.name,
                "extension": path.suffix.lower(),
                "size": 0,
                "sha256": None,
                "type": "ERROR",
                "metadata": {"error": str(error)},
                "protocol_candidates": []
            }

        inventory.append(record)
        type_counts[record["type"]] += 1

        for relation in record.get("protocol_candidates", []):
            val = relation.get("value")
            if val is not None:
                numeric_index[str(val)].append({
                    "source": record["relative_path"],
                    "name": relation.get("name"),
                    "context": relation.get("context", "")
                })

        sproto = record.get("metadata", {}).get("sproto", {})
        for name, item in sproto.get("types", {}).items():
            sproto_types[name] = item
        for tag, item in sproto.get("protocols", {}).items():
            sproto_protocols[tag] = item

    # Save output JSON files
    with open(OUTPUT / "index.json", "w", encoding="utf-8") as f:
        json.dump({"files": inventory}, f, indent=2, ensure_ascii=False)

    with open(OUTPUT / "protocol" / "numeric_relations.json", "w", encoding="utf-8") as f:
        json.dump(dict(numeric_index), f, indent=2, ensure_ascii=False)

    with open(OUTPUT / "protocol" / "sproto_types.json", "w", encoding="utf-8") as f:
        json.dump(sproto_types, f, indent=2, ensure_ascii=False)

    with open(OUTPUT / "protocol" / "sproto_protocols.json", "w", encoding="utf-8") as f:
        json.dump(sproto_protocols, f, indent=2, ensure_ascii=False)

    print("\n" + "=" * 70)
    print("INDEX COMPLETE")
    print("=" * 70)
    print(f"Files Processed   : {len(inventory)}")
    print(f"Sproto Protocols  : {len(sproto_protocols)}")
    print(f"Sproto Types      : {len(sproto_types)}")
    print(f"Index Output      : {OUTPUT / 'index.json'}\n")


if __name__ == "__main__":
    main()
