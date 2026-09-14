import socket, struct, threading, random, json, os, time, traceback, math

PORT = int(os.environ.get("PORT", 9555))
DB_PATH = "db"
if not os.path.exists(DB_PATH): os.makedirs(DB_PATH)

MDB_FILE = "mails.json"
GDB_FILE = "gangs.json"
TDB_FILE = "teams.json"

# --- DATA ENGINE ---
DATA_DIR = "Decompiled/assets/Bundle/TextAsset"
GAME_DATA = {}

def load_game_data():
    files = [
        "NpcData", "ItemData", "ShopData", "SlotData", "TeamData", "BadgeData", "EquipData", "MountData", "SkillData", 
        "StoryData", "BaseLvData", "ConfigData", "MapInfoData", "MissionData", "MonsterData", "DailyActiveData", 
        "DailyActiveRewardData", "DominData", "WildBossData", "CopySceneData", "GuildLevelData", "GuildSkillData",
        "RetrieveData", "LevelRewardData", "SignInWeekData", "SignInMonthData", "MapConnectInfoData", "KillTargetMissionData",
        "TargetCarMissionData", "MoveTargetMissionData", "QualityData", "EquipDrop", "SkillupgradeData"
    ]
    for name in files:
        path = os.path.join(DATA_DIR, name)
        if os.path.exists(path):
            data = {}
            try:
                with open(path, "r", encoding='utf-8') as f:
                    lines = f.readlines()
                    headers = []
                    for line in lines:
                        line = line.strip()
                        if line.startswith("*,"): headers = [h for h in line.split(",") if h]
                        elif headers and ("," in line):
                            parts = [p.strip() for p in line.split(",")]
                            if len(parts) > 1:
                                key = parts[1]
                                row = {}
                                for i, h in enumerate(headers):
                                    if i+1 < len(parts):
                                        val = parts[i+1]
                                        try:
                                            if "." in val: row[h] = float(val)
                                            else: row[h] = int(val)
                                        except: row[h] = val
                                data[key] = row
            except: pass
            GAME_DATA[name] = data
    
    for mfile in ["missions.json", "mission_rewards.json"]:
        p = os.path.join("C:/Users/User/Downloads/dec&normal", mfile)
        if os.path.exists(p):
            with open(p, "r", encoding='utf-8') as f:
                GAME_DATA[mfile.replace(".json", "")] = json.load(f)

load_game_data()

# --- SPROTO CORE ---
def encode_sproto(fields, fn=None):
    if not fields: return struct.pack("<H", 0)
    fields.sort(key=lambda x: x[0])
    header = []; body = bytearray(); last_tag = -1
    for tag, val in fields:
        skip = tag - last_tag - 1
        if skip > 0: header.append(2 * (skip - 1) + 1)
        if val is None: header.append(1)
        elif isinstance(val, bool): header.append((1 if val else 0) * 2 + 2)
        elif isinstance(val, int):
            if 0 <= val <= 32766: header.append((val + 1) * 2)
            else:
                header.append(0)
                if -2147483648 <= val <= 2147483647: body += struct.pack("<I", 4) + struct.pack("<i", val)
                else: body += struct.pack("<I", 8) + struct.pack("<q", val)
        elif isinstance(val, (str, bytes, bytearray, list, dict)):
            header.append(0)
            if isinstance(val, str): v = val.encode('utf-8')
            elif isinstance(val, list):
                if val and isinstance(val[0], int): v = b"\x08" + b"".join([struct.pack("<q", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str): item = item.encode('utf-8')
                        elif not isinstance(item, (bytes, bytearray)): item = str(item).encode('utf-8')
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for k in sorted(val.keys()):
                    item = val[k]
                    if isinstance(item, (bytes, bytearray)): items.append(struct.pack("<I", len(item)) + item)
                    else: items.append(struct.pack("<I", 1) + (b'\x01' if item else b'\x00'))
                v = b"".join(items)
            else: v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag
    fn_val = fn if fn is not None else len(header)
    res = struct.pack("<H", fn_val)
    for h in header: res += struct.pack("<H", h)
    return res + body

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
        elif v == 1: curr_tag += 1
        elif v & 1: curr_tag += (v >> 1) + 1
        else: curr_tag += 1; fields[curr_tag] = (v >> 1) - 1
    return fields

def sproto_pack(data):
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i+8]
        if len(chunk) < 8: chunk += b'\x00' * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0: mask |= (1 << j)
        if mask == 0xFF: out.append(0xFF); out.append(0); out.extend(chunk)
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
            out.extend(data[i:i + count]); i += count
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < n: out.append(data[i]); i += 1
                else: out.append(0)
    return bytes(out)

# --- UTILS ---
def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None: return default
    if isinstance(val, int): return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4: return struct.unpack("<i", val)[0]
        if len(val) == 8: return struct.unpack("<q", val)[0]
        return val[0]
    return default

def calculate_power(d):
    # apk: atk*16 + hp*1 + def*11 + hit*2 + eva*5.5 + cri*10 + res*10
    atk = d.get('atk', 150); hp = d.get('hp_max', 3000); df = d.get('def', 50)
    p = atk*16 + hp*1 + df*11 + d.get('hit', 100)*2 + d.get('eva', 50)*5.5
    return int(p)

# --- GLOBAL MANAGERS ---
ONLINE_PLAYERS = {}
TEAMS = {}
GANGS = {}
MAILBOX = {}

def load_globals():
    for f, target in [(MDB_FILE, MAILBOX), (GDB_FILE, GANGS), (TDB_FILE, TEAMS)]:
        if os.path.exists(f):
            with open(f, "r") as r: target.update(json.load(r))

def save_globals():
    for f, source in [(MDB_FILE, MAILBOX), (GDB_FILE, GANGS), (TDB_FILE, TEAMS)]:
        with open(f, "w") as w: json.dump(source, w, indent=4)

load_globals()

# --- MISSION SYSTEM ---
def get_sync_missions(char_data):
    missions = []
    for mid, mdata in char_data['active_missions'].items():
        missions.append(encode_sproto([(0, str(mid)), (1, mdata['state']), (2, 0), (3, mdata['parm'])]))
    return encode_sproto([(0, missions), (1, str(char_data.get('last_main_mission_id', "-1")))])

def accept_mission(char_data, mission_id):
    cfg = GAME_DATA.get("missions", {}).get(str(mission_id))
    if not cfg: return False
    char_data['active_missions'][str(mission_id)] = {'state': 1, 'parm': [0]*8}
    return True

# --- SYSTEM HELPERS ---
def send_push(conn, tag, data):
    try:
        ph = encode_sproto([(0, tag)])
        pf = sproto_pack(ph + data)
        conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: pass

def get_full_char(d):
    gen = encode_sproto([(0, d['name']), (1, d['prof']), (3, str(d['map_id']))])
    attr_oth = encode_sproto([(0, d.get('hp', 3000)), (1, d['exp']), (2, d['level']), (3, calculate_power(d))])
    prop = encode_sproto([(13, d['cash']), (14, d.get('gold', 100))])
    vis = encode_sproto([(0, d['name']), (1, "100"), (2, "XD_A_T"), (3, "XD_A_S"), (4, "XD_A_X")])
    pos = d.get('pos', [29860, 100, -17005])
    move = encode_sproto([(0, encode_sproto([(0, int(pos[0])), (1, int(pos[1])), (2, int(pos[2]))]))])
    return encode_sproto([(0, d['id']), (1, gen), (2, attr_oth), (5, prop), (6, vis), (7, move), (15, 2)])

# --- NETWORK CORE ---
def handle_client(conn, addr):
    p_id = None
    try:
        while True:
            h = conn.recv(2)
            if not h: break
            size = struct.unpack(">H", h)[0]
            data = b""
            while len(data) < size: data += conn.recv(size - len(data))
            raw = sproto_unpack(data); pkg = decode_sproto(raw, 0)
            msg, session = get_val_int(pkg, 0), get_val_int(pkg, 1, None)
            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2); body = decode_sproto(raw, off)
            
            resp = b""
            if msg == 4: # login
                resp = encode_sproto([(0, 2), (1, "1.012.017"), (2, "205"), (3, 1)])
            elif msg == 103: # character_list
                char_files = os.listdir(DB_PATH)
                ovs = []
                for f in char_files:
                    with open(os.path.join(DB_PATH, f), "r") as r:
                        c = json.load(r)
                        gen = encode_sproto([(0, c['name']), (1, c['prof']), (3, c['map_id'])])
                        attr = encode_sproto([(0, c['level']), (1, calculate_power(c))])
                        ovs.append(encode_sproto([(0, c['id']), (1, gen), (2, attr), (4, int(time.time()))]))
                resp = encode_sproto([(0, ovs)])
            elif msg == 104: # character_create
                c_data = decode_sproto(body.get(0, b""))
                name = c_data.get(0, b"").decode(); prof = get_val_int(c_data, 1, 0)
                cid = int(time.time() * 1000)
                nc = {'id': cid, 'name': name, 'prof': prof, 'level': 1, 'exp': 0, 'cash': 1000, 
                      'map_id': "11", 'active_missions': {}, 'last_main_mission_id': "-1", 'pos': [29860, 100, -17005]}
                accept_mission(nc, "1001")
                with open(os.path.join(DB_PATH, f"{cid}.json"), "w") as w: json.dump(nc, w, indent=4)
                resp = encode_sproto([(0, None), (1, 0)])
            elif msg == 105: # character_pick
                cid = get_val_int(body, 0); p_id = str(cid)
                path = os.path.join(DB_PATH, f"{cid}.json")
                if os.path.exists(path):
                    with open(path, "r") as r: p_data = json.load(r)
                    ONLINE_PLAYERS[p_id] = {'data': p_data, 'conn': conn}; resp = encode_sproto([(0, 1)])
                    # Handshake
                    send_push(conn, 614, encode_sproto([(0, int(time.time())), (13, 1), (14, int(time.time()))]))
                    send_push(conn, 611, encode_sproto([(0, {})]))
                    send_push(conn, 540, encode_sproto([(0, {}), (1, False)]))
                    send_push(conn, 519, get_sync_missions(p_data))
                    send_push(conn, 503, encode_sproto([(0, str(p_data['map_id'])), (1, 0), (2, 1)]))
                    send_push(conn, 504, encode_sproto([(0, get_full_char(p_data))]))
                else: resp = encode_sproto([(0, 0)])
            elif msg == 100: # map_ready
                send_push(conn, 654, encode_sproto([(0, 1)]))
            elif msg == 101: # move
                if p_id:
                    pd = decode_sproto(body.get(0, b""))
                    ONLINE_PLAYERS[p_id]['data']['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2)]
                resp = encode_sproto([(0, body.get(0))])
            elif msg == 112: # accept_mission
                mid = body.get(0, b"").decode()
                if p_id: 
                    accept_mission(ONLINE_PLAYERS[p_id]['data'], mid)
                    send_push(conn, 519, get_sync_missions(ONLINE_PLAYERS[p_id]['data']))
                resp = encode_sproto([])
            elif msg == 113: # complete_mission
                mid = body.get(0, b"").decode()
                if p_id:
                    char = ONLINE_PLAYERS[p_id]['data']
                    if mid in char['active_missions']:
                        char['last_main_mission_id'] = mid
                        del char['active_missions'][mid]
                        send_push(conn, 519, get_sync_missions(char))
                resp = encode_sproto([])
            elif msg == 120: # chat
                c_type = get_val_int(body, 0); info = body.get(3, b"").decode()
                for p in ONLINE_PLAYERS.values():
                    send_push(p['conn'], 528, encode_sproto([(0, c_type), (1, int(p_id)), (2, ONLINE_PLAYERS[p_id]['data']['name']), (3, info)]))
                resp = encode_sproto([])
            elif msg == 218: # heart_beat
                resp = encode_sproto([(0, body.get(0, 0)), (1, int(time.time()))])
            
            if session is not None:
                ph = encode_sproto([(1, session)]); pf = sproto_pack(ph + resp)
                conn.sendall(struct.pack(">H", len(pf)) + pf)
    except: pass
    finally:
        if p_id and p_id in ONLINE_PLAYERS:
            with open(os.path.join(DB_PATH, f"{p_id}.json"), "w") as w: json.dump(ONLINE_PLAYERS[p_id]['data'], w, indent=4)
            del ONLINE_PLAYERS[p_id]
        conn.close()

def start():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM); s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind(("0.0.0.0", PORT)); s.listen(50)
    print(f"[GAME SERVER] Online. Data loaded.")
    while True:
        c, a = s.accept()
        threading.Thread(target=handle_client, args=(c, a), daemon=True).start()

if __name__ == "__main__": start()
