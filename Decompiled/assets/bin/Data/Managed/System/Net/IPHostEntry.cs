using System;

namespace System.Net
{
	// Token: 0x02000044 RID: 68
	public class IPHostEntry
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004DDC File Offset: 0x00002FDC
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public string[] Aliases
		{
			set
			{
				this.aliases = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00004DFC File Offset: 0x00002FFC
		public string HostName
		{
			set
			{
				this.hostName = value;
			}
		}

		// Token: 0x040007DA RID: 2010
		private IPAddress[] addressList;

		// Token: 0x040007DB RID: 2011
		private string[] aliases;

		// Token: 0x040007DC RID: 2012
		private string hostName;
	}
}
