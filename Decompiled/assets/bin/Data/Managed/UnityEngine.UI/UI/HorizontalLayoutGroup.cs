using System;

namespace UnityEngine.UI
{
	// Token: 0x02000052 RID: 82
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00008354 File Offset: 0x00006554
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000835C File Offset: 0x0000655C
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000836C File Offset: 0x0000656C
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008378 File Offset: 0x00006578
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008384 File Offset: 0x00006584
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
