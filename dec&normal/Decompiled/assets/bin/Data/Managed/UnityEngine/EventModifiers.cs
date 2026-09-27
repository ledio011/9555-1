using System;

namespace UnityEngine
{
	// Token: 0x0200004F RID: 79
	[Flags]
	public enum EventModifiers
	{
		// Token: 0x04000098 RID: 152
		None = 0,
		// Token: 0x04000099 RID: 153
		Shift = 1,
		// Token: 0x0400009A RID: 154
		Control = 2,
		// Token: 0x0400009B RID: 155
		Alt = 4,
		// Token: 0x0400009C RID: 156
		Command = 8,
		// Token: 0x0400009D RID: 157
		Numeric = 16,
		// Token: 0x0400009E RID: 158
		CapsLock = 32,
		// Token: 0x0400009F RID: 159
		FunctionKey = 64
	}
}
