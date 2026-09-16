import socket
import struct
import threading
import time
import traceback

HOST = "0.0.0.0"
PORT = 15678

LOG_FILE = "game.log"


def log(text):
    line = f"[{time.strftime('%H:%M:%S')}] {text}"
    print(line, flush=True)

    try:
        with open(LOG_FILE, "a", encoding="utf-8") as f:
            f.write(line + "\n")
    except Exception:
        pass


def recv_exact(conn, size):
    data = bytearray()

    while len(data) < size:
        chunk = conn.recv(size - len(data))

        if not chunk:
            return None

        data.extend(chunk)

    return bytes(data)


def unpack_sproto(data):
    """
    Sproto packed-data decoder.

    This only unpacks the transport compression.
    It does NOT invent or assume game fields.
    """

    output = bytearray()
    pos = 0

    while pos < len(data):
        mask = data[pos]
        pos += 1

        if mask == 0xFF:
            if pos >= len(data):
                break

            count = (data[pos] + 1) * 8
            pos += 1

            output.extend(data[pos:pos + count])
            pos += count

        else:
            for bit in range(8):

                if mask & (1 << bit):
                    if pos >= len(data):
                        break

                    output.append(data[pos])
                    pos += 1

                else:
                    output.append(0)

    return bytes(output)


def hexdump(data, maximum=512):
    if not data:
        return "(empty)"

    shown = data[:maximum]

    result = " ".join(f"{x:02X}" for x in shown)

    if len(data) > maximum:
        result += f" ... ({len(data)} bytes total)"

    return result


def inspect_sproto(data):
    """
    Read only the Sproto header enough to identify
    the RPC/message tag without assuming the rest.
    """

    if len(data) < 4:
        return None

    try:
        field_count = struct.unpack_from("<H", data, 0)[0]

        header_size = 2 + field_count * 2

        if header_size > len(data):
            return None

        fields = {}

        current_tag = -1
        body_pos = header_size

        for i in range(field_count):

            value = struct.unpack_from(
                "<H",
                data,
                2 + i * 2
            )[0]

            if value == 0:

                current_tag += 1

                if body_pos + 4 > len(data):
                    continue

                length = struct.unpack_from(
                    "<I",
                    data,
                    body_pos
                )[0]

                body_pos += 4

                fields[current_tag] = data[
                    body_pos:
                    body_pos + length
                ]

                body_pos += length

            elif value == 1:

                current_tag += 1

            elif value & 1:

                current_tag += (value >> 1) + 1

            else:

                current_tag += 1

                fields[current_tag] = (value >> 1) - 1

        return {
            "field_count": field_count,
            "fields": fields
        }

    except Exception:
        return None


def inspect_frame(frame):

    log("----------------------------------------")
    log(f"RAW PACKED SIZE: {len(frame)}")

    log("PACKED:")
    log(hexdump(frame))

    unpacked = unpack_sproto(frame)

    log(f"UNPACKED SIZE: {len(unpacked)}")

    log("UNPACKED:")
    log(hexdump(unpacked))

    info = inspect_sproto(unpacked)

    if not info:
        log("SPROTO: unable to parse header")
        return

    log(f"SPROTO FIELD COUNT: {info['field_count']}")

    for tag, value in info["fields"].items():

        if isinstance(value, int):

            log(
                f"SPROTO TAG {tag}: INT = {value}"
            )

        else:

            log(
                f"SPROTO TAG {tag}: "
                f"{len(value)} bytes"
            )

            if len(value) <= 64:
                log(
                    f"  DATA: {hexdump(value, 64)}"
                )


def send_raw(conn, data):

    packed = data

    packet = struct.pack(
        ">H",
        len(packed)
    ) + packed

    conn.sendall(packet)

    log(
        f"[TX] raw packet sent: "
        f"{len(packed)} bytes"
    )


def client_handler(conn, addr):

    log(f"[CONNECT] {addr}")

    conn.settimeout(30)

    frame_number = 0

    try:

        while True:

            header = recv_exact(conn, 2)

            if header is None:
                log(
                    f"[DISCONNECT] {addr}"
                )
                break

            frame_size = struct.unpack(
                ">H",
                header
            )[0]

            frame_number += 1

            log(
                f"[RX] FRAME #{frame_number} "
                f"SIZE={frame_size}"
            )

            if frame_size == 0:

                log(
                    "[RX] Empty frame"
                )

                continue

            frame = recv_exact(
                conn,
                frame_size
            )

            if frame is None:

                log(
                    "[DISCONNECT] "
                    "while receiving frame"
                )

                break

            inspect_frame(frame)

            log(
                f"[WAIT] Frame #{frame_number} "
                f"recorded. No fabricated response sent."
            )

    except socket.timeout:

        log(
            f"[TIMEOUT] {addr}"
        )

    except ConnectionResetError:

        log(
            f"[RESET] {addr}"
        )

    except Exception:

        log(
            f"[ERROR] {addr}"
        )

        traceback.print_exc()

    finally:

        try:
            conn.close()
        except Exception:
            pass

        log(
            f"[CLOSED] {addr}"
        )


def main():

    log("========================================")
    log("ATG 9555 TEST 1 SERVER")
    log("REAL GAME PORT: 15678")
    log("MODE: PROTOCOL DISCOVERY")
    log("NO FABRICATED GAME DATA")
    log("========================================")

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
        (HOST, PORT)
    )

    server.listen(20)

    log(
        f"[LISTEN] {HOST}:{PORT}"
    )

    while True:

        try:

            conn, addr = server.accept()

            thread = threading.Thread(
                target=client_handler,
                args=(conn, addr),
                daemon=True
            )

            thread.start()

        except KeyboardInterrupt:

            log("Server stopped")
            break

        except Exception:

            traceback.print_exc()


if __name__ == "__main__":
    main()
