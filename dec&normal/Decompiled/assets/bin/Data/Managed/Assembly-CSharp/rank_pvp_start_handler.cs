using System;
using Sproto;
using SprotoType;

// Token: 0x02000251 RID: 593
public class rank_pvp_start_handler
{
	// Token: 0x06001328 RID: 4904 RVA: 0x0007C9B0 File Offset: 0x0007ABB0
	public static SprotoTypeBase rank_pvp_start_request(SprotoTypeBase req)
	{
		rank_pvp_start.request request = req as rank_pvp_start.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.StartGame();
		}
		return null;
	}
}
