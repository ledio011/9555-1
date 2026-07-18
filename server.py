import socket
import struct
import threading


PORT = 9555


# -------------------------
# SPROTO UNPACK
# -------------------------

class SprotoPack:

    def unpack(self, data):

        out = bytearray()
        i = 0

        while i < len(data):

            mask = data[i]
            i += 1

            if mask == 0xFF:

                if i >= len(data):
                    break

                count = (data[i] + 1) * 8
                i += 1

                out.extend(data[i:i+count])
                i += count

            else:

                for bit in range(8):

                    if (mask >> bit) & 1:

                        if i < len(data):
                            out.append(data[i])
                            i += 1

                    else:

                        out.append(0)

        return bytes(out)



# -------------------------
# SPROTO HEADER DECODER
# -------------------------

def read_word(data, pos):

    return data[pos] | (data[pos+1] << 8)



def decode_package(data):

    try:

        fn = read_word(data,0)

        cur = 2

        type_id = None
        session = None


        for tag in range(fn):

            word = read_word(data,cur)
            cur += 2


            if (word & 1) == 0:

                value = (word // 2) - 1


                if tag == 0:
                    type_id = value


                if tag == 1:
                    session = value


        return type_id, session


    except Exception as e:

        print("Decode error:", e)
        return None,None



# -------------------------
# SEND TEST (placeholder)
# -------------------------

def send_test(conn):

    pass



# -------------------------
# CLIENT HANDLER
# -------------------------

def handle_client(conn, addr):

    print("[+] Client connected:", addr)


    packer = SprotoPack()


    try:

        while True:


            header = conn.recv(2)


            if not header:
                break



            length = struct.unpack(">H", header)[0]


            packet = b""


            while len(packet) < length:

                part = conn.recv(length-len(packet))

                if not part:
                    break

                packet += part



            print("\nRAW:", packet.hex())


            unpacked = packer.unpack(packet)


            print("UNPACKED:", unpacked.hex())


            protocol, session = decode_package(unpacked)


            print("Protocol ID:", protocol)
            print("Session:", session)



            if protocol == 4:

                print("LOGIN REQUEST RECEIVED")


            elif protocol == 103:

                print("CHARACTER LIST REQUEST RECEIVED")


            elif protocol == 104:

                print("CHARACTER CREATE REQUEST RECEIVED")


            elif protocol == 105:

                print("CHARACTER PICK REQUEST RECEIVED")



    except Exception as e:

        print("Client error:", e)



    finally:

        conn.close()

        print("[-] Client disconnected")




# -------------------------
# SERVER START
# -------------------------

def start():

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
        ("0.0.0.0", PORT)
    )


    server.listen(20)


    print("==============================")
    print(" Auto Theft Gangsters Server ")
    print(" Port 9555 running")
    print("==============================")


    while True:


        conn, addr = server.accept()


        thread = threading.Thread(
            target=handle_client,
            args=(conn,addr)
        )


        thread.start()



if __name__ == "__main__":

    start()
