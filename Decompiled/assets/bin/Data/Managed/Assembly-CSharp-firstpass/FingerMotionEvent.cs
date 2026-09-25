using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class FingerMotionEvent : FingerEvent
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00003F98 File Offset: 0x00002198
	// (set) Token: 0x0600009D RID: 157 RVA: 0x00003FA0 File Offset: 0x000021A0
	public override Vector2 Position
	{
		get
		{
			return this.position;
		}
		internal set
		{
			this.position = value;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600009E RID: 158 RVA: 0x00003FAC File Offset: 0x000021AC
	// (set) Token: 0x0600009F RID: 159 RVA: 0x00003FB4 File Offset: 0x000021B4
	public FingerMotionPhase Phase
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

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003FC0 File Offset: 0x000021C0
	public float ElapsedTime
	{
		get
		{
			return Mathf.Max(0f, Time.time - this.StartTime);
		}
	}

	// Token: 0x04000069 RID: 105
	private FingerMotionPhase phase;

	// Token: 0x0400006A RID: 106
	private Vector2 position = Vector2.zero;

	// Token: 0x0400006B RID: 107
	internal float StartTime;
}
