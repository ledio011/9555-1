using System;

namespace UnityEngine
{
	// Token: 0x02000088 RID: 136
	internal sealed class GUIWordWrapSizer : GUILayoutEntry
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00011080 File Offset: 0x0000F280
		public GUIWordWrapSizer(GUIStyle _style, GUIContent _content, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, _style)
		{
			this.content = new GUIContent(_content);
			base.ApplyOptions(options);
			this.forcedMinHeight = this.minHeight;
			this.forcedMaxHeight = this.maxHeight;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public override void CalcWidth()
		{
			if (this.minWidth == 0f || this.maxWidth == 0f)
			{
				float minWidth;
				float maxWidth;
				base.style.CalcMinMaxWidth(this.content, out minWidth, out maxWidth);
				if (this.minWidth == 0f)
				{
					this.minWidth = minWidth;
				}
				if (this.maxWidth == 0f)
				{
					this.maxWidth = maxWidth;
				}
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00011144 File Offset: 0x0000F344
		public override void CalcHeight()
		{
			if (this.forcedMinHeight == 0f || this.forcedMaxHeight == 0f)
			{
				float num = base.style.CalcHeight(this.content, this.rect.width);
				if (this.forcedMinHeight == 0f)
				{
					this.minHeight = num;
				}
				else
				{
					this.minHeight = this.forcedMinHeight;
				}
				if (this.forcedMaxHeight == 0f)
				{
					this.maxHeight = num;
				}
				else
				{
					this.maxHeight = this.forcedMaxHeight;
				}
			}
		}

		// Token: 0x04000194 RID: 404
		private GUIContent content;

		// Token: 0x04000195 RID: 405
		private float forcedMinHeight;

		// Token: 0x04000196 RID: 406
		private float forcedMaxHeight;
	}
}
