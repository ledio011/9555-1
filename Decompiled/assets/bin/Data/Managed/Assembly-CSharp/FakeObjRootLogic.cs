using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000917 RID: 2327
public class FakeObjRootLogic : SingletonUnity<FakeObjRootLogic>
{
	// Token: 0x0600402A RID: 16426 RVA: 0x0012CDFC File Offset: 0x0012AFFC
	private new void Awake()
	{
		base.Awake();
		base.transform.position = new Vector3(0f, 8f, 0f);
	}

	// Token: 0x0600402B RID: 16427 RVA: 0x0012CE30 File Offset: 0x0012B030
	private void CreateModelPic(float rate)
	{
		this.MoveFlag = false;
		this.MeshRoot.localPosition = this.BeginPos;
		this.MeshRoot.localRotation = Quaternion.Euler(this.BeginRot);
		this.ModelPic = new RenderTexture((int)(512f * rate), 512, 16, 0);
		this.ModelPic.useMipMap = false;
		this.objCam.targetTexture = this.ModelPic;
		this.objCam.ResetAspect();
	}

	// Token: 0x0600402C RID: 16428 RVA: 0x0012CEB0 File Offset: 0x0012B0B0
	public void SetPicValue(float rate = 0.5f)
	{
		this.MoveFlag = false;
		this.MeshRoot.localPosition = this.BeginPos;
		this.MeshRoot.localRotation = Quaternion.Euler(this.BeginRot);
		if (this.ModelPic == null || !this.ModelPic.IsCreated())
		{
			this.CreateModelPic(rate);
		}
		else
		{
			if (this.curRate == rate && this.ModelPic.IsCreated())
			{
				return;
			}
			if (this.ModelPic.IsCreated())
			{
				this.ModelPic.Release();
			}
			this.CreateModelPic(rate);
		}
		this.curRate = rate;
	}

	// Token: 0x0600402D RID: 16429 RVA: 0x0012CF60 File Offset: 0x0012B160
	public void MoveShowPart(int profession, int curpart)
	{
		if (this.PartDataList.Count == 0)
		{
			this.PartDataList = DataManager.GetModelPartDataList();
		}
		this.MoveFlag = true;
		this.MeshRoot.localPosition = this.BeginPos;
		this.MeshRoot.localRotation = Quaternion.Euler(this.BeginRot);
		this.TargetPos = this.BeginPos;
		this.TargetRot = this.BeginRot;
		for (int i = 0; i < this.PartDataList.Count; i++)
		{
			if (profession == this.PartDataList[i].Profession && curpart == this.PartDataList[i].PartType)
			{
				this.TargetPos = new Vector3(this.PartDataList[i].CamPosX, this.PartDataList[i].CamPosY, this.PartDataList[i].CamPosZ);
				if (curpart == 0)
				{
					this.TargetRot = new Vector3(0f, this.PartDataList[i].RotationY, 0f);
				}
				break;
			}
		}
	}

	// Token: 0x0600402E RID: 16430 RVA: 0x0012D088 File Offset: 0x0012B288
	public void EnableFakeObjRoot()
	{
		this.objCam.cullingMask = 4194304;
		base.gameObject.layer = 22;
		this.MeshRoot.gameObject.layer = 22;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x0600402F RID: 16431 RVA: 0x0012D0D0 File Offset: 0x0012B2D0
	public void DisableFakeObjRoot()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x06004030 RID: 16432 RVA: 0x0012D0E0 File Offset: 0x0012B2E0
	protected override void OnDestroy()
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.Release();
		}
		base.OnDestroy();
	}

	// Token: 0x06004031 RID: 16433 RVA: 0x0012D104 File Offset: 0x0012B304
	public void resetTexture(int width, int height, int depth)
	{
		if (this.ModelPic.IsCreated())
		{
			this.ModelPic.width = width;
			this.ModelPic.height = height;
			this.ModelPic.depth = depth;
		}
	}

	// Token: 0x06004032 RID: 16434 RVA: 0x0012D148 File Offset: 0x0012B348
	private void Update()
	{
		if (this.MoveFlag)
		{
			this.MeshRoot.localPosition = Vector3.Lerp(this.MeshRoot.localPosition, this.TargetPos, Time.deltaTime * 1f);
			this.MeshRoot.localRotation = Quaternion.Slerp(this.MeshRoot.localRotation, Quaternion.Euler(this.TargetRot), Time.deltaTime * 1f);
			if (Vector3.Distance(this.MeshRoot.localPosition, this.TargetPos) < 0.05f)
			{
				this.MoveFlag = false;
			}
		}
	}

	// Token: 0x04002BE6 RID: 11238
	public Transform MeshRoot;

	// Token: 0x04002BE7 RID: 11239
	public GameObject mTopLeftObj;

	// Token: 0x04002BE8 RID: 11240
	public GameObject mBottomRightObj;

	// Token: 0x04002BE9 RID: 11241
	public Camera objCam;

	// Token: 0x04002BEA RID: 11242
	public RenderTexture ModelPic;

	// Token: 0x04002BEB RID: 11243
	private float curRate = -1f;

	// Token: 0x04002BEC RID: 11244
	private Vector3 BeginPos = new Vector3(0f, -1f, 2.7f);

	// Token: 0x04002BED RID: 11245
	private Vector3 BeginRot = new Vector3(0f, 175f, 0f);

	// Token: 0x04002BEE RID: 11246
	private Vector3 TargetPos;

	// Token: 0x04002BEF RID: 11247
	private Vector3 TargetRot;

	// Token: 0x04002BF0 RID: 11248
	private bool MoveFlag;

	// Token: 0x04002BF1 RID: 11249
	private List<ModelPartData> PartDataList = new List<ModelPartData>();
}
