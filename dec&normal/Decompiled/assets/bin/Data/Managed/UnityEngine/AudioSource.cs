using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	public sealed class AudioSource : Behaviour
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000268 RID: 616
		// (set) Token: 0x06000269 RID: 617
		public extern float volume { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026A RID: 618
		// (set) Token: 0x0600026B RID: 619
		public extern float pitch { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026C RID: 620
		// (set) Token: 0x0600026D RID: 621
		public extern AudioClip clip { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600026E RID: 622
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Play([DefaultValue("0")] ulong delay);

		// Token: 0x0600026F RID: 623 RVA: 0x000072C0 File Offset: 0x000054C0
		[ExcludeFromDocs]
		public void Play()
		{
			ulong delay = 0UL;
			this.Play(delay);
		}

		// Token: 0x06000270 RID: 624
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Stop();

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000271 RID: 625
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000272 RID: 626
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale);

		// Token: 0x06000273 RID: 627 RVA: 0x000072D8 File Offset: 0x000054D8
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			float volumeScale = 1f;
			this.PlayOneShot(clip, volumeScale);
		}

		// Token: 0x17000053 RID: 83
		// (set) Token: 0x06000274 RID: 628
		public extern bool loop { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000054 RID: 84
		// (set) Token: 0x06000275 RID: 629
		public extern bool playOnAwake { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000055 RID: 85
		// (set) Token: 0x06000276 RID: 630
		public extern float panLevel { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000056 RID: 86
		// (set) Token: 0x06000277 RID: 631
		public extern float spread { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000278 RID: 632
		// (set) Token: 0x06000279 RID: 633
		public extern int priority { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000058 RID: 88
		// (set) Token: 0x0600027A RID: 634
		public extern float minDistance { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
