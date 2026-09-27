using System;
using UnityEngine;

// Token: 0x0200089C RID: 2204
public class TestBone : MonoBehaviour
{
	// Token: 0x06003B92 RID: 15250 RVA: 0x00104580 File Offset: 0x00102780
	private void Start()
	{
		Transform[] bones = base.GetComponent<SkinnedMeshRenderer>().bones;
		for (int i = 0; i < bones.Length; i++)
		{
			Debug.Log(bones[i]);
		}
	}

	// Token: 0x06003B93 RID: 15251 RVA: 0x001045B8 File Offset: 0x001027B8
	private void Update()
	{
	}
}
