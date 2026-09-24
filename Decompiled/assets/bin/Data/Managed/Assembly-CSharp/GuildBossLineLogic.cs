using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009D8 RID: 2520
public class GuildBossLineLogic : MonoBehaviour
{
	// Token: 0x0600479B RID: 18331 RVA: 0x0016D3F8 File Offset: 0x0016B5F8
	public bool IsGuildBossLine()
	{
		return this.curGuildBossList != null && this.curGuildBossList.Count > 0;
	}

	// Token: 0x0600479C RID: 18332 RVA: 0x0016D41C File Offset: 0x0016B61C
	public void ResetItem(List<guild_boss> infoList, DelegateDefine.ThirdIntParamDelegate clickFunc, int index)
	{
		this.mCurIndex = index;
		this.curGuildBossList = infoList;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.OnClickLine = clickFunc;
		this.isLevelUnlock = false;
		this.curActType = GameDefine.ACTIVITY_TYPE.GUILD_BOSS;
		if (this.curGuildBossList.Count > 0)
		{
			bool flag = false;
			this.mCurSubIndex = 0;
			for (int i = 0; i < this.curGuildBossList.Count; i++)
			{
				if (this.curGuildBossList[i].state == 1L)
				{
					flag = true;
					this.mCurSubIndex = i;
					break;
				}
			}
			this.curBossData = DataManager.GetGuildBossDataByID(infoList[this.mCurSubIndex].id);
			TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)this.curBossData.StartTime, playerCommonData.TimeOffset);
			TimeSpan localShowTime2 = TimeTools.GetLocalShowTime((long)(this.curBossData.StartTime + this.curBossData.DurationTime), playerCommonData.TimeOffset);
			this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime.Hours, localShowTime.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime2.Hours, localShowTime2.Minutes));
			if (!flag)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
			}
			else
			{
				this.SetLabelWarining(this.DurationLabel, false, true);
			}
			this.isWarningflag = false;
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				this.LimitLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{100735}", new object[0]), this.curBossData.LevelMin);
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.GuilLevel >= this.curBossData.LevelMin)
				{
					NGUITools.SetActive(this.LimitLabel.gameObject, false);
					this.SetLabelWarining(this.LimitLabel, false, false);
					this.IconSprite.color = Color.white;
					this.IconLockFlag.enabled = false;
					this.isLevelUnlock = true;
				}
				else
				{
					NGUITools.SetActive(this.LimitLabel.gameObject, true);
					this.SetLabelWarining(this.LimitLabel, true, false);
					this.IconSprite.color = Color.gray;
					this.IconLockFlag.enabled = true;
				}
			}
			else
			{
				this.LimitLabel.text = StrDictionary.GetDictionaryString("#{102087}", new object[0]);
				NGUITools.SetActive(this.LimitLabel.gameObject, true);
				this.SetLabelWarining(this.LimitLabel, true, false);
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			this.IconSprite.spriteName = this.curBossData.Icon;
			this.showName.text = StrDictionary.GetDictionaryString("#{100748}", new object[0]);
			this.ShowStateFlag(GameDefine.ACTIVITY_TYPE.GUILD_BOSS);
		}
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x0600479D RID: 18333 RVA: 0x0016D734 File Offset: 0x0016B934
	private void ShowStateFlag(GameDefine.ACTIVITY_TYPE acttype)
	{
		if (acttype == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE || acttype == GameDefine.ACTIVITY_TYPE.GUILD_DONMINE)
		{
			this.PVPSP.alpha = 1f;
			this.PVESP.alpha = 0f;
		}
		else
		{
			this.PVPSP.alpha = 0f;
			this.PVESP.alpha = 1f;
		}
		this.GuildSp.alpha = 1f;
		this.TeamSp.alpha = 0f;
		this.AloneSP.alpha = 0f;
		this.DailyActFlag.alpha = 0f;
		if (acttype != GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
		{
			if (acttype == GameDefine.ACTIVITY_TYPE.GUILD_DANCE)
			{
				this.ShowScore(20);
			}
		}
		else
		{
			this.ShowScore(19);
		}
	}

	// Token: 0x0600479E RID: 18334 RVA: 0x0016D808 File Offset: 0x0016BA08
	public void ResetItem(string id, DelegateDefine.ThirdIntParamDelegate clickFunc, int index, guild_battle_info curinfo)
	{
		this.curGuildBattleData = DataManager.GetGuildBattleDataById(id);
		this.isClose = true;
		this.mHasSubLine = false;
		this.OnClickLine = clickFunc;
		this.curActType = GameDefine.ACTIVITY_TYPE.GUILD_BATTLE;
		this.DurationLabel.text = string.Empty;
		this.LimitLabel.text = string.Empty;
		string text = string.Empty;
		int num = 0;
		int num2 = 0;
		this.isLevelUnlock = false;
		if (curinfo.state < 0L || curinfo.state >= 6L)
		{
			num = this.curGuildBattleData.Week;
			num2 = this.curGuildBattleData.StartTime;
			text = StrDictionary.GetDictionaryString("#{105076}", new object[0]);
		}
		else if (curinfo.state <= 1L)
		{
			num = this.curGuildBattleData.Week1;
			num2 = this.curGuildBattleData.StartTime1;
			text = StrDictionary.GetDictionaryString("#{105002}", new object[0]);
		}
		else if (curinfo.state <= 3L)
		{
			num = this.curGuildBattleData.Week2;
			num2 = this.curGuildBattleData.StartTime2;
			text = StrDictionary.GetDictionaryString("#{105003}", new object[0]);
		}
		else if (curinfo.state <= 5L)
		{
			num = this.curGuildBattleData.Week3;
			num2 = this.curGuildBattleData.StartTime3;
			text = StrDictionary.GetDictionaryString("#{105004}", new object[0]);
		}
		if (curinfo.state == -2L)
		{
			this.DurationLabel.text = StrDictionary.GetDictionaryString("#{101406}", new object[0]);
		}
		else
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)num2, playerCommonData.TimeOffset);
			int num3 = (num - 1 + TimeTools.GetOffsetDay((long)num2, playerCommonData.TimeOffset) + GameDefine.WEEK_NAME.Count) % GameDefine.WEEK_NAME.Count;
			this.DurationLabel.text = string.Format("{0}:{1} {2:d2}:{3:d2}", new object[]
			{
				text,
				StrDictionary.GetDictionaryString(GameDefine.WEEK_NAME[num3], new object[0]),
				localShowTime.Hours,
				localShowTime.Minutes
			});
		}
		if (curinfo.state == 0L || curinfo.state == 1L || curinfo.state == 3L || curinfo.state == 5L)
		{
			this.SetLabelWarining(this.DurationLabel, false, true);
		}
		else
		{
			this.SetLabelWarining(this.DurationLabel, true, true);
		}
		this.IconSprite.spriteName = this.curGuildBattleData.Icon;
		this.showName.text = this.curGuildBattleData.MName;
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NGUITools.SetActive(this.LimitLabel.gameObject, false);
			this.IconSprite.color = Color.white;
			this.IconLockFlag.enabled = false;
			this.isLevelUnlock = true;
		}
		else
		{
			this.LimitLabel.text = StrDictionary.GetDictionaryString("#{102087}", new object[0]);
			NGUITools.SetActive(this.LimitLabel.gameObject, true);
			this.SetLabelWarining(this.LimitLabel, true, false);
			this.IconSprite.color = Color.gray;
			this.IconLockFlag.enabled = true;
		}
		this.mCurIndex = index;
		this.mCurSubIndex = 0;
		this.ShowStateFlag(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE);
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x0600479F RID: 18335 RVA: 0x0016DB7C File Offset: 0x0016BD7C
	public void ResetItem(DelegateDefine.ThirdIntParamDelegate clickFunc, int index, dance_state_info curinfo)
	{
		this.CurGuildDanceData = DataManager.GetCityDanceDataById(curinfo.ID);
		this.isClose = true;
		this.mHasSubLine = false;
		this.OnClickLine = clickFunc;
		this.curActType = GameDefine.ACTIVITY_TYPE.GUILD_DANCE;
		this.DurationLabel.text = string.Empty;
		this.LimitLabel.text = string.Empty;
		this.isLevelUnlock = false;
		int num = 0;
		if (curinfo.HasParm)
		{
			num = (int)curinfo.parm - 1;
		}
		if (num < 0)
		{
			num = 0;
		}
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		TimeSpan localShowTime = TimeTools.GetLocalShowTime(this.CurGuildDanceData.StartTimes[num], playerCommonData.TimeOffset);
		TimeSpan localShowTime2 = TimeTools.GetLocalShowTime(this.CurGuildDanceData.StartTimes[num] + (long)this.CurGuildDanceData.DurationTime, playerCommonData.TimeOffset);
		this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime.Hours, localShowTime.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime2.Hours, localShowTime2.Minutes));
		if (curinfo.state == 1L)
		{
			this.SetLabelWarining(this.DurationLabel, false, true);
		}
		else
		{
			this.SetLabelWarining(this.DurationLabel, true, true);
		}
		this.IconSprite.spriteName = this.CurGuildDanceData.Icon;
		this.showName.text = StrDictionary.GetDictionaryString(this.CurGuildDanceData.Name, new object[0]);
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NGUITools.SetActive(this.LimitLabel.gameObject, false);
			this.IconSprite.color = Color.white;
			this.IconLockFlag.enabled = false;
			this.isLevelUnlock = true;
		}
		else
		{
			this.LimitLabel.text = StrDictionary.GetDictionaryString("#{102087}", new object[0]);
			NGUITools.SetActive(this.LimitLabel.gameObject, true);
			this.SetLabelWarining(this.LimitLabel, true, false);
			this.IconSprite.color = Color.gray;
			this.IconLockFlag.enabled = true;
		}
		this.mCurIndex = index;
		this.mCurSubIndex = 0;
		this.ShowStateFlag(GameDefine.ACTIVITY_TYPE.GUILD_DANCE);
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x060047A0 RID: 18336 RVA: 0x0016DDDC File Offset: 0x0016BFDC
	public void ResetItem(DelegateDefine.ThirdIntParamDelegate clickFunc, int index, List<guild_map_info> curinfolist)
	{
		this.GuildCityList = curinfolist;
		guild_map_info guild_map_info = this.GuildCityList[0];
		GuildCaptureData guildCaptureDataByID = DataManager.GetGuildCaptureDataByID(this.GuildCityList[0].id);
		this.isClose = true;
		this.mHasSubLine = false;
		this.OnClickLine = clickFunc;
		this.curActType = GameDefine.ACTIVITY_TYPE.GUILD_DONMINE;
		this.DurationLabel.text = string.Empty;
		this.LimitLabel.text = string.Empty;
		this.isLevelUnlock = false;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)guildCaptureDataByID.StartTime, playerCommonData.TimeOffset);
		int num = (guildCaptureDataByID.Week - 1 + TimeTools.GetOffsetDay((long)guildCaptureDataByID.StartTime, playerCommonData.TimeOffset) + GameDefine.WEEK_NAME.Count) % GameDefine.WEEK_NAME.Count;
		TimeSpan localShowTime2 = TimeTools.GetLocalShowTime((long)(guildCaptureDataByID.StartTime + guildCaptureDataByID.DurationTime), playerCommonData.TimeOffset);
		this.DurationLabel.text = string.Format("{0} {1:d2}:{2:d2}-{3:d2}:{4:d2}", new object[]
		{
			StrDictionary.GetDictionaryString(GameDefine.WEEK_NAME[num], new object[0]),
			localShowTime.Hours,
			localShowTime.Minutes,
			localShowTime2.Hours,
			localShowTime2.Minutes
		});
		if (guild_map_info.state == 1L)
		{
			this.SetLabelWarining(this.DurationLabel, false, true);
		}
		else
		{
			this.SetLabelWarining(this.DurationLabel, true, true);
		}
		this.IconSprite.spriteName = guildCaptureDataByID.Icon;
		this.showName.text = StrDictionary.GetDictionaryString(guildCaptureDataByID.Name, new object[0]);
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NGUITools.SetActive(this.LimitLabel.gameObject, false);
			this.IconSprite.color = Color.white;
			this.IconLockFlag.enabled = false;
			this.isLevelUnlock = true;
		}
		else
		{
			this.LimitLabel.text = StrDictionary.GetDictionaryString("#{102087}", new object[0]);
			NGUITools.SetActive(this.LimitLabel.gameObject, true);
			this.SetLabelWarining(this.LimitLabel, true, false);
			this.IconSprite.color = Color.gray;
			this.IconLockFlag.enabled = true;
		}
		this.mCurIndex = index;
		this.mCurSubIndex = 0;
		this.ShowStateFlag(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE);
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x060047A1 RID: 18337 RVA: 0x0016E058 File Offset: 0x0016C258
	private void ShowFirstUnlockState(bool isunlock)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (isunlock)
		{
			GameDefine.ACTIVITY_TYPE type = this.curActType;
			if (playerCommonData.CheckFirstClickState(GameDefine.GetActKey_FirstClick(type)))
			{
				NGUITools.SetActive(this.HighEffectObj, true);
			}
			else
			{
				NGUITools.SetActive(this.HighEffectObj, false);
			}
		}
		else
		{
			NGUITools.SetActive(this.HighEffectObj, false);
		}
	}

	// Token: 0x060047A2 RID: 18338 RVA: 0x0016E0BC File Offset: 0x0016C2BC
	private void CheckFirstClickState()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (UnityVersionUtil.IsActive(this.HighEffectObj))
		{
			GameDefine.ACTIVITY_TYPE type = this.curActType;
			playerCommonData.SetFirstClickState(GameDefine.GetActKey_FirstClick(type));
			NGUITools.SetActive(this.HighEffectObj, false);
		}
	}

	// Token: 0x060047A3 RID: 18339 RVA: 0x0016E104 File Offset: 0x0016C304
	public void OnClickItemBtn()
	{
		if (this.isLevelUnlock)
		{
			this.CheckFirstClickState();
		}
		if (this.OnClickLine != null)
		{
			this.OnClickLine(this.mCurIndex, this.mCurSubIndex, 0);
		}
	}

	// Token: 0x060047A4 RID: 18340 RVA: 0x0016E148 File Offset: 0x0016C348
	private void SetLabelWarining(UILabel label, bool needWarning, bool isLowWarning = false)
	{
		if (needWarning)
		{
			if (isLowWarning)
			{
				label.color = new Color(0.537f, 0.537f, 0.537f, 1f);
			}
			else
			{
				label.color = Color.red;
			}
		}
		else
		{
			label.color = Color.white;
		}
	}

	// Token: 0x060047A5 RID: 18341 RVA: 0x0016E1A0 File Offset: 0x0016C3A0
	public void RefreshSelect(int curchoose, int curSubChoose)
	{
		if (this.mCurIndex != curchoose)
		{
			this.selectbtnbg.spriteName = "CZ_huaDongBG";
		}
		else
		{
			this.selectbtnbg.spriteName = "CZ_huaDongBG_1";
		}
	}

	// Token: 0x060047A6 RID: 18342 RVA: 0x0016E1D4 File Offset: 0x0016C3D4
	private void ShowScore(int type)
	{
		this.DailyActFlag.alpha = 0f;
		List<DailyActiveData> dailyActiveDataList = DataManager.GetDailyActiveDataList();
		for (int i = 0; i < dailyActiveDataList.Count; i++)
		{
			if (dailyActiveDataList[i].Type == type)
			{
				int score = dailyActiveDataList[i].Score;
				this.DailyActFlag.alpha = 1f;
				this.DailyActValLabel.text = string.Format("{0}", score);
				return;
			}
		}
	}

	// Token: 0x040034D4 RID: 13524
	public UILabel showName;

	// Token: 0x040034D5 RID: 13525
	public UILabel DurationLabel;

	// Token: 0x040034D6 RID: 13526
	public UILabel LimitLabel;

	// Token: 0x040034D7 RID: 13527
	private int mCurIndex;

	// Token: 0x040034D8 RID: 13528
	private int mCurSubIndex;

	// Token: 0x040034D9 RID: 13529
	public UISprite selectbtnbg;

	// Token: 0x040034DA RID: 13530
	public UISprite IconSprite;

	// Token: 0x040034DB RID: 13531
	public UISprite IconLockFlag;

	// Token: 0x040034DC RID: 13532
	private GuildBossData curBossData;

	// Token: 0x040034DD RID: 13533
	private List<guild_boss> curGuildBossList;

	// Token: 0x040034DE RID: 13534
	private GuildBattleData curGuildBattleData;

	// Token: 0x040034DF RID: 13535
	private CityDanceData CurGuildDanceData;

	// Token: 0x040034E0 RID: 13536
	private bool mHasSubLine;

	// Token: 0x040034E1 RID: 13537
	private bool isClose;

	// Token: 0x040034E2 RID: 13538
	private DelegateDefine.ThirdIntParamDelegate OnClickLine;

	// Token: 0x040034E3 RID: 13539
	public UIWidget PVPSP;

	// Token: 0x040034E4 RID: 13540
	public UIWidget PVESP;

	// Token: 0x040034E5 RID: 13541
	public UIWidget AloneSP;

	// Token: 0x040034E6 RID: 13542
	public UIWidget TeamSp;

	// Token: 0x040034E7 RID: 13543
	public UIWidget GuildSp;

	// Token: 0x040034E8 RID: 13544
	public UISprite DailyActFlag;

	// Token: 0x040034E9 RID: 13545
	public UILabel DailyActValLabel;

	// Token: 0x040034EA RID: 13546
	private bool isWarningflag;

	// Token: 0x040034EB RID: 13547
	public GameObject HighEffectObj;

	// Token: 0x040034EC RID: 13548
	private bool isLevelUnlock;

	// Token: 0x040034ED RID: 13549
	private List<guild_map_info> GuildCityList;

	// Token: 0x040034EE RID: 13550
	private GameDefine.ACTIVITY_TYPE curActType = GameDefine.ACTIVITY_TYPE.INVALID;
}
