using System;
using System.Collections.Generic;

// Token: 0x02000081 RID: 129
[Serializable]
public class BMGlyph
{
	// Token: 0x06000296 RID: 662 RVA: 0x000125B4 File Offset: 0x000107B4
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null && previousChar != 0)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00012614 File Offset: 0x00010814
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00012690 File Offset: 0x00010890
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x040002EF RID: 751
	public int index;

	// Token: 0x040002F0 RID: 752
	public int x;

	// Token: 0x040002F1 RID: 753
	public int y;

	// Token: 0x040002F2 RID: 754
	public int width;

	// Token: 0x040002F3 RID: 755
	public int height;

	// Token: 0x040002F4 RID: 756
	public int offsetX;

	// Token: 0x040002F5 RID: 757
	public int offsetY;

	// Token: 0x040002F6 RID: 758
	public int advance;

	// Token: 0x040002F7 RID: 759
	public int channel;

	// Token: 0x040002F8 RID: 760
	public List<int> kerning;
}
