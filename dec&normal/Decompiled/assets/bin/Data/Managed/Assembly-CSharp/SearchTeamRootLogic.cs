using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009BB RID: 2491
public class SearchTeamRootLogic : SingletonUnity<SearchTeamRootLogic>
{
	// Token: 0x17000FAF RID: 4015
	// (get) Token: 0x060046E4 RID: 18148 RVA: 0x00167F9C File Offset: 0x0016619C
	// (set) Token: 0x060046E5 RID: 18149 RVA: 0x00167FA4 File Offset: 0x001661A4
	public bool IsMatching
	{
		get
		{
			return this.mIsMatching;
		}
		set
		{
			this.mIsMatching = value;
		}
	}

	// Token: 0x060046E6 RID: 18150 RVA: 0x00167FB0 File Offset: 0x001661B0
	private void UpdateTeamList(string goalId)
	{
		get_team_list.request request = new get_team_list.request();
		if (!string.IsNullOrEmpty(goalId))
		{
			request.goalId = goalId;
		}
		NetLogic.GetInstance().Send<Protocol.get_team_list>(request, null);
	}

	// Token: 0x060046E7 RID: 18151 RVA: 0x00167FE4 File Offset: 0x001661E4
	public void Reset(string GoalId)
	{
		this.UpdateTeamList(GoalId);
		this.mResetGoalId = GoalId;
		UnityVersionUtil.SetActiveRecursive(this.TeamTargetTabRoot.gameObject, false);
		for (int i = 0; i < this.TeamLineList.Count; i++)
		{
			NGUITools.SetActive(this.TeamLineList[i].gameObject, false);
		}
	}

	// Token: 0x060046E8 RID: 18152 RVA: 0x00168044 File Offset: 0x00166244
	public void Init()
	{
		UnityVersionUtil.SetActiveRecursive(this.TeamTargetTabRoot.gameObject, true);
		for (int i = 0; i < this.TeamLineList.Count; i++)
		{
			NGUITools.SetActive(this.TeamLineList[i].gameObject, true);
		}
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<TeamData> teamDataList = DataManager.GetTeamDataList();
		Dictionary<string, TeamTargetTabData> dictionary = new Dictionary<string, TeamTargetTabData>();
		CopyData copyInfoData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData;
		for (int j = 0; j < teamDataList.Count; j++)
		{
			if (string.IsNullOrEmpty(teamDataList[j].CopyId) || copyInfoData.DailyCopyInfoDic.ContainsKey(teamDataList[j].CopyId))
			{
				string text = string.Empty;
				if (string.IsNullOrEmpty(teamDataList[j].TitleName))
				{
					if (teamDataList[j].GoalType == 1)
					{
						CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(teamDataList[j].CopyId);
						text = copySceneDataById.MName;
					}
				}
				else
				{
					text = teamDataList[j].MTitleName;
				}
				if (string.IsNullOrEmpty(teamDataList[j].ParentId))
				{
					TeamTargetTabData teamTargetTabData = new TeamTargetTabData();
					teamTargetTabData.Reset(text, new List<string>(), teamDataList[j].ID, new List<string>());
					dictionary.Add(teamDataList[j].ID, teamTargetTabData);
				}
				else
				{
					TeamTargetTabData teamTargetTabData2 = dictionary[teamDataList[j].ParentId];
					teamTargetTabData2.SubTitle.Add(text);
					teamTargetTabData2.SubKey.Add(teamDataList[j].ID);
				}
			}
		}
		this.TeamTargetTabRoot.Reset(new List<TeamTargetTabData>(dictionary.Values), new DelegateDefine.OneStringParamDelegate(this.OnClickLeftTab));
		if (string.IsNullOrEmpty(this.mResetGoalId))
		{
			this.TeamTargetTabRoot.TargetTabLineList[0].OnClickTab();
		}
		else
		{
			for (int k = 0; k < this.TeamTargetTabRoot.TargetTabLineList.Count; k++)
			{
				if (this.TeamTargetTabRoot.TargetTabLineList[k].Key.Equals(this.mResetGoalId))
				{
					this.TeamTargetTabRoot.TargetTabLineList[k].OnClickTab();
				}
			}
		}
	}

	// Token: 0x060046E9 RID: 18153 RVA: 0x001682BC File Offset: 0x001664BC
	private void OnClickLeftTab(string key)
	{
		get_team_list.request request = new get_team_list.request();
		if (!string.IsNullOrEmpty(key))
		{
			request.goalId = key;
		}
		NetLogic.GetInstance().Send<Protocol.get_team_list>(request, null);
		this.ResetPage(key);
	}

	// Token: 0x060046EA RID: 18154 RVA: 0x001682F4 File Offset: 0x001664F4
	public void ResetPage(string goalId)
	{
		this.mCurKey = goalId;
		if (this.mCurKey.Equals("1"))
		{
			NGUITools.SetActive(this.MatchBtnObj, false);
		}
		else
		{
			NGUITools.SetActive(this.MatchBtnObj, true);
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
		{
			NGUITools.SetActive(this.CreateBtn, false);
		}
		else
		{
			NGUITools.SetActive(this.CreateBtn, true);
		}
		this.UpdateMatchingLabel();
		if (this.mCurTeamList == null || this.mCurTeamList.Count == 0)
		{
			for (int i = 0; i < this.TeamLineList.Count; i++)
			{
				UnityVersionUtil.SetActiveRecursive(this.TeamLineList[i].gameObject, false);
			}
			NGUITools.SetActive(this.NoTeamObj, true);
			return;
		}
		NGUITools.SetActive(this.NoTeamObj, false);
		this.mCurPageTeamList.Clear();
		for (int j = 0; j < this.mCurTeamList.Count; j++)
		{
			if (this.mCurTeamList[j].goalId.Equals(this.mCurKey))
			{
				if (!this.mPlayerData.IsHaveTeam() || this.mCurTeamList[j].id != this.mPlayerData.TeamInfo.TeamID)
				{
					this.mCurPageTeamList.Add(this.mCurTeamList[j]);
				}
			}
		}
		if (this.mCurPageTeamList.Count == 0)
		{
			NGUITools.SetActive(this.NoTeamObj, true);
		}
		int num = this.mCurPageTeamList.Count - this.TeamLineList.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.TeamLineList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.TeamLineGrid.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				TeamLineRoot component = gameObject.GetComponent<TeamLineRoot>();
				this.TeamLineList.Add(component);
			}
		}
		for (int l = 0; l < this.TeamLineList.Count; l++)
		{
			if (l < this.mCurPageTeamList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.TeamLineList[l].gameObject, true);
				this.TeamLineList[l].Reset(this.mCurPageTeamList[l], this.mPlayerData.TeamInfo.HasAppliedTeam(this.mCurPageTeamList[l].id));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.TeamLineList[l].gameObject, false);
			}
		}
		this.TeamLineGrid.Reposition();
	}

	// Token: 0x060046EB RID: 18155 RVA: 0x001685E0 File Offset: 0x001667E0
	public void UpdateTeamList(List<team> teamList)
	{
		this.mCurTeamList = teamList;
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.ResetPage(this.mCurKey);
		}
	}

	// Token: 0x060046EC RID: 18156 RVA: 0x00168608 File Offset: 0x00166808
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SearchTeamRoot);
	}

	// Token: 0x060046ED RID: 18157 RVA: 0x0016861C File Offset: 0x0016681C
	public void OnClickFreshBtn()
	{
		if (Time.time < this.time)
		{
			NoticeLogic.AddNotifyData("#{100272}", true, false);
			return;
		}
		get_team_list.request request = new get_team_list.request();
		if (!string.IsNullOrEmpty(this.mCurKey))
		{
			request.goalId = this.mCurKey;
		}
		NetLogic.GetInstance().Send<Protocol.get_team_list>(request, null);
		this.time = Time.time + 2f;
	}

	// Token: 0x060046EE RID: 18158 RVA: 0x00168688 File Offset: 0x00166888
	public void OnClickCreateBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SearchTeamRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CreateTeamRoot, delegate
		{
			WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
			SingletonUnity<CreateTeamRootLogic>.Instance.Reset(true, this.mCurKey);
		}, null);
	}

	// Token: 0x060046EF RID: 18159 RVA: 0x001686C0 File Offset: 0x001668C0
	public void OnClickMatchingBtn()
	{
		if (string.IsNullOrEmpty(this.mCurKey) || this.mCurKey.Equals("1"))
		{
			return;
		}
		this.mCurCopyScene = DataManager.GetCopySceneDataById(this.mCurKey);
		if (!this.CheckLevel())
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam() && !playerData.IsTeamLeader())
		{
			NoticeLogic.AddNotifyData("#{102008}", true, false);
			return;
		}
		copyscene_info copyinfoByID = playerData.CopyInfoData.GetCopyinfoByID(this.mCurKey);
		int num = (int)copyinfoByID.CurNum;
		if (num <= 0)
		{
			if (!string.IsNullOrEmpty(this.mCurCopyScene.TimeInc))
			{
				PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				int itemStackNumById = playerData2.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.TimeInc);
				if (itemStackNumById > 0)
				{
					ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurCopyScene.TimeInc);
					MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101598}", new object[]
					{
						itemDataByID.MName
					}), "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickYesUseitemMatch), null, null, null);
					return;
				}
			}
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
			return;
		}
		this.MatchFun();
	}

	// Token: 0x060046F0 RID: 18160 RVA: 0x0016881C File Offset: 0x00166A1C
	public bool CheckLevel()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.mCurCopyScene.MinLevel);
	}

	// Token: 0x060046F1 RID: 18161 RVA: 0x00168838 File Offset: 0x00166A38
	public void OnClickYesUseitemMatch()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer itemBackPack = playerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.REMAIN, false, PROFESSION_TYPE.INVALID);
		bool flag = false;
		GameItem gameItem = null;
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			flag = false;
		}
		else
		{
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				if (targetTypeItem[i].ItemId.Equals(this.mCurCopyScene.TimeInc))
				{
					flag = true;
					gameItem = targetTypeItem[i];
					break;
				}
			}
		}
		if (flag)
		{
			use_item.request request = new use_item.request();
			request.indexId = gameItem.IndexId;
			NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(115, 10f, 0f, null);
			this.MatchFun();
			return;
		}
	}

	// Token: 0x060046F2 RID: 18162 RVA: 0x00168914 File Offset: 0x00166B14
	public void MatchFun()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		TeamData curCopyTeamData = DataManager.GetTeamDataDataByID(this.mCurCopyScene.ID);
		if (playerData.IsTeamLeader())
		{
			if (playerData.TeamInfo.IsVertify)
			{
				if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
				{
					stop_random_select_team.request request = new stop_random_select_team.request();
					request.id = playerData.TeamInfo.TeamGoalData.ID;
					request.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
					NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request, null);
				}
				else
				{
					MessageBoxLogic.OpenOKCancelBox("#{103204}", "#{100127}", delegate
					{
						req_change_team_goal.request request5 = new req_change_team_goal.request();
						request5.goalId = this.mCurCopyScene.ID;
						request5.minLevel = (long)this.mCurCopyScene.MinLevel;
						request5.maxLevel = (long)this.mCurCopyScene.MaxLevel;
						request5.isVerfiy = ((!playerData.TeamInfo.IsVertify) ? 1L : 0L);
						NetLogic.GetInstance().Send<Protocol.req_change_team_goal>(request5, null);
						random_select_team.request request6 = new random_select_team.request();
						request6.id = this.mCurCopyScene.ID;
						request6.type1 = (long)curCopyTeamData.GoalType;
						NetLogic.GetInstance().Send<Protocol.random_select_team>(request6, null);
					}, null, null, null);
				}
			}
			else if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				random_select_team.request request2 = new random_select_team.request();
				request2.id = this.mCurCopyScene.ID;
				request2.type1 = (long)curCopyTeamData.GoalType;
				NetLogic.GetInstance().Send<Protocol.random_select_team>(request2, null);
			}
			else
			{
				MessageBoxLogic.OpenOKCancelBox("#{103204}", "#{100127}", delegate
				{
					req_change_team_goal.request request5 = new req_change_team_goal.request();
					request5.goalId = this.mCurCopyScene.ID;
					request5.minLevel = (long)this.mCurCopyScene.MinLevel;
					request5.maxLevel = (long)this.mCurCopyScene.MaxLevel;
					request5.isVerfiy = ((!playerData.TeamInfo.IsVertify) ? 1L : 0L);
					NetLogic.GetInstance().Send<Protocol.req_change_team_goal>(request5, null);
					random_select_team.request request6 = new random_select_team.request();
					request6.id = this.mCurCopyScene.ID;
					request6.type1 = (long)curCopyTeamData.GoalType;
					NetLogic.GetInstance().Send<Protocol.random_select_team>(request6, null);
				}, null, null, null);
			}
		}
		else if (!playerData.IsHaveTeam())
		{
			if (playerData.TeamInfo.IsVertify)
			{
				if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
				{
					stop_random_select_team.request request3 = new stop_random_select_team.request();
					request3.id = playerData.TeamInfo.TeamGoalData.ID;
					request3.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
					NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request3, null);
				}
				else
				{
					MessageBoxLogic.OpenOKCancelBox("#{103204}", "#{100127}", delegate
					{
						stop_random_select_team.request request5 = new stop_random_select_team.request();
						request5.id = playerData.TeamInfo.TeamGoalData.ID;
						request5.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
						NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request5, null);
						random_select_team.request request6 = new random_select_team.request();
						request6.id = this.mCurCopyScene.ID;
						request6.type1 = (long)curCopyTeamData.GoalType;
						NetLogic.GetInstance().Send<Protocol.random_select_team>(request6, null);
					}, null, null, null);
				}
			}
			else
			{
				random_select_team.request request4 = new random_select_team.request();
				request4.id = this.mCurCopyScene.ID;
				request4.type1 = (long)curCopyTeamData.GoalType;
				NetLogic.GetInstance().Send<Protocol.random_select_team>(request4, null);
			}
		}
		this.UpdateMatchingLabel();
	}

	// Token: 0x060046F3 RID: 18163 RVA: 0x00168BAC File Offset: 0x00166DAC
	public void UpdateMatchingLabel()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsTeamLeader())
		{
			if (playerData.TeamInfo.IsVertify)
			{
				if (this.mCurKey.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
				{
					this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
					this.SetMatchAnima(true);
				}
				else
				{
					this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{100826}", new object[0]);
					this.SetMatchAnima(false);
				}
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{100826}", new object[0]);
				this.SetMatchAnima(false);
			}
		}
		else if (playerData.IsHaveTeam())
		{
			if (playerData.TeamInfo.IsVertify && this.mCurKey.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
				this.SetMatchAnima(true);
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{100826}", new object[0]);
				this.SetMatchAnima(false);
			}
		}
		else if (playerData.TeamInfo.IsVertify)
		{
			if (this.mCurKey.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
				this.SetMatchAnima(true);
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{100826}", new object[0]);
				this.SetMatchAnima(false);
			}
		}
		else
		{
			this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{100826}", new object[0]);
			this.SetMatchAnima(false);
		}
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x00168DA0 File Offset: 0x00166FA0
	public void SetMatchAnima(bool isshow)
	{
		if (isshow)
		{
			this.MatchAnima.enabled = true;
			this.MatchAnima.PlayForward();
			this.IsMatching = true;
		}
		else
		{
			this.MatchAnima.ResetToBeginning();
			this.MatchAnima.enabled = false;
			this.IsMatching = false;
		}
	}

	// Token: 0x040033F6 RID: 13302
	public TeamTargetTab TeamTargetTabRoot;

	// Token: 0x040033F7 RID: 13303
	public List<TeamLineRoot> TeamLineList;

	// Token: 0x040033F8 RID: 13304
	public UIGrid TeamLineGrid;

	// Token: 0x040033F9 RID: 13305
	private List<team> mCurTeamList;

	// Token: 0x040033FA RID: 13306
	private List<team> mCurPageTeamList = new List<team>();

	// Token: 0x040033FB RID: 13307
	private PlayerData mPlayerData;

	// Token: 0x040033FC RID: 13308
	private string mCurKey;

	// Token: 0x040033FD RID: 13309
	private string mResetGoalId = string.Empty;

	// Token: 0x040033FE RID: 13310
	private float time;

	// Token: 0x040033FF RID: 13311
	public GameObject NoTeamObj;

	// Token: 0x04003400 RID: 13312
	public GameObject MatchBtnObj;

	// Token: 0x04003401 RID: 13313
	public UILabel MatchBtnLabel;

	// Token: 0x04003402 RID: 13314
	public TweenAlpha MatchAnima;

	// Token: 0x04003403 RID: 13315
	public GameObject CreateBtn;

	// Token: 0x04003404 RID: 13316
	private bool mIsMatching;

	// Token: 0x04003405 RID: 13317
	private CopySceneData mCurCopyScene;
}
