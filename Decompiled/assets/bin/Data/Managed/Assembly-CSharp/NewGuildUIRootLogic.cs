using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200085E RID: 2142
public class NewGuildUIRootLogic : SingletonUnity<NewGuildUIRootLogic>
{
	// Token: 0x0600378A RID: 14218 RVA: 0x000E4A24 File Offset: 0x000E2C24
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x0600378B RID: 14219 RVA: 0x000E4A30 File Offset: 0x000E2C30
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x0600378C RID: 14220 RVA: 0x000E4A50 File Offset: 0x000E2C50
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x0600378D RID: 14221 RVA: 0x000E4A5C File Offset: 0x000E2C5C
	public void RequestGuildInfo()
	{
		Singleton<ObjManager>.Instance.MainPlayer.OpenGuild();
		WaitResponseUIRootLogic.OpenWaitBox(153, 10f, 0f, null);
	}

	// Token: 0x0600378E RID: 14222 RVA: 0x000E4A90 File Offset: 0x000E2C90
	public void RequestGuildList()
	{
		Singleton<ObjManager>.Instance.MainPlayer.SearchAllGuild();
		WaitResponseUIRootLogic.OpenWaitBox(153, 10f, 0f, null);
	}

	// Token: 0x0600378F RID: 14223 RVA: 0x000E4AC4 File Offset: 0x000E2CC4
	private void ShowBlankTabBase()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate
		{
			List<MenuTabBtnInfo> leftBtnInfo = new List<MenuTabBtnInfo>();
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(leftBtnInfo, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.RequestGuildInfo();
		}, null);
	}

	// Token: 0x06003790 RID: 14224 RVA: 0x000E4AE4 File Offset: 0x000E2CE4
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCreateUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildListInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewGuildUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStrengthenRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCityRoot);
	}

	// Token: 0x06003791 RID: 14225 RVA: 0x000E4BB4 File Offset: 0x000E2DB4
	public void CloseHasGuildUI()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		this.curShowType = NewGuildUIRootLogic.SHOW_GUILD_TYPE.NOTHING;
	}

	// Token: 0x06003792 RID: 14226 RVA: 0x000E4C14 File Offset: 0x000E2E14
	public void CloseNoGuildUI()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCreateUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildListInfoRoot);
		this.curShowType = NewGuildUIRootLogic.SHOW_GUILD_TYPE.NOTHING;
	}

	// Token: 0x06003793 RID: 14227 RVA: 0x000E4C3C File Offset: 0x000E2E3C
	private void OnEnable()
	{
		this.curShowType = NewGuildUIRootLogic.SHOW_GUILD_TYPE.NOTHING;
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_NOTING;
		this.ShowBlankTabBase();
		this.targetTabType = -1;
	}

	// Token: 0x06003794 RID: 14228 RVA: 0x000E4C5C File Offset: 0x000E2E5C
	public void OnClickCreatBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_CREATE)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildListInfoRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildCreateUIRoot, null, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_CREATE;
	}

	// Token: 0x06003795 RID: 14229 RVA: 0x000E4CA8 File Offset: 0x000E2EA8
	public void OnClickGuildListBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_LIST)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCreateUIRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildListInfoRoot, delegate
		{
			SingletonUnity<GuildListInfoRootLogic>.Instance.PreResetGuildListInfo();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		Singleton<ObjManager>.Instance.MainPlayer.SearchAllGuild();
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_LIST;
	}

	// Token: 0x06003796 RID: 14230 RVA: 0x000E4D20 File Offset: 0x000E2F20
	public void ShowHasNoGuildInfo(ret_guild_req_list.request request)
	{
		if (this.curShowType != NewGuildUIRootLogic.SHOW_GUILD_TYPE.NO_GUILD)
		{
			this.CloseHasGuildUI();
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickGuildListBtn), true, "CZ_left_List", StrDictionary.GetDictionaryString("#{100730}", new object[0]), FUNCTION_TYPE.GUILD_LIST, null);
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickCreatBtn), true, "CZ_left_Create", StrDictionary.GetDictionaryString("#{100731}", new object[0]), FUNCTION_TYPE.GUILD_CREATE, null);
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_NOTING;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildListInfoRoot, delegate
			{
				if (request != null && request.HasGuild_info)
				{
					SingletonUnity<GuildListInfoRootLogic>.Instance.UpdateGuildListInfo(request.guild_info, (int)request.curPage, (int)request.maxPage);
				}
				else
				{
					SingletonUnity<GuildListInfoRootLogic>.Instance.UpdateGuildListInfo(null, 1, 1);
				}
				SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
				this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_LIST;
			}, null);
			this.isInitTab = true;
			this.curShowType = NewGuildUIRootLogic.SHOW_GUILD_TYPE.NO_GUILD;
		}
		else if (SingletonUnity<GuildListInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildListInfoRootLogic>.Instance.gameObject))
		{
			if (request != null && request.HasGuild_info)
			{
				SingletonUnity<GuildListInfoRootLogic>.Instance.UpdateGuildListInfo(request.guild_info, (int)request.curPage, (int)request.maxPage);
			}
			else
			{
				SingletonUnity<GuildListInfoRootLogic>.Instance.UpdateGuildListInfo(null, 1, 1);
			}
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06003797 RID: 14231 RVA: 0x000E4EA0 File Offset: 0x000E30A0
	public void OnClickGuildInfoBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_INFO)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildInfoRootLogic, null, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStrengthenRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCityRoot);
		this.RequestGuildInfo();
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_INFO;
	}

	// Token: 0x06003798 RID: 14232 RVA: 0x000E4F5C File Offset: 0x000E315C
	public void OnClickMemberBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_MEMBER)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStrengthenRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCityRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildMemberRootLogic, delegate
		{
			SingletonUnity<GuildMemberRootLogic>.Instance.EnableReset();
			Singleton<ObjManager>.Instance.MainPlayer.ApplyUpdataGuildMemberList();
			WaitResponseUIRootLogic.OpenWaitBox(170, 10f, 0f, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_MEMBER;
	}

	// Token: 0x06003799 RID: 14233 RVA: 0x000E5030 File Offset: 0x000E3230
	public void OnClickShopBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_SHOP)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStrengthenRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCityRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopTabRootLogic, delegate
		{
			SingletonUnity<ShopTabRootLogic>.Instance.EnableReset(GameDefine.SHOP_TYPE.GUILD_SHOP, null);
			SingletonUnity<ShopTabRootLogic>.Instance.ClearSelectObj();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_SHOP;
	}

	// Token: 0x0600379A RID: 14234 RVA: 0x000E5104 File Offset: 0x000E3304
	public void OnClickActivityBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_ACTIVITY)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStrengthenRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCityRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildActivityRootLogic, delegate
		{
			SingletonUnity<GuildActivityRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(195, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_guild_boss>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(4);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_ACTIVITY;
	}

	// Token: 0x0600379B RID: 14235 RVA: 0x000E51D8 File Offset: 0x000E33D8
	public void ShowHasGuildInfo()
	{
		PlayerData playeData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.curShowType != NewGuildUIRootLogic.SHOW_GUILD_TYPE.HAS_GUILD)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			this.CloseNoGuildUI();
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickGuildInfoBtn), true, "CZ_left_Information", StrDictionary.GetDictionaryString("#{100702}", new object[0]), FUNCTION_TYPE.GUILD_INFO, null);
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickMemberBtn), true, "CZ_left_Member", StrDictionary.GetDictionaryString("#{100703}", new object[0]), FUNCTION_TYPE.GUILD_MEMBER, new DelegateDefine.NoParamReturnDelegate(playerData.PlayerGuild.IsHaveNewApply));
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickStrengthenBtn), true, "CZ_left_Skill", StrDictionary.GetDictionaryString("#{100619}", new object[0]), FUNCTION_TYPE.GUILD_SKILL, null);
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickShopBtn), true, "CZ_left_Shop", StrDictionary.GetDictionaryString("#{100705}", new object[0]), FUNCTION_TYPE.GUILD_SHOP, null);
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickActivityBtn), true, "CZ_left_Activity", StrDictionary.GetDictionaryString("#{101536}", new object[0]), FUNCTION_TYPE.GUILD_ACTIVITY, new DelegateDefine.NoParamReturnDelegate(playerData.ActivityData.IsHaveGuildActTips));
			MenuTabBtnInfo menuTabBtnInfo6 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickGuildCityBtn), true, "CZ_left_GangDomain", StrDictionary.GetDictionaryString("#{106033}", new object[0]), FUNCTION_TYPE.GUILD_CITY, null);
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			list.Add(menuTabBtnInfo3);
			list.Add(menuTabBtnInfo4);
			list.Add(menuTabBtnInfo5);
			list.Add(menuTabBtnInfo6);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curShowType = NewGuildUIRootLogic.SHOW_GUILD_TYPE.HAS_GUILD;
			if (this.targetTabType == 2)
			{
				this.targetTabType = -1;
				this.OnClickStrengthenBtn();
			}
			else if (this.targetTabType == 0)
			{
				this.targetTabType = -1;
				this.OnClickGuildInfoBtn();
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildInfoRootLogic, delegate
				{
					SingletonUnity<GuildInfoRootLogic>.Instance.UpDateGuildInfo(playeData.PlayerGuild);
				}, null);
				SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
				this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_INFO;
				if (UIUpdateEvent.OnUIPageLoadFinished != null)
				{
					UIUpdateEvent.OnUIPageLoadFinished();
				}
				else
				{
					SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap();
				}
			}
		}
		else if (SingletonUnity<GuildInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildInfoRootLogic>.Instance.gameObject))
		{
			SingletonUnity<GuildInfoRootLogic>.Instance.UpDateGuildInfo(playeData.PlayerGuild);
		}
	}

	// Token: 0x0600379C RID: 14236 RVA: 0x000E5470 File Offset: 0x000E3670
	public void OnClickStrengthenBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_SKILL)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCityRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildStrengthenRoot, delegate
		{
			SingletonUnity<GuildStrengthenRootLogic>.Instance.Reset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(2);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_SKILL;
	}

	// Token: 0x0600379D RID: 14237 RVA: 0x000E5534 File Offset: 0x000E3734
	public void OnClickGuildCityBtn()
	{
		if (this.curTabType == NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_CITY)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildInfoRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStrengthenRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildMemberRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildCityRoot, delegate
		{
			WaitResponseUIRootLogic.OpenWaitBox(319, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_guild_map_info>(null, null);
			SingletonUnity<GuildCityRootLogic>.Instance.EnableReset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(5);
		this.curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_CITY;
	}

	// Token: 0x040024B3 RID: 9395
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040024B4 RID: 9396
	private bool isShowActivityTab;

	// Token: 0x040024B5 RID: 9397
	public bool isInitTab;

	// Token: 0x040024B6 RID: 9398
	public NewGuildUIRootLogic.SHOW_GUILD_TYPE curShowType = NewGuildUIRootLogic.SHOW_GUILD_TYPE.NOTHING;

	// Token: 0x040024B7 RID: 9399
	public NewGuildUIRootLogic.GUILD_TAB_TYPE curTabType = NewGuildUIRootLogic.GUILD_TAB_TYPE.GUILD_NOTING;

	// Token: 0x040024B8 RID: 9400
	public int targetTabType = -1;

	// Token: 0x0200085F RID: 2143
	public enum SHOW_GUILD_TYPE
	{
		// Token: 0x040024C0 RID: 9408
		NOTHING = -1,
		// Token: 0x040024C1 RID: 9409
		NO_GUILD,
		// Token: 0x040024C2 RID: 9410
		HAS_GUILD
	}

	// Token: 0x02000860 RID: 2144
	public enum GUILD_TAB_TYPE
	{
		// Token: 0x040024C4 RID: 9412
		GUILD_NOTING = -1,
		// Token: 0x040024C5 RID: 9413
		GUILD_INFO,
		// Token: 0x040024C6 RID: 9414
		GUILD_MEMBER,
		// Token: 0x040024C7 RID: 9415
		GUILD_SKILL,
		// Token: 0x040024C8 RID: 9416
		GUILD_SHOP,
		// Token: 0x040024C9 RID: 9417
		GUILD_ACTIVITY,
		// Token: 0x040024CA RID: 9418
		GUILD_LIST,
		// Token: 0x040024CB RID: 9419
		GUILD_CREATE,
		// Token: 0x040024CC RID: 9420
		GUILD_STAR,
		// Token: 0x040024CD RID: 9421
		GUILD_CITY
	}
}
