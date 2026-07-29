# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - STABLE v16 DATA-DRIVEN
# GAME SERVER 9555
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters_final.json"
server_session_counter = 8000

# Data Source Truth: assets\Bundle\Data\Data.bundle extraction
def find_data_file(name):
    paths = [
        os.path.join("assets", "Bundle", "TextAsset", name),
        os.path.join("assets", "Bundle", "Data", name),
        os.path.join("assets", "bin", "Data", "Resources", "Data", name),
        os.path.join("assets", "bin", "Data", "TextAsset", name)
    ]
    for p in paths:
        if os.path.exists(p): return p
    return None

mission_logic_db = {}
kill_target_db = {}
car_target_db = {}
mission_require_db = {}

def load_game_data():
    mf = find_data_file("MissionData")
    if mf:
        with open(mf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                parts = line.split(",")
                if len(parts) > 14:
                    mid = parts[1].strip()
                    mclass = int(parts[6]) if parts[6].isdigit() else 0
                    if mclass == 1:
                        mission_logic_db[mid] = {
                            "logicType": int(parts[7]) if parts[7].isdigit() else 0,
                            "logicId": parts[9].strip(),
                            "target": parts[11].strip(),
                            "nextId": parts[14].strip()
                        }
    kf = find_data_file("KillTargetMissionData")
    if kf:
        with open(kf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 8:
                    lid = p[1].strip()
                    kill_target_db[lid] = {
                        "npcId": p[6].strip(), 
                        "x": int(p[3]), 
                        "z": int(p[4]), 
                        "require": int(p[8]) if p[8].isdigit() else 1
                    }
    cf = find_data_file("TargetCarMissionData")
    if cf:
        with open(cf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 6:
                    car_target_db[p[1].strip()] = {
                        "carId": p[6].strip(), 
                        "x": int(p[3]), 
                        "z": int(p[4])
                    }
    rf = find_data_file("MissionRequireData")
    if rf:
        with open(rf, "r", encoding="utf-8") as f:
            for line in f:
                if not line.startswith("*"): continue
                p = line.split(",")
                if len(p) > 5:
                    lid = p[1].strip()
                    mission_require_db[lid] = {
                        "npcId": p[4].strip(), 
                        "require": int(p[5]) if p[5].isdigit() else 1
                    }

load_game_data()

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
online_clients = {} 
npc_hps = {}

def get_default_skills(prof):
    # Original starter skills: Basic combo (3 stages) and Dodge
    m = {
        0: ["101", "102", "103", "104"], # Melee
        1: ["201", "202", "203", "204"], # Boxer
        2: ["301", "302", "303", "304"]  # Gunner
    }
    ids = m.get(prof, m[0])
    slots = {ids[0]:0, ids[1]:1, ids[2]:2, ids[3]:3}
    res = {}
    for sid in ids:
        res[sid] = {"id": sid, "lv": 1, "pos": slots[sid], "unlock": 1, "pos2": slots[sid], "dis": False}
    return res

def init_mission_state(mid):
    logic = mission_logic_db.get(mid)
    if not logic: return {"alive_sids": [], "progress": 0}
    lid = logic['logicId']
    ltype = logic['logicType']
    
    alive_sids = []
    if ltype in [23, 1]:
        target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
        count = target.get('require', 1) if target else 1
        base_sid = 900000 + int(lid) if ltype == 23 else 700000 + int(lid)
        for i in range(count):
            alive_sids.append(base_sid * 10 + i)
            
    return {"alive_sids": alive_sids, "progress": 0}

def generate_unique_char_id():
    return int(time.time() * 1000) % 1000000000

def get_area_id(serverId):
    try:
        sid = int(serverId)
        if sid == 1 or (300 <= sid < 400): return 1
        if sid == 2 or (600 <= sid < 700): return 2
        if sid == 3 or (10 <= sid < 100): return 0
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
                items = []
                for item in val.values():
                    if isinstance(item, str): item = item.encode('utf-8')
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else:
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

def get_general(c):
    return encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, "11"), (4, 1)])

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c):
    gen = get_general(c)
    attr = encode_sproto([(0, 1), (1, 5000)])
    return encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))), (4, int(time.time())), (5, 0)])

def get_full_char(c):
    gen = get_general(c)
    attr_oth = encode_sproto([(0, 3000), (1, 0), (2, 1), (3, 5000), (15, 1)])
    prop_data = c.get('property', [0]*20)
    prop = encode_sproto([(13, prop_data[13]), (14, prop_data[14]), (15, prop_data[15]), (16, 0), (17, 0), (18, 0)])
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])
    attr_run = encode_sproto([(0, 3000), (2, 300), (3, 35)])
    attr_all = encode_sproto([(0, 3000), (2, 300), (3, 35), (13, 500)])
    run = encode_sproto([(6, attr_run), (7, attr_all)])
    skills_data = c.get('skills', get_default_skills(c.get('prof', 0)))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])
    prof = c.get('prof', 0)
    wid = "10001" if prof == 0 else "20001" if prof == 1 else "30001"
    w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])
    equip_map = {5: w1}
    return encode_sproto([(0, c['id']), (1, gen), (2, attr_oth), (5, prop), (6, get_visual(c.get('name', 'Hero'), prof)), (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)])

def get_skill_sync(c):
    skills_data = c.get('skills', get_default_skills(c.get('prof', 0)))
    skills_map = {}
    for sid, sd in skills_data.items():
        skills_map[sid] = encode_sproto([(0, sd['id']), (1, sd['lv']), (2, sd['pos']), (3, sd['unlock']), (4, sd['pos2']), (5, sd['dis'])])
    return encode_sproto([(0, skills_map), (1, False)])

def get_mission_sync(c):
    active = c.get('active_missions', {"1001": [1, 0, [0]*8]})
    missions_map = {}
    for mid, mdata in active.items():
        state, qual, parms = mdata
        missions_map[mid] = encode_sproto([(0, mid), (1, state), (2, qual), (3, parms)])
    last_id = c.get('last_main_mission', "")
    done_side = c.get('completed_side_missions', [])
    return encode_sproto([(0, missions_map), (1, last_id), (2, done_side)])

def get_aoi_npc(npc_id, server_id, x, z, name="NPC"):
    vis = encode_sproto([(0, name), (1, "NPC_Nan_013"), (10, 0)])
    gen = encode_sproto([(0, name), (1, 0), (2, 1), (3, "11"), (4, 1)])
    stats = encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])
    pos_data = encode_sproto([(0, x), (1, 0), (2, z), (3, 0)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])
    attr = encode_sproto([(6, stats), (7, stats)])
    return encode_sproto([(0, server_id), (1, vis), (2, gen), (3, stats), (5, mv), (6, attr)])

def get_aoi_car(car_id, server_id, x, z, name="Car"):
    vis = encode_sproto([(0, name), (1, "DJ_Car_01"), (10, 0)])
    gen = encode_sproto([(0, name), (1, 0), (2, 1), (3, "11"), (4, 1)])
    stats = encode_sproto([(0, 1000), (1, 0), (2, 1), (15, 2)])
    pos_data = encode_sproto([(0, x), (1, 0), (2, z), (3, 0)])
    mv = encode_sproto([(0, pos_data), (1, pos_data)])
    attr = encode_sproto([(6, stats), (7, stats)])
    return encode_sproto([(0, server_id), (1, vis), (2, gen), (3, stats), (5, mv), (6, attr)])

def sync_mission_world_objects(char, send_push_func):
    active = char.get('active_missions', {})
    mstate = char.get('mission_state', {})
    for mid, mdata in active.items():
        logic = mission_logic_db.get(mid)
        if not logic: continue
        lid = logic['logicId']
        ltype = logic['logicType']
        
        state = mstate.get(mid, {})
        alive_sids = state.get('alive_sids', [])
        
        if ltype in [23, 1]:
            target = kill_target_db.get(lid) if ltype == 23 else mission_require_db.get(lid)
            if target:
                npc_id = target['npcId']
                for sid in alive_sids:
                    npc_hps[sid] = 1000
                    x = target.get('x', 29860) + random.randint(-100, 100)
                    z = target.get('z', -17005) + random.randint(-100, 100)
                    send_push_func(505, get_aoi_npc(npc_id, sid, x, z, "Target"))
        elif ltype == 24 and state.get('progress', 0) == 0:
            target = car_target_db.get(lid)
            if target:
                sid = 800000 + int(lid)
                send_push_func(505, get_aoi_car(target['carId'], sid, target['x'], target['z'], "Car"))

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0
    global server_session_counter, online_clients, npc_hps

    def send_rpc_push(tag, data, target_conn=None):
        try:
            target = target_conn if target_conn else conn
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            target.sendall(struct.pack(">H", len(pf_p)) + pf_p)
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
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)

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
                nc = {'id': cid, 'name': name, 'prof': prof, 'hp': 3000, 
                      'skills': get_default_skills(prof), 
                      'active_missions': {"1001": [1, 0, [0]*8]}, 
                      'mission_state': {"1001": init_mission_state("1001")},
                      'last_main_mission': ""}
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
                    # Backward compatibility and state recovery
                    if 'skills' not in picked_char or len(picked_char['skills']) > 4: 
                        picked_char['skills'] = get_default_skills(picked_char.get('prof', 0))
                    if 'active_missions' not in picked_char: 
                        picked_char['active_missions'] = {"1001": [1, 0, [0]*8]}
                    if 'mission_state' not in picked_char:
                        picked_char['mission_state'] = {}
                        for mid in picked_char['active_missions']:
                            picked_char['mission_state'][mid] = init_mission_state(mid)
                    save_chars(all_accounts_chars)
                    map_id, line_idx = "11", 1; online_clients[char_id] = (conn, map_id, line_idx, picked_char)
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))
                    send_rpc_push(519, get_mission_sync(picked_char))
                    send_rpc_push(503, encode_sproto([(0, map_id), (1, line_idx), (2, 1)]))
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, get_movement(29860, 100, -17005))]))

            elif msg == 100: # map_ready
                if picked_char:
                    send_rpc_push(611, encode_sproto([(0, [])]))
                    send_rpc_push(540, get_skill_sync(picked_char))
                    sync_mission_world_objects(picked_char, send_rpc_push)
                    send_rpc_push(654, encode_sproto([(0, 1)]))

            elif msg == 101: # move
                if session is not None and picked_char:
                    p_raw = body.get(0)
                    if p_raw:
                        pd = decode_sproto(p_raw)
                        picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                        save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, p_raw)]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 111: # accept_damge
                if session is not None and picked_char:
                    dmgs = body.get(0, [])
                    if isinstance(dmgs, bytes):
                        r, dmgs, p = dmgs, [], 0
                        while p < len(r):
                            l = struct.unpack("<I", r[p:p+4])[0]
                            dmgs.append(decode_sproto(r[p+4:p+4+l])); p += 4 + l
                    for d in dmgs:
                        tid, val = get_val_int(d, 0), get_val_int(d, 1)
                        cur_h = npc_hps.get(tid, 1000)
                        new_hp = cur_h - val
                        if new_hp < 0: new_hp = 0
                        npc_hps[tid] = new_hp
                        send_rpc_push(510, encode_sproto([(0, tid), (1, encode_sproto([(0, new_hp)]))]))
                        if new_hp == 0:
                            send_rpc_push(506, encode_sproto([(0, tid)]))
                            for mid, ms in picked_char.get('mission_state', {}).items():
                                    if tid in ms.get('alive_sids', []):
                                        ms['alive_sids'].remove(tid)
                                        ms['progress'] += 1
                                        if mid in picked_char['active_missions']:
                                            picked_char['active_missions'][mid][2][0] = ms['progress']
                                            logic = mission_logic_db.get(mid, {})
                                            lid = logic.get('logicId')
                                            ltype = logic.get('logicType')
                                            if ltype == 23:
                                                req = kill_target_db.get(lid, {}).get('require', 1)
                                            else:
                                                req = mission_require_db.get(lid, {}).get('require', 1)
                                            
                                            if ms['progress'] >= req:
                                                picked_char['active_missions'][mid][0] = 2 # COMPLETE
                                                send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                                        send_rpc_push(519, get_mission_sync(picked_char))
                                        save_chars(all_accounts_chars)
                                        break
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 113: # complete_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    if mid in picked_char.get('active_missions', {}):
                        del picked_char['active_missions'][mid]
                        if 'mission_state' in picked_char and mid in picked_char['mission_state']:
                            del picked_char['mission_state'][mid]
                        picked_char['last_main_mission'] = mid
                        logic = mission_logic_db.get(mid)
                        if logic and logic['nextId'] and logic['nextId'] != "#N/A":
                            next_mid = logic['nextId']
                            picked_char['active_missions'][next_mid] = [1, 0, [0]*8]
                            picked_char['mission_state'][next_mid] = init_mission_state(next_mid)
                            print(f"[MISSION] {mid} -> {next_mid}")
                        save_chars(all_accounts_chars)
                        send_rpc_push(519, get_mission_sync(picked_char))
                        sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 112: # accept_mission
                if session is not None and picked_char:
                    mid = body.get(0, b"").decode('utf-8')
                    picked_char['active_missions'][mid] = [1, 0, [0]*8]
                    if 'mission_state' not in picked_char: picked_char['mission_state'] = {}
                    picked_char['mission_state'][mid] = init_mission_state(mid)
                    save_chars(all_accounts_chars); send_rpc_push(519, get_mission_sync(picked_char))
                    sync_mission_world_objects(picked_char, send_rpc_push)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 238: # use_mount (rob car)
                if session is not None and picked_char:
                    for mid, mdata in picked_char.get('active_missions', {}).items():
                        logic = mission_logic_db.get(mid)
                        if logic and logic['logicType'] == 24:
                            mdata[2][0], mdata[0] = 1, 2
                            send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]))
                            send_rpc_push(519, get_mission_sync(picked_char))
                    save_chars(all_accounts_chars)
                    ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 7: # game server list
                srv = [encode_sproto([(0, 302), (1, "EU-001"), (2, "tokaido.proxy.rlwy.net"), (3, 48282), (4, 1), (5, 1), (6, 1), (7, 0), (8, 1), (9, 1)])]
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([(0, srv)]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 310, 313, 319]:
                resp = encode_sproto([(0, f"U_{random.randint(10,99)}")]) if msg == 118 else encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))]) if msg == 218 else encode_sproto([])
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
                if msg == 310: send_rpc_push(684, encode_sproto([]))
                elif msg == 145: send_rpc_push(555, encode_sproto([(0, [])]))

            elif session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally:
        if picked_char:
            cid = picked_char['id']
            if cid in online_clients: del online_clients[cid]
        conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (v16 DATA-DRIVEN)")
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
