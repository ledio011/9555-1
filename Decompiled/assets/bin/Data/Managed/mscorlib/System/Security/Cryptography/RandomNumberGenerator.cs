using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200032A RID: 810
	public abstract class RandomNumberGenerator
	{
		// Token: 0x06001890 RID: 6288 RVA: 0x0005A2D0 File Offset: 0x000584D0
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0005A2DC File Offset: 0x000584DC
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		// Token: 0x06001892 RID: 6290
		public abstract void GetBytes(byte[] data);
	}
}
