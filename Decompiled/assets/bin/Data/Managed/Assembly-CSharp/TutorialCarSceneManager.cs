using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x0200088F RID: 2191
public class TutorialCarSceneManager : SceneManager
{
	// Token: 0x06003B20 RID: 15136 RVA: 0x001014A8 File Offset: 0x000FF6A8
	public override void Init(string id)
	{
		base.Init(id);
		GameObject gameObject = GameObject.Find("SmoothFollowCamPos");
		if (gameObject != null)
		{
			this.mSmoothFollowCamPos = gameObject.GetComponent<SmoothFollowNew>();
		}
		UnityVersionUtil.SetActiveRecursive(this.mSmoothFollowCamPos.gameObject, false);
		gameObject = GameObject.Find("MovePathPoint");
		this.mMovePathPoint = gameObject.GetComponent<MovePathPoint>();
		this.mMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnPlayerArrivePathPoint));
		this.mWaveCount = base.GetMonsterGroupCount();
		this.mStepIndex = 0;
		this.mCurEnemyGroup = 0;
		this.mCurPlayerPathIndex = 1;
		if (SingletonUnity<MyEvent>.Exists)
		{
			SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		}
		else
		{
			Debug.Log("SingletonUnity<MyEvent>.Exists == false");
		}
		UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, false);
		this.mMovePathPoint.ActiveRadius = 1f;
		ResourcesManager.LoadAndInstantiate("Tutorial/TutorialStaticMesh");
		this.mEndMissionFlag = false;
		this.InitBlock(id);
		this.StrikePool = new SimplePool<ParticleSystem>();
		this.StrikePool.Reset(new SimplePool<ParticleSystem>.CreateFunc(base.CreateStrike), new SimplePool<ParticleSystem>.DestroyFunc(base.DestroyStrike), 10);
		this.mRandomWalkPosRoot = GameObject.Find("TutorialWalkNPCPos");
		this.mRandomWalkPosList = new List<Vector3>();
		for (int i = 0; i < this.mRandomWalkPosRoot.transform.childCount; i++)
		{
			this.mRandomWalkPosList.Add(this.mRandomWalkPosRoot.transform.GetChild(i).position);
		}
		for (int j = 0; j < this.mRandomWalkPosList.Count; j++)
		{
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			objInitNpcData.mServerID = UUID.GenUUID();
			objInitNpcData.mPos = this.mRandomWalkPosList[j];
			NpcData npcDataByID = DataManager.GetNpcDataByID("301");
			objInitNpcData.HP = npcDataByID.Hp;
			objInitNpcData.MaxHP = npcDataByID.Hp;
			objInitNpcData.npcInfoData = npcDataByID;
			objInitNpcData.mCharacterModelId = npcDataByID.Model;
			Singleton<ObjManager>.Instance.GetRagdollNPC(objInitNpcData, delegate(ObjNPC npc)
			{
				if (npc != null)
				{
					this.mWalkNpcList.Add(npc as ObjRagdollNPC);
					npc.WalkMoveTo(this.mRandomWalkPosList[Random.Range(0, this.mRandomWalkPosList.Count)], (float)Random.Range(1, 4), new ObjCharacter.TargetArriveFinsh(this.OnNPCArriveMoveTarget));
				}
			});
		}
		GameObject gameObject2 = GameObject.Find("TutorialPlayerPathRoot");
		this.mPlayerPathPointRoot = gameObject2.transform;
	}

	// Token: 0x06003B21 RID: 15137 RVA: 0x001016E0 File Offset: 0x000FF8E0
	private void OnNPCArriveMoveTarget(ObjCharacter objCha)
	{
		objCha.WalkMoveTo(this.mRandomWalkPosList[Random.Range(0, this.mRandomWalkPosList.Count)], (float)Random.Range(1, 4), new ObjCharacter.TargetArriveFinsh(this.OnNPCArriveMoveTarget));
	}

	// Token: 0x06003B22 RID: 15138 RVA: 0x00101724 File Offset: 0x000FF924
	private void ClearWalkNpc()
	{
		for (int i = this.mWalkNpcList.Count - 1; i >= 0; i--)
		{
			this.mWalkNpcList[i].RecycleSelf();
		}
		this.mWalkNpcList.Clear();
	}

	// Token: 0x06003B23 RID: 15139 RVA: 0x0010176C File Offset: 0x000FF96C
	private void InitBlock(string sceneId)
	{
		Debug.Log("sceneId :: " + sceneId);
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
					boxCollider.size = new Vector3(20f, 5f, 1f);
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

	// Token: 0x06003B24 RID: 15140 RVA: 0x00101E2C File Offset: 0x0010002C
	public override void StartGame()
	{
		Debug.Log("CarChaseSceneManager StartGame");
	}

	// Token: 0x06003B25 RID: 15141 RVA: 0x00101E38 File Offset: 0x00100038
	private void OnArriveCarPathPoint(Vector3 pos)
	{
		this.mCurPlayerCarPathIndex++;
		if (this.mCurPlayerCarPathIndex < this.mPlayerCarPathList.Count)
		{
			this.SetPlayerCarMoveTarget();
		}
		else
		{
			this.MoveNextState();
		}
	}

	// Token: 0x06003B26 RID: 15142 RVA: 0x00101E70 File Offset: 0x00100070
	public override void SuccessMission()
	{
		this.EndMission(true);
	}

	// Token: 0x06003B27 RID: 15143 RVA: 0x00101E7C File Offset: 0x0010007C
	public override void FailMission()
	{
		this.EndMission(false);
	}

	// Token: 0x06003B28 RID: 15144 RVA: 0x00101E88 File Offset: 0x00100088
	public override void Update()
	{
	}

	// Token: 0x06003B29 RID: 15145 RVA: 0x00101E8C File Offset: 0x0010008C
	private void CheckPlayerBlock()
	{
		if (Vector3.SqrMagnitude(this.prePos - this.mPlayerCar.Position) < 4f)
		{
			this.EndMission(false);
		}
		this.prePos = this.mPlayerCar.Position;
	}

	// Token: 0x06003B2A RID: 15146 RVA: 0x00101ED8 File Offset: 0x001000D8
	private void FreshNPC()
	{
		if (SingletonUnity<EasyCitySimController>.Exists)
		{
			SingletonUnity<EasyCitySimController>.Instance.FreshNPC(this.mMainPlayer.CacheTransform);
		}
	}

	// Token: 0x06003B2B RID: 15147 RVA: 0x00101EFC File Offset: 0x001000FC
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
					Debug.Log("PlayAnimation Finish");
					this.mMainPlayer.DisableMainPlayer();
					this.MoveNextState();
					this.ChangeToCarCtl();
				}, -1f);
				ObjInitNpcData objInitNpcData = new ObjInitNpcData();
				objInitNpcData.mServerID = UUID.GenUUID();
				objInitNpcData.mPos = this.mPlayerCar.DummyNPCPoint.position;
				NpcData npcDataByID = DataManager.GetNpcDataByID("1001");
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

	// Token: 0x06003B2C RID: 15148 RVA: 0x00101F48 File Offset: 0x00100148
	private void GetOffCar()
	{
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		this.mPlayerCar.DisableCar();
		this.mMainPlayer.EnableMainPlayer();
		this.mPlayerCar.MeshRoot.animation.Play("GetOffCar");
		this.mMainPlayer.CameraController.LerpToTargetLocalZero(this.mPlayerCar.CarDoorView, 1f, null);
		this.mMainPlayer.AnimationLogic.PlayAnimation("GetOffCar", delegate()
		{
			for (int i = 0; i < this.mMainPlayer.PartObject.Length; i++)
			{
				if (this.mMainPlayer.PartObject[i] != null)
				{
					this.mMainPlayer.PartObject[i].layer = LayerMask.NameToLayer("ShadowCaster");
				}
			}
		}, -1f);
	}

	// Token: 0x06003B2D RID: 15149 RVA: 0x00101FD8 File Offset: 0x001001D8
	private void ChangeToCarCtl()
	{
		UIManager instance = SingletonUnity<UIManager>.Instance;
		instance.ReShowBaseUI();
		instance.CloseUI(UIInfo.YiDongKongZhiUI);
		instance.CloseUI(UIInfo.JueseJiNengQuUI);
		instance.CloseUI(UIInfo.TouXiangKuangUI);
		instance.ShowCarDefaultUI();
		instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMovePathPoint.gameObject, this.mPlayerCar.gameObject);
		}, null);
		MapInfoData mapInfo = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		if (!string.IsNullOrEmpty(mapInfo.MiniMapName))
		{
			instance.ShowUI(UIInfo.MiniMapRoot, delegate
			{
				SingletonUnity<MiniMap>.Instance.Reset(mapInfo.fMapLength, mapInfo.fMapHeight, 200f, mapInfo.MiniMapName, mapInfo.Name);
				SingletonUnity<MiniMap>.Instance.SetTarget(this.mPlayerCarPathList[0]);
			}, null);
		}
		CameraController cameraController = this.mMainPlayer.CameraController;
		cameraController.LerpToTargetLocalZero(this.mSmoothFollowCamPos.transform, 1f, delegate
		{
			this.mPlayerCar.EnableCar(this.mMainPlayer);
		});
	}

	// Token: 0x06003B2E RID: 15150 RVA: 0x001020B0 File Offset: 0x001002B0
	private void CreatePlayerCar()
	{
		ObjCarInitData initData = new ObjCarInitData(this.mPlayerCarCreatePos, this.mPlayerCarCreateAngle, UUID.GenUUID(), "Chevrolet", false, null);
		Singleton<ObjManager>.Instance.CreateTutorialPlayerCar(initData);
		this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
		this.mSmoothFollowCamPos.SetTarget(this.mPlayerCar.CarControl);
	}

	// Token: 0x06003B2F RID: 15151 RVA: 0x00102110 File Offset: 0x00100310
	public void OnMainPlayerCreate()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.mMainPlayer.gameObject.rigidbody.isKinematic = false;
		this.mMainPlayer.Position = this.mPlayerPathPointRoot.GetChild(0).position;
		this.mMainPlayer.FaceToPub(this.mMainPlayer.Position + this.mPlayerPathPointRoot.GetChild(0).forward);
		GameObject gameObject = GameObject.Find("TutorialStartCamMove");
		CameraController cam = this.mMainPlayer.CameraController;
		cam.Init(CameraController.CAMERAVIEWSTATE.FIXED);
		cam.enabled = false;
		cam.transform.parent = gameObject.transform;
		cam.transform.localPosition = Vector3.zero;
		cam.transform.localRotation = Quaternion.identity;
		gameObject.animation.Play();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PrinterPageRoot, delegate
		{
			SingletonUnity<PrinterPageRootLogic>.Instance.Reset(this.PageStr, delegate
			{
				cam.LerpBackToPlayer(0f, null);
				StoryDialogRootLogic.ShowStory("101", null);
				UIUpdateEvent.OnStoryShowOver = (UIUpdateEvent.OnStoryShowOverDelegate)Delegate.Combine(UIUpdateEvent.OnStoryShowOver, new UIUpdateEvent.OnStoryShowOverDelegate(this.OnStoryShowOver));
			});
		}, null);
	}

	// Token: 0x06003B30 RID: 15152 RVA: 0x0010224C File Offset: 0x0010044C
	private void OnStoryShowOver(string storyId)
	{
		UIUpdateEvent.OnStoryShowOver = (UIUpdateEvent.OnStoryShowOverDelegate)Delegate.Remove(UIUpdateEvent.OnStoryShowOver, new UIUpdateEvent.OnStoryShowOverDelegate(this.OnStoryShowOver));
		this.MoveNextState();
	}

	// Token: 0x06003B31 RID: 15153 RVA: 0x00102280 File Offset: 0x00100480
	private void EndMission(bool isSuccess)
	{
		if (this.mEndMissionFlag)
		{
			return;
		}
		this.mEndMissionFlag = true;
		this.GetOffCar();
	}

	// Token: 0x06003B32 RID: 15154 RVA: 0x0010229C File Offset: 0x0010049C
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

	// Token: 0x06003B33 RID: 15155 RVA: 0x001022EC File Offset: 0x001004EC
	private void OnPlayerArrivePathPoint(Vector3 pos)
	{
		UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, false);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
		this.MoveNextState();
		this.mCurPlayerPathIndex++;
	}

	// Token: 0x06003B34 RID: 15156 RVA: 0x00102330 File Offset: 0x00100530
	public void MoveNextState()
	{
		this.mStepIndex++;
		switch (this.mStepIndex)
		{
		case 1:
			SingletonUnity<UIManager>.Instance.ShowTutorialDefaultUI();
			TutorialManager.ShowTutorial(TUTORIAL_STEP.SWITCH_VIEW);
			break;
		case 2:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SelectTargetUI, null, null);
			this.ClearWalkNpc();
			SingletonUnity<JueseJiNengQuLogic>.Instance.ShowSkillBtn(0);
			TutorialManager.MoveNext(false);
			this.CreateNPCGroup();
			break;
		case 3:
			SingletonUnity<JueseJiNengQuLogic>.Instance.ShowAllSkillBtn();
			TutorialManager.ShowTutorial(TUTORIAL_STEP.SKILL1_BUTTON);
			this.CreateNPCGroup();
			break;
		case 4:
			this.SetPlayerMoveTarget();
			break;
		case 5:
			this.StartRobCarState();
			break;
		case 6:
			this.mMovePathPoint.DeRegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnPlayerArrivePathPoint));
			this.mMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArriveCarPathPoint));
			this.mMovePathPoint.ActiveRadius = 15f;
			this.SetPlayerCarMoveTarget();
			break;
		case 7:
			this.StartCrashCar();
			break;
		}
	}

	// Token: 0x06003B35 RID: 15157 RVA: 0x00102448 File Offset: 0x00100648
	private void StartCrashCar()
	{
		UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.gameObject, false);
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		string path = string.Format("Tutorial/CrashCarAnima", new object[0]);
		GameObject gameObject = ResourcesManager.LoadAndInstantiate(path) as GameObject;
		SceneAnimationCtl component = gameObject.GetComponent<SceneAnimationCtl>();
		component.RegisterOnFinished(new DelegateDefine.NoParamDelegate(this.FinishTutorial));
	}

	// Token: 0x06003B36 RID: 15158 RVA: 0x001024A8 File Offset: 0x001006A8
	private void FinishTutorial()
	{
		vp_Timer.In(5f, delegate()
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
			enter_new_map.request request = new enter_new_map.request();
			request.mapInfoId = 101.ToString();
			NetLogic.GetInstance().Send<Protocol.enter_new_map>(request, null);
		}, null);
	}

	// Token: 0x06003B37 RID: 15159 RVA: 0x001024E0 File Offset: 0x001006E0
	private void StartRobCarState()
	{
		this.mMainPlayer.DisactiveIdleAttack();
		this.mMainPlayer.DisableMainPlayer();
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		string path = string.Empty;
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession == PROFESSION_TYPE.XD)
		{
			path = string.Format("Tutorial/XD_RobCarSceneAnima", new object[0]);
		}
		else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession == PROFESSION_TYPE.QJ)
		{
			path = string.Format("Tutorial/XD_RobCarSceneAnima", new object[0]);
		}
		else
		{
			path = string.Format("Tutorial/XD_RobCarSceneAnima", new object[0]);
		}
		GameObject gameObject = ResourcesManager.LoadAndInstantiate(path) as GameObject;
		SceneAnimationCtl component = gameObject.GetComponent<SceneAnimationCtl>();
		component.RegisterOnFinished(new DelegateDefine.NoParamDelegate(this.onFinishedRobCarState));
	}

	// Token: 0x06003B38 RID: 15160 RVA: 0x001025A0 File Offset: 0x001007A0
	private void onFinishedRobCarState()
	{
		UnityVersionUtil.SetActiveRecursive(this.mSmoothFollowCamPos.gameObject, true);
		this.CreatePlayerCar();
		this.mPlayerCar.transform.position = new Vector3(-36f, 0f, 174f);
		this.mSmoothFollowCamPos.UpdateToTargetPos();
		this.mMainPlayer.Position = new Vector3(-33f, 0f, 170f);
		this.mMainPlayer.EnableMainPlayer();
		this.mMainPlayer.EnableNavMeshAgent();
		this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 0.1f, delegate(ObjCharacter A_1)
		{
			this.RobCar();
		});
	}

	// Token: 0x06003B39 RID: 15161 RVA: 0x00102654 File Offset: 0x00100854
	public void SetPlayerMoveTarget()
	{
		this.mMovePathPoint.transform.position = this.mPlayerPathPointRoot.GetChild(this.mCurPlayerPathIndex).position;
		UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, true);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMovePathPoint.gameObject, this.mMainPlayer.gameObject);
		}, null);
	}

	// Token: 0x06003B3A RID: 15162 RVA: 0x001026B4 File Offset: 0x001008B4
	public void SetPlayerCarMoveTarget()
	{
		this.mMovePathPoint.transform.position = this.mPlayerCarPathList[this.mCurPlayerCarPathIndex];
		UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, true);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMovePathPoint.gameObject, this.mPlayerCar.gameObject);
		}, null);
	}

	// Token: 0x06003B3B RID: 15163 RVA: 0x00102710 File Offset: 0x00100910
	private void CreateNPCGroup()
	{
		this.mCurEnemyGroup++;
		this.NpcCreate(base.GetMonsterDataByGroup(this.mCurEnemyGroup));
	}

	// Token: 0x06003B3C RID: 15164 RVA: 0x00102740 File Offset: 0x00100940
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
			NpcData npcDataByID = DataManager.GetNpcDataByID(monsterData.NpcID);
			objInitNpcData.npcInfoData = npcDataByID;
			objInitNpcData.HP = npcDataByID.Hp;
			objInitNpcData.MaxHP = npcDataByID.Hp;
			if (npcDataByID.AI.Equals("BlockAI"))
			{
				Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, null, null);
			}
			else
			{
				Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, new ObjManager.OnGetNPC(this.OnNPCCreated), null);
			}
		}
	}

	// Token: 0x06003B3D RID: 15165 RVA: 0x0010280C File Offset: 0x00100A0C
	private void OnNPCCreated(ObjNPC npc)
	{
		npc.ChangeBornPos(Singleton<ObjManager>.Instance.MainPlayer.Position);
	}

	// Token: 0x06003B3E RID: 15166 RVA: 0x00102824 File Offset: 0x00100A24
	public override void OnNPCDie(object objNpc)
	{
		ObjNPC objNpc2 = objNpc as ObjNPC;
		if (Singleton<ObjManager>.Instance.CheckNPCClear(objNpc2))
		{
			this.MoveNextState();
		}
	}

	// Token: 0x040026AD RID: 9901
	private string[] PageStr = new string[]
	{
		"My brother is a detective at FBI. At the age of 13, our parents were involved in the terrorist attacks, my brother vowed to punish the wicked. In my eyes, becoming FBI's brother is no doubt a great superhero.\n10 years later, as usual, I was preparing for work, when suddenly the black man who knocked on the door told me the news that had changed my life......",
		"\"Your brother, the mob was in the act of killing, has been confirmed.\"\nThe irony of life, my last relatives, my super hero, also died in the hands of the terrorist.After that, I found a hidden note from brother's room, and I knew the case that my brother had been tracking was a shooting and murder in Vice City. Even more so, brother shows that the case is likely to belong to the terrorist that killed its parents 10 years ago.",
		"In order to find out the truth, I quit my job as a journalist. Bought some light equipment through the familiar arms dealer. Pack up, came to Vice City."
	};

	// Token: 0x040026AE RID: 9902
	private SmoothFollowNew mSmoothFollowCamPos;

	// Token: 0x040026AF RID: 9903
	private Vector3 mMainPlayerRunTargetPos;

	// Token: 0x040026B0 RID: 9904
	private Vector3 mPlayerCarCreatePos;

	// Token: 0x040026B1 RID: 9905
	private Vector3 mPlayerCarCreateAngle;

	// Token: 0x040026B2 RID: 9906
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040026B3 RID: 9907
	private ObjPlayerCar mPlayerCar;

	// Token: 0x040026B4 RID: 9908
	private MovePathPoint mMovePathPoint;

	// Token: 0x040026B5 RID: 9909
	private List<Vector3> mPlayerCarPathList = new List<Vector3>();

	// Token: 0x040026B6 RID: 9910
	private int mCurPlayerCarPathIndex;

	// Token: 0x040026B7 RID: 9911
	private CopySceneData mCurCopySceneData;

	// Token: 0x040026B8 RID: 9912
	private CarHPRootLogic carHPRoot;

	// Token: 0x040026B9 RID: 9913
	private Transform mPlayerPathPointRoot;

	// Token: 0x040026BA RID: 9914
	private int mWaveCount;

	// Token: 0x040026BB RID: 9915
	private int mStepIndex;

	// Token: 0x040026BC RID: 9916
	private int mCurEnemyGroup;

	// Token: 0x040026BD RID: 9917
	private int mCurPlayerPathIndex;

	// Token: 0x040026BE RID: 9918
	private GameObject mRandomWalkPosRoot;

	// Token: 0x040026BF RID: 9919
	private List<Vector3> mRandomWalkPosList;

	// Token: 0x040026C0 RID: 9920
	private List<ObjRagdollNPC> mWalkNpcList = new List<ObjRagdollNPC>();

	// Token: 0x040026C1 RID: 9921
	private Dictionary<string, GameObject> mStaticBlockDic = new Dictionary<string, GameObject>();

	// Token: 0x040026C2 RID: 9922
	private Dictionary<int, MoveCarBlock> mMoveCarBlockDic = new Dictionary<int, MoveCarBlock>();

	// Token: 0x040026C3 RID: 9923
	private float FreshNPCTimeCount;

	// Token: 0x040026C4 RID: 9924
	private float FreshNPCTime = 2f;

	// Token: 0x040026C5 RID: 9925
	private float PlayerBolckCheckTime = 5f;

	// Token: 0x040026C6 RID: 9926
	private float playerBlockTimeCount;

	// Token: 0x040026C7 RID: 9927
	private Vector3 prePos;

	// Token: 0x040026C8 RID: 9928
	private bool LerpToCarFlag;

	// Token: 0x040026C9 RID: 9929
	private Vector3 startPosition;

	// Token: 0x040026CA RID: 9930
	private Quaternion startRotation;

	// Token: 0x040026CB RID: 9931
	private float lerpTime = 1f;

	// Token: 0x040026CC RID: 9932
	private float lerpCountTime;

	// Token: 0x040026CD RID: 9933
	private bool mEndMissionFlag;
}
