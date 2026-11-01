#!/usr/bin/env python3
"""
One-time ATG grant: add 100 standard healing potions (ItemData ID 9001)
to every saved character in characters_final.json.

Run from this repository with the 9555 game server and its watchdog stopped:
    python3 grant_medkits.py --confirm
"""

import importlib.util
import json
import os
import socket
import sys
import time
from pathlib import Path

ROOT = Path(__file__).resolve().parent
SERVER_FILE = ROOT / "9555.py"
ITEM_ID = "9001"
AMOUNT = 100
DEFAULT_PORT = 15678
MARKER = ROOT / ".grant_medkits_9001_100_all.done"
LOCK = ROOT / ".grant_medkits_9001_100_all.lock"


def game_port_is_open(port):
    try:
        with socket.create_connection(("127.0.0.1", port), timeout=0.5):
            return True
    except OSError:
        return False


def write_json_atomic(path, data):
    temp_path = path.with_name(path.name + ".tmp")
    with temp_path.open("w", encoding="utf-8") as handle:
        json.dump(data, handle, indent=2, ensure_ascii=False)
        handle.flush()
        os.fsync(handle.fileno())
    os.replace(temp_path, path)


def main():
    if "--confirm" not in sys.argv[1:]:
        print("No changes made. Review this operation, then run:")
        print("  python3 grant_medkits.py --confirm")
        print("This grants +100 of ItemData 9001 to every saved character.")
        return 2

    if not SERVER_FILE.is_file():
        print(f"ERROR: server source not found: {SERVER_FILE}")
        return 1

    if MARKER.exists():
        print(f"ERROR: this one-time grant was already recorded: {MARKER.name}")
        print("No items were added. Inspect the marker and character database before any manual retry.")
        return 1

    try:
        lock_fd = os.open(str(LOCK), os.O_CREAT | os.O_EXCL | os.O_WRONLY, 0o600)
    except FileExistsError:
        print(f"ERROR: grant lock already exists: {LOCK.name}")
        print("Check for another running grant process before removing the lock.")
        return 1

    try:
        os.write(lock_fd, f"pid={os.getpid()}\n".encode("utf-8"))
        os.close(lock_fd)

        port = int(os.environ.get("PORT", DEFAULT_PORT))
        if game_port_is_open(port):
            print(f"ERROR: port {port} is accepting connections.")
            print("Stop the 9555 server and its watchdog before changing the saved database.")
            return 1

        spec = importlib.util.spec_from_file_location("atg_9555_grant_module", SERVER_FILE)
        if spec is None or spec.loader is None:
            print("ERROR: could not load 9555.py")
            return 1

        server = importlib.util.module_from_spec(spec)
        sys.modules[spec.name] = server
        spec.loader.exec_module(server)

        db_path = Path(server.CHAR_DB)
        if not db_path.is_file() or db_path.stat().st_size <= 10:
            print(f"ERROR: character database is missing or empty: {db_path}")
            return 1

        item_cfg = server.ITEM_CONFIG.get(ITEM_ID)
        if not item_cfg:
            print(f"ERROR: ItemData entry {ITEM_ID} was not loaded.")
            return 1

        # Guard against a changed or mismatched ItemData table.
        expected = (
            int(item_cfg.get("type", -1)) == 3
            and int(item_cfg.get("level", -1)) == 1
            and int(item_cfg.get("function", -1)) == 30000
        )
        if not expected:
            print(f"ERROR: ItemData {ITEM_ID} does not match the expected healing-potion row: {item_cfg}")
            return 1

        database = server.all_accounts_chars
        if not isinstance(database, dict) or not database:
            print("ERROR: no saved account/character records were loaded.")
            return 1

        targets = []
        for area_key, account_map in database.items():
            if not isinstance(account_map, dict):
                raise ValueError(f"Malformed character area entry: {area_key!r}")
            for account_key, characters in account_map.items():
                if not isinstance(characters, list):
                    raise ValueError(f"Malformed character list for area={area_key!r}, account={account_key!r}")
                for character in characters:
                    if not isinstance(character, dict):
                        raise ValueError(f"Malformed character record for area={area_key!r}, account={account_key!r}")
                    targets.append(character)

        if not targets:
            print("ERROR: the character database contains no characters.")
            return 1

        blocked = []
        for character in targets:
            if not server.can_add_to_inventory(character, ITEM_ID, AMOUNT):
                blocked.append(
                    f"{character.get('name', 'Unknown')} (id={character.get('id', 'unknown')})"
                )

        if blocked:
            print("ERROR: grant cancelled; these characters do not have room in their item backpack:")
            for entry in blocked:
                print("  " + entry)
            print("No character database changes were saved.")
            return 1

        # Write an in-progress marker before changing data so an interrupted
        # run cannot silently grant the same 100 items twice.
        marker_data = {
            "status": "in_progress",
            "item_id": ITEM_ID,
            "amount_per_character": AMOUNT,
            "character_count": len(targets),
            "started_at_utc": time.strftime("%Y-%m-%dT%H:%M:%SZ", time.gmtime()),
        }
        write_json_atomic(MARKER, marker_data)

        # Re-check after loading and preflight. The server must remain stopped.
        if game_port_is_open(port):
            print(f"ERROR: port {port} became active before the grant was saved.")
            print("The in-progress marker was retained; inspect it and the database before retrying.")
            return 1

        for character in targets:
            if not server.add_to_inventory(character, ITEM_ID, AMOUNT):
                raise RuntimeError(
                    f"add_to_inventory failed for {character.get('name')} "
                    f"(id={character.get('id')}) after successful preflight"
                )

        if not server.save_chars(database):
            raise RuntimeError("save_chars() did not confirm the atomic database save")

        marker_data.update({
            "status": "complete",
            "completed_at_utc": time.strftime("%Y-%m-%dT%H:%M:%SZ", time.gmtime()),
            "database": str(db_path),
            "backup": str(db_path) + ".bak",
        })
        write_json_atomic(MARKER, marker_data)

        print(f"SUCCESS: granted +{AMOUNT} of item {ITEM_ID} to {len(targets)} characters.")
        print(f"Database: {db_path}")
        print(f"Backup:   {db_path}.bak")
        print(f"One-time marker: {MARKER}")
        print("Restart the 9555 server; characters will receive the updated inventory on their next login.")
        return 0

    except Exception as exc:
        print(f"ERROR: grant did not complete: {exc}")
        print("Check the in-progress marker and characters_final.json before attempting any retry.")
        return 1
    finally:
        try:
            LOCK.unlink()
        except FileNotFoundError:
            pass


if __name__ == "__main__":
    sys.exit(main())
