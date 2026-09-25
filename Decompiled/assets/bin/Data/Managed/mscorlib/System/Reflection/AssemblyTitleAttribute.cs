using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000189 RID: 393
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x000395EC File Offset: 0x000377EC
		public AssemblyTitleAttribute(string title)
		{
			this.name = title;
		}

		// Token: 0x0400060A RID: 1546
		private string name;
	}
}
