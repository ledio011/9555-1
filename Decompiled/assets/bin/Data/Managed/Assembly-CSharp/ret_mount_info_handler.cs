using System;
using Sproto;
using SprotoType;

// Token: 0x02000283 RID: 643
public class ret_mount_info_handler
{
	// Token: 0x0600138D RID: 5005 RVA: 0x0007FD28 File Offset: 0x0007DF28
	public static SprotoTypeBase ret_mount_info_request(SprotoTypeBase req)
	{
		ret_mount_info.request request = req as ret_mount_info.request;
		if (request != null)
		{
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.playerMountData.UpdateMountInfo(request);
			if (SingletonUnity<WaitResponseUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<WaitResponseUIRootLogic>.Instance.gameObject))
			{
				WaitResponseUIRootLogic.CloseBox();
			}
			if (SingletonUnity<PlayerCarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PlayerCarRootLogic>.Instance.Reset(request);
				if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_START)
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTutorialEvent();
				}
			}
		}
		return null;
	}
}
