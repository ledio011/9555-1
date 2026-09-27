using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class MonsterData
{
	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x000643B0 File Offset: 0x000625B0
	public float PositionX
	{
		get
		{
			return (float)this.PosX / 100f;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000643C0 File Offset: 0x000625C0
	public float PositionZ
	{
		get
		{
			return (float)this.PosZ / 100f;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x000643D0 File Offset: 0x000625D0
	public float PositionO
	{
		get
		{
			return (float)this.PosO / 100f;
		}
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x000643E0 File Offset: 0x000625E0
	public Vector3 GetNpcPos()
	{
		return new Vector3(this.PositionX, SceneManager.GetHitHeight(new Vector3(this.PositionX, 0f, this.PositionZ)), this.PositionZ);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0006441C File Offset: 0x0006261C
	public Vector3 GetNpcXZPos()
	{
		return new Vector3(this.PositionX, 0f, this.PositionZ);
	}

	// Token: 0x040010DF RID: 4319
	public string MapID;

	// Token: 0x040010E0 RID: 4320
	public int Group;

	// Token: 0x040010E1 RID: 4321
	public string NpcID;

	// Token: 0x040010E2 RID: 4322
	public int PosX;

	// Token: 0x040010E3 RID: 4323
	public int PosZ;

	// Token: 0x040010E4 RID: 4324
	public int PosO;

	// Token: 0x040010E5 RID: 4325
	public int ReBirthType;

	// Token: 0x040010E6 RID: 4326
	public int ReBirthTime;

	// Token: 0x040010E7 RID: 4327
	public string PathId;
}
