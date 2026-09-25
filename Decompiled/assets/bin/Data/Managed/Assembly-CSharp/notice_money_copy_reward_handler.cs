using System;
using Sproto;
using SprotoType;

// Token: 0x02000247 RID: 583
public class notice_money_copy_reward_handler
{
	// Token: 0x06001313 RID: 4883 RVA: 0x0007C508 File Offset: 0x0007A708
	public static SprotoTypeBase notice_money_copy_reward_request(SprotoTypeBase req)
	{
		notice_money_copy_reward.request request = req as notice_money_copy_reward.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurSceneReward = (int)request.items[0].itemCount;
		}
		return null;
	}
}
