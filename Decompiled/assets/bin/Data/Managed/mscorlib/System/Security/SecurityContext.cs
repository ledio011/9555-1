using System;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
	// Token: 0x0200037C RID: 892
	public sealed class SecurityContext
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x00061004 File Offset: 0x0005F204
		internal SecurityContext()
		{
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0006100C File Offset: 0x0005F20C
		internal SecurityContext(SecurityContext sc)
		{
			this._capture = true;
			this._winid = sc._winid;
			if (sc._stack != null)
			{
				this._stack = sc._stack.CreateCopy();
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00061044 File Offset: 0x0005F244
		public static SecurityContext Capture()
		{
			SecurityContext securityContext = Thread.CurrentThread.ExecutionContext.SecurityContext;
			if (securityContext.FlowSuppressed)
			{
				return null;
			}
			return new SecurityContext
			{
				_capture = true,
				_winid = WindowsIdentity.GetCurrentToken(),
				_stack = CompressedStack.Capture()
			};
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x00061094 File Offset: 0x0005F294
		internal bool FlowSuppressed
		{
			get
			{
				return this._suppressFlow;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x0006109C File Offset: 0x0005F29C
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x000610A4 File Offset: 0x0005F2A4
		internal CompressedStack CompressedStack
		{
			get
			{
				return this._stack;
			}
			set
			{
				this._stack = value;
			}
		}

		// Token: 0x04000E69 RID: 3689
		private bool _capture;

		// Token: 0x04000E6A RID: 3690
		private IntPtr _winid;

		// Token: 0x04000E6B RID: 3691
		private CompressedStack _stack;

		// Token: 0x04000E6C RID: 3692
		private bool _suppressFlowWindowsIdentity;

		// Token: 0x04000E6D RID: 3693
		private bool _suppressFlow;
	}
}
