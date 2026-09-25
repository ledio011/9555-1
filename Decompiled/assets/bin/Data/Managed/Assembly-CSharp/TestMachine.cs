using System;
using UnityEngine;

// Token: 0x020008A5 RID: 2213
public class TestMachine : MonoBehaviour
{
	// Token: 0x06003BB3 RID: 15283 RVA: 0x00104838 File Offset: 0x00102A38
	private void InitStateMachine()
	{
		this.mStateMachine = new StateMachine<TestMachine>(this, 2);
		this.mStateMachine.AddState(Singleton<Teststate1>.Instance, 0);
		this.mStateMachine.AddState(Singleton<Teststate2>.Instance, 1);
		this.mStateMachine.SetState(0);
		this.mStateMachine.SetState(1);
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x0010488C File Offset: 0x00102A8C
	private void Start()
	{
		this.InitStateMachine();
	}

	// Token: 0x06003BB5 RID: 15285 RVA: 0x00104894 File Offset: 0x00102A94
	private void Update()
	{
		this.mStateMachine.Update();
	}

	// Token: 0x04002715 RID: 10005
	private StateMachine<TestMachine> mStateMachine;
}
