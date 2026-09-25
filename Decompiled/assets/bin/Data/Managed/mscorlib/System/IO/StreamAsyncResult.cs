using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200013A RID: 314
	internal class StreamAsyncResult : IAsyncResult
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		public StreamAsyncResult(object state)
		{
			this.state = state;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
		public void SetComplete(Exception e)
		{
			this.exc = e;
			this.completed = true;
			lock (this)
			{
				if (this.wh != null)
				{
					this.wh.Set();
				}
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002F620 File Offset: 0x0002D820
		public void SetComplete(Exception e, int nbytes)
		{
			this.nbytes = nbytes;
			this.SetComplete(e);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002F630 File Offset: 0x0002D830
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle result;
				lock (this)
				{
					if (this.wh == null)
					{
						this.wh = new ManualResetEvent(this.completed);
					}
					result = this.wh;
				}
				return result;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002F68C File Offset: 0x0002D88C
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002F694 File Offset: 0x0002D894
		public int NBytes
		{
			get
			{
				return this.nbytes;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002F69C File Offset: 0x0002D89C
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002F6A4 File Offset: 0x0002D8A4
		public bool Done
		{
			get
			{
				return this.done;
			}
			set
			{
				this.done = value;
			}
		}

		// Token: 0x04000511 RID: 1297
		private object state;

		// Token: 0x04000512 RID: 1298
		private bool completed;

		// Token: 0x04000513 RID: 1299
		private bool done;

		// Token: 0x04000514 RID: 1300
		private Exception exc;

		// Token: 0x04000515 RID: 1301
		private int nbytes = -1;

		// Token: 0x04000516 RID: 1302
		private ManualResetEvent wh;
	}
}
