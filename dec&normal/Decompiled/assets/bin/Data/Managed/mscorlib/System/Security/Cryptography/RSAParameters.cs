using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200032D RID: 813
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		// Token: 0x04000D43 RID: 3395
		[NonSerialized]
		public byte[] P;

		// Token: 0x04000D44 RID: 3396
		[NonSerialized]
		public byte[] Q;

		// Token: 0x04000D45 RID: 3397
		[NonSerialized]
		public byte[] D;

		// Token: 0x04000D46 RID: 3398
		[NonSerialized]
		public byte[] DP;

		// Token: 0x04000D47 RID: 3399
		[NonSerialized]
		public byte[] DQ;

		// Token: 0x04000D48 RID: 3400
		[NonSerialized]
		public byte[] InverseQ;

		// Token: 0x04000D49 RID: 3401
		public byte[] Modulus;

		// Token: 0x04000D4A RID: 3402
		public byte[] Exponent;
	}
}
