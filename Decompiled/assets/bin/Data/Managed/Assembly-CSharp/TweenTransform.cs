using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x060004D1 RID: 1233 RVA: 0x00020A98 File Offset: 0x0001EC98
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00020C60 File Offset: 0x0001EE60
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00020C6C File Offset: 0x0001EE6C
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x04000427 RID: 1063
	public Transform from;

	// Token: 0x04000428 RID: 1064
	public Transform to;

	// Token: 0x04000429 RID: 1065
	public bool parentWhenFinished;

	// Token: 0x0400042A RID: 1066
	private Transform mTrans;

	// Token: 0x0400042B RID: 1067
	private Vector3 mPos;

	// Token: 0x0400042C RID: 1068
	private Quaternion mRot;

	// Token: 0x0400042D RID: 1069
	private Vector3 mScale;
}
