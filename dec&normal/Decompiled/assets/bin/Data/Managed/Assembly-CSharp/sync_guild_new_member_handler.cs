using System;
using Sproto;
using SprotoType;

// Token: 0x020002C9 RID: 713
public class sync_guild_new_member_handler
{
	// Token: 0x0600141A RID: 5146 RVA: 0x000827E4 File Offset: 0x000809E4
	public static SprotoTypeBase sync_guild_new_member_request(SprotoTypeBase req)
	{
		sync_guild_new_member.request request = req as sync_guild_new_member.request;
		if (request != null && request.HasGuild_member_info)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.PlayerGuild.AddNewMember(request.guild_member_info);
			if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildMemberRootLogic>.Instance.UpdateGuildMemberItemList(playerData.PlayerGuild.GuildMemberList);
			}
		}
		return null;
	}
}
