using System;

namespace System.Reflection.Emit
{
	// Token: 0x020001C0 RID: 448
	internal interface TokenGenerator
	{
		// Token: 0x060010B3 RID: 4275
		int GetToken(string str);

		// Token: 0x060010B4 RID: 4276
		int GetToken(MemberInfo member);

		// Token: 0x060010B5 RID: 4277
		int GetToken(MethodInfo method, Type[] opt_param_types);
	}
}
