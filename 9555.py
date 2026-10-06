import socket
import struct
import threading
import time
import os

HOST = "0.0.0.0"
PORT = int(os.getenv("PORT", "15678"))

def hex_dump(data):
    return " ".join(f"{b:02X}" for b in data)

def ascii_dump(data):
    return "".join(chr(b) if 32 <= b <= 126 else "." for b in data)

def classify(payload):
    """
    Pa Sproto decoder/schema nuk mund të përcaktojmë
    me siguri TAG/REQUEST/RESPONSE/PUSH.
    """
    return {
        "TAG": "UNKNOWN",
        "TYPE": "UNKNOWN",
        "SESSION": "UNKNOWN",
        "REQUEST": "UNKNOWN",
        "RESPONSE": "UNKNOWN",
        "PUSH": "UNKNOWN",
    }

def handle_client(conn, addr):
    ip, port = addr
    print(f"\n[+] CONNECT {ip}:{port}")

    buffer = b""
    request_no = 0

    try:
        while True:
            chunk = conn.recv(65535)

            if not chunk:
                break

            buffer += chunk

            while len(buffer) >= 2:
                # 2-byte big-endian frame length
                payload_len = struct.unpack(">H", buffer[:2])[0]
                frame_len = payload_len + 2

                if len(buffer) < frame_len:
                    break

                frame = buffer[:frame_len]
                buffer = buffer[frame_len:]

                payload = frame[2:]
                request_no += 1

                info = classify(payload)

                print("\n" + "=" * 90)
                print(f"[FRAME #{request_no}] {ip}:{port}")
                print("=" * 90)

                print(f"FRAME_SIZE   : {len(frame)}")
                print(f"PAYLOAD_SIZE : {len(payload)}")

                print()
                print(f"TAG          : {info['TAG']}")
                print(f"TYPE         : {info['TYPE']}")
                print(f"SESSION      : {info['SESSION']}")
                print(f"REQUEST      : {info['REQUEST']}")
                print(f"RESPONSE     : {info['RESPONSE']}")
                print(f"PUSH         : {info['PUSH']}")

                print()
                print("PAYLOAD HEX  :", hex_dump(payload))
                print("PAYLOAD RAW  :", repr(payload))
                print("PAYLOAD ASCII:", ascii_dump(payload))

                print("=" * 90)

                # Save raw frame
                os.makedirs("raw_requests", exist_ok=True)

                filename = (
                    f"raw_requests/"
                    f"{ip.replace('.', '_')}_{port}_"
                    f"{int(time.time()*1000)}_{request_no}.bin"
                )

                with open(filename, "wb") as f:
                    f.write(frame)

    except Exception as e:
        print(f"[ERROR] {ip}:{port}: {e}")

    finally:
        conn.close()
        print(f"[-] DISCONNECT {ip}:{port}")


def main():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)

    server.bind((HOST, PORT))
    server.listen(50)

    print("[*] ATG Protocol Analyzer")
    print(f"[*] Listening on {HOST}:{PORT}")
    print("[*] Waiting for frames...")
    print()

    while True:
        conn, addr = server.accept()

        thread = threading.Thread(
            target=handle_client,
            args=(conn, addr),
            daemon=True
        )

        thread.start()


if __name__ == "__main__":
    main()
