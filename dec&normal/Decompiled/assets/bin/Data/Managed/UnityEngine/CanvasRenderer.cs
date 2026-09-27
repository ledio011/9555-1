using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	public sealed class CanvasRenderer : Component
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00007870 File Offset: 0x00005A70
		public void SetColor(Color color)
		{
			CanvasRenderer.INTERNAL_CALL_SetColor(this, ref color);
		}

		// Token: 0x0600030E RID: 782
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetColor(CanvasRenderer self, ref Color color);

		// Token: 0x0600030F RID: 783
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color GetColor();

		// Token: 0x17000094 RID: 148
		// (set) Token: 0x06000310 RID: 784
		public extern bool isMask { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000311 RID: 785
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetMaterial(Material material, Texture texture);

		// Token: 0x06000312 RID: 786 RVA: 0x0000787C File Offset: 0x00005A7C
		public void SetVertices(List<UIVertex> vertices)
		{
			if (vertices.Count > 65535)
			{
				Debug.LogWarning(UnityString.Format("Number of vertices set exceeds {0}, rendering of this element will be skipped", new object[]
				{
					ushort.MaxValue
				}), this);
				vertices.Clear();
			}
			this.SetVerticesInternal(vertices);
		}

		// Token: 0x06000313 RID: 787
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void SetVerticesInternal(object vertices);

		// Token: 0x06000314 RID: 788 RVA: 0x000078CC File Offset: 0x00005ACC
		public void SetVertices(UIVertex[] vertices, int size)
		{
			if (size > 65535)
			{
				Debug.LogWarning(UnityString.Format("Number of vertices set exceeds {0}, rendering of this element will be skipped", new object[]
				{
					ushort.MaxValue
				}), this);
				size = 0;
			}
			this.SetVerticesInternalArray(vertices, size);
		}

		// Token: 0x06000315 RID: 789
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void SetVerticesInternalArray(UIVertex[] vertices, int size);

		// Token: 0x06000316 RID: 790
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Clear();

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000317 RID: 791
		public extern int absoluteDepth { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
