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
server_session = 1000 # Server initiated sessions start from 1000

def sproto_pack(data):
    out = bytearray()
    i = 0
    while i < len(data):
        chunk = data[i:i+8]
        mask = 0
        actual_chunk = bytearray(8)
        for j in range(len(chunk)):
            if chunk[j] != 0:
                mask |= (1 << j)
                actual_chunk[j] = chunk[j]
        
        if mask == 0xFF:
            out.append(0xFF)
            out.append(0)
            out.extend(actual_chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j):
                    out.append(actual_chunk[j])
        i += 8
    return bytes(out)

def sproto_unpack(data):
    out = bytearray()
    i = 0
    while i < len(data):
        mask = data[i]; i += 1
        if mask == 0xFF:
            if i >= len(data): break
            n = (data[i] + 1) * 8; i += 1
            out.extend(data[i:i+n]); i += n
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < len(data): out.append(data[i]); i += 1
                else: out.append(0)
    return bytes(out)

def encode_sproto(fields, fn=None):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    if fn is None: fn = fields[-1][0] + 1
    header = [0] * fn
    body = bytearray()
    field_dict = {f[0]: f[1] for f in fields}
    for tag in range(fn):
        if tag in field_dict:
            val = field_dict[tag]
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
                    else:
                        s = str(item).encode('utf-8')
                        list_bin += struct.pack("<I", len(s)) + s
                body += struct.pack("<I", len(list_bin)) + list_bin
            elif isinstance(val, dict):
                header[tag] = 0
                dict_bin = bytearray()
                for k, v in val.items():
                    obj = v if isinstance(v, (bytes, bytearray)) else str(v).encode('utf-8')
                    dict_bin += struct.pack("<I", len(obj)) + obj
                body += struct.pack("<I", len(dict_bin)) + dict_bin
        else: header[tag] = 1
    res = struct.pack("<H", fn)
    for h in header: res += struct.pack("<H", h)
    res += body
    return bytes(res)

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    try:
        fn = struct.unpack("<H", data[offset:offset+2])[0]
        h_ptr, b_ptr = offset + 2, offset + 2 + fn*2
        fields, curr_tag = {}, -1
        for i in range(fn):
            curr_tag += 1
            if h_ptr + i*2 + 2 > len(data): break
            v = struct.unpack("<H", data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
            if v == 0:
                if b_ptr + 4 <= len(data):
                    l = struct.unpack("<I", data[b_ptr:b_ptr+4])[0]
                    raw_val = data[b_ptr+4:b_ptr+4+l]
                    if l == 4: fields[curr_tag] = struct.unpack("<I", raw_val)[0]
                    elif l == 8: fields[curr_tag] = struct.unpack("<q", raw_val)[0]
                    else: fields[curr_tag] = raw_val
                    b_ptr += 4 + l
            elif v == 1: pass
            elif v & 1: curr_tag += (v >> 1)
            else: fields[curr_tag] = (v >> 1) - 1
        return fields
    except: return {}

def send_push_rpc(conn, tag, data):
    global server_session
    try:
        server_session += 1
        pkg_h = encode_sproto([(0, tag), (1, server_session)], fn=2)
        full = sproto_pack(pkg_h + data)
        conn.sendall(struct.pack(">H", len(full)) + full)
        print(f"[PUSH-RPC] Tag {tag} (Session: {server_session})")
    except: pass

def get_visual_data(name, prof):
    defaults = {
        0: {"mode": "100", "head": "XD_A_T", "body": "XD_A_S", "leg": "XD_A_X", "weapon": "XD_A_WQ"},
        1: {"mode": "104", "head": "QJ_A_T", "body": "QJ_A_S", "leg": "QJ_A_X", "weapon": "QJ_A_WQ"},
        2: {"mode": "105", "head": "NQS_A_T", "body": "NQS_A_S", "leg": "NQS_A_X", "weapon": "NQS_A_WQ"}
    }
    d = defaults.get(prof, defaults[0])
    return encode_sproto([(0, name), (1, d["mode"]), (2, d["head"]), (3, d["body"]), (4, d["leg"]), (5, d["weapon"]), (10, 0)], fn=17)

def get_full_attributes():
    return encode_sproto([(0, 10560), (1, 0), (2, 1), (3, 55653), (4, 1), (5, 0), (13, 800)], fn=25)

def client_handler(conn, addr):
    print(f"[+] Connection: {addr}")
    acc_id = "0"
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size: data += conn.recv(size - len(data))
            raw = sproto_unpack(data)
            pkg = decode_sproto(raw, 0)
            msg_type, session = pkg.get(0), pkg.get(1)
            body_off = 2 + (struct.unpack("<H", raw[:2])[0] * 2)
            body = decode_sproto(raw, body_off)

            if msg_type == 4: # login
                acc_id = body.get(1, b"").decode('utf-8', 'ignore') if isinstance(body.get(1), bytes) else str(body.get(1))
                print(f"[LOGIN] {acc_id}")
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "1000"), (3, 1)], fn=4)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 118: # random_name
                name = f"Viper_{random.randint(100, 999)}"
                print(f"[NAME] {name} (Session: {session})")
                resp = encode_sproto([(0, name)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 103: # character_list
                if acc_id in characters:
                    c = characters[acc_id]
                    gen_ov = encode_sproto([(0, c['name']), (1, c['prof']), (3, "101")], fn=4)
                    attr_ov = encode_sproto([(0, 1), (1, 55653)], fn=2)
                    vis_ov = get_visual_data(c['name'], c['prof'])
                    char_ov = encode_sproto([(0, c['id']), (1, gen_ov), (2, attr_ov), (3, vis_ov), (4, int(time.time()))], fn=6)
                    resp = encode_sproto([(0, [char_ov])], fn=1)
                else: resp = encode_sproto([(0, [])], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 104: # create
                gen_data = decode_sproto(body.get(0, b"")) if isinstance(body.get(0), bytes) else {}
                name = gen_data.get(0, b"").decode('utf-8') if isinstance(gen_data.get(0), bytes) else str(gen_data.get(0))
                prof = gen_data.get(1, 0)
                char_id = random.randint(1000000, 9999999)
                characters[acc_id] = {'id': char_id, 'name': name, 'prof': prof, 'map': "101"}
                save_chars(characters)
                resp = encode_sproto([(0, char_id), (1, 0)], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 105: # pick
                print(f"[PICK] {acc_id} (Session: {session})")
                resp = encode_sproto([(0, 1)], fn=1) # errno 1 = Success?
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)
                
                # Use RPC headers for Pushes that expect responses
                send_push_rpc(conn, 614, encode_sproto([(0, int(time.time())), (12, 12345), (13, 1)], fn=14))
                send_push_rpc(conn, 538, encode_sproto([(0, [])], fn=1)) 
                send_push_rpc(conn, 541, encode_sproto([(0, 55653), (2, 9999), (3, 9999)], fn=8))
                send_push_rpc(conn, 519, encode_sproto([(0, []), (1, ""), (2, [])], fn=3))
                time.sleep(0.2)
                send_push_rpc(conn, 503, encode_sproto([(0, "101"), (1, 1), (2, 1)], fn=3))

            elif msg_type == 100: # map_ready
                print(f"[READY] {acc_id}")
                char_info = characters.get(acc_id)
                if char_info:
                    name, prof, char_id = char_info['name'], char_info['prof'], char_info['id']
                    attr_other = encode_sproto([(0, 10560), (2, 1), (3, 55653), (15, 1)], fn=19)
                    attr_full = get_full_attributes()
                    runtime = encode_sproto([(6, attr_full), (7, attr_full)], fn=8)
                    prop = encode_sproto([(13, 1000), (14, 1000), (15, 1000)], fn=19)
                    gen = encode_sproto([(0, name), (1, prof), (3, "101"), (4, 1)], fn=5)
                    pos = encode_sproto([(0, 1500), (1, 500), (2, 2000), (3, 0)], fn=4)
                    mov = encode_sproto([(0, pos), (1, pos)], fn=2)
                    vis = get_visual_data(name, prof)
                    char_data = encode_sproto([(0, char_id), (1, gen), (2, attr_other), (5, prop), (6, vis), (7, mov), (13, runtime), (15, 2)], fn=17)
                    send_push_rpc(conn, 504, encode_sproto([(0, char_data)], fn=1))
                    time.sleep(0.5)
                    send_push_rpc(conn, 654, encode_sproto([(0, 1)], fn=1))

            elif msg_type == 218: # heartbeat
                resp_body = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp_body)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type is None and session is not None:
                # This is a RESPONSE from the client to one of our PUSH-RPCs
                # print(f"RES from client for session {session}")
                pass

    except: traceback.print_exc()
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(10)
print(f"GAME SERVER READY ON {PORT} (X-RAY FIX v3 - RPC HEADERS)")
while True: c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
