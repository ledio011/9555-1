using System;
using System.Runtime.ConstrainedExecution;

namespace System.Security
{
	// Token: 0x0200037B RID: 891
	[MonoTODO("work in progress - encryption is missing")]
	public sealed class SecureString : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00060F24 File Offset: 0x0005F124
		public int Length
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SecureString");
				}
				return this.length;
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00060F44 File Offset: 0x0005F144
		public void Dispose()
		{
			this.disposed = true;
			if (this.data != null)
			{
				Array.Clear(this.data, 0, this.data.Length);
				this.data = null;
			}
			this.length = 0;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00060F7C File Offset: 0x0005F17C
		private void Encrypt()
		{
			if (this.data == null || this.data.Length > 0)
			{
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00060F98 File Offset: 0x0005F198
		private void Decrypt()
		{
			if (this.data == null || this.data.Length > 0)
			{
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00060FB4 File Offset: 0x0005F1B4
		internal byte[] GetBuffer()
		{
			byte[] array = new byte[this.length << 1];
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, 0, array, 0, array.Length);
			}
			finally
			{
				this.Encrypt();
			}
			return array;
		}

		// Token: 0x04000E63 RID: 3683
		private const int BlockSize = 16;

		// Token: 0x04000E64 RID: 3684
		private const int MaxSize = 65536;

		// Token: 0x04000E65 RID: 3685
		private int length;

		// Token: 0x04000E66 RID: 3686
		private bool disposed;

		// Token: 0x04000E67 RID: 3687
		private bool read_only;

		// Token: 0x04000E68 RID: 3688
		private byte[] data;
	}
}
