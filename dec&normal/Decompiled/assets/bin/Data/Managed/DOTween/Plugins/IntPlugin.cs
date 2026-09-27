using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200002F RID: 47
	public class IntPlugin : ABSTweenPlugin<int, int, NoOptions>
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00005580 File Offset: 0x00003780
		public override void Reset(TweenerCore<int, int, NoOptions> t)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005584 File Offset: 0x00003784
		public override int ConvertToStartValue(TweenerCore<int, int, NoOptions> t, int value)
		{
			return value;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005588 File Offset: 0x00003788
		public override void SetRelativeEndValue(TweenerCore<int, int, NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000055A0 File Offset: 0x000037A0
		public override void SetChangeValue(TweenerCore<int, int, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000055B8 File Offset: 0x000037B8
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, int changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000055D8 File Offset: 0x000037D8
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<int> getter, DOSetter<int> setter, float elapsed, int startValue, int changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((int)Math.Round((double)((float)startValue + (float)changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))));
		}
	}
}
