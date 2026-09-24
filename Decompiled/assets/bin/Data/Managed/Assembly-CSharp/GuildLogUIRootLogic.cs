using System;
using System.Collections.Generic;

// Token: 0x02000859 RID: 2137
public class GuildLogUIRootLogic : SingletonUnity<GuildLogUIRootLogic>
{
	// Token: 0x06003755 RID: 14165 RVA: 0x000E3D1C File Offset: 0x000E1F1C
	private Logs GetLogs(string str)
	{
		string[] array = str.Split(new char[]
		{
			'^'
		});
		Logs result;
		try
		{
			long time = long.Parse(array[array.Length - 2]);
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

	// Token: 0x06003756 RID: 14166 RVA: 0x000E3DBC File Offset: 0x000E1FBC
	private string GetStringInfo(string str)
	{
		string[] array = str.Split(new char[]
		{
			'^'
		});
		string result;
		try
		{
			string text = DateTimeTool.LongToDateTimeLocal(long.Parse(array[array.Length - 2])).ToString();
			result = string.Format("{0}    {1}", text, StrDictionary.GetServerDictionaryString(array[0]));
		}
		catch (Exception ex)
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x06003757 RID: 14167 RVA: 0x000E3E44 File Offset: 0x000E2044
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
		this.contentLabel.text = text;
		this.scrollView.ResetPosition();
	}

	// Token: 0x06003758 RID: 14168 RVA: 0x000E3F28 File Offset: 0x000E2128
	public void OnlClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildLogUIRootLogic);
	}

	// Token: 0x04002484 RID: 9348
	public UILabel contentLabel;

	// Token: 0x04002485 RID: 9349
	public UIScrollView scrollView;
}
