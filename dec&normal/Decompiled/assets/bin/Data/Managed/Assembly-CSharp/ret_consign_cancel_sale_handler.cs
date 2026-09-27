using System;
using Sproto;
using SprotoType;

// Token: 0x02000265 RID: 613
public class ret_consign_cancel_sale_handler
{
	// Token: 0x06001351 RID: 4945 RVA: 0x0007EB64 File Offset: 0x0007CD64
	public static SprotoTypeBase ret_consign_cancel_sale_request(SprotoTypeBase req)
	{
		ret_consign_cancel_sale.request request = req as ret_consign_cancel_sale.request;
		if (request != null)
		{
			if (request.success == 0L)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CancelSuccess(request.id);
				if (SingletonUnity<ConsignRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ConsignRootLogic>.Instance.gameObject))
				{
					SingletonUnity<ConsignRootLogic>.Instance.RefreshOnSaleNow();
					SingletonUnity<ConsignRootLogic>.Instance.BuySuccess(request.id);
				}
				NoticeLogic.AddNotifyData("#{101231}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{101234}", true, false);
			}
		}
		return null;
	}
}
