using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000202 RID: 514
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DecimalConstantAttribute : Attribute
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x0004525C File Offset: 0x0004345C
		[CLSCompliant(false)]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this.scale = scale;
			this.sign = Convert.ToBoolean(sign);
			this.hi = (int)hi;
			this.mid = (int)mid;
			this.low = (int)low;
		}

		// Token: 0x040009BA RID: 2490
		private byte scale;

		// Token: 0x040009BB RID: 2491
		private bool sign;

		// Token: 0x040009BC RID: 2492
		private int hi;

		// Token: 0x040009BD RID: 2493
		private int mid;

		// Token: 0x040009BE RID: 2494
		private int low;
	}
}
