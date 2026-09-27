using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Correction (Ramp)")]
public class ColorCorrectionEffect : ImageEffectBase
{
	// Token: 0x0600023B RID: 571 RVA: 0x0000964C File Offset: 0x0000784C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001AC RID: 428
	public Texture textureRamp;
}
