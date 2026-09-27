using System;
using UnityEngine;

// Token: 0x02000916 RID: 2326
public class FakeObjOtherPlayer : SingletonUnity<FakeObjOtherPlayer>
{
	// Token: 0x06004024 RID: 16420 RVA: 0x0012CC88 File Offset: 0x0012AE88
	protected override void Awake()
	{
		base.Awake();
		if (this.ModelPic == null)
		{
			this.ModelPic = new RenderTexture(332, 512, 16, 0);
		}
		this.ModelPic.useMipMap = false;
		this.objCam.targetTexture = this.ModelPic;
		this.objCam.ResetAspect();
	}

	// Token: 0x06004025 RID: 16421 RVA: 0x0012CCEC File Offset: 0x0012AEEC
	public void EnableFakeObjRoot()
	{
		this.objCam.cullingMask = 2097152;
		base.gameObject.layer = 21;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x06004026 RID: 16422 RVA: 0x0012CD24 File Offset: 0x0012AF24
	public void DisableFakeObjRoot()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x06004027 RID: 16423 RVA: 0x0012CD34 File Offset: 0x0012AF34
	protected override void OnDestroy()
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.Release();
		}
		base.OnDestroy();
	}

	// Token: 0x06004028 RID: 16424 RVA: 0x0012CD58 File Offset: 0x0012AF58
	public void resetTexture(int width, int height, int depth)
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.width = width;
			this.ModelPic.height = height;
			this.ModelPic.depth = depth;
		}
	}

	// Token: 0x04002BE3 RID: 11235
	public Transform MeshRoot;

	// Token: 0x04002BE4 RID: 11236
	public Camera objCam;

	// Token: 0x04002BE5 RID: 11237
	public RenderTexture ModelPic;
}
