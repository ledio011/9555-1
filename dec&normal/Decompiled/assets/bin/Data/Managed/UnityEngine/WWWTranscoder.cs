using System;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000140 RID: 320
	internal sealed class WWWTranscoder
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x0001B70C File Offset: 0x0001990C
		private static byte[] Byte2Hex(byte b, byte[] hexChars)
		{
			return new byte[]
			{
				hexChars[b >> 4],
				hexChars[(int)(b & 15)]
			};
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0001B734 File Offset: 0x00019934
		public static byte[] URLEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0001B74C File Offset: 0x0001994C
		public static string QPEncode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
			return WWW.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001B788 File Offset: 0x00019988
		public static byte[] Encode(byte[] input, byte escapeChar, byte space, byte[] forbidden, bool uppercase)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == 32)
					{
						memoryStream.WriteByte(space);
					}
					else if (input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]))
					{
						memoryStream.WriteByte(escapeChar);
						memoryStream.Write(WWWTranscoder.Byte2Hex(input[i], (!uppercase) ? WWWTranscoder.lcHexChars : WWWTranscoder.ucHexChars), 0, 2);
					}
					else
					{
						memoryStream.WriteByte(input[i]);
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001B858 File Offset: 0x00019A58
		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array[i] == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001B888 File Offset: 0x00019A88
		public static bool SevenBitClean(string s, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			return WWWTranscoder.SevenBitClean(e.GetBytes(s));
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001B898 File Offset: 0x00019A98
		public static bool SevenBitClean(byte[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] < 32 || input[i] > 126)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000505 RID: 1285
		private static byte[] ucHexChars = WWW.DefaultEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x04000506 RID: 1286
		private static byte[] lcHexChars = WWW.DefaultEncoding.GetBytes("0123456789abcdef");

		// Token: 0x04000507 RID: 1287
		private static byte urlEscapeChar = 37;

		// Token: 0x04000508 RID: 1288
		private static byte urlSpace = 43;

		// Token: 0x04000509 RID: 1289
		private static byte[] urlForbidden = WWW.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		// Token: 0x0400050A RID: 1290
		private static byte qpEscapeChar = 61;

		// Token: 0x0400050B RID: 1291
		private static byte qpSpace = 95;

		// Token: 0x0400050C RID: 1292
		private static byte[] qpForbidden = WWW.DefaultEncoding.GetBytes("&;=?\"'%+_");
	}
}
