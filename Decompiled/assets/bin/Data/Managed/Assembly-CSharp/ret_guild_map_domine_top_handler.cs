using System;
using Sproto;
using SprotoType;

// Token: 0x02000279 RID: 633
public class ret_guild_map_domine_top_handler
{
	// Token: 0x06001379 RID: 4985 RVA: 0x0007F624 File Offset: 0x0007D824
	public static SprotoTypeBase ret_guild_map_domine_top_request(SprotoTypeBase req)
	{
		ret_guild_map_domine_top.request request = req as ret_guild_map_domine_top.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<CityDamageRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CityDamageRootLogic>.Instance.gameObject))
			{
				SingletonUnity<CityDamageRootLogic>.Instance.UpdateInfo(request);
			}
		}
		return null;
	}
}
