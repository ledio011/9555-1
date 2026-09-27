using System;
using Sproto;
using SprotoType;

// Token: 0x020002A6 RID: 678
public class ret_sign_week_handler
{
	// Token: 0x060013D3 RID: 5075 RVA: 0x00080F48 File Offset: 0x0007F148
	public static SprotoTypeBase ret_sign_week_request(SprotoTypeBase req)
	{
		ret_sign_week.request request = req as ret_sign_week.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.welfareData.SetWeekFlag(request);
			if (SingletonUnity<SignWeekRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SignWeekRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SignWeekRootLogic>.Instance.UpdateInfo(request);
			}
		}
		return null;
	}
}
