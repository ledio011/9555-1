using System;
using System.Collections.Generic;
using System.Text;
using SprotoType;
using UnityEngine;

// Token: 0x02000A2D RID: 2605
public class NewMapUIRootLogic : SingletonUnity<NewMapUIRootLogic>
{
	// Token: 0x17000FCB RID: 4043
	// (get) Token: 0x06004BDD RID: 19421 RVA: 0x00198648 File Offset: 0x00196848
	public MapInfoData CurMapInfo
	{
		get
		{
			return this.curMapInfo;
		}
	}

	// Token: 0x06004BDE RID: 19422 RVA: 0x00198650 File Offset: 0x00196850
	protected override void Awake()
	{
		base.Awake();
		this.Player = Singleton<ObjManager>.Instance.MainPlayer;
		this.curSceneTelePortInfo.Clear();
		this.curSceneMonsterData.Clear();
		this.curSceneNPCData.Clear();
		this.curActivityMapData.Clear();
		for (int i = 0; i < this.MonsterPointPic.Count; i++)
		{
			this.MonsterPointPic[i].enabled = false;
		}
		for (int j = 0; j < this.TeleportPointPic.Count; j++)
		{
			this.TeleportPointPic[j].enabled = false;
		}
		for (int k = 0; k < this.SurviveTelPic.Count; k++)
		{
			this.SurviveTelPic[k].enabled = false;
		}
		for (int l = 0; l < this.NPCPointPic.Count; l++)
		{
			this.NPCPointPic[l].enabled = false;
		}
		for (int m = 0; m < this.NPC2PointPic.Count; m++)
		{
			this.NPC2PointPic[m].enabled = false;
		}
		for (int n = 0; n < this.NPC3PointPic.Count; n++)
		{
			this.NPC3PointPic[n].enabled = false;
		}
		for (int num = 0; num < this.NpcPointPicList.Count; num++)
		{
			this.NpcPointPicList[num].enabled = false;
		}
		this.MoveTargetPic.enabled = false;
	}

	// Token: 0x06004BDF RID: 19423 RVA: 0x001987FC File Offset: 0x001969FC
	public void InitTexture()
	{
		List<string> list = new List<string>();
		if (this.LocalMapTexture.mainTexture == null || !this.LocalMapTexture.mainTexture.name.Equals(this.curMapInfo.MiniMapName))
		{
			list.Add(this.curMapInfo.MiniMapName);
		}
		if (list.Count == 0)
		{
			this.UpdateMapLockIcon();
			return;
		}
		this.mLoadingMapFlag = true;
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004BE0 RID: 19424 RVA: 0x001988A0 File Offset: 0x00196AA0
	public void UpdateMapLockIcon()
	{
		Material material = this.LocalMapTexture.material;
		for (int i = 0; i < this.MapLockLabel.Count; i++)
		{
			NGUITools.SetActive(this.MapLockLabel[i].gameObject, false);
		}
		material.SetTexture("_MapLockTex", null);
		material.SetFloat("_LockShowRange", 1.1f);
	}

	// Token: 0x06004BE1 RID: 19425 RVA: 0x00198908 File Offset: 0x00196B08
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		this.mLoadingMapFlag = false;
		if (retdic.Count == 0)
		{
			return;
		}
		if (retdic.ContainsKey(this.curMapInfo.MiniMapName))
		{
			Texture texture = retdic[this.curMapInfo.MiniMapName];
			if (texture != null)
			{
				Material material = this.LocalMapTexture.material;
				material.SetTexture("_MainTex", texture);
				this.UpdateMapLockIcon();
				this.LocalMapTexture.mainTexture = texture;
				if (texture.width > texture.height)
				{
					if ((float)texture.width > this.MaxMapWidth)
					{
						this.LocalMapTexture.width = (int)this.MaxMapWidth;
						this.LocalMapTexture.height = (int)((float)texture.height * (this.MaxMapWidth / (float)texture.width));
					}
					else
					{
						this.LocalMapTexture.width = texture.width;
						this.LocalMapTexture.height = texture.height;
					}
				}
				else if ((float)texture.height > this.MaxMapHeight)
				{
					this.LocalMapTexture.width = (int)((float)texture.width * (this.MaxMapHeight / (float)texture.height));
					this.LocalMapTexture.height = (int)this.MaxMapHeight;
				}
				else
				{
					this.LocalMapTexture.width = texture.width;
					this.LocalMapTexture.height = texture.height;
				}
				this.LocalMapTexture.SetDirty();
			}
			else
			{
				Debug.LogWarning("no mini map1 :" + this.curMapInfo.MiniMapName);
			}
		}
		else
		{
			Debug.LogWarning("no mini map2 :" + this.curMapInfo.MiniMapName);
		}
		this.InitTeleportPointPic();
		this.InitMapActivityPic();
		this.InitMissionActivityPic();
		this.UpdateMissionPointPic();
		this.InitLeftBtn();
		this.InitActivityBossPointPic();
		this.UpdateMoveTargetPic();
		this.UpdateNpcPos();
		if (this.mFindTargetFlag)
		{
			if (this.mTargetType != -99)
			{
				this.ChooseActivityObj(this.mTargetType, this.mTargetSubtype, this.mTargetActId);
			}
			else
			{
				this.ChooseActivityObj(this.mTargetActId, this.mTargetIsMission);
			}
		}
	}

	// Token: 0x06004BE2 RID: 19426 RVA: 0x00198B38 File Offset: 0x00196D38
	public void OnClickChangeLineBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MapLineInfoLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<MapLineInfoLogic>.Instance.PreReset();
			NetLogic.GetInstance().Send<Protocol.request_line_state>(null, null);
		}, null);
	}

	// Token: 0x06004BE3 RID: 19427 RVA: 0x00198B68 File Offset: 0x00196D68
	public void OnClickWorldMapBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.WorldMapRoot, delegate
			{
				WaitResponseUIRootLogic.OpenWaitBox(319, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_guild_map_info>(null, null);
				SingletonUnity<WorldMapRoot>.Instance.EnableReset(false);
			}, null);
			return;
		}
		if (SingletonUnity<DownloadTipRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DownloadTipRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
			SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
			SingletonUnity<DownloadTipRootLogic>.Instance.OnClickDownloadTipBtn();
		}
	}

	// Token: 0x06004BE4 RID: 19428 RVA: 0x00198BFC File Offset: 0x00196DFC
	private void InitLeftBtn()
	{
		int curLineIndex = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex;
		int lineCount = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.LineCount;
		this.OnLineINfoLabel.text = string.Format("line{0}", curLineIndex);
	}

	// Token: 0x06004BE5 RID: 19429 RVA: 0x00198C44 File Offset: 0x00196E44
	public void Reset(string mapId)
	{
		if (this.curMapInfo != null && mapId.Equals(this.curMapInfo.ID))
		{
			this.UpdateMapLockIcon();
			return;
		}
		this.curMapInfo = DataManager.GetMapInfoDataByID(mapId);
		if (this.curMapInfo == null)
		{
			Debug.Log("No Map Data!!!!!!!!!!!!!!!!  " + mapId);
			return;
		}
		this.EnableReset();
		this.MapNameLabel.text = StrDictionary.GetDictionaryString(this.curMapInfo.Name, new object[0]);
		this.curActivityMapData = DataManager.GetAcitvityMapDataByMapId(mapId);
		guild_map_info guildMapInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.GetGuildMapInfo(mapId);
		if (guildMapInfo != null && guildMapInfo.HasGuildName)
		{
			this.MapcapturLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{106040}", new object[0]), guildMapInfo.guildName);
		}
		else
		{
			this.MapcapturLabel.text = string.Empty;
		}
		this.InitTexture();
		if (!this.mLoadingMapFlag)
		{
			this.InitMapActivityPic();
			this.InitMissionActivityPic();
			this.UpdateMissionPointPic();
			this.InitLeftBtn();
			this.InitActivityBossPointPic();
			this.UpdateMoveTargetPic();
			this.UpdateMapLockIcon();
			this.UpdateNpcPos();
		}
	}

	// Token: 0x06004BE6 RID: 19430 RVA: 0x00198D7C File Offset: 0x00196F7C
	public void EnableReset()
	{
		this.mLoadingMapFlag = false;
		for (int i = 0; i < this.MapActivityObjList.Count; i++)
		{
			NGUITools.SetActive(this.MapActivityObjList[i].gameObject, false);
		}
		for (int j = 0; j < this.MissionActivityObjList.Count; j++)
		{
			NGUITools.SetActive(this.MissionActivityObjList[j].gameObject, false);
		}
		for (int k = 0; k < this.MapLockLabel.Count; k++)
		{
			NGUITools.SetActive(this.MapLockLabel[k].gameObject, false);
		}
		this.MoveTargetPic.enabled = false;
	}

	// Token: 0x06004BE7 RID: 19431 RVA: 0x00198E38 File Offset: 0x00197038
	public void InitMapActivityPic()
	{
		this.curVisibleActivityMapData.Clear();
		for (int i = 0; i < this.curActivityMapData.Count; i++)
		{
			if (this.curActivityMapData[i].IsVisible)
			{
				this.curVisibleActivityMapData.Add(this.curActivityMapData[i]);
			}
		}
		int num = this.curVisibleActivityMapData.Count - this.MapActivityObjList.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.MapActivityObjList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.MapActivityObjList[0].transform.parent;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				this.MapActivityObjList.Add(gameObject.GetComponent<NewMapActivityObj>());
			}
		}
		for (int k = 0; k < this.MapActivityObjList.Count; k++)
		{
			if (k < this.curVisibleActivityMapData.Count)
			{
				NGUITools.SetActive(this.MapActivityObjList[k].gameObject, true);
				this.MapActivityObjList[k].Reset(this.curVisibleActivityMapData[k]);
				this.MapActivityObjList[k].transform.localPosition = this.WorldPos2MapPos(this.curVisibleActivityMapData[k].Position);
			}
			else
			{
				NGUITools.SetActive(this.MapActivityObjList[k].gameObject, false);
			}
		}
	}

	// Token: 0x06004BE8 RID: 19432 RVA: 0x00199000 File Offset: 0x00197200
	public void InitMissionActivityPic()
	{
		this.curAcceptableMissionDataList.Clear();
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		this.curMapAcceptMissionDataList = DataManager.GetAcceptMissionDataByMapId(this.curMapInfo.ID);
		Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
		if (this.curMapAcceptMissionDataList != null)
		{
			for (int i = 0; i < this.curMapAcceptMissionDataList.Count; i++)
			{
				if (this.curMapAcceptMissionDataList[i].Class != 4 && this.curMapAcceptMissionDataList[i].Class != 5)
				{
					if (missionManager.IsMissionAcceptable(this.curMapAcceptMissionDataList[i].ID) && !string.IsNullOrEmpty(this.curMapAcceptMissionDataList[i].Accept))
					{
						if (dictionary.ContainsKey(this.curMapAcceptMissionDataList[i].Accept))
						{
							dictionary[this.curMapAcceptMissionDataList[i].Accept].Add(this.curMapAcceptMissionDataList[i].ID);
						}
						else
						{
							dictionary.Add(this.curMapAcceptMissionDataList[i].Accept, new List<string>());
							dictionary[this.curMapAcceptMissionDataList[i].Accept].Add(this.curMapAcceptMissionDataList[i].ID);
							this.curAcceptableMissionDataList.Add(this.curMapAcceptMissionDataList[i]);
						}
					}
				}
			}
		}
		this.curAcceptedMissionDataList.Clear();
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
				goto IL_23C;
			}
			if (missionState == MISSION_STATE.COMPLETE)
			{
				text2 = missionDataByID.SubmitMapId;
				text = missionDataByID.Submit;
				goto IL_23C;
			}
			IL_2CD:
			j++;
			continue;
			IL_23C:
			if (string.IsNullOrEmpty(text2))
			{
				goto IL_2CD;
			}
			if (!text2.Equals(this.curMapInfo.ID))
			{
				goto IL_2CD;
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (dictionary.ContainsKey(text))
				{
					dictionary[text].Add(allMissionId[j]);
					goto IL_2CD;
				}
				dictionary.Add(text, new List<string>());
				dictionary[text].Add(allMissionId[j]);
			}
			this.curAcceptedMissionDataList.Add(missionDataByID);
			goto IL_2CD;
		}
		int num = this.curAcceptableMissionDataList.Count + this.curAcceptedMissionDataList.Count - this.MissionActivityObjList.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.MissionActivityObjList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.MissionActivityObjList[0].transform.parent;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				this.MissionActivityObjList.Add(gameObject.GetComponent<NewMapActivityObj>());
			}
		}
		int num2 = this.curAcceptedMissionDataList.Count + this.curAcceptableMissionDataList.Count;
		for (int l = 0; l < this.MissionActivityObjList.Count; l++)
		{
			if (l < num2)
			{
				if (l < this.curAcceptableMissionDataList.Count)
				{
					if (!string.IsNullOrEmpty(this.curAcceptableMissionDataList[l].Accept))
					{
						NGUITools.SetActive(this.MissionActivityObjList[l].gameObject, true);
						if (dictionary.ContainsKey(this.curAcceptableMissionDataList[l].Accept))
						{
							this.MissionActivityObjList[l].Reset(this.curAcceptableMissionDataList[l], dictionary[this.curAcceptableMissionDataList[l].Accept]);
						}
						else
						{
							this.MissionActivityObjList[l].Reset(this.curAcceptableMissionDataList[l], null);
						}
						Vector3 pos = DataManager.GetNPCPosInMonsterData(this.curMapInfo.ID, this.curAcceptableMissionDataList[l].Accept);
						if (this.curAcceptableMissionDataList[l].Class == 1 && this.curAcceptableMissionDataList[l].MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
						{
							DominData dominDataByID = DataManager.GetDominDataByID(this.curAcceptableMissionDataList[l].LogicID);
							if (dominDataByID != null)
							{
								pos = dominDataByID.GetPos();
							}
						}
						this.MissionActivityObjList[l].transform.localPosition = this.WorldPos2MapPos(pos);
					}
				}
				else
				{
					int num3 = l - this.curAcceptableMissionDataList.Count;
					NGUITools.SetActive(this.MissionActivityObjList[l].gameObject, true);
					MISSION_STATE missionState = missionManager.GetMissionState(this.curAcceptedMissionDataList[num3].ID);
					Vector3 pos2 = Vector3.zero;
					if (missionState == MISSION_STATE.ACCEPTED)
					{
						if (this.curAcceptedMissionDataList[num3].MissionLogicType == MISSION_LOGICTYPE.SURVEY)
						{
							SurveyMissionData surveyMissionDataById2 = DataManager.GetSurveyMissionDataById(this.curAcceptedMissionDataList[num3].LogicID);
							pos2..ctor(surveyMissionDataById2.PosX, surveyMissionDataById2.PosY, surveyMissionDataById2.PosZ);
							this.MissionActivityObjList[l].Reset(this.curAcceptedMissionDataList[num3], null);
						}
						else
						{
							pos2 = DataManager.GetNPCPosInMonsterData(this.curMapInfo.ID, this.curAcceptedMissionDataList[num3].Target);
							if (dictionary.ContainsKey(this.curAcceptedMissionDataList[num3].Target))
							{
								this.MissionActivityObjList[l].Reset(this.curAcceptedMissionDataList[num3], dictionary[this.curAcceptedMissionDataList[num3].Target]);
							}
							else
							{
								this.MissionActivityObjList[l].Reset(this.curAcceptedMissionDataList[num3], null);
							}
						}
					}
					else
					{
						pos2 = DataManager.GetNPCPosInMonsterData(this.curMapInfo.ID, this.curAcceptedMissionDataList[num3].Submit);
						if (dictionary.ContainsKey(this.curAcceptedMissionDataList[num3].Submit))
						{
							this.MissionActivityObjList[l].Reset(this.curAcceptedMissionDataList[num3], dictionary[this.curAcceptedMissionDataList[num3].Submit]);
						}
						else
						{
							this.MissionActivityObjList[l].Reset(this.curAcceptedMissionDataList[num3], null);
						}
					}
					if (this.curAcceptedMissionDataList[num3].Class == 1 && this.curAcceptedMissionDataList[num3].MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
					{
						DominData dominDataByID2 = DataManager.GetDominDataByID(this.curAcceptedMissionDataList[num3].LogicID);
						if (dominDataByID2 != null)
						{
							pos2 = dominDataByID2.GetPos();
						}
					}
					this.MissionActivityObjList[l].transform.localPosition = this.WorldPos2MapPos(pos2);
				}
			}
			else
			{
				NGUITools.SetActive(this.MissionActivityObjList[l].gameObject, false);
			}
		}
	}

	// Token: 0x06004BE9 RID: 19433 RVA: 0x001997FC File Offset: 0x001979FC
	private void UpdateMissionPointPic()
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		MapInfoData currentMapInofData = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		int curLineIndex = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex;
		CurMission escortMission = missionManager.GetEscortMission();
		string empty = string.Empty;
		if (escortMission != null)
		{
			Vector3 escortNpcPos = missionManager.GetEscortNpcPos(out empty);
			int num = (int)escortMission.GetParam(2);
			if (currentMapInofData.ID.Equals(empty) && curLineIndex == num)
			{
				this.NPCPointPic[0].enabled = true;
				this.NPCPointPic[0].transform.localPosition = this.WorldPos2MapPos(escortNpcPos);
			}
			else
			{
				this.NPCPointPic[0].enabled = false;
			}
		}
		else
		{
			this.NPCPointPic[0].enabled = false;
		}
		List<ObjNPC> otherPlayerEscortNPCList = Singleton<ObjManager>.Instance.OtherPlayerEscortNPCList;
		if (otherPlayerEscortNPCList.Count > 0)
		{
			int num2 = otherPlayerEscortNPCList.Count - this.NPC2PointPic.Count;
			if (num2 > 0)
			{
				GameObject gameObject = Object.Instantiate(this.NPC2PointPic[0].gameObject) as GameObject;
				gameObject.transform.parent = this.NPC2PointPic[0].transform.parent;
				gameObject.transform.localScale = Vector3.one;
				this.NPC2PointPic.Add(gameObject.GetComponent<UISprite>());
			}
		}
		for (int i = 0; i < this.NPC2PointPic.Count; i++)
		{
			this.NPC2PointPic[i].enabled = (i < otherPlayerEscortNPCList.Count);
			if (i < otherPlayerEscortNPCList.Count)
			{
				this.NPC2PointPic[i].transform.localPosition = this.WorldPos2MapPos(otherPlayerEscortNPCList[i].Position);
			}
		}
	}

	// Token: 0x06004BEA RID: 19434 RVA: 0x001999E4 File Offset: 0x00197BE4
	private void InitMonsterPointPic()
	{
		this.curSceneMonsterData.Clear();
		this.curSceneNPCData.Clear();
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		List<int> monsterGroupList = sceneManager.GetMonsterGroupList();
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		for (int i = 0; i < monsterGroupList.Count; i++)
		{
			List<MonsterData> monsterDataByGroup = sceneManager.GetMonsterDataByGroup(monsterGroupList[i]);
			if (monsterGroupList[i] != GameDefine.MISSION_NPC_GROUP_VAL && !sceneManager.IsSurviveBattleScene())
			{
				Vector3 vector = Vector3.zero;
				for (int j = 0; j < monsterDataByGroup.Count; j++)
				{
					vector += monsterDataByGroup[j].GetNpcXZPos();
				}
				NpcData npcDataByID = DataManager.GetNpcDataByID(monsterDataByGroup[0].NpcID);
				vector /= (float)monsterDataByGroup.Count;
				if (npcDataByID.Type == 2)
				{
					list.Add(monsterDataByGroup[0].GetNpcXZPos());
				}
				else if (string.IsNullOrEmpty(monsterDataByGroup[0].PathId))
				{
					list2.Add(monsterDataByGroup[0].GetNpcXZPos());
				}
				if (npcDataByID.FunctionType != 2 && npcDataByID.FunctionType != 3)
				{
					MapPoint mapPoint = new MapPoint();
					if (string.IsNullOrEmpty(monsterDataByGroup[0].PathId))
					{
						mapPoint.PointName = npcDataByID.MName;
					}
					else
					{
						mapPoint.PointName = "[Patrol] " + npcDataByID.MName;
					}
					mapPoint.PointPos = vector;
					mapPoint.PointType = MAP_POINT_TYPE.MONSTER;
					mapPoint.NpcId = monsterDataByGroup[0].NpcID;
					this.curSceneMonsterData.Add(mapPoint);
				}
			}
			else
			{
				for (int k = 0; k < monsterDataByGroup.Count; k++)
				{
					NpcData npcDataByID2 = DataManager.GetNpcDataByID(monsterDataByGroup[k].NpcID);
					if (npcDataByID2.Group != 6 && npcDataByID2.FunctionType != 2 && npcDataByID2.FunctionType != 3)
					{
						MapPoint mapPoint2 = new MapPoint();
						mapPoint2.PointName = DataManager.GetNpcDataByID(monsterDataByGroup[k].NpcID).MName;
						mapPoint2.PointPos = monsterDataByGroup[k].GetNpcXZPos();
						mapPoint2.NpcId = monsterDataByGroup[k].NpcID;
						if (sceneManager.IsSurviveBattleScene())
						{
							NpcData npcDataByID3 = DataManager.GetNpcDataByID(monsterDataByGroup[0].NpcID);
							if (npcDataByID3.Type == 2)
							{
								list.Add(monsterDataByGroup[k].GetNpcXZPos());
							}
							else
							{
								list2.Add(monsterDataByGroup[k].GetNpcXZPos());
							}
							mapPoint2.PointType = MAP_POINT_TYPE.MONSTER;
							this.curSceneMonsterData.Add(mapPoint2);
						}
						else
						{
							mapPoint2.PointType = MAP_POINT_TYPE.NPC;
							this.curSceneNPCData.Add(mapPoint2);
						}
					}
				}
			}
		}
		int num = list2.Count - this.NPC3PointPic.Count;
		if (num > 0)
		{
			for (int l = 0; l < num; l++)
			{
				GameObject gameObject = Object.Instantiate(this.NPC3PointPic[0].gameObject) as GameObject;
				gameObject.transform.parent = this.NPC3PointPic[0].transform.parent;
				this.NPC3PointPic.Add(gameObject.GetComponent<UISprite>());
			}
		}
		for (int m = 0; m < this.NPC3PointPic.Count; m++)
		{
			this.NPC3PointPic[m].enabled = (m < list2.Count);
		}
		for (int n = 0; n < list2.Count; n++)
		{
			this.NPC3PointPic[n].transform.localPosition = this.WorldPos2MapPos(list2[n]);
		}
	}

	// Token: 0x06004BEB RID: 19435 RVA: 0x00199DE8 File Offset: 0x00197FE8
	public void UpdateMoveTargetPic()
	{
		if (this.curMapInfo.ID.Equals(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID) && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsHaveMoveTarget)
		{
			this.MoveTargetPic.transform.localPosition = this.WorldPos2MapPos(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurMoveTarget);
			this.MoveTargetPic.enabled = true;
		}
		else
		{
			this.MoveTargetPic.enabled = false;
		}
	}

	// Token: 0x06004BEC RID: 19436 RVA: 0x00199E74 File Offset: 0x00198074
	private void InitActivityBossPointPic()
	{
		List<Vector3> list = new List<Vector3>();
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		List<TimerActivityData> enableTimerActivityList = DataManager.GetEnableTimerActivityList();
		List<ActivityBossData> list2 = new List<ActivityBossData>();
		if (enableTimerActivityList != null)
		{
			for (int i = 0; i < enableTimerActivityList.Count; i++)
			{
				ActivityBossData activityBossDataByActivityIdMapId = DataManager.GetActivityBossDataByActivityIdMapId(enableTimerActivityList[i].ID, this.CurMapInfo.ID);
				if (activityBossDataByActivityIdMapId != null)
				{
					list2.Add(activityBossDataByActivityIdMapId);
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				for (int k = 0; k < list2[j].NpcPosList.Count; k++)
				{
					list.Add(list2[j].NpcPosList[k]);
				}
			}
		}
		int num = list.Count - this.MonsterPointPic.Count;
		if (num > 0)
		{
			for (int l = 0; l < num; l++)
			{
				GameObject gameObject = Object.Instantiate(this.MonsterPointPic[0].gameObject) as GameObject;
				gameObject.transform.parent = this.MonsterPointPic[0].transform.parent;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				this.MonsterPointPic.Add(gameObject.GetComponent<UISprite>());
			}
		}
		for (int m = 0; m < this.MonsterPointPic.Count; m++)
		{
			this.MonsterPointPic[m].enabled = (m < list.Count);
		}
		for (int n = 0; n < list.Count; n++)
		{
			this.MonsterPointPic[n].transform.localPosition = this.WorldPos2MapPos(list[n]);
		}
	}

	// Token: 0x06004BED RID: 19437 RVA: 0x0019A070 File Offset: 0x00198270
	private void InitTeleportPointPic()
	{
		this.TeleportPointPic[0].enabled = false;
		this.SurviveTelPic[0].alpha = 0f;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsCopyShowMap() && !sceneManager.IsTutorialScene())
		{
			if (sceneManager.IsSurviveBattleScene())
			{
				this.SurviveTelPic[0].alpha = 1f;
				this.curSceneTelePortInfo.Clear();
				MapInfoData currentMapInofData = sceneManager.CurrentMapInofData;
				int num = currentMapInofData.TeleportPosList.Count - this.SurviveTelPic.Count;
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.SurviveTelPic[0].gameObject) as GameObject;
					UISprite component = gameObject.GetComponent<UISprite>();
					gameObject.transform.parent = this.SurviveTelPic[0].transform.parent;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localPosition = Vector3.zero;
					this.SurviveTelPic.Add(component);
				}
				for (int j = 0; j < currentMapInofData.TeleportPosList.Count; j++)
				{
					MapPoint mapPoint = new MapPoint();
					if (GameManager.IsSupportCurDataVersion56())
					{
						mapPoint.PointName = StrDictionary.GetDictionaryString("#{101812}", new object[0]);
					}
					else
					{
						mapPoint.PointName = StrDictionary.GetDictionaryString("#{101809}", new object[0]);
					}
					mapPoint.PointPos = currentMapInofData.TeleportPosList[j];
					mapPoint.PointType = MAP_POINT_TYPE.TELEPORT;
					mapPoint.NpcId = string.Empty;
					this.curSceneTelePortInfo.Add(mapPoint);
					this.SurviveTelPic[j].transform.localPosition = this.WorldPos2MapPos(mapPoint.PointPos);
				}
			}
			else
			{
				this.SurviveTelPic[0].alpha = 0f;
			}
			return;
		}
		this.TeleportPointPic[0].enabled = true;
		this.curSceneTelePortInfo.Clear();
		MapInfoData mapInfoData = this.curMapInfo;
		MapPoint mapPoint2 = new MapPoint();
		mapPoint2.PointName = StrDictionary.GetDictionaryString("#{101809}", new object[0]);
		mapPoint2.PointPos = mapInfoData.TelePortPosVector3;
		mapPoint2.PointType = MAP_POINT_TYPE.TELEPORT;
		mapPoint2.NpcId = string.Empty;
		this.curSceneTelePortInfo.Add(mapPoint2);
		this.TeleportPointPic[0].transform.localPosition = this.WorldPos2MapPos(mapInfoData.TelePortPosVector3);
	}

	// Token: 0x06004BEE RID: 19438 RVA: 0x0019A308 File Offset: 0x00198508
	private void Update()
	{
		this.intervalCount += Time.deltaTime;
		if (this.intervalCount > this.FlashInterval)
		{
			this.intervalCount = 0f;
			this.FlashPlayerPos();
		}
	}

	// Token: 0x06004BEF RID: 19439 RVA: 0x0019A34C File Offset: 0x0019854C
	private void FlashPlayerPos()
	{
		if (this.curMapInfo == null)
		{
			return;
		}
		if (!this.curMapInfo.ID.Equals(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID))
		{
			if (UnityVersionUtil.IsActive(this.LocalMapPlayerPic.gameObject))
			{
				NGUITools.SetActive(this.LocalMapPlayerPic.gameObject, false);
			}
			return;
		}
		if (!UnityVersionUtil.IsActive(this.LocalMapPlayerPic.gameObject))
		{
			NGUITools.SetActive(this.LocalMapPlayerPic.gameObject, true);
		}
		this.LocalMapPlayerPic.localPosition = this.WorldPos2MapPos(this.Player.Position);
		if (this.Player.IsLocalDrivingCar)
		{
			this.LocalMapPlayerPic.localEulerAngles = new Vector3(0f, 0f, -this.Player.CurPlayerCar.transform.localEulerAngles.y);
		}
		else
		{
			this.LocalMapPlayerPic.localEulerAngles = new Vector3(0f, 0f, -this.Player.transform.localEulerAngles.y);
		}
		this.mTempStr.Length = 0;
		this.UpdateMissionPointPic();
	}

	// Token: 0x06004BF0 RID: 19440 RVA: 0x0019A48C File Offset: 0x0019868C
	private Vector3 WorldPos2MapPos(Vector3 pos)
	{
		return new Vector3(pos.x / this.curMapInfo.fMapLength * (float)this.LocalMapTexture.width, pos.z / this.curMapInfo.fMapHeight * (float)this.LocalMapTexture.height, 0f);
	}

	// Token: 0x06004BF1 RID: 19441 RVA: 0x0019A4E4 File Offset: 0x001986E4
	private Vector3 WorldPos2MapPos(float posX, float posZ)
	{
		return new Vector3(posX / this.curMapInfo.fMapLength * (float)this.LocalMapTexture.width, posZ / this.curMapInfo.fMapHeight * (float)this.LocalMapTexture.height, 0f);
	}

	// Token: 0x06004BF2 RID: 19442 RVA: 0x0019A530 File Offset: 0x00198730
	private Vector3 MapPos2WorldPos(Vector3 pos)
	{
		return new Vector3(pos.x / (float)this.LocalMapTexture.width * this.curMapInfo.fMapLength, 0f, pos.y / (float)this.LocalMapTexture.height * this.curMapInfo.fMapHeight);
	}

	// Token: 0x06004BF3 RID: 19443 RVA: 0x0019A588 File Offset: 0x00198788
	public void OnClickLeftMapBtn(int index)
	{
		change_scene_line.request request = new change_scene_line.request();
		request.line_index = (long)(index + 1);
		NetLogic.GetInstance().Send<Protocol.change_scene_line>(request, null);
	}

	// Token: 0x06004BF4 RID: 19444 RVA: 0x0019A5B4 File Offset: 0x001987B4
	public void OnClickRightNPCBtn(MapPoint clickPoint)
	{
		this.Player.BreakAutoCombatState();
		this.Player.SkillLogic.BreakCurSkill();
		Vector3 pointPos = clickPoint.PointPos;
		pointPos.y = SceneManager.GetHitHeight(clickPoint.PointPos);
		SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.StopAutoMoveToMission();
		this.Player.IsNeedAutoMountCar = true;
		if (Vector3.Distance(pointPos, this.Player.Position) / this.Player.NavMeshAgent.speed > 5f)
		{
			this.Player.EnterAutoMoving(Time.time);
		}
		else
		{
			this.Player.EnterAutoMoving(float.MaxValue);
		}
		this.Player.MoveTo(pointPos, 1f, delegate(ObjCharacter A_1)
		{
			this.Player.IsNeedAutoMountCar = false;
			if (clickPoint.PointType == MAP_POINT_TYPE.NPC)
			{
				ObjNPC objNPC = this.FindNPC(clickPoint.NpcId);
				if (objNPC != null && objNPC.CheckInDialogRange())
				{
					Singleton<DialogManager>.Instance.ShowDialog(objNPC, string.Empty);
				}
			}
			else if (clickPoint.PointType == MAP_POINT_TYPE.MONSTER)
			{
				SingletonUnity<JueseJiNengQuLogic>.Instance.UseSkill_1_Onclick();
			}
		});
	}

	// Token: 0x06004BF5 RID: 19445 RVA: 0x0019A69C File Offset: 0x0019889C
	private ObjNPC FindNPC(string npcId)
	{
		List<Obj> list = new List<Obj>(Singleton<ObjManager>.Instance.ObjDict.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
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

	// Token: 0x06004BF6 RID: 19446 RVA: 0x0019A708 File Offset: 0x00198908
	public void OnClickZhuChengBtn()
	{
	}

	// Token: 0x06004BF7 RID: 19447 RVA: 0x0019A70C File Offset: 0x0019890C
	public void OnClickPingMingKuBtn()
	{
	}

	// Token: 0x06004BF8 RID: 19448 RVA: 0x0019A710 File Offset: 0x00198910
	public void ChooseActivityObj(int actType, int subType, string actId)
	{
		if (this.mLoadingMapFlag)
		{
			this.mFindTargetFlag = true;
			this.mTargetType = actType;
			this.mTargetSubtype = subType;
			this.mTargetActId = actId;
			this.mTargetIsMission = false;
		}
		else
		{
			this.mFindTargetFlag = false;
			for (int i = 0; i < this.MapActivityObjList.Count; i++)
			{
				if (UnityVersionUtil.IsActive(this.MapActivityObjList[i].gameObject))
				{
					this.MapActivityObjList[i].UpdateSelection(actType, subType, actId);
				}
			}
			for (int j = 0; j < this.MissionActivityObjList.Count; j++)
			{
				if (UnityVersionUtil.IsActive(this.MissionActivityObjList[j].gameObject))
				{
					this.MissionActivityObjList[j].UpdateSelection(actType, subType, actId);
				}
			}
		}
	}

	// Token: 0x06004BF9 RID: 19449 RVA: 0x0019A7F0 File Offset: 0x001989F0
	public void ChooseActivityObj(string actId, bool isMission)
	{
		if (this.mLoadingMapFlag)
		{
			this.mFindTargetFlag = true;
			this.mTargetType = -99;
			this.mTargetSubtype = -99;
			this.mTargetActId = actId;
			this.mTargetIsMission = isMission;
		}
		else
		{
			this.mFindTargetFlag = false;
			for (int i = 0; i < this.MapActivityObjList.Count; i++)
			{
				if (UnityVersionUtil.IsActive(this.MapActivityObjList[i].gameObject))
				{
					this.MapActivityObjList[i].UpdateSelection(actId, isMission);
				}
			}
			for (int j = 0; j < this.MissionActivityObjList.Count; j++)
			{
				if (UnityVersionUtil.IsActive(this.MissionActivityObjList[j].gameObject))
				{
					this.MissionActivityObjList[j].UpdateSelection(actId, isMission);
				}
			}
		}
	}

	// Token: 0x06004BFA RID: 19450 RVA: 0x0019A8D0 File Offset: 0x00198AD0
	public void SetActivityObjNormal()
	{
		for (int i = 0; i < this.MapActivityObjList.Count; i++)
		{
			if (UnityVersionUtil.IsActive(this.MapActivityObjList[i].gameObject))
			{
				this.MapActivityObjList[i].SetNormalState();
			}
		}
		for (int j = 0; j < this.MissionActivityObjList.Count; j++)
		{
			if (UnityVersionUtil.IsActive(this.MissionActivityObjList[j].gameObject))
			{
				this.MissionActivityObjList[j].SetNormalState();
			}
		}
	}

	// Token: 0x06004BFB RID: 19451 RVA: 0x0019A970 File Offset: 0x00198B70
	public void SetActivityObjDisable()
	{
		for (int i = 0; i < this.MapActivityObjList.Count; i++)
		{
			if (UnityVersionUtil.IsActive(this.MapActivityObjList[i].gameObject))
			{
				this.MapActivityObjList[i].SetDisActiveState();
			}
		}
		for (int j = 0; j < this.MissionActivityObjList.Count; j++)
		{
			if (UnityVersionUtil.IsActive(this.MissionActivityObjList[j].gameObject))
			{
				this.MissionActivityObjList[j].SetDisActiveState();
			}
		}
	}

	// Token: 0x06004BFC RID: 19452 RVA: 0x0019AA10 File Offset: 0x00198C10
	public void OnClickLocalMap()
	{
		if (this.mLoadingMapFlag || !this.CurMapInfo.ID.Equals(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID))
		{
			return;
		}
		if (this.Player.AttributeData.Level <= 5)
		{
			return;
		}
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (instance.SceneManager.IsHaveMoveTarget)
		{
			instance.SceneManager.ClearMoveTarget();
			return;
		}
		Transform cachedTransform = this.LocalMapTexture.cachedTransform;
		Vector3 vector = this.MapPos2WorldPos(cachedTransform.worldToLocalMatrix.MultiplyPoint(UICamera.lastHit.point));
		if (!SceneManager.IsInNavmeshArea(vector))
		{
			return;
		}
		this.Player.StopAutoAndSkill();
		vector.y = SceneManager.GetHitHeight(vector);
		instance.MissionManager.StopAutoMoveToMission();
		this.Player.IsNeedAutoMountCar = true;
		if (Vector3.Distance(vector, this.Player.Position) / this.Player.NavMeshAgent.speed > 5f)
		{
			this.Player.EnterAutoMoving(Time.time);
		}
		else
		{
			this.Player.EnterAutoMoving(float.MaxValue);
		}
		instance.SceneManager.SetMoveTarget(vector, string.Empty);
		this.Player.MoveTo(vector, 1f, delegate(ObjCharacter A_1)
		{
			this.Player.IsNeedAutoMountCar = false;
		});
	}

	// Token: 0x06004BFD RID: 19453 RVA: 0x0019AB70 File Offset: 0x00198D70
	private void OnDisable()
	{
		this.curMapInfo = null;
	}

	// Token: 0x06004BFE RID: 19454 RVA: 0x0019AB7C File Offset: 0x00198D7C
	public void DrawMapLine(List<Vector3> posList, Vector3 playerPos, Vector3 targetPos)
	{
		this.PathLineRender.SetVertexCount(posList.Count);
		for (int i = 0; i < posList.Count; i++)
		{
			this.PathLineRender.SetPosition(i, this.WorldPos2MapPos(posList[i]));
		}
		this.WalkLineRender1.SetVertexCount(0);
		this.WalkLineRender2.SetVertexCount(0);
	}

	// Token: 0x06004BFF RID: 19455 RVA: 0x0019ABE4 File Offset: 0x00198DE4
	public void ClearMapLine()
	{
		this.PathLineRender.SetVertexCount(0);
		this.WalkLineRender1.SetVertexCount(0);
		this.WalkLineRender2.SetVertexCount(0);
	}

	// Token: 0x06004C00 RID: 19456 RVA: 0x0019AC18 File Offset: 0x00198E18
	public void OnlyDrawWalkLine(Vector3 playerPos, Vector3 targetPos)
	{
		this.WalkLineRender1.SetVertexCount(0);
		this.WalkLineRender2.SetVertexCount(0);
		this.PathLineRender.SetVertexCount(2);
		this.PathLineRender.SetPosition(0, this.WorldPos2MapPos(playerPos));
		this.PathLineRender.SetPosition(1, this.WorldPos2MapPos(targetPos));
	}

	// Token: 0x06004C01 RID: 19457 RVA: 0x0019AC70 File Offset: 0x00198E70
	public void UpdateNpcPos()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (this.CurMapInfo == null || sceneManager == null || !sceneManager.CurrentMapInofData.ID.Equals(this.CurMapInfo.ID))
		{
			for (int i = 0; i < this.NpcPointPicList.Count; i++)
			{
				this.NpcPointPicList[i].enabled = false;
			}
			return;
		}
		List<ObjNPC> mapShowObjList = Singleton<ObjManager>.Instance.MapShowObjList;
		List<ObjNPC> list = new List<ObjNPC>();
		for (int j = 0; j < mapShowObjList.Count; j++)
		{
			if (mapShowObjList[j].IsCityCaptureNpc())
			{
				list.Add(mapShowObjList[j]);
			}
		}
		int num = list.Count - this.NpcPointPicList.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.NpcPointPicList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.NpcPointPicList[0].transform.parent;
				gameObject.transform.localScale = Vector3.one;
				this.NpcPointPicList.Add(gameObject.GetComponent<UISprite>());
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			this.NpcPointPicList[l].enabled = true;
			this.NpcPointPicList[l].spriteName = GameDefine.GetObjTypeSpriteName(list[l]);
			if (list[l].IsCityCaptureNpc())
			{
				this.NpcPointPicList[l].SetDimensions(20, 20);
			}
			else
			{
				this.NpcPointPicList[l].MakePixelPerfect();
			}
			this.NpcPointPicList[l].transform.localPosition = this.WorldPos2MapPos(list[l].transform.position);
		}
		for (int m = list.Count; m < this.NpcPointPicList.Count; m++)
		{
			this.NpcPointPicList[m].enabled = false;
		}
	}

	// Token: 0x04003984 RID: 14724
	public float FlashInterval;

	// Token: 0x04003985 RID: 14725
	public UILabel MapNameLabel;

	// Token: 0x04003986 RID: 14726
	public Transform LocalMapPlayerPic;

	// Token: 0x04003987 RID: 14727
	public UITexture LocalMapTexture;

	// Token: 0x04003988 RID: 14728
	private ObjMainPlayer Player;

	// Token: 0x04003989 RID: 14729
	private MapInfoData curMapInfo;

	// Token: 0x0400398A RID: 14730
	public List<UISprite> MonsterPointPic;

	// Token: 0x0400398B RID: 14731
	public List<UISprite> TeleportPointPic;

	// Token: 0x0400398C RID: 14732
	public List<UISprite> SurviveTelPic;

	// Token: 0x0400398D RID: 14733
	public List<UISprite> NPCPointPic;

	// Token: 0x0400398E RID: 14734
	public List<UISprite> NPC2PointPic;

	// Token: 0x0400398F RID: 14735
	public List<UISprite> NPC3PointPic;

	// Token: 0x04003990 RID: 14736
	public List<UISprite> NpcPointPicList = new List<UISprite>();

	// Token: 0x04003991 RID: 14737
	public List<NewMapActivityObj> MapActivityObjList = new List<NewMapActivityObj>();

	// Token: 0x04003992 RID: 14738
	public List<NewMapActivityObj> MissionActivityObjList = new List<NewMapActivityObj>();

	// Token: 0x04003993 RID: 14739
	private List<MapPoint> curSceneTelePortInfo = new List<MapPoint>();

	// Token: 0x04003994 RID: 14740
	private List<MapPoint> curSceneMonsterData = new List<MapPoint>();

	// Token: 0x04003995 RID: 14741
	private List<MapPoint> curSceneNPCData = new List<MapPoint>();

	// Token: 0x04003996 RID: 14742
	private List<ActivityMapData> curActivityMapData = new List<ActivityMapData>();

	// Token: 0x04003997 RID: 14743
	private List<ActivityMapData> curVisibleActivityMapData = new List<ActivityMapData>();

	// Token: 0x04003998 RID: 14744
	private List<MissionData> curMapAcceptMissionDataList = new List<MissionData>();

	// Token: 0x04003999 RID: 14745
	private List<MissionData> curAcceptableMissionDataList = new List<MissionData>();

	// Token: 0x0400399A RID: 14746
	private List<MissionData> curAcceptedMissionDataList = new List<MissionData>();

	// Token: 0x0400399B RID: 14747
	private List<MapPoint> curShowPointList = new List<MapPoint>();

	// Token: 0x0400399C RID: 14748
	public int LineCount = 5;

	// Token: 0x0400399D RID: 14749
	public UILabel OnLineINfoLabel;

	// Token: 0x0400399E RID: 14750
	private bool mLocalMapFlag = true;

	// Token: 0x0400399F RID: 14751
	public GameObject LineObj;

	// Token: 0x040039A0 RID: 14752
	public UISprite MoveTargetPic;

	// Token: 0x040039A1 RID: 14753
	public List<UILabel> MapLockLabel;

	// Token: 0x040039A2 RID: 14754
	public LineRenderer PathLineRender;

	// Token: 0x040039A3 RID: 14755
	public LineRenderer WalkLineRender1;

	// Token: 0x040039A4 RID: 14756
	public LineRenderer WalkLineRender2;

	// Token: 0x040039A5 RID: 14757
	public UILabel MapcapturLabel;

	// Token: 0x040039A6 RID: 14758
	private NewMapUIRootLogic.SHOW_TYPE curShowType;

	// Token: 0x040039A7 RID: 14759
	private bool mLoadingMapFlag;

	// Token: 0x040039A8 RID: 14760
	private bool mFindTargetFlag;

	// Token: 0x040039A9 RID: 14761
	private int mTargetType = -99;

	// Token: 0x040039AA RID: 14762
	private int mTargetSubtype = -99;

	// Token: 0x040039AB RID: 14763
	private bool mTargetIsMission;

	// Token: 0x040039AC RID: 14764
	private string mTargetActId = string.Empty;

	// Token: 0x040039AD RID: 14765
	private Texture tutorialLockPic;

	// Token: 0x040039AE RID: 14766
	private float MaxMapWidth = 300f;

	// Token: 0x040039AF RID: 14767
	private float MaxMapHeight = 300f;

	// Token: 0x040039B0 RID: 14768
	private float intervalCount;

	// Token: 0x040039B1 RID: 14769
	private StringBuilder mTempStr = new StringBuilder();

	// Token: 0x02000A2E RID: 2606
	public enum SHOW_TYPE
	{
		// Token: 0x040039B5 RID: 14773
		FUNCTION,
		// Token: 0x040039B6 RID: 14774
		MONSTER
	}
}
