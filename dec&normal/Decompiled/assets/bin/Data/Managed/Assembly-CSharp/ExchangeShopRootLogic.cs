using System;
using System.Collections.Generic;

// Token: 0x0200098B RID: 2443
public class ExchangeShopRootLogic : SingletonUnity<ExchangeShopRootLogic>
{
	// Token: 0x06004517 RID: 17687 RVA: 0x0015A3B4 File Offset: 0x001585B4
	public void InitShopUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickBattleBtn), true, "CZ_left_duiZhanHuoBi", StrDictionary.GetDictionaryString("#{300012}", new object[0]), FUNCTION_TYPE.SHOP_BATTLE, null);
			list.Add(menuTabBtnInfo);
			if (this.isHaveActivityShop())
			{
				MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickActivityBtn), true, "CZ_left_huoDong", StrDictionary.GetDictionaryString("#{300013}", new object[0]), FUNCTION_TYPE.SHOP, null);
				list.Add(menuTabBtnInfo2);
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.curPageIndex = -1;
		}, null);
	}

	// Token: 0x06004518 RID: 17688 RVA: 0x0015A3D4 File Offset: 0x001585D4
	public bool isHaveActivityShop()
	{
		this.mShopDataList = DataManager.GetShopDataList();
		for (int i = 0; i < this.mShopDataList.Count; i++)
		{
			if (this.mShopDataList[i].Shop == 7 && TimeTools.IsTimeRange(this.mShopDataList[i].StartTimes, this.mShopDataList[i].EndTimes))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004519 RID: 17689 RVA: 0x0015A450 File Offset: 0x00158650
	public void Reset()
	{
		this.OnClickBattleBtn();
		SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap();
	}

	// Token: 0x0600451A RID: 17690 RVA: 0x0015A464 File Offset: 0x00158664
	public void ResetToEventShop()
	{
		if (this.isHaveActivityShop())
		{
			this.OnClickActivityBtn();
		}
		else
		{
			this.OnClickBattleBtn();
		}
	}

	// Token: 0x0600451B RID: 17691 RVA: 0x0015A484 File Offset: 0x00158684
	public void OnClickBattleBtn()
	{
		this.OnClickBattleBtn(GameDefine.SHOP_TAB_TYPE.INVALID);
	}

	// Token: 0x0600451C RID: 17692 RVA: 0x0015A490 File Offset: 0x00158690
	public void OnClickBattleBtn(GameDefine.SHOP_TAB_TYPE tarclass)
	{
		if (this.curPageIndex == 0)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopTabRootLogic, delegate
		{
			SingletonUnity<ShopTabRootLogic>.Instance.EnableReset(GameDefine.SHOP_TYPE.BATTLECOIN_SHOP, null);
			SingletonUnity<ShopTabRootLogic>.Instance.ClearSelectObj();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.curPageIndex = 0;
	}

	// Token: 0x0600451D RID: 17693 RVA: 0x0015A4E8 File Offset: 0x001586E8
	public void OnClickActivityBtn()
	{
		this.OnClickActivityBtn(GameDefine.SHOP_TAB_TYPE.INVALID);
	}

	// Token: 0x0600451E RID: 17694 RVA: 0x0015A4F4 File Offset: 0x001586F4
	public void OnClickActivityBtn(GameDefine.SHOP_TAB_TYPE tarclass)
	{
		if (this.curPageIndex == 1)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopTabRootLogic, delegate
		{
			SingletonUnity<ShopTabRootLogic>.Instance.EnableReset(GameDefine.SHOP_TYPE.ACTIVITY_SHOP, null);
			SingletonUnity<ShopTabRootLogic>.Instance.ClearSelectObj();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		this.curPageIndex = 1;
	}

	// Token: 0x0600451F RID: 17695 RVA: 0x0015A550 File Offset: 0x00158750
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShopTabRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ExchangeShopRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
	}

	// Token: 0x06004520 RID: 17696 RVA: 0x0015A58C File Offset: 0x0015878C
	private void OnEnable()
	{
		this.InitShopUI();
	}

	// Token: 0x04003207 RID: 12807
	private int curPageIndex = -1;

	// Token: 0x04003208 RID: 12808
	public List<ShopData> mShopDataList = new List<ShopData>();
}
