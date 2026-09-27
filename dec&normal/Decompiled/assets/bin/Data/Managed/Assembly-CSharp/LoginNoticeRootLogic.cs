using System;

// Token: 0x020009F1 RID: 2545
public class LoginNoticeRootLogic : SingletonUnity<LoginNoticeRootLogic>
{
	// Token: 0x060048BA RID: 18618 RVA: 0x001768B8 File Offset: 0x00174AB8
	public void Reset(string infoStr)
	{
		this.InfoLabel.text = StrDictionary.GetDictionaryString(infoStr, new object[0]).Replace("#r", "\n");
	}

	// Token: 0x060048BB RID: 18619 RVA: 0x001768EC File Offset: 0x00174AEC
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginNoticeRootUI);
	}

	// Token: 0x0400360B RID: 13835
	public UILabel InfoLabel;
}
