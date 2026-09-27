using System;
using Sproto;
using SprotoType;

// Token: 0x0200029B RID: 667
public class ret_request_top_rank_pvp_list_handler
{
	// Token: 0x060013BD RID: 5053 RVA: 0x000808AC File Offset: 0x0007EAAC
	public static SprotoTypeBase ret_request_top_rank_pvp_list_request(SprotoTypeBase req)
	{
		ret_request_top_rank_pvp_list.request request = req as ret_request_top_rank_pvp_list.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
		}
		return null;
	}
}
