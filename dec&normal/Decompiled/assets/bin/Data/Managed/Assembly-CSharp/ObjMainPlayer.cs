using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200081C RID: 2076
public class ObjMainPlayer : ObjOtherPlayer
{
	// Token: 0x060032AC RID: 12972 RVA: 0x000C5BE0 File Offset: 0x000C3DE0
	public ObjMainPlayer()
	{
		this.AutoMountTime = 3f;
		this.mLastPosition = Vector3.zero;
		this.timeWait = 0.2f;
		this.request = new move.request();
		this.pos = new position();
		this.mWaitForSkillRetList = new List<string>();
		this.switchCD = 30f;
		this.lastChangeTime = float.MinValue;
		base..ctor();
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER;
	}

	// Token: 0x17000E5E RID: 3678
	// (get) Token: 0x060032AD RID: 12973 RVA: 0x000C5C9C File Offset: 0x000C3E9C
	// (set) Token: 0x060032AE RID: 12974 RVA: 0x000C5CA4 File Offset: 0x000C3EA4
	public override long ServerId
	{
		get
		{
			return PlayerData.MainPlayerServerId;
		}
		set
		{
			PlayerData.MainPlayerServerId = value;
		}
	}

	// Token: 0x060032AF RID: 12975 RVA: 0x000C5CAC File Offset: 0x000C3EAC
	public float GetStopDistance()
	{
		if (base.CurPlayerState == PLAYER_STATE.DRIVING)
		{
			return 3f;
		}
		return 1.5f;
	}

	// Token: 0x17000E5F RID: 3679
	// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000C5CC8 File Offset: 0x000C3EC8
	public override long GuildId
	{
		get
		{
			return this.GameManager.PlayerData.PlayerGuild.ServerId;
		}
	}

	// Token: 0x17000E60 RID: 3680
	// (get) Token: 0x060032B1 RID: 12977 RVA: 0x000C5CEC File Offset: 0x000C3EEC
	public override long TeamId
	{
		get
		{
			return this.GameManager.PlayerData.TeamInfo.TeamID;
		}
	}

	// Token: 0x17000E61 RID: 3681
	// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000C5D10 File Offset: 0x000C3F10
	public ThirdPersonController ThirdPersonController
	{
		get
		{
			return this.mThirdPersonController;
		}
	}

	// Token: 0x17000E62 RID: 3682
	// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000C5D18 File Offset: 0x000C3F18
	public CameraController CameraController
	{
		get
		{
			return this.mCameraController;
		}
	}

	// Token: 0x17000E63 RID: 3683
	// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000C5D20 File Offset: 0x000C3F20
	// (set) Token: 0x060032B5 RID: 12981 RVA: 0x000C5D4C File Offset: 0x000C3F4C
	public override CharacterAttributeData AttributeData
	{
		get
		{
			if (this.mAttributeData == null)
			{
				this.mAttributeData = this.GameManager.PlayerData.MainPlayerAttrData;
			}
			return this.mAttributeData;
		}
		set
		{
			this.mAttributeData = value;
		}
	}

	// Token: 0x17000E64 RID: 3684
	// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000C5D58 File Offset: 0x000C3F58
	// (set) Token: 0x060032B7 RID: 12983 RVA: 0x000C5D6C File Offset: 0x000C3F6C
	public override PROFESSION_TYPE Profession
	{
		get
		{
			return this.GameManager.PlayerData.Profession;
		}
		set
		{
			this.GameManager.PlayerData.Profession = value;
		}
	}

	// Token: 0x17000E65 RID: 3685
	// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000C5D80 File Offset: 0x000C3F80
	// (set) Token: 0x060032B9 RID: 12985 RVA: 0x000C5DAC File Offset: 0x000C3FAC
	public override List<CharacterSkillData> CharacterSkillData
	{
		get
		{
			if (this.mCharacterSkillData == null)
			{
				this.mCharacterSkillData = this.GameManager.PlayerData.MainPlayerSkillDataList;
			}
			return this.mCharacterSkillData;
		}
		set
		{
			this.mCharacterSkillData = value;
		}
	}

	// Token: 0x17000E66 RID: 3686
	// (get) Token: 0x060032BA RID: 12986 RVA: 0x000C5DB8 File Offset: 0x000C3FB8
	// (set) Token: 0x060032BB RID: 12987 RVA: 0x000C5DCC File Offset: 0x000C3FCC
	public int SkillIndex
	{
		get
		{
			return this.GameManager.PlayerData.SkillIndex;
		}
		set
		{
			this.GameManager.PlayerData.SkillIndex = value;
		}
	}

	// Token: 0x17000E67 RID: 3687
	// (get) Token: 0x060032BC RID: 12988 RVA: 0x000C5DE0 File Offset: 0x000C3FE0
	private PlayerData mPlayerData
	{
		get
		{
			if (this.mCachePlayerData == null)
			{
				this.mCachePlayerData = this.GameManager.PlayerData;
			}
			return this.mCachePlayerData;
		}
	}

	// Token: 0x17000E68 RID: 3688
	// (get) Token: 0x060032BD RID: 12989 RVA: 0x000C5E10 File Offset: 0x000C4010
	// (set) Token: 0x060032BE RID: 12990 RVA: 0x000C5E20 File Offset: 0x000C4020
	public override string MountId
	{
		get
		{
			return this.mPlayerData.MountId;
		}
		set
		{
			this.mPlayerData.MountId = value;
		}
	}

	// Token: 0x17000E69 RID: 3689
	// (get) Token: 0x060032BF RID: 12991 RVA: 0x000C5E30 File Offset: 0x000C4030
	// (set) Token: 0x060032C0 RID: 12992 RVA: 0x000C5E40 File Offset: 0x000C4040
	public override string MountColor
	{
		get
		{
			return this.mPlayerData.MountColor;
		}
		set
		{
			this.mPlayerData.MountColor = value;
		}
	}

	// Token: 0x17000E6A RID: 3690
	// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000C5E50 File Offset: 0x000C4050
	// (set) Token: 0x060032C2 RID: 12994 RVA: 0x000C5E60 File Offset: 0x000C4060
	public override bool IsServerRidingMount
	{
		get
		{
			return this.mPlayerData.IsServerRidingMount;
		}
		set
		{
			this.mPlayerData.IsServerRidingMount = value;
		}
	}

	// Token: 0x17000E6B RID: 3691
	// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000C5E70 File Offset: 0x000C4070
	// (set) Token: 0x060032C4 RID: 12996 RVA: 0x000C5EAC File Offset: 0x000C40AC
	public Material XRayMat
	{
		get
		{
			if (this.mXRayMat == null)
			{
				this.mXRayMat = (ResourcesManager.Load("Material/XRay") as Material);
			}
			return this.mXRayMat;
		}
		set
		{
			this.mXRayMat = value;
		}
	}

	// Token: 0x17000E6C RID: 3692
	// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000C5EB8 File Offset: 0x000C40B8
	// (set) Token: 0x060032C6 RID: 12998 RVA: 0x000C5EC0 File Offset: 0x000C40C0
	public bool IsTalking
	{
		get
		{
			return this.mIsTalking;
		}
		set
		{
			this.mIsTalking = value;
			if (value)
			{
				this.AutoComabat = false;
			}
		}
	}

	// Token: 0x17000E6D RID: 3693
	// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000C5ED8 File Offset: 0x000C40D8
	public bool IsAutoMovingFlag
	{
		get
		{
			return this.GameManager.AutoSearchPath.IsAutoMovingFlag;
		}
	}

	// Token: 0x17000E6E RID: 3694
	// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000C5EEC File Offset: 0x000C40EC
	// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000C5EF4 File Offset: 0x000C40F4
	public bool IsNeedAutoMountCar
	{
		get
		{
			return this.mIsNeedAutoMountCar;
		}
		set
		{
			this.mIsNeedAutoMountCar = value;
		}
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x000C5F00 File Offset: 0x000C4100
	public void EnterAutoMoving(float time)
	{
		this.mStartAutoMoveTime = time;
	}

	// Token: 0x17000E6F RID: 3695
	// (get) Token: 0x060032CB RID: 13003 RVA: 0x000C5F0C File Offset: 0x000C410C
	// (set) Token: 0x060032CC RID: 13004 RVA: 0x000C5F14 File Offset: 0x000C4114
	public bool CompleteMissionFlag
	{
		get
		{
			return this.mCompleteMissionFlag;
		}
		set
		{
			this.mCompleteMissionFlag = value;
		}
	}

	// Token: 0x17000E70 RID: 3696
	// (get) Token: 0x060032CD RID: 13005 RVA: 0x000C5F20 File Offset: 0x000C4120
	private GameManager GameManager
	{
		get
		{
			if (this.mGameManager == null)
			{
				this.mGameManager = SingletonDontDestoryUnity<GameManager>.Instance;
			}
			return this.mGameManager;
		}
	}

	// Token: 0x060032CE RID: 13006 RVA: 0x000C5F50 File Offset: 0x000C4150
	public override void SetTargetPartObjId(MODEL_TYPE type, string id)
	{
		this.TargetPartObjId[(int)type] = id;
		this.mPlayerData.UpdateMainPlayerPartBundleIdList(this.TargetPartObjId);
	}

	// Token: 0x060032CF RID: 13007 RVA: 0x000C5F6C File Offset: 0x000C416C
	private void Awake()
	{
	}

	// Token: 0x060032D0 RID: 13008 RVA: 0x000C5F70 File Offset: 0x000C4170
	public override void Init()
	{
		base.Init();
		this.InitMainPlayer();
		this.BonesTrsDict = base.gameObject.GetComponentInChildren<TransDicts>().TransDict;
	}

	// Token: 0x060032D1 RID: 13009 RVA: 0x000C5FA0 File Offset: 0x000C41A0
	private new void InitHeadInfo()
	{
		ResourcesManager.LoadHeadInfoPrefab(UIInfo.PlayerHeadInfoUI, "PlayerHeadInfoRoot", new ResourcesManager.LoadHeadInfoDelegate(this.LoadPlayerHeadInfo));
	}

	// Token: 0x060032D2 RID: 13010 RVA: 0x000C5FC0 File Offset: 0x000C41C0
	private void LoadPlayerHeadInfo(GameObject newObj)
	{
		if (newObj != null)
		{
			BillBoard billBoard = newObj.GetComponent<BillBoard>();
			if (billBoard == null)
			{
				billBoard = newObj.AddComponent<BillBoard>();
			}
			billBoard.BindObj = base.gameObject;
			billBoard.DeltaHeight = base.CurrentCharacterModelData.ModelHeight + this.PLAYER_NAME_DELTA_HIGHT;
		}
		PlayerHeadInfoLogic component = newObj.GetComponent<PlayerHeadInfoLogic>();
		this.mHeadInfoLogic = component;
		if (this.mGameManager.SceneManager.IsTutorialScene())
		{
			component.Reset(true, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, GameDefine.CAMP_TYPE.PLAYER_1, false, this.AttributeData.IsChampionGuild());
		}
		else if (this.mGameManager.SceneManager.IsSurviveBattleScene())
		{
			component.Reset(true, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, this.AttributeData.Camp, false, this.AttributeData.IsChampionGuild());
		}
		else
		{
			component.Reset(true, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, GameDefine.CAMP_TYPE.PLAYER_1, false, this.AttributeData.IsChampionGuild());
		}
	}

	// Token: 0x060032D3 RID: 13011 RVA: 0x000C6104 File Offset: 0x000C4304
	public void HideHeadInfo()
	{
		if (this.mHeadInfoLogic != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mHeadInfoLogic.gameObject, false);
		}
	}

	// Token: 0x060032D4 RID: 13012 RVA: 0x000C6134 File Offset: 0x000C4334
	public void ShowHeadInfo()
	{
		if (this.mHeadInfoLogic != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mHeadInfoLogic.gameObject, true);
			if (this.mGameManager.SceneManager.IsSurviveBattleScene())
			{
				(this.mHeadInfoLogic as PlayerHeadInfoLogic).Reset(true, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, this.AttributeData.Camp, false, this.AttributeData.IsChampionGuild());
			}
			else
			{
				(this.mHeadInfoLogic as PlayerHeadInfoLogic).Reset(true, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, GameDefine.CAMP_TYPE.PLAYER_1, false, this.AttributeData.IsChampionGuild());
			}
		}
	}

	// Token: 0x060032D5 RID: 13013 RVA: 0x000C6208 File Offset: 0x000C4408
	public void reName(string newname)
	{
		if (this.mHeadInfoLogic != null)
		{
			(this.mHeadInfoLogic as PlayerHeadInfoLogic).UpdateName(newname);
		}
	}

	// Token: 0x060032D6 RID: 13014 RVA: 0x000C6238 File Offset: 0x000C4438
	public void InitMainPlayer()
	{
		if (this.mThirdPersonController == null)
		{
			this.mThirdPersonController = base.gameObject.AddComponent<ThirdPersonController>();
		}
		if (this.mCameraController == null)
		{
			this.mCameraController = Camera.main.transform.parent.gameObject.AddComponent<CameraController>();
		}
		if (this.mGameManager == null)
		{
			this.mGameManager = SingletonDontDestoryUnity<GameManager>.Instance;
		}
	}

	// Token: 0x060032D7 RID: 13015 RVA: 0x000C62B4 File Offset: 0x000C44B4
	public void UpdateMainPlayerVisual(characterVisual visual)
	{
		this.mPlayerData.CharacterModelId = visual.ModeId;
		this.mPlayerData.CharacterModelData = DataManager.GetCharacterModelDataByID(visual.ModeId);
		this.mPlayerData.PartHeadId = visual.HeadId;
		this.mPlayerData.PartBodyId = visual.BodyId;
		this.mPlayerData.PartLegId = visual.LegId;
		this.mPlayerData.PartWeaponId = visual.WeaponId;
		if (visual.HasWeaponItemId)
		{
			this.mPlayerData.WeaponItemId = visual.WeaponItemId;
		}
		if (visual.HasFashionItemId)
		{
			this.mPlayerData.FashionItemId = visual.FashionItemId;
		}
		this.mPlayerData.FashionWeaponId = ((!visual.HasFashion_WeaponId) ? visual.WeaponId : visual.Fashion_WeaponId);
		this.mPlayerData.FashionHeadId = ((!visual.HasFashion_HeadId) ? visual.HeadId : visual.Fashion_HeadId);
		this.mPlayerData.FashionBodyId = ((!visual.HasFashion_BodyId) ? visual.BodyId : visual.Fashion_BodyId);
		this.mPlayerData.FashionLegId = ((!visual.HasFashion_LegId) ? visual.LegId : visual.Fashion_LegId);
		this.mPlayerData.IsShowFashion = (visual.showType == 1L);
		if (this.mPlayerData.IsHaveTeam())
		{
			TeamMember selfMember = this.mPlayerData.TeamInfo.SelfMember;
			selfMember.Visual = visual;
		}
	}

	// Token: 0x060032D8 RID: 13016 RVA: 0x000C6440 File Offset: 0x000C4640
	public void ResetMainPlayer(ObjInitPlayerData playerInitData)
	{
		base.Reset();
		base.Position = playerInitData.mPos;
		base.CacheTransform.forward = playerInitData.mDir;
		PlayerData.MainPlayerServerId = playerInitData.mServerID;
		this.mPlayerData.MainPlayerStartPos = playerInitData.mPos;
		this.mPlayerData.MainPlayerStartDir = playerInitData.mDir;
		this.mPlayerData.CharacterModelId = playerInitData.mCharacterModelId;
		this.mPlayerData.CharacterModelData = DataManager.GetCharacterModelDataByID(this.mPlayerData.CharacterModelId);
		this.mPlayerData.PartHeadId = playerInitData.visual.HeadId;
		this.mPlayerData.PartBodyId = playerInitData.visual.BodyId;
		this.mPlayerData.PartLegId = playerInitData.visual.LegId;
		this.mPlayerData.PartWeaponId = playerInitData.visual.WeaponId;
		if (playerInitData.visual.HasWeaponItemId)
		{
			this.mPlayerData.WeaponItemId = playerInitData.visual.WeaponItemId;
		}
		if (playerInitData.visual.HasFashionItemId)
		{
			this.mPlayerData.FashionItemId = playerInitData.visual.FashionItemId;
		}
		this.mPlayerData.FashionHeadId = ((!playerInitData.visual.HasFashion_HeadId) ? playerInitData.visual.HeadId : playerInitData.visual.Fashion_HeadId);
		this.mPlayerData.FashionBodyId = ((!playerInitData.visual.HasFashion_BodyId) ? playerInitData.visual.BodyId : playerInitData.visual.Fashion_BodyId);
		this.mPlayerData.FashionLegId = ((!playerInitData.visual.HasFashion_LegId) ? playerInitData.visual.LegId : playerInitData.visual.Fashion_LegId);
		this.mPlayerData.FashionWeaponId = ((!playerInitData.visual.HasFashion_WeaponId) ? playerInitData.visual.WeaponId : playerInitData.visual.Fashion_WeaponId);
		this.mPlayerData.IsShowFashion = (playerInitData.visual.showType == 1L);
		this.mPlayerData.MountColor = playerInitData.visual.mount_color;
		this.mPlayerData.MountId = playerInitData.visual.MountId;
		this.mPlayerData.IsServerRidingMount = (playerInitData.visual.mount_state == 1L);
		this.AttributeData.InitData(playerInitData.Attribute, playerInitData.AttributeAll);
		this.Profession = playerInitData.Profession;
		this.AttributeData.HP = (long)playerInitData.HP;
		this.AttributeData.Name = playerInitData.Name;
		this.AttributeData.GuildName = playerInitData.GuildName;
		this.AttributeData.CurEXP = playerInitData.EXP;
		this.AttributeData.Level = playerInitData.Level;
		this.AttributeData.CurTitleExp = playerInitData.TitleExp;
		this.AttributeData.CurTitleLevel = playerInitData.TitleLevel;
		this.AttributeData.ComboValue = playerInitData.ComboValue;
		this.AttributeData.CurSpeed = playerInitData.Speed;
		this.AttributeData.WalkSpeed = playerInitData.WalkSpeed;
		this.AttributeData.CurRec = playerInitData.Rec;
		this.AttributeData.RefineLevel = playerInitData.RefineLevel;
		this.AttributeData.RefineNeckLevel = playerInitData.RefineNeckLevel;
		this.AttributeData.RefineRing1Level = playerInitData.RefineRing1Level;
		this.AttributeData.RefineRing2Level = playerInitData.RefineRing2Level;
		this.AttributeData.RefineBeltLevel = playerInitData.RefineBeltLevel;
		for (int i = 0; i < playerInitData.EnhanceLevelList.Length; i++)
		{
			this.AttributeData.EquipEnhanceList[i] = playerInitData.EnhanceLevelList[i];
		}
		this.AttributeData.Camp = playerInitData.Camp;
		this.AttributeData.PkMode = playerInitData.PkMode;
		this.AttributeData.DanceState = playerInitData.DanceState;
		this.AttributeData.DanceId = playerInitData.DanceId;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SkillIndex = playerInitData.SkillIndex;
		this.UpdateSkillList(playerInitData.skills);
		this.ResetCombo();
		this.InitHeadInfo();
		this.UpdatePlayerHp(this.AttributeData.HP);
		base.InitNavMeshAgent();
		this.mCompleteMissionFlag = false;
		if (this.IsServerRidingMount && this.GameManager.SceneManager.IsBigWorld())
		{
			this.MountCar(this.MountId, this.MountColor);
		}
	}

	// Token: 0x060032D9 RID: 13017 RVA: 0x000C68C8 File Offset: 0x000C4AC8
	public void ResetMainPlayer(movement posInfo)
	{
		base.Reset();
		base.Position = new Vector3((float)posInfo.pos.x / 100f, SceneManager.GetHitHeight((float)posInfo.pos.x / 100f, (float)posInfo.pos.z / 100f), (float)posInfo.pos.z / 100f);
		this.mPlayerData.MainPlayerStartDir = MathUtil.HeadingToVector3((float)posInfo.pos.o / 100f);
		base.CacheTransform.forward = this.mPlayerData.MainPlayerStartDir;
		this.mPlayerData.MainPlayerStartPos = base.Position;
		this.ResetCombo();
		this.InitHeadInfo();
		this.UpdatePlayerHp(this.AttributeData.HP);
		base.InitNavMeshAgent();
		this.mCompleteMissionFlag = false;
		if (this.IsServerRidingMount && this.GameManager.SceneManager.IsBigWorld())
		{
			this.MountCar(this.MountId, this.MountColor);
		}
	}

	// Token: 0x060032DA RID: 13018 RVA: 0x000C69D8 File Offset: 0x000C4BD8
	public void SyncPlayerData(PlayerData data)
	{
	}

	// Token: 0x060032DB RID: 13019 RVA: 0x000C69DC File Offset: 0x000C4BDC
	private void SynPlayerPosition()
	{
		if (this.mGameManager.SceneManager.IsDontSynPostion())
		{
			return;
		}
		if (base.IsDie || this.mNavMeshAgent == null || !this.mNavMeshAgent.enabled)
		{
			return;
		}
		if (Time.time > this.ftime && Vector3.SqrMagnitude(this.mLastPosition - base.Position) > 0.010000001f)
		{
			this.mLastPosition = base.CacheTransform.position;
			this.ftime = Time.time + this.timeWait;
			this.request.clear();
			this.pos.clear();
			this.pos.x = (long)Mathf.CeilToInt(base.CacheTransform.position.x * 100f);
			this.pos.y = (long)Mathf.CeilToInt(base.CacheTransform.position.y * 100f);
			this.pos.z = (long)Mathf.CeilToInt(base.CacheTransform.position.z * 100f);
			this.pos.o = (long)Mathf.CeilToInt(MathUtil.Heading(base.CacheTransform.forward) * 100f);
			this.request.pos = this.pos;
			this.request.moving = base.IsMoving;
			this.request.index = 1L;
			this.request.parm = (long)(this.AttributeData.CurSpeed * 100f);
			NetLogic.GetInstance().Send<Protocol.move>(this.request, null);
		}
	}

	// Token: 0x060032DC RID: 13020 RVA: 0x000C6B98 File Offset: 0x000C4D98
	public override void ChangeHPVal(long newHP)
	{
		if (!base.IsDie)
		{
			this.AttributeData.HP = newHP;
			this.UpdatePlayerHp(newHP);
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
		}
		else if (newHP > 0L)
		{
			this.relifeCount++;
			if (this.relifeCount > 5)
			{
				this.relifeCount = 0;
				this.OnRelife(newHP, base.Position);
			}
		}
	}

	// Token: 0x060032DD RID: 13021 RVA: 0x000C6C18 File Offset: 0x000C4E18
	public override void ChangeHPEffect(long newHP, GameDefine.DAMAGEBOARD_TYPE type)
	{
		if (!base.IsDie)
		{
			long cHP = this.AttributeData.HP - newHP;
			base.UpdateDamgeBoard(type, cHP);
			if (newHP < 0L)
			{
				newHP = 0L;
			}
			this.mReceiveBiggerHpTimeCount = Time.time;
		}
	}

	// Token: 0x060032DE RID: 13022 RVA: 0x000C6C60 File Offset: 0x000C4E60
	public override void ChangeLevel(int level, int combovalue = 0)
	{
		if (level > this.AttributeData.Level)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LevelUpUIRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<LevelUpUIRoot>.Instance.Reset(this.AttributeData.Level, level, this.AttributeData.ComboValue, combovalue);
			}, null);
			if (SingletonUnity<TouXiangKuangLogic>.Exists)
			{
				SingletonUnity<TouXiangKuangLogic>.Instance.ChangeLevel(level);
			}
			this.GameManager.FlurryLogEventMap("LevelEvent", "LevelUp", level.ToString());
		}
	}

	// Token: 0x060032DF RID: 13023 RVA: 0x000C6CF4 File Offset: 0x000C4EF4
	private void UpdatePlayerHp(long newHp)
	{
		if (!base.IsLocalDrivingCar && SingletonUnity<TouXiangKuangLogic>.Exists)
		{
			SingletonUnity<TouXiangKuangLogic>.Instance.ChangeHP(newHp, this.AttributeData.MaxHP);
		}
	}

	// Token: 0x060032E0 RID: 13024 RVA: 0x000C6D2C File Offset: 0x000C4F2C
	public void UpdateComboTime()
	{
		if (this.mComboTimeCount > 0f)
		{
			this.mComboTimeCount -= Time.deltaTime;
			if (this.mComboTimeCount <= 0f)
			{
				this.ResetCombo();
			}
		}
	}

	// Token: 0x060032E1 RID: 13025 RVA: 0x000C6D74 File Offset: 0x000C4F74
	public void UpdateStep()
	{
		if (base.IsMoving)
		{
			if (this.mCurPlayerState != PLAYER_STATE.DRIVING)
			{
				SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(0, 1f, null);
			}
		}
	}

	// Token: 0x060032E2 RID: 13026 RVA: 0x000C6DA4 File Offset: 0x000C4FA4
	private void UpdateAutoMountCar()
	{
	}

	// Token: 0x060032E3 RID: 13027 RVA: 0x000C6DA8 File Offset: 0x000C4FA8
	private void Update()
	{
		base.UpdateComponent();
		base.UpdateMove();
		base.UpdateSkillCD();
		base.UpdateHoldTime();
		this.UpdateComboTime();
		this.UpdateAutoCombatBreakState();
		this.UpdateAuto();
		this.UpdateTeamFollow();
		base.SkillLogic.UpdateSkill();
		this.SynPlayerPosition();
		this.UpdateStep();
		this.UpdateAutoMountCar();
		this.UpdateMoveCheck();
	}

	// Token: 0x060032E4 RID: 13028 RVA: 0x000C6E08 File Offset: 0x000C5008
	public void AddSkillWaitRet(string skillId)
	{
	}

	// Token: 0x060032E5 RID: 13029 RVA: 0x000C6E0C File Offset: 0x000C500C
	public void RemoveSkillWaitRet(string skillId)
	{
		if (this.mWaitForSkillRetList.Contains(skillId))
		{
			this.mWaitForSkillRetList.Remove(skillId);
		}
	}

	// Token: 0x060032E6 RID: 13030 RVA: 0x000C6E2C File Offset: 0x000C502C
	public bool CheckInSkillWaitRet(string skillId)
	{
		return this.mWaitForSkillRetList.Contains(skillId) || this.mWaitForSkillRetList.Count > 3;
	}

	// Token: 0x060032E7 RID: 13031 RVA: 0x000C6E54 File Offset: 0x000C5054
	public void ClearSkillWaitRet()
	{
		this.mWaitForSkillRetList.Clear();
	}

	// Token: 0x060032E8 RID: 13032 RVA: 0x000C6E64 File Offset: 0x000C5064
	public bool IsCanAttackTarget(ObjCharacter obj)
	{
		return obj.ObjType != GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || CampTool.ISPlayerCanAttack(this, obj as ObjOtherPlayer);
	}

	// Token: 0x060032E9 RID: 13033 RVA: 0x000C6E80 File Offset: 0x000C5080
	public void UseComboSkill()
	{
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			return;
		}
		if (this.CharacterSkillData.Count <= 0)
		{
			return;
		}
		if (!base.CheckHoldTime())
		{
			this.CheckComboDelay();
			return;
		}
		if (base.BeforeSkillCheck())
		{
			return;
		}
		ObjCharacter objCharacter = null;
		if (this.mSelectedTarget != null && this.mSelectedTarget.ServerId != this.ServerId && this.IsCanAttackTarget(this.mSelectedTarget))
		{
			objCharacter = this.mSelectedTarget;
		}
		if (objCharacter == null || objCharacter.IsDie)
		{
			objCharacter = this.ChooseTarget(this.mComboIndex);
			this.SelectTarget(objCharacter);
		}
		if (objCharacter == null)
		{
			return;
		}
		base.CurUseSkillId = this.mComboIndex;
		if (this.mComboIndex == this.CharacterSkillData[0].ID)
		{
			this.EnterCombat(objCharacter, new ObjCharacter.SkillUseSuccess(this.OnComboSuccess));
		}
		else
		{
			if (base.SkillLogic.IsUsingSkill && !base.SkillLogic.CheckSkillCanBeBreak(base.CurUseSkillId))
			{
				return;
			}
			SkillData skillDataById = DataManager.GetSkillDataById(base.CurUseSkillId);
			if (!base.CheckSkillDistance(skillDataById, objCharacter))
			{
				this.MoveTo(objCharacter.Position, skillDataById.TraceDistanceMeter + objCharacter.ModelRadius + this.ModelRadius - 0.5f, new ObjCharacter.TargetArriveFinsh(this.TargetArriveUseSkill), true);
				return;
			}
			if (base.IsDrivingMount())
			{
				this.SendServerDisMountCar();
			}
			if (!this.mGameManager.SceneManager.IsSingleCopyScene())
			{
				List<attack_list> list = new List<attack_list>();
				attack_list attack_list = new attack_list();
				if (!objCharacter.InvincibleFlag)
				{
					attack_list.id = objCharacter.ServerId;
					list.Add(attack_list);
				}
				skill_use.request request = new skill_use.request();
				request.skillId = base.CurUseSkillId;
				request.targetId = objCharacter.ServerId;
				request.combo = true;
				request.attack_list = list;
				request.parm = (long)((int)(Time.realtimeSinceStartup * 100f));
				if (base.SkillLogic.ServerUseSkill(base.CurUseSkillId, this.ServerId, objCharacter.ServerId, list))
				{
					NetLogic.GetInstance().Send<Protocol.skill_use>(request, null);
					this.OnComboSuccess(base.CurUseSkillId);
				}
			}
			else if (base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, objCharacter.ServerId))
			{
				this.OnComboSuccess(base.CurUseSkillId);
			}
			this.mGameManager.MissionManager.StopAutoMoveToMission();
			this.mIsNeedAutoMountCar = false;
		}
	}

	// Token: 0x060032EA RID: 13034 RVA: 0x000C7118 File Offset: 0x000C5318
	public void UseSkill(string skillId, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			return;
		}
		if (base.BeforeSkillCheck())
		{
			return;
		}
		ObjCharacter objCharacter = null;
		if (this.mSelectedTarget != null && this.mSelectedTarget.ServerId != this.ServerId && this.IsCanAttackTarget(this.mSelectedTarget))
		{
			objCharacter = this.mSelectedTarget;
		}
		if (objCharacter == null || objCharacter.IsDie)
		{
			objCharacter = this.ChooseTarget(skillId);
			this.SelectTarget(objCharacter);
		}
		base.CurUseSkillId = skillId;
		this.EnterCombat(objCharacter, onSkillUseSuccess);
	}

	// Token: 0x060032EB RID: 13035 RVA: 0x000C71B4 File Offset: 0x000C53B4
	public override void EnterCombat(ObjCharacter target, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
		if (base.SkillLogic.IsUsingSkill && !base.SkillLogic.CheckSkillCanBeBreak(base.CurUseSkillId))
		{
			return;
		}
		SkillData skillDataById = DataManager.GetSkillDataById(base.CurUseSkillId);
		if (skillDataById == null)
		{
			return;
		}
		if (target != null)
		{
			if (!base.CheckSkillDistance(skillDataById, target))
			{
				this.MoveTo(target.Position, skillDataById.TraceDistanceMeter + target.ModelRadius + this.ModelRadius - 0.5f, new ObjCharacter.TargetArriveFinsh(this.TargetArriveUseSkill), true);
				return;
			}
		}
		else
		{
			EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(skillDataById.EffId_0);
			if (effInfoDataById != null && effInfoDataById.Target == 0 && (effInfoDataById.AreaType == 0 || effInfoDataById.AreaType == 1))
			{
				return;
			}
		}
		if (!base.CheckSkillCD(skillDataById))
		{
			return;
		}
		if (onSkillUseSuccess != null && !this.mSkillUseSuccessDic.ContainsKey(base.CurUseSkillId))
		{
			this.mSkillUseSuccessDic.Add(base.CurUseSkillId, onSkillUseSuccess);
		}
		if (base.IsDrivingMount())
		{
			this.SendServerDisMountCar();
		}
		if (!this.mGameManager.SceneManager.IsSingleCopyScene())
		{
			skill_use.request request = new skill_use.request();
			request.skillId = base.CurUseSkillId;
			long targetId = -1L;
			if (target != null)
			{
				targetId = target.ServerId;
			}
			request.targetId = targetId;
			request.combo = true;
			List<attack_list> list = new List<attack_list>();
			if (!string.IsNullOrEmpty(skillDataById.EffId_0))
			{
				EffInfoData effInfoDataById2 = DataManager.GetEffInfoDataById(skillDataById.EffId_0);
				if (target != null && effInfoDataById2 != null && this.mSkillLogic.IsNeedFaceTarget(effInfoDataById2))
				{
					base.FaceToPub(target.Position);
				}
				BuffInfoData buffInfoData = null;
				if (!string.IsNullOrEmpty(effInfoDataById2.BuffID))
				{
					buffInfoData = DataManager.GetBuffInfoDataByID(effInfoDataById2.BuffID);
				}
				int num = GameDefine.BUF_USE_FAIL;
				List<ObjCharacter> effectTargetList = this.mSkillLogic.GetEffectTargetList(this, skillDataById, effInfoDataById2, target, this.mSkillLogic.GetCandidateList(this.AttributeData.Camp));
				for (int i = 0; i < effectTargetList.Count; i++)
				{
					attack_list attack_list = new attack_list();
					attack_list.id = effectTargetList[i].ServerId;
					if (buffInfoData != null)
					{
						num = CharacterAttributeData.BuffUseState(this, effectTargetList[i].AttributeData, skillDataById.ID, effInfoDataById2, (float)Random.Range(0, 100), buffInfoData.BufType);
						attack_list.value = (long)num;
					}
					else
					{
						attack_list.value = (long)GameDefine.BUF_USE_FAIL;
					}
					list.Add(attack_list);
				}
				request.attack_list = list;
			}
			request.parm = (long)((int)(Time.realtimeSinceStartup * 100f));
			if (this.mSkillLogic.ServerUseSkill(base.CurUseSkillId, this.ServerId, targetId, list))
			{
				NetLogic.GetInstance().Send<Protocol.skill_use>(request, null);
			}
		}
		else if (target != null)
		{
			base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, target.ServerId);
		}
		else
		{
			base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, -1L);
		}
		this.mGameManager.MissionManager.StopAutoMoveToMission();
		this.mIsNeedAutoMountCar = false;
	}

	// Token: 0x060032EC RID: 13036 RVA: 0x000C74FC File Offset: 0x000C56FC
	public void OnComboSuccess(string skillId)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(skillId);
		this.mComboIndex = skillDataById.NextSkill;
		this.mComboTimeCount = skillDataById.ComboValidTimeSecond;
		this.mComboTimeTotalCount = this.mComboTimeCount;
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x000C7534 File Offset: 0x000C5734
	public override void OnSkillUseSuccess(string skillId)
	{
		base.OnSkillUseSuccess(skillId);
		this.RemoveSkillWaitRet(skillId);
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x000C7544 File Offset: 0x000C5744
	public override void OnSkillUseFail(string skillId)
	{
		if (this.mSkillUseSuccessDic.ContainsKey(skillId))
		{
			this.mSkillUseSuccessDic.Remove(skillId);
		}
		this.RemoveSkillWaitRet(skillId);
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x000C756C File Offset: 0x000C576C
	public override void OnSkillFinished()
	{
		base.OnSkillFinished();
		if (this.mComboNextFlag)
		{
			this.UseComboSkill();
			this.mComboNextFlag = false;
		}
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x000C758C File Offset: 0x000C578C
	public void TargetArriveUseSkill(ObjCharacter objCha)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(this.mCurUseSkillId);
		if (skillDataById != null)
		{
			if (!string.IsNullOrEmpty(skillDataById.NextSkill))
			{
				this.UseComboSkill();
			}
			else
			{
				this.UseSkill(this.mCurUseSkillId, null);
			}
		}
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x000C75D4 File Offset: 0x000C57D4
	public ObjCharacter ChooseTarget(string skillId)
	{
		ObjCharacter result = null;
		float num = 9999f;
		bool flag = false;
		bool flag2 = false;
		this.mCurSearchTargetList = Singleton<ObjManager>.Instance.CampTargetList[(int)this.AttributeData.Camp];
		for (int i = 0; i < this.mCurSearchTargetList.Count; i++)
		{
			if (this.mCurSearchTargetList[i].ServerId != this.ServerId)
			{
				if (!this.mCurSearchTargetList[i].IsDie)
				{
					if (!this.AutoComabat || this.mCurSearchTargetList[i].ObjType != GameDefine.OBJ_TYPE.OBJ_NPC || (this.mCurSearchTargetList[i] as ObjNPC).NPCFunctionType != GameDefine.NPC_FUNCTION_TYPE.CITIZEN_NPC)
					{
						if (this.mCurSearchTargetList[i].ObjType != GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || CampTool.ISPlayerCanAttack(this, this.mCurSearchTargetList[i] as ObjOtherPlayer))
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
									num = pathDistance;
								}
								else if (num > pathDistance)
								{
									result = this.mCurSearchTargetList[i];
									num = pathDistance;
								}
							}
							else if (flag2)
							{
								float pathDistance = base.GetPathDistance(this.mCurSearchTargetList[i].Position);
								if (num > pathDistance)
								{
									result = this.mCurSearchTargetList[i];
									num = pathDistance;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x000C77A8 File Offset: 0x000C59A8
	public void CheckComboDelay()
	{
		if (base.SkillLogic.UsingSkillData != null && base.SkillLogic.UsingSkillData.NextSkill == this.mComboIndex)
		{
			this.mComboNextFlag = true;
		}
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x000C77EC File Offset: 0x000C59EC
	public bool CheckSkillEnable(SkillData skillData, ObjCharacter target)
	{
		return base.CheckSkillCD(skillData) && base.CheckSkillDistance(skillData, target);
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x000C780C File Offset: 0x000C5A0C
	private void ResetCombo()
	{
		if (this.CharacterSkillData.Count != 0)
		{
			this.mComboIndex = this.CharacterSkillData[0].ID;
		}
		SkillData skillDataById = DataManager.GetSkillDataById(this.mComboIndex);
		this.mComboTimeCount = skillDataById.ComboValidTimeSecond;
	}

	// Token: 0x060032F5 RID: 13045 RVA: 0x000C7858 File Offset: 0x000C5A58
	public bool IsComboFirstSkill()
	{
		return this.mComboIndex == this.CharacterSkillData[0].ID;
	}

	// Token: 0x060032F6 RID: 13046 RVA: 0x000C7878 File Offset: 0x000C5A78
	public float GetComboSKillTimePercent()
	{
		CharacterSkillData characterSkillDataByID = base.GetCharacterSkillDataByID(this.mComboIndex);
		if (characterSkillDataByID == null)
		{
			return 0f;
		}
		if (characterSkillDataByID.CDTimeCount > 0f)
		{
			return Mathf.Clamp01(characterSkillDataByID.CDTimeCount / DataManager.GetSkillDataById(this.mComboIndex).CDSecond);
		}
		return 0f;
	}

	// Token: 0x060032F7 RID: 13047 RVA: 0x000C78D0 File Offset: 0x000C5AD0
	public float GetSkillTimePercent(string skillId)
	{
		CharacterSkillData characterSkillDataByID = base.GetCharacterSkillDataByID(skillId);
		if (characterSkillDataByID == null)
		{
			return 0f;
		}
		if (characterSkillDataByID.CDTimeCount > 0f)
		{
			return Mathf.Clamp01(characterSkillDataByID.CDTimeCount / DataManager.GetSkillDataById(skillId).CDSecond);
		}
		return 0f;
	}

	// Token: 0x060032F8 RID: 13048 RVA: 0x000C7920 File Offset: 0x000C5B20
	public float GetSwitchSkillCDPercent()
	{
		float num = this.switchCD - (Time.time - this.lastChangeTime);
		return Mathf.Clamp01(num / this.switchCD);
	}

	// Token: 0x060032F9 RID: 13049 RVA: 0x000C7950 File Offset: 0x000C5B50
	public bool SwitchSkillGroup()
	{
		if (Time.time - this.lastChangeTime > this.switchCD)
		{
			this.lastChangeTime = Time.time;
			if (this.SkillIndex != 0)
			{
				change_skill_index.request request = new change_skill_index.request();
				request.index = 0L;
				NetLogic.GetInstance().Send<Protocol.change_skill_index>(request, null);
				this.SkillIndex = 0;
			}
			else
			{
				change_skill_index.request request2 = new change_skill_index.request();
				request2.index = 1L;
				NetLogic.GetInstance().Send<Protocol.change_skill_index>(request2, null);
				this.SkillIndex = 1;
			}
			this.UpdateSkillIdList();
			return true;
		}
		return false;
	}

	// Token: 0x060032FA RID: 13050 RVA: 0x000C79DC File Offset: 0x000C5BDC
	public void RemoveSkillPosition(string skillId)
	{
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			if (skillId.Equals(this.CharacterSkillData[i].ID))
			{
				this.CharacterSkillData[i].Index = 10;
				change_skill_position.request request = new change_skill_position.request();
				request.skillId = skillId;
				request.indexPos = 10L;
				NetLogic.GetInstance().Send<Protocol.change_skill_position>(request, null);
			}
		}
		this.UpdateSkillIdList();
	}

	// Token: 0x060032FB RID: 13051 RVA: 0x000C7A5C File Offset: 0x000C5C5C
	public void ChangeSkillPosition(string skillId, int newindex)
	{
		string text = string.Empty;
		int num = 10;
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			int index = this.CharacterSkillData[i].Index;
			if (index == newindex && this.CharacterSkillData[i].ID == skillId)
			{
				return;
			}
			if (this.CharacterSkillData[i].ID == skillId)
			{
				num = this.CharacterSkillData[i].Index;
				this.CharacterSkillData[i].Index = 10;
			}
			else if (index == newindex)
			{
				text = this.CharacterSkillData[i].ID;
				this.CharacterSkillData[i].Index = 10;
			}
		}
		for (int j = 0; j < this.CharacterSkillData.Count; j++)
		{
			if (skillId == this.CharacterSkillData[j].ID)
			{
				this.CharacterSkillData[j].Index = newindex;
				change_skill_position.request request = new change_skill_position.request();
				request.skillId = skillId;
				request.indexPos = (long)newindex;
				NetLogic.GetInstance().Send<Protocol.change_skill_position>(request, null);
			}
			if (text == this.CharacterSkillData[j].ID)
			{
				this.CharacterSkillData[j].Index = num;
				change_skill_position.request request2 = new change_skill_position.request();
				request2.skillId = text;
				request2.indexPos = (long)num;
				NetLogic.GetInstance().Send<Protocol.change_skill_position>(request2, null);
			}
		}
		this.UpdateSkillIdList();
	}

	// Token: 0x060032FC RID: 13052 RVA: 0x000C7C0C File Offset: 0x000C5E0C
	public override void UpdateSkillList(Dictionary<string, skill_info> skills)
	{
		this.CharacterSkillData.Clear();
		if (skills == null)
		{
			return;
		}
		int count = skills.Count;
		foreach (KeyValuePair<string, skill_info> keyValuePair in skills)
		{
			this.CharacterSkillData.Add(new CharacterSkillData(keyValuePair.Value.skillId, (int)keyValuePair.Value.skillLevel, (int)keyValuePair.Value.indexPos, (int)keyValuePair.Value.indexPos2, keyValuePair.Value.disable));
		}
		this.CharacterSkillData.Sort((CharacterSkillData temp1, CharacterSkillData temp2) => temp1.Index2 - temp2.Index2);
		this.UpdateSkillIdList();
		this.ResetCombo();
	}

	// Token: 0x060032FD RID: 13053 RVA: 0x000C7D04 File Offset: 0x000C5F04
	public void UpdateSkillIdList()
	{
		this.PlayerSkillIDList.Clear();
		for (int i = 0; i < 4; i++)
		{
			this.PlayerSkillIDList.Add(string.Empty);
		}
		for (int j = 0; j < this.CharacterSkillData.Count; j++)
		{
			int index = this.CharacterSkillData[j].Index;
			if (index == 3)
			{
				this.PlayerSkillIDList[index - 3] = this.CharacterSkillData[j].ID;
			}
			if (this.SkillIndex == 0)
			{
				if (index > 3 && index < 7)
				{
					this.PlayerSkillIDList[index - 3] = this.CharacterSkillData[j].ID;
				}
			}
			else if (index > 6 && index < 10)
			{
				this.PlayerSkillIDList[index - 6] = this.CharacterSkillData[j].ID;
			}
		}
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x000C7E00 File Offset: 0x000C6000
	public int GetPlayerSkillLevelByPos(int skillpos)
	{
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			if (this.CharacterSkillData[i].Index2 == skillpos)
			{
				return this.CharacterSkillData[i].Level;
			}
		}
		return 0;
	}

	// Token: 0x17000E71 RID: 3697
	// (get) Token: 0x060032FF RID: 13055 RVA: 0x000C7E54 File Offset: 0x000C6054
	// (set) Token: 0x06003300 RID: 13056 RVA: 0x000C7E80 File Offset: 0x000C6080
	public List<string> PlayerSkillIDList
	{
		get
		{
			if (this.mPlayerSkillIDList == null)
			{
				this.mPlayerSkillIDList = this.GameManager.PlayerData.MainPlayerSkillIDList;
			}
			return this.mPlayerSkillIDList;
		}
		set
		{
			this.mPlayerSkillIDList = value;
		}
	}

	// Token: 0x06003301 RID: 13057 RVA: 0x000C7E8C File Offset: 0x000C608C
	public bool CheckSkillCanUpdate()
	{
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			CharacterSkillData characterSkillData = this.CharacterSkillData[i];
			if (characterSkillData != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(characterSkillData.UnlockLevel))
			{
				int index = characterSkillData.Index;
				if (index >= 4 && index <= 6)
				{
					SkillData skillDataById = DataManager.GetSkillDataById(characterSkillData.ID);
					if (skillDataById != null && skillDataById.IsUpgrade != 0)
					{
						if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level > characterSkillData.Level + 1)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003302 RID: 13058 RVA: 0x000C7F40 File Offset: 0x000C6140
	public void SelectTarget(ObjCharacter target)
	{
		if (target != null)
		{
			if (target.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
			{
				ObjNPC targetNPC = target as ObjNPC;
				if (targetNPC.IsMissionNpc())
				{
					if (targetNPC.CheckInDialogRange())
					{
						Singleton<DialogManager>.Instance.ShowDialog(targetNPC, string.Empty);
					}
					else
					{
						float stopRange = this.GetStopDistance();
						Vector3 vector = target.Position;
						if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsShopScene())
						{
							stopRange = 1f;
							vector = target.Position + target.transform.forward * 2.5f;
						}
						this.MoveTo(vector, stopRange, delegate(ObjCharacter A_1)
						{
							Singleton<DialogManager>.Instance.ShowDialog(targetNPC, string.Empty);
						});
					}
					return;
				}
				if (!CampTool.CanAttack(this.AttributeData.Camp, targetNPC.AttributeData.Camp))
				{
					return;
				}
			}
			else if (target.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
			{
				ObjOtherPlayer target2 = target as ObjOtherPlayer;
				if (!CampTool.ISPlayerCanAttack(this, target2))
				{
					return;
				}
			}
			else if (target.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
			{
				ObjZombieRagdollPlayer objZombieRagdollPlayer = target as ObjZombieRagdollPlayer;
				if (objZombieRagdollPlayer.IsMissionNpc)
				{
					if (objZombieRagdollPlayer.CheckInDialogRange())
					{
						objZombieRagdollPlayer.ShowAcitvityDialog();
					}
					return;
				}
			}
			else if ((target.ObjType == GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR || target.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC_CAR) && !CampTool.CanAttack(this.AttributeData.Camp, target.AttributeData.Camp))
			{
				return;
			}
		}
		if (base.CurPlayerCar != null && base.CurPlayerCar == target)
		{
			target = null;
		}
		base.SelectedTarget = target;
	}

	// Token: 0x06003303 RID: 13059 RVA: 0x000C8100 File Offset: 0x000C6300
	public void SelectTargetNPC(ObjNPC target)
	{
		if (target != null)
		{
			ObjNPC targetNPC = target;
			if (targetNPC.IsMissionNpc())
			{
				if (targetNPC.CheckInDialogRange())
				{
					Singleton<DialogManager>.Instance.ShowDialog(targetNPC, string.Empty);
				}
				else
				{
					this.MoveTo(target.Position, this.GetStopDistance(), delegate(ObjCharacter A_1)
					{
						Singleton<DialogManager>.Instance.ShowDialog(targetNPC, string.Empty);
					});
				}
				return;
			}
		}
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x000C8180 File Offset: 0x000C6380
	public void UpdateSelectTarget()
	{
		if (base.SelectedTarget == null)
		{
			return;
		}
		if (base.SelectedTarget.IsDie)
		{
			this.SelectTarget(null);
			return;
		}
		if (base.IsDie)
		{
			this.SelectTarget(null);
			return;
		}
		float num = 144f;
		if (num < Vector3.SqrMagnitude(base.SelectedTarget.Position - base.Position))
		{
			this.SelectTarget(null);
			return;
		}
	}

	// Token: 0x06003305 RID: 13061 RVA: 0x000C81FC File Offset: 0x000C63FC
	public override void OnDie()
	{
		if (base.IsDrivingMount())
		{
			this.SendServerDisMountCar();
		}
		base.OnDie();
		this.ClearSkillWaitRet();
		this.mSkillUseSuccessDic.Clear();
	}

	// Token: 0x06003306 RID: 13062 RVA: 0x000C8234 File Offset: 0x000C6434
	public override void OnRelife(long hp, Vector3 pos)
	{
		this.mCurPlayerState = PLAYER_STATE.NORMAL;
		base.OnRelife(this.AttributeData.MaxHP, pos);
		this.UpdatePlayerHp(this.AttributeData.HP);
		this.UseInvincibleSkill();
		this.LeveAutoCombat();
		base.IsLocalDrivingCar = false;
		base.CurPlayerCar = null;
	}

	// Token: 0x06003307 RID: 13063 RVA: 0x000C8288 File Offset: 0x000C6488
	public void SendNotify(bool isFilterRepeat, string msg, params object[] args)
	{
		NoticeLogic.AddNotifyData2Client(isFilterRepeat, msg, false, args);
	}

	// Token: 0x06003308 RID: 13064 RVA: 0x000C8294 File Offset: 0x000C6494
	public void EquipItem(GameItem item, bool isinhert = false)
	{
		equip_item.request request = new equip_item.request();
		request.indexId = item.IndexId;
		request.inhert = isinhert;
		NetLogic.GetInstance().Send<Protocol.equip_item>(request, null);
	}

	// Token: 0x06003309 RID: 13065 RVA: 0x000C82C8 File Offset: 0x000C64C8
	public void UnEquipItem(GameItem item)
	{
		unequip_item.request request = new unequip_item.request();
		request.indexId = item.IndexId;
		NetLogic.GetInstance().Send<Protocol.unequip_item>(request, null);
	}

	// Token: 0x0600330A RID: 13066 RVA: 0x000C82F4 File Offset: 0x000C64F4
	public void EquipBadge(GameItem item, int pos)
	{
		equip_badge.request request = new equip_badge.request();
		request.indexId = item.IndexId;
		request.pos = (long)pos;
		NetLogic.GetInstance().Send<Protocol.equip_badge>(request, null);
	}

	// Token: 0x0600330B RID: 13067 RVA: 0x000C8328 File Offset: 0x000C6528
	public void UnEquipBadge(GameItem item)
	{
		unequip_badge.request request = new unequip_badge.request();
		request.indexId = item.IndexId;
		NetLogic.GetInstance().Send<Protocol.unequip_badge>(request, null);
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x000C8354 File Offset: 0x000C6554
	public bool UseDrag(GameItem item)
	{
		if (this.mGameManager.PlayerData.CanUseDrag() && this.mGameManager.SceneManager.IsCanUsePotion() && !base.IsDie)
		{
			use_item.request request = new use_item.request();
			request.indexId = item.IndexId;
			NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
			this.mGameManager.PlayerData.UpdateDragCD();
			return true;
		}
		return false;
	}

	// Token: 0x0600330D RID: 13069 RVA: 0x000C83C8 File Offset: 0x000C65C8
	public void UseBuffDrag(GameItem item)
	{
		use_item.request request = new use_item.request();
		request.indexId = item.IndexId;
		NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(item.ItemData.Function.ToString());
		base.AddBuffInfoData(effInfoDataById.BuffID, 0f, effInfoDataById.BuffDurationSecond, this);
		if (item.ItemId == GameDefine.AtkBuffItemId)
		{
			NoticeLogic.AddNotifyData("#{100650}", true, false);
		}
		else if (item.ItemId == GameDefine.DefBuffItemId)
		{
			NoticeLogic.AddNotifyData("#{100651}", true, false);
		}
		else if (item.ItemId == GameDefine.MovBuffItemId)
		{
			NoticeLogic.AddNotifyData("#{100652}", true, false);
		}
	}

	// Token: 0x0600330E RID: 13070 RVA: 0x000C8490 File Offset: 0x000C6690
	public void UseDanceItem(string itemid)
	{
		List<GameItem> itemByItemId = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack.GetItemByItemId(itemid);
		if (itemByItemId != null && itemByItemId.Count > 0)
		{
			use_item.request request = new use_item.request();
			request.indexId = itemByItemId[0].IndexId;
			List<Vector3> circlePoint = this.GetCirclePoint(base.transform.position, 0.5f);
			circlePoint.Add(base.transform.position);
			Vector3 vector = circlePoint[0];
			for (int i = 0; i < circlePoint.Count; i++)
			{
				vector = circlePoint[i];
				bool flag = SceneManager.IsInNavmeshArea(vector);
				if (flag && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsInGatherArea(vector))
				{
					break;
				}
			}
			if (!SceneManager.IsInNavmeshArea(vector))
			{
				return;
			}
			request.x = (long)vector.x * 100L;
			request.z = (long)vector.z * 100L;
			NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
		}
	}

	// Token: 0x0600330F RID: 13071 RVA: 0x000C859C File Offset: 0x000C679C
	public List<Vector3> GetCirclePoint(Vector3 orign, float radius)
	{
		List<Vector3> list = new List<Vector3>();
		for (float num = 0f; num < 6.2831855f; num += 0.62831855f)
		{
			list.Add(new Vector3(orign.x + radius * Mathf.Cos(num), 0f, orign.z + radius * Mathf.Sin(num)));
		}
		List<Vector3> list2 = new List<Vector3>();
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			int num2 = Random.Range(0, list.Count);
			list2.Add(list[num2]);
			list.RemoveAt(num2);
		}
		return list2;
	}

	// Token: 0x06003310 RID: 13072 RVA: 0x000C8648 File Offset: 0x000C6848
	public void EquipFashionItem(GameItem item)
	{
		equip_fashion_item.request request = new equip_fashion_item.request();
		request.indexId = item.IndexId;
		NetLogic.GetInstance().Send<Protocol.equip_fashion_item>(request, null);
	}

	// Token: 0x06003311 RID: 13073 RVA: 0x000C8674 File Offset: 0x000C6874
	public void UnEquipFashionItem(GameItem item)
	{
		unequip_fashion_item.request request = new unequip_fashion_item.request();
		request.indexId = item.IndexId;
		NetLogic.GetInstance().Send<Protocol.unequip_fashion_item>(request, null);
	}

	// Token: 0x06003312 RID: 13074 RVA: 0x000C86A0 File Offset: 0x000C68A0
	public bool IsHaveWeapon()
	{
		return true;
	}

	// Token: 0x06003313 RID: 13075 RVA: 0x000C86A4 File Offset: 0x000C68A4
	public void DisableMainPlayer()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		base.DisactiveHeadInfo();
	}

	// Token: 0x06003314 RID: 13076 RVA: 0x000C86B8 File Offset: 0x000C68B8
	public void EnableMainPlayer()
	{
		base.enabled = true;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		base.ActiveHeadInfo();
		(this.mHeadInfoLogic as PlayerHeadInfoLogic).Reset(true, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, GameDefine.CAMP_TYPE.PLAYER_1, false, this.AttributeData.IsChampionGuild());
	}

	// Token: 0x06003315 RID: 13077 RVA: 0x000C8720 File Offset: 0x000C6920
	public void ChangeMountColor(string mountId, string newColorId)
	{
		if (this.GameManager.SceneManager.IsBigWorld() && base.IsDrivingMount() && this.MountId.Equals(mountId))
		{
			this.MountCar(mountId, newColorId);
		}
	}

	// Token: 0x06003316 RID: 13078 RVA: 0x000C8768 File Offset: 0x000C6968
	public void ChangeMountCar(string mountId, string mountColor)
	{
		if (this.GameManager.SceneManager.IsBigWorld() && base.IsDrivingMount() && !this.MountId.Equals(mountId))
		{
			this.MountCar(mountId, mountColor);
		}
	}

	// Token: 0x06003317 RID: 13079 RVA: 0x000C87B0 File Offset: 0x000C69B0
	public override void MountCar(string mountId, string mountColor)
	{
		base.MountCar(mountId, mountColor);
		this.RemoveXRayMat();
		if (SingletonUnity<RealTimeShadow>.Exists)
		{
			SingletonUnity<RealTimeShadow>.Instance.DisableRealTimeShadow();
		}
		if (this.mCameraController.InitFlag)
		{
			this.mCameraController.ChangeToCarView();
		}
		base.UpdateStopDistance(this.GetStopDistance());
	}

	// Token: 0x06003318 RID: 13080 RVA: 0x000C8808 File Offset: 0x000C6A08
	public override void DisMountCar()
	{
		base.DisMountCar();
		this.AddXRayMat();
		if (SingletonUnity<RealTimeShadow>.Exists)
		{
			SingletonUnity<RealTimeShadow>.Instance.EnableRealTimeShadow();
		}
		this.mCameraController.BackToNormalView();
		base.UpdateStopDistance(this.GetStopDistance());
	}

	// Token: 0x06003319 RID: 13081 RVA: 0x000C884C File Offset: 0x000C6A4C
	public void RemoveXRayMat()
	{
		for (int i = 0; i < base.PartObject.Length; i++)
		{
			if (base.PartObject[i] != null)
			{
				SkinnedMeshRenderer component = base.PartObject[i].GetComponent<SkinnedMeshRenderer>();
				if (component.materials.Length > 1)
				{
					Material[] array = new Material[component.materials.Length - 1];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = component.materials[j];
					}
					component.materials = array;
				}
			}
		}
	}

	// Token: 0x0600331A RID: 13082 RVA: 0x000C88DC File Offset: 0x000C6ADC
	public void AddXRayMat()
	{
		for (int i = 0; i < base.PartObject.Length; i++)
		{
			if (base.PartObject[i] != null)
			{
				SkinnedMeshRenderer component = base.PartObject[i].GetComponent<SkinnedMeshRenderer>();
				Material[] array = new Material[component.materials.Length + 1];
				bool flag = false;
				for (int j = 0; j < component.materials.Length; j++)
				{
					if (component.materials[j].name.Contains("XRay"))
					{
						flag = true;
						break;
					}
					array[j] = component.materials[j];
				}
				if (!flag)
				{
					array[component.materials.Length] = this.XRayMat;
					component.materials = array;
				}
			}
		}
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x000C89A4 File Offset: 0x000C6BA4
	public void SendServerMountCar()
	{
		if (this.mSkillLogic.IsUsingSkill || base.IsDie || base.IsStun())
		{
			return;
		}
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.StopDance();
		}
		NetLogic.GetInstance().Send<Protocol.use_mount>(null, null);
		this.MountCar(this.MountId, this.MountColor);
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x000C8A08 File Offset: 0x000C6C08
	public void SendServerDisMountCar()
	{
		NetLogic.GetInstance().Send<Protocol.unuse_mount>(null, null);
		this.DisMountCar();
		this.mStartAutoMoveTime = Time.time;
	}

	// Token: 0x0600331D RID: 13085 RVA: 0x000C8A28 File Offset: 0x000C6C28
	public override void StartDance(string danceId)
	{
		if (base.IsDrivingMount())
		{
			this.SendServerDisMountCar();
		}
		if (this.mCurPlayerState != PLAYER_STATE.DANCE)
		{
			base.StartDance(danceId);
			this.mPlayerData.PlayerDanceData.CurDanceId = danceId;
			this.mPlayerData.PlayerDanceData.CurDanceData = DataManager.GetDanceDataById(danceId);
		}
		if (SingletonUnity<DanceBtnRootLogic>.Exists)
		{
			SingletonUnity<DanceBtnRootLogic>.Instance.UpdateDanceBtn();
		}
	}

	// Token: 0x0600331E RID: 13086 RVA: 0x000C8A94 File Offset: 0x000C6C94
	public override bool PlayeSocialDance(string danceId)
	{
		if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			return false;
		}
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			return false;
		}
		if (this.IsOpenAutoCombat)
		{
			return false;
		}
		if (base.SkillLogic.IsUsingSkill)
		{
			return false;
		}
		if (base.IsMoving)
		{
			base.DisactiveTargetArriveFinish();
			this.StopMove();
		}
		if (base.IsDrivingMount())
		{
			this.SendServerDisMountCar();
		}
		SocialDanceData socialDanceDataById = DataManager.GetSocialDanceDataById(danceId);
		if (socialDanceDataById == null)
		{
			return false;
		}
		if (socialDanceDataById.Level > this.AttributeData.Level)
		{
			return false;
		}
		this.mCurPlayerState = PLAYER_STATE.SOCIAL_DANCE;
		base.AnimationLogic.PlayAnimation(socialDanceDataById.ActionID, new AnimationLogic.OnAnimFinished(this.SocialDanceFinish), -1f);
		play_social_dance.request request = new play_social_dance.request();
		request.id = this.ServerId;
		request.danceId = danceId;
		NetLogic.GetInstance().Send<Protocol.play_social_dance>(request, null);
		return true;
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x000C8B7C File Offset: 0x000C6D7C
	public override void StopDance()
	{
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			base.StopDance();
			NetLogic.GetInstance().Send<Protocol.pause_participate_dance>(null, null);
			if (SingletonUnity<DanceBtnRootLogic>.Exists)
			{
				SingletonUnity<DanceBtnRootLogic>.Instance.UpdateDanceBtn();
			}
		}
	}

	// Token: 0x06003320 RID: 13088 RVA: 0x000C8BBC File Offset: 0x000C6DBC
	public override void RemoveDance()
	{
		base.RemoveDance();
		this.mPlayerData.PlayerDanceData.Reset();
	}

	// Token: 0x06003321 RID: 13089 RVA: 0x000C8BD4 File Offset: 0x000C6DD4
	public override void MoveTo(Vector3 pos, float stopRange = 1f, ObjCharacter.TargetArriveFinsh arriveFinsh = null)
	{
		this.mIsNeedUpdateCheck = false;
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.StopDance();
		}
		base.MoveTo(pos, stopRange, arriveFinsh);
	}

	// Token: 0x06003322 RID: 13090 RVA: 0x000C8C04 File Offset: 0x000C6E04
	public void UseInvincibleSkill()
	{
		switch (base.GetWeaponType())
		{
		case 0:
			this.UseSkill(GameDefine.XD_INVINCIBLE_SKILL_ID, null);
			break;
		case 1:
			this.UseSkill(GameDefine.QJ_INVINCIBLE_SKILL_ID, null);
			break;
		case 2:
			this.UseSkill(GameDefine.NQS_INVINCIBLE_SKILL_ID, null);
			break;
		}
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x000C8C64 File Offset: 0x000C6E64
	public void MoveTo(Vector3 pos, float stopRange, ObjCharacter.TargetArriveFinsh arriveFinsh, bool isNeedUpdateCheck)
	{
		this.MoveTo(pos, stopRange, arriveFinsh);
		this.mIsNeedUpdateCheck = isNeedUpdateCheck;
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x000C8C78 File Offset: 0x000C6E78
	public new void StopMove()
	{
		base.StopMove();
		this.mIsNeedUpdateCheck = false;
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x000C8C88 File Offset: 0x000C6E88
	private void UpdateMoveCheck()
	{
		if (this.mIsNeedUpdateCheck && this.targetArriveFinish != null)
		{
			this.tempTargetArriveFinish = this.targetArriveFinish;
			this.targetArriveFinish = null;
			this.tempTargetArriveFinish(this);
		}
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x000C8CC0 File Offset: 0x000C6EC0
	public override void OnStun(BuffInfoData buffInfoData)
	{
		if (base.IsDrivingMount())
		{
			this.SendServerDisMountCar();
		}
		base.OnStun(buffInfoData);
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x000C8CDC File Offset: 0x000C6EDC
	public override void OnKnockDown(BuffInfoData buffInfoData, ObjCharacter sender)
	{
		if (base.IsDrivingMount())
		{
			this.SendServerDisMountCar();
		}
		base.OnKnockDown(buffInfoData, sender);
	}

	// Token: 0x17000E72 RID: 3698
	// (get) Token: 0x06003328 RID: 13096 RVA: 0x000C8CF8 File Offset: 0x000C6EF8
	// (set) Token: 0x06003329 RID: 13097 RVA: 0x000C8D0C File Offset: 0x000C6F0C
	public bool IsOpenAutoCombat
	{
		get
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsOpenAutoCombat;
		}
		set
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsOpenAutoCombat = value;
		}
	}

	// Token: 0x17000E73 RID: 3699
	// (get) Token: 0x0600332A RID: 13098 RVA: 0x000C8D20 File Offset: 0x000C6F20
	// (set) Token: 0x0600332B RID: 13099 RVA: 0x000C8D34 File Offset: 0x000C6F34
	public float BreakAutoCombatTime
	{
		get
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BreakAutoCombatTime;
		}
		set
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BreakAutoCombatTime = value;
		}
	}

	// Token: 0x17000E74 RID: 3700
	// (get) Token: 0x0600332C RID: 13100 RVA: 0x000C8D48 File Offset: 0x000C6F48
	// (set) Token: 0x0600332D RID: 13101 RVA: 0x000C8D80 File Offset: 0x000C6F80
	public bool AutoComabat
	{
		get
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoComabat && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.LoadingFlag;
		}
		set
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoComabat = value;
		}
	}

	// Token: 0x17000E75 RID: 3701
	// (get) Token: 0x0600332E RID: 13102 RVA: 0x000C8D94 File Offset: 0x000C6F94
	// (set) Token: 0x0600332F RID: 13103 RVA: 0x000C8DA8 File Offset: 0x000C6FA8
	public bool AutoUseDrag
	{
		get
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoUseDrag;
		}
		set
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoUseDrag = value;
		}
	}

	// Token: 0x17000E76 RID: 3702
	// (get) Token: 0x06003330 RID: 13104 RVA: 0x000C8DBC File Offset: 0x000C6FBC
	// (set) Token: 0x06003331 RID: 13105 RVA: 0x000C8DD0 File Offset: 0x000C6FD0
	public float AutoUseDragThreshold
	{
		get
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoUseDragThreshold;
		}
		set
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoUseDragThreshold = value;
		}
	}

	// Token: 0x17000E77 RID: 3703
	// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C8DE4 File Offset: 0x000C6FE4
	// (set) Token: 0x06003333 RID: 13107 RVA: 0x000C8DF8 File Offset: 0x000C6FF8
	public bool AutoUseSort
	{
		get
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoUseSort;
		}
		set
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.AutoUseSort = value;
		}
	}

	// Token: 0x06003334 RID: 13108 RVA: 0x000C8E0C File Offset: 0x000C700C
	public void EnterAutoCombat()
	{
		this.IsOpenAutoCombat = true;
		if (base.IsMoving)
		{
			this.AutoComabat = false;
			this.BreakAutoCombatTime = Time.time;
		}
		else
		{
			this.AutoComabat = true;
		}
		this.UpdateShowAuotState();
	}

	// Token: 0x06003335 RID: 13109 RVA: 0x000C8E50 File Offset: 0x000C7050
	public void LeveAutoCombat()
	{
		this.AutoComabat = false;
		this.IsOpenAutoCombat = false;
		this.UpdateShowAuotState();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateAutoBtn();
		}
		if (SingletonUnity<CopyFunctionRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CopyFunctionRootLogic>.Instance.gameObject))
		{
			SingletonUnity<CopyFunctionRootLogic>.Instance.UpdateAutoFightBtn();
		}
	}

	// Token: 0x06003336 RID: 13110 RVA: 0x000C8EC4 File Offset: 0x000C70C4
	public void ReturnAutoCombatState()
	{
		if (this.IsOpenAutoCombat && !this.AutoComabat)
		{
			this.AutoComabat = true;
			this.BreakAutoCombatTime = 0f;
		}
	}

	// Token: 0x17000E78 RID: 3704
	// (get) Token: 0x06003337 RID: 13111 RVA: 0x000C8EFC File Offset: 0x000C70FC
	// (set) Token: 0x06003338 RID: 13112 RVA: 0x000C8F04 File Offset: 0x000C7104
	public bool AutoInviteTeamAccept
	{
		get
		{
			return this.mAutoInviteTeamAccept;
		}
		set
		{
			this.mAutoInviteTeamAccept = value;
		}
	}

	// Token: 0x17000E79 RID: 3705
	// (get) Token: 0x06003339 RID: 13113 RVA: 0x000C8F10 File Offset: 0x000C7110
	// (set) Token: 0x0600333A RID: 13114 RVA: 0x000C8F18 File Offset: 0x000C7118
	public bool AutoJoinTeamAccept
	{
		get
		{
			return this.mAutoJoinTeamAccept;
		}
		set
		{
			this.mAutoJoinTeamAccept = value;
		}
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x000C8F24 File Offset: 0x000C7124
	private void InitAutoInfo()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AutoComboInfoUI, delegate(bool bSuccess, object param)
		{
			this.mAutoComboInfoLogic = SingletonUnity<AutoComboInfoLogic>.Instance;
		}, null);
	}

	// Token: 0x0600333C RID: 13116 RVA: 0x000C8F44 File Offset: 0x000C7144
	public GameItem SelectDragItem()
	{
		int num = 0;
		if (this.dragList == null || this.dragList.Count <= 0)
		{
			this.dragList = ItemContainerTool.GetTargetPotionItemLevel(this.mGameManager.PlayerData.ItemBackPack, this.AttributeData.Level);
		}
		while (this.dragList.Count > 0)
		{
			if (!this.AutoUseSort)
			{
				num = this.dragList.Count - 1;
			}
			GameItem gameItem = this.dragList[num];
			if (gameItem != null && !gameItem.IsEmpty())
			{
				return gameItem;
			}
			this.dragList.RemoveAt(num);
		}
		this.dragList = ItemContainerTool.GetTargetPotionItemLevel(this.mGameManager.PlayerData.ItemBackPack, this.AttributeData.Level);
		while (this.dragList.Count > 0)
		{
			if (!this.AutoUseSort)
			{
				num = this.dragList.Count - 1;
			}
			GameItem gameItem2 = this.dragList[num];
			if (gameItem2 != null && !gameItem2.IsEmpty())
			{
				return gameItem2;
			}
			this.dragList.RemoveAt(num);
		}
		return null;
	}

	// Token: 0x0600333D RID: 13117 RVA: 0x000C9074 File Offset: 0x000C7274
	private void UpdateUseDrag()
	{
		if (!this.IsOpenAutoCombat)
		{
			return;
		}
		if (!this.AutoUseDrag)
		{
			return;
		}
		if ((float)this.AttributeData.HP / (float)this.AttributeData.MaxHP <= this.AutoUseDragThreshold)
		{
			GameItem gameItem = this.SelectDragItem();
			if (gameItem != null && this.UseDrag(gameItem))
			{
				PotionLogic.AutoUpdateSelectItem(gameItem);
			}
		}
	}

	// Token: 0x0600333E RID: 13118 RVA: 0x000C90DC File Offset: 0x000C72DC
	private void UpdateShowAuotState()
	{
		if (this.mAutoComboInfoLogic == null)
		{
			this.InitAutoInfo();
		}
		else
		{
			this.mAutoComboInfoLogic.Show(this.AutoComabat && this.currentCharacter != null);
		}
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x000C912C File Offset: 0x000C732C
	public bool GetAutoCombatState()
	{
		return this.AutoComabat && this.currentCharacter != null;
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x000C9148 File Offset: 0x000C7348
	public void ClearAutoSelectCharacter()
	{
		this.currentCharacter = null;
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x000C9154 File Offset: 0x000C7354
	public void BreakAutoCombatState()
	{
		this.AutoComabat = false;
		this.BreakAutoCombatTime = Time.time;
		this.currentCharacter = null;
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x000C9170 File Offset: 0x000C7370
	public void StopAutoAndSkill()
	{
		this.BreakAutoCombatState();
		base.SkillLogic.BreakCurSkill();
	}

	// Token: 0x06003343 RID: 13123 RVA: 0x000C9184 File Offset: 0x000C7384
	public void UpdateAutoCombatBreakState()
	{
		if (this.IsOpenAutoCombat && !this.AutoComabat)
		{
			if (base.IsMoving || this.IsTalking || this.mCurPlayerState == PLAYER_STATE.DANCE)
			{
				this.BreakAutoCombatTime = Time.time;
			}
			if (Time.time - this.BreakAutoCombatTime >= 2f)
			{
				this.AutoComabat = true;
			}
		}
		else
		{
			this.BreakAutoCombatTime = 0f;
		}
	}

	// Token: 0x06003344 RID: 13124 RVA: 0x000C9204 File Offset: 0x000C7404
	public string PickSkill(List<string> list)
	{
		if (list == null || list.Count == 0)
		{
			return string.Empty;
		}
		string result = string.Empty;
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			SkillData skillDataById = DataManager.GetSkillDataById(list[i]);
			if (skillDataById != null && skillDataById.PriorityAutoCombat > num && skillDataById.PriorityAutoCombat > 0)
			{
				num = skillDataById.PriorityAutoCombat;
				result = list[i];
			}
		}
		return result;
	}

	// Token: 0x06003345 RID: 13125 RVA: 0x000C9284 File Offset: 0x000C7484
	public string SeleSkill()
	{
		this.skillList.Clear();
		for (int i = 1; i < this.PlayerSkillIDList.Count; i++)
		{
			CharacterSkillData characterSkillDataByID = base.GetCharacterSkillDataByID(this.PlayerSkillIDList[i]);
			if (characterSkillDataByID != null && characterSkillDataByID.CDTimeCount <= 0f && characterSkillDataByID.UnlockLevel <= this.AttributeData.Level)
			{
				this.skillList.Add(this.PlayerSkillIDList[i]);
			}
		}
		if (this.skillList.Count > 0)
		{
			return this.PickSkill(this.skillList);
		}
		return string.Empty;
	}

	// Token: 0x06003346 RID: 13126 RVA: 0x000C9334 File Offset: 0x000C7534
	private void UpdateAuto()
	{
		this.UpdateShowAuotState();
		if (base.IsDie)
		{
			return;
		}
		this.UpdateUseDrag();
		if (!this.AutoComabat)
		{
			return;
		}
		if (!GameManager.OnLineState)
		{
			return;
		}
		if (this.mSkillLogic.IsUsingSkill)
		{
			this.lastUseTime = Time.time;
			return;
		}
		if ((double)(Time.time - this.lastUseTime) < 0.1)
		{
			return;
		}
		this.lastUseTime = Time.time;
		string text = this.SeleSkill();
		if (this.currentCharacter != this.mSelectedTarget && this.mSelectedTarget != null && Singleton<ObjManager>.Instance.IsCanAttack(this.mSelectedTarget))
		{
			this.currentCharacter = this.mSelectedTarget;
		}
		if (this.currentCharacter == null || this.currentCharacter.IsDie || !UnityVersionUtil.IsActive(this.currentCharacter.gameObject) || Vector3.SqrMagnitude(this.currentCharacter.Position - base.Position) > 64f)
		{
			this.currentCharacter = Singleton<ObjManager>.Instance.FindCanAttackCharacter((int)this.AttributeData.Camp, base.Position);
			if (this.currentCharacter == null)
			{
				if (this.mCurSceneManager == null)
				{
					this.mCurSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
				}
				this.mCurSceneManager.AutoFightAction();
				return;
			}
		}
		this.SelectTarget(this.currentCharacter);
		if (string.IsNullOrEmpty(text))
		{
			this.UseComboSkill();
		}
		else
		{
			SkillData skillDataById = DataManager.GetSkillDataById(text);
			float num = skillDataById.AutoAttackDistanceMeter + this.currentCharacter.ModelRadius;
			float num2 = Vector3.Distance(base.Position, this.currentCharacter.Position);
			if (num2 > num)
			{
				this.MoveTo(this.currentCharacter.Position, num2 - num - 0.5f, null);
				return;
			}
			this.UseSkill(text, null);
		}
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x000C9534 File Offset: 0x000C7734
	public bool IsInLeaveGuildTime()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() - this.LeaveGuildTime < 86400L;
	}

	// Token: 0x06003348 RID: 13128 RVA: 0x000C9554 File Offset: 0x000C7754
	public void CreatGuild(string GuildName, string notice, long icon, GameDefine.MONEY_TYPE type)
	{
		if (string.IsNullOrEmpty(GuildName))
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			return;
		}
		if (type == GameDefine.MONEY_TYPE.GOLD)
		{
			if (!GameMoneyHelper.BeforeCheckBuy(type, 499))
			{
				return;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.DIAMOND && !GameMoneyHelper.BeforeCheckBuy(type, 49))
		{
			return;
		}
		guild_create.request request = new guild_create.request();
		request.guildName = GuildName;
		request.Icon = icon;
		if (!string.IsNullOrEmpty(notice))
		{
			request.notice = notice;
		}
		request.costType = (long)type;
		NetLogic.GetInstance().Send<Protocol.guild_create>(request, null);
		if (type == GameDefine.MONEY_TYPE.GOLD)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Guild", "Create", "type_cash");
		}
		else if (type == GameDefine.MONEY_TYPE.DIAMOND)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Guild", "Create", "type_diamond");
		}
	}

	// Token: 0x06003349 RID: 13129 RVA: 0x000C963C File Offset: 0x000C783C
	public void OpenGuild()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			guild_req_info.request request = new guild_req_info.request();
			request.characterId = this.ServerId;
			NetLogic.GetInstance().Send<Protocol.guild_req_info>(request, null);
		}
		else
		{
			guild_req_list.request request2 = new guild_req_list.request();
			request2.characterId = this.ServerId;
			request2.curPage = 1L;
			NetLogic.GetInstance().Send<Protocol.guild_req_list>(request2, null);
		}
	}

	// Token: 0x0600334A RID: 13130 RVA: 0x000C96A8 File Offset: 0x000C78A8
	public void ApplyUpDataGuild()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			guild_req_info.request request = new guild_req_info.request();
			request.characterId = this.ServerId;
			NetLogic.GetInstance().Send<Protocol.guild_req_info>(request, null);
		}
	}

	// Token: 0x0600334B RID: 13131 RVA: 0x000C96EC File Offset: 0x000C78EC
	public void ApplyUpdataGuildMemberList()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			req_guild_member_info.request request = new req_guild_member_info.request();
			request.guildId = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId;
			NetLogic.GetInstance().Send<Protocol.req_guild_member_info>(request, null);
		}
	}

	// Token: 0x0600334C RID: 13132 RVA: 0x000C973C File Offset: 0x000C793C
	public void ReqGuildSkill()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NetLogic.GetInstance().Send<Protocol.req_guild_skill>(null, null);
		}
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x000C976C File Offset: 0x000C796C
	public bool JoinGuild(long GuildId)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			return false;
		}
		if (this.ApplyGuildIDList.Contains(GuildId))
		{
			return false;
		}
		if (Singleton<ObjManager>.Instance.MainPlayer != null && Singleton<ObjManager>.Instance.MainPlayer.IsInLeaveGuildTime())
		{
			NoticeLogic.AddNotifyData("#{100777}", true, false);
			return false;
		}
		if (this.ApplyGuildIDList.Count <= 30)
		{
			this.ApplyGuildIDList.Add(GuildId);
			guild_join.request request = new guild_join.request();
			request.guildId = GuildId;
			NetLogic.GetInstance().Send<Protocol.guild_join>(request, null);
			return true;
		}
		NoticeLogic.AddNotifyData("#{100779}*30", true, false);
		return false;
	}

	// Token: 0x0600334E RID: 13134 RVA: 0x000C9824 File Offset: 0x000C7A24
	public void LeaveGuild()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanLeaveGuild())
		{
			guild_leave.request request = new guild_leave.request();
			request.characterId = this.ServerId;
			NetLogic.GetInstance().Send<Protocol.guild_leave>(request, null);
		}
	}

	// Token: 0x0600334F RID: 13135 RVA: 0x000C986C File Offset: 0x000C7A6C
	public void KickGuildMember(long ID)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanKickedMember(ID))
		{
			guild_kick.request request = new guild_kick.request();
			request.characterId = ID;
			NetLogic.GetInstance().Send<Protocol.guild_kick>(request, null);
		}
	}

	// Token: 0x06003350 RID: 13136 RVA: 0x000C98B0 File Offset: 0x000C7AB0
	public void ChangeMemberJob(long ID, Guild_JOB job)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanChangMemmberJob(job))
		{
			guild_job_change.request request = new guild_job_change.request();
			request.characterId = ID;
			request.jobId = (long)job;
			NetLogic.GetInstance().Send<Protocol.guild_job_change>(request, null);
		}
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x000C98FC File Offset: 0x000C7AFC
	public void UpGuildLevel()
	{
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x000C9900 File Offset: 0x000C7B00
	public void SearchAllGuild()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			guild_req_list.request request = new guild_req_list.request();
			request.characterId = this.ServerId;
			NetLogic.GetInstance().Send<Protocol.guild_req_list>(request, null);
		}
	}

	// Token: 0x06003353 RID: 13139 RVA: 0x000C9940 File Offset: 0x000C7B40
	public void AgreeJoinGuild(long ID)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanApprove())
		{
			guild_approve_resverve.request request = new guild_approve_resverve.request();
			request.characterId = ID;
			request.isAgree = 1L;
			NetLogic.GetInstance().Send<Protocol.guild_approve_resverve>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Guild", "Member", "agree_times");
		}
	}

	// Token: 0x06003354 RID: 13140 RVA: 0x000C99A4 File Offset: 0x000C7BA4
	public void DisAgreeJoinGuild(long ID)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanApprove())
		{
			guild_approve_resverve.request request = new guild_approve_resverve.request();
			request.characterId = ID;
			request.isAgree = 0L;
			NetLogic.GetInstance().Send<Protocol.guild_approve_resverve>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Guild", "Member", "disagree_times");
		}
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x000C9A08 File Offset: 0x000C7C08
	public void LookAtLog()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			guild_log.request rpcReq = new guild_log.request();
			NetLogic.GetInstance().Send<Protocol.guild_log>(rpcReq, null);
		}
	}

	// Token: 0x06003356 RID: 13142 RVA: 0x000C9A40 File Offset: 0x000C7C40
	public void ChangeGuildNotice(string notice)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanEditNotice())
		{
			req_guild_notice.request request = new req_guild_notice.request();
			request.notice = notice;
			NetLogic.GetInstance().Send<Protocol.req_guild_notice>(request, null);
		}
	}

	// Token: 0x06003357 RID: 13143 RVA: 0x000C9A84 File Offset: 0x000C7C84
	public void GuildDonate(GuildDonateData curData)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild())
		{
			if (GameDefine.ITEM_ID_MONEYTYPR.ContainsKey(curData.ItemID))
			{
				GameDefine.MONEY_TYPE type = GameDefine.ITEM_ID_MONEYTYPR[curData.ItemID];
				if (!GameMoneyHelper.BeforeCheckBuy(type, curData.ItemCount))
				{
					return;
				}
			}
			guild_donate.request request = new guild_donate.request();
			request.id = curData.ID;
			NetLogic.GetInstance().Send<Protocol.guild_donate>(request, null);
		}
	}

	// Token: 0x06003358 RID: 13144 RVA: 0x000C9B00 File Offset: 0x000C7D00
	public void SetGuildNeedAppro(bool needAppro)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerGuild.CanSetApprove())
		{
			req_seting_guild_appro.request request = new req_seting_guild_appro.request();
			request.guildId = playerData.PlayerGuild.ServerId;
			request.isNeedAppro = needAppro;
			NetLogic.GetInstance().Send<Protocol.req_seting_guild_appro>(request, null);
		}
	}

	// Token: 0x06003359 RID: 13145 RVA: 0x000C9B54 File Offset: 0x000C7D54
	public void ReqInviteTeam(long teamId)
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (instance.PlayerData.IsHaveTeam() && instance.PlayerData.TeamInfo.IsFull())
		{
			this.SendNotify(false, "team is full", new object[0]);
			return;
		}
		if (teamId != -1L)
		{
			this.SendNotify(false, "invite is send", new object[0]);
		}
		req_invite_team.request request = new req_invite_team.request();
		request.characterid = teamId;
		NetLogic.GetInstance().Send<Protocol.req_invite_team>(request, null);
	}

	// Token: 0x0600335A RID: 13146 RVA: 0x000C9BD4 File Offset: 0x000C7DD4
	public void ReqJoinTeam(long memberId)
	{
	}

	// Token: 0x0600335B RID: 13147 RVA: 0x000C9BD8 File Offset: 0x000C7DD8
	public void ReqLeaveTeam()
	{
	}

	// Token: 0x0600335C RID: 13148 RVA: 0x000C9BDC File Offset: 0x000C7DDC
	public void ReqKickTeamMenber(long memberId)
	{
	}

	// Token: 0x0600335D RID: 13149 RVA: 0x000C9BE0 File Offset: 0x000C7DE0
	public void ReqChangeTeamLeader(long memberId)
	{
	}

	// Token: 0x0600335E RID: 13150 RVA: 0x000C9BE4 File Offset: 0x000C7DE4
	public void LeaveTeam()
	{
	}

	// Token: 0x0600335F RID: 13151 RVA: 0x000C9BE8 File Offset: 0x000C7DE8
	public bool IsTeamLeader(long id)
	{
		return id != -1L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.TeamID != -1L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.GetTeamber(0).ServerId == id;
	}

	// Token: 0x06003360 RID: 13152 RVA: 0x000C9C40 File Offset: 0x000C7E40
	public bool IsTeamLeader()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.TeamID != -1L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.GetTeamber(0).ServerId == this.ServerId;
	}

	// Token: 0x06003361 RID: 13153 RVA: 0x000C9C94 File Offset: 0x000C7E94
	private void UpdateFollowSpeed()
	{
		if (this.mFollowCharacter == null)
		{
			this.Speed = this.AttributeData.CurSpeed;
			if (this.Speed != this.mNavMeshAgent.speed)
			{
				this.mNavMeshAgent.speed = this.Speed;
				if (base.MountRoot != null)
				{
					base.MountRoot.UpdateSetSpeed(this.Speed);
				}
			}
			return;
		}
		if (Vector3.SqrMagnitude(this.mFollowCharacter.Position - base.Position) <= 16f)
		{
			this.Speed = this.mFollowCharacter.AttributeData.WalkSpeed;
			this.isWalk = true;
		}
		else
		{
			this.isWalk = false;
			this.Speed = this.AttributeData.CurSpeed;
		}
		if (this.Speed != this.mNavMeshAgent.speed)
		{
			this.mNavMeshAgent.speed = this.Speed;
			if (base.MountRoot != null)
			{
				base.MountRoot.UpdateSetSpeed(this.Speed);
			}
		}
	}

	// Token: 0x17000E7A RID: 3706
	// (get) Token: 0x06003362 RID: 13154 RVA: 0x000C9DB8 File Offset: 0x000C7FB8
	// (set) Token: 0x06003363 RID: 13155 RVA: 0x000C9DC0 File Offset: 0x000C7FC0
	public long FollowServerID
	{
		get
		{
			return this.mFollowServerID;
		}
		set
		{
			this.Speed = -1f;
			this.mFollowServerID = value;
			this.mFollowCharacter = Singleton<ObjManager>.Instance.FindObjInScene(value);
			this.UpdateFollowSpeed();
		}
	}

	// Token: 0x06003364 RID: 13156 RVA: 0x000C9DEC File Offset: 0x000C7FEC
	public void EnterTeamFollow()
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (instance.SceneManager.IsCopyScene())
		{
			return;
		}
		if (instance.PlayerData.TeamInfo.TeamID == -1L)
		{
			return;
		}
		if (this.IsTeamLeader())
		{
			return;
		}
		TeamMember teamber = instance.PlayerData.TeamInfo.GetTeamber(0);
		if (teamber.IsValid())
		{
			foreach (KeyValuePair<long, Obj> keyValuePair in Singleton<ObjManager>.Instance.ObjDict)
			{
				if (keyValuePair.Value.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
				{
					ObjOtherPlayer objOtherPlayer = keyValuePair.Value as ObjOtherPlayer;
					if (objOtherPlayer != null && objOtherPlayer.ServerId == teamber.ServerId)
					{
						this.FollowServerID = objOtherPlayer.ServerId;
						break;
					}
				}
			}
		}
	}

	// Token: 0x06003365 RID: 13157 RVA: 0x000C9EF8 File Offset: 0x000C80F8
	public void EnterFollowTarget(long targetId)
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (instance.SceneManager.IsCopyScene())
		{
			return;
		}
		ObjManager instance2 = Singleton<ObjManager>.Instance;
		if (instance2.ObjDict.ContainsKey(targetId))
		{
			Obj obj = instance2.ObjDict[targetId];
			this.FollowServerID = obj.ServerId;
		}
	}

	// Token: 0x06003366 RID: 13158 RVA: 0x000C9F4C File Offset: 0x000C814C
	public void LeaveTeamFollow()
	{
		this.mFollowServerID = -1L;
		this.mFollowCharacter = null;
	}

	// Token: 0x06003367 RID: 13159 RVA: 0x000C9F60 File Offset: 0x000C8160
	public bool IsTeamFollowState()
	{
		return this.mFollowServerID != -1L;
	}

	// Token: 0x06003368 RID: 13160 RVA: 0x000C9F70 File Offset: 0x000C8170
	public void UpdateTeamFollow()
	{
		if (!this.IsTeamFollowState())
		{
			return;
		}
		if (this.mFollowCharacter != null)
		{
			if (this.isWalk)
			{
				base.WalkMoveTo(this.mFollowCharacter.CacheTransform.position, 3f, null);
			}
			else
			{
				this.MoveTo(this.mFollowCharacter.CacheTransform.position, 3f, null);
			}
			this.UpdateFollowSpeed();
		}
	}

	// Token: 0x040021A3 RID: 8611
	private ThirdPersonController mThirdPersonController;

	// Token: 0x040021A4 RID: 8612
	private CameraController mCameraController;

	// Token: 0x040021A5 RID: 8613
	private PlayerData mCachePlayerData;

	// Token: 0x040021A6 RID: 8614
	private Material mXRayMat;

	// Token: 0x040021A7 RID: 8615
	private bool mIsTalking;

	// Token: 0x040021A8 RID: 8616
	private bool mIsNeedAutoMountCar;

	// Token: 0x040021A9 RID: 8617
	private float mStartAutoMoveTime;

	// Token: 0x040021AA RID: 8618
	private float AutoMountTime;

	// Token: 0x040021AB RID: 8619
	private bool mCompleteMissionFlag;

	// Token: 0x040021AC RID: 8620
	private GameManager mGameManager;

	// Token: 0x040021AD RID: 8621
	private float ftime;

	// Token: 0x040021AE RID: 8622
	private Vector3 mLastPosition;

	// Token: 0x040021AF RID: 8623
	private float timeWait;

	// Token: 0x040021B0 RID: 8624
	private move.request request;

	// Token: 0x040021B1 RID: 8625
	private position pos;

	// Token: 0x040021B2 RID: 8626
	private int relifeCount;

	// Token: 0x040021B3 RID: 8627
	public List<string> mWaitForSkillRetList;

	// Token: 0x040021B4 RID: 8628
	private List<ObjCharacter> mCurSearchTargetList;

	// Token: 0x040021B5 RID: 8629
	private float switchCD;

	// Token: 0x040021B6 RID: 8630
	private float lastChangeTime;

	// Token: 0x040021B7 RID: 8631
	private List<string> mPlayerSkillIDList;

	// Token: 0x040021B8 RID: 8632
	private bool mIsNeedUpdateCheck;

	// Token: 0x040021B9 RID: 8633
	private SceneManager mCurSceneManager;

	// Token: 0x040021BA RID: 8634
	private List<string> skillList = new List<string>();

	// Token: 0x040021BB RID: 8635
	protected bool mAutoInviteTeamAccept;

	// Token: 0x040021BC RID: 8636
	protected bool mAutoJoinTeamAccept;

	// Token: 0x040021BD RID: 8637
	private List<GameItem> dragList;

	// Token: 0x040021BE RID: 8638
	private float lastUseTime;

	// Token: 0x040021BF RID: 8639
	private AutoComboInfoLogic mAutoComboInfoLogic;

	// Token: 0x040021C0 RID: 8640
	private ObjCharacter currentCharacter;

	// Token: 0x040021C1 RID: 8641
	public List<long> ApplyGuildIDList = new List<long>();

	// Token: 0x040021C2 RID: 8642
	private float CurTime = -30f;

	// Token: 0x040021C3 RID: 8643
	private float RuquestTime = 30f;

	// Token: 0x040021C4 RID: 8644
	public long LeaveGuildTime = -1L;

	// Token: 0x040021C5 RID: 8645
	private long mFollowServerID = -1L;

	// Token: 0x040021C6 RID: 8646
	private ObjCharacter mFollowCharacter;

	// Token: 0x040021C7 RID: 8647
	private float Speed = -1f;

	// Token: 0x040021C8 RID: 8648
	private bool isWalk;
}
