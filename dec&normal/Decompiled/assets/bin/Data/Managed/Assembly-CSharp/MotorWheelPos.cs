using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class MotorWheelPos : MonoBehaviour
{
	// Token: 0x06000A0B RID: 2571 RVA: 0x000497C8 File Offset: 0x000479C8
	private void Start()
	{
		this.mCacheTrans = base.transform;
		this.defaultPos = this.mCacheTrans.localPosition;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x000497E8 File Offset: 0x000479E8
	private void Update()
	{
		this.mCacheTrans.localPosition = new Vector3(this.defaultPos.x, (this.LeftWheelTrans.localPosition.y + this.RightWheelTrans.localPosition.y) / 2f, this.defaultPos.z);
		this.mCacheTrans.rotation = this.LeftWheelTrans.rotation;
	}

	// Token: 0x040008DA RID: 2266
	public Transform LeftWheelTrans;

	// Token: 0x040008DB RID: 2267
	public Transform RightWheelTrans;

	// Token: 0x040008DC RID: 2268
	private Transform mCacheTrans;

	// Token: 0x040008DD RID: 2269
	private Vector3 defaultPos = Vector3.zero;
}
