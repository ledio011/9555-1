using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000044 RID: 68
	public class Vector3Plugin : ABSTweenPlugin<Vector3, Vector3, VectorOptions>
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00007484 File Offset: 0x00005684
		public override void Reset(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007488 File Offset: 0x00005688
		public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 value)
		{
			return value;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000748C File Offset: 0x0000568C
		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000074A8 File Offset: 0x000056A8
		public override void SetChangeValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				t.changeValue = new Vector3(t.endValue.x - t.startValue.x, 0f, 0f);
				return;
			}
			if (axisConstraint == AxisConstraint.Y)
			{
				t.changeValue = new Vector3(0f, t.endValue.y - t.startValue.y, 0f);
				return;
			}
			if (axisConstraint != AxisConstraint.Z)
			{
				t.changeValue = t.endValue - t.startValue;
				return;
			}
			t.changeValue = new Vector3(0f, 0f, t.endValue.z - t.startValue.z);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007570 File Offset: 0x00005770
		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000757C File Offset: 0x0000577C
		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
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
				Vector3 vector = getter();
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
				Vector3 vector2 = getter();
				vector2.y = startValue.y + changeValue.y * num;
				if (options.snapping)
				{
					vector2.y = (float)Math.Round((double)vector2.y);
				}
				setter(vector2);
				return;
			}
			if (axisConstraint != AxisConstraint.Z)
			{
				startValue.x += changeValue.x * num;
				startValue.y += changeValue.y * num;
				startValue.z += changeValue.z * num;
				if (options.snapping)
				{
					startValue.x = (float)Math.Round((double)startValue.x);
					startValue.y = (float)Math.Round((double)startValue.y);
					startValue.z = (float)Math.Round((double)startValue.z);
				}
				setter(startValue);
				return;
			}
			Vector3 vector3 = getter();
			vector3.z = startValue.z + changeValue.z * num;
			if (options.snapping)
			{
				vector3.z = (float)Math.Round((double)vector3.z);
			}
			setter(vector3);
		}
	}
}
