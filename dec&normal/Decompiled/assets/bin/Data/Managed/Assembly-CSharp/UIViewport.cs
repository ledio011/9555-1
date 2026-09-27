using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
[AddComponentMenu("NGUI/UI/Viewport Camera")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class UIViewport : MonoBehaviour
{
	// Token: 0x060006DE RID: 1758 RVA: 0x00031718 File Offset: 0x0002F918
	private void Start()
	{
		this.mCam = base.camera;
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00031750 File Offset: 0x0002F950
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
			Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
			Rect rect;
			rect..ctor(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = this.fullSize * rect.height;
			if (rect != this.mCam.rect)
			{
				this.mCam.rect = rect;
			}
			if (this.mCam.orthographicSize != num)
			{
				this.mCam.orthographicSize = num;
			}
		}
	}

	// Token: 0x0400060A RID: 1546
	public Camera sourceCamera;

	// Token: 0x0400060B RID: 1547
	public Transform topLeft;

	// Token: 0x0400060C RID: 1548
	public Transform bottomRight;

	// Token: 0x0400060D RID: 1549
	public float fullSize = 1f;

	// Token: 0x0400060E RID: 1550
	private Camera mCam;
}
