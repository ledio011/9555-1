using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000077 RID: 119
	[AddComponentMenu("UI/Effects/Outline", 15)]
	public class Outline : Shadow
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		protected Outline()
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		public override void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			int start = 0;
			int count = verts.Count;
			base.ApplyShadow(verts, base.effectColor, start, verts.Count, base.effectDistance.x, base.effectDistance.y);
			start = count;
			count = verts.Count;
			base.ApplyShadow(verts, base.effectColor, start, verts.Count, base.effectDistance.x, -base.effectDistance.y);
			start = count;
			count = verts.Count;
			base.ApplyShadow(verts, base.effectColor, start, verts.Count, -base.effectDistance.x, base.effectDistance.y);
			start = count;
			count = verts.Count;
			base.ApplyShadow(verts, base.effectColor, start, verts.Count, -base.effectDistance.x, -base.effectDistance.y);
		}
	}
}
