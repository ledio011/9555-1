using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x02000829 RID: 2089
public class ObjOtherPlayer : ObjCharacter
{
	// Token: 0x0600348D RID: 13453 RVA: 0x000D37C0 File Offset: 0x000D19C0
	public ObjOtherPlayer()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER;
	}

	// Token: 0x17000EAC RID: 3756
	// (get) Token: 0x0600348F RID: 13455 RVA: 0x000D3860 File Offset: 0x000D1A60
	// (set) Token: 0x06003490 RID: 13456 RVA: 0x000D3868 File Offset: 0x000D1A68
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

	// Token: 0x17000EAD RID: 3757
	// (get) Token: 0x06003491 RID: 13457 RVA: 0x000D3874 File Offset: 0x000D1A74
	// (set) Token: 0x06003492 RID: 13458 RVA: 0x000D387C File Offset: 0x000D1A7C
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

	// Token: 0x17000EAE RID: 3758
	// (get) Token: 0x06003493 RID: 13459 RVA: 0x000D3888 File Offset: 0x000D1A88
	// (set) Token: 0x06003494 RID: 13460 RVA: 0x000D3890 File Offset: 0x000D1A90
	public GameObject VisibleRootObj
	{
		get
		{
			return this.mVisibleRootObj;
		}
		set
		{
			this.mVisibleRootObj = value;
		}
	}

	// Token: 0x17000EAF RID: 3759
	// (get) Token: 0x06003495 RID: 13461 RVA: 0x000D389C File Offset: 0x000D1A9C
	// (set) Token: 0x06003496 RID: 13462 RVA: 0x000D38A4 File Offset: 0x000D1AA4
	public virtual PROFESSION_TYPE Profession
	{
		get
		{
			return this.mProfession;
		}
		set
		{
			this.mProfession = value;
		}
	}

	// Token: 0x17000EB0 RID: 3760
	// (get) Token: 0x06003497 RID: 13463 RVA: 0x000D38B0 File Offset: 0x000D1AB0
	public string[] PartObjId
	{
		get
		{
			return this.mPartObjId;
		}
	}

	// Token: 0x17000EB1 RID: 3761
	// (get) Token: 0x06003498 RID: 13464 RVA: 0x000D38B8 File Offset: 0x000D1AB8
	public BundleManager.LoadModelData[] LoadingModelData
	{
		get
		{
			return this.mLoadingModelData;
		}
	}

	// Token: 0x17000EB2 RID: 3762
	// (get) Token: 0x06003499 RID: 13465 RVA: 0x000D38C0 File Offset: 0x000D1AC0
	public long[] LoadingModelDataId
	{
		get
		{
			return this.mLoadingModelDataId;
		}
	}

	// Token: 0x17000EB3 RID: 3763
	// (get) Token: 0x0600349A RID: 13466 RVA: 0x000D38C8 File Offset: 0x000D1AC8
	// (set) Token: 0x0600349B RID: 13467 RVA: 0x000D38D0 File Offset: 0x000D1AD0
	public GameObject[] PartObject
	{
		get
		{
			return this.mPartObject;
		}
		set
		{
			this.mPartObject = value;
		}
	}

	// Token: 0x17000EB4 RID: 3764
	// (get) Token: 0x0600349C RID: 13468 RVA: 0x000D38DC File Offset: 0x000D1ADC
	public Material[] PartMatList
	{
		get
		{
			return this.mPartMatList;
		}
	}

	// Token: 0x17000EB5 RID: 3765
	// (get) Token: 0x0600349D RID: 13469 RVA: 0x000D38E4 File Offset: 0x000D1AE4
	public List<GameObject>[] PartEffectList
	{
		get
		{
			return this.mPartEffectList;
		}
	}

	// Token: 0x0600349E RID: 13470 RVA: 0x000D38EC File Offset: 0x000D1AEC
	public void ClearPartEffect()
	{
		for (int i = 0; i < this.mPartEffectList.Length; i++)
		{
			if (this.mPartEffectList[i] != null)
			{
				for (int j = 0; j < this.mPartEffectList[i].Count; j++)
				{
					Object.Destroy(this.mPartEffectList[i][j].gameObject);
				}
			}
			this.mPartEffectList[i] = null;
		}
	}

	// Token: 0x0600349F RID: 13471 RVA: 0x000D3960 File Offset: 0x000D1B60
	public void ReshowPartEffect()
	{
		if (base.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			for (int i = 0; i < this.mPartObjId.Length; i++)
			{
				if (!string.IsNullOrEmpty(this.mPartObjId[i]))
				{
					Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(this.mPartObjId[i]), this);
				}
			}
		}
		else
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (playerData.IsShowFashion)
			{
				if (playerData.CheckWeaponIsSame())
				{
					if (!string.IsNullOrEmpty(playerData.FashionWeaponId))
					{
						Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.FashionWeaponId), this);
					}
				}
				else if (!string.IsNullOrEmpty(playerData.PartWeaponId))
				{
					Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.PartWeaponId), this);
				}
				Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.FashionHeadId), this);
				Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.FashionLegId), this);
				Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.FashionBodyId), this);
			}
			else
			{
				if (!string.IsNullOrEmpty(playerData.PartWeaponId))
				{
					Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.PartWeaponId), this);
				}
				Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.PartHeadId), this);
				Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.PartLegId), this);
				Singleton<ObjManager>.Instance.CheckModelEffect(DataManager.GetModeDataByID(playerData.PartBodyId), this);
			}
		}
	}

	// Token: 0x17000EB6 RID: 3766
	// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000D3AE4 File Offset: 0x000D1CE4
	// (set) Token: 0x060034A1 RID: 13473 RVA: 0x000D3AF4 File Offset: 0x000D1CF4
	public virtual long GuildId
	{
		get
		{
			return this.AttributeData.GuildId;
		}
		set
		{
			this.AttributeData.GuildId = value;
		}
	}

	// Token: 0x17000EB7 RID: 3767
	// (get) Token: 0x060034A2 RID: 13474 RVA: 0x000D3B04 File Offset: 0x000D1D04
	// (set) Token: 0x060034A3 RID: 13475 RVA: 0x000D3B0C File Offset: 0x000D1D0C
	public virtual long TeamId
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

	// Token: 0x060034A4 RID: 13476 RVA: 0x000D3B18 File Offset: 0x000D1D18
	private void InitOtherPlayer()
	{
		if (this.mAutoMoveLogic == null)
		{
			this.mAutoMoveLogic = base.gameObject.AddComponent<AutoMoveLogic>();
		}
		this.mAutoMoveLogic.Init(this);
		this.BonesTrsDict = base.gameObject.GetComponentInChildren<TransDicts>().TransDict;
	}

	// Token: 0x060034A5 RID: 13477 RVA: 0x000D3B6C File Offset: 0x000D1D6C
	public override void Init()
	{
		base.Init();
		this.mVisibleRootObj = base.CacheTransform.FindChild("ModelRoot").gameObject;
		this.InitOtherPlayer();
	}

	// Token: 0x060034A6 RID: 13478 RVA: 0x000D3BA0 File Offset: 0x000D1DA0
	private void OnDisable()
	{
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.Stop();
			this.mNavMeshAgent.ResetPath();
			this.mNavMeshAgent.enabled = false;
		}
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x000D3BF0 File Offset: 0x000D1DF0
	public void ResetOtherPlayer(ObjInitPlayerData playerInitData)
	{
		base.Reset();
		base.Position = playerInitData.mPos;
		base.CacheTransform.forward = playerInitData.mDir;
		this.ServerId = playerInitData.mServerID;
		this.Profession = playerInitData.Profession;
		this.AttributeData.InitData(playerInitData.Attribute, playerInitData.AttributeAll);
		this.AttributeData.HP = (long)playerInitData.HP;
		this.AttributeData.Name = playerInitData.Name;
		this.AttributeData.GuildName = playerInitData.GuildName;
		this.AttributeData.GuildId = playerInitData.GuildId;
		this.AttributeData.CurEXP = playerInitData.EXP;
		this.AttributeData.Level = playerInitData.Level;
		this.AttributeData.CurSpeed = playerInitData.Speed;
		this.AttributeData.WalkSpeed = playerInitData.WalkSpeed;
		this.AttributeData.CurRec = playerInitData.Rec;
		this.AttributeData.ComboValue = playerInitData.ComboValue;
		this.AttributeData.Camp = playerInitData.Camp;
		this.AttributeData.PkMode = playerInitData.PkMode;
		this.AttributeData.DanceState = playerInitData.DanceState;
		this.AttributeData.DanceId = playerInitData.DanceId;
		this.AttributeData.CurTitleLevel = playerInitData.TitleLevel;
		this.GuildId = playerInitData.GuildId;
		this.TeamId = playerInitData.TeamId;
		this.mIsServerRidingMount = (playerInitData.visual.mount_state == 1L);
		this.mMountId = playerInitData.visual.MountId;
		this.mMountColor = playerInitData.visual.mount_color;
		this.UpdateSkillList(playerInitData.skills);
		this.InitHeadInfo();
		base.InitNavMeshAgent();
		if (this.mAutoMoveLogic != null)
		{
			this.mAutoMoveLogic.Reset();
		}
		this.mServerNotDieNumCount = 0;
		this.mLoadingMountFlag = false;
		if (this.AttributeData.HP <= 0L)
		{
			this.OnDie();
			this.mAnimationLogic.ForcePlayAnimation(this.mAnimationLogic.animationName[3], null, -1f, 0.9f);
		}
		this.SetVisible(playerInitData.IsVisible);
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x000D3E28 File Offset: 0x000D2028
	public virtual void ReLoadPlayerVisual(characterVisual visual)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		string weaponTypeName = base.WeaponTypeName;
		base.UpdateWeaponModeID(visual.WeaponId);
		if (visual.HasWeaponItemId)
		{
			base.UpdateWeaponItemID(visual.WeaponItemId);
		}
		string weaponTypeName2 = base.WeaponTypeName;
		if (visual.showType == 0L)
		{
			text = visual.WeaponId;
			text2 = visual.HeadId;
			text3 = visual.BodyId;
			text4 = visual.LegId;
		}
		else
		{
			if (this.CheckWeaponIsSame(visual))
			{
				text = ((!visual.HasFashion_WeaponId) ? visual.WeaponId : visual.Fashion_WeaponId);
			}
			else
			{
				text = visual.WeaponId;
			}
			text2 = ((!visual.HasFashion_HeadId) ? visual.HeadId : visual.Fashion_HeadId);
			text3 = ((!visual.HasFashion_BodyId) ? visual.BodyId : visual.Fashion_BodyId);
			text4 = ((!visual.HasFashion_LegId) ? visual.LegId : visual.Fashion_LegId);
		}
		if (UnityVersionUtil.IsActive(this.VisibleRootObj))
		{
			Singleton<ObjManager>.Instance.LoadPlayerVisual(this, text, text2, text3, text4);
		}
		else
		{
			this.TargetPartObjId[1] = text2;
			this.TargetPartObjId[2] = text3;
			this.TargetPartObjId[3] = text4;
			this.TargetPartObjId[0] = text;
		}
		if (string.IsNullOrEmpty(weaponTypeName) || !weaponTypeName.Equals(weaponTypeName2))
		{
			this.ChangeWeaponAnimaCheck();
		}
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x000D3FA0 File Offset: 0x000D21A0
	public bool CheckWeaponIsSame(characterVisual visual)
	{
		if (visual.HasWeaponItemId && visual.HasFashionItemId)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(visual.WeaponItemId);
			EquipData equipDataById2 = DataManager.GetEquipDataById(visual.FashionItemId);
			return equipDataById.WeaponType == equipDataById2.WeaponType;
		}
		return !visual.HasWeaponId || !visual.HasFashion_WeaponId || GameDefine.GetWeaponName(visual.WeaponId).Equals(GameDefine.GetWeaponName(visual.Fashion_WeaponId));
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x000D4028 File Offset: 0x000D2228
	public override void UpdatePlayerSpeed()
	{
		if (this.AttributeData.CurSpeed != this.mNavMeshAgent.speed)
		{
			this.mNavMeshAgent.speed = this.AttributeData.CurSpeed;
			if (this.MountRoot != null)
			{
				this.MountRoot.UpdateSpeed();
			}
		}
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x000D4084 File Offset: 0x000D2284
	public override void UpdateMountSpeed()
	{
		if (this.MountRoot != null)
		{
			this.MountRoot.UpdateSpeed();
		}
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x000D40A4 File Offset: 0x000D22A4
	public void InitHeadInfo()
	{
		ResourcesManager.LoadHeadInfoPrefab(UIInfo.PlayerHeadInfoUI, "PlayerHeadInfoRoot", new ResourcesManager.LoadHeadInfoDelegate(this.LoadOtherPlayerHeadInfo));
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x000D40C4 File Offset: 0x000D22C4
	private void LoadOtherPlayerHeadInfo(GameObject obj)
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
			billBoard.DeltaHeight = base.CurrentCharacterModelData.ModelHeight + this.PLAYER_NAME_DELTA_HIGHT;
			PlayerHeadInfoLogic component = obj.GetComponent<PlayerHeadInfoLogic>();
			this.mHeadInfoLogic = component;
			bool flag = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerCamp == this.AttributeData.Camp;
			component.Reset(false, this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, this.AttributeData.Camp, true, this.AttributeData.IsChampionGuild());
		}
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x000D418C File Offset: 0x000D238C
	public void SetNormalNameHeight()
	{
		BillBoard component = this.mHeadInfoLogic.gameObject.GetComponent<BillBoard>();
		component.DeltaHeight = base.CurrentCharacterModelData.ModelHeight + this.PLAYER_NAME_DELTA_HIGHT;
	}

	// Token: 0x060034AF RID: 13487 RVA: 0x000D41C4 File Offset: 0x000D23C4
	public void SetDrivingNameHeight(MountData mountData)
	{
		BillBoard component = this.mHeadInfoLogic.gameObject.GetComponent<BillBoard>();
		component.DeltaHeight = mountData.NameHeight;
	}

	// Token: 0x060034B0 RID: 13488 RVA: 0x000D41F0 File Offset: 0x000D23F0
	public override void RefreshHeadInfo()
	{
		if (base.IsDie)
		{
			return;
		}
		if (this.mHeadInfoLogic != null)
		{
			PlayerHeadInfoLogic playerHeadInfoLogic = this.mHeadInfoLogic as PlayerHeadInfoLogic;
			if (playerHeadInfoLogic != null)
			{
				playerHeadInfoLogic.Refresh(this.AttributeData.CurTitleLevel, this.AttributeData.Name, this.AttributeData.GuildName, this.AttributeData.IsChampionGuild());
			}
		}
	}

	// Token: 0x060034B1 RID: 13489 RVA: 0x000D4264 File Offset: 0x000D2464
	private void Update()
	{
		base.UpdateComponent();
		base.UpdateMove();
		base.SkillLogic.UpdateSkill();
	}

	// Token: 0x060034B2 RID: 13490 RVA: 0x000D4280 File Offset: 0x000D2480
	public override void OnDie()
	{
		if (this.mObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && this.IsDrivingMount())
		{
			this.DisMountCar();
		}
		base.OnDie();
	}

	// Token: 0x060034B3 RID: 13491 RVA: 0x000D42A8 File Offset: 0x000D24A8
	public void SetVisible(bool visible)
	{
		UnityVersionUtil.SetActiveRecursive(this.mVisibleRootObj.gameObject, visible);
		if (visible)
		{
			for (int i = 0; i < this.mPartMatList.Length; i++)
			{
				if (this.mPartMatList[i] != null)
				{
					Color color = this.mPartMatList[i].GetColor("_DefaultColor");
					this.mPartMatList[i].SetColor("_Color", new Color(color.r, color.g, color.b, 0f));
					ShortcutExtensions.DOColor(this.mPartMatList[i], color, 1.5f);
				}
			}
		}
		update_client_state.request request = new update_client_state.request();
		request.id = this.mServerId;
		request.state = 1L;
		NetLogic.GetInstance().Send<Protocol.update_client_state>(request, null);
		if (this.IsServerRidingMount && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld())
		{
			this.MountCar(this.MountId, this.MountColor);
		}
		if (this.AttributeData.DanceState == 1)
		{
			this.StartDance(this.AttributeData.DanceId);
		}
	}

	// Token: 0x060034B4 RID: 13492 RVA: 0x000D43CC File Offset: 0x000D25CC
	public bool IsVisible()
	{
		return this.mVisibleRootObj != null && UnityVersionUtil.IsActive(this.mVisibleRootObj.gameObject);
	}

	// Token: 0x060034B5 RID: 13493 RVA: 0x000D4400 File Offset: 0x000D2600
	protected override void ChangeRunState()
	{
		if (base.GetWeaponType() == 2 && this.mIdleAttackFlag)
		{
			this.mAnimationLogic.PlayAnimation(ObjOtherPlayer.NQS_run_naQiang_ActName, null, -1f);
		}
		else
		{
			this.mAnimationLogic.PlayAnimation(1, null);
		}
	}

	// Token: 0x17000EB8 RID: 3768
	// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000D444C File Offset: 0x000D264C
	// (set) Token: 0x060034B7 RID: 13495 RVA: 0x000D4454 File Offset: 0x000D2654
	public virtual string MountId
	{
		get
		{
			return this.mMountId;
		}
		set
		{
			this.mMountId = value;
		}
	}

	// Token: 0x17000EB9 RID: 3769
	// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000D4460 File Offset: 0x000D2660
	// (set) Token: 0x060034B9 RID: 13497 RVA: 0x000D4468 File Offset: 0x000D2668
	public virtual string MountColor
	{
		get
		{
			return this.mMountColor;
		}
		set
		{
			this.mMountColor = value;
		}
	}

	// Token: 0x17000EBA RID: 3770
	// (get) Token: 0x060034BA RID: 13498 RVA: 0x000D4474 File Offset: 0x000D2674
	public ObjPlayerMountCar MountRoot
	{
		get
		{
			return this.mMountRoot;
		}
	}

	// Token: 0x17000EBB RID: 3771
	// (get) Token: 0x060034BB RID: 13499 RVA: 0x000D447C File Offset: 0x000D267C
	// (set) Token: 0x060034BC RID: 13500 RVA: 0x000D4484 File Offset: 0x000D2684
	public virtual bool IsServerRidingMount
	{
		get
		{
			return this.mIsServerRidingMount;
		}
		set
		{
			this.mIsServerRidingMount = value;
		}
	}

	// Token: 0x060034BD RID: 13501 RVA: 0x000D4490 File Offset: 0x000D2690
	public virtual bool PlayeSocialDance(string danceId)
	{
		if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			return false;
		}
		if (this.IsDrivingMount())
		{
			this.DisMountCar();
		}
		else if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.StopDance();
		}
		base.SkillLogic.BreakCurSkill();
		if (base.IsMoving)
		{
			this.StopMove();
		}
		this.mCurPlayerState = PLAYER_STATE.SOCIAL_DANCE;
		SocialDanceData socialDanceDataById = DataManager.GetSocialDanceDataById(danceId);
		if (socialDanceDataById == null)
		{
			return false;
		}
		base.AnimationLogic.PlayAnimation(socialDanceDataById.ActionID, new AnimationLogic.OnAnimFinished(this.SocialDanceFinish), -1f);
		return true;
	}

	// Token: 0x060034BE RID: 13502 RVA: 0x000D452C File Offset: 0x000D272C
	public virtual void SocialDanceFinish()
	{
		if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			this.mCurPlayerState = PLAYER_STATE.NORMAL;
		}
	}

	// Token: 0x060034BF RID: 13503 RVA: 0x000D4544 File Offset: 0x000D2744
	public void StopSocialDance()
	{
		if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			this.mCurPlayerState = PLAYER_STATE.NORMAL;
			base.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
		}
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x000D4560 File Offset: 0x000D2760
	public bool IsDrivingMount()
	{
		return base.CurPlayerState == PLAYER_STATE.DRIVING || this.mLoadingMountFlag;
	}

	// Token: 0x17000EBC RID: 3772
	// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000D457C File Offset: 0x000D277C
	// (set) Token: 0x060034C2 RID: 13506 RVA: 0x000D4584 File Offset: 0x000D2784
	public bool LoadingMountFlag
	{
		get
		{
			return this.mLoadingMountFlag;
		}
		set
		{
			this.mLoadingMountFlag = value;
		}
	}

	// Token: 0x060034C3 RID: 13507 RVA: 0x000D4590 File Offset: 0x000D2790
	public virtual void MountCar(string mountId, string mountColor)
	{
		if (!this.IsVisible())
		{
			return;
		}
		if (this.mLoadingMountFlag)
		{
			return;
		}
		if (this.mCurPlayerState == PLAYER_STATE.DRIVING)
		{
			if (!mountId.Equals(this.MountId))
			{
				this.ChangeMount(mountId, mountColor);
			}
			else if (!mountColor.Equals(this.MountColor))
			{
				this.MountColor = mountColor;
				this.MountRoot.ChangeColor(DataManager.GetColorDataById(mountColor));
			}
			return;
		}
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.StopDance();
		}
		else if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			this.StopSocialDance();
		}
		this.MountId = mountId;
		this.MountColor = mountColor;
		MountData mountDataById = DataManager.GetMountDataById(this.MountId);
		RideMountData rideMountData = new RideMountData();
		rideMountData.MountData = mountDataById;
		rideMountData.mColorData = DataManager.GetColorDataById(this.MountColor);
		rideMountData.Player = this;
		this.mLoadingMountFlag = true;
		this.mMountRoot = Singleton<ObjManager>.Instance.GetMountCar(rideMountData);
		this.mMountRoot.transform.parent = base.CacheTransform;
		this.mMountRoot.transform.localPosition = Vector3.zero;
		this.mMountRoot.transform.localRotation = Quaternion.identity;
		UnityVersionUtil.SetActiveRecursive(this.mMountRoot.gameObject, true);
		this.SetDrivingNameHeight(mountDataById);
		CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
		component.radius = 1.5f;
	}

	// Token: 0x060034C4 RID: 13508 RVA: 0x000D46F8 File Offset: 0x000D28F8
	public virtual void DisMountCar()
	{
		if (this.IsDrivingMount())
		{
			this.mCurPlayerState = PLAYER_STATE.NORMAL;
			Transform transform = this.mAnimationLogic.AnimaObj.transform;
			transform.transform.parent = base.CacheTransform;
			transform.transform.localPosition = Vector3.zero;
			transform.transform.localRotation = Quaternion.identity;
			if (base.IsMoving)
			{
				base.CurAnimationState = GameDefine.ANIMATIONSTATE.RUN;
			}
			else
			{
				base.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
			}
			this.mMountRoot.transform.parent = null;
			UnityVersionUtil.SetActiveRecursive(this.mMountRoot.gameObject, false);
			Singleton<ObjManager>.Instance.RecycleMountCar(this.mMountRoot);
			CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
			component.radius = 0.35f;
			this.SetNormalNameHeight();
			this.mLoadingMountFlag = false;
		}
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x000D47D0 File Offset: 0x000D29D0
	public void ChangeMount(string newMountId, string colorId)
	{
		this.DisMountCar();
		this.MountCar(newMountId, colorId);
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x000D47E0 File Offset: 0x000D29E0
	public virtual void StartDance(string danceId)
	{
		if (!this.IsVisible())
		{
			return;
		}
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			return;
		}
		if (this.IsDrivingMount())
		{
			this.DisMountCar();
		}
		base.SkillLogic.BreakCurSkill();
		if (base.IsMoving)
		{
			this.StopMove();
		}
		this.mCurPlayerState = PLAYER_STATE.DANCE;
		if (this.mDanceLogic == null)
		{
			this.mDanceLogic = base.gameObject.GetComponent<DanceLogic>();
			if (this.mDanceLogic == null)
			{
				this.mDanceLogic = base.gameObject.AddComponent<DanceLogic>();
			}
		}
		this.mDanceLogic.Reset(this);
		this.mDanceLogic.StartDance(danceId);
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x000D4898 File Offset: 0x000D2A98
	public virtual void StopDance()
	{
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.mDanceLogic.StopDance();
			this.mCurPlayerState = PLAYER_STATE.NORMAL;
			base.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
		}
	}

	// Token: 0x060034C8 RID: 13512 RVA: 0x000D48C0 File Offset: 0x000D2AC0
	public virtual void RemoveDance()
	{
		this.StopDance();
		Object.Destroy(this.mDanceLogic);
		this.mDanceLogic = null;
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x000D48DC File Offset: 0x000D2ADC
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
			this.AttributeData.HP = newHP;
			this.mReceiveBiggerHpTimeCount = Time.time;
		}
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x000D4930 File Offset: 0x000D2B30
	public override void ChangeHPVal(long newHP)
	{
		if (!base.IsDie)
		{
			this.AttributeData.HP = newHP;
			this.UpdateHeadInfo();
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
		}
		else if (newHP > 0L)
		{
			this.mServerNotDieNumCount++;
			if (this.mServerNotDieNumCount > GameDefine.NPC_SERVER_WAIT_RELIFE_NUM)
			{
				this.OnRelife(newHP, base.Position);
				this.mServerNotDieNumCount = 0;
			}
		}
	}

	// Token: 0x060034CB RID: 13515 RVA: 0x000D49B4 File Offset: 0x000D2BB4
	public override void OnRelife(long hp, Vector3 pos)
	{
		base.OnRelife(hp, pos);
		this.RefreshHeadInfo();
		base.IsLocalDrivingCar = false;
		base.CurPlayerCar = null;
	}

	// Token: 0x060034CC RID: 13516 RVA: 0x000D49E0 File Offset: 0x000D2BE0
	public void RecycleUnloadModelBundle()
	{
		for (int i = 0; i < this.LoadingModelData.Length; i++)
		{
			if (this.LoadingModelData[i] != null)
			{
				this.LoadingModelData[i].OnLoadFinished = null;
				this.LoadingModelData[i] = null;
				BundleManager.RemoveFromLoadModelList(this.LoadingModelDataId[i]);
				this.LoadingModelDataId[i] = -1L;
			}
		}
		if (this.PartObject[1] != null)
		{
			BundleManager.UnloadModel(this.PartObjId[1], -1L, false);
		}
		if (this.PartObject[0] != null && !string.IsNullOrEmpty(this.PartObjId[0]))
		{
			BundleManager.UnloadModel(this.PartObjId[0], -1L, false);
		}
		if (this.PartObject[3] != null)
		{
			BundleManager.UnloadModel(this.PartObjId[3], -1L, false);
		}
		if (this.PartObject[2] != null)
		{
			BundleManager.UnloadModel(this.PartObjId[2], -1L, false);
		}
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x000D4AE0 File Offset: 0x000D2CE0
	public override void OnStun(BuffInfoData buffInfoData)
	{
		if (this.mObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && this.IsDrivingMount())
		{
			this.DisMountCar();
		}
		base.OnStun(buffInfoData);
	}

	// Token: 0x060034CE RID: 13518 RVA: 0x000D4B14 File Offset: 0x000D2D14
	public override void OnKnockDown(BuffInfoData buffInfoData, ObjCharacter sender)
	{
		if (this.mObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && this.IsDrivingMount())
		{
			this.DisMountCar();
		}
		base.OnKnockDown(buffInfoData, sender);
	}

	// Token: 0x060034CF RID: 13519 RVA: 0x000D4B3C File Offset: 0x000D2D3C
	public override void ChangeWeaponAnimaCheck()
	{
		if (base.IsDie)
		{
			base.CurAnimationState = GameDefine.ANIMATIONSTATE.DIE;
			return;
		}
		if (this.IsDrivingMount())
		{
			base.ChangeDrivingState();
			return;
		}
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.StopDance();
			return;
		}
		if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			this.StopSocialDance();
			return;
		}
		if (base.IsMoving)
		{
			this.ChangeRunState();
			return;
		}
		base.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
	}

	// Token: 0x060034D0 RID: 13520 RVA: 0x000D4BB0 File Offset: 0x000D2DB0
	public void CheckBeforeOnCar()
	{
		if (this.mCurPlayerState == PLAYER_STATE.DANCE)
		{
			this.StopDance();
		}
		else if (this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE)
		{
			this.StopSocialDance();
		}
	}

	// Token: 0x04002273 RID: 8819
	public float PLAYER_NAME_DELTA_HIGHT = 0.25f;

	// Token: 0x04002274 RID: 8820
	protected ObjCharacter mSelectedTarget;

	// Token: 0x04002275 RID: 8821
	private AutoMoveLogic mAutoMoveLogic;

	// Token: 0x04002276 RID: 8822
	private GameObject mVisibleRootObj;

	// Token: 0x04002277 RID: 8823
	private PROFESSION_TYPE mProfession;

	// Token: 0x04002278 RID: 8824
	private string[] mPartObjId = new string[4];

	// Token: 0x04002279 RID: 8825
	private BundleManager.LoadModelData[] mLoadingModelData = new BundleManager.LoadModelData[4];

	// Token: 0x0400227A RID: 8826
	private long[] mLoadingModelDataId = new long[4];

	// Token: 0x0400227B RID: 8827
	private GameObject[] mPartObject = new GameObject[4];

	// Token: 0x0400227C RID: 8828
	private Material[] mPartMatList = new Material[4];

	// Token: 0x0400227D RID: 8829
	private List<GameObject>[] mPartEffectList = new List<GameObject>[4];

	// Token: 0x0400227E RID: 8830
	private long mGuildId = -1L;

	// Token: 0x0400227F RID: 8831
	private long mTeamId = -1L;

	// Token: 0x04002280 RID: 8832
	private static string NQS_run_naQiang_ActName = "run_naQiang";

	// Token: 0x04002281 RID: 8833
	private string mMountId = string.Empty;

	// Token: 0x04002282 RID: 8834
	private string mMountColor = string.Empty;

	// Token: 0x04002283 RID: 8835
	private ObjPlayerMountCar mMountRoot;

	// Token: 0x04002284 RID: 8836
	private bool mIsServerRidingMount;

	// Token: 0x04002285 RID: 8837
	private bool mLoadingMountFlag;

	// Token: 0x04002286 RID: 8838
	private DanceLogic mDanceLogic;

	// Token: 0x04002287 RID: 8839
	private int mServerNotDieNumCount;
}
