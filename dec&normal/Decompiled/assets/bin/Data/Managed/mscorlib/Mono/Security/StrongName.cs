using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x02000045 RID: 69
	internal sealed class StrongName
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00009F40 File Offset: 0x00008140
		public StrongName(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 16)
			{
				int i = 0;
				int num = 0;
				while (i < data.Length)
				{
					num += (int)data[i++];
				}
				if (num == 4)
				{
					this.publicKey = (byte[])data.Clone();
				}
			}
			else
			{
				this.RSA = CryptoConvert.FromCapiKeyBlob(data);
				if (this.rsa == null)
				{
					throw new ArgumentException("data isn't a correctly encoded RSA public key");
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009FCC File Offset: 0x000081CC
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000A000 File Offset: 0x00008200
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x1700000C RID: 12
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000A010 File Offset: 0x00008210
		public RSA RSA
		{
			set
			{
				this.rsa = value;
				this.InvalidateCache();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000A020 File Offset: 0x00008220
		public byte[] PublicKey
		{
			get
			{
				if (this.publicKey == null)
				{
					byte[] array = CryptoConvert.ToCapiKeyBlob(this.rsa, false);
					this.publicKey = new byte[32 + (this.rsa.KeySize >> 3)];
					this.publicKey[0] = array[4];
					this.publicKey[1] = array[5];
					this.publicKey[2] = array[6];
					this.publicKey[3] = array[7];
					this.publicKey[4] = 4;
					this.publicKey[5] = 128;
					this.publicKey[6] = 0;
					this.publicKey[7] = 0;
					byte[] bytes = BitConverterLE.GetBytes(this.publicKey.Length - 12);
					this.publicKey[8] = bytes[0];
					this.publicKey[9] = bytes[1];
					this.publicKey[10] = bytes[2];
					this.publicKey[11] = bytes[3];
					this.publicKey[12] = 6;
					Buffer.BlockCopy(array, 1, this.publicKey, 13, this.publicKey.Length - 13);
					this.publicKey[23] = 49;
				}
				return (byte[])this.publicKey.Clone();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000A134 File Offset: 0x00008334
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.keyToken == null)
				{
					byte[] array = this.PublicKey;
					if (array == null)
					{
						return null;
					}
					HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.TokenAlgorithm);
					byte[] array2 = hashAlgorithm.ComputeHash(array);
					this.keyToken = new byte[8];
					Buffer.BlockCopy(array2, array2.Length - 8, this.keyToken, 0, 8);
					Array.Reverse(this.keyToken, 0, 8);
				}
				return (byte[])this.keyToken.Clone();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000A1AC File Offset: 0x000083AC
		public string TokenAlgorithm
		{
			get
			{
				if (this.tokenAlgorithm == null)
				{
					this.tokenAlgorithm = "SHA1";
				}
				return this.tokenAlgorithm;
			}
		}

		// Token: 0x04000105 RID: 261
		private RSA rsa;

		// Token: 0x04000106 RID: 262
		private byte[] publicKey;

		// Token: 0x04000107 RID: 263
		private byte[] keyToken;

		// Token: 0x04000108 RID: 264
		private string tokenAlgorithm;

		// Token: 0x04000109 RID: 265
		private static object lockObject = new object();

		// Token: 0x0400010A RID: 266
		private static bool initialized = false;
	}
}
