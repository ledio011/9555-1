using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x060000D8 RID: 216 RVA: 0x0000653C File Offset: 0x0000473C
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000654C File Offset: 0x0000474C
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
		}
		else
		{
			this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
		}
	}

	// Token: 0x040000F2 RID: 242
	public Transform target;

	// Token: 0x040000F3 RID: 243
	public float speed = 1f;

	// Token: 0x040000F4 RID: 244
	private Transform mTrans;
}
