using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000038 RID: 56
	public struct UintOptions : IPlugOptions
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00005840 File Offset: 0x00003A40
		public void Reset()
		{
			this.isNegativeChangeValue = false;
		}

		// Token: 0x040000F8 RID: 248
		public bool isNegativeChangeValue;
	}
}
