using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x020002C3 RID: 707
public class sync_backpack_item_handler
{
	// Token: 0x0600140E RID: 5134 RVA: 0x00081FC0 File Offset: 0x000801C0
	public static SprotoTypeBase sync_backpack_item_request(SprotoTypeBase req)
	{
		sync_backpack_item.request request = req as sync_backpack_item.request;
		if (request != null)
		{
			ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.EQUIP_BACKPACK);
			if (itemContainer != null && request.HasGameitems)
			{
				foreach (KeyValuePair<long, gameitem> keyValuePair in request.gameitems)
				{
					gameitem value = keyValuePair.Value;
					GameItem itemByIndexId = itemContainer.GetItemByIndexId(value.indexId);
					if (itemByIndexId != null)
					{
						itemByIndexId.ItemId = value.itemId;
						itemByIndexId.ContainerType = ITEM_CONTAINER_TYPE.EQUIP_BACKPACK;
						if (value.HasParm)
						{
							itemByIndexId.SetParm(value.parm);
						}
						if (value.HasBindflag)
						{
							itemByIndexId.BindFlag = value.bindflag;
						}
						else
						{
							itemByIndexId.BindFlag = false;
						}
						if (value.HasStack)
						{
							itemByIndexId.StackNum = (int)value.stack;
						}
						else
						{
							itemByIndexId.StackNum = 1;
						}
						if (value.HasIndexId)
						{
							itemByIndexId.IndexId = value.indexId;
						}
						else
						{
							itemByIndexId.IndexId = -1L;
						}
						if (value.HasQuality)
						{
							itemByIndexId.Quality = (EQUIP_QUALITY)value.quality;
						}
						else
						{
							itemByIndexId.Quality = EQUIP_QUALITY.INVALID;
						}
						if (value.HasLevel)
						{
							itemByIndexId.ItemLevel = (int)value.level;
						}
						else
						{
							itemByIndexId.ItemLevel = 0;
						}
						if (value.HasAppraise)
						{
							itemByIndexId.Appraise = (int)value.appraise;
						}
						else
						{
							itemByIndexId.Appraise = 0;
						}
						if (value.HasRandom_attri)
						{
							itemByIndexId.Random_AttriDic = value.random_attri;
						}
						else
						{
							itemByIndexId.Random_AttriDic = null;
						}
						if (value.HasInlay)
						{
							itemByIndexId.InlayDic = value.inlay;
						}
						else
						{
							itemByIndexId.InlayDic = null;
						}
					}
				}
				if (UIUpdateEvent.SyncBackPackEvent != null)
				{
					UIUpdateEvent.SyncBackPackEvent();
				}
			}
		}
		return null;
	}
}
