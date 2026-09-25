using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000035 RID: 53
	public struct QuaternionOptions : IPlugOptions
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000057E4 File Offset: 0x000039E4
		public void Reset()
		{
			this.rotateMode = RotateMode.Fast;
			this.axisConstraint = AxisConstraint.None;
			this.up = Vector3.zero;
		}

		// Token: 0x040000EF RID: 239
		internal RotateMode rotateMode;

		// Token: 0x040000F0 RID: 240
		internal AxisConstraint axisConstraint;

		// Token: 0x040000F1 RID: 241
		internal Vector3 up;
	}
}
