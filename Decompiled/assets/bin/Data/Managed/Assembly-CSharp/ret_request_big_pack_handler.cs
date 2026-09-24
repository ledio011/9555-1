using System;
using Sproto;
using SprotoType;

// Token: 0x0200028E RID: 654
public class ret_request_big_pack_handler
{
	// Token: 0x060013A3 RID: 5027 RVA: 0x0008022C File Offset: 0x0007E42C
	public static SprotoTypeBase ret_request_big_pack_request(SprotoTypeBase req)
	{
		ret_request_big_pack.request request = req as ret_request_big_pack.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.SetBigPack(request);
			if (SingletonUnity<BigPackRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<BigPackRootLogic>.Instance.gameObject))
			{
				SingletonUnity<BigPackRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
