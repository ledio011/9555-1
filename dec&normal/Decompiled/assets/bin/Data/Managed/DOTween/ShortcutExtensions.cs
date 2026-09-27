using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000049 RID: 73
	public static class ShortcutExtensions
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00008274 File Offset: 0x00006474
		public static Tweener DOColor(this Material target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000082B8 File Offset: 0x000064B8
		public static Tweener DOColor(this Material target, Color endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			return DOTween.To(() => target.GetColor(property), delegate(Color x)
			{
				target.SetColor(property, x);
			}, endValue, duration).SetTarget(target);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000832C File Offset: 0x0000652C
		public static Tweener DOFloat(this Material target, float endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			return DOTween.To(() => target.GetFloat(property), delegate(float x)
			{
				target.SetFloat(property, x);
			}, endValue, duration).SetTarget(target);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000083A0 File Offset: 0x000065A0
		public static Tweener DOMove(this Transform target, Vector3 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000083EC File Offset: 0x000065EC
		public static Tweener DOLocalMove(this Transform target, Vector3 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008438 File Offset: 0x00006638
		public static Tweener DORotate(this Transform target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.rotation, delegate(Quaternion x)
			{
				target.rotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000848C File Offset: 0x0000668C
		public static Tweener DOLocalRotate(this Transform target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.localRotation, delegate(Quaternion x)
			{
				target.localRotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000084E0 File Offset: 0x000066E0
		public static Tweener DOScale(this Transform target, Vector3 endValue, float duration)
		{
			return DOTween.To(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, endValue, duration).SetTarget(target);
		}
	}
}
