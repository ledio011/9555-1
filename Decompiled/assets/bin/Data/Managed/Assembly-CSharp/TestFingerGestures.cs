using System;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public class TestFingerGestures : MonoBehaviour
{
	// Token: 0x06003BA1 RID: 15265 RVA: 0x001046F4 File Offset: 0x001028F4
	private void Start()
	{
	}

	// Token: 0x06003BA2 RID: 15266 RVA: 0x001046F8 File Offset: 0x001028F8
	private void Update()
	{
	}

	// Token: 0x06003BA3 RID: 15267 RVA: 0x001046FC File Offset: 0x001028FC
	private void OnTap(TapGesture gesture)
	{
		Debug.Log(string.Concat(new object[]
		{
			"Tap gesture detected at ",
			gesture.Position,
			". It was sent by ",
			gesture.Recognizer.name
		}));
		if (gesture.Selection)
		{
			Debug.Log("Tapped object: " + gesture.Selection.name);
		}
		else
		{
			Debug.Log("No object was tapped at " + gesture.Position);
		}
	}
}
