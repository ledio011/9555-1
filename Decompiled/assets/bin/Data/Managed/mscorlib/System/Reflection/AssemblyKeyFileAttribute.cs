using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000184 RID: 388
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x00038E40 File Offset: 0x00037040
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.name = keyFile;
		}

		// Token: 0x040005F2 RID: 1522
		private string name;
	}
}
