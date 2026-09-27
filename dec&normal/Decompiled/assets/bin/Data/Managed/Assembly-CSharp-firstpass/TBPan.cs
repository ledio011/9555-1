using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[RequireComponent(typeof(DragRecognizer))]
[AddComponentMenu("FingerGestures/Toolbox/Camera/Pan")]
public class TBPan : MonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x060001D2 RID: 466 RVA: 0x00008140 File Offset: 0x00006340
	// (remove) Token: 0x060001D3 RID: 467 RVA: 0x0000815C File Offset: 0x0000635C
	public event TBPan.PanEventHandler OnPan;

	// Token: 0x060001D4 RID: 468 RVA: 0x00008178 File Offset: 0x00006378
	private void Awake()
	{
		this.cachedTransform = base.transform;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00008188 File Offset: 0x00006388
	private void Start()
	{
		this.idealPos = this.cachedTransform.position;
		if (!base.GetComponent<DragRecognizer>())
		{
			Debug.LogWarning("No drag recognizer found on " + base.name + ". Disabling TBPan.");
			base.enabled = false;
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x000081D8 File Offset: 0x000063D8
	private void OnDrag(DragGesture gesture)
	{
		this.dragGesture = ((gesture.State != GestureRecognitionState.Ended) ? gesture : null);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000081F4 File Offset: 0x000063F4
	private void Update()
	{
		if (this.dragGesture != null && this.dragGesture.DeltaMove.SqrMagnitude() > 0f)
		{
			Vector2 vector = this.sensitivity * this.dragGesture.DeltaMove;
			Vector3 vector2 = vector.x * this.cachedTransform.right + vector.y * this.cachedTransform.up;
			this.idealPos -= vector2;
			if (this.OnPan != null)
			{
				this.OnPan(this, vector2);
			}
		}
		this.idealPos = this.ConstrainToMoveArea(this.idealPos);
		if (this.smoothSpeed > 0f)
		{
			this.cachedTransform.position = Vector3.Lerp(this.cachedTransform.position, this.idealPos, Time.deltaTime * this.smoothSpeed);
		}
		else
		{
			this.cachedTransform.position = this.idealPos;
		}
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00008304 File Offset: 0x00006504
	public Vector3 ConstrainToPanningPlane(Vector3 p)
	{
		Vector3 position = this.cachedTransform.InverseTransformPoint(p);
		position.z = 0f;
		return this.cachedTransform.TransformPoint(position);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00008338 File Offset: 0x00006538
	public void TeleportTo(Vector3 worldPos)
	{
		this.cachedTransform.position = (this.idealPos = this.ConstrainToPanningPlane(worldPos));
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00008360 File Offset: 0x00006560
	public void FlyTo(Vector3 worldPos)
	{
		this.idealPos = this.ConstrainToPanningPlane(worldPos);
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00008370 File Offset: 0x00006570
	public Vector3 ConstrainToMoveArea(Vector3 p)
	{
		if (this.moveArea)
		{
			Vector3 min = this.moveArea.bounds.min;
			Vector3 max = this.moveArea.bounds.max;
			p.x = Mathf.Clamp(p.x, min.x, max.x);
			p.y = Mathf.Clamp(p.y, min.y, max.y);
			p.z = Mathf.Clamp(p.z, min.z, max.z);
		}
		return p;
	}

	// Token: 0x04000150 RID: 336
	private Transform cachedTransform;

	// Token: 0x04000151 RID: 337
	public float sensitivity = 1f;

	// Token: 0x04000152 RID: 338
	public float smoothSpeed = 10f;

	// Token: 0x04000153 RID: 339
	public BoxCollider moveArea;

	// Token: 0x04000154 RID: 340
	private Vector3 idealPos;

	// Token: 0x04000155 RID: 341
	private DragGesture dragGesture;

	// Token: 0x02000067 RID: 103
	// (Invoke) Token: 0x0600028F RID: 655
	public delegate void PanEventHandler(TBPan source, Vector3 move);
}
