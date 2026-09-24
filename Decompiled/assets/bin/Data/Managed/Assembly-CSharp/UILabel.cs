using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000276F8 File Offset: 0x000258F8
	// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00027700 File Offset: 0x00025900
	private bool shouldBeProcessed
	{
		get
		{
			return this.mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
			}
			else
			{
				this.mShouldBeProcessed = false;
			}
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00027730 File Offset: 0x00025930
	public override bool isAnchoredHorizontally
	{
		get
		{
			return base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0002774C File Offset: 0x0002594C
	public override bool isAnchoredVertically
	{
		get
		{
			return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00027774 File Offset: 0x00025974
	// (set) Token: 0x060005B5 RID: 1461 RVA: 0x000277D4 File Offset: 0x000259D4
	public override Material material
	{
		get
		{
			if (this.mMaterial != null)
			{
				return this.mMaterial;
			}
			if (this.mFont != null)
			{
				return this.mFont.material;
			}
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont.material;
			}
			return null;
		}
		set
		{
			if (this.mMaterial != value)
			{
				this.MarkAsChanged();
				this.mMaterial = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00027808 File Offset: 0x00025A08
	// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00027810 File Offset: 0x00025A10
	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont font
	{
		get
		{
			return this.bitmapFont;
		}
		set
		{
			this.bitmapFont = value;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0002781C File Offset: 0x00025A1C
	// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00027824 File Offset: 0x00025A24
	public UIFont bitmapFont
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				base.RemoveFromPanel();
				this.mFont = value;
				this.mTrueTypeFont = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060005BA RID: 1466 RVA: 0x00027854 File Offset: 0x00025A54
	// (set) Token: 0x060005BB RID: 1467 RVA: 0x0002789C File Offset: 0x00025A9C
	public Font trueTypeFont
	{
		get
		{
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont;
			}
			return (!(this.mFont != null)) ? null : this.mFont.dynamicFont;
		}
		set
		{
			if (this.mTrueTypeFont != value)
			{
				this.SetActiveFont(null);
				base.RemoveFromPanel();
				this.mTrueTypeFont = value;
				this.shouldBeProcessed = true;
				this.mFont = null;
				this.SetActiveFont(value);
				this.ProcessAndRequest();
				if (this.mActiveTTF != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060005BC RID: 1468 RVA: 0x00027900 File Offset: 0x00025B00
	// (set) Token: 0x060005BD RID: 1469 RVA: 0x00027930 File Offset: 0x00025B30
	public Object ambigiousFont
	{
		get
		{
			return (!(this.mFont != null)) ? this.mTrueTypeFont : this.mFont;
		}
		set
		{
			UIFont uifont = value as UIFont;
			if (uifont != null)
			{
				this.bitmapFont = uifont;
			}
			else
			{
				this.trueTypeFont = (value as Font);
			}
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060005BE RID: 1470 RVA: 0x00027968 File Offset: 0x00025B68
	// (set) Token: 0x060005BF RID: 1471 RVA: 0x00027970 File Offset: 0x00025B70
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (this.mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = string.Empty;
					this.shouldBeProcessed = true;
					this.ProcessAndRequest();
				}
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
			if (this.autoResizeBoxCollider)
			{
				base.ResizeCollider();
			}
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00027A00 File Offset: 0x00025C00
	public int defaultFontSize
	{
		get
		{
			return (!(this.trueTypeFont != null)) ? ((!(this.mFont != null)) ? 16 : this.mFont.defaultSize) : this.mFontSize;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00027A4C File Offset: 0x00025C4C
	// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00027A54 File Offset: 0x00025C54
	public int fontSize
	{
		get
		{
			return this.mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (this.mFontSize != value)
			{
				this.mFontSize = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00027A90 File Offset: 0x00025C90
	// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00027A98 File Offset: 0x00025C98
	public FontStyle fontStyle
	{
		get
		{
			return this.mFontStyle;
		}
		set
		{
			if (this.mFontStyle != value)
			{
				this.mFontStyle = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00027AC8 File Offset: 0x00025CC8
	// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00027AD0 File Offset: 0x00025CD0
	public NGUIText.Alignment alignment
	{
		get
		{
			return this.mAlignment;
		}
		set
		{
			if (this.mAlignment != value)
			{
				this.mAlignment = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00027B00 File Offset: 0x00025D00
	// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00027B08 File Offset: 0x00025D08
	public bool applyGradient
	{
		get
		{
			return this.mApplyGradient;
		}
		set
		{
			if (this.mApplyGradient != value)
			{
				this.mApplyGradient = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00027B24 File Offset: 0x00025D24
	// (set) Token: 0x060005CA RID: 1482 RVA: 0x00027B2C File Offset: 0x00025D2C
	public Color gradientTop
	{
		get
		{
			return this.mGradientTop;
		}
		set
		{
			if (this.mGradientTop != value)
			{
				this.mGradientTop = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060005CB RID: 1483 RVA: 0x00027B58 File Offset: 0x00025D58
	// (set) Token: 0x060005CC RID: 1484 RVA: 0x00027B60 File Offset: 0x00025D60
	public Color gradientBottom
	{
		get
		{
			return this.mGradientBottom;
		}
		set
		{
			if (this.mGradientBottom != value)
			{
				this.mGradientBottom = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060005CD RID: 1485 RVA: 0x00027B8C File Offset: 0x00025D8C
	// (set) Token: 0x060005CE RID: 1486 RVA: 0x00027B94 File Offset: 0x00025D94
	public int spacingX
	{
		get
		{
			return this.mSpacingX;
		}
		set
		{
			if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x060005CF RID: 1487 RVA: 0x00027BB0 File Offset: 0x00025DB0
	// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00027BB8 File Offset: 0x00025DB8
	public int spacingY
	{
		get
		{
			return this.mSpacingY;
		}
		set
		{
			if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00027BD4 File Offset: 0x00025DD4
	private bool keepCrisp
	{
		get
		{
			return this.trueTypeFont != null && this.keepCrispWhenShrunk != UILabel.Crispness.Never && this.keepCrispWhenShrunk == UILabel.Crispness.Always;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00027C00 File Offset: 0x00025E00
	// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00027C08 File Offset: 0x00025E08
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00027C24 File Offset: 0x00025E24
	// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00027C2C File Offset: 0x00025E2C
	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00027C48 File Offset: 0x00025E48
	// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00027C50 File Offset: 0x00025E50
	public UILabel.Overflow overflowMethod
	{
		get
		{
			return this.mOverflow;
		}
		set
		{
			if (this.mOverflow != value)
			{
				this.mOverflow = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00027C6C File Offset: 0x00025E6C
	// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00027C74 File Offset: 0x00025E74
	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x060005DA RID: 1498 RVA: 0x00027C80 File Offset: 0x00025E80
	// (set) Token: 0x060005DB RID: 1499 RVA: 0x00027C88 File Offset: 0x00025E88
	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x060005DC RID: 1500 RVA: 0x00027C94 File Offset: 0x00025E94
	// (set) Token: 0x060005DD RID: 1501 RVA: 0x00027CA4 File Offset: 0x00025EA4
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = ((!value) ? 1 : 0);
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x060005DE RID: 1502 RVA: 0x00027CE0 File Offset: 0x00025EE0
	public override Vector3[] localCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.localCorners;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x060005DF RID: 1503 RVA: 0x00027CFC File Offset: 0x00025EFC
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.worldCorners;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00027D18 File Offset: 0x00025F18
	public override Vector4 drawingDimensions
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.drawingDimensions;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00027D34 File Offset: 0x00025F34
	// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00027D3C File Offset: 0x00025F3C
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.shouldBeProcessed = true;
				if (this.overflowMethod == UILabel.Overflow.ShrinkContent)
				{
					this.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00027D70 File Offset: 0x00025F70
	// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00027D78 File Offset: 0x00025F78
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00027D94 File Offset: 0x00025F94
	// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00027D9C File Offset: 0x00025F9C
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.shouldBeProcessed = true;
				}
			}
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00027DD4 File Offset: 0x00025FD4
	// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00027DDC File Offset: 0x00025FDC
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00027E00 File Offset: 0x00026000
	// (set) Token: 0x060005EA RID: 1514 RVA: 0x00027E0C File Offset: 0x0002600C
	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return this.mOverflow == UILabel.Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				this.overflowMethod = UILabel.Overflow.ShrinkContent;
			}
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x060005EB RID: 1515 RVA: 0x00027E1C File Offset: 0x0002601C
	public string processedText
	{
		get
		{
			if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
			{
				this.mLastWidth = this.mWidth;
				this.mLastHeight = this.mHeight;
				this.mShouldBeProcessed = true;
			}
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x00027E84 File Offset: 0x00026084
	public Vector2 printedSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return this.mCalculatedSize;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x00027EA0 File Offset: 0x000260A0
	public override Vector2 localSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.localSize;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x060005EE RID: 1518 RVA: 0x00027EBC File Offset: 0x000260BC
	private bool isValid
	{
		get
		{
			return this.mFont != null || this.mTrueTypeFont != null;
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00027EEC File Offset: 0x000260EC
	protected override void OnInit()
	{
		base.OnInit();
		UILabel.mList.Add(this);
		this.SetActiveFont(this.trueTypeFont);
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00027F18 File Offset: 0x00026118
	protected override void OnDisable()
	{
		this.SetActiveFont(null);
		UILabel.mList.Remove(this);
		base.OnDisable();
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00027F34 File Offset: 0x00026134
	protected void SetActiveFont(Font fnt)
	{
		if (this.mActiveTTF != fnt)
		{
			if (this.mActiveTTF != null)
			{
				int num;
				if (UILabel.mFontUsage.TryGetValue(this.mActiveTTF, ref num))
				{
					num = Mathf.Max(0, --num);
					if (num == 0)
					{
						this.mActiveTTF.textureRebuildCallback = null;
						UILabel.mFontUsage.Remove(this.mActiveTTF);
					}
					else
					{
						UILabel.mFontUsage[this.mActiveTTF] = num;
					}
				}
				else
				{
					this.mActiveTTF.textureRebuildCallback = null;
				}
			}
			this.mActiveTTF = fnt;
			if (this.mActiveTTF != null)
			{
				int num2 = 0;
				if (!UILabel.mFontUsage.TryGetValue(this.mActiveTTF, ref num2))
				{
					this.mActiveTTF.textureRebuildCallback = new Font.FontTextureRebuildCallback(UILabel.OnFontTextureChanged);
				}
				num2 = (UILabel.mFontUsage[this.mActiveTTF] = num2 + 1);
			}
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00028030 File Offset: 0x00026230
	private static void OnFontTextureChanged()
	{
		for (int i = 0; i < UILabel.mList.size; i++)
		{
			UILabel uilabel = UILabel.mList[i];
			if (uilabel != null)
			{
				Font trueTypeFont = uilabel.trueTypeFont;
				if (trueTypeFont != null)
				{
					trueTypeFont.RequestCharactersInTexture(uilabel.mText, uilabel.mPrintedSize, uilabel.mFontStyle);
					uilabel.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x000280A4 File Offset: 0x000262A4
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.shouldBeProcessed)
		{
			this.ProcessText();
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x000280C0 File Offset: 0x000262C0
	protected override void UpgradeFrom265()
	{
		this.ProcessText(true, true);
		if (this.mShrinkToFit)
		{
			this.overflowMethod = UILabel.Overflow.ShrinkContent;
			this.mMaxLineCount = 0;
		}
		if (this.mMaxLineWidth != 0)
		{
			base.width = this.mMaxLineWidth;
			this.overflowMethod = ((this.mMaxLineCount <= 0) ? UILabel.Overflow.ShrinkContent : UILabel.Overflow.ResizeHeight);
		}
		else
		{
			this.overflowMethod = UILabel.Overflow.ResizeFreely;
		}
		if (this.mMaxLineHeight != 0)
		{
			base.height = this.mMaxLineHeight;
		}
		if (this.mFont != null)
		{
			int defaultSize = this.mFont.defaultSize;
			if (base.height < defaultSize)
			{
				base.height = defaultSize;
			}
		}
		this.mMaxLineWidth = 0;
		this.mMaxLineHeight = 0;
		this.mShrinkToFit = false;
		if (base.GetComponent<BoxCollider>() != null)
		{
			NGUITools.AddWidgetCollider(base.gameObject, true);
		}
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x000281A4 File Offset: 0x000263A4
	protected override void OnAnchor()
	{
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				this.mOverflow = UILabel.Overflow.ShrinkContent;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeHeight && this.topAnchor.target != null && this.bottomAnchor.target != null)
		{
			this.mOverflow = UILabel.Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0002821C File Offset: 0x0002641C
	private void ProcessAndRequest()
	{
		if (this.ambigiousFont != null)
		{
			this.ProcessText();
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00028238 File Offset: 0x00026438
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		this.mPremultiply = (this.material != null && this.material.shader != null && this.material.shader.name.Contains("Premultiplied"));
		this.ProcessAndRequest();
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x000282E0 File Offset: 0x000264E0
	public override void MarkAsChanged()
	{
		this.shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x000282F0 File Offset: 0x000264F0
	private void ProcessText()
	{
		this.ProcessText(false, true);
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x000282FC File Offset: 0x000264FC
	private void ProcessText(bool legacyMode, bool full)
	{
		if (!this.isValid)
		{
			return;
		}
		this.mChanged = true;
		this.shouldBeProcessed = false;
		NGUIText.rectWidth = ((!legacyMode) ? base.width : ((this.mMaxLineWidth == 0) ? 1000000 : this.mMaxLineWidth));
		NGUIText.rectHeight = ((!legacyMode) ? base.height : ((this.mMaxLineHeight == 0) ? 1000000 : this.mMaxLineHeight));
		this.mPrintedSize = Mathf.Abs((!legacyMode) ? this.defaultFontSize : Mathf.RoundToInt(base.cachedTransform.localScale.x));
		this.mScale = 1f;
		if (NGUIText.rectWidth < 1 || NGUIText.rectHeight < 0)
		{
			this.mProcessedText = string.Empty;
			return;
		}
		bool flag = this.trueTypeFont != null;
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				this.mDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			this.mDensity = 1f;
		}
		if (full)
		{
			this.UpdateNGUIText();
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			NGUIText.rectWidth = 1000000;
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight)
		{
			NGUIText.rectHeight = 1000000;
		}
		if (this.mPrintedSize > 0)
		{
			bool keepCrisp = this.keepCrisp;
			for (int i = this.mPrintedSize; i > 0; i--)
			{
				if (keepCrisp)
				{
					this.mPrintedSize = i;
					NGUIText.fontSize = this.mPrintedSize;
				}
				else
				{
					this.mScale = (float)i / (float)this.mPrintedSize;
					NGUIText.fontScale = ((!flag) ? ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale) : this.mScale);
				}
				NGUIText.Update(false);
				bool flag2 = NGUIText.WrapText(this.mText, out this.mProcessedText, true);
				if (this.mOverflow != UILabel.Overflow.ShrinkContent || flag2)
				{
					if (this.mOverflow == UILabel.Overflow.ResizeFreely)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						this.mWidth = Mathf.Max(this.minWidth, Mathf.RoundToInt(this.mCalculatedSize.x));
						this.mHeight = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if ((this.mWidth & 1) == 1)
						{
							this.mWidth++;
						}
						if ((this.mHeight & 1) == 1)
						{
							this.mHeight++;
						}
					}
					else if (this.mOverflow == UILabel.Overflow.ResizeHeight)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						this.mHeight = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if ((this.mHeight & 1) == 1)
						{
							this.mHeight++;
						}
					}
					else
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
					}
					if (legacyMode)
					{
						base.width = Mathf.RoundToInt(this.mCalculatedSize.x);
						base.height = Mathf.RoundToInt(this.mCalculatedSize.y);
						base.cachedTransform.localScale = Vector3.one;
					}
					break;
				}
				if (--i <= 1)
				{
					break;
				}
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			this.mProcessedText = string.Empty;
			this.mScale = 1f;
		}
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x000286D8 File Offset: 0x000268D8
	public override void MakePixelPerfect()
	{
		if (this.ambigiousFont != null)
		{
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = Vector3.one;
			if (this.mOverflow == UILabel.Overflow.ResizeFreely)
			{
				this.AssumeNaturalSize();
			}
			else
			{
				int width = base.width;
				int height = base.height;
				UILabel.Overflow overflow = this.mOverflow;
				if (overflow != UILabel.Overflow.ResizeHeight)
				{
					this.mWidth = 100000;
				}
				this.mHeight = 100000;
				this.mOverflow = UILabel.Overflow.ShrinkContent;
				this.ProcessText(false, true);
				this.mOverflow = overflow;
				int num = Mathf.RoundToInt(this.mCalculatedSize.x);
				int num2 = Mathf.RoundToInt(this.mCalculatedSize.y);
				num = Mathf.Max(num, base.minWidth);
				num2 = Mathf.Max(num2, base.minHeight);
				this.mWidth = Mathf.Max(width, num);
				this.mHeight = Mathf.Max(height, num2);
				this.MarkAsChanged();
			}
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00028828 File Offset: 0x00026A28
	public void AssumeNaturalSize()
	{
		if (this.ambigiousFont != null)
		{
			this.mWidth = 100000;
			this.mHeight = 100000;
			this.ProcessText(false, true);
			this.mWidth = Mathf.RoundToInt(this.mCalculatedSize.x);
			this.mHeight = Mathf.RoundToInt(this.mCalculatedSize.y);
			if ((this.mWidth & 1) == 1)
			{
				this.mWidth++;
			}
			if ((this.mHeight & 1) == 1)
			{
				this.mHeight++;
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x000288D0 File Offset: 0x00026AD0
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		return this.GetCharacterIndexAtPosition(worldPos);
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x000288DC File Offset: 0x00026ADC
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		return this.GetCharacterIndexAtPosition(localPos);
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x000288E8 File Offset: 0x00026AE8
	public int GetCharacterIndexAtPosition(Vector3 worldPos)
	{
		Vector2 localPos = base.cachedTransform.InverseTransformPoint(worldPos);
		return this.GetCharacterIndexAtPosition(localPos);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00028910 File Offset: 0x00026B10
	public int GetCharacterIndexAtPosition(Vector2 localPos)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			this.UpdateNGUIText();
			NGUIText.PrintCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.size > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int num = NGUIText.GetClosestCharacter(UILabel.mTempVerts, localPos);
				num = UILabel.mTempIndices[num];
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
				return num;
			}
		}
		return 0;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x000289A0 File Offset: 0x00026BA0
	public string GetWordAtPosition(Vector3 worldPos)
	{
		return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos));
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x000289B0 File Offset: 0x00026BB0
	public string GetWordAtPosition(Vector2 localPos)
	{
		return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos));
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x000289C0 File Offset: 0x00026BC0
	public string GetWordAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < this.mText.Length)
		{
			int num = this.mText.LastIndexOf(' ', characterIndex) + 1;
			int num2 = this.mText.IndexOf(' ', characterIndex);
			if (num2 == -1)
			{
				num2 = this.mText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					string text = this.mText.Substring(num, num3);
					return NGUIText.StripSymbols(text);
				}
			}
		}
		return null;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00028A40 File Offset: 0x00026C40
	public string GetUrlAtPosition(Vector3 worldPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos));
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00028A50 File Offset: 0x00026C50
	public string GetUrlAtPosition(Vector2 localPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos));
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x00028A60 File Offset: 0x00026C60
	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < this.mText.Length)
		{
			int num = this.mText.LastIndexOf("[url=", characterIndex);
			if (num != -1)
			{
				num += 5;
				int num2 = this.mText.IndexOf("]", num);
				if (num2 != -1)
				{
					int num3 = this.mText.IndexOf("[/url]", num2);
					if (num3 == -1 || num3 >= characterIndex)
					{
						return this.mText.Substring(num, num2 - num);
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00028AEC File Offset: 0x00026CEC
	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			int defaultFontSize = this.defaultFontSize;
			this.UpdateNGUIText();
			NGUIText.PrintCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.size > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int i = 0;
				while (i < UILabel.mTempIndices.size)
				{
					if (UILabel.mTempIndices[i] == currentIndex)
					{
						Vector2 pos = UILabel.mTempVerts[i];
						if (key == 273)
						{
							pos.y += (float)(defaultFontSize + this.spacingY);
						}
						else if (key == 274)
						{
							pos.y -= (float)(defaultFontSize + this.spacingY);
						}
						else if (key == 278)
						{
							pos.x -= 1000f;
						}
						else if (key == 279)
						{
							pos.x += 1000f;
						}
						int num = NGUIText.GetClosestCharacter(UILabel.mTempVerts, pos);
						num = UILabel.mTempIndices[num];
						if (num == currentIndex)
						{
							break;
						}
						UILabel.mTempVerts.Clear();
						UILabel.mTempIndices.Clear();
						return num;
					}
					else
					{
						i++;
					}
				}
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
			}
			if (key == 273 || key == 278)
			{
				return 0;
			}
			if (key == 274 || key == 279)
			{
				return processedText.Length;
			}
		}
		return currentIndex;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00028CA8 File Offset: 0x00026EA8
	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		if (caret != null)
		{
			caret.Clear();
		}
		if (highlight != null)
		{
			highlight.Clear();
		}
		if (!this.isValid)
		{
			return;
		}
		string processedText = this.processedText;
		this.UpdateNGUIText();
		int size = caret.verts.size;
		Vector2 item;
		item..ctor(0.5f, 0.5f);
		float finalAlpha = this.finalAlpha;
		if (highlight != null && start != end)
		{
			int size2 = highlight.verts.size;
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
			if (highlight.verts.size > size2)
			{
				this.ApplyOffset(highlight.verts, size2);
				Color32 item2 = new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha);
				for (int i = size2; i < highlight.verts.size; i++)
				{
					highlight.uvs.Add(item);
					highlight.cols.Add(item2);
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, null);
		}
		this.ApplyOffset(caret.verts, size);
		Color32 item3 = new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha);
		for (int j = size; j < caret.verts.size; j++)
		{
			caret.uvs.Add(item);
			caret.cols.Add(item3);
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00028E4C File Offset: 0x0002704C
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (!this.isValid)
		{
			return;
		}
		int start = verts.size;
		Color color = base.color;
		color.a = this.finalAlpha;
		if (this.mFont != null && this.mFont.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		string processedText = this.processedText;
		int size = verts.size;
		this.UpdateNGUIText();
		NGUIText.tint = color;
		NGUIText.Print(processedText, verts, uvs, cols);
		Vector2 vector = this.ApplyOffset(verts, size);
		if (this.mFont != null && this.mFont.packedFontShader)
		{
			return;
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			int size2 = verts.size;
			vector.x = this.mEffectDistance.x;
			vector.y = this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, start, size2, vector.x, -vector.y);
			if (this.effectStyle == UILabel.Effect.Outline)
			{
				start = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size2, -vector.x, vector.y);
				start = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size2, vector.x, vector.y);
				start = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size2, -vector.x, -vector.y);
			}
		}
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00028FC8 File Offset: 0x000271C8
	protected Vector2 ApplyOffset(BetterList<Vector3> verts, int start)
	{
		Vector2 pivotOffset = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-(float)this.mWidth), pivotOffset.x);
		float num2 = Mathf.Lerp((float)this.mHeight, 0f, pivotOffset.y) + Mathf.Lerp(this.mCalculatedSize.y - (float)this.mHeight, 0f, pivotOffset.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		for (int i = start; i < verts.size; i++)
		{
			Vector3[] buffer = verts.buffer;
			int num3 = i;
			buffer[num3].x = buffer[num3].x + num;
			Vector3[] buffer2 = verts.buffer;
			int num4 = i;
			buffer2[num4].y = buffer2[num4].y + num2;
		}
		return new Vector2(num, num2);
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00029094 File Offset: 0x00027294
	private void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= this.finalAlpha;
		Color32 color2 = (!(this.bitmapFont != null) || !this.bitmapFont.premultipliedAlphaShader) ? color : NGUITools.ApplyPMA(color);
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 vector = verts.buffer[i];
			vector.x += x;
			vector.y += y;
			verts.buffer[i] = vector;
			cols.buffer[i] = color2;
		}
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x000291A0 File Offset: 0x000273A0
	public int CalculateOffsetToFit(string text)
	{
		this.UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		return NGUIText.CalculateOffsetToFit(text);
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x000291BC File Offset: 0x000273BC
	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			this.text = UIProgressBar.current.value.ToString("F");
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x000291F8 File Offset: 0x000273F8
	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00029240 File Offset: 0x00027440
	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			this.text = ((!UIPopupList.current.isLocalized) ? UIPopupList.current.value : Localization.Get(UIPopupList.current.value));
		}
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00029290 File Offset: 0x00027490
	public bool Wrap(string text, out string final)
	{
		return this.Wrap(text, out final, 1000000);
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x000292A0 File Offset: 0x000274A0
	public bool Wrap(string text, out string final, int height)
	{
		this.UpdateNGUIText();
		return NGUIText.WrapText(text, out final);
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x000292B0 File Offset: 0x000274B0
	public void UpdateNGUIText()
	{
		Font trueTypeFont = this.trueTypeFont;
		bool flag = trueTypeFont != null;
		if (this.mPrintedSize == 0)
		{
			this.mPrintedSize = this.defaultFontSize;
		}
		NGUIText.fontSize = this.mPrintedSize;
		NGUIText.fontStyle = this.mFontStyle;
		NGUIText.rectWidth = this.mWidth;
		NGUIText.rectHeight = this.mHeight;
		NGUIText.gradient = (this.mApplyGradient && (this.mFont == null || !this.mFont.packedFontShader));
		NGUIText.gradientTop = this.mGradientTop;
		NGUIText.gradientBottom = this.mGradientBottom;
		NGUIText.encoding = this.mEncoding;
		NGUIText.premultiply = this.mPremultiply;
		NGUIText.symbolStyle = this.mSymbols;
		NGUIText.maxLines = this.mMaxLineCount;
		NGUIText.spacingX = (float)this.mSpacingX;
		NGUIText.spacingY = (float)this.mSpacingY;
		NGUIText.fontScale = ((!flag) ? ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale) : this.mScale);
		if (this.mFont != null)
		{
			NGUIText.bitmapFont = this.mFont;
			for (;;)
			{
				UIFont replacement = NGUIText.bitmapFont.replacement;
				if (replacement == null)
				{
					break;
				}
				NGUIText.bitmapFont = replacement;
			}
			if (NGUIText.bitmapFont.isDynamic)
			{
				NGUIText.dynamicFont = NGUIText.bitmapFont.dynamicFont;
				NGUIText.bitmapFont = null;
			}
			else
			{
				NGUIText.dynamicFont = null;
			}
		}
		else
		{
			NGUIText.dynamicFont = trueTypeFont;
			NGUIText.bitmapFont = null;
		}
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				NGUIText.pixelDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (this.mDensity != NGUIText.pixelDensity)
		{
			this.ProcessText(false, false);
			NGUIText.rectWidth = this.mWidth;
			NGUIText.rectHeight = this.mHeight;
		}
		if (this.alignment == NGUIText.Alignment.Automatic)
		{
			UIWidget.Pivot pivot = base.pivot;
			if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
			{
				NGUIText.alignment = NGUIText.Alignment.Left;
			}
			else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
			{
				NGUIText.alignment = NGUIText.Alignment.Right;
			}
			else
			{
				NGUIText.alignment = NGUIText.Alignment.Center;
			}
		}
		else
		{
			NGUIText.alignment = this.alignment;
		}
		NGUIText.Update();
	}

	// Token: 0x0400051E RID: 1310
	public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;

	// Token: 0x0400051F RID: 1311
	[SerializeField]
	[HideInInspector]
	private Font mTrueTypeFont;

	// Token: 0x04000520 RID: 1312
	[SerializeField]
	[HideInInspector]
	private UIFont mFont;

	// Token: 0x04000521 RID: 1313
	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = string.Empty;

	// Token: 0x04000522 RID: 1314
	[SerializeField]
	[HideInInspector]
	private int mFontSize = 16;

	// Token: 0x04000523 RID: 1315
	[SerializeField]
	[HideInInspector]
	private FontStyle mFontStyle;

	// Token: 0x04000524 RID: 1316
	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	// Token: 0x04000525 RID: 1317
	[SerializeField]
	[HideInInspector]
	private bool mEncoding = true;

	// Token: 0x04000526 RID: 1318
	[SerializeField]
	[HideInInspector]
	private int mMaxLineCount;

	// Token: 0x04000527 RID: 1319
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x04000528 RID: 1320
	[SerializeField]
	[HideInInspector]
	private Color mEffectColor = Color.black;

	// Token: 0x04000529 RID: 1321
	[SerializeField]
	[HideInInspector]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	// Token: 0x0400052A RID: 1322
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x0400052B RID: 1323
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x0400052C RID: 1324
	[HideInInspector]
	[SerializeField]
	private Material mMaterial;

	// Token: 0x0400052D RID: 1325
	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	// Token: 0x0400052E RID: 1326
	[SerializeField]
	[HideInInspector]
	private Color mGradientTop = Color.white;

	// Token: 0x0400052F RID: 1327
	[SerializeField]
	[HideInInspector]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x04000530 RID: 1328
	[SerializeField]
	[HideInInspector]
	private int mSpacingX;

	// Token: 0x04000531 RID: 1329
	[SerializeField]
	[HideInInspector]
	private int mSpacingY;

	// Token: 0x04000532 RID: 1330
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x04000533 RID: 1331
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x04000534 RID: 1332
	[SerializeField]
	[HideInInspector]
	private int mMaxLineHeight;

	// Token: 0x04000535 RID: 1333
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x04000536 RID: 1334
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x04000537 RID: 1335
	[NonSerialized]
	private Font mActiveTTF;

	// Token: 0x04000538 RID: 1336
	private float mDensity = 1f;

	// Token: 0x04000539 RID: 1337
	private bool mShouldBeProcessed = true;

	// Token: 0x0400053A RID: 1338
	private string mProcessedText;

	// Token: 0x0400053B RID: 1339
	private bool mPremultiply;

	// Token: 0x0400053C RID: 1340
	private Vector2 mCalculatedSize = Vector2.zero;

	// Token: 0x0400053D RID: 1341
	private float mScale = 1f;

	// Token: 0x0400053E RID: 1342
	private int mPrintedSize;

	// Token: 0x0400053F RID: 1343
	private int mLastWidth;

	// Token: 0x04000540 RID: 1344
	private int mLastHeight;

	// Token: 0x04000541 RID: 1345
	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	// Token: 0x04000542 RID: 1346
	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	// Token: 0x04000543 RID: 1347
	private static BetterList<Vector3> mTempVerts = new BetterList<Vector3>();

	// Token: 0x04000544 RID: 1348
	private static BetterList<int> mTempIndices = new BetterList<int>();

	// Token: 0x020000C4 RID: 196
	public enum Effect
	{
		// Token: 0x04000546 RID: 1350
		None,
		// Token: 0x04000547 RID: 1351
		Shadow,
		// Token: 0x04000548 RID: 1352
		Outline
	}

	// Token: 0x020000C5 RID: 197
	public enum Overflow
	{
		// Token: 0x0400054A RID: 1354
		ShrinkContent,
		// Token: 0x0400054B RID: 1355
		ClampContent,
		// Token: 0x0400054C RID: 1356
		ResizeFreely,
		// Token: 0x0400054D RID: 1357
		ResizeHeight
	}

	// Token: 0x020000C6 RID: 198
	public enum Crispness
	{
		// Token: 0x0400054F RID: 1359
		Never,
		// Token: 0x04000550 RID: 1360
		OnDesktop,
		// Token: 0x04000551 RID: 1361
		Always
	}
}
