using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class ActivityData
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000765 RID: 1893 RVA: 0x00033670 File Offset: 0x00031870
	public Dictionary<string, activity_info> CurActivityDataDic
	{
		get
		{
			return this.mCurActivityDataDic;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000766 RID: 1894 RVA: 0x00033678 File Offset: 0x00031878
	public Dictionary<string, activity_info> CurWildBossDataDic
	{
		get
		{
			return this.mCurWildBossDataDic;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06000767 RID: 1895 RVA: 0x00033680 File Offset: 0x00031880
	public Dictionary<string, guild_boss> CurGuildBossDataDic
	{
		get
		{
			return this.mCurGuildBossDataDic;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06000768 RID: 1896 RVA: 0x00033688 File Offset: 0x00031888
	public List<activity_info> ActivityInfoList
	{
		get
		{
			return this.mActivityInfoList;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06000769 RID: 1897 RVA: 0x00033690 File Offset: 0x00031890
	public List<activity_info> WildBossInfoList
	{
		get
		{
			return this.mWildBossInfoList;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x0600076A RID: 1898 RVA: 0x00033698 File Offset: 0x00031898
	public List<guild_boss> GuildBossInfoList
	{
		get
		{
			return this.mGuildBossInfoList;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x0600076B RID: 1899 RVA: 0x000336A0 File Offset: 0x000318A0
	public guild_battle_info GuildBattleInfo
	{
		get
		{
			return this.mguild_battle_info;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x0600076C RID: 1900 RVA: 0x000336A8 File Offset: 0x000318A8
	public dance_state_info GuildDanceInfo
	{
		get
		{
			return this.mguild_dance_info;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x0600076D RID: 1901 RVA: 0x000336B0 File Offset: 0x000318B0
	public List<guild_member_info> guild_member_info
	{
		get
		{
			return this.mguild_member_info;
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600076E RID: 1902 RVA: 0x000336B8 File Offset: 0x000318B8
	public Dictionary<long, dance_state_info> CurDanceStateDic
	{
		get
		{
			return this.mCurDanceStateDic;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600076F RID: 1903 RVA: 0x000336C0 File Offset: 0x000318C0
	public int CurDanceState
	{
		get
		{
			return this.mCurDanceState;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000770 RID: 1904 RVA: 0x000336C8 File Offset: 0x000318C8
	public int CurDanceOpen
	{
		get
		{
			return this.mCurDanceOpen;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000771 RID: 1905 RVA: 0x000336D0 File Offset: 0x000318D0
	public int FirstGuildDanceOpen
	{
		get
		{
			return this.mFirstGuildDanceOpen;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000772 RID: 1906 RVA: 0x000336D8 File Offset: 0x000318D8
	public int GuildDanceOpen
	{
		get
		{
			return this.mGuildDanceOpen;
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000773 RID: 1907 RVA: 0x000336E0 File Offset: 0x000318E0
	public int GuildDonmineOpen
	{
		get
		{
			return this.mGuildDonmineOpen;
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000774 RID: 1908 RVA: 0x000336E8 File Offset: 0x000318E8
	public List<string> MissionTimeOutList
	{
		get
		{
			return this.mMissionTimeOutList;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06000775 RID: 1909 RVA: 0x000336F0 File Offset: 0x000318F0
	public Dictionary<string, guild_map_info> CurGuildCityDataDic
	{
		get
		{
			return this.mCurGuildCityDataDic;
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x000336F8 File Offset: 0x000318F8
	public void Reset()
	{
		this.GuildLevel = 0;
		if (this.mCurActivityDataDic != null)
		{
			this.mCurActivityDataDic.Clear();
		}
		if (this.mCurWildBossDataDic != null)
		{
			this.mCurWildBossDataDic.Clear();
		}
		if (this.mCurGuildBossDataDic != null)
		{
			this.mCurGuildBossDataDic.Clear();
		}
		if (this.mCurDanceStateDic != null)
		{
			this.mCurDanceStateDic.Clear();
		}
		if (this.mCurGuildCityDataDic != null)
		{
			this.mCurGuildCityDataDic.Clear();
		}
		this.mActivityInfoList.Clear();
		this.mWildBossInfoList.Clear();
		this.GuildBossInfoList.Clear();
		this.mguild_battle_info = null;
		this.mguild_dance_info = null;
		this.mguild_member_info.Clear();
		this.mCurDanceState = 0;
		this.mCurDanceOpen = 0;
		this.mFirstGuildDanceOpen = 0;
		this.mGuildDanceOpen = 0;
		this.mMissionTimeOutList.Clear();
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x000337DC File Offset: 0x000319DC
	public void SyncGuildCityInfo(ret_request_guild_map_info.request request)
	{
		if (this.mCurGuildCityDataDic != null)
		{
			this.mCurGuildCityDataDic.Clear();
		}
		if (request.HasGuild_map_info)
		{
			this.mCurGuildCityDataDic = request.guild_map_info;
		}
		this.UpdateTips();
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00033814 File Offset: 0x00031A14
	public void SyncGuildCityRewardInfo(ret_guild_map_reward.request request)
	{
		if (this.mCurGuildCityDataDic == null)
		{
			return;
		}
		if (request.HasId && this.mCurGuildCityDataDic.ContainsKey(request.id))
		{
			this.mCurGuildCityDataDic[request.id].requireState = request.state;
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0003386C File Offset: 0x00031A6C
	public bool IsOpenCityCapture(string mapid)
	{
		if (this.CurGuildCityDataDic == null)
		{
			return false;
		}
		foreach (guild_map_info guild_map_info in this.CurGuildCityDataDic.Values)
		{
			GuildCaptureData guildCaptureDataByID = DataManager.GetGuildCaptureDataByID(guild_map_info.id);
			if (guildCaptureDataByID.MapID.Equals(mapid) && guild_map_info.state == 1L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00033914 File Offset: 0x00031B14
	public Vector3 GetCityCaptureNpcPos(MapInfoData mapinfo)
	{
		if (this.CurGuildCityDataDic == null)
		{
			return mapinfo.BirthPosVector3;
		}
		foreach (guild_map_info guild_map_info in this.CurGuildCityDataDic.Values)
		{
			GuildCaptureData guildCaptureDataByID = DataManager.GetGuildCaptureDataByID(guild_map_info.id);
			if (guildCaptureDataByID.MapID.Equals(mapinfo.ID) && guild_map_info.state == 1L)
			{
				return guildCaptureDataByID.GetNpcPos();
			}
		}
		return mapinfo.BirthPosVector3;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x000339D0 File Offset: 0x00031BD0
	public guild_map_info GetGuildMapInfo(string mapid)
	{
		if (this.CurGuildCityDataDic == null)
		{
			return null;
		}
		foreach (guild_map_info guild_map_info in this.CurGuildCityDataDic.Values)
		{
			GuildCaptureData guildCaptureDataByID = DataManager.GetGuildCaptureDataByID(guild_map_info.id);
			if (guildCaptureDataByID.MapID.Equals(mapid))
			{
				return guild_map_info;
			}
		}
		return null;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x00033A68 File Offset: 0x00031C68
	public void SetCityCaptureFinish(string id)
	{
		if (this.CurGuildCityDataDic != null && this.CurGuildCityDataDic.ContainsKey(id) && this.CurGuildCityDataDic[id].state == 1L)
		{
			this.CurGuildCityDataDic[id].state = 2L;
		}
		this.UpdateCityShow();
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00033AC4 File Offset: 0x00031CC4
	public void SetAllCityCaptureOpenState(bool isOpen)
	{
		if (this.CurGuildCityDataDic != null)
		{
			foreach (guild_map_info guild_map_info in this.CurGuildCityDataDic.Values)
			{
				if (isOpen)
				{
					this.CurGuildCityDataDic[guild_map_info.id].state = 1L;
				}
				else
				{
					this.CurGuildCityDataDic[guild_map_info.id].state = 0L;
				}
			}
		}
		this.UpdateCityShow();
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00033B74 File Offset: 0x00031D74
	public dance_state_info GetDanceInfoByType(GameDefine.DANCE_TYPE dancetype)
	{
		List<dance_state_info> list = new List<dance_state_info>(this.mCurDanceStateDic.Values);
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(list[i].ID);
				if (cityDanceDataById.Type == (int)dancetype)
				{
					return list[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00033BE4 File Offset: 0x00031DE4
	public void SyncDanceStateInfo(sync_dance_state_info.request request)
	{
		if (request.HasState)
		{
			this.mCurDanceState = (int)request.state;
		}
		if (request.HasOpen)
		{
			this.mCurDanceOpen = (int)request.open;
		}
		if (request.HasDance_state_info)
		{
			this.mCurDanceStateDic = request.dance_state_info;
		}
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00033C38 File Offset: 0x00031E38
	public bool IsCanDance()
	{
		List<dance_state_info> list = new List<dance_state_info>(this.mCurDanceStateDic.Values);
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(list[i].ID);
				if (cityDanceDataById.Type != 0)
				{
					return true;
				}
				if (cityDanceDataById.Type == 0 && list[i].HasDuration && list[i].duration > 0L)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00033CD0 File Offset: 0x00031ED0
	public void UpdateActivity(chat_item listdata)
	{
		List<long> intdata = listdata.intdata;
		List<string> strdata = new List<string>();
		if (listdata.HasStringdata)
		{
			strdata = listdata.stringdata;
		}
		if (intdata.Count >= 2)
		{
			this.ChangeActivity((int)intdata[0], (int)intdata[1], strdata);
			this.UpdateTips();
		}
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00033D28 File Offset: 0x00031F28
	public void ChangeActivity(int first, int secd, List<string> strdata)
	{
		switch (first)
		{
		case 1:
		case 2:
			for (int i = 0; i < this.mActivityInfoList.Count; i++)
			{
				if (this.mActivityInfoList[i].Type == 1L || this.mActivityInfoList[i].Type == 2L)
				{
					this.mActivityInfoList[i].State = (long)secd;
				}
			}
			break;
		case 4:
			for (int j = 0; j < this.mActivityInfoList.Count; j++)
			{
				if (this.mActivityInfoList[j].Type == (long)first)
				{
					this.mActivityInfoList[j].State = (long)secd;
				}
			}
			break;
		case 5:
			for (int k = 0; k < this.mWildBossInfoList.Count; k++)
			{
				this.mWildBossInfoList[k].State = (long)secd;
			}
			break;
		case 6:
			for (int l = 0; l < this.mGuildBossInfoList.Count; l++)
			{
				this.mGuildBossInfoList[l].state = (long)secd;
			}
			break;
		case 7:
			for (int m = 0; m < this.mActivityInfoList.Count; m++)
			{
				if (this.mActivityInfoList[m].Type == (long)first)
				{
					this.mActivityInfoList[m].State = (long)secd;
				}
			}
			break;
		case 9:
			if (this.mguild_battle_info != null)
			{
				this.mguild_battle_info.state = (long)secd;
			}
			break;
		case 15:
			this.mFirstGuildDanceOpen = secd;
			break;
		case 16:
			this.mGuildDanceOpen = secd;
			break;
		case 17:
		{
			string text = strdata[0];
			for (int n = 0; n < this.mMissionTimeOutList.Count; n++)
			{
				if (this.mMissionTimeOutList[n].Equals(text))
				{
					return;
				}
			}
			this.mMissionTimeOutList.Add(text);
			break;
		}
		case 18:
			this.mGuildDonmineOpen = secd;
			this.SetAllCityCaptureOpenState(secd == 1);
			break;
		case 19:
			this.SetCityCaptureFinish(strdata[1]);
			break;
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00033FB0 File Offset: 0x000321B0
	public bool IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE activitytype)
	{
		if (activitytype == GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
		{
			return this.IsHaveGuildBossTips();
		}
		if (activitytype == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
		{
			return this.IsHaveWildBoss();
		}
		if (activitytype == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
		{
			return this.IsHaveGuildBattleTips();
		}
		if (activitytype == GameDefine.ACTIVITY_TYPE.GUILD_DANCE)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild() && this.GuildDanceOpen == 1;
		}
		if (activitytype == GameDefine.ACTIVITY_TYPE.GUILD_DONMINE)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild() && this.GuildDonmineOpen == 1;
		}
		return this.CheckTipsByType(activitytype);
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00034040 File Offset: 0x00032240
	public bool IsHaveMissionTimeOut()
	{
		return this.MissionTimeOutList != null && this.MissionTimeOutList.Count > 0;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00034064 File Offset: 0x00032264
	public void DecMissionLine(string missionid)
	{
		if (this.MissionTimeOutList != null && this.MissionTimeOutList.Count > 0)
		{
			for (int i = this.MissionTimeOutList.Count - 1; i >= 0; i--)
			{
				if (this.MissionTimeOutList[i].Equals(missionid))
				{
					this.MissionTimeOutList.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x000340D0 File Offset: 0x000322D0
	public bool CheckTipsByType(GameDefine.ACTIVITY_TYPE acttype)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return false;
		}
		if (this.mActivityInfoList.Count > 0)
		{
			for (int i = 0; i < this.mActivityInfoList.Count; i++)
			{
				if (this.mActivityInfoList[i].Type == (long)acttype)
				{
					activity_info activity_info = this.mActivityInfoList[i];
					if (activity_info.Type == 1L || activity_info.Type == 2L)
					{
						EscortData escortDataById = DataManager.GetEscortDataById(activity_info.ID);
						if (this.mActivityInfoList[i].State == 1L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(escortDataById.UnlockLevel) && activity_info.CurNum > 0L)
						{
							return true;
						}
					}
					else if (activity_info.Type == 3L)
					{
						CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(activity_info.ID);
						if (this.mActivityInfoList[i].State == 1L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(cityDanceDataById.UnlockLevel))
						{
							return true;
						}
					}
					else if (activity_info.Type == 4L)
					{
						BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(activity_info.ID);
						if (this.mActivityInfoList[i].State == 2L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(barFightCopyDataByID.UnlockLevel))
						{
							return true;
						}
					}
					else if (activity_info.Type == 7L)
					{
						SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(activity_info.ID);
						if (this.mActivityInfoList[i].State == 1L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(surviveBattleDataById.UnlockLevel))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x000342B0 File Offset: 0x000324B0
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMessageTips();
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.IsHaveGuildTips(), GameDefine.TIPS_TYPE.GUILD);
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateCityDamageFlag();
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
		if (SingletonUnity<ActivityTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ActivityTipsRootLogic>.Instance.UpdateInfo();
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MapActivityManager != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MapActivityManager.UpdateTimeActivityMapFlag();
		}
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0003437C File Offset: 0x0003257C
	public void UpdateCityShow()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateCityDamageFlag();
		}
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00034394 File Offset: 0x00032594
	public void SyncGuildBattleMember(ret_guild_battle_member.request request)
	{
		if (request.HasGuild_member_info)
		{
			this.mguild_member_info = request.guild_member_info;
		}
		this.UpdateTips();
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x000343B4 File Offset: 0x000325B4
	public void SyncGuildBattleInfo(ret_guild_battle_state.request request)
	{
		if (request.HasBattle_info)
		{
			this.mguild_battle_info = request.battle_info;
		}
		this.UpdateTips();
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x000343D4 File Offset: 0x000325D4
	public void SyncGuildBattleInfo(ret_guild_battle_info.request request)
	{
		if (request.HasBattle_info)
		{
			this.mguild_battle_info = request.battle_info;
		}
		this.UpdateTips();
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x000343F4 File Offset: 0x000325F4
	public activity_info GetActivityInfoByType(int type)
	{
		List<activity_info> list = new List<activity_info>(this.mCurActivityDataDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Type == (long)type)
			{
				return list[i];
			}
		}
		return null;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00034448 File Offset: 0x00032648
	public void SyncActivityInfoData(ret_request_activity_info.request request)
	{
		this.mCurActivityDataDic = new Dictionary<string, activity_info>(request.activity_info);
		List<activity_info> list = new List<activity_info>(this.mCurActivityDataDic.Values);
		this.mActivityInfoList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Type != 5L)
			{
				if (list[i].Type != 6L)
				{
					this.mActivityInfoList.Add(list[i]);
				}
			}
		}
		this.UpdateTips();
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x000344E4 File Offset: 0x000326E4
	public activity_info GetWildBossInfo()
	{
		for (int i = 0; i < this.mWildBossInfoList.Count; i++)
		{
			WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.mWildBossInfoList[i].ID);
			if (this.CheckLevel(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax))
			{
				return this.mWildBossInfoList[i];
			}
		}
		return null;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0003454C File Offset: 0x0003274C
	public void SyncWildBossInfoData(ret_request_wild_boss_info.request request)
	{
		this.mCurWildBossDataDic = new Dictionary<string, activity_info>(request.activity_info);
		List<activity_info> list = new List<activity_info>(this.mCurWildBossDataDic.Values);
		this.mWildBossInfoList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Type == 5L)
			{
				WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(list[i].ID);
				if (wildBossDataByID.PVP == 1)
				{
					if ((long)wildBossDataByID.Time == list[i].next)
					{
						this.mWildBossInfoList.Add(list[i]);
					}
				}
				else if ((long)wildBossDataByID.Time != list[i].next)
				{
					this.mWildBossInfoList.Add(list[i]);
				}
			}
		}
		this.UpdateTips();
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00034630 File Offset: 0x00032830
	public guild_boss GetGuildBoss()
	{
		for (int i = 0; i < this.mGuildBossInfoList.Count; i++)
		{
			GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(this.mGuildBossInfoList[i].id);
			if (this.mGuildBossInfoList[i].state == 1L && this.GuildLevel >= guildBossDataByID.LevelMin)
			{
				return this.mGuildBossInfoList[i];
			}
		}
		if (this.mGuildBossInfoList.Count > 0)
		{
			return this.mGuildBossInfoList[0];
		}
		return null;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x000346C8 File Offset: 0x000328C8
	public void SyncGuildBossInfoData(ret_request_guild_boss.request request)
	{
		if (request.HasGuild_boss)
		{
			this.mCurGuildBossDataDic = new Dictionary<string, guild_boss>(request.guild_boss);
			this.mGuildBossInfoList = new List<guild_boss>(request.guild_boss.Values);
		}
		if (request.HasLevel)
		{
			this.GuildLevel = (int)request.level;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.GuildLevel = 0;
		}
		if (request.HasGuild_battle_info)
		{
			this.mguild_battle_info = request.guild_battle_info;
		}
		if (request.HasDance_state_info)
		{
			this.mguild_dance_info = request.dance_state_info;
		}
		if (request.HasGuild_map_info)
		{
			this.mCurGuildCityDataDic = request.guild_map_info;
		}
		this.UpdateTips();
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00034788 File Offset: 0x00032988
	public bool IsHaveGuildTips()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return this.IsHaveGuildActTips() || playerData.PlayerGuild.IsHaveNewApply();
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x000347BC File Offset: 0x000329BC
	public bool IsHaveGuildActTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD_ACTIVITY) && (this.IsHaveGuildBossTips() || this.IsHaveGuildBattleTips() || this.IsHaveGuildDanceTips() || this.IsHaveGuildCityTips());
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00034810 File Offset: 0x00032A10
	public bool IsHaveGuildCityTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			return false;
		}
		if (this.mGuildDonmineOpen != 1)
		{
			return false;
		}
		if (this.CurGuildCityDataDic == null)
		{
			return false;
		}
		foreach (guild_map_info guild_map_info in this.CurGuildCityDataDic.Values)
		{
			if (guild_map_info.state == 1L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x000348BC File Offset: 0x00032ABC
	public bool IsHaveGuildDanceTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild() && (this.GuildDanceInfo != null && this.GuildDanceInfo.HasState && this.GuildDanceInfo.state == 1L);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00034910 File Offset: 0x00032B10
	public bool IsHaveGuildBattleTips()
	{
		if (this.mguild_battle_info == null || this.mguild_member_info.Count == 0)
		{
			return false;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < this.mguild_member_info.Count; i++)
		{
			if (this.mguild_member_info[i].characterId == PlayerData.MainPlayerServerId)
			{
				flag = true;
				break;
			}
		}
		return flag && (this.GuildBattleInfo.state == 1L || this.GuildBattleInfo.state == 3L || this.GuildBattleInfo.state == 5L);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x000349D4 File Offset: 0x00032BD4
	public bool IsHaveGuildBossTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD_ACTIVITY))
		{
			return false;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			return false;
		}
		if (this.mGuildBossInfoList.Count > 0)
		{
			for (int i = 0; i < this.mGuildBossInfoList.Count; i++)
			{
				GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(this.mGuildBossInfoList[i].id);
				if (this.mGuildBossInfoList[i].state == 1L && this.GuildLevel >= guildBossDataByID.LevelMin)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00034A84 File Offset: 0x00032C84
	public bool IsHaveActTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_TIME) && (this.IsHaveActivity() || this.IsHaveWildBoss() || this.IsHaveGuildActTips());
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00034ACC File Offset: 0x00032CCC
	public bool IsHaveActivity()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return false;
		}
		if (this.mActivityInfoList.Count > 0)
		{
			for (int i = 0; i < this.mActivityInfoList.Count; i++)
			{
				activity_info activity_info = this.mActivityInfoList[i];
				if (activity_info.Type == 1L || activity_info.Type == 2L)
				{
					if (activity_info.CurNum > 0L && this.mActivityInfoList[i].State == 1L)
					{
						return true;
					}
				}
				else if (this.mActivityInfoList[i].State == 1L)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00034B94 File Offset: 0x00032D94
	public bool IsHaveWildBoss()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_WORLDBOSS))
		{
			return false;
		}
		if (this.mWildBossInfoList.Count > 0)
		{
			for (int i = 0; i < this.mWildBossInfoList.Count; i++)
			{
				WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.mWildBossInfoList[i].ID);
				if (wildBossDataByID != null && this.CheckLevel(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax) && this.mWildBossInfoList[i].State == 1L)
				{
					if (this.mWildBossInfoList[i].CurNum > 0L || SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() <= this.mWildBossInfoList[i].time + 5400L)
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00034C84 File Offset: 0x00032E84
	public bool CheckLevel(int minLevel, int maxlevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxlevel);
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00034C98 File Offset: 0x00032E98
	public string GetCityDanceInfo()
	{
		string result = string.Empty;
		for (int i = 0; i < this.mActivityInfoList.Count; i++)
		{
			if (this.mActivityInfoList[i].Type == 3L)
			{
				result = this.mActivityInfoList[i].ID;
				break;
			}
		}
		return result;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00034CF8 File Offset: 0x00032EF8
	public int GetActivityUnlockLevel(activity_info info)
	{
		int result = 0;
		long type = info.Type;
		if (type >= 1L && type <= 7L)
		{
			switch ((int)(type - 1L))
			{
			case 0:
			{
				EscortData escortDataById = DataManager.GetEscortDataById(info.ID);
				result = escortDataById.UnlockLevel;
				break;
			}
			case 1:
			{
				EscortData escortDataById2 = DataManager.GetEscortDataById(info.ID);
				result = escortDataById2.UnlockLevel;
				break;
			}
			case 2:
			{
				CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(info.ID);
				result = cityDanceDataById.UnlockLevel;
				break;
			}
			case 3:
			{
				BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(info.ID);
				result = barFightCopyDataByID.UnlockLevel;
				break;
			}
			case 6:
			{
				SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(info.ID);
				result = surviveBattleDataById.UnlockLevel;
				break;
			}
			}
		}
		return result;
	}

	// Token: 0x04000655 RID: 1621
	private Dictionary<string, activity_info> mCurActivityDataDic = new Dictionary<string, activity_info>();

	// Token: 0x04000656 RID: 1622
	private Dictionary<string, activity_info> mCurWildBossDataDic = new Dictionary<string, activity_info>();

	// Token: 0x04000657 RID: 1623
	private Dictionary<string, guild_boss> mCurGuildBossDataDic = new Dictionary<string, guild_boss>();

	// Token: 0x04000658 RID: 1624
	private List<activity_info> mActivityInfoList = new List<activity_info>();

	// Token: 0x04000659 RID: 1625
	private List<activity_info> mWildBossInfoList = new List<activity_info>();

	// Token: 0x0400065A RID: 1626
	private List<guild_boss> mGuildBossInfoList = new List<guild_boss>();

	// Token: 0x0400065B RID: 1627
	private guild_battle_info mguild_battle_info;

	// Token: 0x0400065C RID: 1628
	public dance_state_info mguild_dance_info;

	// Token: 0x0400065D RID: 1629
	private List<guild_member_info> mguild_member_info = new List<guild_member_info>();

	// Token: 0x0400065E RID: 1630
	private int GuildLevel;

	// Token: 0x0400065F RID: 1631
	private Dictionary<long, dance_state_info> mCurDanceStateDic = new Dictionary<long, dance_state_info>();

	// Token: 0x04000660 RID: 1632
	private int mCurDanceState;

	// Token: 0x04000661 RID: 1633
	private int mCurDanceOpen;

	// Token: 0x04000662 RID: 1634
	private int mFirstGuildDanceOpen;

	// Token: 0x04000663 RID: 1635
	private int mGuildDanceOpen;

	// Token: 0x04000664 RID: 1636
	private int mGuildDonmineOpen;

	// Token: 0x04000665 RID: 1637
	private List<string> mMissionTimeOutList = new List<string>();

	// Token: 0x04000666 RID: 1638
	private Dictionary<string, guild_map_info> mCurGuildCityDataDic = new Dictionary<string, guild_map_info>();
}
