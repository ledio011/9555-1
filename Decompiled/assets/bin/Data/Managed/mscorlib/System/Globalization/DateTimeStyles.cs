using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000FA RID: 250
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum DateTimeStyles
	{
		// Token: 0x040003BD RID: 957
		None = 0,
		// Token: 0x040003BE RID: 958
		AllowLeadingWhite = 1,
		// Token: 0x040003BF RID: 959
		AllowTrailingWhite = 2,
		// Token: 0x040003C0 RID: 960
		AllowInnerWhite = 4,
		// Token: 0x040003C1 RID: 961
		AllowWhiteSpaces = 7,
		// Token: 0x040003C2 RID: 962
		NoCurrentDateDefault = 8,
		// Token: 0x040003C3 RID: 963
		AdjustToUniversal = 16,
		// Token: 0x040003C4 RID: 964
		AssumeLocal = 32,
		// Token: 0x040003C5 RID: 965
		AssumeUniversal = 64,
		// Token: 0x040003C6 RID: 966
		RoundtripKind = 128
	}
}
