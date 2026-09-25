using System;
using Sproto;
using SprotoType;

// Token: 0x02000295 RID: 661
public class ret_request_invest_pack_handler
{
	// Token: 0x060013B1 RID: 5041 RVA: 0x000805F8 File Offset: 0x0007E7F8
	public static SprotoTypeBase ret_request_invest_pack_request(SprotoTypeBase req)
	{
		ret_request_invest_pack.request request = req as ret_request_invest_pack.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.InitInvestPack(request);
			if (SingletonUnity<InvestRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<InvestRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<InvestRewardRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
