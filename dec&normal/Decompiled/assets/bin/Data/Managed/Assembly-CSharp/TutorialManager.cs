using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class TutorialManager
{
	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000854 RID: 2132 RVA: 0x0003B6A8 File Offset: 0x000398A8
	public static TUTORIAL_STEP CurStep
	{
		get
		{
			return TutorialManager.mCurStep;
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0003B6B0 File Offset: 0x000398B0
	public static void ClearTutorialEvent(TUTORIAL_STEP step)
	{
		switch (step)
		{
		case TUTORIAL_STEP.SKILL_DRAG_START:
			break;
		default:
			if (step != TUTORIAL_STEP.CAR_COPY_START && step != TUTORIAL_STEP.EXP_COPY_START && step != TUTORIAL_STEP.GOLD_COPY_START && step != TUTORIAL_STEP.TOWER_START && step != TUTORIAL_STEP.SCUFFLE_COPY_START && step != TUTORIAL_STEP.RANK_PVP_START && step != TUTORIAL_STEP.TITLE_START && step != TUTORIAL_STEP.BADGE_START && step != TUTORIAL_STEP.WORLD_BOSS_START && step != TUTORIAL_STEP.EQUIP_COPY_START && step != TUTORIAL_STEP.BAR_FIGHT_START && step != TUTORIAL_STEP.ESCORT_START && step != TUTORIAL_STEP.ROBBORY_START && step != TUTORIAL_STEP.SURVIVAL_BATTLE_START && step != TUTORIAL_STEP.GUILD_BOSS_START && step != TUTORIAL_STEP.GUILD_START && step != TUTORIAL_STEP.STRENGTH_STAR_START && step != TUTORIAL_STEP.CAPTURE_START)
			{
				return;
			}
			break;
		case TUTORIAL_STEP.CREATE_TEAM_START:
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.ClearTutorialEvent();
			}
			return;
		case TUTORIAL_STEP.CREATE_TEAM_CREATE:
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.TeamTipRoot.ClearTutorialEvent();
			}
			return;
		case TUTORIAL_STEP.CREATE_TEAM_CREATE_BTN:
			if (SingletonUnity<CreateTeamRootLogic>.Exists)
			{
				SingletonUnity<CreateTeamRootLogic>.Instance.ClearTutorialEvent();
			}
			return;
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.ClearTutorialEvent();
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0003B7FC File Offset: 0x000399FC
	private static void InitTutorialStep()
	{
		if (TutorialManager.mTutorialStepDic.Count != 0)
		{
			return;
		}
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.INVALID, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MOVE_SCREEN, TUTORIAL_STEP.JOYSTICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.JOYSTICK_START, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CLICK_NPC, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CLICK_NPC_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.NORMAL_ATTACK_BUTTON, TUTORIAL_STEP.NORMAL_ATTACK_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.NORMAL_ATTACK_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MOVETO_CAR_POINT, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL1_BUTTON, TUTORIAL_STEP.SKILL1_BTN_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL1_BTN_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL2_BUTTON, TUTORIAL_STEP.SKILL2_BTN_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL2_BTN_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL3_BUTTON, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_START_OPEN_MENUROOT, TUTORIAL_STEP.EQUIP_OPEN_BACKPACK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_OPEN_BACKPACK, TUTORIAL_STEP.EQUIP_PRESS_EQUIPMENT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_PRESS_EQUIPMENT, TUTORIAL_STEP.EQUIP_PRESS_PUTONBTN);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_PRESS_PUTONBTN, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_START_OPEN_MENU, TUTORIAL_STEP.COPY_OPEN_COPY_MENU);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_OPEN_COPY_MENU, TUTORIAL_STEP.COPY_OPEN_NORMAL_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_OPEN_NORMAL_COPY, TUTORIAL_STEP.COPY_PRESS_MISSION);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_PRESS_MISSION, TUTORIAL_STEP.COPY_PRESS_PLAY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_PRESS_PLAY, TUTORIAL_STEP.COPY_CHANGE_SCENE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_CHANGE_SCENE, TUTORIAL_STEP.COPY_BEGIN_TIP);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.COPY_BEGIN_TIP, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_START, TUTORIAL_STEP.MAIN_MISSION_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_PHONE_START, TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION_TAB, TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION, TUTORIAL_STEP.MAIN_MISSION_PHONE_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_PHONE_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_TIP_START, TUTORIAL_STEP.MAIN_MISSION_TIP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_TIP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_UPGRADE_START, TUTORIAL_STEP.SKILL_UPGRADE_CLICK_SKILL);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_UPGRADE_CLICK_SKILL, TUTORIAL_STEP.SKILL_UPGRADE_CLICK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_UPGRADE_CLICK, TUTORIAL_STEP.SKILL_UPGRADE_CLICK_ALL);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_UPGRADE_CLICK_ALL, TUTORIAL_STEP.SKILL_UPGRADE_EXIT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_UPGRADE_EXIT, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_START, TUTORIAL_STEP.ENHANCE_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_CLICK_TAB, TUTORIAL_STEP.ENHANCE_CLICK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_CLICK, TUTORIAL_STEP.ENHANCE_CLICK_ALL);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_CLICK_ALL, TUTORIAL_STEP.ENHANCE_EXIT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_EXIT, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_ALL_START, TUTORIAL_STEP.ENHANCE_ALL_CLICK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_ALL_CLICK, TUTORIAL_STEP.ENHANCE_ALL_EXIT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ENHANCE_ALL_EXIT, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_START, TUTORIAL_STEP.CAR_CLICK_ACQUIRE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CLICK_ACQUIRE, TUTORIAL_STEP.CAR_CLICK_GET);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CLICK_GET, TUTORIAL_STEP.CAR_CLICK_OK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CLICK_OK, TUTORIAL_STEP.CAR_CLICK_EQUIP);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CLICK_EQUIP, TUTORIAL_STEP.CAR_CLICK_BACK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CLICK_BACK, TUTORIAL_STEP.CAR_CLICK_MOUNT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CLICK_MOUNT, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_COPY_START, TUTORIAL_STEP.CAR_COPY_CLICK_DAILY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_COPY_CLICK_DAILY, TUTORIAL_STEP.CAR_COPY_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_COPY_WAIT_DATA, TUTORIAL_STEP.CAR_COPY_CLICK_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_COPY_CLICK_COPY, TUTORIAL_STEP.CAR_COPY_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_COPY_CLICK_START, TUTORIAL_STEP.CAR_COPY_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_COPY_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_SPECIAL_TIPS, TUTORIAL_STEP.TOWER_SPECIAL_TIPS_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_SPECIAL_TIPS_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_DRAG_START, TUTORIAL_STEP.SKILL_DRAG_MOVE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_DRAG_MOVE, TUTORIAL_STEP.SKILL_DRAG_EXIT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SKILL_DRAG_EXIT, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_START, TUTORIAL_STEP.CREATE_TEAM_CREATE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_CREATE, TUTORIAL_STEP.CREATE_TEAM_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_CHOOSE_COPY, TUTORIAL_STEP.CREATE_TEAM_CREATE_BTN);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_CREATE_BTN, TUTORIAL_STEP.CREATE_TEAM_WAIT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_WAIT, TUTORIAL_STEP.CREATE_TEAM_SHOW_INVITE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_SHOW_INVITE, TUTORIAL_STEP.CREATE_TEAM_SHOW_SHOUT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_SHOW_SHOUT, TUTORIAL_STEP.CREATE_TEAM_SHOW_APPLICATION);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_SHOW_APPLICATION, TUTORIAL_STEP.CREATE_TEAM_SHOW_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_SHOW_START, TUTORIAL_STEP.CREATE_TEAM_SHOW_LEAVE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_SHOW_LEAVE, TUTORIAL_STEP.CREATE_TEAM_SHOW_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CREATE_TEAM_SHOW_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EXP_COPY_START, TUTORIAL_STEP.EXP_COPY_CLICK_DAILY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EXP_COPY_CLICK_DAILY, TUTORIAL_STEP.EXP_COPY_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EXP_COPY_WAIT_DATA, TUTORIAL_STEP.EXP_COPY_CLICK_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EXP_COPY_CLICK_COPY, TUTORIAL_STEP.EXP_COPY_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EXP_COPY_CLICK_START, TUTORIAL_STEP.EXP_COPY_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EXP_COPY_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GOLD_COPY_START, TUTORIAL_STEP.GOLD_COPY_CLICK_DAILY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GOLD_COPY_CLICK_DAILY, TUTORIAL_STEP.GOLD_COPY_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GOLD_COPY_WAIT_DATA, TUTORIAL_STEP.GOLD_COPY_CLICK_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GOLD_COPY_CLICK_COPY, TUTORIAL_STEP.GOLD_COPY_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GOLD_COPY_CLICK_START, TUTORIAL_STEP.GOLD_COPY_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GOLD_COPY_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_START, TUTORIAL_STEP.TOWER_CLICK_DAILY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_CLICK_DAILY, TUTORIAL_STEP.TOWER_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_WAIT_DATA, TUTORIAL_STEP.TOWER_CLICK_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_CLICK_COPY, TUTORIAL_STEP.TOWER_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_CLICK_START, TUTORIAL_STEP.TOWER_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TOWER_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SCUFFLE_COPY_START, TUTORIAL_STEP.SCUFFLE_COPY_CLICK_DAILY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SCUFFLE_COPY_CLICK_DAILY, TUTORIAL_STEP.SCUFFLE_COPY_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SCUFFLE_COPY_WAIT_DATA, TUTORIAL_STEP.SCUFFLE_COPY_CLICK_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SCUFFLE_COPY_CLICK_COPY, TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START, TUTORIAL_STEP.SCUFFLE_COPY_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SCUFFLE_COPY_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.RANK_PVP_START, TUTORIAL_STEP.RANK_PVP_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.RANK_PVP_CLICK_TAB, TUTORIAL_STEP.RANK_PVP_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.RANK_PVP_WAIT_DATA, TUTORIAL_STEP.RANK_PVP_CHOOSE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.RANK_PVP_CHOOSE, TUTORIAL_STEP.RANK_PVP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.RANK_PVP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TITLE_START, TUTORIAL_STEP.TITLE_CLICK_TAP);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TITLE_CLICK_TAP, TUTORIAL_STEP.TITLE_CLICK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TITLE_CLICK, TUTORIAL_STEP.TITLE_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.TITLE_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BADGE_START, TUTORIAL_STEP.BADGE_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BADGE_CLICK_TAB, TUTORIAL_STEP.BADGE_CLICK_ITEM);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BADGE_CLICK_ITEM, TUTORIAL_STEP.BADGE_CLICK_EQUIP);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BADGE_CLICK_EQUIP, TUTORIAL_STEP.BADGE_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BADGE_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.WORLD_BOSS_START, TUTORIAL_STEP.WORLD_BOSS_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.WORLD_BOSS_CLICK_TAB, TUTORIAL_STEP.WORLD_BOSS_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.WORLD_BOSS_WAIT_DATA, TUTORIAL_STEP.WORLD_BOSS_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.WORLD_BOSS_CHOOSE_COPY, TUTORIAL_STEP.WORLD_BOSS_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.WORLD_BOSS_CLICK_START, TUTORIAL_STEP.WORLD_BOSS_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.WORLD_BOSS_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_COPY_START, TUTORIAL_STEP.EQUIP_COPY_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_COPY_CLICK_TAB, TUTORIAL_STEP.EQUIP_COPY_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_COPY_WAIT_DATA, TUTORIAL_STEP.EQUIP_COPY_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_COPY_CHOOSE_COPY, TUTORIAL_STEP.EQUIP_COPY_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_COPY_CLICK_START, TUTORIAL_STEP.EQUIP_COPY_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.EQUIP_COPY_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SELL_ITEM_START, TUTORIAL_STEP.SELL_ITEM_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SELL_ITEM_CLICK_TAB, TUTORIAL_STEP.SELL_ITEM_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SELL_ITEM_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BAR_FIGHT_START, TUTORIAL_STEP.BAR_FIGHT_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BAR_FIGHT_CLICK_TAB, TUTORIAL_STEP.BAR_FIGHT_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BAR_FIGHT_WAIT_DATA, TUTORIAL_STEP.BAR_FIGHT_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BAR_FIGHT_CHOOSE_COPY, TUTORIAL_STEP.BAR_FIGHT_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BAR_FIGHT_CLICK_START, TUTORIAL_STEP.BAR_FIGHT_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BAR_FIGHT_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ESCORT_START, TUTORIAL_STEP.ESCORT_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ESCORT_CLICK_TAB, TUTORIAL_STEP.ESCORT_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ESCORT_WAIT_DATA, TUTORIAL_STEP.ESCORT_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ESCORT_CHOOSE_COPY, TUTORIAL_STEP.ESCORT_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ESCORT_CLICK_START, TUTORIAL_STEP.ESCORT_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ESCORT_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROBBORY_START, TUTORIAL_STEP.ROBBORY_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROBBORY_CLICK_TAB, TUTORIAL_STEP.ROBBORY_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROBBORY_WAIT_DATA, TUTORIAL_STEP.ROBBORY_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROBBORY_CHOOSE_COPY, TUTORIAL_STEP.ROBBORY_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROBBORY_CLICK_START, TUTORIAL_STEP.ROBBORY_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROBBORY_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SURVIVAL_BATTLE_START, TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_TAB, TUTORIAL_STEP.SURVIVAL_BATTLE_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SURVIVAL_BATTLE_WAIT_DATA, TUTORIAL_STEP.SURVIVAL_BATTLE_CHOOSE_COPY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SURVIVAL_BATTLE_CHOOSE_COPY, TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START, TUTORIAL_STEP.SURVIVAL_BATTLE_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SURVIVAL_BATTLE_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_BOSS_START, TUTORIAL_STEP.GUILD_BOSS_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_BOSS_CLICK_TAB, TUTORIAL_STEP.GUILD_BOSS_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_BOSS_WAIT_DATA, TUTORIAL_STEP.GUILD_BOSS_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_BOSS_CLICK_START, TUTORIAL_STEP.GUILD_BOSS_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_BOSS_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_START, TUTORIAL_STEP.GUILD_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_WAIT_DATA, TUTORIAL_STEP.GUILD_LIST);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_LIST, TUTORIAL_STEP.GUILD_CREATE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_CREATE, TUTORIAL_STEP.GUILD_NAME);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_NAME, TUTORIAL_STEP.GUILD_COST);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_COST, TUTORIAL_STEP.GUILD_INFO);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_INFO, TUTORIAL_STEP.GUILD_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.GUILD_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.STRENGTH_STAR_START, TUTORIAL_STEP.STRENGTH_STAR_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.STRENGTH_STAR_CLICK_TAB, TUTORIAL_STEP.STRENGTH_STAR_CLICK_BTN);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.STRENGTH_STAR_CLICK_BTN, TUTORIAL_STEP.STRENGTH_STAR_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.STRENGTH_STAR_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SLOT_START, TUTORIAL_STEP.SLOT_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SLOT_WAIT_DATA, TUTORIAL_STEP.SLOT_CLICK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SLOT_CLICK, TUTORIAL_STEP.SLOAT_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SLOAT_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_CLICK_START, TUTORIAL_STEP.MAIN_MISSION_CLICK_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.MAIN_MISSION_CLICK_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.DAILY_MISSION_CLICK_START, TUTORIAL_STEP.DAILY_MISSION_CLICK_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.DAILY_MISSION_CLICK_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SIDE_MISSION_CLICK_START, TUTORIAL_STEP.SIDE_MISSION_CLICK_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SIDE_MISSION_CLICK_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CLICK_STRONGER_START, TUTORIAL_STEP.CLICK_STRONGER_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CLICK_STRONGER_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.DRUG_TIP_START, TUTORIAL_STEP.DRUG_TIP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.DRUG_TIP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.DRUG_USE_TIP_START, TUTORIAL_STEP.DRUG_USE_TIP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.DRUG_USE_TIP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_ACCBTN_START, TUTORIAL_STEP.CAR_CTL_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CTL_START, TUTORIAL_STEP.CAR_CTL_BRAKE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CTL_BRAKE, TUTORIAL_STEP.CAR_CTL_TARGET);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CTL_TARGET, TUTORIAL_STEP.CAR_CTL_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAR_CTL_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROB_CAR_START, TUTORIAL_STEP.ROB_CAR_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.ROB_CAR_FINISH, TUTORIAL_STEP.CAR_ACCBTN_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.AUTO_FIGHT_CLICK, TUTORIAL_STEP.AUTO_FIGHT_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.AUTO_FIGHT_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SIDE_MISSION_CAR_TIP_START, TUTORIAL_STEP.SIDE_MISSION_CAR_TIP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.SIDE_MISSION_CAR_TIP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.PVP_TIP_START, TUTORIAL_STEP.PVP_TIP_POINT_PVP);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.PVP_TIP_POINT_PVP, TUTORIAL_STEP.PVP_TIP_POINT_PVE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.PVP_TIP_POINT_PVE, TUTORIAL_STEP.PVP_TIP_SHOW_WORD1);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.PVP_TIP_SHOW_WORD1, TUTORIAL_STEP.PVP_TIP_SHOW_WORD2);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.PVP_TIP_SHOW_WORD2, TUTORIAL_STEP.PVP_TIP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.PVP_TIP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_START, TUTORIAL_STEP.BUY_BADGE_WAIT);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_WAIT, TUTORIAL_STEP.BUY_BADGE_CHOOSE_BADGE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_CHOOSE_BADGE, TUTORIAL_STEP.BUY_BADGE_CLICK_BUY);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_CLICK_BUY, TUTORIAL_STEP.BUY_BADGE_WAIT_OPEN_BOX);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_WAIT_OPEN_BOX, TUTORIAL_STEP.BUY_BADGE_CLICK_CONFIRM);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_CLICK_CONFIRM, TUTORIAL_STEP.BUY_BADGE_CLICK_BACK);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.BUY_BADGE_CLICK_BACK, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.JOIN_TEAM_START, TUTORIAL_STEP.JOIN_TEAM_SHOW_1);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.JOIN_TEAM_SHOW_1, TUTORIAL_STEP.JOIN_TEAM_CLICK_URGE);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.JOIN_TEAM_CLICK_URGE, TUTORIAL_STEP.JOIN_TEAM_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.JOIN_TEAM_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.NEW_MAP_TIP_START, TUTORIAL_STEP.NEW_MAP_TIP_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.NEW_MAP_TIP_FINISH, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.FUNCTION_TIP_START, TUTORIAL_STEP.INVALID);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAPTURE_START, TUTORIAL_STEP.CAPTURE_CLICK_TAB);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAPTURE_CLICK_TAB, TUTORIAL_STEP.CAPTURE_WAIT_DATA);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAPTURE_WAIT_DATA, TUTORIAL_STEP.CAPTURE_CLICK_ITEM);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAPTURE_CLICK_ITEM, TUTORIAL_STEP.CAPTURE_CLICK_START);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAPTURE_CLICK_START, TUTORIAL_STEP.CAPTURE_FINISH);
		TutorialManager.mTutorialStepDic.Add(TUTORIAL_STEP.CAPTURE_FINISH, TUTORIAL_STEP.INVALID);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0003C600 File Offset: 0x0003A800
	public static void ShowTutorial(TUTORIAL_STEP curStep)
	{
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		TutorialManager.InitTutorialStep();
		if (TutorialManager.mCurStep != TUTORIAL_STEP.INVALID && curStep == TUTORIAL_STEP.INVALID)
		{
			TutorialManager.mCurStep = curStep;
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.ContinueAutoMoveToMission();
			return;
		}
		TutorialManager.mCurStep = curStep;
		if (TutorialManager.mCurStep != TUTORIAL_STEP.INVALID && TutorialManager.mCurStep != TUTORIAL_STEP.NEW_MAP_TIP_START)
		{
			if (SingletonUnity<JoyStickLogic>.Exists)
			{
				SingletonUnity<JoyStickLogic>.Instance.MoveOutScreen();
			}
			if (!(Singleton<ObjManager>.Instance.MainPlayer != null))
			{
				return;
			}
			Singleton<ObjManager>.Instance.MainPlayer.DisactiveTargetArriveFinish();
			Singleton<ObjManager>.Instance.MainPlayer.StopMove();
			SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath.IsAutoMovingFlag = false;
		}
		if (TutorialManager.mCurStep == TUTORIAL_STEP.JOYSTICK_START)
		{
			TutorialManager.Show_JOYSTICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CLICK_NPC)
		{
			TutorialManager.Show_CLICK_NPC();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.MAIN_MISSION_TIP_START)
		{
			TutorialManager.Show_MAIN_MISSION_TIP_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.MAIN_MISSION_TIP_FINISH)
		{
			TutorialManager.Show_MAIN_MISSION_TIP_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROB_CAR_START)
		{
			TutorialManager.Show_ROB_CAR_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROB_CAR_FINISH)
		{
			TutorialManager.Show_ROB_CAR_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.DRUG_USE_TIP_START)
		{
			TutorialManager.Show_DRUG_USE_TIP_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.DRUG_USE_TIP_FINISH)
		{
			TutorialManager.Show_DRUG_USE_TIP_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SIDE_MISSION_CLICK_START)
		{
			TutorialManager.Show_SIDE_MISSION_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SIDE_MISSION_CLICK_FINISH)
		{
			TutorialManager.Show_SIDE_MISSION_CLICK_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ENHANCE_ALL_START)
		{
			TutorialManager.Show_ENHANCE_ALL_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ENHANCE_ALL_CLICK)
		{
			TutorialManager.Show_ENHANCE_ALL_CLICK();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ENHANCE_ALL_EXIT)
		{
			TutorialManager.Show_ENHANCE_ALL_EXIT();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.AUTO_FIGHT_CLICK)
		{
			TutorialManager.Show_AUTO_FIGHT_CLICK();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.AUTO_FIGHT_FINISH)
		{
			TutorialManager.Show_AUTO_FIGHT_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SIDE_MISSION_CAR_TIP_START)
		{
			TutorialManager.Show_SIDE_MISSION_CAR_TIP_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SIDE_MISSION_CAR_TIP_FINISH)
		{
			TutorialManager.Show_SIDE_MISSION_CAR_TIP_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK_ALL)
		{
			TutorialManager.Show_SKILL_UPGRADE_CLICK_ALL();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK_SKILL)
		{
			TutorialManager.Show_SKILL_UPGRADE_CLICK_SKILL();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ENHANCE_CLICK_TAB)
		{
			TutorialManager.Show_ENHANCE_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ENHANCE_CLICK_ALL)
		{
			TutorialManager.Show_ENHANCE_CLICK_ALL();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_SHOW_REWARD)
		{
			TutorialManager.Show_GOLD_COPY_SHOW_REWARD();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_WAIT)
		{
			TutorialManager.Show_CREATE_TEAM_WAIT();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_CHOOSE_COPY)
		{
			TutorialManager.Show_CREATE_TEAM_CHOOSE_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SHOW_INVITE)
		{
			TutorialManager.Show_CREATE_TEAM_SHOW_INVITE();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SHOW_SHOUT)
		{
			TutorialManager.Show_CREATE_TEAM_SHOW_SHOUT();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SHOW_APPLICATION)
		{
			TutorialManager.Show_CREATE_TEAM_SHOW_APPLICATION();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SHOW_START)
		{
			TutorialManager.Show_CREATE_TEAM_SHOW_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SHOW_LEAVE)
		{
			TutorialManager.Show_CREATE_TEAM_SHOW_LEAVE();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SHOW_FINISH)
		{
			TutorialManager.Show_CREATE_TEAM_SHOW_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_CLICK_BACK)
		{
			TutorialManager.Show_CREATE_TEAM_CLICK_BACK();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CREATE_TEAM_SWITCH_MISSION)
		{
			TutorialManager.Show_CREATE_TEAM_SWITCH_MISSION();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.PVP_TIP_START)
		{
			TutorialManager.Show_PVP_TIP_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.PVP_TIP_POINT_PVP)
		{
			TutorialManager.Show_PVP_TIP_POINT_PVP();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.PVP_TIP_POINT_PVE)
		{
			TutorialManager.Show_PVP_TIP_POINT_PVE();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.PVP_TIP_SHOW_WORD1)
		{
			TutorialManager.Show_PVP_TIP_SHOW_WORD1();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.PVP_TIP_SHOW_WORD2)
		{
			TutorialManager.Show_PVP_TIP_SHOW_WORD2();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.PVP_TIP_FINISH)
		{
			TutorialManager.Show_PVP_TIP_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_GIFT_CLICK_BACK)
		{
			TutorialManager.Show_CAR_GIFT_CLICK_BACK();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_START)
		{
			TutorialManager.Show_BUY_BADGE_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_WAIT)
		{
			TutorialManager.Show_BUY_BADGE_WAIT();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_CHOOSE_BADGE)
		{
			TutorialManager.Show_BUY_BADGE_CHOOSE_BADGE();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BUY)
		{
			TutorialManager.Show_BUY_BADGE_CLICK_BUY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_WAIT_OPEN_BOX)
		{
			TutorialManager.Show_BUY_BADGE_WAIT_OPEN_BOX();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_CONFIRM)
		{
			TutorialManager.Show_BUY_BADGE_CLICK_CONFIRM();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BACK)
		{
			TutorialManager.Show_BUY_BADGE_CLICK_BACK();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.JOIN_TEAM_START)
		{
			TutorialManager.Show_JOIN_TEAM_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.JOIN_TEAM_SHOW_1)
		{
			TutorialManager.Show_JOIN_TEAM_SHOW_1();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.JOIN_TEAM_CLICK_URGE)
		{
			TutorialManager.Show_JOIN_TEAM_CLICK_URGE();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.JOIN_TEAM_FINISH)
		{
			TutorialManager.Show_JOIN_TEAM_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_WAIT_DATA)
		{
			TutorialManager.Show_WORLD_BOSS_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.NEW_MAP_TIP_START)
		{
			TutorialManager.Show_NEW_MAP_TIP_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.NEW_MAP_TIP_FINISH)
		{
			TutorialManager.Show_NEW_MAP_TIP_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TITLE_CLICK_TAP)
		{
			TutorialManager.Show_TITLE_CLICK_TAP();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.FUNCTION_TIP_START)
		{
			TutorialManager.Show_FUNCTION_TIP_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EXP_COPY_START)
		{
			TutorialManager.Show_EXP_COPY_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EXP_COPY_CLICK_DAILY)
		{
			TutorialManager.Show_EXP_COPY_CLICK_DAILY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EXP_COPY_WAIT_DATA)
		{
			TutorialManager.Show_EXP_COPY_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EXP_COPY_CLICK_COPY)
		{
			TutorialManager.Show_EXP_COPY_CLICK_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EXP_COPY_CLICK_START)
		{
			TutorialManager.Show_EXP_COPY_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EXP_COPY_FINISH)
		{
			TutorialManager.Show_EXP_COPY_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_COPY_START)
		{
			TutorialManager.Show_CAR_COPY_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_COPY_CLICK_DAILY)
		{
			TutorialManager.Show_CAR_COPY_CLICK_DAILY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_COPY_WAIT_DATA)
		{
			TutorialManager.Show_CAR_COPY_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_COPY_CLICK_COPY)
		{
			TutorialManager.Show_CAR_COPY_CLICK_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_COPY_CLICK_START)
		{
			TutorialManager.Show_CAR_COPY_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAR_COPY_FINISH)
		{
			TutorialManager.Show_CAR_COPY_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_START)
		{
			TutorialManager.Show_GOLD_COPY_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_DAILY)
		{
			TutorialManager.Show_GOLD_COPY_CLICK_DAILY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_WAIT_DATA)
		{
			TutorialManager.Show_GOLD_COPY_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_COPY)
		{
			TutorialManager.Show_GOLD_COPY_CLICK_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_START)
		{
			TutorialManager.Show_GOLD_COPY_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.GOLD_COPY_FINISH)
		{
			TutorialManager.Show_GOLD_COPY_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TOWER_START)
		{
			TutorialManager.Show_TOWER_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TOWER_CLICK_DAILY)
		{
			TutorialManager.Show_TOWER_CLICK_DAILY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TOWER_WAIT_DATA)
		{
			TutorialManager.Show_TOWER_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TOWER_CLICK_COPY)
		{
			TutorialManager.Show_TOWER_CLICK_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TOWER_CLICK_START)
		{
			TutorialManager.Show_TOWER_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.TOWER_FINISH)
		{
			TutorialManager.Show_TOWER_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EQUIP_COPY_START)
		{
			TutorialManager.Show_EQUIP_COPY_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EQUIP_COPY_CLICK_TAB)
		{
			TutorialManager.Show_EQUIP_COPY_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EQUIP_COPY_WAIT_DATA)
		{
			TutorialManager.Show_EQUIP_COPY_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EQUIP_COPY_CHOOSE_COPY)
		{
			TutorialManager.Show_EQUIP_COPY_CHOOSE_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EQUIP_COPY_CLICK_START)
		{
			TutorialManager.Show_EQUIP_COPY_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.EQUIP_COPY_FINISH)
		{
			TutorialManager.Show_EQUIP_COPY_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAPTURE_START)
		{
			TutorialManager.Show_CAPTURE_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAPTURE_CLICK_TAB)
		{
			TutorialManager.Show_CAPTURE_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAPTURE_WAIT_DATA)
		{
			TutorialManager.Show_CAPTURE_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAPTURE_CLICK_ITEM)
		{
			TutorialManager.Show_CAPTURE_CLICK_ITEM();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAPTURE_CLICK_START)
		{
			TutorialManager.Show_CAPTURE_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.CAPTURE_FINISH)
		{
			TutorialManager.Show_CAPTURE_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_START)
		{
			TutorialManager.Show_WORLD_BOSS_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_TAB)
		{
			TutorialManager.Show_WORLD_BOSS_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_WAIT_DATA)
		{
			TutorialManager.Show_WORLD_BOSS_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_CHOOSE_COPY)
		{
			TutorialManager.Show_WORLD_BOSS_CHOOSE_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_START)
		{
			TutorialManager.Show_WORLD_BOSS_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.WORLD_BOSS_FINISH)
		{
			TutorialManager.Show_WORLD_BOSS_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_START)
		{
			TutorialManager.Show_SURVIVAL_BATTLE_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_TAB)
		{
			TutorialManager.Show_SURVIVAL_BATTLE_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_WAIT_DATA)
		{
			TutorialManager.Show_SURVIVAL_BATTLE_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CHOOSE_COPY)
		{
			TutorialManager.Show_SURVIVAL_BATTLE_CHOOSE_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START)
		{
			TutorialManager.Show_SURVIVAL_BATTLE_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_FINISH)
		{
			TutorialManager.Show_SURVIVAL_BATTLE_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.MAIN_MISSION_PHONE_START)
		{
			TutorialManager.Show_MAIN_MISSION_PHONE_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION_TAB)
		{
			TutorialManager.Show_MAIN_MISSION_CLICK_MISSION_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION)
		{
			TutorialManager.Show_MAIN_MISSION_CLICK_MISSION();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.MAIN_MISSION_PHONE_FINISH)
		{
			TutorialManager.Show_MAIN_MISSION_PHONE_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ESCORT_START)
		{
			TutorialManager.Show_ESCORT_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ESCORT_CLICK_TAB)
		{
			TutorialManager.Show_ESCORT_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ESCORT_WAIT_DATA)
		{
			TutorialManager.Show_ESCORT_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ESCORT_CHOOSE_COPY)
		{
			TutorialManager.Show_ESCORT_CHOOSE_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ESCORT_CLICK_START)
		{
			TutorialManager.Show_ESCORT_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ESCORT_FINISH)
		{
			TutorialManager.Show_ESCORT_FINISH();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROBBORY_START)
		{
			TutorialManager.Show_ROBBORY_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROBBORY_CLICK_TAB)
		{
			TutorialManager.Show_ROBBORY_CLICK_TAB();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROBBORY_WAIT_DATA)
		{
			TutorialManager.Show_ROBBORY_WAIT_DATA();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROBBORY_CHOOSE_COPY)
		{
			TutorialManager.Show_ROBBORY_CHOOSE_COPY();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROBBORY_CLICK_START)
		{
			TutorialManager.Show_ROBBORY_CLICK_START();
		}
		else if (TutorialManager.mCurStep == TUTORIAL_STEP.ROBBORY_FINISH)
		{
			TutorialManager.Show_ROBBORY_FINISH();
		}
		else
		{
			switch (TutorialManager.mCurStep)
			{
			case TUTORIAL_STEP.MOVE_SCREEN:
				TutorialManager.Show_MOVE_SCREEN();
				break;
			case TUTORIAL_STEP.JOYSTICK_START:
				TutorialManager.Show_JOYSTICK_START();
				break;
			case TUTORIAL_STEP.CLICK_NPC:
				TutorialManager.Show_CLICK_NPC();
				break;
			case TUTORIAL_STEP.CLICK_NPC_FINISH:
				TutorialManager.Show_CLICK_NPC_FINISH();
				break;
			case TUTORIAL_STEP.MOVETO_CAR_POINT:
				TutorialManager.Show_MOVETO_CAR_POINT();
				break;
			case TUTORIAL_STEP.SWITCH_VIEW:
				TutorialManager.Show_SWITCH_VIEW();
				break;
			case TUTORIAL_STEP.NORMAL_ATTACK_BUTTON:
				TutorialManager.Show_NORMAL_ATTACK_BUTTON();
				break;
			case TUTORIAL_STEP.NORMAL_ATTACK_FINISH:
				TutorialManager.Show_NORMAL_ATTACK_FINISH();
				break;
			case TUTORIAL_STEP.SKILL1_BUTTON:
				TutorialManager.Show_SKILL1_BUTTON();
				break;
			case TUTORIAL_STEP.SKILL1_BTN_FINISH:
				TutorialManager.Show_SKILL1_BTN_FINISH();
				break;
			case TUTORIAL_STEP.SKILL2_BUTTON:
				TutorialManager.Show_SKILL2_BUTTON();
				break;
			case TUTORIAL_STEP.SKILL2_BTN_FINISH:
				TutorialManager.Show_SKILL2_BTN_FINISH();
				break;
			case TUTORIAL_STEP.SKILL3_BUTTON:
				TutorialManager.Show_SKILL3_BUTTON();
				break;
			case TUTORIAL_STEP.EQUIP_START_OPEN_MENUROOT:
				TutorialManager.Show_EQUIP_START_OPEN_MENUROOT();
				break;
			case TUTORIAL_STEP.EQUIP_OPEN_BACKPACK:
				TutorialManager.Show_EQUIP_OPEN_BACKPACK();
				break;
			case TUTORIAL_STEP.EQUIP_PRESS_EQUIPMENT:
				TutorialManager.Show_EQUIP_PRESS_EQUIPMENT();
				break;
			case TUTORIAL_STEP.EQUIP_PRESS_PUTONBTN:
				TutorialManager.Show_EQUIP_PRESS_PUTONBTN();
				break;
			case TUTORIAL_STEP.COPY_START_OPEN_MENU:
				TutorialManager.Show_COPY_START_OPEN_MENU();
				break;
			case TUTORIAL_STEP.COPY_OPEN_COPY_MENU:
				TutorialManager.Show_COPY_OPEN_COPY_MENU();
				break;
			case TUTORIAL_STEP.COPY_OPEN_NORMAL_COPY:
				TutorialManager.Show_COPY_OPEN_NORMAL_COPY();
				break;
			case TUTORIAL_STEP.COPY_PRESS_MISSION:
				TutorialManager.Show_COPY_PRESS_MISSION();
				break;
			case TUTORIAL_STEP.COPY_PRESS_PLAY:
				TutorialManager.Show_COPY_PRESS_PLAY();
				break;
			case TUTORIAL_STEP.COPY_BEGIN_TIP:
				TutorialManager.Show_COPY_BEGIN_TIP();
				break;
			case TUTORIAL_STEP.MAIN_MISSION_START:
				TutorialManager.Show_MAIN_MISSION_START();
				break;
			case TUTORIAL_STEP.MAIN_MISSION_FINISH:
				TutorialManager.Show_MAIN_MISSION_FINISH();
				break;
			case TUTORIAL_STEP.MAIN_MISSION_CLICK_START:
				TutorialManager.Show_Click_MAIN_MISSION_START();
				break;
			case TUTORIAL_STEP.MAIN_MISSION_CLICK_FINISH:
				TutorialManager.Show_Click_MAIN_MISSION_FINISH();
				break;
			case TUTORIAL_STEP.DAILY_MISSION_CLICK_START:
				TutorialManager.Show_Click_DAILY_MISSION_START();
				break;
			case TUTORIAL_STEP.DAILY_MISSION_CLICK_FINISH:
				TutorialManager.Show_Click_DAILY_MISSION_FINISH();
				break;
			case TUTORIAL_STEP.SKILL_UPGRADE_START:
				TutorialManager.Show_SKILL_UPGRADE_START();
				break;
			case TUTORIAL_STEP.SKILL_UPGRADE_CLICK:
				TutorialManager.Show_SKILL_UPGRADE_CLICK();
				break;
			case TUTORIAL_STEP.SKILL_UPGRADE_EXIT:
				TutorialManager.Show_SKILL_UPGRADE_EXIT();
				break;
			case TUTORIAL_STEP.ENHANCE_START:
				TutorialManager.Show_ENHANCE_START();
				break;
			case TUTORIAL_STEP.ENHANCE_CLICK:
				TutorialManager.Show_ENHANCE_CLICK();
				break;
			case TUTORIAL_STEP.ENHANCE_EXIT:
				TutorialManager.Show_ENHANCE_EXIT();
				break;
			case TUTORIAL_STEP.CAR_START:
				TutorialManager.Show_CAR_START();
				break;
			case TUTORIAL_STEP.CAR_CLICK_ACQUIRE:
				TutorialManager.Show_CAR_CLICK_ACQUIRE();
				break;
			case TUTORIAL_STEP.CAR_CLICK_GET:
				TutorialManager.Show_CAR_CLICK_GET();
				break;
			case TUTORIAL_STEP.CAR_CLICK_OK:
				TutorialManager.Show_CAR_CLICK_OK();
				break;
			case TUTORIAL_STEP.CAR_CLICK_EQUIP:
				TutorialManager.Show_CAR_CLICK_EQUIP();
				break;
			case TUTORIAL_STEP.CAR_CLICK_BACK:
				TutorialManager.Show_CAR_CLICK_BACK();
				break;
			case TUTORIAL_STEP.CAR_CLICK_MOUNT:
				TutorialManager.Show_CAR_CLICK_MOUNT();
				break;
			case TUTORIAL_STEP.CAR_SHOW_NEXT:
				TutorialManager.Show_CAR_SHOW_NEXT();
				break;
			case TUTORIAL_STEP.TOWER_SPECIAL_TIPS:
				TutorialManager.Show_TOWER_SPECIAL_TIPS();
				break;
			case TUTORIAL_STEP.TOWER_SPECIAL_TIPS_FINISH:
				TutorialManager.Show_TOWER_SPECIAL_TIPS_FINISH();
				break;
			case TUTORIAL_STEP.SKILL_DRAG_START:
				TutorialManager.Show_SKILL_DRAG_START();
				break;
			case TUTORIAL_STEP.SKILL_DRAG_MOVE:
				TutorialManager.Show_SKILL_DRAG_MOVE();
				break;
			case TUTORIAL_STEP.SKILL_DRAG_EXIT:
				TutorialManager.Show_SKILL_DRAG_EXIT();
				break;
			case TUTORIAL_STEP.CREATE_TEAM_START:
				TutorialManager.Show_CREATE_TEAM_START();
				break;
			case TUTORIAL_STEP.CREATE_TEAM_CREATE:
				TutorialManager.Show_CREATE_TEAM_CREATE();
				break;
			case TUTORIAL_STEP.CREATE_TEAM_CREATE_BTN:
				TutorialManager.Show_CREATE_TEAM_CREATE_BTN();
				break;
			case TUTORIAL_STEP.CREATE_TEAM_FINISH:
				TutorialManager.Show_CREATE_TEAM_FINISH();
				break;
			case TUTORIAL_STEP.SCUFFLE_COPY_START:
				TutorialManager.Show_SCUFFLE_COPY_START();
				break;
			case TUTORIAL_STEP.SCUFFLE_COPY_CLICK_DAILY:
				TutorialManager.Show_SCUFFLE_COPY_CLICK_DAILY();
				break;
			case TUTORIAL_STEP.SCUFFLE_COPY_WAIT_DATA:
				TutorialManager.Show_SCUFFLE_COPY_WAIT_DATA();
				break;
			case TUTORIAL_STEP.SCUFFLE_COPY_CLICK_COPY:
				TutorialManager.Show_SCUFFLE_COPY_CLICK_COPY();
				break;
			case TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START:
				TutorialManager.Show_SCUFFLE_COPY_CLICK_START();
				break;
			case TUTORIAL_STEP.SCUFFLE_COPY_FINISH:
				TutorialManager.Show_SCUFFLE_COPY_FINISH();
				break;
			case TUTORIAL_STEP.RANK_PVP_START:
				TutorialManager.Show_RANK_PVP_START();
				break;
			case TUTORIAL_STEP.RANK_PVP_CLICK_TAB:
				TutorialManager.Show_RANK_PVP_CLICK_TAB();
				break;
			case TUTORIAL_STEP.RANK_PVP_WAIT_DATA:
				TutorialManager.Show_RANK_PVP_WAIT_DATA();
				break;
			case TUTORIAL_STEP.RANK_PVP_CHOOSE:
				TutorialManager.Show_RANK_PVP_CHOOSE();
				break;
			case TUTORIAL_STEP.RANK_PVP_FINISH:
				TutorialManager.Show_RANK_PVP_FINISH();
				break;
			case TUTORIAL_STEP.TITLE_START:
				TutorialManager.Show_TITLE_START();
				break;
			case TUTORIAL_STEP.TITLE_CLICK:
				TutorialManager.Show_TITLE_CLICK();
				break;
			case TUTORIAL_STEP.TITLE_FINISH:
				TutorialManager.Show_TITLE_FINISH();
				break;
			case TUTORIAL_STEP.BADGE_START:
				TutorialManager.Show_BADGE_START();
				break;
			case TUTORIAL_STEP.BADGE_CLICK_TAB:
				TutorialManager.Show_BADGE_CLICK_TAB();
				break;
			case TUTORIAL_STEP.BADGE_CLICK_ITEM:
				TutorialManager.Show_BADGE_CLICK_ITEM();
				break;
			case TUTORIAL_STEP.BADGE_CLICK_EQUIP:
				TutorialManager.Show_BADGE_CLICK_EQUIP();
				break;
			case TUTORIAL_STEP.BADGE_FINISH:
				TutorialManager.Show_BADGE_FINISH();
				break;
			case TUTORIAL_STEP.SELL_ITEM_START:
				TutorialManager.Show_SELL_ITEM_START();
				break;
			case TUTORIAL_STEP.SELL_ITEM_CLICK_TAB:
				TutorialManager.Show_SELL_ITEM_CLICK_TAB();
				break;
			case TUTORIAL_STEP.SELL_ITEM_FINISH:
				TutorialManager.Show_SELL_ITEM_FINISH();
				break;
			case TUTORIAL_STEP.BAR_FIGHT_START:
				TutorialManager.Show_BAR_FIGHT_START();
				break;
			case TUTORIAL_STEP.BAR_FIGHT_CLICK_TAB:
				TutorialManager.Show_BAR_FIGHT_CLICK_TAB();
				break;
			case TUTORIAL_STEP.BAR_FIGHT_WAIT_DATA:
				TutorialManager.Show_BAR_FIGHT_WAIT_DATA();
				break;
			case TUTORIAL_STEP.BAR_FIGHT_CHOOSE_COPY:
				TutorialManager.Show_BAR_FIGHT_CHOOSE_COPY();
				break;
			case TUTORIAL_STEP.BAR_FIGHT_CLICK_START:
				TutorialManager.Show_BAR_FIGHT_CLICK_START();
				break;
			case TUTORIAL_STEP.BAR_FIGHT_FINISH:
				TutorialManager.Show_BAR_FIGHT_FINISH();
				break;
			case TUTORIAL_STEP.GUILD_BOSS_START:
				TutorialManager.Show_GUILD_BOSS_START();
				break;
			case TUTORIAL_STEP.GUILD_BOSS_CLICK_TAB:
				TutorialManager.Show_GUILD_BOSS_CLICK_TAB();
				break;
			case TUTORIAL_STEP.GUILD_BOSS_WAIT_DATA:
				TutorialManager.Show_GUILD_BOSS_WAIT_DATA();
				break;
			case TUTORIAL_STEP.GUILD_BOSS_CLICK_START:
				TutorialManager.Show_GUILD_BOSS_CLICK_START();
				break;
			case TUTORIAL_STEP.GUILD_BOSS_FINISH:
				TutorialManager.Show_GUILD_BOSS_FINISH();
				break;
			case TUTORIAL_STEP.GUILD_START:
				TutorialManager.Show_GUILD_START();
				break;
			case TUTORIAL_STEP.GUILD_WAIT_DATA:
				TutorialManager.Show_GUILD_WAIT_DATA();
				break;
			case TUTORIAL_STEP.GUILD_LIST:
				TutorialManager.Show_GUILD_LIST();
				break;
			case TUTORIAL_STEP.GUILD_CREATE:
				TutorialManager.Show_GUILD_CREATE();
				break;
			case TUTORIAL_STEP.GUILD_NAME:
				TutorialManager.Show_GUILD_NAME();
				break;
			case TUTORIAL_STEP.GUILD_COST:
				TutorialManager.Show_GUILD_COST();
				break;
			case TUTORIAL_STEP.GUILD_INFO:
				TutorialManager.Show_GUILD_INFO();
				break;
			case TUTORIAL_STEP.GUILD_FINISH:
				TutorialManager.Show_GUILD_FINISH();
				break;
			case TUTORIAL_STEP.STRENGTH_STAR_START:
				TutorialManager.Show_STRENGTH_STAR_START();
				break;
			case TUTORIAL_STEP.STRENGTH_STAR_CLICK_TAB:
				TutorialManager.Show_STRENGTH_STAR_CLICK_TAB();
				break;
			case TUTORIAL_STEP.STRENGTH_STAR_CLICK_BTN:
				TutorialManager.Show_STRENGTH_STAR_CLICK_BTN();
				break;
			case TUTORIAL_STEP.STRENGTH_STAR_FINISH:
				TutorialManager.Show_STRENGTH_STAR_FINISH();
				break;
			case TUTORIAL_STEP.SLOT_START:
				TutorialManager.Show_SLOT_START();
				break;
			case TUTORIAL_STEP.SLOT_WAIT_DATA:
				TutorialManager.Show_SLOT_WAIT_DATA();
				break;
			case TUTORIAL_STEP.SLOT_CLICK:
				TutorialManager.Show_SLOT_CLICK();
				break;
			case TUTORIAL_STEP.SLOAT_FINISH:
				TutorialManager.Show_SLOAT_FINISH();
				break;
			case TUTORIAL_STEP.CLICK_STRONGER_START:
				TutorialManager.Show_Click_STRONGER_START();
				break;
			case TUTORIAL_STEP.CLICK_STRONGER_FINISH:
				TutorialManager.Show_Click_STRONGER_FINISH();
				break;
			case TUTORIAL_STEP.DRUG_TIP_START:
				TutorialManager.Show_DRUG_TIP_START();
				break;
			case TUTORIAL_STEP.DRUG_TIP_FINISH:
				TutorialManager.Show_DRUG_TIP_FINISH();
				break;
			case TUTORIAL_STEP.CAR_ACCBTN_START:
				TutorialManager.Show_CAR_ACCBTN_START();
				break;
			case TUTORIAL_STEP.CAR_ACCBTN_FINISH:
				TutorialManager.Show_CAR_ACCBTN_FINISH();
				break;
			case TUTORIAL_STEP.CAR_CTL_START:
				TutorialManager.Show_CAR_CTL_START();
				break;
			case TUTORIAL_STEP.CAR_CTL_TARGET:
				TutorialManager.Show_CAR_CTL_TARGET();
				break;
			case TUTORIAL_STEP.CAR_CTL_BRAKE:
				TutorialManager.Show_CAR_CTL_BRAKE();
				break;
			case TUTORIAL_STEP.CAR_CTL_FINISH:
				TutorialManager.Show_CAR_CTL_FINISH();
				break;
			}
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0003D92C File Offset: 0x0003BB2C
	public static void Show_NEW_MAP_TIP_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.MapPicObj.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.NEW_MAP_TIP_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, 20f, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0003D9B0 File Offset: 0x0003BBB0
	public static void Show_NEW_MAP_TIP_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
	public static void Show_JOIN_TEAM_START()
	{
		if (!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
		{
			TutorialManager.CloseTutorial();
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsTeamLeader())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.JOIN_TEAM_TUTORIAL);
		UIWidget emptyPlusPic = SingletonUnity<TeamUIRootNewLogic>.Instance.PlayerPicLogicList[1].EmptyPlusPic;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(emptyPlusPic.gameObject, showObjList, emptyPlusPic.width, emptyPlusPic.height, StrDictionary.GetClientDictionaryString("#{605520}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, false, false);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0003DA8C File Offset: 0x0003BC8C
	public static void Show_JOIN_TEAM_SHOW_1()
	{
		if (!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
		{
			TutorialManager.CloseTutorial();
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsTeamLeader())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		UIWidget emptyPlusPic = SingletonUnity<TeamUIRootNewLogic>.Instance.PlayerPicLogicList[1].EmptyPlusPic;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(emptyPlusPic.gameObject, showObjList, emptyPlusPic.width, emptyPlusPic.height, StrDictionary.GetClientDictionaryString("#{605521}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, false, false);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0003DB48 File Offset: 0x0003BD48
	public static void Show_JOIN_TEAM_CLICK_URGE()
	{
		if (!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
		{
			TutorialManager.CloseTutorial();
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsTeamLeader())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		GameObject urgeBtnRoot = SingletonUnity<TeamUIRootNewLogic>.Instance.UrgeBtnRoot;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(urgeBtnRoot, showObjList, 100, 50, StrDictionary.GetClientDictionaryString("#{605522}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0003DBEC File Offset: 0x0003BDEC
	public static void Show_JOIN_TEAM_FINISH()
	{
		if (!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
		{
			TutorialManager.CloseTutorial();
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsTeamLeader())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		UIWidget emptyPlusPic = SingletonUnity<TeamUIRootNewLogic>.Instance.PlayerPicLogicList[1].EmptyPlusPic;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(emptyPlusPic.gameObject, showObjList, emptyPlusPic.width, emptyPlusPic.height, StrDictionary.GetClientDictionaryString("#{605523}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, false, false);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
	public static void Show_BUY_BADGE_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject) && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.ShopFuncBtn.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.ShopFuncBtn.IconSp;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(iconSp);
			TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height + 100, StrDictionary.GetDictionaryString("#{605516}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, true, true, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0003DD60 File Offset: 0x0003BF60
	public static void Show_BUY_BADGE_WAIT()
	{
		if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ShopTabRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0003DDAC File Offset: 0x0003BFAC
	public static void Show_BUY_BADGE_CHOOSE_BADGE()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject) && SingletonUnity<ShopTabRootLogic>.Instance.CurSelectObj != null)
		{
			ShopItemBtnLogic component = SingletonUnity<ShopTabRootLogic>.Instance.CurSelectObj.GetComponent<ShopItemBtnLogic>();
			shop_item curSelectItem = SingletonUnity<ShopTabRootLogic>.Instance.CurSelectItem;
			if (component != null && curSelectItem.ID.Equals("9999"))
			{
				component.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget bkSprite = component.BkSprite;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(bkSprite);
				TutorialUIRootLogic.ShowWindow(bkSprite.gameObject, list, bkSprite.width, bkSprite.height, StrDictionary.GetDictionaryString("#{605517}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0003DE98 File Offset: 0x0003C098
	public static void Show_BUY_BADGE_CLICK_BUY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ShopTabRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget buyBtnRoot = SingletonUnity<ShopTabRootLogic>.Instance.BuyBtnRoot;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(buyBtnRoot);
			TutorialUIRootLogic.ShowWindow(buyBtnRoot.gameObject, list, buyBtnRoot.width, buyBtnRoot.height, StrDictionary.GetDictionaryString("#{605518}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, true, true, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0003DF30 File Offset: 0x0003C130
	public static void Show_BUY_BADGE_WAIT_OPEN_BOX()
	{
		if (SingletonUnity<OpenBoxRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OpenBoxRootLogic>.Instance.gameObject))
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.BUY_BADGE_TUTORIAL);
			SingletonUnity<OpenBoxRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0003DF8C File Offset: 0x0003C18C
	public static void Show_BUY_BADGE_CLICK_CONFIRM()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<OpenBoxRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OpenBoxRootLogic>.Instance.gameObject))
		{
			SingletonUnity<OpenBoxRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget okBtnRoot = SingletonUnity<OpenBoxRootLogic>.Instance.OkBtnRoot;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(okBtnRoot);
			TutorialUIRootLogic.ShowWindow(okBtnRoot.gameObject, list, okBtnRoot.width, okBtnRoot.height, StrDictionary.GetDictionaryString("#{605519}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, false, true, true, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0003E024 File Offset: 0x0003C224
	public static void Show_BUY_BADGE_CLICK_BACK()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(exitBtn);
		TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0003E088 File Offset: 0x0003C288
	public static void Show_PVP_TIP_START()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.PVP_BTN_TUTORIAL_TIP);
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		UIWidget pvpBtnPic = instance.PvpBtnPic;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(instance.PvpBtnPic);
		list.Add(instance.PveBtnPic);
		list.Add(instance.PvpLabel);
		list.Add(instance.PveLabel);
		TutorialUIRootLogic.ShowWindow(pvpBtnPic.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600060}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, false, false);
		SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0003E130 File Offset: 0x0003C330
	public static void Show_PVP_TIP_POINT_PVP()
	{
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		UIWidget pvpBtnPic = instance.PvpBtnPic;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(instance.PvpBtnPic);
		list.Add(instance.PveBtnPic);
		list.Add(instance.PvpLabel);
		list.Add(instance.PveLabel);
		TutorialUIRootLogic.ShowWindow(pvpBtnPic.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600061}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, true, true);
		UnityVersionUtil.SetActiveRecursive(SingletonUnity<TutorialUIRootLogic>.Instance.TipCircleSprite.gameObject, false);
		SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0003E1DC File Offset: 0x0003C3DC
	public static void Show_PVP_TIP_POINT_PVE()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetPKModeState(0);
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		UIWidget pveBtnPic = instance.PveBtnPic;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(instance.PvpBtnPic);
		list.Add(instance.PveBtnPic);
		list.Add(instance.PvpLabel);
		list.Add(instance.PveLabel);
		TutorialUIRootLogic.ShowWindow(pveBtnPic.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600062}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, true, true);
		UnityVersionUtil.SetActiveRecursive(SingletonUnity<TutorialUIRootLogic>.Instance.TipCircleSprite.gameObject, false);
		SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0003E298 File Offset: 0x0003C498
	public static void Show_PVP_TIP_SHOW_WORD1()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetPKModeState(1);
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		UIWidget pveBtnPic = instance.PveBtnPic;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(instance.PvpBtnPic);
		list.Add(instance.PveBtnPic);
		list.Add(instance.PvpLabel);
		list.Add(instance.PveLabel);
		TutorialUIRootLogic.ShowWindow(pveBtnPic.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600063}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, false, false);
		SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0003E33C File Offset: 0x0003C53C
	public static void Show_PVP_TIP_SHOW_WORD2()
	{
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		UIWidget pveBtnPic = instance.PveBtnPic;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(instance.PvpBtnPic);
		list.Add(instance.PveBtnPic);
		list.Add(instance.PvpLabel);
		list.Add(instance.PveLabel);
		TutorialUIRootLogic.ShowWindow(pveBtnPic.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600064}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, false, false);
		SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
	public static void Show_PVP_TIP_FINISH()
	{
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
	public static void Show_AUTO_FIGHT_CLICK()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.AUTO_FIGHT_TUTORIAL_TIP))
		{
			Singleton<ObjManager>.Instance.MainPlayer.LeveAutoCombat();
			SingletonUnity<CopyFunctionRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget autoBtnSprite = SingletonUnity<CopyFunctionRootLogic>.Instance.AutoBtnSprite;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(autoBtnSprite);
			TutorialUIRootLogic.ShowWindow(autoBtnSprite.gameObject, list, autoBtnSprite.width, autoBtnSprite.height + 100, StrDictionary.GetDictionaryString("#{600012}", new object[0]), SCREEN_DIRECTION.BOTTOM_LEFT, true, true, true, true, true);
		}
		else
		{
			NetLogic.GetInstance().Send<Protocol.map_ready>(null, null);
			LoadingWindow.isSendMapReady = true;
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0003E484 File Offset: 0x0003C684
	public static void Show_AUTO_FIGHT_FINISH()
	{
		NetLogic.GetInstance().Send<Protocol.map_ready>(null, null);
		LoadingWindow.isSendMapReady = true;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.AUTO_FIGHT_TUTORIAL_TIP);
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0003E4C0 File Offset: 0x0003C6C0
	public static void Show_ROB_CAR_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<RobCarBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
			Time.timeScale = 0f;
			SingletonUnity<RobCarBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<RobCarBtnRootLogic>.Instance.RobCarBtn.gameObject;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(SingletonUnity<RobCarBtnRootLogic>.Instance.RobCarBtn);
			TutorialUIRootLogic.ShowWindow(gameObject, list, 70, 70, StrDictionary.GetClientDictionaryString("#{600011}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, false, true, true);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "8ROB_CAR_START");
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0003E57C File Offset: 0x0003C77C
	public static void Show_ROB_CAR_FINISH()
	{
		Time.timeScale = 1f;
		FunctionTipsRootLogic.ClearHandTip();
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0003E590 File Offset: 0x0003C790
	public static void Show_CAR_ACCBTN_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		Time.timeScale = 0f;
		if (SingletonUnity<CarControllerRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CarControllerRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
			GameObject gameObject = SingletonUnity<CarControllerRootLogic>.Instance.AccelBtnListener.gameObject;
			List<UIWidget> showObjList = new List<UIWidget>();
			TutorialUIRootLogic.ShowWindow(gameObject, showObjList, 70, 70, StrDictionary.GetClientDictionaryString("#{600071}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, false, true, true);
			if (SingletonUnity<TutorialUIRootLogic>.Exists)
			{
				SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), 0.5f);
			}
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "9CAR_ACCBTN");
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0003E64C File Offset: 0x0003C84C
	public static void Show_CAR_ACCBTN_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0003E65C File Offset: 0x0003C85C
	public static void Show_CAR_CTL_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.StreetRacingMode == 1)
		{
			SingletonUnity<CarControllerRootLogic>.Instance.TutorialShowAllBtn();
			GameObject gameObject = SingletonUnity<CarControllerRootLogic>.Instance.LeftBtnListener.gameObject;
			List<UIWidget> showObjList = new List<UIWidget>();
			TutorialUIRootLogic.ShowWindow(gameObject, showObjList, 70, 70, StrDictionary.GetClientDictionaryString("#{600006}", new object[0]), SCREEN_DIRECTION.TOP_RIGHT, true, true, false, true, true);
			if (SingletonUnity<TutorialUIRootLogic>.Exists)
			{
				SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), 0.5f);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.ROB_CAR_TIP);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "10CAR_CTL_START");
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0003E724 File Offset: 0x0003C924
	public static void Show_CAR_CTL_TARGET()
	{
		if (SingletonUnity<CarTargetUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CarTargetUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<CarTargetUIRootLogic>.Instance.targetPic.gameObject;
			List<UIWidget> showObjList = new List<UIWidget>();
			TutorialUIRootLogic.ShowWindow(gameObject, showObjList, 70, 70, StrDictionary.GetClientDictionaryString("#{600007}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, true, true);
			if (SingletonUnity<TutorialUIRootLogic>.Exists)
			{
				SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), 0.5f);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "11CAR_CTL_TARGET");
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0003E7D0 File Offset: 0x0003C9D0
	public static void Show_CAR_CTL_BRAKE()
	{
		GameObject gameObject = SingletonUnity<CarControllerRootLogic>.Instance.BrakeBtnListener.gameObject;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(gameObject, showObjList, 70, 70, StrDictionary.GetClientDictionaryString("#{600008}", new object[0]), SCREEN_DIRECTION.TOP, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), 0.5f);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "12CAR_CTL_BRAKE");
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0003E854 File Offset: 0x0003CA54
	public static void Show_CAR_CTL_FINISH()
	{
		Time.timeScale = 1f;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "13CAR_CTL_FINISH");
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0003E87C File Offset: 0x0003CA7C
	public static void Show_DRUG_TIP_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<PotionLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PotionLogic>.Instance.gameObject))
		{
			SingletonUnity<PotionLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<PotionLogic>.Instance.PotionShowSprite.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.DRUG_TIP_START, true, string.Empty, SCREEN_DIRECTION.TOP, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
	public static void Show_DRUG_TIP_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.USE_DRUG_TIP);
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0003E918 File Offset: 0x0003CB18
	public static void Show_DRUG_USE_TIP_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<PotionLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PotionLogic>.Instance.gameObject))
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (mainPlayer != null)
			{
				SingletonUnity<PotionLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				GameObject gameObject = SingletonUnity<PotionLogic>.Instance.PotionShowSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.DRUG_USE_TIP_START, true, string.Empty, SCREEN_DIRECTION.TOP, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0003E9C0 File Offset: 0x0003CBC0
	public static void Show_DRUG_USE_TIP_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.LOW_HP_USE_DRUG_TIP);
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0003E9E0 File Offset: 0x0003CBE0
	public static void Show_Click_STRONGER_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject) && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.StrongerIcon.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.StrongerIcon.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SLOT_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0003EA80 File Offset: 0x0003CC80
	public static void Show_Click_STRONGER_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0003EA90 File Offset: 0x0003CC90
	public static void Show_Click_DAILY_MISSION_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonUnity<MissionTeamTipLogic>.Instance.ShowMissionTip();
		MissionTipLineLogic dailyMissionLine = SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.GetDailyMissionLine();
		if (dailyMissionLine != null)
		{
			dailyMissionLine.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = dailyMissionLine.gameObject;
			if (GameManager.IsSupportCurDataVersion())
			{
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, -Vector3.up * (float)dailyMissionLine.GetComponent<UIWidget>().height / 2f, TUTORIAL_STEP.DAILY_MISSION_CLICK_START, false, StrDictionary.GetDictionaryString("#{102058}", new object[0]), SCREEN_DIRECTION.RIGHT, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, -Vector3.up * (float)dailyMissionLine.GetComponent<UIWidget>().height / 2f, TUTORIAL_STEP.DAILY_MISSION_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
		}
		else
		{
			Debug.Log("Show click daily Mission Line Is Null!");
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0003EB8C File Offset: 0x0003CD8C
	public static void Show_Click_DAILY_MISSION_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0003EB9C File Offset: 0x0003CD9C
	public static void Show_SIDE_MISSION_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonUnity<MissionTeamTipLogic>.Instance.ShowMissionTip();
		MissionTipLineLogic targetMissionLine = SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.GetTargetMissionLine(2);
		if (targetMissionLine != null)
		{
			targetMissionLine.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = targetMissionLine.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, -Vector3.up * (float)targetMissionLine.GetComponent<UIWidget>().height / 2f, TUTORIAL_STEP.SIDE_MISSION_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			Debug.Log("Show click side Mission Line Is Null!");
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0003EC40 File Offset: 0x0003CE40
	public static void Show_SIDE_MISSION_CLICK_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0003EC50 File Offset: 0x0003CE50
	public static void Show_SIDE_MISSION_CAR_TIP_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MissionTeamTipLogic>.Instance.CurPage != 1)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.ShowMissionTip();
		}
		MissionTipLineLogic targetMissionLineById = SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.GetTargetMissionLineById("40010");
		if (targetMissionLineById != null)
		{
			targetMissionLineById.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = targetMissionLineById.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, -Vector3.up * (float)targetMissionLineById.GetComponent<UIWidget>().height / 2f + Vector3.right * 50f, TUTORIAL_STEP.SIDE_MISSION_CLICK_START, false, StrDictionary.GetDictionaryString("#{600019}", new object[0]), SCREEN_DIRECTION.RIGHT, Vector3.zero, TutorialManager.TutorialTipDuration, SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.transform, false, false);
		}
		else
		{
			Debug.Log("Show click side Mission Line Is Null!");
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0003ED34 File Offset: 0x0003CF34
	public static void Show_SIDE_MISSION_CAR_TIP_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0003ED44 File Offset: 0x0003CF44
	public static void Show_Click_MAIN_MISSION_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonUnity<MissionTeamTipLogic>.Instance.ShowMissionTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0003ED5C File Offset: 0x0003CF5C
	public static void Show_Click_MAIN_MISSION_FINISH()
	{
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0003ED64 File Offset: 0x0003CF64
	public static void Show_SLOT_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject) && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.SlotFuncBtn.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.SlotFuncBtn.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SLOT_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0003EE04 File Offset: 0x0003D004
	public static void Show_SLOT_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<SlotUIRootLogic>.Exists)
		{
			SingletonUnity<SlotUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0003EE2C File Offset: 0x0003D02C
	public static void Show_SLOT_CLICK()
	{
		if (SingletonUnity<SlotUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SlotUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<SlotUIRootLogic>.Instance.SpinSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SLOT_CLICK, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
	public static void Show_SLOAT_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
	public static void Show_STRENGTH_STAR_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(false);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.STRENGTH_STAR_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0003EF68 File Offset: 0x0003D168
	public static void Show_STRENGTH_STAR_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<EquipStrengthenUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EquipStrengthenUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<EquipStrengthenUIRootLogic>.Instance.CurPageType != EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.REFINE)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.STRENGTH_STAR_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0003F014 File Offset: 0x0003D214
	public static void Show_STRENGTH_STAR_CLICK_BTN()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<RefineUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RefineUIRootLogic>.Instance.gameObject))
		{
			GameObject upgradeBtn = SingletonUnity<RefineUIRootLogic>.Instance.UpgradeBtn;
			FunctionTipsRootLogic.AddFunctionHandTips(upgradeBtn, Vector3.zero, TUTORIAL_STEP.STRENGTH_STAR_CLICK_BTN, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<RefineUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0003F08C File Offset: 0x0003D28C
	public static void Show_STRENGTH_STAR_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0003F09C File Offset: 0x0003D29C
	public static void Show_GUILD_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(false);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.GuildFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.GuildFuncBtn.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GUILD_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0003F150 File Offset: 0x0003D350
	public static void Show_GUILD_WAIT_DATA()
	{
		SingletonUnity<NewGuildUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0003F168 File Offset: 0x0003D368
	public static void Show_GUILD_LIST()
	{
		UIWidget btnIconSprite = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[0].BtnIconSprite;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(btnIconSprite.gameObject, showObjList, btnIconSprite.width + 70, btnIconSprite.height, StrDictionary.GetClientDictionaryString("#{605506}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
	public static void Show_GUILD_CREATE()
	{
		UIWidget btnIconSprite = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnIconSprite;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(btnIconSprite.gameObject, showObjList, btnIconSprite.width + 70, btnIconSprite.height, string.Format(StrDictionary.GetClientDictionaryString("#{605507}", new object[0]), new object[0]), SCREEN_DIRECTION.RIGHT, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0003F274 File Offset: 0x0003D474
	public static void Show_GUILD_NAME()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].OnClickBtn();
		UIWidget diamondLabel = SingletonUnity<CreateGuildRootLogic>.Instance.DiamondLabel;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(diamondLabel.gameObject, showObjList, diamondLabel.width, 50, StrDictionary.GetClientDictionaryString("#{605508}", new object[0]), SCREEN_DIRECTION.LEFT, false, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0003F2F8 File Offset: 0x0003D4F8
	public static void Show_GUILD_COST()
	{
		UIWidget diamondLabel = SingletonUnity<CreateGuildRootLogic>.Instance.DiamondLabel;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(SingletonUnity<CreateGuildRootLogic>.Instance.DiamondCreateBtn);
		list.Add(SingletonUnity<CreateGuildRootLogic>.Instance.CashCreateBtn);
		TutorialUIRootLogic.ShowWindow(diamondLabel.gameObject, list, diamondLabel.width, 50, StrDictionary.GetClientDictionaryString("#{605509}", new object[0]), SCREEN_DIRECTION.LEFT, true, true, true, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0003F388 File Offset: 0x0003D588
	public static void Show_GUILD_INFO()
	{
		UIWidget diamondLabel = SingletonUnity<CreateGuildRootLogic>.Instance.DiamondLabel;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(diamondLabel.gameObject, showObjList, diamondLabel.width, 50, string.Format(StrDictionary.GetClientDictionaryString("#{605510}", new object[0]), new object[0]), SCREEN_DIRECTION.LEFT, false, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0003F400 File Offset: 0x0003D600
	public static void Show_GUILD_FINISH()
	{
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0003F408 File Offset: 0x0003D608
	public static void Show_GUILD_BOSS_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			if (!SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenLeftBtn)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickLeftArrowBtn(false);
			}
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.ActivityBtnIcon.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.ActivityBtnIcon.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GUILD_BOSS_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0003F4CC File Offset: 0x0003D6CC
	public static void Show_GUILD_BOSS_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<ActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<ActivityUIRootLogic>.Instance.CurPageIndex != 4)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GUILD_BOSS_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0003F578 File Offset: 0x0003D778
	public static void Show_GUILD_BOSS_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			if (SingletonUnity<GuildActivityRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildActivityRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildActivityRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0003F5DC File Offset: 0x0003D7DC
	public static void Show_GUILD_BOSS_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<GuildActivityRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildActivityRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<GuildActivityRootLogic>.Instance.startBtnSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GUILD_BOSS_CLICK_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<GuildActivityRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0003F658 File Offset: 0x0003D858
	public static void Show_GUILD_BOSS_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0003F668 File Offset: 0x0003D868
	public static void Show_ROBBORY_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ROBBORY_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0003F6EC File Offset: 0x0003D8EC
	public static void Show_ROBBORY_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 3)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ROBBORY_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0003F798 File Offset: 0x0003D998
	public static void Show_ROBBORY_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0003F7E0 File Offset: 0x0003D9E0
	public static void Show_ROBBORY_CHOOSE_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			List<ActivityItemLogic> activityItems = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.ActivityItems;
			for (int i = 0; i < activityItems.Count; i++)
			{
				if (activityItems[i].curActivityInfo.Type == 2L)
				{
					GameObject gameObject = activityItems[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ROBBORY_CHOOSE_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, activityItems[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
	public static void Show_ROBBORY_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.startBtnSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ROBBORY_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0003F92C File Offset: 0x0003DB2C
	public static void Show_ROBBORY_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0003F93C File Offset: 0x0003DB3C
	public static void Show_ESCORT_START()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.ESCORT_TUTORIAL_TIP);
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ESCORT_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0003F9D4 File Offset: 0x0003DBD4
	public static void Show_ESCORT_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 3)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ESCORT_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0003FA80 File Offset: 0x0003DC80
	public static void Show_ESCORT_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0003FAC8 File Offset: 0x0003DCC8
	public static void Show_ESCORT_CHOOSE_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			List<ActivityItemLogic> activityItems = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.ActivityItems;
			for (int i = 0; i < activityItems.Count; i++)
			{
				if (activityItems[i].curActivityInfo.Type == 1L)
				{
					GameObject gameObject = activityItems[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ESCORT_CHOOSE_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, activityItems[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0003FB98 File Offset: 0x0003DD98
	public static void Show_ESCORT_SHOW_REWARD()
	{
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0003FB9C File Offset: 0x0003DD9C
	public static void Show_ESCORT_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.startBtnSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.ESCORT_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0003FC18 File Offset: 0x0003DE18
	public static void Show_ESCORT_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0003FC28 File Offset: 0x0003DE28
	public static void Show_BAR_FIGHT_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			if (!SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenLeftBtn)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickLeftArrowBtn(false);
			}
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.ActivityBtnIcon.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.ActivityBtnIcon.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.BAR_FIGHT_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0003FCEC File Offset: 0x0003DEEC
	public static void Show_BAR_FIGHT_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<ActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<ActivityUIRootLogic>.Instance.CurPageIndex != 1)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.BAR_FIGHT_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0003FD98 File Offset: 0x0003DF98
	public static void Show_BAR_FIGHT_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<DailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyActivityUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<DailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
	public static void Show_BAR_FIGHT_CHOOSE_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		List<ActivityItemLogic> activityItems = SingletonUnity<DailyActivityUIRootLogic>.Instance.ActivityItems;
		List<activity_info> activityList = SingletonUnity<DailyActivityUIRootLogic>.Instance.ActivityList;
		for (int i = 0; i < activityItems.Count; i++)
		{
			if (activityList[activityItems[i].curIndex].Type == 4L)
			{
				GameObject gameObject = activityItems[i].IconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.BAR_FIGHT_CHOOSE_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, activityItems[i].transform.parent.parent, false, true);
				SingletonUnity<DailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				break;
			}
		}
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
	public static void Show_BAR_FIGHT_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		GameObject gameObject = SingletonUnity<DailyActivityUIRootLogic>.Instance.startBtnSp.gameObject;
		FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.BAR_FIGHT_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		SingletonUnity<DailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0003FF00 File Offset: 0x0003E100
	public static void Show_BAR_FIGHT_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0003FF10 File Offset: 0x0003E110
	public static void Show_SELL_ITEM_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			if (!SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenLeftBtn)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickLeftArrowBtn(false);
			}
			TutorialManager.CloseTutorial();
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0003FF6C File Offset: 0x0003E16C
	public static void Show_SELL_ITEM_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<ConsignRootLogic>.Instance.CurrentPage != 1)
		{
			SingletonUnity<ConsignRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject tab2Obj = SingletonUnity<ConsignRootLogic>.Instance.Tab2Obj;
			FunctionTipsRootLogic.AddFunctionHandTips(tab2Obj, Vector3.zero, TUTORIAL_STEP.SELL_ITEM_CLICK_TAB, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0003FFE0 File Offset: 0x0003E1E0
	public static void Show_SELL_ITEM_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0003FFF0 File Offset: 0x0003E1F0
	public static void Show_CAPTURE_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAPTURE_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00040074 File Offset: 0x0003E274
	public static void Show_CAPTURE_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 1)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAPTURE_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00040120 File Offset: 0x0003E320
	public static void Show_CAPTURE_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<DominRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DominRootLogic>.Instance.gameObject))
		{
			SingletonUnity<DominRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00040168 File Offset: 0x0003E368
	public static void Show_CAPTURE_CLICK_ITEM()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<DominRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DominRootLogic>.Instance.gameObject))
		{
			List<DominLineLogic> dominLineList = SingletonUnity<DominRootLogic>.Instance.DominLineList;
			if (dominLineList.Count > 0)
			{
				GameObject gameObject = dominLineList[0].PlayerIcon.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAPTURE_CLICK_ITEM, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, dominLineList[0].transform.parent.parent, false, true);
				SingletonUnity<DominRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
		}
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00040210 File Offset: 0x0003E410
	public static void Show_CAPTURE_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<DominInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DominInfoRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<DominInfoRootLogic>.Instance.ChallengesRoot.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAPTURE_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<DominInfoRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0004028C File Offset: 0x0003E48C
	public static void Show_CAPTURE_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0004029C File Offset: 0x0003E49C
	public static void Show_EQUIP_COPY_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EQUIP_COPY_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00040320 File Offset: 0x0003E520
	public static void Show_EQUIP_COPY_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 2)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EQUIP_COPY_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x000403CC File Offset: 0x0003E5CC
	public static void Show_EQUIP_COPY_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x00040414 File Offset: 0x0003E614
	public static void Show_EQUIP_COPY_CHOOSE_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			List<DailyCopyLineLogic> dailyCopyLineList = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.DailyCopyLineList;
			for (int i = 0; i < dailyCopyLineList.Count; i++)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(dailyCopyLineList[i].Key);
				if (copySceneDataById.SubType == 16)
				{
					GameObject gameObject = dailyCopyLineList[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EQUIP_COPY_CHOOSE_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, dailyCopyLineList[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x000404F0 File Offset: 0x0003E6F0
	public static void Show_EQUIP_COPY_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.SingleBtn.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EQUIP_COPY_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00040570 File Offset: 0x0003E770
	public static void Show_EQUIP_COPY_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00040580 File Offset: 0x0003E780
	public static void Show_WORLD_BOSS_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.WORLD_BOSS_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00040604 File Offset: 0x0003E804
	public static void Show_WORLD_BOSS_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 3)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.WORLD_BOSS_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x000406AC File Offset: 0x0003E8AC
	public static void Show_WORLD_BOSS_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x000406F4 File Offset: 0x0003E8F4
	public static void Show_WORLD_BOSS_CHOOSE_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			List<WildBossLineLogic> wildBossLineList = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.WildBossLineList;
			GameObject gameObject = wildBossLineList[0].IconSprite.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.WORLD_BOSS_CHOOSE_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, wildBossLineList[0].transform.parent.parent, false, true);
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00040790 File Offset: 0x0003E990
	public static void Show_WORLD_BOSS_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.startBtnSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.WORLD_BOSS_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00040808 File Offset: 0x0003EA08
	public static void Show_WORLD_BOSS_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00040818 File Offset: 0x0003EA18
	public static void Show_SURVIVAL_BATTLE_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SURVIVAL_BATTLE_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0004089C File Offset: 0x0003EA9C
	public static void Show_SURVIVAL_BATTLE_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 3)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00040948 File Offset: 0x0003EB48
	public static void Show_SURVIVAL_BATTLE_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00040990 File Offset: 0x0003EB90
	public static void Show_SURVIVAL_BATTLE_CHOOSE_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			List<ActivityItemLogic> activityItems = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.ActivityItems;
			for (int i = 0; i < activityItems.Count; i++)
			{
				if (activityItems[i].curActivityInfo.Type == 7L)
				{
					GameObject gameObject = activityItems[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SURVIVAL_BATTLE_CHOOSE_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, activityItems[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00040A60 File Offset: 0x0003EC60
	public static void Show_SURVIVAL_BATTLE_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyActivityUIRootLogic>.Instance.startBtnSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00040ADC File Offset: 0x0003ECDC
	public static void Show_SURVIVAL_BATTLE_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00040AEC File Offset: 0x0003ECEC
	public static void Show_BADGE_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(true);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.BagFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.BagFuncBtn.IconSp;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(iconSp);
				TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height, StrDictionary.GetDictionaryString("#{600031}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00040BB8 File Offset: 0x0003EDB8
	public static void Show_BADGE_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<PlayerInfoMenuRootLogic>.Instance.CurTapType != GAME_MENU_TAP_TYPE.BADGE_BACKPACK_TAB)
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget btnIconSprite = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnIconSprite;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(btnIconSprite);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnWidget);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[3].BtnNameLabel);
			TutorialUIRootLogic.ShowWindow(btnIconSprite.gameObject, list, btnIconSprite.width + 50, btnIconSprite.height, StrDictionary.GetDictionaryString("#{600032}", new object[0]), SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00040C90 File Offset: 0x0003EE90
	public static void Show_BADGE_CLICK_ITEM()
	{
		FunctionTipsRootLogic.ClearHandTip();
		ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.BADGE_BACKPACK);
		ItemContainer itemContainer2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.BADGE_EQUIPPACK);
		if (itemContainer.GetItemCount() > 0 && !itemContainer2.IsFull())
		{
			SingletonUnity<BackPackPageRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			BackPackRootLogic backPackRoot = SingletonUnity<BackPackPageRootLogic>.Instance.BackPackRoot;
			UIWidget rootWidget = backPackRoot.ItemLineList[0].ItemObjList[0].RootWidget;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(rootWidget);
			TutorialUIRootLogic.ShowWindow(rootWidget.gameObject, list, rootWidget.width, rootWidget.height, StrDictionary.GetDictionaryString("#{600033}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, true, true, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00040D64 File Offset: 0x0003EF64
	public static void Show_BADGE_CLICK_EQUIP()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<ItemInfoRootLogicNew>.Instance.CurItem.ItemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			SingletonUnity<ItemInfoRootLogicNew>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget uiwidget = SingletonUnity<ItemInfoRootLogicNew>.Instance.BtnSpList[0];
			List<UIWidget> list = new List<UIWidget>();
			list.Add(uiwidget);
			TutorialUIRootLogic.ShowWindow(uiwidget.gameObject, list, uiwidget.width, uiwidget.height, StrDictionary.GetDictionaryString("#{600034}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, true, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00040DF8 File Offset: 0x0003EFF8
	public static void Show_BADGE_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00040E08 File Offset: 0x0003F008
	public static void Show_TITLE_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(false);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				GameObject gameObject = SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TITLE_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00040EB8 File Offset: 0x0003F0B8
	public static void Show_TITLE_CLICK_TAP()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<EquipStrengthenUIRootLogic>.Instance.CurPageType != EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.TITLE)
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget btnIconSprite = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].BtnIconSprite;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(btnIconSprite);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].BtnWidget);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].BtnNameLabel);
			TutorialUIRootLogic.ShowWindow(btnIconSprite.gameObject, list, btnIconSprite.width + 50, btnIconSprite.height, StrDictionary.GetDictionaryString("#{600074}", new object[0]), SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00040F90 File Offset: 0x0003F190
	public static void Show_TITLE_CLICK()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<JSShengWangLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<JSShengWangLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<JSShengWangLogic>.Instance.promoteBtnsp.gameObject;
			if (UnityVersionUtil.IsActive(gameObject))
			{
				SingletonUnity<JSShengWangLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TITLE_CLICK, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00041028 File Offset: 0x0003F228
	public static void Show_TITLE_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00041038 File Offset: 0x0003F238
	public static void Show_RANK_PVP_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.RANK_PVP_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000410BC File Offset: 0x0003F2BC
	public static void Show_RANK_PVP_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 4)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.RANK_PVP_CLICK_TAB, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[4].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00041164 File Offset: 0x0003F364
	public static void Show_RANK_PVP_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonUnity<RankPVPUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00041184 File Offset: 0x0003F384
	public static void Show_RANK_PVP_CHOOSE()
	{
		FunctionTipsRootLogic.ClearHandTip();
		SingletonUnity<RankPVPUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		GameObject targetObj = SingletonUnity<RankPVPUIRootLogic>.Instance.FightBtnList[2];
		FunctionTipsRootLogic.AddFunctionHandTips(targetObj, Vector3.zero, TUTORIAL_STEP.RANK_PVP_CHOOSE, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x000411DC File Offset: 0x0003F3DC
	public static void Show_RANK_PVP_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x000411EC File Offset: 0x0003F3EC
	public static void Show_SCUFFLE_COPY_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.ClickTipsSprite.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SCUFFLE_COPY_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00041270 File Offset: 0x0003F470
	public static void Show_SCUFFLE_COPY_CLICK_DAILY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 2)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SCUFFLE_COPY_CLICK_DAILY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00041318 File Offset: 0x0003F518
	public static void Show_SCUFFLE_COPY_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00041360 File Offset: 0x0003F560
	public static void Show_SCUFFLE_COPY_CLICK_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			List<DailyCopyLineLogic> dailyCopyLineList = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.DailyCopyLineList;
			for (int i = 0; i < dailyCopyLineList.Count; i++)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(dailyCopyLineList[i].Key);
				if (copySceneDataById.SubType == 20)
				{
					GameObject gameObject = dailyCopyLineList[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SCUFFLE_COPY_CLICK_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, dailyCopyLineList[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00041438 File Offset: 0x0003F638
	public static void Show_SCUFFLE_COPY_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.AcceptBtn.gameObject;
			if (UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.StartBtnPic.gameObject))
			{
				gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.StartBtnPic.gameObject;
			}
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x000414E8 File Offset: 0x0003F6E8
	public static void Show_SCUFFLE_COPY_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x000414F8 File Offset: 0x0003F6F8
	public static void Show_TOWER_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TOWER_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0004157C File Offset: 0x0003F77C
	public static void Show_TOWER_CLICK_DAILY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 2)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TOWER_CLICK_DAILY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00041624 File Offset: 0x0003F824
	public static void Show_TOWER_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0004166C File Offset: 0x0003F86C
	public static void Show_TOWER_CLICK_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.TowerLineItem.IconSprite.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TOWER_CLICK_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, SingletonUnity<NewDailyCopyUIRootLogic>.Instance.TowerLineItem.transform.parent.parent, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00041700 File Offset: 0x0003F900
	public static void Show_TOWER_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.StartBtnPic.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TOWER_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00041784 File Offset: 0x0003F984
	public static void Show_TOWER_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00041794 File Offset: 0x0003F994
	public static void Show_GOLD_COPY_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GOLD_COPY_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00041818 File Offset: 0x0003FA18
	public static void Show_GOLD_COPY_CLICK_DAILY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 2)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GOLD_COPY_CLICK_DAILY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x000418C0 File Offset: 0x0003FAC0
	public static void Show_GOLD_COPY_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00041908 File Offset: 0x0003FB08
	public static void Show_GOLD_COPY_CLICK_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			List<DailyCopyLineLogic> dailyCopyLineList = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.DailyCopyLineList;
			for (int i = 0; i < dailyCopyLineList.Count; i++)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(dailyCopyLineList[i].Key);
				if (copySceneDataById.SubType == 11)
				{
					GameObject gameObject = dailyCopyLineList[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GOLD_COPY_CLICK_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, dailyCopyLineList[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x000419E0 File Offset: 0x0003FBE0
	public static void Show_GOLD_COPY_SHOW_REWARD()
	{
		UIWidget iconSprite = SingletonUnity<DailyCopyUIRootLogic>.Instance.ShowRewardItemsScripts.rewardItmes[0].iconSprite;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(iconSprite);
		TutorialUIRootLogic.ShowWindow(iconSprite.gameObject, list, iconSprite.width, iconSprite.height, StrDictionary.GetClientDictionaryString("#{600036}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, true, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00041A68 File Offset: 0x0003FC68
	public static void Show_GOLD_COPY_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.StartBtnPic.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.GOLD_COPY_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00041AE4 File Offset: 0x0003FCE4
	public static void Show_GOLD_COPY_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00041AF4 File Offset: 0x0003FCF4
	public static void Show_CAR_COPY_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.ClickTipsSprite.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAR_COPY_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00041B78 File Offset: 0x0003FD78
	public static void Show_CAR_COPY_CLICK_DAILY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 2)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAR_COPY_CLICK_DAILY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00041C20 File Offset: 0x0003FE20
	public static void Show_CAR_COPY_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00041C68 File Offset: 0x0003FE68
	public static void Show_CAR_COPY_CLICK_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			List<DailyCopyLineLogic> dailyCopyLineList = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.DailyCopyLineList;
			for (int i = 0; i < dailyCopyLineList.Count; i++)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(dailyCopyLineList[i].Key);
				if (copySceneDataById.SubType == 7)
				{
					GameObject gameObject = dailyCopyLineList[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAR_COPY_CLICK_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, dailyCopyLineList[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00041D40 File Offset: 0x0003FF40
	public static void Show_CAR_COPY_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.StartBtnPic.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.CAR_COPY_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00041DBC File Offset: 0x0003FFBC
	public static void Show_CAR_COPY_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00041DCC File Offset: 0x0003FFCC
	public static void Show_EXP_COPY_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			GameObject gameObject = SingletonUnity<MiniMap>.Instance.TutorialClickSp.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EXP_COPY_START, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00041E50 File Offset: 0x00040050
	public static void Show_EXP_COPY_CLICK_DAILY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			if (SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex != 2)
			{
				GameObject gameObject = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].BtnIconSprite.gameObject;
				FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EXP_COPY_CLICK_DAILY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
				SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[2].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00041EF8 File Offset: 0x000400F8
	public static void Show_EXP_COPY_WAIT_DATA()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00041F40 File Offset: 0x00040140
	public static void Show_EXP_COPY_CLICK_COPY()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			List<DailyCopyLineLogic> dailyCopyLineList = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.DailyCopyLineList;
			for (int i = 0; i < dailyCopyLineList.Count; i++)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(dailyCopyLineList[i].Key);
				if (copySceneDataById.SubType == 12)
				{
					GameObject gameObject = dailyCopyLineList[i].IconSprite.gameObject;
					FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EXP_COPY_CLICK_COPY, false, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, dailyCopyLineList[i].transform.parent.parent, false, true);
					SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					break;
				}
			}
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00042018 File Offset: 0x00040218
	public static void Show_EXP_COPY_CLICK_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			GameObject gameObject = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ActivityInfoPage.SingleBtn.gameObject;
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.EXP_COPY_CLICK_START, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		}
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00042094 File Offset: 0x00040294
	public static void Show_EXP_COPY_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x000420A4 File Offset: 0x000402A4
	public static void Show_CREATE_TEAM_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x000420B4 File Offset: 0x000402B4
	public static void Show_CREATE_TEAM_CREATE()
	{
		TutorialManager.IsMatchTeamTutorial = false;
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
		{
			MissionTeamTipLogic instance = SingletonUnity<MissionTeamTipLogic>.Instance;
			if (instance.CurPage == 1)
			{
				TutorialManager.CloseTutorial();
			}
			else if (instance.CurPage == 2)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
				{
					TutorialManager.CloseTutorial();
				}
				else
				{
					SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CREATE_TEAM_TUTORIAL);
					instance.TeamTipRoot.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
					UIWidget createTeamBtn = instance.TeamTipRoot.CreateTeamBtn;
					List<UIWidget> list = new List<UIWidget>();
					list.Add(createTeamBtn);
					TutorialUIRootLogic.ShowWindow(createTeamBtn.gameObject, list, 70, 150, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
				}
			}
		}
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00042194 File Offset: 0x00040394
	public static void Show_CREATE_TEAM_CHOOSE_COPY()
	{
		CreateTeamRootLogic instance = SingletonUnity<CreateTeamRootLogic>.Instance;
		for (int i = 0; i < instance.TeamTargetTabRoot.TargetTabLineList.Count; i++)
		{
			TeamData teamDataDataByID = DataManager.GetTeamDataDataByID(instance.TeamTargetTabRoot.TargetTabLineList[i].Key);
			if (teamDataDataByID.GoalType == 1)
			{
				instance.TeamTargetTabRoot.TargetTabLineList[i].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget bottomPic = instance.TeamTargetTabRoot.TargetTabLineList[i].BottomPic;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(bottomPic);
				TutorialUIRootLogic.ShowWindow(instance.TeamTargetTabRoot.TargetTabLineList[i].TitleLabel.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600040}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, true, true, true);
				break;
			}
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0004227C File Offset: 0x0004047C
	public static void Show_CREATE_TEAM_CREATE_BTN()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<CreateTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CreateTeamRootLogic>.Instance.gameObject))
		{
			SingletonUnity<CreateTeamRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget component = SingletonUnity<CreateTeamRootLogic>.Instance.CreateBtnRoot.GetComponent<UISprite>();
			List<UIWidget> list = new List<UIWidget>();
			list.Add(component);
			TutorialUIRootLogic.ShowWindow(component.gameObject, list, component.width, component.height, StrDictionary.GetClientDictionaryString("#{600041}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
		}
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00042310 File Offset: 0x00040510
	public static void Show_CREATE_TEAM_WAIT()
	{
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00042314 File Offset: 0x00040514
	public static void Show_CREATE_TEAM_SHOW_INVITE()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CREATE_TEAM_TUTORIAL);
		UIWidget emptyPlusPic = SingletonUnity<TeamUIRootNewLogic>.Instance.PlayerPicLogicList[1].EmptyPlusPic;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(emptyPlusPic.gameObject, showObjList, emptyPlusPic.width, emptyPlusPic.height, StrDictionary.GetClientDictionaryString("#{600042}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x000423A4 File Offset: 0x000405A4
	public static void Show_CREATE_TEAM_SHOW_SHOUT()
	{
		UIWidget shoutRootPic = SingletonUnity<TeamUIRootNewLogic>.Instance.ShoutRootPic;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(shoutRootPic.gameObject, showObjList, shoutRootPic.width, shoutRootPic.height, StrDictionary.GetClientDictionaryString("#{600043}", new object[0]), SCREEN_DIRECTION.TOP, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00042418 File Offset: 0x00040618
	public static void Show_CREATE_TEAM_SHOW_APPLICATION()
	{
		GameObject applyBtnRoot = SingletonUnity<TeamUIRootNewLogic>.Instance.ApplyBtnRoot;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(applyBtnRoot, showObjList, 100, 50, StrDictionary.GetClientDictionaryString("#{600044}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0004247C File Offset: 0x0004067C
	public static void Show_CREATE_TEAM_SHOW_START()
	{
		GameObject startBtnRoot = SingletonUnity<TeamUIRootNewLogic>.Instance.StartBtnRoot;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(startBtnRoot, showObjList, 100, 50, StrDictionary.GetClientDictionaryString("#{600045}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000424E0 File Offset: 0x000406E0
	public static void Show_CREATE_TEAM_SHOW_LEAVE()
	{
		GameObject gameObject = SingletonUnity<TeamUIRootNewLogic>.Instance.LeaveBtnPic.gameObject;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(gameObject, showObjList, 100, 50, StrDictionary.GetClientDictionaryString("#{600046}", new object[0]), SCREEN_DIRECTION.BOTTOM_LEFT, true, true, false, true, true);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0004254C File Offset: 0x0004074C
	public static void Show_CREATE_TEAM_SHOW_FINISH()
	{
		UIWidget emptyPlusPic = SingletonUnity<TeamUIRootNewLogic>.Instance.PlayerPicLogicList[1].EmptyPlusPic;
		List<UIWidget> showObjList = new List<UIWidget>();
		TutorialUIRootLogic.ShowWindow(emptyPlusPic.gameObject, showObjList, emptyPlusPic.width, emptyPlusPic.height, StrDictionary.GetClientDictionaryString("#{600047}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, false, false);
		if (SingletonUnity<TutorialUIRootLogic>.Exists)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext), -1f);
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x000425C8 File Offset: 0x000407C8
	public static void Show_CREATE_TEAM_CLICK_BACK()
	{
		if (TutorialManager.IsMatchTeamTutorial)
		{
			FunctionTipsRootLogic.ClearHandTip();
			TutorialManager.CloseTutorial();
		}
		else
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(exitBtn);
			TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, StrDictionary.GetDictionaryString("#{600023}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, true, true);
		}
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00042650 File Offset: 0x00040850
	public static void Show_CREATE_TEAM_SWITCH_MISSION()
	{
		if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
		{
			MissionTeamTipLogic instance = SingletonUnity<MissionTeamTipLogic>.Instance;
			if (instance.CurPage == 2)
			{
				instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget missionBtn = instance.MissionBtn;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(missionBtn);
				TutorialUIRootLogic.ShowWindow(missionBtn.gameObject, list, 70, 150, StrDictionary.GetClientDictionaryString("#{600065}", new object[0]), SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
			}
			else
			{
				TutorialManager.MoveNext(false);
			}
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x000426E8 File Offset: 0x000408E8
	public static void Show_CREATE_TEAM_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x000426F8 File Offset: 0x000408F8
	public static void Show_SKILL_DRAG_START()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(true);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.SkillFuncBtn.gameObject))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.SKILL_DRAG);
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.SkillFuncBtn.IconSp;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(iconSp);
				TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height, StrDictionary.GetDictionaryString("#{100167}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x000427D0 File Offset: 0x000409D0
	public static void Show_SKILL_DRAG_MOVE()
	{
		if (SingletonUnity<SkillInfoRootLogic>.Exists)
		{
			SkillInfoRootLogic instance = SingletonUnity<SkillInfoRootLogic>.Instance;
			instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget skillIconSprite = instance.SkillBtnLogicList[0].SkillIconSprite;
			for (int i = 0; i < instance.SkillBtnLogicList.Count; i++)
			{
				if (instance.SkillBtnLogicList[i].CharacterSkillData != null && !instance.SkillBtnLogicList[i].CharacterSkillData.IsDisable && instance.SkillBtnLogicList[i].CharacterSkillData.Index >= 10)
				{
					skillIconSprite = instance.SkillBtnLogicList[i].SkillIconSprite;
					break;
				}
			}
			TutorialUIRootLogic.ShowWindow(skillIconSprite.gameObject, skillIconSprite.width, skillIconSprite.height, string.Empty, SCREEN_DIRECTION.RIGHT, -1f, false, false, true, UIWidget.Pivot.Center);
			UIWidget skillIconSprite2 = instance.SkillBtnUseLogicList[3].SkillIconSprite;
			for (int j = 0; j < instance.SkillBtnUseLogicList.Count; j++)
			{
				if (instance.SkillBtnUseLogicList[j].CharacterSkillData == null || instance.SkillBtnUseLogicList[j].CharacterSkillData.IsDisable)
				{
					skillIconSprite2 = instance.SkillBtnUseLogicList[j].SkillIconSprite;
					break;
				}
			}
			TweenScale handTipTweenS = SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS;
			handTipTweenS.enabled = false;
			TutorialManager.mTutorialTweener = TweenSettingsExtensions.OnComplete<Tweener>(ShortcutExtensions.DOMove(handTipTweenS.transform, skillIconSprite2.transform.position, 1f, false), delegate()
			{
				TutorialManager.MoveNext(false);
			});
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00042998 File Offset: 0x00040B98
	public static void Show_SKILL_DRAG_EXIT()
	{
		TweenExtensions.Kill(TutorialManager.mTutorialTweener, false);
		SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS.transform.localPosition = Vector3.zero;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.SKILL_DRAG);
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x000429E0 File Offset: 0x00040BE0
	public static void Show_TOWER_SPECIAL_TIPS()
	{
		SingletonUnity<TowerUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		GameObject gameObject = SingletonUnity<TowerUIRootLogic>.Instance.SpecialRewardBtn.gameObject;
		FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.TOWER_SPECIAL_TIPS, true, string.Empty, SCREEN_DIRECTION.BOTTOM, Vector3.zero, TutorialManager.TutorialTipDuration, null, false, true);
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00042A34 File Offset: 0x00040C34
	public static void Show_TOWER_SPECIAL_TIPS_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00042A44 File Offset: 0x00040C44
	public static void Show_CAR_START()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(true);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.CarFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.CarFuncBtn.IconSp;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(iconSp);
				TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height, StrDictionary.GetDictionaryString("#{100165}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00042B08 File Offset: 0x00040D08
	public static void Show_CAR_CLICK_ACQUIRE()
	{
		if (SingletonUnity<PlayerCarRootLogic>.Instance.CurMountInfoDic["1001"].state != 0L)
		{
			TutorialManager.CloseTutorial();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR);
			return;
		}
		UIWidget getBtn = SingletonUnity<PlayerCarRootLogic>.Instance.GetBtn;
		SingletonUnity<PlayerCarRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		List<UIWidget> list = new List<UIWidget>();
		list.Add(getBtn);
		TutorialUIRootLogic.ShowWindow(getBtn.gameObject, list, getBtn.width, getBtn.height, StrDictionary.GetDictionaryString("#{600013}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00042BAC File Offset: 0x00040DAC
	public static void Show_CAR_CLICK_GET()
	{
		if (!SingletonUnity<PlayerCarRootLogic>.Instance.IsBackPackHaveCarTicket())
		{
			TutorialManager.CloseTutorial();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR);
			return;
		}
		SingletonUnity<PlayerCarRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget mMessageBoxOkButtonObj = SingletonUnity<MessageBoxLogic>.Instance.mMessageBoxOkButtonObj;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(mMessageBoxOkButtonObj);
		TutorialUIRootLogic.ShowWindow(mMessageBoxOkButtonObj.gameObject, list, mMessageBoxOkButtonObj.width, mMessageBoxOkButtonObj.height, StrDictionary.GetDictionaryString("#{600014}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, true, true);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00042C40 File Offset: 0x00040E40
	public static void Show_CAR_CLICK_OK()
	{
		SingletonUnity<GetCarUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget yesBtn = SingletonUnity<GetCarUIRootLogic>.Instance.YesBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(yesBtn);
		TutorialUIRootLogic.ShowWindow(yesBtn.gameObject, list, yesBtn.width, yesBtn.height, StrDictionary.GetDictionaryString("#{600015}", new object[0]), SCREEN_DIRECTION.BOTTOM, true, true, false, true, true);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00042CAC File Offset: 0x00040EAC
	public static void Show_CAR_CLICK_EQUIP()
	{
		if (SingletonUnity<PlayerCarRootLogic>.Instance.CurMountInfoDic["1001"].state != 1L)
		{
			TutorialManager.CloseTutorial();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR);
			return;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR);
		SingletonUnity<PlayerCarRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget equipBtn = SingletonUnity<PlayerCarRootLogic>.Instance.EquipBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(equipBtn);
		TutorialUIRootLogic.ShowWindow(equipBtn.gameObject, list, equipBtn.width, equipBtn.height, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00042D58 File Offset: 0x00040F58
	public static void Show_CAR_CLICK_BACK()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(exitBtn);
		TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00042DBC File Offset: 0x00040FBC
	public static void Show_CAR_CLICK_MOUNT()
	{
		if (string.IsNullOrEmpty(Singleton<ObjManager>.Instance.MainPlayer.MountId))
		{
			TutorialManager.CloseTutorial();
			return;
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject) && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.MountCarBtn.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget mountCarBtn = SingletonUnity<FunctionBtnRootLogic>.Instance.MountCarBtn;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(mountCarBtn);
			TutorialUIRootLogic.ShowWindow(mountCarBtn.gameObject, list, mountCarBtn.width, mountCarBtn.height, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00042E70 File Offset: 0x00041070
	public static void Show_CAR_SHOW_NEXT()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR);
		if (GameManager.IsSupportCurDataVersion())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TipCarUIRoot, delegate
			{
				SingletonUnity<TipCarUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				SingletonUnity<TipCarUIRootLogic>.Instance.Reset("1002", "#{605515}");
			}, null);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00042ED4 File Offset: 0x000410D4
	public static void Show_CAR_GIFT_CLICK_BACK()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(exitBtn);
		TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, StrDictionary.GetDictionaryString("#{600030}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, false, true, true);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00042F40 File Offset: 0x00041140
	public static void Show_ENHANCE_START()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.IsMissionCompleted("40003"))
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.ENHANCE_EQUIP);
			TutorialManager.CloseTutorial();
			return;
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(true);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.ENHANCE_EQUIP);
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.IconSp;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(iconSp);
				TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height, StrDictionary.GetDictionaryString("#{600027}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0004304C File Offset: 0x0004124C
	public static void Show_ENHANCE_CLICK_TAB()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<EquipStrengthenUIRootLogic>.Instance.CurPageType != EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.ENHANCE)
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget btnIconSprite = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnIconSprite;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(btnIconSprite);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnWidget);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[1].BtnNameLabel);
			TutorialUIRootLogic.ShowWindow(btnIconSprite.gameObject, list, btnIconSprite.width + 50, btnIconSprite.height, StrDictionary.GetDictionaryString("#{600073}", new object[0]), SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00043124 File Offset: 0x00041324
	public static void Show_ENHANCE_CLICK()
	{
		if (SingletonUnity<EnhanceUIRootLogic>.Instance.CurItem == null || SingletonUnity<EnhanceUIRootLogic>.Instance.CurItem.IsEmpty())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		SingletonUnity<EnhanceUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget upLevelBtn = SingletonUnity<EnhanceUIRootLogic>.Instance.UpLevelBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(upLevelBtn);
		TutorialUIRootLogic.ShowWindow(upLevelBtn.gameObject, list, upLevelBtn.width, upLevelBtn.height, StrDictionary.GetDictionaryString("#{600028}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000431B8 File Offset: 0x000413B8
	public static void Show_ENHANCE_CLICK_ALL()
	{
		if (SingletonUnity<EnhanceUIRootLogic>.Instance.CurItem == null || SingletonUnity<EnhanceUIRootLogic>.Instance.CurItem.IsEmpty())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		SingletonUnity<EnhanceUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget upLevelAllBtn = SingletonUnity<EnhanceUIRootLogic>.Instance.UpLevelAllBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(upLevelAllBtn);
		TutorialUIRootLogic.ShowWindow(upLevelAllBtn.gameObject, list, upLevelAllBtn.width, upLevelAllBtn.height, StrDictionary.GetDictionaryString("#{600029}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0004324C File Offset: 0x0004144C
	public static void Show_ENHANCE_EXIT()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(exitBtn);
		TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, StrDictionary.GetDictionaryString("#{600030}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, true, true);
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.ENHANCE);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x000432CC File Offset: 0x000414CC
	public static void Show_ENHANCE_ALL_START()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(true);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.IconSp;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(iconSp);
				TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height, StrDictionary.GetDictionaryString("#{100164}", new object[0]), SCREEN_DIRECTION.LEFT, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00043390 File Offset: 0x00041590
	public static void Show_ENHANCE_ALL_CLICK()
	{
		if (SingletonUnity<EnhanceUIRootLogic>.Instance.CurItem == null || SingletonUnity<EnhanceUIRootLogic>.Instance.CurItem.IsEmpty())
		{
			TutorialManager.CloseTutorial();
			return;
		}
		SingletonUnity<EnhanceUIRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget upLevelAllBtn = SingletonUnity<EnhanceUIRootLogic>.Instance.UpLevelAllBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(upLevelAllBtn);
		TutorialUIRootLogic.ShowWindow(upLevelAllBtn.gameObject, list, upLevelAllBtn.width, upLevelAllBtn.height, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, false, true, true);
		CurMission curMissionByClassType = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.MAIN);
		if (curMissionByClassType != null)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionByClassType.MissionId);
			if (missionDataByID.TrigerType != MISSION_TRIGGER_TUTORIAL_TYPE.INVALID)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish((FUNCTION_TYPE)missionDataByID.TriggerTutorialType);
			}
		}
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0004345C File Offset: 0x0004165C
	public static void Show_ENHANCE_ALL_EXIT()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(exitBtn);
		TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, string.Empty, SCREEN_DIRECTION.RIGHT, true, true, false, true, true);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x000434C0 File Offset: 0x000416C0
	public static void Show_SKILL_UPGRADE_START()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(true);
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget iconSp = SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.IconSp;
				List<UIWidget> list = new List<UIWidget>();
				list.Add(iconSp);
				TutorialUIRootLogic.ShowWindow(iconSp.gameObject, list, iconSp.width, iconSp.height, StrDictionary.GetDictionaryString("#{600020}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.SKILL);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00043598 File Offset: 0x00041798
	public static void Show_SKILL_UPGRADE_CLICK_SKILL()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<EquipStrengthenUIRootLogic>.Instance.CurPageType != EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.SKILL)
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[0].RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget btnIconSprite = SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[0].BtnIconSprite;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(btnIconSprite);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[0].BtnWidget);
			list.Add(SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[0].BtnNameLabel);
			TutorialUIRootLogic.ShowWindow(btnIconSprite.gameObject, list, btnIconSprite.width + 50, btnIconSprite.height, StrDictionary.GetDictionaryString("#{600072}", new object[0]), SCREEN_DIRECTION.RIGHT, true, true, true, true, true);
		}
		else
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00043670 File Offset: 0x00041870
	public static void Show_SKILL_UPGRADE_CLICK()
	{
		if (UnityVersionUtil.IsActive(SingletonUnity<SkillInfoRootLogic>.Instance.UpgradeBtnRoot.gameObject))
		{
			SingletonUnity<SkillInfoRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget upgradeBtnRoot = SingletonUnity<SkillInfoRootLogic>.Instance.UpgradeBtnRoot;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(upgradeBtnRoot);
			TutorialUIRootLogic.ShowWindow(upgradeBtnRoot.gameObject, list, upgradeBtnRoot.width, upgradeBtnRoot.height, StrDictionary.GetDictionaryString("#{600021}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x000436FC File Offset: 0x000418FC
	public static void Show_SKILL_UPGRADE_CLICK_ALL()
	{
		if (UnityVersionUtil.IsActive(SingletonUnity<SkillInfoRootLogic>.Instance.UpgradeAllBtnRoot.gameObject))
		{
			SingletonUnity<SkillInfoRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget upgradeAllBtnRoot = SingletonUnity<SkillInfoRootLogic>.Instance.UpgradeAllBtnRoot;
			List<UIWidget> list = new List<UIWidget>();
			list.Add(upgradeAllBtnRoot);
			TutorialUIRootLogic.ShowWindow(upgradeAllBtnRoot.gameObject, list, upgradeAllBtnRoot.width, upgradeAllBtnRoot.height, StrDictionary.GetDictionaryString("#{600022}", new object[0]), SCREEN_DIRECTION.TOP, true, true, true, true, true);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.SKILL);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0004379C File Offset: 0x0004199C
	public static void Show_SKILL_UPGRADE_EXIT()
	{
		SingletonUnity<MenuBaseRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget exitBtn = SingletonUnity<MenuBaseRootLogic>.Instance.ExitBtn;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(exitBtn);
		TutorialUIRootLogic.ShowWindow(exitBtn.gameObject, list, exitBtn.width, exitBtn.height + 100, StrDictionary.GetDictionaryString("#{600023}", new object[0]), SCREEN_DIRECTION.BOTTOM_RIGHT, true, true, true, true, true);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00043808 File Offset: 0x00041A08
	public static void Show_MAIN_MISSION_PHONE_START()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.MAIN_MISSION);
		SingletonUnity<MissionTeamTipLogic>.Instance.SetToCloseState();
		GameObject gameObject = SingletonUnity<MissionTeamTipLogic>.Instance.TwHideBtnPic.gameObject;
		if (gameObject != null)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget component = gameObject.gameObject.GetComponent<UIWidget>();
			TutorialUIRootLogic.ShowWindow(gameObject.gameObject, component.width, component.height, null, SCREEN_DIRECTION.RIGHT, -1f, true, true, true, component.pivot);
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00043898 File Offset: 0x00041A98
	public static void Show_MAIN_MISSION_CLICK_MISSION_TAB()
	{
		GameObject gameObject = SingletonUnity<MissionTeamTipLogic>.Instance.MissionBtn.gameObject;
		if (gameObject != null)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget component = gameObject.gameObject.GetComponent<UIWidget>();
			TutorialUIRootLogic.ShowWindow(gameObject.gameObject, component.width, component.height, null, SCREEN_DIRECTION.RIGHT, -1f, true, true, true, component.pivot);
		}
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0004390C File Offset: 0x00041B0C
	public static void Show_MAIN_MISSION_CLICK_MISSION()
	{
		MissionTipLineLogic targetMissionLine = SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.GetTargetMissionLine(1);
		if (targetMissionLine != null)
		{
			targetMissionLine.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget component = targetMissionLine.gameObject.GetComponent<UIWidget>();
			TutorialUIRootLogic.ShowWindow(targetMissionLine.gameObject, component.width, component.height, StrDictionary.GetDictionaryString("#{100162}", new object[0]), SCREEN_DIRECTION.RIGHT, -1f, true, true, true, component.pivot);
		}
		else
		{
			Debug.Log("Show_MAIN_MISSION_START Main Mission Line Is Null!!!!!!!!!!!!!!!!!!");
		}
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0004399C File Offset: 0x00041B9C
	public static void Show_MAIN_MISSION_PHONE_FINISH()
	{
		TutorialManager.MoveNext(false);
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x000439B4 File Offset: 0x00041BB4
	public static void Show_MAIN_MISSION_START()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.MAIN_MISSION);
		SingletonUnity<MissionTeamTipLogic>.Instance.ShowMissionTip();
		MissionTipLineLogic targetMissionLine = SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.GetTargetMissionLine(1);
		if (targetMissionLine != null)
		{
			targetMissionLine.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			UIWidget component = targetMissionLine.gameObject.GetComponent<UIWidget>();
			TutorialUIRootLogic.ShowWindow(targetMissionLine.gameObject, component.width, component.height, StrDictionary.GetDictionaryString("#{100162}", new object[0]), SCREEN_DIRECTION.RIGHT, -1f, true, true, true, component.pivot);
		}
		else
		{
			Debug.Log("Show_MAIN_MISSION_START Main Mission Line Is Null!!!!!!!!!!!!!!!!!!");
		}
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00043A5C File Offset: 0x00041C5C
	public static void Show_MAIN_MISSION_FINISH()
	{
		TutorialManager.MoveNext(false);
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00043A74 File Offset: 0x00041C74
	public static void Show_MAIN_MISSION_TIP_START()
	{
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<MissionTeamTipLogic>.Exists)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.ShowMissionTip();
		}
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00043A98 File Offset: 0x00041C98
	public static void Show_MAIN_MISSION_TIP_FINISH()
	{
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00043AA0 File Offset: 0x00041CA0
	public static void Show_SWITCH_VIEW()
	{
		SingletonUnity<ExpLineRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UIWidget viewBtnPic = SingletonUnity<ExpLineRootLogic>.Instance.ViewBtnPic;
		List<UIWidget> list = new List<UIWidget>();
		list.Add(viewBtnPic);
		TutorialUIRootLogic.ShowWindow(viewBtnPic.gameObject, list, viewBtnPic.width, viewBtnPic.height, string.Format("You Can Press Here To Change View", new object[0]), SCREEN_DIRECTION.RIGHT, false, true, true, true, true);
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00043B0C File Offset: 0x00041D0C
	public static void Show_MOVE_SCREEN()
	{
		SingletonUnity<ScreenBottomBtn>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		TutorialUIRootLogic.ShowWindow(SingletonUnity<ScreenBottomBtn>.Instance.gameObject, 180, 200, StrDictionary.GetDictionaryString("#{600009}", new object[0]), SCREEN_DIRECTION.BOTTOM, -1f, false, false, true, UIWidget.Pivot.Center);
		TweenScale handTipTweenS = SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS;
		handTipTweenS.enabled = false;
		handTipTweenS.transform.localPosition = new Vector3(-200f, 150f, 0f);
		TutorialManager.mTutorialTweener = TweenSettingsExtensions.SetLoops<Tweener>(ShortcutExtensions.DOLocalMove(handTipTweenS.transform, handTipTweenS.transform.localPosition + new Vector3(400f, 0f, 0f), 2f, false), int.MaxValue);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "1MOVE_SCREEN");
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00043C00 File Offset: 0x00041E00
	public static void Show_JOYSTICK_START()
	{
		(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager).SetPlayerMoveTarget();
		TweenExtensions.Kill(TutorialManager.mTutorialTweener, false);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		TutorialUIRootLogic.ShowWindow(SingletonUnity<JoyStickLogic>.Instance.gameObject, 180, 200, StrDictionary.GetDictionaryString("#{600001}", new object[0]), SCREEN_DIRECTION.RIGHT, -1f, false, false, true, UIWidget.Pivot.Center);
		TweenScale handTipTweenS = SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS;
		handTipTweenS.enabled = false;
		TutorialManager.mTutorialTweener = TweenSettingsExtensions.SetLoops<Tweener>(ShortcutExtensions.DOMove(handTipTweenS.transform, handTipTweenS.transform.position + new Vector3(0.2f, 0.2f, 0f), 1f, false), int.MaxValue);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "2JOYSTICK");
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00043CE0 File Offset: 0x00041EE0
	public static void Show_CLICK_NPC()
	{
		TweenExtensions.Kill(TutorialManager.mTutorialTweener, false);
		SingletonUnity<ScreenBottomBtn>.Instance.LockBtn = true;
		CameraController mCamCtl = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
		mCamCtl.IsCamCanUse = false;
		mCamCtl.IdealPitch = 15f;
		mCamCtl.smoothOrbitSpeed = 5f;
		Transform talkNpcPos = (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager).TalkNpcPos;
		mCamCtl.IdealYaw = MathUtil.Heading(talkNpcPos.position - Singleton<ObjManager>.Instance.MainPlayer.Position);
		vp_Timer.In(0.5f, delegate()
		{
			Vector3 vector = Camera.main.WorldToViewportPoint(talkNpcPos.position + Vector3.up * 2f);
			float num = (float)Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
			Vector3 vector2;
			vector2..ctor(vector.x * num - num / 2f, vector.y * 480f - 240f, 0f);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
			mCamCtl.enabled = false;
			FunctionTipsRootLogic.AddFunctionHandTips(talkNpcPos.gameObject, Vector3.zero, TUTORIAL_STEP.CLICK_NPC, false, StrDictionary.GetDictionaryString("#{600002}", new object[0]), SCREEN_DIRECTION.BOTTOM, Vector3.zero, float.MaxValue, null, true, true);
			SingletonUnity<ScreenBottomBtn>.Instance.LockBtn = false;
			NewTutorialSceneManager newTutorialSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager;
			SingletonUnity<InputController>.Instance.TapRecognizer.OnGesture += new GestureRecognizerTS<TapGesture>.GestureEventHandler(newTutorialSceneManager.OnClickTalkNpc);
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "3CLICK_NPC");
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00043DC4 File Offset: 0x00041FC4
	public static void Show_CLICK_NPC_FINISH()
	{
		Singleton<ObjManager>.Instance.MainPlayer.CameraController.enabled = true;
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "4CLICK_NPC_FINISH");
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00043E0C File Offset: 0x0004200C
	public static void Show_NORMAL_ATTACK_BUTTON()
	{
		SingletonUnity<JueseJiNengQuLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		GameObject gameObject = SingletonUnity<JueseJiNengQuLogic>.Instance.SkillBtnRoot[0].gameObject;
		FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.NORMAL_ATTACK_BUTTON, true, StrDictionary.GetDictionaryString("#{600003}", new object[0]), SCREEN_DIRECTION.LEFT, Vector3.zero, -1f, null, false, true);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00043E6C File Offset: 0x0004206C
	public static void Show_NORMAL_ATTACK_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00043E7C File Offset: 0x0004207C
	public static void Show_MOVETO_CAR_POINT()
	{
		(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager).SetPlayerMoveTarget();
		TweenExtensions.Kill(TutorialManager.mTutorialTweener, false);
		SingletonUnity<JoyStickLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "7MOVETO_CAR");
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00043ED8 File Offset: 0x000420D8
	public static void Show_SKILL1_BUTTON()
	{
		if (!SingletonUnity<JueseJiNengQuLogic>.Exists)
		{
			return;
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenRightBtn)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(false);
		}
		SingletonUnity<JueseJiNengQuLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		GameObject gameObject = SingletonUnity<JueseJiNengQuLogic>.Instance.SkillBtnRoot[2].gameObject;
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR)
		{
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SKILL1_BUTTON, true, StrDictionary.GetDictionaryString("#{600004}", new object[0]), SCREEN_DIRECTION.LEFT, Vector3.zero, -1f, null, false, true);
		}
		else
		{
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SKILL1_BUTTON, true, StrDictionary.GetDictionaryString("#{600004}", new object[0]), SCREEN_DIRECTION.TOP, Vector3.zero, -1f, null, false, true);
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00043FB4 File Offset: 0x000421B4
	public static void Show_SKILL1_BTN_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00043FC4 File Offset: 0x000421C4
	public static void Show_SKILL2_BUTTON()
	{
		if (!SingletonUnity<JueseJiNengQuLogic>.Exists)
		{
			return;
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenRightBtn)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMenu(false);
		}
		GameObject gameObject = SingletonUnity<JueseJiNengQuLogic>.Instance.SkillBtnRoot[3].gameObject;
		if (UnityVersionUtil.IsActive(gameObject.gameObject))
		{
			SingletonUnity<JueseJiNengQuLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			FunctionTipsRootLogic.AddFunctionHandTips(gameObject, Vector3.zero, TUTORIAL_STEP.SKILL2_BUTTON, true, StrDictionary.GetDictionaryString("#{600005}", new object[0]), SCREEN_DIRECTION.TOP, Vector3.left * 30f, -1f, null, false, true);
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00044078 File Offset: 0x00042278
	public static void Show_SKILL2_BTN_FINISH()
	{
		FunctionTipsRootLogic.ClearHandTip();
		TutorialManager.MoveNext(false);
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x00044088 File Offset: 0x00042288
	public static void Show_SKILL3_BUTTON()
	{
		SingletonUnity<JueseJiNengQuLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		UISprite uisprite = SingletonUnity<JueseJiNengQuLogic>.Instance.skillIconSprites[4];
		TutorialUIRootLogic.ShowWindow(uisprite.gameObject, uisprite.width, uisprite.height, string.Format("Use New Skill To Kill Enemy", new object[0]), SCREEN_DIRECTION.LEFT, -1f, false, false, true, UIWidget.Pivot.Center);
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x000440EC File Offset: 0x000422EC
	public static void Show_EQUIP_START_OPEN_MENUROOT()
	{
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		TutorialUIRootLogic.ShowWindow(instance.PlayerIcon.gameObject, instance.PlayerIcon.width, instance.PlayerIcon.height, string.Format("Press To Open Menu", new object[0]), SCREEN_DIRECTION.BOTTOM, -1f, true, true, true, UIWidget.Pivot.Center);
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00044154 File Offset: 0x00042354
	public static void Show_EQUIP_OPEN_BACKPACK()
	{
		PlayerInfoMenuRootLogic instance = SingletonUnity<PlayerInfoMenuRootLogic>.Instance;
		if (instance.CurTapType == GAME_MENU_TAP_TYPE.EQUIP_BACKPACK_TAB)
		{
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00044180 File Offset: 0x00042380
	public static void Show_EQUIP_PRESS_EQUIPMENT()
	{
		ItemUILogic firstEquipItem = SingletonUnity<BackPackPageRootLogic>.Instance.BackPackRoot.GetFirstEquipItem();
		if (firstEquipItem != null)
		{
			SingletonUnity<BackPackPageRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
			TutorialUIRootLogic.ShowWindow(firstEquipItem.gameObject, firstEquipItem.ItemIcon.width, firstEquipItem.ItemIcon.height, string.Format("Press Equip To Open Equip Info", new object[0]), SCREEN_DIRECTION.BOTTOM, -1f, true, true, true, UIWidget.Pivot.Center);
		}
		else
		{
			TutorialManager.mCurStep = TUTORIAL_STEP.INVALID;
			TutorialManager.MoveNext(false);
		}
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0004420C File Offset: 0x0004240C
	public static void Show_EQUIP_PRESS_PUTONBTN()
	{
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00044210 File Offset: 0x00042410
	public static void Show_COPY_START_OPEN_MENU()
	{
		GongNengQuLogic instance = SingletonUnity<GongNengQuLogic>.Instance;
		instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
		TutorialUIRootLogic.ShowWindow(instance.MenuBtnRoot.gameObject, instance.MenuBtnSprite.width, instance.MenuBtnSprite.height, string.Format("Open Menu", new object[0]), SCREEN_DIRECTION.LEFT, -1f, true, true, true, UIWidget.Pivot.Center);
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00044278 File Offset: 0x00042478
	public static void Show_COPY_OPEN_COPY_MENU()
	{
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0004427C File Offset: 0x0004247C
	public static void Show_COPY_OPEN_NORMAL_COPY()
	{
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00044280 File Offset: 0x00042480
	public static void Show_COPY_PRESS_MISSION()
	{
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00044284 File Offset: 0x00042484
	public static void Show_COPY_PRESS_PLAY()
	{
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00044288 File Offset: 0x00042488
	public static void Show_COPY_CHANGE_SCENE()
	{
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0004428C File Offset: 0x0004248C
	public static void Show_COPY_BEGIN_TIP()
	{
		TouXiangKuangLogic instance = SingletonUnity<TouXiangKuangLogic>.Instance;
		TutorialUIRootLogic.ShowWindow(instance.gameObject, 0, 0, string.Format("Pass Copy!", new object[0]), SCREEN_DIRECTION.BOTTOM, -1f, true, true, true, UIWidget.Pivot.Center);
		vp_Timer.In(3f, delegate()
		{
			TutorialManager.MoveNext(false);
		}, null);
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x000442F0 File Offset: 0x000424F0
	public static void Show_FUNCTION_TIP_START()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.TIPBTN_TUTORIAL_TIP);
		FunctionTipsRootLogic.ClearHandTip();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			if (UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.MessageObj.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.RegisterTutorialEvent(new TutorialManager.OnClickTutorialBtn(TutorialManager.MoveNext));
				UIWidget component = SingletonUnity<FunctionBtnRootLogic>.Instance.MessageObj.GetComponent<UISprite>();
				List<UIWidget> list = new List<UIWidget>();
				list.Add(component);
				TutorialUIRootLogic.ShowWindow(component.gameObject, list, 70, 70, StrDictionary.GetClientDictionaryString("#{600075}", new object[0]), SCREEN_DIRECTION.TOP_LEFT, true, true, true, true, true);
			}
			else
			{
				TutorialManager.CloseTutorial();
			}
		}
		else
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x000443BC File Offset: 0x000425BC
	public static void MoveNext(bool needDelayFlag = false)
	{
		if (!needDelayFlag)
		{
			TutorialUIRootLogic.CloseWindow();
			TutorialManager.ShowTutorial(TutorialManager.mTutorialStepDic[TutorialManager.mCurStep]);
		}
		else
		{
			vp_Timer.In(Time.deltaTime * 2f, delegate()
			{
				TutorialUIRootLogic.CloseWindow();
				TutorialManager.ShowTutorial(TutorialManager.mTutorialStepDic[TutorialManager.mCurStep]);
			}, null);
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0004441C File Offset: 0x0004261C
	public static void CloseTutorial()
	{
		if (TutorialManager.mTutorialTweener != null)
		{
			TweenExtensions.Kill(TutorialManager.mTutorialTweener, false);
		}
		TutorialUIRootLogic.CloseWindow();
		if (TutorialManager.CurStep == TUTORIAL_STEP.MOVE_SCREEN)
		{
			TutorialManager.MoveNext(false);
		}
		else
		{
			TutorialManager.ShowTutorial(TUTORIAL_STEP.INVALID);
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00044454 File Offset: 0x00042654
	public static bool IsTutorialCanShow()
	{
		return (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager == null || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene()) && (!SingletonUnity<UIManager>.Instance.IsHideBaseUI && (!SingletonUnity<MissionPassShowRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<MissionPassShowRootLogic>.Instance.gameObject))) && (!SingletonUnity<LevelUpUIRoot>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<LevelUpUIRoot>.Instance.gameObject)) && (!SingletonUnity<LevelRewardGetLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<LevelRewardGetLogic>.Instance.gameObject)) && (!SingletonUnity<TutorialUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject)) && (!SingletonUnity<TipCarUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TipCarUIRootLogic>.Instance.gameObject)) && (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.LoadingFlag && (!SingletonUnity<RebirthUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<RebirthUIRootLogic>.Instance.gameObject))) && (!SingletonUnity<OtherPlayerInfoUILogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<OtherPlayerInfoUILogic>.Instance.gameObject));
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00044590 File Offset: 0x00042790
	public static bool IsTutorialCanShow_ClickMission()
	{
		return (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager == null || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene()) && !SingletonUnity<UIManager>.Instance.IsHideBaseUI && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.LoadingFlag;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x000445FC File Offset: 0x000427FC
	public static bool IsNeedCheckTutorial(UIPathData info)
	{
		return ((SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld()) || !SingletonUnity<UIManager>.Instance.IsHideBaseUI) && (info == UIInfo.MissionPassShowRoot || info == UIInfo.LevelUpUIRoot || info == UIInfo.LevelRewardGetRoot || info == UIInfo.TipCarUIRoot || info == UIInfo.OtherPlayerInfoUILogicRoot);
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00044678 File Offset: 0x00042878
	public static void LevelLimitAction()
	{
		MissionTipLogic missionTipRoot = SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot;
		MissionTipLineLogic dailyMissionLine = missionTipRoot.GetDailyMissionLine();
		if (dailyMissionLine != null)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100435}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
			{
				SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
				TutorialManager.ShowTutorial(TUTORIAL_STEP.DAILY_MISSION_CLICK_START);
			}, null, null, null);
		}
		else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.IsDailyCopyCanPlay(MAPTYPE.EXP_DAILY_COPY))
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100436}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
			{
				SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ActivityUIRootLogic, delegate
				{
					SingletonUnity<ActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EXP_DAILY_COPY);
				}, null);
			}, null, null, null);
		}
	}

	// Token: 0x04000760 RID: 1888
	private static float TutorialTipDuration = 10f;

	// Token: 0x04000761 RID: 1889
	private static Dictionary<TUTORIAL_STEP, TUTORIAL_STEP> mTutorialStepDic = new Dictionary<TUTORIAL_STEP, TUTORIAL_STEP>();

	// Token: 0x04000762 RID: 1890
	private static TUTORIAL_STEP mCurStep = TUTORIAL_STEP.INVALID;

	// Token: 0x04000763 RID: 1891
	public static bool IsMatchTeamTutorial = false;

	// Token: 0x04000764 RID: 1892
	private static Tweener mTutorialTweener;

	// Token: 0x02000ABF RID: 2751
	// (Invoke) Token: 0x06004F85 RID: 20357
	public delegate void OnClickTutorialBtn(bool needDelayFlag = false);
}
