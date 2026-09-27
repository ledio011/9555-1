using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000039 RID: 57
	public struct Vector3ArrayOptions : IPlugOptions
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000584C File Offset: 0x00003A4C
		public void Reset()
		{
			this.axisConstraint = AxisConstraint.None;
			this.snapping = false;
			this.durations = null;
		}

		// Token: 0x040000F9 RID: 249
		public AxisConstraint axisConstraint;

		// Token: 0x040000FA RID: 250
		public bool snapping;

		// Token: 0x040000FB RID: 251
		internal float[] durations;
	}
}
