using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A2B RID: 2603
public class NewDailyCopyUIRootLogic : SingletonUnity<NewDailyCopyUIRootLogic>
{
	// Token: 0x06004BA7 RID: 19367 RVA: 0x001959C0 File Offset: 0x00193BC0
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004BA8 RID: 19368 RVA: 0x001959CC File Offset: 0x00193BCC
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004BA9 RID: 19369 RVA: 0x001959FC File Offset: 0x00193BFC
	private new void Awake()
	{
		base.Awake();
		this.ActivityInfoPage.RegisterStartBtn(new DelegateDefine.NoParamDelegate(this.OnClickStart));
		this.ActivityInfoPage.RegisterRankBtn(new DelegateDefine.NoParamDelegate(this.OnClickRankBtn));
		this.ActivityInfoPage.RegisterMatchBtn(new DelegateDefine.NoParamDelegate(this.OnClickMatchBtn));
		this.ActivityInfoPage.RegisterCloseBtn(new DelegateDefine.NoParamDelegate(this.ShowAcitvityLine));
	}

	// Token: 0x06004BAA RID: 19370 RVA: 0x00195A6C File Offset: 0x00193C6C
	public void ShowAcitvityLine()
	{
		this.IsShowActivityInfo = false;
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, false);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, true);
		this.RefreshCopyPage();
		this.ResfershTowerInfo();
	}

	// Token: 0x06004BAB RID: 19371 RVA: 0x00195AB0 File Offset: 0x00193CB0
	public void ShowActivityInfo(copyscene_info info)
	{
		this.IsShowActivityInfo = true;
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, true);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, false);
		this.ActivityInfoPage.UpdateUI(info);
	}

	// Token: 0x06004BAC RID: 19372 RVA: 0x00195AF4 File Offset: 0x00193CF4
	public void EnableReset()
	{
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, true);
		for (int i = 0; i < this.DailyCopyLineList.Count; i++)
		{
			NGUITools.SetActive(this.DailyCopyLineList[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.TowerLineItem.gameObject, false);
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, false);
		NGUITools.SetActive(this.SexMiniGameItem.gameObject, false);
		this.IsShowActivityInfo = false;
		this.TargetTypeId = -1;
		this.TargetActId = string.Empty;
		this.mCurCopyScene = null;
		this.mPlayerTowerInfo = null;
		this.curChoosekey = string.Empty;
		this.TargetActType = GameDefine.ACTIVITY_TYPE.INVALID;
	}

	// Token: 0x06004BAD RID: 19373 RVA: 0x00195BB4 File Offset: 0x00193DB4
	public void RefreshCopyPage()
	{
		if (this.IsShowActivityInfo)
		{
			this.UpdateRefershCopy();
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.list = new List<copyscene_info>(playerData.CopyInfoData.DailyCopyInfoList);
		this.DailyCopyInfoDic = playerData.CopyInfoData.DailyCopyInfoDic;
		this.equipList.Clear();
		this.pklist.Clear();
		this.ExpList.Clear();
		this.curChoosekey = string.Empty;
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
				this.list.RemoveAt(i);
			}
			else if (copySceneDataById.SubType == 12)
			{
				this.ExpList.Add(this.list[i]);
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
		this.ExpList.Sort(delegate(copyscene_info x, copyscene_info y)
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
		this.ExpCopy_index = this.list.Count;
		this.equipcopy_index = this.list.Count + 1;
		int num3 = -1;
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
					this.DailyCopyLineList[k].ResetItem(string.Empty, "#{101511}", this.ExpList, new DelegateDefine.StringGameObjectDelegate(this.OnClickItemBtn), true);
					if (this.TargetTypeId == 12)
					{
						num3 = this.ExpCopy_index;
					}
				}
				else if (k == this.list.Count + 1)
				{
					this.DailyCopyLineList[k].ResetItem(string.Empty, "#{101510}", this.equipList, new DelegateDefine.StringGameObjectDelegate(this.OnClickItemBtn), true);
					if (this.TargetTypeId == 16)
					{
						num3 = this.equipcopy_index;
					}
				}
			}
		}
		this.ResetSexMinigameInfo();
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		if (num3 != -1)
		{
			this.DailyCopyLineList[num3].OnClickSelectItemBtn(this.TargetActId);
			this.TargetTypeId = -1;
			this.TargetActId = string.Empty;
			this.TargetActType = GameDefine.ACTIVITY_TYPE.INVALID;
		}
		else if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_WAIT_DATA || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004BAE RID: 19374 RVA: 0x001960B4 File Offset: 0x001942B4
	public void UpdateRefershCopy()
	{
		this.ActivityInfoPage.refershInfo();
	}

	// Token: 0x06004BAF RID: 19375 RVA: 0x001960C4 File Offset: 0x001942C4
	public void ResetTowerInfo(ret_request_tower_copy_info.request request)
	{
		if (request.HasTower_info)
		{
			this.mPlayerTowerInfo = request.tower_info;
		}
		if (request.HasTower_special_reward)
		{
			this.mPlayerSpecialRewardList = request.tower_special_reward;
			this.mPlayerSpecialRewardList.Sort((tower_special_reward x, tower_special_reward y) => (int)(x.floor - y.floor));
		}
		if (UnityVersionUtil.IsActive(this.ActivityLineRoot.gameObject))
		{
			if (this.mPlayerTowerInfo != null)
			{
				NGUITools.SetActive(this.TowerLineItem.gameObject, true);
				this.TowerLineItem.SetTowerInfo(this.mPlayerTowerInfo, new DelegateDefine.StringGameObjectDelegate(this.OnClickTowerItemBtn));
			}
			else
			{
				NGUITools.SetActive(this.TowerLineItem.gameObject, false);
			}
			this.RootTable.Reposition();
			this.uiScrollView.ResetPosition();
			if (this.TargetActType == GameDefine.ACTIVITY_TYPE.TOWER)
			{
				this.TowerLineItem.OnClikcItemBtn();
				this.TargetTypeId = -1;
				this.TargetActId = string.Empty;
				this.TargetActType = GameDefine.ACTIVITY_TYPE.INVALID;
			}
			if (TutorialManager.CurStep == TUTORIAL_STEP.TOWER_WAIT_DATA)
			{
				this.CheckTutorialEvent();
			}
		}
		if (UnityVersionUtil.IsActive(this.ActivityInfoPage.gameObject) && this.ActivityInfoPage.mPlayerTowerInfo != null)
		{
			this.ActivityInfoPage.UpdateTowerUI(this.mPlayerTowerInfo, this.mPlayerSpecialRewardList, false);
		}
	}

	// Token: 0x06004BB0 RID: 19376 RVA: 0x00196224 File Offset: 0x00194424
	public void ResetSexMinigameInfo()
	{
		this.CurSexMiniGameData = DataManager.GetSexMiniDataById(GameDefine.SEX_MINI_GAME_ID);
		if (this.CurSexMiniGameData == null)
		{
			NGUITools.SetActive(this.SexMiniGameItem.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.SexMiniGameItem.gameObject, true);
			this.SexMiniGameItem.SetSexGameInfo(this.CurSexMiniGameData, new DelegateDefine.StringGameObjectDelegate(this.OnClickSexGameBtn));
		}
		if (this.TargetTypeId == 27)
		{
			this.TargetTypeId = -1;
			this.TargetActId = string.Empty;
			this.TargetActType = GameDefine.ACTIVITY_TYPE.INVALID;
			this.SexMiniGameItem.OnClikcItemBtn();
		}
	}

	// Token: 0x06004BB1 RID: 19377 RVA: 0x001962C4 File Offset: 0x001944C4
	public void UpdateMatchingLabel()
	{
		if (UnityVersionUtil.IsActive(this.ActivityInfoPage.gameObject))
		{
			this.ActivityInfoPage.UpdateMatchingLabel();
		}
	}

	// Token: 0x06004BB2 RID: 19378 RVA: 0x001962F4 File Offset: 0x001944F4
	public void ResetTowerInfo(ret_grant_tower_reward.request request)
	{
		if (request.HasTower_info)
		{
			this.mPlayerTowerInfo = request.tower_info;
		}
		if (request.HasTower_special_reward)
		{
			this.mPlayerSpecialRewardList = request.tower_special_reward;
			this.mPlayerSpecialRewardList.Sort((tower_special_reward x, tower_special_reward y) => (int)(x.floor - y.floor));
		}
		if (UnityVersionUtil.IsActive(this.ActivityLineRoot.gameObject))
		{
			if (this.mPlayerTowerInfo != null)
			{
				NGUITools.SetActive(this.TowerLineItem.gameObject, true);
				this.TowerLineItem.SetTowerInfo(this.mPlayerTowerInfo, new DelegateDefine.StringGameObjectDelegate(this.OnClickTowerItemBtn));
			}
			else
			{
				NGUITools.SetActive(this.TowerLineItem.gameObject, false);
			}
			this.RootTable.Reposition();
			this.uiScrollView.ResetPosition();
		}
		if (UnityVersionUtil.IsActive(this.ActivityInfoPage.gameObject) && this.ActivityInfoPage.mPlayerTowerInfo != null)
		{
			this.ActivityInfoPage.UpdateTowerUI(this.mPlayerTowerInfo, this.mPlayerSpecialRewardList, false);
		}
	}

	// Token: 0x06004BB3 RID: 19379 RVA: 0x00196410 File Offset: 0x00194610
	public void ResfershTowerInfo()
	{
		this.mPlayerTowerInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TowerData.PlayerTowerInfo;
		if (this.mPlayerTowerInfo != null)
		{
			NGUITools.SetActive(this.TowerLineItem.gameObject, true);
			this.TowerLineItem.SetTowerInfo(this.mPlayerTowerInfo, new DelegateDefine.StringGameObjectDelegate(this.OnClickTowerItemBtn));
		}
		else
		{
			NGUITools.SetActive(this.TowerLineItem.gameObject, false);
		}
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
	}

	// Token: 0x06004BB4 RID: 19380 RVA: 0x0019649C File Offset: 0x0019469C
	public void ResetTowerInfo(tower_info towerInfo)
	{
		this.mPlayerTowerInfo = towerInfo;
		if (UnityVersionUtil.IsActive(this.ActivityLineRoot.gameObject))
		{
			if (this.mPlayerTowerInfo != null)
			{
				NGUITools.SetActive(this.TowerLineItem.gameObject, true);
				this.TowerLineItem.SetTowerInfo(this.mPlayerTowerInfo, new DelegateDefine.StringGameObjectDelegate(this.OnClickTowerItemBtn));
			}
			else
			{
				NGUITools.SetActive(this.TowerLineItem.gameObject, false);
			}
			this.RootTable.Reposition();
			this.uiScrollView.ResetPosition();
		}
		if (UnityVersionUtil.IsActive(this.ActivityInfoPage.gameObject) && this.ActivityInfoPage.mPlayerTowerInfo != null)
		{
			this.ActivityInfoPage.ResetTowerInfo(this.mPlayerTowerInfo);
		}
	}

	// Token: 0x06004BB5 RID: 19381 RVA: 0x00196560 File Offset: 0x00194760
	public bool CheckLevel()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.mCurCopyScene.MinLevel);
	}

	// Token: 0x06004BB6 RID: 19382 RVA: 0x0019657C File Offset: 0x0019477C
	public void ClickTargetDailyCopy(MAPTYPE type, string actid)
	{
		int num = -1;
		if (type == MAPTYPE.EQUIP_COPY)
		{
			num = this.equipcopy_index;
		}
		if (num != -1)
		{
			this.DailyCopyLineList[num].ClickTargetBtn(actid);
		}
	}

	// Token: 0x06004BB7 RID: 19383 RVA: 0x001965B4 File Offset: 0x001947B4
	public void ClickTargetDailyCopy(MAPTYPE type)
	{
		for (int i = 0; i < this.DailyCopyLineList.Count; i++)
		{
			if (!string.IsNullOrEmpty(this.DailyCopyLineList[i].Key))
			{
				if (DataManager.GetCopySceneDataById(this.DailyCopyLineList[i].Key).SubType == (int)type)
				{
					this.DailyCopyLineList[i].OnClikcItemBtn();
					break;
				}
			}
		}
		if (type == MAPTYPE.SEX_GAME)
		{
			this.SexMiniGameItem.OnClikcItemBtn();
		}
	}

	// Token: 0x06004BB8 RID: 19384 RVA: 0x00196648 File Offset: 0x00194848
	public void ClickTargetType(GameDefine.ACTIVITY_TYPE clicktype)
	{
		if (clicktype == GameDefine.ACTIVITY_TYPE.TOWER)
		{
			this.TowerLineItem.OnClikcItemBtn();
		}
	}

	// Token: 0x06004BB9 RID: 19385 RVA: 0x00196660 File Offset: 0x00194860
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
		this.ShowActivityInfo(this.curInfo);
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_CLICK_COPY)
		{
			if (this.mCurCopyScene.SubType == 7)
			{
				this.CheckTutorialEvent();
			}
		}
		else if (TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_COPY)
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
	}

	// Token: 0x06004BBA RID: 19386 RVA: 0x00196798 File Offset: 0x00194998
	public void UpdateSelectItem()
	{
		for (int i = 0; i < this.DailyCopyLineList.Count; i++)
		{
			this.DailyCopyLineList[i].RefreshLineSelect(this.curChoosekey);
		}
	}

	// Token: 0x06004BBB RID: 19387 RVA: 0x001967D8 File Offset: 0x001949D8
	public void OnClickRankBtn()
	{
		SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			if (this.mCurCopyScene != null && this.mCurCopyScene.IsCarChasingCopy)
			{
				SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToCar();
			}
			else if (this.CurSexMiniGameData != null)
			{
				SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToSexMini();
			}
		}, null);
	}

	// Token: 0x06004BBC RID: 19388 RVA: 0x0019680C File Offset: 0x00194A0C
	public void OnClickStart()
	{
		if (this.mCurCopyScene == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_CLICK_START && this.mCurCopyScene.SubType == 7)
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
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
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
				return;
			}
			if (this.mCurCopyScene.IsSingleDance)
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.mCurCopyScene.MapId, Vector3.right * -5f, AUTO_SEARCH_PARTH_FINISHEVENT.CITY_DANCE, null);
				this.CheckStartFlurry();
				return;
			}
			if (this.mCurCopyScene.IsPVPMap)
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
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
							enter_copy_scene.request request2 = new enter_copy_scene.request();
							request2.mapInfoId = this.mCurCopyScene.ID;
							request2.reaminItem = true;
							NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request2, null);
							this.CheckStartFlurry();
						}, null, null, null);
						return;
					}
					if (this.mCurCopyScene.IsCashCopy || this.mCurCopyScene.IsExpCopy || this.mCurCopyScene.IsCarChasingCopy)
					{
						GameMoneyHelper.ShowItemProduct(this.mCurCopyScene.TimeInc, GameDefine.SHOP_TYPE.TOOL_SHOP);
					}
				}
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
				return;
			}
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
			enter_copy_scene.request request = new enter_copy_scene.request();
			request.mapInfoId = this.mCurCopyScene.ID;
			NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request, null);
			this.CarTimesCheck();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.SyncTimeslocal(this.mCurCopyScene.ID);
			this.CheckStartFlurry();
		}
	}

	// Token: 0x06004BBD RID: 19389 RVA: 0x00196C7C File Offset: 0x00194E7C
	public void CheckStartFlurry()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", this.mCurCopyScene.ID), "start");
	}

	// Token: 0x06004BBE RID: 19390 RVA: 0x00196CA8 File Offset: 0x00194EA8
	public void CarTimesCheck()
	{
		if (this.mCurCopyScene.IsCarChasingCopy)
		{
			if (this.DailyCopyInfoDic.ContainsKey(this.mCurCopyScene.ID) && this.DailyCopyInfoDic[this.mCurCopyScene.ID].CurNum > 0L)
			{
				this.DailyCopyInfoDic[this.mCurCopyScene.ID].CurNum -= 1L;
			}
			if (this.remainNum > 0)
			{
				this.remainNum--;
			}
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.UpdateTips();
		}
	}

	// Token: 0x06004BBF RID: 19391 RVA: 0x00196D54 File Offset: 0x00194F54
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

	// Token: 0x06004BC0 RID: 19392 RVA: 0x00196EB8 File Offset: 0x001950B8
	public void TeamStartFun()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam())
		{
			if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
			}
			else
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
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

	// Token: 0x06004BC1 RID: 19393 RVA: 0x00196F64 File Offset: 0x00195164
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

	// Token: 0x06004BC2 RID: 19394 RVA: 0x00197108 File Offset: 0x00195308
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
			this.ActivityInfoPage.UpdateUI(this.curInfo);
			this.DailyCopyLineList[this.equipcopy_index].RefershParentLine(this.curInfo);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.UpdateTips();
		}
	}

	// Token: 0x06004BC3 RID: 19395 RVA: 0x001971E0 File Offset: 0x001953E0
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

	// Token: 0x06004BC4 RID: 19396 RVA: 0x001972EC File Offset: 0x001954EC
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

	// Token: 0x06004BC5 RID: 19397 RVA: 0x00197450 File Offset: 0x00195650
	public void MatchFun()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		TeamData curCopyTeamData = DataManager.GetTeamDataDataByID(this.mCurCopyScene.ID);
		if (curCopyTeamData == null)
		{
			return;
		}
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
					MessageBoxLogic.OpenOKCancelBox("#{103204}", "#{100127}", delegate
					{
						req_change_team_goal.request request5 = new req_change_team_goal.request();
						request5.goalId = this.mCurCopyScene.ID;
						request5.minLevel = (long)this.mCurCopyScene.MinLevel;
						request5.maxLevel = (long)this.mCurCopyScene.MaxLevel;
						request5.isVerfiy = ((!playerData.TeamInfo.IsVertify) ? 1L : 0L);
						NetLogic.GetInstance().Send<Protocol.req_change_team_goal>(request5, null);
						random_select_team.request request6 = new random_select_team.request();
						request6.id = this.mCurCopyScene.ID;
						request6.type1 = (long)curCopyTeamData.GoalType;
						NetLogic.GetInstance().Send<Protocol.random_select_team>(request6, null);
					}, null, null, null);
				}
			}
			else if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				random_select_team.request request2 = new random_select_team.request();
				request2.id = this.mCurCopyScene.ID;
				request2.type1 = (long)curCopyTeamData.GoalType;
				NetLogic.GetInstance().Send<Protocol.random_select_team>(request2, null);
			}
			else
			{
				MessageBoxLogic.OpenOKCancelBox("#{103204}", "#{100127}", delegate
				{
					req_change_team_goal.request request5 = new req_change_team_goal.request();
					request5.goalId = this.mCurCopyScene.ID;
					request5.minLevel = (long)this.mCurCopyScene.MinLevel;
					request5.maxLevel = (long)this.mCurCopyScene.MaxLevel;
					request5.isVerfiy = ((!playerData.TeamInfo.IsVertify) ? 1L : 0L);
					NetLogic.GetInstance().Send<Protocol.req_change_team_goal>(request5, null);
					random_select_team.request request6 = new random_select_team.request();
					request6.id = this.mCurCopyScene.ID;
					request6.type1 = (long)curCopyTeamData.GoalType;
					NetLogic.GetInstance().Send<Protocol.random_select_team>(request6, null);
				}, null, null, null);
			}
		}
		else if (!playerData.IsHaveTeam())
		{
			if (playerData.TeamInfo.IsVertify)
			{
				if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
				{
					stop_random_select_team.request request3 = new stop_random_select_team.request();
					request3.id = playerData.TeamInfo.TeamGoalData.ID;
					request3.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
					NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request3, null);
				}
				else
				{
					MessageBoxLogic.OpenOKCancelBox("#{103204}", "#{100127}", delegate
					{
						stop_random_select_team.request request5 = new stop_random_select_team.request();
						request5.id = playerData.TeamInfo.TeamGoalData.ID;
						request5.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
						NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request5, null);
						random_select_team.request request6 = new random_select_team.request();
						request6.id = this.mCurCopyScene.ID;
						request6.type1 = (long)curCopyTeamData.GoalType;
						NetLogic.GetInstance().Send<Protocol.random_select_team>(request6, null);
					}, null, null, null);
				}
			}
			else
			{
				random_select_team.request request4 = new random_select_team.request();
				request4.id = this.mCurCopyScene.ID;
				request4.type1 = (long)curCopyTeamData.GoalType;
				NetLogic.GetInstance().Send<Protocol.random_select_team>(request4, null);
			}
		}
		this.UpdateMatchingLabel();
	}

	// Token: 0x06004BC6 RID: 19398 RVA: 0x001976F4 File Offset: 0x001958F4
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_FINISH || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004BC7 RID: 19399 RVA: 0x00197748 File Offset: 0x00195948
	public void OnClickTowerItemBtn(string key, GameObject lineObj)
	{
		this.curChoosekey = key;
		this.mCurCopyScene = null;
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, true);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, false);
		this.ActivityInfoPage.UpdateTowerUI(this.mPlayerTowerInfo, this.mPlayerSpecialRewardList, true);
		if (TutorialManager.CurStep == TUTORIAL_STEP.TOWER_CLICK_COPY)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004BC8 RID: 19400 RVA: 0x001977B0 File Offset: 0x001959B0
	public void OnClickSexGameBtn(string key, GameObject lineObj)
	{
		this.curChoosekey = key;
		this.mCurCopyScene = null;
		NGUITools.SetActive(this.ActivityInfoPage.gameObject, true);
		NGUITools.SetActive(this.ActivityLineRoot.gameObject, false);
		this.ActivityInfoPage.UpdateSexGameUI(this.CurSexMiniGameData);
	}

	// Token: 0x0400395D RID: 14685
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400395E RID: 14686
	public List<DailyCopyLineLogic> DailyCopyLineList = new List<DailyCopyLineLogic>();

	// Token: 0x0400395F RID: 14687
	public UITable RootTable;

	// Token: 0x04003960 RID: 14688
	public UIScrollView uiScrollView;

	// Token: 0x04003961 RID: 14689
	private List<copyscene_info> list;

	// Token: 0x04003962 RID: 14690
	private List<copyscene_info> equipList = new List<copyscene_info>();

	// Token: 0x04003963 RID: 14691
	private List<copyscene_info> pklist = new List<copyscene_info>();

	// Token: 0x04003964 RID: 14692
	private List<copyscene_info> ExpList = new List<copyscene_info>();

	// Token: 0x04003965 RID: 14693
	private Dictionary<string, copyscene_info> DailyCopyInfoDic;

	// Token: 0x04003966 RID: 14694
	private copyscene_info curInfo;

	// Token: 0x04003967 RID: 14695
	private int remainNum;

	// Token: 0x04003968 RID: 14696
	private CopySceneData mCurCopyScene;

	// Token: 0x04003969 RID: 14697
	private string curChoosekey = string.Empty;

	// Token: 0x0400396A RID: 14698
	private int equipcopy_index;

	// Token: 0x0400396B RID: 14699
	private int ExpCopy_index;

	// Token: 0x0400396C RID: 14700
	public NewActivityInfoRootLogic ActivityInfoPage;

	// Token: 0x0400396D RID: 14701
	public GameObject ActivityLineRoot;

	// Token: 0x0400396E RID: 14702
	public int TargetTypeId;

	// Token: 0x0400396F RID: 14703
	public string TargetActId;

	// Token: 0x04003970 RID: 14704
	public GameDefine.ACTIVITY_TYPE TargetActType = GameDefine.ACTIVITY_TYPE.INVALID;

	// Token: 0x04003971 RID: 14705
	public tower_info mPlayerTowerInfo;

	// Token: 0x04003972 RID: 14706
	public DailyCopyLineLogic TowerLineItem;

	// Token: 0x04003973 RID: 14707
	private List<tower_special_reward> mPlayerSpecialRewardList = new List<tower_special_reward>();

	// Token: 0x04003974 RID: 14708
	public DailyCopyLineLogic SexMiniGameItem;

	// Token: 0x04003975 RID: 14709
	private SexMiniData CurSexMiniGameData;

	// Token: 0x04003976 RID: 14710
	private bool IsShowActivityInfo;
}
