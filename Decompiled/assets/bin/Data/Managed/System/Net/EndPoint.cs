using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public abstract class EndPoint
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000041C8 File Offset: 0x000023C8
		public virtual System.Net.Sockets.AddressFamily AddressFamily
		{
			get
			{
				throw EndPoint.NotImplemented();
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000041D0 File Offset: 0x000023D0
		public virtual EndPoint Create(SocketAddress address)
		{
			throw EndPoint.NotImplemented();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000041D8 File Offset: 0x000023D8
		public virtual SocketAddress Serialize()
		{
			throw EndPoint.NotImplemented();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000041E0 File Offset: 0x000023E0
		private static Exception NotImplemented()
		{
			return new NotImplementedException();
		}
	}
}
