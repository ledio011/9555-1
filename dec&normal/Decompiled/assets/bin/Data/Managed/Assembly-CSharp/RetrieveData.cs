using System;

// Token: 0x020001A2 RID: 418
public class RetrieveData
{
	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00065114 File Offset: 0x00063314
	public bool isDanceOrExp
	{
		get
		{
			return this.Type == 2 || this.Type == 4;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00065130 File Offset: 0x00063330
	public bool isGuildDance
	{
		get
		{
			return this.Type == 9;
		}
	}

	// Token: 0x040011EB RID: 4587
	public string ID;

	// Token: 0x040011EC RID: 4588
	public int Type;

	// Token: 0x040011ED RID: 4589
	public string Name;

	// Token: 0x040011EE RID: 4590
	public string ShowRewardID;

	// Token: 0x040011EF RID: 4591
	public string DropID;

	// Token: 0x040011F0 RID: 4592
	public int PriceType1;

	// Token: 0x040011F1 RID: 4593
	public int PriceCost1;

	// Token: 0x040011F2 RID: 4594
	public int AddCost1;

	// Token: 0x040011F3 RID: 4595
	public int MaxCost1;

	// Token: 0x040011F4 RID: 4596
	public int PriceType2;

	// Token: 0x040011F5 RID: 4597
	public int PriceCost2;

	// Token: 0x040011F6 RID: 4598
	public int AddCost2;

	// Token: 0x040011F7 RID: 4599
	public int MaxCost2;

	// Token: 0x040011F8 RID: 4600
	public int Scale1;

	// Token: 0x040011F9 RID: 4601
	public int Scale2;

	// Token: 0x040011FA RID: 4602
	public int UnlockLevel;
}
