using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200091A RID: 2330
public class ActivityTipsRootLogic : SingletonUnity<ActivityTipsRootLogic>
{
	// Token: 0x060040B7 RID: 16567 RVA: 0x001309D4 File Offset: 0x0012EBD4
	public void ShowInfo(ActivityMapData curdata)
	{
		this.isShow = true;
		this.CurData = curdata;
		this.curType = ActivityTipsRootLogic.ShowType.activity;
		this.ShowLabelInfo();
		this.picSp.spriteName = this.CurData.Icon;
		this.PosAnima.ResetToBeginning();
		this.PosAnima.PlayForward();
		this.BtnLabel.text = StrDictionary.GetDictionaryString("#{100702}", new object[0]);
	}

	// Token: 0x060040B8 RID: 16568 RVA: 0x00130A44 File Offset: 0x0012EC44
	public void ShowShopNpcInfo(ObjNPC target)
	{
		this.isShow = true;
		this.curType = ActivityTipsRootLogic.ShowType.shopnpc;
		this.BtnSp.alpha = 1f;
		this.CurShopNpc = target;
		this.picSp.spriteName = "CZ_zhuJieMianAnNiu_ShangCheng";
		this.PosAnima.ResetToBeginning();
		this.PosAnima.PlayForward();
		this.BtnLabel.text = StrDictionary.GetDictionaryString("#{301139}", new object[0]);
		this.TitleLabel.text = string.Empty;
		this.InfoLabel.text = string.Empty;
		NpcData npcdata = target.NPCData;
		if (npcdata == null)
		{
			return;
		}
		switch (npcdata.FunctionType)
		{
		case 4:
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301127}", new object[0]);
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301128}", new object[0]);
			break;
		case 5:
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301129}", new object[0]);
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301130}", new object[0]);
			break;
		case 6:
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301131}", new object[0]);
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301132}", new object[0]);
			break;
		case 7:
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301133}", new object[0]);
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301134}", new object[0]);
			break;
		case 8:
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301135}", new object[0]);
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301136}", new object[0]);
			break;
		case 9:
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301137}", new object[0]);
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301138}", new object[0]);
			break;
		}
	}

	// Token: 0x060040B9 RID: 16569 RVA: 0x00130C74 File Offset: 0x0012EE74
	public void UpdateInfo()
	{
		if (this.CurData != null && this.curType == ActivityTipsRootLogic.ShowType.activity)
		{
			this.ShowLabelInfo();
		}
	}

	// Token: 0x060040BA RID: 16570 RVA: 0x00130C94 File Offset: 0x0012EE94
	private void ShowLabelInfo()
	{
		this.BtnSp.alpha = 1f;
		this.TitleLabel.text = string.Empty;
		this.InfoLabel.text = string.Empty;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			string activityID = this.CurData.ActivityID;
			MissionData missionDataByID = DataManager.GetMissionDataByID(activityID);
			if (missionDataByID == null)
			{
				return;
			}
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			MISSION_STATE missionState = missionManager.GetMissionState(activityID);
			string text = string.Empty;
			switch (missionDataByID.Class)
			{
			case 0:
				text = StrDictionary.GetDictionaryString("#{100168}", new object[]
				{
					missionManager.GetMissionParam(activityID, 3) + 1L
				});
				break;
			case 1:
				text = StrDictionary.GetDictionaryString("#{100169}", new object[0]);
				break;
			case 2:
				text = StrDictionary.GetDictionaryString("#{100170}", new object[0]);
				break;
			case 3:
			case 6:
				text = StrDictionary.GetDictionaryString("#{100171}", new object[0]);
				break;
			case 4:
				text = StrDictionary.GetDictionaryString("#{100172}", new object[0]);
				break;
			case 5:
				text = StrDictionary.GetDictionaryString("#{100173}", new object[0]);
				break;
			case 7:
				text = StrDictionary.GetDictionaryString("#{100200}", new object[0]);
				break;
			case 8:
				text = StrDictionary.GetDictionaryString("#{100001}", new object[0]);
				break;
			default:
				text = "[Need Loc]";
				break;
			}
			if (missionDataByID.Class == 0)
			{
				this.TitleLabel.text = string.Concat(new string[]
				{
					StrDictionary.GetDictionaryString("#{100171}", new object[0]),
					"[FDAE33]",
					StrDictionary.GetDictionaryString(missionDataByID.TipDescribeID, new object[0]),
					text,
					"[-]"
				});
				MissionManager.GetMissionStateLabel(missionDataByID, missionState, this.InfoLabel);
			}
			else if (missionDataByID.Class == 8)
			{
				if (missionState == MISSION_STATE.INVALID)
				{
					TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
					this.TitleLabel.text = text + "[FDAE33]" + timeLimitMissionDataByID.MName + "[-]";
					this.InfoLabel.text = timeLimitMissionDataByID.MTip;
				}
				else
				{
					this.TitleLabel.text = text + "[FDAE33]" + StrDictionary.GetDictionaryString(missionDataByID.TipDescribeID, new object[0]) + "[-]";
					MissionManager.GetMissionStateLabel(missionDataByID, missionState, this.InfoLabel);
				}
			}
			else
			{
				this.TitleLabel.text = text + "[FDAE33]" + StrDictionary.GetDictionaryString(missionDataByID.TipDescribeID, new object[0]) + "[-]";
				MissionManager.GetMissionStateLabel(missionDataByID, missionState, this.InfoLabel);
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.DAILY_COPY)
		{
			CopySceneData copySceneData = DataManager.GetCopySceneDataById(this.CurData.ActivityID);
			copyscene_info dailyCopyInfo = this.CurData.GetDailyCopyInfo();
			this.TitleLabel.text = copySceneData.MName;
			if (dailyCopyInfo != null)
			{
				if (this.CurData.IsUnlock)
				{
					int num = (int)dailyCopyInfo.CurNum;
					if (copySceneData.IsScuffleCopy || copySceneData.IsSingleDance)
					{
						this.InfoLabel.text = string.Format("{0}  {1}", StrDictionary.GetDictionaryString("#{102018}", new object[0]), TimeTools.GetMinuteSecondStr(num));
					}
					else if (copySceneData.IsPVPMap)
					{
						this.InfoLabel.text = StrDictionary.GetDictionaryString("#{102081}", new object[0]);
					}
					else if (copySceneData.IsEquipCopy)
					{
						copySceneData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.GetCurPlayerEquipCopyData();
						this.TitleLabel.text = copySceneData.MName;
						this.InfoLabel.text = string.Format("{0}  {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, copySceneData.MaxPlayNum);
					}
					else if (copySceneData.IsExpCopy)
					{
						copySceneData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.GetCurPlayerExpCopyData();
						this.TitleLabel.text = copySceneData.MName;
						this.InfoLabel.text = string.Format("{0}  {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, copySceneData.MaxPlayNum);
					}
					else
					{
						this.InfoLabel.text = string.Format("{0}  {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, copySceneData.MaxPlayNum);
					}
				}
				else
				{
					this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
					{
						copySceneData.MinLevel
					});
					this.BtnSp.alpha = 0f;
				}
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.ESCORT)
		{
			EscortData escortDataById = DataManager.GetEscortDataById(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString(escortDataById.Name, new object[0]);
			if (this.CurData.IsUnlock)
			{
				activity_info activityInfo = this.CurData.GetActivityInfo();
				if (activityInfo != null)
				{
					int num2 = (int)activityInfo.CurNum;
					this.InfoLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num2, escortDataById.MaxPlayNum);
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					escortDataById.UnlockLevel
				});
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.BAR_FIGHT)
		{
			BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString(barFightCopyDataByID.Name, new object[0]);
			if (this.CurData.IsUnlock)
			{
				activity_info activityInfo2 = this.CurData.GetActivityInfo();
				if (activityInfo2 != null)
				{
					TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)(barFightCopyDataByID.StartTime + barFightCopyDataByID.DurationTime), playerCommonData.TimeOffset);
					TimeSpan localShowTime2 = TimeTools.GetLocalShowTime((long)(barFightCopyDataByID.StartTime + barFightCopyDataByID.DurationTime + barFightCopyDataByID.WaitTime), playerCommonData.TimeOffset);
					this.InfoLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime.Hours, localShowTime.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime2.Hours, localShowTime2.Minutes));
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					barFightCopyDataByID.UnlockLevel
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.CITY_DANCE)
		{
			CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString(cityDanceDataById.Name, new object[0]);
			if (this.CurData.IsUnlock)
			{
				activity_info activityInfo3 = this.CurData.GetActivityInfo();
				if (activityInfo3 != null)
				{
					TimeSpan localShowTime3 = TimeTools.GetLocalShowTime((long)cityDanceDataById.StartTime.get_Chars(0), playerCommonData.TimeOffset);
					TimeSpan localShowTime4 = TimeTools.GetLocalShowTime((long)((int)cityDanceDataById.StartTime.get_Chars(0) + cityDanceDataById.DurationTime), playerCommonData.TimeOffset);
					this.InfoLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime3.Hours, localShowTime3.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime4.Hours, localShowTime4.Minutes));
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					cityDanceDataById.UnlockLevel
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.SEX_MINI)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString(sexMiniDataById.Name, new object[0]);
			if (this.CurData.IsUnlock)
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{102063}", new object[0]);
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					sexMiniDataById.UnlockLevel
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE)
		{
			SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString(surviveBattleDataById.Name, new object[0]);
			if (this.CurData.IsUnlock)
			{
				activity_info activityInfo4 = this.CurData.GetActivityInfo();
				if (activityInfo4 != null)
				{
					List<TimeSpan> localShowTime5 = TimeTools.GetLocalShowTime(surviveBattleDataById.StartTimes, playerCommonData.TimeOffset, (long)surviveBattleDataById.DurationTime, (int)activityInfo4.next, false);
					if (localShowTime5 != null && localShowTime5.Count == 2)
					{
						TimeSpan timeSpan = localShowTime5[0];
						TimeSpan timeSpan2 = localShowTime5[1];
						this.InfoLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", timeSpan.Hours, timeSpan.Minutes), string.Format("{0:D2}:{1:D2}", timeSpan2.Hours, timeSpan2.Minutes));
					}
					else
					{
						this.InfoLabel.text = string.Empty;
					}
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					surviveBattleDataById.UnlockLevel
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
		{
			GuildBattleData guildBattleDataById = DataManager.GetGuildBattleDataById(this.CurData.ActivityID);
			this.TitleLabel.text = guildBattleDataById.MName;
			if (this.CurData.IsUnlock)
			{
				guild_battle_info guildBattleInfo = this.CurData.GetGuildBattleInfo();
				if (guildBattleInfo != null)
				{
					guildBattleDataById = DataManager.GetGuildBattleDataById(guildBattleInfo.ID);
					string text2 = string.Empty;
					int num3 = 0;
					int num4 = 0;
					if (guildBattleInfo.state < 0L || guildBattleInfo.state >= 6L)
					{
						num3 = guildBattleDataById.Week;
						num4 = guildBattleDataById.StartTime;
						text2 = StrDictionary.GetDictionaryString("#{105076}", new object[0]);
					}
					else if (guildBattleInfo.state <= 1L)
					{
						num3 = guildBattleDataById.Week1;
						num4 = guildBattleDataById.StartTime1;
						text2 = StrDictionary.GetDictionaryString("#{105002}", new object[0]);
					}
					else if (guildBattleInfo.state <= 3L)
					{
						num3 = guildBattleDataById.Week2;
						num4 = guildBattleDataById.StartTime2;
						text2 = StrDictionary.GetDictionaryString("#{105003}", new object[0]);
					}
					else if (guildBattleInfo.state <= 5L)
					{
						num3 = guildBattleDataById.Week3;
						num4 = guildBattleDataById.StartTime3;
						text2 = StrDictionary.GetDictionaryString("#{105004}", new object[0]);
					}
					if (guildBattleInfo.state == -2L)
					{
						this.InfoLabel.text = StrDictionary.GetDictionaryString("#{101406}", new object[0]);
					}
					else
					{
						TimeSpan localShowTime6 = TimeTools.GetLocalShowTime((long)num4, playerCommonData.TimeOffset);
						int num5 = (num3 - 1 + TimeTools.GetOffsetDay((long)num4, playerCommonData.TimeOffset) + GameDefine.WEEK_NAME.Count) % GameDefine.WEEK_NAME.Count;
						this.InfoLabel.text = string.Format("{0}:{1} {2:d2}:{3:d2}", new object[]
						{
							text2,
							StrDictionary.GetDictionaryString(GameDefine.WEEK_NAME[num5], new object[0]),
							localShowTime6.Hours,
							localShowTime6.Minutes
						});
					}
					this.TitleLabel.text = guildBattleDataById.MName;
				}
			}
			else
			{
				int condition = DataManager.GetFunctionDataById(4077.ToString()).Condition;
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					condition
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
		{
			GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100748}", new object[0]);
			if (this.CurData.IsUnlock)
			{
				guild_boss guildBossInfo = this.CurData.GetGuildBossInfo();
				if (guildBossInfo != null)
				{
					guildBossDataByID = DataManager.GetGuildBossDataByID(guildBossInfo.id);
					TimeSpan localShowTime7 = TimeTools.GetLocalShowTime((long)guildBossDataByID.StartTime, playerCommonData.TimeOffset);
					TimeSpan localShowTime8 = TimeTools.GetLocalShowTime((long)(guildBossDataByID.StartTime + guildBossDataByID.DurationTime), playerCommonData.TimeOffset);
					this.InfoLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime7.Hours, localShowTime7.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime8.Hours, localShowTime8.Minutes));
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{100796}", new object[0]);
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
		{
			WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.CurData.ActivityID);
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{101519}", new object[0]);
			if (this.CurData.IsUnlock)
			{
				activity_info wildBossInfo = this.CurData.GetWildBossInfo();
				if (wildBossInfo != null)
				{
					wildBossDataByID = DataManager.GetWildBossDataByID(wildBossInfo.ID);
					List<TimeSpan> localShowTime9 = TimeTools.GetLocalShowTime(wildBossDataByID.StartTimes, playerCommonData.TimeOffset, (long)wildBossDataByID.DurationTime, (int)wildBossInfo.next, wildBossInfo.State == 2L);
					if (localShowTime9 != null && localShowTime9.Count == 2)
					{
						TimeSpan timeSpan3 = localShowTime9[0];
						TimeSpan timeSpan4 = localShowTime9[1];
						this.InfoLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", timeSpan3.Hours, timeSpan3.Minutes), string.Format("{0:D2}:{1:D2}", timeSpan4.Hours, timeSpan4.Minutes));
					}
					else
					{
						this.InfoLabel.text = string.Empty;
					}
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					wildBossDataByID.LevelMin
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.TOWER)
		{
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{101538}", new object[0]);
			int condition2 = DataManager.GetFunctionDataById(4003.ToString()).Condition;
			if (this.CurData.IsUnlock)
			{
				tower_info playerTowerInfo = playerData.TowerData.PlayerTowerInfo;
				if (playerTowerInfo != null)
				{
					this.InfoLabel.text = string.Format("{0}: {1}", StrDictionary.GetDictionaryString("#{101525}", new object[0]), playerTowerInfo.floor + 1L);
				}
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					condition2
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.RANKPVP)
		{
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100106}", new object[0]);
			int condition3 = DataManager.GetFunctionDataById(3002.ToString()).Condition;
			if (this.CurData.IsUnlock)
			{
				this.InfoLabel.text = string.Format("{0}: {1}", StrDictionary.GetDictionaryString("#{101001}", new object[0]), playerData.RankPVPData.GetRankPosStr());
			}
			else
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					condition3
				});
				this.BtnSp.alpha = 0f;
			}
		}
		else if (this.CurData.ActivityType == GameDefine.ACTIVITY_TYPE.SHOP_GATE)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{301139}", new object[0]);
			int condition4 = DataManager.GetFunctionDataById(4025.ToString()).Condition;
			switch (this.CurData.SubType)
			{
			case 0:
				this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301129}", new object[0]);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301130}", new object[0]);
				condition4 = DataManager.GetFunctionDataById(4021.ToString()).Condition;
				break;
			case 1:
				this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301131}", new object[0]);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301132}", new object[0]);
				condition4 = DataManager.GetFunctionDataById(4022.ToString()).Condition;
				break;
			case 2:
				this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301133}", new object[0]);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301134}", new object[0]);
				condition4 = DataManager.GetFunctionDataById(4023.ToString()).Condition;
				break;
			case 3:
				this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301135}", new object[0]);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301136}", new object[0]);
				condition4 = DataManager.GetFunctionDataById(4024.ToString()).Condition;
				break;
			case 4:
				this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301127}", new object[0]);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301128}", new object[0]);
				condition4 = DataManager.GetFunctionDataById(4025.ToString()).Condition;
				break;
			case 5:
				this.TitleLabel.text = StrDictionary.GetDictionaryString("#{301137}", new object[0]);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301138}", new object[0]);
				condition4 = DataManager.GetFunctionDataById(4027.ToString()).Condition;
				break;
			}
			if (!this.CurData.IsUnlock)
			{
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{605501}", new object[]
				{
					condition4
				});
			}
			this.BtnSp.alpha = 0f;
		}
	}

	// Token: 0x060040BB RID: 16571 RVA: 0x00132124 File Offset: 0x00130324
	public void CloseUI()
	{
		if (this.isShow)
		{
			this.isShow = false;
			this.PosAnima.PlayReverse();
		}
	}

	// Token: 0x060040BC RID: 16572 RVA: 0x00132144 File Offset: 0x00130344
	public void OnClickStartBtn()
	{
		if (this.curType == ActivityTipsRootLogic.ShowType.activity)
		{
			this.OnActivityStart();
		}
		else if (this.curType == ActivityTipsRootLogic.ShowType.shopnpc)
		{
			this.ShopNpcStart();
		}
	}

	// Token: 0x060040BD RID: 16573 RVA: 0x0013217C File Offset: 0x0013037C
	public void ShopNpcStart()
	{
		this.CloseUI();
		if (null != Singleton<ObjManager>.Instance.MainPlayer && null != this.CurShopNpc)
		{
			Singleton<ObjManager>.Instance.MainPlayer.SelectTargetNPC(this.CurShopNpc);
		}
	}

	// Token: 0x060040BE RID: 16574 RVA: 0x001321CC File Offset: 0x001303CC
	public void OnActivityStart()
	{
		if (!this.CurData.IsUnlock)
		{
			return;
		}
		this.CloseUI();
		switch (this.CurData.ActivityType)
		{
		case GameDefine.ACTIVITY_TYPE.ESCORT:
		case GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT:
		case GameDefine.ACTIVITY_TYPE.CITY_DANCE:
		case GameDefine.ACTIVITY_TYPE.BAR_FIGHT:
		case GameDefine.ACTIVITY_TYPE.WILD_BOSS:
		case GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(this.CurData.ActivityType, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BOSS:
		case GameDefine.ACTIVITY_TYPE.GUILD_BATTLE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(this.CurData.ActivityType, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.SEX_MINI:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SEX_GAME, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.DAILY_COPY:
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.CurData.ActivityID);
			if (copySceneDataById.IsPVPMap)
			{
				MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(copySceneDataById.MapId);
				if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
					enter_new_map.request request = new enter_new_map.request();
					request.mapInfoId = mapInfoDataByID.ID;
					NetLogic.GetInstance().Send<Protocol.enter_new_map>(request, null);
					WaitResponseUIRootLogic.OpenWaitBox(106, 10f, 0f, null);
				}
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					if (this.CurData.IsNeedDailyActid())
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn((MAPTYPE)this.CurData.SubType, this.CurData.ActivityID, null, GameDefine.ACTIVITY_TYPE.INVALID);
					}
					else
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn((MAPTYPE)this.CurData.SubType, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
					}
				}, null);
			}
			break;
		}
		case GameDefine.ACTIVITY_TYPE.MISSION:
			if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogMissionUI, delegate
				{
					SingletonUnity<DialogMissionUIRoot>.Instance.ResetActMissionAcceptUI(this.CurData.ActivityID);
				}, null);
			}
			break;
		case GameDefine.ACTIVITY_TYPE.TOWER:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.INVALID, null, null, GameDefine.ACTIVITY_TYPE.TOWER);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.RANKPVP:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.ResetToPVP();
			}, null);
			break;
		}
	}

	// Token: 0x04002C4E RID: 11342
	public UILabel TitleLabel;

	// Token: 0x04002C4F RID: 11343
	public UILabel InfoLabel;

	// Token: 0x04002C50 RID: 11344
	public UISprite picSp;

	// Token: 0x04002C51 RID: 11345
	private ActivityMapData CurData;

	// Token: 0x04002C52 RID: 11346
	public TweenPosition PosAnima;

	// Token: 0x04002C53 RID: 11347
	public bool isShow;

	// Token: 0x04002C54 RID: 11348
	public UISprite BtnSp;

	// Token: 0x04002C55 RID: 11349
	public UILabel BtnLabel;

	// Token: 0x04002C56 RID: 11350
	private ActivityTipsRootLogic.ShowType curType;

	// Token: 0x04002C57 RID: 11351
	private ObjNPC CurShopNpc;

	// Token: 0x0200091B RID: 2331
	public enum ShowType
	{
		// Token: 0x04002C5C RID: 11356
		activity,
		// Token: 0x04002C5D RID: 11357
		shopnpc
	}
}
