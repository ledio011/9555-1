using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("Image Effects/Grayscale")]
[ExecuteInEditMode]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x06000253 RID: 595 RVA: 0x00009F18 File Offset: 0x00008118
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_RampOffset", this.rampOffset);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001C5 RID: 453
	public Texture textureRamp;

	// Token: 0x040001C6 RID: 454
	public float rampOffset;
}
