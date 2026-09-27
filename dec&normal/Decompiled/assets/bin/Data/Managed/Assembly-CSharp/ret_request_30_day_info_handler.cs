using System;
using Sproto;
using SprotoType;

// Token: 0x0200028C RID: 652
public class ret_request_30_day_info_handler
{
	// Token: 0x0600139F RID: 5023 RVA: 0x00080128 File Offset: 0x0007E328
	public static SprotoTypeBase ret_request_30_day_info_request(SprotoTypeBase req)
	{
		ret_request_30_day_info.request request = req as ret_request_30_day_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.welfareData.SetMonthFlag(request);
			if (SingletonUnity<SignMonthRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SignMonthRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SignMonthRootLogic>.Instance.UpdateInfo(request);
			}
		}
		return null;
	}
}
