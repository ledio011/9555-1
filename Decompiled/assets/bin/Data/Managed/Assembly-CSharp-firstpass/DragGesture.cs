using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class DragGesture : ContinuousGesture
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000122 RID: 290 RVA: 0x000052FC File Offset: 0x000034FC
	// (set) Token: 0x06000123 RID: 291 RVA: 0x00005304 File Offset: 0x00003504
	public Vector2 DeltaMove
	{
		get
		{
			return this.deltaMove;
		}
		internal set
		{
			this.deltaMove = value;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000124 RID: 292 RVA: 0x00005310 File Offset: 0x00003510
	public Vector2 TotalMove
	{
		get
		{
			return base.Position - base.StartPosition;
		}
	}

	// Token: 0x040000BE RID: 190
	private Vector2 deltaMove = Vector2.zero;

	// Token: 0x040000BF RID: 191
	internal Vector2 LastPos = Vector2.zero;

	// Token: 0x040000C0 RID: 192
	internal Vector2 LastDelta = Vector2.zero;
}
