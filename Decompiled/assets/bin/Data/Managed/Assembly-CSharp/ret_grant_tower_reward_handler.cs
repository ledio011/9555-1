using System;
using Sproto;
using SprotoType;

// Token: 0x0200026B RID: 619
public class ret_grant_tower_reward_handler
{
	// Token: 0x0600135D RID: 4957 RVA: 0x0007EF54 File Offset: 0x0007D154
	public static SprotoTypeBase ret_grant_tower_reward_request(SprotoTypeBase req)
	{
		ret_grant_tower_reward.request request = req as ret_grant_tower_reward.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TowerData.UpdateTowerData(request);
			if (SingletonUnity<TowerUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TowerUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<TowerUIRootLogic>.Instance.UpdateTowerInfo(request);
			}
			if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ResetTowerInfo(request);
			}
		}
		return null;
	}
}
