using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security;

namespace System.IO
{
	// Token: 0x0200011A RID: 282
	[ComVisible(true)]
	[Serializable]
	public class BinaryWriter : IDisposable
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002AEB8 File Offset: 0x000290B8
		protected BinaryWriter() : this(Stream.Null, Encoding.UTF8UnmarkedUnsafe)
		{
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002AECC File Offset: 0x000290CC
		public BinaryWriter(Stream output) : this(output, Encoding.UTF8UnmarkedUnsafe)
		{
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002AEDC File Offset: 0x000290DC
		public BinaryWriter(Stream output, Encoding encoding)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!output.CanWrite)
			{
				throw new ArgumentException(Locale.GetText("Stream does not support writing or already closed."));
			}
			this.OutStream = output;
			this.m_encoding = encoding;
			this.buffer = new byte[16];
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002AF54 File Offset: 0x00029154
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002AF60 File Offset: 0x00029160
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.OutStream != null)
			{
				this.OutStream.Close();
			}
			this.buffer = null;
			this.m_encoding = null;
			this.disposed = true;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002AF94 File Offset: 0x00029194
		public virtual void Flush()
		{
			this.OutStream.Flush();
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002AFA4 File Offset: 0x000291A4
		public virtual void Write(bool value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = ((!value) ? 0 : 1);
			this.OutStream.Write(this.buffer, 0, 1);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002AFF8 File Offset: 0x000291F8
		public virtual void Write(byte value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.OutStream.WriteByte(value);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002B024 File Offset: 0x00029224
		public virtual void Write(byte[] buffer)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002B064 File Offset: 0x00029264
		public virtual void Write(byte[] buffer, int index, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, index, count);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public virtual void Write(char ch)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			char[] chars = new char[]
			{
				ch
			};
			byte[] bytes = this.m_encoding.GetBytes(chars, 0, 1);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002B0F4 File Offset: 0x000292F4
		public virtual void Write(char[] chars)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this.m_encoding.GetBytes(chars, 0, chars.Length);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002B150 File Offset: 0x00029350
		public unsafe virtual void Write(decimal value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 16; i++)
				{
					if (i < 4)
					{
						this.buffer[i + 12] = *(ref value + i);
					}
					else if (i < 8)
					{
						this.buffer[i + 4] = *(ref value + i);
					}
					else if (i < 12)
					{
						this.buffer[i - 8] = *(ref value + i);
					}
					else
					{
						this.buffer[i - 8] = *(ref value + i);
					}
				}
			}
			else
			{
				for (int j = 0; j < 16; j++)
				{
					if (j < 4)
					{
						this.buffer[15 - j] = *(ref value + j);
					}
					else if (j < 8)
					{
						this.buffer[15 - j] = *(ref value + j);
					}
					else if (j < 12)
					{
						this.buffer[11 - j] = *(ref value + j);
					}
					else
					{
						this.buffer[19 - j] = *(ref value + j);
					}
				}
			}
			this.OutStream.Write(this.buffer, 0, 16);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002B284 File Offset: 0x00029484
		public virtual void Write(double value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 8);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002B2B4 File Offset: 0x000294B4
		public virtual void Write(short value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this.buffer, 0, 2);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002B308 File Offset: 0x00029508
		public virtual void Write(int value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.buffer[2] = (byte)(value >> 16);
			this.buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this.buffer, 0, 4);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002B374 File Offset: 0x00029574
		public virtual void Write(long value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			int i = 0;
			int num = 0;
			while (i < 8)
			{
				this.buffer[i] = (byte)(value >> num);
				i++;
				num += 8;
			}
			this.OutStream.Write(this.buffer, 0, 8);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002B3D8 File Offset: 0x000295D8
		[CLSCompliant(false)]
		public virtual void Write(sbyte value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.OutStream.Write(this.buffer, 0, 1);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002B414 File Offset: 0x00029614
		public virtual void Write(float value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 4);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002B444 File Offset: 0x00029644
		public virtual void Write(string value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			int byteCount = this.m_encoding.GetByteCount(value);
			this.Write7BitEncodedInt(byteCount);
			if (this.stringBuffer == null)
			{
				this.stringBuffer = new byte[512];
				this.maxCharsPerRound = 512 / this.m_encoding.GetMaxByteCount(1);
			}
			int num = 0;
			int num2;
			for (int i = value.Length; i > 0; i -= num2)
			{
				num2 = ((i <= this.maxCharsPerRound) ? i : this.maxCharsPerRound);
				int bytes = this.m_encoding.GetBytes(value, num, num2, this.stringBuffer, 0);
				this.OutStream.Write(this.stringBuffer, 0, bytes);
				num += num2;
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002B514 File Offset: 0x00029714
		[CLSCompliant(false)]
		public virtual void Write(ushort value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this.buffer, 0, 2);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002B568 File Offset: 0x00029768
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.buffer[2] = (byte)(value >> 16);
			this.buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this.buffer, 0, 4);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002B5D4 File Offset: 0x000297D4
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			int i = 0;
			int num = 0;
			while (i < 8)
			{
				this.buffer[i] = (byte)(value >> num);
				i++;
				num += 8;
			}
			this.OutStream.Write(this.buffer, 0, 8);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002B638 File Offset: 0x00029838
		protected void Write7BitEncodedInt(int value)
		{
			do
			{
				int num = value >> 7 & 33554431;
				byte b = (byte)(value & 127);
				if (num != 0)
				{
					b |= 128;
				}
				this.Write(b);
				value = num;
			}
			while (value != 0);
		}

		// Token: 0x0400046F RID: 1135
		public static readonly BinaryWriter Null = new BinaryWriter();

		// Token: 0x04000470 RID: 1136
		protected Stream OutStream;

		// Token: 0x04000471 RID: 1137
		private Encoding m_encoding;

		// Token: 0x04000472 RID: 1138
		private byte[] buffer;

		// Token: 0x04000473 RID: 1139
		private bool disposed;

		// Token: 0x04000474 RID: 1140
		private byte[] stringBuffer;

		// Token: 0x04000475 RID: 1141
		private int maxCharsPerRound;
	}
}
