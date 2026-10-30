#!/usr/bin/env python3
"""
ATG Live Protocol Monitor
-------------------------
Read-only Sproto request/response observer for the ATG game server.

Server integration:
    from atg_live_monitor import monitor_socket
    conn = monitor_socket(
        conn, decode_sproto, sproto_unpack,
        context_provider=lambda: {
            "character_id": picked_char.get("id") if picked_char else None,
            "character_name": picked_char.get("name") if picked_char else None,
            "map_id": picked_char.get("map_id") if picked_char else None,
        },
    )

This wrapper forwards socket operations unchanged. It does not alter packets,
send traffic, mutate game state, or block the game loop. Events are appended
to a local JSONL file (never committed to GitHub).
"""
from __future__ import annotations

import argparse
import json
import os
import re
import struct
import sys
import threading
import time
from collections import deque
from datetime import datetime, timezone
from pathlib import Path
from typing import Any, Callable, Dict, Optional

BASE_DIR = Path(__file__).resolve().parent
DEFAULT_LOG = Path(os.environ.get(
    "ATG_MONITOR_LOG",
    str(Path.home() / "atg-monitor" / "events.jsonl")
)).expanduser()
CATALOG_PATH = BASE_DIR / "atg_protocol_catalog.json"

KNOWN_PROTOCOLS = {
    "2": "visitor", "3": "verify", "4": "login", "7": "update_game_server",
    "100": "map_ready", "101": "move", "102": "skill_use",
    "103": "character_list", "104": "character_create", "105": "character_pick",
    "108": "leave_copy_scene", "111": "accept_damage", "112": "accept_mission",
    "113": "complete_mission", "115": "use_item", "119": "ask_pickup_item",
    "120": "chat", "121": "request_daily_mission", "124": "add_friend",
    "125": "delete_friend", "128": "local_character_attack",
    "130": "skill_level_up", "132": "relife_player", "143": "ask_shop_list",
    "144": "buy_shop_item", "145": "ask_copy_scenes_info",
    "146": "guild_create", "147": "guild_join_apply", "148": "guild_leave",
    "149": "guild_kick", "150": "guild_job_change", "151": "guild_skill_level",
    "152": "guild_request_list", "153": "guild_request_info",
    "154": "guild_approve_reserve", "165": "get_team_list",
    "167": "equip_enhance", "169": "request_guild_notice",
    "170": "request_guild_member_info", "175": "guild_donate",
    "176": "search_guild", "183": "equip_refine", "184": "equip_inherit",
    "191": "request_top_rank_list", "192": "change_skill_position",
    "193": "car_chase_result", "195": "request_guild_boss",
    "201": "world_boss", "212": "request_guild_skill",
    "218": "heart_beat", "220": "start_battle",
    "223": "change_show_type", "224": "open_item_package",
    "227": "request_dance_info", "228": "pause_participate_dance",
    "229": "use_dance", "231": "request_battle_info",
    "233": "open_guild_boss", "235": "request_mount_info",
    "241": "mount_use_color", "247": "guild_invite",
    "267": "change_mount_state", "270": "download_finish",
    "275": "play_social_dance", "281": "refresh_online_state",
    "284": "send_mail_box", "285": "request_guild_battle_info",
    "286": "enter_guild_battle", "287": "request_guild_battle_rank",
    "288": "request_guild_battle_member", "289": "set_guild_battle_member",
    "291": "request_guild_score_info", "293": "request_guild_star",
    "294": "update_guild_star", "295": "request_guild_battle_state",
    "298": "impact_npc", "301": "rename", "303": "equip_appraise",
    "306": "tutorial_finish", "307": "npc_or_combat_event",
    "310": "request_domin_info", "311": "enter_domin_pk_scene",
    "314": "use_dance_sound_box", "315": "update_guild_dance_time",
    "316": "change_skill_index", "318": "request_guild_map_domine_top",
    "319": "request_guild_map_info", "320": "request_guild_map_reward",
    "321": "gather_other_player", "322": "request_guild_capture",
    "323": "buy_car_shop", "324": "update_player_map_info",
    "503": "enter_map", "504": "main_player_create", "505": "aoi_add",
    "509": "npc_create", "510": "visual_update", "519": "sync_mission",
    "520": "accept_mission_push", "521": "complete_mission_push",
    "522": "abandon_mission_push", "523": "mission_state",
    "524": "mission_param", "540": "skills", "555": "copy_attempts",
    "608": "car_copy_result", "611": "inventory", "614": "server_time",
    "628": "guild_boss_entry_result", "629": "copy_timer",
    "652": "guild_shop_purchase_result", "654": "start_enter_game",
    "681": "team_match_state",
}

CATEGORY_TAGS = {
    "team": {165, 681},
    "gang": {143, 144, 146, 147, 148, 149, 150, 151, 152, 153, 154, 169, 170,
             173, 174, 175, 176, 195, 212, 231, 233, 247, 285, 286, 287, 288,
             289, 291, 293, 294, 295, 318, 319, 320, 322, 628, 652},
    "aoi": {101, 298, 321, 503, 504, 505, 509, 510},
    "combat": {102, 111, 128, 132, 193, 201, 220, 307, 311},
    "equip": {115, 130, 156, 167, 183, 184, 192, 223, 235, 236, 241, 267, 303, 316, 540, 611},
    "mission": {112, 113, 114, 121, 178, 268, 306, 519, 520, 521, 522, 523, 524},
    "map": {7, 100, 108, 145, 503, 614, 654},
    "reward": {119, 122, 123, 132, 156, 175, 193, 199, 320, 521, 608},
    "shop": {143, 144, 224, 323, 652},
    "social": {120, 124, 125, 126, 147, 148, 158, 159, 160, 165, 247, 281},
    "car": {193, 220, 235, 236, 241, 267, 298, 323, 608},
    "rank": {137, 191, 231, 287, 318},
    "dance": {227, 228, 229, 275, 314, 315},
}
VALID_CATEGORIES = ("all", *CATEGORY_TAGS.keys(), "other")
_SENSITIVE = re.compile(r"(password|passwd|token|secret|credential|authorization|authkey|sessionkey)", re.I)
_PENDING: Dict[tuple, dict] = {}
_PENDING_LOCK = threading.Lock()
_CONNECTION_COUNTER = 0
_CONNECTION_COUNTER_LOCK = threading.Lock()
_WRITE_LOCK = threading.Lock()


def _load_catalog() -> dict:
    try:
        with CATALOG_PATH.open("r", encoding="utf-8") as f:
            obj = json.load(f)
        return obj.get("protocols", {}) if isinstance(obj, dict) else {}
    except Exception:
        return {}


SERVER_HANDLER_COMMENTS = _load_catalog()


def protocol_name(tag: Optional[int]) -> str:
    if tag is None:
        return "unknown"
    key = str(tag)
    return KNOWN_PROTOCOLS.get(key) or SERVER_HANDLER_COMMENTS.get(key) or "unknown"


def category_for(tag: Optional[int], name: str = "") -> str:
    if tag is not None:
        for category, tags in CATEGORY_TAGS.items():
            if tag in tags:
                return category
    low = name.lower()
    for category, words in {
        "team": ("team", "match", "invite"),
        "gang": ("guild", "gang", "capture"),
        "aoi": ("aoi", "move", "npc", "map_enter"),
        "combat": ("skill", "attack", "damage", "battle", "boss", "fight"),
        "equip": ("equip", "refine", "enhance", "mount", "inventory", "item"),
        "mission": ("mission", "tutorial"),
        "map": ("map", "scene", "copy"),
        "reward": ("reward", "mail", "pickup", "relife"),
        "shop": ("shop", "buy"),
        "social": ("friend", "chat", "online", "character"),
        "car": ("car", "mount", "race"),
        "rank": ("rank", "score"),
        "dance": ("dance",),
    }.items():
        if any(word in low for word in words):
            return category
    return "other"


def _as_int(value: Any) -> Optional[int]:
    if isinstance(value, bool):
        return None
    try:
        return int(value) if value is not None else None
    except (TypeError, ValueError):
        return None


def _safe_value(value: Any, decoder: Callable, depth: int = 0) -> Any:
    if isinstance(value, bytes):
        if not value:
            return {"bytes": 0, "hex": ""}
        # Attempt to expose nested Sproto structs. Keep the original bytes too,
        # so a heuristic parse can never silently replace the source payload.
        nested = None
        if depth < 2:
            try:
                candidate = decoder(value, 0)
                if isinstance(candidate, dict) and candidate:
                    nested = {str(k): _safe_value(v, decoder, depth + 1)
                              for k, v in candidate.items()}
            except Exception:
                pass
        text_value = None
        try:
            decoded = value.decode("utf-8")
            if decoded and all(ch.isprintable() or ch in "\r\n\t" for ch in decoded):
                text_value = decoded[:512]
        except UnicodeDecodeError:
            pass
        result = {"bytes": len(value), "hex": value[:96].hex()}
        if text_value is not None:
            result["text"] = text_value
        if nested is not None:
            result["nested_fields"] = nested
        if len(value) > 96:
            result["truncated"] = True
        return result
    if isinstance(value, dict):
        return {str(k): _safe_value(v, decoder, depth + 1) for k, v in value.items()}
    if isinstance(value, (list, tuple)):
        return [_safe_value(v, decoder, depth + 1) for v in value[:100]]
    if value is None or isinstance(value, (str, int, float, bool)):
        return value
    return repr(value)[:256]


def _redact_sensitive_fields(fields: dict, tag: Optional[int]) -> dict:
    # Login/visitor/verification payloads may contain account credentials.
    if tag in (2, 3, 4):
        return {key: "<redacted-auth-field>" for key in fields}
    return fields


def _write_event(path: Path, event: dict) -> None:
    try:
        path.parent.mkdir(parents=True, exist_ok=True)
        line = json.dumps(event, ensure_ascii=False, separators=(",", ":")) + "\n"
        with _WRITE_LOCK:
            with path.open("a", encoding="utf-8") as f:
                f.write(line)
                f.flush()
    except Exception as exc:
        # Monitoring must never break the game server.
        try:
            sys.stderr.write("[ATG-MONITOR] log write failed: %s\n" % exc)
        except Exception:
            pass


def _decode_frame(frame: bytes, decoder: Callable, unpacker: Callable) -> dict:
    if len(frame) < 3:
        raise ValueError("short frame")
    declared = struct.unpack(">H", frame[:2])[0]
    if declared != len(frame) - 2:
        raise ValueError("frame length mismatch")
    raw = unpacker(frame[2:])
    header = decoder(raw, 0)
    tag = _as_int(header.get(0)) if isinstance(header, dict) else None
    session = _as_int(header.get(1)) if isinstance(header, dict) else None
    try:
        header_words = struct.unpack("<H", raw[:2])[0]
        body_offset = 2 + header_words * 2
        body = decoder(raw, body_offset) if body_offset < len(raw) else {}
    except Exception:
        body = {}
    fields = body if isinstance(body, dict) else {}
    safe_fields = {str(k): _safe_value(v, decoder) for k, v in fields.items()}
    safe_fields = _redact_sensitive_fields(safe_fields, tag)
    return {
        "tag": tag,
        "session": session,
        "fields": safe_fields,
        "frame_bytes": len(frame),
    }


class MonitoredSocket:
    """Transparent socket facade; only observes complete length-prefixed frames."""
    def __init__(self, sock, decoder: Callable, unpacker: Callable,
                 context_provider: Optional[Callable[[], dict]] = None,
                 log_path: Path = DEFAULT_LOG):
        global _CONNECTION_COUNTER
        self._sock = sock
        self._decoder = decoder
        self._unpacker = unpacker
        self._context_provider = context_provider
        self._log_path = Path(log_path)
        self._rx_buffer = bytearray()
        self._rx_lock = threading.Lock()
        self._closed = False
        with _CONNECTION_COUNTER_LOCK:
            _CONNECTION_COUNTER += 1
            self._connection_id = _CONNECTION_COUNTER
        try:
            self._peer = repr(sock.getpeername())
        except Exception:
            self._peer = "unknown"

    def __getattr__(self, name):
        return getattr(self._sock, name)

    def _context(self) -> dict:
        try:
            value = self._context_provider() if self._context_provider else {}
            if not isinstance(value, dict):
                return {}
            allowed = ("character_id", "character_name", "map_id")
            return {key: value.get(key) for key in allowed if value.get(key) is not None}
        except Exception:
            return {}

    def _emit(self, direction: str, frame: bytes) -> None:
        try:
            decoded = _decode_frame(frame, self._decoder, self._unpacker)
            tag = decoded["tag"]
            session = decoded["session"]
            name = protocol_name(tag)
            kind = "request" if direction == "C2S" and tag is not None else (
                "push" if direction == "S2C" and tag is not None else
                "response" if session is not None else "unclassified"
            )
            matched_request = None
            if kind == "request" and session is not None:
                with _PENDING_LOCK:
                    _PENDING[(self._connection_id, session)] = {
                        "tag": tag, "name": name, "started": time.monotonic()
                    }
            elif kind == "response" and session is not None:
                with _PENDING_LOCK:
                    matched_request = _PENDING.pop((self._connection_id, session), None)
            event = {
                "ts": datetime.now(timezone.utc).isoformat(timespec="milliseconds"),
                "monotonic": time.monotonic(),
                "direction": direction,
                "kind": kind,
                "category": category_for(tag, name),
                "tag": tag,
                "protocol": name,
                "session": session,
                "connection_id": self._connection_id,
                "peer": self._peer,
                **self._context(),
                "fields": decoded["fields"],
                "frame_bytes": decoded["frame_bytes"],
            }
            if kind == "response":
                if matched_request:
                    event["response_to"] = {
                        "tag": matched_request["tag"],
                        "protocol": matched_request["name"],
                        "latency_ms": round((time.monotonic() - matched_request["started"]) * 1000, 2),
                        "status": "MATCHED",
                    }
                else:
                    event["response_to"] = {"status": "UNMATCHED_SESSION"}
            _write_event(self._log_path, event)
        except Exception as exc:
            _write_event(self._log_path, {
                "ts": datetime.now(timezone.utc).isoformat(timespec="milliseconds"),
                "direction": direction, "kind": "decode_error",
                "connection_id": self._connection_id, "peer": self._peer,
                "error": str(exc)[:300], "frame_bytes": len(frame),
            })

    def _ingest_rx(self, data: bytes) -> None:
        if not data:
            return
        with self._rx_lock:
            self._rx_buffer.extend(data)
            while len(self._rx_buffer) >= 2:
                size = struct.unpack(">H", self._rx_buffer[:2])[0]
                if size <= 0 or size > 65535:
                    # Resynchronisation is intentionally conservative: discard
                    # one byte and continue rather than stalling the receiver.
                    del self._rx_buffer[0]
                    continue
                if len(self._rx_buffer) < size + 2:
                    break
                frame = bytes(self._rx_buffer[:size + 2])
                del self._rx_buffer[:size + 2]
                self._emit("C2S", frame)

    def recv(self, bufsize: int, flags: int = 0):
        data = self._sock.recv(bufsize, flags)
        if data and not (flags & getattr(__import__("socket"), "MSG_PEEK", 0)):
            self._ingest_rx(data)
        return data

    def sendall(self, data: bytes, flags: int = 0):
        self._sock.sendall(data, flags)
        # Server sends one complete framed Sproto packet per sendall call.
        # Ignore non-protocol writes (for example HTTP updater responses).
        if len(data) >= 3:
            try:
                declared = struct.unpack(">H", data[:2])[0]
                if declared == len(data) - 2:
                    self._emit("S2C", bytes(data))
            except Exception:
                pass

    def close(self):
        if not self._closed:
            self._closed = True
            with _PENDING_LOCK:
                unresolved = [
                    {"session": session, "request_tag": item["tag"], "request_protocol": item["name"]}
                    for (conn_id, session), item in list(_PENDING.items())
                    if conn_id == self._connection_id
                ]
                for item in list(_PENDING):
                    if item[0] == self._connection_id:
                        _PENDING.pop(item, None)
            _write_event(self._log_path, {
                "ts": datetime.now(timezone.utc).isoformat(timespec="milliseconds"),
                "direction": "SYSTEM", "kind": "connection_closed",
                "connection_id": self._connection_id, "peer": self._peer,
                "unanswered_requests": unresolved,
                "context": self._context(),
            })
        return self._sock.close()


def monitor_socket(sock, decoder: Callable, unpacker: Callable,
                   context_provider: Optional[Callable[[], dict]] = None,
                   log_path: Optional[str] = None):
    """Return a transparent monitoring facade around an accepted client socket."""
    path = Path(log_path).expanduser() if log_path else DEFAULT_LOG
    return MonitoredSocket(sock, decoder, unpacker, context_provider, path)


def _matches(event: dict, category: str) -> bool:
    return category == "all" or event.get("category", "other") == category


def _format_event(event: dict, pretty: bool = False) -> str:
    ts = event.get("ts", "")
    direction = event.get("direction", "??")
    kind = event.get("kind", "event").upper()
    tag = event.get("tag")
    proto = event.get("protocol", "unknown")
    session = event.get("session")
    cat = str(event.get("category", "system")).upper()
    who = event.get("character_name") or event.get("character_id") or "-"
    map_id = event.get("map_id", "-")
    response = event.get("response_to", {})
    suffix = ""
    if response:
        suffix = " | response_to=%s latency=%sms" % (
            response.get("protocol", response.get("status", "?")),
            response.get("latency_ms", "?")
        )
    line = "%s %-4s %-8s %-8s tag=%-4s %-30s session=%-6s player=%s map=%s%s" % (
        ts, direction, kind, cat, str(tag if tag is not None else "-"),
        proto, str(session if session is not None else "-"), who, map_id, suffix
    )
    if pretty:
        line += "\n" + json.dumps(event.get("fields", {}), ensure_ascii=False, indent=2)
    return line


def tail_events(path: Path, category: str = "all", follow: bool = True,
                initial_lines: int = 40, pretty: bool = False):
    if not path.exists():
        print("Waiting for monitor log: %s" % path)
        if not follow:
            return
        while not path.exists():
            time.sleep(0.5)
    with path.open("r", encoding="utf-8", errors="replace") as f:
        if initial_lines > 0:
            lines = deque(f, maxlen=initial_lines)
            for line in lines:
                try:
                    event = json.loads(line)
                    if _matches(event, category):
                        print(_format_event(event, pretty), flush=True)
                except json.JSONDecodeError:
                    continue
        else:
            f.seek(0, os.SEEK_END)
        if not follow:
            return
        while True:
            line = f.readline()
            if not line:
                time.sleep(0.15)
                continue
            try:
                event = json.loads(line)
                if _matches(event, category):
                    print(_format_event(event, pretty), flush=True)
            except json.JSONDecodeError:
                continue


def main():
    parser = argparse.ArgumentParser(
        description="Live, read-only ATG Sproto request/response monitor"
    )
    parser.add_argument("--log", default=str(DEFAULT_LOG), help="JSONL event log path")
    parser.add_argument("--category", choices=VALID_CATEGORIES, default="all")
    parser.add_argument("--tail", type=int, default=40, help="initial events to show; 0 starts at end")
    parser.add_argument("--follow", action="store_true", help="continue streaming new events")
    parser.add_argument("--pretty", action="store_true", help="show decoded fields")
    parser.add_argument("--list-categories", action="store_true")
    args = parser.parse_args()
    if args.list_categories:
        print("\n".join(VALID_CATEGORIES))
        return 0
    tail_events(Path(args.log).expanduser(), args.category, args.follow, max(0, args.tail), args.pretty)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
