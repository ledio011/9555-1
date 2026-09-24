using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
[AddComponentMenu("FingerGestures/Toolbox/Twist To Rotate")]
public class TBTwistToRotate : MonoBehaviour
{
	// Token: 0x0600022C RID: 556 RVA: 0x000090FC File Offset: 0x000072FC
	private void Start()
	{
		if (!this.ReferenceCamera)
		{
			this.ReferenceCamera = Camera.main;
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000911C File Offset: 0x0000731C
	public Vector3 GetRotationAxis()
	{
		switch (this.Axis)
		{
		case TBTwistToRotate.RotationAxis.WorldX:
			return Vector3.right;
		case TBTwistToRotate.RotationAxis.WorldY:
			return Vector3.up;
		case TBTwistToRotate.RotationAxis.WorldZ:
			return Vector3.forward;
		case TBTwistToRotate.RotationAxis.ObjectX:
			return base.transform.right;
		case TBTwistToRotate.RotationAxis.ObjectY:
			return base.transform.up;
		case TBTwistToRotate.RotationAxis.ObjectZ:
			return base.transform.forward;
		case TBTwistToRotate.RotationAxis.CameraX:
			return this.ReferenceCamera.transform.right;
		case TBTwistToRotate.RotationAxis.CameraY:
			return this.ReferenceCamera.transform.up;
		case TBTwistToRotate.RotationAxis.CameraZ:
			return this.ReferenceCamera.transform.forward;
		default:
			Debug.LogWarning("Unhandled rotation axis: " + this.Axis);
			return Vector3.forward;
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x000091E8 File Offset: 0x000073E8
	private void OnTwist(TwistGesture gesture)
	{
		Quaternion lhs = Quaternion.AngleAxis(this.Sensitivity * gesture.DeltaRotation, this.GetRotationAxis());
		base.transform.rotation = lhs * base.transform.rotation;
	}

	// Token: 0x0400018F RID: 399
	public float Sensitivity = 1f;

	// Token: 0x04000190 RID: 400
	public TBTwistToRotate.RotationAxis Axis = TBTwistToRotate.RotationAxis.WorldY;

	// Token: 0x04000191 RID: 401
	public Camera ReferenceCamera;

	// Token: 0x0200004E RID: 78
	public enum RotationAxis
	{
		// Token: 0x04000193 RID: 403
		WorldX,
		// Token: 0x04000194 RID: 404
		WorldY,
		// Token: 0x04000195 RID: 405
		WorldZ,
		// Token: 0x04000196 RID: 406
		ObjectX,
		// Token: 0x04000197 RID: 407
		ObjectY,
		// Token: 0x04000198 RID: 408
		ObjectZ,
		// Token: 0x04000199 RID: 409
		CameraX,
		// Token: 0x0400019A RID: 410
		CameraY,
		// Token: 0x0400019B RID: 411
		CameraZ
	}
}
