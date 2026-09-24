using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000042 RID: 66
[AddComponentMenu("FingerGestures/Toolbox/Camera/Orbit")]
public class TBOrbit : MonoBehaviour
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060001B9 RID: 441 RVA: 0x000079F8 File Offset: 0x00005BF8
	public float Distance
	{
		get
		{
			return this.distance;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060001BA RID: 442 RVA: 0x00007A00 File Offset: 0x00005C00
	// (set) Token: 0x060001BB RID: 443 RVA: 0x00007A08 File Offset: 0x00005C08
	public float IdealDistance
	{
		get
		{
			return this.idealDistance;
		}
		set
		{
			this.idealDistance = Mathf.Clamp(value, this.minDistance, this.maxDistance);
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060001BC RID: 444 RVA: 0x00007A24 File Offset: 0x00005C24
	public float Yaw
	{
		get
		{
			return this.yaw;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060001BD RID: 445 RVA: 0x00007A2C File Offset: 0x00005C2C
	// (set) Token: 0x060001BE RID: 446 RVA: 0x00007A34 File Offset: 0x00005C34
	public float IdealYaw
	{
		get
		{
			return this.idealYaw;
		}
		set
		{
			this.idealYaw = ((!this.clampYawAngle) ? value : TBOrbit.ClampAngle(value, this.minYaw, this.maxYaw));
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060001BF RID: 447 RVA: 0x00007A60 File Offset: 0x00005C60
	public float Pitch
	{
		get
		{
			return this.pitch;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007A68 File Offset: 0x00005C68
	// (set) Token: 0x060001C1 RID: 449 RVA: 0x00007A70 File Offset: 0x00005C70
	public float IdealPitch
	{
		get
		{
			return this.idealPitch;
		}
		set
		{
			this.idealPitch = ((!this.clampPitchAngle) ? value : TBOrbit.ClampAngle(value, this.minPitch, this.maxPitch));
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007A9C File Offset: 0x00005C9C
	// (set) Token: 0x060001C3 RID: 451 RVA: 0x00007AA4 File Offset: 0x00005CA4
	public Vector3 IdealPanOffset
	{
		get
		{
			return this.idealPanOffset;
		}
		set
		{
			this.idealPanOffset = value;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007AB0 File Offset: 0x00005CB0
	public Vector3 PanOffset
	{
		get
		{
			return this.panOffset;
		}
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00007AB8 File Offset: 0x00005CB8
	private void InstallGestureRecognizers()
	{
		List<GestureRecognizer> list = new List<GestureRecognizer>(base.GetComponents<GestureRecognizer>());
		DragRecognizer dragRecognizer = list.Find((GestureRecognizer r) => r.EventMessageName == "OnDrag") as DragRecognizer;
		DragRecognizer dragRecognizer2 = list.Find((GestureRecognizer r) => r.EventMessageName == "OnTwoFingerDrag") as DragRecognizer;
		PinchRecognizer exists = list.Find((GestureRecognizer r) => r.EventMessageName == "OnPinch") as PinchRecognizer;
		if (this.OnlyRotateWhenDragStartsOnObject)
		{
			ScreenRaycaster exists2 = base.gameObject.GetComponent<ScreenRaycaster>();
			if (!exists2)
			{
				exists2 = base.gameObject.AddComponent<ScreenRaycaster>();
			}
		}
		if (!dragRecognizer)
		{
			dragRecognizer = base.gameObject.AddComponent<DragRecognizer>();
			dragRecognizer.RequiredFingerCount = 1;
			dragRecognizer.IsExclusive = true;
			dragRecognizer.MaxSimultaneousGestures = 1;
			dragRecognizer.SendMessageToSelection = GestureRecognizer.SelectionType.None;
		}
		if (!exists)
		{
			exists = base.gameObject.AddComponent<PinchRecognizer>();
		}
		if (!dragRecognizer2)
		{
			dragRecognizer2 = base.gameObject.AddComponent<DragRecognizer>();
			dragRecognizer2.RequiredFingerCount = 2;
			dragRecognizer2.IsExclusive = true;
			dragRecognizer2.MaxSimultaneousGestures = 1;
			dragRecognizer2.ApplySameDirectionConstraint = true;
			dragRecognizer2.EventMessageName = "OnTwoFingerDrag";
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00007C08 File Offset: 0x00005E08
	private void Start()
	{
		this.InstallGestureRecognizers();
		if (!this.panningPlane)
		{
			this.panningPlane = base.transform;
		}
		Vector3 eulerAngles = base.transform.eulerAngles;
		float num = this.initialDistance;
		this.IdealDistance = num;
		this.distance = num;
		num = eulerAngles.y;
		this.IdealYaw = num;
		this.yaw = num;
		num = eulerAngles.x;
		this.IdealPitch = num;
		this.pitch = num;
		if (base.rigidbody)
		{
			base.rigidbody.freezeRotation = true;
		}
		this.Apply();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00007CA8 File Offset: 0x00005EA8
	private void OnDrag(DragGesture gesture)
	{
		if (this.OnlyRotateWhenDragStartsOnObject)
		{
			if (gesture.Phase == ContinuousGesturePhase.Started)
			{
				if (!gesture.Recognizer.Raycaster)
				{
					Debug.LogWarning("The drag recognizer on " + gesture.Recognizer.name + " has no ScreenRaycaster component set. This will prevent OnlyRotateWhenDragStartsOnObject flag from working.");
					this.OnlyRotateWhenDragStartsOnObject = false;
					return;
				}
				if (this.target && !this.target.collider)
				{
					Debug.LogWarning("The target object has no collider set. OnlyRotateWhenDragStartsOnObject won't work.");
					this.OnlyRotateWhenDragStartsOnObject = false;
					return;
				}
			}
			if (!this.target || gesture.StartSelection != this.target.gameObject)
			{
				return;
			}
		}
		if (Time.time < this.nextDragTime)
		{
			return;
		}
		if (this.target)
		{
			this.IdealYaw += gesture.DeltaMove.x.Centimeters() * this.yawSensitivity;
			this.IdealPitch -= gesture.DeltaMove.y.Centimeters() * this.pitchSensitivity;
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00007DDC File Offset: 0x00005FDC
	private void OnPinch(PinchGesture gesture)
	{
		if (this.allowPinchZoom)
		{
			this.IdealDistance -= gesture.Delta.Centimeters() * this.pinchZoomSensitivity;
			this.nextDragTime = Time.time + 0.25f;
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00007E24 File Offset: 0x00006024
	private void OnTwoFingerDrag(DragGesture gesture)
	{
		if (this.allowPanning)
		{
			Vector3 b = -this.panningSensitivity * (this.panningPlane.right * gesture.DeltaMove.x.Centimeters() + this.panningPlane.up * gesture.DeltaMove.y.Centimeters());
			if (this.invertPanningDirections)
			{
				this.IdealPanOffset -= b;
			}
			else
			{
				this.IdealPanOffset += b;
			}
			this.nextDragTime = Time.time + 0.25f;
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00007EDC File Offset: 0x000060DC
	private void Apply()
	{
		if (this.smoothMotion)
		{
			this.distance = Mathf.Lerp(this.distance, this.IdealDistance, Time.deltaTime * this.smoothZoomSpeed);
			this.yaw = Mathf.Lerp(this.yaw, this.IdealYaw, Time.deltaTime * this.smoothOrbitSpeed);
			this.pitch = Mathf.LerpAngle(this.pitch, this.IdealPitch, Time.deltaTime * this.smoothOrbitSpeed);
		}
		else
		{
			this.distance = this.IdealDistance;
			this.yaw = this.IdealYaw;
			this.pitch = this.IdealPitch;
		}
		if (this.smoothPanning)
		{
			this.panOffset = Vector3.Lerp(this.panOffset, this.idealPanOffset, Time.deltaTime * this.smoothPanningSpeed);
		}
		else
		{
			this.panOffset = this.idealPanOffset;
		}
		base.transform.rotation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		Vector3 vector = this.target.position + this.panOffset;
		Vector3 vector2 = vector - this.distance * base.transform.forward;
		if (this.collisionLayerMask != 0)
		{
			Vector3 vector3 = vector2 - vector;
			float magnitude = vector3.magnitude;
			vector3.Normalize();
			RaycastHit raycastHit;
			if (Physics.Raycast(vector, vector3, out raycastHit, magnitude, this.collisionLayerMask))
			{
				vector2 = raycastHit.point - vector3 * 0.1f;
				this.distance = raycastHit.distance;
			}
		}
		base.transform.position = vector2;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000808C File Offset: 0x0000628C
	private void LateUpdate()
	{
		this.Apply();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00008094 File Offset: 0x00006294
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x000080D4 File Offset: 0x000062D4
	public void ResetPanning()
	{
		this.IdealPanOffset = Vector3.zero;
	}

	// Token: 0x04000126 RID: 294
	public Transform target;

	// Token: 0x04000127 RID: 295
	public float initialDistance = 10f;

	// Token: 0x04000128 RID: 296
	public float minDistance = 1f;

	// Token: 0x04000129 RID: 297
	public float maxDistance = 30f;

	// Token: 0x0400012A RID: 298
	public float yawSensitivity = 45f;

	// Token: 0x0400012B RID: 299
	public float pitchSensitivity = 45f;

	// Token: 0x0400012C RID: 300
	public bool clampYawAngle;

	// Token: 0x0400012D RID: 301
	public float minYaw = -75f;

	// Token: 0x0400012E RID: 302
	public float maxYaw = 75f;

	// Token: 0x0400012F RID: 303
	public bool clampPitchAngle = true;

	// Token: 0x04000130 RID: 304
	public float minPitch = -20f;

	// Token: 0x04000131 RID: 305
	public float maxPitch = 80f;

	// Token: 0x04000132 RID: 306
	public bool allowPinchZoom = true;

	// Token: 0x04000133 RID: 307
	public float pinchZoomSensitivity = 5f;

	// Token: 0x04000134 RID: 308
	public bool smoothMotion = true;

	// Token: 0x04000135 RID: 309
	public float smoothZoomSpeed = 5f;

	// Token: 0x04000136 RID: 310
	public float smoothOrbitSpeed = 10f;

	// Token: 0x04000137 RID: 311
	public bool allowPanning;

	// Token: 0x04000138 RID: 312
	public bool invertPanningDirections;

	// Token: 0x04000139 RID: 313
	public float panningSensitivity = 1f;

	// Token: 0x0400013A RID: 314
	public Transform panningPlane;

	// Token: 0x0400013B RID: 315
	public bool smoothPanning = true;

	// Token: 0x0400013C RID: 316
	public float smoothPanningSpeed = 12f;

	// Token: 0x0400013D RID: 317
	public LayerMask collisionLayerMask;

	// Token: 0x0400013E RID: 318
	private float distance = 10f;

	// Token: 0x0400013F RID: 319
	private float yaw;

	// Token: 0x04000140 RID: 320
	private float pitch;

	// Token: 0x04000141 RID: 321
	private float idealDistance;

	// Token: 0x04000142 RID: 322
	private float idealYaw;

	// Token: 0x04000143 RID: 323
	private float idealPitch;

	// Token: 0x04000144 RID: 324
	private Vector3 idealPanOffset = Vector3.zero;

	// Token: 0x04000145 RID: 325
	private Vector3 panOffset = Vector3.zero;

	// Token: 0x04000146 RID: 326
	private PinchRecognizer pinchRecognizer;

	// Token: 0x04000147 RID: 327
	private float nextDragTime;

	// Token: 0x04000148 RID: 328
	public bool OnlyRotateWhenDragStartsOnObject;

	// Token: 0x02000043 RID: 67
	public enum PanMode
	{
		// Token: 0x0400014D RID: 333
		Disabled,
		// Token: 0x0400014E RID: 334
		OneFinger,
		// Token: 0x0400014F RID: 335
		TwoFingers
	}
}
