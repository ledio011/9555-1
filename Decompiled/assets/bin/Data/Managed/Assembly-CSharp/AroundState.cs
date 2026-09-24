using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class AroundState : FSMState<AILogic>
{
	// Token: 0x0600072A RID: 1834 RVA: 0x00032608 File Offset: 0x00030808
	public override void Enter(AILogic owner, int previous)
	{
		owner.EnterAroundStateTime = Time.time;
		owner.AroundTime = owner.AiData.AroundTimeSecond;
		if (owner.Ownner.SelectedTarget != null)
		{
			if (!owner.ReturnBackFlag)
			{
				this.FindNextPoint(owner);
			}
			else
			{
				owner.Ownner.StopMove();
				owner.Ownner.WalkMoveTo(owner.Ownner.BornPos, 1f, null);
			}
		}
		else
		{
			owner.Ownner.StopMove();
			owner.Ownner.WalkMoveTo(owner.Ownner.BornPos, 1f, null);
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x000326B4 File Offset: 0x000308B4
	private void FindNextPoint(AILogic owner)
	{
		if (owner.Ownner.SelectedTarget == null)
		{
			return;
		}
		float aroundDistanceMeter = owner.AiData.AroundDistanceMeter;
		float num = (float)owner.AiData.AroundAngle;
		Quaternion quaternion = Quaternion.Euler(0f, num, 0f);
		Vector3 vector = quaternion * owner.Ownner.SelectedTarget.CacheTransform.forward * aroundDistanceMeter + owner.Ownner.SelectedTarget.Position;
		NavMeshHit navMeshHit;
		if (!NavMesh.SamplePosition(vector, ref navMeshHit, 0.1f, 15))
		{
			NavMeshHit navMeshHit2 = default(NavMeshHit);
			NavMesh.Raycast(owner.Ownner.SelectedTarget.Position, vector, ref navMeshHit2, owner.Ownner.NavMeshAgent.walkableMask);
			vector = navMeshHit2.position;
		}
		owner.Ownner.MoveTo(vector, 1f, null);
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0003279C File Offset: 0x0003099C
	public override void Exit(AILogic owner, int previous)
	{
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x000327A0 File Offset: 0x000309A0
	public override void Update(AILogic owner)
	{
		if (!owner.OutOffRangeFlag)
		{
			if (VectorXZ.Distance(owner.Ownner.Position, owner.Ownner.BornPos) > owner.AiData.ReturnDistanceMeter)
			{
				owner.OutOffRangeFlag = true;
				owner.StartChaseTime = Time.time;
			}
			else if (Time.time - owner.EnterAroundStateTime < owner.AroundTime)
			{
				if (!owner.Ownner.IsMoving)
				{
					this.FindNextPoint(owner);
				}
			}
			else
			{
				owner.ChangeState(1);
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
				owner.ChangeState(1);
			}
		}
		else if (VectorXZ.Distance(owner.Ownner.Position, owner.Ownner.BornPos) < owner.Ownner.NavMeshAgent.stoppingDistance + 0.5f)
		{
			owner.ChangeState(0);
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x000328FC File Offset: 0x00030AFC
	public override void Notify(int messageID, params object[] messageParams)
	{
		if (messageID == 0)
		{
			AILogic ailogic = (AILogic)messageParams[0];
			ailogic.ReturnBackFlag = false;
			ailogic.StartChaseTime = Time.time;
		}
	}
}
