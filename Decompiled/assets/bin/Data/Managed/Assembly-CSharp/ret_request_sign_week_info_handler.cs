using System;
using Sproto;
using SprotoType;

// Token: 0x02000299 RID: 665
public class ret_request_sign_week_info_handler
{
	// Token: 0x060013B9 RID: 5049 RVA: 0x00080790 File Offset: 0x0007E990
	public static SprotoTypeBase ret_request_sign_week_info_request(SprotoTypeBase req)
	{
		ret_request_sign_week_info.request request = req as ret_request_sign_week_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.welfareData.SetWeekFlag(request);
			if (SingletonUnity<SignWeekRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SignWeekRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SignWeekRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
