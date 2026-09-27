using System;

namespace System.Reflection
{
	// Token: 0x020001E8 RID: 488
	[Flags]
	internal enum PInfo
	{
		// Token: 0x04000940 RID: 2368
		Attributes = 1,
		// Token: 0x04000941 RID: 2369
		GetMethod = 2,
		// Token: 0x04000942 RID: 2370
		SetMethod = 4,
		// Token: 0x04000943 RID: 2371
		ReflectedType = 8,
		// Token: 0x04000944 RID: 2372
		DeclaringType = 16,
		// Token: 0x04000945 RID: 2373
		Name = 32
	}
}
