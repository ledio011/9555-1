using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200008C RID: 140
	public static class StencilMaterial
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00013240 File Offset: 0x00011440
		public static Material Add(Material baseMat, int stencilID)
		{
			if (stencilID <= 0 || baseMat == null)
			{
				return null;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have stencil properties", baseMat);
				return null;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (matEntry.baseMat == baseMat && matEntry.stencilID == stencilID)
				{
					matEntry.count++;
					return matEntry.customMat;
				}
			}
			StencilMaterial.MatEntry matEntry2 = new StencilMaterial.MatEntry();
			matEntry2.count = 1;
			matEntry2.baseMat = baseMat;
			matEntry2.customMat = new Material(baseMat);
			matEntry2.customMat.name = string.Concat(new object[]
			{
				"Stencil ",
				stencilID,
				" (",
				baseMat.name,
				")"
			});
			matEntry2.customMat.hideFlags = HideFlags.HideAndDontSave;
			matEntry2.stencilID = stencilID;
			if (baseMat.HasProperty("_StencilComp"))
			{
				matEntry2.customMat.SetInt("_StencilComp", 3);
			}
			matEntry2.customMat.SetInt("_Stencil", stencilID);
			StencilMaterial.m_List.Add(matEntry2);
			return matEntry2.customMat;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000133A0 File Offset: 0x000115A0
		public static void Remove(Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (!(matEntry.customMat != customMat))
				{
					if (--matEntry.count == 0)
					{
						Misc.DestroyImmediate(matEntry.customMat);
						matEntry.baseMat = null;
						StencilMaterial.m_List.RemoveAt(i);
					}
					return;
				}
			}
		}

		// Token: 0x04000243 RID: 579
		private static List<StencilMaterial.MatEntry> m_List = new List<StencilMaterial.MatEntry>();

		// Token: 0x0200008D RID: 141
		private class MatEntry
		{
			// Token: 0x04000244 RID: 580
			public Material baseMat;

			// Token: 0x04000245 RID: 581
			public Material customMat;

			// Token: 0x04000246 RID: 582
			public int count;

			// Token: 0x04000247 RID: 583
			public int stencilID;
		}
	}
}
