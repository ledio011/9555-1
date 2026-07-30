import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters_final.json"
server_session_counter = 8000

# Load Mission Data
missions_data = {}
rewards_data = {}
try:
    md_path = os.path.join(os.path.dirname(__file__), "missions.json")
    rd_path = os.path.join(os.path.dirname(__file__), "mission_rewards.json")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding='utf-8') as f: missions_data = json.load(f)
    if os.path.exists(rd_path):
        with open(rd_path, "r", encoding='utf-8') as f: rewards_data = json.load(f)
except: traceback.print_exc()

def load_chars():
    if os.path.exists(CHAR_DB):
        try:
            with open(CHAR_DB, "r") as f: return json.load(f)
        except: return {}
    return {}

def save_chars(data):
    try:
        with open(CHAR_DB, "w") as f: json.dump(data, f, indent=4)
    except: pass

all_accounts_chars = load_chars()

def generate_unique_char_id():
    return int(time.time() * 1000) % 1000000000

def get_area_id(serverId):
    try:
        sid = int(serverId)
        if sid == 1 or (300 <= sid < 400): return 1 # Europe
        if sid == 2 or (600 <= sid < 700): return 2 # Asia
        if sid == 3 or (10 <= sid < 100): return 0 # America
    except: pass
    return 0

def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, int): return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4: return struct.unpack("<i", val)[0]
        if len(val) == 8: return struct.unpack("<q", val)[0]
    return default

def encode_sproto(fields, fn=None):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    header = []; body = bytearray(); last_tag = -1
    for tag, val in fields:
        skip = tag - last_tag - 1
        if skip > 0: header.append(2 * (skip - 1) + 1)

        if val is None:
            header.append(1)
        elif isinstance(val, bool):
            header.append((1 if val else 0) * 2 + 2)
        elif isinstance(val, int):
            if 0 <= val <= 32766:
                header.append((val + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= val <= 2147483647:
                    body += struct.pack("<I", 4) + struct.pack("<i", val)
                else:
                    body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray, list, dict)):
            header.append(0)
            if isinstance(val, str):
                v = val.encode('utf-8')
            elif isinstance(val, list):
                if val and isinstance(val[0], int):
                    v = b"\x04" + b"".join([struct.pack("<i", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)): pass
                        else: item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                # Sproto maps are encoded as arrays of objects (structs)
                items = []
                for item in val.values():
                    if isinstance(item, str): item = item.encode('utf-8')
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else: # Fallback for primitive types if ever passed in dict values
                        items.append(struct.pack("<I", 1) + (b'\x01' if item else b'\x00'))
                v = b"".join(items)
            else:
                v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag

    fn_val = fn if fn is not None else len(header)
    res = struct.pack("<H", fn_val)
    for h in header: res += struct.pack("<H", h)
    return res + body

def sproto_pack(data):
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i+8]
        if len(chunk) < 8: chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0: mask |= (1 << j)
        if mask == 0xFF:
            out.append(0xFF); out.append(0); out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j): out.append(chunk[j])
    return bytes(out)

def sproto_unpack(data):
    out = bytearray(); i = 0; n = len(data)
    while i < n:
        mask = data[i]; i += 1
        if mask == 0xFF:
            if i >= n: break
            count = (data[i] + 1) * 8; i += 1
            out.extend(data[i:i+count]); i += count
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < n: out.append(data[i]); i += 1
                else: out.append(0)
    return bytes(out)

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    fn = struct.unpack("<H", data[offset:offset+2])[0]
    h_ptr, b_ptr = offset + 2, offset + 2 + fn*2
    fields, curr_tag = {}, -1
    for i in range(fn):
        v = struct.unpack("<H", data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
        if v == 0:
            curr_tag += 1
            if b_ptr + 4 <= len(data):
                l = struct.unpack("<I", data[b_ptr:b_ptr+4])[0]
                fields[curr_tag] = data[b_ptr+4:b_ptr+4+l]
                b_ptr += 4 + l
        elif v == 1:
            curr_tag += 1
        elif v & 1:
            curr_tag += (v >> 1) + 1
        else:
            curr_tag += 1
            fields[curr_tag] = (v >> 1) - 1
    return fields

def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

# Skill System Constants
PROF_SKILLS = {
    0: {"atk": "101", "dodge": "104", "actives": ["105", "106", "107", "108", "109", "110"]},
    1: {"atk": "201", "dodge": "204", "actives": ["205", "206", "207", "208", "209", "210"]},
    2: {"atk": "301", "dodge": "304", "actives": ["305", "306", "307", "308", "309", "310"]}
}
SKILL_UNLOCK_LVS = [1, 5, 10, 15, 20, 25]

def get_skill_upgrade_cost(lv):
    # Current level lv, upgrading to lv+1
    if lv < 0: return 0
    if lv < 15: return (lv + 1) * 10000
    if lv < 24: return (lv - 13) * 100000 + 100000 # 200k, 300k...
    if lv == 24: return 3000000
    if lv == 25: return 7000000
    if lv == 26: return 18000000
    return 20000000 # Fallback

def build_skills_map(prof, char_level, skill_levels=None):
    if skill_levels is None: skill_levels = {}
    p = PROF_SKILLS.get(prof, PROF_SKILLS[0])
    smap = {}
    
    # Attack and Dodge (always unlocked)
    # Tag 0: skillId, 1: skillLevel, 2: indexPos, 3: unlockLevel, 4: indexPos2, 5: disable
    smap[p["atk"]] = encode_sproto([(0, p["atk"]), (1, skill_levels.get(p["atk"], 0)), (2, 0), (3, 1), (4, 0), (5, False)])
    smap[p["dodge"]] = encode_sproto([(0, p["dodge"]), (1, skill_levels.get(p["dodge"], 0)), (2, 3), (3, 1), (4, 1), (5, False)])
    
    # Actives
    for i in range(len(p["actives"])):
        sid = p["actives"][i]
        unlock_lv = SKILL_UNLOCK_LVS[i]
        disabled = char_level < unlock_lv
        # indexPos: 4, 5, 6, 7, 8, 9
        # indexPos2: 2, 3, 4, 5, 6, 7
        smap[sid] = encode_sproto([
            (0, sid),
            (1, 0 if disabled else skill_levels.get(sid, 0)),
            (2, 4 + i),
            (3, unlock_lv),
            (4, 2 + i),
            (5, disabled)
        ])
    return smap

def get_general(c):
    # Tag 4: tutorial (1=finished). Unblocks HUD and NPCManagers immediately.
    return encode_sproto([
        (0, c.get('name', 'Hero')),
        (1, c.get('prof', 0)),
        (2, 1), # lineIndex
        (3, "11"), # mapInfoId
        (4, 1) # tutorial state
    ])

def get_movement(x, y, z, o=0):
    # Matches SprotoType.position (Tags 0-3: x, y, z, o)
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c):
    gen = get_general(c)
    attr = encode_sproto([(0, c.get('level', 1)), (1, 5000)]) # level, combValue
    return encode_sproto([
        (0, c['id']),
        (1, gen),
        (2, attr),
        (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))),
        (4, int(time.time())),
        (5, 0) # forbidden
    ])

def get_full_char(c):
    gen = get_general(c)
    # attribute_other: hp(0), exp(1), level(2), combValue(3), camp(15)
    attr_oth = encode_sproto([(0, 3000), (1, c.get('exp', 0)), (2, c.get('level', 1)), (3, 5000), (15, 1)])
    # property: Tag 13-15 are money fields. Cash: 1000, Gold: 100, Diamond: 10
    prop = encode_sproto([(13, c.get('cash', 1000)), (14, 100), (15, 10), (16, 0), (17, 0), (18, 0)])

    # Position Persistence: Default to Mission 1001 area for new chars
    # Raw int coords in cm.
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])
    # runtime_agent: max_hp(0), atk(2), def(3).
    attr_run = encode_sproto([(0, 3000), (2, 300), (3, 35)])
    # attribute_all: mov(13)=500 (Speed 5.0)
    attr_all = encode_sproto([(0, 3000), (2, 300), (3, 35), (13, 500)])
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    prof = c.get('prof', 0)
    char_level = c.get('level', 1)
    skill_levels = c.get('skill_levels', {})
    skills_map = build_skills_map(prof, char_level, skill_levels)

    # Tag 9: equip (map long->gameitem). Key 5 = WEAPON slot.
    wid = "10001" if prof == 0 else "20001" if prof == 1 else "30001"
    w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])
    equip_map = {5: w1}

    return encode_sproto([
        (0, c['id']),
        (1, gen),
        (2, attr_oth),
        (5, prop),
        (6, get_visual(c.get('name', 'Hero'), prof)),
        (7, mv),
        (8, skills_map),
        (9, equip_map),
        (12, 0), # potionIndex
        (13, run),
        (15, 2)  # download finish
    ])

def sync_mission(picked_char):
    own_missions = {}
    for mid, mdata in picked_char.get('active_missions', {}).items():
        own_missions[mid] = encode_sproto([
            (0, mid),
            (1, mdata['state']),
            (3, mdata['parm'])
        ])
    return encode_sproto([
        (0, own_missions),
        (1, picked_char.get('last_main_mission_id', "")),
        (2, picked_char.get('completed_side_missions', []))
    ])

def accept_mission_logic(picked_char, mid):
    if mid not in missions_data: return False
    m = missions_data[mid]
    if picked_char.get('level', 1) < m.get('min_level', 0): return False
    pre_id = m.get('pre_id', "")
    if pre_id:
        if m['class'] == 1:
            if picked_char.get('last_main_mission_id', "") != pre_id: return False
        else:
            if pre_id not in picked_char.get('completed_side_missions', []): return False
    if 'active_missions' not in picked_char: picked_char['active_missions'] = {}
    if mid in picked_char['active_missions']: return False
    parm = [0]*8; parm[7] = int(time.time())
    picked_char['active_missions'][mid] = {'state': 1, 'parm': parm, 'accept_time': int(time.time())}
    return True

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0
    global server_session_counter

    def send_rpc_push(tag, data):
        try:
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            print(f"[TX] PUSH TAG={tag} SIZE={len(data)}")
        except Exception: pass

    try:
        while True:
            h_bytes = conn.recv(2)
            if not h_bytes: break
            size = struct.unpack(">H", h_bytes)[0]
            data = b""
            while len(data) < size:
                chunk = conn.recv(size - len(data))
                if not chunk: break
                data += chunk
            if len(data) < size: break

            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            print(f"[RX] MSG={msg} SESSION={session}")
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)
            print("BODY =", body)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1); cur_areaId = get_area_id(sid)
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (3, 1)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                chars = all_accounts_chars.get(cur_areaId, {}).get(acc_id, [])
                resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8') if isinstance(c_data.get(0), bytes) else str(c_data.get(0, "Hero"))
                prof = get_val_int(c_data, 1, 0); cid = generate_unique_char_id()
                if cur_areaId not in all_accounts_chars: all_accounts_chars[cur_areaId] = {}
                if acc_id not in all_accounts_chars[cur_areaId]: all_accounts_chars[cur_areaId][acc_id] = []
                nc = {'id': cid, 'name': name, 'prof': prof, 'level': 1, 'exp': 0, 'cash': 1000, 'active_missions': {}, 'completed_side_missions': [], 'last_main_mission_id': ""}
                all_accounts_chars[cur_areaId][acc_id].append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(cur_areaId, {}).get(acc_id, []) if c['id'] == char_id), None)
                resp = encode_sproto([(0, 1 if picked_char else 0)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if picked_char:
                    # Init mission fields if missing
                    if 'active_missions' not in picked_char: picked_char['active_missions'] = {}
                    if 'completed_side_missions' not in picked_char: picked_char['completed_side_missions'] = []
                    if 'last_main_mission_id' not in picked_char: picked_char['last_main_mission_id'] = ""
                    if 'level' not in picked_char: picked_char['level'] = 1
                    if 'exp' not in picked_char: picked_char['exp'] = 0
                    if 'cash' not in picked_char: picked_char['cash'] = 1000

                    # Auto-accept next main mission if none active
                    has_active_main = any(missions_data.get(mid, {}).get('class') == 1 for mid in picked_char['active_missions'])
                    if not has_active_main:
                        last_mid = picked_char['last_main_mission_id']
                        if not last_mid:
                            first_main = next((mid for mid, m in missions_data.items() if m['class'] == 1 and not m.get('pre_id')), None)
                            if first_main: accept_mission_logic(picked_char, first_main)
                        else:
                            next_mid = missions_data.get(last_mid, {}).get('next_id')
                            if next_mid: accept_mission_logic(picked_char, next_mid)
                    save_chars(all_accounts_chars)

                    # Sync common data
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))

                    # Push Missions and Full Char
                    send_rpc_push(519, sync_mission(picked_char))
                    send_rpc_push(503, encode_sproto([(0, "11"), (1, 1), (2, 1)]))
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(29860, 100, -17005))]))

            elif msg == 100: # map_ready
                if picked_char:
                    send_rpc_push(611, encode_sproto([(0, [])]))
                    send_rpc_push(540, encode_sproto([(0, []), (1, False)]))
                    send_rpc_push(654, encode_sproto([(0, 1)]))

            elif msg == 101: # move
                if session is not None:
                    p_raw = body.get(0)
                    if p_raw and picked_char:
                        pd = decode_sproto(p_raw)
                        picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                        save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # update_game_server
                servers = [encode_sproto([(0, 302), (1, "EU-001"), (2, "tokaido.proxy.rlwy.net"), (3, 48282), (4, 1), (5, 1), (6, 1), (7, 0), (8, 1), (9, 1)])]
                resp = encode_sproto([(0, servers)])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 113: # complete_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    if mid in picked_char.get('active_missions', {}) and picked_char['active_missions'][mid]['state'] == 2:
                        m = missions_data.get(mid)
                        if m:
                            prof = picked_char.get('prof', 0)
                            rids = m.get('reward_ids', [])
                            if rids and prof < len(rids):
                                rid = rids[prof]
                                reward = rewards_data.get(rid)
                                if reward:
                                    picked_char['exp'] = picked_char.get('exp', 0) + reward.get('exp', 0)
                                    picked_char['cash'] = picked_char.get('cash', 1000) + reward.get('cash', 0)
                                    while picked_char['exp'] >= picked_char['level'] * 100000:
                                        picked_char['exp'] -= picked_char['level'] * 100000
                                        picked_char['level'] += 1
                            if m['class'] == 1: picked_char['last_main_mission_id'] = mid
                            else:
                                if mid not in picked_char['completed_side_missions']: picked_char['completed_side_missions'].append(mid)
                            del picked_char['active_missions'][mid]
                            if m.get('is_multi') == 1 or m['class'] == 8:
                                next_id = m.get('next_id')
                                if next_id: accept_mission_logic(picked_char, next_id)
                            save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission(picked_char))

            elif msg == 112: # accept_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    accept_mission_logic(picked_char, mid)
                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission(picked_char))

            elif msg == 524: # set_mission_param
                mid = body.get(0, b"").decode('utf-8')
                idx = get_val_int(body, 1); val = get_val_int(body, 2)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['parm'][idx-1] = val
                    save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 523: # set_mission_state
                mid = body.get(0, b"").decode('utf-8')
                state = get_val_int(body, 1)
                if picked_char and mid in picked_char.get('active_missions', {}):
                    picked_char['active_missions'][mid]['state'] = state
                    save_chars(all_accounts_chars)
                    if session is not None:
                        ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                        conn.sendall(struct.pack(">H", len(pf)) + pf)
                    send_rpc_push(519, sync_mission(picked_char))

            elif msg == 130: # skill_level_up
                sid = body.get(0, b"").decode('utf-8')
                cur_lv = get_val_int(body, 1)
                is_all = get_val_int(body, 2)
                if picked_char:
                    cost = get_skill_upgrade_cost(cur_lv)
                    if picked_char.get('cash', 0) >= cost and picked_char.get('level', 1) > cur_lv + 1:
                        picked_char['cash'] -= cost
                        if 'skill_levels' not in picked_char: picked_char['skill_levels'] = {}
                        new_lv = cur_lv + 1
                        picked_char['skill_levels'][sid] = new_lv
                        save_chars(all_accounts_chars)
                        # sync_skill_info (Push 540)
                        smap = build_skills_map(picked_char['prof'], picked_char['level'], picked_char['skill_levels'])
                        send_rpc_push(540, encode_sproto([(0, smap), (1, True)]))
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 102: # skill_use
                sid = body.get(1, b"").decode('utf-8')
                tid = get_val_int(body, 0)
                alist = body.get(3, [])
                if picked_char:
                    # ret_skill_use (Push 508): senderId(0), targetId(1), skillId(2), attack_list(3)
                    send_rpc_push(508, encode_sproto([(0, picked_char['id']), (1, tid), (2, sid), (3, alist)]))
                    # accept_damge (Push 111) if needed for HP sync
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 310, 313, 319]:
                # Generic Responder for Scene Info and UI Requests
                resp_data = encode_sproto([])
                if msg == 118: resp_data = encode_sproto([(0, f"User_{random.randint(100,999)}")])
                elif msg == 218: resp_data = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])

                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp_data)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

                # Side effect pushes for scene completion
                if msg == 310: send_rpc_push(684, encode_sproto([]))
                elif msg == 145: send_rpc_push(555, encode_sproto([(0, [])]))

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (STABLE v15)");
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
