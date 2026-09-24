using System;

namespace System.ComponentModel
{
	// Token: 0x02000009 RID: 9
	public class BackgroundWorker
	{
		// Token: 0x04000012 RID: 18
		private AsyncOperation async;

		// Token: 0x04000013 RID: 19
		private bool cancel_pending;

		// Token: 0x04000014 RID: 20
		private bool report_progress;

		// Token: 0x04000015 RID: 21
		private bool support_cancel;

		// Token: 0x04000016 RID: 22
		private DoWorkEventHandler DoWork;

		// Token: 0x04000017 RID: 23
		private ProgressChangedEventHandler ProgressChanged;

		// Token: 0x04000018 RID: 24
		private RunWorkerCompletedEventHandler RunWorkerCompleted;
	}
}
