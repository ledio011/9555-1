using System;
using UnityEngine;

// Token: 0x020001C7 RID: 455
[Serializable]
public class CityPathPointData
{
	// Token: 0x06001074 RID: 4212 RVA: 0x00067678 File Offset: 0x00065878
	public float GetLinkDisByIndex(int index)
	{
		for (int i = 0; i < this.LinkPointIndex.Length; i++)
		{
			if (this.LinkPointIndex[i] == index)
			{
				return this.LinkPointDis[i];
			}
		}
		return 0f;
	}

	// Token: 0x040013CF RID: 5071
	public Vector3 PointPos;

	// Token: 0x040013D0 RID: 5072
	public Vector3 PointForward;

	// Token: 0x040013D1 RID: 5073
	public Vector3 PointRight;

	// Token: 0x040013D2 RID: 5074
	public int[] LinkPointIndex = new int[4];

	// Token: 0x040013D3 RID: 5075
	public float[] LinkPointDis = new float[4];

	// Token: 0x040013D4 RID: 5076
	public bool IsWalkable = true;

	// Token: 0x040013D5 RID: 5077
	public bool IsCross;

	// Token: 0x040013D6 RID: 5078
	public bool IsFork;

	// Token: 0x040013D7 RID: 5079
	public bool IsNsCross;

	// Token: 0x040013D8 RID: 5080
	public bool IsFourLines;

	// Token: 0x040013D9 RID: 5081
	public float MinWalkDis;

	// Token: 0x040013DA RID: 5082
	public float MaxWalkDis;

	// Token: 0x040013DB RID: 5083
	public int SelfIndex;

	// Token: 0x040013DC RID: 5084
	public Transform pathTrans;
}
