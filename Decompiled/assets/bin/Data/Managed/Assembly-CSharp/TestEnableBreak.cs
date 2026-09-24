using System;
using UnityEngine;

// Token: 0x02000A76 RID: 2678
public class TestEnableBreak : MonoBehaviour
{
	// Token: 0x06004DFB RID: 19963 RVA: 0x001AA570 File Offset: 0x001A8770
	private void OnEnable()
	{
		Debug.Log(base.gameObject.name + " :: Enable!!!!!!!!!!!!!!!!!!!");
	}

	// Token: 0x06004DFC RID: 19964 RVA: 0x001AA58C File Offset: 0x001A878C
	private void OnDisable()
	{
		Debug.Log(base.gameObject.name + " :: Disable!!!!!!!!!!!!!!!!!!!");
	}
}
