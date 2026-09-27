using System;

namespace UnityEngine
{
	// Token: 0x0200007E RID: 126
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public override void CalcWidth()
		{
			float minWidth = this.minWidth;
			float maxWidth = this.maxWidth;
			if (this.allowHorizontalScroll)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			if (this.allowHorizontalScroll)
			{
				if (this.minWidth > 32f)
				{
					this.minWidth = 32f;
				}
				if (minWidth != 0f)
				{
					this.minWidth = minWidth;
				}
				if (maxWidth != 0f)
				{
					this.maxWidth = maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000F858 File Offset: 0x0000DA58
		public override void SetHorizontal(float x, float width)
		{
			float num = (!this.needsVerticalScrollbar) ? width : (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left);
			if (this.allowHorizontalScroll && num < this.calcMinWidth)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				if (this.allowHorizontalScroll)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, num);
				this.rect.width = width;
				this.clientWidth = num;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000F93C File Offset: 0x0000DB3C
		public override void CalcHeight()
		{
			float minHeight = this.minHeight;
			float maxHeight = this.maxHeight;
			if (this.allowVerticalScroll)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			if (this.needsHorizontalScrollbar)
			{
				float num = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += num;
				this.maxHeight += num;
			}
			if (this.allowVerticalScroll)
			{
				if (this.minHeight > 32f)
				{
					this.minHeight = 32f;
				}
				if (minHeight != 0f)
				{
					this.minHeight = minHeight;
				}
				if (maxHeight != 0f)
				{
					this.maxHeight = maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		public override void SetVertical(float y, float height)
		{
			float num = height;
			if (this.needsHorizontalScrollbar)
			{
				num -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			if (this.allowVerticalScroll && num < this.calcMinHeight)
			{
				if (!this.needsHorizontalScrollbar && !this.needsVerticalScrollbar)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					if (this.clientWidth < this.calcMinWidth)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float width = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = width;
				}
				float minHeight = this.minHeight;
				float maxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = minHeight;
				this.maxHeight = maxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				if (this.allowVerticalScroll)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, num);
				this.rect.height = height;
				this.clientHeight = num;
			}
		}

		// Token: 0x04000150 RID: 336
		public float calcMinWidth;

		// Token: 0x04000151 RID: 337
		public float calcMaxWidth;

		// Token: 0x04000152 RID: 338
		public float calcMinHeight;

		// Token: 0x04000153 RID: 339
		public float calcMaxHeight;

		// Token: 0x04000154 RID: 340
		public float clientWidth;

		// Token: 0x04000155 RID: 341
		public float clientHeight;

		// Token: 0x04000156 RID: 342
		public bool allowHorizontalScroll = true;

		// Token: 0x04000157 RID: 343
		public bool allowVerticalScroll = true;

		// Token: 0x04000158 RID: 344
		public bool needsHorizontalScrollbar;

		// Token: 0x04000159 RID: 345
		public bool needsVerticalScrollbar;

		// Token: 0x0400015A RID: 346
		public GUIStyle horizontalScrollbar;

		// Token: 0x0400015B RID: 347
		public GUIStyle verticalScrollbar;
	}
}
