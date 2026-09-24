using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	public sealed class Animator : Behaviour
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x000069E4 File Offset: 0x00004BE4
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000069F0 File Offset: 0x00004BF0
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x060001D9 RID: 473
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex);

		// Token: 0x060001DA RID: 474 RVA: 0x000069FC File Offset: 0x00004BFC
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x060001DB RID: 475
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DC RID: 476
		public extern RuntimeAnimatorController runtimeAnimatorController { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060001DD RID: 477
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int StringToHash(string name);

		// Token: 0x060001DE RID: 478
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void SetTriggerString(string name);

		// Token: 0x060001DF RID: 479
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void ResetTriggerString(string name);

		// Token: 0x060001E0 RID: 480
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Update(float deltaTime);
	}
}
