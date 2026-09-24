using System;

// Token: 0x0200012E RID: 302
public class Hyperlink
{
	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00052B24 File Offset: 0x00050D24
	// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00052B2C File Offset: 0x00050D2C
	public string HyperlinkName
	{
		get
		{
			return this.mHyperlinkName;
		}
		set
		{
			this.mHyperlinkName = value;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00052B38 File Offset: 0x00050D38
	public HyperlinkType HyperlinkType
	{
		get
		{
			return this.mHyperlinkType;
		}
	}

	// Token: 0x04000A23 RID: 2595
	private string mHyperlinkName;

	// Token: 0x04000A24 RID: 2596
	private HyperlinkType mHyperlinkType;
}
