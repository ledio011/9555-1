using System;

// Token: 0x02000848 RID: 2120
public class PlayingEffectData
{
	// Token: 0x0600368A RID: 13962 RVA: 0x000E0BC4 File Offset: 0x000DEDC4
	public PlayingEffectData(long ownerId, string effectId)
	{
		this.EffectDataId = effectId;
		this.OwnerId = ownerId;
	}

	// Token: 0x040023F3 RID: 9203
	public string EffectDataId;

	// Token: 0x040023F4 RID: 9204
	public long OwnerId;
}
