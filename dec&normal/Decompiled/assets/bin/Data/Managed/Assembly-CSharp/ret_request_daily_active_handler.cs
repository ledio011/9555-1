using System;
using Sproto;
using SprotoType;

// Token: 0x0200028F RID: 655
public class ret_request_daily_active_handler
{
	// Token: 0x060013A5 RID: 5029 RVA: 0x00080294 File Offset: 0x0007E494
	public static SprotoTypeBase ret_request_daily_active_request(SprotoTypeBase req)
	{
		ret_request_daily_active.request request = req as ret_request_daily_active.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.InitDailyRewards(request);
			if (SingletonUnity<DailyActiveRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyActiveRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DailyActiveRewardRootLogic>.Instance.Reset(request);
			}
			if (SingletonUnity<DailyRewardNewLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyRewardNewLogic>.Instance.gameObject))
			{
				SingletonUnity<DailyRewardNewLogic>.Instance.Reset(request);
			}
			if (SingletonUnity<DailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyCopyUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DailyCopyUIRootLogic>.Instance.UpdataActiveInfo();
			}
			if (SingletonUnity<DailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DailyActivityUIRootLogic>.Instance.UpdataActiveInfo();
			}
		}
		return null;
	}
}
