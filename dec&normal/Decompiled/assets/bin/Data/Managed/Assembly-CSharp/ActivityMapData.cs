using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class ActivityMapData
{
	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00060394 File Offset: 0x0005E594
	public Vector3 Position
	{
		get
		{
			float num = (float)this.PosX / 100f;
			float num2 = (float)this.PosZ / 100f;
			return new Vector3(num, SceneManager.GetHitHeight(num, num2), num2);
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06000EAC RID: 3756 RVA: 0x000603CC File Offset: 0x0005E5CC
	public GameDefine.ACTIVITY_TYPE ActivityType
	{
		get
		{
			return (GameDefine.ACTIVITY_TYPE)this.Type;
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000603D4 File Offset: 0x0005E5D4
	public bool IsShowDoorFlag()
	{
		return this.ActivityType == GameDefine.ACTIVITY_TYPE.SHOP_GATE;
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000EAE RID: 3758 RVA: 0x000603E0 File Offset: 0x0005E5E0
	public bool IsUnlock
	{
		get
		{
			if (this.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
			{
				this.isCheckVisible = -1;
				this.isCheckUnlock = -1;
				this.Init();
				return this.isCheckUnlock == 1;
			}
			if (this.isCheckUnlock == 1)
			{
				return true;
			}
			this.Init();
			return this.isCheckUnlock == 1;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00060438 File Offset: 0x0005E638
	public bool IsVisible
	{
		get
		{
			if (this.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
			{
				this.isCheckVisible = -1;
				this.isCheckUnlock = -1;
				this.Init();
				return this.isCheckVisible == 1;
			}
			if (this.isCheckVisible == 1)
			{
				return true;
			}
			this.Init();
			return this.isCheckVisible == 1;
		}
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x00060490 File Offset: 0x0005E690
	private void Init()
	{
		if (this.isCheckVisible != -1 && this.isCheckUnlock != -1)
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerData == null)
		{
			return;
		}
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.ESCORT)
		{
			EscortData escortDataById = DataManager.GetEscortDataById(this.ActivityID);
			if (playerData.CheckLevel(escortDataById.UnlockLevel))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.BAR_FIGHT)
		{
			BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(this.ActivityID);
			if (playerData.CheckLevel(barFightCopyDataByID.UnlockLevel))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.CITY_DANCE)
		{
			CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(this.ActivityID);
			if (playerData.CheckLevel(cityDanceDataById.UnlockLevel))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.DAILY_COPY)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.ActivityID);
			if (copySceneDataById != null)
			{
				if (playerData.CheckLevel(copySceneDataById.MinLevel))
				{
					this.isCheckUnlock = 1;
				}
				this.isCheckVisible = 1;
			}
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
		{
			GuildBattleData guildBattleDataById = DataManager.GetGuildBattleDataById(this.ActivityID);
			if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD_ACTIVITY))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
		{
			GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(this.ActivityID);
			if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD_ACTIVITY) && playerData.IsHaveGuild())
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.SEX_MINI)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.ActivityID);
			if (playerData.CheckLevel(sexMiniDataById.UnlockLevel))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE)
		{
			SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(this.ActivityID);
			if (playerData.CheckLevel(surviveBattleDataById.UnlockLevel))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
		{
			WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.ActivityID);
			if (playerData.CheckLevel(wildBossDataByID.LevelMin))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.IsMissionAcceptable(this.ActivityID))
			{
				this.isCheckUnlock = 1;
				this.isCheckVisible = 1;
			}
			else
			{
				this.isCheckUnlock = -1;
				this.isCheckVisible = -1;
			}
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.TOWER)
		{
			if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_CHALLENGE))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.RANKPVP)
		{
			if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.RANK_PVP))
			{
				this.isCheckUnlock = 1;
			}
			this.isCheckVisible = 1;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.DOMIN)
		{
			if (playerData.Domin_InfoDic.ContainsKey(this.ActivityID))
			{
				DominData dominDataByID = DataManager.GetDominDataByID(this.ActivityID);
				if (playerData.Level >= dominDataByID.LevelMin)
				{
					this.isCheckUnlock = 1;
				}
				this.isCheckVisible = 1;
			}
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.SHOP_GATE)
		{
			this.isCheckVisible = 1;
			this.isCheckUnlock = 1;
		}
		if (this.MinLevel != -2147483648 && this.MaxLevel != 2147483647)
		{
			if (playerData.CheckLevel(this.MinLevel, this.MaxLevel))
			{
				this.isCheckVisible = 1;
			}
			else
			{
				this.isCheckVisible = -1;
			}
		}
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x00060880 File Offset: 0x0005EA80
	public copyscene_info GetDailyCopyInfo()
	{
		if (this.ActivityType != GameDefine.ACTIVITY_TYPE.DAILY_COPY)
		{
			return null;
		}
		if (!this.IsNeedDailyActid())
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			return playerData.CopyInfoData.GetCopyinfoByType(this.SubType);
		}
		PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return playerData2.CopyInfoData.GetCopyinfoByID(this.ActivityID);
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000608E0 File Offset: 0x0005EAE0
	public bool IsNeedDailyActid()
	{
		return this.SubType != 12 && this.SubType != 7 && this.SubType != 11 && this.SubType != 20 && this.SubType != 16 && this.SubType != 26 && this.SubType != 27;
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0006094C File Offset: 0x0005EB4C
	public bool IsTimeActivity()
	{
		return this.ActivityType == GameDefine.ACTIVITY_TYPE.ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.CITY_DANCE || this.ActivityType == GameDefine.ACTIVITY_TYPE.BAR_FIGHT || this.ActivityType == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE || this.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS || this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE || this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BOSS;
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x000609C0 File Offset: 0x0005EBC0
	public bool IsInTimeActivityUI()
	{
		return this.ActivityType == GameDefine.ACTIVITY_TYPE.ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.CITY_DANCE || this.ActivityType == GameDefine.ACTIVITY_TYPE.BAR_FIGHT || this.ActivityType == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE || this.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS;
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x00060A18 File Offset: 0x0005EC18
	public activity_info GetActivityInfo()
	{
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.CITY_DANCE || this.ActivityType == GameDefine.ACTIVITY_TYPE.BAR_FIGHT || this.ActivityType == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			return playerData.ActivityData.GetActivityInfoByType(this.Type);
		}
		return null;
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00060A80 File Offset: 0x0005EC80
	public guild_battle_info GetGuildBattleInfo()
	{
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			return playerData.ActivityData.GuildBattleInfo;
		}
		return null;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00060AB4 File Offset: 0x0005ECB4
	public guild_boss GetGuildBossInfo()
	{
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			return playerData.ActivityData.GetGuildBoss();
		}
		return null;
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x00060AE8 File Offset: 0x0005ECE8
	public activity_info GetWildBossInfo()
	{
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			return playerData.ActivityData.GetWildBossInfo();
		}
		return null;
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x00060B1C File Offset: 0x0005ED1C
	public bool CheckCanGoTo()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			return true;
		}
		if (this.ActivityType == GameDefine.ACTIVITY_TYPE.DAILY_COPY)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.ActivityID);
			if (!this.IsUnlock)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					copySceneDataById.MinLevel
				}), true, false);
				return false;
			}
			return true;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || this.ActivityType == GameDefine.ACTIVITY_TYPE.ESCORT)
		{
			EscortData escortDataById = DataManager.GetEscortDataById(this.ActivityID);
			if (!this.IsUnlock)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					escortDataById.UnlockLevel
				}), true, false);
				return false;
			}
			return true;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.BAR_FIGHT)
		{
			BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(this.ActivityID);
			if (!this.IsUnlock)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					barFightCopyDataByID.UnlockLevel
				}), true, false);
				return false;
			}
			return true;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.CITY_DANCE)
		{
			CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(this.ActivityID);
			if (!this.IsUnlock)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					cityDanceDataById.UnlockLevel
				}), true, false);
				return false;
			}
			return true;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.SEX_MINI)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.ActivityID);
			if (!this.IsUnlock)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					sexMiniDataById.UnlockLevel
				}), true, false);
				return false;
			}
			return true;
		}
		else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE)
		{
			SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(this.ActivityID);
			if (!this.IsUnlock)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					surviveBattleDataById.UnlockLevel
				}), true, false);
				return false;
			}
			return true;
		}
		else
		{
			if (this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
			{
				return true;
			}
			if (this.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
			{
				GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(this.ActivityID);
				if (!this.IsUnlock)
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102006}", new object[0]), true, false);
					return false;
				}
				return true;
			}
			else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
			{
				WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.ActivityID);
				if (!this.IsUnlock)
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
					{
						wildBossDataByID.LevelMin
					}), true, false);
					return false;
				}
				return true;
			}
			else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.TOWER)
			{
				int condition = DataManager.GetFunctionDataById(4003.ToString()).Condition;
				if (!this.IsUnlock)
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
					{
						condition
					}), true, false);
					return false;
				}
				return true;
			}
			else if (this.ActivityType == GameDefine.ACTIVITY_TYPE.RANKPVP)
			{
				int condition2 = DataManager.GetFunctionDataById(3002.ToString()).Condition;
				if (!this.IsUnlock)
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
					{
						condition2
					}), true, false);
					return false;
				}
				return true;
			}
			else
			{
				if (this.ActivityType != GameDefine.ACTIVITY_TYPE.DOMIN)
				{
					return this.ActivityType == GameDefine.ACTIVITY_TYPE.SHOP_GATE && this.IsUnlock;
				}
				if (!this.IsUnlock)
				{
					NoticeLogic.AddNotifyData("#{103009}", true, false);
					return false;
				}
				return true;
			}
		}
	}

	// Token: 0x04000C29 RID: 3113
	public static int DefaultSubType = -1;

	// Token: 0x04000C2A RID: 3114
	public string ID = string.Empty;

	// Token: 0x04000C2B RID: 3115
	public int Type = -1;

	// Token: 0x04000C2C RID: 3116
	public int SubType = ActivityMapData.DefaultSubType;

	// Token: 0x04000C2D RID: 3117
	public string ActivityID = string.Empty;

	// Token: 0x04000C2E RID: 3118
	public string MapId = string.Empty;

	// Token: 0x04000C2F RID: 3119
	public int PosX;

	// Token: 0x04000C30 RID: 3120
	public int PosZ;

	// Token: 0x04000C31 RID: 3121
	public string Icon = string.Empty;

	// Token: 0x04000C32 RID: 3122
	public int Color;

	// Token: 0x04000C33 RID: 3123
	public int MinLevel = int.MinValue;

	// Token: 0x04000C34 RID: 3124
	public int MaxLevel = int.MaxValue;

	// Token: 0x04000C35 RID: 3125
	public string TargetMapID;

	// Token: 0x04000C36 RID: 3126
	private int isCheckUnlock = -1;

	// Token: 0x04000C37 RID: 3127
	private int isCheckVisible = -1;
}
