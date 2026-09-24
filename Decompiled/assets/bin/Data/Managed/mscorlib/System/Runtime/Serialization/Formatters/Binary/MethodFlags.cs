using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002F1 RID: 753
	internal enum MethodFlags
	{
		// Token: 0x04000C12 RID: 3090
		NoArguments = 1,
		// Token: 0x04000C13 RID: 3091
		PrimitiveArguments,
		// Token: 0x04000C14 RID: 3092
		ArgumentsInSimpleArray = 4,
		// Token: 0x04000C15 RID: 3093
		ArgumentsInMultiArray = 8,
		// Token: 0x04000C16 RID: 3094
		ExcludeLogicalCallContext = 16,
		// Token: 0x04000C17 RID: 3095
		IncludesLogicalCallContext = 64,
		// Token: 0x04000C18 RID: 3096
		IncludesSignature = 128,
		// Token: 0x04000C19 RID: 3097
		FormatMask = 15,
		// Token: 0x04000C1A RID: 3098
		GenericArguments = 32768,
		// Token: 0x04000C1B RID: 3099
		NeedsInfoArrayMask = 32972
	}
}
