using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	public sealed class CanvasGroup : Component, ICanvasRaycastFilter
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000309 RID: 777
		public extern bool interactable { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600030A RID: 778
		public extern bool blocksRaycasts { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600030B RID: 779
		public extern bool ignoreParentGroups { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600030C RID: 780 RVA: 0x00007868 File Offset: 0x00005A68
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.blocksRaycasts;
		}
	}
}
