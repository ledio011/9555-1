using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200004D RID: 77
	public class PingOptions
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006058 File Offset: 0x00004258
		public bool DontFragment
		{
			get
			{
				return this.dont_fragment;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006060 File Offset: 0x00004260
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
		}

		// Token: 0x0400080C RID: 2060
		private int ttl = 128;

		// Token: 0x0400080D RID: 2061
		private bool dont_fragment;
	}
}
