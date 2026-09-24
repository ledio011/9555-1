using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x060004DF RID: 1247 RVA: 0x00020E54 File Offset: 0x0001F054
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

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00020E7C File Offset: 0x0001F07C
	// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00020E84 File Offset: 0x0001F084
	[Obsolete("Use 'value' instead")]
	public int width
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

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00020E90 File Offset: 0x0001F090
	// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00020EA0 File Offset: 0x0001F0A0
	public int value
	{
		get
		{
			return this.cachedWidget.width;
		}
		set
		{
			this.cachedWidget.width = value;
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00020EB0 File Offset: 0x0001F0B0
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

	// Token: 0x060004E5 RID: 1253 RVA: 0x00020F34 File Offset: 0x0001F134
	public static TweenWidth Begin(UIWidget widget, float duration, int width)
	{
		TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(widget.gameObject, duration);
		tweenWidth.from = widget.width;
		tweenWidth.to = width;
		if (duration <= 0f)
		{
			tweenWidth.Sample(1f, true);
			tweenWidth.enabled = false;
		}
		return tweenWidth;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00020F80 File Offset: 0x0001F180
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00020F90 File Offset: 0x0001F190
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00020FA0 File Offset: 0x0001F1A0
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00020FB0 File Offset: 0x0001F1B0
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000431 RID: 1073
	public int from = 100;

	// Token: 0x04000432 RID: 1074
	public int to = 100;

	// Token: 0x04000433 RID: 1075
	public bool updateTable;

	// Token: 0x04000434 RID: 1076
	private UIWidget mWidget;

	// Token: 0x04000435 RID: 1077
	private UITable mTable;
}
