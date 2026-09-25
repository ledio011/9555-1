using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class BoxItemData
{
	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00061520 File Offset: 0x0005F720
	public Vector3 Position
	{
		get
		{
			return new Vector3(this.PositionX, this.PositionY, this.PositionZ);
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0006153C File Offset: 0x0005F73C
	public Vector3 Angel
	{
		get
		{
			return new Vector3(this.AngelX, this.AngelY, this.AngelZ);
		}
	}

	// Token: 0x04000D10 RID: 3344
	public string ID;

	// Token: 0x04000D11 RID: 3345
	public string Name;

	// Token: 0x04000D12 RID: 3346
	public int Type;

	// Token: 0x04000D13 RID: 3347
	public float PositionX;

	// Token: 0x04000D14 RID: 3348
	public float PositionY;

	// Token: 0x04000D15 RID: 3349
	public float PositionZ;

	// Token: 0x04000D16 RID: 3350
	public float AngelX;

	// Token: 0x04000D17 RID: 3351
	public float AngelY;

	// Token: 0x04000D18 RID: 3352
	public float AngelZ;

	// Token: 0x02000151 RID: 337
	public enum BOXTYPE
	{
		// Token: 0x04000D1A RID: 3354
		BLOCK
	}
}
