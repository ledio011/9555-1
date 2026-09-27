using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200003F RID: 63
	internal static class ComponentListPool
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00006298 File Offset: 0x00004498
		public static List<Component> Get()
		{
			return ComponentListPool.s_ComponentListPool.Get();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000062A4 File Offset: 0x000044A4
		public static void Release(List<Component> toRelease)
		{
			ComponentListPool.s_ComponentListPool.Release(toRelease);
		}

		// Token: 0x040000D6 RID: 214
		private static readonly ObjectPool<List<Component>> s_ComponentListPool = new ObjectPool<List<Component>>(null, delegate(List<Component> l)
		{
			l.Clear();
		});
	}
}
