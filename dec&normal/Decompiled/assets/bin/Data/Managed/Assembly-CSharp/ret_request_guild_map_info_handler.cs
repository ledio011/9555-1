using System;
using Sproto;
using SprotoType;

// Token: 0x02000294 RID: 660
public class ret_request_guild_map_info_handler
{
	// Token: 0x060013AF RID: 5039 RVA: 0x00080568 File Offset: 0x0007E768
	public static SprotoTypeBase ret_request_guild_map_info_request(SprotoTypeBase req)
	{
		ret_request_guild_map_info.request request = req as ret_request_guild_map_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.SyncGuildCityInfo(request);
			if (SingletonUnity<GuildCityRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildCityRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildCityRootLogic>.Instance.UpdateCityInfo();
			}
			if (SingletonUnity<WorldMapRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<WorldMapRoot>.Instance.gameObject))
			{
				SingletonUnity<WorldMapRoot>.Instance.Reset();
			}
		}
		return null;
	}
}
