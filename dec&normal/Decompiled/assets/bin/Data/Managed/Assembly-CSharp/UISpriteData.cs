using System;

// Token: 0x020000D3 RID: 211
[Serializable]
public class UISpriteData
{
	// Token: 0x17000145 RID: 325
	// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0002F900 File Offset: 0x0002DB00
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0002F924 File Offset: 0x0002DB24
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0002F948 File Offset: 0x0002DB48
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0002F968 File Offset: 0x0002DB68
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0002F988 File Offset: 0x0002DB88
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
	public void CopyFrom(UISpriteData sd)
	{
		this.name = sd.name;
		this.x = sd.x;
		this.y = sd.y;
		this.width = sd.width;
		this.height = sd.height;
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
		this.paddingLeft = sd.paddingLeft;
		this.paddingRight = sd.paddingRight;
		this.paddingTop = sd.paddingTop;
		this.paddingBottom = sd.paddingBottom;
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0002FA54 File Offset: 0x0002DC54
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x040005BE RID: 1470
	public string name = "Sprite";

	// Token: 0x040005BF RID: 1471
	public int x;

	// Token: 0x040005C0 RID: 1472
	public int y;

	// Token: 0x040005C1 RID: 1473
	public int width;

	// Token: 0x040005C2 RID: 1474
	public int height;

	// Token: 0x040005C3 RID: 1475
	public int borderLeft;

	// Token: 0x040005C4 RID: 1476
	public int borderRight;

	// Token: 0x040005C5 RID: 1477
	public int borderTop;

	// Token: 0x040005C6 RID: 1478
	public int borderBottom;

	// Token: 0x040005C7 RID: 1479
	public int paddingLeft;

	// Token: 0x040005C8 RID: 1480
	public int paddingRight;

	// Token: 0x040005C9 RID: 1481
	public int paddingTop;

	// Token: 0x040005CA RID: 1482
	public int paddingBottom;
}
