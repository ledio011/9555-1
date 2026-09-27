using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class NPCPathData
{
	// Token: 0x17000340 RID: 832
	// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000648E4 File Offset: 0x00062AE4
	public float PositionX
	{
		get
		{
			return (float)this.PosX / 100f;
		}
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000648F4 File Offset: 0x00062AF4
	public float PositionZ
	{
		get
		{
			return (float)this.PosZ / 100f;
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00064904 File Offset: 0x00062B04
	public Vector3 Position
	{
		get
		{
			return new Vector3(this.PositionX, 0f, this.PositionZ);
		}
	}

	// Token: 0x04001141 RID: 4417
	public string ID = string.Empty;

	// Token: 0x04001142 RID: 4418
	public int PointIndex = -1;

	// Token: 0x04001143 RID: 4419
	public int PosX;

	// Token: 0x04001144 RID: 4420
	public int PosZ;
}
