using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000036 RID: 54
	public struct RectOptions : IPlugOptions
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00005800 File Offset: 0x00003A00
		public void Reset()
		{
			this.snapping = false;
		}

		// Token: 0x040000F2 RID: 242
		public bool snapping;
	}
}
