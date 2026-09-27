using System;

namespace System.Reflection.Emit
{
	// Token: 0x020001AE RID: 430
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x0003E788 File Offset: 0x0003C988
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003E798 File Offset: 0x0003C998
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
		public int GetToken(MemberInfo member)
		{
			return this.mb.GetToken(member);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		public int GetToken(MethodInfo method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x0400070D RID: 1805
		private ModuleBuilder mb;
	}
}
