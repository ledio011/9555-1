using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000139 RID: 313
	[ComVisible(true)]
	[Serializable]
	public abstract class Stream : IDisposable
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C19 RID: 3097
		public abstract bool CanRead { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C1A RID: 3098
		public abstract bool CanSeek { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C1B RID: 3099
		public abstract bool CanWrite { get; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C1C RID: 3100
		public abstract long Length { get; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C1D RID: 3101
		// (set) Token: 0x06000C1E RID: 3102
		public abstract long Position { get; set; }

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002F378 File Offset: 0x0002D578
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002F380 File Offset: 0x0002D580
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002F384 File Offset: 0x0002D584
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C22 RID: 3106
		public abstract void Flush();

		// Token: 0x06000C23 RID: 3107
		public abstract int Read([In] [Out] byte[] buffer, int offset, int count);

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002F390 File Offset: 0x0002D590
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 1)
			{
				return (int)array[0];
			}
			return -1;
		}

		// Token: 0x06000C25 RID: 3109
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06000C26 RID: 3110
		public abstract void SetLength(long value);

		// Token: 0x06000C27 RID: 3111
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002F3B8 File Offset: 0x0002D5B8
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			StreamAsyncResult streamAsyncResult = new StreamAsyncResult(state);
			try
			{
				int nbytes = this.Read(buffer, offset, count);
				streamAsyncResult.SetComplete(null, nbytes);
			}
			catch (Exception e)
			{
				streamAsyncResult.SetComplete(e, 0);
			}
			if (callback != null)
			{
				callback(streamAsyncResult);
			}
			return streamAsyncResult;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002F44C File Offset: 0x0002D64C
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			StreamAsyncResult streamAsyncResult = new StreamAsyncResult(state);
			try
			{
				this.Write(buffer, offset, count);
				streamAsyncResult.SetComplete(null);
			}
			catch (Exception complete)
			{
				streamAsyncResult.SetComplete(complete);
			}
			if (callback != null)
			{
				callback.BeginInvoke(streamAsyncResult, null, null);
			}
			return streamAsyncResult;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			StreamAsyncResult streamAsyncResult = asyncResult as StreamAsyncResult;
			if (streamAsyncResult == null || streamAsyncResult.NBytes == -1)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (streamAsyncResult.Done)
			{
				throw new InvalidOperationException("EndRead already called.");
			}
			streamAsyncResult.Done = true;
			if (streamAsyncResult.Exception != null)
			{
				throw streamAsyncResult.Exception;
			}
			return streamAsyncResult.NBytes;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002F538 File Offset: 0x0002D738
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			StreamAsyncResult streamAsyncResult = asyncResult as StreamAsyncResult;
			if (streamAsyncResult == null || streamAsyncResult.NBytes != -1)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (streamAsyncResult.Done)
			{
				throw new InvalidOperationException("EndWrite already called.");
			}
			streamAsyncResult.Done = true;
			if (streamAsyncResult.Exception != null)
			{
				throw streamAsyncResult.Exception;
			}
		}

		// Token: 0x04000510 RID: 1296
		public static readonly Stream Null = new NullStream();
	}
}
