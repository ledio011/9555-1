using System;

namespace System.Reflection
{
	// Token: 0x020001C8 RID: 456
	[Flags]
	public enum GenericParameterAttributes
	{
		// Token: 0x040008AA RID: 2218
		Covariant = 1,
		// Token: 0x040008AB RID: 2219
		Contravariant = 2,
		// Token: 0x040008AC RID: 2220
		VarianceMask = 3,
		// Token: 0x040008AD RID: 2221
		None = 0,
		// Token: 0x040008AE RID: 2222
		ReferenceTypeConstraint = 4,
		// Token: 0x040008AF RID: 2223
		NotNullableValueTypeConstraint = 8,
		// Token: 0x040008B0 RID: 2224
		DefaultConstructorConstraint = 16,
		// Token: 0x040008B1 RID: 2225
		SpecialConstraintMask = 28
	}
}
