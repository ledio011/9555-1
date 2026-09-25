using System;
using System.Collections;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000033 RID: 51
	internal class Level2MapComparer : IComparer
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public int Compare(object o1, object o2)
		{
			Level2Map level2Map = (Level2Map)o1;
			Level2Map level2Map2 = (Level2Map)o2;
			return (int)(level2Map.Source - level2Map2.Source);
		}

		// Token: 0x04000083 RID: 131
		public static readonly Level2MapComparer Instance = new Level2MapComparer();
	}
}
