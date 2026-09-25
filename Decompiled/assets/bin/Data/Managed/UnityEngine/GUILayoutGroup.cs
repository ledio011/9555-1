using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000079 RID: 121
	internal class GUILayoutGroup : GUILayoutEntry
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		public GUILayoutGroup() : base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public override RectOffset margin
		{
			get
			{
				return this.m_Margin;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		public override void ApplyOptions(GUILayoutOption[] options)
		{
			if (options == null)
			{
				return;
			}
			base.ApplyOptions(options);
			foreach (GUILayoutOption guilayoutOption in options)
			{
				switch (guilayoutOption.type)
				{
				case GUILayoutOption.Type.fixedWidth:
				case GUILayoutOption.Type.minWidth:
				case GUILayoutOption.Type.maxWidth:
					this.userSpecifiedHeight = true;
					break;
				case GUILayoutOption.Type.fixedHeight:
				case GUILayoutOption.Type.minHeight:
				case GUILayoutOption.Type.maxHeight:
					this.userSpecifiedWidth = true;
					break;
				case GUILayoutOption.Type.spacing:
					this.spacing = (float)((int)guilayoutOption.value);
					break;
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000DC68 File Offset: 0x0000BE68
		protected override void ApplyStyleSettings(GUIStyle style)
		{
			base.ApplyStyleSettings(style);
			RectOffset margin = style.margin;
			this.m_Margin.left = margin.left;
			this.m_Margin.right = margin.right;
			this.m_Margin.top = margin.top;
			this.m_Margin.bottom = margin.bottom;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public void ResetCursor()
		{
			this.cursor = 0;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public GUILayoutEntry GetNext()
		{
			if (this.cursor < this.entries.Count)
			{
				GUILayoutEntry result = this.entries[this.cursor];
				this.cursor++;
				return result;
			}
			throw new ArgumentException(string.Concat(new object[]
			{
				"Getting control ",
				this.cursor,
				"'s position in a group with only ",
				this.entries.Count,
				" controls when doing ",
				Event.current.rawType,
				"\nAborting"
			}));
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public void Add(GUILayoutEntry e)
		{
			this.entries.Add(e);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public override void CalcWidth()
		{
			if (this.entries.Count == 0)
			{
				this.maxWidth = (this.minWidth = (float)base.style.padding.horizontal);
				return;
			}
			this.childMinWidth = 0f;
			this.childMaxWidth = 0f;
			int num = 0;
			int num2 = 0;
			this.stretchableCountX = 0;
			bool flag = true;
			if (this.isVertical)
			{
				foreach (GUILayoutEntry guilayoutEntry in this.entries)
				{
					guilayoutEntry.CalcWidth();
					RectOffset margin = guilayoutEntry.margin;
					if (guilayoutEntry.style != GUILayoutUtility.spaceStyle)
					{
						if (!flag)
						{
							num = Mathf.Min(margin.left, num);
							num2 = Mathf.Min(margin.right, num2);
						}
						else
						{
							num = margin.left;
							num2 = margin.right;
							flag = false;
						}
						this.childMinWidth = Mathf.Max(guilayoutEntry.minWidth + (float)margin.horizontal, this.childMinWidth);
						this.childMaxWidth = Mathf.Max(guilayoutEntry.maxWidth + (float)margin.horizontal, this.childMaxWidth);
					}
					this.stretchableCountX += guilayoutEntry.stretchWidth;
				}
				this.childMinWidth -= (float)(num + num2);
				this.childMaxWidth -= (float)(num + num2);
			}
			else
			{
				int num3 = 0;
				foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
				{
					guilayoutEntry2.CalcWidth();
					RectOffset margin2 = guilayoutEntry2.margin;
					if (guilayoutEntry2.style != GUILayoutUtility.spaceStyle)
					{
						int num4;
						if (!flag)
						{
							num4 = ((num3 <= margin2.left) ? margin2.left : num3);
						}
						else
						{
							num4 = 0;
							flag = false;
						}
						this.childMinWidth += guilayoutEntry2.minWidth + this.spacing + (float)num4;
						this.childMaxWidth += guilayoutEntry2.maxWidth + this.spacing + (float)num4;
						num3 = margin2.right;
						this.stretchableCountX += guilayoutEntry2.stretchWidth;
					}
					else
					{
						this.childMinWidth += guilayoutEntry2.minWidth;
						this.childMaxWidth += guilayoutEntry2.maxWidth;
						this.stretchableCountX += guilayoutEntry2.stretchWidth;
					}
				}
				this.childMinWidth -= this.spacing;
				this.childMaxWidth -= this.spacing;
				if (this.entries.Count != 0)
				{
					num = this.entries[0].margin.left;
					num2 = num3;
				}
				else
				{
					num2 = (num = 0);
				}
			}
			float num5;
			float num6;
			if (base.style != GUIStyle.none || this.userSpecifiedWidth)
			{
				num5 = (float)Mathf.Max(base.style.padding.left, num);
				num6 = (float)Mathf.Max(base.style.padding.right, num2);
			}
			else
			{
				this.m_Margin.left = num;
				this.m_Margin.right = num2;
				num6 = (num5 = 0f);
			}
			this.minWidth = Mathf.Max(this.minWidth, this.childMinWidth + num5 + num6);
			if (this.maxWidth == 0f)
			{
				this.stretchWidth += this.stretchableCountX + ((!base.style.stretchWidth) ? 0 : 1);
				this.maxWidth = this.childMaxWidth + num5 + num6;
			}
			else
			{
				this.stretchWidth = 0;
			}
			this.maxWidth = Mathf.Max(this.maxWidth, this.minWidth);
			if (base.style.fixedWidth != 0f)
			{
				this.maxWidth = (this.minWidth = base.style.fixedWidth);
				this.stretchWidth = 0;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000E200 File Offset: 0x0000C400
		public override void SetHorizontal(float x, float width)
		{
			base.SetHorizontal(x, width);
			if (this.resetCoords)
			{
				x = 0f;
			}
			RectOffset padding = base.style.padding;
			if (this.isVertical)
			{
				if (base.style != GUIStyle.none)
				{
					foreach (GUILayoutEntry guilayoutEntry in this.entries)
					{
						float num = (float)Mathf.Max(guilayoutEntry.margin.left, padding.left);
						float x2 = x + num;
						float num2 = width - (float)Mathf.Max(guilayoutEntry.margin.right, padding.right) - num;
						if (guilayoutEntry.stretchWidth != 0)
						{
							guilayoutEntry.SetHorizontal(x2, num2);
						}
						else
						{
							guilayoutEntry.SetHorizontal(x2, Mathf.Clamp(num2, guilayoutEntry.minWidth, guilayoutEntry.maxWidth));
						}
					}
				}
				else
				{
					float num3 = x - (float)this.margin.left;
					float num4 = width + (float)this.margin.horizontal;
					foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
					{
						if (guilayoutEntry2.stretchWidth != 0)
						{
							guilayoutEntry2.SetHorizontal(num3 + (float)guilayoutEntry2.margin.left, num4 - (float)guilayoutEntry2.margin.horizontal);
						}
						else
						{
							guilayoutEntry2.SetHorizontal(num3 + (float)guilayoutEntry2.margin.left, Mathf.Clamp(num4 - (float)guilayoutEntry2.margin.horizontal, guilayoutEntry2.minWidth, guilayoutEntry2.maxWidth));
						}
					}
				}
			}
			else
			{
				if (base.style != GUIStyle.none)
				{
					float num5 = (float)padding.left;
					float num6 = (float)padding.right;
					if (this.entries.Count != 0)
					{
						num5 = Mathf.Max(num5, (float)this.entries[0].margin.left);
						num6 = Mathf.Max(num6, (float)this.entries[this.entries.Count - 1].margin.right);
					}
					x += num5;
					width -= num6 + num5;
				}
				float num7 = width - this.spacing * (float)(this.entries.Count - 1);
				float t = 0f;
				if (this.childMinWidth != this.childMaxWidth)
				{
					t = Mathf.Clamp((num7 - this.childMinWidth) / (this.childMaxWidth - this.childMinWidth), 0f, 1f);
				}
				float num8 = 0f;
				if (num7 > this.childMaxWidth && this.stretchableCountX > 0)
				{
					num8 = (num7 - this.childMaxWidth) / (float)this.stretchableCountX;
				}
				int num9 = 0;
				bool flag = true;
				foreach (GUILayoutEntry guilayoutEntry3 in this.entries)
				{
					float num10 = Mathf.Lerp(guilayoutEntry3.minWidth, guilayoutEntry3.maxWidth, t);
					num10 += num8 * (float)guilayoutEntry3.stretchWidth;
					if (guilayoutEntry3.style != GUILayoutUtility.spaceStyle)
					{
						int num11 = guilayoutEntry3.margin.left;
						if (flag)
						{
							num11 = 0;
							flag = false;
						}
						int num12 = (num9 <= num11) ? num11 : num9;
						x += (float)num12;
						num9 = guilayoutEntry3.margin.right;
					}
					guilayoutEntry3.SetHorizontal(Mathf.Round(x), Mathf.Round(num10));
					x += num10 + this.spacing;
				}
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		public override void CalcHeight()
		{
			if (this.entries.Count == 0)
			{
				this.maxHeight = (this.minHeight = (float)base.style.padding.vertical);
				return;
			}
			this.childMinHeight = (this.childMaxHeight = 0f);
			int num = 0;
			int num2 = 0;
			this.stretchableCountY = 0;
			if (this.isVertical)
			{
				int num3 = 0;
				bool flag = true;
				foreach (GUILayoutEntry guilayoutEntry in this.entries)
				{
					guilayoutEntry.CalcHeight();
					RectOffset margin = guilayoutEntry.margin;
					if (guilayoutEntry.style != GUILayoutUtility.spaceStyle)
					{
						int num4;
						if (!flag)
						{
							num4 = Mathf.Max(num3, margin.top);
						}
						else
						{
							num4 = 0;
							flag = false;
						}
						this.childMinHeight += guilayoutEntry.minHeight + this.spacing + (float)num4;
						this.childMaxHeight += guilayoutEntry.maxHeight + this.spacing + (float)num4;
						num3 = margin.bottom;
						this.stretchableCountY += guilayoutEntry.stretchHeight;
					}
					else
					{
						this.childMinHeight += guilayoutEntry.minHeight;
						this.childMaxHeight += guilayoutEntry.maxHeight;
						this.stretchableCountY += guilayoutEntry.stretchHeight;
					}
				}
				this.childMinHeight -= this.spacing;
				this.childMaxHeight -= this.spacing;
				if (this.entries.Count != 0)
				{
					num = this.entries[0].margin.top;
					num2 = num3;
				}
				else
				{
					num = (num2 = 0);
				}
			}
			else
			{
				bool flag2 = true;
				foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
				{
					guilayoutEntry2.CalcHeight();
					RectOffset margin2 = guilayoutEntry2.margin;
					if (guilayoutEntry2.style != GUILayoutUtility.spaceStyle)
					{
						if (!flag2)
						{
							num = Mathf.Min(margin2.top, num);
							num2 = Mathf.Min(margin2.bottom, num2);
						}
						else
						{
							num = margin2.top;
							num2 = margin2.bottom;
							flag2 = false;
						}
						this.childMinHeight = Mathf.Max(guilayoutEntry2.minHeight, this.childMinHeight);
						this.childMaxHeight = Mathf.Max(guilayoutEntry2.maxHeight, this.childMaxHeight);
					}
					this.stretchableCountY += guilayoutEntry2.stretchHeight;
				}
			}
			float num5;
			float num6;
			if (base.style != GUIStyle.none || this.userSpecifiedHeight)
			{
				num5 = (float)Mathf.Max(base.style.padding.top, num);
				num6 = (float)Mathf.Max(base.style.padding.bottom, num2);
			}
			else
			{
				this.m_Margin.top = num;
				this.m_Margin.bottom = num2;
				num6 = (num5 = 0f);
			}
			this.minHeight = Mathf.Max(this.minHeight, this.childMinHeight + num5 + num6);
			if (this.maxHeight == 0f)
			{
				this.stretchHeight += this.stretchableCountY + ((!base.style.stretchHeight) ? 0 : 1);
				this.maxHeight = this.childMaxHeight + num5 + num6;
			}
			else
			{
				this.stretchHeight = 0;
			}
			this.maxHeight = Mathf.Max(this.maxHeight, this.minHeight);
			if (base.style.fixedHeight != 0f)
			{
				this.maxHeight = (this.minHeight = base.style.fixedHeight);
				this.stretchHeight = 0;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000EA08 File Offset: 0x0000CC08
		public override void SetVertical(float y, float height)
		{
			base.SetVertical(y, height);
			if (this.entries.Count == 0)
			{
				return;
			}
			RectOffset padding = base.style.padding;
			if (this.resetCoords)
			{
				y = 0f;
			}
			if (this.isVertical)
			{
				if (base.style != GUIStyle.none)
				{
					float num = (float)padding.top;
					float num2 = (float)padding.bottom;
					if (this.entries.Count != 0)
					{
						num = Mathf.Max(num, (float)this.entries[0].margin.top);
						num2 = Mathf.Max(num2, (float)this.entries[this.entries.Count - 1].margin.bottom);
					}
					y += num;
					height -= num2 + num;
				}
				float num3 = height - this.spacing * (float)(this.entries.Count - 1);
				float t = 0f;
				if (this.childMinHeight != this.childMaxHeight)
				{
					t = Mathf.Clamp((num3 - this.childMinHeight) / (this.childMaxHeight - this.childMinHeight), 0f, 1f);
				}
				float num4 = 0f;
				if (num3 > this.childMaxHeight && this.stretchableCountY > 0)
				{
					num4 = (num3 - this.childMaxHeight) / (float)this.stretchableCountY;
				}
				int num5 = 0;
				bool flag = true;
				foreach (GUILayoutEntry guilayoutEntry in this.entries)
				{
					float num6 = Mathf.Lerp(guilayoutEntry.minHeight, guilayoutEntry.maxHeight, t);
					num6 += num4 * (float)guilayoutEntry.stretchHeight;
					if (guilayoutEntry.style != GUILayoutUtility.spaceStyle)
					{
						int num7 = guilayoutEntry.margin.top;
						if (flag)
						{
							num7 = 0;
							flag = false;
						}
						int num8 = (num5 <= num7) ? num7 : num5;
						y += (float)num8;
						num5 = guilayoutEntry.margin.bottom;
					}
					guilayoutEntry.SetVertical(Mathf.Round(y), Mathf.Round(num6));
					y += num6 + this.spacing;
				}
			}
			else if (base.style != GUIStyle.none)
			{
				foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
				{
					float num9 = (float)Mathf.Max(guilayoutEntry2.margin.top, padding.top);
					float y2 = y + num9;
					float num10 = height - (float)Mathf.Max(guilayoutEntry2.margin.bottom, padding.bottom) - num9;
					if (guilayoutEntry2.stretchHeight != 0)
					{
						guilayoutEntry2.SetVertical(y2, num10);
					}
					else
					{
						guilayoutEntry2.SetVertical(y2, Mathf.Clamp(num10, guilayoutEntry2.minHeight, guilayoutEntry2.maxHeight));
					}
				}
			}
			else
			{
				float num11 = y - (float)this.margin.top;
				float num12 = height + (float)this.margin.vertical;
				foreach (GUILayoutEntry guilayoutEntry3 in this.entries)
				{
					if (guilayoutEntry3.stretchHeight != 0)
					{
						guilayoutEntry3.SetVertical(num11 + (float)guilayoutEntry3.margin.top, num12 - (float)guilayoutEntry3.margin.vertical);
					}
					else
					{
						guilayoutEntry3.SetVertical(num11 + (float)guilayoutEntry3.margin.top, Mathf.Clamp(num12 - (float)guilayoutEntry3.margin.vertical, guilayoutEntry3.minHeight, guilayoutEntry3.maxHeight));
					}
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000EE00 File Offset: 0x0000D000
		public override string ToString()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text2 += " ";
			}
			string text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				base.ToString(),
				" Margins: ",
				this.childMinHeight,
				" {\n"
			});
			GUILayoutEntry.indent += 4;
			foreach (GUILayoutEntry guilayoutEntry in this.entries)
			{
				text = text + guilayoutEntry.ToString() + "\n";
			}
			text = text + text2 + "}";
			GUILayoutEntry.indent -= 4;
			return text;
		}

		// Token: 0x04000126 RID: 294
		public List<GUILayoutEntry> entries = new List<GUILayoutEntry>();

		// Token: 0x04000127 RID: 295
		public bool isVertical = true;

		// Token: 0x04000128 RID: 296
		public bool resetCoords;

		// Token: 0x04000129 RID: 297
		public float spacing;

		// Token: 0x0400012A RID: 298
		public bool sameSize = true;

		// Token: 0x0400012B RID: 299
		public bool isWindow;

		// Token: 0x0400012C RID: 300
		public int windowID = -1;

		// Token: 0x0400012D RID: 301
		private int cursor;

		// Token: 0x0400012E RID: 302
		protected int stretchableCountX = 100;

		// Token: 0x0400012F RID: 303
		protected int stretchableCountY = 100;

		// Token: 0x04000130 RID: 304
		protected bool userSpecifiedWidth;

		// Token: 0x04000131 RID: 305
		protected bool userSpecifiedHeight;

		// Token: 0x04000132 RID: 306
		protected float childMinWidth = 100f;

		// Token: 0x04000133 RID: 307
		protected float childMaxWidth = 100f;

		// Token: 0x04000134 RID: 308
		protected float childMinHeight = 100f;

		// Token: 0x04000135 RID: 309
		protected float childMaxHeight = 100f;

		// Token: 0x04000136 RID: 310
		private RectOffset m_Margin = new RectOffset();
	}
}
