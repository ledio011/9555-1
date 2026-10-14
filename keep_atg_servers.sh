cat > "$HOME/keep_atg_servers.sh" <<'EOF'
#!/bin/sh

HOME="/home/ledio01"
USER_NAME="ledio01"
PATH="/bin:/usr/bin:/usr/local/bin"
export PATH

WATCHLOG="$HOME/keep_atg_servers.log"
LOCK="$HOME/.keep_atg_servers.lock"

# Prevent overlapping watchdog runs.
if ! mkdir "$LOCK" 2>/dev/null; then
    exit 0
fi
trap 'rmdir "$LOCK" 2>/dev/null' 0
trap 'exit 0' HUP INT TERM

log() {
    echo "$(date) $*" >> "$WATCHLOG"
}

# Fail closed if sockstat cannot return a valid table.
load_sockets() {
    SOCKETS=$(/usr/bin/sockstat -4 -l 2>>"$WATCHLOG") || return 1
    case "$SOCKETS" in
        *"LOCAL ADDRESS"*) return 0 ;;
        *) return 1 ;;
    esac
}

port_pids() {
    printf '%s\n' "$SOCKETS" |
        /usr/bin/awk -v p=":$1" \
        'NR > 1 && substr($6, length($6)-length(p)+1) == p {
            print $3
        }'
}

# Find matching server processes and their elapsed time in seconds.
script_processes() {
    script="$1"
    ps_output=$(/bin/ps -U "$USER_NAME" \
        -o pid=,etime=,command= 2>>"$WATCHLOG") || return 1

    printf '%s\n' "$ps_output" | /usr/bin/awk -v s="$script" '
        function elapsed(t, a, n, d, x) {
            d = 0
            if (index(t, "-")) {
                n = split(t, x, "-")
                d = x[1] + 0
                t = x[2]
            }
            n = split(t, a, ":")
            if (n == 3)
                return d*86400 + a[1]*3600 + a[2]*60 + a[3]
            if (n == 2)
                return d*86400 + a[1]*60 + a[2]
            return d*86400 + (a[1] + 0)
        }

        ($3 == "python3" ||
         $3 ~ /^python3\.[0-9]+$/ ||
         $3 ~ /\/python3(\.[0-9]+)?$/) &&
        $4 == "-u" &&
        ($5 == s || $5 ~ ("/" s "$")) {
            print $1, elapsed($2)
        }
    '
}

ensure_server() {
    port="$1"
    directory="$2"
    script="$3"
    logfile="$4"
    label="$5"

    load_sockets
    status=$?
    if [ "$status" -ne 0 ]; then
        log "$label ERROR: sockstat failed; no action taken"
        return
    fi

    pids=$(port_pids "$port")

    # Listener(s) exist: validate every owner before removing duplicates.
    if [ -n "$pids" ]; then
        for pid in $pids; do
            cmd=$(/bin/ps -p "$pid" -o command= 2>/dev/null) || cmd=""
            case "$cmd" in
                *"$script"*) ;;
                *)
                    log "$label WARNING: port $port has unrecognized PID=$pid; leaving it alone"
                    return
                    ;;
            esac
        done

        keeper=""
        for pid in $pids; do
            if [ -z "$keeper" ]; then
                keeper="$pid"
            else
                if kill -TERM "$pid" 2>/dev/null; then
                    log "$label duplicate PID=$pid sent TERM; keeping PID=$keeper on port $port"
                fi
            fi
        done
        return
    fi

    # No listener. Check whether the server is still starting or is stale.
    rows=$(script_processes "$script") || {
        log "$label ERROR: ps failed; refusing to launch another copy"
        return
    }

    young=$(printf '%s\n' "$rows" |
        /usr/bin/awk '$2 < 120 { print $1 }')
    stale=$(printf '%s\n' "$rows" |
        /usr/bin/awk '$2 >= 120 { print $1 }')

    if [ -n "$young" ]; then
        log "$label PID(s) $young have no listener yet and are under 120s old; waiting"
        return
    fi

    if [ -n "$stale" ]; then
        for pid in $stale; do
            cmd=$(/bin/ps -p "$pid" -o command= 2>/dev/null) || cmd=""
            case "$cmd" in
                *"$script"*)
                    kill -TERM "$pid" 2>/dev/null &&
                        log "$label stale PID=$pid sent TERM; will recheck next run"
                    ;;
                *)
                    log "$label PID=$pid no longer matches; did not signal"
                    ;;
            esac
        done
        return
    fi

    cd "$directory" || {
        log "$label ERROR: cannot enter $directory"
        return
    }

    nohup python3 -u "$script" \
        >> "$logfile" 2>&1 </dev/null &
    pid=$!
    log "$label started PID=$pid; waiting for listener"
}

ensure_server 9777 "$HOME/9777" main.py \
    "$HOME/9777/login.log" LOGIN

ensure_server 15678 "$HOME/9555-1" 9555.py \
    "$HOME/9555-1/game.log" GAME
EOF

chmod +x "$HOME/keep_atg_servers.sh"
