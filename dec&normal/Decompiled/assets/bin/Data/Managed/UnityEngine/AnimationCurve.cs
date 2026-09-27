using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[StructLayout(0)]
	public sealed class AnimationCurve
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000686C File Offset: 0x00004A6C
		public AnimationCurve(params Keyframe[] keys)
		{
			this.Init(keys);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000687C File Offset: 0x00004A7C
		public AnimationCurve()
		{
			this.Init(null);
		}

		// Token: 0x060001B1 RID: 433
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060001B2 RID: 434 RVA: 0x0000688C File Offset: 0x00004A8C
		~AnimationCurve()
		{
			this.Cleanup();
		}

		// Token: 0x060001B3 RID: 435
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern float Evaluate(float time);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000068BC File Offset: 0x00004ABC
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000068C4 File Offset: 0x00004AC4
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060001B6 RID: 438
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int AddKey(float time, float value);

		// Token: 0x060001B7 RID: 439 RVA: 0x000068D0 File Offset: 0x00004AD0
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000068DC File Offset: 0x00004ADC
		private int AddKey_Internal(Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_AddKey_Internal(this, ref key);
		}

		// Token: 0x060001B9 RID: 441
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_AddKey_Internal(AnimationCurve self, ref Keyframe key);

		// Token: 0x060001BA RID: 442 RVA: 0x000068E8 File Offset: 0x00004AE8
		public int MoveKey(int index, Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_MoveKey(this, index, ref key);
		}

		// Token: 0x060001BB RID: 443
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_MoveKey(AnimationCurve self, int index, ref Keyframe key);

		// Token: 0x060001BC RID: 444
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RemoveKey(int index);

		// Token: 0x17000014 RID: 20
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey_Internal(index);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001BE RID: 446
		public extern int length { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060001BF RID: 447
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060001C0 RID: 448
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Keyframe GetKey_Internal(int index);

		// Token: 0x060001C1 RID: 449
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060001C2 RID: 450
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060001C3 RID: 451 RVA: 0x00006900 File Offset: 0x00004B00
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			float num = (valueEnd - valueStart) / (timeEnd - timeStart);
			Keyframe[] keys = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, num),
				new Keyframe(timeEnd, valueEnd, num, 0f)
			};
			return new AnimationCurve(keys);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006954 File Offset: 0x00004B54
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			Keyframe[] keys = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, 0f),
				new Keyframe(timeEnd, valueEnd, 0f, 0f)
			};
			return new AnimationCurve(keys);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001C5 RID: 453
		// (set) Token: 0x060001C6 RID: 454
		public extern WrapMode preWrapMode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001C7 RID: 455
		// (set) Token: 0x060001C8 RID: 456
		public extern WrapMode postWrapMode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060001C9 RID: 457
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init(Keyframe[] keys);

		// Token: 0x04000017 RID: 23
		internal IntPtr m_Ptr;
	}
}
