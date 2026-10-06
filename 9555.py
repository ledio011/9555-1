import socket
import threading
import struct
import time
import os
from datetime import datetime

HOST = "0.0.0.0"
PORT = int(os.getenv("PORT", "15678"))

LOG_FILE = "requests.log"
RAW_DIR = "raw_requests"

MAX_FRAME_SIZE = 1024 * 1024  # 1 MB
BACKLOG = 100


def now():
    return datetime.now().strftime("%Y-%m-%d %H:%M:%S.%f")[:-3]


def safe_ascii(data: bytes) -> str:
    return "".join(chr(b) if 32 <= b <= 126 else "." for b in data)


def hex_dump(data: bytes, width=16) -> str:
    lines = []

    for i in range(0, len(data), width):
        part = data[i:i + width]
        hex_part = " ".join(f"{b:02x}" for b in part)
        ascii_part = safe_ascii(part)

        lines.append(
            f"{i:04x}  {hex_part:<{width * 3 - 1}}  |{ascii_part}|"
        )

    return "\n".join(lines)


def log_text(text: str):
    print(text, flush=True)

    with open(LOG_FILE, "a", encoding="utf-8") as f:
        f.write(text + "\n")
        f.flush()


def save_raw(conn_id: str, frame_no: int, frame: bytes):
    os.makedirs(RAW_DIR, exist_ok=True)

    filename = os.path.join(
        RAW_DIR,
        f"{conn_id}_frame_{frame_no:06d}.bin"
    )

    with open(filename, "wb") as f:
        f.write(frame)


def parse_frames(buffer: bytearray):
    """
    Extract as many complete protocol frames as possible.

    Format currently observed:
        2-byte big-endian length
        followed by <length> payload bytes
    """

    frames = []

    while True:
        if len(buffer) < 2:
            break

        length = struct.unpack(">H", buffer[:2])[0]

        if length <= 0:
            # Avoid looping forever on invalid data.
            del buffer[:2]
            continue

        if length > MAX_FRAME_SIZE:
            raise ValueError(
                f"Invalid frame length: {length}"
            )

        total = 2 + length

        if len(buffer) < total:
            break

        frame = bytes(buffer[:total])
        del buffer[:total]

        frames.append(frame)

    return frames


def handle_client(conn: socket.socket, addr):
    client_ip, client_port = addr
    conn_id = f"{client_ip.replace('.', '_')}_{client_port}_{int(time.time() * 1000)}"

    buffer = bytearray()
    frame_no = 0
    total_bytes = 0
    total_frames = 0

    log_text(
        f"\n[{now()}] [CONNECT] "
        f"{client_ip}:{client_port} "
        f"ID={conn_id}"
    )

    conn.settimeout(None)
    conn.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)

    try:
        while True:
            chunk = conn.recv(65535)

            if not chunk:
                break

            total_bytes += len(chunk)
            buffer.extend(chunk)

            log_text(
                f"[{now()}] [TCP] "
                f"{client_ip}:{client_port} "
                f"received={len(chunk)} bytes "
                f"buffer={len(buffer)}"
            )

            try:
                frames = parse_frames(buffer)
            except Exception as e:
                log_text(
                    f"[{now()}] [FRAME ERROR] "
                    f"{client_ip}:{client_port}: {e}"
                )

                # Keep the connection alive so we can inspect
                # what follows instead of silently dying.
                continue

            for frame in frames:
                frame_no += 1
                total_frames += 1

                payload_length = struct.unpack(">H", frame[:2])[0]
                payload = frame[2:]

                log_text(
                    "\n"
                    + "=" * 80
                    + f"\n[{now()}] [REQUEST #{frame_no}] "
                    f"{client_ip}:{client_port}"
                    + f"\nFRAME_SIZE   = {len(frame)}"
                    + f"\nPAYLOAD_SIZE = {payload_length}"
                    + f"\nHEADER       = {frame[:2].hex(' ')}"
                    + f"\nPAYLOAD HEX  = {payload.hex(' ')}"
                    + f"\nPAYLOAD RAW  = {payload!r}"
                    + f"\nPAYLOAD ASCII= {safe_ascii(payload)}"
                    + "\n\nFULL FRAME:"
                    + "\n"
                    + hex_dump(frame)
                    + "\n"
                    + "=" * 80
                )

                save_raw(conn_id, frame_no, frame)

                # IMPORTANT:
                # No response is sent here.
                #
                # This script is intentionally a passive logger.
                # The client will therefore retry requests that expect
                # a server response.

    except ConnectionResetError:
        log_text(
            f"[{now()}] [RESET] "
            f"{client_ip}:{client_port}"
        )

    except Exception as e:
        log_text(
            f"[{now()}] [ERROR] "
            f"{client_ip}:{client_port}: {e}"
        )

    finally:
        if buffer:
            log_text(
                f"[{now()}] [LEFTOVER] "
                f"{client_ip}:{client_port} "
                f"{len(buffer)} bytes not yet forming a complete frame"
            )

            log_text(
                "LEFTOVER HEX: " + buffer.hex(" ")
            )

        try:
            conn.shutdown(socket.SHUT_RDWR)
        except Exception:
            pass

        try:
            conn.close()
        except Exception:
            pass

        log_text(
            f"[{now()}] [DISCONNECT] "
            f"{client_ip}:{client_port} "
            f"frames={total_frames} "
            f"bytes={total_bytes}"
        )


def main():
    print(f"[*] Starting TCP request logger")
    print(f"[*] Listening on {HOST}:{PORT}")
    print(f"[*] Log file: {os.path.abspath(LOG_FILE)}")
    print(f"[*] Raw frames: {os.path.abspath(RAW_DIR)}")

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    server.setsockopt(
        socket.SOL_SOCKET,
        socket.SO_REUSEADDR,
        1
    )

    server.setsockopt(
        socket.SOL_SOCKET,
        socket.SO_KEEPALIVE,
        1
    )

    server.bind((HOST, PORT))
    server.listen(BACKLOG)

    print("[*] READY")

    try:
        while True:
            conn, addr = server.accept()

            thread = threading.Thread(
                target=handle_client,
                args=(conn, addr),
                daemon=True
            )

            thread.start()

    except KeyboardInterrupt:
        print("\n[*] Stopping...")

    finally:
        server.close()


if __name__ == "__main__":
    main()
