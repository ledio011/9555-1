using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0002076C File Offset: 0x0001E96C
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

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060004BA RID: 1210 RVA: 0x00020794 File Offset: 0x0001E994
	// (set) Token: 0x060004BB RID: 1211 RVA: 0x0002079C File Offset: 0x0001E99C
	[Obsolete("Use 'value' instead")]
	public Quaternion rotation
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

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060004BC RID: 1212 RVA: 0x000207A8 File Offset: 0x0001E9A8
	// (set) Token: 0x060004BD RID: 1213 RVA: 0x000207B8 File Offset: 0x0001E9B8
	public Quaternion value
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x000207C8 File Offset: 0x0001E9C8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Quaternion.Euler(new Vector3(Mathf.Lerp(this.from.x, this.to.x, factor), Mathf.Lerp(this.from.y, this.to.y, factor), Mathf.Lerp(this.from.z, this.to.z, factor)));
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002083C File Offset: 0x0001EA3C
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.value.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00020894 File Offset: 0x0001EA94
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x000208B8 File Offset: 0x0001EAB8
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x000208DC File Offset: 0x0001EADC
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x000208F0 File Offset: 0x0001EAF0
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}

	// Token: 0x0400041F RID: 1055
	public Vector3 from;

	// Token: 0x04000420 RID: 1056
	public Vector3 to;

	// Token: 0x04000421 RID: 1057
	private Transform mTrans;
}
