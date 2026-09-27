using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000110 RID: 272
	[ComVisible(true)]
	public interface IFormatProvider
	{
		// Token: 0x06000A97 RID: 2711
		object GetFormat(Type formatType);
	}
}
