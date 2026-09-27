using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000232 RID: 562
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		// Token: 0x0600132E RID: 4910 RVA: 0x00045398 File Offset: 0x00043598
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._type = defaultInterface;
		}

		// Token: 0x040009E6 RID: 2534
		private Type _type;
	}
}
