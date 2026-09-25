using System;

namespace UnityEngine
{
	// Token: 0x02000107 RID: 263
	public struct SoftJointLimit
	{
		// Token: 0x17000227 RID: 551
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00015EF8 File Offset: 0x000140F8
		public float limit
		{
			set
			{
				this.m_Limit = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00015F04 File Offset: 0x00014104
		public float spring
		{
			set
			{
				this.m_Spring = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00015F10 File Offset: 0x00014110
		public float damper
		{
			set
			{
				this.m_Damper = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00015F1C File Offset: 0x0001411C
		public float bounciness
		{
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x040003CE RID: 974
		private float m_Limit;

		// Token: 0x040003CF RID: 975
		private float m_Bounciness;

		// Token: 0x040003D0 RID: 976
		private float m_Spring;

		// Token: 0x040003D1 RID: 977
		private float m_Damper;
	}
}
