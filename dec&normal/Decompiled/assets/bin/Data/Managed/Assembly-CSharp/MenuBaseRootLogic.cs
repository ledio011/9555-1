using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009FA RID: 2554
public class MenuBaseRootLogic : SingletonUnity<MenuBaseRootLogic>
{
	// Token: 0x0600490E RID: 18702 RVA: 0x0017895C File Offset: 0x00176B5C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x0600490F RID: 18703 RVA: 0x00178968 File Offset: 0x00176B68
	private void CheckTutorialEvent()
	{
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(true);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06004910 RID: 18704 RVA: 0x001789BC File Offset: 0x00176BBC
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06004911 RID: 18705 RVA: 0x001789C8 File Offset: 0x00176BC8
	private void Start()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.OnUnityAdsStateChange = new GameManager.Void_Bool_Delegate(this.VideoBtnUpdate);
	}

	// Token: 0x06004912 RID: 18706 RVA: 0x001789E0 File Offset: 0x00176BE0
	public void ResetPage(List<MenuTabBtnInfo> leftBtnInfo, DelegateDefine.NoParamDelegate backBtnFunc, bool hideTab = false, string titleStr = null)
	{
		if (hideTab)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftTabRoot, false);
			this.PageNameLabel.text = titleStr;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftTabRoot, true);
		}
		if (leftBtnInfo != null)
		{
			this.mCurMenuTabBtnInfoList = leftBtnInfo;
			int num = this.mCurMenuTabBtnInfoList.Count - this.mCurBtnList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.BtnPrefab.gameObject) as GameObject;
					MenuBaseTabBtnLogic component = gameObject.GetComponent<MenuBaseTabBtnLogic>();
					gameObject.name = this.mCurBtnList.Count.ToString();
					gameObject.transform.parent = this.LeftTabGrid.transform;
					gameObject.transform.localScale = Vector3.one;
					this.mCurBtnList.Add(component);
				}
			}
			for (int j = 0; j < this.mCurBtnList.Count; j++)
			{
				if (j < this.mCurMenuTabBtnInfoList.Count)
				{
					UnityVersionUtil.SetActiveRecursive(this.mCurBtnList[j].gameObject, true);
					this.mCurBtnList[j].Reset(this.mCurMenuTabBtnInfoList[j]);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.mCurBtnList[j].gameObject, false);
				}
			}
			this.LeftTabGrid.Reposition();
		}
		this.onClickBackBtn = backBtnFunc;
		this.hideGangMoney();
	}

	// Token: 0x06004913 RID: 18707 RVA: 0x00178B64 File Offset: 0x00176D64
	private void UpdateBtnAlpha(int index)
	{
		for (int i = 0; i < this.mCurBtnList.Count; i++)
		{
			if (index == i)
			{
				this.mCurBtnList[i].BtnWidget.alpha = 1f;
			}
			else
			{
				this.mCurBtnList[i].BtnWidget.alpha = 0.4f;
			}
		}
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x00178BD0 File Offset: 0x00176DD0
	public bool AutoClickTipsTap()
	{
		for (int i = 0; i < this.mCurBtnList.Count; i++)
		{
			if (UnityVersionUtil.IsActive(this.mCurBtnList[i].gameObject) && this.mCurBtnList[i].isTips)
			{
				this.mCurBtnList[i].OnClickBtn();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004915 RID: 18709 RVA: 0x00178C40 File Offset: 0x00176E40
	public void RefershTips()
	{
		for (int i = 0; i < this.mCurBtnList.Count; i++)
		{
			if (UnityVersionUtil.IsActive(this.mCurBtnList[i].gameObject))
			{
				this.mCurBtnList[i].UpdateTips();
			}
		}
	}

	// Token: 0x06004916 RID: 18710 RVA: 0x00178C98 File Offset: 0x00176E98
	public void SetTargetBtnToggleEnable(int index)
	{
		for (int i = 0; i < this.mCurBtnList.Count; i++)
		{
			if (i == index)
			{
				this.mCurBtnList[i].BtnToggle.alpha = 1f;
				this.mCurBtnList[i].BtnWidget.alpha = 1f;
			}
			else
			{
				this.mCurBtnList[i].BtnToggle.alpha = 0f;
				this.mCurBtnList[i].BtnWidget.alpha = 0.4f;
			}
		}
		if (index < this.mCurMenuTabBtnInfoList.Count)
		{
			this.SetPageLabel(this.mCurMenuTabBtnInfoList[index].PageName);
		}
	}

	// Token: 0x06004917 RID: 18711 RVA: 0x00178D64 File Offset: 0x00176F64
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		this.showMoneyAnima = false;
		this.curCashnum = GameMoneyHelper.GetCash();
		this.curGoldnum = GameMoneyHelper.GetGold();
		this.curDiamondnum = GameMoneyHelper.GetDiamond();
		this.curGangnum = GameMoneyHelper.GetGuildContribute();
		this.DiamondLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetDiamond());
		this.GoldLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetGold());
		this.CashLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetCash());
		this.GangLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetGuildContribute());
	}

	// Token: 0x06004918 RID: 18712 RVA: 0x00178E40 File Offset: 0x00177040
	private void OnDisable()
	{
		this.showMoneyAnima = false;
		this.StopMoneyAnima();
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		FunctionTipsRootLogic.ClearHandTip();
		this.ClearTutorialEvent();
	}

	// Token: 0x06004919 RID: 18713 RVA: 0x00178E88 File Offset: 0x00177088
	public void ResetBackBtn(DelegateDefine.NoParamDelegate func)
	{
		this.onClickBackBtn = func;
	}

	// Token: 0x0600491A RID: 18714 RVA: 0x00178E94 File Offset: 0x00177094
	public void OnClickBackBtn()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_EXIT || TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_EXIT || TutorialManager.CurStep == TUTORIAL_STEP.CAR_CLICK_BACK || TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_ALL_EXIT || TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_CLICK_BACK || TutorialManager.CurStep == TUTORIAL_STEP.CAR_GIFT_CLICK_BACK || TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BACK)
		{
			this.CheckTutorialEvent();
		}
		if (this.onClickBackBtn != null)
		{
			this.onClickBackBtn();
		}
	}

	// Token: 0x0600491B RID: 18715 RVA: 0x00178F18 File Offset: 0x00177118
	private void Update()
	{
		if (this.showMoneyAnima)
		{
			this.temptime += Time.deltaTime;
			if (this.diamondratio != 0L)
			{
				this.DiamondLabel.text = string.Format("{0:N0}", (float)this.curDiamondnum + this.temptime * (float)this.diamondratio);
			}
			if (this.goldratio != 0L)
			{
				this.GoldLabel.text = string.Format("{0:N0}", (float)this.curGoldnum + this.temptime * (float)this.goldratio);
			}
			if (this.cashratio != 0L)
			{
				this.CashLabel.text = string.Format("{0:N0}", (float)this.curCashnum + this.temptime * (float)this.cashratio);
			}
			if (this.gangratio != 0L)
			{
				this.GangLabel.text = string.Format("{0:N0}", (float)this.curGangnum + this.temptime * (float)this.gangratio);
			}
			if (this.temptime >= (float)this.delaTimeAnima)
			{
				this.showMoneyAnima = false;
				this.StopMoneyAnima();
			}
		}
	}

	// Token: 0x0600491C RID: 18716 RVA: 0x00179050 File Offset: 0x00177250
	public void UpdateMoney()
	{
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			this.showMoneyAnima = true;
			this.temptime = 0f;
			this.targetCashnum = GameMoneyHelper.GetCash();
			this.targetGoldnum = GameMoneyHelper.GetGold();
			this.targetDiamondnum = GameMoneyHelper.GetDiamond();
			this.targetGangnum = GameMoneyHelper.GetGuildContribute();
			if (Mathf.Abs((float)(this.curCashnum - this.targetCashnum)) > 1E-45f)
			{
				if (!this.CashAnima.enabled)
				{
					this.CashAnima.PlayForward();
				}
				this.cashratio = (this.targetCashnum - this.curCashnum) / this.delaTimeAnima;
			}
			else
			{
				this.cashratio = 0L;
			}
			if (Mathf.Abs((float)(this.curGoldnum - this.targetGoldnum)) > 1E-45f)
			{
				if (!this.GoldAnima.enabled)
				{
					this.GoldAnima.PlayForward();
				}
				this.goldratio = (this.targetGoldnum - this.curGoldnum) / this.delaTimeAnima;
			}
			else
			{
				this.goldratio = 0L;
			}
			if (Mathf.Abs((float)(this.curDiamondnum - this.targetDiamondnum)) > 1E-45f)
			{
				if (!this.DiamondAnima.enabled)
				{
					this.DiamondAnima.PlayForward();
				}
				this.diamondratio = (this.targetDiamondnum - this.curDiamondnum) / this.delaTimeAnima;
			}
			else
			{
				this.diamondratio = 0L;
			}
			if (Mathf.Abs((float)(this.curGangnum - this.targetGangnum)) > 1E-45f)
			{
				if (UnityVersionUtil.IsActive(this.GangObj) && !this.GangAnima.enabled)
				{
					this.GangAnima.PlayForward();
				}
				this.gangratio = (this.targetGangnum - this.curGangnum) / this.delaTimeAnima;
			}
			else
			{
				this.gangratio = 0L;
			}
		}
		else
		{
			this.showMoneyAnima = false;
			this.curCashnum = GameMoneyHelper.GetCash();
			this.curGoldnum = GameMoneyHelper.GetGold();
			this.curDiamondnum = GameMoneyHelper.GetDiamond();
			this.curGangnum = GameMoneyHelper.GetGuildContribute();
			this.targetCashnum = this.curCashnum;
			this.targetGoldnum = this.curGoldnum;
			this.targetDiamondnum = this.curDiamondnum;
			this.targetGangnum = this.curGangnum;
			this.DiamondLabel.text = string.Format("{0:N0}", this.curDiamondnum);
			this.GoldLabel.text = string.Format("{0:N0}", this.curGoldnum);
			this.CashLabel.text = string.Format("{0:N0}", this.curCashnum);
			this.GangLabel.text = string.Format("{0:N0}", this.curGangnum);
		}
	}

	// Token: 0x0600491D RID: 18717 RVA: 0x00179330 File Offset: 0x00177530
	public void StopMoneyAnima()
	{
		if (this.CashAnima != null)
		{
			if (this.CashAnima.mAmountPerDelta < 0f)
			{
				this.CashAnima.mAmountPerDelta = -this.CashAnima.mAmountPerDelta;
			}
			this.CashAnima.ResetToBeginning();
			this.CashAnima.enabled = false;
		}
		if (this.GoldAnima != null)
		{
			if (this.GoldAnima.mAmountPerDelta < 0f)
			{
				this.GoldAnima.mAmountPerDelta = -this.GoldAnima.mAmountPerDelta;
			}
			this.GoldAnima.ResetToBeginning();
			this.GoldAnima.enabled = false;
		}
		if (this.DiamondAnima != null)
		{
			if (this.DiamondAnima.mAmountPerDelta < 0f)
			{
				this.DiamondAnima.mAmountPerDelta = -this.DiamondAnima.mAmountPerDelta;
			}
			this.DiamondAnima.ResetToBeginning();
			this.DiamondAnima.enabled = false;
		}
		if (this.GangAnima != null)
		{
			if (this.GangAnima.mAmountPerDelta < 0f)
			{
				this.GangAnima.mAmountPerDelta = -this.GangAnima.mAmountPerDelta;
			}
			this.GangAnima.ResetToBeginning();
			this.GangAnima.enabled = false;
		}
		this.curDiamondnum = this.targetDiamondnum;
		this.curGoldnum = this.targetGoldnum;
		this.curCashnum = this.targetCashnum;
		this.curGangnum = this.targetGangnum;
		this.DiamondLabel.text = string.Format("{0:N0}", this.curDiamondnum);
		this.GoldLabel.text = string.Format("{0:N0}", this.curGoldnum);
		this.CashLabel.text = string.Format("{0:N0}", this.curCashnum);
		this.GangLabel.text = string.Format("{0:N0}", this.curGangnum);
	}

	// Token: 0x0600491E RID: 18718 RVA: 0x00179540 File Offset: 0x00177740
	public void OnClickDiamondBtn()
	{
		if (!this.CheckShopOpen())
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopShopRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopDiamondBuyRoot, delegate
		{
			SingletonUnity<PopDiamondBuyRootLogic>.Instance.EnableReset();
			ask_shop_list.request request = new ask_shop_list.request();
			request.type = 4L;
			request.subType = 1L;
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		}, null);
	}

	// Token: 0x0600491F RID: 18719 RVA: 0x00179598 File Offset: 0x00177798
	public void OnClickGoldBtn()
	{
		if (!this.CheckShopOpen())
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopDiamondBuyRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopShopRoot, delegate
		{
			SingletonUnity<PopShopRootLogic>.Instance.EnableReset();
			ask_shop_list.request request = new ask_shop_list.request();
			ItemData itemDataByID = DataManager.GetItemDataByID("5006");
			GameDefine.SHOP_TYPE itemShopType = itemDataByID.GetItemShopType();
			request.type = (long)itemShopType;
			request.itemId = "5006";
			request.subType = 1L;
			SingletonUnity<PopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		}, null);
	}

	// Token: 0x06004920 RID: 18720 RVA: 0x001795F0 File Offset: 0x001777F0
	public void OnClickCashBtn()
	{
		if (!this.CheckShopOpen())
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopDiamondBuyRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopShopRoot, delegate
		{
			SingletonUnity<PopShopRootLogic>.Instance.EnableReset();
			ask_shop_list.request request = new ask_shop_list.request();
			string text = GameMoneyHelper.GetShopMoneyItemID(GameDefine.MONEY_TYPE.CASH);
			if (string.IsNullOrEmpty(text))
			{
				text = "5001";
			}
			ItemData itemDataByID = DataManager.GetItemDataByID(text);
			GameDefine.SHOP_TYPE itemShopType = itemDataByID.GetItemShopType();
			request.type = (long)itemShopType;
			request.itemId = text;
			request.subType = 1L;
			SingletonUnity<PopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		}, null);
	}

	// Token: 0x06004921 RID: 18721 RVA: 0x00179648 File Offset: 0x00177848
	public void OnClickGangBtn()
	{
		NoticeLogic.AddNotifyData("#{200508}", true, false);
	}

	// Token: 0x06004922 RID: 18722 RVA: 0x00179658 File Offset: 0x00177858
	private bool CheckShopOpen()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (!playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP))
		{
			int condition = DataManager.GetFunctionDataById(3006.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100649}", new object[]
			{
				condition
			}), true, false);
			return false;
		}
		return true;
	}

	// Token: 0x06004923 RID: 18723 RVA: 0x001796BC File Offset: 0x001778BC
	public void SetPageLabel(string nameStr)
	{
		this.PageNameLabel.text = nameStr;
	}

	// Token: 0x06004924 RID: 18724 RVA: 0x001796CC File Offset: 0x001778CC
	public void ShowGangMoney()
	{
		NGUITools.SetActive(this.GangObj.gameObject, true);
		NGUITools.SetActive(this.VideoObjs.gameObject, false);
	}

	// Token: 0x06004925 RID: 18725 RVA: 0x001796FC File Offset: 0x001778FC
	public void hideGangMoney()
	{
		NGUITools.SetActive(this.GangObj.gameObject, false);
		NGUITools.SetActive(this.VideoObjs.gameObject, true);
		this.VideoBtnUpdate(SingletonDontDestoryUnity<GameManager>.Instance.IsUnityAdsReady);
	}

	// Token: 0x06004926 RID: 18726 RVA: 0x0017973C File Offset: 0x0017793C
	public void VideoBtnUpdate(bool enabled)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		bool flag = false;
		if (playerData.VideoTimes >= playerData.VideoMaxTimes)
		{
			flag = true;
		}
		for (int i = 0; i < this.VideoGetLabel.Length; i++)
		{
			this.VideoGetLabel[i].text = "+" + playerData.VideoDiamond;
		}
		if (enabled && !flag)
		{
			NGUITools.SetActive(this.VideoCanObj, true);
			NGUITools.SetActive(this.VideoNoObj, false);
		}
		else
		{
			NGUITools.SetActive(this.VideoCanObj, false);
			NGUITools.SetActive(this.VideoNoObj, true);
		}
	}

	// Token: 0x06004927 RID: 18727 RVA: 0x001797E8 File Offset: 0x001779E8
	public void OnClickVideoBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.IsUnityAdsReady)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.OnUnityAdsFinished = new GameManager.Void_Bool_Delegate(this.VideoFinish);
			SingletonDontDestoryUnity<GameManager>.Instance.ShowUnityAds();
		}
	}

	// Token: 0x06004928 RID: 18728 RVA: 0x0017981C File Offset: 0x00177A1C
	private void VideoFinish(bool IsFinish)
	{
		if (IsFinish)
		{
			watch_video_info.request rpcReq = new watch_video_info.request();
			NetLogic.GetInstance().Send<Protocol.watch_video_info>(rpcReq, null);
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.OnUnityAdsStateChange != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.OnUnityAdsStateChange(SingletonDontDestoryUnity<GameManager>.Instance.IsUnityAdsReady);
		}
	}

	// Token: 0x04003632 RID: 13874
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003633 RID: 13875
	public UISprite ExitBtn;

	// Token: 0x04003634 RID: 13876
	public UILabel DiamondLabel;

	// Token: 0x04003635 RID: 13877
	public UILabel GoldLabel;

	// Token: 0x04003636 RID: 13878
	public UILabel CashLabel;

	// Token: 0x04003637 RID: 13879
	public GameObject GangObj;

	// Token: 0x04003638 RID: 13880
	public UILabel GangLabel;

	// Token: 0x04003639 RID: 13881
	public UILabel PageNameLabel;

	// Token: 0x0400363A RID: 13882
	public GameObject LeftTabRoot;

	// Token: 0x0400363B RID: 13883
	public GameObject VideoObjs;

	// Token: 0x0400363C RID: 13884
	public GameObject VideoCanObj;

	// Token: 0x0400363D RID: 13885
	public GameObject VideoNoObj;

	// Token: 0x0400363E RID: 13886
	public UILabel[] VideoGetLabel;

	// Token: 0x0400363F RID: 13887
	public UIGrid LeftTabGrid;

	// Token: 0x04003640 RID: 13888
	public MenuBaseTabBtnLogic BtnPrefab;

	// Token: 0x04003641 RID: 13889
	public List<MenuBaseTabBtnLogic> mCurBtnList;

	// Token: 0x04003642 RID: 13890
	private DelegateDefine.NoParamDelegate onClickBackBtn;

	// Token: 0x04003643 RID: 13891
	private List<MenuTabBtnInfo> mCurMenuTabBtnInfoList;

	// Token: 0x04003644 RID: 13892
	public TweenScale CashAnima;

	// Token: 0x04003645 RID: 13893
	public TweenScale GoldAnima;

	// Token: 0x04003646 RID: 13894
	public TweenScale DiamondAnima;

	// Token: 0x04003647 RID: 13895
	public TweenScale GangAnima;

	// Token: 0x04003648 RID: 13896
	private long curCashnum;

	// Token: 0x04003649 RID: 13897
	private long curGoldnum;

	// Token: 0x0400364A RID: 13898
	private long curDiamondnum;

	// Token: 0x0400364B RID: 13899
	private long curGangnum;

	// Token: 0x0400364C RID: 13900
	private long targetCashnum;

	// Token: 0x0400364D RID: 13901
	private long targetGoldnum;

	// Token: 0x0400364E RID: 13902
	private long targetDiamondnum;

	// Token: 0x0400364F RID: 13903
	private long targetGangnum;

	// Token: 0x04003650 RID: 13904
	private bool showMoneyAnima;

	// Token: 0x04003651 RID: 13905
	private long cashratio;

	// Token: 0x04003652 RID: 13906
	private long goldratio;

	// Token: 0x04003653 RID: 13907
	private long diamondratio;

	// Token: 0x04003654 RID: 13908
	private long gangratio;

	// Token: 0x04003655 RID: 13909
	private bool[] isplayanima = new bool[3];

	// Token: 0x04003656 RID: 13910
	private long delaTimeAnima = 1L;

	// Token: 0x04003657 RID: 13911
	private float temptime;
}
