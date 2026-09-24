using System;
using Sproto;
using SprotoType;

// Token: 0x020002A1 RID: 673
public class ret_require_vip_reward_handler
{
	// Token: 0x060013C9 RID: 5065 RVA: 0x00080CF8 File Offset: 0x0007EEF8
	public static SprotoTypeBase ret_require_vip_reward_request(SprotoTypeBase req)
	{
		ret_require_vip_reward.request request = req as ret_require_vip_reward.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.SyncVipInfo(request);
			if (SingletonUnity<MonthlyCardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MonthlyCardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MonthlyCardRootLogic>.Instance.RefershInfo(request);
			}
		}
		return null;
	}
}
