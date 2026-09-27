using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[Serializable]
public class CharacterMotorMovingPlatform
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002218 File Offset: 0x00000418
	public CharacterMotorMovingPlatform()
	{
		this.enabled = true;
		this.movementTransfer = MovementTransferOnJump.PermaTransfer;
	}

	// Token: 0x0400001D RID: 29
	public bool enabled;

	// Token: 0x0400001E RID: 30
	public MovementTransferOnJump movementTransfer;

	// Token: 0x0400001F RID: 31
	[NonSerialized]
	public Transform hitPlatform;

	// Token: 0x04000020 RID: 32
	[NonSerialized]
	public Transform activePlatform;

	// Token: 0x04000021 RID: 33
	[NonSerialized]
	public Vector3 activeLocalPoint;

	// Token: 0x04000022 RID: 34
	[NonSerialized]
	public Vector3 activeGlobalPoint;

	// Token: 0x04000023 RID: 35
	[NonSerialized]
	public Quaternion activeLocalRotation;

	// Token: 0x04000024 RID: 36
	[NonSerialized]
	public Quaternion activeGlobalRotation;

	// Token: 0x04000025 RID: 37
	[NonSerialized]
	public Matrix4x4 lastMatrix;

	// Token: 0x04000026 RID: 38
	[NonSerialized]
	public Vector3 platformVelocity;

	// Token: 0x04000027 RID: 39
	[NonSerialized]
	public bool newPlatform;
}
