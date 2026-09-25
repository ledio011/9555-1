import os
import socket
import threading


PORT = int(os.environ.get("PORT", 15678))
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
DECOMPILED_ROOT = os.path.join(SCRIPT_DIR, "Decompiled")

ASSET_CACHE = {}
ASSET_CACHE_LOADED = False
ASSET_CACHE_LOCK = threading.Lock()


def read_game_assets(client_address):
    global ASSET_CACHE, ASSET_CACHE_LOADED

    if ASSET_CACHE_LOADED:
        return

    with ASSET_CACHE_LOCK:
        if ASSET_CACHE_LOADED:
            return

        if not os.path.isdir(DECOMPILED_ROOT):
            raise FileNotFoundError(f"Decompiled directory not found: {DECOMPILED_ROOT}")

        asset_paths = []
        for root, _, filenames in os.walk(DECOMPILED_ROOT):
            asset_paths.extend(os.path.join(root, filename) for filename in filenames)
        asset_paths.sort()

        if not asset_paths:
            raise RuntimeError(f"No game assets found in {DECOMPILED_ROOT}")

        total_bytes = sum(os.path.getsize(path) for path in asset_paths)
        print(f"[+] Client connected: {client_address}")
        print("Please wait reading game assets 0/100%", end="", flush=True)

        assets = {}
        bytes_read = 0
        last_percent = 0

        try:
            for path in asset_paths:
                relative_path = os.path.relpath(path, DECOMPILED_ROOT)
                contents = bytearray()

                with open(path, "rb") as asset_file:
                    while True:
                        chunk = asset_file.read(1024 * 1024)
                        if not chunk:
                            break

                        contents.extend(chunk)
                        bytes_read += len(chunk)

                        if total_bytes:
                            percent = min(100, bytes_read * 100 // total_bytes)
                            if percent > last_percent:
                                print(
                                    f"\rPlease wait reading game assets {percent}/100%",
                                    end="",
                                    flush=True,
                                )
                                last_percent = percent

                assets[relative_path] = bytes(contents)

            if bytes_read != total_bytes:
                raise RuntimeError(
                    f"Asset files changed while reading: expected {total_bytes} bytes, "
                    f"read {bytes_read}"
                )
        except Exception:
            print()
            raise

        ASSET_CACHE = assets
        ASSET_CACHE_LOADED = True
        print("\rPlease wait reading game assets 100/100%")
        print(f"[ASSETS] Cached {len(assets)} files ({bytes_read:,} bytes) in RAM.")


def handle_client(connection, address):
    try:
        read_game_assets(address)
    except Exception as exc:
        print(f"[!] Could not read game assets for {address}: {exc}")
    finally:
        connection.close()
        print(f"[-] Client disconnected: {address}")


def start_server():
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server:
        server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        server.bind(("0.0.0.0", PORT))
        server.listen()
        print(f"ASSET READER READY ON PORT {PORT}")

        while True:
            try:
                connection, address = server.accept()
                threading.Thread(
                    target=handle_client,
                    args=(connection, address),
                    daemon=True,
                ).start()
            except OSError as exc:
                print(f"[!] Accept failed: {exc}")


if __name__ == "__main__":
    start_server()
