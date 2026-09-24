using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x020009AE RID: 2478
public class SocialUIRootLogic : SingletonUnity<SocialUIRootLogic>
{
	// Token: 0x06004655 RID: 18005 RVA: 0x001640CC File Offset: 0x001622CC
	public void InitSocialUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickMailBtn), true, "CZ_left_Mail", StrDictionary.GetDictionaryString("#{100215}", new object[0]), FUNCTION_TYPE.SOCIAL_MAIL, new DelegateDefine.NoParamReturnDelegate(friendInfo.IsHaveMailTips));
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickFriendBtn), true, "CZ_left_Friend", StrDictionary.GetDictionaryString("#{100212}", new object[0]), FUNCTION_TYPE.SOCIAL_FRIEND, null);
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickEnemyBtn), true, "CZ_left_chouRen", StrDictionary.GetDictionaryString("#{103301}", new object[0]), FUNCTION_TYPE.SOCIAL_ENEMY, null);
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickFriendListBtn), true, "CZ_left_Request", StrDictionary.GetDictionaryString("#{100214}", new object[0]), FUNCTION_TYPE.SOCIAL_APPLY, new DelegateDefine.NoParamReturnDelegate(friendInfo.IshavefriendApply));
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			list.Add(menuTabBtnInfo3);
			list.Add(menuTabBtnInfo4);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curPageIndex = -1;
		}, null);
	}

	// Token: 0x06004656 RID: 18006 RVA: 0x001640EC File Offset: 0x001622EC
	public void SelectFriendInfobtn()
	{
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (friendInfo.IshavefriendApply())
		{
			this.OnClickFriendListBtn();
		}
		else
		{
			this.OnClickFriendBtn();
		}
	}

	// Token: 0x06004657 RID: 18007 RVA: 0x00164128 File Offset: 0x00162328
	public void OnClickFriendBtn()
	{
		if (this.curPageIndex == 1)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendApplyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MailUIRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FriendUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<FriendUIRootLogic>.Instance.EnableReset();
			SingletonUnity<FriendUIRootLogic>.Instance.UpdateFriendList();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		this.curPageIndex = 1;
	}

	// Token: 0x06004658 RID: 18008 RVA: 0x001641B0 File Offset: 0x001623B0
	public void OnClickEnemyBtn()
	{
		if (this.curPageIndex == 2)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendApplyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MailUIRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EnemyUIRoot, delegate(bool bSuccess, object param)
		{
			WaitResponseUIRootLogic.OpenWaitBox(126, 10f, 0f, null);
			request_update_friend_useinfo.request request = new request_update_friend_useinfo.request();
			request.type = 1L;
			NetLogic.GetInstance().Send<Protocol.request_update_friend_useinfo>(request, null);
			SingletonUnity<EnemyUIRootLogic>.Instance.EnableReset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(2);
		this.curPageIndex = 2;
	}

	// Token: 0x06004659 RID: 18009 RVA: 0x00164238 File Offset: 0x00162438
	public void OnClickFriendListBtn()
	{
		if (this.curPageIndex == 3)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MailUIRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FriendApplyUIRootLogic, delegate
		{
			SingletonUnity<FriendApplyUIRootLogic>.Instance.UpdateFriendList();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
		this.curPageIndex = 3;
	}

	// Token: 0x0600465A RID: 18010 RVA: 0x001642C0 File Offset: 0x001624C0
	public void OnClickMailBtn()
	{
		if (this.curPageIndex == 0)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendApplyUIRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MailUIRootLogic, delegate
		{
			SingletonUnity<MailUIRootLogic>.Instance.Reset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.curPageIndex = 0;
	}

	// Token: 0x0600465B RID: 18011 RVA: 0x00164348 File Offset: 0x00162548
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendApplyUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MailUIRootLogic);
		this.CloseSocialUI();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
	}

	// Token: 0x0600465C RID: 18012 RVA: 0x001643A8 File Offset: 0x001625A8
	public void ShowSocialUI()
	{
		if (this.state == 0)
		{
			this.InitSocialUI();
			this.state = 1;
		}
	}

	// Token: 0x0600465D RID: 18013 RVA: 0x001643C4 File Offset: 0x001625C4
	public void CloseSocialUI()
	{
		this.state = 0;
	}

	// Token: 0x0600465E RID: 18014 RVA: 0x001643D0 File Offset: 0x001625D0
	public void UpdateSocialInfo()
	{
		if (SingletonUnity<FriendAddUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FriendAddUILogic>.Instance.gameObject))
		{
			SingletonUnity<FriendAddUILogic>.Instance.UpdateFriendList();
		}
		if (SingletonUnity<FriendUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FriendUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FriendUIRootLogic>.Instance.UpdateFriendList();
		}
		if (SingletonUnity<MailUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MailUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MailUIRootLogic>.Instance.UpdateMailList();
		}
		if (SingletonUnity<FriendApplyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FriendApplyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FriendApplyUIRootLogic>.Instance.UpdateFriendList();
		}
		if (SingletonUnity<EnemyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EnemyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<EnemyUIRootLogic>.Instance.UpdateEnemyList();
		}
	}

	// Token: 0x04003367 RID: 13159
	private int curPageIndex = -1;

	// Token: 0x04003368 RID: 13160
	private int state;
}
