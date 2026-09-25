using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000AD RID: 173
	[ComVisible(true)]
	public interface IEqualityComparer
	{
		// Token: 0x060005C9 RID: 1481
		bool Equals(object x, object y);

		// Token: 0x060005CA RID: 1482
		int GetHashCode(object obj);
	}
}
