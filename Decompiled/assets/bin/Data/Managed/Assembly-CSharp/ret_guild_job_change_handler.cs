using System;
using Sproto;
using SprotoType;

// Token: 0x02000274 RID: 628
public class ret_guild_job_change_handler
{
	// Token: 0x0600136F RID: 4975 RVA: 0x0007F32C File Offset: 0x0007D52C
	public static SprotoTypeBase ret_guild_job_change_request(SprotoTypeBase req)
	{
		ret_guild_job_change.request request = req as ret_guild_job_change.request;
		if (request != null && request.state)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (request.HasCharacterId && request.HasJob)
			{
				if ((int)request.job == 0)
				{
					playerData.PlayerGuild.GuildChiefId = request.characterId;
					playerData.PlayerGuild.GuildChiefName = playerData.PlayerGuild.getMemberName(request.characterId);
					playerData.PlayerGuild.setMemberJob(Singleton<ObjManager>.Instance.MainPlayer.ServerId, Guild_JOB.JOB_Member);
				}
				playerData.PlayerGuild.setMemberJob(request.characterId, (Guild_JOB)request.job);
				if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildMemberRootLogic>.Instance.UpdateGuildMemberItemList(playerData.PlayerGuild.GuildMemberList);
				}
			}
		}
		return null;
	}
}
