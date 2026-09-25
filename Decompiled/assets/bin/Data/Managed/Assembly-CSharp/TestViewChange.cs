using System;
using UnityEngine;

// Token: 0x020008AA RID: 2218
public class TestViewChange : MonoBehaviour
{
	// Token: 0x06003BCB RID: 15307 RVA: 0x00104C04 File Offset: 0x00102E04
	public void Click()
	{
		if (this.open)
		{
			Singleton<ObjManager>.Instance.MainPlayer.CameraController.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FREE);
		}
		else
		{
			Singleton<ObjManager>.Instance.MainPlayer.CameraController.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FIXED);
		}
		this.open = !this.open;
	}

	// Token: 0x06003BCC RID: 15308 RVA: 0x00104C5C File Offset: 0x00102E5C
	private void Start()
	{
	}

	// Token: 0x06003BCD RID: 15309 RVA: 0x00104C60 File Offset: 0x00102E60
	private void Update()
	{
	}

	// Token: 0x0400271D RID: 10013
	private bool open;
}
