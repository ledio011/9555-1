using System;
using UnityEngine;

// Token: 0x02000842 RID: 2114
public class SmoothFollowCar : MonoBehaviour
{
	// Token: 0x0600364D RID: 13901 RVA: 0x000DEFE8 File Offset: 0x000DD1E8
	private void LateUpdate()
	{
		if (!this.target)
		{
			return;
		}
		float y = this.target.eulerAngles.y;
		float num = this.target.position.y + this.height;
		float num2 = base.transform.eulerAngles.y;
		float num3 = base.transform.position.y;
		num2 = Mathf.LerpAngle(num2, y, this.rotationDamping * Time.deltaTime);
		num3 = Mathf.Lerp(num3, num, this.heightDamping * Time.deltaTime);
		Quaternion quaternion = Quaternion.Euler(0f, num2, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= quaternion * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num3, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	// Token: 0x0600364E RID: 13902 RVA: 0x000DF128 File Offset: 0x000DD328
	public void SetToTarget()
	{
		float y = this.target.eulerAngles.y;
		float num = this.target.position.y + this.height;
		Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= quaternion * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	// Token: 0x040023B3 RID: 9139
	public Transform target;

	// Token: 0x040023B4 RID: 9140
	public float distance = 10f;

	// Token: 0x040023B5 RID: 9141
	public float height = 0.5f;

	// Token: 0x040023B6 RID: 9142
	public float heightDamping = 2f;

	// Token: 0x040023B7 RID: 9143
	public float rotationDamping = 3f;
}
