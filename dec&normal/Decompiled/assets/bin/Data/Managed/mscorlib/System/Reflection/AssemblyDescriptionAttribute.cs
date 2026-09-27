using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000181 RID: 385
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00038E00 File Offset: 0x00037000
		public AssemblyDescriptionAttribute(string description)
		{
			this.name = description;
		}

		// Token: 0x040005EF RID: 1519
		private string name;
	}
}
