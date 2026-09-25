using System;

// Token: 0x0200014A RID: 330
public class AnnounceData
{
	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0006117C File Offset: 0x0005F37C
	public string MName1
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name1, new object[0]);
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00061190 File Offset: 0x0005F390
	public string MName2
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name2, new object[0]);
		}
	}

	// Token: 0x04000C7D RID: 3197
	public string ID;

	// Token: 0x04000C7E RID: 3198
	public string ICON;

	// Token: 0x04000C7F RID: 3199
	public string Name1;

	// Token: 0x04000C80 RID: 3200
	public string Name2;

	// Token: 0x04000C81 RID: 3201
	public int StartLevel;

	// Token: 0x04000C82 RID: 3202
	public int EndLevel;

	// Token: 0x04000C83 RID: 3203
	public int Type;

	// Token: 0x04000C84 RID: 3204
	public string ItemId = string.Empty;

	// Token: 0x04000C85 RID: 3205
	public int quality;
}
