using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000121 RID: 289
	public sealed class Time
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A7F RID: 2687
		public static extern float time { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A80 RID: 2688
		public static extern float deltaTime { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A81 RID: 2689
		public static extern float fixedTime { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A82 RID: 2690
		public static extern float unscaledTime { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A83 RID: 2691
		public static extern float unscaledDeltaTime { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A84 RID: 2692
		public static extern float fixedDeltaTime { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A85 RID: 2693
		public static extern float smoothDeltaTime { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A86 RID: 2694
		// (set) Token: 0x06000A87 RID: 2695
		public static extern float timeScale { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A88 RID: 2696
		public static extern int frameCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A89 RID: 2697
		public static extern float realtimeSinceStartup { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
