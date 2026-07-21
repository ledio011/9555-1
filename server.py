import socket
import struct
import threading
import random
import os

PORT = int(os.environ.get("PORT", 9555))
# Emra nderkombetare
NAMES = ["Alex", "John", "Smith", "Lucas", "Elena", "Sofia", "Marcus", "Oliver", "Maya", "Victor"]

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

def encode_sproto(fields):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    header = bytearray()
    body = bytearray()
    last_tag = -1
    for tag, value in fields:
        skip = tag - last_tag - 1
        if skip > 0: header += struct.pack("<H", (skip - 1) * 2 + 1)
        if value is None: header += struct.pack("<H", 0)
        elif isinstance(value, int):
            if 0 <= value <= 32766: header += struct.pack("<H", (value + 1) * 2)
            else:
                header += struct.pack("<H", 0)
                body += struct.pack("<I", 8) + struct.pack("<q", value)
        elif isinstance(value, (str, bytes, bytearray)):
            if isinstance(value, str): value = value.encode('utf-8')
            header += struct.pack("<H", 0)
            body += struct.pack("<I", len(value)) + value
        elif isinstance(value, list):
            header += struct.pack("<H", 0)
            list_bin = bytearray()
            for item in value: list_bin += struct.pack("<I", len(item)) + item
            body += struct.pack("<I", len(list_bin)) + list_bin
        last_tag = tag
    return struct.pack("<H", len(header) // 2) + header + body

def decode_header(data):
    if len(data) < 2: return None, None
    try:
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
    except: return None, None

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
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "0"), (3, 1)])
                pkg_h = encode_sproto([(1, session)])
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 118: # Random Name Fix
                name = random.choice(NAMES) + str(random.randint(100, 999))
                resp = encode_sproto([(0, name)])
                pkg_h = encode_sproto([(1, session)])
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 103: # Char List
                resp = encode_sproto([(0, [])])
                pkg_h = encode_sproto([(1, session)])
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 104: # Create Char
                gen = encode_sproto([(0, "Hero"), (1, 0), (3, "3001")])
                char_ov = encode_sproto([(0, 1001), (1, gen), (5, 0)])
                resp = encode_sproto([(0, char_ov), (1, 0)])
                pkg_h = encode_sproto([(1, session)])
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 105: # Pick Char
                resp = encode_sproto([(0, 1)])
                pkg_h = encode_sproto([(1, session)])
                full = sproto_pack(pkg_h + resp); conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg_type == 100: # Map Ready (Tag 100) -> Dërgon lojtarin ne skenë
                visual = encode_sproto([(1,"1001"),(2,"1001"),(3,"1001"),(4,"1001")])
                attr = encode_sproto([(0,1000),(2,1)])
                gen = encode_sproto([(0,"Player"),(1,0),(3,"3001")])
                char_obj = encode_sproto([(0, 1001),(1,gen),(2,attr),(6,visual)])
                main_pkt = sproto_pack(encode_sproto([(0, 504)]) + encode_sproto([(0, char_obj)]))
                conn.sendall(struct.pack(">H", len(main_pkt)) + main_pkt)

            elif msg_type == 218: # Heartbeat
                pkg_h = encode_sproto([(1, session)])
                full = sproto_pack(pkg_h); conn.sendall(struct.pack(">H", len(full)) + full)

    except Exception as e: print(f"Game Error: {e}")
    finally: conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT))
server.listen(10)
print(f"GAME SERVER ACTIVE ON PORT {PORT}")
while True:
    c, a = server.accept(); threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
