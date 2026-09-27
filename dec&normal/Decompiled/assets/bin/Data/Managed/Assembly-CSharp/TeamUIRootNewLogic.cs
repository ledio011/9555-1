using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009C9 RID: 2505
public class TeamUIRootNewLogic : SingletonUnity<TeamUIRootNewLogic>
{
	// Token: 0x17000FB7 RID: 4023
	// (get) Token: 0x06004748 RID: 18248 RVA: 0x0016B56C File Offset: 0x0016976C
	// (set) Token: 0x06004749 RID: 18249 RVA: 0x0016B574 File Offset: 0x00169774
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

	// Token: 0x0600474A RID: 18250 RVA: 0x0016B580 File Offset: 0x00169780
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(null, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), true, StrDictionary.GetDictionaryString("#{100251}", new object[0]));
		}, null);
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.Reset();
		this.mPreCount = 0;
	}

	// Token: 0x0600474B RID: 18251 RVA: 0x0016B5C8 File Offset: 0x001697C8
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_START || TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_SHOW_1 || TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_CLICK_URGE || TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_FINISH)
		{
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x0600474C RID: 18252 RVA: 0x0016B618 File Offset: 0x00169818
	public void Reset()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			if (this.mPlayerData.IsHaveTeam())
			{
				this.ResetTeamPage();
			}
			else
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamRootNew);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
			}
		}
	}

	// Token: 0x0600474D RID: 18253 RVA: 0x0016B670 File Offset: 0x00169870
	private void ResetTeamPage()
	{
		CopySceneData copySceneData = null;
		if (this.mPlayerData.TeamInfo.TeamGoalData.GoalType == 1)
		{
			copySceneData = DataManager.GetCopySceneDataById(this.mPlayerData.TeamInfo.TeamGoalData.CopyId);
		}
		this.PlayerPicLogicList[0].Reset(this.mPlayerData.TeamInfo.TeamLeader, false);
		for (int i = 1; i < this.PlayerPicLogicList.Count; i++)
		{
			if (this.mPlayerData.TeamInfo.TeamMembers[i - 1].IsValid())
			{
				this.PlayerPicLogicList[i].Reset(this.mPlayerData.TeamInfo.TeamMembers[i - 1], false);
			}
			else if (copySceneData != null && i >= copySceneData.MaxMember)
			{
				this.PlayerPicLogicList[i].Reset(null, true);
			}
			else
			{
				this.PlayerPicLogicList[i].Reset(null, false);
			}
		}
		if (this.mPlayerData.IsTeamLeader())
		{
			UnityVersionUtil.SetActiveRecursive(this.ApplyBtnRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.UrgeBtnRoot, false);
			if (this.mPlayerData.TeamInfo.TeamGoalData.GoalType == 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.StartBtnRoot, false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.StartBtnRoot, true);
				this.mPreCount = this.mPlayerData.TeamInfo.GetTeamberCount();
			}
			if (this.mPlayerData.TeamInfo.ApplyMemberDic.Count > 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.ApplyTipPic, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ApplyTipPic, false);
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ApplyBtnRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.StartBtnRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.ApplyTipPic, false);
			if (this.mPlayerData.TeamInfo.TeamGoalData.GoalType == 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.UrgeBtnRoot, false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.UrgeBtnRoot, true);
			}
		}
		if (this.mPlayerData.TeamInfo.IsVertify)
		{
			this.AutoMatchAnima.enabled = true;
			this.AutoMatchAnima.PlayForward();
			this.AutoMatchLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
			this.IsMatching = true;
		}
		else
		{
			this.AutoMatchAnima.ResetToBeginning();
			this.AutoMatchAnima.enabled = false;
			this.AutoMatchLabel.text = StrDictionary.GetDictionaryString("#{100826}", new object[0]);
			this.IsMatching = false;
		}
		if (!string.IsNullOrEmpty(this.mPlayerData.TeamInfo.TeamGoalData.TitleName))
		{
			this.TeamTargetLabel.text = this.mPlayerData.TeamInfo.TeamGoalData.MTitleName;
		}
		else
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.mPlayerData.TeamInfo.TeamGoalData.CopyId);
			this.TeamTargetLabel.text = copySceneDataById.MName;
		}
		this.TeamLimitLevelLabel.text = string.Format("Lv.{0}-{1}", this.mPlayerData.TeamInfo.MinLimitLevel, this.mPlayerData.TeamInfo.MaxLimitLevel);
		if (this.mPlayerData.IsTeamLeader())
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_START || TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_SHOW_1 || TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_CLICK_URGE || TutorialManager.CurStep == TUTORIAL_STEP.JOIN_TEAM_FINISH)
			{
				TutorialManager.CloseTutorial();
			}
		}
	}

	// Token: 0x0600474E RID: 18254 RVA: 0x0016BA18 File Offset: 0x00169C18
	public void OnApplyTeam()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			NGUITools.SetActive(this.ApplyTipPic.gameObject, true);
		}
	}

	// Token: 0x0600474F RID: 18255 RVA: 0x0016BA3C File Offset: 0x00169C3C
	public void OnClearApplyTeam()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			NGUITools.SetActive(this.ApplyTipPic.gameObject, false);
		}
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x0016BA60 File Offset: 0x00169C60
	public void OnClickListingBtn()
	{
		WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
		NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SearchTeamRoot, delegate
		{
			SingletonUnity<SearchTeamRootLogic>.Instance.Reset(string.Empty);
		}, null);
	}

	// Token: 0x06004751 RID: 18257 RVA: 0x0016BABC File Offset: 0x00169CBC
	public void OnClickApplyBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamApplyListRoot, null, null);
	}

	// Token: 0x06004752 RID: 18258 RVA: 0x0016BAD0 File Offset: 0x00169CD0
	public void OnClickStartBtn()
	{
		this.mPlayerData.TeamInfo.EnterMultiCopyAction();
	}

	// Token: 0x06004753 RID: 18259 RVA: 0x0016BAE4 File Offset: 0x00169CE4
	public void OnClickAutoMatchBtn()
	{
		if (this.mPlayerData.IsTeamLeader())
		{
			req_change_team_goal.request request = new req_change_team_goal.request();
			request.goalId = this.mPlayerData.TeamInfo.TeamGoalData.ID;
			request.minLevel = (long)this.mPlayerData.TeamInfo.MinLimitLevel;
			request.maxLevel = (long)this.mPlayerData.TeamInfo.MaxLimitLevel;
			request.isVerfiy = ((!this.mPlayerData.TeamInfo.IsVertify) ? 0L : 1L);
			NetLogic.GetInstance().Send<Protocol.req_change_team_goal>(request, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{102008}", true, false);
		}
	}

	// Token: 0x06004754 RID: 18260 RVA: 0x0016BB90 File Offset: 0x00169D90
	public void OnClickLeaveBtn()
	{
		leave_team.request request = new leave_team.request();
		request.teamid = this.mPlayerData.TeamInfo.TeamID;
		request.characterId = PlayerData.MainPlayerServerId;
		NetLogic.GetInstance().Send<Protocol.leave_team>(request, null);
	}

	// Token: 0x06004755 RID: 18261 RVA: 0x0016BBD0 File Offset: 0x00169DD0
	public void OnClickChangeTargetBtn()
	{
		if (this.mPlayerData.IsTeamLeader())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CreateTeamRoot, delegate
			{
				WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
				SingletonUnity<CreateTeamRootLogic>.Instance.Reset(false, string.Empty);
			}, null);
		}
	}

	// Token: 0x06004756 RID: 18262 RVA: 0x0016BC10 File Offset: 0x00169E10
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamRootNew);
	}

	// Token: 0x06004757 RID: 18263 RVA: 0x0016BC30 File Offset: 0x00169E30
	public void OnClickNormalChatBtn()
	{
		this.OnClickCloseBtn();
		ChatUIRootLogic.ResetLinkChat(GameDefine.CHAT_LINK_TYPE.TEAM, GameDefine.CHAT_CHANNEL_TYPE.NORMAL, this.mPlayerData.TeamInfo);
	}

	// Token: 0x06004758 RID: 18264 RVA: 0x0016BC4C File Offset: 0x00169E4C
	public void OnClickWorldChatBtn()
	{
		this.OnClickCloseBtn();
		ChatUIRootLogic.ResetLinkChat(GameDefine.CHAT_LINK_TYPE.TEAM, GameDefine.CHAT_CHANNEL_TYPE.WORLD, this.mPlayerData.TeamInfo);
	}

	// Token: 0x06004759 RID: 18265 RVA: 0x0016BC68 File Offset: 0x00169E68
	public void OnClickGuildChatBtn()
	{
		if (this.mPlayerData.IsHaveGuild())
		{
			this.OnClickCloseBtn();
			ChatUIRootLogic.ResetLinkChat(GameDefine.CHAT_LINK_TYPE.TEAM, GameDefine.CHAT_CHANNEL_TYPE.GUILD, this.mPlayerData.TeamInfo);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
		}
	}

	// Token: 0x0600475A RID: 18266 RVA: 0x0016BCB0 File Offset: 0x00169EB0
	public void OnClickUrgeBtn()
	{
		if (Time.time - this.lastClickUrgeTime >= 5f)
		{
			this.lastClickUrgeTime = Time.time;
			NetLogic.GetInstance().Send<Protocol.urge_team_leader>(null, null);
		}
	}

	// Token: 0x0600475B RID: 18267 RVA: 0x0016BCE0 File Offset: 0x00169EE0
	public void OnClickRecruitBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamBroadCastRoot, null, null);
	}

	// Token: 0x04003478 RID: 13432
	public List<TeamPlayerPicLogic> PlayerPicLogicList;

	// Token: 0x04003479 RID: 13433
	public GameObject ApplyBtnRoot;

	// Token: 0x0400347A RID: 13434
	public GameObject StartBtnRoot;

	// Token: 0x0400347B RID: 13435
	public GameObject ApplyTipPic;

	// Token: 0x0400347C RID: 13436
	public UILabel TeamTargetLabel;

	// Token: 0x0400347D RID: 13437
	public UILabel TeamLimitLevelLabel;

	// Token: 0x0400347E RID: 13438
	public TweenAlpha AutoMatchAnima;

	// Token: 0x0400347F RID: 13439
	public UILabel AutoMatchLabel;

	// Token: 0x04003480 RID: 13440
	public UISprite ShoutRootPic;

	// Token: 0x04003481 RID: 13441
	public UISprite LeaveBtnPic;

	// Token: 0x04003482 RID: 13442
	private PlayerData mPlayerData;

	// Token: 0x04003483 RID: 13443
	public GameObject UrgeBtnRoot;

	// Token: 0x04003484 RID: 13444
	private int mPreCount;

	// Token: 0x04003485 RID: 13445
	private bool mIsMatching;

	// Token: 0x04003486 RID: 13446
	private float lastClickUrgeTime;
}
