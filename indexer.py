"""Generic mixed-format source indexer. Protocol data is extracted, never hardcoded."""
from pathlib import Path
import hashlib,json,re,sys
TEXT_EXT={".cs",".json",".xml",".txt",".shader",".prefab",".unity",".proto",".sproto"}
MAX_SCAN=16*1024*1024
def digest(p):
    h=hashlib.sha256()
    with p.open("rb") as f:
        for c in iter(lambda:f.read(1024*1024),b""): h.update(c)
    return h.hexdigest()
def strings(data,n=4):
    a=[m.group().decode("ascii","ignore") for m in re.finditer(rb"[ -~]{%d,}"%n,data)]
    u=[]
    for m in re.finditer(rb"(?:[ -~]\x00){%d,}"%n,data):
        try:u.append(m.group().decode("utf-16le","ignore"))
        except UnicodeDecodeError:pass
    return list(dict.fromkeys(a+u))
def candidates(text):
    return [{"name":m.group(1),"value":int(m.group(2)),"context":"source"} for m in re.finditer(r"(?im)\b([A-Za-z_]\w*)\s+(\d{1,6})\s*[:{]",text)]
def analyze(source,out):
    source=Path(source).resolve(); out=Path(out).resolve()
    (out/"protocol").mkdir(parents=True,exist_ok=True); (out/"text").mkdir(parents=True,exist_ok=True); (out/"strings").mkdir(parents=True,exist_ok=True)
    records=[]; numeric={}
    for p in source.rglob("*"):
        if not p.is_file(): continue
        try:size=p.stat().st_size; sha=digest(p); data=p.read_bytes() if size<=MAX_SCAN else b""
        except OSError: continue
        text=data.decode("utf-8","ignore") if p.suffix.lower() in TEXT_EXT else None
        rec={"path":str(p),"relative_path":p.relative_to(source).as_posix(),"name":p.name,"extension":p.suffix.lower(),"size":size,"sha256":sha,"type":"TEXT" if text is not None else "BINARY","metadata":{},"strings_file":None,"text_file":None,"protocol_candidates":[]}
        if text is not None:
            q=out/"text"/f"{sha}.txt"; q.write_text(text,encoding="utf-8"); rec["text_file"]=str(q); rec["protocol_candidates"]=candidates(text)
        ss=strings(data)
        if ss:
            q=out/"strings"/f"{sha}.strings.txt"; q.write_text("\n".join(ss),encoding="utf-8"); rec["strings_file"]=str(q)
        records.append(rec)
        for x in rec["protocol_candidates"]: numeric.setdefault(str(x["value"]),[]).append({"source":rec["relative_path"],**x})
    (out/"index.json").write_text(json.dumps({"index_version":1,"source":str(source),"file_count":len(records),"files":records},indent=2),encoding="utf-8")
    for n,v in {"sproto_protocols":{},"sproto_types":{},"numeric_relations":numeric,"classes":{},"methods":{},"fields":{}}.items():
        (out/"protocol"/f"{n}.json").write_text(json.dumps(v,indent=2),encoding="utf-8")
    return records
if __name__=="__main__":
    if len(sys.argv)!=3: raise SystemExit("usage: python indexer.py <source> <output>")
    print("indexed",len(analyze(sys.argv[1],sys.argv[2])),"files")
