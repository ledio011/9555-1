using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200006C RID: 108
	[StructLayout(0)]
	public sealed class Gradient
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public Gradient()
		{
			this.Init();
		}

		// Token: 0x060004CB RID: 1227
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x060004CC RID: 1228
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060004CD RID: 1229 RVA: 0x0000A400 File Offset: 0x00008600
		~Gradient()
		{
			this.Cleanup();
		}

		// Token: 0x060004CE RID: 1230
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color Evaluate(float time);

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004CF RID: 1231
		// (set) Token: 0x060004D0 RID: 1232
		public extern GradientColorKey[] colorKeys { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004D1 RID: 1233
		// (set) Token: 0x060004D2 RID: 1234
		public extern GradientAlphaKey[] alphaKeys { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060004D3 RID: 1235
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		// Token: 0x040000FB RID: 251
		internal IntPtr m_Ptr;
	}
}
