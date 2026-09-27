using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	// Token: 0x02000027 RID: 39
	[ComVisible(true)]
	public enum RegistryValueKind
	{
		// Token: 0x04000065 RID: 101
		Unknown,
		// Token: 0x04000066 RID: 102
		String,
		// Token: 0x04000067 RID: 103
		ExpandString,
		// Token: 0x04000068 RID: 104
		Binary,
		// Token: 0x04000069 RID: 105
		DWord,
		// Token: 0x0400006A RID: 106
		MultiString = 7,
		// Token: 0x0400006B RID: 107
		QWord = 11
	}
}
