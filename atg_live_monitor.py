#!/usr/bin/env python3
"""ATG single-file live Sproto monitor for one character.

On Serv00 this script starts 9555.py in-process with a transparent socket
wrapper. It does not edit 9555.py or alter packet bytes. On Windows it SSHes to
the configured Serv00 host and runs the same script remotely.
"""
from __future__ import annotations

import argparse
import importlib.util
import json
import os
import re
import shlex
import socket
import struct
import subprocess
import sys
import threading
import time
from datetime import datetime
from pathlib import Path
from typing import Any, Callable

DEFAULT_CHARACTER_ID = "546880743"
DEFAULT_SSH_TARGET = os.environ.get("ATG_SSH_TARGET", "ledio01@s16")
REAL_SOCKET = socket.socket
MSG_PEEK = getattr(socket, "MSG_PEEK", 0)

# Protocol names are verified server handler labels where available. Numeric
# Sproto fields remain numeric until matched against the decompiled client.
PROTOCOLS = {
    2: "visitor", 3: "verify", 4: "login", 7: "update_game_server",
    100: "map_ready", 101: "move", 102: "skill_use", 103: "character_list",
    104: "character_create", 105: "character_pick", 108: "leave_copy_scene",
    111: "accept_damage", 112: "accept_mission", 113: "complete_mission",
    114: "abandon_mission", 115: "use_item", 119: "ask_pickup_item",
    120: "chat", 121: "request_daily_mission", 122: "send_mail",
    123: "mail_operation", 124: "add_friend", 125: "delete_friend",
    126: "request_friend_info", 128: "local_character_attack",
    129: "sell_item", 130: "skill_level_up", 132: "relife_player",
    137: "rank_pvp_other_player_die", 143: "ask_shop_list", 144: "buy_shop_item",
    146: "guild_create", 147: "guild_join_apply", 148: "guild_leave",
    149: "guild_kick", 150: "guild_job_change", 151: "guild_skill_level",
    152: "guild_request_list", 153: "guild_request_info", 154: "guild_approve_request",
    156: "title_level_up", 158: "approve_friend_request", 159: "random_online_characters",
    160: "search_online_character", 165: "get_team_list", 167: "equip_enhance",
    168: "offline_chat", 169: "request_guild_notice", 170: "request_guild_member_info",
    173: "set_guild_application_mode", 174: "guild_log", 175: "guild_donate",
    176: "search_guild", 178: "update_mission_param", 183: "equip_refine",
    184: "equip_inherit", 185: "change_potion", 191: "request_top_rank_list",
    192: "change_skill_position", 193: "car_chase_result", 195: "request_guild_boss",
    199: "badge_merge", 201: "world_boss", 212: "request_guild_skill",
    218: "heartbeat", 220: "start_battle", 223: "change_show_type",
    224: "open_item_package", 227: "request_dance_info", 228: "pause_dance",
    229: "use_dance", 231: "request_guild_boss_battle_info", 233: "open_guild_boss",
    235: "request_mount_info", 240: "change_item_state", 241: "mount_use_color",
    247: "guild_invite", 267: "change_mount_state", 268: "unlock_function_complete",
    270: "download_finish", 275: "play_social_dance", 281: "refresh_online_state",
    284: "send_system_mail", 285: "request_guild_battle_info", 286: "enter_guild_battle",
    287: "request_guild_battle_rank", 288: "request_guild_battle_members",
    289: "set_guild_battle_member", 291: "request_guild_score_info",
    293: "request_guild_star", 294: "update_guild_star", 295: "request_guild_battle_state",
    298: "impact_npc", 301: "rename", 303: "equip_appraise", 306: "tutorial_finish",
    307: "combat_or_npc_event", 310: "request_domin_info", 311: "enter_domin_pk_scene",
    314: "use_dance_sound_box", 315: "update_guild_dance_time", 316: "change_skill_index",
    318: "request_guild_capture_rank", 319: "request_guild_map_info",
    320: "request_guild_map_reward", 321: "gather_other_player", 322: "request_guild_capture",
    323: "buy_car_shop", 324: "update_player_map_info",
    503: "enter_map", 504: "main_player_create", 505: "aoi_add", 508: "skill_broadcast",
    509: "npc_create", 510: "visual_update", 519: "sync_mission",
    520: "accept_mission_push", 521: "complete_mission_push", 522: "abandon_mission_push",
    523: "mission_state", 524: "mission_param", 526: "item_use_result",
    529: "scene_notice", 540: "skills", 554: "shop_list", 555: "copy_attempts",
    608: "car_copy_result", 611: "inventory", 614: "server_time", 628: "guild_boss_entry_result",
    629: "copy_timer", 652: "guild_shop_purchase_result", 654: "start_enter_game",
    681: "team_match_state",
}

CATEGORY_TAGS = {
    "TEAM": {165, 681},
    "GANG": {143, 144, 146, 147, 148, 149, 150, 151, 152, 153, 154, 169, 170, 173, 174,
             175, 176, 195, 212, 231, 233, 247, 285, 286, 287, 288, 289, 291, 293, 294,
             295, 318, 319, 320, 322, 628, 652},
    "AOI": {101, 298, 321, 503, 504, 505, 509, 510},
    "SKILL/COMBAT": {102, 111, 128, 130, 137, 193, 201, 220, 307, 311, 508},
    "EQUIP/ITEM": {115, 119, 129, 156, 167, 183, 184, 185, 192, 199, 223, 224, 235, 240,
                   241, 267, 303, 316, 526, 540, 611},
    "MISSION": {112, 113, 114, 121, 178, 306, 519, 520, 521, 522, 523, 524},
    "MAP/COPY": {7, 100, 108, 145, 503, 614, 629, 654},
    "REWARD/MAIL": {122, 123, 132, 156, 175, 193, 199, 320, 521, 608},
    "SHOP": {143, 144, 224, 323, 554, 652},
    "SOCIAL": {120, 124, 125, 126, 147, 148, 158, 159, 160, 168, 247, 281},
    "CAR/RACE": {193, 220, 235, 241, 267, 298, 323, 608},
    "RANK": {137, 191, 231, 287, 318},
    "DANCE": {227, 228, 229, 275, 314, 315},
}


def category_for(tag: int | None) -> str:
    for label, tags in CATEGORY_TAGS.items():
        if tag in tags:
            return label
    return "OTHER"


def as_int(value: Any) -> int | None:
    if isinstance(value, bool) or value is None:
        return None
    try:
        if isinstance(value, bytes):
            value = value.decode("ascii", "strict")
        return int(value)
    except (TypeError, ValueError, UnicodeDecodeError):
        return None


def safe_value(value: Any, decoder: Callable, depth: int = 0) -> Any:
    if isinstance(value, bytes):
        result: dict[str, Any] = {"bytes": len(value), "hex": value[:128].hex()}
        try:
            text = value.decode("utf-8")
            if text and all(ch.isprintable() or ch in "\r\n\t" for ch in text):
                result["text"] = text[:512]
        except UnicodeDecodeError:
            pass
        if depth < 3 and len(value) >= 2:
            try:
                nested = decoder(value, 0)
                if isinstance(nested, dict) and nested:
                    result["nested_fields"] = {
                        str(k): safe_value(v, decoder, depth + 1)
                        for k, v in nested.items()
                    }
            except Exception:
                pass
        if len(value) > 128:
            result["hex_truncated"] = True
        return result
    if isinstance(value, dict):
        return {str(k): safe_value(v, decoder, depth + 1) for k, v in value.items()}
    if isinstance(value, (list, tuple)):
        return [safe_value(v, decoder, depth + 1) for v in value[:150]]
    if value is None or isinstance(value, (str, int, float, bool)):
        return value
    return repr(value)[:256]


class MonitorState:
    def __init__(self, target_id: str, server_ns: dict, log_dir: Path,
                 timeout_sec: float, show_fields: bool, handler_tags: set[int]):
        self.target_id = str(target_id)
        self.server_ns = server_ns
        self.log_dir = log_dir
        self.log_dir.mkdir(parents=True, exist_ok=True)
        self.event_path = self.log_dir / f"character_{self.target_id}_protocol.jsonl"
        self.server_path = self.log_dir / f"character_{self.target_id}_server.log"
        self.timeout_sec = timeout_sec
        self.show_fields = show_fields
        self.handler_tags = handler_tags
        self.lock = threading.RLock()
        self.output_lock = threading.Lock()
        self.pending: dict[tuple[int, int], dict] = {}
        self.target_name = ""
        self.started = time.monotonic()
        self._stop = threading.Event()
        threading.Thread(target=self._timeout_worker, daemon=True, name="atg-monitor-timeouts").start()

    def _rotate(self, path: Path, max_bytes: int = 20 * 1024 * 1024) -> None:
        try:
            if path.exists() and path.stat().st_size >= max_bytes:
                old = path.with_name(path.name + ".1")
                try:
                    old.unlink(missing_ok=True)
                except OSError:
                    pass
                path.replace(old)
        except OSError:
            pass

    def append_json(self, event: dict) -> None:
        try:
            self._rotate(self.event_path, 40 * 1024 * 1024)
            with self.event_path.open("a", encoding="utf-8") as f:
                f.write(json.dumps(event, ensure_ascii=False, separators=(",", ":")) + "\n")
        except OSError as exc:
            with self.output_lock:
                sys.__stdout__.write(f"[MONITOR LOG ERROR] {exc}\n")
                sys.__stdout__.flush()

    def server_print(self, *args, sep=" ", end="\n", **kwargs) -> None:
        # Keep complete server console output on disk, but show only startup
        # messages and lines explicitly containing this character's ID/name.
        line = sep.join(str(x) for x in args) + end
        plain = line.strip()
        startup = ("GAME SERVER 9555 READY" in plain or
                   "[MISSION DATA LOADED]" in plain or
                   "[REWARDS DATA LOADED]" in plain or
                   "[MAP CONFIG LOADED]" in plain)
        target_line = (self.target_id in plain or
                       (self.target_name and self.target_name in plain))
        # Global RX/TX console summaries are deliberately discarded; the
        # decoded protocol stream below is already filtered to the target ID.
        if plain.startswith("[RX]") or plain.startswith("[TX]"):
            return
        if not (startup or target_line):
            return
        try:
            self._rotate(self.server_path)
            with self.server_path.open("a", encoding="utf-8", errors="replace") as f:
                f.write(line)
        except OSError:
            pass
        with self.output_lock:
            sys.__stdout__.write(line)
            sys.__stdout__.flush()

    def player_context(self, conn) -> dict:
        try:
            pair = self.server_ns.get("ALL_CONNECTIONS", {}).get(int(self.target_id))
            if pair and pair[0] is conn and isinstance(pair[1], dict):
                char = pair[1]
                self.target_name = str(char.get("name") or self.target_name)
                return {
                    "character_id": self.target_id,
                    "character_name": self.target_name or None,
                    "map_id": str(char.get("map_id")) if char.get("map_id") is not None else None,
                    "level": char.get("level"),
                }
        except Exception:
            pass
        return {"character_id": self.target_id}

    def decode_frame(self, frame: bytes) -> dict:
        if len(frame) < 3:
            raise ValueError("frame too short")
        frame_len = struct.unpack(">H", frame[:2])[0]
        if frame_len != len(frame) - 2:
            raise ValueError("length prefix mismatch")
        unpacker = self.server_ns.get("sproto_unpack")
        decoder = self.server_ns.get("decode_sproto")
        if not callable(unpacker) or not callable(decoder):
            raise RuntimeError("server Sproto decoder is not initialized")
        raw = unpacker(frame[2:])
        header = decoder(raw, 0)
        if not isinstance(header, dict):
            raise ValueError("invalid Sproto header")
        tag = as_int(header.get(0))
        session = as_int(header.get(1))
        if len(raw) < 2:
            body = {}
        else:
            header_words = struct.unpack("<H", raw[:2])[0]
            body_offset = 2 + header_words * 2
            body = decoder(raw, body_offset) if body_offset < len(raw) else {}
        if not isinstance(body, dict):
            body = {}
        fields = {str(k): safe_value(v, decoder) for k, v in body.items()}
        # Never print or persist account/auth payload contents.
        if tag in (2, 3, 4):
            fields = {key: "<redacted-auth-field>" for key in fields}
        return {"tag": tag, "session": session, "fields": fields, "raw_body": body}

    def observe(self, conn, direction: str, frame: bytes) -> None:
        try:
            decoded = self.decode_frame(frame)
            tag = decoded["tag"]
            session = decoded["session"]
            fields = decoded["fields"]
            raw_body = decoded.get("raw_body", {})

            if direction == "C2S" and tag == 105:
                picked_id = as_int(raw_body.get(0))
                if picked_id is not None and str(picked_id) == self.target_id:
                    conn._atg_target = True
                    with self.output_lock:
                        sys.__stdout__.write(
                            f"\n[TARGET SELECTED] character_id={self.target_id} "
                            f"connection={conn.connection_id}\n"
                        )
                        sys.__stdout__.flush()
            if not getattr(conn, "_atg_target", False):
                return

            protocol = PROTOCOLS.get(tag, f"unknown_tag_{tag}" if tag is not None else "unknown")
            kind = "REQUEST" if direction == "C2S" else ("RESPONSE" if session is not None else "PUSH")
            dispatch_status = ("FOUND" if tag in self.handler_tags else "NO_EXPLICIT_HANDLER_BRANCH") if kind == "REQUEST" else None
            matched = None
            now_mono = time.monotonic()
            if direction == "C2S" and tag is not None and session is not None:
                with self.lock:
                    self.pending[(conn.connection_id, session)] = {
                        "tag": tag, "protocol": protocol, "category": category_for(tag),
                        "dispatch_status": dispatch_status, "started": now_mono,
                        "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds")
                    }
            elif direction == "S2C" and session is not None:
                with self.lock:
                    matched = self.pending.pop((conn.connection_id, session), None)

            category = category_for(tag)
            if kind == "RESPONSE" and matched:
                protocol = matched["protocol"] + "_response"
                category = matched["category"]
            ctx = self.player_context(conn)
            event = {
                "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                "direction": direction,
                "kind": kind,
                "category": category,
                "tag": tag,
                "protocol_label": protocol,
                "server_dispatch": dispatch_status,
                "client_expectation": "UNVERIFIED_CLIENT_SCHEMA_NOT_LINKED",
                "session": session,
                "connection_id": conn.connection_id,
                **ctx,
                "fields": fields,
            }
            if kind == "RESPONSE":
                if matched:
                    event["response_to"] = {
                        "tag": matched["tag"],
                        "protocol_label": matched["protocol"],
                        "latency_ms": round((now_mono - matched["started"]) * 1000, 2),
                        "status": "MATCHED_BY_SESSION",
                    }
                else:
                    event["response_to"] = {"status": "UNMATCHED_SESSION"}
            self.append_json(event)
            self.display_event(event)
        except Exception as exc:
            if getattr(conn, "_atg_target", False):
                event = {
                    "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                    "direction": direction, "kind": "DECODE_ERROR",
                    "connection_id": conn.connection_id, "error": str(exc)[:300],
                    "frame_bytes": len(frame), "character_id": self.target_id,
                }
                self.append_json(event)
                with self.output_lock:
                    sys.__stdout__.write(f"[DECODE ERROR] {direction}: {exc}\n")
                    sys.__stdout__.flush()

    def display_event(self, event: dict) -> None:
        ts = event["timestamp"].split("T")[-1]
        direction = "CLIENT -> SERVER" if event["direction"] == "C2S" else "SERVER -> CLIENT"
        kind = event["kind"]
        category = event["category"]
        tag = event["tag"]
        protocol = event["protocol_label"]
        session = event.get("session")
        context = event.get("map_id", "?")
        title = (f"\n[{ts}] {direction} | {kind} | {category}\n"
                 f"  TAG={tag}  SPROTO={protocol}  SESSION={session}  MAP={context}")
        if kind == "REQUEST":
            title += f"\n  SERVER DISPATCH: {event.get('server_dispatch')}"
        response_to = event.get("response_to")
        if response_to:
            if response_to.get("status") == "MATCHED_BY_SESSION":
                title += (f"\n  RESPONSE TO TAG={response_to['tag']} "
                          f"({response_to['protocol_label']})  "
                          f"LATENCY={response_to['latency_ms']} ms")
            else:
                title += "\n  RESPONSE WARNING: no matching request session was captured"
        with self.output_lock:
            sys.__stdout__.write(title + "\n")
            if self.show_fields and event.get("fields"):
                sys.__stdout__.write(json.dumps(event["fields"], ensure_ascii=False, indent=2) + "\n")
            sys.__stdout__.flush()

    def connection_closed(self, conn) -> None:
        if not getattr(conn, "_atg_target", False):
            return
        unresolved = []
        with self.lock:
            for key, request in list(self.pending.items()):
                if key[0] == conn.connection_id:
                    unresolved.append(request)
                    self.pending.pop(key, None)
        if unresolved:
            event = {
                "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                "direction": "SYSTEM", "kind": "CONNECTION_CLOSED",
                "character_id": self.target_id,
                "unanswered_requests": unresolved,
            }
            self.append_json(event)
            with self.output_lock:
                sys.__stdout__.write(f"\n[CONNECTION CLOSED] {len(unresolved)} request(s) had no response before disconnect\n")
                sys.__stdout__.flush()

    def _timeout_worker(self) -> None:
        while not self._stop.wait(1.0):
            now = time.monotonic()
            expired = []
            with self.lock:
                for key, request in list(self.pending.items()):
                    if now - request["started"] >= self.timeout_sec:
                        expired.append((key, request))
                        self.pending.pop(key, None)
            for (connection_id, session), request in expired:
                event = {
                    "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                    "direction": "SYSTEM", "kind": "RESPONSE_TIMEOUT",
                    "character_id": self.target_id, "connection_id": connection_id,
                    "request_tag": request["tag"], "request_protocol": request["protocol"],
                    "server_dispatch": request.get("dispatch_status"),
                    "session": session, "timeout_seconds": self.timeout_sec,
                    "status": "NO_RESPONSE_OBSERVED_WITHIN_TIMEOUT",
                }
                self.append_json(event)
                with self.output_lock:
                    sys.__stdout__.write(
                        f"\n[NO RESPONSE OBSERVED] tag={request['tag']} "
                        f"{request['protocol']} session={session} "
                        f"after {self.timeout_sec:g}s (check any delayed push too)\n"
                    )
                    sys.__stdout__.flush()


class MonitoredConnection:
    def __init__(self, raw_socket, address, monitor: MonitorState):
        self._sock = raw_socket
        self._monitor = monitor
        self._rx_buffer = bytearray()
        self._tx_buffer = bytearray()
        self._buffer_lock = threading.Lock()
        self._atg_target = False
        self._is_http = False
        self.connection_id = id(self)
        self.address = address

    def __getattr__(self, name):
        return getattr(self._sock, name)

    def _ingest(self, buffer: bytearray, data: bytes, direction: str) -> None:
        if not data:
            return
        with self._buffer_lock:
            buffer.extend(data)
            while len(buffer) >= 2:
                size = struct.unpack(">H", buffer[:2])[0]
                if size == 0 or size > 65535:
                    # This is not a framed game packet; avoid blocking or changing
                    # the real stream, and stop interpreting this direction.
                    buffer.clear()
                    if direction == "C2S":
                        self._is_http = True
                    return
                if len(buffer) < size + 2:
                    return
                frame = bytes(buffer[:size + 2])
                del buffer[:size + 2]
                self._monitor.observe(self, direction, frame)

    def recv(self, bufsize: int, flags: int = 0):
        data = self._sock.recv(bufsize, flags)
        if data and (flags & MSG_PEEK):
            if data.startswith((b"GET ", b"HEAD", b"POST ")):
                self._is_http = True
        elif data and not self._is_http:
            self._ingest(self._rx_buffer, data, "C2S")
        return data

    def sendall(self, data, flags: int = 0):
        if flags:
            self._sock.sendall(data, flags)
        else:
            self._sock.sendall(data)
        if not self._is_http:
            self._ingest(self._tx_buffer, bytes(data), "S2C")

    def send(self, data, flags: int = 0):
        sent = self._sock.send(data, flags)
        if sent and not self._is_http:
            self._ingest(self._tx_buffer, bytes(data[:sent]), "S2C")
        return sent

    def close(self):
        try:
            self._monitor.connection_closed(self)
        finally:
            return self._sock.close()


class InstrumentedListener(REAL_SOCKET):
    monitor: MonitorState | None = None

    def accept(self):
        raw, address = super().accept()
        if self.monitor is None:
            return raw, address
        return MonitoredConnection(raw, address, self.monitor), address


def port_is_open(port: int) -> bool:
    probe = REAL_SOCKET(socket.AF_INET, socket.SOCK_STREAM)
    probe.settimeout(0.5)
    try:
        return probe.connect_ex(("127.0.0.1", port)) == 0
    finally:
        probe.close()


def extract_handler_tags(source: str) -> set[int]:
    """Read explicit msg==N branches from the live server's client_handler."""
    start = source.find("def client_handler(")
    if start < 0:
        return set()
    handler = source[start:]
    end = handler.find("\nif __name__ ==")
    if end >= 0:
        handler = handler[:end]
    return {int(x) for x in re.findall(r"(?:if|elif)\s+msg\s*==\s*(\d+)", handler)}


def run_server(args) -> int:
    server_path = Path(args.server_file).expanduser().resolve()
    if not server_path.is_file():
        print(f"ERROR: cannot find {server_path}", file=sys.stderr)
        return 2
    if port_is_open(args.port):
        print(f"ERROR: port {args.port} is already accepting connections.")
        print("This monitor cannot attach to an already-running Python server.")
        print("Stop only the existing 9555.py process, then launch this script instead.")
        return 3

    os.environ["PORT"] = str(args.port)
    try:
        source = server_path.read_text(encoding="utf-8")
    except OSError as exc:
        print(f"ERROR: cannot read {server_path}: {exc}", file=sys.stderr)
        return 2
    handler_tags = extract_handler_tags(source)
    spec = importlib.util.spec_from_file_location("_atg_server_runtime", server_path)
    if spec is None or spec.loader is None:
        print(f"ERROR: cannot load {server_path}", file=sys.stderr)
        return 2
    server_module = importlib.util.module_from_spec(spec)
    server_ns = server_module.__dict__
    monitor = MonitorState(args.character_id, server_ns, Path(args.log_dir).expanduser(),
                           args.timeout, not args.compact, handler_tags)
    InstrumentedListener.monitor = monitor
    socket.socket = InstrumentedListener
    server_ns["print"] = monitor.server_print

    print("=" * 76)
    print("ATG SINGLE-CHARACTER LIVE SPROTO MONITOR")
    print(f"Character filter : {args.character_id}")
    print(f"Game server file : {server_path}")
    print(f"Game port        : {args.port}")
    print(f"Protocol events  : {monitor.event_path}")
    print(f"Target server log: {monitor.server_path}")
    print("Mode             : in-process socket observation; no 9555.py edits")
    print("Other characters : hidden from this terminal and target logs")
    print("Client validation : not guessed; client schema fields remain UNVERIFIED")
    print("=" * 76)
    print("Starting the game server under the monitor. Press Ctrl+C to stop it.\n")

    try:
        sys.modules[spec.name] = server_module
        spec.loader.exec_module(server_module)
        server_module.start_server()
    except KeyboardInterrupt:
        print("\nMonitor stopped by user.")
        return 0
    except Exception:
        import traceback
        traceback.print_exc()
        return 1
    return 0


def run_over_ssh(args) -> int:
    target = args.ssh_target
    remote_script = args.remote_script
    remote_args = [
        "python3", remote_script, "--server-mode",
        "--character-id", args.character_id,
        "--port", str(args.port),
        "--timeout", str(args.timeout),
    ]
    if args.compact:
        remote_args.append("--compact")
    if args.log_dir:
        remote_args.extend(["--log-dir", args.log_dir])
    remote_command = "cd ~/9555-1 && " + " ".join(shlex.quote(x) for x in remote_args)
    ssh = "ssh.exe" if os.name == "nt" else "ssh"
    print(f"Connecting to {target}; monitor will run beside the Serv00 game server.")
    try:
        return subprocess.call([ssh, "-tt", target, remote_command])
    except FileNotFoundError:
        print("ERROR: OpenSSH client was not found. Run this from Serv00, or install Windows OpenSSH.",
              file=sys.stderr)
        return 2


def parse_args():
    parser = argparse.ArgumentParser(
        description="Observe live ATG Sproto requests/responses for one character; never sends packets."
    )
    parser.add_argument("--character-id", default=DEFAULT_CHARACTER_ID,
                        help=f"only show this character (default: {DEFAULT_CHARACTER_ID})")
    parser.add_argument("--port", type=int, default=int(os.environ.get("PORT", "15678")))
    parser.add_argument("--server-file", default="9555.py", help="path to the game server script")
    parser.add_argument("--log-dir", default="~/atg-monitor")
    parser.add_argument("--timeout", type=float, default=12.0,
                        help="seconds before an unanswered Sproto session is reported")
    parser.add_argument("--compact", action="store_true", help="hide field JSON in terminal; still save it")
    parser.add_argument("--ssh-target", default=DEFAULT_SSH_TARGET,
                        help="SSH target used when launched from Windows")
    parser.add_argument("--remote-script", default="atg_live_monitor.py")
    parser.add_argument("--server-mode", action="store_true", help=argparse.SUPPRESS)
    return parser.parse_args()


def main() -> int:
    args = parse_args()
    if not args.character_id.isdigit():
        print("ERROR: --character-id must be numeric", file=sys.stderr)
        return 2
    if args.timeout <= 0:
        print("ERROR: --timeout must be greater than zero", file=sys.stderr)
        return 2
    if os.name == "nt" and not args.server_mode:
        return run_over_ssh(args)
    return run_server(args)


if __name__ == "__main__":
    raise SystemExit(main())
