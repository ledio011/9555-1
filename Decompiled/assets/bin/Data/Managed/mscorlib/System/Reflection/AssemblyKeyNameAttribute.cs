using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000185 RID: 389
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x00038E50 File Offset: 0x00037050
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.name = keyName;
		}

		// Token: 0x040005F3 RID: 1523
		private string name;
	}
}
