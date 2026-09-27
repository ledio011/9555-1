using System;
using Sproto;
using SprotoType;

// Token: 0x0200024E RID: 590
public class rank_pvp_create_zombie_user_handler
{
	// Token: 0x06001321 RID: 4897 RVA: 0x0007C870 File Offset: 0x0007AA70
	public static SprotoTypeBase rank_pvp_create_zombie_user_request(SprotoTypeBase req)
	{
		rank_pvp_create_zombie_user.request request = req as rank_pvp_create_zombie_user.request;
		if (request != null)
		{
			ObjInitPlayerData objInitPlayerData = new ObjInitPlayerData();
			objInitPlayerData.InitData(request.character);
			Singleton<ObjManager>.Instance.CreateZombiePlayer(objInitPlayerData);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PVPBeforeStartRoot, new UIManager.OnOpenUIDelegate(rank_pvp_create_zombie_user_handler.OnPVPBeforeStartShow), request.character);
		}
		return null;
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x0007C8CC File Offset: 0x0007AACC
	private static void OnPVPBeforeStartShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			character character = param as character;
			SingletonUnity<PVPBeforeStartRootLogic>.Instance.ResetRankPVPPage(character);
		}
	}
}
