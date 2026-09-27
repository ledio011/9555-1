using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000930 RID: 2352
public class ConsignRootLogic : SingletonUnity<ConsignRootLogic>
{
	// Token: 0x06004158 RID: 16728 RVA: 0x00136B84 File Offset: 0x00134D84
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004159 RID: 16729 RVA: 0x00136B90 File Offset: 0x00134D90
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x0600415A RID: 16730 RVA: 0x00136BB0 File Offset: 0x00134DB0
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x17000F9E RID: 3998
	// (get) Token: 0x0600415B RID: 16731 RVA: 0x00136BBC File Offset: 0x00134DBC
	public int CurrentPage
	{
		get
		{
			return this.currentPage;
		}
	}

	// Token: 0x0600415C RID: 16732 RVA: 0x00136BC4 File Offset: 0x00134DC4
	private void OnEnable()
	{
		this.currentPage = 0;
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> leftBtnInfo = new List<MenuTabBtnInfo>();
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(leftBtnInfo, new DelegateDefine.NoParamDelegate(this.CloseUI), true, StrDictionary.GetDictionaryString("#{100105}", new object[0]));
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Trade", "open", "opentimes");
	}

	// Token: 0x0600415D RID: 16733 RVA: 0x00136C10 File Offset: 0x00134E10
	private void ChangeTab(int subIndex = 0)
	{
		switch (this.currentPage)
		{
		case 0:
		{
			UnityVersionUtil.SetActiveRecursive(this.consignBuyLogic.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.consignOnSaleLogic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.consignSellLogic.gameObject, false);
			this.consignBuyLogic.Reset();
			this.SelectFlag.transform.parent = this.Tab1Obj.transform;
			Vector3 localPosition = this.SelectFlag.transform.localPosition;
			localPosition.x = 0f;
			this.SelectFlag.transform.localPosition = localPosition;
			this.BuyLabelState.active = true;
			this.SaleLabelState.active = false;
			this.SellLabelState.active = false;
			break;
		}
		case 1:
		{
			UnityVersionUtil.SetActiveRecursive(this.consignBuyLogic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.consignOnSaleLogic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.consignSellLogic.gameObject, true);
			this.consignSellLogic.Reset(subIndex);
			this.SelectFlag.transform.parent = this.Tab2Obj.transform;
			Vector3 localPosition2 = this.SelectFlag.transform.localPosition;
			localPosition2.x = 0f;
			this.SelectFlag.transform.localPosition = localPosition2;
			this.BuyLabelState.active = false;
			this.SaleLabelState.active = false;
			this.SellLabelState.active = true;
			break;
		}
		case 2:
		{
			UnityVersionUtil.SetActiveRecursive(this.consignBuyLogic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.consignOnSaleLogic.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.consignSellLogic.gameObject, false);
			this.RequestOnSaleList();
			this.consignOnSaleLogic.Reset();
			this.SelectFlag.transform.parent = this.Tab3Obj.transform;
			Vector3 localPosition3 = this.SelectFlag.transform.localPosition;
			localPosition3.x = 0f;
			this.SelectFlag.transform.localPosition = localPosition3;
			this.BuyLabelState.active = false;
			this.SaleLabelState.active = true;
			this.SellLabelState.active = false;
			break;
		}
		}
	}

	// Token: 0x0600415E RID: 16734 RVA: 0x00136E54 File Offset: 0x00135054
	public void RefreshOnSaleNow()
	{
		if (this.currentPage == 2)
		{
			this.consignOnSaleLogic.Reset();
		}
	}

	// Token: 0x0600415F RID: 16735 RVA: 0x00136E70 File Offset: 0x00135070
	public void RefreshOnBuyList(ret_consign_ask_items_info.request request)
	{
		if (this.currentPage == 0)
		{
			this.consignBuyLogic.UpdateList(request);
		}
	}

	// Token: 0x06004160 RID: 16736 RVA: 0x00136E8C File Offset: 0x0013508C
	public void BuySuccess(long id)
	{
		if (this.currentPage == 0)
		{
			this.consignBuyLogic.BuySuccess(id);
		}
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x00136EA8 File Offset: 0x001350A8
	public void SellSuccess()
	{
		if (this.currentPage == 1)
		{
			this.consignSellLogic.UpdateInfo(false);
		}
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x00136EC4 File Offset: 0x001350C4
	private void RequestOnSaleList()
	{
		consign_ask_my_items.request rpcReq = new consign_ask_my_items.request();
		NetLogic.GetInstance().Send<Protocol.consign_ask_my_items>(rpcReq, null);
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x00136EE4 File Offset: 0x001350E4
	public void Reset()
	{
		this.ChangeTab(0);
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x00136EF0 File Offset: 0x001350F0
	public void Reset(int tapIndex, int subIndex = 0)
	{
		this.currentPage = tapIndex;
		this.ChangeTab(subIndex);
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x00136F00 File Offset: 0x00135100
	public void ClickTab1()
	{
		if (this.currentPage != 0)
		{
			this.currentPage = 0;
			this.ChangeTab(0);
		}
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x00136F1C File Offset: 0x0013511C
	public void ClickTab2()
	{
		if (this.currentPage != 1)
		{
			this.currentPage = 1;
			this.ChangeTab(0);
			if (TutorialManager.CurStep == TUTORIAL_STEP.SELL_ITEM_CLICK_TAB)
			{
				this.CheckTutorialEvent();
			}
		}
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x00136F50 File Offset: 0x00135150
	public void ClickTab3()
	{
		if (this.currentPage != 2)
		{
			this.currentPage = 2;
			this.ChangeTab(0);
		}
	}

	// Token: 0x06004168 RID: 16744 RVA: 0x00136F6C File Offset: 0x0013516C
	public void CloseUI()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ConsignUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
	}

	// Token: 0x04002D20 RID: 11552
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002D21 RID: 11553
	private int currentPage;

	// Token: 0x04002D22 RID: 11554
	public ConsignBuyLogic consignBuyLogic;

	// Token: 0x04002D23 RID: 11555
	public ConsignOnSaleLogic consignOnSaleLogic;

	// Token: 0x04002D24 RID: 11556
	public ConsignSellLogic consignSellLogic;

	// Token: 0x04002D25 RID: 11557
	public GameObject SelectFlag;

	// Token: 0x04002D26 RID: 11558
	public LabelState BuyLabelState;

	// Token: 0x04002D27 RID: 11559
	public LabelState SaleLabelState;

	// Token: 0x04002D28 RID: 11560
	public LabelState SellLabelState;

	// Token: 0x04002D29 RID: 11561
	public GameObject Tab1Obj;

	// Token: 0x04002D2A RID: 11562
	public GameObject Tab2Obj;

	// Token: 0x04002D2B RID: 11563
	public GameObject Tab3Obj;
}
