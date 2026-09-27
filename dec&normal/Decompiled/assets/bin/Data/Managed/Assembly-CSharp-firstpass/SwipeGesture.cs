using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
[Serializable]
public class SwipeGesture : DiscreteGesture
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600016A RID: 362 RVA: 0x000065D0 File Offset: 0x000047D0
	// (set) Token: 0x0600016B RID: 363 RVA: 0x000065D8 File Offset: 0x000047D8
	public Vector2 Move
	{
		get
		{
			return this.move;
		}
		internal set
		{
			this.move = value;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600016C RID: 364 RVA: 0x000065E4 File Offset: 0x000047E4
	// (set) Token: 0x0600016D RID: 365 RVA: 0x000065EC File Offset: 0x000047EC
	public float Velocity
	{
		get
		{
			return this.velocity;
		}
		internal set
		{
			this.velocity = value;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600016E RID: 366 RVA: 0x000065F8 File Offset: 0x000047F8
	// (set) Token: 0x0600016F RID: 367 RVA: 0x00006600 File Offset: 0x00004800
	public FingerGestures.SwipeDirection Direction
	{
		get
		{
			return this.direction;
		}
		internal set
		{
			this.direction = value;
		}
	}

	// Token: 0x040000E2 RID: 226
	private Vector2 move = Vector2.zero;

	// Token: 0x040000E3 RID: 227
	private float velocity;

	// Token: 0x040000E4 RID: 228
	private FingerGestures.SwipeDirection direction;

	// Token: 0x040000E5 RID: 229
	internal int MoveCounter;

	// Token: 0x040000E6 RID: 230
	internal float Deviation;
}
