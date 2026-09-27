using System;

namespace System.Threading
{
	// Token: 0x020003BA RID: 954
	public class SynchronizationContext
	{
		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006DB3C File Offset: 0x0006BD3C
		public virtual void OperationCompleted()
		{
		}

		// Token: 0x04000F24 RID: 3876
		private bool notification_required;

		// Token: 0x04000F25 RID: 3877
		[ThreadStatic]
		private static SynchronizationContext currentContext;
	}
}
