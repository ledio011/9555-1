using System;

// Token: 0x02000152 RID: 338
public class BuffInfoData
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x000615B4 File Offset: 0x0005F7B4
	public BUFF_TYPE BufType
	{
		get
		{
			return (BUFF_TYPE)this.BuffType;
		}
	}

	// Token: 0x04000D1B RID: 3355
	public string ID = string.Empty;

	// Token: 0x04000D1C RID: 3356
	public string Name = string.Empty;

	// Token: 0x04000D1D RID: 3357
	[ServerExclude("ServerNoUse")]
	public string Icon = string.Empty;

	// Token: 0x04000D1E RID: 3358
	[ServerExclude("ServerNoUse")]
	public string Description = string.Empty;

	// Token: 0x04000D1F RID: 3359
	public string Action = string.Empty;

	// Token: 0x04000D20 RID: 3360
	public string Effect = string.Empty;

	// Token: 0x04000D21 RID: 3361
	public int Flags;

	// Token: 0x04000D22 RID: 3362
	public int Priority;

	// Token: 0x04000D23 RID: 3363
	public int Group;

	// Token: 0x04000D24 RID: 3364
	public int MaxStack;

	// Token: 0x04000D25 RID: 3365
	public int BuffType = -1;

	// Token: 0x04000D26 RID: 3366
	public int AttrID;

	// Token: 0x04000D27 RID: 3367
	public int AttrValue;

	// Token: 0x04000D28 RID: 3368
	public int AttrType;
}
