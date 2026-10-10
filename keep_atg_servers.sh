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

listener_up() {
    sockstat -4 -l |
        awk -v p=":$1" '$6 ~ (p "$") {found=1}
        END {exit !found}'
}

ensure_server() {
    port="$1"
    directory="$2"
    script="$3"
    logfile="$4"
    label="$5"

    if listener_up "$port"; then
        return
    fi

    echo "$(date) $label port $port missing" >> "$WATCHLOG"

    # Stop only stale copies of this particular server.
    pids=$(ps -U "$USER_NAME" -o pid=,command= |
        awk -v s="$script" \
        '$2=="python3" && $3=="-u" &&
        ($4==s || $4 ~ ("/" s "$")) {print $1}')

    for pid in $pids; do
        kill "$pid" 2>/dev/null
    done

    sleep 2
    cd "$directory" || return

    nohup python3 -u "$script" \
        >> "$logfile" 2>&1 </dev/null &

    pid=$!
    sleep 2

    if listener_up "$port"; then
        echo "$(date) $label listening; pid=$pid" >> "$WATCHLOG"
    else
        echo "$(date) $label failed to listen; pid=$pid" >> "$WATCHLOG"
    fi
}

ensure_server 9777 "$HOME/9777" main.py \
    "$HOME/9777/login.log" LOGIN

ensure_server 15678 "$HOME/9555-1" 9555.py \
    "$HOME/9555-1/game.log" GAME
EOF

chmod +x "$HOME/keep_atg_servers.sh"
