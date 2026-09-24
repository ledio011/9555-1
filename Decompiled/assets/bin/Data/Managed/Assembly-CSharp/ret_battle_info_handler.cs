using System;
using Sproto;
using SprotoType;

// Token: 0x0200025A RID: 602
public class ret_battle_info_handler
{
	// Token: 0x0600133A RID: 4922 RVA: 0x0007CD00 File Offset: 0x0007AF00
	public static SprotoTypeBase ret_battle_info_request(SprotoTypeBase req)
	{
		ret_battle_info.request request = req as ret_battle_info.request;
		if (request != null)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager.IsBigWorld())
			{
				return null;
			}
			if (request.battle_info.HasDamage_list && request.battle_info.damage_list.Count > 0 && SingletonUnity<MultiRankSmallRootLogic>.Exists)
			{
				if (request.type == 4L)
				{
					SingletonUnity<MultiRankSmallRootLogic>.Instance.Reset(request.battle_info, GameDefine.ACTIVITY_TYPE.BAR_FIGHT);
				}
				else
				{
					SingletonUnity<MultiRankSmallRootLogic>.Instance.Reset(request.battle_info);
				}
			}
			if (request.battle_info.HasEnd_time && SingletonUnity<CountDownTimeLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CountDownTimeLogic>.Instance.gameObject))
			{
				SingletonUnity<CountDownTimeLogic>.Instance.SetReamainTime(request.battle_info.end_time, 0L);
			}
		}
		return null;
	}
}
