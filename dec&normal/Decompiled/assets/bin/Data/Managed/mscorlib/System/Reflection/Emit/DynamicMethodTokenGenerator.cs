using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200019B RID: 411
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003C11C File Offset: 0x0003A31C
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003C12C File Offset: 0x0003A32C
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003C13C File Offset: 0x0003A33C
		public int GetToken(MethodInfo method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003C144 File Offset: 0x0003A344
		public int GetToken(MemberInfo member)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x0400067F RID: 1663
		private DynamicMethod m;
	}
}
