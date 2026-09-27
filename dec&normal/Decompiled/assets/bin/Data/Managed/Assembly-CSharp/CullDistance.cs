using System;
using UnityEngine;

// Token: 0x0200083E RID: 2110
public class CullDistance : MonoBehaviour
{
	// Token: 0x06003624 RID: 13860 RVA: 0x000DE53C File Offset: 0x000DC73C
	private void Awake()
	{
		this.distances[24] = 0f;
		base.camera.layerCullSpherical = true;
		base.camera.layerCullDistances = this.distances;
	}

	// Token: 0x04002399 RID: 9113
	public float[] distances = new float[32];
}
