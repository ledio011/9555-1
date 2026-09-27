using System;
using Sproto;
using SprotoType;

// Token: 0x02000276 RID: 630
public class ret_guild_kick_handler
{
	// Token: 0x06001373 RID: 4979 RVA: 0x0007F488 File Offset: 0x0007D688
	public static SprotoTypeBase ret_guild_kick_request(SprotoTypeBase req)
	{
		ret_guild_kick.request request = req as ret_guild_kick.request;
		if (request != null && request.HasState && request.state && request.HasCharacterId)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.PlayerGuild.RemoveByID(request.characterId);
			if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildMemberRootLogic>.Instance.UpdateGuildMemberItemList(playerData.PlayerGuild.GuildMemberList);
			}
		}
		return null;
	}
}
