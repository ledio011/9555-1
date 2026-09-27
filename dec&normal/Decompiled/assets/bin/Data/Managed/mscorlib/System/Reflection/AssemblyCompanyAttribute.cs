using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200017C RID: 380
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x00038DB0 File Offset: 0x00036FB0
		public AssemblyCompanyAttribute(string company)
		{
			this.name = company;
		}

		// Token: 0x040005EA RID: 1514
		private string name;
	}
}
