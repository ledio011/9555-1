using System;

// Token: 0x02000153 RID: 339
public class PlayBuffInfoData
{
	// Token: 0x06000ED4 RID: 3796 RVA: 0x000615BC File Offset: 0x0005F7BC
	public PlayBuffInfoData(string buffID, float delayTime, float duration, ObjCharacter sender = null)
	{
		this.BuffInfoData = DataManager.GetBuffInfoDataByID(buffID);
		this.DelayTime = delayTime;
		this.Duration = duration;
		this.Sender = sender;
		this.FxEffectId = string.Empty;
	}

	// Token: 0x04000D29 RID: 3369
	public BuffInfoData BuffInfoData;

	// Token: 0x04000D2A RID: 3370
	public ObjCharacter Sender;

	// Token: 0x04000D2B RID: 3371
	public float DelayTime;

	// Token: 0x04000D2C RID: 3372
	public float Duration;

	// Token: 0x04000D2D RID: 3373
	public string FxEffectId = string.Empty;
}
