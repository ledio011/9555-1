using System;
using UnityEngine;

// Token: 0x020008A9 RID: 2217
public class TestTime : MonoBehaviour
{
	// Token: 0x06003BC4 RID: 15300 RVA: 0x00104AF0 File Offset: 0x00102CF0
	private void Start()
	{
		this.Test();
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x00104AF8 File Offset: 0x00102CF8
	private void Test()
	{
		Debug.Log("one1=" + Time.time);
		vp_Timer.In(2f, delegate()
		{
			Debug.Log("one=" + Time.time);
		}, null);
		vp_Timer.In(1f, new vp_Timer.ArgCallback(this.tesmore), new object[]
		{
			"zzzz",
			0.5f
		}, null);
		vp_Timer.Handle timerHandle = new vp_Timer.Handle();
		vp_Timer.In(3f, delegate()
		{
			Debug.Log("cancel");
		}, timerHandle);
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x00104BA8 File Offset: 0x00102DA8
	private void tesmore(object o)
	{
		object[] array = (object[])o;
		Debug.Log("more=" + array[1]);
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x00104BD0 File Offset: 0x00102DD0
	private void Update()
	{
	}
}
