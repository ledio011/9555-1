using System;
using Sproto;
using SprotoType;

// Token: 0x02000237 RID: 567
public class guild_battle_start_handler
{
	// Token: 0x060012EF RID: 4847 RVA: 0x0007BA10 File Offset: 0x00079C10
	public static SprotoTypeBase guild_battle_start_request(SprotoTypeBase req)
	{
		guild_battle_start.request request = req as guild_battle_start.request;
		if (request != null)
		{
			GuildBattleSceneManager guildBattleSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as GuildBattleSceneManager;
			if (guildBattleSceneManager != null)
			{
				guildBattleSceneManager.OpenControl();
			}
		}
		return null;
	}
}
