using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class drop_item_info_handler
{
	// Token: 0x060012DB RID: 4827 RVA: 0x0007B2FC File Offset: 0x000794FC
	public static SprotoTypeBase drop_item_info_request(SprotoTypeBase req)
	{
		drop_item_info.request request = req as drop_item_info.request;
		if (request != null)
		{
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.serverId);
			if (objCharacter != null)
			{
				Singleton<ObjManager>.Instance.RemoveObj(request.serverId);
			}
			ObjInitDropItemData objInitDropItemData = new ObjInitDropItemData();
			objInitDropItemData.Pos = new VectorXZ((float)request.pos_x / 100f, (float)request.pos_z / 100f);
			objInitDropItemData.ownerServerId = request.ownServerId;
			objInitDropItemData.ServerID = request.serverId;
			objInitDropItemData.item = request.item;
			ItemData itemDataByID = DataManager.GetItemDataByID(request.item.itemId);
			objInitDropItemData.ItemType = itemDataByID.Type;
			Singleton<ObjManager>.Instance.CreateDropItem(objInitDropItemData);
		}
		return null;
	}
}
