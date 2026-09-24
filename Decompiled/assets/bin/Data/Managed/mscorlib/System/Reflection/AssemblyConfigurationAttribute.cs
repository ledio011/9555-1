using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200017D RID: 381
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00038DC0 File Offset: 0x00036FC0
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.name = configuration;
		}

		// Token: 0x040005EB RID: 1515
		private string name;
	}
}
