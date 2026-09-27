using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
[AddComponentMenu("Image Effects/Twirl")]
[ExecuteInEditMode]
public class TwirlEffect : ImageEffectBase
{
	// Token: 0x06000271 RID: 625 RVA: 0x0000AB6C File Offset: 0x00008D6C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x040001ED RID: 493
	public Vector2 radius = new Vector2(0.3f, 0.3f);

	// Token: 0x040001EE RID: 494
	public float angle = 50f;

	// Token: 0x040001EF RID: 495
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
