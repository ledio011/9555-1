using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x0200034F RID: 847
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x0005CE04 File Offset: 0x0005B004
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			this.pubkey = publicKey;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0005CE24 File Offset: 0x0005B024
		internal static StrongNamePublicKeyBlob FromString(string s)
		{
			if (s == null || s.Length == 0)
			{
				return null;
			}
			int num = s.Length / 2;
			byte[] array = new byte[num];
			int i = 0;
			int num2 = 0;
			while (i < s.Length)
			{
				byte b = StrongNamePublicKeyBlob.CharToByte(s[i]);
				byte b2 = StrongNamePublicKeyBlob.CharToByte(s[i + 1]);
				array[num2] = Convert.ToByte((int)(b * 16 + b2));
				i += 2;
				num2++;
			}
			return new StrongNamePublicKeyBlob(array);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0005CEA8 File Offset: 0x0005B0A8
		private static byte CharToByte(char c)
		{
			char c2 = char.ToLowerInvariant(c);
			if (char.IsDigit(c2))
			{
				return (byte)(c2 - '0');
			}
			return (byte)(c2 - 'a' + '\n');
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0005CED8 File Offset: 0x0005B0D8
		public override bool Equals(object obj)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = obj as StrongNamePublicKeyBlob;
			if (strongNamePublicKeyBlob == null)
			{
				return false;
			}
			bool flag = this.pubkey.Length == strongNamePublicKeyBlob.pubkey.Length;
			if (flag)
			{
				for (int i = 0; i < this.pubkey.Length; i++)
				{
					if (this.pubkey[i] != strongNamePublicKeyBlob.pubkey[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0005CF40 File Offset: 0x0005B140
		public override int GetHashCode()
		{
			int num = 0;
			int i = 0;
			int num2 = Math.Min(this.pubkey.Length, 4);
			while (i < num2)
			{
				num = (num << 8) + (int)this.pubkey[i++];
			}
			return num;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0005CF80 File Offset: 0x0005B180
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.pubkey.Length; i++)
			{
				stringBuilder.Append(this.pubkey[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000DD2 RID: 3538
		internal byte[] pubkey;
	}
}
