#!/usr/bin/env python3
"""
atg_sniffer.py - live "truth log" for the ATG game server (9555.py)
====================================================================

A transparent TCP proxy that sits between the game client and 9555.py, decodes
every sproto packet in BOTH directions and tells you in plain text:

  * what the client asked for                (REQ  msg + decoded fields)
  * what the server answered                 (RES  fields, latency)
  * every unsolicited server message         (PUSH tag + decoded fields)
  * whether the server really owns that request, or only sends the generic
    empty catch-all reply                    ([MISS] NOT IMPLEMENTED)
  * requests that never got an answer, double answers, duplicate pushes,
    duplicate reward claims, dropped connections (handler crashed)
  * exact mission rewards compared with the client's own data tables
  * (optional) differences against a recorded "golden" baseline

It does NOT edit or import 9555.py.  It only needs two ports:

    PORT=15679 python 9555.py                       # real server, moved to 15679
    python atg_sniffer.py --listen 15678 --server 127.0.0.1:15679

The client keeps connecting to 15678 and has no idea the sniffer is there.
Only connections of --account (default 68086000311496; every character on that
account is logged) and/or --char are logged, all other players are forwarded
silently.  Use --all to see everybody.

Python 3.8+, standard library only.   python atg_sniffer.py --help
"""
import argparse
import collections
import json
import os
import re
import socket
import struct
import sys
import threading
import time
import traceback

# ----------------------------------------------------------------------------
# Name tables.  Request names were extracted from the msg handlers of 9555.py.
# Push names come from comments in 9555.py - they are labels for humans, NOT
# verified against the client.  Override/extend with --names file.json
# ({"req": {"109": "team_create"}, "push": {"603": ["team_ready", "team"]}}).
# ----------------------------------------------------------------------------
_REQ_NAMES_RAW = """2=visitor
3=verfiy
4=login
7=update_game_server
100=map_ready
101=move
102=skill_use
103=character_list
104=character_create
105=character_pick
106=
107=enter_copy_scene
108=leave_copy_scene
109=team_op_109
110=team_op_110
111=accept_damge
112=accept_mission
113=complete_mission
114=abandon_mission
115=use_item
116=equip_item
117=unequip_item
118=request_random_name
119=ask_pickup_item
120=chat
121=request_daily_mission
122=send_mail
123=mail_operation
124=add_friend
125=del_friend
126=request_update_friend_useinfo
127=single_copy_scene_npc_die
128=local_character_attack
129=sell_item
130=skill_level_up
132=relife_player
137=rank_pvp_other_player_die
143=ask_shop_list
144=buy_shop_item
145=request_daily_copy
146=guild_create
147=guild_join
148=guild_leave
149=guild_kick
150=guild_job_change
151=guild_skill_level
152=guild_req_list
153=guild_req_info
154=guild_approve_resverve
156=title_req_level_up
158=approve_resverve_friend
159=req_random_online_character_list
160=search_online_character_by_name
161=team_op_161
162=team_op_162
163=team_op_163
164=team_op_164
165=get_team_list
166=team_op_166
167=equip_enhance
168=req_offline_chat
169=req_guild_notice
170=req_guild_member_info
173=req_seting_guild_appro
174=guild_log
175=guild_donate
176=search_guild
178=update_misison_parm
179=update_misison_complete
182=update_misison_complete
183=equip_refine
184=equip_inhert
185=change_potion
191=request_top_rank_list
192=change_skill_position
193=car_chase_result
194=guild_boss_enter
195=request_guild_boss
197=equip_badge
198=unequip_badge
199=badge_merge
201=world_boss_enter
202=request_tower_copy_info
207=
210=req_210
212=req_guild_skill
213=team_op_213
214=team_op_214
215=team_op_215
216=team_op_216
217=team_op_217
218=time_sync
220=start_battle
221=equip_fashion_item
222=unequip_fashion_item
223=change_show_type
224=open_item_package
225=request_activity_info
227=request_dance_info
228=pause_participate_dance
229=use_dance
231=request_battle_info
233=open_guild_boss
235=request_mount_info
236=mount_equip
237=mount_unequip
238=use_mount
239=unuse_mount
240=change_item_state
241=mount_use_color
242=request_slot_info
246=
247=guild_invite
252=request_sign_30_day_info
253=request_sign_week_info
254=sign_30_day
255=sign_week
256=request_level_pack
257=request_invest_pack
258=request_daily_buy
261=request_daily_active
262=require_level_reward
265=require_daily_active_reward
266=req_266
267=change_mount_state
268=unlock_function_complete
270=download_finish
273=
275=play_social_dance
276=enter_single_exp_scene
278=request_retrieve_info
279=request_retrieve
281=refresh_online_state
284=send_mail_box
285=req_guild_battle_info
286=enter_guild_battle
287=req_guild_battle_rank
288=req_guild_battle_member
289=set_guild_battle_member
291=req_guild_score_info
293=req_guild_star
294=update_guild_star
295=req_guild_battle_state
296=req_level_reward
297=receive_level_reward
298=impact_npc
299=req_299
301=re_name
302=equip_inhert_move
303=equip_appraise
305=equip_inhert_move
306=tutorial_finish
307=
310=request_domin_info
311=enter_domin_pk_scene
313=request_dance_state_info
314=use_dance_sound_box
315=update_guild_dance_time
316=change_skill_index
318=request_guild_map_domine_top
319=request_guild_map_info
320=request_guild_map_reward
321=gather_other_player
322=guild_capture_enter
323=buy_car_shop
324=update_player_map_info
523=set_mission_state
524=set_mission_param"""

PUSH_INFO = {   # tag: (name, category)
    503: ("enter_map", "map"), 504: ("main_player_create", "map"),
    505: ("aoi_add_player", "aoi"), 507: ("aoi_update_move", "aoi"),
    509: ("npc_create", "npc"), 510: ("aoi_update_attribute/team_update", "aoi"),
    511: ("damage_board", "combat"), 512: ("aoi_relife_player", "aoi"),
    513: ("aoi_stop_move", "aoi"), 514: ("hit_action", "combat"),
    519: ("mission_sync", "mission"), 520: ("mission_update", "mission"),
    521: ("ret_complete_mission", "mission"), 525: ("item_consume", "items"),
    529: ("notice_text", "system"), 540: ("skills_sync", "skill"),
    555: ("daily_copy_state", "copy"), 606: ("tower_copy_info", "copy"),
    629: ("copy_start_info", "copy"), 638: ("reward_popup", "reward"),
    644: ("level_package_state", "reward"), 649: ("activity_rows", "reward"),
    650: ("rewards_display", "reward"), 658: ("retrieve_info", "reward"),
    674: ("level_reward_state", "reward"), 686: ("dance_state", "dance"),
}
LBL = {"REQ": "CLIENT ASKED", "RES": "SERVER REPLIED", "PUSH": "SERVER SENT", "OK": "RESULT OK",
       "INFO": "RESULT INFO", "WARN": "RESULT WARNING", "MISS": "RESULT MISSING", "FAIL": "RESULT PROBLEM"}

# Plain-English meaning of push tags (derived from comments/handlers in 9555.py, not from the client).
PLAIN_PUSH = {
    503: "map entered", 504: "your character created in the map", 505: "another player appeared nearby",
    507: "a player/NPC moved", 508: "skill/hit effect", 509: "NPC appeared", 510: "player/NPC stats or health update",
    511: "damage board update", 512: "player revived", 513: "a player/NPC stopped moving", 514: "hit action",
    519: "mission list refresh", 520: "mission progress update", 521: "mission completion confirmed",
    525: "inventory item changed", 529: "on-screen notice text", 540: "skill list refresh", 555: "daily copy list",
    606: "tower copy info", 629: "dungeon start/timer info", 638: "reward popup", 644: "level gift state",
    649: "activity list", 650: "rewards display", 658: "retrieve (recover) info", 674: "level reward state",
    686: "dance state",
    538: "friend online status (other players)", 581: "gang member list (other players)",
    665: "gang battle members (other players)",
}
# Pushes whose CONTENT is about other players: collapsed to one line unless --show-others
OTHER_PLAYER_PUSHES = {505, 538, 581, 665}

# Field names, ONLY where I verified the layout in 9555.py.  Anything else stays "#N".
#   "name"  or  ("name", {sub-labels})  or  ("name", [{item-labels}])  for lists of structs
_POS = ("position", {0: "x", 1: "y", 2: "z", 3: "direction"})
_ENT = ("entity", {0: "entityId"})
LABELS = {
    ("req", 3): {0: "account"}, ("req", 4): {1: "account", 5: "serverId"},
    ("req", 105): {0: "characterId"}, ("res", 105): {0: "success"},
    ("req", 101): {0: _POS}, ("res", 101): {0: _POS},
    ("req", 112): {0: "missionId"}, ("req", 113): {0: "missionId"}, ("req", 114): {0: "missionId"},
    ("req", 116): {0: "itemIndex"}, ("req", 117): {0: "itemIndex"},
    ("req", 197): {0: "itemIndex"}, ("req", 198): {0: "itemIndex"},
    ("req", 218): {0: "clientTime"}, ("res", 218): {0: "clientTime", 1: "serverTime"},
    ("res", 103): {0: ("characters", [{0: "characterId", 1: ("info", {0: "name", 3: "mapId"})}])},
    ("push", 503): {0: "mapId"},
    ("push", 504): {0: _ENT}, ("push", 505): {0: _ENT}, ("push", 509): {0: _ENT}, ("push", 510): {0: _ENT},
    ("push", 507): {0: "entityId"}, ("push", 508): {0: "entityId"},
    ("push", 519): {0: ("missions", [{0: "missionId", 1: "state"}])},
    ("push", 520): {0: "missionId", 3: ("mission", {0: "missionId", 1: "state"})},
    ("push", 521): {0: "missionId", 1: "result"},
    ("push", 638): {0: ("rewards", [{0: "itemId", 1: "count", 2: "quality"}])},
}


def human(name):
    m = re.match(r"^msg_(\d+)$", name)
    return "unknown request %s" % m.group(1) if m else name.replace("_", " ")


def req_label(msg):
    n = req_name(msg)
    return "request number %d" % msg if re.match(r"^msg_\d+$", n) else "'%s' (msg %d)" % (human(n), msg)


def plain_push(tag):
    if tag is None:
        return "unlabelled message"
    if tag in PLAIN_PUSH:
        return PLAIN_PUSH[tag]
    h = PUSH_HINT.get(tag)
    return "update from the '%s' code" % human(h[0]) if h else "unnamed update"


def item_label(i):
    return {"2001": "EXP", "1001": "cash"}.get(str(i), "item %s" % i)


def describe_pushes(tags):
    if not tags:
        return "no extra messages sent"
    c = collections.OrderedDict()
    for t in tags:
        c[t] = c.get(t, 0) + 1
    parts = ["%s%s" % (plain_push(t), " x%d" % n if n > 1 else "") for t, n in c.items()]
    more = ", +%d more kinds" % (len(parts) - 6) if len(parts) > 6 else ""
    return "%d extra message(s) sent: %s%s" % (len(tags), ", ".join(parts[:6]), more)


NOISY_PUSH = {507, 513}          # AOI movement spam: throttled in the log
NOISY_REQ = {101}                # client move packets (~10/s): throttled
CLAIM_MSGS = {113, 254, 255, 262, 265, 279, 297, 320}   # one-shot reward claims
REWARD_PUSHES = {638, 650}

CAT_RULES = [   # (category, substrings of the request name)
    ("team", ("team",)), ("gang", ("guild", "gang")), ("mission", ("mission",)),
    ("combat", ("skill", "attack", "npc_die", "damge", "relife", "impact", "rank_pvp",
                "car_chase", "battle", "boss")),
    ("map", ("move", "map_ready", "enter_", "leave_", "copy", "gather", "domin", "player_map")),
    ("shop", ("shop", "buy", "sell", "consign")),
    ("items", ("item", "equip", "badge", "fashion", "mount", "potion", "package", "refine",
               "enhance", "inhert", "appraise")),
    ("reward", ("sign", "daily", "level", "invest", "retrieve", "reward", "activity", "slot",
                "title", "tower", "pack")),
    ("social", ("friend", "chat", "mail", "search_online", "random_online", "re_name")),
    ("rank", ("rank", "top_")),
    ("dance", ("dance",)),
    ("login", ("visitor", "verfiy", "login", "character_", "update_game_server",
               "refresh_online", "download", "tutorial", "unlock", "time_sync", "random_name")),
]


def load_req_names():
    names = {}
    for line in _REQ_NAMES_RAW.splitlines():
        if "=" in line:
            k, v = line.split("=", 1)
            if k.strip().isdigit():
                names[int(k)] = v.strip()
    return names


REQ_NAMES = load_req_names()
SERVER_MSGS = set(REQ_NAMES)          # msg ids the server source contains a branch for


def req_name(msg):
    n = REQ_NAMES.get(msg)
    return n if n else "msg_%s" % msg


def req_cat(msg):
    n = req_name(msg).lower()
    for cat, keys in CAT_RULES:
        if any(k in n for k in keys):
            return cat
    return "other"


PUSH_HINT = {}     # tag -> handler/function names in 9555.py that send it (source scan)


def push_name(tag):
    if tag in PUSH_INFO:
        return PUSH_INFO[tag][0]
    h = PUSH_HINT.get(tag)
    return "push_%s~%s" % (tag, "/".join(h[:2])) if h else "push_%s" % tag


def push_cat(tag):
    return PUSH_INFO.get(tag, ("", "other"))[1]


def refresh_from_server_source(path):
    """Re-derive msg ids / names from a (read-only) copy of 9555.py."""
    global SERVER_MSGS
    try:
        text = open(path, encoding="utf-8", errors="ignore").read()
    except OSError as exc:
        print("[sniffer] cannot read --server-src %s: %s" % (path, exc))
        return
    ids = set()
    for m in re.finditer(r"\bmsg\s*==\s*(\d+)", text):
        ids.add(int(m.group(1)))
    for m in re.finditer(r"\bmsg\s+in\s+[\(\[]([\d,\s]+)[\)\]]", text):
        ids.update(int(x) for x in re.findall(r"\d+", m.group(1)))
    for line in text.splitlines():
        m = re.search(r"\bmsg\s*==\s*(\d+)\s*(?:and [^:#]*)?:?\s*#\s*([A-Za-z_]\w*)", line)
        if m and not REQ_NAMES.get(int(m.group(1))):
            REQ_NAMES[int(m.group(1))] = m.group(2)
    SERVER_MSGS = ids | set(REQ_NAMES)
    cur = None
    pat = re.compile(r"(?:send_rpc_push\s*\(|send_rpc_push_safe\s*\(\s*\w+\s*,|send_push_to_char\s*\([^,]+,"
                     r"|exp_send_push\s*\([^,]+,|send_push\s*\(|encode_sproto\(\[\(0,)\s*(\d{3})\b")
    for line in text.splitlines():
        d = re.match(r"def (\w+)", line)
        if d:
            cur = d.group(1)
        m = re.match(r"\s*(?:el)?if msg\s*(?:==\s*(\d+)|in\s*[\(\[]\s*(\d+))", line)
        if m:
            cur = req_name(int(m.group(1) or m.group(2)))
        for pm in pat.finditer(line):
            lst = PUSH_HINT.setdefault(int(pm.group(1)), [])
            if cur and cur not in lst:
                lst.append(cur)
    print("[sniffer] server source scanned: %d request ids have a handler branch" % len(ids))


# ----------------------------------------------------------------------------
# sproto codec (independent re-implementation of what 9555.py speaks)
# ----------------------------------------------------------------------------
def sp_unpack(data):
    out = bytearray()
    i, n = 0, len(data)
    while i < n:
        mask = data[i]
        i += 1
        if mask == 0xFF:
            if i >= n:
                break
            count = (data[i] + 1) * 8
            i += 1
            out.extend(data[i:i + count])
            i += count
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < n:
                        out.append(data[i])
                        i += 1
                else:
                    out.append(0)
    return bytes(out)


def sp_pack(data):                     # used by --selftest only
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i + 8]
        if len(chunk) < 8:
            chunk += b"\x00" * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j]:
                mask |= 1 << j
        if mask == 0xFF:
            out += b"\xff\x00" + chunk
        else:
            out.append(mask)
            out.extend(b for b in chunk if b)
    return bytes(out)


def sp_encode(fields):                 # used by --selftest only
    fields = sorted(fields, key=lambda f: f[0])
    header, body, last = [], bytearray(), -1
    for tag, val in fields:
        if tag - last - 1 > 0:
            header.append(2 * (tag - last - 2) + 1)
        if isinstance(val, int) and 0 <= val <= 32766:
            header.append((val + 1) * 2)
        else:
            header.append(0)
            if isinstance(val, int):
                v = struct.pack("<i", val) if -2**31 <= val < 2**31 else struct.pack("<q", val)
            elif isinstance(val, str):
                v = val.encode()
            elif isinstance(val, list):
                v = b"".join(struct.pack("<I", len(x)) + x for x in val)
            else:
                v = bytes(val)
            body += struct.pack("<I", len(v)) + v
        last = tag
    return struct.pack("<H", len(header)) + b"".join(struct.pack("<H", h) for h in header) + bytes(body)


def parse_struct(buf, off=0):
    """Strict sproto struct parse -> (fields, end_offset) or None.
    small ints -> int, everything else -> bytes (length-prefixed)."""
    if len(buf) < off + 2:
        return None
    fn = struct.unpack_from("<H", buf, off)[0]
    hp = off + 2
    bp = hp + fn * 2
    if bp > len(buf):
        return None
    fields, tag = {}, -1
    for i in range(fn):
        v = struct.unpack_from("<H", buf, hp + 2 * i)[0]
        if v == 0:
            tag += 1
            if bp + 4 > len(buf):
                return None
            ln = struct.unpack_from("<I", buf, bp)[0]
            if bp + 4 + ln > len(buf):
                return None
            fields[tag] = bytes(buf[bp + 4:bp + 4 + ln])
            bp += 4 + ln
        elif v & 1:
            tag += (v >> 1) + 1
        else:
            tag += 1
            fields[tag] = (v >> 1) - 1
    return fields, bp


def get_int(fields, tag, default=None):
    v = fields.get(tag)
    if v is None:
        return default
    if isinstance(v, int):
        return v
    if len(v) == 4:
        return struct.unpack("<i", v)[0]
    if len(v) == 8:
        return struct.unpack("<q", v)[0]
    if len(v) == 1:
        return v[0]
    return default


def get_str(fields, tag, default=""):
    v = fields.get(tag)
    if isinstance(v, (bytes, bytearray)):
        return v.decode("utf-8", "replace")
    if isinstance(v, int):
        return str(v)
    return default


AOI_ENTITY_PUSHES = {505, 507, 508, 509, 510, 512, 513, 514}   # world updates about ONE entity


def push_entity_id(body):
    """Entity (player/NPC id) an AOI push is about: #0 is either the id or a struct whose #0 is the id."""
    v = body.get(0)
    if isinstance(v, int):
        return v
    if isinstance(v, (bytes, bytearray)):
        st = parse_struct(v, 0)
        if st and st[1] == len(v) and 0 in st[0]:
            return get_int(st[0], 0)
        return get_int(body, 0)
    return None


def summarize_tags(tags, limit=14):
    """[538,538,538,537] -> '538x3, 537'  (keeps first-seen order)"""
    c = collections.OrderedDict()
    for t in tags:
        c[t] = c.get(t, 0) + 1
    parts = ["%s%s" % ("none" if t is None else t, "x%d" % n if n > 1 else "") for t, n in c.items()]
    return "[" + ", ".join(parts[:limit]) + (", ... +%d more kinds" % (len(parts) - limit) if len(parts) > limit else "") + "]"


def parse_list(b):
    """Array of length-prefixed elements covering b exactly -> list[bytes] or None."""
    out, p = [], 0
    while p < len(b):
        if p + 4 > len(b):
            return None
        ln = struct.unpack_from("<I", b, p)[0]
        if p + 4 + ln > len(b):
            return None
        out.append(b[p + 4:p + 4 + ln])
        p += 4 + ln
    return out if out else None


def printable(b):
    try:
        s = b.decode("utf-8")
    except UnicodeDecodeError:
        return None
    return s if s and all(ch.isprintable() for ch in s) else None


def render_bytes(b, depth=0, sub=None):
    """Best-effort pretty print of an untyped sproto value (no schema available).
    sub = optional labels: dict (struct) or [dict] (list of structs)."""
    if not b:
        return '""'
    st = parse_struct(b, 0) if depth < 5 else None
    if st and st[1] == len(b) and (st[0] or len(b) == 2):
        return render_fields(st[0], depth + 1, sub if isinstance(sub, dict) else None)
    if len(b) in (4, 8):
        # 4/8 bytes is ambiguous (int32/int64 or a short string). Without the schema:
        # all-ASCII-alphanumeric -> text (item ids like "2001"), otherwise -> integer.
        if all(0x30 <= ch <= 0x39 or 0x41 <= ch <= 0x5a or 0x61 <= ch <= 0x7a or ch in (0x5f, 0x2d, 0x2e)
               for ch in b):
            return '"%s"' % b.decode("ascii")
        return str(struct.unpack("<i" if len(b) == 4 else "<q", b)[0])
    if b[0] in (4, 8) and (len(b) - 1) % b[0] == 0 and len(b) > 1:
        w = b[0]
        fmt = "<i" if w == 4 else "<q"
        return "[" + ",".join(str(struct.unpack_from(fmt, b, 1 + k * w)[0])
                              for k in range((len(b) - 1) // w)) + "]"
    s_ = printable(b)
    if s_ is not None:
        return '"%s"' % s_
    lst = parse_list(b) if depth < 5 else None
    if lst:
        item_sub = sub[0] if isinstance(sub, list) and sub else None
        return "[" + ", ".join(render_bytes(x, depth + 1, item_sub) for x in lst) + "]"
    return "0x" + b.hex() if len(b) <= 24 else "0x%s..(%dB)" % (b[:24].hex(), len(b))


def render_fields(fields, depth=0, labels=None):
    if not fields:
        return "{}"
    parts = []
    for tag in sorted(fields):
        v = fields[tag]
        lab = labels.get(tag) if labels else None
        name, sub = "#%d" % tag, None
        if isinstance(lab, str):
            name = lab
        elif isinstance(lab, tuple):
            name, sub = lab
        parts.append("%s=%s" % (name, v if isinstance(v, int) else render_bytes(v, depth, sub)))
    return "{" + ", ".join(parts) + "}"


# ----------------------------------------------------------------------------
# Client data tables (reward verification)
# ----------------------------------------------------------------------------
class Tables:
    def __init__(self):
        self.mission_rewards = {}      # mission id -> list of reward-id strings
        self.rewards = {}              # reward id -> {"2001": exp, "1001": cash, item: n}
        self.mission_names = {}
        self.source = "(none)"

    @staticmethod
    def find_root(explicit=None):
        here = os.path.dirname(os.path.abspath(__file__))
        cands = [explicit] if explicit else []
        for base in (here, os.getcwd()):
            for rel in ("assets/Bundle/TextAsset", "assets/Bundle/TextAssets",
                        "Decompiled/assets/Bundle/TextAsset",
                        "Decompiled/Decompiled/assets/Bundle/TextAsset",
                        "ATG-project-main/Decompiled/Decompiled/assets/Bundle/TextAsset", "."):
                cands.append(os.path.join(base, rel))
        for c in cands:
            if c and os.path.isdir(c) and (os.path.exists(os.path.join(c, "MissionData"))
                                           or Tables.find_json("missions.json", c)):
                return c
        return None

    @staticmethod
    def find_json(name, root):
        here = os.path.dirname(os.path.abspath(__file__))
        for base in (root, os.path.join(root, "..", "..", ".."), here, os.getcwd(),
                     os.path.join(here, "dec&normal", "Decompiled"), os.path.join(here, "decompiled_src"),
                     os.path.join(here, "Decompiled", "Decompiled"),
                     os.path.join(os.getcwd(), "dec&normal", "Decompiled"),
                     os.path.join(os.getcwd(), "decompiled_src")):
            pth = os.path.join(base, name)
            if os.path.exists(pth):
                return pth
        return None

    def load(self, root):
        if not root:
            return
        loaded = []
        # reward definitions: JSON first (same priority 9555.py uses), then ShowRewardData
        for jp in [Tables.find_json("mission_rewards.json", root)]:
            if jp:
                try:
                    raw = json.load(open(jp, encoding="utf-8"))
                    for rid, rv in raw.items():
                        d = {}
                        if int(rv.get("exp", 0)):
                            d["2001"] = int(rv["exp"])
                        if int(rv.get("cash", 0)):
                            d["1001"] = int(rv["cash"])
                        for i, amt in zip(rv.get("items", []), rv.get("item_amounts", [])):
                            if i:
                                d[str(i)] = d.get(str(i), 0) + int(amt)
                        self.rewards[str(rid)] = d
                    loaded.append("mission_rewards.json")
                    break
                except Exception as exc:
                    print("[sniffer] mission_rewards.json: %s" % exc)
        srd = os.path.join(root, "ShowRewardData")
        if not self.rewards and os.path.exists(srd):
            for line in open(srd, encoding="utf-8", errors="ignore"):
                p = line.strip().split(",")
                if len(p) > 5 and p[0] == "*" and p[1].isdigit():
                    d = {}
                    for i in range(8):
                        a, c = 3 + i * 3, 5 + i * 3
                        if c < len(p) and p[a].isdigit():
                            d[p[a]] = d.get(p[a], 0) + (int(p[c]) if p[c].isdigit() else 1)
                    self.rewards[p[1]] = d
            loaded.append("ShowRewardData")
        # mission -> reward ids
        md = os.path.join(root, "MissionData")
        if os.path.exists(md):
            for line in open(md, encoding="utf-8", errors="ignore"):
                p = line.strip().split(",")
                if len(p) > 23 and p[0] == "*" and p[1].isdigit():
                    self.mission_rewards[p[1]] = [p[25] if len(p) > 25 else "",
                                                  p[27] if len(p) > 27 else "",
                                                  p[29] if len(p) > 29 else ""]
                    self.mission_names[p[1]] = p[2]
            loaded.append("MissionData")
        for jp in [Tables.find_json("missions.json", root)]:
            if jp:
                try:
                    raw = json.load(open(jp, encoding="utf-8"))
                    for mid, mv in raw.items():
                        if str(mid) not in self.mission_rewards:
                            self.mission_rewards[str(mid)] = [str(x) for x in mv.get("reward_ids", [])]
                        self.mission_names.setdefault(str(mid), mv.get("name", ""))
                    loaded.append("missions.json")
                    break
                except Exception as exc:
                    print("[sniffer] missions.json: %s" % exc)
        self.source = "%s (%s)" % (root, ", ".join(loaded) or "nothing usable")

    def expected_options(self, mid):
        """[(reward_id, {item: count})] - one per profession slot."""
        out = []
        for rid in self.mission_rewards.get(str(mid), []):
            if rid and rid in self.rewards:
                out.append((rid, self.rewards[rid]))
        return out


# ----------------------------------------------------------------------------
# Logging
# ----------------------------------------------------------------------------
class Out:
    COL = {"OK": "32", "INFO": "36", "WARN": "33", "MISS": "35", "FAIL": "31",
           "REQ": "1;37", "RES": "37", "PUSH": "34", "DIM": "90"}

    def __init__(self, color, logfile, maxlen=0):
        self.maxlen = maxlen
        self.color = color
        self.fh = open(logfile, "a", encoding="utf-8") if logfile else None
        self.lock = threading.RLock()
        self.last_print = 0.0

    def line(self, kind, text, ts=None):
        ts = ts or time.time()
        stamp = time.strftime("%H:%M:%S", time.localtime(ts)) + ".%03d" % int((ts % 1) * 1000)
        with self.lock:
            if self.last_print and ts - self.last_print > 1.2:
                self._w("", None)
                self._w("---------------- new action %s ----------------" % stamp, "DIM")
            self.last_print = ts
            self._w("%s %s" % (stamp, text), kind)

    def raw(self, text, kind=None):
        with self.lock:
            self._w(text, kind)

    def _w(self, text, kind):
        plain = text
        if self.maxlen and len(text) > self.maxlen:
            text = text[:self.maxlen] + " ...(+%d chars, full text in --log)" % (len(text) - self.maxlen)
        if self.color and kind in self.COL:
            text = "\033[%sm%s\033[0m" % (self.COL[kind], text)
        try:
            print(text, flush=True)
        except UnicodeEncodeError:
            print(text.encode("ascii", "replace").decode(), flush=True)
        if self.fh:
            self.fh.write(plain + "\n")
            self.fh.flush()


# ----------------------------------------------------------------------------
# Core state
# ----------------------------------------------------------------------------
NPC_REQ_CATS = {"combat", "map", "login"}   # NPC world-updates are only credited to requests of these kinds


class ConnStats:
    """Everything the final report needs, kept PER CONNECTION so that other players never leak into the totals."""

    def __init__(self):
        self.stat = collections.defaultdict(collections.Counter)
        self.lat = collections.defaultdict(list)
        self.unhandled = {}
        self.push_seen = collections.Counter()
        self.speeds = []
        self.max_jump = (0, None)
        self.record = {"version": 1, "msgs": {}, "pushes": {}}
        self.unknown = {}                # (kind, id) -> {"n", "example", "reply", "after", "hex"}
        self.layouts = {}                # (kind, id) -> [count, example]  formats nobody has labelled yet
        self.hidden = collections.Counter()   # world updates about other players / NPCs that were not shown


class Exchange:
    __slots__ = ("seq", "msg", "session", "body", "t0", "t_last", "resp", "resp_t", "n_resp",
                 "pushes", "done", "name", "cat", "raw_body", "noisy", "issues", "custom_ok", "headerless",
                 "unknown", "shown", "hid")

    def __init__(self, seq, msg, session, body, raw_body, now):
        self.seq, self.msg, self.session, self.body, self.raw_body = seq, msg, session, body, raw_body
        self.t0 = self.t_last = now
        self.resp, self.resp_t, self.n_resp = None, None, 0
        self.pushes = []                 # (tag, fields, raw_bytes, t)
        self.done = False
        self.name = req_name(msg)
        self.cat = req_cat(msg)
        self.noisy = msg in NOISY_REQ
        self.issues = 0
        self.custom_ok = False
        self.headerless = 0
        self.unknown = bool(re.match(r"^msg_\d+$", self.name))   # the sniffer has no name for this request
        self.shown = {}                  # push tag -> how many were printed for this request
        self.hid = {}                    # push tag -> how many identical-kind pushes were collapsed


class Conn:
    def __init__(self, cid, peer):
        self.cid, self.peer = cid, peer
        self.char_id = None
        self.account = ""                # account id sent in the login request (msg 4, field #1)
        self.target = None               # None = not decided yet, True = log it, False = ignore (muted)
        self.muted = False
        self.is_http = False
        self.S = ConnStats()
        self.npc_ids = set()             # entity ids learned from npc_create pushes
        self.unk_buf = []                # unknown-log lines held back until we know whose connection this is
        self.cur_frame = b""
        self.verd = collections.Counter()
        self.problems = {}               # (level, request, text-with-numbers-stripped) -> [count, example text]
        self.seq = 0                     # per-connection request counter (#1, #2, ...)
        self.eof_by = None               # which side hung up first
        self.closed_flag = False
        self.late = {}                   # session -> finished Exchange (to spot late/double replies)
        self.buf = []                    # (kind, text, ts, cat) before identification
        self.pending = {}                # session -> Exchange
        self.open = []                   # exchanges not finalised
        self.recent_pushes = collections.deque(maxlen=64)   # (tag, rawbytes, t)
        self.claims = {}                 # (msg, body) -> count of paid claims
        self.paid_missions = set()
        self.last_move = None            # (t, x, z)
        self.noise = {}                  # key -> [last_print, suppressed]
        self.last_req = None


class Sniffer:
    def __init__(self, args):
        self.a = args
        self.out = Out(not args.no_color and sys.stdout.isatty(), args.log, args.maxlen)
        self.lock = threading.RLock()
        self.seq = 0
        self.conns = {}
        self.tables = Tables()
        if args.rewards_check:
            self.tables.load(Tables.find_root(args.data or None))
        self.only = set(x.strip() for x in args.only.split(",")) if args.only else None
        self.baseline = None
        if args.baseline:
            self.baseline = json.load(open(args.baseline, encoding="utf-8"))
        # statistics live per connection (see ConnStats)
        self.unk_fh = open(args.unknown_log, "a", encoding="utf-8") if args.unknown_log else None
        self.verdicts = collections.Counter()
        self.stop = False
        threading.Thread(target=self.janitor, daemon=True).start()

    # ---------------- output helpers ------------------------------------
    def emit(self, conn, kind, text, cat="other", ts=None, force=False, unk=False):
        ts = ts or time.time()
        with self.lock:
            if conn.muted:
                return
            if conn.target is None and not self.a.all and not force:
                conn.buf.append((kind, text, ts, cat, unk))
                if len(conn.buf) > 400:
                    conn.buf.pop(0)
                return
            if self.only and cat not in self.only and kind not in ("FAIL",) and not force:
                return
            if self.a.only_unknown and not unk and kind != "FAIL" and not force:
                return
            tag = "" if not self.a.all else "[c%d] " % conn.cid
            self.out.line(kind, tag + text, ts)

    # ---------------- unknowns ------------------------------------------
    def unknown_file(self, line):
        if self.unk_fh:
            self.unk_fh.write(line + "\n")
            self.unk_fh.flush()

    def note_unknown(self, conn, kind, ident, example, after=None, reply=None, count=True):
        """Remember something the sniffer cannot name; first 3 occurrences go to the unknown log with raw bytes."""
        u = conn.S.unknown.setdefault((kind, ident), {"n": 0, "example": example, "reply": None, "after": [],
                                                      "hex": conn.cur_frame.hex()})
        if count:
            u["n"] += 1
        if reply is not None and u["reply"] is None:
            u["reply"] = reply
        if after and after not in u["after"] and len(u["after"]) < 6:
            u["after"].append(after)
        if (count and u["n"] <= 3) or (reply is not None and u["reply"] == reply):
            stamp = time.strftime("%Y-%m-%d %H:%M:%S")
            self.write_unk(conn, "%s | UNKNOWN %s %s | seen %dx | %s%s | raw packet hex: %s" % (
                stamp, kind, ident, u["n"], "REPLY: " if reply is not None else "data: ",
                reply if reply is not None else example, conn.cur_frame.hex()))

    def write_unk(self, conn, line):
        if conn.target is None and not self.a.all:
            conn.unk_buf.append(line)
        elif not conn.muted:
            self.unknown_file(line)

    def note_layout(self, conn, kind, ident, example):
        """A request/reply/push body whose fields have no names yet. File only (too many for the console)."""
        d = conn.S.layouts.setdefault((kind, ident), [0, example])
        d[0] += 1
        if d[0] <= 2:
            self.write_unk(conn, "%s | LAYOUT NOT LABELLED %s %s | seen %dx | example: %s | raw packet hex: %s" % (
                time.strftime("%Y-%m-%d %H:%M:%S"), kind, ident, d[0], example, conn.cur_frame.hex()))

    def check_fields(self, conn, kind, ident, body, labels, txt, after=None):
        """Unlabelled message layouts + unexpected extra fields in messages that DO have a schema."""
        if not body:
            return
        if not labels:
            self.note_layout(conn, kind + " layout", ident, txt)
            return
        for t in sorted(set(body) - set(labels)):
            self.note_unknown(conn, "unlabelled field", "%s %s #%d" % (kind, ident, t), txt, after=after)

    def throttle(self, conn, key):
        """True if this noisy line should be printed now (adds suppressed counter)."""
        now = time.time()
        n = conn.noise.setdefault(key, [0.0, 0])
        if self.a.noisy or now - n[0] >= self.a.noise_interval:
            sup, n[0], n[1] = n[1], now, 0
            return True, sup
        n[1] += 1
        return False, 0

    # ---------------- connection lifecycle ------------------------------
    def new_conn(self, peer):
        with self.lock:
            cid = len(self.conns) + 1
            c = Conn(cid, peer)
            self.conns[cid] = c
        return c

    def accept(self, conn, why):
        """This connection belongs to the watched account/character: flush what was held back and log from now on."""
        if conn.target is not None:
            return
        conn.target = True
        buf, conn.buf = conn.buf, []
        self.out.raw("[sniffer] %s on connection c%d (%s) - logging" % (why, conn.cid, conn.peer), "OK")
        for kind, text, ts, cat, unk in buf:
            if self.only and cat not in self.only and kind != "FAIL":
                continue
            if self.a.only_unknown and not unk and kind != "FAIL":
                continue
            self.out.line(kind, text, ts)
        for line in conn.unk_buf:
            self.unknown_file(line)
        conn.unk_buf = []

    def reject(self, conn):
        """Somebody else: forward their traffic silently, never analyse, print or count it."""
        conn.target = False
        conn.muted = True
        conn.buf = []
        conn.unk_buf = []

    def login_seen(self, conn, account):
        """msg 4 (login): the account id is known before any character is picked."""
        conn.account = account
        if self.a.all or conn.target is not None:
            return
        if self.a.account:
            if account == self.a.account:
                self.accept(conn, "account %s logged in" % account)
            elif not self.a.char:
                self.reject(conn)            # not our account and no character filter -> decide right now
            # else: wait for the character pick (--char may still match)

    def identified(self, conn, char_id):
        # (msg 105 character_pick: request field #0 is the character id)
        with self.lock:
            conn.char_id = char_id
            if self.a.all or conn.target is not None:
                return                       # already accepted by account (or filter off) - char_id is now known
            if self.a.char and char_id == self.a.char:
                self.accept(conn, "character %d picked" % char_id)
            else:
                self.reject(conn)

    def closed(self, conn, who):
        with self.lock:
            if conn.muted or conn.closed_flag:
                return
            conn.closed_flag = True
            for ex in list(conn.open):
                if ex.session is not None and ex.resp is None:
                    if who == "server":
                        self.issue(conn, ex, "FAIL", "THE SERVER CLOSED THE CONNECTION while it was handling '%s'. "
                                   "The code in 9555.py almost certainly crashed - read the error in the server console."
                                   % human(ex.name))
                    else:
                        self.issue(conn, ex, "WARN", "the client disconnected before the server answered '%s'" % human(ex.name))
                self.finalize(conn, ex, force=True)
            self.emit(conn, "INFO", "--  CONNECTION       closed (%s hung up first)" % ("the game server" if who == "server" else "the game client"),
                      force=bool(conn.target))

    # ---------------- frame handling ------------------------------------
    def on_frame(self, conn, direction, frame):
        if conn.muted:                    # other players: forwarded, never analysed or counted
            return
        now = time.time()
        conn.cur_frame = frame
        raw = sp_unpack(frame)
        hdr = parse_struct(raw, 0)
        if not hdr:
            self.emit(conn, "WARN", "--  BAD PACKET       could not decode a %s packet (%d bytes): %s  <-- UNKNOWN" % (
                "client->server" if direction == "C2S" else "server->client", len(frame), frame[:24].hex()), unk=True)
            with self.lock:
                self.note_unknown(conn, "bad packet", "%s %dB" % (direction, len(frame)), frame[:40].hex())
            return
        pkg, end = hdr
        body_p = parse_struct(raw, end)
        body = body_p[0] if body_p else {}
        raw_body = raw[end:body_p[1]] if body_p else b""
        with self.lock:
            if direction == "C2S":
                self.on_request(conn, pkg, body, raw_body, now)
            else:
                self.on_server(conn, pkg, body, raw_body, now, body_p is not None)

    def on_request(self, conn, pkg, body, raw_body, now):
        msg = get_int(pkg, 0)
        session = get_int(pkg, 1) if 1 in pkg else None
        if msg is None:
            self.emit(conn, "WARN", "--  BAD PACKET       client packet without a request number: %s" % render_fields(pkg))
            return
        conn.seq += 1
        ex = Exchange(conn.seq, msg, session, body, raw_body, now)
        conn.last_req = ex
        conn.open.append(ex)
        if session is not None:
            if session in conn.pending and conn.pending[session].resp is None:
                self.issue(conn, conn.pending[session], "WARN", "the client reused session number %d before the earlier request was answered" % session)
            conn.pending[session] = ex
        st = conn.S.stat[msg]
        st["n"] += 1
        if msg == 4:
            self.login_seen(conn, get_str(body, 1))
        if msg == 105:
            cid = get_int(body, 0, 0)
            self.identified(conn, cid)
        show = True
        sup = 0
        if ex.noisy:
            show, sup = self.throttle(conn, ("req", msg))
        if msg == 101:
            self.track_move(conn, body, now)
        if show:
            extra = " (+%d similar suppressed)" % sup if sup else ""
            how = ("waits for a direct reply" if session is not None
                   else "notification - the server answers with messages, not a direct reply")
            txt = render_fields(body, 0, LABELS.get(("req", msg)))
            flag = "  <-- UNKNOWN request (no name known)" if ex.unknown else ""
            self.emit(conn, "REQ", "#%d %-15s %s [%s]  (msg %d, %s)  %s%s%s" % (
                ex.seq, LBL["REQ"], human(ex.name), ex.cat, msg, how, txt, extra, flag), ex.cat, now, unk=ex.unknown)
            if "0x" in txt:
                self.note_unknown(conn, "undecodable data", "request %d" % msg, txt, after=ex.name)
            if not ex.unknown:
                self.check_fields(conn, "request", msg, body, LABELS.get(("req", msg)), txt, after=ex.name)
        if ex.unknown:
            self.note_unknown(conn, "request", msg, render_fields(body),
                              after="9555.py has %s" % ("a handler" if msg in SERVER_MSGS else "NO handler"))
        # duplicate request check (client double-send)
        key = ("req", msg, raw_body)
        last = conn.noise.get(key)
        conn.noise[key] = [now, 0]
        if len(conn.noise) > 4000:
            conn.noise = {k: v for k, v in conn.noise.items() if k and k[0] != "req"}
        if last and now - last[0] < self.a.dup_window and msg not in NOISY_REQ and msg != 100:
            self.issue(conn, ex, "WARN", "the client sent the exact same '%s' request again after %d ms (double click or "
                       "resend). Make sure the server does not process or pay it twice." % (human(ex.name), int((now - last[0]) * 1000)))

    def on_server(self, conn, pkg, body, raw_body, now, ok_body):
        is_resp = 1 in pkg and 0 not in pkg
        if is_resp:
            session = get_int(pkg, 1)
            ex = conn.pending.get(session)
            if ex is None:
                old = conn.late.get(session)
                if old is not None and old.resp is None:
                    old.resp, old.resp_t = body, now
                    self.issue(conn, old, "WARN", "the reply to '%s' finally arrived LATE (%.1f s) - after it had already "
                               "been reported as 'never answered'" % (human(old.name), now - old.t0))
                elif old is not None:
                    self.issue(conn, old, "FAIL", "DOUBLE ANSWER: the server answered '%s' a second time. The request was "
                               "probably processed twice." % human(old.name))
                else:
                    self.emit(conn, "WARN", "--  SERVER REPLIED   with session number %s but the client never sent a request "
                              "with that number: %s" % (session, render_fields(body)))
                return
            ex.n_resp += 1
            ex.t_last = now
            if ex.n_resp > 1:
                self.issue(conn, ex, "FAIL", "DOUBLE ANSWER: the server answered '%s' %d times. The request was "
                           "probably processed twice." % (human(ex.name), ex.n_resp))
                return
            ex.resp, ex.resp_t = body, now
            lat = (now - ex.t0) * 1000
            conn.S.lat[ex.msg].append(lat)
            if not body:
                conn.S.stat[ex.msg]["empty"] += 1
            show = True
            if ex.noisy:
                show, _ = self.throttle(conn, ("res", ex.msg))
            if show:
                rtxt = render_fields(body, 0, LABELS.get(("res", ex.msg)))
                self.emit(conn, "RES", "#%d %-15s %s in %d ms  %s%s" % (
                    ex.seq, LBL["RES"], "an empty OK (no data)" if not body else "%d value(s)" % len(body), lat,
                    rtxt, "  <-- reply to an UNKNOWN request" if ex.unknown else ""), ex.cat, now, unk=ex.unknown)
                if "0x" in rtxt:
                    self.note_unknown(conn, "undecodable data", "reply to %d" % ex.msg, rtxt, after=ex.name)
                if not ex.unknown:
                    self.check_fields(conn, "reply", ex.msg, body, LABELS.get(("res", ex.msg)), rtxt, after=ex.name)
            if ex.unknown:
                self.note_unknown(conn, "request", ex.msg, "", reply=render_fields(body), count=False)
            return
        tag = get_int(pkg, 0)
        S = conn.S
        if tag is None:
            # Neither a push tag nor a session: the client cannot route this frame.  9555.py does this when it
            # answers a request that carried no session (encode_sproto([(1, None)])).
            owner = self.pick_owner(conn, None, now)
            if owner is not None:
                owner.headerless += 1
                owner.t_last = now
                if owner.headerless == 1:
                    self.issue(conn, owner, "WARN", "the server sent a message with NO LABEL (empty header) after '%s'. It looks "
                               "like a reply to a request that had no session number, so the client cannot match it "
                               "to anything. Content: %s" % (human(owner.name), render_fields(body)))
            else:
                self.emit(conn, "WARN", "--  SERVER SENT      a message with NO LABEL (empty header) that the client cannot "
                          "route: %s  <-- UNKNOWN" % render_fields(body), unk=True)
            S.stat["headerless"]["n"] += 1
            self.note_unknown(conn, "unlabelled message", "after %s" % (owner.name if owner else "nothing"),
                              render_fields(body), after=owner.name if owner else None)
            return
        # ---- world updates (AOI) about ONE entity: other players are never shown, NPCs only when you caused them
        other = None
        if tag in AOI_ENTITY_PUSHES and conn.char_id is not None:
            eid = push_entity_id(body)
            if tag == 509 and eid is not None:
                conn.npc_ids.add(eid)
            if eid is not None and eid != conn.char_id:
                other = ("npc" if (tag == 509 or eid in conn.npc_ids) else "player", eid)
        owner = None
        if other is None:
            owner = self.pick_owner(conn, tag, now)
        elif other[0] == "npc":
            cand = self.pick_owner(conn, tag, now)
            if cand is not None and cand.cat in NPC_REQ_CATS:
                owner = cand
        if other is not None and owner is None:
            S.hidden[other[0]] += 1
            if not self.a.show_others:
                return
        S.push_seen[tag] += 1
        cat = push_cat(tag) if owner is None else owner.cat
        if owner:
            owner.pushes.append((tag, body, raw_body, now))
            if tag not in NOISY_PUSH:
                owner.t_last = now
        # duplicate push (same bytes within window)
        dup = False
        for (t2, b2, ts2) in conn.recent_pushes:
            if t2 == tag and b2 == raw_body and now - ts2 < self.a.dup_window and tag not in NOISY_PUSH:
                dup = True
                break
        conn.recent_pushes.append((tag, raw_body, now))
        txt = render_fields(body, 0, LABELS.get(("push", tag)))
        unk = tag not in PUSH_INFO and tag not in PLAIN_PUSH and tag not in PUSH_HINT
        if unk:
            self.note_unknown(conn, "push", tag, txt, after=owner.name if owner else None)
        if "0x" in txt:
            self.note_unknown(conn, "undecodable data", "push %d" % tag, txt, after=owner.name if owner else None)
        if not unk:
            self.check_fields(conn, "push", tag, body, LABELS.get(("push", tag)), txt, after=owner.name if owner else None)
        others_data = tag in OTHER_PLAYER_PUSHES and not self.a.show_others
        if others_data:
            S.hidden["player"] += 1
            txt = "(contains other players' data - hidden, use --show-others to see it)"
        show, sup = (True, 0)
        if tag in NOISY_PUSH:
            show, sup = self.throttle(conn, ("push", tag))
        elif owner is not None and not self.a.noisy:
            owner.shown[tag] = owner.shown.get(tag, 0) + 1
            if owner.shown[tag] > (1 if others_data else self.a.max_same):          # collapse long runs of the same kind
                owner.hid[tag] = owner.hid.get(tag, 0) + 1
                show = False
        if show:
            extra = " (+%d similar suppressed)" % sup if sup else ""
            lead = "#%d" % owner.seq if owner else "--"
            who = ""
            if other:
                who = ", about %s %s" % ("an NPC" if other[0] == "npc" else "another player", other[1])
            elif not owner:
                who = ", not caused by a click"
            self.emit(conn, "PUSH", "%s %-15s %s [%s]  (push %d%s)  %s%s%s" % (
                lead, LBL["PUSH"], plain_push(tag), push_cat(tag), tag, who, txt, extra,
                "  <-- UNKNOWN message (no name known)" if unk else ""), cat, now, unk=unk)
        if dup:
            self.issue(conn, owner, "FAIL", "DUPLICATE: the server sent the identical message '%s' (push %d) twice within "
                       "%d ms. The client will apply it twice (double damage / reward / spawn)." %
                       (plain_push(tag), tag, int(self.a.dup_window * 1000)), cat=cat, tag_only=owner is None)

    def pick_owner(self, conn, tag, now):
        """Which open request does this push belong to?  Newest request inside the attribution window,
        preferring the request whose handler (per the 9555.py source scan) is known to send this tag."""
        cands = [ex for ex in conn.open if now - ex.t0 < self.a.attr_window]
        if not cands:
            return None
        hints = PUSH_HINT.get(tag, []) if tag is not None else []
        if hints:
            for ex in reversed(cands):
                if ex.name in hints:
                    return ex
        return cands[-1]

    # ---------------- verdicts -------------------------------------------
    def result(self, conn, ex, level, text, cat=None):
        lead = "#%d" % ex.seq if ex else "--"
        c = (ex.cat if ex else cat) or "other"
        self.emit(conn, level, "%s %-15s %s" % (lead, LBL[level], text), c, unk=bool(ex and ex.unknown))

    def issue(self, conn, ex, level, text, cat=None, tag_only=False):
        conn.verd[level] += 1
        key = (level, human(ex.name) if ex else "-", re.sub(r"\d+", "#", text)[:110])
        pr = conn.problems.setdefault(key, [0, text])
        pr[0] += 1
        self.result(conn, ex, level, text, cat)
        if ex is not None:
            ex.issues += 1

    def finalize(self, conn, ex, force=False):
        if ex.done:
            return
        ex.done = True
        if ex in conn.open:
            conn.open.remove(ex)
        if ex.session is not None and conn.pending.get(ex.session) is ex:
            conn.pending.pop(ex.session, None)
            conn.late[ex.session] = ex
            if len(conn.late) > 200:
                conn.late.pop(next(iter(conn.late)))
        msg = ex.msg
        known = msg in SERVER_MSGS
        resp_empty = ex.resp is not None and not ex.resp
        ptags = [p[0] for p in ex.pushes]
        lat = int((ex.resp_t - ex.t0) * 1000) if ex.resp_t else None

        if ex.session is not None and ex.resp is None and not force:
            self.issue(conn, ex, "FAIL", "NO ANSWER: the server never answered '%s' within %.0f s, so the client keeps "
                       "waiting. The handler is stuck, crashed, or forgot to reply." % (human(ex.name), self.a.timeout))
        if not known:
            conn.S.stat[msg]["unhandled"] += 1
            conn.S.unhandled.setdefault(msg, [0, render_fields(ex.body), ptags])
            conn.S.unhandled[msg][0] += 1
            self.issue(conn, ex, "MISS", "MISSING IN SERVER: 9555.py has no code for %s. "
                       "The client asked and got %s - %s. => This request still has to be implemented." % (
                           req_label(msg), "only the server's generic empty OK" if resp_empty else "no useful reply",
                           describe_pushes(ptags)))
        elif resp_empty and not ptags and ex.session is not None and msg not in NOISY_REQ:
            conn.S.stat[msg]["empty_ack"] += 1
            ex.custom_ok = True
            self.result(conn, ex, "INFO", "%s: the server answered with an empty OK and sent nothing else. Normal for simple "
                        "'ack' requests, but if the client expects data here, this handler is only a stub." % human(ex.name))
        if lat is not None and lat > self.a.slow_ms:
            self.issue(conn, ex, "WARN", "SLOW: the server needed %d ms to answer '%s' (limit %d ms)" % (lat, human(ex.name), self.a.slow_ms))
        self.check_claims(conn, ex, ptags)
        if msg == 113 and self.a.rewards_check:
            self.check_mission_reward(conn, ex)
        if self.baseline is not None:
            self.check_baseline(conn, ex)
        for t, n in ex.hid.items():
            self.emit(conn, "PUSH", "#%d %-15s +%d more '%s' message(s) of the same kind (push %d) not shown "
                      "(use --noisy to see all)" % (ex.seq, LBL["PUSH"], n, plain_push(t), t), ex.cat)
        self.record_ex(conn, ex)
        for t in ptags:
            conn.S.stat[("push", t)]["n"] += 1
        if ex.issues == 0 and not ex.custom_ok and not ex.noisy:
            if ex.session is None and not ptags and not ex.headerless:
                usual = sorted(t for t, h in PUSH_HINT.items() if ex.name in h)
                ex.custom_ok = True
                conn.verd["INFO"] += 1
                self.result(conn, ex, "INFO", "%s: sent as a notification and the server sent NOTHING back. Fine for pure "
                            "'report' messages (movement, ready ...). If the client waits for a result here, the handler "
                            "is incomplete.%s" % (human(ex.name), (" In the server code this request can trigger: %s." % ", ".join(
                                plain_push(t) for t in usual[:5])) if usual else ""))
            else:
                conn.verd["OK"] += 1
                if ex.session is None:
                    head = "notification answered by the server with messages"
                elif ex.resp is None:
                    head = "no direct reply"
                else:
                    head = "answered with %s%s" % ("an empty OK" if not ex.resp else "%d value(s)" % len(ex.resp),
                                                   " in %d ms" % lat if lat is not None else "")
                self.result(conn, ex, "OK", "%s: %s; %s" % (human(ex.name), head, describe_pushes(ptags)))

    def check_claims(self, conn, ex, ptags):
        if ex.msg not in CLAIM_MSGS:
            return
        paid = bool(set(ptags) & REWARD_PUSHES) or bool(ex.resp)
        if not paid:
            return
        key = (ex.msg, ex.raw_body)
        conn.claims[key] = conn.claims.get(key, 0) + 1
        if conn.claims[key] > 1:
            self.issue(conn, ex, "FAIL", "DOUBLE PAYOUT: '%s' with the same arguments was paid out %d times in this "
                       "session. A reward must only be given once." % (human(ex.name), conn.claims[key]))
        n_rew = sum(1 for t in ptags if t in REWARD_PUSHES)
        if n_rew > 1 and ex.issues == 0:
            self.issue(conn, ex, "FAIL", "TOO MANY REWARD POPUPS: the server sent %d reward popups for ONE claim (should be 1)" % n_rew)

    def check_mission_reward(self, conn, ex):
        mid = get_str(ex.body, 0)
        pushed_ok = any(p[0] == 521 for p in ex.pushes)
        if not pushed_ok:
            ex.custom_ok = True
            self.result(conn, ex, "INFO", "mission %s: the server did NOT confirm completion (mission not finished yet, or "
                        "already claimed). No reward expected." % mid)
            return
        pops = [p for p in ex.pushes if p[0] == 638]
        options = self.tables.expected_options(mid)
        if not options:
            self.result(conn, ex, "INFO", "mission %s: no reward row found in the client data tables (%s), so the reward "
                        "cannot be verified" % (mid, self.tables.source))
            return
        if not pops:
            self.issue(conn, ex, "FAIL", "NO REWARD: mission %s was completed but the server sent no reward popup. The client "
                       "table says the reward is: %s" % (mid, " OR ".join(
                           ", ".join("%d x %s" % (n, item_label(k)) for k, n in d.items()) or "nothing" for r, d in options)))
            return
        got = {}
        lst = parse_list(pops[0][1].get(0, b"")) if isinstance(pops[0][1].get(0), bytes) else None
        for it in lst or []:
            st = parse_struct(it, 0)
            if st:
                iid, cnt = get_str(st[0], 0), get_int(st[0], 1, 0)
                if cnt:
                    got[iid] = got.get(iid, 0) + cnt
        best, best_diff = None, None
        for rid, exp in options:
            exp_n = {k: v for k, v in exp.items() if v}
            diff = {k: (got.get(k, 0), exp_n.get(k, 0)) for k in set(got) | set(exp_n) if got.get(k, 0) != exp_n.get(k, 0)}
            if best_diff is None or len(diff) < len(best_diff):
                best, best_diff = rid, diff
        name = self.tables.mission_names.get(mid, "")
        if not best_diff:
            self.result(conn, ex, "OK" if ex.issues == 0 else "INFO",
                        "mission %s %s: the reward is EXACTLY what the client table says: %s" % (
                            mid, name, ", ".join("%d x %s" % (n, item_label(k)) for k, n in got.items()) or "nothing"))
            conn.verd["OK"] += 1
            ex.custom_ok = ex.issues == 0
        else:
            txt = "; ".join("%s: server gave %d, table says %d" % (item_label(k), g, e) for k, (g, e) in sorted(best_diff.items()))
            self.issue(conn, ex, "FAIL", "WRONG REWARD for mission %s: %s" % (mid, txt))

    def check_baseline(self, conn, ex):
        b = self.baseline.get("msgs", {}).get(str(ex.msg))
        if not b or not b.get("n"):
            self.result(conn, ex, "INFO", "'%s' is not in the baseline file, nothing to compare" % human(ex.name))
            return
        n = b["n"]
        exp_push = {int(t) for t, c in b.get("push", {}).items() if c / n >= self.a.baseline_ratio}
        got_push = {p[0] for p in ex.pushes}
        miss, extra = sorted(exp_push - got_push), sorted(got_push - exp_push)
        if miss:
            self.issue(conn, ex, "MISS", "BASELINE SAYS MISSING: after '%s' the client normally gets %s, but the server did "
                       "not send it this time" % (human(ex.name), ", ".join("'%s' (push %d)" % (plain_push(t), t) for t in miss)))
        if extra:
            self.issue(conn, ex, "WARN", "BASELINE SAYS EXTRA: the server also sent %s, which the baseline does not have" % (
                ", ".join("'%s' (push %d)" % (plain_push(t), t) for t in extra)))
        if ex.resp is not None:
            exp_tags = {int(t) for t, c in b.get("resp_tags", {}).items() if c / n >= self.a.baseline_ratio}
            got_tags = set(ex.resp)
            if exp_tags - got_tags:
                self.issue(conn, ex, "MISS", "BASELINE SAYS MISSING: the reply to '%s' lacks field number(s) %s that it normally "
                           "contains" % (human(ex.name), sorted(exp_tags - got_tags)))
            if got_tags - exp_tags and exp_tags:
                self.issue(conn, ex, "WARN", "BASELINE SAYS EXTRA: the reply to '%s' has extra field number(s) %s" % (
                    human(ex.name), sorted(got_tags - exp_tags)))
        for tag, body, _, _ in ex.pushes:
            pb = self.baseline.get("pushes", {}).get(str(tag))
            if pb and pb.get("n"):
                et = {int(t) for t, c in pb.get("fields", {}).items() if c / pb["n"] >= self.a.baseline_ratio}
                if et - set(body):
                    self.issue(conn, ex, "MISS", "BASELINE SAYS MISSING: message '%s' (push %d) lacks field number(s) %s" % (
                        plain_push(tag), tag, sorted(et - set(body))))
                extra_t = set(body) - et
                if extra_t and et:
                    self.issue(conn, ex, "WARN", "BASELINE SAYS EXTRA: message '%s' (push %d) has extra field number(s) %s" % (
                        plain_push(tag), tag, sorted(extra_t)))

    def record_ex(self, conn, ex):
        rec = conn.S.record
        m = rec["msgs"].setdefault(str(ex.msg), {"n": 0, "resp_tags": {}, "push": {}, "name": ex.name})
        m["n"] += 1
        for t in (ex.resp or {}):
            m["resp_tags"][str(t)] = m["resp_tags"].get(str(t), 0) + 1
        for t in {p[0] for p in ex.pushes}:
            m["push"][str(t)] = m["push"].get(str(t), 0) + 1
        for tag, body, _, _ in ex.pushes:
            p = rec["pushes"].setdefault(str(tag), {"n": 0, "fields": {}})
            p["n"] += 1
            for t in body:
                p["fields"][str(t)] = p["fields"].get(str(t), 0) + 1

    # ---------------- movement / speed -------------------------------------
    def track_move(self, conn, body, now):
        p = body.get(0)
        st = parse_struct(p, 0) if isinstance(p, bytes) else None
        if not st:
            return
        x, z = get_int(st[0], 0), get_int(st[0], 2)
        if x is None or z is None:
            return
        if conn.last_move:
            t0, x0, z0 = conn.last_move
            dt = now - t0
            if 0.03 <= dt <= 2.0:
                d = ((x - x0) ** 2 + (z - z0) ** 2) ** 0.5
                sp = d / dt
                conn.S.speeds.append(sp)
                if d > conn.S.max_jump[0]:
                    conn.S.max_jump = (d, (x0, z0, x, z, dt))
                if self.a.max_speed and sp > self.a.max_speed:
                    ex = conn.last_req
                    self.issue(conn, ex, "WARN", "TOO FAST: the client reported a move of %.0f units/s (limit --max-speed %.0f); jumped %.0f units in %.0f ms" % (
                        sp, self.a.max_speed, d, dt * 1000))
        conn.last_move = (now, x, z)

    # ---------------- background janitor -----------------------------------
    def what_watched(self):
        parts = []
        if self.a.account:
            parts.append("account %s" % self.a.account)
        if self.a.char:
            parts.append("character %d" % self.a.char)
        return " / ".join(parts) or "nobody"

    def janitor(self):
        t_wait = time.time()
        while not self.stop:
            time.sleep(0.2)
            now = time.time()
            with self.lock:
                if not self.a.all and (self.a.char or self.a.account) and now - t_wait >= 30:
                    active = any(c.target and not c.closed_flag for c in self.conns.values())
                    if not active:
                        ignored = sum(1 for c in self.conns.values() if c.muted)
                        self.out.raw("[sniffer] waiting for %s to log in ... (%d other player connection(s) seen and "
                                     "ignored so far)" % (self.what_watched(), ignored), "INFO")
                    t_wait = now
                for conn in list(self.conns.values()):
                    for ex in list(conn.open):
                        waiting = ex.session is not None and ex.resp is None
                        if waiting and now - ex.t0 > self.a.timeout:
                            self.finalize(conn, ex)
                        elif not waiting and now - ex.t_last > self.a.settle:
                            self.finalize(conn, ex)

    def total_verdicts(self):
        t = collections.Counter()
        for c in self.conns.values():
            if not c.muted:
                t.update(c.verd)
        return t

    # ---------------- final report ------------------------------------------
    def merged_problems(self):
        out = {}
        for c in self.conns.values():
            if c.muted:
                continue
            for k, (n, text) in c.problems.items():
                e = out.setdefault(k, [0, text])
                e[0] += n
        return out

    def merged_stats(self):
        M = ConnStats()
        for c in self.conns.values():
            if c.muted:
                continue
            S = c.S
            for k, cnt in S.stat.items():
                M.stat[k].update(cnt)
            for k, v in S.lat.items():
                M.lat[k].extend(v)
            for k, v in S.unhandled.items():
                e = M.unhandled.setdefault(k, [0, v[1], v[2]])
                e[0] += v[0]
            M.push_seen.update(S.push_seen)
            M.speeds.extend(S.speeds)
            if S.max_jump[0] > M.max_jump[0]:
                M.max_jump = S.max_jump
            M.hidden.update(S.hidden)
            for k, v in S.layouts.items():
                e = M.layouts.setdefault(k, [0, v[1]])
                e[0] += v[0]
            for k, v in S.unknown.items():
                e = M.unknown.setdefault(k, {"n": 0, "example": v["example"], "reply": v["reply"], "after": [], "hex": v["hex"]})
                e["n"] += v["n"]
                e["reply"] = e["reply"] or v["reply"]
                for a in v["after"]:
                    if a not in e["after"]:
                        e["after"].append(a)
            for mid, m in S.record["msgs"].items():
                e = M.record["msgs"].setdefault(mid, {"n": 0, "resp_tags": {}, "push": {}, "name": m["name"]})
                e["n"] += m["n"]
                for key in ("resp_tags", "push"):
                    for t, n in m[key].items():
                        e[key][t] = e[key].get(t, 0) + n
            for tg, pp in S.record["pushes"].items():
                e = M.record["pushes"].setdefault(tg, {"n": 0, "fields": {}})
                e["n"] += pp["n"]
                for t, n in pp["fields"].items():
                    e["fields"][t] = e["fields"].get(t, 0) + n
        return M

    def report(self):
        o = self.out
        with self.lock:
            for conn in self.conns.values():
                for ex in list(conn.open):
                    self.finalize(conn, ex, force=True)
            M = self.merged_stats()
            tv = self.total_verdicts()
            total = sum(s_["n"] for m, s_ in M.stat.items() if isinstance(m, int))
            o.raw("")
            o.raw("============================== SUMMARY ==============================", "REQ")
            o.raw("The client sent %d request(s).  OK: %d   MISSING: %d   PROBLEMS: %d   WARNINGS: %d" % (
                total, tv["OK"], tv["MISS"], tv["FAIL"], tv["WARN"]))
            if M.hidden:
                o.raw("Not shown (not about your character): %d update(s) about other players, %d about NPCs not caused by "
                      "your actions.  --show-others prints them." % (M.hidden["player"], M.hidden["npc"]))
            groups = self.merged_problems()
            for level, title in (("FAIL", "PROBLEMS - the server did something wrong (fix these first):"),
                                 ("MISS", "MISSING - the client needs this, the server does not provide it:"),
                                 ("WARN", "WARNINGS - suspicious, please check:")):
                rows = [(k, v) for k, v in groups.items() if k[0] == level]
                if rows:
                    o.raw("")
                    o.raw(title, level)
                    for (lv, who, _), (cnt, text) in sorted(rows, key=lambda kv: -kv[1][0]):
                        o.raw("  %s %s: %s" % ("(x%d)" % cnt if cnt > 1 else "    ", who, text), level)
            if M.unhandled:
                o.raw("")
                o.raw("TO-DO LIST - requests the client sent that 9555.py has no code for:", "MISS")
                for msg, (n, sample, ptags) in sorted(M.unhandled.items(), key=lambda kv: -kv[1][0]):
                    o.raw("  %s, sent %d time(s). Example data: %s" % (req_label(msg), n, sample), "MISS")
            if M.unknown:
                o.raw("")
                o.raw("UNKNOWN TO THE SNIFFER - I cannot name these (raw bytes saved in %s):" % (self.a.unknown_log or "(disabled)"), "WARN")
                for (kind, ident), u in sorted(M.unknown.items(), key=lambda kv: -kv[1]["n"]):
                    o.raw("  [%s] %s  x%d  data: %s%s%s" % (
                        kind, ident, u["n"], u["example"][:140],
                        "   reply: %s" % u["reply"][:100] if u["reply"] else "",
                        "   (context: %s)" % ", ".join(u["after"]) if u["after"] else ""), "WARN")
                if self.a.names_todo:
                    todo = {"req": {}, "push": {}}
                    for (kind, ident) in M.unknown:
                        if kind == "request":
                            todo["req"][str(ident)] = ""
                        elif kind == "push":
                            todo["push"][str(ident)] = ["", "other"]
                    json.dump(todo, open(self.a.names_todo, "w", encoding="utf-8"), indent=1)
                    o.raw("  -> fill in the names in %s and start the sniffer with --names %s" % (self.a.names_todo, self.a.names_todo), "WARN")
            if M.layouts:
                kinds = collections.Counter(k[0] for k in M.layouts)
                o.raw("")
                o.raw("FORMATS NOT LABELLED YET: %s. Examples with raw bytes are in %s - send me that file and I can name the fields." % (
                    ", ".join("%d %s(s)" % (n, k) for k, n in kinds.items()), self.a.unknown_log or "(log disabled)"), "INFO")
            empties = [(m, s_) for m, s_ in M.stat.items() if isinstance(m, int) and s_.get("empty_ack")]
            if empties:
                o.raw("")
                o.raw("STUBS? - these only answered an empty OK and sent nothing (check the client does not need data):", "INFO")
                for m, s_ in sorted(empties):
                    o.raw("  '%s' (msg %d), %d time(s)" % (human(req_name(m)), m, s_["empty_ack"]), "INFO")
            o.raw("")
            o.raw("ALL REQUESTS SEEN (how many times, average / slowest answer):")
            for m in sorted(k for k in M.stat if isinstance(k, int)):
                l = M.lat.get(m, [])
                o.raw("  %-34s x%-4d %s" % ("%s (msg %d)" % (human(req_name(m)), m), M.stat[m]["n"],
                                           "avg %d ms / max %d ms" % (sum(l) / len(l), max(l)) if l else "no direct reply"))
            if M.push_seen:
                o.raw("")
                o.raw("MESSAGES THE SERVER SENT TO YOU (how many of each):")
                for t, c in sorted(M.push_seen.items(), key=lambda kv: -kv[1]):
                    o.raw("  %-44s x%d" % ("%s (push %s)" % (plain_push(t), t), c))
            if M.speeds:
                sp = sorted(M.speeds)
                o.raw("")
                o.raw("MOVEMENT (positions reported by the client, raw units per second): average %.0f, 95%% under %.0f, "
                      "fastest %.0f, biggest single jump %.0f units" % (sum(sp) / len(sp), sp[max(0, int(len(sp) * .95) - 1)], sp[-1], M.max_jump[0]))
            if self.a.record:
                json.dump(M.record, open(self.a.record, "w", encoding="utf-8"), indent=1)
                o.raw("")
                o.raw("Baseline saved to %s. Later run with --baseline %s to compare." % (self.a.record, self.a.record), "OK")


# ----------------------------------------------------------------------------
# Proxy plumbing
# ----------------------------------------------------------------------------
def pump(sn, conn, src, dst, direction):
    buf = b""
    first = True
    try:
        while True:
            data = src.recv(65536)
            if not data:
                with sn.lock:
                    conn.eof_by = conn.eof_by or ("client" if direction == "C2S" else "server")
                break
            # Decode BEFORE forwarding: otherwise a very fast server reply could be
            # logged before the request that caused it.  Decode errors never block traffic.
            if not conn.is_http:
                buf += data
                if first:
                    first = False
                    if buf[:4] in (b"GET ", b"HEAD", b"POST"):
                        conn.is_http = True
                        buf = b""
                while len(buf) >= 2 and not conn.is_http:
                    size = struct.unpack(">H", buf[:2])[0]
                    if len(buf) < 2 + size:
                        break
                    frame, buf = buf[2:2 + size], buf[2 + size:]
                    try:
                        sn.on_frame(conn, direction, frame)
                    except Exception as exc:
                        tb = traceback.extract_tb(exc.__traceback__)[-1]
                        sn.out.raw("[sniffer] internal error in %s() line %d: %r  (game traffic is still forwarded "
                                   "normally; please report this line)" % (tb.name, tb.lineno, exc), "WARN")
            try:
                dst.sendall(data)
            except OSError:
                break
    except OSError:
        with sn.lock:
            conn.eof_by = conn.eof_by or ("client" if direction == "C2S" else "server")
    finally:
        for s in (src, dst):
            try:
                s.shutdown(socket.SHUT_RDWR)
            except OSError:
                pass
        sn.closed(conn, conn.eof_by or ("client" if direction == "C2S" else "server"))


def handle_client(sn, csock, peer, target):
    conn = sn.new_conn("%s:%s" % peer[:2])
    try:
        ssock = socket.create_connection(target, timeout=5)
        ssock.settimeout(None)
    except OSError as exc:
        sn.out.raw("[sniffer] cannot reach game server %s:%s - %s" % (target[0], target[1], exc), "FAIL")
        csock.close()
        return
    threading.Thread(target=pump, args=(sn, conn, csock, ssock, "C2S"), daemon=True).start()
    threading.Thread(target=pump, args=(sn, conn, ssock, csock, "S2C"), daemon=True).start()


def serve(sn, listen_port, target):
    srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    srv.bind(("0.0.0.0", listen_port))
    srv.listen(50)
    sn.out.raw("[sniffer] listening on :%d  ->  game server %s:%d" % (listen_port, target[0], target[1]), "OK")
    if sn.a.all:
        sn.out.raw("[sniffer] filter: *** OFF (--all) - logging ALL players. Remove --all to log only %s ***" % sn.what_watched(), "FAIL")
    else:
        sn.out.raw("[sniffer] filter: ON - only %s is logged%s, everybody else is ignored" % (
            sn.what_watched(), " (every character on the account)" if sn.a.account else ""), "OK")
    sn.out.raw("[sniffer] data tables: %s" % sn.tables.source, "INFO")
    sn.out.raw("[sniffer] Press Ctrl+C to print the summary (problems, missing requests, to-do list)", "INFO")
    sn.out.raw("")
    sn.out.raw("HOW TO READ THIS LOG", "REQ")
    sn.out.raw("  #12             every line with the same number belongs to the same click/request")
    sn.out.raw("  CLIENT ASKED    the game client sent a request (what you clicked)")
    sn.out.raw("  SERVER REPLIED  the server's direct answer to that request")
    sn.out.raw("  SERVER SENT     an extra message the server pushed (screen refresh, reward popup, other players...)")
    sn.out.raw("  --              a server message that was not caused by your click")
    sn.out.raw("  <-- UNKNOWN     the sniffer has no name for it; raw bytes go to %s (summary lists them)" % (sn.a.unknown_log or "(off)"))
    if sn.a.all:
        sn.out.raw("  WARNING: --all is ON, so EVERY player is logged. Start WITHOUT --all to see only %s." % sn.what_watched(), "FAIL")
    else:
        sn.out.raw("  Only %s is logged. All other players are ignored completely." % sn.what_watched())
    sn.out.raw("  RESULT OK       everything looked normal      RESULT INFO     worth knowing, not an error")
    sn.out.raw("  RESULT WARNING  suspicious                    RESULT MISSING  server lacks code the client needs")
    sn.out.raw("  RESULT PROBLEM  the server did something wrong (crash, no answer, double reward, wrong reward...)")
    sn.out.raw("  {name=...}      field names are shown only where verified in 9555.py; otherwise #0, #1 = field numbers")
    sn.out.raw("")
    while True:
        c, peer = srv.accept()
        threading.Thread(target=handle_client, args=(sn, c, peer, target), daemon=True).start()


# ----------------------------------------------------------------------------
# Self test: fake game server + fake clients through the real proxy path
# ----------------------------------------------------------------------------
def selftest(args):
    import tempfile
    items = lambda *its: [sp_encode([(0, i), (1, n), (2, 0)]) for i, n in its]

    def frame(fields_hdr, fields_body):
        pf = sp_pack(sp_encode(fields_hdr) + sp_encode(fields_body))
        return struct.pack(">H", len(pf)) + pf

    def recv_frame(s):
        h = s.recv(2)
        n = struct.unpack(">H", h)[0]
        d = b""
        while len(d) < n:
            d += s.recv(n - len(d))
        return d

    def fake_server(port):
        srv = socket.socket()
        srv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        srv.bind(("127.0.0.1", port))
        srv.listen(5)

        def serve_one(c):
            seen113 = False
            try:
                while True:
                    h = c.recv(2)
                    if not h:
                        break
                    n = struct.unpack(">H", h)[0]
                    d = b""
                    while len(d) < n:
                        d += c.recv(n - len(d))
                    raw = sp_unpack(d)
                    pkg, end = parse_struct(raw, 0)
                    body = parse_struct(raw, end)[0]
                    msg, ses = get_int(pkg, 0), get_int(pkg, 1)
                    reply = lambda f=(): c.sendall(frame([(1, ses)], list(f)))
                    push = lambda tag, f=(): c.sendall(frame([(0, tag)], list(f)))
                    if msg == 105:
                        reply([(0, 1)])
                    elif msg == 113:
                        mid = get_str(body, 0)
                        reply()
                        push(521, [(0, mid), (1, 0)])
                        if mid == "1":       # correct reward
                            push(638, [(0, items(("2001", 1000), ("1001", 2000)))])
                        elif mid == "2":     # wrong reward (cash too low, item missing)
                            push(638, [(0, items(("2001", 1000), ("1001", 5)))])
                        else:                # mission 3 reward paid twice
                            push(638, [(0, items(("2001", 1000), ("1001", 2000), ("10003", 1)))])
                            push(638, [(0, items(("2001", 1000), ("1001", 2000), ("10003", 1)))])
                    elif msg == 150:         # silently ignores -> no reply
                        pass
                    elif msg == 160:         # handler "crashes" -> connection closes
                        break
                    elif msg == 101:
                        reply([(0, body.get(0, b""))])
                    elif msg == 235:         # real data
                        reply([(0, 1), (1, "car_01")])
                        push(555, [(0, 3)])
                        # AOI update about ANOTHER player (id 48671533) while this request is open:
                        push(510, [(0, sp_encode([(0, 48671533), (1, 5)]))])
                        # ... and about the watched character itself (must stay attached to the request)
                        push(510, [(0, sp_encode([(0, 546880743), (1, 7)]))])
                        # NPC spawn + NPC update during an unrelated (inventory) request -> must be hidden
                        push(509, [(0, sp_encode([(0, 3000001), (1, 5)]))])
                        push(510, [(0, sp_encode([(0, 3000001), (1, 9)]))])
                        # a push tag nobody has named yet -> must be marked UNKNOWN and written to the unknown log
                        push(777, [(0, 42), (1, "mystery")])
                    elif msg == 999:         # catch-all like 9555.py
                        reply()
                    else:
                        reply()
            except OSError:
                pass
            finally:
                c.close()

        while True:
            c, _ = srv.accept()
            threading.Thread(target=serve_one, args=(c,), daemon=True).start()

    def free_port():
        s = socket.socket()
        s.bind(("127.0.0.1", 0))
        p = s.getsockname()[1]
        s.close()
        return p

    sp, pp = free_port(), free_port()
    threading.Thread(target=fake_server, args=(sp,), daemon=True).start()
    tmp = tempfile.mkdtemp()                     # tiny copy of the real reward rows for missions 1-3
    json.dump({"1": {"name": "m1", "reward_ids": ["41001"]}, "2": {"name": "m2", "reward_ids": ["41002"]},
               "3": {"name": "m3", "reward_ids": ["41003"]}}, open(os.path.join(tmp, "missions.json"), "w"))
    json.dump({"41001": {"exp": 1000, "cash": 2000, "items": [], "item_amounts": []},
               "41002": {"exp": 1000, "cash": 2000, "items": ["10002"], "item_amounts": [1]},
               "41003": {"exp": 1000, "cash": 2000, "items": ["10003"], "item_amounts": [1]}},
              open(os.path.join(tmp, "mission_rewards.json"), "w"))
    args.account, args.char = "", 546880743
    args.data, args.rewards_check = tmp, True
    args.unknown_log, args.names_todo = os.path.join(tmp, "unk.log"), ""
    args.timeout, args.settle, args.log = 1.0, 0.5, None
    sn = Sniffer(args)
    threading.Thread(target=serve, args=(sn, pp, ("127.0.0.1", sp)), daemon=True).start()
    time.sleep(0.4)

    def client(char, script):
        s = socket.create_connection(("127.0.0.1", pp))
        ses = 0
        for msg, body, wait in script:
            ses += 1
            s.sendall(frame([(0, msg), (1, ses)], body))
            s.settimeout(0.3)
            t_end = time.time() + wait
            while time.time() < t_end:
                try:
                    if not s.recv(65536):
                        return
                except socket.timeout:
                    pass
                except OSError:
                    return
        s.close()

    mv = lambda x, z: [(0, sp_encode([(0, x), (1, 0), (2, z), (3, 0)]))]
    other = threading.Thread(target=client, args=(1, [(105, [(0, 111222333)], .3), (113, [(0, "1")], .8)]))
    other.start()
    me = [(105, [(0, 546880743)], .3), (235, [], .8), (101, mv(1000, 1000), .1), (101, mv(1900, 1000), .1),
          (113, [(0, "1")], .9), (113, [(0, "1")], .9), (113, [(0, "2")], .9), (113, [(0, "3")], .9),
          (999, [(0, "x")], .8), (150, [(0, 5)], 1.6), (160, [(0, "bob")], .8)]
    client(546880743, me)
    other.join()
    time.sleep(1.0)
    sn.report()
    tv = sn.total_verdicts()
    M = sn.merged_stats()
    pushes_235 = M.record["msgs"].get("235", {}).get("push", {})
    attrib_ok = pushes_235.get("555") == 1 and pushes_235.get("510") == 1     # own 510 attached, Diana's NOT
    sn.out.raw("attribution check (other player's AOI push must not belong to the request): %s" %
               ("OK" if attrib_ok else "WRONG %s" % pushes_235), "OK" if attrib_ok else "FAIL")
    unk_txt = open(args.unknown_log).read() if os.path.exists(args.unknown_log) else ""
    unk_ok = ("UNKNOWN push 777" in unk_txt and "UNKNOWN request 999" in unk_txt and "raw packet hex" in unk_txt
              and "LAYOUT NOT LABELLED reply layout 235" in unk_txt)
    hid_ok = M.hidden["player"] >= 1 and M.hidden["npc"] >= 2 and 509 not in pushes_235 and "509" not in pushes_235
    sn.out.raw("unknown log written with raw bytes: %s | other players/NPC updates hidden (%s): %s" % (
        "OK" if unk_ok else "WRONG", dict(M.hidden), "OK" if hid_ok else "WRONG"), "OK" if (unk_ok and hid_ok) else "FAIL")
    ok = (tv["FAIL"] == 5 and tv["MISS"] == 1 and tv["OK"] >= 2 and attrib_ok and unk_ok and hid_ok)
    sn.out.raw("SELFTEST %s (FAIL=%d MISS=%d OK=%d)" % ("PASSED" if ok else "FAILED", tv["FAIL"], tv["MISS"], tv["OK"]),
               "OK" if ok else "FAIL")
    return 0 if ok else 1


# ----------------------------------------------------------------------------
def main():
    ap = argparse.ArgumentParser(description="ATG live protocol truth-log (transparent proxy in front of 9555.py)")
    ap.add_argument("--listen", type=int, default=15678, help="port the GAME CLIENT connects to (default 15678)")
    ap.add_argument("--server", default="127.0.0.1:15679", help="real 9555.py host:port (start it with PORT=15679)")
    ap.add_argument("--account", default="68086000311496", help="log every character of this account id (default 68086000311496; '' = off)")
    ap.add_argument("--char", type=int, default=0, help="additionally log this single character id (0 = off)")
    ap.add_argument("--all", action="store_true", help="log every connection, ignore --char")
    ap.add_argument("--only", default="", help="only show these categories, e.g. team,gang,mission,combat,map,shop,items,reward,social,rank,dance,aoi,npc")
    ap.add_argument("--log", default="", help="also append the log to this file")
    ap.add_argument("--data", default="", help="folder with the client TextAsset tables (auto-detected if omitted)")
    ap.add_argument("--server-src", default="", help="path to 9555.py (read-only) to refresh the handler list")
    ap.add_argument("--no-rewards-check", dest="rewards_check", action="store_false", help="skip mission reward verification")
    ap.add_argument("--record", default="", help="at exit write a baseline of what was seen (run against a KNOWN-GOOD server)")
    ap.add_argument("--baseline", default="", help="compare every exchange with a recorded baseline")
    ap.add_argument("--baseline-ratio", type=float, default=0.6, help="fraction of baseline runs a push/tag must appear in")
    ap.add_argument("--names", default="", help="json with extra names {'req':{'109':'x'},'push':{'603':['name','cat']}}")
    ap.add_argument("--timeout", type=float, default=5.0, help="seconds before an unanswered request is a FAIL")
    ap.add_argument("--settle", type=float, default=1.0, help="seconds of quiet after which an exchange is judged")
    ap.add_argument("--slow-ms", type=int, default=1500)
    ap.add_argument("--attr-window", type=float, default=3.0, help="a push is only credited to a request younger than this (s)")
    ap.add_argument("--dup-window", type=float, default=0.3, help="identical pushes/requests within this many seconds are duplicates")
    ap.add_argument("--max-speed", type=float, default=0.0, help="flag client move packets faster than this (raw units/s); 0=off")
    ap.add_argument("--show-others", action="store_true", help="also print world updates about other players / NPCs (hidden by default)")
    ap.add_argument("--only-unknown", action="store_true", help="print only what the sniffer cannot name (plus problems)")
    ap.add_argument("--max-same", type=int, default=3, help="print at most this many messages of the same kind per request (rest is counted)")
    ap.add_argument("--unknown-log", default="atg_unknown.log", help="file for unknown requests/messages with raw bytes ('' = off)")
    ap.add_argument("--names-todo", default="atg_names_todo.json", help="template of unknown ids to fill in for --names ('' = off)")
    ap.add_argument("--noisy", action="store_true", help="print every move / AOI-move packet (no throttling)")
    ap.add_argument("--noise-interval", type=float, default=2.0)
    ap.add_argument("--maxlen", type=int, default=260, help="truncate console lines (0 = never); --log always keeps full lines")
    ap.add_argument("--no-color", action="store_true")
    ap.add_argument("--selftest", action="store_true", help="run a built-in fake server/client check and exit")
    args = ap.parse_args()
    if os.name == "nt":
        os.system("")
    if args.names:
        extra = json.load(open(args.names, encoding="utf-8"))
        for k, v in extra.get("req", {}).items():
            REQ_NAMES[int(k)] = v
        for k, v in extra.get("push", {}).items():
            PUSH_INFO[int(k)] = tuple(v) if isinstance(v, list) else (v, "other")
        SERVER_MSGS.update(REQ_NAMES)
    if args.server_src:
        refresh_from_server_source(args.server_src)
    else:
        for guess in ("9555.py", os.path.join(os.path.dirname(os.path.abspath(__file__)), "9555.py")):
            if os.path.exists(guess):
                refresh_from_server_source(guess)
                break
    if args.selftest:
        sys.exit(selftest(args))
    host, _, port = args.server.rpartition(":")
    target = (host or "127.0.0.1", int(port))
    sn = Sniffer(args)
    import signal

    def _stop(signum, frame):
        raise KeyboardInterrupt
    signal.signal(signal.SIGINT, _stop)          # also works when started from a shell that ignores SIGINT
    try:
        signal.signal(signal.SIGTERM, _stop)
    except (AttributeError, ValueError):
        pass
    try:
        serve(sn, args.listen, target)
    except KeyboardInterrupt:
        pass
    finally:
        sn.stop = True
        sn.report()


if __name__ == "__main__":
    main()
