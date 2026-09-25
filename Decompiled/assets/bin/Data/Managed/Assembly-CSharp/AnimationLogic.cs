using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000834 RID: 2100
public class AnimationLogic : MonoBehaviour
{
	// Token: 0x06003571 RID: 13681 RVA: 0x000D9AE0 File Offset: 0x000D7CE0
	public AnimationLogic()
	{
		List<string> list = new List<string>();
		list.Add("attack_P");
		list.Add("attack_p");
		this.beatonAnimationNameList = list;
		this.mIdleShowTime = 10f;
		this.PlayAnimationDataList = new List<PlayAnimationData>();
		this.talkActionName = "talk";
		base..ctor();
	}

	// Token: 0x17000ED4 RID: 3796
	// (get) Token: 0x06003572 RID: 13682 RVA: 0x000D9BF8 File Offset: 0x000D7DF8
	public Animation AnimaObj
	{
		get
		{
			return this.mAnimation;
		}
	}

	// Token: 0x17000ED5 RID: 3797
	// (get) Token: 0x06003573 RID: 13683 RVA: 0x000D9C00 File Offset: 0x000D7E00
	public AnimationState CurrentAnimationState
	{
		get
		{
			return this.mCurAnimationState;
		}
	}

	// Token: 0x17000ED6 RID: 3798
	// (get) Token: 0x06003574 RID: 13684 RVA: 0x000D9C08 File Offset: 0x000D7E08
	public string CurrentAnimationName
	{
		get
		{
			return this.mCurAnimationName;
		}
	}

	// Token: 0x17000ED7 RID: 3799
	// (get) Token: 0x06003575 RID: 13685 RVA: 0x000D9C10 File Offset: 0x000D7E10
	// (set) Token: 0x06003576 RID: 13686 RVA: 0x000D9C18 File Offset: 0x000D7E18
	public string NextActionName
	{
		get
		{
			return this.mNextActionName;
		}
		set
		{
			this.mNextActionName = value;
		}
	}

	// Token: 0x17000ED8 RID: 3800
	// (get) Token: 0x06003577 RID: 13687 RVA: 0x000D9C24 File Offset: 0x000D7E24
	// (set) Token: 0x06003578 RID: 13688 RVA: 0x000D9C2C File Offset: 0x000D7E2C
	public ActionData CurActionData
	{
		get
		{
			return this.mCurActionData;
		}
		set
		{
			this.mCurActionData = value;
		}
	}

	// Token: 0x17000ED9 RID: 3801
	// (get) Token: 0x06003579 RID: 13689 RVA: 0x000D9C38 File Offset: 0x000D7E38
	public float Duration
	{
		get
		{
			return this.mDuration;
		}
	}

	// Token: 0x17000EDA RID: 3802
	// (get) Token: 0x0600357A RID: 13690 RVA: 0x000D9C40 File Offset: 0x000D7E40
	public float CurAnimationLength
	{
		get
		{
			return this.mCurAnimationLength;
		}
	}

	// Token: 0x0600357B RID: 13691 RVA: 0x000D9C48 File Offset: 0x000D7E48
	public void AddPlayAnimationData(string actionName, float delayTime, string effInfoID)
	{
		if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
		{
			this.PlayAnimationDataList.Add(new PlayAnimationData(actionName, delayTime, effInfoID));
		}
		else
		{
			this.PlayAnimationDataList.Add(new PlayAnimationData(this.owner.IndexName + "_" + actionName, delayTime, effInfoID));
		}
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x000D9CDC File Offset: 0x000D7EDC
	public void Init(ObjCharacter owner)
	{
		if (this.mAnimation == null)
		{
			this.mAnimation = base.GetComponent<Animation>();
		}
		if (this.mAnimation == null)
		{
			this.mAnimation = base.GetComponentInChildren<Animation>();
		}
		this.owner = owner;
		if (this.mAnimation != null)
		{
			this.RegisterEvent();
			this.LoadDefaultAnima();
			this.mAnimaFitCtl = this.mAnimation.gameObject.GetComponent<AnimationFit>();
		}
		if (this.mNpcBeforeLoadAction != null)
		{
			if (this.mNpcBeforeLoadAction.AnimationWrapMode == 8)
			{
				this.PlayAnimation(this.mNpcBeforeLoadAction, null, -1f, 0.9f);
			}
			else
			{
				this.PlayAnimation(this.mNpcBeforeLoadAction, null, -1f, 0f);
			}
			this.mNpcBeforeLoadAction = null;
		}
		if (this.mSoundManager == null)
		{
			this.mSoundManager = SingletonDontDestoryUnity<SoundManager>.Instance;
		}
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x000D9DD0 File Offset: 0x000D7FD0
	private void LoadDefaultAnima()
	{
		ActionData actionDataByName = DataManager.GetActionDataByName(this.owner.GetActionNameNoName("idle"));
		this.LoadAnim(actionDataByName);
		actionDataByName = DataManager.GetActionDataByName(this.owner.GetActionNameNoName("run"));
		this.LoadAnim(actionDataByName);
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x000D9E1C File Offset: 0x000D801C
	public void PlayAnimation(int id, AnimationLogic.OnAnimFinished animFinished = null)
	{
		if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
		{
			this.PlayAnimation(this.animationName[id], animFinished, -1f);
		}
		else
		{
			this.PlayAnimation(DataManager.GetActionDataByName(this.animationName[id]), animFinished, -1f, 0f);
		}
	}

	// Token: 0x0600357F RID: 13695 RVA: 0x000D9EA4 File Offset: 0x000D80A4
	public void PlayAnimation(string actionName, AnimationLogic.OnAnimFinished animFinished = null, float duration = -1f)
	{
		ActionData actionDataByName = DataManager.GetActionDataByName(this.owner.GetActionName(actionName));
		if (actionDataByName == null)
		{
			Debug.Log("actionName == null !!!! actionName :: " + actionName);
			return;
		}
		this.PlayAnimation(actionDataByName, animFinished, duration, 0f);
	}

	// Token: 0x06003580 RID: 13696 RVA: 0x000D9EE8 File Offset: 0x000D80E8
	public void PlayAnimation(ActionData actionData, AnimationLogic.OnAnimFinished animFinished = null, float duration = -1f, float startTime = 0f)
	{
		if (actionData == null)
		{
			return;
		}
		if (this.mAnimation == null)
		{
			if (actionData.AnimationWrapMode == 2 || actionData.AnimationWrapMode == 4 || actionData.AnimationWrapMode == 8)
			{
				this.mNpcBeforeLoadAction = actionData;
			}
			else
			{
				this.mNpcBeforeLoadAction = null;
			}
			return;
		}
		this.InternalPlayAnimation(actionData, animFinished, duration, startTime);
	}

	// Token: 0x06003581 RID: 13697 RVA: 0x000D9F50 File Offset: 0x000D8150
	private void InternalPlayAnimation(ActionData actionData, AnimationLogic.OnAnimFinished animFinished = null, float duration = -1f, float startTime = 0f)
	{
		if (actionData == null)
		{
			return;
		}
		if (this.IsKnockDownFlag)
		{
			return;
		}
		if (!actionData.AnimName.Equals(this.dieAction) && !actionData.AnimName.Equals(this.knockDownAnimationName) && (this.IsStunFlag || this.IsSleepFlag))
		{
			return;
		}
		this.mPreActionData = this.mCurActionData;
		this.mCurActionData = actionData;
		if (!this.SetCurAnimationState(this.mCurActionData))
		{
			return;
		}
		this.mCurAnimationName = this.GetAnimationStateName(actionData);
		this.mCurAnimationLength = this.mAnimation[this.mCurAnimationName].length;
		if (actionData.AnimDurationTimeSecond > 1E-45f)
		{
			this.mAnimation[this.mCurAnimationName].speed = this.mAnimation[this.mCurAnimationName].length / actionData.AnimDurationTimeSecond;
			this.mCurAnimationLength = actionData.AnimDurationTimeSecond;
		}
		if (startTime > 1E-45f)
		{
			this.mAnimation[this.mCurAnimationName].normalizedTime = startTime;
		}
		if (this.mAnimaFitCtl != null)
		{
			this.mAnimaFitCtl.UpdateAnim(this.mCurAnimationName, this.GetCrossFadeTime());
		}
		this.mAnimation.CrossFade(this.mCurAnimationName, this.GetCrossFadeTime());
		this.PlaySoundAtPos(actionData.SoundID, this.owner.Position);
		if (this.mCurActionData.CrossOutTime != -1)
		{
			this.mCurActionCrossOutTime = this.mCurActionData.CrossOutTimeSecond;
		}
		else
		{
			this.mCurActionCrossOutTime = GameSettingData.DefaultCrossOutTime;
		}
		this.CheckAnimaState(this.mCurAnimationName);
		this.mCurAnimationStartTime = Time.time;
		if (this.onAnimFinished != null)
		{
			this.onAnimFinished();
		}
		this.onAnimFinished = animFinished;
		this.mDuration = duration;
		if (actionData.NextActionName != string.Empty)
		{
			this.mNextActionName = actionData.NextActionName;
		}
		else if (actionData.AnimationWrapMode == 2 || actionData.AnimationWrapMode == 8)
		{
			if (this.mDuration < 0f)
			{
				this.mNextActionName = string.Empty;
			}
			else if (!this.owner.IdleAttackFlag)
			{
				this.mNextActionName = this.owner.GetActionNameNoName(this.idleAction);
			}
			else
			{
				this.mNextActionName = this.owner.GetActionNameNoName(this.idleAttackAction);
			}
		}
		else if (!this.owner.IdleAttackFlag)
		{
			this.mNextActionName = this.owner.GetActionNameNoName(this.idleAction);
		}
		else
		{
			this.mNextActionName = this.owner.GetActionNameNoName(this.idleAttackAction);
		}
	}

	// Token: 0x06003582 RID: 13698 RVA: 0x000DA220 File Offset: 0x000D8420
	public string GetAnimationStateName(ActionData actionData)
	{
		if (this.owner.CurrentCharacterModelData.TypeID == 0)
		{
			return string.Format("{0}_{1}", actionData.GetAnimaFileName(), actionData.AnimName);
		}
		return actionData.AnimName;
	}

	// Token: 0x06003583 RID: 13699 RVA: 0x000DA260 File Offset: 0x000D8460
	private float GetCrossFadeTime()
	{
		float result = GameSettingData.DefaultCrossInTime;
		if (this.mCurActionData.CrossInTime != -1)
		{
			if (this.mPreActionData != null && this.mPreActionData.CrossOutTime != -1)
			{
				if (this.mPreActionData.CrossOutTimeSecond > this.mCurActionData.CrossInTimeSecond)
				{
					result = this.mPreActionData.CrossOutTimeSecond;
				}
				else
				{
					result = this.mCurActionData.CrossInTimeSecond;
				}
			}
			else
			{
				result = this.mCurActionData.CrossInTimeSecond;
			}
		}
		else if (this.mPreActionData != null && this.mPreActionData.CrossOutTime != -1)
		{
			result = this.mPreActionData.CrossOutTimeSecond;
		}
		return result;
	}

	// Token: 0x06003584 RID: 13700 RVA: 0x000DA318 File Offset: 0x000D8518
	public bool LoadAnim(ActionData actionData)
	{
		if (this.mAnimation[this.GetAnimationStateName(actionData)] != null)
		{
			return true;
		}
		if (this.owner.CurrentCharacterModelData.TypeID == 0)
		{
			AnimationClip animationClip = AnimationManager.LoadAnimation(actionData.GetAnimaFileName(), actionData.AnimName) as AnimationClip;
			if (animationClip != null)
			{
				animationClip.wrapMode = actionData.AnimationWrapMode;
				this.mAnimation.AddClip(animationClip, string.Format("{0}_{1}", actionData.GetAnimaFileName(), actionData.AnimName));
				return true;
			}
			return false;
		}
		else
		{
			AnimationClip animationClip2 = AnimationManager.LoadAnimation(this.owner.CurrentCharacterModelData.ModelFirstType, actionData.AnimName) as AnimationClip;
			if (animationClip2 != null)
			{
				animationClip2.wrapMode = actionData.AnimationWrapMode;
				this.mAnimation.AddClip(animationClip2, actionData.AnimName);
				return true;
			}
			animationClip2 = (AnimationManager.LoadAnimation(this.owner.CurrentCharacterModelData.ModelSubType, actionData.AnimName) as AnimationClip);
			if (animationClip2 != null)
			{
				animationClip2.wrapMode = actionData.AnimationWrapMode;
				this.mAnimation.AddClip(animationClip2, actionData.AnimName);
				return true;
			}
			animationClip2 = (AnimationManager.LoadAnimation(this.owner.CurrentCharacterModelData.ModelType, actionData.AnimName) as AnimationClip);
			if (animationClip2 != null)
			{
				animationClip2.wrapMode = actionData.AnimationWrapMode;
				this.mAnimation.AddClip(animationClip2, actionData.AnimName);
				return true;
			}
			return false;
		}
	}

	// Token: 0x06003585 RID: 13701 RVA: 0x000DA498 File Offset: 0x000D8698
	public void ForceSampleAnimation(string actionName, float normalizedTime, AnimationLogic.OnAnimFinished animFinished = null, float duration = -1f, float startTime = 0f)
	{
		ActionData actionDataByName = DataManager.GetActionDataByName(this.owner.GetActionNameNoName(actionName));
		if (actionDataByName == null)
		{
			return;
		}
		string animationStateName = this.GetAnimationStateName(actionDataByName);
		this.mAnimation.Stop(animationStateName);
		if (this.mAnimation[animationStateName] == null)
		{
			if (!this.LoadAnim(actionDataByName))
			{
				return;
			}
		}
		this.mAnimation.Play(animationStateName);
		this.mAnimation[animationStateName].normalizedTime = normalizedTime;
		this.mAnimation.Sample();
		this.mAnimation.Stop();
	}

	// Token: 0x06003586 RID: 13702 RVA: 0x000DA530 File Offset: 0x000D8730
	public void ForcePlayAnimation(string actionName, AnimationLogic.OnAnimFinished animFinished = null, float duration = -1f, float startTime = 0f)
	{
		ActionData actionDataByName = DataManager.GetActionDataByName(this.owner.GetActionNameNoName(actionName));
		if (this.mAnimation == null)
		{
			if (actionDataByName.AnimationWrapMode == 2 || actionDataByName.AnimationWrapMode == 4 || actionDataByName.AnimationWrapMode == 8)
			{
				this.mNpcBeforeLoadAction = actionDataByName;
			}
			else
			{
				this.mNpcBeforeLoadAction = null;
			}
			return;
		}
		this.mAnimation.Stop(this.GetAnimationStateName(actionDataByName));
		this.InternalPlayAnimation(actionDataByName, animFinished, duration, startTime);
	}

	// Token: 0x06003587 RID: 13703 RVA: 0x000DA5B8 File Offset: 0x000D87B8
	private void GoNextAnimation(string actionName)
	{
		this.mPreActionData = this.mCurActionData;
		this.mCurActionData = DataManager.GetActionDataByName(actionName);
		this.mCurAnimationName = this.GetAnimationStateName(this.mCurActionData);
		if (this.SetCurAnimationState(this.mCurActionData))
		{
			if (this.mCurActionData.CrossOutTime != -1)
			{
				this.mCurActionCrossOutTime = this.mCurActionData.CrossOutTimeSecond;
			}
			else
			{
				this.mCurActionCrossOutTime = GameSettingData.DefaultCrossOutTime;
			}
			if (this.mAnimaFitCtl != null)
			{
				this.mAnimaFitCtl.UpdateAnim(this.mCurAnimationName, this.GetCrossFadeTime());
			}
			this.mAnimation.CrossFade(this.mCurAnimationName, this.GetCrossFadeTime());
			this.PlaySoundAtPos(this.mCurActionData.SoundID, this.owner.Position);
			this.CheckAnimaState(this.mCurAnimationName);
			this.mCurAnimationStartTime = Time.time;
			this.mCurAnimationLength = this.mAnimation[this.mCurAnimationName].length;
			this.mNextActionName = string.Empty;
		}
	}

	// Token: 0x06003588 RID: 13704 RVA: 0x000DA6CC File Offset: 0x000D88CC
	private void CheckAnimaState(string animationName)
	{
		if (animationName.Equals(this.idleAnimationName))
		{
			this.IsIdleFlag = true;
		}
		else
		{
			this.IsIdleFlag = false;
			this.mIdleTimeCount = 0f;
		}
	}

	// Token: 0x06003589 RID: 13705 RVA: 0x000DA700 File Offset: 0x000D8900
	private bool SetCurAnimationState(ActionData curActData)
	{
		this.mCurAnimationState = this.mAnimation[this.GetAnimationStateName(curActData)];
		if (this.mCurAnimationState == null)
		{
			if (!this.LoadAnim(curActData))
			{
				return false;
			}
			this.mCurAnimationState = this.mAnimation[this.GetAnimationStateName(curActData)];
		}
		this.mCurAnimationState.wrapMode = curActData.AnimationWrapMode;
		return true;
	}

	// Token: 0x0600358A RID: 13706 RVA: 0x000DA774 File Offset: 0x000D8974
	public void CheckAnimationState()
	{
		if (this.mNextActionName != string.Empty)
		{
			if (this.mDuration < 0f)
			{
				if (this.CalCurAnimaLeftTime() < this.mCurActionCrossOutTime)
				{
					if (this.onAnimFinished != null)
					{
						this.tempAnimFinished = this.onAnimFinished;
						this.onAnimFinished = null;
						this.tempAnimFinished();
						return;
					}
					this.GoNextAnimation(this.mNextActionName);
				}
			}
			else
			{
				this.mDuration -= Time.deltaTime;
				if (this.mDuration <= 0f)
				{
					this.OnAnimaFinished();
					this.GoNextAnimation(this.mNextActionName);
					this.mDuration = -1f;
				}
			}
		}
		else if (this.onAnimFinished != null && this.CalCurAnimaLeftTime() < this.mCurActionCrossOutTime)
		{
			this.tempAnimFinished = this.onAnimFinished;
			this.onAnimFinished = null;
			this.tempAnimFinished();
		}
		if (this.PlayAnimationDataList.Count != 0)
		{
			for (int i = this.PlayAnimationDataList.Count - 1; i >= 0; i--)
			{
				this.PlayAnimationDataList[i].DelayTime -= Time.deltaTime;
				if (this.PlayAnimationDataList[i].DelayTime <= 0f)
				{
					if (!this.owner.IsDie && this.mCurActionData.AnimCanBeBreak == 1)
					{
						this.ForcePlayAnimation(this.PlayAnimationDataList[i].ActionName, null, -1f, 0f);
						this.OnAnimaFinished();
					}
					this.PlayAnimationDataList.RemoveAt(i);
				}
			}
		}
		if (this.mCurAnimationState == null)
		{
			if (!this.owner.IsDie && !this.owner.IdleAttackFlag)
			{
				this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAction));
			}
			else
			{
				this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAttackAction));
			}
		}
		if (!this.mAnimation.IsPlaying(this.mCurAnimationName))
		{
			if (this.mCurActionData.AnimName.Equals(this.walkAction) || this.mCurActionData.AnimName.Equals(this.runAction) || this.mCurActionData.AnimName.Equals(this.attackRunAction) || this.mCurActionData.AnimName.Equals(this.dieAction))
			{
				this.GoNextAnimation(this.mCurActionData.ID);
			}
			else if (!this.owner.IdleAttackFlag)
			{
				this.OnAnimaFinished();
				this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAction));
			}
			else
			{
				this.OnAnimaFinished();
				this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAttackAction));
			}
		}
		if (this.onAnimFinished == null && (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER) && this.IsIdleFlag)
		{
			this.mIdleTimeCount += Time.deltaTime;
			if (this.mIdleTimeCount > this.mIdleShowTime)
			{
				this.mIdleTimeCount = 0f;
				this.mIdleShowTime = (float)Random.Range(10, 20);
				if (Random.Range(0, 100) > 70)
				{
					this.PlayAnimation(this.idleShowAction, null, -1f);
				}
			}
		}
	}

	// Token: 0x0600358B RID: 13707 RVA: 0x000DAB10 File Offset: 0x000D8D10
	public void OnAnimaFinished()
	{
		if (this.onAnimFinished != null)
		{
			this.tempAnimFinished = this.onAnimFinished;
			this.onAnimFinished = null;
			this.tempAnimFinished();
		}
	}

	// Token: 0x0600358C RID: 13708 RVA: 0x000DAB3C File Offset: 0x000D8D3C
	private float CalCurAnimaNormalizedTime()
	{
		return Mathf.Clamp01((Time.time - this.mCurAnimationStartTime) / this.mCurAnimationLength);
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x000DAB58 File Offset: 0x000D8D58
	private float CalCurAnimaLeftTime()
	{
		return this.mCurAnimationLength - (Time.time - this.mCurAnimationStartTime);
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x000DAB70 File Offset: 0x000D8D70
	public bool AnimationPlayCheck(PlayAnimationData playAnimationData)
	{
		if (this.mCurActionData.AnimCanBeBreak == 1)
		{
			return true;
		}
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(playAnimationData.EffInfoID);
		return effInfoDataById.MoveDistance != 0 && effInfoDataById.ForceMove != 1;
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x000DABB8 File Offset: 0x000D8DB8
	public void AnimationLogicUpdate()
	{
		if (this.mAnimation == null || !base.enabled)
		{
			return;
		}
		this.CheckAnimationState();
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x000DABE0 File Offset: 0x000D8DE0
	public void DisableAnimationLogic()
	{
		base.enabled = false;
		if (this.mAnimation != null)
		{
			this.mAnimation.Stop();
		}
	}

	// Token: 0x06003591 RID: 13713 RVA: 0x000DAC08 File Offset: 0x000D8E08
	public void EnableAnimationLogic()
	{
		base.enabled = true;
	}

	// Token: 0x06003592 RID: 13714 RVA: 0x000DAC14 File Offset: 0x000D8E14
	public void BreakCurAnima()
	{
		if (this.mCurActionData != null)
		{
			this.mSoundManager.StopSoundEffect(this.mCurActionData.SoundID);
		}
		if (!this.owner.IsDie)
		{
			if (!this.owner.IdleAttackFlag)
			{
				this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAction));
			}
			else
			{
				this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAttackAction));
			}
		}
	}

	// Token: 0x06003593 RID: 13715 RVA: 0x000DAC98 File Offset: 0x000D8E98
	public void OnStun(BuffInfoData buffInfoData)
	{
		if (!this.owner.IsDie)
		{
			if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
			{
				this.ForcePlayAnimation(buffInfoData.Action, null, -1f, 0f);
			}
			else
			{
				this.ForcePlayAnimation(string.Format("{0}_{1}", this.owner.IndexName, buffInfoData.Action), null, -1f, 0f);
			}
			this.IsStunFlag = true;
		}
	}

	// Token: 0x06003594 RID: 13716 RVA: 0x000DAD48 File Offset: 0x000D8F48
	public void OnStunDone(BuffInfoData buffInfoData)
	{
		this.IsStunFlag = false;
		if (!this.owner.IsDie)
		{
			this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAttackAction));
		}
	}

	// Token: 0x06003595 RID: 13717 RVA: 0x000DAD84 File Offset: 0x000D8F84
	public void OnKnockDown(BuffInfoData buffInfoData, ObjCharacter sender)
	{
		if (!this.owner.IsDie)
		{
			if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
			{
				this.ForcePlayAnimation(buffInfoData.Action, null, -1f, 0f);
			}
			else
			{
				this.ForcePlayAnimation(string.Format("{0}_{1}", this.owner.IndexName, buffInfoData.Action), null, -1f, 0f);
			}
			this.IsKnockDownFlag = true;
		}
	}

	// Token: 0x06003596 RID: 13718 RVA: 0x000DAE34 File Offset: 0x000D9034
	public void OnKnockDownDone(BuffInfoData buffInfoData)
	{
		this.IsKnockDownFlag = false;
		if (!this.owner.IsDie)
		{
			this.GoNextAnimation(this.owner.GetActionName(this.getUpAction));
		}
	}

	// Token: 0x06003597 RID: 13719 RVA: 0x000DAE70 File Offset: 0x000D9070
	public void OnSleep(BuffInfoData buffInfoData)
	{
		if (!this.owner.IsDie)
		{
			this.IsSleepFlag = true;
		}
	}

	// Token: 0x06003598 RID: 13720 RVA: 0x000DAE8C File Offset: 0x000D908C
	public void OnSleepDone(BuffInfoData buffInfoData)
	{
		this.IsSleepFlag = false;
		if (!this.owner.IsDie)
		{
			this.GoNextAnimation(this.owner.GetActionNameNoName(this.idleAction));
		}
	}

	// Token: 0x06003599 RID: 13721 RVA: 0x000DAEC8 File Offset: 0x000D90C8
	private void Destroy()
	{
		this.DeRegisterEvent();
	}

	// Token: 0x0600359A RID: 13722 RVA: 0x000DAED0 File Offset: 0x000D90D0
	private void RegisterEvent()
	{
		this.owner.BuffLogic.RegisterOnStun(new BuffLogic.BuffDelegate(this.OnStun));
		this.owner.BuffLogic.RegisterOnStunDone(new BuffLogic.BuffDelegate(this.OnStunDone));
		this.owner.BuffLogic.RegisterOnSleep(new BuffLogic.BuffDelegate(this.OnSleep));
		this.owner.BuffLogic.RegisterOnSleepDone(new BuffLogic.BuffDelegate(this.OnSleepDone));
		this.owner.BuffLogic.RegisterOnKnockDown(new BuffLogic.BuffSenderDelegate(this.OnKnockDown));
		this.owner.BuffLogic.RegisterOnKnockDownDone(new BuffLogic.BuffDelegate(this.OnKnockDownDone));
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x000DAF88 File Offset: 0x000D9188
	private void DeRegisterEvent()
	{
		this.owner.BuffLogic.DeRegisterOnStun(new BuffLogic.BuffDelegate(this.OnStun));
		this.owner.BuffLogic.DeRegisterOnStunDone(new BuffLogic.BuffDelegate(this.OnStunDone));
		this.owner.BuffLogic.DeRegisterOnSleep(new BuffLogic.BuffDelegate(this.OnSleep));
		this.owner.BuffLogic.DeRegisterOnSleepDone(new BuffLogic.BuffDelegate(this.OnSleepDone));
		this.owner.BuffLogic.DeRegisterOnKnockDown(new BuffLogic.BuffSenderDelegate(this.OnKnockDown));
		this.owner.BuffLogic.DeRegisterOnKnockDownDone(new BuffLogic.BuffDelegate(this.OnKnockDownDone));
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x000DB040 File Offset: 0x000D9240
	public void PrintPLayerMessage(string message)
	{
		if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x000DB05C File Offset: 0x000D925C
	public void PlayTalkAnimation()
	{
		this.ForcePlayAnimation(this.talkActionName, null, -1f, 0f);
	}

	// Token: 0x0600359E RID: 13726 RVA: 0x000DB078 File Offset: 0x000D9278
	public void ClearAllAnima()
	{
		if (this.mAnimation != null)
		{
			this.mAnimation.Stop();
			foreach (object obj in this.mAnimation)
			{
				AnimationState animationState = (AnimationState)obj;
				this.mAnimation.RemoveClip(animationState.name);
			}
		}
	}

	// Token: 0x0600359F RID: 13727 RVA: 0x000DB110 File Offset: 0x000D9310
	public void ResetAnimationFlag()
	{
		this.IsStunFlag = false;
		this.IsSleepFlag = false;
		this.IsKnockDownFlag = false;
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x000DB128 File Offset: 0x000D9328
	private void PlaySoundAtPos(int nSoundID, Vector3 playingPos)
	{
		if (this.mSoundManager == null)
		{
			if (SingletonDontDestoryUnity<SoundManager>.Exists)
			{
				this.mSoundManager = SingletonDontDestoryUnity<SoundManager>.Instance;
			}
			if (this.mSoundManager == null)
			{
				return;
			}
		}
		if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			this.mSoundManager.PlaySoundEffect(nSoundID, 1f, null);
		}
		else if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			this.mSoundManager.PlaySoundEffectAtPos2(nSoundID, playingPos, Singleton<ObjManager>.Instance.MainPlayer.Position);
		}
		else
		{
			this.mSoundManager.PlaySoundEffect(nSoundID, 1f, null);
		}
	}

	// Token: 0x040022EA RID: 8938
	public string[] animationName = new string[]
	{
		"idle",
		"run",
		"walk",
		"die",
		"idle_attack",
		"kaiChe"
	};

	// Token: 0x040022EB RID: 8939
	private Animation mAnimation;

	// Token: 0x040022EC RID: 8940
	private AnimationState mCurAnimationState;

	// Token: 0x040022ED RID: 8941
	private string mCurAnimationName = string.Empty;

	// Token: 0x040022EE RID: 8942
	private string mNextActionName = string.Empty;

	// Token: 0x040022EF RID: 8943
	private ActionData mCurActionData;

	// Token: 0x040022F0 RID: 8944
	private float mCurActionCrossOutTime;

	// Token: 0x040022F1 RID: 8945
	private ActionData mPreActionData;

	// Token: 0x040022F2 RID: 8946
	private float mDuration;

	// Token: 0x040022F3 RID: 8947
	public string idleAction = "idle";

	// Token: 0x040022F4 RID: 8948
	public string idleShowAction = "show";

	// Token: 0x040022F5 RID: 8949
	public ActionData idleShowActionData;

	// Token: 0x040022F6 RID: 8950
	public string walkAction = "walk";

	// Token: 0x040022F7 RID: 8951
	public string runAction = "run";

	// Token: 0x040022F8 RID: 8952
	public string attackRunAction = "run_naQiang";

	// Token: 0x040022F9 RID: 8953
	public string idleAttackAction = "idle_attack";

	// Token: 0x040022FA RID: 8954
	public string getUpAction = "getUp";

	// Token: 0x040022FB RID: 8955
	public string dieAction = "die";

	// Token: 0x040022FC RID: 8956
	public string idleAnimationName = "idle";

	// Token: 0x040022FD RID: 8957
	public string knockDownAnimationName = "knockDown";

	// Token: 0x040022FE RID: 8958
	public List<string> beatonAnimationNameList;

	// Token: 0x040022FF RID: 8959
	public AnimationLogic.OnAnimFinished onAnimFinished;

	// Token: 0x04002300 RID: 8960
	public AnimationLogic.OnAnimFinished tempAnimFinished;

	// Token: 0x04002301 RID: 8961
	private ObjCharacter owner;

	// Token: 0x04002302 RID: 8962
	private bool IsStunFlag;

	// Token: 0x04002303 RID: 8963
	private bool IsKnockDownFlag;

	// Token: 0x04002304 RID: 8964
	private bool IsSleepFlag;

	// Token: 0x04002305 RID: 8965
	private float mCurAnimationStartTime;

	// Token: 0x04002306 RID: 8966
	private float mCurAnimationLength;

	// Token: 0x04002307 RID: 8967
	private float mIdleTimeCount;

	// Token: 0x04002308 RID: 8968
	private float mIdleShowTime;

	// Token: 0x04002309 RID: 8969
	private bool IsIdleFlag;

	// Token: 0x0400230A RID: 8970
	private ActionData mNpcBeforeLoadAction;

	// Token: 0x0400230B RID: 8971
	private SoundManager mSoundManager;

	// Token: 0x0400230C RID: 8972
	public List<PlayAnimationData> PlayAnimationDataList;

	// Token: 0x0400230D RID: 8973
	private AnimationFit mAnimaFitCtl;

	// Token: 0x0400230E RID: 8974
	private string talkActionName;

	// Token: 0x02000ADF RID: 2783
	// (Invoke) Token: 0x06005005 RID: 20485
	public delegate void OnAnimFinished();
}
