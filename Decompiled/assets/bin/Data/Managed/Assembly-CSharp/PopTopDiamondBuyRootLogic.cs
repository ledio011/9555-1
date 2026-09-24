using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200098E RID: 2446
public class PopTopDiamondBuyRootLogic : SingletonUnity<PopTopDiamondBuyRootLogic>
{
	// Token: 0x06004533 RID: 17715 RVA: 0x0015B1A0 File Offset: 0x001593A0
	public void EnableReset()
	{
		for (int i = 0; i < this.BuyDiamondItemList.Count; i++)
		{
			NGUITools.SetActive(this.BuyDiamondItemList[i].gameObject, false);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_dollar", "open", "times");
	}

	// Token: 0x06004534 RID: 17716 RVA: 0x0015B1FC File Offset: 0x001593FC
	public void Reset(ret_ask_shop_list.request request)
	{
		this.mCurPurchaseDataList.Clear();
		for (int i = 0; i < request.shop_list.Count; i++)
		{
			this.mCurPurchaseDataList.Add(DataManager.GetPurchaseDataBuyId(request.shop_list[i].ID));
		}
		this.mCurPurchaseDataList.Sort((PurchaseData pre, PurchaseData next) => pre.Key.CompareTo(next.Key));
		for (int j = 0; j < this.BuyDiamondItemList.Count; j++)
		{
			NGUITools.SetActive(this.BuyDiamondItemList[j].gameObject, j < this.mCurPurchaseDataList.Count);
			if (j < this.mCurPurchaseDataList.Count)
			{
				this.BuyDiamondItemList[j].Reset(this.mCurPurchaseDataList[j]);
			}
		}
	}

	// Token: 0x06004535 RID: 17717 RVA: 0x0015B2E8 File Offset: 0x001594E8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopDiamondBuyRoot);
	}

	// Token: 0x0400321C RID: 12828
	private List<PurchaseData> mCurPurchaseDataList = new List<PurchaseData>();

	// Token: 0x0400321D RID: 12829
	public List<BuyDiamondItem> BuyDiamondItemList = new List<BuyDiamondItem>();
}
