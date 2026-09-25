using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
[AddComponentMenu("Image Effects/Sepia Tone")]
[ExecuteInEditMode]
public class SepiaToneEffect : ImageEffectBase
{
	// Token: 0x0600026F RID: 623 RVA: 0x0000AB1C File Offset: 0x00008D1C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, base.material);
	}
}
