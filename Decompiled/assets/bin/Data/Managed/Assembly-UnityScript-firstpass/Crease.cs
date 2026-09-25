using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Crease")]
[Serializable]
public class Crease : PostEffectsBase
{
	// Token: 0x06000069 RID: 105 RVA: 0x00005DF4 File Offset: 0x00003FF4
	public Crease()
	{
		this.intensity = 0.5f;
		this.softness = 1;
		this.spread = 1f;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00005E1C File Offset: 0x0000401C
	public virtual void OnDisable()
	{
		if (this.blurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.blurMaterial);
		}
		if (this.depthFetchMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.depthFetchMaterial);
		}
		if (this.creaseApplyMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.creaseApplyMaterial);
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00005E7C File Offset: 0x0000407C
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.blurMaterial = this.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
		this.depthFetchMaterial = this.CheckShaderAndCreateMaterial(this.depthFetchShader, this.depthFetchMaterial);
		this.creaseApplyMaterial = this.CheckShaderAndCreateMaterial(this.creaseApplyShader, this.creaseApplyMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00005EF0 File Offset: 0x000040F0
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
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0);
			Graphics.Blit(source, temporary, this.depthFetchMaterial);
			Graphics.Blit(temporary, temporary2);
			for (int i = 0; i < this.softness; i++)
			{
				this.blurMaterial.SetVector("offsets", new Vector4((float)0, this.spread * num2, (float)0, (float)0));
				Graphics.Blit(temporary2, temporary3, this.blurMaterial);
				this.blurMaterial.SetVector("offsets", new Vector4(this.spread * num2 / num, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary3, temporary2, this.blurMaterial);
			}
			this.creaseApplyMaterial.SetTexture("_HrDepthTex", temporary);
			this.creaseApplyMaterial.SetTexture("_LrDepthTex", temporary2);
			this.creaseApplyMaterial.SetFloat("intensity", this.intensity);
			Graphics.Blit(source, destination, this.creaseApplyMaterial);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00006064 File Offset: 0x00004264
	public override void Main()
	{
	}

	// Token: 0x040000F3 RID: 243
	public float intensity;

	// Token: 0x040000F4 RID: 244
	public int softness;

	// Token: 0x040000F5 RID: 245
	public float spread;

	// Token: 0x040000F6 RID: 246
	public Shader blurShader;

	// Token: 0x040000F7 RID: 247
	private Material blurMaterial;

	// Token: 0x040000F8 RID: 248
	public Shader depthFetchShader;

	// Token: 0x040000F9 RID: 249
	private Material depthFetchMaterial;

	// Token: 0x040000FA RID: 250
	public Shader creaseApplyShader;

	// Token: 0x040000FB RID: 251
	private Material creaseApplyMaterial;
}
