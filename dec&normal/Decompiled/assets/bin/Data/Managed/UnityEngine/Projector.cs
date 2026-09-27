using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D4 RID: 212
	public sealed class Projector : Behaviour
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600086C RID: 2156
		// (set) Token: 0x0600086D RID: 2157
		public extern float nearClipPlane { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001C4 RID: 452
		// (set) Token: 0x0600086E RID: 2158
		public extern float farClipPlane { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001C5 RID: 453
		// (set) Token: 0x0600086F RID: 2159
		public extern float aspectRatio { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001C6 RID: 454
		// (set) Token: 0x06000870 RID: 2160
		public extern bool orthographic { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001C7 RID: 455
		// (set) Token: 0x06000871 RID: 2161
		public extern float orthographicSize { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001C8 RID: 456
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00013624 File Offset: 0x00011824
		public float orthoGraphicSize
		{
			set
			{
				this.orthographicSize = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (set) Token: 0x06000873 RID: 2163
		public extern int ignoreLayers { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001CA RID: 458
		// (set) Token: 0x06000874 RID: 2164
		public extern Material material { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
