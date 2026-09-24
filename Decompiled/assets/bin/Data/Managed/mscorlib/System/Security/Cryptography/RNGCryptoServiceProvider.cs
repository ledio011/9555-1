using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200032B RID: 811
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x0005A2EC File Offset: 0x000584EC
		public RNGCryptoServiceProvider()
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			this.Check();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0005A308 File Offset: 0x00058508
		static RNGCryptoServiceProvider()
		{
			if (RNGCryptoServiceProvider.RngOpen())
			{
				RNGCryptoServiceProvider._lock = new object();
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0005A320 File Offset: 0x00058520
		private void Check()
		{
			if (this._handle == IntPtr.Zero)
			{
				throw new CryptographicException(Locale.GetText("Couldn't access random source."));
			}
		}

		// Token: 0x06001896 RID: 6294
		[MethodImpl(4096)]
		private static extern bool RngOpen();

		// Token: 0x06001897 RID: 6295
		[MethodImpl(4096)]
		private static extern IntPtr RngInitialize(byte[] seed);

		// Token: 0x06001898 RID: 6296
		[MethodImpl(4096)]
		private static extern IntPtr RngGetBytes(IntPtr handle, byte[] data);

		// Token: 0x06001899 RID: 6297
		[MethodImpl(4096)]
		private static extern void RngClose(IntPtr handle);

		// Token: 0x0600189A RID: 6298 RVA: 0x0005A348 File Offset: 0x00058548
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (RNGCryptoServiceProvider._lock == null)
			{
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data);
			}
			else
			{
				object @lock = RNGCryptoServiceProvider._lock;
				lock (@lock)
				{
					this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data);
				}
			}
			this.Check();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0005A3C8 File Offset: 0x000585C8
		~RNGCryptoServiceProvider()
		{
			if (this._handle != IntPtr.Zero)
			{
				RNGCryptoServiceProvider.RngClose(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x04000D41 RID: 3393
		private static object _lock;

		// Token: 0x04000D42 RID: 3394
		private IntPtr _handle;
	}
}
