#!/usr/bin/env python3
"""ATG single-file live Sproto monitor for one character.

On Serv00 this script starts 9555.py in-process with a transparent socket
wrapper. It does not edit 9555.py or alter packet bytes. On Windows it SSHes to
the configured Serv00 host and runs the same script remotely.
"""
from __future__ import annotations

import argparse
import ast
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


# Human-readable action labels are protocol hints inferred from the server's
# request handler names. They are NOT proof of the exact Unity button text.
# Requests without a mapping are still recorded with their numeric tag.
UI_ACTIONS = {
    100: "Scene load complete / map-ready",
    101: "Movement joystick / player position update",
    102: "Combat skill button / skill activation",
    103: "Login: character selection list",
    104: "Character creation submit",
    105: "Character selection / enter character",
    108: "Leave copy scene / exit instance",
    111: "Combat damage or hit report",
    112: "Mission: accept mission",
    113: "Mission: complete/claim mission",
    114: "Mission: abandon mission",
    115: "Backpack: use item",
    119: "World pickup: request pickup item",
    120: "Chat: send message",
    121: "Daily missions: request daily data",
    122: "Mail: send mail",
    123: "Mail: mailbox operation",
    124: "Social: add friend or enemy",
    125: "Social: remove friend or enemy",
    126: "Social: refresh friend profile",
    128: "Combat: local-player attack event",
    129: "Backpack: sell item",
    130: "Skills: upgrade skill",
    132: "Death/revive UI: request revival",
    137: "PVP: player-death/ranking event",
    143: "Gang shop: request shop list",
    144: "Gang shop: buy item",
    146: "Gang: create gang",
    147: "Gang: apply to join / join",
    148: "Gang: leave gang",
    149: "Gang: kick member",
    150: "Gang: change member rank/position",
    151: "Gang: upgrade gang skill",
    152: "Gang: request application list",
    153: "Gang: request application details",
    154: "Gang: approve/reject application",
    156: "Title/honor: level-up request",
    158: "Social: approve friend request",
    159: "Social: list online players",
    160: "Social: search online player",
    165: "Team: request team list/state",
    167: "Equipment: enhance equipped item",
    168: "Chat: request offline messages",
    169: "Gang: request announcement",
    170: "Gang: request member details",
    173: "Gang: configure application approval",
    174: "Gang: open gang log",
    175: "Gang: donate resources",
    176: "Gang: search gang",
    178: "Mission: update mission parameter",
    183: "Equipment: refine accessory",
    184: "Equipment: transfer/inherit equipment experience",
    185: "Backpack: select potion",
    191: "Ranking: request top list",
    192: "Skills: change skill-bar position",
    193: "Car/race: submit car chase result",
    195: "Gang activity: request gang-boss overview",
    199: "Badge: merge badges",
    201: "World boss: interact/request action",
    212: "Gang: request gang skill data",
    218: "Connection heartbeat (automatic)",
    220: "Street Race / EXP Stage: start battle",
    223: "Character appearance: toggle fashion display",
    224: "Backpack: open item package/box",
    227: "Dance: request dance information",
    228: "Dance: pause/stop participation",
    229: "Dance: select/use dance",
    231: "Gang boss: request battle/rank information",
    233: "Gang boss: enter/open activity",
    235: "Garage: request vehicle information",
    240: "Backpack: mark item read/seen",
    241: "Garage: select/purchase vehicle colour",
    247: "Gang: invite player",
    267: "Garage: change vehicle active/riding state",
    268: "Tutorial/function unlock: report completion",
    270: "Resource download: report finished",
    275: "Social: play emote/dance",
    281: "Social: refresh online state",
    284: "Mail: request system mailbox",
    285: "Gang battle: request battle info",
    286: "Gang battle: enter battle",
    287: "Gang battle: request ranking",
    288: "Gang battle: request members",
    289: "Gang battle: update member selection",
    291: "Gang battle: request score",
    293: "Gang: request star-upgrade info",
    294: "Gang: upgrade gang star",
    295: "Gang battle: request battle state",
    298: "Vehicle: impact/collision with NPC",
    301: "Character: rename",
    303: "Equipment: appraise",
    306: "Tutorial: report completion",
    307: "Combat/NPC: gameplay event report (semantics need client verification)",
    310: "Domin/Capture: request activity information",
    311: "Domin/PVP: enter PK scene",
    314: "Dance: use dance sound box",
    315: "Gang dance: update dance time",
    316: "Skills: change skill index/slot",
    318: "Gang city: request domination ranking",
    319: "Gang city: request map/capture information",
    320: "Gang city: claim configured map reward",
    321: "Social/world: move to another player",
    322: "Gang city: capture information/action",
    323: "Garage: buy vehicle",
    324: "Social/PVP: request player map/location",
}

PUSH_LABELS = {
    503: "Scene transition / enter map",
    504: "Create local main-player object",
    505: "AOI: another player entered visibility range",
    509: "AOI: create NPC",
    510: "AOI: update player visual/equipment appearance",
    519: "Mission: full mission-state synchronization",
    520: "Mission: accepted mission push",
    521: "Mission: completed mission push",
    522: "Mission: abandoned mission push",
    523: "Mission: update mission state",
    524: "Mission: update mission parameter",
    540: "Skills: synchronize skills",
    554: "Shop: deliver shop list",
    555: "Copy: synchronize remaining attempts",
    608: "Car copy: result/reward",
    611: "Backpack: inventory synchronization",
    614: "Login/scene: server-time synchronization",
    628: "Gang boss: entry result",
    629: "Copy/boss: timer state",
    652: "Gang shop: purchase result",
    654: "Login: start entering game",
    681: "Team: match-state update",
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
                 timeout_sec: float, show_fields: bool, handler_tags: set[int],
                 field_hints: dict[int, dict[int, list[str]]]):
        self.target_id = str(target_id)
        self.server_ns = server_ns
        self.log_dir = log_dir
        self.log_dir.mkdir(parents=True, exist_ok=True)
        self.event_path = self.log_dir / f"character_{self.target_id}_protocol.jsonl"
        self.server_path = self.log_dir / f"character_{self.target_id}_server.log"
        self.timeout_sec = timeout_sec
        self.show_fields = show_fields
        self.handler_tags = handler_tags
        self.field_hints = field_hints
        self.lock = threading.RLock()
        self.server_pending: dict[tuple[int, int], dict] = {}
        self.thread_connections: dict[int, Any] = {}
        self.thread_unpacked: dict[int, dict] = {}
        self.original_unpacker: Callable | None = None
        self.original_decoder: Callable | None = None
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


    def bind_current_thread(self, conn) -> None:
        self.thread_connections[threading.get_ident()] = conn

    def note_sproto_unpacked(self, raw: bytes) -> None:
        thread_id = threading.get_ident()
        conn = self.thread_connections.get(thread_id)
        if conn is not None:
            self.thread_unpacked[thread_id] = {"raw": raw, "conn": conn, "header": None}

    def note_sproto_decoded(self, raw: bytes, offset: int, result: Any) -> None:
        thread_id = threading.get_ident()
        pending = self.thread_unpacked.get(thread_id)
        if not pending or pending.get("raw") is not raw:
            return
        if offset == 0:
            pending["header"] = result if isinstance(result, dict) else None
            return
        self.thread_unpacked.pop(thread_id, None)
        header = pending.get("header") or {}
        conn = pending.get("conn")
        if conn is None:
            return
        self.on_request(conn, as_int(header.get(0)), as_int(header.get(1)),
                        result if isinstance(result, dict) else {})

    def decode_frame(self, frame: bytes) -> dict:
        if len(frame) < 3:
            raise ValueError("frame too short")
        frame_len = struct.unpack(">H", frame[:2])[0]
        if frame_len != len(frame) - 2:
            raise ValueError("length prefix mismatch")
        unpacker = self.original_unpacker or self.server_ns.get("sproto_unpack")
        decoder = self.original_decoder or self.server_ns.get("decode_sproto")
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


    def on_request(self, conn, tag: int | None, session: int | None, body: dict) -> None:
        """Log the exact decoded client packet before the server dispatches it."""
        try:
            if not isinstance(body, dict):
                body = {}
            if tag == 105:
                picked_id = as_int(body.get(0))
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

            is_request = tag is not None
            protocol = PROTOCOLS.get(tag, f"unknown_tag_{tag}") if is_request else "client_rpc_response"
            kind = "REQUEST" if is_request else "CLIENT_RESPONSE"
            dispatch_status = (
                "FOUND" if tag in self.handler_tags else "NO_EXPLICIT_HANDLER_BRANCH"
            ) if is_request else None
            decoder = self.original_decoder or self.server_ns.get("decode_sproto")
            fields = {str(k): safe_value(v, decoder) for k, v in body.items()}
            if tag in (2, 3, 4):
                fields = {key: "<redacted-auth-field>" for key in fields}

            matched_server_request = None
            if is_request and session is not None:
                with self.lock:
                    self.pending[(conn.connection_id, session)] = {
                        "tag": tag, "protocol": protocol, "category": category_for(tag),
                        "dispatch_status": dispatch_status, "started": time.monotonic(),
                    }
            elif not is_request and session is not None:
                with self.lock:
                    matched_server_request = self.server_pending.pop(
                        (conn.connection_id, session), None
                    )

            ctx = self.player_context(conn)
            event = {
                "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                "direction": "C2S",
                "kind": kind,
                "category": category_for(tag),
                "tag": tag,
                "protocol_label": protocol,
                "ui_action_hint": (
                    UI_ACTIONS.get(tag, f"Unknown/unmapped UI action for request tag {tag}")
                    if is_request else "Client response to a server-initiated RPC"
                ),
                "server_dispatch": dispatch_status,
                "server_field_hints": self.field_hints.get(tag, {}) if is_request else {},
                "client_expectation": "NOT_ASSERTED: verify against Assembly-CSharp and client Sproto schema",
                "session": session,
                "connection_id": conn.connection_id,
                **ctx,
                "fields": fields,
            }
            if matched_server_request:
                event["response_to_server_request"] = {
                    "tag": matched_server_request["tag"],
                    "protocol_label": matched_server_request["protocol"],
                    "status": "MATCHED_BY_SESSION",
                }
            self.append_json(event)
            self.display_event(event)
        except Exception as exc:
            with self.output_lock:
                sys.__stdout__.write(f"[MONITOR REQUEST TAP ERROR] {exc}\n")
                sys.__stdout__.flush()


    def observe(self, conn, direction: str, frame: bytes) -> None:
        """Capture only outgoing packets; inbound requests use the real decoder hook."""
        if direction != "S2C" or not getattr(conn, "_atg_target", False):
            return
        try:
            decoded = self.decode_frame(frame)
            tag = decoded["tag"]
            session = decoded["session"]
            fields = decoded["fields"]
            now_mono = time.monotonic()
            matched = None
            if session is not None and tag is None:
                with self.lock:
                    matched = self.pending.pop((conn.connection_id, session), None)
                kind = "RESPONSE"
                protocol = (matched["protocol"] + "_response") if matched else "response_by_session"
                category = matched["category"] if matched else "OTHER"
            elif session is not None and tag is not None:
                with self.lock:
                    self.server_pending[(conn.connection_id, session)] = {
                        "tag": tag, "protocol": PROTOCOLS.get(tag, f"unknown_tag_{tag}")
                    }
                kind = "SERVER_REQUEST"
                protocol = PROTOCOLS.get(tag, f"unknown_server_request_tag_{tag}")
                category = category_for(tag)
            elif tag is not None:
                kind = "PUSH"
                protocol = PROTOCOLS.get(tag, f"unknown_push_tag_{tag}")
                category = category_for(tag)
            else:
                kind = "UNCLASSIFIED"
                protocol = "empty_sproto_message"
                category = "OTHER"

            ctx = self.player_context(conn)
            if kind == "PUSH":
                action_hint = PUSH_LABELS.get(tag, "Server push; client handler still needs verification")
            elif kind == "RESPONSE":
                action_hint = "RPC response to the matching client request"
            elif kind == "SERVER_REQUEST":
                action_hint = "Server-initiated RPC request"
            else:
                action_hint = "Unclassified server protocol packet"
            event = {
                "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                "direction": "S2C",
                "kind": kind,
                "category": category,
                "tag": tag,
                "protocol_label": protocol,
                "ui_action_hint": action_hint,
                "client_expectation": "NOT_ASSERTED: verify against Assembly-CSharp and client Sproto schema",
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
            event = {
                "timestamp": datetime.now().astimezone().isoformat(timespec="milliseconds"),
                "direction": direction,
                "kind": "DECODE_ERROR",
                "connection_id": conn.connection_id,
                "error": str(exc)[:300],
                "frame_bytes": len(frame),
                "character_id": self.target_id,
            }
            self.append_json(event)
            with self.output_lock:
                sys.__stdout__.write(f"[MONITOR RESPONSE TAP ERROR] {exc}\n")
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
        title = (
            f"\n[{ts}] {direction} | {kind} | {category}\n"
            f"  TAG={tag}  SPROTO={protocol}  SESSION={session}  MAP={context}"
        )
        title += f"\n  UI / ACTION HINT: {event.get('ui_action_hint', 'Unmapped protocol event')}"
        if kind == "REQUEST":
            title += f"\n  SERVER DISPATCH: {event.get('server_dispatch')}"
            hints = event.get("server_field_hints") or {}
            if hints:
                hint_lines = []
                for field_index, descriptions in sorted(hints.items(), key=lambda item: int(item[0])):
                    hint_lines.extend(descriptions if isinstance(descriptions, list) else [str(descriptions)])
                title += "\n  SERVER FIELD USAGE: " + "; ".join(hint_lines[:20])
        response_to = event.get("response_to")
        if response_to:
            if response_to.get("status") == "MATCHED_BY_SESSION":
                title += (
                    f"\n  RESPONSE TO TAG={response_to['tag']} "
                    f"({response_to['protocol_label']})  LATENCY={response_to['latency_ms']} ms"
                )
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
                    "status": "NO_SESSION_RESPONSE_WITHIN_TIMEOUT",
                    "note": "Not proof of no UI response; related server pushes are logged separately.",
                }
                self.append_json(event)
                with self.output_lock:
                    sys.__stdout__.write(
                        f"\n[NO SESSION RESPONSE] tag={request['tag']} "
                        f"{request['protocol']} session={session} "
                        f"after {self.timeout_sec:g}s (server pushes are logged separately)\n"
                    )
                    sys.__stdout__.flush()


class MonitoredConnection:
    def __init__(self, raw_socket, address, monitor: MonitorState):
        self._sock = raw_socket
        self._monitor = monitor
        self._tx_buffer = bytearray()
        self._buffer_lock = threading.Lock()
        self._atg_target = False
        self.connection_id = id(self)
        self.address = address

    def __getattr__(self, name):
        return getattr(self._sock, name)

    def recv(self, bufsize: int, flags: int = 0):
        data = self._sock.recv(bufsize, flags)
        if data:
            self._monitor.bind_current_thread(self)
        return data

    def _ingest_outgoing(self, data: bytes) -> None:
        if not data:
            return
        with self._buffer_lock:
            self._tx_buffer.extend(data)
            while len(self._tx_buffer) >= 2:
                size = struct.unpack(">H", self._tx_buffer[:2])[0]
                if size == 0 or size > 65535:
                    self._tx_buffer.clear()
                    return
                if len(self._tx_buffer) < size + 2:
                    return
                frame = bytes(self._tx_buffer[:size + 2])
                del self._tx_buffer[:size + 2]
                self._monitor.observe(self, "S2C", frame)

    def sendall(self, data, flags: int = 0):
        if flags:
            self._sock.sendall(data, flags)
        else:
            self._sock.sendall(data)
        self._ingest_outgoing(bytes(data))

    def send(self, data, flags: int = 0):
        sent = self._sock.send(data, flags)
        if sent:
            self._ingest_outgoing(bytes(data[:sent]))
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



def extract_request_field_hints(source: str) -> dict[int, dict[int, list[str]]]:
    """Infer server-side field-use labels from each client_handler msg==TAG branch.

    These are names used by 9555.py, not official Sproto schema names.
    Numeric field indices remain authoritative in the displayed output.
    """
    try:
        tree = ast.parse(source)
    except SyntaxError:
        return {}
    handler = next((node for node in ast.walk(tree)
                    if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef))
                    and node.name == "client_handler"), None)
    if handler is None:
        return {}

    parents: dict[int, ast.AST] = {}
    for parent in ast.walk(handler):
        for child in ast.iter_child_nodes(parent):
            parents[id(child)] = parent

    result: dict[int, dict[int, set[str]]] = {}
    for node in ast.walk(handler):
        if not isinstance(node, ast.If) or not isinstance(node.test, ast.Compare):
            continue
        test = node.test
        if not isinstance(test.left, ast.Name) or test.left.id != "msg" or len(test.comparators) != 1:
            continue
        if not isinstance(test.comparators[0], ast.Constant) or not isinstance(test.comparators[0].value, int):
            continue
        tag = test.comparators[0].value

        # Only inspect this branch body, never its sibling elif/else handlers.
        calls = []
        for statement in node.body:
            calls.extend(ast.walk(statement))
        for call in calls:
            if not isinstance(call, ast.Call):
                continue
            func_name = (call.func.id if isinstance(call.func, ast.Name)
                         else call.func.attr if isinstance(call.func, ast.Attribute) else "")
            index = None
            getter = ""
            if func_name in ("get_val_int", "get_val_str", "get_val_bool", "get_val_float"):
                if len(call.args) >= 2 and isinstance(call.args[0], ast.Name) and call.args[0].id == "body":
                    idx = call.args[1]
                    if isinstance(idx, ast.Constant) and isinstance(idx.value, int):
                        index, getter = idx.value, func_name.replace("get_val_", "")
            elif func_name == "get" and isinstance(call.func, ast.Attribute):
                if isinstance(call.func.value, ast.Name) and call.func.value.id == "body" and call.args:
                    idx = call.args[0]
                    if isinstance(idx, ast.Constant) and isinstance(idx.value, int):
                        index, getter = idx.value, "raw"
            if index is None:
                continue

            assigned = []
            cursor: ast.AST = call
            for _ in range(8):
                parent = parents.get(id(cursor))
                if parent is None:
                    break
                if isinstance(parent, (ast.Assign, ast.AnnAssign, ast.NamedExpr)):
                    targets = parent.targets if isinstance(parent, ast.Assign) else [parent.target]
                    for target in targets:
                        if isinstance(target, ast.Name):
                            assigned.append(target.id)
                        elif isinstance(target, (ast.Tuple, ast.List)):
                            assigned.extend(x.id for x in target.elts if isinstance(x, ast.Name))
                    break
                cursor = parent
            label = "/".join(dict.fromkeys(assigned)) if assigned else f"body[{index}]"
            result.setdefault(tag, {}).setdefault(index, set()).add(
                f"field {index} -> {label} ({getter})"
            )

    return {tag: {index: sorted(values) for index, values in fields.items()}
            for tag, fields in result.items()}


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
    field_hints = extract_request_field_hints(source)
    spec = importlib.util.spec_from_file_location("_atg_server_monitored", server_path)
    if spec is None or spec.loader is None:
        print(f"ERROR: cannot load {server_path}", file=sys.stderr)
        return 2
    server_module = importlib.util.module_from_spec(spec)
    server_ns = server_module.__dict__
    sys.modules[server_module.__name__] = server_module
    monitor = MonitorState(
        args.character_id, server_ns, Path(args.log_dir).expanduser(),
        args.timeout, not args.compact, handler_tags, field_hints
    )
    server_ns["print"] = monitor.server_print

    try:
        spec.loader.exec_module(server_module)
    except Exception:
        import traceback
        traceback.print_exc()
        return 1

    # Tap the real codec functions after startup data has loaded. The request
    # hook triggers on the exact header/body decode pair used in client_handler.
    monitor.original_unpacker = server_module.sproto_unpack
    monitor.original_decoder = server_module.decode_sproto
    original_unpacker = monitor.original_unpacker
    original_decoder = monitor.original_decoder

    def monitored_unpacker(payload):
        raw = original_unpacker(payload)
        monitor.note_sproto_unpacked(raw)
        return raw

    def monitored_decoder(raw, offset):
        decoded = original_decoder(raw, offset)
        monitor.note_sproto_decoded(raw, offset, decoded)
        return decoded

    server_module.sproto_unpack = monitored_unpacker
    server_module.decode_sproto = monitored_decoder

    # Only accepted client sockets are wrapped. Bytes are forwarded unchanged.
    InstrumentedListener.monitor = monitor
    socket.socket = InstrumentedListener

    print("=" * 76)
    print("ATG SINGLE-CHARACTER LIVE SPROTO MONITOR")
    print(f"Character filter : {args.character_id}")
    print(f"Game server file : {server_path}")
    print(f"Game port        : {args.port}")
    print(f"Protocol events  : {monitor.event_path}")
    print(f"Target server log: {monitor.server_path}")
    print("Mode             : actual Sproto decoder hook + outgoing response/push tap; no 9555.py edits")
    print("Other characters : hidden from this terminal and target event files")
    print("UI labels         : protocol action hints + server-side field-use descriptions")
    print("Client expectations: not guessed; UI-only clicks require Unity-side tracing")
    print(f"Handler tags      : {len(handler_tags)} explicit branches; field hints for {len(field_hints)} tags")
    print("=" * 76)
    print("Starting the game server under the monitor. Press Ctrl+C to stop it.\n")

    try:
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
