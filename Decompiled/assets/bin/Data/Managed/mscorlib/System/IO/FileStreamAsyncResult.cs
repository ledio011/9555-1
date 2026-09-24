using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000129 RID: 297
	internal class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
		public FileStreamAsyncResult(AsyncCallback cb, object state)
		{
			this.state = state;
			this.realcb = cb;
			if (this.realcb != null)
			{
				this.cb = new AsyncCallback(FileStreamAsyncResult.CBWrapper);
			}
			this.wh = new ManualResetEvent(false);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002D714 File Offset: 0x0002B914
		private static void CBWrapper(IAsyncResult ares)
		{
			FileStreamAsyncResult fileStreamAsyncResult = (FileStreamAsyncResult)ares;
			fileStreamAsyncResult.realcb.BeginInvoke(ares, null, null);
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002D738 File Offset: 0x0002B938
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.wh;
			}
		}

		// Token: 0x040004B5 RID: 1205
		private object state;

		// Token: 0x040004B6 RID: 1206
		private bool completed;

		// Token: 0x040004B7 RID: 1207
		private bool done;

		// Token: 0x040004B8 RID: 1208
		private Exception exc;

		// Token: 0x040004B9 RID: 1209
		private ManualResetEvent wh;

		// Token: 0x040004BA RID: 1210
		private AsyncCallback cb;

		// Token: 0x040004BB RID: 1211
		private bool completedSynch;

		// Token: 0x040004BC RID: 1212
		public byte[] Buffer;

		// Token: 0x040004BD RID: 1213
		public int Offset;

		// Token: 0x040004BE RID: 1214
		public int Count;

		// Token: 0x040004BF RID: 1215
		public int OriginalCount;

		// Token: 0x040004C0 RID: 1216
		public int BytesRead;

		// Token: 0x040004C1 RID: 1217
		private AsyncCallback realcb;
	}
}
