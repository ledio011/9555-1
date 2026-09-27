using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class DataManager
{
	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00056D58 File Offset: 0x00054F58
	public static bool DataInitFinishFlag
	{
		get
		{
			return DataManager.initDoneFlag && AnimationManager.initStreamDoneFlag && AnimationManager.initDownloadDoneFlag;
		}
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00056D78 File Offset: 0x00054F78
	public static void InitData(MonoBehaviour mono)
	{
		if (DataManager.initFlag)
		{
			return;
		}
		DataManager.initDoneFlag = false;
		DataManager.initFlag = true;
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadData(new BundleManager.OnLoadDataFinishedDelegate(DataManager.OnLoadBundleFinished)));
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00056DC4 File Offset: 0x00054FC4
	public static void ReImportData(MonoBehaviour mono)
	{
		DataManager.initDoneFlag = false;
		DataManager.reImportFlag = true;
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadData(new BundleManager.OnLoadDataFinishedDelegate(DataManager.OnLoadBundleFinished)));
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00056E08 File Offset: 0x00055008
	public static void LoadBaseLocalization()
	{
		LocalizationManager.ResourceLoadDictionary();
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00056E10 File Offset: 0x00055010
	private static void OnLoadBundleFinished()
	{
		DataManager.ClearAllList();
		DataManager.mSkillDataDict = DataReader.LoadTable<string, SkillData>("SkillData", "ID");
		foreach (KeyValuePair<string, SkillData> keyValuePair in DataManager.mSkillDataDict)
		{
			keyValuePair.Value.Init();
		}
		DataManager.mEffInfoDataDict = DataReader.LoadTable<string, EffInfoData>("EffInfoData", "ID");
		foreach (KeyValuePair<string, EffInfoData> keyValuePair2 in DataManager.mEffInfoDataDict)
		{
			keyValuePair2.Value.Init();
		}
		DataManager.mActionDataDict = DataReader.LoadTable<string, ActionData>("ActionData", "ID");
		DataManager.mBuffInfoDataDict = DataReader.LoadTable<string, BuffInfoData>("BuffInfoData", "ID");
		DataManager.mNpcDataDict = DataReader.LoadTable<string, NpcData>("NpcData", "ID");
		DataManager.mMapInfoDataDict = DataReader.LoadTable<string, MapInfoData>("MapInfoData", "ID");
		DataManager.mCharacterModelDataDict = DataReader.LoadTable<string, CharacterModelData>("CharacterModelData", "ID");
		foreach (KeyValuePair<string, CharacterModelData> keyValuePair3 in DataManager.mCharacterModelDataDict)
		{
			keyValuePair3.Value.Init();
		}
		DataManager.mFxEffInfoDataDict = DataReader.LoadTableList<string, FxEffInfoData>("FxEffectData", "ID");
		DataManager.mItemDataDict = DataReader.LoadTable<string, ItemData>("ItemData", "ID");
		DataManager.mMissionDataDict = DataReader.LoadMissionDataTable("MissionData", "ID", ref DataManager.mLvAutoAcceptMissionDic);
		DataManager.mNPCDialogDataDict = DataReader.LoadTable<string, NPCDialogData>("NPCDialogData", "ID");
		DataManager.mMissionRequireDataDict = DataReader.LoadTable<string, MissionRequireData>("MissionRequireData", "ID");
		DataManager.mNpcOptionDialogDataDict = DataReader.LoadTable<string, NpcOptionDialogData>("NPCOptionDialogData", "ID");
		DataManager.mEquipDataDic = DataReader.LoadTable<string, EquipData>("EquipData", "ID");
		if (GameManager.IsSupportCurDataVersion184())
		{
			DataManager.mSkillupgradeDataDic = DataReader.LoadTable<int, SkillupgradeData>("SkillupgradeData", "level");
		}
		DataManager.mCopySceneDataDic = DataReader.LoadTable<string, CopySceneData>("CopySceneData", "ID");
		DataManager.mBaseLvDataDic = DataReader.LoadTable<int, BaseLvData>("BaseLvData", "Lv");
		DataManager.mTitleDateDic = DataReader.LoadTable<string, TitleData>("TitleData", "ID");
		DataManager.mStoryDataDic = DataReader.LoadTableList<string, StoryData>("StoryData", "ID");
		DataManager.mEquipmentUpgradeDataDic = DataReader.LoadTableList<int, EquipmentUpgradeData>("EquipmentUpgradeData", "PartID");
		DataManager.mModeDataDict = DataReader.LoadTable<string, ModelData>("ModelData", "ID");
		DataManager.mMapConnectInfoDataDic = DataReader.LoadTable<string, MapConnectInfoData>("MapConnectInfoData", "ID");
		DataManager.mDamageBoardTypeDataDic = DataReader.LoadTable<int, DamageBoardTypeData>("DamageBoardTypeData", "ID");
		DataManager.mCamRockCurveDataDic = DataReader.LoadTableList<string, CamRockCurveData>("CamRockCurveData", "AnimaClipName");
		DataManager.mCamRockDataDic = DataReader.LoadTable<string, CamRockData>("CamRockData", "ID");
		DataManager.mSurveyMissionDataDic = DataReader.LoadTable<string, SurveyMissionData>("SurveyMissionData", "ID");
		DataManager.mDailyMissionDataDic = DataReader.LoadTable<string, DailyMissionData>("DailyMissionData", "ID");
		DataManager.mMultiDeliveryMissionDataDic = DataReader.LoadTableList<string, MultiDeliveryMissionData>("MultiDeliveryMissionData", "ID");
		DataManager.mRefineDataDic = DataReader.LoadTableList<int, RefineData>("RefineData", "Part");
		DataManager.mRefineLevelDataDic = DataReader.LoadTable<int, RefineLevelData>("RefineLevelData", "ID");
		DataManager.mCarMissionBlockDataDic = DataReader.LoadTableList<string, CarMissionBlockData>("CarMissionBlockData", "SceneID");
		DataManager.mConsignBuyTabDataDic = DataReader.LoadTableList<int, ConsignBuyTabData>("ConsignBuyTabData", "TopTabId");
		DataManager.mSneakingMissionDataDic = DataReader.LoadTableList<string, SneakingMissionData>("SneakingMissionData", "MapID");
		DataManager.mBadgeDataDic = DataReader.LoadTable<string, BadgeData>("BadgeData", "ID");
		DataManager.mTowerDataDic = DataReader.LoadTable<int, TowerData>("TowerData", "FloorID");
		DataManager.mAIDataDic = DataReader.LoadTable<string, AIData>("AIData", "ID");
		DataManager.mWildBossDataDic = DataReader.LoadTable<string, WildBossData>("WildBossData", "ID");
		DataManager.mBarFightCopyDataDic = DataReader.LoadTable<string, BarFightCopyData>("BarFightCopyData", "ID");
		DataManager.mTeamDataDic = DataReader.LoadTable<string, TeamData>("TeamData", "ID");
		DataManager.mAdaptDataDic = DataReader.LoadDicTable<int, AdaptData>("AdaptData", "ID", "DorpKeyDic");
		LocalizationManager.BundleLoadDictionary();
		DataManager.mMonsterDataDic = DataReader.LoadTableList<string, MonsterData>("MonsterData", "MapID");
		DataManager.mNPCPathDataDic = DataReader.LoadTableList<string, NPCPathData>("NPCPathData", "ID");
		DataManager.mGuildBossDataDic = DataReader.LoadTable<string, GuildBossData>("GuildBossData", "ID");
		DataManager.mGuildDonateDataDic = DataReader.LoadTable<string, GuildDonateData>("GuildDonateData", "ID");
		DataManager.mGuildSkillDataDic = DataReader.LoadTableList<int, GuildSkillData>("GuildSkillData", "GuildSkillType");
		DataManager.mGuildLevelDataList = DataReader.LoadImportData<GuildLevelData>("GuildLevelData");
		DataManager.mShowRewardDataDic = DataReader.LoadTable<string, ShowRewardData>("ShowRewardData", "ID");
		DataManager.mFunctionDataDic = DataReader.LoadTable<string, FunctionData>("FunctionData", "ID");
		DataManager.mSoundDataDic = DataReader.LoadTable<int, SoundData>("SoundData", "Id");
		DataManager.mMapAreaInfoDataDic = DataReader.LoadTableList<string, MapAreaInfoData>("MapAreaInfoData", "ID");
		foreach (KeyValuePair<string, List<MapAreaInfoData>> keyValuePair4 in DataManager.mMapAreaInfoDataDic)
		{
			for (int i = 0; i < keyValuePair4.Value.Count; i++)
			{
				keyValuePair4.Value[i].Init();
			}
		}
		DataManager.mEscortDataDic = DataReader.LoadTable<string, EscortData>("EscortData", "ID");
		DataManager.mCityDanceDataDic = DataReader.LoadTable<string, CityDanceData>("CityDanceData", "ID");
		DataManager.mDanceDataDic = DataReader.LoadTable<string, DanceData>("DanceData", "ID");
		DataManager.mLadderRewardDataDic = DataReader.LoadTable<string, LadderRewardData>("LadderRewardData", "ID");
		DataManager.mMountDataDic = DataReader.LoadTable<string, MountData>("MountData", "ID");
		DataManager.mColorDataDic = DataReader.LoadTable<string, ColorData>("ColorData", "ID");
		DataManager.mSlotIconDataDic = DataReader.LoadTable<string, SlotIconData>("SlotIconData", "ID");
		DataManager.mSlotAutoDataDic = DataReader.LoadTable<string, SlotAutoData>("SlotAutoData", "ID");
		DataManager.mSurviveBattleDataDic = DataReader.LoadTable<string, SurviveBattleData>("SurviveBattleData", "ID");
		DataManager.mSignInMonthDataDic = DataReader.LoadTable<int, SignInMonthData>("SignInMonthData", "Day");
		DataManager.mDailyBuyDataDic = DataReader.LoadTable<string, DailyBuyData>("DailyBuyData", "ID");
		DataManager.mInvestDataDic = DataReader.LoadTable<string, InvestData>("InvestData", "ID");
		DataManager.mLevelPackageDataDic = DataReader.LoadTable<string, LevelPackageData>("LevelPackageData", "ID");
		DataManager.mRetrieveDataDic = DataReader.LoadTable<string, RetrieveData>("RetrieveData", "ID");
		DataManager.mSignInWeekDataDic = DataReader.LoadTable<int, SignInWeekData>("SignInWeekData", "Day");
		DataManager.mDailyActiveDataDic = DataReader.LoadTable<string, DailyActiveData>("DailyActiveData", "ID");
		DataManager.mDailyActiveRewardDataDic = DataReader.LoadTable<string, DailyActiveRewardData>("DailyActiveRewardData", "ID");
		DataManager.mFirstBuyDataDic = DataReader.LoadTable<string, FirstBuyData>("FirstBuyData", "ID");
		DataManager.mBigPackageDataDic = DataReader.LoadTable<string, BigPackageData>("BigPackageData", "ID");
		DataManager.mPurchaseDataDic = DataReader.LoadTable<string, PurchaseData>("PurchaseData", "ProductId");
		DataManager.mModelPartDataDic = DataReader.LoadTable<string, ModelPartData>("ModelPartData", "ID");
		DataManager.mShowModelDataDic = DataReader.LoadTable<string, ShowModelData>("ShowModelData", "ID");
		DataManager.mScuffleDataDic = DataReader.LoadTable<string, ScuffleData>("ScuffleData", "ID");
		DataManager.mSocialDanceDataDic = DataReader.LoadTable<string, SocialDanceData>("SocialDanceData", "ID");
		DataManager.mAnnounceDataList = DataReader.LoadImportData<AnnounceData>("AnnounceData");
		DataManager.mDownloadRewardDataDic = DataReader.LoadTable<string, DownloadRewardData>("DownloadRewardData", "ID");
		DataManager.mSexMiniDataDic = DataReader.LoadTable<string, SexMiniData>("SexMiniData", "ID");
		DataManager.mShopDataDic = DataReader.LoadTable<string, ShopData>("ShopData", "ID");
		DataManager.mStrongerDataDic = DataReader.LoadTable<string, StrongerData>("StrongerData", "ID");
		DataManager.mLoadingUIDataDic = DataReader.LoadTable<string, LoadingUIData>("LoadingUIData", "ID");
		DataManager.mSceneComponentDataDict = DataReader.LoadTableList<string, SceneComponentData>("SceneComponentData", "MapID");
		DataManager.mTimerActivityTipsDataDic = DataReader.LoadTable<string, TimerActivityTipsData>("TimerActivityTipsData", "ID");
		DataManager.mTimerActivityDataDic = DataReader.LoadTable<string, TimerActivityData>("TimerActivityData", "ID");
		DataManager.mActivityBossDataDic = DataReader.LoadTable<string, ActivityBossData>("ActivityBossData", "Key");
		DataManager.mNotifyDataDic = DataReader.LoadTable<string, NotifyData>("NotifyData", "ID");
		DataManager.mLevelRewardDataDic = DataReader.LoadTable<string, LevelRewardData>("LevelRewardData", "ID");
		DataManager.mGuildStarDataDic = DataReader.LoadTable<string, GuildStarData>("GuildStarData", "ID");
		DataManager.mGuildBattleDataDic = DataReader.LoadTable<string, GuildBattleData>("GuildBattleData", "ID");
		DataManager.mActivityMapDataDic = DataReader.LoadTable<string, ActivityMapData>("ActivityMapData", "ID");
		DataManager.mConfigDataDic = DataReader.LoadTable<string, ConfigData>("ConfigData", "Key");
		DataManager.mMonthlyCardDataDic = DataReader.LoadTable<string, MonthlyCardData>("MonthlyCardData", "ID");
		DataManager.mMoveTargetMissionDataDic = DataReader.LoadTable<string, MoveTargetMissionData>("MoveTargetMissionData", "ID");
		DataManager.mPoliceLevelDataDic = DataReader.LoadTable<string, PoliceLevelData>("PoliceLevelData", "ID");
		DataManager.mSingleMapLockDataDic = DataReader.LoadTable<int, SingleMapLockData>("SingleMapLockData", "ID");
		DataManager.mKillTargetMissionDataDic = DataReader.LoadTable<string, KillTargetMissionData>("KillTargetMissionData", "ID");
		DataManager.mDailyExpDataDic = DataReader.LoadTable<string, DailyExpData>("DailyExpData", "ID");
		DataManager.mOnlineMissionDataDict = DataReader.LoadTable<string, OnlineMissionData>("OnlineMissionData", "ID");
		DataManager.mDominDataDict = DataReader.LoadTable<string, DominData>("DominData", "ID");
		DataManager.mLevelSealDataDict = DataReader.LoadTable<string, LevelSealData>("LevelSealData", "ID");
		DataManager.mTargetCarMissionDataDic = DataReader.LoadTable<string, TargetCarMissionData>("TargetCarMissionData", "ID");
		DataManager.mBigPackageTimeListDataDic = DataReader.LoadTable<string, BigPackageTimeListData>("BigPackageTimeListData", "ID");
		DataManager.mTimeLimitMissionDataDict = DataReader.LoadTable<string, TimeLimitMissionData>("TimeLimitMissionData", "ID");
		DataManager.mSkillLabelDataDict = DataReader.LoadTable<string, SkillLabelData>("SkillLabelData", "ID");
		DataManager.mGuildCaptureDataDict = DataReader.LoadTable<string, GuildCaptureData>("GuildCaptureData", "ID");
		DataManager.mQualityDataDic = DataReader.LoadTableList<string, QualityData>("QualityData", "ID");
		DataManager.initDoneFlag = true;
		BundleManager.UnloadDataBundle();
		if (!DataManager.reImportFlag)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ResetPlayerData();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ClearData();
			if (SingletonUnity<MenuSceneController>.Exists)
			{
				SingletonUnity<MenuSceneController>.Instance.DataLoadFinish();
			}
		}
		else
		{
			DataManager.reImportFlag = false;
		}
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00057850 File Offset: 0x00055A50
	private static void ClearAllList()
	{
		DataManager.mLadderRewardDataList.Clear();
		DataManager.mCopySceneDataList.Clear();
		DataManager.mTitleList.Clear();
		DataManager.mSurveyMissionDataList.Clear();
		DataManager.BadgeDataList = null;
		DataManager.mGuildLevelDataList.Clear();
		DataManager.mSlotIconList.Clear();
		DataManager.mSlotAutoList.Clear();
		DataManager.mSignInMonthDataList.Clear();
		DataManager.mLevelPackageDataList.Clear();
		DataManager.mSignInWeekDataList.Clear();
		DataManager.mDailyActiveDataList.Clear();
		DataManager.mDailyActiveRewardDataList.Clear();
		DataManager.mModelPartDataList.Clear();
		DataManager.mAnnounceDataList.Clear();
		DataManager.mShopDataList.Clear();
		DataManager.mStrongerDataList.Clear();
		DataManager.mLoadingUIDataList.Clear();
		DataManager.mTimerActivityTipsDataList.Clear();
		DataManager.mNotifyDataList.Clear();
		DataManager.mLevelRewardDataList.Clear();
		DataManager.mMapMissionDataDic.Clear();
		DataManager.mActivityMapDataByMapIdDic.Clear();
		DataManager.mActivityMapDataByTypeDic.Clear();
		DataManager.mSideMissionList.Clear();
		if (DataManager.mConfigDataDic != null)
		{
			DataManager.mConfigDataDic.Clear();
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00057968 File Offset: 0x00055B68
	public static List<SceneComponentData> GetSceneComponentDataListById(string mapId)
	{
		if (DataManager.mSceneComponentDataDict != null && DataManager.mSceneComponentDataDict.Count == 0)
		{
			DataManager.mSceneComponentDataDict = DataReader.LoadTableList<string, SceneComponentData>("SceneComponentData", "MapID");
		}
		return DataReader.GetTableList<string, SceneComponentData>(DataManager.mSceneComponentDataDict, mapId);
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x000579B0 File Offset: 0x00055BB0
	public static SkillData GetSkillDataById(string id)
	{
		if (DataManager.mSkillDataDict.Count == 0)
		{
			DataManager.mSkillDataDict = DataReader.LoadTable<string, SkillData>("SkillData", "ID");
		}
		return DataReader.GetTableRow<string, SkillData>(DataManager.mSkillDataDict, id);
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x000579EC File Offset: 0x00055BEC
	public static EffInfoData GetEffInfoDataById(string id)
	{
		if (DataManager.mEffInfoDataDict.Count == 0)
		{
			DataManager.mEffInfoDataDict = DataReader.LoadTable<string, EffInfoData>("EffInfoData", "ID");
		}
		return DataReader.GetTableRow<string, EffInfoData>(DataManager.mEffInfoDataDict, id);
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00057A28 File Offset: 0x00055C28
	public static ActionData GetActionDataByName(string name)
	{
		if (DataManager.mActionDataDict.Count == 0)
		{
			DataManager.mActionDataDict = DataReader.LoadTable<string, ActionData>("ActionData", "ID");
		}
		return DataReader.GetTableRow<string, ActionData>(DataManager.mActionDataDict, name);
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00057A64 File Offset: 0x00055C64
	public static CharacterModelData GetCharacterModelDataByID(string id)
	{
		if (DataManager.mCharacterModelDataDict.Count == 0)
		{
			DataManager.mCharacterModelDataDict = DataReader.LoadTable<string, CharacterModelData>("CharacterModelData", "ID");
		}
		return DataReader.GetTableRow<string, CharacterModelData>(DataManager.mCharacterModelDataDict, id);
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00057AA0 File Offset: 0x00055CA0
	public static ModelData GetModeDataByID(string id)
	{
		if (DataManager.mModeDataDict.Count == 0)
		{
			DataManager.mModeDataDict = DataReader.LoadTable<string, ModelData>("ModelData", "ID");
		}
		return DataReader.GetTableRow<string, ModelData>(DataManager.mModeDataDict, id);
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00057ADC File Offset: 0x00055CDC
	public static BuffInfoData GetBuffInfoDataByID(string id)
	{
		if (DataManager.mBuffInfoDataDict.Count == 0)
		{
			DataManager.mBuffInfoDataDict = DataReader.LoadTable<string, BuffInfoData>("BuffInfoData", "ID");
		}
		return DataReader.GetTableRow<string, BuffInfoData>(DataManager.mBuffInfoDataDict, id);
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00057B18 File Offset: 0x00055D18
	public static Dictionary<string, NpcData> NpcDataDict
	{
		get
		{
			return DataManager.mNpcDataDict;
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00057B20 File Offset: 0x00055D20
	public static NpcData GetNpcDataByID(string id)
	{
		if (DataManager.mNpcDataDict.Count == 0)
		{
			DataManager.mNpcDataDict = DataReader.LoadTable<string, NpcData>("NpcData", "ID");
		}
		return DataReader.GetTableRow<string, NpcData>(DataManager.mNpcDataDict, id);
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00057B5C File Offset: 0x00055D5C
	public static List<MapInfoData> GetMapInfoDataListByType(MAPTYPE type)
	{
		if (DataManager.mMapInfoTypeDataDict.ContainsKey(type))
		{
			return DataManager.mMapInfoTypeDataDict[type];
		}
		Dictionary<string, MapInfoData> allMapInfo = DataManager.GetAllMapInfo();
		List<MapInfoData> list = new List<MapInfoData>();
		foreach (KeyValuePair<string, MapInfoData> keyValuePair in allMapInfo)
		{
			if (keyValuePair.Value.MapType == type)
			{
				list.Add(keyValuePair.Value);
			}
		}
		if (list.Count > 0)
		{
			DataManager.mMapInfoTypeDataDict.Add(type, list);
			return list;
		}
		return null;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00057C18 File Offset: 0x00055E18
	public static MapInfoData GetMapInfoDataByID(string id)
	{
		if (DataManager.mMapInfoDataDict.Count == 0)
		{
			DataManager.mMapInfoDataDict = DataReader.LoadTable<string, MapInfoData>("MapInfoData", "ID");
		}
		return DataReader.GetTableRow<string, MapInfoData>(DataManager.mMapInfoDataDict, id);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00057C54 File Offset: 0x00055E54
	public static MapInfoData GetMapInfoDataByID(GameDefine.SCENE_DEFINE sceneId)
	{
		int num = (int)sceneId;
		return DataManager.GetMapInfoDataByID(num.ToString());
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00057C70 File Offset: 0x00055E70
	public static Dictionary<string, MapInfoData> GetAllMapInfo()
	{
		if (DataManager.mMapInfoDataDict.Count == 0)
		{
			DataManager.mMapInfoDataDict = DataReader.LoadTable<string, MapInfoData>("MapInfoData", "ID");
		}
		return DataManager.mMapInfoDataDict;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00057CA8 File Offset: 0x00055EA8
	public static List<FxEffInfoData> GetFxEffInfoDataListById(string id)
	{
		if (DataManager.mFxEffInfoDataDict.Count == 0)
		{
			DataManager.mFxEffInfoDataDict = DataReader.LoadTableList<string, FxEffInfoData>("FxEffectData", "ID");
		}
		return DataReader.GetTableList<string, FxEffInfoData>(DataManager.mFxEffInfoDataDict, id);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00057CE4 File Offset: 0x00055EE4
	public static List<NPCPathData> GetNPCPathDataListById(string id)
	{
		if (DataManager.mNPCPathDataDic.Count == 0)
		{
			DataManager.mNPCPathDataDic = DataReader.LoadTableList<string, NPCPathData>("NPCPathData", "ID");
		}
		return DataReader.GetTableList<string, NPCPathData>(DataManager.mNPCPathDataDic, id);
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x00057D20 File Offset: 0x00055F20
	public static List<MonsterData> GetMonsterDataListByMapId(string mapId)
	{
		if (DataManager.mMonsterDataDic.Count == 0)
		{
			DataManager.mMonsterDataDic = DataReader.LoadTableList<string, MonsterData>("MonsterData", "MapID");
		}
		return DataReader.GetTableList<string, MonsterData>(DataManager.mMonsterDataDic, mapId);
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00057D5C File Offset: 0x00055F5C
	public static Vector3 GetNPCPosInMonsterData(string mapId, string npcId)
	{
		List<MonsterData> monsterDataListByMapId = DataManager.GetMonsterDataListByMapId(mapId);
		for (int i = 0; i < monsterDataListByMapId.Count; i++)
		{
			if (monsterDataListByMapId[i].NpcID.Equals(npcId))
			{
				return monsterDataListByMapId[i].GetNpcPos();
			}
		}
		return Vector3.zero;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00057DB0 File Offset: 0x00055FB0
	public static bool GetNPCPosInMonsterData2(string mapId, string npcId, out Vector3 pos)
	{
		pos = Vector3.zero;
		List<MonsterData> monsterDataListByMapId = DataManager.GetMonsterDataListByMapId(mapId);
		for (int i = 0; i < monsterDataListByMapId.Count; i++)
		{
			if (monsterDataListByMapId[i].NpcID.Equals(npcId))
			{
				pos = monsterDataListByMapId[i].GetNpcPos();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00057E14 File Offset: 0x00056014
	public static ItemData GetItemDataByID(string id)
	{
		if (DataManager.mItemDataDict.Count == 0)
		{
			DataManager.mItemDataDict = DataReader.LoadTable<string, ItemData>("ItemData", "ID");
		}
		return DataReader.GetTableRow<string, ItemData>(DataManager.mItemDataDict, id);
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00057E50 File Offset: 0x00056050
	public static List<string> GetLvAutoAcceptMissionListByLevel(int lv)
	{
		if (DataManager.mLvAutoAcceptMissionDic.ContainsKey(lv))
		{
			return DataManager.mLvAutoAcceptMissionDic[lv];
		}
		return null;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00057E70 File Offset: 0x00056070
	public static MissionData GetMissionDataByID(string id)
	{
		if (DataManager.mMissionDataDict.Count == 0)
		{
			DataManager.mMissionDataDict = DataReader.LoadTable<string, MissionData>("MissionData", "ID");
		}
		return DataReader.GetTableRow<string, MissionData>(DataManager.mMissionDataDict, id);
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00057EAC File Offset: 0x000560AC
	public static List<MissionData> GetAcceptMissionDataByMapId(string mapId)
	{
		if (DataManager.mMapMissionDataDic.Count == 0)
		{
			List<MissionData> list = new List<MissionData>(DataManager.mMissionDataDict.Values);
			for (int i = 0; i < list.Count; i++)
			{
				if (DataManager.mMapMissionDataDic.ContainsKey(list[i].AcceptMapId))
				{
					DataManager.mMapMissionDataDic[list[i].AcceptMapId].Add(list[i]);
				}
				else
				{
					DataManager.mMapMissionDataDic.Add(list[i].AcceptMapId, new List<MissionData>());
					DataManager.mMapMissionDataDic[list[i].AcceptMapId].Add(list[i]);
				}
			}
		}
		if (DataManager.mMapMissionDataDic.ContainsKey(mapId))
		{
			return DataManager.mMapMissionDataDic[mapId];
		}
		return null;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00057F8C File Offset: 0x0005618C
	public static List<MissionData> GetAllSideMissionList()
	{
		if (DataManager.mSideMissionList.Count == 0)
		{
			List<MissionData> list = new List<MissionData>(DataManager.mMissionDataDict.Values);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Class == 2 || list[i].Class == 8)
				{
					DataManager.mSideMissionList.Add(list[i]);
				}
			}
		}
		return DataManager.mSideMissionList;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0005800C File Offset: 0x0005620C
	public static NPCDialogData GetNPCDialogDataByID(string id)
	{
		if (DataManager.mNPCDialogDataDict.Count == 0)
		{
			DataManager.mNPCDialogDataDict = DataReader.LoadTable<string, NPCDialogData>("NPCDialogData", "ID");
		}
		return DataReader.GetTableRow<string, NPCDialogData>(DataManager.mNPCDialogDataDict, id);
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x00058048 File Offset: 0x00056248
	public static MissionRequireData GetMissionRequireDataByID(string id)
	{
		if (DataManager.mMissionRequireDataDict.Count == 0)
		{
			DataManager.mMissionRequireDataDict = DataReader.LoadTable<string, MissionRequireData>("MissionRequireData", "ID");
		}
		return DataReader.GetTableRow<string, MissionRequireData>(DataManager.mMissionRequireDataDict, id);
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00058084 File Offset: 0x00056284
	public static NpcOptionDialogData GetNpcOptionDialogDataByID(string id)
	{
		if (DataManager.mNpcOptionDialogDataDict.Count == 0)
		{
			DataManager.mNpcOptionDialogDataDict = DataReader.LoadTable<string, NpcOptionDialogData>("NpcOptionDialogData", "ID");
		}
		return DataReader.GetTableRow<string, NpcOptionDialogData>(DataManager.mNpcOptionDialogDataDict, id);
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x000580C0 File Offset: 0x000562C0
	public static List<MapConnectInfoData> GetMapConnectData()
	{
		if (DataManager.mMapConnectInfoDataDic.Count == 0)
		{
			DataManager.mMapConnectInfoDataDic = DataReader.LoadTable<string, MapConnectInfoData>("MapConnectInfoData", "ID");
		}
		return new List<MapConnectInfoData>(DataManager.mMapConnectInfoDataDic.Values);
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x00058100 File Offset: 0x00056300
	public static EquipData GetEquipDataById(string id)
	{
		if (DataManager.mEquipDataDic.Count == 0)
		{
			DataManager.mEquipDataDic = DataReader.LoadTable<string, EquipData>("EquipData", "ID");
		}
		return DataReader.GetTableRow<string, EquipData>(DataManager.mEquipDataDic, id);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0005813C File Offset: 0x0005633C
	public static SkillupgradeData GetSkillupgradeDataByLevel(int level)
	{
		if (!GameManager.IsSupportCurDataVersion184())
		{
			return null;
		}
		if (DataManager.mSkillupgradeDataDic.Count == 0)
		{
			DataManager.mSkillupgradeDataDic = DataReader.LoadTable<int, SkillupgradeData>("SkillupgradeData", "level");
		}
		return DataReader.GetTableRow<int, SkillupgradeData>(DataManager.mSkillupgradeDataDic, level);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00058184 File Offset: 0x00056384
	public static CopySceneData GetCopySceneDataById(string id)
	{
		if (DataManager.mCopySceneDataDic.Count == 0)
		{
			DataManager.mCopySceneDataDic = DataReader.LoadTable<string, CopySceneData>("CopySceneData", "ID");
		}
		return DataReader.GetTableRow<string, CopySceneData>(DataManager.mCopySceneDataDic, id);
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x000581C0 File Offset: 0x000563C0
	public static List<CopySceneData> GetCopySceneDataByType(int type)
	{
		if (DataManager.mCopySceneDataDic.Count == 0)
		{
			DataManager.mCopySceneDataDic = DataReader.LoadTable<string, CopySceneData>("CopySceneData", "ID");
		}
		if (DataManager.mCopySceneDataList.Count == 0)
		{
			DataManager.mCopySceneDataList = new List<CopySceneData>(DataManager.mCopySceneDataDic.Values);
		}
		List<CopySceneData> list = new List<CopySceneData>();
		for (int i = 0; i < DataManager.mCopySceneDataList.Count; i++)
		{
			if (DataManager.mCopySceneDataList[i].SubType == type)
			{
				list.Add(DataManager.mCopySceneDataList[i]);
			}
		}
		list.Sort((CopySceneData x, CopySceneData y) => x.ID.CompareTo(y.ID));
		return list;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00058280 File Offset: 0x00056480
	public static List<KeyValuePair<string, CopySceneData>> GetCopySceneDataList()
	{
		if (DataManager.mCopySceneDataDic.Count == 0)
		{
			DataManager.mCopySceneDataDic = DataReader.LoadTable<string, CopySceneData>("CopySceneData", "ID");
		}
		return new List<KeyValuePair<string, CopySceneData>>(DataManager.mCopySceneDataDic);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x000582B0 File Offset: 0x000564B0
	public static LadderRewardData GetLadderRewardDataByRank(int rank)
	{
		if (DataManager.mLadderRewardDataDic.Count == 0)
		{
			DataManager.mLadderRewardDataDic = DataReader.LoadTable<string, LadderRewardData>("LadderRewardData", "ID");
		}
		if (DataManager.mLadderRewardDataList.Count == 0)
		{
			DataManager.mLadderRewardDataList = new List<LadderRewardData>(DataManager.mLadderRewardDataDic.Values);
		}
		for (int i = 0; i < DataManager.mLadderRewardDataList.Count; i++)
		{
			if (DataManager.mLadderRewardDataList[i].Rankdown <= rank && rank <= DataManager.mLadderRewardDataList[i].Rankup)
			{
				return DataManager.mLadderRewardDataList[i];
			}
		}
		return null;
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00058358 File Offset: 0x00056558
	public static List<LadderRewardData> GetLadderRewardDataList()
	{
		if (DataManager.mLadderRewardDataDic.Count == 0)
		{
			DataManager.mLadderRewardDataDic = DataReader.LoadTable<string, LadderRewardData>("LadderRewardData", "ID");
		}
		if (DataManager.mLadderRewardDataList.Count == 0)
		{
			DataManager.mLadderRewardDataList = new List<LadderRewardData>(DataManager.mLadderRewardDataDic.Values);
		}
		return DataManager.mLadderRewardDataList;
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x000583B0 File Offset: 0x000565B0
	public static BaseLvData GetLevelDataByLevel(int level)
	{
		if (DataManager.mBaseLvDataDic.Count == 0)
		{
			DataManager.mBaseLvDataDic = DataReader.LoadTable<int, BaseLvData>("BaseLvData", "Lv");
		}
		return DataReader.GetTableRow<int, BaseLvData>(DataManager.mBaseLvDataDic, level);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x000583EC File Offset: 0x000565EC
	public static TitleData GetTitleDateById(string id)
	{
		if (DataManager.mTitleDateDic.Count == 0)
		{
			DataManager.mTitleDateDic = DataReader.LoadTable<string, TitleData>("TitleData", "ID");
		}
		if (DataManager.mTitleList.Count == 0)
		{
			DataManager.mTitleList = new List<TitleData>(DataManager.mTitleDateDic.Values);
		}
		return DataReader.GetTableRow<string, TitleData>(DataManager.mTitleDateDic, id);
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x0005844C File Offset: 0x0005664C
	public static List<TitleData> GetTitleList()
	{
		if (DataManager.mTitleDateDic.Count == 0)
		{
			DataManager.mTitleDateDic = DataReader.LoadTable<string, TitleData>("TitleData", "ID");
		}
		if (DataManager.mTitleList.Count == 0)
		{
			DataManager.mTitleList = new List<TitleData>(DataManager.mTitleDateDic.Values);
		}
		return DataManager.mTitleList;
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x000584A4 File Offset: 0x000566A4
	public static List<StoryData> GetStoryDataListById(string id)
	{
		if (DataManager.mStoryDataDic.Count == 0)
		{
			DataManager.mStoryDataDic = DataReader.LoadTableList<string, StoryData>("StoryData", "ID");
		}
		return DataReader.GetTableList<string, StoryData>(DataManager.mStoryDataDic, id);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x000584E0 File Offset: 0x000566E0
	public static List<EquipmentUpgradeData> GetEquipmentUpgradeDataListByPartID(int pos)
	{
		if (DataManager.mEquipmentUpgradeDataDic.Count == 0)
		{
			DataManager.mEquipmentUpgradeDataDic = DataReader.LoadTableList<int, EquipmentUpgradeData>("EquipmentUpgradeData", "PartID");
		}
		return DataReader.GetTableList<int, EquipmentUpgradeData>(DataManager.mEquipmentUpgradeDataDic, pos);
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0005851C File Offset: 0x0005671C
	public static DamageBoardTypeData GetDamageBoardTypeDataById(int id)
	{
		if (DataManager.mDamageBoardTypeDataDic.Count == 0)
		{
			DataManager.mDamageBoardTypeDataDic = DataReader.LoadTable<int, DamageBoardTypeData>("DamageBoardTypeData", "ID");
		}
		return DataReader.GetTableRow<int, DamageBoardTypeData>(DataManager.mDamageBoardTypeDataDic, id);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x00058558 File Offset: 0x00056758
	public static List<CamRockCurveData> GetCamRockCurveDataListByName(string name)
	{
		if (DataManager.mCamRockCurveDataDic.Count == 0)
		{
			DataManager.mCamRockCurveDataDic = DataReader.LoadTableList<string, CamRockCurveData>("CamRockCurveData", "AnimaClipName");
		}
		return DataReader.GetTableList<string, CamRockCurveData>(DataManager.mCamRockCurveDataDic, name);
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00058594 File Offset: 0x00056794
	public static CamRockData GetCamRockDataByID(string id)
	{
		if (DataManager.mCamRockDataDic.Count == 0)
		{
			DataManager.mCamRockDataDic = DataReader.LoadTable<string, CamRockData>("CamRockData", "ID");
		}
		return DataReader.GetTableRow<string, CamRockData>(DataManager.mCamRockDataDic, id);
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x000585D0 File Offset: 0x000567D0
	public static List<SurveyMissionData> GetSurveyMissionDataBySceneId(string sceneId)
	{
		if (DataManager.mSurveyMissionDataDic.Count == 0)
		{
			DataManager.mSurveyMissionDataDic = DataReader.LoadTable<string, SurveyMissionData>("SurveyMissionData", "ID");
		}
		if (DataManager.mSurveyMissionDataList.Count == 0)
		{
			DataManager.mSurveyMissionDataList = new List<SurveyMissionData>(DataManager.mSurveyMissionDataDic.Values);
		}
		List<SurveyMissionData> list = new List<SurveyMissionData>();
		for (int i = 0; i < DataManager.mSurveyMissionDataList.Count; i++)
		{
			if (DataManager.mSurveyMissionDataList[i].SceneID.Equals(sceneId))
			{
				list.Add(DataManager.mSurveyMissionDataList[i]);
			}
		}
		return list;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00058674 File Offset: 0x00056874
	public static SurveyMissionData GetSurveyMissionDataById(string id)
	{
		if (DataManager.mSurveyMissionDataDic.Count == 0)
		{
			DataManager.mSurveyMissionDataDic = DataReader.LoadTable<string, SurveyMissionData>("SurveyMissionData", "ID");
		}
		return DataReader.GetTableRow<string, SurveyMissionData>(DataManager.mSurveyMissionDataDic, id);
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x000586B0 File Offset: 0x000568B0
	public static List<MultiDeliveryMissionData> GetMultiDeliveryMissionDataListById(string id)
	{
		if (DataManager.mMultiDeliveryMissionDataDic.Count == 0)
		{
			DataManager.mMultiDeliveryMissionDataDic = DataReader.LoadTableList<string, MultiDeliveryMissionData>("MultiDeliveryMissionData", "ID");
		}
		return DataReader.GetTableList<string, MultiDeliveryMissionData>(DataManager.mMultiDeliveryMissionDataDic, id);
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x000586EC File Offset: 0x000568EC
	public static DailyMissionData GetDailyMissionDataById(string id)
	{
		if (DataManager.mDailyMissionDataDic.Count == 0)
		{
			DataManager.mDailyMissionDataDic = DataReader.LoadTable<string, DailyMissionData>("DailyMissionData", "ID");
		}
		return DataReader.GetTableRow<string, DailyMissionData>(DataManager.mDailyMissionDataDic, id);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00058728 File Offset: 0x00056928
	public static List<RefineData> GetRefineDataListByPart(int partId)
	{
		if (DataManager.mRefineDataDic.Count == 0)
		{
			DataManager.mRefineDataDic = DataReader.LoadTableList<int, RefineData>("RefineData", "Part");
		}
		return DataReader.GetTableList<int, RefineData>(DataManager.mRefineDataDic, partId);
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00058764 File Offset: 0x00056964
	public static RefineData GetRefineDataByPartLevelPRO(int part, int level, int profession)
	{
		List<RefineData> refineDataListByPart = DataManager.GetRefineDataListByPart(part);
		for (int i = 0; i < refineDataListByPart.Count; i++)
		{
			if (refineDataListByPart[i].Job == profession && refineDataListByPart[i].Lv == level)
			{
				return refineDataListByPart[i];
			}
		}
		return null;
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x000587BC File Offset: 0x000569BC
	public static RefineLevelData GetRefineLevelDataById(int id)
	{
		if (DataManager.mRefineLevelDataDic.Count == 0)
		{
			DataManager.mRefineLevelDataDic = DataReader.LoadTable<int, RefineLevelData>("RefineLevelData", "ID");
		}
		return DataReader.GetTableRow<int, RefineLevelData>(DataManager.mRefineLevelDataDic, id);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x000587F8 File Offset: 0x000569F8
	public static List<CarMissionBlockData> GetCarMissionBlockDataListBySceneId(string sceneid)
	{
		if (DataManager.mCarMissionBlockDataDic.Count == 0)
		{
			DataManager.mCarMissionBlockDataDic = DataReader.LoadTableList<string, CarMissionBlockData>("CarMissionBlockData", "SceneID");
		}
		return DataReader.GetTableList<string, CarMissionBlockData>(DataManager.mCarMissionBlockDataDic, sceneid);
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x00058834 File Offset: 0x00056A34
	public static List<ConsignBuyTabData> GetConsignBuyTabDataListById(int id)
	{
		if (DataManager.mConsignBuyTabDataDic.Count == 0)
		{
			DataManager.mConsignBuyTabDataDic = DataReader.LoadTableList<int, ConsignBuyTabData>("ConsignBuyTabData", "TopTabId");
		}
		return DataReader.GetTableList<int, ConsignBuyTabData>(DataManager.mConsignBuyTabDataDic, id);
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00058870 File Offset: 0x00056A70
	public static List<List<ConsignBuyTabData>> GetConsignBuyTabData()
	{
		if (DataManager.mConsignBuyTabDataDic.Count == 0)
		{
			DataManager.mConsignBuyTabDataDic = DataReader.LoadTableList<int, ConsignBuyTabData>("ConsignBuyTabData", "TopTabId");
		}
		List<List<ConsignBuyTabData>> list = new List<List<ConsignBuyTabData>>();
		foreach (List<ConsignBuyTabData> list2 in DataManager.mConsignBuyTabDataDic.Values)
		{
			list.Add(list2);
		}
		return list;
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x00058904 File Offset: 0x00056B04
	public static List<SneakingMissionData> GetSneakingMissionDataListByMapId(string mapId)
	{
		if (DataManager.mSneakingMissionDataDic.Count == 0)
		{
			DataManager.mSneakingMissionDataDic = DataReader.LoadTableList<string, SneakingMissionData>("SneakingMissionData", "MapID");
		}
		return DataReader.GetTableList<string, SneakingMissionData>(DataManager.mSneakingMissionDataDic, mapId);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00058940 File Offset: 0x00056B40
	public static BadgeData GetBadgeDataById(string id)
	{
		if (DataManager.mBadgeDataDic.Count == 0)
		{
			DataManager.mBadgeDataDic = DataReader.LoadTable<string, BadgeData>("BadgeData", "ID");
		}
		return DataReader.GetTableRow<string, BadgeData>(DataManager.mBadgeDataDic, id);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0005897C File Offset: 0x00056B7C
	public static void GetBadgeTypeByColor(int color, List<int> typeList, List<string> nameList)
	{
		if (DataManager.BadgeDataList == null)
		{
			DataManager.BadgeDataList = new List<BadgeData>(DataManager.mBadgeDataDic.Values);
		}
		for (int i = 0; i < DataManager.BadgeDataList.Count; i++)
		{
			if (DataManager.BadgeDataList[i].Color == color && !typeList.Contains(DataManager.BadgeDataList[i].BadgeType))
			{
				typeList.Add(DataManager.BadgeDataList[i].BadgeType);
				nameList.Add(DataManager.GetItemDataByID(DataManager.BadgeDataList[i].ID).Name);
			}
		}
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00058A2C File Offset: 0x00056C2C
	public static TowerData GetTowerDataByFloorID(int id)
	{
		if (DataManager.mTowerDataDic.Count == 0)
		{
			DataManager.mTowerDataDic = DataReader.LoadTable<int, TowerData>("TowerData", "FloorID");
		}
		return DataReader.GetTableRow<int, TowerData>(DataManager.mTowerDataDic, id);
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00058A68 File Offset: 0x00056C68
	public static Dictionary<int, TowerData> TowerDataDic
	{
		get
		{
			if (DataManager.mTowerDataDic.Count == 0)
			{
				DataManager.mTowerDataDic = DataReader.LoadTable<int, TowerData>("TowerData", "FloorID");
			}
			return DataManager.mTowerDataDic;
		}
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x00058AA0 File Offset: 0x00056CA0
	public static AIData GetAIDataByID(string id)
	{
		if (DataManager.mAIDataDic.Count == 0)
		{
			DataManager.mAIDataDic = DataReader.LoadTable<string, AIData>("AIData", "ID");
		}
		return DataReader.GetTableRow<string, AIData>(DataManager.mAIDataDic, id);
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00058ADC File Offset: 0x00056CDC
	public static WildBossData GetWildBossDataByID(string id)
	{
		if (DataManager.mWildBossDataDic.Count == 0)
		{
			DataManager.mWildBossDataDic = DataReader.LoadTable<string, WildBossData>("WildBossData", "ID");
		}
		return DataReader.GetTableRow<string, WildBossData>(DataManager.mWildBossDataDic, id);
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00058B18 File Offset: 0x00056D18
	public static GuildBossData GetGuildBossDataByID(string id)
	{
		if (DataManager.mGuildBossDataDic.Count == 0)
		{
			DataManager.mGuildBossDataDic = DataReader.LoadTable<string, GuildBossData>("GuildBossData", "ID");
		}
		return DataReader.GetTableRow<string, GuildBossData>(DataManager.mGuildBossDataDic, id);
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00058B54 File Offset: 0x00056D54
	public static BarFightCopyData GetBarFightCopyDataByID(string id)
	{
		if (DataManager.mBarFightCopyDataDic.Count == 0)
		{
			DataManager.mBarFightCopyDataDic = DataReader.LoadTable<string, BarFightCopyData>("BarFightCopyData", "ID");
		}
		return DataReader.GetTableRow<string, BarFightCopyData>(DataManager.mBarFightCopyDataDic, id);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00058B90 File Offset: 0x00056D90
	public static GuildDonateData GetGuildDonateDataByID(string id)
	{
		if (DataManager.mGuildDonateDataDic.Count == 0)
		{
			DataManager.mGuildDonateDataDic = DataReader.LoadTable<string, GuildDonateData>("GuildDonateData", "ID");
		}
		return DataReader.GetTableRow<string, GuildDonateData>(DataManager.mGuildDonateDataDic, id);
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00058BCC File Offset: 0x00056DCC
	public static List<GuildLevelData> GetGuildLevelDataList()
	{
		if (DataManager.mGuildLevelDataList.Count == 0)
		{
			DataManager.mGuildLevelDataList = DataReader.LoadImportData<GuildLevelData>("GuildLevelData");
		}
		return DataManager.mGuildLevelDataList;
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00058BF4 File Offset: 0x00056DF4
	public static GuildLevelData GetGuildLevelDataByLevel(int level)
	{
		if (DataManager.mGuildLevelDataList.Count == 0)
		{
			DataManager.mGuildLevelDataList = DataReader.LoadImportData<GuildLevelData>("GuildLevelData");
		}
		if (DataManager.mGuildLevelDataList.Count > level)
		{
			return DataManager.mGuildLevelDataList[level];
		}
		return null;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00058C3C File Offset: 0x00056E3C
	public static List<GuildSkillData> GetGuildSkillDataListByType(int type)
	{
		if (DataManager.mGuildSkillDataDic.Count == 0)
		{
			DataManager.mGuildSkillDataDic = DataReader.LoadTableList<int, GuildSkillData>("GuildSkillData", "GuildSkillType");
		}
		return DataReader.GetTableList<int, GuildSkillData>(DataManager.mGuildSkillDataDic, type);
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00058C78 File Offset: 0x00056E78
	public static GuildSkillData GetGuildSkillDataByTypeLevel(int type, int level)
	{
		List<GuildSkillData> guildSkillDataListByType = DataManager.GetGuildSkillDataListByType(type);
		if (level < guildSkillDataListByType.Count)
		{
			return guildSkillDataListByType[level];
		}
		return null;
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00058CA4 File Offset: 0x00056EA4
	public static TeamData GetTeamDataDataByID(string id)
	{
		if (DataManager.mTeamDataDic.Count == 0)
		{
			DataManager.mTeamDataDic = DataReader.LoadTable<string, TeamData>("TeamData", "ID");
		}
		return DataReader.GetTableRow<string, TeamData>(DataManager.mTeamDataDic, id);
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x00058CE0 File Offset: 0x00056EE0
	public static List<TeamData> GetTeamDataList()
	{
		if (DataManager.mTeamDataDic.Count == 0)
		{
			DataManager.mTeamDataDic = DataReader.LoadTable<string, TeamData>("TeamData", "ID");
		}
		return new List<TeamData>(DataManager.mTeamDataDic.Values);
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00058D20 File Offset: 0x00056F20
	public static ShowRewardData GetShowRewardDataByID(string id)
	{
		if (DataManager.mShowRewardDataDic.Count == 0)
		{
			DataManager.mShowRewardDataDic = DataReader.LoadTable<string, ShowRewardData>("ShowRewardData", "ID");
		}
		return DataReader.GetTableRow<string, ShowRewardData>(DataManager.mShowRewardDataDic, id);
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00058D5C File Offset: 0x00056F5C
	public static AdaptData GetAdaptDataByID(int id)
	{
		if (DataManager.mAdaptDataDic.Count == 0)
		{
			DataManager.mAdaptDataDic = DataReader.LoadDicTable<int, AdaptData>("AdaptData", "ID", "DorpKeyDic");
		}
		return DataReader.GetTableRow<int, AdaptData>(DataManager.mAdaptDataDic, id);
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x00058D94 File Offset: 0x00056F94
	public static FunctionData GetFunctionDataById(string id)
	{
		if (DataManager.mFunctionDataDic.Count == 0)
		{
			DataManager.mFunctionDataDic = DataReader.LoadTable<string, FunctionData>("FunctionData", "ID");
		}
		return DataReader.GetTableRow<string, FunctionData>(DataManager.mFunctionDataDic, id);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00058DD0 File Offset: 0x00056FD0
	public static List<FunctionData> GetFunctionDataList()
	{
		if (DataManager.mFunctionDataDic.Count == 0)
		{
			DataManager.mFunctionDataDic = DataReader.LoadTable<string, FunctionData>("FunctionData", "ID");
		}
		return new List<FunctionData>(DataManager.mFunctionDataDic.Values);
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x00058E10 File Offset: 0x00057010
	public static SoundData GetSoundDataById(int id)
	{
		if (DataManager.mSoundDataDic == null || DataManager.mSoundDataDic.Count == 0)
		{
			DataManager.mSoundDataDic = DataReader.LoadTable<int, SoundData>("SoundData", "Id");
		}
		return DataReader.GetTableRow<int, SoundData>(DataManager.mSoundDataDic, id);
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x00058E58 File Offset: 0x00057058
	public static List<MapAreaInfoData> GetMapAreaInfoDataListById(string id)
	{
		if (DataManager.mMapAreaInfoDataDic.Count == 0)
		{
			DataManager.mMapAreaInfoDataDic = DataReader.LoadTableList<string, MapAreaInfoData>("MapAreaInfoData", "ID");
		}
		return DataReader.GetTableList<string, MapAreaInfoData>(DataManager.mMapAreaInfoDataDic, id);
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00058E94 File Offset: 0x00057094
	public static EscortData GetEscortDataById(string id)
	{
		if (DataManager.mEscortDataDic.Count == 0)
		{
			DataManager.mEscortDataDic = DataReader.LoadTable<string, EscortData>("EscortData", "ID");
		}
		return DataReader.GetTableRow<string, EscortData>(DataManager.mEscortDataDic, id);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00058ED0 File Offset: 0x000570D0
	public static CityDanceData GetCityDanceDataById(string id)
	{
		if (DataManager.mCityDanceDataDic == null || DataManager.mCityDanceDataDic.Count == 0)
		{
			DataManager.mCityDanceDataDic = DataReader.LoadTable<string, CityDanceData>("CityDanceData", "ID");
		}
		return DataReader.GetTableRow<string, CityDanceData>(DataManager.mCityDanceDataDic, id);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00058F18 File Offset: 0x00057118
	public static List<CityDanceData> GetCityDanceDataList()
	{
		if (DataManager.mCityDanceDataDic.Count == 0)
		{
			DataManager.mCityDanceDataDic = DataReader.LoadTable<string, CityDanceData>("CityDanceData", "ID");
		}
		return new List<CityDanceData>(DataManager.mCityDanceDataDic.Values);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00058F58 File Offset: 0x00057158
	public static MountData GetMountDataById(string id)
	{
		if (DataManager.mMountDataDic.Count == 0)
		{
			DataManager.mMountDataDic = DataReader.LoadTable<string, MountData>("MountData", "ID");
		}
		return DataReader.GetTableRow<string, MountData>(DataManager.mMountDataDic, id);
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00058F94 File Offset: 0x00057194
	public static ColorData GetColorDataById(string id)
	{
		if (DataManager.mColorDataDic.Count == 0)
		{
			DataManager.mColorDataDic = DataReader.LoadTable<string, ColorData>("ColorData", "ID");
		}
		return DataReader.GetTableRow<string, ColorData>(DataManager.mColorDataDic, id);
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00058FD0 File Offset: 0x000571D0
	public static DanceData GetDanceDataById(string id)
	{
		if (DataManager.mDanceDataDic.Count == 0)
		{
			DataManager.mDanceDataDic = DataReader.LoadTable<string, DanceData>("DanceData", "ID");
		}
		return DataReader.GetTableRow<string, DanceData>(DataManager.mDanceDataDic, id);
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0005900C File Offset: 0x0005720C
	public static SlotIconData GetSlotIconDataById(string id)
	{
		if (DataManager.mSlotIconDataDic.Count == 0)
		{
			DataManager.mSlotIconDataDic = DataReader.LoadTable<string, SlotIconData>("SlotIconData", "ID");
		}
		if (DataManager.mSlotIconList.Count == 0)
		{
			DataManager.mSlotIconList = new List<SlotIconData>(DataManager.mSlotIconDataDic.Values);
		}
		return DataReader.GetTableRow<string, SlotIconData>(DataManager.mSlotIconDataDic, id);
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0005906C File Offset: 0x0005726C
	public static List<SlotIconData> GetSlotIconDataList()
	{
		if (DataManager.mSlotIconDataDic.Count == 0)
		{
			DataManager.mSlotIconDataDic = DataReader.LoadTable<string, SlotIconData>("SlotIconData", "ID");
		}
		if (DataManager.mSlotIconList.Count == 0)
		{
			DataManager.mSlotIconList = new List<SlotIconData>(DataManager.mSlotIconDataDic.Values);
		}
		return DataManager.mSlotIconList;
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000590C4 File Offset: 0x000572C4
	public static SlotAutoData GetSlotAutoDataById(string id)
	{
		if (DataManager.mSlotAutoDataDic.Count == 0)
		{
			DataManager.mSlotAutoDataDic = DataReader.LoadTable<string, SlotAutoData>("SlotAutoData", "ID");
		}
		if (DataManager.mSlotAutoList.Count == 0)
		{
			DataManager.mSlotAutoList = new List<SlotAutoData>(DataManager.mSlotAutoDataDic.Values);
		}
		return DataReader.GetTableRow<string, SlotAutoData>(DataManager.mSlotAutoDataDic, id);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00059124 File Offset: 0x00057324
	public static List<SlotAutoData> GetSlotAutoDataList()
	{
		if (DataManager.mSlotAutoDataDic.Count == 0)
		{
			DataManager.mSlotAutoDataDic = DataReader.LoadTable<string, SlotAutoData>("SlotAutoData", "ID");
		}
		if (DataManager.mSlotAutoList.Count == 0)
		{
			DataManager.mSlotAutoList = new List<SlotAutoData>(DataManager.mSlotAutoDataDic.Values);
		}
		return DataManager.mSlotAutoList;
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0005917C File Offset: 0x0005737C
	public static SurviveBattleData GetSurviveBattleDataById(string id)
	{
		if (DataManager.mSurviveBattleDataDic.Count == 0)
		{
			DataManager.mSurviveBattleDataDic = DataReader.LoadTable<string, SurviveBattleData>("SurviveBattleData", "ID");
		}
		return DataReader.GetTableRow<string, SurviveBattleData>(DataManager.mSurviveBattleDataDic, id);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x000591B8 File Offset: 0x000573B8
	public static ScuffleData GetScuffleDataById(string id)
	{
		if (DataManager.mScuffleDataDic.Count == 0)
		{
			DataManager.mScuffleDataDic = DataReader.LoadTable<string, ScuffleData>("ScuffleData", "ID");
		}
		return DataReader.GetTableRow<string, ScuffleData>(DataManager.mScuffleDataDic, id);
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x000591F4 File Offset: 0x000573F4
	public static SignInMonthData GetSignInMonthDataById(int id)
	{
		if (DataManager.mSignInMonthDataDic.Count == 0)
		{
			DataManager.mSignInMonthDataDic = DataReader.LoadTable<int, SignInMonthData>("SignInMonthData", "Day");
		}
		if (DataManager.mSignInMonthDataList.Count == 0)
		{
			DataManager.mSignInMonthDataList = new List<SignInMonthData>(DataManager.mSignInMonthDataDic.Values);
			DataManager.mSignInMonthDataList.Sort((SignInMonthData x, SignInMonthData y) => x.Day - y.Day);
		}
		return DataReader.GetTableRow<int, SignInMonthData>(DataManager.mSignInMonthDataDic, id);
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0005927C File Offset: 0x0005747C
	public static List<SignInMonthData> GetSignInMonthDataList()
	{
		if (DataManager.mSignInMonthDataDic.Count == 0)
		{
			DataManager.mSignInMonthDataDic = DataReader.LoadTable<int, SignInMonthData>("SignInMonthData", "Day");
		}
		if (DataManager.mSignInMonthDataList.Count == 0)
		{
			DataManager.mSignInMonthDataList = new List<SignInMonthData>(DataManager.mSignInMonthDataDic.Values);
			DataManager.mSignInMonthDataList.Sort((SignInMonthData x, SignInMonthData y) => x.Day - y.Day);
		}
		return DataManager.mSignInMonthDataList;
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x000592FC File Offset: 0x000574FC
	public static DailyBuyData GetDailyBuyDataBuyId(string id)
	{
		if (DataManager.mDailyBuyDataDic.Count == 0)
		{
			DataManager.mDailyBuyDataDic = DataReader.LoadTable<string, DailyBuyData>("DailyBuyData", "ID");
		}
		return DataReader.GetTableRow<string, DailyBuyData>(DataManager.mDailyBuyDataDic, id);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00059338 File Offset: 0x00057538
	public static InvestData GetInvestDataBuyId(string id)
	{
		if (DataManager.mInvestDataDic.Count == 0)
		{
			DataManager.mInvestDataDic = DataReader.LoadTable<string, InvestData>("InvestData", "ID");
		}
		return DataReader.GetTableRow<string, InvestData>(DataManager.mInvestDataDic, id);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00059374 File Offset: 0x00057574
	public static LevelPackageData GetLevelPackageDataBuyId(string id)
	{
		if (DataManager.mLevelPackageDataDic.Count == 0)
		{
			DataManager.mLevelPackageDataDic = DataReader.LoadTable<string, LevelPackageData>("LevelPackageData", "ID");
		}
		return DataReader.GetTableRow<string, LevelPackageData>(DataManager.mLevelPackageDataDic, id);
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x000593B0 File Offset: 0x000575B0
	public static List<LevelPackageData> GetLevelPackageDataList()
	{
		if (DataManager.mLevelPackageDataDic.Count == 0)
		{
			DataManager.mLevelPackageDataDic = DataReader.LoadTable<string, LevelPackageData>("LevelPackageData", "ID");
		}
		if (DataManager.mLevelPackageDataList.Count == 0)
		{
			DataManager.mLevelPackageDataList = new List<LevelPackageData>(DataManager.mLevelPackageDataDic.Values);
		}
		return DataManager.mLevelPackageDataList;
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00059408 File Offset: 0x00057608
	public static RetrieveData GetRetrieveDataBuyId(string id)
	{
		if (DataManager.mRetrieveDataDic.Count == 0)
		{
			DataManager.mRetrieveDataDic = DataReader.LoadTable<string, RetrieveData>("RetrieveData", "ID");
		}
		return DataReader.GetTableRow<string, RetrieveData>(DataManager.mRetrieveDataDic, id);
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00059444 File Offset: 0x00057644
	public static List<SignInWeekData> GetSignInWeekDataList()
	{
		if (DataManager.mSignInWeekDataDic.Count == 0)
		{
			DataManager.mSignInWeekDataDic = DataReader.LoadTable<int, SignInWeekData>("SignInWeekData", "Day");
		}
		if (DataManager.mSignInWeekDataList.Count == 0)
		{
			DataManager.mSignInWeekDataList = new List<SignInWeekData>(DataManager.mSignInWeekDataDic.Values);
			DataManager.mSignInWeekDataList.Sort((SignInWeekData x, SignInWeekData y) => x.Day - y.Day);
		}
		return DataManager.mSignInWeekDataList;
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x000594C4 File Offset: 0x000576C4
	public static DailyActiveData GetDailyActiveDataById(string id)
	{
		if (DataManager.mDailyActiveDataDic.Count == 0)
		{
			DataManager.mDailyActiveDataDic = DataReader.LoadTable<string, DailyActiveData>("DailyActiveData", "ID");
		}
		return DataReader.GetTableRow<string, DailyActiveData>(DataManager.mDailyActiveDataDic, id);
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00059500 File Offset: 0x00057700
	public static List<DailyActiveData> GetDailyActiveDataList()
	{
		if (DataManager.mDailyActiveDataDic.Count == 0)
		{
			DataManager.mDailyActiveDataDic = DataReader.LoadTable<string, DailyActiveData>("DailyActiveData", "ID");
		}
		if (DataManager.mDailyActiveDataList.Count == 0)
		{
			DataManager.mDailyActiveDataList = new List<DailyActiveData>(DataManager.mDailyActiveDataDic.Values);
			DataManager.mDailyActiveDataList.Sort((DailyActiveData x, DailyActiveData y) => int.Parse(x.ID) - int.Parse(y.ID));
		}
		return DataManager.mDailyActiveDataList;
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00059580 File Offset: 0x00057780
	public static DailyActiveRewardData GetDailyActiveRewardDataById(string id)
	{
		if (DataManager.mDailyActiveRewardDataDic.Count == 0)
		{
			DataManager.mDailyActiveRewardDataDic = DataReader.LoadTable<string, DailyActiveRewardData>("DailyActiveRewardData", "ID");
		}
		return DataReader.GetTableRow<string, DailyActiveRewardData>(DataManager.mDailyActiveRewardDataDic, id);
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x000595BC File Offset: 0x000577BC
	public static List<DailyActiveRewardData> GetDailyActiveRewardDataList()
	{
		if (DataManager.mDailyActiveRewardDataDic.Count == 0)
		{
			DataManager.mDailyActiveRewardDataDic = DataReader.LoadTable<string, DailyActiveRewardData>("DailyActiveRewardData", "ID");
		}
		if (DataManager.mDailyActiveRewardDataList.Count == 0)
		{
			DataManager.mDailyActiveRewardDataList = new List<DailyActiveRewardData>(DataManager.mDailyActiveRewardDataDic.Values);
			DataManager.mDailyActiveRewardDataList.Sort((DailyActiveRewardData x, DailyActiveRewardData y) => int.Parse(x.ID) - int.Parse(y.ID));
		}
		return DataManager.mDailyActiveRewardDataList;
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x0005963C File Offset: 0x0005783C
	public static FirstBuyData GetFirstBuyDataById(string id)
	{
		if (DataManager.mFirstBuyDataDic.Count == 0)
		{
			DataManager.mFirstBuyDataDic = DataReader.LoadTable<string, FirstBuyData>("FirstBuyData", "ID");
		}
		return DataReader.GetTableRow<string, FirstBuyData>(DataManager.mFirstBuyDataDic, id);
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00059678 File Offset: 0x00057878
	public static BigPackageData GetBigPackageDataById(string id)
	{
		if (DataManager.mBigPackageDataDic.Count == 0)
		{
			DataManager.mBigPackageDataDic = DataReader.LoadTable<string, BigPackageData>("BigPackageData", "ID");
		}
		return DataReader.GetTableRow<string, BigPackageData>(DataManager.mBigPackageDataDic, id);
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x000596B4 File Offset: 0x000578B4
	public static List<BigPackageTimeListData> GetBigPackageTimeListDataListById(string[] ids)
	{
		if (DataManager.mBigPackageTimeListDataDic.Count == 0)
		{
			DataManager.mBigPackageTimeListDataDic = DataReader.LoadTable<string, BigPackageTimeListData>("BigPackageTimeListData", "ID");
		}
		List<BigPackageTimeListData> list = new List<BigPackageTimeListData>();
		for (int i = 0; i < ids.Length; i++)
		{
			if (DataManager.mBigPackageTimeListDataDic.ContainsKey(ids[i]))
			{
				list.Add(DataManager.mBigPackageTimeListDataDic[ids[i]]);
			}
		}
		return list;
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00059724 File Offset: 0x00057924
	public static PurchaseData GetPurchaseDataBuyId(string id)
	{
		if (DataManager.mPurchaseDataDic.Count == 0)
		{
			DataManager.mPurchaseDataDic = DataReader.LoadTable<string, PurchaseData>("PurchaseData", "ProductId");
		}
		return DataReader.GetTableRow<string, PurchaseData>(DataManager.mPurchaseDataDic, id);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00059760 File Offset: 0x00057960
	public static Dictionary<string, PurchaseData> GetPurchaseData()
	{
		if (DataManager.mPurchaseDataDic.Count == 0)
		{
			DataManager.mPurchaseDataDic = DataReader.LoadTable<string, PurchaseData>("PurchaseData", "ProductId");
		}
		return DataManager.mPurchaseDataDic;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00059798 File Offset: 0x00057998
	public static List<ModelPartData> GetModelPartDataList()
	{
		if (DataManager.mModelPartDataDic.Count == 0)
		{
			DataManager.mModelPartDataDic = DataReader.LoadTable<string, ModelPartData>("ModelPartData", "ID");
		}
		if (DataManager.mModelPartDataList.Count == 0)
		{
			DataManager.mModelPartDataList = new List<ModelPartData>(DataManager.mModelPartDataDic.Values);
		}
		return DataManager.mModelPartDataList;
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x000597F0 File Offset: 0x000579F0
	public static DownloadRewardData GetDownloadRewardDataBuyId(string id)
	{
		if (DataManager.mDownloadRewardDataDic.Count == 0)
		{
			DataManager.mDownloadRewardDataDic = DataReader.LoadTable<string, DownloadRewardData>("DownloadRewardData", "ID");
		}
		return DataReader.GetTableRow<string, DownloadRewardData>(DataManager.mDownloadRewardDataDic, id);
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0005982C File Offset: 0x00057A2C
	public static ShowModelData GetShowModelDataById(string id)
	{
		if (DataManager.mShowModelDataDic.Count == 0)
		{
			DataManager.mShowModelDataDic = DataReader.LoadTable<string, ShowModelData>("ShowModelData", "ID");
		}
		return DataReader.GetTableRow<string, ShowModelData>(DataManager.mShowModelDataDic, id);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00059868 File Offset: 0x00057A68
	public static SocialDanceData GetSocialDanceDataById(string id)
	{
		if (DataManager.mSocialDanceDataDic.Count == 0)
		{
			DataManager.mSocialDanceDataDic = DataReader.LoadTable<string, SocialDanceData>("SocialDanceData", "ID");
		}
		return DataReader.GetTableRow<string, SocialDanceData>(DataManager.mSocialDanceDataDic, id);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x000598A4 File Offset: 0x00057AA4
	public static Dictionary<string, SocialDanceData> GetAllSocialDanceData()
	{
		if (DataManager.mSocialDanceDataDic.Count == 0)
		{
			DataManager.mSocialDanceDataDic = DataReader.LoadTable<string, SocialDanceData>("SocialDanceData", "ID");
		}
		return DataManager.mSocialDanceDataDic;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x000598DC File Offset: 0x00057ADC
	public static List<AnnounceData> GetAnnounceDataList()
	{
		if (DataManager.mAnnounceDataList.Count == 0)
		{
			DataManager.mAnnounceDataList = DataReader.LoadImportData<AnnounceData>("AnnounceData");
		}
		return DataManager.mAnnounceDataList;
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00059904 File Offset: 0x00057B04
	public static SexMiniData GetSexMiniDataById(string id)
	{
		if (DataManager.mSexMiniDataDic == null || DataManager.mSexMiniDataDic.Count == 0)
		{
			DataManager.mSexMiniDataDic = DataReader.LoadTable<string, SexMiniData>("SexMiniData", "ID");
		}
		return DataReader.GetTableRow<string, SexMiniData>(DataManager.mSexMiniDataDic, id);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0005994C File Offset: 0x00057B4C
	public static ShopData GetShopDataByID(string id)
	{
		if (DataManager.mShopDataDic.Count == 0)
		{
			DataManager.mShopDataDic = DataReader.LoadTable<string, ShopData>("ShopData", "ID");
		}
		return DataReader.GetTableRow<string, ShopData>(DataManager.mShopDataDic, id);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00059988 File Offset: 0x00057B88
	public static List<ShopData> GetShopDataList()
	{
		if (DataManager.mShopDataDic.Count == 0)
		{
			DataManager.mShopDataDic = DataReader.LoadTable<string, ShopData>("ShopData", "ID");
		}
		if (DataManager.mShopDataList.Count == 0)
		{
			DataManager.mShopDataList = new List<ShopData>(DataManager.mShopDataDic.Values);
		}
		return DataManager.mShopDataList;
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x000599E0 File Offset: 0x00057BE0
	public static ShopData GetPlayerShopWeapon()
	{
		if (DataManager.mShopDataDic.Count == 0)
		{
			DataManager.mShopDataDic = DataReader.LoadTable<string, ShopData>("ShopData", "ID");
		}
		if (DataManager.mShopDataList.Count == 0)
		{
			DataManager.mShopDataList = new List<ShopData>(DataManager.mShopDataDic.Values);
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<ShopData> list = new List<ShopData>();
		for (int i = 0; i < DataManager.mShopDataList.Count; i++)
		{
			if (DataManager.mShopDataList[i].ProfessionType == (int)playerData.Profession && DataManager.mShopDataList[i].Class == 6)
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(DataManager.mShopDataList[i].ItemID);
				if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP && itemDataByID.SubType == 0)
				{
					list.Add(DataManager.mShopDataList[i]);
				}
			}
		}
		list.Sort(delegate(ShopData x, ShopData y)
		{
			ItemData itemDataByID3 = DataManager.GetItemDataByID(x.ItemID);
			ItemData itemDataByID4 = DataManager.GetItemDataByID(y.ItemID);
			return itemDataByID4.Level - itemDataByID3.Level;
		});
		for (int j = 0; j < list.Count; j++)
		{
			ItemData itemDataByID2 = DataManager.GetItemDataByID(list[j].ItemID);
			if (playerData.CheckLevel(itemDataByID2.Level))
			{
				return list[j];
			}
		}
		return null;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00059B3C File Offset: 0x00057D3C
	public static List<StrongerData> GetStrongerDataList()
	{
		if (DataManager.mStrongerDataDic.Count == 0)
		{
			DataManager.mStrongerDataDic = DataReader.LoadTable<string, StrongerData>("StrongerData", "ID");
		}
		if (DataManager.mStrongerDataList.Count == 0)
		{
			DataManager.mStrongerDataList = new List<StrongerData>(DataManager.mStrongerDataDic.Values);
		}
		return DataManager.mStrongerDataList;
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00059B94 File Offset: 0x00057D94
	public static List<LoadingUIData> GetLoadingUIDataList()
	{
		if (DataManager.mLoadingUIDataDic.Count == 0)
		{
			DataManager.mLoadingUIDataDic = DataReader.LoadTable<string, LoadingUIData>("LoadingUIData", "ID");
		}
		if (DataManager.mLoadingUIDataList.Count == 0)
		{
			DataManager.mLoadingUIDataList = new List<LoadingUIData>(DataManager.mLoadingUIDataDic.Values);
			DataManager.mLoadingUIDataList.Sort(delegate(LoadingUIData x, LoadingUIData y)
			{
				if (x.ID.Length != y.ID.Length)
				{
					return x.ID.Length - y.ID.Length;
				}
				return x.ID.CompareTo(y.ID);
			});
		}
		return DataManager.mLoadingUIDataList;
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00059C14 File Offset: 0x00057E14
	public static List<TimerActivityTipsData> GetTimerActivityTipsDataList()
	{
		if (DataManager.mTimerActivityTipsDataDic == null || DataManager.mTimerActivityTipsDataDic.Count == 0)
		{
			DataManager.mTimerActivityTipsDataDic = DataReader.LoadTable<string, TimerActivityTipsData>("TimerActivityTipsData", "ID");
		}
		DataManager.mTimerActivityTipsDataList.Clear();
		if (DataManager.mTimerActivityTipsDataDic != null && DataManager.mTimerActivityTipsDataList.Count == 0)
		{
			DataManager.mTimerActivityTipsDataList = new List<TimerActivityTipsData>(DataManager.mTimerActivityTipsDataDic.Values);
			for (int i = DataManager.mTimerActivityTipsDataList.Count - 1; i >= 0; i--)
			{
				if (!TimeTools.IsTimeRange(DataManager.mTimerActivityTipsDataList[i].Starttimes, DataManager.mTimerActivityTipsDataList[i].EndTimes))
				{
					DataManager.mTimerActivityTipsDataList.RemoveAt(i);
				}
			}
			DataManager.mTimerActivityTipsDataList.Sort((TimerActivityTipsData x, TimerActivityTipsData y) => x.Weight - y.Weight);
		}
		return DataManager.mTimerActivityTipsDataList;
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00059D04 File Offset: 0x00057F04
	public static TimerActivityData GetTimerActivityDataById(string id)
	{
		if (DataManager.mTimerActivityDataDic == null || DataManager.mTimerActivityDataDic.Count == 0)
		{
			DataManager.mTimerActivityDataDic = DataReader.LoadTable<string, TimerActivityData>("TimerActivityData", "ID");
		}
		return DataReader.GetTableRow<string, TimerActivityData>(DataManager.mTimerActivityDataDic, id);
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x00059D4C File Offset: 0x00057F4C
	public static List<TimerActivityData> GetEnableTimerActivityList()
	{
		if (DataManager.mTimerActivityDataDic == null || DataManager.mTimerActivityDataDic.Count == 0)
		{
			DataManager.mTimerActivityDataDic = DataReader.LoadTable<string, TimerActivityData>("TimerActivityData", "ID");
		}
		if (DataManager.mTimerActivityDataDic != null)
		{
			List<TimerActivityData> list = new List<TimerActivityData>(DataManager.mTimerActivityDataDic.Values);
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (!TimeTools.IsTimeRange(list[i].StartTimeList, list[i].EndTimeList))
				{
					list.RemoveAt(i);
				}
			}
			return list;
		}
		return null;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00059DE8 File Offset: 0x00057FE8
	public static ActivityBossData GetActivityBossDataById(string id)
	{
		if (DataManager.mActivityBossDataDic == null || DataManager.mActivityBossDataDic.Count == 0)
		{
			DataManager.mActivityBossDataDic = DataReader.LoadTable<string, ActivityBossData>("ActivityBossData", "Key");
		}
		return DataReader.GetTableRow<string, ActivityBossData>(DataManager.mActivityBossDataDic, id);
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00059E30 File Offset: 0x00058030
	public static ActivityBossData GetActivityBossDataByActivityIdMapId(string activityId, string mapId)
	{
		if (DataManager.mActivityBossDataDic == null || DataManager.mActivityBossDataDic.Count == 0)
		{
			DataManager.mActivityBossDataDic = DataReader.LoadTable<string, ActivityBossData>("ActivityBossData", "Key");
		}
		if (DataManager.mActivityBossDataDic != null)
		{
			List<ActivityBossData> list = new List<ActivityBossData>(DataManager.mActivityBossDataDic.Values);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].ID.Equals(activityId) && list[i].MapID.Equals(mapId))
				{
					return list[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00059ED4 File Offset: 0x000580D4
	public static List<NotifyData> GetNotifyDataList()
	{
		if (DataManager.mNotifyDataDic == null || DataManager.mNotifyDataDic.Count == 0)
		{
			DataManager.mNotifyDataDic = DataReader.LoadTable<string, NotifyData>("NotifyData", "ID");
		}
		DataManager.mNotifyDataList.Clear();
		if (DataManager.mNotifyDataDic != null && DataManager.mNotifyDataList.Count == 0)
		{
			DataManager.mNotifyDataList = new List<NotifyData>(DataManager.mNotifyDataDic.Values);
		}
		return DataManager.mNotifyDataList;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00059F4C File Offset: 0x0005814C
	public static LevelRewardData GetLevelRewardDataByID(string id)
	{
		if (DataManager.mLevelRewardDataDic.Count == 0)
		{
			DataManager.mLevelRewardDataDic = DataReader.LoadTable<string, LevelRewardData>("LevelRewardData", "ID");
		}
		return DataReader.GetTableRow<string, LevelRewardData>(DataManager.mLevelRewardDataDic, id);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00059F88 File Offset: 0x00058188
	public static List<LevelRewardData> GetLevelRewardDataList()
	{
		if (DataManager.mLevelRewardDataDic.Count == 0)
		{
			DataManager.mLevelRewardDataDic = DataReader.LoadTable<string, LevelRewardData>("LevelRewardData", "ID");
		}
		if (DataManager.mLevelRewardDataDic != null && DataManager.mLevelRewardDataList.Count == 0)
		{
			DataManager.mLevelRewardDataList = new List<LevelRewardData>(DataManager.mLevelRewardDataDic.Values);
		}
		return DataManager.mLevelRewardDataList;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00059FEC File Offset: 0x000581EC
	public static GuildBattleData GetGuildBattleDataById(string id)
	{
		if (DataManager.mGuildBattleDataDic == null || DataManager.mGuildBattleDataDic.Count == 0)
		{
			DataManager.mGuildBattleDataDic = DataReader.LoadTable<string, GuildBattleData>("GuildBattleData", "ID");
		}
		return DataReader.GetTableRow<string, GuildBattleData>(DataManager.mGuildBattleDataDic, id);
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0005A034 File Offset: 0x00058234
	public static ConfigData GetConfigDataByKey(string key)
	{
		if (DataManager.mConfigDataDic == null || DataManager.mConfigDataDic.Count == 0)
		{
			DataManager.mConfigDataDic = DataReader.LoadTable<string, ConfigData>("ConfigData", "Key");
		}
		return DataReader.GetTableRow<string, ConfigData>(DataManager.mConfigDataDic, key);
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0005A07C File Offset: 0x0005827C
	public static List<ConfigData> GetConfigStarScoreList()
	{
		DataManager.mConfigDataStarScoreList.Clear();
		for (int i = 0; i < GameDefine.EquipStarScoreName.Length; i++)
		{
			DataManager.mConfigDataStarScoreList.Add(DataManager.GetConfigDataByKey(GameDefine.EquipStarScoreName[i]));
		}
		return DataManager.mConfigDataStarScoreList;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0005A0C8 File Offset: 0x000582C8
	public static List<ConfigData> GetConfigQualityScoreList()
	{
		DataManager.mConfigDataQualityScoreList.Clear();
		for (int i = 0; i < GameDefine.EquipQualityScoreName.Length; i++)
		{
			DataManager.mConfigDataQualityScoreList.Add(DataManager.GetConfigDataByKey(GameDefine.EquipQualityScoreName[i]));
		}
		return DataManager.mConfigDataQualityScoreList;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0005A114 File Offset: 0x00058314
	public static GuildStarData GetGuildStarDataById(string id)
	{
		if (DataManager.mGuildStarDataDic == null || DataManager.mGuildStarDataDic.Count == 0)
		{
			DataManager.mGuildStarDataDic = DataReader.LoadTable<string, GuildStarData>("GuildStarData", "ID");
		}
		return DataReader.GetTableRow<string, GuildStarData>(DataManager.mGuildStarDataDic, id);
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0005A15C File Offset: 0x0005835C
	public static List<GuildStarData> GetGuildStarDataListByMapID(int mapid)
	{
		if (DataManager.mGuildStarDataDic == null || DataManager.mGuildStarDataDic.Count == 0)
		{
			DataManager.mGuildStarDataDic = DataReader.LoadTable<string, GuildStarData>("GuildStarData", "ID");
		}
		DataManager.mGuildStarDataList.Clear();
		if (DataManager.mGuildStarDataDic != null && DataManager.mGuildStarDataList.Count == 0)
		{
			DataManager.mGuildStarDataList = new List<GuildStarData>(DataManager.mGuildStarDataDic.Values);
		}
		List<GuildStarData> list = new List<GuildStarData>();
		for (int i = 0; i < DataManager.mGuildStarDataList.Count; i++)
		{
			if (DataManager.mGuildStarDataList[i].MapID == mapid)
			{
				list.Add(DataManager.mGuildStarDataList[i]);
			}
		}
		return list;
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0005A218 File Offset: 0x00058418
	public static ActivityMapData GetActivityMapDataById(string id)
	{
		if (DataManager.mActivityMapDataDic == null || DataManager.mActivityMapDataDic.Count == 0)
		{
			DataManager.mActivityMapDataDic = DataReader.LoadTable<string, ActivityMapData>("ActivityMapData", "ID");
		}
		return DataReader.GetTableRow<string, ActivityMapData>(DataManager.mActivityMapDataDic, id);
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0005A260 File Offset: 0x00058460
	public static ActivityMapData GetActivityMapDataByActID(int type, string actid)
	{
		if (DataManager.mActivityMapDataDic == null || DataManager.mActivityMapDataDic.Count == 0)
		{
			DataManager.mActivityMapDataDic = DataReader.LoadTable<string, ActivityMapData>("ActivityMapData", "ID");
		}
		List<ActivityMapData> list = new List<ActivityMapData>(DataManager.mActivityMapDataDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].ActivityID.Equals(actid) && list[i].Type == type)
			{
				return list[i];
			}
		}
		return null;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0005A2F4 File Offset: 0x000584F4
	public static List<ActivityMapData> GetAcitvityMapDataByMapId(string mapId)
	{
		if (DataManager.mActivityMapDataDic == null || DataManager.mActivityMapDataDic.Count == 0)
		{
			DataManager.mActivityMapDataDic = DataReader.LoadTable<string, ActivityMapData>("ActivityMapData", "ID");
		}
		if (DataManager.mActivityMapDataDic == null)
		{
			return new List<ActivityMapData>();
		}
		if (DataManager.mActivityMapDataByMapIdDic.Count == 0)
		{
			List<ActivityMapData> list = new List<ActivityMapData>(DataManager.mActivityMapDataDic.Values);
			for (int i = 0; i < list.Count; i++)
			{
				if (DataManager.mActivityMapDataByMapIdDic.ContainsKey(list[i].MapId))
				{
					DataManager.mActivityMapDataByMapIdDic[list[i].MapId].Add(list[i]);
				}
				else
				{
					DataManager.mActivityMapDataByMapIdDic.Add(list[i].MapId, new List<ActivityMapData>());
					DataManager.mActivityMapDataByMapIdDic[list[i].MapId].Add(list[i]);
				}
			}
		}
		if (DataManager.mActivityMapDataByMapIdDic.ContainsKey(mapId))
		{
			return DataManager.mActivityMapDataByMapIdDic[mapId];
		}
		return new List<ActivityMapData>();
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0005A414 File Offset: 0x00058614
	private static void InitActivityMapDataByTypeDic()
	{
		if (DataManager.mActivityMapDataDic == null || DataManager.mActivityMapDataDic.Count == 0)
		{
			return;
		}
		if (DataManager.mActivityMapDataByTypeDic.Count == 0)
		{
			List<ActivityMapData> list = new List<ActivityMapData>(DataManager.mActivityMapDataDic.Values);
			for (int i = 0; i < list.Count; i++)
			{
				int type = list[i].Type;
				int subType = list[i].SubType;
				if (DataManager.mActivityMapDataByTypeDic.ContainsKey(type))
				{
					if (!DataManager.mActivityMapDataByTypeDic[type].ContainsKey(subType))
					{
						DataManager.mActivityMapDataByTypeDic[type].Add(subType, new List<ActivityMapData>());
					}
					DataManager.mActivityMapDataByTypeDic[type][subType].Add(list[i]);
				}
				else
				{
					DataManager.mActivityMapDataByTypeDic.Add(type, new Dictionary<int, List<ActivityMapData>>());
					DataManager.mActivityMapDataByTypeDic[type].Add(subType, new List<ActivityMapData>());
					DataManager.mActivityMapDataByTypeDic[type][subType].Add(list[i]);
				}
			}
		}
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0005A530 File Offset: 0x00058730
	public static List<ActivityMapData> GetActivityMapDataByType(int type, int subType)
	{
		DataManager.InitActivityMapDataByTypeDic();
		if (DataManager.mActivityMapDataByTypeDic.ContainsKey(type) && DataManager.mActivityMapDataByTypeDic[type].ContainsKey(subType))
		{
			return DataManager.mActivityMapDataByTypeDic[type][subType];
		}
		return null;
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0005A57C File Offset: 0x0005877C
	public static MonthlyCardData GetMonthlyCardDataById(string id)
	{
		if (DataManager.mMonthlyCardDataDic == null || DataManager.mMonthlyCardDataDic.Count == 0)
		{
			DataManager.mMonthlyCardDataDic = DataReader.LoadTable<string, MonthlyCardData>("MonthlyCardData", "ID");
		}
		return DataReader.GetTableRow<string, MonthlyCardData>(DataManager.mMonthlyCardDataDic, id);
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0005A5C4 File Offset: 0x000587C4
	public static MoveTargetMissionData GetMoveTargetMissionDataById(string id)
	{
		if (DataManager.mMoveTargetMissionDataDic == null || DataManager.mMoveTargetMissionDataDic.Count == 0)
		{
			DataManager.mMoveTargetMissionDataDic = DataReader.LoadTable<string, MoveTargetMissionData>("MoveTargetMissionData", "ID");
		}
		return DataReader.GetTableRow<string, MoveTargetMissionData>(DataManager.mMoveTargetMissionDataDic, id);
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0005A60C File Offset: 0x0005880C
	public static PoliceLevelData GetPoliceLevelDataById(string id)
	{
		if (DataManager.mPoliceLevelDataDic == null || DataManager.mPoliceLevelDataDic.Count == 0)
		{
			DataManager.mPoliceLevelDataDic = DataReader.LoadTable<string, PoliceLevelData>("PoliceLevelData", "ID");
		}
		return DataReader.GetTableRow<string, PoliceLevelData>(DataManager.mPoliceLevelDataDic, id);
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0005A654 File Offset: 0x00058854
	public static List<QualityData> GetQualityDataListByID(string id)
	{
		if (DataManager.mQualityDataDic.Count == 0)
		{
			DataManager.mQualityDataDic = DataReader.LoadTableList<string, QualityData>("QualityData", "ID");
		}
		return DataReader.GetTableList<string, QualityData>(DataManager.mQualityDataDic, id);
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0005A690 File Offset: 0x00058890
	public static SingleMapLockData GetSingleMapLockDataById(int id)
	{
		if (DataManager.mSingleMapLockDataDic == null || DataManager.mSingleMapLockDataDic.Count == 0)
		{
			DataManager.mSingleMapLockDataDic = DataReader.LoadTable<int, SingleMapLockData>("SingleMapLockData", "ID");
		}
		return DataReader.GetTableRow<int, SingleMapLockData>(DataManager.mSingleMapLockDataDic, id);
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0005A6D8 File Offset: 0x000588D8
	public static Dictionary<int, SingleMapLockData> SingleMapLockDataDic
	{
		get
		{
			if (DataManager.mSingleMapLockDataDic == null || DataManager.mSingleMapLockDataDic.Count == 0)
			{
				DataManager.mSingleMapLockDataDic = DataReader.LoadTable<int, SingleMapLockData>("SingleMapLockData", "ID");
			}
			return DataManager.mSingleMapLockDataDic;
		}
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0005A718 File Offset: 0x00058918
	public static KillTargetMissionData GetKillTargetMissionDataById(string id)
	{
		if (DataManager.mKillTargetMissionDataDic == null || DataManager.mKillTargetMissionDataDic.Count == 0)
		{
			DataManager.mKillTargetMissionDataDic = DataReader.LoadTable<string, KillTargetMissionData>("KillTargetMissionData", "ID");
		}
		return DataReader.GetTableRow<string, KillTargetMissionData>(DataManager.mKillTargetMissionDataDic, id);
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0005A760 File Offset: 0x00058960
	public static TargetCarMissionData GetTargetCarMissionDataById(string id)
	{
		if (DataManager.mTargetCarMissionDataDic == null || DataManager.mTargetCarMissionDataDic.Count == 0)
		{
			DataManager.mTargetCarMissionDataDic = DataReader.LoadTable<string, TargetCarMissionData>("TargetCarMissionData", "ID");
		}
		return DataReader.GetTableRow<string, TargetCarMissionData>(DataManager.mTargetCarMissionDataDic, id);
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0005A7A8 File Offset: 0x000589A8
	public static DailyExpData GetDailyExpDataById(string id)
	{
		if (DataManager.mDailyExpDataDic == null || DataManager.mDailyExpDataDic.Count == 0)
		{
			DataManager.mDailyExpDataDic = DataReader.LoadTable<string, DailyExpData>("DailyExpData", "ID");
		}
		return DataReader.GetTableRow<string, DailyExpData>(DataManager.mDailyExpDataDic, id);
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0005A7F0 File Offset: 0x000589F0
	public static OnlineMissionData GetOnlineMissionDataByID(string id)
	{
		if (DataManager.mOnlineMissionDataDict.Count == 0)
		{
			DataManager.mOnlineMissionDataDict = DataReader.LoadTable<string, OnlineMissionData>("OnlineMissionData", "ID");
		}
		return DataReader.GetTableRow<string, OnlineMissionData>(DataManager.mOnlineMissionDataDict, id);
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0005A82C File Offset: 0x00058A2C
	public static DominData GetDominDataByID(string id)
	{
		if (DataManager.mDominDataDict.Count == 0)
		{
			DataManager.mDominDataDict = DataReader.LoadTable<string, DominData>("DominData", "ID");
		}
		return DataReader.GetTableRow<string, DominData>(DataManager.mDominDataDict, id);
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0005A868 File Offset: 0x00058A68
	public static LevelSealData GetLevelSealDataByID(string id)
	{
		if (DataManager.mLevelSealDataDict == null || DataManager.mLevelSealDataDict.Count == 0)
		{
			DataManager.mLevelSealDataDict = DataReader.LoadTable<string, LevelSealData>("LevelSealData", "ID");
		}
		return DataReader.GetTableRow<string, LevelSealData>(DataManager.mLevelSealDataDict, id);
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0005A8B0 File Offset: 0x00058AB0
	public static TimeLimitMissionData GetTimeLimitMissionDataByID(string id)
	{
		if (DataManager.mTimeLimitMissionDataDict == null || DataManager.mTimeLimitMissionDataDict.Count == 0)
		{
			DataManager.mTimeLimitMissionDataDict = DataReader.LoadTable<string, TimeLimitMissionData>("TimeLimitMissionData", "ID");
		}
		return DataReader.GetTableRow<string, TimeLimitMissionData>(DataManager.mTimeLimitMissionDataDict, id);
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0005A8F8 File Offset: 0x00058AF8
	public static SkillLabelData GetSkillLabelDataByID(string id)
	{
		if (DataManager.mSkillLabelDataDict == null || DataManager.mSkillLabelDataDict.Count == 0)
		{
			DataManager.mSkillLabelDataDict = DataReader.LoadTable<string, SkillLabelData>("SkillLabelData", "ID");
		}
		return DataReader.GetTableRow<string, SkillLabelData>(DataManager.mSkillLabelDataDict, id);
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0005A940 File Offset: 0x00058B40
	public static GuildCaptureData GetGuildCaptureDataByID(string id)
	{
		if (DataManager.mGuildCaptureDataDict == null || DataManager.mGuildCaptureDataDict.Count == 0)
		{
			DataManager.mGuildCaptureDataDict = DataReader.LoadTable<string, GuildCaptureData>("GuildCaptureData", "ID");
		}
		return DataReader.GetTableRow<string, GuildCaptureData>(DataManager.mGuildCaptureDataDict, id);
	}

	// Token: 0x04000AB1 RID: 2737
	public static bool initFlag = false;

	// Token: 0x04000AB2 RID: 2738
	public static bool initDoneFlag = false;

	// Token: 0x04000AB3 RID: 2739
	private static bool reImportFlag = false;

	// Token: 0x04000AB4 RID: 2740
	private static Dictionary<string, List<SceneComponentData>> mSceneComponentDataDict = new Dictionary<string, List<SceneComponentData>>();

	// Token: 0x04000AB5 RID: 2741
	private static Dictionary<string, SkillData> mSkillDataDict = new Dictionary<string, SkillData>();

	// Token: 0x04000AB6 RID: 2742
	private static Dictionary<string, EffInfoData> mEffInfoDataDict = new Dictionary<string, EffInfoData>();

	// Token: 0x04000AB7 RID: 2743
	private static Dictionary<string, ActionData> mActionDataDict = new Dictionary<string, ActionData>();

	// Token: 0x04000AB8 RID: 2744
	private static Dictionary<string, CharacterModelData> mCharacterModelDataDict = new Dictionary<string, CharacterModelData>();

	// Token: 0x04000AB9 RID: 2745
	private static Dictionary<string, ModelData> mModeDataDict = new Dictionary<string, ModelData>();

	// Token: 0x04000ABA RID: 2746
	private static Dictionary<string, BuffInfoData> mBuffInfoDataDict = new Dictionary<string, BuffInfoData>();

	// Token: 0x04000ABB RID: 2747
	private static Dictionary<string, NpcData> mNpcDataDict = new Dictionary<string, NpcData>();

	// Token: 0x04000ABC RID: 2748
	private static Dictionary<MAPTYPE, List<MapInfoData>> mMapInfoTypeDataDict = new Dictionary<MAPTYPE, List<MapInfoData>>();

	// Token: 0x04000ABD RID: 2749
	private static Dictionary<string, MapInfoData> mMapInfoDataDict = new Dictionary<string, MapInfoData>();

	// Token: 0x04000ABE RID: 2750
	private static Dictionary<string, List<FxEffInfoData>> mFxEffInfoDataDict = new Dictionary<string, List<FxEffInfoData>>();

	// Token: 0x04000ABF RID: 2751
	private static Dictionary<string, List<NPCPathData>> mNPCPathDataDic = new Dictionary<string, List<NPCPathData>>();

	// Token: 0x04000AC0 RID: 2752
	private static Dictionary<string, List<MonsterData>> mMonsterDataDic = new Dictionary<string, List<MonsterData>>();

	// Token: 0x04000AC1 RID: 2753
	private static Dictionary<string, ItemData> mItemDataDict = new Dictionary<string, ItemData>();

	// Token: 0x04000AC2 RID: 2754
	private static Dictionary<int, List<string>> mLvAutoAcceptMissionDic = new Dictionary<int, List<string>>();

	// Token: 0x04000AC3 RID: 2755
	private static Dictionary<string, MissionData> mMissionDataDict = new Dictionary<string, MissionData>();

	// Token: 0x04000AC4 RID: 2756
	private static Dictionary<string, List<MissionData>> mMapMissionDataDic = new Dictionary<string, List<MissionData>>();

	// Token: 0x04000AC5 RID: 2757
	private static List<MissionData> mSideMissionList = new List<MissionData>();

	// Token: 0x04000AC6 RID: 2758
	private static Dictionary<string, NPCDialogData> mNPCDialogDataDict = new Dictionary<string, NPCDialogData>();

	// Token: 0x04000AC7 RID: 2759
	private static Dictionary<string, MissionRequireData> mMissionRequireDataDict = new Dictionary<string, MissionRequireData>();

	// Token: 0x04000AC8 RID: 2760
	private static Dictionary<string, NpcOptionDialogData> mNpcOptionDialogDataDict = new Dictionary<string, NpcOptionDialogData>();

	// Token: 0x04000AC9 RID: 2761
	private static Dictionary<string, MapConnectInfoData> mMapConnectInfoDataDic = new Dictionary<string, MapConnectInfoData>();

	// Token: 0x04000ACA RID: 2762
	private static Dictionary<string, EquipData> mEquipDataDic = new Dictionary<string, EquipData>();

	// Token: 0x04000ACB RID: 2763
	private static Dictionary<int, SkillupgradeData> mSkillupgradeDataDic = new Dictionary<int, SkillupgradeData>();

	// Token: 0x04000ACC RID: 2764
	private static Dictionary<string, CopySceneData> mCopySceneDataDic = new Dictionary<string, CopySceneData>();

	// Token: 0x04000ACD RID: 2765
	private static List<CopySceneData> mCopySceneDataList = new List<CopySceneData>();

	// Token: 0x04000ACE RID: 2766
	public static Dictionary<string, LadderRewardData> mLadderRewardDataDic = new Dictionary<string, LadderRewardData>();

	// Token: 0x04000ACF RID: 2767
	private static List<LadderRewardData> mLadderRewardDataList = new List<LadderRewardData>();

	// Token: 0x04000AD0 RID: 2768
	private static Dictionary<int, BaseLvData> mBaseLvDataDic = new Dictionary<int, BaseLvData>();

	// Token: 0x04000AD1 RID: 2769
	private static Dictionary<string, TitleData> mTitleDateDic = new Dictionary<string, TitleData>();

	// Token: 0x04000AD2 RID: 2770
	private static List<TitleData> mTitleList = new List<TitleData>();

	// Token: 0x04000AD3 RID: 2771
	private static Dictionary<string, List<StoryData>> mStoryDataDic = new Dictionary<string, List<StoryData>>();

	// Token: 0x04000AD4 RID: 2772
	private static Dictionary<int, List<EquipmentUpgradeData>> mEquipmentUpgradeDataDic = new Dictionary<int, List<EquipmentUpgradeData>>();

	// Token: 0x04000AD5 RID: 2773
	private static Dictionary<int, DamageBoardTypeData> mDamageBoardTypeDataDic = new Dictionary<int, DamageBoardTypeData>();

	// Token: 0x04000AD6 RID: 2774
	private static Dictionary<string, List<CamRockCurveData>> mCamRockCurveDataDic = new Dictionary<string, List<CamRockCurveData>>();

	// Token: 0x04000AD7 RID: 2775
	private static Dictionary<string, CamRockData> mCamRockDataDic = new Dictionary<string, CamRockData>();

	// Token: 0x04000AD8 RID: 2776
	private static Dictionary<string, SurveyMissionData> mSurveyMissionDataDic = new Dictionary<string, SurveyMissionData>();

	// Token: 0x04000AD9 RID: 2777
	private static List<SurveyMissionData> mSurveyMissionDataList = new List<SurveyMissionData>();

	// Token: 0x04000ADA RID: 2778
	private static Dictionary<string, List<MultiDeliveryMissionData>> mMultiDeliveryMissionDataDic = new Dictionary<string, List<MultiDeliveryMissionData>>();

	// Token: 0x04000ADB RID: 2779
	private static Dictionary<string, DailyMissionData> mDailyMissionDataDic = new Dictionary<string, DailyMissionData>();

	// Token: 0x04000ADC RID: 2780
	private static Dictionary<int, List<RefineData>> mRefineDataDic = new Dictionary<int, List<RefineData>>();

	// Token: 0x04000ADD RID: 2781
	private static Dictionary<int, RefineLevelData> mRefineLevelDataDic = new Dictionary<int, RefineLevelData>();

	// Token: 0x04000ADE RID: 2782
	private static Dictionary<string, List<CarMissionBlockData>> mCarMissionBlockDataDic = new Dictionary<string, List<CarMissionBlockData>>();

	// Token: 0x04000ADF RID: 2783
	private static Dictionary<int, List<ConsignBuyTabData>> mConsignBuyTabDataDic = new Dictionary<int, List<ConsignBuyTabData>>();

	// Token: 0x04000AE0 RID: 2784
	private static Dictionary<string, List<SneakingMissionData>> mSneakingMissionDataDic = new Dictionary<string, List<SneakingMissionData>>();

	// Token: 0x04000AE1 RID: 2785
	private static Dictionary<string, BadgeData> mBadgeDataDic = new Dictionary<string, BadgeData>();

	// Token: 0x04000AE2 RID: 2786
	private static List<BadgeData> BadgeDataList = null;

	// Token: 0x04000AE3 RID: 2787
	private static Dictionary<int, TowerData> mTowerDataDic = new Dictionary<int, TowerData>();

	// Token: 0x04000AE4 RID: 2788
	private static Dictionary<string, AIData> mAIDataDic = new Dictionary<string, AIData>();

	// Token: 0x04000AE5 RID: 2789
	private static Dictionary<string, WildBossData> mWildBossDataDic = new Dictionary<string, WildBossData>();

	// Token: 0x04000AE6 RID: 2790
	private static Dictionary<string, GuildBossData> mGuildBossDataDic = new Dictionary<string, GuildBossData>();

	// Token: 0x04000AE7 RID: 2791
	private static Dictionary<string, BarFightCopyData> mBarFightCopyDataDic = new Dictionary<string, BarFightCopyData>();

	// Token: 0x04000AE8 RID: 2792
	public static Dictionary<string, GuildDonateData> mGuildDonateDataDic = new Dictionary<string, GuildDonateData>();

	// Token: 0x04000AE9 RID: 2793
	public static List<GuildLevelData> mGuildLevelDataList = new List<GuildLevelData>();

	// Token: 0x04000AEA RID: 2794
	private static Dictionary<int, List<GuildSkillData>> mGuildSkillDataDic = new Dictionary<int, List<GuildSkillData>>();

	// Token: 0x04000AEB RID: 2795
	private static Dictionary<string, TeamData> mTeamDataDic = new Dictionary<string, TeamData>();

	// Token: 0x04000AEC RID: 2796
	private static Dictionary<string, ShowRewardData> mShowRewardDataDic = new Dictionary<string, ShowRewardData>();

	// Token: 0x04000AED RID: 2797
	private static Dictionary<int, AdaptData> mAdaptDataDic = new Dictionary<int, AdaptData>();

	// Token: 0x04000AEE RID: 2798
	private static Dictionary<string, FunctionData> mFunctionDataDic = new Dictionary<string, FunctionData>();

	// Token: 0x04000AEF RID: 2799
	private static Dictionary<int, SoundData> mSoundDataDic = new Dictionary<int, SoundData>();

	// Token: 0x04000AF0 RID: 2800
	private static Dictionary<string, List<MapAreaInfoData>> mMapAreaInfoDataDic = new Dictionary<string, List<MapAreaInfoData>>();

	// Token: 0x04000AF1 RID: 2801
	private static Dictionary<string, EscortData> mEscortDataDic = new Dictionary<string, EscortData>();

	// Token: 0x04000AF2 RID: 2802
	private static Dictionary<string, CityDanceData> mCityDanceDataDic = new Dictionary<string, CityDanceData>();

	// Token: 0x04000AF3 RID: 2803
	private static Dictionary<string, MountData> mMountDataDic = new Dictionary<string, MountData>();

	// Token: 0x04000AF4 RID: 2804
	private static Dictionary<string, ColorData> mColorDataDic = new Dictionary<string, ColorData>();

	// Token: 0x04000AF5 RID: 2805
	private static Dictionary<string, DanceData> mDanceDataDic = new Dictionary<string, DanceData>();

	// Token: 0x04000AF6 RID: 2806
	private static Dictionary<string, SlotIconData> mSlotIconDataDic = new Dictionary<string, SlotIconData>();

	// Token: 0x04000AF7 RID: 2807
	private static List<SlotIconData> mSlotIconList = new List<SlotIconData>();

	// Token: 0x04000AF8 RID: 2808
	private static Dictionary<string, SlotAutoData> mSlotAutoDataDic = new Dictionary<string, SlotAutoData>();

	// Token: 0x04000AF9 RID: 2809
	private static List<SlotAutoData> mSlotAutoList = new List<SlotAutoData>();

	// Token: 0x04000AFA RID: 2810
	private static Dictionary<string, SurviveBattleData> mSurviveBattleDataDic = new Dictionary<string, SurviveBattleData>();

	// Token: 0x04000AFB RID: 2811
	private static Dictionary<string, ScuffleData> mScuffleDataDic = new Dictionary<string, ScuffleData>();

	// Token: 0x04000AFC RID: 2812
	private static Dictionary<int, SignInMonthData> mSignInMonthDataDic = new Dictionary<int, SignInMonthData>();

	// Token: 0x04000AFD RID: 2813
	private static List<SignInMonthData> mSignInMonthDataList = new List<SignInMonthData>();

	// Token: 0x04000AFE RID: 2814
	private static Dictionary<string, DailyBuyData> mDailyBuyDataDic = new Dictionary<string, DailyBuyData>();

	// Token: 0x04000AFF RID: 2815
	private static Dictionary<string, InvestData> mInvestDataDic = new Dictionary<string, InvestData>();

	// Token: 0x04000B00 RID: 2816
	private static Dictionary<string, LevelPackageData> mLevelPackageDataDic = new Dictionary<string, LevelPackageData>();

	// Token: 0x04000B01 RID: 2817
	private static List<LevelPackageData> mLevelPackageDataList = new List<LevelPackageData>();

	// Token: 0x04000B02 RID: 2818
	private static Dictionary<string, RetrieveData> mRetrieveDataDic = new Dictionary<string, RetrieveData>();

	// Token: 0x04000B03 RID: 2819
	private static Dictionary<int, SignInWeekData> mSignInWeekDataDic = new Dictionary<int, SignInWeekData>();

	// Token: 0x04000B04 RID: 2820
	private static List<SignInWeekData> mSignInWeekDataList = new List<SignInWeekData>();

	// Token: 0x04000B05 RID: 2821
	private static Dictionary<string, DailyActiveData> mDailyActiveDataDic = new Dictionary<string, DailyActiveData>();

	// Token: 0x04000B06 RID: 2822
	private static List<DailyActiveData> mDailyActiveDataList = new List<DailyActiveData>();

	// Token: 0x04000B07 RID: 2823
	private static Dictionary<string, DailyActiveRewardData> mDailyActiveRewardDataDic = new Dictionary<string, DailyActiveRewardData>();

	// Token: 0x04000B08 RID: 2824
	private static List<DailyActiveRewardData> mDailyActiveRewardDataList = new List<DailyActiveRewardData>();

	// Token: 0x04000B09 RID: 2825
	private static Dictionary<string, FirstBuyData> mFirstBuyDataDic = new Dictionary<string, FirstBuyData>();

	// Token: 0x04000B0A RID: 2826
	private static Dictionary<string, BigPackageData> mBigPackageDataDic = new Dictionary<string, BigPackageData>();

	// Token: 0x04000B0B RID: 2827
	private static Dictionary<string, BigPackageTimeListData> mBigPackageTimeListDataDic = new Dictionary<string, BigPackageTimeListData>();

	// Token: 0x04000B0C RID: 2828
	private static Dictionary<string, PurchaseData> mPurchaseDataDic = new Dictionary<string, PurchaseData>();

	// Token: 0x04000B0D RID: 2829
	private static Dictionary<string, ModelPartData> mModelPartDataDic = new Dictionary<string, ModelPartData>();

	// Token: 0x04000B0E RID: 2830
	public static List<ModelPartData> mModelPartDataList = new List<ModelPartData>();

	// Token: 0x04000B0F RID: 2831
	private static Dictionary<string, DownloadRewardData> mDownloadRewardDataDic = new Dictionary<string, DownloadRewardData>();

	// Token: 0x04000B10 RID: 2832
	private static Dictionary<string, ShowModelData> mShowModelDataDic = new Dictionary<string, ShowModelData>();

	// Token: 0x04000B11 RID: 2833
	private static Dictionary<string, SocialDanceData> mSocialDanceDataDic = new Dictionary<string, SocialDanceData>();

	// Token: 0x04000B12 RID: 2834
	private static List<AnnounceData> mAnnounceDataList = new List<AnnounceData>();

	// Token: 0x04000B13 RID: 2835
	private static Dictionary<string, SexMiniData> mSexMiniDataDic = new Dictionary<string, SexMiniData>();

	// Token: 0x04000B14 RID: 2836
	private static Dictionary<string, ShopData> mShopDataDic = new Dictionary<string, ShopData>();

	// Token: 0x04000B15 RID: 2837
	public static List<ShopData> mShopDataList = new List<ShopData>();

	// Token: 0x04000B16 RID: 2838
	private static Dictionary<string, StrongerData> mStrongerDataDic = new Dictionary<string, StrongerData>();

	// Token: 0x04000B17 RID: 2839
	public static List<StrongerData> mStrongerDataList = new List<StrongerData>();

	// Token: 0x04000B18 RID: 2840
	private static Dictionary<string, LoadingUIData> mLoadingUIDataDic = new Dictionary<string, LoadingUIData>();

	// Token: 0x04000B19 RID: 2841
	public static List<LoadingUIData> mLoadingUIDataList = new List<LoadingUIData>();

	// Token: 0x04000B1A RID: 2842
	private static Dictionary<string, TimerActivityTipsData> mTimerActivityTipsDataDic = new Dictionary<string, TimerActivityTipsData>();

	// Token: 0x04000B1B RID: 2843
	private static List<TimerActivityTipsData> mTimerActivityTipsDataList = new List<TimerActivityTipsData>();

	// Token: 0x04000B1C RID: 2844
	private static Dictionary<string, TimerActivityData> mTimerActivityDataDic = new Dictionary<string, TimerActivityData>();

	// Token: 0x04000B1D RID: 2845
	private static Dictionary<string, ActivityBossData> mActivityBossDataDic = new Dictionary<string, ActivityBossData>();

	// Token: 0x04000B1E RID: 2846
	private static Dictionary<string, NotifyData> mNotifyDataDic = new Dictionary<string, NotifyData>();

	// Token: 0x04000B1F RID: 2847
	private static List<NotifyData> mNotifyDataList = new List<NotifyData>();

	// Token: 0x04000B20 RID: 2848
	private static Dictionary<string, LevelRewardData> mLevelRewardDataDic = new Dictionary<string, LevelRewardData>();

	// Token: 0x04000B21 RID: 2849
	private static List<LevelRewardData> mLevelRewardDataList = new List<LevelRewardData>();

	// Token: 0x04000B22 RID: 2850
	private static Dictionary<string, GuildBattleData> mGuildBattleDataDic = new Dictionary<string, GuildBattleData>();

	// Token: 0x04000B23 RID: 2851
	private static Dictionary<string, ConfigData> mConfigDataDic = new Dictionary<string, ConfigData>();

	// Token: 0x04000B24 RID: 2852
	private static List<ConfigData> mConfigDataStarScoreList = new List<ConfigData>();

	// Token: 0x04000B25 RID: 2853
	private static List<ConfigData> mConfigDataQualityScoreList = new List<ConfigData>();

	// Token: 0x04000B26 RID: 2854
	private static Dictionary<string, GuildStarData> mGuildStarDataDic = new Dictionary<string, GuildStarData>();

	// Token: 0x04000B27 RID: 2855
	private static List<GuildStarData> mGuildStarDataList = new List<GuildStarData>();

	// Token: 0x04000B28 RID: 2856
	private static Dictionary<string, ActivityMapData> mActivityMapDataDic = new Dictionary<string, ActivityMapData>();

	// Token: 0x04000B29 RID: 2857
	private static Dictionary<string, List<ActivityMapData>> mActivityMapDataByMapIdDic = new Dictionary<string, List<ActivityMapData>>();

	// Token: 0x04000B2A RID: 2858
	private static Dictionary<int, Dictionary<int, List<ActivityMapData>>> mActivityMapDataByTypeDic = new Dictionary<int, Dictionary<int, List<ActivityMapData>>>();

	// Token: 0x04000B2B RID: 2859
	private static Dictionary<string, MonthlyCardData> mMonthlyCardDataDic = new Dictionary<string, MonthlyCardData>();

	// Token: 0x04000B2C RID: 2860
	private static Dictionary<string, MoveTargetMissionData> mMoveTargetMissionDataDic = new Dictionary<string, MoveTargetMissionData>();

	// Token: 0x04000B2D RID: 2861
	private static Dictionary<string, PoliceLevelData> mPoliceLevelDataDic = new Dictionary<string, PoliceLevelData>();

	// Token: 0x04000B2E RID: 2862
	private static Dictionary<string, List<QualityData>> mQualityDataDic = new Dictionary<string, List<QualityData>>();

	// Token: 0x04000B2F RID: 2863
	private static Dictionary<int, SingleMapLockData> mSingleMapLockDataDic = new Dictionary<int, SingleMapLockData>();

	// Token: 0x04000B30 RID: 2864
	private static Dictionary<string, KillTargetMissionData> mKillTargetMissionDataDic = new Dictionary<string, KillTargetMissionData>();

	// Token: 0x04000B31 RID: 2865
	private static Dictionary<string, TargetCarMissionData> mTargetCarMissionDataDic = new Dictionary<string, TargetCarMissionData>();

	// Token: 0x04000B32 RID: 2866
	private static Dictionary<string, DailyExpData> mDailyExpDataDic = new Dictionary<string, DailyExpData>();

	// Token: 0x04000B33 RID: 2867
	private static Dictionary<string, OnlineMissionData> mOnlineMissionDataDict = new Dictionary<string, OnlineMissionData>();

	// Token: 0x04000B34 RID: 2868
	private static Dictionary<string, DominData> mDominDataDict = new Dictionary<string, DominData>();

	// Token: 0x04000B35 RID: 2869
	private static Dictionary<string, LevelSealData> mLevelSealDataDict = new Dictionary<string, LevelSealData>();

	// Token: 0x04000B36 RID: 2870
	private static Dictionary<string, TimeLimitMissionData> mTimeLimitMissionDataDict = new Dictionary<string, TimeLimitMissionData>();

	// Token: 0x04000B37 RID: 2871
	private static Dictionary<string, SkillLabelData> mSkillLabelDataDict = new Dictionary<string, SkillLabelData>();

	// Token: 0x04000B38 RID: 2872
	private static Dictionary<string, GuildCaptureData> mGuildCaptureDataDict = new Dictionary<string, GuildCaptureData>();
}
