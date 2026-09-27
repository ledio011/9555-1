using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200024E RID: 590
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x00046508 File Offset: 0x00044708
		public TypeLibVersionAttribute(int major, int minor)
		{
			this.major = major;
			this.minor = minor;
		}

		// Token: 0x04000A17 RID: 2583
		private int major;

		// Token: 0x04000A18 RID: 2584
		private int minor;
	}
}
