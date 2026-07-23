# ==========================================================
# AUTO THEFT GANGSTERS REVIVAL - PERFECT ALIGNMENT
# GAME SERVER 9555
# ==========================================================

import socket
import struct
import threading
import random
import json
import os
import time
import traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters.json"
server_session_counter = 5000

# ==========================================================
# DATABASE
# ==========================================================
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

# ==========================================================
# SPROTO PACK/UNPACK (BIT-PERFECT FIX)
# ==========================================================
def sproto_pack(data):
    out = bytearray()
    i = 0
    n = len(data)
    while i < n:
        chunk = data[i:i+8]
        if len(chunk) < 8: chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0: mask |= (1 << j)
        
        if mask == 0xFF:
            # Sproto Spec: 0xFF indicates a sequence of unpacked blocks.
            # Byte following 0xFF is (number_of_blocks - 1)
            out.append(0xFF)
            out.append(0) # We send 1 block (0 means 1)
            out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j): out.append(chunk[j])
        i += 8
    return bytes(out)

def sproto_unpack(data):
    out = bytearray()
    i = 0
    n = len(data)
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

# ==========================================================
# SPROTO ENCODE/DECODE
# ==========================================================
def encode_sproto(fields, fn=None):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    if fn is None: fn = fields[-1][0] + 1
    header = [1] * fn
    body = bytearray()
    values = {tag: val for tag, val in fields}
    for tag in range(fn):
        if tag not in values: continue
        val = values[tag]
        if val is None: header[tag] = 1
        elif isinstance(val, int):
            if 0 <= val <= 32766: header[tag] = (val + 1) * 2
            else:
                header[tag] = 0
                body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray)):
            if isinstance(val, str): val = val.encode('utf-8')
            header[tag] = 0
            body += struct.pack("<I", len(val)) + val
        elif isinstance(val, list):
            header[tag] = 0
            list_bin = bytearray()
            for item in val:
                if isinstance(item, (bytes, bytearray)): list_bin += struct.pack("<I", len(item)) + item
                elif isinstance(item, int): list_bin += struct.pack("<I", 8) + struct.pack("<q", item)
                else:
                    s = str(item).encode('utf-8')
                    list_bin += struct.pack("<I", len(s)) + s
            body += struct.pack("<I", len(list_bin)) + list_bin
    res = struct.pack("<H", fn)
    for h in header: res += struct.pack("<H", h)
    res += body
    return bytes(res)

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    try:
        fn = struct.unpack("<H", data[offset:offset+2])[0]
        h_ptr, b_ptr = offset + 2, offset + 2 + fn*2
        fields, tag = {}, -1
        for i in range(fn):
            tag += 1
            v = struct.unpack("<H", data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
            if v == 0:
                if b_ptr + 4 <= len(data):
                    l = struct.unpack("<I", data[b_ptr:b_ptr+4])[0]
                    raw = data[b_ptr+4:b_ptr+4+l]
                    if l == 4: fields[tag] = struct.unpack("<I", raw)[0]
                    elif l == 8: fields[tag] = struct.unpack("<q", raw)[0]
                    else: fields[tag] = raw
                    b_ptr += 4 + l
            elif v == 1: pass
            elif v & 1: tag += (v >> 1)
            else: fields[tag] = (v >> 1) - 1
        return fields
    except: return {}

# ==========================================================
# CHARACTER DATA HELPERS
# ==========================================================
def create_visual(name, prof):
    v_map = {
        0: {"mode":"100", "head":"XD_A_T", "body":"XD_A_S", "leg":"XD_A_X", "weapon":"XD_A_WQ"},
        1: {"mode":"104", "head":"QJ_A_T", "body":"QJ_A_S", "leg":"QJ_A_X", "weapon":"QJ_A_WQ"},
        2: {"mode":"105", "head":"NQS_A_T", "body":"NQS_A_S", "leg":"NQS_A_X", "weapon":"NQS_A_WQ"}
    }
    v = v_map.get(prof, v_map[0])
    return encode_sproto([(0, name), (1, v["mode"]), (2, v["head"]), (3, v["body"]), (4, v["leg"]), (5, v["weapon"]), (10, 0)], fn=17)

def create_attribute_other(hp=10560, level=1, power=55653):
    return encode_sproto([(0, hp), (2, level), (3, power), (15, 1)], fn=19)

def client_handler(conn, addr):
    print(f"[+] Connected: {addr}")
    acc_id = "0"
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size: data += conn.recv(size - len(data))
            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = pkg.get(0), pkg.get(1)
            body_off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, body_off)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8', 'ignore') if isinstance(body.get(1), bytes) else str(body.get(1))
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "1000"), (3, 1)], fn=4)
                pkg_h = encode_sproto([(1, session)], fn=2); conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h + resp))) + sproto_pack(pkg_h + resp))

            elif msg == 103: # character_list
                if acc_id in characters:
                    c = characters[acc_id]
                    gen = encode_sproto([(0, c['name']), (1, c['prof']), (3, "101")], fn=5)
                    vis = create_visual(c['name'], c['prof'])
                    attr = create_attribute_other()
                    char_ov = encode_sproto([(0, c['id']), (1, gen), (2, attr), (3, vis), (4, int(time.time()))], fn=6)
                    resp = encode_sproto([(0, [char_ov])], fn=1)
                else: resp = encode_sproto([(0, [])], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2); conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h + resp))) + sproto_pack(pkg_h + resp))

            elif msg == 118: # random_name
                name = f"Hero_{random.randint(100, 999)}"
                resp = encode_sproto([(0, name)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2); conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h + resp))) + sproto_pack(pkg_h + resp))

            elif msg == 104: # create
                gen_data = decode_sproto(body.get(0, b"")) if isinstance(body.get(0), bytes) else {}
                name = gen_data.get(0, b"").decode('utf-8') if isinstance(gen_data.get(0), bytes) else "Hero"
                char_id = random.randint(1000000, 9999999)
                characters[acc_id] = {'id': char_id, 'name': name, 'prof': gen_data.get(1, 0), 'map': "101"}
                save_chars(characters)
                resp = encode_sproto([(0, char_id), (1, 0)], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2); conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h + resp))) + sproto_pack(pkg_h + resp))

            elif msg == 105: # pick
                print(f"[PICK] {acc_id}")
                resp = encode_sproto([(0, 1)], fn=1); pkg_h = encode_sproto([(1, session)], fn=2)
                conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h + resp))) + sproto_pack(pkg_h + resp))
                # Immediate pushes
                global server_session_counter
                def send_push(tag, data):
                    global server_session_counter
                    server_session_counter += 1
                    ph = encode_sproto([(0, tag), (1, server_session_counter)], fn=2)
                    pf = sproto_pack(ph + data)
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

                send_push(614, encode_sproto([(0, int(time.time())), (12, 12345), (13, 1)], fn=14))
                send_push(503, encode_sproto([(0, "101"), (1, 1), (2, 1)], fn=3))

            elif msg == 100: # map_ready
                c = characters.get(acc_id)
                if c:
                    attr_f = encode_sproto([(0, 10560), (2, 500), (3, 300), (13, 800)], fn=25)
                    runtime = encode_sproto([(6, attr_f), (7, attr_f)], fn=8)
                    gen = encode_sproto([(0, c['name']), (1, c['prof']), (3, "101"), (4, 1)], fn=5)
                    pos = encode_sproto([(0, 1500), (1, 500), (2, 2000), (3, 0)], fn=4)
                    char_obj = encode_sproto([(0, c['id']), (1, gen), (2, create_attribute_other()), (5, encode_sproto([(13, 0)], fn=19)), (6, create_visual(c['name'], c['prof'])), (7, encode_sproto([(0, pos), (1, pos)], fn=2)), (13, runtime), (15, 2)], fn=17)
                    
                    server_session_counter += 1
                    ph = encode_sproto([(0, 504), (1, server_session_counter)], fn=2)
                    pf = sproto_pack(ph + encode_sproto([(0, char_obj)], fn=1))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)
                    
                    time.sleep(0.5)
                    server_session_counter += 1
                    ph = encode_sproto([(0, 654), (1, server_session_counter)], fn=2)
                    pf = sproto_pack(ph + encode_sproto([(0, 1)], fn=1))
                    conn.sendall(struct.pack(">H", len(pf)) + pf)

            elif msg == 218: # heartbeat
                resp = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2); conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h + resp))) + sproto_pack(pkg_h + resp))

            elif msg in [121, 139, 141, 145, 202, 210]:
                pkg_h = encode_sproto([(1, session)], fn=2); conn.sendall(struct.pack(">H", len(sproto_pack(pkg_h))) + sproto_pack(pkg_h))

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(20)
print(f"GAME SERVER 9555 READY (PROTOCOL ALIGNED)")
while True: c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
