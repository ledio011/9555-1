using System;
using UnityEngine;

// Token: 0x020009BF RID: 2495
public class TeamFakeObjPicRootLogic : MonoBehaviour
{
	// Token: 0x06004704 RID: 18180 RVA: 0x00169344 File Offset: 0x00167544
	protected void Awake()
	{
		this.objCam.targetTexture = null;
		if (this.ModelPic == null)
		{
			if (this.type == 0)
			{
				this.ModelPic = new RenderTexture(180, 250, 16, 0);
			}
			else if (this.type == 1)
			{
				this.ModelPic = new RenderTexture(375, 474, 16, 0);
			}
			else if (this.type == 2)
			{
				this.ModelPic = new RenderTexture(400, 520, 16, 0);
			}
			this.ModelPic.isPowerOfTwo = false;
			this.ModelPic.useMipMap = false;
		}
		this.objCam.targetTexture = this.ModelPic;
		this.objCam.ResetAspect();
	}

	// Token: 0x06004705 RID: 18181 RVA: 0x00169418 File Offset: 0x00167618
	public void CreateModelPic()
	{
		this.objCam.targetTexture = null;
		if (this.ModelPic == null)
		{
			if (this.type == 0)
			{
				this.ModelPic = new RenderTexture(180, 250, 16, 0);
			}
			else if (this.type == 1)
			{
				this.ModelPic = new RenderTexture(375, 474, 16, 0);
			}
			else if (this.type == 2)
			{
				this.ModelPic = new RenderTexture(400, 520, 16, 0);
			}
			this.ModelPic.isPowerOfTwo = false;
			this.ModelPic.useMipMap = false;
		}
		this.objCam.targetTexture = this.ModelPic;
		this.objCam.ResetAspect();
	}

	// Token: 0x06004706 RID: 18182 RVA: 0x001694EC File Offset: 0x001676EC
	public void EnableFakeObjRoot()
	{
		this.objCam.cullingMask = 1 << this.layer;
		base.gameObject.layer = this.layer;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x06004707 RID: 18183 RVA: 0x0016952C File Offset: 0x0016772C
	public void DisableFakeObjRoot()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x06004708 RID: 18184 RVA: 0x0016953C File Offset: 0x0016773C
	protected void OnDestroy()
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.Release();
		}
	}

	// Token: 0x06004709 RID: 18185 RVA: 0x0016955C File Offset: 0x0016775C
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

	// Token: 0x04003419 RID: 13337
	public Transform MeshRoot;

	// Token: 0x0400341A RID: 13338
	public Camera objCam;

	// Token: 0x0400341B RID: 13339
	public RenderTexture ModelPic;

	// Token: 0x0400341C RID: 13340
	public int type;

	// Token: 0x0400341D RID: 13341
	public int layer = 22;
}
