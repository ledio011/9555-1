using System;

namespace System.Net.Sockets
{
	// Token: 0x0200005A RID: 90
	public enum SocketOptionName
	{
		// Token: 0x040008B0 RID: 2224
		Debug = 1,
		// Token: 0x040008B1 RID: 2225
		AcceptConnection,
		// Token: 0x040008B2 RID: 2226
		ReuseAddress = 4,
		// Token: 0x040008B3 RID: 2227
		KeepAlive = 8,
		// Token: 0x040008B4 RID: 2228
		DontRoute = 16,
		// Token: 0x040008B5 RID: 2229
		Broadcast = 32,
		// Token: 0x040008B6 RID: 2230
		UseLoopback = 64,
		// Token: 0x040008B7 RID: 2231
		Linger = 128,
		// Token: 0x040008B8 RID: 2232
		OutOfBandInline = 256,
		// Token: 0x040008B9 RID: 2233
		DontLinger = -129,
		// Token: 0x040008BA RID: 2234
		ExclusiveAddressUse = -5,
		// Token: 0x040008BB RID: 2235
		SendBuffer = 4097,
		// Token: 0x040008BC RID: 2236
		ReceiveBuffer,
		// Token: 0x040008BD RID: 2237
		SendLowWater,
		// Token: 0x040008BE RID: 2238
		ReceiveLowWater,
		// Token: 0x040008BF RID: 2239
		SendTimeout,
		// Token: 0x040008C0 RID: 2240
		ReceiveTimeout,
		// Token: 0x040008C1 RID: 2241
		Error,
		// Token: 0x040008C2 RID: 2242
		Type,
		// Token: 0x040008C3 RID: 2243
		MaxConnections = 2147483647,
		// Token: 0x040008C4 RID: 2244
		IPOptions = 1,
		// Token: 0x040008C5 RID: 2245
		HeaderIncluded,
		// Token: 0x040008C6 RID: 2246
		TypeOfService,
		// Token: 0x040008C7 RID: 2247
		IpTimeToLive,
		// Token: 0x040008C8 RID: 2248
		MulticastInterface = 9,
		// Token: 0x040008C9 RID: 2249
		MulticastTimeToLive,
		// Token: 0x040008CA RID: 2250
		MulticastLoopback,
		// Token: 0x040008CB RID: 2251
		AddMembership,
		// Token: 0x040008CC RID: 2252
		DropMembership,
		// Token: 0x040008CD RID: 2253
		DontFragment,
		// Token: 0x040008CE RID: 2254
		AddSourceMembership,
		// Token: 0x040008CF RID: 2255
		DropSourceMembership,
		// Token: 0x040008D0 RID: 2256
		BlockSource,
		// Token: 0x040008D1 RID: 2257
		UnblockSource,
		// Token: 0x040008D2 RID: 2258
		PacketInformation,
		// Token: 0x040008D3 RID: 2259
		NoDelay = 1,
		// Token: 0x040008D4 RID: 2260
		BsdUrgent,
		// Token: 0x040008D5 RID: 2261
		Expedited = 2,
		// Token: 0x040008D6 RID: 2262
		NoChecksum = 1,
		// Token: 0x040008D7 RID: 2263
		ChecksumCoverage = 20,
		// Token: 0x040008D8 RID: 2264
		HopLimit,
		// Token: 0x040008D9 RID: 2265
		UpdateAcceptContext = 28683,
		// Token: 0x040008DA RID: 2266
		UpdateConnectContext = 28688
	}
}
