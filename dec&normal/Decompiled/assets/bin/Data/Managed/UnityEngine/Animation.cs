using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000159 RID: 345
		// (set) Token: 0x0600015A RID: 346
		public extern AnimationClip clip { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600015B RID: 347
		// (set) Token: 0x0600015C RID: 348
		public extern bool playAutomatically { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600015D RID: 349
		// (set) Token: 0x0600015E RID: 350
		public extern WrapMode wrapMode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600015F RID: 351 RVA: 0x0000658C File Offset: 0x0000478C
		public void Stop()
		{
			Animation.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x06000160 RID: 352
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Stop(Animation self);

		// Token: 0x06000161 RID: 353 RVA: 0x00006594 File Offset: 0x00004794
		public void Stop(string name)
		{
			this.Internal_StopByName(name);
		}

		// Token: 0x06000162 RID: 354
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_StopByName(string name);

		// Token: 0x06000163 RID: 355 RVA: 0x000065A0 File Offset: 0x000047A0
		public void Rewind(string name)
		{
			this.Internal_RewindByName(name);
		}

		// Token: 0x06000164 RID: 356
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_RewindByName(string name);

		// Token: 0x06000165 RID: 357 RVA: 0x000065AC File Offset: 0x000047AC
		public void Rewind()
		{
			Animation.INTERNAL_CALL_Rewind(this);
		}

		// Token: 0x06000166 RID: 358
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Rewind(Animation self);

		// Token: 0x06000167 RID: 359 RVA: 0x000065B4 File Offset: 0x000047B4
		public void Sample()
		{
			Animation.INTERNAL_CALL_Sample(this);
		}

		// Token: 0x06000168 RID: 360
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Sample(Animation self);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000169 RID: 361
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600016A RID: 362
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool IsPlaying(string name);

		// Token: 0x17000007 RID: 7
		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000065C8 File Offset: 0x000047C8
		[ExcludeFromDocs]
		public bool Play()
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.Play(mode);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000065E0 File Offset: 0x000047E0
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x0600016E RID: 366
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600016F RID: 367 RVA: 0x000065EC File Offset: 0x000047EC
		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.Play(animation, mode);
		}

		// Token: 0x06000170 RID: 368
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000171 RID: 369 RVA: 0x00006604 File Offset: 0x00004804
		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			this.CrossFade(animation, fadeLength, mode);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000661C File Offset: 0x0000481C
		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			float fadeLength = 0.3f;
			this.CrossFade(animation, fadeLength, mode);
		}

		// Token: 0x06000173 RID: 371
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		// Token: 0x06000174 RID: 372 RVA: 0x0000663C File Offset: 0x0000483C
		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			float fadeLength = 0.3f;
			this.Blend(animation, targetWeight, fadeLength);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006658 File Offset: 0x00004858
		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			float fadeLength = 0.3f;
			float targetWeight = 1f;
			this.Blend(animation, targetWeight, fadeLength);
		}

		// Token: 0x06000176 RID: 374
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000177 RID: 375 RVA: 0x0000667C File Offset: 0x0000487C
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.CrossFadeQueued(animation, fadeLength, queue, mode);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006698 File Offset: 0x00004898
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			QueueMode queue = QueueMode.CompleteOthers;
			return this.CrossFadeQueued(animation, fadeLength, queue, mode);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000066B4 File Offset: 0x000048B4
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			QueueMode queue = QueueMode.CompleteOthers;
			float fadeLength = 0.3f;
			return this.CrossFadeQueued(animation, fadeLength, queue, mode);
		}

		// Token: 0x0600017A RID: 378
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600017B RID: 379 RVA: 0x000066D8 File Offset: 0x000048D8
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.PlayQueued(animation, queue, mode);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000066F0 File Offset: 0x000048F0
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			QueueMode queue = QueueMode.CompleteOthers;
			return this.PlayQueued(animation, queue, mode);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000670C File Offset: 0x0000490C
		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		// Token: 0x0600017E RID: 382
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		// Token: 0x0600017F RID: 383 RVA: 0x00006720 File Offset: 0x00004920
		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			bool addLoopFrame = false;
			this.AddClip(clip, newName, firstFrame, lastFrame, addLoopFrame);
		}

		// Token: 0x06000180 RID: 384
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RemoveClip(AnimationClip clip);

		// Token: 0x06000181 RID: 385 RVA: 0x0000673C File Offset: 0x0000493C
		public void RemoveClip(string clipName)
		{
			this.RemoveClip2(clipName);
		}

		// Token: 0x06000182 RID: 386
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetClipCount();

		// Token: 0x06000183 RID: 387
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void RemoveClip2(string clipName);

		// Token: 0x06000184 RID: 388
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		// Token: 0x06000185 RID: 389 RVA: 0x00006748 File Offset: 0x00004948
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006754 File Offset: 0x00004954
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006760 File Offset: 0x00004960
		public void SyncLayer(int layer)
		{
			Animation.INTERNAL_CALL_SyncLayer(this, layer);
		}

		// Token: 0x06000188 RID: 392
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SyncLayer(Animation self, int layer);

		// Token: 0x06000189 RID: 393 RVA: 0x0000676C File Offset: 0x0000496C
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x0600018A RID: 394
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern AnimationState GetState(string name);

		// Token: 0x0600018B RID: 395
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern AnimationState GetStateAtIndex(int index);

		// Token: 0x0600018C RID: 396
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern int GetStateCount();

		// Token: 0x0600018D RID: 397 RVA: 0x00006774 File Offset: 0x00004974
		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			if (state)
			{
				return state.clip;
			}
			return null;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600018E RID: 398
		// (set) Token: 0x0600018F RID: 399
		public extern bool animatePhysics { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000190 RID: 400
		// (set) Token: 0x06000191 RID: 401
		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000192 RID: 402
		// (set) Token: 0x06000193 RID: 403
		public extern AnimationCullingType cullingType { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000679C File Offset: 0x0000499C
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000067B4 File Offset: 0x000049B4
		public Bounds localBounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_localBounds(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x06000196 RID: 406
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x06000197 RID: 407
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x0200000F RID: 15
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000198 RID: 408 RVA: 0x000067C0 File Offset: 0x000049C0
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000199 RID: 409 RVA: 0x000067D8 File Offset: 0x000049D8
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x0600019A RID: 410 RVA: 0x000067EC File Offset: 0x000049EC
			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			// Token: 0x0600019B RID: 411 RVA: 0x0000681C File Offset: 0x00004A1C
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x04000010 RID: 16
			private Animation m_Outer;

			// Token: 0x04000011 RID: 17
			private int m_CurrentIndex = -1;
		}
	}
}
