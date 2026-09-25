using System;
using Sproto;
using SprotoType;

// Token: 0x02000292 RID: 658
public class ret_request_first_buy_handler
{
	// Token: 0x060013AB RID: 5035 RVA: 0x00080464 File Offset: 0x0007E664
	public static SprotoTypeBase ret_request_first_buy_request(SprotoTypeBase req)
	{
		ret_request_first_buy.request request = req as ret_request_first_buy.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<FirstBuyRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FirstBuyRootLogic>.Instance.gameObject))
			{
				SingletonUnity<FirstBuyRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
