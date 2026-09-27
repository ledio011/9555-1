using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	public sealed class CharacterJoint : Joint
	{
		// Token: 0x170000A0 RID: 160
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00007968 File Offset: 0x00005B68
		public Vector3 swingAxis
		{
			set
			{
				this.INTERNAL_set_swingAxis(ref value);
			}
		}

		// Token: 0x0600032B RID: 811
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_swingAxis(ref Vector3 value);

		// Token: 0x170000A1 RID: 161
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00007974 File Offset: 0x00005B74
		public SoftJointLimit lowTwistLimit
		{
			set
			{
				this.INTERNAL_set_lowTwistLimit(ref value);
			}
		}

		// Token: 0x0600032D RID: 813
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_lowTwistLimit(ref SoftJointLimit value);

		// Token: 0x170000A2 RID: 162
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00007980 File Offset: 0x00005B80
		public SoftJointLimit highTwistLimit
		{
			set
			{
				this.INTERNAL_set_highTwistLimit(ref value);
			}
		}

		// Token: 0x0600032F RID: 815
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_highTwistLimit(ref SoftJointLimit value);

		// Token: 0x170000A3 RID: 163
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000798C File Offset: 0x00005B8C
		public SoftJointLimit swing1Limit
		{
			set
			{
				this.INTERNAL_set_swing1Limit(ref value);
			}
		}

		// Token: 0x06000331 RID: 817
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_swing1Limit(ref SoftJointLimit value);

		// Token: 0x170000A4 RID: 164
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00007998 File Offset: 0x00005B98
		public SoftJointLimit swing2Limit
		{
			set
			{
				this.INTERNAL_set_swing2Limit(ref value);
			}
		}

		// Token: 0x06000333 RID: 819
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_swing2Limit(ref SoftJointLimit value);
	}
}
