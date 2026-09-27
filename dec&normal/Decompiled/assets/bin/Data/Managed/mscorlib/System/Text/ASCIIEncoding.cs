using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000390 RID: 912
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class ASCIIEncoding : Encoding
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x00066D68 File Offset: 0x00064F68
		public ASCIIEncoding() : base(20127)
		{
			this.body_name = (this.header_name = (this.web_name = "us-ascii"));
			this.encoding_name = "US-ASCII";
			this.is_mail_news_display = true;
			this.is_mail_news_save = true;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00066DB8 File Offset: 0x00064FB8
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
			return count;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00066E24 File Offset: 0x00065024
		public override int GetByteCount(string chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			return chars.Length;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00066E40 File Offset: 0x00065040
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char[] array = null;
			return this.GetBytes(chars, charIndex, charCount, bytes, byteIndex, ref encoderFallbackBuffer, ref array);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00066E64 File Offset: 0x00065064
		private int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, ref EncoderFallbackBuffer buffer, ref char[] fallback_chars)
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
			if (bytes.Length - byteIndex < charCount)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = charCount;
			while (num-- > 0)
			{
				char c = chars[charIndex++];
				if (c < '\u0080')
				{
					bytes[byteIndex++] = (byte)c;
				}
				else
				{
					if (buffer == null)
					{
						buffer = base.EncoderFallback.CreateFallbackBuffer();
					}
					if (char.IsSurrogate(c) && num > 1 && char.IsSurrogate(chars[charIndex]))
					{
						buffer.Fallback(c, chars[charIndex], charIndex++ - 1);
					}
					else
					{
						buffer.Fallback(c, charIndex - 1);
					}
					if (fallback_chars == null || fallback_chars.Length < buffer.Remaining)
					{
						fallback_chars = new char[buffer.Remaining];
					}
					for (int i = 0; i < fallback_chars.Length; i++)
					{
						fallback_chars[i] = buffer.GetNextChar();
					}
					byteIndex += this.GetBytes(fallback_chars, 0, fallback_chars.Length, bytes, byteIndex, ref buffer, ref fallback_chars);
				}
			}
			return charCount;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00067024 File Offset: 0x00065224
		public override int GetBytes(string chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char[] array = null;
			return this.GetBytes(chars, charIndex, charCount, bytes, byteIndex, ref encoderFallbackBuffer, ref array);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00067048 File Offset: 0x00065248
		private int GetBytes(string chars, int charIndex, int charCount, byte[] bytes, int byteIndex, ref EncoderFallbackBuffer buffer, ref char[] fallback_chars)
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
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_StringIndex"));
			}
			if (charCount < 0 || charCount > chars.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_StringRange"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (bytes.Length - byteIndex < charCount)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = charCount;
			while (num-- > 0)
			{
				char c = chars[charIndex++];
				if (c < '\u0080')
				{
					bytes[byteIndex++] = (byte)c;
				}
				else
				{
					if (buffer == null)
					{
						buffer = base.EncoderFallback.CreateFallbackBuffer();
					}
					if (char.IsSurrogate(c) && num > 1 && char.IsSurrogate(chars[charIndex]))
					{
						buffer.Fallback(c, chars[charIndex], charIndex++ - 1);
					}
					else
					{
						buffer.Fallback(c, charIndex - 1);
					}
					if (fallback_chars == null || fallback_chars.Length < buffer.Remaining)
					{
						fallback_chars = new char[buffer.Remaining];
					}
					for (int i = 0; i < fallback_chars.Length; i++)
					{
						fallback_chars[i] = buffer.GetNextChar();
					}
					byteIndex += this.GetBytes(fallback_chars, 0, fallback_chars.Length, bytes, byteIndex, ref buffer, ref fallback_chars);
				}
			}
			return charCount;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00067218 File Offset: 0x00065418
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
			return count;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00067284 File Offset: 0x00065484
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, ref decoderFallbackBuffer);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000672A4 File Offset: 0x000654A4
		private int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, ref DecoderFallbackBuffer buffer)
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
			if (chars.Length - charIndex < byteCount)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = byteCount;
			while (num-- > 0)
			{
				char c = (char)bytes[byteIndex++];
				if (c < '\u0080')
				{
					chars[charIndex++] = c;
				}
				else
				{
					if (buffer == null)
					{
						buffer = base.DecoderFallback.CreateFallbackBuffer();
					}
					buffer.Fallback(bytes, byteIndex);
					while (buffer.Remaining > 0)
					{
						chars[charIndex++] = buffer.GetNextChar();
					}
				}
			}
			return byteCount;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000673E4 File Offset: 0x000655E4
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_NonNegative"));
			}
			return charCount;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00067404 File Offset: 0x00065604
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_NonNegative"));
			}
			return byteCount;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00067424 File Offset: 0x00065624
		public unsafe override string GetString(byte[] bytes, int byteIndex, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (byteCount < 0 || byteCount > bytes.Length - byteIndex)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_Array"));
			}
			if (byteCount == 0)
			{
				return string.Empty;
			}
			fixed (byte* ptr = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
			{
				string text = string.InternalAllocateStr(byteCount);
				fixed (string text2 = text)
				{
					fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						byte* ptr3 = ptr + byteIndex;
						byte* ptr4 = ptr3 + byteCount;
						char* ptr5 = ptr2;
						while (ptr3 < ptr4)
						{
							byte b = *(ptr3++);
							*(ptr5++) = ((b > 127) ? '?' : ((char)b));
						}
						text2 = null;
						return text;
					}
				}
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00067510 File Offset: 0x00065710
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			if (byteCount < charCount)
			{
				throw new ArgumentException("bytecount is less than the number of bytes required", "byteCount");
			}
			for (int i = 0; i < charCount; i++)
			{
				char c = chars[i];
				bytes[i] = (byte)((c >= '\u0080') ? '?' : c);
			}
			return charCount;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000675B0 File Offset: 0x000657B0
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			return count;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000675B4 File Offset: 0x000657B4
		[MonoTODO("we have simple override to match method signature.")]
		[ComVisible(false)]
		public override Decoder GetDecoder()
		{
			return base.GetDecoder();
		}

		// Token: 0x04000EB4 RID: 3764
		internal const int ASCII_CODE_PAGE = 20127;
	}
}
