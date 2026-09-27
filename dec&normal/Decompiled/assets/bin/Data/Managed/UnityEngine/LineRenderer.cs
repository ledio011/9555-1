using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000AB RID: 171
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00011A48 File Offset: 0x0000FC48
		public void SetWidth(float start, float end)
		{
			LineRenderer.INTERNAL_CALL_SetWidth(this, start, end);
		}

		// Token: 0x06000730 RID: 1840
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetWidth(LineRenderer self, float start, float end);

		// Token: 0x06000731 RID: 1841 RVA: 0x00011A54 File Offset: 0x0000FC54
		public void SetVertexCount(int count)
		{
			LineRenderer.INTERNAL_CALL_SetVertexCount(this, count);
		}

		// Token: 0x06000732 RID: 1842
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetVertexCount(LineRenderer self, int count);

		// Token: 0x06000733 RID: 1843 RVA: 0x00011A60 File Offset: 0x0000FC60
		public void SetPosition(int index, Vector3 position)
		{
			LineRenderer.INTERNAL_CALL_SetPosition(this, index, ref position);
		}

		// Token: 0x06000734 RID: 1844
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetPosition(LineRenderer self, int index, ref Vector3 position);

		// Token: 0x1700018C RID: 396
		// (set) Token: 0x06000735 RID: 1845
		public extern bool useWorldSpace { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
