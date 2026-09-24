using System;

// Token: 0x0200003A RID: 58
public class TwistGesture : ContinuousGesture
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000184 RID: 388 RVA: 0x00006B84 File Offset: 0x00004D84
	// (set) Token: 0x06000185 RID: 389 RVA: 0x00006B8C File Offset: 0x00004D8C
	public float DeltaRotation { get; internal set; }

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000186 RID: 390 RVA: 0x00006B98 File Offset: 0x00004D98
	// (set) Token: 0x06000187 RID: 391 RVA: 0x00006BA0 File Offset: 0x00004DA0
	public float TotalRotation { get; internal set; }

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000188 RID: 392 RVA: 0x00006BAC File Offset: 0x00004DAC
	// (set) Token: 0x06000189 RID: 393 RVA: 0x00006BB4 File Offset: 0x00004DB4
	public FingerGestures.Finger Pivot { get; internal set; }
}
