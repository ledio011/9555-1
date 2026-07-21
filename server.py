import socket
import struct
import threading
import random

PORT = 9555

def sproto_pack(data):
    out = bytearray()
    n = len(data)
    for i in range(0, n, 8):
        chunk = data[i:i+8]
        if len(chunk) < 8:
            chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        values = bytearray()
        for j in range(8):
            if chunk[j] != 0:
                mask |= (1 << j)
                values.append(chunk[j])
        if mask == 0xFF:
            out.append(0xFF)
            out.append(0)
            out.extend(chunk)
        else:
            out.append(mask)
            out.extend(values)
    return bytes(out)

def sproto_unpack(data):
    out = bytearray()
    i = 0
    while i < len(data):
        mask = data[i]
        i += 1
        if mask == 0xFF:
            n = (data[i] + 1) * 8
            i += 1
            out.extend(data[i:i+n])
            i += n
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    out.append(data[i])
                    i += 1
                else:
                    out.append(0)
    return bytes(out)

def encode_sproto(fields, is_root=False):
    if not fields: return struct.pack("<H", 0) if is_root else b""
    fields.sort(key=lambda x: x[0])
    header = bytearray()
    body = bytearray()
    last_tag = -1
    for tag, value in fields:
        skip = tag - last_tag - 1
        if skip > 0: header += struct.pack("<H", (skip - 1) * 2 + 1)
        if value is None:
            header += struct.pack("<H", 0)
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
            for item in value:
                list_bin += struct.pack("<I", len(item)) + item
            body += struct.pack("<I", len(list_bin)) + list_bin
        last_tag = tag
    res = bytearray()
    if is_root: res += struct.pack("<H", len(header) // 2)
    res += header
    res += body
    return bytes(res)

def decode_header(data):
    if len(data) < 2: return None, None
    try:
        fn = struct.unpack("<H", data[:2])[0]
        header = data[2:2+fn*2]
        msg_type, session = None, None
        idx, curr_tag = 0, 0
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
    print(f"[+] Game Client connected: {addr}")
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size:
                part = conn.recv(size - len(data))
                if not part: break
                data += part

            raw = sproto_unpack(data)
            msg_type, session = decode_header(raw)
            if msg_type is None: continue

            if msg_type == 4: # Login Request
                resp = encode_sproto([
                    (0, 2),             # type (Success/Online)
                    (1, "1.012.017"),    # versionCode
                    (2, "0"),           # dataVersionCode
                    (3, 1)              # serverLevel
                ])
                pkg_h = encode_sproto([(1, session)], is_root=True)
                full_pkt = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full_pkt)) + full_pkt)
                print(f"[LOGIN] Player entered game. Session={session}")

            elif msg_type == 103: # Char List Request
                resp = encode_sproto([(0, [])])
                pkg_h = encode_sproto([(1, session)], is_root=True)
                full_pkt = sproto_pack(pkg_h + resp)
                conn.sendall(struct.pack(">H", len(full_pkt)) + full_pkt)

            elif msg_type == 218: # Heartbeat
                pkg_h = encode_sproto([(1, session)], is_root=True)
                full_pkt = sproto_pack(pkg_h)
                conn.sendall(struct.pack(">H", len(full_pkt)) + full_pkt)

    except Exception as e:
        print(f"Game Error: {e}")
    finally:
        conn.close()

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("0.0.0.0", PORT))
server.listen(10)
print(f"GAME SERVER 9555 ON")
while True:
    c, a = server.accept()
    threading.Thread(target=client_handler, args=(c, a), daemon=True).start()
