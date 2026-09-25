using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000188 RID: 392
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x000395DC File Offset: 0x000377DC
		public AssemblyProductAttribute(string product)
		{
			this.name = product;
		}

		// Token: 0x04000609 RID: 1545
		private string name;
	}
}
