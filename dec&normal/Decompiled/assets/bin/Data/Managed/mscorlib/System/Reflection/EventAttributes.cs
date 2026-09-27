using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001C3 RID: 451
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum EventAttributes
	{
		// Token: 0x04000890 RID: 2192
		None = 0,
		// Token: 0x04000891 RID: 2193
		SpecialName = 512,
		// Token: 0x04000892 RID: 2194
		ReservedMask = 1024,
		// Token: 0x04000893 RID: 2195
		RTSpecialName = 1024
	}
}
