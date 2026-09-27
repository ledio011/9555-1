using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000180 RID: 384
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		// Token: 0x06000E8F RID: 3727 RVA: 0x00038DF0 File Offset: 0x00036FF0
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.delay = delaySign;
		}

		// Token: 0x040005EE RID: 1518
		private bool delay;
	}
}
