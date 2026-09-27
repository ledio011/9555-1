using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x02000825 RID: 2085
public class ObjNPC : ObjCharacter
{
	// Token: 0x06003410 RID: 13328 RVA: 0x000D0544 File Offset: 0x000CE744
	public ObjNPC()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_NPC;
	}

	// Token: 0x17000E8F RID: 3727
	// (get) Token: 0x06003411 RID: 13329 RVA: 0x000D05C8 File Offset: 0x000CE7C8
	public string NPCDataID
	{
		get
		{
			if (this.mNpcData != null)
			{
				return this.mNpcData.ID;
			}
			return "-1";
		}
	}

	// Token: 0x17000E90 RID: 3728
	// (get) Token: 0x06003412 RID: 13330 RVA: 0x000D05E8 File Offset: 0x000CE7E8
	// (set) Token: 0x06003413 RID: 13331 RVA: 0x000D05F0 File Offset: 0x000CE7F0
	public Material MeshMat
	{
		get
		{
			return this.mMeshMat;
		}
		set
		{
			this.mMeshMat = value;
		}
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x000D05FC File Offset: 0x000CE7FC
	protected void ShowMesh()
	{
		if (this.mMeshMat == null)
		{
			return;
		}
		if (this.mMeshMat.HasProperty("_DefaultColor"))
		{
			Color color = this.mMeshMat.GetColor("_DefaultColor");
			this.mMeshMat.SetColor("_Color", new Color(color.r, color.g, color.b, 0f));
			ShortcutExtensions.DOColor(this.mMeshMat, color, 1f);
		}
	}

	// Token: 0x17000E91 RID: 3729
	// (get) Token: 0x06003415 RID: 13333 RVA: 0x000D0684 File Offset: 0x000CE884
	// (set) Token: 0x06003416 RID: 13334 RVA: 0x000D068C File Offset: 0x000CE88C
	public NpcData NPCData
	{
		get
		{
			return this.mNpcData;
		}
		set
		{
			this.mNpcData = value;
		}
	}

	// Token: 0x17000E92 RID: 3730
	// (get) Token: 0x06003417 RID: 13335 RVA: 0x000D0698 File Offset: 0x000CE898
	// (set) Token: 0x06003418 RID: 13336 RVA: 0x000D06A0 File Offset: 0x000CE8A0
	public AutoMoveLogic AutoMoveLogic
	{
		get
		{
			return this.mAutoMoveLogic;
		}
		set
		{
			this.mAutoMoveLogic = value;
		}
	}

	// Token: 0x17000E93 RID: 3731
	// (get) Token: 0x06003419 RID: 13337 RVA: 0x000D06AC File Offset: 0x000CE8AC
	// (set) Token: 0x0600341A RID: 13338 RVA: 0x000D06B4 File Offset: 0x000CE8B4
	public AILogic AILogic
	{
		get
		{
			return this.mAILogic;
		}
		set
		{
			this.mAILogic = value;
		}
	}

	// Token: 0x17000E94 RID: 3732
	// (get) Token: 0x0600341B RID: 13339 RVA: 0x000D06C0 File Offset: 0x000CE8C0
	// (set) Token: 0x0600341C RID: 13340 RVA: 0x000D06C8 File Offset: 0x000CE8C8
	public BundleManager.LoadModelData LoadingModelData
	{
		get
		{
			return this.mLoadingModelData;
		}
		set
		{
			this.mLoadingModelData = value;
		}
	}

	// Token: 0x17000E95 RID: 3733
	// (get) Token: 0x0600341D RID: 13341 RVA: 0x000D06D4 File Offset: 0x000CE8D4
	// (set) Token: 0x0600341E RID: 13342 RVA: 0x000D06DC File Offset: 0x000CE8DC
	public long LoadingModelDataId
	{
		get
		{
			return this.mLoadingModelDataId;
		}
		set
		{
			this.mLoadingModelDataId = value;
		}
	}

	// Token: 0x17000E96 RID: 3734
	// (get) Token: 0x0600341F RID: 13343 RVA: 0x000D06E8 File Offset: 0x000CE8E8
	// (set) Token: 0x06003420 RID: 13344 RVA: 0x000D06F0 File Offset: 0x000CE8F0
	public GameObject MeshRoot
	{
		get
		{
			return this.mMeshRoot;
		}
		set
		{
			this.mMeshRoot = value;
		}
	}

	// Token: 0x17000E97 RID: 3735
	// (get) Token: 0x06003421 RID: 13345 RVA: 0x000D06FC File Offset: 0x000CE8FC
	// (set) Token: 0x06003422 RID: 13346 RVA: 0x000D0704 File Offset: 0x000CE904
	public Vector3 BornPos
	{
		get
		{
			return this.mBornPos;
		}
		set
		{
			this.mBornPos = value;
		}
	}

	// Token: 0x17000E98 RID: 3736
	// (get) Token: 0x06003423 RID: 13347 RVA: 0x000D0710 File Offset: 0x000CE910
	// (set) Token: 0x06003424 RID: 13348 RVA: 0x000D0718 File Offset: 0x000CE918
	public float PatrolRange
	{
		get
		{
			return this.mPatrolRange;
		}
		set
		{
			this.mPatrolRange = value;
		}
	}

	// Token: 0x17000E99 RID: 3737
	// (get) Token: 0x06003425 RID: 13349 RVA: 0x000D0724 File Offset: 0x000CE924
	// (set) Token: 0x06003426 RID: 13350 RVA: 0x000D072C File Offset: 0x000CE92C
	public float SearchRange
	{
		get
		{
			return this.mSearchRange;
		}
		set
		{
			this.mSearchRange = value;
		}
	}

	// Token: 0x17000E9A RID: 3738
	// (get) Token: 0x06003427 RID: 13351 RVA: 0x000D0738 File Offset: 0x000CE938
	// (set) Token: 0x06003428 RID: 13352 RVA: 0x000D0740 File Offset: 0x000CE940
	public Transform FollowTarget
	{
		get
		{
			return this.mFollowTarget;
		}
		set
		{
			this.mFollowTarget = value;
		}
	}

	// Token: 0x17000E9B RID: 3739
	// (get) Token: 0x06003429 RID: 13353 RVA: 0x000D074C File Offset: 0x000CE94C
	// (set) Token: 0x0600342A RID: 13354 RVA: 0x000D0754 File Offset: 0x000CE954
	public GameDefine.NPC_FUNCTION_TYPE NPCFunctionType
	{
		get
		{
			return this.mNPCFunctionType;
		}
		set
		{
			this.mNPCFunctionType = value;
		}
	}

	// Token: 0x17000E9C RID: 3740
	// (get) Token: 0x0600342B RID: 13355 RVA: 0x000D0760 File Offset: 0x000CE960
	// (set) Token: 0x0600342C RID: 13356 RVA: 0x000D0768 File Offset: 0x000CE968
	public GameDefine.NPC_TYPE NPCType
	{
		get
		{
			return this.mNPCType;
		}
		set
		{
			this.mNPCType = value;
		}
	}

	// Token: 0x17000E9D RID: 3741
	// (get) Token: 0x0600342D RID: 13357 RVA: 0x000D0774 File Offset: 0x000CE974
	// (set) Token: 0x0600342E RID: 13358 RVA: 0x000D077C File Offset: 0x000CE97C
	public ObjCharacter SelectedTarget
	{
		get
		{
			return this.mSelectedTarget;
		}
		set
		{
			this.mSelectedTarget = value;
		}
	}

	// Token: 0x17000E9E RID: 3742
	// (get) Token: 0x0600342F RID: 13359 RVA: 0x000D0788 File Offset: 0x000CE988
	// (set) Token: 0x06003430 RID: 13360 RVA: 0x000D0790 File Offset: 0x000CE990
	public string PathID
	{
		get
		{
			return this.mPathID;
		}
		set
		{
			this.mPathID = value;
		}
	}

	// Token: 0x17000E9F RID: 3743
	// (get) Token: 0x06003431 RID: 13361 RVA: 0x000D079C File Offset: 0x000CE99C
	public List<SkillData> EnableSkillIDList
	{
		get
		{
			return this.mEnableSkillIDList;
		}
	}

	// Token: 0x06003432 RID: 13362 RVA: 0x000D07A4 File Offset: 0x000CE9A4
	public static Vector3 HeadingToVector3(float heading)
	{
		float num = Mathf.Sin(heading * 0.017453292f);
		float num2 = Mathf.Cos(heading * 0.017453292f);
		return new Vector3(num, 0f, num2);
	}

	// Token: 0x17000EA0 RID: 3744
	// (get) Token: 0x06003433 RID: 13363 RVA: 0x000D07D8 File Offset: 0x000CE9D8
	public string PlayerName
	{
		get
		{
			return this.mPlayerName;
		}
	}

	// Token: 0x17000EA1 RID: 3745
	// (get) Token: 0x06003434 RID: 13364 RVA: 0x000D07E0 File Offset: 0x000CE9E0
	// (set) Token: 0x06003435 RID: 13365 RVA: 0x000D07E8 File Offset: 0x000CE9E8
	public long GuildId
	{
		get
		{
			return this.mGuildId;
		}
		set
		{
			this.mGuildId = value;
		}
	}

	// Token: 0x17000EA2 RID: 3746
	// (get) Token: 0x06003436 RID: 13366 RVA: 0x000D07F4 File Offset: 0x000CE9F4
	// (set) Token: 0x06003437 RID: 13367 RVA: 0x000D07FC File Offset: 0x000CE9FC
	public long TeamId
	{
		get
		{
			return this.mTeamId;
		}
		set
		{
			this.mTeamId = value;
		}
	}

	// Token: 0x17000EA3 RID: 3747
	// (get) Token: 0x06003438 RID: 13368 RVA: 0x000D0808 File Offset: 0x000CEA08
	// (set) Token: 0x06003439 RID: 13369 RVA: 0x000D0810 File Offset: 0x000CEA10
	public string DefaultDialogID
	{
		get
		{
			return this.mDefaultDialogID;
		}
		set
		{
			this.mDefaultDialogID = value;
		}
	}

	// Token: 0x17000EA4 RID: 3748
	// (get) Token: 0x0600343A RID: 13370 RVA: 0x000D081C File Offset: 0x000CEA1C
	// (set) Token: 0x0600343B RID: 13371 RVA: 0x000D0824 File Offset: 0x000CEA24
	public List<string> MissionIdList
	{
		get
		{
			return this.mMissionIdList;
		}
		set
		{
			this.mMissionIdList = value;
		}
	}

	// Token: 0x0600343C RID: 13372 RVA: 0x000D0830 File Offset: 0x000CEA30
	private void AddMissionToList(string missionId)
	{
		if (!string.IsNullOrEmpty(missionId))
		{
			this.mMissionIdList.Add(missionId);
		}
	}

	// Token: 0x0600343D RID: 13373 RVA: 0x000D084C File Offset: 0x000CEA4C
	private void ClearMissionList()
	{
		this.mMissionIdList.Clear();
	}

	// Token: 0x0600343E RID: 13374 RVA: 0x000D085C File Offset: 0x000CEA5C
	public bool IsContainsMission(string missionId)
	{
		return this.mMissionIdList.Contains(missionId);
	}

	// Token: 0x0600343F RID: 13375 RVA: 0x000D0874 File Offset: 0x000CEA74
	protected void AddDialogMission()
	{
		if (this.IsMissionNpc())
		{
			NPCDialogData npcdialogDataByID = DataManager.GetNPCDialogDataByID(this.mDefaultDialogID);
			if (npcdialogDataByID == null)
			{
				Debug.LogWarning("mission npc no dialogData");
				return;
			}
			if (npcdialogDataByID.MissionIDList != null && npcdialogDataByID.MissionIDList.Count != 0)
			{
				for (int i = 0; i < npcdialogDataByID.MissionIDList.Count; i++)
				{
					this.AddMissionToList(npcdialogDataByID.MissionIDList[i]);
				}
			}
		}
	}

	// Token: 0x06003440 RID: 13376 RVA: 0x000D08F4 File Offset: 0x000CEAF4
	public bool CheckInDialogRange()
	{
		float num = (float)((!Singleton<ObjManager>.Instance.MainPlayer.IsLocalDrivingCar) ? 5 : 10);
		return VectorXZ.Distance(base.Position, Singleton<ObjManager>.Instance.MainPlayer.Position) < num;
	}

	// Token: 0x06003441 RID: 13377 RVA: 0x000D094C File Offset: 0x000CEB4C
	public bool IsMissionNpc()
	{
		return this.mNPCType == GameDefine.NPC_TYPE.MISSION;
	}

	// Token: 0x06003442 RID: 13378 RVA: 0x000D0958 File Offset: 0x000CEB58
	public bool IsSoundBoxNpc()
	{
		return this.mNpcData != null && this.mNpcData.FunctionType == 3;
	}

	// Token: 0x06003443 RID: 13379 RVA: 0x000D0978 File Offset: 0x000CEB78
	public bool IsShopFunctionNpc()
	{
		return this.mNpcData != null && this.mNpcData.FunctionType >= 4 && this.mNpcData.FunctionType <= 9;
	}

	// Token: 0x06003444 RID: 13380 RVA: 0x000D09AC File Offset: 0x000CEBAC
	public bool IsCityCaptureNpc()
	{
		return this.mNpcData != null && this.mNpcData.FunctionType == 10 && this.mNpcData.Group == 4;
	}

	// Token: 0x06003445 RID: 13381 RVA: 0x000D09E8 File Offset: 0x000CEBE8
	public void ChangeToAttackNPC()
	{
		this.mDefaultDialogID = string.Empty;
		if (this.mAILogic == null)
		{
			this.mAILogic = base.gameObject.AddComponent<AILogic>();
			this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, this.mPathID);
		}
		this.mNavMeshAgent.enabled = true;
		this.AttributeData.HP = this.AttributeData.MaxHP;
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x000D0A6C File Offset: 0x000CEC6C
	public void ChangeNpcAI(string aiId)
	{
		if (this.mAILogic == null)
		{
			this.mAILogic = base.gameObject.AddComponent<AILogic>();
			this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, this.mPathID);
		}
		else
		{
			this.mAILogic.ResetAI(string.Empty, aiId, this.mPathID);
		}
	}

	// Token: 0x06003447 RID: 13383 RVA: 0x000D0AE0 File Offset: 0x000CECE0
	public override void Init()
	{
		base.Init();
		if (this.mAutoMoveLogic == null)
		{
			this.mAutoMoveLogic = base.gameObject.AddComponent<AutoMoveLogic>();
		}
		this.mAutoMoveLogic.Init(this);
	}

	// Token: 0x06003448 RID: 13384 RVA: 0x000D0B24 File Offset: 0x000CED24
	public void InitNPCHeadInfo()
	{
		if (this.mNPCType != GameDefine.NPC_TYPE.BOSS)
		{
			ResourcesManager.LoadHeadInfoPrefab(UIInfo.NPCHeadInfoUI, "NPCHeadInfoRoot", new ResourcesManager.LoadHeadInfoDelegate(this.LoadNPCHeadInfo));
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BossXueTiaoUI, delegate
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.RegisterBoss(this);
				SingletonUnity<BossXueTiaoLogicNew>.Instance.resetHPinfo(this.AttributeData);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BossXueTiaoUI);
				SingletonUnity<UIManager>.Instance.CheckFunctionTop(false);
			}, null);
		}
	}

	// Token: 0x17000EA5 RID: 3749
	// (get) Token: 0x06003449 RID: 13385 RVA: 0x000D0B7C File Offset: 0x000CED7C
	// (set) Token: 0x0600344A RID: 13386 RVA: 0x000D0B84 File Offset: 0x000CED84
	public NPCHeadInfoLogic NpcHeadInfoLogic
	{
		get
		{
			return this.mNpcHeadInfoLogic;
		}
		set
		{
			this.mNpcHeadInfoLogic = value;
		}
	}

	// Token: 0x0600344B RID: 13387 RVA: 0x000D0B90 File Offset: 0x000CED90
	public void UpdateNpcHeadInfo(HEAD_PIC_TYPE type)
	{
		if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
		{
			return;
		}
		this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, type, false);
	}

	// Token: 0x0600344C RID: 13388 RVA: 0x000D0BD0 File Offset: 0x000CEDD0
	private void LoadNPCHeadInfo(GameObject obj)
	{
		if (obj != null)
		{
			BillBoard billBoard = obj.GetComponent<BillBoard>();
			if (billBoard == null)
			{
				billBoard = obj.AddComponent<BillBoard>();
			}
			billBoard.enabled = true;
			billBoard.BindObj = base.gameObject;
			billBoard.DeltaHeight = base.CurrentCharacterModelData.ModelHeight + 0.3f;
			this.mNpcHeadInfoLogic = obj.GetComponent<NPCHeadInfoLogic>();
			this.mHeadInfoLogic = this.mNpcHeadInfoLogic;
			if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
			{
				return;
			}
			if (this.mNpcData.Type == 4)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if ((playerData.IsHaveTeam() && playerData.TeamInfo.TeamID == this.mTeamId) || (playerData.IsHaveGuild() && playerData.PlayerGuild.ServerId == this.mGuildId))
				{
					this.mNpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.mPlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.SELF_ESCORT_NPC, true);
				}
				else
				{
					CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
					if (escortMission != null && escortMission.GetParam(1) == this.ServerId)
					{
						this.mNpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.mPlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.SELF_ESCORT_NPC, true);
					}
					else
					{
						this.mNpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.mPlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.OTHER_ESCORT_NPC, true);
					}
				}
			}
			else if (this.AttributeData.Camp == GameDefine.CAMP_TYPE.FUNCTION_NPC)
			{
				this.UpdateMissionNpcHead();
			}
			else
			{
				this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.INVALID, false);
			}
			this.mNpcHeadInfoLogic.ForceSetHpVal((float)this.AttributeData.HP / (float)this.AttributeData.MaxHP);
		}
	}

	// Token: 0x0600344D RID: 13389 RVA: 0x000D0DE8 File Offset: 0x000CEFE8
	public void UpdateMissionNpcHead()
	{
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		Dictionary<string, List<string>> curMissionNpcDic = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurMissionNpcDic;
		if (curMissionNpcDic.ContainsKey(this.NPCDataID) && curMissionNpcDic[this.NPCDataID] != null && curMissionNpcDic[this.NPCDataID].Count > 0)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			MISSION_STATE mission_STATE = MISSION_STATE.INVALID;
			string text = string.Empty;
			for (int i = 0; i < curMissionNpcDic[this.NPCDataID].Count; i++)
			{
				MISSION_STATE missionState = missionManager.GetMissionState(curMissionNpcDic[this.NPCDataID][i]);
				if (missionState == MISSION_STATE.COMPLETE)
				{
					MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionNpcDic[this.NPCDataID][i]);
					if (level >= missionDataByID.MinLv)
					{
						mission_STATE = MISSION_STATE.COMPLETE;
						text = curMissionNpcDic[this.NPCDataID][i];
						break;
					}
					text = curMissionNpcDic[this.NPCDataID][i];
				}
				else if (missionState == MISSION_STATE.ACCEPTED)
				{
					mission_STATE = MISSION_STATE.ACCEPTED;
					text = curMissionNpcDic[this.NPCDataID][i];
				}
			}
			if (mission_STATE == MISSION_STATE.ACCEPTED)
			{
				this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.MISSION_ACCEPT_NPC, GameSettingData.IsShowNormalNpcName);
			}
			else if (mission_STATE == MISSION_STATE.COMPLETE)
			{
				this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.MISSION_COMPLETE_NPC, GameSettingData.IsShowNormalNpcName);
			}
			else if (!string.IsNullOrEmpty(text) && missionManager.IsMissionAccepted(text))
			{
				this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.INVALID, false);
			}
			else
			{
				this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.MISSION_TARGET_NPC, GameSettingData.IsShowNormalNpcName);
			}
		}
		else
		{
			this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.INVALID, false);
		}
	}

	// Token: 0x0600344E RID: 13390 RVA: 0x000D1028 File Offset: 0x000CF228
	public void UpdateEscortNpcCamp(long guildId, long teamId)
	{
		if (this.NPCData.Type == 4)
		{
			this.GuildId = guildId;
			this.TeamId = teamId;
			this.UpdateEscortNpcCamp();
		}
	}

	// Token: 0x0600344F RID: 13391 RVA: 0x000D1050 File Offset: 0x000CF250
	public void UpdateEscortNpcCamp()
	{
		if (this.NPCData.Type == 4 && !base.IsDie)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if ((playerData.IsHaveGuild() && playerData.PlayerGuild.ServerId == this.GuildId) || (playerData.IsHaveTeam() && playerData.TeamInfo.TeamID == this.TeamId))
			{
				if (this.AttributeData.Camp == GameDefine.CAMP_TYPE.NORMAL_NPC)
				{
					Singleton<ObjManager>.Instance.ChangeCamp(this, this.AttributeData.Camp, (GameDefine.CAMP_TYPE)this.NPCData.Group);
					this.AttributeData.Camp = (GameDefine.CAMP_TYPE)this.NPCData.Group;
					this.NpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.PlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.SELF_ESCORT_NPC, true);
					if (Singleton<ObjManager>.Instance.MainPlayer.SelectedTarget != null && Singleton<ObjManager>.Instance.MainPlayer.SelectedTarget.ServerId == this.ServerId)
					{
						Singleton<ObjManager>.Instance.MainPlayer.SelectTarget(null);
					}
				}
			}
			else
			{
				CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
				if (escortMission != null && escortMission.GetParam(1) == this.ServerId)
				{
					if (this.AttributeData.Camp == GameDefine.CAMP_TYPE.NORMAL_NPC)
					{
						Singleton<ObjManager>.Instance.ChangeCamp(this, this.AttributeData.Camp, (GameDefine.CAMP_TYPE)this.NPCData.Group);
						this.AttributeData.Camp = (GameDefine.CAMP_TYPE)this.NPCData.Group;
						this.NpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.PlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.SELF_ESCORT_NPC, true);
						if (Singleton<ObjManager>.Instance.MainPlayer.SelectedTarget != null && Singleton<ObjManager>.Instance.MainPlayer.SelectedTarget.ServerId == this.ServerId)
						{
							Singleton<ObjManager>.Instance.MainPlayer.SelectTarget(null);
						}
					}
				}
				else if (this.AttributeData.Camp != GameDefine.CAMP_TYPE.NORMAL_NPC)
				{
					Singleton<ObjManager>.Instance.ChangeCamp(this, this.AttributeData.Camp, GameDefine.CAMP_TYPE.NORMAL_NPC);
					this.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
					this.NpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.PlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.OTHER_ESCORT_NPC, true);
				}
			}
		}
	}

	// Token: 0x06003450 RID: 13392 RVA: 0x000D12EC File Offset: 0x000CF4EC
	public void LoadModelFinishInit()
	{
		if (this.IsCityCaptureNpc())
		{
			Transform transform = base.transform.FindChild("MeshRoot/effect_Smoke");
			Transform transform2 = base.transform.FindChild("MeshRoot/effect_baoZha");
			if (transform != null)
			{
				this.SmokeParticle = transform.gameObject.GetComponent<ParticleSystem>();
				this.SmokeParticle.Stop();
				UnityVersionUtil.SetActiveRecursive(this.SmokeParticle.gameObject, false);
			}
			if (transform2 != null)
			{
				this.ExplosionParticle = transform2.gameObject.GetComponent<ParticleSystem>();
				this.ExplosionParticle.Stop();
				UnityVersionUtil.SetActiveRecursive(this.ExplosionParticle.gameObject, false);
			}
			if (this.FLWheel == null)
			{
				this.FLWheel = base.transform.FindChild("MeshRoot/qianlun-L").transform;
				this.FLPos = this.FLWheel.transform.localPosition;
			}
			if (this.FRWheel == null)
			{
				this.FRWheel = base.transform.FindChild("MeshRoot/qianlun-R").transform;
				this.FRPos = this.FRWheel.transform.localPosition;
			}
			if (this.BLWheel == null)
			{
				this.BLWheel = base.transform.FindChild("MeshRoot/houlun-L").transform;
				this.BLPos = this.BLWheel.transform.localPosition;
			}
			if (this.BRWheel == null)
			{
				this.BRWheel = base.transform.FindChild("MeshRoot/houlun-R").transform;
				this.BRPos = this.BRWheel.transform.localPosition;
			}
		}
		if (!this.IsSoundBoxNpc() && !this.IsCityCaptureNpc())
		{
			this.mAnimationLogic.Init(this);
		}
		if (this.onLoadMeshFinished != null)
		{
			this.onLoadMeshFinished(this);
		}
		this.ShowMesh();
		if (this.IsCityCaptureNpc() && this.AttributeData.HP <= 0L)
		{
			base.IsDie = false;
			this.ChangeHPVal(0L);
		}
	}

	// Token: 0x06003451 RID: 13393 RVA: 0x000D150C File Offset: 0x000CF70C
	private void OnDisable()
	{
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.Stop();
			this.mNavMeshAgent.ResetPath();
			this.mNavMeshAgent.enabled = false;
		}
	}

	// Token: 0x06003452 RID: 13394 RVA: 0x000D155C File Offset: 0x000CF75C
	public virtual void ResetNpc(ObjInitNpcData initData)
	{
		this.mNPCType = (GameDefine.NPC_TYPE)initData.npcInfoData.Type;
		base.Reset();
		this.ServerId = initData.mServerID;
		base.Position = initData.mPos;
		this.mTransform.forward = initData.mDir;
		this.mNpcData = initData.npcInfoData;
		this.BornPos = initData.mPos;
		this.mPlayerName = initData.PlayerName;
		this.mTeamId = initData.TeamId;
		this.mGuildId = initData.GuildId;
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		if (this.mNpcData.Type == 4)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if ((playerData.IsHaveTeam() && playerData.TeamInfo.TeamID == this.mTeamId) || (playerData.IsHaveGuild() && playerData.PlayerGuild.ServerId == this.mGuildId))
			{
				this.AttributeData.Camp = (GameDefine.CAMP_TYPE)initData.npcInfoData.Group;
			}
			else if (missionManager.IsInEscortMission() && this.ServerId == missionManager.GetEscortMission().GetParam(1))
			{
				this.AttributeData.Camp = (GameDefine.CAMP_TYPE)initData.npcInfoData.Group;
			}
			else
			{
				this.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
			}
			UIUpdateEvent.OnChangeTeam = (DelegateDefine.NoParamDelegate)Delegate.Combine(UIUpdateEvent.OnChangeTeam, new DelegateDefine.NoParamDelegate(this.UpdateEscortNpcCamp));
			UIUpdateEvent.OnChangeGuild = (DelegateDefine.NoParamDelegate)Delegate.Combine(UIUpdateEvent.OnChangeGuild, new DelegateDefine.NoParamDelegate(this.UpdateEscortNpcCamp));
		}
		else
		{
			this.AttributeData.Camp = (GameDefine.CAMP_TYPE)initData.npcInfoData.Group;
		}
		this.mNPCFunctionType = (GameDefine.NPC_FUNCTION_TYPE)initData.npcInfoData.FunctionType;
		this.AttributeData.MaxHP = initData.MaxHP;
		this.AttributeData.HP = initData.HP;
		this.AttributeData.Name = initData.npcInfoData.Name;
		this.AttributeData.CurATK = (float)initData.ATK;
		this.AttributeData.CurDEF = (float)initData.DEF;
		this.AttributeData.CurEXD = (float)initData.EXD / 10000f;
		this.AttributeData.CurEXR = (float)initData.EXR / 10000f;
		this.AttributeData.CurHIT = (float)initData.HIT;
		this.AttributeData.CurDGE = (float)initData.EVA;
		this.AttributeData.CurCRI = (float)initData.CRI;
		this.AttributeData.CurRES = (float)initData.RES;
		this.AttributeData.CurCRD = (float)initData.CRD / 10000f;
		this.AttributeData.CurCRR = (float)initData.CRR / 10000f;
		this.AttributeData.CurDEFA = initData.DEFA;
		this.AttributeData.CurDGEA = initData.DGEA;
		this.AttributeData.CurRESA = initData.RESA;
		this.AttributeData.CurHITA = initData.HITA;
		this.AttributeData.CurCRIA = initData.CRIA;
		this.AttributeData.CurAntiKnockDown = (float)initData.AntiKnockDown / 10000f;
		this.AttributeData.CurAntiStun = (float)initData.AntiStun / 10000f;
		this.AttributeData.Level = initData.Level;
		this.AttributeData.CurSpeed = initData.npcInfoData.MoveSpeedMeter;
		this.AttributeData.WalkSpeed = initData.npcInfoData.WalkSpeedMeter;
		this.mPatrolRange = initData.npcInfoData.PatrolRadius;
		this.mSearchRange = initData.npcInfoData.SearchRadius;
		this.mPathID = initData.PathID;
		if (this.dieDelayHandle != null)
		{
			this.dieDelayHandle.Cancel();
		}
		this.mDefaultDialogID = initData.npcInfoData.TalkGroup;
		this.InitSkill(this.mNpcData.SkillList);
		base.InitNavMeshAgent();
		this.mNavMeshAgent.walkableMask += 56;
		this.InitNPCHeadInfo();
		this.AddDialogMission();
		if (this.mAutoMoveLogic != null)
		{
			this.mAutoMoveLogic.Reset();
		}
		if (this.mMeshRoot != null)
		{
			this.mMeshRoot.transform.localScale = Vector3.one * initData.npcInfoData.ModelScale;
		}
		CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
		if (component != null)
		{
			component.height = base.CurrentCharacterModelData.ModelHeight * 1.2f;
			component.center = Vector3.up * base.CurrentCharacterModelData.ModelHeight / 2f * 1.2f;
			component.radius = base.CurrentCharacterModelData.ModelRadius;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsLowPhoneManager())
		{
			if (!this.IsMissionNpc() && this.AttributeData.Camp != GameDefine.CAMP_TYPE.FUNCTION_NPC)
			{
				if (this.mAILogic == null)
				{
					this.mAILogic = base.gameObject.AddComponent<AILogic>();
				}
				if (this.mAILogic != null)
				{
					this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, initData.PathID);
				}
			}
		}
		else if (this.mNpcData.AI.Equals("FollowAI"))
		{
			if (this.mAILogic == null)
			{
				this.mAILogic = base.gameObject.AddComponent<AILogic>();
			}
			if (this.mAILogic != null)
			{
				this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, initData.PathID);
			}
		}
		else
		{
			if (this.mAILogic != null)
			{
				Object.Destroy(this.mAILogic);
				this.mAILogic = null;
			}
			if (this.IsMissionNpc() || this.IsSoundBoxNpc() || this.IsCityCaptureNpc())
			{
				this.mNavMeshAgent.enabled = false;
				NavMeshHit navMeshHit;
				NavMesh.SamplePosition(initData.mPos, ref navMeshHit, 1f, -1);
				base.Position = navMeshHit.position;
			}
			else
			{
				this.mNavMeshAgent.enabled = true;
			}
		}
		this.mServerNotDieNumCount = 0;
		this.ShowMesh();
		if (this.IsCityCaptureNpc())
		{
			if (this.AttributeData.HP <= 0L)
			{
				this.ChangeHPVal(0L);
			}
			else
			{
				if (this.SmokeParticle != null)
				{
					this.SmokeParticle.Stop();
					UnityVersionUtil.SetActiveRecursive(this.SmokeParticle.gameObject, false);
				}
				if (this.ExplosionParticle != null)
				{
					this.ExplosionParticle.Stop();
					UnityVersionUtil.SetActiveRecursive(this.ExplosionParticle.gameObject, false);
				}
				if (this.MeshRoot != null)
				{
					this.MeshRoot.SampleAnimation(this.MeshRoot.animation.GetClip("GTACheBaoZa_Animation"), 0f);
					if (this.FLWheel != null)
					{
						this.FLWheel.localPosition = this.FLPos;
					}
					if (this.FRWheel != null)
					{
						this.FRWheel.localPosition = this.FRPos;
					}
					if (this.BLWheel != null)
					{
						this.BLWheel.localPosition = this.BLPos;
					}
					if (this.BRWheel != null)
					{
						this.BRWheel.localPosition = this.BRPos;
					}
				}
			}
		}
	}

	// Token: 0x06003453 RID: 13395 RVA: 0x000D1D20 File Offset: 0x000CFF20
	public void OnRecycle()
	{
		if (this.NPCType == GameDefine.NPC_TYPE.ESCORT)
		{
			UIUpdateEvent.OnChangeTeam = (DelegateDefine.NoParamDelegate)Delegate.Remove(UIUpdateEvent.OnChangeTeam, new DelegateDefine.NoParamDelegate(this.UpdateEscortNpcCamp));
			UIUpdateEvent.OnChangeGuild = (DelegateDefine.NoParamDelegate)Delegate.Remove(UIUpdateEvent.OnChangeGuild, new DelegateDefine.NoParamDelegate(this.UpdateEscortNpcCamp));
		}
	}

	// Token: 0x06003454 RID: 13396 RVA: 0x000D1D7C File Offset: 0x000CFF7C
	public void ChangeBornPos(Vector3 pos)
	{
		this.BornPos = pos;
	}

	// Token: 0x06003455 RID: 13397 RVA: 0x000D1D88 File Offset: 0x000CFF88
	public void InitSkill(string[] skillList)
	{
		this.CharacterSkillData.Clear();
		this.mEnableSkillIDList.Clear();
		if (skillList == null)
		{
			return;
		}
		List<SkillData> list = new List<SkillData>();
		for (int i = 0; i < skillList.Length; i++)
		{
			this.CharacterSkillData.Add(new CharacterSkillData(skillList[i]));
			list.Add(DataManager.GetSkillDataById(skillList[i]));
		}
		list.Sort((SkillData x, SkillData y) => y.PriorityAutoCombat - x.PriorityAutoCombat);
		for (int j = 0; j < list.Count; j++)
		{
			this.mEnableSkillIDList.Add(list[j]);
		}
	}

	// Token: 0x06003456 RID: 13398 RVA: 0x000D1E3C File Offset: 0x000D003C
	private void Start()
	{
	}

	// Token: 0x06003457 RID: 13399 RVA: 0x000D1E40 File Offset: 0x000D0040
	private void Update()
	{
		if (this.IsSoundBoxNpc())
		{
			return;
		}
		if (this.IsCityCaptureNpc())
		{
			this.updateBossHPState();
			return;
		}
		base.UpdateComponent();
		base.UpdateMove();
		base.UpdateSkillCD();
		base.UpdateHoldTime();
		base.SkillLogic.UpdateSkill();
		this.updateBossHPState();
	}

	// Token: 0x06003458 RID: 13400 RVA: 0x000D1E94 File Offset: 0x000D0094
	private void FixedUpdate()
	{
		if (this.IsShopFunctionNpc())
		{
			if (null == this.mMainPlayerTransform)
			{
				if (null != Singleton<ObjManager>.Instance.MainPlayer)
				{
					this.mMainPlayerTransform = Singleton<ObjManager>.Instance.MainPlayer.transform;
				}
				if (null == this.mMainPlayerTransform)
				{
					return;
				}
			}
			if (null != Singleton<ObjManager>.Instance.MainPlayer)
			{
				this.sqrDis = (this.mMainPlayerTransform.position - base.CacheTransform.position).sqrMagnitude;
				if (this.sqrDis <= this.SqrActiveRadius)
				{
					if (!this.mInCircleFlag)
					{
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ActivityTipstRoot, delegate
						{
							SingletonUnity<ActivityTipsRootLogic>.Instance.ShowShopNpcInfo(this);
						}, null);
						this.mInCircleFlag = true;
					}
				}
				else if (this.sqrDis > this.ExitSqrActiveRadius)
				{
					if (this.mInCircleFlag && SingletonUnity<ActivityTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityTipsRootLogic>.Instance.gameObject))
					{
						SingletonUnity<ActivityTipsRootLogic>.Instance.CloseUI();
					}
					this.mInCircleFlag = false;
				}
			}
		}
	}

	// Token: 0x06003459 RID: 13401 RVA: 0x000D1FC8 File Offset: 0x000D01C8
	public void UseSkill(string skillId, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
		if (base.BeforeSkillCheck())
		{
			return;
		}
		if (this.mMeshRoot == null)
		{
			return;
		}
		ObjCharacter objCharacter = null;
		if (this.mSelectedTarget != null && this.mSelectedTarget.ServerId != this.ServerId)
		{
			if (this.mSelectedTarget.IsDie)
			{
				this.mSelectedTarget = null;
			}
			else
			{
				objCharacter = this.mSelectedTarget;
			}
		}
		if (objCharacter == null)
		{
			objCharacter = this.ChooseTarget(9999f);
			this.mSelectedTarget = objCharacter;
		}
		this.mCurUseSkillId = skillId;
		this.EnterCombat(objCharacter, onSkillUseSuccess);
	}

	// Token: 0x0600345A RID: 13402 RVA: 0x000D2070 File Offset: 0x000D0270
	public ObjCharacter ChooseTarget(float minSqrDis = 9999f)
	{
		ObjCharacter result = null;
		bool flag = false;
		bool flag2 = false;
		this.mCurSearchTargetList = Singleton<ObjManager>.Instance.CampTargetList[(int)this.AttributeData.Camp];
		for (int i = 0; i < this.mCurSearchTargetList.Count; i++)
		{
			if (this.mCurSearchTargetList[i].ServerId != this.ServerId)
			{
				if (!this.mCurSearchTargetList[i].IsDie)
				{
					if (this.mCurSearchTargetList[i].AttributeData.Camp != GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC || this.mCurSearchTargetList[i].ObjType != GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR)
					{
						if (CampTool.IsFirstClass(this.AttributeData.Camp, this.mCurSearchTargetList[i].AttributeData.Camp))
						{
							flag2 = true;
						}
						if (!flag)
						{
							float pathDistance = base.GetPathDistance(this.mCurSearchTargetList[i].Position);
							if (flag2)
							{
								flag = true;
								result = this.mCurSearchTargetList[i];
								minSqrDis = pathDistance;
							}
							else if (minSqrDis > pathDistance)
							{
								result = this.mCurSearchTargetList[i];
								minSqrDis = pathDistance;
							}
						}
						else if (flag2)
						{
							float pathDistance = base.GetPathDistance(this.mCurSearchTargetList[i].Position);
							if (minSqrDis > pathDistance)
							{
								result = this.mCurSearchTargetList[i];
								minSqrDis = pathDistance;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600345B RID: 13403 RVA: 0x000D21F8 File Offset: 0x000D03F8
	public override void EnterCombat(ObjCharacter target, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(base.CurUseSkillId);
		if (skillDataById == null)
		{
			return;
		}
		if (!(target != null))
		{
			return;
		}
		if (target.IsLocalDrivingCar)
		{
			target = target.CurPlayerCar;
		}
		if (!base.CheckSkillDistance(skillDataById, target))
		{
			this.MoveTo(target.Position, skillDataById.TraceDistanceMeter + target.ModelRadius + this.ModelRadius - 0.5f, null);
			return;
		}
		if (!base.CheckSkillCD(skillDataById))
		{
			return;
		}
		if (onSkillUseSuccess != null)
		{
			this.mSkillUseSuccessDic.Add(base.CurUseSkillId, onSkillUseSuccess);
		}
		if (target != null)
		{
			base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, target.ServerId);
		}
		else
		{
			base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, -1L);
		}
	}

	// Token: 0x0600345C RID: 13404 RVA: 0x000D22E0 File Offset: 0x000D04E0
	public override void OnSkillUseSuccess(string skillId)
	{
		base.OnSkillUseSuccess(skillId);
		this.OnSkillDisable(base.CurUseSkillId);
	}

	// Token: 0x0600345D RID: 13405 RVA: 0x000D22F8 File Offset: 0x000D04F8
	public override void ChangeHPVal(long newHP)
	{
		if (!base.IsDie)
		{
			if (newHP > this.AttributeData.HP && Time.time - this.mReceiveBiggerHpTimeCount < this.mReceiveBiggerHpTime)
			{
				return;
			}
			if (this.AttributeData.HP != newHP)
			{
				this.AttributeData.HP = newHP;
				this.UpdateHeadInfo();
			}
			if (this.mNPCType == GameDefine.NPC_TYPE.BOSS && SingletonUnity<BossXueTiaoLogicNew>.Exists)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.ChangeHP(newHP, this);
			}
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
				if (!this.IsCityCaptureNpc())
				{
					float delay = 4f;
					if (this.dieDelayHandle == null)
					{
						this.dieDelayHandle = new vp_Timer.Handle();
					}
					vp_Timer.In(delay, delegate()
					{
						this.DelayRecycle();
					}, this.dieDelayHandle);
				}
			}
		}
		else if (newHP <= 0L)
		{
			if (!this.IsCityCaptureNpc())
			{
				float delay2 = 10f;
				if (this.dieDelayHandle == null)
				{
					this.dieDelayHandle = new vp_Timer.Handle();
				}
				vp_Timer.In(delay2, delegate()
				{
					this.DelayRecycle();
				}, this.dieDelayHandle);
			}
		}
		else
		{
			this.mServerNotDieNumCount++;
			if (this.mServerNotDieNumCount > GameDefine.NPC_SERVER_WAIT_RELIFE_NUM)
			{
				this.OnRelife(newHP, base.Position);
				this.mServerNotDieNumCount = 0;
			}
		}
	}

	// Token: 0x0600345E RID: 13406 RVA: 0x000D2458 File Offset: 0x000D0658
	public override void OnRelife(long hp, Vector3 pos)
	{
		base.OnRelife(hp, pos);
		if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
		{
			if (SingletonUnity<BossXueTiaoLogicNew>.Exists)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.ChangeHP(hp, this);
			}
		}
		else
		{
			if (this.mNpcData.Type == 4)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if ((playerData.IsHaveTeam() && playerData.TeamInfo.TeamID == this.mTeamId) || (playerData.IsHaveGuild() && playerData.PlayerGuild.ServerId == this.mGuildId))
				{
					this.mNpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.mPlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.SELF_ESCORT_NPC, true);
				}
				else
				{
					CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
					if (escortMission != null && escortMission.GetParam(1) == this.ServerId)
					{
						this.mNpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.mPlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.SELF_ESCORT_NPC, true);
					}
					else
					{
						this.mNpcHeadInfoLogic.SetNameLabel(string.Format("{0}'s {1}", this.mPlayerName, this.AttributeData.Name), this.AttributeData.Camp, false, HEAD_PIC_TYPE.OTHER_ESCORT_NPC, true);
					}
				}
			}
			else
			{
				this.mNpcHeadInfoLogic.SetNameLabel(this.AttributeData.Name, this.AttributeData.Camp, false, HEAD_PIC_TYPE.INVALID, false);
			}
			this.mNpcHeadInfoLogic.ForceSetHpVal((float)this.AttributeData.HP / (float)this.AttributeData.MaxHP);
		}
	}

	// Token: 0x0600345F RID: 13407 RVA: 0x000D2610 File Offset: 0x000D0810
	public override void ChangeHPEffect(long newHP, GameDefine.DAMAGEBOARD_TYPE type)
	{
		if (!base.IsDie)
		{
			long num = this.AttributeData.HP - newHP;
			if (num > 0L)
			{
				base.UpdateDamgeBoard(type, num);
				this.OnBeaton();
			}
			else
			{
				base.UpdateDamgeBoard(type, num);
			}
			if (newHP < 0L)
			{
				newHP = 0L;
			}
			this.AttributeData.HP = newHP;
			if (this.AttributeData.HP < this.AttributeData.MaxHP / 2L && this.AttributeData.Camp == GameDefine.CAMP_TYPE.STATIC_NPC && this.NPCFunctionType == GameDefine.NPC_FUNCTION_TYPE.GANGCITY_NPC && this.SmokeParticle != null && (!UnityVersionUtil.IsActive(this.SmokeParticle.gameObject) || !this.SmokeParticle.isPlaying))
			{
				UnityVersionUtil.SetActiveRecursive(this.SmokeParticle.gameObject, true);
				this.SmokeParticle.Play();
			}
			this.UpdateHeadInfo();
			if (this.mNPCType == GameDefine.NPC_TYPE.BOSS && SingletonUnity<BossXueTiaoLogicNew>.Exists)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.ChangeHP(newHP, this);
			}
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager.CurrentMapInofData.MapType == MAPTYPE.CASH_DAILY_COPY && this.mNPCType == GameDefine.NPC_TYPE.BOSS)
			{
				int num2 = (int)((float)num / (float)this.AttributeData.MaxHP * (float)SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurSceneReward);
				if (num2 <= 0)
				{
					return;
				}
				ObjInitDropItemData objInitDropItemData = new ObjInitDropItemData();
				objInitDropItemData.item = new item
				{
					itemId = GameDefine.CASH_ITEM_ID,
					itemCount = (long)num2
				};
				objInitDropItemData.ownerServerId = PlayerData.MainPlayerServerId;
				objInitDropItemData.ServerID = UUID.GenUUID();
				float num3 = Random.Range(0.5f, 3f);
				objInitDropItemData.Pos = base.Position + new Vector3(num3, 0f, num3);
				Singleton<ObjManager>.Instance.CreateDropItem(objInitDropItemData);
			}
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
			this.mReceiveBiggerHpTimeCount = Time.time;
		}
	}

	// Token: 0x06003460 RID: 13408 RVA: 0x000D2820 File Offset: 0x000D0A20
	public override void OnDie()
	{
		if (base.IsDie)
		{
			return;
		}
		base.OnDie();
		if (this.NPCType == GameDefine.NPC_TYPE.BOSS)
		{
			if (SingletonUnity<BossXueTiaoLogicNew>.Exists)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.RemoveBoss(this);
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BossXueTiaoUI);
			SingletonUnity<UIManager>.Instance.CheckFunctionTop(false);
			Singleton<ObjManager>.Instance.ClearSceneBomb();
		}
		if (this.IsCityCaptureNpc())
		{
			if (this.ExplosionParticle != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.ExplosionParticle.gameObject, true);
				this.ExplosionParticle.Play();
			}
			if (this.MeshRoot != null)
			{
				this.MeshRoot.animation.Play("GTACheBaoZa_Animation");
			}
		}
		if (GameManager.OnLineState && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene())
		{
			return;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.OnNPCDie(this);
	}

	// Token: 0x06003461 RID: 13409 RVA: 0x000D2914 File Offset: 0x000D0B14
	public override void Recyle()
	{
		base.Recyle();
		if (this.NPCType == GameDefine.NPC_TYPE.BOSS)
		{
			if (SingletonUnity<BossXueTiaoLogicNew>.Exists)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.RemoveBoss(this);
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BossXueTiaoUI);
			SingletonUnity<UIManager>.Instance.CheckFunctionTop(false);
		}
	}

	// Token: 0x06003462 RID: 13410 RVA: 0x000D2964 File Offset: 0x000D0B64
	private void CalDropItem()
	{
	}

	// Token: 0x06003463 RID: 13411 RVA: 0x000D2968 File Offset: 0x000D0B68
	public void DelayRecycle()
	{
		Singleton<ObjManager>.Instance.RecycleNpc(this);
	}

	// Token: 0x06003464 RID: 13412 RVA: 0x000D2978 File Offset: 0x000D0B78
	public override void OnSkillEnable(string skillID)
	{
		if (this.mAILogic == null || !this.mAILogic.enabled)
		{
			return;
		}
		SkillData skillDataById = DataManager.GetSkillDataById(skillID);
		if (this.mEnableSkillIDList.Contains(skillDataById))
		{
			return;
		}
		if (this.mEnableSkillIDList.Count > 0)
		{
			for (int i = 0; i < this.mEnableSkillIDList.Count; i++)
			{
				if (skillDataById.PriorityAutoCombat > this.mEnableSkillIDList[i].PriorityAutoCombat)
				{
					this.mEnableSkillIDList.Insert(i, skillDataById);
					return;
				}
			}
		}
		this.mEnableSkillIDList.Add(skillDataById);
	}

	// Token: 0x06003465 RID: 13413 RVA: 0x000D2A24 File Offset: 0x000D0C24
	public override void OnSkillDisable(string skillID)
	{
		this.mEnableSkillIDList.Remove(DataManager.GetSkillDataById(skillID));
	}

	// Token: 0x06003466 RID: 13414 RVA: 0x000D2A38 File Offset: 0x000D0C38
	public override void OnSkillFinished()
	{
		base.OnSkillFinished();
	}

	// Token: 0x06003467 RID: 13415 RVA: 0x000D2A40 File Offset: 0x000D0C40
	public void PrintSkill()
	{
		MonoBehaviour.print(base.gameObject.name + "====================");
		for (int i = 0; i < this.mEnableSkillIDList.Count; i++)
		{
			MonoBehaviour.print(this.mEnableSkillIDList[i]);
		}
	}

	// Token: 0x06003468 RID: 13416 RVA: 0x000D2A94 File Offset: 0x000D0C94
	public void DisableAllComponent()
	{
		base.enabled = false;
		this.mAnimationLogic.enabled = false;
		this.mNavMeshAgent.enabled = false;
		this.mEffectLogic.enabled = false;
		this.mSkillMotion.enabled = false;
		this.mEffectMotion.enabled = false;
		this.mBuffLogic.enabled = false;
		this.mAutoMoveLogic.enabled = false;
		if (this.mAILogic != null)
		{
			this.mAILogic.enabled = false;
		}
	}

	// Token: 0x06003469 RID: 13417 RVA: 0x000D2B1C File Offset: 0x000D0D1C
	public void EnableAllComponent()
	{
		base.enabled = true;
		this.mAnimationLogic.enabled = true;
		this.mNavMeshAgent.enabled = true;
		this.mEffectLogic.enabled = true;
		this.mSkillMotion.enabled = true;
		this.mEffectMotion.enabled = true;
		this.mBuffLogic.enabled = true;
		this.mAutoMoveLogic.enabled = true;
		if (this.mAILogic != null)
		{
			this.mAILogic.enabled = true;
		}
	}

	// Token: 0x0600346A RID: 13418 RVA: 0x000D2BA4 File Offset: 0x000D0DA4
	public void updateBossHPState()
	{
		if (!base.IsDie && this.mNPCType == GameDefine.NPC_TYPE.BOSS && SingletonUnity<BossXueTiaoLogicNew>.Exists)
		{
			SingletonUnity<BossXueTiaoLogicNew>.Instance.UpdateBossHp(this, this.AttributeData);
		}
	}

	// Token: 0x0600346B RID: 13419 RVA: 0x000D2BE4 File Offset: 0x000D0DE4
	protected override void ChangeIdleState()
	{
		if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
		{
			this.mAnimationLogic.PlayAnimation(4, null);
		}
		else
		{
			this.mAnimationLogic.PlayAnimation(0, null);
		}
	}

	// Token: 0x0400223A RID: 8762
	private const float mDialogRange = 5f;

	// Token: 0x0400223B RID: 8763
	private Material mMeshMat;

	// Token: 0x0400223C RID: 8764
	protected NpcData mNpcData;

	// Token: 0x0400223D RID: 8765
	protected AutoMoveLogic mAutoMoveLogic;

	// Token: 0x0400223E RID: 8766
	protected AILogic mAILogic;

	// Token: 0x0400223F RID: 8767
	protected BundleManager.LoadModelData mLoadingModelData;

	// Token: 0x04002240 RID: 8768
	protected long mLoadingModelDataId;

	// Token: 0x04002241 RID: 8769
	protected GameObject mMeshRoot;

	// Token: 0x04002242 RID: 8770
	protected Vector3 mBornPos;

	// Token: 0x04002243 RID: 8771
	protected float mPatrolRange = 5f;

	// Token: 0x04002244 RID: 8772
	protected float mSearchRange = 7f;

	// Token: 0x04002245 RID: 8773
	private Transform mFollowTarget;

	// Token: 0x04002246 RID: 8774
	public vp_Timer.Handle dieDelayHandle;

	// Token: 0x04002247 RID: 8775
	protected GameDefine.NPC_FUNCTION_TYPE mNPCFunctionType;

	// Token: 0x04002248 RID: 8776
	protected GameDefine.NPC_TYPE mNPCType;

	// Token: 0x04002249 RID: 8777
	private ObjCharacter mSelectedTarget;

	// Token: 0x0400224A RID: 8778
	protected string mPathID;

	// Token: 0x0400224B RID: 8779
	private List<SkillData> mEnableSkillIDList = new List<SkillData>();

	// Token: 0x0400224C RID: 8780
	private string mPlayerName = string.Empty;

	// Token: 0x0400224D RID: 8781
	private long mGuildId = -1L;

	// Token: 0x0400224E RID: 8782
	private long mTeamId = -1L;

	// Token: 0x0400224F RID: 8783
	private ParticleSystem SmokeParticle;

	// Token: 0x04002250 RID: 8784
	private ParticleSystem ExplosionParticle;

	// Token: 0x04002251 RID: 8785
	private Transform FLWheel;

	// Token: 0x04002252 RID: 8786
	private Transform FRWheel;

	// Token: 0x04002253 RID: 8787
	private Transform BLWheel;

	// Token: 0x04002254 RID: 8788
	private Transform BRWheel;

	// Token: 0x04002255 RID: 8789
	private Vector3 FLPos;

	// Token: 0x04002256 RID: 8790
	private Vector3 FRPos;

	// Token: 0x04002257 RID: 8791
	private Vector3 BLPos;

	// Token: 0x04002258 RID: 8792
	private Vector3 BRPos;

	// Token: 0x04002259 RID: 8793
	protected string mDefaultDialogID = string.Empty;

	// Token: 0x0400225A RID: 8794
	private List<string> mMissionIdList = new List<string>();

	// Token: 0x0400225B RID: 8795
	private NPCHeadInfoLogic mNpcHeadInfoLogic;

	// Token: 0x0400225C RID: 8796
	public ObjManager.OnGetNPC onLoadMeshFinished;

	// Token: 0x0400225D RID: 8797
	private Transform mMainPlayerTransform;

	// Token: 0x0400225E RID: 8798
	private bool mInCircleFlag;

	// Token: 0x0400225F RID: 8799
	private float SqrActiveRadius = 16f;

	// Token: 0x04002260 RID: 8800
	private float ExitSqrActiveRadius = 20.25f;

	// Token: 0x04002261 RID: 8801
	private float sqrDis;

	// Token: 0x04002262 RID: 8802
	private List<ObjCharacter> mCurSearchTargetList;

	// Token: 0x04002263 RID: 8803
	private int mServerNotDieNumCount;
}
