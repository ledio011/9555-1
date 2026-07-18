import socket
import struct
import threading


PORT = 9555


# -------------------------
# SPROTO PACK
# -------------------------

class SprotoPack:

    def pack(self, data):
        out = bytearray()

        for i in range(0, len(data), 8):

            chunk = data[i:i+8]

            mask = 0
            values = []

            for j, b in enumerate(chunk):

                if b != 0:
                    mask |= (1 << j)
                    values.append(b)

            out.append(mask)
            out.extend(values)

        return bytes(out)



    def unpack(self, data):

        out = bytearray()
        i = 0

        while i < len(data):

            mask = data[i]
            i += 1

            for bit in range(8):

                if (mask >> bit) & 1:
                    out.append(data[i])
                    i += 1
                else:
                    out.append(0)

                if i >= len(data):
                    break

        return bytes(out)



# -------------------------
# SIMPLE SPROTO ENCODER
# -------------------------

def write_integer(value, tag):

    # small integer encoding
    return struct.pack("<H", ((value + 1) * 2))



def write_string(value, tag):

    data = value.encode()

    header = struct.pack("<I", len(data))

    return header + data



def encode_struct(fields):

    header = bytearray()

    body = bytearray()


    count = len(fields)

    header.extend(struct.pack("<H", count))


    for tag, value in fields:

        if isinstance(value, int):

            header.extend(write_integer(value, tag))

        elif isinstance(value, str):

            header.extend(struct.pack("<H", 1))
            body.extend(write_string(value, tag))


    return bytes(header + header[2:] + body)



# -------------------------
# LOGIN RESPONSE
# -------------------------

def create_login_response(session):

    # Package header
    package = bytearray()

    # only session, no type in response
    package.extend(struct.pack("<H", 2))

    # tag 1 session
    package.extend(struct.pack("<H", 1))
    package.extend(struct.pack("<H", (session + 1) * 2))


    # login.response body

    body = bytearray()

    body.extend(struct.pack("<H", 4))


    # type = 1
    body.extend(struct.pack("<H", 0))
    body.extend(struct.pack("<H", 4))


    # versionCode
    version = b"1.012.017"

    body.extend(struct.pack("<H", 1))
    body.extend(struct.pack("<I", len(version)))
    body.extend(version)


    # dataVersionCode
    data_version = b"1"

    body.extend(struct.pack("<H", 1))
    body.extend(struct.pack("<I", len(data_version)))
    body.extend(data_version)


    # serverLevel
    body.extend(struct.pack("<H", 6))
    body.extend(struct.pack("<H", 4))


    raw = package + body


    packed = SprotoPack().pack(raw)


    final = struct.pack(">H", len(packed)) + packed

    return final



# -------------------------
# CLIENT
# -------------------------

def handle_client(conn, addr):

    print("[+] Client:", addr)


    try:

        while True:

            header = conn.recv(2)

            if not header:
                break


            length = struct.unpack(">H", header)[0]


            data = b""

            while len(data) < length:

                data += conn.recv(length-len(data))


            unpacked = SprotoPack().unpack(data)


            print("UNPACKED:", unpacked.hex())


            # login request detected
            if b"\x04\x00" in unpacked:

                print("LOGIN REQUEST")

                response = create_login_response(1)

                conn.send(response)

                print("LOGIN RESPONSE SENT")


    except Exception as e:

        print("ERROR:", e)


    finally:

        conn.close()



# -------------------------
# SERVER
# -------------------------

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

server.bind(("0.0.0.0", PORT))

server.listen(20)


print("9555 running")


while True:

    conn, addr = server.accept()

    threading.Thread(
        target=handle_client,
        args=(conn,addr)
    ).start()
