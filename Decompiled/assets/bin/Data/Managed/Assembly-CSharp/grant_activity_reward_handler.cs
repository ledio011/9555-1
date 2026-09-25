using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class grant_activity_reward_handler
{
	// Token: 0x060012E9 RID: 4841 RVA: 0x0007B580 File Offset: 0x00079780
	public static SprotoTypeBase grant_activity_reward_request(SprotoTypeBase req)
	{
		grant_activity_reward.request request = req as grant_activity_reward.request;
		if (request != null)
		{
			if (request.type == 1L)
			{
				if (request.win)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
					{
						SingletonUnity<MissionPassShowRootLogic>.Instance.ResetNormalMissionReward(request.win, request.items, null);
						SimpleRewardRootLogic.AddRewards(request.items);
					}, null);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", request.type), "success");
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyFailShowRoot, delegate
					{
						SingletonUnity<CopyFailShowRootLogic>.Instance.ResetMission(request.items);
						SimpleRewardRootLogic.AddRewards(request.items);
					}, null);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", request.type), "failure");
				}
				MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
				CurMission escortMission = missionManager.GetEscortMission();
				escortMission.SetMissionState(MISSION_STATE.COMPLETE);
				missionManager.CompleteMissionSuccess(escortMission.MissionId);
			}
			else if (request.type == 2L)
			{
				if (request.win)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
					{
						SingletonUnity<MissionPassShowRootLogic>.Instance.ResetNormalMissionReward(request.win, request.items, null);
						SimpleRewardRootLogic.AddRewards(request.items);
					}, null);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", request.type), "success");
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyFailShowRoot, delegate
					{
						SingletonUnity<CopyFailShowRootLogic>.Instance.ResetMission(request.items);
						SimpleRewardRootLogic.AddRewards(request.items);
					}, null);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", request.type), "failure");
				}
				MissionManager missionManager2 = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
				CurMission curMissionByClassType = missionManager2.GetCurMissionByClassType(MISSION_CLASS_TYPE.ROBBERY);
				curMissionByClassType.SetMissionState(MISSION_STATE.COMPLETE);
				missionManager2.CompleteMissionSuccess(curMissionByClassType.MissionId);
			}
			else if (request.type == 4L || request.type == 5L || request.type == 6L)
			{
				if (request.battle_info.HasDamage_list && request.battle_info.damage_list.Count > 0)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MultiCopyRankResultRoot, delegate
					{
						SingletonUnity<MultiCopyRankResultRootLogic>.Instance.Reset(request.win, request.battle_info, request.items, (int)request.type, request.ID);
						if (request.HasItems)
						{
							SimpleRewardRootLogic.AddRewards(request.items);
						}
					}, null);
					if (request.win)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", request.type), "success");
					}
					else
					{
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", request.type), "failure");
					}
				}
				else
				{
					Debug.Log("don't have damage list!");
				}
			}
			else if (request.type == 3L)
			{
				SimpleRewardRootLogic.AddRewards(request.items);
			}
		}
		return null;
	}
}
