using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000325 RID: 805
	[ComVisible(true)]
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x0005925C File Offset: 0x0005745C
		protected HashAlgorithm()
		{
			this.disposed = false;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0005926C File Offset: 0x0005746C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0005927C File Offset: 0x0005747C
		public byte[] ComputeHash(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.ComputeHash(buffer, 0, buffer.Length);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0005929C File Offset: 0x0005749C
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("HashAlgorithm");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentException("count", "< 0");
			}
			if (offset > buffer.Length - count)
			{
				throw new ArgumentException("offset + count", Locale.GetText("Overflow"));
			}
			this.HashCore(buffer, offset, count);
			this.HashValue = this.HashFinal();
			this.Initialize();
			return this.HashValue;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00059340 File Offset: 0x00057540
		public byte[] ComputeHash(Stream inputStream)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("HashAlgorithm");
			}
			byte[] array = new byte[4096];
			for (int i = inputStream.Read(array, 0, 4096); i > 0; i = inputStream.Read(array, 0, 4096))
			{
				this.HashCore(array, 0, i);
			}
			this.HashValue = this.HashFinal();
			this.Initialize();
			return this.HashValue;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000593B8 File Offset: 0x000575B8
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
		}

		// Token: 0x06001880 RID: 6272
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		// Token: 0x06001881 RID: 6273
		protected abstract byte[] HashFinal();

		// Token: 0x06001882 RID: 6274
		public abstract void Initialize();

		// Token: 0x06001883 RID: 6275 RVA: 0x000593C8 File Offset: 0x000575C8
		protected virtual void Dispose(bool disposing)
		{
			this.disposed = true;
		}

		// Token: 0x04000D32 RID: 3378
		protected internal byte[] HashValue;

		// Token: 0x04000D33 RID: 3379
		protected int HashSizeValue;

		// Token: 0x04000D34 RID: 3380
		protected int State;

		// Token: 0x04000D35 RID: 3381
		private bool disposed;
	}
}
