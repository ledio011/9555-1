using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class ChatRecord : MonoBehaviour
{
	// Token: 0x06000B1F RID: 2847 RVA: 0x00052908 File Offset: 0x00050B08
	public static void AddChatRecord(string data, bool isFilterRepeate = false, long SendId = 0L)
	{
		string text = string.Empty;
		if (!string.IsNullOrEmpty(data))
		{
			char c = data.get_Chars(0);
			if (c != '#')
			{
				text = data;
			}
			else
			{
				text = StrDictionary.GetServerDictionaryString(data);
			}
		}
		if (ChatRecord.WordChatRecordList.Count > 0 && isFilterRepeate)
		{
			if (ChatRecord.WordChatRecordList[ChatRecord.WordChatRecordList.Count - 1] != text)
			{
				ChatRecord.WordChatRecordList.Add(text);
			}
		}
		else
		{
			ChatRecord.WordChatRecordList.Add(text);
		}
		ChatRecord.canCall = true;
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0005299C File Offset: 0x00050B9C
	public static string GetaRecord()
	{
		return ChatRecord.WordChatRecordList[ChatRecord.WordChatRecordList.Count - 1];
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x000529B4 File Offset: 0x00050BB4
	private void Start()
	{
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x000529B8 File Offset: 0x00050BB8
	private void Update()
	{
		if (ChatRecord.canCall)
		{
			this.UpdataToMianText();
			ChatRecord.canCall = false;
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x000529D0 File Offset: 0x00050BD0
	private void UpdataToMianText()
	{
		string text = ChatRecord.GetaRecord();
		string text2 = this.red.Replace("Some Message or Link", this.getHyperlinkType(text));
		text = text.Replace("{", text2);
		text = text.Replace("}", this.end);
		UILabel mainText = this.MainText;
		mainText.text = mainText.text + text + "\n";
		Vector3 localPosition = this.MainText.gameObject.transform.localPosition;
		localPosition.y -= 10f;
		this.MainText.gameObject.transform.localPosition = localPosition;
		Vector3 size = this.MainText.gameObject.GetComponent<BoxCollider>().size;
		size.y += 20f;
		this.MainText.gameObject.GetComponent<BoxCollider>().size = size;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00052AB4 File Offset: 0x00050CB4
	private string getHyperlinkType(string str)
	{
		for (int i = 0; i < str.Length; i++)
		{
			if (str.get_Chars(i) == '{')
			{
				for (int j = i; j < str.Length; j++)
				{
					if (str.get_Chars(j) == '}')
					{
						return str.Substring(i, j - i);
					}
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x04000A19 RID: 2585
	public UILabel MainText;

	// Token: 0x04000A1A RID: 2586
	public static bool canCall = false;

	// Token: 0x04000A1B RID: 2587
	private static List<string> WordChatRecordList = new List<string>();

	// Token: 0x04000A1C RID: 2588
	public string red = "[cc0033][u][url=Some Message or Link]";

	// Token: 0x04000A1D RID: 2589
	public string end = "[/url][/u][-]";

	// Token: 0x04000A1E RID: 2590
	public Dictionary<long, string> FriendChatRecordDic = new Dictionary<long, string>();
}
