using System;
using Sproto;
using SprotoType;

// Token: 0x02000235 RID: 565
public class grant_daily_mission_reward_handler
{
	// Token: 0x060012EB RID: 4843 RVA: 0x0007B8C8 File Offset: 0x00079AC8
	public static SprotoTypeBase grant_daily_mission_reward_request(SprotoTypeBase req)
	{
		grant_daily_mission_reward.request request = req as grant_daily_mission_reward.request;
		if (request != null)
		{
			if (request.HasItems2 && request.HasItems)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
				{
					SingletonUnity<MissionPassShowRootLogic>.Instance.ResetDailyMissionReward(request.items, delegate
					{
						SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoDailyMissionCheck();
						vp_Timer.In(0.5f, delegate()
						{
							SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
							{
								SingletonUnity<MissionPassShowRootLogic>.Instance.ResetDailyFinishReward(request.items2, null);
							}, null);
						}, null);
					});
				}, null);
			}
			else if (request.HasItems)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
				{
					SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoDailyMissionCheck();
					SingletonUnity<MissionPassShowRootLogic>.Instance.ResetDailyMissionReward(request.items, null);
				}, null);
			}
			else if (request.HasItems2)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
				{
					SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoDailyMissionCheck();
					SingletonUnity<MissionPassShowRootLogic>.Instance.ResetDailyFinishReward(request.items2, null);
				}, null);
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoDailyMissionCheck();
			}
		}
		return null;
	}
}
