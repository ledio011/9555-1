using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x0200003A RID: 58
	internal sealed class MonoIO
	{
		// Token: 0x060000CB RID: 203
		[MethodImpl(4096)]
		public static extern bool Close(IntPtr handle, out MonoIOError error);

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CC RID: 204
		public static extern IntPtr ConsoleOutput { [MethodImpl(4096)] get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CD RID: 205
		public static extern IntPtr ConsoleInput { [MethodImpl(4096)] get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CE RID: 206
		public static extern IntPtr ConsoleError { [MethodImpl(4096)] get; }

		// Token: 0x060000CF RID: 207
		[MethodImpl(4096)]
		public static extern bool CreatePipe(out IntPtr read_handle, out IntPtr write_handle);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(4096)]
		public static extern bool DuplicateHandle(IntPtr source_process_handle, IntPtr source_handle, IntPtr target_process_handle, out IntPtr target_handle, int access, int inherit, int options);
	}
}
