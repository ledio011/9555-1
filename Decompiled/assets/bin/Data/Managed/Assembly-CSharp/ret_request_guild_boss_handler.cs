using System;
using Sproto;
using SprotoType;

// Token: 0x02000293 RID: 659
public class ret_request_guild_boss_handler
{
	// Token: 0x060013AD RID: 5037 RVA: 0x000804B8 File Offset: 0x0007E6B8
	public static SprotoTypeBase ret_request_guild_boss_request(SprotoTypeBase req)
	{
		ret_request_guild_boss.request request = req as ret_request_guild_boss.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncGuildBossInfoData(request);
			if (SingletonUnity<GuildActivityRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildActivityRootLogic>.Instance.gameObject))
			{
				if (SingletonUnity<GuildActivityRootLogic>.Instance.CurChoosedIndex == -1)
				{
					SingletonUnity<GuildActivityRootLogic>.Instance.RefershBossInfo(request);
				}
				else
				{
					SingletonUnity<GuildActivityRootLogic>.Instance.UpdateGuildBossInfo(request);
				}
			}
			if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RefershGuildBossInfo();
			}
		}
		return null;
	}
}
