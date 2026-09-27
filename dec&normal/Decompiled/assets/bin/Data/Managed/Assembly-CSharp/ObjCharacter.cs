using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x0200081A RID: 2074
public class ObjCharacter : Obj
{
	// Token: 0x17000E3D RID: 3645
	// (get) Token: 0x06003201 RID: 12801 RVA: 0x000C36E8 File Offset: 0x000C18E8
	public virtual float ModelRadius
	{
		get
		{
			return base.CurrentCharacterModelData.ModelRadius;
		}
	}

	// Token: 0x17000E3E RID: 3646
	// (get) Token: 0x06003202 RID: 12802 RVA: 0x000C36F8 File Offset: 0x000C18F8
	public float mMoveSpeed
	{
		get
		{
			return this.AttributeData.CurSpeed;
		}
	}

	// Token: 0x17000E3F RID: 3647
	// (get) Token: 0x06003203 RID: 12803 RVA: 0x000C3708 File Offset: 0x000C1908
	// (set) Token: 0x06003204 RID: 12804 RVA: 0x000C3710 File Offset: 0x000C1910
	public bool IsMoving
	{
		get
		{
			return this.mIsMoving;
		}
		set
		{
			this.mIsMoving = value;
		}
	}

	// Token: 0x17000E40 RID: 3648
	// (get) Token: 0x06003205 RID: 12805 RVA: 0x000C371C File Offset: 0x000C191C
	// (set) Token: 0x06003206 RID: 12806 RVA: 0x000C3724 File Offset: 0x000C1924
	public bool IsDie
	{
		get
		{
			return this.mIsDie;
		}
		set
		{
			this.mIsDie = value;
		}
	}

	// Token: 0x17000E41 RID: 3649
	// (get) Token: 0x06003207 RID: 12807 RVA: 0x000C3730 File Offset: 0x000C1930
	// (set) Token: 0x06003208 RID: 12808 RVA: 0x000C3738 File Offset: 0x000C1938
	public bool IsStunFlag
	{
		get
		{
			return this.mIsStunFlag;
		}
		set
		{
			this.mIsStunFlag = value;
		}
	}

	// Token: 0x17000E42 RID: 3650
	// (get) Token: 0x06003209 RID: 12809 RVA: 0x000C3744 File Offset: 0x000C1944
	// (set) Token: 0x0600320A RID: 12810 RVA: 0x000C374C File Offset: 0x000C194C
	public bool IsKnockDownFlag
	{
		get
		{
			return this.mIsKnockDownFlag;
		}
		set
		{
			this.mIsKnockDownFlag = value;
		}
	}

	// Token: 0x17000E43 RID: 3651
	// (get) Token: 0x0600320B RID: 12811 RVA: 0x000C3758 File Offset: 0x000C1958
	// (set) Token: 0x0600320C RID: 12812 RVA: 0x000C3760 File Offset: 0x000C1960
	public bool IsSleepFlag
	{
		get
		{
			return this.mIsSleepFlag;
		}
		set
		{
			this.mIsSleepFlag = value;
		}
	}

	// Token: 0x17000E44 RID: 3652
	// (get) Token: 0x0600320D RID: 12813 RVA: 0x000C376C File Offset: 0x000C196C
	// (set) Token: 0x0600320E RID: 12814 RVA: 0x000C3774 File Offset: 0x000C1974
	public bool InvincibleFlag
	{
		get
		{
			return this.mInvincibleFlag;
		}
		set
		{
			this.mInvincibleFlag = value;
			if (value)
			{
				this.invincibleFlashSkinPercent = 0f;
				this.invincibleEffectSpeed = Mathf.Abs(this.invincibleEffectSpeed);
			}
			else
			{
				this.ResetSkinColor();
			}
		}
	}

	// Token: 0x17000E45 RID: 3653
	// (get) Token: 0x0600320F RID: 12815 RVA: 0x000C37B8 File Offset: 0x000C19B8
	// (set) Token: 0x06003210 RID: 12816 RVA: 0x000C37C0 File Offset: 0x000C19C0
	public bool IdleAttackFlag
	{
		get
		{
			return this.mIdleAttackFlag;
		}
		set
		{
			this.mIdleAttackFlag = value;
		}
	}

	// Token: 0x17000E46 RID: 3654
	// (get) Token: 0x06003211 RID: 12817 RVA: 0x000C37CC File Offset: 0x000C19CC
	protected Vector3 TargetPos
	{
		get
		{
			return this.mTargetPos;
		}
	}

	// Token: 0x17000E47 RID: 3655
	// (get) Token: 0x06003212 RID: 12818 RVA: 0x000C37D4 File Offset: 0x000C19D4
	// (set) Token: 0x06003213 RID: 12819 RVA: 0x000C37DC File Offset: 0x000C19DC
	public AnimationLogic AnimationLogic
	{
		get
		{
			return this.mAnimationLogic;
		}
		set
		{
			this.mAnimationLogic = value;
		}
	}

	// Token: 0x17000E48 RID: 3656
	// (get) Token: 0x06003214 RID: 12820 RVA: 0x000C37E8 File Offset: 0x000C19E8
	public SkillLogic SkillLogic
	{
		get
		{
			return this.mSkillLogic;
		}
	}

	// Token: 0x17000E49 RID: 3657
	// (get) Token: 0x06003215 RID: 12821 RVA: 0x000C37F0 File Offset: 0x000C19F0
	public SkillMotion SkillMotion
	{
		get
		{
			return this.mSkillMotion;
		}
	}

	// Token: 0x17000E4A RID: 3658
	// (get) Token: 0x06003216 RID: 12822 RVA: 0x000C37F8 File Offset: 0x000C19F8
	public EffectLogic EffectLogic
	{
		get
		{
			return this.mEffectLogic;
		}
	}

	// Token: 0x17000E4B RID: 3659
	// (get) Token: 0x06003217 RID: 12823 RVA: 0x000C3800 File Offset: 0x000C1A00
	public EffectMotion EffectMotion
	{
		get
		{
			return this.mEffectMotion;
		}
	}

	// Token: 0x17000E4C RID: 3660
	// (get) Token: 0x06003218 RID: 12824 RVA: 0x000C3808 File Offset: 0x000C1A08
	public BuffLogic BuffLogic
	{
		get
		{
			return this.mBuffLogic;
		}
	}

	// Token: 0x17000E4D RID: 3661
	// (get) Token: 0x06003219 RID: 12825 RVA: 0x000C3810 File Offset: 0x000C1A10
	// (set) Token: 0x0600321A RID: 12826 RVA: 0x000C3818 File Offset: 0x000C1A18
	public string CurUseSkillId
	{
		get
		{
			return this.mCurUseSkillId;
		}
		set
		{
			this.mCurUseSkillId = value;
		}
	}

	// Token: 0x17000E4E RID: 3662
	// (get) Token: 0x0600321B RID: 12827 RVA: 0x000C3824 File Offset: 0x000C1A24
	// (set) Token: 0x0600321C RID: 12828 RVA: 0x000C382C File Offset: 0x000C1A2C
	public HeadInfoLogic HeadInfoLogic
	{
		get
		{
			return this.mHeadInfoLogic;
		}
		set
		{
			this.mHeadInfoLogic = value;
		}
	}

	// Token: 0x17000E4F RID: 3663
	// (get) Token: 0x0600321D RID: 12829 RVA: 0x000C3838 File Offset: 0x000C1A38
	// (set) Token: 0x0600321E RID: 12830 RVA: 0x000C3840 File Offset: 0x000C1A40
	public GameObject SimpleShadow
	{
		get
		{
			return this.mSimpleShadow;
		}
		set
		{
			this.mSimpleShadow = value;
		}
	}

	// Token: 0x17000E50 RID: 3664
	// (get) Token: 0x0600321F RID: 12831 RVA: 0x000C384C File Offset: 0x000C1A4C
	// (set) Token: 0x06003220 RID: 12832 RVA: 0x000C3854 File Offset: 0x000C1A54
	public virtual CharacterAttributeData AttributeData
	{
		get
		{
			return this.mAttributeData;
		}
		set
		{
			this.mAttributeData = value;
		}
	}

	// Token: 0x17000E51 RID: 3665
	// (get) Token: 0x06003221 RID: 12833 RVA: 0x000C3860 File Offset: 0x000C1A60
	// (set) Token: 0x06003222 RID: 12834 RVA: 0x000C3868 File Offset: 0x000C1A68
	public NavMeshAgent NavMeshAgent
	{
		get
		{
			return this.mNavMeshAgent;
		}
		set
		{
			this.mNavMeshAgent = value;
		}
	}

	// Token: 0x06003223 RID: 12835 RVA: 0x000C3874 File Offset: 0x000C1A74
	public void DisactiveTargetArriveFinish()
	{
		this.targetArriveFinish = null;
	}

	// Token: 0x17000E52 RID: 3666
	// (get) Token: 0x06003224 RID: 12836 RVA: 0x000C3880 File Offset: 0x000C1A80
	public float ComboValidTime
	{
		get
		{
			return this.mComboTimeCount;
		}
	}

	// Token: 0x17000E53 RID: 3667
	// (get) Token: 0x06003225 RID: 12837 RVA: 0x000C3888 File Offset: 0x000C1A88
	// (set) Token: 0x06003226 RID: 12838 RVA: 0x000C3890 File Offset: 0x000C1A90
	public bool IsLocalDrivingCar
	{
		get
		{
			return this.mIsLocalDrivingCar;
		}
		set
		{
			this.mIsLocalDrivingCar = value;
		}
	}

	// Token: 0x17000E54 RID: 3668
	// (get) Token: 0x06003227 RID: 12839 RVA: 0x000C389C File Offset: 0x000C1A9C
	// (set) Token: 0x06003228 RID: 12840 RVA: 0x000C38A4 File Offset: 0x000C1AA4
	public ObjPlayerCar CurPlayerCar
	{
		get
		{
			return this.mCurPlayerCar;
		}
		set
		{
			this.mCurPlayerCar = value;
		}
	}

	// Token: 0x17000E55 RID: 3669
	// (get) Token: 0x06003229 RID: 12841 RVA: 0x000C38B0 File Offset: 0x000C1AB0
	// (set) Token: 0x0600322A RID: 12842 RVA: 0x000C38B8 File Offset: 0x000C1AB8
	public GameDefine.ANIMATIONSTATE CurAnimationState
	{
		get
		{
			return this.mCurAnimationState;
		}
		set
		{
			this.OnSwithAnimState(value);
		}
	}

	// Token: 0x17000E56 RID: 3670
	// (get) Token: 0x0600322B RID: 12843 RVA: 0x000C38C4 File Offset: 0x000C1AC4
	// (set) Token: 0x0600322C RID: 12844 RVA: 0x000C38CC File Offset: 0x000C1ACC
	public virtual List<CharacterSkillData> CharacterSkillData
	{
		get
		{
			return this.mCharacterSkillData;
		}
		set
		{
			this.mCharacterSkillData = value;
		}
	}

	// Token: 0x17000E57 RID: 3671
	// (get) Token: 0x0600322D RID: 12845 RVA: 0x000C38D8 File Offset: 0x000C1AD8
	// (set) Token: 0x0600322E RID: 12846 RVA: 0x000C38E0 File Offset: 0x000C1AE0
	public float HoldTimeCount
	{
		get
		{
			return this.mHoldTimeCount;
		}
		set
		{
			this.mHoldTimeCount = value;
		}
	}

	// Token: 0x17000E58 RID: 3672
	// (get) Token: 0x0600322F RID: 12847 RVA: 0x000C38EC File Offset: 0x000C1AEC
	// (set) Token: 0x06003230 RID: 12848 RVA: 0x000C38F4 File Offset: 0x000C1AF4
	public PLAYER_STATE CurPlayerState
	{
		get
		{
			return this.mCurPlayerState;
		}
		set
		{
			this.mCurPlayerState = value;
		}
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x000C3900 File Offset: 0x000C1B00
	public void UpdatePlayerMeshId(string weaponId, string headId, string bodyId, string legId)
	{
		this.SetTargetPartObjId(MODEL_TYPE.WEAPON, weaponId);
		this.SetTargetPartObjId(MODEL_TYPE.HEAD, headId);
		this.SetTargetPartObjId(MODEL_TYPE.BODY, bodyId);
		this.SetTargetPartObjId(MODEL_TYPE.LEG, legId);
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x000C3930 File Offset: 0x000C1B30
	public virtual void SetTargetPartObjId(MODEL_TYPE type, string id)
	{
		this.TargetPartObjId[(int)type] = id;
	}

	// Token: 0x17000E59 RID: 3673
	// (get) Token: 0x06003233 RID: 12851 RVA: 0x000C393C File Offset: 0x000C1B3C
	// (set) Token: 0x06003234 RID: 12852 RVA: 0x000C396C File Offset: 0x000C1B6C
	public string WeaponTypeName
	{
		get
		{
			if (string.IsNullOrEmpty(this.mWeaponTypeName))
			{
				this.mWeaponTypeName = this.GetWeaponName();
			}
			return this.mWeaponTypeName;
		}
		set
		{
			this.mWeaponTypeName = value;
		}
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x000C3978 File Offset: 0x000C1B78
	public string GetWeaponName()
	{
		if (!string.IsNullOrEmpty(this.WeaponItemID))
		{
			EquipData equipDataById = DataManager.GetEquipDataById(this.WeaponItemID);
			if (equipDataById != null && GameDefine.WeaponTypeName.ContainsKey(equipDataById.WeaponType))
			{
				return GameDefine.WeaponTypeName[equipDataById.WeaponType];
			}
		}
		return GameDefine.GetWeaponName(this.WeaponModelID);
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x000C39D8 File Offset: 0x000C1BD8
	public int GetWeaponType()
	{
		if (GameDefine.NameWeaponType.ContainsKey(this.WeaponTypeName))
		{
			return GameDefine.NameWeaponType[this.WeaponTypeName];
		}
		return 0;
	}

	// Token: 0x06003237 RID: 12855 RVA: 0x000C3A0C File Offset: 0x000C1C0C
	public void UpdateWeaponModeID(string weaponmodeid)
	{
		if (string.IsNullOrEmpty(weaponmodeid))
		{
			return;
		}
		if (string.IsNullOrEmpty(this.WeaponModelID) || !this.WeaponModelID.Equals(weaponmodeid))
		{
			this.WeaponModelID = weaponmodeid;
			this.WeaponTypeName = this.GetWeaponName();
			if (base.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && this.mAnimationLogic != null)
			{
				this.mAnimationLogic.PlayAnimationDataList.Clear();
			}
		}
	}

	// Token: 0x06003238 RID: 12856 RVA: 0x000C3A88 File Offset: 0x000C1C88
	public void UpdateWeaponItemID(string weaponitemid)
	{
		if (string.IsNullOrEmpty(weaponitemid))
		{
			return;
		}
		if (string.IsNullOrEmpty(this.WeaponItemID) || !this.WeaponItemID.Equals(weaponitemid))
		{
			this.WeaponItemID = weaponitemid;
			this.WeaponTypeName = this.GetWeaponName();
			if (base.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && this.mAnimationLogic != null)
			{
				this.mAnimationLogic.PlayAnimationDataList.Clear();
			}
		}
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x000C3B04 File Offset: 0x000C1D04
	public string GetActionName(string actname)
	{
		if (base.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && base.ObjType != GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && base.ObjType != GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER && base.ObjType != GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
		{
			return string.Format("{0}_{1}", base.IndexName, actname);
		}
		if (!string.IsNullOrEmpty(this.WeaponTypeName))
		{
			return string.Format("{0}_{1}_{2}", base.IndexName, this.WeaponTypeName, actname);
		}
		return null;
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x000C3B7C File Offset: 0x000C1D7C
	public string GetActionNameNoName(string actname)
	{
		if (base.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && base.ObjType != GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && base.ObjType != GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER && base.ObjType != GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
		{
			return string.Format("{0}", actname);
		}
		if (!string.IsNullOrEmpty(this.WeaponTypeName))
		{
			return string.Format("{0}_{1}_{2}", base.IndexName, this.WeaponTypeName, actname);
		}
		return null;
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x000C3BF0 File Offset: 0x000C1DF0
	public override void Init()
	{
		base.Init();
		if (this.mAnimationLogic == null)
		{
			this.mAnimationLogic = base.gameObject.AddComponent<AnimationLogic>();
		}
		if (this.mSkillLogic == null)
		{
			this.mSkillLogic = new SkillLogic();
		}
		if (this.mEffectLogic == null)
		{
			this.mEffectLogic = base.gameObject.AddComponent<EffectLogic>();
		}
		if (this.mSkillMotion == null)
		{
			this.mSkillMotion = base.gameObject.AddComponent<SkillMotion>();
		}
		if (this.mEffectMotion == null)
		{
			this.mEffectMotion = base.gameObject.AddComponent<EffectMotion>();
		}
		if (this.mBuffLogic == null)
		{
			this.mBuffLogic = base.gameObject.AddComponent<BuffLogic>();
		}
		if (this.AttributeData == null)
		{
			this.AttributeData = new CharacterAttributeData();
		}
		this.RegisterEvent();
		this.mAnimationLogic.Init(this);
		this.mCurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
		this.mSkillMotion.Init(this);
		this.mEffectLogic.Init(this);
		this.mEffectMotion.Init(this);
		this.mBuffLogic.Init(this);
		this.mTransform = base.transform;
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x000C3D30 File Offset: 0x000C1F30
	public void InitNavMeshAgent()
	{
		if (this.mNavMeshAgent == null)
		{
			this.mNavMeshAgent = base.gameObject.AddComponent<NavMeshAgent>();
		}
		this.ResetNavMeshAgent();
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x000C3D68 File Offset: 0x000C1F68
	public virtual void UpdatePlayerSpeed()
	{
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x000C3D6C File Offset: 0x000C1F6C
	public virtual void UpdateMountSpeed()
	{
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x000C3D70 File Offset: 0x000C1F70
	public virtual void Reset()
	{
		this.IdleAttackFlag = false;
		this.IsDie = false;
		this.IsStunFlag = false;
		this.IsKnockDownFlag = false;
		this.IsSleepFlag = false;
		this.InvincibleFlag = false;
		this.IsShowInvincibleEffect = true;
		this.BuffLogic.ClearBuff();
		this.AnimationLogic.ResetAnimationFlag();
		this.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.Stop();
			this.mNavMeshAgent.ResetPath();
			this.mNavMeshAgent.enabled = false;
		}
		this.mSkillLogic.ResetSkillLogic();
		if (this.mObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			this.InitSimpleShadow();
		}
		else if (!GameSettingData.IsShowPlayerShadow[GameSettingData.GetPhoneClass()])
		{
			this.InitSimpleShadow();
		}
		this.targetArriveFinish = null;
		this.mIsLocalDrivingCar = false;
		this.mCurPlayerCar = null;
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x000C3E5C File Offset: 0x000C205C
	public void InitSimpleShadow()
	{
		if (this.mSimpleShadow == null)
		{
			ResourcesManager.LoadSimpleShadowPrefab(UIInfo.SimpleShadowUI, "SimpleShadow", new ResourcesManager.LoadSimpleShadowDelegate(this.LoadSimpleShadow));
		}
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x000C3E98 File Offset: 0x000C2098
	private void LoadSimpleShadow(GameObject obj)
	{
		if (obj != null)
		{
			SimpleShadowFollow simpleShadowFollow = obj.GetComponent<SimpleShadowFollow>();
			if (simpleShadowFollow == null)
			{
				simpleShadowFollow = obj.AddComponent<SimpleShadowFollow>();
			}
			simpleShadowFollow.enabled = true;
			simpleShadowFollow.BindObj = base.gameObject;
			simpleShadowFollow.DeltaHeight = 0f;
			this.mSimpleShadow = obj;
			UnityVersionUtil.SetActiveRecursive(this.mSimpleShadow.gameObject, true);
		}
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x000C3F04 File Offset: 0x000C2104
	public void ResetNavMeshAgent()
	{
		if (this.mNavMeshAgent != null)
		{
			this.mNavMeshAgent.enabled = true;
			this.mNavMeshAgent.radius = this.ModelRadius;
			this.mNavMeshAgent.stoppingDistance = 0.8f;
			this.mNavMeshAgent.speed = this.mMoveSpeed;
			this.mNavMeshAgent.acceleration = 10000f;
			this.mNavMeshAgent.angularSpeed = 30000f;
			this.mNavMeshAgent.walkableMask = 1;
			this.mNavMeshAgent.autoBraking = false;
			this.mNavMeshAgent.obstacleAvoidanceType = 0;
			if (this.mObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
			{
				this.mNavMeshAgent.walkableMask += 65536;
			}
			this.UpdateMountSpeed();
		}
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x000C3FCC File Offset: 0x000C21CC
	public void InitInfo(CharacterModelData characterModelData)
	{
		if (characterModelData != null)
		{
			this.mCharacterModelData = characterModelData;
		}
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x000C3FDC File Offset: 0x000C21DC
	public void UpdateMove()
	{
		if (this.IsMoving)
		{
			float num = VectorXZ.Distance(new VectorXZ(this.mTargetPos.x, this.mTargetPos.z), new VectorXZ(base.Position.x, base.Position.z));
			if (num - this.mStopRange <= 0f)
			{
				this.StopMove();
				return;
			}
			if (num - this.mMoveSpeed * Time.deltaTime <= 0f)
			{
				base.Position = this.mTargetPos;
				this.StopMove();
				return;
			}
		}
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x000C407C File Offset: 0x000C227C
	public void UpdateSkillCD()
	{
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			if (this.CharacterSkillData[i].CDTimeCount > 0f)
			{
				this.CharacterSkillData[i].CDTimeCount -= Time.deltaTime;
				if (this.CharacterSkillData[i].CDTimeCount <= 0f)
				{
					this.OnSkillEnable(this.CharacterSkillData[i].ID);
				}
			}
		}
	}

	// Token: 0x06003246 RID: 12870 RVA: 0x000C4110 File Offset: 0x000C2310
	public void UpdateHoldTime()
	{
		if (this.mHoldTimeCount > 0f)
		{
			this.mHoldTimeCount -= Time.deltaTime;
		}
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x000C4140 File Offset: 0x000C2340
	public void UpdateComponent()
	{
		this.mAnimationLogic.AnimationLogicUpdate();
		this.mEffectLogic.UpdatePlayEffInfoData();
		this.mEffectMotion.UpdateEffectMotion();
		this.mSkillMotion.UpdateSkillMotion();
		this.mBuffLogic.UpdateBuffLogic();
		this.UpdateIdleAttackLastTime();
		this.UpdateInvincibleEffect();
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x000C4190 File Offset: 0x000C2390
	public void UpdateInvincibleEffect()
	{
		if (this.InvincibleFlag && this.IsShowInvincibleEffect)
		{
			this.invincibleFlashSkinPercent += this.invincibleEffectSpeed;
			this.InvincibleFlashSkin();
			if (this.invincibleFlashSkinPercent >= 1f || this.invincibleFlashSkinPercent <= 0f)
			{
				this.invincibleEffectSpeed = -this.invincibleEffectSpeed;
			}
		}
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x000C41FC File Offset: 0x000C23FC
	public void UpdateIdleAttackLastTime()
	{
		if (this.IdleAttackFlag && !this.IsStun() && !this.mSkillLogic.IsUsingSkill)
		{
			this.idleAttackTimeCount -= Time.deltaTime;
			if (this.idleAttackTimeCount <= 0f)
			{
				this.DisactiveIdleAttack();
			}
		}
	}

	// Token: 0x0600324A RID: 12874 RVA: 0x000C4258 File Offset: 0x000C2458
	public void AddPlayAnimationData(string actionName, float delayTime, string effInfoID)
	{
		if (this.mAnimationLogic != null)
		{
			this.mAnimationLogic.AddPlayAnimationData(actionName, delayTime, effInfoID);
		}
	}

	// Token: 0x0600324B RID: 12875 RVA: 0x000C427C File Offset: 0x000C247C
	public void AddPlayEffInfoData(string actionName, string effInfoID, float delayTime, Vector3 senderPos, Transform targetTransform = null)
	{
		if (this.mEffectLogic != null)
		{
			this.mEffectLogic.AddPlayEffInfoData(actionName, effInfoID, delayTime, senderPos, targetTransform);
		}
	}

	// Token: 0x0600324C RID: 12876 RVA: 0x000C42A4 File Offset: 0x000C24A4
	public void AddPlayeBufEffInfoData(string fxeffectInfoId, float delayTime, float duration, Vector3 senderPosition)
	{
		if (this.mEffectLogic != null)
		{
			this.mEffectLogic.AddPlayeBufEffInfoData(fxeffectInfoId, delayTime, duration, senderPosition);
		}
	}

	// Token: 0x0600324D RID: 12877 RVA: 0x000C42C8 File Offset: 0x000C24C8
	public void AddPlayerEffMotionData(string effInfoId, float delayTime, Vector3 senderPos)
	{
		if (this.mEffectMotion != null)
		{
			this.mEffectMotion.AddPlayEffInfoMotionData(effInfoId, delayTime, senderPos);
		}
	}

	// Token: 0x0600324E RID: 12878 RVA: 0x000C42EC File Offset: 0x000C24EC
	public void PlayYinChangeEffInfo(string effInfoId, float duration, Vector3 senderPos)
	{
		if (this.mEffectLogic != null)
		{
			this.mEffectLogic.PlayYinChangeEffInfo(effInfoId, duration, senderPos);
		}
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x000C4310 File Offset: 0x000C2510
	public void AddBuffInfoData(string buffID, float delayTime, float duration, ObjCharacter sender)
	{
		if (this.mBuffLogic != null)
		{
			this.mBuffLogic.AddBuffInfoData(buffID, delayTime, duration, sender);
		}
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x000C4334 File Offset: 0x000C2534
	public void OnSwithAnimState(GameDefine.ANIMATIONSTATE newState)
	{
		if (this.mCurPlayerState != PLAYER_STATE.NORMAL)
		{
			return;
		}
		if (newState == GameDefine.ANIMATIONSTATE.IDLE && this.mIdleAttackFlag)
		{
			newState = GameDefine.ANIMATIONSTATE.IDLE_ATTACK;
		}
		this.mCurAnimationState = newState;
		switch (this.CurAnimationState)
		{
		case GameDefine.ANIMATIONSTATE.IDLE:
			this.ChangeIdleState();
			break;
		case GameDefine.ANIMATIONSTATE.RUN:
			this.ChangeRunState();
			break;
		case GameDefine.ANIMATIONSTATE.WALK:
			this.ChangeWalkState();
			break;
		case GameDefine.ANIMATIONSTATE.DIE:
			this.ChangeDieState();
			break;
		case GameDefine.ANIMATIONSTATE.IDLE_ATTACK:
			this.ChangeIdleAttackState();
			break;
		case GameDefine.ANIMATIONSTATE.DRIVING:
			this.ChangeDrivingState();
			break;
		}
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x000C43D4 File Offset: 0x000C25D4
	protected virtual void ChangeIdleState()
	{
		this.mAnimationLogic.PlayAnimation(0, null);
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x000C43E4 File Offset: 0x000C25E4
	protected virtual void ChangeRunState()
	{
		this.mAnimationLogic.PlayAnimation(1, null);
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x000C43F4 File Offset: 0x000C25F4
	private void ChangeWalkState()
	{
		this.mAnimationLogic.PlayAnimation(2, null);
	}

	// Token: 0x06003254 RID: 12884 RVA: 0x000C4404 File Offset: 0x000C2604
	private void ChangeDieState()
	{
		this.mAnimationLogic.ForcePlayAnimation(this.mAnimationLogic.animationName[3], null, -1f, 0f);
	}

	// Token: 0x06003255 RID: 12885 RVA: 0x000C442C File Offset: 0x000C262C
	private void ChangeIdleAttackState()
	{
		this.mAnimationLogic.PlayAnimation(4, null);
	}

	// Token: 0x06003256 RID: 12886 RVA: 0x000C443C File Offset: 0x000C263C
	public void ChangeDrivingState()
	{
		this.mAnimationLogic.PlayAnimation(5, null);
	}

	// Token: 0x06003257 RID: 12887 RVA: 0x000C444C File Offset: 0x000C264C
	public virtual void ChangeWeaponAnimaCheck()
	{
		if (this.IsDie)
		{
			return;
		}
		if (this.IsMoving)
		{
			this.ChangeRunState();
			return;
		}
		if (this.IdleAttackFlag)
		{
			this.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
			return;
		}
		this.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
	}

	// Token: 0x06003258 RID: 12888 RVA: 0x000C4494 File Offset: 0x000C2694
	private void StartMove()
	{
		if (this.mNavMeshAgent != null)
		{
			this.mNavMeshAgent.speed = this.AttributeData.CurSpeed;
			this.UpdateMountSpeed();
		}
		if (this.CurAnimationState != GameDefine.ANIMATIONSTATE.RUN)
		{
			this.CurAnimationState = GameDefine.ANIMATIONSTATE.RUN;
		}
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x000C44E4 File Offset: 0x000C26E4
	public virtual void StopMove()
	{
		if (this.IsMoving)
		{
			this.IsMoving = false;
			this.currentTime = this.checkTime;
			this.lastPos = Vector3.forward * float.MaxValue;
			this.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
			if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
			{
				this.mNavMeshAgent.Stop();
			}
			if (this.targetArriveFinish != null)
			{
				this.tempTargetArriveFinish = this.targetArriveFinish;
				this.targetArriveFinish = null;
				this.tempTargetArriveFinish(this);
			}
		}
		else if (this.targetArriveFinish != null)
		{
			this.tempTargetArriveFinish = this.targetArriveFinish;
			this.targetArriveFinish = null;
			this.tempTargetArriveFinish(this);
		}
	}

	// Token: 0x0600325A RID: 12890 RVA: 0x000C45B0 File Offset: 0x000C27B0
	private bool CheckArrive(Vector3 target)
	{
		float num = Vector3.SqrMagnitude(target - base.Position);
		return num <= this.mStopRange * this.mStopRange;
	}

	// Token: 0x0600325B RID: 12891 RVA: 0x000C45E8 File Offset: 0x000C27E8
	public bool BeforeMoveCheck()
	{
		return this.IsDie || this.IsStun() || this.mSkillLogic.IsUsingSkill || this.mEffectMotion.NeedMoveFlag || this.mSkillMotion.NeedMoveFlag || this.mCurPlayerState == PLAYER_STATE.SOCIAL_DANCE || this.IsLocalDrivingCar;
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x000C4664 File Offset: 0x000C2864
	public bool BeforeSkillCheck()
	{
		return this.IsDie || this.IsStun() || !this.CheckHoldTime() || this.mEffectMotion.NeedMoveFlag || this.mSkillMotion.NeedMoveFlag;
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x000C46C0 File Offset: 0x000C28C0
	public void UpdateStopDistance(float distance)
	{
		this.mStopRange = distance;
		if (this.mNavMeshAgent != null)
		{
			this.mNavMeshAgent.stoppingDistance = distance;
		}
	}

	// Token: 0x0600325E RID: 12894 RVA: 0x000C46F4 File Offset: 0x000C28F4
	public void MoveTo(float posX, float posY, float posZ, float stopRange = 1f, ObjCharacter.TargetArriveFinsh arriveFinsh = null)
	{
		this.MoveTo(new Vector3(posX, posY, posZ), stopRange, arriveFinsh);
	}

	// Token: 0x0600325F RID: 12895 RVA: 0x000C4708 File Offset: 0x000C2908
	public void MoveTo(float posX, float posZ, float stopRange = 1f, ObjCharacter.TargetArriveFinsh arriveFinsh = null)
	{
		this.MoveTo(new Vector3(posX, SceneManager.GetHitHeight(new Vector3(posX, 0f, posZ)), posZ), stopRange, arriveFinsh);
	}

	// Token: 0x06003260 RID: 12896 RVA: 0x000C472C File Offset: 0x000C292C
	public virtual void MoveTo(Vector3 pos, float stopRange = 1f, ObjCharacter.TargetArriveFinsh arriveFinsh = null)
	{
		if (this.BeforeMoveCheck())
		{
			return;
		}
		this.targetArriveFinish = arriveFinsh;
		this.mStopRange = stopRange;
		this.mTargetPos = pos;
		if (this.CheckArrive(pos))
		{
			this.StopMove();
			return;
		}
		this.StartMove();
		this.IsMoving = true;
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.stoppingDistance = stopRange;
			this.mNavMeshAgent.SetDestination(this.mTargetPos);
		}
		else
		{
			Debug.Log("mNavMeshAgent.enabled == false");
		}
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x000C47C8 File Offset: 0x000C29C8
	private void FaceTo(Vector3 pos)
	{
		Vector3 vector = pos - base.Position;
		vector.y = 0f;
		if (vector != Vector3.zero)
		{
			base.CacheTransform.rotation = Quaternion.Slerp(base.CacheTransform.rotation, Quaternion.LookRotation(vector), 10f * Time.deltaTime);
		}
	}

	// Token: 0x06003262 RID: 12898 RVA: 0x000C482C File Offset: 0x000C2A2C
	public void WalkMoveTo(Vector3 pos, float stopRange = 1f, ObjCharacter.TargetArriveFinsh arriveFinsh = null)
	{
		if (this.BeforeMoveCheck())
		{
			return;
		}
		this.targetArriveFinish = arriveFinsh;
		this.mStopRange = stopRange;
		this.mTargetPos = pos;
		if (this.CheckArrive(pos))
		{
			this.StopMove();
			return;
		}
		this.StartWalk();
		this.IsMoving = true;
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.stoppingDistance = stopRange;
			this.mNavMeshAgent.SetDestination(this.mTargetPos);
		}
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x000C48BC File Offset: 0x000C2ABC
	private void StartWalk()
	{
		if (this.mNavMeshAgent != null)
		{
			this.mNavMeshAgent.speed = this.AttributeData.WalkSpeed;
			this.UpdateMountSpeed();
		}
		this.CurAnimationState = GameDefine.ANIMATIONSTATE.WALK;
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x000C4900 File Offset: 0x000C2B00
	public void FaceToPub(Vector3 pos)
	{
		Vector3 vector = pos - base.Position;
		vector.y = 0f;
		if (vector != Vector3.zero)
		{
			base.CacheTransform.rotation = Quaternion.LookRotation(vector);
		}
	}

	// Token: 0x06003265 RID: 12901 RVA: 0x000C4948 File Offset: 0x000C2B48
	public CharacterSkillData GetCharacterSkillDataByID(string skillID)
	{
		if (this.CharacterSkillData.Count == 0)
		{
			return null;
		}
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			if (this.CharacterSkillData[i].ID == skillID)
			{
				return this.CharacterSkillData[i];
			}
		}
		return null;
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x000C49B0 File Offset: 0x000C2BB0
	public virtual void UpdateSkillList(Dictionary<string, skill_info> skills)
	{
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x000C49B4 File Offset: 0x000C2BB4
	public bool CheckHoldTime()
	{
		return this.mHoldTimeCount <= 0f;
	}

	// Token: 0x06003268 RID: 12904 RVA: 0x000C49CC File Offset: 0x000C2BCC
	public bool CheckSkillCD(SkillData skillData)
	{
		CharacterSkillData characterSkillDataByID = this.GetCharacterSkillDataByID(skillData.ID);
		return characterSkillDataByID == null || characterSkillDataByID.CDTimeCount <= 0f;
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x000C4A04 File Offset: 0x000C2C04
	public bool CheckSkillDistance(SkillData skillData, ObjCharacter target)
	{
		if (skillData.TraceDistance <= 0)
		{
			return true;
		}
		float traceDistanceMeter = skillData.TraceDistanceMeter;
		float num = VectorXZ.Distance(base.Position, target.Position);
		float num2 = num - traceDistanceMeter - this.ModelRadius - target.ModelRadius;
		return num2 <= 0f;
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x000C4A64 File Offset: 0x000C2C64
	public float GetPathDistance(Vector3 pos)
	{
		NavMeshPath navMeshPath = new NavMeshPath();
		if (!NavMesh.CalculatePath(base.Position, pos, -1, navMeshPath))
		{
			return 9999f;
		}
		float num = 0f;
		for (int i = 1; i < navMeshPath.corners.Length; i++)
		{
			num += VectorXZ.Distance(navMeshPath.corners[i], navMeshPath.corners[i - 1]);
		}
		return num;
	}

	// Token: 0x0600326B RID: 12907 RVA: 0x000C4AE8 File Offset: 0x000C2CE8
	public virtual void OnDie()
	{
		if (this.IsDie)
		{
			return;
		}
		this.IsDie = true;
		this.DisableNavMeshAgent();
		if (this.mSkillLogic.IsUsingSkill)
		{
			this.mSkillLogic.BreakCurSkill();
		}
		if (this.IsMoving)
		{
			this.StopMove();
		}
		this.CurAnimationState = GameDefine.ANIMATIONSTATE.DIE;
		this.DisactiveHeadInfo();
		this.BuffLogic.ClearBuff();
	}

	// Token: 0x0600326C RID: 12908 RVA: 0x000C4B54 File Offset: 0x000C2D54
	public virtual void OnRelife(long hp, Vector3 pos)
	{
		this.Reset();
		if (this.mAnimationLogic != null && this.mAnimationLogic.AnimaObj != null)
		{
			this.mAnimationLogic.AnimaObj.Stop();
			this.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
		}
		this.AttributeData.HP = hp;
		this.EnableNavMeshAgent();
		this.ActiveHeadInfo();
		this.UpdateHeadInfo();
		base.Position = pos;
	}

	// Token: 0x0600326D RID: 12909 RVA: 0x000C4BCC File Offset: 0x000C2DCC
	public bool IsStun()
	{
		return this.IsStunFlag || this.IsSleepFlag || this.IsKnockDownFlag;
	}

	// Token: 0x0600326E RID: 12910 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
	public virtual void OnStun(BuffInfoData buffInfoData)
	{
		this.mIsStunFlag = true;
		if (this.IsMoving)
		{
			this.StopMove();
		}
		if (this.mSkillLogic.IsUsingSkill)
		{
			this.mSkillLogic.BreakCurSkill();
		}
	}

	// Token: 0x0600326F RID: 12911 RVA: 0x000C4C28 File Offset: 0x000C2E28
	public void OnStunDone(BuffInfoData buffInfoData)
	{
		this.mIsStunFlag = false;
		if (!this.mNavMeshAgent.enabled)
		{
			this.EnableNavMeshAgent();
		}
	}

	// Token: 0x06003270 RID: 12912 RVA: 0x000C4C48 File Offset: 0x000C2E48
	public virtual void OnKnockDown(BuffInfoData buffInfoData, ObjCharacter sender)
	{
		this.mIsKnockDownFlag = true;
		if (this.IsMoving)
		{
			this.StopMove();
		}
		this.FaceToPub(sender.Position);
		if (this.mSkillLogic.IsUsingSkill)
		{
			this.mSkillLogic.BreakCurSkill();
		}
	}

	// Token: 0x06003271 RID: 12913 RVA: 0x000C4C94 File Offset: 0x000C2E94
	public void OnKnockDownDone(BuffInfoData buffInfoData)
	{
		this.mIsKnockDownFlag = false;
		if (!this.mNavMeshAgent.enabled)
		{
			this.EnableNavMeshAgent();
		}
	}

	// Token: 0x06003272 RID: 12914 RVA: 0x000C4CB4 File Offset: 0x000C2EB4
	public void OnSleep(BuffInfoData buffInfoData)
	{
		this.mIsSleepFlag = true;
		if (this.IsMoving)
		{
			this.StopMove();
		}
	}

	// Token: 0x06003273 RID: 12915 RVA: 0x000C4CD0 File Offset: 0x000C2ED0
	public void OnSleepDone(BuffInfoData buffInfoData)
	{
		this.mIsSleepFlag = false;
	}

	// Token: 0x06003274 RID: 12916 RVA: 0x000C4CDC File Offset: 0x000C2EDC
	public void OnChangeAttr(BuffInfoData buffInfoData)
	{
		switch (buffInfoData.AttrID)
		{
		case 1001:
			this.AttributeData.CurATK = this.CalAttrValue(this.AttributeData.CurATK, (float)this.AttributeData.ATK, buffInfoData);
			break;
		case 1003:
			this.AttributeData.CurDEF = this.CalAttrValue(this.AttributeData.CurDEF, (float)this.AttributeData.DEF, buffInfoData);
			break;
		case 1004:
			this.AttributeData.CurHIT = this.CalAttrValue(this.AttributeData.CurHIT, (float)this.AttributeData.HIT, buffInfoData);
			break;
		case 1005:
			this.AttributeData.CurDGE = this.CalAttrValue(this.AttributeData.CurDGE, (float)this.AttributeData.DGE, buffInfoData);
			break;
		case 1006:
			this.AttributeData.CurCRI = this.CalAttrValue(this.AttributeData.CurCRI, (float)this.AttributeData.CRI, buffInfoData);
			break;
		}
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x000C4E04 File Offset: 0x000C3004
	public float CalAttrValue(float curNum, float defaultNum, BuffInfoData buffInfoData)
	{
		switch (buffInfoData.AttrType)
		{
		case 0:
			return curNum + (float)buffInfoData.AttrValue;
		case 1:
			return curNum + defaultNum * (float)buffInfoData.AttrValue;
		case 2:
			return curNum;
		default:
			return curNum;
		}
	}

	// Token: 0x06003276 RID: 12918 RVA: 0x000C4E48 File Offset: 0x000C3048
	public void OnChangeAttrDone(BuffInfoData buffInfoData)
	{
		switch (buffInfoData.AttrID)
		{
		case 1001:
			this.AttributeData.CurATK = this.CalRemoveAttrValue(this.AttributeData.CurATK, (float)this.AttributeData.ATK, buffInfoData);
			break;
		case 1003:
			this.AttributeData.CurDEF = this.CalRemoveAttrValue(this.AttributeData.CurDEF, (float)this.AttributeData.DEF, buffInfoData);
			break;
		case 1004:
			this.AttributeData.CurHIT = this.CalRemoveAttrValue(this.AttributeData.CurHIT, (float)this.AttributeData.HIT, buffInfoData);
			break;
		case 1005:
			this.AttributeData.CurDGE = this.CalRemoveAttrValue(this.AttributeData.CurDGE, (float)this.AttributeData.DGE, buffInfoData);
			break;
		case 1006:
			this.AttributeData.CurCRI = this.CalRemoveAttrValue(this.AttributeData.CurCRI, (float)this.AttributeData.CRI, buffInfoData);
			break;
		}
	}

	// Token: 0x06003277 RID: 12919 RVA: 0x000C4F70 File Offset: 0x000C3170
	public float CalRemoveAttrValue(float curNum, float defaultNum, BuffInfoData buffInfoData)
	{
		switch (buffInfoData.AttrType)
		{
		case 0:
			return curNum - (float)buffInfoData.AttrValue;
		case 1:
			return curNum - defaultNum * (float)buffInfoData.AttrValue;
		case 2:
			return curNum;
		default:
			return curNum;
		}
	}

	// Token: 0x06003278 RID: 12920 RVA: 0x000C4FB4 File Offset: 0x000C31B4
	public void FlashSkin()
	{
		if (this.mObjType == GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR)
		{
			return;
		}
		if (this.meshMats == null)
		{
			SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
			if (componentsInChildren.Length == 0)
			{
				return;
			}
			this.meshMats = new Material[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.meshMats[i] = componentsInChildren[i].material;
			}
		}
		if (this.meshMats == null)
		{
			return;
		}
		vp_Timer.In(0.1f, delegate()
		{
			for (int j = 0; j < this.meshMats.Length; j++)
			{
				this.meshMats[j].SetColor("_RimColor", Color.black);
				this.meshMats[j].SetFloat("_RimPower", 0.05f);
				ShortcutExtensions.DOColor(this.meshMats[j], new Color(0.5f, 0.5f, 0.5f, 1f), "_RimColor", 0.1f);
				TweenSettingsExtensions.OnComplete<Tweener>(ShortcutExtensions.DOFloat(this.meshMats[j], 0f, "_RimPower", 0.1f), delegate()
				{
					for (int k = 0; k < this.meshMats.Length; k++)
					{
						ShortcutExtensions.DOFloat(this.meshMats[k], 1f, "_RimPower", 0.05f);
						ShortcutExtensions.DOColor(this.meshMats[k], Color.black, "_RimColor", 0.05f);
					}
				});
			}
		}, null);
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x000C5044 File Offset: 0x000C3244
	public void InvincibleFlashSkin()
	{
		if (this.meshMats == null)
		{
			SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
			if (componentsInChildren.Length == 0)
			{
				return;
			}
			this.meshMats = new Material[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.meshMats[i] = componentsInChildren[i].material;
			}
		}
		if (this.meshMats == null)
		{
			return;
		}
		Color color = Color.Lerp(Color.black, Color.gray, this.invincibleFlashSkinPercent);
		float num = Mathf.Lerp(1f, 0f, this.invincibleFlashSkinPercent);
		for (int j = 0; j < this.meshMats.Length; j++)
		{
			this.meshMats[j].SetColor("_RimColor", color);
			this.meshMats[j].SetFloat("_RimPower", num);
		}
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x000C5124 File Offset: 0x000C3324
	public void ResetSkinColor()
	{
		if (this.meshMats == null)
		{
			SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
			if (componentsInChildren.Length == 0)
			{
				return;
			}
			this.meshMats = new Material[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.meshMats[i] = componentsInChildren[i].material;
			}
		}
		if (this.meshMats == null)
		{
			return;
		}
		for (int j = 0; j < this.meshMats.Length; j++)
		{
			this.meshMats[j].SetColor("_RimColor", Color.black);
			this.meshMats[j].SetFloat("_RimPower", 1f);
		}
	}

	// Token: 0x0600327B RID: 12923 RVA: 0x000C51D8 File Offset: 0x000C33D8
	public virtual void ChangeHPVal(long newHP)
	{
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x000C51DC File Offset: 0x000C33DC
	public virtual void ChangeHPEffect(long newHP, GameDefine.DAMAGEBOARD_TYPE damageType)
	{
	}

	// Token: 0x0600327D RID: 12925 RVA: 0x000C51E0 File Offset: 0x000C33E0
	public virtual void ChangeLevel(int level, int combovalue = 0)
	{
	}

	// Token: 0x0600327E RID: 12926 RVA: 0x000C51E4 File Offset: 0x000C33E4
	public void UpdateDamgeBoard(GameDefine.DAMAGEBOARD_TYPE type, long cHP)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		string strValue = string.Empty;
		if (type == GameDefine.DAMAGEBOARD_TYPE.PLAYER_ATTACK_MISS || type == GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_MISS)
		{
			strValue = StrDictionary.GetDictionaryString("#{100140}", new object[0]);
		}
		else if (type == GameDefine.DAMAGEBOARD_TYPE.PLAYER_ATTACK_CRITICAL || type == GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_CRITICAL)
		{
			strValue = string.Format("{0}-{1}", StrDictionary.GetDictionaryString("#{100141}", new object[0]), cHP);
		}
		else if (type == GameDefine.DAMAGEBOARD_TYPE.PLAYER_HP_UP)
		{
			strValue = string.Format("+{0}", cHP);
		}
		else
		{
			strValue = string.Format("-{0}", cHP);
		}
		if (sceneManager.DamageBoardManger != null)
		{
			Vector3 position = base.Position;
			position.y += Mathf.Max(this.ModelHeight - 2f, 0f);
			sceneManager.DamageBoardManger.ShowDamgaeBoard((int)type, strValue, position);
		}
		else
		{
			Debug.Log("sceneManger.DamageBoardManger==null");
		}
	}

	// Token: 0x0600327F RID: 12927 RVA: 0x000C52E8 File Offset: 0x000C34E8
	public void UpdateSkillName(GameDefine.DAMAGEBOARD_TYPE type, string name)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.DamageBoardManger != null)
		{
			sceneManager.DamageBoardManger.ShowDamgaeBoard((int)type, name, base.Position);
		}
	}

	// Token: 0x06003280 RID: 12928 RVA: 0x000C5328 File Offset: 0x000C3528
	public virtual void OnBeaton()
	{
		if (this.onBeaton != null)
		{
			this.onBeaton();
		}
		this.ActiveIdleAttack();
	}

	// Token: 0x06003281 RID: 12929 RVA: 0x000C5348 File Offset: 0x000C3548
	public virtual void OnSkillEnable(string skillID)
	{
	}

	// Token: 0x06003282 RID: 12930 RVA: 0x000C534C File Offset: 0x000C354C
	public virtual void OnSkillDisable(string skillID)
	{
	}

	// Token: 0x06003283 RID: 12931 RVA: 0x000C5350 File Offset: 0x000C3550
	public virtual void OnSkillFinished()
	{
		this.EnableNavMeshAgent();
		if (this.onSkillFinished != null)
		{
			this.onSkillFinished();
		}
	}

	// Token: 0x06003284 RID: 12932 RVA: 0x000C5370 File Offset: 0x000C3570
	public virtual void OnSkillUseSuccess(string skillId)
	{
		this.DisableNavMeshAgent();
		if (this.mSkillUseSuccessDic.Count != 0 && this.mSkillUseSuccessDic.ContainsKey(skillId))
		{
			this.mSkillUseSuccessDic[skillId](skillId);
			this.mSkillUseSuccessDic.Remove(skillId);
		}
		this.ActiveIdleAttack();
	}

	// Token: 0x06003285 RID: 12933 RVA: 0x000C53CC File Offset: 0x000C35CC
	public virtual void OnSkillUseFail(string skillId)
	{
	}

	// Token: 0x06003286 RID: 12934 RVA: 0x000C53D0 File Offset: 0x000C35D0
	public void ActiveIdleAttack()
	{
		this.mIdleAttackFlag = true;
		this.idleAttackTimeCount = ObjCharacter.idleAttackLastTime;
		this.mCurAnimationState = GameDefine.ANIMATIONSTATE.IDLE_ATTACK;
	}

	// Token: 0x06003287 RID: 12935 RVA: 0x000C53EC File Offset: 0x000C35EC
	public void DisactiveIdleAttack()
	{
		this.mIdleAttackFlag = false;
		if (!this.IsDie && this.mCurAnimationState == GameDefine.ANIMATIONSTATE.IDLE_ATTACK)
		{
			this.OnSwithAnimState(GameDefine.ANIMATIONSTATE.IDLE);
		}
	}

	// Token: 0x06003288 RID: 12936 RVA: 0x000C5414 File Offset: 0x000C3614
	public virtual void UpdateHeadInfo()
	{
		if (this.mHeadInfoLogic != null)
		{
			this.mHeadInfoLogic.SetHpVal((float)this.AttributeData.HP / (float)this.AttributeData.MaxHP);
		}
	}

	// Token: 0x06003289 RID: 12937 RVA: 0x000C5458 File Offset: 0x000C3658
	public virtual void RefreshHeadInfo()
	{
	}

	// Token: 0x0600328A RID: 12938 RVA: 0x000C545C File Offset: 0x000C365C
	public void DisactiveHeadInfo()
	{
		if (this.mHeadInfoLogic != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mHeadInfoLogic.gameObject, false);
		}
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x000C548C File Offset: 0x000C368C
	public void ActiveHeadInfo()
	{
		if (CameraController.CurrentViewState != CameraController.CAMERAVIEWSTATE.FIXED_3D && CameraController.CurrentViewState != CameraController.CAMERAVIEWSTATE.FEXED_2_FIXED_3D && this.mHeadInfoLogic != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mHeadInfoLogic.gameObject, true);
			this.mHeadInfoLogic.Init();
		}
	}

	// Token: 0x0600328C RID: 12940 RVA: 0x000C54DC File Offset: 0x000C36DC
	public virtual void Recyle()
	{
		if (this.mHeadInfoLogic != null)
		{
			ResourcesManager.UnLoadHeadInfoPrefab(this.mHeadInfoLogic.gameObject);
			this.mHeadInfoLogic = null;
		}
		if (this.mSimpleShadow != null)
		{
			ResourcesManager.UnLoadSimpleShadowPrefab(this.mSimpleShadow.gameObject);
			this.mSimpleShadow = null;
		}
	}

	// Token: 0x0600328D RID: 12941 RVA: 0x000C553C File Offset: 0x000C373C
	public virtual void EnterCombat(ObjCharacter target, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
	}

	// Token: 0x0600328E RID: 12942 RVA: 0x000C5540 File Offset: 0x000C3740
	public void RegisterOnBeaton(ObjCharacter.OnBeatonDelegate func)
	{
		this.onBeaton = (ObjCharacter.OnBeatonDelegate)Delegate.Combine(this.onBeaton, func);
	}

	// Token: 0x0600328F RID: 12943 RVA: 0x000C555C File Offset: 0x000C375C
	public void DeRegisterOnBeaton(ObjCharacter.OnBeatonDelegate func)
	{
		if (this.onBeaton != null)
		{
			this.onBeaton = (ObjCharacter.OnBeatonDelegate)Delegate.Remove(this.onBeaton, func);
		}
	}

	// Token: 0x06003290 RID: 12944 RVA: 0x000C558C File Offset: 0x000C378C
	public void RegisterOnSkillFinished(ObjCharacter.OnSkillFinishedDelegate func)
	{
		this.onSkillFinished = (ObjCharacter.OnSkillFinishedDelegate)Delegate.Combine(this.onSkillFinished, func);
	}

	// Token: 0x06003291 RID: 12945 RVA: 0x000C55A8 File Offset: 0x000C37A8
	public void DeRegisterOnSkillFinished(ObjCharacter.OnSkillFinishedDelegate func)
	{
		if (this.onSkillFinished != null)
		{
			this.onSkillFinished = (ObjCharacter.OnSkillFinishedDelegate)Delegate.Remove(this.onSkillFinished, func);
		}
	}

	// Token: 0x06003292 RID: 12946 RVA: 0x000C55D8 File Offset: 0x000C37D8
	private void Destroy()
	{
		this.DeRegisterEvent();
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x000C55E0 File Offset: 0x000C37E0
	private void RegisterEvent()
	{
		this.mBuffLogic.RegisterOnSleep(new BuffLogic.BuffDelegate(this.OnSleep));
		this.mBuffLogic.RegisterOnSleepDone(new BuffLogic.BuffDelegate(this.OnSleepDone));
		this.mBuffLogic.RegisterOnStun(new BuffLogic.BuffDelegate(this.OnStun));
		this.mBuffLogic.RegisterOnStunDone(new BuffLogic.BuffDelegate(this.OnStunDone));
		this.mBuffLogic.RegisterOnKnockDown(new BuffLogic.BuffSenderDelegate(this.OnKnockDown));
		this.mBuffLogic.RegisterOnKnockDownDone(new BuffLogic.BuffDelegate(this.OnKnockDownDone));
	}

	// Token: 0x06003294 RID: 12948 RVA: 0x000C567C File Offset: 0x000C387C
	private void DeRegisterEvent()
	{
		this.mBuffLogic.DeRegisterOnSleep(new BuffLogic.BuffDelegate(this.OnSleep));
		this.mBuffLogic.DeRegisterOnSleepDone(new BuffLogic.BuffDelegate(this.OnSleepDone));
		this.mBuffLogic.DeRegisterOnStun(new BuffLogic.BuffDelegate(this.OnStun));
		this.mBuffLogic.DeRegisterOnStunDone(new BuffLogic.BuffDelegate(this.OnStunDone));
		this.mBuffLogic.DeRegisterOnKnockDown(new BuffLogic.BuffSenderDelegate(this.OnKnockDown));
		this.mBuffLogic.DeRegisterOnKnockDownDone(new BuffLogic.BuffDelegate(this.OnKnockDownDone));
	}

	// Token: 0x06003295 RID: 12949 RVA: 0x000C5718 File Offset: 0x000C3918
	public void EnableNavMeshAgent()
	{
		this.mNavMeshAgent.enabled = true;
	}

	// Token: 0x06003296 RID: 12950 RVA: 0x000C5728 File Offset: 0x000C3928
	public void DisableNavMeshAgent()
	{
		this.mNavMeshAgent.enabled = false;
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x000C5738 File Offset: 0x000C3938
	public float MainPlayerDistance()
	{
		if (this.mObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			return 0f;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			return VectorXZ.Distance(base.Position, Singleton<ObjManager>.Instance.MainPlayer.Position);
		}
		return float.MaxValue;
	}

	// Token: 0x0400215F RID: 8543
	private float currentTime = 0.5f;

	// Token: 0x04002160 RID: 8544
	private float checkTime = 0.5f;

	// Token: 0x04002161 RID: 8545
	private Vector3 lastPos = Vector3.forward * float.MaxValue;

	// Token: 0x04002162 RID: 8546
	public Dictionary<string, Transform> BonesTrsDict;

	// Token: 0x04002163 RID: 8547
	private float mStopRange = 1f;

	// Token: 0x04002164 RID: 8548
	private bool mIsMoving;

	// Token: 0x04002165 RID: 8549
	private bool mIsDie;

	// Token: 0x04002166 RID: 8550
	private bool mIsStunFlag;

	// Token: 0x04002167 RID: 8551
	private bool mIsKnockDownFlag;

	// Token: 0x04002168 RID: 8552
	private bool mIsSleepFlag;

	// Token: 0x04002169 RID: 8553
	private bool mInvincibleFlag;

	// Token: 0x0400216A RID: 8554
	public bool mIdleAttackFlag;

	// Token: 0x0400216B RID: 8555
	private float idleAttackTimeCount;

	// Token: 0x0400216C RID: 8556
	public static float idleAttackLastTime = 5f;

	// Token: 0x0400216D RID: 8557
	private Vector3 mTargetPos;

	// Token: 0x0400216E RID: 8558
	protected AnimationLogic mAnimationLogic;

	// Token: 0x0400216F RID: 8559
	protected SkillLogic mSkillLogic;

	// Token: 0x04002170 RID: 8560
	protected SkillMotion mSkillMotion;

	// Token: 0x04002171 RID: 8561
	protected EffectLogic mEffectLogic;

	// Token: 0x04002172 RID: 8562
	protected EffectMotion mEffectMotion;

	// Token: 0x04002173 RID: 8563
	protected BuffLogic mBuffLogic;

	// Token: 0x04002174 RID: 8564
	protected string mCurUseSkillId = string.Empty;

	// Token: 0x04002175 RID: 8565
	protected HeadInfoLogic mHeadInfoLogic;

	// Token: 0x04002176 RID: 8566
	protected GameObject mSimpleShadow;

	// Token: 0x04002177 RID: 8567
	protected CharacterAttributeData mAttributeData;

	// Token: 0x04002178 RID: 8568
	protected NavMeshAgent mNavMeshAgent;

	// Token: 0x04002179 RID: 8569
	protected ObjCharacter.TargetArriveFinsh targetArriveFinish;

	// Token: 0x0400217A RID: 8570
	protected ObjCharacter.TargetArriveFinsh tempTargetArriveFinish;

	// Token: 0x0400217B RID: 8571
	protected Dictionary<string, ObjCharacter.SkillUseSuccess> mSkillUseSuccessDic = new Dictionary<string, ObjCharacter.SkillUseSuccess>();

	// Token: 0x0400217C RID: 8572
	public ObjCharacter.OnBeatonDelegate onBeaton;

	// Token: 0x0400217D RID: 8573
	public ObjCharacter.OnSkillFinishedDelegate onSkillFinished;

	// Token: 0x0400217E RID: 8574
	protected float mComboTimeCount;

	// Token: 0x0400217F RID: 8575
	protected float mComboTimeTotalCount;

	// Token: 0x04002180 RID: 8576
	protected string mComboIndex;

	// Token: 0x04002181 RID: 8577
	protected bool mComboNextFlag;

	// Token: 0x04002182 RID: 8578
	private bool mIsLocalDrivingCar;

	// Token: 0x04002183 RID: 8579
	private ObjPlayerCar mCurPlayerCar;

	// Token: 0x04002184 RID: 8580
	private GameDefine.ANIMATIONSTATE mCurAnimationState;

	// Token: 0x04002185 RID: 8581
	protected List<CharacterSkillData> mCharacterSkillData = new List<CharacterSkillData>();

	// Token: 0x04002186 RID: 8582
	protected List<ObjCharacter> mEffectTargetList = new List<ObjCharacter>();

	// Token: 0x04002187 RID: 8583
	protected float mHoldTimeCount;

	// Token: 0x04002188 RID: 8584
	protected PLAYER_STATE mCurPlayerState;

	// Token: 0x04002189 RID: 8585
	public string[] TargetPartObjId = new string[4];

	// Token: 0x0400218A RID: 8586
	private string WeaponModelID = string.Empty;

	// Token: 0x0400218B RID: 8587
	private string WeaponItemID = string.Empty;

	// Token: 0x0400218C RID: 8588
	private string mWeaponTypeName = string.Empty;

	// Token: 0x0400218D RID: 8589
	public bool IsShowInvincibleEffect = true;

	// Token: 0x0400218E RID: 8590
	private float invincibleEffectSpeed = 0.2f;

	// Token: 0x0400218F RID: 8591
	private Material[] meshMats;

	// Token: 0x04002190 RID: 8592
	private float invincibleFlashSkinPercent;

	// Token: 0x04002191 RID: 8593
	protected float mReceiveBiggerHpTime = 1f;

	// Token: 0x04002192 RID: 8594
	protected float mReceiveBiggerHpTimeCount;

	// Token: 0x02000AC0 RID: 2752
	// (Invoke) Token: 0x06004F89 RID: 20361
	public delegate void TargetArriveFinsh(ObjCharacter objCha);

	// Token: 0x02000AC1 RID: 2753
	// (Invoke) Token: 0x06004F8D RID: 20365
	public delegate void SkillUseSuccess(string id);

	// Token: 0x02000AC2 RID: 2754
	// (Invoke) Token: 0x06004F91 RID: 20369
	public delegate void OnBeatonDelegate();

	// Token: 0x02000AC3 RID: 2755
	// (Invoke) Token: 0x06004F95 RID: 20373
	public delegate void OnSkillFinishedDelegate();
}
