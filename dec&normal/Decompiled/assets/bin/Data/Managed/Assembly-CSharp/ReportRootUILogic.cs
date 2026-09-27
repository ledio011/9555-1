using System;
using System.Text.RegularExpressions;
using SprotoType;

// Token: 0x02000A40 RID: 2624
public class ReportRootUILogic : SingletonUnity<ReportRootUILogic>
{
	// Token: 0x06004C8E RID: 19598 RVA: 0x0019F1A8 File Offset: 0x0019D3A8
	public void OnClickSendContent()
	{
		string value = this.InputEmail.value;
		string value2 = this.InputTitle.value;
		string value3 = this.InputContent.value;
		if (string.IsNullOrEmpty(value))
		{
			NoticeLogic.AddNotifyData("#{705007}", true, false);
			return;
		}
		if (value.Length < 4 || value.Length > 141)
		{
			NoticeLogic.AddNotifyData("#{705006}", true, false);
			return;
		}
		Regex regex = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
		if (!regex.IsMatch(value))
		{
			NoticeLogic.AddNotifyData("#{705006}", true, false);
			return;
		}
		if (string.IsNullOrEmpty(value2))
		{
			NoticeLogic.AddNotifyData("#{705008}", true, false);
			return;
		}
		if (value2.Length < 5 || value2.Length > 140)
		{
			NoticeLogic.AddNotifyData("#{705008}", true, false);
			return;
		}
		if (string.IsNullOrEmpty(value3))
		{
			NoticeLogic.AddNotifyData("#{705009}", true, false);
			return;
		}
		if (value3.Length < 50 || value3.Length > 1500)
		{
			NoticeLogic.AddNotifyData("#{705009}", true, false);
			return;
		}
		send_mail_box.request request = new send_mail_box.request();
		request.subject = value2;
		request.context = value3;
		request.email = value;
		NetLogic.GetInstance().Send<Protocol.send_mail_box>(request, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ReportRoot);
		NoticeLogic.AddNotifyData("#{705010}", true, false);
	}

	// Token: 0x06004C8F RID: 19599 RVA: 0x0019F308 File Offset: 0x0019D508
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ReportRoot);
	}

	// Token: 0x04003A35 RID: 14901
	private const int MAX_CONTENT = 1500;

	// Token: 0x04003A36 RID: 14902
	private const int MAX_Title = 140;

	// Token: 0x04003A37 RID: 14903
	private const int MAX_EMAIL = 141;

	// Token: 0x04003A38 RID: 14904
	public UIInput InputEmail;

	// Token: 0x04003A39 RID: 14905
	public UIInput InputTitle;

	// Token: 0x04003A3A RID: 14906
	public UIInput InputContent;
}
