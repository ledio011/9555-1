using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000864 RID: 2148
public class SkillLogic
{
	// Token: 0x17000F39 RID: 3897
	// (get) Token: 0x060037BE RID: 14270 RVA: 0x000E5B40 File Offset: 0x000E3D40
	// (set) Token: 0x060037BF RID: 14271 RVA: 0x000E5B48 File Offset: 0x000E3D48
	public ObjCharacter SkillSender
	{
		get
		{
			return this.mSkillSender;
		}
		set
		{
			this.mSkillSender = value;
		}
	}

	// Token: 0x17000F3A RID: 3898
	// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000E5B54 File Offset: 0x000E3D54
	// (set) Token: 0x060037C1 RID: 14273 RVA: 0x000E5B5C File Offset: 0x000E3D5C
	public bool IsUsingSkill
	{
		get
		{
			return this.mIsUsingSkill;
		}
		set
		{
			this.mIsUsingSkill = value;
		}
	}

	// Token: 0x17000F3B RID: 3899
	// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000E5B68 File Offset: 0x000E3D68
	// (set) Token: 0x060037C3 RID: 14275 RVA: 0x000E5B70 File Offset: 0x000E3D70
	public int LastSkillId
	{
		get
		{
			return this.mLastSkillId;
		}
		set
		{
			this.mLastSkillId = value;
		}
	}

	// Token: 0x17000F3C RID: 3900
	// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000E5B7C File Offset: 0x000E3D7C
	// (set) Token: 0x060037C5 RID: 14277 RVA: 0x000E5B84 File Offset: 0x000E3D84
	public SkillData UsingSkillData
	{
		get
		{
			return this.mUsingSkillData;
		}
		set
		{
			this.mUsingSkillData = value;
		}
	}

	// Token: 0x17000F3D RID: 3901
	// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000E5B90 File Offset: 0x000E3D90
	// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000E5B98 File Offset: 0x000E3D98
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

	// Token: 0x060037C8 RID: 14280 RVA: 0x000E5BA4 File Offset: 0x000E3DA4
	public bool ServerUseSkill(string skillId, long senderId, long targetId, List<attack_list> attackList)
	{
		this.mSkillSender = Singleton<ObjManager>.Instance.FindObjInScene(senderId);
		if (this.mSkillSender == null)
		{
			Log.DEBUG_MSG("SkillSender is null" + senderId);
			return false;
		}
		if (this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC && (this.mSkillSender as ObjNPC).MeshRoot == null)
		{
			return false;
		}
		if (this.mIsUsingSkill && this.CheckSkillCanBeBreak(skillId))
		{
			this.BreakCurSkill();
		}
		if (this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
		{
			ObjOtherPlayer objOtherPlayer = this.mSkillSender as ObjOtherPlayer;
			if (objOtherPlayer.IsDrivingMount())
			{
				objOtherPlayer.DisMountCar();
			}
		}
		this.mUsingSkillData = DataManager.GetSkillDataById(skillId);
		if (this.mUsingSkillData == null)
		{
			Log.DEBUG_MSG("mUsingSkillData == null  skillId : " + skillId);
			this.mSkillSender.OnSkillUseFail(skillId);
			return false;
		}
		this.mCurActionData = DataManager.GetActionDataByName(this.mSkillSender.GetActionName(this.mUsingSkillData.ActionName));
		if (this.mCurActionData == null)
		{
			Log.DEBUG_MSG("mCurActionData == null  ActionName : " + this.mSkillSender.GetActionName(this.mUsingSkillData.ActionName));
			this.mSkillSender.OnSkillUseFail(skillId);
			return false;
		}
		if (this.mSkillSender.IsDie)
		{
			this.mSkillSender.OnSkillUseFail(skillId);
			return false;
		}
		this.mIsUsingSkill = true;
		if (this.mSkillSender.IsMoving)
		{
			this.mSkillSender.StopMove();
		}
		this.mSkillSender.OnSkillUseSuccess(skillId);
		ObjCharacter objCharacter = null;
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(this.UsingSkillData.EffId_0);
		if (targetId != -1L && targetId != senderId && effInfoDataById != null && this.IsNeedFaceTarget(effInfoDataById))
		{
			objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(targetId);
			if (objCharacter != null)
			{
				this.SkillSender.FaceToPub(objCharacter.Position);
			}
		}
		float durationTime = -1f;
		if (this.mUsingSkillData.MoveTime != 0)
		{
			this.mSkillSender.SkillMotion.ResetSkillMove(this.mUsingSkillData, (float)this.mUsingSkillData.CastTime, objCharacter);
		}
		float num;
		if (this.mUsingSkillData.CastTime > 0)
		{
			Log.ERROR_MSG("Server Skill Can not contains CastTime!!!!!!!!!!!!!!!!!!");
			this.PlayAnimation(this.mUsingSkillData.CastAction, null, -1f);
			this.mSkillSender.PlayYinChangeEffInfo(this.mUsingSkillData.EffId_0, this.mUsingSkillData.CastTimeSecond, this.mSkillSender.Position);
			vp_Timer.In(this.mUsingSkillData.CastTimeSecond, delegate()
			{
				this.PlayAnimation(this.mUsingSkillData.ActionName, null, durationTime);
			}, this.handle);
			num = this.mUsingSkillData.CastTimeSecond;
		}
		else
		{
			this.PlayAnimation(this.mUsingSkillData.ActionName, null, durationTime);
			num = 0f;
		}
		if (objCharacter != null)
		{
			this.mSkillSender.AddPlayEffInfoData(this.mUsingSkillData.ActionName, string.Empty, num, this.mSkillSender.Position, objCharacter.transform);
		}
		else
		{
			this.mSkillSender.AddPlayEffInfoData(this.mUsingSkillData.ActionName, string.Empty, num, this.mSkillSender.Position, null);
		}
		this.ResetSenderEffInfoList(this.mUsingSkillData, num, this.mSkillSender);
		if (!string.IsNullOrEmpty(this.mUsingSkillData.EffId_0))
		{
			this.mSkillEffectList.Add(new SkillEffInfoData(this.mUsingSkillData.ID, this.mUsingSkillData.EffId_0, num + this.mUsingSkillData.EffTime_0Second, targetId, attackList, true));
		}
		if (!string.IsNullOrEmpty(this.mUsingSkillData.EffId_1))
		{
			this.mSkillEffectList.Add(new SkillEffInfoData(this.mUsingSkillData.ID, this.mUsingSkillData.EffId_1, num + this.mUsingSkillData.EffTime_1Second, targetId, attackList, true));
		}
		if (!string.IsNullOrEmpty(this.mUsingSkillData.EffId_2))
		{
			this.mSkillEffectList.Add(new SkillEffInfoData(this.mUsingSkillData.ID, this.mUsingSkillData.EffId_2, num + this.mUsingSkillData.EffTime_2Second, targetId, attackList, true));
		}
		CharacterSkillData characterSkillDataByID = this.mSkillSender.GetCharacterSkillDataByID(skillId);
		if (characterSkillDataByID != null)
		{
			characterSkillDataByID.CDTimeCount = this.mUsingSkillData.CDSecond;
		}
		this.mSkillSender.HoldTimeCount = this.mUsingSkillData.HoldTimeSecond;
		if (this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			this.CameraOpt();
		}
		return true;
	}

	// Token: 0x060037C9 RID: 14281 RVA: 0x000E605C File Offset: 0x000E425C
	public bool IsNeedFaceTarget(EffInfoData effInfoData)
	{
		return (effInfoData.AreaType == 0 || effInfoData.AreaType == 1 || effInfoData.AreaType == 3 || effInfoData.AreaType == 2 || effInfoData.AreaType == 4) && effInfoData.Target != 1;
	}

	// Token: 0x060037CA RID: 14282 RVA: 0x000E60B4 File Offset: 0x000E42B4
	public void ResetSkillLogic()
	{
		this.mIsUsingSkill = false;
		this.mSkillEffectList.Clear();
	}

	// Token: 0x060037CB RID: 14283 RVA: 0x000E60C8 File Offset: 0x000E42C8
	public bool UseSkill(string skillId, long senderId, long targetId)
	{
		if (this.mIsUsingSkill)
		{
			if (!this.CheckSkillCanBeBreak(skillId))
			{
				return false;
			}
			this.BreakCurSkill();
		}
		this.mSkillSender = Singleton<ObjManager>.Instance.FindObjInScene(senderId);
		if (this.mSkillSender == null)
		{
			Log.DEBUG_MSG("SkillSender is null" + senderId);
			return false;
		}
		this.mUsingSkillData = DataManager.GetSkillDataById(skillId);
		if (this.mUsingSkillData == null)
		{
			Log.DEBUG_MSG("mUsingSkillData == null  skillId : " + skillId);
			this.mSkillSender.OnSkillUseFail(skillId);
			return false;
		}
		this.mCurActionData = DataManager.GetActionDataByName(this.mSkillSender.GetActionName(this.mUsingSkillData.ActionName));
		if (this.mCurActionData == null)
		{
			Log.DEBUG_MSG("mCurActionData == null  ActionName : " + this.mSkillSender.IndexName + "_" + this.mUsingSkillData.ActionName);
			this.mSkillSender.OnSkillUseFail(skillId);
			return false;
		}
		if (this.mSkillSender.IsDie)
		{
			Log.DEBUG_MSG("PlayerDie");
			this.mSkillSender.OnSkillUseFail(skillId);
			return false;
		}
		this.mIsUsingSkill = true;
		if (this.mSkillSender.IsMoving)
		{
			this.mSkillSender.StopMove();
		}
		this.mSkillSender.OnSkillUseSuccess(skillId);
		ObjCharacter objCharacter = null;
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(this.UsingSkillData.EffId_0);
		if (targetId != -1L && targetId != senderId && effInfoDataById != null && this.IsNeedFaceTarget(effInfoDataById))
		{
			objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(targetId);
			if (objCharacter != null)
			{
				this.SkillSender.FaceToPub(objCharacter.Position);
			}
		}
		float durationTime = -1f;
		if (this.mUsingSkillData.MoveTime != 0)
		{
			this.mSkillSender.SkillMotion.ResetSkillMove(this.mUsingSkillData, (float)this.mUsingSkillData.CastTime, objCharacter);
		}
		float num;
		if (this.mUsingSkillData.CastTime > 0)
		{
			this.PlayAnimation(this.mUsingSkillData.CastAction, null, -1f);
			this.mSkillSender.PlayYinChangeEffInfo(this.mUsingSkillData.EffId_0, this.mUsingSkillData.CastTimeSecond, this.mSkillSender.Position);
			vp_Timer.In(this.mUsingSkillData.CastTimeSecond, delegate()
			{
				this.PlayAnimation(this.mUsingSkillData.ActionName, null, durationTime);
			}, this.handle);
			num = this.mUsingSkillData.CastTimeSecond;
		}
		else
		{
			this.PlayAnimation(this.mUsingSkillData.ActionName, null, durationTime);
			num = 0f;
		}
		if (objCharacter != null)
		{
			this.mSkillSender.AddPlayEffInfoData(this.mUsingSkillData.ActionName, string.Empty, num, this.mSkillSender.Position, objCharacter.transform);
		}
		else
		{
			this.mSkillSender.AddPlayEffInfoData(this.mUsingSkillData.ActionName, string.Empty, num, this.mSkillSender.Position, null);
		}
		this.ResetSenderEffInfoList(this.mUsingSkillData, num, this.mSkillSender);
		if (!string.IsNullOrEmpty(this.mUsingSkillData.EffId_0))
		{
			this.mSkillEffectList.Add(new SkillEffInfoData(this.mUsingSkillData.ID, this.mUsingSkillData.EffId_0, num + this.mUsingSkillData.EffTime_0Second, targetId, null, false));
		}
		if (!string.IsNullOrEmpty(this.mUsingSkillData.EffId_1))
		{
			this.mSkillEffectList.Add(new SkillEffInfoData(this.mUsingSkillData.ID, this.mUsingSkillData.EffId_1, num + this.mUsingSkillData.EffTime_1Second, targetId, null, false));
		}
		if (!string.IsNullOrEmpty(this.mUsingSkillData.EffId_2))
		{
			this.mSkillEffectList.Add(new SkillEffInfoData(this.mUsingSkillData.ID, this.mUsingSkillData.EffId_2, num + this.mUsingSkillData.EffTime_2Second, targetId, null, false));
		}
		CharacterSkillData characterSkillDataByID = this.mSkillSender.GetCharacterSkillDataByID(skillId);
		if (characterSkillDataByID != null)
		{
			characterSkillDataByID.CDTimeCount = this.mUsingSkillData.CDSecond;
		}
		this.mSkillSender.HoldTimeCount = this.mUsingSkillData.HoldTimeSecond;
		if (this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC || this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			this.CameraOpt();
		}
		return true;
	}

	// Token: 0x060037CC RID: 14284 RVA: 0x000E6538 File Offset: 0x000E4738
	private void CameraOpt()
	{
		if (this.mUsingSkillData == null)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.mUsingSkillData.CamRockID))
		{
			CameraController cameraController = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
			if (cameraController != null)
			{
				for (int i = 0; i < this.mUsingSkillData.CamRockIDList.Length; i++)
				{
					cameraController.AddCamRock(this.mUsingSkillData.CamRockIDList[i]);
				}
			}
		}
	}

	// Token: 0x060037CD RID: 14285 RVA: 0x000E65B4 File Offset: 0x000E47B4
	public void SkillFinished()
	{
		this.mIsUsingSkill = false;
		this.mSkillSender.CurUseSkillId = string.Empty;
		this.mLastSkillId = -1;
		this.mSkillSender.OnSkillFinished();
	}

	// Token: 0x060037CE RID: 14286 RVA: 0x000E65E0 File Offset: 0x000E47E0
	public void SetSkillEffect(EffInfoData effInfoData, float delayTime, ObjCharacter target, bool isBufSuccess)
	{
		if (effInfoData.HitAction != string.Empty)
		{
			if (this.CheckEffectAdd(effInfoData, target))
			{
				target.AddPlayAnimationData(effInfoData.HitAction, delayTime, effInfoData.ID);
			}
			target.AddPlayEffInfoData(effInfoData.HitAction, effInfoData.ID, delayTime, this.SkillSender.Position, null);
			target.FlashSkin();
		}
		if (effInfoData.ForceMove == 0 && effInfoData.MoveTime != 0)
		{
			target.AddPlayerEffMotionData(effInfoData.ID, delayTime, this.SkillSender.Position);
		}
		if (!string.IsNullOrEmpty(effInfoData.BuffID) && isBufSuccess)
		{
			target.AddBuffInfoData(effInfoData.BuffID, delayTime, effInfoData.BuffDurationSecond, this.mSkillSender);
		}
	}

	// Token: 0x060037CF RID: 14287 RVA: 0x000E66A8 File Offset: 0x000E48A8
	public void ResetSenderEffInfoList(SkillData skillData, float preActionDelayTime, ObjCharacter target)
	{
		if (!string.IsNullOrEmpty(skillData.EffId_0))
		{
			this.SetSenderSkillEffect(skillData, DataManager.GetEffInfoDataById(skillData.EffId_0), preActionDelayTime + skillData.EffTime_0Second, target);
		}
		if (!string.IsNullOrEmpty(skillData.EffId_1))
		{
			this.SetSenderSkillEffect(skillData, DataManager.GetEffInfoDataById(skillData.EffId_1), preActionDelayTime + skillData.EffTime_1Second, target);
		}
		if (!string.IsNullOrEmpty(skillData.EffId_2))
		{
			this.SetSenderSkillEffect(skillData, DataManager.GetEffInfoDataById(skillData.EffId_2), preActionDelayTime + skillData.EffTime_2Second, target);
		}
	}

	// Token: 0x060037D0 RID: 14288 RVA: 0x000E6738 File Offset: 0x000E4938
	public void SetSenderSkillEffect(SkillData skData, EffInfoData effInfoData, float delayTime, ObjCharacter target)
	{
		if (effInfoData.ForceMove == 1 && effInfoData.MoveTime != 0)
		{
			target.AddPlayerEffMotionData(effInfoData.ID, delayTime, target.Position);
		}
	}

	// Token: 0x060037D1 RID: 14289 RVA: 0x000E6774 File Offset: 0x000E4974
	public void UpdateSkill()
	{
		if (this.mSkillEffectList.Count != 0)
		{
			for (int i = this.mSkillEffectList.Count - 1; i >= 0; i--)
			{
				this.mSkillEffectList[i].DelayTime -= Time.deltaTime;
				if (this.mSkillEffectList[i].DelayTime <= 0f)
				{
					this.SetSkillEffectTarget(this.mSkillEffectList[i]);
					if (this.mSkillEffectList.Count > 0)
					{
						this.mSkillEffectList.RemoveAt(i);
					}
				}
			}
		}
		if (this.mIsUsingSkill && this.mSkillSender.AnimationLogic.CurActionData != this.mCurActionData && !this.mSkillSender.AnimationLogic.CurActionData.AnimName.Equals(this.mUsingSkillData.CastAction))
		{
			this.SkillFinished();
		}
	}

	// Token: 0x060037D2 RID: 14290 RVA: 0x000E686C File Offset: 0x000E4A6C
	public List<ObjCharacter> GetCandidateList(GameDefine.CAMP_TYPE camp)
	{
		return Singleton<ObjManager>.Instance.CampTargetList[(int)camp];
	}

	// Token: 0x060037D3 RID: 14291 RVA: 0x000E687C File Offset: 0x000E4A7C
	public void SetSkillEffectTarget(SkillEffInfoData skillEffInfoData)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(skillEffInfoData.skillId);
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(skillEffInfoData.EffInfoId);
		ObjCharacter target = Singleton<ObjManager>.Instance.FindObjInScene(skillEffInfoData.TargetId);
		List<ObjCharacter> candidateList = this.GetCandidateList(this.mSkillSender.AttributeData.Camp);
		this.SetEffect(skillDataById, effInfoDataById, target, candidateList, skillEffInfoData.AttackList, skillEffInfoData.IsServerAttack);
		if (this.mIsUsingSkill && effInfoDataById.AreaType == 5)
		{
			float delay = effInfoDataById.Param1Meter * 2f / (skillDataById.MoveDistanceMeter / skillDataById.MoveTimeSecond);
			vp_Timer.In(delay, delegate()
			{
				if (this.mIsUsingSkill)
				{
					this.mSkillEffectList.Add(skillEffInfoData);
				}
			}, null);
		}
	}

	// Token: 0x060037D4 RID: 14292 RVA: 0x000E695C File Offset: 0x000E4B5C
	public void SetEffect(SkillData skillData, EffInfoData effInfoData, ObjCharacter target, List<ObjCharacter> targetList, List<attack_list> attackList, bool IsServerAttack)
	{
		if (effInfoData.AreaType == 4)
		{
			int param = effInfoData.Param3;
			int num = -effInfoData.Param2 / 2;
			int num2 = effInfoData.Param2 / 2;
			float num3 = (float)effInfoData.Param1 / 100f;
			for (int i = 0; i < param; i++)
			{
				int num4 = Random.Range(num, num2);
				float num5 = Random.Range(0f, num3);
				Vector3 targetPos = this.mSkillSender.Position + Quaternion.AngleAxis((float)num4, Vector3.up) * this.mSkillSender.CacheTransform.forward * num5;
				BombSustainedRangeObj bombSustainedRangeObj = Singleton<ObjManager>.Instance.GetBombSustainedRangeObj();
				if (bombSustainedRangeObj != null)
				{
					UnityVersionUtil.SetActiveRecursive(bombSustainedRangeObj.gameObject, true);
					bombSustainedRangeObj.Reset(this.mSkillSender.Position + Vector3.up * 2f, targetPos, this.mSkillSender, targetList, effInfoData, 1f);
				}
			}
			return;
		}
		List<CharacterEffInfoData> list = new List<CharacterEffInfoData>();
		if (IsServerAttack)
		{
			if (attackList != null)
			{
				ObjManager instance = Singleton<ObjManager>.Instance;
				for (int j = 0; j < attackList.Count; j++)
				{
					ObjCharacter objCharacter = instance.FindObjInScene(attackList[j].id);
					if (objCharacter != null)
					{
						if ((int)attackList[j].value >= 0)
						{
							BuffInfoData buffInfoData = null;
							if (!string.IsNullOrEmpty(effInfoData.BuffID))
							{
								buffInfoData = DataManager.GetBuffInfoDataByID(effInfoData.BuffID);
							}
							if (buffInfoData != null)
							{
								int val = CharacterAttributeData.BuffUseState(this.mSkillSender, objCharacter.AttributeData, skillData.ID, effInfoData, (float)Random.Range(0, 100), buffInfoData.BufType);
								list.Add(new CharacterEffInfoData(objCharacter, val));
							}
							else
							{
								list.Add(new CharacterEffInfoData(objCharacter, GameDefine.BUF_USE_FAIL));
							}
						}
						else
						{
							list.Add(new CharacterEffInfoData(objCharacter, (int)attackList[j].value));
						}
					}
				}
			}
		}
		else
		{
			BuffInfoData buffInfoData2 = null;
			if (!string.IsNullOrEmpty(effInfoData.BuffID))
			{
				buffInfoData2 = DataManager.GetBuffInfoDataByID(effInfoData.BuffID);
			}
			List<ObjCharacter> effectTargetList = this.GetEffectTargetList(this.mSkillSender, skillData, effInfoData, target, targetList);
			for (int k = 0; k < effectTargetList.Count; k++)
			{
				if (buffInfoData2 != null)
				{
					int val2 = CharacterAttributeData.BuffUseState(this.mSkillSender, effectTargetList[k].AttributeData, skillData.ID, effInfoData, (float)Random.Range(0, 100), buffInfoData2.BufType);
					list.Add(new CharacterEffInfoData(effectTargetList[k], val2));
				}
				else
				{
					list.Add(new CharacterEffInfoData(effectTargetList[k], GameDefine.BUF_USE_FAIL));
				}
			}
		}
		if (effInfoData.Target != 1)
		{
			this.DamageOperation(list, effInfoData, skillData);
		}
		this.BuffOperation(list, effInfoData);
		if (!string.IsNullOrEmpty(effInfoData.BuffID))
		{
			BuffInfoData buffInfoDataByID = DataManager.GetBuffInfoDataByID(effInfoData.BuffID);
			for (int l = 0; l < list.Count; l++)
			{
				this.SetSkillEffect(effInfoData, 0f, list[l].TargetObj, list[l].EffVal == GameDefine.BUF_USE_SUCCESS);
			}
		}
		else
		{
			for (int m = 0; m < list.Count; m++)
			{
				this.SetSkillEffect(effInfoData, 0f, list[m].TargetObj, false);
			}
		}
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x000E6CFC File Offset: 0x000E4EFC
	public List<ObjCharacter> GetEffectTargetList(ObjCharacter sender, SkillData skillData, EffInfoData effInfoData, ObjCharacter target, List<ObjCharacter> targetList)
	{
		List<ObjCharacter> list = new List<ObjCharacter>();
		if (effInfoData.Target == 1)
		{
			list.Add(sender);
		}
		else if (effInfoData.AreaType == 0)
		{
			if (target != null && !target.IsDie && !target.InvincibleFlag && VectorXZ.Distance(sender.Position, target.Position) < skillData.TraceDistanceMeter + sender.ModelRadius + target.ModelRadius + 0.5f)
			{
				list.Add(target);
			}
		}
		else if (effInfoData.AreaType == 1)
		{
			for (int i = 0; i < targetList.Count; i++)
			{
				if (this.CanAttack(targetList[i], sender))
				{
					if (target == null)
					{
						Debug.Log("target == null");
					}
					else if (AreaCheckTool.CheckInCircle(targetList[i].Position, target.Position, (float)effInfoData.Param1 / 100f + targetList[i].ModelRadius))
					{
						list.Add(targetList[i]);
					}
				}
			}
		}
		else if (effInfoData.AreaType == 2)
		{
			for (int j = 0; j < targetList.Count; j++)
			{
				if (this.CanAttack(targetList[j], sender))
				{
					if (AreaCheckTool.CheckInForwardRectangle(targetList[j].Position, sender.CacheTransform, (float)effInfoData.Param1 / 100f + targetList[j].ModelRadius, (float)effInfoData.Param2 / 100f + targetList[j].ModelRadius))
					{
						list.Add(targetList[j]);
					}
				}
			}
		}
		else if (effInfoData.AreaType == 3)
		{
			for (int k = 0; k < targetList.Count; k++)
			{
				if (this.CanAttack(targetList[k], sender))
				{
					if (AreaCheckTool.CheckInSector(targetList[k].Position, sender.CacheTransform, (float)effInfoData.Param1 / 100f + sender.ModelRadius + targetList[k].ModelRadius, (float)effInfoData.Param2))
					{
						list.Add(targetList[k]);
					}
				}
			}
		}
		else if (effInfoData.AreaType == 5)
		{
			for (int l = 0; l < targetList.Count; l++)
			{
				if (this.CanAttack(targetList[l], sender))
				{
					if (sender.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || targetList[l].ObjType != GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || CampTool.ISPlayerCanAttack(sender as ObjOtherPlayer, targetList[l] as ObjOtherPlayer))
					{
						if (AreaCheckTool.CheckInCircle(targetList[l].Position, this.SkillSender.Position, (float)effInfoData.Param1 / 100f))
						{
							list.Add(targetList[l]);
						}
					}
				}
			}
		}
		if (list.Count > skillData.MaxAttackCount)
		{
			int num = list.Count - skillData.MaxAttackCount;
			for (int m = 0; m < num; m++)
			{
				list.RemoveAt(Random.Range(0, list.Count));
			}
		}
		return list;
	}

	// Token: 0x060037D6 RID: 14294 RVA: 0x000E7084 File Offset: 0x000E5284
	private bool CanAttack(ObjCharacter target, ObjCharacter sender)
	{
		return target.ServerId != sender.ServerId && !target.IsDie && !target.InvincibleFlag && (target.ObjType != GameDefine.OBJ_TYPE.OBJ_NPC || !(target as ObjNPC).IsMissionNpc()) && this.CheckSenderCanAttackOtherPlayer(sender, target);
	}

	// Token: 0x060037D7 RID: 14295 RVA: 0x000E70EC File Offset: 0x000E52EC
	private bool CheckSenderCanAttackOtherPlayer(ObjCharacter sender, ObjCharacter target)
	{
		return sender.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || target.ObjType != GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || CampTool.ISPlayerCanAttack(sender as ObjOtherPlayer, target as ObjOtherPlayer);
	}

	// Token: 0x060037D8 RID: 14296 RVA: 0x000E711C File Offset: 0x000E531C
	private bool NeedSendBuffToServer(ObjCharacter target)
	{
		if (this.mSkillSender.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
		{
			return false;
		}
		if (target.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
		{
			return !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene();
		}
		return target.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER;
	}

	// Token: 0x060037D9 RID: 14297 RVA: 0x000E7170 File Offset: 0x000E5370
	private void BuffOperation(List<CharacterEffInfoData> targetList, EffInfoData effInfoData)
	{
		if (!string.IsNullOrEmpty(effInfoData.BuffID))
		{
			this.sendServerBuffList.Clear();
			for (int i = 0; i < targetList.Count; i++)
			{
				ObjCharacter targetObj = targetList[i].TargetObj;
				if (this.NeedSendBuffToServer(targetObj) && targetList[i].EffVal == GameDefine.BUF_USE_SUCCESS)
				{
					buff buff = new buff();
					buff.id = targetObj.ServerId;
					buff.effinfoId = effInfoData.ID;
					this.sendServerBuffList.Add(buff);
				}
			}
			if (this.sendServerBuffList.Count > 0)
			{
				this.sendServerBuffRequest.buffs = this.sendServerBuffList;
				NetLogic.GetInstance().Send<Protocol.use_skill_buff>(this.sendServerBuffRequest, null);
			}
		}
	}

	// Token: 0x060037DA RID: 14298 RVA: 0x000E723C File Offset: 0x000E543C
	private bool IsPlayerAttackOthers()
	{
		return this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER;
	}

	// Token: 0x060037DB RID: 14299 RVA: 0x000E7254 File Offset: 0x000E5454
	private bool IsPlayerAttackLocalCharacter()
	{
		return (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsRankPvPScene()) && this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER;
	}

	// Token: 0x060037DC RID: 14300 RVA: 0x000E72A0 File Offset: 0x000E54A0
	private bool IsPlayerAttackServerCharacter()
	{
		return this.mSkillSender.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER;
	}

	// Token: 0x060037DD RID: 14301 RVA: 0x000E72B8 File Offset: 0x000E54B8
	private bool IsLocalChracterAttackPlayer(ObjCharacter target)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		return sceneManager.IsSingleCopyScene() && this.mSkillSender.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && (target.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || target.ObjType == GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR);
	}

	// Token: 0x060037DE RID: 14302 RVA: 0x000E7308 File Offset: 0x000E5508
	private bool IsOnlineAIAttackPlayer()
	{
		return (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsRealPvPScene() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsMultiScene()) && this.mSkillSender.ObjType != GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER;
	}

	// Token: 0x060037DF RID: 14303 RVA: 0x000E7368 File Offset: 0x000E5568
	private void DamageOperation(List<CharacterEffInfoData> list, EffInfoData effInfoData, SkillData skillData)
	{
		if (this.IsOnlineAIAttackPlayer())
		{
			return;
		}
		if (!effInfoData.IsHaveDamage())
		{
			return;
		}
		CharacterAttributeData attributeData = this.mSkillSender.AttributeData;
		int num = -1;
		if (this.IsPlayerAttackOthers())
		{
			num = 0;
			this.damagelist.Clear();
		}
		for (int i = 0; i < list.Count; i++)
		{
			ObjCharacter targetObj = list[i].TargetObj;
			CharacterAttributeData attributeData2 = targetObj.AttributeData;
			bool flag = false;
			bool flag2 = false;
			int damage = CharacterAttributeData.GetDamage(this.mSkillSender, targetObj, skillData.ID, effInfoData, out flag, out flag2);
			if (this.IsPlayerAttackOthers())
			{
				if (damage > 0)
				{
					if (flag2)
					{
						targetObj.ChangeHPEffect(targetObj.AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.PLAYER_ATTACK_CRITICAL);
					}
					else
					{
						targetObj.ChangeHPEffect(targetObj.AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.TARGET_HPDOWN_PLAYER);
					}
				}
				else
				{
					targetObj.ChangeHPEffect(targetObj.AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.PLAYER_ATTACK_MISS);
				}
			}
			else if (this.IsLocalChracterAttackPlayer(targetObj) && !targetObj.InvincibleFlag)
			{
				if (damage > 0)
				{
					if (flag2)
					{
						targetObj.ChangeHPEffect(targetObj.AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_CRITICAL);
					}
					else
					{
						targetObj.ChangeHPEffect(targetObj.AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.PLAYER_HP_DOWN);
					}
				}
				else
				{
					targetObj.ChangeHPEffect(targetObj.AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_MISS);
				}
				this.request.clear();
				this.request.characterId = targetObj.ServerId;
				this.request.damage = (long)damage;
				this.request.effinfoId = effInfoData.ID;
				NetLogic.GetInstance().Send<Protocol.local_character_attack>(this.request, null);
			}
			if (num > -1)
			{
				num++;
				acceptdamge acceptdamge = new acceptdamge();
				acceptdamge.damage = (long)damage;
				acceptdamge.id = targetObj.ServerId;
				acceptdamge.skillId = skillData.ID;
				acceptdamge.effinfoId = effInfoData.ID;
				acceptdamge.cri = flag2;
				acceptdamge.parm = ((!flag2) ? 0L : 1L);
				acceptdamge.parm2 = ((!flag) ? 0L : 1L);
				acceptdamge.parm3 = (long)((int)(Time.realtimeSinceStartup * 100f));
				acceptdamge.parm4 = PlayerCommonData.sendIndex;
				this.damagelist.Add(acceptdamge);
			}
		}
		if (num > 0)
		{
			this.request1.clear();
			this.request1.damges = this.damagelist;
			NetLogic.GetInstance().Send<Protocol.accept_damge>(this.request1, null);
		}
	}

	// Token: 0x060037E0 RID: 14304 RVA: 0x000E7600 File Offset: 0x000E5800
	public bool CheckEffectAdd(EffInfoData effInfoData, ObjCharacter objCha)
	{
		return !(objCha == null) && !objCha.IsDie && (objCha.ObjType != GameDefine.OBJ_TYPE.OBJ_NPC || !((objCha as ObjNPC).MeshRoot == null)) && objCha.ObjType != GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR && objCha.ObjType != GameDefine.OBJ_TYPE.OBJ_NPC_CAR && ((objCha.AnimationLogic.CurActionData != null && objCha.AnimationLogic.CurActionData.AnimCanBeBreak == 1) || (effInfoData.MoveDistance != 0 && effInfoData.ForceMove != 1));
	}

	// Token: 0x060037E1 RID: 14305 RVA: 0x000E76A8 File Offset: 0x000E58A8
	public bool CheckSkillCanBeBreak(string skillId)
	{
		return !this.CheckSameSkill(skillId) && this.mUsingSkillData.CanBeBreak != 0;
	}

	// Token: 0x060037E2 RID: 14306 RVA: 0x000E76CC File Offset: 0x000E58CC
	public bool CheckSameSkill(string skillId)
	{
		return skillId == this.mUsingSkillData.ID || (!string.IsNullOrEmpty(DataManager.GetSkillDataById(skillId).NextSkill) && !string.IsNullOrEmpty(this.mUsingSkillData.NextSkill));
	}

	// Token: 0x060037E3 RID: 14307 RVA: 0x000E7720 File Offset: 0x000E5920
	public void BreakCurSkill()
	{
		if (!this.mIsUsingSkill)
		{
			return;
		}
		this.mIsUsingSkill = false;
		this.handle.Cancel();
		this.mSkillEffectList.Clear();
		if (this.mSkillSender.AnimationLogic != null)
		{
			this.mSkillSender.AnimationLogic.BreakCurAnima();
		}
		if (this.mSkillSender.EffectLogic != null)
		{
			this.mSkillSender.EffectLogic.BreakEffect(this.mCurActionData.FxEffID);
			if (!string.IsNullOrEmpty(this.mUsingSkillData.CastAction))
			{
				this.mSkillSender.EffectLogic.BreakYinChangEffect(this.mUsingSkillData.EffId_0);
			}
		}
		if (this.mSkillSender.SkillMotion != null)
		{
			this.mSkillSender.SkillMotion.BreakCurSkillMotion();
		}
		if (this.mSkillSender.EffectMotion != null)
		{
			this.mSkillSender.EffectMotion.BreakCurEffectMotion();
		}
		this.SkillFinished();
	}

	// Token: 0x060037E4 RID: 14308 RVA: 0x000E7830 File Offset: 0x000E5A30
	private void PlayAnimation(string actionName, AnimationLogic.OnAnimFinished onSkillAnimFinished = null, float duration = -1f)
	{
		if (this.SkillSender != null && this.SkillSender.AnimationLogic != null)
		{
			this.SkillSender.AnimationLogic.PlayAnimation(actionName, onSkillAnimFinished, duration);
		}
	}

	// Token: 0x040024D7 RID: 9431
	private ObjCharacter mSkillSender;

	// Token: 0x040024D8 RID: 9432
	public bool mIsUsingSkill;

	// Token: 0x040024D9 RID: 9433
	private int mLastSkillId = -1;

	// Token: 0x040024DA RID: 9434
	private SkillData mUsingSkillData;

	// Token: 0x040024DB RID: 9435
	private ActionData mCurActionData;

	// Token: 0x040024DC RID: 9436
	private vp_Timer.Handle handle = new vp_Timer.Handle();

	// Token: 0x040024DD RID: 9437
	private List<SkillEffInfoData> mSkillEffectList = new List<SkillEffInfoData>();

	// Token: 0x040024DE RID: 9438
	private List<ObjCharacter> mTempEffectTargetList = new List<ObjCharacter>();

	// Token: 0x040024DF RID: 9439
	private use_skill_buff.request sendServerBuffRequest = new use_skill_buff.request();

	// Token: 0x040024E0 RID: 9440
	private List<buff> sendServerBuffList = new List<buff>();

	// Token: 0x040024E1 RID: 9441
	private accept_damge.request request1 = new accept_damge.request();

	// Token: 0x040024E2 RID: 9442
	private List<acceptdamge> damagelist = new List<acceptdamge>();

	// Token: 0x040024E3 RID: 9443
	private local_character_attack.request request = new local_character_attack.request();
}
