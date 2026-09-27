using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x020008F2 RID: 2290
public class ActivityUIRootLogic : SingletonUnity<ActivityUIRootLogic>
{
	// Token: 0x06003DFF RID: 15871 RVA: 0x00118F10 File Offset: 0x00117110
	public void CheckPrePage()
	{
		if (this.mPrePage != UI_PAGE_TYPE.INVALID)
		{
			switch (this.mPrePage)
			{
			case UI_PAGE_TYPE.BACK_PACK_ITEM:
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
				{
					SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickItemBackPackBtn();
				}, null);
				break;
			case UI_PAGE_TYPE.ENHANCE_EQUIP:
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
				}, null);
				break;
			case UI_PAGE_TYPE.REFINE:
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
				}, null);
				break;
			}
		}
		this.mPrePage = UI_PAGE_TYPE.INVALID;
	}

	// Token: 0x06003E00 RID: 15872 RVA: 0x00118FE4 File Offset: 0x001171E4
	public void SetPrePage(UI_PAGE_TYPE prePage)
	{
		this.mPrePage = prePage;
	}

	// Token: 0x06003E01 RID: 15873 RVA: 0x00118FF0 File Offset: 0x001171F0
	public void InitActivityUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickDailyBtn), true, "CZ_left_Daily", StrDictionary.GetDictionaryString("#{101535}", new object[0]), FUNCTION_TYPE.ACTIVITY_DAILY, new DelegateDefine.NoParamReturnDelegate(playerData.CopyInfoData.IsHaveDailyCopyTips));
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickTimeBtn), true, "CZ_left_Activity", StrDictionary.GetDictionaryString("#{101536}", new object[0]), FUNCTION_TYPE.ACTIVITY_TIME, new DelegateDefine.NoParamReturnDelegate(playerData.ActivityData.IsHaveActivity));
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickTowerBtn), true, "CZ_left_Challenge", StrDictionary.GetDictionaryString("#{101538}", new object[0]), FUNCTION_TYPE.ACTIVITY_CHALLENGE, new DelegateDefine.NoParamReturnDelegate(playerData.TowerData.IsHaveTowerTips));
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickActivityBtn), true, "CZ_left_Battle", StrDictionary.GetDictionaryString("#{100114}", new object[0]), FUNCTION_TYPE.GUILD_ACTIVITY, new DelegateDefine.NoParamReturnDelegate(playerData.ActivityData.IsHaveGuildActTips));
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickRankBtn), true, "CZ_left_PVP", StrDictionary.GetDictionaryString("#{100106}", new object[0]), FUNCTION_TYPE.RANK_PVP, new DelegateDefine.NoParamReturnDelegate(playerData.RankPVPData.IsHavePVPTips));
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			list.Add(menuTabBtnInfo5);
			list.Add(menuTabBtnInfo3);
			list.Add(menuTabBtnInfo4);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.CurPageIndex = -1;
		}, null);
	}

	// Token: 0x06003E02 RID: 15874 RVA: 0x00119010 File Offset: 0x00117210
	public void OnClickActivityBtn()
	{
		this.OnClickActivityBtn(GameDefine.ACTIVITY_TYPE.INVALID);
	}

	// Token: 0x06003E03 RID: 15875 RVA: 0x0011901C File Offset: 0x0011721C
	public void OnClickActivityBtn(GameDefine.ACTIVITY_TYPE type)
	{
		if (this.CurPageIndex == 4)
		{
			return;
		}
		Singleton<ObjManager>.Instance.MainPlayer.ApplyUpDataGuild();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WildBossUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildActivityRootLogic, delegate
		{
			SingletonUnity<GuildActivityRootLogic>.Instance.EnableReset();
			SingletonUnity<GuildActivityRootLogic>.Instance.TargetTypeId = (int)type;
			WaitResponseUIRootLogic.OpenWaitBox(195, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_guild_boss>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(4);
		this.CurPageIndex = 4;
	}

	// Token: 0x06003E04 RID: 15876 RVA: 0x001190E8 File Offset: 0x001172E8
	public void Reset()
	{
		if (!SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap())
		{
			this.OnClickDailyBtn();
		}
	}

	// Token: 0x06003E05 RID: 15877 RVA: 0x00119100 File Offset: 0x00117300
	public void ResetToTower()
	{
		this.OnClickTowerBtn();
	}

	// Token: 0x06003E06 RID: 15878 RVA: 0x00119108 File Offset: 0x00117308
	public void ResetToPVP()
	{
		this.OnClickRankBtn();
	}

	// Token: 0x06003E07 RID: 15879 RVA: 0x00119110 File Offset: 0x00117310
	public void ResetToDaily()
	{
		this.OnClickDailyBtn();
	}

	// Token: 0x06003E08 RID: 15880 RVA: 0x00119118 File Offset: 0x00117318
	public void OnClickDailyBtn()
	{
		this.OnClickDailyBtn(MAPTYPE.INVALID);
	}

	// Token: 0x06003E09 RID: 15881 RVA: 0x00119124 File Offset: 0x00117324
	public void OnClickDailyBtn(MAPTYPE type)
	{
		if (this.CurPageIndex == 0)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WildBossUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DailyCopyUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<DailyCopyUIRootLogic>.Instance.EnableReset();
			SingletonUnity<DailyCopyUIRootLogic>.Instance.TargetTypeId = (int)type;
			WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.CurPageIndex = 0;
	}

	// Token: 0x06003E0A RID: 15882 RVA: 0x001191D4 File Offset: 0x001173D4
	public void OnClickTimeBtn()
	{
		this.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.INVALID);
	}

	// Token: 0x06003E0B RID: 15883 RVA: 0x001191E0 File Offset: 0x001173E0
	public void OnClickTimeBtn(GameDefine.ACTIVITY_TYPE type)
	{
		if (this.CurPageIndex == 1)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WildBossUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DailyActivityUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<DailyActivityUIRootLogic>.Instance.EnableReset();
			SingletonUnity<DailyActivityUIRootLogic>.Instance.TargetTypeId = (int)type;
			WaitResponseUIRootLogic.OpenWaitBox(225, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_activity_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_wild_boss_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		this.CurPageIndex = 1;
	}

	// Token: 0x06003E0C RID: 15884 RVA: 0x00119290 File Offset: 0x00117490
	public void OnClickTowerBtn()
	{
		if (this.CurPageIndex == 3)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WildBossUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerUIRootLogic, delegate
		{
			SingletonUnity<TowerUIRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(202, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
		this.CurPageIndex = 3;
	}

	// Token: 0x06003E0D RID: 15885 RVA: 0x00119344 File Offset: 0x00117544
	public void OnClickRankBtn()
	{
		if (this.CurPageIndex == 2)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WildBossUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		WaitResponseUIRootLogic.OpenWaitBox(133, 10f, 0f, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RankPVPRoot, delegate
		{
			SingletonUnity<RankPVPUIRootLogic>.Instance.EnableReset();
		}, null);
		request_rank_pvp_data.request rpcReq = new request_rank_pvp_data.request();
		NetLogic.GetInstance().Send<Protocol.request_rank_pvp_data>(rpcReq, null);
		request_random_rank_pvp_opponent.request rpcReq2 = new request_random_rank_pvp_opponent.request();
		NetLogic.GetInstance().Send<Protocol.request_random_rank_pvp_opponent>(rpcReq2, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(2);
		this.CurPageIndex = 2;
	}

	// Token: 0x06003E0E RID: 15886 RVA: 0x00119430 File Offset: 0x00117630
	public void OnClickWildBossBtn()
	{
		if (this.CurPageIndex == 4)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.WildBossUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<WildBossRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(200, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_wild_boss_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(4);
		this.CurPageIndex = 4;
	}

	// Token: 0x06003E0F RID: 15887 RVA: 0x001194E4 File Offset: 0x001176E4
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WildBossUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyRewardNewRoot);
		this.CheckPrePage();
	}

	// Token: 0x06003E10 RID: 15888 RVA: 0x00119590 File Offset: 0x00117790
	public void BackTowerUI()
	{
		this.OnClickTowerBtn();
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
	}

	// Token: 0x06003E11 RID: 15889 RVA: 0x001195A4 File Offset: 0x001177A4
	private void OnEnable()
	{
		this.InitActivityUI();
		this.mPrePage = UI_PAGE_TYPE.INVALID;
	}

	// Token: 0x040029AA RID: 10666
	private UI_PAGE_TYPE mPrePage = UI_PAGE_TYPE.INVALID;

	// Token: 0x040029AB RID: 10667
	public int CurPageIndex = -1;
}
