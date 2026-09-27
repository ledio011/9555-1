using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x02000874 RID: 2164
public class CarChaseSceneManager : SceneManager
{
	// Token: 0x06003965 RID: 14693 RVA: 0x000F5480 File Offset: 0x000F3680
	public override void Init(string id)
	{
		base.Init(id);
		this.mCurCopySceneData = DataManager.GetCopySceneDataById(this.mapInfoData.ID);
		if (this.mCurCopySceneData != null)
		{
			this.MISSION_TIME = (float)this.mCurCopySceneData.ExistTime;
			this.mMissionTimeCount = this.MISSION_TIME;
		}
		this.mStartTimeCountFlag = false;
		this.mEndMissionFlag = false;
		GameObject gameObject = GameObject.Find("SmoothFollowCamPos");
		if (gameObject != null)
		{
			this.mSmoothFollowCamPos = gameObject.GetComponent<SmoothFollowNew>();
		}
		gameObject = (ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject);
		this.mMovePathPoint = gameObject.GetComponent<MovePathPoint>();
		this.mMovePathPoint.ActiveRadius = 15f;
		this.mMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArrivePathPoint));
		SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		this.InitBlock(id);
		this.StrikePool = new SimplePool<ParticleSystem>();
		this.StrikePool.Reset(new SimplePool<ParticleSystem>.CreateFunc(base.CreateStrike), new SimplePool<ParticleSystem>.DestroyFunc(base.DestroyStrike), 10);
		base.IsMissionStart = false;
		copyscene_info copyscene_info = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.DailyCopyInfoDic[this.mCurCopySceneData.ID];
		if (copyscene_info.HasBestGrade)
		{
			this.mBestTime = (int)copyscene_info.BestGrade;
		}
		else
		{
			this.mBestTime = -1;
		}
		if (copyscene_info.HasStr)
		{
			string[] array = copyscene_info.str.Split(new char[]
			{
				'#'
			});
			this.mPathPointBestTime = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.mPathPointBestTime[i] = int.Parse(array[i]);
			}
		}
		this.mCurPathPointTime = new int[this.mPlayerPathList.Count];
		GameObject go = GameObject.Find("FB_saiDao_1/dibiao_100+/FB_saiDao_dangBan_GTA");
		UnityVersionUtil.SetActiveRecursive(go, false);
		ResourcesManager.LoadAndInstantiate("Tutorial/CitySimController");
	}

	// Token: 0x06003966 RID: 14694 RVA: 0x000F5670 File Offset: 0x000F3870
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
					if (gameObject5 == null)
					{
						goto IL_627;
					}
					this.mStaticBlockDic.Add(carMissionBlockDataListBySceneId[i].ObstacleName, gameObject5);
				}
				if (!(gameObject5 == null))
				{
					gameObject5.transform.parent = gameObject2.transform;
					gameObject5.transform.position = new Vector3(carMissionBlockDataListBySceneId[i].PosX, carMissionBlockDataListBySceneId[i].PosY, carMissionBlockDataListBySceneId[i].PosZ);
					gameObject5.transform.eulerAngles = new Vector3(carMissionBlockDataListBySceneId[i].AngleX, carMissionBlockDataListBySceneId[i].AngleY, carMissionBlockDataListBySceneId[i].AngleZ);
				}
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
			IL_627:;
		}
		list.Sort((KeyValuePair<int, Vector3> pre, KeyValuePair<int, Vector3> next) => pre.Key - next.Key);
		this.mCurPlayerPathIndex = 0;
		for (int j = 0; j < list.Count; j++)
		{
			this.mPlayerPathList.Add(list[j].Value);
		}
		this.mMovePathPoint.transform.position = this.mPlayerPathList[0];
	}

	// Token: 0x06003967 RID: 14695 RVA: 0x000F5D38 File Offset: 0x000F3F38
	public override void StartGame()
	{
		base.IsMissionStart = true;
		this.ChangeToCarCtl();
		NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
		this.mMissionStartTime = Time.time;
		SingletonUnity<CarBestTimeCountRoot>.Instance.EnableTimeCount(this.mMissionStartTime);
	}

	// Token: 0x06003968 RID: 14696 RVA: 0x000F5D7C File Offset: 0x000F3F7C
	private void OnArrivePathPoint(Vector3 pos)
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarPointBestTimeRoot, delegate
		{
			int bestTime;
			if (this.mPathPointBestTime == null || this.mPathPointBestTime.Length <= this.mCurPlayerPathIndex)
			{
				bestTime = -1;
			}
			else
			{
				bestTime = this.mPathPointBestTime[this.mCurPlayerPathIndex];
			}
			SingletonUnity<CarPointBestTimeRoot>.Instance.Reset(this.GetCurMissionTime(), bestTime);
		}, null);
		this.mCurPathPointTime[this.mCurPlayerPathIndex] = this.GetCurMissionTime();
		this.mCurPlayerPathIndex++;
		if (this.mCurPlayerPathIndex < this.mPlayerPathList.Count)
		{
			this.mMovePathPoint.transform.position = this.mPlayerPathList[this.mCurPlayerPathIndex];
			UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, true);
			SingletonUnity<MiniMap>.Instance.SetTarget(this.mPlayerPathList[this.mCurPlayerPathIndex]);
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMovePathPoint.gameObject, this.mPlayerCar.gameObject);
		}
		else
		{
			this.SuccessMission();
		}
	}

	// Token: 0x06003969 RID: 14697 RVA: 0x000F5E54 File Offset: 0x000F4054
	public override void OnLoadingOver()
	{
		base.OnLoadingOver();
		vp_Timer.In(0.5f, delegate()
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarMissionStartTimeCountRoot, delegate
			{
				SingletonUnity<CarMissionStartTimeCountRoot>.Instance.Reset(Time.time, null);
			}, null);
		}, null);
		CameraController cameraController = this.mMainPlayer.CameraController;
		cameraController.enabled = false;
		cameraController.transform.position = this.mPlayerCar.StartCamView.transform.position;
		cameraController.transform.rotation = this.mPlayerCar.StartCamView.transform.rotation;
		cameraController.LerpToTargetLocalZero(this.mSmoothFollowCamPos.transform, 1f, null);
		this.mPlayerCar.EnableCar(this.mMainPlayer);
		this.mPlayerCar.FreezeCar();
	}

	// Token: 0x0600396A RID: 14698 RVA: 0x000F5F18 File Offset: 0x000F4118
	public override void SuccessMission()
	{
		base.SuccessMission();
		this.EndMission(true);
	}

	// Token: 0x0600396B RID: 14699 RVA: 0x000F5F28 File Offset: 0x000F4128
	public override void FailMission()
	{
		base.FailMission();
		this.EndMission(false);
	}

	// Token: 0x0600396C RID: 14700 RVA: 0x000F5F38 File Offset: 0x000F4138
	public override void Update()
	{
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.OnlyUpdateNpc();
		}
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x000F5F50 File Offset: 0x000F4150
	private void CheckPlayerBlock()
	{
		if (Vector3.SqrMagnitude(this.prePos - this.mPlayerCar.Position) < 4f)
		{
			this.EndMission(false);
		}
		this.prePos = this.mPlayerCar.Position;
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x000F5F9C File Offset: 0x000F419C
	private void FreshNPC()
	{
		if (SingletonUnity<EasyCitySimController>.Exists)
		{
			SingletonUnity<EasyCitySimController>.Instance.FreshNPC(this.mMainPlayer.CacheTransform);
		}
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x000F5FC0 File Offset: 0x000F41C0
	private void RobCar()
	{
		this.mMainPlayer.rigidbody.isKinematic = true;
		this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 1f, delegate(ObjCharacter A_1)
		{
			SingletonUnity<UIManager>.Instance.HideBaseUI();
			this.mMainPlayer.CameraController.LerpToTargetLocalZero(this.mPlayerCar.CarDoorView, 1f, null);
			this.mMainPlayer.DisableNavMeshAgent();
			this.mMainPlayer.CacheTransform.parent = this.mPlayerCar.DummyPlayerPoint;
			TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, false), 10);
			TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalRotate(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, 0), 10);
			vp_Timer.In(0.1f, delegate()
			{
				this.mPlayerCar.MeshRoot.animation.Play("RobCar");
				this.mMainPlayer.AnimationLogic.PlayAnimation("RobCar", delegate()
				{
					this.mMainPlayer.DisableMainPlayer();
					this.ChangeToCarCtl();
				}, -1f);
				ObjInitNpcData objInitNpcData = new ObjInitNpcData();
				objInitNpcData.mServerID = UUID.GenUUID();
				objInitNpcData.mPos = this.mPlayerCar.DummyNPCPoint.position;
				NpcData npcDataByID = DataManager.GetNpcDataByID("201");
				objInitNpcData.HP = npcDataByID.Hp;
				objInitNpcData.MaxHP = npcDataByID.Hp;
				objInitNpcData.npcInfoData = npcDataByID;
				Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, delegate(ObjNPC npc)
				{
					npc.CacheTransform.parent = this.mPlayerCar.DummyNPCPoint;
					npc.CacheTransform.localPosition = Vector3.zero;
					npc.CacheTransform.localRotation = Quaternion.identity;
				}, delegate(ObjNPC npc)
				{
					npc.AnimationLogic.PlayAnimation("RobOffCar", delegate()
					{
						npc.CacheTransform.parent = null;
					}, -1f);
				});
			}, null);
		});
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x000F600C File Offset: 0x000F420C
	private void GetOffCar()
	{
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		this.mPlayerCar.DisableCar();
		this.mMainPlayer.EnableMainPlayer();
		this.mPlayerCar.MeshRoot.animation.Play("GetOffCar");
		this.mMainPlayer.CameraController.LerpToTargetLocalZero(this.mPlayerCar.CarDoorView, 1f, null);
		this.mMainPlayer.AnimationLogic.PlayAnimation("GetOffCar", delegate()
		{
			this.ChangeToPlayerCtl();
		}, -1f);
	}

	// Token: 0x06003971 RID: 14705 RVA: 0x000F609C File Offset: 0x000F429C
	private void ChangeToPlayerCtl()
	{
		this.mMainPlayer.transform.parent = null;
		this.mMainPlayer.EnableNavMeshAgent();
		this.mMainPlayer.rigidbody.isKinematic = false;
		UIManager uiManager = SingletonUnity<UIManager>.Instance;
		CameraController camCtl = this.mMainPlayer.CameraController;
		vp_Timer.In(0.1f, delegate()
		{
			camCtl.LerpBackToPlayer(1f, delegate
			{
				uiManager.ReShowBaseUI();
				uiManager.CloseUI(UIInfo.CarControllerRoot);
				uiManager.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
				uiManager.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
				uiManager.ShowUI(UIInfo.TouXiangKuangUI, delegate(bool isSuccess, object param)
				{
					if (isSuccess)
					{
						SingletonUnity<TouXiangKuangLogic>.Instance.Init();
					}
				}, null);
			});
		}, null);
	}

	// Token: 0x06003972 RID: 14706 RVA: 0x000F6110 File Offset: 0x000F4310
	private void ChangeToCarCtl()
	{
		UIManager instance = SingletonUnity<UIManager>.Instance;
		instance.ReShowBaseUI();
		instance.CloseUI(UIInfo.YiDongKongZhiUI);
		instance.CloseUI(UIInfo.JueseJiNengQuUI);
		instance.CloseUI(UIInfo.TouXiangKuangUI);
		instance.ShowCarDefaultUI();
		instance.ShowUI(UIInfo.CarBestTimeCountRoot, delegate
		{
			SingletonUnity<CarBestTimeCountRoot>.Instance.Reset(this.mBestTime);
		}, null);
		instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMovePathPoint.gameObject, this.mPlayerCar.gameObject);
		}, null);
		SingletonUnity<MiniMap>.Instance.SetTarget(this.mPlayerPathList[0]);
		this.mPlayerCar.DisFreezeCar();
		this.mStartTimeCountFlag = true;
	}

	// Token: 0x06003973 RID: 14707 RVA: 0x000F61A8 File Offset: 0x000F43A8
	private void CreatePlayerCar()
	{
		MountData mountDataById = DataManager.GetMountDataById(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MountId);
		ObjCarInitData initData = new ObjCarInitData(this.mPlayerCarCreatePos, this.mPlayerCarCreateAngle, UUID.GenUUID(), string.Empty, false, mountDataById);
		Singleton<ObjManager>.Instance.CreateMainPlayerCar(initData);
		this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
		this.mSmoothFollowCamPos.SetTarget(this.mPlayerCar.CarControl);
	}

	// Token: 0x06003974 RID: 14708 RVA: 0x000F621C File Offset: 0x000F441C
	public void OnPlayerCarMeshLoadDone()
	{
		GameManager.IsSceneReady = true;
	}

	// Token: 0x06003975 RID: 14709 RVA: 0x000F6224 File Offset: 0x000F4424
	public void OnMainPlayerCreate()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		ObjManager instance = Singleton<ObjManager>.Instance;
		this.mMainPlayer = instance.MainPlayer;
		this.mMainPlayer.CurAnimationState = GameDefine.ANIMATIONSTATE.DRIVING;
		this.mMainPlayer.CurPlayerState = PLAYER_STATE.DRIVING;
		this.mMainPlayer.rigidbody.isKinematic = true;
		GameManager.IsSceneReady = false;
		this.CreatePlayerCar();
	}

	// Token: 0x06003976 RID: 14710 RVA: 0x000F6290 File Offset: 0x000F4490
	private void EndMission(bool isSuccess)
	{
		if (this.mEndMissionFlag)
		{
			return;
		}
		Singleton<ObjManager>.Instance.StopAllPoliceSound();
		this.mEndMissionFlag = true;
		int curMissionTime = this.GetCurMissionTime();
		copyscene_info copyscene_info = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.DailyCopyInfoDic[this.mCurCopySceneData.ID];
		string text = string.Format("{0}", this.mCurPathPointTime[0]);
		for (int i = 1; i < this.mCurPathPointTime.Length; i++)
		{
			text = string.Format("{0}#{1}", text, this.mCurPathPointTime[i]);
		}
		if (copyscene_info.HasBestGrade)
		{
			if (copyscene_info.BestGrade > (long)curMissionTime)
			{
				copyscene_info.BestGrade = (long)curMissionTime;
				copyscene_info.str = text;
			}
		}
		else
		{
			copyscene_info.BestGrade = (long)curMissionTime;
			copyscene_info.str = text;
		}
		car_chase_result.request request = new car_chase_result.request();
		request.state = isSuccess;
		request.param1 = (long)curMissionTime;
		request.param2 = text;
		NetLogic.GetInstance().Send<Protocol.car_chase_result>(request, null);
		this.mPlayerCar.StopCar();
		CameraController cameraController = this.mMainPlayer.CameraController;
		cameraController.LerpToTargetLocalZero(this.mPlayerCar.WinCamView, 1f, delegate
		{
			this.mPlayerCar.WinCamView.animation.Play();
		});
		SingletonUnity<UIManager>.Instance.HideBaseUI();
	}

	// Token: 0x06003977 RID: 14711 RVA: 0x000F63E0 File Offset: 0x000F45E0
	public int GetCurMissionTime()
	{
		return (int)((Time.time - this.mMissionStartTime) * 100f);
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x000F63F8 File Offset: 0x000F45F8
	public override void LeaveScene()
	{
		MessageBoxLogic.OpenOKCancelBox(this.mapInfoData.MExitCon, StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
			Singleton<ObjManager>.Instance.StopAllPoliceSound();
		}, null, null, null);
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x000F6448 File Offset: 0x000F4648
	public void ResetPlayerCarToLastPoint()
	{
		this.mPlayerCar.rigidbody.velocity = Vector3.zero;
		this.mPlayerCar.rigidbody.angularVelocity = Vector3.zero;
		if (this.mCurPlayerPathIndex == 0)
		{
			this.mPlayerCar.transform.position = this.mPlayerCarCreatePos;
			this.mPlayerCar.transform.eulerAngles = this.mPlayerCarCreateAngle;
		}
		else
		{
			this.mPlayerCar.transform.position = this.mPlayerPathList[this.mCurPlayerPathIndex - 1];
			this.mPlayerCar.transform.forward = (this.mPlayerPathList[this.mCurPlayerPathIndex] - this.mPlayerPathList[this.mCurPlayerPathIndex - 1]).normalized;
		}
	}

	// Token: 0x0400258D RID: 9613
	private SmoothFollowNew mSmoothFollowCamPos;

	// Token: 0x0400258E RID: 9614
	private Vector3 mMainPlayerRunTargetPos;

	// Token: 0x0400258F RID: 9615
	private Vector3 mPlayerCarCreatePos;

	// Token: 0x04002590 RID: 9616
	private Vector3 mPlayerCarCreateAngle;

	// Token: 0x04002591 RID: 9617
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002592 RID: 9618
	private ObjPlayerCar mPlayerCar;

	// Token: 0x04002593 RID: 9619
	private MovePathPoint mMovePathPoint;

	// Token: 0x04002594 RID: 9620
	private List<Vector3> mPlayerPathList = new List<Vector3>();

	// Token: 0x04002595 RID: 9621
	private int mCurPlayerPathIndex;

	// Token: 0x04002596 RID: 9622
	private float MISSION_TIME = 180f;

	// Token: 0x04002597 RID: 9623
	private float mMissionTimeCount;

	// Token: 0x04002598 RID: 9624
	private CopySceneData mCurCopySceneData;

	// Token: 0x04002599 RID: 9625
	private bool mStartTimeCountFlag;

	// Token: 0x0400259A RID: 9626
	private int mBestTime;

	// Token: 0x0400259B RID: 9627
	private int[] mPathPointBestTime;

	// Token: 0x0400259C RID: 9628
	private int[] mCurPathPointTime;

	// Token: 0x0400259D RID: 9629
	private float mMissionStartTime;

	// Token: 0x0400259E RID: 9630
	private Dictionary<string, GameObject> mStaticBlockDic = new Dictionary<string, GameObject>();

	// Token: 0x0400259F RID: 9631
	private Dictionary<int, MoveCarBlock> mMoveCarBlockDic = new Dictionary<int, MoveCarBlock>();

	// Token: 0x040025A0 RID: 9632
	private float FreshNPCTimeCount;

	// Token: 0x040025A1 RID: 9633
	private float FreshNPCTime = 2f;

	// Token: 0x040025A2 RID: 9634
	private float PlayerBolckCheckTime = 5f;

	// Token: 0x040025A3 RID: 9635
	private float playerBlockTimeCount;

	// Token: 0x040025A4 RID: 9636
	private Vector3 prePos;

	// Token: 0x040025A5 RID: 9637
	private bool LerpToCarFlag;

	// Token: 0x040025A6 RID: 9638
	private Vector3 startPosition;

	// Token: 0x040025A7 RID: 9639
	private Quaternion startRotation;

	// Token: 0x040025A8 RID: 9640
	private float lerpTime = 1f;

	// Token: 0x040025A9 RID: 9641
	private float lerpCountTime;

	// Token: 0x040025AA RID: 9642
	private bool mEndMissionFlag;
}
