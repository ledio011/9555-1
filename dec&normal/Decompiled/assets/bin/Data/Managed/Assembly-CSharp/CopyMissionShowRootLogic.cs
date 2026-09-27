using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000933 RID: 2355
public class CopyMissionShowRootLogic : SingletonUnity<CopyMissionShowRootLogic>
{
	// Token: 0x0600418C RID: 16780 RVA: 0x00138178 File Offset: 0x00136378
	public void ResetTowerCopy(copy_scene_result.request request)
	{
		NGUITools.SetActive(this.RankPvPObj, false);
		this.ShowRewardItem.ShowRewards(request.items);
		this.mStartTime = Time.time;
		this.isTowerCanContinue = (request.grade > 0L);
		TowerSceneManager towerSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as TowerSceneManager;
		int currentFloor = towerSceneManager.CurrentFloor;
		TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(currentFloor);
		if (towerDataByFloorID != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (playerData.CheckLevel(towerDataByFloorID.LimitLevel))
			{
				NGUITools.SetActive(this.ContinueObj, request.grade > 0L);
			}
			else
			{
				NGUITools.SetActive(this.ContinueObj, false);
			}
		}
		else
		{
			NGUITools.SetActive(this.ContinueObj, false);
		}
		this.mCurTimeCount = this.mWaitTime;
		this.ContinueTimelabel.text = string.Format("{0}s", this.mWaitTime);
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100753}", new object[0]);
		this.ContentLabel.text = StrDictionary.GetDictionaryString("#{101579}", new object[0]);
		this.BtnGrid.Reposition();
	}

	// Token: 0x0600418D RID: 16781 RVA: 0x001382A4 File Offset: 0x001364A4
	public void ResetRankPvP(tiantti_result.request request)
	{
		this.ContentLabel.text = string.Empty;
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100753}", new object[0]);
		if (request.HasBestRankPos)
		{
			NGUITools.SetActive(this.BestRankObj, true);
			this.BestLabel.text = string.Format("{0}", request.bestRankPos);
		}
		else
		{
			NGUITools.SetActive(this.BestRankObj, false);
		}
		this.ShowRewardItem.ShowRewards(request.items);
		NGUITools.SetActive(this.NowRankObj, true);
		this.CurLabel.text = request.rankPos1.ToString();
		this.NextLabel.text = request.rankPos2.ToString();
		NGUITools.SetActive(this.LevelUpObj, request.rankPos1 - request.rankPos2 > 0L);
		NGUITools.SetActive(this.ContinueObj, false);
		this.BtnGrid.Reposition();
		this.RankGrid.Reposition();
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.RANK_PVP);
	}

	// Token: 0x0600418E RID: 16782 RVA: 0x001383B8 File Offset: 0x001365B8
	public void OnClickLeave()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CopyMissionShowRoot);
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x0600418F RID: 16783 RVA: 0x001383D8 File Offset: 0x001365D8
	public void OnClickContinue()
	{
		if (UnityVersionUtil.IsActive(this.ContinueObj))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CopyMissionShowRoot);
			TowerSceneManager towerSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as TowerSceneManager;
			if (towerSceneManager != null)
			{
				towerSceneManager.GoNextLevel();
			}
		}
		else
		{
			this.OnClickLeave();
		}
	}

	// Token: 0x06004190 RID: 16784 RVA: 0x0013842C File Offset: 0x0013662C
	private void Update()
	{
		if (this.isTowerCanContinue)
		{
			this.tempTime = (int)(Time.time - this.mStartTime);
			if (this.mCurTimeCount != this.mWaitTime - this.tempTime)
			{
				this.mCurTimeCount = this.mWaitTime - this.tempTime;
				if (this.mCurTimeCount < 0)
				{
					this.ContinueTimelabel.text = string.Empty;
					this.isTowerCanContinue = false;
					this.OnClickContinue();
				}
				else
				{
					this.ContinueTimelabel.text = string.Format("{0}s", this.mCurTimeCount);
				}
			}
		}
	}

	// Token: 0x06004191 RID: 16785 RVA: 0x001384D0 File Offset: 0x001366D0
	private void OnEnable()
	{
		this.isTowerCanContinue = false;
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
		}
		SingletonUnity<UIManager>.Instance.CloseOtherPlayerUI();
	}

	// Token: 0x06004192 RID: 16786 RVA: 0x00138514 File Offset: 0x00136714
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
	}

	// Token: 0x04002D61 RID: 11617
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002D62 RID: 11618
	public GameObject RankPvPObj;

	// Token: 0x04002D63 RID: 11619
	public UIGrid BtnGrid;

	// Token: 0x04002D64 RID: 11620
	public GameObject ContinueObj;

	// Token: 0x04002D65 RID: 11621
	public UILabel ContentLabel;

	// Token: 0x04002D66 RID: 11622
	public UILabel TitleLabel;

	// Token: 0x04002D67 RID: 11623
	public GameObject BestRankObj;

	// Token: 0x04002D68 RID: 11624
	public GameObject NowRankObj;

	// Token: 0x04002D69 RID: 11625
	public UILabel BestLabel;

	// Token: 0x04002D6A RID: 11626
	public UIGrid RankGrid;

	// Token: 0x04002D6B RID: 11627
	public UILabel CurLabel;

	// Token: 0x04002D6C RID: 11628
	public UILabel NextLabel;

	// Token: 0x04002D6D RID: 11629
	public GameObject LevelUpObj;

	// Token: 0x04002D6E RID: 11630
	private bool isTowerCanContinue;

	// Token: 0x04002D6F RID: 11631
	private int mWaitTime = 12;

	// Token: 0x04002D70 RID: 11632
	private int mCurTimeCount;

	// Token: 0x04002D71 RID: 11633
	private float mStartTime;

	// Token: 0x04002D72 RID: 11634
	private int tempTime;

	// Token: 0x04002D73 RID: 11635
	public UILabel ContinueTimelabel;
}
