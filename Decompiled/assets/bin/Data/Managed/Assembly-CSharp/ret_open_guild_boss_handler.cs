using System;
using Sproto;
using SprotoType;

// Token: 0x02000286 RID: 646
public class ret_open_guild_boss_handler
{
	// Token: 0x06001393 RID: 5011 RVA: 0x0007FEB4 File Offset: 0x0007E0B4
	public static SprotoTypeBase ret_open_guild_boss_request(SprotoTypeBase req)
	{
		ret_open_guild_boss.request request = req as ret_open_guild_boss.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.ok && request.HasGuild_boss && SingletonUnity<GuildActivityRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildActivityRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildActivityRootLogic>.Instance.refershOpenBtn(request.guild_boss);
			}
		}
		return null;
	}
}
