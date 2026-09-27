using System;

namespace System.Collections.Generic
{
	// Token: 0x02000098 RID: 152
	public interface IEqualityComparer<T>
	{
		// Token: 0x06000515 RID: 1301
		bool Equals(T x, T y);

		// Token: 0x06000516 RID: 1302
		int GetHashCode(T obj);
	}
}
