using System;
using Sproto;
using SprotoType;

// Token: 0x0200027F RID: 639
public class ret_guild_skill_level_handler
{
	// Token: 0x06001385 RID: 4997 RVA: 0x0007FADC File Offset: 0x0007DCDC
	public static SprotoTypeBase ret_guild_skill_level_request(SprotoTypeBase req)
	{
		ret_guild_skill_level.request request = req as ret_guild_skill_level.request;
		if (request != null && request.state == 0L)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (request.HasContribute)
			{
				GameMoneyHelper.SetGuildContribute((long)((int)request.contribute));
			}
			if (request.HasGuildSkillType)
			{
				playerData.PlayerGuild.UpdateSkillType(request.guildSkillType, request.level);
			}
			if (SingletonUnity<GuildSkillRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildSkillRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildSkillRootLogic>.Instance.UpdateGuildSKill();
			}
			NoticeLogic.AddNotifyData("#{101145}", true, false);
		}
		return null;
	}
}
