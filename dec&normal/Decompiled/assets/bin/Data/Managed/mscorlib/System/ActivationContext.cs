using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000052 RID: 82
	[ComVisible(false)]
	[Serializable]
	public sealed class ActivationContext : IDisposable, ISerializable
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000CA40 File Offset: 0x0000AC40
		[MonoTODO("Missing serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CA54 File Offset: 0x0000AC54
		~ActivationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public ApplicationIdentity Identity
		{
			get
			{
				return this._appid;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				if (disposing)
				{
				}
				this._disposed = true;
			}
		}

		// Token: 0x04000147 RID: 327
		private ActivationContext.ContextForm _form;

		// Token: 0x04000148 RID: 328
		private ApplicationIdentity _appid;

		// Token: 0x04000149 RID: 329
		private bool _disposed;

		// Token: 0x02000053 RID: 83
		public enum ContextForm
		{
			// Token: 0x0400014B RID: 331
			Loose,
			// Token: 0x0400014C RID: 332
			StoreBounded
		}
	}
}
