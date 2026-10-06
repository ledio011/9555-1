import socket
import struct
import threading
import os
import time

HOST = "0.0.0.0"
PORT = int(os.getenv("PORT", "15678"))

def decode_packet(payload):
    # Placeholder until the real Sproto schema/decoder is connected.
    return {
        "TAG": "?",
        "TYPE": "?",
        "NAME": "?",
        "SESSION": "?",
        "REQUEST": "?",
        "RESPONSE": "?",
        "PUSH": "?",
        "FIELDS": []
    }

def handle_client(conn, addr):
    ip, port = addr
    print(f"\n[+] CONNECT {ip}:{port}")

    buffer = b""
    frame_no = 0

    try:
        while True:
            data = conn.recv(65535)

            if not data:
                break

            buffer += data

            while len(buffer) >= 2:
                payload_size = struct.unpack(">H", buffer[:2])[0]
                frame_size = payload_size + 2

                if len(buffer) < frame_size:
                    break

                payload = buffer[2:frame_size]
                buffer = buffer[frame_size:]

                frame_no += 1
                info = decode_packet(payload)

                print()
                print("=" * 60)
                print(f"[FRAME #{frame_no}] {ip}:{port}")
                print("=" * 60)

                print(f"TAG       : {info['TAG']}")
                print(f"TYPE      : {info['TYPE']}")
                print(f"NAME      : {info['NAME']}")
                print(f"SESSION   : {info['SESSION']}")
                print(f"REQUEST   : {info['REQUEST']}")
                print(f"RESPONSE  : {info['RESPONSE']}")
                print(f"PUSH      : {info['PUSH']}")

                if info["FIELDS"]:
                    print("FIELDS:")
                    for key, value in info["FIELDS"]:
                        print(f"  {key} = {value}")
                else:
                    print("FIELDS    : ?")

                print("=" * 60)

    except Exception as e:
        print(f"[ERROR] {ip}:{port} -> {e}")

    finally:
        conn.close()
        print(f"[-] DISCONNECT {ip}:{port}")


def main():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

    server.bind((HOST, PORT))
    server.listen(50)

    print("[*] ATG Protocol Analyzer")
    print(f"[*] Listening on {HOST}:{PORT}")
    print("[*] Waiting for frames...")

    while True:
        conn, addr = server.accept()

        threading.Thread(
            target=handle_client,
            args=(conn, addr),
            daemon=True
        ).start()


if __name__ == "__main__":
    main()
