using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000884 RID: 2180
public class SceneManager
{
	// Token: 0x17000F64 RID: 3940
	// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000FBF40 File Offset: 0x000FA140
	public GameObject UIRoot
	{
		get
		{
			return this.mUIRoot;
		}
	}

	// Token: 0x17000F65 RID: 3941
	// (get) Token: 0x06003A60 RID: 14944 RVA: 0x000FBF48 File Offset: 0x000FA148
	// (set) Token: 0x06003A61 RID: 14945 RVA: 0x000FBF50 File Offset: 0x000FA150
	public GameObject NameBoardRoot
	{
		get
		{
			return this.mNameBoardRoot;
		}
		set
		{
			this.mNameBoardRoot = value;
		}
	}

	// Token: 0x17000F66 RID: 3942
	// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000FBF5C File Offset: 0x000FA15C
	// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000FBF64 File Offset: 0x000FA164
	public GameObject DamageBoadRoot
	{
		get
		{
			return this.mDamageBoadRoot;
		}
		set
		{
			this.mDamageBoadRoot = value;
		}
	}

	// Token: 0x17000F67 RID: 3943
	// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000FBF70 File Offset: 0x000FA170
	public DamageBoardManager DamageBoardManger
	{
		get
		{
			return this.mDamageBoardManger;
		}
	}

	// Token: 0x17000F68 RID: 3944
	// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000FBF78 File Offset: 0x000FA178
	// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000FBF80 File Offset: 0x000FA180
	public GameObject MapActivityRoot
	{
		get
		{
			return this.mMapActivityRoot;
		}
		set
		{
			this.mMapActivityRoot = value;
		}
	}

	// Token: 0x17000F69 RID: 3945
	// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000FBF8C File Offset: 0x000FA18C
	public MapActivityManager MapActivityManager
	{
		get
		{
			return this.mMapActivityManager;
		}
	}

	// Token: 0x17000F6A RID: 3946
	// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000FBF94 File Offset: 0x000FA194
	// (set) Token: 0x06003A69 RID: 14953 RVA: 0x000FBF9C File Offset: 0x000FA19C
	public GameObjectPool NameBoadPool
	{
		get
		{
			return this.mNameBoadPool;
		}
		set
		{
			this.mNameBoadPool = value;
		}
	}

	// Token: 0x17000F6B RID: 3947
	// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000FBFA8 File Offset: 0x000FA1A8
	// (set) Token: 0x06003A6B RID: 14955 RVA: 0x000FBFB0 File Offset: 0x000FA1B0
	public GameObjectPool SimpleShadowPool
	{
		get
		{
			return this.mSimpleShadowPool;
		}
		set
		{
			this.mSimpleShadowPool = value;
		}
	}

	// Token: 0x17000F6C RID: 3948
	// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000FBFBC File Offset: 0x000FA1BC
	// (set) Token: 0x06003A6D RID: 14957 RVA: 0x000FBFC4 File Offset: 0x000FA1C4
	public GameObject DropItemRoot
	{
		get
		{
			return this.mDropItemRoot;
		}
		set
		{
			this.mDropItemRoot = value;
		}
	}

	// Token: 0x17000F6D RID: 3949
	// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000FBFD0 File Offset: 0x000FA1D0
	// (set) Token: 0x06003A6F RID: 14959 RVA: 0x000FBFD8 File Offset: 0x000FA1D8
	public GameObjectPool DropItemPool
	{
		get
		{
			return this.mDropItemPool;
		}
		set
		{
			this.mDropItemPool = value;
		}
	}

	// Token: 0x17000F6E RID: 3950
	// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000FBFE4 File Offset: 0x000FA1E4
	// (set) Token: 0x06003A71 RID: 14961 RVA: 0x000FBFEC File Offset: 0x000FA1EC
	public bool IsMissionStart
	{
		get
		{
			return this.mIsMissionStart;
		}
		set
		{
			this.mIsMissionStart = value;
		}
	}

	// Token: 0x17000F6F RID: 3951
	// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000FBFF8 File Offset: 0x000FA1F8
	// (set) Token: 0x06003A73 RID: 14963 RVA: 0x000FC000 File Offset: 0x000FA200
	public GameObjectPool UIItemPool
	{
		get
		{
			return this.mUIItemPool;
		}
		set
		{
			this.mUIItemPool = value;
		}
	}

	// Token: 0x17000F70 RID: 3952
	// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000FC00C File Offset: 0x000FA20C
	public MapInfoData CurrentMapInofData
	{
		get
		{
			return this.mapInfoData;
		}
	}

	// Token: 0x17000F71 RID: 3953
	// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000FC014 File Offset: 0x000FA214
	// (set) Token: 0x06003A76 RID: 14966 RVA: 0x000FC01C File Offset: 0x000FA21C
	public int CurSceneReward
	{
		get
		{
			return this.mCurSceneReward;
		}
		set
		{
			this.mCurSceneReward = value;
		}
	}

	// Token: 0x17000F72 RID: 3954
	// (get) Token: 0x06003A77 RID: 14967 RVA: 0x000FC028 File Offset: 0x000FA228
	public List<MapAreaInfoData> SaftyAreaData
	{
		get
		{
			return this.mSaftyAreaData;
		}
	}

	// Token: 0x17000F73 RID: 3955
	// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000FC030 File Offset: 0x000FA230
	public List<MapAreaInfoData> GatherAreaData
	{
		get
		{
			return this.mGatherAreaData;
		}
	}

	// Token: 0x17000F74 RID: 3956
	// (get) Token: 0x06003A79 RID: 14969 RVA: 0x000FC038 File Offset: 0x000FA238
	// (set) Token: 0x06003A7A RID: 14970 RVA: 0x000FC040 File Offset: 0x000FA240
	public bool LoadingFlag
	{
		get
		{
			return this.mLoadingFlag;
		}
		set
		{
			this.mLoadingFlag = value;
		}
	}

	// Token: 0x17000F75 RID: 3957
	// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000FC04C File Offset: 0x000FA24C
	public List<MonsterData> MonsterDataList
	{
		get
		{
			return this.mMonsterDataList;
		}
	}

	// Token: 0x06003A7C RID: 14972 RVA: 0x000FC054 File Offset: 0x000FA254
	public int GetMonsterGroupCount()
	{
		for (int i = this.mMonsterDataList.Count - 1; i >= 0; i--)
		{
			if (this.mMonsterDataList[i].Group != GameDefine.MISSION_NPC_GROUP_VAL)
			{
				return this.mMonsterDataList[i].Group;
			}
		}
		return 0;
	}

	// Token: 0x06003A7D RID: 14973 RVA: 0x000FC0B0 File Offset: 0x000FA2B0
	public List<int> GetMonsterGroupList()
	{
		List<int> list = new List<int>();
		if (this.mMonsterDataList == null)
		{
			return list;
		}
		for (int i = 0; i < this.mMonsterDataList.Count; i++)
		{
			if (!list.Contains(this.mMonsterDataList[i].Group) && this.mMonsterDataList[i].Group < 10000)
			{
				list.Add(this.mMonsterDataList[i].Group);
			}
		}
		return list;
	}

	// Token: 0x06003A7E RID: 14974 RVA: 0x000FC13C File Offset: 0x000FA33C
	public List<MonsterData> GetMonsterDataByGroup(int group)
	{
		List<MonsterData> list = new List<MonsterData>();
		for (int i = 0; i < this.mMonsterDataList.Count; i++)
		{
			if (this.mMonsterDataList[i].Group == group)
			{
				list.Add(this.mMonsterDataList[i]);
			}
		}
		return list;
	}

	// Token: 0x17000F76 RID: 3958
	// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000FC198 File Offset: 0x000FA398
	public List<ActivityMapData> CurActivityMapDataList
	{
		get
		{
			if (this.mCurActivityMapDataList == null || this.mCurActivityMapDataList.Count == 0)
			{
				if (this.CurrentMapInofData == null)
				{
					return null;
				}
				this.mCurActivityMapDataList = DataManager.GetAcitvityMapDataByMapId(this.CurrentMapInofData.ID);
			}
			return this.mCurActivityMapDataList;
		}
	}

	// Token: 0x17000F77 RID: 3959
	// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000FC1F0 File Offset: 0x000FA3F0
	public List<string> CurAvailableMissionIdList
	{
		get
		{
			if (this.mCurAvailableMissionIdList == null)
			{
				this.InitCurAvailableMissionList();
			}
			return this.mCurAvailableMissionIdList;
		}
	}

	// Token: 0x17000F78 RID: 3960
	// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000FC20C File Offset: 0x000FA40C
	public Dictionary<string, List<string>> CurMissionNpcDic
	{
		get
		{
			if (this.mCurMissionNpcDic == null)
			{
				this.InitCurAvailableMissionList();
			}
			return this.mCurMissionNpcDic;
		}
	}

	// Token: 0x17000F79 RID: 3961
	// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000FC228 File Offset: 0x000FA428
	public List<GameObject> ExitPointList
	{
		get
		{
			return this.mExitPointList;
		}
	}

	// Token: 0x06003A83 RID: 14979 RVA: 0x000FC230 File Offset: 0x000FA430
	public void InitCurAvailableMissionList()
	{
		if (this.mCurAvailableMissionIdList == null)
		{
			this.mCurAvailableMissionIdList = new List<string>();
			this.mCurMissionNpcDic = new Dictionary<string, List<string>>();
		}
		List<string> list = new List<string>(this.mCurMissionNpcDic.Keys);
		this.mCurAvailableMissionIdList.Clear();
		this.mCurMissionNpcDic.Clear();
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		List<MissionData> acceptMissionDataByMapId = DataManager.GetAcceptMissionDataByMapId(this.mapInfoData.ID);
		if (acceptMissionDataByMapId != null)
		{
			for (int i = 0; i < acceptMissionDataByMapId.Count; i++)
			{
				if (missionManager.IsMissionAcceptable(acceptMissionDataByMapId[i].ID))
				{
					this.mCurAvailableMissionIdList.Add(acceptMissionDataByMapId[i].ID);
					if (!string.IsNullOrEmpty(acceptMissionDataByMapId[i].Accept))
					{
						if (this.mCurMissionNpcDic.ContainsKey(acceptMissionDataByMapId[i].Accept))
						{
							this.mCurMissionNpcDic[acceptMissionDataByMapId[i].Accept].Add(acceptMissionDataByMapId[i].ID);
						}
						else
						{
							this.mCurMissionNpcDic.Add(acceptMissionDataByMapId[i].Accept, new List<string>());
							this.mCurMissionNpcDic[acceptMissionDataByMapId[i].Accept].Add(acceptMissionDataByMapId[i].ID);
						}
					}
				}
			}
		}
		List<string> allMissionId = missionManager.GetAllMissionId();
		string text = string.Empty;
		string text2 = string.Empty;
		int j = 0;
		while (j < allMissionId.Count)
		{
			MISSION_STATE missionState = missionManager.GetMissionState(allMissionId[j]);
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[j]);
			if (missionState == MISSION_STATE.ACCEPTED)
			{
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
				{
					SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
					text2 = surveyMissionDataById.SceneID;
					text = string.Empty;
				}
				else
				{
					text2 = missionDataByID.TargetMapId;
					text = missionDataByID.Target;
				}
				goto IL_207;
			}
			if (missionState == MISSION_STATE.COMPLETE)
			{
				text2 = missionDataByID.SubmitMapId;
				text = missionDataByID.Submit;
				goto IL_207;
			}
			IL_2AD:
			j++;
			continue;
			IL_207:
			if (string.IsNullOrEmpty(text2))
			{
				goto IL_2AD;
			}
			if (!text2.Equals(this.mapInfoData.ID))
			{
				goto IL_2AD;
			}
			this.mCurAvailableMissionIdList.Add(missionDataByID.ID);
			if (string.IsNullOrEmpty(text))
			{
				goto IL_2AD;
			}
			if (this.mCurMissionNpcDic.ContainsKey(text))
			{
				this.mCurMissionNpcDic[text].Add(missionDataByID.ID);
				goto IL_2AD;
			}
			this.mCurMissionNpcDic.Add(text, new List<string>());
			this.mCurMissionNpcDic[text].Add(missionDataByID.ID);
			goto IL_2AD;
		}
		List<string> list2 = new List<string>(this.mCurMissionNpcDic.Keys);
		ObjManager instance = Singleton<ObjManager>.Instance;
		for (int k = 0; k < list2.Count; k++)
		{
			ObjNPC objNPC = instance.FindMissionNpcInScene(list2[k]);
			if (objNPC != null)
			{
				objNPC.UpdateMissionNpcHead();
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			if (!this.mCurMissionNpcDic.ContainsKey(list[l]))
			{
				ObjNPC objNPC = instance.FindMissionNpcInScene(list[l]);
				if (objNPC != null)
				{
					objNPC.UpdateMissionNpcHead();
				}
			}
		}
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.UpdateActionMapObj();
		}
		this.RefershMapActivity();
	}

	// Token: 0x06003A84 RID: 14980 RVA: 0x000FC5E4 File Offset: 0x000FA7E4
	public void UpdateDamgeBoadScale()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData != null)
		{
			if (playerData.ViewType == CameraController.CAMERAVIEWSTATE.FREE)
			{
				this.DamageBoadRoot.transform.localScale = Vector3.one * 0.01f;
			}
			else
			{
				this.DamageBoadRoot.transform.localScale = Vector3.one * 0.02f;
			}
		}
	}

	// Token: 0x06003A85 RID: 14981 RVA: 0x000FC654 File Offset: 0x000FA854
	public virtual void Init(string id)
	{
		this.mapInfoData = DataManager.GetMapInfoDataByID(id);
		this.IsHaveMoveTarget = false;
		this.mCurActivityMapDataList.Clear();
		if (this.CurrentMapInofData != null)
		{
			this.mCurActivityMapDataList = DataManager.GetAcitvityMapDataByMapId(this.CurrentMapInofData.ID);
		}
		this.mMapActivityRoot = GameObject.Find("MapActivityRoot");
		if (this.mMapActivityRoot == null)
		{
			this.mMapActivityRoot = (ResourcesManager.LoadAndInstantiate("Items/MapActivityRoot") as GameObject);
			this.mMapActivityRoot.name = "MapActivityRoot";
		}
		if (this.mMapActivityManager == null)
		{
			this.mMapActivityManager = this.mMapActivityRoot.AddComponent<MapActivityManager>();
		}
		this.mMapActivityManager.Reset();
		this.mMonsterDataList = DataManager.GetMonsterDataListByMapId(id);
		this.NameBoardRoot = GameObject.Find("NameBoardRoot");
		if (this.NameBoardRoot == null)
		{
			this.NameBoardRoot = (ResourcesManager.LoadAndInstantiate("UIRoot/NameBoardRoot") as GameObject);
		}
		this.DamageBoadRoot = GameObject.Find("DamageBoadRoot");
		if (this.DamageBoadRoot == null)
		{
			this.DamageBoadRoot = (ResourcesManager.LoadAndInstantiate("UIRoot/DamageBoadRoot") as GameObject);
			this.DamageBoadRoot.transform.localScale = Vector3.one * 0.02f;
		}
		this.UpdateDamgeBoadScale();
		if (this.mDamageBoardManger == null)
		{
			this.mDamageBoardManger = this.DamageBoadRoot.AddComponent<DamageBoardManager>();
		}
		this.mNameBoadPool = new GameObjectPool("headInfo", 128);
		this.mSimpleShadowPool = new GameObjectPool("SimpleShadow", 64);
		this.DropItemRoot = GameObject.Find("DropItemRoot");
		if (this.DropItemRoot == null)
		{
			this.DropItemRoot = (ResourcesManager.LoadAndInstantiate("UIRoot/DropItemRoot") as GameObject);
		}
		this.mDropItemPool = new GameObjectPool("DropItem", 64);
		if (this.mUIItemPool == null)
		{
			this.mUIItemPool = new GameObjectPool("UIItem", 64);
		}
		Singleton<SurveyItemManager>.Instance.InitSurveyItem(id);
		if (this.CurrentMapInofData.ChangeLightMap == 1)
		{
			this.InitLightMap();
		}
		if (!string.IsNullOrEmpty(this.CurrentMapInofData.SaftyAreaId))
		{
			this.mSaftyAreaData = DataManager.GetMapAreaInfoDataListById(this.CurrentMapInofData.SaftyAreaId);
			this.isHaveSaftyArea = true;
		}
		else
		{
			this.isHaveSaftyArea = false;
		}
		if (!string.IsNullOrEmpty(this.CurrentMapInofData.GatherAreaId))
		{
			this.mGatherAreaData = DataManager.GetMapAreaInfoDataListById(this.CurrentMapInofData.GatherAreaId);
		}
		this.mCurCityDanceData = null;
		this.ClearActivityObjInfo();
		if (SingletonDontDestoryUnity<GameManager>.Instance.FirstEnterGame)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FirstEnterGame = false;
			if (!GameManager.IsInitBiling)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.CreateBilling();
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.QueryInventory();
			}
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_activity_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_dance_state_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_guild_map_info>(null, null);
			if (GameManager.IsSupportCurDataVersion145())
			{
				NetLogic.GetInstance().Send<Protocol.req_guild_battle_state>(null, null);
				NetLogic.GetInstance().Send<Protocol.req_guild_battle_member>(null, null);
			}
			NetLogic.GetInstance().Send<Protocol.request_rank_pvp_data>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_wild_boss_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_guild_boss>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_mount_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_30_day_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_invest_pack>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_active>(null, null);
			NetLogic.GetInstance().Send<Protocol.req_level_reward>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_retrieve_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_buy>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_week_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.require_vip_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_domin_info>(null, null);
			request_dance_info.request request = new request_dance_info.request();
			request.type = 1L;
			NetLogic.GetInstance().Send<Protocol.request_dance_info>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckUnlockSideMission();
			SingletonDontDestoryUnity<GameManager>.Instance.SetNextServerRefreshTime();
		}
		else
		{
			this.CheckSceneActivity();
		}
		this.CheckSceneActivityObject();
		if (this.IsBigWorld() || this.IsTutorialScene())
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/CityTeleport") as GameObject;
			gameObject.transform.position = new Vector3(this.CurrentMapInofData.TelePortPosVector3.x, SceneManager.GetHitHeight(this.CurrentMapInofData.TelePortPosVector3.x, this.CurrentMapInofData.TelePortPosVector3.z) + 0.1f, this.CurrentMapInofData.TelePortPosVector3.z);
			this.InitCurAvailableMissionList();
		}
		if (!string.IsNullOrEmpty(this.CurrentMapInofData.ExitPos))
		{
			List<Vector3> exitPosList = this.CurrentMapInofData.ExitPosList;
			GameObject gameObject2 = ResourcesManager.LoadAndInstantiate("Items/City_jinRu") as GameObject;
			ActivityPoint component = gameObject2.GetComponent<ActivityPoint>();
			this.mExitPointList.Add(gameObject2);
			component.transform.position = exitPosList[0];
			component.ResetExit(new ActivityPoint.OnArrivePointDelegate(this.LeaveCopyScene));
			for (int i = 1; i < exitPosList.Count; i++)
			{
				GameObject gameObject3 = Object.Instantiate(gameObject2) as GameObject;
				ActivityPoint component2 = gameObject3.GetComponent<ActivityPoint>();
				gameObject3.transform.position = exitPosList[i];
				this.mExitPointList.Add(gameObject3);
				component2.ResetExit(new ActivityPoint.OnArrivePointDelegate(this.LeaveCopyScene));
			}
		}
		this.mLoadingFlag = true;
		SingletonUnity<MyEvent>.Instance.Register("OnLoadingOver", this, "OnLoadingOver");
		GameManager.IsSceneReady = false;
		if (this.mapInfoData.SceneName.Equals("FB_pkTai_1"))
		{
			GameObject gameObject4 = new GameObject();
			gameObject4.transform.parent = null;
			gameObject4.transform.position = new Vector3(2.6f, 0f, 8.6f);
			gameObject4.transform.eulerAngles = new Vector3(0f, 196.5f, 0f);
			gameObject4.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic = new FakeObjLogic();
			fakeObjLogic.InitAnimaFakeNpcObj("NPC_Nv_004", gameObject4.transform, "tiaowu");
			GameObject gameObject5 = new GameObject();
			gameObject5.transform.parent = null;
			gameObject5.transform.position = new Vector3(1.96f, 0f, -9.45f);
			gameObject5.transform.eulerAngles = new Vector3(0f, 335.5f, 0f);
			gameObject5.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic2 = new FakeObjLogic();
			fakeObjLogic2.InitAnimaFakeNpcObj("NPC_Nv_004", gameObject5.transform, "tiaowu");
			GameObject gameObject6 = new GameObject();
			gameObject6.transform.parent = null;
			gameObject6.transform.position = new Vector3(-0.4f, 0f, 8.6f);
			gameObject6.transform.eulerAngles = new Vector3(0f, 171.9f, 0f);
			gameObject6.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic3 = new FakeObjLogic();
			fakeObjLogic3.InitAnimaFakeNpcObj("NPC_Nv_005", gameObject6.transform, "tiaowu");
			GameObject gameObject7 = new GameObject();
			gameObject7.transform.parent = null;
			gameObject7.transform.position = new Vector3(-3.91f, 0f, -8.05f);
			gameObject7.transform.eulerAngles = new Vector3(0f, 25.6f, 0f);
			gameObject7.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic4 = new FakeObjLogic();
			fakeObjLogic4.InitAnimaFakeNpcObj("NPC_Nv_005", gameObject7.transform, "tiaowu");
		}
		else if (this.mapInfoData.SceneName.Equals("FB_jingJiChang_1"))
		{
			GameObject gameObject8 = new GameObject();
			gameObject8.transform.parent = null;
			gameObject8.transform.position = new Vector3(9.01f, 0.58f, 7.93f);
			gameObject8.transform.eulerAngles = new Vector3(0f, 219.6f, 0f);
			gameObject8.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic5 = new FakeObjLogic();
			fakeObjLogic5.InitAnimaFakeNpcObj("NPC_Nv_004", gameObject8.transform, "tiaowu");
			GameObject gameObject9 = new GameObject();
			gameObject9.transform.parent = null;
			gameObject9.transform.position = new Vector3(-6.32f, 0.58f, 9.13f);
			gameObject9.transform.eulerAngles = new Vector3(0f, 143.5f, 0f);
			gameObject9.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic6 = new FakeObjLogic();
			fakeObjLogic6.InitAnimaFakeNpcObj("NPC_Nv_004", gameObject9.transform, "tiaowu");
			GameObject gameObject10 = new GameObject();
			gameObject10.transform.parent = null;
			gameObject10.transform.position = new Vector3(4.35f, 1.61f, 16.52f);
			gameObject10.transform.eulerAngles = new Vector3(0f, 184.7f, 0f);
			gameObject10.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic7 = new FakeObjLogic();
			fakeObjLogic7.InitAnimaFakeNpcObj("NPC_Nv_005", gameObject10.transform, "tiaowu");
			GameObject gameObject11 = new GameObject();
			gameObject11.transform.parent = null;
			gameObject11.transform.position = new Vector3(-7.82f, 0.58f, 8.26f);
			gameObject11.transform.eulerAngles = new Vector3(0f, 122.3f, 0f);
			gameObject11.transform.localScale = Vector3.one;
			FakeObjLogic fakeObjLogic8 = new FakeObjLogic();
			fakeObjLogic8.InitAnimaFakeNpcObj("NPC_Nv_005", gameObject11.transform, "tiaowu");
		}
		GameObject gameObject12 = GameObject.Find("anQuanQu");
		if (gameObject12 != null)
		{
			UnityVersionUtil.SetActiveRecursive(gameObject12, false);
		}
		if (this.isHaveSaftyArea)
		{
			GameObject gameObject13 = ResourcesManager.LoadAndInstantiate("Items/SafeAreaObj") as GameObject;
			for (int j = 0; j < this.mSaftyAreaData.Count; j++)
			{
				if (this.mSaftyAreaData[j].Type == 0)
				{
					GameObject gameObject14 = Object.Instantiate(gameObject13) as GameObject;
					float num = (this.mSaftyAreaData[j].PointList[0].x + this.mSaftyAreaData[j].PointList[2].x) / 2f;
					float num2 = (this.mSaftyAreaData[j].PointList[0].y + this.mSaftyAreaData[j].PointList[2].y) / 2f;
					Vector3 position;
					position..ctor(num, SceneManager.GetHitHeight(num, num2) + 0.2f, num2);
					Vector3 vector;
					vector..ctor(this.mSaftyAreaData[j].PointList[3].x - this.mSaftyAreaData[j].PointList[0].x, 0f, this.mSaftyAreaData[j].PointList[3].y - this.mSaftyAreaData[j].PointList[0].y);
					Vector3 vector2;
					vector2..ctor(this.mSaftyAreaData[j].PointList[1].x - this.mSaftyAreaData[j].PointList[0].x, 0f, this.mSaftyAreaData[j].PointList[1].y - this.mSaftyAreaData[j].PointList[0].y);
					float magnitude = vector.magnitude;
					float magnitude2 = vector2.magnitude;
					gameObject14.transform.position = position;
					gameObject14.transform.forward = vector.normalized;
					gameObject14.transform.eulerAngles = new Vector3(0f, gameObject14.transform.eulerAngles.y, 0f);
					gameObject14.transform.localScale = new Vector3(magnitude2, 10f, magnitude);
				}
			}
			Object.Destroy(gameObject13);
		}
		this.UpdateKillTargetMission(id);
		this.UpdateTargetCarMission(id);
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.AUTO_FIGHT))
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoComabat = false;
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsOpenAutoCombat = false;
		}
	}

	// Token: 0x06003A86 RID: 14982 RVA: 0x000FD354 File Offset: 0x000FB554
	public void UpdateArriveTargetPoint()
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		if (!missionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.ARRIVE_TARGET))
		{
			for (int i = 0; i < this.mMissionPathPointList.Count; i++)
			{
				UnityVersionUtil.SetActiveRecursive(this.mMissionPathPointList[i].gameObject, false);
			}
			return;
		}
		List<CurMission> curMissionList = missionManager.GetCurMissionList();
		List<MissionData> list = new List<MissionData>();
		List<CurMission> list2 = new List<CurMission>();
		for (int j = 0; j < curMissionList.Count; j++)
		{
			if (curMissionList[j].MissionState == MISSION_STATE.ACCEPTED)
			{
				MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[j].MissionId);
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
				{
					MoveTargetMissionData moveTargetMissionDataById = DataManager.GetMoveTargetMissionDataById(missionDataByID.LogicID);
					if (moveTargetMissionDataById.MapId.Equals(this.CurrentMapInofData.ID))
					{
						list.Add(missionDataByID);
						list2.Add(curMissionList[j]);
					}
				}
			}
		}
		int num = list2.Count - this.mMissionPathPointList.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject;
				MovePathPoint component = gameObject.GetComponent<MovePathPoint>();
				component.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnPlayerArriveMissionPoint));
				component.ActiveRadius = 8f;
				this.mMissionPathPointList.Add(component);
			}
		}
		for (int l = 0; l < this.mMissionPathPointList.Count; l++)
		{
			if (l < list2.Count)
			{
				this.mMissionPathPointList[l].transform.position = missionManager.GetMoveTargetMissionPos(list2[l].MissionId);
				UnityVersionUtil.SetActiveRecursive(this.mMissionPathPointList[l].gameObject, true);
				if (l == 0)
				{
					this.SetMoveTarget(this.mMissionPathPointList[l].transform.position, list2[l].MissionId);
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.mMissionPathPointList[l].gameObject, false);
			}
		}
	}

	// Token: 0x06003A87 RID: 14983 RVA: 0x000FD58C File Offset: 0x000FB78C
	public void OnPlayerArriveMissionPoint(Vector3 pos)
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		if (!missionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.ARRIVE_TARGET))
		{
			return;
		}
		List<CurMission> curMissionList = missionManager.GetCurMissionList();
		List<MissionData> list = new List<MissionData>();
		List<CurMission> list2 = new List<CurMission>();
		for (int i = 0; i < curMissionList.Count; i++)
		{
			if (curMissionList[i].MissionState == MISSION_STATE.ACCEPTED)
			{
				MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[i].MissionId);
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.ARRIVE_TARGET)
				{
					list.Add(missionDataByID);
					list2.Add(curMissionList[i]);
				}
			}
		}
		int num = 0;
		float num2 = float.MaxValue;
		for (int j = 0; j < list2.Count; j++)
		{
			float num3 = Vector3.SqrMagnitude(missionManager.GetMoveTargetMissionPos(list2[j].MissionId) - pos);
			if (num3 < num2)
			{
				num2 = num3;
				num = j;
			}
		}
		if (list2.Count <= num)
		{
			return;
		}
		string missionId = list2[num].MissionId;
		local_npc_die.request request = new local_npc_die.request();
		request.npcid = missionId;
		request.type = 4L;
		NetLogic.GetInstance().Send<Protocol.local_npc_die>(request, null);
	}

	// Token: 0x06003A88 RID: 14984 RVA: 0x000FD6C4 File Offset: 0x000FB8C4
	public virtual void OnLoadingOver()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnLoadingOver", this, "OnLoadingOver");
		NetLogic.GetInstance().Send<Protocol.map_ready>(null, null);
		LoadingWindow.isSendMapReady = true;
		this.mLoadingFlag = false;
		if (this.IsCanShowCheckPopUI())
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckPopTipsUI();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ClearReshowUI();
		}
		this.UpdateArriveTargetPoint();
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x000FD730 File Offset: 0x000FB930
	public virtual void Update()
	{
		this.timeCount += Time.deltaTime;
		if (this.timeCount > 1f)
		{
			this.timeCount = 0f;
			this.CheckKillTargetMission();
			this.CheckTargetCarMission();
		}
	}

	// Token: 0x06003A8A RID: 14986 RVA: 0x000FD76C File Offset: 0x000FB96C
	public void UpdateKillTargetMission(string sceneId)
	{
		this.mIsHaveKillTargetMission = false;
		this.mKillTargetMissionList.Clear();
		this.mKillTargetMissionData.Clear();
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		List<CurMission> curMissionList = missionManager.GetCurMissionList();
		for (int i = 0; i < curMissionList.Count; i++)
		{
			if (curMissionList[i].MissionState == MISSION_STATE.ACCEPTED)
			{
				MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[i].MissionId);
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
				{
					KillTargetMissionData killTargetMissionDataById = DataManager.GetKillTargetMissionDataById(missionDataByID.LogicID);
					if (killTargetMissionDataById != null && killTargetMissionDataById.SceneID.Equals(sceneId))
					{
						this.mIsHaveKillTargetMission = true;
						this.mKillTargetMissionList.Add(curMissionList[i]);
						this.mKillTargetMissionData.Add(killTargetMissionDataById);
					}
				}
			}
		}
		if (this.mKillTargetNpcDic.Count > 0)
		{
			List<string> list = new List<string>(this.mKillTargetNpcDic.Keys);
			ObjManager instance = Singleton<ObjManager>.Instance;
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < curMissionList.Count; k++)
				{
					if (list[j].Equals(curMissionList[k].MissionId))
					{
					}
				}
				List<long> list2 = this.mKillTargetNpcDic[list[j]];
				if (this.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR)
				{
					for (int l = list2.Count - 1; l >= 0; l--)
					{
						ObjCharacter objCharacter = instance.FindObjInScene(list2[l]);
						if (objCharacter != null)
						{
							ObjRagdollNPC objRagdollNPC = objCharacter as ObjRagdollNPC;
							objRagdollNPC.RecycleSelf();
						}
					}
				}
				this.mKillTargetNpcDic.Remove(list[j]);
			}
		}
	}

	// Token: 0x06003A8B RID: 14987 RVA: 0x000FD94C File Offset: 0x000FBB4C
	private void CheckKillTargetMission()
	{
		if (!this.mIsHaveKillTargetMission)
		{
			return;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer == null)
		{
			return;
		}
		for (int i = 0; i < this.mKillTargetMissionData.Count; i++)
		{
			if (!this.mKillTargetNpcDic.ContainsKey(this.mKillTargetMissionList[i].MissionId) && Vector3.Distance(mainPlayer.Position, this.mKillTargetMissionData[i].Pos) < this.mFlashKillTargetNpcDis && this.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR && SingletonUnity<CitySimController>.Exists)
			{
				this.mKillTargetNpcDic.Add(this.mKillTargetMissionList[i].MissionId, new List<long>());
				for (int j = 0; j < this.mKillTargetMissionData[i].FlashNum; j++)
				{
					BlockMonsterData curData = new BlockMonsterData(new MonsterData
					{
						MapID = this.CurrentMapInofData.ID,
						NpcID = this.mKillTargetMissionData[i].NpcID,
						PosX = this.mKillTargetMissionData[i].PosX + Random.Range(-this.mKillTargetMissionData[i].Range, this.mKillTargetMissionData[i].Range),
						PosZ = this.mKillTargetMissionData[i].PosZ + Random.Range(-this.mKillTargetMissionData[i].Range, this.mKillTargetMissionData[i].Range),
						PosO = Random.Range(0, 36000)
					}, -1L);
					ObjRagdollNPC npc = SingletonUnity<CitySimController>.Instance.GetNpc(curData);
					if (npc != null)
					{
						this.mKillTargetNpcDic[this.mKillTargetMissionList[i].MissionId].Add(npc.ServerId);
					}
				}
				if (this.mKillTargetNpcDic[this.mKillTargetMissionList[i].MissionId].Count == 0)
				{
					this.mKillTargetNpcDic.Remove(this.mKillTargetMissionList[i].MissionId);
				}
			}
		}
	}

	// Token: 0x06003A8C RID: 14988 RVA: 0x000FDB90 File Offset: 0x000FBD90
	public void OnRecycleNpc(ObjCharacter objCha)
	{
		if (this.mKillTargetNpcDic.Count > 0)
		{
			List<string> list = new List<string>(this.mKillTargetNpcDic.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				if (this.mKillTargetNpcDic[list[i]].Contains(objCha.ServerId))
				{
					this.mKillTargetNpcDic[list[i]].Remove(objCha.ServerId);
					if (this.mKillTargetNpcDic[list[i]].Count <= 0)
					{
						this.mKillTargetNpcDic.Remove(list[i]);
					}
				}
			}
		}
	}

	// Token: 0x06003A8D RID: 14989 RVA: 0x000FDC48 File Offset: 0x000FBE48
	public void UpdateTargetCarMission(string sceneId)
	{
		this.mIsHaveTargetCarMission = false;
		this.mTargetCarMissionList.Clear();
		this.mTargetCarMissionDataList.Clear();
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		List<CurMission> curMissionList = missionManager.GetCurMissionList();
		for (int i = 0; i < curMissionList.Count; i++)
		{
			if (curMissionList[i].MissionState == MISSION_STATE.ACCEPTED)
			{
				MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[i].MissionId);
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.TARGET_ROB_CAR)
				{
					TargetCarMissionData targetCarMissionDataById = DataManager.GetTargetCarMissionDataById(missionDataByID.LogicID);
					if (targetCarMissionDataById != null && targetCarMissionDataById.SceneID.Equals(sceneId))
					{
						this.mIsHaveTargetCarMission = true;
						this.mTargetCarMissionList.Add(curMissionList[i]);
						this.mTargetCarMissionDataList.Add(targetCarMissionDataById);
					}
				}
			}
		}
		if (this.mTargetCarDic.Count > 0)
		{
			List<string> list = new List<string>(this.mTargetCarDic.Keys);
			ObjManager instance = Singleton<ObjManager>.Instance;
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < curMissionList.Count; k++)
				{
					if (list[j].Equals(curMissionList[k].MissionId))
					{
					}
				}
				long id = this.mTargetCarDic[list[j]];
				if (this.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR)
				{
					Obj obj = instance.FindObj(id);
					if (obj != null)
					{
						ObjFakeAICar component = obj.GetComponent<ObjFakeAICar>();
						component.PlayerCar.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
					}
				}
				this.mTargetCarDic.Remove(list[j]);
			}
		}
	}

	// Token: 0x06003A8E RID: 14990 RVA: 0x000FDE10 File Offset: 0x000FC010
	private void CheckTargetCarMission()
	{
		if (!this.mIsHaveTargetCarMission)
		{
			return;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer == null)
		{
			return;
		}
		for (int i = 0; i < this.mTargetCarMissionDataList.Count; i++)
		{
			if (!this.mTargetCarDic.ContainsKey(this.mTargetCarMissionList[i].MissionId) && Vector3.Distance(mainPlayer.Position, this.mTargetCarMissionDataList[i].Pos) < this.mFlashTargetCarDis && this.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR && SingletonUnity<CitySimController>.Exists)
			{
				this.mTargetCarDic.Add(this.mTargetCarMissionList[i].MissionId, 0L);
				BlockCarData blockCarData = new BlockCarData();
				blockCarData.CarId = this.mTargetCarMissionDataList[i].Mountid;
				blockCarData.CarAngle = this.mTargetCarMissionDataList[i].GetRot;
				blockCarData.CarPos = this.mTargetCarMissionDataList[i].Pos;
				this.mTargetCarDic[this.mTargetCarMissionList[i].MissionId] = SingletonUnity<CitySimController>.Instance.GetStaticCar(blockCarData, true).ServerId;
			}
		}
	}

	// Token: 0x06003A8F RID: 14991 RVA: 0x000FDF58 File Offset: 0x000FC158
	public void OnRecycleTargetCar(ObjFakeAICar objCar)
	{
		if (this.mTargetCarDic.Count > 0)
		{
			List<string> list = new List<string>(this.mTargetCarDic.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				if (this.mTargetCarDic[list[i]].Equals(objCar.ServerId))
				{
					this.mTargetCarDic.Remove(list[i]);
				}
			}
		}
	}

	// Token: 0x06003A90 RID: 14992 RVA: 0x000FDFD8 File Offset: 0x000FC1D8
	public bool CheckCarIsMissionCar(ObjFakeAICar nearcar)
	{
		if (!this.mIsHaveTargetCarMission)
		{
			return false;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		return !(mainPlayer == null) && !(nearcar == null) && (this.isShowDoor(nearcar.transform.InverseTransformPoint(mainPlayer.transform.position)) && this.mTargetCarDic.ContainsValue(nearcar.ServerId) && this.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR);
	}

	// Token: 0x06003A91 RID: 14993 RVA: 0x000FE068 File Offset: 0x000FC268
	private bool isShowDoor(Vector3 inversePos)
	{
		return inversePos.x < 3f && inversePos.x > -3f && inversePos.z > -5f && inversePos.z < 5f;
	}

	// Token: 0x06003A92 RID: 14994 RVA: 0x000FE0BC File Offset: 0x000FC2BC
	public void InitLightMap()
	{
		this.dayPic = LightmapSettings.lightmaps[0].lightmapFar;
		this.dayProbes = LightmapSettings.lightProbes;
		this.daySkyBox = RenderSettings.skybox;
		this.curSkyBox = Camera.mainCamera.GetComponent<Skybox>();
	}

	// Token: 0x06003A93 RID: 14995 RVA: 0x000FE104 File Offset: 0x000FC304
	public void ChangeLightMap(bool isDay)
	{
		if (this.mDayState != isDay)
		{
			this.mDayState = isDay;
		}
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		string text = "night";
		LightmapData[] array = new LightmapData[]
		{
			new LightmapData()
		};
		if (isDay)
		{
			array[0].lightmapFar = this.dayPic;
			RenderSettings.skybox = this.daySkyBox;
			if (this.curSkyBox != null)
			{
				this.curSkyBox.material = this.daySkyBox;
			}
			LightmapSettings.lightProbes = this.dayProbes;
			if (SingletonUnity<SceneObjectController>.Exists)
			{
				GameObject[] dayList = SingletonUnity<SceneObjectController>.Instance.DayList;
				GameObject[] nightList = SingletonUnity<SceneObjectController>.Instance.NightList;
				if (dayList != null)
				{
					for (int i = 0; i < dayList.Length; i++)
					{
						if (i == 1)
						{
							NGUITools.SetActive(dayList[i], false);
						}
						else
						{
							NGUITools.SetActive(dayList[i], true);
						}
					}
				}
				if (nightList != null)
				{
					for (int j = 0; j < nightList.Length; j++)
					{
						NGUITools.SetActive(nightList[j], false);
					}
				}
			}
			LightmapSettings.lightmaps = array;
		}
		else
		{
			if (this.nightPic == null)
			{
				this.nightPic = (ResourcesManager.Load(string.Format("{0}_LightmapFar-{1}", this.CurrentMapInofData.SceneName, text)) as Texture2D);
				if (this.nightPic == null && UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.gameObject))
				{
					SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadItem(string.Format("{0}_LightmapFar-{1}", this.CurrentMapInofData.SceneName, text), new BundleManager.LoaditemsFinish(this.LoadLightmapFinish), null, null));
				}
			}
			array[0].lightmapFar = this.nightPic;
			if (this.nightProbes == null)
			{
				this.nightProbes = (ResourcesManager.Load(string.Format("{0}_LightProbes-{1}", this.CurrentMapInofData.SceneName, text)) as LightProbes);
				if (this.nightProbes == null && UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.gameObject))
				{
					SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadItem(string.Format("{0}_LightProbes-{1}", this.CurrentMapInofData.SceneName, text), new BundleManager.LoaditemsFinish(this.LoadLightProbeFinish), null, null));
				}
			}
			LightmapSettings.lightProbes = this.nightProbes;
			if (this.nightSkyBox == null)
			{
				this.nightSkyBox = (ResourcesManager.Load(string.Format("{0}_{1}", this.CurrentMapInofData.SceneName, text)) as Material);
				if (this.nightSkyBox == null && UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.gameObject))
				{
					SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadItem(string.Format("{0}_{1}", this.CurrentMapInofData.SceneName, text), new BundleManager.LoaditemsFinish(this.LoadSkyBoxFinish), null, null));
				}
			}
			RenderSettings.skybox = this.nightSkyBox;
			if (this.curSkyBox != null)
			{
				this.curSkyBox.material = this.nightSkyBox;
			}
			if (SingletonUnity<SceneObjectController>.Exists)
			{
				GameObject[] dayList2 = SingletonUnity<SceneObjectController>.Instance.DayList;
				GameObject[] nightList2 = SingletonUnity<SceneObjectController>.Instance.NightList;
				if (dayList2 != null)
				{
					for (int k = 0; k < dayList2.Length; k++)
					{
						NGUITools.SetActive(dayList2[k], false);
					}
				}
				if (nightList2 != null)
				{
					for (int l = 0; l < nightList2.Length; l++)
					{
						NGUITools.SetActive(nightList2[l], true);
					}
				}
			}
			LightmapSettings.lightmaps = array;
		}
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x000FE4A0 File Offset: 0x000FC6A0
	public void CloseQiQiuRen()
	{
		if (SingletonUnity<SceneObjectController>.Exists)
		{
			GameObject[] dayList = SingletonUnity<SceneObjectController>.Instance.DayList;
			if (dayList.Length >= 2)
			{
				UnityVersionUtil.SetActiveRecursive(dayList[1].gameObject, false);
			}
		}
	}

	// Token: 0x06003A95 RID: 14997 RVA: 0x000FE4DC File Offset: 0x000FC6DC
	private void LoadLightmapFinish(string name, Object obj, object param1 = null, object param2 = null)
	{
		if (obj != null)
		{
			LightmapData[] array = new LightmapData[]
			{
				new LightmapData()
			};
			this.nightPic = (obj as Texture2D);
			array[0].lightmapFar = this.nightPic;
			LightmapSettings.lightmaps = array;
		}
	}

	// Token: 0x06003A96 RID: 14998 RVA: 0x000FE524 File Offset: 0x000FC724
	private void LoadLightProbeFinish(string name, Object obj, object param1 = null, object param2 = null)
	{
		if (obj != null)
		{
			this.nightProbes = (obj as LightProbes);
			LightmapSettings.lightProbes = this.nightProbes;
		}
	}

	// Token: 0x06003A97 RID: 14999 RVA: 0x000FE54C File Offset: 0x000FC74C
	private void LoadSkyBoxFinish(string name, Object obj, object param1 = null, object param2 = null)
	{
		if (obj != null)
		{
			this.nightSkyBox = (obj as Material);
			RenderSettings.skybox = this.nightSkyBox;
			if (this.curSkyBox != null)
			{
				this.curSkyBox.material = this.nightSkyBox;
			}
		}
	}

	// Token: 0x06003A98 RID: 15000 RVA: 0x000FE5A0 File Offset: 0x000FC7A0
	public void RefershMapActivity()
	{
		if (this.mMapActivityRoot != null)
		{
			this.mMapActivityManager.RefershMapInfo(this.CurActivityMapDataList, this.mMapActivityRoot.transform);
		}
	}

	// Token: 0x06003A99 RID: 15001 RVA: 0x000FE5DC File Offset: 0x000FC7DC
	public virtual void AutoFightAction()
	{
	}

	// Token: 0x06003A9A RID: 15002 RVA: 0x000FE5E0 File Offset: 0x000FC7E0
	public bool IsCopyScene()
	{
		return this.mapInfoData.MapType != MAPTYPE.BIG_WORLD;
	}

	// Token: 0x06003A9B RID: 15003 RVA: 0x000FE5F8 File Offset: 0x000FC7F8
	public bool IsCopyShowMap()
	{
		return this.mapInfoData.MapType == MAPTYPE.SCUFFLE_AREA_1 || this.mapInfoData.MapType == MAPTYPE.SCUFFLE_AREA_2 || this.mapInfoData.MapType == MAPTYPE.CAR_CHASE_COPY || this.mapInfoData.MapType == MAPTYPE.TUTORIAL_CAR || this.mapInfoData.MapType == MAPTYPE.SURVIVE_BATTLE1 || this.mapInfoData.MapType == MAPTYPE.SURVIVE_BATTLE2;
	}

	// Token: 0x06003A9C RID: 15004 RVA: 0x000FE674 File Offset: 0x000FC874
	public bool IsSingleCopyScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.SINGLE_KILL_MONSTER_COPY || this.mapInfoData.MapType == MAPTYPE.TUTORIAL_SCENE || this.mapInfoData.MapType == MAPTYPE.SNEAKING_COPY || this.mapInfoData.MapType == MAPTYPE.CLAMBING_TOWER || this.mapInfoData.MapType == MAPTYPE.TUTORIAL_CAR || this.mapInfoData.MapType == MAPTYPE.RANK_PVP || this.mapInfoData.MapType == MAPTYPE.CASH_DAILY_COPY || this.mapInfoData.MapType == MAPTYPE.DOMIN_MAP || this.mapInfoData.MapType == MAPTYPE.ANIMA_EDITOR || this.mapInfoData.MapType == MAPTYPE.SINGLE_RUN_POINT_COPY || this.mapInfoData.MapType == MAPTYPE.CAR_CHASE_COPY;
	}

	// Token: 0x06003A9D RID: 15005 RVA: 0x000FE748 File Offset: 0x000FC948
	public bool IsSingleCopySceneLocal()
	{
		return this.mapInfoData.MapType == MAPTYPE.SINGLE_KILL_MONSTER_COPY || this.mapInfoData.MapType == MAPTYPE.TUTORIAL_SCENE || this.mapInfoData.MapType == MAPTYPE.SNEAKING_COPY || this.mapInfoData.MapType == MAPTYPE.CLAMBING_TOWER || this.mapInfoData.MapType == MAPTYPE.TUTORIAL_CAR || this.mapInfoData.MapType == MAPTYPE.RANK_PVP || this.mapInfoData.MapType == MAPTYPE.ANIMA_EDITOR || this.mapInfoData.MapType == MAPTYPE.SINGLE_RUN_POINT_COPY || this.mapInfoData.MapType == MAPTYPE.CAR_CHASE_COPY || this.mapInfoData.MapType == MAPTYPE.DOMIN_MAP;
	}

	// Token: 0x06003A9E RID: 15006 RVA: 0x000FE808 File Offset: 0x000FCA08
	public bool IsCanUsePotion()
	{
		return this.mapInfoData.MapType != MAPTYPE.RANK_PVP && this.mapInfoData.MapType != MAPTYPE.DOMIN_MAP;
	}

	// Token: 0x06003A9F RID: 15007 RVA: 0x000FE83C File Offset: 0x000FCA3C
	public bool IsRankPvPScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.RANK_PVP;
	}

	// Token: 0x06003AA0 RID: 15008 RVA: 0x000FE854 File Offset: 0x000FCA54
	public bool IsSurviveBattleScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.SURVIVE_BATTLE1 || this.mapInfoData.MapType == MAPTYPE.SURVIVE_BATTLE2;
	}

	// Token: 0x06003AA1 RID: 15009 RVA: 0x000FE880 File Offset: 0x000FCA80
	public bool IsMultiScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.MUTIPLE_KILL_MONSTER_COPY || this.mapInfoData.MapType == MAPTYPE.EXP_DAILY_COPY || this.mapInfoData.MapType == MAPTYPE.BAR_FIGHT_COPY || this.mapInfoData.MapType == MAPTYPE.WILD_BOSS_COPY || this.mapInfoData.MapType == MAPTYPE.GUILD_BOSS_COPY || this.mapInfoData.MapType == MAPTYPE.SCUFFLE_AREA_1 || this.mapInfoData.MapType == MAPTYPE.SCUFFLE_AREA_2 || this.mapInfoData.MapType == MAPTYPE.SINGLE_EXP_DAILY_COPY || this.mapInfoData.MapType == MAPTYPE.MULTI_TOWER_COPY || this.mapInfoData.MapType == MAPTYPE.EQUIP_COPY;
	}

	// Token: 0x06003AA2 RID: 15010 RVA: 0x000FE944 File Offset: 0x000FCB44
	public bool IsShowTeamScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.EQUIP_COPY || this.mapInfoData.MapType == MAPTYPE.MULTI_TOWER_COPY || this.mapInfoData.MapType == MAPTYPE.EXP_DAILY_COPY;
	}

	// Token: 0x06003AA3 RID: 15011 RVA: 0x000FE988 File Offset: 0x000FCB88
	public bool IsExpDailyCopy()
	{
		return this.mapInfoData.MapType == MAPTYPE.EXP_DAILY_COPY || this.mapInfoData.MapType == MAPTYPE.SINGLE_EXP_DAILY_COPY;
	}

	// Token: 0x06003AA4 RID: 15012 RVA: 0x000FE9B4 File Offset: 0x000FCBB4
	public bool IsEquipCopy()
	{
		return this.mapInfoData.MapType == MAPTYPE.EQUIP_COPY;
	}

	// Token: 0x06003AA5 RID: 15013 RVA: 0x000FE9C8 File Offset: 0x000FCBC8
	public bool IsRealPvPScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.REAL_PVP;
	}

	// Token: 0x06003AA6 RID: 15014 RVA: 0x000FE9E0 File Offset: 0x000FCBE0
	public bool IsPvPScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.REAL_PVP || this.mapInfoData.MapType == MAPTYPE.RANK_PVP;
	}

	// Token: 0x06003AA7 RID: 15015 RVA: 0x000FEA08 File Offset: 0x000FCC08
	public bool IsLowPhoneManager()
	{
		return this.mapInfoData.MapType == MAPTYPE.LOW_PHONE;
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x000FEA20 File Offset: 0x000FCC20
	public bool IsBigWorld()
	{
		return this.mapInfoData.MapType == MAPTYPE.BIG_WORLD;
	}

	// Token: 0x06003AA9 RID: 15017 RVA: 0x000FEA38 File Offset: 0x000FCC38
	public bool IsWildBossScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.WILD_BOSS_COPY;
	}

	// Token: 0x06003AAA RID: 15018 RVA: 0x000FEA50 File Offset: 0x000FCC50
	public bool IsShopScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.SHOP_COPY;
	}

	// Token: 0x06003AAB RID: 15019 RVA: 0x000FEA68 File Offset: 0x000FCC68
	public bool IsScuffleArea()
	{
		return this.mapInfoData.MapType == MAPTYPE.SCUFFLE_AREA_2;
	}

	// Token: 0x06003AAC RID: 15020 RVA: 0x000FEA80 File Offset: 0x000FCC80
	public bool IsCanAttackOtherPlayerScene()
	{
		return this.IsBigWorld() || this.IsScuffleArea() || this.IsGuildBattleScene() || this.IsWildBossScene();
	}

	// Token: 0x06003AAD RID: 15021 RVA: 0x000FEABC File Offset: 0x000FCCBC
	public bool IsGuildBattleScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.GUILD_BATTLE;
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x000FEAD4 File Offset: 0x000FCCD4
	public bool IsCanShowCheckPopUI()
	{
		return this.IsBigWorld() || (this.IsTutorialScene() && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload);
	}

	// Token: 0x06003AAF RID: 15023 RVA: 0x000FEB0C File Offset: 0x000FCD0C
	public bool IsTutorialScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.TUTORIAL_SCENE || this.mapInfoData.MapType == MAPTYPE.TUTORIAL_CAR;
	}

	// Token: 0x06003AB0 RID: 15024 RVA: 0x000FEB40 File Offset: 0x000FCD40
	public bool IsDontSynPostion()
	{
		return !this.IsTutorialScene() && (this.IsSingleCopySceneLocal() || this.IsLowPhoneManager());
	}

	// Token: 0x06003AB1 RID: 15025 RVA: 0x000FEB74 File Offset: 0x000FCD74
	public bool IsCarScene()
	{
		return this.mapInfoData.MapType == MAPTYPE.CAR_CHASE_COPY;
	}

	// Token: 0x06003AB2 RID: 15026 RVA: 0x000FEB8C File Offset: 0x000FCD8C
	public virtual void StartGame()
	{
	}

	// Token: 0x06003AB3 RID: 15027 RVA: 0x000FEB90 File Offset: 0x000FCD90
	public virtual void SuccessMission()
	{
	}

	// Token: 0x06003AB4 RID: 15028 RVA: 0x000FEB94 File Offset: 0x000FCD94
	public virtual void FailMission()
	{
	}

	// Token: 0x06003AB5 RID: 15029 RVA: 0x000FEB98 File Offset: 0x000FCD98
	public virtual void LeaveScene()
	{
		if (!string.IsNullOrEmpty(this.mapInfoData.ExitPos))
		{
			Singleton<ObjManager>.Instance.MainPlayer.MoveTo(this.mapInfoData.ExitPosList[0], 1f, null);
		}
		else
		{
			MessageBoxLogic.OpenOKCancelBox(this.mapInfoData.MExitCon, StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
			{
				NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
			}, null, null, null);
		}
	}

	// Token: 0x06003AB6 RID: 15030 RVA: 0x000FEC28 File Offset: 0x000FCE28
	public void LeaveCopyScene()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x06003AB7 RID: 15031 RVA: 0x000FEC38 File Offset: 0x000FCE38
	public void CompleteMission()
	{
		Singleton<ObjManager>.Instance.MainPlayer.CompleteMissionFlag = true;
		Singleton<ObjManager>.Instance.DisableAllLocalNpcAction();
		Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
	}

	// Token: 0x06003AB8 RID: 15032 RVA: 0x000FEC70 File Offset: 0x000FCE70
	protected ParticleSystem CreateStrike(object data)
	{
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/CarModel/FX_Strike") as GameObject;
		return gameObject.GetComponent<ParticleSystem>();
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x000FEC98 File Offset: 0x000FCE98
	protected void DestroyStrike(ParticleSystem obj)
	{
		Object.Destroy(obj.gameObject);
	}

	// Token: 0x06003ABA RID: 15034 RVA: 0x000FECA8 File Offset: 0x000FCEA8
	public ParticleSystem GetStrike()
	{
		return this.StrikePool.Get(null);
	}

	// Token: 0x06003ABB RID: 15035 RVA: 0x000FECB8 File Offset: 0x000FCEB8
	public void RecycleStrike(ParticleSystem obj)
	{
		this.StrikePool.Recycle(obj);
	}

	// Token: 0x06003ABC RID: 15036 RVA: 0x000FECC8 File Offset: 0x000FCEC8
	public virtual void OnFindPlayer()
	{
	}

	// Token: 0x06003ABD RID: 15037 RVA: 0x000FECCC File Offset: 0x000FCECC
	public bool isPVPScene()
	{
		return this.isHaveSaftyArea;
	}

	// Token: 0x06003ABE RID: 15038 RVA: 0x000FECD4 File Offset: 0x000FCED4
	public bool IsInSafeArea(Vector3 pos)
	{
		if (this.mSaftyAreaData != null)
		{
			for (int i = 0; i < this.mSaftyAreaData.Count; i++)
			{
				if (this.mSaftyAreaData[i].Type == 0)
				{
					if (AreaCheckTool.CheckInRectangle(new Vector2(pos.x, pos.z), this.mSaftyAreaData[i].PointList[0], this.mSaftyAreaData[i].PointList[1], this.mSaftyAreaData[i].PointList[2], this.mSaftyAreaData[i].PointList[3]))
					{
						return true;
					}
				}
				else if (AreaCheckTool.CheckInCircle(new Vector2(pos.x, pos.z), this.mSaftyAreaData[i].PointList[0], this.mSaftyAreaData[0].CircleRange))
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x000FEE04 File Offset: 0x000FD004
	public bool IsInGatherArea(Vector3 pos)
	{
		if (this.mGatherAreaData != null)
		{
			for (int i = 0; i < this.mGatherAreaData.Count; i++)
			{
				if (this.mGatherAreaData[i].Type == 0)
				{
					if (AreaCheckTool.CheckInRectangle(new Vector2(pos.x, pos.z), this.mGatherAreaData[i].PointList[0], this.mGatherAreaData[i].PointList[1], this.mGatherAreaData[i].PointList[2], this.mGatherAreaData[i].PointList[3]))
					{
						return true;
					}
				}
				else if (AreaCheckTool.CheckInCircle(new Vector2(pos.x, pos.z), this.mGatherAreaData[i].PointList[0], this.mGatherAreaData[i].CircleRange))
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x06003AC0 RID: 15040 RVA: 0x000FEF34 File Offset: 0x000FD134
	public void ClearActivityObjInfo()
	{
		this.SceneComponentObjDic.Clear();
		this.SceneComponentObjList.Clear();
		this.NeedShowList.Clear();
		this.CurLoadActivityobjList.Clear();
		this.NeedShowList.Clear();
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x000FEF78 File Offset: 0x000FD178
	public bool CheckSceneActiviyTime(SceneComponentData data)
	{
		return TimeTools.IsTimeRange(data.Starttimes, data.EndTimes);
	}

	// Token: 0x06003AC2 RID: 15042 RVA: 0x000FEF8C File Offset: 0x000FD18C
	public void CheckSceneActivityObject()
	{
		List<SceneComponentData> sceneComponentDataListById = DataManager.GetSceneComponentDataListById(this.CurrentMapInofData.ID);
		if (sceneComponentDataListById == null || sceneComponentDataListById.Count == 0)
		{
			this.CloseActivityObj();
			return;
		}
		List<SceneComponentData> list = new List<SceneComponentData>();
		for (int i = 0; i < sceneComponentDataListById.Count; i++)
		{
			list.Add(sceneComponentDataListById[i]);
		}
		for (int j = list.Count - 1; j >= 0; j--)
		{
			if (this.CheckSceneActiviyTime(list[j]))
			{
				if (list[j].IsDanceShow != 2)
				{
					if (this.mCurCityDanceData == null)
					{
						if (list[j].IsDanceShow == 1)
						{
							list.RemoveAt(j);
						}
					}
					else if (list[j].IsDanceShow == 0)
					{
						list.RemoveAt(j);
					}
				}
			}
			else
			{
				list.RemoveAt(j);
			}
		}
		this.NeedShowList.Clear();
		for (int k = 0; k < list.Count; k++)
		{
			this.NeedShowList.Add(list[k]);
		}
		for (int l = this.SceneComponentObjList.Count - 1; l >= 0; l--)
		{
			bool flag = false;
			for (int m = list.Count - 1; m >= 0; m--)
			{
				if (!string.IsNullOrEmpty(list[m].PrefabName) && list[m].PrefabName.Equals(this.SceneComponentObjList[l]))
				{
					flag = true;
					list.RemoveAt(m);
					break;
				}
			}
			if (!flag)
			{
				if (this.SceneComponentObjDic.ContainsKey(this.SceneComponentObjList[l]))
				{
					if (this.SceneComponentObjDic[this.SceneComponentObjList[l]] != null)
					{
						Object.Destroy(this.SceneComponentObjDic[this.SceneComponentObjList[l]]);
					}
					this.SceneComponentObjDic.Remove(this.SceneComponentObjList[l]);
				}
				this.SceneComponentObjList.RemoveAt(l);
			}
		}
		if (this.CurLoadActivityobjList != null && this.CurLoadActivityobjList.Count > 0)
		{
			for (int n = 0; n < list.Count; n++)
			{
				if (!this.CurLoadActivityobjList.Contains(list[n].PrefabName))
				{
					this.CurLoadActivityDic.Add(list[n].PrefabName, list[n]);
					this.CurLoadActivityobjList.Add(list[n].PrefabName);
				}
			}
		}
		else
		{
			for (int num = 0; num < list.Count; num++)
			{
				this.CurLoadActivityDic.Add(list[num].PrefabName, list[num]);
				this.CurLoadActivityobjList.Add(list[num].PrefabName);
			}
			if (this.CurLoadActivityobjList != null && this.CurLoadActivityobjList.Count > 0)
			{
				this.SceneComponentObjDic.Add(this.CurLoadActivityobjList[0], null);
				this.SceneComponentObjList.Add(this.CurLoadActivityobjList[0]);
				if (UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.gameObject))
				{
					SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadSceneActivityObj(this.CurLoadActivityobjList[0], this.CurLoadActivityDic[this.CurLoadActivityobjList[0]], new BundleManager.OnLoadActObjFinish(this.SceneActivityObjectLoadFinish)));
				}
			}
		}
	}

	// Token: 0x06003AC3 RID: 15043 RVA: 0x000FF348 File Offset: 0x000FD548
	private void SceneActivityObjectLoadFinish(string name, SceneComponentData data, Object obj)
	{
		if (obj == null)
		{
			return;
		}
		if (this.CheckActivityobjList(name))
		{
			GameObject gameObject = Object.Instantiate(obj) as GameObject;
			if (data.StaticCombine)
			{
				gameObject.AddComponent<CombineStatic>();
			}
			if (gameObject == null)
			{
				return;
			}
			if (this.SceneComponentObjDic.ContainsKey(name))
			{
				this.SceneComponentObjDic[name] = gameObject;
			}
			gameObject.transform.name = name;
			gameObject.transform.position = data.FPos;
			gameObject.transform.eulerAngles = data.FAngle;
			gameObject.transform.localScale = data.FScale;
			BundleManager.ResetAllShader(gameObject.transform);
			if (data != null && data.CloseQiqiuFlag && SingletonUnity<SceneObjectController>.Exists)
			{
				GameObject[] dayList = SingletonUnity<SceneObjectController>.Instance.DayList;
				if (dayList.Length >= 2)
				{
					UnityVersionUtil.SetActiveRecursive(dayList[1].gameObject, false);
				}
			}
		}
		if (this.CurLoadActivityobjList != null && this.CurLoadActivityobjList.Count > 0)
		{
			this.CurLoadActivityDic.Remove(this.CurLoadActivityobjList[0]);
			this.CurLoadActivityobjList.RemoveAt(0);
		}
		if (this.CurLoadActivityobjList != null && this.CurLoadActivityobjList.Count > 0)
		{
			this.SceneComponentObjDic.Add(this.CurLoadActivityobjList[0], null);
			this.SceneComponentObjList.Add(this.CurLoadActivityobjList[0]);
			if (UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.gameObject))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadSceneActivityObj(this.CurLoadActivityobjList[0], this.CurLoadActivityDic[this.CurLoadActivityobjList[0]], new BundleManager.OnLoadActObjFinish(this.SceneActivityObjectLoadFinish)));
			}
		}
	}

	// Token: 0x06003AC4 RID: 15044 RVA: 0x000FF51C File Offset: 0x000FD71C
	private bool CheckActivityobjList(string name)
	{
		if (this.NeedShowList != null && this.NeedShowList.Count > 0)
		{
			for (int i = 0; i < this.NeedShowList.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.NeedShowList[i].PrefabName) && this.NeedShowList[i].PrefabName.Equals(name))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06003AC5 RID: 15045 RVA: 0x000FF59C File Offset: 0x000FD79C
	public void CloseActivityObj()
	{
		if (this.SceneComponentObjDic != null && this.SceneComponentObjDic.Count > 0)
		{
			foreach (GameObject gameObject in this.SceneComponentObjDic.Values)
			{
				if (gameObject != null)
				{
					UnityVersionUtil.SetActiveRecursive(gameObject, false);
				}
			}
		}
	}

	// Token: 0x06003AC6 RID: 15046 RVA: 0x000FF630 File Offset: 0x000FD830
	public void CheckSceneActivity()
	{
		CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById("101");
		if (this.CurrentMapInofData.ID.Equals(cityDanceDataById.MapId))
		{
			this.InitCityDanceScene(cityDanceDataById);
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.FirstGuildDanceOpen == 1)
			{
				this.ChangeLightMap(false);
			}
			else
			{
				this.ChangeLightMap(true);
			}
		}
	}

	// Token: 0x06003AC7 RID: 15047 RVA: 0x000FF698 File Offset: 0x000FD898
	public bool IsCiytDanceSceneActive()
	{
		return this.mCityDanceSceneObj != null;
	}

	// Token: 0x17000F7A RID: 3962
	// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000FF6A8 File Offset: 0x000FD8A8
	public CityDanceData CurCityDanceData
	{
		get
		{
			return this.mCurCityDanceData;
		}
	}

	// Token: 0x06003AC9 RID: 15049 RVA: 0x000FF6B0 File Offset: 0x000FD8B0
	public void InitCityDanceScene(CityDanceData data)
	{
		if (this.mCityDanceSceneObj == null)
		{
			this.mCurCityDanceData = data;
			if (UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.gameObject))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadItem("CityDanceScene", new BundleManager.LoaditemsFinish(this.DanceSceneLoadFinish), null, null));
			}
			this.CloseQiQiuRen();
			this.CheckSceneActivityObject();
		}
	}

	// Token: 0x06003ACA RID: 15050 RVA: 0x000FF718 File Offset: 0x000FD918
	private void DanceSceneLoadFinish(string name, Object fakeobj, object param1 = null, object param2 = null)
	{
		if (fakeobj != null)
		{
			this.mCityDanceSceneObj = (Object.Instantiate(fakeobj) as GameObject);
		}
		if (this.mCityDanceSceneObj != null)
		{
			Transform obj = this.mCityDanceSceneObj.transform.FindChild("DSJ_zhuCheng_LingWuXuanZhuan01/DJ_DjTai");
			Transform obj2 = this.mCityDanceSceneObj.transform.FindChild("DSJ_zhuCheng_LingWuXuanZhuan01/NPC_Nv_013");
			Transform obj3 = this.mCityDanceSceneObj.transform.FindChild("DSJ_zhuCheng_LingWuXuanZhuan01/NPC_Nv_014");
			BundleManager.ResetShader(obj);
			BundleManager.ResetShader(obj2);
			BundleManager.ResetShader(obj3);
		}
	}

	// Token: 0x06003ACB RID: 15051 RVA: 0x000FF7AC File Offset: 0x000FD9AC
	public static bool IsInNavmeshArea(Vector3 pos)
	{
		pos.y = 150f;
		RaycastHit raycastHit;
		return Physics.Raycast(pos, Vector3.down, ref raycastHit, 300f, 134217728);
	}

	// Token: 0x06003ACC RID: 15052 RVA: 0x000FF7E4 File Offset: 0x000FD9E4
	public static float GetHitHeight(Vector3 pos)
	{
		pos.y = 150f;
		RaycastHit raycastHit;
		if (Physics.Raycast(pos, Vector3.down, ref raycastHit, 300f, 134217728))
		{
			return raycastHit.point.y;
		}
		return 0f;
	}

	// Token: 0x06003ACD RID: 15053 RVA: 0x000FF830 File Offset: 0x000FDA30
	public static float GetHitHeight(Vector3 pos, out bool isHit)
	{
		pos.y = 150f;
		isHit = false;
		RaycastHit raycastHit;
		if (Physics.Raycast(pos, Vector3.down, ref raycastHit, 300f, 134217728))
		{
			isHit = true;
			return raycastHit.point.y;
		}
		return 0f;
	}

	// Token: 0x06003ACE RID: 15054 RVA: 0x000FF880 File Offset: 0x000FDA80
	public static float GetHitHeight(float x, float z)
	{
		Vector3 pos;
		pos..ctor(x, 0f, z);
		return SceneManager.GetHitHeight(pos);
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x000FF8A4 File Offset: 0x000FDAA4
	public void RemoveCityDanceScene()
	{
		if (this.mCityDanceSceneObj != null)
		{
			Object.Destroy(this.mCityDanceSceneObj);
			this.ChangeLightMap(true);
			Singleton<ObjManager>.Instance.MainPlayer.RemoveDance();
			this.mCityDanceSceneObj = null;
			this.mCurCityDanceData = null;
			this.CheckSceneActivityObject();
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.DisableDanceBtn();
		}
		if (!SingletonDontDestoryUnity<GameManager>.Exists || !SingletonDontDestoryUnity<SoundManager>.Exists)
		{
			return;
		}
		SoundData soundDataById = DataManager.GetSoundDataById(this.CurrentMapInofData.GetAudioID());
		if (soundDataById != null)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlayBGMusic(soundDataById.Id, soundDataById.FadeOutTime, soundDataById.FadeInTime);
		}
	}

	// Token: 0x06003AD0 RID: 15056 RVA: 0x000FF954 File Offset: 0x000FDB54
	public virtual void OnReconnectSuccess()
	{
	}

	// Token: 0x06003AD1 RID: 15057 RVA: 0x000FF958 File Offset: 0x000FDB58
	public virtual void OnNPCDie(object objNpc)
	{
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x000FF95C File Offset: 0x000FDB5C
	public virtual void SetMoveTarget(Vector3 pos, string missionId)
	{
	}

	// Token: 0x06003AD3 RID: 15059 RVA: 0x000FF960 File Offset: 0x000FDB60
	public virtual void ClearMoveTarget()
	{
		this.IsHaveMoveTarget = false;
	}

	// Token: 0x06003AD4 RID: 15060 RVA: 0x000FF96C File Offset: 0x000FDB6C
	public virtual void MissionCheckMoveTarget(string missionId)
	{
	}

	// Token: 0x04002647 RID: 9799
	protected GameObject mUIRoot;

	// Token: 0x04002648 RID: 9800
	protected GameObject mNameBoardRoot;

	// Token: 0x04002649 RID: 9801
	protected GameObject mDamageBoadRoot;

	// Token: 0x0400264A RID: 9802
	protected DamageBoardManager mDamageBoardManger;

	// Token: 0x0400264B RID: 9803
	protected GameObject mMapActivityRoot;

	// Token: 0x0400264C RID: 9804
	protected MapActivityManager mMapActivityManager;

	// Token: 0x0400264D RID: 9805
	protected GameObjectPool mNameBoadPool;

	// Token: 0x0400264E RID: 9806
	protected GameObjectPool mSimpleShadowPool;

	// Token: 0x0400264F RID: 9807
	protected GameObject mDropItemRoot;

	// Token: 0x04002650 RID: 9808
	protected GameObjectPool mDropItemPool;

	// Token: 0x04002651 RID: 9809
	private bool mIsMissionStart;

	// Token: 0x04002652 RID: 9810
	protected GameObjectPool mUIItemPool;

	// Token: 0x04002653 RID: 9811
	protected MapInfoData mapInfoData;

	// Token: 0x04002654 RID: 9812
	protected int mCurSceneReward;

	// Token: 0x04002655 RID: 9813
	public bool isHaveSaftyArea;

	// Token: 0x04002656 RID: 9814
	protected List<MapAreaInfoData> mSaftyAreaData;

	// Token: 0x04002657 RID: 9815
	protected List<MapAreaInfoData> mGatherAreaData;

	// Token: 0x04002658 RID: 9816
	private bool mLoadingFlag;

	// Token: 0x04002659 RID: 9817
	protected List<MonsterData> mMonsterDataList;

	// Token: 0x0400265A RID: 9818
	private List<ActivityMapData> mCurActivityMapDataList = new List<ActivityMapData>();

	// Token: 0x0400265B RID: 9819
	private List<string> mCurAvailableMissionIdList;

	// Token: 0x0400265C RID: 9820
	private Dictionary<string, List<string>> mCurMissionNpcDic;

	// Token: 0x0400265D RID: 9821
	private List<GameObject> mExitPointList = new List<GameObject>();

	// Token: 0x0400265E RID: 9822
	private List<MovePathPoint> mMissionPathPointList = new List<MovePathPoint>();

	// Token: 0x0400265F RID: 9823
	private int messageCount;

	// Token: 0x04002660 RID: 9824
	private float timeCount;

	// Token: 0x04002661 RID: 9825
	private bool mIsHaveKillTargetMission;

	// Token: 0x04002662 RID: 9826
	private List<CurMission> mKillTargetMissionList = new List<CurMission>();

	// Token: 0x04002663 RID: 9827
	private List<KillTargetMissionData> mKillTargetMissionData = new List<KillTargetMissionData>();

	// Token: 0x04002664 RID: 9828
	private float mFlashKillTargetNpcDis = 40f;

	// Token: 0x04002665 RID: 9829
	private Dictionary<string, List<long>> mKillTargetNpcDic = new Dictionary<string, List<long>>();

	// Token: 0x04002666 RID: 9830
	private bool mIsHaveTargetCarMission;

	// Token: 0x04002667 RID: 9831
	private List<CurMission> mTargetCarMissionList = new List<CurMission>();

	// Token: 0x04002668 RID: 9832
	private List<TargetCarMissionData> mTargetCarMissionDataList = new List<TargetCarMissionData>();

	// Token: 0x04002669 RID: 9833
	private float mFlashTargetCarDis = 40f;

	// Token: 0x0400266A RID: 9834
	private Dictionary<string, long> mTargetCarDic = new Dictionary<string, long>();

	// Token: 0x0400266B RID: 9835
	private GameObject mLightFlare;

	// Token: 0x0400266C RID: 9836
	private float ChangeLightMapTimeCount;

	// Token: 0x0400266D RID: 9837
	private float ChangeLightMapTime = 10f;

	// Token: 0x0400266E RID: 9838
	private Texture2D dayPic;

	// Token: 0x0400266F RID: 9839
	private Texture2D nightPic;

	// Token: 0x04002670 RID: 9840
	private LightProbes dayProbes;

	// Token: 0x04002671 RID: 9841
	private LightProbes nightProbes;

	// Token: 0x04002672 RID: 9842
	private Material daySkyBox;

	// Token: 0x04002673 RID: 9843
	private Material nightSkyBox;

	// Token: 0x04002674 RID: 9844
	private Skybox curSkyBox;

	// Token: 0x04002675 RID: 9845
	private bool mDayState = true;

	// Token: 0x04002676 RID: 9846
	public SimplePool<ParticleSystem> StrikePool;

	// Token: 0x04002677 RID: 9847
	private Dictionary<string, GameObject> SceneComponentObjDic = new Dictionary<string, GameObject>();

	// Token: 0x04002678 RID: 9848
	private List<string> SceneComponentObjList = new List<string>();

	// Token: 0x04002679 RID: 9849
	public Dictionary<string, SceneComponentData> CurLoadActivityDic = new Dictionary<string, SceneComponentData>();

	// Token: 0x0400267A RID: 9850
	public List<string> CurLoadActivityobjList = new List<string>();

	// Token: 0x0400267B RID: 9851
	public List<SceneComponentData> NeedShowList = new List<SceneComponentData>();

	// Token: 0x0400267C RID: 9852
	private GameObject mCityDanceSceneObj;

	// Token: 0x0400267D RID: 9853
	private CityDanceData mCurCityDanceData;

	// Token: 0x0400267E RID: 9854
	public bool IsHaveMoveTarget;

	// Token: 0x0400267F RID: 9855
	public Vector3 CurMoveTarget = Vector3.zero;
}
