using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200017F RID: 383
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x00038DE0 File Offset: 0x00036FE0
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.name = defaultAlias;
		}

		// Token: 0x040005ED RID: 1517
		private string name;
	}
}
