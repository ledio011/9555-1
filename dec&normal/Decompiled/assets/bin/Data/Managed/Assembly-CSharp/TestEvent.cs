using System;
using UnityEngine;

// Token: 0x0200089F RID: 2207
public class TestEvent : MonoBehaviour
{
	// Token: 0x06003B9C RID: 15260 RVA: 0x00104638 File Offset: 0x00102838
	private void Start()
	{
		SingletonUnity<MyEvent>.Instance.Register("Test", this, "MyTest");
		SingletonUnity<MyEvent>.Instance.Register("Test", this, "zz");
		SingletonUnity<MyEvent>.Instance.Fire("Test", new object[]
		{
			"first!!!!"
		});
		SingletonUnity<MyEvent>.Instance.DelayFire("Test", 2f, new object[]
		{
			"hahah"
		});
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x001046B0 File Offset: 0x001028B0
	private void Update()
	{
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x001046B4 File Offset: 0x001028B4
	public void MyTest(string str)
	{
		Log.DEBUG_MSG("MyTest1=" + str);
		Log.DEBUG_MSG(Time.time);
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x001046D8 File Offset: 0x001028D8
	public void zz(string str)
	{
		Log.DEBUG_MSG("zz=" + str);
	}
}
