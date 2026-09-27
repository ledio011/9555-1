using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002FA RID: 762
	internal enum TypeTag : byte
	{
		// Token: 0x04000C49 RID: 3145
		PrimitiveType,
		// Token: 0x04000C4A RID: 3146
		String,
		// Token: 0x04000C4B RID: 3147
		ObjectType,
		// Token: 0x04000C4C RID: 3148
		RuntimeType,
		// Token: 0x04000C4D RID: 3149
		GenericType,
		// Token: 0x04000C4E RID: 3150
		ArrayOfObject,
		// Token: 0x04000C4F RID: 3151
		ArrayOfString,
		// Token: 0x04000C50 RID: 3152
		ArrayOfPrimitiveType
	}
}
