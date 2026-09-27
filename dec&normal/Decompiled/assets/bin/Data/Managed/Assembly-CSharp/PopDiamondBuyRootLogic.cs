using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200098D RID: 2445
public class PopDiamondBuyRootLogic : SingletonUnity<PopDiamondBuyRootLogic>
{
	// Token: 0x0600452E RID: 17710 RVA: 0x0015B010 File Offset: 0x00159210
	public void EnableReset()
	{
		for (int i = 0; i < this.BuyDiamondItemList.Count; i++)
		{
			NGUITools.SetActive(this.BuyDiamondItemList[i].gameObject, false);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_dollar", "open", "times");
	}

	// Token: 0x0600452F RID: 17711 RVA: 0x0015B06C File Offset: 0x0015926C
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

	// Token: 0x06004530 RID: 17712 RVA: 0x0015B158 File Offset: 0x00159358
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopDiamondBuyRoot);
	}

	// Token: 0x04003219 RID: 12825
	private List<PurchaseData> mCurPurchaseDataList = new List<PurchaseData>();

	// Token: 0x0400321A RID: 12826
	public List<BuyDiamondItem> BuyDiamondItemList = new List<BuyDiamondItem>();
}
