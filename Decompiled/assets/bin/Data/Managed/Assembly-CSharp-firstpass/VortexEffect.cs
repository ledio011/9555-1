using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
[AddComponentMenu("Image Effects/Vortex")]
[ExecuteInEditMode]
public class VortexEffect : ImageEffectBase
{
	// Token: 0x06000273 RID: 627 RVA: 0x0000ABD8 File Offset: 0x00008DD8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x040001F0 RID: 496
	public Vector2 radius = new Vector2(0.4f, 0.4f);

	// Token: 0x040001F1 RID: 497
	public float angle = 50f;

	// Token: 0x040001F2 RID: 498
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
