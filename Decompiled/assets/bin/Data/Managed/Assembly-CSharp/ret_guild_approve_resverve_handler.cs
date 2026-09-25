using System;
using Sproto;
using SprotoType;

// Token: 0x0200026C RID: 620
public class ret_guild_approve_resverve_handler
{
	// Token: 0x0600135F RID: 4959 RVA: 0x0007EFE0 File Offset: 0x0007D1E0
	public static SprotoTypeBase ret_guild_approve_resverve_request(SprotoTypeBase req)
	{
		ret_guild_approve_resverve.request request = req as ret_guild_approve_resverve.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (request.isAgree == 0L)
			{
				playerData.PlayerGuild.RemoveByID(request.characterId);
			}
			else
			{
				playerData.PlayerGuild.setMemberJob(request.characterId, Guild_JOB.JOB_Member);
			}
			if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildMemberRootLogic>.Instance.UpdateGuildMemberItemList(playerData.PlayerGuild.GuildMemberList);
			}
		}
		return null;
	}
}
