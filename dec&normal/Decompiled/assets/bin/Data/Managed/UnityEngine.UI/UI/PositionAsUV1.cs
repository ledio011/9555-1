using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000078 RID: 120
	[AddComponentMenu("UI/Effects/Position As UV1", 16)]
	public class PositionAsUV1 : BaseVertexEffect
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		protected PositionAsUV1()
		{
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		public override void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			for (int i = 0; i < verts.Count; i++)
			{
				UIVertex value = verts[i];
				value.uv1 = new Vector2(verts[i].position.x, verts[i].position.y);
				verts[i] = value;
			}
		}
	}
}
