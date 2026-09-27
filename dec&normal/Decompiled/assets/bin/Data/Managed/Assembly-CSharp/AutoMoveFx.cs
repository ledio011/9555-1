using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public class AutoMoveFx : MonoBehaviour
{
	// Token: 0x060010C9 RID: 4297 RVA: 0x0006CFDC File Offset: 0x0006B1DC
	private void Start()
	{
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0006CFE0 File Offset: 0x0006B1E0
	private void Update()
	{
		this.mUseTime += Time.deltaTime;
		float num = this.mUseTime / this.mDuartion;
		num = Mathf.Clamp01(num);
		float num2 = 3f - 12f * (num - 0.5f) * (num - 0.5f);
		if (this.mTarget != null)
		{
			this.moveObj.position = Vector3.Lerp(this.mStartPos, this.mTarget.position + Vector3.up * 0.5f, num) + Vector3.up * num2;
		}
		else
		{
			this.moveObj.position = Vector3.Lerp(this.mStartPos, this.mTargetPos + Vector3.up * 0.5f, num) + Vector3.up * num2;
		}
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0006D0D0 File Offset: 0x0006B2D0
	public void Reset(float duration, Transform target)
	{
		this.mTarget = target;
		this.mDuartion = duration * 0.5f;
		this.mUseTime = 0f;
		this.moveObj = base.transform.FindChild("shouLei");
		this.moveObj.transform.localPosition = Vector3.zero;
		this.mStartPos = this.moveObj.position;
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0006D138 File Offset: 0x0006B338
	public void Reset(float duration, Vector3 targetPos)
	{
		this.mTarget = null;
		this.mTargetPos = targetPos;
		this.mDuartion = duration * 0.5f;
		this.mUseTime = 0f;
		this.moveObj = base.transform.FindChild("shouLei");
		this.moveObj.transform.localPosition = Vector3.zero;
		this.mStartPos = this.moveObj.position;
	}

	// Token: 0x0400144E RID: 5198
	private Vector3 mStartPos;

	// Token: 0x0400144F RID: 5199
	private Transform mTarget;

	// Token: 0x04001450 RID: 5200
	private float mDuartion;

	// Token: 0x04001451 RID: 5201
	private float mUseTime;

	// Token: 0x04001452 RID: 5202
	private Vector3 mTargetPos;

	// Token: 0x04001453 RID: 5203
	private Transform moveObj;
}
