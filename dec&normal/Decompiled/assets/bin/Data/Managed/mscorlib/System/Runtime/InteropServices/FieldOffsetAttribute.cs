using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200023F RID: 575
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x000454FC File Offset: 0x000436FC
		public FieldOffsetAttribute(int offset)
		{
			this.val = offset;
		}

		// Token: 0x040009FC RID: 2556
		private int val;
	}
}
