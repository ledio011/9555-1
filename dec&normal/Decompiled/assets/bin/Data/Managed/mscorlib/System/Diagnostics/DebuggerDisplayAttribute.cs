using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020000D5 RID: 213
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x00020E24 File Offset: 0x0001F024
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			this.value = value;
			this.type = string.Empty;
			this.name = string.Empty;
		}

		// Token: 0x1700012E RID: 302
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00020E58 File Offset: 0x0001F058
		public string Name
		{
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040002D7 RID: 727
		private string value;

		// Token: 0x040002D8 RID: 728
		private string type;

		// Token: 0x040002D9 RID: 729
		private string name;

		// Token: 0x040002DA RID: 730
		private string target_type_name;

		// Token: 0x040002DB RID: 731
		private Type target_type;
	}
}
