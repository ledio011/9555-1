using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class FingerHoverEvent : FingerEvent
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000093 RID: 147 RVA: 0x00003E1C File Offset: 0x0000201C
	// (set) Token: 0x06000094 RID: 148 RVA: 0x00003E24 File Offset: 0x00002024
	public FingerHoverPhase Phase
	{
		get
		{
			return this.phase;
		}
		internal set
		{
			this.phase = value;
		}
	}

	// Token: 0x04000060 RID: 96
	private FingerHoverPhase phase;

	// Token: 0x04000061 RID: 97
	internal GameObject PreviousSelection;
}
