using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000032 RID: 50
	public struct FloatOptions : IPlugOptions
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000057D4 File Offset: 0x000039D4
		public void Reset()
		{
			this.snapping = false;
		}

		// Token: 0x040000EE RID: 238
		public bool snapping;
	}
}
