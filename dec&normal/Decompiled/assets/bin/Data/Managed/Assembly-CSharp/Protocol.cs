using System;
using Sproto;
using SprotoType;

// Token: 0x0200066D RID: 1645
public class Protocol : ProtocolBase
{
	// Token: 0x06002FB1 RID: 12209 RVA: 0x000BC6C4 File Offset: 0x000BA8C4
	private Protocol()
	{
		base.Protocol.SetProtocol<Protocol.abandon_mission>(114);
		base.Protocol.SetRequest<SprotoType.abandon_mission.request>(114);
		base.Protocol.SetProtocol<Protocol.accept_damge>(111);
		base.Protocol.SetRequest<SprotoType.accept_damge.request>(111);
		base.Protocol.SetProtocol<Protocol.accept_mission>(112);
		base.Protocol.SetRequest<SprotoType.accept_mission.request>(112);
		base.Protocol.SetProtocol<Protocol.add_friend>(124);
		base.Protocol.SetRequest<SprotoType.add_friend.request>(124);
		base.Protocol.SetProtocol<Protocol.aoi_add>(505);
		base.Protocol.SetRequest<SprotoType.aoi_add.request>(505);
		base.Protocol.SetProtocol<Protocol.aoi_relife_player>(512);
		base.Protocol.SetRequest<SprotoType.aoi_relife_player.request>(512);
		base.Protocol.SetProtocol<Protocol.aoi_remove>(506);
		base.Protocol.SetRequest<SprotoType.aoi_remove.request>(506);
		base.Protocol.SetProtocol<Protocol.aoi_social_dance>(657);
		base.Protocol.SetRequest<SprotoType.aoi_social_dance.request>(657);
		base.Protocol.SetProtocol<Protocol.aoi_stop_move>(513);
		base.Protocol.SetRequest<SprotoType.aoi_stop_move.request>(513);
		base.Protocol.SetProtocol<Protocol.aoi_update_attribute>(510);
		base.Protocol.SetRequest<SprotoType.aoi_update_attribute.request>(510);
		base.Protocol.SetProtocol<Protocol.aoi_update_move>(507);
		base.Protocol.SetRequest<SprotoType.aoi_update_move.request>(507);
		base.Protocol.SetProtocol<Protocol.apply_join_result>(162);
		base.Protocol.SetRequest<SprotoType.apply_join_result.request>(162);
		base.Protocol.SetProtocol<Protocol.apply_join_state>(576);
		base.Protocol.SetRequest<SprotoType.apply_join_state.request>(576);
		base.Protocol.SetProtocol<Protocol.apply_join_team>(572);
		base.Protocol.SetRequest<SprotoType.apply_join_team.request>(572);
		base.Protocol.SetProtocol<Protocol.approve_resverve_friend>(158);
		base.Protocol.SetRequest<SprotoType.approve_resverve_friend.request>(158);
		base.Protocol.SetProtocol<Protocol.ask_character_info>(142);
		base.Protocol.SetRequest<SprotoType.ask_character_info.request>(142);
		base.Protocol.SetResponse<SprotoType.ask_character_info.response>(142);
		base.Protocol.SetProtocol<Protocol.ask_confirm>(601);
		base.Protocol.SetRequest<SprotoType.ask_confirm.request>(601);
		base.Protocol.SetResponse<SprotoType.ask_confirm.response>(601);
		base.Protocol.SetProtocol<Protocol.ask_confirm_multi_copy_scene>(602);
		base.Protocol.SetRequest<SprotoType.ask_confirm_multi_copy_scene.request>(602);
		base.Protocol.SetProtocol<Protocol.ask_copyscenes_info>(145);
		base.Protocol.SetRequest<SprotoType.ask_copyscenes_info.request>(145);
		base.Protocol.SetProtocol<Protocol.ask_pickup_item>(119);
		base.Protocol.SetRequest<SprotoType.ask_pickup_item.request>(119);
		base.Protocol.SetProtocol<Protocol.ask_shop_list>(143);
		base.Protocol.SetRequest<SprotoType.ask_shop_list.request>(143);
		base.Protocol.SetProtocol<Protocol.attack_local_npc>(317);
		base.Protocol.SetRequest<SprotoType.attack_local_npc.request>(317);
		base.Protocol.SetProtocol<Protocol.attribute_inhert>(302);
		base.Protocol.SetRequest<SprotoType.attribute_inhert.request>(302);
		base.Protocol.SetProtocol<Protocol.badge_merge>(199);
		base.Protocol.SetRequest<SprotoType.badge_merge.request>(199);
		base.Protocol.SetResponse<SprotoType.badge_merge.response>(199);
		base.Protocol.SetProtocol<Protocol.bar_fight_notify>(659);
		base.Protocol.SetRequest<SprotoType.bar_fight_notify.request>(659);
		base.Protocol.SetProtocol<Protocol.be_deleted_friend>(537);
		base.Protocol.SetRequest<SprotoType.be_deleted_friend.request>(537);
		base.Protocol.SetProtocol<Protocol.buy_big_pack>(272);
		base.Protocol.SetRequest<SprotoType.buy_big_pack.request>(272);
		base.Protocol.SetProtocol<Protocol.buy_car_shop>(323);
		base.Protocol.SetRequest<SprotoType.buy_car_shop.request>(323);
		base.Protocol.SetProtocol<Protocol.buy_invest_pack>(271);
		base.Protocol.SetRequest<SprotoType.buy_invest_pack.request>(271);
		base.Protocol.SetProtocol<Protocol.buy_shop_item>(144);
		base.Protocol.SetRequest<SprotoType.buy_shop_item.request>(144);
		base.Protocol.SetProtocol<Protocol.cancel_apply_join_team>(573);
		base.Protocol.SetRequest<SprotoType.cancel_apply_join_team.request>(573);
		base.Protocol.SetProtocol<Protocol.car_chase_result>(193);
		base.Protocol.SetRequest<SprotoType.car_chase_result.request>(193);
		base.Protocol.SetProtocol<Protocol.car_copy_result>(608);
		base.Protocol.SetRequest<SprotoType.car_copy_result.request>(608);
		base.Protocol.SetProtocol<Protocol.change_item_state>(240);
		base.Protocol.SetRequest<SprotoType.change_item_state.request>(240);
		base.Protocol.SetProtocol<Protocol.change_mount_state>(267);
		base.Protocol.SetRequest<SprotoType.change_mount_state.request>(267);
		base.Protocol.SetProtocol<Protocol.change_potion>(185);
		base.Protocol.SetRequest<SprotoType.change_potion.request>(185);
		base.Protocol.SetProtocol<Protocol.change_scene_line>(155);
		base.Protocol.SetRequest<SprotoType.change_scene_line.request>(155);
		base.Protocol.SetProtocol<Protocol.change_show_type>(223);
		base.Protocol.SetRequest<SprotoType.change_show_type.request>(223);
		base.Protocol.SetProtocol<Protocol.change_skill_index>(316);
		base.Protocol.SetRequest<SprotoType.change_skill_index.request>(316);
		base.Protocol.SetProtocol<Protocol.change_skill_position>(192);
		base.Protocol.SetRequest<SprotoType.change_skill_position.request>(192);
		base.Protocol.SetProtocol<Protocol.character_create>(104);
		base.Protocol.SetRequest<SprotoType.character_create.request>(104);
		base.Protocol.SetResponse<SprotoType.character_create.response>(104);
		base.Protocol.SetProtocol<Protocol.character_list>(103);
		base.Protocol.SetResponse<SprotoType.character_list.response>(103);
		base.Protocol.SetProtocol<Protocol.character_pick>(105);
		base.Protocol.SetRequest<SprotoType.character_pick.request>(105);
		base.Protocol.SetResponse<SprotoType.character_pick.response>(105);
		base.Protocol.SetProtocol<Protocol.chat>(120);
		base.Protocol.SetRequest<SprotoType.chat.request>(120);
		base.Protocol.SetProtocol<Protocol.check_purchase>(266);
		base.Protocol.SetRequest<SprotoType.check_purchase.request>(266);
		base.Protocol.SetProtocol<Protocol.comb_value_up_tip>(651);
		base.Protocol.SetRequest<SprotoType.comb_value_up_tip.request>(651);
		base.Protocol.SetProtocol<Protocol.complete_mission>(113);
		base.Protocol.SetRequest<SprotoType.complete_mission.request>(113);
		base.Protocol.SetProtocol<Protocol.consign_ask_items_info>(189);
		base.Protocol.SetRequest<SprotoType.consign_ask_items_info.request>(189);
		base.Protocol.SetProtocol<Protocol.consign_ask_my_items>(188);
		base.Protocol.SetRequest<SprotoType.consign_ask_my_items.request>(188);
		base.Protocol.SetProtocol<Protocol.consign_buy_item>(190);
		base.Protocol.SetRequest<SprotoType.consign_buy_item.request>(190);
		base.Protocol.SetProtocol<Protocol.consign_cancel_sale>(187);
		base.Protocol.SetRequest<SprotoType.consign_cancel_sale.request>(187);
		base.Protocol.SetProtocol<Protocol.consign_sale_item>(186);
		base.Protocol.SetRequest<SprotoType.consign_sale_item.request>(186);
		base.Protocol.SetProtocol<Protocol.continue_tower_copy>(205);
		base.Protocol.SetRequest<SprotoType.continue_tower_copy.request>(205);
		base.Protocol.SetProtocol<Protocol.copy_scene_result>(552);
		base.Protocol.SetRequest<SprotoType.copy_scene_result.request>(552);
		base.Protocol.SetProtocol<Protocol.copy_swipe_out>(232);
		base.Protocol.SetRequest<SprotoType.copy_swipe_out.request>(232);
		base.Protocol.SetProtocol<Protocol.count_down>(553);
		base.Protocol.SetRequest<SprotoType.count_down.request>(553);
		base.Protocol.SetProtocol<Protocol.del_friend>(125);
		base.Protocol.SetRequest<SprotoType.del_friend.request>(125);
		base.Protocol.SetProtocol<Protocol.download_finish>(270);
		base.Protocol.SetRequest<SprotoType.download_finish.request>(270);
		base.Protocol.SetProtocol<Protocol.drop_item_info>(527);
		base.Protocol.SetRequest<SprotoType.drop_item_info.request>(527);
		base.Protocol.SetProtocol<Protocol.enter_bar_fight>(207);
		base.Protocol.SetRequest<SprotoType.enter_bar_fight.request>(207);
		base.Protocol.SetProtocol<Protocol.enter_copy_scene>(107);
		base.Protocol.SetRequest<SprotoType.enter_copy_scene.request>(107);
		base.Protocol.SetProtocol<Protocol.enter_domin_pk_scene>(311);
		base.Protocol.SetRequest<SprotoType.enter_domin_pk_scene.request>(311);
		base.Protocol.SetProtocol<Protocol.enter_empty_scene>(451);
		base.Protocol.SetRequest<SprotoType.enter_empty_scene.request>(451);
		base.Protocol.SetProtocol<Protocol.enter_guild_battle>(286);
		base.Protocol.SetRequest<SprotoType.enter_guild_battle.request>(286);
		base.Protocol.SetProtocol<Protocol.enter_guild_boss_scene>(194);
		base.Protocol.SetRequest<SprotoType.enter_guild_boss_scene.request>(194);
		base.Protocol.SetProtocol<Protocol.enter_guild_city_scene>(322);
		base.Protocol.SetRequest<SprotoType.enter_guild_city_scene.request>(322);
		base.Protocol.SetProtocol<Protocol.enter_map>(503);
		base.Protocol.SetRequest<SprotoType.enter_map.request>(503);
		base.Protocol.SetProtocol<Protocol.enter_multi_copy_scene_confirm>(215);
		base.Protocol.SetRequest<SprotoType.enter_multi_copy_scene_confirm.request>(215);
		base.Protocol.SetProtocol<Protocol.enter_new_map>(106);
		base.Protocol.SetRequest<SprotoType.enter_new_map.request>(106);
		base.Protocol.SetProtocol<Protocol.enter_scuffle_batttle>(273);
		base.Protocol.SetRequest<SprotoType.enter_scuffle_batttle.request>(273);
		base.Protocol.SetProtocol<Protocol.enter_single_exp_scene>(276);
		base.Protocol.SetRequest<SprotoType.enter_single_exp_scene.request>(276);
		base.Protocol.SetProtocol<Protocol.enter_survive_batttle>(246);
		base.Protocol.SetRequest<SprotoType.enter_survive_batttle.request>(246);
		base.Protocol.SetProtocol<Protocol.enter_teleport_point>(251);
		base.Protocol.SetRequest<SprotoType.enter_teleport_point.request>(251);
		base.Protocol.SetProtocol<Protocol.enter_tower_copy_info>(204);
		base.Protocol.SetRequest<SprotoType.enter_tower_copy_info.request>(204);
		base.Protocol.SetProtocol<Protocol.enter_wild_boss>(201);
		base.Protocol.SetRequest<SprotoType.enter_wild_boss.request>(201);
		base.Protocol.SetProtocol<Protocol.equip_appraise>(303);
		base.Protocol.SetRequest<SprotoType.equip_appraise.request>(303);
		base.Protocol.SetProtocol<Protocol.equip_badge>(197);
		base.Protocol.SetRequest<SprotoType.equip_badge.request>(197);
		base.Protocol.SetProtocol<Protocol.equip_enhance>(167);
		base.Protocol.SetRequest<SprotoType.equip_enhance.request>(167);
		base.Protocol.SetProtocol<Protocol.equip_fashion_item>(221);
		base.Protocol.SetRequest<SprotoType.equip_fashion_item.request>(221);
		base.Protocol.SetProtocol<Protocol.equip_inhert>(184);
		base.Protocol.SetRequest<SprotoType.equip_inhert.request>(184);
		base.Protocol.SetResponse<SprotoType.equip_inhert.response>(184);
		base.Protocol.SetProtocol<Protocol.equip_inlay>(304);
		base.Protocol.SetRequest<SprotoType.equip_inlay.request>(304);
		base.Protocol.SetProtocol<Protocol.equip_item>(116);
		base.Protocol.SetRequest<SprotoType.equip_item.request>(116);
		base.Protocol.SetProtocol<Protocol.equip_refine>(183);
		base.Protocol.SetRequest<SprotoType.equip_refine.request>(183);
		base.Protocol.SetResponse<SprotoType.equip_refine.response>(183);
		base.Protocol.SetProtocol<Protocol.facebook_link>(5);
		base.Protocol.SetRequest<SprotoType.facebook_link.request>(5);
		base.Protocol.SetResponse<SprotoType.facebook_link.response>(5);
		base.Protocol.SetProtocol<Protocol.facebook_unlink>(6);
		base.Protocol.SetRequest<SprotoType.facebook_unlink.request>(6);
		base.Protocol.SetResponse<SprotoType.facebook_unlink.response>(6);
		base.Protocol.SetProtocol<Protocol.game_check>(308);
		base.Protocol.SetRequest<SprotoType.game_check.request>(308);
		base.Protocol.SetProtocol<Protocol.gather_other_player>(321);
		base.Protocol.SetRequest<SprotoType.gather_other_player.request>(321);
		base.Protocol.SetProtocol<Protocol.gather_team>(177);
		base.Protocol.SetRequest<SprotoType.gather_team.request>(177);
		base.Protocol.SetProtocol<Protocol.get_level_reward>(675);
		base.Protocol.SetRequest<SprotoType.get_level_reward.request>(675);
		base.Protocol.SetProtocol<Protocol.get_team_list>(165);
		base.Protocol.SetRequest<SprotoType.get_team_list.request>(165);
		base.Protocol.SetProtocol<Protocol.grant_activity_reward>(620);
		base.Protocol.SetRequest<SprotoType.grant_activity_reward.request>(620);
		base.Protocol.SetProtocol<Protocol.grant_daily_mission_reward>(612);
		base.Protocol.SetRequest<SprotoType.grant_daily_mission_reward.request>(612);
		base.Protocol.SetProtocol<Protocol.grant_tower_reward>(203);
		base.Protocol.SetRequest<SprotoType.grant_tower_reward.request>(203);
		base.Protocol.SetProtocol<Protocol.guild_approve_resverve>(154);
		base.Protocol.SetRequest<SprotoType.guild_approve_resverve.request>(154);
		base.Protocol.SetProtocol<Protocol.guild_battle_finish_info>(668);
		base.Protocol.SetRequest<SprotoType.guild_battle_finish_info.request>(668);
		base.Protocol.SetProtocol<Protocol.guild_battle_guess>(290);
		base.Protocol.SetRequest<SprotoType.guild_battle_guess.request>(290);
		base.Protocol.SetProtocol<Protocol.guild_battle_start>(670);
		base.Protocol.SetRequest<SprotoType.guild_battle_start.request>(670);
		base.Protocol.SetProtocol<Protocol.guild_create>(146);
		base.Protocol.SetRequest<SprotoType.guild_create.request>(146);
		base.Protocol.SetProtocol<Protocol.guild_donate>(175);
		base.Protocol.SetRequest<SprotoType.guild_donate.request>(175);
		base.Protocol.SetProtocol<Protocol.guild_invite>(247);
		base.Protocol.SetRequest<SprotoType.guild_invite.request>(247);
		base.Protocol.SetProtocol<Protocol.guild_invite_accept>(639);
		base.Protocol.SetRequest<SprotoType.guild_invite_accept.request>(639);
		base.Protocol.SetProtocol<Protocol.guild_job_change>(150);
		base.Protocol.SetRequest<SprotoType.guild_job_change.request>(150);
		base.Protocol.SetProtocol<Protocol.guild_join>(147);
		base.Protocol.SetRequest<SprotoType.guild_join.request>(147);
		base.Protocol.SetProtocol<Protocol.guild_kick>(149);
		base.Protocol.SetRequest<SprotoType.guild_kick.request>(149);
		base.Protocol.SetProtocol<Protocol.guild_leave>(148);
		base.Protocol.SetRequest<SprotoType.guild_leave.request>(148);
		base.Protocol.SetProtocol<Protocol.guild_log>(174);
		base.Protocol.SetRequest<SprotoType.guild_log.request>(174);
		base.Protocol.SetProtocol<Protocol.guild_req_info>(153);
		base.Protocol.SetRequest<SprotoType.guild_req_info.request>(153);
		base.Protocol.SetProtocol<Protocol.guild_req_list>(152);
		base.Protocol.SetRequest<SprotoType.guild_req_list.request>(152);
		base.Protocol.SetProtocol<Protocol.guild_skill_level>(151);
		base.Protocol.SetRequest<SprotoType.guild_skill_level.request>(151);
		base.Protocol.SetProtocol<Protocol.heart_beat>(218);
		base.Protocol.SetRequest<SprotoType.heart_beat.request>(218);
		base.Protocol.SetResponse<SprotoType.heart_beat.response>(218);
		base.Protocol.SetProtocol<Protocol.hit_action>(514);
		base.Protocol.SetRequest<SprotoType.hit_action.request>(514);
		base.Protocol.SetProtocol<Protocol.impact_npc>(298);
		base.Protocol.SetRequest<SprotoType.impact_npc.request>(298);
		base.Protocol.SetProtocol<Protocol.invite_join_team>(516);
		base.Protocol.SetRequest<SprotoType.invite_join_team.request>(516);
		base.Protocol.SetProtocol<Protocol.leave_copy_scene>(108);
		base.Protocol.SetRequest<SprotoType.leave_copy_scene.request>(108);
		base.Protocol.SetProtocol<Protocol.leave_game>(234);
		base.Protocol.SetRequest<SprotoType.leave_game.request>(234);
		base.Protocol.SetProtocol<Protocol.leave_team>(166);
		base.Protocol.SetRequest<SprotoType.leave_team.request>(166);
		base.Protocol.SetProtocol<Protocol.local_character_attack>(128);
		base.Protocol.SetRequest<SprotoType.local_character_attack.request>(128);
		base.Protocol.SetProtocol<Protocol.local_npc_die>(307);
		base.Protocol.SetRequest<SprotoType.local_npc_die.request>(307);
		base.Protocol.SetProtocol<Protocol.login>(4);
		base.Protocol.SetRequest<SprotoType.login.request>(4);
		base.Protocol.SetResponse<SprotoType.login.response>(4);
		base.Protocol.SetProtocol<Protocol.login_max_count>(578);
		base.Protocol.SetRequest<SprotoType.login_max_count.request>(578);
		base.Protocol.SetProtocol<Protocol.mail_delete>(532);
		base.Protocol.SetRequest<SprotoType.mail_delete.request>(532);
		base.Protocol.SetProtocol<Protocol.mail_operation>(123);
		base.Protocol.SetRequest<SprotoType.mail_operation.request>(123);
		base.Protocol.SetProtocol<Protocol.mail_update>(531);
		base.Protocol.SetRequest<SprotoType.mail_update.request>(531);
		base.Protocol.SetProtocol<Protocol.main_player_create>(504);
		base.Protocol.SetRequest<SprotoType.main_player_create.request>(504);
		base.Protocol.SetProtocol<Protocol.map_ready>(100);
		base.Protocol.SetProtocol<Protocol.mount_equip>(236);
		base.Protocol.SetRequest<SprotoType.mount_equip.request>(236);
		base.Protocol.SetProtocol<Protocol.mount_unequip>(237);
		base.Protocol.SetRequest<SprotoType.mount_unequip.request>(237);
		base.Protocol.SetProtocol<Protocol.mount_use_color>(241);
		base.Protocol.SetRequest<SprotoType.mount_use_color.request>(241);
		base.Protocol.SetProtocol<Protocol.move>(101);
		base.Protocol.SetRequest<SprotoType.move.request>(101);
		base.Protocol.SetResponse<SprotoType.move.response>(101);
		base.Protocol.SetProtocol<Protocol.next_wave>(515);
		base.Protocol.SetRequest<SprotoType.next_wave.request>(515);
		base.Protocol.SetProtocol<Protocol.notice>(529);
		base.Protocol.SetRequest<SprotoType.notice.request>(529);
		base.Protocol.SetProtocol<Protocol.notice_add_friend>(536);
		base.Protocol.SetRequest<SprotoType.notice_add_friend.request>(536);
		base.Protocol.SetProtocol<Protocol.notice_copy_scene_info>(683);
		base.Protocol.SetRequest<SprotoType.notice_copy_scene_info.request>(683);
		base.Protocol.SetProtocol<Protocol.notice_guild_battle_rank>(676);
		base.Protocol.SetRequest<SprotoType.notice_guild_battle_rank.request>(676);
		base.Protocol.SetProtocol<Protocol.notice_money_copy_reward>(615);
		base.Protocol.SetRequest<SprotoType.notice_money_copy_reward.request>(615);
		base.Protocol.SetProtocol<Protocol.notice_relife_player>(618);
		base.Protocol.SetRequest<SprotoType.notice_relife_player.request>(618);
		base.Protocol.SetProtocol<Protocol.notice_urge_team_leader>(682);
		base.Protocol.SetRequest<SprotoType.notice_urge_team_leader.request>(682);
		base.Protocol.SetProtocol<Protocol.notify_confirm_state>(603);
		base.Protocol.SetRequest<SprotoType.notify_confirm_state.request>(603);
		base.Protocol.SetProtocol<Protocol.notify_copy_start_info>(629);
		base.Protocol.SetRequest<SprotoType.notify_copy_start_info.request>(629);
		base.Protocol.SetProtocol<Protocol.npc_create>(509);
		base.Protocol.SetRequest<SprotoType.npc_create.request>(509);
		base.Protocol.SetProtocol<Protocol.open_guild_boss>(233);
		base.Protocol.SetRequest<SprotoType.open_guild_boss.request>(233);
		base.Protocol.SetProtocol<Protocol.open_item_package>(224);
		base.Protocol.SetRequest<SprotoType.open_item_package.request>(224);
		base.Protocol.SetProtocol<Protocol.open_multi_tower_reward>(283);
		base.Protocol.SetRequest<SprotoType.open_multi_tower_reward.request>(283);
		base.Protocol.SetProtocol<Protocol.pause_participate_dance>(228);
		base.Protocol.SetRequest<SprotoType.pause_participate_dance.request>(228);
		base.Protocol.SetProtocol<Protocol.play_social_dance>(275);
		base.Protocol.SetRequest<SprotoType.play_social_dance.request>(275);
		base.Protocol.SetProtocol<Protocol.put_item_storagepack>(140);
		base.Protocol.SetRequest<SprotoType.put_item_storagepack.request>(140);
		base.Protocol.SetProtocol<Protocol.random_select_ok>(610);
		base.Protocol.SetRequest<SprotoType.random_select_ok.request>(610);
		base.Protocol.SetProtocol<Protocol.random_select_team>(214);
		base.Protocol.SetRequest<SprotoType.random_select_team.request>(214);
		base.Protocol.SetProtocol<Protocol.rank_pvp_create_zombie_user>(544);
		base.Protocol.SetRequest<SprotoType.rank_pvp_create_zombie_user.request>(544);
		base.Protocol.SetProtocol<Protocol.rank_pvp_history>(546);
		base.Protocol.SetRequest<SprotoType.rank_pvp_history.request>(546);
		base.Protocol.SetProtocol<Protocol.rank_pvp_other_player_die>(137);
		base.Protocol.SetRequest<SprotoType.rank_pvp_other_player_die.request>(137);
		base.Protocol.SetProtocol<Protocol.rank_pvp_player_attack>(136);
		base.Protocol.SetRequest<SprotoType.rank_pvp_player_attack.request>(136);
		base.Protocol.SetProtocol<Protocol.rank_pvp_reward>(545);
		base.Protocol.SetRequest<SprotoType.rank_pvp_reward.request>(545);
		base.Protocol.SetProtocol<Protocol.rank_pvp_start>(547);
		base.Protocol.SetRequest<SprotoType.rank_pvp_start.request>(547);
		base.Protocol.SetProtocol<Protocol.re_name>(301);
		base.Protocol.SetRequest<SprotoType.re_name.request>(301);
		base.Protocol.SetProtocol<Protocol.real_pvp_register>(138);
		base.Protocol.SetRequest<SprotoType.real_pvp_register.request>(138);
		base.Protocol.SetProtocol<Protocol.real_pvp_start>(549);
		base.Protocol.SetRequest<SprotoType.real_pvp_start.request>(549);
		base.Protocol.SetProtocol<Protocol.real_pvp_state>(548);
		base.Protocol.SetRequest<SprotoType.real_pvp_state.request>(548);
		base.Protocol.SetProtocol<Protocol.receive_level_reward>(297);
		base.Protocol.SetRequest<SprotoType.receive_level_reward.request>(297);
		base.Protocol.SetProtocol<Protocol.refresh_online_misison>(309);
		base.Protocol.SetRequest<SprotoType.refresh_online_misison.request>(309);
		base.Protocol.SetProtocol<Protocol.refresh_online_state>(281);
		base.Protocol.SetRequest<SprotoType.refresh_online_state.request>(281);
		base.Protocol.SetProtocol<Protocol.relife_player>(132);
		base.Protocol.SetRequest<SprotoType.relife_player.request>(132);
		base.Protocol.SetProtocol<Protocol.req_buy_guild_goods>(172);
		base.Protocol.SetRequest<SprotoType.req_buy_guild_goods.request>(172);
		base.Protocol.SetProtocol<Protocol.req_change_team_goal>(213);
		base.Protocol.SetRequest<SprotoType.req_change_team_goal.request>(213);
		base.Protocol.SetProtocol<Protocol.req_guild_battle_guess>(292);
		base.Protocol.SetRequest<SprotoType.req_guild_battle_guess.request>(292);
		base.Protocol.SetProtocol<Protocol.req_guild_battle_info>(285);
		base.Protocol.SetRequest<SprotoType.req_guild_battle_info.request>(285);
		base.Protocol.SetProtocol<Protocol.req_guild_battle_member>(288);
		base.Protocol.SetRequest<SprotoType.req_guild_battle_member.request>(288);
		base.Protocol.SetProtocol<Protocol.req_guild_battle_rank>(287);
		base.Protocol.SetRequest<SprotoType.req_guild_battle_rank.request>(287);
		base.Protocol.SetProtocol<Protocol.req_guild_battle_state>(295);
		base.Protocol.SetRequest<SprotoType.req_guild_battle_state.request>(295);
		base.Protocol.SetProtocol<Protocol.req_guild_member_info>(170);
		base.Protocol.SetRequest<SprotoType.req_guild_member_info.request>(170);
		base.Protocol.SetProtocol<Protocol.req_guild_notice>(169);
		base.Protocol.SetRequest<SprotoType.req_guild_notice.request>(169);
		base.Protocol.SetProtocol<Protocol.req_guild_score_info>(291);
		base.Protocol.SetRequest<SprotoType.req_guild_score_info.request>(291);
		base.Protocol.SetProtocol<Protocol.req_guild_skill>(212);
		base.Protocol.SetRequest<SprotoType.req_guild_skill.request>(212);
		base.Protocol.SetProtocol<Protocol.req_guild_star>(293);
		base.Protocol.SetProtocol<Protocol.req_invite_team>(109);
		base.Protocol.SetRequest<SprotoType.req_invite_team.request>(109);
		base.Protocol.SetProtocol<Protocol.req_invite_team_result>(517);
		base.Protocol.SetRequest<SprotoType.req_invite_team_result.request>(517);
		base.Protocol.SetProtocol<Protocol.req_join_team>(161);
		base.Protocol.SetRequest<SprotoType.req_join_team.request>(161);
		base.Protocol.SetProtocol<Protocol.req_level_reward>(296);
		base.Protocol.SetRequest<SprotoType.req_level_reward.request>(296);
		base.Protocol.SetProtocol<Protocol.req_offline_chat>(168);
		base.Protocol.SetRequest<SprotoType.req_offline_chat.request>(168);
		base.Protocol.SetProtocol<Protocol.req_open_guild_shop>(171);
		base.Protocol.SetRequest<SprotoType.req_open_guild_shop.request>(171);
		base.Protocol.SetProtocol<Protocol.req_other_team>(248);
		base.Protocol.SetRequest<SprotoType.req_other_team.request>(248);
		base.Protocol.SetProtocol<Protocol.req_random_online_character_list>(159);
		base.Protocol.SetRequest<SprotoType.req_random_online_character_list.request>(159);
		base.Protocol.SetProtocol<Protocol.req_seting_guild_appro>(173);
		base.Protocol.SetRequest<SprotoType.req_seting_guild_appro.request>(173);
		base.Protocol.SetProtocol<Protocol.request_activity_info>(225);
		base.Protocol.SetRequest<SprotoType.request_activity_info.request>(225);
		base.Protocol.SetProtocol<Protocol.request_bar_fight>(206);
		base.Protocol.SetRequest<SprotoType.request_bar_fight.request>(206);
		base.Protocol.SetProtocol<Protocol.request_battle_info>(231);
		base.Protocol.SetRequest<SprotoType.request_battle_info.request>(231);
		base.Protocol.SetProtocol<Protocol.request_big_pack>(260);
		base.Protocol.SetRequest<SprotoType.request_big_pack.request>(260);
		base.Protocol.SetProtocol<Protocol.request_change_pk_mode>(226);
		base.Protocol.SetRequest<SprotoType.request_change_pk_mode.request>(226);
		base.Protocol.SetProtocol<Protocol.request_daily_active>(261);
		base.Protocol.SetRequest<SprotoType.request_daily_active.request>(261);
		base.Protocol.SetProtocol<Protocol.request_daily_buy>(258);
		base.Protocol.SetRequest<SprotoType.request_daily_buy.request>(258);
		base.Protocol.SetProtocol<Protocol.request_daily_mission>(121);
		base.Protocol.SetRequest<SprotoType.request_daily_mission.request>(121);
		base.Protocol.SetProtocol<Protocol.request_dance_info>(227);
		base.Protocol.SetRequest<SprotoType.request_dance_info.request>(227);
		base.Protocol.SetProtocol<Protocol.request_dance_state_info>(313);
		base.Protocol.SetRequest<SprotoType.request_dance_state_info.request>(313);
		base.Protocol.SetProtocol<Protocol.request_domin_info>(310);
		base.Protocol.SetRequest<SprotoType.request_domin_info.request>(310);
		base.Protocol.SetProtocol<Protocol.request_first_buy>(259);
		base.Protocol.SetRequest<SprotoType.request_first_buy.request>(259);
		base.Protocol.SetProtocol<Protocol.request_guild_boss>(195);
		base.Protocol.SetRequest<SprotoType.request_guild_boss.request>(195);
		base.Protocol.SetProtocol<Protocol.request_guild_map_domine_top>(318);
		base.Protocol.SetRequest<SprotoType.request_guild_map_domine_top.request>(318);
		base.Protocol.SetProtocol<Protocol.request_guild_map_info>(319);
		base.Protocol.SetRequest<SprotoType.request_guild_map_info.request>(319);
		base.Protocol.SetProtocol<Protocol.request_guild_map_reward>(320);
		base.Protocol.SetRequest<SprotoType.request_guild_map_reward.request>(320);
		base.Protocol.SetProtocol<Protocol.request_guild_reward>(196);
		base.Protocol.SetRequest<SprotoType.request_guild_reward.request>(196);
		base.Protocol.SetProtocol<Protocol.request_invest_pack>(257);
		base.Protocol.SetRequest<SprotoType.request_invest_pack.request>(257);
		base.Protocol.SetProtocol<Protocol.request_level_pack>(256);
		base.Protocol.SetRequest<SprotoType.request_level_pack.request>(256);
		base.Protocol.SetProtocol<Protocol.request_line_state>(219);
		base.Protocol.SetRequest<SprotoType.request_line_state.request>(219);
		base.Protocol.SetProtocol<Protocol.request_mount_info>(235);
		base.Protocol.SetRequest<SprotoType.request_mount_info.request>(235);
		base.Protocol.SetProtocol<Protocol.request_random_name>(118);
		base.Protocol.SetRequest<SprotoType.request_random_name.request>(118);
		base.Protocol.SetResponse<SprotoType.request_random_name.response>(118);
		base.Protocol.SetProtocol<Protocol.request_random_rank_pvp_opponent>(133);
		base.Protocol.SetRequest<SprotoType.request_random_rank_pvp_opponent.request>(133);
		base.Protocol.SetProtocol<Protocol.request_rank_pvp_data>(210);
		base.Protocol.SetRequest<SprotoType.request_rank_pvp_data.request>(210);
		base.Protocol.SetProtocol<Protocol.request_rank_pvp_history>(211);
		base.Protocol.SetRequest<SprotoType.request_rank_pvp_history.request>(211);
		base.Protocol.SetProtocol<Protocol.request_retrieve>(279);
		base.Protocol.SetRequest<SprotoType.request_retrieve.request>(279);
		base.Protocol.SetProtocol<Protocol.request_retrieve_info>(278);
		base.Protocol.SetRequest<SprotoType.request_retrieve_info.request>(278);
		base.Protocol.SetProtocol<Protocol.request_sign_30_day_info>(252);
		base.Protocol.SetRequest<SprotoType.request_sign_30_day_info.request>(252);
		base.Protocol.SetProtocol<Protocol.request_sign_week_info>(253);
		base.Protocol.SetRequest<SprotoType.request_sign_week_info.request>(253);
		base.Protocol.SetProtocol<Protocol.request_slot_info>(242);
		base.Protocol.SetRequest<SprotoType.request_slot_info.request>(242);
		base.Protocol.SetProtocol<Protocol.request_slot_reward>(249);
		base.Protocol.SetRequest<SprotoType.request_slot_reward.request>(249);
		base.Protocol.SetProtocol<Protocol.request_slot_sum_reward>(244);
		base.Protocol.SetRequest<SprotoType.request_slot_sum_reward.request>(244);
		base.Protocol.SetProtocol<Protocol.request_special_big_pack>(274);
		base.Protocol.SetRequest<SprotoType.request_special_big_pack.request>(274);
		base.Protocol.SetProtocol<Protocol.request_survive_top>(245);
		base.Protocol.SetRequest<SprotoType.request_survive_top.request>(245);
		base.Protocol.SetProtocol<Protocol.request_top_rank_list>(191);
		base.Protocol.SetRequest<SprotoType.request_top_rank_list.request>(191);
		base.Protocol.SetProtocol<Protocol.request_top_rank_pvp_list>(134);
		base.Protocol.SetRequest<SprotoType.request_top_rank_pvp_list.request>(134);
		base.Protocol.SetProtocol<Protocol.request_tower_copy_info>(202);
		base.Protocol.SetRequest<SprotoType.request_tower_copy_info.request>(202);
		base.Protocol.SetProtocol<Protocol.request_update_friend_useinfo>(126);
		base.Protocol.SetRequest<SprotoType.request_update_friend_useinfo.request>(126);
		base.Protocol.SetProtocol<Protocol.request_update_storagepack>(139);
		base.Protocol.SetRequest<SprotoType.request_update_storagepack.request>(139);
		base.Protocol.SetProtocol<Protocol.request_wild_boss_info>(200);
		base.Protocol.SetRequest<SprotoType.request_wild_boss_info.request>(200);
		base.Protocol.SetProtocol<Protocol.require_daily_active_reward>(265);
		base.Protocol.SetRequest<SprotoType.require_daily_active_reward.request>(265);
		base.Protocol.SetProtocol<Protocol.require_domin_rewards>(312);
		base.Protocol.SetRequest<SprotoType.require_domin_rewards.request>(312);
		base.Protocol.SetProtocol<Protocol.require_first_buy_reward>(264);
		base.Protocol.SetRequest<SprotoType.require_first_buy_reward.request>(264);
		base.Protocol.SetProtocol<Protocol.require_invest_reward>(263);
		base.Protocol.SetRequest<SprotoType.require_invest_reward.request>(263);
		base.Protocol.SetProtocol<Protocol.require_level_reward>(262);
		base.Protocol.SetRequest<SprotoType.require_level_reward.request>(262);
		base.Protocol.SetProtocol<Protocol.require_vip_info>(299);
		base.Protocol.SetRequest<SprotoType.require_vip_info.request>(299);
		base.Protocol.SetProtocol<Protocol.require_vip_reward>(300);
		base.Protocol.SetRequest<SprotoType.require_vip_reward.request>(300);
		base.Protocol.SetProtocol<Protocol.ret_abandon_mission>(522);
		base.Protocol.SetRequest<SprotoType.ret_abandon_mission.request>(522);
		base.Protocol.SetProtocol<Protocol.ret_accept_mission>(520);
		base.Protocol.SetRequest<SprotoType.ret_accept_mission.request>(520);
		base.Protocol.SetProtocol<Protocol.ret_add_friend>(533);
		base.Protocol.SetRequest<SprotoType.ret_add_friend.request>(533);
		base.Protocol.SetProtocol<Protocol.ret_ask_confirm_multi_copy_scene>(217);
		base.Protocol.SetRequest<SprotoType.ret_ask_confirm_multi_copy_scene.request>(217);
		base.Protocol.SetProtocol<Protocol.ret_ask_shop_list>(554);
		base.Protocol.SetRequest<SprotoType.ret_ask_shop_list.request>(554);
		base.Protocol.SetProtocol<Protocol.ret_battle_info>(627);
		base.Protocol.SetRequest<SprotoType.ret_battle_info.request>(627);
		base.Protocol.SetProtocol<Protocol.ret_buy_car_shop>(691);
		base.Protocol.SetRequest<SprotoType.ret_buy_car_shop.request>(691);
		base.Protocol.SetProtocol<Protocol.ret_buy_guild_goods>(583);
		base.Protocol.SetRequest<SprotoType.ret_buy_guild_goods.request>(583);
		base.Protocol.SetProtocol<Protocol.ret_buy_invest_pack>(655);
		base.Protocol.SetRequest<SprotoType.ret_buy_invest_pack.request>(655);
		base.Protocol.SetProtocol<Protocol.ret_buy_shop_item>(652);
		base.Protocol.SetRequest<SprotoType.ret_buy_shop_item.request>(652);
		base.Protocol.SetProtocol<Protocol.ret_chat>(528);
		base.Protocol.SetRequest<SprotoType.ret_chat.request>(528);
		base.Protocol.SetProtocol<Protocol.ret_commercail_reward>(650);
		base.Protocol.SetRequest<SprotoType.ret_commercail_reward.request>(650);
		base.Protocol.SetProtocol<Protocol.ret_complete_mission>(521);
		base.Protocol.SetRequest<SprotoType.ret_complete_mission.request>(521);
		base.Protocol.SetProtocol<Protocol.ret_consign_ask_items_info>(596);
		base.Protocol.SetRequest<SprotoType.ret_consign_ask_items_info.request>(596);
		base.Protocol.SetProtocol<Protocol.ret_consign_ask_my_items>(595);
		base.Protocol.SetRequest<SprotoType.ret_consign_ask_my_items.request>(595);
		base.Protocol.SetProtocol<Protocol.ret_consign_buy_item>(597);
		base.Protocol.SetRequest<SprotoType.ret_consign_buy_item.request>(597);
		base.Protocol.SetProtocol<Protocol.ret_consign_cancel_sale>(594);
		base.Protocol.SetRequest<SprotoType.ret_consign_cancel_sale.request>(594);
		base.Protocol.SetProtocol<Protocol.ret_consign_sale_item>(593);
		base.Protocol.SetRequest<SprotoType.ret_consign_sale_item.request>(593);
		base.Protocol.SetProtocol<Protocol.ret_del_friend>(535);
		base.Protocol.SetRequest<SprotoType.ret_del_friend.request>(535);
		base.Protocol.SetProtocol<Protocol.ret_domin_info>(684);
		base.Protocol.SetRequest<SprotoType.ret_domin_info.request>(684);
		base.Protocol.SetProtocol<Protocol.ret_enter_guild_battle>(677);
		base.Protocol.SetRequest<SprotoType.ret_enter_guild_battle.request>(677);
		base.Protocol.SetProtocol<Protocol.ret_get_team_list>(574);
		base.Protocol.SetRequest<SprotoType.ret_get_team_list.request>(574);
		base.Protocol.SetProtocol<Protocol.ret_grant_tower_reward>(625);
		base.Protocol.SetRequest<SprotoType.ret_grant_tower_reward.request>(625);
		base.Protocol.SetProtocol<Protocol.ret_guild_approve_resverve>(591);
		base.Protocol.SetRequest<SprotoType.ret_guild_approve_resverve.request>(591);
		base.Protocol.SetProtocol<Protocol.ret_guild_battle_guess>(669);
		base.Protocol.SetRequest<SprotoType.ret_guild_battle_guess.request>(669);
		base.Protocol.SetProtocol<Protocol.ret_guild_battle_info>(662);
		base.Protocol.SetRequest<SprotoType.ret_guild_battle_info.request>(662);
		base.Protocol.SetProtocol<Protocol.ret_guild_battle_member>(665);
		base.Protocol.SetRequest<SprotoType.ret_guild_battle_member.request>(665);
		base.Protocol.SetProtocol<Protocol.ret_guild_battle_rank>(663);
		base.Protocol.SetRequest<SprotoType.ret_guild_battle_rank.request>(663);
		base.Protocol.SetProtocol<Protocol.ret_guild_battle_state>(673);
		base.Protocol.SetRequest<SprotoType.ret_guild_battle_state.request>(673);
		base.Protocol.SetProtocol<Protocol.ret_guild_create>(567);
		base.Protocol.SetRequest<SprotoType.ret_guild_create.request>(567);
		base.Protocol.SetProtocol<Protocol.ret_guild_donate>(585);
		base.Protocol.SetRequest<SprotoType.ret_guild_donate.request>(585);
		base.Protocol.SetProtocol<Protocol.ret_guild_job_change>(589);
		base.Protocol.SetRequest<SprotoType.ret_guild_job_change.request>(589);
		base.Protocol.SetProtocol<Protocol.ret_guild_join>(566);
		base.Protocol.SetRequest<SprotoType.ret_guild_join.request>(566);
		base.Protocol.SetProtocol<Protocol.ret_guild_kick>(590);
		base.Protocol.SetRequest<SprotoType.ret_guild_kick.request>(590);
		base.Protocol.SetProtocol<Protocol.ret_guild_leave>(565);
		base.Protocol.SetRequest<SprotoType.ret_guild_leave.request>(565);
		base.Protocol.SetProtocol<Protocol.ret_guild_log>(584);
		base.Protocol.SetRequest<SprotoType.ret_guild_log.request>(584);
		base.Protocol.SetProtocol<Protocol.ret_guild_map_domine_top>(688);
		base.Protocol.SetRequest<SprotoType.ret_guild_map_domine_top.request>(688);
		base.Protocol.SetProtocol<Protocol.ret_guild_map_reward>(690);
		base.Protocol.SetRequest<SprotoType.ret_guild_map_reward.request>(690);
		base.Protocol.SetProtocol<Protocol.ret_guild_member_info>(581);
		base.Protocol.SetRequest<SprotoType.ret_guild_member_info.request>(581);
		base.Protocol.SetProtocol<Protocol.ret_guild_req_info>(563);
		base.Protocol.SetRequest<SprotoType.ret_guild_req_info.request>(563);
		base.Protocol.SetProtocol<Protocol.ret_guild_req_list>(562);
		base.Protocol.SetRequest<SprotoType.ret_guild_req_list.request>(562);
		base.Protocol.SetProtocol<Protocol.ret_guild_score_info>(667);
		base.Protocol.SetRequest<SprotoType.ret_guild_score_info.request>(667);
		base.Protocol.SetProtocol<Protocol.ret_guild_skill_level>(564);
		base.Protocol.SetRequest<SprotoType.ret_guild_skill_level.request>(564);
		base.Protocol.SetProtocol<Protocol.ret_guild_star>(671);
		base.Protocol.SetRequest<SprotoType.ret_guild_star.request>(671);
		base.Protocol.SetProtocol<Protocol.ret_invite_join_team>(110);
		base.Protocol.SetRequest<SprotoType.ret_invite_join_team.request>(110);
		base.Protocol.SetProtocol<Protocol.ret_level_reward>(674);
		base.Protocol.SetRequest<SprotoType.ret_level_reward.request>(674);
		base.Protocol.SetProtocol<Protocol.ret_mount_equip>(631);
		base.Protocol.SetRequest<SprotoType.ret_mount_equip.request>(631);
		base.Protocol.SetProtocol<Protocol.ret_mount_info>(630);
		base.Protocol.SetRequest<SprotoType.ret_mount_info.request>(630);
		base.Protocol.SetProtocol<Protocol.ret_mount_use_color>(632);
		base.Protocol.SetRequest<SprotoType.ret_mount_use_color.request>(632);
		base.Protocol.SetProtocol<Protocol.ret_offline_chat>(579);
		base.Protocol.SetRequest<SprotoType.ret_offline_chat.request>(579);
		base.Protocol.SetProtocol<Protocol.ret_open_guild_boss>(628);
		base.Protocol.SetRequest<SprotoType.ret_open_guild_boss.request>(628);
		base.Protocol.SetProtocol<Protocol.ret_open_guild_shop>(582);
		base.Protocol.SetRequest<SprotoType.ret_open_guild_shop.request>(582);
		base.Protocol.SetProtocol<Protocol.ret_open_item_package>(617);
		base.Protocol.SetRequest<SprotoType.ret_open_item_package.request>(617);
		base.Protocol.SetProtocol<Protocol.ret_random_online_character_list>(570);
		base.Protocol.SetRequest<SprotoType.ret_random_online_character_list.request>(570);
		base.Protocol.SetProtocol<Protocol.ret_re_name>(680);
		base.Protocol.SetRequest<SprotoType.ret_re_name.request>(680);
		base.Protocol.SetProtocol<Protocol.ret_req_guild_skill>(609);
		base.Protocol.SetRequest<SprotoType.ret_req_guild_skill.request>(609);
		base.Protocol.SetProtocol<Protocol.ret_request_30_day_info>(640);
		base.Protocol.SetRequest<SprotoType.ret_request_30_day_info.request>(640);
		base.Protocol.SetProtocol<Protocol.ret_request_activity_info>(619);
		base.Protocol.SetRequest<SprotoType.ret_request_activity_info.request>(619);
		base.Protocol.SetProtocol<Protocol.ret_request_big_pack>(648);
		base.Protocol.SetRequest<SprotoType.ret_request_big_pack.request>(648);
		base.Protocol.SetProtocol<Protocol.ret_request_daily_active>(649);
		base.Protocol.SetRequest<SprotoType.ret_request_daily_active.request>(649);
		base.Protocol.SetProtocol<Protocol.ret_request_daily_buy>(646);
		base.Protocol.SetRequest<SprotoType.ret_request_daily_buy.request>(646);
		base.Protocol.SetProtocol<Protocol.ret_request_dance_info>(623);
		base.Protocol.SetRequest<SprotoType.ret_request_dance_info.request>(623);
		base.Protocol.SetProtocol<Protocol.ret_request_first_buy>(647);
		base.Protocol.SetRequest<SprotoType.ret_request_first_buy.request>(647);
		base.Protocol.SetProtocol<Protocol.ret_request_guild_boss>(599);
		base.Protocol.SetRequest<SprotoType.ret_request_guild_boss.request>(599);
		base.Protocol.SetProtocol<Protocol.ret_request_guild_map_info>(689);
		base.Protocol.SetRequest<SprotoType.ret_request_guild_map_info.request>(689);
		base.Protocol.SetProtocol<Protocol.ret_request_invest_pack>(645);
		base.Protocol.SetRequest<SprotoType.ret_request_invest_pack.request>(645);
		base.Protocol.SetProtocol<Protocol.ret_request_level_pack>(644);
		base.Protocol.SetRequest<SprotoType.ret_request_level_pack.request>(644);
		base.Protocol.SetProtocol<Protocol.ret_request_random_rank_pvp_opponent>(542);
		base.Protocol.SetRequest<SprotoType.ret_request_random_rank_pvp_opponent.request>(542);
		base.Protocol.SetProtocol<Protocol.ret_request_retrieve_info>(658);
		base.Protocol.SetRequest<SprotoType.ret_request_retrieve_info.request>(658);
		base.Protocol.SetProtocol<Protocol.ret_request_sign_week_info>(641);
		base.Protocol.SetRequest<SprotoType.ret_request_sign_week_info.request>(641);
		base.Protocol.SetProtocol<Protocol.ret_request_survive_top>(636);
		base.Protocol.SetRequest<SprotoType.ret_request_survive_top.request>(636);
		base.Protocol.SetProtocol<Protocol.ret_request_top_rank_pvp_list>(543);
		base.Protocol.SetRequest<SprotoType.ret_request_top_rank_pvp_list.request>(543);
		base.Protocol.SetProtocol<Protocol.ret_request_tower_copy_info>(606);
		base.Protocol.SetRequest<SprotoType.ret_request_tower_copy_info.request>(606);
		base.Protocol.SetProtocol<Protocol.ret_request_update_friend_useinfo>(534);
		base.Protocol.SetRequest<SprotoType.ret_request_update_friend_useinfo.request>(534);
		base.Protocol.SetProtocol<Protocol.ret_request_update_storagepack>(550);
		base.Protocol.SetRequest<SprotoType.ret_request_update_storagepack.request>(550);
		base.Protocol.SetProtocol<Protocol.ret_request_wild_boss_info>(605);
		base.Protocol.SetRequest<SprotoType.ret_request_wild_boss_info.request>(605);
		base.Protocol.SetProtocol<Protocol.ret_require_vip_info>(678);
		base.Protocol.SetRequest<SprotoType.ret_require_vip_info.request>(678);
		base.Protocol.SetProtocol<Protocol.ret_require_vip_reward>(679);
		base.Protocol.SetRequest<SprotoType.ret_require_vip_reward.request>(679);
		base.Protocol.SetProtocol<Protocol.ret_search_guild>(586);
		base.Protocol.SetRequest<SprotoType.ret_search_guild.request>(586);
		base.Protocol.SetProtocol<Protocol.ret_search_online_character_by_name>(571);
		base.Protocol.SetRequest<SprotoType.ret_search_online_character_by_name.request>(571);
		base.Protocol.SetProtocol<Protocol.ret_set_guild_battle_member>(666);
		base.Protocol.SetRequest<SprotoType.ret_set_guild_battle_member.request>(666);
		base.Protocol.SetProtocol<Protocol.ret_sign_30_day>(642);
		base.Protocol.SetRequest<SprotoType.ret_sign_30_day.request>(642);
		base.Protocol.SetProtocol<Protocol.ret_sign_week>(643);
		base.Protocol.SetRequest<SprotoType.ret_sign_week.request>(643);
		base.Protocol.SetProtocol<Protocol.ret_skill_use>(508);
		base.Protocol.SetRequest<SprotoType.ret_skill_use.request>(508);
		base.Protocol.SetProtocol<Protocol.ret_slot_info>(633);
		base.Protocol.SetRequest<SprotoType.ret_slot_info.request>(633);
		base.Protocol.SetProtocol<Protocol.ret_slot_sum_reward>(635);
		base.Protocol.SetRequest<SprotoType.ret_slot_sum_reward.request>(635);
		base.Protocol.SetProtocol<Protocol.ret_special_big_pack>(656);
		base.Protocol.SetRequest<SprotoType.ret_special_big_pack.request>(656);
		base.Protocol.SetProtocol<Protocol.ret_spin_slot>(634);
		base.Protocol.SetRequest<SprotoType.ret_spin_slot.request>(634);
		base.Protocol.SetProtocol<Protocol.ret_title_req_level_up>(569);
		base.Protocol.SetRequest<SprotoType.ret_title_req_level_up.request>(569);
		base.Protocol.SetProtocol<Protocol.ret_top_rank_list>(598);
		base.Protocol.SetRequest<SprotoType.ret_top_rank_list.request>(598);
		base.Protocol.SetProtocol<Protocol.ret_tower_reset>(626);
		base.Protocol.SetRequest<SprotoType.ret_tower_reset.request>(626);
		base.Protocol.SetProtocol<Protocol.ret_tower_wipe_out>(607);
		base.Protocol.SetRequest<SprotoType.ret_tower_wipe_out.request>(607);
		base.Protocol.SetProtocol<Protocol.ret_update_guild_star>(672);
		base.Protocol.SetRequest<SprotoType.ret_update_guild_star.request>(672);
		base.Protocol.SetProtocol<Protocol.ret_use_item>(526);
		base.Protocol.SetRequest<SprotoType.ret_use_item.request>(526);
		base.Protocol.SetProtocol<Protocol.ret_watch_video_info>(693);
		base.Protocol.SetRequest<SprotoType.ret_watch_video_info.request>(693);
		base.Protocol.SetProtocol<Protocol.retrieve_account>(660);
		base.Protocol.SetRequest<SprotoType.retrieve_account.request>(660);
		base.Protocol.SetProtocol<Protocol.sample_activity_result>(685);
		base.Protocol.SetRequest<SprotoType.sample_activity_result.request>(685);
		base.Protocol.SetProtocol<Protocol.sample_copy_result>(613);
		base.Protocol.SetRequest<SprotoType.sample_copy_result.request>(613);
		base.Protocol.SetProtocol<Protocol.search_guild>(176);
		base.Protocol.SetRequest<SprotoType.search_guild.request>(176);
		base.Protocol.SetProtocol<Protocol.search_online_character_by_name>(160);
		base.Protocol.SetRequest<SprotoType.search_online_character_by_name.request>(160);
		base.Protocol.SetProtocol<Protocol.select_pk_character>(135);
		base.Protocol.SetRequest<SprotoType.select_pk_character.request>(135);
		base.Protocol.SetProtocol<Protocol.sell_item>(129);
		base.Protocol.SetRequest<SprotoType.sell_item.request>(129);
		base.Protocol.SetProtocol<Protocol.send_daily_mission>(530);
		base.Protocol.SetRequest<SprotoType.send_daily_mission.request>(530);
		base.Protocol.SetProtocol<Protocol.send_dialog_notify>(653);
		base.Protocol.SetRequest<SprotoType.send_dialog_notify.request>(653);
		base.Protocol.SetProtocol<Protocol.send_escort_info>(621);
		base.Protocol.SetRequest<SprotoType.send_escort_info.request>(621);
		base.Protocol.SetProtocol<Protocol.send_mail>(122);
		base.Protocol.SetRequest<SprotoType.send_mail.request>(122);
		base.Protocol.SetProtocol<Protocol.send_mail_box>(284);
		base.Protocol.SetRequest<SprotoType.send_mail_box.request>(284);
		base.Protocol.SetProtocol<Protocol.set_guild_battle_member>(289);
		base.Protocol.SetRequest<SprotoType.set_guild_battle_member.request>(289);
		base.Protocol.SetProtocol<Protocol.set_mission_param>(524);
		base.Protocol.SetRequest<SprotoType.set_mission_param.request>(524);
		base.Protocol.SetProtocol<Protocol.set_mission_state>(523);
		base.Protocol.SetRequest<SprotoType.set_mission_state.request>(523);
		base.Protocol.SetProtocol<Protocol.show_damage_board>(511);
		base.Protocol.SetRequest<SprotoType.show_damage_board.request>(511);
		base.Protocol.SetProtocol<Protocol.show_player_damage_board>(687);
		base.Protocol.SetRequest<SprotoType.show_player_damage_board.request>(687);
		base.Protocol.SetProtocol<Protocol.show_reward_items_tips>(638);
		base.Protocol.SetRequest<SprotoType.show_reward_items_tips.request>(638);
		base.Protocol.SetProtocol<Protocol.sign_30_day>(254);
		base.Protocol.SetRequest<SprotoType.sign_30_day.request>(254);
		base.Protocol.SetProtocol<Protocol.sign_bar_fight>(282);
		base.Protocol.SetRequest<SprotoType.sign_bar_fight.request>(282);
		base.Protocol.SetProtocol<Protocol.sign_week>(255);
		base.Protocol.SetRequest<SprotoType.sign_week.request>(255);
		base.Protocol.SetProtocol<Protocol.single_copy_scene_npc_die>(127);
		base.Protocol.SetRequest<SprotoType.single_copy_scene_npc_die.request>(127);
		base.Protocol.SetProtocol<Protocol.skill_level_up>(130);
		base.Protocol.SetRequest<SprotoType.skill_level_up.request>(130);
		base.Protocol.SetProtocol<Protocol.skill_use>(102);
		base.Protocol.SetRequest<SprotoType.skill_use.request>(102);
		base.Protocol.SetProtocol<Protocol.spin_slot>(243);
		base.Protocol.SetRequest<SprotoType.spin_slot.request>(243);
		base.Protocol.SetProtocol<Protocol.start_battle>(220);
		base.Protocol.SetRequest<SprotoType.start_battle.request>(220);
		base.Protocol.SetProtocol<Protocol.start_download>(269);
		base.Protocol.SetRequest<SprotoType.start_download.request>(269);
		base.Protocol.SetProtocol<Protocol.start_enter_game>(654);
		base.Protocol.SetRequest<SprotoType.start_enter_game.request>(654);
		base.Protocol.SetProtocol<Protocol.start_participate_dance>(624);
		base.Protocol.SetRequest<SprotoType.start_participate_dance.request>(624);
		base.Protocol.SetProtocol<Protocol.stop_leave_copy>(250);
		base.Protocol.SetRequest<SprotoType.stop_leave_copy.request>(250);
		base.Protocol.SetProtocol<Protocol.stop_random_select_team>(216);
		base.Protocol.SetRequest<SprotoType.stop_random_select_team.request>(216);
		base.Protocol.SetProtocol<Protocol.survive_battle_finish>(637);
		base.Protocol.SetRequest<SprotoType.survive_battle_finish.request>(637);
		base.Protocol.SetProtocol<Protocol.syn_friend_info>(538);
		base.Protocol.SetRequest<SprotoType.syn_friend_info.request>(538);
		base.Protocol.SetProtocol<Protocol.syn_rank_pvp_data>(541);
		base.Protocol.SetRequest<SprotoType.syn_rank_pvp_data.request>(541);
		base.Protocol.SetProtocol<Protocol.sync_backpack_item>(592);
		base.Protocol.SetRequest<SprotoType.sync_backpack_item.request>(592);
		base.Protocol.SetProtocol<Protocol.sync_badgepack_item>(604);
		base.Protocol.SetRequest<SprotoType.sync_badgepack_item.request>(604);
		base.Protocol.SetProtocol<Protocol.sync_common_data>(614);
		base.Protocol.SetRequest<SprotoType.sync_common_data.request>(614);
		base.Protocol.SetProtocol<Protocol.sync_copyscenes_info>(555);
		base.Protocol.SetRequest<SprotoType.sync_copyscenes_info.request>(555);
		base.Protocol.SetProtocol<Protocol.sync_dance_state_info>(686);
		base.Protocol.SetRequest<SprotoType.sync_dance_state_info.request>(686);
		base.Protocol.SetProtocol<Protocol.sync_fashion_backpack_item>(616);
		base.Protocol.SetRequest<SprotoType.sync_fashion_backpack_item.request>(616);
		base.Protocol.SetProtocol<Protocol.sync_guild_new_member>(580);
		base.Protocol.SetRequest<SprotoType.sync_guild_new_member.request>(580);
		base.Protocol.SetProtocol<Protocol.sync_item_pack>(611);
		base.Protocol.SetRequest<SprotoType.sync_item_pack.request>(611);
		base.Protocol.SetProtocol<Protocol.sync_mission>(519);
		base.Protocol.SetRequest<SprotoType.sync_mission.request>(519);
		base.Protocol.SetProtocol<Protocol.sync_random_team_state>(681);
		base.Protocol.SetRequest<SprotoType.sync_random_team_state.request>(681);
		base.Protocol.SetProtocol<Protocol.sync_skill_info>(540);
		base.Protocol.SetRequest<SprotoType.sync_skill_info.request>(540);
		base.Protocol.SetResponse<SprotoType.sync_skill_info.response>(540);
		base.Protocol.SetProtocol<Protocol.sync_watch_video_info>(692);
		base.Protocol.SetRequest<SprotoType.sync_watch_video_info.request>(692);
		base.Protocol.SetProtocol<Protocol.take_item_storagepack>(141);
		base.Protocol.SetRequest<SprotoType.take_item_storagepack.request>(141);
		base.Protocol.SetProtocol<Protocol.team_kick>(164);
		base.Protocol.SetRequest<SprotoType.team_kick.request>(164);
		base.Protocol.SetProtocol<Protocol.tianti_req_win_count_rewards>(157);
		base.Protocol.SetRequest<SprotoType.tianti_req_win_count_rewards.request>(157);
		base.Protocol.SetProtocol<Protocol.tiantti_result>(551);
		base.Protocol.SetRequest<SprotoType.tiantti_result.request>(551);
		base.Protocol.SetProtocol<Protocol.title_req_level_up>(156);
		base.Protocol.SetRequest<SprotoType.title_req_level_up.request>(156);
		base.Protocol.SetProtocol<Protocol.tower_reset>(230);
		base.Protocol.SetRequest<SprotoType.tower_reset.request>(230);
		base.Protocol.SetProtocol<Protocol.tower_wipe_out>(208);
		base.Protocol.SetRequest<SprotoType.tower_wipe_out.request>(208);
		base.Protocol.SetProtocol<Protocol.tutorial_finish>(306);
		base.Protocol.SetRequest<SprotoType.tutorial_finish.request>(306);
		base.Protocol.SetProtocol<Protocol.unequip_badge>(198);
		base.Protocol.SetRequest<SprotoType.unequip_badge.request>(198);
		base.Protocol.SetProtocol<Protocol.unequip_fashion_item>(222);
		base.Protocol.SetRequest<SprotoType.unequip_fashion_item.request>(222);
		base.Protocol.SetProtocol<Protocol.unequip_item>(117);
		base.Protocol.SetRequest<SprotoType.unequip_item.request>(117);
		base.Protocol.SetProtocol<Protocol.unlock_function_complete>(268);
		base.Protocol.SetRequest<SprotoType.unlock_function_complete.request>(268);
		base.Protocol.SetProtocol<Protocol.unuse_mount>(239);
		base.Protocol.SetRequest<SprotoType.unuse_mount.request>(239);
		base.Protocol.SetProtocol<Protocol.update_client_state>(280);
		base.Protocol.SetRequest<SprotoType.update_client_state.request>(280);
		base.Protocol.SetProtocol<Protocol.update_copyscene_info>(561);
		base.Protocol.SetRequest<SprotoType.update_copyscene_info.request>(561);
		base.Protocol.SetProtocol<Protocol.update_game_server>(7);
		base.Protocol.SetRequest<SprotoType.update_game_server.request>(7);
		base.Protocol.SetResponse<SprotoType.update_game_server.response>(7);
		base.Protocol.SetProtocol<Protocol.update_guild_dance_time>(315);
		base.Protocol.SetRequest<SprotoType.update_guild_dance_time.request>(315);
		base.Protocol.SetProtocol<Protocol.update_guild_star>(294);
		base.Protocol.SetRequest<SprotoType.update_guild_star.request>(294);
		base.Protocol.SetProtocol<Protocol.update_item>(525);
		base.Protocol.SetRequest<SprotoType.update_item.request>(525);
		base.Protocol.SetProtocol<Protocol.update_line_state>(568);
		base.Protocol.SetRequest<SprotoType.update_line_state.request>(568);
		base.Protocol.SetProtocol<Protocol.update_misison_complete>(182);
		base.Protocol.SetRequest<SprotoType.update_misison_complete.request>(182);
		base.Protocol.SetProtocol<Protocol.update_misison_parm>(178);
		base.Protocol.SetRequest<SprotoType.update_misison_parm.request>(178);
		base.Protocol.SetProtocol<Protocol.update_player_map_info>(324);
		base.Protocol.SetRequest<SprotoType.update_player_map_info.request>(324);
		base.Protocol.SetResponse<SprotoType.update_player_map_info.response>(324);
		base.Protocol.SetProtocol<Protocol.update_queue_rank>(577);
		base.Protocol.SetRequest<SprotoType.update_queue_rank.request>(577);
		base.Protocol.SetProtocol<Protocol.update_sex_mini_score>(277);
		base.Protocol.SetRequest<SprotoType.update_sex_mini_score.request>(277);
		base.Protocol.SetProtocol<Protocol.update_team>(518);
		base.Protocol.SetRequest<SprotoType.update_team.request>(518);
		base.Protocol.SetProtocol<Protocol.update_team_member>(575);
		base.Protocol.SetRequest<SprotoType.update_team_member.request>(575);
		base.Protocol.SetProtocol<Protocol.update_team_setting>(163);
		base.Protocol.SetRequest<SprotoType.update_team_setting.request>(163);
		base.Protocol.SetProtocol<Protocol.urge_team_leader>(450);
		base.Protocol.SetRequest<SprotoType.urge_team_leader.request>(450);
		base.Protocol.SetProtocol<Protocol.use_dance>(229);
		base.Protocol.SetRequest<SprotoType.use_dance.request>(229);
		base.Protocol.SetProtocol<Protocol.use_dance_sound_box>(314);
		base.Protocol.SetRequest<SprotoType.use_dance_sound_box.request>(314);
		base.Protocol.SetProtocol<Protocol.use_item>(115);
		base.Protocol.SetRequest<SprotoType.use_item.request>(115);
		base.Protocol.SetProtocol<Protocol.use_mount>(238);
		base.Protocol.SetRequest<SprotoType.use_mount.request>(238);
		base.Protocol.SetProtocol<Protocol.use_skill_buff>(209);
		base.Protocol.SetRequest<SprotoType.use_skill_buff.request>(209);
		base.Protocol.SetProtocol<Protocol.verfiy>(3);
		base.Protocol.SetRequest<SprotoType.verfiy.request>(3);
		base.Protocol.SetResponse<SprotoType.verfiy.response>(3);
		base.Protocol.SetProtocol<Protocol.visitor>(2);
		base.Protocol.SetRequest<SprotoType.visitor.request>(2);
		base.Protocol.SetResponse<SprotoType.visitor.response>(2);
		base.Protocol.SetProtocol<Protocol.watch_video_info>(452);
		base.Protocol.SetRequest<SprotoType.watch_video_info.request>(452);
		base.Protocol.SetProtocol<Protocol.weapon_inhert>(305);
		base.Protocol.SetRequest<SprotoType.weapon_inhert.request>(305);
	}

	// Token: 0x04001F7E RID: 8062
	public static Protocol Instance = new Protocol();

	// Token: 0x0200066E RID: 1646
	public class abandon_mission
	{
		// Token: 0x04001F7F RID: 8063
		public const int Tag = 114;
	}

	// Token: 0x0200066F RID: 1647
	public class accept_damge
	{
		// Token: 0x04001F80 RID: 8064
		public const int Tag = 111;
	}

	// Token: 0x02000670 RID: 1648
	public class accept_mission
	{
		// Token: 0x04001F81 RID: 8065
		public const int Tag = 112;
	}

	// Token: 0x02000671 RID: 1649
	public class add_friend
	{
		// Token: 0x04001F82 RID: 8066
		public const int Tag = 124;
	}

	// Token: 0x02000672 RID: 1650
	public class aoi_add
	{
		// Token: 0x04001F83 RID: 8067
		public const int Tag = 505;
	}

	// Token: 0x02000673 RID: 1651
	public class aoi_relife_player
	{
		// Token: 0x04001F84 RID: 8068
		public const int Tag = 512;
	}

	// Token: 0x02000674 RID: 1652
	public class aoi_remove
	{
		// Token: 0x04001F85 RID: 8069
		public const int Tag = 506;
	}

	// Token: 0x02000675 RID: 1653
	public class aoi_social_dance
	{
		// Token: 0x04001F86 RID: 8070
		public const int Tag = 657;
	}

	// Token: 0x02000676 RID: 1654
	public class aoi_stop_move
	{
		// Token: 0x04001F87 RID: 8071
		public const int Tag = 513;
	}

	// Token: 0x02000677 RID: 1655
	public class aoi_update_attribute
	{
		// Token: 0x04001F88 RID: 8072
		public const int Tag = 510;
	}

	// Token: 0x02000678 RID: 1656
	public class aoi_update_move
	{
		// Token: 0x04001F89 RID: 8073
		public const int Tag = 507;
	}

	// Token: 0x02000679 RID: 1657
	public class apply_join_result
	{
		// Token: 0x04001F8A RID: 8074
		public const int Tag = 162;
	}

	// Token: 0x0200067A RID: 1658
	public class apply_join_state
	{
		// Token: 0x04001F8B RID: 8075
		public const int Tag = 576;
	}

	// Token: 0x0200067B RID: 1659
	public class apply_join_team
	{
		// Token: 0x04001F8C RID: 8076
		public const int Tag = 572;
	}

	// Token: 0x0200067C RID: 1660
	public class approve_resverve_friend
	{
		// Token: 0x04001F8D RID: 8077
		public const int Tag = 158;
	}

	// Token: 0x0200067D RID: 1661
	public class ask_character_info
	{
		// Token: 0x04001F8E RID: 8078
		public const int Tag = 142;
	}

	// Token: 0x0200067E RID: 1662
	public class ask_confirm
	{
		// Token: 0x04001F8F RID: 8079
		public const int Tag = 601;
	}

	// Token: 0x0200067F RID: 1663
	public class ask_confirm_multi_copy_scene
	{
		// Token: 0x04001F90 RID: 8080
		public const int Tag = 602;
	}

	// Token: 0x02000680 RID: 1664
	public class ask_copyscenes_info
	{
		// Token: 0x04001F91 RID: 8081
		public const int Tag = 145;
	}

	// Token: 0x02000681 RID: 1665
	public class ask_pickup_item
	{
		// Token: 0x04001F92 RID: 8082
		public const int Tag = 119;
	}

	// Token: 0x02000682 RID: 1666
	public class ask_shop_list
	{
		// Token: 0x04001F93 RID: 8083
		public const int Tag = 143;
	}

	// Token: 0x02000683 RID: 1667
	public class attack_local_npc
	{
		// Token: 0x04001F94 RID: 8084
		public const int Tag = 317;
	}

	// Token: 0x02000684 RID: 1668
	public class attribute_inhert
	{
		// Token: 0x04001F95 RID: 8085
		public const int Tag = 302;
	}

	// Token: 0x02000685 RID: 1669
	public class badge_merge
	{
		// Token: 0x04001F96 RID: 8086
		public const int Tag = 199;
	}

	// Token: 0x02000686 RID: 1670
	public class bar_fight_notify
	{
		// Token: 0x04001F97 RID: 8087
		public const int Tag = 659;
	}

	// Token: 0x02000687 RID: 1671
	public class be_deleted_friend
	{
		// Token: 0x04001F98 RID: 8088
		public const int Tag = 537;
	}

	// Token: 0x02000688 RID: 1672
	public class buy_big_pack
	{
		// Token: 0x04001F99 RID: 8089
		public const int Tag = 272;
	}

	// Token: 0x02000689 RID: 1673
	public class buy_car_shop
	{
		// Token: 0x04001F9A RID: 8090
		public const int Tag = 323;
	}

	// Token: 0x0200068A RID: 1674
	public class buy_invest_pack
	{
		// Token: 0x04001F9B RID: 8091
		public const int Tag = 271;
	}

	// Token: 0x0200068B RID: 1675
	public class buy_shop_item
	{
		// Token: 0x04001F9C RID: 8092
		public const int Tag = 144;
	}

	// Token: 0x0200068C RID: 1676
	public class cancel_apply_join_team
	{
		// Token: 0x04001F9D RID: 8093
		public const int Tag = 573;
	}

	// Token: 0x0200068D RID: 1677
	public class car_chase_result
	{
		// Token: 0x04001F9E RID: 8094
		public const int Tag = 193;
	}

	// Token: 0x0200068E RID: 1678
	public class car_copy_result
	{
		// Token: 0x04001F9F RID: 8095
		public const int Tag = 608;
	}

	// Token: 0x0200068F RID: 1679
	public class change_item_state
	{
		// Token: 0x04001FA0 RID: 8096
		public const int Tag = 240;
	}

	// Token: 0x02000690 RID: 1680
	public class change_mount_state
	{
		// Token: 0x04001FA1 RID: 8097
		public const int Tag = 267;
	}

	// Token: 0x02000691 RID: 1681
	public class change_potion
	{
		// Token: 0x04001FA2 RID: 8098
		public const int Tag = 185;
	}

	// Token: 0x02000692 RID: 1682
	public class change_scene_line
	{
		// Token: 0x04001FA3 RID: 8099
		public const int Tag = 155;
	}

	// Token: 0x02000693 RID: 1683
	public class change_show_type
	{
		// Token: 0x04001FA4 RID: 8100
		public const int Tag = 223;
	}

	// Token: 0x02000694 RID: 1684
	public class change_skill_index
	{
		// Token: 0x04001FA5 RID: 8101
		public const int Tag = 316;
	}

	// Token: 0x02000695 RID: 1685
	public class change_skill_position
	{
		// Token: 0x04001FA6 RID: 8102
		public const int Tag = 192;
	}

	// Token: 0x02000696 RID: 1686
	public class character_create
	{
		// Token: 0x04001FA7 RID: 8103
		public const int Tag = 104;
	}

	// Token: 0x02000697 RID: 1687
	public class character_list
	{
		// Token: 0x04001FA8 RID: 8104
		public const int Tag = 103;
	}

	// Token: 0x02000698 RID: 1688
	public class character_pick
	{
		// Token: 0x04001FA9 RID: 8105
		public const int Tag = 105;
	}

	// Token: 0x02000699 RID: 1689
	public class chat
	{
		// Token: 0x04001FAA RID: 8106
		public const int Tag = 120;
	}

	// Token: 0x0200069A RID: 1690
	public class check_purchase
	{
		// Token: 0x04001FAB RID: 8107
		public const int Tag = 266;
	}

	// Token: 0x0200069B RID: 1691
	public class comb_value_up_tip
	{
		// Token: 0x04001FAC RID: 8108
		public const int Tag = 651;
	}

	// Token: 0x0200069C RID: 1692
	public class complete_mission
	{
		// Token: 0x04001FAD RID: 8109
		public const int Tag = 113;
	}

	// Token: 0x0200069D RID: 1693
	public class consign_ask_items_info
	{
		// Token: 0x04001FAE RID: 8110
		public const int Tag = 189;
	}

	// Token: 0x0200069E RID: 1694
	public class consign_ask_my_items
	{
		// Token: 0x04001FAF RID: 8111
		public const int Tag = 188;
	}

	// Token: 0x0200069F RID: 1695
	public class consign_buy_item
	{
		// Token: 0x04001FB0 RID: 8112
		public const int Tag = 190;
	}

	// Token: 0x020006A0 RID: 1696
	public class consign_cancel_sale
	{
		// Token: 0x04001FB1 RID: 8113
		public const int Tag = 187;
	}

	// Token: 0x020006A1 RID: 1697
	public class consign_sale_item
	{
		// Token: 0x04001FB2 RID: 8114
		public const int Tag = 186;
	}

	// Token: 0x020006A2 RID: 1698
	public class continue_tower_copy
	{
		// Token: 0x04001FB3 RID: 8115
		public const int Tag = 205;
	}

	// Token: 0x020006A3 RID: 1699
	public class copy_scene_result
	{
		// Token: 0x04001FB4 RID: 8116
		public const int Tag = 552;
	}

	// Token: 0x020006A4 RID: 1700
	public class copy_swipe_out
	{
		// Token: 0x04001FB5 RID: 8117
		public const int Tag = 232;
	}

	// Token: 0x020006A5 RID: 1701
	public class count_down
	{
		// Token: 0x04001FB6 RID: 8118
		public const int Tag = 553;
	}

	// Token: 0x020006A6 RID: 1702
	public class del_friend
	{
		// Token: 0x04001FB7 RID: 8119
		public const int Tag = 125;
	}

	// Token: 0x020006A7 RID: 1703
	public class download_finish
	{
		// Token: 0x04001FB8 RID: 8120
		public const int Tag = 270;
	}

	// Token: 0x020006A8 RID: 1704
	public class drop_item_info
	{
		// Token: 0x04001FB9 RID: 8121
		public const int Tag = 527;
	}

	// Token: 0x020006A9 RID: 1705
	public class enter_bar_fight
	{
		// Token: 0x04001FBA RID: 8122
		public const int Tag = 207;
	}

	// Token: 0x020006AA RID: 1706
	public class enter_copy_scene
	{
		// Token: 0x04001FBB RID: 8123
		public const int Tag = 107;
	}

	// Token: 0x020006AB RID: 1707
	public class enter_domin_pk_scene
	{
		// Token: 0x04001FBC RID: 8124
		public const int Tag = 311;
	}

	// Token: 0x020006AC RID: 1708
	public class enter_empty_scene
	{
		// Token: 0x04001FBD RID: 8125
		public const int Tag = 451;
	}

	// Token: 0x020006AD RID: 1709
	public class enter_guild_battle
	{
		// Token: 0x04001FBE RID: 8126
		public const int Tag = 286;
	}

	// Token: 0x020006AE RID: 1710
	public class enter_guild_boss_scene
	{
		// Token: 0x04001FBF RID: 8127
		public const int Tag = 194;
	}

	// Token: 0x020006AF RID: 1711
	public class enter_guild_city_scene
	{
		// Token: 0x04001FC0 RID: 8128
		public const int Tag = 322;
	}

	// Token: 0x020006B0 RID: 1712
	public class enter_map
	{
		// Token: 0x04001FC1 RID: 8129
		public const int Tag = 503;
	}

	// Token: 0x020006B1 RID: 1713
	public class enter_multi_copy_scene_confirm
	{
		// Token: 0x04001FC2 RID: 8130
		public const int Tag = 215;
	}

	// Token: 0x020006B2 RID: 1714
	public class enter_new_map
	{
		// Token: 0x04001FC3 RID: 8131
		public const int Tag = 106;
	}

	// Token: 0x020006B3 RID: 1715
	public class enter_scuffle_batttle
	{
		// Token: 0x04001FC4 RID: 8132
		public const int Tag = 273;
	}

	// Token: 0x020006B4 RID: 1716
	public class enter_single_exp_scene
	{
		// Token: 0x04001FC5 RID: 8133
		public const int Tag = 276;
	}

	// Token: 0x020006B5 RID: 1717
	public class enter_survive_batttle
	{
		// Token: 0x04001FC6 RID: 8134
		public const int Tag = 246;
	}

	// Token: 0x020006B6 RID: 1718
	public class enter_teleport_point
	{
		// Token: 0x04001FC7 RID: 8135
		public const int Tag = 251;
	}

	// Token: 0x020006B7 RID: 1719
	public class enter_tower_copy_info
	{
		// Token: 0x04001FC8 RID: 8136
		public const int Tag = 204;
	}

	// Token: 0x020006B8 RID: 1720
	public class enter_wild_boss
	{
		// Token: 0x04001FC9 RID: 8137
		public const int Tag = 201;
	}

	// Token: 0x020006B9 RID: 1721
	public class equip_appraise
	{
		// Token: 0x04001FCA RID: 8138
		public const int Tag = 303;
	}

	// Token: 0x020006BA RID: 1722
	public class equip_badge
	{
		// Token: 0x04001FCB RID: 8139
		public const int Tag = 197;
	}

	// Token: 0x020006BB RID: 1723
	public class equip_enhance
	{
		// Token: 0x04001FCC RID: 8140
		public const int Tag = 167;
	}

	// Token: 0x020006BC RID: 1724
	public class equip_fashion_item
	{
		// Token: 0x04001FCD RID: 8141
		public const int Tag = 221;
	}

	// Token: 0x020006BD RID: 1725
	public class equip_inhert
	{
		// Token: 0x04001FCE RID: 8142
		public const int Tag = 184;
	}

	// Token: 0x020006BE RID: 1726
	public class equip_inlay
	{
		// Token: 0x04001FCF RID: 8143
		public const int Tag = 304;
	}

	// Token: 0x020006BF RID: 1727
	public class equip_item
	{
		// Token: 0x04001FD0 RID: 8144
		public const int Tag = 116;
	}

	// Token: 0x020006C0 RID: 1728
	public class equip_refine
	{
		// Token: 0x04001FD1 RID: 8145
		public const int Tag = 183;
	}

	// Token: 0x020006C1 RID: 1729
	public class facebook_link
	{
		// Token: 0x04001FD2 RID: 8146
		public const int Tag = 5;
	}

	// Token: 0x020006C2 RID: 1730
	public class facebook_unlink
	{
		// Token: 0x04001FD3 RID: 8147
		public const int Tag = 6;
	}

	// Token: 0x020006C3 RID: 1731
	public class game_check
	{
		// Token: 0x04001FD4 RID: 8148
		public const int Tag = 308;
	}

	// Token: 0x020006C4 RID: 1732
	public class gather_other_player
	{
		// Token: 0x04001FD5 RID: 8149
		public const int Tag = 321;
	}

	// Token: 0x020006C5 RID: 1733
	public class gather_team
	{
		// Token: 0x04001FD6 RID: 8150
		public const int Tag = 177;
	}

	// Token: 0x020006C6 RID: 1734
	public class get_level_reward
	{
		// Token: 0x04001FD7 RID: 8151
		public const int Tag = 675;
	}

	// Token: 0x020006C7 RID: 1735
	public class get_team_list
	{
		// Token: 0x04001FD8 RID: 8152
		public const int Tag = 165;
	}

	// Token: 0x020006C8 RID: 1736
	public class grant_activity_reward
	{
		// Token: 0x04001FD9 RID: 8153
		public const int Tag = 620;
	}

	// Token: 0x020006C9 RID: 1737
	public class grant_daily_mission_reward
	{
		// Token: 0x04001FDA RID: 8154
		public const int Tag = 612;
	}

	// Token: 0x020006CA RID: 1738
	public class grant_tower_reward
	{
		// Token: 0x04001FDB RID: 8155
		public const int Tag = 203;
	}

	// Token: 0x020006CB RID: 1739
	public class guild_approve_resverve
	{
		// Token: 0x04001FDC RID: 8156
		public const int Tag = 154;
	}

	// Token: 0x020006CC RID: 1740
	public class guild_battle_finish_info
	{
		// Token: 0x04001FDD RID: 8157
		public const int Tag = 668;
	}

	// Token: 0x020006CD RID: 1741
	public class guild_battle_guess
	{
		// Token: 0x04001FDE RID: 8158
		public const int Tag = 290;
	}

	// Token: 0x020006CE RID: 1742
	public class guild_battle_start
	{
		// Token: 0x04001FDF RID: 8159
		public const int Tag = 670;
	}

	// Token: 0x020006CF RID: 1743
	public class guild_create
	{
		// Token: 0x04001FE0 RID: 8160
		public const int Tag = 146;
	}

	// Token: 0x020006D0 RID: 1744
	public class guild_donate
	{
		// Token: 0x04001FE1 RID: 8161
		public const int Tag = 175;
	}

	// Token: 0x020006D1 RID: 1745
	public class guild_invite
	{
		// Token: 0x04001FE2 RID: 8162
		public const int Tag = 247;
	}

	// Token: 0x020006D2 RID: 1746
	public class guild_invite_accept
	{
		// Token: 0x04001FE3 RID: 8163
		public const int Tag = 639;
	}

	// Token: 0x020006D3 RID: 1747
	public class guild_job_change
	{
		// Token: 0x04001FE4 RID: 8164
		public const int Tag = 150;
	}

	// Token: 0x020006D4 RID: 1748
	public class guild_join
	{
		// Token: 0x04001FE5 RID: 8165
		public const int Tag = 147;
	}

	// Token: 0x020006D5 RID: 1749
	public class guild_kick
	{
		// Token: 0x04001FE6 RID: 8166
		public const int Tag = 149;
	}

	// Token: 0x020006D6 RID: 1750
	public class guild_leave
	{
		// Token: 0x04001FE7 RID: 8167
		public const int Tag = 148;
	}

	// Token: 0x020006D7 RID: 1751
	public class guild_log
	{
		// Token: 0x04001FE8 RID: 8168
		public const int Tag = 174;
	}

	// Token: 0x020006D8 RID: 1752
	public class guild_req_info
	{
		// Token: 0x04001FE9 RID: 8169
		public const int Tag = 153;
	}

	// Token: 0x020006D9 RID: 1753
	public class guild_req_list
	{
		// Token: 0x04001FEA RID: 8170
		public const int Tag = 152;
	}

	// Token: 0x020006DA RID: 1754
	public class guild_skill_level
	{
		// Token: 0x04001FEB RID: 8171
		public const int Tag = 151;
	}

	// Token: 0x020006DB RID: 1755
	public class heart_beat
	{
		// Token: 0x04001FEC RID: 8172
		public const int Tag = 218;
	}

	// Token: 0x020006DC RID: 1756
	public class hit_action
	{
		// Token: 0x04001FED RID: 8173
		public const int Tag = 514;
	}

	// Token: 0x020006DD RID: 1757
	public class impact_npc
	{
		// Token: 0x04001FEE RID: 8174
		public const int Tag = 298;
	}

	// Token: 0x020006DE RID: 1758
	public class invite_join_team
	{
		// Token: 0x04001FEF RID: 8175
		public const int Tag = 516;
	}

	// Token: 0x020006DF RID: 1759
	public class leave_copy_scene
	{
		// Token: 0x04001FF0 RID: 8176
		public const int Tag = 108;
	}

	// Token: 0x020006E0 RID: 1760
	public class leave_game
	{
		// Token: 0x04001FF1 RID: 8177
		public const int Tag = 234;
	}

	// Token: 0x020006E1 RID: 1761
	public class leave_team
	{
		// Token: 0x04001FF2 RID: 8178
		public const int Tag = 166;
	}

	// Token: 0x020006E2 RID: 1762
	public class local_character_attack
	{
		// Token: 0x04001FF3 RID: 8179
		public const int Tag = 128;
	}

	// Token: 0x020006E3 RID: 1763
	public class local_npc_die
	{
		// Token: 0x04001FF4 RID: 8180
		public const int Tag = 307;
	}

	// Token: 0x020006E4 RID: 1764
	public class login
	{
		// Token: 0x04001FF5 RID: 8181
		public const int Tag = 4;
	}

	// Token: 0x020006E5 RID: 1765
	public class login_max_count
	{
		// Token: 0x04001FF6 RID: 8182
		public const int Tag = 578;
	}

	// Token: 0x020006E6 RID: 1766
	public class mail_delete
	{
		// Token: 0x04001FF7 RID: 8183
		public const int Tag = 532;
	}

	// Token: 0x020006E7 RID: 1767
	public class mail_operation
	{
		// Token: 0x04001FF8 RID: 8184
		public const int Tag = 123;
	}

	// Token: 0x020006E8 RID: 1768
	public class mail_update
	{
		// Token: 0x04001FF9 RID: 8185
		public const int Tag = 531;
	}

	// Token: 0x020006E9 RID: 1769
	public class main_player_create
	{
		// Token: 0x04001FFA RID: 8186
		public const int Tag = 504;
	}

	// Token: 0x020006EA RID: 1770
	public class map_ready
	{
		// Token: 0x04001FFB RID: 8187
		public const int Tag = 100;
	}

	// Token: 0x020006EB RID: 1771
	public class mount_equip
	{
		// Token: 0x04001FFC RID: 8188
		public const int Tag = 236;
	}

	// Token: 0x020006EC RID: 1772
	public class mount_unequip
	{
		// Token: 0x04001FFD RID: 8189
		public const int Tag = 237;
	}

	// Token: 0x020006ED RID: 1773
	public class mount_use_color
	{
		// Token: 0x04001FFE RID: 8190
		public const int Tag = 241;
	}

	// Token: 0x020006EE RID: 1774
	public class move
	{
		// Token: 0x04001FFF RID: 8191
		public const int Tag = 101;
	}

	// Token: 0x020006EF RID: 1775
	public class next_wave
	{
		// Token: 0x04002000 RID: 8192
		public const int Tag = 515;
	}

	// Token: 0x020006F0 RID: 1776
	public class notice
	{
		// Token: 0x04002001 RID: 8193
		public const int Tag = 529;
	}

	// Token: 0x020006F1 RID: 1777
	public class notice_add_friend
	{
		// Token: 0x04002002 RID: 8194
		public const int Tag = 536;
	}

	// Token: 0x020006F2 RID: 1778
	public class notice_copy_scene_info
	{
		// Token: 0x04002003 RID: 8195
		public const int Tag = 683;
	}

	// Token: 0x020006F3 RID: 1779
	public class notice_guild_battle_rank
	{
		// Token: 0x04002004 RID: 8196
		public const int Tag = 676;
	}

	// Token: 0x020006F4 RID: 1780
	public class notice_money_copy_reward
	{
		// Token: 0x04002005 RID: 8197
		public const int Tag = 615;
	}

	// Token: 0x020006F5 RID: 1781
	public class notice_relife_player
	{
		// Token: 0x04002006 RID: 8198
		public const int Tag = 618;
	}

	// Token: 0x020006F6 RID: 1782
	public class notice_urge_team_leader
	{
		// Token: 0x04002007 RID: 8199
		public const int Tag = 682;
	}

	// Token: 0x020006F7 RID: 1783
	public class notify_confirm_state
	{
		// Token: 0x04002008 RID: 8200
		public const int Tag = 603;
	}

	// Token: 0x020006F8 RID: 1784
	public class notify_copy_start_info
	{
		// Token: 0x04002009 RID: 8201
		public const int Tag = 629;
	}

	// Token: 0x020006F9 RID: 1785
	public class npc_create
	{
		// Token: 0x0400200A RID: 8202
		public const int Tag = 509;
	}

	// Token: 0x020006FA RID: 1786
	public class open_guild_boss
	{
		// Token: 0x0400200B RID: 8203
		public const int Tag = 233;
	}

	// Token: 0x020006FB RID: 1787
	public class open_item_package
	{
		// Token: 0x0400200C RID: 8204
		public const int Tag = 224;
	}

	// Token: 0x020006FC RID: 1788
	public class open_multi_tower_reward
	{
		// Token: 0x0400200D RID: 8205
		public const int Tag = 283;
	}

	// Token: 0x020006FD RID: 1789
	public class pause_participate_dance
	{
		// Token: 0x0400200E RID: 8206
		public const int Tag = 228;
	}

	// Token: 0x020006FE RID: 1790
	public class play_social_dance
	{
		// Token: 0x0400200F RID: 8207
		public const int Tag = 275;
	}

	// Token: 0x020006FF RID: 1791
	public class put_item_storagepack
	{
		// Token: 0x04002010 RID: 8208
		public const int Tag = 140;
	}

	// Token: 0x02000700 RID: 1792
	public class random_select_ok
	{
		// Token: 0x04002011 RID: 8209
		public const int Tag = 610;
	}

	// Token: 0x02000701 RID: 1793
	public class random_select_team
	{
		// Token: 0x04002012 RID: 8210
		public const int Tag = 214;
	}

	// Token: 0x02000702 RID: 1794
	public class rank_pvp_create_zombie_user
	{
		// Token: 0x04002013 RID: 8211
		public const int Tag = 544;
	}

	// Token: 0x02000703 RID: 1795
	public class rank_pvp_history
	{
		// Token: 0x04002014 RID: 8212
		public const int Tag = 546;
	}

	// Token: 0x02000704 RID: 1796
	public class rank_pvp_other_player_die
	{
		// Token: 0x04002015 RID: 8213
		public const int Tag = 137;
	}

	// Token: 0x02000705 RID: 1797
	public class rank_pvp_player_attack
	{
		// Token: 0x04002016 RID: 8214
		public const int Tag = 136;
	}

	// Token: 0x02000706 RID: 1798
	public class rank_pvp_reward
	{
		// Token: 0x04002017 RID: 8215
		public const int Tag = 545;
	}

	// Token: 0x02000707 RID: 1799
	public class rank_pvp_start
	{
		// Token: 0x04002018 RID: 8216
		public const int Tag = 547;
	}

	// Token: 0x02000708 RID: 1800
	public class re_name
	{
		// Token: 0x04002019 RID: 8217
		public const int Tag = 301;
	}

	// Token: 0x02000709 RID: 1801
	public class real_pvp_register
	{
		// Token: 0x0400201A RID: 8218
		public const int Tag = 138;
	}

	// Token: 0x0200070A RID: 1802
	public class real_pvp_start
	{
		// Token: 0x0400201B RID: 8219
		public const int Tag = 549;
	}

	// Token: 0x0200070B RID: 1803
	public class real_pvp_state
	{
		// Token: 0x0400201C RID: 8220
		public const int Tag = 548;
	}

	// Token: 0x0200070C RID: 1804
	public class receive_level_reward
	{
		// Token: 0x0400201D RID: 8221
		public const int Tag = 297;
	}

	// Token: 0x0200070D RID: 1805
	public class refresh_online_misison
	{
		// Token: 0x0400201E RID: 8222
		public const int Tag = 309;
	}

	// Token: 0x0200070E RID: 1806
	public class refresh_online_state
	{
		// Token: 0x0400201F RID: 8223
		public const int Tag = 281;
	}

	// Token: 0x0200070F RID: 1807
	public class relife_player
	{
		// Token: 0x04002020 RID: 8224
		public const int Tag = 132;
	}

	// Token: 0x02000710 RID: 1808
	public class req_buy_guild_goods
	{
		// Token: 0x04002021 RID: 8225
		public const int Tag = 172;
	}

	// Token: 0x02000711 RID: 1809
	public class req_change_team_goal
	{
		// Token: 0x04002022 RID: 8226
		public const int Tag = 213;
	}

	// Token: 0x02000712 RID: 1810
	public class req_guild_battle_guess
	{
		// Token: 0x04002023 RID: 8227
		public const int Tag = 292;
	}

	// Token: 0x02000713 RID: 1811
	public class req_guild_battle_info
	{
		// Token: 0x04002024 RID: 8228
		public const int Tag = 285;
	}

	// Token: 0x02000714 RID: 1812
	public class req_guild_battle_member
	{
		// Token: 0x04002025 RID: 8229
		public const int Tag = 288;
	}

	// Token: 0x02000715 RID: 1813
	public class req_guild_battle_rank
	{
		// Token: 0x04002026 RID: 8230
		public const int Tag = 287;
	}

	// Token: 0x02000716 RID: 1814
	public class req_guild_battle_state
	{
		// Token: 0x04002027 RID: 8231
		public const int Tag = 295;
	}

	// Token: 0x02000717 RID: 1815
	public class req_guild_member_info
	{
		// Token: 0x04002028 RID: 8232
		public const int Tag = 170;
	}

	// Token: 0x02000718 RID: 1816
	public class req_guild_notice
	{
		// Token: 0x04002029 RID: 8233
		public const int Tag = 169;
	}

	// Token: 0x02000719 RID: 1817
	public class req_guild_score_info
	{
		// Token: 0x0400202A RID: 8234
		public const int Tag = 291;
	}

	// Token: 0x0200071A RID: 1818
	public class req_guild_skill
	{
		// Token: 0x0400202B RID: 8235
		public const int Tag = 212;
	}

	// Token: 0x0200071B RID: 1819
	public class req_guild_star
	{
		// Token: 0x0400202C RID: 8236
		public const int Tag = 293;
	}

	// Token: 0x0200071C RID: 1820
	public class req_invite_team
	{
		// Token: 0x0400202D RID: 8237
		public const int Tag = 109;
	}

	// Token: 0x0200071D RID: 1821
	public class req_invite_team_result
	{
		// Token: 0x0400202E RID: 8238
		public const int Tag = 517;
	}

	// Token: 0x0200071E RID: 1822
	public class req_join_team
	{
		// Token: 0x0400202F RID: 8239
		public const int Tag = 161;
	}

	// Token: 0x0200071F RID: 1823
	public class req_level_reward
	{
		// Token: 0x04002030 RID: 8240
		public const int Tag = 296;
	}

	// Token: 0x02000720 RID: 1824
	public class req_offline_chat
	{
		// Token: 0x04002031 RID: 8241
		public const int Tag = 168;
	}

	// Token: 0x02000721 RID: 1825
	public class req_open_guild_shop
	{
		// Token: 0x04002032 RID: 8242
		public const int Tag = 171;
	}

	// Token: 0x02000722 RID: 1826
	public class req_other_team
	{
		// Token: 0x04002033 RID: 8243
		public const int Tag = 248;
	}

	// Token: 0x02000723 RID: 1827
	public class req_random_online_character_list
	{
		// Token: 0x04002034 RID: 8244
		public const int Tag = 159;
	}

	// Token: 0x02000724 RID: 1828
	public class req_seting_guild_appro
	{
		// Token: 0x04002035 RID: 8245
		public const int Tag = 173;
	}

	// Token: 0x02000725 RID: 1829
	public class request_activity_info
	{
		// Token: 0x04002036 RID: 8246
		public const int Tag = 225;
	}

	// Token: 0x02000726 RID: 1830
	public class request_bar_fight
	{
		// Token: 0x04002037 RID: 8247
		public const int Tag = 206;
	}

	// Token: 0x02000727 RID: 1831
	public class request_battle_info
	{
		// Token: 0x04002038 RID: 8248
		public const int Tag = 231;
	}

	// Token: 0x02000728 RID: 1832
	public class request_big_pack
	{
		// Token: 0x04002039 RID: 8249
		public const int Tag = 260;
	}

	// Token: 0x02000729 RID: 1833
	public class request_change_pk_mode
	{
		// Token: 0x0400203A RID: 8250
		public const int Tag = 226;
	}

	// Token: 0x0200072A RID: 1834
	public class request_daily_active
	{
		// Token: 0x0400203B RID: 8251
		public const int Tag = 261;
	}

	// Token: 0x0200072B RID: 1835
	public class request_daily_buy
	{
		// Token: 0x0400203C RID: 8252
		public const int Tag = 258;
	}

	// Token: 0x0200072C RID: 1836
	public class request_daily_mission
	{
		// Token: 0x0400203D RID: 8253
		public const int Tag = 121;
	}

	// Token: 0x0200072D RID: 1837
	public class request_dance_info
	{
		// Token: 0x0400203E RID: 8254
		public const int Tag = 227;
	}

	// Token: 0x0200072E RID: 1838
	public class request_dance_state_info
	{
		// Token: 0x0400203F RID: 8255
		public const int Tag = 313;
	}

	// Token: 0x0200072F RID: 1839
	public class request_domin_info
	{
		// Token: 0x04002040 RID: 8256
		public const int Tag = 310;
	}

	// Token: 0x02000730 RID: 1840
	public class request_first_buy
	{
		// Token: 0x04002041 RID: 8257
		public const int Tag = 259;
	}

	// Token: 0x02000731 RID: 1841
	public class request_guild_boss
	{
		// Token: 0x04002042 RID: 8258
		public const int Tag = 195;
	}

	// Token: 0x02000732 RID: 1842
	public class request_guild_map_domine_top
	{
		// Token: 0x04002043 RID: 8259
		public const int Tag = 318;
	}

	// Token: 0x02000733 RID: 1843
	public class request_guild_map_info
	{
		// Token: 0x04002044 RID: 8260
		public const int Tag = 319;
	}

	// Token: 0x02000734 RID: 1844
	public class request_guild_map_reward
	{
		// Token: 0x04002045 RID: 8261
		public const int Tag = 320;
	}

	// Token: 0x02000735 RID: 1845
	public class request_guild_reward
	{
		// Token: 0x04002046 RID: 8262
		public const int Tag = 196;
	}

	// Token: 0x02000736 RID: 1846
	public class request_invest_pack
	{
		// Token: 0x04002047 RID: 8263
		public const int Tag = 257;
	}

	// Token: 0x02000737 RID: 1847
	public class request_level_pack
	{
		// Token: 0x04002048 RID: 8264
		public const int Tag = 256;
	}

	// Token: 0x02000738 RID: 1848
	public class request_line_state
	{
		// Token: 0x04002049 RID: 8265
		public const int Tag = 219;
	}

	// Token: 0x02000739 RID: 1849
	public class request_mount_info
	{
		// Token: 0x0400204A RID: 8266
		public const int Tag = 235;
	}

	// Token: 0x0200073A RID: 1850
	public class request_random_name
	{
		// Token: 0x0400204B RID: 8267
		public const int Tag = 118;
	}

	// Token: 0x0200073B RID: 1851
	public class request_random_rank_pvp_opponent
	{
		// Token: 0x0400204C RID: 8268
		public const int Tag = 133;
	}

	// Token: 0x0200073C RID: 1852
	public class request_rank_pvp_data
	{
		// Token: 0x0400204D RID: 8269
		public const int Tag = 210;
	}

	// Token: 0x0200073D RID: 1853
	public class request_rank_pvp_history
	{
		// Token: 0x0400204E RID: 8270
		public const int Tag = 211;
	}

	// Token: 0x0200073E RID: 1854
	public class request_retrieve
	{
		// Token: 0x0400204F RID: 8271
		public const int Tag = 279;
	}

	// Token: 0x0200073F RID: 1855
	public class request_retrieve_info
	{
		// Token: 0x04002050 RID: 8272
		public const int Tag = 278;
	}

	// Token: 0x02000740 RID: 1856
	public class request_sign_30_day_info
	{
		// Token: 0x04002051 RID: 8273
		public const int Tag = 252;
	}

	// Token: 0x02000741 RID: 1857
	public class request_sign_week_info
	{
		// Token: 0x04002052 RID: 8274
		public const int Tag = 253;
	}

	// Token: 0x02000742 RID: 1858
	public class request_slot_info
	{
		// Token: 0x04002053 RID: 8275
		public const int Tag = 242;
	}

	// Token: 0x02000743 RID: 1859
	public class request_slot_reward
	{
		// Token: 0x04002054 RID: 8276
		public const int Tag = 249;
	}

	// Token: 0x02000744 RID: 1860
	public class request_slot_sum_reward
	{
		// Token: 0x04002055 RID: 8277
		public const int Tag = 244;
	}

	// Token: 0x02000745 RID: 1861
	public class request_special_big_pack
	{
		// Token: 0x04002056 RID: 8278
		public const int Tag = 274;
	}

	// Token: 0x02000746 RID: 1862
	public class request_survive_top
	{
		// Token: 0x04002057 RID: 8279
		public const int Tag = 245;
	}

	// Token: 0x02000747 RID: 1863
	public class request_top_rank_list
	{
		// Token: 0x04002058 RID: 8280
		public const int Tag = 191;
	}

	// Token: 0x02000748 RID: 1864
	public class request_top_rank_pvp_list
	{
		// Token: 0x04002059 RID: 8281
		public const int Tag = 134;
	}

	// Token: 0x02000749 RID: 1865
	public class request_tower_copy_info
	{
		// Token: 0x0400205A RID: 8282
		public const int Tag = 202;
	}

	// Token: 0x0200074A RID: 1866
	public class request_update_friend_useinfo
	{
		// Token: 0x0400205B RID: 8283
		public const int Tag = 126;
	}

	// Token: 0x0200074B RID: 1867
	public class request_update_storagepack
	{
		// Token: 0x0400205C RID: 8284
		public const int Tag = 139;
	}

	// Token: 0x0200074C RID: 1868
	public class request_wild_boss_info
	{
		// Token: 0x0400205D RID: 8285
		public const int Tag = 200;
	}

	// Token: 0x0200074D RID: 1869
	public class require_daily_active_reward
	{
		// Token: 0x0400205E RID: 8286
		public const int Tag = 265;
	}

	// Token: 0x0200074E RID: 1870
	public class require_domin_rewards
	{
		// Token: 0x0400205F RID: 8287
		public const int Tag = 312;
	}

	// Token: 0x0200074F RID: 1871
	public class require_first_buy_reward
	{
		// Token: 0x04002060 RID: 8288
		public const int Tag = 264;
	}

	// Token: 0x02000750 RID: 1872
	public class require_invest_reward
	{
		// Token: 0x04002061 RID: 8289
		public const int Tag = 263;
	}

	// Token: 0x02000751 RID: 1873
	public class require_level_reward
	{
		// Token: 0x04002062 RID: 8290
		public const int Tag = 262;
	}

	// Token: 0x02000752 RID: 1874
	public class require_vip_info
	{
		// Token: 0x04002063 RID: 8291
		public const int Tag = 299;
	}

	// Token: 0x02000753 RID: 1875
	public class require_vip_reward
	{
		// Token: 0x04002064 RID: 8292
		public const int Tag = 300;
	}

	// Token: 0x02000754 RID: 1876
	public class ret_abandon_mission
	{
		// Token: 0x04002065 RID: 8293
		public const int Tag = 522;
	}

	// Token: 0x02000755 RID: 1877
	public class ret_accept_mission
	{
		// Token: 0x04002066 RID: 8294
		public const int Tag = 520;
	}

	// Token: 0x02000756 RID: 1878
	public class ret_add_friend
	{
		// Token: 0x04002067 RID: 8295
		public const int Tag = 533;
	}

	// Token: 0x02000757 RID: 1879
	public class ret_ask_confirm_multi_copy_scene
	{
		// Token: 0x04002068 RID: 8296
		public const int Tag = 217;
	}

	// Token: 0x02000758 RID: 1880
	public class ret_ask_shop_list
	{
		// Token: 0x04002069 RID: 8297
		public const int Tag = 554;
	}

	// Token: 0x02000759 RID: 1881
	public class ret_battle_info
	{
		// Token: 0x0400206A RID: 8298
		public const int Tag = 627;
	}

	// Token: 0x0200075A RID: 1882
	public class ret_buy_car_shop
	{
		// Token: 0x0400206B RID: 8299
		public const int Tag = 691;
	}

	// Token: 0x0200075B RID: 1883
	public class ret_buy_guild_goods
	{
		// Token: 0x0400206C RID: 8300
		public const int Tag = 583;
	}

	// Token: 0x0200075C RID: 1884
	public class ret_buy_invest_pack
	{
		// Token: 0x0400206D RID: 8301
		public const int Tag = 655;
	}

	// Token: 0x0200075D RID: 1885
	public class ret_buy_shop_item
	{
		// Token: 0x0400206E RID: 8302
		public const int Tag = 652;
	}

	// Token: 0x0200075E RID: 1886
	public class ret_chat
	{
		// Token: 0x0400206F RID: 8303
		public const int Tag = 528;
	}

	// Token: 0x0200075F RID: 1887
	public class ret_commercail_reward
	{
		// Token: 0x04002070 RID: 8304
		public const int Tag = 650;
	}

	// Token: 0x02000760 RID: 1888
	public class ret_complete_mission
	{
		// Token: 0x04002071 RID: 8305
		public const int Tag = 521;
	}

	// Token: 0x02000761 RID: 1889
	public class ret_consign_ask_items_info
	{
		// Token: 0x04002072 RID: 8306
		public const int Tag = 596;
	}

	// Token: 0x02000762 RID: 1890
	public class ret_consign_ask_my_items
	{
		// Token: 0x04002073 RID: 8307
		public const int Tag = 595;
	}

	// Token: 0x02000763 RID: 1891
	public class ret_consign_buy_item
	{
		// Token: 0x04002074 RID: 8308
		public const int Tag = 597;
	}

	// Token: 0x02000764 RID: 1892
	public class ret_consign_cancel_sale
	{
		// Token: 0x04002075 RID: 8309
		public const int Tag = 594;
	}

	// Token: 0x02000765 RID: 1893
	public class ret_consign_sale_item
	{
		// Token: 0x04002076 RID: 8310
		public const int Tag = 593;
	}

	// Token: 0x02000766 RID: 1894
	public class ret_del_friend
	{
		// Token: 0x04002077 RID: 8311
		public const int Tag = 535;
	}

	// Token: 0x02000767 RID: 1895
	public class ret_domin_info
	{
		// Token: 0x04002078 RID: 8312
		public const int Tag = 684;
	}

	// Token: 0x02000768 RID: 1896
	public class ret_enter_guild_battle
	{
		// Token: 0x04002079 RID: 8313
		public const int Tag = 677;
	}

	// Token: 0x02000769 RID: 1897
	public class ret_get_team_list
	{
		// Token: 0x0400207A RID: 8314
		public const int Tag = 574;
	}

	// Token: 0x0200076A RID: 1898
	public class ret_grant_tower_reward
	{
		// Token: 0x0400207B RID: 8315
		public const int Tag = 625;
	}

	// Token: 0x0200076B RID: 1899
	public class ret_guild_approve_resverve
	{
		// Token: 0x0400207C RID: 8316
		public const int Tag = 591;
	}

	// Token: 0x0200076C RID: 1900
	public class ret_guild_battle_guess
	{
		// Token: 0x0400207D RID: 8317
		public const int Tag = 669;
	}

	// Token: 0x0200076D RID: 1901
	public class ret_guild_battle_info
	{
		// Token: 0x0400207E RID: 8318
		public const int Tag = 662;
	}

	// Token: 0x0200076E RID: 1902
	public class ret_guild_battle_member
	{
		// Token: 0x0400207F RID: 8319
		public const int Tag = 665;
	}

	// Token: 0x0200076F RID: 1903
	public class ret_guild_battle_rank
	{
		// Token: 0x04002080 RID: 8320
		public const int Tag = 663;
	}

	// Token: 0x02000770 RID: 1904
	public class ret_guild_battle_state
	{
		// Token: 0x04002081 RID: 8321
		public const int Tag = 673;
	}

	// Token: 0x02000771 RID: 1905
	public class ret_guild_create
	{
		// Token: 0x04002082 RID: 8322
		public const int Tag = 567;
	}

	// Token: 0x02000772 RID: 1906
	public class ret_guild_donate
	{
		// Token: 0x04002083 RID: 8323
		public const int Tag = 585;
	}

	// Token: 0x02000773 RID: 1907
	public class ret_guild_job_change
	{
		// Token: 0x04002084 RID: 8324
		public const int Tag = 589;
	}

	// Token: 0x02000774 RID: 1908
	public class ret_guild_join
	{
		// Token: 0x04002085 RID: 8325
		public const int Tag = 566;
	}

	// Token: 0x02000775 RID: 1909
	public class ret_guild_kick
	{
		// Token: 0x04002086 RID: 8326
		public const int Tag = 590;
	}

	// Token: 0x02000776 RID: 1910
	public class ret_guild_leave
	{
		// Token: 0x04002087 RID: 8327
		public const int Tag = 565;
	}

	// Token: 0x02000777 RID: 1911
	public class ret_guild_log
	{
		// Token: 0x04002088 RID: 8328
		public const int Tag = 584;
	}

	// Token: 0x02000778 RID: 1912
	public class ret_guild_map_domine_top
	{
		// Token: 0x04002089 RID: 8329
		public const int Tag = 688;
	}

	// Token: 0x02000779 RID: 1913
	public class ret_guild_map_reward
	{
		// Token: 0x0400208A RID: 8330
		public const int Tag = 690;
	}

	// Token: 0x0200077A RID: 1914
	public class ret_guild_member_info
	{
		// Token: 0x0400208B RID: 8331
		public const int Tag = 581;
	}

	// Token: 0x0200077B RID: 1915
	public class ret_guild_req_info
	{
		// Token: 0x0400208C RID: 8332
		public const int Tag = 563;
	}

	// Token: 0x0200077C RID: 1916
	public class ret_guild_req_list
	{
		// Token: 0x0400208D RID: 8333
		public const int Tag = 562;
	}

	// Token: 0x0200077D RID: 1917
	public class ret_guild_score_info
	{
		// Token: 0x0400208E RID: 8334
		public const int Tag = 667;
	}

	// Token: 0x0200077E RID: 1918
	public class ret_guild_skill_level
	{
		// Token: 0x0400208F RID: 8335
		public const int Tag = 564;
	}

	// Token: 0x0200077F RID: 1919
	public class ret_guild_star
	{
		// Token: 0x04002090 RID: 8336
		public const int Tag = 671;
	}

	// Token: 0x02000780 RID: 1920
	public class ret_invite_join_team
	{
		// Token: 0x04002091 RID: 8337
		public const int Tag = 110;
	}

	// Token: 0x02000781 RID: 1921
	public class ret_level_reward
	{
		// Token: 0x04002092 RID: 8338
		public const int Tag = 674;
	}

	// Token: 0x02000782 RID: 1922
	public class ret_mount_equip
	{
		// Token: 0x04002093 RID: 8339
		public const int Tag = 631;
	}

	// Token: 0x02000783 RID: 1923
	public class ret_mount_info
	{
		// Token: 0x04002094 RID: 8340
		public const int Tag = 630;
	}

	// Token: 0x02000784 RID: 1924
	public class ret_mount_use_color
	{
		// Token: 0x04002095 RID: 8341
		public const int Tag = 632;
	}

	// Token: 0x02000785 RID: 1925
	public class ret_offline_chat
	{
		// Token: 0x04002096 RID: 8342
		public const int Tag = 579;
	}

	// Token: 0x02000786 RID: 1926
	public class ret_open_guild_boss
	{
		// Token: 0x04002097 RID: 8343
		public const int Tag = 628;
	}

	// Token: 0x02000787 RID: 1927
	public class ret_open_guild_shop
	{
		// Token: 0x04002098 RID: 8344
		public const int Tag = 582;
	}

	// Token: 0x02000788 RID: 1928
	public class ret_open_item_package
	{
		// Token: 0x04002099 RID: 8345
		public const int Tag = 617;
	}

	// Token: 0x02000789 RID: 1929
	public class ret_random_online_character_list
	{
		// Token: 0x0400209A RID: 8346
		public const int Tag = 570;
	}

	// Token: 0x0200078A RID: 1930
	public class ret_re_name
	{
		// Token: 0x0400209B RID: 8347
		public const int Tag = 680;
	}

	// Token: 0x0200078B RID: 1931
	public class ret_req_guild_skill
	{
		// Token: 0x0400209C RID: 8348
		public const int Tag = 609;
	}

	// Token: 0x0200078C RID: 1932
	public class ret_request_30_day_info
	{
		// Token: 0x0400209D RID: 8349
		public const int Tag = 640;
	}

	// Token: 0x0200078D RID: 1933
	public class ret_request_activity_info
	{
		// Token: 0x0400209E RID: 8350
		public const int Tag = 619;
	}

	// Token: 0x0200078E RID: 1934
	public class ret_request_big_pack
	{
		// Token: 0x0400209F RID: 8351
		public const int Tag = 648;
	}

	// Token: 0x0200078F RID: 1935
	public class ret_request_daily_active
	{
		// Token: 0x040020A0 RID: 8352
		public const int Tag = 649;
	}

	// Token: 0x02000790 RID: 1936
	public class ret_request_daily_buy
	{
		// Token: 0x040020A1 RID: 8353
		public const int Tag = 646;
	}

	// Token: 0x02000791 RID: 1937
	public class ret_request_dance_info
	{
		// Token: 0x040020A2 RID: 8354
		public const int Tag = 623;
	}

	// Token: 0x02000792 RID: 1938
	public class ret_request_first_buy
	{
		// Token: 0x040020A3 RID: 8355
		public const int Tag = 647;
	}

	// Token: 0x02000793 RID: 1939
	public class ret_request_guild_boss
	{
		// Token: 0x040020A4 RID: 8356
		public const int Tag = 599;
	}

	// Token: 0x02000794 RID: 1940
	public class ret_request_guild_map_info
	{
		// Token: 0x040020A5 RID: 8357
		public const int Tag = 689;
	}

	// Token: 0x02000795 RID: 1941
	public class ret_request_invest_pack
	{
		// Token: 0x040020A6 RID: 8358
		public const int Tag = 645;
	}

	// Token: 0x02000796 RID: 1942
	public class ret_request_level_pack
	{
		// Token: 0x040020A7 RID: 8359
		public const int Tag = 644;
	}

	// Token: 0x02000797 RID: 1943
	public class ret_request_random_rank_pvp_opponent
	{
		// Token: 0x040020A8 RID: 8360
		public const int Tag = 542;
	}

	// Token: 0x02000798 RID: 1944
	public class ret_request_retrieve_info
	{
		// Token: 0x040020A9 RID: 8361
		public const int Tag = 658;
	}

	// Token: 0x02000799 RID: 1945
	public class ret_request_sign_week_info
	{
		// Token: 0x040020AA RID: 8362
		public const int Tag = 641;
	}

	// Token: 0x0200079A RID: 1946
	public class ret_request_survive_top
	{
		// Token: 0x040020AB RID: 8363
		public const int Tag = 636;
	}

	// Token: 0x0200079B RID: 1947
	public class ret_request_top_rank_pvp_list
	{
		// Token: 0x040020AC RID: 8364
		public const int Tag = 543;
	}

	// Token: 0x0200079C RID: 1948
	public class ret_request_tower_copy_info
	{
		// Token: 0x040020AD RID: 8365
		public const int Tag = 606;
	}

	// Token: 0x0200079D RID: 1949
	public class ret_request_update_friend_useinfo
	{
		// Token: 0x040020AE RID: 8366
		public const int Tag = 534;
	}

	// Token: 0x0200079E RID: 1950
	public class ret_request_update_storagepack
	{
		// Token: 0x040020AF RID: 8367
		public const int Tag = 550;
	}

	// Token: 0x0200079F RID: 1951
	public class ret_request_wild_boss_info
	{
		// Token: 0x040020B0 RID: 8368
		public const int Tag = 605;
	}

	// Token: 0x020007A0 RID: 1952
	public class ret_require_vip_info
	{
		// Token: 0x040020B1 RID: 8369
		public const int Tag = 678;
	}

	// Token: 0x020007A1 RID: 1953
	public class ret_require_vip_reward
	{
		// Token: 0x040020B2 RID: 8370
		public const int Tag = 679;
	}

	// Token: 0x020007A2 RID: 1954
	public class ret_search_guild
	{
		// Token: 0x040020B3 RID: 8371
		public const int Tag = 586;
	}

	// Token: 0x020007A3 RID: 1955
	public class ret_search_online_character_by_name
	{
		// Token: 0x040020B4 RID: 8372
		public const int Tag = 571;
	}

	// Token: 0x020007A4 RID: 1956
	public class ret_set_guild_battle_member
	{
		// Token: 0x040020B5 RID: 8373
		public const int Tag = 666;
	}

	// Token: 0x020007A5 RID: 1957
	public class ret_sign_30_day
	{
		// Token: 0x040020B6 RID: 8374
		public const int Tag = 642;
	}

	// Token: 0x020007A6 RID: 1958
	public class ret_sign_week
	{
		// Token: 0x040020B7 RID: 8375
		public const int Tag = 643;
	}

	// Token: 0x020007A7 RID: 1959
	public class ret_skill_use
	{
		// Token: 0x040020B8 RID: 8376
		public const int Tag = 508;
	}

	// Token: 0x020007A8 RID: 1960
	public class ret_slot_info
	{
		// Token: 0x040020B9 RID: 8377
		public const int Tag = 633;
	}

	// Token: 0x020007A9 RID: 1961
	public class ret_slot_sum_reward
	{
		// Token: 0x040020BA RID: 8378
		public const int Tag = 635;
	}

	// Token: 0x020007AA RID: 1962
	public class ret_special_big_pack
	{
		// Token: 0x040020BB RID: 8379
		public const int Tag = 656;
	}

	// Token: 0x020007AB RID: 1963
	public class ret_spin_slot
	{
		// Token: 0x040020BC RID: 8380
		public const int Tag = 634;
	}

	// Token: 0x020007AC RID: 1964
	public class ret_title_req_level_up
	{
		// Token: 0x040020BD RID: 8381
		public const int Tag = 569;
	}

	// Token: 0x020007AD RID: 1965
	public class ret_top_rank_list
	{
		// Token: 0x040020BE RID: 8382
		public const int Tag = 598;
	}

	// Token: 0x020007AE RID: 1966
	public class ret_tower_reset
	{
		// Token: 0x040020BF RID: 8383
		public const int Tag = 626;
	}

	// Token: 0x020007AF RID: 1967
	public class ret_tower_wipe_out
	{
		// Token: 0x040020C0 RID: 8384
		public const int Tag = 607;
	}

	// Token: 0x020007B0 RID: 1968
	public class ret_update_guild_star
	{
		// Token: 0x040020C1 RID: 8385
		public const int Tag = 672;
	}

	// Token: 0x020007B1 RID: 1969
	public class ret_use_item
	{
		// Token: 0x040020C2 RID: 8386
		public const int Tag = 526;
	}

	// Token: 0x020007B2 RID: 1970
	public class ret_watch_video_info
	{
		// Token: 0x040020C3 RID: 8387
		public const int Tag = 693;
	}

	// Token: 0x020007B3 RID: 1971
	public class retrieve_account
	{
		// Token: 0x040020C4 RID: 8388
		public const int Tag = 660;
	}

	// Token: 0x020007B4 RID: 1972
	public class sample_activity_result
	{
		// Token: 0x040020C5 RID: 8389
		public const int Tag = 685;
	}

	// Token: 0x020007B5 RID: 1973
	public class sample_copy_result
	{
		// Token: 0x040020C6 RID: 8390
		public const int Tag = 613;
	}

	// Token: 0x020007B6 RID: 1974
	public class search_guild
	{
		// Token: 0x040020C7 RID: 8391
		public const int Tag = 176;
	}

	// Token: 0x020007B7 RID: 1975
	public class search_online_character_by_name
	{
		// Token: 0x040020C8 RID: 8392
		public const int Tag = 160;
	}

	// Token: 0x020007B8 RID: 1976
	public class select_pk_character
	{
		// Token: 0x040020C9 RID: 8393
		public const int Tag = 135;
	}

	// Token: 0x020007B9 RID: 1977
	public class sell_item
	{
		// Token: 0x040020CA RID: 8394
		public const int Tag = 129;
	}

	// Token: 0x020007BA RID: 1978
	public class send_daily_mission
	{
		// Token: 0x040020CB RID: 8395
		public const int Tag = 530;
	}

	// Token: 0x020007BB RID: 1979
	public class send_dialog_notify
	{
		// Token: 0x040020CC RID: 8396
		public const int Tag = 653;
	}

	// Token: 0x020007BC RID: 1980
	public class send_escort_info
	{
		// Token: 0x040020CD RID: 8397
		public const int Tag = 621;
	}

	// Token: 0x020007BD RID: 1981
	public class send_mail
	{
		// Token: 0x040020CE RID: 8398
		public const int Tag = 122;
	}

	// Token: 0x020007BE RID: 1982
	public class send_mail_box
	{
		// Token: 0x040020CF RID: 8399
		public const int Tag = 284;
	}

	// Token: 0x020007BF RID: 1983
	public class set_guild_battle_member
	{
		// Token: 0x040020D0 RID: 8400
		public const int Tag = 289;
	}

	// Token: 0x020007C0 RID: 1984
	public class set_mission_param
	{
		// Token: 0x040020D1 RID: 8401
		public const int Tag = 524;
	}

	// Token: 0x020007C1 RID: 1985
	public class set_mission_state
	{
		// Token: 0x040020D2 RID: 8402
		public const int Tag = 523;
	}

	// Token: 0x020007C2 RID: 1986
	public class show_damage_board
	{
		// Token: 0x040020D3 RID: 8403
		public const int Tag = 511;
	}

	// Token: 0x020007C3 RID: 1987
	public class show_player_damage_board
	{
		// Token: 0x040020D4 RID: 8404
		public const int Tag = 687;
	}

	// Token: 0x020007C4 RID: 1988
	public class show_reward_items_tips
	{
		// Token: 0x040020D5 RID: 8405
		public const int Tag = 638;
	}

	// Token: 0x020007C5 RID: 1989
	public class sign_30_day
	{
		// Token: 0x040020D6 RID: 8406
		public const int Tag = 254;
	}

	// Token: 0x020007C6 RID: 1990
	public class sign_bar_fight
	{
		// Token: 0x040020D7 RID: 8407
		public const int Tag = 282;
	}

	// Token: 0x020007C7 RID: 1991
	public class sign_week
	{
		// Token: 0x040020D8 RID: 8408
		public const int Tag = 255;
	}

	// Token: 0x020007C8 RID: 1992
	public class single_copy_scene_npc_die
	{
		// Token: 0x040020D9 RID: 8409
		public const int Tag = 127;
	}

	// Token: 0x020007C9 RID: 1993
	public class skill_level_up
	{
		// Token: 0x040020DA RID: 8410
		public const int Tag = 130;
	}

	// Token: 0x020007CA RID: 1994
	public class skill_use
	{
		// Token: 0x040020DB RID: 8411
		public const int Tag = 102;
	}

	// Token: 0x020007CB RID: 1995
	public class spin_slot
	{
		// Token: 0x040020DC RID: 8412
		public const int Tag = 243;
	}

	// Token: 0x020007CC RID: 1996
	public class start_battle
	{
		// Token: 0x040020DD RID: 8413
		public const int Tag = 220;
	}

	// Token: 0x020007CD RID: 1997
	public class start_download
	{
		// Token: 0x040020DE RID: 8414
		public const int Tag = 269;
	}

	// Token: 0x020007CE RID: 1998
	public class start_enter_game
	{
		// Token: 0x040020DF RID: 8415
		public const int Tag = 654;
	}

	// Token: 0x020007CF RID: 1999
	public class start_participate_dance
	{
		// Token: 0x040020E0 RID: 8416
		public const int Tag = 624;
	}

	// Token: 0x020007D0 RID: 2000
	public class stop_leave_copy
	{
		// Token: 0x040020E1 RID: 8417
		public const int Tag = 250;
	}

	// Token: 0x020007D1 RID: 2001
	public class stop_random_select_team
	{
		// Token: 0x040020E2 RID: 8418
		public const int Tag = 216;
	}

	// Token: 0x020007D2 RID: 2002
	public class survive_battle_finish
	{
		// Token: 0x040020E3 RID: 8419
		public const int Tag = 637;
	}

	// Token: 0x020007D3 RID: 2003
	public class syn_friend_info
	{
		// Token: 0x040020E4 RID: 8420
		public const int Tag = 538;
	}

	// Token: 0x020007D4 RID: 2004
	public class syn_rank_pvp_data
	{
		// Token: 0x040020E5 RID: 8421
		public const int Tag = 541;
	}

	// Token: 0x020007D5 RID: 2005
	public class sync_backpack_item
	{
		// Token: 0x040020E6 RID: 8422
		public const int Tag = 592;
	}

	// Token: 0x020007D6 RID: 2006
	public class sync_badgepack_item
	{
		// Token: 0x040020E7 RID: 8423
		public const int Tag = 604;
	}

	// Token: 0x020007D7 RID: 2007
	public class sync_common_data
	{
		// Token: 0x040020E8 RID: 8424
		public const int Tag = 614;
	}

	// Token: 0x020007D8 RID: 2008
	public class sync_copyscenes_info
	{
		// Token: 0x040020E9 RID: 8425
		public const int Tag = 555;
	}

	// Token: 0x020007D9 RID: 2009
	public class sync_dance_state_info
	{
		// Token: 0x040020EA RID: 8426
		public const int Tag = 686;
	}

	// Token: 0x020007DA RID: 2010
	public class sync_fashion_backpack_item
	{
		// Token: 0x040020EB RID: 8427
		public const int Tag = 616;
	}

	// Token: 0x020007DB RID: 2011
	public class sync_guild_new_member
	{
		// Token: 0x040020EC RID: 8428
		public const int Tag = 580;
	}

	// Token: 0x020007DC RID: 2012
	public class sync_item_pack
	{
		// Token: 0x040020ED RID: 8429
		public const int Tag = 611;
	}

	// Token: 0x020007DD RID: 2013
	public class sync_mission
	{
		// Token: 0x040020EE RID: 8430
		public const int Tag = 519;
	}

	// Token: 0x020007DE RID: 2014
	public class sync_random_team_state
	{
		// Token: 0x040020EF RID: 8431
		public const int Tag = 681;
	}

	// Token: 0x020007DF RID: 2015
	public class sync_skill_info
	{
		// Token: 0x040020F0 RID: 8432
		public const int Tag = 540;
	}

	// Token: 0x020007E0 RID: 2016
	public class sync_watch_video_info
	{
		// Token: 0x040020F1 RID: 8433
		public const int Tag = 692;
	}

	// Token: 0x020007E1 RID: 2017
	public class take_item_storagepack
	{
		// Token: 0x040020F2 RID: 8434
		public const int Tag = 141;
	}

	// Token: 0x020007E2 RID: 2018
	public class team_kick
	{
		// Token: 0x040020F3 RID: 8435
		public const int Tag = 164;
	}

	// Token: 0x020007E3 RID: 2019
	public class tianti_req_win_count_rewards
	{
		// Token: 0x040020F4 RID: 8436
		public const int Tag = 157;
	}

	// Token: 0x020007E4 RID: 2020
	public class tiantti_result
	{
		// Token: 0x040020F5 RID: 8437
		public const int Tag = 551;
	}

	// Token: 0x020007E5 RID: 2021
	public class title_req_level_up
	{
		// Token: 0x040020F6 RID: 8438
		public const int Tag = 156;
	}

	// Token: 0x020007E6 RID: 2022
	public class tower_reset
	{
		// Token: 0x040020F7 RID: 8439
		public const int Tag = 230;
	}

	// Token: 0x020007E7 RID: 2023
	public class tower_wipe_out
	{
		// Token: 0x040020F8 RID: 8440
		public const int Tag = 208;
	}

	// Token: 0x020007E8 RID: 2024
	public class tutorial_finish
	{
		// Token: 0x040020F9 RID: 8441
		public const int Tag = 306;
	}

	// Token: 0x020007E9 RID: 2025
	public class unequip_badge
	{
		// Token: 0x040020FA RID: 8442
		public const int Tag = 198;
	}

	// Token: 0x020007EA RID: 2026
	public class unequip_fashion_item
	{
		// Token: 0x040020FB RID: 8443
		public const int Tag = 222;
	}

	// Token: 0x020007EB RID: 2027
	public class unequip_item
	{
		// Token: 0x040020FC RID: 8444
		public const int Tag = 117;
	}

	// Token: 0x020007EC RID: 2028
	public class unlock_function_complete
	{
		// Token: 0x040020FD RID: 8445
		public const int Tag = 268;
	}

	// Token: 0x020007ED RID: 2029
	public class unuse_mount
	{
		// Token: 0x040020FE RID: 8446
		public const int Tag = 239;
	}

	// Token: 0x020007EE RID: 2030
	public class update_client_state
	{
		// Token: 0x040020FF RID: 8447
		public const int Tag = 280;
	}

	// Token: 0x020007EF RID: 2031
	public class update_copyscene_info
	{
		// Token: 0x04002100 RID: 8448
		public const int Tag = 561;
	}

	// Token: 0x020007F0 RID: 2032
	public class update_game_server
	{
		// Token: 0x04002101 RID: 8449
		public const int Tag = 7;
	}

	// Token: 0x020007F1 RID: 2033
	public class update_guild_dance_time
	{
		// Token: 0x04002102 RID: 8450
		public const int Tag = 315;
	}

	// Token: 0x020007F2 RID: 2034
	public class update_guild_star
	{
		// Token: 0x04002103 RID: 8451
		public const int Tag = 294;
	}

	// Token: 0x020007F3 RID: 2035
	public class update_item
	{
		// Token: 0x04002104 RID: 8452
		public const int Tag = 525;
	}

	// Token: 0x020007F4 RID: 2036
	public class update_line_state
	{
		// Token: 0x04002105 RID: 8453
		public const int Tag = 568;
	}

	// Token: 0x020007F5 RID: 2037
	public class update_misison_complete
	{
		// Token: 0x04002106 RID: 8454
		public const int Tag = 182;
	}

	// Token: 0x020007F6 RID: 2038
	public class update_misison_parm
	{
		// Token: 0x04002107 RID: 8455
		public const int Tag = 178;
	}

	// Token: 0x020007F7 RID: 2039
	public class update_player_map_info
	{
		// Token: 0x04002108 RID: 8456
		public const int Tag = 324;
	}

	// Token: 0x020007F8 RID: 2040
	public class update_queue_rank
	{
		// Token: 0x04002109 RID: 8457
		public const int Tag = 577;
	}

	// Token: 0x020007F9 RID: 2041
	public class update_sex_mini_score
	{
		// Token: 0x0400210A RID: 8458
		public const int Tag = 277;
	}

	// Token: 0x020007FA RID: 2042
	public class update_team
	{
		// Token: 0x0400210B RID: 8459
		public const int Tag = 518;
	}

	// Token: 0x020007FB RID: 2043
	public class update_team_member
	{
		// Token: 0x0400210C RID: 8460
		public const int Tag = 575;
	}

	// Token: 0x020007FC RID: 2044
	public class update_team_setting
	{
		// Token: 0x0400210D RID: 8461
		public const int Tag = 163;
	}

	// Token: 0x020007FD RID: 2045
	public class urge_team_leader
	{
		// Token: 0x0400210E RID: 8462
		public const int Tag = 450;
	}

	// Token: 0x020007FE RID: 2046
	public class use_dance
	{
		// Token: 0x0400210F RID: 8463
		public const int Tag = 229;
	}

	// Token: 0x020007FF RID: 2047
	public class use_dance_sound_box
	{
		// Token: 0x04002110 RID: 8464
		public const int Tag = 314;
	}

	// Token: 0x02000800 RID: 2048
	public class use_item
	{
		// Token: 0x04002111 RID: 8465
		public const int Tag = 115;
	}

	// Token: 0x02000801 RID: 2049
	public class use_mount
	{
		// Token: 0x04002112 RID: 8466
		public const int Tag = 238;
	}

	// Token: 0x02000802 RID: 2050
	public class use_skill_buff
	{
		// Token: 0x04002113 RID: 8467
		public const int Tag = 209;
	}

	// Token: 0x02000803 RID: 2051
	public class verfiy
	{
		// Token: 0x04002114 RID: 8468
		public const int Tag = 3;
	}

	// Token: 0x02000804 RID: 2052
	public class visitor
	{
		// Token: 0x04002115 RID: 8469
		public const int Tag = 2;
	}

	// Token: 0x02000805 RID: 2053
	public class watch_video_info
	{
		// Token: 0x04002116 RID: 8470
		public const int Tag = 452;
	}

	// Token: 0x02000806 RID: 2054
	public class weapon_inhert
	{
		// Token: 0x04002117 RID: 8471
		public const int Tag = 305;
	}
}
