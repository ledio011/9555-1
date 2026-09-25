using System;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000045 RID: 69
	internal interface ITweenValue
	{
		// Token: 0x060001B8 RID: 440
		void TweenValue(float floatPercentage);

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B9 RID: 441
		bool ignoreTimeScale { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001BA RID: 442
		float duration { get; }

		// Token: 0x060001BB RID: 443
		bool ValidTarget();
	}
}
