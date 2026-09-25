using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200013D RID: 317
	[ComVisible(true)]
	[Serializable]
	public class StreamWriter : TextWriter
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x000300F8 File Offset: 0x0002E2F8
		public StreamWriter(Stream stream) : this(stream, Encoding.UTF8Unmarked, 1024)
		{
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0003010C File Offset: 0x0002E30C
		public StreamWriter(Stream stream, Encoding encoding) : this(stream, encoding, 1024)
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0003011C File Offset: 0x0002E31C
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Can not write to stream");
			}
			this.internalStream = stream;
			this.Initialize(encoding, bufferSize);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00030188 File Offset: 0x0002E388
		public StreamWriter(string path) : this(path, false, Encoding.UTF8Unmarked, 4096)
		{
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0003019C File Offset: 0x0002E39C
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			FileMode mode;
			if (append)
			{
				mode = FileMode.Append;
			}
			else
			{
				mode = FileMode.Create;
			}
			this.internalStream = new FileStream(path, mode, FileAccess.Write, FileShare.Read);
			if (append)
			{
				this.internalStream.Position = this.internalStream.Length;
			}
			else
			{
				this.internalStream.SetLength(0L);
			}
			this.Initialize(encoding, bufferSize);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00030240 File Offset: 0x0002E440
		internal void Initialize(Encoding encoding, int bufferSize)
		{
			this.internalEncoding = encoding;
			this.decode_pos = (this.byte_pos = 0);
			int num = Math.Max(bufferSize, 256);
			this.decode_buf = new char[num];
			this.byte_buf = new byte[encoding.GetMaxByteCount(num)];
			if (this.internalStream.CanSeek && this.internalStream.Position > 0L)
			{
				this.preamble_done = true;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x000302B8 File Offset: 0x0002E4B8
		public virtual bool AutoFlush
		{
			set
			{
				this.iflush = value;
				if (this.iflush)
				{
					this.Flush();
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000302D4 File Offset: 0x0002E4D4
		public override Encoding Encoding
		{
			get
			{
				return this.internalEncoding;
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000302DC File Offset: 0x0002E4DC
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (!this.DisposedAlready && disposing && this.internalStream != null)
			{
				try
				{
					this.Flush();
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				this.DisposedAlready = true;
				try
				{
					this.internalStream.Close();
				}
				catch (Exception ex3)
				{
					if (ex == null)
					{
						ex = ex3;
					}
				}
			}
			this.internalStream = null;
			this.byte_buf = null;
			this.internalEncoding = null;
			this.decode_buf = null;
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00030380 File Offset: 0x0002E580
		public override void Flush()
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			this.Decode();
			if (this.byte_pos > 0)
			{
				this.FlushBytes();
				this.internalStream.Flush();
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000303BC File Offset: 0x0002E5BC
		private void FlushBytes()
		{
			if (!this.preamble_done && this.byte_pos > 0)
			{
				byte[] preamble = this.internalEncoding.GetPreamble();
				if (preamble.Length > 0)
				{
					this.internalStream.Write(preamble, 0, preamble.Length);
				}
				this.preamble_done = true;
			}
			this.internalStream.Write(this.byte_buf, 0, this.byte_pos);
			this.byte_pos = 0;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0003042C File Offset: 0x0002E62C
		private void Decode()
		{
			if (this.byte_pos > 0)
			{
				this.FlushBytes();
			}
			if (this.decode_pos > 0)
			{
				int bytes = this.internalEncoding.GetBytes(this.decode_buf, 0, this.decode_pos, this.byte_buf, this.byte_pos);
				this.byte_pos += bytes;
				this.decode_pos = 0;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00030494 File Offset: 0x0002E694
		public override void Write(char[] buffer, int index, int count)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > buffer.Length - count)
			{
				throw new ArgumentException("index + count > buffer.Length");
			}
			this.LowLevelWrite(buffer, index, count);
			if (this.iflush)
			{
				this.Flush();
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00030528 File Offset: 0x0002E728
		private void LowLevelWrite(char[] buffer, int index, int count)
		{
			while (count > 0)
			{
				int num = this.decode_buf.Length - this.decode_pos;
				if (num == 0)
				{
					this.Decode();
					num = this.decode_buf.Length;
				}
				if (num > count)
				{
					num = count;
				}
				Buffer.BlockCopy(buffer, index * 2, this.decode_buf, this.decode_pos * 2, num * 2);
				count -= num;
				index += num;
				this.decode_pos += num;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000305A4 File Offset: 0x0002E7A4
		private void LowLevelWrite(string s)
		{
			int i = s.Length;
			int num = 0;
			while (i > 0)
			{
				int num2 = this.decode_buf.Length - this.decode_pos;
				if (num2 == 0)
				{
					this.Decode();
					num2 = this.decode_buf.Length;
				}
				if (num2 > i)
				{
					num2 = i;
				}
				for (int j = 0; j < num2; j++)
				{
					this.decode_buf[j + this.decode_pos] = s[j + num];
				}
				i -= num2;
				num += num2;
				this.decode_pos += num2;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00030634 File Offset: 0x0002E834
		public override void Write(char value)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (this.decode_pos >= this.decode_buf.Length)
			{
				this.Decode();
			}
			this.decode_buf[this.decode_pos++] = value;
			if (this.iflush)
			{
				this.Flush();
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0003069C File Offset: 0x0002E89C
		public override void Write(char[] buffer)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (buffer != null)
			{
				this.LowLevelWrite(buffer, 0, buffer.Length);
			}
			if (this.iflush)
			{
				this.Flush();
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000306D8 File Offset: 0x0002E8D8
		public override void Write(string value)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (value != null)
			{
				this.LowLevelWrite(value);
			}
			if (this.iflush)
			{
				this.Flush();
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00030710 File Offset: 0x0002E910
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0003071C File Offset: 0x0002E91C
		~StreamWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x04000527 RID: 1319
		private const int DefaultBufferSize = 1024;

		// Token: 0x04000528 RID: 1320
		private const int DefaultFileBufferSize = 4096;

		// Token: 0x04000529 RID: 1321
		private const int MinimumBufferSize = 256;

		// Token: 0x0400052A RID: 1322
		private Encoding internalEncoding;

		// Token: 0x0400052B RID: 1323
		private Stream internalStream;

		// Token: 0x0400052C RID: 1324
		private bool iflush;

		// Token: 0x0400052D RID: 1325
		private byte[] byte_buf;

		// Token: 0x0400052E RID: 1326
		private int byte_pos;

		// Token: 0x0400052F RID: 1327
		private char[] decode_buf;

		// Token: 0x04000530 RID: 1328
		private int decode_pos;

		// Token: 0x04000531 RID: 1329
		private bool DisposedAlready;

		// Token: 0x04000532 RID: 1330
		private bool preamble_done;

		// Token: 0x04000533 RID: 1331
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, Encoding.UTF8Unmarked, 1);
	}
}
