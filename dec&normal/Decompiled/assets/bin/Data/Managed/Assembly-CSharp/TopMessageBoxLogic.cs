using System;

// Token: 0x02000A53 RID: 2643
public class TopMessageBoxLogic : SingletonUnity<TopMessageBoxLogic>
{
	// Token: 0x06004D04 RID: 19716 RVA: 0x001A3268 File Offset: 0x001A1468
	public void Clear()
	{
		this.onClickNo = null;
		this.onClickYes = null;
	}

	// Token: 0x06004D05 RID: 19717 RVA: 0x001A3278 File Offset: 0x001A1478
	public void OpenOKCancelBox(string text, string title, DelegateDefine.NoParamDelegate delOnYesClick = null, DelegateDefine.NoParamDelegate delOnCancelClick = null, string yesStr = null, string noStr = null)
	{
		this.titlelabel.text = StrDictionary.GetDictionaryString(title, new object[0]);
		this.infoLabel.text = StrDictionary.GetDictionaryString(text, new object[0]);
		this.yesLabel.text = StrDictionary.GetDictionaryString(yesStr, new object[0]);
		this.noLabel.text = StrDictionary.GetDictionaryString(noStr, new object[0]);
		this.onClickYes = delOnYesClick;
		this.onClickNo = delOnCancelClick;
	}

	// Token: 0x06004D06 RID: 19718 RVA: 0x001A32F4 File Offset: 0x001A14F4
	public void OnClickYesBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TopMessageBoxUI);
		if (this.onClickYes != null)
		{
			this.onClickYes();
		}
	}

	// Token: 0x06004D07 RID: 19719 RVA: 0x001A331C File Offset: 0x001A151C
	public void OnClickNoBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TopMessageBoxUI);
		if (this.onClickNo != null)
		{
			this.onClickNo();
		}
	}

	// Token: 0x04003AA5 RID: 15013
	public UILabel titlelabel;

	// Token: 0x04003AA6 RID: 15014
	public UILabel infoLabel;

	// Token: 0x04003AA7 RID: 15015
	public UILabel yesLabel;

	// Token: 0x04003AA8 RID: 15016
	public UILabel noLabel;

	// Token: 0x04003AA9 RID: 15017
	private DelegateDefine.NoParamDelegate onClickNo;

	// Token: 0x04003AAA RID: 15018
	private DelegateDefine.NoParamDelegate onClickYes;
}
