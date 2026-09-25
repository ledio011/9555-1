using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000230 RID: 560
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x00045388 File Offset: 0x00043588
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this.ciType = classInterfaceType;
		}

		// Token: 0x040009E1 RID: 2529
		private ClassInterfaceType ciType;
	}
}
