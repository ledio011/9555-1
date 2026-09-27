using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[SerializeField]
public class BaseAIState : Singleton<BaseAIState>
{
	// Token: 0x06000738 RID: 1848 RVA: 0x00032CD8 File Offset: 0x00030ED8
	public virtual void Enter(ObjNPC owner)
	{
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00032CDC File Offset: 0x00030EDC
	public virtual void Exit(ObjNPC owner)
	{
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00032CE0 File Offset: 0x00030EE0
	public virtual void UpdateAI(ObjNPC owner)
	{
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00032CE4 File Offset: 0x00030EE4
	public virtual void CheckState(ObjNPC owner)
	{
	}
}
