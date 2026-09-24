using System;

namespace System.Collections.Generic
{
	// Token: 0x02000097 RID: 151
	public interface IEnumerator<T> : IEnumerator, IDisposable
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000514 RID: 1300
		T Current { get; }
	}
}
