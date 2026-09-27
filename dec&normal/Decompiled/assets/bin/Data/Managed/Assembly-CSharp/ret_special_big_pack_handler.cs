using System;
using Sproto;
using SprotoType;

// Token: 0x020002AA RID: 682
public class ret_special_big_pack_handler
{
	// Token: 0x060013DB RID: 5083 RVA: 0x00081108 File Offset: 0x0007F308
	public static SprotoTypeBase ret_special_big_pack_request(SprotoTypeBase req)
	{
		ret_special_big_pack.request request = req as ret_special_big_pack.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<MysteryShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MysteryShopRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MysteryShopRootLogic>.Instance.Reset(request);
			}
			if (SingletonUnity<ShopBigSaleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopBigSaleRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ShopBigSaleRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
