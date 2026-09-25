using System;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000126 RID: 294
	[ComVisible(true)]
	public class FileStream : Stream
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x0002C344 File Offset: 0x0002A544
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync) : this(handle, access, ownsHandle, bufferSize, isAsync, false)
		{
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002C354 File Offset: 0x0002A554
		internal FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool noBuffering)
		{
			this.name = "[Unknown]";
			base..ctor();
			this.handle = MonoIO.InvalidHandle;
			if (handle == this.handle)
			{
				throw new ArgumentException("handle", Locale.GetText("Invalid."));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			MonoIOError monoIOError;
			MonoFileType fileType = MonoIO.GetFileType(handle, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.name, monoIOError);
			}
			if (fileType == MonoFileType.Unknown)
			{
				throw new IOException("Invalid handle.");
			}
			if (fileType == MonoFileType.Disk)
			{
				this.canseek = true;
			}
			else
			{
				this.canseek = false;
			}
			this.handle = handle;
			this.access = access;
			this.owner = ownsHandle;
			this.async = isAsync;
			this.anonymous = false;
			this.InitBuffer(bufferSize, noBuffering);
			if (this.canseek)
			{
				this.buf_start = MonoIO.Seek(handle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.name, monoIOError);
				}
			}
			this.append_startpos = 0L;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002C468 File Offset: 0x0002A668
		public FileStream(string path, FileMode mode) : this(path, mode, (mode != FileMode.Append) ? FileAccess.ReadWrite : FileAccess.Write, FileShare.Read, 8192, false, FileOptions.None)
		{
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002C494 File Offset: 0x0002A694
		public FileStream(string path, FileMode mode, FileAccess access) : this(path, mode, access, (access != FileAccess.Write) ? FileShare.Read : FileShare.None, 8192, false, false)
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share) : this(path, mode, access, share, 8192, false, FileOptions.None)
		{
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : this(path, mode, access, share, bufferSize, false, FileOptions.None)
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002C4FC File Offset: 0x0002A6FC
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool isAsync, bool anonymous) : this(path, mode, access, share, bufferSize, anonymous, (!isAsync) ? FileOptions.None : FileOptions.Asynchronous)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002C52C File Offset: 0x0002A72C
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool anonymous, FileOptions options)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			share &= ~FileShare.Inheritable;
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				if (anonymous)
				{
					throw new ArgumentException("mode", "Enum value was out of legal range.");
				}
				throw new ArgumentOutOfRangeException("mode", "Enum value was out of legal range.");
			}
			else if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				if (anonymous)
				{
					throw new IsolatedStorageException("Enum value for FileAccess was out of legal range.");
				}
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			else if (share < FileShare.None || share > (FileShare.Read | FileShare.Write | FileShare.Delete))
			{
				if (anonymous)
				{
					throw new IsolatedStorageException("Enum value for FileShare was out of legal range.");
				}
				throw new ArgumentOutOfRangeException("share", "Enum value was out of legal range.");
			}
			else
			{
				if (path.IndexOfAny(Path.InvalidPathChars) != -1)
				{
					throw new ArgumentException("Name has invalid chars");
				}
				if (Directory.Exists(path))
				{
					string text = Locale.GetText("Access to the path '{0}' is denied.");
					throw new UnauthorizedAccessException(string.Format(text, this.GetSecureFileName(path, false)));
				}
				if (mode == FileMode.Append && (access & FileAccess.Read) == FileAccess.Read)
				{
					throw new ArgumentException("Append access can be requested only in write-only mode.");
				}
				if ((access & FileAccess.Write) == (FileAccess)0 && mode != FileMode.Open && mode != FileMode.OpenOrCreate)
				{
					string text2 = Locale.GetText("Combining FileMode: {0} with FileAccess: {1} is invalid.");
					throw new ArgumentException(string.Format(text2, access, mode));
				}
				string directoryName;
				if (Path.DirectorySeparatorChar != '/' && path.IndexOf('/') >= 0)
				{
					directoryName = Path.GetDirectoryName(Path.GetFullPath(path));
				}
				else
				{
					directoryName = Path.GetDirectoryName(path);
				}
				if (directoryName.Length > 0)
				{
					string fullPath = Path.GetFullPath(directoryName);
					if (!Directory.Exists(fullPath))
					{
						string text3 = Locale.GetText("Could not find a part of the path \"{0}\".");
						string arg = (!anonymous) ? Path.GetFullPath(path) : directoryName;
						throw new IsolatedStorageException(string.Format(text3, arg));
					}
				}
				if (access == FileAccess.Read && mode != FileMode.Create && mode != FileMode.OpenOrCreate && mode != FileMode.CreateNew && !File.Exists(path))
				{
					string text4 = Locale.GetText("Could not find file \"{0}\".");
					string secureFileName = this.GetSecureFileName(path);
					throw new IsolatedStorageException(string.Format(text4, secureFileName));
				}
				if (!anonymous)
				{
					this.name = path;
				}
				MonoIOError error;
				this.handle = MonoIO.Open(path, mode, access, share, options, out error);
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw MonoIO.GetException(this.GetSecureFileName(path), error);
				}
				this.access = access;
				this.owner = true;
				this.anonymous = anonymous;
				if (MonoIO.GetFileType(this.handle, out error) == MonoFileType.Disk)
				{
					this.canseek = true;
					this.async = ((options & FileOptions.Asynchronous) != FileOptions.None);
				}
				else
				{
					this.canseek = false;
					this.async = false;
				}
				if (access == FileAccess.Read && this.canseek && bufferSize == 8192)
				{
					long length = this.Length;
					if ((long)bufferSize > length)
					{
						bufferSize = (int)((length >= 1000L) ? length : 1000L);
					}
				}
				this.InitBuffer(bufferSize, false);
				if (mode == FileMode.Append)
				{
					this.Seek(0L, SeekOrigin.End);
					this.append_startpos = this.Position;
				}
				else
				{
					this.append_startpos = 0L;
				}
				return;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.Read || this.access == FileAccess.ReadWrite;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.Write || this.access == FileAccess.ReadWrite;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002C8E4 File Offset: 0x0002AAE4
		public override bool CanSeek
		{
			get
			{
				return this.canseek;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002C8EC File Offset: 0x0002AAEC
		public override long Length
		{
			get
			{
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				this.FlushBufferIfDirty();
				MonoIOError monoIOError;
				long length = MonoIO.GetLength(this.handle, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return length;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002C960 File Offset: 0x0002AB60
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		public override long Position
		{
			get
			{
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				return this.buf_start + (long)this.buf_offset;
			}
			set
			{
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Attempt to set the position to a negative value");
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002CA14 File Offset: 0x0002AC14
		public override int ReadByte()
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.buf_size != 0)
			{
				if (this.buf_offset >= this.buf_length)
				{
					this.RefillBuffer();
					if (this.buf_length == 0)
					{
						return -1;
					}
				}
				return (int)this.buf[this.buf_offset++];
			}
			if (this.ReadData(this.handle, this.buf, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.buf[0];
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		public override void WriteByte(byte value)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.buf_offset == this.buf_size)
			{
				this.FlushBuffer();
			}
			if (this.buf_size == 0)
			{
				this.buf[0] = value;
				this.buf_dirty = true;
				this.buf_length = 1;
				this.FlushBuffer();
				return;
			}
			this.buf[this.buf_offset++] = value;
			if (this.buf_offset > this.buf_length)
			{
				this.buf_length = this.buf_offset;
			}
			this.buf_dirty = true;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002CB84 File Offset: 0x0002AD84
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			int num = array.Length;
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginRead(array, offset, count, null, null);
				return this.EndRead(asyncResult);
			}
			return this.ReadInternal(array, offset, count);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002CC58 File Offset: 0x0002AE58
		private int ReadInternal(byte[] dest, int offset, int count)
		{
			int num = 0;
			int num2 = this.ReadSegment(dest, offset, count);
			num += num2;
			count -= num2;
			if (count == 0)
			{
				return num;
			}
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				num2 = this.ReadData(this.handle, dest, offset + num, count);
				this.buf_start += (long)num2;
			}
			else
			{
				this.RefillBuffer();
				num2 = this.ReadSegment(dest, offset + num, count);
			}
			return num + num2;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002CCD4 File Offset: 0x0002AED4
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("Buffer too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
			}
			FileStream.ReadDelegate readDelegate = new FileStream.ReadDelegate(this.ReadInternal);
			return readDelegate.BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				return base.EndRead(asyncResult);
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002CE18 File Offset: 0x0002B018
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > array.Length - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginWrite(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				return;
			}
			this.WriteInternal(array, offset, count);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		private void WriteInternal(byte[] src, int offset, int count)
		{
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				int i = count;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num = MonoIO.Write(this.handle, src, offset, i, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num;
					offset += num;
				}
				this.buf_start += (long)count;
			}
			else
			{
				int num2 = 0;
				while (count > 0)
				{
					int num3 = this.WriteSegment(src, offset + num2, count);
					num2 += num3;
					count -= num3;
					if (count == 0)
					{
						break;
					}
					this.FlushBuffer();
				}
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002CF84 File Offset: 0x0002B184
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("array too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
			}
			FileStreamAsyncResult fileStreamAsyncResult = new FileStreamAsyncResult(userCallback, stateObject);
			fileStreamAsyncResult.BytesRead = -1;
			fileStreamAsyncResult.Count = numBytes;
			fileStreamAsyncResult.OriginalCount = numBytes;
			if (this.buf_dirty)
			{
				MemoryStream memoryStream = new MemoryStream();
				this.FlushBuffer(memoryStream);
				memoryStream.Write(array, offset, numBytes);
				offset = 0;
				numBytes = (int)memoryStream.Length;
			}
			FileStream.WriteDelegate writeDelegate = new FileStream.WriteDelegate(this.WriteInternal);
			return writeDelegate.BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D09C File Offset: 0x0002B29C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				base.EndWrite(asyncResult);
				return;
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D114 File Offset: 0x0002B314
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin", "Invalid SeekOrigin");
			}
			if (num < 0L)
			{
				throw new IOException("Attempted to Seek before the beginning of the stream");
			}
			if (num < this.append_startpos)
			{
				throw new IOException("Can't seek back over pre-existing data in append mode");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			this.buf_start = MonoIO.Seek(this.handle, num, SeekOrigin.Begin, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			return this.buf_start;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002D208 File Offset: 0x0002B408
		public override void SetLength(long value)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value is less than 0");
			}
			this.Flush();
			MonoIOError monoIOError;
			MonoIO.SetLength(this.handle, value, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (this.Position > value)
			{
				this.Position = value;
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
		public override void Flush()
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		~FileStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002D310 File Offset: 0x0002B510
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (this.handle != MonoIO.InvalidHandle)
			{
				try
				{
					this.FlushBuffer();
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (this.owner)
				{
					MonoIOError monoIOError;
					MonoIO.Close(this.handle, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					this.handle = MonoIO.InvalidHandle;
				}
			}
			this.canseek = false;
			this.access = (FileAccess)0;
			if (disposing)
			{
				this.buf = null;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002D3C4 File Offset: 0x0002B5C4
		private int ReadSegment(byte[] dest, int dest_offset, int count)
		{
			if (count > this.buf_length - this.buf_offset)
			{
				count = this.buf_length - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(this.buf, this.buf_offset, dest, dest_offset, count);
				this.buf_offset += count;
			}
			return count;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002D420 File Offset: 0x0002B620
		private int WriteSegment(byte[] src, int src_offset, int count)
		{
			if (count > this.buf_size - this.buf_offset)
			{
				count = this.buf_size - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(src, src_offset, this.buf, this.buf_offset, count);
				this.buf_offset += count;
				if (this.buf_offset > this.buf_length)
				{
					this.buf_length = this.buf_offset;
				}
				this.buf_dirty = true;
			}
			return count;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002D4A0 File Offset: 0x0002B6A0
		private void FlushBuffer(Stream st)
		{
			if (this.buf_dirty)
			{
				if (this.CanSeek)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.handle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				if (st == null)
				{
					MonoIOError monoIOError;
					MonoIO.Write(this.handle, this.buf, 0, this.buf_length, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				else
				{
					st.Write(this.buf, 0, this.buf_length);
				}
			}
			this.buf_start += (long)this.buf_offset;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002D570 File Offset: 0x0002B770
		private void FlushBuffer()
		{
			this.FlushBuffer(null);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002D57C File Offset: 0x0002B77C
		private void FlushBufferIfDirty()
		{
			if (this.buf_dirty)
			{
				this.FlushBuffer(null);
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002D590 File Offset: 0x0002B790
		private void RefillBuffer()
		{
			this.FlushBuffer(null);
			this.buf_length = this.ReadData(this.handle, this.buf, 0, this.buf_size);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002D5B8 File Offset: 0x0002B7B8
		private int ReadData(IntPtr handle, byte[] buf, int offset, int count)
		{
			MonoIOError monoIOError;
			int num = MonoIO.Read(handle, buf, offset, count, out monoIOError);
			if (monoIOError == MonoIOError.ERROR_BROKEN_PIPE)
			{
				num = 0;
			}
			else if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (num == -1)
			{
				throw new IOException();
			}
			return num;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002D60C File Offset: 0x0002B80C
		private void InitBuffer(int size, bool noBuffering)
		{
			if (noBuffering)
			{
				size = 0;
				this.buf = new byte[1];
			}
			else
			{
				if (size <= 0)
				{
					throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
				}
				if (size < 8)
				{
					size = 8;
				}
				this.buf = new byte[size];
			}
			this.buf_size = size;
			this.buf_start = 0L;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002D688 File Offset: 0x0002B888
		private string GetSecureFileName(string filename)
		{
			return (!this.anonymous) ? Path.GetFullPath(filename) : Path.GetFileName(filename);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002D6A8 File Offset: 0x0002B8A8
		private string GetSecureFileName(string filename, bool full)
		{
			return (!this.anonymous) ? ((!full) ? filename : Path.GetFullPath(filename)) : Path.GetFileName(filename);
		}

		// Token: 0x040004A5 RID: 1189
		internal const int DefaultBufferSize = 8192;

		// Token: 0x040004A6 RID: 1190
		private FileAccess access;

		// Token: 0x040004A7 RID: 1191
		private bool owner;

		// Token: 0x040004A8 RID: 1192
		private bool async;

		// Token: 0x040004A9 RID: 1193
		private bool canseek;

		// Token: 0x040004AA RID: 1194
		private long append_startpos;

		// Token: 0x040004AB RID: 1195
		private bool anonymous;

		// Token: 0x040004AC RID: 1196
		private byte[] buf;

		// Token: 0x040004AD RID: 1197
		private int buf_size;

		// Token: 0x040004AE RID: 1198
		private int buf_length;

		// Token: 0x040004AF RID: 1199
		private int buf_offset;

		// Token: 0x040004B0 RID: 1200
		private bool buf_dirty;

		// Token: 0x040004B1 RID: 1201
		private long buf_start;

		// Token: 0x040004B2 RID: 1202
		private string name;

		// Token: 0x040004B3 RID: 1203
		private IntPtr handle;

		// Token: 0x040004B4 RID: 1204
		private SafeFileHandle safeHandle;

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000BA2 RID: 2978
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x06000BA6 RID: 2982
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);
	}
}
