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
                    for item in value:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)): pass
                        else: item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for k in sorted(val.keys()):
                    item = val[k]
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
    # Tag 4: tutorial (0=start). Client forces move to Waypoint 0 (near thugs) if 0.
    return encode_sproto([
        (0, c.get('name', 'Hero')),
        (1, c.get('prof', 0)),
        (2, 1), # lineIndex
        (3, "11"), # mapInfoId
        (4, 0) # tutorial state
    ])

def get_movement(x, y, z, o=0):
    # Matches SprotoType.position (Tags 0-3: x, y, z, o)
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c):
    gen = get_general(c)
    attr = encode_sproto([(0, 1), (1, 5000)]) # level, combValue
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
    attr_oth = encode_sproto([(0, 3000), (1, 0), (2, 1), (3, 5000), (15, 1)])
    prop = encode_sproto([(13, 1000), (14, 100), (15, 10), (16, 0), (17, 0), (18, 0)])

    # Position Persistence: Default to Mission 1001 area for new chars
    # Raw int coords in cm.
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])

    attr_run = encode_sproto([(0, 3000), (2, 300), (3, 35)])
    attr_all = encode_sproto([(0, 3000), (2, 300), (3, 35), (13, 500)])
    run = encode_sproto([(6, attr_run), (7, attr_all)])

    prof = c.get('prof', 0)
    sid = "101" if prof == 0 else "201" if prof == 1 else "301"
    did = "104" if prof == 0 else "204" if prof == 1 else "304"
    wid = "10001" if prof == 0 else "20001" if prof == 1 else "30001"

    # Tag 8: skills map string->skill_info: Attack(Slot 0), Dodge(Slot 3)
    s1 = encode_sproto([(0, sid), (1, 1), (2, 0), (3, 1), (4, 0), (5, False)])
    s2 = encode_sproto([(0, did), (1, 1), (2, 3), (3, 1), (4, 1), (5, False)])
    skills_map = {sid: s1, did: s2}

    # Tag 9: equip map long->gameitem. Fill all 6 slots to ensure weapon is in Slot 5.
    equip_map = {}
    for i in range(6):
        item_id = wid if i == 5 else ""
        equip_map[i] = encode_sproto([(0, i), (1, item_id), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])

    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr_oth), (5, prop),
        (6, get_visual(c.get('name', 'Hero'), prof)),
        (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)
    ])

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
                nc = {'id': cid, 'name': name, 'prof': prof}
                all_accounts_chars[cur_areaId][acc_id].append(nc); save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(nc)), (1, 0)])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + resp))) + sproto_pack(encode_sproto([(1, session)]) + resp))

            elif msg == 105: # character_pick
                char_id = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(cur_areaId, {}).get(acc_id, []) if c['id'] == char_id), None)
                resp = encode_sproto([(0, 1 if picked_char else 0)])
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + resp))) + sproto_pack(encode_sproto([(1, session)]) + resp))
                if picked_char:
                    # ORDER: 614 -> 519 -> 503 -> 504. Populates QuestManager BEFORE SceneManager initialization.
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    send_rpc_push(614, encode_sproto([(0, int(time.time())), (2, 0), (9, funcs), (13, 1), (14, int(time.time()))]))

                    p = [0, 0, 0, 0, 0, 0, 0, int(time.time())]
                    m1001 = encode_sproto([(0, "1001"), (1, 1), (2, 0), (3, p)])
                    send_rpc_push(519, encode_sproto([(0, {"1001": m1001}), (1, "1001")]))

                    send_rpc_push(503, encode_sproto([(0, "11"), (1, 1), (2, 1)]))

                    pos = picked_char.get('pos', [29860, 100, -17005, 0])
                    mv = get_movement(pos[0], pos[1], pos[2], pos[3])
                    send_rpc_push(504, encode_sproto([(0, get_full_char(picked_char)), (1, mv)]))

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
                    conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, p_raw)])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, p_raw)])))

            elif msg == 113: # complete_mission
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))

            elif msg == 112: # accept_mission
                conn.sendall(struct.pack(">H", len(sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))) + sproto_pack(encode_sproto([(1, session)]) + encode_sproto([])))
                mid = body.get(0, b"").decode('utf-8')
                m_new = encode_sproto([(0, mid), (1, 1), (2, 0), (3, [0]*8)])
                send_rpc_push(519, encode_sproto([(0, {mid: m_new}), (1, mid)]))
