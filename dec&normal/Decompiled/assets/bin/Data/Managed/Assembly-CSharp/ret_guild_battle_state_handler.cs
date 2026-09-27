using System;
using Sproto;
using SprotoType;

// Token: 0x02000271 RID: 625
public class ret_guild_battle_state_handler
{
	// Token: 0x06001369 RID: 4969 RVA: 0x0007F1BC File Offset: 0x0007D3BC
	public static SprotoTypeBase ret_guild_battle_state_request(SprotoTypeBase req)
	{
		ret_guild_battle_state.request request = req as ret_guild_battle_state.request;
		if (request != null)
		{
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncGuildBattleInfo(request);
		}
		return null;
	}
}
