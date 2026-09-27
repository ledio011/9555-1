using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200024C RID: 588
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06001402 RID: 5122 RVA: 0x000462CC File Offset: 0x000444CC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.invalid_handle_value = invalidHandleValue;
			this.owns_handle = ownsHandle;
			this.refcount = 1;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x000462EC File Offset: 0x000444EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			if (this.refcount == 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num;
			int num2;
			do
			{
				num = this.refcount;
				num2 = num - 1;
			}
			while (Interlocked.CompareExchange(ref this.refcount, num2, num) != num);
			if (num2 == 0 && this.owns_handle && !this.IsInvalid)
			{
				this.ReleaseHandle();
				this.handle = this.invalid_handle_value;
				this.refcount = -1;
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004636C File Offset: 0x0004456C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void DangerousAddRef(ref bool success)
		{
			if (this.refcount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			for (;;)
			{
				int num = this.refcount;
				int value = num + 1;
				if (num <= 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.refcount, value, num) == num)
				{
					goto Block_3;
				}
			}
			throw new ObjectDisposedException(base.GetType().FullName);
			Block_3:
			success = true;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x000463D0 File Offset: 0x000445D0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			if (this.refcount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			return this.handle;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x000463F8 File Offset: 0x000445F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void DangerousRelease()
		{
			if (this.refcount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num;
			int num2;
			do
			{
				num = this.refcount;
				num2 = num - 1;
			}
			while (Interlocked.CompareExchange(ref this.refcount, num2, num) != num);
			if (num2 == 0 && this.owns_handle && !this.IsInvalid)
			{
				this.ReleaseHandle();
				this.handle = this.invalid_handle_value;
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00046470 File Offset: 0x00044670
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00046480 File Offset: 0x00044680
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06001409 RID: 5129
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		// Token: 0x0600140A RID: 5130 RVA: 0x00046494 File Offset: 0x00044694
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600140B RID: 5131
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }

		// Token: 0x0600140C RID: 5132 RVA: 0x000464A0 File Offset: 0x000446A0
		~SafeHandle()
		{
			if (this.owns_handle && !this.IsInvalid)
			{
				this.ReleaseHandle();
				this.handle = this.invalid_handle_value;
			}
		}

		// Token: 0x04000A12 RID: 2578
		protected IntPtr handle;

		// Token: 0x04000A13 RID: 2579
		private IntPtr invalid_handle_value;

		// Token: 0x04000A14 RID: 2580
		private int refcount;

		// Token: 0x04000A15 RID: 2581
		private bool owns_handle;
	}
}
