using System;

// Token: 0x020001D9 RID: 473
public class FSMState<T> : Singleton<FSMState<T>>
{
	// Token: 0x1700039A RID: 922
	// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0006DE64 File Offset: 0x0006C064
	// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0006DE6C File Offset: 0x0006C06C
	public StateMachine<T> StateMachine { get; set; }

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0006DE78 File Offset: 0x0006C078
	// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0006DE80 File Offset: 0x0006C080
	public T BaseObject { get; set; }

	// Token: 0x060010F8 RID: 4344 RVA: 0x0006DE8C File Offset: 0x0006C08C
	public virtual void Enter(T owner, int previous)
	{
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x0006DE90 File Offset: 0x0006C090
	public virtual void Enter(T owner, int previous, params object[] list)
	{
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x0006DE94 File Offset: 0x0006C094
	public virtual void Exit(T owner, int next)
	{
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0006DE98 File Offset: 0x0006C098
	public virtual void Update(T owner)
	{
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0006DE9C File Offset: 0x0006C09C
	public virtual void Notify(int messageID, params object[] messageParams)
	{
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0006DEA0 File Offset: 0x0006C0A0
	public void SetState(int newId, params object[] list)
	{
		this.StateMachine.SetState(newId, list);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0006DEB0 File Offset: 0x0006C0B0
	public void SetState(int newId)
	{
		this.StateMachine.SetState(newId);
	}
}
