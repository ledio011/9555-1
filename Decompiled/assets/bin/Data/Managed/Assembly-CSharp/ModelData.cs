using System;

// Token: 0x0200018C RID: 396
public class ModelData
{
	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00064398 File Offset: 0x00062598
	public MODEL_TYPE MODELTYPE
	{
		get
		{
			return (MODEL_TYPE)this.ModelType;
		}
	}

	// Token: 0x040010CD RID: 4301
	public string ID = string.Empty;

	// Token: 0x040010CE RID: 4302
	public string Name = string.Empty;

	// Token: 0x040010CF RID: 4303
	public string ModelPath = string.Empty;

	// Token: 0x040010D0 RID: 4304
	public int ModelType;

	// Token: 0x040010D1 RID: 4305
	public string EffectId = string.Empty;
}
