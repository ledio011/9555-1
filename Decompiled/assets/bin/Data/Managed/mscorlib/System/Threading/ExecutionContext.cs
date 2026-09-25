using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020003B2 RID: 946
	[Serializable]
	public sealed class ExecutionContext : ISerializable
	{
		// Token: 0x06001C87 RID: 7303 RVA: 0x0006D76C File Offset: 0x0006B96C
		internal ExecutionContext()
		{
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0006D774 File Offset: 0x0006B974
		internal ExecutionContext(ExecutionContext ec)
		{
			if (ec._sc != null)
			{
				this._sc = new SecurityContext(ec._sc);
			}
			this._suppressFlow = ec._suppressFlow;
			this._capture = true;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006D7AC File Offset: 0x0006B9AC
		[MonoTODO]
		internal ExecutionContext(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006D7BC File Offset: 0x0006B9BC
		public static ExecutionContext Capture()
		{
			ExecutionContext executionContext = Thread.CurrentThread.ExecutionContext;
			if (executionContext.FlowSuppressed)
			{
				return null;
			}
			ExecutionContext executionContext2 = new ExecutionContext(executionContext);
			if (SecurityManager.SecurityEnabled)
			{
				executionContext2.SecurityContext = SecurityContext.Capture();
			}
			return executionContext2;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0006D800 File Offset: 0x0006BA00
		[MonoTODO]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			throw new NotImplementedException();
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0006D818 File Offset: 0x0006BA18
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x0006D838 File Offset: 0x0006BA38
		internal SecurityContext SecurityContext
		{
			get
			{
				if (this._sc == null)
				{
					this._sc = new SecurityContext();
				}
				return this._sc;
			}
			set
			{
				this._sc = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0006D844 File Offset: 0x0006BA44
		internal bool FlowSuppressed
		{
			get
			{
				return this._suppressFlow;
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006D84C File Offset: 0x0006BA4C
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.ExecutionContext.FlowSuppressed;
		}

		// Token: 0x04000F18 RID: 3864
		private SecurityContext _sc;

		// Token: 0x04000F19 RID: 3865
		private bool _suppressFlow;

		// Token: 0x04000F1A RID: 3866
		private bool _capture;
	}
}
