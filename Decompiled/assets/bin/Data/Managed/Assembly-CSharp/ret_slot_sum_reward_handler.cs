using System;
using Sproto;
using SprotoType;

// Token: 0x020002A9 RID: 681
public class ret_slot_sum_reward_handler
{
	// Token: 0x060013D9 RID: 5081 RVA: 0x000810A0 File Offset: 0x0007F2A0
	public static SprotoTypeBase ret_slot_sum_reward_request(SprotoTypeBase req)
	{
		ret_slot_sum_reward.request request = req as ret_slot_sum_reward.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasItems && SingletonUnity<SlotUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SlotUIRootLogic>.Instance.ShowSumReward(request.items, request.sumNum);
			}
		}
		return null;
	}
}
