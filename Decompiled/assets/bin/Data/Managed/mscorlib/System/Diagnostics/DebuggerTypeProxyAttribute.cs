using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020000D8 RID: 216
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00020E74 File Offset: 0x0001F074
		public DebuggerTypeProxyAttribute(Type type)
		{
			this.proxy_type_name = type.Name;
		}

		// Token: 0x040002DC RID: 732
		private string proxy_type_name;

		// Token: 0x040002DD RID: 733
		private string target_type_name;

		// Token: 0x040002DE RID: 734
		private Type target_type;
	}
}
