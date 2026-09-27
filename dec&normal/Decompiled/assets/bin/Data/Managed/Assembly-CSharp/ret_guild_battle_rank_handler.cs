using System;
using Sproto;
using SprotoType;

// Token: 0x02000270 RID: 624
public class ret_guild_battle_rank_handler
{
	// Token: 0x06001367 RID: 4967 RVA: 0x0007F168 File Offset: 0x0007D368
	public static SprotoTypeBase ret_guild_battle_rank_request(SprotoTypeBase req)
	{
		ret_guild_battle_rank.request request = req as ret_guild_battle_rank.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<GuildBattleRankRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleRankRoot>.Instance.gameObject))
			{
				SingletonUnity<GuildBattleRankRoot>.Instance.ResershInfo(request);
			}
		}
		return null;
	}
}
