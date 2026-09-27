using System;
using Sproto;
using SprotoType;

// Token: 0x02000236 RID: 566
public class guild_battle_finish_info_handler
{
	// Token: 0x060012ED RID: 4845 RVA: 0x0007B9B0 File Offset: 0x00079BB0
	public static SprotoTypeBase guild_battle_finish_info_request(SprotoTypeBase req)
	{
		guild_battle_finish_info.request request = req as guild_battle_finish_info.request;
		if (request != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleResultRoot, delegate
			{
				SingletonUnity<GuildBattleResultRootLogic>.Instance.RefreshInfo(request);
			}, null);
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CompleteMission();
		}
		return null;
	}
}
