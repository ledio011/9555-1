using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A19 RID: 2585
public class PopShopRootLogic : SingletonUnity<PopShopRootLogic>
{
	// Token: 0x06004A5F RID: 19039 RVA: 0x00185FC4 File Offset: 0x001841C4
	private void OnEnable()
	{
		this.buyCount = 1;
		this.NeedItemid = string.Empty;
		this.curPage = -1;
		this.curSelectObj = null;
	}

	// Token: 0x06004A60 RID: 19040 RVA: 0x00185FF4 File Offset: 0x001841F4
	public void EnableReset()
	{
		this.ClearSelectObj();
		UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, false);
		for (int i = 0; i < this.mShopItemBtns.Count; i++)
		{
			NGUITools.SetActive(this.mShopItemBtns[i].gameObject, false);
		}
		for (int j = 0; j < this.LeftAttrLabelList.Length; j++)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftAttrLabelList[j].gameObject, false);
		}
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.RefershTimeLabel.text = StrDictionary.GetDictionaryString("#{301102}", new object[]
		{
			TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
		});
	}

	// Token: 0x06004A61 RID: 19041 RVA: 0x001860B0 File Offset: 0x001842B0
	public void ShowItemProdect(string needItemid)
	{
		this.NeedItemid = needItemid;
	}

	// Token: 0x06004A62 RID: 19042 RVA: 0x001860BC File Offset: 0x001842BC
	private void UpdateSelectItem()
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
		int itemQuality = (int)this.curSelectItem.Quality;
		if (itemDataByID != null)
		{
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP || itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
			{
				NGUITools.SetActive(this.DescObj.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.AttributeObj, true);
				if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					itemQuality = (int)this.curSelectItem.Quality;
				}
				else if (itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
				{
					itemQuality = itemDataByID.Quality;
				}
				EquipData equipDataById = DataManager.GetEquipDataById(itemDataByID.ID);
				for (int i = 0; i < this.LeftAttrLabelList.Length; i++)
				{
					if (i < equipDataById.GetBaseAttCount())
					{
						UnityVersionUtil.SetActiveRecursive(this.LeftAttrLabelList[i].gameObject, true);
					}
					else
					{
						UnityVersionUtil.SetActiveRecursive(this.LeftAttrLabelList[i].gameObject, false);
					}
				}
				int[] array = new int[]
				{
					-1,
					-1,
					-1,
					-1
				};
				if (equipDataById.BaseStatusType != (ATTRIBUTE_TYPE)0)
				{
					this.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S((int)equipDataById.BaseStatusType);
					array[0] = equipDataById.GetAttrValByQualityAndLevel(0, itemQuality, 0);
					this.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr((int)equipDataById.BaseStatusType, array[0]);
					this.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon((int)equipDataById.BaseStatusType);
				}
				if (equipDataById.Status1 != 0)
				{
					this.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(equipDataById.Status1);
					array[1] = equipDataById.GetAttrValByQualityAndLevel(1, itemQuality, 0);
					this.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr(equipDataById.Status1, array[1]);
					this.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(equipDataById.Status1);
				}
				if (equipDataById.Status2 != 0)
				{
					this.LeftAttrLabelList[2].text = GameDefine.GetAttributeName_S(equipDataById.Status2);
					array[2] = equipDataById.GetAttrValByQualityAndLevel(2, itemQuality, 0);
					this.RightAttrLabelList[2].text = GameDefine.GetAttributeValueStr(equipDataById.Status2, array[2]);
					this.LefeAttrIconList[2].spriteName = GameDefine.GetAttributeIcon(equipDataById.Status2);
				}
				if (equipDataById.ExStatus != 0)
				{
					this.LeftAttrLabelList[3].text = GameDefine.GetAttributeName_S(equipDataById.ExStatus);
					array[3] = equipDataById.GetAttrValByQualityAndLevel(3, itemQuality, 0);
					this.RightAttrLabelList[3].text = GameDefine.GetAttributeValueStr(equipDataById.ExStatus, array[3]);
					this.LefeAttrIconList[3].spriteName = GameDefine.GetAttributeIcon(equipDataById.ExStatus);
				}
			}
			else if (itemDataByID.Type == GameDefine.ITEM_TYPE.BADGE)
			{
				BadgeData badgeDataById = DataManager.GetBadgeDataById(itemDataByID.ID);
				for (int j = 0; j < this.LeftAttrLabelList.Length; j++)
				{
					NGUITools.SetActive(this.LeftAttrLabelList[j].gameObject, false);
				}
				if (badgeDataById.Status1 != -1)
				{
					NGUITools.SetActive(this.LeftAttrLabelList[0].gameObject, true);
					this.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S(badgeDataById.Status1);
					this.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr(badgeDataById.Status1, badgeDataById.Value1);
					this.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status1);
				}
				if (badgeDataById.Status2 != -1)
				{
					NGUITools.SetActive(this.LeftAttrLabelList[1].gameObject, true);
					this.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(badgeDataById.Status2);
					this.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr(badgeDataById.Status2, badgeDataById.Value2);
					this.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status2);
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.AttributeObj, false);
				this.DescLabel.text = itemDataByID.MDescription;
				NGUITools.SetActive(this.DescObj.gameObject, true);
			}
			this.IconSprite.spriteName = itemDataByID.BackPackIcon;
			this.ItemNameLabel.text = itemDataByID.MName;
			if (this.curSelectItem.Limit > 0L)
			{
				this.remainCount = (int)(this.curSelectItem.Limit - this.curSelectItem.curNum);
			}
			else
			{
				this.remainCount = 999999;
			}
			if (this.buyCount > this.remainCount)
			{
				this.buyCount = this.remainCount;
			}
			if (this.remainCount < 1)
			{
				this.buyCount = 0;
			}
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, true);
				this.QualitySprite.spriteName = ((EQUIP_QUALITY)this.curSelectItem.Quality).ToString();
				this.ItemNameLabel.color = GameDefine.GetColorByQuality((EQUIP_QUALITY)this.curSelectItem.Quality);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, true);
				this.QualitySprite.spriteName = itemDataByID.QualityType.ToString();
				this.ItemNameLabel.color = GameDefine.GetColorByQuality(itemDataByID.QualityType);
			}
			this.UpdateBuyCost();
		}
	}

	// Token: 0x06004A63 RID: 19043 RVA: 0x001865F4 File Offset: 0x001847F4
	private void UpdateBuyCost()
	{
		this.BuyCountLabel.text = string.Format("[u]{0}[/u]", this.buyCount);
		this.buyCost = (int)(this.curSelectItem.Price * (long)this.buyCount);
		this.BuyCostLabel.text = GameMoneyHelper.GetMoneyValStr(this.buyCost, (GameDefine.MONEY_TYPE)this.curSelectItem.PriceType);
	}

	// Token: 0x06004A64 RID: 19044 RVA: 0x00186660 File Offset: 0x00184860
	public void OnClickBuyLeft()
	{
		if (this.buyCount > 1)
		{
			this.buyCount--;
			this.UpdateBuyCost();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{101245}", true, false);
		}
	}

	// Token: 0x06004A65 RID: 19045 RVA: 0x00186694 File Offset: 0x00184894
	public void OnClickBuyRight()
	{
		if (this.buyCount < this.remainCount)
		{
			this.buyCount++;
			ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
			if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.BOX)
			{
				ShopData shopDataByID = DataManager.GetShopDataByID(this.curSelectItem.ID);
				if (this.buyCount > shopDataByID.SingleLimit)
				{
					this.buyCount = shopDataByID.SingleLimit;
					NoticeLogic.AddNotifyData("#{301124}", true, false);
				}
			}
			this.UpdateBuyCost();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{101246}", true, false);
		}
	}

	// Token: 0x06004A66 RID: 19046 RVA: 0x00186738 File Offset: 0x00184938
	public void OnClickBuyItem()
	{
		if (this.curSelectItem == null)
		{
			return;
		}
		if (this.buyCount < 1)
		{
			if (this.remainCount == 0)
			{
				NoticeLogic.AddNotifyData("#{101244}", true, false);
			}
			return;
		}
		if (!GameMoneyHelper.BeforeCheckBuy((GameDefine.MONEY_TYPE)this.curSelectItem.PriceType, this.buyCost))
		{
			return;
		}
		ShopData shopDataByID = DataManager.GetShopDataByID(this.curSelectItem.ID);
		if (shopDataByID != null && !string.IsNullOrEmpty(shopDataByID.EndTime) && TimeTools.GetShopItemTime(shopDataByID.EndTimes).TotalSeconds <= 0.0)
		{
			if (GameManager.IsSupportCurDataVersion56())
			{
				NoticeLogic.AddNotifyData("#{301119}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("Sales time is over. The item cannot be purchased.", true, false);
			}
			return;
		}
		buy_shop_item.request request = new buy_shop_item.request();
		request.ID = this.curSelectItem.ID;
		request.itemCount = (long)this.buyCount;
		request.type = (long)this.curType;
		NetLogic.GetInstance().Send<Protocol.buy_shop_item>(request, null);
	}

	// Token: 0x06004A67 RID: 19047 RVA: 0x00186840 File Offset: 0x00184A40
	public void UpdateShopItem(shop_item item, long type)
	{
		if (type != (long)this.curType || item == null)
		{
			return;
		}
		if (this.curSelectItem != null && item.ID == this.curSelectItem.ID)
		{
			this.curSelectItem.curNum = item.curNum;
			this.UpdateSelect(this.curSelectItem, this.curSelectObj);
		}
		if (this.shopList != null)
		{
			for (int i = 0; i < this.shopList.Count; i++)
			{
				if (this.shopList[i].ID == item.ID)
				{
					this.shopList[i].curNum = item.curNum;
				}
			}
		}
		for (int j = 0; j < this.mShopItemBtns.Count; j++)
		{
			this.mShopItemBtns[j].UpdateInfo(item);
		}
	}

	// Token: 0x06004A68 RID: 19048 RVA: 0x00186938 File Offset: 0x00184B38
	public void UpdateShop(ret_ask_shop_list.request request)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.curPage != (int)request.curPage)
		{
			this.curSelectObj = null;
		}
		this.curPage = (int)request.curPage;
		this.maxPage = (int)request.maxPage;
		this.curType = (GameDefine.SHOP_TYPE)request.type;
		List<shop_item> shop_list = request.shop_list;
		this.shopList = shop_list;
		int num = shop_list.Count - this.mShopItemBtns.Count;
		int count = this.mShopItemBtns.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.mShopItemBtns[0].gameObject) as GameObject;
				gameObject.name = string.Format("shangPing_{0}", count + i);
				this.ItemGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				this.mShopItemBtns.Add(gameObject.GetComponent<ShopItemBtnLogic>());
			}
		}
		for (int j = 0; j < this.mShopItemBtns.Count; j++)
		{
			NGUITools.SetActive(this.mShopItemBtns[j].gameObject, j < shop_list.Count);
		}
		this.curPageLable.text = string.Format("{0}/{1}", this.curPage, this.maxPage);
		if (request.shop_list.Count == 0)
		{
			return;
		}
		for (int k = 0; k < shop_list.Count; k++)
		{
			if (this.curSelectObj == null)
			{
				this.curSelectObj = this.mShopItemBtns[k].gameObject;
				this.curSelectItem = shop_list[k];
			}
			this.mShopItemBtns[k].Reset(shop_list[k], this.curType, new ShopItemBtnLogic.OnClickShopItemDelegate(this.UpdateSelect));
		}
		this.ItemGrid.Reposition();
		if (this.curType == GameDefine.SHOP_TYPE.GUILD_SHOP)
		{
			UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
			this.MoneyLabel.text = playerData.GuildContribute.ToString();
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, false);
		}
		if (!string.IsNullOrEmpty(this.NeedItemid))
		{
			for (int l = 0; l < shop_list.Count; l++)
			{
				if (shop_list[l].ItemID.Equals(this.NeedItemid))
				{
					this.curSelectObj = this.mShopItemBtns[l].gameObject;
					this.curSelectItem = shop_list[l];
					this.NeedItemid = string.Empty;
					break;
				}
			}
		}
		this.UpdateSelect(this.curSelectItem, this.curSelectObj);
		this.flurryShopOpen();
	}

	// Token: 0x06004A69 RID: 19049 RVA: 0x00186C38 File Offset: 0x00184E38
	private void flurryShopOpen()
	{
		switch (this.curType)
		{
		case GameDefine.SHOP_TYPE.TOOL_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_tool", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.EQUIP_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_equip", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.BIGSALE_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_bigsale", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.GUILD_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_guild", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.BATTLECOIN_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_battle", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.ACTIVITY_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_activity", "open", "times");
			break;
		}
	}

	// Token: 0x06004A6A RID: 19050 RVA: 0x00186D30 File Offset: 0x00184F30
	public void OnClickLeft()
	{
		int num = this.curPage - 1;
		if (num > 0)
		{
			ask_shop_list.request request = new ask_shop_list.request();
			request.curPage = (long)num;
			request.type = (long)this.curType;
			request.subType = 1L;
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
		}
	}

	// Token: 0x06004A6B RID: 19051 RVA: 0x00186D7C File Offset: 0x00184F7C
	public void OnClikcRight()
	{
		int num = this.curPage + 1;
		if (num <= this.maxPage)
		{
			ask_shop_list.request request = new ask_shop_list.request();
			request.curPage = (long)num;
			request.type = (long)this.curType;
			request.subType = 1L;
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
		}
	}

	// Token: 0x06004A6C RID: 19052 RVA: 0x00186DD0 File Offset: 0x00184FD0
	private void UpdateSelect(shop_item item)
	{
		for (int i = 0; i < this.mShopItemBtns.Count; i++)
		{
			this.mShopItemBtns[i].UpdateSelect(item);
		}
	}

	// Token: 0x06004A6D RID: 19053 RVA: 0x00186E0C File Offset: 0x0018500C
	public void OnClickSelectItem()
	{
		if (this.curSelectItem != null)
		{
			ItemInfoRootLogicNew.ShowItemTips(this.curSelectItem);
		}
	}

	// Token: 0x06004A6E RID: 19054 RVA: 0x00186E24 File Offset: 0x00185024
	public void UpdateSelect(shop_item item, GameObject obj)
	{
		this.curSelectObj = obj;
		if (this.curSelectItem == null || !this.curSelectItem.ID.Equals(item.ID))
		{
			this.buyCount = 1;
		}
		this.curSelectItem = item;
		this.UpdateSelect(item);
		this.UpdateSelectItem();
		this.flurryShopItemclick(item);
	}

	// Token: 0x06004A6F RID: 19055 RVA: 0x00186E80 File Offset: 0x00185080
	private void flurryShopItemclick(shop_item clickitem)
	{
		switch (this.curType)
		{
		case GameDefine.SHOP_TYPE.TOOL_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_tool", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.EQUIP_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_equip", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.BIGSALE_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_bigsale", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.GUILD_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_guild", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.BATTLECOIN_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_battle", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.ACTIVITY_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_activity", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		}
	}

	// Token: 0x06004A70 RID: 19056 RVA: 0x00186FBC File Offset: 0x001851BC
	public void ClearSelectObj()
	{
		this.curSelectItem = null;
		this.curSelectObj = null;
	}

	// Token: 0x06004A71 RID: 19057 RVA: 0x00186FCC File Offset: 0x001851CC
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopShopRoot);
	}

	// Token: 0x06004A72 RID: 19058 RVA: 0x00186FE0 File Offset: 0x001851E0
	public void OnClickNumLabel()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NumRoot, delegate
		{
			SingletonUnity<NumRootLogic>.Instance.Reset(new DelegateDefine.OneIntParamDelegate(this.CalNumFun));
		}, null);
	}

	// Token: 0x06004A73 RID: 19059 RVA: 0x00187000 File Offset: 0x00185200
	private void CalNumFun(int num)
	{
		if (this.curSelectItem == null)
		{
			return;
		}
		int num2 = this.buyCount;
		int num3 = (int)(GameMoneyHelper.GetMoneyNum((int)this.curSelectItem.PriceType) / this.curSelectItem.Price);
		if (num == -1)
		{
			num2 /= 10;
		}
		else if (num == -2)
		{
			num2 = num3;
		}
		else
		{
			num2 = num2 * 10 + num;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
		if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.BOX)
		{
			ShopData shopDataByID = DataManager.GetShopDataByID(this.curSelectItem.ID);
			if (num2 > shopDataByID.SingleLimit)
			{
				num2 = shopDataByID.SingleLimit;
			}
		}
		if (num2 > this.remainCount)
		{
			num2 = this.remainCount;
		}
		if (this.remainCount < 1)
		{
			num2 = 0;
		}
		this.buyCount = num2;
		this.UpdateBuyCost();
	}

	// Token: 0x06004A74 RID: 19060 RVA: 0x001870E0 File Offset: 0x001852E0
	public void OnClickMaxBtn()
	{
		this.CalNumFun(-2);
	}

	// Token: 0x040037F5 RID: 14325
	public List<ShopItemBtnLogic> mShopItemBtns = new List<ShopItemBtnLogic>();

	// Token: 0x040037F6 RID: 14326
	public UIGrid ItemGrid;

	// Token: 0x040037F7 RID: 14327
	public UILabel curPageLable;

	// Token: 0x040037F8 RID: 14328
	private int curPage = 1;

	// Token: 0x040037F9 RID: 14329
	private int maxPage = 1;

	// Token: 0x040037FA RID: 14330
	public GameObject specialMoneyObj;

	// Token: 0x040037FB RID: 14331
	public UILabel MoneyLabel;

	// Token: 0x040037FC RID: 14332
	private GameDefine.SHOP_TYPE curType;

	// Token: 0x040037FD RID: 14333
	private GameObject curSelectObj;

	// Token: 0x040037FE RID: 14334
	private shop_item curSelectItem;

	// Token: 0x040037FF RID: 14335
	public UISprite IconSprite;

	// Token: 0x04003800 RID: 14336
	public UISprite QualitySprite;

	// Token: 0x04003801 RID: 14337
	public GameObject DescObj;

	// Token: 0x04003802 RID: 14338
	public UILabel DescLabel;

	// Token: 0x04003803 RID: 14339
	public UILabel ItemNameLabel;

	// Token: 0x04003804 RID: 14340
	private int buyCount = 1;

	// Token: 0x04003805 RID: 14341
	public UILabel BuyCountLabel;

	// Token: 0x04003806 RID: 14342
	public UILabel BuyCostLabel;

	// Token: 0x04003807 RID: 14343
	private int buyCost;

	// Token: 0x04003808 RID: 14344
	private int remainCount;

	// Token: 0x04003809 RID: 14345
	public UILabel CountTimeLabel;

	// Token: 0x0400380A RID: 14346
	private string NeedItemid = string.Empty;

	// Token: 0x0400380B RID: 14347
	private List<shop_item> shopList;

	// Token: 0x0400380C RID: 14348
	public GameObject AttributeObj;

	// Token: 0x0400380D RID: 14349
	public UILabel[] LeftAttrLabelList;

	// Token: 0x0400380E RID: 14350
	public UILabel[] RightAttrLabelList;

	// Token: 0x0400380F RID: 14351
	public UISprite[] LefeAttrIconList;

	// Token: 0x04003810 RID: 14352
	public UILabel RefershTimeLabel;
}
