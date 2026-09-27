using System;
using Sproto;
using SprotoType;

// Token: 0x02000282 RID: 642
public class ret_mount_equip_handler
{
	// Token: 0x0600138B RID: 5003 RVA: 0x0007FC44 File Offset: 0x0007DE44
	public static SprotoTypeBase ret_mount_equip_request(SprotoTypeBase req)
	{
		ret_mount_equip.request request = req as ret_mount_equip.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<PlayerCarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PlayerCarRootLogic>.Instance.UpdateCarPage(request);
				if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_CLICK_EQUIP)
				{
					SingletonUnity<PlayerCarRootLogic>.Instance.CheckTutorialEvent();
				}
			}
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (request.mount_info.ContainsKey(request.ID) && request.mount_info[request.ID].state == 2L)
				{
					playerData.MountId = request.ID;
				}
				if (SingletonUnity<CitySimController>.Exists)
				{
					SingletonUnity<CitySimController>.Instance.UpdateSpecialCarState();
				}
			}
		}
		return null;
	}
}
