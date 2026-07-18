import socket
import threading
import struct


PORT = 9555


def handle_client(conn, addr):
    print("Client:", addr)

    try:
        while True:

            header = conn.recv(2)

            if not header:
                break

            length = struct.unpack(">H", header)[0]

            data = conn.recv(length)

            print("Received:", data.hex())


            # KËTU do futen përgjigjet Sproto
            # për login 4
            # character_list 103
            # character_create 104
            # character_pick 105


    except Exception as e:
        print(e)

    finally:
        conn.close()



def start():

    server = socket.socket(
        socket.AF_INET,
        socket.SOCK_STREAM
    )

    server.bind(
        ("0.0.0.0", PORT)
    )

    server.listen(20)

    print("9555 running")

    while True:

        conn, addr = server.accept()

        threading.Thread(
            target=handle_client,
            args=(conn,addr)
        ).start()



start()