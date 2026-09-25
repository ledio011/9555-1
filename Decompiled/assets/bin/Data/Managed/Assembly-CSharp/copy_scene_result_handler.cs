using System;
using Sproto;
using SprotoType;

// Token: 0x0200022B RID: 555
public class copy_scene_result_handler
{
	// Token: 0x060012D3 RID: 4819 RVA: 0x0007AF90 File Offset: 0x00079190
	public static SprotoTypeBase copy_scene_result_request(SprotoTypeBase req)
	{
		copy_scene_result.request request = req as copy_scene_result.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.win)
			{
				if (request.HasSwipe)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
					{
						SingletonUnity<MissionPassShowRootLogic>.Instance.ResetEquipSwipe(request.items);
						SimpleRewardRootLogic.AddRewards(request.items);
					}, null);
					return null;
				}
				if (request.subType == 9L)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyMissionShowRoot, delegate
					{
						SingletonUnity<CopyMissionShowRootLogic>.Instance.ResetTowerCopy(request);
					}, null);
					if (request.HasParm)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TowerData.UpdateTowerbest(request.parm);
						int num = (int)request.parm / 5 * 5;
						int num2 = (int)request.parm / 5 * 5 + 4;
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", string.Format("stage{0}_{1}", num, num2), "success");
					}
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.StarRewardPageRoot, delegate(bool bSuccess, object param)
					{
						if (bSuccess)
						{
							copy_scene_result.request request = param as copy_scene_result.request;
							if (request != null)
							{
								SingletonUnity<StarRewardPageRoot>.Instance.ResetCopySceneReward(request);
							}
						}
					}, request);
				}
			}
			else if (request.subType == 9L)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyFailShowRoot, delegate(bool bSuccess, object param)
				{
					if (bSuccess)
					{
						string id = param as string;
						SingletonUnity<CopyFailShowRootLogic>.Instance.ResetTowerCopy(id);
					}
				}, request.id);
				if (request.HasParm)
				{
					int num3 = (int)request.parm / 5 * 5;
					int num4 = (int)request.parm / 5 * 5 + 4;
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", string.Format("stage{0}_{1}", num3, num4), "failure");
				}
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyFailShowRoot, delegate(bool bSuccess, object param)
				{
					if (bSuccess)
					{
						SingletonUnity<CopyFailShowRootLogic>.Instance.ResetNormalCopy();
					}
				}, null);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", request.id), "failure");
			}
			CountDownTimeLogic.CloseTime();
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CompleteMission();
		}
		return null;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x0007B210 File Offset: 0x00079410
	private static void OnRewardPageShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			copy_scene_result.request request = param as copy_scene_result.request;
			if (request != null)
			{
				SingletonUnity<RewardPageRootLogic>.Instance.ResetCopySceneReward(request);
			}
		}
	}
}
