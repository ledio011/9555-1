using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200098A RID: 2442
public class BuyDiamondRootLogic : SingletonUnity<BuyDiamondRootLogic>
{
	// Token: 0x06004513 RID: 17683 RVA: 0x0015A1FC File Offset: 0x001583FC
	public void EnableReset()
	{
		for (int i = 0; i < this.BuyDiamondItemList.Count; i++)
		{
			NGUITools.SetActive(this.BuyDiamondItemList[i].gameObject, false);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_dollar", "open", "times");
	}

	// Token: 0x06004514 RID: 17684 RVA: 0x0015A258 File Offset: 0x00158458
	public void Reset(ret_ask_shop_list.request request)
	{
		this.mCurPurchaseDataList.Clear();
		for (int i = 0; i < request.shop_list.Count; i++)
		{
			this.mCurPurchaseDataList.Add(DataManager.GetPurchaseDataBuyId(request.shop_list[i].ID));
		}
		this.mCurPurchaseDataList.Sort(delegate(PurchaseData pre, PurchaseData next)
		{
			if (pre.Key.Length != next.Key.Length)
			{
				return pre.Key.Length - next.Key.Length;
			}
			return pre.Key.CompareTo(next.Key);
		});
		for (int j = 0; j < this.BuyDiamondItemList.Count; j++)
		{
			NGUITools.SetActive(this.BuyDiamondItemList[j].gameObject, j < this.mCurPurchaseDataList.Count);
			if (j < this.mCurPurchaseDataList.Count)
			{
				this.BuyDiamondItemList[j].Reset(this.mCurPurchaseDataList[j]);
			}
		}
	}

	// Token: 0x04003204 RID: 12804
	private List<PurchaseData> mCurPurchaseDataList = new List<PurchaseData>();

	// Token: 0x04003205 RID: 12805
	public List<BuyDiamondItem> BuyDiamondItemList = new List<BuyDiamondItem>();
}
