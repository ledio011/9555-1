using System;
using Sproto;
using SprotoType;

// Token: 0x0200027B RID: 635
public class ret_guild_member_info_handler
{
	// Token: 0x0600137D RID: 4989 RVA: 0x0007F6DC File Offset: 0x0007D8DC
	public static SprotoTypeBase ret_guild_member_info_request(SprotoTypeBase req)
	{
		ret_guild_member_info.request request = req as ret_guild_member_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasGuild_member_info)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				playerData.PlayerGuild.UpDataGuildMemberList(request.guild_member_info);
				if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildMemberRootLogic>.Instance.UpdateGuildMemberItemList(playerData.PlayerGuild.GuildMemberList);
				}
				if (SingletonUnity<TeamInviteRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamInviteRootLogic>.Instance.gameObject))
				{
					SingletonUnity<TeamInviteRootLogic>.Instance.UpdateGuildMemberInfo();
				}
			}
		}
		return null;
	}
}
