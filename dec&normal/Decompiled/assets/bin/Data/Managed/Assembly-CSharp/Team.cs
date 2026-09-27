using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000868 RID: 2152
public class Team
{
	// Token: 0x060037FA RID: 14330 RVA: 0x000E7CF0 File Offset: 0x000E5EF0
	public Team()
	{
		this.mTeamID = -1L;
		this.mTeamMembers = new TeamMember[3];
		for (int i = 0; i < 3; i++)
		{
			this.mTeamMembers[i] = new TeamMember();
		}
		this.mTeamLeader = new TeamMember();
		this.Init();
	}

	// Token: 0x060037FB RID: 14331 RVA: 0x000E7D70 File Offset: 0x000E5F70
	public void Init()
	{
		this.mTeamID = -1L;
		for (int i = 0; i < 3; i++)
		{
			this.mTeamMembers[i].Init();
		}
		this.mTeamLeader.Init();
		this.mCount = 0;
	}

	// Token: 0x17000F43 RID: 3907
	// (get) Token: 0x060037FC RID: 14332 RVA: 0x000E7DB8 File Offset: 0x000E5FB8
	public long TeamID
	{
		get
		{
			return this.mTeamID;
		}
	}

	// Token: 0x17000F44 RID: 3908
	// (get) Token: 0x060037FD RID: 14333 RVA: 0x000E7DC0 File Offset: 0x000E5FC0
	// (set) Token: 0x060037FE RID: 14334 RVA: 0x000E7DC8 File Offset: 0x000E5FC8
	public int Count
	{
		get
		{
			return this.mCount;
		}
		set
		{
			this.mCount = value;
		}
	}

	// Token: 0x17000F45 RID: 3909
	// (get) Token: 0x060037FF RID: 14335 RVA: 0x000E7DD4 File Offset: 0x000E5FD4
	// (set) Token: 0x06003800 RID: 14336 RVA: 0x000E7DDC File Offset: 0x000E5FDC
	public bool IsVertify
	{
		get
		{
			return this.isVertify;
		}
		set
		{
			this.isVertify = value;
		}
	}

	// Token: 0x17000F46 RID: 3910
	// (get) Token: 0x06003801 RID: 14337 RVA: 0x000E7DE8 File Offset: 0x000E5FE8
	// (set) Token: 0x06003802 RID: 14338 RVA: 0x000E7DF0 File Offset: 0x000E5FF0
	public TeamMember[] TeamMembers
	{
		get
		{
			return this.mTeamMembers;
		}
		set
		{
			this.mTeamMembers = value;
		}
	}

	// Token: 0x17000F47 RID: 3911
	// (get) Token: 0x06003803 RID: 14339 RVA: 0x000E7DFC File Offset: 0x000E5FFC
	// (set) Token: 0x06003804 RID: 14340 RVA: 0x000E7E04 File Offset: 0x000E6004
	public TeamMember TeamLeader
	{
		get
		{
			return this.mTeamLeader;
		}
		set
		{
			this.mTeamLeader = value;
		}
	}

	// Token: 0x17000F48 RID: 3912
	// (get) Token: 0x06003805 RID: 14341 RVA: 0x000E7E10 File Offset: 0x000E6010
	// (set) Token: 0x06003806 RID: 14342 RVA: 0x000E7E18 File Offset: 0x000E6018
	public TeamData TeamGoalData
	{
		get
		{
			return this.mTeamGoalData;
		}
		set
		{
			this.mTeamGoalData = value;
		}
	}

	// Token: 0x17000F49 RID: 3913
	// (get) Token: 0x06003807 RID: 14343 RVA: 0x000E7E24 File Offset: 0x000E6024
	// (set) Token: 0x06003808 RID: 14344 RVA: 0x000E7E2C File Offset: 0x000E602C
	public int MinLimitLevel
	{
		get
		{
			return this.mMinLimitLevel;
		}
		set
		{
			this.mMinLimitLevel = value;
		}
	}

	// Token: 0x17000F4A RID: 3914
	// (get) Token: 0x06003809 RID: 14345 RVA: 0x000E7E38 File Offset: 0x000E6038
	// (set) Token: 0x0600380A RID: 14346 RVA: 0x000E7E40 File Offset: 0x000E6040
	public int MaxLimitLevel
	{
		get
		{
			return this.mMaxLimitLevel;
		}
		set
		{
			this.mMaxLimitLevel = value;
		}
	}

	// Token: 0x17000F4B RID: 3915
	// (get) Token: 0x0600380B RID: 14347 RVA: 0x000E7E4C File Offset: 0x000E604C
	// (set) Token: 0x0600380C RID: 14348 RVA: 0x000E7E54 File Offset: 0x000E6054
	public bool IsCheckingEnterCopy
	{
		get
		{
			return this.mIsCheckingEnterCopy;
		}
		set
		{
			this.mIsCheckingEnterCopy = value;
		}
	}

	// Token: 0x17000F4C RID: 3916
	// (get) Token: 0x0600380D RID: 14349 RVA: 0x000E7E60 File Offset: 0x000E6060
	private PlayerData mPlayerData
	{
		get
		{
			if (this.mCachePlayerData == null)
			{
				this.mCachePlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			}
			return this.mCachePlayerData;
		}
	}

	// Token: 0x0600380E RID: 14350 RVA: 0x000E7E84 File Offset: 0x000E6084
	public void ClearMemberEnterCopyState()
	{
		this.mTeamLeader.ClearEnterCopyState();
		for (int i = 0; i < this.mTeamMembers.Length; i++)
		{
			this.mTeamMembers[i].ClearEnterCopyState();
		}
	}

	// Token: 0x0600380F RID: 14351 RVA: 0x000E7EC4 File Offset: 0x000E60C4
	public bool IsFull()
	{
		return this.mTeamMembers[2].IsValid();
	}

	// Token: 0x06003810 RID: 14352 RVA: 0x000E7ED4 File Offset: 0x000E60D4
	public int GetEmptyNum()
	{
		return 4 - this.mCount;
	}

	// Token: 0x06003811 RID: 14353 RVA: 0x000E7EE0 File Offset: 0x000E60E0
	public TeamMember GetTeamber(int index)
	{
		if (index < 0 || index > 4)
		{
			return null;
		}
		if (index == 0)
		{
			return this.mTeamLeader;
		}
		return this.mTeamMembers[index - 1];
	}

	// Token: 0x06003812 RID: 14354 RVA: 0x000E7F0C File Offset: 0x000E610C
	public int GetTeamberCount()
	{
		return this.mCount;
	}

	// Token: 0x06003813 RID: 14355 RVA: 0x000E7F14 File Offset: 0x000E6114
	public void UpdateTeamInfo(update_team.request request)
	{
		bool flag = false;
		bool flag2 = false;
		if (request.HasTeam)
		{
			if (!this.mPlayerData.IsHaveTeam())
			{
				flag = true;
				NewMessageUIRootLogic.ClearAllteamMessage();
			}
		}
		else if (this.mPlayerData.IsHaveTeam())
		{
			flag2 = true;
			NewMessageUIRootLogic.ClearAllteamMessage();
		}
		long num = this.mTeamID;
		this.mPreCount = this.mCount;
		this.Init();
		if (request.HasTeam)
		{
			if (this.mTeamID != request.team.id)
			{
				this.mTeamID = request.team.id;
				if (UIUpdateEvent.OnChangeTeam != null)
				{
					UIUpdateEvent.OnChangeTeam();
				}
			}
			this.mCount = (int)request.team.count;
			this.isVertify = (request.team.isVerfiy == 0L);
			this.mTeamGoalData = DataManager.GetTeamDataDataByID(request.team.goalId);
			this.mMinLimitLevel = (int)request.team.minLevel;
			this.mMaxLimitLevel = (int)request.team.maxLevel;
			if (request.team.HasTeammembers)
			{
				this.mTempMemberList = new List<teammember>(request.team.teammembers.Values);
				for (int i = 0; i < this.mTempMemberList.Count; i++)
				{
					this.SetTeamMenmberInfo(this.mTempMemberList[i], this.mTeamMembers[i]);
					this.mTeamMembers[i].TeamJob = 1;
				}
			}
			this.SetTeamMenmberInfo(request.team.teamleader, this.mTeamLeader);
			this.mTeamLeader.TeamJob = 0;
		}
		else
		{
			this.mTeamID = -1L;
			if (num != -1L && UIUpdateEvent.OnChangeTeam != null)
			{
				UIUpdateEvent.OnChangeTeam();
			}
		}
		bool flag3 = false;
		bool flag4 = false;
		if (this.mPreCount < this.GetTeamberCount())
		{
			flag4 = this.CheckTeamIsFull();
			flag3 = flag4;
		}
		if (SingletonUnity<TeamPreparationRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamPreparationRootLogic>.Instance.gameObject) && this.mPreCount > this.GetTeamberCount())
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamPreparationRoot);
		}
		if (SingletonUnity<MissionTeamTipLogic>.Exists)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.TeamTipRoot.Reset();
			SingletonUnity<MissionTeamTipLogic>.Instance.CheckTeamTips();
		}
		if (SingletonUnity<CreateTeamRootLogic>.Exists)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonUnity<CreateTeamRootLogic>.Instance.OnClickCloseBtn();
			if (!UIManager.IsUnlockTutorialEnable())
			{
				flag3 = true;
				if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_WAIT)
				{
					TutorialManager.MoveNext(false);
				}
			}
		}
		if (flag || flag2)
		{
			this.mPlayerData.TeamInfo.ClearTeamCacheData();
		}
		if (flag)
		{
			NoticeLogic.AddNotifyData("#{100832}", true, false);
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
				if ((!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject)) && !UIManager.IsUnlockTutorialEnable())
				{
					flag3 = true;
					if (this.mPlayerData.IsTeamLeader())
					{
						if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_WAIT)
						{
							TutorialManager.MoveNext(false);
						}
					}
					else if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_WAIT)
					{
						TutorialManager.CloseTutorial();
					}
				}
			}
			if (SingletonUnity<SearchTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SearchTeamRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SearchTeamRootLogic>.Instance.OnClickCloseBtn();
				if (!UIManager.IsUnlockTutorialEnable())
				{
					flag3 = true;
					if (this.mPlayerData.IsTeamLeader())
					{
						if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_WAIT)
						{
							TutorialManager.MoveNext(false);
						}
					}
					else if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_WAIT)
					{
						TutorialManager.CloseTutorial();
					}
				}
			}
		}
		if (flag3 && !UIManager.IsUnlockTutorialEnable())
		{
			if (!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
			{
				if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
				}
			}
		}
		if (flag2)
		{
			NoticeLogic.AddNotifyData("#{100833}", true, false);
			if (SingletonUnity<TeamUIRootNewLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.OnClickCloseBtn();
			}
			this.mPlayerData.TeamInfo.IsVertify = false;
		}
		if (SingletonUnity<TeamUIRootNewLogic>.Exists)
		{
			SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
		}
		if (flag4 && this.mPlayerData.IsTeamLeader())
		{
			this.EnterMultiCopyAction();
		}
		if (SingletonUnity<MultiRankSmallRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MultiRankSmallRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MultiRankSmallRootLogic>.Instance.ResetTeam();
		}
		if (SingletonUnity<ExpBattleInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ExpBattleInfoRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ExpBattleInfoRootLogic>.Instance.ResetTeam();
		}
		if (SingletonUnity<GuildBattleInfoRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleInfoRoot>.Instance.gameObject))
		{
			SingletonUnity<GuildBattleInfoRoot>.Instance.ResetTeam();
		}
		if (SingletonUnity<TeamPreparationRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamPreparationRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TeamPreparationRootLogic>.Instance.Reset();
		}
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.UpdateMatchingLabel();
		}
		if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMatchBtn();
		}
		if (SingletonUnity<SearchTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SearchTeamRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SearchTeamRootLogic>.Instance.UpdateMatchingLabel();
		}
		if (request.HasTeam && request.team.HasRecruit)
		{
			this.TeamBroadCastShow((int)request.team.recruit);
		}
	}

	// Token: 0x06003814 RID: 14356 RVA: 0x000E84D8 File Offset: 0x000E66D8
	public void UpdateMatchState(sync_random_team_state.request req)
	{
		if (req.HasState)
		{
			this.isVertify = (req.state == 0L);
		}
		if (req.HasId)
		{
			this.mTeamGoalData = DataManager.GetTeamDataDataByID(req.id);
		}
		if (SingletonUnity<TeamUIRootNewLogic>.Exists)
		{
			SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
		}
		if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewDailyCopyUIRootLogic>.Instance.UpdateMatchingLabel();
		}
		if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMatchBtn();
		}
		if (SingletonUnity<SearchTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SearchTeamRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SearchTeamRootLogic>.Instance.UpdateMatchingLabel();
		}
	}

	// Token: 0x06003815 RID: 14357 RVA: 0x000E85A8 File Offset: 0x000E67A8
	public void EnterMultiCopyAction()
	{
		if (this.mPlayerData.TeamInfo.TeamLeader.CopyRestNum <= 0)
		{
			NoticeLogic.AddNotifyData("#{101020}", true, false);
			return;
		}
		CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.mPlayerData.TeamInfo.TeamGoalData.CopyId);
		if (this.mPlayerData.TeamInfo.GetTeamberCount() < copySceneDataById.MinMember)
		{
			NoticeLogic.AddNotifyData("#{102004}", true, false);
			return;
		}
		for (int i = 0; i < this.mPlayerData.TeamInfo.TeamMembers.Length; i++)
		{
			if (this.mPlayerData.TeamInfo.TeamMembers[i].IsValid() && this.mPlayerData.TeamInfo.TeamMembers[i].Level < copySceneDataById.MinLevel)
			{
				NoticeLogic.AddNotifyData2Client(false, "#{102005}", false, new object[]
				{
					this.mPlayerData.TeamInfo.TeamMembers[i].Name
				});
				return;
			}
		}
		enter_multi_copy_scene_confirm.request request = new enter_multi_copy_scene_confirm.request();
		request.id = this.mPlayerData.TeamInfo.TeamGoalData.ID;
		request.type1 = (long)this.mPlayerData.TeamInfo.TeamGoalData.GoalType;
		NetLogic.GetInstance().Send<Protocol.enter_multi_copy_scene_confirm>(request, null);
		this.mPlayerData.TeamInfo.ClearMemberEnterCopyState();
		this.mPlayerData.TeamInfo.IsCheckingEnterCopy = true;
		this.mPlayerData.TeamInfo.TeamLeader.IsReadyEnterCopy = false;
		if (SingletonUnity<TeamUIRootNewLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
		{
			SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
		}
		NewMessageUIRootLogic.ClearAllteamMessage();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", copySceneDataById.ID), string.Format("start_multi_{0}", this.mPlayerData.TeamInfo.GetTeamberCount()));
		if (this.mPlayerData.TeamInfo.GetTeamberCount() <= 1)
		{
			this.mPlayerData.TeamInfo.IsCheckingEnterCopy = false;
			this.mPlayerData.TeamInfo.TeamLeader.IsReadyEnterCopy = false;
		}
	}

	// Token: 0x06003816 RID: 14358 RVA: 0x000E87DC File Offset: 0x000E69DC
	public bool CheckTeamIsFull()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.IsShowTeamScene() || sceneManager.IsCarScene())
		{
			return false;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam() && playerData.IsTeamLeader())
		{
			CopySceneData copySceneData = null;
			if (playerData.TeamInfo.TeamGoalData.GoalType == 1)
			{
				copySceneData = DataManager.GetCopySceneDataById(playerData.TeamInfo.TeamGoalData.CopyId);
			}
			if (!UIManager.IsUnlockTutorialEnable() && copySceneData != null && playerData.TeamInfo.GetTeamberCount() >= copySceneData.MaxMember)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003817 RID: 14359 RVA: 0x000E888C File Offset: 0x000E6A8C
	public TeamMember GetTeamMemberByServerId(long serverId)
	{
		if (this.mTeamLeader.ServerId == serverId)
		{
			return this.mTeamLeader;
		}
		for (int i = 0; i < this.mTeamMembers.Length; i++)
		{
			if (this.mTeamMembers[i].ServerId == serverId)
			{
				return this.mTeamMembers[i];
			}
		}
		return null;
	}

	// Token: 0x06003818 RID: 14360 RVA: 0x000E88E8 File Offset: 0x000E6AE8
	private void SetTeamMenmberInfo(teammember source, TeamMember target)
	{
		if (source.HasId)
		{
			target.ServerId = source.id;
		}
		if (source.HasTeamid)
		{
			target.TeamId = source.teamid;
		}
		if (source.HasName)
		{
			target.Name = source.name;
		}
		if (source.HasLevel)
		{
			target.Level = (int)source.level;
		}
		if (source.HasProfession)
		{
			target.Profession = (PROFESSION_TYPE)source.profession;
		}
		if (source.HasCombValue)
		{
			target.CombValue = (int)source.combValue;
		}
		if (source.HasMapInfoId)
		{
			target.MapInfoId = source.mapInfoId.ToString();
		}
		if (source.HasLineIndex)
		{
			target.ServerLineIndex = (int)source.lineIndex;
		}
		if (source.HasMemberType)
		{
			target.MemberType = (int)source.memberType;
		}
		if (source.HasHp)
		{
			target.HP = (int)source.hp;
		}
		if (source.HasMax_hp)
		{
			target.MaxHP = (int)source.max_hp;
		}
		if (source.HasVisual)
		{
			target.Visual = source.visual;
		}
		if (source.HasGuildId)
		{
			target.GuildName = source.guildName;
			target.GuildId = source.guildId;
		}
		else
		{
			target.GuildName = string.Empty;
			target.GuildId = 0L;
		}
		if (source.HasCurNum)
		{
			target.CopyRestNum = (int)source.curNum;
		}
		else
		{
			target.CopyRestNum = -1;
		}
		if (!this.IsCheckingEnterCopy)
		{
			target.ClearEnterCopyState();
		}
	}

	// Token: 0x06003819 RID: 14361 RVA: 0x000E8A88 File Offset: 0x000E6C88
	public void UpdateTeamMemberInfo(update_team_member.request request)
	{
		if (request.HasMember)
		{
			TeamMember teamMemberByServerId = this.GetTeamMemberByServerId(request.member.id);
			this.SetTeamMenmberInfo(request.member, teamMemberByServerId);
			if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.TeamTipRoot.gameObject))
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.TeamTipRoot.UpdateMemberInfo(teamMemberByServerId);
			}
			if (SingletonUnity<TeamUIRootNewLogic>.Exists)
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
			}
			if (SingletonUnity<MultiRankSmallRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MultiRankSmallRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MultiRankSmallRootLogic>.Instance.UpdateMemberInfo(teamMemberByServerId);
			}
			if (SingletonUnity<ExpBattleInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ExpBattleInfoRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ExpBattleInfoRootLogic>.Instance.UpdateMemberInfo(teamMemberByServerId);
			}
			if (SingletonUnity<GuildBattleInfoRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleInfoRoot>.Instance.gameObject))
			{
				SingletonUnity<GuildBattleInfoRoot>.Instance.UpdateMemberInfo(teamMemberByServerId);
			}
		}
	}

	// Token: 0x17000F4D RID: 3917
	// (get) Token: 0x0600381A RID: 14362 RVA: 0x000E8B84 File Offset: 0x000E6D84
	public TeamMember SelfMember
	{
		get
		{
			return this.GetTeamMemberByServerId(PlayerData.MainPlayerServerId);
		}
	}

	// Token: 0x0600381B RID: 14363 RVA: 0x000E8B94 File Offset: 0x000E6D94
	public void UpdateSelfTeamInfo()
	{
	}

	// Token: 0x17000F4E RID: 3918
	// (get) Token: 0x0600381C RID: 14364 RVA: 0x000E8B98 File Offset: 0x000E6D98
	public Dictionary<long, teammember> ApplyMemberDic
	{
		get
		{
			return this.mApplyMemberDic;
		}
	}

	// Token: 0x0600381D RID: 14365 RVA: 0x000E8BA0 File Offset: 0x000E6DA0
	public void AddApplyMember(teammember member)
	{
		if (!this.mApplyMemberDic.ContainsKey(member.id))
		{
			this.mApplyMemberDic.Add(member.id, member);
			if (SingletonUnity<TeamApplyListLogic>.Exists)
			{
				SingletonUnity<TeamApplyListLogic>.Instance.UpdataApplyList(new List<teammember>(this.mApplyMemberDic.Values));
			}
			if (SingletonUnity<TeamUIRootNewLogic>.Exists)
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.OnApplyTeam();
			}
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.SetTeamApplyTips(true);
			}
		}
	}

	// Token: 0x0600381E RID: 14366 RVA: 0x000E8C24 File Offset: 0x000E6E24
	public void RemoveApplyMember(long id)
	{
		if (this.mApplyMemberDic.ContainsKey(id))
		{
			this.mApplyMemberDic.Remove(id);
			if (SingletonUnity<TeamApplyListLogic>.Exists)
			{
				SingletonUnity<TeamApplyListLogic>.Instance.UpdataApplyList(new List<teammember>(this.mApplyMemberDic.Values));
			}
			if (SingletonUnity<UIManager>.Exists && this.mApplyMemberDic.Count == 0)
			{
				if (SingletonUnity<TeamUIRootNewLogic>.Exists)
				{
					SingletonUnity<TeamUIRootNewLogic>.Instance.OnClearApplyTeam();
				}
				if (SingletonUnity<MissionTeamTipLogic>.Exists)
				{
					SingletonUnity<MissionTeamTipLogic>.Instance.SetTeamApplyTips(false);
				}
			}
		}
	}

	// Token: 0x0600381F RID: 14367 RVA: 0x000E8CB8 File Offset: 0x000E6EB8
	public void ClearApplyMember()
	{
		this.mApplyMemberDic.Clear();
	}

	// Token: 0x06003820 RID: 14368 RVA: 0x000E8CC8 File Offset: 0x000E6EC8
	public bool IsHaveApplyMember()
	{
		return this.mApplyMemberDic.Count != 0;
	}

	// Token: 0x17000F4F RID: 3919
	// (get) Token: 0x06003821 RID: 14369 RVA: 0x000E8CDC File Offset: 0x000E6EDC
	public Dictionary<long, team> AppliedTeamDic
	{
		get
		{
			return this.mAppliedTeamDic;
		}
	}

	// Token: 0x06003822 RID: 14370 RVA: 0x000E8CE4 File Offset: 0x000E6EE4
	public bool HasAppliedTeam(long teamId)
	{
		return this.mAppliedTeamDic.ContainsKey(teamId);
	}

	// Token: 0x06003823 RID: 14371 RVA: 0x000E8CF4 File Offset: 0x000E6EF4
	public void AddApplyTeam(team team)
	{
		if (!this.mAppliedTeamDic.ContainsKey(team.id))
		{
			this.mAppliedTeamDic.Add(team.id, team);
		}
	}

	// Token: 0x06003824 RID: 14372 RVA: 0x000E8D2C File Offset: 0x000E6F2C
	public void RemoveApplyTeam(long teamId)
	{
		if (this.mAppliedTeamDic.ContainsKey(teamId))
		{
			this.mAppliedTeamDic.Remove(teamId);
		}
	}

	// Token: 0x06003825 RID: 14373 RVA: 0x000E8D4C File Offset: 0x000E6F4C
	public bool isTeamMemberById(long id)
	{
		if (this.mTeamLeader.ServerId == id)
		{
			return true;
		}
		for (int i = 0; i < this.mTeamMembers.Length; i++)
		{
			if (this.mTeamMembers[i].ServerId == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003826 RID: 14374 RVA: 0x000E8D9C File Offset: 0x000E6F9C
	public void ClearTeamApplyDic()
	{
		this.mAppliedTeamDic.Clear();
	}

	// Token: 0x17000F50 RID: 3920
	// (get) Token: 0x06003827 RID: 14375 RVA: 0x000E8DAC File Offset: 0x000E6FAC
	// (set) Token: 0x06003828 RID: 14376 RVA: 0x000E8DB4 File Offset: 0x000E6FB4
	public List<long> InvitedPlyaer
	{
		get
		{
			return this.mInvitedPlayer;
		}
		set
		{
			this.mInvitedPlayer = value;
		}
	}

	// Token: 0x06003829 RID: 14377 RVA: 0x000E8DC0 File Offset: 0x000E6FC0
	public bool HasInvited(long serverId)
	{
		return this.mInvitedPlayer.Contains(serverId);
	}

	// Token: 0x0600382A RID: 14378 RVA: 0x000E8DD0 File Offset: 0x000E6FD0
	public void RemoveInvitedPerson(long serverId)
	{
		if (this.mInvitedPlayer.Contains(serverId))
		{
			this.mInvitedPlayer.Remove(serverId);
		}
	}

	// Token: 0x0600382B RID: 14379 RVA: 0x000E8DF0 File Offset: 0x000E6FF0
	public void ClearTeamInviteList()
	{
		this.mInvitedPlayer.Clear();
	}

	// Token: 0x0600382C RID: 14380 RVA: 0x000E8E00 File Offset: 0x000E7000
	public void ClearTeamCacheData()
	{
		this.ClearApplyMember();
		this.ClearTeamApplyDic();
		this.ClearTeamInviteList();
		this.mIsCheckingEnterCopy = false;
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x000E8E1C File Offset: 0x000E701C
	public void Reset()
	{
		this.Init();
		this.ClearTeamCacheData();
		this.isVertify = false;
	}

	// Token: 0x0600382E RID: 14382 RVA: 0x000E8E34 File Offset: 0x000E7034
	public void TeamBroadCastShow(int RecruitNum)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.IsHaveTeam() || !playerData.IsTeamLeader() || RecruitNum <= 0)
		{
			return;
		}
		string chatInfo = string.Empty;
		Team teamInfo = playerData.TeamInfo;
		GameDefine.CHAT_LINK_TYPE mCurLinkType = GameDefine.CHAT_LINK_TYPE.TEAM;
		List<long> list = new List<long>();
		list.Add(teamInfo.TeamID);
		list.Add((long)teamInfo.MinLimitLevel);
		list.Add((long)teamInfo.MaxLimitLevel);
		string text = string.Empty;
		if (teamInfo.TeamGoalData.GoalType == 0)
		{
			text = teamInfo.TeamGoalData.MTitleName;
		}
		else
		{
			text = DataManager.GetCopySceneDataById(teamInfo.TeamGoalData.CopyId).MName;
		}
		string dictionaryString = StrDictionary.GetDictionaryString("#{100262}", new object[]
		{
			text
		});
		chatInfo = dictionaryString;
		if ((RecruitNum & 1) != 0)
		{
			this.SendChatInfo(chatInfo, GameDefine.CHAT_CHANNEL_TYPE.WORLD, mCurLinkType, list);
		}
		if ((RecruitNum & 2) != 0)
		{
			this.SendChatInfo(chatInfo, GameDefine.CHAT_CHANNEL_TYPE.NORMAL, mCurLinkType, list);
		}
		if ((RecruitNum & 4) != 0 && playerData.IsHaveGuild())
		{
			this.SendChatInfo(chatInfo, GameDefine.CHAT_CHANNEL_TYPE.GUILD, mCurLinkType, list);
		}
	}

	// Token: 0x0600382F RID: 14383 RVA: 0x000E8F48 File Offset: 0x000E7148
	public void SendChatInfo(string ChatInfo, GameDefine.CHAT_CHANNEL_TYPE mCurChannelType, GameDefine.CHAT_LINK_TYPE mCurLinkType, List<long> mLinkLongData)
	{
		string text = ChatInfo;
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		if (mCurLinkType == GameDefine.CHAT_LINK_TYPE.INVALID)
		{
			text = text.Replace("\r", " ");
			text = text.Replace("\n", " ");
		}
		if (mCurLinkType != GameDefine.CHAT_LINK_TYPE.INVALID && !text.Contains(ChatInfo))
		{
			return;
		}
		chat.request request = new chat.request();
		request.chattype = (long)mCurChannelType;
		request.linktype = (long)mCurLinkType;
		if (mCurLinkType != GameDefine.CHAT_LINK_TYPE.INVALID)
		{
			if (mLinkLongData.Count != 0)
			{
				request.intdata = mLinkLongData;
			}
			text = text.Replace(ChatInfo, string.Format("[FF0000]{0}[-]", ChatInfo));
		}
		request.chatInfo = text;
		NetLogic.GetInstance().Send<Protocol.chat>(request, null);
	}

	// Token: 0x040024F5 RID: 9461
	private long mTeamID = -1L;

	// Token: 0x040024F6 RID: 9462
	private int mPreCount;

	// Token: 0x040024F7 RID: 9463
	private int mCount;

	// Token: 0x040024F8 RID: 9464
	private bool isVertify;

	// Token: 0x040024F9 RID: 9465
	private TeamMember[] mTeamMembers;

	// Token: 0x040024FA RID: 9466
	private TeamMember mTeamLeader;

	// Token: 0x040024FB RID: 9467
	private TeamData mTeamGoalData;

	// Token: 0x040024FC RID: 9468
	private int mMinLimitLevel;

	// Token: 0x040024FD RID: 9469
	private int mMaxLimitLevel;

	// Token: 0x040024FE RID: 9470
	private bool mIsCheckingEnterCopy;

	// Token: 0x040024FF RID: 9471
	private PlayerData mCachePlayerData;

	// Token: 0x04002500 RID: 9472
	private List<teammember> mTempMemberList;

	// Token: 0x04002501 RID: 9473
	private Dictionary<long, teammember> mApplyMemberDic = new Dictionary<long, teammember>();

	// Token: 0x04002502 RID: 9474
	private Dictionary<long, team> mAppliedTeamDic = new Dictionary<long, team>();

	// Token: 0x04002503 RID: 9475
	private List<long> mInvitedPlayer = new List<long>();
}
