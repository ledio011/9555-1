using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x0600047E RID: 1150 RVA: 0x0001FF88 File Offset: 0x0001E188
	private void Cache()
	{
		this.mCached = true;
		this.mWidget = base.GetComponent<UIWidget>();
		Renderer renderer = base.renderer;
		if (renderer != null)
		{
			this.mMat = renderer.material;
		}
		this.mLight = base.light;
		if (this.mWidget == null && this.mMat == null && this.mLight == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x0600047F RID: 1151 RVA: 0x00020014 File Offset: 0x0001E214
	// (set) Token: 0x06000480 RID: 1152 RVA: 0x0002001C File Offset: 0x0001E21C
	[Obsolete("Use 'value' instead")]
	public Color color
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

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000481 RID: 1153 RVA: 0x00020028 File Offset: 0x0001E228
	// (set) Token: 0x06000482 RID: 1154 RVA: 0x000200A4 File Offset: 0x0001E2A4
	public Color value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			return Color.black;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00020144 File Offset: 0x0001E344
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00020160 File Offset: 0x0001E360
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x000201A8 File Offset: 0x0001E3A8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x000201B8 File Offset: 0x0001E3B8
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x000201C8 File Offset: 0x0001E3C8
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x000201D8 File Offset: 0x0001E3D8
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000409 RID: 1033
	public Color from = Color.white;

	// Token: 0x0400040A RID: 1034
	public Color to = Color.white;

	// Token: 0x0400040B RID: 1035
	private bool mCached;

	// Token: 0x0400040C RID: 1036
	private UIWidget mWidget;

	// Token: 0x0400040D RID: 1037
	private Material mMat;

	// Token: 0x0400040E RID: 1038
	private Light mLight;
}
