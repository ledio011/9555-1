using System;
using Sproto;
using SprotoType;

// Token: 0x02000245 RID: 581
public class notice_guild_battle_rank_handler
{
	// Token: 0x0600130F RID: 4879 RVA: 0x0007C480 File Offset: 0x0007A680
	public static SprotoTypeBase notice_guild_battle_rank_request(SprotoTypeBase req)
	{
		notice_guild_battle_rank.request request = req as notice_guild_battle_rank.request;
		if (request != null && request.HasGuildId)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ChampionGuildId = request.guildId;
			Singleton<ObjManager>.Instance.RefreshPlayerGuildPic();
		}
		return null;
	}
}
