using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200031A RID: 794
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum StreamingContextStates
	{
		// Token: 0x04000C93 RID: 3219
		CrossProcess = 1,
		// Token: 0x04000C94 RID: 3220
		CrossMachine = 2,
		// Token: 0x04000C95 RID: 3221
		File = 4,
		// Token: 0x04000C96 RID: 3222
		Persistence = 8,
		// Token: 0x04000C97 RID: 3223
		Remoting = 16,
		// Token: 0x04000C98 RID: 3224
		Other = 32,
		// Token: 0x04000C99 RID: 3225
		Clone = 64,
		// Token: 0x04000C9A RID: 3226
		CrossAppDomain = 128,
		// Token: 0x04000C9B RID: 3227
		All = 255
	}
}
