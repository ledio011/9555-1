using System;

namespace System.Collections.Generic
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	internal sealed class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00016074 File Offset: 0x00014274
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016090 File Offset: 0x00014290
		public override bool Equals(T x, T y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}
	}
}
