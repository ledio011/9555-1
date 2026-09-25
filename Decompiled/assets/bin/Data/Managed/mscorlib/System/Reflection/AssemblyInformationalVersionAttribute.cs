using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000183 RID: 387
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00038E30 File Offset: 0x00037030
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.name = informationalVersion;
		}

		// Token: 0x040005F1 RID: 1521
		private string name;
	}
}
