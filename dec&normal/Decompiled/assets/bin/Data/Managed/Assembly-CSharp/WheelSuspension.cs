using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class WheelSuspension : MonoBehaviour
{
	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x000504A8 File Offset: 0x0004E6A8
	public float WheelRadius
	{
		get
		{
			return this.wheelRadius;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000504B0 File Offset: 0x0004E6B0
	public bool OnGround
	{
		get
		{
			return this.onGround;
		}
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x000504B8 File Offset: 0x0004E6B8
	private void Start()
	{
		this.Init(-1f);
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x000504C8 File Offset: 0x0004E6C8
	public void Init(float radius = -1f)
	{
		if (this.wheelTrs == null)
		{
			return;
		}
		if (this.mInitFlag)
		{
			return;
		}
		this.mInitFlag = true;
		this.onGround = true;
		this.dummyWheelTrs = base.transform;
		Transform parent = this.dummyWheelTrs.parent;
		while (parent != null)
		{
			if (parent.rigidbody != null)
			{
				this.vehicleRigidbody = parent.rigidbody;
				break;
			}
			parent = parent.parent;
		}
		if (this.vehicleRigidbody == null)
		{
			Debug.LogError("veichle no body");
		}
		this.fullCompressionSpringForce = this.vehicleRigidbody.mass * 0.25f * -Physics.gravity.y;
		this.dummyWheelTrs.localPosition = new Vector3(this.dummyWheelTrs.localPosition.x, this.wheelTrs.localPosition.y, this.dummyWheelTrs.localPosition.z);
		this.originalPosition = this.dummyWheelTrs.localPosition;
		this.meshOriginalPos = this.wheelTrs.transform.localPosition;
		this.damperForce = this.fullCompressionSpringForce * 0.8f;
		if (radius < 0f)
		{
			MeshRenderer componentInChildren = this.wheelTrs.GetComponentInChildren<MeshRenderer>();
			if (componentInChildren != null)
			{
				this.wheelRadius = componentInChildren.bounds.extents.y / 1.5f * this.wheelTrs.localScale.y;
			}
			else
			{
				Transform transform = base.transform.parent.FindChild("houlun");
				if (transform != null)
				{
					componentInChildren = transform.gameObject.renderer.GetComponentInChildren<MeshRenderer>();
					if (componentInChildren != null)
					{
						this.wheelRadius = componentInChildren.bounds.extents.y * this.wheelTrs.localScale.y;
					}
					else
					{
						this.wheelRadius = 0.38f;
					}
				}
				else
				{
					this.wheelRadius = 0.38f;
				}
			}
		}
		else
		{
			this.wheelRadius = radius;
		}
		this.vehicle = this.vehicleRigidbody.gameObject.GetComponent<CarControl>();
		this.castLayer = 8388608;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00050740 File Offset: 0x0004E940
	private void Update()
	{
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00050744 File Offset: 0x0004E944
	private void LateUpdate()
	{
		if (this.wheelTrs == null)
		{
			return;
		}
		if (this.LeftFlag)
		{
			this.wheelAngle += this.vehicle.CurSpeed / this.wheelRadius * Time.deltaTime * 57.29578f;
		}
		else
		{
			this.wheelAngle -= this.vehicle.CurSpeed / this.wheelRadius * Time.deltaTime * 57.29578f;
		}
		if (this.FrontFlag)
		{
			this.steerAngle = this.vehicle.CurSteerAngle;
		}
		else
		{
			this.steerAngle = 0f;
		}
		if (!this.LeftFlag)
		{
			this.steerAngle += 180f;
		}
		this.wheelTrs.localRotation = Quaternion.Euler(new Vector3(this.wheelAngle, this.steerAngle, 0f));
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00050838 File Offset: 0x0004EA38
	private void FixedUpdate()
	{
		if (this.wheelTrs == null)
		{
			return;
		}
		this.onGround = Physics.Raycast(this.dummyWheelTrs.position, -this.dummyWheelTrs.up, ref this.outHit, this.suspensionTravel + this.wheelRadius, this.castLayer);
		if (this.onGround && this.outHit.collider.isTrigger)
		{
			this.onGround = false;
			float num = this.suspensionTravel + this.wheelRadius;
			foreach (RaycastHit raycastHit in Physics.RaycastAll(this.dummyWheelTrs.position, -this.dummyWheelTrs.up, this.suspensionTravel + this.wheelRadius))
			{
				if (!raycastHit.collider.isTrigger && raycastHit.distance <= num)
				{
					this.outHit = raycastHit;
					this.onGround = true;
					num = raycastHit.distance;
				}
			}
		}
		if (this.onGround)
		{
			Vector3 pointVelocity = this.vehicleRigidbody.GetPointVelocity(this.dummyWheelTrs.position);
			Vector3 vector = this.dummyWheelTrs.InverseTransformDirection(pointVelocity);
			Vector3 vector2 = this.dummyWheelTrs.InverseTransformDirection(this.outHit.normal);
			float num2 = Vector3.Dot(vector, vector2) * this.damperForce;
			float num3 = 1f - (this.outHit.distance - this.wheelRadius) / this.suspensionTravel;
			float num4 = Mathf.Clamp01(1f - num3) * this.suspensionTravel;
			this.dummyWheelTrs.localPosition = new Vector3(this.originalPosition.x, this.originalPosition.y - num4, this.originalPosition.z);
			num3 = Mathf.Clamp(num3, -3f, 3f);
			Vector3 vector3 = (this.fullCompressionSpringForce * num3 - num2) * this.dummyWheelTrs.up;
			this.vehicleRigidbody.AddForceAtPosition(vector3, this.dummyWheelTrs.position);
			this.wheelTrs.transform.localPosition = new Vector3(this.meshOriginalPos.x, Mathf.Lerp(this.wheelTrs.transform.localPosition.y, this.meshOriginalPos.y - num4, 0.5f), this.meshOriginalPos.z);
		}
	}

	// Token: 0x040009D0 RID: 2512
	public bool FrontFlag;

	// Token: 0x040009D1 RID: 2513
	public bool LeftFlag;

	// Token: 0x040009D2 RID: 2514
	private Transform dummyWheelTrs;

	// Token: 0x040009D3 RID: 2515
	public Transform wheelTrs;

	// Token: 0x040009D4 RID: 2516
	public CarControl vehicle;

	// Token: 0x040009D5 RID: 2517
	private Rigidbody vehicleRigidbody;

	// Token: 0x040009D6 RID: 2518
	private float fullCompressionSpringForce;

	// Token: 0x040009D7 RID: 2519
	private float suspensionTravel = 0.1f;

	// Token: 0x040009D8 RID: 2520
	private float wheelRadius = 0.5f;

	// Token: 0x040009D9 RID: 2521
	private float damperForece;

	// Token: 0x040009DA RID: 2522
	private bool onGround = true;

	// Token: 0x040009DB RID: 2523
	public RaycastHit outHit;

	// Token: 0x040009DC RID: 2524
	private float damperForce = 30000f;

	// Token: 0x040009DD RID: 2525
	private Vector3 originalPosition;

	// Token: 0x040009DE RID: 2526
	private Vector3 meshOriginalPos;

	// Token: 0x040009DF RID: 2527
	private bool mInitFlag;

	// Token: 0x040009E0 RID: 2528
	private LayerMask castLayer;

	// Token: 0x040009E1 RID: 2529
	private float wheelAngle;

	// Token: 0x040009E2 RID: 2530
	private float steerAngle;
}
