using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
[AddComponentMenu("Camera-Control/Smooth Follow")]
[Serializable]
public class SmoothFollow : MonoBehaviour
{
	// Token: 0x060000DA RID: 218 RVA: 0x0000A740 File Offset: 0x00008940
	public SmoothFollow()
	{
		this.distance = 10f;
		this.height = 5f;
		this.heightDamping = 2f;
		this.rotationDamping = 3f;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000A780 File Offset: 0x00008980
	public virtual void LateUpdate()
	{
		if (this.target)
		{
			float y = this.target.eulerAngles.y;
			float to = this.target.position.y + this.height;
			float num = this.transform.eulerAngles.y;
			float num2 = this.transform.position.y;
			num = Mathf.LerpAngle(num, y, this.rotationDamping * Time.deltaTime);
			num2 = Mathf.Lerp(num2, to, this.heightDamping * Time.deltaTime);
			Quaternion rotation = Quaternion.Euler((float)0, num, (float)0);
			this.transform.position = this.target.position;
			this.transform.position = this.transform.position - rotation * Vector3.forward * this.distance;
			float y2 = num2;
			Vector3 position = this.transform.position;
			float num3 = position.y = y2;
			Vector3 vector = this.transform.position = position;
			this.transform.LookAt(this.target);
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000A8C0 File Offset: 0x00008AC0
	public virtual void Main()
	{
	}

	// Token: 0x040001CF RID: 463
	public Transform target;

	// Token: 0x040001D0 RID: 464
	public float distance;

	// Token: 0x040001D1 RID: 465
	public float height;

	// Token: 0x040001D2 RID: 466
	public float heightDamping;

	// Token: 0x040001D3 RID: 467
	public float rotationDamping;
}
