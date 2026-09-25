#!/usr/bin/env python3
# ATG protocol discovery + per-session auto-responder skeleton.
# Scans Decompiled/ on startup and keeps every client's state isolated.
#
# IMPORTANT:
# This does not invent valid Sproto response bodies. It learns/replays exact
# response frames when they are supplied in responses.json. Unknown requests
# are logged instead of sending a corrupt response.

import os
import re
import json
import socket
import struct
import threading
import time
from collections import defaultdict

ROOT = os.path.dirname(os.path.abspath(__file__))
DECOMPILED = os.path.join(ROOT, "Decompiled")
HOST = os.environ.get("HOST", "0.0.0.0")
PORT = int(os.environ.get("PORT", "15679"))
CATALOG_FILE = os.path.join(ROOT, "protocol_catalog.json")
RESPONSES_FILE = os.path.join(ROOT, "responses.json")
LOG_FILE = os.path.join(ROOT, "auto_responder.log")

# One completely isolated state object per TCP connection/session.
SESSIONS = {}
SESSIONS_LOCK = threading.Lock()

# Broad patterns used against the decompiled client.
TAG_PATTERNS = [
    re.compile(r"\b(?:msg|MSG|tag|Tag|type|cmd|command)\s*==\s*(\d+)"),
    re.compile(r"\b(?:msg|MSG|tag|Tag|type|cmd|command)\s*=\s*(\d+)"),
    re.compile(r"\b(?:Request|Response|Notify|Push|Handler|Send|Recv|Receive)[A-Za-z0-9_]*\s*\([^\n]*?\b(\d{1,4})\b"),
    re.compile(r"\b(?:case|Case)\s+(\d{1,4})\s*:"),
]

NAME_PATTERNS = [
    re.compile(r"\b(?:case\s+\d+\s*:|msg\s*==\s*\d+).*?(?:Request|Response|Notify|Push|Handler|Send|Recv|Receive)[A-Za-z0-9_]*", re.I),
]

KEYWORDS = (
    "request", "response", "push", "notify", "handler", "send", "recv",
    "receive", "callback", "event", "ack", "error", "message", "msg",
    "sproto", "protocol", "pack", "unpack"
)

def log(line):
    stamp = time.strftime("%Y-%m-%d %H:%M:%S")
    out = f"[{stamp}] {line}"
    print(out, flush=True)
    with open(LOG_FILE, "a", encoding="utf-8") as f:
        f.write(out + "\n")

def scan_decompiled():
    catalog = defaultdict(lambda: {
        "tags": set(), "files": set(), "evidence": [], "keywords": set()
    })

    if not os.path.isdir(DECOMPILED):
        log(f"[SCAN] Decompiled not found: {DECOMPILED}")
        return {}

    for base, _, files in os.walk(DECOMPILED):
        for fn in files:
            if not fn.endswith((".cs", ".txt", ".json")):
                continue
            path = os.path.join(base, fn)
            try:
                text = open(path, "r", encoding="utf-8", errors="ignore").read()
            except Exception:
                continue

            lower = text.lower()
            hits = set()
            for pat in TAG_PATTERNS:
                for m in pat.finditer(text):
                    try:
                        n = int(m.group(1))
                        if 0 <= n <= 65535:
                            hits.add(n)
                    except Exception:
                        pass

            for tag in hits:
                item = catalog[str(tag)]
                item["tags"].add(tag)
                rel = os.path.relpath(path, DECOMPILED)
                item["files"].add(rel)

                for line in text.splitlines():
                    ll = line.lower()
                    if any(k in ll for k in KEYWORDS) and str(tag) in line:
                        item["evidence"].add(line.strip()[:500])
                        for k in KEYWORDS:
                            if k in ll:
                                item["keywords"].add(k)

    out = {}
    for tag, item in catalog.items():
        out[tag] = {
            "tag": int(tag),
            "files": sorted(item["files"]),
            "keywords": sorted(item["keywords"]),
            "evidence": sorted(item["evidence"])[:100],
        }

    with open(CATALOG_FILE, "w", encoding="utf-8") as f:
        json.dump({
            "generated_at": time.strftime("%Y-%m-%d %H:%M:%S"),
            "source": "Decompiled",
            "tags": out,
        }, f, ensure_ascii=False, indent=2)

    log(f"[SCAN] discovered {len(out)} candidate tags -> {CATALOG_FILE}")
    return out

def load_replay():
    if not os.path.exists(RESPONSES_FILE):
        return {}
    try:
        with open(RESPONSES_FILE, "r", encoding="utf-8") as f:
            return json.load(f)
    except Exception as e:
        log(f"[RESPONSES] invalid responses.json: {e}")
        return {}

def read_frames(sock):
    while True:
        head = recv_exact(sock, 2)
        if not head:
            return
        size = struct.unpack(">H", head)[0]
        if size == 0:
            continue
        payload = recv_exact(sock, size)
        if payload is None:
            return
        yield payload

def recv_exact(sock, n):
    buf = bytearray()
    while len(buf) < n:
        chunk = sock.recv(n - len(buf))
        if not chunk:
            return None
        buf.extend(chunk)
    return bytes(buf)

def send_frame(sock, payload):
    if len(payload) > 65535:
        raise ValueError("frame too large")
    sock.sendall(struct.pack(">H", len(payload)) + payload)

def session_snapshot(sid):
    with SESSIONS_LOCK:
        s = SESSIONS.get(sid)
        if not s:
            return {}
        return {
            "session_id": sid,
            "requests": list(s["requests"]),
            "responses_sent": list(s["responses_sent"]),
            "pushes_seen": list(s["pushes_seen"]),
            "state": dict(s["state"]),
        }

def detect_tag(payload):
    # Raw Sproto payload is intentionally not guessed here.
    # The catalog and replay layer can still operate once the project's
    # exact Sproto header/dispatch is mapped.
    return None

def handle_client(conn, addr, catalog, replay):
    sid = id(conn)
    with SESSIONS_LOCK:
        SESSIONS[sid] = {
            "addr": addr,
            "connected": time.time(),
            "requests": [],
            "responses_sent": [],
            "pushes_seen": [],
            "state": {},
        }

    log(f"[CONNECT] session={sid} addr={addr}")

    try:
        for payload in read_frames(conn):
            tag = detect_tag(payload)
            rec = {
                "time": time.time(),
                "tag": tag,
                "length": len(payload),
                "hex": payload.hex(),
            }

            with SESSIONS_LOCK:
                SESSIONS[sid]["requests"].append(rec)

            log(f"[RX] session={sid} tag={tag} bytes={len(payload)}")

            # Exact frame replay, keyed by tag, is safe and session-local.
            if tag is not None and str(tag) in replay:
                for hx in replay[str(tag)]:
                    response = bytes.fromhex(hx)
                    send_frame(conn, response)
                    with SESSIONS_LOCK:
                        SESSIONS[sid]["responses_sent"].append({
                            "tag": tag, "length": len(response)
                        })
                    log(f"[TX] session={sid} tag={tag} bytes={len(response)}")
            else:
                # Never broadcast and never send a guessed/corrupt Sproto body.
                log(f"[WAIT] session={sid}: no exact response template for tag={tag}")

    except Exception as e:
        log(f"[ERROR] session={sid}: {e}")
    finally:
        with SESSIONS_LOCK:
            SESSIONS.pop(sid, None)
        try:
            conn.close()
        except Exception:
            pass
        log(f"[DISCONNECT] session={sid}")

def main():
    catalog = scan_decompiled()
    replay = load_replay()

    log(f"[START] {HOST}:{PORT}")
    log(f"[START] isolated sessions enabled")
    log(f"[START] replay tags loaded: {len(replay)}")

    srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    srv.bind((HOST, PORT))
    srv.listen(128)

    while True:
        conn, addr = srv.accept()
        threading.Thread(
            target=handle_client,
            args=(conn, addr, catalog, replay),
            daemon=True
        ).start()

if __name__ == "__main__":
    main()
