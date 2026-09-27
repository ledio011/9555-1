using System;
using Sproto;
using SprotoType;

// Token: 0x02000262 RID: 610
public class ret_consign_ask_items_info_handler
{
	// Token: 0x0600134B RID: 4939 RVA: 0x0007E99C File Offset: 0x0007CB9C
	public static SprotoTypeBase ret_consign_ask_items_info_request(SprotoTypeBase req)
	{
		ret_consign_ask_items_info.request request = req as ret_consign_ask_items_info.request;
		if (request != null && SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ConsignRootLogic>.Instance.RefreshOnBuyList(request);
		}
		return null;
	}
}
