using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Edge Detection (Color)")]
public class EdgeDetectEffect : ImageEffectBase
{
	// Token: 0x06000247 RID: 583 RVA: 0x00009A88 File Offset: 0x00007C88
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Treshold", this.threshold * this.threshold);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001BA RID: 442
	public float threshold = 0.2f;
}
