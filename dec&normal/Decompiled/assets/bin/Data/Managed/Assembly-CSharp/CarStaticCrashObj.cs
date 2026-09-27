using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class CarStaticCrashObj : MonoBehaviour
{
	// Token: 0x060009E2 RID: 2530 RVA: 0x00047D70 File Offset: 0x00045F70
	private void Awake()
	{
		this.Reset();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00047D78 File Offset: 0x00045F78
	private void Update()
	{
		if (this.EnableFlag)
		{
			this.mTimeCount += Time.deltaTime;
			if (this.mTimeCount > this.DisactiveTime)
			{
				this.DisactiveSelf();
			}
		}
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00047DBC File Offset: 0x00045FBC
	private void DisactiveSelf()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00047DCC File Offset: 0x00045FCC
	private void Reset()
	{
		base.rigidbody.isKinematic = true;
		base.rigidbody.useGravity = false;
		this.EnableFlag = false;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00047DF8 File Offset: 0x00045FF8
	private void EnableRigidBody()
	{
		base.rigidbody.isKinematic = false;
		base.rigidbody.useGravity = true;
		this.EnableFlag = true;
		CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
		if (component != null)
		{
			base.rigidbody.centerOfMass = component.center - Vector3.forward * component.height / 4f;
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00047E6C File Offset: 0x0004606C
	private void OnTriggerEnter(Collider other)
	{
		if (!this.EnableFlag && (other.CompareTag("PlayerCar") || other.CompareTag("PoliceCar")))
		{
			if (other.attachedRigidbody == null)
			{
				return;
			}
			if (other.attachedRigidbody != null && other.attachedRigidbody.velocity.sqrMagnitude > 100f)
			{
				this.EnableRigidBody();
			}
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x00047EEC File Offset: 0x000460EC
	private void OnCollisionEnter(Collision other)
	{
		if (!this.EnableFlag && (other.gameObject.CompareTag("PlayerCar") || other.gameObject.CompareTag("PoliceCar")))
		{
			if (other.rigidbody == null)
			{
				return;
			}
			if (other.rigidbody != null && other.rigidbody.velocity.sqrMagnitude > 100f)
			{
				this.EnableRigidBody();
			}
		}
	}

	// Token: 0x040008AB RID: 2219
	public float DisactiveTime = 5f;

	// Token: 0x040008AC RID: 2220
	private float mTimeCount;

	// Token: 0x040008AD RID: 2221
	private bool EnableFlag;
}
