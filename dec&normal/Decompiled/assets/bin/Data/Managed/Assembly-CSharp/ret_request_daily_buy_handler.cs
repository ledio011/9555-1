using System;
using Sproto;
using SprotoType;

// Token: 0x02000290 RID: 656
public class ret_request_daily_buy_handler
{
	// Token: 0x060013A7 RID: 5031 RVA: 0x00080374 File Offset: 0x0007E574
	public static SprotoTypeBase ret_request_daily_buy_request(SprotoTypeBase req)
	{
		ret_request_daily_buy.request request = req as ret_request_daily_buy.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.welfareData.InitDailyBuy(request);
			if (SingletonUnity<DailyBuyPackRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyBuyPackRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DailyBuyPackRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
