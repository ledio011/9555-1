using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class AttackState : FSMState<AILogic>
{
	// Token: 0x06000730 RID: 1840 RVA: 0x00032934 File Offset: 0x00030B34
	public override void Enter(AILogic owner, int previous)
	{
		owner.OutOffRangeFlag = false;
		owner.ReturnBackFlag = false;
		owner.CurUseSkillID = this.GetTargetSkill(owner.Ownner);
		owner.ActionTime = owner.AiData.ActionTimeSecond;
		owner.EnterAttackStateTime = Time.time;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00032980 File Offset: 0x00030B80
	public override void Exit(AILogic owner, int previous)
	{
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00032984 File Offset: 0x00030B84
	public override void Update(AILogic owner)
	{
		if (!owner.Ownner.IsDie)
		{
			this.CheckUseSkill(owner);
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000329A0 File Offset: 0x00030BA0
	public void CheckUseSkill(AILogic owner)
	{
		if (owner.Ownner.SkillLogic.IsUsingSkill)
		{
			return;
		}
		if (Time.time - owner.EnterAttackStateTime > owner.ActionTime)
		{
			owner.ChangeState(3);
			return;
		}
		if (owner.AiData.ReturnDistance <= 0)
		{
			string targetSkill = this.GetTargetSkill(owner.Ownner);
			if (string.IsNullOrEmpty(targetSkill))
			{
				return;
			}
			SkillData skillDataById = DataManager.GetSkillDataById(targetSkill);
			ObjCharacter objCharacter = owner.Ownner.ChooseTarget(owner.AiData.SqrtLockDistanceMeter);
			if (objCharacter == null)
			{
				return;
			}
			owner.Ownner.UseSkill(targetSkill, null);
		}
		else if (!owner.OutOffRangeFlag)
		{
			if (VectorXZ.Distance(owner.Ownner.Position, owner.Ownner.BornPos) > owner.AiData.ReturnDistanceMeter)
			{
				owner.OutOffRangeFlag = true;
				owner.StartChaseTime = Time.time;
			}
			else
			{
				this.UseSkill(owner);
			}
		}
		else if (!owner.ReturnBackFlag)
		{
			if (Time.time - owner.StartChaseTime >= owner.ChaseTime)
			{
				if (!owner.Ownner.BeforeMoveCheck())
				{
					owner.Ownner.StopMove();
					owner.Ownner.WalkMoveTo(owner.Ownner.BornPos, 1f, null);
					owner.ReturnBackFlag = true;
				}
			}
			else
			{
				this.UseSkill(owner);
			}
		}
		else if (VectorXZ.Distance(owner.Ownner.Position, owner.Ownner.BornPos) < owner.Ownner.NavMeshAgent.stoppingDistance + 0.5f)
		{
			owner.ChangeState(0);
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00032B64 File Offset: 0x00030D64
	private void UseSkill(AILogic owner)
	{
		if (owner.Ownner.SkillLogic.IsUsingSkill)
		{
			return;
		}
		if (string.IsNullOrEmpty(owner.CurUseSkillID))
		{
			owner.CurUseSkillID = this.GetTargetSkill(owner.Ownner);
			if (string.IsNullOrEmpty(owner.CurUseSkillID))
			{
				return;
			}
		}
		owner.Ownner.UseSkill(owner.CurUseSkillID, null);
		if (owner.Ownner.SelectedTarget == null)
		{
			owner.OutOffRangeFlag = true;
			if (!owner.Ownner.BeforeMoveCheck())
			{
				owner.Ownner.AILogic.ReturnBackFlag = true;
				owner.Ownner.StopMove();
				owner.Ownner.WalkMoveTo(owner.Ownner.BornPos, 1f, null);
				return;
			}
			owner.StartChaseTime = Time.time - owner.ChaseTime;
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00032C44 File Offset: 0x00030E44
	private string GetTargetSkill(ObjNPC owner)
	{
		if (owner.EnableSkillIDList.Count == 0)
		{
			return string.Empty;
		}
		return owner.EnableSkillIDList[0].ID;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00032C78 File Offset: 0x00030E78
	public override void Notify(int messageID, params object[] messageParams)
	{
		if (messageID == 0)
		{
			AILogic ailogic = (AILogic)messageParams[0];
			ailogic.ReturnBackFlag = false;
			ailogic.StartChaseTime = Time.time;
		}
		if (messageID == 1)
		{
			AILogic ailogic2 = (AILogic)messageParams[0];
			ailogic2.ReturnBackFlag = false;
			ailogic2.StartChaseTime = Time.time;
			ailogic2.ChangeState(3);
		}
	}
}
