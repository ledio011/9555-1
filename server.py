import socket
import struct
import threading
import random


PORT = 9555


# ==========================
# SPROTO PACK / UNPACK
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
# SPROTO ENCODE
# ==========================

def int_encode(v):
    return struct.pack("<H", (v + 1) * 2)



def write_string(value):

    b = value.encode("utf-8")

    return (
        struct.pack("<I", len(b))
        +
        b
    )



def encode_object(fields):

    header = bytearray()
    body = bytearray()

    last = -1


    for tag,value in fields:

        skip = tag - last - 1


        if skip > 0:
            record = (skip - 1) * 2 + 1
            header += struct.pack("<H", record)



        if isinstance(value,int):

            header += int_encode(value)


        elif isinstance(value,str):

            header += struct.pack("<H",0)
            body += write_string(value)


        elif isinstance(value,bytes):

            header += struct.pack("<H",0)
            body += struct.pack("<I",len(value))
            body += value


        last = tag



    result = struct.pack(
        "<H",
        len(header)//2
    )

    result += header
    result += body


    return bytes(result)



# ==========================
# PACKAGE
# ==========================

def make_package(protocol,session=None):

    fields=[]

    fields.append(
        (0,protocol)
    )


    if session is not None:

        fields.append(
            (1,session)
        )


    return encode_object(fields)



def send_packet(sock,protocol,body=b"",session=None):

    raw = (
        make_package(protocol,session)
        +
        body
    )


    packed = sproto_pack(raw)


    packet = (
        struct.pack(">H",len(packed))
        +
        packed
    )


    sock.sendall(packet)



# ==========================
# LOGIN
# ==========================

def login_response():

    return encode_object([

        (0,1),
        (1,"1.012.017"),
        (2,"0"),
        (3,1)

    ])



# ==========================
# CHARACTER LIST
# ==========================

def character_list_response():

    return encode_object([])
# ==========================
# CHARACTER CREATE RESPONSE
# TAG 104
# ==========================

def character_create_response(player_id, name, profession):

    attribute = encode_object([
        (0,1)       # level
    ])


    general = encode_object([
        (0,name),
        (1,profession)
    ])


    character = encode_object([
        (0,player_id),
        (2,general),
        (3,attribute)
    ])


    return encode_object([
        (0,character),
        (1,0)       # errno success
    ])



# ==========================
# CHARACTER PICK RESPONSE
# TAG 105
# ==========================

def character_pick_response():

    # EMPTY RESPONSE
    # IMPORTANT: no errno field

    return encode_object([])



# ==========================
# ENTER MAP
# TAG 503
# SERVER -> CLIENT
# ==========================

def enter_map_packet():

    return encode_object([

        (0,"3001"),
        (1,1),
        (2,1)

    ])



# ==========================
# AOI ADD
# TAG 505
# ==========================

def aoi_add_packet(player_id,name,profession):


    position = encode_object([

        (0,0),
        (1,0),
        (2,0),
        (3,0)

    ])


    movement = encode_object([

        (0,position)

    ])



    general = encode_object([

        (0,name),
        (1,profession)

    ])



    visual = encode_object([

        (1,"1001"),
        (2,"0"),
        (3,"0"),
        (4,"0"),
        (5,"0")

    ])



    character = encode_object([

        (0,player_id),
        (1,visual),
        (2,general),
        (5,movement)

    ])


    return encode_object([

        (0,character)

    ])





# ==========================
# CLIENT HANDLER
# ==========================

def client(conn,addr):

    print("[+] CLIENT:",addr)


    player_id = random.randint(100000,999999)

    player_name = "Player"

    profession = 0


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



            raw=sproto_unpack(data)



            print(
                "RX:",
                raw.hex()
            )



            # LOGIN REQUEST
            if b"\x04\x00" in raw:


                print("LOGIN REQUEST")


                send_packet(
                    conn,
                    4,
                    login_response(),
                    1
                )



            # CHARACTER LIST
            elif b"\x67\x00" in raw:


                print("CHARACTER LIST")


                send_packet(
                    conn,
                    103,
                    character_list_response(),
                    2
                )



            # CHARACTER CREATE
            elif b"\x68\x00" in raw:


                print("CHARACTER CREATE")


                send_packet(
                    conn,
                    104,
                    character_create_response(
                        player_id,
                        player_name,
                        profession
                    ),
                    3
                )



            # CHARACTER PICK
            elif b"\x69\x00" in raw:


                print("CHARACTER PICK")


                send_packet(
                    conn,
                    105,
                    character_pick_response(),
                    4
                )


                # LOAD MAP

                send_packet(
                    conn,
                    503,
                    enter_map_packet()
                )


                # SPAWN PLAYER

                send_packet(
                    conn,
                    505,
                    aoi_add_packet(
                        player_id,
                        player_name,
                        profession
                    )
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
# SERVER START
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


server.listen(50)



print("======================")
print("GAME SERVER 9555 ON")
print("======================")



while True:

    conn,addr = server.accept()


    threading.Thread(
        target=client,
        args=(conn,addr),
        daemon=True
    ).start()
