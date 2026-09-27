using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
[AddComponentMenu("Image Effects/Vignette and Chromatic Aberration")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[Serializable]
public class Vignetting : PostEffectsBase
{
	// Token: 0x060000D5 RID: 213 RVA: 0x0000A448 File Offset: 0x00008648
	public Vignetting()
	{
		this.intensity = 0.375f;
		this.chromaticAberration = 0.2f;
		this.blur = 0.1f;
		this.blurSpread = 1.5f;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000A488 File Offset: 0x00008688
	public virtual void OnDisable()
	{
		if (this.vignetteMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.vignetteMaterial);
		}
		if (this.separableBlurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.separableBlurMaterial);
		}
		if (this.chromAberrationMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.chromAberrationMaterial);
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000A4E8 File Offset: 0x000086E8
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		this.chromAberrationMaterial = this.CheckShaderAndCreateMaterial(this.chromAberrationShader, this.chromAberrationMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000A55C File Offset: 0x0000875C
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / 2f), (int)((float)source.height / 2f), 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
			RenderTexture temporary4 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
			Graphics.Blit(source, temporary2, this.chromAberrationMaterial, 0);
			Graphics.Blit(temporary2, temporary3);
			for (int i = 0; i < 2; i++)
			{
				this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, this.blurSpread * num2, (float)0, (float)0));
				Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
				this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * num2 / num, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
			}
			this.vignetteMaterial.SetFloat("_Intensity", this.intensity);
			this.vignetteMaterial.SetFloat("_Blur", this.blur);
			this.vignetteMaterial.SetTexture("_VignetteTex", temporary3);
			Graphics.Blit(source, temporary, this.vignetteMaterial);
			this.chromAberrationMaterial.SetFloat("_ChromaticAberration", this.chromaticAberration);
			Graphics.Blit(temporary, destination, this.chromAberrationMaterial, 1);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000A73C File Offset: 0x0000893C
	public override void Main()
	{
	}

	// Token: 0x040001C5 RID: 453
	public float intensity;

	// Token: 0x040001C6 RID: 454
	public float chromaticAberration;

	// Token: 0x040001C7 RID: 455
	public float blur;

	// Token: 0x040001C8 RID: 456
	public float blurSpread;

	// Token: 0x040001C9 RID: 457
	public Shader vignetteShader;

	// Token: 0x040001CA RID: 458
	private Material vignetteMaterial;

	// Token: 0x040001CB RID: 459
	public Shader separableBlurShader;

	// Token: 0x040001CC RID: 460
	private Material separableBlurMaterial;

	// Token: 0x040001CD RID: 461
	public Shader chromAberrationShader;

	// Token: 0x040001CE RID: 462
	private Material chromAberrationMaterial;
}
