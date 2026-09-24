using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009BA RID: 2490
public class CreateTeamRootLogic : SingletonUnity<CreateTeamRootLogic>
{
	// Token: 0x060046D0 RID: 18128 RVA: 0x00167528 File Offset: 0x00165728
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060046D1 RID: 18129 RVA: 0x00167534 File Offset: 0x00165734
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060046D2 RID: 18130 RVA: 0x00167554 File Offset: 0x00165754
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x060046D3 RID: 18131 RVA: 0x00167560 File Offset: 0x00165760
	public void Init()
	{
		List<TeamData> teamDataList = DataManager.GetTeamDataList();
		Dictionary<string, TeamTargetTabData> dictionary = new Dictionary<string, TeamTargetTabData>();
		CopyData copyInfoData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData;
		for (int i = 0; i < teamDataList.Count; i++)
		{
			if (string.IsNullOrEmpty(teamDataList[i].CopyId) || copyInfoData.DailyCopyInfoDic.ContainsKey(teamDataList[i].CopyId))
			{
				string text = string.Empty;
				if (string.IsNullOrEmpty(teamDataList[i].TitleName))
				{
					if (teamDataList[i].GoalType == 1)
					{
						CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(teamDataList[i].CopyId);
						text = copySceneDataById.MName;
					}
				}
				else
				{
					text = teamDataList[i].MTitleName;
				}
				if (string.IsNullOrEmpty(teamDataList[i].ParentId))
				{
					TeamTargetTabData teamTargetTabData = new TeamTargetTabData();
					teamTargetTabData.Reset(text, new List<string>(), teamDataList[i].ID, new List<string>());
					dictionary.Add(teamDataList[i].ID, teamTargetTabData);
				}
				else
				{
					TeamTargetTabData teamTargetTabData2 = dictionary[teamDataList[i].ParentId];
					teamTargetTabData2.SubTitle.Add(text);
					teamTargetTabData2.SubKey.Add(teamDataList[i].ID);
				}
			}
		}
		this.TeamTargetTabRoot.Reset(new List<TeamTargetTabData>(dictionary.Values), new DelegateDefine.OneStringParamDelegate(this.OnClickLeftTab));
	}

	// Token: 0x060046D4 RID: 18132 RVA: 0x001676E8 File Offset: 0x001658E8
	public void Reset(bool isCreate, string goalId)
	{
		this.mIsCreatePage = isCreate;
		if (this.mIsCreatePage)
		{
			this.titleLabel.text = StrDictionary.GetDictionaryString("#{100801}", new object[0]);
		}
		else
		{
			this.titleLabel.text = StrDictionary.GetDictionaryString("#{100835}", new object[0]);
		}
		UnityVersionUtil.SetActiveRecursive(this.TeamTargetTabRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.CreateBtnRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.ChangeBtnRoot, false);
		this.targetGoalId = goalId;
		this.IsWorldFlag = true;
		this.WorldFlag.enabled = true;
		this.IsNearbyFlag = true;
		this.NearbyFlag.enabled = true;
		this.IsGangFlag = true;
		this.GangFlag.enabled = true;
	}

	// Token: 0x060046D5 RID: 18133 RVA: 0x001677AC File Offset: 0x001659AC
	public void RefreshPage()
	{
		UnityVersionUtil.SetActiveRecursive(this.TeamTargetTabRoot.gameObject, true);
		this.Init();
		if (this.mIsCreatePage)
		{
			NGUITools.SetActive(this.CreateBtnRoot, true);
			NGUITools.SetActive(this.ChangeBtnRoot, false);
			this.mIsAutoMatch = true;
			NGUITools.SetActive(this.AutoMatchPic, true);
			this.TeamTargetTabRoot.TargetTabLineList[0].OnClickTab();
		}
		else
		{
			NGUITools.SetActive(this.CreateBtnRoot, false);
			NGUITools.SetActive(this.ChangeBtnRoot, true);
			Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
			if (teamInfo.IsVertify)
			{
				this.mIsAutoMatch = true;
				NGUITools.SetActive(this.AutoMatchPic, true);
			}
			else
			{
				this.mIsAutoMatch = false;
				NGUITools.SetActive(this.AutoMatchPic, false);
			}
			bool flag = !string.IsNullOrEmpty(this.targetGoalId);
			bool flag2 = false;
			for (int i = 0; i < this.TeamTargetTabRoot.TargetTabLineList.Count; i++)
			{
				if (flag)
				{
					if (this.targetGoalId.Equals(this.TeamTargetTabRoot.TargetTabLineList[i].Key))
					{
						this.TeamTargetTabRoot.TargetTabLineList[i].OnClickTab();
						break;
					}
				}
				else if (teamInfo.TeamGoalData.ID.Equals(this.TeamTargetTabRoot.TargetTabLineList[i].Key))
				{
					this.TeamTargetTabRoot.TargetTabLineList[i].OnClickTab();
					break;
				}
				if (this.TeamTargetTabRoot.TargetTabLineList[i].HasSubLine)
				{
					for (int j = 0; j < this.TeamTargetTabRoot.TargetTabLineList[i].SubLineList.Count; j++)
					{
						if (flag)
						{
							if (this.TeamTargetTabRoot.TargetTabLineList[i].SubLineList[j].Key.Equals(this.targetGoalId))
							{
								this.TeamTargetTabRoot.TargetTabLineList[i].ArrowPicTw.PlayForward();
								this.TeamTargetTabRoot.TargetTabLineList[i].SubTabRootTw.PlayForward();
								this.TeamTargetTabRoot.TargetTabLineList[i].SubLineList[j].OnClickTab();
								flag2 = true;
								break;
							}
						}
						else if (this.TeamTargetTabRoot.TargetTabLineList[i].SubLineList[j].Key.Equals(teamInfo.TeamGoalData.ID))
						{
							this.TeamTargetTabRoot.TargetTabLineList[i].ArrowPicTw.PlayForward();
							this.TeamTargetTabRoot.TargetTabLineList[i].SubTabRootTw.PlayForward();
							this.TeamTargetTabRoot.TargetTabLineList[i].SubLineList[j].OnClickTab();
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060046D6 RID: 18134 RVA: 0x00167AD8 File Offset: 0x00165CD8
	private void OnInitializeFloorItem(GameObject obj, int index, int realIndex)
	{
		UILabel component = obj.GetComponent<UILabel>();
		component.text = Mathf.Abs(realIndex).ToString();
	}

	// Token: 0x060046D7 RID: 18135 RVA: 0x00167B00 File Offset: 0x00165D00
	private void LeftLevelCenterOn(Transform centerObj)
	{
		this.mCurLeftLevel = int.Parse(centerObj.gameObject.GetComponent<UILabel>().text);
	}

	// Token: 0x060046D8 RID: 18136 RVA: 0x00167B28 File Offset: 0x00165D28
	private void RightLevelCenterOn(Transform centerObj)
	{
		this.mCurRightLevel = int.Parse(centerObj.gameObject.GetComponent<UILabel>().text);
	}

	// Token: 0x060046D9 RID: 18137 RVA: 0x00167B50 File Offset: 0x00165D50
	private void OnClickLeftTab(string key)
	{
		this.ResetPage(key);
	}

	// Token: 0x060046DA RID: 18138 RVA: 0x00167B5C File Offset: 0x00165D5C
	public void OnClickCreateBtn()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_CREATE_BTN)
		{
			this.CheckTutorialEvent();
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(Mathf.Min(this.mCurLeftLevel, this.mCurRightLevel)))
		{
			NoticeLogic.AddNotifyData("#{101539}", true, false);
			return;
		}
		req_invite_team.request request = new req_invite_team.request();
		request.characterid = -1L;
		request.goalId = this.mCurChooseKey;
		request.minLevel = (long)Mathf.Min(this.mCurLeftLevel, this.mCurRightLevel);
		request.maxLevel = (long)Mathf.Max(this.mCurLeftLevel, this.mCurRightLevel);
		request.isVerfiy = ((!this.mIsAutoMatch) ? 1L : 0L);
		request.recruit = (long)this.GetRecruitNum();
		NetLogic.GetInstance().Send<Protocol.req_invite_team>(request, null);
		WaitResponseUIRootLogic.OpenWaitBox(109, 10f, 0f, null);
	}

	// Token: 0x060046DB RID: 18139 RVA: 0x00167C3C File Offset: 0x00165E3C
	public void OnClickChangeBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(Mathf.Min(this.mCurLeftLevel, this.mCurRightLevel)))
		{
			NoticeLogic.AddNotifyData("#{101539}", true, false);
			return;
		}
		req_change_team_goal.request request = new req_change_team_goal.request();
		request.goalId = this.mCurChooseKey;
		request.minLevel = (long)Mathf.Min(this.mCurLeftLevel, this.mCurRightLevel);
		request.maxLevel = (long)Mathf.Max(this.mCurLeftLevel, this.mCurRightLevel);
		request.isVerfiy = ((!this.mIsAutoMatch) ? 1L : 0L);
		request.recruit = (long)this.GetRecruitNum();
		NetLogic.GetInstance().Send<Protocol.req_change_team_goal>(request, null);
		this.OnClickCloseBtn();
	}

	// Token: 0x060046DC RID: 18140 RVA: 0x00167CF8 File Offset: 0x00165EF8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CreateTeamRoot);
		if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_CREATE_BTN)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x060046DD RID: 18141 RVA: 0x00167D1C File Offset: 0x00165F1C
	public void OnClickAutoMatchBtn()
	{
		if (this.mIsAutoMatch)
		{
			this.mIsAutoMatch = false;
			NGUITools.SetActive(this.AutoMatchPic, false);
		}
		else
		{
			this.mIsAutoMatch = true;
			NGUITools.SetActive(this.AutoMatchPic, true);
		}
	}

	// Token: 0x060046DE RID: 18142 RVA: 0x00167D60 File Offset: 0x00165F60
	public void ResetPage(string key)
	{
		int num = GameDefine.PLAYER_MAX_LEVEL;
		this.mCurChooseKey = key;
		TeamData teamDataDataByID = DataManager.GetTeamDataDataByID(this.mCurChooseKey);
		if (teamDataDataByID.GoalType == 1)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(teamDataDataByID.CopyId);
			NGUITools.SetActive(this.LimitObject, true);
			int minLevel = copySceneDataById.MinLevel;
			num = copySceneDataById.MaxLevel;
			this.mCurLeftLevel = minLevel;
			this.mCurRightLevel = num;
			this.LimitLevel.text = string.Format("Lv.{0} - Lv.{1} ", minLevel, num);
			this.LimitTime.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			CopyData copyInfoData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData;
			this.remainNum = (int)copyInfoData.DailyCopyInfoDic[copySceneDataById.ID].CurNum;
			this.LimitTimes.text = string.Format("{0}/{1}", this.remainNum, copySceneDataById.MaxPlayNum);
		}
		else
		{
			this.remainNum = 1;
			this.mCurLeftLevel = 1;
			this.mCurRightLevel = GameDefine.PLAYER_MAX_LEVEL;
			NGUITools.SetActive(this.LimitObject, false);
		}
	}

	// Token: 0x060046DF RID: 18143 RVA: 0x00167E88 File Offset: 0x00166088
	public void OnClickRecruitBtn()
	{
		if (this.IsWorldFlag)
		{
			this.WorldFlag.enabled = false;
		}
		else
		{
			this.WorldFlag.enabled = true;
		}
		this.IsWorldFlag = !this.IsWorldFlag;
	}

	// Token: 0x060046E0 RID: 18144 RVA: 0x00167EC4 File Offset: 0x001660C4
	public void OnClickFriendsBtn()
	{
		if (this.IsNearbyFlag)
		{
			this.NearbyFlag.enabled = false;
		}
		else
		{
			this.NearbyFlag.enabled = true;
		}
		this.IsNearbyFlag = !this.IsNearbyFlag;
	}

	// Token: 0x060046E1 RID: 18145 RVA: 0x00167F00 File Offset: 0x00166100
	public void OnClickGangBtn()
	{
		if (this.IsGangFlag)
		{
			this.GangFlag.enabled = false;
		}
		else
		{
			this.GangFlag.enabled = true;
		}
		this.IsGangFlag = !this.IsGangFlag;
	}

	// Token: 0x060046E2 RID: 18146 RVA: 0x00167F3C File Offset: 0x0016613C
	public int GetRecruitNum()
	{
		int num = 0;
		if (this.IsWorldFlag)
		{
			num++;
		}
		if (this.IsNearbyFlag)
		{
			num += 2;
		}
		if (this.IsGangFlag)
		{
			num += 4;
		}
		return num;
	}

	// Token: 0x040033DF RID: 13279
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040033E0 RID: 13280
	public TeamTargetTab TeamTargetTabRoot;

	// Token: 0x040033E1 RID: 13281
	public GameObject LimitObject;

	// Token: 0x040033E2 RID: 13282
	public UILabel titleLabel;

	// Token: 0x040033E3 RID: 13283
	public UILabel LimitLevel;

	// Token: 0x040033E4 RID: 13284
	public UILabel LimitTime;

	// Token: 0x040033E5 RID: 13285
	public UILabel LimitTimes;

	// Token: 0x040033E6 RID: 13286
	public GameObject CreateBtnRoot;

	// Token: 0x040033E7 RID: 13287
	public GameObject ChangeBtnRoot;

	// Token: 0x040033E8 RID: 13288
	public GameObject AutoMatchPic;

	// Token: 0x040033E9 RID: 13289
	private string mCurChooseKey;

	// Token: 0x040033EA RID: 13290
	private int mCurLeftLevel;

	// Token: 0x040033EB RID: 13291
	private int mCurRightLevel;

	// Token: 0x040033EC RID: 13292
	private bool mIsCreatePage;

	// Token: 0x040033ED RID: 13293
	private bool mIsAutoMatch;

	// Token: 0x040033EE RID: 13294
	private bool IsWorldFlag;

	// Token: 0x040033EF RID: 13295
	private bool IsNearbyFlag;

	// Token: 0x040033F0 RID: 13296
	private bool IsGangFlag;

	// Token: 0x040033F1 RID: 13297
	public UISprite WorldFlag;

	// Token: 0x040033F2 RID: 13298
	public UISprite NearbyFlag;

	// Token: 0x040033F3 RID: 13299
	public UISprite GangFlag;

	// Token: 0x040033F4 RID: 13300
	private string targetGoalId = string.Empty;

	// Token: 0x040033F5 RID: 13301
	private int remainNum;
}
