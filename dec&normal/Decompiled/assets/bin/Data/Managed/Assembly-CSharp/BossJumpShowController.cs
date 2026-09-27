using System;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class BossJumpShowController : MonoBehaviour
{
	// Token: 0x060010CE RID: 4302 RVA: 0x0006D200 File Offset: 0x0006B400
	private void Awake()
	{
		this.mAnimation = base.gameObject.GetComponentInChildren<Animation>();
		this.mTransform = base.transform;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0006D220 File Offset: 0x0006B420
	private void Update()
	{
		this.mUseTime += Time.deltaTime;
		if (this.mUseTime >= this.mDuration)
		{
			base.enabled = false;
			if (this.OnBossJumpOver != null)
			{
				this.OnBossJumpOver();
			}
			Object.Destroy(this);
			return;
		}
		this.CheckPos();
		this.CheckAnima();
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0006D280 File Offset: 0x0006B480
	public void Play(float hPos, Vector3 startPos, Vector3 targetPos, string modelName, string modelTypeName, BossJumpShowController.OnBossJumpOverDelegate func = null)
	{
		this.OnBossJumpOver = func;
		this.mParamB = Mathf.Sqrt(hPos / this.gravity);
		this.mHpos = hPos;
		float num = targetPos.y - startPos.y;
		this.mMoveDuration = Mathf.Sqrt((hPos - num) / this.gravity) + this.mParamB;
		this.mDuration = this.mMoveDuration + this.mJumpUpStartTime + this.mJumpDownEndTime;
		this.mStartPos = startPos;
		this.mTargetPos = targetPos;
		this.mModelName = modelName;
		this.mModelTypeName = modelTypeName;
		this.mUseTime = 0f;
		this.mJumpStep = 0;
		this.mTransform.position = this.mStartPos;
		this.mStartAngle = MathUtil.Heading(this.mTransform.forward);
		this.mTargetAngle = MathUtil.Heading(new Vector3(this.mTargetPos.x - this.mStartPos.x, 0f, this.mTargetPos.z - this.mStartPos.z));
		if (this.mAnimation[this.mJumpUpAnimaName] == null)
		{
			this.LoadAnim(this.mJumpUpAnimaName);
		}
		if (this.mAnimation[this.mJumpLoopAnimaName] == null)
		{
			this.LoadAnim(this.mJumpLoopAnimaName);
		}
		if (this.mAnimation[this.mJumpDownAnimaName] == null)
		{
			this.LoadAnim(this.mJumpDownAnimaName);
		}
		this.mJumpUpLength = this.mAnimation[this.mJumpUpAnimaName].length;
		this.mJumpDownLength = this.mAnimation[this.mJumpDownAnimaName].length;
		this.mAnimation.Play(this.mJumpUpAnimaName);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0006D454 File Offset: 0x0006B654
	private void CheckPos()
	{
		float num = this.mUseTime - this.mJumpUpStartTime;
		if (num < 0f)
		{
			this.mTransform.forward = MathUtil.HeadingToVector3(Mathf.LerpAngle(this.mStartAngle, this.mTargetAngle, Mathf.Clamp01(this.mUseTime / this.mJumpUpStartTime)));
			return;
		}
		if (num > this.mMoveDuration)
		{
			this.mTransform.position = this.mTargetPos;
			return;
		}
		float num2 = -this.gravity * (num - this.mParamB) * (num - this.mParamB) + this.mHpos;
		this.mTransform.position = VectorXZ.Lerp(this.mStartPos, this.mTargetPos, Mathf.Clamp01(num / this.mMoveDuration)) + Vector3.up * (num2 + this.mStartPos.y);
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0006D53C File Offset: 0x0006B73C
	private void CheckAnima()
	{
		if (this.mJumpStep == 0)
		{
			if (this.mUseTime > this.mJumpUpLength - 0.2f)
			{
				this.mAnimation.CrossFade(this.mJumpLoopAnimaName);
				this.mJumpStep = 1;
			}
		}
		else if (this.mJumpStep == 1 && this.mUseTime > this.mDuration - this.mJumpDownLength + 0.3f)
		{
			this.mAnimation.CrossFade(this.mJumpDownAnimaName);
			this.mJumpStep = 2;
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0006D5CC File Offset: 0x0006B7CC
	public bool LoadAnim(string animaName)
	{
		AnimationClip animationClip = AnimationManager.LoadAnimation(this.mModelName, animaName) as AnimationClip;
		if (animationClip != null)
		{
			this.mAnimation.AddClip(animationClip, animaName);
			return true;
		}
		animationClip = (AnimationManager.LoadAnimation(this.mModelTypeName, animaName) as AnimationClip);
		if (animationClip != null)
		{
			this.mAnimation.AddClip(animationClip, animaName);
			return true;
		}
		return false;
	}

	// Token: 0x04001454 RID: 5204
	private BossJumpShowController.OnBossJumpOverDelegate OnBossJumpOver;

	// Token: 0x04001455 RID: 5205
	private Animation mAnimation;

	// Token: 0x04001456 RID: 5206
	private Transform mTransform;

	// Token: 0x04001457 RID: 5207
	private string mJumpUpAnimaName = "jump up";

	// Token: 0x04001458 RID: 5208
	private string mJumpDownAnimaName = "jump down";

	// Token: 0x04001459 RID: 5209
	private string mJumpLoopAnimaName = "jump loop";

	// Token: 0x0400145A RID: 5210
	private string mModelName;

	// Token: 0x0400145B RID: 5211
	private string mModelTypeName;

	// Token: 0x0400145C RID: 5212
	private float mJumpUpLength;

	// Token: 0x0400145D RID: 5213
	private float mJumpDownLength;

	// Token: 0x0400145E RID: 5214
	private Vector3 mStartPos;

	// Token: 0x0400145F RID: 5215
	private Vector3 mTargetPos;

	// Token: 0x04001460 RID: 5216
	private float mDuration;

	// Token: 0x04001461 RID: 5217
	private float mMoveDuration;

	// Token: 0x04001462 RID: 5218
	private float mUseTime;

	// Token: 0x04001463 RID: 5219
	private int mJumpStep;

	// Token: 0x04001464 RID: 5220
	private float mJumpUpStartTime = 0.8f;

	// Token: 0x04001465 RID: 5221
	private float mJumpDownEndTime = 0.8f;

	// Token: 0x04001466 RID: 5222
	private float mHpos;

	// Token: 0x04001467 RID: 5223
	private float mParamB;

	// Token: 0x04001468 RID: 5224
	private float mStartAngle;

	// Token: 0x04001469 RID: 5225
	private float mTargetAngle;

	// Token: 0x0400146A RID: 5226
	private float gravity = 9.81f;

	// Token: 0x02000AC7 RID: 2759
	// (Invoke) Token: 0x06004FA5 RID: 20389
	public delegate void OnBossJumpOverDelegate();
}
