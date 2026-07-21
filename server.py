import socket
import struct
import threading
import random
import os

PORT = int(os.environ.get("PORT", 9555))
NAMES = ["Alex", "John", "Liam", "Lucas", "Elena", "Sofia", "Marcus", "Oliver", "Maya", "Victor", "Hiroshi", "Kaito"]

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

def decode_header(data):
    if len(data) < 2: return None, None
    fn = struct.unpack("<H", data[:2])[0]
    header = data[2:2+fn*2]
    msg_type, session, idx, curr_tag = None, None, 0, 0
    while idx < len(header):
        val = struct.unpack("<H", header[idx:idx+2])[0]
        if val & 1: curr_tag += (val >> 1) + 1
        else:
            real_val = (val >> 1) - 1
            if curr_tag == 0: msg_type = real_val
            if curr_tag == 1: session = real_val
            curr_tag += 1
        idx += 2
    return msg_type, session

def client_handler(conn, addr):
    print(f"[+] Game Client: {addr}")
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size: data += conn.recv(size - len(data))
            raw = sproto_unpack(data)
            msg_type, session = decode_header(raw)
            if msg_type is None: continue
            print(f"[GAME RX] Tag: {msg_type}")

            if msg_type == 4: # Login
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "0"), (3, 1)], fn=4)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 118: # Random Name Fix
                name = random.choice(NAMES) + str(random.randint(100, 999))
                resp = encode_sproto([(0, name)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 103: # Char List
                resp = encode_sproto([(0, [])], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 104: # Create
                gen = encode_sproto([(0, "Hero"), (1, 0), (3, "3001")], fn=4)
                char_ov = encode_sproto([(0, 1001), (1, gen), (5, 0)], fn=6)
                resp = encode_sproto([(0, char_ov), (1, 0)], fn=2)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 105: # Pick
                resp = encode_sproto([(0, 1)], fn=1)
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)
                # Dërgojmë Enter Map (Tag 503)
                map_req = encode_sproto([(0, "3001")], fn=1)
                pkg_req = encode_sproto([(0, 503)], fn=2)
                full_map = sproto_pack(pkg_req + map_req); conn.sendall(struct.pack(">H", len(full_map)) + full_map)

            elif msg_type == 218: # Heartbeat
                pkg_h = encode_sproto([(1, session)], fn=2)
                full = sproto_pack(pkg_h); conn.sendall(struct.pack(">H", len(full)) + full)

    except Exception as e: print(f"Game Error: {e}")
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT))
server.listen(10)
print(f"GAME SERVER ON PORT {PORT}")
while True:
    c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
