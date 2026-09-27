using System;

// Token: 0x020001DA RID: 474
public class StateMachine<T>
{
	// Token: 0x060010FF RID: 4351 RVA: 0x0006DEC0 File Offset: 0x0006C0C0
	public StateMachine()
	{
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0006DED8 File Offset: 0x0006C0D8
	public StateMachine(T t, int numState)
	{
		this.mObject = t;
		this.mStateType = new FSMState<T>[numState];
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06001101 RID: 4353 RVA: 0x0006DF04 File Offset: 0x0006C104
	public T BaseObject
	{
		get
		{
			return this.mObject;
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06001102 RID: 4354 RVA: 0x0006DF0C File Offset: 0x0006C10C
	public int CurrentStateId
	{
		get
		{
			return this.mCurrentId;
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001103 RID: 4355 RVA: 0x0006DF14 File Offset: 0x0006C114
	public FSMState<T> CurrentState
	{
		get
		{
			return this.mCurrentState;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06001104 RID: 4356 RVA: 0x0006DF1C File Offset: 0x0006C11C
	public int PreStateId
	{
		get
		{
			return this.mPreId;
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06001105 RID: 4357 RVA: 0x0006DF24 File Offset: 0x0006C124
	public FSMState<T> PreState
	{
		get
		{
			return this.mPreState;
		}
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0006DF2C File Offset: 0x0006C12C
	public void Notify(int messageID, params object[] messageParams)
	{
		this.mCurrentState.Notify(messageID, messageParams);
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0006DF3C File Offset: 0x0006C13C
	public void AddState(FSMState<T> State, int Id)
	{
		this.mStateType[Id] = State;
		this.mStateType[Id].StateMachine = this;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0006DF58 File Offset: 0x0006C158
	public void SetState(int newId, params object[] list)
	{
		int previous = this.mCurrentId;
		this.SetInState(newId);
		this.mCurrentState.Enter(this.mObject, previous, list);
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0006DF88 File Offset: 0x0006C188
	public void SetState(int newId)
	{
		int previous = this.mCurrentId;
		this.SetInState(newId);
		this.mCurrentState.Enter(this.mObject, previous);
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0006DFB8 File Offset: 0x0006C1B8
	private void SetInState(int newId)
	{
		if (this.mCurrentState != null)
		{
			this.mPreState = this.mCurrentState;
			this.mCurrentState.Exit(this.mObject, newId);
		}
		if (newId >= 0)
		{
			this.mCurrentState = this.mStateType[newId];
		}
		else
		{
			this.mCurrentState = null;
		}
		this.mPreId = this.mCurrentId;
		this.mCurrentId = newId;
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0006E024 File Offset: 0x0006C224
	public void RevertToPreviousState()
	{
		this.SetState(this.mPreId);
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0006E034 File Offset: 0x0006C234
	public void Stop()
	{
		this.SetInState(-1);
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0006E040 File Offset: 0x0006C240
	public void Update()
	{
		this.mCurrentState.Update(this.mObject);
	}

	// Token: 0x04001486 RID: 5254
	private T mObject;

	// Token: 0x04001487 RID: 5255
	public int mCurrentId = -1;

	// Token: 0x04001488 RID: 5256
	private FSMState<T>[] mStateType;

	// Token: 0x04001489 RID: 5257
	private FSMState<T> mCurrentState;

	// Token: 0x0400148A RID: 5258
	public int mPreId = -1;

	// Token: 0x0400148B RID: 5259
	private FSMState<T> mPreState;
}
