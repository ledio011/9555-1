using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008F1 RID: 2289
public class ActivityItemLogic : MonoBehaviour
{
	// Token: 0x06003DF4 RID: 15860 RVA: 0x00118178 File Offset: 0x00116378
	public bool CheckLevel(int minLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel);
	}

	// Token: 0x06003DF5 RID: 15861 RVA: 0x0011818C File Offset: 0x0011638C
	public void UpdateItem(int index, activity_info info, int curChoose)
	{
		this.enableLineFlag = 0;
		this.curIndex = index;
		this.curActivityInfo = info;
		int num = (int)this.curActivityInfo.CurNum;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.isWarningflag = false;
		this.isLevelUnlock = false;
		if (this.curActivityInfo.Type == 1L || this.curActivityInfo.Type == 2L)
		{
			EscortData escortDataById = DataManager.GetEscortDataById(info.ID);
			this.IconSprite.spriteName = escortDataById.Icon;
			TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)escortDataById.StartTime, playerCommonData.TimeOffset);
			TimeSpan localShowTime2 = TimeTools.GetLocalShowTime((long)(escortDataById.StartTime + escortDataById.DurationTime), playerCommonData.TimeOffset);
			this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101558}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime.Hours, localShowTime.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime2.Hours, localShowTime2.Minutes));
			if (this.curActivityInfo.State == 0L && playerCommonData.GetCurServerTime() < this.curActivityInfo.Parm)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
				this.enableLineFlag = 2;
			}
			else
			{
				this.SetLabelWarining(this.DurationLabel, false, true);
			}
			this.LimitLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, escortDataById.MaxPlayNum);
			if (num <= 0)
			{
				this.isWarningflag = true;
				this.enableLineFlag = 3;
			}
			this.NameLabel.text = StrDictionary.GetDictionaryString(escortDataById.Name, new object[0]);
			this.UnlockLevel = escortDataById.UnlockLevel;
			if (!this.CheckLevel(escortDataById.UnlockLevel))
			{
				this.LimitLabel.text = string.Format("Lv {0}", this.UnlockLevel);
				this.enableLineFlag = 1;
				this.isWarningflag = true;
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			else
			{
				this.IconSprite.color = Color.white;
				this.IconLockFlag.enabled = false;
				this.isLevelUnlock = true;
			}
		}
		else if (this.curActivityInfo.Type == 3L)
		{
			CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(info.ID);
			this.NameLabel.text = StrDictionary.GetDictionaryString(cityDanceDataById.Name, new object[0]);
			TimeSpan localShowTime3 = TimeTools.GetLocalShowTime((long)cityDanceDataById.StartTime.get_Chars(0), playerCommonData.TimeOffset);
			TimeSpan localShowTime4 = TimeTools.GetLocalShowTime((long)((int)cityDanceDataById.StartTime.get_Chars(0) + cityDanceDataById.DurationTime), playerCommonData.TimeOffset);
			this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime3.Hours, localShowTime3.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime4.Hours, localShowTime4.Minutes));
			if (this.curActivityInfo.State == 0L && playerCommonData.GetCurServerTime() < this.curActivityInfo.Parm)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
				this.enableLineFlag = 2;
			}
			else
			{
				this.SetLabelWarining(this.DurationLabel, false, true);
			}
			this.IconSprite.spriteName = cityDanceDataById.Icon;
			this.UnlockLevel = cityDanceDataById.UnlockLevel;
			this.LimitLabel.text = string.Format("Lv {0}", this.UnlockLevel);
			if (!this.CheckLevel(cityDanceDataById.UnlockLevel))
			{
				this.isWarningflag = true;
				this.enableLineFlag = 1;
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			else
			{
				this.IconSprite.color = Color.white;
				this.IconLockFlag.enabled = false;
				this.isLevelUnlock = true;
			}
		}
		else if (this.curActivityInfo.Type == 4L)
		{
			BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(info.ID);
			this.NameLabel.text = StrDictionary.GetDictionaryString(barFightCopyDataByID.Name, new object[0]);
			TimeSpan localShowTime5 = TimeTools.GetLocalShowTime((long)(barFightCopyDataByID.StartTime + barFightCopyDataByID.DurationTime), playerCommonData.TimeOffset);
			TimeSpan localShowTime6 = TimeTools.GetLocalShowTime((long)(barFightCopyDataByID.StartTime + barFightCopyDataByID.DurationTime + barFightCopyDataByID.WaitTime), playerCommonData.TimeOffset);
			this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime5.Hours, localShowTime5.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime6.Hours, localShowTime6.Minutes));
			TimeSpan localShowTime7 = TimeTools.GetLocalShowTime((long)barFightCopyDataByID.StartTime, playerCommonData.TimeOffset);
			TimeSpan localShowTime8 = TimeTools.GetLocalShowTime((long)(barFightCopyDataByID.StartTime + barFightCopyDataByID.DurationTime), playerCommonData.TimeOffset);
			if (this.curActivityInfo.State == 0L && playerCommonData.GetCurServerTime() < this.curActivityInfo.Parm)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
				this.enableLineFlag = 2;
			}
			else if (this.curActivityInfo.State == 1L)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
				this.enableLineFlag = 5;
			}
			else if (this.curActivityInfo.State == 2L)
			{
				this.SetLabelWarining(this.DurationLabel, false, true);
			}
			this.IconSprite.spriteName = barFightCopyDataByID.Icon;
			this.UnlockLevel = barFightCopyDataByID.UnlockLevel;
			this.LimitLabel.text = string.Format("Lv {0}", this.UnlockLevel);
			if (!this.CheckLevel(barFightCopyDataByID.UnlockLevel))
			{
				this.isWarningflag = true;
				this.enableLineFlag = 1;
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			else
			{
				this.IconSprite.color = Color.white;
				this.IconLockFlag.enabled = false;
				this.isLevelUnlock = true;
			}
		}
		else if (this.curActivityInfo.Type == 7L)
		{
			SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(info.ID);
			this.NameLabel.text = StrDictionary.GetDictionaryString(surviveBattleDataById.Name, new object[0]);
			List<TimeSpan> localShowTime9 = TimeTools.GetLocalShowTime(surviveBattleDataById.StartTimes, playerCommonData.TimeOffset, (long)surviveBattleDataById.DurationTime, (int)info.next, false);
			if (localShowTime9 != null && localShowTime9.Count == 2)
			{
				TimeSpan timeSpan = localShowTime9[0];
				TimeSpan timeSpan2 = localShowTime9[1];
				this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", timeSpan.Hours, timeSpan.Minutes), string.Format("{0:D2}:{1:D2}", timeSpan2.Hours, timeSpan2.Minutes));
			}
			else
			{
				this.DurationLabel.text = string.Empty;
			}
			if (this.curActivityInfo.State == 0L && playerCommonData.GetCurServerTime() < this.curActivityInfo.Parm)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
				this.enableLineFlag = 2;
			}
			else
			{
				this.SetLabelWarining(this.DurationLabel, false, true);
			}
			this.IconSprite.spriteName = surviveBattleDataById.Icon;
			this.UnlockLevel = surviveBattleDataById.UnlockLevel;
			this.LimitLabel.text = string.Format("Lv {0}", this.UnlockLevel);
			if (!this.CheckLevel(surviveBattleDataById.UnlockLevel))
			{
				this.isWarningflag = true;
				this.enableLineFlag = 1;
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			else
			{
				this.IconSprite.color = Color.white;
				this.IconLockFlag.enabled = false;
				this.isLevelUnlock = true;
			}
		}
		else if (this.curActivityInfo.Type == 8L)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(info.ID);
			this.NameLabel.text = StrDictionary.GetDictionaryString(sexMiniDataById.Name, new object[0]);
			this.DurationLabel.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			this.SetLabelWarining(this.DurationLabel, false, true);
			this.IconSprite.spriteName = sexMiniDataById.Icon;
			this.UnlockLevel = sexMiniDataById.UnlockLevel;
			this.LimitLabel.text = string.Format("Lv {0}", this.UnlockLevel);
			if (!this.CheckLevel(sexMiniDataById.UnlockLevel))
			{
				this.isWarningflag = true;
				this.enableLineFlag = 1;
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			else
			{
				this.IconSprite.color = Color.white;
				this.IconLockFlag.enabled = false;
				this.isLevelUnlock = true;
			}
		}
		this.ShowStateFlag((GameDefine.ACTIVITY_TYPE)this.curActivityInfo.Type);
		this.ShowFirstUnlockState(this.isLevelUnlock);
		if (this.isWarningflag)
		{
			this.SetLabelWarining(this.LimitLabel, true, false);
		}
		else
		{
			this.SetLabelWarining(this.LimitLabel, false, false);
		}
		this.RefreshSelect(curChoose);
	}

	// Token: 0x06003DF6 RID: 15862 RVA: 0x00118B7C File Offset: 0x00116D7C
	private void ShowFirstUnlockState(bool isunlock)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (isunlock)
		{
			GameDefine.ACTIVITY_TYPE type = (GameDefine.ACTIVITY_TYPE)this.curActivityInfo.Type;
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

	// Token: 0x06003DF7 RID: 15863 RVA: 0x00118BE8 File Offset: 0x00116DE8
	private void CheckFirstClickState()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (UnityVersionUtil.IsActive(this.HighEffectObj))
		{
			GameDefine.ACTIVITY_TYPE type = (GameDefine.ACTIVITY_TYPE)this.curActivityInfo.Type;
			playerCommonData.SetFirstClickState(GameDefine.GetActKey_FirstClick(type));
			NGUITools.SetActive(this.HighEffectObj, false);
		}
	}

	// Token: 0x06003DF8 RID: 15864 RVA: 0x00118C38 File Offset: 0x00116E38
	private void ShowStateFlag(GameDefine.ACTIVITY_TYPE acttype)
	{
		if (acttype == GameDefine.ACTIVITY_TYPE.ESCORT || acttype == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || acttype == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE)
		{
			this.PVPSP.alpha = 1f;
			this.PVESP.alpha = 0f;
		}
		else
		{
			this.PVPSP.alpha = 0f;
			this.PVESP.alpha = 1f;
		}
		this.GuildSp.alpha = 0f;
		if (acttype == GameDefine.ACTIVITY_TYPE.CITY_DANCE || acttype == GameDefine.ACTIVITY_TYPE.ESCORT || acttype == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || acttype == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE || acttype == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
		{
			this.TeamSp.alpha = 1f;
			this.AloneSP.alpha = 0f;
		}
		else
		{
			this.TeamSp.alpha = 0f;
			this.AloneSP.alpha = 1f;
		}
		this.DailyActFlag.alpha = 0f;
		switch (acttype)
		{
		case GameDefine.ACTIVITY_TYPE.ESCORT:
			this.ShowScore(14);
			break;
		case GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT:
			this.ShowScore(4);
			break;
		case GameDefine.ACTIVITY_TYPE.WILD_BOSS:
			this.ShowScore(6);
			break;
		case GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE:
			this.ShowScore(18);
			break;
		}
	}

	// Token: 0x06003DF9 RID: 15865 RVA: 0x00118D84 File Offset: 0x00116F84
	public void OnClikcItemBtn()
	{
		if (this.isLevelUnlock)
		{
			this.CheckFirstClickState();
		}
		if (this.onClickItem != null)
		{
			this.onClickItem(this.curIndex, this.enableLineFlag, this.UnlockLevel, false);
		}
	}

	// Token: 0x06003DFA RID: 15866 RVA: 0x00118DCC File Offset: 0x00116FCC
	public void MapClickItemBtn()
	{
		if (this.isLevelUnlock)
		{
			this.CheckFirstClickState();
		}
		if (this.onClickItem != null)
		{
			this.onClickItem(this.curIndex, this.enableLineFlag, this.UnlockLevel, true);
		}
	}

	// Token: 0x06003DFB RID: 15867 RVA: 0x00118E14 File Offset: 0x00117014
	public void RefreshSelect(int index)
	{
	}

	// Token: 0x06003DFC RID: 15868 RVA: 0x00118E18 File Offset: 0x00117018
	private void SetLabelWarining(UILabel label, bool needWarning, bool isLowWarningLimit = false)
	{
		if (needWarning)
		{
			if (isLowWarningLimit)
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

	// Token: 0x06003DFD RID: 15869 RVA: 0x00118E70 File Offset: 0x00117070
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

	// Token: 0x04002995 RID: 10645
	public UILabel NameLabel;

	// Token: 0x04002996 RID: 10646
	public UISprite IconSprite;

	// Token: 0x04002997 RID: 10647
	public UISprite IconLockFlag;

	// Token: 0x04002998 RID: 10648
	public UILabel LimitLabel;

	// Token: 0x04002999 RID: 10649
	public UILabel DurationLabel;

	// Token: 0x0400299A RID: 10650
	public UISprite SelectBkSprite;

	// Token: 0x0400299B RID: 10651
	private int enableLineFlag;

	// Token: 0x0400299C RID: 10652
	private int UnlockLevel;

	// Token: 0x0400299D RID: 10653
	public int curIndex = -1;

	// Token: 0x0400299E RID: 10654
	public ActivityItemLogic.ActivityItemDelegate onClickItem;

	// Token: 0x0400299F RID: 10655
	public activity_info curActivityInfo;

	// Token: 0x040029A0 RID: 10656
	public UIWidget PVPSP;

	// Token: 0x040029A1 RID: 10657
	public UIWidget PVESP;

	// Token: 0x040029A2 RID: 10658
	public UIWidget AloneSP;

	// Token: 0x040029A3 RID: 10659
	public UIWidget TeamSp;

	// Token: 0x040029A4 RID: 10660
	public UIWidget GuildSp;

	// Token: 0x040029A5 RID: 10661
	public UISprite DailyActFlag;

	// Token: 0x040029A6 RID: 10662
	public UILabel DailyActValLabel;

	// Token: 0x040029A7 RID: 10663
	private bool isWarningflag;

	// Token: 0x040029A8 RID: 10664
	public GameObject HighEffectObj;

	// Token: 0x040029A9 RID: 10665
	private bool isLevelUnlock;

	// Token: 0x02000AEB RID: 2795
	// (Invoke) Token: 0x06005035 RID: 20533
	public delegate void ActivityItemDelegate(int a, int b, int c, bool d);
}
