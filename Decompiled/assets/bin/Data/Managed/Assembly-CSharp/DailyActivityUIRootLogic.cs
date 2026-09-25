using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008F3 RID: 2291
public class DailyActivityUIRootLogic : SingletonUnity<DailyActivityUIRootLogic>
{
	// Token: 0x06003E1A RID: 15898 RVA: 0x0011985C File Offset: 0x00117A5C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x00119868 File Offset: 0x00117A68
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x17000F83 RID: 3971
	// (get) Token: 0x06003E1C RID: 15900 RVA: 0x00119898 File Offset: 0x00117A98
	public List<activity_info> ActivityList
	{
		get
		{
			return this.activityList;
		}
	}

	// Token: 0x06003E1D RID: 15901 RVA: 0x001198A0 File Offset: 0x00117AA0
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x001198A8 File Offset: 0x00117AA8
	public void UpdataActiveInfo()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.activeDic = playerData.welfareData.DailyActives;
		NGUITools.SetActive(this.ScoreObj, false);
		if (this.curActivityInfo == null)
		{
			return;
		}
		long type = this.curActivityInfo.Type;
		if (type >= 1L && type <= 5L)
		{
			switch ((int)(type - 1L))
			{
			case 0:
				this.ShowScore(14);
				break;
			case 1:
				this.ShowScore(4);
				break;
			case 4:
				this.ShowScore(6);
				break;
			}
		}
	}

	// Token: 0x06003E1F RID: 15903 RVA: 0x00119950 File Offset: 0x00117B50
	private void ShowScore(int type)
	{
		for (int i = 0; i < this.DailyActiveDataList.Count; i++)
		{
			if (this.DailyActiveDataList[i].Type == type)
			{
				NGUITools.SetActive(this.ScoreObj, true);
				int num = 0;
				if (this.activeDic.ContainsKey(this.DailyActiveDataList[i].ID) && this.activeDic[this.DailyActiveDataList[i].ID].HasCount)
				{
					num = (int)this.activeDic[this.DailyActiveDataList[i].ID].count * this.DailyActiveDataList[i].Score;
				}
				int num2 = this.DailyActiveDataList[i].Score * this.DailyActiveDataList[i].Count;
				if (num <= num2)
				{
					this.scorelabel.text = string.Format("{0}/{1}", num, num2);
				}
				else
				{
					this.scorelabel.text = string.Format("{0}/{1}", num2, num2);
				}
				return;
			}
		}
	}

	// Token: 0x06003E20 RID: 15904 RVA: 0x00119A90 File Offset: 0x00117C90
	public void EnableReset()
	{
		for (int i = 0; i < this.ActivityItems.Count; i++)
		{
			NGUITools.SetActive(this.ActivityItems[i].gameObject, false);
		}
		for (int j = 0; j < this.WildBossLineList.Count; j++)
		{
			NGUITools.SetActive(this.WildBossLineList[j].gameObject, false);
			NGUITools.SetActive(this.WildBossLineList[j].Sublineobj, false);
		}
		this.TargetTypeId = -1;
		NGUITools.SetActive(this.RankBtnObj, false);
		NGUITools.SetActive(this.signBtnSp.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		this.curActivityInfo = null;
		this.curRulestr = string.Empty;
		this.DailyActiveDataList = DataManager.GetDailyActiveDataList();
	}

	// Token: 0x06003E21 RID: 15905 RVA: 0x00119B6C File Offset: 0x00117D6C
	public void RefreshActivityPage(ret_request_activity_info.request request)
	{
		this.activityList = new List<activity_info>(request.activity_info.Values);
		this.activityList.Sort((activity_info x, activity_info y) => this.GetActivityUnlockLevel(x) - this.GetActivityUnlockLevel(y));
		int num = Mathf.Min(this.activityList.Count, this.LineCount) - this.ActivityItems.Count;
		int count = this.ActivityItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.ActivityItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0}", count + i);
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
		int num2 = 0;
		for (int j = 0; j < this.ActivityItems.Count; j++)
		{
			NGUITools.SetActive(this.ActivityItems[j].gameObject, j < this.activityList.Count);
			if (j < this.activityList.Count)
			{
				this.ActivityItems[j].onClickItem = new ActivityItemLogic.ActivityItemDelegate(this.OnClickItemBtn);
				this.ResetItemLine(this.ActivityItems[j], j, this.mCurChoosedIndex);
				if (this.activityList[j].Type == (long)this.TargetTypeId)
				{
					num2 = j;
				}
			}
		}
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		this.mCurChoosedIndex = -1;
		this.ActivityItems[num2].OnClikcItemBtn();
		if (this.wildBossInfos == null || this.wildBossInfos.Count == 0)
		{
			for (int k = 0; k < this.WildBossLineList.Count; k++)
			{
				NGUITools.SetActive(this.WildBossLineList[k].gameObject, false);
				NGUITools.SetActive(this.WildBossLineList[k].Sublineobj, false);
			}
		}
	}

	// Token: 0x06003E22 RID: 15906 RVA: 0x00119DD8 File Offset: 0x00117FD8
	public void RefershBossInfo(ret_request_wild_boss_info.request request)
	{
		this.wildBossInfos = new List<activity_info>(request.activity_info.Values);
		this.wildBossInfos.Sort((activity_info x, activity_info y) => int.Parse(x.ID) - int.Parse(y.ID));
		int num = 1;
		int num2 = num - this.WildBossLineList.Count;
		int num3 = this.WildBossLineList.Count + this.ActivityItems.Count;
		this.WildBossLineList[0].gameObject.name = string.Format("huaDongTiao_{0}", this.ActivityItems.Count);
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.WildBossLineList[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0}", num3 + i);
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
		if (this.TargetTypeId == 5)
		{
			this.WildBossLineList[0].OnClickItemBtn();
		}
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x00119FC8 File Offset: 0x001181C8
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		ActivityItemLogic itemLogic = this.ActivityItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex), this.mCurChoosedIndex);
	}

	// Token: 0x06003E24 RID: 15908 RVA: 0x00119FF8 File Offset: 0x001181F8
	private void ResetItemLine(ActivityItemLogic itemLogic, int idx, int curChoose)
	{
		if (idx < this.activityList.Count)
		{
			itemLogic.UpdateItem(idx, this.activityList[idx], curChoose);
		}
	}

	// Token: 0x06003E25 RID: 15909 RVA: 0x0011A02C File Offset: 0x0011822C
	private void UpdateSelectItem()
	{
		if (this.curActivityInfo.Type != 5L)
		{
			for (int i = 0; i < this.ActivityItems.Count; i++)
			{
				this.ActivityItems[i].RefreshSelect(this.mCurChoosedIndex);
			}
			for (int j = 0; j < this.WildBossLineList.Count; j++)
			{
				this.WildBossLineList[j].RefreshSelect(-1);
			}
		}
		else
		{
			for (int k = 0; k < this.ActivityItems.Count; k++)
			{
				this.ActivityItems[k].RefreshSelect(-1);
			}
			for (int l = 0; l < this.WildBossLineList.Count; l++)
			{
				this.WildBossLineList[l].RefreshSelect(this.mCurChoosedIndex);
			}
		}
	}

	// Token: 0x06003E26 RID: 15910 RVA: 0x0011A114 File Offset: 0x00118314
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
				else if (TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CHOOSE_COPY && (this.curActivityInfo.Type == 1L || this.curActivityInfo.Type == 2L))
				{
					this.CheckTutorialEvent();
				}
			}
			return;
		}
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], false);
		}
		this.unLockLevel = unlock;
		this.mCurChoosedIndex = realIdx;
		activity_info activity_info = this.activityList[realIdx];
		this.curActivityInfo = activity_info;
		if (TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CHOOSE_COPY && this.curActivityInfo.Type == 2L)
		{
			this.CheckTutorialEvent();
			return;
		}
		this.remainNum = (int)this.curActivityInfo.CurNum;
		ShowRewardData showRewardData = null;
		string text = string.Empty;
		this.curRulestr = string.Empty;
		string text2 = string.Empty;
		NGUITools.SetActive(this.RankBtnObj, false);
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
			string id4 = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ServerLevel).DorpKeyDic[surviveBattleDataById.ShowRewardID];
			showRewardData = DataManager.GetShowRewardDataByID(id4);
			text = StrDictionary.GetDictionaryString(surviveBattleDataById.Desc, new object[0]);
			this.curRulestr = surviveBattleDataById.Rule;
			text2 = surviveBattleDataById.Background;
		}
		else if (this.curActivityInfo.Type == 8L)
		{
			SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.curActivityInfo.ID);
			string showRewardID = sexMiniDataById.ShowRewardID;
			showRewardData = DataManager.GetShowRewardDataByID(showRewardID);
			text = StrDictionary.GetDictionaryString(sexMiniDataById.Description, new object[0]);
			this.curRulestr = sexMiniDataById.Rule;
			text2 = sexMiniDataById.Background;
			NGUITools.SetActive(this.RankBtnObj, true);
		}
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
		this.UpdataActiveInfo();
		this.UpdateSelectItem();
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
			if (this.curActivityInfo.Type == 1L || this.curActivityInfo.Type == 2L)
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

	// Token: 0x06003E27 RID: 15911 RVA: 0x0011A878 File Offset: 0x00118A78
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBG.mainTexture = tex;
	}

	// Token: 0x06003E28 RID: 15912 RVA: 0x0011A888 File Offset: 0x00118A88
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardItemsScripts.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06003E29 RID: 15913 RVA: 0x0011A898 File Offset: 0x00118A98
	public void OnClickSignBarFight()
	{
		if (this.canStartFlag == 1)
		{
			TutorialManager.LevelLimitAction();
			return;
		}
		if (this.curActivityInfo.State == 1L)
		{
			if (this.curActivityInfo.HasSign && this.curActivityInfo.sign == 1L)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102056}", new object[0]), true, false);
			}
			else
			{
				sign_bar_fight.request request = new sign_bar_fight.request();
				request.ID = this.curActivityInfo.ID;
				NetLogic.GetInstance().Send<Protocol.sign_bar_fight>(request, null);
				this.curActivityInfo.sign = 1L;
				this.signBtnSp.spriteName = "CZ_anNiu_2+";
				this.signlabel.text = StrDictionary.GetDictionaryString("#{102055}", new object[0]);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", this.curActivityInfo.Type), "entoll");
			}
		}
		else
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102051}", new object[0]), true, false);
		}
	}

	// Token: 0x06003E2A RID: 15914 RVA: 0x0011A9AC File Offset: 0x00118BAC
	public void OnClickStart()
	{
		if (this.curActivityInfo == null)
		{
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
			SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
			MissionData missionDataByID = DataManager.GetMissionDataByID(this.curActivityInfo.ID);
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager.MissionFindPath(missionDataByID);
		}
		else if (this.curActivityInfo.Type == 2L)
		{
			SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
			MissionData missionDataByID2 = DataManager.GetMissionDataByID(this.curActivityInfo.ID);
			MissionManager missionManager2 = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager2.MissionFindPath(missionDataByID2);
		}
		else if (this.curActivityInfo.Type == 3L)
		{
			SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
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
			SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
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

	// Token: 0x06003E2B RID: 15915 RVA: 0x0011AE14 File Offset: 0x00119014
	public void OnClicktishiBtn()
	{
		if (string.IsNullOrEmpty(this.curRulestr))
		{
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

	// Token: 0x06003E2C RID: 15916 RVA: 0x0011AE44 File Offset: 0x00119044
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

	// Token: 0x06003E2D RID: 15917 RVA: 0x0011AF1C File Offset: 0x0011911C
	public void OnClickSexMiniRank()
	{
		SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToSexMini();
		}, null);
	}

	// Token: 0x06003E2E RID: 15918 RVA: 0x0011AF60 File Offset: 0x00119160
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DailyRewardNewRoot, delegate
		{
			SingletonUnity<DailyRewardNewLogic>.Instance.EnableReset();
			NetLogic.GetInstance().Send<Protocol.request_daily_active>(null, null);
		}, null);
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x0011AF90 File Offset: 0x00119190
	private void OnDisable()
	{
		if (SingletonUnity<UIManager>.Exists)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyRewardNewRoot);
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06003E30 RID: 15920 RVA: 0x0011AFEC File Offset: 0x001191EC
	public void OnClickBossBtn(int index, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.curActivityInfo != null && this.curActivityInfo.Type != 5L)
		{
			return;
		}
		for (int i = 0; i < this.RankLabelObjList.Count; i++)
		{
			NGUITools.SetActive(this.RankLabelObjList[i], true);
		}
		this.mCurChoosedIndex = index;
		this.curActivityInfo = this.wildBossInfos[index];
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
		if (showRewardData != null)
		{
			NGUITools.SetActive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			NGUITools.SetActive(this.ShowRewardItemsScripts.gameObject, false);
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
		this.UpdataActiveInfo();
		this.UpdateSelectItem();
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
	}

	// Token: 0x040029B2 RID: 10674
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040029B3 RID: 10675
	public int LineCount = 8;

	// Token: 0x040029B4 RID: 10676
	public List<ActivityItemLogic> ActivityItems = new List<ActivityItemLogic>();

	// Token: 0x040029B5 RID: 10677
	public ShowRewardItems ShowRewardItemsScripts;

	// Token: 0x040029B6 RID: 10678
	public UILabel DescLabel;

	// Token: 0x040029B7 RID: 10679
	public UIScrollView uiScrollView;

	// Token: 0x040029B8 RID: 10680
	public UITexture ActivityPic;

	// Token: 0x040029B9 RID: 10681
	private int remainNum;

	// Token: 0x040029BA RID: 10682
	private int mCurChoosedIndex = -1;

	// Token: 0x040029BB RID: 10683
	private List<activity_info> activityList = new List<activity_info>();

	// Token: 0x040029BC RID: 10684
	private activity_info curActivityInfo;

	// Token: 0x040029BD RID: 10685
	public UISprite startBtnSp;

	// Token: 0x040029BE RID: 10686
	public GameObject RankBtnObj;

	// Token: 0x040029BF RID: 10687
	public UISprite signBtnSp;

	// Token: 0x040029C0 RID: 10688
	public UILabel signlabel;

	// Token: 0x040029C1 RID: 10689
	private int canStartFlag;

	// Token: 0x040029C2 RID: 10690
	private string curRulestr;

	// Token: 0x040029C3 RID: 10691
	private int unLockLevel;

	// Token: 0x040029C4 RID: 10692
	public UITexture CopyBG;

	// Token: 0x040029C5 RID: 10693
	public int TargetTypeId;

	// Token: 0x040029C6 RID: 10694
	public GameObject ScoreObj;

	// Token: 0x040029C7 RID: 10695
	public UILabel scorelabel;

	// Token: 0x040029C8 RID: 10696
	public List<WildBossLineLogic> WildBossLineList = new List<WildBossLineLogic>();

	// Token: 0x040029C9 RID: 10697
	private List<activity_info> wildBossInfos = new List<activity_info>();

	// Token: 0x040029CA RID: 10698
	public UITable RootTable;

	// Token: 0x040029CB RID: 10699
	public List<UILabel> rankLabels;

	// Token: 0x040029CC RID: 10700
	public List<GameObject> RankLabelObjList;

	// Token: 0x040029CD RID: 10701
	private List<DailyActiveData> DailyActiveDataList = new List<DailyActiveData>();

	// Token: 0x040029CE RID: 10702
	private Dictionary<string, daily_active> activeDic = new Dictionary<string, daily_active>();

	// Token: 0x040029CF RID: 10703
	public UIScrollBar ItemRootBar;

	// Token: 0x040029D0 RID: 10704
	private WildBossData curWildBossData;
}
