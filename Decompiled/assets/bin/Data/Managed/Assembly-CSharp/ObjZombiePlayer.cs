using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200082F RID: 2095
public class ObjZombiePlayer : ObjOtherPlayer
{
	// Token: 0x06003516 RID: 13590 RVA: 0x000D72AC File Offset: 0x000D54AC
	public ObjZombiePlayer()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER;
	}

	// Token: 0x17000ECB RID: 3787
	// (get) Token: 0x06003517 RID: 13591 RVA: 0x000D72C8 File Offset: 0x000D54C8
	public bool IsAutoFight
	{
		get
		{
			return this.mIsAutoFight;
		}
	}

	// Token: 0x06003518 RID: 13592 RVA: 0x000D72D0 File Offset: 0x000D54D0
	public override void Init()
	{
		base.Init();
		this.BonesTrsDict = base.gameObject.GetComponentInChildren<TransDicts>().TransDict;
	}

	// Token: 0x06003519 RID: 13593 RVA: 0x000D72FC File Offset: 0x000D54FC
	private void OnDisable()
	{
		if (this.mNavMeshAgent != null && this.mNavMeshAgent.enabled)
		{
			this.mNavMeshAgent.Stop();
			this.mNavMeshAgent.ResetPath();
			this.mNavMeshAgent.enabled = false;
		}
	}

	// Token: 0x0600351A RID: 13594 RVA: 0x000D734C File Offset: 0x000D554C
	public void ResetZombiePlayer(ObjInitPlayerData playerInitData)
	{
		base.Reset();
		base.Position = playerInitData.mPos;
		base.CacheTransform.forward = playerInitData.mDir;
		this.ServerId = playerInitData.mServerID;
		this.Profession = playerInitData.Profession;
		this.AttributeData.HP = (long)playerInitData.HP;
		this.AttributeData.Name = playerInitData.Name;
		this.AttributeData.GuildName = playerInitData.GuildName;
		this.AttributeData.CurEXP = playerInitData.EXP;
		this.AttributeData.Level = playerInitData.Level;
		this.AttributeData.InitData(playerInitData.Attribute, playerInitData.AttributeAll);
		this.AttributeData.HP = this.AttributeData.MaxHP;
		this.AttributeData.CurSpeed = playerInitData.Speed;
		this.AttributeData.WalkSpeed = playerInitData.WalkSpeed;
		this.AttributeData.CurRec = playerInitData.Rec;
		this.AttributeData.Camp = playerInitData.Camp;
		this.AttributeData.CurTitleLevel = playerInitData.TitleLevel;
		this.UpdateSkillList(playerInitData.skills);
		this.ResetCombo();
		this.InitHeadInfo();
		base.InitNavMeshAgent();
		this.mServerNotDieNumCount = 0;
	}

	// Token: 0x0600351B RID: 13595 RVA: 0x000D7494 File Offset: 0x000D5694
	public new void InitHeadInfo()
	{
		ResourcesManager.LoadHeadInfoPrefab(UIInfo.PlayerHeadInfoUI, "PlayerHeadInfoRoot", new ResourcesManager.LoadHeadInfoDelegate(this.LoadZombiePlayerHeadInfo));
	}

	// Token: 0x0600351C RID: 13596 RVA: 0x000D74B4 File Offset: 0x000D56B4
	private void LoadZombiePlayerHeadInfo(GameObject obj)
	{
		if (obj != null)
		{
			BillBoard billBoard = obj.GetComponent<BillBoard>();
			if (billBoard == null)
			{
				billBoard = obj.AddComponent<BillBoard>();
			}
			billBoard.enabled = true;
			billBoard.BindObj = base.gameObject;
			billBoard.DeltaHeight = base.CurrentCharacterModelData.ModelHeight + 0.2f;
			PlayerHeadInfoLogic component = obj.GetComponent<PlayerHeadInfoLogic>();
			this.mHeadInfoLogic = component;
			bool flag = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerCamp == this.AttributeData.Camp;
			component.Reset(false, this.AttributeData.CurTitleLevel, this.AttributeData.Name, string.Empty, this.AttributeData.Camp, true, this.AttributeData.IsChampionGuild());
		}
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x000D7578 File Offset: 0x000D5778
	public override void UpdateSkillList(Dictionary<string, skill_info> skills)
	{
		this.CharacterSkillData.Clear();
		int count = skills.Count;
		this.CharacterSkillData = new List<CharacterSkillData>(count);
		foreach (KeyValuePair<string, skill_info> keyValuePair in skills)
		{
			if (!keyValuePair.Value.disable || !keyValuePair.Value.HasDisable)
			{
				this.CharacterSkillData.Add(new CharacterSkillData(keyValuePair.Value.skillId, (int)keyValuePair.Value.skillLevel, (int)keyValuePair.Value.indexPos, (int)keyValuePair.Value.indexPos2, keyValuePair.Value.disable));
			}
		}
		this.CharacterSkillData.Sort((CharacterSkillData temp1, CharacterSkillData temp2) => temp1.Index - temp2.Index);
		List<SkillData> list = new List<SkillData>();
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			int index = this.CharacterSkillData[i].Index;
			if (index > 3 && index < 7)
			{
				SkillData skillDataById = DataManager.GetSkillDataById(this.CharacterSkillData[i].ID);
				list.Add(skillDataById);
			}
		}
		list.Sort((SkillData x, SkillData y) => y.PriorityAutoCombat - x.PriorityAutoCombat);
		for (int j = 0; j < list.Count; j++)
		{
			this.mEnableSkillIDList.Add(list[j]);
		}
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x000D7748 File Offset: 0x000D5948
	private void Update()
	{
		base.UpdateComponent();
		base.UpdateMove();
		base.UpdateSkillCD();
		base.UpdateHoldTime();
		this.UpdateComboTime();
		base.SkillLogic.UpdateSkill();
		if (this.mIsAutoFight)
		{
			this.AutoFight();
		}
	}

	// Token: 0x0600351F RID: 13599 RVA: 0x000D7790 File Offset: 0x000D5990
	protected void AutoFight()
	{
		if (base.SkillLogic.IsUsingSkill)
		{
			if (!this.mComboNextFlag && base.SkillLogic.UsingSkillData.IsComboSkill() && !base.SkillLogic.UsingSkillData.IsComboLastSkill())
			{
				this.mComboNextFlag = true;
			}
		}
		else
		{
			this.mChoosedSkillFlag = true;
			if (this.mEnableSkillIDList.Count > 0)
			{
				this.UseSkill(this.mEnableSkillIDList[0].ID, null);
			}
			else
			{
				this.UseComboSkill();
			}
		}
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x000D782C File Offset: 0x000D5A2C
	public void UseComboSkill()
	{
		if (this.CharacterSkillData.Count <= 0)
		{
			return;
		}
		if (!base.CheckHoldTime())
		{
			this.CheckComboDelay();
			return;
		}
		if (base.BeforeSkillCheck())
		{
			return;
		}
		ObjCharacter objCharacter = null;
		if (this.mSelectedTarget != null && this.mSelectedTarget.ServerId != this.ServerId)
		{
			objCharacter = this.mSelectedTarget;
		}
		if (objCharacter == null || objCharacter.IsDie)
		{
			objCharacter = this.ChooseTarget();
			this.SelectTarget(objCharacter);
		}
		if (objCharacter == null || objCharacter.IsDie)
		{
			return;
		}
		base.CurUseSkillId = this.mComboIndex;
		if (this.mComboIndex == this.CharacterSkillData[0].ID)
		{
			this.EnterCombat(objCharacter, new ObjCharacter.SkillUseSuccess(this.OnComboSuccess));
		}
		else
		{
			if (base.SkillLogic.IsUsingSkill && !base.SkillLogic.CheckSkillCanBeBreak(base.CurUseSkillId))
			{
				return;
			}
			SkillData skillDataById = DataManager.GetSkillDataById(base.CurUseSkillId);
			if (!base.CheckSkillDistance(skillDataById, objCharacter))
			{
				this.MoveTo(objCharacter.Position, skillDataById.TraceDistanceMeter + objCharacter.ModelRadius + this.ModelRadius - 0.5f, null);
				return;
			}
			if (base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, objCharacter.ServerId))
			{
				this.OnComboSuccess(base.CurUseSkillId);
			}
		}
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x000D79B4 File Offset: 0x000D5BB4
	private bool CheckDistance(ObjCharacter target)
	{
		float num = VectorXZ.Distance(base.Position, target.Position);
		float num2 = num - 1f - this.ModelRadius - target.ModelRadius;
		return num2 <= 0f;
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x000D7A04 File Offset: 0x000D5C04
	public override void EnterCombat(ObjCharacter target, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
		if (base.SkillLogic.IsUsingSkill && !base.SkillLogic.CheckSkillCanBeBreak(base.CurUseSkillId))
		{
			return;
		}
		SkillData skillDataById = DataManager.GetSkillDataById(base.CurUseSkillId);
		if (skillDataById == null)
		{
			return;
		}
		if (!(target != null))
		{
			return;
		}
		if (skillDataById.TraceDistanceMeter > 0f)
		{
			if (!base.CheckSkillDistance(skillDataById, target))
			{
				this.MoveTo(target.Position, skillDataById.TraceDistanceMeter + target.ModelRadius + this.ModelRadius - 0.5f, null);
				return;
			}
		}
		else if (!this.CheckDistance(target))
		{
			this.MoveTo(target.Position, skillDataById.TraceDistanceMeter + target.ModelRadius + this.ModelRadius - 0.5f, null);
			return;
		}
		if (!base.CheckSkillCD(skillDataById))
		{
			return;
		}
		if (onSkillUseSuccess != null && !this.mSkillUseSuccessDic.ContainsKey(base.CurUseSkillId))
		{
			this.mSkillUseSuccessDic.Add(base.CurUseSkillId, onSkillUseSuccess);
		}
		if (target != null)
		{
			base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, target.ServerId);
		}
		else
		{
			base.SkillLogic.UseSkill(base.CurUseSkillId, this.ServerId, -1L);
		}
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x000D7B60 File Offset: 0x000D5D60
	private void UseSkill(string skillId, ObjCharacter.SkillUseSuccess onSkillUseSuccess = null)
	{
		if (base.BeforeSkillCheck())
		{
			return;
		}
		ObjCharacter objCharacter = null;
		if (this.mSelectedTarget != null && this.mSelectedTarget.ServerId != this.ServerId)
		{
			objCharacter = this.mSelectedTarget;
		}
		if (objCharacter == null || objCharacter.IsDie)
		{
			objCharacter = this.ChooseTarget();
			this.SelectTarget(objCharacter);
		}
		if (objCharacter == null || objCharacter.IsDie)
		{
			return;
		}
		base.CurUseSkillId = skillId;
		this.EnterCombat(objCharacter, onSkillUseSuccess);
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x000D7BF8 File Offset: 0x000D5DF8
	private void OnComboSuccess(string skillId)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(skillId);
		this.mComboIndex = skillDataById.NextSkill;
		this.mComboTimeCount = skillDataById.ComboValidTimeSecond;
		this.mComboTimeTotalCount = this.mComboTimeCount;
		this.mChoosedSkillFlag = false;
		this.OnSkillDisable(skillId);
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x000D7C40 File Offset: 0x000D5E40
	private ObjCharacter ChooseTarget()
	{
		return Singleton<ObjManager>.Instance.MainPlayer;
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x000D7C4C File Offset: 0x000D5E4C
	private void SelectTarget(ObjCharacter target)
	{
		base.SelectedTarget = target;
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x000D7C58 File Offset: 0x000D5E58
	public void CheckComboDelay()
	{
		if (base.SkillLogic.UsingSkillData != null && base.SkillLogic.UsingSkillData.NextSkill == this.mComboIndex)
		{
			this.mComboNextFlag = true;
		}
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x000D7C9C File Offset: 0x000D5E9C
	private void ResetCombo()
	{
		this.mComboTimeCount = 0f;
		if (this.CharacterSkillData.Count != 0)
		{
			this.mComboIndex = this.CharacterSkillData[0].ID;
		}
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x000D7CDC File Offset: 0x000D5EDC
	public override void OnSkillFinished()
	{
		base.OnSkillFinished();
		if (this.mComboNextFlag)
		{
			this.UseComboSkill();
			this.mComboNextFlag = false;
		}
		this.mChoosedSkillFlag = false;
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x000D7D04 File Offset: 0x000D5F04
	public void TargetArriveUseSkill()
	{
		SkillData skillDataById = DataManager.GetSkillDataById(this.mCurUseSkillId);
		if (skillDataById != null)
		{
			if (!string.IsNullOrEmpty(skillDataById.NextSkill))
			{
				this.UseComboSkill();
			}
			else
			{
				this.UseSkill(this.mCurUseSkillId, null);
			}
		}
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x000D7D4C File Offset: 0x000D5F4C
	public void ActiveAutoFight()
	{
		this.mIsAutoFight = true;
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x000D7D58 File Offset: 0x000D5F58
	public void DeactiveAutoFight()
	{
		this.mIsAutoFight = false;
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x000D7D64 File Offset: 0x000D5F64
	public void UpdateComboTime()
	{
		if (this.mComboTimeCount > 0f)
		{
			this.mComboTimeCount -= Time.deltaTime;
			if (this.mComboTimeCount <= 0f)
			{
				this.ResetCombo();
			}
		}
	}

	// Token: 0x0600352E RID: 13614 RVA: 0x000D7DAC File Offset: 0x000D5FAC
	public override void OnSkillUseSuccess(string skillId)
	{
		base.OnSkillUseSuccess(skillId);
		this.OnSkillDisable(base.CurUseSkillId);
	}

	// Token: 0x0600352F RID: 13615 RVA: 0x000D7DC4 File Offset: 0x000D5FC4
	public override void OnSkillEnable(string skillID)
	{
		if (!DataManager.GetSkillDataById(skillID).IsComboSkill())
		{
			SkillData skillDataById = DataManager.GetSkillDataById(skillID);
			if (this.mEnableSkillIDList.Count > 0)
			{
				for (int i = 0; i < this.mEnableSkillIDList.Count; i++)
				{
					if (skillDataById.PriorityAutoCombat > this.mEnableSkillIDList[i].PriorityAutoCombat)
					{
						this.mEnableSkillIDList.Insert(i, skillDataById);
						return;
					}
				}
			}
			this.mEnableSkillIDList.Add(skillDataById);
		}
	}

	// Token: 0x06003530 RID: 13616 RVA: 0x000D7E4C File Offset: 0x000D604C
	public override void OnSkillDisable(string skillID)
	{
		this.mEnableSkillIDList.Remove(DataManager.GetSkillDataById(skillID));
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x000D7E60 File Offset: 0x000D6060
	public override void OnDie()
	{
		base.OnDie();
		SingletonUnity<MyEvent>.Instance.Fire("OnZombiePlayerDie", new object[]
		{
			this
		});
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x000D7E8C File Offset: 0x000D608C
	public override void ChangeHPVal(long newHP)
	{
		if (!base.IsDie)
		{
			this.AttributeData.HP = newHP;
			this.UpdateHeadInfo();
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
		}
		else if (newHP > 0L)
		{
			Debug.Log("new hp:" + newHP);
			this.mServerNotDieNumCount++;
			if (this.mServerNotDieNumCount > GameDefine.NPC_SERVER_WAIT_RELIFE_NUM)
			{
				this.OnRelife(newHP, base.Position);
				this.mServerNotDieNumCount = 0;
			}
		}
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x000D7F24 File Offset: 0x000D6124
	public override void ChangeHPEffect(long newHP, GameDefine.DAMAGEBOARD_TYPE type)
	{
		if (!base.IsDie)
		{
			long num = this.AttributeData.HP - newHP;
			if (num > 0L)
			{
				base.UpdateDamgeBoard(type, num);
				this.OnBeaton();
			}
			else
			{
				base.UpdateDamgeBoard(type, num);
			}
			this.mReceiveBiggerHpTimeCount = Time.time;
		}
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x000D7F78 File Offset: 0x000D6178
	public bool CheckInDialogRange()
	{
		float num = (float)((!Singleton<ObjManager>.Instance.MainPlayer.IsLocalDrivingCar) ? 5 : 10);
		return VectorXZ.Distance(base.Position, Singleton<ObjManager>.Instance.MainPlayer.Position) < num;
	}

	// Token: 0x040022AE RID: 8878
	private List<SkillData> mEnableSkillIDList = new List<SkillData>();

	// Token: 0x040022AF RID: 8879
	protected bool mIsAutoFight;

	// Token: 0x040022B0 RID: 8880
	private bool mChoosedSkillFlag;

	// Token: 0x040022B1 RID: 8881
	private int mServerNotDieNumCount;
}
