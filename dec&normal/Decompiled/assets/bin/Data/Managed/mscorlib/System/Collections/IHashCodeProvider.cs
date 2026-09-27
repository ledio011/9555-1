using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000AE RID: 174
	[ComVisible(true)]
	[Obsolete("Please use IEqualityComparer instead.")]
	public interface IHashCodeProvider
	{
		// Token: 0x060005CB RID: 1483
		int GetHashCode(object obj);
	}
}
