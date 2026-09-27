using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[RequireComponent(typeof(CharacterController))]
[Serializable]
public class ThirdPersonController : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x00003CA0 File Offset: 0x00001EA0
	public ThirdPersonController()
	{
		this.walkMaxAnimationSpeed = 0.75f;
		this.trotMaxAnimationSpeed = 1f;
		this.runMaxAnimationSpeed = 1f;
		this.jumpAnimationSpeed = 1.15f;
		this.landAnimationSpeed = 1f;
		this.walkSpeed = 2f;
		this.trotSpeed = 4f;
		this.runSpeed = 6f;
		this.inAirControlAcceleration = 3f;
		this.jumpHeight = 0.5f;
		this.gravity = 20f;
		this.speedSmoothing = 10f;
		this.rotateSpeed = 500f;
		this.trotAfterSeconds = 3f;
		this.canJump = true;
		this.jumpRepeatTime = 0.05f;
		this.jumpTimeout = 0.15f;
		this.groundedTimeout = 0.25f;
		this.moveDirection = Vector3.zero;
		this.lastJumpButtonTime = -10f;
		this.lastJumpTime = -1f;
		this.inAirVelocity = Vector3.zero;
		this.isControllable = true;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003DA8 File Offset: 0x00001FA8
	public virtual void Awake()
	{
		this.moveDirection = this.transform.TransformDirection(Vector3.forward);
		this._animation = (Animation)this.GetComponent(typeof(Animation));
		if (!this._animation)
		{
			Debug.Log("The character you would like to control doesn't have animations. Moving her might look weird.");
		}
		if (!this.idleAnimation)
		{
			this._animation = null;
			Debug.Log("No idle animation found. Turning off animations.");
		}
		if (!this.walkAnimation)
		{
			this._animation = null;
			Debug.Log("No walk animation found. Turning off animations.");
		}
		if (!this.runAnimation)
		{
			this._animation = null;
			Debug.Log("No run animation found. Turning off animations.");
		}
		if (!this.jumpPoseAnimation && this.canJump)
		{
			this._animation = null;
			Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003E90 File Offset: 0x00002090
	public virtual void UpdateSmoothedMovementDirection()
	{
		Transform transform = Camera.main.transform;
		bool flag = this.IsGrounded();
		Vector3 a = transform.TransformDirection(Vector3.forward);
		a.y = (float)0;
		a = a.normalized;
		Vector3 a2 = new Vector3(a.z, (float)0, -a.x);
		float axisRaw = Input.GetAxisRaw("Vertical");
		float axisRaw2 = Input.GetAxisRaw("Horizontal");
		if (axisRaw < -0.2f)
		{
			this.movingBack = true;
		}
		else
		{
			this.movingBack = false;
		}
		bool flag2 = this.isMoving;
		this.isMoving = ((Mathf.Abs(axisRaw2) > 0.1f) ?? (Mathf.Abs(axisRaw) > 0.1f));
		Vector3 vector = axisRaw2 * a2 + axisRaw * a;
		if (flag)
		{
			this.lockCameraTimer += Time.deltaTime;
			if (this.isMoving != flag2)
			{
				this.lockCameraTimer = (float)0;
			}
			if (vector != Vector3.zero)
			{
				if (this.moveSpeed < this.walkSpeed * 0.9f && flag)
				{
					this.moveDirection = vector.normalized;
				}
				else
				{
					this.moveDirection = Vector3.RotateTowards(this.moveDirection, vector, this.rotateSpeed * 0.017453292f * Time.deltaTime, (float)1000);
					this.moveDirection = this.moveDirection.normalized;
				}
			}
			float t = this.speedSmoothing * Time.deltaTime;
			float num = Mathf.Min(vector.magnitude, 1f);
			this._characterState = CharacterState.Idle;
			if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
			{
				num *= this.runSpeed;
				this._characterState = CharacterState.Running;
			}
			else if (Time.time - this.trotAfterSeconds > this.walkTimeStart)
			{
				num *= this.trotSpeed;
				this._characterState = CharacterState.Trotting;
			}
			else
			{
				num *= this.walkSpeed;
				this._characterState = CharacterState.Walking;
			}
			this.moveSpeed = Mathf.Lerp(this.moveSpeed, num, t);
			if (this.moveSpeed < this.walkSpeed * 0.3f)
			{
				this.walkTimeStart = Time.time;
			}
		}
		else
		{
			if (this.jumping)
			{
				this.lockCameraTimer = (float)0;
			}
			if (this.isMoving)
			{
				this.inAirVelocity += vector.normalized * Time.deltaTime * this.inAirControlAcceleration;
			}
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000412C File Offset: 0x0000232C
	public virtual void ApplyJumping()
	{
		if (this.lastJumpTime + this.jumpRepeatTime <= Time.time)
		{
			if (this.IsGrounded() && this.canJump && Time.time < this.lastJumpButtonTime + this.jumpTimeout)
			{
				this.verticalSpeed = this.CalculateJumpVerticalSpeed(this.jumpHeight);
				this.SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000041A0 File Offset: 0x000023A0
	public virtual void ApplyGravity()
	{
		if (this.isControllable)
		{
			bool button = Input.GetButton("Jump");
			if (this.jumping && !this.jumpingReachedApex && this.verticalSpeed <= (float)0)
			{
				this.jumpingReachedApex = true;
				this.SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
			}
			if (this.IsGrounded())
			{
				this.verticalSpeed = (float)0;
			}
			else
			{
				this.verticalSpeed -= this.gravity * Time.deltaTime;
			}
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000422C File Offset: 0x0000242C
	public virtual float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt((float)2 * targetJumpHeight * this.gravity);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00004240 File Offset: 0x00002440
	public virtual void DidJump()
	{
		this.jumping = true;
		this.jumpingReachedApex = false;
		this.lastJumpTime = Time.time;
		this.lastJumpStartHeight = this.transform.position.y;
		this.lastJumpButtonTime = (float)-10;
		this._characterState = CharacterState.Jumping;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00004290 File Offset: 0x00002490
	public virtual void Update()
	{
		if (!this.isControllable)
		{
			Input.ResetInputAxes();
		}
		if (Input.GetButtonDown("Jump"))
		{
			this.lastJumpButtonTime = Time.time;
		}
		this.UpdateSmoothedMovementDirection();
		this.ApplyGravity();
		this.ApplyJumping();
		Vector3 vector = this.moveDirection * this.moveSpeed + new Vector3((float)0, this.verticalSpeed, (float)0) + this.inAirVelocity;
		vector *= Time.deltaTime;
		CharacterController characterController = (CharacterController)this.GetComponent(typeof(CharacterController));
		this.collisionFlags = characterController.Move(vector);
		if (this._animation)
		{
			if (this._characterState == CharacterState.Jumping)
			{
				if (!this.jumpingReachedApex)
				{
					this._animation[this.jumpPoseAnimation.name].speed = this.jumpAnimationSpeed;
					this._animation[this.jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					this._animation.CrossFade(this.jumpPoseAnimation.name);
				}
				else
				{
					this._animation[this.jumpPoseAnimation.name].speed = -this.landAnimationSpeed;
					this._animation[this.jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					this._animation.CrossFade(this.jumpPoseAnimation.name);
				}
			}
			else if (characterController.velocity.sqrMagnitude < 0.1f)
			{
				this._animation.CrossFade(this.idleAnimation.name);
			}
			else if (this._characterState == CharacterState.Running)
			{
				this._animation[this.runAnimation.name].speed = Mathf.Clamp(characterController.velocity.magnitude, (float)0, this.runMaxAnimationSpeed);
				this._animation.CrossFade(this.runAnimation.name);
			}
			else if (this._characterState == CharacterState.Trotting)
			{
				this._animation[this.walkAnimation.name].speed = Mathf.Clamp(characterController.velocity.magnitude, (float)0, this.trotMaxAnimationSpeed);
				this._animation.CrossFade(this.walkAnimation.name);
			}
			else if (this._characterState == CharacterState.Walking)
			{
				this._animation[this.walkAnimation.name].speed = Mathf.Clamp(characterController.velocity.magnitude, (float)0, this.walkMaxAnimationSpeed);
				this._animation.CrossFade(this.walkAnimation.name);
			}
		}
		if (this.IsGrounded())
		{
			this.transform.rotation = Quaternion.LookRotation(this.moveDirection);
		}
		else
		{
			Vector3 forward = vector;
			forward.y = (float)0;
			if (forward.sqrMagnitude > 0.001f)
			{
				this.transform.rotation = Quaternion.LookRotation(forward);
			}
		}
		if (this.IsGrounded())
		{
			this.lastGroundedTime = Time.time;
			this.inAirVelocity = Vector3.zero;
			if (this.jumping)
			{
				this.jumping = false;
				this.SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000045EC File Offset: 0x000027EC
	public virtual void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.moveDirection.y > 0.01f)
		{
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00004618 File Offset: 0x00002818
	public virtual float GetSpeed()
	{
		return this.moveSpeed;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00004620 File Offset: 0x00002820
	public virtual bool IsJumping()
	{
		return this.jumping;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004628 File Offset: 0x00002828
	public virtual bool IsGrounded()
	{
		return (this.collisionFlags & CollisionFlags.Below) != CollisionFlags.None;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00004638 File Offset: 0x00002838
	public virtual Vector3 GetDirection()
	{
		return this.moveDirection;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00004640 File Offset: 0x00002840
	public virtual bool IsMovingBackwards()
	{
		return this.movingBack;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00004648 File Offset: 0x00002848
	public virtual float GetLockCameraTimer()
	{
		return this.lockCameraTimer;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00004650 File Offset: 0x00002850
	public virtual bool IsMoving()
	{
		return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00004684 File Offset: 0x00002884
	public virtual bool HasJumpReachedApex()
	{
		return this.jumpingReachedApex;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000468C File Offset: 0x0000288C
	public virtual bool IsGroundedWithTimeout()
	{
		return this.lastGroundedTime + this.groundedTimeout > Time.time;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000046A4 File Offset: 0x000028A4
	public virtual void Reset()
	{
		this.gameObject.tag = "Player";
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000046B8 File Offset: 0x000028B8
	public virtual void Main()
	{
	}

	// Token: 0x04000058 RID: 88
	public AnimationClip idleAnimation;

	// Token: 0x04000059 RID: 89
	public AnimationClip walkAnimation;

	// Token: 0x0400005A RID: 90
	public AnimationClip runAnimation;

	// Token: 0x0400005B RID: 91
	public AnimationClip jumpPoseAnimation;

	// Token: 0x0400005C RID: 92
	public float walkMaxAnimationSpeed;

	// Token: 0x0400005D RID: 93
	public float trotMaxAnimationSpeed;

	// Token: 0x0400005E RID: 94
	public float runMaxAnimationSpeed;

	// Token: 0x0400005F RID: 95
	public float jumpAnimationSpeed;

	// Token: 0x04000060 RID: 96
	public float landAnimationSpeed;

	// Token: 0x04000061 RID: 97
	private Animation _animation;

	// Token: 0x04000062 RID: 98
	private CharacterState _characterState;

	// Token: 0x04000063 RID: 99
	public float walkSpeed;

	// Token: 0x04000064 RID: 100
	public float trotSpeed;

	// Token: 0x04000065 RID: 101
	public float runSpeed;

	// Token: 0x04000066 RID: 102
	public float inAirControlAcceleration;

	// Token: 0x04000067 RID: 103
	public float jumpHeight;

	// Token: 0x04000068 RID: 104
	public float gravity;

	// Token: 0x04000069 RID: 105
	public float speedSmoothing;

	// Token: 0x0400006A RID: 106
	public float rotateSpeed;

	// Token: 0x0400006B RID: 107
	public float trotAfterSeconds;

	// Token: 0x0400006C RID: 108
	public bool canJump;

	// Token: 0x0400006D RID: 109
	private float jumpRepeatTime;

	// Token: 0x0400006E RID: 110
	private float jumpTimeout;

	// Token: 0x0400006F RID: 111
	private float groundedTimeout;

	// Token: 0x04000070 RID: 112
	private float lockCameraTimer;

	// Token: 0x04000071 RID: 113
	private Vector3 moveDirection;

	// Token: 0x04000072 RID: 114
	private float verticalSpeed;

	// Token: 0x04000073 RID: 115
	private float moveSpeed;

	// Token: 0x04000074 RID: 116
	private CollisionFlags collisionFlags;

	// Token: 0x04000075 RID: 117
	private bool jumping;

	// Token: 0x04000076 RID: 118
	private bool jumpingReachedApex;

	// Token: 0x04000077 RID: 119
	private bool movingBack;

	// Token: 0x04000078 RID: 120
	private bool isMoving;

	// Token: 0x04000079 RID: 121
	private float walkTimeStart;

	// Token: 0x0400007A RID: 122
	private float lastJumpButtonTime;

	// Token: 0x0400007B RID: 123
	private float lastJumpTime;

	// Token: 0x0400007C RID: 124
	private float lastJumpStartHeight;

	// Token: 0x0400007D RID: 125
	private Vector3 inAirVelocity;

	// Token: 0x0400007E RID: 126
	private float lastGroundedTime;

	// Token: 0x0400007F RID: 127
	private bool isControllable;
}
