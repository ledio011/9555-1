using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200008E RID: 142
	[AddComponentMenu("UI/Text", 11)]
	public class Text : MaskableGraphic, ILayoutElement
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x00013434 File Offset: 0x00011634
		protected Text()
		{
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00013458 File Offset: 0x00011658
		public TextGenerator cachedTextGenerator
		{
			get
			{
				TextGenerator result;
				if ((result = this.m_TextCache) == null)
				{
					result = (this.m_TextCache = ((this.m_Text.Length == 0) ? new TextGenerator() : new TextGenerator(this.m_Text.Length)));
				}
				return result;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000134A8 File Offset: 0x000116A8
		public TextGenerator cachedTextGeneratorForLayout
		{
			get
			{
				TextGenerator result;
				if ((result = this.m_TextCacheForLayout) == null)
				{
					result = (this.m_TextCacheForLayout = new TextGenerator());
				}
				return result;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000134D0 File Offset: 0x000116D0
		public override Material defaultMaterial
		{
			get
			{
				if (Text.s_DefaultText == null)
				{
					Text.s_DefaultText = Canvas.GetDefaultCanvasTextMaterial();
				}
				return Text.s_DefaultText;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000134F4 File Offset: 0x000116F4
		public override Texture mainTexture
		{
			get
			{
				if (this.font != null && this.font.material != null && this.font.material.mainTexture != null)
				{
					return this.font.material.mainTexture;
				}
				if (this.m_Material != null)
				{
					return this.m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00013578 File Offset: 0x00011778
		public void FontTextureChanged()
		{
			if (!this)
			{
				FontUpdateTracker.UntrackText(this);
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			this.cachedTextGenerator.Invalidate();
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
			}
			else
			{
				this.SetAllDirty();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000135E0 File Offset: 0x000117E0
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x000135F0 File Offset: 0x000117F0
		public Font font
		{
			get
			{
				return this.m_FontData.font;
			}
			set
			{
				if (this.m_FontData.font == value)
				{
					return;
				}
				FontUpdateTracker.UntrackText(this);
				this.m_FontData.font = value;
				FontUpdateTracker.TrackText(this);
				this.SetAllDirty();
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00013628 File Offset: 0x00011828
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00013630 File Offset: 0x00011830
		public virtual string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					if (string.IsNullOrEmpty(this.m_Text))
					{
						return;
					}
					this.m_Text = string.Empty;
					this.SetVerticesDirty();
				}
				else if (this.m_Text != value)
				{
					this.m_Text = value;
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00013694 File Offset: 0x00011894
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x000136A4 File Offset: 0x000118A4
		public bool supportRichText
		{
			get
			{
				return this.m_FontData.richText;
			}
			set
			{
				if (this.m_FontData.richText == value)
				{
					return;
				}
				this.m_FontData.richText = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000136D0 File Offset: 0x000118D0
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x000136E0 File Offset: 0x000118E0
		public bool resizeTextForBestFit
		{
			get
			{
				return this.m_FontData.bestFit;
			}
			set
			{
				if (this.m_FontData.bestFit == value)
				{
					return;
				}
				this.m_FontData.bestFit = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001370C File Offset: 0x0001190C
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0001371C File Offset: 0x0001191C
		public int resizeTextMinSize
		{
			get
			{
				return this.m_FontData.minSize;
			}
			set
			{
				if (this.m_FontData.minSize == value)
				{
					return;
				}
				this.m_FontData.minSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00013748 File Offset: 0x00011948
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00013758 File Offset: 0x00011958
		public int resizeTextMaxSize
		{
			get
			{
				return this.m_FontData.maxSize;
			}
			set
			{
				if (this.m_FontData.maxSize == value)
				{
					return;
				}
				this.m_FontData.maxSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00013784 File Offset: 0x00011984
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00013794 File Offset: 0x00011994
		public TextAnchor alignment
		{
			get
			{
				return this.m_FontData.alignment;
			}
			set
			{
				if (this.m_FontData.alignment == value)
				{
					return;
				}
				this.m_FontData.alignment = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000137C0 File Offset: 0x000119C0
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x000137D0 File Offset: 0x000119D0
		public int fontSize
		{
			get
			{
				return this.m_FontData.fontSize;
			}
			set
			{
				if (this.m_FontData.fontSize == value)
				{
					return;
				}
				this.m_FontData.fontSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000137FC File Offset: 0x000119FC
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0001380C File Offset: 0x00011A0C
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_FontData.horizontalOverflow;
			}
			set
			{
				if (this.m_FontData.horizontalOverflow == value)
				{
					return;
				}
				this.m_FontData.horizontalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00013838 File Offset: 0x00011A38
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00013848 File Offset: 0x00011A48
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_FontData.verticalOverflow;
			}
			set
			{
				if (this.m_FontData.verticalOverflow == value)
				{
					return;
				}
				this.m_FontData.verticalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00013874 File Offset: 0x00011A74
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00013884 File Offset: 0x00011A84
		public float lineSpacing
		{
			get
			{
				return this.m_FontData.lineSpacing;
			}
			set
			{
				if (this.m_FontData.lineSpacing == value)
				{
					return;
				}
				this.m_FontData.lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000138B0 File Offset: 0x00011AB0
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x000138C0 File Offset: 0x00011AC0
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontData.fontStyle;
			}
			set
			{
				if (this.m_FontData.fontStyle == value)
				{
					return;
				}
				this.m_FontData.fontStyle = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000138EC File Offset: 0x00011AEC
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font || this.font.dynamic)
				{
					return canvas.scaleFactor;
				}
				if (this.m_FontData.fontSize <= 0 || this.font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)this.font.fontSize / (float)this.m_FontData.fontSize;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001397C File Offset: 0x00011B7C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.cachedTextGenerator.Invalidate();
			FontUpdateTracker.TrackText(this);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013998 File Offset: 0x00011B98
		protected override void OnDisable()
		{
			FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000139A8 File Offset: 0x00011BA8
		protected override void UpdateGeometry()
		{
			if (this.font != null)
			{
				base.UpdateGeometry();
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000139C4 File Offset: 0x00011BC4
		public TextGenerationSettings GetGenerationSettings(Vector2 extents)
		{
			TextGenerationSettings result = default(TextGenerationSettings);
			result.generationExtents = extents;
			if (this.font != null && this.font.dynamic)
			{
				result.fontSize = this.m_FontData.fontSize;
				result.resizeTextMinSize = this.m_FontData.minSize;
				result.resizeTextMaxSize = this.m_FontData.maxSize;
			}
			result.textAnchor = this.m_FontData.alignment;
			result.scaleFactor = this.pixelsPerUnit;
			result.color = base.color;
			result.font = this.font;
			result.pivot = base.rectTransform.pivot;
			result.richText = this.m_FontData.richText;
			result.lineSpacing = this.m_FontData.lineSpacing;
			result.fontStyle = this.m_FontData.fontStyle;
			result.resizeTextForBestFit = this.m_FontData.bestFit;
			result.updateBounds = false;
			result.horizontalOverflow = this.m_FontData.horizontalOverflow;
			result.verticalOverflow = this.m_FontData.verticalOverflow;
			return result;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public static Vector2 GetTextAnchorPivot(TextAnchor anchor)
		{
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				return new Vector2(0f, 1f);
			case TextAnchor.UpperCenter:
				return new Vector2(0.5f, 1f);
			case TextAnchor.UpperRight:
				return new Vector2(1f, 1f);
			case TextAnchor.MiddleLeft:
				return new Vector2(0f, 0.5f);
			case TextAnchor.MiddleCenter:
				return new Vector2(0.5f, 0.5f);
			case TextAnchor.MiddleRight:
				return new Vector2(1f, 0.5f);
			case TextAnchor.LowerLeft:
				return new Vector2(0f, 0f);
			case TextAnchor.LowerCenter:
				return new Vector2(0.5f, 0f);
			case TextAnchor.LowerRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013BCC File Offset: 0x00011DCC
		protected override void OnFillVBO(List<UIVertex> vbo)
		{
			if (this.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			Vector2 size = base.rectTransform.rect.size;
			TextGenerationSettings generationSettings = this.GetGenerationSettings(size);
			this.cachedTextGenerator.Populate(this.m_Text, generationSettings);
			Rect rect = base.rectTransform.rect;
			Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_FontData.alignment);
			Vector2 zero = Vector2.zero;
			zero.x = ((textAnchorPivot.x != 1f) ? rect.xMin : rect.xMax);
			zero.y = ((textAnchorPivot.y != 0f) ? rect.yMax : rect.yMin);
			Vector2 lhs = base.PixelAdjustPoint(zero) - zero;
			IList<UIVertex> verts = this.cachedTextGenerator.verts;
			float d = 1f / this.pixelsPerUnit;
			if (lhs != Vector2.zero)
			{
				for (int i = 0; i < verts.Count; i++)
				{
					UIVertex item = verts[i];
					item.position *= d;
					item.position.x = item.position.x + lhs.x;
					item.position.y = item.position.y + lhs.y;
					vbo.Add(item);
				}
			}
			else
			{
				for (int j = 0; j < verts.Count; j++)
				{
					UIVertex item2 = verts[j];
					item2.position *= d;
					vbo.Add(item2);
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013DA0 File Offset: 0x00011FA0
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013DA4 File Offset: 0x00011FA4
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00013DA8 File Offset: 0x00011FA8
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00013DB0 File Offset: 0x00011FB0
		public virtual float preferredWidth
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(Vector2.zero);
				return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00013DE4 File Offset: 0x00011FE4
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00013DEC File Offset: 0x00011FEC
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00013DF4 File Offset: 0x00011FF4
		public virtual float preferredHeight
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(new Vector2(base.rectTransform.rect.size.x, 0f));
				return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00013E48 File Offset: 0x00012048
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00013E50 File Offset: 0x00012050
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000248 RID: 584
		[SerializeField]
		private FontData m_FontData = FontData.defaultFontData;

		// Token: 0x04000249 RID: 585
		[SerializeField]
		[TextArea(3, 10)]
		protected string m_Text = string.Empty;

		// Token: 0x0400024A RID: 586
		private TextGenerator m_TextCache;

		// Token: 0x0400024B RID: 587
		private TextGenerator m_TextCacheForLayout;

		// Token: 0x0400024C RID: 588
		protected static Material s_DefaultText;

		// Token: 0x0400024D RID: 589
		[NonSerialized]
		private bool m_DisableFontTextureRebuiltCallback;
	}
}
