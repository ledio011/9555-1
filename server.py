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
active_sessions = {} # Track spawning threads per connection

def get_visual_data(name, prof):
    defaults = {
        0: {"mode": "100", "head": "XD_A_T", "body": "XD_A_S", "leg": "XD_A_X", "weapon": "XD_A_WQ"},
        1: {"mode": "104", "head": "QJ_A_T", "body": "QJ_A_S", "leg": "QJ_A_X", "weapon": "QJ_A_WQ"},
        2: {"mode": "105", "head": "NQS_A_T", "body": "NQS_A_S", "leg": "NQS_A_X", "weapon": "NQS_A_WQ"}
    }
    d = defaults.get(prof, defaults[0])
    return encode_sproto([
        (0, name), (1, d["mode"]), (2, d["head"]), (3, d["body"]), 
        (4, d["leg"]), (5, d["weapon"]), (10, 0)
    ], fn=17)

def get_attribute_other(level=1, comb=55653, hp=10560):
    return encode_sproto([
        (0, hp), (1, 0), (2, level), (3, comb), (6, -1), (15, 1), (16, 0)
    ], fn=19)

def get_full_attributes(hp=10560):
    return encode_sproto([
        (0, hp), (1, 0), (2, 500), (3, 300), (4, 200), (5, 200), (13, 800)
    ], fn=25)

def generate_random_name():
    first = ["Viper", "Blaze", "Frost", "Iron", "Neon", "Shadow", "Drake", "Rogue"]
    last = ["Wolf", "Hunter", "King", "Blade", "Ace", "Warrior", "Ghost", "Ninja"]
    return f"{random.choice(first)}_{random.choice(last)}{random.randint(10, 99)}"

def sproto_pack(data):
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i+8]
        actual_len = len(chunk)
        if actual_len < 8: chunk += b'\x00' * (8 - actual_len)
        mask = 0
        for j in range(8):
            if chunk[j] != 0: mask |= (1 << j)
        
        if mask == 0xFF:
            out.append(0xFF); out.append(0); out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if chunk[j] != 0: out.append(chunk[j])
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
                    else: list_bin += struct.pack("<I", len(str(item))) + str(item).encode('utf-8')
                body += struct.pack("<I", len(list_bin)) + list_bin
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
    except: return {}

def send_push(conn, tag, data):
    try:
        pkg_h = encode_sproto([(0, tag)], fn=2)
        full = sproto_pack(pkg_h + data)
        conn.sendall(struct.pack(">H", len(full)) + full)
    except: pass

def spawn_burst_thread(conn, char_info):
    """Repeatedly push player data until map_ready is received."""
    conn_id = id(conn)
    print(f"[BURST] Starting spawn burst for {char_info['name']}")
    
    name, prof, char_id = char_info['name'], char_info['prof'], char_info['id']
    attr_other = get_attribute_other(1, 55653, 10560)
    attr_full = get_full_attributes(10560)
    runtime = encode_sproto([(6, attr_full), (7, attr_full)], fn=8)
    prop = encode_sproto([(13, 10000), (14, 10000), (15, 10000)], fn=19)
    attr_aoi = encode_sproto([(0, 10560), (1, 1000), (2, 10), (3, 500)], fn=4)
    gen = encode_sproto([(0, name), (1, prof), (3, "101"), (4, 1)], fn=5)
    pos = encode_sproto([(0, 1500), (1, 500), (2, 2000), (3, 0)], fn=4)
    mov = encode_sproto([(0, pos), (1, pos)], fn=2)
    vis = get_visual_data(name, prof)
    char_data = encode_sproto([(0, char_id), (1, gen), (2, attr_other), (5, prop), (6, vis), (7, mov), (13, runtime), (15, 2)], fn=17)
    
    # Pre-pack the player create packet
    pkg_h = encode_sproto([(0, 504)], fn=2)
    player_packet = sproto_pack(pkg_h + encode_sproto([(0, char_data)], fn=1))
    full_player = struct.pack(">H", len(player_packet)) + player_packet

    while active_sessions.get(conn_id) == "BURST":
        try:
            conn.sendall(full_player)
            time.sleep(1.5)
        except: break
    print(f"[BURST] Stopped for {name}")

def client_handler(conn, addr):
    print(f"[+] Lidhje: {addr}")
    acc_id = "0"
    conn_id = id(conn)
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size:
                chunk = conn.recv(size - len(data))
                if not chunk: break
                data += chunk
            raw = sproto_unpack(data)
            pkg = decode_sproto(raw, 0)
            msg_type, session = pkg.get(0), pkg.get(1)
            body_off = 2 + (struct.unpack("<H", raw[:2])[0] * 2)
            body = decode_sproto(raw, body_off)

            if msg_type == 4: # login
                acc_id = body.get(1, b"").decode('utf-8', 'ignore')
                print(f"[LOGIN] {acc_id}")
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "602"), (3, 1)], fn=4)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 118: # random_name
                name = generate_random_name()
                print(f"[NAME] {name}")
                resp = encode_sproto([(0, name)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

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
                gen_data = decode_sproto(body.get(0, b""))
                name = gen_data.get(0, b"").decode('utf-8'); prof = gen_data.get(1, 0)
                char_id = random.randint(1000000, 9999999)
                characters[acc_id] = {'id': char_id, 'name': name, 'prof': prof, 'map': "101"}
                save_chars(characters)
                gen_ov = encode_sproto([(0, name), (1, prof), (3, "101")], fn=4)
                vis_ov = get_visual_data(name, prof)
                char_ov = encode_sproto([(0, char_id), (1, gen_ov), (2, encode_sproto([(0, 1), (1, 55653)], fn=2)), (3, vis_ov), (4, int(time.time()))], fn=6)
                resp = encode_sproto([(0, char_ov), (1, 0)], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 105: # pick
                char_info = characters.get(acc_id)
                if not char_info: continue
                # 1. Success
                resp = encode_sproto([(0, 3)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)
                
                # 2. Pre-sync state
                send_push(conn, 614, encode_sproto([(0, int(time.time())), (12, 12345), (13, 1)], fn=14))
                send_push(conn, 538, encode_sproto([(0, [])], fn=1)) # Friends
                send_push(conn, 541, encode_sproto([(0, 55653), (2, 9999), (3, 9999)], fn=8)) # Rank
                m1 = encode_sproto([(0, "10001"), (1, 1), (3, [0, 2])], fn=4)
                send_push(conn, 519, encode_sproto([(0, [m1]), (1, "10001")], fn=3))
                
                # 3. Trigger Map Entry
                send_push(conn, 503, encode_sproto([(0, "101"), (1, 1), (2, 1)], fn=3))
                
                # 4. Start Burst Spawning thread
                active_sessions[conn_id] = "BURST"
                threading.Thread(target=spawn_burst_thread, args=(conn, char_info), daemon=True).start()

            elif msg_type == 100: # map_ready
                print(f"[READY] Map is active. Stopping burst.")
                active_sessions[conn_id] = "ACTIVE"
                time.sleep(0.5)
                send_push(conn, 654, encode_sproto([(0, 1)], fn=1))

            elif msg_type in [121, 139, 145, 191, 202, 210, 225, 242, 252, 253, 258, 261]:
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 218: # heartbeat
                resp_body = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2); full = sproto_pack(pkg_h + resp_body)
                conn.sendall(struct.pack(">H", len(full)) + full)

    except: pass
    finally: 
        active_sessions.pop(conn_id, None)
        conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM); server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT)); server.listen(10)
print(f"GAME SERVER READY ON {PORT} (BRUTE FORCE SYNC)")
while True: c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
