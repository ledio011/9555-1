using System;
using Sproto;
using SprotoType;

// Token: 0x0200026E RID: 622
public class ret_guild_battle_info_handler
{
	// Token: 0x06001363 RID: 4963 RVA: 0x0007F098 File Offset: 0x0007D298
	public static SprotoTypeBase ret_guild_battle_info_request(SprotoTypeBase req)
	{
		ret_guild_battle_info.request request = req as ret_guild_battle_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncGuildBattleInfo(request);
			if (SingletonUnity<GuildBattleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildBattleRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
