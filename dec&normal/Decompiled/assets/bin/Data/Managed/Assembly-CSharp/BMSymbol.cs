using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
[Serializable]
public class BMSymbol
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600029A RID: 666 RVA: 0x00012770 File Offset: 0x00010970
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600029B RID: 667 RVA: 0x000127A0 File Offset: 0x000109A0
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600029C RID: 668 RVA: 0x000127A8 File Offset: 0x000109A8
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600029D RID: 669 RVA: 0x000127B0 File Offset: 0x000109B0
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600029E RID: 670 RVA: 0x000127B8 File Offset: 0x000109B8
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600029F RID: 671 RVA: 0x000127C0 File Offset: 0x000109C0
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x000127C8 File Offset: 0x000109C8
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x000127D0 File Offset: 0x000109D0
	public void MarkAsChanged()
	{
		this.mIsValid = false;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x000127DC File Offset: 0x000109DC
	public bool Validate(UIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = ((!(atlas != null)) ? null : atlas.GetSprite(this.spriteName));
			if (this.mSprite != null)
			{
				Texture texture = atlas.texture;
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					this.mUV = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
					this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					this.mOffsetX = this.mSprite.paddingLeft;
					this.mOffsetY = this.mSprite.paddingTop;
					this.mWidth = this.mSprite.width;
					this.mHeight = this.mSprite.height;
					this.mAdvance = this.mSprite.width + (this.mSprite.paddingLeft + this.mSprite.paddingRight);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x040002F9 RID: 761
	public string sequence;

	// Token: 0x040002FA RID: 762
	public string spriteName;

	// Token: 0x040002FB RID: 763
	private UISpriteData mSprite;

	// Token: 0x040002FC RID: 764
	private bool mIsValid;

	// Token: 0x040002FD RID: 765
	private int mLength;

	// Token: 0x040002FE RID: 766
	private int mOffsetX;

	// Token: 0x040002FF RID: 767
	private int mOffsetY;

	// Token: 0x04000300 RID: 768
	private int mWidth;

	// Token: 0x04000301 RID: 769
	private int mHeight;

	// Token: 0x04000302 RID: 770
	private int mAdvance;

	// Token: 0x04000303 RID: 771
	private Rect mUV;
}
