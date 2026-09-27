using System;

namespace UnityEngine.UI
{
	// Token: 0x02000057 RID: 87
	public interface ILayoutElement
	{
		// Token: 0x0600024D RID: 589
		void CalculateLayoutInputHorizontal();

		// Token: 0x0600024E RID: 590
		void CalculateLayoutInputVertical();

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024F RID: 591
		float minWidth { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000250 RID: 592
		float preferredWidth { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000251 RID: 593
		float flexibleWidth { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000252 RID: 594
		float minHeight { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000253 RID: 595
		float preferredHeight { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000254 RID: 596
		float flexibleHeight { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000255 RID: 597
		int layoutPriority { get; }
	}
}
