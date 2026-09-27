using System;
using Sproto;
using SprotoType;

// Token: 0x0200025D RID: 605
public class ret_buy_invest_pack_handler
{
	// Token: 0x06001340 RID: 4928 RVA: 0x0007CE5C File Offset: 0x0007B05C
	public static SprotoTypeBase ret_buy_invest_pack_request(SprotoTypeBase req)
	{
		ret_buy_invest_pack.request request = req as ret_buy_invest_pack.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.InitInvestPack(request);
			if (SingletonUnity<InvestRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<InvestRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<InvestRewardRootLogic>.Instance.UpdateInfo(request);
			}
		}
		return null;
	}
}
