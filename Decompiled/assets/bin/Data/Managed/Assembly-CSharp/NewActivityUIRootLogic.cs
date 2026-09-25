using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000A29 RID: 2601
public class NewActivityUIRootLogic : SingletonUnity<NewActivityUIRootLogic>
{
	// Token: 0x06004B57 RID: 19287 RVA: 0x00190CE8 File Offset: 0x0018EEE8
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

	// Token: 0x06004B58 RID: 19288 RVA: 0x00190DBC File Offset: 0x0018EFBC
	public void SetPrePage(UI_PAGE_TYPE prePage)
	{
		this.mPrePage = prePage;
	}

	// Token: 0x06004B59 RID: 19289 RVA: 0x00190DC8 File Offset: 0x0018EFC8
	public int GetPageIndex(FUNCTION_TYPE type)
	{
		if (type == FUNCTION_TYPE.ACTIVITY_DAILY)
		{
			return 2;
		}
		if (type == FUNCTION_TYPE.ACTIVITY_TIME)
		{
			return 3;
		}
		if (type == FUNCTION_TYPE.RANK_PVP)
		{
			return 4;
		}
		if (type == FUNCTION_TYPE.GIFT_ACTIVITY)
		{
			return 0;
		}
		if (type == FUNCTION_TYPE.MISSION)
		{
			return 5;
		}
		if (type != FUNCTION_TYPE.DOMIN)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x06004B5A RID: 19290 RVA: 0x00190E2C File Offset: 0x0018F02C
	public void InitActivityUI()
	{
		ConfigData configDataByKey = DataManager.GetConfigDataByKey("MissionPosLevel");
		if (configDataByKey != null)
		{
			this.mMissionChangePosLevel = int.Parse(configDataByKey.ContentValue);
		}
		else
		{
			this.mMissionChangePosLevel = 10;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			WelfareData welfareData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickDailyActiveBtn), true, "CZ_left_huoYue", StrDictionary.GetDictionaryString("#{300105}", new object[0]), FUNCTION_TYPE.GIFT_ACTIVITY, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveDailyActivityTips));
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickDailyBtn), true, "CZ_left_Daily", StrDictionary.GetDictionaryString("#{101535}", new object[0]), FUNCTION_TYPE.ACTIVITY_DAILY, new DelegateDefine.NoParamReturnDelegate(playerData.CopyInfoData.IsHaveDailyItemTips));
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickTimeBtn), true, "CZ_left_Activity", StrDictionary.GetDictionaryString("#{101536}", new object[0]), FUNCTION_TYPE.ACTIVITY_TIME, new DelegateDefine.NoParamReturnDelegate(playerData.ActivityData.IsHaveActTips));
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickRankBtn), true, "CZ_left_PVP", StrDictionary.GetDictionaryString("#{100106}", new object[0]), FUNCTION_TYPE.RANK_PVP, new DelegateDefine.NoParamReturnDelegate(playerData.RankPVPData.IsHavePVPTips));
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickMissionBtn), true, "CZ_left_Mission", StrDictionary.GetDictionaryString("#{100324}", new object[0]), FUNCTION_TYPE.MISSION, null);
			MenuTabBtnInfo menuTabBtnInfo6 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickDominBtn), true, "CZ_left_Domain", StrDictionary.GetDictionaryString("#{103003}", new object[0]), FUNCTION_TYPE.DOMIN, null);
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo6);
			list.Add(menuTabBtnInfo2);
			list.Add(menuTabBtnInfo3);
			list.Add(menuTabBtnInfo4);
			list.Add(menuTabBtnInfo5);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.CurPageIndex = -1;
		}, null);
	}

	// Token: 0x06004B5B RID: 19291 RVA: 0x00190E84 File Offset: 0x0018F084
	public void OnClickDailyActiveBtn()
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_ACTIVITY))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMapUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DailyActiveRewardRoot, delegate
		{
			SingletonUnity<DailyActiveRewardRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(261, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_active>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_ACTIVITY));
		this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_ACTIVITY);
	}

	// Token: 0x06004B5C RID: 19292 RVA: 0x00190F94 File Offset: 0x0018F194
	public void OnClickActivityBtn()
	{
		this.OnClickActivityBtn(GameDefine.ACTIVITY_TYPE.INVALID);
	}

	// Token: 0x06004B5D RID: 19293 RVA: 0x00190FA0 File Offset: 0x0018F1A0
	public void OnClickActivityBtn(GameDefine.ACTIVITY_TYPE type)
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.GUILD_ACTIVITY))
		{
			return;
		}
		Singleton<ObjManager>.Instance.MainPlayer.ApplyUpDataGuild();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMapUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildActivityRootLogic, delegate
			{
				SingletonUnity<GuildActivityRootLogic>.Instance.EnableReset();
				SingletonUnity<GuildActivityRootLogic>.Instance.TargetTypeId = (int)type;
				WaitResponseUIRootLogic.OpenWaitBox(195, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_guild_boss>(null, null);
			}, null);
			SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GUILD_ACTIVITY));
			this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.GUILD_ACTIVITY);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
		}
	}

	// Token: 0x06004B5E RID: 19294 RVA: 0x001910E0 File Offset: 0x0018F2E0
	public void Reset()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
		{
			this.OnClickDailyBtn();
		}
		else
		{
			this.OnClickMissionBtn();
		}
	}

	// Token: 0x06004B5F RID: 19295 RVA: 0x00191118 File Offset: 0x0018F318
	public void ResetToPVP()
	{
		this.OnClickRankBtn();
	}

	// Token: 0x06004B60 RID: 19296 RVA: 0x00191120 File Offset: 0x0018F320
	public void ResetToDaily()
	{
		this.OnClickDailyBtn();
	}

	// Token: 0x06004B61 RID: 19297 RVA: 0x00191128 File Offset: 0x0018F328
	public void OnClickDailyBtn()
	{
		this.OnClickDailyBtn(MAPTYPE.INVALID, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
	}

	// Token: 0x06004B62 RID: 19298 RVA: 0x00191134 File Offset: 0x0018F334
	public void OnClickDailyBtn(MAPTYPE type, string copyid = null, string mapid = null, GameDefine.ACTIVITY_TYPE acttype = GameDefine.ACTIVITY_TYPE.INVALID)
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.ACTIVITY_DAILY))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewMapUIRootLogic, delegate
		{
			if (string.IsNullOrEmpty(mapid))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
			}
			else
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(mapid);
			}
		}, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewDailyCopyUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.EnableReset();
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.TargetTypeId = (int)type;
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.TargetActId = copyid;
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.TargetActType = acttype;
			WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.ACTIVITY_DAILY));
		this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.ACTIVITY_DAILY);
	}

	// Token: 0x06004B63 RID: 19299 RVA: 0x00191260 File Offset: 0x0018F460
	public void OnClickTimeBtn()
	{
		this.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.INVALID, null, false);
	}

	// Token: 0x06004B64 RID: 19300 RVA: 0x0019126C File Offset: 0x0018F46C
	public void OnClickTimeBtn(GameDefine.ACTIVITY_TYPE type, string mapid = null, bool isMapClick = false)
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return;
		}
		Singleton<ObjManager>.Instance.MainPlayer.ApplyUpDataGuild();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewMapUIRootLogic, delegate
		{
			if (string.IsNullOrEmpty(mapid))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
			}
			else
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(mapid);
			}
		}, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewDailyActivityUIRoot, delegate(bool bSuccess, object param)
		{
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.EnableReset();
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.TargetTypeId = (int)type;
			SingletonUnity<NewDailyActivityUIRootLogic>.Instance.IsMapClick = isMapClick;
			WaitResponseUIRootLogic.OpenWaitBox(225, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_activity_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_wild_boss_info>(null, null);
			WaitResponseUIRootLogic.OpenWaitBox(195, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_guild_boss>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.ACTIVITY_TIME));
		this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.ACTIVITY_TIME);
	}

	// Token: 0x06004B65 RID: 19301 RVA: 0x001913A0 File Offset: 0x0018F5A0
	public void OnClickRankBtn()
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.RANK_PVP))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMapUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		WaitResponseUIRootLogic.OpenWaitBox(133, 10f, 0f, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RankPVPRoot, delegate
		{
			SingletonUnity<RankPVPUIRootLogic>.Instance.EnableReset();
		}, null);
		request_rank_pvp_data.request rpcReq = new request_rank_pvp_data.request();
		NetLogic.GetInstance().Send<Protocol.request_rank_pvp_data>(rpcReq, null);
		request_random_rank_pvp_opponent.request rpcReq2 = new request_random_rank_pvp_opponent.request();
		NetLogic.GetInstance().Send<Protocol.request_random_rank_pvp_opponent>(rpcReq2, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.RANK_PVP));
		this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.RANK_PVP);
	}

	// Token: 0x06004B66 RID: 19302 RVA: 0x001914E8 File Offset: 0x0018F6E8
	public void OnClickMissionBtn()
	{
		this.OnClickMissionBtn(string.Empty, null);
	}

	// Token: 0x06004B67 RID: 19303 RVA: 0x001914F8 File Offset: 0x0018F6F8
	public void OnClickMissionBtn(string missionId, string mapid = null)
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.MISSION))
		{
			if (SingletonUnity<NewMissionUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMissionUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMissionUIRootLogic>.Instance.Reset(missionId);
			}
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewMapUIRootLogic, delegate
		{
			if (string.IsNullOrEmpty(mapid))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
			}
			else
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(mapid);
			}
		}, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewMissionUIRootLogic, delegate
		{
			SingletonUnity<NewMissionUIRootLogic>.Instance.Reset(missionId);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.MISSION));
		this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.MISSION);
	}

	// Token: 0x06004B68 RID: 19304 RVA: 0x00191644 File Offset: 0x0018F844
	public void OnClickDominBtn()
	{
		this.OnClickDominBtn(string.Empty);
	}

	// Token: 0x06004B69 RID: 19305 RVA: 0x00191654 File Offset: 0x0018F854
	public void OnClickDominBtn(string dominId)
	{
		if (this.CurPageIndex == this.GetPageIndex(FUNCTION_TYPE.DOMIN))
		{
			if (SingletonUnity<DominRootLogic>.Exists)
			{
				SingletonUnity<DominRootLogic>.Instance.ClickTargetLine(dominId);
			}
			else
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DominPageRoot, delegate
				{
					NetLogic.GetInstance().Send<Protocol.request_domin_info>(null, null);
					SingletonUnity<DominRootLogic>.Instance.EnableReset(dominId);
				}, null);
			}
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewMapUIRootLogic, delegate
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.Reset("11");
		}, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DominPageRoot, delegate
		{
			WaitResponseUIRootLogic.OpenWaitBox(310, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_domin_info>(null, null);
			SingletonUnity<DominRootLogic>.Instance.EnableReset(dominId);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.DOMIN));
		this.CurPageIndex = this.GetPageIndex(FUNCTION_TYPE.DOMIN);
	}

	// Token: 0x06004B6A RID: 19306 RVA: 0x001917C8 File Offset: 0x0018F9C8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyCopyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewActivityUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPVPRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMapUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMissionUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		this.CheckPrePage();
	}

	// Token: 0x06004B6B RID: 19307 RVA: 0x001918A0 File Offset: 0x0018FAA0
	private void OnEnable()
	{
		this.InitActivityUI();
		this.mPrePage = UI_PAGE_TYPE.INVALID;
	}

	// Token: 0x04003918 RID: 14616
	private UI_PAGE_TYPE mPrePage = UI_PAGE_TYPE.INVALID;

	// Token: 0x04003919 RID: 14617
	public int CurPageIndex = -1;

	// Token: 0x0400391A RID: 14618
	private int mMissionChangePosLevel = 10;
}
