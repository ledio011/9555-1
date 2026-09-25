using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security;

namespace System.IO
{
	// Token: 0x02000119 RID: 281
	[ComVisible(true)]
	public class BinaryReader : IDisposable
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0002A678 File Offset: 0x00028878
		public BinaryReader(Stream input) : this(input, Encoding.UTF8UnmarkedUnsafe)
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002A688 File Offset: 0x00028888
		public BinaryReader(Stream input, Encoding encoding)
		{
			if (input == null || encoding == null)
			{
				throw new ArgumentNullException(Locale.GetText("Input or Encoding is a null reference."));
			}
			if (!input.CanRead)
			{
				throw new ArgumentException(Locale.GetText("The stream doesn't support reading."));
			}
			this.m_stream = input;
			this.m_encoding = encoding;
			this.decoder = encoding.GetDecoder();
			this.m_buffer = new byte[32];
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002A6FC File Offset: 0x000288FC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002A708 File Offset: 0x00028908
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_stream != null)
			{
				this.m_stream.Close();
			}
			this.m_disposed = true;
			this.m_buffer = null;
			this.m_encoding = null;
			this.m_stream = null;
			this.charBuffer = null;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002A754 File Offset: 0x00028954
		protected virtual void FillBuffer(int numBytes)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("BinaryReader", "Cannot read from a closed BinaryReader.");
			}
			if (this.m_stream == null)
			{
				throw new IOException("Stream is invalid");
			}
			this.CheckBuffer(numBytes);
			int num;
			for (int i = 0; i < numBytes; i += num)
			{
				num = this.m_stream.Read(this.m_buffer, i, numBytes - i);
				if (num == 0)
				{
					throw new EndOfStreamException();
				}
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002A7D0 File Offset: 0x000289D0
		public virtual int Read()
		{
			if (this.charBuffer == null)
			{
				this.charBuffer = new char[128];
			}
			if (this.Read(this.charBuffer, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.charBuffer[0];
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002A818 File Offset: 0x00028A18
		public virtual int Read(byte[] buffer, int index, int count)
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException("Stream is invalid");
			}
			else
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer is null");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index is less than 0");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count is less than 0");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException("buffer is too small");
				}
				return this.m_stream.Read(buffer, index, count);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002A8B4 File Offset: 0x00028AB4
		public virtual int Read(char[] buffer, int index, int count)
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException("Stream is invalid");
			}
			else
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer is null");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index is less than 0");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count is less than 0");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException("buffer is too small");
				}
				int num;
				return this.ReadCharBytes(buffer, index, count, out num);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002A948 File Offset: 0x00028B48
		private int ReadCharBytes(char[] buffer, int index, int count, out int bytes_read)
		{
			int i = 0;
			bytes_read = 0;
			while (i < count)
			{
				int num = 0;
				int chars;
				do
				{
					this.CheckBuffer(num + 1);
					int num2 = this.m_stream.ReadByte();
					if (num2 == -1)
					{
						return i;
					}
					this.m_buffer[num++] = (byte)num2;
					bytes_read++;
					chars = this.m_encoding.GetChars(this.m_buffer, 0, num, buffer, index + i);
				}
				while (chars <= 0);
				i++;
			}
			return i;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002A9CC File Offset: 0x00028BCC
		protected int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < 5; i++)
			{
				byte b = this.ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
				if ((b & 128) == 0)
				{
					break;
				}
			}
			if (i < 5)
			{
				return num;
			}
			throw new FormatException("Too many bytes in what should have been a 7 bit encoded Int32.");
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public virtual bool ReadBoolean()
		{
			return this.ReadByte() != 0;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002AA3C File Offset: 0x00028C3C
		public virtual byte ReadByte()
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException("Stream is invalid");
			}
			else
			{
				int num = this.m_stream.ReadByte();
				if (num != -1)
				{
					return (byte)num;
				}
				throw new EndOfStreamException();
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002AA98 File Offset: 0x00028C98
		public virtual char ReadChar()
		{
			int num = this.Read();
			if (num == -1)
			{
				throw new EndOfStreamException();
			}
			return (char)num;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002AABC File Offset: 0x00028CBC
		public unsafe virtual decimal ReadDecimal()
		{
			this.FillBuffer(16);
			decimal result;
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 16; i++)
				{
					if (i < 4)
					{
						*(ref result + (i + 8)) = this.m_buffer[i];
					}
					else if (i < 8)
					{
						*(ref result + (i + 8)) = this.m_buffer[i];
					}
					else if (i < 12)
					{
						*(ref result + (i - 4)) = this.m_buffer[i];
					}
					else if (i < 16)
					{
						*(ref result + (i - 12)) = this.m_buffer[i];
					}
				}
			}
			else
			{
				for (int j = 0; j < 16; j++)
				{
					if (j < 4)
					{
						*(ref result + (11 - j)) = this.m_buffer[j];
					}
					else if (j < 8)
					{
						*(ref result + (19 - j)) = this.m_buffer[j];
					}
					else if (j < 12)
					{
						*(ref result + (15 - j)) = this.m_buffer[j];
					}
					else if (j < 16)
					{
						*(ref result + (15 - j)) = this.m_buffer[j];
					}
				}
			}
			return result;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002ABDC File Offset: 0x00028DDC
		public virtual double ReadDouble()
		{
			this.FillBuffer(8);
			return BitConverterLE.ToDouble(this.m_buffer, 0);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		public virtual short ReadInt16()
		{
			this.FillBuffer(2);
			return (short)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002AC14 File Offset: 0x00028E14
		public virtual int ReadInt32()
		{
			this.FillBuffer(4);
			return (int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002AC48 File Offset: 0x00028E48
		public virtual long ReadInt64()
		{
			this.FillBuffer(8);
			uint num = (uint)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24);
			uint num2 = (uint)((int)this.m_buffer[4] | (int)this.m_buffer[5] << 8 | (int)this.m_buffer[6] << 16 | (int)this.m_buffer[7] << 24);
			return (long)((ulong)num2 << 32 | (ulong)num);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002ACBC File Offset: 0x00028EBC
		[CLSCompliant(false)]
		public virtual sbyte ReadSByte()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		public virtual string ReadString()
		{
			int num = this.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new IOException("Invalid binary file (string len < 0)");
			}
			if (num == 0)
			{
				return string.Empty;
			}
			if (this.charBuffer == null)
			{
				this.charBuffer = new char[128];
			}
			StringBuilder stringBuilder = null;
			int chars;
			for (;;)
			{
				int num2 = (num <= 128) ? num : 128;
				this.FillBuffer(num2);
				chars = this.decoder.GetChars(this.m_buffer, 0, num2, this.charBuffer, 0);
				if (stringBuilder == null && num2 == num)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num);
				}
				stringBuilder.Append(this.charBuffer, 0, chars);
				num -= num2;
				if (num <= 0)
				{
					goto Block_8;
				}
			}
			return new string(this.charBuffer, 0, chars);
			Block_8:
			return stringBuilder.ToString();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002AD98 File Offset: 0x00028F98
		public virtual float ReadSingle()
		{
			this.FillBuffer(4);
			return BitConverterLE.ToSingle(this.m_buffer, 0);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002ADB0 File Offset: 0x00028FB0
		[CLSCompliant(false)]
		public virtual ushort ReadUInt16()
		{
			this.FillBuffer(2);
			return (ushort)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002ADD0 File Offset: 0x00028FD0
		[CLSCompliant(false)]
		public virtual uint ReadUInt32()
		{
			this.FillBuffer(4);
			return (uint)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002AE04 File Offset: 0x00029004
		[CLSCompliant(false)]
		public virtual ulong ReadUInt64()
		{
			this.FillBuffer(8);
			uint num = (uint)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24);
			uint num2 = (uint)((int)this.m_buffer[4] | (int)this.m_buffer[5] << 8 | (int)this.m_buffer[6] << 16 | (int)this.m_buffer[7] << 24);
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002AE78 File Offset: 0x00029078
		private void CheckBuffer(int length)
		{
			if (this.m_buffer.Length <= length)
			{
				byte[] array = new byte[length];
				Buffer.BlockCopyInternal(this.m_buffer, 0, array, 0, this.m_buffer.Length);
				this.m_buffer = array;
			}
		}

		// Token: 0x04000468 RID: 1128
		private const int MaxBufferSize = 128;

		// Token: 0x04000469 RID: 1129
		private Stream m_stream;

		// Token: 0x0400046A RID: 1130
		private Encoding m_encoding;

		// Token: 0x0400046B RID: 1131
		private byte[] m_buffer;

		// Token: 0x0400046C RID: 1132
		private Decoder decoder;

		// Token: 0x0400046D RID: 1133
		private char[] charBuffer;

		// Token: 0x0400046E RID: 1134
		private bool m_disposed;
	}
}
