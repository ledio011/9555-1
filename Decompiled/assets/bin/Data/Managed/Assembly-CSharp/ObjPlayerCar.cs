using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class ObjPlayerCar : ObjCar
{
	// Token: 0x06000A52 RID: 2642 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
	public ObjPlayerCar()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR;
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
	public LensFlareSensor LLight
	{
		get
		{
			return this.BLLight;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0004C3D8 File Offset: 0x0004A5D8
	public LensFlareSensor RLight
	{
		get
		{
			return this.BRLight;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0004C3E0 File Offset: 0x0004A5E0
	public MountData CurMountData
	{
		get
		{
			return this.mCurMountData;
		}
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0004C3E8 File Offset: 0x0004A5E8
	public void InitCar()
	{
		this.Init();
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0004C3F0 File Offset: 0x0004A5F0
	protected new void Init()
	{
		base.Init();
		this.DummyPlayerPoint = base.transform.FindChild("MeshRoot/Dummy_Player");
		this.DummyNPCPoint = base.transform.FindChild("MeshRoot/Dummy_NPC");
		if (this.DummyNPCPoint != null)
		{
			this.DefaultNpcPos = this.DummyNPCPoint.transform.localPosition;
			this.DefaultNpcRotation = this.DummyNPCPoint.transform.localRotation;
		}
		this.CarDoorView = base.transform.FindChild("MeshRoot/CarDoorView");
		this.StartCamView = base.transform.FindChild("MeshRoot/StartCamViewPoint");
		this.WinCamView = base.transform.FindChild("MeshRoot/WinCamViewPoint");
		Transform transform = base.transform.FindChild("MeshRoot/BLWheel/SkidSmoke");
		if (transform != null)
		{
			this.mLSkidSmoke = transform.gameObject.GetComponent<ParticleSystem>();
		}
		else
		{
			this.mLSkidSmoke = null;
		}
		Transform transform2 = base.transform.FindChild("MeshRoot/BRWheel/SkidSmoke");
		if (transform2 != null)
		{
			this.mRSkidSmoke = transform2.gameObject.GetComponent<ParticleSystem>();
		}
		else
		{
			this.mRSkidSmoke = null;
		}
		this.MeshRoot = base.transform.FindChild("MeshRoot").gameObject;
		if (this.mLSkidSmoke != null && UnityVersionUtil.IsactiveInHierarchy(this.mLSkidSmoke.gameObject))
		{
			this.mLSkidSmoke.enableEmission = false;
		}
		if (this.mRSkidSmoke != null && UnityVersionUtil.IsactiveInHierarchy(this.mRSkidSmoke.gameObject))
		{
			this.mRSkidSmoke.enableEmission = false;
		}
		if (this.mRSkidSmoke != null && UnityVersionUtil.IsactiveInHierarchy(this.mRSkidSmoke.gameObject))
		{
			this.mRSkidSmoke.enableEmission = false;
		}
		if (this.mSkidmarks == null)
		{
			if (SingletonUnity<Skidmarks>.Exists)
			{
				this.mSkidmarks = SingletonUnity<Skidmarks>.Instance;
			}
			else
			{
				Debug.Log("No Skidmarks!!!!!!!!!!!!!!!!!!!!");
			}
		}
		this.mMinDriftSpeedPercent = this.DriftStartMinSpeed / this.MaxSpeed;
		Transform transform3 = base.transform.FindChild("MeshRoot/BackLight/BLLight");
		if (transform3 != null)
		{
			this.BLLight = transform3.gameObject.GetComponent<LensFlareSensor>();
		}
		Transform transform4 = base.transform.FindChild("MeshRoot/BackLight/BRLight");
		if (transform4 != null)
		{
			this.BRLight = transform4.gameObject.GetComponent<LensFlareSensor>();
		}
		if (this.AttributeData == null)
		{
			this.AttributeData = new CharacterAttributeData();
		}
		this.mTransform = base.transform;
		Transform transform5 = base.transform.FindChild("MeshRoot/effect_Smoke");
		Transform transform6 = base.transform.FindChild("MeshRoot/effect_baoZha");
		if (transform5 != null)
		{
			this.SmokeParticle = transform5.gameObject.GetComponent<ParticleSystem>();
			this.SmokeParticle.Stop();
			UnityVersionUtil.SetActiveRecursive(this.SmokeParticle.gameObject, false);
		}
		if (transform6 != null)
		{
			this.ExplosionParticle = transform6.gameObject.GetComponent<ParticleSystem>();
			this.ExplosionParticle.Stop();
			UnityVersionUtil.SetActiveRecursive(this.ExplosionParticle.gameObject, false);
		}
		this.DieCamView = base.transform.FindChild("MeshRoot/DieCamView");
		this.PlayerDiePos = base.transform.FindChild("MeshRoot/PlayerDiePos");
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0004C760 File Offset: 0x0004A960
	public void OnMeshLoadDone(MountCarMeshRoot mountCarMeshRoot)
	{
		this.FLWheel.wheelTrs = mountCarMeshRoot.QLWheel;
		this.FLWheel.Init(mountCarMeshRoot.WheelRadius);
		this.FRWheel.wheelTrs = mountCarMeshRoot.QRWheel;
		this.FRWheel.Init(mountCarMeshRoot.WheelRadius);
		this.BLWheel.wheelTrs = mountCarMeshRoot.HLWheel;
		this.BLWheel.Init(mountCarMeshRoot.WheelRadius);
		this.BRWheel.wheelTrs = mountCarMeshRoot.HRWheel;
		this.BRWheel.Init(mountCarMeshRoot.WheelRadius);
		this.CarBodyRoot = mountCarMeshRoot.CarBodyRoot;
		GameObject gameObject = this.MeshRoot.transform.FindChild("cheshen").gameObject;
		gameObject.layer = LayerMask.NameToLayer("PlayerCar");
		gameObject.tag = "PlayerCar";
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		component.size = mountCarMeshRoot.ColliderSize;
		component.center = mountCarMeshRoot.ColliderCenter;
		this.defaultColliderSize = mountCarMeshRoot.ColliderSize;
		NGUITools.SetLayer(mountCarMeshRoot.gameObject, gameObject.layer);
		GameObject gameObject2 = this.MeshRoot.transform.FindChild("FrontCollision").gameObject;
		gameObject2.layer = gameObject.layer;
		gameObject2.tag = gameObject.tag;
		gameObject2.transform.localPosition = component.center + Vector3.forward * (component.size.z / 2f + 0.18f);
		GameObject gameObject3 = new GameObject("PlayerCarCollider");
		gameObject3.transform.parent = gameObject.transform.parent;
		gameObject3.transform.localPosition = gameObject.transform.localPosition;
		gameObject3.transform.localRotation = gameObject.transform.localRotation;
		BoxCollider boxCollider = gameObject3.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
		gameObject3.layer = gameObject.layer;
		gameObject3.tag = gameObject.tag;
		Singleton<ObjManager>.Instance.MainPlayer.DisableMainPlayer();
		if (this.mCurMountData != null)
		{
			if (this.BLLight != null)
			{
				this.BLLight.transform.localPosition = this.mCurMountData.LightPosL;
			}
			if (this.BRLight != null)
			{
				this.BRLight.transform.localPosition = this.mCurMountData.LightPosR;
			}
		}
		(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as CarChaseSceneManager).OnPlayerCarMeshLoadDone();
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0004CA28 File Offset: 0x0004AC28
	private void Awake()
	{
		this.soundManager = SingletonDontDestoryUnity<SoundManager>.Instance;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0004CA38 File Offset: 0x0004AC38
	private new void Update()
	{
		if (this.mFreezeCarFlag)
		{
			if (this.BLLight != null && !UnityVersionUtil.IsActive(this.BLLight.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.BLLight.gameObject, true);
			}
			if (this.BRLight != null && !UnityVersionUtil.IsActive(this.BRLight.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.BRLight.gameObject, true);
			}
			return;
		}
		if (this.mStopCarFlag)
		{
			if (base.CurSpeed > 0.5f)
			{
				this.mCarControl.OnPressBrakeBtn(true);
			}
			else
			{
				this.FreezeCar();
			}
			return;
		}
		base.Update();
		if (Vector3.Angle(base.transform.up, Vector3.up) > 75f || base.transform.position.y < -7f)
		{
			this.creshCount += Time.deltaTime;
			if (this.creshCount > 2f)
			{
				base.transform.eulerAngles = new Vector3(0f, base.transform.eulerAngles.y, 0f);
				base.rigidbody.velocity = Vector3.zero;
				base.rigidbody.angularVelocity = Vector3.zero;
				this.creshCount = 0f;
				if (base.transform.position.y < -7f)
				{
					base.transform.position = new Vector3(base.transform.position.x, SceneManager.GetHitHeight(base.transform.position) + 0.5f, base.transform.position.z);
				}
			}
		}
		else
		{
			this.creshCount = 0f;
		}
		if (this.mCarControl.IsBarking)
		{
			if (this.BLLight != null && !UnityVersionUtil.IsActive(this.BLLight.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.BLLight.gameObject, true);
				if (base.CurSpeed > 10f)
				{
					this.mBrakeSoundVolume = 0.2f + 0.8f * Mathf.Clamp01(base.CurSpeed / 60f);
					this.soundManager.PlaySoundEffect(this.mCarBrakeSoundId, this.mBrakeSoundVolume, null);
				}
			}
			if (this.BRLight != null && !UnityVersionUtil.IsActive(this.BRLight.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.BRLight.gameObject, true);
			}
		}
		else
		{
			if (this.BLLight != null && UnityVersionUtil.IsActive(this.BLLight.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.BLLight.gameObject, false);
				this.soundManager.StopSoundEffect(this.mCarBrakeSoundId);
			}
			if (this.BRLight != null && UnityVersionUtil.IsActive(this.BRLight.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.BRLight.gameObject, false);
			}
		}
		if (this.mCarControl.CurSpeed > 0f)
		{
			float num = Mathf.Lerp(-this.MaxZAngle, this.MaxZAngle, (this.mCarControl.SignCurSteerPercent + 1f) / 2f);
			if (this.CurMountData.IsMotorBool)
			{
				num = -num;
			}
			num = Mathf.Lerp((this.CarBodyRoot.transform.localEulerAngles.z <= 180f) ? this.CarBodyRoot.transform.localEulerAngles.z : (this.CarBodyRoot.transform.localEulerAngles.z - 360f), num, Time.deltaTime * 4f);
			this.CarBodyRoot.transform.localEulerAngles = new Vector3(this.CarBodyRoot.transform.localEulerAngles.x, this.CarBodyRoot.transform.localEulerAngles.y, num);
		}
		else
		{
			float num2 = 0f;
			num2 = Mathf.Lerp((this.CarBodyRoot.transform.localEulerAngles.z <= 180f) ? this.CarBodyRoot.transform.localEulerAngles.z : (this.CarBodyRoot.transform.localEulerAngles.z - 360f), num2, Time.deltaTime * 4f);
			this.CarBodyRoot.transform.localEulerAngles = new Vector3(this.CarBodyRoot.transform.localEulerAngles.x, this.CarBodyRoot.transform.localEulerAngles.y, num2);
		}
		this.SynPlayerPosition();
		this.SetCarEngineAudio();
		if (SingletonUnity<CarBestTimeCountRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CarBestTimeCountRoot>.Instance.gameObject))
		{
			SingletonUnity<CarBestTimeCountRoot>.Instance.SetSpeedLable((int)((double)base.CurSpeed * 3.6));
		}
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0004CF90 File Offset: 0x0004B190
	public void ResetPlayerCar(ObjCarInitData initData)
	{
		base.Reset();
		Transform transform = base.transform.FindChild("MeshRoot/PlayerCarCollider");
		if (transform != null)
		{
			this.mFrontTrigger = base.transform.FindChild("MeshRoot/PlayerCarCollider").gameObject;
		}
		base.CacheTransform.position = new Vector3(initData.Pos.x, SceneManager.GetHitHeight(initData.Pos.x, initData.Pos.z) + 0.1f, initData.Pos.z);
		base.CacheTransform.eulerAngles = initData.Angle;
		if (!base.rigidbody.isKinematic)
		{
			base.rigidbody.velocity = Vector3.zero;
			base.rigidbody.angularVelocity = Vector3.zero;
		}
		base.rigidbody.centerOfMass = Vector3.zero;
		base.rigidbody.mass = 6000f;
		if (initData.CarMountData != null)
		{
			this.mCarControl.maxSpeed = initData.CarMountData.MaxSp;
			this.mCarControl.maxSteerAngle = initData.CarMountData.MaxSteerAngle;
			this.mCarControl.maxAcceleration = initData.CarMountData.MaxAcce;
			this.mCarControl.brakeAcceleration = initData.CarMountData.BrakeAcce;
			this.mCurMountData = initData.CarMountData;
			this.AttributeData.MaxHP = (long)initData.CarMountData.MaxHP;
			this.AttributeData.HP = this.AttributeData.MaxHP;
			this.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
		}
		else
		{
			this.AttributeData.MaxHP = 900L;
			this.AttributeData.HP = 900L;
		}
		BoxCollider component = this.CarBodyRoot.GetComponent<BoxCollider>();
		if (component != null && this.defaultColliderSize.y < 0.1f)
		{
			this.defaultColliderSize = component.size;
		}
		this.DisableCar();
		this.ServerId = initData.ServerID;
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0004D1A0 File Offset: 0x0004B3A0
	public void BeforeLoadMeshReset(ObjCarInitData initData)
	{
		base.Reset();
		base.CacheTransform.position = initData.Pos;
		base.CacheTransform.eulerAngles = initData.Angle;
		if (!base.rigidbody.isKinematic)
		{
			base.rigidbody.velocity = Vector3.zero;
			base.rigidbody.angularVelocity = Vector3.zero;
		}
		base.rigidbody.centerOfMass = Vector3.zero;
		base.rigidbody.mass = 6000f;
		if (this.AttributeData == null)
		{
			this.AttributeData = new CharacterAttributeData();
		}
		if (initData.CarMountData != null)
		{
			this.mCurMountData = initData.CarMountData;
			this.AttributeData.MaxHP = (long)initData.CarMountData.MaxHP;
			this.AttributeData.HP = this.AttributeData.MaxHP;
			this.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
		}
		else
		{
			this.AttributeData.MaxHP = 900L;
			this.AttributeData.HP = 900L;
		}
		this.ServerId = initData.ServerID;
		if (!base.rigidbody.isKinematic)
		{
			base.rigidbody.velocity = Vector3.zero;
			base.rigidbody.angularVelocity = Vector3.zero;
		}
		base.rigidbody.useGravity = false;
		base.rigidbody.isKinematic = true;
		base.rigidbody.drag = 0.6f;
		base.rigidbody.angularDrag = 0.6f;
		NGUITools.SetLayer(base.gameObject, LayerMask.NameToLayer("Default"));
		this.mCarEngineAudio = null;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0004D344 File Offset: 0x0004B544
	public void SetPath(CarPath path, int curIndex)
	{
		this.mPath = path;
		this.mCurPathIndex = curIndex;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0004D354 File Offset: 0x0004B554
	public void OnPressAccelBtn(bool isPress)
	{
		this.mCarControl.OnPressAccelBtn(isPress);
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0004D364 File Offset: 0x0004B564
	public void OnPressBrakeBtn(bool isPress)
	{
		this.mCarControl.OnPressBrakeBtn(isPress);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0004D374 File Offset: 0x0004B574
	public void OnPressLeftBtn(bool isPress)
	{
		this.mCarControl.OnPressLeftBtn(isPress);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0004D384 File Offset: 0x0004B584
	public void OnPressRightBtn(bool isPress)
	{
		this.mCarControl.OnPressRightBtn(isPress);
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0004D394 File Offset: 0x0004B594
	public void OnCollisionEnter(Collision other)
	{
		if (base.enabled && other.relativeVelocity.sqrMagnitude > 100f && other.gameObject.layer != LayerMask.NameToLayer("ObjCharacter") && other.gameObject.layer != LayerMask.NameToLayer("Floor"))
		{
			base.EnableStrike(other.gameObject, other.contacts[0].point);
			this.mHitCarSoundVolume = 0.2f + 0.8f * Mathf.Clamp01(other.relativeVelocity.sqrMagnitude / 1800f);
			this.soundManager.PlaySoundEffect(this.mHitCarSoundId, this.mHitCarSoundVolume, null);
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0004D458 File Offset: 0x0004B658
	public void OnCollisionExit(Collision other)
	{
		base.DisableStrike(other.gameObject);
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0004D468 File Offset: 0x0004B668
	private void ChangeHP(int newHP)
	{
		if (SingletonUnity<CarHPRootLogic>.Exists)
		{
			SingletonUnity<CarHPRootLogic>.Instance.ChangeHP(Mathf.Max(0, newHP));
			if (newHP <= 0)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.FailMission();
			}
		}
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0004D4A8 File Offset: 0x0004B6A8
	private new void FixedUpdate()
	{
		if (this.mFreezeCarFlag)
		{
			base.rigidbody.velocity = Vector3.zero;
			base.rigidbody.angularVelocity = Vector3.zero;
			return;
		}
		base.FixedUpdate();
		if (!this.DriftFlag && !this.ExitDirftFlag && base.CurSpeed > this.DriftStartMinSpeed && this.mCarControl.IsBarking && this.mCarControl.CurSteerPercent > this.DriftStartMinAnglePercent)
		{
			this.EnterDrift();
			this.DriftFlag = true;
		}
		if (this.DriftFlag && !this.ExitDirftFlag && this.mCarControl.OnTheGroundFlag)
		{
			if (base.CurSpeed < this.DriftExitMinSpeed || this.mCarControl.CurSteerPercent < this.DriftExitMinAnglePercent)
			{
				this.DriftFlag = false;
				this.ExitDirftFlag = true;
				this.ExitDrift();
			}
			else
			{
				float num = Mathf.Sin(this.mCarControl.CurSpeedPercent);
				this.curDriftAngleSpeed = num * this.DriftAngleSpeed * (float)((this.mCarControl.InputSteer <= 0f) ? -1 : 1);
				this.curDriftTargetMeshAngle = Mathf.Abs(num * this.DriftMaxMeshAngle * this.mCarControl.CurSteerPercent);
				float num2 = this.MeshRoot.transform.localEulerAngles.y;
				num2 = Mathf.Clamp(((num2 <= 180f) ? num2 : (num2 - 360f)) + this.curDriftAngleSpeed * Time.deltaTime, -this.curDriftTargetMeshAngle, this.curDriftTargetMeshAngle);
				this.MeshRoot.transform.localEulerAngles = new Vector3(0f, num2, 0f);
			}
		}
		if (this.ExitDirftFlag)
		{
			float num3 = this.MeshRoot.transform.localEulerAngles.y;
			num3 = ((num3 <= 180f) ? num3 : (num3 - 360f));
			float num4 = num3 + this.DriftAngleSpeed * 0.5f * (float)((num3 <= 0f) ? 1 : -1) * Time.deltaTime;
			if (num4 * num3 < 0f)
			{
				num4 = 0f;
				this.ExitDirftFlag = false;
			}
			this.MeshRoot.transform.localEulerAngles = new Vector3(0f, num4, 0f);
		}
		if (((this.mCarControl.IsBarking && base.CurSpeed > 10f) || this.DriftFlag) && this.mCarControl.OnTheGroundFlag)
		{
			this.skidMaskIntensityPercent = Mathf.Min(this.skidMaskIntensityPercent + Time.deltaTime * 3f, 1f);
			if (this.CurMountData.IsMotorBool)
			{
				this.SetSkidmark(ref this.lastFLSkidmark, this.FLWheel, this.FRWheel);
				this.SetSkidmark(ref this.lastBLSkidmark, this.BLWheel, this.BRWheel);
			}
			else
			{
				this.SetSkidmark(ref this.lastFLSkidmark, this.FLWheel);
				this.SetSkidmark(ref this.lastFRSkidmark, this.FRWheel);
				this.SetSkidmark(ref this.lastBLSkidmark, this.BLWheel);
				this.SetSkidmark(ref this.lastBRSkidmark, this.BRWheel);
			}
			if (GameSettingData.IsCarCopyEffectEnable[GameSettingData.GetPhoneClass()])
			{
				if (this.mLSkidSmoke != null && !this.mLSkidSmoke.enableEmission)
				{
					this.mLSkidSmoke.enableEmission = true;
				}
				if (this.mRSkidSmoke != null && !this.mRSkidSmoke.enableEmission)
				{
					this.mRSkidSmoke.enableEmission = true;
				}
			}
		}
		else
		{
			this.lastFLSkidmark = -1;
			this.lastFRSkidmark = -1;
			this.lastBLSkidmark = -1;
			this.lastBRSkidmark = -1;
			this.skidMaskIntensityPercent = 0f;
			if (GameSettingData.IsCarCopyEffectEnable[GameSettingData.GetPhoneClass()])
			{
				if (this.mLSkidSmoke != null)
				{
					this.mLSkidSmoke.Play();
					if (this.mLSkidSmoke.enableEmission)
					{
						this.mLSkidSmoke.enableEmission = false;
					}
				}
				if (this.mRSkidSmoke != null)
				{
					this.mRSkidSmoke.Play();
					if (this.mRSkidSmoke.enableEmission)
					{
						this.mRSkidSmoke.enableEmission = false;
					}
				}
			}
		}
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0004D918 File Offset: 0x0004BB18
	private void EnterDrift()
	{
		this.mCarControl.maxSteerAngle = this.mCarControl.maxSteerAngle * this.DriftAngleEnhance;
		this.mCarControl.brakeAcceleration /= this.DriftBrakeReduce;
		this.mBrakeSoundVolume = 0.2f + 0.8f * Mathf.Clamp01(base.CurSpeed / 60f);
		this.soundManager.PlaySoundEffect(this.mCarDriftSoundId, this.mBrakeSoundVolume, null);
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0004D998 File Offset: 0x0004BB98
	private void ExitDrift()
	{
		this.mCarControl.maxSteerAngle = this.mCarControl.maxSteerAngle / this.DriftAngleEnhance;
		this.mCarControl.brakeAcceleration *= this.DriftBrakeReduce;
		this.soundManager.StopSoundEffect(this.mCarDriftSoundId);
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0004D9EC File Offset: 0x0004BBEC
	private void SetSkidmark(ref int lastindex, WheelSuspension wheel)
	{
		if (!GameSettingData.IsCarCopyEffectEnable[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		this.tempVector3 = wheel.outHit.point + base.rigidbody.velocity * Time.deltaTime * 2f;
		lastindex = this.mSkidmarks.AddSkidMark(this.tempVector3, wheel.outHit.normal, this.mCarControl.CurSpeedPercent * this.skidMaskIntensityPercent * this.SkidMaskIntensityReduce, lastindex, 0.4f);
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0004DA80 File Offset: 0x0004BC80
	private void SetSkidmark(ref int lastIndex, WheelSuspension leftWheel, WheelSuspension rightWheel)
	{
		if (!GameSettingData.IsCarCopyEffectEnable[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		this.tempVector3 = (leftWheel.outHit.point + rightWheel.outHit.point) / 2f + base.rigidbody.velocity * Time.deltaTime * 2f;
		lastIndex = this.mSkidmarks.AddSkidMark(this.tempVector3, leftWheel.outHit.normal, this.mCarControl.CurSpeedPercent * this.skidMaskIntensityPercent * this.SkidMaskIntensityReduce, lastIndex, 0.4f);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0004DB2C File Offset: 0x0004BD2C
	public void FreezeCar()
	{
		this.mFreezeCarFlag = true;
		if (this.mCarEngineAudio != null)
		{
			this.soundManager.StopSoundEffect(this.mCarEngineSoundId);
		}
		this.mCarEngineAudio = null;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0004DB6C File Offset: 0x0004BD6C
	public void DisFreezeCar()
	{
		this.mFreezeCarFlag = false;
		this.soundManager.PlaySoundEffect(this.mCarEngineSoundId, 1f, new SoundClipPools.OnPlaySoundDelegate(this.OnPlaySound));
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0004DB98 File Offset: 0x0004BD98
	public void StopCar()
	{
		this.mStopCarFlag = true;
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0004DBA4 File Offset: 0x0004BDA4
	private void SynPlayerPosition()
	{
		if (!base.enabled || this.mFreezeCarFlag)
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
			this.request.moving = true;
			this.request.index = 1L;
			this.request.parm = (long)(this.AttributeData.CurSpeed * 100f);
			NetLogic.GetInstance().Send<Protocol.move>(this.request, null);
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0004DD30 File Offset: 0x0004BF30
	public override void EnableCar(ObjCharacter insidePlayer)
	{
		base.EnableCar(insidePlayer);
		NGUITools.SetLayer(base.gameObject, LayerMask.NameToLayer("PlayerCar"));
		if (this.BLLight != null)
		{
			this.BLLight.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
		if (this.BRLight != null)
		{
			this.BRLight.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
		this.CarBodyRoot.gameObject.tag = "PlayerCar";
		base.gameObject.tag = "PlayerCar";
		if (this.mFrontTrigger != null)
		{
			this.mFrontTrigger.gameObject.tag = "PlayerCar";
		}
		this.soundManager.PlaySoundEffect(this.mCarEngineSoundId, 1f, new SoundClipPools.OnPlaySoundDelegate(this.OnPlaySound));
		BoxCollider component = this.CarBodyRoot.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.size = new Vector3(this.defaultColliderSize.x, this.defaultColliderSize.y, this.defaultColliderSize.z);
		}
		insidePlayer.IsLocalDrivingCar = true;
		insidePlayer.CurPlayerCar = this;
		if (insidePlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && SingletonUnity<TouXiangKuangLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TouXiangKuangLogic>.Instance.gameObject))
		{
			SingletonUnity<TouXiangKuangLogic>.Instance.ChangeCarIcon(this.AttributeData.HP, this.AttributeData.MaxHP);
		}
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0004DEB8 File Offset: 0x0004C0B8
	private void OnPlaySound(AudioSource soundSounce)
	{
		this.mCarEngineAudio = soundSounce;
		this.mCarEngineAudio.pitch = this.GetTargetAudioPitch();
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0004DED4 File Offset: 0x0004C0D4
	public void BaseDisableCar()
	{
		base.DisableCar();
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0004DEDC File Offset: 0x0004C0DC
	public override void DisableCar()
	{
		if (this.InsidePlayer != null)
		{
			this.InsidePlayer.IsLocalDrivingCar = false;
			this.InsidePlayer.CurPlayerCar = null;
			if (SingletonUnity<TouXiangKuangLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TouXiangKuangLogic>.Instance.gameObject))
			{
				SingletonUnity<TouXiangKuangLogic>.Instance.Init();
			}
		}
		base.DisableCar();
		NGUITools.SetLayer(base.gameObject, LayerMask.NameToLayer("Default"));
		this.CarBodyRoot.gameObject.tag = "Untagged";
		base.gameObject.tag = "Untagged";
		if (this.mFrontTrigger != null)
		{
			this.mFrontTrigger.gameObject.tag = "Untagged";
		}
		if (this.mCarEngineAudio != null)
		{
			this.soundManager.StopSoundEffect(this.mCarEngineSoundId);
		}
		this.mCarEngineAudio = null;
		BoxCollider component = this.CarBodyRoot.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.size = new Vector3(this.defaultColliderSize.x, this.defaultColliderSize.y + this.FLWheel.WheelRadius * 1.5f, this.defaultColliderSize.z);
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0004E020 File Offset: 0x0004C220
	private void SetCarEngineAudio()
	{
		if (this.mCarEngineAudio != null)
		{
			this.mCarEngineAudio.pitch = Mathf.Lerp(this.mCarEngineAudio.pitch, this.GetTargetAudioPitch(), 0.5f);
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0004E064 File Offset: 0x0004C264
	private float GetTargetAudioPitch()
	{
		return (base.CarControl.CurSpeedPercent + 0.5f) / 1.5f;
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0004E080 File Offset: 0x0004C280
	private void OnDisable()
	{
		if (SingletonDontDestoryUnity<SoundManager>.Exists)
		{
			if (this.mCarEngineAudio != null)
			{
				SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(this.mCarEngineSoundId);
			}
			SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(this.mCarDriftSoundId);
			this.mCarEngineAudio = null;
		}
		base.ClearStrike();
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0004E0D8 File Offset: 0x0004C2D8
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
			if (this.AttributeData.HP < this.AttributeData.MaxHP / 2L && this.SmokeParticle != null && (!UnityVersionUtil.IsActive(this.SmokeParticle.gameObject) || !this.SmokeParticle.isPlaying))
			{
				UnityVersionUtil.SetActiveRecursive(this.SmokeParticle.gameObject, true);
				this.SmokeParticle.Play();
			}
			if (this.InsidePlayer != null && this.InsidePlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
			{
				SingletonUnity<TouXiangKuangLogic>.Instance.ChangeHP(this.AttributeData.HP, this.AttributeData.MaxHP);
			}
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0004E204 File Offset: 0x0004C404
	public override void ChangeHPVal(long newHP)
	{
		if (!base.IsDie)
		{
			if (this.AttributeData.HP != newHP)
			{
				this.AttributeData.HP = newHP;
				if (this.InsidePlayer != null && this.InsidePlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
				{
					SingletonUnity<TouXiangKuangLogic>.Instance.ChangeHP(this.AttributeData.HP, this.AttributeData.MaxHP);
				}
			}
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0004E294 File Offset: 0x0004C494
	public override void OnDie()
	{
		base.IsDie = true;
		if (this.ExplosionParticle != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ExplosionParticle.gameObject, true);
			this.ExplosionParticle.Play();
		}
		this.MeshRoot.animation.Play("GTACheBaoZa_Animation");
		vp_Timer.In(this.MeshRoot.animation["GTACheBaoZa_Animation"].length, delegate()
		{
			base.rigidbody.useGravity = true;
			base.rigidbody.isKinematic = false;
		}, null);
		SingletonUnity<CitySimController>.Instance.OnPlayerCarDie();
		if (this.InsidePlayer != null)
		{
			SingletonUnity<UIManager>.Instance.HideBaseUI();
			if (this.InsidePlayer.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
			{
				ObjMainPlayer objMainPlayer = this.InsidePlayer as ObjMainPlayer;
				objMainPlayer.EnableMainPlayer();
				base.enabled = false;
				objMainPlayer.transform.parent = null;
				objMainPlayer.transform.position = this.PlayerDiePos.position;
				objMainPlayer.transform.rotation = this.PlayerDiePos.rotation;
				objMainPlayer.EnableNavMeshAgent();
				objMainPlayer.rigidbody.isKinematic = false;
				CameraController cameraController = objMainPlayer.CameraController;
				cameraController.LerpBackToPlayer(1f, null);
				local_character_attack.request request = new local_character_attack.request();
				request.characterId = objMainPlayer.ServerId;
				request.damage = objMainPlayer.AttributeData.HP;
				NetLogic.GetInstance().Send<Protocol.local_character_attack>(request, null);
				SingletonUnity<CitySimController>.Instance.ResetPlayerUI();
				SingletonUnity<CitySimController>.Instance.PlayerCar = null;
				objMainPlayer.OnDie();
				this.InsidePlayer = null;
			}
		}
		else if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.DESTROY_CAR))
		{
			local_npc_die.request request2 = new local_npc_die.request();
			request2.type = 1L;
			NetLogic.GetInstance().Send<Protocol.local_npc_die>(request2, null);
		}
		this.DisableCar();
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0004E450 File Offset: 0x0004C650
	public void UpdateColor()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ColorData colorDataById = DataManager.GetColorDataById(this.mCurMountData.DefaultColorId);
		List<Material> list = new List<Material>();
		list.Add(this.CarBodyRoot.gameObject.renderer.sharedMaterial);
		for (int i = 0; i < this.CarBodyRoot.childCount; i++)
		{
			list.Add(this.CarBodyRoot.GetChild(i).gameObject.renderer.sharedMaterial);
		}
		Shader shader = Shader.Find(this.CarBodyRoot.gameObject.renderer.sharedMaterial.shader.name);
		if (shader != null)
		{
			for (int j = 0; j < list.Count; j++)
			{
				list[j].shader = shader;
			}
		}
		else
		{
			Debug.Log("No Shader");
		}
		for (int k = 0; k < list.Count; k++)
		{
			list[k].SetColor("_Color", colorDataById.CShaderColor);
			list[k].SetColor("_RimColor", colorDataById.CShaderRimColor);
			list[k].SetFloat("_ReflAmount", colorDataById.ShaderReflAmount);
			list[k].SetFloat("_RimPower", colorDataById.ShaderRimPower);
		}
	}

	// Token: 0x04000950 RID: 2384
	public static int POLICECAR_DAMAGE = 50;

	// Token: 0x04000951 RID: 2385
	public static int STATICBLOCK_DAMAGE = 25;

	// Token: 0x04000952 RID: 2386
	public static int POLICENEAR_DAMAGE = 5;

	// Token: 0x04000953 RID: 2387
	public static int PLAYERCAR_MAXHP = 500;

	// Token: 0x04000954 RID: 2388
	private bool DriftFlag;

	// Token: 0x04000955 RID: 2389
	private bool ExitDirftFlag;

	// Token: 0x04000956 RID: 2390
	public float DriftAngleEnhance = 1.5f;

	// Token: 0x04000957 RID: 2391
	public float DriftBrakeReduce = 2f;

	// Token: 0x04000958 RID: 2392
	public float DriftStartMinSpeed = 10f;

	// Token: 0x04000959 RID: 2393
	public float DriftStartMinAnglePercent = 0.2f;

	// Token: 0x0400095A RID: 2394
	public float DriftExitMinSpeed = 10f;

	// Token: 0x0400095B RID: 2395
	public float DriftExitMinAnglePercent = 0.2f;

	// Token: 0x0400095C RID: 2396
	public float DriftMaxMeshAngle = 20f;

	// Token: 0x0400095D RID: 2397
	public float DriftAngleSpeed = 60f;

	// Token: 0x0400095E RID: 2398
	public float SkidMaskIntensityReduce = 0.6f;

	// Token: 0x0400095F RID: 2399
	public float MaxZAngle = 5f;

	// Token: 0x04000960 RID: 2400
	private float mMinDriftSpeedPercent;

	// Token: 0x04000961 RID: 2401
	public Transform DummyPlayerPoint;

	// Token: 0x04000962 RID: 2402
	public Transform DummyNPCPoint;

	// Token: 0x04000963 RID: 2403
	public Vector3 DefaultNpcPos;

	// Token: 0x04000964 RID: 2404
	public Quaternion DefaultNpcRotation;

	// Token: 0x04000965 RID: 2405
	public Transform CarDoorView;

	// Token: 0x04000966 RID: 2406
	public Transform StartCamView;

	// Token: 0x04000967 RID: 2407
	public Transform WinCamView;

	// Token: 0x04000968 RID: 2408
	private ParticleSystem mLSkidSmoke;

	// Token: 0x04000969 RID: 2409
	private ParticleSystem mRSkidSmoke;

	// Token: 0x0400096A RID: 2410
	protected LensFlareSensor BLLight;

	// Token: 0x0400096B RID: 2411
	protected LensFlareSensor BRLight;

	// Token: 0x0400096C RID: 2412
	public Vector3 defaultColliderSize = Vector3.zero;

	// Token: 0x0400096D RID: 2413
	private Skidmarks mSkidmarks;

	// Token: 0x0400096E RID: 2414
	private MountData mCurMountData;

	// Token: 0x0400096F RID: 2415
	private GameObject mFrontTrigger;

	// Token: 0x04000970 RID: 2416
	public ParticleSystem SmokeParticle;

	// Token: 0x04000971 RID: 2417
	public ParticleSystem ExplosionParticle;

	// Token: 0x04000972 RID: 2418
	private Transform DieCamView;

	// Token: 0x04000973 RID: 2419
	private Transform PlayerDiePos;

	// Token: 0x04000974 RID: 2420
	private SoundManager soundManager;

	// Token: 0x04000975 RID: 2421
	private float creshCount;

	// Token: 0x04000976 RID: 2422
	private Vector3 tempVector3;

	// Token: 0x04000977 RID: 2423
	private int lastFLSkidmark;

	// Token: 0x04000978 RID: 2424
	private int lastFRSkidmark;

	// Token: 0x04000979 RID: 2425
	private int lastBLSkidmark;

	// Token: 0x0400097A RID: 2426
	private int lastBRSkidmark;

	// Token: 0x0400097B RID: 2427
	private float curDriftTargetMeshAngle;

	// Token: 0x0400097C RID: 2428
	private float curDriftAngleSpeed;

	// Token: 0x0400097D RID: 2429
	private float skidMaskIntensityPercent;

	// Token: 0x0400097E RID: 2430
	private bool mFreezeCarFlag;

	// Token: 0x0400097F RID: 2431
	private bool mStopCarFlag;

	// Token: 0x04000980 RID: 2432
	private float ftime;

	// Token: 0x04000981 RID: 2433
	private Vector3 mLastPosition = Vector3.zero;

	// Token: 0x04000982 RID: 2434
	private float timeWait = 0.2f;

	// Token: 0x04000983 RID: 2435
	private move.request request = new move.request();

	// Token: 0x04000984 RID: 2436
	private position pos = new position();

	// Token: 0x04000985 RID: 2437
	private int mCarEngineSoundId = 36;

	// Token: 0x04000986 RID: 2438
	private AudioSource mCarEngineAudio;

	// Token: 0x04000987 RID: 2439
	private int mCarBrakeSoundId = 38;

	// Token: 0x04000988 RID: 2440
	private int mCarDriftSoundId = 39;

	// Token: 0x04000989 RID: 2441
	private int mHitCarSoundId = 41;

	// Token: 0x0400098A RID: 2442
	private float mHitCarSoundVolume = 1f;

	// Token: 0x0400098B RID: 2443
	private float mBrakeSoundVolume = 1f;
}
