using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000042 RID: 66
	public class Vector2Plugin : ABSTweenPlugin<Vector2, Vector2, VectorOptions>
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00006CEC File Offset: 0x00004EEC
		public override void Reset(TweenerCore<Vector2, Vector2, VectorOptions> t)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public override Vector2 ConvertToStartValue(TweenerCore<Vector2, Vector2, VectorOptions> t, Vector2 value)
		{
			return value;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006CF4 File Offset: 0x00004EF4
		public override void SetRelativeEndValue(TweenerCore<Vector2, Vector2, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006D10 File Offset: 0x00004F10
		public override void SetChangeValue(TweenerCore<Vector2, Vector2, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				t.changeValue = new Vector2(t.endValue.x - t.startValue.x, 0f);
				return;
			}
			if (axisConstraint != AxisConstraint.Y)
			{
				t.changeValue = t.endValue - t.startValue;
				return;
			}
			t.changeValue = new Vector2(0f, t.endValue.y - t.startValue.y);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006D9C File Offset: 0x00004F9C
		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector2 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector2> getter, DOSetter<Vector2> setter, float elapsed, Vector2 startValue, Vector2 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
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
			AxisConstraint axisConstraint = options.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				Vector2 vector = getter();
				vector.x = startValue.x + changeValue.x * num;
				if (options.snapping)
				{
					vector.x = (float)Math.Round((double)vector.x);
				}
				setter(vector);
				return;
			}
			if (axisConstraint != AxisConstraint.Y)
			{
				startValue.x += changeValue.x * num;
				startValue.y += changeValue.y * num;
				if (options.snapping)
				{
					startValue.x = (float)Math.Round((double)startValue.x);
					startValue.y = (float)Math.Round((double)startValue.y);
				}
				setter(startValue);
				return;
			}
			Vector2 vector2 = getter();
			vector2.y = startValue.y + changeValue.y * num;
			if (options.snapping)
			{
				vector2.y = (float)Math.Round((double)vector2.y);
			}
			setter(vector2);
		}
	}
}
