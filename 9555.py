import socket
import threading
import struct

HOST = "0.0.0.0"
PORT = 15678

def handle_client(conn, addr):
    print(f"\n[+] CONNECT {addr}")
    buffer = bytearray()

    try:
        while True:
            chunk = conn.recv(65535)
            if not chunk:
                break

            buffer.extend(chunk)

            while True:
                # Duhet minimumi 2 byte për length
                if len(buffer) < 2:
                    break

                # 2-byte big-endian length
                length = struct.unpack(">H", buffer[:2])[0]

                # Pritet i gjithë frame-i
                if len(buffer) < 2 + length:
                    break

                frame = bytes(buffer[:2 + length])
                del buffer[:2 + length]

                payload = frame[2:]

                print(f"\n[REQUEST] FRAME={len(frame)} PAYLOAD={len(payload)}")
                print("HEX :", frame.hex(" "))
                print("DATA:", repr(payload))

    except Exception as e:
        print(f"[ERROR] {addr}: {e}")

    finally:
        conn.close()
        print(f"[-] DISCONNECT {addr}")

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind((HOST, PORT))
server.listen(50)

print(f"[*] Listening on TCP {PORT}")

while True:
    conn, addr = server.accept()
    threading.Thread(
        target=handle_client,
        args=(conn, addr),
        daemon=True
    ).start()
