import socket
import struct
import threading
import time
import os

HOST = "0.0.0.0"
PORT = int(os.getenv("PORT", "15678"))

# Plotësohet automatikisht me emrat që identifikohen nga protokolli.
REQUEST_NAMES = {
    2: "visitor",
    3: "verfiy",
    4: "login",
    7: "update_game_server",
    100: "map_ready",
    103: "character_list",
    104: "character_create",
    105: "character_pick",
    118: "random_name",
    218: "heartbeat",
    503: "enter_map",
    504: "main_player_create",
    505: "aoi_add",
    614: "sync_common_data",
    654: "start_enter_game",
}

def show_request(number, payload):
    print("\n" + "=" * 80)
    print(f"REQUEST #{number}")
    print("=" * 80)

    print(f"FRAME SIZE   : {len(payload) + 2}")
    print(f"PAYLOAD SIZE : {len(payload)}")

    # Deri sa paketa të dekodohet Sproto,
    # ruajmë çdo byte të paketës pa humbur asgjë.
    print(f"PAYLOAD      : {payload.hex(' ')}")

    print("ASCII        :", "".join(
        chr(x) if 32 <= x <= 126 else "."
        for x in payload
    ))

    print("=" * 80)


def client(conn, addr):
    ip, port = addr
    print(f"\n[+] CLIENT CONNECTED: {ip}:{port}")

    buffer = b""
    request_number = 0

    try:
        while True:
            data = conn.recv(65535)

            if not data:
                break

            buffer += data

            while len(buffer) >= 2:
                payload_len = struct.unpack(">H", buffer[:2])[0]
                frame_len = payload_len + 2

                if len(buffer) < frame_len:
                    break

                payload = buffer[2:frame_len]
                buffer = buffer[frame_len:]

                request_number += 1

                show_request(request_number, payload)

    except Exception as e:
        print(f"[ERROR] {ip}:{port}: {e}")

    finally:
        conn.close()
        print(f"[-] CLIENT DISCONNECTED: {ip}:{port}")


def main():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

    server.bind((HOST, PORT))
    server.listen(100)

    print("[*] ATG CLIENT REQUEST LOGGER")
    print(f"[*] Listening: {HOST}:{PORT}")
    print("[*] Capturing EVERY client → server frame")
    print("[*] No server responses are generated.")
    print()

    while True:
        conn, addr = server.accept()

        threading.Thread(
            target=client,
            args=(conn, addr),
            daemon=True
        ).start()


if __name__ == "__main__":
    main()
