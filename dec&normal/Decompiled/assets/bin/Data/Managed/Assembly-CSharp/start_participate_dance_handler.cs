using System;
using Sproto;
using SprotoType;

// Token: 0x020002BF RID: 703
public class start_participate_dance_handler
{
	// Token: 0x06001406 RID: 5126 RVA: 0x00081E70 File Offset: 0x00080070
	public static SprotoTypeBase start_participate_dance_request(SprotoTypeBase req)
	{
		start_participate_dance.request request = req as start_participate_dance.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerDanceData.SyncPlayerDanceInfo(request);
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			mainPlayer.StartDance(request.curUse);
		}
		return null;
	}
}
