using System;
using UnityEngine;

// Token: 0x02000923 RID: 2339
public class CarPointBestTimeRoot : SingletonUnity<CarPointBestTimeRoot>
{
	// Token: 0x06004100 RID: 16640 RVA: 0x001342B0 File Offset: 0x001324B0
	public void Reset(int curTime, int BestTime)
	{
		if (BestTime == -1)
		{
			NGUITools.SetActive(this.BestTimeRoot, false);
		}
		else
		{
			NGUITools.SetActive(this.BestTimeRoot, true);
			if (BestTime < curTime)
			{
				this.BestTimeDifLabel.color = Color.red;
				this.BestTimeDifLabel.text = string.Format("+{0}", TimeTools.GetCentiSecondStr(curTime - BestTime));
			}
			else
			{
				this.BestTimeDifLabel.color = Color.green;
				this.BestTimeDifLabel.text = string.Format("-{0}", TimeTools.GetCentiSecondStr(BestTime - curTime));
			}
		}
		this.CurTimeLabel.text = TimeTools.GetCentiSecondStr(curTime);
		vp_Timer.In(2f, delegate()
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarPointBestTimeRoot);
		}, null);
	}

	// Token: 0x04002CAD RID: 11437
	public UILabel CurTimeLabel;

	// Token: 0x04002CAE RID: 11438
	public UILabel BestTimeDifLabel;

	// Token: 0x04002CAF RID: 11439
	public GameObject BestTimeRoot;
}
