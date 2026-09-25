using System;
using System.Collections.Generic;
using Sproto;

// Token: 0x020002DE RID: 734
public class NetReceiver
{
	// Token: 0x0600148B RID: 5259 RVA: 0x000850A4 File Offset: 0x000832A4
	public static void Init()
	{
		NetReceiver.rpcReqHandlerDict = new Dictionary<int, RpcReqHandler>();
		NetReceiver.AddHandler<Protocol.aoi_add>(new RpcReqHandler(aoi_add_handler.aoi_add_request));
		NetReceiver.AddHandler<Protocol.aoi_relife_player>(new RpcReqHandler(aoi_relife_player_handler.aoi_relife_player_request));
		NetReceiver.AddHandler<Protocol.aoi_remove>(new RpcReqHandler(aoi_remove_handler.aoi_remove_request));
		NetReceiver.AddHandler<Protocol.aoi_social_dance>(new RpcReqHandler(aoi_social_dance_handler.aoi_social_dance_request));
		NetReceiver.AddHandler<Protocol.aoi_stop_move>(new RpcReqHandler(aoi_stop_move_handler.aoi_stop_move_request));
		NetReceiver.AddHandler<Protocol.aoi_update_attribute>(new RpcReqHandler(aoi_update_attribute_handler.aoi_update_attribute_request));
		NetReceiver.AddHandler<Protocol.aoi_update_move>(new RpcReqHandler(aoi_update_move_handler.aoi_update_move_request));
		NetReceiver.AddHandler<Protocol.apply_join_state>(new RpcReqHandler(apply_join_state_handler.apply_join_state_request));
		NetReceiver.AddHandler<Protocol.apply_join_team>(new RpcReqHandler(apply_join_team_handler.apply_join_team_request));
		NetReceiver.AddHandler<Protocol.ask_confirm>(new RpcReqHandler(ask_confirm_handler.ask_confirm_request));
		NetReceiver.AddHandler<Protocol.ask_confirm_multi_copy_scene>(new RpcReqHandler(ask_confirm_multi_copy_scene_handler.ask_confirm_multi_copy_scene_request));
		NetReceiver.AddHandler<Protocol.bar_fight_notify>(new RpcReqHandler(bar_fight_notify_handler.bar_fight_notify_request));
		NetReceiver.AddHandler<Protocol.be_deleted_friend>(new RpcReqHandler(be_deleted_friend_handler.be_deleted_friend_request));
		NetReceiver.AddHandler<Protocol.cancel_apply_join_team>(new RpcReqHandler(cancel_apply_join_team_handler.cancel_apply_join_team_request));
		NetReceiver.AddHandler<Protocol.car_copy_result>(new RpcReqHandler(car_copy_result_handler.car_copy_result_request));
		NetReceiver.AddHandler<Protocol.comb_value_up_tip>(new RpcReqHandler(comb_value_up_tip_handler.comb_value_up_tip_request));
		NetReceiver.AddHandler<Protocol.copy_scene_result>(new RpcReqHandler(copy_scene_result_handler.copy_scene_result_request));
		NetReceiver.AddHandler<Protocol.count_down>(new RpcReqHandler(count_down_handler.count_down_request));
		NetReceiver.AddHandler<Protocol.drop_item_info>(new RpcReqHandler(drop_item_info_handler.drop_item_info_request));
		NetReceiver.AddHandler<Protocol.enter_map>(new RpcReqHandler(enter_map_handler.enter_map_request));
		NetReceiver.AddHandler<Protocol.get_level_reward>(new RpcReqHandler(get_level_reward_handler.get_level_reward_request));
		NetReceiver.AddHandler<Protocol.grant_activity_reward>(new RpcReqHandler(grant_activity_reward_handler.grant_activity_reward_request));
		NetReceiver.AddHandler<Protocol.grant_daily_mission_reward>(new RpcReqHandler(grant_daily_mission_reward_handler.grant_daily_mission_reward_request));
		NetReceiver.AddHandler<Protocol.guild_battle_finish_info>(new RpcReqHandler(guild_battle_finish_info_handler.guild_battle_finish_info_request));
		NetReceiver.AddHandler<Protocol.guild_battle_start>(new RpcReqHandler(guild_battle_start_handler.guild_battle_start_request));
		NetReceiver.AddHandler<Protocol.guild_invite_accept>(new RpcReqHandler(guild_invite_accept_handler.guild_invite_accept_request));
		NetReceiver.AddHandler<Protocol.hit_action>(new RpcReqHandler(hit_action_handler.hit_action_request));
		NetReceiver.AddHandler<Protocol.invite_join_team>(new RpcReqHandler(invite_join_team_handler.invite_join_team_request));
		NetReceiver.AddHandler<Protocol.login_max_count>(new RpcReqHandler(login_max_count_handler.login_max_count_request));
		NetReceiver.AddHandler<Protocol.mail_delete>(new RpcReqHandler(mail_delete_handler.mail_delete_request));
		NetReceiver.AddHandler<Protocol.mail_update>(new RpcReqHandler(mail_update_handler.mail_update_request));
		NetReceiver.AddHandler<Protocol.main_player_create>(new RpcReqHandler(main_player_create_handler.main_player_create_request));
		NetReceiver.AddHandler<Protocol.next_wave>(new RpcReqHandler(next_wave_handler.next_wave_request));
		NetReceiver.AddHandler<Protocol.notice>(new RpcReqHandler(notice_handler.notice_request));
		NetReceiver.AddHandler<Protocol.notice_add_friend>(new RpcReqHandler(notice_add_friend_handler.notice_add_friend_request));
		NetReceiver.AddHandler<Protocol.notice_copy_scene_info>(new RpcReqHandler(notice_copy_scene_info_handler.notice_copy_scene_info_request));
		NetReceiver.AddHandler<Protocol.notice_guild_battle_rank>(new RpcReqHandler(notice_guild_battle_rank_handler.notice_guild_battle_rank_request));
		NetReceiver.AddHandler<Protocol.notice_money_copy_reward>(new RpcReqHandler(notice_money_copy_reward_handler.notice_money_copy_reward_request));
		NetReceiver.AddHandler<Protocol.notice_relife_player>(new RpcReqHandler(notice_relife_player_handler.notice_relife_player_request));
		NetReceiver.AddHandler<Protocol.notice_urge_team_leader>(new RpcReqHandler(notice_urge_team_leader_handler.notice_urge_team_leader_request));
		NetReceiver.AddHandler<Protocol.notify_confirm_state>(new RpcReqHandler(notify_confirm_state_handler.notify_confirm_state_request));
		NetReceiver.AddHandler<Protocol.notify_copy_start_info>(new RpcReqHandler(notify_copy_start_info_handler.notify_copy_start_info_request));
		NetReceiver.AddHandler<Protocol.npc_create>(new RpcReqHandler(npc_create_handler.npc_create_request));
		NetReceiver.AddHandler<Protocol.random_select_ok>(new RpcReqHandler(random_select_ok_handler.random_select_ok_request));
		NetReceiver.AddHandler<Protocol.rank_pvp_create_zombie_user>(new RpcReqHandler(rank_pvp_create_zombie_user_handler.rank_pvp_create_zombie_user_request));
		NetReceiver.AddHandler<Protocol.rank_pvp_history>(new RpcReqHandler(rank_pvp_history_handler.rank_pvp_history_request));
		NetReceiver.AddHandler<Protocol.rank_pvp_reward>(new RpcReqHandler(rank_pvp_reward_handler.rank_pvp_reward_request));
		NetReceiver.AddHandler<Protocol.rank_pvp_start>(new RpcReqHandler(rank_pvp_start_handler.rank_pvp_start_request));
		NetReceiver.AddHandler<Protocol.real_pvp_start>(new RpcReqHandler(real_pvp_start_handler.real_pvp_start_request));
		NetReceiver.AddHandler<Protocol.real_pvp_state>(new RpcReqHandler(real_pvp_state_handler.real_pvp_state_request));
		NetReceiver.AddHandler<Protocol.req_invite_team_result>(new RpcReqHandler(req_invite_team_result_handler.req_invite_team_result_request));
		NetReceiver.AddHandler<Protocol.ret_abandon_mission>(new RpcReqHandler(ret_abandon_mission_handler.ret_abandon_mission_request));
		NetReceiver.AddHandler<Protocol.ret_accept_mission>(new RpcReqHandler(ret_accept_mission_handler.ret_accept_mission_request));
		NetReceiver.AddHandler<Protocol.ret_add_friend>(new RpcReqHandler(ret_add_friend_handler.ret_add_friend_request));
		NetReceiver.AddHandler<Protocol.ret_ask_shop_list>(new RpcReqHandler(ret_ask_shop_list_handler.ret_ask_shop_list_request));
		NetReceiver.AddHandler<Protocol.ret_battle_info>(new RpcReqHandler(ret_battle_info_handler.ret_battle_info_request));
		NetReceiver.AddHandler<Protocol.ret_buy_car_shop>(new RpcReqHandler(ret_buy_car_shop_handler.ret_buy_car_shop_request));
		NetReceiver.AddHandler<Protocol.ret_buy_guild_goods>(new RpcReqHandler(ret_buy_guild_goods_handler.ret_buy_guild_goods_request));
		NetReceiver.AddHandler<Protocol.ret_buy_invest_pack>(new RpcReqHandler(ret_buy_invest_pack_handler.ret_buy_invest_pack_request));
		NetReceiver.AddHandler<Protocol.ret_buy_shop_item>(new RpcReqHandler(ret_buy_shop_item_handler.ret_buy_shop_item_request));
		NetReceiver.AddHandler<Protocol.ret_chat>(new RpcReqHandler(ret_chat_handler.ret_chat_request));
		NetReceiver.AddHandler<Protocol.ret_commercail_reward>(new RpcReqHandler(ret_commercail_reward_handler.ret_commercail_reward_request));
		NetReceiver.AddHandler<Protocol.ret_complete_mission>(new RpcReqHandler(ret_complete_mission_handler.ret_complete_mission_request));
		NetReceiver.AddHandler<Protocol.ret_consign_ask_items_info>(new RpcReqHandler(ret_consign_ask_items_info_handler.ret_consign_ask_items_info_request));
		NetReceiver.AddHandler<Protocol.ret_consign_ask_my_items>(new RpcReqHandler(ret_consign_ask_my_items_handler.ret_consign_ask_my_items_request));
		NetReceiver.AddHandler<Protocol.ret_consign_buy_item>(new RpcReqHandler(ret_consign_buy_item_handler.ret_consign_buy_item_request));
		NetReceiver.AddHandler<Protocol.ret_consign_cancel_sale>(new RpcReqHandler(ret_consign_cancel_sale_handler.ret_consign_cancel_sale_request));
		NetReceiver.AddHandler<Protocol.ret_consign_sale_item>(new RpcReqHandler(ret_consign_sale_item_handler.ret_consign_sale_item_request));
		NetReceiver.AddHandler<Protocol.ret_del_friend>(new RpcReqHandler(ret_del_friend_handler.ret_del_friend_request));
		NetReceiver.AddHandler<Protocol.ret_domin_info>(new RpcReqHandler(ret_domin_info_handler.ret_domin_info_request));
		NetReceiver.AddHandler<Protocol.ret_enter_guild_battle>(new RpcReqHandler(ret_enter_guild_battle_handler.ret_enter_guild_battle_request));
		NetReceiver.AddHandler<Protocol.ret_get_team_list>(new RpcReqHandler(ret_get_team_list_handler.ret_get_team_list_request));
		NetReceiver.AddHandler<Protocol.ret_grant_tower_reward>(new RpcReqHandler(ret_grant_tower_reward_handler.ret_grant_tower_reward_request));
		NetReceiver.AddHandler<Protocol.ret_guild_approve_resverve>(new RpcReqHandler(ret_guild_approve_resverve_handler.ret_guild_approve_resverve_request));
		NetReceiver.AddHandler<Protocol.ret_guild_battle_guess>(new RpcReqHandler(ret_guild_battle_guess_handler.ret_guild_battle_guess_request));
		NetReceiver.AddHandler<Protocol.ret_guild_battle_info>(new RpcReqHandler(ret_guild_battle_info_handler.ret_guild_battle_info_request));
		NetReceiver.AddHandler<Protocol.ret_guild_battle_member>(new RpcReqHandler(ret_guild_battle_member_handler.ret_guild_battle_member_request));
		NetReceiver.AddHandler<Protocol.ret_guild_battle_rank>(new RpcReqHandler(ret_guild_battle_rank_handler.ret_guild_battle_rank_request));
		NetReceiver.AddHandler<Protocol.ret_guild_battle_state>(new RpcReqHandler(ret_guild_battle_state_handler.ret_guild_battle_state_request));
		NetReceiver.AddHandler<Protocol.ret_guild_create>(new RpcReqHandler(ret_guild_create_handler.ret_guild_create_request));
		NetReceiver.AddHandler<Protocol.ret_guild_donate>(new RpcReqHandler(ret_guild_donate_handler.ret_guild_donate_request));
		NetReceiver.AddHandler<Protocol.ret_guild_job_change>(new RpcReqHandler(ret_guild_job_change_handler.ret_guild_job_change_request));
		NetReceiver.AddHandler<Protocol.ret_guild_join>(new RpcReqHandler(ret_guild_join_handler.ret_guild_join_request));
		NetReceiver.AddHandler<Protocol.ret_guild_kick>(new RpcReqHandler(ret_guild_kick_handler.ret_guild_kick_request));
		NetReceiver.AddHandler<Protocol.ret_guild_leave>(new RpcReqHandler(ret_guild_leave_handler.ret_guild_leave_request));
		NetReceiver.AddHandler<Protocol.ret_guild_log>(new RpcReqHandler(ret_guild_log_handler.ret_guild_log_request));
		NetReceiver.AddHandler<Protocol.ret_guild_map_domine_top>(new RpcReqHandler(ret_guild_map_domine_top_handler.ret_guild_map_domine_top_request));
		NetReceiver.AddHandler<Protocol.ret_guild_map_reward>(new RpcReqHandler(ret_guild_map_reward_handler.ret_guild_map_reward_request));
		NetReceiver.AddHandler<Protocol.ret_guild_member_info>(new RpcReqHandler(ret_guild_member_info_handler.ret_guild_member_info_request));
		NetReceiver.AddHandler<Protocol.ret_guild_req_info>(new RpcReqHandler(ret_guild_req_info_handler.ret_guild_req_info_request));
		NetReceiver.AddHandler<Protocol.ret_guild_req_list>(new RpcReqHandler(ret_guild_req_list_handler.ret_guild_req_list_request));
		NetReceiver.AddHandler<Protocol.ret_guild_score_info>(new RpcReqHandler(ret_guild_score_info_handler.ret_guild_score_info_request));
		NetReceiver.AddHandler<Protocol.ret_guild_skill_level>(new RpcReqHandler(ret_guild_skill_level_handler.ret_guild_skill_level_request));
		NetReceiver.AddHandler<Protocol.ret_guild_star>(new RpcReqHandler(ret_guild_star_handler.ret_guild_star_request));
		NetReceiver.AddHandler<Protocol.ret_level_reward>(new RpcReqHandler(ret_level_reward_handler.ret_level_reward_request));
		NetReceiver.AddHandler<Protocol.ret_mount_equip>(new RpcReqHandler(ret_mount_equip_handler.ret_mount_equip_request));
		NetReceiver.AddHandler<Protocol.ret_mount_info>(new RpcReqHandler(ret_mount_info_handler.ret_mount_info_request));
		NetReceiver.AddHandler<Protocol.ret_mount_use_color>(new RpcReqHandler(ret_mount_use_color_handler.ret_mount_use_color_request));
		NetReceiver.AddHandler<Protocol.ret_offline_chat>(new RpcReqHandler(ret_offline_chat_handler.ret_offline_chat_request));
		NetReceiver.AddHandler<Protocol.ret_open_guild_boss>(new RpcReqHandler(ret_open_guild_boss_handler.ret_open_guild_boss_request));
		NetReceiver.AddHandler<Protocol.ret_open_guild_shop>(new RpcReqHandler(ret_open_guild_shop_handler.ret_open_guild_shop_request));
		NetReceiver.AddHandler<Protocol.ret_open_item_package>(new RpcReqHandler(ret_open_item_package_handler.ret_open_item_package_request));
		NetReceiver.AddHandler<Protocol.ret_random_online_character_list>(new RpcReqHandler(ret_random_online_character_list_handler.ret_random_online_character_list_request));
		NetReceiver.AddHandler<Protocol.ret_re_name>(new RpcReqHandler(ret_re_name_handler.ret_re_name_request));
		NetReceiver.AddHandler<Protocol.ret_req_guild_skill>(new RpcReqHandler(ret_req_guild_skill_handler.ret_req_guild_skill_request));
		NetReceiver.AddHandler<Protocol.ret_request_30_day_info>(new RpcReqHandler(ret_request_30_day_info_handler.ret_request_30_day_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_activity_info>(new RpcReqHandler(ret_request_activity_info_handler.ret_request_activity_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_big_pack>(new RpcReqHandler(ret_request_big_pack_handler.ret_request_big_pack_request));
		NetReceiver.AddHandler<Protocol.ret_request_daily_active>(new RpcReqHandler(ret_request_daily_active_handler.ret_request_daily_active_request));
		NetReceiver.AddHandler<Protocol.ret_request_daily_buy>(new RpcReqHandler(ret_request_daily_buy_handler.ret_request_daily_buy_request));
		NetReceiver.AddHandler<Protocol.ret_request_dance_info>(new RpcReqHandler(ret_request_dance_info_handler.ret_request_dance_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_first_buy>(new RpcReqHandler(ret_request_first_buy_handler.ret_request_first_buy_request));
		NetReceiver.AddHandler<Protocol.ret_request_guild_boss>(new RpcReqHandler(ret_request_guild_boss_handler.ret_request_guild_boss_request));
		NetReceiver.AddHandler<Protocol.ret_request_guild_map_info>(new RpcReqHandler(ret_request_guild_map_info_handler.ret_request_guild_map_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_invest_pack>(new RpcReqHandler(ret_request_invest_pack_handler.ret_request_invest_pack_request));
		NetReceiver.AddHandler<Protocol.ret_request_level_pack>(new RpcReqHandler(ret_request_level_pack_handler.ret_request_level_pack_request));
		NetReceiver.AddHandler<Protocol.ret_request_random_rank_pvp_opponent>(new RpcReqHandler(ret_request_random_rank_pvp_opponent_handler.ret_request_random_rank_pvp_opponent_request));
		NetReceiver.AddHandler<Protocol.ret_request_retrieve_info>(new RpcReqHandler(ret_request_retrieve_info_handler.ret_request_retrieve_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_sign_week_info>(new RpcReqHandler(ret_request_sign_week_info_handler.ret_request_sign_week_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_survive_top>(new RpcReqHandler(ret_request_survive_top_handler.ret_request_survive_top_request));
		NetReceiver.AddHandler<Protocol.ret_request_top_rank_pvp_list>(new RpcReqHandler(ret_request_top_rank_pvp_list_handler.ret_request_top_rank_pvp_list_request));
		NetReceiver.AddHandler<Protocol.ret_request_tower_copy_info>(new RpcReqHandler(ret_request_tower_copy_info_handler.ret_request_tower_copy_info_request));
		NetReceiver.AddHandler<Protocol.ret_request_update_friend_useinfo>(new RpcReqHandler(ret_request_update_friend_useinfo_handler.ret_request_update_friend_useinfo_request));
		NetReceiver.AddHandler<Protocol.ret_request_update_storagepack>(new RpcReqHandler(ret_request_update_storagepack_handler.ret_request_update_storagepack_request));
		NetReceiver.AddHandler<Protocol.ret_request_wild_boss_info>(new RpcReqHandler(ret_request_wild_boss_info_handler.ret_request_wild_boss_info_request));
		NetReceiver.AddHandler<Protocol.ret_require_vip_info>(new RpcReqHandler(ret_require_vip_info_handler.ret_require_vip_info_request));
		NetReceiver.AddHandler<Protocol.ret_require_vip_reward>(new RpcReqHandler(ret_require_vip_reward_handler.ret_require_vip_reward_request));
		NetReceiver.AddHandler<Protocol.ret_search_guild>(new RpcReqHandler(ret_search_guild_handler.ret_search_guild_request));
		NetReceiver.AddHandler<Protocol.ret_search_online_character_by_name>(new RpcReqHandler(ret_search_online_character_by_name_handler.ret_search_online_character_by_name_request));
		NetReceiver.AddHandler<Protocol.ret_set_guild_battle_member>(new RpcReqHandler(ret_set_guild_battle_member_handler.ret_set_guild_battle_member_request));
		NetReceiver.AddHandler<Protocol.ret_sign_30_day>(new RpcReqHandler(ret_sign_30_day_handler.ret_sign_30_day_request));
		NetReceiver.AddHandler<Protocol.ret_sign_week>(new RpcReqHandler(ret_sign_week_handler.ret_sign_week_request));
		NetReceiver.AddHandler<Protocol.ret_skill_use>(new RpcReqHandler(ret_skill_use_handler.ret_skill_use_request));
		NetReceiver.AddHandler<Protocol.ret_slot_info>(new RpcReqHandler(ret_slot_info_handler.ret_slot_info_request));
		NetReceiver.AddHandler<Protocol.ret_slot_sum_reward>(new RpcReqHandler(ret_slot_sum_reward_handler.ret_slot_sum_reward_request));
		NetReceiver.AddHandler<Protocol.ret_special_big_pack>(new RpcReqHandler(ret_special_big_pack_handler.ret_special_big_pack_request));
		NetReceiver.AddHandler<Protocol.ret_spin_slot>(new RpcReqHandler(ret_spin_slot_handler.ret_spin_slot_request));
		NetReceiver.AddHandler<Protocol.ret_title_req_level_up>(new RpcReqHandler(ret_title_req_level_up_handler.ret_title_req_level_up_request));
		NetReceiver.AddHandler<Protocol.ret_top_rank_list>(new RpcReqHandler(ret_top_rank_list_handler.ret_top_rank_list_request));
		NetReceiver.AddHandler<Protocol.ret_tower_reset>(new RpcReqHandler(ret_tower_reset_handler.ret_tower_reset_request));
		NetReceiver.AddHandler<Protocol.ret_tower_wipe_out>(new RpcReqHandler(ret_tower_wipe_out_handler.ret_tower_wipe_out_request));
		NetReceiver.AddHandler<Protocol.ret_update_guild_star>(new RpcReqHandler(ret_update_guild_star_handler.ret_update_guild_star_request));
		NetReceiver.AddHandler<Protocol.ret_use_item>(new RpcReqHandler(ret_use_item_handler.ret_use_item_request));
		NetReceiver.AddHandler<Protocol.ret_watch_video_info>(new RpcReqHandler(ret_watch_video_info_handler.ret_watch_video_info_request));
		NetReceiver.AddHandler<Protocol.retrieve_account>(new RpcReqHandler(retrieve_account_handler.retrieve_account_request));
		NetReceiver.AddHandler<Protocol.sample_activity_result>(new RpcReqHandler(sample_activity_result_handler.sample_activity_result_request));
		NetReceiver.AddHandler<Protocol.sample_copy_result>(new RpcReqHandler(sample_copy_result_handler.sample_copy_result_request));
		NetReceiver.AddHandler<Protocol.send_daily_mission>(new RpcReqHandler(send_daily_mission_handler.send_daily_mission_request));
		NetReceiver.AddHandler<Protocol.send_dialog_notify>(new RpcReqHandler(send_dialog_notify_handler.send_dialog_notify_request));
		NetReceiver.AddHandler<Protocol.send_escort_info>(new RpcReqHandler(send_escort_info_handler.send_escort_info_request));
		NetReceiver.AddHandler<Protocol.set_mission_param>(new RpcReqHandler(set_mission_param_handler.set_mission_param_request));
		NetReceiver.AddHandler<Protocol.set_mission_state>(new RpcReqHandler(set_mission_state_handler.set_mission_state_request));
		NetReceiver.AddHandler<Protocol.show_damage_board>(new RpcReqHandler(show_damage_board_handler.show_damage_board_request));
		NetReceiver.AddHandler<Protocol.show_player_damage_board>(new RpcReqHandler(show_player_damage_board_handler.show_player_damage_board_request));
		NetReceiver.AddHandler<Protocol.show_reward_items_tips>(new RpcReqHandler(show_reward_items_tips_handler.show_reward_items_tips_request));
		NetReceiver.AddHandler<Protocol.start_enter_game>(new RpcReqHandler(start_enter_game_handler.start_enter_game_request));
		NetReceiver.AddHandler<Protocol.start_participate_dance>(new RpcReqHandler(start_participate_dance_handler.start_participate_dance_request));
		NetReceiver.AddHandler<Protocol.survive_battle_finish>(new RpcReqHandler(survive_battle_finish_handler.survive_battle_finish_request));
		NetReceiver.AddHandler<Protocol.syn_friend_info>(new RpcReqHandler(syn_friend_info_handler.syn_friend_info_request));
		NetReceiver.AddHandler<Protocol.syn_rank_pvp_data>(new RpcReqHandler(syn_rank_pvp_data_handler.syn_rank_pvp_data_request));
		NetReceiver.AddHandler<Protocol.sync_backpack_item>(new RpcReqHandler(sync_backpack_item_handler.sync_backpack_item_request));
		NetReceiver.AddHandler<Protocol.sync_badgepack_item>(new RpcReqHandler(sync_badgepack_item_handler.sync_badgepack_item_request));
		NetReceiver.AddHandler<Protocol.sync_common_data>(new RpcReqHandler(sync_common_data_handler.sync_common_data_request));
		NetReceiver.AddHandler<Protocol.sync_copyscenes_info>(new RpcReqHandler(sync_copyscenes_info_handler.sync_copyscenes_info_request));
		NetReceiver.AddHandler<Protocol.sync_dance_state_info>(new RpcReqHandler(sync_dance_state_info_handler.sync_dance_state_info_request));
		NetReceiver.AddHandler<Protocol.sync_fashion_backpack_item>(new RpcReqHandler(sync_fashion_backpack_item_handler.sync_fashion_backpack_item_request));
		NetReceiver.AddHandler<Protocol.sync_guild_new_member>(new RpcReqHandler(sync_guild_new_member_handler.sync_guild_new_member_request));
		NetReceiver.AddHandler<Protocol.sync_item_pack>(new RpcReqHandler(sync_item_pack_handler.sync_item_pack_request));
		NetReceiver.AddHandler<Protocol.sync_mission>(new RpcReqHandler(sync_mission_handler.sync_mission_request));
		NetReceiver.AddHandler<Protocol.sync_random_team_state>(new RpcReqHandler(sync_random_team_state_handler.sync_random_team_state_request));
		NetReceiver.AddHandler<Protocol.sync_skill_info>(new RpcReqHandler(sync_skill_info_handler.sync_skill_info_request));
		NetReceiver.AddHandler<Protocol.sync_watch_video_info>(new RpcReqHandler(sync_watch_video_info_handler.sync_watch_video_info_request));
		NetReceiver.AddHandler<Protocol.tiantti_result>(new RpcReqHandler(tiantti_result_handler.tiantti_result_request));
		NetReceiver.AddHandler<Protocol.update_copyscene_info>(new RpcReqHandler(update_copyscene_info_handler.update_copyscene_info_request));
		NetReceiver.AddHandler<Protocol.update_item>(new RpcReqHandler(update_item_handler.update_item_request));
		NetReceiver.AddHandler<Protocol.update_line_state>(new RpcReqHandler(update_line_state_handler.update_line_state_request));
		NetReceiver.AddHandler<Protocol.update_queue_rank>(new RpcReqHandler(update_queue_rank_handler.update_queue_rank_request));
		NetReceiver.AddHandler<Protocol.update_team>(new RpcReqHandler(update_team_handler.update_team_request));
		NetReceiver.AddHandler<Protocol.update_team_member>(new RpcReqHandler(update_team_member_handler.update_team_member_request));
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x00085D54 File Offset: 0x00083F54
	public static void AddHandler(int tag, RpcReqHandler rpcReqHandler)
	{
		NetReceiver.rpcReqHandlerDict.Add(tag, rpcReqHandler);
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x00085D64 File Offset: 0x00083F64
	public static int AddHandler<T>(RpcReqHandler rpcReqHandler)
	{
		int num = NetReceiver.protocol[typeof(T)];
		NetReceiver.AddHandler(num, rpcReqHandler);
		return num;
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x00085D90 File Offset: 0x00083F90
	public static void RemoveHandler(int tag)
	{
		if (NetReceiver.rpcReqHandlerDict.ContainsKey(tag))
		{
			NetReceiver.rpcReqHandlerDict.Remove(tag);
		}
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x00085DB0 File Offset: 0x00083FB0
	public static void RemoveHandler<T>()
	{
		NetReceiver.RemoveHandler(NetReceiver.protocol[typeof(T)]);
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x00085DCC File Offset: 0x00083FCC
	public static RpcReqHandler GetHandler(int tag)
	{
		RpcReqHandler result;
		NetReceiver.rpcReqHandlerDict.TryGetValue(tag, ref result);
		return result;
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x00085DE8 File Offset: 0x00083FE8
	public static RpcReqHandler GetHandler<T>()
	{
		return NetReceiver.GetHandler(NetReceiver.protocol[typeof(T)]);
	}

	// Token: 0x0400181C RID: 6172
	private static ProtocolFunctionDictionary protocol = Protocol.Instance.Protocol;

	// Token: 0x0400181D RID: 6173
	private static Dictionary<int, RpcReqHandler> rpcReqHandlerDict;
}
