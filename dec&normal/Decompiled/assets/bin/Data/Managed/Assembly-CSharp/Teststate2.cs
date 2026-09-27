using System;
using UnityEngine;

// Token: 0x020008A3 RID: 2211
public class Teststate2 : FSMState<TestMachine>
{
	// Token: 0x06003BAE RID: 15278 RVA: 0x00104808 File Offset: 0x00102A08
	public override void Enter(TestMachine owner, int previous)
	{
		Debug.Log("Teststate2 Enter");
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x00104814 File Offset: 0x00102A14
	public override void Update(TestMachine owner)
	{
		Debug.Log("Teststate2 Update");
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x00104820 File Offset: 0x00102A20
	public override void Exit(TestMachine owner, int next)
	{
		Debug.Log("Teststate2 Exit");
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x0010482C File Offset: 0x00102A2C
	public override void Notify(int messageID, params object[] messageParams)
	{
	}
}
