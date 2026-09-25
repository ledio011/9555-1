using System;
using UnityEngine;

// Token: 0x02000921 RID: 2337
public class CarBestTimeCountRoot : SingletonUnity<CarBestTimeCountRoot>
{
	// Token: 0x060040F7 RID: 16631 RVA: 0x00133FBC File Offset: 0x001321BC
	public void Reset(int bestTime)
	{
		if (bestTime == -1)
		{
			this.BestTimeLabel.text = string.Format("-- -- --", new object[0]);
		}
		else
		{
			this.BestTimeLabel.text = TimeTools.GetCentiSecondStr(bestTime);
		}
		this.CurTimeLabel.text = TimeTools.GetCentiSecondStr(0);
		this.mEnableTimeCountFlag = false;
		this.SetSpeedLable(0);
	}

	// Token: 0x060040F8 RID: 16632 RVA: 0x00134020 File Offset: 0x00132220
	public void SetSpeedLable(int speed)
	{
		if (speed < 0)
		{
			speed = 0;
		}
		this.SpeedLabel.text = string.Format("{0}", speed);
		if (speed <= 50)
		{
			this.SpeedLabel.color = this.green;
		}
		else if (speed <= 100)
		{
			this.SpeedLabel.color = this.yellow;
		}
		else if (speed <= 150)
		{
			this.SpeedLabel.color = this.oriange;
		}
		else
		{
			this.SpeedLabel.color = this.red;
		}
	}

	// Token: 0x060040F9 RID: 16633 RVA: 0x001340C0 File Offset: 0x001322C0
	public void EnableTimeCount(float startTime)
	{
		this.mEnableTimeCountFlag = true;
		this.mStartTime = startTime;
	}

	// Token: 0x060040FA RID: 16634 RVA: 0x001340D0 File Offset: 0x001322D0
	public void DisableTimeCount()
	{
		this.mEnableTimeCountFlag = false;
	}

	// Token: 0x060040FB RID: 16635 RVA: 0x001340DC File Offset: 0x001322DC
	private void Update()
	{
		if (this.mEnableTimeCountFlag)
		{
			this.mTempTime = (int)((Time.time - this.mStartTime) * 100f);
			if (this.mCurTime != this.mTempTime)
			{
				this.mCurTime = this.mTempTime;
				this.CurTimeLabel.text = TimeTools.GetCentiSecondStr(this.mCurTime);
			}
		}
	}

	// Token: 0x04002C9C RID: 11420
	public UILabel BestTimeLabel;

	// Token: 0x04002C9D RID: 11421
	public UILabel CurTimeLabel;

	// Token: 0x04002C9E RID: 11422
	public UILabel SpeedLabel;

	// Token: 0x04002C9F RID: 11423
	private bool mEnableTimeCountFlag;

	// Token: 0x04002CA0 RID: 11424
	private float mStartTime;

	// Token: 0x04002CA1 RID: 11425
	private Color green = new Color(0.7607843f, 1f, 0f, 1f);

	// Token: 0x04002CA2 RID: 11426
	private Color yellow = new Color(1f, 0.73333335f, 0f, 1f);

	// Token: 0x04002CA3 RID: 11427
	private Color oriange = new Color(1f, 0.5254902f, 0f, 1f);

	// Token: 0x04002CA4 RID: 11428
	private Color red = new Color(1f, 0.1882353f, 0f, 1f);

	// Token: 0x04002CA5 RID: 11429
	private int mTempTime;

	// Token: 0x04002CA6 RID: 11430
	private int mCurTime;
}
