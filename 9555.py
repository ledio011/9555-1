import socket, struct, threading, random, json, os, time, traceback

PORT = int(os.environ.get("PORT", 9555))
CHAR_DB = "characters_final.json"
GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {} # inst_id -> nid (to resolve rewards)
NPC_HP_MAP = {}   # inst_id -> current hp
ONLINE_CLIENTS = {} # char_id -> (conn, picked_char)

GAME_VERSION = "1.012.017"
DATA_VERSION = "200"

# Data Tables
MISSIONS_DATA = {}
LEVEL_DATA = {}
MONSTER_DATA = {}
NPC_CONFIG = {}
MAP_CONFIG = {}
KILL_TARGET_SPAWNS = {}
TARGET_CAR_SPAWNS = {}

def load_text_asset(file_path):
    if not os.path.exists(file_path): 
        print(f"[!] File not found: {file_path}")
        return []
    with open(file_path, "r", encoding='utf-8', errors='ignore') as f:
        lines = f.readlines()
    
    header = None
    data = []
    for line in lines:
        line = line.strip()
        if not line: continue
        parts = line.split(',')
        if parts[0].startswith('*'):
            header = [p.strip() for p in parts]
            header[0] = header[0].replace('*', '')
            continue
        if header:
            entry = {}
            for i in range(min(len(header), len(parts))):
                entry[header[i]] = parts[i].strip()
            data.append(entry)
    return data

def init_game_data():
    global MISSIONS_DATA, LEVEL_DATA, MONSTER_DATA, NPC_CONFIG, MAP_CONFIG, KILL_TARGET_SPAWNS, TARGET_CAR_SPAWNS
    script_dir = os.path.dirname(__file__)
    assets_dir = os.path.join(script_dir, "Decompiled/assets/Bundle/TextAsset")
    
    for m in load_text_asset(os.path.join(assets_dir, "MissionData")):
        MISSIONS_DATA[m['ID']] = m

    for l in load_text_asset(os.path.join(assets_dir, "BaseLvData")):
        if 'Lv' in l:
            LEVEL_DATA[int(l['Lv'])] = l

    for m in load_text_asset(os.path.join(assets_dir, "MapInfoData")):
        MAP_CONFIG[m['ID']] = m

    for n in load_text_asset(os.path.join(assets_dir, "NpcData")):
        NPC_CONFIG[n['ID']] = n

    for mon in load_text_asset(os.path.join(assets_dir, "MonsterData")):
        mid = mon.get('MapID')
        if mid:
            if mid not in MONSTER_DATA: MONSTER_DATA[mid] = []
            MONSTER_DATA[mid].append(mon)

    for kt in load_text_asset(os.path.join(assets_dir, "KillTargetMissionData")):
        mid = kt.get('ID')
        if mid:
            if mid not in KILL_TARGET_SPAWNS: KILL_TARGET_SPAWNS[mid] = []
            KILL_TARGET_SPAWNS[mid].append(kt)

    for tc in load_text_asset(os.path.join(assets_dir, "TargetCarMissionData")):
        mid = tc.get('ID')
        if mid:
            if mid not in TARGET_CAR_SPAWNS: TARGET_CAR_SPAWNS[mid] = []
            TARGET_CAR_SPAWNS[mid].append(tc)

    print(f"[DATA LOADED] Missions: {len(MISSIONS_DATA)}, Levels: {len(LEVEL_DATA)}, Maps: {len(MAP_CONFIG)}")

init_game_data()

# Persistence
def load_chars():
    if os.path.exists(CHAR_DB):
        try:
            with open(CHAR_DB, "r") as f: return json.load(f)
        except: return {}
    return {}

def save_chars(data):
    try:
        with open(CHAR_DB, "w") as f: json.dump(data, f, indent=4)
    except: pass

all_accounts_chars = load_chars()

# Networking
def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, int): return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4: return struct.unpack("<i", val)[0]
        if len(val) == 8: return struct.unpack("<q", val)[0]
        if len(val) == 1: return val[0]
    return default

def encode_sproto(fields, fn=None):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    header = []; body = bytearray(); last_tag = -1
    for tag, val in fields:
        skip = tag - last_tag - 1
        if skip > 0: header.append(2 * (skip - 1) + 1)
        if val is None:
            header.append(1)
        elif isinstance(val, bool):
            header.append((1 if val else 0) * 2 + 2)
        elif isinstance(val, int):
            if 0 <= val <= 32766:
                header.append((val + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= val <= 2147483647:
                    body += struct.pack("<I", 4) + struct.pack("<i", val)
                else:
                    body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray, list, dict)):
            header.append(0)
            if isinstance(val, str):
                v = val.encode('utf-8')
            elif isinstance(val, list):
                if val and isinstance(val[0], int):
                    v = b"\x08" + b"".join([struct.pack("<q", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif isinstance(item, (bytes, bytearray)): pass
                        else: item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for item in val.values():
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else:
                        items.append(struct.pack("<I", 1) + (b'\x01' if item else b'\x00'))
                v = b"".join(items)
            else:
                v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag
    fn_val = fn if fn is not None else len(header)
    res = struct.pack("<H", fn_val)
    for h in header: res += struct.pack("<H", h)
    return res + body

def sproto_pack(data):
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i+8]
        if len(chunk) < 8: chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0: mask |= (1 << j)
        if mask == 0xFF:
            out.append(0xFF); out.append(0); out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j): out.append(chunk[j])
    return bytes(out)

def sproto_unpack(data):
    out = bytearray(); i = 0; n = len(data)
    while i < n:
        mask = data[i]; i += 1
        if mask == 0xFF:
            if i >= n: break
            count = (data[i] + 1) * 8; i += 1
            out.extend(data[i:i+count]); i += count
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < n: out.append(data[i]); i += 1
                else: out.append(0)
    return bytes(out)

def decode_sproto(data, offset=0):
    if len(data) < offset + 2: return {}
    fn = struct.unpack("<H", data[offset:offset+2])[0]
    h_ptr, b_ptr = offset + 2, offset + 2 + fn*2
    fields, curr_tag = {}, -1
    for i in range(fn):
        v = struct.unpack("<H", data[h_ptr + i*2 : h_ptr + i*2 + 2])[0]
        if v == 0:
            curr_tag += 1
            if b_ptr + 4 <= len(data):
                l = struct.unpack("<I", data[b_ptr:b_ptr+4])[0]
                fields[curr_tag] = data[b_ptr+4:b_ptr+4+l]
                b_ptr += 4 + l
        elif v == 1:
            curr_tag += 1
        elif v & 1:
            curr_tag += (v >> 1) + 1
        else:
            curr_tag += 1
            fields[curr_tag] = (v >> 1) - 1
    return fields

# Gameplay Data Helpers
def get_visual(name, prof):
    m = {0:{"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1:{"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2:{"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    return encode_sproto([(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)])

def get_character_stats(c):
    lv = int(c.get('level', 1))
    prof = int(c.get('prof', 0))
    ld = LEVEL_DATA.get(lv, LEVEL_DATA.get(1))
    if not ld: return {'power': 100, 'atk': 10, 'hp_max': 100, 'def': 10}
    
    coeffs = [
        {"atk":16, "def":11, "hit":2, "eva":5.5, "cri":10, "res":10},
        {"atk":20, "def":12, "hit":1, "eva":6, "cri":5, "res":10},
        {"atk":7, "def":7.4, "hit":3, "eva":3.7, "cri":15, "res":10}
    ][prof]

    atk = int(ld.get('ATKXD' if prof==0 else 'ATKQJ' if prof==1 else 'ATKNQ', 10))
    hp_max = int(ld.get('HPXD' if prof==0 else 'HPQJ' if prof==1 else 'HPNQ', 100))
    df = int(ld.get('DEFXD' if prof==0 else 'DEFQJ' if prof==1 else 'DEFNQ', 10))
    hit = int(ld.get('HITXD' if prof==0 else 'HITQJ' if prof==1 else 'HITNQ', 10))
    eva = int(ld.get('DGEXD' if prof==0 else 'DGEQJ' if prof==1 else 'DGENQ', 10))
    cri = int(ld.get('CRIXD' if prof==0 else 'CRIQJ' if prof==1 else 'CRINQ', 10))
    res = int(ld.get('RESXD' if prof==0 else 'RESQJ' if prof==1 else 'RESNQ', 10))

    power = int(atk * coeffs['atk'] + hp_max * 1.0 + df * coeffs['def'] + hit * coeffs['hit'] + eva * coeffs['eva'] + cri * coeffs['cri'] + res * coeffs['res'])
    
    return {
        'atk': atk, 'hp_max': hp_max, 'def': df, 'hit': hit, 'eva': eva, 'cri': cri, 'res': res, 'power': power,
        'defa': 10000, 'dgea': 10000, 'resa': 10000, 'hita': 10000, 'cria': 10000, 'ate': 0, 'satm': 0, 'satc': 0, 'satp': 0
    }

def get_char_ov(c, sort_index=None):
    prof = int(c.get('prof', 0))
    name = c.get('name', 'Hero')
    gen = encode_sproto([(0, name), (1, prof), (2, 1), (3, str(c.get('map_id', '11'))), (4, 1)])
    stats = get_character_stats(c)
    attr = encode_sproto([(0, int(c.get('level', 1))), (1, stats['power'])])
    ctime = sort_index if sort_index is not None else int(time.time())
    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr), (3, get_visual(name, prof)), (4, ctime), (5, 0)
    ])

def get_full_char(c):
    stats = get_character_stats(c)
    prof = int(c.get('prof', 0))
    name = c.get('name', 'Hero')
    
    gen = encode_sproto([(0, name), (1, prof), (2, 1), (3, str(c.get('map_id', '11'))), (4, 1)])
    attr_oth = encode_sproto([(0, c.get('hp', stats['hp_max'])), (1, c.get('exp', 0)), (2, int(c.get('level', 1))), (3, stats['power']), (15, 1)])
    prop = encode_sproto([(13, c.get('cash', 1000)), (14, 100), (15, 10)])
    pos_data = c.get('pos', [7007, 100, 5033, 0])
    pos = encode_sproto([(0, pos_data[0]), (1, pos_data[1]), (2, pos_data[2]), (3, pos_data[3])])
    mv = encode_sproto([(0, pos), (1, pos)])
    
    attr_run = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all = encode_sproto([
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']), (4, stats['hit']), (5, stats['eva']), 
        (6, stats['cri']), (7, stats['res']), (13, 500)
    ])
    run = encode_sproto([(6, attr_run), (7, attr_all)])
    
    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr_oth), (5, prop), (6, get_visual(name, prof)), (7, mv), (13, run), (15, 2)
    ])

def sync_mission_data(picked_char):
    own_missions = []
    for mid, mstate in picked_char.get('active_missions', {}).items():
        parm = mstate.get('parm', [0]*8)
        own_missions.append(encode_sproto([(0, str(mid)), (1, mstate['state']), (2, 0), (3, parm)]))
    
    last_main = picked_char.get('last_main', "1001")
    return encode_sproto([(0, own_missions), (1, last_main), (2, [])])

def spawn_npc(conn, nid, x, z, o=0):
    global GLOBAL_INST_COUNTER
    cfg = NPC_CONFIG.get(str(nid))
    if not cfg: 
        print(f"[!] NPC Config not found for {nid}")
        return
    
    GLOBAL_INST_COUNTER += 1
    inst_id = GLOBAL_INST_COUNTER
    hp = 10000 # Default
    if 'Hp' in cfg: hp = int(cfg['Hp'])
    elif 'hp_abs' in cfg: hp = int(cfg['hp_abs'])
    
    NPC_HP_MAP[inst_id] = hp
    NPC_INST_MAP[inst_id] = str(nid)
    
    attr = encode_sproto([
        (0, inst_id), (1, str(nid)), (2, hp), (3, hp), (4, 100), (5, 10),
        (15, x), (16, z), (17, o), (18, int(cfg.get('Lv', 1))), (21, cfg.get('Name', 'NPC'))
    ])
    
    packet = sproto_pack(encode_sproto([(0, 509)]) + encode_sproto([(0, attr)]))
    conn.sendall(struct.pack(">H", len(packet)) + packet)
    return inst_id

def client_handler(conn, addr):
    print(f"[+] Game Connection: {addr}")
    acc_id = "0"
    picked_char = None
    
    def send_push(tag, data):
        ph = encode_sproto([(0, tag)])
        pf = sproto_pack(ph + data)
        conn.sendall(struct.pack(">H", len(pf)) + pf)

    try:
        while True:
            h_bytes = conn.recv(2)
            if not h_bytes: break
            size = struct.unpack(">H", h_bytes)[0]
            data = b""
            while len(data) < size:
                data += conn.recv(size - len(data))
            
            raw = sproto_unpack(data)
            pkg = decode_sproto(raw, 0)
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2)
            body = decode_sproto(raw, off)

            if msg == 4: # login
                acc_id = body.get(1, b"").decode('utf-8')
                resp = encode_sproto([(0, 2), (1, GAME_VERSION), (2, DATA_VERSION), (3, 1)])
                full = sproto_pack(encode_sproto([(1, session)]) + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg == 103: # character_list
                chars = all_accounts_chars.get(acc_id, [])
                ovs = [get_char_ov(c, i) for i, c in enumerate(chars)]
                full = sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, ovs)]))
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode('utf-8')
                prof = get_val_int(c_data, 1, 0)
                cid = int(time.time() * 1000) % 1000000000
                if acc_id not in all_accounts_chars: all_accounts_chars[acc_id] = []
                new_char = {'id': cid, 'name': name, 'prof': prof, 'level': 1, 'exp': 0, 'map_id': '11', 'pos': [7007, 100, 5033, 0]}
                all_accounts_chars[acc_id].append(new_char)
                save_chars(all_accounts_chars)
                resp = encode_sproto([(0, get_char_ov(new_char)), (1, 0)])
                full = sproto_pack(encode_sproto([(1, session)]) + resp)
                conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg == 105: # character_pick
                cid = get_val_int(body, 0)
                picked_char = next((c for c in all_accounts_chars.get(acc_id, []) if c['id'] == cid), None)
                if picked_char:
                    if 'active_missions' not in picked_char: picked_char['active_missions'] = {"1001": {"state": 1, "parm": [0]*8}}
                    
                    full = sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, 1)]))
                    conn.sendall(struct.pack(">H", len(full)) + full)
                    
                    # Entry Sequence
                    send_push(614, encode_sproto([(0, int(time.time())), (13, 1), (14, int(time.time()))])) # sync_common_data
                    send_push(611, encode_sproto([(0, {})])) # sync_item_pack
                    send_push(519, sync_mission_data(picked_char))
                    
                    mid = str(picked_char.get('map_id', '11'))
                    send_push(503, encode_sproto([(0, mid), (1, 0), (2, 1)])) # enter_map
                else:
                    full = sproto_pack(encode_sproto([(1, session)]) + encode_sproto([(0, 0)]))
                    conn.sendall(struct.pack(">H", len(full)) + full)

            elif msg == 100: # map_ready
                if picked_char:
                    send_push(654, encode_sproto([(0, 1)])) # start_enter_game
                    send_push(504, encode_sproto([(0, get_full_char(picked_char))])) # main_player_create
                    
                    # Spawn Mission 1001 Target
                    if "1001" in picked_char['active_missions']:
                        spawn_npc(conn, 9901, 6287, 3512)

            elif msg == 307: # local_npc_die
                npcid = body.get(0, b"").decode('utf-8')
                if picked_char:
                    for mid, mstate in picked_char['active_missions'].items():
                        mcfg = MISSIONS_DATA.get(mid)
                        if mcfg and mcfg.get('Target') == npcid:
                            mstate['parm'][0] += 1
                            send_push(524, encode_sproto([(0, mid), (1, 1), (2, mstate['parm'][0])]))
                            if mstate['parm'][0] >= int(mcfg.get('require_num', 2)):
                                mstate['state'] = 2 # COMPLETE
                                send_push(523, encode_sproto([(0, mid), (1, 2)]))
                    save_chars(all_accounts_chars)

            elif session is not None:
                full = sproto_pack(encode_sproto([(1, session)]) + encode_sproto([]))
                conn.sendall(struct.pack(">H", len(full)) + full)
    except: traceback.print_exc()
    finally: conn.close()

def start_server():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind(("0.0.0.0", PORT))
    s.listen(20)
    print(f"RECONSTRUCTED GAME SERVER READY ON {PORT}")
    while True:
        cl, ad = s.accept()
        threading.Thread(target=client_handler, args=(cl, ad), daemon=True).start()

if __name__ == "__main__":
    start_server()
