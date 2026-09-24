using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x060000E7 RID: 231 RVA: 0x000069E4 File Offset: 0x00004BE4
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00006A04 File Offset: 0x00004C04
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x04000102 RID: 258
	public int updateOrder;

	// Token: 0x04000103 RID: 259
	public float degrees = 30f;

	// Token: 0x04000104 RID: 260
	private Vector3 mLastPos;

	// Token: 0x04000105 RID: 261
	private Transform mTrans;

	// Token: 0x04000106 RID: 262
	private float mAngle;
}
