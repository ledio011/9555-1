using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	public sealed class AnimationState : TrackedReference
	{
		// Token: 0x17000018 RID: 24
		// (set) Token: 0x060001CC RID: 460
		public extern WrapMode wrapMode { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001CD RID: 461
		// (set) Token: 0x060001CE RID: 462
		public extern float time { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001CF RID: 463
		// (set) Token: 0x060001D0 RID: 464
		public extern float normalizedTime { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001D1 RID: 465
		// (set) Token: 0x060001D2 RID: 466
		public extern float speed { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001D3 RID: 467
		public extern float length { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700001D RID: 29
		// (set) Token: 0x060001D4 RID: 468
		public extern int layer { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001D5 RID: 469
		public extern AnimationClip clip { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001D6 RID: 470
		public extern string name { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
