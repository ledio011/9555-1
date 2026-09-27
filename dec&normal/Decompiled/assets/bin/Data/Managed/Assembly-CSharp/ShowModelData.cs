using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class ShowModelData
{
	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0006561C File Offset: 0x0006381C
	public Vector3 Position
	{
		get
		{
			return new Vector3(this.posX, this.posY, this.posZ);
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00065638 File Offset: 0x00063838
	public Vector3 Rotation
	{
		get
		{
			return new Vector3(this.rotX, this.rotY, this.rotZ);
		}
	}

	// Token: 0x04001246 RID: 4678
	public string ID;

	// Token: 0x04001247 RID: 4679
	public string ModelName = string.Empty;

	// Token: 0x04001248 RID: 4680
	public float posX;

	// Token: 0x04001249 RID: 4681
	public float posY;

	// Token: 0x0400124A RID: 4682
	public float posZ;

	// Token: 0x0400124B RID: 4683
	public float rotX;

	// Token: 0x0400124C RID: 4684
	public float rotY;

	// Token: 0x0400124D RID: 4685
	public float rotZ;
}
