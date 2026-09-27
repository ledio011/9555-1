using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Tilt shift")]
[RequireComponent(typeof(Camera))]
[Serializable]
public class TiltShift : PostEffectsBase
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00009608 File Offset: 0x00007808
	public TiltShift()
	{
		this.renderTextureDivider = 2;
		this.blurIterations = 2;
		this.enableForegroundBlur = true;
		this.foregroundBlurIterations = 2;
		this.maxBlurSpread = 1.5f;
		this.focalPoint = 30f;
		this.smoothness = 1.65f;
		this.distance01 = 0.2f;
		this.end01 = 1f;
		this.curve = 1f;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000967C File Offset: 0x0000787C
	public virtual void OnDisable()
	{
		if (this.tiltShiftMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.tiltShiftMaterial);
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000969C File Offset: 0x0000789C
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.tiltShiftMaterial = this.CheckShaderAndCreateMaterial(this.tiltShiftShader, this.tiltShiftMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000096D8 File Offset: 0x000078D8
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
			this.renderTextureDivider = ((this.renderTextureDivider >= 1) ? this.renderTextureDivider : 1);
			this.renderTextureDivider = ((this.renderTextureDivider <= 4) ? this.renderTextureDivider : 4);
			this.blurIterations = ((this.blurIterations >= 1) ? this.blurIterations : 0);
			this.blurIterations = ((this.blurIterations <= 4) ? this.blurIterations : 4);
			float num3 = this.camera.WorldToViewportPoint(this.focalPoint * this.camera.transform.forward + this.camera.transform.position).z / this.camera.farClipPlane;
			this.distance01 = num3;
			this.start01 = (float)0;
			this.end01 = 1f;
			this.start01 = Mathf.Min(num3 - float.Epsilon, this.start01);
			this.end01 = Mathf.Max(num3 + float.Epsilon, this.end01);
			this.curve = this.smoothness * this.distance01;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / this.renderTextureDivider, source.height / this.renderTextureDivider, 0);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / this.renderTextureDivider, source.height / this.renderTextureDivider, 0);
			this.tiltShiftMaterial.SetVector("_SimpleDofParams", new Vector4(this.start01, this.distance01, this.end01, this.curve));
			this.tiltShiftMaterial.SetTexture("_Coc", temporary);
			if (this.enableForegroundBlur)
			{
				Graphics.Blit(source, temporary, this.tiltShiftMaterial, 0);
				Graphics.Blit(temporary, temporary3);
				for (int i = 0; i < this.foregroundBlurIterations; i++)
				{
					this.tiltShiftMaterial.SetVector("offsets", new Vector4((float)0, this.maxBlurSpread * 0.75f * num2, (float)0, (float)0));
					Graphics.Blit(temporary3, temporary4, this.tiltShiftMaterial, 3);
					this.tiltShiftMaterial.SetVector("offsets", new Vector4(this.maxBlurSpread * 0.75f / num * num2, (float)0, (float)0, (float)0));
					Graphics.Blit(temporary4, temporary3, this.tiltShiftMaterial, 3);
				}
				Graphics.Blit(temporary3, temporary2, this.tiltShiftMaterial, 7);
				this.tiltShiftMaterial.SetTexture("_Coc", temporary2);
			}
			else
			{
				RenderTexture.active = temporary;
				GL.Clear(false, true, Color.black);
			}
			Graphics.Blit(source, temporary, this.tiltShiftMaterial, 5);
			this.tiltShiftMaterial.SetTexture("_Coc", temporary);
			Graphics.Blit(source, temporary4);
			for (int j = 0; j < this.blurIterations; j++)
			{
				this.tiltShiftMaterial.SetVector("offsets", new Vector4((float)0, this.maxBlurSpread * 1f * num2, (float)0, (float)0));
				Graphics.Blit(temporary4, temporary3, this.tiltShiftMaterial, 6);
				this.tiltShiftMaterial.SetVector("offsets", new Vector4(this.maxBlurSpread * 1f / num * num2, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary3, temporary4, this.tiltShiftMaterial, 6);
			}
			this.tiltShiftMaterial.SetTexture("_Blurred", temporary4);
			Graphics.Blit(source, destination, this.tiltShiftMaterial, (!this.visualizeCoc) ? 1 : 4);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00009AD8 File Offset: 0x00007CD8
	public override void Main()
	{
	}

	// Token: 0x04000199 RID: 409
	public Shader tiltShiftShader;

	// Token: 0x0400019A RID: 410
	private Material tiltShiftMaterial;

	// Token: 0x0400019B RID: 411
	public int renderTextureDivider;

	// Token: 0x0400019C RID: 412
	public int blurIterations;

	// Token: 0x0400019D RID: 413
	public bool enableForegroundBlur;

	// Token: 0x0400019E RID: 414
	public int foregroundBlurIterations;

	// Token: 0x0400019F RID: 415
	public float maxBlurSpread;

	// Token: 0x040001A0 RID: 416
	public float focalPoint;

	// Token: 0x040001A1 RID: 417
	public float smoothness;

	// Token: 0x040001A2 RID: 418
	public bool visualizeCoc;

	// Token: 0x040001A3 RID: 419
	private float start01;

	// Token: 0x040001A4 RID: 420
	private float distance01;

	// Token: 0x040001A5 RID: 421
	private float end01;

	// Token: 0x040001A6 RID: 422
	private float curve;
}
