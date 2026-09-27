using System;
using Sproto;
using SprotoType;

// Token: 0x02000278 RID: 632
public class ret_guild_log_handler
{
	// Token: 0x06001377 RID: 4983 RVA: 0x0007F598 File Offset: 0x0007D798
	public static SprotoTypeBase ret_guild_log_request(SprotoTypeBase req)
	{
		ret_guild_log.request request = req as ret_guild_log.request;
		if (request != null)
		{
			if (request.HasLogs)
			{
				if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildMemberRootLogic>.Instance.ShowGuildLogs(request.logs);
				}
			}
			else if (SingletonUnity<GuildMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildMemberRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildMemberRootLogic>.Instance.ShowGuildLogs(null);
			}
		}
		return null;
	}
}
