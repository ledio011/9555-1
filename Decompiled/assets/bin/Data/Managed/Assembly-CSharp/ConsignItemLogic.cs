using System;
using SprotoType;
using UnityEngine;

// Token: 0x0200092C RID: 2348
public class ConsignItemLogic : MonoBehaviour
{
	// Token: 0x06004141 RID: 16705 RVA: 0x00136244 File Offset: 0x00134444
	public string GetTimeShow()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		long num = playerCommonData.GetCurServerTime() - this.currentConsignItem.startTime;
		long num2 = (long)((1 << (int)this.currentConsignItem.time) * 12 * 60 * 60) - num;
		long num3 = num2 / 86400L;
		long num4 = (num2 - num3 * 60L * 60L * 24L) / 3600L;
		long num5 = num2 % 60L;
		if (num3 > 0L && num4 > 0L)
		{
			return string.Format("{0}d{1}h", num3, num4);
		}
		if (num3 > 0L)
		{
			return string.Format("{0}d", num3);
		}
		if (num4 > 0L)
		{
			return string.Format("{0}h", num4);
		}
		if (num5 > 0L)
		{
			return string.Format("{0}m", num5);
		}
		return "expire";
	}

	// Token: 0x06004142 RID: 16706 RVA: 0x00136344 File Offset: 0x00134544
	public void Reset(consign_item item)
	{
		this.currentConsignItem = item;
		ItemData itemDataByID = DataManager.GetItemDataByID(this.currentConsignItem.itemId);
		this.ItemIconSprite.spriteName = itemDataByID.BackPackIcon;
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.ItemIconQualitySprite.spriteName = ((EQUIP_QUALITY)this.currentConsignItem.quality).ToString();
		}
		else
		{
			this.ItemIconQualitySprite.spriteName = itemDataByID.QualityType.ToString();
		}
		if (item.stack > 1L)
		{
			this.SellCountLabel.text = this.currentConsignItem.stack.ToString();
		}
		else
		{
			this.SellCountLabel.text = string.Empty;
		}
		this.ItemNameLabel.text = itemDataByID.MName;
		this.PriceLabel.text = this.currentConsignItem.price.ToString();
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			BadgeData badgeDataById = DataManager.GetBadgeDataById(itemDataByID.ID);
			this.UseLevelLabel.text = badgeDataById.Lv.ToString();
		}
		else
		{
			this.UseLevelLabel.text = itemDataByID.Level.ToString();
		}
		this.TimeLeftLabel.text = this.GetTimeShow();
		if (this.currentConsignItem.characterId == Singleton<ObjManager>.Instance.MainPlayer.ServerId)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{101218}", new object[0]);
		}
		else
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{101201}", new object[0]);
		}
	}

	// Token: 0x06004143 RID: 16707 RVA: 0x001364EC File Offset: 0x001346EC
	public void ClickUndoBtn()
	{
		if (this.currentConsignItem.characterId == Singleton<ObjManager>.Instance.MainPlayer.ServerId)
		{
			consign_cancel_sale.request request = new consign_cancel_sale.request();
			request.id = this.currentConsignItem.id;
			NetLogic.GetInstance().Send<Protocol.consign_cancel_sale>(request, null);
		}
		else if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.DIAMOND, (int)this.currentConsignItem.price))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ConsignBuyCheckRoot, delegate
			{
				SingletonUnity<ConsignBuyCheckRoot>.Instance.Reset(this.currentConsignItem);
			}, null);
		}
	}

	// Token: 0x06004144 RID: 16708 RVA: 0x00136574 File Offset: 0x00134774
	public void OnClickItem()
	{
		ItemInfoRootLogicNew.ShowItemTips(this.currentConsignItem);
	}

	// Token: 0x06004145 RID: 16709 RVA: 0x00136584 File Offset: 0x00134784
	private void Start()
	{
	}

	// Token: 0x06004146 RID: 16710 RVA: 0x00136588 File Offset: 0x00134788
	private void Update()
	{
	}

	// Token: 0x04002D06 RID: 11526
	public UILabel SellCountLabel;

	// Token: 0x04002D07 RID: 11527
	public UILabel UseLevelLabel;

	// Token: 0x04002D08 RID: 11528
	public UILabel PriceLabel;

	// Token: 0x04002D09 RID: 11529
	public UILabel TimeLeftLabel;

	// Token: 0x04002D0A RID: 11530
	public UISprite ItemIconSprite;

	// Token: 0x04002D0B RID: 11531
	public UISprite ItemIconQualitySprite;

	// Token: 0x04002D0C RID: 11532
	public UILabel ItemNameLabel;

	// Token: 0x04002D0D RID: 11533
	public UILabel BtnLabel;

	// Token: 0x04002D0E RID: 11534
	private consign_item currentConsignItem;
}
