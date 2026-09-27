using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003C1 RID: 961
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ThreadState
	{
		// Token: 0x04000F61 RID: 3937
		Running = 0,
		// Token: 0x04000F62 RID: 3938
		StopRequested = 1,
		// Token: 0x04000F63 RID: 3939
		SuspendRequested = 2,
		// Token: 0x04000F64 RID: 3940
		Background = 4,
		// Token: 0x04000F65 RID: 3941
		Unstarted = 8,
		// Token: 0x04000F66 RID: 3942
		Stopped = 16,
		// Token: 0x04000F67 RID: 3943
		WaitSleepJoin = 32,
		// Token: 0x04000F68 RID: 3944
		Suspended = 64,
		// Token: 0x04000F69 RID: 3945
		AbortRequested = 128,
		// Token: 0x04000F6A RID: 3946
		Aborted = 256
	}
}
