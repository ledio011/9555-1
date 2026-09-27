using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200029A RID: 666
	[ComVisible(true)]
	public class AsyncResult : IAsyncResult, IMessageSink
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0004A700 File Offset: 0x00048900
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle result;
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
					result = this.handle;
				}
				return result;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0004A75C File Offset: 0x0004895C
		public virtual object AsyncDelegate
		{
			get
			{
				return this.async_delegate;
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004A764 File Offset: 0x00048964
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004A76C File Offset: 0x0004896C
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this.message_ctrl = mc;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004A778 File Offset: 0x00048978
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			this.reply_message = msg;
			lock (this)
			{
				this.completed = true;
				if (this.handle != null)
				{
					((ManualResetEvent)this.AsyncWaitHandle).Set();
				}
			}
			if (this.async_callback != null)
			{
				AsyncCallback asyncCallback = (AsyncCallback)this.async_callback;
				asyncCallback(this);
			}
			return null;
		}

		// Token: 0x04000AEA RID: 2794
		private object async_state;

		// Token: 0x04000AEB RID: 2795
		private WaitHandle handle;

		// Token: 0x04000AEC RID: 2796
		private object async_delegate;

		// Token: 0x04000AED RID: 2797
		private IntPtr data;

		// Token: 0x04000AEE RID: 2798
		private object object_data;

		// Token: 0x04000AEF RID: 2799
		private bool sync_completed;

		// Token: 0x04000AF0 RID: 2800
		private bool completed;

		// Token: 0x04000AF1 RID: 2801
		private bool endinvoke_called;

		// Token: 0x04000AF2 RID: 2802
		private object async_callback;

		// Token: 0x04000AF3 RID: 2803
		private ExecutionContext current;

		// Token: 0x04000AF4 RID: 2804
		private ExecutionContext original;

		// Token: 0x04000AF5 RID: 2805
		private int gchandle;

		// Token: 0x04000AF6 RID: 2806
		private MonoMethodMessage call_message;

		// Token: 0x04000AF7 RID: 2807
		private IMessageCtrl message_ctrl;

		// Token: 0x04000AF8 RID: 2808
		private IMessage reply_message;
	}
}
