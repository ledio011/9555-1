using System;
using System.Collections.Generic;

// Token: 0x020008D7 RID: 2263
public class CommercialUIRootLogic : SingletonUnity<CommercialUIRootLogic>
{
	// Token: 0x06003D08 RID: 15624 RVA: 0x0010EB80 File Offset: 0x0010CD80
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			ActivityData activityData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			WelfareData welfareData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData;
			this.commdata = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickMonthBtn), true, "CZ_left_yueQian", StrDictionary.GetDictionaryString("#{300101}", new object[0]), FUNCTION_TYPE.GIFT_CHECK, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveMonthTips));
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickDailyBuyBtn), true, "CZ_left_1yuan", StrDictionary.GetDictionaryString("#{300102}", new object[0]), FUNCTION_TYPE.GIFT_DAILY, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveDailyBuyTips));
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickInvestBtn), true, "CZ_left_TouZi", StrDictionary.GetDictionaryString("#{300103}", new object[0]), FUNCTION_TYPE.GIFT_INVEST, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveInvestTips));
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickWeekBtn), true, "CZ_left_7Days", StrDictionary.GetDictionaryString("#{300104}", new object[0]), FUNCTION_TYPE.GIFT_7DAY, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveWeekTips));
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickLevelBtn), true, "CZ_left_DengJiLiBao", StrDictionary.GetDictionaryString("#{300106}", new object[0]), FUNCTION_TYPE.GIFT_LEVEL, new DelegateDefine.NoParamReturnDelegate(welfareData.IsHaveLevelReward));
			MenuTabBtnInfo menuTabBtnInfo6 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickRetrieveBtn), true, "CZ_left_zhaoHui", StrDictionary.GetDictionaryString("#{301104}", new object[0]), FUNCTION_TYPE.GIFT_RETRIEVE, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveRetrieveTips));
			if ((this.commdata.Push & 8L) != 0L)
			{
				list.Add(menuTabBtnInfo);
				list.Add(menuTabBtnInfo2);
				list.Add(menuTabBtnInfo3);
				list.Add(menuTabBtnInfo5);
				list.Add(menuTabBtnInfo6);
			}
			else
			{
				list.Add(menuTabBtnInfo);
				list.Add(menuTabBtnInfo2);
				list.Add(menuTabBtnInfo3);
				list.Add(menuTabBtnInfo4);
				list.Add(menuTabBtnInfo5);
				list.Add(menuTabBtnInfo6);
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curPageIndex = -1;
		}, null);
	}

	// Token: 0x06003D09 RID: 15625 RVA: 0x0010EBA0 File Offset: 0x0010CDA0
	public void Reset()
	{
		if (!SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap())
		{
			this.OnClickMonthBtn();
		}
	}

	// Token: 0x06003D0A RID: 15626 RVA: 0x0010EBB8 File Offset: 0x0010CDB8
	public void OnClickMonthBtn()
	{
		if (this.curPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_CHECK))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.InvestRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyBuyPackRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RetrieveRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SignMonthRoot, delegate
		{
			SingletonUnity<SignMonthRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(252, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_30_day_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_CHECK));
		this.curPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_CHECK);
	}

	// Token: 0x06003D0B RID: 15627 RVA: 0x0010EC7C File Offset: 0x0010CE7C
	public void OnClickDailyBuyBtn()
	{
		if (this.curPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_DAILY))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignMonthRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.InvestRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RetrieveRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DailyBuyPackRoot, delegate
		{
			SingletonUnity<DailyBuyPackRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(258, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_buy>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_DAILY));
		this.curPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_DAILY);
		if (LocalDataSaveManager.ShowDailyBuyTips)
		{
			LocalDataSaveManager.ShowDailyBuyTips = false;
			if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
			}
		}
	}

	// Token: 0x06003D0C RID: 15628 RVA: 0x0010ED78 File Offset: 0x0010CF78
	public void OnClickInvestBtn()
	{
		if (this.curPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_INVEST))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignMonthRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyBuyPackRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RetrieveRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.InvestRewardRoot, delegate
		{
			SingletonUnity<InvestRewardRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(257, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_invest_pack>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_INVEST));
		this.curPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_INVEST);
		if (LocalDataSaveManager.ShowInvestTips)
		{
			LocalDataSaveManager.ShowInvestTips = false;
			if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
			}
		}
	}

	// Token: 0x06003D0D RID: 15629 RVA: 0x0010EE74 File Offset: 0x0010D074
	public void OnClickWeekBtn()
	{
		if (this.curPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_7DAY))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignMonthRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.InvestRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyBuyPackRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RetrieveRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SignWeekRoot, delegate
		{
			SingletonUnity<SignWeekRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(253, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_week_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_7DAY));
		this.curPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_7DAY);
	}

	// Token: 0x06003D0E RID: 15630 RVA: 0x0010EF38 File Offset: 0x0010D138
	public void OnClickLevelBtn()
	{
		if (this.curPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_LEVEL))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignMonthRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.InvestRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyBuyPackRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RetrieveRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LevelRewardRoot, delegate
		{
			SingletonUnity<LevelRewardRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(296, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.req_level_reward>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_LEVEL));
		this.curPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_LEVEL);
	}

	// Token: 0x06003D0F RID: 15631 RVA: 0x0010EFFC File Offset: 0x0010D1FC
	public void OnClickRetrieveBtn()
	{
		if (this.curPageIndex == this.GetPageIndex(FUNCTION_TYPE.GIFT_RETRIEVE))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignMonthRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.InvestRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyBuyPackRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RetrieveRoot, delegate
		{
			SingletonUnity<RetrieveRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(278, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_retrieve_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(this.GetPageIndex(FUNCTION_TYPE.GIFT_RETRIEVE));
		this.curPageIndex = this.GetPageIndex(FUNCTION_TYPE.GIFT_RETRIEVE);
	}

	// Token: 0x06003D10 RID: 15632 RVA: 0x0010F0C0 File Offset: 0x0010D2C0
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CommercialUIRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignMonthRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.InvestRewardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyBuyPackRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RetrieveRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x06003D11 RID: 15633 RVA: 0x0010F170 File Offset: 0x0010D370
	public int GetPageIndex(FUNCTION_TYPE type)
	{
		if ((this.commdata.Push & 8L) != 0L)
		{
			switch (type)
			{
			case FUNCTION_TYPE.GIFT_CHECK:
				return 0;
			case FUNCTION_TYPE.GIFT_DAILY:
				return 1;
			case FUNCTION_TYPE.GIFT_INVEST:
				return 2;
			default:
				if (type == FUNCTION_TYPE.GIFT_RETRIEVE)
				{
					return 4;
				}
				break;
			case FUNCTION_TYPE.GIFT_LEVEL:
				return 3;
			}
		}
		else
		{
			switch (type)
			{
			case FUNCTION_TYPE.GIFT_CHECK:
				return 0;
			case FUNCTION_TYPE.GIFT_DAILY:
				return 1;
			case FUNCTION_TYPE.GIFT_INVEST:
				return 2;
			case FUNCTION_TYPE.GIFT_7DAY:
				return 3;
			default:
				if (type == FUNCTION_TYPE.GIFT_RETRIEVE)
				{
					return 5;
				}
				break;
			case FUNCTION_TYPE.GIFT_LEVEL:
				return 4;
			}
		}
		return 0;
	}

	// Token: 0x04002873 RID: 10355
	private int curPageIndex = -1;

	// Token: 0x04002874 RID: 10356
	private PlayerCommonData commdata;
}
