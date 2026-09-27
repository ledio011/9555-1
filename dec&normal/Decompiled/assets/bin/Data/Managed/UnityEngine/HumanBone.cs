using System;

namespace UnityEngine
{
	// Token: 0x0200008E RID: 142
	public struct HumanBone
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000111E8 File Offset: 0x0000F3E8
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000111F0 File Offset: 0x0000F3F0
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000111FC File Offset: 0x0000F3FC
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x00011204 File Offset: 0x0000F404
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x040001A2 RID: 418
		private string m_BoneName;

		// Token: 0x040001A3 RID: 419
		private string m_HumanName;

		// Token: 0x040001A4 RID: 420
		public HumanLimit limit;
	}
}
