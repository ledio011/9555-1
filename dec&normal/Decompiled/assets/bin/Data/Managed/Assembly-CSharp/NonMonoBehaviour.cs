using System;
using UnityEngine;

// Token: 0x02000A89 RID: 2697
public class NonMonoBehaviour
{
	// Token: 0x06004E6D RID: 20077 RVA: 0x001AD2D4 File Offset: 0x001AB4D4
	public void Test()
	{
		TestComponent testComponent = (TestComponent)GameObject.Find("ExternalGameObject").GetComponent("TestComponent");
		testComponent.Test("Hello World!");
	}
}
