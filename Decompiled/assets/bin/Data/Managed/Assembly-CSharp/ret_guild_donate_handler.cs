using System;
using Sproto;
using SprotoType;

// Token: 0x02000273 RID: 627
public class ret_guild_donate_handler
{
	// Token: 0x0600136D RID: 4973 RVA: 0x0007F22C File Offset: 0x0007D42C
	public static SprotoTypeBase ret_guild_donate_request(SprotoTypeBase req)
	{
		ret_guild_donate.request request = req as ret_guild_donate.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (request.state == 0L)
			{
				playerData.PlayerGuild.UseDonate(request.id);
				playerData.PlayerGuild.GuilLevel = (int)request.level;
				playerData.PlayerGuild.GuildExp = (int)request.exp;
				playerData.PlayerGuild.UpdateAllContribute((!request.HasAll_contribute) ? 0 : ((int)request.all_contribute));
				if (request.HasContribute)
				{
					GameMoneyHelper.SetGuildContribute((long)((int)request.contribute));
				}
				else
				{
					GameMoneyHelper.SetGuildContribute(0L);
				}
				if (SingletonUnity<GuildInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildInfoRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildInfoRootLogic>.Instance.UpDateGuildInfo(playerData.PlayerGuild);
				}
				NoticeLogic.AddNotifyData("#{100789}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100790}", true, false);
			}
		}
		return null;
	}
}
