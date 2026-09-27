using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200012D RID: 301
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class MemoryStream : Stream
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x0002D8CC File Offset: 0x0002BACC
		public MemoryStream() : this(0)
		{
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002D8D8 File Offset: 0x0002BAD8
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.canWrite = true;
			this.capacity = capacity;
			this.internalBuffer = new byte[capacity];
			this.expandable = true;
			this.allowGetBuffer = true;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002D928 File Offset: 0x0002BB28
		public MemoryStream(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.InternalConstructor(buffer, 0, buffer.Length, true, false);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002D950 File Offset: 0x0002BB50
		private void InternalConstructor(byte[] buffer, int index, int count, bool writable, bool publicallyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("index or count is less than 0.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("index+count", "The size of the buffer is less than index + count.");
			}
			this.canWrite = writable;
			this.internalBuffer = buffer;
			this.capacity = count + index;
			this.length = this.capacity;
			this.position = index;
			this.initialIndex = index;
			this.allowGetBuffer = publicallyVisible;
			this.expandable = false;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
		private void CheckIfClosedThrowDisposed()
		{
			if (this.streamClosed)
			{
				throw new ObjectDisposedException("MemoryStream");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002D9FC File Offset: 0x0002BBFC
		public override bool CanRead
		{
			get
			{
				return !this.streamClosed;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002DA08 File Offset: 0x0002BC08
		public override bool CanSeek
		{
			get
			{
				return !this.streamClosed;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002DA14 File Offset: 0x0002BC14
		public override bool CanWrite
		{
			get
			{
				return !this.streamClosed && this.canWrite;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0002DA2C File Offset: 0x0002BC2C
		public virtual int Capacity
		{
			set
			{
				this.CheckIfClosedThrowDisposed();
				if (value == this.capacity)
				{
					return;
				}
				if (!this.expandable)
				{
					throw new NotSupportedException("Cannot expand this MemoryStream");
				}
				if (value < 0 || value < this.length)
				{
					throw new ArgumentOutOfRangeException("value", string.Concat(new object[]
					{
						"New capacity cannot be negative or less than the current capacity ",
						value,
						" ",
						this.capacity
					}));
				}
				byte[] dst = null;
				if (value != 0)
				{
					dst = new byte[value];
					Buffer.BlockCopy(this.internalBuffer, 0, dst, 0, this.length);
				}
				this.dirty_bytes = 0;
				this.internalBuffer = dst;
				this.capacity = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002DAEC File Offset: 0x0002BCEC
		public override long Length
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return (long)(this.length - this.initialIndex);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002DB04 File Offset: 0x0002BD04
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0002DB1C File Offset: 0x0002BD1C
		public override long Position
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return (long)(this.position - this.initialIndex);
			}
			set
			{
				this.CheckIfClosedThrowDisposed();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Position cannot be negative");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "Position must be non-negative and less than 2^31 - 1 - origin");
				}
				this.position = this.initialIndex + (int)value;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002DB74 File Offset: 0x0002BD74
		protected override void Dispose(bool disposing)
		{
			this.streamClosed = true;
			this.expandable = false;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002DB84 File Offset: 0x0002BD84
		public override void Flush()
		{
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002DB88 File Offset: 0x0002BD88
		public virtual byte[] GetBuffer()
		{
			if (!this.allowGetBuffer)
			{
				throw new UnauthorizedAccessException();
			}
			return this.internalBuffer;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckIfClosedThrowDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.position >= this.length || count == 0)
			{
				return 0;
			}
			if (this.position > this.length - count)
			{
				count = this.length - this.position;
			}
			Buffer.BlockCopy(this.internalBuffer, this.position, buffer, offset, count);
			this.position += count;
			return count;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		public override int ReadByte()
		{
			this.CheckIfClosedThrowDisposed();
			if (this.position >= this.length)
			{
				return -1;
			}
			return (int)this.internalBuffer[this.position++];
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002DC9C File Offset: 0x0002BE9C
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.CheckIfClosedThrowDisposed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("Offset out of range. " + offset);
			}
			int num;
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				num = this.initialIndex;
				break;
			case SeekOrigin.Current:
				num = this.position;
				break;
			case SeekOrigin.End:
				num = this.length;
				break;
			default:
				throw new ArgumentException("loc", "Invalid SeekOrigin");
			}
			num += (int)offset;
			if (num < this.initialIndex)
			{
				throw new IOException("Attempted to seek before start of MemoryStream.");
			}
			this.position = num;
			return (long)this.position;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002DD5C File Offset: 0x0002BF5C
		private int CalculateNewCapacity(int minimum)
		{
			if (minimum < 256)
			{
				minimum = 256;
			}
			if (minimum < this.capacity * 2)
			{
				minimum = this.capacity * 2;
			}
			return minimum;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		private void Expand(int newSize)
		{
			if (newSize > this.capacity)
			{
				this.Capacity = this.CalculateNewCapacity(newSize);
			}
			else if (this.dirty_bytes > 0)
			{
				Array.Clear(this.internalBuffer, this.length, this.dirty_bytes);
				this.dirty_bytes = 0;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002DDE4 File Offset: 0x0002BFE4
		public override void SetLength(long value)
		{
			if (!this.expandable && value > (long)this.capacity)
			{
				throw new NotSupportedException("Expanding this MemoryStream is not supported");
			}
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException(Locale.GetText("Cannot write to this MemoryStream"));
			}
			if (value < 0L || value + (long)this.initialIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = (int)value + this.initialIndex;
			if (num > this.length)
			{
				this.Expand(num);
			}
			else if (num < this.length)
			{
				this.dirty_bytes += this.length - num;
			}
			this.length = num;
			if (this.position > this.length)
			{
				this.position = this.length;
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002DEC0 File Offset: 0x0002C0C0
		public virtual byte[] ToArray()
		{
			int num = this.length - this.initialIndex;
			byte[] array = new byte[num];
			if (this.internalBuffer != null)
			{
				Buffer.BlockCopy(this.internalBuffer, this.initialIndex, array, 0, num);
			}
			return array;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002DF04 File Offset: 0x0002C104
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this stream.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.position > this.length - count)
			{
				this.Expand(this.position + count);
			}
			Buffer.BlockCopy(buffer, offset, this.internalBuffer, this.position, count);
			this.position += count;
			if (this.position >= this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002DFD0 File Offset: 0x0002C1D0
		public override void WriteByte(byte value)
		{
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this stream.");
			}
			if (this.position >= this.length)
			{
				this.Expand(this.position + 1);
				this.length = this.position + 1;
			}
			this.internalBuffer[this.position++] = value;
		}

		// Token: 0x040004C6 RID: 1222
		private bool canWrite;

		// Token: 0x040004C7 RID: 1223
		private bool allowGetBuffer;

		// Token: 0x040004C8 RID: 1224
		private int capacity;

		// Token: 0x040004C9 RID: 1225
		private int length;

		// Token: 0x040004CA RID: 1226
		private byte[] internalBuffer;

		// Token: 0x040004CB RID: 1227
		private int initialIndex;

		// Token: 0x040004CC RID: 1228
		private bool expandable;

		// Token: 0x040004CD RID: 1229
		private bool streamClosed;

		// Token: 0x040004CE RID: 1230
		private int position;

		// Token: 0x040004CF RID: 1231
		private int dirty_bytes;
	}
}
