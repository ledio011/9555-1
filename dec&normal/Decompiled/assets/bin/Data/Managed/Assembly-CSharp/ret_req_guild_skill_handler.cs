using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class ret_req_guild_skill_handler
{
	// Token: 0x0600139D RID: 5021 RVA: 0x000800A0 File Offset: 0x0007E2A0
	public static SprotoTypeBase ret_req_guild_skill_request(SprotoTypeBase req)
	{
		ret_req_guild_skill.request request = req as ret_req_guild_skill.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasGuild_skill)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				playerData.PlayerGuild.UpdateSKill(request.guild_skill);
				if (SingletonUnity<GuildSkillRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildSkillRootLogic>.Instance.gameObject))
				{
					SingletonUnity<GuildSkillRootLogic>.Instance.UpdateGuildSKill();
				}
			}
			else
			{
				Debug.LogError("ret_req_guild_skill guild skill null!!!");
			}
		}
		return null;
	}
}
