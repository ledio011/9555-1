using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000496 RID: 1174 RVA: 0x00020324 File Offset: 0x0001E524
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000497 RID: 1175 RVA: 0x0002034C File Offset: 0x0001E54C
	// (set) Token: 0x06000498 RID: 1176 RVA: 0x00020354 File Offset: 0x0001E554
	[Obsolete("Use 'value' instead")]
	public int height
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

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000499 RID: 1177 RVA: 0x00020360 File Offset: 0x0001E560
	// (set) Token: 0x0600049A RID: 1178 RVA: 0x00020370 File Offset: 0x0001E570
	public int value
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00020380 File Offset: 0x0001E580
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00020404 File Offset: 0x0001E604
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00020450 File Offset: 0x0001E650
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00020460 File Offset: 0x0001E660
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00020470 File Offset: 0x0001E670
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00020480 File Offset: 0x0001E680
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000412 RID: 1042
	public int from = 100;

	// Token: 0x04000413 RID: 1043
	public int to = 100;

	// Token: 0x04000414 RID: 1044
	public bool updateTable;

	// Token: 0x04000415 RID: 1045
	private UIWidget mWidget;

	// Token: 0x04000416 RID: 1046
	private UITable mTable;
}
