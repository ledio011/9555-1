using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x02000880 RID: 2176
public class NewTutorialSceneManager : SceneManager
{
	// Token: 0x17000F63 RID: 3939
	// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000F8900 File Offset: 0x000F6B00
	public SmoothFollowNew SmoothFollowCamPos
	{
		get
		{
			return this.mSmoothFollowCamPos;
		}
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x000F8908 File Offset: 0x000F6B08
	public override void Init(string id)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.FirstEnterGame)
		{
			NetLogic.GetInstance().Send<Protocol.request_domin_info>(null, null);
		}
		base.Init(id);
		GameObject gameObject = GameObject.Find("SmoothFollowCamPos");
		if (gameObject != null)
		{
			this.mSmoothFollowCamPos = gameObject.GetComponent<SmoothFollowNew>();
		}
		UnityVersionUtil.SetActiveRecursive(this.mSmoothFollowCamPos.gameObject, false);
		if (SingletonUnity<MyEvent>.Exists)
		{
			SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		}
		else
		{
			Debug.Log("SingletonUnity<MyEvent>.Exists == false");
		}
		this.StrikePool = new SimplePool<ParticleSystem>();
		this.StrikePool.Reset(new SimplePool<ParticleSystem>.CreateFunc(base.CreateStrike), new SimplePool<ParticleSystem>.DestroyFunc(base.DestroyStrike), 10);
		this.mIsTutorialFinish = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsTutorialFinish;
		if (!this.mIsTutorialFinish)
		{
			gameObject = (ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject);
			this.mTutorialMovePathPoint = gameObject.GetComponent<MovePathPoint>();
			this.mTutorialMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnPlayerArrivePathPoint));
			this.mWaveCount = base.GetMonsterGroupCount();
			this.mStepIndex = 0;
			this.mCurEnemyGroup = 0;
			this.mCurPlayerPathIndex = 1;
			UnityVersionUtil.SetActiveRecursive(this.mTutorialMovePathPoint.gameObject, false);
			this.mTutorialMovePathPoint.ActiveRadius = 2f;
			GameObject gameObject2 = ResourcesManager.LoadAndInstantiate("Tutorial/TutorialPlayerPathRoot") as GameObject;
			this.mPlayerPathPointRoot = gameObject2.transform;
			this.mStartSceneAnimaObj = (ResourcesManager.LoadAndInstantiate("StartSceneAnima/kaiChangSceneAnima") as GameObject);
			UnityVersionUtil.SetActiveRecursive(this.mStartSceneAnimaObj, false);
			if (GameSettingData.GetPhoneClass() == 0)
			{
				this.waitTime = 5;
			}
			else
			{
				this.waitTime = 30;
			}
		}
		if (base.CurrentMapInofData.SceneName.Equals("FB_saiDao_1"))
		{
			ResourcesManager.LoadAndInstantiate("Tutorial/CitySimController");
		}
		else
		{
			ResourcesManager.LoadAndInstantiate("Tutorial/CitySimController_GTA");
		}
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.InitMonsterList();
		}
		base.IsMissionStart = true;
		this.mTutorialCar = null;
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x000F8B18 File Offset: 0x000F6D18
	~NewTutorialSceneManager()
	{
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x000F8B50 File Offset: 0x000F6D50
	public void CheckLockArea()
	{
	}

	// Token: 0x060039ED RID: 14829 RVA: 0x000F8B54 File Offset: 0x000F6D54
	private void CreateTutorialCar()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.ROB_CAR_TIP))
		{
			this.TalkNpcPos = GameObject.Find("TalkNpcPos").transform;
			ObjCarInitData initData = new ObjCarInitData(this.TalkNpcPos.position, this.TalkNpcPos.eulerAngles, UUID.GenUUID(), "Chevrolet", false, DataManager.GetMountDataById("Chevrolet"));
			ObjFakeAICar fakeAICar = Singleton<ObjManager>.Instance.GetFakeAICar(initData, false);
			if (fakeAICar != null)
			{
				fakeAICar.ResetStaticCar();
				this.mTutorialCar = fakeAICar;
			}
		}
	}

	// Token: 0x060039EE RID: 14830 RVA: 0x000F8BE8 File Offset: 0x000F6DE8
	private void CreateStartNPC()
	{
		this.TalkNpcPos = GameObject.Find("TalkNpcPos").transform;
		List<MonsterData> list = new List<MonsterData>();
		list.Add(new MonsterData
		{
			MapID = "11",
			Group = 1,
			NpcID = "100",
			PosX = -13100,
			PosZ = 8700,
			PosO = 0
		});
		this.NpcCreate(list);
		this.CreatWalkNpc();
	}

	// Token: 0x060039EF RID: 14831 RVA: 0x000F8C64 File Offset: 0x000F6E64
	private void CreatWalkNpc()
	{
		this.mRandomWalkPosRoot = GameObject.Find("TutorialRandomWalkNpcPos");
		this.mRandomWalkPosList.Clear();
		for (int i = 0; i < this.mRandomWalkPosRoot.transform.childCount; i++)
		{
			this.mRandomWalkPosList.Add(this.mRandomWalkPosRoot.transform.GetChild(i).position);
		}
		ObjManager instance = Singleton<ObjManager>.Instance;
		for (int j = 0; j < 5; j++)
		{
			instance.CreateNPC(new ObjInitNpcData
			{
				mServerID = UUID.GenUUID(),
				mPos = this.mRandomWalkPosList[j],
				mDir = MathUtil.HeadingToVector3((float)Random.Range(0, 360)),
				npcInfoData = DataManager.GetNpcDataByID(this.NpcIdList[j])
			}, new ObjManager.OnGetNPC(this.OnCreateNpc), null);
		}
	}

	// Token: 0x060039F0 RID: 14832 RVA: 0x000F8D48 File Offset: 0x000F6F48
	private void CreatWalkNpc2()
	{
	}

	// Token: 0x060039F1 RID: 14833 RVA: 0x000F8D58 File Offset: 0x000F6F58
	private void CreateWalkNpc2(string npcId)
	{
		ObjManager instance = Singleton<ObjManager>.Instance;
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = this.mRandomWalkPosList[Random.Range(0, this.mRandomWalkPosList.Count)];
		objInitNpcData.mDir = MathUtil.HeadingToVector3((float)Random.Range(0, 360));
		objInitNpcData.npcInfoData = DataManager.GetNpcDataByID(npcId);
		objInitNpcData.MaxHP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.HP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.ATK = objInitNpcData.npcInfoData.Atk;
		objInitNpcData.DEF = objInitNpcData.npcInfoData.Def;
		objInitNpcData.HIT = objInitNpcData.npcInfoData.HIT;
		objInitNpcData.EVA = objInitNpcData.npcInfoData.DGE;
		objInitNpcData.CRI = objInitNpcData.npcInfoData.CRI;
		objInitNpcData.EXD = objInitNpcData.npcInfoData.EXD;
		objInitNpcData.EXR = objInitNpcData.npcInfoData.EXR;
		objInitNpcData.RES = objInitNpcData.npcInfoData.RES;
		objInitNpcData.CRD = objInitNpcData.npcInfoData.CRD;
		objInitNpcData.CRR = objInitNpcData.npcInfoData.CRR;
		objInitNpcData.DEFA = objInitNpcData.npcInfoData.DEFA;
		objInitNpcData.DGEA = objInitNpcData.npcInfoData.DGEA;
		objInitNpcData.HITA = objInitNpcData.npcInfoData.HITA;
		objInitNpcData.RESA = objInitNpcData.npcInfoData.RESA;
		objInitNpcData.CRIA = objInitNpcData.npcInfoData.CRIA;
		objInitNpcData.Level = objInitNpcData.npcInfoData.Lv;
		objInitNpcData.AntiStun = objInitNpcData.npcInfoData.AntiStun;
		objInitNpcData.AntiKnockDown = objInitNpcData.npcInfoData.AntiKnockDown;
		instance.CreateNPC(objInitNpcData, new ObjManager.OnGetNPC(this.OnCreateNpc), null);
	}

	// Token: 0x060039F2 RID: 14834 RVA: 0x000F8F2C File Offset: 0x000F712C
	private void OnCreateNpc(ObjNPC objNpc)
	{
		this.mCurRandomWalkNpcList.Add(objNpc);
		objNpc.WalkMoveTo(this.mRandomWalkPosList[Random.Range(0, this.mRandomWalkPosList.Count)], 1f, new ObjCharacter.TargetArriveFinsh(this.OnArrivePoint));
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x000F8F78 File Offset: 0x000F7178
	private void OnArrivePoint(ObjCharacter objCha)
	{
		if (this.walkNpcHandle.ContainsKey(objCha.ServerId))
		{
			if (this.walkNpcHandle[objCha.ServerId] != null)
			{
				this.walkNpcHandle[objCha.ServerId].Cancel();
			}
			else
			{
				this.walkNpcHandle[objCha.ServerId] = new vp_Timer.Handle();
			}
		}
		else
		{
			this.walkNpcHandle.Add(objCha.ServerId, new vp_Timer.Handle());
		}
		vp_Timer.In((float)Random.Range(0, 4), delegate()
		{
			if (objCha != null && UnityVersionUtil.IsActive(objCha.gameObject) && objCha.NavMeshAgent.enabled)
			{
				objCha.WalkMoveTo(this.mRandomWalkPosList[Random.Range(0, this.mRandomWalkPosList.Count)], 1f, new ObjCharacter.TargetArriveFinsh(this.OnArrivePoint));
			}
		}, this.walkNpcHandle[objCha.ServerId]);
	}

	// Token: 0x060039F4 RID: 14836 RVA: 0x000F905C File Offset: 0x000F725C
	private void ClearWalkNpc()
	{
		GameObject gameObject = GameObject.Find("TutorialEscapeNpcPos");
		for (int i = 0; i < this.mCurRandomWalkNpcList.Count; i++)
		{
			if (this.walkNpcHandle.ContainsKey(this.mCurRandomWalkNpcList[i].ServerId) && this.walkNpcHandle[this.mCurRandomWalkNpcList[i].ServerId] != null)
			{
				this.walkNpcHandle[this.mCurRandomWalkNpcList[i].ServerId].Cancel();
			}
			this.mCurRandomWalkNpcList[i].DisactiveTargetArriveFinish();
			this.mCurRandomWalkNpcList[i].MoveTo(gameObject.transform.GetChild(i).position, 1f, delegate(ObjCharacter npc)
			{
				Singleton<ObjManager>.Instance.RecycleNpc(npc as ObjNPC);
			});
		}
	}

	// Token: 0x060039F5 RID: 14837 RVA: 0x000F9148 File Offset: 0x000F7348
	private void InitBlock(string sceneId)
	{
		List<CarMissionBlockData> carMissionBlockDataListBySceneId = DataManager.GetCarMissionBlockDataListBySceneId(sceneId);
		GameObject gameObject = new GameObject("BlockRoot");
		GameObject gameObject2 = new GameObject("StaticObstacleRoot");
		GameObject gameObject3 = new GameObject("MoveCarBlockRoot");
		gameObject.transform.position = Vector3.zero;
		gameObject.transform.rotation = Quaternion.identity;
		gameObject2.transform.parent = gameObject.transform;
		gameObject3.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject3.transform.localPosition = Vector3.zero;
		gameObject3.transform.localRotation = Quaternion.identity;
		List<KeyValuePair<int, Vector3>> list = new List<KeyValuePair<int, Vector3>>();
		for (int i = 0; i < carMissionBlockDataListBySceneId.Count; i++)
		{
			if (carMissionBlockDataListBySceneId[i].PointType == 0 || carMissionBlockDataListBySceneId[i].PointType == 3 || carMissionBlockDataListBySceneId[i].PointType == 4)
			{
				MoveCarBlock moveCarBlock;
				CarPath carPath;
				if (this.mMoveCarBlockDic.ContainsKey(carMissionBlockDataListBySceneId[i].BlockGroup))
				{
					moveCarBlock = this.mMoveCarBlockDic[carMissionBlockDataListBySceneId[i].BlockGroup];
					carPath = moveCarBlock.Path;
				}
				else
				{
					GameObject gameObject4 = new GameObject(string.Format("Block{0}", carMissionBlockDataListBySceneId[i].BlockGroup));
					gameObject4.transform.parent = gameObject3.transform;
					gameObject4.transform.position = Vector3.zero;
					gameObject4.transform.rotation = Quaternion.identity;
					BoxCollider boxCollider = gameObject4.AddComponent<BoxCollider>();
					boxCollider.size = new Vector3(120f, 5f, 1f);
					boxCollider.isTrigger = true;
					moveCarBlock = gameObject4.AddComponent<MoveCarBlock>();
					this.mMoveCarBlockDic.Add(carMissionBlockDataListBySceneId[i].BlockGroup, moveCarBlock);
					carPath = new GameObject(string.Format("Path", new object[0]))
					{
						transform = 
						{
							parent = gameObject4.transform,
							localPosition = Vector3.zero,
							localRotation = Quaternion.identity
						}
					}.AddComponent<CarPath>();
					moveCarBlock.Path = carPath;
				}
				if (carMissionBlockDataListBySceneId[i].BlockIndex == -1)
				{
					moveCarBlock.transform.position = new Vector3(carMissionBlockDataListBySceneId[i].PosX, carMissionBlockDataListBySceneId[i].PosY, carMissionBlockDataListBySceneId[i].PosZ);
					moveCarBlock.transform.eulerAngles = new Vector3(carMissionBlockDataListBySceneId[i].AngleX, carMissionBlockDataListBySceneId[i].AngleY, carMissionBlockDataListBySceneId[i].AngleZ);
					moveCarBlock.PoliceFlag = carMissionBlockDataListBySceneId[i].PointType;
				}
				else
				{
					CarPathPoint carPathPoint = new GameObject(string.Format("Point{0}", carMissionBlockDataListBySceneId[i].BlockIndex))
					{
						transform = 
						{
							parent = carPath.transform,
							position = new Vector3(carMissionBlockDataListBySceneId[i].PosX, carMissionBlockDataListBySceneId[i].PosY, carMissionBlockDataListBySceneId[i].PosZ),
							eulerAngles = new Vector3(carMissionBlockDataListBySceneId[i].AngleX, carMissionBlockDataListBySceneId[i].AngleY, carMissionBlockDataListBySceneId[i].AngleZ)
						}
					}.AddComponent<CarPathPoint>();
					carPathPoint.Speed = carMissionBlockDataListBySceneId[i].MaxSpeed;
					if (carPath.PathPointList.Count <= carMissionBlockDataListBySceneId[i].BlockIndex)
					{
						carPath.PathPointList.Add(carPathPoint);
					}
					else if (carPath.PathPointList.IndexOf(carPathPoint) != carMissionBlockDataListBySceneId[i].BlockIndex)
					{
						carPath.PathPointList[carMissionBlockDataListBySceneId[i].BlockIndex] = carPathPoint;
					}
				}
			}
			else if (carMissionBlockDataListBySceneId[i].PointType == 1)
			{
				GameObject gameObject5;
				if (this.mStaticBlockDic.ContainsKey(carMissionBlockDataListBySceneId[i].ObstacleName))
				{
					gameObject5 = (Object.Instantiate(this.mStaticBlockDic[carMissionBlockDataListBySceneId[i].ObstacleName]) as GameObject);
				}
				else
				{
					gameObject5 = (ResourcesManager.LoadAndInstantiate(string.Format("CarMissionBlock/{0}", carMissionBlockDataListBySceneId[i].ObstacleName)) as GameObject);
					this.mStaticBlockDic.Add(carMissionBlockDataListBySceneId[i].ObstacleName, gameObject5);
				}
				if (gameObject5 == null)
				{
					Debug.Log("curBlockDataList[i].ObstacleName :: " + carMissionBlockDataListBySceneId[i].ObstacleName);
				}
				gameObject5.transform.parent = gameObject2.transform;
				gameObject5.transform.position = new Vector3(carMissionBlockDataListBySceneId[i].PosX, carMissionBlockDataListBySceneId[i].PosY, carMissionBlockDataListBySceneId[i].PosZ);
				gameObject5.transform.eulerAngles = new Vector3(carMissionBlockDataListBySceneId[i].AngleX, carMissionBlockDataListBySceneId[i].AngleY, carMissionBlockDataListBySceneId[i].AngleZ);
			}
			else if (carMissionBlockDataListBySceneId[i].PointType == 2)
			{
				if (carMissionBlockDataListBySceneId[i].BlockIndex == 0)
				{
					this.mPlayerCarCreatePos = new Vector3(carMissionBlockDataListBySceneId[i].PosX, carMissionBlockDataListBySceneId[i].PosY, carMissionBlockDataListBySceneId[i].PosZ);
					this.mPlayerCarCreateAngle = new Vector3(carMissionBlockDataListBySceneId[i].AngleX, carMissionBlockDataListBySceneId[i].AngleY, carMissionBlockDataListBySceneId[i].AngleZ);
				}
				else
				{
					list.Add(new KeyValuePair<int, Vector3>(carMissionBlockDataListBySceneId[i].BlockIndex, new Vector3(carMissionBlockDataListBySceneId[i].PosX, carMissionBlockDataListBySceneId[i].PosY, carMissionBlockDataListBySceneId[i].PosZ)));
				}
			}
		}
		list.Sort((KeyValuePair<int, Vector3> pre, KeyValuePair<int, Vector3> next) => pre.Key - next.Key);
		this.mCurPlayerCarPathIndex = 0;
		for (int j = 0; j < list.Count; j++)
		{
			this.mPlayerCarPathList.Add(list[j].Value);
		}
	}

	// Token: 0x060039F6 RID: 14838 RVA: 0x000F97F8 File Offset: 0x000F79F8
	public override void OnLoadingOver()
	{
		base.OnLoadingOver();
		this.loadingOverFlag = true;
		if (this.mStartSceneAnimaObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mStartSceneAnimaObj, true);
			this.curAnimaCtl = this.mStartSceneAnimaObj.GetComponent<SceneAnimationCtl>();
			this.curAnimaCtl.RegisterOnFinished(new DelegateDefine.NoParamDelegate(this.OnFinishedStartSceneAnima));
			this.mCamCtl.CurCamera.enabled = false;
			if (SingletonUnity<ScreenBottomBtn>.Exists)
			{
				SingletonUnity<ScreenBottomBtn>.Instance.LockBtn = true;
			}
			UICamera.mainCamera.depth = 2f;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SkipBtnRoot, null, null);
		}
		else if (!this.mIsTutorialFinish)
		{
			this.OnFinishedStartSceneAnima();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
		}
	}

	// Token: 0x060039F7 RID: 14839 RVA: 0x000F98C4 File Offset: 0x000F7AC4
	public void OnFinishedStartSceneAnima()
	{
		this.mCamCtl.CurCamera.enabled = true;
		this.mCamCtl.CurCamera.farClipPlane = 500f;
		if (SingletonUnity<ScreenBottomBtn>.Exists)
		{
			SingletonUnity<ScreenBottomBtn>.Instance.LockBtn = false;
		}
		UICamera.mainCamera.depth = 0f;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SkipBtnRoot);
		this.MoveNextState();
	}

	// Token: 0x060039F8 RID: 14840 RVA: 0x000F9930 File Offset: 0x000F7B30
	public override void StartGame()
	{
		Debug.Log("CarChaseSceneManager StartGame");
	}

	// Token: 0x060039F9 RID: 14841 RVA: 0x000F993C File Offset: 0x000F7B3C
	private void OnArriveCarPathPoint(Vector3 pos)
	{
		this.mCurPlayerCarPathIndex++;
		if (this.mCurPlayerCarPathIndex < this.mPlayerCarPathList.Count)
		{
			this.SetPlayerCarMoveTarget();
		}
		else
		{
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.HideTarget();
			}
			this.MoveNextState();
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "14CarArriveAtPoint_" + (this.mCurPlayerCarPathIndex - 1));
	}

	// Token: 0x060039FA RID: 14842 RVA: 0x000F99D4 File Offset: 0x000F7BD4
	public override void SuccessMission()
	{
	}

	// Token: 0x060039FB RID: 14843 RVA: 0x000F99D8 File Offset: 0x000F7BD8
	public override void FailMission()
	{
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x000F99DC File Offset: 0x000F7BDC
	public override void Update()
	{
		base.Update();
		if (this.mIsTutorialFinish && this.mMainPlayer != null && SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.UpdateCityCheck();
			this.flashTimeCount += Time.deltaTime;
			if (this.flashTimeCount >= 0.5f)
			{
				this.UpdateMapLine();
			}
		}
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x000F9A48 File Offset: 0x000F7C48
	private void FreshNPC()
	{
		if (SingletonUnity<EasyCitySimController>.Exists)
		{
			SingletonUnity<EasyCitySimController>.Instance.FreshNPC(this.mMainPlayer.CacheTransform);
		}
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x000F9A6C File Offset: 0x000F7C6C
	public void MissionMoveToPoint()
	{
		this.mMainPlayer.MoveTo(this.mTutorialMovePathPoint.transform.position, 1f, null);
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x000F9A9C File Offset: 0x000F7C9C
	public void RobCar()
	{
		this.mMainPlayer.rigidbody.isKinematic = true;
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 1f, delegate(ObjCharacter A_1)
		{
			this.mMainPlayer.CameraController.LerpToTargetLocalZero(this.mPlayerCar.CarDoorView, 1f, null);
			this.mMainPlayer.DisableNavMeshAgent();
			this.mMainPlayer.CacheTransform.parent = this.mPlayerCar.DummyPlayerPoint;
			TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, false), 10);
			TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalRotate(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, 0), 10);
			vp_Timer.In(0.1f, delegate()
			{
				this.mPlayerCar.MeshRoot.animation.Play("RobCar");
				FakeObjLogic fakeObj = new FakeObjLogic();
				fakeObj.InitAnimaFakeNpcObj("NPC_Nan_036", this.mPlayerCar.DummyNPCPoint, "RobOffCar");
				this.mMainPlayer.AnimationLogic.PlayAnimation("RobCar", null, -1f);
				vp_Timer.In(this.mMainPlayer.AnimationLogic.CurAnimationLength, delegate()
				{
					this.mMainPlayer.DisableMainPlayer();
					this.MoveNextState();
					this.ChangeToCarCtl();
					Object.Destroy(this.mCarAnimaObj);
					UnityVersionUtil.SetActiveRecursive(this.mTutorialStaticMeshObj, false);
					fakeObj.FakeObj.transform.parent.parent = null;
					vp_Timer.In(2f, delegate()
					{
						fakeObj.DestroyNpcFakeObj();
						fakeObj = null;
					}, null);
				}, null);
			}, null);
		});
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x000F9AF0 File Offset: 0x000F7CF0
	private void GetOffCar()
	{
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		this.mPlayerCar.DisableCar();
		this.mMainPlayer.EnableMainPlayer();
		this.mPlayerCar.MeshRoot.animation.Play("GetOffCar");
		this.mMainPlayer.CameraController.LerpToTargetLocalZero(this.mPlayerCar.CarDoorView, 1f, null);
		this.mMainPlayer.AnimationLogic.PlayAnimation("GetOffCar", delegate()
		{
		}, -1f);
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x000F9B90 File Offset: 0x000F7D90
	private void ChangeToCarCtl()
	{
		UIManager instance = SingletonUnity<UIManager>.Instance;
		instance.ReShowBaseUI();
		instance.CloseUI(UIInfo.YiDongKongZhiUI);
		instance.CloseUI(UIInfo.JueseJiNengQuUI);
		instance.CloseUI(UIInfo.TouXiangKuangUI);
		instance.ShowCarDefaultUI();
		instance.CloseUI(UIInfo.CopyFunctionBtnRoot);
		instance.CloseUI(UIInfo.ExpLineRoot);
		instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mTutorialMovePathPoint.gameObject, this.mPlayerCar.gameObject);
		}, null);
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.SetTarget(this.mTutorialMovePathPoint.transform.position);
		}
		CameraController cameraController = this.mMainPlayer.CameraController;
		cameraController.LerpToTargetLocalZero(this.mSmoothFollowCamPos.transform, 1f, delegate
		{
			base.IsMissionStart = true;
			this.mPlayerCar.EnableCar(this.mMainPlayer);
		});
		TutorialManager.ShowTutorial(TUTORIAL_STEP.CAR_ACCBTN_START);
	}

	// Token: 0x06003A02 RID: 14850 RVA: 0x000F9C70 File Offset: 0x000F7E70
	private void CreatePlayerCar()
	{
		ObjCarInitData initData = new ObjCarInitData(this.mPlayerCarCreatePos, this.mPlayerCarCreateAngle, UUID.GenUUID(), "Chevrolet", false, null);
		Singleton<ObjManager>.Instance.CreateTutorialPlayerCar(initData);
		this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
		this.mSmoothFollowCamPos.SetTarget(this.mPlayerCar.CarControl);
	}

	// Token: 0x06003A03 RID: 14851 RVA: 0x000F9CD0 File Offset: 0x000F7ED0
	public void OnMainPlayerCreate()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.mMainPlayer.gameObject.rigidbody.isKinematic = false;
		if (!this.mIsTutorialFinish)
		{
			this.mMainPlayer.DisableNavMeshAgent();
			this.mMainPlayer.Position = this.mPlayerPathPointRoot.GetChild(0).position;
			this.mMainPlayer.FaceToPub(this.mMainPlayer.Position + this.mPlayerPathPointRoot.GetChild(0).forward);
			this.mMainPlayer.EnableNavMeshAgent();
			this.mCamCtl = this.mMainPlayer.CameraController;
			this.mCamCtl.Init(CameraController.CAMERAVIEWSTATE.FREE);
			this.mCamCtl.mScale = 0.7f;
			SingletonUnity<UIManager>.Instance.ShowDefaultUI();
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.SetFirstSceneMissionTarget();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowDefaultUI();
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadTipRoot, delegate
				{
					SingletonUnity<DownloadTipRootLogic>.Instance.Reset();
				}, null);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.SetFirstSceneMissionTarget();
		}
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.UpdateSpecialCarState();
		}
	}

	// Token: 0x06003A04 RID: 14852 RVA: 0x000F9E3C File Offset: 0x000F803C
	private void OnStoryShowOver(string storyId)
	{
		UIUpdateEvent.OnStoryShowOver = (UIUpdateEvent.OnStoryShowOverDelegate)Delegate.Remove(UIUpdateEvent.OnStoryShowOver, new UIUpdateEvent.OnStoryShowOverDelegate(this.OnStoryShowOver));
		this.MoveNextState();
	}

	// Token: 0x06003A05 RID: 14853 RVA: 0x000F9E70 File Offset: 0x000F8070
	private void MoveToPlayerCar()
	{
		this.mMainPlayerRunTargetPos = this.mPlayerCar.DummyPlayerPoint.position;
		this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 1f, delegate(ObjCharacter A_1)
		{
			Time.timeScale = 1f;
			this.mMainPlayer.CameraController.LerpToTargetLocalZero(this.mPlayerCar.CarDoorView, 1f, null);
			this.mMainPlayer.DisableNavMeshAgent();
			this.mMainPlayer.CacheTransform.parent = this.mPlayerCar.DummyPlayerPoint;
			TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, false), 10);
			TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalRotate(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, 0), 10);
			vp_Timer.In(0.1f, delegate()
			{
				this.mPlayerCar.MeshRoot.animation.Play("GetOnCar");
				this.mMainPlayer.AnimationLogic.PlayAnimation("GetOnCar", delegate()
				{
					this.mMainPlayer.DisableMainPlayer();
					this.ChangeToCarCtl();
				}, -1f);
			}, null);
		});
	}

	// Token: 0x06003A06 RID: 14854 RVA: 0x000F9EC0 File Offset: 0x000F80C0
	private void OnPlayerArrivePathPoint(Vector3 pos)
	{
		UnityVersionUtil.SetActiveRecursive(this.mTutorialMovePathPoint.gameObject, false);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DragScreenHelpRoot);
		this.MoveNextState();
		this.mCurPlayerPathIndex++;
	}

	// Token: 0x06003A07 RID: 14855 RVA: 0x000F9F10 File Offset: 0x000F8110
	public void OnClickTalkNpc(TapGesture gesture)
	{
		Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
		this.CastScreenRay(ray, gesture);
	}

	// Token: 0x06003A08 RID: 14856 RVA: 0x000F9F3C File Offset: 0x000F813C
	private void CastScreenRay(Ray ray, TapGesture gesture)
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(ray, ref raycastHit, 200f))
		{
			ObjCharacter component = raycastHit.collider.gameObject.GetComponent<ObjCharacter>();
			if (component != null)
			{
				if (component.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
				{
					Ray ray2;
					ray2..ctor(raycastHit.point + ray.direction * 0.1f, ray.direction);
					this.CastScreenRay(ray2, gesture);
					return;
				}
				if (component.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
				{
					ObjNPC objNPC = component as ObjNPC;
					if (objNPC.AttributeData.Camp == GameDefine.CAMP_TYPE.NORMAL_NPC)
					{
						TutorialManager.MoveNext(false);
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, delegate
						{
							SingletonUnity<JoyStickLogic>.Instance.HidePic();
						}, null);
						SingletonUnity<InputController>.Instance.TapRecognizer.OnGesture -= new GestureRecognizerTS<TapGesture>.GestureEventHandler(this.OnClickTalkNpc);
						TutorialManager.Show_CLICK_NPC_FINISH();
						if (this.mStepIndex == 4)
						{
							this.MoveNextState();
						}
					}
				}
			}
		}
	}

	// Token: 0x06003A09 RID: 14857 RVA: 0x000FA04C File Offset: 0x000F824C
	public void MoveNextState()
	{
		this.mStepIndex++;
		switch (this.mStepIndex)
		{
		case 1:
			this.SendServerFinishTutorial();
			break;
		case 2:
			TutorialManager.CloseTutorial();
			this.SendServerFinishTutorial();
			SingletonUnity<JueseJiNengQuLogic>.Instance.ShowAllSkillBtn();
			break;
		case 3:
			this.ClearWalkNpc();
			SingletonUnity<JueseJiNengQuLogic>.Instance.ShowAllSkillBtn();
			TutorialManager.ShowTutorial(TUTORIAL_STEP.SKILL1_BUTTON);
			SingletonUnity<DownloadResTipRootLogic>.Instance.UpdateTutorialStep(0, 0f);
			break;
		case 4:
			this.CreateNPCGroup();
			FunctionTipsRootLogic.ClearHandTip();
			break;
		case 5:
			this.ClearWalkNpc();
			SingletonUnity<JueseJiNengQuLogic>.Instance.ShowAllSkillBtn();
			TutorialManager.ShowTutorial(TUTORIAL_STEP.SKILL1_BUTTON);
			this.SendServerFinishTutorial();
			break;
		case 6:
			this.SetPlayerMoveTarget();
			SingletonUnity<DownloadResTipRootLogic>.Instance.UpdateTutorialStep(3, 1f);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FakeMissioonTutorial, null, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "7MOVETO_CAR");
			break;
		case 7:
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DownLoadResTipRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FakeMissioonTutorial);
			Singleton<ObjManager>.Instance.RecycleAllNPC();
			TutorialManager.CloseTutorial();
			this.StartRobCarState();
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "8ArriveAtPoint");
			break;
		case 8:
			this.mIsCarCtlTutorialShow = false;
			this.mTutorialMovePathPoint.DeRegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnPlayerArrivePathPoint));
			this.mTutorialMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArriveCarPathPoint));
			this.mTutorialMovePathPoint.ActiveRadius = 20f;
			this.SetPlayerCarMoveTarget();
			break;
		case 9:
			Singleton<ObjManager>.Instance.ClearAICar();
			this.StartCrashCar();
			break;
		}
	}

	// Token: 0x06003A0A RID: 14858 RVA: 0x000FA21C File Offset: 0x000F841C
	private void SendServerFinishTutorial()
	{
		this.mIsTutorialFinish = true;
		NetLogic.GetInstance().Send<Protocol.tutorial_finish>(null, null);
		vp_Timer.In(2f, delegate()
		{
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadTipRoot, delegate
				{
					SingletonUnity<DownloadTipRootLogic>.Instance.Reset();
				}, null);
			}
		}, null);
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x000FA25C File Offset: 0x000F845C
	private void CreateShowNpc()
	{
		Transform transform = GameObject.Find("ShowNpcPos").transform;
		SoundManager instance = SingletonDontDestoryUnity<SoundManager>.Instance;
		Transform child = transform.GetChild(0);
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = child.position;
		objInitNpcData.mDir = child.forward;
		NpcData npcDataByID = DataManager.GetNpcDataByID("204");
		objInitNpcData.npcInfoData = npcDataByID;
		objInitNpcData.mCharacterModelId = npcDataByID.Model;
		Singleton<ObjManager>.Instance.GetRagdollNPC(objInitNpcData, delegate(ObjNPC npc)
		{
			if (npc.AnimationLogic.AnimaObj != null)
			{
				vp_Timer.In(1f, delegate()
				{
					npc.AnimationLogic.PlayAnimation("attack_1", delegate()
					{
						npc.MoveTo(-17f, 0f, 134f, 1f, null);
						vp_Timer.In(1.5f, delegate()
						{
							(npc as ObjRagdollNPC).RecycleSelf();
						}, null);
					}, -1f);
				}, null);
			}
			else
			{
				npc.onLoadMeshFinished = delegate(ObjNPC ObjNpc)
				{
					vp_Timer.In(1f, delegate()
					{
						ObjNpc.AnimationLogic.PlayAnimation("attack_1", delegate()
						{
							ObjNpc.MoveTo(-17f, 0f, 134f, 1f, null);
							vp_Timer.In(1.5f, delegate()
							{
								(ObjNpc as ObjRagdollNPC).RecycleSelf();
							}, null);
						}, -1f);
					}, null);
				};
			}
		});
		Transform child2 = transform.GetChild(1);
		ObjInitNpcData objInitNpcData2 = new ObjInitNpcData();
		objInitNpcData2.mServerID = UUID.GenUUID();
		objInitNpcData2.mPos = child2.position;
		objInitNpcData2.mDir = child2.forward;
		NpcData npcDataByID2 = DataManager.GetNpcDataByID("204");
		objInitNpcData2.npcInfoData = npcDataByID2;
		objInitNpcData2.mCharacterModelId = npcDataByID2.Model;
		Singleton<ObjManager>.Instance.GetRagdollNPC(objInitNpcData2, delegate(ObjNPC npc)
		{
			if (npc.AnimationLogic.AnimaObj != null)
			{
				vp_Timer.In(1.5f, delegate()
				{
					npc.AnimationLogic.PlayAnimation("attack_1", delegate()
					{
						npc.MoveTo(-17f, 0f, 134f, 1f, null);
						vp_Timer.In(1.5f, delegate()
						{
							(npc as ObjRagdollNPC).RecycleSelf();
						}, null);
					}, -1f);
				}, null);
			}
			else
			{
				npc.onLoadMeshFinished = delegate(ObjNPC ObjNpc)
				{
					vp_Timer.In(1f, delegate()
					{
						ObjNpc.AnimationLogic.PlayAnimation("attack_1", delegate()
						{
							ObjNpc.MoveTo(-17f, 0f, 134f, 1f, null);
							vp_Timer.In(1.5f, delegate()
							{
								(ObjNpc as ObjRagdollNPC).RecycleSelf();
							}, null);
						}, -1f);
					}, null);
				};
			}
		});
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x000FA380 File Offset: 0x000F8580
	private void StartCrashCar()
	{
		UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.gameObject, false);
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		if (this.mCrashCarSceneAnimaObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mCrashCarSceneAnimaObj, true);
			this.curAnimaCtl = this.mCrashCarSceneAnimaObj.GetComponent<SceneAnimationCtl>();
			this.curAnimaCtl.RegisterOnFinished(new DelegateDefine.NoParamDelegate(this.FinishTutorial));
			this.mCamCtl.CurCamera.enabled = false;
			UICamera.mainCamera.depth = 2f;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SkipBtnRoot, null, null);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "15StartCrashCarAnima");
	}

	// Token: 0x06003A0D RID: 14861 RVA: 0x000FA438 File Offset: 0x000F8638
	private void FinishTutorial()
	{
		UICamera.mainCamera.depth = 0f;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SkipBtnRoot);
		string[] PageStr = new string[]
		{
			StrDictionary.GetDictionaryString("#{90101}", new object[0]),
			StrDictionary.GetDictionaryString("#{90102}", new object[0]),
			StrDictionary.GetDictionaryString("#{90103}", new object[0])
		};
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PrinterPageRoot, delegate
		{
			SingletonUnity<PrinterPageRootLogic>.Instance.Reset(PageStr, delegate
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "17ShowDownloadPage");
			});
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "step", "16ShowPrintPage");
	}

	// Token: 0x06003A0E RID: 14862 RVA: 0x000FA4E4 File Offset: 0x000F86E4
	private void StartRobCarState()
	{
		this.mMainPlayer.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
		this.mMainPlayer.DisactiveIdleAttack();
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		UnityVersionUtil.SetActiveRecursive(this.mCarAnimaObj.gameObject, false);
		if (this.mRobCarSceneAnimaObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mRobCarSceneAnimaObj, true);
			this.curAnimaCtl = this.mRobCarSceneAnimaObj.GetComponent<SceneAnimationCtl>();
			this.curAnimaCtl.RegisterOnFinished(new DelegateDefine.NoParamDelegate(this.onFinishedRobCarState));
			this.mCamCtl.CurCamera.enabled = false;
			UICamera.mainCamera.depth = 2f;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SkipBtnRoot, null, null);
		}
		this.isFinishRobCarAnima = false;
	}

	// Token: 0x06003A0F RID: 14863 RVA: 0x000FA5A0 File Offset: 0x000F87A0
	private void onFinishedRobCarState()
	{
		UnityVersionUtil.SetActiveRecursive(this.mCarAnimaObj.gameObject, true);
		this.mCamCtl.CurCamera.enabled = true;
		UICamera.mainCamera.depth = 0f;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SkipBtnRoot);
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		UnityVersionUtil.SetActiveRecursive(this.mSmoothFollowCamPos.gameObject, true);
		this.CreatePlayerCar();
		this.mPlayerCar.transform.position = this.mPlayerCarCreatePos;
		this.mSmoothFollowCamPos.UpdateToTargetPos();
		this.mMainPlayer.rigidbody.isKinematic = false;
		this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 0.1f, delegate(ObjCharacter A_1)
		{
			this.RobCar();
		});
		this.isFinishRobCarAnima = true;
	}

	// Token: 0x06003A10 RID: 14864 RVA: 0x000FA674 File Offset: 0x000F8874
	public void SetPlayerMoveTarget()
	{
		this.mTutorialMovePathPoint.transform.position = this.mPlayerPathPointRoot.GetChild(this.mCurPlayerPathIndex).position;
		this.mTutorialMovePathPoint.transform.localScale = new Vector3(1f, 0.5f, 1f);
		UnityVersionUtil.SetActiveRecursive(this.mTutorialMovePathPoint.gameObject, true);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mTutorialMovePathPoint.gameObject, this.mMainPlayer.gameObject);
		}, null);
		if (!this.IsNeedCheckMainPlayerActiveRange())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DragScreenHelpRoot, delegate
			{
				SingletonUnity<DragScreenHelpRoot>.Instance.SetTargetObj(this.mTutorialMovePathPoint.gameObject);
			}, null);
		}
	}

	// Token: 0x06003A11 RID: 14865 RVA: 0x000FA720 File Offset: 0x000F8920
	public void SetPlayerCarMoveTarget()
	{
		this.mTutorialMovePathPoint.transform.position = this.mPlayerCarPathList[this.mCurPlayerCarPathIndex];
		UnityVersionUtil.SetActiveRecursive(this.mTutorialMovePathPoint.gameObject, true);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mTutorialMovePathPoint.gameObject, this.mPlayerCar.gameObject);
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.SetTarget(this.mTutorialMovePathPoint.transform.position);
			}
		}, null);
	}

	// Token: 0x06003A12 RID: 14866 RVA: 0x000FA77C File Offset: 0x000F897C
	private void CreateNPCGroup()
	{
		this.mCurEnemyGroup++;
		this.NpcCreate(base.GetMonsterDataByGroup(this.mCurEnemyGroup));
	}

	// Token: 0x06003A13 RID: 14867 RVA: 0x000FA7AC File Offset: 0x000F89AC
	private void NpcCreate(List<MonsterData> list)
	{
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			MonsterData monsterData = list[i];
			objInitNpcData.mServerID = UUID.GenUUID();
			objInitNpcData.mPos = new Vector3(monsterData.PositionX, 0f, monsterData.PositionZ);
			objInitNpcData.mDir = MathUtil.HeadingToVector3((float)monsterData.PosO / 100f);
			NpcData npcDataByID = DataManager.GetNpcDataByID(monsterData.NpcID);
			objInitNpcData.npcInfoData = npcDataByID;
			objInitNpcData.MaxHP = objInitNpcData.npcInfoData.Hp;
			objInitNpcData.HP = objInitNpcData.npcInfoData.Hp;
			objInitNpcData.ATK = objInitNpcData.npcInfoData.Atk;
			objInitNpcData.DEF = objInitNpcData.npcInfoData.Def;
			objInitNpcData.HIT = objInitNpcData.npcInfoData.HIT;
			objInitNpcData.EVA = objInitNpcData.npcInfoData.DGE;
			objInitNpcData.CRI = objInitNpcData.npcInfoData.CRI;
			objInitNpcData.EXD = objInitNpcData.npcInfoData.EXD;
			objInitNpcData.EXR = objInitNpcData.npcInfoData.EXR;
			objInitNpcData.RES = objInitNpcData.npcInfoData.RES;
			objInitNpcData.CRD = objInitNpcData.npcInfoData.CRD;
			objInitNpcData.CRR = objInitNpcData.npcInfoData.CRR;
			objInitNpcData.DEFA = objInitNpcData.npcInfoData.DEFA;
			objInitNpcData.Level = objInitNpcData.npcInfoData.Lv;
			objInitNpcData.AntiStun = objInitNpcData.npcInfoData.AntiStun;
			objInitNpcData.AntiKnockDown = objInitNpcData.npcInfoData.AntiKnockDown;
			Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, new ObjManager.OnGetNPC(this.OnNPCCreated), null);
		}
	}

	// Token: 0x06003A14 RID: 14868 RVA: 0x000FA960 File Offset: 0x000F8B60
	private void OnNPCCreated(ObjNPC npc)
	{
		if (npc.NPCData.AI.Equals("BlockAI"))
		{
			this.staticNpc = npc.gameObject;
			CapsuleCollider component = this.staticNpc.GetComponent<CapsuleCollider>();
			if (component != null)
			{
				component.radius = 0.8f;
				component.height = 2.5f;
			}
		}
		else
		{
			npc.ChangeBornPos(Singleton<ObjManager>.Instance.MainPlayer.Position);
			npc.MoveTo(Singleton<ObjManager>.Instance.MainPlayer.Position, 1f, null);
		}
	}

	// Token: 0x06003A15 RID: 14869 RVA: 0x000FA9F8 File Offset: 0x000F8BF8
	public override void OnNPCDie(object objNpc)
	{
		ObjRagdollNPC npc = objNpc as ObjRagdollNPC;
		npc.recycleHandle.Cancel();
		vp_Timer.In(2f, delegate()
		{
			npc.RecycleSelf();
		}, npc.recycleHandle);
		local_npc_die.request request = new local_npc_die.request();
		request.npcid = npc.NPCDataID;
		request.x = (long)(npc.Position.x * 100f);
		request.z = (long)(npc.Position.z * 100f);
		request.type = 0L;
		if (npc.IsRagdollEnable && SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.IMPACT_NPC))
		{
			request.type = 3L;
		}
		NetLogic.GetInstance().Send<Protocol.local_npc_die>(request, null);
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.OnNPCDie(npc);
		}
	}

	// Token: 0x06003A16 RID: 14870 RVA: 0x000FAAFC File Offset: 0x000F8CFC
	public override void LeaveScene()
	{
		MessageBoxLogic.OpenOKCancelBox(this.mapInfoData.MExitCon, StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
			Singleton<ObjManager>.Instance.StopAllPoliceSound();
		}, null, null, null);
	}

	// Token: 0x06003A17 RID: 14871 RVA: 0x000FAB4C File Offset: 0x000F8D4C
	public void ResetPlayerCarToLastPoint()
	{
		this.mPlayerCar.rigidbody.velocity = Vector3.zero;
		this.mPlayerCar.rigidbody.angularVelocity = Vector3.zero;
		if (this.mCurPlayerCarPathIndex == 0)
		{
			this.mPlayerCar.transform.position = this.mPlayerCarCreatePos;
			this.mPlayerCar.transform.eulerAngles = this.mPlayerCarCreateAngle;
		}
		else
		{
			this.mPlayerCar.transform.position = this.mPlayerCarPathList[this.mCurPlayerCarPathIndex - 1];
			this.mPlayerCar.transform.forward = (this.mPlayerCarPathList[this.mCurPlayerCarPathIndex] - this.mPlayerCarPathList[this.mCurPlayerCarPathIndex - 1]).normalized;
		}
	}

	// Token: 0x06003A18 RID: 14872 RVA: 0x000FAC24 File Offset: 0x000F8E24
	public bool IsNeedCheckMainPlayerActiveRange()
	{
		return this.mStepIndex >= 6;
	}

	// Token: 0x06003A19 RID: 14873 RVA: 0x000FAC34 File Offset: 0x000F8E34
	public override void OnReconnectSuccess()
	{
		base.OnReconnectSuccess();
		if (this.mStepIndex == this.ROBBING_CAR_STATE && this.isFinishRobCarAnima)
		{
			this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 0.1f, delegate(ObjCharacter A_1)
			{
				this.RobCar();
			});
		}
	}

	// Token: 0x06003A1A RID: 14874 RVA: 0x000FAC90 File Offset: 0x000F8E90
	private bool IsNeedUpdateProgressLine()
	{
		return this.mStepIndex == 3 || this.mStepIndex == 4;
	}

	// Token: 0x06003A1B RID: 14875 RVA: 0x000FACB0 File Offset: 0x000F8EB0
	private bool IsNeedCheckDownloadProgress()
	{
		return this.mStepIndex == 5;
	}

	// Token: 0x06003A1C RID: 14876 RVA: 0x000FACC4 File Offset: 0x000F8EC4
	public override void SetMoveTarget(Vector3 pos, string missionId)
	{
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (this.mMainPlayer == null)
			{
				return;
			}
		}
		if (Vector3.Distance(this.mMainPlayer.Position, pos) < 10f)
		{
			return;
		}
		base.SetMoveTarget(pos, missionId);
		this.curMoveTargetMissionId = missionId;
		this.IsHaveMoveTarget = true;
		this.CurMoveTarget = pos;
		if (this.mMoveTargetPathPoint == null)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject;
			this.mMoveTargetPathPoint = gameObject.GetComponent<MovePathPoint>();
			this.mMoveTargetPathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnPlayerArriveTargetPoint));
			this.mMoveTargetPathPoint.ActiveRadius = 8f;
		}
		this.mMoveTargetPathPoint.transform.position = new Vector3(pos.x, SceneManager.GetHitHeight(pos), pos.z);
		UnityVersionUtil.SetActiveRecursive(this.mMoveTargetPathPoint.gameObject, true);
		this.UpdateMapLine();
		if (!SingletonUnity<CarTargetUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<CarTargetUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
			{
				SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMoveTargetPathPoint.gameObject, this.mMainPlayer.gameObject);
			}, null);
		}
		else
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMoveTargetPathPoint.gameObject, this.mMainPlayer.gameObject);
		}
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.SetTarget(pos);
		}
		if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.UpdateMoveTargetPic();
		}
	}

	// Token: 0x06003A1D RID: 14877 RVA: 0x000FAE84 File Offset: 0x000F9084
	private void UpdateMapLine()
	{
		if (this.IsHaveMoveTarget && SingletonUnity<CitySimController>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CitySimController>.Instance.gameObject))
		{
			SingletonUnity<CitySimController>.Instance.UpdateMapLine(this.mMainPlayer.Position, this.CurMoveTarget);
		}
	}

	// Token: 0x06003A1E RID: 14878 RVA: 0x000FAED8 File Offset: 0x000F90D8
	private void ClearMapLine()
	{
		if (SingletonUnity<CitySimController>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CitySimController>.Instance.gameObject))
		{
			SingletonUnity<CitySimController>.Instance.ClearMapLine();
		}
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x000FAF10 File Offset: 0x000F9110
	public override void ClearMoveTarget()
	{
		this.OnPlayerArriveTargetPoint(Vector3.zero);
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x000FAF20 File Offset: 0x000F9120
	public void OnPlayerArriveTargetPoint(Vector3 pos)
	{
		base.ClearMoveTarget();
		this.ClearMapLine();
		if (this.mMoveTargetPathPoint != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mMoveTargetPathPoint.gameObject, false);
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.HideTarget();
		}
		if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.UpdateMoveTargetPic();
		}
	}

	// Token: 0x06003A21 RID: 14881 RVA: 0x000FAFBC File Offset: 0x000F91BC
	public override void MissionCheckMoveTarget(string missionId)
	{
		if (!string.IsNullOrEmpty(this.curMoveTargetMissionId) && this.curMoveTargetMissionId.Equals(missionId))
		{
			this.OnPlayerArriveTargetPoint(Vector3.zero);
		}
	}

	// Token: 0x040025FE RID: 9726
	private SmoothFollowNew mSmoothFollowCamPos;

	// Token: 0x040025FF RID: 9727
	private Vector3 mMainPlayerRunTargetPos;

	// Token: 0x04002600 RID: 9728
	private Vector3 mPlayerCarCreatePos;

	// Token: 0x04002601 RID: 9729
	private Vector3 mPlayerCarCreateAngle;

	// Token: 0x04002602 RID: 9730
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002603 RID: 9731
	private ObjPlayerCar mPlayerCar;

	// Token: 0x04002604 RID: 9732
	private MovePathPoint mTutorialMovePathPoint;

	// Token: 0x04002605 RID: 9733
	private MovePathPoint mMoveTargetPathPoint;

	// Token: 0x04002606 RID: 9734
	private List<Vector3> mPlayerCarPathList = new List<Vector3>();

	// Token: 0x04002607 RID: 9735
	private int mCurPlayerCarPathIndex;

	// Token: 0x04002608 RID: 9736
	private CopySceneData mCurCopySceneData;

	// Token: 0x04002609 RID: 9737
	private CarHPRootLogic carHPRoot;

	// Token: 0x0400260A RID: 9738
	private Transform mPlayerPathPointRoot;

	// Token: 0x0400260B RID: 9739
	private int mWaveCount;

	// Token: 0x0400260C RID: 9740
	private int mStepIndex;

	// Token: 0x0400260D RID: 9741
	private int mCurEnemyGroup;

	// Token: 0x0400260E RID: 9742
	private int mCurPlayerPathIndex;

	// Token: 0x0400260F RID: 9743
	public Transform TalkNpcPos;

	// Token: 0x04002610 RID: 9744
	private ObjNPC mTalkingNpc;

	// Token: 0x04002611 RID: 9745
	private CameraController mCamCtl;

	// Token: 0x04002612 RID: 9746
	private GameObject mCarAnimaObj;

	// Token: 0x04002613 RID: 9747
	private int DRIVING_CAR_TUTORIAL_STEP = 8;

	// Token: 0x04002614 RID: 9748
	private GameObject mTutorialStaticMeshObj;

	// Token: 0x04002615 RID: 9749
	private bool mIsTutorialFinish;

	// Token: 0x04002616 RID: 9750
	public ObjFakeAICar mTutorialCar;

	// Token: 0x04002617 RID: 9751
	private GameObject mRandomWalkPosRoot;

	// Token: 0x04002618 RID: 9752
	private List<Vector3> mRandomWalkPosList = new List<Vector3>();

	// Token: 0x04002619 RID: 9753
	private string[] NpcIdList = new string[]
	{
		"1072",
		"1073",
		"1074",
		"1075",
		"1076"
	};

	// Token: 0x0400261A RID: 9754
	private List<ObjNPC> mCurRandomWalkNpcList = new List<ObjNPC>();

	// Token: 0x0400261B RID: 9755
	private string[] NpcIdList2 = new string[]
	{
		"1082",
		"1083",
		"1084",
		"204",
		"204"
	};

	// Token: 0x0400261C RID: 9756
	private Dictionary<long, vp_Timer.Handle> walkNpcHandle = new Dictionary<long, vp_Timer.Handle>();

	// Token: 0x0400261D RID: 9757
	private GameObject mStartSceneAnimaObj;

	// Token: 0x0400261E RID: 9758
	private GameObject mRobCarSceneAnimaObj;

	// Token: 0x0400261F RID: 9759
	private GameObject mCrashCarSceneAnimaObj;

	// Token: 0x04002620 RID: 9760
	private Dictionary<string, GameObject> mStaticBlockDic = new Dictionary<string, GameObject>();

	// Token: 0x04002621 RID: 9761
	private Dictionary<int, MoveCarBlock> mMoveCarBlockDic = new Dictionary<int, MoveCarBlock>();

	// Token: 0x04002622 RID: 9762
	public SceneAnimationCtl curAnimaCtl;

	// Token: 0x04002623 RID: 9763
	private bool loadingOverFlag;

	// Token: 0x04002624 RID: 9764
	private float FreshNPCTimeCount;

	// Token: 0x04002625 RID: 9765
	private float FreshNPCTime = 2f;

	// Token: 0x04002626 RID: 9766
	private float PlayerBolckCheckTime = 5f;

	// Token: 0x04002627 RID: 9767
	private float playerBlockTimeCount;

	// Token: 0x04002628 RID: 9768
	private bool mIsCarCtlTutorialShow;

	// Token: 0x04002629 RID: 9769
	private float checkHpTimeCount;

	// Token: 0x0400262A RID: 9770
	private float waitDownloadStartTime;

	// Token: 0x0400262B RID: 9771
	private int waitTime = 30;

	// Token: 0x0400262C RID: 9772
	private float flashTimeCount;

	// Token: 0x0400262D RID: 9773
	private bool LerpToCarFlag;

	// Token: 0x0400262E RID: 9774
	private Vector3 startPosition;

	// Token: 0x0400262F RID: 9775
	private Quaternion startRotation;

	// Token: 0x04002630 RID: 9776
	private float lerpTime = 1f;

	// Token: 0x04002631 RID: 9777
	private float lerpCountTime;

	// Token: 0x04002632 RID: 9778
	private bool isFinishRobCarAnima;

	// Token: 0x04002633 RID: 9779
	private GameObject staticNpc;

	// Token: 0x04002634 RID: 9780
	private int ROBBING_CAR_STATE = 7;

	// Token: 0x04002635 RID: 9781
	private string curMoveTargetMissionId = string.Empty;
}
