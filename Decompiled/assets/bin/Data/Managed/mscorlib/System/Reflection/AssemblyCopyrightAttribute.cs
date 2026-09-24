using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200017E RID: 382
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x00038DD0 File Offset: 0x00036FD0
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.name = copyright;
		}

		// Token: 0x040005EC RID: 1516
		private string name;
	}
}
