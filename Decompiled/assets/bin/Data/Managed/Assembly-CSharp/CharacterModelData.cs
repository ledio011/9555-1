using System;

// Token: 0x02000158 RID: 344
public class CharacterModelData
{
	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00061920 File Offset: 0x0005FB20
	public float ModelHeight
	{
		get
		{
			return (float)this.Height / 100f;
		}
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00061930 File Offset: 0x0005FB30
	public void Init()
	{
		this.mdx.value = (float)this.Radius / 100f;
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0006194C File Offset: 0x0005FB4C
	public float ModelRadius
	{
		get
		{
			return this.mdx.value;
		}
	}

	// Token: 0x04000D5A RID: 3418
	public string ID;

	// Token: 0x04000D5B RID: 3419
	[ServerExclude("ServerNoUse")]
	public string Name;

	// Token: 0x04000D5C RID: 3420
	public string IndexName;

	// Token: 0x04000D5D RID: 3421
	public int TypeID;

	// Token: 0x04000D5E RID: 3422
	public int Radius;

	// Token: 0x04000D5F RID: 3423
	public int Height;

	// Token: 0x04000D60 RID: 3424
	[ServerExclude("ServerNoUse")]
	public string ModelFirstType = string.Empty;

	// Token: 0x04000D61 RID: 3425
	[ServerExclude("ServerNoUse")]
	public string ModelSubType = string.Empty;

	// Token: 0x04000D62 RID: 3426
	[ServerExclude("ServerNoUse")]
	public string ModelType;

	// Token: 0x04000D63 RID: 3427
	private XorFloat mdx = new XorFloat();
}
