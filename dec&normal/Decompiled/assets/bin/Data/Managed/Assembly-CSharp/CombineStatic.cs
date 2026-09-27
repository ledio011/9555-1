using System;
using UnityEngine;

// Token: 0x02000A68 RID: 2664
public class CombineStatic : MonoBehaviour
{
	// Token: 0x06004DAF RID: 19887 RVA: 0x001A8CFC File Offset: 0x001A6EFC
	private void Awake()
	{
		StaticBatchingUtility.Combine(base.gameObject);
	}
}
