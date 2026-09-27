using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200098F RID: 2447
public class ShopUIRootLogic : SingletonUnity<ShopUIRootLogic>
{
	// Token: 0x06004538 RID: 17720 RVA: 0x0015B320 File Offset: 0x00159520
	public void InitShopUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			WelfareData welfareData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickBuyDiamondBtn), true, "CZ_left_ShangDian_ChongZhi", StrDictionary.GetDictionaryString("#{300005}", new object[0]), FUNCTION_TYPE.SHOP_BUY, null);
			list.Add(menuTabBtnInfo);
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickToolsBtn), true, "CZ_left_BadgeUp", StrDictionary.GetDictionaryString("#{300001}", new object[0]), FUNCTION_TYPE.SHOP_TOOL, null);
			list.Add(menuTabBtnInfo2);
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickEquipBtn), true, "CZ_left_Equipment", StrDictionary.GetDictionaryString("#{300002}", new object[0]), FUNCTION_TYPE.SHOP_EQUIP, null);
			list.Add(menuTabBtnInfo3);
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickBigSaleBtn), true, "CZ_left_1yuan", StrDictionary.GetDictionaryString("#{300003}", new object[0]), FUNCTION_TYPE.SHOP_BIGSALE, null);
			list.Add(menuTabBtnInfo4);
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickGuildBtn), true, "CZ_left_ShangDian_GongHui", StrDictionary.GetDictionaryString("#{300004}", new object[0]), FUNCTION_TYPE.SHOP_GUILD, null);
			list.Add(menuTabBtnInfo5);
			MenuTabBtnInfo menuTabBtnInfo6 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickMonthlyCardBtn), true, "CZ_left_yueKa", StrDictionary.GetDictionaryString("#{300707}", new object[0]), FUNCTION_TYPE.SHOP_VIP, new DelegateDefine.NoParamReturnDelegate(welfareData.HaveVipTips));
			list.Add(menuTabBtnInfo6);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curPageIndex = -1;
		}, null);
	}

	// Token: 0x06004539 RID: 17721 RVA: 0x0015B340 File Offset: 0x00159540
	public void Reset()
	{
		this.OnClickBuyDiamondBtn();
		SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap();
	}

	// Token: 0x0600453A RID: 17722 RVA: 0x0015B354 File Offset: 0x00159554
	public void OnClickMonthlyCardBtn()
	{
		if (this.curPageIndex == 5)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BuyDiamondRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopBigSaleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MonthlyCardRoot, delegate
		{
			SingletonUnity<MonthlyCardRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(299, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.require_vip_info>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(5);
		this.curPageIndex = 5;
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x0015B3DC File Offset: 0x001595DC
	public void OnClickToolsBtn()
	{
		this.OnClickToolsBtn(GameDefine.SHOP_TAB_TYPE.INVALID, GameDefine.UIBACKTYPE.NOTHINTG, null);
	}

	// Token: 0x0600453C RID: 17724 RVA: 0x0015B3E8 File Offset: 0x001595E8
	public void OnClickToolsBtn(GameDefine.SHOP_TAB_TYPE tarclass, GameDefine.UIBACKTYPE needback = GameDefine.UIBACKTYPE.NOTHINTG, string needitemid = null)
	{
		if (this.curPageIndex == 1)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BuyDiamondRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MonthlyCardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopBigSaleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopTabRootLogic, delegate
		{
			SingletonUnity<ShopTabRootLogic>.Instance.EnableReset(GameDefine.SHOP_TYPE.TOOL_SHOP, needitemid);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		this.curPageIndex = 1;
		this.curBackType = needback;
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x0015B474 File Offset: 0x00159674
	public void OnClickEquipBtn()
	{
		this.OnClickEquipBtn(GameDefine.SHOP_TAB_TYPE.INVALID, GameDefine.UIBACKTYPE.NOTHINTG, null);
	}

	// Token: 0x0600453E RID: 17726 RVA: 0x0015B480 File Offset: 0x00159680
	public void OnClickEquipBtn(GameDefine.SHOP_TAB_TYPE tarclass, GameDefine.UIBACKTYPE needback = GameDefine.UIBACKTYPE.NOTHINTG, string needitemid = null)
	{
		if (this.curPageIndex == 2)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BuyDiamondRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MonthlyCardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopBigSaleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopTabRootLogic, delegate
		{
			SingletonUnity<ShopTabRootLogic>.Instance.EnableReset(GameDefine.SHOP_TYPE.EQUIP_SHOP, needitemid);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(2);
		this.curPageIndex = 2;
		this.curBackType = needback;
	}

	// Token: 0x0600453F RID: 17727 RVA: 0x0015B50C File Offset: 0x0015970C
	public void OnClickBigSaleBtn()
	{
		this.OnClickBigSaleBtn(GameDefine.SHOP_TAB_TYPE.INVALID, GameDefine.UIBACKTYPE.NOTHINTG, null);
	}

	// Token: 0x06004540 RID: 17728 RVA: 0x0015B518 File Offset: 0x00159718
	public void OnClickBigSaleBtn(GameDefine.SHOP_TAB_TYPE tarclass, GameDefine.UIBACKTYPE needback = GameDefine.UIBACKTYPE.NOTHINTG, string needitemid = null)
	{
		if (this.curPageIndex == 3)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BuyDiamondRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MonthlyCardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopBigSaleRoot, delegate
		{
			SingletonUnity<ShopBigSaleRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(274, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_special_big_pack>(null, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
		this.curPageIndex = 3;
		this.curBackType = needback;
	}

	// Token: 0x06004541 RID: 17729 RVA: 0x0015B5A8 File Offset: 0x001597A8
	public void OnClickGuildBtn()
	{
		this.OnClickGuildBtn(GameDefine.SHOP_TAB_TYPE.INVALID, GameDefine.UIBACKTYPE.NOTHINTG, null);
	}

	// Token: 0x06004542 RID: 17730 RVA: 0x0015B5B4 File Offset: 0x001597B4
	public void OnClickGuildBtn(GameDefine.SHOP_TAB_TYPE tarclass, GameDefine.UIBACKTYPE needback = GameDefine.UIBACKTYPE.NOTHINTG, string needitemid = null)
	{
		if (this.curPageIndex == 4)
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BuyDiamondRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MonthlyCardRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopBigSaleRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopTabRootLogic, delegate
			{
				SingletonUnity<ShopTabRootLogic>.Instance.EnableReset(GameDefine.SHOP_TYPE.GUILD_SHOP, needitemid);
			}, null);
			SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(4);
			this.curPageIndex = 4;
			this.curBackType = needback;
		}
		else
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
		}
	}

	// Token: 0x06004543 RID: 17731 RVA: 0x0015B664 File Offset: 0x00159864
	public void OnClickBuyDiamondBtn()
	{
		this.OnClickBuyDiamondBtn(GameDefine.UIBACKTYPE.NOTHINTG);
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x0015B670 File Offset: 0x00159870
	public void OnClickBuyDiamondBtn(GameDefine.UIBACKTYPE needback)
	{
		if (this.curPageIndex == 0)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MonthlyCardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopBigSaleRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BuyDiamondRoot, delegate
		{
			SingletonUnity<BuyDiamondRootLogic>.Instance.EnableReset();
			ask_shop_list.request request = new ask_shop_list.request();
			request.type = 4L;
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.curPageIndex = 0;
		this.curBackType = needback;
	}

	// Token: 0x06004545 RID: 17733 RVA: 0x0015B6FC File Offset: 0x001598FC
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BuyDiamondRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MonthlyCardRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopBigSaleRoot);
		if (this.curBackType == GameDefine.UIBACKTYPE.FASHION)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickFashionBackPackBtn();
			}, null);
		}
		else if (this.curBackType == GameDefine.UIBACKTYPE.EQUIP)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickEquipBackPackBtn();
			}, null);
		}
		else if (this.curBackType == GameDefine.UIBACKTYPE.BADGE)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickBadgeBtn();
			}, null);
		}
		else if (this.curBackType == GameDefine.UIBACKTYPE.ENHANCE)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.Reset();
			}, null);
		}
	}

	// Token: 0x06004546 RID: 17734 RVA: 0x0015B858 File Offset: 0x00159A58
	private void OnEnable()
	{
		this.InitShopUI();
	}

	// Token: 0x0400321F RID: 12831
	private int curPageIndex = -1;

	// Token: 0x04003220 RID: 12832
	private GameDefine.UIBACKTYPE curBackType;
}
