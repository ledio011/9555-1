using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000244 RID: 580
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x000455F0 File Offset: 0x000437F0
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this.intType = interfaceType;
		}

		// Token: 0x04000A04 RID: 2564
		private ComInterfaceType intType;
	}
}
