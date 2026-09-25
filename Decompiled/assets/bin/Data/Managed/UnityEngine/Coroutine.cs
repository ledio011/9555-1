using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000045 RID: 69
	[StructLayout(0)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x06000389 RID: 905
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void ReleaseCoroutine();

		// Token: 0x0600038A RID: 906 RVA: 0x000080D4 File Offset: 0x000062D4
		~Coroutine()
		{
			this.ReleaseCoroutine();
		}

		// Token: 0x04000070 RID: 112
		internal IntPtr m_Ptr;
	}
}
