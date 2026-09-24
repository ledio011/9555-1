using System;

// Token: 0x0200019C RID: 412
public class PlayAnimationData
{
	// Token: 0x06000FD7 RID: 4055 RVA: 0x00064C84 File Offset: 0x00062E84
	public PlayAnimationData(string actionName, float delayTime, string effInfoID)
	{
		this.ActionName = actionName;
		this.DelayTime = delayTime;
		this.EffInfoID = effInfoID;
	}

	// Token: 0x0400119F RID: 4511
	public string ActionName = string.Empty;

	// Token: 0x040011A0 RID: 4512
	public float DelayTime;

	// Token: 0x040011A1 RID: 4513
	public string EffInfoID;
}
