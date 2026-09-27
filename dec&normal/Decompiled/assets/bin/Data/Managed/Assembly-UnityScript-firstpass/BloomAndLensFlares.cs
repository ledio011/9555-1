using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Bloom (HDR, Lens Flares)")]
[ExecuteInEditMode]
[Serializable]
public class BloomAndLensFlares : PostEffectsBase
{
	// Token: 0x06000050 RID: 80 RVA: 0x00004BAC File Offset: 0x00002DAC
	public BloomAndLensFlares()
	{
		this.screenBlendMode = BloomScreenBlendMode.Add;
		this.hdr = HDRBloomMode.Auto;
		this.sepBlurSpread = 1.5f;
		this.useSrcAlphaAsMask = 0.5f;
		this.bloomIntensity = 1f;
		this.bloomThreshhold = 0.5f;
		this.bloomBlurIterations = 2;
		this.hollywoodFlareBlurIterations = 2;
		this.lensflareMode = LensflareStyle34.Anamorphic;
		this.hollyStretchWidth = 3.5f;
		this.lensflareIntensity = 1f;
		this.lensflareThreshhold = 0.3f;
		this.flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		this.flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		this.flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		this.flareColorD = new Color(0.8f, 0.4f, (float)0, 0.75f);
		this.blurWidth = 1f;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00004CB4 File Offset: 0x00002EB4
	public virtual void OnDisable()
	{
		if (this.screenBlend)
		{
			UnityEngine.Object.DestroyImmediate(this.screenBlend);
		}
		if (this.lensFlareMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.lensFlareMaterial);
		}
		if (this.vignetteMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.vignetteMaterial);
		}
		if (this.separableBlurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.separableBlurMaterial);
		}
		if (this.addBrightStuffBlendOneOneMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.addBrightStuffBlendOneOneMaterial);
		}
		if (this.hollywoodFlaresMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.hollywoodFlaresMaterial);
		}
		if (this.brightPassFilterMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.brightPassFilterMaterial);
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004D80 File Offset: 0x00002F80
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.screenBlend = this.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
		this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		this.addBrightStuffBlendOneOneMaterial = this.CheckShaderAndCreateMaterial(this.addBrightStuffOneOneShader, this.addBrightStuffBlendOneOneMaterial);
		this.hollywoodFlaresMaterial = this.CheckShaderAndCreateMaterial(this.hollywoodFlaresShader, this.hollywoodFlaresMaterial);
		this.brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004E54 File Offset: 0x00003054
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.doHdr = false;
			if (this.hdr == HDRBloomMode.Auto)
			{
				bool flag;
				if (flag = (source.format == RenderTextureFormat.ARGBHalf))
				{
					flag = this.camera.hdr;
				}
				this.doHdr = flag;
			}
			else
			{
				this.doHdr = (this.hdr == HDRBloomMode.On);
			}
			bool supportHDRTextures;
			if (supportHDRTextures = this.doHdr)
			{
				supportHDRTextures = this.supportHDRTextures;
			}
			this.doHdr = supportHDRTextures;
			BloomScreenBlendMode pass = this.screenBlendMode;
			if (this.doHdr)
			{
				pass = BloomScreenBlendMode.Add;
			}
			RenderTextureFormat format = (!this.doHdr) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, format);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			Graphics.Blit(source, temporary, this.screenBlend, 2);
			Graphics.Blit(temporary, temporary2, this.screenBlend, 2);
			RenderTexture.ReleaseTemporary(temporary);
			this.BrightFilter(this.bloomThreshhold, this.useSrcAlphaAsMask, temporary2, temporary3);
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num3 = (1f + (float)i * 0.5f) * this.sepBlurSpread;
				this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, num3 * num2, (float)0, (float)0));
				Graphics.Blit((i != 0) ? temporary2 : temporary3, temporary4, this.separableBlurMaterial);
				this.separableBlurMaterial.SetVector("offsets", new Vector4(num3 / num * num2, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary4, temporary2, this.separableBlurMaterial);
			}
			if (this.lensflares)
			{
				if (this.lensflareMode == LensflareStyle34.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshhold, (float)0, temporary2, temporary4);
					this.Vignette(0.975f, temporary4, temporary3);
					this.BlendFlares(temporary3, temporary2);
				}
				else
				{
					this.hollywoodFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshhold, 1f / (1f - this.lensflareThreshhold), (float)0, (float)0));
					this.hollywoodFlaresMaterial.SetVector("tintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 2);
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 3);
					this.hollywoodFlaresMaterial.SetVector("offsets", new Vector4(this.sepBlurSpread * 1f / num * num2, (float)0, (float)0, (float)0));
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 2f);
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 1);
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 4f);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					if (this.lensflareMode == LensflareStyle34.Anamorphic)
					{
						for (int j = 0; j < this.hollywoodFlareBlurIterations; j++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
						}
						this.AddTo(1f, temporary3, temporary2);
					}
					else
					{
						for (int k = 0; k < this.hollywoodFlareBlurIterations; k++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
						}
						this.Vignette(1f, temporary3, temporary4);
						this.BlendFlares(temporary4, temporary3);
						this.AddTo(1f, temporary3, temporary2);
					}
				}
			}
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			Graphics.Blit(temporary2, destination, this.screenBlend, (int)pass);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000053C4 File Offset: 0x000035C4
	private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
	{
		this.addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
		Graphics.Blit(from, to, this.addBrightStuffBlendOneOneMaterial);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000053E4 File Offset: 0x000035E4
	private void BlendFlares(RenderTexture from, RenderTexture to)
	{
		this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
		Graphics.Blit(from, to, this.lensFlareMaterial);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00005530 File Offset: 0x00003730
	private void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
	{
		if (this.doHdr)
		{
			this.brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f, (float)0, (float)0));
		}
		else
		{
			this.brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f / (1f - thresh), (float)0, (float)0));
		}
		this.brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
		Graphics.Blit(from, to, this.brightPassFilterMaterial);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000055B8 File Offset: 0x000037B8
	private void Vignette(float amount, RenderTexture from, RenderTexture to)
	{
		if (this.lensFlareVignetteMask)
		{
			this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
			Graphics.Blit(from, to, this.screenBlend, 3);
		}
		else
		{
			this.vignetteMaterial.SetFloat("vignetteIntensity", amount);
			Graphics.Blit(from, to, this.vignetteMaterial);
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000561C File Offset: 0x0000381C
	public override void Main()
	{
	}

	// Token: 0x040000AC RID: 172
	public TweakMode34 tweakMode;

	// Token: 0x040000AD RID: 173
	public BloomScreenBlendMode screenBlendMode;

	// Token: 0x040000AE RID: 174
	public HDRBloomMode hdr;

	// Token: 0x040000AF RID: 175
	private bool doHdr;

	// Token: 0x040000B0 RID: 176
	public float sepBlurSpread;

	// Token: 0x040000B1 RID: 177
	public float useSrcAlphaAsMask;

	// Token: 0x040000B2 RID: 178
	public float bloomIntensity;

	// Token: 0x040000B3 RID: 179
	public float bloomThreshhold;

	// Token: 0x040000B4 RID: 180
	public int bloomBlurIterations;

	// Token: 0x040000B5 RID: 181
	public bool lensflares;

	// Token: 0x040000B6 RID: 182
	public int hollywoodFlareBlurIterations;

	// Token: 0x040000B7 RID: 183
	public LensflareStyle34 lensflareMode;

	// Token: 0x040000B8 RID: 184
	public float hollyStretchWidth;

	// Token: 0x040000B9 RID: 185
	public float lensflareIntensity;

	// Token: 0x040000BA RID: 186
	public float lensflareThreshhold;

	// Token: 0x040000BB RID: 187
	public Color flareColorA;

	// Token: 0x040000BC RID: 188
	public Color flareColorB;

	// Token: 0x040000BD RID: 189
	public Color flareColorC;

	// Token: 0x040000BE RID: 190
	public Color flareColorD;

	// Token: 0x040000BF RID: 191
	public float blurWidth;

	// Token: 0x040000C0 RID: 192
	public Texture2D lensFlareVignetteMask;

	// Token: 0x040000C1 RID: 193
	public Shader lensFlareShader;

	// Token: 0x040000C2 RID: 194
	private Material lensFlareMaterial;

	// Token: 0x040000C3 RID: 195
	public Shader vignetteShader;

	// Token: 0x040000C4 RID: 196
	private Material vignetteMaterial;

	// Token: 0x040000C5 RID: 197
	public Shader separableBlurShader;

	// Token: 0x040000C6 RID: 198
	private Material separableBlurMaterial;

	// Token: 0x040000C7 RID: 199
	public Shader addBrightStuffOneOneShader;

	// Token: 0x040000C8 RID: 200
	private Material addBrightStuffBlendOneOneMaterial;

	// Token: 0x040000C9 RID: 201
	public Shader screenBlendShader;

	// Token: 0x040000CA RID: 202
	private Material screenBlend;

	// Token: 0x040000CB RID: 203
	public Shader hollywoodFlaresShader;

	// Token: 0x040000CC RID: 204
	private Material hollywoodFlaresMaterial;

	// Token: 0x040000CD RID: 205
	public Shader brightPassFilterShader;

	// Token: 0x040000CE RID: 206
	private Material brightPassFilterMaterial;
}
