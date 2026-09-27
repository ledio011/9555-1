using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[AddComponentMenu("FingerGestures/Toolbox/Drag To Move")]
public class TBDragToMove : MonoBehaviour
{
	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060001EF RID: 495 RVA: 0x000086BC File Offset: 0x000068BC
	// (set) Token: 0x060001F0 RID: 496 RVA: 0x000086C4 File Offset: 0x000068C4
	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
		private set
		{
			if (this.dragging != value)
			{
				this.dragging = value;
				if (base.rigidbody)
				{
					if (this.dragging)
					{
						this.oldUseGravity = base.rigidbody.useGravity;
						this.oldIsKinematic = base.rigidbody.isKinematic;
						base.rigidbody.useGravity = false;
						base.rigidbody.isKinematic = true;
					}
					else
					{
						base.rigidbody.isKinematic = this.oldIsKinematic;
						base.rigidbody.useGravity = this.oldUseGravity;
						base.rigidbody.velocity = Vector3.zero;
					}
				}
			}
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00008770 File Offset: 0x00006970
	private void Start()
	{
		if (!this.RaycastCamera)
		{
			this.RaycastCamera = Camera.main;
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00008790 File Offset: 0x00006990
	public bool ProjectScreenPointOnDragPlane(Vector3 refPos, Vector2 screenPos, out Vector3 worldPos)
	{
		worldPos = refPos;
		if (this.DragPlaneCollider)
		{
			Ray ray = this.RaycastCamera.ScreenPointToRay(screenPos);
			RaycastHit raycastHit;
			if (!this.DragPlaneCollider.Raycast(ray, out raycastHit, 3.4028235E+38f))
			{
				return false;
			}
			worldPos = raycastHit.point + this.DragPlaneOffset * raycastHit.normal;
		}
		else
		{
			Transform transform = this.RaycastCamera.transform;
			Plane plane = new Plane(-transform.forward, refPos);
			Ray ray2 = this.RaycastCamera.ScreenPointToRay(screenPos);
			float distance = 0f;
			if (!plane.Raycast(ray2, out distance))
			{
				return false;
			}
			worldPos = ray2.GetPoint(distance);
		}
		return true;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00008864 File Offset: 0x00006A64
	private void HandleDrag(DragGesture gesture)
	{
		if (!base.enabled)
		{
			return;
		}
		if (gesture.Phase == ContinuousGesturePhase.Started)
		{
			this.Dragging = true;
			this.draggingFinger = gesture.Fingers[0];
		}
		else if (this.Dragging)
		{
			if (gesture.Fingers[0] != this.draggingFinger)
			{
				return;
			}
			if (gesture.Phase == ContinuousGesturePhase.Updated)
			{
				Transform transform = base.transform;
				Vector3 b = Vector3.zero;
				Vector3 b2;
				Vector3 a2;
				if (this.DragFromObjectCenter)
				{
					Vector3 a;
					if (this.ProjectScreenPointOnDragPlane(transform.position, this.draggingFinger.Position, out a))
					{
						b = a - transform.position;
					}
				}
				else if (this.ProjectScreenPointOnDragPlane(transform.position, this.draggingFinger.PreviousPosition, out b2) && this.ProjectScreenPointOnDragPlane(transform.position, this.draggingFinger.Position, out a2))
				{
					b = a2 - b2;
				}
				if (base.rigidbody)
				{
					this.physxDragMove += b;
				}
				else
				{
					transform.position += b;
				}
			}
			else
			{
				this.Dragging = false;
			}
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000089A8 File Offset: 0x00006BA8
	private void FixedUpdate()
	{
		if (this.Dragging && base.rigidbody)
		{
			base.rigidbody.MovePosition(base.rigidbody.position + this.physxDragMove);
			this.physxDragMove = Vector3.zero;
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x000089FC File Offset: 0x00006BFC
	private void OnDrag(DragGesture gesture)
	{
		this.HandleDrag(gesture);
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x00008A08 File Offset: 0x00006C08
	private void OnDisable()
	{
		if (this.Dragging)
		{
			this.Dragging = false;
		}
	}

	// Token: 0x04000164 RID: 356
	public Collider DragPlaneCollider;

	// Token: 0x04000165 RID: 357
	public float DragPlaneOffset;

	// Token: 0x04000166 RID: 358
	public Camera RaycastCamera;

	// Token: 0x04000167 RID: 359
	public bool DragFromObjectCenter;

	// Token: 0x04000168 RID: 360
	private bool dragging;

	// Token: 0x04000169 RID: 361
	private FingerGestures.Finger draggingFinger;

	// Token: 0x0400016A RID: 362
	private GestureRecognizer gestureRecognizer;

	// Token: 0x0400016B RID: 363
	private bool oldUseGravity;

	// Token: 0x0400016C RID: 364
	private bool oldIsKinematic;

	// Token: 0x0400016D RID: 365
	private Vector3 physxDragMove = Vector3.zero;

	// Token: 0x02000048 RID: 72
	public enum DragPlaneType
	{
		// Token: 0x0400016F RID: 367
		Camera,
		// Token: 0x04000170 RID: 368
		UseCollider
	}
}
