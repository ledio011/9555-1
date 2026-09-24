using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000043 RID: 67
	public class Vector3ArrayPlugin : ABSTweenPlugin<Vector3, Vector3[], Vector3ArrayOptions>
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00006F78 File Offset: 0x00005178
		public override void Reset(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006FA0 File Offset: 0x000051A0
		public override Vector3[] ConvertToStartValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, Vector3 value)
		{
			int num = t.endValue.Length;
			Vector3[] array = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				if (i == 0)
				{
					array[i] = value;
				}
				else
				{
					array[i] = t.endValue[i - 1];
				}
			}
			return array;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006FEC File Offset: 0x000051EC
		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				if (i > 0)
				{
					t.startValue[i] = t.endValue[i - 1];
				}
				t.endValue[i] = t.startValue[i] + t.endValue[i];
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007058 File Offset: 0x00005258
		public override void SetChangeValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			int num = t.endValue.Length;
			t.changeValue = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				t.changeValue[i] = t.endValue[i] - t.startValue[i];
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000070B0 File Offset: 0x000052B0
		public override float GetSpeedBasedDuration(Vector3ArrayOptions options, float unitsXSecond, Vector3[] changeValue)
		{
			float num = 0f;
			int num2 = changeValue.Length;
			for (int i = 0; i < num2; i++)
			{
				float num3 = changeValue[i].magnitude / options.durations[i];
				options.durations[i] = num3;
				num += num3;
			}
			return num;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000070F8 File Offset: 0x000052F8
		public override void EvaluateAndApply(Vector3ArrayOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3[] startValue, Vector3[] changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			Vector3 vector = Vector3.zero;
			if (t.loopType == LoopType.Incremental)
			{
				int num = t.isComplete ? (t.completedLoops - 1) : t.completedLoops;
				if (num > 0)
				{
					int num2 = startValue.Length - 1;
					vector = (startValue[num2] + changeValue[num2] - startValue[0]) * (float)num;
				}
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				int num3 = ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				if (num3 > 0)
				{
					int num4 = startValue.Length - 1;
					vector += (startValue[num4] + changeValue[num4] - startValue[0]) * (float)num3;
				}
			}
			int num5 = 0;
			float num6 = 0f;
			float num7 = 0f;
			int num8 = options.durations.Length;
			float num9 = 0f;
			for (int i = 0; i < num8; i++)
			{
				num7 = options.durations[i];
				num9 += num7;
				if (elapsed <= num9)
				{
					num5 = i;
					num6 = elapsed - num6;
					break;
				}
				num6 += num7;
			}
			float num10 = EaseManager.Evaluate(t.easeType, t.customEase, num6, num7, t.easeOvershootOrAmplitude, t.easePeriod);
			AxisConstraint axisConstraint = options.axisConstraint;
			Vector3 vector2;
			if (axisConstraint == AxisConstraint.X)
			{
				vector2 = getter();
				vector2.x = startValue[num5].x + vector.x + changeValue[num5].x * num10;
				if (options.snapping)
				{
					vector2.x = (float)Math.Round((double)vector2.x);
				}
				setter(vector2);
				return;
			}
			if (axisConstraint == AxisConstraint.Y)
			{
				vector2 = getter();
				vector2.y = startValue[num5].y + vector.y + changeValue[num5].y * num10;
				if (options.snapping)
				{
					vector2.y = (float)Math.Round((double)vector2.y);
				}
				setter(vector2);
				return;
			}
			if (axisConstraint != AxisConstraint.Z)
			{
				vector2.x = startValue[num5].x + vector.x + changeValue[num5].x * num10;
				vector2.y = startValue[num5].y + vector.y + changeValue[num5].y * num10;
				vector2.z = startValue[num5].z + vector.z + changeValue[num5].z * num10;
				if (options.snapping)
				{
					vector2.x = (float)Math.Round((double)vector2.x);
					vector2.y = (float)Math.Round((double)vector2.y);
					vector2.z = (float)Math.Round((double)vector2.z);
				}
				setter(vector2);
				return;
			}
			vector2 = getter();
			vector2.z = startValue[num5].z + vector.z + changeValue[num5].z * num10;
			if (options.snapping)
			{
				vector2.z = (float)Math.Round((double)vector2.z);
			}
			setter(vector2);
		}
	}
}
