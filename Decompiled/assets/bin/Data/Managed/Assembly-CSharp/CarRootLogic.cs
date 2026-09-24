using System;

// Token: 0x02000925 RID: 2341
public class CarRootLogic : SingletonUnity<CarRootLogic>
{
	// Token: 0x06004107 RID: 16647 RVA: 0x00134560 File Offset: 0x00132760
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(null, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), true, StrDictionary.GetDictionaryString("#{100121}", new object[0]));
		}, null);
	}

	// Token: 0x06004108 RID: 16648 RVA: 0x00134580 File Offset: 0x00132780
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
	}
}
