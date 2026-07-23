# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - MAP ENTRY PATCH
# GAME SERVER 9555
# ==========================================================
import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters.json"

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

characters = load_chars()

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
            if 0 <= v <= 32766: header[t] = (val + 1) * 2 if 'val' in locals() else (v + 1) * 2 # Fixed val/v typo
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

# Fixed encode_sproto to use 'v' instead of 'val'
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
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[0])
    return encode_sproto([(0,name),(1,v["m"]),(2,v["h"]),(3,v["b"]),(4,v["l"]),(5,v["w"]),(10,0)], 17)

def get_char_ov(c):
    gen = encode_sproto([(0,c['name']),(1,c['prof']),(2,1),(3,"101")], 5)
    attr = encode_sproto([(0,1),(1,55653)], 2)
    return encode_sproto([(0,c['id']),(1,gen),(2,attr),(3,get_visual(c['name'],c['prof'])),(4,int(time.time()))], 6)

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}"); acc_id = "0"
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size: data += conn.recv(size - len(data))
            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = pkg.get(0), pkg.get(1)
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8', 'ignore') if isinstance(body.get(1), bytes) else str(body.get(1))
                resp = encode_sproto([(0,2),(1,"1.012.017"),(2,"1000"),(3,1)], 4)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 103: # character_list
                resp = encode_sproto([(0, [get_char_ov(characters[acc_id])])], 1) if acc_id in characters else encode_sproto([(0, [])], 1)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 104: # create
                gd = decode_sproto(body.get(0, b"")) if isinstance(body.get(0), bytes) else {}
                name = gd.get(0, b"").decode('utf-8') if isinstance(gd.get(0), bytes) else "Hero"
                cid = random.randint(1000000, 9999999)
                characters[acc_id] = {'id': cid, 'name': name, 'prof': gd.get(1, 0), 'map': "101"}
                save_chars(characters); print(f"[CREATED] {name}")
                resp = encode_sproto([(0, get_char_ov(characters[acc_id])), (1, 0)], 2)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 105: # pick
                print(f"[TX] 105 RESPONSE")
                resp = encode_sproto([(0, 1)], 1); ph = encode_sproto([(1, session)], 2)
                conn.sendall(struct.pack(">H", len(sproto_pack(ph + resp))) + sproto_pack(ph + resp))
                
                def send_push(tag, data):
                    print(f"[TX] {tag}")
                    ph_push = encode_sproto([(0, tag)], 2) # PUSH (Request without session)
                    pf_push = sproto_pack(ph_push + data)
                    conn.sendall(struct.pack(">H", len(pf_push)) + pf_push)

                time.sleep(0.1)
                sync = encode_sproto([(0, int(time.time())), (12, 12345), (13, 1)], 15)
                send_push(614, sync)
                time.sleep(0.1)
                map_e = encode_sproto([(0, "101"), (1, 1), (2, 1)], 3)
                send_push(503, map_e)

            elif msg == 100: # map_ready
                print(f"[RX] 100 MAP_READY")
                c = characters.get(acc_id)
                if c:
                    af = encode_sproto([(0, 10560), (2, 500), (3, 300), (13, 800)], 25)
                    rt = encode_sproto([(6, af), (7, af)], 8)
                    gn = encode_sproto([(0, c['name']), (1, c['prof']), (2, 1), (3, "101"), (4, 1)], 5)
                    ps = encode_sproto([(0, 1500), (1, 500), (2, 2000), (3, 0)], 4)
                    mv = encode_sproto([(0, ps), (1, ps)], 2)
                    char_obj = encode_sproto([(0, c['id']), (1, gn), (2, encode_sproto([(0, 10560), (2, 1), (3, 55653), (15, 1)], 19)), (5, encode_sproto([(13, 0)], 19)), (6, get_visual(c['name'], c['prof'])), (7, mv), (13, rt), (15, 2)], 17)
                    
                    print(f"[TX] 504")
                    ph504 = encode_sproto([(0, 504)], 2)
                    pf504 = sproto_pack(ph504 + encode_sproto([(0, char_obj), (1, mv)], 2))
                    conn.sendall(struct.pack(">H", len(pf504)) + pf504)
                    
                    time.sleep(0.5)
                    print(f"[TX] 654")
                    ph654 = encode_sproto([(0, 654)], 2)
                    pf654 = sproto_pack(ph654 + encode_sproto([(0, 1)], 1))
                    conn.sendall(struct.pack(">H", len(pf654)) + pf654)

            elif msg == 218: # heartbeat
                resp = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))], 2)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + r if 'r' in locals() else ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg in [118, 121, 139, 141, 145]:
                r = encode_sproto([(0, f"Hero_{random.randint(100,999)}")], 1) if msg == 118 else encode_sproto([], 0)
                ph = encode_sproto([(1, session)], 2); pf = sproto_pack(ph + r)
                conn.sendall(struct.pack(">H", len(pf)) + pf)

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (STUCK FIX)");
while True: cl, ad = server.accept(); threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()
