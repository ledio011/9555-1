using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
[AddComponentMenu("Image Effects/Screen Overlay")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class ScreenOverlay : PostEffectsBase
{
	// Token: 0x060000B7 RID: 183 RVA: 0x00009020 File Offset: 0x00007220
	public ScreenOverlay()
	{
		this.blendMode = ScreenOverlay.OverlayBlendMode.Overlay;
		this.intensity = 1f;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000903C File Offset: 0x0000723C
	public virtual void OnDisable()
	{
		if (this.overlayMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.overlayMaterial);
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000905C File Offset: 0x0000725C
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.overlayMaterial = this.CheckShaderAndCreateMaterial(this.overlayShader, this.overlayMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00009098 File Offset: 0x00007298
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.overlayMaterial.SetFloat("_Intensity", this.intensity);
			this.overlayMaterial.SetTexture("_Overlay", this.texture);
			Graphics.Blit(source, destination, this.overlayMaterial, (int)this.blendMode);
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000090FC File Offset: 0x000072FC
	public override void Main()
	{
	}

	// Token: 0x04000179 RID: 377
	public ScreenOverlay.OverlayBlendMode blendMode;

	// Token: 0x0400017A RID: 378
	public float intensity;

	// Token: 0x0400017B RID: 379
	public Texture2D texture;

	// Token: 0x0400017C RID: 380
	public Shader overlayShader;

	// Token: 0x0400017D RID: 381
	private Material overlayMaterial;

	// Token: 0x0200002D RID: 45
	[Serializable]
	public enum OverlayBlendMode
	{
		// Token: 0x0400017F RID: 383
		AddSub,
		// Token: 0x04000180 RID: 384
		ScreenBlend,
		// Token: 0x04000181 RID: 385
		Multiply,
		// Token: 0x04000182 RID: 386
		Overlay,
		// Token: 0x04000183 RID: 387
		AlphaBlend
	}
}
