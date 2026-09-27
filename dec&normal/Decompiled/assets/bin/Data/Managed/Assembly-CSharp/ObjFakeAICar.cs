using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class ObjFakeAICar : MonoBehaviour
{
	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0004AEAC File Offset: 0x000490AC
	// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0004AEB4 File Offset: 0x000490B4
	public long ServerId
	{
		get
		{
			return this.mServerId;
		}
		set
		{
			this.mServerId = value;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0004AEC0 File Offset: 0x000490C0
	public ObjPlayerCar PlayerCar
	{
		get
		{
			return this.mPlayerCar;
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0004AEC8 File Offset: 0x000490C8
	private void Awake()
	{
		this.mCacheTransform = base.transform;
		this.mNavMeshAgent = base.gameObject.GetComponent<NavMeshAgent>();
		if (this.mNavMeshAgent == null)
		{
			this.mNavMeshAgent = base.gameObject.AddComponent<NavMeshAgent>();
		}
		this.mNavMeshAgent.speed = this.mSpeed;
		this.mNavMeshAgent.acceleration = this.mAccSpeed;
		this.mNavMeshAgent.radius = 3f;
		this.mNavMeshAgent.obstacleAvoidanceType = 0;
		this.mPlayerCar = base.gameObject.GetComponent<ObjPlayerCar>();
		this.InitCar();
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0004AF6C File Offset: 0x0004916C
	public void InitCar()
	{
		if (this.mPlayerCar.MeshRoot != null)
		{
			this.FLPos = this.mPlayerCar.FLWheel.wheelTrs.localPosition;
			this.FRPos = this.mPlayerCar.FRWheel.wheelTrs.localPosition;
			this.BLPos = this.mPlayerCar.BLWheel.wheelTrs.localPosition;
			this.BRPos = this.mPlayerCar.BRWheel.wheelTrs.localPosition;
		}
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0004AFFC File Offset: 0x000491FC
	public void EnableFackAICar()
	{
		this.mNavMeshAgent.enabled = true;
		base.enabled = true;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0004B014 File Offset: 0x00049214
	public void DisableFakeAICar()
	{
		this.StopMove();
		this.mNavMeshAgent.enabled = false;
		base.enabled = false;
		this.IsMoving = false;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0004B044 File Offset: 0x00049244
	public void Reset(CityPathPointData curTargetPoint, CityPathPointData prePoint, float wayDis, CitySimController cityCtl)
	{
		this.IsStaticCar = false;
		this.stopFlag = false;
		base.enabled = true;
		this.mNavMeshAgent.enabled = true;
		this.mPlayerCar.enabled = false;
		this.mPlayerCar.rigidbody.isKinematic = false;
		this.mPlayerCar.IsDie = false;
		this.mCityCtl = cityCtl;
		UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.ExplosionParticle.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.SmokeParticle.gameObject, false);
		this.mCurTargetPoint = curTargetPoint;
		this.mPreTargetPoint = prePoint;
		this.mWayDis = wayDis;
		if (this.mPlayerCar.DummyNPCPoint != null)
		{
			this.mPlayerCar.DummyNPCPoint.transform.parent = this.mPlayerCar.MeshRoot.transform;
			this.mPlayerCar.DummyNPCPoint.transform.localPosition = this.mPlayerCar.DefaultNpcPos;
			this.mPlayerCar.DummyNPCPoint.transform.localRotation = this.mPlayerCar.DefaultNpcRotation;
		}
		if (this.mPlayerCar.LLight != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.LLight.gameObject, false);
		}
		if (this.mPlayerCar.RLight != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.RLight.gameObject, false);
		}
		this.mAngleSpeed = this.mSpeed * 57.29578f / this.mPlayerCar.FLWheel.WheelRadius / 2f;
		this.mPlayerCar.MeshRoot.SampleAnimation(this.mPlayerCar.MeshRoot.animation.GetClip("GTACheBaoZa_Animation"), 0f);
		this.mPlayerCar.FLWheel.wheelTrs.localPosition = this.FLPos;
		this.mPlayerCar.FRWheel.wheelTrs.localPosition = this.FRPos;
		this.mPlayerCar.BLWheel.wheelTrs.localPosition = this.BLPos;
		this.mPlayerCar.BRWheel.wheelTrs.localPosition = this.BRPos;
		this.MoveTo(this.GetTargetPos(this.mCurTargetPoint, this.mPreTargetPoint), 5f, new ObjFakeAICar.AICarArriveFinsh(this.MoveNextPoint));
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0004B2A4 File Offset: 0x000494A4
	public void ResetStaticCar()
	{
		this.IsStaticCar = true;
		this.stopFlag = false;
		base.enabled = false;
		this.mNavMeshAgent.enabled = false;
		this.mPlayerCar.enabled = false;
		this.mPlayerCar.rigidbody.isKinematic = false;
		this.mPlayerCar.IsDie = false;
		this.mCityCtl = null;
		if (this.mPlayerCar.MeshRoot == null)
		{
			return;
		}
		this.mPlayerCar.ExplosionParticle.Stop();
		this.mPlayerCar.SmokeParticle.Stop();
		this.mCurTargetPoint = null;
		this.mPreTargetPoint = null;
		this.mWayDis = 0f;
		if (this.mPlayerCar.DummyNPCPoint != null)
		{
			this.mPlayerCar.DummyNPCPoint.transform.parent = this.mPlayerCar.MeshRoot.transform;
			this.mPlayerCar.DummyNPCPoint.transform.localPosition = this.mPlayerCar.DefaultNpcPos;
			this.mPlayerCar.DummyNPCPoint.transform.localRotation = this.mPlayerCar.DefaultNpcRotation;
		}
		if (this.mPlayerCar.LLight != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.LLight.gameObject, false);
		}
		if (this.mPlayerCar.RLight != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mPlayerCar.RLight.gameObject, false);
		}
		this.mAngleSpeed = this.mSpeed * 57.29578f / this.mPlayerCar.FLWheel.WheelRadius / 2f;
		if (this.mPlayerCar.CurMountData.IsShowPlayer == 0)
		{
			this.mPlayerCar.MeshRoot.SampleAnimation(this.mPlayerCar.MeshRoot.animation.GetClip("GTACheBaoZa_Animation"), 0f);
		}
		else
		{
			this.mPlayerCar.MeshRoot.SampleAnimation(this.mPlayerCar.MeshRoot.animation.GetClip("GTACheBaoZa_Animation"), 0f);
			this.mPlayerCar.UpdateColor();
		}
		this.mPlayerCar.FLWheel.wheelTrs.localPosition = this.FLPos;
		this.mPlayerCar.FRWheel.wheelTrs.localPosition = this.FRPos;
		this.mPlayerCar.BLWheel.wheelTrs.localPosition = this.BLPos;
		this.mPlayerCar.BRWheel.wheelTrs.localPosition = this.BRPos;
		this.mPlayerCar.BaseDisableCar();
		this.mPlayerCar.rigidbody.useGravity = true;
		this.mPlayerCar.rigidbody.isKinematic = false;
		this.mPlayerCar.rigidbody.velocity = Vector3.zero;
		this.mPlayerCar.rigidbody.angularVelocity = Vector3.zero;
		BoxCollider component = this.mPlayerCar.CarBodyRoot.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.size = new Vector3(this.mPlayerCar.defaultColliderSize.x, this.mPlayerCar.defaultColliderSize.y + this.mPlayerCar.FLWheel.WheelRadius, this.mPlayerCar.defaultColliderSize.z);
		}
		this.onArrivePoint = null;
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x0004B604 File Offset: 0x00049804
	public void MoveNextPoint(ObjFakeAICar aiCar)
	{
		this.IsMoving = false;
		if (this.mPlayerCar.IsDie)
		{
			return;
		}
		if (!base.enabled)
		{
			return;
		}
		if (this.mCurTargetPoint == null || this.mPreTargetPoint == null)
		{
			return;
		}
		if (this.mCurTargetPoint.IsCross && !this.mPreTargetPoint.IsCross)
		{
			if ((this.mCurTargetPoint.IsNsCross && CitySimController.CurRoadState != ROAD_STATE.NS_STRAIT_PASS && CitySimController.CurRoadState != ROAD_STATE.NS_TURN_PASS) || (!this.mCurTargetPoint.IsNsCross && CitySimController.CurRoadState != ROAD_STATE.EW_STRAIT_PASS && CitySimController.CurRoadState != ROAD_STATE.EW_TURN_PASS))
			{
				return;
			}
			bool flag = CitySimController.CurRoadState == ROAD_STATE.EW_STRAIT_PASS || CitySimController.CurRoadState == ROAD_STATE.NS_STRAIT_PASS;
			if (flag)
			{
				if (this.mCurTargetPoint.LinkPointIndex[0] == this.mPreTargetPoint.SelfIndex)
				{
					this.mPreTargetPoint = this.mCurTargetPoint;
					if (this.mCurTargetPoint.LinkPointIndex[1] != -1)
					{
						this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[1]];
					}
					else if (this.mWayDis > 3f)
					{
						if (this.mCurTargetPoint.LinkPointIndex[2] != -1)
						{
							this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
						}
						else
						{
							this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
						}
					}
					else if (this.mCurTargetPoint.LinkPointIndex[3] != -1)
					{
						this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
					}
					else
					{
						this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
					}
				}
				else
				{
					this.mPreTargetPoint = this.mCurTargetPoint;
					if (this.mCurTargetPoint.LinkPointIndex[0] != -1)
					{
						this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[0]];
					}
					else if (this.mWayDis > 3f)
					{
						if (this.mCurTargetPoint.LinkPointIndex[2] != -1)
						{
							this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
						}
						else
						{
							this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
						}
					}
					else if (this.mCurTargetPoint.LinkPointIndex[3] != -1)
					{
						this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
					}
					else
					{
						this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
					}
				}
			}
			else
			{
				if (this.mWayDis > 3f)
				{
					if (this.mCurTargetPoint.LinkPointIndex[2] == -1)
					{
						return;
					}
					this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
				}
				else
				{
					if (this.mCurTargetPoint.LinkPointIndex[3] == -1)
					{
						return;
					}
					this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
				}
				this.mPreTargetPoint = this.mCurTargetPoint;
			}
		}
		else if (this.mCurTargetPoint.LinkPointIndex[0] == this.mPreTargetPoint.SelfIndex)
		{
			this.mPreTargetPoint = this.mCurTargetPoint;
			if (this.mCurTargetPoint.LinkPointIndex[1] != -1)
			{
				this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[1]];
			}
			else if (this.mWayDis > 3f)
			{
				if (this.mCurTargetPoint.LinkPointIndex[2] != -1)
				{
					this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
				}
				else
				{
					this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
				}
			}
			else if (this.mCurTargetPoint.LinkPointIndex[3] != -1)
			{
				this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[3]];
			}
			else
			{
				this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[2]];
			}
		}
		else
		{
			this.mPreTargetPoint = this.mCurTargetPoint;
			if (this.mCurTargetPoint.LinkPointIndex[0] == -1)
			{
				return;
			}
			this.mCurTargetPoint = this.mCityCtl.PointDataList[this.mCurTargetPoint.LinkPointIndex[0]];
		}
		this.MoveTo(this.GetTargetPos(this.mCurTargetPoint, this.mPreTargetPoint), 5f, new ObjFakeAICar.AICarArriveFinsh(this.MoveNextPoint));
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0004BB80 File Offset: 0x00049D80
	private Vector3 GetTargetPos(CityPathPointData targetPoint, CityPathPointData prePoint)
	{
		if (targetPoint.IsFourLines)
		{
			if (!prePoint.IsFourLines)
			{
				this.mWayDis = this.FourWayCarDis2;
			}
		}
		else if (this.mWayDis > 3f)
		{
			this.mWayDis = this.carDis1;
		}
		if (Vector3.Angle(base.transform.forward, targetPoint.PointForward) > 90f)
		{
			return targetPoint.PointPos - targetPoint.PointRight * this.mWayDis;
		}
		return targetPoint.PointPos + targetPoint.PointRight * this.mWayDis;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0004BC2C File Offset: 0x00049E2C
	public void ContinueMove()
	{
		if (this.mPlayerCar.IsDie)
		{
			return;
		}
		if (this.mCurTargetPoint.IsCross && !this.mPreTargetPoint.IsCross && ((this.mCurTargetPoint.IsNsCross && CitySimController.CurRoadState != ROAD_STATE.NS_STRAIT_PASS && CitySimController.CurRoadState != ROAD_STATE.NS_TURN_PASS) || (!this.mCurTargetPoint.IsNsCross && CitySimController.CurRoadState != ROAD_STATE.EW_STRAIT_PASS && CitySimController.CurRoadState != ROAD_STATE.EW_TURN_PASS)))
		{
			return;
		}
		this.MoveTo(this.GetTargetPos(this.mCurTargetPoint, this.mPreTargetPoint), 5f, new ObjFakeAICar.AICarArriveFinsh(this.MoveNextPoint));
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0004BCE0 File Offset: 0x00049EE0
	public void MoveTo(Vector3 pos, float stopRange = 1f, ObjFakeAICar.AICarArriveFinsh arriveFinsh = null)
	{
		if (this.mPlayerCar.IsDie)
		{
			return;
		}
		if (this.stopFlag)
		{
			return;
		}
		this.onArrivePoint = arriveFinsh;
		this.mStopRange = stopRange;
		this.mTargetPos = pos;
		if (this.CheckArrive(pos))
		{
			this.StopMove();
			return;
		}
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.acceleration = this.mAccSpeed;
			this.mNavMeshAgent.stoppingDistance = 0.1f;
			this.mNavMeshAgent.SetDestination(this.mTargetPos);
		}
		this.IsMoving = true;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0004BD90 File Offset: 0x00049F90
	public void DisactiveTargetArriveFinish()
	{
		this.onArrivePoint = null;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0004BD9C File Offset: 0x00049F9C
	public void StopMove()
	{
		this.IsMoving = false;
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.acceleration = this.mAccSpeed * 6f;
			this.mNavMeshAgent.Stop();
		}
		if (this.onArrivePoint != null)
		{
			this.tempTargetArriveFinish = this.onArrivePoint;
			this.onArrivePoint = null;
			this.tempTargetArriveFinish(this);
		}
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0004BE20 File Offset: 0x0004A020
	private void Update()
	{
		if (this.mPlayerCar.IsDie)
		{
			if (base.enabled)
			{
				this.DisableFakeAICar();
			}
			return;
		}
		if (Time.timeScale < 1E-45f)
		{
			return;
		}
		this.UpdateMove();
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0004BE68 File Offset: 0x0004A068
	public void UpdateMove()
	{
		if (Time.deltaTime < 1E-45f)
		{
			return;
		}
		if (this.IsMoving)
		{
			float num = VectorXZ.Distance(new VectorXZ(this.mTargetPos.x, this.mTargetPos.z), new VectorXZ(this.mCacheTransform.position.x, this.mCacheTransform.position.z));
			if (num - this.mStopRange <= 0f)
			{
				this.StopMove();
				return;
			}
			if (num - this.mSpeed * Time.deltaTime <= 0f)
			{
				this.mCacheTransform.position = this.mTargetPos;
				this.StopMove();
				return;
			}
			this.steerAngle = Mathf.Lerp(this.steerAngle, Mathf.Clamp(-Mathf.DeltaAngle(this.mCacheTransform.eulerAngles.y, this.preAngleY) / Time.deltaTime / 4f, -50f, 50f), 0.8f);
			this.steerList[this.curIndex] = this.steerAngle;
			this.steerAngle = this.GetAVE();
			this.curIndex = (this.curIndex + 1) % this.steerList.Length;
			this.wheelAngle += this.mAngleSpeed * Time.deltaTime;
			this.mPlayerCar.FLWheel.wheelTrs.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle, 0f));
			this.mPlayerCar.FRWheel.wheelTrs.localRotation = Quaternion.Euler(new Vector3(-this.wheelAngle, this.steerAngle + 180f, 0f));
			this.mPlayerCar.BLWheel.wheelTrs.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, 0f, 0f));
			this.mPlayerCar.BRWheel.wheelTrs.localRotation = Quaternion.Euler(new Vector3(-this.wheelAngle, 180f, 0f));
			this.preAngleY = this.mCacheTransform.eulerAngles.y;
		}
		else
		{
			this.steerList[this.curIndex] = 0f;
			this.steerAngle = this.GetAVE();
			this.curIndex = (this.curIndex + 1) % this.steerList.Length;
			this.mPlayerCar.FLWheel.wheelTrs.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle, 0f));
			this.mPlayerCar.FRWheel.wheelTrs.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle + 180f, 0f));
		}
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0004C144 File Offset: 0x0004A344
	private float GetAVE()
	{
		float num = 0f;
		for (int i = 0; i < this.steerList.Length; i++)
		{
			num += this.steerList[i];
		}
		return num / (float)this.steerList.Length;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0004C188 File Offset: 0x0004A388
	private bool CheckArrive(Vector3 target)
	{
		float num = Vector3.SqrMagnitude(target - this.mCacheTransform.position);
		return num <= this.mStopRange * this.mStopRange;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
	public void RecycleSelf()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		Singleton<ObjManager>.Instance.RecycleFakeAICar(this);
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0004C1E0 File Offset: 0x0004A3E0
	private void OnCollisionEnter(Collision other)
	{
		if (base.enabled && other.gameObject.CompareTag("PlayerCar"))
		{
			this.DisactiveTargetArriveFinish();
			this.StopMove();
			this.stopFlag = true;
		}
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0004C220 File Offset: 0x0004A420
	private void OnCollisionExit(Collision other)
	{
		if (base.enabled && other.gameObject.CompareTag("PlayerCar"))
		{
			if (this.timerHandle != null)
			{
				this.timerHandle.Cancel();
			}
			vp_Timer.In(5f, delegate()
			{
				if (this.stopFlag)
				{
					this.stopFlag = false;
					if (base.enabled)
					{
						this.ContinueMove();
					}
				}
			}, this.timerHandle);
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0004C280 File Offset: 0x0004A480
	private void OnDisable()
	{
		this.timerHandle.Cancel();
	}

	// Token: 0x0400092E RID: 2350
	protected long mServerId;

	// Token: 0x0400092F RID: 2351
	public bool IsMoving;

	// Token: 0x04000930 RID: 2352
	public string ModelId = string.Empty;

	// Token: 0x04000931 RID: 2353
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x04000932 RID: 2354
	private float mSpeed = 10f;

	// Token: 0x04000933 RID: 2355
	private float mAccSpeed = 5f;

	// Token: 0x04000934 RID: 2356
	private CityPathPointData mCurTargetPoint;

	// Token: 0x04000935 RID: 2357
	private CityPathPointData mPreTargetPoint;

	// Token: 0x04000936 RID: 2358
	private ObjFakeAICar.AICarArriveFinsh onArrivePoint;

	// Token: 0x04000937 RID: 2359
	private ObjFakeAICar.AICarArriveFinsh tempTargetArriveFinish;

	// Token: 0x04000938 RID: 2360
	private float mStopRange;

	// Token: 0x04000939 RID: 2361
	private Vector3 mTargetPos;

	// Token: 0x0400093A RID: 2362
	private Transform mCacheTransform;

	// Token: 0x0400093B RID: 2363
	private float mWayDis;

	// Token: 0x0400093C RID: 2364
	private ObjPlayerCar mPlayerCar;

	// Token: 0x0400093D RID: 2365
	private CitySimController mCityCtl;

	// Token: 0x0400093E RID: 2366
	public int CheckIndex = -1;

	// Token: 0x0400093F RID: 2367
	private bool stopFlag;

	// Token: 0x04000940 RID: 2368
	private Vector3 FLPos;

	// Token: 0x04000941 RID: 2369
	private Vector3 FRPos;

	// Token: 0x04000942 RID: 2370
	private Vector3 BLPos;

	// Token: 0x04000943 RID: 2371
	private Vector3 BRPos;

	// Token: 0x04000944 RID: 2372
	public bool IsStaticCar;

	// Token: 0x04000945 RID: 2373
	private float carDis1 = 2.3f;

	// Token: 0x04000946 RID: 2374
	private float carDis2 = 5.5f;

	// Token: 0x04000947 RID: 2375
	private float FourWayCarDis1 = 2.8f;

	// Token: 0x04000948 RID: 2376
	private float FourWayCarDis2 = 6.7f;

	// Token: 0x04000949 RID: 2377
	private float preAngleY;

	// Token: 0x0400094A RID: 2378
	private float steerAngle;

	// Token: 0x0400094B RID: 2379
	private float wheelAngle;

	// Token: 0x0400094C RID: 2380
	private float[] steerList = new float[6];

	// Token: 0x0400094D RID: 2381
	private int curIndex;

	// Token: 0x0400094E RID: 2382
	private float mAngleSpeed;

	// Token: 0x0400094F RID: 2383
	private vp_Timer.Handle timerHandle = new vp_Timer.Handle();

	// Token: 0x02000AC4 RID: 2756
	// (Invoke) Token: 0x06004F99 RID: 20377
	public delegate void AICarArriveFinsh(ObjFakeAICar aiCar);
}
