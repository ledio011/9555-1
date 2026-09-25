using System;
using UnityEngine;

// Token: 0x020009DF RID: 2527
public class HPLineLogic : MonoBehaviour
{
	// Token: 0x060047CE RID: 18382 RVA: 0x0016F460 File Offset: 0x0016D660
	private void Update()
	{
		if (this.mChangeFlag)
		{
			this.tempVal = (Time.time - this.mStartLerpTime) / 0.5f;
			this.SetVal(this.tempVal);
			if (this.tempVal >= 1f)
			{
				this.mChangeFlag = false;
			}
		}
	}

	// Token: 0x060047CF RID: 18383 RVA: 0x0016F4B4 File Offset: 0x0016D6B4
	public void ChangeVal(float hpPercent)
	{
		this.ForceSetVal(hpPercent);
	}

	// Token: 0x060047D0 RID: 18384 RVA: 0x0016F4C0 File Offset: 0x0016D6C0
	public void ForceSetVal(float val)
	{
		this.mChangeFlag = false;
		this.mHpLine.value = val;
		if ((double)val < 0.0001)
		{
			this.HPLine.enabled = false;
		}
		else
		{
			this.HPLine.enabled = true;
		}
	}

	// Token: 0x060047D1 RID: 18385 RVA: 0x0016F510 File Offset: 0x0016D710
	public void SetVal(float val)
	{
		this.mHpLine.value = Mathf.Lerp(this.mPreVal, this.mTargetVal, val);
		if ((double)this.mHpLine.value < 0.0001)
		{
			this.HPLine.enabled = false;
		}
		else
		{
			this.HPLine.enabled = true;
		}
	}

	// Token: 0x04003524 RID: 13604
	private const float mLerpTime = 0.5f;

	// Token: 0x04003525 RID: 13605
	private bool mChangeFlag;

	// Token: 0x04003526 RID: 13606
	private float mStartLerpTime;

	// Token: 0x04003527 RID: 13607
	private float mPreVal;

	// Token: 0x04003528 RID: 13608
	private float mTargetVal;

	// Token: 0x04003529 RID: 13609
	public UISlider mHpLine;

	// Token: 0x0400352A RID: 13610
	public UISprite HPLine;

	// Token: 0x0400352B RID: 13611
	private float tempVal;
}
