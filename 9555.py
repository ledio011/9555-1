#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Auto Theft Revival - Game Server 9555
Comprehensive Diagnostic & Emulation Server
- Full client request / response logger with Sproto protocol introspection
- Complete login handoff from 9777 to 9555
- Complete character creation, selection, and world entry
- Standard gameplay mechanics (movement, missions, combat, mounts, dungeons)
- Universal RPC session auto-ack (guarantees client never hangs on unhandled packets)
"""

import socket
import struct
import threading
import random
import json
import os
import time
import traceback
import math
import sys

try:
    sys.stdout.reconfigure(line_buffering=True)
except:
    pass

# Server Configuration
PORT = int(os.environ.get("PORT", 15678))
GAME_VERSION = "1.012.017"
DATA_VERSION = "205"

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
CHAR_DB = os.path.join(SCRIPT_DIR, "characters.json")
BAK_DB = CHAR_DB + ".bak"
TMP_DB = CHAR_DB + ".tmp"
RESOURCE_ROOT = os.path.join(SCRIPT_DIR, "assets")

GLOBAL_INST_COUNTER = 3000000
NPC_INST_MAP = {}
NPC_HP_MAP = {}
NPC_SPAWNED_MAPS = {}
DEAD_NPC_SET = set()

# Load Protocol & Schema Information
# Embedded Protocol Mapping: tag -> (name, req_type, resp_type)
PROTOCOLS = {
    2: ('visitor', 'visitor.request', 'visitor.response'),
    3: ('verfiy', 'verfiy.request', 'verfiy.response'),
    4: ('login', 'login.request', 'login.response'),
    5: ('facebook_link', 'facebook_link.request', 'facebook_link.response'),
    6: ('facebook_unlink', 'facebook_unlink.request', 'facebook_unlink.response'),
    7: ('update_game_server', 'update_game_server.request', 'update_game_server.response'),
    100: ('map_ready', '', ''),
    101: ('move', 'move.request', 'move.response'),
    102: ('skill_use', 'skill_use.request', ''),
    103: ('character_list', '', 'character_list.response'),
    104: ('character_create', 'character_create.request', 'character_create.response'),
    105: ('character_pick', 'character_pick.request', 'character_pick.response'),
    106: ('enter_new_map', 'enter_new_map.request', ''),
    107: ('enter_copy_scene', 'enter_copy_scene.request', ''),
    108: ('leave_copy_scene', 'leave_copy_scene.request', ''),
    109: ('req_invite_team', 'req_invite_team.request', ''),
    110: ('ret_invite_join_team', 'ret_invite_join_team.request', ''),
    111: ('accept_damge', 'accept_damge.request', ''),
    112: ('accept_mission', 'accept_mission.request', ''),
    113: ('complete_mission', 'complete_mission.request', ''),
    114: ('abandon_mission', 'abandon_mission.request', ''),
    115: ('use_item', 'use_item.request', ''),
    116: ('equip_item', 'equip_item.request', ''),
    117: ('unequip_item', 'unequip_item.request', ''),
    118: ('request_random_name', 'request_random_name.request', 'request_random_name.response'),
    119: ('ask_pickup_item', 'ask_pickup_item.request', ''),
    120: ('chat', 'chat.request', ''),
    121: ('request_daily_mission', 'request_daily_mission.request', ''),
    122: ('send_mail', 'send_mail.request', ''),
    123: ('mail_operation', 'mail_operation.request', ''),
    124: ('add_friend', 'add_friend.request', ''),
    125: ('del_friend', 'del_friend.request', ''),
    126: ('request_update_friend_useinfo', 'request_update_friend_useinfo.request', ''),
    127: ('single_copy_scene_npc_die', 'single_copy_scene_npc_die.request', ''),
    128: ('local_character_attack', 'local_character_attack.request', ''),
    129: ('sell_item', 'sell_item.request', ''),
    130: ('skill_level_up', 'skill_level_up.request', ''),
    132: ('relife_player', 'relife_player.request', ''),
    133: ('request_random_rank_pvp_opponent', 'request_random_rank_pvp_opponent.request', ''),
    134: ('request_top_rank_pvp_list', 'request_top_rank_pvp_list.request', ''),
    135: ('select_pk_character', 'select_pk_character.request', ''),
    136: ('rank_pvp_player_attack', 'rank_pvp_player_attack.request', ''),
    137: ('rank_pvp_other_player_die', 'rank_pvp_other_player_die.request', ''),
    138: ('real_pvp_register', 'real_pvp_register.request', ''),
    139: ('request_update_storagepack', 'request_update_storagepack.request', ''),
    140: ('put_item_storagepack', 'put_item_storagepack.request', ''),
    141: ('take_item_storagepack', 'take_item_storagepack.request', ''),
    142: ('ask_character_info', 'ask_character_info.request', 'ask_character_info.response'),
    143: ('ask_shop_list', 'ask_shop_list.request', ''),
    144: ('buy_shop_item', 'buy_shop_item.request', ''),
    145: ('ask_copyscenes_info', 'ask_copyscenes_info.request', ''),
    146: ('guild_create', 'guild_create.request', ''),
    147: ('guild_join', 'guild_join.request', ''),
    148: ('guild_leave', 'guild_leave.request', ''),
    149: ('guild_kick', 'guild_kick.request', ''),
    150: ('guild_job_change', 'guild_job_change.request', ''),
    151: ('guild_skill_level', 'guild_skill_level.request', ''),
    152: ('guild_req_list', 'guild_req_list.request', ''),
    153: ('guild_req_info', 'guild_req_info.request', ''),
    154: ('guild_approve_resverve', 'guild_approve_resverve.request', ''),
    155: ('change_scene_line', 'change_scene_line.request', ''),
    156: ('title_req_level_up', 'title_req_level_up.request', ''),
    157: ('tianti_req_win_count_rewards', 'tianti_req_win_count_rewards.request', ''),
    158: ('approve_resverve_friend', 'approve_resverve_friend.request', ''),
    159: ('req_random_online_character_list', 'req_random_online_character_list.request', ''),
    160: ('search_online_character_by_name', 'search_online_character_by_name.request', ''),
    161: ('req_join_team', 'req_join_team.request', ''),
    162: ('apply_join_result', 'apply_join_result.request', ''),
    163: ('update_team_setting', 'update_team_setting.request', ''),
    164: ('team_kick', 'team_kick.request', ''),
    165: ('get_team_list', 'get_team_list.request', ''),
    166: ('leave_team', 'leave_team.request', ''),
    167: ('equip_enhance', 'equip_enhance.request', ''),
    168: ('req_offline_chat', 'req_offline_chat.request', ''),
    169: ('req_guild_notice', 'req_guild_notice.request', ''),
    170: ('req_guild_member_info', 'req_guild_member_info.request', ''),
    171: ('req_open_guild_shop', 'req_open_guild_shop.request', ''),
    172: ('req_buy_guild_goods', 'req_buy_guild_goods.request', ''),
    173: ('req_seting_guild_appro', 'req_seting_guild_appro.request', ''),
    174: ('guild_log', 'guild_log.request', ''),
    175: ('guild_donate', 'guild_donate.request', ''),
    176: ('search_guild', 'search_guild.request', ''),
    177: ('gather_team', 'gather_team.request', ''),
    178: ('update_misison_parm', 'update_misison_parm.request', ''),
    182: ('update_misison_complete', 'update_misison_complete.request', ''),
    183: ('equip_refine', 'equip_refine.request', 'equip_refine.response'),
    184: ('equip_inhert', 'equip_inhert.request', 'equip_inhert.response'),
    185: ('change_potion', 'change_potion.request', ''),
    186: ('consign_sale_item', 'consign_sale_item.request', ''),
    187: ('consign_cancel_sale', 'consign_cancel_sale.request', ''),
    188: ('consign_ask_my_items', 'consign_ask_my_items.request', ''),
    189: ('consign_ask_items_info', 'consign_ask_items_info.request', ''),
    190: ('consign_buy_item', 'consign_buy_item.request', ''),
    191: ('request_top_rank_list', 'request_top_rank_list.request', ''),
    192: ('change_skill_position', 'change_skill_position.request', ''),
    193: ('car_chase_result', 'car_chase_result.request', ''),
    194: ('enter_guild_boss_scene', 'enter_guild_boss_scene.request', ''),
    195: ('request_guild_boss', 'request_guild_boss.request', ''),
    196: ('request_guild_reward', 'request_guild_reward.request', ''),
    197: ('equip_badge', 'equip_badge.request', ''),
    198: ('unequip_badge', 'unequip_badge.request', ''),
    199: ('badge_merge', 'badge_merge.request', 'badge_merge.response'),
    200: ('request_wild_boss_info', 'request_wild_boss_info.request', ''),
    201: ('enter_wild_boss', 'enter_wild_boss.request', ''),
    202: ('request_tower_copy_info', 'request_tower_copy_info.request', ''),
    203: ('grant_tower_reward', 'grant_tower_reward.request', ''),
    204: ('enter_tower_copy_info', 'enter_tower_copy_info.request', ''),
    205: ('continue_tower_copy', 'continue_tower_copy.request', ''),
    206: ('request_bar_fight', 'request_bar_fight.request', ''),
    207: ('enter_bar_fight', 'enter_bar_fight.request', ''),
    208: ('tower_wipe_out', 'tower_wipe_out.request', ''),
    209: ('use_skill_buff', 'use_skill_buff.request', ''),
    210: ('request_rank_pvp_data', 'request_rank_pvp_data.request', ''),
    211: ('request_rank_pvp_history', 'request_rank_pvp_history.request', ''),
    212: ('req_guild_skill', 'req_guild_skill.request', ''),
    213: ('req_change_team_goal', 'req_change_team_goal.request', ''),
    214: ('random_select_team', 'random_select_team.request', ''),
    215: ('enter_multi_copy_scene_confirm', 'enter_multi_copy_scene_confirm.request', ''),
    216: ('stop_random_select_team', 'stop_random_select_team.request', ''),
    217: ('ret_ask_confirm_multi_copy_scene', 'ret_ask_confirm_multi_copy_scene.request', ''),
    218: ('heart_beat', 'heart_beat.request', 'heart_beat.response'),
    219: ('request_line_state', 'request_line_state.request', ''),
    220: ('start_battle', 'start_battle.request', ''),
    221: ('equip_fashion_item', 'equip_fashion_item.request', ''),
    222: ('unequip_fashion_item', 'unequip_fashion_item.request', ''),
    223: ('change_show_type', 'change_show_type.request', ''),
    224: ('open_item_package', 'open_item_package.request', ''),
    225: ('request_activity_info', 'request_activity_info.request', ''),
    226: ('request_change_pk_mode', 'request_change_pk_mode.request', ''),
    227: ('request_dance_info', 'request_dance_info.request', ''),
    228: ('pause_participate_dance', 'pause_participate_dance.request', ''),
    229: ('use_dance', 'use_dance.request', ''),
    230: ('tower_reset', 'tower_reset.request', ''),
    231: ('request_battle_info', 'request_battle_info.request', ''),
    232: ('copy_swipe_out', 'copy_swipe_out.request', ''),
    233: ('open_guild_boss', 'open_guild_boss.request', ''),
    234: ('leave_game', 'leave_game.request', ''),
    235: ('request_mount_info', 'request_mount_info.request', ''),
    236: ('mount_equip', 'mount_equip.request', ''),
    237: ('mount_unequip', 'mount_unequip.request', ''),
    238: ('use_mount', 'use_mount.request', ''),
    239: ('unuse_mount', 'unuse_mount.request', ''),
    240: ('change_item_state', 'change_item_state.request', ''),
    241: ('mount_use_color', 'mount_use_color.request', ''),
    242: ('request_slot_info', 'request_slot_info.request', ''),
    243: ('spin_slot', 'spin_slot.request', ''),
    244: ('request_slot_sum_reward', 'request_slot_sum_reward.request', ''),
    245: ('request_survive_top', 'request_survive_top.request', ''),
    246: ('enter_survive_batttle', 'enter_survive_batttle.request', ''),
    247: ('guild_invite', 'guild_invite.request', ''),
    248: ('req_other_team', 'req_other_team.request', ''),
    249: ('request_slot_reward', 'request_slot_reward.request', ''),
    250: ('stop_leave_copy', 'stop_leave_copy.request', ''),
    251: ('enter_teleport_point', 'enter_teleport_point.request', ''),
    252: ('request_sign_30_day_info', 'request_sign_30_day_info.request', ''),
    253: ('request_sign_week_info', 'request_sign_week_info.request', ''),
    254: ('sign_30_day', 'sign_30_day.request', ''),
    255: ('sign_week', 'sign_week.request', ''),
    256: ('request_level_pack', 'request_level_pack.request', ''),
    257: ('request_invest_pack', 'request_invest_pack.request', ''),
    258: ('request_daily_buy', 'request_daily_buy.request', ''),
    259: ('request_first_buy', 'request_first_buy.request', ''),
    260: ('request_big_pack', 'request_big_pack.request', ''),
    261: ('request_daily_active', 'request_daily_active.request', ''),
    262: ('require_level_reward', 'require_level_reward.request', ''),
    263: ('require_invest_reward', 'require_invest_reward.request', ''),
    264: ('require_first_buy_reward', 'require_first_buy_reward.request', ''),
    265: ('require_daily_active_reward', 'require_daily_active_reward.request', ''),
    266: ('check_purchase', 'check_purchase.request', ''),
    267: ('change_mount_state', 'change_mount_state.request', ''),
    268: ('unlock_function_complete', 'unlock_function_complete.request', ''),
    269: ('start_download', 'start_download.request', ''),
    270: ('download_finish', 'download_finish.request', ''),
    271: ('buy_invest_pack', 'buy_invest_pack.request', ''),
    272: ('buy_big_pack', 'buy_big_pack.request', ''),
    273: ('enter_scuffle_batttle', 'enter_scuffle_batttle.request', ''),
    274: ('request_special_big_pack', 'request_special_big_pack.request', ''),
    275: ('play_social_dance', 'play_social_dance.request', ''),
    276: ('enter_single_exp_scene', 'enter_single_exp_scene.request', ''),
    277: ('update_sex_mini_score', 'update_sex_mini_score.request', ''),
    278: ('request_retrieve_info', 'request_retrieve_info.request', ''),
    279: ('request_retrieve', 'request_retrieve.request', ''),
    280: ('update_client_state', 'update_client_state.request', ''),
    281: ('refresh_online_state', 'refresh_online_state.request', ''),
    282: ('sign_bar_fight', 'sign_bar_fight.request', ''),
    283: ('open_multi_tower_reward', 'open_multi_tower_reward.request', ''),
    284: ('send_mail_box', 'send_mail_box.request', ''),
    285: ('req_guild_battle_info', 'req_guild_battle_info.request', ''),
    286: ('enter_guild_battle', 'enter_guild_battle.request', ''),
    287: ('req_guild_battle_rank', 'req_guild_battle_rank.request', ''),
    288: ('req_guild_battle_member', 'req_guild_battle_member.request', ''),
    289: ('set_guild_battle_member', 'set_guild_battle_member.request', ''),
    290: ('guild_battle_guess', 'guild_battle_guess.request', ''),
    291: ('req_guild_score_info', 'req_guild_score_info.request', ''),
    292: ('req_guild_battle_guess', 'req_guild_battle_guess.request', ''),
    293: ('req_guild_star', '', ''),
    294: ('update_guild_star', 'update_guild_star.request', ''),
    295: ('req_guild_battle_state', 'req_guild_battle_state.request', ''),
    296: ('req_level_reward', 'req_level_reward.request', ''),
    297: ('receive_level_reward', 'receive_level_reward.request', ''),
    298: ('impact_npc', 'impact_npc.request', ''),
    299: ('require_vip_info', 'require_vip_info.request', ''),
    300: ('require_vip_reward', 'require_vip_reward.request', ''),
    301: ('re_name', 're_name.request', ''),
    302: ('attribute_inhert', 'attribute_inhert.request', ''),
    303: ('equip_appraise', 'equip_appraise.request', ''),
    304: ('equip_inlay', 'equip_inlay.request', ''),
    305: ('weapon_inhert', 'weapon_inhert.request', ''),
    306: ('tutorial_finish', 'tutorial_finish.request', ''),
    307: ('local_npc_die', 'local_npc_die.request', ''),
    308: ('game_check', 'game_check.request', ''),
    309: ('refresh_online_misison', 'refresh_online_misison.request', ''),
    310: ('request_domin_info', 'request_domin_info.request', ''),
    311: ('enter_domin_pk_scene', 'enter_domin_pk_scene.request', ''),
    312: ('require_domin_rewards', 'require_domin_rewards.request', ''),
    313: ('request_dance_state_info', 'request_dance_state_info.request', ''),
    314: ('use_dance_sound_box', 'use_dance_sound_box.request', ''),
    315: ('update_guild_dance_time', 'update_guild_dance_time.request', ''),
    316: ('change_skill_index', 'change_skill_index.request', ''),
    317: ('attack_local_npc', 'attack_local_npc.request', ''),
    318: ('request_guild_map_domine_top', 'request_guild_map_domine_top.request', ''),
    319: ('request_guild_map_info', 'request_guild_map_info.request', ''),
    320: ('request_guild_map_reward', 'request_guild_map_reward.request', ''),
    321: ('gather_other_player', 'gather_other_player.request', ''),
    322: ('enter_guild_city_scene', 'enter_guild_city_scene.request', ''),
    323: ('buy_car_shop', 'buy_car_shop.request', ''),
    324: ('update_player_map_info', 'update_player_map_info.request', 'update_player_map_info.response'),
    450: ('urge_team_leader', 'urge_team_leader.request', ''),
    451: ('enter_empty_scene', 'enter_empty_scene.request', ''),
    452: ('watch_video_info', 'watch_video_info.request', ''),
    503: ('enter_map', 'enter_map.request', ''),
    504: ('main_player_create', 'main_player_create.request', ''),
    505: ('aoi_add', 'aoi_add.request', ''),
    506: ('aoi_remove', 'aoi_remove.request', ''),
    507: ('aoi_update_move', 'aoi_update_move.request', ''),
    508: ('ret_skill_use', 'ret_skill_use.request', ''),
    509: ('npc_create', 'npc_create.request', ''),
    510: ('aoi_update_attribute', 'aoi_update_attribute.request', ''),
    511: ('show_damage_board', 'show_damage_board.request', ''),
    512: ('aoi_relife_player', 'aoi_relife_player.request', ''),
    513: ('aoi_stop_move', 'aoi_stop_move.request', ''),
    514: ('hit_action', 'hit_action.request', ''),
    515: ('next_wave', 'next_wave.request', ''),
    516: ('invite_join_team', 'invite_join_team.request', ''),
    517: ('req_invite_team_result', 'req_invite_team_result.request', ''),
    518: ('update_team', 'update_team.request', ''),
    519: ('sync_mission', 'sync_mission.request', ''),
    520: ('ret_accept_mission', 'ret_accept_mission.request', ''),
    521: ('ret_complete_mission', 'ret_complete_mission.request', ''),
    522: ('ret_abandon_mission', 'ret_abandon_mission.request', ''),
    523: ('set_mission_state', 'set_mission_state.request', ''),
    524: ('set_mission_param', 'set_mission_param.request', ''),
    525: ('update_item', 'update_item.request', ''),
    526: ('ret_use_item', 'ret_use_item.request', ''),
    527: ('drop_item_info', 'drop_item_info.request', ''),
    528: ('ret_chat', 'ret_chat.request', ''),
    529: ('notice', 'notice.request', ''),
    530: ('send_daily_mission', 'send_daily_mission.request', ''),
    531: ('mail_update', 'mail_update.request', ''),
    532: ('mail_delete', 'mail_delete.request', ''),
    533: ('ret_add_friend', 'ret_add_friend.request', ''),
    534: ('ret_request_update_friend_useinfo', 'ret_request_update_friend_useinfo.request', ''),
    535: ('ret_del_friend', 'ret_del_friend.request', ''),
    536: ('notice_add_friend', 'notice_add_friend.request', ''),
    537: ('be_deleted_friend', 'be_deleted_friend.request', ''),
    538: ('syn_friend_info', 'syn_friend_info.request', ''),
    540: ('sync_skill_info', 'sync_skill_info.request', 'sync_skill_info.response'),
    541: ('syn_rank_pvp_data', 'syn_rank_pvp_data.request', ''),
    542: ('ret_request_random_rank_pvp_opponent', 'ret_request_random_rank_pvp_opponent.request', ''),
    543: ('ret_request_top_rank_pvp_list', 'ret_request_top_rank_pvp_list.request', ''),
    544: ('rank_pvp_create_zombie_user', 'rank_pvp_create_zombie_user.request', ''),
    545: ('rank_pvp_reward', 'rank_pvp_reward.request', ''),
    546: ('rank_pvp_history', 'rank_pvp_history.request', ''),
    547: ('rank_pvp_start', 'rank_pvp_start.request', ''),
    548: ('real_pvp_state', 'real_pvp_state.request', ''),
    549: ('real_pvp_start', 'real_pvp_start.request', ''),
    550: ('ret_request_update_storagepack', 'ret_request_update_storagepack.request', ''),
    551: ('tiantti_result', 'tiantti_result.request', ''),
    552: ('copy_scene_result', 'copy_scene_result.request', ''),
    553: ('count_down', 'count_down.request', ''),
    554: ('ret_ask_shop_list', 'ret_ask_shop_list.request', ''),
    555: ('sync_copyscenes_info', 'sync_copyscenes_info.request', ''),
    561: ('update_copyscene_info', 'update_copyscene_info.request', ''),
    562: ('ret_guild_req_list', 'ret_guild_req_list.request', ''),
    563: ('ret_guild_req_info', 'ret_guild_req_info.request', ''),
    564: ('ret_guild_skill_level', 'ret_guild_skill_level.request', ''),
    565: ('ret_guild_leave', 'ret_guild_leave.request', ''),
    566: ('ret_guild_join', 'ret_guild_join.request', ''),
    567: ('ret_guild_create', 'ret_guild_create.request', ''),
    568: ('update_line_state', 'update_line_state.request', ''),
    569: ('ret_title_req_level_up', 'ret_title_req_level_up.request', ''),
    570: ('ret_random_online_character_list', 'ret_random_online_character_list.request', ''),
    571: ('ret_search_online_character_by_name', 'ret_search_online_character_by_name.request', ''),
    572: ('apply_join_team', 'apply_join_team.request', ''),
    573: ('cancel_apply_join_team', 'cancel_apply_join_team.request', ''),
    574: ('ret_get_team_list', 'ret_get_team_list.request', ''),
    575: ('update_team_member', 'update_team_member.request', ''),
    576: ('apply_join_state', 'apply_join_state.request', ''),
    577: ('update_queue_rank', 'update_queue_rank.request', ''),
    578: ('login_max_count', 'login_max_count.request', ''),
    579: ('ret_offline_chat', 'ret_offline_chat.request', ''),
    580: ('sync_guild_new_member', 'sync_guild_new_member.request', ''),
    581: ('ret_guild_member_info', 'ret_guild_member_info.request', ''),
    582: ('ret_open_guild_shop', 'ret_open_guild_shop.request', ''),
    583: ('ret_buy_guild_goods', 'ret_buy_guild_goods.request', ''),
    584: ('ret_guild_log', 'ret_guild_log.request', ''),
    585: ('ret_guild_donate', 'ret_guild_donate.request', ''),
    586: ('ret_search_guild', 'ret_search_guild.request', ''),
    589: ('ret_guild_job_change', 'ret_guild_job_change.request', ''),
    590: ('ret_guild_kick', 'ret_guild_kick.request', ''),
    591: ('ret_guild_approve_resverve', 'ret_guild_approve_resverve.request', ''),
    592: ('sync_backpack_item', 'sync_backpack_item.request', ''),
    593: ('ret_consign_sale_item', 'ret_consign_sale_item.request', ''),
    594: ('ret_consign_cancel_sale', 'ret_consign_cancel_sale.request', ''),
    595: ('ret_consign_ask_my_items', 'ret_consign_ask_my_items.request', ''),
    596: ('ret_consign_ask_items_info', 'ret_consign_ask_items_info.request', ''),
    597: ('ret_consign_buy_item', 'ret_consign_buy_item.request', ''),
    598: ('ret_top_rank_list', 'ret_top_rank_list.request', ''),
    599: ('ret_request_guild_boss', 'ret_request_guild_boss.request', ''),
    601: ('ask_confirm', 'ask_confirm.request', 'ask_confirm.response'),
    602: ('ask_confirm_multi_copy_scene', 'ask_confirm_multi_copy_scene.request', ''),
    603: ('notify_confirm_state', 'notify_confirm_state.request', ''),
    604: ('sync_badgepack_item', 'sync_badgepack_item.request', ''),
    605: ('ret_request_wild_boss_info', 'ret_request_wild_boss_info.request', ''),
    606: ('ret_request_tower_copy_info', 'ret_request_tower_copy_info.request', ''),
    607: ('ret_tower_wipe_out', 'ret_tower_wipe_out.request', ''),
    608: ('car_copy_result', 'car_copy_result.request', ''),
    609: ('ret_req_guild_skill', 'ret_req_guild_skill.request', ''),
    610: ('random_select_ok', 'random_select_ok.request', ''),
    611: ('sync_item_pack', 'sync_item_pack.request', ''),
    612: ('grant_daily_mission_reward', 'grant_daily_mission_reward.request', ''),
    613: ('sample_copy_result', 'sample_copy_result.request', ''),
    614: ('sync_common_data', 'sync_common_data.request', ''),
    615: ('notice_money_copy_reward', 'notice_money_copy_reward.request', ''),
    616: ('sync_fashion_backpack_item', 'sync_fashion_backpack_item.request', ''),
    617: ('ret_open_item_package', 'ret_open_item_package.request', ''),
    618: ('notice_relife_player', 'notice_relife_player.request', ''),
    619: ('ret_request_activity_info', 'ret_request_activity_info.request', ''),
    620: ('grant_activity_reward', 'grant_activity_reward.request', ''),
    621: ('send_escort_info', 'send_escort_info.request', ''),
    623: ('ret_request_dance_info', 'ret_request_dance_info.request', ''),
    624: ('start_participate_dance', 'start_participate_dance.request', ''),
    625: ('ret_grant_tower_reward', 'ret_grant_tower_reward.request', ''),
    626: ('ret_tower_reset', 'ret_tower_reset.request', ''),
    627: ('ret_battle_info', 'ret_battle_info.request', ''),
    628: ('ret_open_guild_boss', 'ret_open_guild_boss.request', ''),
    629: ('notify_copy_start_info', 'notify_copy_start_info.request', ''),
    630: ('ret_mount_info', 'ret_mount_info.request', ''),
    631: ('ret_mount_equip', 'ret_mount_equip.request', ''),
    632: ('ret_mount_use_color', 'ret_mount_use_color.request', ''),
    633: ('ret_slot_info', 'ret_slot_info.request', ''),
    634: ('ret_spin_slot', 'ret_spin_slot.request', ''),
    635: ('ret_slot_sum_reward', 'ret_slot_sum_reward.request', ''),
    636: ('ret_request_survive_top', 'ret_request_survive_top.request', ''),
    637: ('survive_battle_finish', 'survive_battle_finish.request', ''),
    638: ('show_reward_items_tips', 'show_reward_items_tips.request', ''),
    639: ('guild_invite_accept', 'guild_invite_accept.request', ''),
    640: ('ret_request_30_day_info', 'ret_request_30_day_info.request', ''),
    641: ('ret_request_sign_week_info', 'ret_request_sign_week_info.request', ''),
    642: ('ret_sign_30_day', 'ret_sign_30_day.request', ''),
    643: ('ret_sign_week', 'ret_sign_week.request', ''),
    644: ('ret_request_level_pack', 'ret_request_level_pack.request', ''),
    645: ('ret_request_invest_pack', 'ret_request_invest_pack.request', ''),
    646: ('ret_request_daily_buy', 'ret_request_daily_buy.request', ''),
    647: ('ret_request_first_buy', 'ret_request_first_buy.request', ''),
    648: ('ret_request_big_pack', 'ret_request_big_pack.request', ''),
    649: ('ret_request_daily_active', 'ret_request_daily_active.request', ''),
    650: ('ret_commercail_reward', 'ret_commercail_reward.request', ''),
    651: ('comb_value_up_tip', 'comb_value_up_tip.request', ''),
    652: ('ret_buy_shop_item', 'ret_buy_shop_item.request', ''),
    653: ('send_dialog_notify', 'send_dialog_notify.request', ''),
    654: ('start_enter_game', 'start_enter_game.request', ''),
    655: ('ret_buy_invest_pack', 'ret_buy_invest_pack.request', ''),
    656: ('ret_special_big_pack', 'ret_special_big_pack.request', ''),
    657: ('aoi_social_dance', 'aoi_social_dance.request', ''),
    658: ('ret_request_retrieve_info', 'ret_request_retrieve_info.request', ''),
    659: ('bar_fight_notify', 'bar_fight_notify.request', ''),
    660: ('retrieve_account', 'retrieve_account.request', ''),
    662: ('ret_guild_battle_info', 'ret_guild_battle_info.request', ''),
    663: ('ret_guild_battle_rank', 'ret_guild_battle_rank.request', ''),
    665: ('ret_guild_battle_member', 'ret_guild_battle_member.request', ''),
    666: ('ret_set_guild_battle_member', 'ret_set_guild_battle_member.request', ''),
    667: ('ret_guild_score_info', 'ret_guild_score_info.request', ''),
    668: ('guild_battle_finish_info', 'guild_battle_finish_info.request', ''),
    669: ('ret_guild_battle_guess', 'ret_guild_battle_guess.request', ''),
    670: ('guild_battle_start', 'guild_battle_start.request', ''),
    671: ('ret_guild_star', 'ret_guild_star.request', ''),
    672: ('ret_update_guild_star', 'ret_update_guild_star.request', ''),
    673: ('ret_guild_battle_state', 'ret_guild_battle_state.request', ''),
    674: ('ret_level_reward', 'ret_level_reward.request', ''),
    675: ('get_level_reward', 'get_level_reward.request', ''),
    676: ('notice_guild_battle_rank', 'notice_guild_battle_rank.request', ''),
    677: ('ret_enter_guild_battle', 'ret_enter_guild_battle.request', ''),
    678: ('ret_require_vip_info', 'ret_require_vip_info.request', ''),
    679: ('ret_require_vip_reward', 'ret_require_vip_reward.request', ''),
    680: ('ret_re_name', 'ret_re_name.request', ''),
    681: ('sync_random_team_state', 'sync_random_team_state.request', ''),
    682: ('notice_urge_team_leader', 'notice_urge_team_leader.request', ''),
    683: ('notice_copy_scene_info', 'notice_copy_scene_info.request', ''),
    684: ('ret_domin_info', 'ret_domin_info.request', ''),
    685: ('sample_activity_result', 'sample_activity_result.request', ''),
    686: ('sync_dance_state_info', 'sync_dance_state_info.request', ''),
    687: ('show_player_damage_board', 'show_player_damage_board.request', ''),
    688: ('ret_guild_map_domine_top', 'ret_guild_map_domine_top.request', ''),
    689: ('ret_request_guild_map_info', 'ret_request_guild_map_info.request', ''),
    690: ('ret_guild_map_reward', 'ret_guild_map_reward.request', ''),
    691: ('ret_buy_car_shop', 'ret_buy_car_shop.request', ''),
    692: ('sync_watch_video_info', 'sync_watch_video_info.request', ''),
    693: ('ret_watch_video_info', 'ret_watch_video_info.request', ''),
}

# Embedded Sproto Type Fields: type_name -> {tag: field_name}
CLASS_FIELDS = {
    'Package': {0: 'type', 1: 'session'},
    'abandon_mission.request': {0: 'missionId', 1: 'parm'},
    'accept_damge.request': {0: 'damges'},
    'accept_mission.request': {0: 'missionId', 1: 'onlineId'},
    'acceptdamge': {0: 'id', 1: 'damage', 2: 'skillId', 3: 'effinfoId', 4: 'cri', 5: 'parm', 6: 'parm2', 7: 'parm3', 8: 'parm4'},
    'activity_info': {0: 'ID', 1: 'CurNum', 2: 'Type', 3: 'State', 4: 'Parm', 5: 'Parmstr', 6: 'sign', 7: 'time', 8: 'next'},
    'add_friend.request': {0: 'characterId', 1: 'type'},
    'aoi_add.request': {0: 'character'},
    'aoi_relife_player.request': {0: 'character'},
    'aoi_remove.request': {0: 'character'},
    'aoi_social_dance.request': {0: 'id', 1: 'danceId'},
    'aoi_stop_move.request': {0: 'character'},
    'aoi_update_attribute.request': {0: 'character'},
    'aoi_update_move.request': {0: 'character'},
    'apply_join_result.request': {0: 'characterId', 1: 'isAgree'},
    'apply_join_state.request': {0: 'teamid', 1: 'isAgree'},
    'apply_join_team.request': {0: 'member'},
    'approve_resverve_friend.request': {0: 'characterId', 1: 'isAgree'},
    'ask_character_info.request': {0: 'characterId'},
    'ask_character_info.response': {0: 'character'},
    'ask_confirm.request': {0: 'type', 1: 'id', 2: 'parm1', 3: 'param2'},
    'ask_confirm.response': {0: 'state'},
    'ask_confirm_multi_copy_scene.request': {0: 'id', 1: 'type1', 2: 'session'},
    'ask_pickup_item.request': {0: 'serverId'},
    'ask_shop_list.request': {0: 'type', 1: 'curPage', 2: 'itemId', 3: 'subType', 4: 'class1', 5: 'special'},
    'attack_list': {0: 'id', 1: 'value'},
    'attack_local_npc.request': {0: 'damge', 1: 'effinfoId'},
    'attribute': {0: 'max_hp', 1: 'exp', 2: 'atk', 3: 'def', 4: 'hit', 5: 'eva', 6: 'cri', 7: 'res', 8: 'exd', 9: 'exr', 10: 'crd', 11: 'crr', 12: 'defa', 13: 'mov', 14: 'rec', 15: 'anti_stun', 16: 'anti_knock_down', 17: 'dgea', 18: 'resa', 19: 'hita', 20: 'cria', 21: 'ate', 22: 'satm', 23: 'satc', 24: 'satp'},
    'attribute_aoi': {0: 'hp', 1: 'exp', 2: 'level', 3: 'combValue', 4: 'title_level', 5: 'title_exp', 6: 'refineNeckLevel', 7: 'refineRing1Level', 8: 'refineRing2Level', 9: 'refineBeltLevel', 10: 'refineLevel'},
    'attribute_inhert.request': {0: 'index1', 1: 'index2', 2: 'attribute_index1', 3: 'attribute_index2'},
    'attribute_other': {0: 'hp', 1: 'exp', 2: 'level', 3: 'combValue', 4: 'title_level', 5: 'title_exp', 6: 'guildId', 7: 'guildJob', 8: 'guildName', 9: 'refineNeckLevel', 10: 'refineRing1Level', 11: 'refineRing2Level', 12: 'refineBeltLevel', 13: 'refineLevel', 14: 'vip', 15: 'camp', 16: 'pkMode', 17: 'dance_state', 18: 'dance_id'},
    'attribute_overview': {0: 'level', 1: 'combValue'},
    'badge_merge.request': {0: 'indexId', 1: 'nextItemId', 2: 'count'},
    'badge_merge.response': {0: 'state'},
    'bar_fight_notify.request': {0: 'id'},
    'battle_info': {0: 'damage_list', 1: 'my_rank', 2: 'my_damage', 3: 'all_damage', 4: 'lastKill', 5: 'end_time'},
    'be_deleted_friend.request': {0: 'characterId'},
    'buff': {0: 'id', 1: 'effinfoId'},
    'buy_big_pack.request': {0: 'ID'},
    'buy_car_shop.request': {0: 'mountId'},
    'buy_invest_pack.request': {0: 'ID'},
    'buy_shop_item.request': {0: 'ID', 1: 'itemCount', 2: 'type'},
    'cancel_apply_join_team.request': {0: 'id'},
    'car_chase_result.request': {0: 'state', 1: 'param1', 2: 'param2'},
    'car_copy_result.request': {0: 'win', 1: 'items', 2: 'rankPos1', 3: 'rankPos2', 4: 'parm', 5: 'new_record', 6: 'id'},
    'change_item_state.request': {0: 'indexId', 1: 'type'},
    'change_mount_state.request': {0: 'ID'},
    'change_potion.request': {0: 'indexId'},
    'change_scene_line.request': {0: 'line_index'},
    'change_show_type.request': {0: 'showType'},
    'change_skill_index.request': {0: 'index'},
    'change_skill_position.request': {0: 'skillId', 1: 'indexPos'},
    'character': {0: 'id', 1: 'general', 2: 'attribute_other', 3: 'property', 4: 'visual', 5: 'movement', 6: 'skills', 7: 'equip', 8: 'badge_equip', 9: 'fashion_equip', 10: 'potionIndex', 11: 'runtime', 12: 'equip_enhance', 13: 'download', 14: 'skill_index'},
    'characterVisual': {0: 'name', 1: 'ModeId', 2: 'HeadId', 3: 'BodyId', 4: 'LegId', 5: 'WeaponId', 6: 'Fashion_HeadId', 7: 'Fashion_BodyId', 8: 'Fashion_LegId', 9: 'Fashion_WeaponId', 10: 'showType', 11: 'MountId', 12: 'mount_state', 13: 'mount_color', 14: 'WeaponItemId', 15: 'FashionItemId'},
    'character_aoi': {0: 'id', 1: 'visual', 2: 'general', 3: 'attribute_other', 4: 'movement', 5: 'runtime'},
    'character_aoi_attribute': {0: 'id', 1: 'attribute_other', 2: 'attribute', 3: 'attribute_all', 4: 'visual', 5: 'property'},
    'character_aoi_move': {0: 'id', 1: 'movement', 2: 'walk'},
    'character_create.request': {0: 'character'},
    'character_create.response': {0: 'character', 1: 'errno'},
    'character_list.response': {0: 'character'},
    'character_look': {0: 'id', 1: 'general', 2: 'attribute', 3: 'attribute_other', 4: 'visual', 5: 'equip', 6: 'fashion_equip', 7: 'badge_equip', 8: 'equip_enhance', 9: 'movement', 10: 'skills'},
    'character_overview': {0: 'id', 1: 'general', 2: 'attribute_other', 3: 'visual', 4: 'createtime', 5: 'forbidden'},
    'character_pick.request': {0: 'id'},
    'character_pick.response': {0: 'errno'},
    'character_relife': {0: 'id', 1: 'attribute_other', 2: 'movement'},
    'chat.request': {0: 'tellId', 1: 'tellName', 2: 'chatInfo', 3: 'chattype', 4: 'linktype', 5: 'intdata', 6: 'stringdata'},
    'chat_item': {0: 'senderId', 1: 'senderName', 2: 'tellId', 3: 'tellName', 4: 'chatInfo', 5: 'chattype', 6: 'linktype', 7: 'intdata', 8: 'stringdata', 9: 'senderProfession', 10: 'level', 11: 'combValue', 12: 'guildId', 13: 'guildName', 14: 'chatInfo2'},
    'check_purchase.request': {0: 'productId', 1: 'token', 2: 'payload', 3: 'packageName'},
    'color': {0: 'ID', 1: 'state'},
    'comb_value_up_tip.request': {0: 'current', 1: 'next'},
    'complete_mission.request': {0: 'missionId', 1: 'parm'},
    'consign_ask_items_info.request': {0: 'type', 1: 'subType', 2: 'quality', 3: 'levelRange', 4: 'use', 5: 'curPage', 6: 'profession'},
    'consign_buy_item.request': {0: 'id', 1: 'itemId'},
    'consign_cancel_sale.request': {0: 'id'},
    'consign_item': {0: 'id', 1: 'characterId', 2: 'itemId', 3: 'quality', 4: 'stack', 5: 'price', 6: 'time', 7: 'startTime'},
    'consign_sale_item.request': {0: 'indexId', 1: 'itemCount', 2: 'price', 3: 'timeType', 4: 'itemType'},
    'copy_scene_result.request': {0: 'subType', 1: 'id', 2: 'win', 3: 'gradeFlag', 4: 'grade', 5: 'items', 6: 'swipe', 7: 'parm', 8: 'type'},
    'copy_swipe_out.request': {0: 'copyInfoId', 1: 'reaminItem'},
    'copyscene_info': {0: 'ID', 1: 'CurNum', 2: 'BestGrade', 3: 'Type', 4: 'str', 5: 'enable', 6: 'state', 7: 'Type2'},
    'count_down.request': {0: 'type', 1: 'count_value'},
    'daily_active': {0: 'ID', 1: 'count', 2: 'Type'},
    'daily_buy': {0: 'ID', 1: 'state'},
    'daily_reward': {0: 'ID', 1: 'state'},
    'dailymission': {0: 'missionId', 1: 'missionstate'},
    'damage_list': {0: 'name', 1: 'damage', 2: 'id'},
    'dance_info': {0: 'ID', 1: 'enable', 2: 'useType', 3: 'endTime'},
    'dance_state_info': {0: 'uuid', 1: 'ID', 2: 'start_time', 3: 'end_time', 4: 'state', 5: 'parm', 6: 'duration', 7: 'reset_time', 8: 'parm2'},
    'del_friend.request': {0: 'characterId', 1: 'type'},
    'dict_hash': {0: 'id', 1: 'parm'},
    'domin_info': {0: 'id', 1: 'max_donmin', 2: 'donmin_time', 3: 'end_time', 4: 'res_time1', 5: 'res_count1', 6: 'res_time2', 7: 'res_count2', 8: 'state', 9: 'serverId', 10: 'serverType', 11: 'res_end_time1', 12: 'res_end_time2'},
    'donate_record': {0: 'id', 1: 'DonateCount'},
    'drop_item_info.request': {0: 'serverId', 1: 'pos_x', 2: 'pos_z', 3: 'type', 4: 'item', 5: 'ownServerId'},
    'enhance_info': {0: 'level', 1: 'subType'},
    'enter_bar_fight.request': {0: 'ID'},
    'enter_copy_scene.request': {0: 'mapInfoId', 1: 'reaminItem'},
    'enter_domin_pk_scene.request': {0: 'id'},
    'enter_empty_scene.request': {0: 'mapInfoId'},
    'enter_guild_boss_scene.request': {0: 'id'},
    'enter_guild_city_scene.request': {0: 'id'},
    'enter_map.request': {0: 'mapInfoId', 1: 'line_index', 2: 'line_count'},
    'enter_multi_copy_scene_confirm.request': {0: 'id', 1: 'type1', 2: 'reaminItem'},
    'enter_new_map.request': {0: 'mapInfoId'},
    'enter_scuffle_batttle.request': {0: 'ID', 1: 'floor'},
    'enter_single_exp_scene.request': {0: 'id'},
    'enter_survive_batttle.request': {0: 'id', 1: 'floor', 2: 'type'},
    'enter_teleport_point.request': {0: 'x', 1: 'y', 2: 'z', 3: 'index'},
    'enter_tower_copy_info.request': {0: 'floorID'},
    'enter_wild_boss.request': {0: 'ID'},
    'equip_appraise.request': {0: 'index'},
    'equip_badge.request': {0: 'indexId', 1: 'pos'},
    'equip_enhance.request': {0: 'indexId', 1: 'level'},
    'equip_fashion_item.request': {0: 'indexId'},
    'equip_inhert.request': {0: 'indexId1', 1: 'containertype1', 2: 'indexId2', 3: 'containertype2'},
    'equip_inhert.response': {0: 'state', 1: 'containertype1', 2: 'containertype2', 3: 'item1', 4: 'item2'},
    'equip_inlay.request': {0: 'index1', 1: 'index2', 2: 'diamond_index1', 3: 'diamond_index2'},
    'equip_item.request': {0: 'indexId', 1: 'inhert'},
    'equip_refine.request': {0: 'Id', 1: 'preId', 2: 'curId', 3: 'partId', 4: 'level', 5: 'safe'},
    'equip_refine.response': {0: 'state', 1: 'partId', 2: 'level', 3: 'allstar'},
    'facebook_link.request': {0: 'facebook_id', 1: 'facebook_token', 2: 'id', 3: 'key', 4: 'confirm', 5: 'bindType'},
    'facebook_link.response': {0: 'state', 1: 'id', 2: 'key', 3: 'bindType'},
    'facebook_unlink.request': {0: 'id', 1: 'key', 2: 'facebook_id', 3: 'facebook_token', 4: 'bindType'},
    'facebook_unlink.response': {0: 'state', 1: 'bindType'},
    'friend_info': {0: 'characterId', 1: 'friendId', 2: 'name', 3: 'level', 4: 'profession', 5: 'combValue', 6: 'state', 7: 'timeInfo', 8: 'friendType', 9: 'guildId', 10: 'guildName', 11: 'friendScore'},
    'function_info': {0: 'ID', 1: 'state'},
    'game_check.request': {0: 'type'},
    'game_server': {0: 'serverId', 1: 'serverName', 2: 'serverIP', 3: 'serverPort', 4: 'serverState', 5: 'serverPlayerState', 6: 'serverArea', 7: 'serverRank', 8: 'serverTimeZone', 9: 'serverWeight', 10: 'newServer'},
    'gameitem': {0: 'indexId', 1: 'itemId', 2: 'bindflag', 3: 'level', 4: 'flags', 5: 'stack', 6: 'quality', 7: 'parm', 8: 'appraise', 9: 'random_attri', 10: 'inlay'},
    'gather_other_player.request': {0: 'characterid'},
    'general': {0: 'name', 1: 'profession', 2: 'lineIndex', 3: 'mapInfoId', 4: 'tutorial'},
    'get_level_reward.request': {0: 'level_reward', 1: 'items'},
    'get_team_list.request': {0: 'goalId'},
    'grant_activity_reward.request': {0: 'type', 1: 'state', 2: 'win', 3: 'items', 4: 'battle_info', 5: 'ID', 6: 'items2'},
    'grant_daily_mission_reward.request': {0: 'items', 1: 'items2'},
    'grant_tower_reward.request': {0: 'type', 1: 'id'},
    'guild_approve_resverve.request': {0: 'characterId', 1: 'isAgree'},
    'guild_battle_finish_info.request': {0: 'guild_battle_score_info'},
    'guild_battle_guess.request': {0: 'guildId'},
    'guild_battle_info': {0: 'state', 1: 'battle_round_1', 2: 'battle_round_2', 3: 'battle_round_3', 4: 'time', 5: 'ID', 6: 'guildId', 7: 'championName'},
    'guild_battle_item_info': {0: 'id', 1: 'name', 2: 'guildId', 3: 'guildName', 4: 'killNum', 5: 'continueKill', 6: 'score'},
    'guild_battle_round': {0: 'battle_team', 1: 'state'},
    'guild_battle_score_info': {0: 'item_info', 1: 'score1', 2: 'score2', 3: 'selfKillNum', 4: 'selfScore', 5: 'guildName1', 6: 'guildName2', 7: 'win', 8: 'guildIcon1', 9: 'guildIcon2'},
    'guild_battle_team': {0: 'guildId', 1: 'guildName', 2: 'index', 3: 'state'},
    'guild_boss': {0: 'id', 1: 'state', 2: 'time', 3: 'curNum', 4: 'sort_item'},
    'guild_create.request': {0: 'guildName', 1: 'Icon', 2: 'notice', 3: 'costType'},
    'guild_donate.request': {0: 'id'},
    'guild_info': {0: 'guildId', 1: 'guildName', 2: 'guildChiefName', 3: 'guildChiefId', 4: 'guildExp', 5: 'guildSkillPoint', 6: 'guildLevel', 7: 'guildMemberNum', 8: 'guildCombo', 9: 'guildApplyNum', 10: 'guildApplyMaxNum', 11: 'guildMaxPlayer', 12: 'notice', 13: 'isNeedAppro', 14: 'createTime', 15: 'guildBoss', 16: 'guildBattle', 17: 'viceNum', 18: 'elderNum', 19: 'playerJob', 20: 'icon', 21: 'disactiveState'},
    'guild_invite.request': {0: 'id'},
    'guild_invite_accept.request': {0: 'name', 1: 'guildId', 2: 'guildName'},
    'guild_job_change.request': {0: 'characterId', 1: 'jobId'},
    'guild_join.request': {0: 'guildId'},
    'guild_kick.request': {0: 'characterId'},
    'guild_leave.request': {0: 'characterId', 1: 'isCancel'},
    'guild_map_info': {0: 'id', 1: 'guildId', 2: 'guildName', 3: 'guildIcon', 4: 'requireState', 5: 'state'},
    'guild_member_info': {0: 'guildId', 1: 'characterId', 2: 'name', 3: 'vip', 4: 'profession', 5: 'level', 6: 'contribute', 7: 'lastLogout', 8: 'state', 9: 'job', 10: 'combValue', 11: 'all_contribute', 12: 'battle'},
    'guild_req_info.request': {0: 'characterId'},
    'guild_req_list.request': {0: 'characterId', 1: 'curPage'},
    'guild_skill': {0: 'skillType', 1: 'level'},
    'guild_skill_level.request': {0: 'guildSkillType'},
    'guild_star': {0: 'ID', 1: 'state'},
    'heart_beat.request': {0: 'time', 1: 'time2'},
    'heart_beat.response': {0: 'time', 1: 'serverTime'},
    'hit_action.request': {0: 'targetid', 1: 'senderId', 2: 'effinfoId'},
    'impact_npc.request': {0: 'type'},
    'inlay': {0: 'index', 1: 'itemId'},
    'invest_pack': {0: 'ID', 1: 'state'},
    'invite_join_team.request': {0: 'teamid', 1: 'member', 2: 'goalId'},
    'item': {0: 'itemId', 1: 'itemCount', 2: 'quality', 3: 'id', 4: 'count2'},
    'leave_team.request': {0: 'teamid', 1: 'characterId'},
    'level_pack': {0: 'ID', 1: 'state'},
    'level_reward': {0: 'ID', 1: 'state'},
    'local_character_attack.request': {0: 'characterId', 1: 'damage', 2: 'effinfoId'},
    'local_npc_die.request': {0: 'npcid', 1: 'x', 2: 'z', 3: 'type'},
    'login.request': {0: 'session', 1: 'id', 2: 'logintype', 3: 'version', 4: 'unityVersion', 5: 'serverId', 6: 'time'},
    'login.response': {0: 'type', 1: 'versionCode', 2: 'dataVersionCode', 3: 'serverLevel'},
    'mail_delete.request': {0: 'mailId'},
    'mail_operation.request': {0: 'mailId', 1: 'operation'},
    'mail_update.request': {0: 'mailId', 1: 'sendertype', 2: 'title', 3: 'senderTime', 4: 'receiveId', 5: 'readTime', 6: 'context', 7: 'mailState', 8: 'sortTime', 9: 'items', 10: 'expireday'},
    'main_player_create.request': {0: 'character', 1: 'movement'},
    'mount': {0: 'ID', 1: 'state', 2: 'colors', 3: 'select'},
    'mount_equip.request': {0: 'ID'},
    'mount_unequip.request': {0: 'ID'},
    'mount_use_color.request': {0: 'mountId', 1: 'colorId'},
    'move.request': {0: 'pos', 1: 'moving', 2: 'index', 3: 'parm'},
    'move.response': {0: 'pos'},
    'movement': {0: 'pos', 1: 'pos2'},
    'next_wave.request': {0: 'waveid'},
    'notice.request': {0: 'notice', 1: 'repeate'},
    'notice_add_friend.request': {0: 'friend'},
    'notice_copy_scene_info.request': {0: 'id', 1: 'index', 2: 'time', 3: 'parm1', 4: 'parm2', 5: 'parm3'},
    'notice_guild_battle_rank.request': {0: 'guildId'},
    'notice_money_copy_reward.request': {0: 'items'},
    'notice_relife_player.request': {0: 'type', 1: 'cost', 2: 'itemId', 3: 'characterid', 4: 'name'},
    'notice_urge_team_leader.request': {0: 'name', 1: 'id'},
    'notify_confirm_state.request': {0: 'characterId', 1: 'state'},
    'notify_copy_start_info.request': {0: 'end_time', 1: 'type', 2: 'wave_time', 3: 'curWave'},
    'npc_attribute': {0: 'id', 1: 'npcdataid', 2: 'hp', 3: 'max_hp', 4: 'atk', 5: 'def', 6: 'hit', 7: 'eva', 8: 'cri', 9: 'exd', 10: 'exr', 11: 'res', 12: 'crd', 13: 'crr', 14: 'defa', 15: 'x', 16: 'z', 17: 'o', 18: 'level', 19: 'anti_stun', 20: 'anti_knock_down', 21: 'player_name', 22: 'guildId', 23: 'teamid', 24: 'dgea', 25: 'resa', 26: 'hita', 27: 'cria'},
    'npc_create.request': {0: 'npc_attribute'},
    'open_guild_boss.request': {0: 'id'},
    'open_item_package.request': {0: 'indexId', 1: 'indexId2', 2: 'count'},
    'open_multi_tower_reward.request': {0: 'index', 1: 'floor'},
    'ownmission': {0: 'missionId', 1: 'missionstate', 2: 'missionquality', 3: 'parm'},
    'play_social_dance.request': {0: 'id', 1: 'danceId'},
    'position': {0: 'x', 1: 'y', 2: 'z', 3: 'o'},
    'property': {0: 'money1', 1: 'money2', 2: 'money3', 3: 'money4', 4: 'money5', 5: 'money6'},
    'put_item_storagepack.request': {0: 'indexId'},
    'random_attri': {0: 'index', 1: 'id', 2: 'value', 3: 'quality', 4: 'skillId', 5: 'qualityId'},
    'random_select_ok.request': {0: 'id', 1: 'type1'},
    'random_select_team.request': {0: 'id', 1: 'type1'},
    'rank_pvp_create_zombie_user.request': {0: 'character'},
    'rank_pvp_history.request': {0: 'logs'},
    'rank_pvp_player_attack.request': {0: 'characterId', 1: 'damage'},
    're_name.request': {0: 'name'},
    'real_pvp_register.request': {0: 'state'},
    'receive_level_reward.request': {0: 'ID', 1: 'index'},
    'refresh_online_state.request': {0: 'type', 1: 'id', 2: 'mapId'},
    'relife_player.request': {0: 'isInplace'},
    'req_buy_guild_goods.request': {0: 'itemId', 1: 'buyCount'},
    'req_change_team_goal.request': {0: 'goalId', 1: 'minLevel', 2: 'maxLevel', 3: 'isVerfiy', 4: 'recruit'},
    'req_guild_member_info.request': {0: 'guildId'},
    'req_guild_notice.request': {0: 'notice'},
    'req_invite_team.request': {0: 'characterid', 1: 'goalId', 2: 'minLevel', 3: 'maxLevel', 4: 'isVerfiy', 5: 'recruit'},
    'req_invite_team_result.request': {0: 'ok', 1: 'id'},
    'req_join_team.request': {0: 'teamid', 1: 'isapply'},
    'req_other_team.request': {0: 'id'},
    'req_random_online_character_list.request': {0: 'characterId'},
    'req_seting_guild_appro.request': {0: 'guildId', 1: 'isNeedAppro'},
    'request_activity_info.request': {0: 'type'},
    'request_change_pk_mode.request': {0: 'pk'},
    'request_dance_info.request': {0: 'type'},
    'request_dance_state_info.request': {0: 'detail'},
    'request_guild_map_domine_top.request': {0: 'id'},
    'request_guild_map_reward.request': {0: 'id'},
    'request_guild_reward.request': {0: 'id'},
    'request_random_name.request': {0: 'type'},
    'request_random_name.response': {0: 'name'},
    'request_retrieve.request': {0: 'ID', 1: 'Type'},
    'request_slot_reward.request': {0: 'uuid'},
    'request_top_rank_list.request': {0: 'sortType'},
    'request_top_rank_pvp_list.request': {0: 'curPage'},
    'request_update_friend_useinfo.request': {0: 'characterId', 1: 'type'},
    'require_daily_active_reward.request': {0: 'ID'},
    'require_domin_rewards.request': {0: 'id', 1: 'index', 2: 'cost'},
    'require_first_buy_reward.request': {0: 'ID'},
    'require_invest_reward.request': {0: 'ID'},
    'require_level_reward.request': {0: 'ID'},
    'ret_abandon_mission.request': {0: 'missionId', 1: 'ret'},
    'ret_accept_mission.request': {0: 'missionId', 1: 'missionquality', 2: 'ret', 3: 'mission'},
    'ret_add_friend.request': {0: 'friend'},
    'ret_ask_confirm_multi_copy_scene.request': {0: 'session', 1: 'state'},
    'ret_ask_shop_list.request': {0: 'type', 1: 'curPage', 2: 'maxPage', 3: 'shop_list', 4: 'subType'},
    'ret_battle_info.request': {0: 'battle_info', 1: 'type'},
    'ret_buy_car_shop.request': {0: 'mountId', 1: 'state'},
    'ret_buy_guild_goods.request': {0: 'itemId', 1: 'buyCount', 2: 'cost', 3: 'leftNum'},
    'ret_buy_invest_pack.request': {0: 'invest_pack'},
    'ret_buy_shop_item.request': {0: 'state', 1: 'shop_item', 2: 'type', 3: 'count'},
    'ret_chat.request': {0: 'chat_list'},
    'ret_commercail_reward.request': {0: 'items', 1: 'type', 2: 'parm1', 3: 'parm2', 4: 'productId', 5: 'token', 6: 'payload', 7: 'state'},
    'ret_complete_mission.request': {0: 'missionId', 1: 'ret'},
    'ret_consign_ask_items_info.request': {0: 'consign_items', 1: 'curPage', 2: 'maxPage', 3: 'success', 4: 'serverTime'},
    'ret_consign_ask_my_items.request': {0: 'consign_items', 1: 'success', 2: 'serverTime'},
    'ret_consign_buy_item.request': {0: 'id', 1: 'success', 2: 'itemId'},
    'ret_consign_cancel_sale.request': {0: 'id', 1: 'success'},
    'ret_consign_sale_item.request': {0: 'indexId', 1: 'success', 2: 'gameitem', 3: 'itemType'},
    'ret_del_friend.request': {0: 'characterId'},
    'ret_domin_info.request': {0: 'domin_infos', 1: 'characters'},
    'ret_enter_guild_battle.request': {0: 'state'},
    'ret_get_team_list.request': {0: 'teams'},
    'ret_grant_tower_reward.request': {0: 'type', 1: 'id', 2: 'items', 3: 'tower_info', 4: 'tower_special_reward'},
    'ret_guild_approve_resverve.request': {0: 'characterId', 1: 'isAgree', 2: 'state'},
    'ret_guild_battle_guess.request': {0: 'guildId'},
    'ret_guild_battle_info.request': {0: 'battle_info'},
    'ret_guild_battle_member.request': {0: 'guild_member_info'},
    'ret_guild_battle_rank.request': {0: 'guild_info'},
    'ret_guild_battle_state.request': {0: 'battle_info'},
    'ret_guild_create.request': {0: 'guild_info', 1: 'state', 2: 'donate_records'},
    'ret_guild_donate.request': {0: 'state', 1: 'id', 2: 'contribute', 3: 'all_contribute', 4: 'level', 5: 'exp'},
    'ret_guild_job_change.request': {0: 'characterId', 1: 'job', 2: 'state'},
    'ret_guild_join.request': {0: 'guildId'},
    'ret_guild_kick.request': {0: 'characterId', 1: 'state'},
    'ret_guild_leave.request': {0: 'guildId', 1: 'dismissTime'},
    'ret_guild_log.request': {0: 'logs'},
    'ret_guild_map_domine_top.request': {0: 'damage_list', 1: 'guild_damage_list', 2: 'my_rank', 3: 'my_damage', 4: 'my_rank2', 5: 'my_damage2'},
    'ret_guild_map_reward.request': {0: 'id', 1: 'state'},
    'ret_guild_member_info.request': {0: 'guild_member_info'},
    'ret_guild_req_info.request': {0: 'guild_info', 1: 'donate_records', 2: 'all_contribute', 3: 'exist', 4: 'contribute'},
    'ret_guild_req_list.request': {0: 'guild_info', 1: 'applyGuildId', 2: 'curPage', 3: 'maxPage', 4: 'leave_time'},
    'ret_guild_score_info.request': {0: 'guild_battle_score_info'},
    'ret_guild_skill_level.request': {0: 'state', 1: 'guildSkillType', 2: 'contribute', 3: 'level'},
    'ret_guild_star.request': {0: 'guild_stars'},
    'ret_invite_join_team.request': {0: 'ok', 1: 'id'},
    'ret_level_reward.request': {0: 'level_reward'},
    'ret_mount_equip.request': {0: 'mount_info', 1: 'ID'},
    'ret_mount_info.request': {0: 'mount_info'},
    'ret_mount_use_color.request': {0: 'mount_info', 1: 'mountId', 2: 'colorId'},
    'ret_offline_chat.request': {0: 'chat_list'},
    'ret_open_guild_boss.request': {0: 'ok', 1: 'guild_boss'},
    'ret_open_guild_shop.request': {0: 'type', 1: 'shop_list'},
    'ret_open_item_package.request': {0: 'items'},
    'ret_random_online_character_list.request': {0: 'friend_list'},
    'ret_re_name.request': {0: 'name', 1: 'state'},
    'ret_req_guild_skill.request': {0: 'guild_skill'},
    'ret_request_30_day_info.request': {0: 'cur_sign', 1: 'replenish', 2: 'sys_sign', 3: 'cur_sign_state', 4: 'replenish_sign_state', 5: 'count', 6: 'str'},
    'ret_request_activity_info.request': {0: 'activity_info'},
    'ret_request_big_pack.request': {0: 'ID', 1: 'state', 2: 'end_time', 3: 'special_big_packs'},
    'ret_request_daily_active.request': {0: 'daily_actives', 1: 'daily_rewards', 2: 'score'},
    'ret_request_daily_buy.request': {0: 'daily_buys'},
    'ret_request_dance_info.request': {0: 'curUse', 1: 'dance_info', 2: 'type'},
    'ret_request_first_buy.request': {0: 'ID', 1: 'state'},
    'ret_request_guild_boss.request': {0: 'guild_boss', 1: 'guild_battle_info', 2: 'level', 3: 'dance_state_info', 4: 'guild_map_info'},
    'ret_request_guild_map_info.request': {0: 'guild_map_info'},
    'ret_request_invest_pack.request': {0: 'invest_pack'},
    'ret_request_level_pack.request': {0: 'level_pack'},
    'ret_request_random_rank_pvp_opponent.request': {0: 'opponentNum', 1: 'characters', 2: 'rankPos'},
    'ret_request_retrieve_info.request': {0: 'info'},
    'ret_request_sign_week_info.request': {0: 'cur_sign', 1: 'cur_sign_state', 2: 'complete'},
    'ret_request_survive_top.request': {0: 'score_infos', 1: 'my_rank', 2: 'my_score', 3: 'end_time'},
    'ret_request_top_rank_pvp_list.request': {0: 'curPage', 1: 'maxPage', 2: 'sort_items'},
    'ret_request_tower_copy_info.request': {0: 'tower_info', 1: 'tower_special_reward'},
    'ret_request_update_friend_useinfo.request': {0: 'friend_list', 1: 'type'},
    'ret_request_update_storagepack.request': {0: 'gameitems'},
    'ret_request_wild_boss_info.request': {0: 'activity_info'},
    'ret_require_vip_info.request': {0: 'vip'},
    'ret_require_vip_reward.request': {0: 'vip'},
    'ret_search_guild.request': {0: 'guild', 1: 'rank'},
    'ret_search_online_character_by_name.request': {0: 'friend_list'},
    'ret_set_guild_battle_member.request': {0: 'state'},
    'ret_sign_30_day.request': {0: 'cur_sign', 1: 'replenish', 2: 'sys_sign', 3: 'cur_sign_state', 4: 'replenish_sign_state', 5: 'count', 6: 'str'},
    'ret_sign_week.request': {0: 'cur_sign', 1: 'cur_sign_state'},
    'ret_skill_use.request': {0: 'sendderId', 1: 'targetId', 2: 'skillId', 3: 'attack_list'},
    'ret_slot_info.request': {0: 'slot_info', 1: 'slot_datas', 2: 'slot_items'},
    'ret_slot_sum_reward.request': {0: 'items', 1: 'sumNum'},
    'ret_special_big_pack.request': {0: 'special_big_packs'},
    'ret_spin_slot.request': {0: 'slot_info', 1: 'slot_items'},
    'ret_title_req_level_up.request': {0: 'title_level', 1: 'title_exp'},
    'ret_top_rank_list.request': {0: 'sort_items', 1: 'sortType'},
    'ret_tower_reset.request': {0: 'state'},
    'ret_tower_wipe_out.request': {0: 'tower_info'},
    'ret_update_guild_star.request': {0: 'guild_stars'},
    'ret_use_item.request': {0: 'success', 1: 'indexId'},
    'ret_watch_video_info.request': {0: 'cur_times', 1: 'max_times', 2: 'every_time', 3: 'state'},
    'retrieve_account.request': {0: 'id'},
    'retrieve_info': {0: 'ID', 1: 'state', 2: 'count'},
    'runtime_agent': {0: 'attribute', 1: 'attribute_all'},
    'sample_activity_result.request': {0: 'win', 1: 'type', 2: 'id'},
    'sample_copy_result.request': {0: 'win', 1: 'type'},
    'score_info': {0: 'id', 1: 'name', 2: 'value'},
    'search_guild.request': {0: 'name', 1: 'guildId', 2: 'rank'},
    'search_online_character_by_name.request': {0: 'name'},
    'select_pk_character.request': {0: 'characterId'},
    'sell_item.request': {0: 'indexId', 1: 'itemCount', 2: 'type'},
    'send_daily_mission.request': {0: 'mission'},
    'send_dialog_notify.request': {0: 'type', 1: 'key', 2: 'parm'},
    'send_escort_info.request': {0: 'npcid', 1: 'lineIndex', 2: 'mapInfoId'},
    'send_mail.request': {0: 'receiveId', 1: 'context'},
    'send_mail_box.request': {0: 'subject', 1: 'context', 2: 'email'},
    'set_guild_battle_member.request': {0: 'list'},
    'set_mission_param.request': {0: 'missionId', 1: 'paramindex', 2: 'param'},
    'set_mission_state.request': {0: 'missionId', 1: 'missionstate'},
    'shop_item': {0: 'ID', 1: 'ItemID', 2: 'Quality', 3: 'PriceType', 4: 'Price', 5: 'Limit', 6: 'curNum', 7: 'Discount', 8: 'Class'},
    'show_damage_board.request': {0: 'damges'},
    'show_player_damage_board.request': {0: 'id', 1: 'hp'},
    'show_reward_items_tips.request': {0: 'items'},
    'sign_30_day.request': {0: 'day'},
    'sign_bar_fight.request': {0: 'ID'},
    'sign_week.request': {0: 'day'},
    'single_copy_scene_npc_die.request': {0: 'characterId', 1: 'npcdataid', 2: 'pos_x', 3: 'pos_z', 4: 'type'},
    'skill_info': {0: 'skillId', 1: 'skillLevel', 2: 'indexPos', 3: 'unlockLevel', 4: 'indexPos2', 5: 'disable'},
    'skill_level_up.request': {0: 'skillId', 1: 'curLevel', 2: 'all'},
    'skill_use.request': {0: 'targetId', 1: 'skillId', 2: 'combo', 3: 'attack_list', 4: 'parm'},
    'slot_data': {0: 'ID', 1: 'RewardMap', 2: 'RewardEffect', 3: 'Desc', 4: 'Rank', 5: 'ShowRewardID', 6: 'PriceType', 7: 'PriceCost'},
    'slot_info': {0: 'curNum', 1: 'sumNum'},
    'slot_item': {0: 'uuid', 1: 'ID', 2: 'items'},
    'sort_item': {0: 'id', 1: 'score', 2: 'name', 3: 'profession', 4: 'sortType', 5: 'parm1', 6: 'parm2', 7: 'parm3', 8: 'parm4'},
    'special_big_pack': {0: 'ID', 1: 'state', 2: 'remain_times', 3: 'end_time'},
    'spin_slot.request': {0: 'ID'},
    'start_enter_game.request': {0: 'state'},
    'start_participate_dance.request': {0: 'curUse', 1: 'dance_info'},
    'stop_random_select_team.request': {0: 'id', 1: 'type1'},
    'survive_battle_finish.request': {0: 'info'},
    'syn_friend_info.request': {0: 'friend'},
    'syn_rank_pvp_data.request': {0: 'combValue', 1: 'times', 2: 'rankPos', 3: 'bestRankPos', 4: 'rewards', 5: 'preRankPos', 6: 'winCount', 7: 'winRewards'},
    'sync_backpack_item.request': {0: 'gameitems'},
    'sync_badgepack_item.request': {0: 'gameitems'},
    'sync_common_data.request': {0: 'serverTime', 1: 'time_offset', 2: 'daily_mission_refresh_time', 3: 'pvp_scale', 4: 'first_buy', 5: 'big_pack', 6: 'adfree', 7: 'tips', 8: 'func_info', 9: 'push', 10: 'guildId', 11: 'seed', 12: 'server_level', 13: 'start_time'},
    'sync_copyscenes_info.request': {0: 'copyscenes'},
    'sync_dance_state_info.request': {0: 'state', 1: 'open', 2: 'dance_state_info'},
    'sync_fashion_backpack_item.request': {0: 'gameitems'},
    'sync_guild_new_member.request': {0: 'guild_member_info'},
    'sync_item_pack.request': {0: 'gameitems'},
    'sync_mission.request': {0: 'missions', 1: 'last_missionId', 2: 'sidedone_mission'},
    'sync_random_team_state.request': {0: 'state', 1: 'id', 2: 'type'},
    'sync_skill_info.request': {0: 'skill_dict', 1: 'isLevelUp'},
    'sync_skill_info.response': {0: 'isLevelUp'},
    'sync_watch_video_info.request': {0: 'cur_times', 1: 'max_times', 2: 'every_time'},
    'take_item_storagepack.request': {0: 'indexId'},
    'team': {0: 'id', 1: 'teamleader', 2: 'count', 3: 'isVerfiy', 4: 'teammembers', 5: 'goalId', 6: 'minLevel', 7: 'maxLevel', 8: 'recruit'},
    'team_kick.request': {0: 'characterId'},
    'teammember': {0: 'id', 1: 'teamid', 2: 'name', 3: 'level', 4: 'profession', 5: 'combValue', 6: 'mapInfoId', 7: 'lineIndex', 8: 'memberType', 9: 'hp', 10: 'max_hp', 11: 'vip', 12: 'time', 13: 'apply_time', 14: 'visual', 15: 'curNum', 16: 'guildId', 17: 'guildName'},
    'tianti_req_win_count_rewards.request': {0: 'index'},
    'tiantti_result.request': {0: 'win', 1: 'items', 2: 'rankPos1', 3: 'rankPos2', 4: 'bestRankPos', 5: 'type'},
    'tower_floor': {0: 'floorID', 1: 'complete'},
    'tower_info': {0: 'floor', 1: 'cur_floor', 2: 'times', 3: 'sum_time', 4: 'wipe_out_state', 5: 'wipe_time', 6: 'max_floor'},
    'tower_special_reward': {0: 'floor', 1: 'state'},
    'tower_wipe_out.request': {0: 'wipeType'},
    'tutorial_finish.request': {0: 'type'},
    'unequip_badge.request': {0: 'indexId'},
    'unequip_fashion_item.request': {0: 'indexId'},
    'unequip_item.request': {0: 'indexId'},
    'unlock_function_complete.request': {0: 'ID', 1: 'state'},
    'update_client_state.request': {0: 'id', 1: 'state'},
    'update_copyscene_info.request': {0: 'copyscene'},
    'update_game_server.response': {0: 'game_server'},
    'update_guild_dance_time.request': {0: 'index'},
    'update_guild_star.request': {0: 'ID'},
    'update_item.request': {0: 'containertype', 1: 'indexId', 2: 'gameitem'},
    'update_line_state.request': {0: 'mapInfoId', 1: 'line_count', 2: 'line_states'},
    'update_misison_complete.request': {0: 'missionId'},
    'update_misison_parm.request': {0: 'missionId', 1: 'paramType', 2: 'paramValue'},
    'update_player_map_info.request': {0: 'characterId'},
    'update_player_map_info.response': {0: 'state', 1: 'mapid', 2: 'pos'},
    'update_queue_rank.request': {0: 'rank', 1: 'remain_time'},
    'update_sex_mini_score.request': {0: 'id', 1: 'score'},
    'update_team.request': {0: 'team'},
    'update_team_member.request': {0: 'teamid', 1: 'member'},
    'update_team_setting.request': {0: 'isVerfiy'},
    'use_dance.request': {0: 'id'},
    'use_dance_sound_box.request': {0: 'index', 1: 'x', 2: 'z'},
    'use_item.request': {0: 'indexId', 1: 'x', 2: 'z'},
    'use_skill_buff.request': {0: 'buffs'},
    'verfiy.request': {0: 'id', 1: 'key', 2: 'versionCode'},
    'verfiy.response': {0: 'state', 1: 'session', 2: 'game_server', 3: 'user_server', 4: 'facebook_bind', 5: 'versionCode', 6: 'dataVersionCode', 7: 'downloadFlag', 8: 'notice', 9: 'notice_version', 10: 'facebook_bind1', 11: 'google_bind'},
    'vip': {0: 'id', 1: 'count', 2: 'state'},
    'visitor.response': {0: 'id', 1: 'key', 2: 'state'},
    'watch_video_info.request': {0: 'state'},
    'weapon_inhert.request': {0: 'index1', 1: 'index2', 2: 'attribute_index1', 3: 'attribute_index2'},
    'wild_boss': {0: 'bossId', 1: 'state', 2: 'refreshTime', 3: 'killId', 4: 'killName', 5: 'mapInfoId', 6: 'lineIndex', 7: 'posx', 8: 'posz'},
}


# ==============================================================================
# SPROTO PROTOCOL ENCODING / DECODING
# ==============================================================================

def get_val_int(fields, tag, default=0):
    val = fields.get(tag)
    if val is None:
        return default
    if isinstance(val, int):
        return val
    if isinstance(val, (bytes, bytearray)):
        if len(val) == 4:
            return struct.unpack("<i", val)[0]
        if len(val) == 8:
            return struct.unpack("<q", val)[0]
        if len(val) == 1:
            return val[0]
    return default

def get_val_str(fields, tag, default=""):
    val = fields.get(tag)
    if val is None:
        return default
    if isinstance(val, bytes):
        return val.decode("utf-8", "ignore")
    return str(val)

def encode_sproto(fields, fn=None):
    if not fields:
        return struct.pack("<H", 0)
    fields = sorted(fields, key=lambda x: x[0])
    header = []
    body = bytearray()
    last_tag = -1
    for tag, val in fields:
        skip = tag - last_tag - 1
        if skip > 0:
            header.append(2 * (skip - 1) + 1)
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
                v = val.encode("utf-8")
            elif isinstance(val, list):
                if val and isinstance(val[0], int):
                    v = b"\x08" + b"".join([struct.pack("<q", item) for item in val])
                else:
                    items = []
                    for item in val:
                        if isinstance(item, str):
                            item = item.encode("utf-8")
                        elif isinstance(item, (bytes, bytearray)):
                            pass
                        else:
                            item = str(item).encode("utf-8")
                        items.append(struct.pack("<I", len(item)) + item)
                    v = b"".join(items)
            elif isinstance(val, dict):
                items = []
                for item in val.values():
                    if isinstance(item, (bytes, bytearray)):
                        items.append(struct.pack("<I", len(item)) + item)
                    else:
                        items.append(struct.pack("<I", 1) + (b"\x01" if item else b"\x00"))
                v = b"".join(items)
            else:
                v = val
            body += struct.pack("<I", len(v)) + v
        last_tag = tag

    fn_val = fn if fn is not None else len(header)
    res = struct.pack("<H", fn_val)
    for h in header:
        res += struct.pack("<H", h)
    return res + body

def sproto_pack(data):
    out = bytearray()
    for i in range(0, len(data), 8):
        chunk = data[i:i+8]
        if len(chunk) < 8:
            chunk += b"\x00" * (8 - len(chunk))
        mask = 0
        for j in range(8):
            if chunk[j] != 0:
                mask |= (1 << j)
        if mask == 0xFF:
            out.append(0xFF)
            out.append(0)
            out.extend(chunk)
        else:
            out.append(mask)
            for j in range(8):
                if mask & (1 << j):
                    out.append(chunk[j])
    return bytes(out)

def sproto_unpack(data):
    out = bytearray()
    i = 0
    n = len(data)
    while i < n:
        mask = data[i]
        i += 1
        if mask == 0xFF:
            if i >= n:
                break
            count = (data[i] + 1) * 8
            i += 1
            out.extend(data[i:i+count])
            i += count
        else:
            for bit in range(8):
                if mask & (1 << bit):
                    if i < n:
                        out.append(data[i])
                        i += 1
                else:
                    out.append(0)
    return bytes(out)

def decode_sproto(data, offset=0):
    if len(data) < offset + 2:
        return {}
    fn = struct.unpack("<H", data[offset:offset+2])[0]
    h_ptr, b_ptr = offset + 2, offset + 2 + fn * 2
    fields, curr_tag = {}, -1
    for i in range(fn):
        v = struct.unpack("<H", data[h_ptr + i * 2 : h_ptr + i * 2 + 2])[0]
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

def decode_sproto_list(data):
    if not data:
        return []
    res = []
    ptr = 0
    while ptr < len(data):
        if ptr + 4 > len(data):
            break
        l = struct.unpack("<I", data[ptr:ptr+4])[0]
        res.append(data[ptr+4:ptr+4+l])
        ptr += 4 + l
    return res

# ==============================================================================
# PROTOCOL & SCHEMA DIAGNOSTIC PRINTER
# ==============================================================================

def format_sproto_field_value(val):
    if isinstance(val, (bytes, bytearray)):
        # Check if printable string
        try:
            s = val.decode("utf-8")
            if s.isprintable() and len(s) > 0:
                return repr(s)
        except:
            pass
        # Check if nested Sproto structure
        if len(val) >= 2:
            try:
                fn = struct.unpack("<H", val[:2])[0]
                if 0 < fn <= 30 and len(val) >= 2 + fn * 2:
                    nested = decode_sproto(val, 0)
                    if nested:
                        return f"[Nested Sproto, {len(nested)} fields]: {nested}"
            except:
                pass
        if len(val) <= 16:
            return f"bytes({val.hex()})"
        return f"bytes({val[:16].hex()}... len={len(val)})"
    return repr(val)

def format_decoded_body(tag, body, is_response=False):
    proto_info = PROTOCOLS.get(tag)
    class_name = None
    if proto_info:
        class_name = proto_info[2] if is_response else proto_info[1]
    
    field_names = CLASS_FIELDS.get(class_name, {}) if class_name else {}
    lines = []
    for f_tag, f_val in sorted(body.items()):
        name_str = f" [{field_names[f_tag]}]" if f_tag in field_names else ""
        lines.append(f"      tag {f_tag}{name_str} = {format_sproto_field_value(f_val)}")
    return "\n".join(lines) if lines else "      (Empty body)"

def log_rx(msg, session, body):
    proto_info = PROTOCOLS.get(msg)
    proto_name = proto_info[0] if proto_info else "UNKNOWN_PROTOCOL"
    has_resp = bool(proto_info and proto_info[2])
    
    sess_str = str(session) if session is not None else "None"
    wait_str = f"RPC Response Expected (Session {session})" if session is not None else ("Notification (One-Way)" if not has_resp else "Server May Push")
    
    print("\n" + "=" * 80)
    print(f"[RX] MSG {msg} [{proto_name}] | Session: {sess_str} | Client Expects: {wait_str}")
    formatted = format_decoded_body(msg, body, is_response=False)
    print(formatted)
    print("=" * 80)

def log_tx_response(session, msg_tag, body_dict=None, raw_len=0):
    proto_info = PROTOCOLS.get(msg_tag)
    proto_name = proto_info[0] if proto_info else "UNKNOWN"
    print(f"[TX RSP] Session: {session} (re: MSG {msg_tag} [{proto_name}]) | Size: {raw_len}b")
    if body_dict:
        formatted = format_decoded_body(msg_tag, body_dict, is_response=True)
        print(formatted)

def log_tx_push(tag, body_dict=None, raw_len=0):
    proto_info = PROTOCOLS.get(tag)
    proto_name = proto_info[0] if proto_info else "UNKNOWN"
    print(f"[TX PUSH] TAG {tag} [{proto_name}] | Size: {raw_len}b")
    if body_dict:
        formatted = format_decoded_body(tag, body_dict, is_response=False)
        print(formatted)

# ==============================================================================
# DATA LOADERS (TEXTASSETS)
# ==============================================================================

missions_data = {}
rewards_data = {}
LEVEL_DATA = {}
MONSTER_DATA = {}
STATIC_NPC_DATA = {}
NPC_CONFIG = {}
MAP_CONFIG = {}
COPY_SCENE_CONFIG = {}
SHOW_REWARD_CONFIG = {}
MOUNT_CONFIG = {}
FUNCTION_DATA = {}
EQUIP_CONFIG = {}
SKILL_CONFIG = {}
EFF_CONFIG = {}
ADAPT_DATA = {}

def load_game_assets():
    global missions_data, rewards_data, LEVEL_DATA, MONSTER_DATA, STATIC_NPC_DATA
    global NPC_CONFIG, MAP_CONFIG, COPY_SCENE_CONFIG, SHOW_REWARD_CONFIG, MOUNT_CONFIG
    global FUNCTION_DATA, EQUIP_CONFIG, SKILL_CONFIG, EFF_CONFIG, ADAPT_DATA

    search_dirs = [
        os.path.join(SCRIPT_DIR, "assets", "Bundle", "TextAsset"),
        os.path.join(SCRIPT_DIR, "assets", "Bundle", "TextAssets"),
        os.path.join(SCRIPT_DIR, "Decompiled", "assets", "Bundle", "TextAsset"),
        os.path.join(SCRIPT_DIR, "Decompiled", "Decompiled", "assets", "Bundle", "TextAsset"),
    ]
    text_asset_root = None
    for d in search_dirs:
        if os.path.isdir(d):
            text_asset_root = d
            break

    if not text_asset_root:
        print("[WARN] TextAsset directory not found, using built-in defaults.")
        return

    print(f"[ASSET] Loading game data from: {text_asset_root}")

    def is_data(line):
        return line.startswith("*,") or ("," in line and line.split(",")[1].isdigit())

    # 1. BaseLvData
    lv_path = os.path.join(text_asset_root, "BaseLvData")
    if os.path.exists(lv_path):
        with open(lv_path, "r", encoding="utf-8") as f:
            for line in f:
                if is_data(line):
                    parts = line.strip().split(",")
                    if len(parts) > 20 and parts[1].isdigit():
                        lv = int(parts[1])
                        LEVEL_DATA[lv] = {
                            'exp': int(parts[3]),
                            'power': int(parts[2]),
                            'atk': [int(parts[4]), int(parts[11]), int(parts[18])],
                            'hp': [int(parts[5]), int(parts[12]), int(parts[19])],
                            'def': [int(parts[6]), int(parts[13]), int(parts[20])],
                            'hit': [int(parts[7]), int(parts[14]), int(parts[21])],
                            'eva': [int(parts[8]), int(parts[15]), int(parts[22])],
                            'cri': [int(parts[9]), int(parts[16]), int(parts[23])],
                            'res': [int(parts[10]), int(parts[17]), int(parts[24])],
                            'exd': [int(parts[25]), int(parts[25]), int(parts[25])],
                            'exr': [int(parts[26]), int(parts[26]), int(parts[26])],
                            'crd': [int(parts[27]), int(parts[27]), int(parts[27])],
                            'crr': [int(parts[28]), int(parts[28]), int(parts[28])],
                            'defa': int(parts[31]), 'dgea': int(parts[32]), 'resa': int(parts[33]),
                            'hita': int(parts[34]), 'cria': int(parts[35])
                        }
        print(f"[ASSET] BaseLvData: {len(LEVEL_DATA)} levels loaded.")

    # 2. MapInfoData
    map_path = os.path.join(text_asset_root, "MapInfoData")
    if os.path.exists(map_path):
        with open(map_path, "r", encoding="utf-8") as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 8 and parts[1].isdigit():
                    mid = parts[1]
                    MAP_CONFIG[mid] = {
                        'name': parts[2],
                        'scene': parts[3],
                        'type': int(parts[4]) if parts[4].isdigit() else 0,
                        'birth': parts[8],
                        'teleport_pos': parts[10] if len(parts) > 10 else "",
                    }
        print(f"[ASSET] MapInfoData: {len(MAP_CONFIG)} maps loaded.")

    # 3. MissionData
    md_path = os.path.join(text_asset_root, "MissionData")
    if os.path.exists(md_path):
        with open(md_path, "r", encoding="utf-8") as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 23 and parts[0] == "*" and parts[1].isdigit():
                    mid = parts[1]
                    missions_data[mid] = {
                        'id': mid,
                        'name': parts[2] if len(parts) > 2 else '',
                        'class': int(parts[6]) if parts[6].isdigit() else 0,
                        'logic_type': int(parts[7]) if parts[7].isdigit() else 0,
                        'logic_id': parts[9],
                        'target_id': parts[11],
                        'pre_id': parts[12],
                        'next_id': parts[14],
                        'min_level': int(parts[23]) if parts[23].isdigit() else 1,
                        'reward_ids': [parts[25] if len(parts)>25 else "", parts[27] if len(parts)>27 else "", parts[29] if len(parts)>29 else ""]
                    }
        print(f"[ASSET] MissionData: {len(missions_data)} missions loaded.")

    # 4. ShowRewardData
    rd_path = os.path.join(text_asset_root, "ShowRewardData")
    if os.path.exists(rd_path):
        with open(rd_path, "r", encoding="utf-8") as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 5 and parts[0] == "*" and parts[1].isdigit():
                    rid = parts[1]
                    exp, cash = 0, 0
                    items, amounts = [], []
                    for i in range(8):
                        idx_item = 3 + i * 3
                        idx_count = 5 + i * 3
                        if idx_count < len(parts) and parts[idx_item].isdigit():
                            iid = parts[idx_item]
                            icount = int(parts[idx_count]) if parts[idx_count].isdigit() else 1
                            if iid == "2001": exp += icount
                            elif iid == "1001": cash += icount
                            else:
                                items.append(iid)
                                amounts.append(icount)
                    rewards_data[rid] = {'exp': exp, 'cash': cash, 'items': items, 'amounts': amounts}
        print(f"[ASSET] ShowRewardData: {len(rewards_data)} rewards loaded.")

    # 5. FunctionData
    func_path = os.path.join(text_asset_root, "FunctionData")
    if os.path.exists(func_path):
        with open(func_path, "r", encoding="utf-8") as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 6 and parts[1].isdigit():
                    fid = parts[1]
                    FUNCTION_DATA[fid] = {
                        'class': int(parts[2]) if parts[2].isdigit() else 0,
                        'condition': int(parts[3]) if parts[3].isdigit() else 0,
                    }
        print(f"[ASSET] FunctionData: {len(FUNCTION_DATA)} functions loaded.")

    # 6. MountData
    mount_path = os.path.join(text_asset_root, "MountData")
    if os.path.exists(mount_path):
        with open(mount_path, "r", encoding="utf-8") as f:
            for line in f:
                parts = line.strip().split(",")
                if len(parts) > 10 and parts[1].isdigit():
                    mid = parts[1]
                    MOUNT_CONFIG[mid] = {
                        'id': mid,
                        'name': parts[2] if len(parts) > 2 else '',
                        'default_color': parts[10] if len(parts) > 10 else '1',
                    }
        print(f"[ASSET] MountData: {len(MOUNT_CONFIG)} vehicles loaded.")

# Load game assets immediately
load_game_assets()

# Built-in Fallbacks for BaseLvData
if not LEVEL_DATA:
    for lv in range(1, 81):
        hp = 3000 + (lv - 1) * 1140
        atk = 180 + (lv - 1) * 35
        df = 120 + (lv - 1) * 25
        exp_need = 300 + lv * 200
        LEVEL_DATA[lv] = {
            'exp': exp_need,
            'power': hp + atk * 15 + df * 10,
            'atk': [atk, atk, atk],
            'hp': [hp, hp, hp],
            'def': [df, df, df],
            'hit': [2800, 2800, 2800],
            'eva': [100, 100, 100],
            'cri': [350, 350, 350],
            'res': [0, 0, 0],
            'exd': [0, 0, 0], 'exr': [0, 0, 0], 'crd': [15000, 15000, 15000], 'crr': [0, 0, 0],
            'defa': 3158, 'dgea': 6317, 'resa': 3158, 'hita': 316, 'cria': 3158
        }

# ==============================================================================
# CHARACTER DATABASE & STATE MANAGEMENT
# ==============================================================================

def load_characters():
    for f in [CHAR_DB, BAK_DB]:
        if os.path.exists(f):
            try:
                with open(f, "r", encoding="utf-8") as fp:
                    d = json.load(fp)
                    if isinstance(d, dict):
                        return d
            except:
                pass
    return {}

def save_characters(data):
    try:
        with open(TMP_DB, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=2)
            f.flush()
            os.fsync(f.fileno())
        if os.path.exists(CHAR_DB):
            try:
                with open(CHAR_DB, "r", encoding="utf-8") as src, open(BAK_DB, "w", encoding="utf-8") as dst:
                    dst.write(src.read())
            except:
                pass
        os.replace(TMP_DB, CHAR_DB)
    except Exception as e:
        print(f"[ERROR] Failed to save characters: {e}")

all_characters = load_characters()

def get_account_chars(area_id, account_id):
    area_key = str(area_id)
    return all_characters.setdefault(area_key, {}).setdefault(str(account_id), [])

def init_character_fields(c):
    c.setdefault('level', 1)
    c.setdefault('exp', 0)
    c.setdefault('cash', 10000)
    c.setdefault('pos', [29860, 100, -17005, 0])
    c.setdefault('map_id', "11")
    c.setdefault('tutorial', 0)
    c.setdefault('skill_levels', {})
    c.setdefault('active_missions', {})
    c.setdefault('completed_side_missions', [])
    c.setdefault('last_main_mission_id', "-1")
    c.setdefault('inventory', [])
    c.setdefault('mounts', {
        '1001': {'state': 2, 'select': '1', 'colors': ['1']},
        '1002': {'state': 1, 'select': '1', 'colors': ['1']},
        '1003': {'state': 1, 'select': '1', 'colors': ['1']},
        '1004': {'state': 1, 'select': '1', 'colors': ['1']},
        '1005': {'state': 1, 'select': '1', 'colors': ['1']},
        '1006': {'state': 1, 'select': '1', 'colors': ['1']},
        '1007': {'state': 1, 'select': '1', 'colors': ['1']},
    })
    c.setdefault('equipped_mount_id', '1001')
    stats = get_character_stats(c)
    c.setdefault('hp', stats['hp_max'])

def get_character_stats(c):
    lv = max(1, min(c.get('level', 1), max(LEVEL_DATA.keys())))
    prof = c.get('prof', 0)
    ld = LEVEL_DATA.get(lv, LEVEL_DATA[1])
    atk = ld['atk'][prof] + 180
    hp_max = ld['hp'][prof]
    df = ld['def'][prof]
    power = int((atk * 16 + hp_max + df * 11) * 3.0)
    return {
        'atk': atk, 'hp_max': hp_max, 'def': df,
        'hit': ld['hit'][prof], 'eva': ld['eva'][prof],
        'cri': ld['cri'][prof], 'res': ld['res'][prof],
        'power': power, 'lv': lv, 'exp': c.get('exp', 0),
        'defa': ld['defa'], 'dgea': ld['dgea'], 'resa': ld['resa'],
        'hita': ld['hita'], 'cria': ld['cria'],
        'exd': ld['exd'][prof], 'exr': ld['exr'][prof],
        'crd': ld['crd'][prof], 'crr': ld['crr'][prof]
    }

PROF_SKILLS = {
    0: {"atk": ["101", "102", "103"], "dodge": "104", "actives": ["105", "106", "107", "108", "109", "110"]},
    1: {"atk": ["201", "202", "203"], "dodge": "204", "actives": ["205", "206", "207", "208", "209", "210"]},
    2: {"atk": ["301", "302", "303"], "dodge": "304", "actives": ["305", "306", "307", "308", "309", "310"]}
}
SKILL_UNLOCK_LVS = [1, 5, 10, 15, 20, 25]

def build_skills_map(prof, char_level, skill_levels=None):
    if skill_levels is None:
        skill_levels = {}
    p = PROF_SKILLS.get(int(prof), PROF_SKILLS[0])
    smap = {}
    for sid in p["atk"]:
        smap[sid] = encode_sproto([(0, sid), (1, skill_levels.get(sid, 0)), (2, 0), (3, 1), (4, 0), (5, False)])
    smap[p["dodge"]] = encode_sproto([(0, p["dodge"]), (1, skill_levels.get(p["dodge"], 0)), (2, 3), (3, 1), (4, 1), (5, False)])
    for i, sid in enumerate(p["actives"]):
        unlock_lv = SKILL_UNLOCK_LVS[i]
        if char_level >= unlock_lv:
            smap[sid] = encode_sproto([
                (0, sid), (1, skill_levels.get(sid, 0)), (2, 4 + i), (3, unlock_lv), (4, 2 + i), (5, False)
            ])
    return smap

def get_visual(name, prof, mount_id='', mount_color='', mount_state=0):
    m = {0: {"m":"100","h":"XD_A_T","b":"XD_A_S","l":"XD_A_X","w":"XD_A_WQ"},
         1: {"m":"104","h":"QJ_A_T","b":"QJ_A_S","l":"QJ_A_X","w":"QJ_A_WQ"},
         2: {"m":"105","h":"NQS_A_T","b":"NQS_A_S","l":"NQS_A_X","w":"NQS_A_WQ"}}
    v = m.get(prof, m[1])
    fields = [(0, name), (1, v["m"]), (2, v["h"]), (3, v["b"]), (4, v["l"]), (5, v["w"]), (10, 0)]
    if mount_id:
        fields.extend([(12, str(mount_id)), (13, int(mount_state)), (14, str(mount_color or ''))])
    return encode_sproto(fields)

def get_movement(x, y, z, o=0):
    pos = encode_sproto([(0, x), (1, y), (2, z), (3, o)])
    return encode_sproto([(0, pos), (1, pos)])

def get_char_ov(c, sort_index=0):
    stats = get_character_stats(c)
    gen = encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('map_id', '11'))), (4, c.get('tutorial', 0))])
    attr = encode_sproto([(0, stats['lv']), (1, stats['power'])])
    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr),
        (3, get_visual(c.get('name', 'Hero'), c.get('prof', 0))),
        (4, sort_index), (5, 0)
    ])

def get_full_char(c):
    stats = get_character_stats(c)
    gen = encode_sproto([(0, c.get('name', 'Hero')), (1, c.get('prof', 0)), (2, 1), (3, str(c.get('map_id', '11'))), (4, c.get('tutorial', 0))])
    hp_cur = c.get('hp', stats['hp_max'])
    attr_oth = encode_sproto([(0, hp_cur), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)])
    prop = encode_sproto([(13, c.get('cash', 1000)), (14, 100), (15, 10), (16, 0), (17, 0), (18, 0)])
    pos = c.get('pos', [29860, 100, -17005, 0])
    mv = get_movement(pos[0], pos[1], pos[2], pos[3])
    attr_run = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    run = encode_sproto([(6, attr_run), (7, attr_all)])
    skills_map = build_skills_map(c.get('prof', 0), stats['lv'], c.get('skill_levels', {}))
    starter_wids = {0: "10001", 1: "20001", 2: "30001"}
    wid = starter_wids.get(c.get('prof', 0), "10001")
    w1 = encode_sproto([(0, 5), (1, wid), (2, True), (3, 1), (5, 1), (6, 1), (7, [0]*8)])
    equip_map = {5: w1}
    mount_id = c.get('equipped_mount_id', '1001')
    return encode_sproto([
        (0, c['id']), (1, gen), (2, attr_oth), (5, prop),
        (6, get_visual(c.get('name', 'Hero'), c.get('prof', 0), mount_id, '1', 0)),
        (7, mv), (8, skills_map), (9, equip_map), (12, 0), (13, run), (15, 2)
    ])

def sync_char_attrs_rpc(conn, picked_char, send_rpc_push):
    stats = get_character_stats(picked_char)
    hp_cur = picked_char.get('hp', stats['hp_max'])
    attr_oth = encode_sproto([(0, hp_cur), (1, stats['exp']), (2, stats['lv']), (3, stats['power']), (15, 1)])
    attr_base = encode_sproto([(0, stats['hp_max']), (2, stats['atk']), (3, stats['def'])])
    attr_all_data = [
        (0, stats['hp_max']), (2, stats['atk']), (3, stats['def']),
        (4, stats['hit']), (5, stats['eva']), (6, stats['cri']), (7, stats['res']),
        (8, stats['exd']), (9, stats['exr']), (10, stats['crd']), (11, stats['crr']),
        (12, stats['defa']), (13, 500), (17, stats['dgea']), (18, stats['resa']), (19, stats['hita']), (20, stats['cria'])
    ]
    attr_all = encode_sproto(attr_all_data)
    prop = encode_sproto([(13, picked_char.get('cash', 0))])
    aoi_attr = encode_sproto([(0, picked_char['id']), (1, attr_oth), (2, attr_base), (3, attr_all), (5, prop)])
    send_rpc_push(510, encode_sproto([(0, aoi_attr)]))

def sync_mission_data(picked_char):
    own_missions_list = []
    for mid, mdata in picked_char.get('active_missions', {}).items():
        parm = mdata.get('parm', [0]*8)
        if len(parm) < 8:
            parm += [0] * (8 - len(parm))
        m_bytes = encode_sproto([
            (0, str(mid)), (1, int(mdata['state'])), (2, 0), (3, [int(x) for x in parm])
        ])
        own_missions_list.append(m_bytes)
    last_main = picked_char.get('last_main_mission_id', "-1")
    return encode_sproto([
        (0, own_missions_list), (1, str(last_main)),
        (2, [int(x) for x in picked_char.get('completed_side_missions', []) if x])
    ])

def sync_inventory_data(picked_char):
    items = {}
    inv = picked_char.get('inventory', [])
    for i, item in enumerate(inv):
        guid = i + 10000
        items[guid] = encode_sproto([(0, guid), (1, item['id']), (2, True), (5, item['amount'])])
    return encode_sproto([(0, items)])

def spawn_map_npcs(conn, map_id, picked_char, send_rpc_push):
    # Spawn Hulks for Quest 1001 if on Map 11
    if str(map_id) == "11":
        # Spawn Quest NPCs
        for i in range(2):
            inst_id = 990100 + i
            NPC_HP_MAP[inst_id] = 3000
            attr = encode_sproto([
                (0, inst_id), (1, "9901"), (2, 3000), (3, 3000), (4, 150), (5, 50),
                (15, 29860 + (i * 200)), (16, -17005 + (i * 200)), (17, 0), (18, 1), (21, "Hulk")
            ])
            send_rpc_push(509, encode_sproto([(0, attr)]))

# ==============================================================================
# HTTP ASSET HANDLER (Supports APK updates on port 9555)
# ==============================================================================

def serve_http(conn, initial_data):
    try:
        request_text = initial_data.decode("utf-8", "ignore")
        while "\r\n\r\n" not in request_text:
            chunk = conn.recv(1024)
            if not chunk:
                break
            request_text += chunk.decode("utf-8", "ignore")
        lines = request_text.split("\r\n")
        if not lines:
            return
        path = lines[0].split(" ")[1].lstrip("/")
        
        search_paths = [
            os.path.join(SCRIPT_DIR, "assets", path),
            os.path.join(SCRIPT_DIR, "Decompiled", "assets", path),
            os.path.join(SCRIPT_DIR, path),
        ]
        local_path = None
        for p in search_paths:
            if os.path.exists(p) and os.path.isfile(p):
                local_path = p
                break
        
        if local_path:
            with open(local_path, "rb") as f:
                content = f.read()
            resp = (b"HTTP/1.1 200 OK\r\n"
                    b"Content-Length: " + str(len(content)).encode() + b"\r\n"
                    b"Content-Type: application/octet-stream\r\n"
                    b"Access-Control-Allow-Origin: *\r\n"
                    b"Connection: close\r\n\r\n")
            conn.sendall(resp + content)
            print(f"[HTTP 200] Served: {path} ({len(content)} bytes)")
        else:
            conn.sendall(b"HTTP/1.1 404 Not Found\r\n\r\n")
            print(f"[HTTP 404] Not Found: {path}")
    except Exception as e:
        print(f"[HTTP ERROR] {e}")
    finally:
        try: conn.close()
        except: pass

# ==============================================================================
# MAIN CLIENT TCP CONNECTION HANDLER
# ==============================================================================

def client_handler(conn, addr):
    print(f"\n[+] CLIENT CONNECTED from {addr}")
    acc_id = "0"
    cur_areaId = "1"
    picked_char = None
    send_lock = threading.Lock()

    def send_rpc_push(tag, data, data_dict=None):
        try:
            ph_p = encode_sproto([(0, tag)])
            pf_p = sproto_pack(ph_p + data)
            with send_lock:
                conn.sendall(struct.pack(">H", len(pf_p)) + pf_p)
            log_tx_push(tag, data_dict, len(data))
        except Exception as e:
            print(f"[!] FAILED TO SEND PUSH TAG {tag}: {e}")

    def send_rpc_response(session, msg_tag, resp_data, resp_dict=None):
        try:
            ph = encode_sproto([(1, session)])
            pf = sproto_pack(ph + resp_data)
            with send_lock:
                conn.sendall(struct.pack(">H", len(pf)) + pf)
            log_tx_response(session, msg_tag, resp_dict, len(resp_data))
        except Exception as e:
            print(f"[!] FAILED TO SEND RESPONSE FOR SESSION {session}: {e}")

    try:
        # Check for HTTP GET / HEAD request
        peek = conn.recv(4, socket.MSG_PEEK)
        if peek.startswith(b"GET ") or peek.startswith(b"HEAD"):
            serve_http(conn, b"")
            return

        def recv_exact(n):
            buf = bytearray()
            while len(buf) < n:
                chunk = conn.recv(n - len(buf))
                if not chunk:
                    return None
                buf.extend(chunk)
            return bytes(buf)

        while True:
            h_bytes = recv_exact(2)
            if not h_bytes:
                break
            size = struct.unpack(">H", h_bytes)[0]
            data = recv_exact(size)
            if not data:
                break

            # Unpack Sproto
            raw = sproto_unpack(data)
            pkg = decode_sproto(raw, 0)
            msg = get_val_int(pkg, 0)
            session = get_val_int(pkg, 1, None)

            off = 2 + (struct.unpack("<H", raw[:2])[0] * 2)
            body = decode_sproto(raw, off)

            # Log packet with full diagnostic details
            log_rx(msg, session, body)

            # ------------------------------------------------------------------
            # HANDLER: MSG 2 (visitor)
            # ------------------------------------------------------------------
            if msg == 2:
                new_id = acc_id if acc_id and acc_id != "0" else f"100{random.randint(1000, 9999)}"
                acc_id = new_id
                resp_fields = [(0, 0), (1, new_id), (2, "key123")]
                send_rpc_response(session, msg, encode_sproto(resp_fields), dict(resp_fields))

            # ------------------------------------------------------------------
            # HANDLER: MSG 3 (verfiy)
            # ------------------------------------------------------------------
            elif msg == 3:
                req_id = get_val_str(body, 0, acc_id)
                if req_id: acc_id = req_id
                resp_fields = [(0, 0), (1, int(time.time()) % 100000), (2, ""), (3, ""), (4, GAME_VERSION), (5, DATA_VERSION)]
                send_rpc_response(session, msg, encode_sproto(resp_fields), dict(resp_fields))

            # ------------------------------------------------------------------
            # HANDLER: MSG 4 (login) - Login Handoff from 9777 to 9555
            # ------------------------------------------------------------------
            elif msg == 4:
                acc_id = get_val_str(body, 1, acc_id)
                sid = get_val_int(body, 5, 1)
                cur_areaId = "1"
                resp_fields = [
                    (0, 2), (1, GAME_VERSION), (2, DATA_VERSION), (3, 1),
                    (4, 10000), (12, random.randint(1, 10000))
                ]
                send_rpc_response(session, msg, encode_sproto(resp_fields), dict(resp_fields))

            # ------------------------------------------------------------------
            # HANDLER: MSG 103 (character_list)
            # ------------------------------------------------------------------
            elif msg == 103:
                chars = get_account_chars(cur_areaId, acc_id)
                chars.sort(key=lambda x: x.get('last_played', 0), reverse=True)
                ov_list = [get_char_ov(c, i) for i, c in enumerate(chars)]
                resp_fields = [(0, ov_list)]
                send_rpc_response(session, msg, encode_sproto(resp_fields), {0: f"[{len(ov_list)} characters]"})

            # ------------------------------------------------------------------
            # HANDLER: MSG 118 (request_random_name)
            # ------------------------------------------------------------------
            elif msg == 118:
                names = ["Tony", "Blake", "Viper", "Shadow", "Ghost", "Ace", "Rider", "Hunter", "Blaze", "Storm"]
                rand_name = f"{random.choice(names)}_{random.randint(100, 999)}"
                resp_fields = [(0, rand_name)]
                send_rpc_response(session, msg, encode_sproto(resp_fields), dict(resp_fields))

            # ------------------------------------------------------------------
            # HANDLER: MSG 104 (character_create)
            # ------------------------------------------------------------------
            elif msg == 104:
                c_data = decode_sproto(body.get(0, b""))
                name = get_val_str(c_data, 0, "Hero")
                prof = get_val_int(c_data, 1, 0)
                cid = int(time.time() * 1000) % 1000000000

                area_key = str(cur_areaId)
                if area_key not in all_characters: all_characters[area_key] = {}
                if acc_id not in all_characters[area_key]: all_characters[area_key][acc_id] = []

                new_char = {'id': cid, 'name': name, 'prof': prof}
                init_character_fields(new_char)
                # Assign starter mission 1001
                new_char['active_missions']['1001'] = {'state': 1, 'parm': [0]*8}
                all_characters[area_key][acc_id].append(new_char)
                save_characters(all_characters)

                resp_fields = [(0, get_char_ov(new_char, 0)), (1, 0)]
                send_rpc_response(session, msg, encode_sproto(resp_fields), {0: "[character_overview]", 1: 0})

            # ------------------------------------------------------------------
            # HANDLER: MSG 105 (character_pick) - Enter World Sequence
            # ------------------------------------------------------------------
            elif msg == 105:
                char_id = get_val_int(body, 0)
                chars = get_account_chars(cur_areaId, acc_id)
                picked_char = next((c for c in chars if c['id'] == char_id), None)
                
                resp_fields = [(0, 1 if picked_char else 0)]
                send_rpc_response(session, msg, encode_sproto(resp_fields), dict(resp_fields))

                if picked_char:
                    picked_char['last_played'] = int(time.time())
                    init_character_fields(picked_char)
                    if not picked_char.get('active_missions'):
                        picked_char['active_missions']['1001'] = {'state': 1, 'parm': [0]*8}
                    save_characters(all_characters)

                    print(f"\n[*] ENTERING WORLD with Character: {picked_char['name']} (ID: {picked_char['id']}, Lv: {picked_char['level']})")

                    # PUSH 1: TAG 614 (sync_common_data)
                    fids = ["100", "107", "108", "3001", "3010", "3013", "3014", "3015", "3030", "4014", "4026", "4061", "4064", "4081", "4084"]
                    funcs = {fid: encode_sproto([(0, fid), (1, 1)]) for fid in fids}
                    send_rpc_push(614, encode_sproto([
                        (0, int(time.time())), (2, 0), (4, 10000), (9, funcs),
                        (12, random.randint(1, 10000)), (13, 1), (14, int(time.time()))
                    ]), {0: int(time.time()), 8: f"[{len(funcs)} functions unlocked]"})

                    # PUSH 2: TAG 611 (sync_item_pack)
                    send_rpc_push(611, sync_inventory_data(picked_char), {0: f"[{len(picked_char['inventory'])} items]"})

                    # PUSH 3: TAG 592 (sync_package)
                    send_rpc_push(592, encode_sproto([(0, {})]), {0: "{}"})

                    # PUSH 4: TAG 616 (sync_fashion)
                    send_rpc_push(616, encode_sproto([(0, {})]), {0: "{}"})

                    # PUSH 5: TAG 510 (aoi_update_attribute - Player Stats)
                    sync_char_attrs_rpc(conn, picked_char, send_rpc_push)

                    # PUSH 6: TAG 540 (sync_skill_info)
                    smap = build_skills_map(picked_char['prof'], picked_char['level'], picked_char.get('skill_levels', {}))
                    send_rpc_push(540, encode_sproto([(0, smap), (1, False)]), {0: f"[{len(smap)} skills]", 1: False})

                    # PUSH 7: TAG 519 (sync_mission)
                    send_rpc_push(519, sync_mission_data(picked_char), {0: f"[{len(picked_char['active_missions'])} missions]"})

                    # PUSH 8: TAG 503 (enter_map - Map 11)
                    mid = str(picked_char.get('map_id', '11'))
                    send_rpc_push(503, encode_sproto([(0, mid), (1, 0), (2, 1)]), {0: mid, 1: 0, 2: 1})

                    # PUSH 9: TAG 504 (main_player_create)
                    pos = picked_char['pos']
                    send_rpc_push(504, encode_sproto([
                        (0, get_full_char(picked_char)),
                        (1, get_movement(pos[0], pos[1], pos[2], pos[3]))
                    ]), {0: "[full_char]", 1: f"pos={pos}"})

                    # PUSH 10: TAG 505 (aoi_add - NPCs)
                    spawn_map_npcs(conn, mid, picked_char, send_rpc_push)

            # ------------------------------------------------------------------
            # HANDLER: MSG 100 (map_ready)
            # ------------------------------------------------------------------
            elif msg == 100:
                if picked_char:
                    mid = str(picked_char.get('map_id', '11'))
                    print(f"[*] Map Ready acknowledged for Map: {mid}")
                    send_rpc_push(519, sync_mission_data(picked_char), {0: f"[{len(picked_char['active_missions'])} missions]"})

            # ------------------------------------------------------------------
            # HANDLER: MSG 101 (move)
            # ------------------------------------------------------------------
            elif msg == 101:
                p_raw = body.get(0)
                if p_raw and picked_char:
                    pd = decode_sproto(p_raw)
                    picked_char['pos'] = [get_val_int(pd, 0), get_val_int(pd, 1), get_val_int(pd, 2), get_val_int(pd, 3)]
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([(0, p_raw)]), {0: p_raw})

            # ------------------------------------------------------------------
            # HANDLER: MSG 218 (heart_beat)
            # ------------------------------------------------------------------
            elif msg == 218:
                t1 = body.get(0, 0)
                resp_fields = [(0, t1), (1, int(time.time()))]
                send_rpc_response(session, msg, encode_sproto(resp_fields), dict(resp_fields))

            # ------------------------------------------------------------------
            # HANDLER: MSG 145 (ask_copyscenes_info)
            # ------------------------------------------------------------------
            elif msg == 145:
                # PUSH TAG 555 (sync_copyscenes_info)
                send_rpc_push(555, encode_sproto([(0, {})]), {0: "{}"})

            # ------------------------------------------------------------------
            # HANDLER: MSG 235 (request_mount_info)
            # ------------------------------------------------------------------
            elif msg == 235:
                # PUSH TAG 630 (ret_mount_info)
                if picked_char:
                    mounts = picked_char.get('mounts', {})
                    mount_info = {}
                    for mid, mstate in mounts.items():
                        mount_info[int(mid)] = encode_sproto([
                            (0, str(mid)), (1, mstate.get('state', 1)),
                            (2, mstate.get('select', '1')), (3, [mstate.get('select', '1')])
                        ])
                    send_rpc_push(630, encode_sproto([(0, mount_info)]), {0: f"[{len(mount_info)} vehicles]"})

            # ------------------------------------------------------------------
            # HANDLER: MSG 310 (request_domin_info)
            # ------------------------------------------------------------------
            elif msg == 310:
                # PUSH TAG 684 (ret_domin_info)
                domin_entry = encode_sproto([(0, "1"), (1, 0), (2, 0), (3, "Dominance")])
                send_rpc_push(684, encode_sproto([(0, [domin_entry]), (1, [])]), {0: "[domin_infos]", 1: "[]"})

            # ------------------------------------------------------------------
            # HANDLER: MSG 306 (tutorial_finish)
            # ------------------------------------------------------------------
            elif msg == 306:
                if picked_char:
                    picked_char['tutorial'] = 1
                    save_characters(all_characters)
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # HANDLER: MSG 112 (accept_mission)
            # ------------------------------------------------------------------
            elif msg == 112:
                mid = get_val_str(body, 0)
                if picked_char and mid:
                    picked_char.setdefault('active_missions', {})[mid] = {'state': 1, 'parm': [0]*8}
                    save_characters(all_characters)
                    send_rpc_push(519, sync_mission_data(picked_char), {0: f"[{len(picked_char['active_missions'])} missions]"})
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # HANDLER: MSG 113 (complete_mission)
            # ------------------------------------------------------------------
            elif msg == 113:
                mid = get_val_str(body, 0)
                if picked_char and mid:
                    if mid in picked_char.get('active_missions', {}):
                        del picked_char['active_missions'][mid]
                    picked_char['last_main_mission_id'] = mid
                    # Give rewards
                    picked_char['exp'] = picked_char.get('exp', 0) + 400
                    picked_char['cash'] = picked_char.get('cash', 0) + 5000
                    # Auto chain next mission
                    mcfg = missions_data.get(mid, {})
                    next_id = mcfg.get('next_id')
                    if next_id:
                        picked_char.setdefault('active_missions', {})[str(next_id)] = {'state': 1, 'parm': [0]*8}
                    save_characters(all_characters)
                    send_rpc_push(521, encode_sproto([(0, mid)]), {0: mid})
                    send_rpc_push(519, sync_mission_data(picked_char), {0: f"[{len(picked_char['active_missions'])} missions]"})
                    sync_char_attrs_rpc(conn, picked_char, send_rpc_push)
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # HANDLER: MSG 307 (local_npc_die)
            # ------------------------------------------------------------------
            elif msg == 307:
                npcid = get_val_str(body, 0)
                die_type = get_val_int(body, 3)
                print(f"[*] NPC Died: ID={npcid}, Type={die_type}")
                if picked_char:
                    # Update active missions
                    for mid, mdata in picked_char.get('active_missions', {}).items():
                        cur_prog = mdata['parm'][0] + 1
                        mdata['parm'][0] = cur_prog
                        send_rpc_push(524, encode_sproto([(0, mid), (1, 1), (2, cur_prog)]), {0: mid, 1: 1, 2: cur_prog})
                        if cur_prog >= 2:
                            mdata['state'] = 2
                            send_rpc_push(523, encode_sproto([(0, mid), (1, 2)]), {0: mid, 1: 2})
                    save_characters(all_characters)
                    send_rpc_push(519, sync_mission_data(picked_char), {0: f"[{len(picked_char['active_missions'])} missions]"})
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # HANDLER: MSG 111 (accept_damge)
            # ------------------------------------------------------------------
            elif msg == 111:
                damages = decode_sproto_list(body.get(0, b""))
                for dmg_raw in damages:
                    dmg_data = decode_sproto(dmg_raw)
                    target = get_val_int(dmg_data, 0)
                    dmg = get_val_int(dmg_data, 1)
                    print(f"[*] Player dealt {dmg} damage to target {target}")
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # HANDLER: MSG 270 (download_finish)
            # ------------------------------------------------------------------
            elif msg == 270:
                if picked_char:
                    picked_char['download_complete'] = True
                    save_characters(all_characters)
                send_rpc_push(654, encode_sproto([(0, 1)]), {0: 1})
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # HANDLER: MSG 115 (use_item)
            # ------------------------------------------------------------------
            elif msg == 115:
                item_id = get_val_str(body, 0)
                amount = get_val_int(body, 1, 1)
                print(f"[*] Used item {item_id} x{amount}")
                send_rpc_push(526, encode_sproto([(0, True), (1, item_id)]), {0: True, 1: item_id})
                if session is not None:
                    send_rpc_response(session, msg, encode_sproto([]), {})

            # ------------------------------------------------------------------
            # UNIVERSAL FALLBACK FOR ANY OTHER MESSAGE
            # ------------------------------------------------------------------
            else:
                proto_info = PROTOCOLS.get(msg)
                proto_name = proto_info[0] if proto_info else f"TAG_{msg}"
                if session is not None:
                    # Client expects an RPC response with this session ID!
                    print(f"[AUTO-ACK] Client sent MSG {msg} [{proto_name}] with Session {session}.")
                    print(f"           -> Sending ACK response to satisfy client callback!")
                    send_rpc_response(session, msg, encode_sproto([]), {})
                else:
                    print(f"[ACK] Client notification MSG {msg} [{proto_name}] received.")

    except Exception as e:
        print(f"[-] Client connection error: {e}")
        traceback.print_exc()
    finally:
        print(f"[-] Client disconnected from {addr}")
        try: conn.close()
        except: pass

# ==============================================================================
# SERVER ENTRY POINT
# ==============================================================================

def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind(("0.0.0.0", PORT))
    server.listen(50)
    print("=" * 80)
    print(f"GAME SERVER 9555 RUNNING ON PORT {PORT}")
    print(f"Loaded {len(PROTOCOLS)} protocols with full field-level introspection.")
    print("Ready for connections from 9777 login handoff.")
    print("=" * 80)
    while True:
        client, addr = server.accept()
        t = threading.Thread(target=client_handler, args=(client, addr), daemon=True)
        t.start()

if __name__ == "__main__":
    start_server()
