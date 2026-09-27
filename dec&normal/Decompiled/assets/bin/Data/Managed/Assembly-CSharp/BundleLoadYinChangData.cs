using System;
using UnityEngine;

// Token: 0x02000847 RID: 2119
public class BundleLoadYinChangData
{
	// Token: 0x06003689 RID: 13961 RVA: 0x000E0BA4 File Offset: 0x000DEDA4
	public BundleLoadYinChangData(EffInfoData efData, float dur, Vector3 sdPos)
	{
		this.effInfoData = efData;
		this.duration = dur;
		this.senderPos = sdPos;
	}

	// Token: 0x040023F0 RID: 9200
	public EffInfoData effInfoData;

	// Token: 0x040023F1 RID: 9201
	public float duration;

	// Token: 0x040023F2 RID: 9202
	public Vector3 senderPos;
}
