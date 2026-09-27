using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
[Serializable]
public class CharacterMotorMovement
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	public CharacterMotorMovement()
	{
		this.maxForwardSpeed = 10f;
		this.maxSidewaysSpeed = 10f;
		this.maxBackwardsSpeed = 10f;
		this.slopeSpeedMultiplier = new AnimationCurve(new Keyframe[]
		{
			new Keyframe((float)-90, (float)1),
			new Keyframe((float)0, (float)1),
			new Keyframe((float)90, (float)0)
		});
		this.maxGroundAcceleration = 30f;
		this.maxAirAcceleration = 20f;
		this.gravity = 10f;
		this.maxFallSpeed = 20f;
		this.frameVelocity = Vector3.zero;
		this.hitPoint = Vector3.zero;
		this.lastHitPoint = new Vector3(float.PositiveInfinity, (float)0, (float)0);
	}

	// Token: 0x04000001 RID: 1
	public float maxForwardSpeed;

	// Token: 0x04000002 RID: 2
	public float maxSidewaysSpeed;

	// Token: 0x04000003 RID: 3
	public float maxBackwardsSpeed;

	// Token: 0x04000004 RID: 4
	public AnimationCurve slopeSpeedMultiplier;

	// Token: 0x04000005 RID: 5
	public float maxGroundAcceleration;

	// Token: 0x04000006 RID: 6
	public float maxAirAcceleration;

	// Token: 0x04000007 RID: 7
	public float gravity;

	// Token: 0x04000008 RID: 8
	public float maxFallSpeed;

	// Token: 0x04000009 RID: 9
	[NonSerialized]
	public CollisionFlags collisionFlags;

	// Token: 0x0400000A RID: 10
	[NonSerialized]
	public Vector3 velocity;

	// Token: 0x0400000B RID: 11
	[NonSerialized]
	public Vector3 frameVelocity;

	// Token: 0x0400000C RID: 12
	[NonSerialized]
	public Vector3 hitPoint;

	// Token: 0x0400000D RID: 13
	[NonSerialized]
	public Vector3 lastHitPoint;
}
