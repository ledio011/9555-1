using System;
using Sproto;
using SprotoType;

// Token: 0x02000280 RID: 640
public class ret_guild_star_handler
{
	// Token: 0x06001387 RID: 4999 RVA: 0x0007FB88 File Offset: 0x0007DD88
	public static SprotoTypeBase ret_guild_star_request(SprotoTypeBase req)
	{
		ret_guild_star.request request = req as ret_guild_star.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<GuildStarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildStarRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildStarRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
