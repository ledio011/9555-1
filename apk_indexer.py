import os
import re
import json
import zipfile
import hashlib
import struct
from pathlib import Path

APK = Path("ATG.apk")
OUT = Path("apk_index")

TEXT_DIR = OUT / "text"
STRINGS_DIR = OUT / "strings"

OUT.mkdir(exist_ok=True)
TEXT_DIR.mkdir(exist_ok=True)
STRINGS_DIR.mkdir(exist_ok=True)


def sha256(data):
    return hashlib.sha256(data).hexdigest()


def printable_strings(data, min_len=5):
    pattern = rb"[\x20-\x7e]{%d,}" % min_len
    return [x.decode("utf-8", errors="ignore") for x in re.findall(pattern, data)]


def detect_type(name, data):
    lower = name.lower()

    if data.startswith(b"UnityFS"):
        return "UnityFS"

    if data.startswith(b"PK\x03\x04"):
        return "ZIP"

    if data.startswith(b"dex\n"):
        return "DEX"

    if data.startswith(b"\x7fELF"):
        return "ELF"

    if data.startswith(b"\x1f\x8b"):
        return "GZIP"

    if lower.endswith(".json"):
        return "JSON"

    if lower.endswith(".xml"):
        return "XML"

    if lower.endswith(".txt"):
        return "TEXT"

    if lower.endswith(".bytes"):
        return "BYTES"

    if lower.endswith(".bundle"):
        return "BUNDLE"

    if lower.endswith(".so"):
        return "SO"

    if lower.endswith(".dll"):
        return "DLL"

    if lower.endswith(".apk"):
        return "APK"

    return "UNKNOWN"


def safe_name(name):
    name = name.replace("\\", "_").replace("/", "_")
    name = re.sub(r"[^a-zA-Z0-9_.-]", "_", name)
    return name[:180]


def save_text_if_possible(name, data):
    try:
        text = data.decode("utf-8")
    except UnicodeDecodeError:
        try:
            text = data.decode("utf-16")
        except UnicodeDecodeError:
            return None

    if not text.strip():
        return None

    filename = safe_name(name) + ".txt"
    path = TEXT_DIR / filename

    try:
        path.write_text(text, encoding="utf-8")
    except Exception:
        return None

    return str(path)


def save_strings(name, data):
    strings = printable_strings(data)

    if not strings:
        return None, 0

    filename = safe_name(name) + ".strings.txt"
    path = STRINGS_DIR / filename

    try:
        path.write_text(
            "\n".join(strings),
            encoding="utf-8",
            errors="ignore"
        )
    except Exception:
        return None, 0

    return str(path), len(strings)


def main():

    if not APK.exists():
        print(f"[ERROR] APK not found: {APK}")
        return

    print("=" * 70)
    print("ATG APK INDEXER")
    print("=" * 70)
    print(f"[APK] {APK.resolve()}")
    print()

    index = {
        "apk": str(APK.resolve()),
        "size": APK.stat().st_size,
        "sha256": None,
        "files": []
    }

    apk_hash = hashlib.sha256()

    with APK.open("rb") as f:
        while True:
            chunk = f.read(1024 * 1024)
            if not chunk:
                break
            apk_hash.update(chunk)

    index["sha256"] = apk_hash.hexdigest()

    print(f"[SHA256] {index['sha256']}")
    print()

    with zipfile.ZipFile(APK, "r") as z:

        names = z.namelist()

        print(f"[FILES] {len(names)}")
        print()

        for i, name in enumerate(names, 1):

            try:
                info = z.getinfo(name)

                if info.is_dir():
                    continue

                data = z.read(name)

                file_type = detect_type(name, data)

                entry = {
                    "path": name,
                    "size": len(data),
                    "compressed_size": info.compress_size,
                    "type": file_type,
                    "sha256": sha256(data),
                }

                text_path = None
                strings_path = None
                string_count = 0

                # Small text/config files
                if len(data) <= 10 * 1024 * 1024:
                    text_path = save_text_if_possible(name, data)

                # Binary strings
                if file_type in (
                    "DEX",
                    "ELF",
                    "SO",
                    "DLL",
                    "UnityFS",
                    "BUNDLE",
                    "BYTES",
                    "UNKNOWN"
                ):
                    strings_path, string_count = save_strings(name, data)

                if text_path:
                    entry["text_extract"] = text_path

                if strings_path:
                    entry["strings_extract"] = strings_path
                    entry["string_count"] = string_count

                index["files"].append(entry)

                if i % 100 == 0:
                    print(f"[{i}/{len(names)}] indexed")

            except Exception as e:
                index["files"].append({
                    "path": name,
                    "error": str(e)
                })

    # Useful grouped index
    grouped = {}

    for entry in index["files"]:
        typ = entry.get("type", "UNKNOWN")
        grouped.setdefault(typ, []).append(entry["path"])

    index["summary"] = {
        "total_files": len(index["files"]),
        "types": {
            k: len(v)
            for k, v in grouped.items()
        }
    }

    index["groups"] = grouped

    output = OUT / "index.json"

    output.write_text(
        json.dumps(index, indent=2, ensure_ascii=False),
        encoding="utf-8"
    )

    print()
    print("=" * 70)
    print("INDEX COMPLETE")
    print("=" * 70)
    print(f"[+] Index: {output}")
    print(f"[+] Text:  {TEXT_DIR}")
    print(f"[+] Strings: {STRINGS_DIR}")
    print()

    for typ, files in sorted(grouped.items()):
        print(f"{typ:10} {len(files)}")

    print()
    print("[+] Done.")


if __name__ == "__main__":
    main()
