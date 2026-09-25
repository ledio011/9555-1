using System;
using Sproto;
using SprotoType;

// Token: 0x02000277 RID: 631
public class ret_guild_leave_handler
{
	// Token: 0x06001375 RID: 4981 RVA: 0x0007F51C File Offset: 0x0007D71C
	public static SprotoTypeBase ret_guild_leave_request(SprotoTypeBase req)
	{
		ret_guild_leave.request request = req as ret_guild_leave.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.PlayerGuild.ResetGuild();
			if (Singleton<ObjManager>.Instance.MainPlayer != null)
			{
				Singleton<ObjManager>.Instance.MainPlayer.SearchAllGuild();
			}
			if (!SingletonUnity<NewGuildUIRootLogic>.Exists || UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
			{
			}
		}
		return null;
	}
}
