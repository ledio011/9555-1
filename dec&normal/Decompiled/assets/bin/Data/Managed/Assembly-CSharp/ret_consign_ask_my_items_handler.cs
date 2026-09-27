using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x02000263 RID: 611
public class ret_consign_ask_my_items_handler
{
	// Token: 0x0600134D RID: 4941 RVA: 0x0007E9E8 File Offset: 0x0007CBE8
	public static SprotoTypeBase ret_consign_ask_my_items_request(SprotoTypeBase req)
	{
		ret_consign_ask_my_items.request request = req as ret_consign_ask_my_items.request;
		if (request != null && request.success == 0L)
		{
			if (request.HasConsign_items && request.consign_items.Count > 0)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SaleNowList = new List<consign_item>(request.consign_items.Values);
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SaleNowList = null;
			}
			if (SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ConsignRootLogic>.Instance.RefreshOnSaleNow();
			}
		}
		return null;
	}
}
