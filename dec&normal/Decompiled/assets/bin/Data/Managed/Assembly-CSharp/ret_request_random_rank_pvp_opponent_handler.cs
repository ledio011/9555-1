using System;
using Sproto;
using SprotoType;

// Token: 0x02000297 RID: 663
public class ret_request_random_rank_pvp_opponent_handler
{
	// Token: 0x060013B5 RID: 5045 RVA: 0x000806C8 File Offset: 0x0007E8C8
	public static SprotoTypeBase ret_request_random_rank_pvp_opponent_request(SprotoTypeBase req)
	{
		ret_request_random_rank_pvp_opponent.request request = req as ret_request_random_rank_pvp_opponent.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			int num = (int)request.opponentNum;
			if (num > 0 && SingletonUnity<RankPVPUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RankPVPUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<RankPVPUIRootLogic>.Instance.ResetOtherPlayer(request);
			}
		}
		return null;
	}
}
