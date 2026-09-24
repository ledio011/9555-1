using System;
using Sproto;
using SprotoType;

// Token: 0x02000233 RID: 563
public class get_level_reward_handler
{
	// Token: 0x060012E7 RID: 4839 RVA: 0x0007B4F0 File Offset: 0x000796F0
	public static SprotoTypeBase get_level_reward_request(SprotoTypeBase req)
	{
		get_level_reward.request request = req as get_level_reward.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.SyncLevelReward(request);
			if (SingletonUnity<LevelRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<LevelRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<LevelRewardRootLogic>.Instance.UpdateInfo(request);
			}
			if (request.HasItems && request.items.Count > 0)
			{
				SimpleRewardRootLogic.AddRewards(request.items);
			}
		}
		return null;
	}
}
