using System;
using SprotoType;

// Token: 0x02000928 RID: 2344
public class ConsignBuyCheckRoot : SingletonUnity<ConsignBuyCheckRoot>
{
	// Token: 0x0600411B RID: 16667 RVA: 0x00134F24 File Offset: 0x00133124
	public void Reset(consign_item curItem)
	{
		this.mCurItem = curItem;
		ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurItem.itemId);
		this.ItemIconSprite.spriteName = itemDataByID.BackPackIcon;
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.ItemCircleSprite.spriteName = ((EQUIP_QUALITY)curItem.quality).ToString();
		}
		else
		{
			this.ItemCircleSprite.spriteName = itemDataByID.QualityType.ToString();
		}
		this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301117}", new object[]
		{
			this.mCurItem.price
		});
	}

	// Token: 0x0600411C RID: 16668 RVA: 0x00134FD0 File Offset: 0x001331D0
	public void OnClickYesBtn()
	{
		if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.DIAMOND, (int)this.mCurItem.price))
		{
			consign_buy_item.request request = new consign_buy_item.request();
			request.id = this.mCurItem.id;
			request.itemId = this.mCurItem.itemId;
			NetLogic.GetInstance().Send<Protocol.consign_buy_item>(request, null);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ConsignBuyCheckRoot);
		}
	}

	// Token: 0x0600411D RID: 16669 RVA: 0x00135038 File Offset: 0x00133238
	public void OnClickNoBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ConsignBuyCheckRoot);
	}

	// Token: 0x04002CD3 RID: 11475
	public UILabel InfoLabel;

	// Token: 0x04002CD4 RID: 11476
	public UISprite ItemIconSprite;

	// Token: 0x04002CD5 RID: 11477
	public UISprite ItemCircleSprite;

	// Token: 0x04002CD6 RID: 11478
	private consign_item mCurItem;
}
