using System;
using Sproto;
using SprotoType;

// Token: 0x02000281 RID: 641
public class ret_level_reward_handler
{
	// Token: 0x06001389 RID: 5001 RVA: 0x0007FBDC File Offset: 0x0007DDDC
	public static SprotoTypeBase ret_level_reward_request(SprotoTypeBase req)
	{
		ret_level_reward.request request = req as ret_level_reward.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.SyncLevelReward(request);
			if (SingletonUnity<LevelRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<LevelRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<LevelRewardRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
