using System;

// Token: 0x02000199 RID: 409
public class NpcOptionDialogData
{
	// Token: 0x1700034E RID: 846
	// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00064C10 File Offset: 0x00062E10
	public string MCenterDialog
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.CenterDialog, new object[0]);
		}
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00064C24 File Offset: 0x00062E24
	public string MOption1
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Option1, new object[0]);
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00064C38 File Offset: 0x00062E38
	public string MOption2
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Option2, new object[0]);
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00064C4C File Offset: 0x00062E4C
	public string MOptionFailDialog
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.OptionFailDialog, new object[0]);
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00064C60 File Offset: 0x00062E60
	public string MFailOption
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.FailOption, new object[0]);
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00064C74 File Offset: 0x00062E74
	public OPTION_TYPE Type
	{
		get
		{
			return (OPTION_TYPE)this.OptionType;
		}
	}

	// Token: 0x04001188 RID: 4488
	public string ID = string.Empty;

	// Token: 0x04001189 RID: 4489
	public string CenterDialog = string.Empty;

	// Token: 0x0400118A RID: 4490
	public string Option1 = string.Empty;

	// Token: 0x0400118B RID: 4491
	public string Option2 = string.Empty;

	// Token: 0x0400118C RID: 4492
	public int OptionType = -1;

	// Token: 0x0400118D RID: 4493
	public string OptionParam = string.Empty;

	// Token: 0x0400118E RID: 4494
	public string OptionFailDialog = string.Empty;

	// Token: 0x0400118F RID: 4495
	public string FailOption = string.Empty;
}
