using System;
using Sproto;
using SprotoType;

// Token: 0x02000259 RID: 601
public class ret_ask_shop_list_handler
{
	// Token: 0x06001338 RID: 4920 RVA: 0x0007CB9C File Offset: 0x0007AD9C
	public static SprotoTypeBase ret_ask_shop_list_request(SprotoTypeBase req)
	{
		ret_ask_shop_list.request request = req as ret_ask_shop_list.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasShop_list)
			{
				if (request.HasSubType && request.subType == 1L && request.type != 4L)
				{
					if (SingletonUnity<PopShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopShopRootLogic>.Instance.gameObject))
					{
						SingletonUnity<PopShopRootLogic>.Instance.UpdateShop(request);
					}
					if (SingletonUnity<PopTopShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopTopShopRootLogic>.Instance.gameObject))
					{
						SingletonUnity<PopTopShopRootLogic>.Instance.UpdateShop(request);
					}
					return null;
				}
				if (request.type == 4L)
				{
					if (SingletonUnity<BuyDiamondRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<BuyDiamondRootLogic>.Instance.gameObject))
					{
						SingletonUnity<BuyDiamondRootLogic>.Instance.Reset(request);
					}
					if (SingletonUnity<PopDiamondBuyRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopDiamondBuyRootLogic>.Instance.gameObject))
					{
						SingletonUnity<PopDiamondBuyRootLogic>.Instance.Reset(request);
					}
					if (SingletonUnity<PopTopDiamondBuyRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopTopDiamondBuyRootLogic>.Instance.gameObject))
					{
						SingletonUnity<PopTopDiamondBuyRootLogic>.Instance.Reset(request);
					}
				}
				else if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
				{
					SingletonUnity<ShopTabRootLogic>.Instance.UpdateShop(request);
				}
			}
		}
		return null;
	}
}
