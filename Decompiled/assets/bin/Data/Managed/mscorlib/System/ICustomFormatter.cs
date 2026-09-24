using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200010D RID: 269
	[ComVisible(true)]
	public interface ICustomFormatter
	{
		// Token: 0x06000A94 RID: 2708
		string Format(string format, object arg, IFormatProvider formatProvider);
	}
}
