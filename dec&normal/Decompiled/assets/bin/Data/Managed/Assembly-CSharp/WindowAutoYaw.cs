using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x060000E3 RID: 227 RVA: 0x00006910 File Offset: 0x00004B10
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00006924 File Offset: 0x00004B24
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00006964 File Offset: 0x00004B64
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040000FE RID: 254
	public int updateOrder;

	// Token: 0x040000FF RID: 255
	public Camera uiCamera;

	// Token: 0x04000100 RID: 256
	public float yawAmount = 20f;

	// Token: 0x04000101 RID: 257
	private Transform mTrans;
}
