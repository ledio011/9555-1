# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - STABLE v16 FINAL GAMEPLAY
# GAME SERVER 9555
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters_final.json"
server_session_counter = 8000

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
                for k in sorted(val.keys()):
                    item = val[k]
                    if isinstance(item, str): item = item.encode('utf-8')
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else: # Fallback for primitive types
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
    # Matches characterVisual.cs: 0:name, 1:ModeId, 2:HeadId, 3:BodyId, 4:LegId, 5:WeaponId, 10:showType
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

def get_general(c):
    # Matches general.cs: 0:name, 1:profession, 2:lineIndex, 3:mapInfoId, 4:tutorial
    return encode_sproto([
        (0, c.get('name', 'Hero')),
        (1, c.get('prof', 0)),
        (2, 1), # lineIndex
        (3, c.get('map', "11")), # mapInfoId
        (4, c.get('tutorial', 0)) # tutorial state
    ])

def get_movement(x, y, z, o=0):
    # Matches movement.cs: 0:pos (position), 1:pos2 (position)
    # position.cs: 0:x, 1:y, 2:z, 3:o
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c):
    gen = get_general(c)
    # attribute_overview.cs: 0:level, 1:combValue
    attr = encode_sproto([(0, c.get('level', 1)), (1, 5000)])
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

    # attribute_other.cs: 0:hp, 1:exp, 2:level, 3:combValue, ... 14:vip, 15:camp, 16:pkMode
    attr_oth = encode_sproto([
        (0, 3000),
        (1, c.get('exp', 0)),
        (2, c.get('level', 1)),
        (3, 5000),
        (14, 0), # vip
        (15, 0), # camp: 0=PLAYER_1
        (16, 0)  # pkMode: 0=Peace
    ])

    # property.cs: 13:money1, 14:money2, 15:money3
    prop = encode_sproto([
        (13, c.get('cash', 1000)),
        (14, c.get('gold', 100)),
        (15, c.get('diamond', 10)),
        (16, 0), (17, 0), (18, 0)
    ])

    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])

    # runtime_agent.cs -> attribute.cs (Tag 6 and 7)
    attr_run = encode_sproto([(0, 3000), (2, 300), (3, 35)])
    attr_all = encode_sproto([(0, 3000), (2, 300), (3, 35), (13, 500)])
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    prof = c.get('prof', 0)
    wid = "10001" if prof == 0 else "20001" if prof == 1 else "30001"

    # skills map string->skill_info
    skills_map = c.get('skills', {})
    if not skills_map:
        sid = "101" if prof == 0 else "201" if prof == 1 else "301"
        did = "104" if prof == 0 else "204" if prof == 1 else "304"
        s1 = encode_sproto([(0, sid), (1, 1), (2, 0), (3, 1), (4, 0), (5, False)])
        s2 = encode_sproto([(0, did), (1, 1), (2, 3), (3, 1), (4, 1), (5, False)])
        skills_map = {sid: s1, did: s2}

    # equip map long->gameitem
    equip_map = c.get('equip', {})
    if not equip_map:
        for i in range(6):
            item_id = wid if i == 5 else ""
            equip_map[i] = encode_sproto([(0, i), (1, item_id), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])

    # character.cs: 0:id, 1:gen, 2:attr_oth, 3:property, 6:visual, 7:mv, 8:skills, 9:equip, 13:run, 14:skill_index
    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr_oth), (5, prop),
        (6, get_visual(c.get('name', 'Hero'), prof)),
        (7, mv), (8, skills_map), (9, equip_map), (13, run), (16, 0)
    ])

def push_npcs_for_map(conn, map_id):
    def send_rpc_push(tag, data_encoded):
        ph_p = encode_sproto([(0, tag)])
        pf_p = sproto_pack(ph_p + data_encoded)
        conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)

    if map_id == "11": # Tutorial Car Scene
        # npc_attribute.cs tags: 0:id, 1:npcdataid, 2:hp, 3:max_hp, 15:x, 16:z, 17:o, 18:level, 21:player_name
        npc1 = encode_sproto([
            (0, 50001), (1, "1001"), (2, 1000), (3, 1000),
            (15, 29860), (16, -17005), (17, 0), (18, 1), (21, "Guide")
        ])
        send_rpc_push(509, encode_sproto([(0, npc1)]))

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"; picked_char = None; cur_areaId = 0
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

            def send_rpc_push(tag, data_encoded):
                ph_p = encode_sproto([(0, tag)])
                pf_p = sproto_pack(ph_p + data_encoded)
                conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8') if isinstance(body.get(1), bytes) else str(body.get(1))
                sid = get_val_int(body, 5, 1); cur_areaId = get_area_id(sid)
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "200"), (3, 1)])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + resp))) + sproto_pack(encode_sproto([(1, session)]) + resp))

            elif msg == 103: # character_list
                chars = all_accounts_chars.get(cur_areaId, {}).get(acc_id, [])
                resp = encode_sproto([(0, [get_char_ov(c) for c in chars])])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + resp))) + sproto_pack(encode_sproto([(1, session)]) + resp))

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8') if isinstance(c_data.get(0), bytes) else str(c_data.get(0, "Hero"))
                prof = get_val_int(c_data, 1, 0); cid = generate_unique_char_id()
                if cur_areaId not in all_accounts_chars: all_accounts_chars[cur_areaId] = {}
                if acc_id not in all_accounts_chars[cur_areaId]: all_accounts_chars[cur_areaId][acc_id] = []
                # Initial state for new char
                nc = {'id': cid, 'name': name, 'prof': prof, 'level': 1, 'exp': 0, 'cash': 1000, 'map': "11", 'pos': [29860, 100, -17005, 0]}
                all_accounts_chars[cur_areaId][acc_id].append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + resp))) + sproto_pack(encode_sproto([(1, session)]) + resp))

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(cur_areaId, {}).get(acc_id, []) if c['id'] == char_id), None)
                resp = encode_sproto([(0, 1 if picked_char else 0)])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + resp))) + sproto_pack(encode_sproto([(1, session)]) + resp))
                if picked_char:
                    # sync_common_data (Tag 614): func_info unlocks UI.
                    # Unlocking most systems to ensure full client logic availability
                    fids = ["100", "101", "107", "108", "3001", "3002", "3003", "3004", "3005", "3006", "3010", "3011", "3012", "3013", "3014", "3015", "3016", "3017", "3018", "3020", "4051", "4052", "4053", "4054", "4055", "4061", "4063", "4071", "4081", "4084", "4087", "4089"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))

                    # sync_mission (Tag 519)
                    missions_map = picked_char.get('missions', {})
                    if not missions_map:
                        p = [0, 0, 0, 0, 0, 0, 0, int(time.time())]
                        m1001 = encode_sproto([(0, "1001"), (1, 1), (2, 0), (3, p)])
                        missions_map = {"1001": m1001}
                    send_rpc_push(519, encode_sproto([(0, missions_map), (1, picked_char.get('last_mid', "1001"))]))

                    # enter_map (Tag 503)
                    send_rpc_push(503, encode_sproto([(0, picked_char.get('map', "11")), (1, 1), (2, 1)]))

                    # main_player_create (Tag 504)
                    pos = picked_char.get('pos', [29860, 100, -17005, 0])
                    mv = get_movement(pos[0], pos[1], pos[2], pos[3])
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, mv)]))

            elif msg == 100: # map_ready
                if picked_char:
                    # sync_item_pack (Tag 611)
                    inv = picked_char.get('inventory', {})
                    send_rpc_push(611, encode_sproto([(0, inv)]))
                    # sync_skill_info (Tag 540)
                    skills = picked_char.get('skills', {})
                    send_rpc_push(540, encode_sproto([(0, skills), (1, False)]))
                    # start_enter_game (Tag 654)
                    send_rpc_push(654, encode_sproto([(0, 1)]))
                    # Spawn Map NPCs
                    push_npcs_for_map(conn, picked_char.get('map', "11"))

            elif msg == 101: # move
                if session is not None:
                    p_raw = body.get(0)
                    if p_raw and picked_char:
                        pd = decode_sproto(p_raw)
                        picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                        save_chars(all_accounts_chars)
                    conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, p_raw)])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, p_raw)])))

            elif msg == 113: # complete_mission
                mid = body.get(0, b"").decode('utf-8')
                if picked_char:
                    picked_char.setdefault('missions', {}).pop(mid, None)
                    picked_char['last_mid'] = mid
                    save_chars(all_accounts_chars)
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))

            elif msg == 112: # accept_mission
                mid = body.get(0, b"").decode('utf-8')
                if picked_char:
                    p = [0, 0, 0, 0, 0, 0, 0, int(time.time())]
                    m_new = encode_sproto([(0, mid), (1, 1), (2, 0), (3, p)])
                    picked_char.setdefault('missions', {})[mid] = m_new
                    save_chars(all_accounts_chars)
                    send_rpc_push(519, encode_sproto([(0, {mid: m_new}), (1, mid)]))
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))

            elif msg == 116: # equip_item
                idx = get_val_int(body, 0)
                # Logic: move from picked_char['inventory'][idx] to picked_char['equip'][slot]
                # Stub: just ACK
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 310, 313, 319]:
                rd = encode_sproto([])
                if msg == 118: rd = encode_sproto([(0, f"User_{random.randint(100,999)}")])
                elif msg == 218: rd = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + rd))) + sproto_pack(encode_sproto([(1, session)]) + rd))
                if msg == 310: send_rpc_push(684, encode_sproto([]))
                elif msg == 145: send_rpc_push(555, encode_sproto([(0, [])]))

            elif session is not None:
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))

            elif msg in [118, 218, 145, 225, 258, 261, 278, 296, 299, 310, 313, 319]:
                rd = encode_sproto([])
                if msg == 118: rd = encode_sproto([(0, f"User_{random.randint(100,999)}")])
                elif msg == 218: rd = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + rd))) + sproto_pack(encode_sproto([(1, session)]) + rd))
                if msg == 310: send_rpc_push(684, encode_sproto([]))
                elif msg == 145: send_rpc_push(555, encode_sproto([(0, [])]))

            elif session is not None:
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (STABLE v16)");
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
