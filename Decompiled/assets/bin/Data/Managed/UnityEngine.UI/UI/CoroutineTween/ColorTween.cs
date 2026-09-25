using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000042 RID: 66
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700006A RID: 106
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006438 File Offset: 0x00004638
		public Color startColor
		{
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006444 File Offset: 0x00004644
		public Color targetColor
		{
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006450 File Offset: 0x00004650
		public ColorTween.ColorTweenMode tweenMode
		{
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000645C File Offset: 0x0000465C
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006464 File Offset: 0x00004664
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006470 File Offset: 0x00004670
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006478 File Offset: 0x00004678
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006484 File Offset: 0x00004684
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color arg = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				arg.r = this.m_StartColor.r;
				arg.g = this.m_StartColor.g;
				arg.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				arg.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(arg);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006524 File Offset: 0x00004724
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006548 File Offset: 0x00004748
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x040000E0 RID: 224
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x040000E1 RID: 225
		private Color m_StartColor;

		// Token: 0x040000E2 RID: 226
		private Color m_TargetColor;

		// Token: 0x040000E3 RID: 227
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x040000E4 RID: 228
		private float m_Duration;

		// Token: 0x040000E5 RID: 229
		private bool m_IgnoreTimeScale;

		// Token: 0x02000043 RID: 67
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}

		// Token: 0x02000044 RID: 68
		public enum ColorTweenMode
		{
			// Token: 0x040000E7 RID: 231
			All,
			// Token: 0x040000E8 RID: 232
			RGB,
			// Token: 0x040000E9 RID: 233
			Alpha
		}
	}
}
