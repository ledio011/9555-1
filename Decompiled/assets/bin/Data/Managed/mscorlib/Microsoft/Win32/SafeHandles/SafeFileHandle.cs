using System;
using System.IO;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000029 RID: 41
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003270 File Offset: 0x00001470
		protected override bool ReleaseHandle()
		{
			MonoIOError monoIOError;
			MonoIO.Close(this.handle, out monoIOError);
			return monoIOError == MonoIOError.ERROR_SUCCESS;
		}
	}
}
