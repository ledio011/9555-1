using System;
using UnityEngine;

// Token: 0x02000857 RID: 2135
public class GuildLogListItemLogic : MonoBehaviour
{
	// Token: 0x0600374F RID: 14159 RVA: 0x000E3CA8 File Offset: 0x000E1EA8
	private void Start()
	{
	}

	// Token: 0x06003750 RID: 14160 RVA: 0x000E3CAC File Offset: 0x000E1EAC
	private void Update()
	{
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x000E3CB0 File Offset: 0x000E1EB0
	public void InitLogINfo(string log)
	{
		this.LogInfo.text = this.GetStringInfo(log);
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x000E3CC4 File Offset: 0x000E1EC4
	private string GetStringInfo(string str)
	{
		string[] array = str.Split(new char[]
		{
			'_'
		});
		string text = DateTimeTool.LongToDateTimeLocal(long.Parse(array[2])).ToString();
		return string.Format("{0} {1} this Gang {2}!", array[0], array[1], text);
	}

	// Token: 0x04002481 RID: 9345
	public UILabel LogInfo;
}
