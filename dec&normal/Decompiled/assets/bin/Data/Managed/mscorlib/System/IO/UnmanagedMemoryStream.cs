using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000148 RID: 328
	[CLSCompliant(false)]
	public class UnmanagedMemoryStream : Stream
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x00031638 File Offset: 0x0002F838
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000CBF RID: 3263 RVA: 0x0003164C File Offset: 0x0002F84C
		// (remove) Token: 0x06000CC0 RID: 3264 RVA: 0x00031668 File Offset: 0x0002F868
		internal event EventHandler Closed;

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00031684 File Offset: 0x0002F884
		public override bool CanRead
		{
			get
			{
				return !this.closed && this.fileaccess != FileAccess.Write;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000316A0 File Offset: 0x0002F8A0
		public override bool CanSeek
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000316AC File Offset: 0x0002F8AC
		public override bool CanWrite
		{
			get
			{
				return !this.closed && this.fileaccess != FileAccess.Read;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public override long Length
		{
			get
			{
				if (this.closed)
				{
					throw new ObjectDisposedException("The stream is closed");
				}
				return this.length;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000316E8 File Offset: 0x0002F8E8
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00031708 File Offset: 0x0002F908
		public override long Position
		{
			get
			{
				if (this.closed)
				{
					throw new ObjectDisposedException("The stream is closed");
				}
				return this.current_position;
			}
			set
			{
				if (this.closed)
				{
					throw new ObjectDisposedException("The stream is closed");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "The position is larger than Int32.MaxValue.");
				}
				this.current_position = value;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00031768 File Offset: 0x0002F968
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("The length of the buffer array minus the offset parameter is less than the count parameter");
			}
			if (this.fileaccess == FileAccess.Write)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.current_position >= this.length)
			{
				return 0;
			}
			int num = (this.current_position + (long)count >= this.length) ? ((int)(this.length - this.current_position)) : count;
			Marshal.Copy(new IntPtr(this.initial_pointer.ToInt64() + this.current_position), buffer, offset, num);
			this.current_position += (long)num;
			return num;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00031864 File Offset: 0x0002FA64
		public override int ReadByte()
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
			if (this.fileaccess == FileAccess.Write)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.current_position >= this.length)
			{
				return -1;
			}
			IntPtr ptr = this.initial_pointer;
			long num;
			this.current_position = (num = this.current_position) + 1L;
			return (int)Marshal.ReadByte(ptr, (int)num);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000318D0 File Offset: 0x0002FAD0
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
			long num;
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("An attempt was made to seek before the beginning of the stream");
				}
				num = this.initial_position;
				break;
			case SeekOrigin.Current:
				num = this.current_position;
				break;
			case SeekOrigin.End:
				num = this.length;
				break;
			default:
				throw new ArgumentException("Invalid SeekOrigin option");
			}
			num += offset;
			if (num < this.initial_position)
			{
				throw new IOException("An attempt was made to seek before the beginning of the stream");
			}
			this.current_position = num;
			return this.current_position;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00031978 File Offset: 0x0002FB78
		public override void SetLength(long value)
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (value > this.capacity)
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
			if (this.fileaccess == FileAccess.Read)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			this.length = value;
			if (this.length < this.current_position)
			{
				this.current_position = this.length;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00031A08 File Offset: 0x0002FC08
		public override void Flush()
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00031A20 File Offset: 0x0002FC20
		protected override void Dispose(bool disposing)
		{
			if (this.closed)
			{
				return;
			}
			this.closed = true;
			if (this.Closed != null)
			{
				this.Closed(this, null);
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00031A50 File Offset: 0x0002FC50
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("The buffer parameter is a null reference");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("The length of the buffer array minus the offset parameter is less than the count parameter");
			}
			if (this.current_position > this.capacity - (long)count)
			{
				throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
			}
			if (this.fileaccess == FileAccess.Read)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			for (int i = 0; i < count; i++)
			{
				IntPtr ptr = this.initial_pointer;
				long num;
				this.current_position = (num = this.current_position) + 1L;
				Marshal.WriteByte(ptr, (int)num, buffer[offset + i]);
			}
			if (this.current_position > this.length)
			{
				this.length = this.current_position;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00031B50 File Offset: 0x0002FD50
		public override void WriteByte(byte value)
		{
			if (this.closed)
			{
				throw new ObjectDisposedException("The stream is closed");
			}
			if (this.current_position == this.capacity)
			{
				throw new NotSupportedException("The current position is at the end of the capacity of the stream");
			}
			if (this.fileaccess == FileAccess.Read)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			Marshal.WriteByte(this.initial_pointer, (int)this.current_position, value);
			this.current_position += 1L;
			if (this.current_position > this.length)
			{
				this.length = this.current_position;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", "The length cannot be greater than the capacity.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			this.fileaccess = access;
			this.length = length;
			this.capacity = capacity;
			this.initial_position = 0L;
			this.current_position = this.initial_position;
			this.initial_pointer = new IntPtr((void*)pointer);
			this.closed = false;
		}

		// Token: 0x04000542 RID: 1346
		private long length;

		// Token: 0x04000543 RID: 1347
		private bool closed;

		// Token: 0x04000544 RID: 1348
		private long capacity;

		// Token: 0x04000545 RID: 1349
		private FileAccess fileaccess;

		// Token: 0x04000546 RID: 1350
		private IntPtr initial_pointer;

		// Token: 0x04000547 RID: 1351
		private long initial_position;

		// Token: 0x04000548 RID: 1352
		private long current_position;
	}
}
