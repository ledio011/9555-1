using System;
using Sproto;
using SprotoType;

// Token: 0x0200028D RID: 653
public class ret_request_activity_info_handler
{
	// Token: 0x060013A1 RID: 5025 RVA: 0x00080190 File Offset: 0x0007E390
	public static SprotoTypeBase ret_request_activity_info_request(SprotoTypeBase req)
	{
		ret_request_activity_info.request request = req as ret_request_activity_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncActivityInfoData(request);
			if (SingletonUnity<DailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DailyActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DailyActivityUIRootLogic>.Instance.RefreshActivityPage(request);
			}
			if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewDailyActivityUIRootLogic>.Instance.RefreshActivityPage();
			}
			instance.SceneManager.CheckSceneActivity();
		}
		return null;
	}
}
