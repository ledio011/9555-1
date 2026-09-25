using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x0600014A RID: 330 RVA: 0x00008E44 File Offset: 0x00007044
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00008E54 File Offset: 0x00007054
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x04000167 RID: 359
	public static Transform root;
}
