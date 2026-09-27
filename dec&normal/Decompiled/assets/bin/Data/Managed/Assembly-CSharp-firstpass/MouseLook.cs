using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	// Token: 0x06000230 RID: 560 RVA: 0x00009284 File Offset: 0x00007484
	private void Update()
	{
		if (this.axes == MouseLook.RotationAxes.MouseXAndY)
		{
			float y = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, y, 0f);
		}
		else if (this.axes == MouseLook.RotationAxes.MouseX)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.sensitivityX, 0f);
		}
		else
		{
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x000093C0 File Offset: 0x000075C0
	private void Start()
	{
		if (base.rigidbody)
		{
			base.rigidbody.freezeRotation = true;
		}
	}

	// Token: 0x0400019C RID: 412
	public MouseLook.RotationAxes axes;

	// Token: 0x0400019D RID: 413
	public float sensitivityX = 15f;

	// Token: 0x0400019E RID: 414
	public float sensitivityY = 15f;

	// Token: 0x0400019F RID: 415
	public float minimumX = -360f;

	// Token: 0x040001A0 RID: 416
	public float maximumX = 360f;

	// Token: 0x040001A1 RID: 417
	public float minimumY = -60f;

	// Token: 0x040001A2 RID: 418
	public float maximumY = 60f;

	// Token: 0x040001A3 RID: 419
	private float rotationY;

	// Token: 0x02000050 RID: 80
	public enum RotationAxes
	{
		// Token: 0x040001A5 RID: 421
		MouseXAndY,
		// Token: 0x040001A6 RID: 422
		MouseX,
		// Token: 0x040001A7 RID: 423
		MouseY
	}
}
