using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

// Token: 0x02000007 RID: 7
[AddComponentMenu("Character/Character Motor")]
[RequireComponent(typeof(CharacterController))]
[Serializable]
public class CharacterMotor : MonoBehaviour
{
	// Token: 0x06000005 RID: 5 RVA: 0x0000226C File Offset: 0x0000046C
	public CharacterMotor()
	{
		this.canControl = true;
		this.useFixedUpdate = true;
		this.inputMoveDirection = Vector3.zero;
		this.movement = new CharacterMotorMovement();
		this.jumping = new CharacterMotorJumping();
		this.movingPlatform = new CharacterMotorMovingPlatform();
		this.sliding = new CharacterMotorSliding();
		this.grounded = true;
		this.groundNormal = Vector3.zero;
		this.lastGroundNormal = Vector3.zero;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000022E4 File Offset: 0x000004E4
	public virtual void Awake()
	{
		this.controller = (CharacterController)this.GetComponent(typeof(CharacterController));
		this.tr = this.transform;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002310 File Offset: 0x00000510
	private void UpdateFunction()
	{
		Vector3 vector = this.movement.velocity;
		vector = this.ApplyInputVelocityChange(vector);
		vector = this.ApplyGravityAndJumping(vector);
		Vector3 vector2 = Vector3.zero;
		if (this.MoveWithPlatform())
		{
			Vector3 a = this.movingPlatform.activePlatform.TransformPoint(this.movingPlatform.activeLocalPoint);
			vector2 = a - this.movingPlatform.activeGlobalPoint;
			if (vector2 != Vector3.zero)
			{
				this.controller.Move(vector2);
			}
			Quaternion lhs = this.movingPlatform.activePlatform.rotation * this.movingPlatform.activeLocalRotation;
			float y = (lhs * Quaternion.Inverse(this.movingPlatform.activeGlobalRotation)).eulerAngles.y;
			if (y != (float)0)
			{
				this.tr.Rotate((float)0, y, (float)0);
			}
		}
		Vector3 position = this.tr.position;
		Vector3 vector3 = vector * Time.deltaTime;
		float d = Mathf.Max(this.controller.stepOffset, new Vector3(vector3.x, (float)0, vector3.z).magnitude);
		if (this.grounded)
		{
			vector3 -= d * Vector3.up;
		}
		this.movingPlatform.hitPlatform = null;
		this.groundNormal = Vector3.zero;
		this.movement.collisionFlags = this.controller.Move(vector3);
		this.movement.lastHitPoint = this.movement.hitPoint;
		this.lastGroundNormal = this.groundNormal;
		if (this.movingPlatform.enabled && this.movingPlatform.activePlatform != this.movingPlatform.hitPlatform && this.movingPlatform.hitPlatform != null)
		{
			this.movingPlatform.activePlatform = this.movingPlatform.hitPlatform;
			this.movingPlatform.lastMatrix = this.movingPlatform.hitPlatform.localToWorldMatrix;
			this.movingPlatform.newPlatform = true;
		}
		Vector3 vector4 = new Vector3(vector.x, (float)0, vector.z);
		this.movement.velocity = (this.tr.position - position) / Time.deltaTime;
		Vector3 lhs2 = new Vector3(this.movement.velocity.x, (float)0, this.movement.velocity.z);
		if (vector4 == Vector3.zero)
		{
			this.movement.velocity = new Vector3((float)0, this.movement.velocity.y, (float)0);
		}
		else
		{
			float value = Vector3.Dot(lhs2, vector4) / vector4.sqrMagnitude;
			this.movement.velocity = vector4 * Mathf.Clamp01(value) + this.movement.velocity.y * Vector3.up;
		}
		if (this.movement.velocity.y < vector.y - 0.001f)
		{
			if (this.movement.velocity.y < (float)0)
			{
				this.movement.velocity.y = vector.y;
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
		}
		if (this.grounded && !this.IsGroundedTest())
		{
			this.grounded = false;
			if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer))
			{
				this.movement.frameVelocity = this.movingPlatform.platformVelocity;
				this.movement.velocity = this.movement.velocity + this.movingPlatform.platformVelocity;
			}
			this.SendMessage("OnFall", SendMessageOptions.DontRequireReceiver);
			this.tr.position = this.tr.position + d * Vector3.up;
		}
		else if (!this.grounded && this.IsGroundedTest())
		{
			this.grounded = true;
			this.jumping.jumping = false;
			this.StartCoroutine_Auto(this.SubtractNewPlatformVelocity());
			this.SendMessage("OnLand", SendMessageOptions.DontRequireReceiver);
		}
		if (this.MoveWithPlatform())
		{
			this.movingPlatform.activeGlobalPoint = this.tr.position + Vector3.up * (this.controller.center.y - this.controller.height * 0.5f + this.controller.radius);
			this.movingPlatform.activeLocalPoint = this.movingPlatform.activePlatform.InverseTransformPoint(this.movingPlatform.activeGlobalPoint);
			this.movingPlatform.activeGlobalRotation = this.tr.rotation;
			this.movingPlatform.activeLocalRotation = Quaternion.Inverse(this.movingPlatform.activePlatform.rotation) * this.movingPlatform.activeGlobalRotation;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002858 File Offset: 0x00000A58
	public virtual void FixedUpdate()
	{
		if (this.movingPlatform.enabled)
		{
			if (this.movingPlatform.activePlatform != null)
			{
				if (!this.movingPlatform.newPlatform)
				{
					Vector3 platformVelocity = this.movingPlatform.platformVelocity;
					this.movingPlatform.platformVelocity = (this.movingPlatform.activePlatform.localToWorldMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocalPoint) - this.movingPlatform.lastMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocalPoint)) / Time.deltaTime;
				}
				this.movingPlatform.lastMatrix = this.movingPlatform.activePlatform.localToWorldMatrix;
				this.movingPlatform.newPlatform = false;
			}
			else
			{
				this.movingPlatform.platformVelocity = Vector3.zero;
			}
		}
		if (this.useFixedUpdate)
		{
			this.UpdateFunction();
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000294C File Offset: 0x00000B4C
	public virtual void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.UpdateFunction();
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002960 File Offset: 0x00000B60
	private Vector3 ApplyInputVelocityChange(Vector3 velocity)
	{
		if (!this.canControl)
		{
			this.inputMoveDirection = Vector3.zero;
		}
		Vector3 vector = default(Vector3);
		if (this.grounded && this.TooSteep())
		{
			vector = new Vector3(this.groundNormal.x, (float)0, this.groundNormal.z).normalized;
			Vector3 vector2 = Vector3.Project(this.inputMoveDirection, vector);
			vector = vector + vector2 * this.sliding.speedControl + (this.inputMoveDirection - vector2) * this.sliding.sidewaysControl;
			vector *= this.sliding.slidingSpeed;
		}
		else
		{
			vector = this.GetDesiredHorizontalVelocity();
		}
		if (this.movingPlatform.enabled && this.movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer)
		{
			vector += this.movement.frameVelocity;
			vector.y = (float)0;
		}
		if (this.grounded)
		{
			vector = this.AdjustGroundVelocityToNormal(vector, this.groundNormal);
		}
		else
		{
			velocity.y = (float)0;
		}
		float num = this.GetMaxAcceleration(this.grounded) * Time.deltaTime;
		Vector3 b = vector - velocity;
		if (b.sqrMagnitude > num * num)
		{
			b = b.normalized * num;
		}
		if (this.grounded || this.canControl)
		{
			velocity += b;
		}
		if (this.grounded)
		{
			velocity.y = Mathf.Min(velocity.y, (float)0);
		}
		return velocity;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002B18 File Offset: 0x00000D18
	private Vector3 ApplyGravityAndJumping(Vector3 velocity)
	{
		if (!this.inputJump || !this.canControl)
		{
			this.jumping.holdingJumpButton = false;
			this.jumping.lastButtonDownTime = (float)-100;
		}
		if (this.inputJump && this.jumping.lastButtonDownTime < (float)0 && this.canControl)
		{
			this.jumping.lastButtonDownTime = Time.time;
		}
		if (this.grounded)
		{
			velocity.y = Mathf.Min((float)0, velocity.y) - this.movement.gravity * Time.deltaTime;
		}
		else
		{
			velocity.y = this.movement.velocity.y - this.movement.gravity * Time.deltaTime;
			if (this.jumping.jumping && this.jumping.holdingJumpButton && Time.time < this.jumping.lastStartTime + this.jumping.extraHeight / this.CalculateJumpVerticalSpeed(this.jumping.baseHeight))
			{
				velocity += this.jumping.jumpDir * this.movement.gravity * Time.deltaTime;
			}
			velocity.y = Mathf.Max(velocity.y, -this.movement.maxFallSpeed);
		}
		if (this.grounded)
		{
			if (this.jumping.enabled && this.canControl && Time.time - this.jumping.lastButtonDownTime < 0.2f)
			{
				this.grounded = false;
				this.jumping.jumping = true;
				this.jumping.lastStartTime = Time.time;
				this.jumping.lastButtonDownTime = (float)-100;
				this.jumping.holdingJumpButton = true;
				if (this.TooSteep())
				{
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this.groundNormal, this.jumping.steepPerpAmount);
				}
				else
				{
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this.groundNormal, this.jumping.perpAmount);
				}
				velocity.y = (float)0;
				velocity += this.jumping.jumpDir * this.CalculateJumpVerticalSpeed(this.jumping.baseHeight);
				if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer))
				{
					this.movement.frameVelocity = this.movingPlatform.platformVelocity;
					velocity += this.movingPlatform.platformVelocity;
				}
				this.SendMessage("OnJump", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
		}
		return velocity;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002E38 File Offset: 0x00001038
	public virtual void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.normal.y > (float)0 && hit.normal.y > this.groundNormal.y && hit.moveDirection.y < (float)0)
		{
			if ((hit.point - this.movement.lastHitPoint).sqrMagnitude > 0.001f || this.lastGroundNormal == Vector3.zero)
			{
				this.groundNormal = hit.normal;
			}
			else
			{
				this.groundNormal = this.lastGroundNormal;
			}
			this.movingPlatform.hitPlatform = hit.collider.transform;
			this.movement.hitPoint = hit.point;
			this.movement.frameVelocity = Vector3.zero;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002F20 File Offset: 0x00001120
	private IEnumerator SubtractNewPlatformVelocity()
	{
		return new CharacterMotor.$SubtractNewPlatformVelocity$53(this).GetEnumerator();
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002F30 File Offset: 0x00001130
	private bool MoveWithPlatform()
	{
		bool flag;
		if (flag = this.movingPlatform.enabled)
		{
			flag = (this.grounded ?? (this.movingPlatform.movementTransfer == MovementTransferOnJump.PermaLocked));
		}
		bool result;
		if (result = flag)
		{
			result = (this.movingPlatform.activePlatform != null);
		}
		return result;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002F84 File Offset: 0x00001184
	private Vector3 GetDesiredHorizontalVelocity()
	{
		Vector3 vector = this.tr.InverseTransformDirection(this.inputMoveDirection);
		float num = this.MaxSpeedInDirection(vector);
		if (this.grounded)
		{
			float time = Mathf.Asin(this.movement.velocity.normalized.y) * 57.29578f;
			num *= this.movement.slopeSpeedMultiplier.Evaluate(time);
		}
		return this.tr.TransformDirection(vector * num);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00003000 File Offset: 0x00001200
	private Vector3 AdjustGroundVelocityToNormal(Vector3 hVelocity, Vector3 groundNormal)
	{
		Vector3 lhs = Vector3.Cross(Vector3.up, hVelocity);
		return Vector3.Cross(lhs, groundNormal).normalized * hVelocity.magnitude;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00003038 File Offset: 0x00001238
	private bool IsGroundedTest()
	{
		return this.groundNormal.y > 0.01f;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000304C File Offset: 0x0000124C
	public virtual float GetMaxAcceleration(bool grounded)
	{
		return (!grounded) ? this.movement.maxAirAcceleration : this.movement.maxGroundAcceleration;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00003080 File Offset: 0x00001280
	public virtual float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt((float)2 * targetJumpHeight * this.movement.gravity);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00003098 File Offset: 0x00001298
	public virtual bool IsJumping()
	{
		return this.jumping.jumping;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000030A8 File Offset: 0x000012A8
	public virtual bool IsSliding()
	{
		bool enabled;
		if (enabled = this.grounded)
		{
			enabled = this.sliding.enabled;
		}
		bool result;
		if (result = enabled)
		{
			result = this.TooSteep();
		}
		return result;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000030D0 File Offset: 0x000012D0
	public virtual bool IsTouchingCeiling()
	{
		return (this.movement.collisionFlags & CollisionFlags.Above) != CollisionFlags.None;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000030E8 File Offset: 0x000012E8
	public virtual bool IsGrounded()
	{
		return this.grounded;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000030F0 File Offset: 0x000012F0
	public virtual bool TooSteep()
	{
		return this.groundNormal.y <= Mathf.Cos(this.controller.slopeLimit * 0.017453292f);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00003124 File Offset: 0x00001324
	public virtual Vector3 GetDirection()
	{
		return this.inputMoveDirection;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000312C File Offset: 0x0000132C
	public virtual void SetControllable(bool controllable)
	{
		this.canControl = controllable;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003138 File Offset: 0x00001338
	public virtual float MaxSpeedInDirection(Vector3 desiredMovementDirection)
	{
		float result;
		if (desiredMovementDirection == Vector3.zero)
		{
			result = (float)0;
		}
		else
		{
			float num = ((desiredMovementDirection.z <= (float)0) ? this.movement.maxBackwardsSpeed : this.movement.maxForwardSpeed) / this.movement.maxSidewaysSpeed;
			Vector3 normalized = new Vector3(desiredMovementDirection.x, (float)0, desiredMovementDirection.z / num).normalized;
			float num2 = new Vector3(normalized.x, (float)0, normalized.z * num).magnitude * this.movement.maxSidewaysSpeed;
			result = num2;
		}
		return result;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000031F4 File Offset: 0x000013F4
	public virtual void SetVelocity(Vector3 velocity)
	{
		this.grounded = false;
		this.movement.velocity = velocity;
		this.movement.frameVelocity = Vector3.zero;
		this.SendMessage("OnExternalVelocity");
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003230 File Offset: 0x00001430
	public virtual void Main()
	{
	}

	// Token: 0x0400002C RID: 44
	public bool canControl;

	// Token: 0x0400002D RID: 45
	public bool useFixedUpdate;

	// Token: 0x0400002E RID: 46
	[NonSerialized]
	public Vector3 inputMoveDirection;

	// Token: 0x0400002F RID: 47
	[NonSerialized]
	public bool inputJump;

	// Token: 0x04000030 RID: 48
	public CharacterMotorMovement movement;

	// Token: 0x04000031 RID: 49
	public CharacterMotorJumping jumping;

	// Token: 0x04000032 RID: 50
	public CharacterMotorMovingPlatform movingPlatform;

	// Token: 0x04000033 RID: 51
	public CharacterMotorSliding sliding;

	// Token: 0x04000034 RID: 52
	[NonSerialized]
	public bool grounded;

	// Token: 0x04000035 RID: 53
	[NonSerialized]
	public Vector3 groundNormal;

	// Token: 0x04000036 RID: 54
	private Vector3 lastGroundNormal;

	// Token: 0x04000037 RID: 55
	private Transform tr;

	// Token: 0x04000038 RID: 56
	private CharacterController controller;

	// Token: 0x02000008 RID: 8
	[CompilerGenerated]
	[Serializable]
	internal sealed class $SubtractNewPlatformVelocity$53 : GenericGenerator<object>
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00003234 File Offset: 0x00001434
		public $SubtractNewPlatformVelocity$53(CharacterMotor self_)
		{
			this.$self_$56 = self_;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003244 File Offset: 0x00001444
		public override IEnumerator<object> GetEnumerator()
		{
			return new CharacterMotor.$SubtractNewPlatformVelocity$53.$(this.$self_$56);
		}

		// Token: 0x04000039 RID: 57
		internal CharacterMotor $self_$56;
	}
}
