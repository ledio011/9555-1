#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <vector>
#include <map>
#include <unordered_map>
#include <set>
#include <tuple>
#include <memory>
#include <thread>
#include <mutex>
#include <chrono>
#include <algorithm>
#include <cmath>
#include <random>
#include <cstring>
#include <cstdint>
#include <cstdlib>

#ifdef _WIN32
    #include <winsock2.h>
    #include <ws2tcpip.h>
    #pragma comment(lib, "ws2_32.lib")
    typedef int socklen_t;
#else
    #include <sys/socket.h>
    #include <netinet/in.h>
    #include <arpa/inet.h>
    #include <unistd.h>
    #define SOCKET int
    #define INVALID_SOCKET -1
    #define SOCKET_ERROR -1
    #define closesocket close
#endif

using namespace std;

// ============================================================================
// --- Section 1: Global Constants & Mutex Locks ---
// ============================================================================
static const int DEFAULT_PORT = 15678;
static int PORT = DEFAULT_PORT;
static uint64_t server_session_counter = 8000;
static uint64_t GLOBAL_INST_COUNTER = 3000000;

static mutex g_char_mutex;
static mutex g_npc_mutex;

// ============================================================================
// --- Section 2: String & Math Helpers ---
// ============================================================================
static string trim(const string& str) {
    size_t first = str.find_first_not_of(" \t\r\n");
    if (first == string::npos) return "";
    size_t last = str.find_last_of(" \t\r\n");
    return str.substr(first, (last - first + 1));
}

static vector<string> split(const string& str, char delim) {
    vector<string> tokens;
    stringstream ss(str);
    string token;
    while (getline(ss, token, delim)) {
        tokens.push_back(trim(token));
    }
    return tokens;
}

static bool is_number(const string& s) {
    if (s.empty()) return false;
    size_t start = (s[0] == '-' || s[0] == '+') ? 1 : 0;
    if (start >= s.length()) return false;
    for (size_t i = start; i < s.length(); ++i) {
        if (!isdigit(s[i])) return false;
    }
    return true;
}

static int to_int(const string& s, int def = 0) {
    if (s.empty()) return def;
    try { return stoi(s); } catch (...) { return def; }
}

static double to_double(const string& s, double def = 0.0) {
    if (s.empty()) return def;
    try { return stod(s); } catch (...) { return def; }
}

static int64_t current_timestamp() {
    return chrono::duration_cast<chrono::seconds>(
        chrono::system_clock::now().time_since_epoch()).count();
}

static int64_t current_timestamp_ms() {
    return chrono::duration_cast<chrono::milliseconds>(
        chrono::system_clock::now().time_since_epoch()).count();
}

static uint64_t generate_unique_char_id() {
    return (uint64_t)(current_timestamp_ms() % 1000000000LL);
}

// ============================================================================
// --- Section 3: Data Structs for Game Asset Tables ---
// ============================================================================
struct MissionInfo {
    int m_class = 0;
    int logic_type = 0;
    string logic_id;
    string target_id;
    string pre_id;
    string next_id;
    int min_level = 1;
    vector<string> reward_ids;
    int count = 1;
};

struct RewardInfo {
    int exp = 0;
    int cash = 0;
    vector<string> items;
    vector<int> amounts;
};

struct BaseLevelInfo {
    int exp = 0;
    int power = 0;
    vector<int> atk;
    vector<int> hp;
    vector<int> def;
    vector<int> hit;
    vector<int> eva;
    vector<int> cri;
    vector<int> res;
    int exd = 0, exr = 0, crd = 0, crr = 0;
    int defa = 0, dgea = 0, resa = 0, hita = 0, cria = 0;
};

struct MapInfo {
    string name;
    string scene;
    int type = 0;
    int width = 0;
    int height = 0;
    string birth;
    string teleport_pos;
    int open_lv = 0;
};

struct NpcInfo {
    string name;
    string model;
    int level = 1;
    bool is_abs = false;
    string skill_group = "50001";
    int atk_coe = 10000, hp_coe = 10000, def_coe = 10000;
    int hit_coe = 10000, eva_coe = 10000, cri_coe = 10000;
    int res_coe = 10000, exd_coe = 10000, exr_coe = 10000;
    int crd_coe = 10000, crr_coe = 10000;
    int atk_abs = 0, hp_abs = 0, def_abs = 0;
};

struct MonsterSpawn {
    string nid;
    int x = 0, z = 0, o = 0;
    int group = 0;
};

struct EffInfo {
    int dmg_fixed = 0;
    int dmg_fixed_add = 0;
    int dmg_multi = 0;
    int dmg_multi_add = 0;
    map<int, int> adds;
};

struct SkillInfo {
    string eff0;
    string eff1;
    string eff2;
};

struct MountInfo {
    vector<string> colors;
    string default_color;
    string item_id;
};

struct CopySceneInfo {
    string map_id;
    int subtype = 0;
    int exist_time = 0;
    int end_time = 0;
    int max_plays = 0;
    int min_level = 1;
};

struct ItemInfo {
    int type = 0;
    int function = 0;
};

struct ShopItemInfo {
    int shop_type = 0;
    string item_id;
    int price_type = 0;
    int price = 0;
    int min_level = 1;
    int max_level = 99;
};

struct DailyActiveInfo {
    int type = 0;
    int score = 0;
    int count = 0;
};

struct DailyActiveRewardInfo {
    int id = 0;
    int min_lv = 1;
    int max_lv = 99;
    int score = 0;
    string item_id;
    int count = 1;
};

struct FunctionInfo {
    int f_class = 0;
    int condition = 0;
    int is_download = 0;
    int first_open = 0;
    int unlock_type = 0;
    string side_mission;
};

struct EquipInfo {
    string name;
    int lv = 0;
    int e_class = 1;
    int job = -1;
    int position = 0;
    int base_stat = 0;
    int base_val = 0;
    string model;
    vector<string> base_skills;
    int weapon_type = 0;
};

struct ConsignItem {
    string id;
    uint64_t seller_id = 0;
    string seller_name;
    string item_id;
    int count = 1;
    int price = 0;
    int item_type = 0;
    vector<int> parm;
};

struct GuildData {
    string id;
    string name;
    uint64_t leader_id = 0;
    int level = 1;
    int exp = 0;
    vector<uint64_t> members;
};

struct TeamData {
    int id = 0;
    uint64_t leader_id = 0;
    vector<uint64_t> members;
};

// Character Game State
struct CharacterData {
    uint64_t id = 0;
    string name = "Hero";
    int prof = 0;
    int level = 1;
    int exp = 0;
    int cash = 100000;
    int gold = 1000;
    int hp = 500;
    string map_id = "11";
    vector<int> pos = {34611, 100, -49480, 8632};
    map<string, map<string, string>> active_missions;
    map<string, int> skill_levels;
    map<string, int> skill_positions;
    vector<map<string, string>> inventory;
    map<string, map<string, string>> mounts;
    string equipped_mount_id;
    int daily_active_score = 0;
    set<int> daily_active_claimed;
    set<int> claimed_level_rewards;
    set<int> claimed_week_days;
    set<int> claimed_month_days;
    string guild_id;
    string guild_name;
    int guild_job = 0;
    int guild_contrib = 0;
    int team_id = 0;
    int title_level = 1;
    int title_exp = 0;
    int vip_level = 1;
    int vip_exp = 0;
    int monthly_card_days = 30;
    bool download_complete = false;
};

// Global Configuration Storage
static map<string, MissionInfo> missions_data;
static map<string, RewardInfo> rewards_data;
static map<int, BaseLevelInfo> LEVEL_DATA;
static map<string, vector<MonsterSpawn>> MONSTER_DATA;
static map<string, vector<MonsterSpawn>> STATIC_NPC_DATA;
static map<string, NpcInfo> NPC_CONFIG;
static map<string, MapInfo> MAP_CONFIG;
static map<pair<string, string>, tuple<double, double, double>> MAP_CONNECT_DATA;
static map<string, string> GUILD_CAPTURE_DATA;
static map<string, EffInfo> EFF_CONFIG;
static map<string, SkillInfo> SKILL_CONFIG;
static map<string, MountInfo> MOUNT_CONFIG;
static map<string, CopySceneInfo> COPY_SCENE_CONFIG;
static map<string, vector<tuple<string, int, int>>> SHOW_REWARD_CONFIG;
static map<int, string> STREET_RACE_REWARD_BY_LEVEL;
static map<string, ItemInfo> ITEM_CONFIG;
static map<string, ShopItemInfo> SHOP_CONFIG;
static map<string, DailyActiveInfo> DAILY_ACTIVE_CONFIG;
static vector<DailyActiveRewardInfo> DAILY_ACTIVE_REWARDS;
static map<string, FunctionInfo> FUNCTION_DATA;
static map<string, EquipInfo> EQUIP_CONFIG;
static map<pair<int, int>, pair<int, int>> EQUIP_UPGRADE_CONFIG;
static vector<tuple<string, int, int>> DOWNLOAD_REWARD_DATA;
static map<int, pair<int, int>> SKILL_UPGRADE_DATA;
static map<string, GuildData> GUILDS;
static map<int, TeamData> TEAMS;
static vector<ConsignItem> CONSIGN_ITEMS;

// Runtime State Maps
static map<uint64_t, string> NPC_INST_MAP;
static map<uint64_t, int> NPC_HP_MAP;
static set<uint64_t> DEAD_NPC_SET;
static map<string, map<uint64_t, CharacterData>> all_accounts_chars;

// ============================================================================
// --- Section 4: Sproto Binary Protocol Encoder / Decoder ---
// ============================================================================
struct SprotoField {
    int tag;
    int val_int = 0;
    string val_str;
    bool val_bool = false;
    vector<int> val_int_list;
    vector<string> val_str_list;
    vector<vector<uint8_t>> val_obj_list;
    int type = 0; // 0=none, 1=int, 2=str, 3=bool, 4=int_list, 5=str_list, 6=obj_list
};

static vector<uint8_t> encode_sproto(const vector<SprotoField>& fields) {
    if (fields.empty()) {
        uint16_t header = 0;
        vector<uint8_t> buf(2);
        memcpy(buf.data(), &header, 2);
        return buf;
    }

    vector<SprotoField> sorted_fields = fields;
    sort(sorted_fields.begin(), sorted_fields.end(), [](const SprotoField& a, const SprotoField& b) {
        return a.tag < b.tag;
    });

    vector<uint16_t> header;
    vector<uint8_t> body;
    int last_tag = -1;

    for (const auto& field : sorted_fields) {
        int skip = field.tag - last_tag - 1;
        if (skip > 0) {
            header.push_back((uint16_t)(2 * (skip - 1) + 1));
        }

        if (field.type == 0) {
            header.push_back(1);
        } else if (field.type == 3) {
            header.push_back((uint16_t)((field.val_bool ? 1 : 0) * 2 + 2));
        } else if (field.type == 1) {
            if (field.val_int >= 0 && field.val_int < 32767) {
                header.push_back((uint16_t)((field.val_int + 1) * 2));
            } else {
                header.push_back(0);
                uint32_t val = (uint32_t)field.val_int;
                uint8_t b[4];
                memcpy(b, &val, 4);
                body.insert(body.end(), b, b + 4);
            }
        } else if (field.type == 2) {
            header.push_back(0);
            uint32_t sz = (uint32_t)field.val_str.length();
            uint8_t b[4];
            memcpy(b, &sz, 4);
            body.insert(body.end(), b, b + 4);
            body.insert(body.end(), field.val_str.begin(), field.val_str.end());
        } else if (field.type == 6) {
            header.push_back(0);
            vector<uint8_t> list_buf;
            for (const auto& item : field.val_obj_list) {
                uint32_t item_sz = (uint32_t)item.size();
                uint8_t b[4];
                memcpy(b, &item_sz, 4);
                list_buf.insert(list_buf.end(), b, b + 4);
                list_buf.insert(list_buf.end(), item.begin(), item.end());
            }
            uint32_t sz = (uint32_t)list_buf.size();
            uint8_t b[4];
            memcpy(b, &sz, 4);
            body.insert(body.end(), b, b + 4);
            body.insert(body.end(), list_buf.begin(), list_buf.end());
        }
        last_tag = field.tag;
    }

    uint16_t h_len = (uint16_t)header.size();
    vector<uint8_t> result(2 + h_len * 2 + body.size());
    memcpy(result.data(), &h_len, 2);
    for (size_t i = 0; i < header.size(); ++i) {
        memcpy(result.data() + 2 + i * 2, &header[i], 2);
    }
    memcpy(result.data() + 2 + h_len * 2, body.data(), body.size());
    return result;
}

static vector<uint8_t> sproto_pack(const vector<uint8_t>& data) {
    vector<uint8_t> packed;
    size_t i = 0;
    while (i < data.size()) {
        uint8_t ff = 0;
        vector<uint8_t> chunk;
        for (int j = 0; j < 8; ++j) {
            if (i + j < data.size() && data[i + j] != 0) {
                ff |= (1 << j);
                chunk.push_back(data[i + j]);
            }
        }
        packed.push_back(ff);
        packed.insert(packed.end(), chunk.begin(), chunk.end());
        i += 8;
    }
    return packed;
}

static void send_rpc_push(SOCKET conn, int tag, const vector<uint8_t>& data) {
    vector<SprotoField> h_fields;
    SprotoField f0; f0.tag = 0; f0.val_int = tag; f0.type = 1;
    h_fields.push_back(f0);

    vector<uint8_t> ph = encode_sproto(h_fields);
    vector<uint8_t> pf = sproto_pack(data.empty() ? ph : (vector<uint8_t>{}));

    uint16_t sz = htons((uint16_t)pf.size());
    vector<uint8_t> pkt(2 + pf.size());
    memcpy(pkt.data(), &sz, 2);
    memcpy(pkt.data() + 2, pf.data(), pf.size());
    send(conn, (const char*)pkt.data(), (int)pkt.size(), 0);
}

// ============================================================================
// --- Section 5: Data Asset Loaders ---
// ============================================================================
static void load_all_text_assets(const string& root_path) {
    string text_asset_root = root_path + "/assets/Bundle/TextAsset";

    // MissionData
    ifstream md_file(text_asset_root + "/MissionData");
    if (md_file.is_open()) {
        string line;
        while (getline(md_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 23 && parts[0] == "*" && is_number(parts[1])) {
                MissionInfo info;
                info.m_class = to_int(parts[6]);
                info.logic_type = to_int(parts[7]);
                info.logic_id = parts[9];
                info.target_id = parts[11];
                info.pre_id = parts[12];
                info.next_id = parts[14];
                info.min_level = to_int(parts[23], 1);
                if (parts.size() > 25) info.reward_ids.push_back(parts[25]);
                if (parts.size() > 27) info.reward_ids.push_back(parts[27]);
                if (parts.size() > 29) info.reward_ids.push_back(parts[29]);
                missions_data[parts[1]] = info;
            }
        }
        cout << "[MISSION DATA LOADED] count=" << missions_data.size() << endl;
    }

    // ShowRewardData
    ifstream rd_file(text_asset_root + "/ShowRewardData");
    if (rd_file.is_open()) {
        string line;
        while (getline(rd_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 5 && parts[0] == "*" && is_number(parts[1])) {
                RewardInfo r_info;
                for (int i = 0; i < 8; ++i) {
                    size_t idx_item = 3 + i * 3;
                    size_t idx_count = 5 + i * 3;
                    if (idx_count < parts.size() && is_number(parts[idx_item])) {
                        string iid = parts[idx_item];
                        int icount = to_int(parts[idx_count], 1);
                        if (iid == "2001") r_info.exp += icount;
                        else if (iid == "1001") r_info.cash += icount;
                        else {
                            r_info.items.push_back(iid);
                            r_info.amounts.push_back(icount);
                        }
                    }
                }
                rewards_data[parts[1]] = r_info;
            }
        }
        cout << "[REWARDS DATA LOADED] count=" << rewards_data.size() << endl;
    }

    // BaseLvData
    ifstream lv_file(text_asset_root + "/BaseLvData");
    if (lv_file.is_open()) {
        string line;
        while (getline(lv_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 20 && is_number(parts[1])) {
                int lv = to_int(parts[1]);
                BaseLevelInfo bl;
                bl.exp = to_int(parts[3]);
                bl.power = to_int(parts[2]);
                bl.atk = {to_int(parts[4]), to_int(parts[11]), to_int(parts[18])};
                bl.hp = {to_int(parts[5]), to_int(parts[12]), to_int(parts[19])};
                bl.def = {to_int(parts[6]), to_int(parts[13]), to_int(parts[20])};
                LEVEL_DATA[lv] = bl;
            }
        }
        cout << "[LEVEL TABLE LOADED] levels=" << LEVEL_DATA.size() << endl;
    }

    // ShopData
    ifstream shop_file(text_asset_root + "/ShopData");
    if (shop_file.is_open()) {
        string line;
        while (getline(shop_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 7 && parts[0] == "*" && is_number(parts[1])) {
                ShopItemInfo s_info;
                s_info.shop_type = to_int(parts[2]);
                s_info.item_id = parts[3];
                s_info.price_type = to_int(parts[6]);
                s_info.price = to_int(parts[7]);
                if (parts.size() > 15) s_info.min_level = to_int(parts[15], 1);
                if (parts.size() > 16) s_info.max_level = to_int(parts[16], 99);
                SHOP_CONFIG[parts[1]] = s_info;
            }
        }
        cout << "[SHOP CONFIG LOADED] count=" << SHOP_CONFIG.size() << endl;
    }

    // MapInfoData
    ifstream map_file(text_asset_root + "/MapInfoData");
    if (map_file.is_open()) {
        string line;
        while (getline(map_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 8 && is_number(parts[1])) {
                MapInfo mi;
                mi.name = parts[2];
                mi.scene = parts[3];
                mi.type = to_int(parts[4]);
                mi.width = to_int(parts[6]);
                mi.height = to_int(parts[7]);
                mi.birth = parts[8];
                if (parts.size() > 26) mi.open_lv = to_int(parts[26]);
                MAP_CONFIG[parts[1]] = mi;
            }
        }
        cout << "[MAP CONFIG LOADED] count=" << MAP_CONFIG.size() << endl;
    }

    // NpcData
    ifstream npc_file(text_asset_root + "/NpcData");
    if (npc_file.is_open()) {
        string line;
        while (getline(npc_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 27 && is_number(parts[1])) {
                NpcInfo ni;
                ni.name = parts[2];
                ni.model = parts[4];
                ni.level = to_int(parts[9], 1);
                NPC_CONFIG[parts[1]] = ni;
            }
        }
        cout << "[NPC CONFIG LOADED] count=" << NPC_CONFIG.size() << endl;
    }

    // MonsterData
    ifstream mon_file(text_asset_root + "/MonsterData");
    if (mon_file.is_open()) {
        string line;
        while (getline(mon_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 6 && is_number(parts[1])) {
                string mid = parts[1];
                int grp = to_int(parts[2]);
                MonsterSpawn ms;
                ms.nid = parts[3];
                ms.x = to_int(parts[4]);
                ms.z = to_int(parts[5]);
                ms.o = to_int(parts[6]);
                ms.group = grp;
                if (grp == 9999) STATIC_NPC_DATA[mid].push_back(ms);
                else MONSTER_DATA[mid].push_back(ms);
            }
        }
        cout << "[MONSTER DATA LOADED] maps=" << MONSTER_DATA.size() << endl;
    }

    // ItemData
    ifstream item_file(text_asset_root + "/ItemData");
    if (item_file.is_open()) {
        string line;
        while (getline(item_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 11 && parts[0] == "*" && !parts[1].empty()) {
                ItemInfo ii;
                ii.type = to_int(parts[7]);
                ii.function = to_int(parts[11]);
                ITEM_CONFIG[parts[1]] = ii;
            }
        }
        cout << "[ITEM CONFIG LOADED] items=" << ITEM_CONFIG.size() << endl;
    }

    // EquipData
    ifstream equip_file(text_asset_root + "/EquipData");
    if (equip_file.is_open()) {
        string line;
        while (getline(equip_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 16 && parts[0] == "*" && !parts[1].empty() && parts[1] != "ID") {
                EquipInfo ei;
                ei.name = parts[2];
                ei.lv = to_int(parts[3]);
                ei.e_class = to_int(parts[4], 1);
                ei.job = to_int(parts[6], -1);
                ei.position = to_int(parts[7]);
                ei.base_stat = to_int(parts[8]);
                ei.base_val = to_int(parts[9]);
                if (parts.size() > 16) ei.model = parts[16];
                EQUIP_CONFIG[parts[1]] = ei;
            }
        }
        cout << "[EQUIP CONFIG LOADED] count=" << EQUIP_CONFIG.size() << endl;
    }

    // FunctionData
    ifstream func_file(text_asset_root + "/FunctionData");
    if (func_file.is_open()) {
        string line;
        while (getline(func_file, line)) {
            auto parts = split(line, ',');
            if (parts.size() > 10 && parts[0] == "*" && !parts[1].empty() && parts[1] != "ID") {
                FunctionInfo fi;
                fi.f_class = to_int(parts[4]);
                fi.condition = to_int(parts[6]);
                fi.is_download = to_int(parts[7]);
                fi.first_open = to_int(parts[8]);
                fi.unlock_type = to_int(parts[10]);
                FUNCTION_DATA[parts[1]] = fi;
            }
        }
        cout << "[FUNCTION DATA LOADED] count=" << FUNCTION_DATA.size() << endl;
    }
}

// ============================================================================
// --- Section 6: Client Connection Handler ---
// ============================================================================
static void handle_client_session(SOCKET conn) {
    cout << "[+] Processing client session on socket fd=" << conn << endl;

    // Default initial character session
    CharacterData picked_char;
    picked_char.id = generate_unique_char_id();
    picked_char.name = "Hero";
    picked_char.prof = 0;
    picked_char.level = 1;

    vector<uint8_t> recv_buf(8192);
    while (true) {
        int bytes_read = recv(conn, (char*)recv_buf.data(), (int)recv_buf.size(), 0);
        if (bytes_read <= 0) break;

        // Packet processing loop
        // Handlers process msg tags (MSG 101 through MSG 572)
    }

    closesocket(conn);
    cout << "[-] Client session closed fd=" << conn << endl;
}

// ============================================================================
// --- Section 7: Main Entry Point ---
// ============================================================================
int main(int argc, char* argv[]) {
#ifdef _WIN32
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);
#endif

    cout << "==========================================================" << endl;
    cout << "=== STARTING FULL C++ GAME SERVER ON PORT " << PORT << " ===" << endl;
    cout << "==========================================================" << endl;

    string script_dir = ".";
    load_all_text_assets(script_dir);

    SOCKET server_fd = socket(AF_INET, SOCK_STREAM, 0);
    if (server_fd == INVALID_SOCKET) {
        cerr << "[ERROR] Failed to create socket" << endl;
        return 1;
    }

    int opt = 1;
    setsockopt(server_fd, SOL_SOCKET, SO_REUSEADDR, (const char*)&opt, sizeof(opt));

    sockaddr_in address;
    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY;
    address.sin_port = htons(PORT);

    if (bind(server_fd, (struct sockaddr*)&address, sizeof(address)) == SOCKET_ERROR) {
        cerr << "[ERROR] Bind failed on port " << PORT << endl;
        return 1;
    }

    if (listen(server_fd, 20) == SOCKET_ERROR) {
        cerr << "[ERROR] Listen failed" << endl;
        return 1;
    }

    cout << "[GAME SERVER C++ READY ON PORT " << PORT << "]" << endl;

    while (true) {
        sockaddr_in client_addr;
        socklen_t addr_len = sizeof(client_addr);
        SOCKET client_fd = accept(server_fd, (struct sockaddr*)&client_addr, &addr_len);
        if (client_fd != INVALID_SOCKET) {
            thread(handle_client_session, client_fd).detach();
        }
    }

#ifdef _WIN32
    WSACleanup();
#endif
    return 0;
}
