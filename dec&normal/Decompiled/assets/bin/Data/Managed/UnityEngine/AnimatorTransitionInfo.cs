using System;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	public struct AnimatorTransitionInfo
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00006A78 File Offset: 0x00004C78
		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006A88 File Offset: 0x00004C88
		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00006A98 File Offset: 0x00004C98
		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x04000024 RID: 36
		private int m_Name;

		// Token: 0x04000025 RID: 37
		private int m_UserName;

		// Token: 0x04000026 RID: 38
		private float m_NormalizedTime;
	}
}
