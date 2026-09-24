using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200099E RID: 2462
public class SlotRewardRootLogic : SingletonUnity<SlotRewardRootLogic>
{
	// Token: 0x060045C0 RID: 17856 RVA: 0x0015F94C File Offset: 0x0015DB4C
	public void Reset(List<item> items, DelegateDefine.NoParamDelegate okfun = null, bool showbtn = false)
	{
		this.mShowRewardFlag = true;
		if (showbtn)
		{
			UnityVersionUtil.SetActiveRecursive(this.OkBtnObj.gameObject, true);
			this.mWaitCloseTime = 10;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.OkBtnObj.gameObject, false);
			this.tweenPosAnima.to = this.tweenPosAnima.transform.parent.InverseTransformPoint(SingletonUnity<SlotUIRootLogic>.Instance.totalTra.position);
			this.rewardAnima.resetOnPlay = true;
			this.rewardAnima.Play(true);
			this.mWaitCloseTime = 2;
		}
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.ShowRewardItem.ShowRewards(items);
		this.onClickOK = okfun;
	}

	// Token: 0x060045C1 RID: 17857 RVA: 0x0015FA10 File Offset: 0x0015DC10
	private void Update()
	{
		if (this.mShowRewardFlag)
		{
			this.mTimeCount = Time.time - this.mStartTime;
			if ((int)((float)this.mWaitCloseTime - this.mTimeCount) < this.mCurSecond)
			{
				this.mCurSecond = (int)((float)this.mWaitCloseTime - this.mTimeCount);
				this.TimeLabel.text = StrDictionary.GetDictionaryString("{0}s", new object[]
				{
					this.mCurSecond
				});
			}
			if (this.mTimeCount >= (float)this.mWaitCloseTime)
			{
				this.OnClickOKBtn();
			}
		}
	}

	// Token: 0x060045C2 RID: 17858 RVA: 0x0015FAAC File Offset: 0x0015DCAC
	public void OnClickOKBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SlotRewardRoot);
		if (this.onClickOK != null)
		{
			this.onClickOK();
		}
	}

	// Token: 0x040032BD RID: 12989
	public ShowRewardItems ShowRewardItem;

	// Token: 0x040032BE RID: 12990
	private bool mShowRewardFlag;

	// Token: 0x040032BF RID: 12991
	private float mStartTime;

	// Token: 0x040032C0 RID: 12992
	private int mCurSecond;

	// Token: 0x040032C1 RID: 12993
	private float mTimeCount;

	// Token: 0x040032C2 RID: 12994
	private int mWaitCloseTime = 2;

	// Token: 0x040032C3 RID: 12995
	private DelegateDefine.NoParamDelegate onClickOK;

	// Token: 0x040032C4 RID: 12996
	public GameObject OkBtnObj;

	// Token: 0x040032C5 RID: 12997
	public UILabel TimeLabel;

	// Token: 0x040032C6 RID: 12998
	public UIPlayTween rewardAnima;

	// Token: 0x040032C7 RID: 12999
	public TweenPosition tweenPosAnima;
}
