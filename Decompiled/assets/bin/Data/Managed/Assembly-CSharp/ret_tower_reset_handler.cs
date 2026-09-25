using System;
using Sproto;
using SprotoType;

// Token: 0x020002AE RID: 686
public class ret_tower_reset_handler
{
	// Token: 0x060013E3 RID: 5091 RVA: 0x00081364 File Offset: 0x0007F564
	public static SprotoTypeBase ret_tower_reset_request(SprotoTypeBase req)
	{
		ret_tower_reset.request request = req as ret_tower_reset.request;
		if (request != null && request.state)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TowerData.ResetTowerData();
			if (SingletonUnity<TowerUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TowerUIRootLogic>.Instance.gameObject))
			{
				tower_info playerTowerInfo = SingletonUnity<TowerUIRootLogic>.Instance.PlayerTowerInfo;
				playerTowerInfo.times = 0L;
				playerTowerInfo.cur_floor = 0L;
				SingletonUnity<TowerUIRootLogic>.Instance.UpdateTowerInfo(playerTowerInfo);
			}
			if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
			{
				tower_info mPlayerTowerInfo = SingletonUnity<NewDailyCopyUIRootLogic>.Instance.mPlayerTowerInfo;
				mPlayerTowerInfo.times = 0L;
				mPlayerTowerInfo.cur_floor = 0L;
				SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ResetTowerInfo(mPlayerTowerInfo);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", "reset", "resettimes");
		}
		return null;
	}
}
