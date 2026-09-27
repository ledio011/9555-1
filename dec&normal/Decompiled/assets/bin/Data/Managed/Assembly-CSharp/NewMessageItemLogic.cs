using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A34 RID: 2612
public class NewMessageItemLogic : MonoBehaviour
{
	// Token: 0x06004C28 RID: 19496 RVA: 0x0019CA18 File Offset: 0x0019AC18
	public void ResetInfo(GameDefine.ACTIVITY_TYPE type)
	{
		this.CurType = NewMessageItemLogic.NewMessageType.Activity;
		this.ActType = type;
		this.CurTeamInfo = null;
		NGUITools.SetActive(this.IconSp.gameObject, true);
		NGUITools.SetActive(this.teamIconObj, false);
		this.BtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
		switch (this.ActType)
		{
		case GameDefine.ACTIVITY_TYPE.CITY_DANCE:
			this.IconSp.spriteName = "CZ_fuBenTuBiao_DANCE";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{101515}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.BAR_FIGHT:
			this.IconSp.spriteName = "CZ_suiJiZuDui_PVE";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{101516}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.WILD_BOSS:
			this.IconSp.spriteName = "CZ_shiJieBOSS_PVE";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{101519}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BOSS:
			this.IconSp.spriteName = "CZ_gongHuiBOSS_PVE";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{100748}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE:
			this.IconSp.spriteName = "CZ_shengCun_PVP";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{101517}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BATTLE:
			this.IconSp.spriteName = "CZ_gongHuiZhan_PVP";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{105001}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_DANCE:
			this.IconSp.spriteName = "CZ_fuBenTuBiao_DANCE";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{105100}", new object[0]);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_DONMINE:
			this.IconSp.spriteName = "CZ_gongHuiZhanLin_PVP";
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{106033}", new object[0]);
			break;
		}
		this.IconSp.MakePixelPerfect();
	}

	// Token: 0x06004C29 RID: 19497 RVA: 0x0019CC54 File Offset: 0x0019AE54
	public void ResetInfo(InviteTeamInfo teaminfo)
	{
		this.CurType = NewMessageItemLogic.NewMessageType.Team;
		this.ActType = GameDefine.ACTIVITY_TYPE.INVALID;
		NGUITools.SetActive(this.IconSp.gameObject, false);
		NGUITools.SetActive(this.teamIconObj, true);
		this.CurTeamInfo = teaminfo;
		if (!teaminfo.IsUrgeFlag)
		{
			this.teamLabel.text = StrDictionary.GetDictionaryString("#{100275}", new object[]
			{
				this.CurTeamInfo.inviteName
			});
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{100809}", new object[0]);
		}
		else
		{
			this.teamLabel.text = StrDictionary.GetDictionaryString("#{100295}", new object[]
			{
				this.CurTeamInfo.inviteName
			});
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
		}
	}

	// Token: 0x06004C2A RID: 19498 RVA: 0x0019CD2C File Offset: 0x0019AF2C
	public void ResetInfo(string missionid)
	{
		this.curMissionId = missionid;
		this.CurType = NewMessageItemLogic.NewMessageType.Mission;
		this.ActType = GameDefine.ACTIVITY_TYPE.INVALID;
		NGUITools.SetActive(this.IconSp.gameObject, true);
		NGUITools.SetActive(this.teamIconObj, false);
		this.IconSp.spriteName = "CZ_lianXianRenWu";
		this.IconSp.MakePixelPerfect();
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionid);
		TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
		if (timeLimitMissionDataByID != null)
		{
			this.NameLabel.text = StrDictionary.GetDictionaryString("#{100003}", new object[]
			{
				timeLimitMissionDataByID.MName
			});
		}
		else
		{
			this.NameLabel.text = string.Empty;
		}
		this.BtnLabel.text = StrDictionary.GetDictionaryString("#{100242}", new object[0]);
	}

	// Token: 0x06004C2B RID: 19499 RVA: 0x0019CDF4 File Offset: 0x0019AFF4
	public void OnClickStart()
	{
		if (this.CurType == NewMessageItemLogic.NewMessageType.Activity)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMessageUIRoot);
			this.ActJump();
		}
		else if (this.CurType == NewMessageItemLogic.NewMessageType.Team)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMessageUIRoot);
			if (this.CurTeamInfo.IsUrgeFlag)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
				}
			}
			else if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
			{
				this.CheckTeamGoal();
			}
		}
		else if (this.CurType == NewMessageItemLogic.NewMessageType.Mission)
		{
			SingletonUnity<NewMessageUIRootLogic>.Instance.DecMissionLine(this.curMissionId);
		}
	}

	// Token: 0x06004C2C RID: 19500 RVA: 0x0019CEB8 File Offset: 0x0019B0B8
	public void CheckTeamGoal()
	{
		if (this.CurTeamInfo == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.CurTeamInfo.TeamGoalId))
		{
			this.teamAgreeInvite();
		}
		else
		{
			TeamData teamDataDataByID = DataManager.GetTeamDataDataByID(this.CurTeamInfo.TeamGoalId);
			if (teamDataDataByID == null || teamDataDataByID.GoalType == 0)
			{
				this.teamAgreeInvite();
			}
			else
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(teamDataDataByID.CopyId);
				if (copySceneDataById.SubType == 12)
				{
					if (this.CheckLevel(copySceneDataById.MinLevel, copySceneDataById.MaxLevel))
					{
						this.teamAgreeInvite();
					}
					else if (this.CheckMinLevel(copySceneDataById.MinLevel))
					{
						string dictionaryString = StrDictionary.GetDictionaryString("#{102093}", new object[]
						{
							copySceneDataById.MinLevel,
							copySceneDataById.MaxLevel
						});
						MessageBoxLogic.OpenOKCancelBox(dictionaryString, "#{100127}", new MessageBoxLogic.OnYesClick(this.teamAgreeInvite), null, null, null);
					}
					else if (this.CheckMaxLevel(copySceneDataById.MaxLevel))
					{
						string dictionaryString2 = StrDictionary.GetDictionaryString("#{102094}", new object[]
						{
							copySceneDataById.MinLevel,
							copySceneDataById.MaxLevel
						});
						MessageBoxLogic.OpenOKCancelBox(dictionaryString2, "#{100127}", new MessageBoxLogic.OnYesClick(this.teamAgreeInvite), null, null, null);
					}
				}
				else if (copySceneDataById.SubType == 16)
				{
					if (this.CheckLevel(copySceneDataById.MinLevel, copySceneDataById.MaxLevel))
					{
						this.teamAgreeInvite();
					}
					else if (this.CheckMinLevel(copySceneDataById.MinLevel))
					{
						string dictionaryString3 = StrDictionary.GetDictionaryString("#{102096}", new object[]
						{
							copySceneDataById.MinLevel,
							copySceneDataById.MaxLevel
						});
						MessageBoxLogic.OpenOKCancelBox(dictionaryString3, "#{100127}", new MessageBoxLogic.OnYesClick(this.teamAgreeInvite), null, null, null);
					}
					else if (this.CheckMaxLevel(copySceneDataById.MaxLevel))
					{
						string dictionaryString4 = StrDictionary.GetDictionaryString("#{102097}", new object[]
						{
							copySceneDataById.MinLevel,
							copySceneDataById.MaxLevel
						});
						MessageBoxLogic.OpenOKCancelBox(dictionaryString4, "#{100127}", new MessageBoxLogic.OnYesClick(this.teamAgreeInvite), null, null, null);
					}
				}
			}
		}
	}

	// Token: 0x06004C2D RID: 19501 RVA: 0x0019D0FC File Offset: 0x0019B2FC
	public bool CheckLevel(int minLevel, int maxLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxLevel);
	}

	// Token: 0x06004C2E RID: 19502 RVA: 0x0019D110 File Offset: 0x0019B310
	public bool CheckMinLevel(int minLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level < minLevel;
	}

	// Token: 0x06004C2F RID: 19503 RVA: 0x0019D124 File Offset: 0x0019B324
	public bool CheckMaxLevel(int maxLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level > maxLevel;
	}

	// Token: 0x06004C30 RID: 19504 RVA: 0x0019D138 File Offset: 0x0019B338
	public void teamAgreeInvite()
	{
		if (this.CurTeamInfo != null)
		{
			ret_invite_join_team.request request = new ret_invite_join_team.request();
			request.ok = 1L;
			request.id = this.CurTeamInfo.characterId;
			NetLogic.GetInstance().Send<Protocol.ret_invite_join_team>(request, null);
			if (this.CurTeamInfo.teamId != -1L)
			{
				req_join_team.request request2 = new req_join_team.request();
				request2.teamid = this.CurTeamInfo.teamId;
				request2.isapply = true;
				NetLogic.GetInstance().Send<Protocol.req_join_team>(request2, null);
			}
		}
	}

	// Token: 0x06004C31 RID: 19505 RVA: 0x0019D1B8 File Offset: 0x0019B3B8
	public void ActJump()
	{
		switch (this.ActType)
		{
		case GameDefine.ACTIVITY_TYPE.CITY_DANCE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.CITY_DANCE, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.BAR_FIGHT:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.BAR_FIGHT, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.WILD_BOSS:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.WILD_BOSS, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BOSS:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BATTLE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_DANCE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_DANCE, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_DONMINE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE, null, false);
			}, null);
			break;
		}
	}

	// Token: 0x040039E2 RID: 14818
	public UISprite IconSp;

	// Token: 0x040039E3 RID: 14819
	public UILabel NameLabel;

	// Token: 0x040039E4 RID: 14820
	public GameObject teamIconObj;

	// Token: 0x040039E5 RID: 14821
	public UILabel teamLabel;

	// Token: 0x040039E6 RID: 14822
	private NewMessageItemLogic.NewMessageType CurType;

	// Token: 0x040039E7 RID: 14823
	private GameDefine.ACTIVITY_TYPE ActType = GameDefine.ACTIVITY_TYPE.INVALID;

	// Token: 0x040039E8 RID: 14824
	private InviteTeamInfo CurTeamInfo;

	// Token: 0x040039E9 RID: 14825
	public UILabel BtnLabel;

	// Token: 0x040039EA RID: 14826
	private string curMissionId;

	// Token: 0x02000A35 RID: 2613
	public enum NewMessageType
	{
		// Token: 0x040039F4 RID: 14836
		Activity,
		// Token: 0x040039F5 RID: 14837
		Team,
		// Token: 0x040039F6 RID: 14838
		Mission
	}
}
