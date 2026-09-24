using System;
using UnityEngine;

// Token: 0x02000841 RID: 2113
public class SmoothFollow : MonoBehaviour
{
	// Token: 0x0600364B RID: 13899 RVA: 0x000DEE30 File Offset: 0x000DD030
	public void UpdateFollow()
	{
		if (this.targetTransform == null)
		{
			if (Singleton<ObjManager>.Instance.MainPlayer == null)
			{
				return;
			}
			this.targetTransform = Singleton<ObjManager>.Instance.MainPlayer.transform;
		}
		if (this.mainCameraTransform == null)
		{
			this.mainCameraTransform = Camera.mainCamera.transform;
		}
		float y = this.targetTransform.eulerAngles.y;
		float num = this.targetTransform.position.y + this.height;
		float num2 = this.mainCameraTransform.eulerAngles.y;
		float num3 = this.mainCameraTransform.position.y;
		num2 = Mathf.LerpAngle(num2, y, this.rotationDamping * Time.deltaTime);
		num3 = Mathf.Lerp(num3, num, this.heightDamping * Time.deltaTime);
		Quaternion quaternion = Quaternion.Euler(0f, num2, 0f);
		this.mainCameraTransform.position = this.targetTransform.position;
		this.mainCameraTransform.position -= quaternion * Vector3.forward * this.distance;
		Vector3 position = this.mainCameraTransform.position;
		position.y = num3;
		this.mainCameraTransform.position = position;
		this.mainCameraTransform.LookAt(this.targetTransform);
	}

	// Token: 0x040023AD RID: 9133
	public float distance = 10f;

	// Token: 0x040023AE RID: 9134
	private float height = 5f;

	// Token: 0x040023AF RID: 9135
	private float heightDamping = 2f;

	// Token: 0x040023B0 RID: 9136
	private float rotationDamping = 3f;

	// Token: 0x040023B1 RID: 9137
	private Transform targetTransform;

	// Token: 0x040023B2 RID: 9138
	private Transform mainCameraTransform;
}
