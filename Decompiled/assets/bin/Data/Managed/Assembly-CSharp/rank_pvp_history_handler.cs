using System;
using Sproto;
using SprotoType;

// Token: 0x0200024F RID: 591
public class rank_pvp_history_handler
{
	// Token: 0x06001324 RID: 4900 RVA: 0x0007C8FC File Offset: 0x0007AAFC
	public static SprotoTypeBase rank_pvp_history_request(SprotoTypeBase req)
	{
		rank_pvp_history.request request = req as rank_pvp_history.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasLogs)
			{
				if (SingletonUnity<PVPLogUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PVPLogUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<PVPLogUIRootLogic>.Instance.UpdataLogList(request.logs);
				}
			}
			else if (SingletonUnity<PVPLogUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PVPLogUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PVPLogUIRootLogic>.Instance.UpdataLogList(null);
			}
		}
		return null;
	}
}
