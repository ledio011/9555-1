using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class DecisionState : FSMState<AILogic>
{
	// Token: 0x06000749 RID: 1865 RVA: 0x00032DD0 File Offset: 0x00030FD0
	public override void Enter(AILogic owner, int previous)
	{
		if (previous == (int)owner.AttackState)
		{
			if (this.IsNeedAround(owner))
			{
				this.EnterAroundState(owner);
			}
			else
			{
				this.EnterAttackState(owner);
			}
		}
		else
		{
			this.EnterAttackState(owner);
		}
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00032E14 File Offset: 0x00031014
	private void EnterAroundState(AILogic owner)
	{
		owner.ChangeState(2);
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00032E20 File Offset: 0x00031020
	private void EnterAttackState(AILogic owner)
	{
		owner.ChangeState(1);
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00032E2C File Offset: 0x0003102C
	private bool IsNeedAround(AILogic owner)
	{
		return Random.Range(0, 100) <= owner.AiData.AroundProb;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00032E4C File Offset: 0x0003104C
	public override void Exit(AILogic owner, int previous)
	{
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00032E50 File Offset: 0x00031050
	public override void Update(AILogic owner)
	{
	}
}
