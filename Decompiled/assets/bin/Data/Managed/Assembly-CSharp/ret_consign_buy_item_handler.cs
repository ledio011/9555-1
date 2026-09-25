using System;
using Sproto;
using SprotoType;

// Token: 0x02000264 RID: 612
public class ret_consign_buy_item_handler
{
	// Token: 0x0600134F RID: 4943 RVA: 0x0007EA90 File Offset: 0x0007CC90
	public static SprotoTypeBase ret_consign_buy_item_request(SprotoTypeBase req)
	{
		ret_consign_buy_item.request request = req as ret_consign_buy_item.request;
		if (request != null)
		{
			if (request.success == 0L)
			{
				if (SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
				{
					SingletonUnity<ConsignRootLogic>.Instance.BuySuccess(request.id);
				}
				NoticeLogic.AddNotifyData("#{101230}", true, false);
				ItemData itemDataByID = DataManager.GetItemDataByID(request.itemId);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Tradebuy", "buytimes", "times");
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Tradebuy", string.Format("buytype_{0}", itemDataByID.Type), string.Format("buy_{0}", itemDataByID.ID));
			}
			else
			{
				NoticeLogic.AddNotifyData("#{101233}", true, false);
			}
		}
		return null;
	}
}
