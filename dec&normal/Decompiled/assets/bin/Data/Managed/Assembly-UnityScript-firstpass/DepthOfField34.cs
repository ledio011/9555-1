using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Depth of Field (3.4)")]
[ExecuteInEditMode]
[Serializable]
public class DepthOfField34 : PostEffectsBase
{
	// Token: 0x0600006E RID: 110 RVA: 0x00006068 File Offset: 0x00004268
	public DepthOfField34()
	{
		this.quality = Dof34QualitySetting.OnlyBackground;
		this.resolution = DofResolution.Low;
		this.simpleTweakMode = true;
		this.focalPoint = 1f;
		this.smoothness = 0.5f;
		this.focalZStartCurve = 1f;
		this.focalZEndCurve = 1f;
		this.focalStartCurve = 2f;
		this.focalEndCurve = 2f;
		this.focalDistance01 = 0.1f;
		this.bluriness = DofBlurriness.High;
		this.maxBlurSpread = 1.75f;
		this.foregroundBlurExtrude = 1.15f;
		this.bokehDestination = BokehDestination.Background;
		this.widthOverHeight = 1.25f;
		this.oneOverBaseSize = 0.001953125f;
		this.bokehSupport = true;
		this.bokehScale = 2.4f;
		this.bokehIntensity = 0.15f;
		this.bokehThreshholdContrast = 0.1f;
		this.bokehThreshholdLuminance = 0.55f;
		this.bokehDownsample = 1;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00006168 File Offset: 0x00004368
	public virtual void CreateMaterials()
	{
		this.dofBlurMaterial = this.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
		this.dofMaterial = this.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
		this.bokehSupport = this.bokehShader.isSupported;
		if (this.bokeh && this.bokehSupport && this.bokehShader)
		{
			this.bokehMaterial = this.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000061F4 File Offset: 0x000043F4
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.dofBlurMaterial = this.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
		this.dofMaterial = this.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
		this.bokehSupport = this.bokehShader.isSupported;
		if (this.bokeh && this.bokehSupport && this.bokehShader)
		{
			this.bokehMaterial = this.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
		}
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000062A0 File Offset: 0x000044A0
	public virtual void OnDisable()
	{
		Quads.Cleanup();
		if (this.dofBlurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.dofBlurMaterial);
		}
		if (this.dofMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.dofMaterial);
		}
		if (this.bokehMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.bokehMaterial);
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00006304 File Offset: 0x00004504
	public override void OnEnable()
	{
		this.camera.depthTextureMode = (this.camera.depthTextureMode | DepthTextureMode.Depth);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000632C File Offset: 0x0000452C
	public virtual float FocalDistance01(float worldDist)
	{
		return this.camera.WorldToViewportPoint((worldDist - this.camera.nearClipPlane) * this.camera.transform.forward + this.camera.transform.position).z / (this.camera.farClipPlane - this.camera.nearClipPlane);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000639C File Offset: 0x0000459C
	public virtual int GetDividerBasedOnQuality()
	{
		int result = 1;
		if (this.resolution == DofResolution.Medium)
		{
			result = 2;
		}
		else if (this.resolution == DofResolution.Low)
		{
			result = 2;
		}
		return result;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000063D0 File Offset: 0x000045D0
	public virtual int GetLowResolutionDividerBasedOnQuality(int baseDivider)
	{
		int num = baseDivider;
		if (this.resolution == DofResolution.High)
		{
			num *= 2;
		}
		if (this.resolution == DofResolution.Low)
		{
			num *= 2;
		}
		return num;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00006400 File Offset: 0x00004600
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.smoothness < 0.1f)
			{
				this.smoothness = 0.1f;
			}
			bool flag;
			if (flag = this.bokeh)
			{
				flag = this.bokehSupport;
			}
			this.bokeh = flag;
			float num = (!this.bokeh) ? 1f : DepthOfField34.BOKEH_EXTRA_BLUR;
			bool flag2 = this.quality > Dof34QualitySetting.OnlyBackground;
			float num2 = this.focalSize / (this.camera.farClipPlane - this.camera.nearClipPlane);
			if (this.simpleTweakMode)
			{
				this.focalDistance01 = ((!this.objectFocus) ? this.FocalDistance01(this.focalPoint) : (this.camera.WorldToViewportPoint(this.objectFocus.position).z / this.camera.farClipPlane));
				this.focalStartCurve = this.focalDistance01 * this.smoothness;
				this.focalEndCurve = this.focalStartCurve;
				bool flag3;
				if (flag3 = flag2)
				{
					flag3 = (this.focalPoint > this.camera.nearClipPlane + float.Epsilon);
				}
				flag2 = flag3;
			}
			else
			{
				if (this.objectFocus)
				{
					Vector3 vector = this.camera.WorldToViewportPoint(this.objectFocus.position);
					vector.z /= this.camera.farClipPlane;
					this.focalDistance01 = vector.z;
				}
				else
				{
					this.focalDistance01 = this.FocalDistance01(this.focalZDistance);
				}
				this.focalStartCurve = this.focalZStartCurve;
				this.focalEndCurve = this.focalZEndCurve;
				bool flag4;
				if (flag4 = flag2)
				{
					flag4 = (this.focalPoint > this.camera.nearClipPlane + float.Epsilon);
				}
				flag2 = flag4;
			}
			this.widthOverHeight = 1f * (float)source.width / (1f * (float)source.height);
			this.oneOverBaseSize = 0.001953125f;
			this.dofMaterial.SetFloat("_ForegroundBlurExtrude", this.foregroundBlurExtrude);
			this.dofMaterial.SetVector("_CurveParams", new Vector4((!this.simpleTweakMode) ? this.focalStartCurve : (1f / this.focalStartCurve), (!this.simpleTweakMode) ? this.focalEndCurve : (1f / this.focalEndCurve), num2 * 0.5f, this.focalDistance01));
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), (float)0, (float)0));
			int dividerBasedOnQuality = this.GetDividerBasedOnQuality();
			int lowResolutionDividerBasedOnQuality = this.GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
			this.AllocateTextures(flag2, source, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
			Graphics.Blit(source, source, this.dofMaterial, 3);
			this.Downsample(source, this.mediumRezWorkTexture);
			this.Blur(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DofBlurriness.Low, 4, this.maxBlurSpread);
			if (this.bokeh && (this.bokehDestination & BokehDestination.Background) != (BokehDestination)0)
			{
				this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThreshholdContrast, this.bokehThreshholdLuminance, 0.95f, (float)0));
				Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
				Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread * num);
			}
			else
			{
				this.Downsample(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread);
			}
			this.dofBlurMaterial.SetTexture("_TapLow", this.lowRezWorkTexture);
			this.dofBlurMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(null, this.finalDefocus, this.dofBlurMaterial, 3);
			if (this.bokeh && (this.bokehDestination & BokehDestination.Background) != (BokehDestination)0)
			{
				this.AddBokeh(this.bokehSource2, this.bokehSource, this.finalDefocus);
			}
			this.dofMaterial.SetTexture("_TapLowBackground", this.finalDefocus);
			this.dofMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(source, (!flag2) ? destination : this.foregroundTexture, this.dofMaterial, (!this.visualize) ? 0 : 2);
			if (flag2)
			{
				Graphics.Blit(this.foregroundTexture, source, this.dofMaterial, 5);
				this.Downsample(source, this.mediumRezWorkTexture);
				this.BlurFg(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DofBlurriness.Low, 2, this.maxBlurSpread);
				if (this.bokeh && (this.bokehDestination & BokehDestination.Foreground) != (BokehDestination)0)
				{
					this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThreshholdContrast * 0.5f, this.bokehThreshholdLuminance, (float)0, (float)0));
					Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
					Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
					this.BlurFg(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread * num);
				}
				else
				{
					this.BlurFg(this.mediumRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread);
				}
				Graphics.Blit(this.lowRezWorkTexture, this.finalDefocus);
				this.dofMaterial.SetTexture("_TapLowForeground", this.finalDefocus);
				Graphics.Blit(source, destination, this.dofMaterial, (!this.visualize) ? 4 : 1);
				if (this.bokeh && (this.bokehDestination & BokehDestination.Foreground) != (BokehDestination)0)
				{
					this.AddBokeh(this.bokehSource2, this.bokehSource, destination);
				}
			}
			this.ReleaseTextures();
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00006A10 File Offset: 0x00004C10
	public virtual void Blur(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
		if (iterations > DofBlurriness.Low)
		{
			this.BlurHex(from, to, blurPass, spread, temporary);
			if (iterations > DofBlurriness.High)
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
				Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
		}
		else
		{
			this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
			Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00006B50 File Offset: 0x00004D50
	public virtual void BlurFg(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
	{
		this.dofBlurMaterial.SetTexture("_TapHigh", from);
		RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
		if (iterations > DofBlurriness.Low)
		{
			this.BlurHex(from, to, blurPass, spread, temporary);
			if (iterations > DofBlurriness.High)
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
				Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
		}
		else
		{
			this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
			Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00006CA0 File Offset: 0x00004EA0
	public virtual void BlurHex(RenderTexture from, RenderTexture to, int blurPass, float spread, RenderTexture tmp)
	{
		this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
		Graphics.Blit(from, tmp, this.dofBlurMaterial, blurPass);
		this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
		Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
		this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, spread * this.oneOverBaseSize, (float)0, (float)0));
		Graphics.Blit(to, tmp, this.dofBlurMaterial, blurPass);
		this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, -spread * this.oneOverBaseSize, (float)0, (float)0));
		Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00006DBC File Offset: 0x00004FBC
	public virtual void Downsample(RenderTexture from, RenderTexture to)
	{
		this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)to.width), 1f / (1f * (float)to.height), (float)0, (float)0));
		Graphics.Blit(from, to, this.dofMaterial, DepthOfField34.SMOOTH_DOWNSAMPLE_PASS);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00006E1C File Offset: 0x0000501C
	public virtual void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
	{
		if (this.bokehMaterial)
		{
			Mesh[] meshes = Quads.GetMeshes(tempTex.width, tempTex.height);
			RenderTexture.active = tempTex;
			GL.Clear(false, true, new Color((float)0, (float)0, (float)0, (float)0));
			GL.PushMatrix();
			GL.LoadIdentity();
			bokehInfo.filterMode = FilterMode.Point;
			float num = (float)bokehInfo.width * 1f / ((float)bokehInfo.height * 1f);
			float num2 = 2f / (1f * (float)bokehInfo.width);
			num2 += this.bokehScale * this.maxBlurSpread * DepthOfField34.BOKEH_EXTRA_BLUR * this.oneOverBaseSize;
			this.bokehMaterial.SetTexture("_Source", bokehInfo);
			this.bokehMaterial.SetTexture("_MainTex", this.bokehTexture);
			this.bokehMaterial.SetVector("_ArScale", new Vector4(num2, num2 * num, 0.5f, 0.5f * num));
			this.bokehMaterial.SetFloat("_Intensity", this.bokehIntensity);
			this.bokehMaterial.SetPass(0);
			int i = 0;
			Mesh[] array = meshes;
			int length = array.Length;
			while (i < length)
			{
				if (array[i])
				{
					Graphics.DrawMeshNow(array[i], Matrix4x4.identity);
				}
				i++;
			}
			GL.PopMatrix();
			Graphics.Blit(tempTex, finalTarget, this.dofMaterial, 8);
			bokehInfo.filterMode = FilterMode.Bilinear;
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00006F90 File Offset: 0x00005190
	public virtual void ReleaseTextures()
	{
		if (this.foregroundTexture)
		{
			RenderTexture.ReleaseTemporary(this.foregroundTexture);
		}
		if (this.finalDefocus)
		{
			RenderTexture.ReleaseTemporary(this.finalDefocus);
		}
		if (this.mediumRezWorkTexture)
		{
			RenderTexture.ReleaseTemporary(this.mediumRezWorkTexture);
		}
		if (this.lowRezWorkTexture)
		{
			RenderTexture.ReleaseTemporary(this.lowRezWorkTexture);
		}
		if (this.bokehSource)
		{
			RenderTexture.ReleaseTemporary(this.bokehSource);
		}
		if (this.bokehSource2)
		{
			RenderTexture.ReleaseTemporary(this.bokehSource2);
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00007040 File Offset: 0x00005240
	public virtual void AllocateTextures(bool blurForeground, RenderTexture source, int divider, int lowTexDivider)
	{
		this.foregroundTexture = null;
		if (blurForeground)
		{
			this.foregroundTexture = RenderTexture.GetTemporary(source.width, source.height, 0);
		}
		this.mediumRezWorkTexture = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
		this.finalDefocus = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
		this.lowRezWorkTexture = RenderTexture.GetTemporary(source.width / lowTexDivider, source.height / lowTexDivider, 0);
		this.bokehSource = null;
		this.bokehSource2 = null;
		if (this.bokeh)
		{
			this.bokehSource = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
			this.bokehSource2 = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
			this.bokehSource.filterMode = FilterMode.Bilinear;
			this.bokehSource2.filterMode = FilterMode.Bilinear;
			RenderTexture.active = this.bokehSource2;
			GL.Clear(false, true, new Color((float)0, (float)0, (float)0, (float)0));
		}
		source.filterMode = FilterMode.Bilinear;
		this.finalDefocus.filterMode = FilterMode.Bilinear;
		this.mediumRezWorkTexture.filterMode = FilterMode.Bilinear;
		this.lowRezWorkTexture.filterMode = FilterMode.Bilinear;
		if (this.foregroundTexture)
		{
			this.foregroundTexture.filterMode = FilterMode.Bilinear;
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000071CC File Offset: 0x000053CC
	public override void Main()
	{
	}

	// Token: 0x0400010B RID: 267
	[NonSerialized]
	private static int SMOOTH_DOWNSAMPLE_PASS = 6;

	// Token: 0x0400010C RID: 268
	[NonSerialized]
	private static float BOKEH_EXTRA_BLUR = 2f;

	// Token: 0x0400010D RID: 269
	public Dof34QualitySetting quality;

	// Token: 0x0400010E RID: 270
	public DofResolution resolution;

	// Token: 0x0400010F RID: 271
	public bool simpleTweakMode;

	// Token: 0x04000110 RID: 272
	public float focalPoint;

	// Token: 0x04000111 RID: 273
	public float smoothness;

	// Token: 0x04000112 RID: 274
	public float focalZDistance;

	// Token: 0x04000113 RID: 275
	public float focalZStartCurve;

	// Token: 0x04000114 RID: 276
	public float focalZEndCurve;

	// Token: 0x04000115 RID: 277
	private float focalStartCurve;

	// Token: 0x04000116 RID: 278
	private float focalEndCurve;

	// Token: 0x04000117 RID: 279
	private float focalDistance01;

	// Token: 0x04000118 RID: 280
	public Transform objectFocus;

	// Token: 0x04000119 RID: 281
	public float focalSize;

	// Token: 0x0400011A RID: 282
	public DofBlurriness bluriness;

	// Token: 0x0400011B RID: 283
	public float maxBlurSpread;

	// Token: 0x0400011C RID: 284
	public float foregroundBlurExtrude;

	// Token: 0x0400011D RID: 285
	public Shader dofBlurShader;

	// Token: 0x0400011E RID: 286
	private Material dofBlurMaterial;

	// Token: 0x0400011F RID: 287
	public Shader dofShader;

	// Token: 0x04000120 RID: 288
	private Material dofMaterial;

	// Token: 0x04000121 RID: 289
	public bool visualize;

	// Token: 0x04000122 RID: 290
	public BokehDestination bokehDestination;

	// Token: 0x04000123 RID: 291
	private float widthOverHeight;

	// Token: 0x04000124 RID: 292
	private float oneOverBaseSize;

	// Token: 0x04000125 RID: 293
	public bool bokeh;

	// Token: 0x04000126 RID: 294
	public bool bokehSupport;

	// Token: 0x04000127 RID: 295
	public Shader bokehShader;

	// Token: 0x04000128 RID: 296
	public Texture2D bokehTexture;

	// Token: 0x04000129 RID: 297
	public float bokehScale;

	// Token: 0x0400012A RID: 298
	public float bokehIntensity;

	// Token: 0x0400012B RID: 299
	public float bokehThreshholdContrast;

	// Token: 0x0400012C RID: 300
	public float bokehThreshholdLuminance;

	// Token: 0x0400012D RID: 301
	public int bokehDownsample;

	// Token: 0x0400012E RID: 302
	private Material bokehMaterial;

	// Token: 0x0400012F RID: 303
	private RenderTexture foregroundTexture;

	// Token: 0x04000130 RID: 304
	private RenderTexture mediumRezWorkTexture;

	// Token: 0x04000131 RID: 305
	private RenderTexture finalDefocus;

	// Token: 0x04000132 RID: 306
	private RenderTexture lowRezWorkTexture;

	// Token: 0x04000133 RID: 307
	private RenderTexture bokehSource;

	// Token: 0x04000134 RID: 308
	private RenderTexture bokehSource2;
}
