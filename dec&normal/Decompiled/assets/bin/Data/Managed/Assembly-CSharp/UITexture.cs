using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
[AddComponentMenu("NGUI/UI/NGUI Texture")]
[ExecuteInEditMode]
public class UITexture : UIWidget
{
	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00030A40 File Offset: 0x0002EC40
	// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00030A48 File Offset: 0x0002EC48
	public override Texture mainTexture
	{
		get
		{
			return this.mTexture;
		}
		set
		{
			if (this.mTexture != value)
			{
				base.RemoveFromPanel();
				this.mTexture = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00030A7C File Offset: 0x0002EC7C
	// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00030A84 File Offset: 0x0002EC84
	public override Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				base.RemoveFromPanel();
				this.mShader = null;
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x060006CA RID: 1738 RVA: 0x00030AC4 File Offset: 0x0002ECC4
	// (set) Token: 0x060006CB RID: 1739 RVA: 0x00030B18 File Offset: 0x0002ED18
	public override Shader shader
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat.shader;
			}
			if (this.mShader == null)
			{
				this.mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				base.RemoveFromPanel();
				this.mShader = value;
				this.mPMA = -1;
				this.mMat = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x00030B58 File Offset: 0x0002ED58
	// (set) Token: 0x060006CD RID: 1741 RVA: 0x00030B60 File Offset: 0x0002ED60
	public UITexture.Flip flip
	{
		get
		{
			return this.mFlip;
		}
		set
		{
			if (this.mFlip != value)
			{
				this.mFlip = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x060006CE RID: 1742 RVA: 0x00030B7C File Offset: 0x0002ED7C
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((!(material != null) || !(material.shader != null) || !material.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x060006CF RID: 1743 RVA: 0x00030BEC File Offset: 0x0002EDEC
	// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00030BF4 File Offset: 0x0002EDF4
	public Rect uvRect
	{
		get
		{
			return this.mRect;
		}
		set
		{
			if (this.mRect != value)
			{
				this.mRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00030C14 File Offset: 0x0002EE14
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			Texture mainTexture = this.mainTexture;
			int num5 = (!(mainTexture != null)) ? this.mWidth : mainTexture.width;
			int num6 = (!(mainTexture != null)) ? this.mHeight : mainTexture.height;
			if ((num5 & 1) != 0)
			{
				num3 -= 1f / (float)num5 * (float)this.mWidth;
			}
			if ((num6 & 1) != 0)
			{
				num4 -= 1f / (float)num6 * (float)this.mHeight;
			}
			return new Vector4((this.mDrawRegion.x != 0f) ? Mathf.Lerp(num, num3, this.mDrawRegion.x) : num, (this.mDrawRegion.y != 0f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.y) : num2, (this.mDrawRegion.z != 1f) ? Mathf.Lerp(num, num3, this.mDrawRegion.z) : num3, (this.mDrawRegion.w != 1f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.w) : num4);
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00030DA4 File Offset: 0x0002EFA4
	public override void MakePixelPerfect()
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture != null)
		{
			int num = mainTexture.width;
			if ((num & 1) == 1)
			{
				num++;
			}
			int num2 = mainTexture.height;
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00030E00 File Offset: 0x0002F000
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 item = (!this.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		Vector4 drawingDimensions = this.drawingDimensions;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		if (this.mFlip == UITexture.Flip.Horizontally)
		{
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMin));
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMin));
		}
		else if (this.mFlip == UITexture.Flip.Vertically)
		{
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMin));
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMin));
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMax));
		}
		else if (this.mFlip == UITexture.Flip.Both)
		{
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMin));
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMin));
		}
		else
		{
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMin));
			uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMax));
			uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMin));
		}
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
	}

	// Token: 0x040005F3 RID: 1523
	[SerializeField]
	[HideInInspector]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040005F4 RID: 1524
	[SerializeField]
	[HideInInspector]
	private Texture mTexture;

	// Token: 0x040005F5 RID: 1525
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x040005F6 RID: 1526
	[SerializeField]
	[HideInInspector]
	private Shader mShader;

	// Token: 0x040005F7 RID: 1527
	[HideInInspector]
	[SerializeField]
	private UITexture.Flip mFlip;

	// Token: 0x040005F8 RID: 1528
	private int mPMA = -1;

	// Token: 0x020000DB RID: 219
	public enum Flip
	{
		// Token: 0x040005FA RID: 1530
		Nothing,
		// Token: 0x040005FB RID: 1531
		Horizontally,
		// Token: 0x040005FC RID: 1532
		Vertically,
		// Token: 0x040005FD RID: 1533
		Both
	}
}
