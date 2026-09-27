using System;

// Token: 0x02000A47 RID: 2631
public class ShowRewardManager
{
	// Token: 0x06004CBA RID: 19642 RVA: 0x001A07A4 File Offset: 0x0019E9A4
	public static void ShowRewardItem(ShowRewardItems showItems, string id, REWARD_TYPE type, int level, PROFESSION_TYPE profession, int showIndex = 0)
	{
		UnityVersionUtil.SetActiveRecursive(showItems.gameObject, true);
		if (type == REWARD_TYPE.MISSION)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(id);
			if (missionDataByID.Class == 8)
			{
				TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
				if (timeLimitMissionDataByID == null)
				{
					UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
				}
				else
				{
					ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(timeLimitMissionDataByID.ShowRewardId);
					if (showRewardDataByID != null)
					{
						showItems.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
					}
					else
					{
						UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
					}
				}
			}
			else
			{
				while (missionDataByID.IsMultiMission == 1)
				{
					missionDataByID = DataManager.GetMissionDataByID(missionDataByID.NextID);
				}
				ShowRewardData showRewardDataByID;
				if (profession == PROFESSION_TYPE.XD)
				{
					showRewardDataByID = DataManager.GetShowRewardDataByID(missionDataByID.XDShowID);
				}
				else if (profession == PROFESSION_TYPE.QJ)
				{
					showRewardDataByID = DataManager.GetShowRewardDataByID(missionDataByID.QJShowID);
				}
				else
				{
					showRewardDataByID = DataManager.GetShowRewardDataByID(missionDataByID.NQSShowID);
				}
				if (showRewardDataByID != null)
				{
					showItems.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
				}
			}
		}
		else if (type == REWARD_TYPE.COPY)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(id);
			ShowRewardData showRewardDataByID2 = DataManager.GetShowRewardDataByID(copySceneDataById.ShowRewardId);
			if (showRewardDataByID2 != null)
			{
				showItems.ShowRewards(showRewardDataByID2.ItemIdList, showRewardDataByID2.QualityList, showRewardDataByID2.CountList);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
			}
		}
		else if (type == REWARD_TYPE.DAILY_MISSION)
		{
			DailyMissionData dailyMissionDataById = DataManager.GetDailyMissionDataById(id);
			if (dailyMissionDataById != null)
			{
				AdaptData adaptDataByID = DataManager.GetAdaptDataByID(level);
				ShowRewardData showRewardData = null;
				if (adaptDataByID != null)
				{
					showRewardData = DataManager.GetShowRewardDataByID(adaptDataByID.DorpKeyDic[dailyMissionDataById.ShowRewardIDList[showIndex]]);
				}
				if (showRewardData != null)
				{
					showItems.ShowRewards(showRewardData.ItemIdList, showRewardData.QualityList, showRewardData.CountList);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
				}
			}
			else if (showIndex == 1)
			{
				AdaptData adaptDataByID2 = DataManager.GetAdaptDataByID(level);
				ShowRewardData showRewardDataByID3 = DataManager.GetShowRewardDataByID(adaptDataByID2.DorpKeyDic["_show_rhqh"]);
				if (showRewardDataByID3 != null)
				{
					showItems.ShowRewards(showRewardDataByID3.ItemIdList, showRewardDataByID3.QualityList, showRewardDataByID3.CountList);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
			}
		}
		else if (type == REWARD_TYPE.ESCORT)
		{
			EscortData escortDataById = DataManager.GetEscortDataById(id);
			AdaptData adaptDataByID3 = DataManager.GetAdaptDataByID(level);
			ShowRewardData showRewardDataByID4 = DataManager.GetShowRewardDataByID(adaptDataByID3.DorpKeyDic[escortDataById.ShowRewardId]);
			if (showRewardDataByID4 != null)
			{
				showItems.ShowRewards(showRewardDataByID4.ItemIdList, showRewardDataByID4.QualityList, showRewardDataByID4.CountList);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
			}
		}
		else if (type == REWARD_TYPE.ONLINEMISSION)
		{
			OnlineMissionData onlineMissionDataByID = DataManager.GetOnlineMissionDataByID(id);
			ShowRewardData showRewardDataByID5 = DataManager.GetShowRewardDataByID(onlineMissionDataByID.ShowReward);
			if (showRewardDataByID5 != null)
			{
				showItems.ShowRewards(showRewardDataByID5.ItemIdList, showRewardDataByID5.QualityList, showRewardDataByID5.CountList);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(showItems.gameObject, false);
			}
		}
	}
}
