using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class MissionManager
{
	// Token: 0x0600121F RID: 4639 RVA: 0x00073ABC File Offset: 0x00071CBC
	public MissionManager()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.LevelUpMissionCheck));
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x00073AF0 File Offset: 0x00071CF0
	public void SetFunctionMissionIdList(List<string> idList)
	{
		this.mFunctionMissionIdList = idList;
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x00073AFC File Offset: 0x00071CFC
	public void RemoveUnLockFunctionMission(string[] idList)
	{
		if (this.mFunctionMissionIdList != null && this.mFunctionMissionIdList.Count > 0)
		{
			for (int i = idList.Length - 1; i > -1; i--)
			{
				for (int j = this.mFunctionMissionIdList.Count - 1; j > -1; j--)
				{
					if (this.mFunctionMissionIdList[j].Equals(idList[i]))
					{
						this.mFunctionMissionIdList.RemoveAt(j);
					}
				}
			}
		}
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x00073B80 File Offset: 0x00071D80
	public string GetLastMainMissionId()
	{
		return this.mCurMissionDictionary.LastMainMissionId.ToString();
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x00073BA0 File Offset: 0x00071DA0
	public long GetLastMainMissionIdLong()
	{
		return this.mCurMissionDictionary.LastMainMissionId;
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x00073BB0 File Offset: 0x00071DB0
	public Dictionary<string, CurMission> GetCurrentMissionDictionary()
	{
		return this.mCurMissionDictionary.CurMissionDic;
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x00073BC0 File Offset: 0x00071DC0
	public List<CurMission> GetCurMissionList()
	{
		return new List<CurMission>(this.mCurMissionDictionary.CurMissionDic.Values);
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00073BD8 File Offset: 0x00071DD8
	public bool IsCurTargetNpc(string npcId)
	{
		return this.mCurMissionDictionary.CurTargetNpcIdList.Contains(npcId);
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x00073BEC File Offset: 0x00071DEC
	public bool IsCurMissionNeedNpc(ObjNPC objnpc)
	{
		List<CurMission> curMissionList = this.GetCurMissionList();
		for (int i = 0; i < curMissionList.Count; i++)
		{
			if (curMissionList[i].MissionState == MISSION_STATE.ACCEPTED)
			{
				MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[i].MissionId);
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC)
				{
					return true;
				}
				if ((missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC) && objnpc.NPCData.ID.Equals(missionDataByID.Target))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x00073CA8 File Offset: 0x00071EA8
	public bool IsHaveDestroyCarMission()
	{
		List<CurMission> curMissionList = this.GetCurMissionList();
		for (int i = 0; i < curMissionList.Count; i++)
		{
			if (curMissionList[i].MissionState == MISSION_STATE.ACCEPTED)
			{
				MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[i].MissionId);
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.DESTROY_CAR)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x00073D08 File Offset: 0x00071F08
	public bool isCurMissionTypeEnable(MISSION_LOGICTYPE type)
	{
		return true;
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x00073D18 File Offset: 0x00071F18
	public bool IsCurCompleteNpc(string npcId)
	{
		return this.mCurMissionDictionary.CurCompleteNpcIdList.Contains(npcId);
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x00073D2C File Offset: 0x00071F2C
	private void AcceptDailyMissionData(MissionData data)
	{
		if (data.Class == 0 || data.Class == 6)
		{
			this.curDailyMissionData = data;
			this.AutoDailyMissionCheck();
		}
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x00073D60 File Offset: 0x00071F60
	public void AutoDailyMissionCheck()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsOpenAutoCombat && this.curDailyMissionData != null && (this.curDailyMissionData.Class == 0 || this.curDailyMissionData.Class == 6))
		{
			this.SimulationClickMission(this.curDailyMissionData);
		}
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x00073DBC File Offset: 0x00071FBC
	public void AcceptMainMissionCheck(MissionData data)
	{
		if (data.Class == 1)
		{
			this.curMainMissionData = data;
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level >= this.curMainMissionData.MinLv)
			{
				this.AutoMissionCheck(this.curMainMissionData);
			}
		}
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x00073E08 File Offset: 0x00072008
	public void AutoMissionCheck(MissionData curCheckMission)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsOpenAutoCombat && curCheckMission != null && curCheckMission.Class == 1)
		{
			this.SimulationClickMission(curCheckMission);
		}
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x00073E44 File Offset: 0x00072044
	private void SimulationClickMission(MissionData mCurSelectMissionData)
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.SkillLogic.BreakCurSkill();
		}
		this.MissionFindPath(mCurSelectMissionData);
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x00073E7C File Offset: 0x0007207C
	public bool IsMissionAcceptable(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID == null)
		{
			return false;
		}
		if (this.IsMissionFull())
		{
			return false;
		}
		if (this.IsMissionAccepted(missionId))
		{
			return false;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.Level < missionDataByID.MinLv)
		{
			return false;
		}
		if (playerData.Level < missionDataByID.DisplayLv)
		{
			return false;
		}
		if (missionDataByID.Class == 8)
		{
			if (this.mCurMissionDictionary.GetCurMissionByClassType(MISSION_CLASS_TYPE.TIME_LIMIT) != null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(missionDataByID.TimeLimitId))
			{
				return false;
			}
			TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
			if (timeLimitMissionDataByID == null)
			{
				return false;
			}
			if (this.IsMissionCompleted(missionId))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(missionDataByID.PreID) && !this.IsMissionCompleted(missionDataByID.PreID))
			{
				return false;
			}
			if (missionId.Equals(timeLimitMissionDataByID.MissionList[0]))
			{
				if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTimeMissionCanAccept(missionId))
				{
					return false;
				}
				for (int i = 0; i < timeLimitMissionDataByID.MissionList.Count; i++)
				{
					if (this.IsMissionAccepted(timeLimitMissionDataByID.MissionList[i]))
					{
						return false;
					}
				}
			}
			return true;
		}
		else
		{
			if (missionDataByID.Class == 1 && this.mCurMissionDictionary.GetCurMissionByClassType(MISSION_CLASS_TYPE.MAIN) != null)
			{
				return false;
			}
			if (missionDataByID.Repeat == 0 && this.IsMissionCompleted(missionId))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(missionDataByID.PreID) && !this.IsMissionCompleted(missionDataByID.PreID))
			{
				return false;
			}
			if (this.mFunctionMissionIdList.Contains(missionId))
			{
				return false;
			}
			if (missionDataByID.Class == 4 || missionDataByID.Class == 5)
			{
				if (playerData.ActivityData.CurActivityDataDic != null)
				{
					activity_info activityInfoByType;
					if (missionDataByID.Class == 4)
					{
						activityInfoByType = playerData.ActivityData.GetActivityInfoByType(1);
					}
					else
					{
						activityInfoByType = playerData.ActivityData.GetActivityInfoByType(2);
					}
					if (activityInfoByType == null || activityInfoByType.CurNum <= 0L)
					{
						return false;
					}
				}
				if (missionDataByID.Class == 4)
				{
					if (this.mCurMissionDictionary.GetCurMissionByClassType(MISSION_CLASS_TYPE.ROBBERY) != null)
					{
						return false;
					}
				}
				else if (missionDataByID.Class == 5 && this.mCurMissionDictionary.GetCurMissionByClassType(MISSION_CLASS_TYPE.ESCORT) != null)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x000740E0 File Offset: 0x000722E0
	public bool IsMissionFull()
	{
		return this.mCurMissionDictionary.IsMissionFull();
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000740F0 File Offset: 0x000722F0
	public bool IsMissionCompleted(string missionId)
	{
		return this.mCurMissionDictionary.IsMissionCompleted(missionId);
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x00074100 File Offset: 0x00072300
	public bool IsMissionAccepted(string missionId)
	{
		return this.mCurMissionDictionary.IsMissionAccepted(missionId);
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x00074110 File Offset: 0x00072310
	private bool AddMission(string missionId, long serverTime)
	{
		if (this.mCurMissionDictionary.AddMission(missionId, serverTime))
		{
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.AddMission(missionId);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x00074148 File Offset: 0x00072348
	private bool RemoveMission(string missionId)
	{
		if (this.mCurMissionDictionary.RemoveMission(missionId))
		{
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.RemoveMission(missionId);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MissionCheckMoveTarget(missionId);
			return true;
		}
		return false;
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x00074190 File Offset: 0x00072390
	private bool SetMissionComplete(string missionId)
	{
		if (this.mCurMissionDictionary.SetMissionComplete(missionId))
		{
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMission(missionId);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x000741BC File Offset: 0x000723BC
	public void SetMissionParam(string missionId, int paramIndex, long val)
	{
		this.mCurMissionDictionary.SetMissionParam(missionId, paramIndex, val);
		if (SingletonUnity<MissionTeamTipLogic>.Exists)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMission(missionId);
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
		{
			MoveTargetMissionData moveTargetMissionDataById = DataManager.GetMoveTargetMissionDataById(missionDataByID.LogicID);
			if ((long)moveTargetMissionDataById.TargetNum > val && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.UpdateArriveTargetPoint();
			}
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x00074238 File Offset: 0x00072438
	public long GetMissionParam(string missionId, int paramIndex)
	{
		return this.mCurMissionDictionary.GetMissionParam(missionId, paramIndex);
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x00074248 File Offset: 0x00072448
	public bool SetMissionState(string missionId, MISSION_STATE state, long changeTime)
	{
		if (!this.mCurMissionDictionary.SetMissionState(missionId, state, changeTime))
		{
			return false;
		}
		this.UpdateMissionUI(missionId);
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID.ShowStoryState == (int)state)
		{
			StoryDialogRootLogic.ShowStory(missionDataByID.StoryID, DataManager.GetNpcDataByID(missionDataByID.Submit));
		}
		if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.STORY && missionDataByID.IsMultiMission != 1 && state == MISSION_STATE.COMPLETE && missionDataByID.Class != 6)
		{
			Singleton<DialogManager>.Instance.ShowMissionDialogUI(missionDataByID.ID);
		}
		else if (missionDataByID.MissionLogicType != MISSION_LOGICTYPE.ESCORT || state == MISSION_STATE.COMPLETE)
		{
		}
		if (missionDataByID.Class == 8 && state == MISSION_STATE.COMPLETE)
		{
			this.CompleteMission(missionId);
			if (!string.IsNullOrEmpty(missionDataByID.NextID))
			{
				this.AcceptMission(missionDataByID.NextID);
			}
		}
		else if (missionDataByID.IsMultiMission == 1 && state == MISSION_STATE.COMPLETE)
		{
			this.CompleteMission(missionId);
			this.AcceptMission(missionDataByID.NextID);
		}
		if ((missionDataByID.Class == 0 || missionDataByID.Class == 6 || missionDataByID.Class == 7) && state == MISSION_STATE.COMPLETE)
		{
			complete_mission.request request = new complete_mission.request();
			request.missionId = missionId;
			NetLogic.GetInstance().Send<Protocol.complete_mission>(request, null);
		}
		if (state == MISSION_STATE.COMPLETE && (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.IMPACT_NPC))
		{
			MissionManager.ClickMissionAction(missionDataByID.ID);
		}
		if (state == MISSION_STATE.COMPLETE && missionDataByID.Class == 1)
		{
			if (missionDataByID.IsMultiMission != 1 && (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.COLLECTITEM || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC))
			{
				this.AcceptMainMissionCheck(missionDataByID);
			}
			MissionManager.SetFirstSceneMissionTarget(missionId);
		}
		if (state != MISSION_STATE.ACCEPTED && missionDataByID.MissionLogicType != MISSION_LOGICTYPE.DELIVERY)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager != null)
			{
				sceneManager.InitCurAvailableMissionList();
			}
		}
		if (missionDataByID.Class == 2 && state == MISSION_STATE.COMPLETE && string.IsNullOrEmpty(missionDataByID.Submit))
		{
			this.CompleteMission(missionId);
		}
		MissionTipRootLogic.ShowMissionTips(missionId, state);
		if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC && state == MISSION_STATE.COMPLETE)
		{
			SceneManager sceneManager2 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			sceneManager2.UpdateKillTargetMission(sceneManager2.CurrentMapInofData.ID);
		}
		else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR && state == MISSION_STATE.COMPLETE)
		{
			SceneManager sceneManager3 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			sceneManager3.UpdateTargetCarMission(sceneManager3.CurrentMapInofData.ID);
		}
		return true;
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x00074508 File Offset: 0x00072708
	public MISSION_STATE GetMissionState(string missionId)
	{
		return this.mCurMissionDictionary.GetMissionState(missionId);
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00074518 File Offset: 0x00072718
	public long GetMissionChangeTime(string missionId)
	{
		return this.mCurMissionDictionary.GetMissionChangeTime(missionId);
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x00074528 File Offset: 0x00072728
	public void AcceptMission(string missionId)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return;
		}
		if (this.IsMissionAccepted(missionId))
		{
			return;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (GameManager.OnLineState)
		{
			accept_mission.request request = new accept_mission.request();
			request.missionId = missionId;
			NetLogic.GetInstance().Send<Protocol.accept_mission>(request, null);
			return;
		}
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x0007457C File Offset: 0x0007277C
	public void AcceptMission(string missionId, string onlineid)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return;
		}
		if (this.IsMissionAccepted(missionId))
		{
			return;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (GameManager.OnLineState)
		{
			accept_mission.request request = new accept_mission.request();
			request.missionId = missionId;
			if (!string.IsNullOrEmpty(onlineid))
			{
				request.onlineId = onlineid;
			}
			NetLogic.GetInstance().Send<Protocol.accept_mission>(request, null);
			return;
		}
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x000745E0 File Offset: 0x000727E0
	public void AcceptMissionSucess(send_daily_mission.request request)
	{
		if (this.AcceptMissionSuccess(request.mission.missionId, PlayerCommonData.GetServerTime(), false))
		{
			this.SetMissionParam(request.mission.missionId, 0, request.mission.parm[0]);
			this.SetMissionParam(request.mission.missionId, 1, request.mission.parm[1]);
			this.SetMissionParam(request.mission.missionId, 2, request.mission.parm[2]);
			this.SetMissionParam(request.mission.missionId, 3, request.mission.parm[3]);
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMission(request.mission.missionId);
			}
			MissionData missionDataByID = DataManager.GetMissionDataByID(request.mission.missionId);
			this.AcceptDailyMissionData(missionDataByID);
		}
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000746CC File Offset: 0x000728CC
	public bool AcceptMissionSuccess(ownmission mission, long serverTime, bool refresh = true)
	{
		if (this.AcceptMissionSuccess(mission.missionId, serverTime, refresh))
		{
			if (mission.HasParm)
			{
				for (int i = 0; i < mission.parm.Count; i++)
				{
					this.SetMissionParam(mission.missionId, i, mission.parm[i]);
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00074730 File Offset: 0x00072930
	public bool AcceptMissionSuccess(string missionId, long serverTime, bool refresh = true)
	{
		if (!this.AddMission(missionId, serverTime))
		{
			return false;
		}
		if (!this.SetMissionState(missionId, MISSION_STATE.ACCEPTED, serverTime))
		{
			return false;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID != null)
		{
			MissionManager.SetFirstSceneMissionTarget(missionId);
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.DELIVERY)
			{
				if (!this.SetMissionState(missionId, MISSION_STATE.COMPLETE, serverTime))
				{
					return false;
				}
			}
			else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.IMPACT_NPC || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ROB_CAR || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.DESTROY_CAR)
			{
				this.SetMissionParam(missionId, 0, 0L);
			}
			else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
			{
				this.SetMissionParam(missionId, 0, 0L);
			}
			else if (missionDataByID.MissionLogicType != MISSION_LOGICTYPE.STORY)
			{
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
				{
					this.SetMissionParam(missionId, 0, 0L);
					SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
					if (surveyMissionDataById.SceneID.Equals(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr))
					{
						Singleton<SurveyItemManager>.Instance.InitSurveyItem(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr);
					}
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.COLLECTITEM)
				{
					this.SetMissionParam(missionId, 0, 0L);
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP)
				{
					this.SetMissionParam(missionId, 0, 0L);
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ESCORT)
				{
					this.SetMissionParam(missionId, 0, 0L);
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
				{
					this.SetMissionParam(missionId, 0, 0L);
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.OPERATION)
				{
					this.SetMissionParam(missionId, 0, 0L);
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.DOWNLOAD_MISSION)
				{
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload && !this.SetMissionState(missionId, MISSION_STATE.COMPLETE, serverTime))
					{
						return false;
					}
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
				{
					this.SetMissionParam(missionId, 0, 0L);
					SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
					sceneManager.UpdateKillTargetMission(sceneManager.CurrentMapInofData.ID);
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR)
				{
					this.SetMissionParam(missionId, 0, 0L);
					SceneManager sceneManager2 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
					sceneManager2.UpdateTargetCarMission(sceneManager2.CurrentMapInofData.ID);
				}
			}
			if (refresh && SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMission(missionId);
			}
			SceneManager sceneManager3 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager3 != null)
			{
				sceneManager3.MapActivityManager.AcceptMapActivity(missionId);
				sceneManager3.InitCurAvailableMissionList();
			}
			this.AcceptMissionFlurry(missionDataByID);
			return true;
		}
		return false;
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000749F0 File Offset: 0x00072BF0
	private void AcceptMissionFlurry(MissionData tempmissiondata)
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		int num = int.Parse(tempmissiondata.ID);
		num--;
		int num2 = num / 5 * 5 + 1;
		int num3 = num / 5 * 5 + 5;
		if (tempmissiondata.Class == 1)
		{
			num = int.Parse(tempmissiondata.ID);
			if (num > 1000 && num < 2000)
			{
				instance.FlurryLogEventMap("Mission", "MainLineNew", string.Format("accept_{0}", tempmissiondata.ID));
			}
			else if (num <= 37)
			{
				instance.FlurryLogEventMap("Mission", "MainLine37", string.Format("accept_{0}", tempmissiondata.ID));
			}
			else
			{
				num -= 38;
				num2 = num / 5 * 5 + 38;
				num3 = num / 5 * 5 + 42;
				instance.FlurryLogEventMap("Mission", "MainLine", string.Format("accept_{0}_{1}", num2, num3));
			}
		}
		else if (tempmissiondata.Class == 0)
		{
			instance.FlurryLogEventMap("Mission", "DailyLine", string.Format("accept_{0}_{1}", num2, num3));
		}
		else if (tempmissiondata.Class == 2)
		{
			instance.FlurryLogEventMap("Mission", "SideLine", string.Format("accept_{0}_{1}", num2, num3));
		}
		else if (tempmissiondata.Class == 4)
		{
			instance.FlurryLogEventMap("TimeActivity", "activity_1", "accept");
		}
		else if (tempmissiondata.Class == 5)
		{
			instance.FlurryLogEventMap("TimeActivity", "activity_2", "accept");
		}
		else if (tempmissiondata.Class == 7)
		{
			instance.FlurryLogEventMap("Mission", "Online", "accept" + tempmissiondata.ID);
		}
		else if (tempmissiondata.Class == 8)
		{
			instance.FlurryLogEventMap("Mission", "TimeLimit", "accept" + tempmissiondata.ID);
		}
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x00074BFC File Offset: 0x00072DFC
	public bool CompleteMission(string missionId)
	{
		if (!this.IsMissionAccepted(missionId))
		{
			return false;
		}
		if (this.GetMissionState(missionId) != MISSION_STATE.COMPLETE)
		{
			return false;
		}
		if (GameManager.OnLineState)
		{
			complete_mission.request request = new complete_mission.request();
			request.missionId = missionId;
			MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
			if (missionDataByID != null && missionDataByID.Class == 8)
			{
				request.parm = (long)missionDataByID.GetTimeLimitMissionStar();
			}
			NetLogic.GetInstance().Send<Protocol.complete_mission>(request, null);
			return true;
		}
		return false;
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x00074C74 File Offset: 0x00072E74
	public bool CompleteMissionSuccess(string missionId)
	{
		MissionData curMissionData = DataManager.GetMissionDataByID(missionId);
		long lastMissionParam = this.GetMissionParam(missionId, 7);
		if (curMissionData.Class == 1)
		{
			if (!this.SetMissionComplete(missionId))
			{
				return false;
			}
		}
		else if (curMissionData.Class == 2 || curMissionData.Class == 8)
		{
			this.SetMissionComplete(missionId);
		}
		if (curMissionData.Class == 7)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
			{
				OnlineMissionData onlineMissionDataByID = DataManager.GetOnlineMissionDataByID(this.GetMissionParam(missionId, 1).ToString());
				if (onlineMissionDataByID != null)
				{
					SingletonUnity<MissionPassShowRootLogic>.Instance.ResetSideMissionReward(onlineMissionDataByID.ShowReward);
				}
			}, null);
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.IsHaveDailyCopyTipsBytype(MAPTYPE.SCUFFLE_AREA_1))
			{
				SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SCUFFLE_AREA_1, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
		}
		this.RemoveMission(missionId);
		this.UpdateMissionUI(missionId);
		if (curMissionData.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			sceneManager.UpdateKillTargetMission(sceneManager.CurrentMapInofData.ID);
		}
		else if (curMissionData.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR)
		{
			SceneManager sceneManager2 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			sceneManager2.UpdateTargetCarMission(sceneManager2.CurrentMapInofData.ID);
		}
		if (curMissionData != null)
		{
			if (curMissionData.Class == 1 || curMissionData.Class == 2)
			{
				if (string.IsNullOrEmpty(curMissionData.Submit) && curMissionData.IsMultiMission == 0)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
					{
						string text = string.Empty;
						PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
						if (playerData.Profession == PROFESSION_TYPE.XD)
						{
							text = curMissionData.XDShowID;
						}
						else if (playerData.Profession == PROFESSION_TYPE.QJ)
						{
							text = curMissionData.QJShowID;
						}
						else if (playerData.Profession == PROFESSION_TYPE.NQS)
						{
							text = curMissionData.NQSShowID;
						}
						if (DataManager.GetShowRewardDataByID(text) == null)
						{
							SingletonUnity<MissionPassShowRootLogic>.Instance.Reset(missionId);
						}
						else
						{
							SingletonUnity<MissionPassShowRootLogic>.Instance.ResetSideMissionReward(text);
						}
					}, null);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
					{
						SingletonUnity<MissionPassShowRootLogic>.Instance.Reset(missionId);
					}, null);
				}
				if (!string.IsNullOrEmpty(curMissionData.NextID))
				{
					if (curMissionData.IsMultiMission == 0)
					{
						MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionData.NextID);
						if (missionDataByID != null && !UIManager.IsUnlockTutorialEnable())
						{
							Singleton<DialogManager>.Instance.ShowMissionDialogUI(curMissionData.NextID);
						}
					}
					this.AcceptMission(curMissionData.NextID);
				}
				else
				{
					SceneManager sceneManager3 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
					if (sceneManager3 != null)
					{
						sceneManager3.InitCurAvailableMissionList();
					}
				}
				if (!string.IsNullOrEmpty(curMissionData.NextSideID))
				{
					for (int i = 0; i < curMissionData.NextSideIDList.Length; i++)
					{
						this.AcceptMission(curMissionData.NextSideIDList[i]);
					}
				}
			}
			else if (curMissionData.Class == 8)
			{
				TimeLimitMissionData tlData = DataManager.GetTimeLimitMissionDataByID(curMissionData.TimeLimitId);
				if (tlData != null)
				{
					if (missionId.Equals(tlData.MissionList[tlData.MissionList.Count - 1]))
					{
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
						{
							SingletonUnity<MissionPassShowRootLogic>.Instance.ResetTimeLimitMissionPassRoot(tlData, tlData.LimitTime - (PlayerCommonData.GetServerTime() - lastMissionParam));
						}, null);
					}
					else
					{
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionPassShowRoot, delegate
						{
							SingletonUnity<MissionPassShowRootLogic>.Instance.Reset(missionId);
						}, null);
					}
				}
				SceneManager sceneManager4 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
				if (sceneManager4 != null)
				{
					sceneManager4.InitCurAvailableMissionList();
				}
			}
			else if (curMissionData.Class == 4)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.DisableEscortBtn();
				if (Singleton<ObjManager>.Instance.MainPlayer != null)
				{
					Singleton<ObjManager>.Instance.MainPlayer.LeaveTeamFollow();
				}
			}
			else if (curMissionData.Class == 6)
			{
				SceneManager sceneManager5 = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
				if (sceneManager5 != null)
				{
					sceneManager5.InitCurAvailableMissionList();
				}
			}
			this.MissionCompleteFlurry(curMissionData);
		}
		this.curDailyMissionData = null;
		return true;
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000750F4 File Offset: 0x000732F4
	private void MissionCompleteFlurry(MissionData tempmissiondata)
	{
		int num = int.Parse(tempmissiondata.ID);
		num--;
		int num2 = num / 5 * 5 + 1;
		int num3 = num / 5 * 5 + 5;
		if (tempmissiondata.Class == 1)
		{
			if (num > 1000 && num < 2000)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "MainLineNew", string.Format("complete_{0}", tempmissiondata.ID));
			}
			else if (int.Parse(tempmissiondata.ID) <= 37)
			{
				if (num == 0)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "MainLine37", string.Format("accept_{0}", tempmissiondata.ID));
				}
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "MainLine37", string.Format("complete_{0}", tempmissiondata.ID));
			}
			else
			{
				num = int.Parse(tempmissiondata.ID);
				num -= 38;
				num2 = num / 5 * 5 + 38;
				num3 = num / 5 * 5 + 42;
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "MainLine", string.Format("complete_{0}_{1}", num2, num3));
			}
		}
		else if (tempmissiondata.Class == 0)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "DailyLine", string.Format("complete_{0}_{1}", num2, num3));
		}
		else if (tempmissiondata.Class == 2)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "SideLine", string.Format("complete_{0}_{1}", num2, num3));
		}
		else if (tempmissiondata.Class == 4)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_1", "finish");
		}
		else if (tempmissiondata.Class == 5)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_2", "finish");
		}
		else if (tempmissiondata.Class == 7)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "Online", "complete" + tempmissiondata.ID);
		}
		else if (tempmissiondata.Class == 8)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "TimeLimit", "complete" + tempmissiondata.ID);
		}
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x00075354 File Offset: 0x00073554
	public bool AbandonMission(string missionId, bool isForced = false)
	{
		if (!this.IsMissionAccepted(missionId))
		{
			return false;
		}
		abandon_mission.request request = new abandon_mission.request();
		request.missionId = missionId;
		if (isForced)
		{
			request.parm = 1L;
		}
		NetLogic.GetInstance().Send<Protocol.abandon_mission>(request, null);
		Debug.Log("AbandonMission :: " + missionId);
		return true;
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x000753A8 File Offset: 0x000735A8
	public bool AbandonMissionSuccess(string missionId)
	{
		if (!this.RemoveMission(missionId))
		{
			return false;
		}
		this.UpdateMissionUI(missionId);
		this.MissionAbandonMissionFlurry(missionId);
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager != null)
		{
			sceneManager.InitCurAvailableMissionList();
		}
		return true;
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000753EC File Offset: 0x000735EC
	private void MissionAbandonMissionFlurry(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		int num = int.Parse(missionDataByID.ID);
		num--;
		int num2 = num / 5 * 5 + 1;
		int num3 = num / 5 * 5 + 5;
		if (missionDataByID.Class != 1)
		{
			if (missionDataByID.Class == 0)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "DailyLine", string.Format("abandon_{0}_{1}", num2, num3));
			}
			else if (missionDataByID.Class != 2)
			{
				if (missionDataByID.Class == 4)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_1", "abandon");
				}
				else if (missionDataByID.Class == 5)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_2", "abandon");
				}
				else if (missionDataByID.Class == 7)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "Online", "abandon" + missionDataByID.ID);
				}
				else if (missionDataByID.Class == 8)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Mission", "TimeLimit", "abandon" + missionDataByID.ID);
				}
			}
		}
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x00075538 File Offset: 0x00073738
	public List<string> GetAllMissionId()
	{
		return new List<string>(this.mCurMissionDictionary.CurMissionDic.Keys);
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00075550 File Offset: 0x00073750
	public void SyncMissionList(sync_mission.request request)
	{
		this.mCurMissionDictionary.Reset();
		List<ownmission> list = new List<ownmission>(request.missions.Values);
		bool flag = false;
		string text = string.Empty;
		for (int i = 0; i < list.Count; i++)
		{
			string missionId = list[i].missionId;
			MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
			if (missionDataByID != null)
			{
				if (this.AddMission(missionId, list[i].parm[7]))
				{
					this.mCurMissionDictionary.SetMissionState(missionId, (MISSION_STATE)list[i].missionstate, list[i].parm[7]);
					List<long> parm = list[i].parm;
					for (int j = 0; j < parm.Count; j++)
					{
						this.SetMissionParam(missionId, j, parm[j]);
					}
				}
				if (missionDataByID.Class == 4)
				{
					ObjManager instance = Singleton<ObjManager>.Instance;
					ObjCharacter objCharacter = instance.FindObjInScene(this.GetMissionParam(missionId, 1));
					if (objCharacter != null)
					{
						instance.RemoveFromTargetCampList(objCharacter);
						objCharacter.AttributeData.Camp = GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC;
					}
				}
				if (missionDataByID.Class == 1)
				{
					flag = true;
					text = missionDataByID.ID;
				}
			}
		}
		if (request.HasLast_missionId)
		{
			this.mCurMissionDictionary.LastMainMissionId = (long)int.Parse(request.last_missionId);
		}
		if (!flag && request.HasLast_missionId)
		{
			MissionData missionDataByID2 = DataManager.GetMissionDataByID(request.last_missionId);
			if (missionDataByID2 != null)
			{
				this.AcceptMission(missionDataByID2.NextID);
			}
		}
		if (request.HasSidedone_mission)
		{
			this.mCurMissionDictionary.MissionCompleteFlag = request.sidedone_mission;
		}
		Singleton<SurveyItemManager>.Instance.InitSurveyItem(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr);
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		for (int k = 1; k <= level; k++)
		{
			List<string> lvAutoAcceptMissionListByLevel = DataManager.GetLvAutoAcceptMissionListByLevel(k);
			if (lvAutoAcceptMissionListByLevel != null)
			{
				for (int l = 0; l < lvAutoAcceptMissionListByLevel.Count; l++)
				{
					if (this.IsMissionAcceptable(lvAutoAcceptMissionListByLevel[l]))
					{
						this.AcceptMission(lvAutoAcceptMissionListByLevel[l]);
					}
				}
			}
		}
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x000757A4 File Offset: 0x000739A4
	public void StopAutoMoveToMission()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath.Finish();
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x000757B8 File Offset: 0x000739B8
	public void ContinueAutoMoveToMission()
	{
		AutoSearchPathManager autoSearchPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		if (autoSearchPath.CurPath.PathPointList.Count > 0 && SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr.Equals(autoSearchPath.CurPath.PathPointList[0].SceneId))
		{
			Vector3 pos;
			pos..ctor(autoSearchPath.CurPath.PathPointList[0].PosX, SceneManager.GetHitHeight(autoSearchPath.CurPath.PathPointList[0].PosX, autoSearchPath.CurPath.PathPointList[0].PosZ), autoSearchPath.CurPath.PathPointList[0].PosZ);
			Singleton<ObjManager>.Instance.MainPlayer.MoveTo(pos, Singleton<ObjManager>.Instance.MainPlayer.GetStopDistance(), new ObjCharacter.TargetArriveFinsh(this.MoveToNextPoint));
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.SetMoveTarget(pos, autoSearchPath.CurMissionId);
		}
		else if (autoSearchPath.CurPath.PathPointList.Count > 1 && autoSearchPath.CurPath.PathPointList[0].SceneId.Equals(LoadingWindow.preSceneId) && autoSearchPath.CurPath.PathPointList[1].SceneId.Equals(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr))
		{
			this.MoveToNextPoint(null);
		}
		else
		{
			this.StopAutoMoveToMission();
		}
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x00075934 File Offset: 0x00073B34
	public void MissionFindPath(string missionId)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		this.MissionFindPath(missionDataByID);
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x0007595C File Offset: 0x00073B5C
	public void MissionFindPath(MissionData missionData)
	{
		if (missionData == null)
		{
			return;
		}
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (Singleton<ObjManager>.Instance.MainPlayer && instance.SceneManager.IsCopyScene() && !instance.SceneManager.IsTutorialScene())
		{
			return;
		}
		Vector3 vector = default(Vector3);
		string text = string.Empty;
		string text2 = string.Empty;
		MISSION_STATE missionState = this.GetMissionState(missionData.ID);
		if (missionState == MISSION_STATE.INVALID || missionState == MISSION_STATE.FAIL)
		{
			if (missionData.Class == 0)
			{
				return;
			}
			text = missionData.AcceptMapId;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Vector3 npcposInMonsterData = DataManager.GetNPCPosInMonsterData(text, missionData.Accept);
			vector.x = npcposInMonsterData.x;
			vector.z = npcposInMonsterData.z;
			text2 = missionData.Accept;
		}
		else if (missionState == MISSION_STATE.ACCEPTED)
		{
			if (missionData.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
			{
				SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionData.LogicID);
				text = surveyMissionDataById.SceneID;
				vector.x = surveyMissionDataById.PosX;
				vector.z = surveyMissionDataById.PosZ;
				vector.y = SceneManager.GetHitHeight(vector.x, vector.z);
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData = this.GetCurMultiDeliveryTargetData(missionData.ID);
				if (curMultiDeliveryTargetData != null)
				{
					text = curMultiDeliveryTargetData.TargetMapId;
					if (string.IsNullOrEmpty(text))
					{
						return;
					}
					Vector3 npcposInMonsterData2 = DataManager.GetNPCPosInMonsterData(text, curMultiDeliveryTargetData.TargetNpcId);
					vector.x = npcposInMonsterData2.x;
					vector.z = npcposInMonsterData2.z;
					vector.y = SceneManager.GetHitHeight(npcposInMonsterData2);
					text2 = curMultiDeliveryTargetData.TargetNpcId;
				}
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
			{
				MoveTargetMissionData moveTargetMissionDataById = DataManager.GetMoveTargetMissionDataById(missionData.LogicID);
				if (moveTargetMissionDataById == null)
				{
					return;
				}
				text = moveTargetMissionDataById.MapId;
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				int num = (int)this.GetMissionParam(missionData.ID, 0);
				if (num > moveTargetMissionDataById.TargetPointList.Count)
				{
					num = moveTargetMissionDataById.TargetPointList.Count - 1;
				}
				Vector3 pos = moveTargetMissionDataById.TargetPointList[num];
				vector.x = pos.x;
				vector.y = SceneManager.GetHitHeight(pos);
				vector.z = pos.z;
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
			{
				KillTargetMissionData killTargetMissionDataById = DataManager.GetKillTargetMissionDataById(missionData.LogicID);
				text = killTargetMissionDataById.SceneID;
				vector = killTargetMissionDataById.Pos;
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR)
			{
				TargetCarMissionData targetCarMissionDataById = DataManager.GetTargetCarMissionDataById(missionData.LogicID);
				text = targetCarMissionDataById.SceneID;
				vector = targetCarMissionDataById.Pos;
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
			{
				if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
				{
					SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
					return;
				}
				DominData dominDataByID = DataManager.GetDominDataByID(missionData.LogicID);
				if (dominDataByID.IsOpen == 0)
				{
					NoticeLogic.AddNotifyData("#{103019}", true, false);
					return;
				}
				if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(dominDataByID.LevelMin))
				{
					NoticeLogic.AddNotifyData("#{103009}", true, false);
					return;
				}
				text = dominDataByID.AcceptMapID;
				vector = dominDataByID.GetPos();
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.ROB_CAR)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType != MAPTYPE.TUTORIAL_CAR)
				{
					text = "11";
					vector..ctor(281f, 0f, -100f);
				}
				else
				{
					if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.ROB_CAR_TIP))
					{
						return;
					}
					NewTutorialSceneManager newTutorialSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager;
					if (newTutorialSceneManager == null)
					{
						return;
					}
					if (!(newTutorialSceneManager.mTutorialCar != null))
					{
						return;
					}
					text = "11";
					vector = newTutorialSceneManager.mTutorialCar.PlayerCar.DummyPlayerPoint.position;
				}
			}
			else if (missionData.Class == 4)
			{
				if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickEscortFollowBtn();
					return;
				}
			}
			else if (missionData.Class == 5)
			{
				MissionManager.ClickMissionAction(missionData.ID);
			}
			else
			{
				text = missionData.TargetMapId;
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				if (string.IsNullOrEmpty(missionData.Target))
				{
					if (!SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr.Equals(text))
					{
						MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(text);
						if (mapInfoDataByID != null)
						{
							this.AutoMoveDest(text, mapInfoDataByID.BirthPosVector3, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP, null);
						}
					}
					return;
				}
				Vector3 npcposInMonsterData3 = DataManager.GetNPCPosInMonsterData(text, missionData.Target);
				vector.x = npcposInMonsterData3.x;
				vector.z = npcposInMonsterData3.z;
				vector.y = SceneManager.GetHitHeight(npcposInMonsterData3);
				text2 = missionData.Target;
			}
		}
		else if (missionState == MISSION_STATE.COMPLETE)
		{
			text = missionData.SubmitMapId;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Vector3 npcposInMonsterData4 = DataManager.GetNPCPosInMonsterData(text, missionData.Submit);
			vector.x = npcposInMonsterData4.x;
			vector.z = npcposInMonsterData4.z;
			vector.y = SceneManager.GetHitHeight(npcposInMonsterData4);
			text2 = missionData.Submit;
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		AutoSearchPathPoint targetPoint = new AutoSearchPathPoint(text, vector.x, vector.y, vector.z);
		if (instance != null && instance.AutoSearchPath != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.BreakAutoCombatState();
			Singleton<ObjManager>.Instance.MainPlayer.SkillLogic.BreakCurSkill();
			instance.AutoSearchPath.FindPath(targetPoint, AUTO_SEARCH_PARTH_FINISHEVENT.MISSION, missionData.ID);
		}
		AutoSearchPath curPath = instance.AutoSearchPath.CurPath;
		if (curPath != null && curPath.PathPointList.Count > 0)
		{
			Vector3 vector2;
			vector2..ctor(curPath.PathPointList[0].PosX, SceneManager.GetHitHeight(curPath.PathPointList[0].PosX, curPath.PathPointList[0].PosZ), curPath.PathPointList[0].PosZ);
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (mainPlayer.IsLocalDrivingCar)
			{
				if (Vector3.Distance(vector2, mainPlayer.Position) < 10f)
				{
					this.MoveToNextPoint(mainPlayer);
				}
			}
			else
			{
				mainPlayer.MoveTo(vector2, mainPlayer.GetStopDistance(), new ObjCharacter.TargetArriveFinsh(this.MoveToNextPoint));
			}
			instance.SceneManager.SetMoveTarget(vector2, missionData.ID);
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00076030 File Offset: 0x00074230
	public void MoveToNextPoint(ObjCharacter objCha)
	{
		AutoSearchPathManager autoSearchPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		if (autoSearchPath.CurPath.PathPointList.Count > 0)
		{
			autoSearchPath.CurPath.PathPointList.RemoveAt(0);
		}
		if (autoSearchPath.CurPath.PathPointList.Count == 0)
		{
			this.FinishPathFindEvent();
			autoSearchPath.Finish();
		}
		else
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr.Equals(autoSearchPath.CurPath.PathPointList[0].SceneId) && mainPlayer != null)
			{
				Vector3 pos;
				pos..ctor(autoSearchPath.CurPath.PathPointList[0].PosX, SceneManager.GetHitHeight(autoSearchPath.CurPath.PathPointList[0].PosX, autoSearchPath.CurPath.PathPointList[0].PosZ), autoSearchPath.CurPath.PathPointList[0].PosZ);
				mainPlayer.MoveTo(pos, Singleton<ObjManager>.Instance.MainPlayer.GetStopDistance(), new ObjCharacter.TargetArriveFinsh(this.MoveToNextPoint));
				SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.SetMoveTarget(pos, autoSearchPath.CurMissionId);
			}
		}
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x00076174 File Offset: 0x00074374
	public void AutoMoveDest(string mapId, Vector3 pos, AUTO_SEARCH_PARTH_FINISHEVENT Type, string param = null)
	{
		AutoSearchPathPoint targetPoint = new AutoSearchPathPoint(mapId, pos.x, pos.y, pos.z);
		AutoSearchPathManager autoSearchPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		if (autoSearchPath != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.BreakAutoCombatState();
			Singleton<ObjManager>.Instance.MainPlayer.SkillLogic.BreakCurSkill();
			SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath.FindPath(targetPoint, Type, param);
			AutoSearchPath curPath = autoSearchPath.CurPath;
			if (curPath != null && curPath.PathPointList.Count > 0)
			{
				Vector3 pos2;
				pos2..ctor(curPath.PathPointList[0].PosX, SceneManager.GetHitHeight(curPath.PathPointList[0].PosX, curPath.PathPointList[0].PosZ), curPath.PathPointList[0].PosZ);
				Singleton<ObjManager>.Instance.MainPlayer.MoveTo(pos2, Singleton<ObjManager>.Instance.MainPlayer.GetStopDistance(), new ObjCharacter.TargetArriveFinsh(this.MoveToNextPoint));
				SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.SetMoveTarget(pos2, autoSearchPath.CurMissionId);
			}
		}
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x00076294 File Offset: 0x00074494
	public void FinishPathFindEvent()
	{
		AutoSearchPathManager autoSearchPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (autoSearchPath.FinishEventType == AUTO_SEARCH_PARTH_FINISHEVENT.MISSION)
		{
			string curMissionId = autoSearchPath.CurMissionId;
			if (string.IsNullOrEmpty(curMissionId))
			{
				return;
			}
			MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionId);
			MISSION_STATE missionState = this.GetMissionState(curMissionId);
			if (missionDataByID.Class == 4 && missionState == MISSION_STATE.ACCEPTED && SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickEscortFollowBtn();
				return;
			}
			string text = string.Empty;
			if (missionState == MISSION_STATE.INVALID || missionState == MISSION_STATE.FAIL)
			{
				text = missionDataByID.Accept;
			}
			else if (missionState == MISSION_STATE.ACCEPTED)
			{
				text = missionDataByID.Target;
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
				{
					MultiDeliveryMissionData curMultiDeliveryTargetData = this.GetCurMultiDeliveryTargetData(curMissionId);
					text = curMultiDeliveryTargetData.TargetNpcId;
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
				{
					ObjZombieRagdollPlayer objZombieRagdollPlayer = this.FindDominNPC(text);
					if (objZombieRagdollPlayer != null && objZombieRagdollPlayer.CheckInDialogRange())
					{
						objZombieRagdollPlayer.ShowAcitvityDialog();
					}
					return;
				}
			}
			else if (missionState == MISSION_STATE.COMPLETE)
			{
				text = missionDataByID.Submit;
			}
			if (!string.IsNullOrEmpty(text))
			{
				ObjNPC objNPC = this.FindMissionNPC(text);
				if (objNPC != null && objNPC.CheckInDialogRange())
				{
					Singleton<DialogManager>.Instance.ShowDialog(objNPC, curMissionId);
				}
				if (objNPC == null && missionState != MISSION_STATE.COMPLETE && (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC))
				{
					SingletonUnity<JueseJiNengQuLogic>.Instance.UseSkill_1_Onclick();
					if (mainPlayer.IsOpenAutoCombat)
					{
						mainPlayer.ReturnAutoCombatState();
					}
					else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.AUTO_FIGHT))
					{
						mainPlayer.EnterAutoCombat();
						SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateAutoBtn();
					}
				}
			}
			else if (missionDataByID.Class == 0)
			{
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY && missionState == MISSION_STATE.ACCEPTED)
				{
					SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
					GameObject gameObject = Singleton<ObjManager>.Instance.FindOtherObjInDic(surveyMissionDataById.GetSurveyItemName());
					if (gameObject != null && Vector3.Distance(gameObject.transform.position, mainPlayer.Position) < 3f)
					{
						SurveyItemObj component = gameObject.GetComponent<SurveyItemObj>();
						if (component != null)
						{
							Singleton<SurveyItemManager>.Instance.StartSurveyItem(component);
						}
						else
						{
							Debug.Log("SurveyItem == null");
						}
					}
				}
			}
			else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY && missionState == MISSION_STATE.ACCEPTED)
			{
				SurveyMissionData surveyMissionDataById2 = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
				GameObject gameObject2 = Singleton<ObjManager>.Instance.FindOtherObjInDic(surveyMissionDataById2.GetSurveyItemName());
				float num = 3.1f;
				if (mainPlayer.IsLocalDrivingCar)
				{
					num = 8f;
				}
				if (gameObject2 != null && Vector3.Distance(gameObject2.transform.position, mainPlayer.Position) < num)
				{
					SurveyItemObj surveyItemObj = gameObject2.GetComponent<SurveyItemObj>();
					if (surveyItemObj != null)
					{
						if (mainPlayer.IsLocalDrivingCar)
						{
							if (SingletonUnity<CitySimController>.Exists)
							{
								SingletonUnity<CitySimController>.Instance.RobCar(delegate
								{
									mainPlayer.MoveTo(surveyItemObj.transform.position, 1f, delegate(ObjCharacter A_1)
									{
										mainPlayer.FaceToPub(surveyItemObj.transform.position);
										Singleton<SurveyItemManager>.Instance.StartSurveyItem(surveyItemObj);
									});
								});
							}
						}
						else
						{
							Singleton<SurveyItemManager>.Instance.StartSurveyItem(surveyItemObj);
						}
					}
					else
					{
						Debug.Log("SurveyItem == null");
					}
				}
			}
			else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
			{
				SingletonUnity<JueseJiNengQuLogic>.Instance.UseSkill_1_Onclick();
				if (mainPlayer.IsOpenAutoCombat)
				{
					mainPlayer.ReturnAutoCombatState();
				}
				else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.AUTO_FIGHT))
				{
					mainPlayer.EnterAutoCombat();
					SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateAutoBtn();
				}
			}
		}
		else if (autoSearchPath.FinishEventType == AUTO_SEARCH_PARTH_FINISHEVENT.CITY_DANCE)
		{
			if (SingletonUnity<DanceBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DanceBtnRootLogic>.Instance.DanceBtnScale.gameObject))
			{
				SingletonUnity<DanceBtnRootLogic>.Instance.OnClickDanceBtn();
			}
		}
		else if (autoSearchPath.FinishEventType == AUTO_SEARCH_PARTH_FINISHEVENT.FIND_NPC)
		{
			ObjNPC objNPC2 = this.FindMissionNPC(autoSearchPath.CurMissionId);
			if (objNPC2 != null && objNPC2.CheckInDialogRange())
			{
				Singleton<DialogManager>.Instance.ShowDialog(objNPC2, string.Empty);
			}
		}
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x00076794 File Offset: 0x00074994
	private ObjNPC FindMissionNPC(string npcId)
	{
		List<Obj> list = new List<Obj>(Singleton<ObjManager>.Instance.ObjDict.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].ObjType == GameDefine.OBJ_TYPE.OBJ_NPC && (list[i] as ObjNPC).IsMissionNpc())
			{
				ObjNPC objNPC = list[i] as ObjNPC;
				if (objNPC.NPCDataID.Equals(npcId))
				{
					return objNPC;
				}
			}
		}
		return null;
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x00076818 File Offset: 0x00074A18
	private ObjZombieRagdollPlayer FindDominNPC(string npcId)
	{
		List<Obj> list = new List<Obj>(Singleton<ObjManager>.Instance.ObjDict.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL && (list[i] as ObjZombieRagdollPlayer).IsMissionNpc)
			{
				ObjZombieRagdollPlayer objZombieRagdollPlayer = list[i] as ObjZombieRagdollPlayer;
				if (objZombieRagdollPlayer.NpcId.Equals(npcId))
				{
					return objZombieRagdollPlayer;
				}
			}
		}
		return null;
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x0007689C File Offset: 0x00074A9C
	public void UpdateMissionUI(string missionId)
	{
		if (SingletonUnity<MissionTeamTipLogic>.Exists)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMission(missionId);
		}
		if (SingletonUnity<MissionPageRootLogic>.Exists)
		{
			SingletonUnity<MissionPageRootLogic>.Instance.Reset(true);
		}
		if (SingletonUnity<NewMissionUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMissionUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMissionUIRootLogic>.Instance.Reset(string.Empty);
		}
		if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.InitMissionActivityPic();
			SingletonUnity<NewMapUIRootLogic>.Instance.InitMapActivityPic();
		}
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00076934 File Offset: 0x00074B34
	public void PrintMissionList()
	{
		List<CurMission> list = new List<CurMission>(this.mCurMissionDictionary.CurMissionDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			Debug.Log("==================");
			this.PrintMission(list[i].MissionId);
		}
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x0007698C File Offset: 0x00074B8C
	public void PrintMission(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		Debug.Log("Mission Id : " + missionId);
		Debug.Log("Param 0 : " + this.mCurMissionDictionary.GetMissionParam(missionId, 0));
		Debug.Log("Mission State : " + this.mCurMissionDictionary.GetMissionState(missionId));
		Debug.Log("MissionFinished : " + this.IsMissionCompleted(missionId));
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x00076A0C File Offset: 0x00074C0C
	public void PickMissionItem(string ItemId)
	{
		List<CurMission> list = new List<CurMission>(this.mCurMissionDictionary.CurMissionDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(list[i].MissionId);
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.COLLECTITEM || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP)
			{
				MissionRequireData missionRequireDataByID = DataManager.GetMissionRequireDataByID(missionDataByID.LogicID);
				if (missionRequireDataByID.RequireItemID.Equals(ItemId))
				{
					this.SetMissionParam(missionDataByID.ID, 0, this.GetMissionParam(missionDataByID.ID, 0) + 1L);
					if (list[i].GetParam(0) >= (long)missionRequireDataByID.RequireNum)
					{
						list[i].SetMissionState(MISSION_STATE.COMPLETE);
					}
					SingletonUnity<MissionTeamTipLogic>.Instance.UpdateMission(list[i].MissionId);
					return;
				}
			}
		}
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x00076AF8 File Offset: 0x00074CF8
	public bool IsHaveEscortMission()
	{
		List<CurMission> list = new List<CurMission>(this.mCurMissionDictionary.CurMissionDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(list[i].MissionId);
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ESCORT)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00076B54 File Offset: 0x00074D54
	public MultiDeliveryMissionData GetCurMultiDeliveryTargetData(string missionId)
	{
		if (this.IsMissionAccepted(missionId))
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				List<MultiDeliveryMissionData> multiDeliveryMissionDataListById = DataManager.GetMultiDeliveryMissionDataListById(missionDataByID.LogicID);
				int num = (int)this.GetMissionParam(missionId, 0);
				if (multiDeliveryMissionDataListById.Count >= num)
				{
					return multiDeliveryMissionDataListById[num];
				}
			}
		}
		return null;
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x00076BAC File Offset: 0x00074DAC
	public static void GetMissionStateLabel(MissionData mCurMissionData, MISSION_STATE curMissionState, UILabel MissionStateLabel)
	{
		if (mCurMissionData == null)
		{
			return;
		}
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (curMissionState == MISSION_STATE.ACCEPTED)
		{
			MissionStateLabel.color = Color.white;
			if (playerData.Level < mCurMissionData.MinLv)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100320}", new object[]
				{
					mCurMissionData.MinLv
				});
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP)
			{
				NpcData npcDataByID = DataManager.GetNpcDataByID(mCurMissionData.Target);
				MissionRequireData missionRequireDataByID = DataManager.GetMissionRequireDataByID(mCurMissionData.LogicID);
				if (npcDataByID != null && missionRequireDataByID != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100315}", new object[]
					{
						npcDataByID.MName
					}), missionManager.GetMissionParam(mCurMissionData.ID, 0), missionRequireDataByID.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
			{
				KillTargetMissionData killTargetMissionDataById = DataManager.GetKillTargetMissionDataById(mCurMissionData.LogicID);
				NpcData npcDataByID2 = DataManager.GetNpcDataByID(killTargetMissionDataById.NpcID);
				if (npcDataByID2 != null && killTargetMissionDataById != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100315}", new object[]
					{
						npcDataByID2.MName
					}), missionManager.GetMissionParam(mCurMissionData.ID, 0), killTargetMissionDataById.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR)
			{
				TargetCarMissionData targetCarMissionDataById = DataManager.GetTargetCarMissionDataById(mCurMissionData.LogicID);
				if (targetCarMissionDataById != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100327}", new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), targetCarMissionDataById.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.IMPACT_NPC)
			{
				MissionRequireData missionRequireDataByID2 = DataManager.GetMissionRequireDataByID(mCurMissionData.LogicID);
				if (missionRequireDataByID2 != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100328}", new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), missionRequireDataByID2.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.DESTROY_CAR)
			{
				MissionRequireData missionRequireDataByID3 = DataManager.GetMissionRequireDataByID(mCurMissionData.LogicID);
				if (missionRequireDataByID3 != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100329}", new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), missionRequireDataByID3.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.ROB_CAR)
			{
				MissionRequireData missionRequireDataByID4 = DataManager.GetMissionRequireDataByID(mCurMissionData.LogicID);
				if (missionRequireDataByID4 != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100327}", new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), missionRequireDataByID4.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC)
			{
				MissionRequireData missionRequireDataByID5 = DataManager.GetMissionRequireDataByID(mCurMissionData.LogicID);
				if (missionRequireDataByID5 != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100325}", new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), missionRequireDataByID5.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
			{
				MoveTargetMissionData moveTargetMissionDataById = DataManager.GetMoveTargetMissionDataById(mCurMissionData.LogicID);
				if (moveTargetMissionDataById != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100330}", new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), moveTargetMissionDataById.TargetNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
			{
				SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(mCurMissionData.LogicID);
				if (surveyMissionDataById != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]), missionManager.GetMissionParam(mCurMissionData.ID, 0), surveyMissionDataById.NeedNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.COLLECTITEM || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP)
			{
				NpcData npcDataByID3 = DataManager.GetNpcDataByID(mCurMissionData.Target);
				MissionRequireData missionRequireDataByID6 = DataManager.GetMissionRequireDataByID(mCurMissionData.LogicID);
				if (npcDataByID3 != null && missionRequireDataByID6 != null)
				{
					MissionStateLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{100317}", new object[]
					{
						npcDataByID3.MName
					}), missionManager.GetMissionParam(mCurMissionData.ID, 0), missionRequireDataByID6.RequireNum);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.COPYSCENE_KILLMONSTER)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.Class == 4)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.Class == 5)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMultiDeliveryTargetData(mCurMissionData.ID);
				if (curMultiDeliveryTargetData != null)
				{
					NpcData npcDataByID4 = DataManager.GetNpcDataByID(curMultiDeliveryTargetData.TargetNpcId);
					if (npcDataByID4 != null)
					{
						MissionStateLabel.text = string.Format("{0} 0/1", npcDataByID4.MName);
					}
				}
				else
				{
					Debug.Log("MultiDeliveryMissionData ERROR!!!!!!!!!!!!!!!!!!!!!");
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.STORY)
			{
				NpcData npcDataByID5 = DataManager.GetNpcDataByID(mCurMissionData.Target);
				if (npcDataByID5 != null)
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100314}", new object[]
					{
						npcDataByID5.MName
					});
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.OPERATION)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
			{
				DominData dominDataByID = DataManager.GetDominDataByID(mCurMissionData.LogicID);
				if (dominDataByID != null)
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100331}", new object[]
					{
						dominDataByID.GetName
					});
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LEVEL_UP)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100320}", new object[]
				{
					mCurMissionData.LogicID
				});
			}
			else
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
		}
		else if (curMissionState == MISSION_STATE.COMPLETE)
		{
			MissionStateLabel.color = Color.white;
			if (playerData.Level < mCurMissionData.MinLv)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100313}", new object[]
				{
					mCurMissionData.MinLv
				});
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.DELIVERY)
			{
				NpcData npcDataByID6 = DataManager.GetNpcDataByID(mCurMissionData.Target);
				if (npcDataByID6 != null)
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100316}", new object[]
					{
						npcDataByID6.MName
					});
				}
				MissionStateLabel.color = Color.white;
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LEVEL_UP)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100320}", new object[]
				{
					mCurMissionData.LogicID
				});
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SINGLE_DANCE)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else
			{
				NpcData npcDataByID7 = DataManager.GetNpcDataByID(mCurMissionData.Submit);
				if (npcDataByID7 != null)
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100318}", new object[]
					{
						npcDataByID7.MName
					});
					MissionStateLabel.color = Color.green;
				}
				else
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
				}
			}
		}
		else
		{
			MissionStateLabel.color = Color.white;
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER)
			{
				NpcData npcDataByID8 = DataManager.GetNpcDataByID(mCurMissionData.Target);
				if (npcDataByID8 != null)
				{
					MissionStateLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString("#{100315}", new object[]
					{
						npcDataByID8.MName
					}));
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100325}", new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.DESTROY_CAR)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100325}", new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.ROB_CAR)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100325}", new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.IMPACT_NPC)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100325}", new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
			{
				MissionStateLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]));
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.COLLECTITEM || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP)
			{
				NpcData npcDataByID9 = DataManager.GetNpcDataByID(mCurMissionData.Target);
				if (npcDataByID9 != null)
				{
					MissionStateLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString("#{100317}", new object[]
					{
						npcDataByID9.MName
					}));
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.COPYSCENE_KILLMONSTER)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.Class == 4)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.Class == 5)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData2 = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMultiDeliveryTargetData(mCurMissionData.ID);
				if (curMultiDeliveryTargetData2 != null)
				{
					NpcData npcDataByID10 = DataManager.GetNpcDataByID(curMultiDeliveryTargetData2.TargetNpcId);
					if (npcDataByID10 != null)
					{
						MissionStateLabel.text = string.Format("{0}", npcDataByID10.MName);
					}
				}
				else
				{
					Debug.Log("MultiDeliveryMissionData ERROR!!!!!!!!!!!!!!!!!!!!!");
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.STORY)
			{
				NpcData npcDataByID11 = DataManager.GetNpcDataByID(mCurMissionData.Target);
				if (npcDataByID11 != null)
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100314}", new object[]
					{
						npcDataByID11.MName
					});
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.OPERATION)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.DELIVERY)
			{
				NpcData npcDataByID12 = DataManager.GetNpcDataByID(mCurMissionData.Accept);
				if (npcDataByID12 != null)
				{
					MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100316}", new object[]
					{
						npcDataByID12.MName
					});
					MissionStateLabel.color = Color.white;
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.LEVEL_UP)
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString("#{100320}", new object[]
				{
					mCurMissionData.LogicID
				});
			}
			else
			{
				MissionStateLabel.text = StrDictionary.GetDictionaryString(mCurMissionData.TagDescribeID, new object[0]);
			}
		}
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x00077760 File Offset: 0x00075960
	public string GetCurDailyMissionID()
	{
		List<string> allMissionId = this.GetAllMissionId();
		for (int i = 0; i < allMissionId.Count; i++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[i]);
			if (missionDataByID.Class == 0)
			{
				return allMissionId[i];
			}
		}
		return string.Empty;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000777B0 File Offset: 0x000759B0
	public bool IsHaveDailyMission()
	{
		List<string> allMissionId = this.GetAllMissionId();
		for (int i = 0; i < allMissionId.Count; i++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[i]);
			if (missionDataByID.Class == 0 || missionDataByID.Class == 3 || missionDataByID.Class == 6)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x00077810 File Offset: 0x00075A10
	public CurMission GetCurMissionByClassType(MISSION_CLASS_TYPE type)
	{
		return this.mCurMissionDictionary.GetCurMissionByClassType(type);
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x00077820 File Offset: 0x00075A20
	public bool IsInEscortMission()
	{
		return this.mCurMissionDictionary.GetCurMissionByClassType(MISSION_CLASS_TYPE.ESCORT) != null;
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x00077834 File Offset: 0x00075A34
	public CurMission GetEscortMission()
	{
		return this.mCurMissionDictionary.GetCurMissionByClassType(MISSION_CLASS_TYPE.ESCORT);
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x00077850 File Offset: 0x00075A50
	public Vector3 GetEscortNpcPos(out string mapId)
	{
		CurMission escortMission = this.GetEscortMission();
		mapId = escortMission.GetParam(3).ToString();
		if (escortMission == null)
		{
			return Vector3.zero;
		}
		ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(escortMission.GetParam(1));
		if (objCharacter != null)
		{
			return objCharacter.Position;
		}
		return new Vector3((float)escortMission.GetParam(4) / 100f, 0f, (float)escortMission.GetParam(5) / 100f);
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x000778CC File Offset: 0x00075ACC
	public HEAD_PIC_TYPE GetMissionNpcHeadPicType(string npcId)
	{
		List<string> allMissionId = this.GetAllMissionId();
		HEAD_PIC_TYPE result = HEAD_PIC_TYPE.INVALID;
		for (int i = 0; i < allMissionId.Count; i++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[i]);
			if (missionDataByID.Submit.Equals(npcId) && this.GetMissionState(allMissionId[i]) == MISSION_STATE.COMPLETE)
			{
				return HEAD_PIC_TYPE.MISSION_COMPLETE_NPC;
			}
			if (missionDataByID.Target.Equals(npcId) && this.GetMissionState(allMissionId[i]) == MISSION_STATE.ACCEPTED)
			{
				result = HEAD_PIC_TYPE.MISSION_TARGET_NPC;
			}
		}
		return result;
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x00077954 File Offset: 0x00075B54
	~MissionManager()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.LevelUpMissionCheck));
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x000779AC File Offset: 0x00075BAC
	public void LevelUpMissionCheck()
	{
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		for (int i = 1; i <= level; i++)
		{
			List<string> lvAutoAcceptMissionListByLevel = DataManager.GetLvAutoAcceptMissionListByLevel(i);
			if (lvAutoAcceptMissionListByLevel != null)
			{
				for (int j = 0; j < lvAutoAcceptMissionListByLevel.Count; j++)
				{
					if (this.IsMissionAcceptable(lvAutoAcceptMissionListByLevel[j]))
					{
						this.AcceptMission(lvAutoAcceptMissionListByLevel[j]);
					}
				}
			}
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x00077A20 File Offset: 0x00075C20
	public Vector3 GetMoveTargetMissionPos(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID.MissionLogicType != MISSION_LOGICTYPE.ARRIVE_TARGET)
		{
			return Vector3.zero;
		}
		MoveTargetMissionData moveTargetMissionDataById = DataManager.GetMoveTargetMissionDataById(missionDataByID.LogicID);
		if (missionDataByID == null)
		{
			return Vector3.zero;
		}
		if (!this.mCurMissionDictionary.CurMissionDic.ContainsKey(missionId))
		{
			return Vector3.zero;
		}
		CurMission curMission = this.mCurMissionDictionary.CurMissionDic[missionId];
		if (curMission.MissionState != MISSION_STATE.ACCEPTED)
		{
			return Vector3.zero;
		}
		int num = (int)curMission.GetParam(0);
		if (num < moveTargetMissionDataById.TargetPointList.Count)
		{
			Vector3 pos;
			pos..ctor(moveTargetMissionDataById.TargetPointList[num].x, 0f, moveTargetMissionDataById.TargetPointList[num].z);
			return new Vector3(pos.x, SceneManager.GetHitHeight(pos), pos.z);
		}
		num = moveTargetMissionDataById.TargetPointList.Count - 1;
		Vector3 pos2;
		pos2..ctor(moveTargetMissionDataById.TargetPointList[num].x, 0f, moveTargetMissionDataById.TargetPointList[num].z);
		return new Vector3(pos2.x, SceneManager.GetHitHeight(pos2), pos2.z);
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00077B68 File Offset: 0x00075D68
	public void SetFirstSceneMissionTarget()
	{
		CurMission curMissionByClassType = this.GetCurMissionByClassType(MISSION_CLASS_TYPE.MAIN);
		if (curMissionByClassType != null)
		{
			MissionManager.SetFirstSceneMissionTarget(curMissionByClassType.MissionId);
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x00077B90 File Offset: 0x00075D90
	public static void SetFirstSceneMissionTarget(string misId)
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		int level = instance.PlayerData.Level;
		int num = 15;
		FunctionData functionDataById = DataManager.GetFunctionDataById(4087.ToString());
		if (functionDataById != null)
		{
			num = functionDataById.Condition;
		}
		if (level > num)
		{
			return;
		}
		Vector3 pos = Vector3.zero;
		string text = string.Empty;
		MissionData missionDataByID = DataManager.GetMissionDataByID(misId);
		if (missionDataByID.Class != 1)
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType != MAPTYPE.TUTORIAL_CAR)
		{
			return;
		}
		instance.SceneManager.ClearMoveTarget();
		MISSION_STATE missionState = instance.MissionManager.GetMissionState(missionDataByID.ID);
		if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.DOWNLOAD_MISSION && missionState == MISSION_STATE.ACCEPTED && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			instance.SceneManager.ClearMoveTarget();
			return;
		}
		if ((missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER) && missionState == MISSION_STATE.ACCEPTED)
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			MissionRequireData missionRequireDataByID = DataManager.GetMissionRequireDataByID(missionDataByID.LogicID);
			ObjNPC objNPC = Singleton<ObjManager>.Instance.FindNearestFightNPCInScene(missionRequireDataByID.NPCID);
			if (objNPC != null)
			{
				text = instance.SceneManager.CurrentMapInofData.ID;
				pos = objNPC.Position;
			}
		}
		MissionData missionData = missionDataByID;
		MISSION_STATE mission_STATE = missionState;
		if (mission_STATE == MISSION_STATE.ACCEPTED)
		{
			if (missionData.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
			{
				SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionData.LogicID);
				text = surveyMissionDataById.SceneID;
				pos.x = surveyMissionDataById.PosX;
				pos.z = surveyMissionDataById.PosZ;
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData = instance.MissionManager.GetCurMultiDeliveryTargetData(missionData.ID);
				if (curMultiDeliveryTargetData != null)
				{
					text = curMultiDeliveryTargetData.TargetMapId;
					if (string.IsNullOrEmpty(text))
					{
						instance.SceneManager.ClearMoveTarget();
						return;
					}
					Vector3 npcposInMonsterData = DataManager.GetNPCPosInMonsterData(text, curMultiDeliveryTargetData.TargetNpcId);
					pos.x = npcposInMonsterData.x;
					pos.z = npcposInMonsterData.z;
					pos.y = SceneManager.GetHitHeight(npcposInMonsterData);
				}
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
			{
				MoveTargetMissionData moveTargetMissionDataById = DataManager.GetMoveTargetMissionDataById(missionData.LogicID);
				if (moveTargetMissionDataById != null)
				{
					text = moveTargetMissionDataById.MapId;
					if (string.IsNullOrEmpty(text))
					{
						instance.SceneManager.ClearMoveTarget();
						return;
					}
					int num2 = (int)instance.MissionManager.GetMissionParam(missionData.ID, 0);
					if (num2 > moveTargetMissionDataById.TargetPointList.Count)
					{
						num2 = moveTargetMissionDataById.TargetPointList.Count - 1;
					}
					Vector3 pos2 = moveTargetMissionDataById.TargetPointList[num2];
					pos.x = pos2.x;
					pos.y = SceneManager.GetHitHeight(pos2);
					pos.z = pos2.z;
				}
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
			{
				KillTargetMissionData killTargetMissionDataById = DataManager.GetKillTargetMissionDataById(missionData.LogicID);
				text = killTargetMissionDataById.SceneID;
				pos = killTargetMissionDataById.Pos;
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR)
			{
				TargetCarMissionData targetCarMissionDataById = DataManager.GetTargetCarMissionDataById(missionData.LogicID);
				text = targetCarMissionDataById.SceneID;
				pos = targetCarMissionDataById.Pos;
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.ROB_CAR)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType != MAPTYPE.TUTORIAL_CAR)
				{
					text = "11";
					pos..ctor(281f, 0f, -100f);
				}
				else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.ROB_CAR_TIP))
				{
					NewTutorialSceneManager newTutorialSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager;
					if (newTutorialSceneManager != null && newTutorialSceneManager.mTutorialCar != null)
					{
						text = "11";
						pos = newTutorialSceneManager.mTutorialCar.PlayerCar.DummyPlayerPoint.position;
					}
				}
			}
			else if (missionData.MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
			{
				DominData dominDataByID = DataManager.GetDominDataByID(missionData.LogicID);
				if (dominDataByID != null)
				{
					text = dominDataByID.AcceptMapID;
					pos = dominDataByID.GetPos();
				}
				else
				{
					text = string.Empty;
				}
			}
			else
			{
				text = missionData.TargetMapId;
				if (string.IsNullOrEmpty(text))
				{
					instance.SceneManager.ClearMoveTarget();
					return;
				}
				Vector3 npcposInMonsterData2 = DataManager.GetNPCPosInMonsterData(text, missionData.Target);
				pos.x = npcposInMonsterData2.x;
				pos.z = npcposInMonsterData2.z;
				pos.y = SceneManager.GetHitHeight(npcposInMonsterData2);
			}
		}
		else if (mission_STATE == MISSION_STATE.COMPLETE)
		{
			text = missionData.SubmitMapId;
			if (string.IsNullOrEmpty(text))
			{
				instance.SceneManager.ClearMoveTarget();
				return;
			}
			Vector3 pos3;
			if (!DataManager.GetNPCPosInMonsterData2(text, missionData.Submit, out pos3))
			{
				return;
			}
			pos.x = pos3.x;
			pos.z = pos3.z;
			pos.y = SceneManager.GetHitHeight(pos3);
		}
		if (string.IsNullOrEmpty(text))
		{
			instance.SceneManager.ClearMoveTarget();
			return;
		}
		instance.SceneManager.SetMoveTarget(pos, missionData.ID);
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000780D8 File Offset: 0x000762D8
	public static void ClickMissionAction(string misId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(misId);
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		instance.SceneManager.ClearMoveTarget();
		int level = instance.PlayerData.Level;
		if (missionDataByID == null)
		{
			return;
		}
		if (!instance.MissionManager.IsMissionAccepted(misId))
		{
			if (string.IsNullOrEmpty(missionDataByID.Accept))
			{
				if (string.IsNullOrEmpty(missionDataByID.AcceptMapId))
				{
					return;
				}
				List<ActivityMapData> acitvityMapDataByMapId = DataManager.GetAcitvityMapDataByMapId(missionDataByID.AcceptMapId);
				ActivityMapData activityMapData = null;
				for (int i = 0; i < acitvityMapDataByMapId.Count; i++)
				{
					if (acitvityMapDataByMapId[i].ActivityType == GameDefine.ACTIVITY_TYPE.MISSION && acitvityMapDataByMapId[i].ActivityID.Equals(missionDataByID.ID))
					{
						activityMapData = acitvityMapDataByMapId[i];
						break;
					}
				}
				if (activityMapData != null)
				{
					Vector3 position = activityMapData.Position;
					SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath.FindPath(activityMapData.MapId, position.x, position.y, position.z, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP);
					AutoSearchPath curPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath.CurPath;
					if (curPath != null && curPath.PathPointList.Count > 0)
					{
						ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
						mainPlayer.BreakAutoCombatState();
						mainPlayer.SkillLogic.BreakCurSkill();
						if (curPath.PathPointList != null && curPath.PathPointList.Count > 0)
						{
							SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.SetMoveTarget(new Vector3(curPath.PathPointList[0].PosX, 0f, curPath.PathPointList[0].PosZ), misId);
							mainPlayer.MoveTo(curPath.PathPointList[0].PosX, curPath.PathPointList[0].PosZ, mainPlayer.GetStopDistance(), new ObjCharacter.TargetArriveFinsh(SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.MoveToNextPoint));
						}
					}
				}
			}
			else
			{
				instance.MissionManager.MissionFindPath(missionDataByID);
			}
			return;
		}
		if (level < missionDataByID.MinLv)
		{
			NoticeLogic.AddNotifyData2Client(false, "#{100313}", true, new object[]
			{
				missionDataByID.MinLv
			});
			if (missionDataByID.Class == 1)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn();
					}, null);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickMissionBtn();
					}, null);
				}
			}
			return;
		}
		MISSION_STATE missionState = instance.MissionManager.GetMissionState(missionDataByID.ID);
		MissionTipRootLogic.ShowMissionTips(missionDataByID.ID, missionState);
		if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.DOWNLOAD_MISSION && missionState == MISSION_STATE.ACCEPTED)
		{
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload && SingletonUnity<DownloadTipRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DownloadTipRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DownloadTipRootLogic>.Instance.OnClickDownloadTipBtn();
			}
			return;
		}
		if ((missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER) && missionState == MISSION_STATE.ACCEPTED)
		{
			ObjMainPlayer mainPlayer2 = Singleton<ObjManager>.Instance.MainPlayer;
			mainPlayer2.SelectTarget(null);
			MissionRequireData missionRequireDataByID = DataManager.GetMissionRequireDataByID(missionDataByID.LogicID);
			ObjNPC objNPC = Singleton<ObjManager>.Instance.FindNearestFightNPCInScene(missionRequireDataByID.NPCID);
			if (objNPC != null)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.SetMoveTarget(objNPC.Position, misId);
				if (!mainPlayer2.IsLocalDrivingCar)
				{
					mainPlayer2.BreakAutoCombatState();
					mainPlayer2.SkillLogic.BreakCurSkill();
					mainPlayer2.MoveTo(objNPC.Position, 1f, delegate(ObjCharacter A_0)
					{
						SingletonUnity<JueseJiNengQuLogic>.Instance.UseSkill_1_Onclick();
					});
				}
				return;
			}
		}
		if (missionDataByID.MissionLogicType != MISSION_LOGICTYPE.MASSACRE_NPC || missionState != MISSION_STATE.ACCEPTED)
		{
			if ((missionDataByID.Class == 0 || missionDataByID.Class == 6 || missionDataByID.Class == 7) && missionState == MISSION_STATE.COMPLETE)
			{
				complete_mission.request request = new complete_mission.request();
				request.missionId = missionDataByID.ID;
				NetLogic.GetInstance().Send<Protocol.complete_mission>(request, null);
			}
			else if (missionDataByID.Class == 4)
			{
				if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickEscortFollowBtn();
					return;
				}
			}
			else if (missionDataByID.Class == 5)
			{
				if (instance.SceneManager.CurrentMapInofData.ID.Equals(missionDataByID.AcceptMapId))
				{
					ObjNPC nearestEscortNPC = Singleton<ObjManager>.Instance.GetNearestEscortNPC();
					if (nearestEscortNPC != null)
					{
						ObjMainPlayer mainPlayer3 = Singleton<ObjManager>.Instance.MainPlayer;
						mainPlayer3.BreakAutoCombatState();
						mainPlayer3.SkillLogic.BreakCurSkill();
						mainPlayer3.SelectTarget(nearestEscortNPC);
						mainPlayer3.MoveTo(nearestEscortNPC.Position, 1f, delegate(ObjCharacter A_0)
						{
							SingletonUnity<JueseJiNengQuLogic>.Instance.UseSkill_1_Onclick();
						});
						return;
					}
					NoticeLogic.AddNotifyData("#{102047}", true, false);
				}
				else
				{
					instance.MissionManager.AutoMoveDest(missionDataByID.AcceptMapId, Vector3.right * 30f, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP, null);
				}
			}
			else if (missionDataByID.Class == 2 || missionDataByID.Class == 1)
			{
				MissionManager.TutorialAction(missionDataByID);
			}
			else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.COPYSCENE_KILLMONSTER && missionState == MISSION_STATE.ACCEPTED && string.IsNullOrEmpty(missionDataByID.Target))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
				enter_copy_scene.request request2 = new enter_copy_scene.request();
				request2.mapInfoId = missionDataByID.LogicID;
				NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request2, null);
			}
			else
			{
				instance.MissionManager.MissionFindPath(missionDataByID);
			}
			return;
		}
		if (!instance.SceneManager.CurrentMapInofData.ID.Equals(missionDataByID.TargetMapId))
		{
			MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(missionDataByID.TargetMapId);
			if (mapInfoDataByID != null)
			{
				instance.MissionManager.AutoMoveDest(missionDataByID.TargetMapId, mapInfoDataByID.BirthPosVector3, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP, null);
			}
			return;
		}
		if (!Singleton<ObjManager>.Instance.MainPlayer.IsLocalDrivingCar)
		{
			ObjNPC objNPC2 = Singleton<ObjManager>.Instance.FindNearestFightNPCInScene();
			if (objNPC2 != null)
			{
				Singleton<ObjManager>.Instance.MainPlayer.SelectTarget(objNPC2);
			}
			SingletonUnity<JueseJiNengQuLogic>.Instance.UseSkill_1_Onclick();
			return;
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x0007876C File Offset: 0x0007696C
	public static void TutorialAction(MissionData mCurMissionData)
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		MISSION_STATE missionState = instance.MissionManager.GetMissionState(mCurMissionData.ID);
		if (missionState == MISSION_STATE.COMPLETE)
		{
			if (string.IsNullOrEmpty(mCurMissionData.Submit))
			{
				complete_mission.request request = new complete_mission.request();
				request.missionId = mCurMissionData.ID;
				NetLogic.GetInstance().Send<Protocol.complete_mission>(request, null);
			}
			else
			{
				instance.MissionManager.MissionFindPath(mCurMissionData);
			}
		}
		else
		{
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SKILL_UPGRADE)
			{
				if (TutorialManager.IsTutorialCanShow() && Singleton<ObjManager>.Instance.MainPlayer.CheckSkillCanUpdate())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.SKILL_UPGRADE_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.EQUIP_UPGRADE)
			{
				if (TutorialManager.IsTutorialCanShow())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.ENHANCE_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.CAR_COPY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.CAR_COPY_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.STRENGTHEN_STAR)
			{
				if (TutorialManager.IsTutorialCanShow())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.STRENGTH_STAR_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.DAILY_GUIDE)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.DAILY_MISSION_CLICK_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.TEAM_GUIDE)
			{
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.EXP_COPY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.EXP_COPY_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.GOLD_COPY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.GOLD_COPY_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.TOWER_COPY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.TOWER_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SCUFFLE_COPY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.SCUFFLE_COPY_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.GUILD_GUIDE)
			{
				if (TutorialManager.IsTutorialCanShow())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.GUILD_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.PVP_GUIDE)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.RANK_PVP_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.TITLE_GUIDE)
			{
				if (TutorialManager.IsTutorialCanShow())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.TITLE_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.BADGE_GUIDE)
			{
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.EQUIP_COPY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.EQUIP_COPY_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.WORLD_BOSS)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.WORLD_BOSS_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SELL_ITEM)
			{
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.BAR_FIGHT)
			{
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.ACCEPT_ESSCORT)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.ESCORT_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.DRAG_SKILL)
			{
				if (TutorialManager.IsTutorialCanShow())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.SKILL_DRAG_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.ACCEPT_ROBBORY)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.ROBBORY_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SURVIVAL_BATTLE)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.SURVIVAL_BATTLE_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.GUILD_BOSS)
			{
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SLOT)
			{
				if (TutorialManager.IsTutorialCanShow())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.SLOT_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.CAPTURE)
			{
				if (TutorialManager.IsTutorialCanShow_ClickMission())
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.CAPTURE_START);
				}
				return;
			}
			if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UPGRADE_ARMOR)
			{
				ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
				GameItem item = equipPack.GetEquipByEquipType(EQUIP_BACKPACK_TYPE.BODY);
				if (item == null)
				{
					item = equipPack.GetEquipByEquipType(EQUIP_BACKPACK_TYPE.LEG);
				}
				if (item == null)
				{
					item = equipPack.GetEquipByEquipType(EQUIP_BACKPACK_TYPE.HEAD);
				}
				if (item == null)
				{
					NoticeLogic.AddNotifyData("#{200095}", true, false);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
					{
						SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(item, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
					}, null);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UPGRADE_WEAPON)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
				}, null);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UPGRADE_JEWELRY)
			{
				ItemContainer equipPack2 = instance.PlayerData.EquipPack;
				GameItem item = equipPack2.GetEquipByEquipType(EQUIP_BACKPACK_TYPE.BELT);
				if (item == null)
				{
					item = equipPack2.GetEquipByEquipType(EQUIP_BACKPACK_TYPE.NECKLACE);
				}
				if (item == null)
				{
					NoticeLogic.AddNotifyData("#{200096}", true, false);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
					{
						SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(item, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
					}, null);
				}
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UPGRADE_STARS)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
				}, null);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UPGRADE_BADGE)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowBadgeMerge(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
				}, null);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UNLOCK_VEHICLE)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickCarBtn();
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.UPGRADE_SKILL)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickSkillBtn();
				}, null);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.SINGLE_DANCE)
			{
				SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest("101", Vector3.right * -5f, AUTO_SEARCH_PARTH_FINISHEVENT.CITY_DANCE, null);
			}
			else if (mCurMissionData.MissionLogicType == MISSION_LOGICTYPE.COPYSCENE_KILLMONSTER && missionState == MISSION_STATE.ACCEPTED && string.IsNullOrEmpty(mCurMissionData.Target))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
				enter_copy_scene.request request2 = new enter_copy_scene.request();
				request2.mapInfoId = mCurMissionData.LogicID;
				NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request2, null);
			}
			else
			{
				instance.MissionManager.MissionFindPath(mCurMissionData);
			}
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x00078DB8 File Offset: 0x00076FB8
	public long GetMissionRestTime(string missionId)
	{
		if (!this.IsMissionAccepted(missionId))
		{
			return -1L;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID == null || missionDataByID.Class != 8)
		{
			return -1L;
		}
		TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
		if (timeLimitMissionDataByID == null)
		{
			return -1L;
		}
		long missionParam = this.mCurMissionDictionary.GetMissionParam(missionId, 7);
		long limitTime = timeLimitMissionDataByID.LimitTime;
		long num = limitTime - (PlayerCommonData.GetServerTime() - missionParam);
		if (num < 0L)
		{
			num = 0L;
		}
		return num;
	}

	// Token: 0x040017A9 RID: 6057
	private bool LocalTestFlag;

	// Token: 0x040017AA RID: 6058
	private CurMissionDictionary mCurMissionDictionary = new CurMissionDictionary();

	// Token: 0x040017AB RID: 6059
	private MissionData curDailyMissionData;

	// Token: 0x040017AC RID: 6060
	private MissionData curMainMissionData;

	// Token: 0x040017AD RID: 6061
	private List<string> mFunctionMissionIdList;
}
