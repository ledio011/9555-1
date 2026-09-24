using System;
using Sproto;
using SprotoType;

// Token: 0x0200026F RID: 623
public class ret_guild_battle_member_handler
{
	// Token: 0x06001365 RID: 4965 RVA: 0x0007F100 File Offset: 0x0007D300
	public static SprotoTypeBase ret_guild_battle_member_request(SprotoTypeBase req)
	{
		ret_guild_battle_member.request request = req as ret_guild_battle_member.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncGuildBattleMember(request);
			if (SingletonUnity<GuildBattleMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleMemberRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildBattleMemberRootLogic>.Instance.RefershInfo(request);
			}
		}
		return null;
	}
}
