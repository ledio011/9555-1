using System;
using System.Runtime.Remoting.Messaging;

namespace System.IO
{
	// Token: 0x0200003C RID: 60
	internal class MonoSyncFileStream : FileStream
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003D24 File Offset: 0x00001F24
		public MonoSyncFileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize) : base(handle, access, ownsHandle, bufferSize, false)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003D34 File Offset: 0x00001F34
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			MonoSyncFileStream.WriteDelegate writeDelegate = new MonoSyncFileStream.WriteDelegate(this.Write);
			return writeDelegate.BeginInvoke(buffer, offset, count, cback, state);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			MonoSyncFileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as MonoSyncFileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003E18 File Offset: 0x00002018
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			MonoSyncFileStream.ReadDelegate readDelegate = new MonoSyncFileStream.ReadDelegate(this.Read);
			return readDelegate.BeginInvoke(buffer, offset, count, cback, state);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003E98 File Offset: 0x00002098
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			MonoSyncFileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as MonoSyncFileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x060000D7 RID: 215
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x060000DB RID: 219
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);
	}
}
