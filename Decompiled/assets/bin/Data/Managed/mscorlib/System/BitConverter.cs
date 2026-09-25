using System;
using System.Text;

namespace System
{
	// Token: 0x0200006F RID: 111
	public static class BitConverter
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00011C68 File Offset: 0x0000FE68
		private static bool AmILittleEndian()
		{
			double num = 1.0;
			return num == (double)0;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011C88 File Offset: 0x0000FE88
		private unsafe static bool DoubleWordsAreSwapped()
		{
			double num = 1.0;
			return *(ref num + 2) == 240;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		public static long DoubleToInt64Bits(double value)
		{
			return BitConverter.ToInt64(BitConverter.GetBytes(value), 0);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		private unsafe static byte[] GetBytes(byte* ptr, int count)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ptr[i];
			}
			return array;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		public unsafe static byte[] GetBytes(float value)
		{
			return BitConverter.GetBytes((byte*)(&value), 4);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00011CFC File Offset: 0x0000FEFC
		public unsafe static byte[] GetBytes(double value)
		{
			if (BitConverter.SwappedWordsInDouble)
			{
				return new byte[]
				{
					*(ref value + 4),
					*(ref value + 5),
					*(ref value + 6),
					*(ref value + 7),
					(byte)value,
					*(ref value + 1),
					*(ref value + 2),
					*(ref value + 3)
				};
			}
			return BitConverter.GetBytes((byte*)(&value), 8);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011D60 File Offset: 0x0000FF60
		private unsafe static void PutBytes(byte* dst, byte[] src, int start_index, int count)
		{
			if (src == null)
			{
				throw new ArgumentNullException("value");
			}
			if (start_index < 0 || start_index > src.Length - 1)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (src.Length - count < start_index)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < count; i++)
			{
				dst[i] = src[i + start_index];
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		public unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long result;
			BitConverter.PutBytes((byte*)(&result), value, startIndex, 8);
			return result;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011DEC File Offset: 0x0000FFEC
		public unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float result;
			BitConverter.PutBytes((byte*)(&result), value, startIndex, 4);
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011E04 File Offset: 0x00010004
		public static string ToString(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return BitConverter.ToString(value, 0, value.Length);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011E24 File Offset: 0x00010024
		public static string ToString(byte[] value, int startIndex, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			if (startIndex < 0 || startIndex >= value.Length)
			{
				if (startIndex == 0 && value.Length == 0)
				{
					return string.Empty;
				}
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", "Value must be positive.");
				}
				if (startIndex > value.Length - length)
				{
					throw new ArgumentException("startIndex + length > value.Length");
				}
				if (length == 0)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder(length * 3 - 1);
				int num = startIndex + length;
				for (int i = startIndex; i < num; i++)
				{
					if (i > startIndex)
					{
						stringBuilder.Append('-');
					}
					char c = (char)(value[i] >> 4 & 15);
					char c2 = (char)(value[i] & 15);
					if (c < '\n')
					{
						c += '0';
					}
					else
					{
						c -= '\n';
						c += 'A';
					}
					if (c2 < '\n')
					{
						c2 += '0';
					}
					else
					{
						c2 -= '\n';
						c2 += 'A';
					}
					stringBuilder.Append(c);
					stringBuilder.Append(c2);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x040001AF RID: 431
		private static readonly bool SwappedWordsInDouble = BitConverter.DoubleWordsAreSwapped();

		// Token: 0x040001B0 RID: 432
		public static readonly bool IsLittleEndian = BitConverter.AmILittleEndian();
	}
}
