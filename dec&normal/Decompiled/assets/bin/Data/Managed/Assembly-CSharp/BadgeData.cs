using System;

// Token: 0x0200014B RID: 331
public class BadgeData
{
	// Token: 0x06000EC6 RID: 3782 RVA: 0x00061204 File Offset: 0x0005F404
	public int GetBaseAttCount()
	{
		int num = 0;
		if (this.Status1 != -1)
		{
			num++;
		}
		if (this.Status2 != -1)
		{
			num++;
		}
		return num;
	}

	// Token: 0x04000C86 RID: 3206
	public string ID = string.Empty;

	// Token: 0x04000C87 RID: 3207
	public int Color = -1;

	// Token: 0x04000C88 RID: 3208
	public int BadgeType = -1;

	// Token: 0x04000C89 RID: 3209
	public int Lv = -1;

	// Token: 0x04000C8A RID: 3210
	public int Status1 = -1;

	// Token: 0x04000C8B RID: 3211
	public int Value1 = -1;

	// Token: 0x04000C8C RID: 3212
	public int Status2 = -1;

	// Token: 0x04000C8D RID: 3213
	public int Value2 = -1;

	// Token: 0x04000C8E RID: 3214
	public int MoneyType = -1;

	// Token: 0x04000C8F RID: 3215
	public int UpgradeCost;

	// Token: 0x04000C90 RID: 3216
	public int UpgradeCount = 4;
}
