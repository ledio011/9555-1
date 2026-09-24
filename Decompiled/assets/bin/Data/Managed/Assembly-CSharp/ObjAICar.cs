using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class ObjAICar : ObjCar
{
	// Token: 0x06000A16 RID: 2582 RVA: 0x00049C8C File Offset: 0x00047E8C
	private ObjAICar()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_NPC_CAR;
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00049CC0 File Offset: 0x00047EC0
	private float mCurSpeed
	{
		get
		{
			return this.mCarControl.CurSpeed;
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00049CD0 File Offset: 0x00047ED0
	private float mMaxSpeed
	{
		get
		{
			return this.mCarControl.MaxSpeed;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00049CE0 File Offset: 0x00047EE0
	private float mCurMaxSteerAngle
	{
		get
		{
			return this.mCarControl.CurMaxSteerAngle;
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00049CF0 File Offset: 0x00047EF0
	protected new void Init()
	{
		base.Init();
		this.mLeftFrontRayPos = base.transform.Find("ModelRoot/LeftFrontRayPos");
		this.mRightFrontRayPos = base.transform.Find("ModelRoot/RightFrontRayPos");
		this.mFrontLeftRayPos = base.transform.Find("ModelRoot/FrontLeftRayPos");
		this.mFrontRightRayPos = base.transform.Find("ModelRoot/FrontRightRayPos");
		this.mCarControl.IsAutoDrive = true;
		this.RayDetectLayer = 67108864;
		this.mCarControl.MaxSpeed = 60f;
		this.mCarControl.maxAcceleration = 20f;
		this.mCarControl.maxSteerAngle = 25f;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00049DA8 File Offset: 0x00047FA8
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00049DB0 File Offset: 0x00047FB0
	public void Reset(ObjCarInitData initData)
	{
		base.Reset();
		base.CacheTransform.position = initData.Pos;
		base.CacheTransform.eulerAngles = initData.Angle;
		this.DisableCar();
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00049DEC File Offset: 0x00047FEC
	public void SetPath(CarPath path, int curIndex)
	{
		this.mPath = path;
		this.mCurPathIndex = curIndex;
		this.mCurTargetPos = this.mPath.PathPointList[this.mCurPathIndex].transform.position;
		this.mTargetSpeed = this.mPath.PathPointList[this.mCurPathIndex].Speed;
		this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00049E60 File Offset: 0x00048060
	private bool CheckArriveTarget()
	{
		return (this.mCurTargetPos - base.transform.position).sqrMagnitude < 100f;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00049E98 File Offset: 0x00048098
	private void OnArriveTraget()
	{
		int curPathIndex = this.mPlayerCar.GetCurPathIndex();
		if (this.mCurPathIndex == curPathIndex)
		{
			this.mFollowPathFlag = false;
			this.mCurTargetPos = this.mPlayerCar.Position + this.mPlayerCar.CacheTransform.forward * 20f;
			this.mTargetSpeed = 2.1474836E+09f;
			this.mCurTarget = this.mPlayerCar.CacheTransform;
		}
		else if (this.mCurPathIndex < this.mPath.PathPointList.Count - 1)
		{
			this.mCurPathIndex++;
			if (this.mCurPathIndex == curPathIndex)
			{
				this.mFollowPathFlag = false;
				this.mCurTargetPos = this.mPlayerCar.Position + this.mPlayerCar.CacheTransform.forward * 20f;
				this.mTargetSpeed = 2.1474836E+09f;
				this.mCurTarget = this.mPlayerCar.CacheTransform;
			}
			else
			{
				this.mFollowPathFlag = true;
				this.mCurTargetPos = this.mPath.PathPointList[this.mCurPathIndex].transform.position;
				if (this.mCurPathIndex - 1 >= 0)
				{
					this.mTargetSpeed = this.mPath.PathPointList[this.mCurPathIndex - 1].Speed;
				}
				this.mCurTarget = this.mPath.PathPointList[this.mCurPathIndex].transform;
			}
		}
		else
		{
			Debug.Log("No Path Point Left");
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0004A034 File Offset: 0x00048234
	protected new void FixedUpdate()
	{
		this.UpdateMove();
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0004A03C File Offset: 0x0004823C
	private new void UpdateMove()
	{
		if (this.ChaseDoneFlag)
		{
			return;
		}
		if (!this.mFollowPathFlag)
		{
			this.mCurTargetPos = this.mPlayerCar.Position + this.mPlayerCar.CacheTransform.forward * this.mPlayerCar.CurSpeed;
			this.mTargetSpeed = 2.1474836E+09f;
			this.mCurTarget = this.mPlayerCar.CacheTransform;
			if ((this.mCurTargetPos - this.mTransform.position).sqrMagnitude <= 64f)
			{
				this.ChaseDoneFlag = true;
				this.mTargetSpeed = 0f;
			}
		}
		else if (this.CheckArriveTarget())
		{
			this.OnArriveTraget();
		}
		Vector3 vector = this.mCurTargetPos - base.CacheTransform.position;
		Vector3 vector2 = base.CacheTransform.InverseTransformPoint(this.mCurTargetPos);
		float num = this.ObstacleAvoidanceSteering();
		this.mRayDetectDis = 5f;
		if (!this.mFrontRayDetect)
		{
			num = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
		}
		if (this.mCurrentAngle * num < 0f)
		{
			this.mCurrentAngle = 0f;
		}
		if (this.mCurrentAngle < num)
		{
			this.mCurrentAngle += Time.fixedDeltaTime * 40f;
			if (this.mCurrentAngle > num)
			{
				this.mCurrentAngle = num;
			}
		}
		else if (this.mCurrentAngle > num)
		{
			this.mCurrentAngle -= Time.fixedDeltaTime * 40f;
			if (this.mCurrentAngle < num)
			{
				this.mCurrentAngle = num;
			}
		}
		this.mCarControl.SetCarTargetAngle(this.mCurrentAngle);
		if (this.mBackWardDriving)
		{
			this.mCarControl.OnPressAccelBtn(false);
			this.mCarControl.OnPressBrakeBtn(true);
		}
		else if (this.mFrontDistance < this.mRayDetectDis * 0.5f && this.mCurSpeed > 10f)
		{
			this.mCarControl.OnPressAccelBtn(false);
			this.mCarControl.OnPressBrakeBtn(true);
		}
		else if (this.mCurSpeed < this.mTargetSpeed)
		{
			this.mCarControl.OnPressBrakeBtn(false);
			this.mCarControl.OnPressAccelBtn(true);
		}
		else
		{
			this.mCarControl.OnPressAccelBtn(false);
			this.mCarControl.OnPressBrakeBtn(true);
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0004A2B8 File Offset: 0x000484B8
	private float ObstacleAvoidanceSteering()
	{
		this.mFrontRayDetect = false;
		this.mFrontDistance = this.mRayDetectDis;
		this.mLeftDistance = this.mSideDetectDis;
		this.mRightDistance = this.mSideDetectDis;
		if (Physics.Raycast(this.mFrontLeftRayPos.position, this.mFrontLeftRayPos.forward, ref this.hitInfo, this.mRayDetectDis, this.RayDetectLayer))
		{
			this.mFrontRayDetect = true;
			if (this.mFrontDistance > this.hitInfo.distance)
			{
				this.mFrontDistance = this.hitInfo.distance;
			}
			if (this.hitInfo.distance < this.mLeftDistance)
			{
				this.mLeftDistance = this.hitInfo.distance;
			}
		}
		if (Physics.Raycast(this.mFrontRightRayPos.position, this.mFrontRightRayPos.forward, ref this.hitInfo, this.mRayDetectDis, this.RayDetectLayer))
		{
			this.mFrontRayDetect = true;
			if (this.mFrontDistance > this.hitInfo.distance)
			{
				this.mFrontDistance = this.hitInfo.distance;
			}
			if (this.hitInfo.distance < this.mRightDistance)
			{
				this.mRightDistance = this.hitInfo.distance;
			}
		}
		if (Physics.Raycast(this.mLeftFrontRayPos.position, this.mLeftFrontRayPos.forward, ref this.hitInfo, this.mSideDetectDis, this.RayDetectLayer))
		{
			if (this.mLeftDistance > this.hitInfo.distance)
			{
				this.mLeftDistance = this.hitInfo.distance;
			}
			if (this.mLeftDistance < this.mSideDetectDis / 2f)
			{
				this.mFrontRayDetect = true;
			}
		}
		if (Physics.Raycast(this.mRightFrontRayPos.position, this.mRightFrontRayPos.forward, ref this.hitInfo, this.mSideDetectDis, this.RayDetectLayer))
		{
			if (this.mRightDistance > this.hitInfo.distance)
			{
				this.mRightDistance = this.hitInfo.distance;
			}
			if (this.mRightDistance < this.mSideDetectDis / 2f)
			{
				this.mFrontRayDetect = true;
			}
		}
		float num = this.SteerDecision(this.mLeftDistance, this.mRightDistance, this.mFrontDistance, this.mFrontRayDetect, this.mCurTargetPos);
		if (this.mBackWardDriving)
		{
			if (this.mCurSpeed < 0f)
			{
				if (num > 0f)
				{
					num = -this.mCurMaxSteerAngle;
				}
				else if (num < 0f)
				{
					num = this.mCurMaxSteerAngle;
				}
			}
			if (this.mFrontDistance > 4f || this.mRayDetectDis - this.mFrontDistance < 1E-45f)
			{
				this.mBackWardDriving = false;
			}
		}
		return num;
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0004A59C File Offset: 0x0004879C
	private float SteerDecision(float leftDistance, float rightDistance, float frontDistance, bool frontContact, Vector3 targetPos)
	{
		float mCurMaxSteerAngle = this.mCurMaxSteerAngle;
		float result = 0f;
		float num = 1f;
		float num2 = 1f;
		if (frontContact && frontDistance < 1f)
		{
			this.mBackWardDriving = true;
		}
		if (!frontContact)
		{
			this.mBackDoneFlag = true;
		}
		if (!this.mBackWardDriving)
		{
			if (!this.mBackDoneFlag)
			{
				if (base.transform.InverseTransformPoint(targetPos).x < 0f)
				{
					result = -1f * mCurMaxSteerAngle;
				}
				else
				{
					result = mCurMaxSteerAngle;
				}
				return result;
			}
			if (leftDistance > rightDistance || (frontContact && this.mSideDetectDis - leftDistance < 1E-45f))
			{
				if (frontContact)
				{
					result = -1f * mCurMaxSteerAngle;
				}
				else
				{
					if (rightDistance < this.mSideDetectDis)
					{
						num2 = rightDistance / this.mSideDetectDis;
					}
					result = -1f * mCurMaxSteerAngle * (1f - num2);
				}
			}
			else if (leftDistance < rightDistance || (frontContact && this.mSideDetectDis - rightDistance < 1E-45f))
			{
				if (frontContact)
				{
					result = mCurMaxSteerAngle;
				}
				else
				{
					if (leftDistance < this.mSideDetectDis)
					{
						num = leftDistance / this.mSideDetectDis;
					}
					result = mCurMaxSteerAngle * (1f - num);
				}
			}
		}
		else
		{
			this.mBackDoneFlag = false;
			if (base.transform.InverseTransformPoint(targetPos).x < 0f)
			{
				result = -1f * mCurMaxSteerAngle;
			}
			else
			{
				result = mCurMaxSteerAngle;
			}
		}
		return result;
	}

	// Token: 0x04000908 RID: 2312
	private ObjPlayerCar mPlayerCar;

	// Token: 0x04000909 RID: 2313
	private Vector3 mCurTargetPos;

	// Token: 0x0400090A RID: 2314
	private Transform mCurTarget;

	// Token: 0x0400090B RID: 2315
	private float mTargetSpeed;

	// Token: 0x0400090C RID: 2316
	private bool mFollowPathFlag = true;

	// Token: 0x0400090D RID: 2317
	private float mCurrentAngle;

	// Token: 0x0400090E RID: 2318
	private bool ChaseDoneFlag;

	// Token: 0x0400090F RID: 2319
	private bool mBackWardDriving;

	// Token: 0x04000910 RID: 2320
	private float mRayDetectDis = 20f;

	// Token: 0x04000911 RID: 2321
	private float mSideDetectDis = 10f;

	// Token: 0x04000912 RID: 2322
	private bool mFrontRayDetect;

	// Token: 0x04000913 RID: 2323
	private float mFrontDistance;

	// Token: 0x04000914 RID: 2324
	private float mLeftDistance;

	// Token: 0x04000915 RID: 2325
	private float mRightDistance;

	// Token: 0x04000916 RID: 2326
	private Transform mLeftFrontRayPos;

	// Token: 0x04000917 RID: 2327
	private Transform mRightFrontRayPos;

	// Token: 0x04000918 RID: 2328
	private Transform mFrontLeftRayPos;

	// Token: 0x04000919 RID: 2329
	private Transform mFrontRightRayPos;

	// Token: 0x0400091A RID: 2330
	public LayerMask RayDetectLayer;

	// Token: 0x0400091B RID: 2331
	private RaycastHit hitInfo;

	// Token: 0x0400091C RID: 2332
	private bool mBackDoneFlag = true;
}
