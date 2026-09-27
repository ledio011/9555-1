using System;
using UnityEngine;

// Token: 0x020008D2 RID: 2258
public class CarHPRootLogic : SingletonUnity<CarHPRootLogic>
{
	// Token: 0x06003CD7 RID: 15575 RVA: 0x0010C7D8 File Offset: 0x0010A9D8
	private void Start()
	{
		this.mTargetHPPercent = 1f;
	}

	// Token: 0x06003CD8 RID: 15576 RVA: 0x0010C7E8 File Offset: 0x0010A9E8
	private void Update()
	{
		if (this.HPTopLineSlider.value > this.mTargetHPPercent)
		{
			this.HPTopLineSlider.value -= 0.2f * Time.deltaTime;
			this.HPTopLineSlider.value = Mathf.Max(this.HPTopLineSlider.value, this.mTargetHPPercent);
		}
		if (this.HPBottomLineSlider.value > this.mTargetHPPercent)
		{
			this.HPBottomLineSlider.value -= 0.15f * Time.deltaTime;
			this.HPBottomLineSlider.value = Mathf.Max(this.HPBottomLineSlider.value, this.mTargetHPPercent);
		}
	}

	// Token: 0x06003CD9 RID: 15577 RVA: 0x0010C8A0 File Offset: 0x0010AAA0
	public void ChangeHP(int newHP)
	{
		this.mTargetHPPercent = (float)newHP / (float)ObjPlayerCar.PLAYERCAR_MAXHP;
	}

	// Token: 0x06003CDA RID: 15578 RVA: 0x0010C8B4 File Offset: 0x0010AAB4
	public void UpdateTime(int newTime)
	{
		if (this.mRestSec != newTime)
		{
			this.mRestSec = newTime;
			this.TimeLabel.text = string.Format("{0}:{1}", this.mRestSec / 60, this.mRestSec % 60);
		}
	}

	// Token: 0x0400282E RID: 10286
	public UILabel HPLabel;

	// Token: 0x0400282F RID: 10287
	public UISlider HPTopLineSlider;

	// Token: 0x04002830 RID: 10288
	public UISlider HPBottomLineSlider;

	// Token: 0x04002831 RID: 10289
	public UILabel TimeLabel;

	// Token: 0x04002832 RID: 10290
	private float mTargetHPPercent;

	// Token: 0x04002833 RID: 10291
	private int mRestSec;
}
