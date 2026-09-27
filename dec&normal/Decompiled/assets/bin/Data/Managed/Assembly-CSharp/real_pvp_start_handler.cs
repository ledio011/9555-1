using System;
using Sproto;
using SprotoType;

// Token: 0x02000252 RID: 594
public class real_pvp_start_handler
{
	// Token: 0x0600132A RID: 4906 RVA: 0x0007C9E4 File Offset: 0x0007ABE4
	public static SprotoTypeBase real_pvp_start_request(SprotoTypeBase req)
	{
		real_pvp_start.request request = req as real_pvp_start.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.StartGame();
		}
		return null;
	}
}
