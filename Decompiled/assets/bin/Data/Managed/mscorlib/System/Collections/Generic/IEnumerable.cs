using System;

namespace System.Collections.Generic
{
	// Token: 0x02000096 RID: 150
	public interface IEnumerable<T> : IEnumerable
	{
		// Token: 0x06000513 RID: 1299
		IEnumerator<T> GetEnumerator();
	}
}
