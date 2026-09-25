using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x0200003A RID: 58
	public struct VectorOptions : IPlugOptions
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005864 File Offset: 0x00003A64
		public void Reset()
		{
			this.axisConstraint = AxisConstraint.None;
			this.snapping = false;
		}

		// Token: 0x040000FC RID: 252
		public AxisConstraint axisConstraint;

		// Token: 0x040000FD RID: 253
		public bool snapping;
	}
}
