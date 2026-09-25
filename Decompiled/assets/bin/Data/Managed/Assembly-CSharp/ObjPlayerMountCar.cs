using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class ObjPlayerMountCar : MonoBehaviour
{
	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0004E608 File Offset: 0x0004C808
	// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0004E610 File Offset: 0x0004C810
	public RideMountData CurRideMountData
	{
		get
		{
			return this.mCurRideMountData;
		}
		set
		{
			this.mCurRideMountData = value;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0004E61C File Offset: 0x0004C81C
	// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0004E624 File Offset: 0x0004C824
	public BundleManager.LoadModelData LoadingMeshData
	{
		get
		{
			return this.mLoadingMeshData;
		}
		set
		{
			this.mLoadingMeshData = value;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0004E630 File Offset: 0x0004C830
	// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0004E638 File Offset: 0x0004C838
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

	// Token: 0x06000A82 RID: 2690 RVA: 0x0004E644 File Offset: 0x0004C844
	public void Reset(RideMountData data)
	{
		if (this.ResetFlag)
		{
			return;
		}
		this.mCurRideMountData = data;
		if (this.MeshRoot != null)
		{
			this.ResetFlag = true;
			this.ChangeColor(data.mColorData);
			if (this.mCurRideMountData.Player != null)
			{
				NGUITools.SetLayer(this.MeshRoot.gameObject, this.mCurRideMountData.Player.gameObject.layer);
				this.mCurRideMountData.Player.AnimationLogic.AnimaObj.transform.parent = this.MeshRoot.PlayerRoot;
				this.mCurRideMountData.Player.AnimationLogic.AnimaObj.transform.localPosition = Vector3.zero;
				this.mCurRideMountData.Player.AnimationLogic.AnimaObj.transform.localRotation = Quaternion.identity;
				this.mRootObj = this.mCurRideMountData.Player.CacheTransform;
				this.mAngleSpeed = this.mCurRideMountData.Player.NavMeshAgent.speed * 57.29578f / this.MeshRoot.WheelRadius / 2f;
				this.mCurRideMountData.Player.CurAnimationState = GameDefine.ANIMATIONSTATE.DRIVING;
				this.mCurRideMountData.Player.CurPlayerState = PLAYER_STATE.DRIVING;
				this.mCurRideMountData.Player.LoadingMountFlag = false;
			}
			if (this.mCurRideMountData.PlayerCar != null)
			{
				this.OnPlayerCarLoadDone();
			}
		}
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0004E7D0 File Offset: 0x0004C9D0
	public void UpdateSpeed()
	{
		if (this.mCurRideMountData != null && this.mCurRideMountData.Player != null && this.MeshRoot != null && Mathf.Abs(this.lastSpeed - this.mCurRideMountData.Player.NavMeshAgent.speed) > 1E-45f)
		{
			this.mAngleSpeed = this.mCurRideMountData.Player.NavMeshAgent.speed * 57.29578f / this.MeshRoot.WheelRadius / 2f;
			this.lastSpeed = this.mCurRideMountData.Player.NavMeshAgent.speed;
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0004E888 File Offset: 0x0004CA88
	public void UpdateSetSpeed(float speed)
	{
		if (this.mCurRideMountData != null && this.mCurRideMountData.Player != null && this.MeshRoot != null && Mathf.Abs(this.lastSpeed - speed) > 1E-45f)
		{
			this.mAngleSpeed = speed * 57.29578f / this.MeshRoot.WheelRadius / 2f;
			this.lastSpeed = speed;
		}
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0004E904 File Offset: 0x0004CB04
	private void OnPlayerCarLoadDone()
	{
		this.mCurRideMountData.PlayerCar.OnMeshLoadDone(this.MeshRoot);
		this.mCurRideMountData.Player.CacheTransform.parent = this.MeshRoot.PlayerRoot;
		this.mCurRideMountData.Player.CacheTransform.localPosition = Vector3.zero;
		this.mCurRideMountData.Player.CacheTransform.localRotation = Quaternion.identity;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0004E97C File Offset: 0x0004CB7C
	public void OnMeshLoadDone(MountCarMeshRoot meshRoot, RideMountData data)
	{
		this.MeshRoot = meshRoot;
		this.MeshRoot.transform.parent = base.transform;
		this.MeshRoot.transform.localPosition = Vector3.zero;
		this.MeshRoot.transform.localRotation = Quaternion.identity;
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(meshRoot.gameObject, true);
			this.Reset(data);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(meshRoot.gameObject, false);
		}
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0004EA04 File Offset: 0x0004CC04
	public void ChangeColor(ColorData data)
	{
		if (data != null && this.MeshRoot != null)
		{
			this.MeshRoot.ChangeColor(data);
		}
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0004EA3C File Offset: 0x0004CC3C
	private void Update()
	{
		if (this.MeshRoot != null)
		{
			if (this.mCurRideMountData != null && this.mCurRideMountData.Player != null && this.mCurRideMountData.Player.IsMoving)
			{
				this.steerAngle = Mathf.Lerp(this.steerAngle, Mathf.Clamp(-Mathf.DeltaAngle(this.mRootObj.eulerAngles.y, this.preAngleY) / Time.deltaTime / 4f, -50f, 50f), 0.8f);
				this.steerList[this.curIndex] = this.steerAngle;
				this.steerAngle = this.GetAVE();
				this.curIndex = (this.curIndex + 1) % this.steerList.Length;
				this.wheelAngle += this.mAngleSpeed * Time.deltaTime;
				this.MeshRoot.QLWheel.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle, 0f));
				this.MeshRoot.QRWheel.localRotation = Quaternion.Euler(new Vector3(-this.wheelAngle, this.steerAngle + 180f, 0f));
				this.MeshRoot.HLWheel.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, 0f, 0f));
				this.MeshRoot.HRWheel.localRotation = Quaternion.Euler(new Vector3(-this.wheelAngle, 180f, 0f));
				this.preAngleY = this.mRootObj.eulerAngles.y;
			}
			else
			{
				this.steerList[this.curIndex] = 0f;
				this.steerAngle = this.GetAVE();
				this.curIndex = (this.curIndex + 1) % this.steerList.Length;
				this.MeshRoot.QLWheel.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle, 0f));
				this.MeshRoot.QRWheel.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle + 180f, 0f));
			}
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0004EC90 File Offset: 0x0004CE90
	private float GetAVE()
	{
		float num = 0f;
		for (int i = 0; i < this.steerList.Length; i++)
		{
			num += this.steerList[i];
		}
		return num / (float)this.steerList.Length;
	}

	// Token: 0x0400098C RID: 2444
	public MountCarMeshRoot MeshRoot;

	// Token: 0x0400098D RID: 2445
	private float mAngleSpeed;

	// Token: 0x0400098E RID: 2446
	private Transform mRootObj;

	// Token: 0x0400098F RID: 2447
	private RideMountData mCurRideMountData;

	// Token: 0x04000990 RID: 2448
	private BundleManager.LoadModelData mLoadingMeshData;

	// Token: 0x04000991 RID: 2449
	private long mLoadingModelDataId;

	// Token: 0x04000992 RID: 2450
	public bool ResetFlag;

	// Token: 0x04000993 RID: 2451
	private float lastSpeed = -1f;

	// Token: 0x04000994 RID: 2452
	private float preAngleY;

	// Token: 0x04000995 RID: 2453
	private float steerAngle;

	// Token: 0x04000996 RID: 2454
	private float wheelAngle;

	// Token: 0x04000997 RID: 2455
	private float[] steerList = new float[6];

	// Token: 0x04000998 RID: 2456
	private int curIndex;
}
