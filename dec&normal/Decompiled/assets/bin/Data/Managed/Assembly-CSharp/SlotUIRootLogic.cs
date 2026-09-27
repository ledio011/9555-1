using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009A0 RID: 2464
public class SlotUIRootLogic : SingletonUnity<SlotUIRootLogic>
{
	// Token: 0x060045C8 RID: 17864 RVA: 0x0015FD88 File Offset: 0x0015DF88
	public void CheckPrePage()
	{
		if (this.mPrePage != UI_PAGE_TYPE.INVALID)
		{
			switch (this.mPrePage)
			{
			case UI_PAGE_TYPE.BACK_PACK_ITEM:
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
				{
					SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickItemBackPackBtn();
				}, null);
				break;
			case UI_PAGE_TYPE.ENHANCE_EQUIP:
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
				}, null);
				break;
			case UI_PAGE_TYPE.REFINE:
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
				}, null);
				break;
			}
		}
		this.mPrePage = UI_PAGE_TYPE.INVALID;
	}

	// Token: 0x060045C9 RID: 17865 RVA: 0x0015FE5C File Offset: 0x0015E05C
	public void SetPrePage(UI_PAGE_TYPE prePage)
	{
		this.mPrePage = prePage;
	}

	// Token: 0x060045CA RID: 17866 RVA: 0x0015FE68 File Offset: 0x0015E068
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060045CB RID: 17867 RVA: 0x0015FE74 File Offset: 0x0015E074
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x060045CC RID: 17868 RVA: 0x0015FEA4 File Offset: 0x0015E0A4
	public void EnableReset()
	{
		this.curSlotinfo = null;
		this.PlaySlotBgm();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> leftBtnInfo = new List<MenuTabBtnInfo>();
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(leftBtnInfo, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), true, null);
			SingletonUnity<MenuBaseRootLogic>.Instance.SetPageLabel(StrDictionary.GetDictionaryString("#{100109}", new object[0]));
		}, null);
		UnityVersionUtil.SetActiveRecursive(this.BigWinObj.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.AutoObj.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.SumAnima.gameObject, false);
		NGUITools.SetActive(this.FreeTimesLabel.gameObject, false);
		this.autoIsopen = false;
		UnityVersionUtil.SetActiveRecursive(this.UpColliderObj.gameObject, false);
		this.icondataList = DataManager.GetSlotIconDataList();
		this.icondataList.Sort((SlotIconData x, SlotIconData y) => int.Parse(x.ID) - int.Parse(y.ID));
		for (int i = 0; i < this.slotlineslist.Count; i++)
		{
			this.slotlineslist[i].Reset(this.icondataList, i);
		}
		this.PlaySpinEndAnima();
		this.SlotItemList.Clear();
		this.AutoAllGetItemList.Clear();
		this.mPrePage = UI_PAGE_TYPE.INVALID;
		this.starAnima.enabled = false;
		this.starEffectSp.enabled = false;
		this.starAnima.transform.localScale = Vector3.one;
	}

	// Token: 0x060045CD RID: 17869 RVA: 0x0015FFF0 File Offset: 0x0015E1F0
	public void Reset(ret_slot_info.request request)
	{
		this.SlotDataDic = request.slot_datas;
		this.SlotDataList = new List<slot_data>(this.SlotDataDic.Values);
		this.curSlotinfo = request.slot_info;
		this.SlotItemList = new List<slot_item>();
		this.AutoAllGetItemList.Clear();
		if (request.HasSlot_items)
		{
			this.SlotItemList = new List<slot_item>(request.slot_items.Values);
			this.curSlotinfo.sumNum -= (long)this.SlotItemList.Count;
			if (this.curSlotinfo.sumNum < 0L)
			{
				Debug.LogError("slot sum num < 0");
			}
		}
		for (int i = 0; i < this.SlotDataList.Count; i++)
		{
			if (this.SlotDataList[i].Rank <= 3L && this.SlotDataList[i].Rank > 0L)
			{
				ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.SlotDataList[i].ShowRewardID);
				if (showRewardDataByID != null)
				{
					UnityVersionUtil.SetActiveRecursive(this.bigwinItems[(int)this.SlotDataList[i].Rank - 1].gameObject, true);
					this.bigwinItems[(int)this.SlotDataList[i].Rank - 1].ShowRewards(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.bigwinItems[(int)this.SlotDataList[i].Rank - 1].gameObject, false);
				}
			}
		}
		this.AllGetItemList.Clear();
		this.isSpinFlag = false;
		this.IsAutoFlag = false;
		this.AutoRollCount = 0;
		this.IsResultOver = true;
		this.ResetAutoInfo();
		this.refershUI();
		this.CheckAutoitems();
		if (TutorialManager.CurStep == TUTORIAL_STEP.SLOT_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Slot", "open", "opentimes");
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x0016020C File Offset: 0x0015E40C
	public void CheckAutoitems()
	{
		if (this.SlotItemList.Count > 0)
		{
			MessageBoxLogic.OpenOkBox_One(StrDictionary.GetDictionaryString("#{301005}", new object[]
			{
				this.SlotItemList.Count
			}), "#{100127}", new MessageBoxLogic.OnYesClick(this.BackYesAutoRoll));
		}
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x00160264 File Offset: 0x0015E464
	public void BackYesAutoRoll()
	{
		if (this.IsAutoFlag)
		{
			return;
		}
		this.IsAutoFlag = true;
		this.AutoRollCount = this.SlotItemList.Count;
		this.DoAutoSpin();
		UnityVersionUtil.SetActiveRecursive(this.UpColliderObj.gameObject, true);
		this.refershUI();
	}

	// Token: 0x060045D0 RID: 17872 RVA: 0x001602B4 File Offset: 0x0015E4B4
	public void refershUI()
	{
		if (this.curSlotinfo.curNum > 0L)
		{
			this.FreeTimesLabel.text = string.Format("{0} x{1}", StrDictionary.GetDictionaryString("#{301003}", new object[0]), this.curSlotinfo.curNum);
			NGUITools.SetActive(this.FreeTimesLabel.gameObject, true);
			NGUITools.SetActive(this.PriceLabel.gameObject, false);
			this.spinTips.enabled = true;
		}
		else
		{
			this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(this.AutoDataList[0].PriceCost, this.AutoDataList[0].PriceType);
			NGUITools.SetActive(this.FreeTimesLabel.gameObject, false);
			NGUITools.SetActive(this.PriceLabel.gameObject, true);
			this.spinTips.enabled = false;
		}
		this.sumSlider.value = (float)this.curSlotinfo.sumNum / (float)this.SumMaxLimit;
		if (this.curSlotinfo.sumNum >= (long)this.SumMaxLimit)
		{
			this.starAnima.enabled = true;
			this.starEffectSp.enabled = true;
		}
		else
		{
			this.starAnima.enabled = false;
			this.starEffectSp.enabled = false;
			this.starAnima.transform.localScale = Vector3.one;
		}
		if (this.AutoRollCount > 0)
		{
			this.AutoLabel.text = string.Format("{0} x{1}", StrDictionary.GetDictionaryString("#{301004}", new object[0]), this.AutoRollCount);
		}
		else
		{
			this.AutoLabel.text = StrDictionary.GetDictionaryString("#{301004}", new object[0]);
		}
	}

	// Token: 0x060045D1 RID: 17873 RVA: 0x00160478 File Offset: 0x0015E678
	private void Update()
	{
		if (this.isSpinFlag && Time.time - this.StartTime > this.SpinTime)
		{
			if (this.linemoveStopnum >= 3)
			{
				return;
			}
			this.SpinTime += this.lineDeltime;
			this.slotlineslist[this.linemoveStopnum].StopRolling(this.resultStr[this.linemoveStopnum]);
		}
		if (this.IsAutoFlag && this.IsResultOver)
		{
			this.autoTime += Time.deltaTime;
			if (this.autoTime > this.autoDelTime)
			{
				if (this.AutoRollCount > 0)
				{
					this.autoTime = 0f;
					this.DoAutoSpin();
				}
				else
				{
					this.IsAutoFlag = false;
					UnityVersionUtil.SetActiveRecursive(this.UpColliderObj.gameObject, false);
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotRewardRoot, delegate
					{
						SingletonUnity<SlotRewardRootLogic>.Instance.Reset(this.AutoAllGetItemList, new DelegateDefine.NoParamDelegate(this.SetShowRewardOver), true);
					}, null);
				}
			}
		}
	}

	// Token: 0x060045D2 RID: 17874 RVA: 0x0016057C File Offset: 0x0015E77C
	public void OnClickAutoBtn()
	{
		if (this.curSlotinfo == null)
		{
			return;
		}
		if (this.autoIsopen)
		{
			NGUITools.SetActive(this.AutoObj, false);
			this.autoIsopen = false;
		}
		else
		{
			NGUITools.SetActive(this.AutoObj, true);
			this.autoIsopen = true;
		}
	}

	// Token: 0x060045D3 RID: 17875 RVA: 0x001605CC File Offset: 0x0015E7CC
	public void OnClickAutoBtn1()
	{
		if (GameMoneyHelper.BeforeCheckBuy(this.AutoDataList[1].PriceType, this.AutoDataList[1].PriceCost))
		{
			this.StartAutoSpin(2);
			this.OnClickAutoBtn();
		}
	}

	// Token: 0x060045D4 RID: 17876 RVA: 0x00160614 File Offset: 0x0015E814
	public void OnClickAutoBtn2()
	{
		if (GameMoneyHelper.BeforeCheckBuy(this.AutoDataList[2].PriceType, this.AutoDataList[2].PriceCost))
		{
			this.StartAutoSpin(3);
			this.OnClickAutoBtn();
		}
	}

	// Token: 0x060045D5 RID: 17877 RVA: 0x0016065C File Offset: 0x0015E85C
	public void OnClickAutoBtn3()
	{
		if (GameMoneyHelper.BeforeCheckBuy(this.AutoDataList[3].PriceType, this.AutoDataList[3].PriceCost))
		{
			this.StartAutoSpin(4);
			this.OnClickAutoBtn();
		}
	}

	// Token: 0x060045D6 RID: 17878 RVA: 0x001606A4 File Offset: 0x0015E8A4
	public void OnClickSpinBtn()
	{
		if (this.curSlotinfo == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.SLOT_CLICK)
		{
			this.CheckTutorialEvent();
		}
		if (this.isSpinFlag || !this.IsResultOver)
		{
			return;
		}
		if (this.autoIsopen)
		{
			NGUITools.SetActive(this.AutoObj, false);
			this.autoIsopen = false;
		}
		if (this.curSlotinfo.curNum <= 0L)
		{
			if (GameMoneyHelper.BeforeCheckBuy(this.AutoDataList[0].PriceType, this.AutoDataList[0].PriceCost))
			{
				this.DoSpin();
			}
		}
		else
		{
			this.DoSpin();
		}
	}

	// Token: 0x060045D7 RID: 17879 RVA: 0x00160758 File Offset: 0x0015E958
	public void DoSpin()
	{
		this.isSpinFlag = true;
		this.isBigWinFlag = false;
		this.IsResultOver = false;
		this.resultSlotdata = null;
		this.linemoveStopnum = 0;
		this.StartTime = Time.time;
		this.SpinTime = this.MaxSpinTime;
		this.SlotItemList.Clear();
		this.curState = SlotUIRootLogic.SpinState.waitResult;
		UnityVersionUtil.SetActiveRecursive(this.UpColliderObj.gameObject, true);
		this.setNotwinResult();
		this.SpinMove();
		spin_slot.request request = new spin_slot.request();
		request.ID = "1";
		NetLogic.GetInstance().Send<Protocol.spin_slot>(request, null);
	}

	// Token: 0x060045D8 RID: 17880 RVA: 0x001607EC File Offset: 0x0015E9EC
	public void StartAutoSpin(int autoid)
	{
		if (this.IsAutoFlag)
		{
			return;
		}
		this.IsAutoFlag = true;
		this.AutoRollCount = this.AutoDataList[autoid - 1].RollNum;
		this.SlotItemList.Clear();
		this.AutoAllGetItemList.Clear();
		this.DoAutoSpin();
		spin_slot.request request = new spin_slot.request();
		request.ID = autoid.ToString();
		NetLogic.GetInstance().Send<Protocol.spin_slot>(request, null);
		UnityVersionUtil.SetActiveRecursive(this.UpColliderObj.gameObject, true);
		this.refershUI();
	}

	// Token: 0x060045D9 RID: 17881 RVA: 0x00160878 File Offset: 0x0015EA78
	public void DoAutoSpin()
	{
		this.isSpinFlag = true;
		this.isBigWinFlag = false;
		this.IsResultOver = false;
		this.resultSlotdata = null;
		this.linemoveStopnum = 0;
		this.StartTime = Time.time;
		this.setNotwinResult();
		if (this.SlotItemList.Count == 0)
		{
			this.SpinTime = this.MaxSpinTime;
			this.curState = SlotUIRootLogic.SpinState.waitResult;
		}
		else
		{
			this.SetRollResult();
			this.SpinTime = this.AutoMinSpinTime;
			this.curState = SlotUIRootLogic.SpinState.getResult;
		}
		this.SpinMove();
	}

	// Token: 0x060045DA RID: 17882 RVA: 0x00160900 File Offset: 0x0015EB00
	public void SpinMove()
	{
		this.PlayRollSound();
		this.PlaySpinStartAnima();
		this.stopBigWinAnima();
		for (int i = 0; i < this.slotlineslist.Count; i++)
		{
			this.slotlineslist[i].StartSpin(new DelegateDefine.NoParamDelegate(this.LineStopFun));
		}
	}

	// Token: 0x060045DB RID: 17883 RVA: 0x00160958 File Offset: 0x0015EB58
	public void UpdateResult(ret_spin_slot.request request)
	{
		int num = (int)this.curSlotinfo.curNum;
		this.curSlotinfo.curNum = request.slot_info.curNum;
		this.SlotItemList = new List<slot_item>(request.slot_items.Values);
		this.SetRollResult();
		if (Time.time - this.StartTime < this.MaxSpinTime)
		{
			this.curState = SlotUIRootLogic.SpinState.getResult;
			this.SpinTime = this.MinSpinTime;
		}
		if (this.IsAutoFlag)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Slot", "spin", string.Format("spin_{0}", this.SlotItemList.Count));
		}
		else if (num > 0)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Slot", "spin", "spin_free");
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Slot", "spin", "spin_1");
		}
	}

	// Token: 0x060045DC RID: 17884 RVA: 0x00160A4C File Offset: 0x0015EC4C
	public void SetRollResult()
	{
		this.resultSlotdata = this.SlotDataDic[this.SlotItemList[0].ID];
		this.SetWinResult(this.resultSlotdata.RewardMap);
		if (this.resultSlotdata.Rank <= 3L)
		{
			this.isBigWinFlag = true;
		}
	}

	// Token: 0x060045DD RID: 17885 RVA: 0x00160AA8 File Offset: 0x0015ECA8
	public void ShowResult()
	{
		this.autoTime = 0f;
		this.AutoGetSumReward();
		this.curSlotinfo.sumNum += 1L;
		this.isSpinFlag = false;
		this.PlaySpinEndAnima();
		this.playResultAnima();
		if (this.SlotItemList.Count > 0 && this.SlotItemList[0].HasItems && this.SlotItemList[0].items.Count > 0)
		{
			if (this.IsAutoFlag)
			{
				this.AddAutoGetItemList(this.SlotItemList[0].items);
				if (this.isBigWinFlag)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotBigWinRoot, delegate
					{
						SingletonUnity<SlotBigWinRootLogic>.Instance.Reset(this.SlotItemList[0].items, new DelegateDefine.NoParamDelegate(this.SetShowRewardOver));
					}, null);
				}
				else
				{
					SimpleRewardRootLogic.AddRewards(this.SlotItemList[0].items);
					this.SetShowRewardOver();
				}
			}
			else if (this.isBigWinFlag)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotBigWinRoot, delegate
				{
					SingletonUnity<SlotBigWinRootLogic>.Instance.Reset(this.SlotItemList[0].items, new DelegateDefine.NoParamDelegate(this.SetShowRewardOver));
				}, null);
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotRewardRoot, delegate
				{
					SingletonUnity<SlotRewardRootLogic>.Instance.Reset(this.SlotItemList[0].items, new DelegateDefine.NoParamDelegate(this.SetShowRewardOver), false);
				}, null);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Slot", "spin_win", string.Format("spin_{0}", this.SlotItemList[0].ID));
		}
		else
		{
			this.SetShowRewardOver();
		}
		if (this.SlotItemList.Count > 0)
		{
			request_slot_reward.request request = new request_slot_reward.request();
			request.uuid = this.SlotItemList[0].uuid;
			NetLogic.GetInstance().Send<Protocol.request_slot_reward>(request, null);
			this.SaveGetitems(this.SlotItemList[0]);
			this.SlotItemList.RemoveAt(0);
		}
		if (!this.IsAutoFlag)
		{
			UnityVersionUtil.SetActiveRecursive(this.UpColliderObj.gameObject, false);
		}
		else
		{
			this.AutoRollCount--;
		}
		this.refershUI();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		playerData.playerSlotData.SetInfo((int)this.curSlotinfo.curNum, (int)this.curSlotinfo.sumNum, this.SlotItemList.Count);
	}

	// Token: 0x060045DE RID: 17886 RVA: 0x00160CE8 File Offset: 0x0015EEE8
	public void AddAutoGetItemList(List<item> newitems)
	{
		for (int i = 0; i < newitems.Count; i++)
		{
			this.AutoAllGetItemList.Add(newitems[i]);
		}
	}

	// Token: 0x060045DF RID: 17887 RVA: 0x00160D20 File Offset: 0x0015EF20
	public void OnClickSumRewardBtn()
	{
		if (this.curSlotinfo == null)
		{
			return;
		}
		if (this.curSlotinfo.HasSumNum && this.curSlotinfo.sumNum >= (long)this.SumMaxLimit)
		{
			this.isautoGetSum = false;
			this.sunItemList.Clear();
			this.curSlotinfo.sumNum = 0L;
			WaitResponseUIRootLogic.OpenWaitBox(244, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_slot_sum_reward>(null, null);
		}
	}

	// Token: 0x060045E0 RID: 17888 RVA: 0x00160DA0 File Offset: 0x0015EFA0
	public void AutoGetSumReward()
	{
		if (this.curSlotinfo.HasSumNum && this.curSlotinfo.sumNum >= (long)this.SumMaxLimit)
		{
			this.isautoGetSum = true;
			this.sunItemList.Clear();
			this.curSlotinfo.sumNum = 0L;
			WaitResponseUIRootLogic.OpenWaitBox(244, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_slot_sum_reward>(null, null);
		}
	}

	// Token: 0x060045E1 RID: 17889 RVA: 0x00160E14 File Offset: 0x0015F014
	public void ShowSumReward(List<item> sumitemlist, long sumresult)
	{
		this.sunItemList = sumitemlist;
		this.SaveGetitems(sumitemlist);
		this.curSlotinfo.sumNum = sumresult;
		this.curSlotinfo.sumNum -= (long)this.SlotItemList.Count;
		if (this.isautoGetSum)
		{
			this.sumShowrewards.ShowRewards(this.sunItemList);
			UnityVersionUtil.SetActiveRecursive(this.SumAnima.gameObject, true);
			this.SumAnima.resetOnPlay = true;
			this.SumAnima.Play(true);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotRewardRoot, delegate
			{
				SingletonUnity<SlotRewardRootLogic>.Instance.Reset(this.sunItemList, null, false);
			}, null);
		}
		this.refershUI();
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x00160EC8 File Offset: 0x0015F0C8
	public void OnClickTotalPrizesBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotLogRoot, delegate
		{
			SingletonUnity<SlotLogRootLogic>.Instance.ShowRewards(this.AllGetItemList);
		}, null);
	}

	// Token: 0x060045E3 RID: 17891 RVA: 0x00160EE8 File Offset: 0x0015F0E8
	public void OnClickTishiBtn()
	{
		if (this.curSlotinfo == null)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotRuleRoot, delegate
		{
			SingletonUnity<SlotRuleRootLogic>.Instance.Reset(this.SlotDataList);
		}, null);
	}

	// Token: 0x060045E4 RID: 17892 RVA: 0x00160F20 File Offset: 0x0015F120
	public void OnClickCloseBtn()
	{
		this.IsAutoFlag = false;
		this.isSpinFlag = true;
		this.IsResultOver = false;
		this.StopRollSound();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SlotUIRoot);
		this.PlaySceneBgmMusic();
		this.CheckPrePage();
	}

	// Token: 0x060045E5 RID: 17893 RVA: 0x00160F74 File Offset: 0x0015F174
	private void PlaySlotBgm()
	{
		SoundData soundDataById = DataManager.GetSoundDataById(8);
		if (soundDataById != null)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlayBGMusic(soundDataById.Id, soundDataById.FadeOutTime, soundDataById.FadeInTime);
		}
	}

	// Token: 0x060045E6 RID: 17894 RVA: 0x00160FAC File Offset: 0x0015F1AC
	private void PlayRollSound()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(11, 1f, null);
	}

	// Token: 0x060045E7 RID: 17895 RVA: 0x00160FC0 File Offset: 0x0015F1C0
	private void StopRollSound()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(11);
	}

	// Token: 0x060045E8 RID: 17896 RVA: 0x00160FD0 File Offset: 0x0015F1D0
	private void PlayResultSound()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(10, 1f, null);
	}

	// Token: 0x060045E9 RID: 17897 RVA: 0x00160FE4 File Offset: 0x0015F1E4
	private void PlaySceneBgmMusic()
	{
		MapInfoData currentMapInofData = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		SoundData soundDataById = DataManager.GetSoundDataById(currentMapInofData.GetAudioID());
		if (soundDataById != null)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlayBGMusic(soundDataById.Id, soundDataById.FadeOutTime, soundDataById.FadeInTime);
		}
	}

	// Token: 0x060045EA RID: 17898 RVA: 0x00161030 File Offset: 0x0015F230
	public void LineStopFun()
	{
		this.linemoveStopnum++;
		if (this.linemoveStopnum >= 3)
		{
			this.ShowResult();
		}
		else if (this.linemoveStopnum == 1)
		{
			this.StopDirAnima(0);
		}
	}

	// Token: 0x060045EB RID: 17899 RVA: 0x00161078 File Offset: 0x0015F278
	public void PlaySpinStartAnima()
	{
		for (int i = 0; i < this.LightsEndAnima.Length; i++)
		{
			this.LightsEndAnima[i].enabled = false;
		}
		this.SetAnimaWiAlph();
		for (int j = 0; j < this.LightsStartAnima.Length; j++)
		{
			this.LightsStartAnima[j].ResetToBeginning();
			this.LightsStartAnima[j].PlayForward();
		}
		for (int k = 0; k < this.DirsAnima.Length; k++)
		{
			this.DirsAnima[k].PlayForward();
		}
	}

	// Token: 0x060045EC RID: 17900 RVA: 0x0016110C File Offset: 0x0015F30C
	public void PlaySpinEndAnima()
	{
		for (int i = 0; i < this.LightsStartAnima.Length; i++)
		{
			this.LightsStartAnima[i].enabled = false;
		}
		this.SetAnimaWiAlph();
		for (int j = 0; j < this.LightsEndAnima.Length; j++)
		{
			this.LightsEndAnima[j].ResetToBeginning();
			this.LightsEndAnima[j].PlayForward();
		}
		for (int k = 0; k < this.DirsAnima.Length; k++)
		{
			this.DirsAnima[k].enabled = false;
			this.DirsAnima[k].transform.localRotation = Quaternion.Euler(Vector3.zero);
		}
	}

	// Token: 0x060045ED RID: 17901 RVA: 0x001611BC File Offset: 0x0015F3BC
	public void StopDirAnima(int i)
	{
		this.DirsAnima[i].enabled = false;
		this.DirsAnima[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	// Token: 0x060045EE RID: 17902 RVA: 0x001611F4 File Offset: 0x0015F3F4
	public void SetAnimaWiAlph()
	{
		for (int i = 0; i < this.AnimaAlphWis.Length; i++)
		{
			this.AnimaAlphWis[i].alpha = 1f;
		}
	}

	// Token: 0x060045EF RID: 17903 RVA: 0x0016122C File Offset: 0x0015F42C
	public void SetShowRewardOver()
	{
		this.IsResultOver = true;
	}

	// Token: 0x060045F0 RID: 17904 RVA: 0x00161238 File Offset: 0x0015F438
	public void playResultAnima()
	{
		this.StopRollSound();
		if (this.IsAutoFlag)
		{
			this.PlayResultSound();
		}
		UnityVersionUtil.SetActiveRecursive(this.BigWinObj, true);
		switch (this.curSlotType)
		{
		case SLOTTYPE.AXC:
			this.slotlineslist[1].lineitems[4].PlayeAnima();
			this.RotateSp[1].enabled = true;
			this.RotateSp[2].enabled = false;
			this.RotateSp[0].enabled = false;
			break;
		case SLOTTYPE.XBX:
			this.slotlineslist[0].lineitems[4].PlayeAnima();
			this.slotlineslist[2].lineitems[4].PlayeAnima();
			this.RotateSp[0].enabled = true;
			this.RotateSp[2].enabled = true;
			this.RotateSp[1].enabled = false;
			break;
		case SLOTTYPE.XXB:
			this.slotlineslist[0].lineitems[4].PlayeAnima();
			this.slotlineslist[1].lineitems[4].PlayeAnima();
			this.RotateSp[0].enabled = true;
			this.RotateSp[1].enabled = true;
			this.RotateSp[2].enabled = false;
			break;
		case SLOTTYPE.BXX:
			this.slotlineslist[1].lineitems[4].PlayeAnima();
			this.slotlineslist[2].lineitems[4].PlayeAnima();
			this.RotateSp[1].enabled = true;
			this.RotateSp[2].enabled = true;
			this.RotateSp[0].enabled = false;
			break;
		case SLOTTYPE.XXX:
			for (int i = 0; i < this.slotlineslist.Count; i++)
			{
				this.slotlineslist[i].lineitems[4].PlayeAnima();
				this.RotateSp[i].enabled = true;
			}
			break;
		}
	}

	// Token: 0x060045F1 RID: 17905 RVA: 0x0016145C File Offset: 0x0015F65C
	public void stopBigWinAnima()
	{
		UnityVersionUtil.SetActiveRecursive(this.BigWinObj.gameObject, false);
		for (int i = 0; i < this.slotlineslist.Count; i++)
		{
			this.slotlineslist[i].lineitems[4].StopAnima();
		}
	}

	// Token: 0x060045F2 RID: 17906 RVA: 0x001614B4 File Offset: 0x0015F6B4
	private List<int> ListRandom(List<int> myList)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < myList.Count; i++)
		{
			int num = Random.Range(0, myList.Count - 1);
			if (num != i)
			{
				int num2 = myList[i];
				myList[i] = myList[num];
				myList[num] = num2;
			}
		}
		return myList;
	}

	// Token: 0x060045F3 RID: 17907 RVA: 0x00161518 File Offset: 0x0015F718
	public void SaveGetitems(slot_item getslotitem)
	{
		List<item> list = new List<item>();
		if (getslotitem.HasItems)
		{
			for (int i = 0; i < getslotitem.items.Count; i++)
			{
				list.Add(new item
				{
					itemId = getslotitem.items[i].itemId,
					quality = getslotitem.items[i].quality,
					itemCount = getslotitem.items[i].itemCount
				});
			}
		}
		else
		{
			list.Clear();
		}
		for (int j = 0; j < list.Count; j++)
		{
			int num = -1;
			for (int k = 0; k < this.AllGetItemList.Count; k++)
			{
				if (this.AllGetItemList[k].itemId.Equals(list[j].itemId) && this.AllGetItemList[k].quality == list[j].quality)
				{
					num = k;
					break;
				}
			}
			item item = new item();
			if (num != -1)
			{
				this.AllGetItemList[num].itemCount += list[j].itemCount;
			}
			else
			{
				this.AllGetItemList.Add(list[j]);
			}
		}
		if (SingletonUnity<SlotLogRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotLogRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SlotLogRootLogic>.Instance.ShowRewards(this.AllGetItemList);
		}
	}

	// Token: 0x060045F4 RID: 17908 RVA: 0x001616BC File Offset: 0x0015F8BC
	public void SaveGetitems(List<item> newitemsold)
	{
		List<item> list = new List<item>();
		if (newitemsold.Count > 0)
		{
			for (int i = 0; i < newitemsold.Count; i++)
			{
				list.Add(new item
				{
					itemId = newitemsold[i].itemId,
					quality = newitemsold[i].quality,
					itemCount = newitemsold[i].itemCount
				});
			}
		}
		else
		{
			list.Clear();
		}
		for (int j = 0; j < list.Count; j++)
		{
			int num = -1;
			for (int k = 0; k < this.AllGetItemList.Count; k++)
			{
				if (this.AllGetItemList[k].itemId.Equals(list[j].itemId))
				{
					num = k;
					break;
				}
			}
			if (num != -1)
			{
				this.AllGetItemList[num].itemCount += list[j].itemCount;
			}
			else
			{
				this.AllGetItemList.Add(list[j]);
			}
		}
		if (SingletonUnity<SlotLogRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotLogRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SlotLogRootLogic>.Instance.ShowRewards(this.AllGetItemList);
		}
	}

	// Token: 0x060045F5 RID: 17909 RVA: 0x00161824 File Offset: 0x0015FA24
	public void ResetAutoInfo()
	{
		this.AutoDataList = DataManager.GetSlotAutoDataList();
		this.AutoDataList.Sort((SlotAutoData x, SlotAutoData y) => x.ID.CompareTo(y.ID));
		for (int i = 0; i < this.rollTimesLabel.Length; i++)
		{
			this.rollTimesLabel[i].text = string.Format("{0}", this.AutoDataList[i + 1].RollNum);
			this.PriceLabels[i].text = GameMoneyHelper.GetMoneyValStr(this.AutoDataList[i + 1].PriceCost, this.AutoDataList[i + 1].PriceType);
		}
	}

	// Token: 0x060045F6 RID: 17910 RVA: 0x001618E4 File Offset: 0x0015FAE4
	public void setNotwinResult()
	{
		this.curSlotType = SLOTTYPE.AXC;
		List<int> list = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			list.Add(i);
		}
		int num = list[Random.Range(0, list.Count)];
		list.Remove(num);
		int num2 = list[Random.Range(0, list.Count)];
		list.Remove(num2);
		int num3 = list[Random.Range(0, list.Count)];
		this.resultStr[0] = num.ToString();
		this.resultStr[1] = num2.ToString();
		this.resultStr[2] = num3.ToString();
	}

	// Token: 0x060045F7 RID: 17911 RVA: 0x00161990 File Offset: 0x0015FB90
	public void SetWinResult(string resultid)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			list.Add(i);
		}
		this.curSlotType = SLOTTYPE.XXX;
		this.resultStr[0] = resultid.Substring(0, 1);
		this.resultStr[1] = resultid.Substring(1, 1);
		this.resultStr[2] = resultid.Substring(2, 1);
		if (this.resultStr[2].Equals("b"))
		{
			int num = int.Parse(this.resultStr[1]);
			list.Remove(num);
			switch (Random.Range(0, 3))
			{
			case 0:
			{
				this.curSlotType = SLOTTYPE.BXX;
				this.resultStr[2] = this.resultStr[0];
				int num2 = list[Random.Range(0, list.Count)];
				this.resultStr[0] = num2.ToString();
				break;
			}
			case 1:
				this.curSlotType = SLOTTYPE.XBX;
				this.resultStr[2] = this.resultStr[0];
				num = list[Random.Range(0, list.Count)];
				this.resultStr[1] = num.ToString();
				break;
			case 2:
			{
				this.curSlotType = SLOTTYPE.XXB;
				int num3 = list[Random.Range(0, list.Count)];
				this.resultStr[2] = num3.ToString();
				break;
			}
			}
		}
		else if (this.resultStr[2].Equals("c"))
		{
			this.curSlotType = SLOTTYPE.AXC;
			int num4 = int.Parse(this.resultStr[1]);
			list.Remove(num4);
			int num5 = list[Random.Range(0, list.Count)];
			list.Remove(num5);
			int num6 = list[Random.Range(0, list.Count)];
			this.resultStr[0] = num5.ToString();
			this.resultStr[2] = num6.ToString();
		}
	}

	// Token: 0x040032CD RID: 13005
	private UI_PAGE_TYPE mPrePage = UI_PAGE_TYPE.INVALID;

	// Token: 0x040032CE RID: 13006
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040032CF RID: 13007
	public List<SlotLineLogic> slotlineslist;

	// Token: 0x040032D0 RID: 13008
	private SlotUIRootLogic.SpinState curState;

	// Token: 0x040032D1 RID: 13009
	private SLOTTYPE curSlotType;

	// Token: 0x040032D2 RID: 13010
	public UILabel FreeTimesLabel;

	// Token: 0x040032D3 RID: 13011
	public UILabel PriceLabel;

	// Token: 0x040032D4 RID: 13012
	public UISlider sumSlider;

	// Token: 0x040032D5 RID: 13013
	public GameObject AutoObj;

	// Token: 0x040032D6 RID: 13014
	public List<ShowRewardItems> bigwinItems;

	// Token: 0x040032D7 RID: 13015
	public TweenAlpha[] LightsStartAnima;

	// Token: 0x040032D8 RID: 13016
	public TweenRotation[] DirsAnima;

	// Token: 0x040032D9 RID: 13017
	public TweenAlpha[] LightsEndAnima;

	// Token: 0x040032DA RID: 13018
	public UIWidget[] AnimaAlphWis;

	// Token: 0x040032DB RID: 13019
	public GameObject BigWinObj;

	// Token: 0x040032DC RID: 13020
	public UISprite[] RotateSp;

	// Token: 0x040032DD RID: 13021
	public UILabel[] rollTimesLabel;

	// Token: 0x040032DE RID: 13022
	public UILabel[] PriceLabels;

	// Token: 0x040032DF RID: 13023
	private Dictionary<string, slot_data> SlotDataDic;

	// Token: 0x040032E0 RID: 13024
	private List<slot_data> SlotDataList;

	// Token: 0x040032E1 RID: 13025
	private slot_info curSlotinfo;

	// Token: 0x040032E2 RID: 13026
	private slot_data resultSlotdata;

	// Token: 0x040032E3 RID: 13027
	private List<slot_item> SlotItemList = new List<slot_item>();

	// Token: 0x040032E4 RID: 13028
	private List<item> AutoAllGetItemList = new List<item>();

	// Token: 0x040032E5 RID: 13029
	private List<item> sunItemList = new List<item>();

	// Token: 0x040032E6 RID: 13030
	private List<item> AllGetItemList = new List<item>();

	// Token: 0x040032E7 RID: 13031
	private List<SlotIconData> icondataList = new List<SlotIconData>();

	// Token: 0x040032E8 RID: 13032
	private List<SlotAutoData> AutoDataList = new List<SlotAutoData>();

	// Token: 0x040032E9 RID: 13033
	private int SumMaxLimit = 10;

	// Token: 0x040032EA RID: 13034
	public bool isSpinFlag;

	// Token: 0x040032EB RID: 13035
	public string[] resultStr;

	// Token: 0x040032EC RID: 13036
	private int linemoveStopnum;

	// Token: 0x040032ED RID: 13037
	public GameObject UpColliderObj;

	// Token: 0x040032EE RID: 13038
	private float MaxSpinTime = 5f;

	// Token: 0x040032EF RID: 13039
	private float MinSpinTime = 1.5f;

	// Token: 0x040032F0 RID: 13040
	private float AutoMinSpinTime = 1f;

	// Token: 0x040032F1 RID: 13041
	private float StartTime;

	// Token: 0x040032F2 RID: 13042
	private float SpinTime;

	// Token: 0x040032F3 RID: 13043
	private float lineDeltime = 0.3f;

	// Token: 0x040032F4 RID: 13044
	public bool IsAutoFlag;

	// Token: 0x040032F5 RID: 13045
	public bool IsResultOver;

	// Token: 0x040032F6 RID: 13046
	public int AutoRollCount;

	// Token: 0x040032F7 RID: 13047
	public UILabel AutoLabel;

	// Token: 0x040032F8 RID: 13048
	public GameObject SpinSp;

	// Token: 0x040032F9 RID: 13049
	private float autoTime;

	// Token: 0x040032FA RID: 13050
	private float autoDelTime = 1.5f;

	// Token: 0x040032FB RID: 13051
	private bool autoIsopen;

	// Token: 0x040032FC RID: 13052
	public UISprite spinTips;

	// Token: 0x040032FD RID: 13053
	private bool isBigWinFlag;

	// Token: 0x040032FE RID: 13054
	public UIPlayTween SumAnima;

	// Token: 0x040032FF RID: 13055
	public ShowRewardItems sumShowrewards;

	// Token: 0x04003300 RID: 13056
	private bool isautoGetSum;

	// Token: 0x04003301 RID: 13057
	public Transform totalTra;

	// Token: 0x04003302 RID: 13058
	public TweenScale starAnima;

	// Token: 0x04003303 RID: 13059
	public UISprite starEffectSp;

	// Token: 0x020009A1 RID: 2465
	public enum SpinState
	{
		// Token: 0x0400330A RID: 13066
		waitResult,
		// Token: 0x0400330B RID: 13067
		getResult
	}
}
