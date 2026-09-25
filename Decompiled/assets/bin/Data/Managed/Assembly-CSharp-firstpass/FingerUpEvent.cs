using System;

// Token: 0x0200001F RID: 31
public class FingerUpEvent : FingerEvent
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000A9 RID: 169 RVA: 0x000042C0 File Offset: 0x000024C0
	// (set) Token: 0x060000AA RID: 170 RVA: 0x000042C8 File Offset: 0x000024C8
	public float TimeHeldDown
	{
		get
		{
			return this.timeHeldDown;
		}
		internal set
		{
			this.timeHeldDown = value;
		}
	}

	// Token: 0x04000075 RID: 117
	private float timeHeldDown;
}
