using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Noise And Grain (Overlay)")]
[RequireComponent(typeof(Camera))]
[Serializable]
public class NoiseAndGrain : PostEffectsBase
{
	// Token: 0x06000095 RID: 149 RVA: 0x00007E08 File Offset: 0x00006008
	public NoiseAndGrain()
	{
		this.strength = 1f;
		this.blackIntensity = 1f;
		this.whiteIntensity = 1f;
		this.redChannelNoise = 0.975f;
		this.greenChannelNoise = 0.875f;
		this.blueChannelNoise = 1.2f;
		this.redChannelTiling = 24f;
		this.greenChannelTiling = 28f;
		this.blueChannelTiling = 34f;
		this.filterMode = FilterMode.Bilinear;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00007E88 File Offset: 0x00006088
	public virtual void OnDisable()
	{
		if (this.noiseMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.noiseMaterial);
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00007EA8 File Offset: 0x000060A8
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.noiseMaterial = this.CheckShaderAndCreateMaterial(this.noiseShader, this.noiseMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00007EE4 File Offset: 0x000060E4
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.noiseMaterial.SetVector("_NoisePerChannel", new Vector3(this.redChannelNoise, this.greenChannelNoise, this.blueChannelNoise));
			this.noiseMaterial.SetVector("_NoiseTilingPerChannel", new Vector3(this.redChannelTiling, this.greenChannelTiling, this.blueChannelTiling));
			this.noiseMaterial.SetVector("_NoiseAmount", new Vector3(this.strength, this.blackIntensity, this.whiteIntensity));
			this.noiseMaterial.SetTexture("_NoiseTex", this.noiseTexture);
			this.noiseTexture.filterMode = this.filterMode;
			NoiseAndGrain.DrawNoiseQuadGrid(source, destination, this.noiseMaterial, this.noiseTexture, 0);
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00007FC8 File Offset: 0x000061C8
	public static void DrawNoiseQuadGrid(RenderTexture source, RenderTexture dest, Material fxMaterial, Texture2D noise, int passNr)
	{
		RenderTexture.active = dest;
		float num = (float)noise.width * 1f;
		float num2 = num;
		float num3 = 1f * (float)source.width / num2;
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		float num4 = 1f * (float)source.width / (1f * (float)source.height);
		float num5 = 1f / num3;
		float num6 = num5 * num4;
		float num7 = num2 / ((float)noise.width * 1f);
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		for (float num8 = (float)0; num8 < 1f; num8 += num5)
		{
			for (float num9 = (float)0; num9 < 1f; num9 += num6)
			{
				float num10 = UnityEngine.Random.Range((float)0, 1f);
				float num11 = UnityEngine.Random.Range((float)0, 1f);
				num10 = Mathf.Floor(num10 * num) / num;
				num11 = Mathf.Floor(num11 * num) / num;
				float num12 = 1f / num;
				GL.MultiTexCoord2(0, num10, num11);
				GL.MultiTexCoord2(1, (float)0, (float)0);
				GL.Vertex3(num8, num9, 0.1f);
				GL.MultiTexCoord2(0, num10 + num7 * num12, num11);
				GL.MultiTexCoord2(1, 1f, (float)0);
				GL.Vertex3(num8 + num5, num9, 0.1f);
				GL.MultiTexCoord2(0, num10 + num7 * num12, num11 + num7 * num12);
				GL.MultiTexCoord2(1, 1f, 1f);
				GL.Vertex3(num8 + num5, num9 + num6, 0.1f);
				GL.MultiTexCoord2(0, num10, num11 + num7 * num12);
				GL.MultiTexCoord2(1, (float)0, 1f);
				GL.Vertex3(num8, num9 + num6, 0.1f);
			}
		}
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00008198 File Offset: 0x00006398
	public override void Main()
	{
	}

	// Token: 0x04000168 RID: 360
	public float strength;

	// Token: 0x04000169 RID: 361
	public float blackIntensity;

	// Token: 0x0400016A RID: 362
	public float whiteIntensity;

	// Token: 0x0400016B RID: 363
	public float redChannelNoise;

	// Token: 0x0400016C RID: 364
	public float greenChannelNoise;

	// Token: 0x0400016D RID: 365
	public float blueChannelNoise;

	// Token: 0x0400016E RID: 366
	public float redChannelTiling;

	// Token: 0x0400016F RID: 367
	public float greenChannelTiling;

	// Token: 0x04000170 RID: 368
	public float blueChannelTiling;

	// Token: 0x04000171 RID: 369
	public FilterMode filterMode;

	// Token: 0x04000172 RID: 370
	public Shader noiseShader;

	// Token: 0x04000173 RID: 371
	public Texture2D noiseTexture;

	// Token: 0x04000174 RID: 372
	private Material noiseMaterial;
}
