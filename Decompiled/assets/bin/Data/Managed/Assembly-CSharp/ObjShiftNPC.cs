using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082E RID: 2094
public class ObjShiftNPC : ObjNPC
{
	// Token: 0x17000EC5 RID: 3781
	// (get) Token: 0x06003506 RID: 13574 RVA: 0x000D6B78 File Offset: 0x000D4D78
	// (set) Token: 0x06003507 RID: 13575 RVA: 0x000D6B80 File Offset: 0x000D4D80
	public int[] ShiftingHP
	{
		get
		{
			return this.mShiftingHP;
		}
		set
		{
			this.mShiftingHP = value;
		}
	}

	// Token: 0x17000EC6 RID: 3782
	// (get) Token: 0x06003508 RID: 13576 RVA: 0x000D6B8C File Offset: 0x000D4D8C
	// (set) Token: 0x06003509 RID: 13577 RVA: 0x000D6B94 File Offset: 0x000D4D94
	public int ShiftingState
	{
		get
		{
			return this.mShiftingState;
		}
		set
		{
			this.mShiftingState = value;
		}
	}

	// Token: 0x17000EC7 RID: 3783
	// (get) Token: 0x0600350A RID: 13578 RVA: 0x000D6BA0 File Offset: 0x000D4DA0
	// (set) Token: 0x0600350B RID: 13579 RVA: 0x000D6BA8 File Offset: 0x000D4DA8
	public int ShiftStateCount
	{
		get
		{
			return this.mShiftStateCount;
		}
		set
		{
			this.mShiftStateCount = value;
		}
	}

	// Token: 0x17000EC8 RID: 3784
	// (get) Token: 0x0600350C RID: 13580 RVA: 0x000D6BB4 File Offset: 0x000D4DB4
	public string[] ShiftSkill
	{
		get
		{
			return this.mShiftSkill;
		}
	}

	// Token: 0x17000EC9 RID: 3785
	// (get) Token: 0x0600350D RID: 13581 RVA: 0x000D6BBC File Offset: 0x000D4DBC
	public List<string[]> ShiftSkillGroup
	{
		get
		{
			return this.mShiftSkillGroup;
		}
	}

	// Token: 0x17000ECA RID: 3786
	// (get) Token: 0x0600350E RID: 13582 RVA: 0x000D6BC4 File Offset: 0x000D4DC4
	public bool IsShifting
	{
		get
		{
			return this.mIsShifting;
		}
	}

	// Token: 0x0600350F RID: 13583 RVA: 0x000D6BCC File Offset: 0x000D4DCC
	public new virtual void ResetNpc(ObjInitNpcData initData)
	{
		base.Reset();
		this.ServerId = initData.mServerID;
		base.Position = initData.mPos;
		this.mTransform.forward = initData.mDir;
		this.mNpcData = initData.npcInfoData;
		base.BornPos = initData.mPos;
		this.AttributeData.Camp = (GameDefine.CAMP_TYPE)initData.npcInfoData.Group;
		this.mNPCFunctionType = (GameDefine.NPC_FUNCTION_TYPE)initData.npcInfoData.FunctionType;
		this.mNPCType = (GameDefine.NPC_TYPE)initData.npcInfoData.Type;
		this.AttributeData.HP = initData.HP;
		this.AttributeData.MaxHP = initData.MaxHP;
		this.AttributeData.CurATK = (float)initData.npcInfoData.Atk;
		this.AttributeData.CurDEF = (float)initData.npcInfoData.Def;
		this.AttributeData.Name = initData.npcInfoData.Name;
		this.AttributeData.CurEXD = (float)this.mNpcData.EXD / 10000f;
		this.AttributeData.CurEXR = (float)this.mNpcData.EXR / 10000f;
		this.AttributeData.CurHIT = (float)this.mNpcData.HIT;
		this.AttributeData.CurDGE = (float)this.mNpcData.DGE;
		this.AttributeData.CurCRI = (float)this.mNpcData.CRI;
		this.AttributeData.CurRES = (float)this.mNpcData.RES;
		this.AttributeData.CurCRD = (float)this.mNpcData.CRD / 10000f;
		this.AttributeData.CurCRR = (float)this.mNpcData.CRR / 10000f;
		this.AttributeData.CurDEFA = this.mNpcData.DEFA;
		this.AttributeData.CurSpeed = initData.npcInfoData.MoveSpeedMeter;
		this.AttributeData.WalkSpeed = initData.npcInfoData.WalkSpeedMeter;
		this.mPatrolRange = initData.npcInfoData.PatrolRadius;
		this.mSearchRange = initData.npcInfoData.SearchRadius;
		this.mPathID = initData.PathID;
		this.mDefaultDialogID = initData.npcInfoData.TalkGroup;
		base.InitSkill(this.mNpcData.SkillList);
		base.InitNavMeshAgent();
		base.InitNPCHeadInfo();
		base.AddDialogMission();
		if (this.mTransform.childCount > 0)
		{
			Transform child = this.mTransform.GetChild(0);
			child.localScale = Vector3.one * initData.npcInfoData.ModelScale;
		}
		CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
		if (component != null)
		{
			component.height = base.CurrentCharacterModelData.ModelHeight;
			component.center = Vector3.up * base.CurrentCharacterModelData.ModelHeight / 2f;
			component.radius = base.CurrentCharacterModelData.ModelRadius;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene())
		{
			if (!base.IsMissionNpc())
			{
				if (this.mAILogic == null)
				{
					this.mAILogic = base.gameObject.AddComponent<AILogic>();
				}
				if (this.mAILogic != null)
				{
					this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, initData.PathID);
				}
			}
		}
		else if (this.mNpcData.AI.Equals("FollowAI"))
		{
			if (this.mAILogic == null)
			{
				this.mAILogic = base.gameObject.AddComponent<AILogic>();
			}
			if (this.mAILogic != null)
			{
				this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, initData.PathID);
			}
		}
		else
		{
			if (this.mAILogic != null)
			{
				Object.Destroy(this.mAILogic);
				this.mAILogic = null;
			}
			if (base.IsMissionNpc())
			{
				this.mNavMeshAgent.enabled = false;
			}
			else
			{
				this.mNavMeshAgent.enabled = true;
			}
		}
		this.mShiftingState = 0;
		this.mShiftingHP = this.mNpcData.ShiftHP;
		this.mShiftSkill = this.mNpcData.ShiftSkill;
		this.mShiftSkillGroup = this.mNpcData.ShiftSkillGroup;
		this.mShiftStateCount = this.mNpcData.ShiftStateCount;
		this.mIsShifting = false;
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x000D7060 File Offset: 0x000D5260
	public override void ChangeHPVal(long newHP)
	{
		if (!base.IsDie)
		{
			this.AttributeData.HP = newHP;
			if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.ChangeHP(newHP, this);
			}
			else
			{
				this.UpdateHeadInfo();
			}
			if (this.AttributeData.HP <= 0L)
			{
				if (this.mShiftingState < this.mShiftStateCount)
				{
					this.Shifting();
				}
				else
				{
					this.OnDie();
				}
			}
		}
	}

	// Token: 0x06003511 RID: 13585 RVA: 0x000D70DC File Offset: 0x000D52DC
	public override void ChangeHPEffect(long newHP, GameDefine.DAMAGEBOARD_TYPE type)
	{
		if (!base.IsDie && !this.mIsShifting)
		{
			long num = this.AttributeData.HP - newHP;
			if (num > 0L)
			{
				base.UpdateDamgeBoard(type, num);
			}
			else
			{
				base.UpdateDamgeBoard(type, num);
			}
			this.AttributeData.HP = newHP;
			if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.ChangeHP(newHP, this);
			}
			else
			{
				this.UpdateHeadInfo();
			}
		}
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x000D715C File Offset: 0x000D535C
	public void Shifting()
	{
		this.mIsShifting = true;
		this.mAILogic.enabled = false;
		base.DisableNavMeshAgent();
		if (this.mSkillLogic.IsUsingSkill)
		{
			this.mSkillLogic.BreakCurSkill();
		}
		if (base.IsMoving)
		{
			this.StopMove();
		}
		vp_Timer.In(3f, delegate()
		{
			this.AttributeData.HP = (long)this.mShiftingHP[this.mShiftingState];
			if (this.mNPCType == GameDefine.NPC_TYPE.BOSS)
			{
				SingletonUnity<BossXueTiaoLogicNew>.Instance.ChangeHP(this.AttributeData.HP, this);
			}
			else
			{
				this.UpdateHeadInfo();
			}
			base.InitSkill(this.mShiftSkillGroup[this.mShiftingState]);
			base.SkillLogic.UseSkill(this.mShiftSkill[this.mShiftingState], this.ServerId, -1L);
			base.RegisterOnSkillFinished(delegate
			{
				vp_Timer.In(3f, delegate()
				{
					this.mIsShifting = false;
					this.mAILogic.enabled = true;
					base.EnableNavMeshAgent();
				}, null);
			});
			this.mShiftingState++;
		}, null);
	}

	// Token: 0x040022A8 RID: 8872
	private int[] mShiftingHP;

	// Token: 0x040022A9 RID: 8873
	private int mShiftingState;

	// Token: 0x040022AA RID: 8874
	private int mShiftStateCount;

	// Token: 0x040022AB RID: 8875
	private string[] mShiftSkill;

	// Token: 0x040022AC RID: 8876
	private List<string[]> mShiftSkillGroup;

	// Token: 0x040022AD RID: 8877
	private bool mIsShifting;
}
