using System;
using Sproto;
using SprotoType;

// Token: 0x0200027E RID: 638
public class ret_guild_score_info_handler
{
	// Token: 0x06001383 RID: 4995 RVA: 0x0007FA7C File Offset: 0x0007DC7C
	public static SprotoTypeBase ret_guild_score_info_request(SprotoTypeBase req)
	{
		ret_guild_score_info.request request = req as ret_guild_score_info.request;
		if (request != null)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager.IsBigWorld())
			{
				return null;
			}
			if (request.HasGuild_battle_score_info && SingletonUnity<GuildBattleInfoRoot>.Exists)
			{
				SingletonUnity<GuildBattleInfoRoot>.Instance.UpdateInfo(request.guild_battle_score_info);
			}
		}
		return null;
	}
}
