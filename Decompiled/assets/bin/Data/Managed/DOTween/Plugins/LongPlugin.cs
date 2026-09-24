using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000030 RID: 48
	public class LongPlugin : ABSTweenPlugin<long, long, NoOptions>
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000056A8 File Offset: 0x000038A8
		public override void Reset(TweenerCore<long, long, NoOptions> t)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000056AC File Offset: 0x000038AC
		public override long ConvertToStartValue(TweenerCore<long, long, NoOptions> t, long value)
		{
			return value;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000056B0 File Offset: 0x000038B0
		public override void SetRelativeEndValue(TweenerCore<long, long, NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000056C8 File Offset: 0x000038C8
		public override void SetChangeValue(TweenerCore<long, long, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000056E0 File Offset: 0x000038E0
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, long changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005700 File Offset: 0x00003900
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<long> getter, DOSetter<long> setter, float elapsed, long startValue, long changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (long)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (long)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (long)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((long)Math.Round((double)((float)startValue + (float)changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))));
		}
	}
}
