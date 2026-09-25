using System;
using Sproto;
using SprotoType;

// Token: 0x0200022A RID: 554
public class comb_value_up_tip_handler
{
	// Token: 0x060012D1 RID: 4817 RVA: 0x0007AF5C File Offset: 0x0007915C
	public static SprotoTypeBase comb_value_up_tip_request(SprotoTypeBase req)
	{
		comb_value_up_tip.request request = req as comb_value_up_tip.request;
		if (request != null)
		{
			FightingValUpgradeRootLogic.ShowFinghtingValUpgradeRoot(request.current, request.next);
		}
		return null;
	}
}
