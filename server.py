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

def generate_random_name(gender_type):
    first_names = ["Shadow", "Storm", "Frost", "Iron", "Viper", "Ghost", "Blaze", "Neon", "Rogue", "Drake"]
    last_names = ["Wolf", "Hunter", "King", "Ghost", "Ninja", "Warrior", "Legend", "X", "Ace", "Blade"]
    return random.choice(first_names) + "_" + random.choice(last_names) + str(random.randint(100, 999))

def sproto_pack(data):
    out = bytearray()
    i = 0
    while i < len(data):
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
                if chunk[j] != 0: out.append(chunk[j])
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
                    else: # assuming string
                        s = str(item).encode('utf-8')
                        list_bin += struct.pack("<I", len(s)) + s
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
        print(f"[PUSH] Tag {tag} dërguar.")
    except: pass

def client_handler(conn, addr):
    print(f"[+] Lidhje: {addr}")
    acc_id = "0"
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

            elif msg_type == 100: # map_ready
                print(f"[MAP] Lojtari {acc_id} është gati në hartë.")

            elif msg_type == 103: # character_list
                if acc_id in characters:
                    char_ov = bytes(characters[acc_id]['ov'])
                    resp = encode_sproto([(0, [char_ov])], fn=1)
                else:
                    resp = encode_sproto([(0, [])], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 118: # random_name
                resp = encode_sproto([(0, generate_random_name(0))], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 104: # create
                gen_data = decode_sproto(body.get(0, b""))
                name = gen_data.get(0, b"").decode('utf-8')
                prof = gen_data.get(1, 0)
                try: char_id = int(acc_id[-9:])
                except: char_id = random.randint(1000000, 9999999)
                map_id = "101"
                gen_ov = encode_sproto([(0, name), (1, prof), (3, map_id)], fn=4)
                attr_ov = encode_sproto([(0, 1), (1, 100)], fn=2) 
                vis_ov = encode_sproto([(0, name), (1, "100")], fn=2)
                char_ov = encode_sproto([(0, char_id), (1, gen_ov), (2, attr_ov), (3, vis_ov), (4, int(time.time()))], fn=5)
                characters[acc_id] = {'id': char_id, 'ov': list(char_ov), 'name': name, 'prof': prof, 'map': map_id}
                save_chars(characters)
                resp = encode_sproto([(0, char_ov), (1, 0)], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 105: # pick
                char_info = characters.get(acc_id)
                if not char_info: continue
                map_id = char_info.get('map', '101')
                print(f"[PICK] Fillimi i sekuencës COMPLETE për {acc_id}")
                
                # 1. Përgjigjja Success
                resp = encode_sproto([(0, 1)], fn=1) 
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)
                
                # 2. Sync Common Data (614) - Kjo është kritike!
                # Tag: 0:time, 2:offset, 3:refresh, 4:pvp, 9:func, 11:guild, 12:seed, 13:level
                common_data = encode_sproto([
                    (0, int(time.time())), 
                    (2, 0), 
                    (3, 0), 
                    (4, 10000), 
                    (12, random.randint(1, 999999)), 
                    (13, 100) # Server Level
                ], fn=14)
                send_push(conn, 614, common_data)

                # 3. Sync State tjerë
                send_push(conn, 519, encode_sproto([(0, []), (1, ""), (2, [])], fn=3)) # Missions
                send_push(conn, 611, encode_sproto([(0, [])], fn=1)) # Items
                send_push(conn, 540, encode_sproto([(0, [])], fn=1)) # Skills
                send_push(conn, 592, encode_sproto([(0, [])], fn=1)) # Backpack
                
                # 4. Create Player (504)
                char_id, name, prof = char_info['id'], char_info['name'], char_info['prof']
                attr_data = encode_sproto([(0, 1000), (13, 500)], fn=14)
                runtime = encode_sproto([(6, attr_data), (7, attr_data)], fn=8)
                prop = encode_sproto([(13, 50000), (14, 50000), (15, 50000)], fn=19)
                attr_aoi = encode_sproto([(0, 1000), (1, 1000), (2, 1), (3, 100)], fn=4)
                gen = encode_sproto([(0, name), (1, prof), (4, 1)], fn=5)
                pos = encode_sproto([(0, 1500), (1, 500), (2, 2000), (3, 0)], fn=4)
                mov = encode_sproto([(0, pos), (1, pos)], fn=2)
                vis = encode_sproto([(0, name), (1, "100")], fn=2)
                char_data = encode_sproto([(0, char_id), (1, gen), (2, attr_aoi), (5, prop), (6, vis), (7, mov), (13, runtime), (15, 2)], fn=16)
                send_push(conn, 504, encode_sproto([(0, char_data)], fn=1))

                # 5. Enter Map (503)
                time.sleep(0.3)
                send_push(conn, 503, encode_sproto([(0, map_id), (1, 1), (2, 1)], fn=3))

            elif msg_type == 139: # request_update_storagepack (Inventari)
                print(f"[REQ] Update Storage Pack kërkuar.")
                resp = encode_sproto([(0, [])], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 218: # heartbeat
                resp_body = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp_body); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 268: # unlock_function_complete
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h); conn.sendall(struct.pack(">H", len(full)) + full)

    except Exception as e: 
        print(f"[ERROR] {e}")
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT))
server.listen(10)
print(f"GAME SERVER READY ON {PORT} (ULTRA SYNC)")
while True:
    c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
