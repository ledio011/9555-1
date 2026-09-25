using System;

// Token: 0x0200002E RID: 46
public class PinchGesture : ContinuousGesture
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000131 RID: 305 RVA: 0x00005590 File Offset: 0x00003790
	// (set) Token: 0x06000132 RID: 306 RVA: 0x00005598 File Offset: 0x00003798
	public float Delta
	{
		get
		{
			return this.delta;
		}
		internal set
		{
			this.delta = value;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000133 RID: 307 RVA: 0x000055A4 File Offset: 0x000037A4
	// (set) Token: 0x06000134 RID: 308 RVA: 0x000055AC File Offset: 0x000037AC
	public float Gap
	{
		get
		{
			return this.gap;
		}
		internal set
		{
			this.gap = value;
		}
	}

	// Token: 0x040000C5 RID: 197
	private float delta;

	// Token: 0x040000C6 RID: 198
	private float gap;
}
