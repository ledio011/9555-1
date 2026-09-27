using System;

namespace System.Collections.Generic
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	internal sealed class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00016030 File Offset: 0x00014230
		public override int Compare(T x, T y)
		{
			if (x == null)
			{
				return (y != null) ? -1 : 0;
			}
			if (y == null)
			{
				return 1;
			}
			return x.CompareTo(y);
		}
	}
}
