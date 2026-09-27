using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200088A RID: 2186
public class SurveyItemManager : Singleton<SurveyItemManager>
{
	// Token: 0x17000F7B RID: 3963
	// (get) Token: 0x06003AFB RID: 15099 RVA: 0x001007E8 File Offset: 0x000FE9E8
	private MissionManager mMissionManager
	{
		get
		{
			if (this.mCacheMissionManager == null)
			{
				this.mCacheMissionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			}
			return this.mCacheMissionManager;
		}
	}

	// Token: 0x06003AFC RID: 15100 RVA: 0x0010080C File Offset: 0x000FEA0C
	public void InitSurveyItem(string sceneId)
	{
		if (string.IsNullOrEmpty(sceneId))
		{
			return;
		}
		if (sceneId.Equals(0.ToString()))
		{
			return;
		}
		ObjManager instance = Singleton<ObjManager>.Instance;
		List<SurveyMissionData> surveyMissionDataBySceneId = DataManager.GetSurveyMissionDataBySceneId(sceneId);
		for (int i = 0; i < surveyMissionDataBySceneId.Count; i++)
		{
			if (surveyMissionDataBySceneId[i].IsAlwaysExist == 1)
			{
				string surveyItemName = surveyMissionDataBySceneId[i].GetSurveyItemName();
				if (instance.FindOtherObjInDic(surveyItemName) == null)
				{
					instance.CreateSurveyItem(surveyMissionDataBySceneId[i]);
				}
			}
		}
		List<string> allMissionId = this.mMissionManager.GetAllMissionId();
		for (int j = 0; j < allMissionId.Count; j++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[j]);
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
			{
				SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
				if (surveyMissionDataById.IsAlwaysExist != 1 && surveyMissionDataById.SceneID.Equals(sceneId) && this.mMissionManager.GetMissionState(missionDataByID.ID) != MISSION_STATE.COMPLETE)
				{
					string surveyItemName2 = surveyMissionDataById.GetSurveyItemName();
					GameObject gameObject = instance.FindOtherObjInDic(surveyItemName2);
					if (gameObject == null)
					{
						instance.CreateSurveyItem(surveyMissionDataById);
					}
					else
					{
						SurveyItemObj component = gameObject.GetComponent<SurveyItemObj>();
						if (component != null)
						{
							component.Reset(surveyMissionDataById);
						}
					}
				}
			}
		}
	}

	// Token: 0x06003AFD RID: 15101 RVA: 0x0010097C File Offset: 0x000FEB7C
	public void StartSurveyItem(SurveyItemObj surveyItemObj)
	{
		if (surveyItemObj == null)
		{
			return;
		}
		if (!surveyItemObj.IsEnable())
		{
			return;
		}
		List<string> allMissionId = this.mMissionManager.GetAllMissionId();
		for (int i = 0; i < allMissionId.Count; i++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[i]);
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
			{
				if (this.mMissionManager.GetMissionState(missionDataByID.ID) != MISSION_STATE.COMPLETE)
				{
					SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
					if (surveyMissionDataById.ID.Equals(surveyItemObj.SurveyMissionData.ID))
					{
						this.mCurMissionId = missionDataByID.ID;
						this.mCurSurveyItem = surveyItemObj;
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SurveyProgressLine, delegate(bool isSuccess, object param)
						{
							if (isSuccess)
							{
								int num = (int)this.mMissionManager.GetMissionParam(this.mCurMissionId, 0);
								SingletonUnity<SurveyProgressLineLogic>.Instance.Reset(surveyItemObj.SurveyMissionData, surveyItemObj.SurveyMissionData.NeedNum - num);
							}
						}, null);
						if (!string.IsNullOrEmpty(surveyMissionDataById.ModelName))
						{
							CameraController cameraController = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
							cameraController.IsCamCanUse = false;
							cameraController.IdealYaw = (surveyMissionDataById.PosO + 270f) % 360f;
							cameraController.IdealPitch = 15f;
							cameraController.smoothOrbitSpeed = 5f;
						}
					}
				}
			}
		}
	}

	// Token: 0x06003AFE RID: 15102 RVA: 0x00100AE0 File Offset: 0x000FECE0
	public void FinishSurveyItem()
	{
		if (this.mCurSurveyItem != null && !string.IsNullOrEmpty(this.mCurMissionId))
		{
			this.mCurSurveyItem.CollectOne();
			update_misison_parm.request request = new update_misison_parm.request();
			request.missionId = this.mCurMissionId;
			request.paramType = 1L;
			request.paramValue = 1L;
			NetLogic.GetInstance().Send<Protocol.update_misison_parm>(request, null);
		}
	}

	// Token: 0x06003AFF RID: 15103 RVA: 0x00100B48 File Offset: 0x000FED48
	public void CompleteSurveyItem()
	{
		if (this.mCurSurveyItem.MeshRoot != null)
		{
			this.mCurSurveyItem.PlayEffect();
			vp_Timer.In((float)this.mCurSurveyItem.SurveyMissionData.DelayRecycleTime, delegate()
			{
				if (this.mCurSurveyItem != null)
				{
					Singleton<ObjManager>.Instance.RecycleSurveyItem(this.mCurSurveyItem);
				}
			}, null);
		}
		else
		{
			Singleton<ObjManager>.Instance.RecycleSurveyItem(this.mCurSurveyItem);
		}
	}

	// Token: 0x0400269E RID: 9886
	private string mCurMissionId = string.Empty;

	// Token: 0x0400269F RID: 9887
	private SurveyItemObj mCurSurveyItem;

	// Token: 0x040026A0 RID: 9888
	private MissionManager mCacheMissionManager;
}
