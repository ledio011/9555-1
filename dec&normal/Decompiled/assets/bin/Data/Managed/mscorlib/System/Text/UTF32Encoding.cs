using System;

namespace System.Text
{
	// Token: 0x020003A8 RID: 936
	[Serializable]
	public sealed class UTF32Encoding : Encoding
	{
		// Token: 0x06001C35 RID: 7221 RVA: 0x0006AC40 File Offset: 0x00068E40
		public UTF32Encoding() : this(false, true, false)
		{
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0006AC4C File Offset: 0x00068E4C
		public UTF32Encoding(bool bigEndian, bool byteOrderMark) : this(bigEndian, byteOrderMark, false)
		{
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0006AC58 File Offset: 0x00068E58
		public UTF32Encoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidCharacters) : base((!bigEndian) ? 12000 : 12001)
		{
			this.bigEndian = bigEndian;
			this.byteOrderMark = byteOrderMark;
			if (throwOnInvalidCharacters)
			{
				base.SetFallbackInternal(EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
			}
			else
			{
				base.SetFallbackInternal(new EncoderReplacementFallback("�"), new DecoderReplacementFallback("�"));
			}
			if (bigEndian)
			{
				this.body_name = "utf-32BE";
				this.encoding_name = "UTF-32 (Big-Endian)";
				this.header_name = "utf-32BE";
				this.web_name = "utf-32BE";
			}
			else
			{
				this.body_name = "utf-32";
				this.encoding_name = "UTF-32";
				this.header_name = "utf-32";
				this.web_name = "utf-32";
			}
			this.windows_code_page = 12000;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006AD34 File Offset: 0x00068F34
		[MonoTODO("handle fallback")]
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
			int num = 0;
			for (int i = index; i < index + count; i++)
			{
				if (char.IsSurrogate(chars[i]))
				{
					if (i + 1 < chars.Length && char.IsSurrogate(chars[i + 1]))
					{
						num += 4;
					}
					else
					{
						num += 4;
					}
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0006ADF4 File Offset: 0x00068FF4
		[MonoTODO("handle fallback")]
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
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
			if (bytes.Length - byteIndex < charCount * 4)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = byteIndex;
			while (charCount-- > 0)
			{
				char c = chars[charIndex++];
				if (char.IsSurrogate(c))
				{
					if (charCount-- > 0)
					{
						int num2 = (int)('Ѐ' * (c - '\ud800')) + 65536 + (int)chars[charIndex++] - 56320;
						if (this.bigEndian)
						{
							for (int i = 0; i < 4; i++)
							{
								bytes[num + 3 - i] = (byte)(num2 % 256);
								num2 >>= 8;
							}
							num += 4;
						}
						else
						{
							for (int j = 0; j < 4; j++)
							{
								bytes[num++] = (byte)(num2 % 256);
								num2 >>= 8;
							}
						}
					}
					else if (this.bigEndian)
					{
						bytes[num++] = 0;
						bytes[num++] = 0;
						bytes[num++] = 0;
						bytes[num++] = 63;
					}
					else
					{
						bytes[num++] = 63;
						bytes[num++] = 0;
						bytes[num++] = 0;
						bytes[num++] = 0;
					}
				}
				else if (this.bigEndian)
				{
					bytes[num++] = 0;
					bytes[num++] = 0;
					bytes[num++] = (byte)(c >> 8);
					bytes[num++] = (byte)c;
				}
				else
				{
					bytes[num++] = (byte)c;
					bytes[num++] = (byte)(c >> 8);
					bytes[num++] = 0;
					bytes[num++] = 0;
				}
			}
			return num - byteIndex;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006B03C File Offset: 0x0006923C
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
			return count / 4;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0006B0AC File Offset: 0x000692AC
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
			if (chars.Length - charIndex < byteCount / 4)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = charIndex;
			if (this.bigEndian)
			{
				while (byteCount >= 4)
				{
					chars[num++] = (char)((int)bytes[byteIndex] << 24 | (int)bytes[byteIndex + 1] << 16 | (int)bytes[byteIndex + 2] << 8 | (int)bytes[byteIndex + 3]);
					byteIndex += 4;
					byteCount -= 4;
				}
			}
			else
			{
				while (byteCount >= 4)
				{
					chars[num++] = (char)((int)bytes[byteIndex] | (int)bytes[byteIndex + 1] << 8 | (int)bytes[byteIndex + 2] << 16 | (int)bytes[byteIndex + 3] << 24);
					byteIndex += 4;
					byteCount -= 4;
				}
			}
			return num - charIndex;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0006B200 File Offset: 0x00069400
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_NonNegative"));
			}
			return charCount * 4;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0006B224 File Offset: 0x00069424
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_NonNegative"));
			}
			return byteCount / 4;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0006B248 File Offset: 0x00069448
		public override Decoder GetDecoder()
		{
			return new UTF32Encoding.UTF32Decoder(this.bigEndian);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0006B258 File Offset: 0x00069458
		public override byte[] GetPreamble()
		{
			if (this.byteOrderMark)
			{
				byte[] array = new byte[4];
				if (this.bigEndian)
				{
					array[2] = 254;
					array[3] = byte.MaxValue;
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

		// Token: 0x06001C40 RID: 7232 RVA: 0x0006B2B0 File Offset: 0x000694B0
		public override bool Equals(object value)
		{
			UTF32Encoding utf32Encoding = value as UTF32Encoding;
			return utf32Encoding != null && (this.codePage == utf32Encoding.codePage && this.bigEndian == utf32Encoding.bigEndian && this.byteOrderMark == utf32Encoding.byteOrderMark) && base.Equals(value);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0006B30C File Offset: 0x0006950C
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.bigEndian)
			{
				num ^= 31;
			}
			if (this.byteOrderMark)
			{
				num ^= 63;
			}
			return num;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0006B344 File Offset: 0x00069544
		[CLSCompliant(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			return count * 4;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0006B35C File Offset: 0x0006955C
		public override int GetByteCount(string s)
		{
			return base.GetByteCount(s);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0006B368 File Offset: 0x00069568
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			return base.GetBytes(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0006B378 File Offset: 0x00069578
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return base.GetBytes(s, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0006B388 File Offset: 0x00069588
		public override string GetString(byte[] bytes, int index, int count)
		{
			return base.GetString(bytes, index, count);
		}

		// Token: 0x04000EFF RID: 3839
		internal const int UTF32_CODE_PAGE = 12000;

		// Token: 0x04000F00 RID: 3840
		internal const int BIG_UTF32_CODE_PAGE = 12001;

		// Token: 0x04000F01 RID: 3841
		private bool bigEndian;

		// Token: 0x04000F02 RID: 3842
		private bool byteOrderMark;

		// Token: 0x020003A9 RID: 937
		private sealed class UTF32Decoder : Decoder
		{
			// Token: 0x06001C47 RID: 7239 RVA: 0x0006B394 File Offset: 0x00069594
			public UTF32Decoder(bool bigEndian)
			{
				this.bigEndian = bigEndian;
				this.leftOverByte = -1;
			}

			// Token: 0x06001C48 RID: 7240 RVA: 0x0006B3AC File Offset: 0x000695AC
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
				int num = charIndex;
				int num2 = this.leftOverByte;
				int num3 = chars.Length;
				int num4 = 4 - this.leftOverLength;
				if (this.leftOverLength > 0 && byteCount > num4)
				{
					if (this.bigEndian)
					{
						for (int i = 0; i < num4; i++)
						{
							num2 += (int)bytes[byteIndex++] << 4 - byteCount--;
						}
					}
					else
					{
						for (int j = 0; j < num4; j++)
						{
							num2 += (int)bytes[byteIndex++] << byteCount--;
						}
					}
					if ((num2 > 65535 && num + 1 < num3) || num < num3)
					{
						throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
					}
					if (num2 > 65535)
					{
						chars[num++] = (char)((num2 - 10000) / 1024 + 55296);
						chars[num++] = (char)((num2 - 10000) % 1024 + 56320);
					}
					else
					{
						chars[num++] = (char)num2;
					}
					this.leftOverLength = 0;
				}
				while (byteCount > 3)
				{
					char c;
					if (this.bigEndian)
					{
						c = (char)((int)bytes[byteIndex++] << 24 | (int)bytes[byteIndex++] << 16 | (int)bytes[byteIndex++] << 8 | (int)bytes[byteIndex++]);
					}
					else
					{
						c = (char)((int)bytes[byteIndex++] | (int)bytes[byteIndex++] << 8 | (int)bytes[byteIndex++] << 16 | (int)bytes[byteIndex++] << 24);
					}
					byteCount -= 4;
					if (num >= num3)
					{
						throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
					}
					chars[num++] = c;
				}
				if (byteCount > 0)
				{
					this.leftOverLength = byteCount;
					num2 = 0;
					if (this.bigEndian)
					{
						for (int k = 0; k < byteCount; k++)
						{
							num2 += (int)bytes[byteIndex++] << 4 - byteCount--;
						}
					}
					else
					{
						for (int l = 0; l < byteCount; l++)
						{
							num2 += (int)bytes[byteIndex++] << byteCount--;
						}
					}
					this.leftOverByte = num2;
				}
				return num - charIndex;
			}

			// Token: 0x04000F03 RID: 3843
			private bool bigEndian;

			// Token: 0x04000F04 RID: 3844
			private int leftOverByte;

			// Token: 0x04000F05 RID: 3845
			private int leftOverLength;
		}
	}
}
