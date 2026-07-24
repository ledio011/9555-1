# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - MULTI-SLOT & VISUAL v8
# GAME SERVER 9555
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters_v8.json"
server_session_counter = 5000

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

# Structure: { acc_id: [char_dict, char_dict, ...] }
all_accounts_chars = load_chars()

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

def encode_sproto(fields, fn=None):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    if fn is None: fn = fields[-1][0] + 1
    header = [1] * fn; body = bytearray(); vals = {t: v for t, v in fields}
    for t in range(fn):
        if t not in vals: continue
        v = vals[t]
        if v is None: header[t] = 1
        elif isinstance(v, int):
            if 0 <= v <= 32766: header[t] = (v + 1) * 2
            else: header[t] = 0; body += struct.pack("<I", 8) + struct.pack("<q", v)
        elif isinstance(v, (str, bytes, bytearray)):
            if isinstance(v, str): v = v.encode('utf-8')
            header[t] = 0; body += struct.pack("<I", len(v)) + v
        elif isinstance(v, list):
            header[t] = 0; lbin = bytearray()
            for it in v:
                if isinstance(it, (bytes, bytearray)): lbin += struct.pack("<I", len(it)) + it
                elif isinstance(it, int): lbin += struct.pack("<I", 8) + struct.pack("<q", it)
                else: s = str(it).encode('utf-8'); lbin += struct.pack("<I", len(s)) + s
            body += struct.pack("<I", len(lbin)) + lbin
    res = struct.pack("<H", fn)
    for h in header: res += struct.pack("<H", h)
    return res + body

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    try:
        fn = struct.unpack("<H", data[offset:offset+2])[0]
        hp, bp = offset + 2, offset + 2 + fn*2
        res, tag = {}, -1
        for i in range(fn):
            tag += 1; v = struct.unpack("<H", data[hp + i*2 : hp + i*2 + 2])[0]
            if v == 0:
                if bp + 4 <= len(data):
                    l = struct.unpack("<I", data[bp:bp+4])[0]
                    rv = data[bp+4:bp+4+l]
                    if l == 4: res[tag] = struct.unpack("<I", rv)[0]
                    elif l == 8: res[tag] = struct.unpack("<q", rv)[0]
                    else: res[tag] = rv
                    bp += 4 + l
            elif v == 1: pass
            elif v & 1: tag += (v >> 1)
            else: res[tag] = (v >> 1) - 1
        return res
    except: return {}

def get_visual(name, prof):
    # Mapping bazuar ne CharacterModelData.csv & ModelData.csv
    # XD (0), QJ (1), NQS (2)
    # Përdorim suffix _A sepse këto bundle ka useri
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[0])
    return encode_sproto([
        (0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)
    ], 17)

def get_char_ov(c):
    # Tag 1: General (name, prof, line, map, tutorial)
    gen = encode_sproto([(0, c['name']), (1, c['prof']), (2, 1), (3, "11"), (4, 1)], 5)
    # Tag 2: Attribute (hp, exp, level, combValue...)
    attr = encode_sproto([(0, 10560), (1, 0), (2, 1), (3, 55653)], 19)
    # Character Overview (Tag 0:id, 1:general, 2:attribute, 3:visual, 4:lasttime)
    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr), (3, get_visual(c['name'], c['prof'])), (4, int(time.time()))
    ], 6)

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"
    global server_session_counter
    
    def send_rpc_push(tag, data):
        try:
            global server_session_counter
            server_session_counter += 1
            ph_p = encode_sproto([(0, tag), (1, server_session_counter)], 2)
            pf_p = sproto_pack(ph_p + data)
            conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            print(f"[TX] {tag} (Session: {server_session_counter})")
            return pf_p
        except Exception:
            return b""

    try:
        while True:
            header_bytes = conn.recv(2)
            if not header_bytes: break
            size = struct.unpack(">H", header_bytes)[0]
            data = b""
            while len(data) < size: data += conn.recv(size - len(data))
            
            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = pkg.get(0), pkg.get(1)
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8', 'ignore') if isinstance(body.get(1), bytes) else str(body.get(1))
                print(f"[LOGIN] {acc_id}")
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "1000"), (3, 1)], 4)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                char_list = []
                if acc_id in all_accounts_chars:
                    for c in all_accounts_chars[acc_id]:
                        char_list.append(get_char_ov(c))
                resp = encode_sproto([(0, char_list)], 1)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # character_create
                gd = decode_sproto(body.get(0, b"")) if isinstance(body.get(0), bytes) else {}
                name = gd.get(0, b"").decode('utf-8') if isinstance(gd.get(0), bytes) else "Hero"
                prof = gd.get(1, 0)
                cid = random.randint(1000000, 9999999)
                
                new_char = {'id': cid, 'name': name, 'prof': prof, 'map': "11"}
                if acc_id not in all_accounts_chars: all_accounts_chars[acc_id] = []
                # Limitojmë në 4 slote
                if len(all_accounts_chars[acc_id]) < 4:
                    all_accounts_chars[acc_id].append(new_char)
                
                save_chars(all_accounts_chars)
                print(f"[CREATED] {name} for {acc_id}")
                resp = encode_sproto([(0, get_char_ov(new_char)), (1, 0)], 2)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # pick character
                char_id = body.get(0)
                print(f"[PICK] ID: {char_id}")
                resp = encode_sproto([(0, 1)], 1); ph = encode_sproto([(1, session)], 2)
                conn.sendall(struct.pack(">H", len(sproto_pack(ph + resp))) + sproto_pack(ph + resp))
                
                # Gjejmë karakterin e përzgjedhur
                current_char = None
                for c in all_accounts_chars.get(acc_id, []):
                    if c['id'] == char_id:
                        current_char = c; break
                
                if current_char:
                    time.sleep(0.2)
                    sync = encode_sproto([(0, int(time.time())), (12, 12345), (13, 1)], 15)
                    send_rpc_push(614, sync)
                    
                    time.sleep(0.2)
                    send_rpc_push(503, encode_sproto([(0, "11"), (1, 1), (2, 1)], 3))
                    
                    time.sleep(0.5)
                    p = current_char['prof']
                    # Skill 101/201/301 ne Pozicionin 3 (Butoni Attack)
                    if p == 0: s_ids = [("101", 3), ("105", 4)] # XD
                    elif p == 1: s_ids = [("201", 3), ("205", 4)] # QJ
                    else: s_ids = [("301", 3), ("305", 4)] # NQS
                    
                    skill_list = []
                    for sid, pos in s_ids:
                        si = encode_sproto([(0, sid), (1, 1), (2, pos), (3, 1), (4, 0), (5, False)], 6)
                        skill_list.append(si)
                    
                    # Character Visual & Parts
                    vis = get_visual(current_char['name'], p)
                    gn = encode_sproto([(0, current_char['name']), (1, p), (2, 1), (3, "11"), (4, 1)], 5)
                    ps = encode_sproto([(0, 7007), (1, 100), (2, 5033), (3, 0)], 4)
                    mv = encode_sproto([(0, ps), (1, ps)], 2)
                    attr = encode_sproto([(0, 10560), (2, 1), (3, 55653), (15, 0), (16, 0)], 19)
                    
                    char_obj = encode_sproto([
                        (0, current_char['id']), (1, gn), (2, attr), (6, vis), (7, mv), (8, skill_list), (15, 2)
                    ], 17)
                    
                    send_rpc_push(504, encode_sproto([(0, char_obj), (1, mv)], 2))

            elif msg == 100: # map_ready
                print(f"[RX] 100 MAP_READY")
                time.sleep(0.5)
                # PUSH MISSIONS (ID 46001 eshte e Hartes 11)
                m1 = encode_sproto([(0, "46001"), (1, 1), (2, 1), (3, [0])], 4)
                m_sync = encode_sproto([(0, {"46001": m1}), (1, "46001")], 3)
                send_rpc_push(654, m_sync)
                
                time.sleep(0.2)
                send_rpc_push(505, encode_sproto([(0, 0)], 1))

            elif msg == 218: # heartbeat
                resp = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))], 2)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            else: # Default response for unhandled tags
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + encode_sproto([], 0))
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (STABLE v8)");
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
