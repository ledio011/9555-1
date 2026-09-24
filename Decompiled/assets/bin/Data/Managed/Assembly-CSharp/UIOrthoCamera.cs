using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x06000619 RID: 1561 RVA: 0x00029674 File Offset: 0x00027874
	private void Start()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x000296A8 File Offset: 0x000278A8
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = this.mCam.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num3))
		{
			this.mCam.orthographicSize = num3;
		}
	}

	// Token: 0x04000554 RID: 1364
	private Camera mCam;

	// Token: 0x04000555 RID: 1365
	private Transform mTrans;
}
