using System;
using UnityEngine;

// Token: 0x0200089E RID: 2206
public class TestCreateMesh : MonoBehaviour
{
	// Token: 0x06003B99 RID: 15257 RVA: 0x001045FC File Offset: 0x001027FC
	private void Start()
	{
		MeshCreate meshCreate = new SectorMeshCreate();
		Mesh mesh = meshCreate.Create(2f, 360f);
		base.GetComponent<MeshFilter>().mesh = mesh;
	}

	// Token: 0x06003B9A RID: 15258 RVA: 0x0010462C File Offset: 0x0010282C
	private void Update()
	{
	}
}
