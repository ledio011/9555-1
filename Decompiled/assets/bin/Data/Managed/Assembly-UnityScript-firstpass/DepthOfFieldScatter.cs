using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Depth of Field (HDR, Scatter, Lens Blur)")]
[ExecuteInEditMode]
[Serializable]
public class DepthOfFieldScatter : PostEffectsBase
{
	// Token: 0x06000080 RID: 128 RVA: 0x000071D0 File Offset: 0x000053D0
	public DepthOfFieldScatter()
	{
		this.focalLength = 10f;
		this.focalSize = 0.05f;
		this.aperture = 10f;
		this.maxBlurSize = 2f;
		this.blurQuality = DepthOfFieldScatter.BlurQuality.Medium;
		this.blurResolution = DepthOfFieldScatter.BlurResolution.Low;
		this.foregroundOverlap = 0.55f;
		this.focalDistance01 = 10f;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00007234 File Offset: 0x00005434
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.dofHdrMaterial = this.CheckShaderAndCreateMaterial(this.dofHdrShader, this.dofHdrMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00007270 File Offset: 0x00005470
	public virtual float FocalDistance01(float worldDist)
	{
		return this.camera.WorldToViewportPoint((worldDist - this.camera.nearClipPlane) * this.camera.transform.forward + this.camera.transform.position).z / (this.camera.farClipPlane - this.camera.nearClipPlane);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000072E0 File Offset: 0x000054E0
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = this.maxBlurSize;
			int num2 = (this.blurResolution != DepthOfFieldScatter.BlurResolution.High) ? 2 : 1;
			if (this.aperture < (float)0)
			{
				this.aperture = (float)0;
			}
			if (this.maxBlurSize < (float)0)
			{
				this.maxBlurSize = (float)0;
			}
			this.focalSize = Mathf.Clamp(this.focalSize, (float)0, 0.3f);
			this.focalDistance01 = ((!this.focalTransform) ? this.FocalDistance01(this.focalLength) : (this.camera.WorldToViewportPoint(this.focalTransform.position).z / this.camera.farClipPlane));
			bool flag = source.format == RenderTextureFormat.ARGBHalf;
			RenderTexture renderTexture = (num2 <= 1) ? null : RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, source.format);
			if (renderTexture)
			{
				renderTexture.filterMode = FilterMode.Bilinear;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / (2 * num2), source.height / (2 * num2), 0, source.format);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / (2 * num2), source.height / (2 * num2), 0, source.format);
			if (temporary)
			{
				temporary.filterMode = FilterMode.Bilinear;
			}
			if (temporary2)
			{
				temporary2.filterMode = FilterMode.Bilinear;
			}
			this.dofHdrMaterial.SetVector("_CurveParams", new Vector4((float)0, this.focalSize, this.aperture / 10f, this.focalDistance01));
			if (this.foregroundBlur)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / (2 * num2), source.height / (2 * num2), 0, source.format);
				Graphics.Blit(source, temporary2, this.dofHdrMaterial, 4);
				this.dofHdrMaterial.SetTexture("_FgOverlap", temporary2);
				float num3 = num * this.foregroundOverlap * 0.225f;
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, num3, (float)0, num3));
				Graphics.Blit(temporary2, temporary3, this.dofHdrMaterial, 2);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num3, (float)0, (float)0, num3));
				Graphics.Blit(temporary3, temporary, this.dofHdrMaterial, 2);
				this.dofHdrMaterial.SetTexture("_FgOverlap", null);
				Graphics.Blit(temporary, source, this.dofHdrMaterial, 7);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				this.dofHdrMaterial.SetTexture("_FgOverlap", null);
			}
			Graphics.Blit(source, source, this.dofHdrMaterial, (!this.foregroundBlur) ? 0 : 3);
			RenderTexture renderTexture2 = source;
			if (num2 > 1)
			{
				Graphics.Blit(source, renderTexture, this.dofHdrMaterial, 6);
				renderTexture2 = renderTexture;
			}
			Graphics.Blit(renderTexture2, temporary2, this.dofHdrMaterial, 6);
			Graphics.Blit(temporary2, renderTexture2, this.dofHdrMaterial, 8);
			int pass = 10;
			DepthOfFieldScatter.BlurQuality blurQuality = this.blurQuality;
			if (blurQuality == DepthOfFieldScatter.BlurQuality.Low)
			{
				pass = ((num2 <= 1) ? 10 : 13);
			}
			else if (blurQuality == DepthOfFieldScatter.BlurQuality.Medium)
			{
				pass = ((num2 <= 1) ? 11 : 12);
			}
			else if (blurQuality == DepthOfFieldScatter.BlurQuality.High)
			{
				pass = ((num2 <= 1) ? 14 : 15);
			}
			else
			{
				Debug.Log("DOF couldn't find valid blur quality setting", this.transform);
			}
			if (this.visualizeFocus)
			{
				Graphics.Blit(source, destination, this.dofHdrMaterial, 1);
			}
			else
			{
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, (float)0, (float)0, num));
				this.dofHdrMaterial.SetTexture("_LowRez", renderTexture2);
				Graphics.Blit(source, destination, this.dofHdrMaterial, pass);
			}
			if (temporary)
			{
				RenderTexture.ReleaseTemporary(temporary);
			}
			if (temporary2)
			{
				RenderTexture.ReleaseTemporary(temporary2);
			}
			if (renderTexture)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000076FC File Offset: 0x000058FC
	public override void Main()
	{
	}

	// Token: 0x04000135 RID: 309
	public bool visualizeFocus;

	// Token: 0x04000136 RID: 310
	public float focalLength;

	// Token: 0x04000137 RID: 311
	public float focalSize;

	// Token: 0x04000138 RID: 312
	public float aperture;

	// Token: 0x04000139 RID: 313
	public Transform focalTransform;

	// Token: 0x0400013A RID: 314
	public float maxBlurSize;

	// Token: 0x0400013B RID: 315
	public DepthOfFieldScatter.BlurQuality blurQuality;

	// Token: 0x0400013C RID: 316
	public DepthOfFieldScatter.BlurResolution blurResolution;

	// Token: 0x0400013D RID: 317
	public bool foregroundBlur;

	// Token: 0x0400013E RID: 318
	public float foregroundOverlap;

	// Token: 0x0400013F RID: 319
	public Shader dofHdrShader;

	// Token: 0x04000140 RID: 320
	private float focalDistance01;

	// Token: 0x04000141 RID: 321
	private Material dofHdrMaterial;

	// Token: 0x02000021 RID: 33
	[Serializable]
	public enum BlurQuality
	{
		// Token: 0x04000143 RID: 323
		Low,
		// Token: 0x04000144 RID: 324
		Medium,
		// Token: 0x04000145 RID: 325
		High
	}

	// Token: 0x02000022 RID: 34
	[Serializable]
	public enum BlurResolution
	{
		// Token: 0x04000147 RID: 327
		High,
		// Token: 0x04000148 RID: 328
		Low
	}
}
