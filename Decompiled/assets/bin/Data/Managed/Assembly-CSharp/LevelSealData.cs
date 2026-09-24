using System;

// Token: 0x02000181 RID: 385
public class LevelSealData
{
	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0006352C File Offset: 0x0006172C
	public int Inhibit
	{
		get
		{
			return this.inhibit / 100;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00063538 File Offset: 0x00061738
	public int Encourage
	{
		get
		{
			return this.encourage / 100;
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00063544 File Offset: 0x00061744
	public float InhibitRatio
	{
		get
		{
			return (float)this.inhibit / 10000f;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00063554 File Offset: 0x00061754
	public float EncourageRatio
	{
		get
		{
			return (float)this.encourage / 10000f;
		}
	}

	// Token: 0x04000FE0 RID: 4064
	public string ID;

	// Token: 0x04000FE1 RID: 4065
	public int inhibit;

	// Token: 0x04000FE2 RID: 4066
	public int encourage;
}
