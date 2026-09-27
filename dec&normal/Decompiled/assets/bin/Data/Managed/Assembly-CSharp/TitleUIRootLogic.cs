using System;
using System.Collections.Generic;

// Token: 0x02000A24 RID: 2596
public class TitleUIRootLogic : SingletonUnity<TitleUIRootLogic>
{
	// Token: 0x06004AFF RID: 19199 RVA: 0x0018C5D8 File Offset: 0x0018A7D8
	public void InitTitleUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickPlayerTitleBtn), true, "CZ_left_Character", StrDictionary.GetDictionaryString("#{101701}", new object[0]), FUNCTION_TYPE.TITLE_TITLE, new DelegateDefine.NoParamReturnDelegate(this.CheckTitleTips));
			list.Add(menuTabBtnInfo);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curPageIndex = -1;
			SingletonUnity<MenuBaseRootLogic>.Instance.mCurBtnList[0].OnClickBtn();
		}, null);
	}

	// Token: 0x06004B00 RID: 19200 RVA: 0x0018C5F8 File Offset: 0x0018A7F8
	public void OnClickPlayerTitleBtn()
	{
		if (this.curPageIndex == 0)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GameMenuShengWangRootUI, delegate
		{
			SingletonUnity<JSShengWangLogic>.Instance.Reset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.curPageIndex = 0;
	}

	// Token: 0x06004B01 RID: 19201 RVA: 0x0018C650 File Offset: 0x0018A850
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TitleUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuShengWangRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
	}

	// Token: 0x06004B02 RID: 19202 RVA: 0x0018C68C File Offset: 0x0018A88C
	private void OnEnable()
	{
		this.InitTitleUI();
	}

	// Token: 0x06004B03 RID: 19203 RVA: 0x0018C694 File Offset: 0x0018A894
	public bool CheckTitleTips()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int curTitleExp = playerData.MainPlayerAttrData.CurTitleExp;
		int curTitleLevel = playerData.MainPlayerAttrData.CurTitleLevel;
		if (curTitleLevel < 10 && curTitleLevel >= 0)
		{
			TitleData titleDateById = DataManager.GetTitleDateById(curTitleLevel.ToString());
			return curTitleExp >= titleDateById.EXP;
		}
		return false;
	}

	// Token: 0x0400389C RID: 14492
	private int curPageIndex = -1;
}
