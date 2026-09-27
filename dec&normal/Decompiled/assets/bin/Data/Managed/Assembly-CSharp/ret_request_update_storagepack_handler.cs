using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class ret_request_update_storagepack_handler
{
	// Token: 0x060013C3 RID: 5059 RVA: 0x00080A2C File Offset: 0x0007EC2C
	public static SprotoTypeBase ret_request_update_storagepack_request(SprotoTypeBase req)
	{
		ret_request_update_storagepack.request request = req as ret_request_update_storagepack.request;
		if (request != null)
		{
			ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.EQUIP_BACKPACK);
			if (itemContainer != null && request.HasGameitems)
			{
				List<gameitem> list = new List<gameitem>(request.gameitems.Values);
				for (int i = 0; i < list.Count; i++)
				{
					gameitem gameitem = list[i];
					GameItem itemByIndexId = itemContainer.GetItemByIndexId(gameitem.indexId);
					if (itemByIndexId != null)
					{
						itemByIndexId.ItemId = gameitem.itemId;
						if (gameitem.HasBindflag)
						{
							itemByIndexId.BindFlag = gameitem.bindflag;
						}
						else
						{
							itemByIndexId.BindFlag = false;
						}
						if (gameitem.HasStack)
						{
							itemByIndexId.StackNum = (int)gameitem.stack;
						}
						else
						{
							itemByIndexId.StackNum = 1;
						}
						if (gameitem.HasIndexId)
						{
							itemByIndexId.IndexId = gameitem.indexId;
						}
						else
						{
							itemByIndexId.IndexId = -1L;
						}
						if (gameitem.HasQuality)
						{
							itemByIndexId.Quality = (EQUIP_QUALITY)gameitem.quality;
						}
						else
						{
							itemByIndexId.Quality = EQUIP_QUALITY.INVALID;
						}
						if (gameitem.HasLevel)
						{
							itemByIndexId.ItemLevel = (int)gameitem.level;
						}
						else
						{
							itemByIndexId.ItemLevel = 0;
						}
						if (gameitem.HasAppraise)
						{
							itemByIndexId.Appraise = (int)gameitem.appraise;
						}
						else
						{
							itemByIndexId.Appraise = 0;
						}
						if (gameitem.HasRandom_attri)
						{
							itemByIndexId.Random_AttriDic = gameitem.random_attri;
						}
						else
						{
							itemByIndexId.Random_AttriDic = null;
						}
						if (gameitem.HasInlay)
						{
							itemByIndexId.InlayDic = gameitem.inlay;
						}
						else
						{
							itemByIndexId.InlayDic = null;
						}
					}
					else
					{
						Debug.Log("NotEnoughPackNum");
					}
				}
				if (UIUpdateEvent.UpdateBackPackEvent != null)
				{
					UIUpdateEvent.UpdateBackPackEvent();
				}
			}
		}
		return null;
	}
}
