using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000111 RID: 273
	[ComVisible(true)]
	public interface IFormattable
	{
		// Token: 0x06000A98 RID: 2712
		string ToString(string format, IFormatProvider formatProvider);
	}
}
