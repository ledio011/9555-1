using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins.Core
{
	// Token: 0x02000029 RID: 41
	public abstract class ABSTweenPlugin<T1, T2, TPlugOptions> : ITweenPlugin where TPlugOptions : struct, IPlugOptions
	{
		// Token: 0x06000098 RID: 152
		public abstract void Reset(TweenerCore<T1, T2, TPlugOptions> t);

		// Token: 0x06000099 RID: 153
		public abstract T2 ConvertToStartValue(TweenerCore<T1, T2, TPlugOptions> t, T1 value);

		// Token: 0x0600009A RID: 154
		public abstract void SetRelativeEndValue(TweenerCore<T1, T2, TPlugOptions> t);

		// Token: 0x0600009B RID: 155
		public abstract void SetChangeValue(TweenerCore<T1, T2, TPlugOptions> t);

		// Token: 0x0600009C RID: 156
		public abstract float GetSpeedBasedDuration(TPlugOptions options, float unitsXSecond, T2 changeValue);

		// Token: 0x0600009D RID: 157
		public abstract void EvaluateAndApply(TPlugOptions options, Tween t, bool isRelative, DOGetter<T1> getter, DOSetter<T1> setter, float elapsed, T2 startValue, T2 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice);
	}
}
