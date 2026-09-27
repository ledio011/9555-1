using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000057 RID: 87
	public static class TweenSettingsExtensions
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00008FC8 File Offset: 0x000071C8
		public static T SetId<T>(this T t, object id) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.id = id;
			return t;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008FF0 File Offset: 0x000071F0
		public static T SetTarget<T>(this T t, object target) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.target = target;
			return t;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009018 File Offset: 0x00007218
		public static T SetLoops<T>(this T t, int loops) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			if (t.tweenType == TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000090A4 File Offset: 0x000072A4
		public static T SetEase<T>(this T t, Ease ease) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				t.easeOvershootOrAmplitude = (float)((int)t.easeOvershootOrAmplitude);
			}
			t.customEase = null;
			return t;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00009104 File Offset: 0x00007304
		public static T SetUpdate<T>(this T t, bool isIndependentUpdate) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, DOTween.defaultUpdateType, isIndependentUpdate);
			return t;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009130 File Offset: 0x00007330
		public static T OnUpdate<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onUpdate = action;
			return t;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009158 File Offset: 0x00007358
		public static T OnComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onComplete = action;
			return t;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009180 File Offset: 0x00007380
		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}
	}
}
