using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200013B RID: 315
	[ComVisible(true)]
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
		internal StreamReader()
		{
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		public StreamReader(Stream stream) : this(stream, Encoding.UTF8Unmarked, true, 1024)
		{
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002F6CC File Offset: 0x0002D8CC
		public StreamReader(Stream stream, Encoding encoding) : this(stream, encoding, true, 1024)
		{
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002F6DC File Offset: 0x0002D8DC
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			this.Initialize(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
		public StreamReader(string path) : this(path, Encoding.UTF8Unmarked, true, 4096)
		{
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002F704 File Offset: 0x0002D904
		public StreamReader(string path, Encoding encoding) : this(path, encoding, true, 4096)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002F714 File Offset: 0x0002D914
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (string.Empty == path)
			{
				throw new ArgumentException("Empty path not allowed");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("path contains invalid characters");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "The minimum size of the buffer must be positive");
			}
			Stream stream = File.OpenRead(path);
			this.Initialize(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002F7B8 File Offset: 0x0002D9B8
		internal void Initialize(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Cannot read stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "The minimum size of the buffer must be positive");
			}
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.base_stream = stream;
			this.input_buffer = new byte[bufferSize];
			this.buffer_size = bufferSize;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			byte[] preamble = encoding.GetPreamble();
			this.do_checks = ((!detectEncodingFromByteOrderMarks) ? 0 : 1);
			this.do_checks += ((preamble.Length != 0) ? 2 : 0);
			this.decoded_buffer = new char[encoding.GetMaxCharCount(bufferSize) + 1];
			this.decoded_count = 0;
			this.pos = 0;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
		public bool EndOfStream
		{
			get
			{
				return this.Peek() < 0;
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002F8BC File Offset: 0x0002DABC
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002F8C8 File Offset: 0x0002DAC8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.base_stream != null)
			{
				this.base_stream.Close();
			}
			this.input_buffer = null;
			this.decoded_buffer = null;
			this.encoding = null;
			this.decoder = null;
			this.base_stream = null;
			base.Dispose(disposing);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002F91C File Offset: 0x0002DB1C
		private int DoChecks(int count)
		{
			if ((this.do_checks & 2) == 2)
			{
				byte[] preamble = this.encoding.GetPreamble();
				int num = preamble.Length;
				if (count >= num)
				{
					int i;
					for (i = 0; i < num; i++)
					{
						if (this.input_buffer[i] != preamble[i])
						{
							break;
						}
					}
					if (i == num)
					{
						return i;
					}
				}
			}
			if ((this.do_checks & 1) == 1)
			{
				if (count < 2)
				{
					return 0;
				}
				if (this.input_buffer[0] == 254 && this.input_buffer[1] == 255)
				{
					this.encoding = Encoding.BigEndianUnicode;
					return 2;
				}
				if (count < 3)
				{
					return 0;
				}
				if (this.input_buffer[0] == 239 && this.input_buffer[1] == 187 && this.input_buffer[2] == 191)
				{
					this.encoding = Encoding.UTF8Unmarked;
					return 3;
				}
				if (count < 4)
				{
					if (this.input_buffer[0] == 255 && this.input_buffer[1] == 254 && this.input_buffer[2] != 0)
					{
						this.encoding = Encoding.Unicode;
						return 2;
					}
					return 0;
				}
				else
				{
					if (this.input_buffer[0] == 0 && this.input_buffer[1] == 0 && this.input_buffer[2] == 254 && this.input_buffer[3] == 255)
					{
						this.encoding = Encoding.BigEndianUTF32;
						return 4;
					}
					if (this.input_buffer[0] == 255 && this.input_buffer[1] == 254)
					{
						if (this.input_buffer[2] == 0 && this.input_buffer[3] == 0)
						{
							this.encoding = Encoding.UTF32;
							return 4;
						}
						this.encoding = Encoding.Unicode;
						return 2;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002FB00 File Offset: 0x0002DD00
		private int ReadBuffer()
		{
			this.pos = 0;
			this.decoded_count = 0;
			int num = 0;
			for (;;)
			{
				int num2 = this.base_stream.Read(this.input_buffer, 0, this.buffer_size);
				if (num2 <= 0)
				{
					break;
				}
				this.mayBlock = (num2 < this.buffer_size);
				if (this.do_checks > 0)
				{
					Encoding encoding = this.encoding;
					num = this.DoChecks(num2);
					if (encoding != this.encoding)
					{
						int num3 = encoding.GetMaxCharCount(this.buffer_size) + 1;
						int num4 = this.encoding.GetMaxCharCount(this.buffer_size) + 1;
						if (num3 != num4)
						{
							this.decoded_buffer = new char[num4];
						}
						this.decoder = this.encoding.GetDecoder();
					}
					this.do_checks = 0;
					num2 -= num;
				}
				this.decoded_count += this.decoder.GetChars(this.input_buffer, num, num2, this.decoded_buffer, 0);
				num = 0;
				if (this.decoded_count != 0)
				{
					goto Block_5;
				}
			}
			return 0;
			Block_5:
			return this.decoded_count;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002FC08 File Offset: 0x0002DE08
		public override int Peek()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return -1;
			}
			return (int)this.decoded_buffer[this.pos];
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public override int Read()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return -1;
			}
			return (int)this.decoded_buffer[this.pos++];
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002FCBC File Offset: 0x0002DEBC
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
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
			int num = 0;
			while (count > 0)
			{
				if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
				{
					return (num <= 0) ? 0 : num;
				}
				int num2 = Math.Min(this.decoded_count - this.pos, count);
				Array.Copy(this.decoded_buffer, this.pos, buffer, index, num2);
				this.pos += num2;
				index += num2;
				count -= num2;
				num += num2;
				if (this.mayBlock)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002FDC8 File Offset: 0x0002DFC8
		private int FindNextEOL()
		{
			while (this.pos < this.decoded_count)
			{
				char c = this.decoded_buffer[this.pos];
				if (c == '\n')
				{
					this.pos++;
					int num = (!this.foundCR) ? (this.pos - 1) : (this.pos - 2);
					if (num < 0)
					{
						num = 0;
					}
					this.foundCR = false;
					return num;
				}
				if (this.foundCR)
				{
					this.foundCR = false;
					if (this.pos == 0)
					{
						return -2;
					}
					return this.pos - 1;
				}
				else
				{
					this.foundCR = (c == '\r');
					this.pos++;
				}
			}
			return -1;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002FE88 File Offset: 0x0002E088
		public override string ReadLine()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return null;
			}
			int num = this.pos;
			int num2 = this.FindNextEOL();
			if (num2 < this.decoded_count && num2 >= num)
			{
				return new string(this.decoded_buffer, num, num2 - num);
			}
			if (num2 == -2)
			{
				return this.line_builder.ToString(0, this.line_builder.Length);
			}
			if (this.line_builder == null)
			{
				this.line_builder = new StringBuilder();
			}
			else
			{
				this.line_builder.Length = 0;
			}
			for (;;)
			{
				if (this.foundCR)
				{
					this.decoded_count--;
				}
				this.line_builder.Append(this.decoded_buffer, num, this.decoded_count - num);
				if (this.ReadBuffer() == 0)
				{
					break;
				}
				num = this.pos;
				num2 = this.FindNextEOL();
				if (num2 < this.decoded_count && num2 >= num)
				{
					goto Block_12;
				}
				if (num2 == -2)
				{
					goto Block_14;
				}
			}
			if (this.line_builder.Capacity > 32768)
			{
				StringBuilder stringBuilder = this.line_builder;
				this.line_builder = null;
				return stringBuilder.ToString(0, stringBuilder.Length);
			}
			return this.line_builder.ToString(0, this.line_builder.Length);
			Block_12:
			this.line_builder.Append(this.decoded_buffer, num, num2 - num);
			if (this.line_builder.Capacity > 32768)
			{
				StringBuilder stringBuilder2 = this.line_builder;
				this.line_builder = null;
				return stringBuilder2.ToString(0, stringBuilder2.Length);
			}
			return this.line_builder.ToString(0, this.line_builder.Length);
			Block_14:
			return this.line_builder.ToString(0, this.line_builder.Length);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00030074 File Offset: 0x0002E274
		public override string ReadToEnd()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.decoded_buffer.Length;
			char[] array = new char[num];
			int charCount;
			while ((charCount = this.Read(array, 0, num)) > 0)
			{
				stringBuilder.Append(array, 0, charCount);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000517 RID: 1303
		private const int DefaultBufferSize = 1024;

		// Token: 0x04000518 RID: 1304
		private const int DefaultFileBufferSize = 4096;

		// Token: 0x04000519 RID: 1305
		private const int MinimumBufferSize = 128;

		// Token: 0x0400051A RID: 1306
		private byte[] input_buffer;

		// Token: 0x0400051B RID: 1307
		private char[] decoded_buffer;

		// Token: 0x0400051C RID: 1308
		private int decoded_count;

		// Token: 0x0400051D RID: 1309
		private int pos;

		// Token: 0x0400051E RID: 1310
		private int buffer_size;

		// Token: 0x0400051F RID: 1311
		private int do_checks;

		// Token: 0x04000520 RID: 1312
		private Encoding encoding;

		// Token: 0x04000521 RID: 1313
		private Decoder decoder;

		// Token: 0x04000522 RID: 1314
		private Stream base_stream;

		// Token: 0x04000523 RID: 1315
		private bool mayBlock;

		// Token: 0x04000524 RID: 1316
		private StringBuilder line_builder;

		// Token: 0x04000525 RID: 1317
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x04000526 RID: 1318
		private bool foundCR;

		// Token: 0x0200013C RID: 316
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06000C4A RID: 3146 RVA: 0x000300E0 File Offset: 0x0002E2E0
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x000300E4 File Offset: 0x0002E2E4
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x000300E8 File Offset: 0x0002E2E8
			public override int Read([In] [Out] char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x000300EC File Offset: 0x0002E2EC
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x000300F0 File Offset: 0x0002E2F0
			public override string ReadToEnd()
			{
				return string.Empty;
			}
		}
	}
}
