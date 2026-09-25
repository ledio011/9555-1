using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000989 RID: 2441
public class BuyDiamondItem : MonoBehaviour
{
	// Token: 0x06004510 RID: 17680 RVA: 0x0015A0A0 File Offset: 0x001582A0
	public void Reset(PurchaseData curData)
	{
		NGUITools.SetActive(this.AdFreePic.gameObject, curData.AdFree == 1 && !LocalDataSaveManager.AdFree);
		this.GetNumLabel.text = curData.ShowPriceCost.ToString();
		this.PriceLabel.text = string.Format("${0}", curData.Dollor);
		this.mCurPurchaseData = curData;
		if (this.mCurPurchaseData.BuyCount == 1)
		{
			if (!string.IsNullOrEmpty(this.mCurPurchaseData.SalePicName))
			{
				this.SaleInfoSp.spriteName = this.mCurPurchaseData.SalePicName;
				this.SaleInfoSp.MakePixelPerfect();
			}
			UnityVersionUtil.SetActiveRecursive(this.SaleInfoSp.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.SaleInfoSp.gameObject, false);
		}
	}

	// Token: 0x06004511 RID: 17681 RVA: 0x0015A17C File Offset: 0x0015837C
	public void OnClickItem()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(4, 1f, null);
		SingletonDontDestoryUnity<GameManager>.Instance.Billing(this.mCurPurchaseData.ProductId);
		if (GameSettingData.IsTestBilling)
		{
			check_purchase.request request = new check_purchase.request();
			request.productId = this.mCurPurchaseData.ProductId;
			NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
		}
	}

	// Token: 0x040031FF RID: 12799
	public UILabel GetNumLabel;

	// Token: 0x04003200 RID: 12800
	public UILabel PriceLabel;

	// Token: 0x04003201 RID: 12801
	public UISprite AdFreePic;

	// Token: 0x04003202 RID: 12802
	private PurchaseData mCurPurchaseData;

	// Token: 0x04003203 RID: 12803
	public UISprite SaleInfoSp;
}
