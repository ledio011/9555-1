using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200004E RID: 78
	public class PingReply
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00006068 File Offset: 0x00004268
		internal PingReply(IPAddress address, byte[] buffer, PingOptions options, long roundtripTime, IPStatus status)
		{
			this.address = address;
			this.buffer = buffer;
			this.options = options;
			this.rtt = roundtripTime;
			this.status = status;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006098 File Offset: 0x00004298
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000060A0 File Offset: 0x000042A0
		public IPStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x0400080E RID: 2062
		private IPAddress address;

		// Token: 0x0400080F RID: 2063
		private byte[] buffer;

		// Token: 0x04000810 RID: 2064
		private PingOptions options;

		// Token: 0x04000811 RID: 2065
		private long rtt;

		// Token: 0x04000812 RID: 2066
		private IPStatus status;
	}
}
