using System;
using System.Collections.Generic;
using System.Text;
using SprotoType;
using UnityEngine;

// Token: 0x02000954 RID: 2388
public class MapUIRootLogic : SingletonUnity<MapUIRootLogic>
{
	// Token: 0x060042CB RID: 17099 RVA: 0x0014733C File Offset: 0x0014553C
	protected override void Awake()
	{
		base.Awake();
		this.Player = Singleton<ObjManager>.Instance.MainPlayer;
		this.InitTexture();
		this.UIWrapContent.enabled = false;
		UIWrapContentNew uiwrapContent = this.UIWrapContent;
		uiwrapContent.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContent.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
		this.curSceneTelePortInfo.Clear();
		this.curSceneMonsterData.Clear();
		this.curSceneNPCData.Clear();
		this.InitMonsterPointPic();
		this.InitTeleportPointPic();
		this.UpdateMissionPointPic();
		this.UpdateRightBtn();
		this.InitLeftBtn();
	}

	// Token: 0x060042CC RID: 17100 RVA: 0x001473D8 File Offset: 0x001455D8
	public void InitTexture()
	{
		this.curLocalMapInfo = DataManager.GetMapInfoDataByID(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr);
		List<string> list = new List<string>();
		if (this.LocalMapTexture.mainTexture == null || !this.LocalMapTexture.mainTexture.name.Equals(this.curLocalMapInfo.MiniMapName))
		{
			list.Add(this.curLocalMapInfo.MiniMapName);
		}
		if (this.WorldMapPic.mainTexture == null)
		{
			list.Add(GameDefine.WorldMap);
		}
		if (list.Count == 0)
		{
			return;
		}
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060042CD RID: 17101 RVA: 0x001474A4 File Offset: 0x001456A4
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			return;
		}
		this.curLocalMapInfo = DataManager.GetMapInfoDataByID(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr);
		if (retdic.ContainsKey(this.curLocalMapInfo.MiniMapName))
		{
			Texture texture = retdic[this.curLocalMapInfo.MiniMapName];
			if (texture != null)
			{
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
				Debug.LogWarning("no mini map1 :" + this.curLocalMapInfo.MiniMapName);
			}
		}
		else
		{
			Debug.LogWarning("no mini map2 :" + this.curLocalMapInfo.MiniMapName);
		}
		if (retdic.ContainsKey(GameDefine.WorldMap))
		{
			this.WorldMapPic.mainTexture = retdic[GameDefine.WorldMap];
		}
		this.InitTeleportPointPic();
		this.InitMonsterPointPic();
		this.UpdateMissionPointPic();
	}

	// Token: 0x060042CE RID: 17102 RVA: 0x00147684 File Offset: 0x00145884
	public void OnClickFuncitonTab()
	{
		if (this.curShowType == MapUIRootLogic.SHOW_TYPE.FUNCTION)
		{
			return;
		}
		this.curShowType = MapUIRootLogic.SHOW_TYPE.FUNCTION;
		this.UpdateRightBtn();
	}

	// Token: 0x060042CF RID: 17103 RVA: 0x001476A0 File Offset: 0x001458A0
	public void OnClickMonsterTab()
	{
		if (this.curShowType == MapUIRootLogic.SHOW_TYPE.MONSTER)
		{
			return;
		}
		this.curShowType = MapUIRootLogic.SHOW_TYPE.MONSTER;
		this.UpdateRightBtn();
	}

	// Token: 0x060042D0 RID: 17104 RVA: 0x001476BC File Offset: 0x001458BC
	public void OnClickChangeLineBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCopyShowMap())
		{
			return;
		}
		this.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MapLineInfoLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<MapLineInfoLogic>.Instance.PreReset();
			NetLogic.GetInstance().Send<Protocol.request_line_state>(null, null);
		}, null);
	}

	// Token: 0x060042D1 RID: 17105 RVA: 0x00147714 File Offset: 0x00145914
	private void InitLeftBtn()
	{
		int curLineIndex = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex;
		int lineCount = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.LineCount;
		this.OnLineINfoLabel.text = string.Format("line{0}", curLineIndex);
	}

	// Token: 0x060042D2 RID: 17106 RVA: 0x0014775C File Offset: 0x0014595C
	private int GetMapBtnIndex(int RunningMapId)
	{
		if (RunningMapId == 101)
		{
			return 0;
		}
		if (RunningMapId != 102)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x060042D3 RID: 17107 RVA: 0x00147788 File Offset: 0x00145988
	private void UpdateCopyBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCopyShowMap())
		{
			UnityVersionUtil.SetActiveRecursive(this.WorldBtnObj, false);
			UnityVersionUtil.SetActiveRecursive(this.LineObj, false);
		}
	}

	// Token: 0x060042D4 RID: 17108 RVA: 0x001477C4 File Offset: 0x001459C4
	public void Reset()
	{
		UnityVersionUtil.SetActiveRecursive(this.WorldMapRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.LocalMapRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.RightBtnRoot, true);
		this.mLocalMapFlag = true;
		this.UpdateMissionPointPic();
		this.UpdateRightBtn();
		this.InitLeftBtn();
		this.UpdateCopyBtn();
	}

	// Token: 0x060042D5 RID: 17109 RVA: 0x00147814 File Offset: 0x00145A14
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
			if (currentMapInofData.ID == empty && curLineIndex == num)
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

	// Token: 0x060042D6 RID: 17110 RVA: 0x001479FC File Offset: 0x00145BFC
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
		List<TimerActivityData> enableTimerActivityList = DataManager.GetEnableTimerActivityList();
		List<ActivityBossData> list3 = new List<ActivityBossData>();
		if (enableTimerActivityList != null)
		{
			for (int num2 = 0; num2 < enableTimerActivityList.Count; num2++)
			{
				ActivityBossData activityBossDataByActivityIdMapId = DataManager.GetActivityBossDataByActivityIdMapId(enableTimerActivityList[num2].ID, sceneManager.CurrentMapInofData.ID);
				if (activityBossDataByActivityIdMapId != null)
				{
					list3.Add(activityBossDataByActivityIdMapId);
				}
			}
			for (int num3 = 0; num3 < list3.Count; num3++)
			{
				for (int num4 = 0; num4 < list3[num3].NpcPosList.Count; num4++)
				{
					list.Add(list3[num3].NpcPosList[num4]);
				}
			}
		}
		int num5 = list.Count - this.MonsterPointPic.Count;
		if (num5 > 0)
		{
			for (int num6 = 0; num6 < num5; num6++)
			{
				GameObject gameObject2 = Object.Instantiate(this.MonsterPointPic[0].gameObject) as GameObject;
				gameObject2.transform.parent = this.MonsterPointPic[0].transform.parent;
				this.MonsterPointPic.Add(gameObject2.GetComponent<UISprite>());
			}
		}
		for (int num7 = 0; num7 < this.MonsterPointPic.Count; num7++)
		{
			this.MonsterPointPic[num7].enabled = (num7 < list.Count);
		}
		for (int num8 = 0; num8 < list.Count; num8++)
		{
			this.MonsterPointPic[num8].transform.localPosition = this.WorldPos2MapPos(list[num8]);
		}
	}

	// Token: 0x060042D7 RID: 17111 RVA: 0x00147FC0 File Offset: 0x001461C0
	private void InitTeleportPointPic()
	{
		this.TeleportPointPic[0].enabled = false;
		this.SurviveTelPic[0].alpha = 0f;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsCopyShowMap())
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
		MapInfoData currentMapInofData2 = sceneManager.CurrentMapInofData;
		MapPoint mapPoint2 = new MapPoint();
		mapPoint2.PointName = StrDictionary.GetDictionaryString("#{101809}", new object[0]);
		mapPoint2.PointPos = currentMapInofData2.TelePortPosVector3;
		mapPoint2.PointType = MAP_POINT_TYPE.TELEPORT;
		mapPoint2.NpcId = string.Empty;
		this.curSceneTelePortInfo.Add(mapPoint2);
		this.TeleportPointPic[0].transform.localPosition = this.WorldPos2MapPos(currentMapInofData2.TelePortPosVector3);
	}

	// Token: 0x060042D8 RID: 17112 RVA: 0x00148250 File Offset: 0x00146450
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		MapBtnLine itemLogic = this.mapRightBtnLineList[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x060042D9 RID: 17113 RVA: 0x00148278 File Offset: 0x00146478
	private void ResetItemLine(MapBtnLine itemLogic, int idx)
	{
		if (idx < this.curShowPointList.Count)
		{
			itemLogic.ResetRightBtn(this.curShowPointList[idx]);
		}
	}

	// Token: 0x060042DA RID: 17114 RVA: 0x001482A0 File Offset: 0x001464A0
	private void UpdateRightBtn()
	{
		if (this.curShowType == MapUIRootLogic.SHOW_TYPE.FUNCTION)
		{
			this.SelectSprite.transform.position = this.FunctionLabel.transform.position;
			this.curShowPointList.Clear();
			this.curShowPointList.AddRange(this.curSceneNPCData);
			this.curShowPointList.AddRange(this.curSceneTelePortInfo);
		}
		else
		{
			this.SelectSprite.transform.position = this.MonsterLabel.transform.position;
			this.curShowPointList.Clear();
			this.curShowPointList.AddRange(this.curSceneMonsterData);
		}
		int count = this.curShowPointList.Count;
		int num = Mathf.Min(count, this.LineCount) - this.mapRightBtnLineList.Count;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = Object.Instantiate(this.mapRightBtnLineList[0].gameObject) as GameObject;
			gameObject.name = string.Format("item_{0}", this.mapRightBtnLineList.Count);
			gameObject.transform.parent = this.mapRightBtnLineList[0].transform.parent;
			gameObject.transform.localScale = Vector3.one;
			this.mapRightBtnLineList.Add(gameObject.GetComponent<MapBtnLine>());
		}
		for (int j = 0; j < this.mapRightBtnLineList.Count; j++)
		{
			NGUITools.SetActive(this.mapRightBtnLineList[j].gameObject, j < this.curShowPointList.Count);
			if (j < this.curShowPointList.Count)
			{
				this.ResetItemLine(this.mapRightBtnLineList[j], j);
			}
		}
		this.UIWrapContent.maxIndex = 0;
		this.UIWrapContent.minIndex = 1 - this.curShowPointList.Count;
		this.WrapContentBottomWidget.height = this.curShowPointList.Count * this.UIWrapContent.itemSize;
		this.UIWrapContent.SortBasedOnScrollMovement();
		this.UIWrapContent.enabled = true;
		this.ScrollView.ResetPosition();
	}

	// Token: 0x060042DB RID: 17115 RVA: 0x001484D4 File Offset: 0x001466D4
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MapUIRoot);
	}

	// Token: 0x060042DC RID: 17116 RVA: 0x001484E8 File Offset: 0x001466E8
	private void Update()
	{
		this.intervalCount += Time.deltaTime;
		if (this.intervalCount > this.FlashInterval)
		{
			this.intervalCount = 0f;
			this.FlashPlayerPos();
		}
	}

	// Token: 0x060042DD RID: 17117 RVA: 0x0014852C File Offset: 0x0014672C
	private void FlashPlayerPos()
	{
		if (this.curLocalMapInfo == null)
		{
			return;
		}
		this.LocalMapPlayerPic.localPosition = this.WorldPos2MapPos(this.Player.Position);
		this.LocalMapPlayerPic.localEulerAngles = new Vector3(0f, 0f, -this.Player.transform.localEulerAngles.y);
		this.mTempStr.Length = 0;
		this.PlayerPosLabel.text = this.mTempStr.AppendFormat("{0},{1}", (int)this.Player.Position.x, (int)this.Player.Position.z).ToString();
		this.UpdateMissionPointPic();
	}

	// Token: 0x060042DE RID: 17118 RVA: 0x001485F8 File Offset: 0x001467F8
	private Vector3 WorldPos2MapPos(Vector3 pos)
	{
		return new Vector3(pos.x / this.curLocalMapInfo.fMapLength * (float)this.LocalMapTexture.width, pos.z / this.curLocalMapInfo.fMapHeight * (float)this.LocalMapTexture.height, 0f);
	}

	// Token: 0x060042DF RID: 17119 RVA: 0x00148650 File Offset: 0x00146850
	private Vector3 WorldPos2MapPos(float posX, float posZ)
	{
		return new Vector3(posX / this.curLocalMapInfo.fMapLength * (float)this.LocalMapTexture.width, posZ / this.curLocalMapInfo.fMapHeight * (float)this.LocalMapTexture.height, 0f);
	}

	// Token: 0x060042E0 RID: 17120 RVA: 0x0014869C File Offset: 0x0014689C
	private Vector3 MapPos2WorldPos(Vector3 pos)
	{
		return new Vector3(pos.x / (float)this.LocalMapTexture.width * this.curLocalMapInfo.fMapLength, 0f, pos.y / (float)this.LocalMapTexture.height * this.curLocalMapInfo.fMapHeight);
	}

	// Token: 0x060042E1 RID: 17121 RVA: 0x001486F4 File Offset: 0x001468F4
	public void OnClickLeftMapBtn(int index)
	{
		change_scene_line.request request = new change_scene_line.request();
		request.line_index = (long)(index + 1);
		NetLogic.GetInstance().Send<Protocol.change_scene_line>(request, null);
	}

	// Token: 0x060042E2 RID: 17122 RVA: 0x00148720 File Offset: 0x00146920
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

	// Token: 0x060042E3 RID: 17123 RVA: 0x00148808 File Offset: 0x00146A08
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

	// Token: 0x060042E4 RID: 17124 RVA: 0x00148874 File Offset: 0x00146A74
	public void OnClickChangeMapBtn()
	{
		if (this.mLocalMapFlag)
		{
			this.mLocalMapFlag = false;
		}
		else
		{
			this.mLocalMapFlag = true;
		}
		this.ChangeToLocalMap(this.mLocalMapFlag);
	}

	// Token: 0x060042E5 RID: 17125 RVA: 0x001488AC File Offset: 0x00146AAC
	public void ChangeToLocalMap(bool isTrue)
	{
		this.mLocalMapFlag = isTrue;
		if (isTrue)
		{
			UnityVersionUtil.SetActiveRecursive(this.WorldMapRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.LocalMapRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.RightBtnRoot, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.WorldMapRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.LocalMapRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.RightBtnRoot, false);
			this.InitWorld();
		}
	}

	// Token: 0x060042E6 RID: 17126 RVA: 0x0014891C File Offset: 0x00146B1C
	public void OnClickLocalMap()
	{
		Transform cachedTransform = this.LocalMapTexture.cachedTransform;
		Vector3 vector = this.MapPos2WorldPos(cachedTransform.worldToLocalMatrix.MultiplyPoint(UICamera.lastHit.point));
		this.Player.StopAutoAndSkill();
		vector.y = SceneManager.GetHitHeight(vector);
		SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.StopAutoMoveToMission();
		this.Player.IsNeedAutoMountCar = true;
		if (Vector3.Distance(vector, this.Player.Position) / this.Player.NavMeshAgent.speed > 5f)
		{
			this.Player.EnterAutoMoving(Time.time);
		}
		else
		{
			this.Player.EnterAutoMoving(float.MaxValue);
		}
		this.Player.MoveTo(vector, 1f, delegate(ObjCharacter A_1)
		{
			this.Player.IsNeedAutoMountCar = false;
		});
	}

	// Token: 0x060042E7 RID: 17127 RVA: 0x001489F8 File Offset: 0x00146BF8
	private void InitWorld()
	{
		List<MapInfoData> mapInfoDataListByType = DataManager.GetMapInfoDataListByType(MAPTYPE.BIG_WORLD);
		mapInfoDataListByType.Add(DataManager.GetMapInfoDataListByType(MAPTYPE.TUTORIAL_CAR)[0]);
		int num = mapInfoDataListByType.Count - this.worldItemLogicList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.worldItemLogicList[0].gameObject) as GameObject;
				WorldItemLogic component = gameObject.GetComponent<WorldItemLogic>();
				gameObject.transform.parent = this.worldItemLogicList[0].gameObject.transform.parent;
				this.worldItemLogicList.Add(component);
			}
		}
		for (int j = 0; j < mapInfoDataListByType.Count; j++)
		{
			this.worldItemLogicList[j].ResetItem(mapInfoDataListByType[j], false);
		}
	}

	// Token: 0x060042E8 RID: 17128 RVA: 0x00148AD8 File Offset: 0x00146CD8
	public void OnClickZhuChengBtn()
	{
	}

	// Token: 0x060042E9 RID: 17129 RVA: 0x00148ADC File Offset: 0x00146CDC
	public void OnClickPingMingKuBtn()
	{
	}

	// Token: 0x04002F23 RID: 12067
	public float FlashInterval;

	// Token: 0x04002F24 RID: 12068
	public GameObject WorldMapRoot;

	// Token: 0x04002F25 RID: 12069
	public GameObject LocalMapRoot;

	// Token: 0x04002F26 RID: 12070
	public GameObject RightBtnRoot;

	// Token: 0x04002F27 RID: 12071
	public UILabel PlayerPosLabel;

	// Token: 0x04002F28 RID: 12072
	public Transform LocalMapPlayerPic;

	// Token: 0x04002F29 RID: 12073
	public UITexture LocalMapTexture;

	// Token: 0x04002F2A RID: 12074
	public UITexture WorldMapPic;

	// Token: 0x04002F2B RID: 12075
	private ObjMainPlayer Player;

	// Token: 0x04002F2C RID: 12076
	private MapInfoData curLocalMapInfo;

	// Token: 0x04002F2D RID: 12077
	public List<UISprite> MonsterPointPic;

	// Token: 0x04002F2E RID: 12078
	public List<UISprite> TeleportPointPic;

	// Token: 0x04002F2F RID: 12079
	public List<UISprite> SurviveTelPic;

	// Token: 0x04002F30 RID: 12080
	public List<UISprite> NPCPointPic;

	// Token: 0x04002F31 RID: 12081
	public List<UISprite> NPC2PointPic;

	// Token: 0x04002F32 RID: 12082
	public List<UISprite> NPC3PointPic;

	// Token: 0x04002F33 RID: 12083
	public List<WorldItemLogic> worldItemLogicList = new List<WorldItemLogic>();

	// Token: 0x04002F34 RID: 12084
	private List<MapPoint> curSceneTelePortInfo = new List<MapPoint>();

	// Token: 0x04002F35 RID: 12085
	private List<MapPoint> curSceneMonsterData = new List<MapPoint>();

	// Token: 0x04002F36 RID: 12086
	private List<MapPoint> curSceneNPCData = new List<MapPoint>();

	// Token: 0x04002F37 RID: 12087
	private List<MapPoint> curShowPointList = new List<MapPoint>();

	// Token: 0x04002F38 RID: 12088
	public List<MapBtnLine> mapRightBtnLineList = new List<MapBtnLine>();

	// Token: 0x04002F39 RID: 12089
	public List<MapBtnLine> mapLeftBtnLineList = new List<MapBtnLine>();

	// Token: 0x04002F3A RID: 12090
	public int LineCount = 5;

	// Token: 0x04002F3B RID: 12091
	public UILabel OnLineINfoLabel;

	// Token: 0x04002F3C RID: 12092
	private bool mLocalMapFlag = true;

	// Token: 0x04002F3D RID: 12093
	public GameObject[] WorldMapBtnRoot;

	// Token: 0x04002F3E RID: 12094
	public UIWrapContentNew UIWrapContent;

	// Token: 0x04002F3F RID: 12095
	public UIScrollView ScrollView;

	// Token: 0x04002F40 RID: 12096
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04002F41 RID: 12097
	public UILabel FunctionLabel;

	// Token: 0x04002F42 RID: 12098
	public UILabel MonsterLabel;

	// Token: 0x04002F43 RID: 12099
	public UISprite SelectSprite;

	// Token: 0x04002F44 RID: 12100
	public GameObject WorldBtnObj;

	// Token: 0x04002F45 RID: 12101
	public GameObject LineObj;

	// Token: 0x04002F46 RID: 12102
	private MapUIRootLogic.SHOW_TYPE curShowType;

	// Token: 0x04002F47 RID: 12103
	private float MaxMapWidth = 350f;

	// Token: 0x04002F48 RID: 12104
	private float MaxMapHeight = 350f;

	// Token: 0x04002F49 RID: 12105
	private float intervalCount;

	// Token: 0x04002F4A RID: 12106
	private StringBuilder mTempStr = new StringBuilder();

	// Token: 0x02000955 RID: 2389
	public enum SHOW_TYPE
	{
		// Token: 0x04002F4D RID: 12109
		FUNCTION,
		// Token: 0x04002F4E RID: 12110
		MONSTER
	}
}
