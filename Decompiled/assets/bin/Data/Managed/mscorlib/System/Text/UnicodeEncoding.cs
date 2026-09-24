using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x020003A6 RID: 934
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class UnicodeEncoding : Encoding
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x0006A054 File Offset: 0x00068254
		public UnicodeEncoding() : this(false, true)
		{
			this.bigEndian = false;
			this.byteOrderMark = true;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0006A06C File Offset: 0x0006826C
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark) : this(bigEndian, byteOrderMark, false)
		{
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0006A078 File Offset: 0x00068278
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidBytes) : base((!bigEndian) ? 1200 : 1201)
		{
			if (throwOnInvalidBytes)
			{
				base.SetFallbackInternal(null, new DecoderExceptionFallback());
			}
			else
			{
				base.SetFallbackInternal(null, new DecoderReplacementFallback("�"));
			}
			this.bigEndian = bigEndian;
			this.byteOrderMark = byteOrderMark;
			if (bigEndian)
			{
				this.body_name = "unicodeFFFE";
				this.encoding_name = "Unicode (Big-Endian)";
				this.header_name = "unicodeFFFE";
				this.is_browser_save = false;
				this.web_name = "unicodeFFFE";
			}
			else
			{
				this.body_name = "utf-16";
				this.encoding_name = "Unicode";
				this.header_name = "utf-16";
				this.is_browser_save = true;
				this.web_name = "utf-16";
			}
			this.windows_code_page = 1200;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0006A154 File Offset: 0x00068354
		public override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (index < 0 || index > chars.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || count > chars.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			return count * 2;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0006A1C4 File Offset: 0x000683C4
		public override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.Length * 2;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0006A1E0 File Offset: 0x000683E0
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return count * 2;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0006A208 File Offset: 0x00068408
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount < 0 || charCount > chars.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_Array"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount == 0)
			{
				return 0;
			}
			int byteCount = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			fixed (char* ptr = ref (chars != null && chars.Length != 0) ? ref chars[0] : ref *null)
			{
				fixed (byte* ptr2 = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
				{
					return this.GetBytesInternal(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount);
				}
			}
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0006A31C File Offset: 0x0006851C
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (charIndex < 0 || charIndex > s.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_StringIndex"));
			}
			if (charCount < 0 || charCount > s.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_StringRange"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount == 0)
			{
				return 0;
			}
			int byteCount = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			fixed (char* ptr = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (byte* ptr2 = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
				{
					return this.GetBytesInternal(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount);
				}
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0006A424 File Offset: 0x00068624
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			return this.GetBytesInternal(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0006A484 File Offset: 0x00068684
		private unsafe int GetBytesInternal(char* chars, int charCount, byte* bytes, int byteCount)
		{
			int num = charCount * 2;
			if (byteCount < num)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			UnicodeEncoding.CopyChars((byte*)chars, bytes, num, this.bigEndian);
			return num;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0006A4BC File Offset: 0x000686BC
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (index < 0 || index > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || count > bytes.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			return count / 2;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006A52C File Offset: 0x0006872C
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (byteCount < 0 || byteCount > bytes.Length - byteIndex)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_Array"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
			}
			if (byteCount == 0)
			{
				return 0;
			}
			int charCount = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			fixed (byte* ptr = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
			{
				fixed (char* ptr2 = ref (chars != null && chars.Length != 0) ? ref chars[0] : ref *null)
				{
					return this.GetCharsInternal(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount);
				}
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0006A640 File Offset: 0x00068840
		[ComVisible(false)]
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (index < 0 || index > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || count > bytes.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			if (count == 0)
			{
				return string.Empty;
			}
			int num = count / 2;
			string text = string.InternalAllocateStr(num);
			fixed (byte* ptr = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
			{
				fixed (string text2 = text)
				{
					fixed (char* chars = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						this.GetCharsInternal(ptr + index, count, chars, num);
						text2 = null;
						ptr = null;
						return text;
					}
				}
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0006A700 File Offset: 0x00068900
		private unsafe int GetCharsInternal(byte* bytes, int byteCount, char* chars, int charCount)
		{
			int num = byteCount / 2;
			if (charCount < num)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			UnicodeEncoding.CopyChars(bytes, (byte*)chars, byteCount, this.bigEndian);
			return num;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0006A738 File Offset: 0x00068938
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_NonNegative"));
			}
			return charCount * 2;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0006A75C File Offset: 0x0006895C
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_NonNegative"));
			}
			return byteCount / 2;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0006A780 File Offset: 0x00068980
		public override Decoder GetDecoder()
		{
			return new UnicodeEncoding.UnicodeDecoder(this.bigEndian);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0006A790 File Offset: 0x00068990
		public override byte[] GetPreamble()
		{
			if (this.byteOrderMark)
			{
				byte[] array = new byte[2];
				if (this.bigEndian)
				{
					array[0] = 254;
					array[1] = byte.MaxValue;
				}
				else
				{
					array[0] = byte.MaxValue;
					array[1] = 254;
				}
				return array;
			}
			return new byte[0];
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006A7E8 File Offset: 0x000689E8
		public override bool Equals(object value)
		{
			UnicodeEncoding unicodeEncoding = value as UnicodeEncoding;
			return unicodeEncoding != null && (this.codePage == unicodeEncoding.codePage && this.bigEndian == unicodeEncoding.bigEndian) && this.byteOrderMark == unicodeEncoding.byteOrderMark;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0006A838 File Offset: 0x00068A38
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0006A840 File Offset: 0x00068A40
		private unsafe static void CopyChars(byte* src, byte* dest, int count, bool bigEndian)
		{
			if (BitConverter.IsLittleEndian != bigEndian)
			{
				string.memcpy(dest, src, count & -2);
				return;
			}
			switch (count)
			{
			case 0:
				return;
			case 1:
				return;
			case 2:
				goto IL_220;
			case 3:
				goto IL_220;
			case 4:
				goto IL_1F1;
			case 5:
				goto IL_1F1;
			case 6:
				goto IL_1F1;
			case 7:
				goto IL_1F1;
			case 8:
				break;
			case 9:
				break;
			case 10:
				break;
			case 11:
				break;
			case 12:
				break;
			case 13:
				break;
			case 14:
				break;
			case 15:
				break;
			default:
				do
				{
					*dest = src[1];
					dest[1] = *src;
					dest[2] = src[3];
					dest[3] = src[2];
					dest[4] = src[5];
					dest[5] = src[4];
					dest[6] = src[7];
					dest[7] = src[6];
					dest[8] = src[9];
					dest[9] = src[8];
					dest[10] = src[11];
					dest[11] = src[10];
					dest[12] = src[13];
					dest[13] = src[12];
					dest[14] = src[15];
					dest[15] = src[14];
					dest += 16;
					src += 16;
					count -= 16;
				}
				while ((count & -16) != 0);
				switch (count)
				{
				case 0:
					return;
				case 1:
					return;
				case 2:
					goto IL_220;
				case 3:
					goto IL_220;
				case 4:
					goto IL_1F1;
				case 5:
					goto IL_1F1;
				case 6:
					goto IL_1F1;
				case 7:
					goto IL_1F1;
				}
				break;
			}
			*dest = src[1];
			dest[1] = *src;
			dest[2] = src[3];
			dest[3] = src[2];
			dest[4] = src[5];
			dest[5] = src[4];
			dest[6] = src[7];
			dest[7] = src[6];
			dest += 8;
			src += 8;
			if ((count & 4) == 0)
			{
				goto IL_217;
			}
			IL_1F1:
			*dest = src[1];
			dest[1] = *src;
			dest[2] = src[3];
			dest[3] = src[2];
			dest += 4;
			src += 4;
			IL_217:
			if ((count & 2) == 0)
			{
				return;
			}
			IL_220:
			*dest = src[1];
			dest[1] = *src;
		}

		// Token: 0x04000EF8 RID: 3832
		internal const int UNICODE_CODE_PAGE = 1200;

		// Token: 0x04000EF9 RID: 3833
		internal const int BIG_UNICODE_CODE_PAGE = 1201;

		// Token: 0x04000EFA RID: 3834
		public const int CharSize = 2;

		// Token: 0x04000EFB RID: 3835
		private bool bigEndian;

		// Token: 0x04000EFC RID: 3836
		private bool byteOrderMark;

		// Token: 0x020003A7 RID: 935
		private sealed class UnicodeDecoder : Decoder
		{
			// Token: 0x06001C33 RID: 7219 RVA: 0x0006AA7C File Offset: 0x00068C7C
			public UnicodeDecoder(bool bigEndian)
			{
				this.bigEndian = bigEndian;
				this.leftOverByte = -1;
			}

			// Token: 0x06001C34 RID: 7220 RVA: 0x0006AA94 File Offset: 0x00068C94
			public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				if (chars == null)
				{
					throw new ArgumentNullException("chars");
				}
				if (byteIndex < 0 || byteIndex > bytes.Length)
				{
					throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
				}
				if (byteCount < 0 || byteCount > bytes.Length - byteIndex)
				{
					throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_Array"));
				}
				if (charIndex < 0 || charIndex > chars.Length)
				{
					throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
				}
				if (byteCount == 0)
				{
					return 0;
				}
				int num = this.leftOverByte;
				int num2;
				if (num != -1)
				{
					num2 = (byteCount + 1) / 2;
				}
				else
				{
					num2 = byteCount / 2;
				}
				if (chars.Length - charIndex < num2)
				{
					throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
				}
				if (num != -1)
				{
					if (this.bigEndian)
					{
						chars[charIndex] = (char)(num << 8 | (int)bytes[byteIndex]);
					}
					else
					{
						chars[charIndex] = (char)((int)bytes[byteIndex] << 8 | num);
					}
					charIndex++;
					byteIndex++;
					byteCount--;
				}
				if ((byteCount & -2) != 0)
				{
					fixed (byte* ptr = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
					{
						fixed (char* ptr2 = ref (chars != null && chars.Length != 0) ? ref chars[0] : ref *null)
						{
							UnicodeEncoding.CopyChars(ptr + byteIndex, (byte*)(ptr2 + charIndex), byteCount, this.bigEndian);
						}
					}
				}
				if ((byteCount & 1) == 0)
				{
					this.leftOverByte = -1;
				}
				else
				{
					this.leftOverByte = (int)bytes[byteCount + byteIndex - 1];
				}
				return num2;
			}

			// Token: 0x04000EFD RID: 3837
			private bool bigEndian;

			// Token: 0x04000EFE RID: 3838
			private int leftOverByte;
		}
	}
}
