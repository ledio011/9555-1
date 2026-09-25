using System;
using UnityEngine;

// Token: 0x02000844 RID: 2116
public class ThirdPersonController : MonoBehaviour
{
	// Token: 0x17000EF5 RID: 3829
	// (get) Token: 0x06003656 RID: 13910 RVA: 0x000DF74C File Offset: 0x000DD94C
	// (set) Token: 0x06003657 RID: 13911 RVA: 0x000DF754 File Offset: 0x000DD954
	public bool IsMoving
	{
		get
		{
			return this.mIsMoving;
		}
		set
		{
			this.mIsMoving = value;
		}
	}

	// Token: 0x17000EF6 RID: 3830
	// (get) Token: 0x06003658 RID: 13912 RVA: 0x000DF760 File Offset: 0x000DD960
	// (set) Token: 0x06003659 RID: 13913 RVA: 0x000DF768 File Offset: 0x000DD968
	public bool IsJoyStickPress
	{
		get
		{
			return this.mIsJoyStickPress;
		}
		set
		{
			this.mIsJoyStickPress = value;
		}
	}

	// Token: 0x17000EF7 RID: 3831
	// (get) Token: 0x0600365A RID: 13914 RVA: 0x000DF774 File Offset: 0x000DD974
	// (set) Token: 0x0600365B RID: 13915 RVA: 0x000DF77C File Offset: 0x000DD97C
	public float VerticalRaw
	{
		get
		{
			return this.mVerticalRaw;
		}
		set
		{
			this.mVerticalRaw = value;
		}
	}

	// Token: 0x17000EF8 RID: 3832
	// (get) Token: 0x0600365C RID: 13916 RVA: 0x000DF788 File Offset: 0x000DD988
	// (set) Token: 0x0600365D RID: 13917 RVA: 0x000DF790 File Offset: 0x000DD990
	public float HorizonRaw
	{
		get
		{
			return this.mHorizonRaw;
		}
		set
		{
			this.mHorizonRaw = value;
		}
	}

	// Token: 0x0600365E RID: 13918 RVA: 0x000DF79C File Offset: 0x000DD99C
	private void UpdateMove()
	{
		if (this.mainPlayer == null)
		{
			if (Singleton<ObjManager>.Instance.MainPlayer == null)
			{
				return;
			}
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (!this.IsJoyStickPress && !this.keyboardFlag)
		{
			if (this.IsMoving)
			{
				this.mIsMoving = false;
				this.mMoveSpeed = 0f;
				this.mainPlayer.DisactiveTargetArriveFinish();
				this.mainPlayer.StopMove();
			}
			return;
		}
		if (this.cacheCameraTransform == null)
		{
			this.cacheCameraTransform = Camera.main.transform;
			if (this.cacheCameraTransform == null)
			{
				return;
			}
		}
		this.mIsMoving = (Mathf.Abs(this.mVerticalRaw) > 0.1f || Mathf.Abs(this.mHorizonRaw) > 0.1f);
		if (this.mIsMoving)
		{
			Vector3 vector = this.cacheCameraTransform.TransformDirection(Vector3.forward);
			vector.y = 0f;
			vector = vector.normalized;
			Vector3 vector2;
			vector2..ctor(vector.z, 0f, -vector.x);
			Vector3 vector3 = this.mVerticalRaw * vector2 + this.mHorizonRaw * vector;
			if (vector3 != Vector3.zero)
			{
				if (this.mMoveSpeed < this.mWalkSpeed * 0.9f)
				{
					this.moveDirection = vector3.normalized;
				}
				else
				{
					this.moveDirection = Vector3.RotateTowards(this.moveDirection, vector3, this.mRrotateSpeed * 0.017453292f * Time.deltaTime, 1000f);
					this.moveDirection = this.moveDirection.normalized;
				}
			}
			float num = this.mSpeedSmoothing * Time.deltaTime;
			float num2 = this.mainPlayer.mMoveSpeed;
			this.mMoveSpeed = Mathf.Lerp(this.mMoveSpeed, num2, num);
			Vector3 vector4 = this.moveDirection * this.mMoveSpeed;
			vector4 *= Time.deltaTime;
			Vector3 pos = this.mainPlayer.transform.localPosition + vector4 * 10f;
			this.mainPlayer.MoveTo(pos, 0.01f, null);
			this.mainPlayer.FollowServerID = -1L;
			this.mainPlayer.BreakAutoCombatState();
			if (this.missionManager == null)
			{
				this.missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			}
			this.missionManager.StopAutoMoveToMission();
			this.mainPlayer.IsNeedAutoMountCar = false;
		}
		else
		{
			this.mMoveSpeed = 0f;
			this.mainPlayer.DisactiveTargetArriveFinish();
			this.mainPlayer.StopMove();
		}
	}

	// Token: 0x0600365F RID: 13919 RVA: 0x000DFA60 File Offset: 0x000DDC60
	private void Start()
	{
		this.moveDirection = base.transform.TransformDirection(Vector3.forward);
	}

	// Token: 0x06003660 RID: 13920 RVA: 0x000DFA78 File Offset: 0x000DDC78
	private void Update()
	{
		this.UpdateMove();
	}

	// Token: 0x040023D1 RID: 9169
	public float mWalkSpeed = 2f;

	// Token: 0x040023D2 RID: 9170
	public float mSpeedSmoothing = 10f;

	// Token: 0x040023D3 RID: 9171
	public float mRrotateSpeed = 500f;

	// Token: 0x040023D4 RID: 9172
	private float mLockCameraTimer;

	// Token: 0x040023D5 RID: 9173
	private Vector3 mMoveDirection = Vector3.zero;

	// Token: 0x040023D6 RID: 9174
	private float mMoveSpeed;

	// Token: 0x040023D7 RID: 9175
	private bool mIsMoving;

	// Token: 0x040023D8 RID: 9176
	private bool mIsJoyStickPress;

	// Token: 0x040023D9 RID: 9177
	private float mVerticalRaw;

	// Token: 0x040023DA RID: 9178
	private float mHorizonRaw;

	// Token: 0x040023DB RID: 9179
	private ObjMainPlayer mainPlayer;

	// Token: 0x040023DC RID: 9180
	private Transform cacheCameraTransform;

	// Token: 0x040023DD RID: 9181
	private Vector3 moveDirection;

	// Token: 0x040023DE RID: 9182
	private MissionManager missionManager;

	// Token: 0x040023DF RID: 9183
	private bool keyboardFlag;
}
