using System;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class ObjSimpleAICar : ObjCar
{
	// Token: 0x06000A8A RID: 2698 RVA: 0x0004ECD4 File Offset: 0x0004CED4
	private ObjSimpleAICar()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_NPC_CAR;
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0004ED04 File Offset: 0x0004CF04
	private float mCurSpeed
	{
		get
		{
			return this.mCarControl.CurSpeed;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0004ED14 File Offset: 0x0004CF14
	private float mMaxSpeed
	{
		get
		{
			return this.mCarControl.MaxSpeed;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0004ED24 File Offset: 0x0004CF24
	private float mCurMaxSteerAngle
	{
		get
		{
			return this.mCarControl.CurMaxSteerAngle;
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0004ED34 File Offset: 0x0004CF34
	public void InitMeshData(MountCarMeshRoot meshRootData)
	{
		this.mCurMeshData = meshRootData;
		this.FLWheel.wheelTrs = this.mCurMeshData.QLWheel;
		this.FLWheel.Init(this.mCurMeshData.WheelRadius);
		this.FRWheel.wheelTrs = this.mCurMeshData.QRWheel;
		this.FRWheel.Init(this.mCurMeshData.WheelRadius);
		this.BLWheel.wheelTrs = this.mCurMeshData.HLWheel;
		this.BLWheel.Init(this.mCurMeshData.WheelRadius);
		this.BRWheel.wheelTrs = this.mCurMeshData.HRWheel;
		this.BRWheel.Init(this.mCurMeshData.WheelRadius);
		this.CarBodyRoot = this.mCurMeshData.CarBodyRoot;
		GameObject gameObject = this.MeshRoot.transform.FindChild("cheshen").gameObject;
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		component.size = this.mCurMeshData.ColliderSize;
		component.center = this.mCurMeshData.ColliderCenter;
		GameObject gameObject2 = this.MeshRoot.transform.FindChild("FrontCollision").gameObject;
		gameObject2.transform.localPosition = component.center + Vector3.forward * (component.size.z / 2f + 0.18f);
		BoxCollider boxCollider = new GameObject("PlayerCarCollider")
		{
			transform = 
			{
				parent = gameObject.transform.parent,
				localPosition = gameObject.transform.localPosition,
				localRotation = gameObject.transform.localRotation
			}
		}.AddComponent<BoxCollider>();
		boxCollider.center = component.center + Vector3.forward * 1.5f / 2f;
		boxCollider.size = component.size + Vector3.forward * 1.5f;
		boxCollider.isTrigger = true;
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0004EF4C File Offset: 0x0004D14C
	protected new void Init()
	{
		base.Init();
		this.MeshRoot = base.transform.FindChild("MeshRoot").gameObject;
		this.mCarControl.IsAutoDrive = true;
		this.mCarControl.MaxSpeed = 50f;
		this.mCarControl.maxAcceleration = 15f;
		this.mCarControl.maxSteerAngle = 30f;
		this.mNearDamageTimeInterval = 1f / (float)ObjPlayerCar.POLICENEAR_DAMAGE;
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0004EFC8 File Offset: 0x0004D1C8
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0004EFD0 File Offset: 0x0004D1D0
	public void Reset(ObjCarInitData initData)
	{
		base.Reset();
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		base.CacheTransform.position = initData.Pos;
		base.CacheTransform.eulerAngles = initData.Angle;
		base.rigidbody.drag = 0f;
		base.rigidbody.angularDrag = 0f;
		this.DisableCar();
		this.ChaseDoneFlag = false;
		this.mRecycleTimeCount = 0f;
		base.ModelId = initData.CharacterModelId;
		this.mTargetCar = Singleton<ObjManager>.Instance.MainPlayerCar;
		this.mCollideTimeCount = 0;
		this.PoliceFlag = initData.PoliceFlag;
		this.mNearDamageTimeCount = 0f;
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.mPoliceCarSoundId, 1f, null);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0004F09C File Offset: 0x0004D29C
	public void SetPath(CarPath path, int curIndex)
	{
		this.mPath = path;
		this.mCurPathIndex = curIndex;
		this.mCurTargetPos = this.mPath.PathPointList[this.mCurPathIndex].transform.position;
		this.mTargetSpeed = this.mPath.PathPointList[this.mCurPathIndex].Speed;
		this.mCurTarget = this.mPath.PathPointList[this.mCurPathIndex].transform;
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0004F120 File Offset: 0x0004D320
	private bool CheckArriveTarget()
	{
		return (this.mCurTargetPos - base.transform.position).sqrMagnitude < 16f;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0004F158 File Offset: 0x0004D358
	private void ChaseDone()
	{
		if (!this.ChaseDoneFlag)
		{
			this.ChaseDoneFlag = true;
			base.rigidbody.drag = 10f;
			base.rigidbody.angularDrag = 10f;
			Singleton<ObjManager>.Instance.StopPoliceSound();
		}
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0004F1A4 File Offset: 0x0004D3A4
	private void OnArriveTraget()
	{
		if (this.mCurPathIndex < this.mPath.PathPointList.Count - 1)
		{
			this.mCurPathIndex++;
			this.mCurTargetPos = this.mPath.PathPointList[this.mCurPathIndex].transform.position;
			if (this.mCurPathIndex - 1 >= 0)
			{
				this.mTargetSpeed = this.mPath.PathPointList[this.mCurPathIndex - 1].Speed;
			}
			this.mCurTarget = this.mPath.PathPointList[this.mCurPathIndex].transform;
		}
		else
		{
			this.ChaseDone();
		}
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0004F260 File Offset: 0x0004D460
	private new void FixedUpdate()
	{
		base.FixedUpdate();
		this.UpdateMove();
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0004F270 File Offset: 0x0004D470
	private new void Update()
	{
		base.Update();
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0004F278 File Offset: 0x0004D478
	public bool IsChaseDone
	{
		get
		{
			return this.ChaseDoneFlag;
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0004F280 File Offset: 0x0004D480
	private new void UpdateMove()
	{
		if (this.ChaseDoneFlag)
		{
			if (base.CurSpeed > 0f)
			{
				this.mCarControl.OnPressBrakeBtn(true);
				this.mCarControl.OnPressAccelBtn(false);
			}
			this.mRecycleTimeCount += Time.fixedDeltaTime;
			if (this.mRecycleTimeCount > this.RECYCLE_TIME)
			{
				UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
				Singleton<ObjManager>.Instance.RecycleSimpleAICar(this);
			}
			return;
		}
		if (this.PoliceFlag && Vector3.SqrMagnitude(this.mTargetCar.Position - base.Position) < 400f)
		{
			this.mCurTargetPos = this.mTargetCar.Position + this.mTargetCar.CacheTransform.forward * this.mTargetCar.CurSpeed / 4f;
		}
		else
		{
			if (this.CheckArriveTarget())
			{
				this.OnArriveTraget();
			}
			this.mCurTargetPos = this.mCurTarget.position;
		}
		Vector3 vector = this.mCurTargetPos - base.CacheTransform.position;
		Vector3 vector2 = base.CacheTransform.InverseTransformPoint(this.mCurTargetPos);
		float num = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
		if (this.mCurrentAngle * num < 0f)
		{
			this.mCurrentAngle = 0f;
		}
		if (this.mCurrentAngle < num)
		{
			this.mCurrentAngle += Time.fixedDeltaTime * 15f;
			if (this.mCurrentAngle > num)
			{
				this.mCurrentAngle = num;
			}
		}
		else if (this.mCurrentAngle > num)
		{
			this.mCurrentAngle -= Time.fixedDeltaTime * 15f;
			if (this.mCurrentAngle < num)
			{
				this.mCurrentAngle = num;
			}
		}
		this.mCarControl.SetCarTargetAngle(this.mCurrentAngle);
		if (Mathf.Abs(this.mCurrentAngle) <= this.mCarControl.CurMaxSteerAngle)
		{
			if (this.mCurSpeed < this.mTargetSpeed)
			{
				this.mCarControl.OnPressBrakeBtn(false);
				this.mCarControl.OnPressAccelBtn(true);
			}
			else if (this.mCarControl.CurSpeed > 0f)
			{
				this.mCarControl.OnPressAccelBtn(false);
				this.mCarControl.OnPressBrakeBtn(true);
			}
			else
			{
				this.mCarControl.OnPressAccelBtn(false);
				this.mCarControl.OnPressBrakeBtn(false);
			}
		}
		else
		{
			this.mCarControl.OnPressAccelBtn(false);
			this.mCarControl.OnPressBrakeBtn(true);
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0004F52C File Offset: 0x0004D72C
	private void CheckPlayerCarDistance()
	{
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0004F530 File Offset: 0x0004D730
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("PlayerCar"))
		{
			this.mCollideTimeCount++;
			if (this.mCollideTimeCount >= 1)
			{
				this.ChaseDone();
			}
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0004F568 File Offset: 0x0004D768
	private void OnDisable()
	{
		this.ChaseDone();
	}

	// Token: 0x04000999 RID: 2457
	private bool PoliceFlag;

	// Token: 0x0400099A RID: 2458
	private float mNearDamageTimeCount;

	// Token: 0x0400099B RID: 2459
	private float mNearDamageTimeInterval;

	// Token: 0x0400099C RID: 2460
	private float mCheckDis = 100f;

	// Token: 0x0400099D RID: 2461
	private MountCarMeshRoot mCurMeshData;

	// Token: 0x0400099E RID: 2462
	private int mPoliceCarSoundId = 35;

	// Token: 0x0400099F RID: 2463
	private Vector3 mCurTargetPos;

	// Token: 0x040009A0 RID: 2464
	private Transform mCurTarget;

	// Token: 0x040009A1 RID: 2465
	private float mTargetSpeed;

	// Token: 0x040009A2 RID: 2466
	private ObjPlayerCar mTargetCar;

	// Token: 0x040009A3 RID: 2467
	private float mCurrentAngle;

	// Token: 0x040009A4 RID: 2468
	private bool ChaseDoneFlag;

	// Token: 0x040009A5 RID: 2469
	private float mRecycleTimeCount;

	// Token: 0x040009A6 RID: 2470
	private float RECYCLE_TIME = 10f;

	// Token: 0x040009A7 RID: 2471
	private int mCollideTimeCount;
}
