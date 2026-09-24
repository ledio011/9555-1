using System;

namespace UnityEngine.UI
{
	// Token: 0x02000073 RID: 115
	internal static class Misc
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000F33C File Offset: 0x0000D53C
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
				}
				else
				{
					Object.Destroy(obj);
				}
			}
		}
	}
}
