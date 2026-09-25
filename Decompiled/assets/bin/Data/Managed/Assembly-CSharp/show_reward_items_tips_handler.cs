using System;
using Sproto;
using SprotoType;

// Token: 0x020002BD RID: 701
public class show_reward_items_tips_handler
{
	// Token: 0x06001401 RID: 5121 RVA: 0x00081D04 File Offset: 0x0007FF04
	public static SprotoTypeBase show_reward_items_tips_request(SprotoTypeBase req)
	{
		show_reward_items_tips.request request = req as show_reward_items_tips.request;
		if (request != null)
		{
			SimpleRewardRootLogic.AddRewards(request.items);
		}
		return null;
	}
}
