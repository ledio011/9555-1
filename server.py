import socket
import struct
import threading
import random
import json
import os
import time

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
        mask, values = 0, bytearray()
        for j in range(8):
            if chunk[j] != 0:
                mask |= (1 << j); values.append(chunk[j])
        if mask == 0xFF:
            out.extend([0xFF, 0]); out.extend(chunk)
        else:
            out.append(mask); out.extend(values)
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
                for item in val: list_bin += struct.pack("<I", len(item)) + item
                body += struct.pack("<I", len(list_bin)) + list_bin
        else: header[tag] = 1
    res = struct.pack("<H", fn)
    for h in header: res += struct.pack("<H", h)
    res += body
    return bytes(res)

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    fn = struct.unpack("<H", data[offset:offset+2])[0]
    h_ptr, b_ptr = offset + 2, offset + 2 + fn*2
    fields, curr_tag = {}, -1
    for i in range(fn):
        curr_tag += 1
        v = struct.unpack("<H", data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
        if v == 0:
            if b_ptr + 4 <= len(data):
                l = struct.unpack("<I", data[b_ptr:b_ptr+4])[0]
                fields[curr_tag] = data[b_ptr+4:b_ptr+4+l]
                b_ptr += 4 + l
        elif v == 1: pass
        elif v & 1: curr_tag += (v >> 1)
        else: fields[curr_tag] = (v >> 1) - 1
    return fields

def client_handler(conn, addr):
    print(f"[+] Game Connect: {addr}")
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

            if msg_type == 4: # Login
                # Tag 1 eshte ID, Tag 2 eshte Key
                acc_id = body.get(1, b"").decode('utf-8', 'ignore')
                print(f"[LOGIN] Player {acc_id}")
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "167"), (3, 1)], fn=4)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h+resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 103: # character_list
                if acc_id not in characters:
                    print(f"[AUTO] Creating character for {acc_id}")
                    name = f"Player_{acc_id[-4:]}"
                    gen = encode_sproto([(0, name), (1, 0), (2, 1), (3, "3001")], fn=4)
                    attr_ov = encode_sproto([(0, 100), (1, 100), (4, 1), (5, 100)], fn=6)
                    vis = encode_sproto([(0, name), (1, "100")], fn=2)
                    
                    try: char_id = int(acc_id[-9:])
                    except: char_id = random.randint(100000, 999999)
                    
                    char_ov = encode_sproto([(0, char_id), (1, gen), (2, attr_ov), (3, vis), (4, int(time.time()))], fn=6)
                    characters[acc_id] = list(char_ov)
                    save_chars(characters)
                    resp = encode_sproto([(0, [char_ov])], fn=1)
                else:
                    char_data = bytes(characters[acc_id])
                    resp = encode_sproto([(0, [char_data])], fn=1)
                
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 105: # character_pick
                resp = encode_sproto([(0, 1)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)
                
                map_req = encode_sproto([(0, "3001")], fn=1)
                pkg_req = encode_sproto([(0, 503)], fn=2)
                full_map = sproto_pack(pkg_req + map_req); conn.sendall(struct.pack(">H", len(full_map)) + full_map)

            elif msg_type == 100: # map_ready
                print(f"[SPAWN] {acc_id}")
                try: char_id = int(acc_id[-9:])
                except: char_id = 1001
                
                prop = encode_sproto([(13, 1000), (14, 1000), (15, 1000), (16, 0), (17, 0), (18, 0)], fn=19)
                attr = encode_sproto([(0, 1000), (1, 1000), (4, 1), (5, 100)], fn=6)
                gen = encode_sproto([(0, f"P_{acc_id[-4:]}"), (1, 0)], fn=3)
                pos = encode_sproto([(0, 1500), (1, 500), (2, 2000), (3, 0)], fn=4)
                mov = encode_sproto([(0, pos), (1, pos)], fn=2)
                char_data = encode_sproto([(0, char_id), (1, gen), (2, attr), (5, prop), (11, mov)], fn=12)
                main_req = encode_sproto([(0, char_data)], fn=1)
                pkg_req = encode_sproto([(0, 504)], fn=2)
                full_player = sproto_pack(pkg_req + main_req); conn.sendall(struct.pack(">H", len(full_player)) + full_player)

            elif msg_type == 218: # Heartbeat
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h); conn.sendall(struct.pack(">H", len(full)) + full)

    except Exception as e: print(f"Game Error: {e}")
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT))
server.listen(10)
print(f"GAME SERVER READY ON {PORT}")
while True:
    c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
