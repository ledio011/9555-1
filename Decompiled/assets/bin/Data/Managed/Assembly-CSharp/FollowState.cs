using System;

// Token: 0x020000E9 RID: 233
public class FollowState : FSMState<AILogic>
{
	// Token: 0x06000750 RID: 1872 RVA: 0x00032E5C File Offset: 0x0003105C
	public override void Enter(AILogic owner, int previous)
	{
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00032E60 File Offset: 0x00031060
	public override void Exit(AILogic owner, int previous)
	{
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00032E64 File Offset: 0x00031064
	public override void Update(AILogic owner)
	{
		owner.Ownner.MoveTo(owner.Ownner.FollowTarget.position, 1f, null);
	}
}
