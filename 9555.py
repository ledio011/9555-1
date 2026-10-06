#!/usr/bin/env python3
"""
================================================================================
  APK Sproto Protocol Inspector & Server 15678 Missing Feature Detector
  Port: 9555 (Proxy / Server)  <--->  Target: s16.serv00.com:15678
================================================================================
This script:
1. Automatically parses all 409 protocols and 910 Sproto schemas from the APK.
2. Intercepts/listens to client network traffic (on port 9555 or 15678).
3. Decodes all incoming client requests (Tags, Sproto types, fields, sessions).
4. Forwards to server 15678 and detects EVERYTHING missing or unhandled by server 15678.
5. Displays ALL possible responses (RPC responses and server notifications) for this APK.
6. Can synthesize valid mock responses so the client proceeds and reveals further missing tags.
7. Provides full offline catalog inspection (--all, --responses, --dump, --search).
"""

import sys
import os
import re
import time
import json
import socket
import select
import struct
import threading
import argparse

# Ensure UTF-8 output on Windows consoles
if hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8', errors='replace')
if hasattr(sys.stderr, 'reconfigure'):
    sys.stderr.reconfigure(encoding='utf-8', errors='replace')

import functools
print = functools.partial(print, flush=True)

# Enable ANSI colors on Windows terminal
if os.name == 'nt':
    os.system('')

# ANSI Colors
C_RESET   = "\033[0m"
C_BOLD    = "\033[1m"
C_DIM     = "\033[2m"
C_RED     = "\033[91m"
C_GREEN   = "\033[92m"
C_YELLOW  = "\033[93m"
C_BLUE    = "\033[94m"
C_MAGENTA = "\033[95m"
C_CYAN    = "\033[96m"
C_WHITE   = "\033[97m"

def c_warn(msg):   return f"{C_YELLOW}{C_BOLD}{msg}{C_RESET}"
def c_err(msg):    return f"{C_RED}{C_BOLD}{msg}{C_RESET}"
def c_ok(msg):     return f"{C_GREEN}{C_BOLD}{msg}{C_RESET}"
def c_info(msg):   return f"{C_CYAN}{msg}{C_RESET}"
def c_title(msg):  return f"{C_MAGENTA}{C_BOLD}{msg}{C_RESET}"
def c_bold(msg):   return f"{C_BOLD}{msg}{C_RESET}"

# ==============================================================================
# 1. Sproto Binary Serialization Engine (CloudWu Sproto Standard)
# ==============================================================================

class SprotoBinary:
    @staticmethod
    def unpack(data: bytes) -> bytes:
        """Sproto 0-pack unpacking."""
        out = bytearray()
        i = 0
        n = len(data)
        while i < n:
            b = data[i]
            i += 1
            if b == 0xFF:
                if i >= n:
                    break
                blocks = (data[i] + 1) * 8
                i += 1
                out.extend(data[i:i + blocks])
                i += blocks
            else:
                for bit in range(8):
                    if (b >> bit) & 1:
                        if i < n:
                            out.append(data[i])
                            i += 1
                        else:
                            out.append(0)
                    else:
                        out.append(0)
        return bytes(out)

    @staticmethod
    def pack(data: bytes) -> bytes:
        """Sproto 0-pack packing."""
        out = bytearray()
        n = len(data)
        ff_src = bytearray()
        i = 0
        while i < n:
            chunk = data[i:i + 8]
            if len(chunk) < 8:
                chunk = chunk + b'\x00' * (8 - len(chunk))
            bitmap = 0
            nonzero = []
            for bit, b in enumerate(chunk):
                if b != 0:
                    bitmap |= (1 << bit)
                    nonzero.append(b)
            if len(nonzero) == 8:
                ff_src.extend(chunk)
                if len(ff_src) == 2048:
                    out.append(0xFF)
                    out.append(len(ff_src) // 8 - 1)
                    out.extend(ff_src)
                    ff_src.clear()
            else:
                if ff_src:
                    out.append(0xFF)
                    out.append(len(ff_src) // 8 - 1)
                    out.extend(ff_src)
                    ff_src.clear()
                out.append(bitmap)
                out.extend(nonzero)
            i += 8
        if ff_src:
            out.append(0xFF)
            out.append(len(ff_src) // 8 - 1)
            out.extend(ff_src)
        return bytes(out)

    @staticmethod
    def decode_struct(data: bytes, schema=None, all_schemas=None):
        """
        Decodes a Sproto struct according to schema or dynamically.
        Returns: (decoded_dict, bytes_consumed)
        """
        if len(data) < 2:
            return {}, 0
        fn = struct.unpack_from('<H', data, 0)[0]
        header_sz = 2 + fn * 2
        if len(data) < header_sz:
            return {}, 0
        
        words = [struct.unpack_from('<H', data, 2 + i * 2)[0] for i in range(fn)]
        field_entries = []
        tag = -1
        for w in words:
            tag += 1
            if (w & 1) == 0:
                val = (w // 2) - 1
                if val >= 0:
                    field_entries.append((tag, True, val))
                else:
                    field_entries.append((tag, False, None))
            else:
                tag += (w // 2)
        
        result = {}
        data_pos = header_sz
        schema_fields = schema.get('fields', {}) if schema else {}

        for tag, is_inline, inline_val in field_entries:
            finfo = schema_fields.get(tag, {})
            fname = finfo.get('name', f"tag_{tag}")
            ftype = finfo.get('type', 'unknown')
            wmethod = finfo.get('write_method', '')
            
            if is_inline:
                if 'bool' in ftype.lower() or 'boolean' in wmethod:
                    result[fname] = bool(inline_val != 0)
                else:
                    result[fname] = inline_val
            else:
                if data_pos + 4 > len(data):
                    break
                sz = struct.unpack_from('<I', data, data_pos)[0]
                data_pos += 4
                raw_bytes = data[data_pos:data_pos + sz]
                data_pos += sz
                
                if 'integer' in wmethod or ftype in ('long', 'int', 'short'):
                    if sz == 4:
                        result[fname] = struct.unpack('<i', raw_bytes)[0]
                    elif sz == 8:
                        result[fname] = struct.unpack('<q', raw_bytes)[0]
                    else:
                        result[fname] = int.from_bytes(raw_bytes, 'little', signed=True)
                elif 'string' in wmethod or ftype == 'string':
                    try:
                        result[fname] = raw_bytes.decode('utf-8')
                    except UnicodeDecodeError:
                        result[fname] = raw_bytes.hex()
                elif 'boolean' in wmethod or ftype == 'bool':
                    result[fname] = (raw_bytes[0] != 0) if len(raw_bytes) > 0 else False
                elif 'obj' in wmethod or (all_schemas and ftype in all_schemas):
                    sub_schema = all_schemas.get(ftype, {}) if all_schemas else None
                    sub_res, _ = SprotoBinary.decode_struct(raw_bytes, sub_schema, all_schemas)
                    result[fname] = sub_res
                else:
                    try:
                        result[fname] = raw_bytes.decode('utf-8')
                    except Exception:
                        result[fname] = f"<hex: {raw_bytes.hex()}>"

        return result, data_pos

    @staticmethod
    def encode_struct(fields_dict: dict, schema=None, all_schemas=None) -> bytes:
        """Encodes a dict into Sproto binary data according to schema."""
        schema_fields = schema.get('fields', {}) if schema else {}
        tags_to_encode = []
        for tag, finfo in schema_fields.items():
            fname = finfo.get('name')
            if fname in fields_dict:
                tags_to_encode.append((tag, finfo, fields_dict[fname]))
        
        tags_to_encode.sort(key=lambda x: x[0])
        records = []
        last_tag = -1
        data_buf = bytearray()
        
        for tag, finfo, val in tags_to_encode:
            gap = tag - last_tag - 1
            if gap > 0:
                skip_record = (gap - 1) * 2 + 1
                records.append(skip_record)
            
            ftype = finfo.get('type', '')
            wmethod = finfo.get('write_method', '')
            
            if 'boolean' in wmethod or ftype == 'bool':
                bval = 1 if val else 0
                inline_val = (bval + 1) * 2
                records.append(inline_val)
            elif ('integer' in wmethod or ftype in ('long', 'int')) and isinstance(val, int) and 0 <= val < 32767:
                inline_val = (val + 1) * 2
                records.append(inline_val)
            else:
                records.append(0)
                if isinstance(val, int):
                    if -0x80000000 <= val <= 0x7FFFFFFF:
                        data_buf.extend(struct.pack('<I', 4))
                        data_buf.extend(struct.pack('<i', val))
                    else:
                        data_buf.extend(struct.pack('<I', 8))
                        data_buf.extend(struct.pack('<q', val))
                elif isinstance(val, str):
                    enc = val.encode('utf-8')
                    data_buf.extend(struct.pack('<I', len(enc)))
                    data_buf.extend(enc)
                elif isinstance(val, bytes):
                    data_buf.extend(struct.pack('<I', len(val)))
                    data_buf.extend(val)
                elif isinstance(val, dict):
                    sub_schema = all_schemas.get(ftype, {}) if all_schemas else None
                    sub_bin = SprotoBinary.encode_struct(val, sub_schema, all_schemas)
                    data_buf.extend(struct.pack('<I', len(sub_bin)))
                    data_buf.extend(sub_bin)
                else:
                    data_buf.extend(struct.pack('<I', 0))
            last_tag = tag

        fn = len(records)
        hdr = struct.pack('<H', fn) + b''.join(struct.pack('<H', r) for r in records)
        return hdr + bytes(data_buf)


# ==============================================================================
# 2. APK Protocol & Sproto Schema Database
# ==============================================================================

class APKSchemaDB:
    def __init__(self, base_dir=None):
        if base_dir is None:
            base_dir = os.path.dirname(os.path.abspath(__file__))
        self.base_dir = base_dir
        self.proto_file = os.path.join(base_dir, 'assets', 'bin', 'Data', 'Managed', 'Assembly-CSharp', 'Protocol.cs')
        self.recv_file = os.path.join(base_dir, 'assets', 'bin', 'Data', 'Managed', 'Assembly-CSharp', 'NetReceiver.cs')
        self.sproto_dir = os.path.join(base_dir, 'assets', 'bin', 'Data', 'Managed', 'Assembly-CSharp', 'SprotoType')
        self.cache_file = os.path.join(base_dir, 'apk_protocol_schema.json')
        
        self.protocols = {}      # tag -> {tag, name, request, response}
        self.name_to_tag = {}    # name -> tag
        self.push_handlers = {}  # name -> func
        self.type_schemas = {}   # full_cls_name -> {name, max_fields, fields: {tag -> finfo}}
        
        self.package_schema = {
            'name': 'Package',
            'max_fields': 2,
            'fields': {
                0: {'tag': 0, 'name': 'type', 'write_method': 'write_integer', 'type': 'long'},
                1: {'tag': 1, 'name': 'session', 'write_method': 'write_integer', 'type': 'long'}
            }
        }
        self.load()

    def load(self):
        if os.path.exists(self.cache_file):
            try:
                with open(self.cache_file, 'r', encoding='utf-8') as f:
                    data = json.load(f)
                self.protocols = {int(k): v for k, v in data['protocols'].items()}
                self.name_to_tag = {v['name']: int(k) for k, v in self.protocols.items()}
                self.push_handlers = data['push_handlers']
                # Convert field keys to int
                self.type_schemas = {}
                for k, v in data['type_schemas'].items():
                    fields = {int(fk): fv for fk, fv in v['fields'].items()}
                    self.type_schemas[k] = {'name': v['name'], 'max_fields': v['max_fields'], 'fields': fields}
                return
            except Exception:
                pass

        self._extract_from_source()
        self._save_cache()

    def _extract_from_source(self):
        if not os.path.exists(self.proto_file):
            return

        # 1. Parse Protocol.cs
        with open(self.proto_file, 'r', encoding='utf-8', errors='ignore') as f:
            proto_txt = f.read()

        for name, tag in re.findall(r'SetProtocol<Protocol\.(\w+)>\((\d+)\)', proto_txt):
            t = int(tag)
            self.protocols[t] = {'tag': t, 'name': name, 'request': None, 'response': None}
            self.name_to_tag[name] = t

        for name, tag in re.findall(r'SetRequest<SprotoType\.([^\.]+)\.request>\((\d+)\)', proto_txt):
            t = int(tag)
            if t in self.protocols:
                self.protocols[t]['request'] = name

        for name, tag in re.findall(r'SetResponse<SprotoType\.([^\.]+)\.response>\((\d+)\)', proto_txt):
            t = int(tag)
            if t in self.protocols:
                self.protocols[t]['response'] = name

        # 2. Parse NetReceiver.cs
        if os.path.exists(self.recv_file):
            with open(self.recv_file, 'r', encoding='utf-8', errors='ignore') as f:
                recv_txt = f.read()
            self.push_handlers = dict(re.findall(r'AddHandler<Protocol\.(\w+)>\(new RpcReqHandler\(([^)]+)\)', recv_txt))

        # 3. Parse all SprotoType files
        if os.path.exists(self.sproto_dir):
            for fname in os.listdir(self.sproto_dir):
                if not fname.endswith('.cs'):
                    continue
                base_name = os.path.splitext(fname)[0]
                with open(os.path.join(self.sproto_dir, fname), 'r', encoding='utf-8', errors='ignore') as f:
                    content = f.read()

                class_sections = re.split(r'public class (\w+)', content)
                for i in range(1, len(class_sections), 2):
                    cls_name = class_sections[i]
                    cls_body = class_sections[i+1]
                    full_cls_name = f"{base_name}.{cls_name}" if cls_name in ('request', 'response') else cls_name

                    props = {name: ptype.strip() for ptype, name in re.findall(r'public\s+([\w<>,]+)\s+(\w+)\s*\{\s*get', cls_body) if not name.startswith('Has')}
                    writes = re.findall(r'this\.serialize\.(write_\w+(?:<[^>]+>)?)\(this\.(\w+),\s*(\d+)\)', cls_body)
                    mfc = re.search(r'max_field_count\s*=\s*(\d+);', cls_body)
                    max_fields = int(mfc.group(1)) if mfc else len(writes)

                    fields = {}
                    for wtype, fname_var, tag_str in writes:
                        t = int(tag_str)
                        fields[t] = {
                            'tag': t,
                            'name': fname_var,
                            'write_method': wtype,
                            'type': props.get(fname_var, 'unknown')
                        }
                    self.type_schemas[full_cls_name] = {'name': full_cls_name, 'max_fields': max_fields, 'fields': fields}

    def _save_cache(self):
        try:
            cache_data = {
                'protocols': self.protocols,
                'push_handlers': self.push_handlers,
                'type_schemas': self.type_schemas
            }
            with open(self.cache_file, 'w', encoding='utf-8') as f:
                json.dump(cache_data, f, indent=2)
        except Exception:
            pass

    def get_protocol(self, tag: int):
        return self.protocols.get(tag)

    def get_schema(self, schema_name: str):
        return self.type_schemas.get(schema_name)

    def get_possible_responses(self, tag: int):
        """
        Returns all possible responses for a given request Tag:
        - Inline RPC response (if defined)
        - Any related server-to-client push notifications triggered in game flow
        """
        proto = self.protocols.get(tag)
        if not proto:
            return None
        
        results = {
            'rpc_response': None,
            'related_pushes': []
        }
        
        if proto['response']:
            rsp_key = f"{proto['response']}.response"
            schema = self.get_schema(rsp_key)
            results['rpc_response'] = {
                'name': proto['response'],
                'full_type': f"SprotoType.{rsp_key}",
                'schema': schema,
                'sample': self.generate_default_sample(schema)
            }
            
        # Check associated push responses (e.g. for mission, login, map, etc.)
        pname = proto['name']
        for push_name in self.push_handlers:
            push_tag = self.name_to_tag.get(push_name)
            # Find related notifications
            if push_name.startswith(f"ret_{pname}") or push_name == f"ret_{pname}" or (pname in push_name and push_name != pname):
                push_proto = self.protocols.get(push_tag, {})
                push_schema_key = f"{push_proto.get('request')}.request" if push_proto.get('request') else push_name
                push_schema = self.get_schema(push_schema_key)
                results['related_pushes'].append({
                    'tag': push_tag,
                    'name': push_name,
                    'full_type': f"SprotoType.{push_schema_key}",
                    'schema': push_schema,
                    'sample': self.generate_default_sample(push_schema)
                })

        return results

    def generate_default_sample(self, schema):
        """Generates a default working dictionary for mock response."""
        if not schema or not schema.get('fields'):
            return {}
        res = {}
        for tag, finfo in schema['fields'].items():
            fname = finfo['name']
            ftype = finfo.get('type', '')
            wmethod = finfo.get('write_method', '')
            
            if 'bool' in ftype or 'boolean' in wmethod:
                res[fname] = True
            elif 'string' in ftype or 'string' in wmethod:
                if 'version' in fname.lower():
                    res[fname] = "1.012.017"
                else:
                    res[fname] = "ok"
            elif 'integer' in wmethod or ftype in ('long', 'int'):
                if fname == 'errno' or fname == 'type':
                    res[fname] = 0  # Success
                elif 'id' in fname.lower() or 'level' in fname.lower():
                    res[fname] = 1
                else:
                    res[fname] = 0
            elif 'obj' in wmethod:
                res[fname] = {}
            else:
                res[fname] = 0
        return res


# ==============================================================================
# 3. Missing Feature Tracker & Inspector Logger
# ==============================================================================

class MissingTracker:
    def __init__(self):
        self.lock = threading.Lock()
        self.missing_requests = {} # tag -> {'count': int, 'last_seen': float, 'name': str, 'req_type': str, 'sample_req': dict, 'rsp_info': dict}
        self.handled_requests = {} # tag -> count

    def record_missing(self, tag: int, name: str, req_type: str, req_payload: dict, rsp_info: dict, session: int):
        with self.lock:
            if tag not in self.missing_requests:
                self.missing_requests[tag] = {
                    'tag': tag,
                    'name': name,
                    'req_type': req_type,
                    'count': 0,
                    'sessions': set(),
                    'last_seen': time.time(),
                    'sample_req': req_payload,
                    'rsp_info': rsp_info
                }
            self.missing_requests[tag]['count'] += 1
            self.missing_requests[tag]['sessions'].add(session)
            self.missing_requests[tag]['last_seen'] = time.time()
            self.missing_requests[tag]['sample_req'] = req_payload

    def record_handled(self, tag: int):
        with self.lock:
            self.handled_requests[tag] = self.handled_requests.get(tag, 0) + 1

    def print_summary(self):
        with self.lock:
            print(f"\n{C_BOLD}{'='*80}{C_RESET}")
            print(f"{C_YELLOW}{C_BOLD}   [SERVER 15678 MISSING REQUESTS SUMMARY REPORT]{C_RESET}")
            print(f"{C_BOLD}{'='*80}{C_RESET}")
            if not self.missing_requests:
                print(f" {C_GREEN}No missing requests detected yet! All requests were handled.{C_RESET}\n")
                return
            
            print(f" Total Unique Tags Missing: {C_RED}{len(self.missing_requests)}{C_RESET}\n")
            for tag, info in sorted(self.missing_requests.items()):
                print(f" {C_RED}{C_BOLD}* Tag {tag:>3d} [{info['name']}]{C_RESET}  (Missed {info['count']} times)")
                print(f"   - Client Sproto Request:  {C_CYAN}{info['req_type']}{C_RESET}")
                print(f"   - Last Request Payload:   {info['sample_req']}")
                
                rsp = info['rsp_info'].get('rpc_response') if info['rsp_info'] else None
                if rsp:
                    print(f"   - {C_GREEN}REQUIRED APK RESPONSE:{C_RESET} {C_BOLD}{rsp['full_type']}{C_RESET}")
                    schema = rsp.get('schema')
                    if schema and schema.get('fields'):
                        for ftag, finfo in schema['fields'].items():
                            print(f"       [{ftag}] {finfo['name']:<18s} ({finfo['type']})")
                    print(f"   - {C_CYAN}Sample Response JSON:{C_RESET} {json.dumps(rsp.get('sample', {}))}")
                else:
                    print(f"   - {C_YELLOW}Expected Action:{C_RESET} Server must accept/process or send push notifications.")
                    pushes = info['rsp_info'].get('related_pushes', []) if info['rsp_info'] else []
                    if pushes:
                        print(f"   - {C_GREEN}Possible Server Push Notifications:{C_RESET}")
                        for p in pushes:
                            print(f"       * Tag {p['tag']}: {p['name']} ({p['full_type']})")
                print(f" {C_DIM}{'-'*76}{C_RESET}")
            print()


# ==============================================================================
# 4. Core Network Proxy & Missing Server 15678 Inspector
# ==============================================================================

class InspectorProxy:
    def __init__(self, listen_host='0.0.0.0', listen_port=9555, target_host='s16.serv00.com', target_port=15678, auto_mock=True, db=None):
        self.listen_host = listen_host
        self.listen_port = listen_port
        self.target_host = target_host
        self.target_port = target_port
        self.auto_mock = auto_mock
        self.db = db or APKSchemaDB()
        self.tracker = MissingTracker()
        self.running = False
        self.server_sock = None

    def start(self):
        self.running = True
        self.server_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server_sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.server_sock.bind((self.listen_host, self.listen_port))
        self.server_sock.listen(10)
        
        print(f"\n{C_GREEN}{C_BOLD}{'='*80}{C_RESET}")
        print(f"{C_GREEN}{C_BOLD}   APK PROTOCOL INSPECTOR & SERVER 15678 DETECTOR STARTED{C_RESET}")
        print(f"{C_GREEN}{C_BOLD}{'='*80}{C_RESET}")
        print(f" * Local Inspector Listening on : {C_BOLD}{self.listen_host}:{self.listen_port}{C_RESET}")
        print(f" * Target Remote Game Server    : {C_BOLD}{self.target_host}:{self.target_port}{C_RESET}")
        print(f" * Auto-Mock Missing Responses  : {C_BOLD}{'ENABLED (Client will proceed)' if self.auto_mock else 'DISABLED'}{C_RESET}")
        print(f" * Loaded APK Protocols         : {C_BOLD}{len(self.db.protocols)} protocols, {len(self.db.type_schemas)} Sproto schemas{C_RESET}")
        print(f"{C_GREEN}{C_BOLD}{'-'*80}{C_RESET}")
        print(f" {C_YELLOW}Point your APK client to port {self.listen_port}. Everything client requests will be shown below!{C_RESET}\n")

        try:
            while self.running:
                client_sock, client_addr = self.server_sock.accept()
                t = threading.Thread(target=self._handle_client_connection, args=(client_sock, client_addr), daemon=True)
                t.start()
        except KeyboardInterrupt:
            print(f"\n{C_YELLOW}Shutting down inspector...{C_RESET}")
        finally:
            self.running = False
            if self.server_sock:
                self.server_sock.close()
            self.tracker.print_summary()

    def _connect_to_target(self):
        try:
            target_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            target_sock.settimeout(3.0)
            target_sock.connect((self.target_host, self.target_port))
            target_sock.settimeout(None)
            return target_sock
        except Exception as e:
            return None

    def _handle_client_connection(self, client_sock, client_addr):
        client_ip, client_port = client_addr
        print(f"{C_GREEN}[+] APK Client Connected from {client_ip}:{client_port}{C_RESET}")

        target_sock = self._connect_to_target()
        if target_sock:
            print(f"{C_BLUE}[+] Connected to target server {self.target_host}:{self.target_port}{C_RESET}")
        else:
            print(f"{C_RED}[!] Target server {self.target_host}:{self.target_port} NOT reachable! Operating in Standalone Emulation Mode.{C_RESET}")

        client_buf = bytearray()
        target_buf = bytearray()
        
        # Pending requests awaiting server response: session -> (tag, proto_name, req_type, req_dict, timestamp)
        pending_requests = {}
        pending_lock = threading.Lock()

        def parse_packets(buf: bytearray):
            packets = []
            while len(buf) >= 2:
                pkt_len = (buf[0] << 8) | buf[1]
                if pkt_len <= 0 or pkt_len > 65535:
                    # Invalid framing, drop byte
                    buf.pop(0)
                    continue
                if len(buf) < pkt_len + 2:
                    break
                # Extract full packet
                pkt_data = bytes(buf[2:pkt_len + 2])
                del buf[:pkt_len + 2]
                packets.append(pkt_data)
            return packets

        while self.running:
            rlist = [client_sock]
            if target_sock:
                rlist.append(target_sock)

            try:
                readable, _, exceptional = select.select(rlist, [], rlist, 0.5)
            except Exception:
                break

            if exceptional:
                break

            # Check for timed out pending requests on server 15678
            now = time.time()
            with pending_lock:
                timed_out = []
                for sess, info in pending_requests.items():
                    if now - info['timestamp'] > 2.5: # 2.5s timeout
                        timed_out.append(sess)
                
                for sess in timed_out:
                    info = pending_requests.pop(sess)
                    self._on_server_missing_response(client_sock, info, sess)

            for s in readable:
                if s is client_sock:
                    try:
                        data = client_sock.recv(4096)
                        if not data:
                            print(f"{C_YELLOW}[-] Client {client_ip}:{client_port} Disconnected{C_RESET}")
                            if target_sock: target_sock.close()
                            return
                        client_buf.extend(data)
                        
                        pkts = parse_packets(client_buf)
                        for pkt in pkts:
                            self._process_client_packet(pkt, client_sock, target_sock, pending_requests, pending_lock)

                    except Exception as e:
                        print(f"{C_RED}[!] Error reading from client: {e}{C_RESET}")
                        if target_sock: target_sock.close()
                        return

                elif s is target_sock:
                    try:
                        data = target_sock.recv(4096)
                        if not data:
                            print(f"{C_RED}[!] Server {self.target_host}:{self.target_port} closed connection!{C_RESET}")
                            target_sock.close()
                            target_sock = None
                            continue
                        target_buf.extend(data)
                        
                        pkts = parse_packets(target_buf)
                        for pkt in pkts:
                            self._process_server_packet(pkt, client_sock, pending_requests, pending_lock)

                    except Exception as e:
                        print(f"{C_RED}[!] Error reading from server 15678: {e}{C_RESET}")
                        if target_sock:
                            target_sock.close()
                            target_sock = None

    def _process_client_packet(self, raw_packed, client_sock, target_sock, pending_requests, pending_lock):
        """Processes a packet from the APK client."""
        try:
            unpacked = SprotoBinary.unpack(raw_packed)
            pkg, pkg_size = SprotoBinary.decode_struct(unpacked, self.db.package_schema, self.db.type_schemas)
        except Exception as e:
            print(f"{C_RED}[!] Failed to unpack client Sproto packet: {e}{C_RESET}")
            return

        tag = pkg.get('type')
        session = pkg.get('session')

        proto = self.db.get_protocol(tag) if tag is not None else None
        pname = proto['name'] if proto else f"Unknown_Tag_{tag}"
        req_type_name = f"{proto['request']}.request" if (proto and proto.get('request')) else pname
        req_schema = self.db.get_schema(req_type_name)

        req_payload, _ = SprotoBinary.decode_struct(unpacked[pkg_size:], req_schema, self.db.type_schemas)
        all_possible_responses = self.db.get_possible_responses(tag) if tag is not None else None

        print(f"\n{C_CYAN}{C_BOLD}{'='*80}{C_RESET}")
        print(f"{C_CYAN}{C_BOLD}>>> [CLIENT REQUEST DETECTED] <<<{C_RESET}")
        print(f"  * {C_BOLD}Tag ID:{C_RESET}          {C_YELLOW}{C_BOLD}{tag}{C_RESET} ({C_WHITE}{pname}{C_RESET})")
        print(f"  * {C_BOLD}Client Session:{C_RESET}  {session}")
        print(f"  * {C_BOLD}Sproto Type:{C_RESET}     {C_GREEN}SprotoType.{req_type_name}{C_RESET}")
        print(f"  * {C_BOLD}Request Fields:{C_RESET}")
        if req_payload:
            for k, v in req_payload.items():
                print(f"      {C_WHITE}{k:<18s}{C_RESET} = {C_CYAN}{repr(v)}{C_RESET}")
        else:
            print(f"      {C_DIM}(no parameters / empty request){C_RESET}")

        # Show all responses that are possible for this APK
        self._print_all_possible_responses(tag, pname, all_possible_responses)

        # Forward to target server 15678 if connected
        forwarded = False
        if target_sock:
            try:
                frame = len(raw_packed).to_bytes(2, 'big') + raw_packed
                target_sock.sendall(frame)
                forwarded = True
                print(f"  * {C_BLUE}Forwarded to Server 15678... Waiting for server response...{C_RESET}")
            except Exception as e:
                print(f"  * {C_RED}Failed to forward to Server 15678: {e}{C_RESET}")

        if session is not None and proto and proto.get('response'):
            # This request expects an RPC response
            with pending_lock:
                pending_requests[session] = {
                    'tag': tag,
                    'name': pname,
                    'req_type': req_type_name,
                    'req_payload': req_payload,
                    'rsp_info': all_possible_responses,
                    'timestamp': time.time(),
                    'forwarded': forwarded
                }
            
            if not target_sock:
                # Target server is not available, immediately trigger missing & mock response
                with pending_lock:
                    info = pending_requests.pop(session, None)
                if info:
                    self._on_server_missing_response(client_sock, info, session)
        else:
            # One-way request or notification
            print(f"  * {C_DIM}Note: Client does not expect inline RPC response session for this tag.{C_RESET}")
            if not target_sock:
                self.tracker.record_missing(tag, pname, req_type_name, req_payload, all_possible_responses, session or 0)
        
        print(f"{C_CYAN}{C_BOLD}{'='*80}{C_RESET}\n")

    def _process_server_packet(self, raw_packed, client_sock, pending_requests, pending_lock):
        """Processes a packet from server 15678."""
        try:
            unpacked = SprotoBinary.unpack(raw_packed)
            pkg, pkg_size = SprotoBinary.decode_struct(unpacked, self.db.package_schema, self.db.type_schemas)
        except Exception as e:
            print(f"{C_RED}[!] Failed to unpack server 15678 packet: {e}{C_RESET}")
            return

        session = pkg.get('session')
        tag = pkg.get('type')

        matched_req = None
        if session is not None:
            with pending_lock:
                matched_req = pending_requests.pop(session, None)

        if matched_req:
            self.tracker.record_handled(matched_req['tag'])
            print(f"\n{C_GREEN}{C_BOLD}[SERVER 15678 RESPONDED]{C_RESET} for Session {session} (Tag {matched_req['tag']}: {matched_req['name']})")
        elif tag is not None:
            proto = self.db.get_protocol(tag)
            pname = proto['name'] if proto else f"tag_{tag}"
            print(f"\n{C_GREEN}{C_BOLD}[SERVER 15678 PUSH NOTIFICATION]{C_RESET} Tag {tag}: {pname}")

        # Forward server packet to client
        try:
            frame = len(raw_packed).to_bytes(2, 'big') + raw_packed
            client_sock.sendall(frame)
        except Exception as e:
            print(f"{C_RED}[!] Failed to forward server response to client: {e}{C_RESET}")

    def _on_server_missing_response(self, client_sock, req_info, session):
        """Called when server 15678 does NOT respond or is missing the response."""
        tag = req_info['tag']
        pname = req_info['name']
        req_type = req_info['req_type']
        req_payload = req_info['req_payload']
        rsp_info = req_info['rsp_info']

        self.tracker.record_missing(tag, pname, req_type, req_payload, rsp_info, session)

        print(f"\n{C_RED}{C_BOLD}{'!'*80}{C_RESET}")
        print(f"{C_RED}{C_BOLD}>>> [MISSING FROM SERVER 15678] <<< {C_RESET}")
        print(f"{C_RED}{C_BOLD}SERVER 15678 DID NOT RESPOND TO CLIENT REQUEST!{C_RESET}")
        print(f"  * {C_BOLD}Requested Tag:{C_RESET}    {C_YELLOW}{C_BOLD}{tag}{C_RESET} ({pname})")
        print(f"  * {C_BOLD}Session ID:{C_RESET}       {session}")
        print(f"  * {C_BOLD}Sproto Type:{C_RESET}      {req_type}")
        print(f"  * {C_BOLD}Client Payload:{C_RESET}   {req_payload}")
        print(f"{C_RED}{C_BOLD}{'-'*80}{C_RESET}")
        
        rpc_rsp = rsp_info.get('rpc_response') if rsp_info else None
        if rpc_rsp:
            print(f"  {C_YELLOW}{C_BOLD}>>> WHAT SERVER 15678 MUST SEND TO SATISFY THIS APK: <<<{C_RESET}")
            print(f"  * {C_BOLD}Expected Sproto Response:{C_RESET} {C_GREEN}{rpc_rsp['full_type']}{C_RESET}")
            schema = rpc_rsp.get('schema')
            if schema and schema.get('fields'):
                print(f"  * {C_BOLD}Response Fields Needed by APK:{C_RESET}")
                for ftag, finfo in schema['fields'].items():
                    print(f"      [{ftag}] {finfo['name']:<18s} : {C_CYAN}{finfo['type']}{C_RESET}")
            sample = rpc_rsp.get('sample', {})
            print(f"  * {C_BOLD}Sample Working Response Payload:{C_RESET}")
            print(f"      {C_GREEN}{json.dumps(sample)}{C_RESET}")
        
        # Send mock response if auto_mock is enabled so client can proceed!
        if self.auto_mock and rpc_rsp:
            try:
                mock_payload = rpc_rsp.get('sample', {})
                rsp_schema = rpc_rsp.get('schema')
                
                # 1. Encode response package (session only, no type)
                pkg_data = {'session': session}
                pkg_bytes = SprotoBinary.encode_struct(pkg_data, self.db.package_schema, self.db.type_schemas)
                
                # 2. Encode mock response body
                body_bytes = SprotoBinary.encode_struct(mock_payload, rsp_schema, self.db.type_schemas)
                
                # 3. Combine and pack
                combined = pkg_bytes + body_bytes
                packed = SprotoBinary.pack(combined)
                frame = len(packed).to_bytes(2, 'big') + packed
                
                client_sock.sendall(frame)
                print(f"  {C_MAGENTA}{C_BOLD}[AUTO-MOCK SENT]{C_RESET} Sent synthesized response {rpc_rsp['name']} to client!")
                print(f"  {C_MAGENTA}-> APK client unblocked and proceeding to next request...{C_RESET}")
            except Exception as e:
                print(f"  {C_RED}[!] Failed to send mock response: {e}{C_RESET}")

        print(f"{C_RED}{C_BOLD}{'!'*80}{C_RESET}\n")

    def _print_all_possible_responses(self, tag, pname, rsp_info):
        """Displays all responses that are possible for this APK."""
        if not rsp_info:
            return

        rpc_rsp = rsp_info.get('rpc_response')
        pushes = rsp_info.get('related_pushes', [])

        print(f"\n  {C_GREEN}{C_BOLD}[ALL POSSIBLE RESPONSES FOR THIS APK]{C_RESET}")
        if rpc_rsp:
            print(f"  {C_BOLD}1. Inline RPC Response:{C_RESET}")
            print(f"     * Type: {C_GREEN}{rpc_rsp['full_type']}{C_RESET}")
            schema = rpc_rsp.get('schema')
            if schema and schema.get('fields'):
                for ftag, finfo in schema['fields'].items():
                    print(f"       [{ftag}] {finfo['name']:<18s} ({finfo['type']})")
            print(f"     * Sample JSON: {C_DIM}{json.dumps(rpc_rsp.get('sample', {}))}{C_RESET}")
        else:
            print(f"  {C_DIM}1. Inline RPC Response: None (client doesn't wait for RPC return session){C_RESET}")

        if pushes:
            print(f"  {C_BOLD}2. Triggered Server Push Notifications (APK NetReceiver Handlers):{C_RESET}")
            for p in pushes:
                print(f"     * Tag {p['tag']}: {C_CYAN}{p['name']}{C_RESET} ({p['full_type']})")
                pschema = p.get('schema')
                if pschema and pschema.get('fields'):
                    for ftag, finfo in pschema['fields'].items():
                        print(f"         [{ftag}] {finfo['name']:<16s} ({finfo['type']})")


# ==============================================================================
# 5. CLI Catalog Inspection & Export Commands
# ==============================================================================

def cmd_dump_all(db: APKSchemaDB, outfile='apk_protocol_reference.json'):
    """Dumps all 409 protocols and their responses to a clean JSON file."""
    catalog = []
    for tag, p in sorted(db.protocols.items()):
        req_schema_name = f"{p['request']}.request" if p['request'] else None
        req_schema = db.get_schema(req_schema_name) if req_schema_name else None
        
        rsp_schema_name = f"{p['response']}.response" if p['response'] else None
        rsp_schema = db.get_schema(rsp_schema_name) if rsp_schema_name else None
        
        entry = {
            'tag': tag,
            'protocol_name': p['name'],
            'client_request': {
                'sproto_type': req_schema_name,
                'fields': list(req_schema['fields'].values()) if req_schema else []
            },
            'server_response': {
                'sproto_type': rsp_schema_name,
                'fields': list(rsp_schema['fields'].values()) if rsp_schema else [],
                'sample': db.generate_default_sample(rsp_schema) if rsp_schema else None
            },
            'is_push_handler': p['name'] in db.push_handlers,
            'push_handler_func': db.push_handlers.get(p['name'])
        }
        catalog.append(entry)

    with open(outfile, 'w', encoding='utf-8') as f:
        json.dump(catalog, f, indent=2)

    print(f"{C_GREEN}{C_BOLD}[+] Exported {len(catalog)} protocols to '{outfile}'!{C_RESET}")
    print(f" Total protocols with direct response: {len([c for c in catalog if c['server_response']['sproto_type']])}")
    print(f" Total server push notifications:      {len([c for c in catalog if c['is_push_handler']])}")

def cmd_show_responses(db: APKSchemaDB):
    """Prints all possible responses in the APK."""
    print(f"\n{C_GREEN}{C_BOLD}{'='*80}{C_RESET}")
    print(f"{C_GREEN}{C_BOLD}   ALL POSSIBLE RESPONSES IN THIS APK ({len(db.protocols)} TOTAL PROTOCOLS){C_RESET}")
    print(f"{C_GREEN}{C_BOLD}{'='*80}{C_RESET}\n")

    print(f"{C_YELLOW}{C_BOLD}--- PART 1: DIRECT RPC RESPONSES (Client Waits For Session Reply) ---{C_RESET}")
    for tag, p in sorted(db.protocols.items()):
        if p['response']:
            rsp_key = f"{p['response']}.response"
            schema = db.get_schema(rsp_key)
            print(f"\n{C_CYAN}{C_BOLD}Tag {tag:>3d}: Protocol '{p['name']}'{C_RESET}")
            print(f"  * Request:  {p['request']}.request")
            print(f"  * Response: {C_GREEN}{rsp_key}{C_RESET}")
            if schema and schema.get('fields'):
                print(f"  * Fields:")
                for ftag, finfo in schema['fields'].items():
                    print(f"      [{ftag}] {finfo['name']:<18s} : {finfo['type']}")
            print(f"  * Sample:   {json.dumps(db.generate_default_sample(schema))}")

    print(f"\n\n{C_YELLOW}{C_BOLD}--- PART 2: SERVER PUSH NOTIFICATIONS / EVENT RESPONSES ({len(db.push_handlers)} TOTAL) ---{C_RESET}")
    for name, handler in sorted(db.push_handlers.items()):
        tag = db.name_to_tag.get(name, -1)
        proto = db.get_protocol(tag)
        req_key = f"{proto['request']}.request" if (proto and proto.get('request')) else name
        schema = db.get_schema(req_key)
        print(f" * Tag {tag:>3d} | {C_WHITE}{name:<32s}{C_RESET} -> {C_CYAN}{handler}{C_RESET}")
    print()

def cmd_search(db: APKSchemaDB, query: str):
    """Searches protocols, tags, and sproto types."""
    query = query.lower()
    matches = []
    for tag, p in db.protocols.items():
        if query in str(tag) or query in p['name'].lower() or (p['request'] and query in p['request'].lower()) or (p['response'] and query in p['response'].lower()):
            matches.append((tag, p))

    print(f"\n{C_BOLD}Search Results for '{query}' ({len(matches)} matches):{C_RESET}")
    for tag, p in sorted(matches, key=lambda x: x[0]):
        print(f" Tag {tag:>3d}: {C_CYAN}{p['name']:<28s}{C_RESET} | Req: {p['request']} | Rsp: {p['response']}")
        if p['response']:
            schema = db.get_schema(f"{p['response']}.response")
            if schema and schema.get('fields'):
                field_strs = [f"{f['name']}({f['type']})" for f in schema['fields'].values()]
                print(f"          -> Rsp Fields: {', '.join(field_strs)}")
    print()


# ==============================================================================
# 6. Main Entry Point
# ==============================================================================

def main():
    parser = argparse.ArgumentParser(
        description="APK Sproto Protocol Inspector & Server 15678 Missing Feature Detector",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python 9555.py                        # Run proxy on port 9555, forwarding to s16.serv00.com:15678
  python 9555.py --server               # Run standalone emulator on port 9555
  python 9555.py --target 127.0.0.1:15678 # Forward to a local server on port 15678
  python 9555.py --responses            # List all possible responses for this APK
  python 9555.py --dump                 # Export full APK protocol dictionary to JSON
  python 9555.py --search login         # Search for 'login' protocols and schemas
        """
    )
    parser.add_argument('-p', '--port', type=int, default=9555, help="Port to listen on (default: 9555)")
    parser.add_argument('-b', '--bind', type=str, default='0.0.0.0', help="IP address to bind on (default: 0.0.0.0)")
    parser.add_argument('-t', '--target', type=str, default='s16.serv00.com:15678', help="Target server address host:port (default: s16.serv00.com:15678)")
    parser.add_argument('--server', action='store_true', help="Run standalone server without forwarding to 15678")
    parser.add_argument('--no-mock', action='store_true', help="Do NOT synthesize mock responses for missing tags")
    parser.add_argument('--responses', action='store_true', help="Print all possible responses for this APK and exit")
    parser.add_argument('--dump', action='store_true', help="Dump full protocol & response catalog to JSON and exit")
    parser.add_argument('--search', type=str, default=None, help="Search protocols, tags, or sproto types")

    args = parser.parse_args()

    # Load APK Database
    db = APKSchemaDB()

    if args.responses:
        cmd_show_responses(db)
        return

    if args.dump:
        cmd_dump_all(db)
        return

    if args.search:
        cmd_search(db, args.search)
        return

    # Parse target
    if args.server:
        target_host = None
        target_port = None
    else:
        if ':' in args.target:
            target_host, port_str = args.target.split(':', 1)
            target_port = int(port_str)
        else:
            target_host = args.target
            target_port = 15678

    proxy = InspectorProxy(
        listen_host=args.bind,
        listen_port=args.port,
        target_host=target_host or '127.0.0.1',
        target_port=target_port or 15678,
        auto_mock=(not args.no_mock),
        db=db
    )

    if args.server:
        proxy.target_host = None
        proxy.target_port = None

    proxy.start()

if __name__ == '__main__':
    main()
