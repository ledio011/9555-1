using System;
using System.Collections;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000031 RID: 49
	internal class ContractionComparer : IComparer
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003C1C File Offset: 0x00001E1C
		public int Compare(object o1, object o2)
		{
			Contraction contraction = (Contraction)o1;
			Contraction contraction2 = (Contraction)o2;
			char[] source = contraction.Source;
			char[] source2 = contraction2.Source;
			int num = (source.Length <= source2.Length) ? source.Length : source2.Length;
			for (int i = 0; i < num; i++)
			{
				if (source[i] != source2[i])
				{
					return (int)(source[i] - source2[i]);
				}
			}
			return source.Length - source2.Length;
		}

		// Token: 0x04000080 RID: 128
		public static readonly ContractionComparer Instance = new ContractionComparer();
	}
}
