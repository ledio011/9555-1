using System;

namespace System.Net.Sockets
{
	// Token: 0x0200005C RID: 92
	public enum SocketType
	{
		// Token: 0x040008E0 RID: 2272
		Stream = 1,
		// Token: 0x040008E1 RID: 2273
		Dgram,
		// Token: 0x040008E2 RID: 2274
		Raw,
		// Token: 0x040008E3 RID: 2275
		Rdm,
		// Token: 0x040008E4 RID: 2276
		Seqpacket,
		// Token: 0x040008E5 RID: 2277
		Unknown = -1
	}
}
