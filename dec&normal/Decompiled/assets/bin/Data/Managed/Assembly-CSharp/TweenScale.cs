using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00020924 File Offset: 0x0001EB24
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

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0002094C File Offset: 0x0001EB4C
	// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0002095C File Offset: 0x0001EB5C
	public Vector3 value
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0002096C File Offset: 0x0001EB6C
	// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00020974 File Offset: 0x0001EB74
	[Obsolete("Use 'value' instead")]
	public Vector3 scale
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

	// Token: 0x060004CA RID: 1226 RVA: 0x00020980 File Offset: 0x0001EB80
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
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

	// Token: 0x060004CB RID: 1227 RVA: 0x00020A08 File Offset: 0x0001EC08
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.value;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00020A50 File Offset: 0x0001EC50
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00020A60 File Offset: 0x0001EC60
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00020A70 File Offset: 0x0001EC70
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00020A80 File Offset: 0x0001EC80
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000422 RID: 1058
	public Vector3 from = Vector3.one;

	// Token: 0x04000423 RID: 1059
	public Vector3 to = Vector3.one;

	// Token: 0x04000424 RID: 1060
	public bool updateTable;

	// Token: 0x04000425 RID: 1061
	private Transform mTrans;

	// Token: 0x04000426 RID: 1062
	private UITable mTable;
}
