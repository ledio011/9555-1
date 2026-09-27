using System;
using Sproto;
using SprotoType;

// Token: 0x02000266 RID: 614
public class ret_consign_sale_item_handler
{
	// Token: 0x06001353 RID: 4947 RVA: 0x0007EBFC File Offset: 0x0007CDFC
	public static SprotoTypeBase ret_consign_sale_item_request(SprotoTypeBase req)
	{
		ret_consign_sale_item.request request = req as ret_consign_sale_item.request;
		if (request != null)
		{
			if (request.success == 0L)
			{
				GameDefine.ITEM_TYPE item_TYPE = (GameDefine.ITEM_TYPE)request.itemType;
				GameItem gameItem = null;
				if (item_TYPE == GameDefine.ITEM_TYPE.BADGE)
				{
					ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.BADGE_BACKPACK);
					if (itemContainer != null)
					{
						gameItem = itemContainer.GetItemByIndexId(request.indexId);
						if (gameItem != null)
						{
							if (request.HasGameitem)
							{
								gameItem.UpdateItem(request.gameitem);
							}
							else
							{
								gameItem.Reset();
							}
						}
						if (SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
						{
							SingletonUnity<ConsignRootLogic>.Instance.SellSuccess();
						}
					}
				}
				else if (item_TYPE == GameDefine.ITEM_TYPE.EQUIP)
				{
					ItemContainer itemContainer2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.EQUIP_BACKPACK);
					if (itemContainer2 != null)
					{
						gameItem = itemContainer2.GetItemByIndexId(request.indexId);
						if (gameItem != null)
						{
							if (request.HasGameitem)
							{
								gameItem.UpdateItem(request.gameitem);
							}
							else
							{
								gameItem.Reset();
							}
						}
						if (SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
						{
							SingletonUnity<ConsignRootLogic>.Instance.SellSuccess();
						}
					}
				}
				else
				{
					ItemContainer itemContainer3 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.ITEM_BACKPACK);
					if (itemContainer3 != null)
					{
						gameItem = itemContainer3.GetItemByIndexId(request.indexId);
						if (gameItem != null)
						{
							if (request.HasGameitem)
							{
								gameItem.UpdateItem(request.gameitem);
							}
							else
							{
								gameItem.Reset();
							}
						}
						if (SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
						{
							SingletonUnity<ConsignRootLogic>.Instance.SellSuccess();
						}
					}
				}
				NoticeLogic.AddNotifyData("#{101232}", true, false);
				if (gameItem != null)
				{
					ItemData itemData = gameItem.ItemData;
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Tradesell", "selltimes", "times");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Tradesell", string.Format("selltype_{0}", itemData.Type), string.Format("sell_{0}", itemData.ID));
				}
			}
			else if (request.success == 3L)
			{
				NoticeLogic.AddNotifyData("#{101243}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{101235}", true, false);
			}
		}
		return null;
	}
}
