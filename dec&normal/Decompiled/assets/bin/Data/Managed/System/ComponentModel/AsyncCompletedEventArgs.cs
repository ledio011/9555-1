using System;

namespace System.ComponentModel
{
	// Token: 0x02000007 RID: 7
	public class AsyncCompletedEventArgs : EventArgs
	{
		// Token: 0x0400000C RID: 12
		private Exception _error;

		// Token: 0x0400000D RID: 13
		private bool _cancelled;

		// Token: 0x0400000E RID: 14
		private object _userState;
	}
}
