using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000182 RID: 386
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x00038E10 File Offset: 0x00037010
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.name = version;
		}

		// Token: 0x040005F0 RID: 1520
		private string name;
	}
}
