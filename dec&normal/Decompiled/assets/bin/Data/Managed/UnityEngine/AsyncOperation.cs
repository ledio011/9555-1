using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[StructLayout(0)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x06000245 RID: 581
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InternalDestroy();

		// Token: 0x06000246 RID: 582 RVA: 0x00006F24 File Offset: 0x00005124
		~AsyncOperation()
		{
			this.InternalDestroy();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000247 RID: 583
		public extern float progress { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0400002B RID: 43
		[NotRenamed]
		internal IntPtr m_Ptr;
	}
}
