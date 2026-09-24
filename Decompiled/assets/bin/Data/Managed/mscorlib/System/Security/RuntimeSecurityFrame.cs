using System;
using System.Reflection;

namespace System.Security
{
	// Token: 0x0200037A RID: 890
	internal class RuntimeSecurityFrame
	{
		// Token: 0x04000E5E RID: 3678
		public AppDomain domain;

		// Token: 0x04000E5F RID: 3679
		public MethodInfo method;

		// Token: 0x04000E60 RID: 3680
		public RuntimeDeclSecurityEntry assert;

		// Token: 0x04000E61 RID: 3681
		public RuntimeDeclSecurityEntry deny;

		// Token: 0x04000E62 RID: 3682
		public RuntimeDeclSecurityEntry permitonly;
	}
}
