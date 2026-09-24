using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200003B RID: 59
	public class QuaternionPlugin : ABSTweenPlugin<Quaternion, Vector3, QuaternionOptions>
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000587C File Offset: 0x00003A7C
		public override void Reset(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005880 File Offset: 0x00003A80
		public override Vector3 ConvertToStartValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Quaternion value)
		{
			return value.eulerAngles;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000588C File Offset: 0x00003A8C
		public override void SetRelativeEndValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000058A8 File Offset: 0x00003AA8
		public override void SetChangeValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			if (t.plugOptions.rotateMode == RotateMode.Fast && !t.isRelative)
			{
				Vector3 endValue = t.endValue;
				if (endValue.x > 360f)
				{
					endValue.x %= 360f;
				}
				if (endValue.y > 360f)
				{
					endValue.y %= 360f;
				}
				if (endValue.z > 360f)
				{
					endValue.z %= 360f;
				}
				Vector3 vector = endValue - t.startValue;
				float num = (vector.x > 0f) ? vector.x : (-vector.x);
				if (num > 180f)
				{
					vector.x = ((vector.x > 0f) ? (-(360f - num)) : (360f - num));
				}
				num = ((vector.y > 0f) ? vector.y : (-vector.y));
				if (num > 180f)
				{
					vector.y = ((vector.y > 0f) ? (-(360f - num)) : (360f - num));
				}
				num = ((vector.z > 0f) ? vector.z : (-vector.z));
				if (num > 180f)
				{
					vector.z = ((vector.z > 0f) ? (-(360f - num)) : (360f - num));
				}
				t.changeValue = vector;
				return;
			}
			if (t.plugOptions.rotateMode == RotateMode.FastBeyond360 || t.isRelative)
			{
				t.changeValue = t.endValue - t.startValue;
				return;
			}
			t.changeValue = t.endValue;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005A64 File Offset: 0x00003C64
		public override float GetSpeedBasedDuration(QuaternionOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005A70 File Offset: 0x00003C70
		public override void EvaluateAndApply(QuaternionOptions options, Tween t, bool isRelative, DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			Vector3 vector = startValue;
			if (t.loopType == LoopType.Incremental)
			{
				vector += changeValue * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				vector += changeValue * (float)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			RotateMode rotateMode = options.rotateMode;
			if (rotateMode != RotateMode.WorldAxisAdd && rotateMode != RotateMode.LocalAxisAdd)
			{
				vector.x += changeValue.x * num;
				vector.y += changeValue.y * num;
				vector.z += changeValue.z * num;
				setter(Quaternion.Euler(vector));
				return;
			}
			Quaternion quaternion = Quaternion.Euler(startValue);
			vector.x = changeValue.x * num;
			vector.y = changeValue.y * num;
			vector.z = changeValue.z * num;
			if (options.rotateMode == RotateMode.WorldAxisAdd)
			{
				setter(quaternion * Quaternion.Inverse(quaternion) * Quaternion.Euler(vector) * quaternion);
				return;
			}
			setter(quaternion * Quaternion.Euler(vector));
		}
	}
}
