using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060004AC RID: 1196 RVA: 0x0002059C File Offset: 0x0001E79C
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060004AD RID: 1197 RVA: 0x000205C4 File Offset: 0x0001E7C4
	// (set) Token: 0x060004AE RID: 1198 RVA: 0x000205CC File Offset: 0x0001E7CC
	[Obsolete("Use 'value' instead")]
	public Vector3 position
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

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060004AF RID: 1199 RVA: 0x000205D8 File Offset: 0x0001E7D8
	// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0002060C File Offset: 0x0001E80C
	public Vector3 value
	{
		get
		{
			return (!this.worldSpace) ? this.cachedTransform.localPosition : this.cachedTransform.position;
		}
		set
		{
			if (this.mRect == null || !this.mRect.isAnchored || this.worldSpace)
			{
				if (this.worldSpace)
				{
					this.cachedTransform.position = value;
				}
				else
				{
					this.cachedTransform.localPosition = value;
				}
			}
			else
			{
				value -= this.cachedTransform.localPosition;
				NGUIMath.MoveRect(this.mRect, value.x, value.y);
			}
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x000206A0 File Offset: 0x0001E8A0
	private void Awake()
	{
		this.mRect = base.GetComponent<UIRect>();
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x000206B0 File Offset: 0x0001E8B0
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x000206DC File Offset: 0x0001E8DC
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00020724 File Offset: 0x0001E924
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00020734 File Offset: 0x0001E934
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00020744 File Offset: 0x0001E944
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00020754 File Offset: 0x0001E954
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x0400041A RID: 1050
	public Vector3 from;

	// Token: 0x0400041B RID: 1051
	public Vector3 to;

	// Token: 0x0400041C RID: 1052
	[HideInInspector]
	public bool worldSpace;

	// Token: 0x0400041D RID: 1053
	private Transform mTrans;

	// Token: 0x0400041E RID: 1054
	private UIRect mRect;
}
