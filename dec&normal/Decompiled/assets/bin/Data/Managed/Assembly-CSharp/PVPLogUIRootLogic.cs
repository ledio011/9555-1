using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000966 RID: 2406
public class PVPLogUIRootLogic : SingletonUnity<PVPLogUIRootLogic>
{
	// Token: 0x06004395 RID: 17301 RVA: 0x0014E9B4 File Offset: 0x0014CBB4
	private Logs GetLogs(string str)
	{
		string[] array = str.Split(new char[]
		{
			'^'
		});
		Logs result;
		try
		{
			long time = long.Parse(array[array.Length - 1]);
			string text = DateTimeTool.LongToDateTimeLocal(time).ToString();
			result = new Logs
			{
				str = string.Format("{0}    {1}", text, StrDictionary.GetServerDictionaryString(array[0])),
				time = time
			};
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06004396 RID: 17302 RVA: 0x0014EA54 File Offset: 0x0014CC54
	public void UpdataLogList(List<string> list)
	{
		if (list == null)
		{
			return;
		}
		List<Logs> list2 = new List<Logs>();
		string text = string.Empty;
		for (int i = 0; i < list.Count; i++)
		{
			Logs logs = this.GetLogs(list[i]);
			if (logs != null)
			{
				list2.Add(logs);
			}
		}
		if (list2.Count > 0)
		{
			list2.Sort((Logs x, Logs y) => (int)(x.time - y.time));
		}
		for (int j = 0; j < list2.Count; j++)
		{
			text += list2[j].str;
			if (j != list2.Count - 1)
			{
				text += "\n";
			}
		}
		this.contentLabel.UpdateNGUIText();
		NGUIText.rectHeight = 1000000;
		string empty = string.Empty;
		NGUIText.WrapText(text, out empty);
		this.contentLabel.text = empty;
		this.uIScrollView.ResetPosition();
	}

	// Token: 0x06004397 RID: 17303 RVA: 0x0014EB5C File Offset: 0x0014CD5C
	public void Reset(string titlestr, DelegateDefine.NoParamDelegate closefun = null)
	{
		this.onClosed = closefun;
		this.titleLabel.text = StrDictionary.GetDictionaryString(titlestr, new object[0]);
		WaitResponseUIRootLogic.OpenWaitBox(211, 10f, 0f, null);
		request_rank_pvp_history.request rpcReq = new request_rank_pvp_history.request();
		NetLogic.GetInstance().Send<Protocol.request_rank_pvp_history>(rpcReq, null);
	}

	// Token: 0x06004398 RID: 17304 RVA: 0x0014EBB0 File Offset: 0x0014CDB0
	public void OnlClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PVPLogUIRoot);
		if (this.onClosed != null)
		{
			this.onClosed();
		}
	}

	// Token: 0x04003037 RID: 12343
	public UILabel titleLabel;

	// Token: 0x04003038 RID: 12344
	public UIGrid LogListGrid;

	// Token: 0x04003039 RID: 12345
	public UILabel contentLabel;

	// Token: 0x0400303A RID: 12346
	public UIScrollView uIScrollView;

	// Token: 0x0400303B RID: 12347
	private DelegateDefine.NoParamDelegate onClosed;
}
