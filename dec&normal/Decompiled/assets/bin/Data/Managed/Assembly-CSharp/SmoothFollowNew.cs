using System;
using UnityEngine;

// Token: 0x02000843 RID: 2115
public class SmoothFollowNew : MonoBehaviour
{
	// Token: 0x06003650 RID: 13904 RVA: 0x000DF244 File Offset: 0x000DD444
	public void SetTarget(CarControl targetCar)
	{
		this.mCarCtl = targetCar;
		this.mCarTransform = targetCar.transform;
		this.mCarRigidbody = targetCar.rigidbody;
		this.UpdatePos();
		this.Update();
	}

	// Token: 0x06003651 RID: 13905 RVA: 0x000DF274 File Offset: 0x000DD474
	private void Update()
	{
		if (this.mCarTransform == null || this.mCarRigidbody == null)
		{
			return;
		}
		this.MainCam.fieldOfView = Mathf.Lerp(this.MinFOV, this.MaxFOV, Mathf.Lerp(this.preSpeedPercent, this.mCarCtl.CurSpeedPercent, Time.deltaTime * 2f));
		this.preSpeedPercent = this.mCarCtl.CurSpeedPercent;
	}

	// Token: 0x06003652 RID: 13906 RVA: 0x000DF2F4 File Offset: 0x000DD4F4
	private void LateUpdate()
	{
		this.UpdatePos();
	}

	// Token: 0x06003653 RID: 13907 RVA: 0x000DF2FC File Offset: 0x000DD4FC
	private void UpdatePos()
	{
		if (this.mCarTransform == null)
		{
			return;
		}
		this.mTargetPos = this.mCarTransform.position + Vector3.up * this.TargetDeltaHeight;
		this.mWantedAngle = this.mCarTransform.eulerAngles.y + this.DiffWantedAngle;
		this.mWantedHeight = this.mTargetPos.y + this.Height;
		this.mCurrentAngle = base.transform.eulerAngles.y;
		this.mCurrentHeight = base.transform.position.y;
		this.mCurrentAngle = Mathf.LerpAngle(this.mCurrentAngle, this.mWantedAngle, this.RotationDamping * Time.deltaTime);
		this.mCurrentHeight = Mathf.Lerp(this.mCurrentHeight, this.mWantedHeight, this.HeightDamping * Time.deltaTime);
		this.mCurRotation = Quaternion.Euler(0f, this.mCurrentAngle, 0f);
		this.mCamTempPos = this.mTargetPos - this.mCurRotation * Vector3.forward * this.Distance;
		base.transform.position = new Vector3(this.mCamTempPos.x, this.mCurrentHeight, this.mCamTempPos.z);
		base.transform.LookAt(this.mTargetPos);
		if (!this.RotateWithSteer)
		{
			return;
		}
		this.mWantedAngleZ = 50f * this.mCarCtl.CurSteerPercent * this.mCarCtl.CurSpeedPercent;
		this.mCurrentAngleZ = Mathf.SmoothDampAngle(base.transform.eulerAngles.z, this.mWantedAngleZ, ref this.mAngleZVelocity, 0.3f);
		if (float.IsNaN(this.mCurrentAngleZ))
		{
			return;
		}
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, this.mCurrentAngleZ);
	}

	// Token: 0x06003654 RID: 13908 RVA: 0x000DF524 File Offset: 0x000DD724
	public void UpdateToTargetPos()
	{
		this.mTargetPos = this.mCarTransform.position + Vector3.up * this.TargetDeltaHeight;
		this.mWantedAngle = this.mCarTransform.eulerAngles.y + this.DiffWantedAngle;
		this.mWantedHeight = this.mTargetPos.y + this.Height;
		this.mCurrentAngle = base.transform.eulerAngles.y;
		this.mCurrentHeight = base.transform.position.y;
		this.mCurrentAngle = this.mWantedAngle;
		this.mCurrentHeight = this.mWantedHeight;
		this.mCurRotation = Quaternion.Euler(0f, this.mCurrentAngle, 0f);
		this.mCamTempPos = this.mTargetPos - this.mCurRotation * Vector3.forward * this.Distance;
		base.transform.position = new Vector3(this.mCamTempPos.x, this.mCurrentHeight, this.mCamTempPos.z);
		base.transform.LookAt(this.mTargetPos);
		if (!this.RotateWithSteer)
		{
			return;
		}
		this.mWantedAngleZ = 50f * this.mCarCtl.CurSteerPercent * this.mCarCtl.CurSpeedPercent;
		this.mCurrentAngleZ = Mathf.SmoothDampAngle(base.transform.eulerAngles.z, this.mWantedAngleZ, ref this.mAngleZVelocity, 0.3f);
		if (float.IsNaN(this.mCurrentAngleZ))
		{
			return;
		}
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, this.mCurrentAngleZ);
	}

	// Token: 0x040023B8 RID: 9144
	public bool RotateWithSteer;

	// Token: 0x040023B9 RID: 9145
	public float MaxFOV;

	// Token: 0x040023BA RID: 9146
	public float MinFOV;

	// Token: 0x040023BB RID: 9147
	public float TargetDeltaHeight = 0.5f;

	// Token: 0x040023BC RID: 9148
	public float Height = 0.5f;

	// Token: 0x040023BD RID: 9149
	public float RotationDamping = 3f;

	// Token: 0x040023BE RID: 9150
	public float HeightDamping = 3f;

	// Token: 0x040023BF RID: 9151
	public float Distance = 5f;

	// Token: 0x040023C0 RID: 9152
	public Camera MainCam;

	// Token: 0x040023C1 RID: 9153
	private CarControl mCarCtl;

	// Token: 0x040023C2 RID: 9154
	public Transform mCarTransform;

	// Token: 0x040023C3 RID: 9155
	public float DiffWantedAngle;

	// Token: 0x040023C4 RID: 9156
	private Rigidbody mCarRigidbody;

	// Token: 0x040023C5 RID: 9157
	private float preSpeedPercent;

	// Token: 0x040023C6 RID: 9158
	private Vector3 mTargetPos;

	// Token: 0x040023C7 RID: 9159
	private float mWantedAngle;

	// Token: 0x040023C8 RID: 9160
	private float mWantedHeight;

	// Token: 0x040023C9 RID: 9161
	private float mCurrentAngle;

	// Token: 0x040023CA RID: 9162
	private float mCurrentHeight;

	// Token: 0x040023CB RID: 9163
	private Quaternion mCurRotation;

	// Token: 0x040023CC RID: 9164
	private Vector3 mCamTempPos;

	// Token: 0x040023CD RID: 9165
	private float mCamAngleZ;

	// Token: 0x040023CE RID: 9166
	private float mWantedAngleZ;

	// Token: 0x040023CF RID: 9167
	private float mCurrentAngleZ;

	// Token: 0x040023D0 RID: 9168
	private float mAngleZVelocity;
}
