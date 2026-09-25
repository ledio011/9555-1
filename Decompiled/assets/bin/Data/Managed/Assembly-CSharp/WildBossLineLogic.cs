using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008FB RID: 2299
public class WildBossLineLogic : MonoBehaviour
{
	// Token: 0x17000F88 RID: 3976
	// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x00120A24 File Offset: 0x0011EC24
	public List<activity_info> CurActivityList
	{
		get
		{
			return this.curActivityList;
		}
	}

	// Token: 0x06003EB5 RID: 16053 RVA: 0x00120A2C File Offset: 0x0011EC2C
	public void ResetItem(List<activity_info> infoList, DelegateDefine.TwoIntParamDelegate clickFunc)
	{
		UnityVersionUtil.SetActiveRecursive(this.Sublineobj, false);
		this.curActivityList = infoList;
		this.onClickItem = clickFunc;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.isWarningflag = false;
		this.isLevelUnlock = false;
		if (this.curActivityList.Count > 0)
		{
			this.curBossIndex = 0;
			for (int i = 0; i < this.curActivityList.Count; i++)
			{
				WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.curActivityList[i].ID);
				if (this.CheckLevel(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax))
				{
					this.curBossIndex = i;
					break;
				}
			}
			this.enableLineFlag = 0;
			activity_info activity_info = infoList[this.curBossIndex];
			WildBossData wildBossDataByID2 = DataManager.GetWildBossDataByID(infoList[this.curBossIndex].ID);
			int num = (int)activity_info.CurNum;
			if (activity_info.State == 2L)
			{
				this.enableLineFlag = 2;
			}
			if (activity_info.State == 0L && playerCommonData.GetCurServerTime() < activity_info.Parm)
			{
				this.enableLineFlag = 2;
			}
			if (num <= 0 && playerCommonData.GetCurServerTime() > activity_info.time + 5400L)
			{
				this.enableLineFlag = 3;
			}
			if (!this.CheckLevel(wildBossDataByID2.LevelMin, wildBossDataByID2.LevelMax))
			{
				this.enableLineFlag = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckIsLevelHeigh(wildBossDataByID2.LevelMin, wildBossDataByID2.LevelMax);
			}
			List<TimeSpan> localShowTime = TimeTools.GetLocalShowTime(wildBossDataByID2.StartTimes, playerCommonData.TimeOffset, (long)wildBossDataByID2.DurationTime, (int)infoList[this.curBossIndex].next, infoList[this.curBossIndex].State == 2L);
			if (localShowTime != null && localShowTime.Count == 2)
			{
				TimeSpan timeSpan = localShowTime[0];
				TimeSpan timeSpan2 = localShowTime[1];
				this.DurationLabel.text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString("#{101509}", new object[0]), string.Format("{0:D2}:{1:D2}", timeSpan.Hours, timeSpan.Minutes), string.Format("{0:D2}:{1:D2}", timeSpan2.Hours, timeSpan2.Minutes));
			}
			else
			{
				this.DurationLabel.text = string.Empty;
			}
			if (infoList[this.curBossIndex].State != 1L)
			{
				this.SetLabelWarining(this.DurationLabel, true, true);
			}
			else
			{
				this.SetLabelWarining(this.DurationLabel, false, true);
			}
			this.LimitLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, wildBossDataByID2.Limit);
			if (num <= 0 && playerCommonData.GetCurServerTime() > infoList[this.curBossIndex].time + 5400L)
			{
				this.isWarningflag = true;
			}
			WildBossData wildBossDataByID3 = DataManager.GetWildBossDataByID(this.curActivityList[0].ID);
			int levelMin = wildBossDataByID3.LevelMin;
			if (!this.CheckLevel(levelMin))
			{
				this.isWarningflag = true;
				this.LimitLabel.text = string.Format("Lv {0}", levelMin);
				this.IconSprite.color = Color.gray;
				this.IconLockFlag.enabled = true;
			}
			else
			{
				this.IconSprite.color = Color.white;
				this.IconLockFlag.enabled = false;
				this.isLevelUnlock = true;
			}
			this.IconSprite.spriteName = wildBossDataByID2.Icon;
			long type = infoList[this.curBossIndex].Type;
			if (type == 5L)
			{
				this.showName.text = StrDictionary.GetDictionaryString("#{101519}", new object[0]);
				if (wildBossDataByID2.PVP == 1)
				{
					this.ShowStateFlag(GameDefine.ACTIVITY_TYPE.WILD_BOSS, true);
				}
				else
				{
					this.ShowStateFlag(GameDefine.ACTIVITY_TYPE.WILD_BOSS, false);
				}
			}
		}
		if (this.isWarningflag)
		{
			this.SetLabelWarining(this.LimitLabel, true, false);
		}
		else
		{
			this.SetLabelWarining(this.LimitLabel, false, false);
		}
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x06003EB6 RID: 16054 RVA: 0x00120E78 File Offset: 0x0011F078
	private void ShowFirstUnlockState(bool isunlock)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (isunlock)
		{
			GameDefine.ACTIVITY_TYPE type = GameDefine.ACTIVITY_TYPE.WILD_BOSS;
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

	// Token: 0x06003EB7 RID: 16055 RVA: 0x00120ED8 File Offset: 0x0011F0D8
	private void CheckFirstClickState()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (UnityVersionUtil.IsActive(this.HighEffectObj))
		{
			GameDefine.ACTIVITY_TYPE type = GameDefine.ACTIVITY_TYPE.WILD_BOSS;
			playerCommonData.SetFirstClickState(GameDefine.GetActKey_FirstClick(type));
			NGUITools.SetActive(this.HighEffectObj, false);
		}
	}

	// Token: 0x06003EB8 RID: 16056 RVA: 0x00120F1C File Offset: 0x0011F11C
	private void ShowStateFlag(GameDefine.ACTIVITY_TYPE acttype, bool ispvp = false)
	{
		if (acttype == GameDefine.ACTIVITY_TYPE.ESCORT || acttype == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT || acttype == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE || ispvp)
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
		}
	}

	// Token: 0x06003EB9 RID: 16057 RVA: 0x0012105C File Offset: 0x0011F25C
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

	// Token: 0x06003EBA RID: 16058 RVA: 0x001210E4 File Offset: 0x0011F2E4
	public bool IsShowNextboss()
	{
		for (int i = 0; i < this.curActivityList.Count; i++)
		{
			WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.curActivityList[i].ID);
			if (this.CheckLevel(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax) && this.curActivityList[i].State == 2L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003EBB RID: 16059 RVA: 0x00121158 File Offset: 0x0011F358
	public bool CheckLevel(int minLevel, int maxlevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxlevel);
	}

	// Token: 0x06003EBC RID: 16060 RVA: 0x0012116C File Offset: 0x0011F36C
	public bool CheckLevel(int minLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel);
	}

	// Token: 0x06003EBD RID: 16061 RVA: 0x00121180 File Offset: 0x0011F380
	public void OpenFinish()
	{
	}

	// Token: 0x06003EBE RID: 16062 RVA: 0x00121184 File Offset: 0x0011F384
	public void OnClickItemBtn()
	{
		if (this.isLevelUnlock)
		{
			this.CheckFirstClickState();
		}
		this.OnClickSelectItemBtn(false);
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x001211A0 File Offset: 0x0011F3A0
	public void ClickTargetBtn()
	{
		for (int i = 0; i < this.curActivityList.Count; i++)
		{
			WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(this.curActivityList[i].ID);
			if (this.CheckLevel(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax))
			{
				break;
			}
		}
		this.OnClickItemBtn();
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x00121208 File Offset: 0x0011F408
	public void OnClickSelectItemBtn(bool isSelect)
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(this.curBossIndex, this.enableLineFlag);
		}
	}

	// Token: 0x06003EC1 RID: 16065 RVA: 0x00121238 File Offset: 0x0011F438
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

	// Token: 0x06003EC2 RID: 16066 RVA: 0x00121290 File Offset: 0x0011F490
	public void RefreshSelect(int curchoose)
	{
	}

	// Token: 0x06003EC3 RID: 16067 RVA: 0x00121294 File Offset: 0x0011F494
	public void OnScaleFinished()
	{
	}

	// Token: 0x04002A6B RID: 10859
	public UILabel showName;

	// Token: 0x04002A6C RID: 10860
	public UILabel DurationLabel;

	// Token: 0x04002A6D RID: 10861
	public UILabel LimitLabel;

	// Token: 0x04002A6E RID: 10862
	private int idxInDic;

	// Token: 0x04002A6F RID: 10863
	public UISprite IconSprite;

	// Token: 0x04002A70 RID: 10864
	public UISprite IconLockFlag;

	// Token: 0x04002A71 RID: 10865
	public UISprite SelectBkSprite;

	// Token: 0x04002A72 RID: 10866
	public GameObject Sublineobj;

	// Token: 0x04002A73 RID: 10867
	private List<activity_info> curActivityList = new List<activity_info>();

	// Token: 0x04002A74 RID: 10868
	public TweenScale SubLineRootScale;

	// Token: 0x04002A75 RID: 10869
	public TweenRotation SubFlagAnima;

	// Token: 0x04002A76 RID: 10870
	private bool mHasSubLine;

	// Token: 0x04002A77 RID: 10871
	public UIMyCenterOnChild CenterOnChild;

	// Token: 0x04002A78 RID: 10872
	public UIWidget PVPSP;

	// Token: 0x04002A79 RID: 10873
	public UIWidget PVESP;

	// Token: 0x04002A7A RID: 10874
	public UIWidget AloneSP;

	// Token: 0x04002A7B RID: 10875
	public UIWidget TeamSp;

	// Token: 0x04002A7C RID: 10876
	public UIWidget GuildSp;

	// Token: 0x04002A7D RID: 10877
	public UISprite DailyActFlag;

	// Token: 0x04002A7E RID: 10878
	public UILabel DailyActValLabel;

	// Token: 0x04002A7F RID: 10879
	private bool isWarningflag;

	// Token: 0x04002A80 RID: 10880
	private int curBossIndex;

	// Token: 0x04002A81 RID: 10881
	public DelegateDefine.TwoIntParamDelegate onClickItem;

	// Token: 0x04002A82 RID: 10882
	public int enableLineFlag;

	// Token: 0x04002A83 RID: 10883
	public GameObject HighEffectObj;

	// Token: 0x04002A84 RID: 10884
	private bool isLevelUnlock;
}
