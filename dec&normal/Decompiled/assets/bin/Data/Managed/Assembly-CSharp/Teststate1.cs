using System;
using UnityEngine;

// Token: 0x020008A2 RID: 2210
public class Teststate1 : FSMState<TestMachine>
{
	// Token: 0x06003BA9 RID: 15273 RVA: 0x001047D8 File Offset: 0x001029D8
	public override void Enter(TestMachine owner, int previous)
	{
		Debug.Log("Teststate1 Enter");
	}

	// Token: 0x06003BAA RID: 15274 RVA: 0x001047E4 File Offset: 0x001029E4
	public override void Update(TestMachine owner)
	{
		Debug.Log("Teststate1 Update");
	}

	// Token: 0x06003BAB RID: 15275 RVA: 0x001047F0 File Offset: 0x001029F0
	public override void Exit(TestMachine owner, int next)
	{
		Debug.Log("Teststate1 Exit");
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x001047FC File Offset: 0x001029FC
	public override void Notify(int messageID, params object[] messageParams)
	{
	}
}
