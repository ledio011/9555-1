using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002EB RID: 747
	internal enum BinaryElement : byte
	{
		// Token: 0x04000BEF RID: 3055
		Header,
		// Token: 0x04000BF0 RID: 3056
		RefTypeObject,
		// Token: 0x04000BF1 RID: 3057
		UntypedRuntimeObject,
		// Token: 0x04000BF2 RID: 3058
		UntypedExternalObject,
		// Token: 0x04000BF3 RID: 3059
		RuntimeObject,
		// Token: 0x04000BF4 RID: 3060
		ExternalObject,
		// Token: 0x04000BF5 RID: 3061
		String,
		// Token: 0x04000BF6 RID: 3062
		GenericArray,
		// Token: 0x04000BF7 RID: 3063
		BoxedPrimitiveTypeValue,
		// Token: 0x04000BF8 RID: 3064
		ObjectReference,
		// Token: 0x04000BF9 RID: 3065
		NullValue,
		// Token: 0x04000BFA RID: 3066
		End,
		// Token: 0x04000BFB RID: 3067
		Assembly,
		// Token: 0x04000BFC RID: 3068
		ArrayFiller8b,
		// Token: 0x04000BFD RID: 3069
		ArrayFiller32b,
		// Token: 0x04000BFE RID: 3070
		ArrayOfPrimitiveType,
		// Token: 0x04000BFF RID: 3071
		ArrayOfObject,
		// Token: 0x04000C00 RID: 3072
		ArrayOfString,
		// Token: 0x04000C01 RID: 3073
		Method,
		// Token: 0x04000C02 RID: 3074
		_Unknown4,
		// Token: 0x04000C03 RID: 3075
		_Unknown5,
		// Token: 0x04000C04 RID: 3076
		MethodCall,
		// Token: 0x04000C05 RID: 3077
		MethodResponse
	}
}
