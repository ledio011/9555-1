using System;
using UnityEngine;

// Token: 0x02000209 RID: 521
public class AutoSearchPathPoint
{
	// Token: 0x060011A4 RID: 4516 RVA: 0x00072154 File Offset: 0x00070354
	public AutoSearchPathPoint(string sceneId, float posX, float posY, float posZ)
	{
		this.mSceneId = sceneId;
		this.mPosX = posX;
		this.mPosY = posY;
		this.mPosZ = posZ;
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0007217C File Offset: 0x0007037C
	public AutoSearchPathPoint(string sceneId, Vector3 pos)
	{
		this.mSceneId = sceneId;
		this.mPosX = pos.x;
		this.mPosY = pos.y;
		this.mPosZ = pos.z;
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000721C0 File Offset: 0x000703C0
	// (set) Token: 0x060011A7 RID: 4519 RVA: 0x000721C8 File Offset: 0x000703C8
	public string SceneId
	{
		get
		{
			return this.mSceneId;
		}
		set
		{
			this.mSceneId = value;
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x060011A8 RID: 4520 RVA: 0x000721D4 File Offset: 0x000703D4
	// (set) Token: 0x060011A9 RID: 4521 RVA: 0x000721DC File Offset: 0x000703DC
	public float PosX
	{
		get
		{
			return this.mPosX;
		}
		set
		{
			this.mPosX = value;
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x060011AA RID: 4522 RVA: 0x000721E8 File Offset: 0x000703E8
	// (set) Token: 0x060011AB RID: 4523 RVA: 0x000721F0 File Offset: 0x000703F0
	public float PosY
	{
		get
		{
			return this.mPosY;
		}
		set
		{
			this.mPosY = value;
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x060011AC RID: 4524 RVA: 0x000721FC File Offset: 0x000703FC
	// (set) Token: 0x060011AD RID: 4525 RVA: 0x00072204 File Offset: 0x00070404
	public float PosZ
	{
		get
		{
			return this.mPosZ;
		}
		set
		{
			this.mPosZ = value;
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x00072210 File Offset: 0x00070410
	public static AutoSearchPathPoint CreatePoint(GameObject obj)
	{
		if (obj == null)
		{
			return null;
		}
		return new AutoSearchPathPoint(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x00072278 File Offset: 0x00070478
	public bool Equal(AutoSearchPathPoint point)
	{
		return this.mSceneId.Equals(point.SceneId) && Mathf.Abs(this.mPosX - point.PosX) < 0.01f && Mathf.Abs(this.mPosZ - point.PosZ) < 0.01f;
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x000722D8 File Offset: 0x000704D8
	public void Reset()
	{
		this.mSceneId = "-1";
		this.mPosX = 0f;
		this.mPosZ = 0f;
	}

	// Token: 0x04001778 RID: 6008
	private string mSceneId;

	// Token: 0x04001779 RID: 6009
	private float mPosX;

	// Token: 0x0400177A RID: 6010
	private float mPosY;

	// Token: 0x0400177B RID: 6011
	private float mPosZ;
}
