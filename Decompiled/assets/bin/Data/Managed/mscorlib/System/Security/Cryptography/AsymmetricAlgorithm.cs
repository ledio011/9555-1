using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000321 RID: 801
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x00058A84 File Offset: 0x00056C84
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00058A94 File Offset: 0x00056C94
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
		}

		// Token: 0x06001870 RID: 6256
		protected abstract void Dispose(bool disposing);

		// Token: 0x04000CA3 RID: 3235
		protected int KeySizeValue;

		// Token: 0x04000CA4 RID: 3236
		protected KeySizes[] LegalKeySizesValue;
	}
}
