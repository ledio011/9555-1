using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Sun Shafts")]
[Serializable]
public class SunShafts : PostEffectsBase
{
	// Token: 0x060000BC RID: 188 RVA: 0x00009100 File Offset: 0x00007300
	public SunShafts()
	{
		this.resolution = SunShaftsResolution.Normal;
		this.screenBlendMode = ShaftsScreenBlendMode.Screen;
		this.radialBlurIterations = 2;
		this.sunColor = Color.white;
		this.sunShaftBlurRadius = 2.5f;
		this.sunShaftIntensity = 1.15f;
		this.useSkyBoxAlpha = 0.75f;
		this.maxRadius = 0.75f;
		this.useDepthTexture = true;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00009168 File Offset: 0x00007368
	public virtual void OnDisable()
	{
		if (this.sunShaftsMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.sunShaftsMaterial);
		}
		if (this.simpleClearMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.simpleClearMaterial);
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000091AC File Offset: 0x000073AC
	public override bool CheckResources()
	{
		this.CheckSupport(this.useDepthTexture);
		this.sunShaftsMaterial = this.CheckShaderAndCreateMaterial(this.sunShaftsShader, this.sunShaftsMaterial);
		this.simpleClearMaterial = this.CheckShaderAndCreateMaterial(this.simpleClearShader, this.simpleClearMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00009210 File Offset: 0x00007410
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.useDepthTexture)
			{
				this.camera.depthTextureMode = (this.camera.depthTextureMode | DepthTextureMode.Depth);
			}
			float num = 4f;
			if (this.resolution == SunShaftsResolution.Normal)
			{
				num = 2f;
			}
			else if (this.resolution == SunShaftsResolution.High)
			{
				num = 1f;
			}
			Vector3 vector = Vector3.one * 0.5f;
			if (this.sunTransform)
			{
				vector = this.camera.WorldToViewportPoint(this.sunTransform.position);
			}
			else
			{
				vector = new Vector3(0.5f, 0.5f, (float)0);
			}
			RenderTexture temporary = RenderTexture.GetTemporary((int)((float)source.width / num), (int)((float)source.height / num), 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / num), (int)((float)source.height / num), 0);
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(1f, 1f, (float)0, (float)0) * this.sunShaftBlurRadius);
			this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.maxRadius));
			this.sunShaftsMaterial.SetFloat("_NoSkyBoxMask", 1f - this.useSkyBoxAlpha);
			if (!this.useDepthTexture)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(source.width, source.height, 0);
				RenderTexture.active = temporary3;
				GL.ClearWithSkybox(false, this.camera);
				this.sunShaftsMaterial.SetTexture("_Skybox", temporary3);
				Graphics.Blit(source, temporary2, this.sunShaftsMaterial, 3);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				Graphics.Blit(source, temporary2, this.sunShaftsMaterial, 2);
			}
			this.DrawBorder(temporary2, this.simpleClearMaterial);
			this.radialBlurIterations = this.ClampBlurIterationsToSomethingThatMakesSense(this.radialBlurIterations);
			float num2 = this.sunShaftBlurRadius * 0.0013020834f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, (float)0, (float)0));
			this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.maxRadius));
			for (int i = 0; i < this.radialBlurIterations; i++)
			{
				Graphics.Blit(temporary2, temporary, this.sunShaftsMaterial, 1);
				num2 = this.sunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
				this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, (float)0, (float)0));
				Graphics.Blit(temporary, temporary2, this.sunShaftsMaterial, 1);
				num2 = this.sunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
				this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, (float)0, (float)0));
			}
			if (vector.z >= (float)0)
			{
				this.sunShaftsMaterial.SetVector("_SunColor", new Vector4(this.sunColor.r, this.sunColor.g, this.sunColor.b, this.sunColor.a) * this.sunShaftIntensity);
			}
			else
			{
				this.sunShaftsMaterial.SetVector("_SunColor", Vector4.zero);
			}
			this.sunShaftsMaterial.SetTexture("_ColorBuffer", temporary2);
			Graphics.Blit(source, destination, this.sunShaftsMaterial, (this.screenBlendMode != ShaftsScreenBlendMode.Screen) ? 4 : 0);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary);
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000095D4 File Offset: 0x000077D4
	private int ClampBlurIterationsToSomethingThatMakesSense(int its)
	{
		return (its >= 1) ? ((its <= 4) ? its : 4) : 1;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00009604 File Offset: 0x00007804
	public override void Main()
	{
	}

	// Token: 0x0400018B RID: 395
	public SunShaftsResolution resolution;

	// Token: 0x0400018C RID: 396
	public ShaftsScreenBlendMode screenBlendMode;

	// Token: 0x0400018D RID: 397
	public Transform sunTransform;

	// Token: 0x0400018E RID: 398
	public int radialBlurIterations;

	// Token: 0x0400018F RID: 399
	public Color sunColor;

	// Token: 0x04000190 RID: 400
	public float sunShaftBlurRadius;

	// Token: 0x04000191 RID: 401
	public float sunShaftIntensity;

	// Token: 0x04000192 RID: 402
	public float useSkyBoxAlpha;

	// Token: 0x04000193 RID: 403
	public float maxRadius;

	// Token: 0x04000194 RID: 404
	public bool useDepthTexture;

	// Token: 0x04000195 RID: 405
	public Shader sunShaftsShader;

	// Token: 0x04000196 RID: 406
	private Material sunShaftsMaterial;

	// Token: 0x04000197 RID: 407
	public Shader simpleClearShader;

	// Token: 0x04000198 RID: 408
	private Material simpleClearMaterial;
}
