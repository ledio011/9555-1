using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.ComponentModel
{
	// Token: 0x02000029 RID: 41
	[SuppressUnmanagedCodeSecurity]
	[Serializable]
	public class Win32Exception : ExternalException
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002C44 File Offset: 0x00000E44
		public Win32Exception() : base(Win32Exception.W32ErrorMessage(Marshal.GetLastWin32Error()))
		{
			this.native_error_code = Marshal.GetLastWin32Error();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002C64 File Offset: 0x00000E64
		public Win32Exception(int error) : base(Win32Exception.W32ErrorMessage(error))
		{
			this.native_error_code = error;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002C7C File Offset: 0x00000E7C
		public Win32Exception(int error, string message) : base(message)
		{
			this.native_error_code = error;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002C8C File Offset: 0x00000E8C
		protected Win32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.native_error_code = info.GetInt32("NativeErrorCode");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("NativeErrorCode", this.native_error_code);
			base.GetObjectData(info, context);
		}

		// Token: 0x06000081 RID: 129
		[MethodImpl(4096)]
		internal static extern string W32ErrorMessage(int error_code);

		// Token: 0x04000053 RID: 83
		private int native_error_code;
	}
}
