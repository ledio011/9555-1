using System;

namespace System.Reflection.Emit
{
	// Token: 0x020001A2 RID: 418
	internal struct ILExceptionBlock
	{
		// Token: 0x040006A6 RID: 1702
		public const int CATCH = 0;

		// Token: 0x040006A7 RID: 1703
		public const int FILTER = 1;

		// Token: 0x040006A8 RID: 1704
		public const int FINALLY = 2;

		// Token: 0x040006A9 RID: 1705
		public const int FAULT = 4;

		// Token: 0x040006AA RID: 1706
		public const int FILTER_START = -1;

		// Token: 0x040006AB RID: 1707
		internal Type extype;

		// Token: 0x040006AC RID: 1708
		internal int type;

		// Token: 0x040006AD RID: 1709
		internal int start;

		// Token: 0x040006AE RID: 1710
		internal int len;

		// Token: 0x040006AF RID: 1711
		internal int filter_offset;
	}
}
