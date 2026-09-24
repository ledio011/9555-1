using System;

namespace System.Net.Sockets
{
	// Token: 0x02000058 RID: 88
	[Flags]
	public enum SocketFlags
	{
		// Token: 0x0400089F RID: 2207
		None = 0,
		// Token: 0x040008A0 RID: 2208
		OutOfBand = 1,
		// Token: 0x040008A1 RID: 2209
		Peek = 2,
		// Token: 0x040008A2 RID: 2210
		DontRoute = 4,
		// Token: 0x040008A3 RID: 2211
		MaxIOVectorLength = 16,
		// Token: 0x040008A4 RID: 2212
		Truncated = 256,
		// Token: 0x040008A5 RID: 2213
		ControlDataTruncated = 512,
		// Token: 0x040008A6 RID: 2214
		Broadcast = 1024,
		// Token: 0x040008A7 RID: 2215
		Multicast = 2048,
		// Token: 0x040008A8 RID: 2216
		Partial = 32768
	}
}
