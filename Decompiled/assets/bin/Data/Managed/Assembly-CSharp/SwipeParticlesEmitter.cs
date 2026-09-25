using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class SwipeParticlesEmitter : MonoBehaviour
{
	// Token: 0x06000069 RID: 105 RVA: 0x00003EFC File Offset: 0x000020FC
	private void Start()
	{
		if (!this.emitter)
		{
			this.emitter = base.particleEmitter;
		}
		this.emitter.emit = false;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003F34 File Offset: 0x00002134
	public void Emit(Vector3 heading, float swipeVelocity)
	{
		this.emitter.transform.rotation = Quaternion.LookRotation(heading);
		Vector3 localVelocity = this.emitter.localVelocity;
		localVelocity.z = this.baseSpeed * this.swipeVelocityScale * swipeVelocity;
		this.emitter.localVelocity = localVelocity;
		this.emitter.Emit();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003F90 File Offset: 0x00002190
	public static Vector3 GetSwipeDirectionVector(FingerGestures.SwipeDirection direction)
	{
		switch (direction)
		{
		case 1:
			return Vector3.right;
		case 2:
			return Vector3.left;
		default:
			if (direction == 16)
			{
				return 0.5f * (Vector3.up + Vector3.left);
			}
			if (direction == 32)
			{
				return 0.5f * (Vector3.up + Vector3.right);
			}
			if (direction == 64)
			{
				return 0.5f * (Vector3.down + Vector3.right);
			}
			if (direction != 128)
			{
				Debug.LogError("Unhandled swipe direction: " + direction);
				return Vector3.zero;
			}
			return 0.5f * (Vector3.down + Vector3.left);
		case 4:
			return Vector3.up;
		case 8:
			return Vector3.down;
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000408C File Offset: 0x0000228C
	public void Emit(FingerGestures.SwipeDirection direction, float swipeVelocity)
	{
		Vector3 swipeDirectionVector = SwipeParticlesEmitter.GetSwipeDirectionVector(direction);
		this.Emit(swipeDirectionVector, swipeVelocity);
	}

	// Token: 0x04000062 RID: 98
	public ParticleEmitter emitter;

	// Token: 0x04000063 RID: 99
	public float baseSpeed = 4f;

	// Token: 0x04000064 RID: 100
	public float swipeVelocityScale = 0.001f;
}
