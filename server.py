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

    return struct.pack("<I", len(b)) + b



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



    return (
        struct.pack("<H",len(header)//2)
        +
        header
        +
        body
    )



# ==========================
# SEND PACKET
# ==========================

def make_package(tag,session=None):

    fields=[
        (0,tag)
    ]

    if session is not None:
        fields.append(
            (1,session)
        )

    return encode_object(fields)



def send_packet(sock,tag,body=b"",session=None):

    raw = (
        make_package(tag,session)
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
# LOGIN RESPONSE 4
# ==========================

def login_response():

    return encode_object([

        (0,1),
        (1,"1.012.017"),
        (2,"0"),
        (3,1)

    ])



# ==========================
# CHARACTER LIST 103
# ==========================

def character_list_response():

    # EMPTY OBJECT
    return encode_object([])



# ==========================
# CHARACTER CREATE 104
# ==========================

def character_create_response(player_id,name,profession):


    general = encode_object([

        (0,name),
        (1,profession)

    ])


    character = encode_object([

        (0,player_id),
        (1,general),
        (2,1)

    ])


    return encode_object([

        (0,character),
        (1,0)

    ])



# ==========================
# CHARACTER PICK 105
# ==========================

def character_pick_response():

    return encode_object([])



# ==========================
# ENTER MAP 503
# ==========================

def enter_map_packet():

    return encode_object([

        (0,"3001")

    ])



# ==========================
# AOI ADD 505
# ==========================

def aoi_add_packet(player_id,name):


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
        (1,0)

    ])


    visual = encode_object([

        (1,"1001")

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
# CLIENT
# ==========================

def client(conn,addr):

    print("[+] CLIENT:",addr)


    player_id=random.randint(100000,999999)

    player_name="Player"

    profession=0



    try:

        while True:


            h=conn.recv(2)

            if not h:
                break


            size=struct.unpack(">H",h)[0]


            data=b""


            while len(data)<size:

                data += conn.recv(
                    size-len(data)
                )


            raw=sproto_unpack(data)


            print("RX:",raw.hex())



            # LOGIN 4

            if b"\x04\x00" in raw:

                print("LOGIN REQUEST")


                send_packet(
                    conn,
                    4,
                    login_response(),
                    1
                )



            # CHARACTER LIST 103

            elif b"\x67\x00" in raw:

                print("CHARACTER LIST")


                send_packet(
                    conn,
                    103,
                    character_list_response(),
                    2
                )



            # CHARACTER CREATE 104

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



            # CHARACTER PICK 105

            elif b"\x69\x00" in raw:

                print("CHARACTER PICK")


                send_packet(
                    conn,
                    105,
                    character_pick_response(),
                    4
                )


                send_packet(
                    conn,
                    503,
                    enter_map_packet()
                )


                send_packet(
                    conn,
                    505,
                    aoi_add_packet(
                        player_id,
                        player_name
                    )
                )



    except Exception as e:

        print("ERROR:",e)



    finally:

        conn.close()

        print("Disconnected")




# ==========================
# SERVER START
# ==========================

server=socket.socket(
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

    conn,addr=server.accept()


    threading.Thread(
        target=client,
        args=(conn,addr),
        daemon=True
    ).start()
