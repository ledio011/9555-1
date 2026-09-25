using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal class AndroidJavaRunnableProxy : AndroidJavaProxy
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004D04 File Offset: 0x00002F04
		public AndroidJavaRunnableProxy(AndroidJavaRunnable runnable) : base("java/lang/Runnable")
		{
			this.mRunnable = runnable;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004D18 File Offset: 0x00002F18
		public void run()
		{
			this.mRunnable();
		}

		// Token: 0x04000009 RID: 9
		private AndroidJavaRunnable mRunnable;
	}
}
