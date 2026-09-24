using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class MiniMap : SingletonUnity<MiniMap>
{
	// Token: 0x060009EE RID: 2542 RVA: 0x0004812C File Offset: 0x0004632C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00048138 File Offset: 0x00046338
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00048158 File Offset: 0x00046358
	private new void Awake()
	{
		base.Awake();
		MeshFilter component = this.MapPicObj.GetComponent<MeshFilter>();
		if (component.mesh == null)
		{
			component.mesh = new Mesh();
		}
		this.mMapMesh = component.mesh;
		this.mMapMat = this.MapPicObj.renderer.sharedMaterial;
		this.GenerateMesh();
		this.UpdateNpcPos();
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x000481C4 File Offset: 0x000463C4
	private void InitOnline()
	{
		int curLineIndex = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex;
		if (curLineIndex >= 0)
		{
			this.OnlineLabel.text = string.Format("{0}", curLineIndex);
		}
		else
		{
			this.OnlineLabel.text = "-";
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00048218 File Offset: 0x00046418
	public void Reset(float mapLength, float mapHeight, float showLength, string minimapName, string mapName)
	{
		this.curMapId = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID;
		if (this.curMapId.Equals("11"))
		{
			showLength = 100f;
		}
		this.mMapLength = mapLength;
		this.mMapHeight = mapHeight;
		this.mMapShowRange = showLength / mapLength / 2f * 1.5f;
		this.mRadius = this.mMapShowRange;
		this.mMapWHRatio = this.mMapLength / this.mMapHeight;
		this.mMapMat.SetFloat("_Radius", this.mRadius);
		this.mMapMat.SetFloat("_WHRatio", this.mMapWHRatio);
		this.mMapMat.SetFloat("_OutLine", this.mRadius * 0.01f);
		float num = showLength / mapLength / 3f * 1.5f;
		this.mMapMat.SetFloat("_PoliceRadius", num);
		this.SetPoliceAlph(0f);
		MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(this.curMapId);
		this.mMapMat.SetFloat("_LockShowRange", 1.1f);
		if (this.mMapMat.mainTexture == null || !this.mMapMat.mainTexture.name.Equals(minimapName))
		{
			this.curMapName = minimapName;
			if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(minimapName, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
				this.mMapMat.SetTexture("_AreaTipTex", null);
			}
		}
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.mPlayerTransform = this.mMainPlayer.CacheTransform;
		this.mCamTrans = Singleton<ObjManager>.Instance.MainPlayer.CameraController.transform;
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsHaveMoveTarget)
		{
			UnityVersionUtil.SetActiveRecursive(this.TargetPic.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TargetPic.gameObject, false);
		}
		this.MapNameLabel.text = StrDictionary.GetDictionaryString(mapName, new object[0]);
		this.InitOnline();
		this.UpdateMapName();
		this.UpdateActionMapObj();
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCopyScene())
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
			{
				this.ClickTipsSprite.enabled = true;
			}
			else
			{
				this.ClickTipsSprite.enabled = false;
			}
		}
		else
		{
			this.ClickTipsSprite.enabled = true;
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x000484A0 File Offset: 0x000466A0
	private void TextureLoadFinish(string name, Texture curtex)
	{
		if (string.IsNullOrEmpty(this.curMapName) || this.curMapName.Equals(name))
		{
			this.mMapMat.SetTexture("_MainTex", curtex);
		}
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000484E0 File Offset: 0x000466E0
	private void UpdateMapName()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCopyShowMap())
		{
			UnityVersionUtil.SetActiveRecursive(this.MapNameObj, false);
		}
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00048510 File Offset: 0x00046710
	public void OnClickMapBtn()
	{
		if (GameSettingData.IsLowPhone)
		{
			return;
		}
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType == MAPTYPE.CAR_CHASE_COPY)
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCopyScene() && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MapUIRoot, delegate
			{
				SingletonUnity<MapUIRootLogic>.Instance.Reset();
			}, null);
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
			if (TutorialManager.CurStep == TUTORIAL_STEP.NEW_MAP_TIP_START || TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.TOWER_START || TutorialManager.CurStep == TUTORIAL_STEP.RANK_PVP_START || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_START || TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_START || TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_START || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_START || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "minimapBtn");
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000485F8 File Offset: 0x000467F8
	private void UpdateNpcPos()
	{
		List<ObjNPC> mapShowObjList = Singleton<ObjManager>.Instance.MapShowObjList;
		int num = mapShowObjList.Count - this.NpcPointPicList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.NpcPointPicList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.NpcPointPicList[0].transform.parent;
				gameObject.transform.localScale = Vector3.one;
				this.NpcPointPicList.Add(gameObject.GetComponent<UISprite>());
			}
		}
		for (int j = 0; j < mapShowObjList.Count; j++)
		{
			Vector3 position = mapShowObjList[j].Position;
			Vector2 vector;
			vector..ctor((position.x + this.mMapLength / 2f) / this.mMapLength, (position.z + this.mMapHeight / 2f) / this.mMapHeight);
			Vector2 vector2 = vector - this.playerPos;
			float num2 = this.mRadius * 0.92f;
			if (vector2.sqrMagnitude < num2 * num2)
			{
				this.NpcPointPicList[j].enabled = true;
				this.NpcPointPicList[j].spriteName = GameDefine.GetObjTypeSpriteName(mapShowObjList[j]);
				if (mapShowObjList[j].IsCityCaptureNpc())
				{
					this.NpcPointPicList[j].SetDimensions(20, 20);
				}
				else
				{
					this.NpcPointPicList[j].MakePixelPerfect();
				}
				this.NpcPointPicList[j].transform.localPosition = vector2 / this.mRadius * this.MapSize / 2f;
				if (mapShowObjList[j].IsCityCaptureNpc())
				{
					this.NpcPointPicList[j].transform.localEulerAngles = new Vector3(0f, 0f, -this.NpcPointPicList[0].transform.parent.localEulerAngles.z);
				}
			}
			else
			{
				this.NpcPointPicList[j].enabled = false;
			}
		}
		for (int k = mapShowObjList.Count; k < this.NpcPointPicList.Count; k++)
		{
			this.NpcPointPicList[k].enabled = false;
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0004888C File Offset: 0x00046A8C
	private Vector2 WorldPos2MapPos(Vector3 pos)
	{
		Vector2 vector;
		vector..ctor((pos.x + this.mMapLength / 2f) / this.mMapLength, (pos.z + this.mMapHeight / 2f) / this.mMapHeight);
		Vector2 vector2 = vector - this.playerPos;
		return vector2 / this.mRadius * this.MapSize / 2f;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00048908 File Offset: 0x00046B08
	public void UpdateActionMapObj()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		this.curActivityMapDataList = sceneManager.CurActivityMapDataList;
		List<string> curAvailableMissionIdList = sceneManager.CurAvailableMissionIdList;
		Dictionary<string, List<string>> curMissionNpcDic = sceneManager.CurMissionNpcDic;
		this.curShowMissionList = new List<string>();
		List<string> list = new List<string>(curMissionNpcDic.Keys);
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		this.curMapObjDataList.Clear();
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < list.Count; i++)
		{
			text = list[i];
			List<string> list2 = curMissionNpcDic[text];
			text2 = string.Empty;
			if (list2 != null && list2.Count > 0)
			{
				MISSION_STATE mission_STATE = MISSION_STATE.INVALID;
				text2 = list2[0];
				for (int j = 0; j < list2.Count; j++)
				{
					mission_STATE = missionManager.GetMissionState(list2[j]);
					if (mission_STATE == MISSION_STATE.COMPLETE)
					{
						text2 = list2[j];
						break;
					}
					if (mission_STATE == MISSION_STATE.ACCEPTED)
					{
						text2 = list2[j];
					}
				}
				this.curShowMissionList.Add(text2);
				this.curMapObjDataList.Add(new ActivityObjData(text2, mission_STATE, DataManager.GetNPCPosInMonsterData(this.curMapId, text)));
			}
		}
		for (int k = 0; k < curAvailableMissionIdList.Count; k++)
		{
			if (!this.curShowMissionList.Contains(curAvailableMissionIdList[k]))
			{
				MISSION_STATE mission_STATE = missionManager.GetMissionState(curAvailableMissionIdList[k]);
				if (mission_STATE == MISSION_STATE.ACCEPTED)
				{
					MissionData missionDataByID = DataManager.GetMissionDataByID(curAvailableMissionIdList[k]);
					if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
					{
						SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(missionDataByID.LogicID);
						this.curMapObjDataList.Add(new ActivityObjData(curAvailableMissionIdList[k], mission_STATE, new Vector3(surveyMissionDataById.PosX, 0f, surveyMissionDataById.PosZ)));
						this.curShowMissionList.Add(curAvailableMissionIdList[k]);
					}
				}
			}
		}
		for (int l = 0; l < this.curActivityMapDataList.Count; l++)
		{
			if (this.curActivityMapDataList[l].IsVisible)
			{
				this.curMapObjDataList.Add(new ActivityObjData(this.curActivityMapDataList[l]));
			}
		}
		int num = this.curMapObjDataList.Count - this.ActionMapObjList.Count;
		if (num > 0)
		{
			for (int m = 0; m < num; m++)
			{
				GameObject gameObject = Object.Instantiate(this.ActionMapObjList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.ActionMapObjList[0].transform.parent;
				gameObject.transform.localScale = Vector3.one;
				this.ActionMapObjList.Add(gameObject.GetComponent<NewMiniMapActivityObj>());
			}
		}
		this.UpdateActionMapObjPos();
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00048C14 File Offset: 0x00046E14
	private void UpdateActionMapObjPos()
	{
		for (int i = 0; i < this.ActionMapObjList.Count; i++)
		{
			if (i < this.curMapObjDataList.Count)
			{
				Vector3 position = this.curMapObjDataList[i].Position;
				Vector2 vector;
				vector..ctor((position.x + this.mMapLength / 2f) / this.mMapLength, (position.z + this.mMapHeight / 2f) / this.mMapHeight);
				Vector2 vector2 = vector - this.playerPos;
				float num = this.mRadius * 0.85f;
				if (vector2.sqrMagnitude < num * num)
				{
					NGUITools.SetActive(this.ActionMapObjList[i].gameObject, true);
					this.ActionMapObjList[i].Reset(this.curMapObjDataList[i].GetIcon(), this.curMapObjDataList[i].GetStateIcon(), this.curMapObjDataList[i].IsMission());
					this.ActionMapObjList[i].transform.localPosition = vector2 / this.mRadius * this.MapSize / 2f;
				}
				else
				{
					NGUITools.SetActive(this.ActionMapObjList[i].gameObject, false);
				}
			}
			else
			{
				NGUITools.SetActive(this.ActionMapObjList[i].gameObject, false);
			}
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00048D98 File Offset: 0x00046F98
	private void Update()
	{
		this.UpdatePlayerPos();
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00048DA0 File Offset: 0x00046FA0
	public void SetTarget(Vector3 pos)
	{
		Vector2 target;
		target..ctor((pos.x + this.mMapLength / 2f) / this.mMapLength, (pos.z + this.mMapHeight / 2f) / this.mMapHeight);
		this.SetTarget(target);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00048DF4 File Offset: 0x00046FF4
	public void SetTarget(Vector2 pos)
	{
		if (!UnityVersionUtil.IsActive(this.TargetPic.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.TargetPic.gameObject, true);
		}
		this.TargetPos = pos;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00048E24 File Offset: 0x00047024
	public void HideTarget()
	{
		UnityVersionUtil.SetActiveRecursive(this.TargetPic.gameObject, false);
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00048E38 File Offset: 0x00047038
	private void UpdateTargetPos()
	{
		if (!UnityVersionUtil.IsActive(this.TargetPic.gameObject))
		{
			return;
		}
		Vector2 vector = this.TargetPos - this.playerPos;
		if (vector.sqrMagnitude > this.mRadius * this.mRadius)
		{
			this.TargetPic.transform.localPosition = vector.normalized * this.MapSize / 2f;
		}
		else
		{
			this.TargetPic.transform.localPosition = vector / this.mRadius * this.MapSize / 2f;
		}
		this.TargetPic.transform.localPosition -= Vector3.forward * 5f;
		this.TargetPic.transform.localEulerAngles = new Vector3(0f, 0f, -this.mCamTrans.eulerAngles.y);
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00048F50 File Offset: 0x00047150
	public void UpdatePlayerPos()
	{
		this.playerPos = new Vector2((this.mPlayerTransform.position.x + this.mMapLength / 2f) / this.mMapLength, (this.mPlayerTransform.position.z + this.mMapHeight / 2f) / this.mMapHeight);
		this.SetMapCenterPos(this.playerPos);
		if (this.mMainPlayer.IsLocalDrivingCar)
		{
			this.PlayerPic.localEulerAngles = new Vector3(0f, 0f, -this.mMainPlayer.CurPlayerCar.transform.eulerAngles.y);
		}
		else
		{
			this.PlayerPic.localEulerAngles = new Vector3(0f, 0f, -this.mPlayerTransform.eulerAngles.y);
		}
		this.MapPicObj.transform.localEulerAngles = new Vector3(0f, 0f, this.mCamTrans.eulerAngles.y);
		this.UpdateTargetPos();
		this.UpdateNpcPos();
		this.UpdateActionMapObjPos();
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00049084 File Offset: 0x00047284
	public void SetMapUnlockAlph(float unlockAlph)
	{
		this.mMapMat.SetFloat("_LockShowRange", unlockAlph);
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00049098 File Offset: 0x00047298
	public void SetMapCenterPos(Vector2 pos)
	{
		Vector2 vector;
		vector..ctor(pos.x - this.mMapShowRange, pos.y - this.mMapShowRange * this.mMapWHRatio);
		Vector2 vector2 = Vector2.zero;
		for (int i = 0; i < this.mMapUV.Length; i++)
		{
			vector2 = this.mMapUV[i];
			vector2.x = vector2.x * this.mMapShowRange * 2f + vector.x;
			vector2.y = vector2.y * this.mMapShowRange * 2f * this.mMapWHRatio + vector.y;
			this.mTempUV[i] = vector2;
		}
		this.mMapMesh.uv = this.mTempUV;
		this.mMapMat.SetFloat("_CenterX", pos.x);
		this.mMapMat.SetFloat("_CenterY", pos.y);
		this.mMapMat.SetFloat("_PoliceCenterX", pos.x);
		this.mMapMat.SetFloat("_PoliceCenterY", pos.y);
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x000491CC File Offset: 0x000473CC
	public void SetPoliceAlph(float alph)
	{
		this.mMapMat.SetColor("_PoliceColor", new Color(1f, 0f, 0f, alph));
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x000491F4 File Offset: 0x000473F4
	private void GenerateMesh()
	{
		this.mMapMesh.Clear();
		Vector3[] array = new Vector3[4];
		int[] array2 = new int[6];
		Vector3[] array3 = new Vector3[4];
		this.mMapUV = new Vector2[4];
		float num = this.MapSize / 2f;
		array[0] = new Vector3(base.transform.position.x - num, base.transform.position.y - num, 0f);
		array[1] = new Vector3(base.transform.position.x + num, base.transform.position.y - num, 0f);
		array[2] = new Vector3(base.transform.position.x + num, base.transform.position.y + num, 0f);
		array[3] = new Vector3(base.transform.position.x - num, base.transform.position.y + num, 0f);
		this.mMapMesh.vertices = array;
		array2[0] = 0;
		array2[1] = 2;
		array2[2] = 1;
		array2[3] = 0;
		array2[4] = 3;
		array2[5] = 2;
		this.mMapMesh.triangles = array2;
		array3[0] = -Vector3.forward;
		array3[1] = -Vector3.forward;
		array3[2] = -Vector3.forward;
		array3[3] = -Vector3.forward;
		this.mMapMesh.normals = array3;
		this.mMapUV[0] = new Vector2(0f, 0f);
		this.mMapUV[1] = new Vector2(1f, 0f);
		this.mMapUV[2] = new Vector2(1f, 1f);
		this.mMapUV[3] = new Vector2(0f, 1f);
		this.mMapMesh.uv = this.mMapUV;
		this.mTempUV = new Vector2[this.mMapUV.Length];
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0004947C File Offset: 0x0004767C
	public Vector2 GetLineCirclePos(Vector2 pos1, Vector2 pos2, float radius_2)
	{
		float num = (pos2.y - pos1.y) / (pos2.x - pos1.x);
		float num2 = pos1.y - num * pos1.x;
		float num3 = num * num;
		float num4 = num2 * num2;
		float num5 = Mathf.Sqrt(num3 * num4 - (1f + num3) * (num4 - radius_2));
		float num6 = (-num * num2 + num5) / (1f + num3);
		float num7;
		if ((num6 - pos1.x) * (num6 - pos2.x) < 0f)
		{
			num7 = num6;
		}
		else
		{
			num7 = (-num * num2 - num5) / (1f + num3);
		}
		float num8 = num * num7 + num2;
		return new Vector2(num7, num8);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00049540 File Offset: 0x00047740
	public void DrawMapLine(List<Vector3> posList, Vector3 targetPos)
	{
		List<Vector3> list = new List<Vector3>();
		float num = this.MapSize / 2f - 5f;
		num *= num;
		bool flag = true;
		for (int i = 0; i < posList.Count; i++)
		{
			Vector2 vector = this.WorldPos2MapPos(posList[i]);
			if (vector.sqrMagnitude < num)
			{
				list.Add(vector);
			}
			else if (list.Count > 0 && flag)
			{
				flag = false;
				list.Add(this.GetLineCirclePos(list[list.Count - 1], vector, num));
			}
		}
		this.PathLineRender.SetVertexCount(list.Count);
		for (int j = 0; j < list.Count; j++)
		{
			this.PathLineRender.SetPosition(j, list[j]);
		}
		this.WalkLineRender1.SetVertexCount(0);
		this.WalkLineRender2.SetVertexCount(0);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00049648 File Offset: 0x00047848
	public void ClearMapLine()
	{
		this.PathLineRender.SetVertexCount(0);
		this.WalkLineRender1.SetVertexCount(0);
		this.WalkLineRender2.SetVertexCount(0);
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0004967C File Offset: 0x0004787C
	public void OnlyDrawWalkLine(Vector3 playerPos, Vector3 targetPos)
	{
		this.WalkLineRender1.SetVertexCount(0);
		this.WalkLineRender2.SetVertexCount(0);
		this.PathLineRender.SetVertexCount(2);
		this.PathLineRender.SetPosition(0, this.WorldPos2MapPos(playerPos));
		this.PathLineRender.SetPosition(1, this.WorldPos2MapPos(targetPos));
	}

	// Token: 0x040008B6 RID: 2230
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040008B7 RID: 2231
	public UILabel MapNameLabel;

	// Token: 0x040008B8 RID: 2232
	public GameObject MapPicObj;

	// Token: 0x040008B9 RID: 2233
	public GameObject MapNameObj;

	// Token: 0x040008BA RID: 2234
	private float MapSize = 130f;

	// Token: 0x040008BB RID: 2235
	private float mMapLength;

	// Token: 0x040008BC RID: 2236
	private float mMapHeight;

	// Token: 0x040008BD RID: 2237
	private float mMapWHRatio;

	// Token: 0x040008BE RID: 2238
	private float mRadius;

	// Token: 0x040008BF RID: 2239
	private float mMapShowRange;

	// Token: 0x040008C0 RID: 2240
	private Mesh mMapMesh;

	// Token: 0x040008C1 RID: 2241
	private Material mMapMat;

	// Token: 0x040008C2 RID: 2242
	private Vector2[] mTempUV;

	// Token: 0x040008C3 RID: 2243
	private Vector2[] mMapUV;

	// Token: 0x040008C4 RID: 2244
	public Transform PlayerPic;

	// Token: 0x040008C5 RID: 2245
	public Transform TargetPic;

	// Token: 0x040008C6 RID: 2246
	public Vector2 TargetPos;

	// Token: 0x040008C7 RID: 2247
	public Vector2 playerPos;

	// Token: 0x040008C8 RID: 2248
	private Transform mPlayerTransform;

	// Token: 0x040008C9 RID: 2249
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040008CA RID: 2250
	private Transform mCamTrans;

	// Token: 0x040008CB RID: 2251
	public List<UISprite> NpcPointPicList = new List<UISprite>();

	// Token: 0x040008CC RID: 2252
	public List<NewMiniMapActivityObj> ActionMapObjList = new List<NewMiniMapActivityObj>();

	// Token: 0x040008CD RID: 2253
	private List<ActivityMapData> curActivityMapDataList;

	// Token: 0x040008CE RID: 2254
	private List<string> curShowMissionList;

	// Token: 0x040008CF RID: 2255
	private List<ActivityObjData> curMapObjDataList = new List<ActivityObjData>();

	// Token: 0x040008D0 RID: 2256
	public UILabel OnlineLabel;

	// Token: 0x040008D1 RID: 2257
	public UISprite ClickTipsSprite;

	// Token: 0x040008D2 RID: 2258
	public TweenColor PoliceTweenColor;

	// Token: 0x040008D3 RID: 2259
	public LineRenderer PathLineRender;

	// Token: 0x040008D4 RID: 2260
	public LineRenderer WalkLineRender1;

	// Token: 0x040008D5 RID: 2261
	public LineRenderer WalkLineRender2;

	// Token: 0x040008D6 RID: 2262
	private string curMapName = string.Empty;

	// Token: 0x040008D7 RID: 2263
	private string curMapId = string.Empty;

	// Token: 0x040008D8 RID: 2264
	public UIWidget TutorialClickSp;
}
