using System;
using Sproto;
using SprotoType;

// Token: 0x0200027A RID: 634
public class ret_guild_map_reward_handler
{
	// Token: 0x0600137B RID: 4987 RVA: 0x0007F678 File Offset: 0x0007D878
	public static SprotoTypeBase ret_guild_map_reward_request(SprotoTypeBase req)
	{
		ret_guild_map_reward.request request = req as ret_guild_map_reward.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.SyncGuildCityRewardInfo(request);
			if (SingletonUnity<GuildCityRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildCityRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildCityRootLogic>.Instance.UpdateCityItemInfo(request);
			}
		}
		return null;
	}
}
