using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001FE60 File Offset: 0x0001E060
	public UIRect cachedRect
	{
		get
		{
			if (this.mRect == null)
			{
				this.mRect = base.GetComponent<UIRect>();
				if (this.mRect == null)
				{
					this.mRect = base.GetComponentInChildren<UIRect>();
				}
			}
			return this.mRect;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
	// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
	[Obsolete("Use 'value' instead")]
	public float alpha
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
	// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001FED4 File Offset: 0x0001E0D4
	public float value
	{
		get
		{
			return this.cachedRect.alpha;
		}
		set
		{
			this.cachedRect.alpha = value;
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001FF00 File Offset: 0x0001E100
	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0001FF48 File Offset: 0x0001E148
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001FF58 File Offset: 0x0001E158
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000406 RID: 1030
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000407 RID: 1031
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000408 RID: 1032
	private UIRect mRect;
}
