using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200095B RID: 2395
public class MissionPassShowRootLogic : SingletonUnity<MissionPassShowRootLogic>
{
	// Token: 0x06004307 RID: 17159 RVA: 0x001499A4 File Offset: 0x00147BA4
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.CloseOtherPlayerUI();
	}

	// Token: 0x06004308 RID: 17160 RVA: 0x001499B0 File Offset: 0x00147BB0
	public void Reset(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID.Class != 0)
		{
			this.mShowRewardFlag = false;
			UnityVersionUtil.SetActiveRecursive(this.RewardRoot.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.BottomCollider.gameObject, false);
			vp_Timer.In(4f, delegate()
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MissionPassShowRoot);
			}, null);
		}
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100301}", new object[0]);
		this.onClickOK = null;
		NGUITools.SetActive(this.SuccessRoot, true);
		NGUITools.SetActive(this.FailRoot, false);
		NGUITools.SetActive(this.StarRootObj, false);
	}

	// Token: 0x06004309 RID: 17161 RVA: 0x00149A6C File Offset: 0x00147C6C
	public void ResetDailyMissionReward(List<item> items, DelegateDefine.NoParamDelegate onFinish = null)
	{
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100301}", new object[0]);
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.ShowRewardItem.ShowRewards(items);
		this.onClickOK = onFinish;
		NGUITools.SetActive(this.SuccessRoot, true);
		NGUITools.SetActive(this.FailRoot, false);
		NGUITools.SetActive(this.FailLineObj, false);
		NGUITools.SetActive(this.StarRootObj, false);
	}

	// Token: 0x0600430A RID: 17162 RVA: 0x00149AF8 File Offset: 0x00147CF8
	public void ResetSideMissionReward(string showDropId)
	{
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100301}", new object[0]);
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(showDropId);
		if (showRewardDataByID == null)
		{
			NGUITools.SetActive(this.ShowRewardItem.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.ShowRewardItem.gameObject, true);
			this.ShowRewardItem.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
		}
		NGUITools.SetActive(this.SuccessRoot, true);
		NGUITools.SetActive(this.FailRoot, false);
		NGUITools.SetActive(this.FailLineObj, false);
		NGUITools.SetActive(this.StarRootObj, false);
	}

	// Token: 0x0600430B RID: 17163 RVA: 0x00149BC0 File Offset: 0x00147DC0
	public void ResetDailyFinishReward(List<item> items, DelegateDefine.NoParamDelegate onFinish = null)
	{
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100311}", new object[0]);
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.ShowRewardItem.ShowRewards(items);
		this.onClickOK = onFinish;
		NGUITools.SetActive(this.SuccessRoot, true);
		NGUITools.SetActive(this.FailRoot, false);
		NGUITools.SetActive(this.FailLineObj, false);
		NGUITools.SetActive(this.StarRootObj, false);
	}

	// Token: 0x0600430C RID: 17164 RVA: 0x00149C4C File Offset: 0x00147E4C
	public void ResetNormalMissionReward(bool isSuccess, List<item> items, DelegateDefine.NoParamDelegate onFinish = null)
	{
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100301}", new object[0]);
		if (isSuccess)
		{
			NGUITools.SetActive(this.SuccessRoot, true);
			NGUITools.SetActive(this.FailRoot, false);
			NGUITools.SetActive(this.FailLineObj, false);
		}
		else
		{
			NGUITools.SetActive(this.SuccessRoot, false);
			NGUITools.SetActive(this.FailRoot, true);
			NGUITools.SetActive(this.SucessLineObj, false);
		}
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.ShowRewardItem.ShowRewards(items);
		this.onClickOK = onFinish;
		NGUITools.SetActive(this.StarRootObj, false);
	}

	// Token: 0x0600430D RID: 17165 RVA: 0x00149D04 File Offset: 0x00147F04
	public void ResetEquipSwipe(List<item> items)
	{
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{101522}", new object[0]);
		NGUITools.SetActive(this.SuccessRoot, true);
		NGUITools.SetActive(this.FailRoot, false);
		NGUITools.SetActive(this.FailLineObj, false);
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.ShowRewardItem.ShowRewards(items);
		NGUITools.SetActive(this.StarRootObj, false);
	}

	// Token: 0x0600430E RID: 17166 RVA: 0x00149D88 File Offset: 0x00147F88
	public void ResetTimeLimitMissionPassRoot(TimeLimitMissionData tlData, long restTime)
	{
		ShowRewardData showRewardData = null;
		if (restTime > tlData.Reward3Time)
		{
			NGUITools.SetActive(this.SuccessRoot, true);
			NGUITools.SetActive(this.FailRoot, false);
			if (!string.IsNullOrEmpty(tlData.ShowReward3))
			{
				showRewardData = DataManager.GetShowRewardDataByID(tlData.ShowReward3);
			}
		}
		else if (restTime > tlData.Reward2Time)
		{
			NGUITools.SetActive(this.StarObjList[2], false);
			NGUITools.SetActive(this.SuccessRoot, true);
			NGUITools.SetActive(this.FailRoot, false);
			if (!string.IsNullOrEmpty(tlData.ShowReward2))
			{
				showRewardData = DataManager.GetShowRewardDataByID(tlData.ShowReward2);
			}
		}
		else if (restTime > tlData.Reward1Time)
		{
			NGUITools.SetActive(this.StarObjList[2], false);
			NGUITools.SetActive(this.StarObjList[1], false);
			NGUITools.SetActive(this.SuccessRoot, true);
			NGUITools.SetActive(this.FailRoot, false);
			if (!string.IsNullOrEmpty(tlData.ShowReward1))
			{
				showRewardData = DataManager.GetShowRewardDataByID(tlData.ShowReward1);
			}
		}
		else
		{
			NGUITools.SetActive(this.StarRootObj, false);
			NGUITools.SetActive(this.SuccessRoot, false);
			NGUITools.SetActive(this.FailRoot, true);
		}
		if (showRewardData != null)
		{
			this.ShowRewardItem.ShowRewards(showRewardData.ItemIdList, showRewardData.QualityList, showRewardData.CountList);
		}
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.TimeLabel.text = StrDictionary.GetDictionaryString("#{100303}", new object[]
		{
			this.mCurSecond
		});
	}

	// Token: 0x0600430F RID: 17167 RVA: 0x00149F20 File Offset: 0x00148120
	private void Update()
	{
		if (this.mShowRewardFlag)
		{
			this.mTimeCount = Time.time - this.mStartTime;
			if ((int)((float)this.mWaitCloseTime - this.mTimeCount) < this.mCurSecond)
			{
				this.mCurSecond = (int)((float)this.mWaitCloseTime - this.mTimeCount);
				this.TimeLabel.text = StrDictionary.GetDictionaryString("#{100303}", new object[]
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

	// Token: 0x06004310 RID: 17168 RVA: 0x00149FBC File Offset: 0x001481BC
	public void OnClickOKBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MissionPassShowRoot);
		if (this.onClickOK != null)
		{
			this.onClickOK();
		}
	}

	// Token: 0x06004311 RID: 17169 RVA: 0x00149FE4 File Offset: 0x001481E4
	public bool IsShowReward()
	{
		return this.mShowRewardFlag;
	}

	// Token: 0x04002F78 RID: 12152
	public GameObject RewardRoot;

	// Token: 0x04002F79 RID: 12153
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002F7A RID: 12154
	public GameObject BottomCollider;

	// Token: 0x04002F7B RID: 12155
	public UILabel TimeLabel;

	// Token: 0x04002F7C RID: 12156
	public UILabel TitleLabel;

	// Token: 0x04002F7D RID: 12157
	public GameObject SuccessRoot;

	// Token: 0x04002F7E RID: 12158
	public GameObject FailRoot;

	// Token: 0x04002F7F RID: 12159
	private bool mShowRewardFlag;

	// Token: 0x04002F80 RID: 12160
	private float mStartTime;

	// Token: 0x04002F81 RID: 12161
	private int mCurSecond;

	// Token: 0x04002F82 RID: 12162
	private float mTimeCount;

	// Token: 0x04002F83 RID: 12163
	public GameObject SucessLineObj;

	// Token: 0x04002F84 RID: 12164
	public GameObject FailLineObj;

	// Token: 0x04002F85 RID: 12165
	public GameObject StarRootObj;

	// Token: 0x04002F86 RID: 12166
	public GameObject[] StarObjList;

	// Token: 0x04002F87 RID: 12167
	private int mWaitCloseTime = 15;

	// Token: 0x04002F88 RID: 12168
	private DelegateDefine.NoParamDelegate onClickOK;
}
