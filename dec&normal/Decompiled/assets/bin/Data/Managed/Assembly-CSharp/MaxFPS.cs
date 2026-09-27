using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class MaxFPS : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003898 File Offset: 0x00001A98
	private void OnEnable()
	{
		Debug.Log("fffffffffffffffffffffffffffffffffffffffffffffffffffff");
		Application.targetFrameRate = this.maxFPS;
		base.enabled = false;
	}

	// Token: 0x04000045 RID: 69
	public int maxFPS = 60;
}
