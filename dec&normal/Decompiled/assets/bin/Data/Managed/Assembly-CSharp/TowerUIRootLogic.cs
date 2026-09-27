using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008F9 RID: 2297
public class TowerUIRootLogic : SingletonUnity<TowerUIRootLogic>
{
	// Token: 0x06003E8B RID: 16011 RVA: 0x0011EC30 File Offset: 0x0011CE30
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003E8C RID: 16012 RVA: 0x0011EC3C File Offset: 0x0011CE3C
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x17000F87 RID: 3975
	// (get) Token: 0x06003E8D RID: 16013 RVA: 0x0011EC6C File Offset: 0x0011CE6C
	public tower_info PlayerTowerInfo
	{
		get
		{
			return this.mPlayerTowerInfo;
		}
	}

	// Token: 0x06003E8E RID: 16014 RVA: 0x0011EC74 File Offset: 0x0011CE74
	protected override void Awake()
	{
		base.Awake();
		this.Init();
		this.RemainTimeStr = StrDictionary.GetDictionaryString("#{101580}", new object[0]);
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x0011ECA4 File Offset: 0x0011CEA4
	private void Init()
	{
		for (int i = 0; i < this.FloorBtnList.Count; i++)
		{
			this.FloorBtnList[i].Init(new DelegateDefine.TwoIntParamDelegate(this.OnClickFloorBtn), i);
		}
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeFloorItem));
		TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(0);
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if ((this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(towerDataByFloorID.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(towerDataByFloorID.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003E90 RID: 16016 RVA: 0x0011EDD0 File Offset: 0x0011CFD0
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBG.mainTexture = tex;
	}

	// Token: 0x06003E91 RID: 16017 RVA: 0x0011EDE0 File Offset: 0x0011CFE0
	public void EnableReset()
	{
		for (int i = 0; i < this.FloorBtnList.Count; i++)
		{
			NGUITools.SetActive(this.FloorBtnList[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.curShowRewardItemScripts.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RewardTipPic.gameObject, false);
		this.mPlayerTowerInfo = null;
		this.WipeoutTipsPic.enabled = false;
	}

	// Token: 0x06003E92 RID: 16018 RVA: 0x0011EE58 File Offset: 0x0011D058
	private void OnInitializeFloorItem(GameObject obj, int index, int realIndex)
	{
		TowerFloorInfoLogic towerFloorInfoLogic = this.FloorBtnList[index];
		realIndex = Mathf.Abs(realIndex);
		bool isShow = false;
		bool isEnable = false;
		if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
		{
			for (int i = 0; i < this.mPlayerSpecialRewardList.Count; i++)
			{
				if (this.mPlayerSpecialRewardList[i].floor == (long)realIndex)
				{
					isShow = (this.mPlayerSpecialRewardList[i].state == 0L);
					isEnable = (this.mPlayerSpecialRewardList[i].state == 0L && this.mPlayerSpecialRewardList[i].floor <= this.mPlayerTowerInfo.floor);
					break;
				}
			}
		}
		else
		{
			isShow = false;
			isEnable = false;
		}
		towerFloorInfoLogic.Reset(realIndex, this.mPlayerTowerInfo != null && realIndex == this.mCurChoosedFloorIndex, (this.mPlayerTowerInfo == null) ? -1 : ((int)this.mPlayerTowerInfo.cur_floor), isShow, isEnable);
	}

	// Token: 0x06003E93 RID: 16019 RVA: 0x0011EF7C File Offset: 0x0011D17C
	public void UpdateTowerInfo(tower_info info)
	{
		this.mPlayerTowerInfo = info;
		this.mReceiveServertime = Time.time;
		this.MAX_FLOOR_NUM = (int)Mathf.Min((float)(this.mPlayerTowerInfo.floor + 20L), (float)this.mPlayerTowerInfo.max_floor);
		this.uiWrapContent.maxIndex = 0;
		this.uiWrapContent.minIndex = -this.MAX_FLOOR_NUM + 1;
		this.WrapContentBottomWidget.height = this.MAX_FLOOR_NUM * this.uiWrapContent.itemSize;
		this.cangetSpecial = false;
		if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
		{
			for (int i = 0; i < this.mPlayerSpecialRewardList.Count; i++)
			{
				if (this.mPlayerSpecialRewardList[i].state == 0L)
				{
					if (i == 0 && this.mPlayerSpecialRewardList[i].floor <= this.mPlayerTowerInfo.floor)
					{
						TutorialManager.ShowTutorial(TUTORIAL_STEP.TOWER_SPECIAL_TIPS);
					}
					this.mRewardFloorIndex = (int)this.mPlayerSpecialRewardList[i].floor;
					NGUITools.SetActive(this.RewardTipPic.gameObject, this.mPlayerSpecialRewardList[i].floor <= this.mPlayerTowerInfo.floor);
					this.cangetSpecial = true;
					break;
				}
			}
		}
		if (!this.cangetSpecial)
		{
			NGUITools.SetActive(this.RewardTipPic.gameObject, false);
		}
		this.mCurChoosedFloorIndex = (int)this.mPlayerTowerInfo.cur_floor;
		this.UpdateFloorInfo(this.mCurChoosedFloorIndex);
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.FloorLineScrollBar.value = Mathf.Clamp01(((float)this.mCurChoosedFloorIndex + (float)(this.mCurChoosedFloorIndex - (this.MAX_FLOOR_NUM - 1) / 2) * 2f / (float)((this.MAX_FLOOR_NUM - 1) / 2)) / (float)(this.MAX_FLOOR_NUM - 1));
		this.FloorLineScrollBar.ForceUpdate();
		this.FloorLineScrollView.UpdatePosition();
		this.uiWrapContent.ForceWrapContent();
		for (int j = 0; j < this.FloorBtnList.Count; j++)
		{
			if (this.FloorBtnList[j].CurFloorIndex == this.mCurChoosedFloorIndex)
			{
				this.SetSelectPic(this.mCurChoosedFloorIndex, j);
			}
		}
		this.RefershBtn();
	}

	// Token: 0x06003E94 RID: 16020 RVA: 0x0011F1D0 File Offset: 0x0011D3D0
	public void RefershBtn()
	{
		if (this.mPlayerTowerInfo.cur_floor <= this.mPlayerTowerInfo.floor)
		{
			this.WipeOutBtnPic.spriteName = GameDefine.BtnIcon[0];
			this.WipeoutTipsPic.enabled = true;
		}
		else
		{
			this.WipeOutBtnPic.spriteName = GameDefine.BtnIcon[2];
			this.WipeoutTipsPic.enabled = false;
		}
		if (this.mPlayerTowerInfo.wipe_out_state == 1L)
		{
			NGUITools.SetActive(this.WappingRestTimeLabel.gameObject, true);
			this.wippingRestTime = (int)(this.mPlayerTowerInfo.wipe_time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime());
			this.secondTimeCount = (float)this.wippingRestTime;
			this.WipeoutTipsPic.enabled = false;
			this.WipeOutlabel.text = StrDictionary.GetDictionaryString("#{102034}", new object[0]);
		}
		else if (this.mPlayerTowerInfo.wipe_out_state == 2L)
		{
			this.WipeoutTipsPic.enabled = true;
			NGUITools.SetActive(this.WappingRestTimeLabel.gameObject, false);
			this.WipeOutlabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
		}
		else
		{
			NGUITools.SetActive(this.WappingRestTimeLabel.gameObject, false);
			this.WipeOutlabel.text = StrDictionary.GetDictionaryString("#{101502}", new object[0]);
		}
		if (this.mPlayerTowerInfo.wipe_out_state == 0L)
		{
			this.StartBtnPic.spriteName = GameDefine.BtnIcon[1];
		}
		else
		{
			this.StartBtnPic.spriteName = GameDefine.BtnIcon[2];
		}
		if (this.mPlayerTowerInfo.cur_floor != 0L && this.mPlayerTowerInfo.times > 0L && this.mPlayerTowerInfo.wipe_out_state != 1L)
		{
			this.ResetBtnPic.spriteName = GameDefine.BtnIcon[1];
		}
		else
		{
			this.ResetBtnPic.spriteName = GameDefine.BtnIcon[2];
		}
	}

	// Token: 0x06003E95 RID: 16021 RVA: 0x0011F3C8 File Offset: 0x0011D5C8
	public void UpdateTowerInfo(ret_grant_tower_reward.request request)
	{
		this.mPlayerTowerInfo = request.tower_info;
		this.mReceiveServertime = Time.time;
		this.MAX_FLOOR_NUM = (int)Mathf.Min((float)(this.mPlayerTowerInfo.floor + 20L), (float)this.mPlayerTowerInfo.max_floor);
		this.uiWrapContent.maxIndex = 0;
		this.uiWrapContent.minIndex = -this.MAX_FLOOR_NUM + 1;
		this.WrapContentBottomWidget.height = this.MAX_FLOOR_NUM * this.uiWrapContent.itemSize;
		if (request.HasTower_special_reward)
		{
			this.mPlayerSpecialRewardList = request.tower_special_reward;
			this.mPlayerSpecialRewardList.Sort((tower_special_reward x, tower_special_reward y) => (int)(x.floor - y.floor));
		}
		this.mCurChoosedFloorIndex = (int)this.mPlayerTowerInfo.cur_floor;
		this.cangetSpecial = false;
		if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
		{
			for (int i = 0; i < this.mPlayerSpecialRewardList.Count; i++)
			{
				if (this.mPlayerSpecialRewardList[i].state == 0L)
				{
					if (i == 0 && this.mPlayerSpecialRewardList[i].floor <= this.mPlayerTowerInfo.floor)
					{
						TutorialManager.ShowTutorial(TUTORIAL_STEP.TOWER_SPECIAL_TIPS);
					}
					this.mRewardFloorIndex = (int)this.mPlayerSpecialRewardList[i].floor;
					NGUITools.SetActive(this.RewardTipPic.gameObject, this.mPlayerSpecialRewardList[i].floor <= this.mPlayerTowerInfo.floor);
					this.cangetSpecial = true;
					break;
				}
			}
		}
		if (!this.cangetSpecial)
		{
			NGUITools.SetActive(this.RewardTipPic.gameObject, false);
		}
		this.UpdateFloorInfo(this.mCurChoosedFloorIndex);
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.FloorLineScrollBar.value = Mathf.Clamp01(((float)this.mCurChoosedFloorIndex + (float)(this.mCurChoosedFloorIndex - (this.MAX_FLOOR_NUM - 1) / 2) * 2f / (float)((this.MAX_FLOOR_NUM - 1) / 2)) / (float)(this.MAX_FLOOR_NUM - 1));
		this.FloorLineScrollBar.ForceUpdate();
		this.FloorLineScrollView.UpdatePosition();
		this.uiWrapContent.ForceWrapContent();
		for (int j = 0; j < this.FloorBtnList.Count; j++)
		{
			if (this.FloorBtnList[j].CurFloorIndex == this.mCurChoosedFloorIndex)
			{
				this.SetSelectPic(this.mCurChoosedFloorIndex, j);
			}
		}
		this.RefershBtn();
	}

	// Token: 0x06003E96 RID: 16022 RVA: 0x0011F660 File Offset: 0x0011D860
	public void UpdateTowerCopyInfo(ret_request_tower_copy_info.request request)
	{
		if (request.HasTower_info)
		{
			this.mPlayerTowerInfo = request.tower_info;
			this.mReceiveServertime = Time.time;
			for (int i = 0; i < this.FloorBtnList.Count; i++)
			{
				NGUITools.SetActive(this.FloorBtnList[i].gameObject, true);
			}
			this.MAX_FLOOR_NUM = (int)Mathf.Min((float)(this.mPlayerTowerInfo.floor + 20L), (float)this.mPlayerTowerInfo.max_floor);
			this.uiWrapContent.maxIndex = 0;
			this.uiWrapContent.minIndex = -this.MAX_FLOOR_NUM + 1;
			this.WrapContentBottomWidget.height = this.MAX_FLOOR_NUM * this.uiWrapContent.itemSize;
		}
		if (request.HasTower_special_reward)
		{
			this.mPlayerSpecialRewardList = request.tower_special_reward;
			this.mPlayerSpecialRewardList.Sort((tower_special_reward x, tower_special_reward y) => (int)(x.floor - y.floor));
		}
		this.cangetSpecial = false;
		if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
		{
			for (int j = 0; j < this.mPlayerSpecialRewardList.Count; j++)
			{
				if (this.mPlayerSpecialRewardList[j].state == 0L)
				{
					if (j == 0 && this.mPlayerSpecialRewardList[j].floor <= this.mPlayerTowerInfo.floor)
					{
						TutorialManager.ShowTutorial(TUTORIAL_STEP.TOWER_SPECIAL_TIPS);
					}
					this.mRewardFloorIndex = (int)this.mPlayerSpecialRewardList[j].floor;
					NGUITools.SetActive(this.RewardTipPic.gameObject, this.mPlayerSpecialRewardList[j].floor <= this.mPlayerTowerInfo.floor);
					this.cangetSpecial = true;
					break;
				}
			}
		}
		this.mCurChoosedFloorIndex = (int)this.mPlayerTowerInfo.cur_floor;
		this.UpdateFloorInfo(this.mCurChoosedFloorIndex);
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.FloorLineScrollBar.value = Mathf.Clamp01(((float)this.mCurChoosedFloorIndex + (float)(this.mCurChoosedFloorIndex - (this.MAX_FLOOR_NUM - 1) / 2) * 2f / (float)((this.MAX_FLOOR_NUM - 1) / 2)) / (float)(this.MAX_FLOOR_NUM - 1));
		this.FloorLineScrollBar.ForceUpdate();
		this.FloorLineScrollView.UpdatePosition();
		this.uiWrapContent.ForceWrapContent();
		for (int k = 0; k < this.FloorBtnList.Count; k++)
		{
			if (this.FloorBtnList[k].CurFloorIndex == this.mCurChoosedFloorIndex)
			{
				this.SetSelectPic(this.mCurChoosedFloorIndex, k);
			}
		}
		if (!this.cangetSpecial)
		{
			NGUITools.SetActive(this.RewardTipPic.gameObject, false);
		}
		this.RefershBtn();
	}

	// Token: 0x06003E97 RID: 16023 RVA: 0x0011F938 File Offset: 0x0011DB38
	public void UpdateFloorInfo(int floorId)
	{
		if (floorId >= this.MAX_FLOOR_NUM)
		{
			this.RewardTipsLabel.enabled = false;
			NGUITools.SetActive(this.curShowRewardItemScripts.gameObject, false);
		}
		else
		{
			this.RewardTipsLabel.enabled = true;
			NGUITools.SetActive(this.curShowRewardItemScripts.gameObject, true);
			if (this.mRewardFloorIndex > 0)
			{
				TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(this.mRewardFloorIndex);
				if (towerDataByFloorID.IsShowReward == 1)
				{
					ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(towerDataByFloorID.SpecialRewardId);
					this.curShowRewardItemScripts.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
					if (GameManager.IsSupportCurDataVersion137())
					{
						this.RewardTipsLabel.text = StrDictionary.GetDictionaryString("#{102060}", new object[]
						{
							towerDataByFloorID.FloorID + 1
						});
					}
					else
					{
						this.RewardTipsLabel.text = "Reward:";
					}
				}
				else
				{
					if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
					{
						for (int i = 0; i < this.mPlayerSpecialRewardList.Count; i++)
						{
							if (this.mPlayerSpecialRewardList[i].floor >= (long)this.mRewardFloorIndex)
							{
								towerDataByFloorID = DataManager.GetTowerDataByFloorID((int)this.mPlayerSpecialRewardList[i].floor);
								if (towerDataByFloorID.IsShowReward == 1)
								{
									ShowRewardData showRewardDataByID2 = DataManager.GetShowRewardDataByID(towerDataByFloorID.SpecialRewardId);
									this.curShowRewardItemScripts.ShowRewards(showRewardDataByID2.ItemIdList, showRewardDataByID2.QualityList, showRewardDataByID2.CountList);
									if (GameManager.IsSupportCurDataVersion137())
									{
										this.RewardTipsLabel.text = StrDictionary.GetDictionaryString("#{102060}", new object[]
										{
											towerDataByFloorID.FloorID + 1
										});
									}
									else
									{
										this.RewardTipsLabel.text = "Reward:";
									}
									break;
								}
							}
						}
					}
					if (towerDataByFloorID.IsShowReward == 0)
					{
						towerDataByFloorID = DataManager.GetTowerDataByFloorID(floorId);
						ShowRewardData showRewardDataByID3 = DataManager.GetShowRewardDataByID(towerDataByFloorID.ShowRewardID);
						this.curShowRewardItemScripts.ShowRewards(showRewardDataByID3.ItemIdList, showRewardDataByID3.QualityList, showRewardDataByID3.CountList);
						if (GameManager.IsSupportCurDataVersion137())
						{
							this.RewardTipsLabel.text = StrDictionary.GetDictionaryString("#{102060}", new object[]
							{
								towerDataByFloorID.FloorID + 1
							});
						}
						else
						{
							this.RewardTipsLabel.text = "Reward:";
						}
					}
				}
			}
			else
			{
				TowerData towerDataByFloorID2 = DataManager.GetTowerDataByFloorID(floorId);
				ShowRewardData showRewardDataByID4 = DataManager.GetShowRewardDataByID(towerDataByFloorID2.ShowRewardID);
				this.curShowRewardItemScripts.ShowRewards(showRewardDataByID4.ItemIdList, showRewardDataByID4.QualityList, showRewardDataByID4.CountList);
				if (GameManager.IsSupportCurDataVersion137())
				{
					this.RewardTipsLabel.text = StrDictionary.GetDictionaryString("#{102060}", new object[]
					{
						towerDataByFloorID2.FloorID + 1
					});
				}
				else
				{
					this.RewardTipsLabel.text = "Reward:";
				}
			}
		}
		this.BestFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.floor + 1L);
		this.ResetNumLabel.text = string.Format("{0}:{1}/1", StrDictionary.GetDictionaryString("#{101528}", new object[0]), this.mPlayerTowerInfo.times);
	}

	// Token: 0x06003E98 RID: 16024 RVA: 0x0011FC88 File Offset: 0x0011DE88
	public void OnClickWipeOutBtn()
	{
		if (this.mPlayerTowerInfo == null)
		{
			return;
		}
		if (this.mPlayerTowerInfo.wipe_out_state == 0L)
		{
			if (this.mPlayerTowerInfo.cur_floor <= this.mPlayerTowerInfo.floor)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerWipeOutRootLogic, delegate
				{
					SingletonUnity<TowerWipeOutRootLogic>.Instance.Reset((int)this.mPlayerTowerInfo.cur_floor, (int)this.mPlayerTowerInfo.floor);
				}, null);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{102002}", true, false);
			}
		}
		else if (this.mPlayerTowerInfo.wipe_out_state == 1L)
		{
			int restTime = (int)(this.mPlayerTowerInfo.wipe_time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime());
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerWipeOutRootLogic, delegate
			{
				SingletonUnity<TowerWipeOutRootLogic>.Instance.ResetWipingPage((int)this.mPlayerTowerInfo.cur_floor, (int)this.mPlayerTowerInfo.floor, restTime);
			}, null);
		}
		else if (this.mPlayerTowerInfo.wipe_out_state == 2L)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerWipeOutRootLogic, delegate
			{
				SingletonUnity<TowerWipeOutRootLogic>.Instance.ResetWipeOutRewardPage((int)this.mPlayerTowerInfo.cur_floor, (int)this.mPlayerTowerInfo.floor);
			}, null);
			NoticeLogic.AddNotifyData("#{101522}", true, false);
		}
	}

	// Token: 0x06003E99 RID: 16025 RVA: 0x0011FD9C File Offset: 0x0011DF9C
	public void OnClickStartBtn()
	{
		if (this.mPlayerTowerInfo == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.TOWER_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
		if (this.mPlayerTowerInfo.wipe_out_state == 1L)
		{
			NoticeLogic.AddNotifyData("#{102035}", true, false);
		}
		else if (this.mPlayerTowerInfo.cur_floor >= (long)this.MAX_FLOOR_NUM)
		{
			NoticeLogic.AddNotifyData("#{101523}", true, false);
		}
		else
		{
			NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
			enter_tower_copy_info.request request = new enter_tower_copy_info.request();
			request.floorID = (long)this.mCurChoosedFloorIndex;
			NetLogic.GetInstance().Send<Protocol.enter_tower_copy_info>(request, null);
			int num = this.mCurChoosedFloorIndex / 5 * 5;
			int num2 = this.mCurChoosedFloorIndex / 5 * 5 + 4;
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", string.Format("stage{0}_{1}", num, num2), "starttimes");
		}
	}

	// Token: 0x06003E9A RID: 16026 RVA: 0x0011FE80 File Offset: 0x0011E080
	public void OnClickResetBtn()
	{
		if (this.mPlayerTowerInfo == null)
		{
			return;
		}
		if (this.mPlayerTowerInfo.wipe_out_state == 1L)
		{
			NoticeLogic.AddNotifyData("#{102035}", true, false);
			return;
		}
		if (this.mPlayerTowerInfo.cur_floor == 0L)
		{
			return;
		}
		if (this.mPlayerTowerInfo.times > 0L)
		{
			NetLogic.GetInstance().Send<Protocol.tower_reset>(null, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{102003}", true, false);
		}
	}

	// Token: 0x06003E9B RID: 16027 RVA: 0x0011FEF8 File Offset: 0x0011E0F8
	public void OnClickRankingBtn()
	{
		SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToTower();
		}, null);
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x0011FF3C File Offset: 0x0011E13C
	public void OnClickRewardBtn()
	{
		if (this.mPlayerTowerInfo == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.TOWER_SPECIAL_TIPS)
		{
			this.CheckTutorialEvent();
		}
		if (this.mRewardFloorIndex > 0)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerWipeOutRootLogic, delegate
			{
				SingletonUnity<TowerWipeOutRootLogic>.Instance.ResetGetSpecialRewardPage(this.mRewardFloorIndex, (long)this.mRewardFloorIndex <= this.mPlayerTowerInfo.floor && this.cangetSpecial);
			}, null);
		}
	}

	// Token: 0x06003E9D RID: 16029 RVA: 0x0011FF90 File Offset: 0x0011E190
	public void OnClickFloorBtn(int floorIndex, int itemIndex)
	{
	}

	// Token: 0x06003E9E RID: 16030 RVA: 0x0011FF94 File Offset: 0x0011E194
	private void SetSelectPic(int floorIndex, int itemIndex)
	{
	}

	// Token: 0x06003E9F RID: 16031 RVA: 0x0011FF98 File Offset: 0x0011E198
	private void Update()
	{
		if (this.mPlayerTowerInfo == null)
		{
			return;
		}
		if (this.mPlayerTowerInfo.wipe_out_state == 1L)
		{
			if (!UnityVersionUtil.IsActive(this.WappingRestTimeLabel.gameObject))
			{
				NGUITools.SetActive(this.WappingRestTimeLabel.gameObject, true);
			}
			this.secondTimeCount -= Time.deltaTime;
			if ((int)this.secondTimeCount != this.wippingRestTime)
			{
				this.wippingRestTime = (int)this.secondTimeCount;
				if (this.wippingRestTime >= 0)
				{
					this.WappingRestTimeLabel.text = string.Format("{0}:{1}", this.RemainTimeStr, new TimeSpan(0, 0, this.wippingRestTime));
				}
				else
				{
					this.WappingRestTimeLabel.text = string.Empty;
				}
				if (this.wippingRestTime <= -2)
				{
					this.mPlayerTowerInfo.wipe_out_state = 2L;
					NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
				}
			}
		}
		else if (UnityVersionUtil.IsActive(this.WappingRestTimeLabel.gameObject))
		{
			NGUITools.SetActive(this.WappingRestTimeLabel.gameObject, false);
		}
	}

	// Token: 0x06003EA0 RID: 16032 RVA: 0x001200BC File Offset: 0x0011E2BC
	public void OnClicktishiBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{101616}", null, new object[]
			{
				TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
			});
		}, null);
	}

	// Token: 0x06003EA1 RID: 16033 RVA: 0x001200EC File Offset: 0x0011E2EC
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.TOWER_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x04002A34 RID: 10804
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002A35 RID: 10805
	private int MAX_FLOOR_NUM;

	// Token: 0x04002A36 RID: 10806
	public UIWrapContentNew uiWrapContent;

	// Token: 0x04002A37 RID: 10807
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04002A38 RID: 10808
	public UILabel BestFloorLabel;

	// Token: 0x04002A39 RID: 10809
	public UILabel ResetNumLabel;

	// Token: 0x04002A3A RID: 10810
	public List<TowerFloorInfoLogic> FloorBtnList;

	// Token: 0x04002A3B RID: 10811
	public ShowRewardItems curShowRewardItemScripts;

	// Token: 0x04002A3C RID: 10812
	public UIScrollView FloorLineScrollView;

	// Token: 0x04002A3D RID: 10813
	public UIScrollBar FloorLineScrollBar;

	// Token: 0x04002A3E RID: 10814
	public UISprite StartBtnPic;

	// Token: 0x04002A3F RID: 10815
	public UISprite WipeOutBtnPic;

	// Token: 0x04002A40 RID: 10816
	public UILabel WipeOutlabel;

	// Token: 0x04002A41 RID: 10817
	public UISprite ResetBtnPic;

	// Token: 0x04002A42 RID: 10818
	public UISprite WipeoutTipsPic;

	// Token: 0x04002A43 RID: 10819
	public UISprite RewardTipPic;

	// Token: 0x04002A44 RID: 10820
	public UILabel WappingRestTimeLabel;

	// Token: 0x04002A45 RID: 10821
	public UISprite SpecialRewardBtn;

	// Token: 0x04002A46 RID: 10822
	public UILabel RewardTipsLabel;

	// Token: 0x04002A47 RID: 10823
	private int mCurChoosedFloorIndex = -1;

	// Token: 0x04002A48 RID: 10824
	private int mPlayerCurFloor;

	// Token: 0x04002A49 RID: 10825
	private int mPlayerTimes;

	// Token: 0x04002A4A RID: 10826
	private int mWipeOutTimes;

	// Token: 0x04002A4B RID: 10827
	public UITexture CopyBG;

	// Token: 0x04002A4C RID: 10828
	private tower_info mPlayerTowerInfo;

	// Token: 0x04002A4D RID: 10829
	private List<tower_special_reward> mPlayerSpecialRewardList = new List<tower_special_reward>();

	// Token: 0x04002A4E RID: 10830
	private long mServerTime;

	// Token: 0x04002A4F RID: 10831
	private TowerData mCurTowerData;

	// Token: 0x04002A50 RID: 10832
	private long mRestFreshTime;

	// Token: 0x04002A51 RID: 10833
	private float mReceiveServertime;

	// Token: 0x04002A52 RID: 10834
	private int mRewardFloorIndex = -1;

	// Token: 0x04002A53 RID: 10835
	private string RemainTimeStr;

	// Token: 0x04002A54 RID: 10836
	private bool cangetSpecial;

	// Token: 0x04002A55 RID: 10837
	private int wippingRestTime;

	// Token: 0x04002A56 RID: 10838
	private float secondTimeCount;
}
