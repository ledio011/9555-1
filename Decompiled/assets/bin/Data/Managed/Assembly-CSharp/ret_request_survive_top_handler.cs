using System;
using Sproto;
using SprotoType;

// Token: 0x0200029A RID: 666
public class ret_request_survive_top_handler
{
	// Token: 0x060013BB RID: 5051 RVA: 0x000807F8 File Offset: 0x0007E9F8
	public static SprotoTypeBase ret_request_survive_top_request(SprotoTypeBase req)
	{
		ret_request_survive_top.request request = req as ret_request_survive_top.request;
		if (request != null)
		{
			SurvivalBattleSceneManager survivalBattleSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as SurvivalBattleSceneManager;
			if (survivalBattleSceneManager == null)
			{
				return null;
			}
			if (request.HasScore_infos && SingletonUnity<MultiRankSmallRootLogic>.Exists)
			{
				SingletonUnity<MultiRankSmallRootLogic>.Instance.Reset(request);
			}
			if (request.HasEnd_time && SingletonUnity<CountDownTimeLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CountDownTimeLogic>.Instance.gameObject))
			{
				SingletonUnity<CountDownTimeLogic>.Instance.SetReamainTime(request.end_time, 0L);
			}
			if (request.HasMy_score && survivalBattleSceneManager != null)
			{
				survivalBattleSceneManager.UpdatePlayerScore(request.my_score);
			}
		}
		return null;
	}
}
