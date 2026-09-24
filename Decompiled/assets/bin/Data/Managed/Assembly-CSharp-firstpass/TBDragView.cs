using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
[RequireComponent(typeof(DragRecognizer))]
[AddComponentMenu("FingerGestures/Toolbox/Camera/Drag View")]
public class TBDragView : MonoBehaviour
{
	// Token: 0x060001AB RID: 427 RVA: 0x00007604 File Offset: 0x00005804
	private void Awake()
	{
		this.cachedTransform = base.transform;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00007614 File Offset: 0x00005814
	private void Start()
	{
		this.IdealRotation = this.cachedTransform.rotation;
		if (!base.GetComponent<DragRecognizer>())
		{
			Debug.LogWarning("No drag recognizer found on " + base.name + ". Disabling TBDragView.");
			base.enabled = false;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060001AD RID: 429 RVA: 0x00007664 File Offset: 0x00005864
	public bool Dragging
	{
		get
		{
			return this.dragGesture != null;
		}
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00007674 File Offset: 0x00005874
	private void OnDrag(DragGesture gesture)
	{
		if (gesture.Phase != ContinuousGesturePhase.Ended)
		{
			this.dragGesture = gesture;
		}
		else
		{
			this.dragGesture = null;
		}
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00007698 File Offset: 0x00005898
	private void Update()
	{
		if (this.Dragging && this.allowUserInput)
		{
			this.useAngularVelocity = true;
		}
		if (this.useAngularVelocity)
		{
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			Vector2 to = Vector2.zero;
			float num = this.dragDeceleration;
			if (this.Dragging)
			{
				to = this.sensitivity * this.dragGesture.DeltaMove.Centimeters();
				num = this.dragAcceleration;
			}
			this.angularVelocity = Vector2.Lerp(this.angularVelocity, to, Time.deltaTime * num);
			Vector2 a = Time.deltaTime * this.angularVelocity;
			if (this.reverseControls)
			{
				a = -a;
			}
			localEulerAngles.x = Mathf.Clamp(TBDragView.NormalizePitch(localEulerAngles.x + a.y), this.minPitchAngle, this.maxPitchAngle);
			localEulerAngles.y -= a.x;
			base.transform.localEulerAngles = localEulerAngles;
		}
		else if (this.idealRotationSmoothingSpeed > 0f)
		{
			this.cachedTransform.rotation = Quaternion.Slerp(this.cachedTransform.rotation, this.IdealRotation, Time.deltaTime * this.idealRotationSmoothingSpeed);
		}
		else
		{
			this.cachedTransform.rotation = this.idealRotation;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x000077F8 File Offset: 0x000059F8
	private static float NormalizePitch(float angle)
	{
		if (angle > 180f)
		{
			angle -= 360f;
		}
		return angle;
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007810 File Offset: 0x00005A10
	// (set) Token: 0x060001B2 RID: 434 RVA: 0x00007818 File Offset: 0x00005A18
	public Quaternion IdealRotation
	{
		get
		{
			return this.idealRotation;
		}
		set
		{
			this.idealRotation = value;
			this.useAngularVelocity = false;
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00007828 File Offset: 0x00005A28
	public void LookAt(Vector3 pos)
	{
		this.IdealRotation = Quaternion.LookRotation(pos - this.cachedTransform.position);
	}

	// Token: 0x04000118 RID: 280
	public bool allowUserInput = true;

	// Token: 0x04000119 RID: 281
	public float sensitivity = 360f;

	// Token: 0x0400011A RID: 282
	public float dragAcceleration = 40f;

	// Token: 0x0400011B RID: 283
	public float dragDeceleration = 15f;

	// Token: 0x0400011C RID: 284
	public bool reverseControls;

	// Token: 0x0400011D RID: 285
	public float minPitchAngle = -60f;

	// Token: 0x0400011E RID: 286
	public float maxPitchAngle = 60f;

	// Token: 0x0400011F RID: 287
	public float idealRotationSmoothingSpeed = 7f;

	// Token: 0x04000120 RID: 288
	private Transform cachedTransform;

	// Token: 0x04000121 RID: 289
	private Vector2 angularVelocity = Vector2.zero;

	// Token: 0x04000122 RID: 290
	private Quaternion idealRotation;

	// Token: 0x04000123 RID: 291
	private bool useAngularVelocity;

	// Token: 0x04000124 RID: 292
	private DragGesture dragGesture;
}
