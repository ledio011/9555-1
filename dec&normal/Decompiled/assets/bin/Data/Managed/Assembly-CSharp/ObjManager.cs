using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x02000824 RID: 2084
public class ObjManager : Singleton<ObjManager>
{
	// Token: 0x06003381 RID: 13185 RVA: 0x000CAC98 File Offset: 0x000C8E98
	public ObjManager()
	{
		this.ResetObjManager();
	}

	// Token: 0x17000E81 RID: 3713
	// (get) Token: 0x06003382 RID: 13186 RVA: 0x000CAD40 File Offset: 0x000C8F40
	public ObjMainPlayer MainPlayer
	{
		get
		{
			return this.mMainPlayer;
		}
	}

	// Token: 0x17000E82 RID: 3714
	// (get) Token: 0x06003383 RID: 13187 RVA: 0x000CAD48 File Offset: 0x000C8F48
	public ObjPlayerCar MainPlayerCar
	{
		get
		{
			return this.mMainPlayerCar;
		}
	}

	// Token: 0x17000E83 RID: 3715
	// (get) Token: 0x06003384 RID: 13188 RVA: 0x000CAD50 File Offset: 0x000C8F50
	// (set) Token: 0x06003385 RID: 13189 RVA: 0x000CAD58 File Offset: 0x000C8F58
	public Dictionary<long, Obj> ObjDict
	{
		get
		{
			return this.mObjDict;
		}
		set
		{
			this.mObjDict = value;
		}
	}

	// Token: 0x17000E84 RID: 3716
	// (get) Token: 0x06003386 RID: 13190 RVA: 0x000CAD64 File Offset: 0x000C8F64
	// (set) Token: 0x06003387 RID: 13191 RVA: 0x000CAD6C File Offset: 0x000C8F6C
	public Dictionary<string, GameObject> OtherObjDic
	{
		get
		{
			return this.mOtherObjDic;
		}
		set
		{
			this.mOtherObjDic = value;
		}
	}

	// Token: 0x17000E85 RID: 3717
	// (get) Token: 0x06003388 RID: 13192 RVA: 0x000CAD78 File Offset: 0x000C8F78
	public List<ObjNPC> OtherPlayerEscortNPCList
	{
		get
		{
			return this.mOtherPlayerEscortNPCList;
		}
	}

	// Token: 0x06003389 RID: 13193 RVA: 0x000CAD80 File Offset: 0x000C8F80
	public ObjNPC GetNearestEscortNPC()
	{
		float num = float.MaxValue;
		ObjNPC result = null;
		for (int i = 0; i < this.mOtherPlayerEscortNPCList.Count; i++)
		{
			float num2 = Vector3.SqrMagnitude(this.mMainPlayer.Position - this.mOtherPlayerEscortNPCList[i].Position);
			if (num2 < num)
			{
				result = this.mOtherPlayerEscortNPCList[i];
				num = num2;
			}
		}
		return result;
	}

	// Token: 0x17000E86 RID: 3718
	// (get) Token: 0x0600338A RID: 13194 RVA: 0x000CADF8 File Offset: 0x000C8FF8
	public ObjOtherPlayerPool OtherPlayerPool
	{
		get
		{
			return this.mOtherPlayerPool;
		}
	}

	// Token: 0x17000E87 RID: 3719
	// (get) Token: 0x0600338B RID: 13195 RVA: 0x000CAE00 File Offset: 0x000C9000
	public List<ObjCharacter>[] CampList
	{
		get
		{
			return this.mCampList;
		}
	}

	// Token: 0x17000E88 RID: 3720
	// (get) Token: 0x0600338C RID: 13196 RVA: 0x000CAE08 File Offset: 0x000C9008
	public List<ObjCharacter>[] CampTargetList
	{
		get
		{
			return this.mCampTargetList;
		}
	}

	// Token: 0x17000E89 RID: 3721
	// (get) Token: 0x0600338D RID: 13197 RVA: 0x000CAE10 File Offset: 0x000C9010
	public List<ObjNPC> MapShowObjList
	{
		get
		{
			return this.mMapShowObjList;
		}
	}

	// Token: 0x0600338E RID: 13198 RVA: 0x000CAE18 File Offset: 0x000C9018
	public void AddMapShowObj(Obj obj)
	{
		ObjNPC objNPC = obj as ObjNPC;
		if (objNPC != null && objNPC.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC && !objNPC.IsSoundBoxNpc())
		{
			this.mMapShowObjList.Add(objNPC);
			this.UpdateMapNpc();
		}
	}

	// Token: 0x0600338F RID: 13199 RVA: 0x000CAE60 File Offset: 0x000C9060
	public void RemoveShowObj(ObjCharacter objCha)
	{
		if (this.mMapShowObjList.Remove(objCha as ObjNPC))
		{
			this.UpdateMapNpc();
		}
	}

	// Token: 0x06003390 RID: 13200 RVA: 0x000CAE80 File Offset: 0x000C9080
	private void UpdateMapNpc()
	{
		if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.UpdateNpcPos();
		}
	}

	// Token: 0x06003391 RID: 13201 RVA: 0x000CAEB8 File Offset: 0x000C90B8
	public void AddToTargetCampList(ObjCharacter objCha)
	{
		CampTool.GetBeAttackedList(objCha.AttributeData.Camp, this.mTargetCampList);
		if (objCha.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCanAttackOtherPlayerScene() && !this.mTargetCampList.Contains(0))
		{
			this.mTargetCampList.Add(0);
		}
		for (int i = 0; i < this.mTargetCampList.Count; i++)
		{
			this.mCampTargetList[this.mTargetCampList[i]].Add(objCha);
		}
		this.mCampList[(int)objCha.AttributeData.Camp].Add(objCha);
	}

	// Token: 0x06003392 RID: 13202 RVA: 0x000CAF68 File Offset: 0x000C9168
	public void RemoveFromTargetCampList(ObjCharacter objCha)
	{
		CampTool.GetBeAttackedList(objCha.AttributeData.Camp, this.mTargetCampList);
		if (objCha.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCanAttackOtherPlayerScene() && !this.mTargetCampList.Contains(0))
		{
			this.mTargetCampList.Add(0);
		}
		for (int i = 0; i < this.mTargetCampList.Count; i++)
		{
			this.mCampTargetList[this.mTargetCampList[i]].Remove(objCha);
		}
		this.mCampList[(int)objCha.AttributeData.Camp].Remove(objCha);
	}

	// Token: 0x06003393 RID: 13203 RVA: 0x000CB018 File Offset: 0x000C9218
	public void ChangeCamp(ObjCharacter objCha, GameDefine.CAMP_TYPE preCamp, GameDefine.CAMP_TYPE targetCamp)
	{
		this.mCampList[(int)preCamp].Remove(objCha);
		this.mCampList[(int)targetCamp].Add(objCha);
		CampTool.GetBeAttackedList(preCamp, this.mTargetCampList);
		for (int i = 0; i < this.mTargetCampList.Count; i++)
		{
			this.mCampTargetList[this.mTargetCampList[i]].Remove(objCha);
		}
		CampTool.GetBeAttackedList(targetCamp, this.mTargetCampList);
		for (int j = 0; j < this.mTargetCampList.Count; j++)
		{
			this.mCampTargetList[this.mTargetCampList[j]].Add(objCha);
		}
	}

	// Token: 0x17000E8A RID: 3722
	// (get) Token: 0x06003394 RID: 13204 RVA: 0x000CB0C8 File Offset: 0x000C92C8
	public Dictionary<long, ObjInitPlayerData> OtherPlayerNoLogicDataDic
	{
		get
		{
			return this.mOtherPlayerNoLogicDataDic;
		}
	}

	// Token: 0x06003395 RID: 13205 RVA: 0x000CB0D0 File Offset: 0x000C92D0
	public ObjInitPlayerData GetNoLogicOtherPlayerData(long serverId)
	{
		if (this.mOtherPlayerNoLogicDataDic.ContainsKey(serverId))
		{
			return this.mOtherPlayerNoLogicDataDic[serverId];
		}
		return null;
	}

	// Token: 0x06003396 RID: 13206 RVA: 0x000CB0F4 File Offset: 0x000C92F4
	public bool RemoveNoLogicOtherPlayerData(long serverId)
	{
		if (this.mOtherPlayerNoLogicDataDic.ContainsKey(serverId))
		{
			this.mOtherPlayerNoLogicDataDic.Remove(serverId);
			return true;
		}
		return false;
	}

	// Token: 0x17000E8B RID: 3723
	// (get) Token: 0x06003397 RID: 13207 RVA: 0x000CB118 File Offset: 0x000C9318
	public List<ObjOtherPlayer> ObjOtherPlayerVisibleList
	{
		get
		{
			return this.mObjOtherPlayerVisibleList;
		}
	}

	// Token: 0x17000E8C RID: 3724
	// (get) Token: 0x06003398 RID: 13208 RVA: 0x000CB120 File Offset: 0x000C9320
	public List<ObjOtherPlayer> ObjOtherPlayerInVisibleList
	{
		get
		{
			return this.mObjOtherPlayerInVisibleList;
		}
	}

	// Token: 0x06003399 RID: 13209 RVA: 0x000CB128 File Offset: 0x000C9328
	public void ResetObjManager()
	{
		this.mObjNpcPoolGroup = new ObjNpcPoolGroup();
		this.mObjNpcPoolGroup.Reset(GameSettingData.MaxNPCPoolGroupNum[GameSettingData.GetPhoneClass()], GameSettingData.MaxNPCPoolNum[GameSettingData.GetPhoneClass()]);
		this.mObjNpcPoolGroup.RegisterOnNpcRecycle(new ObjNpcPoolGroup.OnNpcRecycle(this.OnNpcRecycle));
		this.mOtherPlayerPool = new ObjOtherPlayerPool();
		this.mOtherPlayerPool.SetPoolMaxNum(GameSettingData.MaxOtherPlayerPoolNum[GameSettingData.GetPhoneClass()]);
		this.ResetObjSimpleAICarPoolGroup();
		this.ResetObjRagdollNPCPoolGroup();
		this.ResetObjPatrolNPCPoolGroup();
		this.ResetBombSustainedRangeObjPool();
		this.ResetMountCarPoolGroup();
		this.ResetObjFakeAICarPoolGroup();
	}

	// Token: 0x0600339A RID: 13210 RVA: 0x000CB1C0 File Offset: 0x000C93C0
	public void ResetPhoneClass()
	{
		this.RefreshPoolNum();
		this.ResetPlayerFashionEffect();
		this.ResetMainPlayerXRay();
		this.ResetPlayerShadow();
	}

	// Token: 0x0600339B RID: 13211 RVA: 0x000CB1DC File Offset: 0x000C93DC
	public void ResetMainPlayerXRay()
	{
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (GameSettingData.IsShowPlayerShadow[GameSettingData.GetPhoneClass()])
		{
			this.mMainPlayer.AddXRayMat();
		}
		else
		{
			this.mMainPlayer.RemoveXRayMat();
		}
	}

	// Token: 0x0600339C RID: 13212 RVA: 0x000CB21C File Offset: 0x000C941C
	public void ResetPlayerShadow()
	{
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (!GameSettingData.IsShowPlayerShadow[GameSettingData.GetPhoneClass()])
		{
			if (SingletonUnity<RealTimeShadow>.Exists)
			{
				SingletonUnity<RealTimeShadow>.Instance.DisableRealTimeShadow();
			}
			if (this.mMainPlayer.SimpleShadow == null)
			{
				this.mMainPlayer.InitSimpleShadow();
			}
		}
		else
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.mMainPlayer.PartObject.Length; i++)
			{
				if (this.mMainPlayer.PartObject[i] != null)
				{
					list.Add(this.mMainPlayer.PartObject[i]);
				}
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType != MAPTYPE.CAR_CHASE_COPY)
			{
				if (!SingletonUnity<RealTimeShadow>.Exists)
				{
					GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/PlayerRealTimeShadow") as GameObject;
					RealTimeShadow component = gameObject.GetComponent<RealTimeShadow>();
					component.Reset(list);
				}
				else
				{
					SingletonUnity<RealTimeShadow>.Instance.EnableRealTimeShadow();
					SingletonUnity<RealTimeShadow>.Instance.Reset(list);
				}
			}
			if (this.mMainPlayer.SimpleShadow != null)
			{
				ResourcesManager.UnLoadSimpleShadowPrefab(this.mMainPlayer.SimpleShadow.gameObject);
				this.mMainPlayer.SimpleShadow = null;
			}
		}
	}

	// Token: 0x0600339D RID: 13213 RVA: 0x000CB368 File Offset: 0x000C9568
	public void ResetPlayerFashionEffect()
	{
		if (GameSettingData.IsShowFashionEffect[GameSettingData.GetPhoneClass()])
		{
			for (int i = 0; i < this.mObjOtherPlayerVisibleList.Count; i++)
			{
				this.mObjOtherPlayerVisibleList[i].ReshowPartEffect();
			}
			if (this.mMainPlayer != null)
			{
				this.mMainPlayer.ReshowPartEffect();
			}
		}
		else
		{
			for (int j = 0; j < this.mObjOtherPlayerVisibleList.Count; j++)
			{
				this.mObjOtherPlayerVisibleList[j].ClearPartEffect();
			}
			if (this.mMainPlayer != null)
			{
				this.mMainPlayer.ClearPartEffect();
			}
		}
	}

	// Token: 0x0600339E RID: 13214 RVA: 0x000CB41C File Offset: 0x000C961C
	public void ClearPlayerFashionEffect()
	{
	}

	// Token: 0x0600339F RID: 13215 RVA: 0x000CB420 File Offset: 0x000C9620
	public void ReshowPlayerFashionEffect()
	{
	}

	// Token: 0x060033A0 RID: 13216 RVA: 0x000CB424 File Offset: 0x000C9624
	public void RefreshPoolNum()
	{
		this.mObjNpcPoolGroup.RefreshPool(GameSettingData.MaxNPCPoolGroupNum[GameSettingData.GetPhoneClass()], GameSettingData.MaxNPCPoolNum[GameSettingData.GetPhoneClass()]);
		this.mOtherPlayerPool.SetPoolMaxNum(GameSettingData.MaxOtherPlayerPoolNum[GameSettingData.GetPhoneClass()]);
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x000CB468 File Offset: 0x000C9668
	~ObjManager()
	{
		this.mObjNpcPoolGroup.DeRegisterOnNpcRecycle(new ObjNpcPoolGroup.OnNpcRecycle(this.OnNpcRecycle));
	}

	// Token: 0x060033A2 RID: 13218 RVA: 0x000CB4B4 File Offset: 0x000C96B4
	public bool IsMainPlayerPart(string bundleKey)
	{
		return SingletonDontDestoryUnity<GameManager>.Exists && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsInMainPlayerPartBundleIdList(bundleKey);
	}

	// Token: 0x060033A3 RID: 13219 RVA: 0x000CB4D4 File Offset: 0x000C96D4
	public void CreateMainPlayer(ObjInitPlayerData initData)
	{
		if (this.mMainPlayer != null)
		{
			return;
		}
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		initData.mPos = new Vector3(initData.mPos.x, SceneManager.GetHitHeight(initData.mPos), initData.mPos.z);
		PlayerData playerData = instance.PlayerData;
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/PlayerRoot") as GameObject;
		gameObject.name = "MainPlayer_" + initData.Camp.ToString();
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.mCharacterModelId);
		if (characterModelDataByID != null)
		{
			string path = string.Format("TestModel/Model/{0}/ModelRoot", initData.Profession);
			this.ReloadModel(gameObject.transform, path);
		}
		if (gameObject != null)
		{
			this.mMainPlayer = gameObject.GetComponent<ObjMainPlayer>();
			if (this.mMainPlayer == null)
			{
				this.mMainPlayer = gameObject.AddComponent<ObjMainPlayer>();
			}
		}
		this.mMainPlayer.UpdateWeaponModeID(initData.GetWeaponID());
		this.mMainPlayer.UpdateWeaponItemID(initData.WeaponItemId);
		this.mMainPlayer.InitInfo(characterModelDataByID);
		this.mMainPlayer.Init();
		this.mMainPlayer.ResetMainPlayer(initData);
		if (playerData.IsShowFashion)
		{
			if (playerData.CheckWeaponIsSame())
			{
				this.LoadPlayerVisual(this.mMainPlayer, playerData.FashionWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId);
				BundleManager.ClearMainPlayerBundleFlag(playerData.FashionWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, playerData.MountId);
			}
			else
			{
				this.LoadPlayerVisual(this.mMainPlayer, playerData.PartWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId);
				BundleManager.ClearMainPlayerBundleFlag(playerData.PartWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, playerData.MountId);
			}
		}
		else
		{
			this.LoadPlayerVisual(this.mMainPlayer, playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId);
			BundleManager.ClearMainPlayerBundleFlag(playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId, playerData.MountId);
		}
		this.AddDict(this.mMainPlayer.ServerId, this.mMainPlayer);
		this.AddToTargetCampList(this.mMainPlayer);
		if (instance.AutoSearchPath.IsAutoMovingFlag)
		{
			instance.MissionManager.ContinueAutoMoveToMission();
		}
		instance.PlayerCommonData.CheckTitleLevelFunction();
		SingletonUnity<UIManager>.Instance.ShowBaseUI();
		GameManager.IsSceneReady = true;
		SingletonUnity<MyEvent>.Instance.Fire("OnMainPlayerCreate", new object[0]);
	}

	// Token: 0x060033A4 RID: 13220 RVA: 0x000CB774 File Offset: 0x000C9974
	public void CreateMainPlayer(movement posInfo)
	{
		if (this.mMainPlayer != null)
		{
			return;
		}
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		PlayerData playerData = instance.PlayerData;
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/PlayerRoot") as GameObject;
		gameObject.name = "MainPlayer_" + playerData.PlayerCamp.ToString();
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(playerData.CharacterModelId);
		if (characterModelDataByID != null)
		{
			string path = string.Format("TestModel/Model/{0}/ModelRoot", playerData.Profession);
			this.ReloadModel(gameObject.transform, path);
		}
		if (gameObject != null)
		{
			this.mMainPlayer = gameObject.GetComponent<ObjMainPlayer>();
			if (this.mMainPlayer == null)
			{
				this.mMainPlayer = gameObject.AddComponent<ObjMainPlayer>();
			}
		}
		this.mMainPlayer.InitInfo(characterModelDataByID);
		this.mMainPlayer.UpdateWeaponModeID(playerData.PartWeaponId);
		this.mMainPlayer.UpdateWeaponItemID(playerData.WeaponItemId);
		this.mMainPlayer.Init();
		this.mMainPlayer.ResetMainPlayer(posInfo);
		if (playerData.IsShowFashion)
		{
			if (playerData.CheckWeaponIsSame())
			{
				this.LoadPlayerVisual(this.mMainPlayer, playerData.FashionWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId);
				BundleManager.ClearMainPlayerBundleFlag(playerData.FashionWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, playerData.MountId);
			}
			else
			{
				this.LoadPlayerVisual(this.mMainPlayer, playerData.PartWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId);
				BundleManager.ClearMainPlayerBundleFlag(playerData.PartWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, playerData.MountId);
			}
		}
		else
		{
			this.LoadPlayerVisual(this.mMainPlayer, playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId);
			BundleManager.ClearMainPlayerBundleFlag(playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId, playerData.MountId);
		}
		this.AddDict(this.mMainPlayer.ServerId, this.mMainPlayer);
		this.AddToTargetCampList(this.mMainPlayer);
		if (instance.AutoSearchPath.IsAutoMovingFlag)
		{
			instance.MissionManager.ContinueAutoMoveToMission();
		}
		instance.PlayerCommonData.CheckTitleLevelFunction();
		SingletonUnity<UIManager>.Instance.ShowBaseUI();
		GameManager.IsSceneReady = true;
		SingletonUnity<MyEvent>.Instance.Fire("OnMainPlayerCreate", new object[0]);
	}

	// Token: 0x060033A5 RID: 13221 RVA: 0x000CB9E8 File Offset: 0x000C9BE8
	public void LoadPlayerVisual(ObjOtherPlayer player, string partWeaponId, string partHeadId, string partBodyId, string partLegId)
	{
		if (this.mPlayerMeshLoadNumDic.ContainsKey(player.ServerId))
		{
			this.mPlayerMeshLoadNumDic[player.ServerId] = 0;
		}
		else
		{
			this.mPlayerMeshLoadNumDic.Add(player.ServerId, 0);
		}
		if (this.mPlayerMeshLoadTargetNumDic.ContainsKey(player.ServerId))
		{
			this.mPlayerMeshLoadTargetNumDic[player.ServerId] = 0;
		}
		else
		{
			this.mPlayerMeshLoadTargetNumDic.Add(player.ServerId, 0);
		}
		bool flag;
		if (!partLegId.Equals(player.TargetPartObjId[3]))
		{
			player.SetTargetPartObjId(MODEL_TYPE.LEG, partLegId);
			if (!partLegId.Equals(player.PartObjId[3]))
			{
				Dictionary<long, int> dictionary2;
				Dictionary<long, int> dictionary = dictionary2 = this.mPlayerMeshLoadTargetNumDic;
				long serverId;
				long num = serverId = player.ServerId;
				int num2 = dictionary2[serverId];
				dictionary[num] = num2 + 1;
				if (player.LoadingModelData[3] != null)
				{
					player.LoadingModelData[3].OnLoadFinished = null;
					player.LoadingModelData[3] = null;
				}
				flag = true;
			}
			else
			{
				if (player.LoadingModelData[3] != null)
				{
					player.LoadingModelData[3].OnLoadFinished = null;
					player.LoadingModelData[3] = null;
				}
				flag = false;
			}
		}
		else if (!partLegId.Equals(player.PartObjId[3]))
		{
			Dictionary<long, int> dictionary4;
			Dictionary<long, int> dictionary3 = dictionary4 = this.mPlayerMeshLoadTargetNumDic;
			long serverId;
			long num3 = serverId = player.ServerId;
			int num2 = dictionary4[serverId];
			dictionary3[num3] = num2 + 1;
			flag = (player.LoadingModelData[3] == null);
		}
		else
		{
			flag = false;
		}
		bool flag2;
		if (!partHeadId.Equals(player.TargetPartObjId[1]))
		{
			player.SetTargetPartObjId(MODEL_TYPE.HEAD, partHeadId);
			if (!partHeadId.Equals(player.PartObjId[1]))
			{
				Dictionary<long, int> dictionary6;
				Dictionary<long, int> dictionary5 = dictionary6 = this.mPlayerMeshLoadTargetNumDic;
				long serverId;
				long num4 = serverId = player.ServerId;
				int num2 = dictionary6[serverId];
				dictionary5[num4] = num2 + 1;
				if (player.LoadingModelData[1] != null)
				{
					player.LoadingModelData[1].OnLoadFinished = null;
					player.LoadingModelData[1] = null;
				}
				flag2 = true;
			}
			else
			{
				if (player.LoadingModelData[1] != null)
				{
					player.LoadingModelData[1].OnLoadFinished = null;
					player.LoadingModelData[1] = null;
				}
				flag2 = false;
			}
		}
		else if (!partHeadId.Equals(player.PartObjId[1]))
		{
			Dictionary<long, int> dictionary8;
			Dictionary<long, int> dictionary7 = dictionary8 = this.mPlayerMeshLoadTargetNumDic;
			long serverId;
			long num5 = serverId = player.ServerId;
			int num2 = dictionary8[serverId];
			dictionary7[num5] = num2 + 1;
			flag2 = (player.LoadingModelData[1] == null);
		}
		else
		{
			flag2 = false;
		}
		bool flag3;
		if (!partBodyId.Equals(player.TargetPartObjId[2]))
		{
			player.SetTargetPartObjId(MODEL_TYPE.BODY, partBodyId);
			if (!partBodyId.Equals(player.PartObjId[2]))
			{
				Dictionary<long, int> dictionary10;
				Dictionary<long, int> dictionary9 = dictionary10 = this.mPlayerMeshLoadTargetNumDic;
				long serverId;
				long num6 = serverId = player.ServerId;
				int num2 = dictionary10[serverId];
				dictionary9[num6] = num2 + 1;
				if (player.LoadingModelData[2] != null)
				{
					player.LoadingModelData[2].OnLoadFinished = null;
					player.LoadingModelData[2] = null;
				}
				flag3 = true;
			}
			else
			{
				if (player.LoadingModelData[2] != null)
				{
					player.LoadingModelData[2].OnLoadFinished = null;
					player.LoadingModelData[2] = null;
				}
				flag3 = false;
			}
		}
		else if (!partBodyId.Equals(player.PartObjId[2]))
		{
			Dictionary<long, int> dictionary12;
			Dictionary<long, int> dictionary11 = dictionary12 = this.mPlayerMeshLoadTargetNumDic;
			long serverId;
			long num7 = serverId = player.ServerId;
			int num2 = dictionary12[serverId];
			dictionary11[num7] = num2 + 1;
			flag3 = (player.LoadingModelData[2] == null);
		}
		else
		{
			flag3 = false;
		}
		bool flag4;
		if (!partWeaponId.Equals(player.TargetPartObjId[0]))
		{
			player.SetTargetPartObjId(MODEL_TYPE.WEAPON, partWeaponId);
			if (!partWeaponId.Equals(player.PartObjId[0]))
			{
				Dictionary<long, int> dictionary14;
				Dictionary<long, int> dictionary13 = dictionary14 = this.mPlayerMeshLoadTargetNumDic;
				long serverId;
				long num8 = serverId = player.ServerId;
				int num2 = dictionary14[serverId];
				dictionary13[num8] = num2 + 1;
				if (player.LoadingModelData[0] != null)
				{
					player.LoadingModelData[0].OnLoadFinished = null;
					player.LoadingModelData[0] = null;
				}
				flag4 = true;
			}
			else
			{
				if (player.LoadingModelData[0] != null)
				{
					player.LoadingModelData[0].OnLoadFinished = null;
					player.LoadingModelData[0] = null;
				}
				flag4 = false;
			}
		}
		else if (!partWeaponId.Equals(player.PartObjId[0]))
		{
			Dictionary<long, int> dictionary16;
			Dictionary<long, int> dictionary15 = dictionary16 = this.mPlayerMeshLoadTargetNumDic;
			long serverId;
			long num9 = serverId = player.ServerId;
			int num2 = dictionary16[serverId];
			dictionary15[num9] = num2 + 1;
			flag4 = (player.LoadingModelData[0] == null);
		}
		else
		{
			flag4 = false;
		}
		if (this.mPlayerMeshLoadTargetNumDic[player.ServerId] != 0)
		{
			player.AnimationLogic.AnimaObj.cullingType = 0;
		}
		bool isNeedUnload = player.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || GameSettingData.IsBundleNeedUnload[GameSettingData.GetPhoneClass()];
		ModelData modeDataByID = DataManager.GetModeDataByID(partLegId);
		if (flag)
		{
			if (modeDataByID != null)
			{
				player.LoadingModelData[3] = BundleManager.LoadModelInList(modeDataByID.Name, isNeedUnload, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), modeDataByID, player, modeDataByID.ModelPath);
				this.CheckModelEffect(modeDataByID, player);
				player.LoadingModelDataId[3] = ((player.LoadingModelData[3] != null) ? player.LoadingModelData[3].ID : -1L);
			}
		}
		else
		{
			this.CheckModelEffect(modeDataByID, player);
		}
		modeDataByID = DataManager.GetModeDataByID(partWeaponId);
		if (flag4)
		{
			if (modeDataByID != null)
			{
				player.LoadingModelData[0] = BundleManager.LoadModelInList(modeDataByID.Name, isNeedUnload, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), modeDataByID, player, modeDataByID.ModelPath);
				this.CheckModelEffect(modeDataByID, player);
				player.LoadingModelDataId[0] = ((player.LoadingModelData[0] != null) ? player.LoadingModelData[0].ID : -1L);
			}
		}
		else
		{
			this.CheckModelEffect(modeDataByID, player);
		}
		modeDataByID = DataManager.GetModeDataByID(partHeadId);
		if (flag2)
		{
			if (modeDataByID != null)
			{
				player.LoadingModelData[1] = BundleManager.LoadModelInList(modeDataByID.Name, isNeedUnload, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), modeDataByID, player, modeDataByID.ModelPath);
				this.CheckModelEffect(modeDataByID, player);
				player.LoadingModelDataId[1] = ((player.LoadingModelData[1] != null) ? player.LoadingModelData[1].ID : -1L);
			}
		}
		else
		{
			this.CheckModelEffect(modeDataByID, player);
		}
		modeDataByID = DataManager.GetModeDataByID(partBodyId);
		if (flag3)
		{
			if (modeDataByID != null)
			{
				player.LoadingModelData[2] = BundleManager.LoadModelInList(modeDataByID.Name, isNeedUnload, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), modeDataByID, player, modeDataByID.ModelPath);
				this.CheckModelEffect(modeDataByID, player);
				player.LoadingModelDataId[2] = ((player.LoadingModelData[2] != null) ? player.LoadingModelData[2].ID : -1L);
			}
		}
		else
		{
			this.CheckModelEffect(modeDataByID, player);
		}
	}

	// Token: 0x060033A6 RID: 13222 RVA: 0x000CC0E8 File Offset: 0x000CA2E8
	public static string GetWeaponId(characterVisual visual)
	{
		if (visual.showType != 1L || !visual.HasFashion_WeaponId)
		{
			return visual.WeaponId;
		}
		if (ObjManager.CheckWeaponIsSame(visual))
		{
			return visual.Fashion_WeaponId;
		}
		return visual.WeaponId;
	}

	// Token: 0x060033A7 RID: 13223 RVA: 0x000CC12C File Offset: 0x000CA32C
	public static bool CheckWeaponIsSame(characterVisual visual)
	{
		if (visual.HasWeaponItemId && visual.HasFashionItemId)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(visual.WeaponItemId);
			EquipData equipDataById2 = DataManager.GetEquipDataById(visual.FashionItemId);
			return equipDataById.WeaponType == equipDataById2.WeaponType;
		}
		return !visual.HasWeaponId || !visual.HasFashion_WeaponId || GameDefine.GetWeaponName(visual.WeaponId).Equals(GameDefine.GetWeaponName(visual.Fashion_WeaponId));
	}

	// Token: 0x060033A8 RID: 13224 RVA: 0x000CC1B4 File Offset: 0x000CA3B4
	public static string GetHeadId(characterVisual visual)
	{
		if (visual.showType == 1L && visual.HasFashion_HeadId)
		{
			return visual.Fashion_HeadId;
		}
		return visual.HeadId;
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x000CC1E8 File Offset: 0x000CA3E8
	public static string GetBodyId(characterVisual visual)
	{
		if (visual.showType == 1L && visual.HasFashion_BodyId)
		{
			return visual.Fashion_BodyId;
		}
		return visual.BodyId;
	}

	// Token: 0x060033AA RID: 13226 RVA: 0x000CC21C File Offset: 0x000CA41C
	public static string GetLegId(characterVisual visual)
	{
		if (visual.showType == 1L && visual.HasFashion_LegId)
		{
			return visual.Fashion_LegId;
		}
		return visual.LegId;
	}

	// Token: 0x060033AB RID: 13227 RVA: 0x000CC250 File Offset: 0x000CA450
	public void OnLoadPlayerPartFinished(object objBundle, object param1 = null, object param2 = null)
	{
		ObjOtherPlayer objOtherPlayer = param2 as ObjOtherPlayer;
		if (objOtherPlayer == null)
		{
			return;
		}
		ModelData modelData = param1 as ModelData;
		GameObject gameObject = objBundle as GameObject;
		Material material = null;
		if (objOtherPlayer.PartObject[(int)modelData.MODELTYPE] != null)
		{
			Object.Destroy(objOtherPlayer.PartObject[modelData.ModelType]);
			objOtherPlayer.PartObject[modelData.ModelType] = null;
			BundleManager.UnloadModel(objOtherPlayer.PartObjId[modelData.ModelType], -1L, objOtherPlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER);
		}
		objOtherPlayer.PartObject[modelData.ModelType] = gameObject;
		objOtherPlayer.PartObjId[modelData.ModelType] = modelData.ID;
		objOtherPlayer.LoadingModelData[modelData.ModelType] = null;
		objOtherPlayer.LoadingModelDataId[modelData.ModelType] = -1L;
		gameObject.name = modelData.MODELTYPE.ToString();
		gameObject.transform.parent = objOtherPlayer.AnimationLogic.AnimaObj.transform;
		gameObject.transform.localPosition = Vector3.zero;
		BundleManager.RebuildBones(objOtherPlayer, gameObject);
		BundleManager.ResetShader(gameObject.transform, out material);
		if (material.HasProperty("_DefaultColor"))
		{
			Color color = material.GetColor("_DefaultColor");
			material.SetColor("_Color", new Color(color.r, color.g, color.b, 0f));
			ShortcutExtensions.DOColor(material, color, 1.5f);
		}
		objOtherPlayer.PartMatList[modelData.ModelType] = material;
		if (UnityVersionUtil.IsActive(objOtherPlayer.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, false);
		}
		if (objOtherPlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCarScene())
			{
				ObjManager.AddOutLineMaterial(gameObject, objOtherPlayer as ObjMainPlayer);
			}
			gameObject.layer = 31;
		}
		Dictionary<long, int> dictionary2;
		Dictionary<long, int> dictionary = dictionary2 = this.mPlayerMeshLoadNumDic;
		long serverId;
		long num = serverId = objOtherPlayer.ServerId;
		int num2 = dictionary2[serverId];
		dictionary[num] = num2 + 1;
		if (this.mPlayerMeshLoadNumDic[objOtherPlayer.ServerId] == this.mPlayerMeshLoadTargetNumDic[objOtherPlayer.ServerId])
		{
			if (objOtherPlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
			{
				SingletonUnity<MyEvent>.Instance.Fire("OnMainPlayerMeshLoadDone", new object[0]);
				if (GameSettingData.IsShowPlayerShadow[GameSettingData.GetPhoneClass()])
				{
					List<GameObject> list = new List<GameObject>();
					for (int i = 0; i < objOtherPlayer.PartObject.Length; i++)
					{
						if (objOtherPlayer.PartObject[i] != null)
						{
							list.Add(objOtherPlayer.PartObject[i]);
						}
					}
					if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType != MAPTYPE.CAR_CHASE_COPY)
					{
						if (!SingletonUnity<RealTimeShadow>.Exists)
						{
							GameObject gameObject2 = ResourcesManager.LoadAndInstantiate("Items/PlayerRealTimeShadow") as GameObject;
							RealTimeShadow component = gameObject2.GetComponent<RealTimeShadow>();
							component.Reset(list);
						}
						else
						{
							SingletonUnity<RealTimeShadow>.Instance.Reset(list);
						}
					}
				}
			}
			this.mPlayerMeshLoadNumDic.Remove(objOtherPlayer.ServerId);
			this.mPlayerMeshLoadTargetNumDic.Remove(objOtherPlayer.ServerId);
		}
	}

	// Token: 0x060033AC RID: 13228 RVA: 0x000CC578 File Offset: 0x000CA778
	public static void AddOutLineMaterial(GameObject parentObj, ObjMainPlayer mainPlayer)
	{
		if (mainPlayer.IsServerRidingMount || mainPlayer.IsDrivingMount() || !GameSettingData.IsShowPlayerShadow[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in parentObj.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			Material[] array = new Material[skinnedMeshRenderer.materials.Length + 1];
			for (int j = 0; j < skinnedMeshRenderer.materials.Length; j++)
			{
				if (skinnedMeshRenderer.materials[j].name.Contains("XRay"))
				{
					return;
				}
				array[j] = skinnedMeshRenderer.materials[j];
			}
			array[skinnedMeshRenderer.materials.Length] = mainPlayer.XRayMat;
			skinnedMeshRenderer.materials = array;
		}
	}

	// Token: 0x060033AD RID: 13229 RVA: 0x000CC63C File Offset: 0x000CA83C
	public void CheckModelEffect(ModelData modelData, ObjOtherPlayer player)
	{
		if (!GameSettingData.IsShowFashionEffect[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		if (!string.IsNullOrEmpty(modelData.EffectId))
		{
			List<FxEffInfoData> fxEffInfoDataListById = DataManager.GetFxEffInfoDataListById(modelData.EffectId);
			for (int i = 0; i < fxEffInfoDataListById.Count; i++)
			{
				ModelEffectData modelEffectData = new ModelEffectData(modelData, fxEffInfoDataListById[i], i == 0, string.Empty, -1);
				string path = string.Format("{0}/{1}", fxEffInfoDataListById[i].EffFilePath, fxEffInfoDataListById[i].EffName);
				GameObject gameObject = ResourcesManager.LoadAndInstantiate(path) as GameObject;
				List<GameObject> list = player.PartEffectList[modelData.ModelType];
				if (list == null)
				{
					list = new List<GameObject>();
					player.PartEffectList[modelData.ModelType] = list;
				}
				else if (i == 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						Object.Destroy(list[j]);
					}
					list.Clear();
				}
				if (gameObject != null)
				{
					list.Add(gameObject);
					gameObject.transform.parent = TransformUtil.FindChildTransform(player.CacheTransform, modelEffectData.fxEffinfoData.EffLinkNode, false);
					gameObject.transform.localPosition = modelEffectData.fxEffinfoData.Position;
					gameObject.transform.localEulerAngles = modelEffectData.fxEffinfoData.Angel;
				}
				else
				{
					BundleManager.LoadEffectInList(fxEffInfoDataListById[i].EffName, false, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartEffectFinished), modelEffectData, player);
				}
			}
		}
		else
		{
			List<GameObject> list2 = player.PartEffectList[modelData.ModelType];
			if (list2 != null)
			{
				for (int k = 0; k < list2.Count; k++)
				{
					Object.Destroy(list2[k]);
				}
				list2.Clear();
			}
		}
	}

	// Token: 0x060033AE RID: 13230 RVA: 0x000CC818 File Offset: 0x000CAA18
	public void OnLoadPlayerPartEffectFinished(object objBundle, object param1 = null, object param2 = null)
	{
		ObjOtherPlayer objOtherPlayer = param2 as ObjOtherPlayer;
		if (objOtherPlayer == null)
		{
			return;
		}
		ModelEffectData modelEffectData = param1 as ModelEffectData;
		GameObject gameObject = objBundle as GameObject;
		if (objOtherPlayer.TargetPartObjId[modelEffectData.modelData.ModelType].Equals(modelEffectData.modelData.ID))
		{
			List<GameObject> list = objOtherPlayer.PartEffectList[modelEffectData.modelData.ModelType];
			list.Add(gameObject);
			gameObject.transform.parent = TransformUtil.FindChildTransform(objOtherPlayer.CacheTransform, modelEffectData.fxEffinfoData.EffLinkNode, false);
			gameObject.transform.localPosition = modelEffectData.fxEffinfoData.Position;
			gameObject.transform.localEulerAngles = modelEffectData.fxEffinfoData.Angel;
			if (UnityVersionUtil.IsActive(objOtherPlayer.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, false);
			}
		}
		else
		{
			Object.Destroy(gameObject);
		}
	}

	// Token: 0x060033AF RID: 13231 RVA: 0x000CC910 File Offset: 0x000CAB10
	public void CreateZombiePlayer(ObjInitPlayerData initData)
	{
		initData.mPos = new Vector3(initData.mPos.x, SceneManager.GetHitHeight(initData.mPos), initData.mPos.z);
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/PlayerRoot") as GameObject;
		gameObject.name = string.Format("ZombiePlayer{0}", initData.mServerID);
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.mCharacterModelId);
		if (characterModelDataByID != null)
		{
			string path = string.Format("TestModel/Model/{0}/ModelRoot", initData.Profession);
			this.ReloadModel(gameObject.transform, path);
		}
		if (gameObject != null)
		{
			ObjZombiePlayer objZombiePlayer = gameObject.GetComponent<ObjZombiePlayer>();
			if (objZombiePlayer == null)
			{
				objZombiePlayer = gameObject.AddComponent<ObjZombiePlayer>();
			}
			objZombiePlayer.InitInfo(characterModelDataByID);
			objZombiePlayer.UpdateWeaponModeID(initData.GetWeaponID());
			objZombiePlayer.UpdateWeaponItemID(initData.WeaponItemId);
			objZombiePlayer.Init();
			objZombiePlayer.ResetZombiePlayer(initData);
			this.AddDict(objZombiePlayer.ServerId, objZombiePlayer);
			this.AddToTargetCampList(objZombiePlayer);
			if (this.CheckOtherPlayerVisiable(objZombiePlayer))
			{
				this.LoadPlayerVisual(objZombiePlayer, initData.GetWeaponID(), initData.HeadId, initData.BodyId, initData.LegId);
			}
		}
	}

	// Token: 0x060033B0 RID: 13232 RVA: 0x000CCA40 File Offset: 0x000CAC40
	public void RecycleZombiePlayer(ObjZombiePlayer zombiePlayer)
	{
		this.RemoveObj(zombiePlayer);
		this.RemoveFromTargetCampList(zombiePlayer);
		if (this.mObjOtherPlayerVisibleList.Contains(zombiePlayer))
		{
			this.mObjOtherPlayerVisibleList.Remove(zombiePlayer);
		}
		if (this.mObjOtherPlayerInVisibleList.Contains(zombiePlayer))
		{
			this.mObjOtherPlayerInVisibleList.Remove(zombiePlayer);
		}
		this.UpdateInVisibleOtherPlayer();
		Object.Destroy(zombiePlayer.gameObject);
	}

	// Token: 0x060033B1 RID: 13233 RVA: 0x000CCAA8 File Offset: 0x000CACA8
	public ObjZombieRagdollPlayer CreateZombieRagdollPlayer(ObjInitPlayerData initData)
	{
		initData.mPos = new Vector3(initData.mPos.x, SceneManager.GetHitHeight(initData.mPos), initData.mPos.z);
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/PlayerRoot") as GameObject;
		gameObject.name = string.Format("ZombiePlayer{0}", initData.mServerID);
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.mCharacterModelId);
		if (characterModelDataByID != null)
		{
			string path = string.Format("TestModel/Model/{0}/ModelRoot", initData.Profession);
			this.ReloadModel(gameObject.transform, path);
		}
		if (gameObject != null)
		{
			ObjZombieRagdollPlayer objZombieRagdollPlayer = gameObject.GetComponent<ObjZombieRagdollPlayer>();
			if (objZombieRagdollPlayer == null)
			{
				objZombieRagdollPlayer = gameObject.AddComponent<ObjZombieRagdollPlayer>();
			}
			objZombieRagdollPlayer.InitInfo(characterModelDataByID);
			objZombieRagdollPlayer.UpdateWeaponModeID(initData.GetWeaponID());
			objZombieRagdollPlayer.UpdateWeaponItemID(initData.WeaponItemId);
			objZombieRagdollPlayer.Init();
			objZombieRagdollPlayer.ResetObjZombieRagdollPlayer(initData);
			this.AddDict(objZombieRagdollPlayer.ServerId, objZombieRagdollPlayer);
			this.AddToTargetCampList(objZombieRagdollPlayer);
			if (this.CheckOtherPlayerVisiable(objZombieRagdollPlayer))
			{
				this.LoadPlayerVisual(objZombieRagdollPlayer, initData.GetWeaponID(), initData.HeadId, initData.BodyId, initData.LegId);
			}
			ObjZombieRagdollPlayer.InitRagdollObj(gameObject.transform.GetChild(0), objZombieRagdollPlayer);
			if (objZombieRagdollPlayer.IsMissionNpc)
			{
				GameObject gameObject2 = ResourcesManager.LoadAndInstantiate("Items/qiGan") as GameObject;
				gameObject2.transform.position = objZombieRagdollPlayer.transform.position;
				gameObject2.transform.rotation = objZombieRagdollPlayer.transform.rotation;
				gameObject2.transform.localScale = Vector3.one;
				objZombieRagdollPlayer.FlagObj = gameObject2;
			}
			return objZombieRagdollPlayer;
		}
		return null;
	}

	// Token: 0x060033B2 RID: 13234 RVA: 0x000CCC50 File Offset: 0x000CAE50
	public void RecycleZombieRagdollPlayer(ObjZombieRagdollPlayer zombiePlayer)
	{
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.OnRecycleZombiePlayer(zombiePlayer);
		}
		this.RemoveObj(zombiePlayer);
		this.RemoveFromTargetCampList(zombiePlayer);
		if (this.mObjOtherPlayerVisibleList.Contains(zombiePlayer))
		{
			this.mObjOtherPlayerVisibleList.Remove(zombiePlayer);
		}
		if (this.mObjOtherPlayerInVisibleList.Contains(zombiePlayer))
		{
			this.mObjOtherPlayerInVisibleList.Remove(zombiePlayer);
		}
		this.UpdateInVisibleOtherPlayer();
		if (zombiePlayer.FlagObj != null)
		{
			Object.Destroy(zombiePlayer.FlagObj);
		}
		Object.Destroy(zombiePlayer.gameObject);
	}

	// Token: 0x060033B3 RID: 13235 RVA: 0x000CCCEC File Offset: 0x000CAEEC
	private int GetPlayerCount()
	{
		return this.mCampList[0].Count + this.mCampList[1].Count;
	}

	// Token: 0x060033B4 RID: 13236 RVA: 0x000CCD0C File Offset: 0x000CAF0C
	public void CreateOtherPlayer(ObjInitPlayerData initData)
	{
		initData.mPos = new Vector3(initData.mPos.x, SceneManager.GetHitHeight(initData.mPos), initData.mPos.z);
		if (this.GetPlayerCount() <= GameSettingData.MaxOtherPlayerLogicNum[GameSettingData.GetPhoneClass()])
		{
			ObjOtherPlayer objOtherPlayer = this.mOtherPlayerPool.GetOtherPlayer(initData);
			if (objOtherPlayer != null)
			{
				bool flag = this.CheckOtherPlayerVisiable(objOtherPlayer);
				initData.SetVisible(flag);
				UnityVersionUtil.SetActiveRecursive(objOtherPlayer.gameObject, true);
				objOtherPlayer.UpdateWeaponModeID(initData.GetWeaponID());
				objOtherPlayer.UpdateWeaponItemID(initData.WeaponItemId);
				objOtherPlayer.ResetOtherPlayer(initData);
				this.AddDict(objOtherPlayer.ServerId, objOtherPlayer);
				this.AddToTargetCampList(objOtherPlayer);
				if (flag)
				{
					this.LoadPlayerVisual(objOtherPlayer, initData.GetWeaponID(), initData.HeadId, initData.BodyId, initData.LegId);
				}
				return;
			}
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/OtherPlayerRoot") as GameObject;
			gameObject.name = string.Format("OtherPlayer{0}", initData.mServerID);
			CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.mCharacterModelId);
			if (characterModelDataByID != null)
			{
				string path = string.Format("TestModel/Model/{0}/ModelRoot", initData.Profession);
				this.ReloadModel(gameObject.transform, path);
			}
			if (gameObject != null)
			{
				objOtherPlayer = gameObject.GetComponent<ObjOtherPlayer>();
				if (objOtherPlayer == null)
				{
					objOtherPlayer = gameObject.AddComponent<ObjOtherPlayer>();
				}
				bool flag2 = this.CheckOtherPlayerVisiable(objOtherPlayer);
				initData.SetVisible(flag2);
				objOtherPlayer.InitInfo(characterModelDataByID);
				objOtherPlayer.UpdateWeaponModeID(initData.GetWeaponID());
				objOtherPlayer.UpdateWeaponItemID(initData.WeaponItemId);
				objOtherPlayer.Init();
				objOtherPlayer.ResetOtherPlayer(initData);
				this.AddDict(objOtherPlayer.ServerId, objOtherPlayer);
				this.AddToTargetCampList(objOtherPlayer);
				if (flag2)
				{
					this.LoadPlayerVisual(objOtherPlayer, initData.GetWeaponID(), initData.HeadId, initData.BodyId, initData.LegId);
				}
				else
				{
					objOtherPlayer.SetTargetPartObjId(MODEL_TYPE.WEAPON, initData.GetWeaponID());
					objOtherPlayer.SetTargetPartObjId(MODEL_TYPE.HEAD, initData.HeadId);
					objOtherPlayer.SetTargetPartObjId(MODEL_TYPE.BODY, initData.BodyId);
					objOtherPlayer.SetTargetPartObjId(MODEL_TYPE.LEG, initData.LegId);
				}
			}
			else
			{
				Debug.Log("Create Other Player Error");
			}
		}
		else
		{
			if (this.mOtherPlayerNoLogicDataDic.ContainsKey(initData.mServerID))
			{
				this.mOtherPlayerNoLogicDataDic[initData.mServerID] = initData;
			}
			else
			{
				this.mOtherPlayerNoLogicDataDic.Add(initData.mServerID, initData);
			}
			update_client_state.request request = new update_client_state.request();
			request.id = initData.mServerID;
			request.state = 0L;
			NetLogic.GetInstance().Send<Protocol.update_client_state>(request, null);
		}
	}

	// Token: 0x060033B5 RID: 13237 RVA: 0x000CCFA0 File Offset: 0x000CB1A0
	public void RecycleOtherPlayer(ObjOtherPlayer otherPlayer)
	{
		otherPlayer.DisMountCar();
		otherPlayer.StopDance();
		this.RemoveObj(otherPlayer);
		this.RemoveFromTargetCampList(otherPlayer);
		otherPlayer.IsDie = true;
		if (this.mObjOtherPlayerVisibleList.Contains(otherPlayer))
		{
			this.mObjOtherPlayerVisibleList.Remove(otherPlayer);
		}
		if (this.mObjOtherPlayerInVisibleList.Contains(otherPlayer))
		{
			this.mObjOtherPlayerInVisibleList.Remove(otherPlayer);
		}
		this.UpdateInVisibleOtherPlayer();
		this.mOtherPlayerPool.RecycleOtherPlayer(otherPlayer);
		this.UpdateNoLogicOtherPlayer();
	}

	// Token: 0x060033B6 RID: 13238 RVA: 0x000CD024 File Offset: 0x000CB224
	private void UpdateNoLogicOtherPlayer()
	{
		if (this.mOtherPlayerNoLogicDataDic.Count > 0 && this.GetPlayerCount() <= GameSettingData.MaxOtherPlayerLogicNum[GameSettingData.GetPhoneClass()])
		{
			ObjInitPlayerData objInitPlayerData = null;
			List<ObjInitPlayerData> list = new List<ObjInitPlayerData>(this.mOtherPlayerNoLogicDataDic.Values);
			float maxValue = float.MaxValue;
			for (int i = 0; i < list.Count; i++)
			{
				float num = VectorXZ.Distance(this.MainPlayer.Position, list[i].mPos);
				if (num < maxValue)
				{
					objInitPlayerData = list[i];
				}
			}
			this.RemoveNoLogicOtherPlayerData(objInitPlayerData.mServerID);
			this.CreateOtherPlayer(objInitPlayerData);
		}
	}

	// Token: 0x060033B7 RID: 13239 RVA: 0x000CD0E0 File Offset: 0x000CB2E0
	public bool CheckOtherPlayerVisiable(ObjOtherPlayer otherPlayer)
	{
		if (this.mObjOtherPlayerVisibleList.Count >= GameSettingData.MaxOtherPlayerVisibleNum[GameSettingData.GetPhoneClass()])
		{
			this.mObjOtherPlayerInVisibleList.Add(otherPlayer);
			return false;
		}
		this.mObjOtherPlayerVisibleList.Add(otherPlayer);
		return true;
	}

	// Token: 0x060033B8 RID: 13240 RVA: 0x000CD124 File Offset: 0x000CB324
	public void UpdateInVisibleOtherPlayer()
	{
		if (this.mObjOtherPlayerVisibleList.Count < GameSettingData.MaxOtherPlayerVisibleNum[GameSettingData.GetPhoneClass()])
		{
			int num = GameSettingData.MaxOtherPlayerVisibleNum[GameSettingData.GetPhoneClass()] - this.mObjOtherPlayerVisibleList.Count;
			num = ((num <= this.mObjOtherPlayerInVisibleList.Count) ? num : this.mObjOtherPlayerInVisibleList.Count);
			List<long> list = new List<long>();
			if (this.mMainPlayer != null && this.mMainPlayer.TeamId != -1L)
			{
				Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
				for (int i = 0; i < teamInfo.TeamMembers.Length; i++)
				{
					if (teamInfo.TeamMembers[i].IsValid() && teamInfo.TeamMembers[i].ServerId != PlayerData.MainPlayerServerId)
					{
						list.Add(teamInfo.TeamMembers[i].ServerId);
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				ObjOtherPlayer objOtherPlayer = null;
				if (list.Count > 0)
				{
					for (int k = 0; k < this.mObjOtherPlayerInVisibleList.Count; k++)
					{
						if (list.Contains(this.mObjOtherPlayerInVisibleList[k].ServerId))
						{
							list.Remove(this.mObjOtherPlayerInVisibleList[k].ServerId);
							objOtherPlayer = this.mObjOtherPlayerInVisibleList[k];
							this.mObjOtherPlayerInVisibleList.Remove(objOtherPlayer);
							break;
						}
					}
				}
				if (objOtherPlayer == null)
				{
					objOtherPlayer = this.mObjOtherPlayerInVisibleList[0];
					this.mObjOtherPlayerInVisibleList.RemoveAt(0);
				}
				this.mObjOtherPlayerVisibleList.Add(objOtherPlayer);
				objOtherPlayer.UpdateWeaponModeID(objOtherPlayer.TargetPartObjId[0]);
				objOtherPlayer.SetVisible(true);
				if (objOtherPlayer.PartObject[1] == null)
				{
					this.LoadPlayerVisual(objOtherPlayer, objOtherPlayer.TargetPartObjId[0], objOtherPlayer.TargetPartObjId[1], objOtherPlayer.TargetPartObjId[2], objOtherPlayer.TargetPartObjId[3]);
				}
			}
		}
	}

	// Token: 0x060033B9 RID: 13241 RVA: 0x000CD340 File Offset: 0x000CB540
	public GameObject ReloadModel(Transform root, string path)
	{
		GameObject gameObject = ResourcesManager.LoadAndInstantiate(path) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.parent = root;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.name = "ModelRoot";
		return gameObject;
	}

	// Token: 0x060033BA RID: 13242 RVA: 0x000CD3A0 File Offset: 0x000CB5A0
	public Obj FindObj(long id)
	{
		if (this.mObjDict.ContainsKey(id))
		{
			return this.mObjDict[id];
		}
		return null;
	}

	// Token: 0x060033BB RID: 13243 RVA: 0x000CD3C4 File Offset: 0x000CB5C4
	public ObjCharacter FindObjInScene(long id)
	{
		if (this.mObjDict.ContainsKey(id))
		{
			return this.mObjDict[id] as ObjCharacter;
		}
		return null;
	}

	// Token: 0x060033BC RID: 13244 RVA: 0x000CD3F8 File Offset: 0x000CB5F8
	public ObjNPC FindMissionNpcInScene(string npcId)
	{
		List<ObjNPC> enableNpcList = this.mObjNpcPoolGroup.EnableNpcList;
		int i = 0;
		while (i < enableNpcList.Count)
		{
			if (enableNpcList[i].NPCDataID.Equals(npcId))
			{
				if (enableNpcList[i].AttributeData.Camp == GameDefine.CAMP_TYPE.FUNCTION_NPC)
				{
					return enableNpcList[i];
				}
				return null;
			}
			else
			{
				i++;
			}
		}
		List<ObjRagdollNPC> enableList = this.mObjRagdollNPCPoolGroup.EnableList;
		int j = 0;
		while (j < enableList.Count)
		{
			if (enableList[j].NPCData.ID.Equals(npcId))
			{
				if (enableList[j].AttributeData.Camp == GameDefine.CAMP_TYPE.FUNCTION_NPC)
				{
					return enableList[j];
				}
				return null;
			}
			else
			{
				j++;
			}
		}
		return null;
	}

	// Token: 0x060033BD RID: 13245 RVA: 0x000CD4C4 File Offset: 0x000CB6C4
	public void Clear()
	{
		if (this.mObjDict != null)
		{
			this.mObjDict.Clear();
		}
		if (this.mOtherObjDic != null)
		{
			this.mOtherObjDic.Clear();
		}
		if (this.mOtherPlayerEscortNPCList != null)
		{
			this.mOtherPlayerEscortNPCList.Clear();
		}
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x000CD514 File Offset: 0x000CB714
	public void AddDict(long id, Obj obj)
	{
		if (this.mObjDict.ContainsKey(id))
		{
			Log.ERROR_MSG("Erro AddDict SeverId=" + id);
			return;
		}
		this.mObjDict.Add(id, obj);
		this.AddMapShowObj(obj);
	}

	// Token: 0x060033BF RID: 13247 RVA: 0x000CD55C File Offset: 0x000CB75C
	public void RemoveDict(long id)
	{
		if (this.mObjDict != null)
		{
			this.mObjDict.Remove(id);
		}
	}

	// Token: 0x060033C0 RID: 13248 RVA: 0x000CD578 File Offset: 0x000CB778
	public void RemoveObj(ObjCharacter character)
	{
		character.Recyle();
		this.RemoveDict(character.ServerId);
		this.RemoveShowObj(character);
	}

	// Token: 0x060033C1 RID: 13249 RVA: 0x000CD5A0 File Offset: 0x000CB7A0
	public void RemoveObj(long serverId)
	{
		Obj obj = this.FindObj(serverId);
		if (obj.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
		{
			ObjOtherPlayer otherPlayer = obj as ObjOtherPlayer;
			this.RecycleOtherPlayer(otherPlayer);
		}
		else if (obj.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
		{
			ObjNPC objNpc = obj as ObjNPC;
			this.RecycleNpc(objNpc);
		}
		else if (obj.ObjType == GameDefine.OBJ_TYPE.OBJ_DROP_ITEM)
		{
			ObjDropItem dropItem = obj as ObjDropItem;
			this.RecycleDropItem(dropItem);
		}
		else if (obj.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			this.RemoveDict(serverId);
			Object.Destroy(obj);
		}
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x000CD62C File Offset: 0x000CB82C
	public void RecycleNpc(ObjNPC objNpc)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.OnRecycleNpc(objNpc);
		this.mObjNpcPoolGroup.RecycleNpc(objNpc);
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x000CD64C File Offset: 0x000CB84C
	public void CreateNPC(ObjInitNpcData initData, ObjManager.OnGetNPC func = null, ObjManager.OnGetNPC onLoadModelDone = null)
	{
		if (func != null)
		{
			func = (ObjManager.OnGetNPC)Delegate.Combine(func, new ObjManager.OnGetNPC(this.OnNpcGet));
		}
		else
		{
			func = new ObjManager.OnGetNPC(this.OnNpcGet);
		}
		initData.mPos = new Vector3(initData.mPos.x, SceneManager.GetHitHeight(initData.mPos), initData.mPos.z);
		this.mObjNpcPoolGroup.GetNpc(initData, func, onLoadModelDone);
	}

	// Token: 0x060033C4 RID: 13252 RVA: 0x000CD6C8 File Offset: 0x000CB8C8
	public void DisableAllLocalNpcAction()
	{
		for (int i = 0; i < this.mObjNpcPoolGroup.EnableNpcList.Count; i++)
		{
			if (this.mObjNpcPoolGroup.EnableNpcList[i].AILogic != null)
			{
				this.mObjNpcPoolGroup.EnableNpcList[i].AILogic.DisableAIAction();
			}
		}
	}

	// Token: 0x060033C5 RID: 13253 RVA: 0x000CD734 File Offset: 0x000CB934
	public void OnNpcGet(ObjNPC objNpc)
	{
		this.AddDict(objNpc.ServerId, objNpc);
		this.AddToTargetCampList(objNpc);
		if (objNpc.NPCData.Type == 4)
		{
			CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
			if (escortMission == null || objNpc.ServerId != escortMission.GetParam(1))
			{
				this.mOtherPlayerEscortNPCList.Add(objNpc);
			}
		}
	}

	// Token: 0x060033C6 RID: 13254 RVA: 0x000CD79C File Offset: 0x000CB99C
	public bool IsMyEscortNpc(ObjNPC objNpc)
	{
		CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
		return escortMission != null && objNpc.ServerId == escortMission.GetParam(1);
	}

	// Token: 0x060033C7 RID: 13255 RVA: 0x000CD7D4 File Offset: 0x000CB9D4
	public void OnNpcRecycle(ObjNPC objNpc)
	{
		this.RemoveObj(objNpc);
		this.RemoveFromTargetCampList(objNpc);
		if (objNpc.NPCData.Type == 4)
		{
			CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
			if (escortMission == null || objNpc.ServerId != escortMission.GetParam(1))
			{
				this.mOtherPlayerEscortNPCList.Remove(objNpc);
			}
		}
	}

	// Token: 0x060033C8 RID: 13256 RVA: 0x000CD838 File Offset: 0x000CBA38
	public bool CheckNPCClear(ObjNPC objNpc)
	{
		return this.mObjNpcPoolGroup.CheckNpcClear(objNpc);
	}

	// Token: 0x060033C9 RID: 13257 RVA: 0x000CD848 File Offset: 0x000CBA48
	public void CreateDropItem(ObjInitDropItemData dropItemInitData)
	{
		if (dropItemInitData.ownerServerId != this.MainPlayer.ServerId)
		{
			return;
		}
		if (dropItemInitData.ItemType == GameDefine.ITEM_TYPE.ADD_COIN)
		{
			ResourcesManager.LoadDropItemPrefab(UIInfo.DropMoney, "DropItem", new ResourcesManager.LoadDropItemDelegate(this.OnCreateDropItem), dropItemInitData);
		}
		else
		{
			SimpleRewardRootLogic.AddReward(dropItemInitData.item);
		}
	}

	// Token: 0x060033CA RID: 13258 RVA: 0x000CD8A4 File Offset: 0x000CBAA4
	public void OnCreateDropItem(GameObject newObj, ObjInitDropItemData initData)
	{
		if (newObj == null)
		{
			Debug.Log("create dropItem obj == null");
			return;
		}
		if (initData == null)
		{
			Debug.Log("dropItem InitData == null");
			return;
		}
		ObjDropItem objDropItem = newObj.GetComponent<ObjDropItem>();
		if (objDropItem == null)
		{
			objDropItem = newObj.AddComponent<ObjDropItem>();
		}
		objDropItem.Init(initData);
		this.AddDict(initData.ServerID, objDropItem);
	}

	// Token: 0x060033CB RID: 13259 RVA: 0x000CD908 File Offset: 0x000CBB08
	public void RecycleDropItem(ObjDropItem dropItem)
	{
		this.RemoveDict(dropItem.ServerId);
		ResourcesManager.UnLoadDropItemPrefab(dropItem.gameObject);
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x000CD924 File Offset: 0x000CBB24
	public void CreateSurveyItem(SurveyMissionData surveyMissionData)
	{
		if (surveyMissionData == null)
		{
			return;
		}
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/SurveyItemRoot") as GameObject;
		gameObject.gameObject.name = surveyMissionData.GetSurveyItemName();
		SurveyItemObj surveyItemObj = gameObject.AddComponent<SurveyItemObj>();
		surveyItemObj.Reset(surveyMissionData);
		surveyItemObj.MeshRoot = null;
		surveyItemObj.EffectRoot = gameObject.transform.GetChild(0).gameObject;
		if (!string.IsNullOrEmpty(surveyMissionData.ModelName))
		{
			GameObject gameObject2 = ResourcesManager.LoadAndInstantiate("TestModel/" + surveyMissionData.ModelName) as GameObject;
			if (gameObject2 != null)
			{
				surveyItemObj.MeshRoot = gameObject2;
				gameObject2.transform.parent = surveyItemObj.gameObject.transform;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
			}
		}
		this.AddOtherObjDic(surveyMissionData.GetSurveyItemName(), gameObject);
	}

	// Token: 0x060033CD RID: 13261 RVA: 0x000CDA08 File Offset: 0x000CBC08
	private void OnLoadSurveyItemFinished(object objBundle, object param1, object param2)
	{
		SurveyItemObj surveyItemObj = param1 as SurveyItemObj;
		surveyItemObj.LoadingModelData = null;
		surveyItemObj.LoadingModelDataId = -1L;
		SurveyMissionData surveyMissionData = param2 as SurveyMissionData;
		GameObject gameObject = objBundle as GameObject;
		BundleManager.ResetShader(gameObject.transform);
		gameObject.transform.parent = surveyItemObj.transform;
		gameObject.transform.localPosition = Vector3.zero;
		UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, true);
		surveyItemObj.MeshRoot = gameObject;
	}

	// Token: 0x060033CE RID: 13262 RVA: 0x000CDA7C File Offset: 0x000CBC7C
	public void RecycleSurveyItem(SurveyItemObj surveyItem)
	{
		if (surveyItem == null || surveyItem.gameObject == null)
		{
			return;
		}
		if (surveyItem.LoadingModelData != null)
		{
			surveyItem.LoadingModelData.OnLoadFinished = null;
			surveyItem.LoadingModelData = null;
		}
		this.RemoveOtherObj(string.Format("SurveyItem{0}", surveyItem.SurveyMissionData.ID));
		Object.Destroy(surveyItem.gameObject);
	}

	// Token: 0x060033CF RID: 13263 RVA: 0x000CDAEC File Offset: 0x000CBCEC
	private void AddOtherObjDic(string id, GameObject obj)
	{
		if (this.mOtherObjDic == null)
		{
			this.mOtherObjDic = new Dictionary<string, GameObject>();
		}
		if (!this.mOtherObjDic.ContainsKey(id))
		{
			this.mOtherObjDic.Add(id, obj);
		}
		else
		{
			Debug.Log(id + " Has Already In The Dic");
		}
	}

	// Token: 0x060033D0 RID: 13264 RVA: 0x000CDB44 File Offset: 0x000CBD44
	public GameObject FindOtherObjInDic(string id)
	{
		if (this.mOtherObjDic != null && this.mOtherObjDic.ContainsKey(id))
		{
			return this.mOtherObjDic[id];
		}
		return null;
	}

	// Token: 0x060033D1 RID: 13265 RVA: 0x000CDB7C File Offset: 0x000CBD7C
	public bool RemoveOtherObj(string id)
	{
		if (this.mOtherObjDic.ContainsKey(id))
		{
			this.mOtherObjDic.Remove(id);
			return true;
		}
		return false;
	}

	// Token: 0x060033D2 RID: 13266 RVA: 0x000CDBA0 File Offset: 0x000CBDA0
	public void ClearAll()
	{
		for (int i = 0; i < this.mCampTargetList.Length; i++)
		{
			if (this.mCampTargetList[i] == null)
			{
				this.mCampTargetList[i] = new List<ObjCharacter>();
			}
			this.mCampTargetList[i].Clear();
		}
		for (int j = 0; j < this.mCampList.Length; j++)
		{
			if (this.mCampList[j] == null)
			{
				this.mCampList[j] = new List<ObjCharacter>();
			}
			this.mCampList[j].Clear();
		}
		this.mMapShowObjList.Clear();
		this.ClearNPC();
		this.ClearOtherPlayer();
		this.mMainPlayer = null;
		this.mMainPlayerCar = null;
		this.Clear();
		this.ClearAICar();
		this.ClearRagdollNPCPool();
		this.ClearPatrolNPCPool();
		this.ClearMountCarPoolGroup();
		this.ClearBombPool();
		this.ClearFakeCar();
		this.mPlayerMeshLoadNumDic.Clear();
		this.mPlayerMeshLoadTargetNumDic.Clear();
	}

	// Token: 0x060033D3 RID: 13267 RVA: 0x000CDC94 File Offset: 0x000CBE94
	public void RecycleAllNPC()
	{
		for (int i = this.mObjNpcPoolGroup.EnableNpcList.Count - 1; i >= 0; i--)
		{
			this.RecycleNpc(this.mObjNpcPoolGroup.EnableNpcList[i]);
		}
	}

	// Token: 0x060033D4 RID: 13268 RVA: 0x000CDCDC File Offset: 0x000CBEDC
	public void ClearNPC()
	{
		this.mObjNpcPoolGroup.ClearNPC();
	}

	// Token: 0x060033D5 RID: 13269 RVA: 0x000CDCEC File Offset: 0x000CBEEC
	public void RecycleAllOtherPlayer()
	{
		this.mOtherPlayerNoLogicDataDic.Clear();
		List<Obj> list = new List<Obj>(this.mObjDict.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
			{
				this.RecycleOtherPlayer(list[i] as ObjOtherPlayer);
			}
		}
	}

	// Token: 0x060033D6 RID: 13270 RVA: 0x000CDD50 File Offset: 0x000CBF50
	public void ClearOtherPlayer()
	{
		this.mOtherPlayerPool.Clear();
		this.mObjOtherPlayerVisibleList.Clear();
		this.mObjOtherPlayerInVisibleList.Clear();
		this.mOtherPlayerNoLogicDataDic.Clear();
	}

	// Token: 0x060033D7 RID: 13271 RVA: 0x000CDD8C File Offset: 0x000CBF8C
	public ObjNPC FindNearestFightNPCInScene(string npcID)
	{
		float num = float.MaxValue;
		ObjNPC result = null;
		for (int i = 0; i < this.mObjNpcPoolGroup.EnableNpcList.Count; i++)
		{
			ObjNPC objNPC = this.mObjNpcPoolGroup.EnableNpcList[i];
			if (!objNPC.IsMissionNpc())
			{
				if (objNPC.NPCDataID.Equals(npcID))
				{
					float num2 = VectorXZ.Distance(this.mMainPlayer.Position, objNPC.Position);
					if (num2 < num)
					{
						num = num2;
						result = objNPC;
					}
				}
			}
		}
		for (int j = 0; j < this.EnableRagdollNpcList.Count; j++)
		{
			ObjNPC objNPC2 = this.EnableRagdollNpcList[j];
			if (!objNPC2.IsMissionNpc())
			{
				if (objNPC2.NPCDataID.Equals(npcID))
				{
					float num3 = VectorXZ.Distance(this.mMainPlayer.Position, objNPC2.Position);
					if (num3 < num)
					{
						num = num3;
						result = objNPC2;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060033D8 RID: 13272 RVA: 0x000CDEA8 File Offset: 0x000CC0A8
	public ObjNPC FindNearestFightNPCInScene()
	{
		float num = float.MaxValue;
		ObjNPC result = null;
		for (int i = 0; i < this.mObjNpcPoolGroup.EnableNpcList.Count; i++)
		{
			ObjNPC objNPC = this.mObjNpcPoolGroup.EnableNpcList[i];
			if (!objNPC.IsMissionNpc())
			{
				float num2 = VectorXZ.Distance(this.mMainPlayer.Position, objNPC.Position);
				if (num2 < num)
				{
					num = num2;
					result = objNPC;
				}
			}
		}
		for (int j = 0; j < this.EnableRagdollNpcList.Count; j++)
		{
			ObjNPC objNPC2 = this.EnableRagdollNpcList[j];
			if (!objNPC2.IsMissionNpc())
			{
				float num3 = VectorXZ.Distance(this.mMainPlayer.Position, objNPC2.Position);
				if (num3 < num)
				{
					num = num3;
					result = objNPC2;
				}
			}
		}
		return result;
	}

	// Token: 0x060033D9 RID: 13273 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
	public bool IsCanAttack(ObjCharacter curCharacter)
	{
		float num = 100f;
		if (curCharacter == null || curCharacter.IsDie || !UnityVersionUtil.IsActive(curCharacter.gameObject))
		{
			return false;
		}
		if (curCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
		{
			ObjNPC objNPC = curCharacter as ObjNPC;
			if (objNPC.NPCFunctionType == GameDefine.NPC_FUNCTION_TYPE.CITIZEN_NPC)
			{
				return false;
			}
			if (objNPC.IsMissionNpc())
			{
				return false;
			}
		}
		else if (curCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && !CampTool.ISPlayerCanAttack(this.MainPlayer, curCharacter as ObjOtherPlayer))
		{
			return false;
		}
		float num2 = VectorXZ.Distance(this.mMainPlayer.Position, curCharacter.Position);
		return num2 < num;
	}

	// Token: 0x060033DA RID: 13274 RVA: 0x000CE064 File Offset: 0x000CC264
	public ObjCharacter FindCanAttackCharacter(int camp, Vector3 pos)
	{
		float num = 100f;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager != null && sceneManager.CurrentMapInofData != null)
		{
			num = (float)sceneManager.CurrentMapInofData.AutoFightDis;
		}
		ObjCharacter result = null;
		List<ObjCharacter> list = this.mCampTargetList[camp];
		for (int i = 0; i < list.Count; i++)
		{
			if (this.CheckCanAutoAttack(list[i]))
			{
				ObjCharacter objCharacter = list[i];
				float num2 = VectorXZ.Distance(this.mMainPlayer.Position, objCharacter.Position);
				if (num2 < num)
				{
					num = num2;
					result = objCharacter;
				}
			}
		}
		return result;
	}

	// Token: 0x060033DB RID: 13275 RVA: 0x000CE128 File Offset: 0x000CC328
	public bool CheckCanAutoAttack(ObjCharacter curCharacter)
	{
		if (curCharacter.IsDie)
		{
			return false;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (curCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
		{
			ObjNPC objNPC = curCharacter as ObjNPC;
			if (objNPC.NPCFunctionType == GameDefine.NPC_FUNCTION_TYPE.CITIZEN_NPC)
			{
				return false;
			}
			if (objNPC.IsMissionNpc())
			{
				return false;
			}
			if (playerData.NonMissionTarget == 0 && (sceneManager.IsBigWorld() || sceneManager.IsTutorialScene()) && !missionManager.IsCurMissionNeedNpc(objNPC))
			{
				return false;
			}
		}
		else if (curCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
		{
			if (!CampTool.ISPlayerCanAttack(this.MainPlayer, curCharacter as ObjOtherPlayer))
			{
				return false;
			}
		}
		else if ((curCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC_CAR || curCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR) && playerData.VehicleTarget == 0 && (sceneManager.IsBigWorld() || sceneManager.IsTutorialScene()) && !missionManager.IsHaveDestroyCarMission())
		{
			return false;
		}
		return true;
	}

	// Token: 0x060033DC RID: 13276 RVA: 0x000CE234 File Offset: 0x000CC434
	public ObjPlayerCar CreateMainPlayerCar(ObjCarInitData initData)
	{
		if (this.mMainPlayerCar != null)
		{
			return null;
		}
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/CarRoot") as GameObject;
		gameObject.name = string.Format("PlayerCar", new object[0]);
		gameObject.tag = "PlayerCar";
		ObjPlayerCar objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
		if (objPlayerCar == null)
		{
			objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
			objPlayerCar.InitCar();
		}
		objPlayerCar.ResetPlayerCar(initData);
		this.mMainPlayerCar = objPlayerCar;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ObjPlayerMountCar mountCar = this.GetMountCar(new RideMountData
		{
			MountData = DataManager.GetMountDataById(playerData.MountId),
			mColorData = DataManager.GetColorDataById(playerData.MountColor),
			Player = this.MainPlayer,
			PlayerCar = this.mMainPlayerCar
		});
		if (mountCar == null)
		{
			Debug.Log("mountCar == null !!!!!!!!!!!!!!!!!!!!!!!!!");
		}
		if (objPlayerCar == null)
		{
			Debug.Log("playerCar == null !!!!!!!!!!!!!!!!!!!!!!!!!");
		}
		if (objPlayerCar.MeshRoot == null)
		{
			Debug.Log("playerCar.MeshRoot == null !!!!!!!!!!!!!!!!!!!!!!!!!");
		}
		mountCar.transform.parent = objPlayerCar.MeshRoot.transform;
		mountCar.transform.localPosition = Vector3.zero;
		mountCar.transform.localRotation = Quaternion.identity;
		return objPlayerCar;
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x000CE38C File Offset: 0x000CC58C
	public ObjPlayerCar CreateTutorialPlayerCar(ObjCarInitData initData)
	{
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject;
		gameObject.name = string.Format("PlayerCar", new object[0]);
		gameObject.tag = "PlayerCar";
		GameObject gameObject2 = null;
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.CharacterModelId);
		if (characterModelDataByID != null)
		{
			string path = string.Format("TestModel/CarModel/{0}", characterModelDataByID.Name);
			gameObject2 = this.ReloadModel(gameObject.transform, path);
			gameObject2.gameObject.name = string.Format("MeshRoot", new object[0]);
		}
		ObjPlayerCar objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
		if (objPlayerCar == null)
		{
			objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
			objPlayerCar.InitCar();
		}
		objPlayerCar.MeshRoot = gameObject2;
		GameObject gameObject3 = gameObject2.transform.FindChild("cheshen").gameObject;
		gameObject3.layer = LayerMask.NameToLayer("PlayerCar");
		gameObject3.tag = "PlayerCar";
		BoxCollider component = gameObject3.GetComponent<BoxCollider>();
		GameObject gameObject4 = gameObject2.transform.FindChild("FrontCollision").gameObject;
		gameObject4.layer = gameObject3.layer;
		gameObject4.tag = gameObject3.tag;
		GameObject gameObject5 = new GameObject("PlayerCarCollider");
		gameObject5.transform.parent = gameObject3.transform.parent;
		gameObject5.transform.localPosition = gameObject3.transform.localPosition;
		gameObject5.transform.localRotation = gameObject3.transform.localRotation;
		BoxCollider boxCollider = gameObject5.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
		gameObject5.layer = gameObject3.layer;
		gameObject5.tag = gameObject3.tag;
		objPlayerCar.ResetPlayerCar(initData);
		return objPlayerCar;
	}

	// Token: 0x060033DE RID: 13278 RVA: 0x000CE590 File Offset: 0x000CC790
	public ObjPlayerCar CreateTutorialAICar(ObjCarInitData initData)
	{
		if (initData.CarMountData == null)
		{
			return null;
		}
		GameObject gameObject;
		ObjPlayerCar objPlayerCar;
		GameObject gameObject2;
		if (GameManager.IsSupportCurDataVersion167() || !SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			ModelData modeDataByID = DataManager.GetModeDataByID(initData.CarMountData.ModelId);
			if (modeDataByID == null)
			{
				return null;
			}
			if (initData.CarMountData.IsShowPlayer == 1)
			{
				gameObject = (ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject);
				objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
				if (objPlayerCar == null)
				{
					objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
				}
				ModelData modeDataByID2 = DataManager.GetModeDataByID(initData.CarMountData.ModelId);
				BundleManager.LoadModelInList(modeDataByID2.Name, false, false, new BundleManager.OnLoadModelFinish(this.OnLoadTutorialPlayerCarFinish), initData, objPlayerCar, modeDataByID2.ModelPath);
				return objPlayerCar;
			}
			string path = string.Format("TestModel/CarModel/{0}", modeDataByID.Name);
			gameObject2 = (ResourcesManager.LoadAndInstantiate(path) as GameObject);
			if (gameObject2 == null)
			{
				if (GameManager.IsSupportCurDataVersion177())
				{
					gameObject = (ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject);
					objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
					if (objPlayerCar == null)
					{
						objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
					}
					ModelData modeDataByID3 = DataManager.GetModeDataByID(initData.CarMountData.ModelId);
					SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadItem(modeDataByID3.Name, new BundleManager.LoaditemsFinish(this.OnLoadTutorialPlayerCarFinishFromItem), initData, objPlayerCar));
					return objPlayerCar;
				}
				return null;
			}
			else
			{
				gameObject2.gameObject.name = string.Format("MeshRoot", new object[0]);
			}
		}
		else
		{
			CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.CarMountData.ID);
			if (characterModelDataByID == null)
			{
				return null;
			}
			if (initData.CarMountData.IsShowPlayer == 1)
			{
				gameObject = (ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject);
				objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
				if (objPlayerCar == null)
				{
					objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
				}
				ModelData modeDataByID4 = DataManager.GetModeDataByID(initData.CarMountData.ModelId);
				BundleManager.LoadModelInList(modeDataByID4.Name, false, false, new BundleManager.OnLoadModelFinish(this.OnLoadTutorialPlayerCarFinish), initData, objPlayerCar, modeDataByID4.ModelPath);
				return objPlayerCar;
			}
			string path2 = string.Format("TestModel/CarModel/{0}", characterModelDataByID.Name);
			gameObject2 = (ResourcesManager.LoadAndInstantiate(path2) as GameObject);
			if (gameObject2 == null)
			{
				if (GameManager.IsSupportCurDataVersion177())
				{
					gameObject = (ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject);
					objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
					if (objPlayerCar == null)
					{
						objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
					}
					ModelData modeDataByID5 = DataManager.GetModeDataByID(initData.CarMountData.ModelId);
					SingletonDontDestoryUnity<GameManager>.Instance.StartCoroutine(BundleManager.LoadItem(modeDataByID5.Name, new BundleManager.LoaditemsFinish(this.OnLoadTutorialPlayerCarFinishFromItem), initData, objPlayerCar));
					return objPlayerCar;
				}
				return null;
			}
			else
			{
				gameObject2.gameObject.name = string.Format("MeshRoot", new object[0]);
			}
		}
		gameObject = (ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject);
		objPlayerCar = gameObject.GetComponent<ObjPlayerCar>();
		if (objPlayerCar == null)
		{
			objPlayerCar = gameObject.AddComponent<ObjPlayerCar>();
		}
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject2.transform.localScale = Vector3.one;
		objPlayerCar.MeshRoot = gameObject2;
		objPlayerCar.InitCar();
		GameObject gameObject3 = gameObject2.transform.FindChild("cheshen").gameObject;
		BoxCollider component = gameObject3.GetComponent<BoxCollider>();
		GameObject gameObject4 = gameObject2.transform.FindChild("FrontCollision").gameObject;
		gameObject4.layer = gameObject3.layer;
		gameObject4.tag = gameObject3.tag;
		GameObject gameObject5 = new GameObject("PlayerCarCollider");
		gameObject5.transform.parent = gameObject3.transform.parent;
		gameObject5.transform.localPosition = gameObject3.transform.localPosition;
		gameObject5.transform.localRotation = gameObject3.transform.localRotation;
		BoxCollider boxCollider = gameObject5.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
		gameObject5.layer = gameObject3.layer;
		gameObject5.tag = gameObject3.tag;
		objPlayerCar.ResetPlayerCar(initData);
		return objPlayerCar;
	}

	// Token: 0x060033DF RID: 13279 RVA: 0x000CEA04 File Offset: 0x000CCC04
	private void OnLoadTutorialPlayerCarFinishFromItem(string name, Object modelBundle, object param1 = null, object param2 = null)
	{
		ObjCarInitData objCarInitData = param1 as ObjCarInitData;
		ObjPlayerCar objPlayerCar = param2 as ObjPlayerCar;
		MountData carMountData = objCarInitData.CarMountData;
		GameObject gameObject = Object.Instantiate(modelBundle) as GameObject;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		BundleManager.ResetShader(gameObject.transform);
		UnityVersionUtil.SetActiveRecursive(gameObject, true);
		gameObject.gameObject.name = "MeshRoot";
		gameObject.transform.parent = objPlayerCar.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.animation.cullingType = 1;
		objPlayerCar.MeshRoot = gameObject;
		objPlayerCar.InitCar();
		GameObject gameObject2 = gameObject.transform.FindChild("cheshen").gameObject;
		BoxCollider component = gameObject2.GetComponent<BoxCollider>();
		GameObject gameObject3 = gameObject.transform.FindChild("FrontCollision").gameObject;
		gameObject3.layer = gameObject2.layer;
		gameObject3.tag = gameObject2.tag;
		GameObject gameObject4 = new GameObject("PlayerCarCollider");
		gameObject4.transform.parent = gameObject2.transform.parent;
		gameObject4.transform.localPosition = gameObject2.transform.localPosition;
		gameObject4.transform.localRotation = gameObject2.transform.localRotation;
		BoxCollider boxCollider = gameObject4.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
		gameObject4.layer = gameObject2.layer;
		gameObject4.tag = gameObject2.tag;
		objPlayerCar.ResetPlayerCar(objCarInitData);
		if (carMountData.IsShowPlayer == 1)
		{
			objPlayerCar.UpdateColor();
		}
		ObjFakeAICar component2 = objPlayerCar.gameObject.GetComponent<ObjFakeAICar>();
		if (component2 != null)
		{
			component2.InitCar();
			component2.ResetStaticCar();
		}
	}

	// Token: 0x060033E0 RID: 13280 RVA: 0x000CEC1C File Offset: 0x000CCE1C
	private void OnLoadTutorialPlayerCarFinish(object modelBundle, object param1, object param2)
	{
		ObjCarInitData objCarInitData = param1 as ObjCarInitData;
		ObjPlayerCar objPlayerCar = param2 as ObjPlayerCar;
		MountData carMountData = objCarInitData.CarMountData;
		GameObject gameObject = modelBundle as GameObject;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		BundleManager.ResetShader(gameObject.transform);
		UnityVersionUtil.SetActiveRecursive(gameObject, true);
		gameObject.gameObject.name = "MeshRoot";
		gameObject.transform.parent = objPlayerCar.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.animation.cullingType = 1;
		objPlayerCar.MeshRoot = gameObject;
		objPlayerCar.InitCar();
		GameObject gameObject2 = gameObject.transform.FindChild("cheshen").gameObject;
		BoxCollider component = gameObject2.GetComponent<BoxCollider>();
		GameObject gameObject3 = gameObject.transform.FindChild("FrontCollision").gameObject;
		gameObject3.layer = gameObject2.layer;
		gameObject3.tag = gameObject2.tag;
		GameObject gameObject4 = new GameObject("PlayerCarCollider");
		gameObject4.transform.parent = gameObject2.transform.parent;
		gameObject4.transform.localPosition = gameObject2.transform.localPosition;
		gameObject4.transform.localRotation = gameObject2.transform.localRotation;
		BoxCollider boxCollider = gameObject4.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
		gameObject4.layer = gameObject2.layer;
		gameObject4.tag = gameObject2.tag;
		objPlayerCar.ResetPlayerCar(objCarInitData);
		if (carMountData.IsShowPlayer == 1)
		{
			objPlayerCar.UpdateColor();
		}
		ObjFakeAICar component2 = objPlayerCar.gameObject.GetComponent<ObjFakeAICar>();
		if (component2 != null)
		{
			component2.InitCar();
			component2.ResetStaticCar();
		}
	}

	// Token: 0x17000E8D RID: 3725
	// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000CEE2C File Offset: 0x000CD02C
	public List<ObjFakeAICar> EnableFakeAICarList
	{
		get
		{
			return this.mObjFakeAICarPoolGroup.EnableList;
		}
	}

	// Token: 0x060033E2 RID: 13282 RVA: 0x000CEE3C File Offset: 0x000CD03C
	private void ResetObjFakeAICarPoolGroup()
	{
		this.mObjFakeAICarPoolGroup = new SimplePoolGroup<ObjFakeAICar>();
		this.mObjFakeAICarPoolGroup.Reset(new SimplePool<ObjFakeAICar>.CreateFunc(this.CreateObjFakeAICar), new SimplePool<ObjFakeAICar>.DestroyFunc(this.DestroyObjFakeAICar), 10);
	}

	// Token: 0x060033E3 RID: 13283 RVA: 0x000CEE7C File Offset: 0x000CD07C
	public void ClearFakeCar()
	{
		for (int i = 0; i < this.mObjFakeAICarPoolGroup.EnableList.Count; i++)
		{
			this.RemoveDict(this.mObjFakeAICarPoolGroup.EnableList[i].PlayerCar.ServerId);
		}
		this.mObjFakeAICarPoolGroup.Clear();
	}

	// Token: 0x060033E4 RID: 13284 RVA: 0x000CEED8 File Offset: 0x000CD0D8
	private ObjFakeAICar CreateObjFakeAICar(object data)
	{
		ObjCarInitData objCarInitData = data as ObjCarInitData;
		ObjPlayerCar objPlayerCar = this.CreateTutorialAICar(objCarInitData);
		if (objPlayerCar == null)
		{
			return null;
		}
		objPlayerCar.gameObject.name = "FakeAICar" + this.tt++;
		ObjFakeAICar objFakeAICar = objPlayerCar.gameObject.GetComponent<ObjFakeAICar>();
		if (objFakeAICar == null)
		{
			objFakeAICar = objPlayerCar.gameObject.AddComponent<ObjFakeAICar>();
		}
		objFakeAICar.ModelId = objCarInitData.CharacterModelId;
		return objFakeAICar;
	}

	// Token: 0x060033E5 RID: 13285 RVA: 0x000CEF60 File Offset: 0x000CD160
	private void DestroyObjFakeAICar(ObjFakeAICar aiCar)
	{
		if (aiCar != null)
		{
			Object.Destroy(aiCar.gameObject);
		}
	}

	// Token: 0x060033E6 RID: 13286 RVA: 0x000CEF7C File Offset: 0x000CD17C
	public ObjFakeAICar GetFakeAICar(ObjCarInitData initData, bool isFriendCar = false)
	{
		ObjFakeAICar objFakeAICar = this.mObjFakeAICarPoolGroup.Get(initData.CharacterModelId, initData);
		if (objFakeAICar == null)
		{
			return null;
		}
		if (initData.CarMountData.IsShowPlayer == 0 && objFakeAICar.PlayerCar.MeshRoot == null)
		{
			UnityVersionUtil.SetActiveRecursive(objFakeAICar.gameObject, false);
			this.mObjFakeAICarPoolGroup.Recycle(objFakeAICar, initData.CharacterModelId);
			return null;
		}
		if (objFakeAICar.PlayerCar.MeshRoot != null)
		{
			objFakeAICar.PlayerCar.ResetPlayerCar(initData);
		}
		else
		{
			objFakeAICar.PlayerCar.BeforeLoadMeshReset(initData);
		}
		objFakeAICar.ServerId = initData.ServerID;
		UnityVersionUtil.SetActiveRecursive(objFakeAICar.gameObject, true);
		if (isFriendCar)
		{
			objFakeAICar.PlayerCar.AttributeData.Camp = GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC;
		}
		else
		{
			objFakeAICar.PlayerCar.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
		}
		this.AddDict(initData.ServerID, objFakeAICar.PlayerCar);
		this.AddToTargetCampList(objFakeAICar.PlayerCar);
		return objFakeAICar;
	}

	// Token: 0x060033E7 RID: 13287 RVA: 0x000CF088 File Offset: 0x000CD288
	public void RecycleFakeAICar(ObjFakeAICar aiCar)
	{
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.OnRecycleFakeAICar(aiCar);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.OnRecycleTargetCar(aiCar);
		this.RemoveDict(aiCar.PlayerCar.ServerId);
		this.RemoveFromTargetCampList(aiCar.PlayerCar);
		this.mObjFakeAICarPoolGroup.Recycle(aiCar, aiCar.ModelId);
	}

	// Token: 0x060033E8 RID: 13288 RVA: 0x000CF0EC File Offset: 0x000CD2EC
	private void ResetObjSimpleAICarPoolGroup()
	{
		this.mObjSimpleAICarPoolGroup = new SimplePoolGroup<ObjSimpleAICar>();
		this.mObjSimpleAICarPoolGroup.Reset(new SimplePool<ObjSimpleAICar>.CreateFunc(this.CreateObjSimpleAICar), new SimplePool<ObjSimpleAICar>.DestroyFunc(this.DestroyObjSimpleAICar), 10);
	}

	// Token: 0x060033E9 RID: 13289 RVA: 0x000CF12C File Offset: 0x000CD32C
	public void ClearAICar()
	{
		this.mObjSimpleAICarPoolGroup.Clear();
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x000CF13C File Offset: 0x000CD33C
	private ObjSimpleAICar CreateObjSimpleAICar(object data)
	{
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/TutorialCarRoot") as GameObject;
		gameObject.name = string.Format("AICar", new object[0]);
		gameObject.tag = "PoliceCar";
		ObjCarInitData objCarInitData = data as ObjCarInitData;
		GameObject gameObject2 = null;
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(objCarInitData.CharacterModelId);
		if (characterModelDataByID != null)
		{
			string path = string.Format("TestModel/CarModel/{0}", characterModelDataByID.Name);
			gameObject2 = this.ReloadModel(gameObject.transform, path);
			gameObject2.gameObject.name = string.Format("MeshRoot", new object[0]);
		}
		ObjSimpleAICar objSimpleAICar = gameObject.GetComponent<ObjSimpleAICar>();
		if (objSimpleAICar == null)
		{
			objSimpleAICar = gameObject.AddComponent<ObjSimpleAICar>();
		}
		objSimpleAICar.MeshRoot = gameObject2;
		GameObject gameObject3 = gameObject2.transform.FindChild("cheshen").gameObject;
		gameObject3.layer = LayerMask.NameToLayer("PlayerCar");
		gameObject3.tag = "PoliceCar";
		BoxCollider component = gameObject3.GetComponent<BoxCollider>();
		GameObject gameObject4 = gameObject2.transform.FindChild("FrontCollision").gameObject;
		gameObject4.layer = gameObject3.layer;
		gameObject4.tag = gameObject3.tag;
		GameObject gameObject5 = new GameObject("PlayerCarCollider");
		gameObject5.transform.parent = gameObject3.transform.parent;
		gameObject5.transform.localPosition = gameObject3.transform.localPosition;
		gameObject5.transform.localRotation = gameObject3.transform.localRotation;
		BoxCollider boxCollider = gameObject5.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
		gameObject5.layer = gameObject3.layer;
		gameObject5.tag = gameObject3.tag;
		return objSimpleAICar;
	}

	// Token: 0x060033EB RID: 13291 RVA: 0x000CF338 File Offset: 0x000CD538
	private void DestroyObjSimpleAICar(ObjSimpleAICar aiCar)
	{
		Object.Destroy(aiCar.gameObject);
	}

	// Token: 0x060033EC RID: 13292 RVA: 0x000CF348 File Offset: 0x000CD548
	public ObjSimpleAICar GetSimpleAICar(ObjCarInitData initData)
	{
		return this.mObjSimpleAICarPoolGroup.Get(initData.CharacterModelId, initData);
	}

	// Token: 0x060033ED RID: 13293 RVA: 0x000CF36C File Offset: 0x000CD56C
	public void RecycleSimpleAICar(ObjSimpleAICar aiCar)
	{
		this.mObjSimpleAICarPoolGroup.Recycle(aiCar, aiCar.ModelId);
	}

	// Token: 0x060033EE RID: 13294 RVA: 0x000CF380 File Offset: 0x000CD580
	public void StopPoliceSound()
	{
		int num = 0;
		for (int i = 0; i < this.mObjSimpleAICarPoolGroup.EnableList.Count; i++)
		{
			if (!this.mObjSimpleAICarPoolGroup.EnableList[i].IsChaseDone)
			{
				num++;
			}
		}
		if (num <= 0 && SingletonDontDestoryUnity<SoundManager>.Exists)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(this.mPoliceSoundId);
		}
	}

	// Token: 0x060033EF RID: 13295 RVA: 0x000CF3F0 File Offset: 0x000CD5F0
	public void StopAllPoliceSound()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(this.mPoliceSoundId);
	}

	// Token: 0x17000E8E RID: 3726
	// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000CF404 File Offset: 0x000CD604
	public List<ObjRagdollNPC> EnableRagdollNpcList
	{
		get
		{
			return this.mObjRagdollNPCPoolGroup.EnableList;
		}
	}

	// Token: 0x060033F1 RID: 13297 RVA: 0x000CF414 File Offset: 0x000CD614
	private void ResetObjRagdollNPCPoolGroup()
	{
		this.mObjRagdollNPCPoolGroup = new SimplePoolGroup<ObjRagdollNPC>();
		this.mObjRagdollNPCPoolGroup.Reset(new SimplePool<ObjRagdollNPC>.CreateFunc(this.CreateRagdollNPC), new SimplePool<ObjRagdollNPC>.DestroyFunc(this.DestroyRagdollNPC), 20);
	}

	// Token: 0x060033F2 RID: 13298 RVA: 0x000CF454 File Offset: 0x000CD654
	private void ClearRagdollNPCPool()
	{
		this.mObjRagdollNPCPoolGroup.Clear();
	}

	// Token: 0x060033F3 RID: 13299 RVA: 0x000CF464 File Offset: 0x000CD664
	private ObjRagdollNPC CreateRagdollNPC(object data)
	{
		ObjInitNpcData objInitNpcData = data as ObjInitNpcData;
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(objInitNpcData.npcInfoData.Model);
		if (characterModelDataByID != null)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/NPCRoot") as GameObject;
			ObjRagdollNPC objRagdollNPC = null;
			if (gameObject != null)
			{
				gameObject.name = string.Concat(new object[]
				{
					"NPC",
					objInitNpcData.npcInfoData.ID,
					"_",
					objInitNpcData.mServerID
				});
				objRagdollNPC = gameObject.GetComponent<ObjRagdollNPC>();
				if (objRagdollNPC == null)
				{
					objRagdollNPC = gameObject.AddComponent<ObjRagdollNPC>();
				}
				objRagdollNPC.InitInfo(characterModelDataByID);
				objRagdollNPC.Init();
				UnityVersionUtil.SetActiveRecursive(objRagdollNPC.gameObject, true);
			}
			objRagdollNPC.LoadingModelData = BundleManager.LoadModelInList(characterModelDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadRagdollNpcModelFinished), objInitNpcData, objRagdollNPC, null);
			objRagdollNPC.LoadingModelDataId = ((objRagdollNPC.LoadingModelData != null) ? objRagdollNPC.LoadingModelData.ID : -1L);
			return objRagdollNPC;
		}
		Debug.Log("characterModelData == null " + objInitNpcData.npcInfoData.Model);
		return null;
	}

	// Token: 0x060033F4 RID: 13300 RVA: 0x000CF580 File Offset: 0x000CD780
	private void OnLoadRagdollNpcModelFinished(object modelBundle, object param1, object param2)
	{
		ObjInitNpcData objInitNpcData = param1 as ObjInitNpcData;
		ObjRagdollNPC objRagdollNPC = param2 as ObjRagdollNPC;
		GameObject gameObject = modelBundle as GameObject;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		gameObject.gameObject.name = string.Format("MeshRoot", new object[0]);
		Material meshMat = null;
		BundleManager.ResetShader(gameObject.transform, out meshMat);
		objRagdollNPC.MeshMat = meshMat;
		gameObject.transform.parent = objRagdollNPC.CacheTransform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		objRagdollNPC.LoadModelFinishInit();
		objRagdollNPC.LoadingModelData = null;
		objRagdollNPC.LoadingModelDataId = -1L;
		objRagdollNPC.MeshRoot = gameObject;
		gameObject.transform.localScale = Vector3.one * objInitNpcData.npcInfoData.ModelScale;
		if (objRagdollNPC.enabled)
		{
			UnityVersionUtil.SetActiveRecursive(gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(gameObject, false);
		}
		ObjRagdollNPC.InitRagdollObj(objRagdollNPC.AnimationLogic.AnimaObj.transform, objRagdollNPC);
	}

	// Token: 0x060033F5 RID: 13301 RVA: 0x000CF68C File Offset: 0x000CD88C
	private void DestroyRagdollNPC(ObjRagdollNPC objNpc)
	{
		if (objNpc.LoadingModelData != null)
		{
			objNpc.LoadingModelData.OnLoadFinished = null;
			objNpc.LoadingModelData = null;
			BundleManager.RemoveFromLoadModelList(objNpc.LoadingModelDataId);
			objNpc.LoadingModelDataId = -1L;
		}
		if (objNpc.MeshRoot != null)
		{
			BundleManager.UnloadModel(objNpc.CurrentCharacterModelData.ID, -1L, false);
		}
		if (objNpc != null && objNpc.gameObject != null)
		{
			Object.Destroy(objNpc.gameObject);
		}
		objNpc = null;
	}

	// Token: 0x060033F6 RID: 13302 RVA: 0x000CF71C File Offset: 0x000CD91C
	public ObjRagdollNPC GetRagdollNPC(ObjInitNpcData initData, ObjManager.OnGetNPC func)
	{
		ObjRagdollNPC objRagdollNPC = this.mObjRagdollNPCPoolGroup.Get(initData.mCharacterModelId, initData);
		if (objRagdollNPC != null)
		{
			objRagdollNPC.ResetNpc(initData);
			if (objRagdollNPC.LoadingModelData == null && objRagdollNPC.MeshRoot == null)
			{
				objRagdollNPC.LoadingModelData = BundleManager.LoadModelInList(objRagdollNPC.CurrentCharacterModelData.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadRagdollNpcModelFinished), initData, objRagdollNPC, null);
				objRagdollNPC.LoadingModelDataId = ((objRagdollNPC.LoadingModelData != null) ? objRagdollNPC.LoadingModelData.ID : -1L);
			}
		}
		else
		{
			SimplePool<ObjRagdollNPC> simplePool = null;
			if (this.mObjRagdollNPCPoolGroup.ObjPoolDic.TryGetValue(initData.mCharacterModelId, ref simplePool) && simplePool.EnableObjList.Count > 0)
			{
				simplePool.EnableObjList[0].RecycleSelf();
				objRagdollNPC = this.mObjRagdollNPCPoolGroup.Get(initData.mCharacterModelId, initData);
				if (objRagdollNPC != null)
				{
					objRagdollNPC.ResetNpc(initData);
					if (objRagdollNPC.LoadingModelData == null && objRagdollNPC.MeshRoot == null)
					{
						objRagdollNPC.LoadingModelData = BundleManager.LoadModelInList(objRagdollNPC.CurrentCharacterModelData.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadRagdollNpcModelFinished), initData, objRagdollNPC, null);
						objRagdollNPC.LoadingModelDataId = ((objRagdollNPC.LoadingModelData != null) ? objRagdollNPC.LoadingModelData.ID : -1L);
					}
				}
			}
		}
		func = (ObjManager.OnGetNPC)Delegate.Combine(func, new ObjManager.OnGetNPC(this.OnNpcGet));
		if (objRagdollNPC != null && func != null)
		{
			func(objRagdollNPC);
		}
		return objRagdollNPC;
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x000CF8BC File Offset: 0x000CDABC
	public void RecycleRagdollNPC(ObjRagdollNPC npc)
	{
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.OnRecycleNpc(npc);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.OnRecycleNpc(npc);
		this.OnNpcRecycle(npc);
		ResourcesManager.UnLoadSimpleShadowPrefab(npc.SimpleShadow);
		this.mObjRagdollNPCPoolGroup.Recycle(npc, npc.CurrentCharacterModelData.ID);
		if (npc != null && npc.LoadingModelData != null)
		{
			npc.LoadingModelData.OnLoadFinished = null;
			npc.LoadingModelData = null;
		}
	}

	// Token: 0x060033F8 RID: 13304 RVA: 0x000CF944 File Offset: 0x000CDB44
	private void ResetObjPatrolNPCPoolGroup()
	{
		this.mObjPatrolNPCPoolGroup = new SimplePoolGroup<ObjPatrolNPC>();
		this.mObjPatrolNPCPoolGroup.Reset(new SimplePool<ObjPatrolNPC>.CreateFunc(this.CreatePatrolNPC), new SimplePool<ObjPatrolNPC>.DestroyFunc(this.DestroyPatrolNPC), 50);
	}

	// Token: 0x060033F9 RID: 13305 RVA: 0x000CF984 File Offset: 0x000CDB84
	private void ClearPatrolNPCPool()
	{
		this.mObjPatrolNPCPoolGroup.Clear();
	}

	// Token: 0x060033FA RID: 13306 RVA: 0x000CF994 File Offset: 0x000CDB94
	private ObjPatrolNPC CreatePatrolNPC(object data)
	{
		ObjInitNpcData objInitNpcData = data as ObjInitNpcData;
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(objInitNpcData.npcInfoData.Model);
		if (characterModelDataByID != null)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/NPCRoot") as GameObject;
			ObjPatrolNPC objPatrolNPC = null;
			if (gameObject != null)
			{
				gameObject.name = "NPC" + objInitNpcData.mServerID;
				objPatrolNPC = gameObject.GetComponent<ObjPatrolNPC>();
				if (objPatrolNPC == null)
				{
					objPatrolNPC = gameObject.AddComponent<ObjPatrolNPC>();
				}
				objPatrolNPC.InitInfo(characterModelDataByID);
				objPatrolNPC.Init();
				UnityVersionUtil.SetActiveRecursive(objPatrolNPC.gameObject, true);
				objPatrolNPC.ResetNpc(objInitNpcData);
			}
			objPatrolNPC.LoadingModelData = BundleManager.LoadModelInList(characterModelDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadNpcModelFinished), objInitNpcData, objPatrolNPC, null);
			objPatrolNPC.LoadingModelDataId = ((objPatrolNPC.LoadingModelData != null) ? objPatrolNPC.LoadingModelData.ID : -1L);
			return objPatrolNPC;
		}
		Debug.Log("characterModelData == null " + objInitNpcData.npcInfoData.Model);
		return null;
	}

	// Token: 0x060033FB RID: 13307 RVA: 0x000CFA94 File Offset: 0x000CDC94
	private void OnLoadNpcModelFinished(object modelBundle, object param1, object param2)
	{
		ObjInitNpcData objInitNpcData = param1 as ObjInitNpcData;
		ObjNPC objNPC = param2 as ObjNPC;
		objNPC.LoadingModelData = null;
		objNPC.LoadingModelDataId = -1L;
		GameObject gameObject = modelBundle as GameObject;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		gameObject.gameObject.name = string.Format("MeshRoot", new object[0]);
		Material material = null;
		BundleManager.ResetShader(gameObject.transform, out material);
		gameObject.transform.parent = objNPC.CacheTransform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		objNPC.LoadModelFinishInit();
		objNPC.MeshRoot = gameObject;
		gameObject.transform.localScale = Vector3.one * objInitNpcData.npcInfoData.ModelScale;
		if (objNPC.enabled)
		{
			UnityVersionUtil.SetActiveRecursive(gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(gameObject, false);
		}
	}

	// Token: 0x060033FC RID: 13308 RVA: 0x000CFB84 File Offset: 0x000CDD84
	private void DestroyPatrolNPC(ObjPatrolNPC objNpc)
	{
		if (objNpc.LoadingModelData != null)
		{
			objNpc.LoadingModelData.OnLoadFinished = null;
			objNpc.LoadingModelData = null;
			BundleManager.RemoveFromLoadModelList(objNpc.LoadingModelDataId);
			objNpc.LoadingModelDataId = -1L;
		}
		if (objNpc.MeshRoot != null)
		{
			BundleManager.UnloadModel(objNpc.CurrentCharacterModelData.ID, -1L, false);
		}
		Object.Destroy(objNpc.gameObject);
		objNpc = null;
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x000CFBF4 File Offset: 0x000CDDF4
	public ObjPatrolNPC GetPatrolNPC(ObjInitNpcData initData, ObjManager.OnGetNPC func)
	{
		ObjPatrolNPC objPatrolNPC = this.mObjPatrolNPCPoolGroup.Get(initData.mCharacterModelId, initData);
		if (objPatrolNPC != null)
		{
			objPatrolNPC.ResetNpc(initData);
			if (objPatrolNPC.LoadingModelData == null && objPatrolNPC.MeshRoot == null)
			{
				objPatrolNPC.LoadingModelData = BundleManager.LoadModelInList(objPatrolNPC.CurrentCharacterModelData.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadNpcModelFinished), initData, objPatrolNPC, null);
				objPatrolNPC.LoadingModelDataId = ((objPatrolNPC.LoadingModelData != null) ? objPatrolNPC.LoadingModelData.ID : -1L);
			}
		}
		else
		{
			SimplePool<ObjPatrolNPC> simplePool = null;
			if (this.mObjPatrolNPCPoolGroup.ObjPoolDic.TryGetValue(initData.mCharacterModelId, ref simplePool) && simplePool.EnableObjList.Count > 0)
			{
				simplePool.EnableObjList[0].RecycleSelf();
				objPatrolNPC = this.mObjPatrolNPCPoolGroup.Get(initData.mCharacterModelId, initData);
				if (objPatrolNPC != null)
				{
					objPatrolNPC.ResetNpc(initData);
					if (objPatrolNPC.LoadingModelData == null && objPatrolNPC.MeshRoot == null)
					{
						objPatrolNPC.LoadingModelData = BundleManager.LoadModelInList(objPatrolNPC.CurrentCharacterModelData.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadNpcModelFinished), initData, objPatrolNPC, null);
						objPatrolNPC.LoadingModelDataId = ((objPatrolNPC.LoadingModelData != null) ? objPatrolNPC.LoadingModelData.ID : -1L);
					}
				}
			}
		}
		if (objPatrolNPC == null)
		{
			Debug.Log("NO NPC!!!!!!!!!!!!!!!!!!!!!!!");
		}
		if (objPatrolNPC != null && func != null)
		{
			func(objPatrolNPC);
		}
		return objPatrolNPC;
	}

	// Token: 0x060033FE RID: 13310 RVA: 0x000CFD90 File Offset: 0x000CDF90
	public void RecyclePatrolNPC(ObjPatrolNPC npc)
	{
		this.mObjPatrolNPCPoolGroup.Recycle(npc, npc.CurrentCharacterModelData.ID);
		if (npc != null && npc.LoadingModelData != null)
		{
			npc.LoadingModelData.OnLoadFinished = null;
			npc.LoadingModelData = null;
		}
	}

	// Token: 0x060033FF RID: 13311 RVA: 0x000CFDE0 File Offset: 0x000CDFE0
	public ObjShiftNPC GetShiftNPC(ObjInitNpcData initData, ObjManager.OnGetNPC func)
	{
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.npcInfoData.Model);
		if (characterModelDataByID != null)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/NPCRoot") as GameObject;
			ObjShiftNPC objShiftNPC = null;
			if (gameObject != null)
			{
				gameObject.name = "NPC" + initData.mServerID;
				objShiftNPC = gameObject.GetComponent<ObjShiftNPC>();
				if (objShiftNPC == null)
				{
					objShiftNPC = gameObject.AddComponent<ObjShiftNPC>();
				}
				objShiftNPC.InitInfo(characterModelDataByID);
				objShiftNPC.Init();
				UnityVersionUtil.SetActiveRecursive(objShiftNPC.gameObject, true);
				objShiftNPC.ResetNpc(initData);
			}
			objShiftNPC.LoadingModelData = BundleManager.LoadModelInList(characterModelDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadNpcModelFinished), initData, objShiftNPC, null);
			objShiftNPC.LoadingModelDataId = ((objShiftNPC.LoadingModelData != null) ? objShiftNPC.LoadingModelData.ID : -1L);
			this.AddDict(initData.mServerID, objShiftNPC);
			this.AddToTargetCampList(objShiftNPC);
			return objShiftNPC;
		}
		Debug.Log("characterModelData == null " + initData.npcInfoData.Model);
		return null;
	}

	// Token: 0x06003400 RID: 13312 RVA: 0x000CFEF0 File Offset: 0x000CE0F0
	private void ResetBombSustainedRangeObjPool()
	{
		this.mBombSustainedRangeObjPool = new SimplePool<BombSustainedRangeObj>();
		this.mBombSustainedRangeObjPool.Reset(new SimplePool<BombSustainedRangeObj>.CreateFunc(this.CreateBombSustainedRangeObj), new SimplePool<BombSustainedRangeObj>.DestroyFunc(this.DestroyBombSustainedRangeObj), 10);
	}

	// Token: 0x06003401 RID: 13313 RVA: 0x000CFF30 File Offset: 0x000CE130
	private BombSustainedRangeObj CreateBombSustainedRangeObj(object data)
	{
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/SustainedBomb") as GameObject;
		return gameObject.GetComponent<BombSustainedRangeObj>();
	}

	// Token: 0x06003402 RID: 13314 RVA: 0x000CFF58 File Offset: 0x000CE158
	private void DestroyBombSustainedRangeObj(BombSustainedRangeObj obj)
	{
		Object.Destroy(obj.gameObject);
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x000CFF68 File Offset: 0x000CE168
	public BombSustainedRangeObj GetBombSustainedRangeObj()
	{
		BombSustainedRangeObj bombSustainedRangeObj = this.mBombSustainedRangeObjPool.Get(null);
		if (bombSustainedRangeObj == null && this.mBombSustainedRangeObjPool.EnableObjList.Count > 0)
		{
			bombSustainedRangeObj = this.mBombSustainedRangeObjPool.EnableObjList[0];
			this.mBombSustainedRangeObjPool.EnableObjList.Remove(bombSustainedRangeObj);
			this.mBombSustainedRangeObjPool.EnableObjList.Add(bombSustainedRangeObj);
		}
		return bombSustainedRangeObj;
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x000CFFDC File Offset: 0x000CE1DC
	public void RecycleBombSustainedRangeObj(BombSustainedRangeObj bomb)
	{
		this.mBombSustainedRangeObjPool.Recycle(bomb);
	}

	// Token: 0x06003405 RID: 13317 RVA: 0x000CFFEC File Offset: 0x000CE1EC
	public void ClearBombPool()
	{
		this.mBombSustainedRangeObjPool.Clear();
	}

	// Token: 0x06003406 RID: 13318 RVA: 0x000CFFFC File Offset: 0x000CE1FC
	public void ClearSceneBomb()
	{
		for (int i = this.mBombSustainedRangeObjPool.EnableObjList.Count - 1; i >= 0; i--)
		{
			this.mBombSustainedRangeObjPool.EnableObjList[i].OnRecycle();
		}
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x000D0044 File Offset: 0x000CE244
	private void ResetMountCarPoolGroup()
	{
		this.mMountCarPoolGroup = new SimplePoolGroup<ObjPlayerMountCar>();
		this.mMountCarPoolGroup.Reset(new SimplePool<ObjPlayerMountCar>.CreateFunc(this.CreateMountCar), new SimplePool<ObjPlayerMountCar>.DestroyFunc(this.DestroyMountCar), 20);
	}

	// Token: 0x06003408 RID: 13320 RVA: 0x000D0084 File Offset: 0x000CE284
	private void ClearMountCarPoolGroup()
	{
		this.mMountCarPoolGroup.Clear();
	}

	// Token: 0x06003409 RID: 13321 RVA: 0x000D0094 File Offset: 0x000CE294
	private ObjPlayerMountCar CreateMountCar(object data)
	{
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/MountCarRoot") as GameObject;
		ObjPlayerMountCar objPlayerMountCar = gameObject.GetComponent<ObjPlayerMountCar>();
		if (objPlayerMountCar == null)
		{
			objPlayerMountCar = gameObject.AddComponent<ObjPlayerMountCar>();
		}
		RideMountData rideMountData = data as RideMountData;
		bool isNeedUnload = !rideMountData.MountData.ID.Equals(this.mMainPlayer.MountId) || GameSettingData.IsBundleNeedUnload[GameSettingData.GetPhoneClass()];
		ModelData modeDataByID = DataManager.GetModeDataByID(rideMountData.MountData.ModelId);
		if (modeDataByID != null)
		{
			objPlayerMountCar.LoadingMeshData = BundleManager.LoadModelInList(modeDataByID.Name, isNeedUnload, false, new BundleManager.OnLoadModelFinish(this.OnLoadMountCarModelFinished), rideMountData, objPlayerMountCar, modeDataByID.ModelPath);
			objPlayerMountCar.LoadingModelDataId = ((objPlayerMountCar.LoadingMeshData != null) ? objPlayerMountCar.LoadingMeshData.ID : -1L);
		}
		else
		{
			Debug.Log("carModelData == null " + rideMountData.MountData.ModelId);
		}
		return objPlayerMountCar;
	}

	// Token: 0x0600340A RID: 13322 RVA: 0x000D018C File Offset: 0x000CE38C
	private void OnLoadMountCarModelFinished(object modelBundle, object param1, object param2)
	{
		RideMountData rideMountData = param1 as RideMountData;
		ObjPlayerMountCar objPlayerMountCar = param2 as ObjPlayerMountCar;
		ObjOtherPlayer player = rideMountData.Player;
		MountData mountData = rideMountData.MountData;
		GameObject gameObject = modelBundle as GameObject;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		BundleManager.ResetAllShader(gameObject.transform);
		NGUITools.SetLayer(gameObject, objPlayerMountCar.gameObject.layer);
		gameObject.gameObject.name = string.Format("CarMeshRoot", new object[0]);
		MountCarMeshRoot component = gameObject.GetComponent<MountCarMeshRoot>();
		objPlayerMountCar.LoadingMeshData = null;
		objPlayerMountCar.LoadingModelDataId = -1L;
		component.CarShadow.transform.localPosition = new Vector3(0f, rideMountData.MountData.ShadowHeight, 0f);
		objPlayerMountCar.OnMeshLoadDone(component, rideMountData);
	}

	// Token: 0x0600340B RID: 13323 RVA: 0x000D025C File Offset: 0x000CE45C
	private void DestroyMountCar(ObjPlayerMountCar objMount)
	{
		if (objMount.LoadingMeshData != null)
		{
			objMount.LoadingMeshData.OnLoadFinished = null;
			objMount.LoadingMeshData = null;
			BundleManager.RemoveFromLoadModelList(objMount.LoadingModelDataId);
			objMount.LoadingModelDataId = -1L;
		}
		if (objMount.MeshRoot != null)
		{
			BundleManager.UnloadModel(objMount.CurRideMountData.MountData.ModelId, -1L, false);
		}
		Object.Destroy(objMount.gameObject);
	}

	// Token: 0x0600340C RID: 13324 RVA: 0x000D02D0 File Offset: 0x000CE4D0
	public ObjPlayerMountCar GetMountCar(RideMountData initData)
	{
		ObjPlayerMountCar objPlayerMountCar = this.mMountCarPoolGroup.Get(initData.MountData.ModelId, initData);
		if (objPlayerMountCar != null)
		{
			if (objPlayerMountCar.MeshRoot == null && objPlayerMountCar.LoadingMeshData == null)
			{
				bool isNeedUnload = !initData.MountData.ID.Equals(this.mMainPlayer.MountId) || GameSettingData.IsBundleNeedUnload[GameSettingData.GetPhoneClass()];
				ModelData modeDataByID = DataManager.GetModeDataByID(initData.MountData.ModelId);
				if (modeDataByID != null)
				{
					objPlayerMountCar.LoadingMeshData = BundleManager.LoadModelInList(modeDataByID.Name, isNeedUnload, false, new BundleManager.OnLoadModelFinish(this.OnLoadMountCarModelFinished), initData, objPlayerMountCar, modeDataByID.ModelPath);
					objPlayerMountCar.LoadingModelDataId = ((objPlayerMountCar.LoadingMeshData != null) ? objPlayerMountCar.LoadingMeshData.ID : -1L);
				}
				else
				{
					Debug.Log("carModelData == null " + initData.MountData.ModelId);
				}
			}
			objPlayerMountCar.Reset(initData);
		}
		else
		{
			Debug.Log("No Mount Car!!!!!!!!!!!!!!!!!!!!!!");
		}
		return objPlayerMountCar;
	}

	// Token: 0x0600340D RID: 13325 RVA: 0x000D03E8 File Offset: 0x000CE5E8
	public void RecycleMountCar(ObjPlayerMountCar mountCar)
	{
		if (mountCar.LoadingMeshData != null)
		{
			mountCar.LoadingMeshData.OnLoadFinished = null;
			mountCar.LoadingMeshData = null;
		}
		if (mountCar.CurRideMountData != null && mountCar.CurRideMountData.Player != null)
		{
			mountCar.CurRideMountData.Player.LoadingMountFlag = false;
		}
		mountCar.ResetFlag = false;
		this.mMountCarPoolGroup.Recycle(mountCar, mountCar.CurRideMountData.MountData.ModelId);
	}

	// Token: 0x0600340E RID: 13326 RVA: 0x000D0468 File Offset: 0x000CE668
	public void RefreshPlayerGuildPic()
	{
		if (this.mMainPlayer != null)
		{
			this.mMainPlayer.RefreshHeadInfo();
		}
		for (int i = 0; i < this.mObjOtherPlayerVisibleList.Count; i++)
		{
			this.mObjOtherPlayerVisibleList[i].RefreshHeadInfo();
		}
	}

	// Token: 0x0600340F RID: 13327 RVA: 0x000D04C0 File Offset: 0x000CE6C0
	public float GetActiveNpcHpPercent()
	{
		long num = 0L;
		long num2 = 0L;
		for (int i = 0; i < this.mObjNpcPoolGroup.EnableNpcList.Count; i++)
		{
			num += this.mObjNpcPoolGroup.EnableNpcList[i].AttributeData.MaxHP;
			num2 += this.mObjNpcPoolGroup.EnableNpcList[i].AttributeData.HP;
		}
		if (num != 0L)
		{
			return (float)num2 / (float)num;
		}
		return 0f;
	}

	// Token: 0x04002222 RID: 8738
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002223 RID: 8739
	private ObjPlayerCar mMainPlayerCar;

	// Token: 0x04002224 RID: 8740
	private Dictionary<long, Obj> mObjDict = new Dictionary<long, Obj>();

	// Token: 0x04002225 RID: 8741
	private Dictionary<string, GameObject> mOtherObjDic = new Dictionary<string, GameObject>();

	// Token: 0x04002226 RID: 8742
	private List<ObjNPC> mOtherPlayerEscortNPCList = new List<ObjNPC>();

	// Token: 0x04002227 RID: 8743
	private ObjNpcPoolGroup mObjNpcPoolGroup;

	// Token: 0x04002228 RID: 8744
	private ObjOtherPlayerPool mOtherPlayerPool;

	// Token: 0x04002229 RID: 8745
	private List<ObjCharacter>[] mCampList = new List<ObjCharacter>[7];

	// Token: 0x0400222A RID: 8746
	private List<ObjCharacter>[] mCampTargetList = new List<ObjCharacter>[7];

	// Token: 0x0400222B RID: 8747
	private List<ObjNPC> mMapShowObjList = new List<ObjNPC>();

	// Token: 0x0400222C RID: 8748
	private List<int> mTargetCampList = new List<int>();

	// Token: 0x0400222D RID: 8749
	private Dictionary<long, ObjInitPlayerData> mOtherPlayerNoLogicDataDic = new Dictionary<long, ObjInitPlayerData>();

	// Token: 0x0400222E RID: 8750
	private List<ObjOtherPlayer> mObjOtherPlayerVisibleList = new List<ObjOtherPlayer>();

	// Token: 0x0400222F RID: 8751
	private List<ObjOtherPlayer> mObjOtherPlayerInVisibleList = new List<ObjOtherPlayer>();

	// Token: 0x04002230 RID: 8752
	private Dictionary<long, int> mPlayerMeshLoadNumDic = new Dictionary<long, int>();

	// Token: 0x04002231 RID: 8753
	private Dictionary<long, int> mPlayerMeshLoadTargetNumDic = new Dictionary<long, int>();

	// Token: 0x04002232 RID: 8754
	private SimplePoolGroup<ObjFakeAICar> mObjFakeAICarPoolGroup;

	// Token: 0x04002233 RID: 8755
	private int tt;

	// Token: 0x04002234 RID: 8756
	private SimplePoolGroup<ObjSimpleAICar> mObjSimpleAICarPoolGroup;

	// Token: 0x04002235 RID: 8757
	private int mPoliceSoundId = 35;

	// Token: 0x04002236 RID: 8758
	private SimplePoolGroup<ObjRagdollNPC> mObjRagdollNPCPoolGroup;

	// Token: 0x04002237 RID: 8759
	private SimplePoolGroup<ObjPatrolNPC> mObjPatrolNPCPoolGroup;

	// Token: 0x04002238 RID: 8760
	private SimplePool<BombSustainedRangeObj> mBombSustainedRangeObjPool;

	// Token: 0x04002239 RID: 8761
	private SimplePoolGroup<ObjPlayerMountCar> mMountCarPoolGroup;

	// Token: 0x02000ADD RID: 2781
	// (Invoke) Token: 0x06004FFD RID: 20477
	public delegate void OnGetNPC(ObjNPC npc);
}
