using System;
using Sproto;
using SprotoType;

// Token: 0x020002AD RID: 685
public class ret_top_rank_list_handler
{
	// Token: 0x060013E1 RID: 5089 RVA: 0x00081324 File Offset: 0x0007F524
	public static SprotoTypeBase ret_top_rank_list_request(SprotoTypeBase req)
	{
		ret_top_rank_list.request request = req as ret_top_rank_list.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<PlayerRankInfoRootLogic>.Exists)
			{
				SingletonUnity<PlayerRankInfoRootLogic>.Instance.UpdateRankTypeList(request);
			}
		}
		return null;
	}
}
