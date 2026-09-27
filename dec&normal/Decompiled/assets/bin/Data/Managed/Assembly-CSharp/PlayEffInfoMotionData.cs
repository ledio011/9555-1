using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class PlayEffInfoMotionData
{
	// Token: 0x06000F27 RID: 3879 RVA: 0x00062374 File Offset: 0x00060574
	public PlayEffInfoMotionData(string id, float delayTime, Vector3 senderPos)
	{
		this.EffInfoData = DataManager.GetEffInfoDataById(id);
		this.DelayTime = delayTime;
		this.SenderPos = senderPos;
	}

	// Token: 0x04000E79 RID: 3705
	public EffInfoData EffInfoData;

	// Token: 0x04000E7A RID: 3706
	public float DelayTime;

	// Token: 0x04000E7B RID: 3707
	public Vector3 SenderPos;
}
