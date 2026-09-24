using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002F7 RID: 759
	internal enum ReturnTypeTag : byte
	{
		// Token: 0x04000C40 RID: 3136
		Null = 2,
		// Token: 0x04000C41 RID: 3137
		PrimitiveType = 8,
		// Token: 0x04000C42 RID: 3138
		ObjectType = 16,
		// Token: 0x04000C43 RID: 3139
		Exception = 32
	}
}
