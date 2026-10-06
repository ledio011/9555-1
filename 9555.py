import socket
import threading

HOST = "0.0.0.0"
PORT = 15678

def client(conn, addr):
    print(f"\n[+] CONNECT {addr}")

    try:
        while True:
            data = conn.recv(65535)
            if not data:
                break

            print(f"\n[REQUEST] {len(data)} bytes")
            print("HEX:", data.hex(" "))
            print("RAW:", repr(data))

    except Exception as e:
        print(f"[ERROR] {addr}: {e}")

    finally:
        conn.close()
        print(f"[-] DISCONNECT {addr}")

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind((HOST, PORT))
server.listen()

print(f"[*] Listening on {HOST}:{PORT}")

while True:
    conn, addr = server.accept()
    threading.Thread(target=client, args=(conn, addr), daemon=True).start()
