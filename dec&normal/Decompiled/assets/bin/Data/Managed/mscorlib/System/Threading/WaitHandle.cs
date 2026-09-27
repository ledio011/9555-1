using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020003C8 RID: 968
	[ComVisible(true)]
	public abstract class WaitHandle : MarshalByRefObject, IDisposable
	{
		// Token: 0x06001D4F RID: 7503 RVA: 0x0006EEB0 File Offset: 0x0006D0B0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0006EEC0 File Offset: 0x0006D0C0
		private static void CheckArray(WaitHandle[] handles, bool waitAll)
		{
			if (handles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			int num = handles.Length;
			if (num > 64)
			{
				throw new NotSupportedException("Too many handles");
			}
			foreach (WaitHandle waitHandle in handles)
			{
				if (waitHandle == null)
				{
					throw new ArgumentNullException("waitHandles", "null handle");
				}
				if (waitHandle.safe_wait_handle == null)
				{
					throw new ArgumentException("null element found", "waitHandle");
				}
			}
		}

		// Token: 0x06001D51 RID: 7505
		[MethodImpl(4096)]
		private static extern int WaitAny_internal(WaitHandle[] handles, int ms, bool exitContext);

		// Token: 0x06001D52 RID: 7506 RVA: 0x0006EF40 File Offset: 0x0006D140
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			WaitHandle.CheckArray(waitHandles, false);
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			int result;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				result = WaitHandle.WaitAny_internal(waitHandles, (int)num, exitContext);
			}
			finally
			{
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return result;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0006EFB8 File Offset: 0x0006D1B8
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0006EFC8 File Offset: 0x0006D1C8
		[Obsolete("In the profiles > 2.x, use SafeHandle instead of Handle")]
		public virtual IntPtr Handle
		{
			get
			{
				return this.safe_wait_handle.DangerousGetHandle();
			}
			set
			{
				if (value == WaitHandle.InvalidHandle)
				{
					this.safe_wait_handle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
				}
				else
				{
					this.safe_wait_handle = new SafeWaitHandle(value, true);
				}
			}
		}

		// Token: 0x06001D55 RID: 7509
		[MethodImpl(4096)]
		private extern bool WaitOne_internal(IntPtr handle, int ms, bool exitContext);

		// Token: 0x06001D56 RID: 7510 RVA: 0x0006F000 File Offset: 0x0006D200
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.safe_wait_handle == null)
				{
					return;
				}
				lock (this)
				{
					if (this.safe_wait_handle != null)
					{
						this.safe_wait_handle.Dispose();
					}
				}
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0006F068 File Offset: 0x0006D268
		public virtual bool WaitOne()
		{
			this.CheckDisposed();
			bool flag = false;
			bool result;
			try
			{
				this.safe_wait_handle.DangerousAddRef(ref flag);
				result = this.WaitOne_internal(this.safe_wait_handle.DangerousGetHandle(), -1, false);
			}
			finally
			{
				if (flag)
				{
					this.safe_wait_handle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0006F0CC File Offset: 0x0006D2CC
		public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			this.CheckDisposed();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			bool flag = false;
			bool result;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				this.safe_wait_handle.DangerousAddRef(ref flag);
				result = this.WaitOne_internal(this.safe_wait_handle.DangerousGetHandle(), millisecondsTimeout, exitContext);
			}
			finally
			{
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
				if (flag)
				{
					this.safe_wait_handle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0006F158 File Offset: 0x0006D358
		internal void CheckDisposed()
		{
			if (this.disposed || this.safe_wait_handle == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0006F184 File Offset: 0x0006D384
		~WaitHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x04000F75 RID: 3957
		public const int WaitTimeout = 258;

		// Token: 0x04000F76 RID: 3958
		private SafeWaitHandle safe_wait_handle;

		// Token: 0x04000F77 RID: 3959
		protected static readonly IntPtr InvalidHandle = (IntPtr)(-1);

		// Token: 0x04000F78 RID: 3960
		private bool disposed;
	}
}
