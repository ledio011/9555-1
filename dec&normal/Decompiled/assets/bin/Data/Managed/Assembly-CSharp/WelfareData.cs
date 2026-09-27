using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x020001C6 RID: 454
public class WelfareData
{
	// Token: 0x1700038E RID: 910
	// (get) Token: 0x0600104A RID: 4170 RVA: 0x00066984 File Offset: 0x00064B84
	public Dictionary<string, level_pack> LevelPackDic
	{
		get
		{
			return this.levelPack;
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x0600104B RID: 4171 RVA: 0x0006698C File Offset: 0x00064B8C
	public Dictionary<string, daily_active> DailyActives
	{
		get
		{
			return this.daily_actives;
		}
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x00066994 File Offset: 0x00064B94
	public void Reset()
	{
		this.curWeekSignDay = -1;
		this.SignMonthFlag = false;
		this.SignWeekFlag = false;
		this.curSignState = false;
		this.completeState = false;
		this.dailyBuy.Clear();
		this.investPack.Clear();
		this.levelPack.Clear();
		this.daily_actives.Clear();
		this.dailyRewards.Clear();
		this.Retrieve_Dic.Clear();
		this.BigPackDic.Clear();
		this.levelrewardDic.Clear();
		this.VipInfo = null;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x00066A24 File Offset: 0x00064C24
	public void SyncVipInfo(ret_require_vip_info.request request)
	{
		if (request.HasVip)
		{
			this.VipInfo = request.vip;
		}
		this.UpdateTips();
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00066A44 File Offset: 0x00064C44
	public void SyncVipInfo(ret_require_vip_reward.request request)
	{
		if (request.HasVip)
		{
			this.VipInfo = request.vip;
		}
		this.UpdateTips();
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x00066A64 File Offset: 0x00064C64
	public void UpdateVipState(string id)
	{
		if (this.VipInfo != null && this.VipInfo.id.Equals(id))
		{
			this.VipInfo.state = 1L;
		}
		this.UpdateTips();
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x00066AA8 File Offset: 0x00064CA8
	public bool HaveVipTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_VIP) && (this.VipInfo != null && this.VipInfo.state == 0L);
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x00066AF0 File Offset: 0x00064CF0
	public void SyncLevelReward(ret_level_reward.request request)
	{
		if (request.HasLevel_reward)
		{
			this.levelrewardDic = request.level_reward;
		}
		this.UpdateTips();
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x00066B10 File Offset: 0x00064D10
	public void SyncLevelReward(get_level_reward.request request)
	{
		if (request.HasLevel_reward)
		{
			this.levelrewardDic = request.level_reward;
		}
		this.UpdateTips();
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x00066B30 File Offset: 0x00064D30
	public bool IsHaveLevelReward()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_LEVEL))
		{
			return false;
		}
		LevelRewardData levelRewardData = null;
		level_reward level_reward = null;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		List<LevelRewardData> levelRewardDataList = DataManager.GetLevelRewardDataList();
		levelRewardDataList.Sort(delegate(LevelRewardData x, LevelRewardData y)
		{
			if (x.ID.Length == y.ID.Length)
			{
				return x.ID.CompareTo(y.ID);
			}
			return x.ID.Length - y.ID.Length;
		});
		for (int i = 0; i < levelRewardDataList.Count; i++)
		{
			if (level >= levelRewardDataList[i].StartLv && level <= levelRewardDataList[i].EndLv)
			{
				levelRewardData = levelRewardDataList[i];
				if (this.levelrewardDic.ContainsKey(levelRewardData.ID))
				{
					level_reward = this.levelrewardDic[levelRewardData.ID];
				}
			}
		}
		if (levelRewardData != null && level_reward != null)
		{
			if (level >= levelRewardData.TargetLevel1 && (level_reward.state & 1L) == 0L)
			{
				return true;
			}
			if (level >= levelRewardData.TargetLevel2 && (level_reward.state & 2L) == 0L)
			{
				return true;
			}
			if (level >= levelRewardData.TargetLevel3 && (level_reward.state & 4L) == 0L)
			{
				return true;
			}
			if (level >= levelRewardData.TargetLevel && (level_reward.state & 8L) == 0L && (level_reward.state & 1L) != 0L && (level_reward.state & 2L) != 0L && (level_reward.state & 4L) != 0L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x00066CB0 File Offset: 0x00064EB0
	public void SetBigPack(ret_request_big_pack.request request)
	{
		this.BigPackDic = request.special_big_packs;
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x00066CC0 File Offset: 0x00064EC0
	public void UpdateBigPack(string id)
	{
		if (this.BigPackDic.ContainsKey(id))
		{
			this.BigPackDic[id].state = 1L;
		}
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x00066CF4 File Offset: 0x00064EF4
	public bool isHaveWeaponPack()
	{
		return this.BigPackDic.ContainsKey("4") && this.BigPackDic["4"].state == 0L;
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x00066D34 File Offset: 0x00064F34
	public void SetRetrieve(ret_request_retrieve_info.request request)
	{
		this.Retrieve_Dic = request.info;
		this.UpdateTips();
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x00066D48 File Offset: 0x00064F48
	public bool HaveRetrieveTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_RETRIEVE))
		{
			return false;
		}
		foreach (retrieve_info retrieve_info in this.Retrieve_Dic.Values)
		{
			RetrieveData retrieveDataBuyId = DataManager.GetRetrieveDataBuyId(retrieve_info.ID);
			if (retrieveDataBuyId != null)
			{
				if (!retrieveDataBuyId.isGuildDance)
				{
					if (retrieve_info.state == 0L)
					{
						return true;
					}
				}
				else if (retrieve_info.state == 0L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x00066E2C File Offset: 0x0006502C
	public void SetWeekFlag(ret_request_sign_week_info.request request)
	{
		this.SignWeekFlag = (!request.complete && request.cur_sign_state);
		this.curWeekSignDay = (int)request.cur_sign;
		this.curSignState = request.cur_sign_state;
		this.completeState = request.complete;
		this.UpdateTips();
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00066E80 File Offset: 0x00065080
	public void SetWeekFlag(ret_sign_week.request request)
	{
		this.SignWeekFlag = request.cur_sign_state;
		this.curWeekSignDay = (int)request.cur_sign;
		this.curSignState = request.cur_sign_state;
		if (this.curWeekSignDay == 7 && !this.curSignState)
		{
			this.completeState = true;
		}
		this.UpdateTips();
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x00066ED8 File Offset: 0x000650D8
	public int GetNextWeekRewardDay()
	{
		if (this.completeState)
		{
			return -1;
		}
		if (this.curWeekSignDay == 7 && !this.curSignState)
		{
			return -1;
		}
		if (this.curSignState)
		{
			return this.curWeekSignDay;
		}
		return this.curWeekSignDay + 1;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x00066F28 File Offset: 0x00065128
	public bool HaveWeekTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_7DAY) && this.SignWeekFlag;
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x00066F4C File Offset: 0x0006514C
	public void SetMonthFlag(ret_request_30_day_info.request request)
	{
		this.SignMonthFlag = (request.cur_sign_state || request.replenish_sign_state);
		this.UpdateTips();
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x00066F7C File Offset: 0x0006517C
	public void SetMonthFlag(ret_sign_30_day.request request)
	{
		this.SignMonthFlag = (request.cur_sign_state || request.replenish_sign_state);
		this.UpdateTips();
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x00066FAC File Offset: 0x000651AC
	public bool HaveMonthTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_CHECK) && this.SignMonthFlag;
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x00066FD0 File Offset: 0x000651D0
	public void InitDailyBuy(ret_request_daily_buy.request info)
	{
		this.dailyBuy = info.daily_buys;
		this.UpdateTips();
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x00066FE4 File Offset: 0x000651E4
	public void UpdateDailyBuy(string key)
	{
		if (this.dailyBuy.ContainsKey(key))
		{
			this.dailyBuy[key].state = 2L;
		}
		this.UpdateTips();
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0006701C File Offset: 0x0006521C
	public bool HaveDailyBuyTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_DAILY))
		{
			return false;
		}
		if (!LocalDataSaveManager.ShowDailyBuyTips)
		{
			return false;
		}
		foreach (daily_buy daily_buy in this.dailyBuy.Values)
		{
			if (daily_buy.state == 0L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x000670BC File Offset: 0x000652BC
	public void InitInvestPack(ret_request_invest_pack.request info)
	{
		this.investPack = info.invest_pack;
		this.UpdateTips();
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x000670D0 File Offset: 0x000652D0
	public void InitInvestPack(ret_buy_invest_pack.request info)
	{
		this.investPack = info.invest_pack;
		this.UpdateTips();
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x000670E4 File Offset: 0x000652E4
	public void UpdateInvestPack(string key)
	{
		if (this.investPack.ContainsKey(key))
		{
			this.investPack[key].state = 2L;
		}
		this.UpdateTips();
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0006711C File Offset: 0x0006531C
	public bool HaveInvestTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_INVEST))
		{
			return false;
		}
		foreach (invest_pack invest_pack in this.investPack.Values)
		{
			if (invest_pack.state == -1L)
			{
				return LocalDataSaveManager.ShowInvestTips;
			}
			if (invest_pack.state == 1L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x000671CC File Offset: 0x000653CC
	public void InitLevelPack(ret_request_level_pack.request info)
	{
		this.levelPack = info.level_pack;
		this.UpdateTips();
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x000671E0 File Offset: 0x000653E0
	public void UpdateLevelPack(string key)
	{
		if (this.levelPack.ContainsKey(key))
		{
			this.levelPack[key].state = 2L;
		}
		this.UpdateTips();
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00067218 File Offset: 0x00065418
	public bool HaveLevelTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_LEVEL))
		{
			return false;
		}
		foreach (level_pack level_pack in this.levelPack.Values)
		{
			if (level_pack.state == 1L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x000672B0 File Offset: 0x000654B0
	public List<LevelPackageData> GetUnGetLevelPack()
	{
		if (this.levelPack.Count > 0)
		{
			int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
			List<level_pack> list = new List<level_pack>(this.levelPack.Values);
			List<LevelPackageData> list2 = new List<LevelPackageData>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(DataManager.GetLevelPackageDataBuyId(list[i].ID));
			}
			list2.Sort((LevelPackageData x, LevelPackageData y) => x.LvTarget - y.LvTarget);
			for (int j = list2.Count - 1; j >= 0; j--)
			{
				if (list2[j].LvTarget <= level)
				{
					if (this.levelPack[list2[j].ID].state == 2L)
					{
						list2.RemoveAt(j);
					}
					else
					{
						this.levelPack[list2[j].ID].state = 1L;
					}
				}
				else
				{
					list2.RemoveAt(j);
				}
			}
			return list2;
		}
		return null;
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x000673D8 File Offset: 0x000655D8
	public void InitDailyRewards(ret_request_daily_active.request info)
	{
		if (info.HasDaily_actives)
		{
			this.daily_actives = info.daily_actives;
		}
		if (info.HasDaily_rewards)
		{
			this.dailyRewards = info.daily_rewards;
		}
		this.UpdateTips();
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x0006741C File Offset: 0x0006561C
	public void UpdateDailyReward(string key)
	{
		if (this.dailyRewards.ContainsKey(key))
		{
			this.dailyRewards[key].state = 2L;
		}
		this.UpdateTips();
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x00067454 File Offset: 0x00065654
	public bool HaveDailyActivityTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_ACTIVITY))
		{
			return false;
		}
		foreach (daily_reward daily_reward in this.dailyRewards.Values)
		{
			if (daily_reward.state == 1L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000674EC File Offset: 0x000656EC
	public bool isHaveWelfareTips()
	{
		return this.HaveWeekTips() || this.HaveDailyBuyTips() || this.HaveInvestTips() || this.IsHaveLevelReward() || this.HaveMonthTips() || this.HaveRetrieveTips();
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0006753C File Offset: 0x0006573C
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.isHaveWelfareTips(), GameDefine.TIPS_TYPE.WELFARE);
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.HaveDailyActivityTips(), GameDefine.TIPS_TYPE.DAILYACT);
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.HaveVipTips(), GameDefine.TIPS_TYPE.SHOP);
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x000675B4 File Offset: 0x000657B4
	public bool IsGetSignWeekCar()
	{
		return this.curWeekSignDay > 3 || (this.curWeekSignDay == 3 && !this.curSignState);
	}

	// Token: 0x040013BF RID: 5055
	private Dictionary<string, daily_buy> dailyBuy = new Dictionary<string, daily_buy>();

	// Token: 0x040013C0 RID: 5056
	private Dictionary<string, invest_pack> investPack = new Dictionary<string, invest_pack>();

	// Token: 0x040013C1 RID: 5057
	private Dictionary<string, level_pack> levelPack = new Dictionary<string, level_pack>();

	// Token: 0x040013C2 RID: 5058
	private Dictionary<string, daily_active> daily_actives = new Dictionary<string, daily_active>();

	// Token: 0x040013C3 RID: 5059
	private Dictionary<string, daily_reward> dailyRewards = new Dictionary<string, daily_reward>();

	// Token: 0x040013C4 RID: 5060
	private Dictionary<string, retrieve_info> Retrieve_Dic = new Dictionary<string, retrieve_info>();

	// Token: 0x040013C5 RID: 5061
	private Dictionary<string, special_big_pack> BigPackDic = new Dictionary<string, special_big_pack>();

	// Token: 0x040013C6 RID: 5062
	private Dictionary<string, level_reward> levelrewardDic = new Dictionary<string, level_reward>();

	// Token: 0x040013C7 RID: 5063
	private vip VipInfo;

	// Token: 0x040013C8 RID: 5064
	private bool SignMonthFlag;

	// Token: 0x040013C9 RID: 5065
	private bool SignWeekFlag;

	// Token: 0x040013CA RID: 5066
	private int curWeekSignDay = -1;

	// Token: 0x040013CB RID: 5067
	private bool curSignState;

	// Token: 0x040013CC RID: 5068
	private bool completeState;
}
