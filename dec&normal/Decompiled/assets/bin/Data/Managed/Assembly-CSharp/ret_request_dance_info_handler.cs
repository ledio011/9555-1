using System;
using Sproto;
using SprotoType;

// Token: 0x02000291 RID: 657
public class ret_request_dance_info_handler
{
	// Token: 0x060013A9 RID: 5033 RVA: 0x000803DC File Offset: 0x0007E5DC
	public static SprotoTypeBase ret_request_dance_info_request(SprotoTypeBase req)
	{
		ret_request_dance_info.request request = req as ret_request_dance_info.request;
		if (request != null)
		{
			PlayerDanceData playerDanceData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerDanceData;
			playerDanceData.SyncPlayerDanceInfo(request);
			if (request.type == 0L && SingletonUnity<WaitResponseUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<WaitResponseUIRootLogic>.Instance.gameObject))
			{
				WaitResponseUIRootLogic.CloseBox();
				if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DanceChooseRoot, null, null);
				}
			}
		}
		return null;
	}
}
