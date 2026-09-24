using System;
using Sproto;
using SprotoType;

// Token: 0x02000275 RID: 629
public class ret_guild_join_handler
{
	// Token: 0x06001371 RID: 4977 RVA: 0x0007F41C File Offset: 0x0007D61C
	public static SprotoTypeBase ret_guild_join_request(SprotoTypeBase req)
	{
		ret_guild_join.request request = req as ret_guild_join.request;
		if (request != null && request.HasGuildId)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId = request.guildId;
			if (Singleton<ObjManager>.Instance.MainPlayer != null)
			{
				Singleton<ObjManager>.Instance.MainPlayer.ApplyUpDataGuild();
			}
		}
		return null;
	}
}
