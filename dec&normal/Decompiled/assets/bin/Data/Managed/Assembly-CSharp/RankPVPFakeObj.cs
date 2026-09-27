using System;
using UnityEngine;

// Token: 0x0200097C RID: 2428
public class RankPVPFakeObj : MonoBehaviour
{
	// Token: 0x0600449E RID: 17566 RVA: 0x00156D90 File Offset: 0x00154F90
	protected void Awake()
	{
		this.objCam.targetTexture = null;
		if (this.ModelPic == null)
		{
			this.ModelPic = new RenderTexture(375, 420, 16, 0);
			this.ModelPic.isPowerOfTwo = false;
			this.ModelPic.useMipMap = false;
		}
		this.objCam.targetTexture = this.ModelPic;
		this.objCam.ResetAspect();
	}

	// Token: 0x0600449F RID: 17567 RVA: 0x00156E08 File Offset: 0x00155008
	public void EnableFakeObjRoot()
	{
		this.objCam.cullingMask = 1 << this.layer;
		base.gameObject.layer = this.layer;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x060044A0 RID: 17568 RVA: 0x00156E48 File Offset: 0x00155048
	public void DisableFakeObjRoot()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x060044A1 RID: 17569 RVA: 0x00156E58 File Offset: 0x00155058
	protected void OnDestroy()
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.Release();
		}
	}

	// Token: 0x060044A2 RID: 17570 RVA: 0x00156E78 File Offset: 0x00155078
	public void resetTexture(int width, int height, int depth)
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.width = width;
			this.ModelPic.height = height;
			this.ModelPic.depth = depth;
			this.objCam.targetTexture = this.ModelPic;
			this.objCam.ResetAspect();
		}
	}

	// Token: 0x04003151 RID: 12625
	public Transform MeshRoot;

	// Token: 0x04003152 RID: 12626
	public Camera objCam;

	// Token: 0x04003153 RID: 12627
	public RenderTexture ModelPic;

	// Token: 0x04003154 RID: 12628
	public int layer = 22;
}
