using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000036 RID: 54
	internal static class CanvasListPool
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00005670 File Offset: 0x00003870
		public static List<Canvas> Get()
		{
			return CanvasListPool.s_CanvasListPool.Get();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000567C File Offset: 0x0000387C
		public static void Release(List<Canvas> toRelease)
		{
			CanvasListPool.s_CanvasListPool.Release(toRelease);
		}

		// Token: 0x040000A1 RID: 161
		private static readonly ObjectPool<List<Canvas>> s_CanvasListPool = new ObjectPool<List<Canvas>>(null, delegate(List<Canvas> l)
		{
			l.Clear();
		});
	}
}
