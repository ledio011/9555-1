using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000932 RID: 2354
public class CopyFailShowRootLogic : SingletonUnity<CopyFailShowRootLogic>
{
	// Token: 0x06004183 RID: 16771 RVA: 0x00137D28 File Offset: 0x00135F28
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.CloseOtherPlayerUI();
	}

	// Token: 0x06004184 RID: 16772 RVA: 0x00137D34 File Offset: 0x00135F34
	public void ResetNormalCopy()
	{
		this.isMission = false;
		this.mShowRewardFlag = false;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.TimeLabel.enabled = false;
		UnityVersionUtil.SetActiveRecursive(this.RewardRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.NoRewardRoot, true);
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100757}", new object[0]);
		this.ContentLabel.text = StrDictionary.GetDictionaryString("#{101578}", new object[0]);
		UnityVersionUtil.SetActiveRecursive(this.BtnRetryBtn, false);
		this.BtnGrid.Reposition();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.StopAutoAndSkill();
		}
		LocalDataSaveManager.SetDiedFlag(1);
	}

	// Token: 0x06004185 RID: 16773 RVA: 0x00137DFC File Offset: 0x00135FFC
	public void ResetTowerCopy(string id)
	{
		this.isMission = false;
		this.Id = id;
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.TimeLabel.enabled = true;
		UnityVersionUtil.SetActiveRecursive(this.RewardRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.NoRewardRoot, true);
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100757}", new object[0]);
		this.ContentLabel.text = StrDictionary.GetDictionaryString("#{101578}", new object[0]);
		UnityVersionUtil.SetActiveRecursive(this.BtnRetryBtn, true);
		this.BtnGrid.Reposition();
		this.type = 0;
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.StopAutoAndSkill();
		}
		LocalDataSaveManager.SetDiedFlag(1);
	}

	// Token: 0x06004186 RID: 16774 RVA: 0x00137ED0 File Offset: 0x001360D0
	public void ResetMission(List<item> items)
	{
		this.isMission = true;
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.TimeLabel.enabled = true;
		UnityVersionUtil.SetActiveRecursive(this.RewardRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.NoRewardRoot, false);
		this.ShowRewardItem.ShowRewards(items);
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100312}", new object[0]);
		this.ContentLabel.text = StrDictionary.GetDictionaryString("#{101578}", new object[0]);
		UnityVersionUtil.SetActiveRecursive(this.BtnRetryBtn, false);
		this.BtnGrid.Reposition();
	}

	// Token: 0x06004187 RID: 16775 RVA: 0x00137F80 File Offset: 0x00136180
	public void ResetRankPvp(List<item> items)
	{
		this.isMission = false;
		this.mShowRewardFlag = false;
		this.mStartTime = Time.time;
		this.mCurSecond = this.mWaitCloseTime;
		this.TimeLabel.enabled = false;
		UnityVersionUtil.SetActiveRecursive(this.RewardRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.NoRewardRoot, false);
		this.ShowRewardItem.ShowRewards(items);
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100312}", new object[0]);
		this.ContentLabel.text = StrDictionary.GetDictionaryString("#{101578}", new object[0]);
		UnityVersionUtil.SetActiveRecursive(this.BtnRetryBtn, false);
		this.BtnGrid.Reposition();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.StopAutoAndSkill();
		}
		LocalDataSaveManager.SetDiedFlag(1);
	}

	// Token: 0x06004188 RID: 16776 RVA: 0x00138054 File Offset: 0x00136254
	public void OnRetry()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CopyFailShowRoot);
		TowerSceneManager towerSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as TowerSceneManager;
		if (towerSceneManager != null)
		{
			towerSceneManager.Retry();
		}
	}

	// Token: 0x06004189 RID: 16777 RVA: 0x0013808C File Offset: 0x0013628C
	public void OnClickLeave()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CopyFailShowRoot);
		if (!this.isMission)
		{
			NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
		}
	}

	// Token: 0x0600418A RID: 16778 RVA: 0x001380C0 File Offset: 0x001362C0
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
				this.OnClickLeave();
				this.TimeLabel.enabled = false;
			}
		}
	}

	// Token: 0x04002D51 RID: 11601
	public UIGrid BtnGrid;

	// Token: 0x04002D52 RID: 11602
	public GameObject BtnRetryBtn;

	// Token: 0x04002D53 RID: 11603
	public GameObject RewardRoot;

	// Token: 0x04002D54 RID: 11604
	public GameObject NoRewardRoot;

	// Token: 0x04002D55 RID: 11605
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002D56 RID: 11606
	public UILabel TimeLabel;

	// Token: 0x04002D57 RID: 11607
	public UILabel TitleLabel;

	// Token: 0x04002D58 RID: 11608
	public UILabel ContentLabel;

	// Token: 0x04002D59 RID: 11609
	private bool mShowRewardFlag;

	// Token: 0x04002D5A RID: 11610
	private float mStartTime;

	// Token: 0x04002D5B RID: 11611
	private int mCurSecond;

	// Token: 0x04002D5C RID: 11612
	private float mTimeCount;

	// Token: 0x04002D5D RID: 11613
	private int mWaitCloseTime = 15;

	// Token: 0x04002D5E RID: 11614
	private string Id;

	// Token: 0x04002D5F RID: 11615
	private bool isMission;

	// Token: 0x04002D60 RID: 11616
	private int type;
}
