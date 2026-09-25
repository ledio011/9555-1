using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200032C RID: 812
	[ComVisible(true)]
	public abstract class RSA : AsymmetricAlgorithm
	{
		// Token: 0x0600189C RID: 6300 RVA: 0x0005A41C File Offset: 0x0005861C
		public static RSA Create()
		{
			return RSA.Create("System.Security.Cryptography.RSA");
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0005A428 File Offset: 0x00058628
		public static RSA Create(string algName)
		{
			return (RSA)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x0600189E RID: 6302
		public abstract RSAParameters ExportParameters(bool includePrivateParameters);

		// Token: 0x0600189F RID: 6303
		public abstract void ImportParameters(RSAParameters parameters);
	}
}
