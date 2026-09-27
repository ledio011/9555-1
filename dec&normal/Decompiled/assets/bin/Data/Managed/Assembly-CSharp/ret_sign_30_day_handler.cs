using System;
using Sproto;
using SprotoType;

// Token: 0x020002A5 RID: 677
public class ret_sign_30_day_handler
{
	// Token: 0x060013D1 RID: 5073 RVA: 0x00080EE0 File Offset: 0x0007F0E0
	public static SprotoTypeBase ret_sign_30_day_request(SprotoTypeBase req)
	{
		ret_sign_30_day.request request = req as ret_sign_30_day.request;
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
