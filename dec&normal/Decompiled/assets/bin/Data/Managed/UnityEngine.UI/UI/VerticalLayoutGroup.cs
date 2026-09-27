using System;

namespace UnityEngine.UI
{
	// Token: 0x02000093 RID: 147
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x000142A8 File Offset: 0x000124A8
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000142B0 File Offset: 0x000124B0
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000142C0 File Offset: 0x000124C0
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000142CC File Offset: 0x000124CC
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000142D8 File Offset: 0x000124D8
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
