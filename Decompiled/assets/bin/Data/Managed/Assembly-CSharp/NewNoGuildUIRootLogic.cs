using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000861 RID: 2145
public class NewNoGuildUIRootLogic : SingletonUnity<NewNoGuildUIRootLogic>
{
	// Token: 0x060037A6 RID: 14246 RVA: 0x000E5718 File Offset: 0x000E3918
	public void Reset()
	{
	}

	// Token: 0x060037A7 RID: 14247 RVA: 0x000E571C File Offset: 0x000E391C
	public void UpdateGuildList(Dictionary<long, guild_info> guildDic, int curPage, int maxPage)
	{
	}

	// Token: 0x060037A8 RID: 14248 RVA: 0x000E5720 File Offset: 0x000E3920
	public void ShowSearchResult(List<guild_info> resultList, List<long> rankList)
	{
	}

	// Token: 0x060037A9 RID: 14249 RVA: 0x000E5724 File Offset: 0x000E3924
	public void DisableSearchFlag()
	{
	}

	// Token: 0x060037AA RID: 14250 RVA: 0x000E5728 File Offset: 0x000E3928
	public void OnClickCreatBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildCreateUIRoot, null, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildListInfoRoot);
	}

	// Token: 0x060037AB RID: 14251 RVA: 0x000E5758 File Offset: 0x000E3958
	public void OnClickGuildListBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCreateUIRoot);
		Singleton<ObjManager>.Instance.MainPlayer.SearchAllGuild();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildListInfoRoot, delegate
		{
			SingletonUnity<GuildListInfoRootLogic>.Instance.PreResetGuildListInfo();
		}, null);
		WaitResponseUIRootLogic.OpenWaitBox(152, 10f, 0f, null);
	}

	// Token: 0x060037AC RID: 14252 RVA: 0x000E57C8 File Offset: 0x000E39C8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildCreateUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildListInfoRoot);
	}

	// Token: 0x060037AD RID: 14253 RVA: 0x000E5804 File Offset: 0x000E3A04
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate
		{
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickGuildListBtn), true, "CZ_tuBiao_RenWuShuXing", "List", FUNCTION_TYPE.GUILD_LIST, null);
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickCreatBtn), true, "CZ_tuBiao_RenWuShuXing", "Create", FUNCTION_TYPE.GUILD_CREATE, null);
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.OnClickCreatBtn();
			SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		}, null);
	}

	// Token: 0x02000862 RID: 2146
	public enum GuildRootType
	{
		// Token: 0x040024D0 RID: 9424
		CREATEGUILD_TYPE,
		// Token: 0x040024D1 RID: 9425
		GUILDLIST_TYPE
	}
}
