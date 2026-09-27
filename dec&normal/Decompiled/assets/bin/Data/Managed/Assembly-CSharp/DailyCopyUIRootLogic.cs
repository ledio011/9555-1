using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008F6 RID: 2294
public class DailyCopyUIRootLogic : SingletonUnity<DailyCopyUIRootLogic>
{
	// Token: 0x06003E50 RID: 15952 RVA: 0x0011C4AC File Offset: 0x0011A6AC
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003E51 RID: 15953 RVA: 0x0011C4B8 File Offset: 0x0011A6B8
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06003E52 RID: 15954 RVA: 0x0011C4E8 File Offset: 0x0011A6E8
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DailyRewardNewRoot, delegate
		{
			SingletonUnity<DailyRewardNewLogic>.Instance.EnableReset();
			NetLogic.GetInstance().Send<Protocol.request_daily_active>(null, null);
		}, null);
	}

	// Token: 0x06003E53 RID: 15955 RVA: 0x0011C518 File Offset: 0x0011A718
	public void UpdataActiveInfo()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.activeDic = playerData.welfareData.DailyActives;
		NGUITools.SetActive(this.ScoreObj, false);
		if (this.mCurCopyScene == null)
		{
			return;
		}
		int subType = this.mCurCopyScene.SubType;
		switch (subType)
		{
		case 11:
			this.ShowScore(2);
			break;
		case 12:
			this.ShowScore(1);
			break;
		default:
			if (subType == 7)
			{
				this.ShowScore(3);
			}
			break;
		case 16:
			this.ShowScore(0);
			break;
		}
	}

	// Token: 0x06003E54 RID: 15956 RVA: 0x0011C5C4 File Offset: 0x0011A7C4
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
					this.ScoreLabel.text = string.Format("{0}/{1}", num, num2);
				}
				else
				{
					this.ScoreLabel.text = string.Format("{0}/{1}", num2, num2);
				}
				return;
			}
		}
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x0011C704 File Offset: 0x0011A904
	public void EnableReset()
	{
		for (int i = 0; i < this.DailyCopyLineList.Count; i++)
		{
			NGUITools.SetActive(this.DailyCopyLineList[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.WipeOutBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.MatchBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.BtnRankObj, false);
		this.TargetTypeId = -1;
		UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		this.mCurCopyScene = null;
		this.DailyActiveDataList = DataManager.GetDailyActiveDataList();
	}

	// Token: 0x06003E56 RID: 15958 RVA: 0x0011C794 File Offset: 0x0011A994
	public void RefreshCopyPage()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.list = new List<copyscene_info>(playerData.CopyInfoData.DailyCopyInfoList);
		this.DailyCopyInfoDic = playerData.CopyInfoData.DailyCopyInfoDic;
		this.equipList.Clear();
		this.pklist.Clear();
		for (int i = this.list.Count - 1; i >= 0; i--)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.list[i].ID);
			if (copySceneDataById == null)
			{
				this.list.RemoveAt(i);
			}
			else if (copySceneDataById.SubType == 16)
			{
				this.equipList.Add(this.list[i]);
				this.list.RemoveAt(i);
			}
			else if (copySceneDataById.SubType == 1)
			{
				this.pklist.Add(this.list[i]);
				this.list.RemoveAt(i);
			}
		}
		this.list.Sort(delegate(copyscene_info x, copyscene_info y)
		{
			CopySceneData copySceneDataById2 = DataManager.GetCopySceneDataById(x.ID);
			CopySceneData copySceneDataById3 = DataManager.GetCopySceneDataById(y.ID);
			return copySceneDataById2.MinLevel - copySceneDataById3.MinLevel;
		});
		this.equipList.Sort(delegate(copyscene_info x, copyscene_info y)
		{
			CopySceneData copySceneDataById2 = DataManager.GetCopySceneDataById(x.ID);
			CopySceneData copySceneDataById3 = DataManager.GetCopySceneDataById(y.ID);
			return copySceneDataById2.MinLevel - copySceneDataById3.MinLevel;
		});
		this.pklist.Sort(delegate(copyscene_info x, copyscene_info y)
		{
			CopySceneData copySceneDataById2 = DataManager.GetCopySceneDataById(x.ID);
			CopySceneData copySceneDataById3 = DataManager.GetCopySceneDataById(y.ID);
			return copySceneDataById2.MinLevel - copySceneDataById3.MinLevel;
		});
		int num = this.list.Count + 2;
		int num2 = num - this.DailyCopyLineList.Count;
		int count = this.DailyCopyLineList.Count;
		if (num2 > 0)
		{
			for (int j = 0; j < num2; j++)
			{
				GameObject gameObject = Object.Instantiate(this.DailyCopyLineList[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D3}", count + j);
				DailyCopyLineLogic component = gameObject.GetComponent<DailyCopyLineLogic>();
				if (component != null)
				{
					component.transform.parent = this.DailyCopyLineList[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.DailyCopyLineList.Add(component);
				}
			}
		}
		this.equipcopy_index = this.list.Count + 1;
		int num3 = 0;
		for (int k = 0; k < this.DailyCopyLineList.Count; k++)
		{
			NGUITools.SetActive(this.DailyCopyLineList[k].gameObject, k < num);
			if (k < num)
			{
				if (k < this.list.Count)
				{
					this.DailyCopyLineList[k].ResetItem(this.list[k], new DelegateDefine.StringGameObjectDelegate(this.OnClickItemBtn), false);
					if (DataManager.GetCopySceneDataById(this.list[k].ID).SubType == this.TargetTypeId)
					{
						num3 = k;
					}
				}
				else if (k == this.list.Count)
				{
					this.DailyCopyLineList[k].ResetItem(string.Empty, "#{101633}", this.pklist, new DelegateDefine.StringGameObjectDelegate(this.OnClickItemBtn), false);
					if (this.TargetTypeId == 1)
					{
						num3 = this.list.Count;
					}
				}
				else
				{
					this.DailyCopyLineList[k].ResetItem(string.Empty, "#{101510}", this.equipList, new DelegateDefine.StringGameObjectDelegate(this.OnClickItemBtn), true);
					if (this.TargetTypeId == 16)
					{
						num3 = this.equipcopy_index;
					}
				}
			}
		}
		this.curChoosekey = string.Empty;
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		this.DailyCopyLineList[num3].OnClikcItemBtn();
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x0011CB98 File Offset: 0x0011AD98
	public bool CheckLevel()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.mCurCopyScene.MinLevel);
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x0011CBB4 File Offset: 0x0011ADB4
	public void OnClickCarRank()
	{
		SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToCar();
		}, null);
	}

	// Token: 0x06003E59 RID: 15961 RVA: 0x0011CBF8 File Offset: 0x0011ADF8
	public void OnClickItemBtn(string key, GameObject lineObj)
	{
		if (this.curChoosekey.Equals(key))
		{
			return;
		}
		this.curChoosekey = key;
		copyscene_info copyscene_info = this.DailyCopyInfoDic[key];
		this.mCurCopyScene = DataManager.GetCopySceneDataById(copyscene_info.ID);
		this.curInfo = copyscene_info;
		this.remainNum = (int)this.curInfo.CurNum;
		ShowRewardData showRewardDataByID;
		if (this.mCurCopyScene.SubType == 12 || this.mCurCopyScene.SubType == 11 || this.mCurCopyScene.SubType == 23)
		{
			string id = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Level).DorpKeyDic[this.mCurCopyScene.ShowRewardId];
			showRewardDataByID = DataManager.GetShowRewardDataByID(id);
		}
		else if (this.mCurCopyScene.SubType == 20)
		{
			string id2 = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ServerLevel).DorpKeyDic[this.mCurCopyScene.ShowRewardId];
			showRewardDataByID = DataManager.GetShowRewardDataByID(id2);
		}
		else
		{
			showRewardDataByID = DataManager.GetShowRewardDataByID(this.mCurCopyScene.ShowRewardId);
		}
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
		this.DescLabel.text = StrDictionary.GetDictionaryString(this.mCurCopyScene.Desc, new object[0]);
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if ((this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.mCurCopyScene.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurCopyScene.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		this.refershUI();
		if (TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_COPY)
		{
			if (this.mCurCopyScene.SubType == 12)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_COPY)
		{
			if (this.mCurCopyScene.SubType == 11)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_CLICK_COPY)
		{
			if (this.mCurCopyScene.SubType == 20)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_CHOOSE_COPY)
		{
			if (this.mCurCopyScene.SubType == 16)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_FINISH || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06003E5A RID: 15962 RVA: 0x0011CF88 File Offset: 0x0011B188
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBG.mainTexture = tex;
	}

	// Token: 0x06003E5B RID: 15963 RVA: 0x0011CF98 File Offset: 0x0011B198
	public void refershUI()
	{
		if (this.mCurCopyScene.SubType == 7)
		{
			UnityVersionUtil.SetActiveRecursive(this.BtnRankObj, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.BtnRankObj, false);
		}
		if (this.mCurCopyScene.CanWipeOut == 1)
		{
			if (this.curInfo.HasBestGrade)
			{
				if (this.curInfo.BestGrade >= 3L)
				{
					this.WipeOutSp.spriteName = "CZ_anNiu_1";
				}
				else
				{
					this.WipeOutSp.spriteName = "CZ_anNiu_2+";
				}
			}
			else
			{
				this.WipeOutSp.spriteName = "CZ_anNiu_2+";
			}
			if (this.CheckLevel())
			{
				this.StartSp.spriteName = "CZ_anNiu_2";
			}
			else
			{
				this.StartSp.spriteName = "CZ_anNiu_2+";
				this.WipeOutSp.spriteName = "CZ_anNiu_2+";
			}
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			int itemStackNumById = playerData.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.WipeItem);
			this.WipeOutLabel.text = string.Format("1/{0}", itemStackNumById);
			NGUITools.SetActive(this.MatchBtn.gameObject, false);
		}
		else if (this.mCurCopyScene.IsTeamCopy)
		{
			PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (!this.CheckLevel())
			{
				this.StartSp.spriteName = "CZ_anNiu_2+";
			}
			else
			{
				this.StartSp.spriteName = "CZ_anNiu_2";
			}
			NGUITools.SetActive(this.MatchBtn.gameObject, false);
			this.UpdateMatchingLabel();
		}
		else
		{
			NGUITools.SetActive(this.MatchBtn.gameObject, false);
			if (this.CheckLevel())
			{
				this.StartSp.spriteName = "CZ_anNiu_2";
			}
			else
			{
				this.StartSp.spriteName = "CZ_anNiu_2+";
			}
		}
		this.UpdataActiveInfo();
		this.UpdateSelectItem();
	}

	// Token: 0x06003E5C RID: 15964 RVA: 0x0011D188 File Offset: 0x0011B388
	public void UpdateSelectItem()
	{
		for (int i = 0; i < this.DailyCopyLineList.Count; i++)
		{
			this.DailyCopyLineList[i].RefreshLineSelect(this.curChoosekey);
		}
	}

	// Token: 0x06003E5D RID: 15965 RVA: 0x0011D1C8 File Offset: 0x0011B3C8
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardItemsScripts.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06003E5E RID: 15966 RVA: 0x0011D1D8 File Offset: 0x0011B3D8
	public void OnClickStart()
	{
		if (this.mCurCopyScene == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_FINISH && this.mCurCopyScene.SubType == 7)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_START && this.mCurCopyScene.SubType == 12)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_START && this.mCurCopyScene.SubType == 11)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START && this.mCurCopyScene.SubType == 20)
		{
			this.CheckTutorialEvent();
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_CHOOSE_COPY && this.mCurCopyScene.SubType == 16)
		{
			this.CheckTutorialEvent();
		}
		if (this.mCurCopyScene.SubType == 7 && SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.CAR_COPY))
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR_COPY);
		}
		if (!this.CheckLevel())
		{
			TutorialManager.LevelLimitAction();
			return;
		}
		if (this.mCurCopyScene.IsTeamCopy)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (playerData.IsHaveTeam() && !playerData.IsTeamLeader())
			{
				SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
				return;
			}
			if (this.remainNum <= 0)
			{
				if (!string.IsNullOrEmpty(this.mCurCopyScene.TimeInc))
				{
					PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
					int itemStackNumById = playerData2.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.TimeInc);
					if (itemStackNumById > 0)
					{
						ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurCopyScene.TimeInc);
						MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101598}", new object[]
						{
							itemDataByID.MName
						}), "#{100127}", new MessageBoxLogic.OnYesClick(this.TeamYesStartfun), null, null, null);
						return;
					}
				}
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
				return;
			}
			this.TeamStartFun();
		}
		else
		{
			if (this.mCurCopyScene.IsCarChasingCopy)
			{
				ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
				if (string.IsNullOrEmpty(mainPlayer.MountId))
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101557}", new object[0]), true, false);
					return;
				}
			}
			if (this.mCurCopyScene.IsScuffleCopy)
			{
				if (this.remainNum <= 0)
				{
					NoticeLogic.AddNotifyData("#{102046}", true, false);
					return;
				}
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
				enter_scuffle_batttle.request request = new enter_scuffle_batttle.request();
				request.ID = this.mCurCopyScene.ID;
				request.floor = 0L;
				NetLogic.GetInstance().Send<Protocol.enter_scuffle_batttle>(request, null);
				this.CheckStartFlurry();
				return;
			}
			else
			{
				if (this.mCurCopyScene.IsPVPMap)
				{
					SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
					MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(this.mCurCopyScene.MapId);
					SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.mCurCopyScene.MapId, mapInfoDataByID.BirthPosVector3, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP, null);
					this.CheckStartFlurry();
					return;
				}
				if (this.remainNum <= 0)
				{
					if (!string.IsNullOrEmpty(this.mCurCopyScene.TimeInc))
					{
						PlayerData playerData3 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
						int itemStackNumById2 = playerData3.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.TimeInc);
						if (itemStackNumById2 > 0)
						{
							ItemData itemDataByID2 = DataManager.GetItemDataByID(this.mCurCopyScene.TimeInc);
							MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101598}", new object[]
							{
								itemDataByID2.MName
							}), "#{100127}", delegate
							{
								WaitResponseUIRootLogic.OpenWaitBox(107, 10f, 0f, null);
								SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
								enter_copy_scene.request request3 = new enter_copy_scene.request();
								request3.mapInfoId = this.mCurCopyScene.ID;
								request3.reaminItem = true;
								NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request3, null);
								this.CheckStartFlurry();
							}, null, null, null);
							return;
						}
					}
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
					return;
				}
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
				enter_copy_scene.request request2 = new enter_copy_scene.request();
				request2.mapInfoId = this.mCurCopyScene.ID;
				NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request2, null);
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.SyncTimeslocal(this.mCurCopyScene.ID);
				this.CheckStartFlurry();
			}
		}
	}

	// Token: 0x06003E5F RID: 15967 RVA: 0x0011D614 File Offset: 0x0011B814
	public void CheckStartFlurry()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", this.mCurCopyScene.ID), "start");
	}

	// Token: 0x06003E60 RID: 15968 RVA: 0x0011D640 File Offset: 0x0011B840
	public void TeamYesStartfun()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer itemBackPack = playerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.REMAIN, false, PROFESSION_TYPE.INVALID);
		bool flag = false;
		GameItem gameItem = null;
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			flag = false;
		}
		else
		{
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				if (targetTypeItem[i].ItemId.Equals(this.mCurCopyScene.TimeInc))
				{
					flag = true;
					gameItem = targetTypeItem[i];
					break;
				}
			}
		}
		if (flag)
		{
			use_item.request request = new use_item.request();
			request.indexId = gameItem.IndexId;
			NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(115, 10f, 0f, null);
			this.remainNum++;
			if (this.DailyCopyInfoDic.ContainsKey(this.curInfo.ID))
			{
				this.DailyCopyInfoDic[this.curInfo.ID].CurNum += 1L;
			}
			for (int j = 0; j < this.DailyCopyLineList.Count; j++)
			{
				this.DailyCopyLineList[j].UpdateLineInfo(this.curInfo);
			}
			this.TeamStartFun();
			return;
		}
	}

	// Token: 0x06003E61 RID: 15969 RVA: 0x0011D7A4 File Offset: 0x0011B9A4
	public void TeamStartFun()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam())
		{
			if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
			}
			else
			{
				SingletonUnity<ActivityUIRootLogic>.Instance.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CreateTeamRoot, delegate
				{
					WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
					SingletonUnity<CreateTeamRootLogic>.Instance.Reset(false, this.mCurCopyScene.ID);
				}, null);
			}
		}
		else
		{
			this.OnClickMatchBtn();
		}
	}

	// Token: 0x06003E62 RID: 15970 RVA: 0x0011D850 File Offset: 0x0011BA50
	public void OnClicktishiBtn()
	{
		if (this.mCurCopyScene == null)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurCopyScene.Rule, null, new object[]
			{
				TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
			});
		}, null);
	}

	// Token: 0x06003E63 RID: 15971 RVA: 0x0011D888 File Offset: 0x0011BA88
	public void OnClickWipeOutBtn()
	{
		copyscene_info copyscene_info = this.curInfo;
		if (copyscene_info == null)
		{
			return;
		}
		if (!this.CheckLevel())
		{
			TutorialManager.LevelLimitAction();
			return;
		}
		if (copyscene_info.BestGrade < 3L)
		{
			NoticeLogic.AddNotifyData("#{101574}", true, false);
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int itemStackNumById = playerData.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.WipeItem);
		if (itemStackNumById <= 0)
		{
			NoticeLogic.AddNotifyData("#{101543}", true, false);
			GameMoneyHelper.ShowItemProduct(this.mCurCopyScene.WipeItem, GameDefine.SHOP_TYPE.TOOL_SHOP);
			return;
		}
		if (this.remainNum <= 0)
		{
			if (!string.IsNullOrEmpty(this.mCurCopyScene.TimeInc))
			{
				PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				int itemStackNumById2 = playerData2.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.TimeInc);
				if (itemStackNumById2 > 0)
				{
					ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurCopyScene.TimeInc);
					MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101598}", new object[]
					{
						itemDataByID.MName
					}), "#{100127}", delegate
					{
						WaitResponseUIRootLogic.OpenWaitBox(232, 10f, 0f, null);
						copy_swipe_out.request request2 = new copy_swipe_out.request();
						request2.copyInfoId = this.mCurCopyScene.ID;
						request2.reaminItem = true;
						NetLogic.GetInstance().Send<Protocol.copy_swipe_out>(request2, null);
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", this.mCurCopyScene.ID), "wipe_out");
					}, null, null, null);
					return;
				}
			}
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
			return;
		}
		WaitResponseUIRootLogic.OpenWaitBox(232, 10f, 0f, null);
		copy_swipe_out.request request = new copy_swipe_out.request();
		request.copyInfoId = this.mCurCopyScene.ID;
		NetLogic.GetInstance().Send<Protocol.copy_swipe_out>(request, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", this.mCurCopyScene.ID), "wipe_out");
		this.wipeoutUpdate();
	}

	// Token: 0x06003E64 RID: 15972 RVA: 0x0011DA2C File Offset: 0x0011BC2C
	public void wipeoutUpdate()
	{
		if (this.mCurCopyScene.SubType == 16)
		{
			for (int i = 0; i < this.equipList.Count; i++)
			{
				if (this.DailyCopyInfoDic.ContainsKey(this.equipList[i].ID))
				{
					this.DailyCopyInfoDic[this.equipList[i].ID].CurNum -= 1L;
				}
			}
			this.remainNum--;
			this.refershUI();
			this.DailyCopyLineList[this.equipcopy_index].RefershParentLine(this.curInfo);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.UpdateTips();
		}
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x0011DAF8 File Offset: 0x0011BCF8
	public void UpdateWipeoutItem(string item_id)
	{
		if (this.mCurCopyScene.CanWipeOut == 1 && item_id.Equals(this.mCurCopyScene.WipeItem))
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			int itemStackNumById = playerData.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.WipeItem);
			this.WipeOutLabel.text = string.Format("1/{0}", itemStackNumById);
		}
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x0011DB6C File Offset: 0x0011BD6C
	public void OnClickMatchBtn()
	{
		if (!this.CheckLevel())
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam() && !playerData.IsTeamLeader())
		{
			NoticeLogic.AddNotifyData("#{102008}", true, false);
			return;
		}
		if (this.remainNum <= 0)
		{
			if (!string.IsNullOrEmpty(this.mCurCopyScene.TimeInc))
			{
				PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				int itemStackNumById = playerData2.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.TimeInc);
				if (itemStackNumById > 0)
				{
					ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurCopyScene.TimeInc);
					MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101598}", new object[]
					{
						itemDataByID.MName
					}), "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickYesUseitemMatch), null, null, null);
					return;
				}
			}
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
			return;
		}
		this.MatchFun();
	}

	// Token: 0x06003E67 RID: 15975 RVA: 0x0011DC78 File Offset: 0x0011BE78
	public void OnClickYesUseitemMatch()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer itemBackPack = playerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.REMAIN, false, PROFESSION_TYPE.INVALID);
		bool flag = false;
		GameItem gameItem = null;
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			flag = false;
		}
		else
		{
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				if (targetTypeItem[i].ItemId.Equals(this.mCurCopyScene.TimeInc))
				{
					flag = true;
					gameItem = targetTypeItem[i];
					break;
				}
			}
		}
		if (flag)
		{
			use_item.request request = new use_item.request();
			request.indexId = gameItem.IndexId;
			NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(115, 10f, 0f, null);
			this.remainNum++;
			if (this.DailyCopyInfoDic.ContainsKey(this.curInfo.ID))
			{
				this.DailyCopyInfoDic[this.curInfo.ID].CurNum += 1L;
			}
			for (int j = 0; j < this.DailyCopyLineList.Count; j++)
			{
				this.DailyCopyLineList[j].UpdateLineInfo(this.curInfo);
			}
			this.MatchFun();
			return;
		}
	}

	// Token: 0x06003E68 RID: 15976 RVA: 0x0011DDDC File Offset: 0x0011BFDC
	public void MatchFun()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		TeamData teamDataDataByID = DataManager.GetTeamDataDataByID(this.mCurCopyScene.ID);
		if (playerData.IsTeamLeader())
		{
			if (playerData.TeamInfo.IsVertify)
			{
				if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
				{
					stop_random_select_team.request request = new stop_random_select_team.request();
					request.id = playerData.TeamInfo.TeamGoalData.ID;
					request.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
					NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request, null);
				}
				else
				{
					WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SearchTeamRoot, delegate
					{
						SingletonUnity<SearchTeamRootLogic>.Instance.Reset(this.mCurCopyScene.ID);
					}, null);
				}
			}
			else if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				random_select_team.request request2 = new random_select_team.request();
				request2.id = this.mCurCopyScene.ID;
				request2.type1 = (long)teamDataDataByID.GoalType;
				NetLogic.GetInstance().Send<Protocol.random_select_team>(request2, null);
			}
			else
			{
				WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SearchTeamRoot, delegate
				{
					SingletonUnity<SearchTeamRootLogic>.Instance.Reset(this.mCurCopyScene.ID);
				}, null);
			}
		}
		else if (!playerData.IsHaveTeam())
		{
			random_select_team.request request3 = new random_select_team.request();
			request3.id = this.mCurCopyScene.ID;
			request3.type1 = (long)teamDataDataByID.GoalType;
			NetLogic.GetInstance().Send<Protocol.random_select_team>(request3, null);
		}
	}

	// Token: 0x06003E69 RID: 15977 RVA: 0x0011DFA8 File Offset: 0x0011C1A8
	public void UpdateMatchingLabel()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsTeamLeader())
		{
			if (playerData.TeamInfo.IsVertify)
			{
				if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
				{
					this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
				}
				else
				{
					this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101503}", new object[0]);
				}
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101503}", new object[0]);
			}
		}
		else if (playerData.IsHaveTeam())
		{
			if (playerData.TeamInfo.IsVertify && this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101503}", new object[0]);
			}
		}
		else
		{
			this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101503}", new object[0]);
		}
	}

	// Token: 0x06003E6A RID: 15978 RVA: 0x0011E0FC File Offset: 0x0011C2FC
	private void OnDisable()
	{
		if (SingletonUnity<UIManager>.Exists)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyRewardNewRoot);
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_FINISH || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x040029F8 RID: 10744
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040029F9 RID: 10745
	public List<DailyCopyLineLogic> DailyCopyLineList = new List<DailyCopyLineLogic>();

	// Token: 0x040029FA RID: 10746
	public UITexture CopyBG;

	// Token: 0x040029FB RID: 10747
	public ShowRewardItems ShowRewardItemsScripts;

	// Token: 0x040029FC RID: 10748
	public UILabel DescLabel;

	// Token: 0x040029FD RID: 10749
	public UITable RootTable;

	// Token: 0x040029FE RID: 10750
	public UIScrollView uiScrollView;

	// Token: 0x040029FF RID: 10751
	public UISprite WipeOutSp;

	// Token: 0x04002A00 RID: 10752
	public UISprite StartSp;

	// Token: 0x04002A01 RID: 10753
	public UISprite MatchSp;

	// Token: 0x04002A02 RID: 10754
	public GameObject WipeOutBtn;

	// Token: 0x04002A03 RID: 10755
	public GameObject MatchBtn;

	// Token: 0x04002A04 RID: 10756
	public UILabel WipeOutLabel;

	// Token: 0x04002A05 RID: 10757
	public UILabel MatchBtnLabel;

	// Token: 0x04002A06 RID: 10758
	public GameObject BtnRankObj;

	// Token: 0x04002A07 RID: 10759
	private List<copyscene_info> list;

	// Token: 0x04002A08 RID: 10760
	private List<copyscene_info> equipList = new List<copyscene_info>();

	// Token: 0x04002A09 RID: 10761
	private List<copyscene_info> pklist = new List<copyscene_info>();

	// Token: 0x04002A0A RID: 10762
	private Dictionary<string, copyscene_info> DailyCopyInfoDic;

	// Token: 0x04002A0B RID: 10763
	private copyscene_info curInfo;

	// Token: 0x04002A0C RID: 10764
	private int remainNum;

	// Token: 0x04002A0D RID: 10765
	private CopySceneData mCurCopyScene;

	// Token: 0x04002A0E RID: 10766
	private string curChoosekey = string.Empty;

	// Token: 0x04002A0F RID: 10767
	private int equipcopy_index;

	// Token: 0x04002A10 RID: 10768
	public GameObject ScoreObj;

	// Token: 0x04002A11 RID: 10769
	public UILabel ScoreLabel;

	// Token: 0x04002A12 RID: 10770
	private List<DailyActiveData> DailyActiveDataList = new List<DailyActiveData>();

	// Token: 0x04002A13 RID: 10771
	private Dictionary<string, daily_active> activeDic = new Dictionary<string, daily_active>();

	// Token: 0x04002A14 RID: 10772
	public int TargetTypeId;
}
