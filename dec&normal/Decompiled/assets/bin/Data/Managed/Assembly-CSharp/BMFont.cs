using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000080 RID: 128
[Serializable]
public class BMFont
{
	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000284 RID: 644 RVA: 0x000123FC File Offset: 0x000105FC
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000285 RID: 645 RVA: 0x0001240C File Offset: 0x0001060C
	// (set) Token: 0x06000286 RID: 646 RVA: 0x00012414 File Offset: 0x00010614
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000287 RID: 647 RVA: 0x00012420 File Offset: 0x00010620
	// (set) Token: 0x06000288 RID: 648 RVA: 0x00012428 File Offset: 0x00010628
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000289 RID: 649 RVA: 0x00012434 File Offset: 0x00010634
	// (set) Token: 0x0600028A RID: 650 RVA: 0x0001243C File Offset: 0x0001063C
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600028B RID: 651 RVA: 0x00012448 File Offset: 0x00010648
	// (set) Token: 0x0600028C RID: 652 RVA: 0x00012450 File Offset: 0x00010650
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600028D RID: 653 RVA: 0x0001245C File Offset: 0x0001065C
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600028E RID: 654 RVA: 0x0001247C File Offset: 0x0001067C
	// (set) Token: 0x0600028F RID: 655 RVA: 0x00012484 File Offset: 0x00010684
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000290 RID: 656 RVA: 0x00012490 File Offset: 0x00010690
	public List<BMGlyph> glyphs
	{
		get
		{
			return this.mSaved;
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00012498 File Offset: 0x00010698
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, ref bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00012534 File Offset: 0x00010734
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00012540 File Offset: 0x00010740
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00012558 File Offset: 0x00010758
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x040002E8 RID: 744
	[SerializeField]
	[HideInInspector]
	private int mSize = 16;

	// Token: 0x040002E9 RID: 745
	[SerializeField]
	[HideInInspector]
	private int mBase;

	// Token: 0x040002EA RID: 746
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x040002EB RID: 747
	[SerializeField]
	[HideInInspector]
	private int mHeight;

	// Token: 0x040002EC RID: 748
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x040002ED RID: 749
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x040002EE RID: 750
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
