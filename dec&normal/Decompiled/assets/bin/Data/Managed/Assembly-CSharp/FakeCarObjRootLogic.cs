using System;
using UnityEngine;

// Token: 0x02000912 RID: 2322
public class FakeCarObjRootLogic : SingletonUnity<FakeCarObjRootLogic>
{
	// Token: 0x06003FE0 RID: 16352 RVA: 0x0012AE88 File Offset: 0x00129088
	protected override void Awake()
	{
		base.Awake();
		if (this.ModelPic == null)
		{
			this.ModelPic = new RenderTexture(400, 400, 16, 0);
		}
		this.ModelPic.useMipMap = false;
		this.objCam.targetTexture = this.ModelPic;
		this.objCam.ResetAspect();
		this.objCam.cullingMask = 4194304;
		base.gameObject.layer = 22;
	}

	// Token: 0x06003FE1 RID: 16353 RVA: 0x0012AF0C File Offset: 0x0012910C
	public void InitFakeCarObj(MountData mountData, ColorData colordata)
	{
		if (this.CurCarMesh != null)
		{
			if (this.curMountdata == null || !this.curMountdata.ID.Equals(mountData.ID) || this.curColordata == null || !this.curColordata.ID.Equals(colordata.ID))
			{
				UnityVersionUtil.SetActiveRecursive(this.CurCarMesh.gameObject, false);
				this.CurCarMesh.transform.parent = null;
				Singleton<ObjManager>.Instance.RecycleMountCar(this.CurCarMesh);
				this.CurCarMesh = null;
			}
		}
		if (this.CurCarMesh == null)
		{
			RideMountData rideMountData = new RideMountData();
			rideMountData.Player = null;
			rideMountData.MountData = mountData;
			rideMountData.mColorData = colordata;
			this.CurCarMesh = Singleton<ObjManager>.Instance.GetMountCar(rideMountData);
		}
		UnityVersionUtil.SetActiveRecursive(this.CurCarMesh.gameObject, true);
		NGUITools.SetLayer(this.CurCarMesh.gameObject, 22);
		this.MeshRoot.transform.localPosition = mountData.ModelPos;
		this.MeshRoot.transform.localRotation = Quaternion.identity;
		this.CurCarMesh.transform.parent = this.MeshRoot.transform;
		this.CurCarMesh.transform.localPosition = Vector3.zero;
		this.CurCarMesh.transform.localRotation = Quaternion.identity;
		this.curMountdata = mountData;
		this.curColordata = colordata;
	}

	// Token: 0x06003FE2 RID: 16354 RVA: 0x0012B094 File Offset: 0x00129294
	public void ChangeColor(ColorData colordata)
	{
		if (this.CurCarMesh != null)
		{
			this.CurCarMesh.ChangeColor(colordata);
		}
		else
		{
			Debug.Log("not find mesh !!!!!");
		}
	}

	// Token: 0x06003FE3 RID: 16355 RVA: 0x0012B0D0 File Offset: 0x001292D0
	public void EnableFakeObjRoot()
	{
		this.StopRotate();
		this.curMountdata = null;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x06003FE4 RID: 16356 RVA: 0x0012B0EC File Offset: 0x001292EC
	public void DisableFakeObjRoot()
	{
		if (this.CurCarMesh != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.CurCarMesh.gameObject, false);
			this.CurCarMesh.transform.parent = null;
			Singleton<ObjManager>.Instance.RecycleMountCar(this.CurCarMesh);
			this.CurCarMesh = null;
		}
		this.StopRotate();
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x06003FE5 RID: 16357 RVA: 0x0012B15C File Offset: 0x0012935C
	protected override void OnDestroy()
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.Release();
		}
		base.OnDestroy();
	}

	// Token: 0x06003FE6 RID: 16358 RVA: 0x0012B180 File Offset: 0x00129380
	public void resetTexture(int width, int height, int depth)
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.width = width;
			this.ModelPic.height = height;
			this.ModelPic.depth = depth;
		}
	}

	// Token: 0x06003FE7 RID: 16359 RVA: 0x0012B1C4 File Offset: 0x001293C4
	public void PlayRotate()
	{
		this.rotateAnima.PlayAnima();
	}

	// Token: 0x06003FE8 RID: 16360 RVA: 0x0012B1D4 File Offset: 0x001293D4
	public void StopRotate()
	{
		this.rotateAnima.StopAnima();
	}

	// Token: 0x04002BBF RID: 11199
	public Transform MeshRoot;

	// Token: 0x04002BC0 RID: 11200
	public GameObject mTopLeftObj;

	// Token: 0x04002BC1 RID: 11201
	public GameObject mBottomRightObj;

	// Token: 0x04002BC2 RID: 11202
	public Camera objCam;

	// Token: 0x04002BC3 RID: 11203
	public RenderTexture ModelPic;

	// Token: 0x04002BC4 RID: 11204
	private ObjPlayerMountCar CurCarMesh;

	// Token: 0x04002BC5 RID: 11205
	private MountData curMountdata;

	// Token: 0x04002BC6 RID: 11206
	private ColorData curColordata;

	// Token: 0x04002BC7 RID: 11207
	public RotateAnima rotateAnima;
}
