using System;
using Sproto;
using SprotoType;

// Token: 0x020002B0 RID: 688
public class ret_update_guild_star_handler
{
	// Token: 0x060013E7 RID: 5095 RVA: 0x00081558 File Offset: 0x0007F758
	public static SprotoTypeBase ret_update_guild_star_request(SprotoTypeBase req)
	{
		ret_update_guild_star.request request = req as ret_update_guild_star.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<GuildStarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildStarRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildStarRootLogic>.Instance.UpdateInfo(request);
			}
		}
		return null;
	}
}
