using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
[Serializable]
public class CharacterMotorJumping
{
	// Token: 0x06000002 RID: 2 RVA: 0x000021C8 File Offset: 0x000003C8
	public CharacterMotorJumping()
	{
		this.enabled = true;
		this.baseHeight = 1f;
		this.extraHeight = 4.1f;
		this.steepPerpAmount = 0.5f;
		this.lastButtonDownTime = (float)-100;
		this.jumpDir = Vector3.up;
	}

	// Token: 0x04000013 RID: 19
	public bool enabled;

	// Token: 0x04000014 RID: 20
	public float baseHeight;

	// Token: 0x04000015 RID: 21
	public float extraHeight;

	// Token: 0x04000016 RID: 22
	public float perpAmount;

	// Token: 0x04000017 RID: 23
	public float steepPerpAmount;

	// Token: 0x04000018 RID: 24
	[NonSerialized]
	public bool jumping;

	// Token: 0x04000019 RID: 25
	[NonSerialized]
	public bool holdingJumpButton;

	// Token: 0x0400001A RID: 26
	[NonSerialized]
	public float lastStartTime;

	// Token: 0x0400001B RID: 27
	[NonSerialized]
	public float lastButtonDownTime;

	// Token: 0x0400001C RID: 28
	[NonSerialized]
	public Vector3 jumpDir;
}
