using System;
using Sproto;
using SprotoType;

// Token: 0x0200029F RID: 671
public class ret_request_wild_boss_info_handler
{
	// Token: 0x060013C5 RID: 5061 RVA: 0x00080C28 File Offset: 0x0007EE28
	public static SprotoTypeBase ret_request_wild_boss_info_request(SprotoTypeBase req)
	{
		ret_request_wild_boss_info.request request = req as ret_request_wild_boss_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncWildBossInfoData(request);
			if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RefershBossInfo();
			}
		}
		return null;
	}
}
