using System;

namespace System.Net.Sockets
{
	// Token: 0x02000051 RID: 81
	public class LingerOption
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000625C File Offset: 0x0000445C
		public LingerOption(bool enable, int secs)
		{
			this.enabled = enable;
			this.seconds = secs;
		}

		// Token: 0x04000834 RID: 2100
		private bool enabled;

		// Token: 0x04000835 RID: 2101
		private int seconds;
	}
}
