using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Contrast Enhance (Unsharp Mask)")]
[ExecuteInEditMode]
[Serializable]
public class ContrastEnhance : PostEffectsBase
{
	// Token: 0x06000064 RID: 100 RVA: 0x00005BC0 File Offset: 0x00003DC0
	public ContrastEnhance()
	{
		this.intensity = 0.5f;
		this.blurSpread = 1f;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00005BE0 File Offset: 0x00003DE0
	public virtual void OnDisable()
	{
		if (this.contrastCompositeMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.contrastCompositeMaterial);
		}
		if (this.separableBlurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.separableBlurMaterial);
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00005C24 File Offset: 0x00003E24
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.contrastCompositeMaterial = this.CheckShaderAndCreateMaterial(this.contrastCompositeShader, this.contrastCompositeMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00005C80 File Offset: 0x00003E80
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			RenderTexture temporary = RenderTexture.GetTemporary((int)((float)source.width / 2f), (int)((float)source.height / 2f), 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
			Graphics.Blit(source, temporary);
			Graphics.Blit(temporary, temporary2);
			this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, this.blurSpread * 1f / (float)temporary2.height, (float)0, (float)0));
			Graphics.Blit(temporary2, temporary3, this.separableBlurMaterial);
			this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * 1f / (float)temporary2.width, (float)0, (float)0, (float)0));
			Graphics.Blit(temporary3, temporary2, this.separableBlurMaterial);
			this.contrastCompositeMaterial.SetTexture("_MainTexBlurred", temporary2);
			this.contrastCompositeMaterial.SetFloat("intensity", this.intensity);
			this.contrastCompositeMaterial.SetFloat("threshhold", this.threshhold);
			Graphics.Blit(source, destination, this.contrastCompositeMaterial);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00005DF0 File Offset: 0x00003FF0
	public override void Main()
	{
	}

	// Token: 0x040000EC RID: 236
	public float intensity;

	// Token: 0x040000ED RID: 237
	public float threshhold;

	// Token: 0x040000EE RID: 238
	private Material separableBlurMaterial;

	// Token: 0x040000EF RID: 239
	private Material contrastCompositeMaterial;

	// Token: 0x040000F0 RID: 240
	public float blurSpread;

	// Token: 0x040000F1 RID: 241
	public Shader separableBlurShader;

	// Token: 0x040000F2 RID: 242
	public Shader contrastCompositeShader;
}
