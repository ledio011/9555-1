using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000833 RID: 2099
public class SustainedRangeObj : MonoBehaviour
{
	// Token: 0x0600356B RID: 13675 RVA: 0x000D96F8 File Offset: 0x000D78F8
	public void ResetSustainedRange(ObjCharacter skillSender, List<ObjCharacter> targetList, EffInfoData effInfoData, float damageInterval = 1f)
	{
		this.mTargetList = targetList;
		this.mEffInfoData = effInfoData;
		this.mDamageInterval = damageInterval;
		this.mDamageRange = effInfoData.Param4Meter;
		this.mDamageVal = effInfoData.Damagex;
		this.mDamageMulti = effInfoData.DamageMulti_100f;
		this.mDamageDuration = effInfoData.Param5Meter;
		this.mSkillSender = skillSender;
		this.mEnableFlag = false;
		UnityVersionUtil.SetActiveRecursive(this.DurationEffect.gameObject, false);
	}

	// Token: 0x0600356C RID: 13676 RVA: 0x000D976C File Offset: 0x000D796C
	public void StartDamage()
	{
		if (!this.mEnableFlag)
		{
			this.mEnableFlag = true;
			this.mLastDamageTime = Time.time;
			this.mStartTime = Time.time;
			UnityVersionUtil.SetActiveRecursive(this.DurationEffect.gameObject, true);
		}
	}

	// Token: 0x0600356D RID: 13677 RVA: 0x000D97A8 File Offset: 0x000D79A8
	public void UpdateDamage()
	{
		if (this.mEnableFlag)
		{
			if (Time.time - this.mLastDamageTime >= this.mDamageInterval)
			{
				this.mLastDamageTime = Time.time;
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < this.mTargetList.Count; i++)
				{
					if (Vector3.SqrMagnitude(this.mTargetList[i].Position - base.transform.position) <= this.mDamageRange * this.mDamageRange && this.mTargetList[i].ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && !this.mTargetList[i].IsDie && !this.mTargetList[i].InvincibleFlag)
					{
						int damage = CharacterAttributeData.GetDamage(this.mSkillSender, this.mTargetList[i], this.mEffInfoData, (float)this.mDamageVal, this.mDamageMulti, out flag, out flag2);
						this.request.clear();
						this.request.characterId = this.mTargetList[i].ServerId;
						this.request.damage = (long)damage;
						NetLogic.GetInstance().Send<Protocol.local_character_attack>(this.request, null);
						if (damage > 0)
						{
							if (flag2)
							{
								this.mTargetList[i].ChangeHPEffect(this.mTargetList[i].AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_CRITICAL);
							}
							else
							{
								this.mTargetList[i].ChangeHPEffect(this.mTargetList[i].AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.PLAYER_HP_DOWN);
							}
						}
						else
						{
							this.mTargetList[i].ChangeHPEffect(this.mTargetList[i].AttributeData.HP - (long)damage, GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_MISS);
						}
						this.SetSkillEffect(this.mEffInfoData, 0f, this.mTargetList[i]);
					}
				}
			}
			if (Time.time - this.mStartTime >= this.mDamageDuration)
			{
				this.OnRecycle();
			}
		}
	}

	// Token: 0x0600356E RID: 13678 RVA: 0x000D99C8 File Offset: 0x000D7BC8
	public void SetSkillEffect(EffInfoData effInfoData, float delayTime, ObjCharacter target)
	{
		if (this.CheckEffectAdd(effInfoData, target))
		{
			if (effInfoData.HitAction != string.Empty)
			{
				target.AddPlayAnimationData(effInfoData.HitAction, delayTime, effInfoData.ID);
			}
			target.AddPlayEffInfoData(effInfoData.HitAction, effInfoData.ID, delayTime, base.transform.position, null);
		}
		if (effInfoData.ForceMove == 0 && effInfoData.MoveTime != 0)
		{
			target.AddPlayerEffMotionData(effInfoData.ID, delayTime, base.transform.position);
		}
		if (!string.IsNullOrEmpty(effInfoData.BuffID))
		{
			target.AddBuffInfoData(effInfoData.BuffID, delayTime, effInfoData.BuffDurationSecond, null);
		}
	}

	// Token: 0x0600356F RID: 13679 RVA: 0x000D9A7C File Offset: 0x000D7C7C
	public bool CheckEffectAdd(EffInfoData effInfoData, ObjCharacter objCha)
	{
		return !(objCha == null) && !objCha.IsDie && (objCha.AnimationLogic.CurActionData.AnimCanBeBreak == 1 || (effInfoData.MoveDistance != 0 && effInfoData.ForceMove != 1));
	}

	// Token: 0x06003570 RID: 13680 RVA: 0x000D9AD4 File Offset: 0x000D7CD4
	public virtual void OnRecycle()
	{
		Debug.Log("BaseRecycle");
	}

	// Token: 0x040022DD RID: 8925
	public ParticleSystem DurationEffect;

	// Token: 0x040022DE RID: 8926
	private List<ObjCharacter> mTargetList;

	// Token: 0x040022DF RID: 8927
	private EffInfoData mEffInfoData;

	// Token: 0x040022E0 RID: 8928
	private float mDamageInterval;

	// Token: 0x040022E1 RID: 8929
	private float mDamageRange;

	// Token: 0x040022E2 RID: 8930
	private int mDamageVal;

	// Token: 0x040022E3 RID: 8931
	private float mDamageMulti;

	// Token: 0x040022E4 RID: 8932
	private float mDamageDuration;

	// Token: 0x040022E5 RID: 8933
	private ObjCharacter mSkillSender;

	// Token: 0x040022E6 RID: 8934
	private bool mEnableFlag;

	// Token: 0x040022E7 RID: 8935
	private float mLastDamageTime;

	// Token: 0x040022E8 RID: 8936
	private float mStartTime;

	// Token: 0x040022E9 RID: 8937
	private local_character_attack.request request = new local_character_attack.request();
}
