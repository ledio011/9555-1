using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x0200025E RID: 606
public class ret_buy_shop_item_handler
{
	// Token: 0x06001342 RID: 4930 RVA: 0x0007CEC4 File Offset: 0x0007B0C4
	public static SprotoTypeBase ret_buy_shop_item_request(SprotoTypeBase req)
	{
		ret_buy_shop_item.request request = req as ret_buy_shop_item.request;
		if (request != null)
		{
			if (SingletonUnity<PopShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopShopRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PopShopRootLogic>.Instance.UpdateShopItem(request.shop_item, request.type);
			}
			if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ShopTabRootLogic>.Instance.UpdateShopItem(request.shop_item, request.type);
			}
			if (SingletonUnity<PopTopShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopTopShopRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PopTopShopRootLogic>.Instance.UpdateShopItem(request.shop_item, request.type);
			}
			ItemData itemDataByID = DataManager.GetItemDataByID(request.shop_item.ItemID);
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.BOX)
			{
				ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
				List<GameItem> itemByItemId = itemBackPack.GetItemByItemId(itemDataByID.ID);
				if (itemByItemId != null && itemByItemId.Count > 0)
				{
					open_item_package.request request2 = new open_item_package.request();
					request2.indexId = itemByItemId[0].IndexId;
					request2.count = (long)((int)request.count);
					if (request2.count > 99L)
					{
						request2.count = 99L;
					}
					NetLogic.GetInstance().Send<Protocol.open_item_package>(request2, null);
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
					{
						SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
						if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BUY)
						{
							TutorialManager.MoveNext(false);
						}
					}, null);
				}
			}
			int num = (int)request.count;
			string text;
			if (num <= 5)
			{
				text = "1_5";
			}
			else if (num <= 10)
			{
				text = "6_10";
			}
			else if (num <= 20)
			{
				text = "11_20";
			}
			else if (num <= 50)
			{
				text = "21_50";
			}
			else if (num <= 100)
			{
				text = "50_100";
			}
			else
			{
				text = "100+";
			}
			long type = request.type;
			if (type >= 0L && type <= 7L)
			{
				switch ((int)type)
				{
				case 0:
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_tool", string.Format("shopitem_{0}", request.shop_item.ID), "buytimes");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_tool", string.Format("shopitem_{0}", request.shop_item.ID), string.Format("buynum_{0}", text));
					break;
				case 1:
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_equip", string.Format("shopitem_{0}", request.shop_item.ID), "buytimes");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_equip", string.Format("shopitem_{0}", request.shop_item.ID), string.Format("buynum_{0}", text));
					break;
				case 2:
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_bigsale", string.Format("shopitem_{0}", request.shop_item.ID), "buytimes");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_bigsale", string.Format("shopitem_{0}", request.shop_item.ID), string.Format("buynum_{0}", text));
					break;
				case 3:
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_guild", string.Format("shopitem_{0}", request.shop_item.ID), "buytimes");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_guild", string.Format("shopitem_{0}", request.shop_item.ID), string.Format("buynum_{0}", text));
					break;
				case 6:
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_battle", string.Format("shopitem_{0}", request.shop_item.ID), "buytimes");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_battle", string.Format("shopitem_{0}", request.shop_item.ID), string.Format("buynum_{0}", text));
					break;
				case 7:
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_activity", string.Format("shopitem_{0}", request.shop_item.ID), "buytimes");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_activity", string.Format("shopitem_{0}", request.shop_item.ID), string.Format("buynum_{0}", text));
					break;
				}
			}
		}
		return null;
	}
}
