using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class CarShadowController : MonoBehaviour
{
	// Token: 0x060009DF RID: 2527 RVA: 0x00047BBC File Offset: 0x00045DBC
	public void Reset(Transform car)
	{
		this.mTargetCar = car;
		this.mTransform = base.transform;
		this.mProjecter = base.gameObject.GetComponent<Projector>();
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x00047BF0 File Offset: 0x00045DF0
	private void Update()
	{
		if (this.mTargetCar != null)
		{
			this.mTransform.position = this.mTargetCar.transform.position + Vector3.up * 2f;
			this.mTransform.eulerAngles = new Vector3(90f, 0f, -this.mTargetCar.eulerAngles.y);
			this.zAngle = Mathf.Clamp(Mathf.Abs((this.mTargetCar.transform.eulerAngles.z <= 180f) ? this.mTargetCar.transform.eulerAngles.z : (this.mTargetCar.transform.eulerAngles.z - 360f)), 0f, 90f);
			this.mProjecter.aspectRatio = Mathf.Lerp(1.3f, 0.5f, this.zAngle / 90f);
			if (!UnityVersionUtil.IsActive(this.mTargetCar.gameObject))
			{
				if (this.mProjecter.enabled)
				{
					this.mProjecter.enabled = false;
				}
			}
			else if (!this.mProjecter.enabled)
			{
				this.mProjecter.enabled = true;
			}
		}
	}

	// Token: 0x040008A7 RID: 2215
	private Projector mProjecter;

	// Token: 0x040008A8 RID: 2216
	private Transform mTargetCar;

	// Token: 0x040008A9 RID: 2217
	private Transform mTransform;

	// Token: 0x040008AA RID: 2218
	private float zAngle;
}
