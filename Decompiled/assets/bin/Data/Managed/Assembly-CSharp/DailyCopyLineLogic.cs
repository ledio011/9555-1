using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008F4 RID: 2292
public class DailyCopyLineLogic : MonoBehaviour
{
	// Token: 0x17000F84 RID: 3972
	// (get) Token: 0x06003E37 RID: 15927 RVA: 0x0011B4C0 File Offset: 0x001196C0
	public string Key
	{
		get
		{
			return this.mKey;
		}
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x0011B4C8 File Offset: 0x001196C8
	public void ResetItem(copyscene_info info, DelegateDefine.StringGameObjectDelegate clickFunc, bool isfinal)
	{
		this.isFinal = isfinal;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mKey = info.ID;
		this.onClickItem = clickFunc;
		this.mCurCopyScene = DataManager.GetCopySceneDataById(info.ID);
		int num = (int)info.CurNum;
		this.isWarningflag = false;
		this.LimitLabel.enabled = true;
		this.isLevelUnlock = false;
		this.CurMapType = (MAPTYPE)this.mCurCopyScene.SubType;
		if (this.mCurCopyScene.IsSingleDance)
		{
			this.LimitLabel.text = string.Format("{0}  {1}", StrDictionary.GetDictionaryString("#{102018}", new object[0]), TimeTools.GetMinuteSecondStr(num));
		}
		else
		{
			this.LimitLabel.text = string.Format("{0}  {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, this.mCurCopyScene.MaxPlayNum);
		}
		if (num <= 0)
		{
			this.isWarningflag = true;
		}
		if (!this.CheckLevel(this.mCurCopyScene.MinLevel))
		{
			this.LimitLabel.text = string.Format("Lv {0}", this.mCurCopyScene.MinLevel);
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
		if (this.isWarningflag)
		{
			this.SetLabelWarining(this.LimitLabel, true, false);
		}
		else
		{
			this.SetLabelWarining(this.LimitLabel, false, false);
		}
		this.ShowDailyActInfo(this.mCurCopyScene);
		this.GuildSp.alpha = 0f;
		if (this.mCurCopyScene.IsTeamCopy)
		{
			this.MultiSp.alpha = 0f;
			this.AloneSP.alpha = 0f;
			this.TeamSp.alpha = 1f;
		}
		else if (this.mCurCopyScene.IsScuffleCopy)
		{
			this.MultiSp.alpha = 1f;
			this.AloneSP.alpha = 0f;
			this.TeamSp.alpha = 0f;
		}
		else
		{
			this.MultiSp.alpha = 0f;
			this.AloneSP.alpha = 1f;
			this.TeamSp.alpha = 0f;
		}
		if (this.mCurCopyScene.IsPVPFlag())
		{
			this.PVPSP.alpha = 1f;
			this.PVESP.alpha = 0f;
		}
		else
		{
			this.PVPSP.alpha = 0f;
			this.PVESP.alpha = 1f;
		}
		this.NameLabel.text = this.mCurCopyScene.MName;
		this.IconSprite.spriteName = this.mCurCopyScene.Icon;
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x0011B7E0 File Offset: 0x001199E0
	public void ResetItem(string key, string name, List<copyscene_info> infoList, DelegateDefine.StringGameObjectDelegate clickFunc, bool isfinal)
	{
		this.isFinal = isfinal;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.onClickItem = clickFunc;
		this.CurInfoList = infoList;
		this.isLevelUnlock = false;
		if (infoList.Count > 0)
		{
			int num = 0;
			for (int i = 0; i < infoList.Count; i++)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(infoList[i].ID);
				if (!this.CheckLevel(copySceneDataById.MinLevel))
				{
					break;
				}
				num = i;
			}
			copyscene_info copyscene_info = infoList[num];
			this.mKey = copyscene_info.ID;
			this.mCurCopyScene = DataManager.GetCopySceneDataById(copyscene_info.ID);
			this.CurMapType = (MAPTYPE)this.mCurCopyScene.SubType;
			this.GuildSp.alpha = 0f;
			if (this.mCurCopyScene.IsTeamCopy)
			{
				this.MultiSp.alpha = 0f;
				this.AloneSP.alpha = 0f;
				this.TeamSp.alpha = 1f;
			}
			else
			{
				this.MultiSp.alpha = 0f;
				this.AloneSP.alpha = 1f;
				this.TeamSp.alpha = 0f;
			}
			if (this.mCurCopyScene.IsPVPFlag())
			{
				this.PVPSP.alpha = 1f;
				this.PVESP.alpha = 0f;
			}
			else
			{
				this.PVPSP.alpha = 0f;
				this.PVESP.alpha = 1f;
			}
			this.ShowDailyActInfo(this.mCurCopyScene);
			if (this.mCurCopyScene.SubType == 16 || this.mCurCopyScene.SubType == 12)
			{
				int num2 = (int)copyscene_info.CurNum;
				this.IconSprite.spriteName = this.mCurCopyScene.Icon;
				this.isWarningflag = false;
				this.LimitLabel.enabled = true;
				this.LimitLabel.text = string.Format("{0}  {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num2, this.mCurCopyScene.MaxPlayNum);
				if (num2 <= 0)
				{
					this.isWarningflag = true;
				}
				if (!this.CheckLevel(this.mCurCopyScene.MinLevel))
				{
					this.isWarningflag = true;
					this.LimitLabel.text = string.Format("Lv {0}", this.mCurCopyScene.MinLevel);
					this.IconSprite.color = Color.gray;
					this.IconLockFlag.enabled = true;
				}
				else
				{
					this.IconSprite.color = Color.white;
					this.IconLockFlag.enabled = false;
					this.isLevelUnlock = true;
				}
				if (this.isWarningflag)
				{
					this.SetLabelWarining(this.LimitLabel, true, false);
				}
				else
				{
					this.SetLabelWarining(this.LimitLabel, false, false);
				}
			}
			else if (this.mCurCopyScene.SubType == 1)
			{
				this.IconSprite.spriteName = this.mCurCopyScene.Icon;
				this.LimitLabel.enabled = false;
			}
		}
		else
		{
			this.LimitLabel.enabled = false;
		}
		this.NameLabel.text = StrDictionary.GetDictionaryString(name, new object[0]);
		this.SelectBkSprite.spriteName = "CZ_huaDongBG";
		this.ShowFirstUnlockState(this.isLevelUnlock);
	}

	// Token: 0x06003E3A RID: 15930 RVA: 0x0011BB54 File Offset: 0x00119D54
	private void ShowFirstUnlockState(bool isunlock)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (isunlock)
		{
			MAPTYPE curMapType = this.CurMapType;
			if (playerCommonData.CheckFirstClickState(GameDefine.GetCopyKey_FirstClick(curMapType)))
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

	// Token: 0x06003E3B RID: 15931 RVA: 0x0011BBB8 File Offset: 0x00119DB8
	private void CheckFirstClickState()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (UnityVersionUtil.IsActive(this.HighEffectObj))
		{
			MAPTYPE curMapType = this.CurMapType;
			playerCommonData.SetFirstClickState(GameDefine.GetCopyKey_FirstClick(curMapType));
			NGUITools.SetActive(this.HighEffectObj, false);
		}
	}

	// Token: 0x06003E3C RID: 15932 RVA: 0x0011BC00 File Offset: 0x00119E00
	public void ShowDailyActInfo(CopySceneData curdata)
	{
		this.DailyActFlag.alpha = 0f;
		if (curdata == null)
		{
			return;
		}
		int subType = curdata.SubType;
		switch (subType)
		{
		case 11:
			this.ShowScore(2);
			break;
		case 12:
			this.ShowScore(1);
			break;
		default:
			if (subType != 7)
			{
				if (subType != 20)
				{
					if (subType == 26)
					{
						this.ShowScore(17);
					}
				}
				else
				{
					this.ShowScore(16);
				}
			}
			else
			{
				this.ShowScore(3);
			}
			break;
		case 16:
			this.ShowScore(0);
			break;
		}
	}

	// Token: 0x06003E3D RID: 15933 RVA: 0x0011BCB4 File Offset: 0x00119EB4
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

	// Token: 0x06003E3E RID: 15934 RVA: 0x0011BD3C File Offset: 0x00119F3C
	public bool CheckLevel(int minLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel);
	}

	// Token: 0x06003E3F RID: 15935 RVA: 0x0011BD50 File Offset: 0x00119F50
	public void RefershParentLine(copyscene_info datainfo)
	{
	}

	// Token: 0x06003E40 RID: 15936 RVA: 0x0011BD54 File Offset: 0x00119F54
	public void UpdateLineInfo(copyscene_info datainfo)
	{
		if (datainfo.ID.Equals(this.mKey))
		{
			int num = (int)datainfo.CurNum;
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(datainfo.ID);
			this.LimitLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), num, copySceneDataById.MaxPlayNum);
			if (num <= 0)
			{
				this.SetLabelWarining(this.LimitLabel, true, true);
			}
			else
			{
				this.SetLabelWarining(this.LimitLabel, false, true);
			}
		}
	}

	// Token: 0x06003E41 RID: 15937 RVA: 0x0011BDEC File Offset: 0x00119FEC
	public void ClickTargetBtn(string actid)
	{
	}

	// Token: 0x06003E42 RID: 15938 RVA: 0x0011BDF0 File Offset: 0x00119FF0
	public void OnClikcItemBtn()
	{
		this.OnClickSelectItemBtn(null);
	}

	// Token: 0x06003E43 RID: 15939 RVA: 0x0011BDFC File Offset: 0x00119FFC
	public void OnClickSelectItemBtn(string actid)
	{
		if (this.isLevelUnlock)
		{
			this.CheckFirstClickState();
		}
		if (this.onClickItem != null)
		{
			this.onClickItem(this.mKey, base.gameObject);
		}
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x0011BE34 File Offset: 0x0011A034
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

	// Token: 0x06003E45 RID: 15941 RVA: 0x0011BE8C File Offset: 0x0011A08C
	public void RefreshLineSelect(string key)
	{
	}

	// Token: 0x06003E46 RID: 15942 RVA: 0x0011BE90 File Offset: 0x0011A090
	public void SetTowerInfo(tower_info curtowerinfo, DelegateDefine.StringGameObjectDelegate clickFunc)
	{
		this.onClickItem = clickFunc;
		this.mKey = "tower";
		this.GuildSp.alpha = 0f;
		this.MultiSp.alpha = 0f;
		this.TeamSp.alpha = 0f;
		this.AloneSP.alpha = 1f;
		this.PVESP.alpha = 1f;
		this.PVPSP.alpha = 0f;
		this.ShowScore(5);
		this.NameLabel.text = StrDictionary.GetDictionaryString("#{101538}", new object[0]);
		this.IconSprite.spriteName = GameDefine.TowerIconName;
		int condition = DataManager.GetFunctionDataById(4003.ToString()).Condition;
		this.isWarningflag = false;
		this.LimitLabel.enabled = true;
		this.LimitLabel.text = string.Format("{0}:{1}/1", StrDictionary.GetDictionaryString("#{101528}", new object[0]), curtowerinfo.times);
		this.isLevelUnlock = false;
		this.CurMapType = MAPTYPE.SEX_GAME;
		if (!this.CheckLevel(condition))
		{
			this.LimitLabel.text = string.Format("Lv {0}", condition);
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

	// Token: 0x06003E47 RID: 15943 RVA: 0x0011C058 File Offset: 0x0011A258
	public void SetSexGameInfo(SexMiniData CurSexInfo, DelegateDefine.StringGameObjectDelegate clickFunc)
	{
		this.onClickItem = clickFunc;
		this.mKey = "Sex";
		this.GuildSp.alpha = 0f;
		this.MultiSp.alpha = 0f;
		this.TeamSp.alpha = 0f;
		this.AloneSP.alpha = 1f;
		this.PVESP.alpha = 1f;
		this.PVPSP.alpha = 0f;
		this.ShowDailyActInfo(null);
		this.NameLabel.text = StrDictionary.GetDictionaryString(CurSexInfo.Name, new object[0]);
		this.IconSprite.spriteName = CurSexInfo.Icon;
		int unlockLevel = CurSexInfo.UnlockLevel;
		this.isWarningflag = false;
		this.LimitLabel.enabled = true;
		this.LimitLabel.text = string.Empty;
		this.isLevelUnlock = false;
		this.CurMapType = MAPTYPE.CLAMBING_TOWER;
		if (!this.CheckLevel(unlockLevel))
		{
			this.LimitLabel.text = string.Format("Lv {0}", unlockLevel);
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

	// Token: 0x040029D4 RID: 10708
	public UILabel NameLabel;

	// Token: 0x040029D5 RID: 10709
	public UISprite IconSprite;

	// Token: 0x040029D6 RID: 10710
	public UISprite IconLockFlag;

	// Token: 0x040029D7 RID: 10711
	public UILabel LimitLabel;

	// Token: 0x040029D8 RID: 10712
	public UISprite SelectBkSprite;

	// Token: 0x040029D9 RID: 10713
	public UIButtonColor LineBtnColor;

	// Token: 0x040029DA RID: 10714
	public int curIndex = -1;

	// Token: 0x040029DB RID: 10715
	private DelegateDefine.StringGameObjectDelegate onClickItem;

	// Token: 0x040029DC RID: 10716
	public UIGrid SubLineRootGride;

	// Token: 0x040029DD RID: 10717
	public UIMyCenterOnChild CenterOnChild;

	// Token: 0x040029DE RID: 10718
	private string mKey = string.Empty;

	// Token: 0x040029DF RID: 10719
	private bool isFinal;

	// Token: 0x040029E0 RID: 10720
	private List<copyscene_info> CurInfoList = new List<copyscene_info>();

	// Token: 0x040029E1 RID: 10721
	public UIWidget PVPSP;

	// Token: 0x040029E2 RID: 10722
	public UIWidget PVESP;

	// Token: 0x040029E3 RID: 10723
	public UIWidget AloneSP;

	// Token: 0x040029E4 RID: 10724
	public UIWidget MultiSp;

	// Token: 0x040029E5 RID: 10725
	public UIWidget TeamSp;

	// Token: 0x040029E6 RID: 10726
	public UIWidget GuildSp;

	// Token: 0x040029E7 RID: 10727
	public UISprite DailyActFlag;

	// Token: 0x040029E8 RID: 10728
	public UILabel DailyActValLabel;

	// Token: 0x040029E9 RID: 10729
	private bool isWarningflag;

	// Token: 0x040029EA RID: 10730
	public GameObject HighEffectObj;

	// Token: 0x040029EB RID: 10731
	private bool isLevelUnlock;

	// Token: 0x040029EC RID: 10732
	private CopySceneData mCurCopyScene;

	// Token: 0x040029ED RID: 10733
	private MAPTYPE CurMapType = MAPTYPE.INVALID;
}
