using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x020002A2 RID: 674
public class ret_search_guild_handler
{
	// Token: 0x060013CB RID: 5067 RVA: 0x00080D60 File Offset: 0x0007EF60
	public static SprotoTypeBase ret_search_guild_request(SprotoTypeBase req)
	{
		ret_search_guild.request request = req as ret_search_guild.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasGuild)
			{
				List<guild_info> list = new List<guild_info>();
				list.Clear();
				list.Add(request.guild);
				List<long> list2 = new List<long>();
				list2.Clear();
				list2.Add(request.rank);
				if (SingletonUnity<GuildListInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildListInfoRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildListInfoRootLogic>.Instance.ShowSearchResult(list, list2);
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100793}", true, false);
				if (SingletonUnity<GuildListInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildListInfoRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildListInfoRootLogic>.Instance.DisableSearchFlag();
				}
			}
		}
		return null;
	}
}
