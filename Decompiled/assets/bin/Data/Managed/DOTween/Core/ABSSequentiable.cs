using System;

namespace DG.Tweening.Core
{
	// Token: 0x02000009 RID: 9
	public abstract class ABSSequentiable
	{
		// Token: 0x04000012 RID: 18
		internal TweenType tweenType;

		// Token: 0x04000013 RID: 19
		internal float sequencedPosition;

		// Token: 0x04000014 RID: 20
		internal float sequencedEndPosition;

		// Token: 0x04000015 RID: 21
		internal TweenCallback onStart;
	}
}
