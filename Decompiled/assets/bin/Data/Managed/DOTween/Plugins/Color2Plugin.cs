using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000027 RID: 39
	internal class Color2Plugin : ABSTweenPlugin<Color2, Color2, ColorOptions>
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004A34 File Offset: 0x00002C34
		public override void Reset(TweenerCore<Color2, Color2, ColorOptions> t)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004A38 File Offset: 0x00002C38
		public override Color2 ConvertToStartValue(TweenerCore<Color2, Color2, ColorOptions> t, Color2 value)
		{
			return value;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004A3C File Offset: 0x00002C3C
		public override void SetRelativeEndValue(TweenerCore<Color2, Color2, ColorOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004A58 File Offset: 0x00002C58
		public override void SetChangeValue(TweenerCore<Color2, Color2, ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004A74 File Offset: 0x00002C74
		public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color2 changeValue)
		{
			return 1f / unitsXSecond;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004A80 File Offset: 0x00002C80
		public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color2> getter, DOSetter<Color2> setter, float elapsed, Color2 startValue, Color2 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (float)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			if (!options.alphaOnly)
			{
				startValue.ca.r = startValue.ca.r + changeValue.ca.r * num;
				startValue.ca.g = startValue.ca.g + changeValue.ca.g * num;
				startValue.ca.b = startValue.ca.b + changeValue.ca.b * num;
				startValue.ca.a = startValue.ca.a + changeValue.ca.a * num;
				startValue.cb.r = startValue.cb.r + changeValue.cb.r * num;
				startValue.cb.g = startValue.cb.g + changeValue.cb.g * num;
				startValue.cb.b = startValue.cb.b + changeValue.cb.b * num;
				startValue.cb.a = startValue.cb.a + changeValue.cb.a * num;
				setter(startValue);
				return;
			}
			Color2 pNewValue = getter();
			pNewValue.ca.a = startValue.ca.a + changeValue.ca.a * num;
			pNewValue.cb.a = startValue.cb.a + changeValue.cb.a * num;
			setter(pNewValue);
		}
	}
}
