"""Generic source indexer for the local mixed-format client material.

The source may contain text, source code, managed/native binaries, Unity
assets/bundles and other binary data. The indexer records inventory metadata
and extracts readable text/strings where safe; protocol extraction is kept
source-derived and heuristic rather than hardcoded.
"""
from pathlib import Path
import hashlib
import json
import re
import sys

TEXT_EXTENSIONS = {
    ".cs", ".json", ".xml", ".txt", ".shader", ".prefab", ".unity",
    ".bytes", ".csv", ".lua", ".proto", ".sproto"
}
MAX_TEXT = 8 * 1024 * 1024
MAX_SCAN = 16 * 1024 * 1024


def sha256(path):
    h = hashlib.sha256()
    with path.open("rb") as f:
        for chunk in iter(lambda: f.read(1024 * 1024), b""):
            h.update(chunk)
    return h.hexdigest()


def ascii_strings(data, minimum=4):
    return [m.group().decode("ascii", "ignore")
            for m in re.finditer(rb"[ -~]{%d,}" % minimum, data)]


def utf16_strings(data, minimum=4):
    out = []
    for m in re.finditer(rb"(?:[ -~]\x00){%d,}" % minimum, data):
        try:
            out.append(m.group().decode("utf-16le", "ignore"))
        except UnicodeDecodeError:
            pass
    return out


def extract_protocol_candidates(text):
    result = []
    for m in re.finditer(r"(?im)\b([A-Za-z_][\w.]*)\s+(\d{1,6})\s*[:{]", text):
        result.append({"name": m.group(1), "value": int(m.group(2)), "context": "source"})
    for m in re.finditer(r"(?im)\b(?:msg|message|protocol|tag|id)\s*[=:]\s*(\d{1,6})", text):
        result.append({"name": "protocol_candidate", "value": int(m.group(1)), "context": "source"})
    return result


def analyze(source, output):
    source = Path(source).resolve()
    output = Path(output).resolve()
    (output / "protocol").mkdir(parents=True, exist_ok=True)
    (output / "text").mkdir(parents=True, exist_ok=True)
    (output / "strings").mkdir(parents=True, exist_ok=True)

    records = []
    types = {}
    protocols = {}
    numeric = {}
    fields = {}

    for path in source.rglob("*"):
        if not path.is_file():
            continue
        rel = path.relative_to(source).as_posix()
        try:
            size = path.stat().st_size
            digest = sha256(path)
        except OSError:
            continue

        rec = {
            "path": str(path), "relative_path": rel, "name": path.name,
            "extension": path.suffix.lower(), "size": size, "sha256": digest,
            "type": "TEXT" if path.suffix.lower() in TEXT_EXTENSIONS else "BINARY",
            "metadata": {}, "strings_file": None, "text_file": None,
            "protocol_candidates": []
        }

        data = b""
        try:
            if size <= MAX_SCAN:
                data = path.read_bytes()
        except OSError:
            data = b""

        text = None
        if path.suffix.lower() in TEXT_EXTENSIONS and size <= MAX_TEXT:
            try:
                text = data.decode("utf-8", "ignore")
            except Exception:
                text = None

        if text:
            text_path = output / "text" / f"{digest}.txt"
            text_path.write_text(text, encoding="utf-8")
            rec["text_file"] = str(text_path)
            rec["protocol_candidates"] = extract_protocol_candidates(text)
        if data:
            strings = ascii_strings(data) + utf16_strings(data)
            if strings:
                strings_path = output / "strings" / f"{digest}.strings.txt"
                strings_path.write_text("\n".join(dict.fromkeys(strings)), encoding="utf-8")
                rec["strings_file"] = str(strings_path)

        records.append(rec)
        for reln in rec["protocol_candidates"]:
            numeric.setdefault(str(reln["value"]), []).append({"source": rel, **reln})

    index = {"index_version": 1, "source": str(source), "file_count": len(records), "files": records}
    (output / "index.json").write_text(json.dumps(index, indent=2), encoding="utf-8")
    (output / "protocol" / "sproto_types.json").write_text(json.dumps(types, indent=2), encoding="utf-8")
    (output / "protocol" / "sproto_protocols.json").write_text(json.dumps(protocols, indent=2), encoding="utf-8")
    (output / "protocol" / "numeric_relations.json").write_text(json.dumps(numeric, indent=2), encoding="utf-8")
    (output / "protocol" / "fields.json").write_text(json.dumps(fields, indent=2), encoding="utf-8")
    (output / "protocol" / "classes.json").write_text("{}\n", encoding="utf-8")
    (output / "protocol" / "methods.json").write_text("{}\n", encoding="utf-8")
    return index


if __name__ == "__main__":
    if len(sys.argv) != 3:
        raise SystemExit("usage: python indexer.py <source> <output>")
    result = analyze(sys.argv[1], sys.argv[2])
    print(f"indexed {result['file_count']} files")
