using System;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x02000008 RID: 8
	public sealed class AsyncOperation
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002410 File Offset: 0x00000610
		~AsyncOperation()
		{
			if (!this.done && this.ctx != null)
			{
				this.ctx.OperationCompleted();
			}
		}

		// Token: 0x0400000F RID: 15
		private SynchronizationContext ctx;

		// Token: 0x04000010 RID: 16
		private object state;

		// Token: 0x04000011 RID: 17
		private bool done;
	}
}
