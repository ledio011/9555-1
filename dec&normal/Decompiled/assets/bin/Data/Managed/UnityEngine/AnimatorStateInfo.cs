using System;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	public struct AnimatorStateInfo
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00006A0C File Offset: 0x00004C0C
		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == this.m_Name || num == this.m_Path;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00006A38 File Offset: 0x00004C38
		public int nameHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006A40 File Offset: 0x00004C40
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006A48 File Offset: 0x00004C48
		public float length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006A50 File Offset: 0x00004C50
		public int tagHash
		{
			get
			{
				return this.m_Tag;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006A58 File Offset: 0x00004C58
		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == this.m_Tag;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006A68 File Offset: 0x00004C68
		public bool loop
		{
			get
			{
				return this.m_Loop != 0;
			}
		}

		// Token: 0x0400001E RID: 30
		private int m_Name;

		// Token: 0x0400001F RID: 31
		private int m_Path;

		// Token: 0x04000020 RID: 32
		private float m_NormalizedTime;

		// Token: 0x04000021 RID: 33
		private float m_Length;

		// Token: 0x04000022 RID: 34
		private int m_Tag;

		// Token: 0x04000023 RID: 35
		private int m_Loop;
	}
}
