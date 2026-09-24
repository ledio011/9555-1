using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000252 RID: 594
	[ComVisible(true)]
	[Serializable]
	public enum UnmanagedType
	{
		// Token: 0x04000A1B RID: 2587
		Bool = 2,
		// Token: 0x04000A1C RID: 2588
		I1,
		// Token: 0x04000A1D RID: 2589
		U1,
		// Token: 0x04000A1E RID: 2590
		I2,
		// Token: 0x04000A1F RID: 2591
		U2,
		// Token: 0x04000A20 RID: 2592
		I4,
		// Token: 0x04000A21 RID: 2593
		U4,
		// Token: 0x04000A22 RID: 2594
		I8,
		// Token: 0x04000A23 RID: 2595
		U8,
		// Token: 0x04000A24 RID: 2596
		R4,
		// Token: 0x04000A25 RID: 2597
		R8,
		// Token: 0x04000A26 RID: 2598
		Currency = 15,
		// Token: 0x04000A27 RID: 2599
		BStr = 19,
		// Token: 0x04000A28 RID: 2600
		LPStr,
		// Token: 0x04000A29 RID: 2601
		LPWStr,
		// Token: 0x04000A2A RID: 2602
		LPTStr,
		// Token: 0x04000A2B RID: 2603
		ByValTStr,
		// Token: 0x04000A2C RID: 2604
		IUnknown = 25,
		// Token: 0x04000A2D RID: 2605
		IDispatch,
		// Token: 0x04000A2E RID: 2606
		Struct,
		// Token: 0x04000A2F RID: 2607
		Interface,
		// Token: 0x04000A30 RID: 2608
		SafeArray,
		// Token: 0x04000A31 RID: 2609
		ByValArray,
		// Token: 0x04000A32 RID: 2610
		SysInt,
		// Token: 0x04000A33 RID: 2611
		SysUInt,
		// Token: 0x04000A34 RID: 2612
		VBByRefStr = 34,
		// Token: 0x04000A35 RID: 2613
		AnsiBStr,
		// Token: 0x04000A36 RID: 2614
		TBStr,
		// Token: 0x04000A37 RID: 2615
		VariantBool,
		// Token: 0x04000A38 RID: 2616
		FunctionPtr,
		// Token: 0x04000A39 RID: 2617
		AsAny = 40,
		// Token: 0x04000A3A RID: 2618
		LPArray = 42,
		// Token: 0x04000A3B RID: 2619
		LPStruct,
		// Token: 0x04000A3C RID: 2620
		CustomMarshaler,
		// Token: 0x04000A3D RID: 2621
		Error
	}
}
