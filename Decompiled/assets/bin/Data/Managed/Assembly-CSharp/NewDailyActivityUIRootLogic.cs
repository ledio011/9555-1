using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A2A RID: 2602
public class NewDailyActivityUIRootLogic : SingletonUnity<NewDailyActivityUIRootLogic>
{
	// Token: 0x06004B74 RID: 19316 RVA: 0x00191B80 File Offset: 0x0018FD80
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004B75 RID: 19317 RVA: 0x00191B8C File Offset: 0x0018FD8C
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x17000FC9 RID: 4041
	// (get) Token: 0x06004B76 RID: 19318 RVA: 0x00191BBC File Offset: 0x0018FDBC
	public List<activity_info> ActivityList
	{
		get
		{
			return this.activityList;
		}
	}

	// Token: 0x06004B77 RID: 19319 RVA: 0x00191BC4 File Offset: 0x0018FDC4
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06004B78 RID: 19320 RVA: 0x00191BCC File Offset: 0x0018FDCC
	public void EnableReset()
	{
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, false);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, true);
		this.isShowInfoPage = false;
		for (int i = 0; i < this.ActivityItems.Count; i++)
		{
			NGUITools.SetActive(this.ActivityItems[i].gameObject, false);
		}
		for (int j = 0; j < this.WildBossLineList.Count; j++)
		{
			NGUITools.SetActive(this.WildBossLineList[j].gameObject, false);
			NGUITools.SetActive(this.WildBossLineList[j].Sublineobj, false);
		}
		for (int k = 0; k < this.guildBossLines.Count; k++)
		{
			NGUITools.SetActive(this.guildBossLines[k].gameObject, false);
		}
		this.TargetTypeId = -1;
	}

	// Token: 0x06004B79 RID: 19321 RVA: 0x00191CBC File Offset: 0x0018FEBC
	public void ShowAcitvityLine()
	{
		SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
		SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, false);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, true);
		this.isShowInfoPage = false;
		this.RefreshActivityPage();
		this.RefershBossInfo();
		this.RefershGuildBossInfo();
	}

	// Token: 0x06004B7A RID: 19322 RVA: 0x00191D2C File Offset: 0x0018FF2C
	public void ShowActivityInfo()
	{
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, true);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, false);
		this.isShowInfoPage = true;
		this.ReamainTime = 0L;
	}

	// Token: 0x06004B7B RID: 19323 RVA: 0x00191D6C File Offset: 0x0018FF6C
	public void RefreshActivityPage()
	{
		if (this.isShowInfoPage)
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.activityList = new List<activity_info>(playerData.ActivityData.CurActivityDataDic.Values);
		for (int i = this.activityList.Count - 1; i >= 0; i--)
		{
			if (this.activityList[i].Type == 3L || this.activityList[i].Type == 8L)
			{
				this.activityList.RemoveAt(i);
			}
		}
		this.activityList.Sort((activity_info x, activity_info y) => this.GetActivityUnlockLevel(x) - this.GetActivityUnlockLevel(y));
		int num = this.activityList.Count - this.ActivityItems.Count;
		int count = this.ActivityItems.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.ActivityItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D2}", count + j);
				ActivityItemLogic component = gameObject.GetComponent<ActivityItemLogic>();
				if (component != null)
				{
					component.transform.parent = this.ActivityItems[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.ActivityItems.Add(component);
				}
			}
		}
		int num2 = -1;
		for (int k = 0; k < this.ActivityItems.Count; k++)
		{
			NGUITools.SetActive(this.ActivityItems[k].gameObject, k < this.activityList.Count);
			if (k < this.activityList.Count)
			{
				this.ActivityItems[k].onClickItem = new ActivityItemLogic.ActivityItemDelegate(this.OnClickItemBtn);
				this.ResetItemLine(this.ActivityItems[k], k, this.mCurChoosedIndex);
				if (this.activityList[k].Type == (long)this.TargetTypeId)
				{
					num2 = k;
					this.TargetTypeId = -1;
				}
			}
		}
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		this.mCurChoosedIndex = -1;
		if (TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
		else if (num2 != -1)
		{
			if (this.IsMapClick)
			{
				this.ActivityItems[num2].MapClickItemBtn();
				this.IsMapClick = false;
			}
			else
			{
				this.ActivityItems[num2].OnClikcItemBtn();
			}
		}
		if (this.wildBossInfos == null || this.wildBossInfos.Count == 0)
		{
			for (int l = 0; l < this.WildBossLineList.Count; l++)
			{
				NGUITools.SetActive(this.WildBossLineList[l].gameObject, false);
				NGUITools.SetActive(this.WildBossLineList[l].Sublineobj, false);
			}
		}
	}

	// Token: 0x06004B7C RID: 19324 RVA: 0x001920BC File Offset: 0x001902BC
	public void ClickTargetType(GameDefine.ACTIVITY_TYPE type)
	{
		for (int i = 0; i < this.ActivityItems.Count; i++)
		{
			if (this.ActivityItems[i].curActivityInfo.Type == (long)type)
			{
				this.ActivityItems[i].MapClickItemBtn();
				break;
			}
		}
	}

	// Token: 0x06004B7D RID: 19325 RVA: 0x00192118 File Offset: 0x00190318
	public void RefershBossInfo()
	{
		if (this.isShowInfoPage)
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.wildBossInfos = playerData.ActivityData.WildBossInfoList;
		this.wildBossInfos.Sort((activity_info x, activity_info y) => int.Parse(x.ID) - int.Parse(y.ID));
		int num = 1;
		int num2 = num - this.WildBossLineList.Count;
		int num3 = 50;
		this.WildBossLineList[0].gameObject.name = string.Format("huaDongTiao_{0:D2}", num3);
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.WildBossLineList[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D2}", num3 + i + 1);
				WildBossLineLogic component = gameObject.GetComponent<WildBossLineLogic>();
				if (component != null)
				{
					component.transform.parent = this.WildBossLineList[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.WildBossLineList.Add(component);
				}
			}
		}
		for (int j = 0; j < this.WildBossLineList.Count; j++)
		{
			NGUITools.SetActive(this.WildBossLineList[j].gameObject, true);
			this.WildBossLineList[j].ResetItem(this.wildBossInfos, new DelegateDefine.TwoIntParamDelegate(this.OnClickBossBtn));
		}
		this.mCurChoosedIndex = -1;
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		if (TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
		else if (this.TargetTypeId == 5)
		{
			this.WildBossLineList[0].ClickTargetBtn();
			this.TargetTypeId = -1;
			this.IsMapClick = false;
		}
	}

	// Token: 0x06004B7E RID: 19326 RVA: 0x00192328 File Offset: 0x00190528
	public void RefershGuildBossInfo()
	{
		if (this.isShowInfoPage)
		{
			this.UpdateGuildBossInfo();
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.ActivityData.CurGuildBossDataDic.Count > 0)
		{
			this.guildBossInfos = new List<guild_boss>(playerData.ActivityData.CurGuildBossDataDic.Values);
			this.guildBossInfos.Sort((guild_boss x, guild_boss y) => int.Parse(x.id) - int.Parse(y.id));
		}
		int num = Mathf.Min(this.guildBossInfos.Count, 1);
		if (playerData.ActivityData.GuildBattleInfo != null)
		{
			num++;
			this.mGuildBattleIndex = this.GUILD_BATTLE_INDEX;
		}
		else
		{
			this.mGuildBattleIndex = -1;
		}
		if (playerData.ActivityData.GuildDanceInfo != null)
		{
			num++;
		}
		if (playerData.ActivityData.CurGuildCityDataDic != null)
		{
			num++;
		}
		int num2 = num - this.guildBossLines.Count;
		int num3 = 80;
		this.guildBossLines[0].gameObject.name = string.Format("huaDongTiao_{0:D2}", num3);
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.guildBossLines[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D2}", num3 + i + 1);
				GuildBossLineLogic component = gameObject.GetComponent<GuildBossLineLogic>();
				if (component != null)
				{
					component.transform.parent = this.guildBossLines[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.guildBossLines.Add(component);
				}
			}
		}
		for (int j = 0; j < this.guildBossLines.Count; j++)
		{
			NGUITools.SetActive(this.guildBossLines[j].gameObject, j < num);
		}
		int num4 = -1;
		if (playerData.ActivityData.GuildBattleInfo != null)
		{
			for (int k = 1; k < num; k++)
			{
				this.guildBossLines[k].ResetItem(this.guildBossInfos, new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildBossBtn), k);
			}
			this.GuildBattleInfo = playerData.ActivityData.GuildBattleInfo;
			this.mCurGuildBattleData = DataManager.GetGuildBattleDataById(this.GuildBattleInfo.ID);
			this.guildBossLines[this.mGuildBattleIndex].ResetItem(this.mCurGuildBattleData.ID, new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildBattle), this.mGuildBattleIndex, this.GuildBattleInfo);
			if (this.TargetTypeId == 9)
			{
				num4 = this.mGuildBattleIndex;
				this.TargetTypeId = -1;
			}
			else if (this.TargetTypeId == 6 && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				num4 = 1;
				this.TargetTypeId = -1;
			}
		}
		else
		{
			for (int l = 0; l < num; l++)
			{
				this.guildBossLines[l].ResetItem(this.guildBossInfos, new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildBossBtn), l);
			}
		}
		if (playerData.ActivityData.GuildDanceInfo != null)
		{
			this.GuildDanceInfo = playerData.ActivityData.GuildDanceInfo;
			this.mCurGuildDanceData = DataManager.GetCityDanceDataById(this.GuildDanceInfo.ID);
			this.guildBossLines[this.GUILD_DANCE_INDEX].ResetItem(new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildDance), this.GUILD_DANCE_INDEX, this.GuildDanceInfo);
			if (this.TargetTypeId == 16)
			{
				num4 = this.GUILD_DANCE_INDEX;
				this.TargetTypeId = -1;
			}
		}
		if (playerData.ActivityData.CurGuildCityDataDic != null)
		{
			this.GuildCityList = new List<guild_map_info>(playerData.ActivityData.CurGuildCityDataDic.Values);
			this.isOpenGuildCity = false;
			if (this.GuildCityList != null && this.GuildCityList.Count > 0)
			{
				for (int m = 0; m < this.GuildCityList.Count; m++)
				{
					if (this.GuildCityList[m].state == 1L)
					{
						this.isOpenGuildCity = true;
						break;
					}
				}
				this.GuildCityIndex = num - 1;
				this.guildBossLines[this.GuildCityIndex].ResetItem(new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildCity), this.GuildCityIndex, this.GuildCityList);
				if (this.TargetTypeId == 18)
				{
					num4 = this.GuildCityIndex;
					this.TargetTypeId = -1;
				}
			}
		}
		this.mCurChoosedIndex = -1;
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		if (num > 0)
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_WAIT_DATA)
			{
				for (int n = 0; n < num; n++)
				{
					if (this.guildBossLines[n].IsGuildBossLine())
					{
						this.guildBossLines[n].OnClickItemBtn();
						break;
					}
				}
				this.CheckTutorialEvent();
			}
			else if (num4 != -1)
			{
				this.guildBossLines[num4].OnClickItemBtn();
				this.IsMapClick = false;
			}
		}
	}

	// Token: 0x06004B7F RID: 19327 RVA: 0x00192888 File Offset: 0x00190A88
	public void OnClickTargetType(GameDefine.ACTIVITY_TYPE targettype)
	{
		if (targettype == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
		{
			if (this.guildBossLines.Count > 0)
			{
				this.guildBossLines[0].OnClickItemBtn();
			}
		}
		else if (this.TargetTypeId == 6 && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild() && this.guildBossLines.Count > 1)
		{
			this.guildBossLines[1].OnClickItemBtn();
		}
	}

	// Token: 0x06004B80 RID: 19328 RVA: 0x00192908 File Offset: 0x00190B08
	public bool CheckLevel(int minLevel, int maxlevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxlevel);
	}

	// Token: 0x06004B81 RID: 19329 RVA: 0x0019291C File Offset: 0x00190B1C
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		ActivityItemLogic itemLogic = this.ActivityItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex), this.mCurChoosedIndex);
	}

	// Token: 0x06004B82 RID: 19330 RVA: 0x0019294C File Offset: 0x00190B4C
	private void ResetItemLine(ActivityItemLogic itemLogic, int idx, int curChoose)
	{
		if (idx < this.activityList.Count)
		{
			itemLogic.UpdateItem(idx, this.activityList[idx], curChoose);
		}
	}

	// Token: 0x06004B83 RID: 19331 RVA: 0x00192980 File Offset: 0x00190B80
	private void UpdateSelectItem()
	{
	}

	// Token: 0x06004B84 RID: 19332 RVA: 0x00192984 File Offset: 0x00190B84
	public void OnClickGuildCity(int index, int subIndex, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.mCurChoosedSubIndex == subIndex)
		{
			return;
		}
		this.curActivityInfo = null;
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		this.IsShowGuildDanceFlag = false;
		this.ShowActivityInfo();
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], false);
		}
		this.SelectShowINMap(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE, false);
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		NGUITools.SetActive(this.startBtnSp.gameObject, true);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.RewardObj, false);
		if (!this.isOpenGuildCity)
		{
			this.canStartFlag = 2;
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.canStartFlag = 0;
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		string text = string.Empty;
		string empty = string.Empty;
		this.CurGuildCityData = DataManager.GetGuildCaptureDataByID(this.GuildCityList[0].id);
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		text = StrDictionary.GetDictionaryString(this.CurGuildCityData.Description, new object[0]);
		this.DescLabel.text = text;
		for (int j = 0; j < this.rankLabels.Count; j++)
		{
			this.rankLabels[j].text = string.Empty;
		}
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.CurGuildCityData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.CurGuildCityData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.CurGuildCityData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004B85 RID: 19333 RVA: 0x00192C18 File Offset: 0x00190E18
	public void OnClickItemBtn(int realIdx, int enableflag, int unlock, bool isMapClick)
	{
		if (this.mCurChoosedIndex == realIdx && this.curActivityInfo != null && this.curActivityInfo.Type != 5L)
		{
			if (this.curActivityInfo != null)
			{
				if (TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CHOOSE_COPY)
				{
					if (this.curActivityInfo.Type == 7L)
					{
						this.CheckTutorialEvent();
					}
				}
				else if (TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CHOOSE_COPY)
				{
					if (this.curActivityInfo.Type == 1L)
					{
						this.CheckTutorialEvent();
					}
				}
				else if (TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CHOOSE_COPY && this.curActivityInfo.Type == 2L)
				{
					this.CheckTutorialEvent();
				}
			}
			return;
		}
		this.ShowActivityInfo();
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], false);
		}
		this.unLockLevel = unlock;
		this.mCurChoosedIndex = realIdx;
		this.mCurChoosedSubIndex = -1;
		activity_info activity_info = this.activityList[realIdx];
		this.curActivityInfo = activity_info;
		this.SelectShowINMap(this.curActivityInfo, isMapClick);
		this.remainNum = (int)this.curActivityInfo.CurNum;
		ShowRewardData showRewardData = null;
		string text = string.Empty;
		this.curRulestr = string.Empty;
		string text2 = string.Empty;
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		if (this.curActivityInfo.Type == 1L || this.curActivityInfo.Type == 2L)
		{
			EscortData escortDataById = DataManager.GetEscortDataById(this.curActivityInfo.ID);
			string id = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Level).DorpKeyDic[escortDataById.ShowRewardId];
			showRewardData = DataManager.GetShowRewardDataByID(id);
			text = StrDictionary.GetDictionaryString(escortDataById.Description, new object[0]);
			this.curRulestr = escortDataById.Rule;
			if (enableflag == 2)
			{
				enableflag = 0;
			}
			text2 = escortDataById.Background;
		}
		else if (this.curActivityInfo.Type == 3L)
		{
			CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(this.curActivityInfo.ID);
			string id2 = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Level).DorpKeyDic[cityDanceDataById.ShowRewardId];
			showRewardData = DataManager.GetShowRewardDataByID(id2);
			text = StrDictionary.GetDictionaryString(cityDanceDataById.Description, new object[0]);
			this.curRulestr = cityDanceDataById.Rule;
			text2 = cityDanceDataById.Background;
		}
		else if (this.curActivityInfo.Type == 4L)
		{
			if (this.curActivityInfo.State == 1L)
			{
				if (this.curActivityInfo.HasSign && this.curActivityInfo.sign == 1L)
				{
					this.signBtnSp.spriteName = "CZ_anNiu_2+";
					this.signlabel.text = StrDictionary.GetDictionaryString("#{102055}", new object[0]);
				}
				else
				{
					this.signBtnSp.spriteName = "CZ_anNiu_2";
					this.signlabel.text = StrDictionary.GetDictionaryString("#{102048}", new object[0]);
				}
			}
			else
			{
				this.signBtnSp.spriteName = "CZ_anNiu_2+";
				this.signlabel.text = StrDictionary.GetDictionaryString("#{102048}", new object[0]);
			}
			NGUITools.SetActive(this.signBtnSp.gameObject, true);
			BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(this.curActivityInfo.ID);
			string id3 = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ServerLevel).DorpKeyDic[barFightCopyDataByID.RewardID];
			showRewardData = DataManager.GetShowRewardDataByID(id3);
			text = StrDictionary.GetDictionaryString(barFightCopyDataByID.Description, new object[0]);
			this.curRulestr = barFightCopyDataByID.Rule;
			text2 = barFightCopyDataByID.Background;
		}
		else if (this.curActivityInfo.Type == 7L)
		{
			SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(this.curActivityInfo.ID);
			string showRewardID = surviveBattleDataById.ShowRewardID;
			showRewardData = DataManager.GetShowRewardDataByID(showRewardID);
			text = StrDictionary.GetDictionaryString(surviveBattleDataById.Desc, new object[0]);
			this.curRulestr = surviveBattleDataById.Rule;
			text2 = surviveBattleDataById.Background;
		}
		else if (this.curActivityInfo.Type == 8L)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.curActivityInfo.ID);
			string showRewardID2 = sexMiniDataById.ShowRewardID;
			showRewardData = DataManager.GetShowRewardDataByID(showRewardID2);
			text = StrDictionary.GetDictionaryString(sexMiniDataById.Description, new object[0]);
			this.curRulestr = sexMiniDataById.Rule;
			text2 = sexMiniDataById.Background;
			NGUITools.SetActive(this.RankBtnObj, true);
		}
		NGUITools.SetActive(this.RewardObj.gameObject, true);
		if (showRewardData != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
		this.DescLabel.text = text;
		if (enableflag == 0)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
		}
		this.canStartFlag = enableflag;
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (!string.IsNullOrEmpty(text2) && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(text2)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(text2, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_CHOOSE_COPY)
		{
			if (this.curActivityInfo.Type == 4L)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CHOOSE_COPY)
		{
			if (this.curActivityInfo.Type == 1L)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CHOOSE_COPY)
		{
			if (this.curActivityInfo.Type == 2L)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CHOOSE_COPY)
		{
			if (this.curActivityInfo.Type == 7L)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004B86 RID: 19334 RVA: 0x00193370 File Offset: 0x00191570
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBG.mainTexture = tex;
	}

	// Token: 0x06004B87 RID: 19335 RVA: 0x00193380 File Offset: 0x00191580
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardItemsScripts.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06004B88 RID: 19336 RVA: 0x00193390 File Offset: 0x00191590
	public void OnClickSignBarFight()
	{
	}

	// Token: 0x06004B89 RID: 19337 RVA: 0x00193394 File Offset: 0x00191594
	public void OnClickStart()
	{
		if (this.curActivityInfo == null)
		{
			this.OnClickGuildActivityStartBtn();
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_CLICK_START && this.curActivityInfo.Type == 4L)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CLICK_START && this.curActivityInfo.Type == 1L)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CLICK_START && this.curActivityInfo.Type == 2L)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START && this.curActivityInfo.Type == 7L)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_START && this.curActivityInfo.Type == 5L)
		{
			this.CheckTutorialEvent();
		}
		if (this.canStartFlag != 0)
		{
			switch (this.canStartFlag)
			{
			case 1:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
				TutorialManager.LevelLimitAction();
				break;
			case 2:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
				break;
			case 3:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102057}", new object[0]), true, false);
				break;
			case 5:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
				break;
			case 6:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
				break;
			case 7:
				TutorialManager.LevelLimitAction();
				break;
			}
			return;
		}
		if (this.curActivityInfo.Type == 1L)
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			MissionData missionDataByID = DataManager.GetMissionDataByID(this.curActivityInfo.ID);
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager.MissionFindPath(missionDataByID);
		}
		else if (this.curActivityInfo.Type == 2L)
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			MissionData missionDataByID2 = DataManager.GetMissionDataByID(this.curActivityInfo.ID);
			MissionManager missionManager2 = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager2.MissionFindPath(missionDataByID2);
		}
		else if (this.curActivityInfo.Type == 3L)
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.curActivityInfo.ID, Vector3.right * -5f, AUTO_SEARCH_PARTH_FINISHEVENT.CITY_DANCE, null);
		}
		else if (this.curActivityInfo.Type == 4L)
		{
			if (this.curActivityInfo.State == 2L)
			{
				enter_bar_fight.request request = new enter_bar_fight.request();
				request.ID = this.curActivityInfo.ID;
				WaitResponseUIRootLogic.OpenWaitBox(207, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.enter_bar_fight>(request, null);
			}
		}
		else if (this.curActivityInfo.Type == 7L)
		{
			enter_survive_batttle.request request2 = new enter_survive_batttle.request();
			request2.id = this.curActivityInfo.ID;
			request2.floor = 0L;
			WaitResponseUIRootLogic.OpenWaitBox(246, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_survive_batttle>(request2, null);
		}
		else if (this.curActivityInfo.Type == 8L)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.curActivityInfo.ID);
			string text = string.Empty;
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession == PROFESSION_TYPE.NQS)
			{
				text = sexMiniDataById.ManNpcId;
			}
			else
			{
				text = sexMiniDataById.WomenNpcId;
			}
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			MissionManager missionManager3 = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager3.AutoMoveDest(sexMiniDataById.MapId, DataManager.GetNPCPosInMonsterData(sexMiniDataById.MapId, text), AUTO_SEARCH_PARTH_FINISHEVENT.FIND_NPC, text);
		}
		else if (this.curActivityInfo.Type == 5L)
		{
			enter_wild_boss.request request3 = new enter_wild_boss.request();
			request3.ID = this.curActivityInfo.ID;
			WaitResponseUIRootLogic.OpenWaitBox(201, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_wild_boss>(request3, null);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", this.curActivityInfo.Type), "start");
	}

	// Token: 0x06004B8A RID: 19338 RVA: 0x00193800 File Offset: 0x00191A00
	public void OnClicktishiBtn()
	{
		if (string.IsNullOrEmpty(this.curRulestr) || this.curActivityInfo == null)
		{
			this.OnClickGuildtishiBtn();
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			if (this.curActivityInfo.Type == 7L)
			{
				SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById(this.curActivityInfo.ID);
				TimeSpan localShowTime = TimeTools.GetLocalShowTime(surviveBattleDataById.StartTimes, playerCommonData.TimeOffset);
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.curRulestr, null, new object[0]);
			}
			else
			{
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.curRulestr, null, new object[]
				{
					TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
				});
			}
		}, null);
	}

	// Token: 0x06004B8B RID: 19339 RVA: 0x0019384C File Offset: 0x00191A4C
	public void OnClickGuildtishiBtn()
	{
		if (this.mCurChoosedIndex == this.mGuildBattleIndex)
		{
			if (this.mCurGuildBattleData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
				{
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurGuildBattleData.MRule, null, new object[0]);
				}, null);
			}
		}
		else if (this.mCurChoosedIndex == this.GUILD_DANCE_INDEX)
		{
			if (this.mCurGuildDanceData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
				{
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurGuildDanceData.Rule, null, new object[0]);
				}, null);
			}
		}
		else if (this.mCurChoosedIndex == this.GuildCityIndex)
		{
			if (this.CurGuildCityData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
				{
					Debug.Log(this.CurGuildCityData.Rule);
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.CurGuildCityData.Rule, null, new object[0]);
				}, null);
			}
		}
		else if (this.curGuildBossData != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.curGuildBossData.Rule, null, new object[0]);
			}, null);
		}
	}

	// Token: 0x06004B8C RID: 19340 RVA: 0x00193938 File Offset: 0x00191B38
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

	// Token: 0x06004B8D RID: 19341 RVA: 0x00193A10 File Offset: 0x00191C10
	public void OnClickSexMiniRank()
	{
		SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToSexMini();
		}, null);
	}

	// Token: 0x06004B8E RID: 19342 RVA: 0x00193A54 File Offset: 0x00191C54
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004B8F RID: 19343 RVA: 0x00193AB0 File Offset: 0x00191CB0
	public void OnClickBossBtn(int index, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.curActivityInfo != null && this.curActivityInfo.Type != 5L)
		{
			return;
		}
		this.ShowActivityInfo();
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], true);
		}
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = -1;
		this.curActivityInfo = this.wildBossInfos[index];
		this.SelectShowINMap(this.curActivityInfo, false);
		ShowRewardData showRewardData = null;
		string text = string.Empty;
		if (this.curActivityInfo.Type == 5L)
		{
			this.curWildBossData = DataManager.GetWildBossDataByID(this.curActivityInfo.ID);
			showRewardData = DataManager.GetShowRewardDataByID(this.curWildBossData.ShowRewardID);
			text = StrDictionary.GetDictionaryString(this.curWildBossData.Desc, new object[0]);
			this.curRulestr = this.curWildBossData.Rule;
		}
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		this.DescLabel.text = text;
		if (this.curActivityInfo.Parmstr != null)
		{
			string[] array = this.curActivityInfo.Parmstr.Split(new char[]
			{
				'#'
			});
			for (int j = 0; j < this.rankLabels.Count; j++)
			{
				if (j < array.Length)
				{
					if (!array[j].Equals(string.Empty))
					{
						string[] array2 = array[j].Split(new char[]
						{
							'@'
						});
						this.rankLabels[j].text = string.Format("{0}", array2[1]);
					}
					else
					{
						this.rankLabels[j].text = string.Format("{0}", "- - - -");
					}
				}
				else
				{
					this.rankLabels[j].text = string.Format("{0}", "- - - -");
				}
			}
		}
		else
		{
			for (int k = 0; k < this.rankLabels.Count; k++)
			{
				this.rankLabels[k].text = string.Format("{0}", "- - - -");
			}
		}
		NGUITools.SetActive(this.RewardObj.gameObject, true);
		if (showRewardData != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
		if (isenable == 0)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
		}
		this.canStartFlag = isenable;
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.curWildBossData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.curWildBossData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.curWildBossData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CHOOSE_COPY)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004B90 RID: 19344 RVA: 0x00193E90 File Offset: 0x00192090
	public void SelectShowINMap(activity_info datainfo, bool isMapSelect)
	{
		if (!isMapSelect)
		{
			if (datainfo.Type == 1L || datainfo.Type == 2L)
			{
				EscortData escortDataById = DataManager.GetEscortDataById(datainfo.ID);
				if (escortDataById != null)
				{
					bool flag = false;
					ActivityMapData activityMapDataById = DataManager.GetActivityMapDataById(escortDataById.ActivityMapId);
					if (activityMapDataById == null)
					{
						return;
					}
					if (activityMapDataById.MapId.Equals(SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID))
					{
						flag = true;
					}
					bool flag2 = !flag;
					if (activityMapDataById != null)
					{
						if (flag2)
						{
							SingletonUnity<NewMapUIRootLogic>.Instance.Reset(activityMapDataById.MapId);
						}
						if (!activityMapDataById.IsNeedDailyActid())
						{
							SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(activityMapDataById.Type, activityMapDataById.SubType, string.Empty);
						}
						else
						{
							SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(activityMapDataById.Type, activityMapDataById.SubType, activityMapDataById.ActivityID);
						}
					}
				}
				else if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
				}
			}
			else
			{
				List<ActivityMapData> activityMapDataByType = DataManager.GetActivityMapDataByType((int)datainfo.Type, ActivityMapData.DefaultSubType);
				if (activityMapDataByType != null)
				{
					List<ActivityMapData> list = new List<ActivityMapData>();
					for (int i = 0; i < activityMapDataByType.Count; i++)
					{
						if (activityMapDataByType[i].IsVisible)
						{
							list.Add(activityMapDataByType[i]);
						}
					}
					if (list != null && list.Count > 0)
					{
						if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
						{
							bool flag3 = false;
							ActivityMapData activityMapData = null;
							for (int j = 0; j < list.Count; j++)
							{
								if (list[j].MapId.Equals(SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID))
								{
									flag3 = true;
									activityMapData = list[j];
									break;
								}
							}
							bool flag4;
							if (flag3)
							{
								flag4 = false;
							}
							else
							{
								flag4 = true;
								if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID.Equals(SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID))
								{
									bool flag5 = false;
									for (int k = 0; k < list.Count; k++)
									{
										if (list[k].MapId.Equals(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID))
										{
											flag5 = true;
											activityMapData = list[k];
											break;
										}
									}
									if (!flag5)
									{
										activityMapData = list[0];
									}
								}
								else
								{
									activityMapData = list[0];
								}
							}
							if (activityMapData != null)
							{
								if (flag4)
								{
									SingletonUnity<NewMapUIRootLogic>.Instance.Reset(activityMapData.MapId);
								}
								if (!activityMapData.IsNeedDailyActid())
								{
									SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(activityMapData.Type, activityMapData.SubType, string.Empty);
								}
								else
								{
									SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(activityMapData.Type, activityMapData.SubType, activityMapData.ActivityID);
								}
							}
						}
					}
					else if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
					}
				}
			}
		}
	}

	// Token: 0x06004B91 RID: 19345 RVA: 0x001941F8 File Offset: 0x001923F8
	public void SelectShowINMap(GameDefine.ACTIVITY_TYPE ActType, bool isMapSelect)
	{
		if (!isMapSelect)
		{
			List<ActivityMapData> activityMapDataByType = DataManager.GetActivityMapDataByType((int)ActType, ActivityMapData.DefaultSubType);
			if (activityMapDataByType != null)
			{
				List<ActivityMapData> list = new List<ActivityMapData>();
				for (int i = 0; i < activityMapDataByType.Count; i++)
				{
					if (activityMapDataByType[i].IsVisible)
					{
						list.Add(activityMapDataByType[i]);
					}
				}
				if (list != null && list.Count > 0)
				{
					if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
					{
						bool flag = false;
						ActivityMapData activityMapData = null;
						for (int j = 0; j < list.Count; j++)
						{
							if (list[j].MapId.Equals(SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID))
							{
								flag = true;
								activityMapData = list[j];
								break;
							}
						}
						bool flag2;
						if (flag)
						{
							flag2 = false;
						}
						else
						{
							flag2 = true;
							if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID.Equals(SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID))
							{
								bool flag3 = false;
								for (int k = 0; k < list.Count; k++)
								{
									if (list[k].MapId.Equals(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID))
									{
										flag3 = true;
										activityMapData = list[k];
										break;
									}
								}
								if (!flag3)
								{
									activityMapData = list[0];
								}
							}
							else
							{
								activityMapData = list[0];
							}
						}
						if (activityMapData != null)
						{
							if (flag2)
							{
								SingletonUnity<NewMapUIRootLogic>.Instance.Reset(activityMapData.MapId);
							}
							if (!activityMapData.IsNeedDailyActid())
							{
								SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(activityMapData.Type, activityMapData.SubType, string.Empty);
							}
							else
							{
								SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(activityMapData.Type, activityMapData.SubType, activityMapData.ActivityID);
							}
						}
					}
				}
				else if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
				}
			}
		}
	}

	// Token: 0x06004B92 RID: 19346 RVA: 0x00194438 File Offset: 0x00192638
	public void OnClickGuildBossBtn(int index, int subIndex, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.mCurChoosedSubIndex == subIndex)
		{
			return;
		}
		this.curActivityInfo = null;
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		this.ShowActivityInfo();
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], true);
		}
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		this.curBossInfo = this.guildBossInfos[this.mCurChoosedSubIndex];
		this.SelectShowINMap(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, false);
		ShowRewardData showRewardData = null;
		string text = string.Empty;
		this.curGuildBossData = DataManager.GetGuildBossDataByID(this.curBossInfo.id);
		int id = 2;
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			id = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.GuilLevel;
		}
		if (DataManager.GetAdaptDataByID(id).DorpKeyDic.ContainsKey(this.curGuildBossData.ShowRewardID))
		{
			string id2 = DataManager.GetAdaptDataByID(id).DorpKeyDic[this.curGuildBossData.ShowRewardID];
			showRewardData = DataManager.GetShowRewardDataByID(id2);
		}
		text = StrDictionary.GetDictionaryString(this.curGuildBossData.Desc, new object[0]);
		this.DescLabel.text = text;
		for (int j = 0; j < this.rankLabels.Count; j++)
		{
			if (this.curBossInfo.HasSort_item && j < this.curBossInfo.sort_item.Count)
			{
				this.rankLabels[j].text = string.Format("{0}", this.curBossInfo.sort_item[j].name);
			}
			else
			{
				this.rankLabels[j].text = string.Format("{0}", "- - - -");
			}
		}
		NGUITools.SetActive(this.RewardObj.gameObject, true);
		if (showRewardData != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
		if (this.curBossInfo.state == 0L)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
			this.canStartFlag = 2;
		}
		else if (this.curBossInfo.state == 1L)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2";
			this.canStartFlag = 0;
		}
		else if (this.curBossInfo.state == 2L)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
			this.canStartFlag = 3;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
		}
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.curGuildBossData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.curGuildBossData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.curGuildBossData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004B93 RID: 19347 RVA: 0x0019481C File Offset: 0x00192A1C
	public void OnClickGuildBattle(int index, int subIndex, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.mCurChoosedSubIndex == subIndex)
		{
			return;
		}
		this.curActivityInfo = null;
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		this.ShowActivityInfo();
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], false);
		}
		this.SelectShowINMap(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE, false);
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		string text = string.Empty;
		NGUITools.SetActive(this.startBtnSp.gameObject, true);
		if (this.GuildBattleInfo != null && this.GuildBattleInfo.state == -2L)
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.mCurGuildBattleData.ShowRewardID);
		text = this.mCurGuildBattleData.MDesc;
		this.DescLabel.text = text;
		for (int j = 0; j < this.rankLabels.Count; j++)
		{
			this.rankLabels[j].text = string.Empty;
		}
		NGUITools.SetActive(this.RewardObj.gameObject, true);
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.mCurGuildBattleData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.mCurGuildBattleData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurGuildBattleData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		this.canStartFlag = 0;
		if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_CLICK_START)
		{
			FunctionTipsRootLogic.ClearHandTip();
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x06004B94 RID: 19348 RVA: 0x00194AF4 File Offset: 0x00192CF4
	public void OnClickGuildActivityStartBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
			return;
		}
		if (this.mCurChoosedIndex == this.mGuildBattleIndex)
		{
			if (this.GuildBattleInfo != null && this.GuildBattleInfo.state == -2L)
			{
				NoticeLogic.AddNotifyData("#{105077}", true, false);
				return;
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewDailyActivityUIRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMapUIRootLogic);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleRoot, delegate
			{
				SingletonUnity<GuildBattleRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(285, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.req_guild_battle_info>(null, null);
			}, null);
		}
		else if (this.mCurChoosedIndex == this.GUILD_DANCE_INDEX)
		{
			if (this.canStartFlag != 0)
			{
				switch (this.canStartFlag)
				{
				case 1:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
					break;
				case 2:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
					break;
				case 3:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102061}", new object[0]), true, false);
					break;
				}
				return;
			}
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.mCurGuildDanceData.MapId, Vector3.right * -5f, AUTO_SEARCH_PARTH_FINISHEVENT.CITY_DANCE, null);
		}
		else if (this.mCurChoosedIndex == this.GuildCityIndex)
		{
			if (this.canStartFlag != 0)
			{
				switch (this.canStartFlag)
				{
				case 1:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
					break;
				case 2:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
					break;
				case 3:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102061}", new object[0]), true, false);
					break;
				}
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.WorldMapRoot, delegate
			{
				WaitResponseUIRootLogic.OpenWaitBox(319, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_guild_map_info>(null, null);
				SingletonUnity<WorldMapRoot>.Instance.EnableReset(true);
			}, null);
		}
		else
		{
			if (this.curBossInfo == null)
			{
				return;
			}
			if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_CLICK_START)
			{
				this.CheckTutorialEvent();
			}
			if (this.canStartFlag != 0)
			{
				switch (this.canStartFlag)
				{
				case 1:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
					break;
				case 2:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
					break;
				case 3:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102061}", new object[0]), true, false);
					break;
				}
				return;
			}
			enter_guild_boss_scene.request request = new enter_guild_boss_scene.request();
			request.id = this.curBossInfo.id;
			WaitResponseUIRootLogic.OpenWaitBox(194, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_guild_boss_scene>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("guildboss", string.Format("guildboss_{0}", this.curBossInfo.id), "starttimes");
		}
	}

	// Token: 0x06004B95 RID: 19349 RVA: 0x00194E40 File Offset: 0x00193040
	public void OnClickGuildDance(int index, int subIndex, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.mCurChoosedSubIndex == subIndex)
		{
			return;
		}
		this.curActivityInfo = null;
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		this.IsShowGuildDanceFlag = true;
		this.ShowActivityInfo();
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], false);
		}
		this.SelectShowINMap(GameDefine.ACTIVITY_TYPE.GUILD_DANCE, false);
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		string text = string.Empty;
		if (this.GuildDanceInfo.state != 1L)
		{
			this.canStartFlag = 2;
			NGUITools.SetActive(this.startBtnSp.gameObject, false);
			NGUITools.SetActive(this.ChangeTimeBtn.gameObject, true);
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.canStartFlag = 0;
			NGUITools.SetActive(this.startBtnSp.gameObject, true);
			NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		if (this.GuildDanceInfo.HasReset_time)
		{
			this.ReamainTime = this.GuildDanceInfo.reset_time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
		}
		else
		{
			this.ReamainTime = -1L;
		}
		if (this.ReamainTime > 0L)
		{
			this.NextTimeLabel.text = StrDictionary.GetDictionaryString("#{105095}", new object[]
			{
				TimeTools.GetFullTime(this.ReamainTime)
			});
			this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[2];
			this.NextTimeName.enabled = true;
		}
		else
		{
			this.NextTimeLabel.text = string.Empty;
			this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[1];
			this.NextTimeName.enabled = false;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.mCurGuildDanceData.ShowRewardId);
		text = StrDictionary.GetDictionaryString(this.mCurGuildDanceData.Description, new object[0]);
		this.DescLabel.text = text;
		for (int j = 0; j < this.rankLabels.Count; j++)
		{
			this.rankLabels[j].text = string.Empty;
		}
		NGUITools.SetActive(this.RewardObj.gameObject, true);
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.mCurGuildDanceData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.mCurGuildDanceData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurGuildDanceData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004B96 RID: 19350 RVA: 0x001951F8 File Offset: 0x001933F8
	public void OnClickChangeTimeBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
			return;
		}
		if (this.GuildDanceInfo.state == 1L)
		{
			NoticeLogic.AddNotifyData("#{105098}", true, false);
			return;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.isGuildChief())
		{
			NoticeLogic.AddNotifyData("#{105097}", true, false);
			return;
		}
		if (this.ReamainTime > 0L)
		{
			NoticeLogic.AddNotifyData("#{105096}", true, false);
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChangeTimeRoot, delegate
		{
			SingletonUnity<ChangeTimeRootLogic>.Instance.ResetGuildDance(this.mCurGuildDanceData.StartTimes, (int)this.GuildDanceInfo.parm - 1, StrDictionary.GetDictionaryString(this.mCurGuildDanceData.Name, new object[0]), this.mCurGuildDanceData.DurationTime);
		}, null);
	}

	// Token: 0x06004B97 RID: 19351 RVA: 0x0019529C File Offset: 0x0019349C
	private void Update()
	{
		if (this.ReamainTime > 0L)
		{
			this.tempCountTime += Time.deltaTime;
			if (this.tempCountTime >= 1f)
			{
				this.ReamainTime -= 1L;
				this.tempCountTime -= 1f;
				this.NextTimeLabel.text = StrDictionary.GetDictionaryString("#{105095}", new object[]
				{
					TimeTools.GetFullTime(this.ReamainTime)
				});
			}
			if (this.ReamainTime <= 0L)
			{
				this.tempCountTime = 0f;
				this.NextTimeLabel.text = string.Empty;
				this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[1];
				this.NextTimeName.enabled = false;
			}
		}
	}

	// Token: 0x06004B98 RID: 19352 RVA: 0x00195368 File Offset: 0x00193568
	public void UpdateGuildBossInfo()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.ActivityData.CurGuildBossDataDic.Count > 0)
		{
			this.guildBossInfos = new List<guild_boss>(playerData.ActivityData.CurGuildBossDataDic.Values);
			this.guildBossInfos.Sort((guild_boss x, guild_boss y) => int.Parse(x.id) - int.Parse(y.id));
		}
		if (playerData.ActivityData.GuildBattleInfo != null)
		{
			this.GuildBattleInfo = playerData.ActivityData.GuildBattleInfo;
			this.mCurGuildBattleData = DataManager.GetGuildBattleDataById(this.GuildBattleInfo.ID);
		}
		if (playerData.ActivityData.GuildDanceInfo != null)
		{
			this.GuildDanceInfo = playerData.ActivityData.GuildDanceInfo;
			this.mCurGuildDanceData = DataManager.GetCityDanceDataById(this.GuildDanceInfo.ID);
		}
		if (playerData.ActivityData.CurGuildCityDataDic != null)
		{
			this.GuildCityList = new List<guild_map_info>(playerData.ActivityData.CurGuildCityDataDic.Values);
			this.isOpenGuildCity = false;
			if (this.GuildCityList != null && this.GuildCityList.Count > 0)
			{
				for (int i = 0; i < this.GuildCityList.Count; i++)
				{
					if (this.GuildCityList[i].state == 1L)
					{
						this.isOpenGuildCity = true;
						break;
					}
				}
			}
			this.CurGuildCityData = DataManager.GetGuildCaptureDataByID(this.GuildCityList[0].id);
		}
		if (this.curActivityInfo == null && this.mCurChoosedIndex == this.GUILD_DANCE_INDEX)
		{
			if (this.GuildDanceInfo.state != 1L)
			{
				this.canStartFlag = 2;
				NGUITools.SetActive(this.startBtnSp.gameObject, false);
				NGUITools.SetActive(this.ChangeTimeBtn.gameObject, true);
				this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
			}
			else
			{
				this.canStartFlag = 0;
				NGUITools.SetActive(this.startBtnSp.gameObject, true);
				NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
				this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
			}
			if (this.GuildDanceInfo.HasReset_time)
			{
				this.ReamainTime = this.GuildDanceInfo.reset_time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
			}
			else
			{
				this.ReamainTime = -1L;
			}
			if (this.ReamainTime > 0L)
			{
				this.NextTimeLabel.text = StrDictionary.GetDictionaryString("#{105095}", new object[]
				{
					TimeTools.GetFullTime(this.ReamainTime)
				});
				this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[2];
				this.NextTimeName.enabled = true;
			}
			else
			{
				this.NextTimeLabel.text = string.Empty;
				this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[1];
				this.NextTimeName.enabled = false;
			}
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
			}
		}
	}

	// Token: 0x04003921 RID: 14625
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003922 RID: 14626
	public List<ActivityItemLogic> ActivityItems = new List<ActivityItemLogic>();

	// Token: 0x04003923 RID: 14627
	public ShowRewardItems ShowRewardItemsScripts;

	// Token: 0x04003924 RID: 14628
	public UILabel DescLabel;

	// Token: 0x04003925 RID: 14629
	public UIScrollView uiScrollView;

	// Token: 0x04003926 RID: 14630
	public UITexture ActivityPic;

	// Token: 0x04003927 RID: 14631
	private int remainNum;

	// Token: 0x04003928 RID: 14632
	private int mCurChoosedIndex = -1;

	// Token: 0x04003929 RID: 14633
	private List<activity_info> activityList = new List<activity_info>();

	// Token: 0x0400392A RID: 14634
	private activity_info curActivityInfo;

	// Token: 0x0400392B RID: 14635
	public UISprite startBtnSp;

	// Token: 0x0400392C RID: 14636
	public GameObject RankBtnObj;

	// Token: 0x0400392D RID: 14637
	public UISprite signBtnSp;

	// Token: 0x0400392E RID: 14638
	public UILabel signlabel;

	// Token: 0x0400392F RID: 14639
	private int canStartFlag;

	// Token: 0x04003930 RID: 14640
	private string curRulestr;

	// Token: 0x04003931 RID: 14641
	private int unLockLevel;

	// Token: 0x04003932 RID: 14642
	public UITexture CopyBG;

	// Token: 0x04003933 RID: 14643
	public bool IsMapClick;

	// Token: 0x04003934 RID: 14644
	public int TargetTypeId;

	// Token: 0x04003935 RID: 14645
	public List<WildBossLineLogic> WildBossLineList = new List<WildBossLineLogic>();

	// Token: 0x04003936 RID: 14646
	private List<activity_info> wildBossInfos = new List<activity_info>();

	// Token: 0x04003937 RID: 14647
	public UITable RootTable;

	// Token: 0x04003938 RID: 14648
	public List<UILabel> rankLabels;

	// Token: 0x04003939 RID: 14649
	public List<GameObject> RankLabelObjList;

	// Token: 0x0400393A RID: 14650
	private Dictionary<string, daily_active> activeDic = new Dictionary<string, daily_active>();

	// Token: 0x0400393B RID: 14651
	public UIScrollBar ItemRootBar;

	// Token: 0x0400393C RID: 14652
	public GameObject ActivityInfoPage;

	// Token: 0x0400393D RID: 14653
	public GameObject ActivityLineRoot;

	// Token: 0x0400393E RID: 14654
	private bool isShowInfoPage;

	// Token: 0x0400393F RID: 14655
	private List<guild_map_info> GuildCityList;

	// Token: 0x04003940 RID: 14656
	private GuildCaptureData CurGuildCityData;

	// Token: 0x04003941 RID: 14657
	private bool isOpenGuildCity;

	// Token: 0x04003942 RID: 14658
	public GameObject RewardObj;

	// Token: 0x04003943 RID: 14659
	private List<guild_boss> guildBossInfos = new List<guild_boss>();

	// Token: 0x04003944 RID: 14660
	private guild_boss curBossInfo;

	// Token: 0x04003945 RID: 14661
	private GuildBattleData mCurGuildBattleData;

	// Token: 0x04003946 RID: 14662
	private int mGuildBattleIndex = -1;

	// Token: 0x04003947 RID: 14663
	private int GUILD_BATTLE_INDEX;

	// Token: 0x04003948 RID: 14664
	private int mCurChoosedSubIndex = -1;

	// Token: 0x04003949 RID: 14665
	private GuildBossData curGuildBossData;

	// Token: 0x0400394A RID: 14666
	private guild_battle_info GuildBattleInfo;

	// Token: 0x0400394B RID: 14667
	public List<GuildBossLineLogic> guildBossLines = new List<GuildBossLineLogic>();

	// Token: 0x0400394C RID: 14668
	private CityDanceData mCurGuildDanceData;

	// Token: 0x0400394D RID: 14669
	private int GUILD_DANCE_INDEX = 2;

	// Token: 0x0400394E RID: 14670
	private dance_state_info GuildDanceInfo;

	// Token: 0x0400394F RID: 14671
	public UISprite ChangeTimeBtn;

	// Token: 0x04003950 RID: 14672
	private long ReamainTime;

	// Token: 0x04003951 RID: 14673
	public UILabel NextTimeLabel;

	// Token: 0x04003952 RID: 14674
	public UILabel NextTimeName;

	// Token: 0x04003953 RID: 14675
	private int GuildCityIndex;

	// Token: 0x04003954 RID: 14676
	private WildBossData curWildBossData;

	// Token: 0x04003955 RID: 14677
	private bool IsShowGuildDanceFlag;

	// Token: 0x04003956 RID: 14678
	private float tempCountTime;
}
