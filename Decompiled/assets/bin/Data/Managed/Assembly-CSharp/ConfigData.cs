using System;

// Token: 0x0200015B RID: 347
public class ConfigData
{
	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00061A28 File Offset: 0x0005FC28
	public float Valuef
	{
		get
		{
			if (this.Type == 0)
			{
				return float.Parse(this.ContentValue);
			}
			return 1f;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00061A48 File Offset: 0x0005FC48
	public int Valuei
	{
		get
		{
			if (this.Type == 1)
			{
				return int.Parse(this.ContentValue);
			}
			return 1;
		}
	}

	// Token: 0x04000D80 RID: 3456
	public string Key;

	// Token: 0x04000D81 RID: 3457
	public int Type;

	// Token: 0x04000D82 RID: 3458
	public string ContentValue;
}
