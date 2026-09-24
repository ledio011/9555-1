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


# ============================================================
# SOURCE
# ============================================================

def choose_source():

    print("=" * 70)
    print("GENERIC dec&normal INDEXER")
    print("=" * 70)
    print()
    print("Jep folderin dec&normal.")
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


# ============================================================
# HASH
# ============================================================

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


# ============================================================
# TYPE DETECTION
# ============================================================

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

    if header.startswith(b"\x89PNG\r\n\x1a\n"):
        return "PNG"

    if header.startswith(b"\xff\xd8\xff"):
        return "JPEG"

    if header.startswith(b"\x1f\x8b"):
        return "GZIP"

    if header.startswith(b"SQLite format 3"):
        return "SQLITE"

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

    return extension_types.get(
        suffix,
        "UNKNOWN"
    )


# ============================================================
# STRINGS
# ============================================================

def extract_ascii_strings(data):

    pattern = rb"[\x20-\x7e]{%d,}" % MIN_STRING_LENGTH

    result = []

    try:

        for match in re.finditer(
            pattern,
            data
        ):

            result.append(
                match.group(0).decode(
                    "utf-8",
                    errors="replace"
                )
            )

    except Exception:
        pass

    return result


def extract_utf16_strings(data):

    pattern = rb"(?:[\x20-\x7e]\x00){%d,}" % MIN_STRING_LENGTH

    result = []

    try:

        for match in re.finditer(
            pattern,
            data
        ):

            try:

                result.append(
                    match.group(0).decode(
                        "utf-16le",
                        errors="replace"
                    )
                )

            except Exception:
                pass

    except Exception:
        pass

    return result


# ============================================================
# TEXT DETECTION
# ============================================================

def looks_like_text(data):

    if not data:
        return False

    sample = data[:100000]

    nulls = sample.count(
        b"\x00"
    )

    if nulls > len(sample) * 0.05:
        return False

    printable = sum(
        1
        for byte in sample
        if byte in (9, 10, 13)
        or 32 <= byte <= 126
    )

    ratio = printable / max(
        1,
        len(sample)
    )

    return ratio > 0.80


# ============================================================
# DEX
# ============================================================

def parse_dex_header(data):

    result = {}

    if len(data) < 112:
        return result

    if not data.startswith(b"dex\n"):
        return result

    try:

        result["magic"] = data[:8].decode(
            "ascii",
            errors="replace"
        )

        result["checksum"] = data[
            8:12
        ].hex()

        result["signature"] = data[
            12:32
        ].hex()

        result["file_size"] = struct.unpack_from(
            "<I",
            data,
            0x20
        )[0]

        result["header_size"] = struct.unpack_from(
            "<I",
            data,
            0x24
        )[0]

        result["endian_tag"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x28
            )[0]
        )

        result["string_ids_size"] = struct.unpack_from(
            "<I",
            data,
            0x38
        )[0]

        result["string_ids_offset"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x3C
            )[0]
        )

        result["type_ids_size"] = struct.unpack_from(
            "<I",
            data,
            0x40
        )[0]

        result["type_ids_offset"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x44
            )[0]
        )

        result["proto_ids_size"] = struct.unpack_from(
            "<I",
            data,
            0x48
        )[0]

        result["proto_ids_offset"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x4C
            )[0]
        )

        result["field_ids_size"] = struct.unpack_from(
            "<I",
            data,
            0x50
        )[0]

        result["field_ids_offset"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x54
            )[0]
        )

        result["method_ids_size"] = struct.unpack_from(
            "<I",
            data,
            0x58
        )[0]

        result["method_ids_offset"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x5C
            )[0]
        )

        result["class_defs_size"] = struct.unpack_from(
            "<I",
            data,
            0x60
        )[0]

        result["class_defs_offset"] = hex(
            struct.unpack_from(
                "<I",
                data,
                0x64
            )[0]
        )

    except Exception:
        pass

    return result


# ============================================================
# ELF
# ============================================================

def parse_elf_header(data):

    result = {}

    if len(data) < 64:
        return result

    if not data.startswith(b"\x7fELF"):
        return result

    try:

        elf_class = data[4]
        endian = data[5]

        result["class"] = {
            1: "ELF32",
            2: "ELF64"
        }.get(
            elf_class,
            str(elf_class)
        )

        result["endianness"] = {
            1: "little",
            2: "big"
        }.get(
            endian,
            str(endian)
        )

        prefix = "<" if endian == 1 else ">"

        result["type"] = struct.unpack_from(
            prefix + "H",
            data,
            16
        )[0]

        result["machine"] = struct.unpack_from(
            prefix + "H",
            data,
            18
        )[0]

        if elf_class == 1:

            result["entry"] = hex(
                struct.unpack_from(
                    prefix + "I",
                    data,
                    24
                )[0]
            )

        elif elf_class == 2:

            result["entry"] = hex(
                struct.unpack_from(
                    prefix + "Q",
                    data,
                    24
                )[0]
            )

    except Exception:
        pass

    return result


# ============================================================
# UNITY
# ============================================================

def parse_unity_header(data):

    result = {}

    if not (
        data.startswith(b"UnityFS")
        or data.startswith(b"UnityWeb")
        or data.startswith(b"UnityRaw")
    ):
        return result

    try:

        parts = data.split(
            b"\n",
            4
        )

        if len(parts) > 0:

            result["signature"] = parts[
                0
            ].decode(
                "ascii",
                errors="replace"
            )

        if len(parts) > 1:

            result["format"] = parts[
                1
            ].decode(
                "ascii",
                errors="replace"
            )

        if len(parts) > 2:

            result["unity_version"] = parts[
                2
            ].decode(
                "utf-8",
                errors="replace"
            )

        if len(parts) > 3:

            result["revision"] = parts[
                3
            ].decode(
                "utf-8",
                errors="replace"
            )

    except Exception:
        pass

    return result


# ============================================================
# C# ANALYSIS
# ============================================================

def analyze_csharp(text):

    result = {
        "classes": [],
        "interfaces": [],
        "enums": [],
        "structs": [],
        "methods": [],
        "fields": [],
        "attributes": []
    }

    for match in re.finditer(
        r"\bclass\s+([A-Za-z_][A-Za-z0-9_]*)",
        text
    ):

        result["classes"].append(
            match.group(1)
        )

    for match in re.finditer(
        r"\binterface\s+([A-Za-z_][A-Za-z0-9_]*)",
        text
    ):

        result["interfaces"].append(
            match.group(1)
        )

    for match in re.finditer(
        r"\benum\s+([A-Za-z_][A-Za-z0-9_]*)",
        text
    ):

        result["enums"].append(
            match.group(1)
        )

    for match in re.finditer(
        r"\bstruct\s+([A-Za-z_][A-Za-z0-9_]*)",
        text
    ):

        result["structs"].append(
            match.group(1)
        )

    method_pattern = re.compile(
        r"""
        (?:
            public|private|protected|internal|
            static|virtual|override|abstract|
            async|sealed|extern
        )*
        \s*
        [A-Za-z_][A-Za-z0-9_<>\[\],.?]*
        \s+
        ([A-Za-z_][A-Za-z0-9_]*)
        \s*
        \(
        ([^)]*)
        \)
        """,
        re.VERBOSE
    )

    for match in method_pattern.finditer(text):

        result["methods"].append({
            "name": match.group(1),
            "arguments": match.group(2)
        })

    field_pattern = re.compile(
        r"""
        (?:
            public|private|protected|internal|
            static|readonly|const|volatile
        )*
        \s*
        ([A-Za-z_][A-Za-z0-9_<>\[\],.?]*)
        \s+
        ([A-Za-z_][A-Za-z0-9_]*)
        \s*
        ;
        """,
        re.VERBOSE
    )

    for match in field_pattern.finditer(text):

        result["fields"].append({
            "type": match.group(1),
            "name": match.group(2)
        })

    for match in re.finditer(
        r"\[([A-Za-z_][A-Za-z0-9_.]*)",
        text
    ):

        result["attributes"].append(
            match.group(1)
        )

    result["classes"] = sorted(
        set(result["classes"])
    )

    result["interfaces"] = sorted(
        set(result["interfaces"])
    )

    result["enums"] = sorted(
        set(result["enums"])
    )

    result["structs"] = sorted(
        set(result["structs"])
    )

    result["attributes"] = sorted(
        set(result["attributes"])
    )

    unique_methods = []
    seen_methods = set()

    for item in result["methods"]:

        key = (
            item["name"],
            item["arguments"]
        )

        if key not in seen_methods:

            seen_methods.add(key)
            unique_methods.append(item)

    result["methods"] = unique_methods

    unique_fields = []
    seen_fields = set()

    for item in result["fields"]:

        key = (
            item["type"],
            item["name"]
        )

        if key not in seen_fields:

            seen_fields.add(key)
            unique_fields.append(item)

    result["fields"] = unique_fields

    return result


# ============================================================
# NUMERIC RELATIONS
# ============================================================

def extract_numeric_relations(text):

    records = []

    patterns = [

        r"\b([A-Za-z_][A-Za-z0-9_]*)\s*=\s*(-?\d{1,9})\b",

        r"\b([A-Za-z_][A-Za-z0-9_]*)\s*:\s*(-?\d{1,9})\b",

        r"\b(?:id|ID|tag|Tag|msg|MSG|message|Message)"
        r"\s*=\s*(-?\d{1,9})\b"
    ]

    for pattern in patterns:

        for match in re.finditer(
            pattern,
            text
        ):

            try:

                if match.lastindex == 2:

                    name = match.group(1)
                    value = int(
                        match.group(2)
                    )

                else:

                    name = None
                    value = int(
                        match.group(1)
                    )

            except Exception:
                continue

            start = max(
                0,
                match.start() - 500
            )

            end = min(
                len(text),
                match.end() + 1000
            )

            records.append({
                "name": name,
                "value": value,
                "context": text[start:end]
            })

    return records


# ============================================================
# FILE ANALYSIS
# ============================================================

def analyze_file(path, source):

    result = {

        "path": str(path),

        "relative_path": str(
            path.relative_to(source)
        ),

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

        result["sha256"] = sha256_file(
            path
        )

        with open(path, "rb") as f:

            header = f.read(4096)

        result["type"] = detect_type(
            path,
            header
        )

    except Exception as error:

        result["metadata"]["error"] = str(
            error
        )

        return result

    if result["type"] == "DEX":

        try:

            with open(path, "rb") as f:
                data = f.read()

            result["metadata"]["dex"] = parse_dex_header(
                data
            )

        except Exception:
            pass

    elif result["type"] == "ELF":

        result["metadata"]["elf"] = parse_elf_header(
            header
        )

    elif result["type"] in (
        "UNITYFS",
        "UNITYWEB",
        "UNITYRAW",
        "UNITY_BUNDLE"
    ):

        result["metadata"]["unity"] = parse_unity_header(
            header
        )

    text = None
    data = None

    textual = result["type"] in {
        "C_SHARP",
        "JSON",
        "XML",
        "TEXT",
        "SHADER",
        "PREFAB",
        "SCENE"
    }

    if textual and result["size"] <= MAX_TEXT_SIZE:

        try:

            text = path.read_text(
                encoding="utf-8",
                errors="replace"
            )

        except Exception:
            text = None

    if text is None and result["size"] <= MAX_BINARY_SCAN:

        try:

            with open(path, "rb") as f:

                data = f.read(
                    MAX_BINARY_SCAN
                )

            if looks_like_text(data):

                text = data.decode(
                    "utf-8",
                    errors="replace"
                )

        except Exception:
            pass

    if data is None and result["size"] <= MAX_BINARY_SCAN:

        try:

            with open(path, "rb") as f:

                data = f.read(
                    MAX_BINARY_SCAN
                )

        except Exception:
            data = None

    if data is not None:

        ascii_strings = extract_ascii_strings(
            data
        )

        utf16_strings = extract_utf16_strings(
            data
        )

        all_strings = sorted(
            set(
                ascii_strings
                + utf16_strings
            )
        )

        if all_strings:

            output_name = (
                hashlib.sha1(
                    str(
                        path.relative_to(source)
                    ).encode(
                        "utf-8",
                        errors="ignore"
                    )
                ).hexdigest()
                + ".strings.txt"
            )

            output_path = (
                OUTPUT
                / "strings"
                / output_name
            )

            with open(
                output_path,
                "w",
                encoding="utf-8",
                errors="replace"
            ) as f:

                for value in all_strings:

                    f.write(value)
                    f.write("\n")

            result["strings_file"] = str(
                output_path
            )

    if text is not None:

        output_name = (
            hashlib.sha1(
                str(
                    path.relative_to(source)
                ).encode(
                    "utf-8",
                    errors="ignore"
                )
            ).hexdigest()
            + ".txt"
        )

        output_path = (
            OUTPUT
            / "text"
            / output_name
        )

        with open(
            output_path,
            "w",
            encoding="utf-8",
            errors="replace"
        ) as f:

            f.write(text)

        result["text_file"] = str(
            output_path
        )

        result["protocol_candidates"] = (
            extract_numeric_relations(text)
        )

        if result["type"] == "C_SHARP":

            result["metadata"]["csharp"] = (
                analyze_csharp(text)
            )

    return result


# ============================================================
# MAIN
# ============================================================

def main():

    source = choose_source()

    print()
    print("[+] Source:")
    print(f"    {source}")
    print()

    files = [
        item
        for item in source.rglob("*")
        if item.is_file()
    ]

    files.sort(
        key=lambda item:
        str(item).lower()
    )

    print(
        f"[+] Found {len(files)} files."
    )

    print()

    inventory = []

    type_counts = defaultdict(int)

    numeric_index = defaultdict(list)

    class_index = defaultdict(list)

    method_index = defaultdict(list)

    field_index = defaultdict(list)

    for index, path in enumerate(
        files,
        start=1
    ):

        print(
            f"[{index}/{len(files)}] "
            f"{path.relative_to(source)}"
        )

        try:

            record = analyze_file(
                path,
                source
            )

        except Exception as error:

            record = {

                "path": str(path),

                "relative_path": str(
                    path.relative_to(source)
                ),

                "name": path.name,

                "extension": path.suffix.lower(),

                "size": 0,

                "sha256": None,

                "type": "ERROR",

                "metadata": {
                    "error": str(error)
                },

                "strings_file": None,

                "text_file": None,

                "protocol_candidates": []
            }

        inventory.append(record)

        type_counts[
            record["type"]
        ] += 1

        for relation in record.get(
            "protocol_candidates",
            []
        ):

            value = relation.get(
                "value"
            )

            if value is None:
                continue

            numeric_index[
                str(value)
            ].append({

                "source":
                    record["relative_path"],

                "name":
                    relation.get("name"),

                "context":
                    relation.get(
                        "context",
                        ""
                    )
            })

        csharp = (
            record
            .get("metadata", {})
            .get("csharp")
        )

        if csharp:

            for name in csharp.get(
                "classes",
                []
            ):

                class_index[
                    name
                ].append(
                    record["relative_path"]
                )

            for method in csharp.get(
                "methods",
                []
            ):

                name = method.get(
                    "name"
                )

                if name:

                    method_index[
                        name
                    ].append(
                        record["relative_path"]
                    )

            for field in csharp.get(
                "fields",
                []
            ):

                name = field.get(
                    "name"
                )

                if name:

                    field_index[
                        name
                    ].append(
                        record["relative_path"]
                    )

    class_index = {
        key: sorted(set(value))
        for key, value
        in class_index.items()
    }

    method_index = {
        key: sorted(set(value))
        for key, value
        in method_index.items()
    }

    field_index = {
        key: sorted(set(value))
        for key, value
        in field_index.items()
    }

    # ========================================================
    # MASTER INDEX
    # ========================================================

    master = {

        "index_version": 2,

        "source": str(source),

        "file_count": len(
            inventory
        ),

        "types": dict(
            sorted(
                type_counts.items()
            )
        ),

        "files": inventory
    }

    with open(
        OUTPUT / "index.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            master,
            f,
            indent=2,
            ensure_ascii=False
        )

    with open(
        OUTPUT
        / "protocol"
        / "numeric_relations.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            dict(numeric_index),
            f,
            indent=2,
            ensure_ascii=False
        )

    with open(
        OUTPUT
        / "protocol"
        / "classes.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            class_index,
            f,
            indent=2,
            ensure_ascii=False
        )

    with open(
        OUTPUT
        / "protocol"
        / "methods.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            method_index,
            f,
            indent=2,
            ensure_ascii=False
        )

    with open(
        OUTPUT
        / "protocol"
        / "fields.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            field_index,
            f,
            indent=2,
            ensure_ascii=False
        )

    summary = {

        "source": str(source),

        "file_count": len(
            inventory
        ),

        "types": dict(
            sorted(
                type_counts.items()
            )
        ),

        "numeric_relations":
            len(numeric_index),

        "classes":
            len(class_index),

        "methods":
            len(method_index),

        "fields":
            len(field_index)
    }

    with open(
        OUTPUT
        / "metadata"
        / "summary.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            summary,
            f,
            indent=2,
            ensure_ascii=False
        )

    print()
    print("=" * 70)
    print("INDEX COMPLETE")
    print("=" * 70)
    print()

    print(
        f"Files              : "
        f"{len(inventory)}"
    )

    print(
        f"Numeric relations  : "
        f"{len(numeric_index)}"
    )

    print(
        f"Classes            : "
        f"{len(class_index)}"
    )

    print(
        f"Methods            : "
        f"{len(method_index)}"
    )

    print(
        f"Fields             : "
        f"{len(field_index)}"
    )

    print()

    print(
        f"Index: "
        f"{OUTPUT / 'index.json'}"
    )


if __name__ == "__main__":
    main()
