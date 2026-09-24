using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class SocketException : System.ComponentModel.Win32Exception
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00007098 File Offset: 0x00005298
		public SocketException() : base(SocketException.WSAGetLastError_internal())
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000070A8 File Offset: 0x000052A8
		public SocketException(int error) : base(error)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000070B4 File Offset: 0x000052B4
		protected SocketException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000070C0 File Offset: 0x000052C0
		internal SocketException(int error, string message) : base(error, message)
		{
		}

		// Token: 0x06000184 RID: 388
		[MethodImpl(4096)]
		private static extern int WSAGetLastError_internal();

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000070CC File Offset: 0x000052CC
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}
	}
}
