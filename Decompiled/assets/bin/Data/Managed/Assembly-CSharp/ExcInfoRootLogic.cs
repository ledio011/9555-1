using System;

// Token: 0x0200093A RID: 2362
public class ExcInfoRootLogic : SingletonUnity<ExcInfoRootLogic>
{
	// Token: 0x060041B5 RID: 16821 RVA: 0x00139C00 File Offset: 0x00137E00
	public void Reset(string titlestr, string infostr, DelegateDefine.NoParamDelegate closefun = null, params object[] args)
	{
		this.titleLabel.text = StrDictionary.GetDictionaryString(titlestr, new object[0]);
		this.InfoLabel.text = StrDictionary.GetDictionaryString(infostr, args);
		this.CurScrollView.ResetPosition();
		this.onClosed = closefun;
	}

	// Token: 0x060041B6 RID: 16822 RVA: 0x00139C4C File Offset: 0x00137E4C
	public void Close()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ExcInfoRoot);
		if (this.onClosed != null)
		{
			this.onClosed();
		}
	}

	// Token: 0x04002D97 RID: 11671
	public UIScrollView CurScrollView;

	// Token: 0x04002D98 RID: 11672
	public UILabel InfoLabel;

	// Token: 0x04002D99 RID: 11673
	public UILabel titleLabel;

	// Token: 0x04002D9A RID: 11674
	private DelegateDefine.NoParamDelegate onClosed;
}
