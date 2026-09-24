using System;

namespace UnityEngine
{
	// Token: 0x02000078 RID: 120
	internal class GUILayoutEntry
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			if (_style == null)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000D644 File Offset: 0x0000B844
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		public virtual RectOffset margin
		{
			get
			{
				return this.style.margin;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000D700 File Offset: 0x0000B900
		public virtual void CalcWidth()
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000D704 File Offset: 0x0000B904
		public virtual void CalcHeight()
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000D708 File Offset: 0x0000B908
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000D724 File Offset: 0x0000B924
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000D740 File Offset: 0x0000B940
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth != 0f || !style.stretchWidth) ? 0 : 1);
			this.stretchHeight = ((style.fixedHeight != 0f || !style.stretchHeight) ? 0 : 1);
			this.m_Style = style;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			if (options == null)
			{
				return;
			}
			foreach (GUILayoutOption guilayoutOption in options)
			{
				switch (guilayoutOption.type)
				{
				case GUILayoutOption.Type.fixedWidth:
					this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.fixedHeight:
					this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.minWidth:
					this.minWidth = (float)guilayoutOption.value;
					if (this.maxWidth < this.minWidth)
					{
						this.maxWidth = this.minWidth;
					}
					break;
				case GUILayoutOption.Type.maxWidth:
					this.maxWidth = (float)guilayoutOption.value;
					if (this.minWidth > this.maxWidth)
					{
						this.minWidth = this.maxWidth;
					}
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.minHeight:
					this.minHeight = (float)guilayoutOption.value;
					if (this.maxHeight < this.minHeight)
					{
						this.maxHeight = this.minHeight;
					}
					break;
				case GUILayoutOption.Type.maxHeight:
					this.maxHeight = (float)guilayoutOption.value;
					if (this.minHeight > this.maxHeight)
					{
						this.minHeight = this.maxHeight;
					}
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.stretchWidth:
					this.stretchWidth = (int)guilayoutOption.value;
					break;
				case GUILayoutOption.Type.stretchHeight:
					this.stretchHeight = (int)guilayoutOption.value;
					break;
				}
			}
			if (this.maxWidth != 0f && this.maxWidth < this.minWidth)
			{
				this.maxWidth = this.minWidth;
			}
			if (this.maxHeight != 0f && this.maxHeight < this.minHeight)
			{
				this.maxHeight = this.minHeight;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new object[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style == null) ? "NULL" : this.style.name,
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth,
				"-",
				this.maxWidth,
				(this.stretchWidth == 0) ? string.Empty : "+",
				", H: ",
				this.minHeight,
				"-",
				this.maxHeight,
				(this.stretchHeight == 0) ? string.Empty : "+"
			});
		}

		// Token: 0x0400011C RID: 284
		public float minWidth;

		// Token: 0x0400011D RID: 285
		public float maxWidth;

		// Token: 0x0400011E RID: 286
		public float minHeight;

		// Token: 0x0400011F RID: 287
		public float maxHeight;

		// Token: 0x04000120 RID: 288
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x04000121 RID: 289
		public int stretchWidth;

		// Token: 0x04000122 RID: 290
		public int stretchHeight;

		// Token: 0x04000123 RID: 291
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x04000124 RID: 292
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000125 RID: 293
		protected static int indent = 0;
	}
}
