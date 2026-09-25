using System;

namespace System.Reflection.Emit
{
	// Token: 0x020001A3 RID: 419
	internal struct ILExceptionInfo
	{
		// Token: 0x040006B0 RID: 1712
		private ILExceptionBlock[] handlers;

		// Token: 0x040006B1 RID: 1713
		internal int start;

		// Token: 0x040006B2 RID: 1714
		private int len;

		// Token: 0x040006B3 RID: 1715
		internal Label end;
	}
}
