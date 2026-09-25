using System;

namespace UnityEngine
{
	// Token: 0x020000A1 RID: 161
	public struct Keyframe
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x00011960 File Offset: 0x0000FB60
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00011988 File Offset: 0x0000FB88
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000119A8 File Offset: 0x0000FBA8
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x000119BC File Offset: 0x0000FBBC
		public float value
		{
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public float inTangent
		{
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public float outTangent
		{
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public int tangentMode
		{
			set
			{
			}
		}

		// Token: 0x040002CE RID: 718
		private float m_Time;

		// Token: 0x040002CF RID: 719
		private float m_Value;

		// Token: 0x040002D0 RID: 720
		private float m_InTangent;

		// Token: 0x040002D1 RID: 721
		private float m_OutTangent;
	}
}
