using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A28 RID: 2600
public class NewActivityInfoRootLogic : MonoBehaviour
{
	// Token: 0x06004B25 RID: 19237 RVA: 0x0018E22C File Offset: 0x0018C42C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004B26 RID: 19238 RVA: 0x0018E238 File Offset: 0x0018C438
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004B27 RID: 19239 RVA: 0x0018E268 File Offset: 0x0018C468
	public void RegisterStartBtn(DelegateDefine.NoParamDelegate func)
	{
		this.onClickStartBtn = func;
	}

	// Token: 0x06004B28 RID: 19240 RVA: 0x0018E274 File Offset: 0x0018C474
	public void RegisterRankBtn(DelegateDefine.NoParamDelegate func)
	{
		this.onClickRankBtn = func;
	}

	// Token: 0x06004B29 RID: 19241 RVA: 0x0018E280 File Offset: 0x0018C480
	public void RegisterCloseBtn(DelegateDefine.NoParamDelegate func)
	{
		this.onClickCloseBtn = func;
	}

	// Token: 0x06004B2A RID: 19242 RVA: 0x0018E28C File Offset: 0x0018C48C
	public void RegisterMatchBtn(DelegateDefine.NoParamDelegate func)
	{
		this.onClickMatchBtn = func;
	}

	// Token: 0x06004B2B RID: 19243 RVA: 0x0018E298 File Offset: 0x0018C498
	public void refershInfo()
	{
		if (this.mCurCopyInfo != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			this.mCurCopyInfo = playerData.CopyInfoData.GetCopyinfoByID(this.mCurCopyInfo.ID);
			this.UpdateUI(this.mCurCopyInfo);
		}
	}

	// Token: 0x06004B2C RID: 19244 RVA: 0x0018E2E4 File Offset: 0x0018C4E4
	public void UpdateUI(copyscene_info info)
	{
		this.mCurCopyInfo = info;
		this.mPlayerTowerInfo = null;
		this.CurSexMiniData = null;
		this.mCurCopyScene = DataManager.GetCopySceneDataById(this.mCurCopyInfo.ID);
		if (this.mCurCopyScene.IsScuffleCopy)
		{
			this.UpdateScuffleInfo();
			return;
		}
		NGUITools.SetActive(this.DescObj, true);
		NGUITools.SetActive(this.TowerInfoObj, false);
		NGUITools.SetActive(this.ScuffleObj, false);
		ShowRewardData showRewardDataByID;
		if (this.mCurCopyScene.SubType == 11 || this.mCurCopyScene.SubType == 23 || this.mCurCopyScene.SubType == 7)
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
		if (this.mCurCopyScene.SubType == 16 || this.mCurCopyScene.SubType == 12)
		{
			NGUITools.SetActive(this.SingleBtn.gameObject, true);
			NGUITools.SetActive(this.MatchBtn.gameObject, true);
			NGUITools.SetActive(this.StartBtnPic.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.SingleBtn.gameObject, false);
			NGUITools.SetActive(this.MatchBtn.gameObject, false);
			NGUITools.SetActive(this.StartBtnPic.gameObject, true);
		}
		this.UpdateMatchingLabel();
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, false);
		}
		this.DescLabel.text = StrDictionary.GetDictionaryString(this.mCurCopyScene.Desc, new object[0]);
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBGTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if ((this.CopyBGTexture.mainTexture == null || !this.CopyBGTexture.mainTexture.name.Equals(this.mCurCopyScene.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurCopyScene.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		if (this.mCurCopyScene.SubType == 7)
		{
			UnityVersionUtil.SetActiveRecursive(this.RankBtnSp.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.RankBtnSp.gameObject, false);
		}
		if (this.CheckLevel())
		{
			this.StartBtnPic.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.StartBtnPic.spriteName = "CZ_anNiu_2+";
		}
		this.SelectDailyCopyMapItem();
	}

	// Token: 0x06004B2D RID: 19245 RVA: 0x0018E65C File Offset: 0x0018C85C
	public void UpdateScuffleInfo()
	{
		NGUITools.SetActive(this.DescObj, true);
		NGUITools.SetActive(this.TowerInfoObj, false);
		NGUITools.SetActive(this.ScuffleObj, true);
		this.DescLabel.text = string.Empty;
		NGUITools.SetActive(this.SingleBtn.gameObject, false);
		NGUITools.SetActive(this.MatchBtn.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RankBtnSp.gameObject, false);
		ConfigData configDataByKey = DataManager.GetConfigDataByKey("HangupMissionCost");
		this.costprice = 0;
		this.costpricetype = 1;
		if (configDataByKey != null)
		{
			this.costprice = configDataByKey.Valuei;
		}
		this.priceLabel.text = GameMoneyHelper.GetMoneyValStr(this.costprice, this.costpricetype);
		this.remainNum = (int)this.mCurCopyInfo.CurNum;
		this.TimesLabel.text = string.Format("{0}  {1}/{2}", StrDictionary.GetDictionaryString("#{101508}", new object[0]), this.remainNum, this.mCurCopyScene.MaxPlayNum);
		if (this.mCurCopyInfo.HasStr)
		{
			this.CurOnLineMissiondata = DataManager.GetOnlineMissionDataByID(this.mCurCopyInfo.str);
		}
		if (this.CurOnLineMissiondata != null)
		{
			this.CurMissiondata = DataManager.GetMissionDataByID(this.CurOnLineMissiondata.MissionId);
			if (this.CurMissiondata != null)
			{
				this.MissionLabel.text = this.CurMissiondata.MDescribeID;
			}
			else
			{
				this.MissionLabel.text = string.Empty;
			}
			for (int i = 0; i < this.Stars.Length; i++)
			{
				if (i < this.CurOnLineMissiondata.MissionStar)
				{
					this.Stars[i].color = Color.white;
					this.Stars[i].alpha = 1f;
				}
				else
				{
					this.Stars[i].color = Color.black;
					this.Stars[i].alpha = 0.5f;
				}
			}
			ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.CurOnLineMissiondata.ShowReward);
			if (showRewardDataByID != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, true);
				this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, false);
			}
		}
		else
		{
			for (int j = 0; j < this.Stars.Length; j++)
			{
				this.Stars[j].color = Color.black;
				this.Stars[j].alpha = 0.5f;
			}
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, false);
		}
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBGTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if ((this.CopyBGTexture.mainTexture == null || !this.CopyBGTexture.mainTexture.name.Equals(this.mCurCopyScene.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurCopyScene.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		if (this.CheckLevel() && this.mCurCopyInfo.state != 2L)
		{
			this.StartBtnPic.spriteName = "CZ_anNiu_1";
			this.AcceptBtn.spriteName = "CZ_anNiu_1";
			this.RefershBtn.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.StartBtnPic.spriteName = "CZ_anNiu_2+";
			this.AcceptBtn.spriteName = "CZ_anNiu_2+";
			this.RefershBtn.spriteName = "CZ_anNiu_2+";
		}
		if (this.mCurCopyInfo.state == 0L)
		{
			NGUITools.SetActive(this.AcceptBtn.gameObject, true);
			NGUITools.SetActive(this.StartBtnPic.gameObject, false);
			NGUITools.SetActive(this.RefershBtn.gameObject, true);
			NGUITools.SetActive(this.GiveUpBtn.gameObject, false);
		}
		else if (this.mCurCopyInfo.state == 1L)
		{
			NGUITools.SetActive(this.AcceptBtn.gameObject, false);
			NGUITools.SetActive(this.StartBtnPic.gameObject, true);
			NGUITools.SetActive(this.RefershBtn.gameObject, false);
			NGUITools.SetActive(this.GiveUpBtn.gameObject, true);
		}
		else if (this.mCurCopyInfo.state == 2L)
		{
			NGUITools.SetActive(this.AcceptBtn.gameObject, true);
			NGUITools.SetActive(this.StartBtnPic.gameObject, false);
			NGUITools.SetActive(this.RefershBtn.gameObject, true);
			NGUITools.SetActive(this.GiveUpBtn.gameObject, false);
		}
		this.SelectDailyCopyMapItem();
	}

	// Token: 0x06004B2E RID: 19246 RVA: 0x0018EB6C File Offset: 0x0018CD6C
	public void UpdateSexGameUI(SexMiniData info)
	{
		this.CurSexMiniData = info;
		this.mPlayerTowerInfo = null;
		this.mCurCopyInfo = null;
		this.mCurCopyScene = null;
		NGUITools.SetActive(this.DescObj, true);
		NGUITools.SetActive(this.TowerInfoObj, false);
		NGUITools.SetActive(this.ScuffleObj, false);
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.CurSexMiniData.ShowRewardID);
		NGUITools.SetActive(this.SingleBtn.gameObject, false);
		NGUITools.SetActive(this.MatchBtn.gameObject, false);
		NGUITools.SetActive(this.StartBtnPic.gameObject, true);
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, false);
		}
		this.DescLabel.text = StrDictionary.GetDictionaryString(this.CurSexMiniData.Description, new object[0]);
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBGTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if ((this.CopyBGTexture.mainTexture == null || !this.CopyBGTexture.mainTexture.name.Equals(this.CurSexMiniData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.CurSexMiniData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		UnityVersionUtil.SetActiveRecursive(this.RankBtnSp.gameObject, true);
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.CurSexMiniData.UnlockLevel))
		{
			this.StartBtnPic.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.StartBtnPic.spriteName = "CZ_anNiu_2+";
		}
		this.SelectShowINMapByType(GameDefine.ACTIVITY_TYPE.SEX_MINI);
	}

	// Token: 0x06004B2F RID: 19247 RVA: 0x0018ED90 File Offset: 0x0018CF90
	public int GetCurPlayerLimitFloor()
	{
		Dictionary<int, TowerData> towerDataDic = DataManager.TowerDataDic;
		int count = towerDataDic.Count;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		for (int i = count - 1; i >= 0; i--)
		{
			if (towerDataDic[i].LimitLevel <= level)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06004B30 RID: 19248 RVA: 0x0018EDE4 File Offset: 0x0018CFE4
	public void UpdateTowerUI(tower_info info, List<tower_special_reward> SpecialRewardList = null, bool selectmapflag = false)
	{
		this.mPlayerTowerInfo = info;
		this.mCurCopyInfo = null;
		this.mCurCopyScene = null;
		this.CurSexMiniData = null;
		NGUITools.SetActive(this.DescObj, false);
		NGUITools.SetActive(this.TowerInfoObj, true);
		NGUITools.SetActive(this.ScuffleObj, false);
		NGUITools.SetActive(this.RankBtnSp.gameObject, false);
		NGUITools.SetActive(this.SingleBtn.gameObject, false);
		NGUITools.SetActive(this.MatchBtn.gameObject, false);
		NGUITools.SetActive(this.StartBtnPic.gameObject, true);
		this.cangetSpecial = false;
		this.mRewardFloorIndex = -1;
		this.mPlayerSpecialRewardList = SpecialRewardList;
		if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
		{
			for (int i = 0; i < this.mPlayerSpecialRewardList.Count; i++)
			{
				if (this.mPlayerSpecialRewardList[i].state == 0L)
				{
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
		this.BestFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.floor + 1L);
		if (this.mPlayerTowerInfo.cur_floor < 80L)
		{
			this.CurFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.cur_floor + 1L);
		}
		else
		{
			this.CurFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.cur_floor);
		}
		this.ResetNumLabel.text = string.Format("{0}:{1}/1", StrDictionary.GetDictionaryString("#{101528}", new object[0]), this.mPlayerTowerInfo.times);
		this.MAX_FLOOR_NUM = (int)this.mPlayerTowerInfo.max_floor;
		this.UpdateFloorInfo((int)this.mPlayerTowerInfo.cur_floor);
		this.mCurPlayerLimitFloor = this.GetCurPlayerLimitFloor();
		this.TowerLimitLevelLabel.text = StrDictionary.GetDictionaryString("#{102082}", new object[]
		{
			this.mCurPlayerLimitFloor + 1
		});
		this.RefershTowerBtn();
		TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(0);
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBGTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if ((this.CopyBGTexture.mainTexture == null || !this.CopyBGTexture.mainTexture.name.Equals(towerDataByFloorID.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(towerDataByFloorID.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		if (selectmapflag)
		{
			this.SelectShowINMapByType(GameDefine.ACTIVITY_TYPE.TOWER);
		}
	}

	// Token: 0x06004B31 RID: 19249 RVA: 0x0018F138 File Offset: 0x0018D338
	public void ResetTowerInfo(tower_info info)
	{
		if (this.mPlayerTowerInfo != null)
		{
			this.mPlayerTowerInfo = info;
			this.mCurCopyInfo = null;
			this.mCurCopyScene = null;
			this.CurSexMiniData = null;
			NGUITools.SetActive(this.DescObj, false);
			NGUITools.SetActive(this.TowerInfoObj, true);
			NGUITools.SetActive(this.ScuffleObj, false);
			NGUITools.SetActive(this.RankBtnSp.gameObject, false);
			this.cangetSpecial = false;
			this.mRewardFloorIndex = -1;
			if (this.mPlayerSpecialRewardList != null && this.mPlayerSpecialRewardList.Count != 0)
			{
				for (int i = 0; i < this.mPlayerSpecialRewardList.Count; i++)
				{
					if (this.mPlayerSpecialRewardList[i].state == 0L)
					{
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
			this.BestFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.floor + 1L);
			if (this.mPlayerTowerInfo.cur_floor < 80L)
			{
				this.CurFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.cur_floor + 1L);
			}
			else
			{
				this.CurFloorLabel.text = string.Format("{0}", this.mPlayerTowerInfo.cur_floor);
			}
			this.ResetNumLabel.text = string.Format("{0}:{1}/1", StrDictionary.GetDictionaryString("#{101528}", new object[0]), this.mPlayerTowerInfo.times);
			this.MAX_FLOOR_NUM = (int)this.mPlayerTowerInfo.max_floor;
			this.UpdateFloorInfo((int)this.mPlayerTowerInfo.cur_floor);
			this.mCurPlayerLimitFloor = this.GetCurPlayerLimitFloor();
			this.TowerLimitLevelLabel.text = StrDictionary.GetDictionaryString("#{102082}", new object[]
			{
				this.mCurPlayerLimitFloor + 1
			});
			this.RefershTowerBtn();
		}
	}

	// Token: 0x06004B32 RID: 19250 RVA: 0x0018F390 File Offset: 0x0018D590
	public void UpdateFloorInfo(int floorId)
	{
		if (floorId >= this.MAX_FLOOR_NUM)
		{
			this.RewardTipsLabel.enabled = false;
			NGUITools.SetActive(this.ShowRewardRoot.gameObject, false);
		}
		else
		{
			this.RewardTipsLabel.enabled = true;
			NGUITools.SetActive(this.ShowRewardRoot.gameObject, true);
			if (this.mRewardFloorIndex > 0)
			{
				TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(this.mRewardFloorIndex);
				if (towerDataByFloorID.IsShowReward == 1)
				{
					ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(towerDataByFloorID.SpecialRewardId);
					this.ShowRewardRoot.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
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
									this.ShowRewardRoot.ShowRewards(showRewardDataByID2.ItemIdList, showRewardDataByID2.QualityList, showRewardDataByID2.CountList);
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
						this.ShowRewardRoot.ShowRewards(showRewardDataByID3.ItemIdList, showRewardDataByID3.QualityList, showRewardDataByID3.CountList);
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
				this.ShowRewardRoot.ShowRewards(showRewardDataByID4.ItemIdList, showRewardDataByID4.QualityList, showRewardDataByID4.CountList);
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
	}

	// Token: 0x06004B33 RID: 19251 RVA: 0x0018F684 File Offset: 0x0018D884
	public void OnClickAcceptBtn()
	{
		if (!this.CheckLevel())
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			return;
		}
		if (this.remainNum <= 0)
		{
			NoticeLogic.AddNotifyData("#{102046}", true, false);
			return;
		}
		if (this.CurMissiondata != null && this.mCurCopyInfo.state == 0L && this.CurMissiondata.Class == 7 && this.CurOnLineMissiondata != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.CurMissiondata.ID, this.CurOnLineMissiondata.ID);
		}
	}

	// Token: 0x06004B34 RID: 19252 RVA: 0x0018F72C File Offset: 0x0018D92C
	public void OnClickGiveUpBtn()
	{
		if (this.CurMissiondata != null && this.mCurCopyInfo.state == 1L && this.CurMissiondata.Class == 7)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AbandonMission(this.CurMissiondata.ID, false);
		}
	}

	// Token: 0x06004B35 RID: 19253 RVA: 0x0018F784 File Offset: 0x0018D984
	public void OnClickRefershBtn()
	{
		if (!this.CheckLevel())
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			return;
		}
		if (this.remainNum <= 0)
		{
			NoticeLogic.AddNotifyData("#{102046}", true, false);
			return;
		}
		if (GameMoneyHelper.BeforeCheckBuy(this.costpricetype, this.costprice))
		{
			NetLogic.GetInstance().Send<Protocol.refresh_online_misison>(null, null);
		}
	}

	// Token: 0x06004B36 RID: 19254 RVA: 0x0018F7F0 File Offset: 0x0018D9F0
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

	// Token: 0x06004B37 RID: 19255 RVA: 0x0018F868 File Offset: 0x0018DA68
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

	// Token: 0x06004B38 RID: 19256 RVA: 0x0018F97C File Offset: 0x0018DB7C
	public void OnClickRankingBtn()
	{
		SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToTower();
		}, null);
	}

	// Token: 0x06004B39 RID: 19257 RVA: 0x0018F9C0 File Offset: 0x0018DBC0
	public void OnClickRewardBtn()
	{
		if (this.mPlayerTowerInfo == null)
		{
			return;
		}
		if (this.mRewardFloorIndex > 0)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerWipeOutRootLogic, delegate
			{
				SingletonUnity<TowerWipeOutRootLogic>.Instance.ResetGetSpecialRewardPage(this.mRewardFloorIndex, (long)this.mRewardFloorIndex <= this.mPlayerTowerInfo.floor && this.cangetSpecial);
			}, null);
		}
	}

	// Token: 0x06004B3A RID: 19258 RVA: 0x0018FA04 File Offset: 0x0018DC04
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardRoot.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06004B3B RID: 19259 RVA: 0x0018FA14 File Offset: 0x0018DC14
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBGTexture.mainTexture = tex;
	}

	// Token: 0x06004B3C RID: 19260 RVA: 0x0018FA24 File Offset: 0x0018DC24
	public bool CheckLevel()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.mCurCopyScene.MinLevel);
	}

	// Token: 0x06004B3D RID: 19261 RVA: 0x0018FA40 File Offset: 0x0018DC40
	public bool CheckLevel(int minLevel, int maxLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxLevel);
	}

	// Token: 0x06004B3E RID: 19262 RVA: 0x0018FA54 File Offset: 0x0018DC54
	public void UpdateWipeoutItem(string item_id)
	{
		if (this.mCurCopyScene.CanWipeOut == 1 && item_id.Equals(this.mCurCopyScene.WipeItem))
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			int itemStackNumById = playerData.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.WipeItem);
		}
	}

	// Token: 0x06004B3F RID: 19263 RVA: 0x0018FAAC File Offset: 0x0018DCAC
	public void OnClickStartBtn()
	{
		if (this.CurSexMiniData != null)
		{
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.CurSexMiniData.UnlockLevel))
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100313}", new object[]
				{
					this.CurSexMiniData.UnlockLevel
				}), true, false);
				return;
			}
			string text = string.Empty;
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession == PROFESSION_TYPE.NQS)
			{
				text = this.CurSexMiniData.ManNpcId;
			}
			else
			{
				text = this.CurSexMiniData.WomenNpcId;
			}
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager.AutoMoveDest(this.CurSexMiniData.MapId, DataManager.GetNPCPosInMonsterData(this.CurSexMiniData.MapId, text), AUTO_SEARCH_PARTH_FINISHEVENT.FIND_NPC, text);
			return;
		}
		else if (this.mPlayerTowerInfo != null)
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.TOWER_CLICK_START)
			{
				this.CheckTutorialEvent();
			}
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			int condition = DataManager.GetFunctionDataById(4003.ToString()).Condition;
			if (!playerData.CheckLevel(condition))
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					condition
				}), true, false);
				return;
			}
			int num = (int)this.mPlayerTowerInfo.cur_floor;
			if (num >= this.MAX_FLOOR_NUM)
			{
				num = this.MAX_FLOOR_NUM - 1;
			}
			int limitLevel = DataManager.GetTowerDataByFloorID(num).LimitLevel;
			if (!playerData.CheckLevel(limitLevel))
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102083}", new object[]
				{
					limitLevel
				}), true, false);
				return;
			}
			if (this.mPlayerTowerInfo.wipe_out_state == 1L)
			{
				NoticeLogic.AddNotifyData("#{102035}", true, false);
			}
			else if (this.mPlayerTowerInfo.cur_floor >= this.mPlayerTowerInfo.max_floor)
			{
				NoticeLogic.AddNotifyData("#{101523}", true, false);
			}
			else
			{
				NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
				enter_tower_copy_info.request request = new enter_tower_copy_info.request();
				request.floorID = (long)((int)this.mPlayerTowerInfo.cur_floor);
				NetLogic.GetInstance().Send<Protocol.enter_tower_copy_info>(request, null);
				int num2 = (int)this.mPlayerTowerInfo.cur_floor / 5 * 5;
				int num3 = (int)this.mPlayerTowerInfo.cur_floor / 5 * 5 + 4;
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", string.Format("stage{0}_{1}", num2, num3), "starttimes");
			}
			return;
		}
		else
		{
			if (this.mCurCopyScene == null || !this.mCurCopyScene.IsScuffleCopy)
			{
				if (this.onClickStartBtn != null)
				{
					this.onClickStartBtn();
				}
				return;
			}
			if (!this.CheckLevel())
			{
				TutorialManager.LevelLimitAction();
				return;
			}
			if (this.CurMissiondata != null)
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
				MissionManager.ClickMissionAction(this.CurMissiondata.ID);
			}
			return;
		}
	}

	// Token: 0x06004B40 RID: 19264 RVA: 0x0018FD90 File Offset: 0x0018DF90
	public void OnClickRankBtn()
	{
		if (this.onClickRankBtn != null)
		{
			this.onClickRankBtn();
		}
	}

	// Token: 0x06004B41 RID: 19265 RVA: 0x0018FDA8 File Offset: 0x0018DFA8
	public void OnClickCloseBtn()
	{
		SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
		SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
		if (this.onClickCloseBtn != null)
		{
			this.onClickCloseBtn();
		}
	}

	// Token: 0x06004B42 RID: 19266 RVA: 0x0018FDF4 File Offset: 0x0018DFF4
	public void RefershTowerBtn()
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
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int condition = DataManager.GetFunctionDataById(4003.ToString()).Condition;
		int num = (int)this.mPlayerTowerInfo.cur_floor;
		if (num >= this.MAX_FLOOR_NUM)
		{
			num = this.MAX_FLOOR_NUM - 1;
		}
		int limitLevel = DataManager.GetTowerDataByFloorID(num).LimitLevel;
		if (!playerData.CheckLevel(condition))
		{
			this.StartBtnPic.spriteName = GameDefine.BtnIcon[2];
		}
		else if (this.mPlayerTowerInfo.wipe_out_state == 0L)
		{
			if (playerData.CheckLevel(limitLevel))
			{
				this.StartBtnPic.spriteName = GameDefine.BtnIcon[1];
			}
			else
			{
				this.StartBtnPic.spriteName = GameDefine.BtnIcon[2];
			}
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

	// Token: 0x06004B43 RID: 19267 RVA: 0x00190084 File Offset: 0x0018E284
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
					this.WappingRestTimeLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101580}", new object[0]), new TimeSpan(0, 0, this.wippingRestTime));
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

	// Token: 0x06004B44 RID: 19268 RVA: 0x001901B0 File Offset: 0x0018E3B0
	public void OnClicktishiBtn()
	{
		if (this.mCurCopyScene != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurCopyScene.Rule, null, new object[]
				{
					TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
				});
			}, null);
			return;
		}
		if (this.mPlayerTowerInfo != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{101616}", null, new object[]
				{
					TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
				});
			}, null);
			return;
		}
		if (this.CurSexMiniData != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.CurSexMiniData.Rule, null, new object[]
				{
					TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
				});
			}, null);
			return;
		}
	}

	// Token: 0x06004B45 RID: 19269 RVA: 0x00190248 File Offset: 0x0018E448
	public void SelectDailyCopyMapItem()
	{
		List<ActivityMapData> activityMapDataByType = DataManager.GetActivityMapDataByType(10, this.mCurCopyScene.SubType);
		if (activityMapDataByType != null)
		{
			List<ActivityMapData> list = new List<ActivityMapData>();
			for (int i = 0; i < activityMapDataByType.Count; i++)
			{
				if (activityMapDataByType[i].IsNeedDailyActid())
				{
					if (activityMapDataByType[i].IsVisible && activityMapDataByType[i].ActivityID.Equals(this.mCurCopyScene.ID))
					{
						list.Add(activityMapDataByType[i]);
					}
				}
				else if (activityMapDataByType[i].IsVisible)
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

	// Token: 0x06004B46 RID: 19270 RVA: 0x001904D8 File Offset: 0x0018E6D8
	public void SelectShowINMapByType(GameDefine.ACTIVITY_TYPE needtype)
	{
		List<ActivityMapData> activityMapDataByType = DataManager.GetActivityMapDataByType((int)needtype, ActivityMapData.DefaultSubType);
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

	// Token: 0x06004B47 RID: 19271 RVA: 0x0019070C File Offset: 0x0018E90C
	public void OnClickMatchBtn()
	{
		if (this.onClickMatchBtn != null)
		{
			this.onClickMatchBtn();
		}
	}

	// Token: 0x06004B48 RID: 19272 RVA: 0x00190724 File Offset: 0x0018E924
	public void OnClickSingleBtn()
	{
		if (this.mCurCopyScene == null)
		{
			return;
		}
		if (!this.CheckLevel())
		{
			TutorialManager.LevelLimitAction();
			return;
		}
		this.remainNum = (int)this.mCurCopyInfo.CurNum;
		if (this.remainNum <= 0)
		{
			if (!string.IsNullOrEmpty(this.mCurCopyScene.TimeInc))
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				int itemStackNumById = playerData.ItemBackPack.GetItemStackNumById(this.mCurCopyScene.TimeInc);
				if (itemStackNumById > 0)
				{
					ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurCopyScene.TimeInc);
					MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101598}", new object[]
					{
						itemDataByID.MName
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
			}
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
			return;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
		enter_copy_scene.request request = new enter_copy_scene.request();
		request.mapInfoId = this.mCurCopyScene.ID;
		NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request, null);
		this.CheckStartFlurry();
	}

	// Token: 0x06004B49 RID: 19273 RVA: 0x00190840 File Offset: 0x0018EA40
	public void CheckStartFlurry()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", this.mCurCopyScene.ID), "start");
	}

	// Token: 0x17000FC8 RID: 4040
	// (get) Token: 0x06004B4A RID: 19274 RVA: 0x0019086C File Offset: 0x0018EA6C
	// (set) Token: 0x06004B4B RID: 19275 RVA: 0x00190874 File Offset: 0x0018EA74
	public bool IsMatching
	{
		get
		{
			return this.mIsMatching;
		}
		set
		{
			this.mIsMatching = value;
		}
	}

	// Token: 0x06004B4C RID: 19276 RVA: 0x00190880 File Offset: 0x0018EA80
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
					this.SetMatchAnima(true);
				}
				else
				{
					this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
					this.SetMatchAnima(false);
				}
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
				this.SetMatchAnima(false);
			}
		}
		else if (playerData.IsHaveTeam())
		{
			if (playerData.TeamInfo.IsVertify && this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
				this.SetMatchAnima(true);
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
				this.SetMatchAnima(false);
			}
		}
		else if (playerData.TeamInfo.IsVertify)
		{
			if (this.mCurCopyScene.ID.Equals(playerData.TeamInfo.TeamGoalData.CopyId))
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{102007}", new object[0]);
				this.SetMatchAnima(true);
			}
			else
			{
				this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
				this.SetMatchAnima(false);
			}
		}
		else
		{
			this.MatchBtnLabel.text = StrDictionary.GetDictionaryString("#{101501}", new object[0]);
			this.SetMatchAnima(false);
		}
	}

	// Token: 0x06004B4D RID: 19277 RVA: 0x00190A80 File Offset: 0x0018EC80
	public void SetMatchAnima(bool isshow)
	{
		if (isshow)
		{
			this.MatchAnima.enabled = true;
			this.MatchAnima.PlayForward();
			this.IsMatching = true;
		}
		else
		{
			this.MatchAnima.ResetToBeginning();
			this.MatchAnima.enabled = false;
			this.IsMatching = false;
		}
	}

	// Token: 0x040038E2 RID: 14562
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040038E3 RID: 14563
	public GameObject DescObj;

	// Token: 0x040038E4 RID: 14564
	public GameObject TowerInfoObj;

	// Token: 0x040038E5 RID: 14565
	public GameObject ScuffleObj;

	// Token: 0x040038E6 RID: 14566
	public UILabel BestFloorLabel;

	// Token: 0x040038E7 RID: 14567
	public UILabel CurFloorLabel;

	// Token: 0x040038E8 RID: 14568
	public UILabel ResetNumLabel;

	// Token: 0x040038E9 RID: 14569
	public ShowRewardItems ShowRewardRoot;

	// Token: 0x040038EA RID: 14570
	public UILabel DescLabel;

	// Token: 0x040038EB RID: 14571
	public UISprite StartBtnPic;

	// Token: 0x040038EC RID: 14572
	public UISprite WipeOutBtnPic;

	// Token: 0x040038ED RID: 14573
	public UILabel WipeOutlabel;

	// Token: 0x040038EE RID: 14574
	public UISprite ResetBtnPic;

	// Token: 0x040038EF RID: 14575
	public UISprite WipeoutTipsPic;

	// Token: 0x040038F0 RID: 14576
	public UISprite RewardTipPic;

	// Token: 0x040038F1 RID: 14577
	public UILabel WappingRestTimeLabel;

	// Token: 0x040038F2 RID: 14578
	public UILabel RewardTipsLabel;

	// Token: 0x040038F3 RID: 14579
	public UILabel TowerLimitLevelLabel;

	// Token: 0x040038F4 RID: 14580
	public UISprite RankBtnSp;

	// Token: 0x040038F5 RID: 14581
	public UITexture CopyBGTexture;

	// Token: 0x040038F6 RID: 14582
	private copyscene_info mCurCopyInfo;

	// Token: 0x040038F7 RID: 14583
	private CopySceneData mCurCopyScene;

	// Token: 0x040038F8 RID: 14584
	public UISprite MatchBtn;

	// Token: 0x040038F9 RID: 14585
	public UILabel MatchBtnLabel;

	// Token: 0x040038FA RID: 14586
	public TweenAlpha MatchAnima;

	// Token: 0x040038FB RID: 14587
	public UISprite SingleBtn;

	// Token: 0x040038FC RID: 14588
	private DelegateDefine.NoParamDelegate onClickStartBtn;

	// Token: 0x040038FD RID: 14589
	private DelegateDefine.NoParamDelegate onClickRankBtn;

	// Token: 0x040038FE RID: 14590
	private DelegateDefine.NoParamDelegate onClickCloseBtn;

	// Token: 0x040038FF RID: 14591
	private DelegateDefine.NoParamDelegate onClickMatchBtn;

	// Token: 0x04003900 RID: 14592
	public tower_info mPlayerTowerInfo;

	// Token: 0x04003901 RID: 14593
	private List<tower_special_reward> mPlayerSpecialRewardList = new List<tower_special_reward>();

	// Token: 0x04003902 RID: 14594
	private int mRewardFloorIndex = -1;

	// Token: 0x04003903 RID: 14595
	private bool cangetSpecial;

	// Token: 0x04003904 RID: 14596
	private int MAX_FLOOR_NUM;

	// Token: 0x04003905 RID: 14597
	private int remainNum;

	// Token: 0x04003906 RID: 14598
	public UISprite AcceptBtn;

	// Token: 0x04003907 RID: 14599
	public UISprite RefershBtn;

	// Token: 0x04003908 RID: 14600
	public UISprite GiveUpBtn;

	// Token: 0x04003909 RID: 14601
	public UILabel MissionLabel;

	// Token: 0x0400390A RID: 14602
	public UILabel TimesLabel;

	// Token: 0x0400390B RID: 14603
	public UISprite[] Stars;

	// Token: 0x0400390C RID: 14604
	public UILabel priceLabel;

	// Token: 0x0400390D RID: 14605
	private OnlineMissionData CurOnLineMissiondata;

	// Token: 0x0400390E RID: 14606
	private MissionData CurMissiondata;

	// Token: 0x0400390F RID: 14607
	private int costprice;

	// Token: 0x04003910 RID: 14608
	private int costpricetype = 1;

	// Token: 0x04003911 RID: 14609
	private SexMiniData CurSexMiniData;

	// Token: 0x04003912 RID: 14610
	private int mCurPlayerLimitFloor;

	// Token: 0x04003913 RID: 14611
	private int wippingRestTime;

	// Token: 0x04003914 RID: 14612
	private float secondTimeCount;

	// Token: 0x04003915 RID: 14613
	private bool mIsMatching;
}
