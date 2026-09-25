using System;

// Token: 0x02000038 RID: 56
[Serializable]
public class TapGesture : DiscreteGesture
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000177 RID: 375 RVA: 0x00006890 File Offset: 0x00004A90
	// (set) Token: 0x06000178 RID: 376 RVA: 0x00006898 File Offset: 0x00004A98
	public int Taps
	{
		get
		{
			return this.taps;
		}
		internal set
		{
			this.taps = value;
		}
	}

	// Token: 0x040000EC RID: 236
	private int taps;

	// Token: 0x040000ED RID: 237
	internal bool Down;

	// Token: 0x040000EE RID: 238
	internal bool WasDown;

	// Token: 0x040000EF RID: 239
	internal float LastDownTime;

	// Token: 0x040000F0 RID: 240
	internal float LastTapTime;
}
