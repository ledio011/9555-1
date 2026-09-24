using System;
using Sproto;
using SprotoType;

// Token: 0x0200025B RID: 603
public class ret_buy_car_shop_handler
{
	// Token: 0x0600133C RID: 4924 RVA: 0x0007CDE4 File Offset: 0x0007AFE4
	public static SprotoTypeBase ret_buy_car_shop_request(SprotoTypeBase req)
	{
		ret_buy_car_shop.request request = req as ret_buy_car_shop.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<PlayerCarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PlayerCarRootLogic>.Instance.UpdateCarPage(request);
			}
		}
		return null;
	}
}
