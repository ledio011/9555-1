using System;
using Sproto;
using SprotoType;

// Token: 0x020002A0 RID: 672
public class ret_require_vip_info_handler
{
	// Token: 0x060013C7 RID: 5063 RVA: 0x00080C90 File Offset: 0x0007EE90
	public static SprotoTypeBase ret_require_vip_info_request(SprotoTypeBase req)
	{
		ret_require_vip_info.request request = req as ret_require_vip_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.SyncVipInfo(request);
			if (SingletonUnity<MonthlyCardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MonthlyCardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MonthlyCardRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
