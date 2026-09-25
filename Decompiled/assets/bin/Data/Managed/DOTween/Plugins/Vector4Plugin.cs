using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000045 RID: 69
	public class Vector4Plugin : ABSTweenPlugin<Vector4, Vector4, VectorOptions>
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000077C4 File Offset: 0x000059C4
		public override void Reset(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000077C8 File Offset: 0x000059C8
		public override Vector4 ConvertToStartValue(TweenerCore<Vector4, Vector4, VectorOptions> t, Vector4 value)
		{
			return value;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000077CC File Offset: 0x000059CC
		public override void SetRelativeEndValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000077E8 File Offset: 0x000059E8
		public override void SetChangeValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint <= AxisConstraint.Y)
			{
				if (axisConstraint == AxisConstraint.X)
				{
					t.changeValue = new Vector4(t.endValue.x - t.startValue.x, 0f, 0f, 0f);
					return;
				}
				if (axisConstraint == AxisConstraint.Y)
				{
					t.changeValue = new Vector4(0f, t.endValue.y - t.startValue.y, 0f, 0f);
					return;
				}
			}
			else
			{
				if (axisConstraint == AxisConstraint.Z)
				{
					t.changeValue = new Vector4(0f, 0f, t.endValue.z - t.startValue.z, 0f);
					return;
				}
				if (axisConstraint == AxisConstraint.W)
				{
					t.changeValue = new Vector4(0f, 0f, 0f, t.endValue.w - t.startValue.w);
					return;
				}
			}
			t.changeValue = t.endValue - t.startValue;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007904 File Offset: 0x00005B04
		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector4 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007910 File Offset: 0x00005B10
		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector4> getter, DOSetter<Vector4> setter, float elapsed, Vector4 startValue, Vector4 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
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
			if (axisConstraint <= AxisConstraint.Y)
			{
				if (axisConstraint == AxisConstraint.X)
				{
					Vector4 vector = getter();
					vector.x = startValue.x + changeValue.x * num;
					if (options.snapping)
					{
						vector.x = (float)Math.Round((double)vector.x);
					}
					setter(vector);
					return;
				}
				if (axisConstraint == AxisConstraint.Y)
				{
					Vector4 vector2 = getter();
					vector2.y = startValue.y + changeValue.y * num;
					if (options.snapping)
					{
						vector2.y = (float)Math.Round((double)vector2.y);
					}
					setter(vector2);
					return;
				}
			}
			else
			{
				if (axisConstraint == AxisConstraint.Z)
				{
					Vector4 vector3 = getter();
					vector3.z = startValue.z + changeValue.z * num;
					if (options.snapping)
					{
						vector3.z = (float)Math.Round((double)vector3.z);
					}
					setter(vector3);
					return;
				}
				if (axisConstraint == AxisConstraint.W)
				{
					Vector4 vector4 = getter();
					vector4.w = startValue.w + changeValue.w * num;
					if (options.snapping)
					{
						vector4.w = (float)Math.Round((double)vector4.w);
					}
					setter(vector4);
					return;
				}
			}
			startValue.x += changeValue.x * num;
			startValue.y += changeValue.y * num;
			startValue.z += changeValue.z * num;
			startValue.w += changeValue.w * num;
			if (options.snapping)
			{
				startValue.x = (float)Math.Round((double)startValue.x);
				startValue.y = (float)Math.Round((double)startValue.y);
				startValue.z = (float)Math.Round((double)startValue.z);
				startValue.w = (float)Math.Round((double)startValue.w);
			}
			setter(startValue);
		}
	}
}
