using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Sprite")]
public class UISprite : UIWidget
{
	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000672 RID: 1650 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
	// (set) Token: 0x06000673 RID: 1651 RVA: 0x0002C5E8 File Offset: 0x0002A7E8
	public virtual UISprite.Type type
	{
		get
		{
			return this.mType;
		}
		set
		{
			if (this.mType != value)
			{
				this.mType = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06000674 RID: 1652 RVA: 0x0002C604 File Offset: 0x0002A804
	// (set) Token: 0x06000675 RID: 1653 RVA: 0x0002C60C File Offset: 0x0002A80C
	public UISprite.Flip flip
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

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000676 RID: 1654 RVA: 0x0002C628 File Offset: 0x0002A828
	public override Material material
	{
		get
		{
			return (!(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000677 RID: 1655 RVA: 0x0002C658 File Offset: 0x0002A858
	// (set) Token: 0x06000678 RID: 1656 RVA: 0x0002C660 File Offset: 0x0002A860
	public UIAtlas atlas
	{
		get
		{
			return this.mAtlas;
		}
		set
		{
			if (this.mAtlas != value)
			{
				base.RemoveFromPanel();
				this.mAtlas = value;
				this.mSpriteSet = false;
				this.mSprite = null;
				if (string.IsNullOrEmpty(this.mSpriteName) && this.mAtlas != null && this.mAtlas.spriteList.Count > 0)
				{
					this.SetAtlasSprite(this.mAtlas.spriteList[0]);
					this.mSpriteName = this.mSprite.name;
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string spriteName = this.mSpriteName;
					this.mSpriteName = string.Empty;
					this.spriteName = spriteName;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000679 RID: 1657 RVA: 0x0002C728 File Offset: 0x0002A928
	// (set) Token: 0x0600067A RID: 1658 RVA: 0x0002C730 File Offset: 0x0002A930
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (string.IsNullOrEmpty(this.mSpriteName))
				{
					return;
				}
				this.mSpriteName = string.Empty;
				this.mSprite = null;
				this.mChanged = true;
				this.mSpriteSet = false;
			}
			else if (this.mSpriteName != value)
			{
				this.mSpriteName = value;
				this.mSprite = null;
				this.mChanged = true;
				this.mSpriteSet = false;
			}
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x0600067B RID: 1659 RVA: 0x0002C7AC File Offset: 0x0002A9AC
	public bool isValid
	{
		get
		{
			return this.GetAtlasSprite() != null;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x0600067C RID: 1660 RVA: 0x0002C7BC File Offset: 0x0002A9BC
	// (set) Token: 0x0600067D RID: 1661 RVA: 0x0002C7CC File Offset: 0x0002A9CC
	[Obsolete("Use 'centerType' instead")]
	public bool fillCenter
	{
		get
		{
			return this.centerType != UISprite.AdvancedType.Invisible;
		}
		set
		{
			if (value != (this.centerType != UISprite.AdvancedType.Invisible))
			{
				this.centerType = ((!value) ? UISprite.AdvancedType.Invisible : UISprite.AdvancedType.Sliced);
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x0600067E RID: 1662 RVA: 0x0002C7FC File Offset: 0x0002A9FC
	// (set) Token: 0x0600067F RID: 1663 RVA: 0x0002C804 File Offset: 0x0002AA04
	public UISprite.FillDirection fillDirection
	{
		get
		{
			return this.mFillDirection;
		}
		set
		{
			if (this.mFillDirection != value)
			{
				this.mFillDirection = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000680 RID: 1664 RVA: 0x0002C820 File Offset: 0x0002AA20
	// (set) Token: 0x06000681 RID: 1665 RVA: 0x0002C828 File Offset: 0x0002AA28
	public float fillAmount
	{
		get
		{
			return this.mFillAmount;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mFillAmount != num)
			{
				this.mFillAmount = num;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000682 RID: 1666 RVA: 0x0002C858 File Offset: 0x0002AA58
	// (set) Token: 0x06000683 RID: 1667 RVA: 0x0002C860 File Offset: 0x0002AA60
	public bool invert
	{
		get
		{
			return this.mInvert;
		}
		set
		{
			if (this.mInvert != value)
			{
				this.mInvert = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000684 RID: 1668 RVA: 0x0002C87C File Offset: 0x0002AA7C
	public override Vector4 border
	{
		get
		{
			if (this.type != UISprite.Type.Sliced && this.type != UISprite.Type.Advanced)
			{
				return base.border;
			}
			UISpriteData atlasSprite = this.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return Vector2.zero;
			}
			return new Vector4((float)atlasSprite.borderLeft, (float)atlasSprite.borderBottom, (float)atlasSprite.borderRight, (float)atlasSprite.borderTop);
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000685 RID: 1669 RVA: 0x0002C8E4 File Offset: 0x0002AAE4
	public override int minWidth
	{
		get
		{
			if (this.type == UISprite.Type.Sliced || this.type == UISprite.Type.Advanced)
			{
				Vector4 vector = this.border;
				if (this.atlas != null)
				{
					vector *= this.atlas.pixelSize;
				}
				int num = Mathf.RoundToInt(vector.x + vector.z);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += atlasSprite.paddingLeft + atlasSprite.paddingRight;
				}
				return Mathf.Max(base.minWidth, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minWidth;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000686 RID: 1670 RVA: 0x0002C98C File Offset: 0x0002AB8C
	public override int minHeight
	{
		get
		{
			if (this.type == UISprite.Type.Sliced || this.type == UISprite.Type.Advanced)
			{
				Vector4 vector = this.border;
				if (this.atlas != null)
				{
					vector *= this.atlas.pixelSize;
				}
				int num = Mathf.RoundToInt(vector.y + vector.w);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += atlasSprite.paddingTop + atlasSprite.paddingBottom;
				}
				return Mathf.Max(base.minHeight, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minHeight;
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0002CA34 File Offset: 0x0002AC34
	public UISpriteData GetAtlasSprite()
	{
		if (!this.mSpriteSet)
		{
			this.mSprite = null;
		}
		if (this.mSprite == null && this.mAtlas != null)
		{
			if (!string.IsNullOrEmpty(this.mSpriteName))
			{
				UISpriteData sprite = this.mAtlas.GetSprite(this.mSpriteName);
				if (sprite == null)
				{
					return null;
				}
				this.SetAtlasSprite(sprite);
			}
			if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
			{
				UISpriteData uispriteData = this.mAtlas.spriteList[0];
				if (uispriteData == null)
				{
					return null;
				}
				this.SetAtlasSprite(uispriteData);
				if (this.mSprite == null)
				{
					Debug.LogError(this.mAtlas.name + " seems to have a null sprite!");
					return null;
				}
				this.mSpriteName = this.mSprite.name;
			}
		}
		return this.mSprite;
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0002CB20 File Offset: 0x0002AD20
	protected void SetAtlasSprite(UISpriteData sp)
	{
		this.mChanged = true;
		this.mSpriteSet = true;
		if (sp != null)
		{
			this.mSprite = sp;
			this.mSpriteName = this.mSprite.name;
		}
		else
		{
			this.mSpriteName = ((this.mSprite == null) ? string.Empty : this.mSprite.name);
			this.mSprite = sp;
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002CB8C File Offset: 0x0002AD8C
	public override void MakePixelPerfect()
	{
		if (!this.isValid)
		{
			return;
		}
		base.MakePixelPerfect();
		UISpriteData atlasSprite = this.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		UISprite.Type type = this.type;
		if (type == UISprite.Type.Simple || type == UISprite.Type.Filled || !atlasSprite.hasBorder)
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null && atlasSprite != null)
			{
				int num = Mathf.RoundToInt(this.atlas.pixelSize * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
				int num2 = Mathf.RoundToInt(this.atlas.pixelSize * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				base.width = num;
				base.height = num2;
			}
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002CC6C File Offset: 0x0002AE6C
	protected override void OnInit()
	{
		if (!this.mFillCenter)
		{
			this.mFillCenter = true;
			this.centerType = UISprite.AdvancedType.Invisible;
		}
		base.OnInit();
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0002CC90 File Offset: 0x0002AE90
	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (this.mChanged || !this.mSpriteSet)
		{
			this.mSpriteSet = true;
			this.mSprite = null;
			this.mChanged = true;
		}
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0002CCC4 File Offset: 0x0002AEC4
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if (this.mSprite == null)
		{
			this.mSprite = this.atlas.GetSprite(this.spriteName);
		}
		if (this.mSprite == null)
		{
			return;
		}
		this.mOuterUV.Set((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
		this.mInnerUV.Set((float)(this.mSprite.x + this.mSprite.borderLeft), (float)(this.mSprite.y + this.mSprite.borderTop), (float)(this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float)(this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
		this.mOuterUV = NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
		this.mInnerUV = NGUIMath.ConvertToTexCoords(this.mInnerUV, mainTexture.width, mainTexture.height);
		switch (this.type)
		{
		case UISprite.Type.Simple:
			this.SimpleFill(verts, uvs, cols);
			break;
		case UISprite.Type.Sliced:
			this.SlicedFill(verts, uvs, cols);
			break;
		case UISprite.Type.Tiled:
			this.TiledFill(verts, uvs, cols);
			break;
		case UISprite.Type.Filled:
			this.FilledFill(verts, uvs, cols);
			break;
		case UISprite.Type.Advanced:
			this.AdvancedFill(verts, uvs, cols);
			break;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x0002CE78 File Offset: 0x0002B078
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.GetAtlasSprite() != null && this.mType != UISprite.Type.Tiled)
			{
				int paddingLeft = this.mSprite.paddingLeft;
				int paddingBottom = this.mSprite.paddingBottom;
				int num5 = this.mSprite.paddingRight;
				int num6 = this.mSprite.paddingTop;
				int num7 = this.mSprite.width + paddingLeft + num5;
				int num8 = this.mSprite.height + paddingBottom + num6;
				float num9 = 1f;
				float num10 = 1f;
				if (num7 > 0 && num8 > 0 && (this.mType == UISprite.Type.Simple || this.mType == UISprite.Type.Filled))
				{
					if ((num7 & 1) != 0)
					{
						num5++;
					}
					if ((num8 & 1) != 0)
					{
						num6++;
					}
					num9 = 1f / (float)num7 * (float)this.mWidth;
					num10 = 1f / (float)num8 * (float)this.mHeight;
				}
				if (this.mFlip == UISprite.Flip.Horizontally || this.mFlip == UISprite.Flip.Both)
				{
					num += (float)num5 * num9;
					num3 -= (float)paddingLeft * num9;
				}
				else
				{
					num += (float)paddingLeft * num9;
					num3 -= (float)num5 * num9;
				}
				if (this.mFlip == UISprite.Flip.Vertically || this.mFlip == UISprite.Flip.Both)
				{
					num2 += (float)num6 * num10;
					num4 -= (float)paddingBottom * num10;
				}
				else
				{
					num2 += (float)paddingBottom * num10;
					num4 -= (float)num6 * num10;
				}
			}
			Vector4 vector = (!(this.mAtlas != null)) ? Vector4.zero : (this.border * this.mAtlas.pixelSize);
			float num11 = vector.x + vector.z;
			float num12 = vector.y + vector.w;
			float num13 = Mathf.Lerp(num, num3 - num11, this.mDrawRegion.x);
			float num14 = Mathf.Lerp(num2, num4 - num12, this.mDrawRegion.y);
			float num15 = Mathf.Lerp(num + num11, num3, this.mDrawRegion.z);
			float num16 = Mathf.Lerp(num2 + num12, num4, this.mDrawRegion.w);
			return new Vector4(num13, num14, num15, num16);
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600068E RID: 1678 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
	protected virtual Vector4 drawingUVs
	{
		get
		{
			switch (this.mFlip)
			{
			case UISprite.Flip.Horizontally:
				return new Vector4(this.mOuterUV.xMax, this.mOuterUV.yMin, this.mOuterUV.xMin, this.mOuterUV.yMax);
			case UISprite.Flip.Vertically:
				return new Vector4(this.mOuterUV.xMin, this.mOuterUV.yMax, this.mOuterUV.xMax, this.mOuterUV.yMin);
			case UISprite.Flip.Both:
				return new Vector4(this.mOuterUV.xMax, this.mOuterUV.yMax, this.mOuterUV.xMin, this.mOuterUV.yMin);
			default:
				return new Vector4(this.mOuterUV.xMin, this.mOuterUV.yMin, this.mOuterUV.xMax, this.mOuterUV.yMax);
			}
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
	protected void SimpleFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Vector4 drawingDimensions = this.drawingDimensions;
		Vector4 drawingUVs = this.drawingUVs;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		uvs.Add(new Vector2(drawingUVs.x, drawingUVs.y));
		uvs.Add(new Vector2(drawingUVs.x, drawingUVs.w));
		uvs.Add(new Vector2(drawingUVs.z, drawingUVs.w));
		uvs.Add(new Vector2(drawingUVs.z, drawingUVs.y));
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002D320 File Offset: 0x0002B520
	protected void SlicedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (!this.mSprite.hasBorder)
		{
			this.SimpleFill(verts, uvs, cols);
			return;
		}
		Vector4 drawingDimensions = this.drawingDimensions;
		Vector4 vector = this.border * this.atlas.pixelSize;
		UISprite.mTempPos[0].x = drawingDimensions.x;
		UISprite.mTempPos[0].y = drawingDimensions.y;
		UISprite.mTempPos[3].x = drawingDimensions.z;
		UISprite.mTempPos[3].y = drawingDimensions.w;
		if (this.mFlip == UISprite.Flip.Horizontally || this.mFlip == UISprite.Flip.Both)
		{
			UISprite.mTempPos[1].x = UISprite.mTempPos[0].x + vector.z;
			UISprite.mTempPos[2].x = UISprite.mTempPos[3].x - vector.x;
			UISprite.mTempUVs[3].x = this.mOuterUV.xMin;
			UISprite.mTempUVs[2].x = this.mInnerUV.xMin;
			UISprite.mTempUVs[1].x = this.mInnerUV.xMax;
			UISprite.mTempUVs[0].x = this.mOuterUV.xMax;
		}
		else
		{
			UISprite.mTempPos[1].x = UISprite.mTempPos[0].x + vector.x;
			UISprite.mTempPos[2].x = UISprite.mTempPos[3].x - vector.z;
			UISprite.mTempUVs[0].x = this.mOuterUV.xMin;
			UISprite.mTempUVs[1].x = this.mInnerUV.xMin;
			UISprite.mTempUVs[2].x = this.mInnerUV.xMax;
			UISprite.mTempUVs[3].x = this.mOuterUV.xMax;
		}
		if (this.mFlip == UISprite.Flip.Vertically || this.mFlip == UISprite.Flip.Both)
		{
			UISprite.mTempPos[1].y = UISprite.mTempPos[0].y + vector.w;
			UISprite.mTempPos[2].y = UISprite.mTempPos[3].y - vector.y;
			UISprite.mTempUVs[3].y = this.mOuterUV.yMin;
			UISprite.mTempUVs[2].y = this.mInnerUV.yMin;
			UISprite.mTempUVs[1].y = this.mInnerUV.yMax;
			UISprite.mTempUVs[0].y = this.mOuterUV.yMax;
		}
		else
		{
			UISprite.mTempPos[1].y = UISprite.mTempPos[0].y + vector.y;
			UISprite.mTempPos[2].y = UISprite.mTempPos[3].y - vector.w;
			UISprite.mTempUVs[0].y = this.mOuterUV.yMin;
			UISprite.mTempUVs[1].y = this.mInnerUV.yMin;
			UISprite.mTempUVs[2].y = this.mInnerUV.yMax;
			UISprite.mTempUVs[3].y = this.mOuterUV.yMax;
		}
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		for (int i = 0; i < 3; i++)
		{
			int num = i + 1;
			for (int j = 0; j < 3; j++)
			{
				if (this.centerType != UISprite.AdvancedType.Invisible || i != 1 || j != 1)
				{
					int num2 = j + 1;
					verts.Add(new Vector3(UISprite.mTempPos[i].x, UISprite.mTempPos[j].y));
					verts.Add(new Vector3(UISprite.mTempPos[i].x, UISprite.mTempPos[num2].y));
					verts.Add(new Vector3(UISprite.mTempPos[num].x, UISprite.mTempPos[num2].y));
					verts.Add(new Vector3(UISprite.mTempPos[num].x, UISprite.mTempPos[j].y));
					uvs.Add(new Vector2(UISprite.mTempUVs[i].x, UISprite.mTempUVs[j].y));
					uvs.Add(new Vector2(UISprite.mTempUVs[i].x, UISprite.mTempUVs[num2].y));
					uvs.Add(new Vector2(UISprite.mTempUVs[num].x, UISprite.mTempUVs[num2].y));
					uvs.Add(new Vector2(UISprite.mTempUVs[num].x, UISprite.mTempUVs[j].y));
					cols.Add(item);
					cols.Add(item);
					cols.Add(item);
					cols.Add(item);
				}
			}
		}
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0002D900 File Offset: 0x0002BB00
	protected void TiledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.material.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Vector4 drawingDimensions = this.drawingDimensions;
		Vector4 vector;
		if (this.mFlip == UISprite.Flip.Horizontally || this.mFlip == UISprite.Flip.Both)
		{
			vector.x = this.mInnerUV.xMax;
			vector.z = this.mInnerUV.xMin;
		}
		else
		{
			vector.x = this.mInnerUV.xMin;
			vector.z = this.mInnerUV.xMax;
		}
		if (this.mFlip == UISprite.Flip.Vertically || this.mFlip == UISprite.Flip.Both)
		{
			vector.y = this.mInnerUV.yMax;
			vector.w = this.mInnerUV.yMin;
		}
		else
		{
			vector.y = this.mInnerUV.yMin;
			vector.w = this.mInnerUV.yMax;
		}
		Vector2 vector2;
		vector2..ctor(this.mInnerUV.width * (float)mainTexture.width, this.mInnerUV.height * (float)mainTexture.height);
		vector2 *= this.atlas.pixelSize;
		if (vector2.x < 2f || vector2.y < 2f)
		{
			return;
		}
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		float num = drawingDimensions.x;
		float num2 = drawingDimensions.y;
		float x = vector.x;
		float y = vector.y;
		while (num2 < drawingDimensions.w)
		{
			num = drawingDimensions.x;
			float num3 = num2 + vector2.y;
			float num4 = vector.w;
			if (num3 > drawingDimensions.w)
			{
				num4 = Mathf.Lerp(vector.y, vector.w, (drawingDimensions.w - num2) / vector2.y);
				num3 = drawingDimensions.w;
			}
			while (num < drawingDimensions.z)
			{
				float num5 = num + vector2.x;
				float num6 = vector.z;
				if (num5 > drawingDimensions.z)
				{
					num6 = Mathf.Lerp(vector.x, vector.z, (drawingDimensions.z - num) / vector2.x);
					num5 = drawingDimensions.z;
				}
				verts.Add(new Vector3(num, num2));
				verts.Add(new Vector3(num, num3));
				verts.Add(new Vector3(num5, num3));
				verts.Add(new Vector3(num5, num2));
				uvs.Add(new Vector2(x, y));
				uvs.Add(new Vector2(x, num4));
				uvs.Add(new Vector2(num6, num4));
				uvs.Add(new Vector2(num6, y));
				cols.Add(item);
				cols.Add(item);
				cols.Add(item);
				cols.Add(item);
				num += vector2.x;
			}
			num2 += vector2.y;
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0002DC3C File Offset: 0x0002BE3C
	protected void FilledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (this.mFillAmount < 0.001f)
		{
			return;
		}
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		Vector4 drawingDimensions = this.drawingDimensions;
		Vector4 drawingUVs = this.drawingUVs;
		if (this.mFillDirection == UISprite.FillDirection.Horizontal || this.mFillDirection == UISprite.FillDirection.Vertical)
		{
			if (this.mFillDirection == UISprite.FillDirection.Horizontal)
			{
				float num = (drawingUVs.z - drawingUVs.x) * this.mFillAmount;
				if (this.mInvert)
				{
					drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * this.mFillAmount;
					drawingUVs.x = drawingUVs.z - num;
				}
				else
				{
					drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * this.mFillAmount;
					drawingUVs.z = drawingUVs.x + num;
				}
			}
			else if (this.mFillDirection == UISprite.FillDirection.Vertical)
			{
				float num2 = (drawingUVs.w - drawingUVs.y) * this.mFillAmount;
				if (this.mInvert)
				{
					drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * this.mFillAmount;
					drawingUVs.y = drawingUVs.w - num2;
				}
				else
				{
					drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * this.mFillAmount;
					drawingUVs.w = drawingUVs.y + num2;
				}
			}
		}
		UISprite.mTempPos[0] = new Vector2(drawingDimensions.x, drawingDimensions.y);
		UISprite.mTempPos[1] = new Vector2(drawingDimensions.x, drawingDimensions.w);
		UISprite.mTempPos[2] = new Vector2(drawingDimensions.z, drawingDimensions.w);
		UISprite.mTempPos[3] = new Vector2(drawingDimensions.z, drawingDimensions.y);
		UISprite.mTempUVs[0] = new Vector2(drawingUVs.x, drawingUVs.y);
		UISprite.mTempUVs[1] = new Vector2(drawingUVs.x, drawingUVs.w);
		UISprite.mTempUVs[2] = new Vector2(drawingUVs.z, drawingUVs.w);
		UISprite.mTempUVs[3] = new Vector2(drawingUVs.z, drawingUVs.y);
		if (this.mFillAmount < 1f)
		{
			if (this.mFillDirection == UISprite.FillDirection.Radial90)
			{
				if (UISprite.RadialCut(UISprite.mTempPos, UISprite.mTempUVs, this.mFillAmount, this.mInvert, 0))
				{
					for (int i = 0; i < 4; i++)
					{
						verts.Add(UISprite.mTempPos[i]);
						uvs.Add(UISprite.mTempUVs[i]);
						cols.Add(item);
					}
				}
				return;
			}
			if (this.mFillDirection == UISprite.FillDirection.Radial180)
			{
				for (int j = 0; j < 2; j++)
				{
					float num3 = 0f;
					float num4 = 1f;
					float num5;
					float num6;
					if (j == 0)
					{
						num5 = 0f;
						num6 = 0.5f;
					}
					else
					{
						num5 = 0.5f;
						num6 = 1f;
					}
					UISprite.mTempPos[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num5);
					UISprite.mTempPos[1].x = UISprite.mTempPos[0].x;
					UISprite.mTempPos[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num6);
					UISprite.mTempPos[3].x = UISprite.mTempPos[2].x;
					UISprite.mTempPos[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num3);
					UISprite.mTempPos[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num4);
					UISprite.mTempPos[2].y = UISprite.mTempPos[1].y;
					UISprite.mTempPos[3].y = UISprite.mTempPos[0].y;
					UISprite.mTempUVs[0].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num5);
					UISprite.mTempUVs[1].x = UISprite.mTempUVs[0].x;
					UISprite.mTempUVs[2].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num6);
					UISprite.mTempUVs[3].x = UISprite.mTempUVs[2].x;
					UISprite.mTempUVs[0].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num3);
					UISprite.mTempUVs[1].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num4);
					UISprite.mTempUVs[2].y = UISprite.mTempUVs[1].y;
					UISprite.mTempUVs[3].y = UISprite.mTempUVs[0].y;
					float num7 = this.mInvert ? (this.mFillAmount * 2f - (float)(1 - j)) : (this.fillAmount * 2f - (float)j);
					if (UISprite.RadialCut(UISprite.mTempPos, UISprite.mTempUVs, Mathf.Clamp01(num7), !this.mInvert, NGUIMath.RepeatIndex(j + 3, 4)))
					{
						for (int k = 0; k < 4; k++)
						{
							verts.Add(UISprite.mTempPos[k]);
							uvs.Add(UISprite.mTempUVs[k]);
							cols.Add(item);
						}
					}
				}
				return;
			}
			if (this.mFillDirection == UISprite.FillDirection.Radial360)
			{
				for (int l = 0; l < 4; l++)
				{
					float num8;
					float num9;
					if (l < 2)
					{
						num8 = 0f;
						num9 = 0.5f;
					}
					else
					{
						num8 = 0.5f;
						num9 = 1f;
					}
					float num10;
					float num11;
					if (l == 0 || l == 3)
					{
						num10 = 0f;
						num11 = 0.5f;
					}
					else
					{
						num10 = 0.5f;
						num11 = 1f;
					}
					UISprite.mTempPos[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num8);
					UISprite.mTempPos[1].x = UISprite.mTempPos[0].x;
					UISprite.mTempPos[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num9);
					UISprite.mTempPos[3].x = UISprite.mTempPos[2].x;
					UISprite.mTempPos[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num10);
					UISprite.mTempPos[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num11);
					UISprite.mTempPos[2].y = UISprite.mTempPos[1].y;
					UISprite.mTempPos[3].y = UISprite.mTempPos[0].y;
					UISprite.mTempUVs[0].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num8);
					UISprite.mTempUVs[1].x = UISprite.mTempUVs[0].x;
					UISprite.mTempUVs[2].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num9);
					UISprite.mTempUVs[3].x = UISprite.mTempUVs[2].x;
					UISprite.mTempUVs[0].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num10);
					UISprite.mTempUVs[1].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num11);
					UISprite.mTempUVs[2].y = UISprite.mTempUVs[1].y;
					UISprite.mTempUVs[3].y = UISprite.mTempUVs[0].y;
					float num12 = (!this.mInvert) ? (this.mFillAmount * 4f - (float)(3 - NGUIMath.RepeatIndex(l + 2, 4))) : (this.mFillAmount * 4f - (float)NGUIMath.RepeatIndex(l + 2, 4));
					if (UISprite.RadialCut(UISprite.mTempPos, UISprite.mTempUVs, Mathf.Clamp01(num12), this.mInvert, NGUIMath.RepeatIndex(l + 2, 4)))
					{
						for (int m = 0; m < 4; m++)
						{
							verts.Add(UISprite.mTempPos[m]);
							uvs.Add(UISprite.mTempUVs[m]);
							cols.Add(item);
						}
					}
				}
				return;
			}
		}
		for (int n = 0; n < 4; n++)
		{
			verts.Add(UISprite.mTempPos[n]);
			uvs.Add(UISprite.mTempUVs[n]);
			cols.Add(item);
		}
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0002E688 File Offset: 0x0002C888
	private static bool RadialCut(Vector2[] xy, Vector2[] uv, float fill, bool invert, int corner)
	{
		if (fill < 0.001f)
		{
			return false;
		}
		if ((corner & 1) == 1)
		{
			invert = !invert;
		}
		if (!invert && fill > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(fill);
		if (invert)
		{
			num = 1f - num;
		}
		num *= 1.5707964f;
		float cos = Mathf.Cos(num);
		float sin = Mathf.Sin(num);
		UISprite.RadialCut(xy, cos, sin, invert, corner);
		UISprite.RadialCut(uv, cos, sin, invert, corner);
		return true;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0002E708 File Offset: 0x0002C908
	private static void RadialCut(Vector2[] xy, float cos, float sin, bool invert, int corner)
	{
		int num = NGUIMath.RepeatIndex(corner + 1, 4);
		int num2 = NGUIMath.RepeatIndex(corner + 2, 4);
		int num3 = NGUIMath.RepeatIndex(corner + 3, 4);
		if ((corner & 1) == 1)
		{
			if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num2].x = xy[num].x;
				}
			}
			else if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num2].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num3].y = xy[num2].y;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (!invert)
			{
				xy[num3].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
			else
			{
				xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
		}
		else
		{
			if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num2].y = xy[num].y;
				}
			}
			else if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num2].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num3].x = xy[num2].x;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (invert)
			{
				xy[num3].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
			else
			{
				xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
		}
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
	protected void AdvancedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (!this.mSprite.hasBorder)
		{
			this.SimpleFill(verts, uvs, cols);
			return;
		}
		Texture mainTexture = this.material.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Vector4 drawingDimensions = this.drawingDimensions;
		Vector4 vector = this.border * this.atlas.pixelSize;
		Vector2 vector2;
		vector2..ctor(this.mInnerUV.width * (float)mainTexture.width, this.mInnerUV.height * (float)mainTexture.height);
		vector2 *= this.atlas.pixelSize;
		if (vector2.x < 1f)
		{
			vector2.x = 1f;
		}
		if (vector2.y < 1f)
		{
			vector2.y = 1f;
		}
		UISprite.mTempPos[0].x = drawingDimensions.x;
		UISprite.mTempPos[0].y = drawingDimensions.y;
		UISprite.mTempPos[3].x = drawingDimensions.z;
		UISprite.mTempPos[3].y = drawingDimensions.w;
		if (this.mFlip == UISprite.Flip.Horizontally || this.mFlip == UISprite.Flip.Both)
		{
			UISprite.mTempPos[1].x = UISprite.mTempPos[0].x + vector.z;
			UISprite.mTempPos[2].x = UISprite.mTempPos[3].x - vector.x;
			UISprite.mTempUVs[3].x = this.mOuterUV.xMin;
			UISprite.mTempUVs[2].x = this.mInnerUV.xMin;
			UISprite.mTempUVs[1].x = this.mInnerUV.xMax;
			UISprite.mTempUVs[0].x = this.mOuterUV.xMax;
		}
		else
		{
			UISprite.mTempPos[1].x = UISprite.mTempPos[0].x + vector.x;
			UISprite.mTempPos[2].x = UISprite.mTempPos[3].x - vector.z;
			UISprite.mTempUVs[0].x = this.mOuterUV.xMin;
			UISprite.mTempUVs[1].x = this.mInnerUV.xMin;
			UISprite.mTempUVs[2].x = this.mInnerUV.xMax;
			UISprite.mTempUVs[3].x = this.mOuterUV.xMax;
		}
		if (this.mFlip == UISprite.Flip.Vertically || this.mFlip == UISprite.Flip.Both)
		{
			UISprite.mTempPos[1].y = UISprite.mTempPos[0].y + vector.w;
			UISprite.mTempPos[2].y = UISprite.mTempPos[3].y - vector.y;
			UISprite.mTempUVs[3].y = this.mOuterUV.yMin;
			UISprite.mTempUVs[2].y = this.mInnerUV.yMin;
			UISprite.mTempUVs[1].y = this.mInnerUV.yMax;
			UISprite.mTempUVs[0].y = this.mOuterUV.yMax;
		}
		else
		{
			UISprite.mTempPos[1].y = UISprite.mTempPos[0].y + vector.y;
			UISprite.mTempPos[2].y = UISprite.mTempPos[3].y - vector.w;
			UISprite.mTempUVs[0].y = this.mOuterUV.yMin;
			UISprite.mTempUVs[1].y = this.mInnerUV.yMin;
			UISprite.mTempUVs[2].y = this.mInnerUV.yMax;
			UISprite.mTempUVs[3].y = this.mOuterUV.yMax;
		}
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 color2 = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		for (int i = 0; i < 3; i++)
		{
			int num = i + 1;
			for (int j = 0; j < 3; j++)
			{
				if (this.centerType != UISprite.AdvancedType.Invisible || i != 1 || j != 1)
				{
					int num2 = j + 1;
					if (i == 1 && j == 1)
					{
						if (this.centerType == UISprite.AdvancedType.Tiled)
						{
							float x = UISprite.mTempPos[i].x;
							float x2 = UISprite.mTempPos[num].x;
							float y = UISprite.mTempPos[j].y;
							float y2 = UISprite.mTempPos[num2].y;
							float x3 = UISprite.mTempUVs[i].x;
							float y3 = UISprite.mTempUVs[j].y;
							for (float num3 = y; num3 < y2; num3 += vector2.y)
							{
								float num4 = x;
								float num5 = UISprite.mTempUVs[num2].y;
								float num6 = num3 + vector2.y;
								if (num6 > y2)
								{
									num5 = Mathf.Lerp(y3, num5, (y2 - num3) / vector2.y);
									num6 = y2;
								}
								while (num4 < x2)
								{
									float num7 = num4 + vector2.x;
									float num8 = UISprite.mTempUVs[num].x;
									if (num7 > x2)
									{
										num8 = Mathf.Lerp(x3, num8, (x2 - num4) / vector2.x);
										num7 = x2;
									}
									this.FillBuffers(num4, num7, num3, num6, x3, num8, y3, num5, color2, verts, uvs, cols);
									num4 += vector2.x;
								}
							}
						}
						else if (this.centerType == UISprite.AdvancedType.Sliced)
						{
							this.FillBuffers(UISprite.mTempPos[i].x, UISprite.mTempPos[num].x, UISprite.mTempPos[j].y, UISprite.mTempPos[num2].y, UISprite.mTempUVs[i].x, UISprite.mTempUVs[num].x, UISprite.mTempUVs[j].y, UISprite.mTempUVs[num2].y, color2, verts, uvs, cols);
						}
					}
					else if (i == 1)
					{
						if ((j == 0 && this.bottomType == UISprite.AdvancedType.Tiled) || (j == 2 && this.topType == UISprite.AdvancedType.Tiled))
						{
							float x4 = UISprite.mTempPos[i].x;
							float x5 = UISprite.mTempPos[num].x;
							float y4 = UISprite.mTempPos[j].y;
							float y5 = UISprite.mTempPos[num2].y;
							float x6 = UISprite.mTempUVs[i].x;
							float y6 = UISprite.mTempUVs[j].y;
							float y7 = UISprite.mTempUVs[num2].y;
							for (float num9 = x4; num9 < x5; num9 += vector2.x)
							{
								float num10 = num9 + vector2.x;
								float num11 = UISprite.mTempUVs[num].x;
								if (num10 > x5)
								{
									num11 = Mathf.Lerp(x6, num11, (x5 - num9) / vector2.x);
									num10 = x5;
								}
								this.FillBuffers(num9, num10, y4, y5, x6, num11, y6, y7, color2, verts, uvs, cols);
							}
						}
						else if ((j == 0 && this.bottomType == UISprite.AdvancedType.Sliced) || (j == 2 && this.topType == UISprite.AdvancedType.Sliced))
						{
							this.FillBuffers(UISprite.mTempPos[i].x, UISprite.mTempPos[num].x, UISprite.mTempPos[j].y, UISprite.mTempPos[num2].y, UISprite.mTempUVs[i].x, UISprite.mTempUVs[num].x, UISprite.mTempUVs[j].y, UISprite.mTempUVs[num2].y, color2, verts, uvs, cols);
						}
					}
					else if (j == 1)
					{
						if ((i == 0 && this.leftType == UISprite.AdvancedType.Tiled) || (i == 2 && this.rightType == UISprite.AdvancedType.Tiled))
						{
							float x7 = UISprite.mTempPos[i].x;
							float x8 = UISprite.mTempPos[num].x;
							float y8 = UISprite.mTempPos[j].y;
							float y9 = UISprite.mTempPos[num2].y;
							float x9 = UISprite.mTempUVs[i].x;
							float x10 = UISprite.mTempUVs[num].x;
							float y10 = UISprite.mTempUVs[j].y;
							for (float num12 = y8; num12 < y9; num12 += vector2.y)
							{
								float num13 = UISprite.mTempUVs[num2].y;
								float num14 = num12 + vector2.y;
								if (num14 > y9)
								{
									num13 = Mathf.Lerp(y10, num13, (y9 - num12) / vector2.y);
									num14 = y9;
								}
								this.FillBuffers(x7, x8, num12, num14, x9, x10, y10, num13, color2, verts, uvs, cols);
							}
						}
						else if ((i == 0 && this.leftType == UISprite.AdvancedType.Sliced) || (i == 2 && this.rightType == UISprite.AdvancedType.Sliced))
						{
							this.FillBuffers(UISprite.mTempPos[i].x, UISprite.mTempPos[num].x, UISprite.mTempPos[j].y, UISprite.mTempPos[num2].y, UISprite.mTempUVs[i].x, UISprite.mTempUVs[num].x, UISprite.mTempUVs[j].y, UISprite.mTempUVs[num2].y, color2, verts, uvs, cols);
						}
					}
					else
					{
						this.FillBuffers(UISprite.mTempPos[i].x, UISprite.mTempPos[num].x, UISprite.mTempPos[j].y, UISprite.mTempPos[num2].y, UISprite.mTempUVs[i].x, UISprite.mTempUVs[num].x, UISprite.mTempUVs[j].y, UISprite.mTempUVs[num2].y, color2, verts, uvs, cols);
					}
				}
			}
		}
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0002F54C File Offset: 0x0002D74C
	private void FillBuffers(float v0x, float v1x, float v0y, float v1y, float u0x, float u1x, float u0y, float u1y, Color col, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		verts.Add(new Vector3(v0x, v0y));
		verts.Add(new Vector3(v0x, v1y));
		verts.Add(new Vector3(v1x, v1y));
		verts.Add(new Vector3(v1x, v0y));
		uvs.Add(new Vector2(u0x, u0y));
		uvs.Add(new Vector2(u0x, u1y));
		uvs.Add(new Vector2(u1x, u1y));
		uvs.Add(new Vector2(u1x, u0y));
		cols.Add(col);
		cols.Add(col);
		cols.Add(col);
		cols.Add(col);
	}

	// Token: 0x0400058E RID: 1422
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x0400058F RID: 1423
	[SerializeField]
	[HideInInspector]
	private string mSpriteName;

	// Token: 0x04000590 RID: 1424
	[HideInInspector]
	[SerializeField]
	private UISprite.Type mType;

	// Token: 0x04000591 RID: 1425
	[HideInInspector]
	[SerializeField]
	private UISprite.FillDirection mFillDirection = UISprite.FillDirection.Radial360;

	// Token: 0x04000592 RID: 1426
	[Range(0f, 1f)]
	[SerializeField]
	[HideInInspector]
	private float mFillAmount = 1f;

	// Token: 0x04000593 RID: 1427
	[HideInInspector]
	[SerializeField]
	private bool mInvert;

	// Token: 0x04000594 RID: 1428
	[HideInInspector]
	[SerializeField]
	private UISprite.Flip mFlip;

	// Token: 0x04000595 RID: 1429
	[SerializeField]
	[HideInInspector]
	private bool mFillCenter = true;

	// Token: 0x04000596 RID: 1430
	[NonSerialized]
	protected UISpriteData mSprite;

	// Token: 0x04000597 RID: 1431
	protected Rect mInnerUV = default(Rect);

	// Token: 0x04000598 RID: 1432
	protected Rect mOuterUV = default(Rect);

	// Token: 0x04000599 RID: 1433
	private bool mSpriteSet;

	// Token: 0x0400059A RID: 1434
	public UISprite.AdvancedType centerType = UISprite.AdvancedType.Sliced;

	// Token: 0x0400059B RID: 1435
	public UISprite.AdvancedType leftType = UISprite.AdvancedType.Sliced;

	// Token: 0x0400059C RID: 1436
	public UISprite.AdvancedType rightType = UISprite.AdvancedType.Sliced;

	// Token: 0x0400059D RID: 1437
	public UISprite.AdvancedType bottomType = UISprite.AdvancedType.Sliced;

	// Token: 0x0400059E RID: 1438
	public UISprite.AdvancedType topType = UISprite.AdvancedType.Sliced;

	// Token: 0x0400059F RID: 1439
	private static Vector2[] mTempPos = new Vector2[4];

	// Token: 0x040005A0 RID: 1440
	private static Vector2[] mTempUVs = new Vector2[4];

	// Token: 0x020000CE RID: 206
	public enum Type
	{
		// Token: 0x040005A2 RID: 1442
		Simple,
		// Token: 0x040005A3 RID: 1443
		Sliced,
		// Token: 0x040005A4 RID: 1444
		Tiled,
		// Token: 0x040005A5 RID: 1445
		Filled,
		// Token: 0x040005A6 RID: 1446
		Advanced
	}

	// Token: 0x020000CF RID: 207
	public enum FillDirection
	{
		// Token: 0x040005A8 RID: 1448
		Horizontal,
		// Token: 0x040005A9 RID: 1449
		Vertical,
		// Token: 0x040005AA RID: 1450
		Radial90,
		// Token: 0x040005AB RID: 1451
		Radial180,
		// Token: 0x040005AC RID: 1452
		Radial360
	}

	// Token: 0x020000D0 RID: 208
	public enum AdvancedType
	{
		// Token: 0x040005AE RID: 1454
		Invisible,
		// Token: 0x040005AF RID: 1455
		Sliced,
		// Token: 0x040005B0 RID: 1456
		Tiled
	}

	// Token: 0x020000D1 RID: 209
	public enum Flip
	{
		// Token: 0x040005B2 RID: 1458
		Nothing,
		// Token: 0x040005B3 RID: 1459
		Horizontally,
		// Token: 0x040005B4 RID: 1460
		Vertically,
		// Token: 0x040005B5 RID: 1461
		Both
	}
}
