using System;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class PatrolState : FSMState<AILogic>
{
	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06000754 RID: 1876 RVA: 0x00032E9C File Offset: 0x0003109C
	public ObjMainPlayer MainPlayer
	{
		get
		{
			if (this.mMainPlayer == null)
			{
				this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			}
			return this.mMainPlayer;
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00032EC8 File Offset: 0x000310C8
	public override void Enter(AILogic owner, int previous)
	{
		this.FindNextTarget(owner);
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00032ED4 File Offset: 0x000310D4
	public override void Exit(AILogic owner, int previous)
	{
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00032ED8 File Offset: 0x000310D8
	public override void Update(AILogic owner)
	{
		if (!owner.Ownner.IsDie)
		{
			if (owner.DelayMoveTimeCount > 0f && Time.time - owner.DelayMoveTimeCount > 2f)
			{
				owner.DelayMoveTimeCount = -1f;
				this.FindNextTarget(owner);
			}
			this.CheckState(owner);
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00032F34 File Offset: 0x00031134
	public void FindNextTarget(AILogic owner)
	{
		if (owner.IsHavePath())
		{
			this.mTarget = owner.PathList[owner.CurPathIndex].Position;
			owner.CurPathIndex = (owner.CurPathIndex + 1) % owner.PathList.Count;
			owner.Ownner.WalkMoveTo(this.mTarget, 1f, new ObjCharacter.TargetArriveFinsh(owner.OnPatrolStateArriveTarget));
		}
		else if (owner.AiData.PATROL_Type == PATROL_TYPE.CIRCLE)
		{
			float num = Random.Range(owner.AiData.PatrolDistanceMeter * -1f, owner.AiData.PatrolDistanceMeter);
			float num2 = Random.Range(owner.AiData.PatrolDistanceMeter * -1f, owner.AiData.PatrolDistanceMeter);
			Vector3 vector = owner.Ownner.BornPos + new Vector3(num, 0f, num2);
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(vector, ref navMeshHit, 0.1f, 15))
			{
				this.mTarget = vector;
			}
			else
			{
				NavMeshHit navMeshHit2 = default(NavMeshHit);
				NavMesh.Raycast(owner.Ownner.BornPos, vector, ref navMeshHit2, owner.Ownner.NavMeshAgent.walkableMask);
				vector = navMeshHit2.position;
				this.mTarget = vector;
			}
			owner.Ownner.WalkMoveTo(this.mTarget, 1f, new ObjCharacter.TargetArriveFinsh(owner.OnPatrolStateArriveTarget));
		}
		else if (owner.AiData.PATROL_Type == PATROL_TYPE.STATIC)
		{
			return;
		}
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x000330B4 File Offset: 0x000312B4
	public void CheckState(AILogic owner)
	{
		if (owner.AiData.LockType == 1)
		{
			if (this.MainPlayer != null && !this.MainPlayer.IsDie && AreaCheckTool.CheckInCircle(this.MainPlayer.Position, owner.Ownner.Position, owner.AiData.LockDistanceMeter))
			{
				owner.ChangeState((int)owner.AttackState);
			}
		}
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00033130 File Offset: 0x00031330
	public override void Notify(int messageID, params object[] messageParams)
	{
		if (messageID == 0)
		{
			AILogic ailogic = (AILogic)messageParams[0];
			ailogic.ChangeState((int)ailogic.AttackState);
		}
	}

	// Token: 0x04000641 RID: 1601
	private Vector3 mTarget;

	// Token: 0x04000642 RID: 1602
	private ObjMainPlayer mMainPlayer;
}
