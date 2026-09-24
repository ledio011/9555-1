using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000247 RID: 583
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x000462A4 File Offset: 0x000444A4
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.utype = unmanagedType;
		}

		// Token: 0x04000A08 RID: 2568
		private UnmanagedType utype;

		// Token: 0x04000A09 RID: 2569
		public UnmanagedType ArraySubType;

		// Token: 0x04000A0A RID: 2570
		public string MarshalCookie;

		// Token: 0x04000A0B RID: 2571
		[ComVisible(true)]
		public string MarshalType;

		// Token: 0x04000A0C RID: 2572
		[ComVisible(true)]
		public Type MarshalTypeRef;

		// Token: 0x04000A0D RID: 2573
		public VarEnum SafeArraySubType;

		// Token: 0x04000A0E RID: 2574
		public int SizeConst;

		// Token: 0x04000A0F RID: 2575
		public short SizeParamIndex;

		// Token: 0x04000A10 RID: 2576
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04000A11 RID: 2577
		public int IidParameterIndex;
	}
}
