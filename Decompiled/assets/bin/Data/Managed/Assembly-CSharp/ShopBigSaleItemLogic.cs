using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A1D RID: 2589
public class ShopBigSaleItemLogic : MonoBehaviour
{
	// Token: 0x06004AAA RID: 19114 RVA: 0x001892B4 File Offset: 0x001874B4
	public void updateinfo(special_big_pack curinfo)
	{
		if (curinfo.ID.Equals(this.CurInfo.ID))
		{
			this.CurInfo = curinfo;
		}
	}

	// Token: 0x06004AAB RID: 19115 RVA: 0x001892E4 File Offset: 0x001874E4
	public void Reset(special_big_pack curinfo, ShopBigSaleItemLogic.OnClickShopBigSaleItemDelegate clickitem = null)
	{
		this.CurInfo = curinfo;
		this.onClickShopItem = clickitem;
		BigPackageData bigPackageDataById = DataManager.GetBigPackageDataById(this.CurInfo.ID);
		if (bigPackageDataById == null)
		{
			return;
		}
		this.Icon.spriteName = bigPackageDataById.Icon;
		this.ItemQualitySprite.spriteName = ((EQUIP_QUALITY)bigPackageDataById.IconQuality).ToString();
		this.NameLabel.text = StrDictionary.GetDictionaryString(bigPackageDataById.Name, new object[0]);
		if (string.IsNullOrEmpty(bigPackageDataById.ProductId))
		{
			this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(bigPackageDataById.PriceCost, bigPackageDataById.PriceType);
		}
		else
		{
			this.PriceLabel.text = string.Format("${0}", bigPackageDataById.Dollor);
		}
		if (bigPackageDataById.Discount < 100 && bigPackageDataById.Discount != 0)
		{
			this.saleValueLabel.text = string.Format("{0}", bigPackageDataById.Discount);
			UnityVersionUtil.SetActiveRecursive(this.ForSaleObj, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ForSaleObj, false);
		}
		this.ExpireFlag.enabled = false;
		this.isHaveLimittime = false;
		if (!string.IsNullOrEmpty(bigPackageDataById.TimeList))
		{
			TimeSpan shopItemTime = TimeTools.GetShopItemTime(bigPackageDataById.GetCurTimeEnd());
			this.limitTime = (float)shopItemTime.TotalSeconds;
			this.isHaveLimittime = true;
			this.starttime = Time.time;
			if (shopItemTime.Days > 0)
			{
				this.limittimelabel.text = string.Format("{0}D", shopItemTime.Days + 1);
			}
			else if (shopItemTime.Hours > 0)
			{
				this.limittimelabel.text = string.Format("{0}H", shopItemTime.Hours + 1);
			}
			else if (shopItemTime.Minutes > 0)
			{
				this.limittimelabel.text = string.Format("{0}M", shopItemTime.Minutes + 1);
			}
			else if (shopItemTime.Seconds > 0)
			{
				this.limittimelabel.text = string.Format("{0}S", shopItemTime.Seconds);
			}
			if (shopItemTime.TotalSeconds <= 0.0)
			{
				this.limittimelabel.text = "0S";
			}
			NGUITools.SetActive(this.limittimeobj.gameObject, true);
		}
		else
		{
			NGUITools.SetActive(this.limittimeobj.gameObject, false);
		}
	}

	// Token: 0x06004AAC RID: 19116 RVA: 0x00189568 File Offset: 0x00187768
	public void OnClickItemBtn()
	{
		if (this.onClickShopItem != null)
		{
			this.onClickShopItem(this.CurInfo, base.gameObject);
		}
	}

	// Token: 0x06004AAD RID: 19117 RVA: 0x00189598 File Offset: 0x00187798
	public void UpdateSelect(special_big_pack item)
	{
		if (this.CurInfo != null && this.CurInfo.ID.Equals(item.ID))
		{
			this.BkSprite.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.BkSprite.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x06004AAE RID: 19118 RVA: 0x001895F0 File Offset: 0x001877F0
	private void Update()
	{
		if (this.isHaveLimittime)
		{
			this.limitTime -= Time.deltaTime;
			TimeSpan timeSpan;
			timeSpan..ctor(0, 0, (int)this.limitTime);
			if (timeSpan.Days > 0)
			{
				this.limittimelabel.text = string.Format("{0}D", timeSpan.Days + 1);
			}
			else if (timeSpan.Hours > 0)
			{
				this.limittimelabel.text = string.Format("{0}H", timeSpan.Hours + 1);
			}
			else if (timeSpan.Minutes > 0)
			{
				this.limittimelabel.text = string.Format("{0}M", timeSpan.Minutes + 1);
			}
			else if (timeSpan.Seconds > 0)
			{
				this.limittimelabel.text = string.Format("{0}S", timeSpan.Seconds);
			}
			if (timeSpan.TotalSeconds <= 0.0)
			{
				this.limittimelabel.text = "0S";
				this.isHaveLimittime = false;
			}
		}
	}

	// Token: 0x04003841 RID: 14401
	public ShopBigSaleItemLogic.OnClickShopBigSaleItemDelegate onClickShopItem;

	// Token: 0x04003842 RID: 14402
	public UISprite Icon;

	// Token: 0x04003843 RID: 14403
	public UILabel NameLabel;

	// Token: 0x04003844 RID: 14404
	public UILabel PriceLabel;

	// Token: 0x04003845 RID: 14405
	public UISprite ItemQualitySprite;

	// Token: 0x04003846 RID: 14406
	public UISprite BkSprite;

	// Token: 0x04003847 RID: 14407
	public GameObject ForSaleObj;

	// Token: 0x04003848 RID: 14408
	public UILabel saleValueLabel;

	// Token: 0x04003849 RID: 14409
	public UISprite ExpireFlag;

	// Token: 0x0400384A RID: 14410
	public UILabel limittimelabel;

	// Token: 0x0400384B RID: 14411
	public GameObject limittimeobj;

	// Token: 0x0400384C RID: 14412
	private bool isHaveLimittime;

	// Token: 0x0400384D RID: 14413
	private float starttime;

	// Token: 0x0400384E RID: 14414
	private float limitTime;

	// Token: 0x0400384F RID: 14415
	private special_big_pack CurInfo;

	// Token: 0x02000B01 RID: 2817
	// (Invoke) Token: 0x0600508D RID: 20621
	public delegate void OnClickShopBigSaleItemDelegate(special_big_pack info, GameObject itemobj);
}
