import socket
import struct
import threading


PORT = 9555


# ==========================
# SPROTO PACK
# ==========================

def sproto_pack(data):

    out = bytearray()

    for i in range(0, len(data), 8):

        chunk = data[i:i+8]

        mask = 0
        values = bytearray()

        for j, b in enumerate(chunk):

            if b != 0:
                mask |= (1 << j)
                values.append(b)

        out.append(mask)
        out.extend(values)

    return bytes(out)



def sproto_unpack(data):

    out = bytearray()
    i = 0

    while i < len(data):

        mask = data[i]
        i += 1

        for bit in range(8):

            if mask & (1 << bit):

                if i < len(data):
                    out.append(data[i])
                    i += 1

            else:

                out.append(0)

    return bytes(out)



# ==========================
# SPROTO HELPERS
# ==========================

def integer(value):

    return struct.pack(
        "<H",
        (value + 1) * 2
    )



def string_value(text):

    data = text.encode("utf-8")

    return (
        struct.pack("<I", len(data))
        +
        data
    )



# ==========================
# PACKAGE
# ==========================

def package(session):

    # Package response:
    # only session tag

    return (
        struct.pack("<H",2)
        +
        struct.pack("<H",0)
        +
        integer(session)
    )



# ==========================
# LOGIN RESPONSE
# ==========================

def login_response(session):


    body = bytearray()


    # login.response
    # fields:
    # 0 type
    # 1 versionCode
    # 2 dataVersionCode
    # 3 serverLevel

    body += struct.pack("<H",4)


    # type = 1
    body += integer(1)


    # versionCode
    body += struct.pack("<H",0)


    # dataVersionCode
    body += struct.pack("<H",0)


    # serverLevel = 1
    body += integer(1)



    body += string_value(
        "1.012.017"
    )

    body += string_value(
        "0"
    )


    raw = (
        package(session)
        +
        body
    )


    packed = sproto_pack(raw)


    return (
        struct.pack(">H",len(packed))
        +
        packed
    )



# ==========================
# CHARACTER LIST RESPONSE
# ==========================

def character_list_response(session):


    # empty character map

    body = bytearray()


    # response has one field
    # tag 0 map

    body += struct.pack("<H",1)

    # map is empty
    body += struct.pack("<H",0)


    raw = (
        package(session)
        +
        body
    )


    packed = sproto_pack(raw)


    return (
        struct.pack(">H",len(packed))
        +
        packed
    )



# ==========================
# CLIENT
# ==========================

def handle(conn,addr):

    print("[+] GAME CLIENT:",addr)


    try:

        while True:

            header = conn.recv(2)

            if not header:
                break


            size = struct.unpack(
                ">H",
                header
            )[0]


            data=b""


            while len(data)<size:

                data += conn.recv(
                    size-len(data)
                )


            unpacked = sproto_unpack(data)


            print(
                "RX:",
                unpacked.hex()
            )


            # login protocol 4

            if b"\x04\x00" in unpacked:

                print(
                    "LOGIN REQUEST"
                )

                conn.sendall(
                    login_response(1)
                )



            # character_list protocol 103

            elif b"\x67\x00" in unpacked:

                print(
                    "CHARACTER LIST REQUEST"
                )


                conn.sendall(
                    character_list_response(2)
                )


    except Exception as e:

        print(
            "ERROR:",
            e
        )


    finally:

        conn.close()

        print(
            "Disconnected"
        )



# ==========================
# SERVER
# ==========================

server = socket.socket(
    socket.AF_INET,
    socket.SOCK_STREAM
)


server.setsockopt(
    socket.SOL_SOCKET,
    socket.SO_REUSEADDR,
    1
)


server.bind(
    ("0.0.0.0",PORT)
)


server.listen(20)


print("================")
print("GAME SERVER 9555 ON")
print("================")


while True:

    c,a = server.accept()


    threading.Thread(
        target=handle,
        args=(c,a),
        daemon=True
    ).start()
