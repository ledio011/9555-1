using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A2 RID: 162
	public struct LayerMask
	{
		// Token: 0x0600071A RID: 1818
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x0600071B RID: 1819 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask result;
			result.m_Mask = intVal;
			return result;
		}

		// Token: 0x040002D2 RID: 722
		private int m_Mask;
	}
}
