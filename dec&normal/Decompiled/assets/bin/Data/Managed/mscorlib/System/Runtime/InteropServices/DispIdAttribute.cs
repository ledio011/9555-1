using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200023B RID: 571
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	public sealed class DispIdAttribute : Attribute
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x0004547C File Offset: 0x0004367C
		public DispIdAttribute(int dispId)
		{
			this.id = dispId;
		}

		// Token: 0x040009F1 RID: 2545
		private int id;
	}
}
